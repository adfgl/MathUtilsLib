using LinearAlgebraLib;

namespace GeometryLib
{
    public readonly struct Rectangle
    {
        public readonly double minX, minY;
        public readonly double maxX, maxY;

        /// <summary>
        /// Inverted constructor to create a bounding box with invalid bounds.
        /// Useful for initializing a bounding box that will be expanded by a series of points.
        /// </summary>
        public Rectangle()
        {
            minX = minY = double.MaxValue;
            maxX = maxY = double.MinValue;
        }

        public Rectangle(double minX, double minY, double maxX, double maxY)
        {
            this.minX = minX;
            this.maxX = maxX;
            if (minX > maxX)
            {
                double t = minX;
                this.minX = maxX;
                this.maxX = t;
            }

            this.minY = minY;
            this.maxY = maxY;
            if (minY > maxY)
            {
                double t = minY;
                this.minY = maxY;
                this.maxY = t;
            }
        }

        public void Deconstruct(out Vec2 min, out Vec2 max)
        {
            min = new Vec2(minX, minY);
            max = new Vec2(maxX, maxY);
        }

        public static Rectangle FromPoints(IEnumerable<Vec2> points)
        {
            Rectangle box = new Rectangle();
            foreach (Vec2 point in points)
            {
                box = box.Expand(point.x, point.y);
            }
            return box;
        }

        public bool Contains(double x, double y)
        {
            return
                minX <= x && x <= maxX &&
                minY <= y && y <= maxY;
        }

        public bool Contains(Rectangle other)
        {
            return
                minX <= other.minX && other.maxX <= maxX &&
                minY <= other.minY && other.maxY <= maxY;
        }

        public Rectangle Expand(double x, double y)
        {
            return new Rectangle(
                x < minX ? x : minX,
                y < minY ? y : minY,
                x > maxX ? x : maxX,
                y > maxY ? y : maxY);
        }

        public Rectangle Expand(Rectangle other)
        {
            return new Rectangle(
                other.minX < minX ? other.minX : minX,
                other.minY < minY ? other.minY : minY,
                other.maxX > maxX ? other.maxX : maxX,
                other.maxY > maxY ? other.maxY : maxY);
        }

        public Rectangle Intersect(Rectangle other)
        {
            return new Rectangle(
                other.minX > minX ? other.minX : minX,
                other.minY > minY ? other.minY : minY,
                other.maxX < maxX ? other.maxX : maxX,
                other.maxY < maxY ? other.maxY : maxY);
        }
    }
}
