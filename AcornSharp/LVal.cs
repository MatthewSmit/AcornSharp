using System.Collections.Generic;
using AcornSharp.Nodes;
using JetBrains.Annotations;

namespace AcornSharp
{
    public sealed partial class Parser
    {
        // Convert existing expression atom to assignable pattern
        // if possible.
        [ContractAnnotation("node: null => null; node: notnull => notnull")]
        private ExpressionNode ToAssignable(ref ExpressionNode node, bool isBinding, [CanBeNull] DestructuringErrors refDestructuringErrors = null)
        {
            if (Options.EcmaVersion >= 6 && node != null)
            {
                if (node is IdentifierNode identifier)
                {
                    if (InAsync && identifier.Name == "await")
                    {
                        Raise(node.Start, "Can not use 'await' as identifier inside an async function");
                    }
                }
                else if (node is ObjectPatternNode || node is ArrayPatternNode || node is RestElementNode)
                {
                }
                else if (node is ObjectExpressionNode objectExpression)
                {
                    node = new ObjectPatternNode(this, node.Start, node.End, node.Location.Start, node.Location.End, objectExpression.Properties);
                    if (refDestructuringErrors != null)
                    {
                        CheckPatternErrors(refDestructuringErrors, true);
                    }

                    for (var i = 0; i < objectExpression.Properties.Count; i++)
                    {
                        var prop = objectExpression.Properties[i];
                        ToAssignable(ref prop, isBinding);
                        // Early error:
                        //   AssignmentRestProperty[Yield, Await] :
                        //     `...` DestructuringAssignmentTarget[Yield, Await]
                        //
                        //   It is a Syntax Error if |DestructuringAssignmentTarget| is an |ArrayLiteral| or an |ObjectLiteral|.
                        if (prop is RestElementNode restElement &&
                            (restElement.Argument is ArrayPatternNode || restElement.Argument is ObjectPatternNode))
                        {
                            Raise(restElement.Argument.Start, "Unexpected token");
                        }

                        objectExpression.Properties[i] = prop;
                    }
                }
                else if (node is PropertyNode property)
                {
                    // AssignmentProperty has type === "Property"
                    if (property.Kind != PropertyKind.Init)
                    {
                        Raise(property.Key.Start, "Object pattern can't contain getter or setter");
                    }

                    ToAssignable(ref property.RefValue, isBinding);
                }
                else if (node is ArrayExpressionNode arrayExpression)
                {
                    node = new ArrayPatternNode(this, node.Start, node.End, node.Location.Start, node.Location.End, arrayExpression.Elements);
                    if (refDestructuringErrors != null)
                    {
                        CheckPatternErrors(refDestructuringErrors, true);
                    }

                    ToAssignableList(arrayExpression.Elements, isBinding);
                }
                else if (node is SpreadElementNode spread)
                {
                    var argument = spread.Argument;
                    ToAssignable(ref argument, isBinding);
                    node = new RestElementNode(this, node.Start, node.End, node.Location.Start, node.Location.End, argument);
                    if (argument is AssignmentPatternNode)
                    {
                        Raise(argument.Start, "Rest elements cannot have a default value");
                    }
                }
                else if (node is AssignmentExpressionNode assignmentExpression)
                {
                    if (assignmentExpression.Operator != Operator.Assignment)
                    {
                        Raise(assignmentExpression.Left.End, "Only '=' operator can be used for specifying default value.");
                    }

                    var left = assignmentExpression.Left;
                    ToAssignable(ref left, isBinding);
                    node = new AssignmentPatternNode(this, assignmentExpression.Start, assignmentExpression.End, assignmentExpression.Location.Start, assignmentExpression.Location.End, left, assignmentExpression.Right);
                    // falls through to AssignmentPattern
                }
                else if (node is AssignmentPatternNode)
                {
                }
                else if (node is ParenthesisedExpressionNode parenthesised)
                {
                    ToAssignable(ref parenthesised.ExpressionRef, isBinding);
                }
                else if (node is MemberExpressionNode && !isBinding)
                {
                }
                else
                {
                    Raise(node.Start, "Assigning to rvalue");
                }
            }
            else if (refDestructuringErrors != null)
            {
                CheckPatternErrors(refDestructuringErrors, true);
            }

            return node;
        }

        // Convert list of expression atoms to binding list.
        [NotNull]
        private IList<ExpressionNode> ToAssignableList([NotNull] IList<ExpressionNode> exprList, bool isBinding)
        {
            var end = exprList.Count;
            for (var i = 0; i < end; i++)
            {
                var elt = exprList[i];
                if (elt != null)
                {
                    ToAssignable(ref elt, isBinding);
                }

                exprList[i] = elt;
            }

            if (end != 0)
            {
                var last = exprList[end - 1];
                if (Options.EcmaVersion == 6 && isBinding && last != null && last is RestElementNode restElement && !(restElement.Argument is IdentifierNode))
                {
                    Unexpected(restElement.Argument.Start);
                }
            }

            return exprList;
        }

        // Parses spread element.
        [NotNull]
        private SpreadElementNode ParseSpread(DestructuringErrors refDestructuringErrors)
        {
            var start = Start;
            var startLoc = StartLocation;
            Next();
            var argument = ParseMaybeAssign(false, refDestructuringErrors);
            var node = new SpreadElementNode(this, start, startLoc, argument);
            return FinishNode(node);
        }

