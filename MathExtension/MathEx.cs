using System;
using System.Drawing;
using System.Numerics;

namespace MathExtension
{
	public static class MathEx
	{
		public const double SQRT_2 = 1.4142135623730950488016887242097;
		public const double SQRT_3 = 1.7320508075688772935274463415059;
		public const double SQRT_1_2 = 0.70710678118654752440084436210485;

		public static float Abs(float x)
		{
			return Math.Abs(x);
		}

		public static double Abs(double x)
		{
			return Math.Abs(x);
		}

		public static Rational Abs(Rational x)
		{
			return new Rational(Math.Abs(x.Numerator), Math.Abs(x.Denominator));
		}

		public static int Sign(Rational x)
		{
			return Math.Sign(x.Numerator) * Math.Sign(x.Denominator);
		}

		public static Rational Max(Rational a, Rational b)
		{
			return (a > b ? a : b);
		}

		public static Rational Min(Rational a, Rational b)
		{
			return (a < b ? a : b);
		}

		/// <summary>
		/// Gets whether or not a floating-point number is zero, within a certain tolerance.
		/// </summary>
		/// <param name="x">A floating-point number.</param>
		/// <returns>True if the number is zero, false if not.</returns>
        public static bool IsZero(float x, float tolerance = 1e-6f)
		{
			return Math.Abs(x) < tolerance;
		}

		/// <summary>
		/// Gets whether or not a floating-point number is zero, within a certain tolerance.
		/// </summary>
		/// <param name="x">A floating-point number.</param>
		/// <returns>True if the number is zero, false if not.</returns>
		public static bool IsZero(double x, double tolerance = 1e-6)
		{
            return Math.Abs(x) < tolerance;
		}

        /// <summary>
        /// Gets whether or not a complex number is zero, within a certain tolerance.
        /// </summary>
        /// <param name="x">A complex number.</param>
        /// <returns>True if the number is zero, false if not.</returns>
        public static bool IsZero(Complex x, double tolerance = 1e-6)
        {
            return Math.Abs(x.Real) < tolerance && Math.Abs(x.Imaginary) < tolerance;
        }

		/// <summary>
		/// Gets whether or not two floating-point numbers are equal to each other, within a certain tolerance.
		/// </summary>
		/// <param name="x">The first floating-point number.</param>
		/// <param name="x">The second floating-point number.</param>
		/// <returns>True if the numbers are equal, false if not.</returns>
        public static bool AreEqual(float x, float y, double tolerance = 1e-6)
		{
            return Math.Abs(x - y) < tolerance;
		}

		/// <summary>
		/// Gets whether or not two floating-point numbers are equal to each other, within a certain tolerance.
		/// </summary>
		/// <param name="x">The first floating-point number.</param>
		/// <param name="x">The second floating-point number.</param>
		/// <returns>True if the numbers are equal, false if not.</returns>
        public static bool AreEqual(double x, double y, double tolerance = 1e-6)
		{
            return Math.Abs(x - y) < tolerance;
		}

        /// <summary>
        /// Gets whether or not two complex numbers are equal to each other, within a certain tolerance.
        /// </summary>
        /// <param name="x">The first complex number.</param>
        /// <param name="x">The second complex number.</param>
        /// <returns>True if the number is zero, false if not.</returns>
        public static bool AreEqual(Complex x, Complex y, double tolerance = 1e-6)
        {
            return Math.Abs(x.Real - y.Real) < tolerance && Math.Abs(x.Imaginary - y.Imaginary) < tolerance;
        }

		/// <summary>
		/// Determines whether or not a number is an integer (has no fraction part).
		/// </summary>
		/// <param name="x">The number to test.</param>
		/// <returns>True if the number is an integer, false if it has a fraction part.</returns>
        public static bool IsInteger(float x, float tolerance = 1e-6f)
		{
            var fpart = Math.Abs(x % 1.0);
            return fpart < tolerance || fpart > 1 - tolerance;
		}

		/// <summary>
		/// Determines whether or not a number is an integer (has no fraction part).
		/// </summary>
		/// <param name="x">The number to test.</param>
		/// <returns>True if the number is an integer, false if it has a fraction part.</returns>
        public static bool IsInteger(double x, double tolerance = 1e-6)
        {
            var fpart = Math.Abs(x % 1.0);
            return fpart < tolerance || fpart > 1 - tolerance;
		}

