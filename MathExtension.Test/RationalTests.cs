using System;
using NUnit.Framework;

namespace MathExtension.Test
{
    [TestFixture]
    public class RationalTests
    {
        [Test]
        public void EqualTest()
        {
            Assert.IsTrue(new Rational(1, 3) == new Rational(1, 3));
            Assert.IsTrue(new Rational(1, 3) == new Rational(2, 6));
        }

        [Test]
        public void AddTest()
        {
            var r = new Rational(2, 5) + new Rational(4, 5);
            Assert.AreEqual(new Rational(6, 5), r);

            r = new Rational(2, 5) + new Rational(0, 6);
            Assert.AreEqual(new Rational(2, 5), r);
        }

        [Test]
        public void AddTestIndeterminate()
        {
            var r = new Rational(1, 6) + new Rational(0, 0);
            Assert.AreEqual(new Rational(0, 0), r);
        }

        [Test]
        public void AddTestDifferentBase()
        {
            var r = new Rational(2, 5) + new Rational(4, 7);
            Assert.AreEqual(new Rational(34, 35), r);

            r = new Rational(1, 4) + new Rational(3, 10);
            Assert.AreEqual(new Rational(11, 20), r);
        }

        [Test]
        public void SubtractTest()
        {
            var r = new Rational(2, 5) - new Rational(4, 5);
            Assert.AreEqual(new Rational(-2, 5), r);
        }

