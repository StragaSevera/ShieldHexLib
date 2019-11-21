using System.Numerics;
using NUnit.Framework;

namespace ShieldHexLib.Test
{
    [TestFixture]
    public class LayoutTest
    {
        [TestFixture]
        public class WithBasicTests
        {
            [Test]
            public void CanBeCreated()
            {
                var orientation = new Orientation();
                var layout = new Layout(orientation, Vector2.One, Vector2.Zero);

                Assert.That(layout.Orientation, Is.EqualTo(orientation));
                Assert.That(layout.Size, Is.EqualTo(Vector2.One));
                Assert.That(layout.Origin, Is.EqualTo(Vector2.Zero));
            }

            [Test]
            public void CanBePointy()
            {
                Orientation orientation = Orientation.Pointy;
                Layout layout = Layout.Pointy(Vector2.One, Vector2.Zero);

                Assert.That(layout.Orientation, Is.EqualTo(orientation));
                Assert.That(layout.Size, Is.EqualTo(Vector2.One));
                Assert.That(layout.Origin, Is.EqualTo(Vector2.Zero));
            }

            [Test]
            public void CanBeFlat()
            {
                Orientation orientation = Orientation.Flat;
                Layout layout = Layout.Flat(Vector2.One, Vector2.Zero);

                Assert.That(layout.Orientation, Is.EqualTo(orientation));
                Assert.That(layout.Size, Is.EqualTo(Vector2.One));
                Assert.That(layout.Origin, Is.EqualTo(Vector2.Zero));
            }
        }

        // All test cases must be calculated by hand
        // to verify that algorythm is right
        [TestFixture]
        public class WithHexToScreenConversion
        {
            [Test]
            public void CanTransformZero()
            {
                Layout layout = Layout.Pointy(new Vector2(100f), Vector2.Zero);
                var hex = new Hex(0, 0, 0);
                Assert.That(layout.HexToScreen(hex), Is.EqualTo(Vector2.Zero));
            }

            [TestCase(-1, 2, -1, 0f, 300f)]
            [TestCase(1, 0, -1, 173.205f, 0f)]
            [TestCase(1, 1, -2, 259.808f, 150f)]
            [TestCase(-3, 5, -2, -86.602f, 750f)]
            public void CanTransformPointyHex(int q, int r, int s, float resultX, float resultY)
            {
                Layout layout = Layout.Pointy(new Vector2(100f), Vector2.Zero);
                var hex = new Hex(q, r, s);
                Vector2 screenVector = layout.HexToScreen(hex);
                Assert.That(screenVector.X, Is.EqualTo(resultX).Within(0.001f));
                Assert.That(screenVector.Y, Is.EqualTo(resultY).Within(0.001f));
            }

            [TestCase(-1, 2, -1, -150f, 259.808f)]
            [TestCase(1, 0, -1, 150f, 86.602f)]
            [TestCase(1, 1, -2, 150f, 259.808f)]
            [TestCase(-3, 5, -2, -450f, 606.218f)]
            public void CanTransformFlatHex(int q, int r, int s, float resultX, float resultY)
            {
                Layout layout = Layout.Flat(new Vector2(100f), Vector2.Zero);
                var hex = new Hex(q, r, s);
                Vector2 screenVector = layout.HexToScreen(hex);
                Assert.That(screenVector.X, Is.EqualTo(resultX).Within(0.001f));
                Assert.That(screenVector.Y, Is.EqualTo(resultY).Within(0.001f));
            }

            [TestCase(5f, 10f, 12.990f, 15f)]
            [TestCase(-10f, 0.1f, -25.980f, 0.15f)]
            public void CanTransformWithSize(float sizeX, float sizeY, float resultX, float resultY)
            {
                Layout layout = Layout.Pointy(new Vector2(sizeX, sizeY), Vector2.Zero);
                var hex = new Hex(1, 1, -2);
                Vector2 screenVector = layout.HexToScreen(hex);
                Assert.That(screenVector.X, Is.EqualTo(resultX).Within(0.001f));
                Assert.That(screenVector.Y, Is.EqualTo(resultY).Within(0.001f));
            }

