using System.Diagnostics;

namespace LinearAlgebraLib
{
    [DebuggerDisplay("{x}, {y}, {z}")]
    public readonly struct Vec3
    {
        public readonly double x, y, z, w;
        public readonly bool normalized;

        public static Vec3 Zero => new Vec3(0, 0, 0, 1, true);
        public static Vec3 NaN => new Vec3(double.NaN, double.NaN, double.NaN);
        public static Vec3 UnitX => new Vec3(1, 0, 0, 1, true);
        public static Vec3 UnitY => new Vec3(0, 1, 0, 1, true);
        public static Vec3 UnitZ => new Vec3(0, 0, 1, 1, true);

        public Vec3(double x, double y, double z, double w = 1, bool normalized = false)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.z = z;
            this.normalized = normalized;
        }

        public void Deconstruct(out double x, out double y, out double z)
        {
            x = this.x;
            y = this.y;
            z = this.z;
        }

        public double Get(int index)
        {
            switch (index)
            {
                case 0: return x;
                case 1: return y;
                case 2: return z;
                case 3: return w;

                default:
                    throw new ArgumentOutOfRangeException(nameof(index));
            }
        }

        public double this[int index] => Get(index);

        public bool IsNaN() => double.IsNaN(x) || double.IsNaN(y) || double.IsNaN(z);

        public double SquareLength() => x * x + y * y + z * z;

        public double Length() => Math.Sqrt(x * x + y * y + z * z);

        public Vec3 Normalize()
        {
            if (normalized) return this;
            double length = Length();
            return new Vec3(x / length, y / length, z / length, 1, true);
        }

        public Vec3 Abs()
        {
            return new Vec3(Math.Abs(x), Math.Abs(y), Math.Abs(z), 1, normalized);
        }
    }
}
