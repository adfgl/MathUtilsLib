using System.Diagnostics;

namespace LinearAlgebraLib
{
    [DebuggerDisplay("{x} {y}")]
    public readonly struct Vec2
    {
        public readonly double x, y, w;

        public static Vec2 Zero => new Vec2(0, 0);
        public static Vec2 NaN => new Vec2(double.NaN, double.NaN, 1);
        public static Vec2 UnitX => new Vec2(1, 0);
        public static Vec2 UnitY => new Vec2(0, 1);

        public Vec2(double x, double y, double w = 1)
        {
            this.x = x;
            this.y = y;
            this.w = w;
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

        public bool IsNaN()
        {
            return double.IsNaN(x) || double.IsNaN(y);
        }

        public double this[int index] => Get(index);
    }
}
