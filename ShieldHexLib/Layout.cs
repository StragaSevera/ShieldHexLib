using System.Numerics;

namespace ShieldHexLib
{
    public class Layout
    {
        public Orientation Orientation { get; }
        public Vector2 Size { get; }
        public Vector2 Origin { get; }
        private readonly Matrix3x2 _forward;
        private readonly Matrix3x2 _backward;

        public Layout(Orientation orientation, Vector2 size, Vector2 origin)
        {
            Orientation = orientation;
            Size = size;
            Origin = origin;
            _forward = CalculateForwardMatrix(Orientation.Forward);
            _backward = CalculateBackwardMatrix(Orientation.Backward);
        }

        public static Layout Pointy(Vector2 size, Vector2 origin)
        {
            return new Layout(Orientation.Pointy, size, origin);
        }

        public static Layout Flat(Vector2 size, Vector2 origin)
        {
            return new Layout(Orientation.Flat, size, origin);
        }

        public Vector2 HexToScreen(Hex hex)
        {
            return Vector2.Transform(hex.Vector(), _forward);
        }

        public HexF ScreenToHex(Vector2 point)
        {
            return new HexF(Vector2.Transform(point, _backward));
        }

        private Matrix3x2 CalculateForwardMatrix(Matrix3x2 orientation)
        {
            Matrix3x2 scale = Matrix3x2.CreateScale(Size);
            Matrix3x2 translation = Matrix3x2.CreateTranslation(Origin);
            Matrix3x2 transformed = orientation * scale * translation;
            return transformed;
        }

        private Matrix3x2 CalculateBackwardMatrix(Matrix3x2 orientation)
        {
            Matrix3x2 scale = Matrix3x2.CreateScale(new Vector2(1f / Size.X, 1f / Size.Y));
            Matrix3x2 translation = Matrix3x2.CreateTranslation(-Origin);
            Matrix3x2 transformed = translation * scale * orientation;
            return transformed;
        }
    }
}