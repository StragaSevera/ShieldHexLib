using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
            public void HasCoordsAccessor()
            {
                var hex = new Hex(1, 2, -3);
                Assert.That(hex[0], Is.EqualTo(1));
                Assert.That(hex[1], Is.EqualTo(2));
                Assert.That(hex[2], Is.EqualTo(-3));
                Assert.That(() => hex[3], Throws.TypeOf(typeof(IndexOutOfRangeException)));
            }

            [Test]
            public void HasCoordsVector()
            {
                var hex = new Hex(1, 2, -3);
                Assert.That(hex.Vector(), Is.EqualTo(new Vector2(1f, 2f)));
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
            public void CanBeInitializedFromHexF()
            {
                var hexF = new HexF(-1.5f, 4.5f);
                Hex hex = Hex.FromHexF(hexF);
                Assert.That(hex.Q, Is.EqualTo(-2));
                Assert.That(hex.R, Is.EqualTo(4));
                Assert.That(hex.S, Is.EqualTo(-2));
            }

            [Test]
            public void CannotBeInitializedFromInvalidCoords()
            {
                Hex HexFunc() => new Hex(1, 2, 0);
                Assert.That(HexFunc, Throws.ArgumentException);
            }

            [Test]
            public void EqualsToAnotherHexWithSameCoords()
            {
                var hex = new Hex(1, 2, -3);
                var anotherHex = new Hex(1, 2, -3);
                Assert.That(hex == anotherHex);
                Assert.That(hex.Equals(anotherHex));
            }

            [Test]
            public void DoesNotEqualAnotherHexWithDifferentCoords()
            {
                var hex = new Hex(1, 2, -3);
                var anotherHex = new Hex(1, 1, -2);
                Assert.That(hex != anotherHex);
                Assert.That(!hex.Equals(anotherHex));
            }

            [Test]
            public void DoesNotEqualNullHex()
            {
                var hex = new Hex(1, 2, -3);
                Assert.That(hex != null);
                Assert.That(!hex.Equals(null));
            }

            [Test]
            public void CanBeHashed()
            {
                var dict = new Dictionary<Hex, int>
                {
                    [new Hex(1, 1, -2)] = 1
                };
                var hex = new Hex(1, 1, -2);
                Assert.That(dict[hex] == 1);
            }

            [Test]
            public void HasStringRepresentation()
            {
                var hex = new Hex(1, 2, -3);
                Assert.That(hex.ToString(), Is.EqualTo("Hex(1, 2, -3)"));
            }

            [Test]
            public void CanBeIterated()
            {
                var hex = new Hex(1, 2, -3);
                var list = new List<int> {1, 2, -3};
                Assert.That(hex.ToList().SequenceEqual(list));
            }

            [Test]
            public void CanBeBracketsAccessed()
            {
                var hex = new Hex(1, 2, -3);
                Assert.That(hex[0] == 1);
                Assert.That(hex[1] == 2);
                Assert.That(hex[2] == -3);
                Assert.That(hex.Length == 3);
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
        public class WithSpatialAwareness
        {
            [TestCase(1, 2, -3, ExpectedResult = 3)]
            [TestCase(0, 0, 0, ExpectedResult = 0)]
            [TestCase(-2, 4, -2, ExpectedResult = 4)]
            public int HasValidDistanceFromOrigin(int q, int r, int s)
            {
                var hex = new Hex(q, r, s);
                return hex.DistanceFromOrigin();
            }

            [TestCase(1, 2, -3, 0, 0, 0, ExpectedResult = 3)]
            [TestCase(2, 2, -4, 2, 2, -4, ExpectedResult = 0)]
            [TestCase(1, 2, -3, -4, 2, 2, ExpectedResult = 5)]
            [TestCase(5, -1, -4, -2, 1, 1, ExpectedResult = 7)]
            public int HasValidDistance(int q1, int r1, int s1, int q2, int r2, int s2)
            {
                var hex = new Hex(q1, r1, s1);
                var anotherHex = new Hex(q2, r2, s2);
                return hex.Distance(anotherHex);
            }

            [TestCase(0, ExpectedResult = new[] {1, 0, -1})]
            [TestCase(1, ExpectedResult = new[] {1, -1, 0})]
            [TestCase(5, ExpectedResult = new[] {0, 1, -1})]
            [TestCase(6, ExpectedResult = new[] {1, 0, -1})]
            [TestCase(-1, ExpectedResult = new[] {0, 1, -1})]
            public int[] HasValidDirection(int dir)
            {
                Hex direction = Hex.Direction(dir);
                return direction.ToArray();
            }

            [TestCase(0, 0, 0, 0, ExpectedResult = new[] {1, 0, -1})]
            [TestCase(1, 2, -3, 6, ExpectedResult = new[] {2, 2, -4})]
            [TestCase(-3, 3, 0, 4, ExpectedResult = new[] {-4, 4, 0})]
            public int[] HasValidNeighbor(int q, int r, int s, int dir)
            {
                var hex = new Hex(q, r, s);
                Hex neighbor = hex.Neighbor(dir);
                return neighbor.ToArray();
            }

            [Test]
            public void HasValidNeighbors()
            {
                var hex = new Hex(1, 2, -3);
                var neighbors = hex.Neighbors();
                var validNeighbors = new[]
                {
                    new Hex(2, 2, -4), new Hex(2, 1, -3),
                    new Hex(1, 1, -2), new Hex(0, 2, -2),
                    new Hex(0, 3, -3), new Hex(1, 3, -4)
                };
                Assert.That(neighbors.Length == 6);
                Assert.That(neighbors, Is.EqualTo(validNeighbors));
            }

            [TestCase(0, 0.433f, 0.433f, -0.866f)]
            [TestCase(1, 0.866f, -0.433f, -0.433f)]
            [TestCase(5, -0.433f, 0.866f, -0.433f)]
            [TestCase(6, 0.433f, 0.433f, -0.866f)]
            [TestCase(-1, -0.433f, 0.866f, -0.433f)]
            public void HasValidCornerOffset(int dir, float resultQ, float resultR, float resultS)
            {
                HexF offset = Hex.CornerOffset(dir);
                Assert.That(offset.Q, Is.EqualTo(resultQ).Within(0.001f));
                Assert.That(offset.R, Is.EqualTo(resultR).Within(0.001f));
                Assert.That(offset.S, Is.EqualTo(resultS).Within(0.001f));
            }

            [TestCase(0, 0, 0, 0, 0.433f, 0.433f, -0.866f)]
            [TestCase(1, 2, -3, 6, 1.433f, 2.433f, -3.866f)]
            [TestCase(-3, 3, 0, 4, -3.866f, 3.433f, 0.433f)]
            public void HasValidCorner(int q, int r, int s, int dir, float resultQ, float resultR,
                float resultS)
            {
                var hex = new Hex(q, r, s);
                HexF corner = hex.Corner(dir);
                Assert.That(corner.Q, Is.EqualTo(resultQ).Within(0.001f));
                Assert.That(corner.R, Is.EqualTo(resultR).Within(0.001f));
                Assert.That(corner.S, Is.EqualTo(resultS).Within(0.001f));
            }

            [Test]
            public void HasValidCorners()
            {
                var hex = new Hex(1, 2, -3);
                var corners = hex.Corners();
                var validCorners = new[]
                {
                    new HexF(1.433f, 2.433f, -3.866f),
                    new HexF(1.866f, 1.567f, -3.433f),
                    new HexF(1.433f, 1.134f, -2.567f),
                    new HexF(0.567f, 1.567f, -2.134f),
                    new HexF(0.134f, 2.433f, -2.567f),
                    new HexF(0.567f, 2.866f, -3.433f)
                };
                Assert.That(corners.Length == 6);
                Assert.That(corners, Is.EqualTo(validCorners).Within(0.001f));
            }

            [Test]
            public void HasValidCornersWrapping()
            {
                var hex = new Hex(1, 2, -3);
                var corners = hex.CornersWrapping();
                var validCorners = new[]
                {
                    new HexF(1.433f, 2.433f, -3.866f),
                    new HexF(1.866f, 1.567f, -3.433f),
                    new HexF(1.433f, 1.134f, -2.567f),
                    new HexF(0.567f, 1.567f, -2.134f),
                    new HexF(0.134f, 2.433f, -2.567f),
                    new HexF(0.567f, 2.866f, -3.433f),
                    new HexF(1.433f, 2.433f, -3.866f),
                };
                Assert.That(corners.Length == 7);
                Assert.That(corners, Is.EqualTo(validCorners).Within(0.001f));
            }
        }
    }
}