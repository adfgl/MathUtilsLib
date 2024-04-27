namespace LinearAlgebraLib
{
    public readonly struct BoundingSphere
    {
        public readonly double cx, cy;
        public readonly double radius;

        public BoundingSphere(double cx, double cy, double radius)
        {
            this.cx = cx;
            this.cy = cy;
            this.radius = radius;
        }

        public void Deconstruct(out double cx, out double cy, out double radius)
        {
            cx = this.cx;
            cy = this.cy;
            radius = this.radius;
        }
    }
}
