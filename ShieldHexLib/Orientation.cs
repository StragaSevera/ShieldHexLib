using System;
using System.Numerics;

namespace ShieldHexLib
{
    public struct Orientation
    {
        public Matrix3x2 Forward { get; }
        public Matrix3x2 Backward { get; }
        public float StartAngle { get; } // in multiplies of 60°

        private static readonly Matrix3x2 _pointyMatrix = new Matrix3x2(
            (float) Math.Sqrt(3f), 0f,
            (float) Math.Sqrt(3f) / 2f, 3f / 2f,
            0f, 0f
        );
        private static readonly Matrix3x2 _flatMatrix = new Matrix3x2(
            3f / 2f, (float) Math.Sqrt(3f) / 2f,
            0f, (float) Math.Sqrt(3f),
            0f, 0f
        );
        public static readonly Orientation Pointy = new Orientation(_pointyMatrix, 0.5f);
        public static readonly Orientation Flat = new Orientation(_flatMatrix, 0f);


        public Orientation(Matrix3x2 forward, float startAngle)
        {
            Forward = forward;
            StartAngle = startAngle;
            Matrix3x2.Invert(forward, out Matrix3x2 backward);
            Backward = backward;
        }
    }
}
