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
    }
}