        /// <summary>
        /// Determines whether or not a number is an integer (has no fraction part).
        /// </summary>
        /// <param name="x">The number to test.</param>
        /// <returns>True if the number is an integer, false if it has a fraction part.</returns>
        public static bool IsInteger(Complex x, double tolerance = 1e-6)
        {
            return IsZero(x.Imaginary, tolerance) && IsInteger(x.Real, tolerance);
        }

		/// <summary>
		/// Rounds a floating-point number to the nearest integer.
		/// </summary>
		/// <param name="x">A floating-point number.</param>
		/// <returns>The rounded version of the number.</returns>
		public static int Round(float x)
		{
			if (x >= 0.0f)
				return (int)(x + 0.5f);
			else
				return (int)(x - 0.5f);
		}

		/// <summary>
		/// Rounds a floating-point number to the nearest integer.
		/// </summary>
		/// <param name="x">A floating-point number.</param>
		/// <returns>The rounded version of the number.</returns>
        public static int Round(double x)
		{
			if (x >= 0.0)
				return (int)(x + 0.5);
			else
				return (int)(x - 0.5);
		}

		public static float Exp(float x)
		{
			return (float)Math.Exp(x);
		}

		private static readonly double log_2 = Math.Log(2.0);
		private static readonly double log_10 = Math.Log(10.0);

		public static float Log(float x)
		{
			return (float)Math.Log(x);
		}

		public static double Log(double x)
		{
			return Math.Log(x);
		}

		public static double Log(Rational x)
		{
			return Math.Log((double)x.Numerator / (double)x.Denominator);
		}

		public static float Log2(float x)
		{
			return (float)Math.Log(x, 2.0);
		}
		
		public static double Log2(double x)
		{
			return Math.Log(x, 2);
		}

		public static double Log2(Rational x)
		{
			double input = (double)x.Numerator / (double)x.Denominator;
			double retVal = Math.Log(input, 2);
			return retVal;
		}

		public static float Log10(float x)
		{
			return (float)Math.Log10(x);
		}

		public static double Log10(double x)
		{
			return Math.Log10(x);
		}

		public static double Log10(Rational x)
		{
			return Math.Log10((double)x.Numerator / (double)x.Denominator);
		}

		/// <summary>
		/// Returns a specified number raised to a specified power.
		/// </summary>
		/// <param name="x">An integer to be raised to a power.</param>
		/// <param name="y">A positive integer that specifies the power to be raised to.</param>
		/// <returns>A specified number raised to a specified power.</returns>
		public static int Pow(int x, uint y)
		{
			int ret = 1;
			while (y > 0)
			{
				if ((y & 1) == 1)
					ret *= x;
				x *= x;
				y >>= 1;
			}
			return ret;
		}

		/// <summary>
		/// Returns a real number raised to a specified power.
		/// </summary>
		/// <param name="x">A real number to be raised to a power.</param>
		/// <param name="y">A positive integer that specifies the power to be raised to.</param>
		/// <returns>A specified number raised to a specified power.</returns>
		public static float Pow(float x, uint y)
		{
			float ret = 1;
			while (y > 0)
			{
				if ((y & 1) == 1)
					ret *= x;
				x *= x;
				y >>= 1;
			}
			return ret;
		}

		/// <summary>
		/// Returns a real number raised to a specified power.
		/// </summary>
		/// <param name="x">A real number to be raised to a power.</param>
		/// <param name="y">A positive integer that specifies the power to be raised to.</param>
		/// <returns>A specified number raised to a specified power.</returns>
		public static double Pow(double x, uint y)
		{
			double ret = 1;
			while (y > 0)
			{
				if ((y & 1) == 1)
					ret *= x;
				x *= x;
				y >>= 1;
			}
			return ret;
		}

		/// <summary>
		/// Returns a specified number raised to a specified power.
		/// </summary>
		/// <param name="x">A floating-point number to be raised to a power.</param>
		/// <param name="y">A floating-point number that specifies a power.</param>
		/// <returns>A specified number raised to a specified power.</returns>
		public static float Pow(float x, float y)
		{
			return (float)Math.Pow(x, y);
		}

