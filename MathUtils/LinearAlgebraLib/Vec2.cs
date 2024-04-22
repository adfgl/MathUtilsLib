namespace LinearAlgebraLib
{
    public readonly struct Vec2
    {
        public readonly double x, y, w;
        public readonly bool normalized;

        public Vec2(double x, double y, double w, bool normalized)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.normalized = normalized;
        }
    }
}
