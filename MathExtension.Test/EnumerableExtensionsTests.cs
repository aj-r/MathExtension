using NUnit.Framework;
using System;
using System.Collections.Generic;
using Q = MathExtension.Rational;

namespace MathExtension.Test
{
    [TestFixture]
    public class EnumerableExtensionsTests
    {
        #region Min

        public static TestCaseData[] minTestCases =
        {
            new TestCaseData((object)new [] { (Q)1 / 2 }).Returns((Q)1 / 2),
            new TestCaseData((object)new [] { (Q)1 / 2, (Q)2 / 5 }).Returns((Q)2 / 5),
            new TestCaseData((object)new [] { (Q)1 / 2, (Q)3 / 5 }).Returns((Q)1 / 2),
            new TestCaseData((object)new [] { (Q)1 / 2, -(Q)1 / 2, (Q)0 }).Returns(-(Q)1 / 2),
        };

        [TestCaseSource("minTestCases")]
        public Q MinTests(IEnumerable<Q> source)
        {
            return source.Min();
        }

        public void MinTests_ThrowIfEmpty()
        {
            Assert.Throws<InvalidOperationException>(() => new Q[0].Min());
        }

        static TestCaseData[] minNullableTestCases =
        {
            new TestCaseData((object)new Q?[0]).Returns(null),
            new TestCaseData((object)new Q?[] { (Q)1 / 2, (Q)2 / 5 }).Returns((Q)2 / 5),
            new TestCaseData((object)new Q?[] { (Q)1 / 2, (Q)3 / 5 }).Returns((Q)1 / 2),
            new TestCaseData((object)new Q?[] { (Q)1 / 2, -(Q)1 / 2, (Q)0 }).Returns(-(Q)1 / 2),
            new TestCaseData((object)new Q?[] { (Q)1 / 2, null, -(Q)1 / 2 }).Returns(-(Q)1 / 2),
            new TestCaseData((object)new Q?[] { null }).Returns(null),
        };

        [TestCaseSource("minNullableTestCases")]
        public Q? MinNullableTests(IEnumerable<Q?> source)
        {
            return source.Min();
        }

        static TestCaseData[] minReferenceTestCases =
        {
            new TestCaseData((object)new TestReferenceType[0]).Returns(null),
            new TestCaseData((object)new TestReferenceType[] { new TestReferenceType(1), new TestReferenceType(2) }).Returns(new TestReferenceType(1)),
            new TestCaseData((object)new TestReferenceType[] { null, new TestReferenceType(2) }).Returns(new TestReferenceType(2)),
            new TestCaseData((object)new TestReferenceType[] { null }).Returns(null),
        };

        [TestCaseSource("minReferenceTestCases")]
        public TestReferenceType MinReferenceTests(IEnumerable<TestReferenceType> source)
        {
            return source.Min();
        }

        static TestCaseData[] minSelectorTestCases =
        {
            new TestCaseData((object)new Tuple<Q>[] { Tuple.Create((Q)1 / 2) }).Returns((Q)1 / 2),
            new TestCaseData((object)new Tuple<Q>[] { Tuple.Create((Q)1 / 2), Tuple.Create(-(Q)1 / 2), Tuple.Create((Q)0) }).Returns(-(Q)1 / 2),
        };

        [TestCaseSource("minSelectorTestCases")]
        public Q MinSelectorTests(IEnumerable<Tuple<Q>> source)
        {
            return source.Min(t => t.Item1);
        }

        public void MinSelectorTests_ThrowIfEmpty()
        {
            Assert.Throws<InvalidOperationException>(() => new Tuple<Q>[0].Min(t => t.Item1));
        }

        #endregion

        #region Max

        public static TestCaseData[] maxTestCases =
        {
            new TestCaseData((object)new [] { (Q)1 / 2 }).Returns((Q)1 / 2),
            new TestCaseData((object)new [] { (Q)1 / 2, (Q)2 / 5 }).Returns((Q)1 / 2),
            new TestCaseData((object)new [] { (Q)1 / 2, (Q)3 / 5 }).Returns((Q)3 / 5),
            new TestCaseData((object)new [] { (Q)1 / 2, -(Q)1 / 2, (Q)0 }).Returns((Q)1 / 2),
        };

        [TestCaseSource("maxTestCases")]
        public Q MaxTests(IEnumerable<Q> source)
        {
            return source.Max();
        }

        public void MaxTests_ThrowIfEmpty()
        {
            Assert.Throws<InvalidOperationException>(() => new Q[0].Max());
        }

        static TestCaseData[] maxNullableTestCases =
        {
            new TestCaseData((object)new Q?[0]).Returns(null),
            new TestCaseData((object)new Q?[] { (Q)1 / 2, (Q)2 / 5 }).Returns((Q)1 / 2),
            new TestCaseData((object)new Q?[] { (Q)1 / 2, (Q)3 / 5 }).Returns((Q)3 / 5),
            new TestCaseData((object)new Q?[] { (Q)1 / 2, -(Q)1 / 2, (Q)0 }).Returns((Q)1 / 2),
            new TestCaseData((object)new Q?[] { (Q)1 / 2, null, -(Q)1 / 2 }).Returns((Q)1 / 2),
            new TestCaseData((object)new Q?[] { null }).Returns(null),
        };

        [TestCaseSource("maxNullableTestCases")]
        public Q? MaxNullableTests(IEnumerable<Q?> source)
        {
            return source.Max();
        }

        static TestCaseData[] maxReferenceTestCases =
        {
            new TestCaseData((object)new TestReferenceType[0]).Returns(null),
            new TestCaseData((object)new TestReferenceType[] { new TestReferenceType(1), new TestReferenceType(2) }).Returns(new TestReferenceType(2)),
            new TestCaseData((object)new TestReferenceType[] { null, new TestReferenceType(2) }).Returns(new TestReferenceType(2)),
            new TestCaseData((object)new TestReferenceType[] { null }).Returns(null),
        };

        [TestCaseSource("maxReferenceTestCases")]
        public TestReferenceType MaxReferenceTests(IEnumerable<TestReferenceType> source)
        {
            return source.Max();
        }

        static TestCaseData[] maxSelectorTestCases =
        {
            new TestCaseData((object)new Tuple<Q>[] { Tuple.Create((Q)1 / 2) }).Returns((Q)1 / 2),
            new TestCaseData((object)new Tuple<Q>[] { Tuple.Create((Q)1 / 2), Tuple.Create(-(Q)1 / 2), Tuple.Create((Q)0) }).Returns((Q)1 / 2),
        };

        [TestCaseSource("maxSelectorTestCases")]
        public Q MaxSelectorTests(IEnumerable<Tuple<Q>> source)
        {
            return source.Max(t => t.Item1);
        }

        public void MaxSelectorTests_ThrowIfEmpty()
        {
            Assert.Throws<InvalidOperationException>(() => new Tuple<Q>[0].Max(t => t.Item1));
        }

        #endregion

        public class TestReferenceType : IComparable<TestReferenceType>
        {
            private readonly int value;

            public TestReferenceType(int value)
            {
                this.value = value;
            }

            public int CompareTo(TestReferenceType other)
            {
                return value.CompareTo(other.value);
            }

            public override bool Equals(object obj)
            {
                var trt = obj as TestReferenceType;
                if (trt == null)
                    return false;
                return value.Equals(trt.value);
            }

            public override int GetHashCode()
            {
                return value.GetHashCode();
            }
        }
    }
}
