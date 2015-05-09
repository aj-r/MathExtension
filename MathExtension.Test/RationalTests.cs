using System;
using NUnit.Framework;

namespace MathExtension.Test
{
    [TestFixture]
    public class RationalTests
    {
        static TestCaseData[] equalsTestCases =
        {
            new TestCaseData(new Rational(1, 3), new Rational(1, 3)).Returns(true),
            new TestCaseData(new Rational(1, 3), new Rational(2, 6)).Returns(true),
            new TestCaseData(new Rational(1, 3), new Rational(-1, 3)).Returns(false),
            new TestCaseData(new Rational(-1, 3), new Rational(2, -6)).Returns(true),
            new TestCaseData(new Rational(-1, 3), new Rational(0, 0)).Returns(false),
            new TestCaseData(new Rational(0, 0), new Rational(0, 0)).Returns(true),
            new TestCaseData(new Rational(5, 0), new Rational(1, 0)).Returns(true),
            new TestCaseData(new Rational(5, 11), new Rational(1, 0)).Returns(false),
            new TestCaseData(new Rational(-5, 0), new Rational(1, 0)).Returns(false),
            new TestCaseData(new Rational(0, 4), new Rational(0, -9)).Returns(true),
        };

        [TestCaseSource("equalsTestCases")]
        public bool EqualsTest(Rational a, Rational b)
        {
            return a.Equals(b);
        }

        static TestCaseData[] strictlyEqualsTestCases =
        {
            new TestCaseData(new Rational(1, 3), new Rational(1, 3)).Returns(true),
            new TestCaseData(new Rational(1, 3), new Rational(2, 6)).Returns(false),
            new TestCaseData(new Rational(1, 3), new Rational(-1, 3)).Returns(false),
            new TestCaseData(new Rational(-1, 3), new Rational(2, -6)).Returns(false),
            new TestCaseData(new Rational(-1, 3), new Rational(0, 0)).Returns(false),
            new TestCaseData(new Rational(0, 0), new Rational(0, 0)).Returns(true),
            new TestCaseData(new Rational(5, 0), new Rational(1, 0)).Returns(false),
            new TestCaseData(new Rational(5, 11), new Rational(1, 0)).Returns(false),
            new TestCaseData(new Rational(-5, 0), new Rational(1, 0)).Returns(false),
            new TestCaseData(new Rational(0, 4), new Rational(0, -9)).Returns(false),
        };

        [TestCaseSource("strictlyEqualsTestCases")]
        public bool StrictlyEqualsTest(Rational a, Rational b)
        {
            return a.StrictlyEquals(b);
        }

        [Test]
        public void GetHashCodeTest()
        {
            // Equivalent forms of the same Rational should have the same hash code.
            var h1 = new Rational(3, 7).GetHashCode();
            var h2 = new Rational(9, 21).GetHashCode();
            Assert.That(h1, Is.EqualTo(h2));
        }

        static TestCaseData[] compareToTestCases =
        {
            new TestCaseData(new Rational(1, 3), new Rational(1, 3)).Returns(0),
            new TestCaseData(new Rational(1, 3), new Rational(2, 6)).Returns(0),
            new TestCaseData(new Rational(1, 3), new Rational(-1, 3)).Returns(1),
            new TestCaseData(new Rational(-1, 3), new Rational(2, -6)).Returns(0),
            new TestCaseData(new Rational(-1, 3), new Rational(0, 0)).Returns(1),
            new TestCaseData(new Rational(0, 0), new Rational(0, 0)).Returns(0),
            new TestCaseData(new Rational(5, 0), new Rational(1, 0)).Returns(0),
            new TestCaseData(new Rational(5, 11), new Rational(1, 0)).Returns(-1),
            new TestCaseData(new Rational(-1, 0), new Rational(5, 11)).Returns(-1),
            new TestCaseData(new Rational(-5, 0), new Rational(1, 0)).Returns(-1),
            new TestCaseData(new Rational(0, 4), new Rational(0, -9)).Returns(0),
            new TestCaseData(Rational.MaxValue, new Rational(1, int.MaxValue)).Returns(1),
        };

        [TestCaseSource("compareToTestCases")]
        public int CompareToTest(Rational a, Rational b)
        {
            return a.CompareTo(b);
        }

