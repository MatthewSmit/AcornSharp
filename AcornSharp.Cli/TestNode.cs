using System;
using System.Collections.Generic;
using AcornSharp.Node;
using JetBrains.Annotations;

namespace AcornSharp.Cli
{
    internal sealed class TestNode
    {
        public Type type;
        public SourceLocation? location;
        public object body;
        public object expression;
        public object value;
        public RegexNode regex;
        public TestNode left;
        public TestNode right;
        public Operator? @operator;
        public string name;
        public string raw;
        public IList<TestNode> elements;
        public IList<TestNode> parameters;
        public IList<TestNode> properties;
        public IList<TestNode> arguments;
        public IList<TestNode> declarations;
        public IList<TestNode> cases;
        public IList<TestNode> expressions;
        public IList<TestNode> specifiers;
        public IList<TestNode> quasis;
        public TestNode key;
        public TestNode callee;
        public TestNode discriminant;
        public TestNode argument;
        public TestNode id;
        public TestNode init;
        public TestNode test;
        public TestNode label;
        public TestNode @object;
        public TestNode update;
        public TestNode declaration;
        public TestNode local;
        public TestNode exported;
        public TestNode imported;
        public object consequent;
        public TestNode alternate;
        public object kind;
        public TestNode property;
        public bool? computed;
        public TestNode block;
        public TestNode handler;
        public TestNode finaliser;
        public TestNode param;
        public TestNode tag;
        public TestNode quasi;
        public TestNode superClass;
        public TestNode meta;
        public object source;
        public bool? generator;
        public bool? async;
        public bool? method;
        public bool? @static;
        public bool? shorthand;
        public bool? tail;
        public bool? @delegate;
        public string directive;

