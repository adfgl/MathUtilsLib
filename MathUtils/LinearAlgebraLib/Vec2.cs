using System.Diagnostics;

namespace LinearAlgebraLib
{
    [DebuggerDisplay("{x} {y}")]
    public readonly struct Vec2
    {
        public readonly double x, y, w;
        public readonly bool normalized;

        public static Vec2 Zero => new Vec2(0, 0, 1, false);
        public static Vec2 NaN => new Vec2(double.NaN, double.NaN, 1);
        public static Vec2 UnitX => new Vec2(1, 0, 1, true);
        public static Vec2 UnitY => new Vec2(0, 1, 1, true);

        public Vec2(double x, double y, double w = 1, bool normalized = false)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.normalized = normalized;
        }

        public void Deconstruct(out double x, out double y)
        {
            x = this.x;
            y = this.y;
        }

        /// <summary>
        /// Returns the value of the vector at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public double Get(int index)
        {
            switch (index)
            {
                case 0: return x;
                case 1: return y;
                case 2: return w;

                default:
                    throw new ArgumentOutOfRangeException(nameof(index));
            }
        }
        public double this[int index] => Get(index);

        public bool IsNaN() => double.IsNaN(x) || double.IsNaN(y);

        public double SquareLength() => x * x + y * y;

        public double Length() => Math.Sqrt(x * x + y * y);

        public Vec2 Normalize()
        {
            if (normalized) return this;
            double length = Length();
            return new Vec2(x / length, y / length, 1, true);
        }

        public Vec2 Abs()
        {
            return new Vec2(Math.Abs(x), Math.Abs(y), 1, normalized);
        }

        public double Min() => (x < y) ? x : y;
        public double Max() => (x > y) ? x : y;

        public double Dot(Vec2 other) => x * other.x + y * other.y;
        public double Cross(Vec2 other) => x * other.y - y * other.x;

        public static Vec2 operator +(Vec2 a, Vec2 b)
        {
            return new Vec2(a.x + b.x, a.y + b.y);
        }

        public static Vec2 operator -(Vec2 a, Vec2 b)
        {
            return new Vec2(a.x - b.x, a.y - b.y);
        }

        public static Vec2 operator -(Vec2 v)
        {
            return new Vec2(-v.x, -v.y);
        }

        public static Vec2 operator *(Vec2 v, double scalar)
        {
            return new Vec2(v.x * scalar, v.y * scalar);
        }
        public static Vec2 operator *(double scalar, Vec2 v)
        {
            return new Vec2(v.x * scalar, v.y * scalar);
        }
    }
}
