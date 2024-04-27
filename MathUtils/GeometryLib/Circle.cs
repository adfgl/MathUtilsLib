using LinearAlgebraLib;

namespace GeometryLib
{
    public readonly struct Circle
    {
        public readonly double cx, cy;
        public readonly double radius;

        public Circle(double cx, double cy, double radius)
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

        public static Circle FromPoints(IEnumerable<Vec2> points, Vec2 center)
        {
            Circle box = new Circle(center.x, center.y, 0);
            foreach (Vec2 point in points)
            {
                box = box.Expand(point.x, point.y);
            }
            return box;
        }

        public bool Contains(double x, double y)
        {
            double dx = x - cx;
            double dy = y - cy;
            return dx * dx + dy * dy <= radius * radius;
        }

        public bool Contains(Circle other)
        {
            double dx = other.cx - cx;
            double dy = other.cy - cy;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            return distance + other.radius <= radius;
        }

        public Circle Expand(double x, double y)
        {
            double dx = x - cx;
            double dy = y - cy;

            double squareDistance = dx * dx + dy * dy;
            if (squareDistance <= radius * radius) return this;
            return new Circle(cx, cy, Math.Sqrt(squareDistance));
        }   

        public Circle Expand(Circle other)
        {
            double dx = other.cx - cx;
            double dy = other.cy - cy;

            double squareDistance = dx * dx + dy * dy;
            if (squareDistance <= radius * radius) return this;
            return new Circle(cx, cy, Math.Sqrt(squareDistance));
        }

        public bool Intersects(Circle other)
        {
            double dx = other.cx - cx;
            double dy = other.cy - cy;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            return distance <= radius + other.radius;
        }
    }
}
