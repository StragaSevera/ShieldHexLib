using System;
using System.Numerics;
using NUnit.Framework;

namespace ShieldHexLib.Test
{
    [TestFixture]
    public class OrientationTest
    {
        [Test]
        public void CanBeCreated()
        {
            var forward = new Matrix3x2(
                (float) Math.Sqrt(3f), (float) Math.Sqrt(3f) / 2f,
                0f, 3f / 2f,
                0f, 0f);
            var backward = new Matrix3x2(
                (float) Math.Sqrt(3f) / 3f, -1f / 3f,
                0f, 2f / 3f,
                0f, 0f);
            const float angle = 0.5f;

            var orientation = new Orientation(forward, angle);

            Assert.That(orientation.Forward == forward);
            Assert.That(orientation.Backward == backward);
            Assert.That(orientation.StartAngle, Is.EqualTo(angle).Within(0.00001));
        }

        [TestFixture]
        public class WithPointyOrientation
        {
            private static object[] _transformVectors =
            {
                new object[] {new Vector2(0, 0), new Vector2(0, 0)},
                new object[] {new Vector2(1, 1), new Vector2((float) Math.Sqrt(3) * 1.5f, 1.5f)},
                new object[] {new Vector2(5, 3), new Vector2((float) Math.Sqrt(3) * 6.5f, 4.5f)},
                new object[] {new Vector2(1, -2), new Vector2(0f, -3f)},
                new object[] {new Vector2(-3, 4), new Vector2((float) -Math.Sqrt(3), 6f)}
            };

            [TestCaseSource(nameof(_transformVectors))]
            public void HasValidTransform(Vector2 v1, Vector2 v2)
            {
                Orientation or = Orientation.Pointy;

                Vector2 v = Vector2.Transform(v1, or.Forward);
                Assert.That(v.X, Is.EqualTo(v2.X).Within(0.00001));
                Assert.That(v.Y, Is.EqualTo(v2.Y).Within(0.00001));
            }

            [TestCaseSource(nameof(_transformVectors))]
            public void HasValidBackwardsTransform(Vector2 v1, Vector2 v2)
            {
                Orientation or = Orientation.Pointy;

                Vector2 v = Vector2.Transform(v2, or.Backward);
                Assert.That(v.X, Is.EqualTo(v1.X).Within(0.00001));
                Assert.That(v.Y, Is.EqualTo(v1.Y).Within(0.00001));
            }

            [Test]
            public void HasValidAngle()
            {
                const float angle = 0.5f;
                Orientation or = Orientation.Pointy;

                Assert.That(or.StartAngle, Is.EqualTo(angle).Within(0.00001));
            }
        }


        [TestFixture]
        public class WithFlatOrientation
        {
            private static object[] _transformVectors =
            {
                new object[] {new Vector2(0, 0), new Vector2(0, 0)},
                new object[] {new Vector2(1, 1), new Vector2(1.5f, (float) Math.Sqrt(3) * 1.5f)},
                new object[] {new Vector2(5, 3), new Vector2(7.5f, (float) Math.Sqrt(3) * 5.5f)},
                new object[] {new Vector2(1, -2), new Vector2(1.5f, (float) -Math.Sqrt(3) * 1.5f)},
                new object[] {new Vector2(-3, 4), new Vector2(-4.5f, (float) Math.Sqrt(3) * 2.5f)}
            };

            [TestCaseSource(nameof(_transformVectors))]
            public void HasValidTransform(Vector2 v1, Vector2 v2)
            {
                Orientation or = Orientation.Flat;

                Vector2 v = Vector2.Transform(v1, or.Forward);
                Assert.That(v.X, Is.EqualTo(v2.X).Within(0.00001));
                Assert.That(v.Y, Is.EqualTo(v2.Y).Within(0.00001));
            }

            [TestCaseSource(nameof(_transformVectors))]
            public void HasValidBackwardsTransform(Vector2 v1, Vector2 v2)
            {
                Orientation or = Orientation.Flat;

                Vector2 v = Vector2.Transform(v2, or.Backward);
                Assert.That(v.X, Is.EqualTo(v1.X).Within(0.00001));
                Assert.That(v.Y, Is.EqualTo(v1.Y).Within(0.00001));
            }

            [Test]
            public void HasValidAngle()
            {
                const float angle = 0f;
                Orientation or = Orientation.Flat;

                Assert.That(or.StartAngle, Is.EqualTo(angle).Within(0.00001));
            }
        }
    }
}
