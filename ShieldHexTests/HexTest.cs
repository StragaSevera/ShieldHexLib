using System;
using NUnit.Framework;

namespace ShieldHexLib.Test
{
    [TestFixture]
    public class HexTest
    {
        [TestFixture]
        public class WithBasicTests
        {
            [Test]
            public void HasCoords()
            {
                var hex = new Hex(1, 2, -3);
                Assert.That(hex.Q, Is.EqualTo(1));
                Assert.That(hex.R, Is.EqualTo(2));
                Assert.That(hex.S, Is.EqualTo(-3));
            }

            [Test]
            public void HasCoordsVector()
            {
                var hex = new Hex(1, 2, -3);
                Assert.That(hex[0], Is.EqualTo(1));
                Assert.That(hex[1], Is.EqualTo(2));
                Assert.That(hex[2], Is.EqualTo(-3));
                Assert.That(() => hex[3], Throws.TypeOf(typeof(IndexOutOfRangeException)));
            }

            [Test]
            public void CanBeInitializedByTwoCoords()
            {
                var hex = new Hex(-1, -2);
                Assert.That(hex.Q, Is.EqualTo(-1));
                Assert.That(hex.R, Is.EqualTo(-2));
                Assert.That(hex.S, Is.EqualTo(3));
            }

            [Test]
            public void IsValid()
            {
                var hex = new Hex(1, 2, -3);
                Assert.That(hex.IsValid(), Is.True);
            }

            [Test]
            public void IsInvalidWhenSumOfCoordsNotZero()
            {
                var hex = new Hex(1, 2, 0);
                Assert.That(hex.IsValid(), Is.False);
            }

            [Test]
            public void EqualsToAnotherHexWithSameCoords()
            {
                var hex = new Hex(1, 2, -3);
                var anotherHex = new Hex(1, 2, -3);
                Assert.That(hex, Is.EqualTo(anotherHex));
            }

            [Test]
            public void DoesNotEqualAnotherHexWithDifferentCoords()
            {
                var hex = new Hex(1, 2, -3);
                var anotherHex = new Hex(1, 1, -2);
                Assert.That(hex, Is.Not.EqualTo(anotherHex));
            }
        }

        [TestFixture]
        public class WithArithmetic
        {
            [Test]
            public void HasValidAddition()
            {
                var hex = new Hex(1, 2, -3);
                var anotherHex = new Hex(0, 2, -2);
                Hex result = hex + anotherHex;
                var matchingResult = new Hex(1, 4, -5);

                Assert.That(result, Is.EqualTo(matchingResult));
            }

            [Test]
            public void HasValidSubtraction()
            {
                var hex = new Hex(1, 2, -3);
                var anotherHex = new Hex(0, 2, -2);
                Hex result = hex - anotherHex;
                var matchingResult = new Hex(1, 0, -1);

                Assert.That(result, Is.EqualTo(matchingResult));
            }

            [Test]
            public void HasValidMultiplicationByInt()
            {
                var hex = new Hex(1, 2, -3);
                const int coefficient = 2;
                Hex result = hex * coefficient;
                var matchingResult = new Hex(2, 4, -6);

                Assert.That(result, Is.EqualTo(matchingResult));
            }

            [Test]
            public void HasValidReversedMultiplicationByInt()
            {
                var hex = new Hex(1, 2, -3);
                const int coefficient = 2;
                Hex result = coefficient * hex;
                var matchingResult = new Hex(2, 4, -6);

                Assert.That(result, Is.EqualTo(matchingResult));
            }
        }

        [TestFixture]
        public class WithDistance
        {
            [TestCase(1, 2, -3, ExpectedResult = 3)]
            [TestCase(0, 0, 0, ExpectedResult = 0)]
            [TestCase(-2, 4, -2, ExpectedResult = 4)]
            public int HasValidLength(int q, int r, int s)
            {
                var hex = new Hex(q, r, s);
                return hex.Length();
            }
        }
    }
}
