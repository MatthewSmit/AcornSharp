using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AcornSharp.Node;
using JetBrains.Annotations;

namespace AcornSharp
{
    [SuppressMessage("ReSharper", "LocalVariableHidesMember")]
    internal sealed partial class Parser
    {
        // Convert existing expression atom to assignable pattern
        // if possible.
        [ContractAnnotation("node:notnull=>notnull")]
        private ExpressionNode toAssignable([CanBeNull] ExpressionNode node, bool isBinding = false)
        {
            if (Options.ecmaVersion >= 6 && node != null)
            {
                switch (node)
                {
                    case IdentifierNode identifierNode:
                        if (inAsync && identifierNode.Name == "await")
                            raise(node.Location.Start, "Can not use 'await' as identifier inside an async function");
                        break;

                    case MemberExpressionNode _:
                        if (!isBinding) break;
                        goto default;

                    case ObjectPatternNode _:
                    case ArrayPatternNode _:
                        break;

                    case ObjectExpressionNode objectExpression:
                        var newProperties = new PropertyNode[objectExpression.Properties.Count];
                        for (var i = 0; i < newProperties.Length; i++)
                        {
                            var property = objectExpression.Properties[i];
                            if (property.Kind != PropertyKind.Initialise)
                                raise(property.Key.Location.Start, "Object pattern can't contain getter or setter");

                            newProperties[i] = new PropertyNode(this,
                                property.Location.Start,
                                property.Location.End,
                                property.Kind,
                                property.Computed,
                                property.Shorthand,
                                property.Method,
                                property.Key,
                                toAssignable(property.Value, isBinding));
                        }
                        node = new ObjectPatternNode(this, node.Location.Start, node.Location.End, newProperties);
                        break;

                    case ArrayExpressionNode arrayExpression:
                        node = new ArrayPatternNode(this, node.Location.Start, node.Location.End, toAssignableList(arrayExpression.Elements, isBinding));
                        break;

                    case AssignmentExpressionNode assignmentExpression:
                        if (assignmentExpression.Operator == Operator.Assignment)
                        {
                            var left = toAssignable(assignmentExpression.Left, isBinding);
                            var right = assignmentExpression.Right;
                            node = new AssignmentPatternNode(this, node.Location.Start, node.Location.End, left, right);
                            goto AssignmentPatternNode;
                        }
                        else
                        {
                            raise(assignmentExpression.Left.Location.End, "Only '=' operator can be used for specifying default value.");
                            break;
                        }

                    case AssignmentPatternNode _:
                        AssignmentPatternNode:
                        break;

                    case ParenthesisedExpressionNode parenthesisedExpressionNode:
                        node = new ParenthesisedExpressionNode(this, node.Location.Start, node.Location.End, toAssignable(parenthesisedExpressionNode.Expression, isBinding));
                        break;

                    default:
                        raise(node.Location.Start, "Assigning to rvalue");
                        break;
                }
            }
            return node;
        }

        // Convert list of expression atoms to binding list.
        [NotNull]
        private IReadOnlyList<ExpressionNode> toAssignableList([NotNull] IReadOnlyList<ExpressionNode> expressionList, bool isBinding)
        {
            var newList = expressionList.ToArray();
            var end = expressionList.Count;
            if (end != 0)
            {
                var last = expressionList[end - 1];
                if (last is RestElementNode)
                {
                    --end;
                }
                else if (last is SpreadElementNode spreadElement)
                {
                    newList[end - 1] = last = new RestElementNode(this, last.Location.Start, last.Location.End, toAssignable(spreadElement.Argument, isBinding));
                    --end;
                }

                if (Options.ecmaVersion == 6 && isBinding && last is RestElementNode restElementNode && !(restElementNode.Argument is IdentifierNode))
                {
                    raise(restElementNode.Argument.Location.Start, "Unexpected token");
                }
            }

            for (var i = 0; i < end; i++)
            {
                if (newList[i] != null)
                    newList[i] = toAssignable(newList[i], isBinding);
            }

            return newList;
        }

