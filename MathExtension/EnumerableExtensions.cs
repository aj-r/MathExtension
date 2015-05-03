using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathExtension
{
    public static class EnumerableExtensions
    {
        #region General Extensions

        /// <summary>
        /// Returns the minimum value in a list of values.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in source.</typeparam>
        /// <param name="source">A sequence of <see cref="Decimal"/> values to get the minimum value of.</param>
        /// <returns>The minimum value.</returns>
        public static TSource Min<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            return default(TSource) == null ? NullableGenericMin(source) : NonNullableGenericMin(source);
        }

        /// <summary>
        /// Returns the minimum value in a list of values.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in source.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="source">A sequence of values to get the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value.</returns>
        public static TResult Min<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            return source.Select(selector).Min();
        }

        /// <summary> 
        /// Implements the generic behaviour for non-nullable value types. 
        /// </summary> 
        /// <remarks> 
        /// Empty sequences will cause an InvalidOperationException to be thrown. 
        /// Note that there's no *compile-time* validation in the caller that the type 
        /// is a non-nullable value type, hence the lack of a constraint on T. 
        /// </remarks> 
        private static T NonNullableGenericMin<T>(IEnumerable<T> source)
        {
            Comparer<T> comparer = Comparer<T>.Default;

            using (IEnumerator<T> iterator = source.GetEnumerator())
            {
                if (!iterator.MoveNext())
                    throw new InvalidOperationException("Sequence was empty");

                T min = iterator.Current;
                while (iterator.MoveNext())
                {
                    T item = iterator.Current;
                    if (comparer.Compare(min, item) > 0)
                        min = item;
                }
                return min;
            }
        }

        /// <summary> 
        /// Implements the generic behaviour for nullable types - both reference types and nullable value types. 
        /// </summary> 
        /// <remarks> 
        /// Empty sequences and sequences comprising only of null values will cause the null value 
        /// to be returned. Any sequence containing non-null values will return a non-null value. 
        /// </remarks> 
        private static T NullableGenericMin<T>(IEnumerable<T> source)
        {
            Comparer<T> comparer = Comparer<T>.Default;

            T min = default(T);
            foreach (T item in source)
                if (item != null && (min == null || comparer.Compare(min, item) > 0))
                    min = item;
            return min;
        }

        /// <summary>
        /// Returns the maximum value in a list of values.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in source.</typeparam>
        /// <param name="source">A sequence of <see cref="Decimal"/> values to get the maximum value of.</param>
        /// <returns>The maximum value.</returns>
        public static TSource Max<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            return default(TSource) == null ? NullableGenericMax(source) : NonNullableGenericMax(source);
        }

        /// <summary>
        /// Returns the maximum value in a list of values.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in source.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="source">A sequence of <see cref="Decimal"/> values to get the maximum value of.</param>
        /// <returns>The maximum value.</returns>
        public static TResult Max<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            return source.Select(selector).Max();
        }

        /// <summary> 
        /// Implements the generic behaviour for non-nullable value types. 
        /// </summary> 
        /// <remarks> 
        /// Empty sequences will cause an InvalidOperationException to be thrown. 
        /// Note that there's no *compile-time* validation in the caller that the type 
        /// is a non-nullable value type, hence the lack of a constraint on T. 
        /// </remarks> 
        private static T NonNullableGenericMax<T>(IEnumerable<T> source)
        {
            Comparer<T> comparer = Comparer<T>.Default;

            using (IEnumerator<T> iterator = source.GetEnumerator())
            {
                if (!iterator.MoveNext())
                    throw new InvalidOperationException("Sequence was empty");
                T max = iterator.Current;
                while (iterator.MoveNext())
                {
                    T item = iterator.Current;
                    if (comparer.Compare(max, item) < 0)
                        max = item;
                }
                return max;
            }
        }
        
        /// <summary> 
        /// Implements the generic behaviour for nullable types - both reference types and nullable value types. 
        /// </summary> 
        /// <remarks> 
        /// Empty sequences and sequences comprising only of null values will cause the null value 
        /// to be returned. Any sequence containing non-null values will return a non-null value. 
        /// </remarks> 
        private static T NullableGenericMax<T>(IEnumerable<T> source)
        {
            Comparer<T> comparer = Comparer<T>.Default;

            T max = default(T);
            foreach (T item in source)
                if (item != null && (max == null || comparer.Compare(max, item) < 0))
                    max = item;
            return max;
        }

        #endregion

        #region Rational Extentions

        /// <summary>
        /// Computes the sum of a list of <see cref="Rational"/> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="Rational"/> values to compute the sum of.</param>
        /// <returns>The sum of the values.</returns>
        public static Rational Sum(this IEnumerable<Rational> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            var sum = Rational.Zero;
            foreach (var item in source)
                sum += item;
            return sum;
        }

        /// <summary>
        /// Computes the sum of a list of <see cref="Rational"/> values.
        /// </summary>
        /// <param name="source">A sequence of nullable <see cref="Rational"/> values to compute the sum of.</param>
        /// <returns>The sum of the values.</returns>
        public static Rational Sum(this IEnumerable<Rational?> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            var sum = Rational.Zero;
            foreach (var item in source)
                sum += item ?? Rational.Zero;
            return sum;
        }

        /// <summary>
        /// Compues the sum of the selected values in a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in source.</typeparam>
        /// <param name="source">A sequence of elements to compute the sum of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The sum of the values.</returns>
        public static Rational Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, Rational> selector)
        {
            return source.Select(selector).Sum();
        }

        /// <summary>
        /// Compues the sum of the selected values in a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in source.</typeparam>
        /// <param name="source">A sequence of elements to compute the sum of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The sum of the values.</returns>
        public static Rational Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, Rational?> selector)
        {
            return source.Select(selector).Sum();
        }

        #endregion
    }
}
