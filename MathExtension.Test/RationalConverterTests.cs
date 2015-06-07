using System;
using NUnit.Framework;

namespace MathExtension.Test
{
    [TestFixture]
    public class RationalConverterTests
    {
        #region Convert To

        [Test]
        public void CanConvertToString()
        {
            var c = new RationalConverter();

            Assert.That(c.CanConvertTo(typeof(string)));

            var value = c.ConvertToString(new Rational(2, 3));
            Assert.That(value, Is.InstanceOf<string>());
            Assert.That(value, Is.EqualTo("2/3"));
        }

        [Test]
        public void CanConvertToInt32()
        {
            var c = new RationalConverter();

            Assert.That(c.CanConvertTo(typeof(int)));

            var value = c.ConvertTo(new Rational(7, 4), typeof(int));
            Assert.That(value, Is.InstanceOf<int>());
            Assert.That(value, Is.EqualTo(2));
        }

        [Test]
        public void CanConvertToInt32_AndRoundDown()
        {
            var c = new RationalConverter();

            var value = c.ConvertTo(new Rational(5, 4), typeof(int));
            Assert.That(value, Is.EqualTo(1));
        }

        [Test]
        public void CanConvertToUInt32()
        {
            var c = new RationalConverter();

            Assert.That(c.CanConvertTo(typeof(uint)));

            var value = c.ConvertTo(new Rational(7, 4), typeof(uint));
            Assert.That(value, Is.InstanceOf<uint>());
            Assert.That(value, Is.EqualTo(2));
        }

        [Test]
        public void CanConvertToInt16()
        {
            var c = new RationalConverter();

            Assert.That(c.CanConvertTo(typeof(short)));

            var value = c.ConvertTo(new Rational(7, 4), typeof(short));
            Assert.That(value, Is.InstanceOf<short>());
            Assert.That(value, Is.EqualTo((short)2));
        }

        [Test]
        public void CanConvertToUInt16()
        {
            var c = new RationalConverter();

            Assert.That(c.CanConvertTo(typeof(ushort)));

            var value = c.ConvertTo(new Rational(7, 4), typeof(ushort));
            Assert.That(value, Is.InstanceOf<ushort>());
            Assert.That(value, Is.EqualTo((ushort)2));
        }

        [Test]
        public void CanConvertToInt64()
        {
            var c = new RationalConverter();

            Assert.That(c.CanConvertTo(typeof(long)));

            var value = c.ConvertTo(new Rational(7, 4), typeof(long));
            Assert.That(value, Is.InstanceOf<long>());
            Assert.That(value, Is.EqualTo(2L));
        }

        [Test]
        public void CanConvertToUInt64()
        {
            var c = new RationalConverter();

            Assert.That(c.CanConvertTo(typeof(ulong)));

            var value = c.ConvertTo(new Rational(7, 4), typeof(ulong));
            Assert.That(value, Is.InstanceOf<ulong>());
            Assert.That(value, Is.EqualTo(2L));
        }

        [Test]
        public void CanConvertToSByte()
        {
            var c = new RationalConverter();

            Assert.That(c.CanConvertTo(typeof(sbyte)));

            var value = c.ConvertTo(new Rational(7, 4), typeof(sbyte));
            Assert.That(value, Is.InstanceOf<sbyte>());
            Assert.That(value, Is.EqualTo((sbyte)2));
        }

        [Test]
        public void CanConvertToByte()
        {
            var c = new RationalConverter();

            Assert.That(c.CanConvertTo(typeof(byte)));

            var value = c.ConvertTo(new Rational(7, 4), typeof(byte));
            Assert.That(value, Is.InstanceOf<byte>());
            Assert.That(value, Is.EqualTo((byte)2));
        }

        [Test]
        public void CanConvertToDouble()
        {
            var c = new RationalConverter();

            Assert.That(c.CanConvertTo(typeof(double)));

            var value = c.ConvertTo(new Rational(7, 4), typeof(double));
            Assert.That(value, Is.InstanceOf<double>());
            Assert.That(value, Is.EqualTo(1.75));
        }

