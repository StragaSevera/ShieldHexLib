using System.Numerics;

namespace ShieldHexLib
{
    public class Layout
    {
        public Orientation Orientation { get; }
        public Vector2 Size { get; }
        public Vector2 Origin { get; }

        public Layout(Orientation orientation, Vector2 size, Vector2 origin)
        {
            Orientation = orientation;
            Size = size;
            Origin = origin;
        }

        public static Layout Pointy(Vector2 size, Vector2 origin)
        {
            return new Layout(Orientation.Pointy, size, origin);
        }
         
        public static Layout Flat(Vector2 size, Vector2 origin)
        {
            return new Layout(Orientation.Flat, size, origin);
        }
    }
}
