using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public class BaseNode : IEquatable<BaseNode>
    {
        public NodeType type;
        public IList<BaseNode> body;
        public BaseNode expression;
        public bool bexpression;
        public SourceLocation loc;
        public object value;
        public RegexNode regex;
        public BaseNode left;
        public string @operator;
        public BaseNode right;
        public string raw;
        public IList<BaseNode> elements;
        public IList<BaseNode> properties;
        public BaseNode id;
        public IList<BaseNode> @params;
        public BaseNode fbody;
        public BaseNode argument;
        public BaseNode test;
        public BaseNode consequent;
        public BaseNode callee;
        public IList<BaseNode> arguments;
        public BaseNode alternate;
        public BaseNode discriminant;
        public IList<BaseNode> cases;
        public IList<BaseNode> declarations;
        public IList<BaseNode> sconsequent;
        public BaseNode init;
        public string kind;
        public BaseNode @object;
        public BaseNode property;
        public bool computed;
        public IList<BaseNode> expressions;
        public bool prefix;
        public BaseNode update;
        public IdentifierNode label;
        public BaseNode block;
        public BaseNode handler;
        public BaseNode param;
        public BaseNode finalizer;
        public bool generator;
        public bool async;
        public bool @static;
        public BaseNode key;
        public string directive;
        public BaseNode tag;
        public BaseNode quasi;
        public bool @delegate;
        public BaseNode meta;
        public BaseNode superClass;
        public BaseNode source;
        public BaseNode declaration;
        public IList<BaseNode> specifiers;
        public bool tail;
        public bool method;
        public IdentifierNode local;
        public IdentifierNode exported;
        public List<BaseNode> quasis;
        public IdentifierNode imported;
        public bool shorthand;
        public string sourceType;

        public BaseNode()
        {
        }

        public BaseNode(SourceLocation location)
        {
            type = NodeType.Unknown;
            loc = location;
        }

        public BaseNode([NotNull] Parser parser, Position start)
        {
            type = NodeType.Unknown;
            loc = new SourceLocation(start, default, parser.sourceFile);
        }

        public BaseNode([NotNull] Parser parser, Position start, Position end)
        {
            type = NodeType.Unknown;
            loc = new SourceLocation(start, end, parser.sourceFile);
        }

        public virtual bool TestEquals([CanBeNull] BaseNode other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;

            if (type != NodeType.Unknown && type != other.type) return false;
            if (body != null && !TestEquals(body, other.body)) return false;
            if (!TestEquals(expression, other.expression)) return false;
            if (bexpression && bexpression != other.bexpression) return false;
            if (loc.Start.Line > 0 && !Equals(loc, other.loc)) return false;
            if (value != null && !TestEquals(value, other.value)) return false;
            if (regex != null && !Equals(regex, other.regex)) return false;
            if (left != null && !TestEquals(left, other.left)) return false;
            if (!string.Equals(@operator, other.@operator, StringComparison.Ordinal)) return false;
            if (right != null && !TestEquals(right, other.right)) return false;
            if (raw != null && !string.Equals(raw, other.raw, StringComparison.Ordinal)) return false;
            if (!TestEquals(elements, other.elements)) return false;
            if (!TestEquals(properties, other.properties)) return false;
            if (id != null && !TestEquals(id, other.id)) return false;
            if (@params != null && !TestEquals(@params, other.@params)) return false;
            if (fbody != null && !TestEquals(fbody, other.fbody)) return false;
            if (!TestEquals(argument, other.argument)) return false;
            if (test != null && !TestEquals(test, other.test)) return false;
            if (!TestEquals(consequent, other.consequent)) return false;
            if (!TestEquals(callee, other.callee)) return false;
            if (argument != null && !TestEquals(arguments, other.arguments)) return false;
            if (!TestEquals(alternate, other.alternate)) return false;
            if (!TestEquals(discriminant, other.discriminant)) return false;
            if (!TestEquals(cases, other.cases)) return false;
            if (!TestEquals(declarations, other.declarations)) return false;
            if (!TestEquals(sconsequent, other.sconsequent)) return false;
            if (!TestEquals(init, other.init)) return false;
            if (!string.Equals(kind, other.kind, StringComparison.Ordinal)) return false;
            if (!TestEquals(@object, other.@object)) return false;
            if (!TestEquals(property, other.property)) return false;
            if (computed != other.computed) return false;
            if (!TestEquals(expressions, other.expressions)) return false;
            if (prefix != other.prefix) return false;
            if (!TestEquals(update, other.update)) return false;
            if (!TestEquals(label, other.label)) return false;
            if (!TestEquals(block, other.block)) return false;
            if (!TestEquals(handler, other.handler)) return false;
            if (!TestEquals(param, other.param)) return false;
            if (!TestEquals(finalizer, other.finalizer)) return false;
            if (generator && generator != other.generator) return false;
            if (async && async != other.async) return false;
            if (@static != other.@static) return false;
            if (!TestEquals(key, other.key)) return false;
            if (directive != null && !string.Equals(directive, other.directive, StringComparison.Ordinal)) return false;
            if (!TestEquals(tag, other.tag)) return false;
            if (!TestEquals(quasi, other.quasi)) return false;
            if (@delegate != other.@delegate) return false;
            if (!TestEquals(meta, other.meta)) return false;
            if (!TestEquals(superClass, other.superClass)) return false;
            if (!TestEquals(source, other.source)) return false;
            if (declaration != null && !TestEquals(declaration, other.declaration)) return false;
            if (!TestEquals(specifiers, other.specifiers)) return false;
            if (tail != other.tail) return false;
            if (method != other.method) return false;
            if (!TestEquals(local, other.local)) return false;
            if (!TestEquals(exported, other.exported)) return false;
            if (!TestEquals(quasis, other.quasis)) return false;
            if (!TestEquals(imported, other.imported)) return false;
            if (shorthand != other.shorthand) return false;
            if (sourceType != null && !string.Equals(sourceType, other.sourceType, StringComparison.Ordinal)) return false;
            return true;
        }

        internal static bool TestEquals(object lhs, object rhs)
        {
            if (ReferenceEquals(lhs, rhs))
                return true;
            if (ReferenceEquals(lhs, null))
                return false;

            if (lhs is BaseNode lhsNode && rhs is BaseNode rhsNode)
                return lhsNode.TestEquals(rhsNode);

            if (lhs is int lhsInt && rhs is double rhsDouble)
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                return lhsInt == rhsDouble;
            }

            return Equals(lhs, rhs);
        }

        internal static bool TestEquals(BaseNode lhs, BaseNode rhs)
        {
            if (ReferenceEquals(lhs, rhs))
                return true;
            if (ReferenceEquals(lhs, null))
                return false;

            return lhs.TestEquals(rhs);
        }

        internal static bool TestEquals(IList<BaseNode> lhs, IList<BaseNode> rhs)
        {
            if (ReferenceEquals(lhs, rhs))
                return true;
            if (ReferenceEquals(lhs, null))
                return false;

            if (lhs.Count != rhs.Count)
                return false;

            for (var i = 0; i < lhs.Count; i++)
            {
                var lhsChild = lhs[i];
                var rhsChild = rhs[i];
                if (ReferenceEquals(lhsChild, rhsChild))
                    continue;
                if (ReferenceEquals(lhsChild, null))
                    return false;

                if (!lhsChild.TestEquals(rhsChild))
                    return false;
            }
            return true;
        }

        public virtual bool Equals([CanBeNull] BaseNode other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;

            if (!Equals(type, other.type)) return false;
            if (!Equals(body, other.body)) return false;
            if (!Equals(expression, other.expression)) return false;
            if (bexpression != other.bexpression) return false;
            if (!Equals(loc, other.loc)) return false;
            if (!Equals(value, other.value)) return false;
            if (!Equals(regex, other.regex)) return false;
            if (!Equals(left, other.left)) return false;
            if (!string.Equals(@operator, other.@operator)) return false;
            if (!Equals(right, other.right)) return false;
            if (!string.Equals(raw, other.raw)) return false;
            if (!Equals(elements, other.elements)) return false;
            if (!Equals(properties, other.properties)) return false;
            if (!Equals(id, other.id)) return false;
            if (!Equals(@params, other.@params)) return false;
            if (!Equals(fbody, other.fbody)) return false;
            if (!Equals(argument, other.argument)) return false;
            if (!Equals(test, other.test)) return false;
            if (!Equals(consequent, other.consequent)) return false;
            if (!Equals(callee, other.callee)) return false;
            if (!Equals(arguments, other.arguments)) return false;
            if (!Equals(alternate, other.alternate)) return false;
            if (!Equals(discriminant, other.discriminant)) return false;
            if (!Equals(cases, other.cases)) return false;
            if (!Equals(declarations, other.declarations)) return false;
            if (!Equals(sconsequent, other.sconsequent)) return false;
            if (!Equals(init, other.init)) return false;
            if (!string.Equals(kind, other.kind)) return false;
            if (!Equals(@object, other.@object)) return false;
            if (!Equals(property, other.property)) return false;
            if (computed != other.computed) return false;
            if (!Equals(expressions, other.expressions)) return false;
            if (prefix != other.prefix) return false;
            if (!Equals(update, other.update)) return false;
            if (!Equals(label, other.label)) return false;
            if (!Equals(block, other.block)) return false;
            if (!Equals(handler, other.handler)) return false;
            if (!Equals(param, other.param)) return false;
            if (!Equals(finalizer, other.finalizer)) return false;
            if (generator != other.generator) return false;
            if (async != other.async) return false;
            if (@static != other.@static) return false;
            if (!Equals(key, other.key)) return false;
            if (!string.Equals(directive, other.directive)) return false;
            if (!Equals(tag, other.tag)) return false;
            if (!Equals(quasi, other.quasi)) return false;
            if (@delegate != other.@delegate) return false;
            if (!Equals(meta, other.meta)) return false;
            if (!Equals(superClass, other.superClass)) return false;
            if (!Equals(source, other.source)) return false;
            if (!Equals(declaration, other.declaration)) return false;
            if (!Equals(specifiers, other.specifiers)) return false;
            if (tail != other.tail) return false;
            if (method != other.method) return false;
            if (!Equals(local, other.local)) return false;
            if (!Equals(exported, other.exported)) return false;
            if (!Equals(quasis, other.quasis)) return false;
            if (!Equals(imported, other.imported)) return false;
            if (shorthand != other.shorthand) return false;
            if (!string.Equals(sourceType, other.sourceType)) return false;
            return true;
        }

        private static bool Equals(IList<BaseNode> lhs, IList<BaseNode> rhs)
        {
            if (ReferenceEquals(lhs, rhs))
                return true;
            if (ReferenceEquals(lhs, null))
                return false;

            if (lhs.Count != rhs.Count)
                return false;

            for (var i = 0; i < lhs.Count; i++)
            {
                var lhsChild = lhs[i];
                var rhsChild = rhs[i];
                if (lhsChild != rhsChild)
                    return false;
            }
            return true;
        }

        public override bool Equals([CanBeNull] object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((BaseNode)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = type.GetHashCode();
                hashCode = (hashCode * 397) ^ (body != null ? body.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (expression != null ? expression.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ bexpression.GetHashCode();
                hashCode = (hashCode * 397) ^ (loc != null ? loc.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (value != null ? value.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (regex != null ? regex.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (left != null ? left.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (@operator != null ? @operator.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (right != null ? right.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (raw != null ? raw.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (elements != null ? elements.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (properties != null ? properties.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (id != null ? id.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (@params != null ? @params.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (fbody != null ? fbody.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (argument != null ? argument.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (test != null ? test.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (consequent != null ? consequent.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (callee != null ? callee.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (arguments != null ? arguments.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (alternate != null ? alternate.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (discriminant != null ? discriminant.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (cases != null ? cases.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (declarations != null ? declarations.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (sconsequent != null ? sconsequent.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (init != null ? init.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (kind != null ? kind.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (@object != null ? @object.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (property != null ? property.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ computed.GetHashCode();
                hashCode = (hashCode * 397) ^ (expressions != null ? expressions.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ prefix.GetHashCode();
                hashCode = (hashCode * 397) ^ (update != null ? update.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (label != null ? label.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (block != null ? block.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (handler != null ? handler.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (param != null ? param.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (finalizer != null ? finalizer.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ generator.GetHashCode();
                hashCode = (hashCode * 397) ^ async.GetHashCode();
                hashCode = (hashCode * 397) ^ @static.GetHashCode();
                hashCode = (hashCode * 397) ^ (key != null ? key.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (directive != null ? directive.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (tag != null ? tag.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (quasi != null ? quasi.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ @delegate.GetHashCode();
                hashCode = (hashCode * 397) ^ (meta != null ? meta.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (superClass != null ? superClass.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (source != null ? source.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (declaration != null ? declaration.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (specifiers != null ? specifiers.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ tail.GetHashCode();
                hashCode = (hashCode * 397) ^ method.GetHashCode();
                hashCode = (hashCode * 397) ^ (local != null ? local.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (exported != null ? exported.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (quasis != null ? quasis.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (imported != null ? imported.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ shorthand.GetHashCode();
                hashCode = (hashCode * 397) ^ (sourceType != null ? sourceType.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==([CanBeNull] BaseNode left, [CanBeNull] BaseNode right)
        {
            return Equals(left, right);
        }

        public static bool operator !=([CanBeNull] BaseNode left, [CanBeNull] BaseNode right)
        {
            return !Equals(left, right);
        }
    }
}