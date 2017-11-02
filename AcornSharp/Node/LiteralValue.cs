using System;
using System.Globalization;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public struct LiteralValue : IEquatable<LiteralValue>
    {
        private enum Type
        {
            Null,
            Boolean,
            Double,
            String,
            Regex
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct Union
        {
            [FieldOffset(0)] public readonly bool boolValue;
            [FieldOffset(0)] public readonly double doubleValue;

            public Union(bool value)
                : this()
            {
                boolValue = value;
            }

            public Union(double value)
                : this()
            {
                doubleValue = value;
            }
        }

        private readonly Union union;
        private readonly string stringValue;
        private readonly RegexNode regexValue;
        private readonly Type type;

        private LiteralValue(Type type)
            : this()
        {
            this.type = type;
        }

        public LiteralValue(bool value)
            : this(Type.Boolean)
        {
            union = new Union(value);
        }

        public LiteralValue(double value)
            : this(Type.Double)
        {
            union = new Union(value);
        }

        public LiteralValue(string value)
            : this(Type.String)
        {
            stringValue = value;
        }

        public LiteralValue(RegexNode value)
            : this(Type.Regex)
        {
            regexValue = value;
        }

        public bool Equals(LiteralValue other)
        {
            if (type != other.type)
                return false;
            switch (type)
            {
                case Type.Null:
                    return true;
                case Type.Boolean:
                    return union.boolValue == other.union.boolValue;
                case Type.Double:
                    // ReSharper disable once CompareOfFloatsByEqualityOperator
                    return union.doubleValue == other.union.doubleValue;
                case Type.String:
                    return stringValue == other.stringValue;
                case Type.Regex:
                    return regexValue.Equals(other.regexValue);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override bool Equals([CanBeNull] object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is LiteralValue value && Equals(value);
        }

        public override int GetHashCode()
        {
            switch (type)
            {
                case Type.Null:
                    return 0;
                case Type.Boolean:
                    return union.boolValue.GetHashCode();
                case Type.Double:
                    return union.doubleValue.GetHashCode();
                case Type.String:
                    return stringValue.GetHashCode();
                case Type.Regex:
                    return regexValue.GetHashCode();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override string ToString()
        {
            switch (type)
            {
                case Type.Null:
                    return "null";
                case Type.Boolean:
                    return union.boolValue.ToString();
                case Type.Double:
                    // ReSharper disable once ImpureMethodCallOnReadonlyValueField
                    return union.doubleValue.ToString(CultureInfo.InvariantCulture);
                case Type.String:
                    return stringValue;
                case Type.Regex:
                    return regexValue.ToString();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static implicit operator LiteralValue(bool value)
        {
            return new LiteralValue(value);
        }

        public static implicit operator LiteralValue(double value)
        {
            return new LiteralValue(value);
        }

        public static implicit operator LiteralValue([CanBeNull] string value)
        {
            if (value == null)
                return new LiteralValue();
            return new LiteralValue(value);
        }

        public static implicit operator LiteralValue([CanBeNull] RegexNode value)
        {
            if (value == null)
                return new LiteralValue();
            return new LiteralValue(value);
        }

        public static bool operator ==(LiteralValue left, LiteralValue right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(LiteralValue left, LiteralValue right)
        {
            return !left.Equals(right);
        }

        public bool IsNull => type == Type.Null;
        public bool IsBoolean => type == Type.Boolean;
        public bool IsDouble => type == Type.Double;
        public bool IsString => type == Type.String;
        public bool IsRegex => type == Type.Regex;

        public bool AsBoolean
        {
            get
            {
                if (!IsBoolean)
                    throw new InvalidOperationException();
                return union.boolValue;
            }
        }

        public double AsDouble
        {
            get
            {
                if (!IsDouble)
                    throw new InvalidOperationException();
                return union.doubleValue;
            }
        }

        public string AsString
        {
            get
            {
                if (!IsString)
                    throw new InvalidOperationException();
                return stringValue;
            }
        }

        public RegexNode AsRegex
        {
            get
            {
                if (!IsRegex)
                    throw new InvalidOperationException();
                return regexValue;
            }
        }
    }
}