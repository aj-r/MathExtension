using System;
using System.Numerics;
using NUnit.Framework;

namespace MathExtension.Test
{
    [TestFixture]
    public class MathExTests
    {
        [TestCase(1, 1, Result = 1)]
        [TestCase(1, 574, Result = 1)]
        [TestCase(38, 1, Result = 38)]
        [TestCase(1, 0, Result = 1)]
        [TestCase(38, 0, Result = 1)]
        [TestCase(2, 2, Result = 4)]
        [TestCase(2, 3, Result = 8)]
        [TestCase(2, 4, Result = 16)]
        [TestCase(2, 5, Result = 32)]
        [TestCase(3, 5, Result = 243)]
        [TestCase(3, 6, Result = 729)]
        [TestCase(3, 7, Result = 2187)]
        [TestCase(3, 8, Result = 6561)]
        [TestCase(3, 9, Result = 19683)]
        [TestCase(45, 2, Result = 2025)]
        [TestCase(45, 3, Result = 91125)]
        [TestCase(45, 7, Result = 373669453125)]
        public long PowerTest(long x, int y)
        {
            return MathEx.Pow(x, (uint)y);
        }

        static TestCaseData[] sqrtTestCases =
        {
            new TestCaseData((BigInteger)0).Returns((BigInteger)0),
            new TestCaseData((BigInteger)1).Returns((BigInteger)1),
            new TestCaseData((BigInteger)2).Returns((BigInteger)1),
            new TestCaseData((BigInteger)3).Returns((BigInteger)1),
            new TestCaseData((BigInteger)4).Returns((BigInteger)2),
            new TestCaseData((BigInteger)9).Returns((BigInteger)3),
            new TestCaseData((BigInteger)2209).Returns((BigInteger)47),
            new TestCaseData((BigInteger)2209).Returns((BigInteger)47),
            new TestCaseData(BigInteger.Parse("23974000248716731684")).Returns((BigInteger)4896325178),
            new TestCaseData((BigInteger)(-1)).Throws(typeof(ArithmeticException)),
        };

        [TestCaseSource("sqrtTestCases")]
        public BigInteger SqrtTest(BigInteger x)
        {
            return MathEx.Sqrt(x);
        }
    }
}