            [TestCase(5f, 10f, 264.808f, 160f)]
            [TestCase(-100f, -0.1f, 159.808f, 149.9f)]
            public void CanTransformWithOffset(float offsetX, float offsetY, float resultX,
                float resultY)
            {
                Layout layout =
                    Layout.Pointy(new Vector2(100f, 100f), new Vector2(offsetX, offsetY));
                var hex = new Hex(1, 1, -2);
                Vector2 screenVector = layout.HexToScreen(hex);
                Assert.That(screenVector.X, Is.EqualTo(resultX).Within(0.001f));
                Assert.That(screenVector.Y, Is.EqualTo(resultY).Within(0.001f));
            }
        }

        [TestFixture]
        public class WithScreenToHexConversion
        {
            [Test]
            public void CanTransformZero()
            {
                Layout layout = Layout.Pointy(new Vector2(100f), Vector2.Zero);

                Assert.That(layout.ScreenToHex(Vector2.Zero), Is.EqualTo(new HexF(0, 0, 0)));
            }

            [TestCase(0f, 300f, -1, 2, -1)]
            [TestCase(173.205f, 0f, 1, 0, -1)]
            [TestCase(259.808f, 150f, 1, 1, -2)]
            [TestCase(-86.602f, 750f, -3, 5, -2)]
            public void CanTransformPointyHex(float x, float y, int resultQ, int resultR,
                int resultS)
            {
                Layout layout = Layout.Pointy(new Vector2(100f), Vector2.Zero);
                HexF hex = layout.ScreenToHex(new Vector2(x, y));
                Assert.That(hex.Q, Is.EqualTo(resultQ).Within(0.001f));
                Assert.That(hex.R, Is.EqualTo(resultR).Within(0.001f));
                Assert.That(hex.S, Is.EqualTo(resultS).Within(0.001f));
            }

            [TestCase(-150f, 259.808f, -1, 2, -1)]
            [TestCase(150f, 86.602f, 1, 0, -1)]
            [TestCase(150f, 259.808f, 1, 1, -2)]
            [TestCase(-450f, 606.218f, -3, 5, -2)]
            public void CanTransformFlatHex(float x, float y, int resultQ, int resultR, int resultS)
            {
                Layout layout = Layout.Flat(new Vector2(100f), Vector2.Zero);
                HexF hex = layout.ScreenToHex(new Vector2(x, y));
                Assert.That(hex.Q, Is.EqualTo(resultQ).Within(0.001f));
                Assert.That(hex.R, Is.EqualTo(resultR).Within(0.001f));
                Assert.That(hex.S, Is.EqualTo(resultS).Within(0.001f));
            }

            [TestCase(5f, 10f, 1.821f, -1.333f, -0.488f)]
            [TestCase(-10f, 0.1f, 66.089f, -133.333f, 67.2439f)]
            public void CanTransformWithSize(float sizeX, float sizeY, float resultQ, float resultR,
                float resultS)
            {
                Layout layout = Layout.Pointy(new Vector2(sizeX, sizeY), Vector2.Zero);
                var vector = new Vector2(10, -20);
                HexF hex = layout.ScreenToHex(vector);
                Assert.That(hex.Q, Is.EqualTo(resultQ).Within(0.001f));
                Assert.That(hex.R, Is.EqualTo(resultR).Within(0.001f));
                Assert.That(hex.S, Is.EqualTo(resultS).Within(0.001f));
            }

            [TestCase(5f, 10f, 128.867f, -200, 71.132f)]
            [TestCase(-100f, -0.1f, 701.418f, -132.667f, -568.752f)]
            public void CanTransformWithOffset(float offsetX, float offsetY, float resultQ, float resultR,
                float resultS)
            {
                Layout layout = Layout.Pointy(new Vector2(0.1f, 0.1f), new Vector2(offsetX, offsetY));
                var vector = new Vector2(10, -20);
                HexF hex = layout.ScreenToHex(vector);
                Assert.That(hex.Q, Is.EqualTo(resultQ).Within(0.001f));
                Assert.That(hex.R, Is.EqualTo(resultR).Within(0.001f));
                Assert.That(hex.S, Is.EqualTo(resultS).Within(0.001f));
            }
        }
    }
}