		/// <summary>
		/// Returns a specified number raised to a specified power.
		/// </summary>
		/// <param name="x">A floating-point number to be raised to a power.</param>
		/// <param name="y">A floating-point number that specifies a power.</param>
		/// <returns>A specified number raised to a specified power.</returns>
		public static double Pow(double x, double y)
		{
			return Math.Pow(x, y);
		}

		/// <summary>
		/// Returns the primary cube root of a specified number.
		/// </summary>
		/// <param name="x">A real number.</param>
		/// <returns>The primary square root of a specified number.</returns>
		public static double Cbrt(double x)
		{
			if (x >= 0)
			{
				return Math.Pow(x, 1.0 / 3.0);
			}
			else
			{
				return -Math.Pow(-x, 1.0 / 3.0);
			}
		}

		/// <summary>
		/// Computes the factorial of the given number.
		/// </summary>
		/// <param name="n">The number to compute the factorial of.</param>
		/// <returns>The factorial of n.</returns>
		public static int Factorial(int n)
		{
			if (n < 0)
				throw new ArgumentException("Cannot compute the factorial of a negative number.", "n");

			// Note that 0! = 1
			int result = 1;
			for (int i = 2; i <= n; i++)
				result *= i;
			return result;
		}

		/// <summary>
		/// Computes the number of ways to choose r items from a group of n items, where order matters.
		/// </summary>
		/// <param name="n">The number of items in the set.</param>
		/// <param name="r">The number to choose from the set.</param>
		/// <returns>The number of ways to choose r items from a group of n items, where order matters.</returns>
		public static int nPr(int n, int r)
		{
			if (n < 0)
				throw new ArgumentException("n must be grater than or equal to zero.", "n");
			if (r < 0)
				throw new ArgumentException("r must be grater than or equal to zero.", "r");
			if (r > n)
				throw new ArgumentException("r must be less than or equal to n.", "r");

			// nPr = n!/r!
			int result = 1;
			for (int i = r + 1; i <= n; i++)
				result *= i;
			return result;
		}

		/// <summary>
		/// Computes the number of ways to choose r items from a group of n items, regardless of order.
		/// </summary>
		/// <param name="n">The number of items in the set.</param>
		/// <param name="r">The number to choose from the set.</param>
		/// <returns>The number of ways to choose r items from a group of n items, regardless of order.</returns>
		public static int nCr(int n, int r)
		{
			if (n < 0)
				throw new ArgumentException("n must be grater than or equal to zero.", "n");
			if (r < 0)
				throw new ArgumentException("r must be grater than or equal to zero.", "r");
			if (r > n)
				throw new ArgumentException("r must be less than or equal to n.", "r");

			// nCr = n!/(r!(n-r)!)
			if (r >= n / 2)
			{
				// Since n!/r! = nPr,  nCr = nPr/(n-r)!
				int result = nPr(n, r);
				result /= Factorial(n - r);
				return result;
			}
			else
			{
				// Note that nCr = nC(n-r)
				// If r is very small, it will be faster to calculate nC(n-r)
				return nCr(n, n - r);
			}
		}

		/// <summary>
		/// Gets the greatest common divisor of two numbers.
		/// </summary>
		/// <param name="x">The first number.</param>
		/// <param name="y">The second number.</param>
		/// <returns>The greatest common divisor of the two numbers.</returns>
		public static int Gcd(int x, int y)
		{
			x = Math.Abs(x);
			y = Math.Abs(y);
			while (y != 0)
			{
				int remainder = x % y;
				x = y;
				y = remainder;
			}
			return x;
		}

		/// <summary>
		/// Gets the least common multiple of two numbers.
		/// </summary>
		/// <param name="x">The first number.</param>
		/// <param name="y">The second number.</param>
		/// <returns>The least common multiple of the two numbers.</returns>
		public static int Lcm(int x, int y)
		{
			int gcd = Gcd(x, y);
			return x * y / gcd;
		}

