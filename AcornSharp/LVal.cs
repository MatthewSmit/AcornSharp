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
        [ContractAnnotation("node:notnull=>notnull")]
        private BaseNode toAssignable([CanBeNull] BaseNode node, bool isBinding = false)
        {
            if (Options.ecmaVersion >= 6 && node != null)
            {
                switch (node)
                {
                    case IdentifierNode identifierNode:
                        if (inAsync && identifierNode.name == "await")
                            raise(node.location.Start, "Can not use 'await' as identifier inside an async function");
                        break;

                    case MemberExpressionNode _:
                        if (!isBinding) break;
                        goto default;

                    case ObjectPatternNode _:
                    case ArrayPatternNode _:
                        break;

                    case ObjectExpressionNode objectExpression:
                        node = new ObjectPatternNode(this, node.location.Start, node.location.End)
                        {
                            properties = objectExpression.properties
                        };
                        foreach (var prop in objectExpression.properties)
                        {
                            if (prop.kind != PropertyKind.Initialise) raise(prop.key.location.Start, "Object pattern can't contain getter or setter");
                            prop.value = toAssignable(prop.value, isBinding);
                        }
                        break;

                    case ArrayExpressionNode arrayExpression:
                        node = new ArrayPatternNode(this, node.location.Start, node.location.End)
                        {
                            elements = toAssignableList(arrayExpression.elements, isBinding)
                        };
                        break;

                    case AssignmentExpressionNode assignmentExpression:
                        if (assignmentExpression.@operator == Operator.Assignment)
                        {
                            node = new AssignmentPatternNode(this, node.location.Start, node.location.End)
                            {
                                left = toAssignable(assignmentExpression.left, isBinding),
                                right = assignmentExpression.right
                            };
                            goto AssignmentPatternNode;
                        }
                        else
                        {
                            raise(assignmentExpression.left.location.End, "Only '=' operator can be used for specifying default value.");
                            break;
                        }

                    case AssignmentPatternNode _:
                        AssignmentPatternNode:
                        break;

                    case ParenthesisedExpressionNode parenthesisedExpressionNode:
                        parenthesisedExpressionNode.expression = toAssignable(parenthesisedExpressionNode.expression, isBinding);
                        break;

                    default:
                        raise(node.location.Start, "Assigning to rvalue");
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
                if (last is RestElementNode)
                {
                    --end;
                }
                else if (last is SpreadElementNode spreadElement)
                {
                    exprList[end - 1] = last = new RestElementNode(this, last.location.Start, last.location.End)
                    {
                        argument = toAssignable(spreadElement.argument, isBinding)
                    };
                    --end;
                }

                if (Options.ecmaVersion == 6 && isBinding && last is RestElementNode restElementNode && !(restElementNode.argument is IdentifierNode))
                {
                    raise(restElementNode.argument.location.Start, "Unexpected token");
                }
            }
            for (var i = 0; i < end; i++)
            {
                if (exprList[i] != null)
                    exprList[i] = toAssignable(exprList[i], isBinding);
            }
            return exprList;
        }

        // Parses spread element.
        [NotNull]
        private SpreadElementNode parseSpread(DestructuringErrors refDestructuringErrors)
        {
            var startLoc = start;
            next();
            var argument = parseMaybeAssign(false, refDestructuringErrors);
            return new SpreadElementNode(this, startLoc, lastTokEnd)
            {
                argument = argument
            };
        }

        [NotNull]
        private BaseNode parseRestBinding()
        {
            var startLoc = start;
            next();

            // RestElement inside of a function parameter must be an identifier
            if (Options.ecmaVersion == 6 && type != TokenType.name)
            {
                raise(start, "Unexpected token");
            }

            var argument = parseBindingAtom();

            return new RestElementNode(this, startLoc, lastTokEnd)
            {
                argument = argument
            };
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
                        var startLoc = start;
                        next();
                        var elements = parseBindingList(TokenType.bracketR, true, true);
                        return new ArrayPatternNode(this, startLoc, lastTokEnd)
                        {
                            elements = elements
                        };

                    case TokenType.braceL:
                        return parseObj(true);
                }
            }
            return parseIdent();
        }

        [NotNull]
        private List<BaseNode> parseBindingList(TokenType close, bool allowEmpty, bool allowTrailingComma)
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
        [NotNull]
        private BaseNode parseMaybeDefault(Position startLoc, BaseNode left = null)
        {
            left = left ?? parseBindingAtom();
            if (Options.ecmaVersion < 6 || !eat(TokenType.eq)) return left;
            var right = parseMaybeAssign();
            return new AssignmentPatternNode(this, startLoc, lastTokEnd)
            {
                left = left,
                right = right
            };
        }

        // Verify that a node is an lval — something that can be assigned
        // to.
        // bindingType can be either:
        // var indicating that the lval creates a 'var' binding
        // let indicating that the lval creates a lexical ('let' or 'const') binding
        // null indicating that the binding should be checked for illegal identifiers, but not for duplicate references
        private void checkLVal([NotNull] BaseNode expr, bool isBinding, VariableKind? bindingType, [CanBeNull] ISet<string> checkClashes = null)
        {
            switch (expr)
            {
                case IdentifierNode identifierNode:
                    if (strict && reservedWordsStrictBind.IsMatch(identifierNode.name))
                        raiseRecoverable(expr.location.Start, (isBinding ? "Binding " : "Assigning to ") + identifierNode.name + " in strict mode");
                    if (checkClashes != null)
                    {
                        if (checkClashes.Contains(identifierNode.name))
                            raiseRecoverable(expr.location.Start, "Argument name clash");
                        checkClashes.Add(identifierNode.name);
                    }
                    if (bindingType != null && isBinding)
                    {
                        if (bindingType == VariableKind.Var && !canDeclareVarName(identifierNode.name) ||
                            bindingType != VariableKind.Var && !canDeclareLexicalName(identifierNode.name))
                        {
                            raiseRecoverable(expr.location.Start, $"Identifier '{identifierNode.name}' has already been declared");
                        }
                        if (bindingType == VariableKind.Var)
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
                    if (bindingType != null) raiseRecoverable(expr.location.Start, "Binding" + " member expression");
                    break;
                case ObjectPatternNode objectPattern:
                    foreach (var prop in objectPattern.properties)
                    {
                        checkLVal(prop.value, isBinding, bindingType, checkClashes);
                    }
                    break;
                case ArrayPatternNode arrayPattern:
                    foreach (var elem in arrayPattern.elements)
                    {
                        if (elem != null)
                        {
                            checkLVal(elem, isBinding, bindingType, checkClashes);
                        }
                    }
                    break;
                case AssignmentPatternNode assignmentPattern:
                    checkLVal(assignmentPattern.left, isBinding, bindingType, checkClashes);
                    break;
                case RestElementNode restElement:
                    checkLVal(restElement.argument, isBinding, bindingType, checkClashes);
                    break;
                case ParenthesisedExpressionNode parenthesisedExpressionNode:
                    checkLVal(parenthesisedExpressionNode.expression, isBinding, bindingType, checkClashes);
                    break;
                default:
                    raise(expr.location.Start, (bindingType != null ? "Binding" : "Assigning to") + " rvalue");
                    break;
            }
        }
    }
}
