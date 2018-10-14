using NUnit.Framework;

namespace ShieldHexLib.Test
{
    [TestFixture]
    public class HexFTest
    {
        [TestFixture]
        public class WithFloatInit : HexFTest
        {
            [Test]
            public void HasCoords()
            {
                var hex = new HexF(1.4f, 2.3f, -3.7f);
                Assert.That(hex.Q, Is.EqualTo(1.4f).Within(0.00001));
                Assert.That(hex.R, Is.EqualTo(2.3f).Within(0.00001));
                Assert.That(hex.S, Is.EqualTo(-3.7f).Within(0.00001));
            }

            [Test]
            public void CanBeInitializedByTwoCoords()
            {
                var hex = new HexF(-1.4f, -2.3f);
                Assert.That(hex.Q, Is.EqualTo(-1.4f).Within(0.00001));
                Assert.That(hex.R, Is.EqualTo(-2.3f).Within(0.00001));
                Assert.That(hex.S, Is.EqualTo(3.7f).Within(0.00001));
            }
        }

        [TestFixture]
        public class WithDoubleInit : HexFTest
        {
            [SetUp]
            public void Setup() { }

            [Test]
            public void HasCoords()
            {
                var hex = new HexF(1.4, 2.3, -3.7);
                Assert.That(hex.Q, Is.EqualTo(1.4).Within(0.00001));
                Assert.That(hex.R, Is.EqualTo(2.3).Within(0.00001));
                Assert.That(hex.S, Is.EqualTo(-3.7).Within(0.00001));
            }

            [Test]
            public void CanBeInitializedByTwoCoords()
            {
                var hex = new HexF(-1.4, -2.3);
                Assert.That(hex.Q, Is.EqualTo(-1.4).Within(0.00001));
                Assert.That(hex.R, Is.EqualTo(-2.3).Within(0.00001));
                Assert.That(hex.S, Is.EqualTo(3.7).Within(0.00001));
            }
        }

        [Test]
        public void IsValid()
        {
            var hex = new HexF(1.2, 2.3, -3.5);
            Assert.That(hex.IsValid(), Is.True);
        }

        [Test]
        public void IsInvalidWhenSumOfCoordsNotZero()
        {
            var hex = new HexF(1.2, 2.3, 0);
            Assert.That(hex.IsValid(), Is.False);
        }

        [Test]
        public void EqualsToAnotherHexWithSameCoords()
        {
            var hex = new HexF(1, 2, -3);
            var anotherHex = new HexF(1, 2, -3);
            Assert.That(hex, Is.EqualTo(anotherHex));
        }

        [Test]
        public void DoesNotEqualAnotherHexWithDifferentCoords()
        {
            var hex = new HexF(1, 2, -3);
            var anotherHex = new HexF(1, 1, -2);
            Assert.That(hex, Is.Not.EqualTo(anotherHex));
        }
    }
}
