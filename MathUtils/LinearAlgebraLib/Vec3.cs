using System.Diagnostics;

namespace LinearAlgebraLib
{
    [DebuggerDisplay("{x}, {y}, {z}")]
    public readonly struct Vec3
    {
        public readonly double x, y, z, w;
        public readonly bool normalized;

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
    }
}
