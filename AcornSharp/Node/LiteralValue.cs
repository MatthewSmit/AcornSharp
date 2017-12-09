using System;
using System.Globalization;
using System.Runtime.InteropServices;
using JetBrains.Annotations;

namespace AcornSharp.Node
{
    public struct LiteralValue : IEquatable<LiteralValue>
    {
        public enum LiteralType
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

        private LiteralValue(LiteralType type)
            : this()
        {
            Type = type;
        }

        public LiteralValue(bool value)
            : this(LiteralType.Boolean)
        {
            union = new Union(value);
        }

        public LiteralValue(double value)
            : this(LiteralType.Double)
        {
            union = new Union(value);
        }

        public LiteralValue(string value)
            : this(LiteralType.String)
        {
            stringValue = value;
        }

        public LiteralValue(RegexNode value)
            : this(LiteralType.Regex)
        {
            regexValue = value;
        }

        public bool Equals(LiteralValue other)
        {
            if (Type != other.Type)
                return false;
            switch (Type)
            {
                case LiteralType.Null:
                    return true;
                case LiteralType.Boolean:
                    return union.boolValue == other.union.boolValue;
                case LiteralType.Double:
                    // ReSharper disable once CompareOfFloatsByEqualityOperator
                    return union.doubleValue == other.union.doubleValue;
                case LiteralType.String:
                    return stringValue == other.stringValue;
                case LiteralType.Regex:
                    return regexValue.Equals(other.regexValue);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is LiteralValue value && Equals(value);
        }

        public override int GetHashCode()
        {
            switch (Type)
            {
                case LiteralType.Null:
                    return 0;
                case LiteralType.Boolean:
                    return union.boolValue.GetHashCode();
                case LiteralType.Double:
                    return union.doubleValue.GetHashCode();
                case LiteralType.String:
                    return stringValue.GetHashCode();
                case LiteralType.Regex:
                    return regexValue.GetHashCode();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override string ToString()
        {
            switch (Type)
            {
                case LiteralType.Null:
                    return "null";
                case LiteralType.Boolean:
                    return union.boolValue.ToString();
                case LiteralType.Double:
                    // ReSharper disable once ImpureMethodCallOnReadonlyValueField
                    return union.doubleValue.ToString(CultureInfo.InvariantCulture);
                case LiteralType.String:
                    return stringValue;
                case LiteralType.Regex:
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

        public bool IsNull => Type == LiteralType.Null;
        public bool IsBoolean => Type == LiteralType.Boolean;
        public bool IsDouble => Type == LiteralType.Double;
        public bool IsString => Type == LiteralType.String;
        public bool IsRegex => Type == LiteralType.Regex;

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

        public LiteralType Type { get; }
    }
}