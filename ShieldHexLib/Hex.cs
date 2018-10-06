using System;
using System.Numerics;

namespace ShieldHexLib
{
    public class Hex
    {
        private readonly Vector3 _coords;

        public bool IsFractional { get; }
        public int Q => (int) Math.Round(_coords.X);
        public int R => (int) Math.Round(_coords.Y);
        public int S => (int) Math.Round(_coords.Z);
        public float Qf => _coords.X;
        public float Rf => _coords.Y;
        public float Sf => _coords.Z;


        public Hex(int q, int r, int s)
        {
            _coords = new Vector3(q, r, s);
            IsFractional = false;
        }

        public Hex(int q, int r)
        {
            _coords = new Vector3(q, r, -q - r);
            IsFractional = false;
        }

        public Hex(float q, float r, float s)
        {
            _coords = new Vector3(q, r, s);
            IsFractional = true;
        }

        public Hex(float q, float r)
        {
            _coords = new Vector3(q, r, -q - r);
            IsFractional = true;
        }

        public Hex(double q, double r, double s) : this((float) q, (float) r, (float) s)
        {
        }

        public Hex(double q, double r) : this((float) q, (float) r)
        {
        }
    }
}