        static TestCaseData[] addTestCases =
        {
            new TestCaseData(new Rational(2, 5), new Rational(4, 5)).Returns(new Rational(6, 5)),
            new TestCaseData(new Rational(2, 5), new Rational(3, 5)).Returns(new Rational(1, 1)),
            new TestCaseData(new Rational(2, 5), new Rational(0, 6)).Returns(new Rational(2, 5)),
            new TestCaseData(new Rational(2, 5), new Rational(4, 7)).Returns(new Rational(34, 35)),
            new TestCaseData(new Rational(1, 4), new Rational(3, 10)).Returns(new Rational(11, 20)),
            new TestCaseData(new Rational(1, 6), new Rational(0, 0)).Returns(new Rational(0, 0)),
            new TestCaseData(new Rational(1, 6), new Rational(1, 0)).Returns(new Rational(1, 0)),
            new TestCaseData(new Rational(1, 6), new Rational(-2, 0)).Returns(new Rational(-1, 0)),
            new TestCaseData(new Rational(0, 0), new Rational(1, 0)).Returns(new Rational(0, 0)),
            new TestCaseData(new Rational(-2, 0), new Rational(1, 0)).Returns(new Rational(0, 0)),
        };

        [TestCaseSource("addTestCases")]
        public Rational AddTest(Rational a, Rational b)
        {
            return a + b;
        }

        static TestCaseData[] subtractTestCases =
        {
            new TestCaseData(new Rational(2, 5), new Rational(4, 5)).Returns(new Rational(-2, 5)),
            new TestCaseData(new Rational(2, 5), new Rational(2, 5)).Returns(new Rational(0, 1)),
            new TestCaseData(new Rational(2, 5), new Rational(0, 6)).Returns(new Rational(2, 5)),
            new TestCaseData(new Rational(2, 5), new Rational(4, 7)).Returns(new Rational(-6, 35)),
            new TestCaseData(new Rational(1, 4), new Rational(3, 10)).Returns(new Rational(-1, 20)),
            new TestCaseData(new Rational(1, 6), new Rational(0, 0)).Returns(new Rational(0, 0)),
            new TestCaseData(new Rational(1, 6), new Rational(1, 0)).Returns(new Rational(-1, 0)),
            new TestCaseData(new Rational(1, 6), new Rational(-2, 0)).Returns(new Rational(1, 0)),
            new TestCaseData(new Rational(0, 0), new Rational(1, 0)).Returns(new Rational(0, 0)),
            new TestCaseData(new Rational(-2, 0), new Rational(-1, 0)).Returns(new Rational(0, 0)),
        };

        [TestCaseSource("subtractTestCases")]
        public Rational SubtractTest(Rational a, Rational b)
        {
            return a - b;
        }

        [Test]
        public void MultiplyTest()
        {
            var r = new Rational(2, 5) * new Rational(4, 7);
            Assert.AreEqual(new Rational(8, 35), r);

            r = new Rational(1, -4) * new Rational(3, 10);
            Assert.AreEqual(new Rational(-3, 40), r);

            r = new Rational(3, -4) * new Rational(3, -4);
            Assert.AreEqual(new Rational(9, 16), r);
        }

        [Test]
        public void MultiplyTest_Indeterminate()
        {
            var r = new Rational(1, 6) * new Rational(0, 0);
            Assert.AreEqual(new Rational(0, 0), r);
        }

        [Test]
        public void DivideTest()
        {
            var r = new Rational(2, 5) / new Rational(4, 7);
            Assert.AreEqual(new Rational(7, 10), r);

            r = new Rational(3, 4) / new Rational(1, 4);
            Assert.AreEqual(new Rational(3, 1), r);
        }

        [Test]
        public void DivideTest_Indeterminate()
        {
            var r = new Rational(1, 6) / new Rational(0, 0);
            Assert.AreEqual(new Rational(0, 0), r);
        }

