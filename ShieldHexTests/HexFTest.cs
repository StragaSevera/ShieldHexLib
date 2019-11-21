using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using NUnit.Framework;

namespace ShieldHexLib.Test
{
    [TestFixture]
    public class HexFTest
    {
        private const float Tolerance = HexF.Tolerance;
        [TestFixture]
        public class WithFloatInit : HexFTest
        {
            [Test]
            public void HasCoords()
            {
                var hex = new HexF(1.4f, 2.3f, -3.7f);
                Assert.That(hex.Q, Is.EqualTo(1.4f).Within(Tolerance));
                Assert.That(hex.R, Is.EqualTo(2.3f).Within(Tolerance));
                Assert.That(hex.S, Is.EqualTo(-3.7f).Within(Tolerance));
            }

            [Test]
            public void CanBeInitializedByTwoCoords()
            {
                var hex = new HexF(-1.4f, -2.3f);
                Assert.That(hex.Q, Is.EqualTo(-1.4f).Within(Tolerance));
                Assert.That(hex.R, Is.EqualTo(-2.3f).Within(Tolerance));
                Assert.That(hex.S, Is.EqualTo(3.7f).Within(Tolerance));
            }

            [Test]
            public void CanBeInitializedByVector2()
            {
                var vector = new Vector2(-1.4f, -2.3f);
                var hex = new HexF(vector);
                Assert.That(hex.Q, Is.EqualTo(-1.4f).Within(Tolerance));
                Assert.That(hex.R, Is.EqualTo(-2.3f).Within(Tolerance));
                Assert.That(hex.S, Is.EqualTo(3.7f).Within(Tolerance));
            }
        }

        [TestFixture]
        public class WithDoubleInit : HexFTest
        {
            [Test]
            public void HasCoords()
            {
                var hex = new HexF(1.4, 2.3, -3.7);
                Assert.That(hex.Q, Is.EqualTo(1.4).Within(Tolerance));
                Assert.That(hex.R, Is.EqualTo(2.3).Within(Tolerance));
                Assert.That(hex.S, Is.EqualTo(-3.7).Within(Tolerance));
            }

            [Test]
            public void CanBeInitializedByTwoCoords()
            {
                var hex = new HexF(-1.4, -2.3);
                Assert.That(hex.Q, Is.EqualTo(-1.4).Within(Tolerance));
                Assert.That(hex.R, Is.EqualTo(-2.3).Within(Tolerance));
                Assert.That(hex.S, Is.EqualTo(3.7).Within(Tolerance));
            }
        }

        [TestFixture]
        public class WithCommonFeatures
        {
            [Test]
            public void CannotBeInvalid()
            {
                HexF HexFunc() => new HexF(1.2, 2.3, 0);
                Assert.That(HexFunc, Throws.ArgumentException);
            }

            [Test]
            public void EqualsToAnotherHexWithSameCoords()
            {
                var hex = new HexF(1, 2, -3);
                var anotherHex = new HexF(1, 2, -3);
                Assert.That(hex == anotherHex);
                Assert.That(hex.Equals(anotherHex));
            }

            [Test]
            public void DoesNotEqualAnotherHexWithDifferentCoords()
            {
                var hex = new HexF(1, 2, -3);
                var anotherHex = new HexF(1, 1, -2);
                Assert.That(hex != anotherHex);
                Assert.That(!hex.Equals(anotherHex));
            }

            [Test]
            public void CanBeHashed()
            {
                var dict = new Dictionary<HexF, int>
                {
                    [new HexF(1, 1, -2)] = 1
                };
                var hex = new HexF(1, 1, -2);
                Assert.That(dict[hex] == 1);
            }

            [Test]
            public void CanBeIterated()
            {
                var hex = new HexF(1, 2, -3);
                var list = new List<float> {1, 2, -3};
                Assert.That(hex.ToList().SequenceEqual(list));
            }

            [Test]
            public void CanBeBracketsAccessed()
            {
                var hex = new HexF(1, 2, -3);
                Assert.That(hex[0], Is.EqualTo(1f).Within(Tolerance));
                Assert.That(hex[1], Is.EqualTo(2f).Within(Tolerance));
                Assert.That(hex[2], Is.EqualTo(-3f).Within(Tolerance));
                Assert.That(hex.Dimensions == 3);
            }
        }
    }
}
