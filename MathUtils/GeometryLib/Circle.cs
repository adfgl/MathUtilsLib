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

        public Circle(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            // https://stackoverflow.com/questions/62488827/solving-equation-to-find-center-point-of-circle-from-3-points
            var x12 = x1 - x2;
            var x13 = x1 - x3;

            var y12 = y1 - y2;
            var y13 = y1 - y3;

            var y31 = y3 - y1;
            var y21 = y2 - y1;

            var x31 = x3 - x1;
            var x21 = x2 - x1;

            var sx13 = x1 * x1 - x3 * x3;
            var sy13 = y1 * y1 - y3 * y3;
            var sx21 = x2 * x2 - x1 * x1;
            var sy21 = y2 * y2 - y1 * y1;

            var f = (sx13 * x12 + sy13 * x12 + sx21 * x13 + sy21 * x13) / (2 * (y31 * x12 - y21 * x13));
            var g = (sx13 * y12 + sy13 * y12 + sx21 * y13 + sy21 * y13) / (2 * (x31 * y12 - x21 * y13));
            var c = -(x1 * x1) - y1 * y1 - 2 * g * x1 - 2 * f * y1;

            this.cx = -g;
            this.cy = -f;
            this.radius = Math.Sqrt(cx * cx + cy * cy - c);
        }

        public void Deconstruct(out Vec2 center, out double radius)
        {
            center = new Vec2(cx, cy);
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