        static TestCaseData[] moduloTestCases =
        {
            new TestCaseData(new Rational(3, 10), new Rational(1, 5)).Returns(new Rational(1, 10)),
            new TestCaseData(new Rational(1, 7), new Rational(1, 4)).Returns(new Rational(1, 7)),
            new TestCaseData(new Rational(4, 7), new Rational(1, 3)).Returns(new Rational(5, 21)),
            new TestCaseData(new Rational(3, 10), new Rational(1, 0)).Returns(new Rational(3, 10)),
            new TestCaseData(new Rational(3, 10), new Rational(0, 1)).Returns(new Rational(0, 1)),
            new TestCaseData(new Rational(1, 6), new Rational(0, 0)).Returns(new Rational(0, 0)),
            new TestCaseData(new Rational(-5, 6), new Rational(1, 3)).Returns(new Rational(-1, 6)),
            new TestCaseData(new Rational(int.MaxValue, 1), new Rational(3, 4)).Returns(new Rational(1, 4)),
        };

        [TestCaseSource("moduloTestCases")]
        public Rational ModuloTest(Rational a, Rational b)
        {
            return a % b;
        }

        static TestCaseData[] divRemTestCases =
        {
            new TestCaseData(new Rational(1, 2), new Rational(1, 5), 2, new Rational(1, 10)),
            new TestCaseData(new Rational(1, 2), new Rational(1, 6), 3, new Rational(0, 1)),
            new TestCaseData(new Rational(1, 7), new Rational(1, 4), 0, new Rational(1, 7)),
            new TestCaseData(new Rational(1, 2), new Rational(1, 0), 0, new Rational(1, 2)),
            new TestCaseData(new Rational(1, 0), new Rational(1, 5), 0, new Rational()).Throws(typeof(DivideByZeroException)),
            new TestCaseData(new Rational(1, 2), new Rational(0, 5), 0, new Rational()).Throws(typeof(DivideByZeroException)),
            new TestCaseData(new Rational(0, 2), new Rational(1, 5), 0, new Rational(0, 1)),
            new TestCaseData(new Rational(1, 2), new Rational(0, 0), 0, new Rational()).Throws(typeof(DivideByZeroException)),
            new TestCaseData(new Rational(-3, 2), new Rational(2, 3), -2, new Rational(-1, 6)),
        };

        [TestCaseSource("divRemTestCases")]
        public void DivRemTest(Rational a, Rational b, int expectedDividend, Rational expectedRemainder)
        {
            Rational actualRemainder;
            var actualDividend = Rational.DivRem(a, b, out actualRemainder);
            Assert.That(actualDividend, Is.EqualTo(expectedDividend));
            Assert.That(actualRemainder, Is.EqualTo(expectedRemainder));
        }

        static TestCaseData[] powerTestCases =
        {
            new TestCaseData(new Rational(3, 8), 0).Returns(new Rational(1, 1)),
            new TestCaseData(new Rational(3, 8), 1).Returns(new Rational(3, 8)),
            new TestCaseData(new Rational(3, 8), 3).Returns(new Rational(27, 512)),
            new TestCaseData(new Rational(2, 3), 10).Returns(new Rational(1024, 59049)),
            new TestCaseData(new Rational(3, 8), 0).Returns(new Rational(1, 1)),
            new TestCaseData(new Rational(4, 7), -1).Returns(new Rational(7, 4)),
            new TestCaseData(new Rational(4, 7), -2).Returns(new Rational(49, 16)),
            new TestCaseData(new Rational(-4, 7), 2).Returns(new Rational(16, 49)),
            new TestCaseData(new Rational(-4, 7), 3).Returns(new Rational(-64, 343)),
            new TestCaseData(new Rational(-4, 7), -3).Returns(new Rational(-343, 64)),
        };

        [TestCaseSource("powerTestCases")]
        public Rational PowerTest(Rational baseValue, int exponent)
        {
            return Rational.Pow(baseValue, exponent);
        }

        static TestCaseData[] simplifyTestCases =
        {
            new TestCaseData(new Rational(4, 7), 4, 7),
            new TestCaseData(new Rational(64, 4), 16, 1),
            new TestCaseData(new Rational(45, 18), 5, 2),
            new TestCaseData(new Rational(-10, -5), 2, 1),
            new TestCaseData(new Rational(10, -5), -2, 1),
            new TestCaseData(new Rational(0, 0), 0, 0),
            new TestCaseData(new Rational(7, 0), 1, 0),
            new TestCaseData(new Rational(-7, 0), -1, 0),
            new TestCaseData(new Rational(0, -7), 0, 1),
        };

