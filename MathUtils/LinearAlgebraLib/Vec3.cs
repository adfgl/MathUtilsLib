using System.Diagnostics;

namespace LinearAlgebraLib
{
    [DebuggerDisplay("{x}, {y}, {z}")]
    public readonly struct Vec3 : ILinearAlgebraObject
    {
        public readonly double x, y, z, w;
        public readonly bool normalized;

        public int Rows => 3;
        public int Columns => 1;

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

        public double Get(int row, int col = 0)
        {
            switch (row)
            {
                case 0: return x;
                case 1: return y;
                case 2: return z;
                case 3: return w;

                default:
                    throw new ArgumentOutOfRangeException(nameof(row));
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

        public double Dot(Vec3 other) => x * other.x + y * other.y + z * other.z;

        public Vec3 Cross(Vec3 other)
        {
            return new Vec3(
                y * other.z - z * other.y,
                z * other.x - x * other.z,
                x * other.y - y * other.x
            );
        }

        /// <summary>
        /// Verifies whether <see cref="x"/>, <see cref="y"/> and <see cref="z"/> are <i><b>exactly</b></i> equal to the corresponding values of <paramref name="other"/>.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Vec3 other) => x == other.x && y == other.y && z == other.z;

        public static Vec3 operator +(Vec3 a, Vec3 b) => new Vec3(a.x + b.x, a.y + b.y, a.z + b.z);

        public static Vec3 operator -(Vec3 a, Vec3 b) => new Vec3(a.x - b.x, a.y - b.y, a.z - b.z);
        public static Vec3 operator -(Vec3 v) => new Vec3(-v.x, -v.y, -v.z);

        public static Vec3 operator *(Vec3 v, double scalar) => new Vec3(v.x * scalar, v.y * scalar, v.z * scalar);
        public static Vec3 operator *(double scalar, Vec3 v) => new Vec3(v.x * scalar, v.y * scalar, v.z * scalar);

        public static Vec3 operator /(Vec3 v, double scalar) => new Vec3(v.x / scalar, v.y / scalar, v.z / scalar);

        /// <summary>
        /// Uses <see cref="Equals(Vec3)"/> to compare the two vectors. Verifies whether components <see cref="x"/>, <see cref="y"/> and <see cref="z"/> are <i><b>exactly</b></i> equal. 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Vec3 a, Vec3 b) => a.Equals(b);

        /// <summary>
        /// Uses <see cref="Equals(Vec3)"/> to compare the two vectors. Verifies whether <see cref="x"/>, <see cref="y"/> and <see cref="z"/> are <i><b>exactly</b></i> unequal.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Vec3 a, Vec3 b) => false == a.Equals(b);

        public override string ToString()
        {
            return $"{x} {y} {z}";
        }
    }
}
