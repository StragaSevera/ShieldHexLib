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
                var hexF = new HexF(1.4f, 2.3f, -3.7f);
                Assert.That(hexF.Q, Is.EqualTo(1.4f).Within(Tolerance));
                Assert.That(hexF.R, Is.EqualTo(2.3f).Within(Tolerance));
                Assert.That(hexF.S, Is.EqualTo(-3.7f).Within(Tolerance));
            }

            [Test]
            public void CanBeInitializedByTwoCoords()
            {
                var hexF = new HexF(-1.4f, -2.3f);
                Assert.That(hexF.Q, Is.EqualTo(-1.4f).Within(Tolerance));
                Assert.That(hexF.R, Is.EqualTo(-2.3f).Within(Tolerance));
                Assert.That(hexF.S, Is.EqualTo(3.7f).Within(Tolerance));
            }

            [Test]
            public void CanBeInitializedFromHex()
            {
                var hex = new Hex(-1, -2);
                var hexF = new HexF(hex);
                Assert.That(hexF.Q, Is.EqualTo(-1f).Within(Tolerance));
                Assert.That(hexF.R, Is.EqualTo(-2f).Within(Tolerance));
                Assert.That(hexF.S, Is.EqualTo(3f).Within(Tolerance));
            }

            [Test]
            public void CanBeInitializedByVector2()
            {
                var vector = new Vector2(-1.4f, -2.3f);
                var hexF = new HexF(vector);
                Assert.That(hexF.Q, Is.EqualTo(-1.4f).Within(Tolerance));
                Assert.That(hexF.R, Is.EqualTo(-2.3f).Within(Tolerance));
                Assert.That(hexF.S, Is.EqualTo(3.7f).Within(Tolerance));
            }
        }

        [TestFixture]
        public class WithDoubleInit : HexFTest
        {
            [Test]
            public void HasCoords()
            {
                var hexF = new HexF(1.4, 2.3, -3.7);
                Assert.That(hexF.Q, Is.EqualTo(1.4).Within(Tolerance));
                Assert.That(hexF.R, Is.EqualTo(2.3).Within(Tolerance));
                Assert.That(hexF.S, Is.EqualTo(-3.7).Within(Tolerance));
            }

            [Test]
            public void CanBeInitializedByTwoCoords()
            {
                var hexF = new HexF(-1.4, -2.3);
                Assert.That(hexF.Q, Is.EqualTo(-1.4).Within(Tolerance));
                Assert.That(hexF.R, Is.EqualTo(-2.3).Within(Tolerance));
                Assert.That(hexF.S, Is.EqualTo(3.7).Within(Tolerance));
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
                var hexF = new HexF(1, 2, -3);
                var anotherHexF = new HexF(1, 2, -3);
                Assert.That(hexF == anotherHexF);
                Assert.That(hexF.Equals(anotherHexF));
            }

            [Test]
            public void DoesNotEqualAnotherHexWithDifferentCoords()
            {
                var hexF = new HexF(1, 2, -3);
                var anotherHexF = new HexF(1, 1, -2);
                Assert.That(hexF != anotherHexF);
                Assert.That(!hexF.Equals(anotherHexF));
            }

            [Test]
            public void CanBeHashed()
            {
                var dict = new Dictionary<HexF, int>
                {
                    [new HexF(1, 1, -2)] = 1
                };
                var hexF = new HexF(1, 1, -2);
                Assert.That(dict[hexF] == 1);
            }

            [Test]
            public void HasStringRepresentation()
            {
                var hexF = new HexF(1, 2, -3);
                Assert.That(hexF.ToString(), Is.EqualTo("Hex(1, 2, -3)"));
            }

            [Test]
            public void CanBeIterated()
            {
                var hexF = new HexF(1, 2, -3);
                var list = new List<float> {1, 2, -3};
                Assert.That(hexF.ToList().SequenceEqual(list));
            }

            [Test]
            public void CanBeBracketsAccessed()
            {
                var hexF = new HexF(1, 2, -3);
                Assert.That(hexF[0], Is.EqualTo(1f).Within(Tolerance));
                Assert.That(hexF[1], Is.EqualTo(2f).Within(Tolerance));
                Assert.That(hexF[2], Is.EqualTo(-3f).Within(Tolerance));
                Assert.That(hexF.Length == 3);
            }
            
            
        }

        [TestFixture]
        public class WithMathsInit : HexFTest
        {
            [Test]
            public void CanBeAddedToHexF()
            {
                var hexF = new HexF(0.5f, 0f, -0.5f);
                var anotherHexF = new HexF(-1f, -2f, 3f);
                var result = new HexF(-0.5f, -2f, 2.5f);
                Assert.That(hexF + anotherHexF == result);
            }

            [Test]
            public void CanBeAddedToHex()
            {
                var hexF = new HexF(0.5f, 0f, -0.5f);
                var hex = new Hex(-1, -2, 3);
                var result = new HexF(-0.5f, -2f, 2.5f);
                Assert.That(hexF + hex == result);
            }

            [Test]
            public void CanBeReverseAddedToHex()
            {
                var hex = new Hex(-1, -2, 3);
                var hexF = new HexF(0.5f, 0f, -0.5f);
                var result = new HexF(-0.5f, -2f, 2.5f);
                Assert.That(hex + hexF == result);
            }
        }
    }
}
