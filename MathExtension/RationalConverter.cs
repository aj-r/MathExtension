using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace MathExtension
{
    /// <summary>
    /// Converts to and from the <see cref="Rational"/> type.
    /// </summary>
    public class RationalConverter : TypeConverter
    {
        /// <inheritdoc />
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            sourceType = GetUnderlyingType(sourceType);
            return sourceType == typeof(Rational)
                || sourceType == typeof(string)
                || sourceType == typeof(int)
                || sourceType == typeof(double)
                || sourceType == typeof(long)
                || sourceType == typeof(short)
                || sourceType == typeof(uint)
                || sourceType == typeof(ulong)
                || sourceType == typeof(ushort)
                || sourceType == typeof(float)
                || sourceType == typeof(decimal)
                || sourceType == typeof(byte)
                || sourceType == typeof(sbyte)
                || sourceType == typeof(bool);
        }

        /// <inheritdoc />
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value == null)
                return (Rational?)null;

            if (value is Rational)
                return (Rational)value;
            if (value is string)
                return Rational.Parse((string)value, culture);
            if (value is int)
                return new Rational((int)value);
            if (value is double)
                return Rational.FromDouble((double)value);
            if (value is long)
                return (Rational)(long)value;
            if (value is short)
                return (Rational)(short)value;
            if (value is uint)
                return (Rational)(uint)value;
            if (value is ulong)
                return (Rational)(ulong)value;
            if (value is ushort)
                return (Rational)(ushort)value;
            if (value is float)
                return (Rational)(float)value;
            if (value is byte)
                return (Rational)(byte)value;
            if (value is sbyte)
                return (Rational)(sbyte)value;
            if (value is bool)
                return (bool)value ? Rational.One : Rational.Zero;

            throw new ArgumentException("Inavlid value type: " + value.GetType().FullName, "value");
        }

        /// <inheritdoc />
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            destinationType = GetUnderlyingType(destinationType);
            return destinationType == typeof(Rational)
                || destinationType == typeof(string)
                || destinationType == typeof(int)
                || destinationType == typeof(double)
                || destinationType == typeof(long)
                || destinationType == typeof(short)
                || destinationType == typeof(uint)
                || destinationType == typeof(ulong)
                || destinationType == typeof(ushort)
                || destinationType == typeof(float)
                || destinationType == typeof(decimal)
                || destinationType == typeof(byte)
                || destinationType == typeof(sbyte)
                || destinationType == typeof(bool);
        }

        /// <inheritdoc />
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value == null)
            {
                if (IsNullableType(destinationType))
                    return null;

                throw new InvalidCastException("Cannot convert null to type " + destinationType.FullName);
            }
            if (!(value is Rational))
                throw new ArgumentException("value must be a rational.", "value");

            var r = (Rational)value;
            if (destinationType == typeof(Rational))
                return r;
            if (destinationType == typeof(string))
                return r.ToString(culture);
            if (destinationType == typeof(int))
                return Rational.Round(r);
            if (destinationType == typeof(double))
                return r.Value;
            if (destinationType == typeof(long))
                return (long)Rational.Round(r);
            if (destinationType == typeof(short))
                return (short)Rational.Round(r);
            if (destinationType == typeof(uint))
                return (uint)Rational.Round(r);
            if (destinationType == typeof(ulong))
                return (ulong)Rational.Round(r);
            if (destinationType == typeof(ushort))
                return (ushort)Rational.Round(r);
            if (destinationType == typeof(float))
                return (float)r;
            if (destinationType == typeof(byte))
                return (byte)Rational.Round(r);
            if (destinationType == typeof(sbyte))
                return (sbyte)Rational.Round(r);
            if (destinationType == typeof(bool))
                return !Rational.IsZero(r);

            throw new ArgumentException("Inavlid destinationType: " + destinationType.FullName, "destinationType");
        }

        private static bool IsNullableType(Type t)
        {
#if NET40
            var isGeneric = t.IsGenericType;
#else
            var isGeneric = t.GetTypeInfo().IsGenericType;
#endif
            return (isGeneric && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        private static Type GetUnderlyingType(Type t)
        {
            return IsNullableType(t) ? Nullable.GetUnderlyingType(t) : t;
        }
    }
}