        public bool TestEquals([NotNull] BaseNode node)
        {
            if (type != node.GetType())
                return false;

            if (!location?.Equals(node.Location) == true)
                return false;

            switch (node)
            {
                case ThisExpressionNode _:
                case EmptyStatementNode _:
                case DebuggerStatementNode _:
                case SuperNode _:
                    return true;

                case ProgramNode realNode:
                    if (source != null && (SourceType)source != realNode.SourceType)
                        return false;
                    if (body != null && !TestEquals((IList<TestNode>)body, realNode.Body))
                        return false;
                    return true;

                case ExpressionStatementNode realNode:
                    if (!((TestNode)expression)?.TestEquals(realNode.Expression) == true)
                        return false;
                    if (directive != null && !string.Equals(directive, realNode.Directive, StringComparison.Ordinal))
                        return false;
                    return true;

                case LiteralNode realNode:
                    if (raw != null && !string.Equals(raw, realNode.Raw, StringComparison.Ordinal))
                        return false;

                    if (value != null)
                    {
                        if (value is int intValue)
                        {
                            // ReSharper disable once CompareOfFloatsByEqualityOperator
                            if (!realNode.Value.IsDouble || intValue != realNode.Value.AsDouble)
                                return false;
                        }
                        else if (value is double doubleValue)
                        {
                            // ReSharper disable once CompareOfFloatsByEqualityOperator
                            if (!realNode.Value.IsDouble || doubleValue != realNode.Value.AsDouble)
                                return false;
                        }
                        else if (value is bool boolValue)
                        {
                            if (!realNode.Value.IsBoolean || boolValue != realNode.Value.AsBoolean)
                                return false;
                        }
                        else if (value is string stringValue)
                        {
                            if (!realNode.Value.IsString || !string.Equals(stringValue, realNode.Value.AsString, StringComparison.Ordinal))
                                return false;
                        }
                        else throw new NotImplementedException();
                    }

                    if (regex != null)
                    {
                        if (!realNode.Value.IsRegex ||
                            !string.Equals(regex.Pattern, realNode.Value.AsRegex.Pattern, StringComparison.Ordinal) ||
                            !string.Equals(regex.Flags, realNode.Value.AsRegex.Flags, StringComparison.Ordinal))
                            return false;
                    }

                    return true;

                case BinaryExpressionNode realNode:
                    if (!left?.TestEquals(realNode.Left) == true)
                        return false;
                    if (!right?.TestEquals(realNode.Right) == true)
                        return false;
                    if (@operator.HasValue && @operator != realNode.Operator)
                        return false;
                    return true;

                case LogicalExpressionNode realNode:
                    if (!left?.TestEquals(realNode.Left) == true)
                        return false;
                    if (!right?.TestEquals(realNode.Right) == true)
                        return false;
                    if (@operator.HasValue && @operator != realNode.Operator)
                        return false;
                    return true;

                case AssignmentExpressionNode realNode:
                    if (!left?.TestEquals(realNode.Left) == true)
                        return false;
                    if (!right?.TestEquals(realNode.Right) == true)
                        return false;
                    if (@operator.HasValue && @operator != realNode.Operator)
                        return false;
                    return true;

                case ParenthesisedExpressionNode realNode:
                    if (!((TestNode)expression)?.TestEquals(realNode.Expression) == true)
                        return false;
                    return true;

                case IdentifierNode realNode:
                    if (name != null && !string.Equals(name, realNode.Name, StringComparison.Ordinal))
                        return false;
                    return true;

                case ArrayExpressionNode realNode:
                    if (elements != null && !TestEquals(elements, realNode.Elements))
                        return false;
                    return true;

                case ObjectExpressionNode realNode:
                    if (properties != null && !TestEquals(properties, realNode.Properties))
                        return false;
                    return true;

                case PropertyNode realNode:
                    if (kind != null && (PropertyKind)kind != realNode.Kind)
                        return false;
                    if (computed.HasValue && computed != realNode.Computed)
                        return false;
                    if (shorthand.HasValue && shorthand != realNode.Shorthand)
                        return false;
                    if (method.HasValue && method != realNode.Method)
                        return false;
                    if (!key?.TestEquals(realNode.Key) == true)
                        return false;
                    if (!((TestNode)value)?.TestEquals(realNode.Value) == true)
                        return false;
                    return true;

                case FunctionExpressionNode realNode:
                {
                    if (expression is bool boolExpression && boolExpression != realNode.Expression)
                        return false;
                    if (async.HasValue && async != realNode.Async)
                        return false;
                    if (generator.HasValue && generator != realNode.Generator)
                        return false;
                    if (!id?.TestEquals(realNode.Id) == true)
                        return false;
                    if (parameters != null && !TestEquals(parameters, realNode.Parameters))
                        return false;
                    if (!((TestNode)body)?.TestEquals(realNode.Body) == true)
                        return false;
                    return true;
                }

                case FunctionDeclarationNode realNode:
                {
                    if (expression is bool boolExpression && boolExpression != realNode.Expression)
                        return false;
                    if (async.HasValue && async != realNode.Async)
                        return false;
                    if (generator.HasValue && generator != realNode.Generator)
                        return false;
                    if (!id?.TestEquals(realNode.Id) == true)
                        return false;
                    if (parameters != null && !TestEquals(parameters, realNode.Parameters))
                        return false;
                    if (!((TestNode)body)?.TestEquals(realNode.Body) == true)
                        return false;
                    return true;
                }

                case ArrowFunctionExpressionNode realNode:
                {
                    if (expression is bool boolExpression && boolExpression != realNode.Expression)
                        return false;
                    if (async.HasValue && async != realNode.Async)
                        return false;
                    if (parameters != null && !TestEquals(parameters, realNode.Parameters))
                        return false;
                    if (!((TestNode)body)?.TestEquals(realNode.Body) == true)
                        return false;
                    return true;
                }

                case BlockStatementNode realNode:
                    if (body != null && !TestEquals((IList<TestNode>)body, realNode.Body))
                        return false;
                    return true;

                case ReturnStatementNode realNode:
                    if (!argument?.TestEquals(realNode.Argument) == true)
                        return false;
                    return true;

                case IfStatementNode realNode:
                    if (!test?.TestEquals(realNode.Test) == true)
                        return false;
                    if (!((TestNode)consequent)?.TestEquals(realNode.Consequent) == true)
                        return false;
                    if (!alternate?.TestEquals(realNode.Alternate) == true)
                        return false;
                    return true;

                case CallExpressionNode realNode:
                    if (!callee?.TestEquals(realNode.Callee) == true)
                        return false;
                    if (arguments != null && !TestEquals(arguments, realNode.Arguments))
                        return false;
                    return true;

                case SwitchStatementNode realNode:
                    if (!discriminant?.TestEquals(realNode.Discriminant) == true)
                        return false;
                    if (cases != null && !TestEquals(cases, realNode.Cases))
                        return false;
                    return true;

                case SwitchCaseNode realNode:
                    if (!test?.TestEquals(realNode.Test) == true)
                        return false;
                    if (consequent != null && !TestEquals((IList<TestNode>)consequent, realNode.Consequent))
                        return false;
                    return true;

                case VariableDeclarationNode realNode:
                    if (kind != null && (VariableKind)kind != realNode.Kind)
                        return false;
                    if (declarations != null && !TestEquals(declarations, realNode.Declarations))
                        return false;
                    return true;

                case VariableDeclaratorNode realNode:
                    if (kind != null && (VariableKind)kind != realNode.Kind)
                        return false;
                    if (!id?.TestEquals(realNode.Id) == true)
                        return false;
                    if (!init?.TestEquals(realNode.Init) == true)
                        return false;
                    return true;

                case NewExpressionNode realNode:
                    if (!callee?.TestEquals(realNode.Callee) == true)
                        return false;
                    if (arguments != null && !TestEquals(arguments, realNode.Arguments))
                        return false;
                    return true;

                case MemberExpressionNode realNode:
                    if (!@object?.TestEquals(realNode.Object) == true)
                        return false;
                    if (!property?.TestEquals(realNode.Property) == true)
                        return false;
                    if (computed.HasValue && computed != realNode.Computed)
                        return false;
                    return true;

                case SequenceExpressionNode realNode:
                    if (expressions != null && !TestEquals(expressions, realNode.Expressions))
                        return false;
                    return true;

                case UpdateExpressionNode realNode:
                    if (@operator.HasValue && @operator != realNode.Operator)
                        return false;
                    if (!argument?.TestEquals(realNode.Argument) == true)
                        return false;
                    return true;

                case UnaryExpressionNode realNode:
                    if (@operator.HasValue && @operator != realNode.Operator)
                        return false;
                    if (!argument?.TestEquals(realNode.Argument) == true)
                        return false;
                    return true;

                case ConditionalExpressionNode realNode:
                    if (!test?.TestEquals(realNode.Test) == true)
                        return false;
                    if (!((TestNode)consequent)?.TestEquals(realNode.Consequent) == true)
                        return false;
                    if (!alternate?.TestEquals(realNode.Alternate) == true)
                        return false;
                    return true;

                case DoWhileStatementNode realNode:
                    if (!test?.TestEquals(realNode.Test) == true)
                        return false;
                    if (!((TestNode)body)?.TestEquals(realNode.Body) == true)
                        return false;
                    return true;

                case WhileStatementNode realNode:
                    if (!test?.TestEquals(realNode.Test) == true)
                        return false;
                    if (!((TestNode)body)?.TestEquals(realNode.Body) == true)
                        return false;
                    return true;

                case ForStatementNode realNode:
                    if (!init?.TestEquals(realNode.Init) == true)
                        return false;
                    if (!test?.TestEquals(realNode.Test) == true)
                        return false;
                    if (!update?.TestEquals(realNode.Update) == true)
                        return false;
                    if (!((TestNode)body)?.TestEquals(realNode.Body) == true)
                        return false;
                    return true;

                case ForInStatementNode realNode:
                    if (!left?.TestEquals(realNode.Left) == true)
                        return false;
                    if (!right?.TestEquals(realNode.Right) == true)
                        return false;
                    if (!((TestNode)body)?.TestEquals(realNode.Body) == true)
                        return false;
                    return true;

                case ForOfStatementNode realNode:
                    if (!left?.TestEquals(realNode.Left) == true)
                        return false;
                    if (!right?.TestEquals(realNode.Right) == true)
                        return false;
                    if (!((TestNode)body)?.TestEquals(realNode.Body) == true)
                        return false;
                    return true;

                case ContinueStatementNode realNode:
                    if (!label?.TestEquals(realNode.Label) == true)
                        return false;
                    return true;

                case BreakStatementNode realNode:
                    if (!label?.TestEquals(realNode.Label) == true)
                        return false;
                    return true;

                case LabelledStatementNode realNode:
                    if (!label?.TestEquals(realNode.Label) == true)
                        return false;
                    if (!((TestNode)body)?.TestEquals(realNode.Body) == true)
                        return false;
                    return true;

                case WithStatementNode realNode:
                    if (!@object?.TestEquals(realNode.Object) == true)
                        return false;
                    if (!((TestNode)body)?.TestEquals(realNode.Body) == true)
                        return false;
                    return true;

                case ThrowStatementNode realNode:
                    if (!argument?.TestEquals(realNode.Argument) == true)
                        return false;
                    return true;

                case TryStatementNode realNode:
                    if (!block?.TestEquals(realNode.Block) == true)
                        return false;
                    if (!handler?.TestEquals(realNode.Handler) == true)
                        return false;
                    if (!finaliser?.TestEquals(realNode.Finaliser) == true)
                        return false;
                    return true;

                case CatchClauseNode realNode:
                    if (!param?.TestEquals(realNode.Param) == true)
                        return false;
                    if (!((TestNode)body)?.TestEquals(realNode.Body) == true)
                        return false;
                    return true;

                case RestElementNode realNode:
                    if (!argument?.TestEquals(realNode.Argument) == true)
                        return false;
                    return true;

                case TemplateLiteralNode realNode:
                    if (expressions != null && !TestEquals(expressions, realNode.Expressions))
                        return false;
                    if (quasis != null && !TestEquals(quasis, realNode.Quasis))
                        return false;
                    return true;

                case TemplateElementNode realNode:
                    if (!((TemplateNode)value)?.Equals(realNode.Value) == true)
                        return false;
                    if (tail.HasValue && tail != realNode.Tail)
                        return false;
                    return true;

                case TaggedTemplateExpressionNode realNode:
                    if (!tag?.TestEquals(realNode.Tag) == true)
                        return false;
                    if (!quasi?.TestEquals(realNode.Quasi) == true)
                        return false;
                    return true;

                case ArrayPatternNode realNode:
                    if (elements != null && !TestEquals(elements, realNode.Elements))
                        return false;
                    return true;

                case AssignmentPatternNode realNode:
                    if (!left?.TestEquals(realNode.Left) == true)
                        return false;
                    if (!right?.TestEquals(realNode.Right) == true)
                        return false;
                    return true;

                case ObjectPatternNode realNode:
                    if (properties != null && !TestEquals(properties, realNode.Properties))
                        return false;
                    return true;

                case ExportNamedDeclarationNode realNode:
                    if (!((TestNode)source)?.TestEquals(realNode.Source) == true)
                        return false;
                    if (!declaration?.TestEquals(realNode.Declaration) == true)
                        return false;
                    if (specifiers != null && !TestEquals(specifiers, realNode.Specifiers))
                        return false;
                    return true;

                case ClassDeclarationNode realNode:
                    if (!id?.TestEquals(realNode.Id) == true)
                        return false;
                    if (!superClass?.TestEquals(realNode.SuperClass) == true)
                        return false;
                    if (!((TestNode)body)?.TestEquals(realNode.Body) == true)
                        return false;
                    return true;

                case ClassExpressionNode realNode:
                    if (!id?.TestEquals(realNode.Id) == true)
                        return false;
                    if (!superClass?.TestEquals(realNode.SuperClass) == true)
                        return false;
                    if (!((TestNode)body)?.TestEquals(realNode.Body) == true)
                        return false;
                    return true;

                case ClassBodyNode realNode:
                    if (body != null && !TestEquals((IList<TestNode>)body, realNode.Body))
                        return false;
                    return true;

                case ExportDefaultDeclarationNode realNode:
                    if (!declaration?.TestEquals(realNode.Declaration) == true)
                        return false;
                    return true;

                case ExportAllDeclarationNode realNode:
                    if (!((TestNode)source)?.TestEquals(realNode.Source) == true)
                        return false;
                    return true;

                case ExportSpecifierNode realNode:
                    if (!local?.TestEquals(realNode.Local) == true)
                        return false;
                    if (!exported?.TestEquals(realNode.Exported) == true)
                        return false;
                    return true;

                case ImportDeclarationNode realNode:
                    if (!((TestNode)source)?.TestEquals(realNode.Source) == true)
                        return false;
                    if (specifiers != null && !TestEquals(specifiers, realNode.Specifiers))
                        return false;
                    return true;

                case ImportDefaultSpecifierNode realNode:
                    if (!local?.TestEquals(realNode.Local) == true)
                        return false;
                    return true;

                case ImportSpecifierNode realNode:
                    if (!local?.TestEquals(realNode.Local) == true)
                        return false;
                    if (!imported?.TestEquals(realNode.Imported) == true)
                        return false;
                    return true;

                case ImportNamespaceSpecifierNode realNode:
                    if (!local?.TestEquals(realNode.Local) == true)
                        return false;
                    return true;

                case YieldExpressionNode realNode:
                    if (@delegate.HasValue && @delegate != realNode.Delegate)
                        return false;
                    if (!argument?.TestEquals(realNode.Argument) == true)
                        return false;
                    return true;

                case MethodDefinitionNode realNode:
                    if (kind != null && (PropertyKind)kind != realNode.Kind)
                        return false;
                    if (computed.HasValue && computed != realNode.Computed)
                        return false;
                    if (@static.HasValue && @static != realNode.Static)
                        return false;
                    if (!key?.TestEquals(realNode.Key) == true)
                        return false;
                    if (!((TestNode)value)?.TestEquals(realNode.Value) == true)
                        return false;
                    return true;

                case SpreadElementNode realNode:
                    if (!argument?.TestEquals(realNode.Argument) == true)
                        return false;
                    return true;

                case MetaPropertyNode realNode:
                    if (!meta?.TestEquals(realNode.Meta) == true)
                        return false;
                    if (!property?.TestEquals(realNode.Property) == true)
                        return false;
                    return true;

                case AwaitExpressionNode realNode:
                    if (!argument?.TestEquals(realNode.Argument) == true)
                        return false;
                    return true;

                default:
                    throw new InvalidOperationException();
            }
        }

        private static bool TestEquals<T>([NotNull] IList<TestNode> lhs, [NotNull] IReadOnlyList<T> rhs)
            where T : BaseNode
        {
            if (lhs.Count != rhs.Count)
                return false;

            for (var i = 0; i < lhs.Count; i++)
            {
                var lhsNode = lhs[i];
                var rhsNode = rhs[i];

                if (ReferenceEquals(lhsNode, null))
                {
                    if (!ReferenceEquals(rhsNode, null))
                        return false;
                    continue;
                }

                if (!lhsNode.TestEquals(rhsNode))
                    return false;
            }

            return true;
        }
    }
}
