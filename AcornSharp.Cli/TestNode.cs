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

            if (!location?.Equals(node.location) == true)
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
                    if (body != null && !TestEquals((IList<TestNode>)body, realNode.body))
                        return false;
                    return true;

                case ExpressionStatementNode realNode:
                    if (!((TestNode)expression)?.TestEquals(realNode.expression) == true)
                        return false;
                    if (directive != null && !string.Equals(directive, realNode.directive, StringComparison.Ordinal))
                        return false;
                    return true;

                case LiteralNode realNode:
                    if (raw != null && !string.Equals(raw, realNode.raw, StringComparison.Ordinal))
                        return false;

                    if (value != null)
                    {
                        if (value is int intValue)
                        {
                            // ReSharper disable once CompareOfFloatsByEqualityOperator
                            if (!realNode.value.IsDouble || intValue != realNode.value.AsDouble)
                                return false;
                        }
                        else if (value is double doubleValue)
                        {
                            // ReSharper disable once CompareOfFloatsByEqualityOperator
                            if (!realNode.value.IsDouble || doubleValue != realNode.value.AsDouble)
                                return false;
                        }
                        else if (value is bool boolValue)
                        {
                            if (!realNode.value.IsBoolean || boolValue != realNode.value.AsBoolean)
                                return false;
                        }
                        else if (value is string stringValue)
                        {
                            if (!realNode.value.IsString || !string.Equals(stringValue, realNode.value.AsString, StringComparison.Ordinal))
                                return false;
                        }
                        else throw new NotImplementedException();
                    }

                    if (regex != null)
                    {
                        if (realNode.regex == null ||
                            !string.Equals(regex.pattern, realNode.regex.pattern, StringComparison.Ordinal) ||
                            !string.Equals(regex.flags, realNode.regex.flags, StringComparison.Ordinal))
                            return false;
                    }

                    return true;

                case BinaryExpressionNode realNode:
                    if (!left?.TestEquals(realNode.left) == true)
                        return false;
                    if (!right?.TestEquals(realNode.right) == true)
                        return false;
                    if (@operator.HasValue && @operator != realNode.@operator)
                        return false;
                    return true;

                case LogicalExpressionNode realNode:
                    if (!left?.TestEquals(realNode.left) == true)
                        return false;
                    if (!right?.TestEquals(realNode.right) == true)
                        return false;
                    if (@operator.HasValue && @operator != realNode.@operator)
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
                    if (!((TestNode)expression)?.TestEquals(realNode.expression) == true)
                        return false;
                    return true;

                case IdentifierNode realNode:
                    if (name != null && !string.Equals(name, realNode.name, StringComparison.Ordinal))
                        return false;
                    return true;

                case ArrayExpressionNode realNode:
                    if (elements != null && !TestEquals(elements, realNode.Elements))
                        return false;
                    return true;

                case ObjectExpressionNode realNode:
                    if (properties != null && !TestEquals(properties, realNode.properties))
                        return false;
                    return true;

                case PropertyNode realNode:
                    if (kind != null && (PropertyKind)kind != realNode.kind)
                        return false;
                    if (computed.HasValue && computed != realNode.computed)
                        return false;
                    if (shorthand.HasValue && shorthand != realNode.shorthand)
                        return false;
                    if (method.HasValue && method != realNode.method)
                        return false;
                    if (!key?.TestEquals(realNode.key) == true)
                        return false;
                    if (!((TestNode)value)?.TestEquals(realNode.value) == true)
                        return false;
                    return true;

                case FunctionExpressionNode realNode:
                {
                    if (expression is bool boolExpression && boolExpression != realNode.expression)
                        return false;
                    if (async.HasValue && async != realNode.async)
                        return false;
                    if (generator.HasValue && generator != realNode.generator)
                        return false;
                    if (!id?.TestEquals(realNode.id) == true)
                        return false;
                    if (parameters != null && !TestEquals(parameters, realNode.parameters))
                        return false;
                    if (!((TestNode)body)?.TestEquals(realNode.body) == true)
                        return false;
                    return true;
                }

                case FunctionDeclarationNode realNode:
                {
                    if (expression is bool boolExpression && boolExpression != realNode.expression)
                        return false;
                    if (async.HasValue && async != realNode.async)
                        return false;
                    if (generator.HasValue && generator != realNode.generator)
                        return false;
                    if (!id?.TestEquals(realNode.id) == true)
                        return false;
                    if (parameters != null && !TestEquals(parameters, realNode.parameters))
                        return false;
                    if (!((TestNode)body)?.TestEquals(realNode.body) == true)
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
                    if (body != null && !TestEquals((IList<TestNode>)body, realNode.body))
                        return false;
                    return true;

                case ReturnStatementNode realNode:
                    if (!argument?.TestEquals(realNode.argument) == true)
                        return false;
                    return true;

                case IfStatementNode realNode:
                    if (!test?.TestEquals(realNode.test) == true)
                        return false;
                    if (!((TestNode)consequent)?.TestEquals(realNode.consequent) == true)
                        return false;
                    if (!alternate?.TestEquals(realNode.alternate) == true)
                        return false;
                    return true;

                case CallExpressionNode realNode:
                    if (!callee?.TestEquals(realNode.callee) == true)
                        return false;
                    if (arguments != null && !TestEquals(arguments, realNode.arguments))
                        return false;
                    return true;

                case SwitchStatementNode realNode:
                    if (!discriminant?.TestEquals(realNode.discriminant) == true)
                        return false;
                    if (cases != null && !TestEquals(cases, realNode.cases))
                        return false;
                    return true;

                case SwitchCaseNode realNode:
                    if (!test?.TestEquals(realNode.test) == true)
                        return false;
                    if (consequent != null && !TestEquals((IList<TestNode>)consequent, realNode.consequent))
                        return false;
                    return true;

                case VariableDeclarationNode realNode:
                    if (kind != null && (VariableKind)kind != realNode.kind)
                        return false;
                    if (declarations != null && !TestEquals(declarations, realNode.declarations))
                        return false;
                    return true;

                case VariableDeclaratorNode realNode:
                    if (kind != null && (VariableKind)kind != realNode.kind)
                        return false;
                    if (!id?.TestEquals(realNode.id) == true)
                        return false;
                    if (!init?.TestEquals(realNode.init) == true)
                        return false;
                    return true;

                case NewExpressionNode realNode:
                    if (!callee?.TestEquals(realNode.callee) == true)
                        return false;
                    if (arguments != null && !TestEquals(arguments, realNode.arguments))
                        return false;
                    return true;

                case MemberExpressionNode realNode:
                    if (!@object?.TestEquals(realNode.@object) == true)
                        return false;
                    if (!property?.TestEquals(realNode.property) == true)
                        return false;
                    if (computed.HasValue && computed != realNode.computed)
                        return false;
                    return true;

                case SequenceExpressionNode realNode:
                    if (expressions != null && !TestEquals(expressions, realNode.expressions))
                        return false;
                    return true;

                case UpdateExpressionNode realNode:
                    if (@operator.HasValue && @operator != realNode.@operator)
                        return false;
                    if (!argument?.TestEquals(realNode.argument) == true)
                        return false;
                    return true;

                case UnaryExpressionNode realNode:
                    if (@operator.HasValue && @operator != realNode.@operator)
                        return false;
                    if (!argument?.TestEquals(realNode.argument) == true)
                        return false;
                    return true;

                case ConditionalExpressionNode realNode:
                    if (!test?.TestEquals(realNode.test) == true)
                        return false;
                    if (!((TestNode)consequent)?.TestEquals(realNode.consequent) == true)
                        return false;
                    if (!alternate?.TestEquals(realNode.alternate) == true)
                        return false;
                    return true;

                case DoWhileStatementNode realNode:
                    if (!test?.TestEquals(realNode.test) == true)
                        return false;
                    if (!((TestNode)body)?.TestEquals(realNode.body) == true)
                        return false;
                    return true;

                case WhileStatementNode realNode:
                    if (!test?.TestEquals(realNode.test) == true)
                        return false;
                    if (!((TestNode)body)?.TestEquals(realNode.body) == true)
                        return false;
                    return true;

                case ForStatementNode realNode:
                    if (!init?.TestEquals(realNode.init) == true)
                        return false;
                    if (!test?.TestEquals(realNode.test) == true)
                        return false;
                    if (!update?.TestEquals(realNode.update) == true)
                        return false;
                    if (!((TestNode)body)?.TestEquals(realNode.body) == true)
                        return false;
                    return true;

                case ForInStatementNode realNode:
                    if (!left?.TestEquals(realNode.left) == true)
                        return false;
                    if (!right?.TestEquals(realNode.right) == true)
                        return false;
                    if (!((TestNode)body)?.TestEquals(realNode.body) == true)
                        return false;
                    return true;

                case ForOfStatementNode realNode:
                    if (!left?.TestEquals(realNode.left) == true)
                        return false;
                    if (!right?.TestEquals(realNode.right) == true)
                        return false;
                    if (!((TestNode)body)?.TestEquals(realNode.body) == true)
                        return false;
                    return true;

                case ContinueStatementNode realNode:
                    if (!label?.TestEquals(realNode.label) == true)
                        return false;
                    return true;

                case BreakStatementNode realNode:
                    if (!label?.TestEquals(realNode.label) == true)
                        return false;
                    return true;

                case LabelledStatementNode realNode:
                    if (!label?.TestEquals(realNode.label) == true)
                        return false;
                    if (!((TestNode)body)?.TestEquals(realNode.body) == true)
                        return false;
                    return true;

                case WithStatementNode realNode:
                    if (!@object?.TestEquals(realNode.@object) == true)
                        return false;
                    if (!((TestNode)body)?.TestEquals(realNode.body) == true)
                        return false;
                    return true;

                case ThrowStatementNode realNode:
                    if (!argument?.TestEquals(realNode.argument) == true)
                        return false;
                    return true;

                case TryStatementNode realNode:
                    if (!block?.TestEquals(realNode.block) == true)
                        return false;
                    if (!handler?.TestEquals(realNode.handler) == true)
                        return false;
                    if (!finaliser?.TestEquals(realNode.finaliser) == true)
                        return false;
                    return true;

                case CatchClauseNode realNode:
                    if (!param?.TestEquals(realNode.param) == true)
                        return false;
                    if (!((TestNode)body)?.TestEquals(realNode.body) == true)
                        return false;
                    return true;

                case RestElementNode realNode:
                    if (!argument?.TestEquals(realNode.argument) == true)
                        return false;
                    return true;

                case TemplateLiteralNode realNode:
                    if (expressions != null && !TestEquals(expressions, realNode.expressions))
                        return false;
                    if (quasis != null && !TestEquals(quasis, realNode.quasis))
                        return false;
                    return true;

                case TemplateElementNode realNode:
                    if (!((TemplateNode)value)?.Equals(realNode.value) == true)
                        return false;
                    if (tail.HasValue && tail != realNode.tail)
                        return false;
                    return true;

                case TaggedTemplateExpressionNode realNode:
                    if (!tag?.TestEquals(realNode.tag) == true)
                        return false;
                    if (!quasi?.TestEquals(realNode.quasi) == true)
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
                    if (properties != null && !TestEquals(properties, realNode.properties))
                        return false;
                    return true;

                case ExportNamedDeclarationNode realNode:
                    if (!((TestNode)source)?.TestEquals(realNode.source) == true)
                        return false;
                    if (!declaration?.TestEquals(realNode.declaration) == true)
                        return false;
                    if (specifiers != null && !TestEquals(specifiers, realNode.specifiers))
                        return false;
                    return true;

                case ClassDeclarationNode realNode:
                    if (!id?.TestEquals(realNode.id) == true)
                        return false;
                    if (!superClass?.TestEquals(realNode.superClass) == true)
                        return false;
                    if (!((TestNode)body)?.TestEquals(realNode.body) == true)
                        return false;
                    return true;

                case ClassExpressionNode realNode:
                    if (!id?.TestEquals(realNode.id) == true)
                        return false;
                    if (!superClass?.TestEquals(realNode.superClass) == true)
                        return false;
                    if (!((TestNode)body)?.TestEquals(realNode.body) == true)
                        return false;
                    return true;

                case ClassBodyNode realNode:
                    if (body != null && !TestEquals((IList<TestNode>)body, realNode.body))
                        return false;
                    return true;

                case ExportDefaultDeclarationNode realNode:
                    if (!declaration?.TestEquals(realNode.declaration) == true)
                        return false;
                    return true;

                case ExportAllDeclarationNode realNode:
                    if (!((TestNode)source)?.TestEquals(realNode.source) == true)
                        return false;
                    return true;

                case ExportSpecifierNode realNode:
                    if (!local?.TestEquals(realNode.local) == true)
                        return false;
                    if (!exported?.TestEquals(realNode.exported) == true)
                        return false;
                    return true;

                case ImportDeclarationNode realNode:
                    if (!((TestNode)source)?.TestEquals(realNode.source) == true)
                        return false;
                    if (specifiers != null && !TestEquals(specifiers, realNode.specifiers))
                        return false;
                    return true;

                case ImportDefaultSpecifierNode realNode:
                    if (!local?.TestEquals(realNode.local) == true)
                        return false;
                    return true;

                case ImportSpecifierNode realNode:
                    if (!local?.TestEquals(realNode.local) == true)
                        return false;
                    if (!imported?.TestEquals(realNode.imported) == true)
                        return false;
                    return true;

                case ImportNamespaceSpecifierNode realNode:
                    if (!local?.TestEquals(realNode.local) == true)
                        return false;
                    return true;

                case YieldExpressionNode realNode:
                    if (@delegate.HasValue && @delegate != realNode.@delegate)
                        return false;
                    if (!argument?.TestEquals(realNode.argument) == true)
                        return false;
                    return true;

                case MethodDefinitionNode realNode:
                    if (kind != null && (PropertyKind)kind != realNode.kind)
                        return false;
                    if (computed.HasValue && computed != realNode.computed)
                        return false;
                    if (@static.HasValue && @static != realNode.@static)
                        return false;
                    if (!key?.TestEquals(realNode.key) == true)
                        return false;
                    if (!((TestNode)value)?.TestEquals(realNode.value) == true)
                        return false;
                    return true;

                case SpreadElementNode realNode:
                    if (!argument?.TestEquals(realNode.argument) == true)
                        return false;
                    return true;

                case MetaPropertyNode realNode:
                    if (!meta?.TestEquals(realNode.meta) == true)
                        return false;
                    if (!property?.TestEquals(realNode.property) == true)
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
