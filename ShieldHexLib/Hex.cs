using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ShieldHexLib
{
    public struct Hex : IEnumerable<int>
    {
        public int Q { get; }
        public int R { get; }
        public int S { get; }

        public int this[int i]
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

        private static readonly Hex[] _directions =
        {
            new Hex(1, 0, -1), new Hex(1, -1, 0),
            new Hex(0, -1, 1), new Hex(-1, 0, 1),
            new Hex(-1, 1, 0), new Hex(0, 1, -1)
        };

        public Hex(int q, int r, int s)
        {
            if (!IsValid(q, r, s))
                throw new ArgumentException("Invalid coords!");

            Q = q;
            R = r;
            S = s;
        }

        public Hex(int q, int r) : this(q, r, -q - r) { }

        public Vector2 Vector()
        {
            return new Vector2(Q, R);
        }

        public IEnumerator<int> GetEnumerator()
        {
            yield return Q;
            yield return R;
            yield return S;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Equals(Hex other)
        {
            return Q == other.Q && R == other.R && S == other.S;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Hex other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Q;
                hashCode = (hashCode * 397) ^ R;
                hashCode = (hashCode * 397) ^ S;
                return hashCode;
            }
        }

        public static bool operator ==(Hex a, Hex b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Hex a, Hex b)
        {
            return !(a == b);
        }

        private static bool IsValid(int q, int r, int s)
        {
            return q + r + s == 0;
        }

        public static Hex operator +(Hex a, Hex b)
        {
            return new Hex(a.Q + b.Q, a.R + b.R, a.S + b.S);
        }

        public static Hex operator -(Hex a, Hex b)
        {
            return new Hex(a.Q - b.Q, a.R - b.R, a.S - b.S);
        }

        // Multiplying by a scalar

        public static Hex operator *(Hex a, int k)
        {
            return new Hex(a.Q * k, a.R * k, a.S * k);
        }

        public static Hex operator *(int k, Hex a)
        {
            return new Hex(a.Q * k, a.R * k, a.S * k);
        }

        public int DistanceFromOrigin()
        {
            return this.Select(Math.Abs).Max();
        }

        public int Distance(Hex hex)
        {
            return (this - hex).DistanceFromOrigin();
        }

        public static Hex Direction(int dir)
        {
            int index = dir % _directions.Length;
            if (index < 0)
            {
                index += _directions.Length;
            }
            return _directions[index];
        }

        public Hex Neighbor(int dir)
        {
            return this + _directions[dir];
        }
    }
}
