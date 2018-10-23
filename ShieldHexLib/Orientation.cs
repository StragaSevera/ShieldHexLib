using System;
using System.Numerics;

namespace ShieldHexLib
{
    public struct Orientation
    {
        public Matrix3x2 Forward { get; }
        public Matrix3x2 Backward { get; }
        public float StartAngle { get; } // in multiplies of 60°

        public static Orientation Pointy = new Orientation(
            new Matrix3x2(
                (float) Math.Sqrt(3f), (float) Math.Sqrt(3f) / 2f,
                0f, 3f / 2f,
                0f, 0f
            ), 0.5f);

        public static Orientation Flat = new Orientation(
            new Matrix3x2(
                3f / 2f, 0f,
                (float) Math.Sqrt(3f) / 2f, (float) Math.Sqrt(3f),
                0f, 0f
            ), 0f);

        public Orientation(Matrix3x2 forward, float startAngle)
        {
            Forward = forward;
            StartAngle = startAngle;
            Matrix3x2.Invert(forward, out Matrix3x2 backward);
            Backward = backward;
        }
    }
}
