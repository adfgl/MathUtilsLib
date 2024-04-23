namespace LinearAlgebraLib
{
    public readonly struct BoundingBox2
    {
        public readonly double minX, minY;
        public readonly double maxX, maxY;

        public BoundingBox2(double minX, double minY, double maxX, double maxY)
        {
#if DEBUG
            if (minX > maxX || minY > maxY)
            {
                throw new ArgumentException("Minimum bounds must be less than or equal to maximum bounds.");
            }
#endif

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

        public bool Contains(double x, double y)
        {
            return 
                minX <= x && x <= maxX && 
                minY <= y && y <= maxY;
        }
    }
}
