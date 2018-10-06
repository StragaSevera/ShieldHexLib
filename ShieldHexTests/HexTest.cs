using NUnit.Framework;
using ShieldHexLib;

namespace ShieldHexTests
{
    public class HexTest
    {
        private Hex _hex;

        [TestFixture]
        public class WithIntegerInit : HexTest
        {
            [SetUp]
            public void Setup()
            {
                _hex = new Hex(1, 2, -3);
            }

            [Test]
            public void HasCoords()
            {
                Assert.That(_hex.Q, Is.EqualTo(1));
                Assert.That(_hex.R, Is.EqualTo(2));
                Assert.That(_hex.S, Is.EqualTo(-3));
            }

            [Test]
            public void IsNotFractional()
            {
                Assert.That(_hex.IsFractional, Is.False);
            }

            [Test]
            public void CanBeInitializedByTwoCoords()
            {
                _hex = new Hex(-1, -2);
                Assert.That(_hex.Q, Is.EqualTo(-1));
                Assert.That(_hex.R, Is.EqualTo(-2));
                Assert.That(_hex.S, Is.EqualTo(3));
                Assert.That(_hex.IsFractional, Is.False);
            }
        }

        [TestFixture]
        public class WithFloatInit : HexTest
        {
            [SetUp]
            public void Setup()
            {
                _hex = new Hex(1.4f, 2.3f, -3.7f);
            }

            [Test]
            public void HasCoords()
            {
                Assert.That(_hex.Qf, Is.EqualTo(1.4f).Within(0.00001));
                Assert.That(_hex.Rf, Is.EqualTo(2.3f).Within(0.00001));
                Assert.That(_hex.Sf, Is.EqualTo(-3.7f).Within(0.00001));
            }

            [Test]
            public void IsFractional()
            {
                Assert.That(_hex.IsFractional, Is.True);
            }

            [Test]
            public void CanBeInitializedByTwoCoords()
            {
                _hex = new Hex(-1.4f, -2.3f);
                Assert.That(_hex.Qf, Is.EqualTo(-1.4f).Within(0.00001));
                Assert.That(_hex.Rf, Is.EqualTo(-2.3f).Within(0.00001));
                Assert.That(_hex.Sf, Is.EqualTo(3.7f).Within(0.00001));
                Assert.That(_hex.IsFractional, Is.True);
            }
        }

        [TestFixture]
        public class WithDoubleInit : HexTest
        {
            [SetUp]
            public void Setup()
            {
                _hex = new Hex(1.4, 2.3, -3.7);
            }

            [Test]
            public void HasCoords()
            {
                Assert.That(_hex.Qf, Is.EqualTo(1.4).Within(0.00001));
                Assert.That(_hex.Rf, Is.EqualTo(2.3).Within(0.00001));
                Assert.That(_hex.Sf, Is.EqualTo(-3.7).Within(0.00001));
            }

            [Test]
            public void IsFractional()
            {
                Assert.That(_hex.IsFractional, Is.True);
            }

            [Test]
            public void CanBeInitializedByTwoCoords()
            {
                _hex = new Hex(-1.4, -2.3);
                Assert.That(_hex.Qf, Is.EqualTo(-1.4).Within(0.00001));
                Assert.That(_hex.Rf, Is.EqualTo(-2.3).Within(0.00001));
                Assert.That(_hex.Sf, Is.EqualTo(3.7).Within(0.00001));
                Assert.That(_hex.IsFractional, Is.True);
            }
        }
    }
}
