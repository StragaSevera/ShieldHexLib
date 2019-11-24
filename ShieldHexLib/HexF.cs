using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace ShieldHexLib
{
    public struct HexF : IEnumerable<float>, IVectorizable
    {
        private readonly Vector3 _coords;

        public float Q => _coords.X;
        public float R => _coords.Y;
        public float S => _coords.Z;

        public float this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return Q;
                    case 1:
                        return R;
                    case 2:
                        return S;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }
        // ReSharper disable once MemberCanBeMadeStatic.Global
        public int Length => 3;
        public const float Tolerance = 0.00001f;

        public HexF(float q, float r, float s)
        {
            if (!IsValid(q, r, s))
                throw new ArgumentException("Invalid coords!");
            _coords = new Vector3(q, r, s);
        }

        public HexF(float q, float r)
        {
            _coords = new Vector3(q, r, -q - r);
        }

        public HexF(Vector2 vector) : this(vector.X, vector.Y) { }

        public HexF(Hex hex) : this(hex.Q, hex.R) { }

        public HexF(double q, double r, double s) : this((float) q, (float) r, (float) s) { }

        public HexF(double q, double r) : this((float) q, (float) r) { }

        public IEnumerator<float> GetEnumerator()
        {
            yield return Q;
            yield return R;
            yield return S;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Vector2 Vector()
        {
            return new Vector2(Q, R);
        }

        private bool Equals(HexF other)
        {
            // TODO: Add comparsion within tolerance
            return _coords.Equals(other._coords);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is HexF other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _coords.GetHashCode();
        }

        public override string ToString()
        {
            return $"Hex({Q}, {R}, {S})";
        }

        public static bool operator ==(HexF a, HexF b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(HexF a, HexF b)
        {
            return !(a == b);
        }
        
        public static bool IsValid(float q, float r, float s)
        {
            return Math.Abs(q + r + s) < Tolerance;
        }
    }
}