        // Parses spread element.
        [NotNull]
        private SpreadElementNode parseSpread(DestructuringErrors refDestructuringErrors)
        {
            var startLoc = start;
            next();
            var argument = ParseMaybeAssign(false, refDestructuringErrors);
            return new SpreadElementNode(this, startLoc, lastTokEnd, argument);
        }

        [NotNull]
        private ExpressionNode parseRestBinding()
        {
            var startLoc = start;
            next();

            // RestElement inside of a function parameter must be an identifier
            if (Options.ecmaVersion == 6 && type != TokenType.name)
            {
                raise(start, "Unexpected token");
            }

            var argument = parseBindingAtom();
            return new RestElementNode(this, startLoc, lastTokEnd, argument);
        }

        // Parses lvalue (assignable) atom.
        [NotNull]
        private ExpressionNode parseBindingAtom()
        {
            if (Options.ecmaVersion >= 6)
            {
                switch (type)
                {
                    case TokenType.bracketL:
                        var startLoc = start;
                        next();
                        var elements = parseBindingList(TokenType.bracketR, true, true);
                        return new ArrayPatternNode(this, startLoc, lastTokEnd, elements);

                    case TokenType.braceL:
                        return parseObj(true);
                }
            }
            return parseIdent();
        }

        [NotNull]
        private List<ExpressionNode> parseBindingList(TokenType close, bool allowEmpty, bool allowTrailingComma)
        {
            var elts = new List<ExpressionNode>();
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
        private ExpressionNode parseMaybeDefault(Position startLoc, ExpressionNode left = null)
        {
            left = left ?? parseBindingAtom();
            if (Options.ecmaVersion < 6 || !eat(TokenType.eq))
                return left;
            var right = ParseMaybeAssign();
            return new AssignmentPatternNode(this, startLoc, lastTokEnd, left, right);
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
                    if (strict && reservedWordsStrictBind.IsMatch(identifierNode.Name))
                        raiseRecoverable(expr.Location.Start, (isBinding ? "Binding " : "Assigning to ") + identifierNode.Name + " in strict mode");
                    if (checkClashes != null)
                    {
                        if (checkClashes.Contains(identifierNode.Name))
                            raiseRecoverable(expr.Location.Start, "Argument name clash");
                        checkClashes.Add(identifierNode.Name);
                    }
                    if (bindingType != null && isBinding)
                    {
                        if (bindingType == VariableKind.Var && !canDeclareVarName(identifierNode.Name) ||
                            bindingType != VariableKind.Var && !canDeclareLexicalName(identifierNode.Name))
                        {
                            raiseRecoverable(expr.Location.Start, $"Identifier '{identifierNode.Name}' has already been declared");
                        }
                        if (bindingType == VariableKind.Var)
                        {
                            declareVarName(identifierNode.Name);
                        }
                        else
                        {
                            declareLexicalName(identifierNode.Name);
                        }
                    }
                    break;
                case MemberExpressionNode _:
                    if (bindingType != null) raiseRecoverable(expr.Location.Start, "Binding" + " member expression");
                    break;
                case ObjectPatternNode objectPattern:
                    foreach (var prop in objectPattern.Properties)
                    {
                        checkLVal(prop.Value, isBinding, bindingType, checkClashes);
                    }
                    break;
                case ArrayPatternNode arrayPattern:
                    foreach (var elem in arrayPattern.Elements)
                    {
                        if (elem != null)
                        {
                            checkLVal(elem, isBinding, bindingType, checkClashes);
                        }
                    }
                    break;
                case AssignmentPatternNode assignmentPattern:
                    checkLVal(assignmentPattern.Left, isBinding, bindingType, checkClashes);
                    break;
                case RestElementNode restElement:
                    checkLVal(restElement.Argument, isBinding, bindingType, checkClashes);
                    break;
                case ParenthesisedExpressionNode parenthesisedExpressionNode:
                    checkLVal(parenthesisedExpressionNode.Expression, isBinding, bindingType, checkClashes);
                    break;
                default:
                    raise(expr.Location.Start, (bindingType != null ? "Binding" : "Assigning to") + " rvalue");
                    break;
            }
        }
    }
}
