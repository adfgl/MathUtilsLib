namespace LinearAlgebraLib
{
    public readonly struct BoundingBox2
    {
        public readonly double minX, minY;
        public readonly double maxX, maxY;

        /// <summary>
        /// Inverted constructor to create a bounding box with invalid bounds.
        /// Useful for initializing a bounding box that will be expanded by a series of points.
        /// </summary>
        public BoundingBox2()
        {
            minX = double.MaxValue;
            minY = double.MaxValue;
            maxX = double.MinValue;
            maxY = double.MinValue;
        }

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

        public bool Contains(BoundingBox2 other)
        {
            return
                minX <= other.minX && other.maxX <= maxX &&
                minY <= other.minY && other.maxY <= maxY;
        }

        public BoundingBox2 Expand(double x, double y)
        {
            return new BoundingBox2(
                x < this.minX ? x : this.minX,
                y < this.minY ? y : this.minY,
                x > this.maxX ? x : this.maxX,
                y > this.maxY ? y : this.maxY);
        }
    }
}