        [Test]
        public void SubtractTestDifferentBase()
        {
            var r = new Rational(2, 5) - new Rational(4, 7);
            Assert.AreEqual(new Rational(-6, 35), r);

            r = new Rational(1, 4) - new Rational(3, 10);
            Assert.AreEqual(new Rational(-1, 20), r);
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
        public void MultiplyTestIndeterminate()
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
        public void DivideTestIndeterminate()
        {
            var r = new Rational(1, 6) / new Rational(0, 0);
            Assert.AreEqual(new Rational(0, 0), r);
        }

        [Test]
        public void ModuloTestA()
        {
            var r = new Rational(3, 10) % new Rational(1, 5);
            Assert.AreEqual(new Rational(1, 10), r);
        }

        [Test]
        public void ModuloTestB()
        {
            var r = new Rational(1, 7) % new Rational(1, 4);
            Assert.AreEqual(new Rational(1, 7), r);
        }

        [Test]
        public void ModuloTestC()
        {
            var r = new Rational(4, 7) % new Rational(1, 3);
            Assert.AreEqual(new Rational(5, 21), r);
        }

        [Test]
        public void ModuloTestInfinity()
        {
            var r = new Rational(3, 10) % new Rational(1, 0);
            Assert.AreEqual(new Rational(3, 10), r);
        }

        [Test]
        public void ModuloTestZero()
        {
            var r = new Rational(3, 10) % new Rational(0, 1);
            Assert.AreEqual(new Rational(0, 1), r);
        }

        [Test]
        public void ModuloTestIndeterminate()
        {
            var r = new Rational(1, 6) % new Rational(0, 0);
            Assert.AreEqual(new Rational(0, 0), r);
        }

        [Test]
        public void ModuloTestNegative()
        {
            var r = new Rational(-5, 6) % new Rational(1, 3);
            Assert.AreEqual(new Rational(-1, 6), r);
        }

        [Test]
        public void DivRemTestA()
        {
            Rational r;
            var result = Rational.DivRem(new Rational(1, 2), new Rational(1, 5), out r);
            Assert.AreEqual(2, result);
            Assert.AreEqual(new Rational(1, 10), r);
        }

        [Test]
        public void DivRemTestB()
        {
            Rational r;
            var result = Rational.DivRem(new Rational(1, 7), new Rational(1, 4), out r);
            Assert.AreEqual(0, result);
            Assert.AreEqual(new Rational(1, 7), r);
        }

        [Test]
        public void DivRemTestInfinityDivisor()
        {
            Rational r;
            var result = Rational.DivRem(new Rational(1, 2), new Rational(1, 0), out r);
            Assert.AreEqual(0, result);
            Assert.AreEqual(new Rational(1, 2), r);
        }
        
        [Test]
        [ExpectedException(typeof(DivideByZeroException))]
        public void DivRemTestInfinityDividend()
        {
            Rational r;
            var result = Rational.DivRem(new Rational(1, 0), new Rational(1, 5), out r);
        }

        [Test]
        [ExpectedException(typeof(DivideByZeroException))]
        public void DivRemTestZeroDivisor()
        {
            Rational r;
            var result = Rational.DivRem(new Rational(1, 2), new Rational(0, 5), out r);
        }

        [Test]
        public void DivRemTestZeroDividend()
        {
            Rational r;
            var result = Rational.DivRem(new Rational(0, 2), new Rational(1, 5), out r);
            Assert.AreEqual(0, result);
            Assert.AreEqual(new Rational(0, 1), r);
        }

        [Test]
        [ExpectedException(typeof(DivideByZeroException))]
        public void DivRemTestIndeterminate()
        {
            Rational r;
            var result = Rational.DivRem(new Rational(1, 2), new Rational(0, 0), out r);
        }

        [Test]
        public void DivRemTestNegative()
        {
            Rational r;
            var result = Rational.DivRem(new Rational(-3, 2), new Rational(2, 3), out r);
            Assert.AreEqual(-2, result);
            Assert.AreEqual(new Rational(-1, 6), r);
        }

        [Test]
        public void PowerTestZero()
        {
            var r = Rational.Pow(new Rational(3, 8), 0);
            Assert.AreEqual(new Rational(1, 1), r);
        }

        [Test]
        public void PowerTestOne()
        {
            var r = Rational.Pow(new Rational(3, 8), 1);
            Assert.AreEqual(new Rational(3, 8), r);
        }

        [Test]
        public void PowerTestInteger()
        {
            var r = Rational.Pow(new Rational(3, 8), 3);
            Assert.AreEqual(new Rational(27, 512), r);
        }

        [Test]
        public void PowerTestLarge()
        {
            var r = Rational.Pow(new Rational(2, 3), 10);
            Assert.AreEqual(new Rational(1024, 59049), r);
        }

        [Test]
        public void PowerTestNegativeOne()
        {
            var r = Rational.Pow(new Rational(4, 7), -1);
            Assert.AreEqual(new Rational(7, 4), r);
        }

        [Test]
        public void PowerTestNegative()
        {
            var r = Rational.Pow(new Rational(4, 7), -2);
            Assert.AreEqual(new Rational(49, 16), r);
        }

        [Test]
        public void SimplifyTestTrivial()
        {
            var r = Rational.Simplify(4, 7);
            Assert.AreEqual(4, r.Numerator);
            Assert.AreEqual(7, r.Denominator);
        }

        [Test]
        public void SimplifyTestMultiples()
        {
            var r = Rational.Simplify(64, 4);
            Assert.AreEqual(16, r.Numerator);
            Assert.AreEqual(1, r.Denominator);
        }

        [Test]
        public void SimplifyTestNonTrivial()
        {
            var r = Rational.Simplify(45, 18);
            Assert.AreEqual(5, r.Numerator);
            Assert.AreEqual(2, r.Denominator);
        }

        [Test]
        public void SimplifyTestBothNegative()
        {
            var r = Rational.Simplify(-10, -5);
            Assert.AreEqual(2, r.Numerator);
            Assert.AreEqual(1, r.Denominator);
        }

        [Test]
        public void SimplifyTestDenominatorNegative()
        {
            var r = Rational.Simplify(10, -5);
            Assert.AreEqual(-2, r.Numerator);
            Assert.AreEqual(1, r.Denominator);
        }

        [Test]
        public void SimplifyTestIndeterminate()
        {
            var r = Rational.Simplify(0, 0);
            Assert.AreEqual(0, r.Numerator);
            Assert.AreEqual(0, r.Denominator);
        }

        [Test]
        public void SimplifyTestPositiveInfinity()
        {
            var r = Rational.Simplify(7, 0);
            Assert.AreEqual(1, r.Numerator);
            Assert.AreEqual(0, r.Denominator);
        }

        [Test]
        public void SimplifyTestNegativeInfinity()
        {
            var r = Rational.Simplify(-7, 0);
            Assert.AreEqual(-1, r.Numerator);
            Assert.AreEqual(0, r.Denominator);
        }

        [Test]
        public void FromDoubleTestA()
        {
            var r = Rational.FromDouble(1.0 / 3.0);
            Assert.AreEqual(1, r.Numerator);
            Assert.AreEqual(3, r.Denominator);
        }

        [Test]
        public void FromDoubleTestB()
        {
            var r = Rational.FromDouble(2.0);
            Assert.AreEqual(2, r.Numerator);
            Assert.AreEqual(1, r.Denominator);
        }

        [Test]
        public void FromDoubleTestC()
        {
            var r = Rational.FromDouble(1.0 / 7.0);
            Assert.AreEqual(1, r.Numerator);
            Assert.AreEqual(7, r.Denominator);
        }

        [Test]
        public void FromDoubleTestD()
        {
            var r = Rational.FromDouble(3.0 / 14.0);
            Assert.AreEqual(3, r.Numerator);
            Assert.AreEqual(14, r.Denominator);
        }

        [Test]
        public void FromDoubleTestE()
        {
            var r = Rational.FromDouble(47.0 / 49.0);
            Assert.AreEqual(47, r.Numerator);
            Assert.AreEqual(49, r.Denominator);
        }

        [Test]
        public void FromDoubleTestNegative()
        {
            var r = Rational.FromDouble(-1.0 / 5.0);
            Assert.AreEqual(-1, r.Numerator);
            Assert.AreEqual(5, r.Denominator);
        }

        [Test]
        public void FromDoubleTestPositiveInfinity()
        {
            var r = Rational.FromDouble(double.PositiveInfinity);
            Assert.AreEqual(1, r.Numerator);
            Assert.AreEqual(0, r.Denominator);
        }

        [Test]
        public void FromDoubleTestNegativeInfinity()
        {
            var r = Rational.FromDouble(double.NegativeInfinity);
            Assert.AreEqual(-1, r.Numerator);
            Assert.AreEqual(0, r.Denominator);
        }

        [Test]
        public void FromDoubleTestNaN()
        {
            var r = Rational.FromDouble(double.NaN);
            Assert.AreEqual(0, r.Numerator);
            Assert.AreEqual(0, r.Denominator);
        }

    }
}
