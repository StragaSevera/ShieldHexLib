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

        [Test]
        public void HasValidPointyLayout()
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

            Orientation orientation = Orientation.Pointy;

            Assert.That(orientation.Forward == forward);
            Assert.That(orientation.Backward == backward);
            Assert.That(orientation.StartAngle, Is.EqualTo(angle).Within(0.00001));
        }

        [Test]
        public void HasValidFlatLayout()
        {
            var forward = new Matrix3x2(
                3f / 2f, 0f,
                (float) Math.Sqrt(3f) / 2f, (float) Math.Sqrt(3f),
                0f, 0f);
            var backward = new Matrix3x2(
                2f / 3f, 0f,
                -1f / 3f, (float) Math.Sqrt(3f) / 3f,
                0f, 0f);
            const float angle = 0f;

            Orientation orientation = Orientation.Flat;

            Assert.That(orientation.Forward == forward);
            Assert.That(orientation.Backward == backward);
            Assert.That(orientation.StartAngle, Is.EqualTo(angle).Within(0.00001));
        }
    }
}
