using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AcornSharp.Node;
using JetBrains.Annotations;

namespace AcornSharp
{
    [SuppressMessage("ReSharper", "LocalVariableHidesMember")]
    public sealed partial class Parser
    {
        // Convert existing expression atom to assignable pattern
        // if possible.
        [CanBeNull]
        private BaseNode toAssignable([CanBeNull] BaseNode node, bool isBinding = false)
        {
            if (Options.ecmaVersion >= 6 && node != null)
            {
                switch (node)
                {
                    case IdentifierNode identifierNode:
                        if (inAsync && identifierNode.name == "await")
                            raise(node.loc.Start, "Can not use 'await' as identifier inside an async function");
                        break;

                    case MemberExpressionNode _:
                        if (!isBinding) break;
                        goto default;

                    default:
                        switch (node.type)
                        {
                            case NodeType.ObjectPattern:
                            case NodeType.ArrayPattern:
                                break;

                            case NodeType.ObjectExpression:
                                node.type = NodeType.ObjectPattern;
                                foreach (var prop in node.properties)
                                {
                                    if (prop.kind != "init") raise(prop.key.loc.Start, "Object pattern can't contain getter or setter");
                                    toAssignable((BaseNode)prop.value, isBinding);
                                }
                                break;

                            case NodeType.ArrayExpression:
                                node.type = NodeType.ArrayPattern;
                                toAssignableList(node.elements, isBinding);
                                break;

                            case NodeType.AssignmentExpression:
                                if (node.@operator == "=")
                                {
                                    node.type = NodeType.AssignmentPattern;
                                    node.@operator = null;
                                    toAssignable(node.left, isBinding);
                                    goto case NodeType.AssignmentPattern;
                                }
                                else
                                {
                                    raise(node.left.loc.End, "Only '=' operator can be used for specifying default value.");
                                    break;
                                }

                            case NodeType.AssignmentPattern:
                                break;

                            case NodeType.ParenthesizedExpression:
                                toAssignable(node.expression, isBinding);
                                break;

                            default:
                                raise(node.loc.Start, "Assigning to rvalue");
                                break;
                        }
                        break;
                }
            }
            return node;
        }

        // Convert list of expression atoms to binding list.
        [NotNull]
        private IList<BaseNode> toAssignableList([NotNull] IList<BaseNode> exprList, bool isBinding)
        {
            var end = exprList.Count;
            if (end != 0)
            {
                var last = exprList[end - 1];
                if (last != null && last.type == NodeType.RestElement)
                {
                    --end;
                }
                else if (last != null && last.type == NodeType.SpreadElement)
                {
                    last.type = NodeType.RestElement;
                    var arg = last.argument;
                    toAssignable(arg, isBinding);
                    --end;
                }

                if (Options.ecmaVersion == 6 && isBinding && last != null && last.type == NodeType.RestElement && !(last.argument is IdentifierNode))
                {
                    raise(last.argument.loc.Start, "Unexpected token");
                }
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
        [NotNull]
        private BaseNode parseSpread(DestructuringErrors refDestructuringErrors)
        {
            var node = new BaseNode(this, start);
            next();
            node.argument = parseMaybeAssign(false, refDestructuringErrors);
            node.type = NodeType.SpreadElement;
            node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
            return node;
        }

        [NotNull]
        private BaseNode parseRestBinding()
        {
            var node = new BaseNode(this, start);
            next();

            // RestElement inside of a function parameter must be an identifier
            if (Options.ecmaVersion == 6 && type != TokenType.name)
            {
                raise(start, "Unexpected token");
            }

            node.argument = parseBindingAtom();

            node.type = NodeType.RestElement;
            node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
            return node;
        }

        // Parses lvalue (assignable) atom.
        [NotNull]
        private BaseNode parseBindingAtom()
        {
            if (Options.ecmaVersion >= 6)
            {
                switch (type)
                {
                    case TokenType.bracketL:
                        var node = new BaseNode(this, start);
                        next();
                        node.elements = parseBindingList(TokenType.bracketR, true, true);
                        node.type = NodeType.ArrayPattern;
                        node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
                        return node;

                    case TokenType.braceL:
                        return parseObj(true);
                }
            }
            return parseIdent();
        }

        [NotNull]
        private IList<BaseNode> parseBindingList(TokenType close, bool allowEmpty, bool allowTrailingComma)
        {
            var elts = new List<BaseNode>();
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
                    elts.Add(rest);
                    if (type == TokenType.comma) raise(start, "Comma is not permitted after the rest element");
                    expect(close);
                    break;
                }
                else
                {
                    var elem = parseMaybeDefault(start);
                    elts.Add(elem);
                }
            }
            return elts;
        }