		/// <summary>
		/// Gets the roots of a polynomial expression given its coefficients.
		/// </summary>
		/// <param name="coefficients">
		/// The coefficients in the polynomial, starting with the coefficient for the highest order term
		/// and ending with the constant term. The number of coefficients should be one greater than the
		/// order of the polynomial.</param>
		/// <returns>The root(s) of the polynomial.</returns>
		public static Complex[] GetPolynomialRoots(params Complex[] coefficients)
		{
			if (coefficients == null)
			{
				throw new ArgumentNullException("coefficients");
			}

			int order = coefficients.Length - 1;
			int startIndex = 0;
			int endIndex = order;

			// Ignore leading terms with a coefficient of zero since they don't actually exist.
            while (startIndex <= endIndex && MathEx.IsZero(coefficients[startIndex].Real) && MathEx.IsZero(coefficients[startIndex].Imaginary))
			{
				order--;
				startIndex++;
			}

			//The number of roots will equal the order of the polynomial
			Complex[] roots = new Complex[order];

			// If the last coefficient is zero, then we can factor out an 'x' from each other term
			// to reduce the order of the polynomial (this results in a root of 0).
			// e.g. 3x^4 + x^3 - 2x^2  =  x^2(3x^2 + x - 2)
			while (endIndex < startIndex && MathEx.IsZero(coefficients[endIndex]))
			{
				order--;
				roots[order] = 0;
				endIndex--;
			}

			if (order == 1)
			{
				// Single root: 0 = ax + b
				// x = -b / a
				Complex a = coefficients[startIndex];
				Complex b = coefficients[startIndex + 1];
				roots[0] = -b / a;
			}
			else if (order == 2)
			{
				// Quatratic roots: 0 = ax^2 + bx + c
				// x = (-b+/-sqrt(b^2-4ac) / (2a)
				Complex a = coefficients[startIndex];
				Complex b = coefficients[startIndex + 1];
				Complex c = coefficients[startIndex + 2];
				Complex expr1 = Complex.Sqrt(b * b - 4 * a * c);
				roots[0] = (-b + expr1) / (2 * a);
				roots[1] = (-b - expr1) / (2 * a);
			}
			else if (order == 3)
			{
				// Cubic roots: 0 = ax^3 + bx^2 + cx + d
				// x = 
				Complex a = coefficients[startIndex];
				Complex b = coefficients[startIndex + 1];
				Complex c = coefficients[startIndex + 2];
				Complex d = coefficients[startIndex + 3];
				Complex expr1 = 2 * b * b * b - 9 * a * b * c + 27 * a * a * d;
				Complex expr2 = Complex.Pow(-27 * a * a * (18 * a * b * c * d - 4 * b * b * b * d + b * b * c * c - 4 * a * c * c * c - 27 * a * a * d * d), 0.5);
				Complex expr3 = Complex.Pow((expr1 + expr2) / 2, 1.0 / 3);
				Complex expr4 = Complex.Pow((expr1 - expr2) / 2, 1.0 / 3);
				Complex c1 = new Complex(0.5, Math.Sqrt(3) / 2);
				Complex c2 = Complex.Conjugate(c1);
				roots[0] = (-b - expr3 - expr4) / (3 * a);
				roots[1] = (-b + c1 * expr3 + c2 * expr4) / (3 * a);
				roots[2] = (-b + c2 * expr3 + c1 * expr4) / (3 * a);
			}
			else if (order == 4)
			{
				// 4th order: 0 = ax^4 + bx^3 + cx^2 + dx + f
				// x1 = 
				throw new NotImplementedException();
			}
			else if (order > 4)
			{
				// Order is too high to solve.
				return null;
			}
			return roots;
		}

		public static Rectangle RectangleUnion(Rectangle a, Rectangle b)
		{
			Rectangle newRect = new Rectangle();
			newRect.X = Math.Min(a.Left, b.Left);
			newRect.Y = Math.Min(a.Top, b.Top);
			newRect.Width = Math.Max(a.Right, b.Right) - newRect.X;
			newRect.Height = Math.Max(a.Bottom, b.Bottom) - newRect.Y;
			return newRect;
		}

		public static RectangleF RectangleUnion(RectangleF a, RectangleF b)
		{
			RectangleF newRect = new RectangleF();
			newRect.X = Math.Min(a.Left, b.Left);
			newRect.Y = Math.Min(a.Top, b.Top);
			newRect.Width = Math.Max(a.Right, b.Right) - newRect.X;
			newRect.Height = Math.Max(a.Bottom, b.Bottom) - newRect.Y;
			return newRect;
		}
	}
}
