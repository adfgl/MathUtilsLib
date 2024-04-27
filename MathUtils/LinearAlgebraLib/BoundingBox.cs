namespace LinearAlgebraLib
{
    public readonly struct BoundingBox
    {
        public readonly double minX, minY;
        public readonly double maxX, maxY;

        /// <summary>
        /// Inverted constructor to create a bounding box with invalid bounds.
        /// Useful for initializing a bounding box that will be expanded by a series of points.
        /// </summary>
        public BoundingBox()
        {
            minX = double.MaxValue;
            minY = double.MaxValue;
            maxX = double.MinValue;
            maxY = double.MinValue;
        }

        public BoundingBox(double minX, double minY, double maxX, double maxY)
        {
            if (minX > maxX || minY > maxY)
            {
                throw new ArgumentException("Minimum bounds must be less than or equal to maximum bounds.");
            }

            this.minX = minX;
            this.minY = minY;
            this.maxX = maxX;
            this.maxY = maxY;
        }

        public static BoundingBox FromPoints(IEnumerable<Vec2> points)
        {
            BoundingBox box = new BoundingBox();
            foreach (Vec2 point in points)
            {
                box = box.Expand(point.x, point.y);
            }
            return box;
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

        public bool Contains(BoundingBox other)
        {
            return
                minX <= other.minX && other.maxX <= maxX &&
                minY <= other.minY && other.maxY <= maxY;
        }


        public BoundingBox Expand(double x, double y)
        {
            return new BoundingBox(
                x < this.minX ? x : this.minX,
                y < this.minY ? y : this.minY,
                x > this.maxX ? x : this.maxX,
                y > this.maxY ? y : this.maxY);
        }

        public BoundingBox Expand(BoundingBox other)
        {
            return new BoundingBox(
                other.minX < this.minX ? other.minX : this.minX,
                other.minY < this.minY ? other.minY : this.minY,
                other.maxX > this.maxX ? other.maxX : this.maxX,
                other.maxY > this.maxY ? other.maxY : this.maxY);
        }

        public BoundingBox Intersect(BoundingBox other)
        {
            return new BoundingBox(
                other.minX > this.minX ? other.minX : this.minX,
                other.minY > this.minY ? other.minY : this.minY,
                other.maxX < this.maxX ? other.maxX : this.maxX,
                other.maxY < this.maxY ? other.maxY : this.maxY);
        }
    }
}