        [Test]
        public void CanConvertToSingle()
        {
            var c = new RationalConverter();

            Assert.That(c.CanConvertTo(typeof(float)));

            var value = c.ConvertTo(new Rational(7, 4), typeof(float));
            Assert.That(value, Is.InstanceOf<float>());
            Assert.That(value, Is.EqualTo(1.75f));
        }

        [Test]
        public void CanConvertToBoolean()
        {
            var c = new RationalConverter();

            Assert.That(c.CanConvertTo(typeof(bool)));

            var value = c.ConvertTo(new Rational(7, 4), typeof(bool));
            Assert.That(value, Is.InstanceOf<bool>());
            Assert.That(value, Is.EqualTo(true));
        }

        #endregion

        #region Convert From

        [Test]
        public void CanConvertFromString()
        {
            var c = new RationalConverter();

            Assert.That(c.CanConvertFrom(typeof(string)));

            var value = c.ConvertFromString("2/3");
            Assert.That(value, Is.EqualTo(new Rational(2, 3)));
        }

        [Test]
        public void CanConvertFromInt32()
        {
            var c = new RationalConverter();

            Assert.That(c.CanConvertFrom(typeof(int)));

            var value = c.ConvertFrom(2);
            Assert.That(value, Is.EqualTo(new Rational(2)));
        }

        [Test]
        public void CanConvertFromUInt32()
        {
            var c = new RationalConverter();

            Assert.That(c.CanConvertFrom(typeof(uint)));

            var value = c.ConvertFrom((uint)2);
            Assert.That(value, Is.EqualTo(new Rational(2)));
        }

        [Test]
        public void CanConvertFromInt16()
        {
            var c = new RationalConverter();

            Assert.That(c.CanConvertFrom(typeof(short)));

            var value = c.ConvertFrom((short)2);
            Assert.That(value, Is.EqualTo(new Rational(2)));
        }

        [Test]
        public void CanConvertFromUInt16()
        {
            var c = new RationalConverter();

            Assert.That(c.CanConvertFrom(typeof(ushort)));

            var value = c.ConvertFrom((ushort)2);
            Assert.That(value, Is.EqualTo(new Rational(2)));
        }

        [Test]
        public void CanConvertFromInt64()
        {
            var c = new RationalConverter();

            Assert.That(c.CanConvertFrom(typeof(long)));

            var value = c.ConvertFrom(2L);
            Assert.That(value, Is.EqualTo(new Rational(2)));
        }

        [Test]
        public void CanConvertFromUInt64()
        {
            var c = new RationalConverter();

            Assert.That(c.CanConvertFrom(typeof(ulong)));

            var value = c.ConvertFrom((ulong)2);
            Assert.That(value, Is.EqualTo(new Rational(2)));
        }

        [Test]
        public void CanConvertFromSByte()
        {
            var c = new RationalConverter();

            Assert.That(c.CanConvertFrom(typeof(sbyte)));

            var value = c.ConvertFrom((sbyte)-2);
            Assert.That(value, Is.EqualTo(new Rational(-2)));
        }

        [Test]
        public void CanConvertFromByte()
        {
            var c = new RationalConverter();

            Assert.That(c.CanConvertFrom(typeof(byte)));

            var value = c.ConvertFrom((byte)2);
            Assert.That(value, Is.EqualTo(new Rational(2)));
        }

        [Test]
        public void CanConvertFromDouble()
        {
            var c = new RationalConverter();

            Assert.That(c.CanConvertFrom(typeof(double)));

            var value = c.ConvertFrom(3.6);
            Assert.That(value, Is.EqualTo(new Rational(18, 5)));
        }

        [Test]
        public void CanConvertFromSingle()
        {
            var c = new RationalConverter();

            Assert.That(c.CanConvertFrom(typeof(float)));

            var value = c.ConvertFrom(3.6f);
            Assert.That(value, Is.EqualTo(new Rational(18, 5)));
        }

        [Test]
        public void CanConvertFromBoolean()
        {
            var c = new RationalConverter();

            Assert.That(c.CanConvertFrom(typeof(bool)));

            var value = c.ConvertFrom(false);
            Assert.That(value, Is.EqualTo(Rational.Zero));
        }

        #endregion
    }
}