        [NotNull]
        private RestElementNode ParseRestBinding()
        {
            var start = Start;
            var startLoc = StartLocation;
            Next();

            // RestElement inside of a function parameter must be an identifier
            if (Options.EcmaVersion == 6 && Type != TokenType.Name)
            {
                Unexpected();
            }

            var argument = ParseBindingAtom();

            return FinishNode(new RestElementNode(this, start, startLoc, argument));
        }

        // Parses lvalue (assignable) atom.
        [NotNull]
        private ExpressionNode ParseBindingAtom()
        {
            if (Options.EcmaVersion >= 6)
            {
                if (Type == TokenType.BracketLeft)
                {
                    var start = Start;
                    var startLoc = StartLocation;
                    Next();
                    var elements = ParseBindingList(TokenType.BracketRight, true, true);
                    return FinishNode(new ArrayPatternNode(this, start, startLoc, elements));
                }

                if (Type == TokenType.BraceLeft)
                {
                    return ParseObject(true);
                }
            }

            return ParseIdentifier();
        }

        [NotNull]
        [ItemCanBeNull]
        private IList<ExpressionNode> ParseBindingList(TokenType close, bool allowEmpty, bool allowTrailingComma)
        {
            var elements = new List<ExpressionNode>();
            var first = true;
            while (!Eat(close))
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    Expect(TokenType.Comma);
                }

                if (allowEmpty && Type == TokenType.Comma)
                {
                    elements.Add(null);
                }
                else if (allowTrailingComma && AfterTrailingComma(close))
                {
                    break;
                }
                else if (Type == TokenType.Ellipsis)
                {
                    var rest = ParseRestBinding();
                    ParseBindingListItem(rest);
                    elements.Add(rest);
                    if (Type == TokenType.Comma)
                    {
                        Raise(Start, "Comma is not permitted after the rest element");
                    }

                    Expect(close);
                    break;
                }
                else
                {
                    var element = ParseMaybeDefault(Start, StartLocation);
                    ParseBindingListItem(element);
                    elements.Add(element);
                }
            }

            return elements;
        }

        private static ExpressionNode ParseBindingListItem(ExpressionNode param)
        {
            return param;
        }

        // Parses assignment pattern around given atom if possible.
        [NotNull]
        private ExpressionNode ParseMaybeDefault(int startPos, Position startLoc, ExpressionNode left = null)
        {
            left = left ?? ParseBindingAtom();
            if (Options.EcmaVersion < 6 || !Eat(TokenType.Equal))
            {
                return left;
            }

            var right = ParseMaybeAssign();
            return FinishNode(new AssignmentPatternNode(this, startPos, startLoc, left, right));
        }

        // Verify that a node is an lval — something that can be assigned to.
        // bindingType can be either:
        // 'var' indicating that the lval creates a 'var' binding
        // 'let' indicating that the lval creates a lexical ('let' or 'const') binding
        // 'none' indicating that the binding should be checked for illegal identifiers, but not for duplicate references
        private void CheckLeftValue([NotNull] BaseNode expr, BindType bindingType = BindType.None, [CanBeNull] ISet<string> checkClashes = null)
        {
            if (expr is IdentifierNode identifier)
            {
                if (strict && reservedWordsStrictBind.IsMatch(identifier.Name))
                {
                    RaiseRecoverable(expr.Start, (bindingType != BindType.None ? "Binding " : "Assigning to ") + identifier.Name + " in strict mode");
                }

                if (checkClashes != null)
                {
                    if (checkClashes.Contains(identifier.Name))
                    {
                        RaiseRecoverable(expr.Start, "Argument name clash");
                    }

                    checkClashes.Add(identifier.Name);
                }

                if (bindingType != BindType.None && bindingType != BindType.Outside)
                {
                    DeclareName(identifier.Name, bindingType, expr.Start);
                }
            }
            else if (expr is MemberExpressionNode)
            {
                if (bindingType != BindType.None)
                {
                    RaiseRecoverable(expr.Start, "Binding member expression");
                }
            }
            else if (expr is ObjectPatternNode objectPattern)
            {
                foreach (var property in objectPattern.Properties)
                {
                    CheckLeftValue(property, bindingType, checkClashes);
                }
            }
            else if (expr is PropertyNode property)
            {
                // AssignmentProperty has type === "Property"
                CheckLeftValue(property.Value, bindingType, checkClashes);
            }
            else if (expr is ArrayPatternNode arrayPattern)
            {
                foreach (var element in arrayPattern.Elements)
                {
                    if (element != null)
                    {
                        CheckLeftValue(element, bindingType, checkClashes);
                    }
                }
            }
            else if (expr is AssignmentPatternNode assignmentPattern)
            {
                CheckLeftValue(assignmentPattern.Left, bindingType, checkClashes);
            }
            else if (expr is RestElementNode restElement)
            {
                CheckLeftValue(restElement.Argument, bindingType, checkClashes);
            }
            else if (expr is ParenthesisedExpressionNode parenthesised)
            {
                CheckLeftValue(parenthesised.Expression, bindingType, checkClashes);
            }
            else
            {
                Raise(expr.Start, (bindingType != BindType.None ? "Binding" : "Assigning to") + " rvalue");
            }
        }
    }
}