        // Parses assignment pattern around given atom if possible.
        private BaseNode parseMaybeDefault(Position startLoc, BaseNode left = null)
        {
            left = left ?? parseBindingAtom();
            if (Options.ecmaVersion < 6 || !eat(TokenType.eq)) return left;
            var node = new BaseNode(this, startLoc);
            node.left = left;
            node.right = parseMaybeAssign();
            node.type = NodeType.AssignmentPattern;
            node.loc = new SourceLocation(node.loc.Start, lastTokEnd, node.loc.Source);
            return node;
        }

        // Verify that a node is an lval — something that can be assigned
        // to.
        // bindingType can be either:
        // 'var' indicating that the lval creates a 'var' binding
        // 'let' indicating that the lval creates a lexical ('let' or 'const') binding
        // 'none' indicating that the binding should be checked for illegal identifiers, but not for duplicate references
        private void checkLVal([NotNull] BaseNode expr, [CanBeNull] string bindingType = null, [CanBeNull] ISet<string> checkClashes = null)
        {
            switch (expr)
            {
                case IdentifierNode identifierNode:
                    if (strict && reservedWordsStrictBind.IsMatch(identifierNode.name))
                        raiseRecoverable(expr.loc.Start, (bindingType != null ? "Binding " : "Assigning to ") + identifierNode.name + " in strict mode");
                    if (checkClashes != null)
                    {
                        if (checkClashes.Contains(identifierNode.name))
                            raiseRecoverable(expr.loc.Start, "Argument name clash");
                        checkClashes.Add(identifierNode.name);
                    }
                    if (bindingType != null && bindingType != "none")
                    {
                        if (
                            bindingType == "var" && !canDeclareVarName(identifierNode.name) ||
                            bindingType != "var" && !canDeclareLexicalName(identifierNode.name)
                        )
                        {
                            raiseRecoverable(expr.loc.Start, $"Identifier '{identifierNode.name}' has already been declared");
                        }
                        if (bindingType == "var")
                        {
                            declareVarName(identifierNode.name);
                        }
                        else
                        {
                            declareLexicalName(identifierNode.name);
                        }
                    }
                    break;
                case MemberExpressionNode _:
                    if (bindingType != null) raiseRecoverable(expr.loc.Start, "Binding" + " member expression");
                    break;
                default:
                    if (expr.type == NodeType.ObjectPattern)
                    {
                        foreach (var prop in expr.properties)
                            checkLVal((BaseNode)prop.value, bindingType, checkClashes);
                    }
                    else if (expr.type == NodeType.ArrayPattern)
                    {
                        foreach (var elem in expr.elements)
                        {
                            if (elem != null) checkLVal(elem, bindingType, checkClashes);
                        }
                    }
                    else if (expr.type == NodeType.AssignmentPattern)
                    {
                        checkLVal(expr.left, bindingType, checkClashes);
                    }
                    else if (expr.type == NodeType.RestElement)
                    {
                        checkLVal(expr.argument, bindingType, checkClashes);
                    }
                    else if (expr.type == NodeType.ParenthesizedExpression)
                    {
                        checkLVal(expr.expression, bindingType, checkClashes);
                    }
                    else
                    {
                        raise(expr.loc.Start, (bindingType != null ? "Binding" : "Assigning to") + " rvalue");
                    }
                    break;
            }
        }
    }
}
