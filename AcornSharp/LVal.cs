using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AcornSharp
{
    [SuppressMessage("ReSharper", "LocalVariableHidesMember")]
    public sealed partial class Parser
    {
        // Convert existing expression atom to assignable pattern
        // if possible.
        private Node toAssignable(Node node, bool isBinding = false)
        {
            if (Options.ecmaVersion >= 6 && node != null)
            {
                switch (node.type)
                {
                    case "Identifier":
                        if (inAsync && node.name == "await")
                            raise(node.start, "Can not use 'await' as identifier inside an async function");
                        break;

                    case "ObjectPattern":
                    case "ArrayPattern":
                        break;

                    case "ObjectExpression":
                        node.type = "ObjectPattern";
                        foreach (var prop in node.properties)
                        {
                            if (prop.kind != "init") raise(prop.key.start, "Object pattern can't contain getter or setter");
                            toAssignable((Node)prop.value, isBinding);
                        }
                        break;

                    case "ArrayExpression":
                        node.type = "ArrayPattern";
                        toAssignableList(node.elements, isBinding);
                        break;

                    case "AssignmentExpression":
                        if (node.@operator == "=")
                        {
                            node.type = "AssignmentPattern";
                            node.@operator = null;
                            toAssignable(node.left, isBinding);
                            goto case "AssignmentPattern";
                        }
                        else
                        {
                            raise(node.left.end, "Only '=' operator can be used for specifying default value.");
                            break;
                        }

                    case "AssignmentPattern":
                        break;

                    case "ParenthesizedExpression":
                        toAssignable(node.expression, isBinding);
                        break;

                    case "MemberExpression":
                        if (!isBinding) break;
                        goto default;

                    default:
                        raise(node.start, "Assigning to rvalue");
                        break;
                }
            }
            return node;
        }

        // Convert list of expression atoms to binding list.
        private IList<Node> toAssignableList(IList<Node> exprList, bool isBinding)
        {
            var end = exprList.Count;
            if (end != 0)
            {
                var last = exprList[end - 1];
                if (last != null && last.type == "RestElement")
                {
                    --end;
                }
                else if (last != null && last.type == "SpreadElement")
                {
                    last.type = "RestElement";
                    var arg = last.argument;
                    toAssignable(arg, isBinding);
                    --end;
                }

                if (Options.ecmaVersion == 6 && isBinding && last != null && last.type == "RestElement" && last.argument.type != "Identifier")
                    unexpected(last.argument.start);
            }
            for (var i = 0; i < end; i++)
            {
                var elt = exprList[i];
                if (elt != null)
                    toAssignable(elt, isBinding);
            }
            return exprList;
        }

        // Parses spread element.
        private Node parseSpread(DestructuringErrors refDestructuringErrors)
        {
            var node = startNode();
            next();
            node.argument = parseMaybeAssign(false, refDestructuringErrors);
            return finishNode(node, "SpreadElement");
        }

        private Node parseRestBinding()
        {
            var node = startNode();
            next();

            // RestElement inside of a function parameter must be an identifier
            if (Options.ecmaVersion == 6 && type != TokenType.name)
                unexpected();

            node.argument = parseBindingAtom();

            return finishNode(node, "RestElement");
        }

        // Parses lvalue (assignable) atom.
        private Node parseBindingAtom()
        {
            if (Options.ecmaVersion < 6) return parseIdent();
            if (type == TokenType.name)
            {
                return parseIdent();
            }
            if (type == TokenType.bracketL)
            {
                var node = startNode();
                next();
                node.elements = parseBindingList(TokenType.bracketR, true, true);
                return finishNode(node, "ArrayPattern");
            }
            if (type == TokenType.braceL)
            {
                return parseObj(true);
            }

            unexpected();
            return null;
        }

        private IList<Node> parseBindingList(TokenType close, bool allowEmpty, bool allowTrailingComma)
        {
            var elts = new List<Node>();
            var first = true;
            while (!eat(close))
            {
                if (first) first = false;
                else expect(TokenType.comma);
                if (allowEmpty && type == TokenType.comma)
                {
                    elts.Add(null);
                }
                else if (allowTrailingComma && afterTrailingComma(close))
                {
                    break;
                }
                else if (type == TokenType.ellipsis)
                {
                    var rest = parseRestBinding();
                    parseBindingListItem(rest);
                    elts.Add(rest);
                    if (type == TokenType.comma) raise(start, "Comma is not permitted after the rest element");
                    expect(close);
                    break;
                }
                else
                {
                    var elem = parseMaybeDefault(start, startLoc);
                    parseBindingListItem(elem);
                    elts.Add(elem);
                }
            }
            return elts;
        }

        private Node parseBindingListItem(Node param)
        {
            return param;
        }

        // Parses assignment pattern around given atom if possible.
        private Node parseMaybeDefault(int startPos, Position startLoc, Node left = null)
        {
            left = left ?? parseBindingAtom();
            if (Options.ecmaVersion < 6 || !eat(TokenType.eq)) return left;
            var node = startNodeAt(startPos, startLoc);
            node.left = left;
            node.right = parseMaybeAssign();
            return finishNode(node, "AssignmentPattern");
        }

        // Verify that a node is an lval — something that can be assigned
        // to.
        // bindingType can be either:
        // 'var' indicating that the lval creates a 'var' binding
        // 'let' indicating that the lval creates a lexical ('let' or 'const') binding
        // 'none' indicating that the binding should be checked for illegal identifiers, but not for duplicate references
        private void checkLVal(Node expr, string bindingType = null, ISet<string> checkClashes = null)
        {
            switch (expr.type)
            {
                case "Identifier":
                    if (strict && reservedWordsStrictBind.IsMatch(expr.name))
                        raiseRecoverable(expr.start, (bindingType != null ? "Binding " : "Assigning to ") + expr.name + " in strict mode");
                    if (checkClashes != null)
                    {
                        if (checkClashes.Contains(expr.name))
                            raiseRecoverable(expr.start, "Argument name clash");
                        checkClashes.Add(expr.name);
                    }
                    if (bindingType != null && bindingType != "none")
                    {
                        if (
                            bindingType == "var" && !canDeclareVarName(expr.name) ||
                            bindingType != "var" && !canDeclareLexicalName(expr.name)
                        )
                        {
                            raiseRecoverable(expr.start, $"Identifier '{expr.name}' has already been declared");
                        }
                        if (bindingType == "var")
                        {
                            declareVarName(expr.name);
                        }
                        else
                        {
                            declareLexicalName(expr.name);
                        }
                    }
                    break;

                case "MemberExpression":
                    if (bindingType != null) raiseRecoverable(expr.start, "Binding" + " member expression");
                    break;

                case "ObjectPattern":
                    foreach (var prop in expr.properties)
                        checkLVal((Node)prop.value, bindingType, checkClashes);
                    break;

                case "ArrayPattern":
                    foreach (var elem in expr.elements)
                    {
                        if (elem != null) checkLVal(elem, bindingType, checkClashes);
                    }
                    break;

                case "AssignmentPattern":
                    checkLVal(expr.left, bindingType, checkClashes);
                    break;

                case "RestElement":
                    checkLVal(expr.argument, bindingType, checkClashes);
                    break;

                case "ParenthesizedExpression":
                    checkLVal(expr.expression, bindingType, checkClashes);
                    break;

                default:
                    raise(expr.start, (bindingType != null ? "Binding" : "Assigning to") + " rvalue");
                    break;
            }
        }
    }
}
