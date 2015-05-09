﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MathExtension
{
    [Serializable]
    public struct Rational : IComparable, IComparable<Rational>, IConvertible, IEquatable<Rational>, IFormattable
    {
        #region Members

        private readonly int _numerator;
        private readonly int _denominator;

        /// <summary>
        /// Represents the number zero.
        /// </summary>
        public static readonly Rational Zero = new Rational(0, 1);

        /// <summary>
        /// Represents the number one.
        /// </summary>
        public static readonly Rational One = new Rational(1, 1);

        /// <summary>
        /// Represents the minimum finite value of a <see cref="Rational"/>.
        /// </summary>
        public static readonly Rational MinValue = new Rational(int.MinValue, 1);

        /// <summary>
        /// Represents the maximum finite value of a <see cref="Rational"/>.
        /// </summary>
        public static readonly Rational MaxValue = new Rational(int.MaxValue, 1);

        /// <summary>
        /// Represents an indeterminate value.
        /// </summary>
        public static readonly Rational Indeterminate = new Rational(0, 0);

        /// <summary>
        /// Represents positive infinity.
        /// </summary>
        public static readonly Rational PositiveInfinity = new Rational(1, 0);

        /// <summary>
        /// Represents negative infinity.
        /// </summary>
        public static readonly Rational NegativeInfinity = new Rational(-1, 0);

        /// <summary>
        /// Represents the minimum positive value of a <see cref="Rational"/>.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This field has a value of 1 / 2,147,483,647.
        /// </para>
        /// <para>
        /// This does NOT represent the minimum possible difference between two <see cref="Rational"/> instances; some rationals may have a smaller difference.
        /// If you try to subrtact two rationals whose difference is smaller than this value, you will get unexpected results due to overflow.
        /// </para>
        /// <example>
        /// To check for this case, you can add this value to one of the rationals and compare to the other rational.
        /// <code>
        ///   if (r1 + Rational.Epsilon &gt; r2 && r1 - Rational.Epsilon &lt; r2)
        ///   {
        ///     // Difference between r1 and r2 is less than Rational.Epsilon.
        ///   }
        /// </code>
        /// </example>
        /// </remarks>
        public static readonly Rational Epsilon = new Rational(1, int.MaxValue);

        #endregion

        #region Static Methods

        /// <summary>
        /// Converts the string representation of a number to its <see cref="Rational"/> representation.
        /// </summary>
        /// <param name="s">A string that represents a number.</param>
        /// <returns>A <see cref="Rational"/>.</returns>
        public static Rational Parse(string s)
        {
            return Parse(s, null);
        }

        /// <summary>
        /// Converts the string representation of a number to its <see cref="Rational"/> representation.
        /// </summary>
        /// <param name="s">A string that represents a number.</param>
        /// <param name="style">Indicates the styles that can be present when parsing a number.</param>
        /// <returns>A <see cref="Rational"/>.</returns>
        public static Rational Parse(string s, NumberStyles style)
        {
            return Parse(s, style, null);
        }

        /// <summary>
        /// Converts the string representation of a number to its <see cref="Rational"/> representation.
        /// </summary>
        /// <param name="s">A string that represents a number.</param>
        /// <param name="provider">An object that supplies culture-specific information about the format of <paramref name="s"/>.</param>
        /// <returns>A <see cref="Rational"/>.</returns>
        public static Rational Parse(string s, IFormatProvider provider)
        {
            return Parse(s, NumberStyles.Any, null);
        }

        /// <summary>
        /// Converts the string representation of a number to its <see cref="Rational"/> representation.
        /// </summary>
        /// <param name="s">A string that represents a number.</param>
        /// <param name="style">Indicates the styles that can be present when parsing a number.</param>
        /// <param name="provider">An object that supplies culture-specific information about the format of <paramref name="s"/>.</param>
        /// <returns>A <see cref="Rational"/>.</returns>
        public static Rational Parse(string s, NumberStyles style, IFormatProvider provider)
        {
            Rational result;
            TryParse(s, style, provider, true, out result);
            return result;
        }

        /// <summary>
        /// Converts the string representation of a number to its <see cref="Rational"/> representation. A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="s">A string that represents a number.</param>
        /// <returns>True if the conversion succeeded; otherwise false.</returns>
        public static bool TryParse(string s, out Rational result)
        {
            return TryParse(s, null, out result);
        }

        /// <summary>
        /// Converts the string representation of a number to its <see cref="Rational"/> representation. A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="s">A string that represents a number.</param>
        /// <param name="style">Indicates the styles that can be present when parsing a number.</param>
        /// <returns>True if the conversion succeeded; otherwise false.</returns>
        public static bool TryParse(string s, NumberStyles style, out Rational result)
        {
            return TryParse(s, style, null, out result);
        }

        /// <summary>
        /// Converts the string representation of a number to its <see cref="Rational"/> representation. A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="s">A string that represents a number.</param>
        /// <param name="provider">An object that supplies culture-specific information about the format of <paramref name="s"/>.</param>
        /// <returns>True if the conversion succeeded; otherwise false.</returns>
        public static bool TryParse(string s, IFormatProvider provider, out Rational result)
        {
            return TryParse(s, NumberStyles.Any, provider, out result);
        }

        /// <summary>
        /// Converts the string representation of a number to its <see cref="Rational"/> representation. A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="s">A string that represents a number.</param>
        /// <param name="style">Indicates the styles that can be present when parsing a number.</param>
        /// <param name="provider">An object that supplies culture-specific information about the format of <paramref name="s"/>.</param>
        /// <returns>True if the conversion succeeded; otherwise false.</returns>
        public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out Rational result)
        {
            return TryParse(s, style, provider, false, out result);
        }

        private static bool TryParse(string s, NumberStyles style, IFormatProvider provider, bool throwOnFailure, out Rational result)
        {
            if (string.IsNullOrWhiteSpace(s))
                return ParseFailure(throwOnFailure, out result);
            var parts = s.Split('/');
            if (parts.Length > 2)
                return ParseFailure(throwOnFailure, out result);
            if (parts.Length == 1)
            {
                int n;
                if (!int.TryParse(s, style, provider, out n))
                {
                    // Maybe the number is in decimal format. Try parsing as such.
                    double d;
                    if (!double.TryParse(s, style, provider, out d))
                        return ParseFailure(throwOnFailure, out result);
                    result = FromDouble(d);
                    return true;
                }
                result = new Rational(n);
                return true;
            }

            var numeratorString = parts[0].Trim();

            var separatorIndex = numeratorString.LastIndexOf('-');
            if (separatorIndex <= 0)
                separatorIndex = numeratorString.LastIndexOf(' ');
            int integerPart = 0;
            if (separatorIndex > 0)
            {
                var integerString = numeratorString.Remove(separatorIndex);
                numeratorString = numeratorString.Substring(separatorIndex + 1);
                if (!TryParseInt(integerString, style, provider, throwOnFailure, out integerPart))
                {
                    result = Indeterminate;
                    return false;
                }
            }
            int numerator, denominator;
            if (!TryParseInt(numeratorString, style, provider, throwOnFailure, out numerator)
                || !TryParseInt(parts[1], style, provider, throwOnFailure, out denominator))
            {
                result = Indeterminate;
                return false;
            }
            if (integerPart < 0)
                numerator *= -1;

            result = new Rational(integerPart * denominator + numerator, denominator);
            return true;
        }

        private static bool ParseFailure(bool throwOnFailure, out Rational result)
        {
            if (throwOnFailure)
                throw new FormatException();
            result = Indeterminate;
            return false;
        }

        private static bool TryParseInt(string s, NumberStyles style, IFormatProvider provider, bool throwOnFailure, out int result)
        {
            if (throwOnFailure)
            {
                result = int.Parse(s, style, provider);
                return true;
            }
            return int.TryParse(s, style, provider, out result);
        }

        /// <summary>
        /// Converts a floating-point number to a rational number.
        /// </summary>
        /// <param name="value">A floating-point number to convert to a rational number.</param>
        /// <returns>A rational number.</returns>
        public static Rational FromDouble(double value, double tolerance = MathEx.DEFAULT_TOLERANCE)
        {
            if (double.IsPositiveInfinity(value))
                return PositiveInfinity;
            if (double.IsNegativeInfinity(value))
                return NegativeInfinity;
            if (double.IsNaN(value))
                return Indeterminate;

            // TODO: this algorithm has really bad rounding errors. Use a better algorithm
            // if this function will used often.

            bool isNegative = false;
            if (value < 0)
            {
                value *= -1;
                isNegative = true;
            }
            // Set numerator to 'value' for now; we will set it to the actual numerator once we know
            // what the denominator is.
            double numerator = value;

            // Get the denominator
            double denominator = 1.0;
            double fractionPart = value - Math.Truncate(value);
            int n = 0;
            while (!MathEx.IsInteger(fractionPart, tolerance) && n < 100)
            {
                value = 1.0 / fractionPart;
                denominator *= value;
                fractionPart = value - Math.Truncate(value);
                n++;
            }

            // Get the actual numerator
            numerator *= denominator;
            if (isNegative)
                numerator *= -1;
            return new Rational(Convert.ToInt32(numerator), Convert.ToInt32(denominator));
        }

        /// <summary>
        /// Converts a floating-point decimal to a rational number.
        /// </summary>
        /// <param name="value">A floating-point number to convert to a rational number.</param>
        /// <param name="maxDenominator">The maximum value that the denominator can have.</param>
        /// <returns>A rational number.</returns>
        public static Rational FromDoubleWithMaxDenominator(double value, int maxDenominator, double tolerance = MathEx.DEFAULT_TOLERANCE)
        {
            if (double.IsPositiveInfinity(value))
                return PositiveInfinity;
            if (double.IsNegativeInfinity(value))
                return NegativeInfinity;
            if (double.IsNaN(value))
                return Indeterminate;

            if (maxDenominator < 1)
            {
                throw new ArgumentException("Maximum denominator base must be greater than or equal to 1.", "maxDenominator");
            }

            int denominator = 0;
            int bestDenominator = 1;
            double bestDifference = 1.0;
            double numerator;
            do
            {
                denominator++;
                numerator = value * (double)denominator;
                double difference = numerator % 1.0;
                if (difference < bestDifference)
                {
                    bestDifference = difference;
                    bestDenominator = denominator;
                }
            } while (!MathEx.IsInteger(numerator, tolerance) && denominator < maxDenominator);

            return new Rational(Convert.ToInt32(numerator), denominator);
        }

        /// <summary>
        /// Returns the absolute value of a Rational.
        /// </summary>
        /// <param name="value">A Rational number.</param>
        /// <returns>Gets the absolute value of the Rational.</returns>
        public static Rational Abs(Rational value)
        {
            return new Rational(Math.Abs(value.Numerator), Math.Abs(value.Denominator));
        }

        /// <summary>
        /// Returns the smaller of two Rationals.
        /// </summary>
        /// <param name="val1">A Rational number.</param>
        /// <param name="val2">A Rational number.</param>
        /// <returns>The smaller of two Rationals.</returns>
        public static Rational Min(Rational val1, Rational val2)
        {
            return val1 <= val2 ? val1 : val2;
        }

        /// <summary>
        /// Returns the larger of two Rationals.
        /// </summary>
        /// <param name="val1">A Rational number.</param>
        /// <param name="val2">A Rational number.</param>
        /// <returns>The larger of two Rationals.</returns>
        public static Rational Max(Rational val1, Rational val2)
        {
            return val1 >= val2 ? val1 : val2;
        }

        /// <summary>
        /// Returns a specified number raised to a specified power.
        /// </summary>
        /// <param name="val1">A Rational number.</param>
        /// <param name="val2">A Rational number.</param>
        /// <returns>The larger of two Rationals.</returns>
        public static Rational Pow(Rational baseValue, int exponent)
        {
            var result = Rational.One;
            if (exponent < 0)
            {
                exponent *= -1;
                baseValue = baseValue.Inverse();
            }
            while (exponent > 0)
            {
                if ((exponent & 1) == 1)
                    result *= baseValue;
                exponent >>= 1;
                baseValue *= baseValue;
            }
            return result;
        }

        /// <summary>
        /// Calculates the quotient of two rational numbers and also returns the remainder in an output parameter.
        /// </summary>
        /// <param name="a">The dividend.</param>
        /// <param name="b">The divisor.</param>
        /// <param name="remainder">The remainder.</param>
        /// <returns>The quotient.</returns>
        public static int DivRem(Rational a, Rational b, out Rational remainder)
        {
            if (b._numerator == 0 || a._denominator == 0)
                throw new DivideByZeroException();
            if (b._denominator == 0)
            {
                remainder = a;
                return 0;
            }
            int xNum = a._numerator * b._denominator;
            int yNum = b._numerator * a._denominator;
            int denominator = a._denominator * b._denominator;
            int rem;
            var result = Math.DivRem(xNum, yNum, out rem);
            remainder = Rational.Simplify(rem, denominator);
            return result;
        }

        /// <summary>
        /// Returns a value that indicates whether the specified <see cref="Rational"/> evaluates to positive or negative infinity.
        /// </summary>
        /// <param name="r">A rational number.</param>
        /// <returns>True if <paramref name="r"/> evaluates to positive or negative infinity; otherwise false.</returns>
        public bool IsInfinity(Rational r)
        {
            return r._denominator == 0 && r._numerator != 0;
        }

        /// <summary>
        /// Returns a value that indicates whether the specified <see cref="Rational"/> evaluates to positive infinity.
        /// </summary>
        /// <param name="r">A rational number.</param>
        /// <returns>True if <paramref name="r"/> evaluates to positive infinity; otherwise false.</returns>
        public bool IsPositiveInfinity(Rational r)
        {
            return r._denominator == 0 && r._numerator > 0;
        }

        /// <summary>
        /// Returns a value that indicates whether the specified <see cref="Rational"/> evaluates to negative infinity.
        /// </summary>
        /// <param name="r">A rational number.</param>
        /// <returns>True if <paramref name="r"/> evaluates to negative infinity; otherwise false.</returns>
        public bool IsNegativeInfinity(Rational r)
        {
            return r._denominator == 0 && r._numerator < 0;
        }

        /// <summary>
        /// Returns a value that indicates whether the specified <see cref="Rational"/> represents an indeterminate value.
        /// </summary>
        /// <param name="r">A rational number.</param>
        /// <returns>True if <paramref name="r"/> represents an indeterminate value; otherwise false.</returns>
        public bool IsIndeterminate(Rational r)
        {
            return r._denominator == 0 && r._numerator == 0;
        }

        /// <summary>
        /// Returns a value that indicates whether the specified <see cref="Rational"/> evaluates to zero.
        /// </summary>
        /// <param name="r">A rational number.</param>
        /// <returns>True if <paramref name="r"/> evaluates to zero; otherwise false.</returns>
        public bool IsZero(Rational r)
        {
            return r._numerator == 0 && r._denominator != 0;
        }

        #endregion

        #region Constructors

        public Rational(int numerator)
            : this(numerator, 1)
        { }

        public Rational(int numerator, int denominator)
        {
            _numerator = numerator;
            _denominator = denominator;
        }

        public Rational(double value)
        {
            this = FromDouble(value);
        }

        #endregion

        #region Properties

        public int Numerator
        {
            get { return _numerator; }
        }

        public int Denominator
        {
            get { return _denominator; }
        }

        public double Value
        {
            get
            {
                return (double)Numerator / (double)Denominator;
            }
        }

        public Rational Inverse()
        {
            if (Numerator >= 0)
            {
                return new Rational(Denominator, Numerator);
            }
            else
            {
                return new Rational(-Denominator, -Numerator);
            }
        }

        public Rational Negate()
        {
            return new Rational(-Numerator, Denominator);
        }

        #endregion

        #region Instance Methods

        /// <summary>
        /// Gets the simplified version of the rational number.
        /// </summary>
        public static Rational Simplify(int numerator, int denominator)
        {
            if (denominator == 0)
                return new Rational(Math.Sign(numerator), 0);

            var gcd = MathEx.Gcd(numerator, denominator);
            // The denominator in the simplified version should always be positive,
            // so if it is negative, multiply both numbers by -1.
            if (denominator < 0)
                gcd *= -1;
            return new Rational(numerator / gcd, denominator / gcd);
        }

        /// <summary>
        /// Gets the simplified version of the rational number.
        /// </summary>
        public static Rational Simplify(long numerator, long denominator)
        {
            if (denominator == 0)
                return new Rational(Math.Sign(numerator), 0);

            var gcd = MathEx.Gcd(numerator, denominator);
            if (denominator < 0)
                gcd *= -1;
            return new Rational((int)(numerator / gcd), (int)(denominator / gcd));
        }

        /// <summary>
        /// Gets the simplified version of the rational number.
        /// </summary>
        public Rational Simplify()
        {
            return Simplify(_numerator, _denominator);
        }

        /// <summary>
        /// Converts the Rational to a string in the form of an improper fraction.
        /// </summary>
        /// <returns>A string representaion of the rational number.</returns>
        public override string ToString()
        {
            return Numerator.ToString() + (Denominator != 1 ? "/" + Denominator.ToString() : string.Empty);
        }

        /// <summary>
        /// Converts the Rational to a string in the form of a mixed fraction.
        /// </summary>
        /// <returns>A string representaion of the rational number.</returns>
        public string ToMixedString()
        {
            return ToMixedString(" ");
        }

        /// <summary>
        /// Converts the Rational to a string in the form of a mixed fraction.
        /// </summary>
        /// <param name="numberSeparator">The separator between the number part and the fraction part</param>
        /// <returns>A string representaion of the rational number.</returns>
        public string ToMixedString(string numberSeparator)
        {
            string s = string.Empty;
            Rational x = this;
            if (x < Zero)
            {
                s += "-";
                x = x.Negate();
            }
            else if (x.Numerator < 0)
            {
                // The numerator and denominator are both negative
                x = new Rational(-x.Numerator, -x.Denominator);
            }
            bool hasIntegerPart = false;
            if (x.Numerator >= x.Denominator)
            {
                s += ((int)x).ToString();
                hasIntegerPart = true;
            }
            Rational fractionPart = x % Rational.One;
            bool hasFractionPart = fractionPart.Numerator != 0;
            if (hasFractionPart)
            {
                if (hasIntegerPart)
                    s += numberSeparator;
                s += fractionPart.ToString();
            }
            else if (!hasIntegerPart)
            {
                s = "0";
            }
            return s;
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">Another object to compare to.</param>
        /// <returns><value>true</value> if the current object and <paramref name="obj"/> are the same type and represent the same value; otherwise, <value>false</value>.</returns>
        public override bool Equals(object obj)
        {
            return (obj is Rational) && this == (Rational)obj;
        }

        /// <summary>
        /// Indicates whether this instance and a specified <see cref="Rational"/> are strictly equal; that is, the two instances must have equal numerators and denominators.
        /// </summary>
        /// <param name="r">Another <see cref="Rational"/> to compare to.</param>
        /// <returns><value>true</value> if the current number and <paramref name="r"/> have equal numerators and denominators; otherwise, <value>false</value>.</returns>
        /// <remarks>
        /// The basic Equals implementation considers unsimplified fractions to be equal to their simplified forms; e.g. 2/4 = 1/2.
        /// This method considers those values to be different.
        /// </remarks>
        public bool StrictlyEquals(Rational r)
        {
            return Numerator == r.Numerator && Denominator == r.Denominator;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>the hash code.</returns>
        public override int GetHashCode()
        {
            // Get the hash code of the simplified Rational. Equivalent Rationals (e.g. 1/2 and 2/4) should have the same hash code.
            var simplified = Simplify();
            return FnvCombine(simplified.Numerator.GetHashCode(), simplified.Denominator.GetHashCode());
        }

        private static int FnvCombine(params int[] hashes)
        {
            unchecked // Overflow is ok
            {
                uint h = 2166136261;
                foreach (var hash in hashes)
                    h = (h * 16777619) ^ (uint)hash;
                return (int)h;
            }
        }

        #endregion

        #region Operators

        public static bool operator ==(Rational x, Rational y)
        {
            return x.CompareTo(y) == 0;
        }

        public static bool operator !=(Rational x, Rational y)
        {
            return !(x == y);
        }

        public static bool operator >(Rational x, Rational y)
        {
            return x.CompareTo(y) > 0;
        }

        public static bool operator >=(Rational x, Rational y)
        {
            return !(x < y);
        }

        public static bool operator <(Rational x, Rational y)
        {
            return x.CompareTo(y) < 0;
        }

        public static bool operator <=(Rational x, Rational y)
        {
            return !(x > y);
        }

        public static Rational operator +(Rational x)
        {
            return x;
        }

        public static Rational operator -(Rational x)
        {
            if (x.Denominator < 0)
                return new Rational(x.Numerator, -x.Denominator);
            else
                return new Rational(-x.Numerator, x.Denominator);
        }

        public static Rational operator +(Rational x, Rational y)
        {
            if (x.Denominator == 0)
            {
                if (y.Denominator == 0 && Math.Sign(x.Numerator) != Math.Sign(y.Numerator))
                    return Indeterminate;
                return new Rational(Math.Sign(x.Numerator), 0);
            }
            if (y.Denominator == 0)
            {
                if (y.Numerator == 0)
                    return Indeterminate;
                return new Rational(Math.Sign(y.Numerator), 0);
            }

            int denominator = MathEx.Lcm(x.Denominator, y.Denominator);
            int xFactor = denominator / x.Denominator;
            int yFactor = denominator / y.Denominator;
            int numerator = x.Numerator * xFactor + y.Numerator * yFactor;
            return Rational.Simplify(numerator, denominator);
        }

        public static Rational operator -(Rational x, Rational y)
        {
            if (x.Denominator == 0)
            {
                if (y.Denominator == 0 && Math.Sign(x.Numerator) != -Math.Sign(y.Numerator))
                    return Indeterminate;
                return new Rational(Math.Sign(x.Numerator), 0);
            }
            if (y.Denominator == 0)
            {
                if (y.Numerator == 0)
                    return Indeterminate;
                return new Rational(-Math.Sign(y.Numerator), 0);
            }

            int denominator = MathEx.Lcm(x.Denominator, y.Denominator);
            int xFactor = denominator / x.Denominator;
            int yFactor = denominator / y.Denominator;
            int numerator = x.Numerator * xFactor - y.Numerator * yFactor;
            return Rational.Simplify(numerator, denominator);
        }

        public static Rational operator *(Rational x, Rational y)
        {
            return Rational.Simplify(x.Numerator * y.Numerator, x.Denominator * y.Denominator);
        }

        public static Rational operator /(Rational x, Rational y)
        {
            return Rational.Simplify(x.Numerator * y.Denominator, x.Denominator * y.Numerator);
        }

        public static Rational operator %(Rational x, Rational y)
        {
            if (y.Denominator == 0)
                return x.Denominator == 0 || y.Numerator == 0 ? Indeterminate : x.Simplify();
            if (x.Denominator == 0)
                return x.Simplify();
            if (x.Numerator == 0 || y.Numerator == 0)
                return Zero;

            var xNum = Math.BigMul(x.Numerator, y.Denominator);
            var yNum = Math.BigMul(y.Numerator, x.Denominator);
            int denominator = x.Denominator * y.Denominator;
            return Rational.Simplify((int)(xNum % yNum), denominator);
        }

        public static Rational operator ++(Rational x)
        {
            return x + Rational.One;
        }

        public static Rational operator --(Rational x)
        {
            return x - Rational.One;
        }

        #endregion

        #region Casts

        public static implicit operator Rational(int x)
        {
            return new Rational(x);
        }

        public static explicit operator int(Rational x)
        {
            return x.Numerator / x.Denominator;
        }

        public static implicit operator Rational(uint x)
        {
            return new Rational(x);
        }

        public static explicit operator uint(Rational x)
        {
            return (uint)(x.Numerator / x.Denominator);
        }

        public static implicit operator Rational(short x)
        {
            return new Rational(x);
        }

        public static explicit operator short(Rational x)
        {
            return (short)(x.Numerator / x.Denominator);
        }

        public static implicit operator Rational(ushort x)
        {
            return new Rational(x);
        }

        public static explicit operator ushort(Rational x)
        {
            return (ushort)(x.Numerator / x.Denominator);
        }

        public static implicit operator Rational(long x)
        {
            return new Rational(x);
        }

        public static explicit operator long(Rational x)
        {
            return x.Numerator / x.Denominator;
        }

        public static implicit operator Rational(ulong x)
        {
            return new Rational(x);
        }

        public static explicit operator ulong(Rational x)
        {
            return (ulong)(x.Numerator / x.Denominator);
        }

        public static explicit operator Rational(float x)
        {
            return new Rational(x);
        }

        public static explicit operator float(Rational x)
        {
            return (float)x.Value;
        }

        public static explicit operator Rational(double x)
        {
            return new Rational(x);
        }

        public static explicit operator double(Rational x)
        {
            return x.Value;
        }

        public static explicit operator Rational(decimal x)
        {
            return Rational.FromDouble((double)x);
        }

        public static explicit operator decimal(Rational x)
        {
            return (decimal)x.Numerator / (decimal)x.Denominator;
        }

        #endregion

        #region IComparable Members

        public int CompareTo(object obj)
        {
            Rational r;
            if (!(obj is Rational))
            {
                r = (Rational)obj;
            }
            else
            {
                var d = Convert.ToDouble(obj);
                r = new Rational(d);
            }
            return CompareTo(r);
        }

        #endregion

        #region IComparable<Rational> Members

        public int CompareTo(Rational other)
        {
            if (Denominator == 0)
            {
                if (other.Denominator == 0)
                    return Math.Sign(Numerator).CompareTo(Math.Sign(other.Numerator));
                return Numerator == 0 ? -1 : Math.Sign(Numerator);
            }
            if (other.Denominator == 0)
            {
                return other.Numerator == 0 ? 1 : -Math.Sign(other.Numerator);
            }
            // Use BigMul to avoid losing data when multiplying large integers
            long value1 = Math.BigMul(Numerator, other.Denominator);
            long value2 = Math.BigMul(Denominator, other.Numerator);
            return value1.CompareTo(value2);
        }

        #endregion

        #region IFormattable Members

        /// <summary>
        /// Converts this instance to its equivalent string representation.
        /// </summary>
        /// <param name="format">The format to use for both the numerator and the denominator.</param>
        /// <param name="formatProvider">An object that has culture-specific formatting information.</param>
        /// <returns>The string representation of the <see cref="Rational"/>.</returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return Numerator.ToString(format, formatProvider) + (Denominator != 1 ? "/" + Denominator.ToString(format, formatProvider) : string.Empty);
        }

        #endregion

        #region IEquatable<Rational> Members

        public bool Equals(Rational other)
        {
            return this == other;
        }

        #endregion

        #region IConvertible Members

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return !IsZero(this);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return (byte)(_numerator / _denominator);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return (decimal)this;
        }

        public double ToDouble(IFormatProvider provider)
        {
            return (double)this;
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return (short)this;
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return (int)this;
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return (long)this;
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return (sbyte)(_numerator / _denominator);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return (float)this;
        }

        public string ToString(IFormatProvider provider)
        {
            return Numerator.ToString(provider) + (Denominator != 1 ? "/" + Denominator.ToString(provider) : string.Empty);
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType((double)this, conversionType, provider);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return (ushort)this;
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return (uint)this;
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return (ulong)this;
        }

        #endregion
    }
}
