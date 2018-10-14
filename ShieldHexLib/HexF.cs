using System;
using System.Collections.Generic;
using System.Numerics;
using ShieldHexLib.Vendor;

namespace ShieldHexLib
{
    public class HexF : ValueObject
    {
        private readonly Vector3 _coords;

        public float Q => _coords.X;
        public float R => _coords.Y;
        public float S => _coords.Z;

        public HexF(float q, float r, float s)
        {
            _coords = new Vector3(q, r, s);
        }

        public HexF(float q, float r)
        {
            _coords = new Vector3(q, r, -q - r);
        }

        public HexF(double q, double r, double s) : this((float) q, (float) r, (float) s) { }

        public HexF(double q, double r) : this((float) q, (float) r) { }

        public bool IsValid()
        {
            return Math.Abs(Q + R + S) < 0.00001;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Q;
            yield return R;
            yield return S;
        }
    }
}
