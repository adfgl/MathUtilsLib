namespace LinearAlgebraLib
{
    public readonly struct BoundingBox2
    {
        public readonly double minX, minY;
        public readonly double maxX, maxY;

        public BoundingBox2(double minX, double minY, double maxX, double maxY)
        {
            this.minX = minX;
            this.minY = minY;
            this.maxX = maxX;
            this.maxY = maxY;
        }

        public void Deconstruct(out double minX, out double minY, out double maxX, out double maxY)
        {
            minX = this.minX;
            minY = this.minY;
            maxX = this.maxX;
            maxY = this.maxY;
        }
    }
}
