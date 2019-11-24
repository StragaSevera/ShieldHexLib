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
            [TestFixture]
            public class WithIntegralHex
            {
                [Test]
                public void CanTransformZero()
                {
                    Layout layout = Layout.Pointy(new Vector2(100f), Vector2.Zero);
                    var hex = new Hex(0, 0, 0);
                    Assert.That(layout.HexToScreen(hex), Is.EqualTo(Vector2.Zero));
                }

                [TestCase(-1, 2, -1, 0f, 3f)]
                [TestCase(1, 0, -1, 1.732f, 0f)]
                [TestCase(1, 1, -2, 2.598f, 1.5f)]
                [TestCase(-3, 5, -2, -0.866f, 7.5f)]
                public void CanTransformPointyHex(int q, int r, int s, float resultX, float resultY)
                {
                    Layout layout = Layout.Pointy(Vector2.One, Vector2.Zero);
                    var hex = new Hex(q, r, s);
                    Vector2 screenVector = layout.HexToScreen(hex);
                    Assert.That(screenVector.X, Is.EqualTo(resultX).Within(0.001f));
                    Assert.That(screenVector.Y, Is.EqualTo(resultY).Within(0.001f));
                }

                [TestCase(-1, 2, -1, -1.5f, 2.5981f)]
                [TestCase(1, 0, -1, 1.5f, 0.866f)]
                [TestCase(1, 1, -2, 1.5f, 2.5981f)]
                [TestCase(-3, 5, -2, -4.5f, 6.0622f)]
                public void CanTransformFlatHex(int q, int r, int s, float resultX, float resultY)
                {
                    Layout layout = Layout.Flat(Vector2.One, Vector2.Zero);
                    var hex = new Hex(q, r, s);
                    Vector2 screenVector = layout.HexToScreen(hex);
                    Assert.That(screenVector.X, Is.EqualTo(resultX).Within(0.001f));
                    Assert.That(screenVector.Y, Is.EqualTo(resultY).Within(0.001f));
                }

                [TestCase(5f, 10f, 12.990f, 15f)]
                [TestCase(-10f, 0.1f, -25.980f, 0.15f)]
                public void CanTransformWithSize(float sizeX, float sizeY, float resultX,
                    float resultY)
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
            public class WithFractionalHex
            {
                [Test]
                public void CanTransformZero()
                {
                    Layout layout = Layout.Pointy(new Vector2(100f), Vector2.Zero);
                    var hexF = new HexF(0f, 0f, 0f);
                    Assert.That(layout.HexToScreen(hexF), Is.EqualTo(Vector2.Zero));
                }

                [TestCase(-1f, 2f, -1f, 0f, 3f)]
                [TestCase(1.1f, 0f, -1.1f, 1.905f, 0f)]
                [TestCase(1.5f, 0.8f, -2.3f, 3.291f, 1.2f)]
                [TestCase(-3.9f, 5.3f, -1.4f, -2.165f, 7.95f)]
                public void CanTransformPointyHex(float q, float r, float s, float resultX, float
                resultY)
                {
                    Layout layout = Layout.Pointy(Vector2.One, Vector2.Zero);
                    var hexF = new HexF(q, r, s);
                    Vector2 screenVector = layout.HexToScreen(hexF);
                    Assert.That(screenVector.X, Is.EqualTo(resultX).Within(0.001f));
                    Assert.That(screenVector.Y, Is.EqualTo(resultY).Within(0.001f));
                }

                [TestCase(-1f, 2f, -1f, -1.5f, 2.598f)]
                [TestCase(1.1f, 0f, -1.1f, 1.65f, 0.95263f)]
                [TestCase(1.5f, 0.8f, -2.3f, 2.25f, 2.6847f)]
                [TestCase(-3.9f, 5.3f, -1.4f, -5.85f, 5.802f)]
                public void CanTransformFlatHex(float q, float r, float s, float resultX, float
                    resultY)
                {
                    Layout layout = Layout.Flat(Vector2.One, Vector2.Zero);
                    var hexF = new HexF(q, r, s);
                    Vector2 screenVector = layout.HexToScreen(hexF);
                    Assert.That(screenVector.X, Is.EqualTo(resultX).Within(0.001f));
                    Assert.That(screenVector.Y, Is.EqualTo(resultY).Within(0.001f));
                }

                [TestCase(5f, 10f, 13.423f, 13.5f)]
                [TestCase(-10f, 0.1f, -26.847f, 0.135f)]
                public void CanTransformWithSize(float sizeX, float sizeY, float resultX,
                    float resultY)
                {
                    Layout layout = Layout.Pointy(new Vector2(sizeX, sizeY), Vector2.Zero);
                    var hexF = new HexF(1.1f, 0.9f, -2f);
                    Vector2 screenVector = layout.HexToScreen(hexF);
                    Assert.That(screenVector.X, Is.EqualTo(resultX).Within(0.001f));
                    Assert.That(screenVector.Y, Is.EqualTo(resultY).Within(0.001f));
                }

                [TestCase(5f, 10f, 273.468f, 145f)]
                [TestCase(-100f, -0.1f, 168.468f, 134.9f)]
                public void CanTransformWithOffset(float offsetX, float offsetY, float resultX,
                    float resultY)
                {
                    Layout layout =
                        Layout.Pointy(new Vector2(100f, 100f), new Vector2(offsetX, offsetY));
                    var hexF = new HexF(1.1f, 0.9f, -2f);
                    Vector2 screenVector = layout.HexToScreen(hexF);
                    Assert.That(screenVector.X, Is.EqualTo(resultX).Within(0.001f));
                    Assert.That(screenVector.Y, Is.EqualTo(resultY).Within(0.001f));
                }
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
            public void CanTransformWithOffset(float offsetX, float offsetY, float resultQ,
                float resultR,
                float resultS)
            {
                Layout layout =
                    Layout.Pointy(new Vector2(0.1f, 0.1f), new Vector2(offsetX, offsetY));
                var vector = new Vector2(10, -20);
                HexF hex = layout.ScreenToHex(vector);
                Assert.That(hex.Q, Is.EqualTo(resultQ).Within(0.001f));
                Assert.That(hex.R, Is.EqualTo(resultR).Within(0.001f));
                Assert.That(hex.S, Is.EqualTo(resultS).Within(0.001f));
            }
        }
    }
}