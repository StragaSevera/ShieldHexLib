using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ShieldHexLib.Vendor;

namespace ShieldHexLib
{
    public class Hex : ValueObject, IEnumerable<int>
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

        public Hex(int q, int r, int s)
        {
            Q = q;
            R = r;
            S = s;
        }

        public Hex(int q, int r) : this(q, r, -q - r) { }

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

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (int x in this)
            {
                yield return x;
            }
        }

        public bool IsValid()
        {
            return Q + R + S == 0;
        }

        public static Hex operator +(Hex a, Hex b)
        {
            return new Hex(a.Q + b.Q, a.R + b.R, a.S + b.S);
        }

        public static Hex operator -(Hex a, Hex b)
        {
            return new Hex(a.Q - b.Q, a.R - b.R, a.S - b.S);
        }

        public static Hex operator *(Hex a, int k)
        {
            return new Hex(a.Q * k, a.R * k, a.S * k);
        }

        public static Hex operator *(int k, Hex a)
        {
            return new Hex(a.Q * k, a.R * k, a.S * k);
        }

        public int Length()
        {
            return this.Select(Math.Abs).Max();
        }
    }
}
