using System;
using System.Collections.Generic;
using System.Text;

namespace MathExtension
{
    public static class EnumerableExtensions
    {
        public static Rational Sum(this IEnumerable<Rational> source)
        {
            var sum = Rational.Zero;
            foreach (var item in source)
                sum += item;
            return sum;
        }

        public static Rational Sum(this IEnumerable<Rational?> source)
        {
            var sum = Rational.Zero;
            foreach (var item in source)
                sum += item ?? Rational.Zero;
            return sum;
        }

        public static Rational Sum<T>(this IEnumerable<T> source, Func<T, Rational> selector)
        {
            var sum = Rational.Zero;
            foreach (var item in source)
                sum += selector(item);
            return sum;
        }

        public static Rational Sum<T>(this IEnumerable<T> source, Func<T, Rational?> selector)
        {
            var sum = Rational.Zero;
            foreach (var item in source)
                sum += selector(item) ?? Rational.Zero;
            return sum;
        }
    }
}
