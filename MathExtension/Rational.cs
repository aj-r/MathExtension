using System;
using System.Collections.Generic;
using System.Text;

namespace MathExtension
{
	[Serializable]
	public struct Rational : IComparable, IComparable<Rational>, IFormattable, IEquatable<Rational>
	{
		private readonly int _numerator;
        private readonly int _denominator;

		public static readonly Rational Zero = new Rational(0, 1);

        public static readonly Rational One = new Rational(1, 1);

		public static readonly Rational MinValue = new Rational(int.MinValue, 1);

		public static readonly Rational MaxValue = new Rational(int.MaxValue, 1);

		public static readonly Rational Indeterminate = new Rational(0, 0);

		public static readonly Rational PositiveInfinity = new Rational(1, 0);

		public static readonly Rational NegativeInfinity = new Rational(-1, 0);

		#region Static Methods

		public static Rational Parse(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				throw new FormatException();
			}
			// Start at one, in case the first character is a negative sign.
			int i = 1;

			while (i < s.Length && char.IsDigit(s[i]))
			{
				i++;
			}
			if (i == s.Length)
			{
				// The input string only has an integer part.
				return new Rational(int.Parse(s));
			}

			// Get the first number by parsing from the beginning of the string to i.
			int firstNumber = int.Parse(s.Remove(i));

			// Check for common separators - if found, then the first number is actually
			// the integer part, not the numerator.
			bool hasIntegerPart = false;
			bool hasDash = false;
			while (i < s.Length && (s[i] == ' ' || s[i] == '-'))
			{
				if (s[i] == '-')
				{
					if (hasDash)
						throw new FormatException();	// Cannot have multiple dashes
					hasDash = true;
				}
				hasIntegerPart = true;
				i++;
			}
			if (i == s.Length)
			{
				if (hasDash)
				{
					// A dash was placed at the end of the string.
					throw new FormatException();
				}
				// If there were only spaces after the number, then the input string only has an integer part.
				return new Rational(firstNumber);
			}

			int integerPart;
			int numerator;

			if (hasIntegerPart)
			{
				integerPart = firstNumber;

				int j = i;
				// Now parse the actual numerator
				if (!char.IsDigit(s[j]))
				{
					throw new FormatException();
				}
				// Increment j until we find the end of the number.
				while (j < s.Length && char.IsDigit(s[j]))
				{
					j++;
				}
				if (j == s.Length)
				{
					// We can't be at the end of the string yet - we haven't gotten the denominator yet!
					throw new FormatException();
				}

				// Parse the string between i and j (excluding the character at j)
				numerator = int.Parse(s.Substring(i, j - i));

				// set i equal to j to get ready to parse the denominator.
				i = j;
			}
			else
			{
				// If there is not integer part, then the first number we parsed was the numerator.
				integerPart = 0;
				numerator = firstNumber;
			}

			// Parse the denominator
			if (s[i] != '/')
			{
				throw new FormatException();
			}
			i++;
			if (i == s.Length)
			{
				throw new FormatException();
			}
			// Parse the number from here to the end of the string.
			int denominator = int.Parse(s.Substring(i));

			return new Rational(integerPart * denominator + numerator, denominator);
		}

		public static bool TryParse(string s, out Rational result)
		{
			try
			{
				result = Parse(s);
				return true;
			}
			catch (FormatException)
			{
				result = Indeterminate;
				return false;
			}
		}

		/// <summary>
		/// Converts a floating-point number to a rational number.
		/// </summary>
		/// <param name="value">A floating-point number to convert to a rational number.</param>
		/// <returns>A rational number.</returns>
		public static Rational FromDouble(double value)
		{
			return new Rational(value);
		}

		/// <summary>
		/// Converts a floating-point decimal to a rational number.
		/// </summary>
		/// <param name="value">A floating-point number to convert to a rational number.</param>
		/// <param name="denominatorBase">A base that the denominator must be a power of.</param>
		/// <returns>A rational number.</returns>
		public static Rational FromDouble(double value, int denominatorBase)
		{
			if (denominatorBase <= 1)
			{
				throw new ArgumentException("Denominator base must be greater than 1.", "denominatorBase");
			}

			int denominator = 1;
			while (!MathEx.IsInteger(value))
			{
				value *= (double)denominatorBase;
				denominator *= denominatorBase;
			}

			return new Rational(MathEx.Round(value), denominator);
		}

		/// <summary>
		/// Converts a floating-point decimal to a rational number.
		/// </summary>
		/// <param name="value">A floating-point number to convert to a rational number.</param>
		/// <param name="maxDenominator">The maximum value that the denominator can have.</param>
		/// <returns>A rational number.</returns>
		public static Rational FromDoubleWithMaxDenominator(double value, int maxDenominator)
		{
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
			} while (!MathEx.IsInteger(numerator) && denominator < maxDenominator);

			return new Rational(MathEx.Round(numerator), denominator);
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
            if (b.Numerator == 0 || a.Denominator == 0)
                throw new DivideByZeroException();
            if (b.Denominator == 0)
            {
                remainder = a;
                return 0;
            }
            int xNum = a.Numerator * b.Denominator;
            int yNum = b.Numerator * a.Denominator;
            int denominator = a.Denominator * b.Denominator;
            int rem;
            var result = Math.DivRem(xNum, yNum, out rem);
            remainder = Rational.Simplify(rem, denominator);
            return result;
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
            if (double.IsPositiveInfinity(value))
            {
                _numerator = 1;
                _denominator = 0;
            }
            else if (double.IsNegativeInfinity(value))
            {
                _numerator = -1;
                _denominator = 0;
            }
            else if (double.IsNaN(value))
            {
                _numerator = 0;
                _denominator = 0;
            }
            else
            {
                // TODO: this algorithm has really bad rounding errors. Use a better algorithm
                // if this function will used often.

                const double tolerance = 1e-8;

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
                while (MathEx.Abs(fractionPart) > tolerance && n < 100)
                {
                    value = 1.0 / fractionPart;
                    denominator *= value;
                    fractionPart = value - Math.Truncate(value);
                    n++;
                }

                // Get the actual numerator
                numerator *= denominator;
                _numerator = (int)numerator;
                if (isNegative)
                    _numerator *= -1;
                _denominator = (int)denominator;
            }
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
            return Simplify(Numerator, Denominator);
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

		public override bool Equals(object obj)
		{
			return (obj is Rational) && this == (Rational)obj;
		}

        public bool StrictlyEquals(Rational r)
        {
            return Numerator == r.Numerator && Denominator == r.Denominator;
        }

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
                return x.Denominator == 0 || y.Numerator == 0 ? Indeterminate : x;
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
            if (!(obj is Rational))
                throw new ArgumentException("Can only compare to rationals", "obj");
			return CompareTo((Rational)obj);
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

		public string ToString(string format, IFormatProvider formatProvider)
		{
			return Numerator.ToString(format, formatProvider) + "/" + Denominator.ToString(format, formatProvider);
		}

		#endregion

		#region IEquatable<Rational> Members

		public bool Equals(Rational other)
		{
			return this == other;
		}

		#endregion

	}
}
