namespace LinearAlgebraLib
{
    public readonly struct BoundingSphere
    {
        public readonly double cx, cy, cz;
        public readonly double radius;

        public BoundingSphere(double cx, double cy, double cz, double radius)
        {
            this.cx = cx;
            this.cy = cy;
            this.cz = cz;
            this.radius = radius;
        }

        public void Deconstruct(out double cx, out double cy, out double radius)
        {
            cx = this.cx;
            cy = this.cy;
            radius = this.radius;
        }

        public bool Contains(double x, double y, double z)
        {
            double dx = x - cx;
            double dy = y - cy;
            double dz = z - cz;
            return dx * dx + dy * dy + dz * dz <= radius * radius;
        }
    }
}