        [TestCaseSource("simplifyTestCases")]
        public void SimplifyTest(Rational r, int expectedNumerator, int expectedDenominator)
        {
            var actual = r.Simplify();
            Assert.AreEqual(expectedNumerator, actual.Numerator);
            Assert.AreEqual(expectedDenominator, actual.Denominator);
        }

        static TestCaseData[] fromDoubleTestCases =
        {
            new TestCaseData(1.0 / 3.0).Returns(new Rational(1, 3)),
            new TestCaseData(2.0).Returns(new Rational(2, 1)),
            new TestCaseData(1.0 / 7.0).Returns(new Rational(1, 7)),
            new TestCaseData(3.0 / 14.0).Returns(new Rational(3, 14)),
            new TestCaseData(47.0 / 49.0).Returns(new Rational(47, 49)),
            new TestCaseData(1394687.0 / 27849.0).Returns(new Rational(1394687, 27849)),
            new TestCaseData(-1.0 / 5.0).Returns(new Rational(-1, 5)),
            new TestCaseData(double.PositiveInfinity).Returns(new Rational(1, 0)),
            new TestCaseData(double.NegativeInfinity).Returns(new Rational(-1, 0)),
            new TestCaseData(double.NaN).Returns(new Rational(0, 0)),
        };

        [TestCaseSource("fromDoubleTestCases")]
        public Rational FromDoubleTest(double value)
        {
            return Rational.FromDouble(value);
        }

        static TestCaseData[] parseTestCases =
        {
            new TestCaseData("1/17").Returns(new Rational(1, 17)),
            new TestCaseData(" 15/2 ").Returns(new Rational(15, 2)),
            new TestCaseData("7-1/2").Returns(new Rational(15, 2)),
            new TestCaseData("7 1/2").Returns(new Rational(15, 2)),
            new TestCaseData("1 / 2").Returns(new Rational(1, 2)),
            new TestCaseData("5 - 3 / 4").Returns(new Rational(23, 4)),
            new TestCaseData("-9/11").Returns(new Rational(-9, 11)),
            new TestCaseData("-3 9/11").Returns(new Rational(-42, 11)),
            new TestCaseData("0.6").Returns(new Rational(3, 5)),
            new TestCaseData("-0.6").Returns(new Rational(-3, 5)),
            new TestCaseData("1/").Throws(typeof(FormatException)),
            new TestCaseData(" ").Throws(typeof(FormatException)),
            new TestCaseData("1b").Throws(typeof(FormatException)),
            new TestCaseData("1/2/3").Throws(typeof(FormatException)),
        };

        [TestCaseSource("parseTestCases")]
        public Rational ParseTest(string s)
        {
            return Rational.Parse(s);
        }

        static TestCaseData[] tryParseTestCases =
        {
            new TestCaseData("1/17", new Rational(1, 17)).Returns(true),
            new TestCaseData(" 15/2 ", new Rational(15, 2)).Returns(true),
            new TestCaseData("7-1/2", new Rational(15, 2)).Returns(true),
            new TestCaseData("7 1/2", new Rational(15, 2)).Returns(true),
            new TestCaseData("1 / 2", new Rational(1, 2)).Returns(true),
            new TestCaseData("5 - 3 / 4", new Rational(23, 4)).Returns(true),
            new TestCaseData("-9/11", new Rational(-9, 11)).Returns(true),
            new TestCaseData("-3 9/11", new Rational(-42, 11)).Returns(true),
            new TestCaseData("0.6", new Rational(3, 5)).Returns(true),
            new TestCaseData("-0.6", new Rational(-3, 5)).Returns(true),
            new TestCaseData("1/", Rational.Indeterminate).Returns(false),
            new TestCaseData(" ", Rational.Indeterminate).Returns(false),
            new TestCaseData("1b", Rational.Indeterminate).Returns(false),
            new TestCaseData("1/2/3", Rational.Indeterminate).Returns(false),
        };

        [TestCaseSource("tryParseTestCases")]
        public bool TryParseTest(string s, Rational expected)
        {
            Rational r;
            var result = Rational.TryParse(s, out r);
            Assert.That(r, Is.EqualTo(expected));
            return result;
        }
    }
}
