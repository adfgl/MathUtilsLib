namespace LinearAlgebraLib.Geometry
{
    public readonly struct Sphere
    {
        public readonly double cx, cy, cz;
        public readonly double radius;

        public Sphere(double cx, double cy, double cz, double radius)
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

        public static Sphere FromPoints(IEnumerable<Vec3> points, Vec3 center)
        {
            Sphere box = new Sphere(center.x, center.y, center.z, 0);
            foreach (Vec3 point in points)
            {
                box = box.Expand(point.x, point.y, point.z);
            }
            return box;
        }

        public bool Contains(double x, double y, double z)
        {
            double dx = x - cx;
            double dy = y - cy;
            double dz = z - cz;
            return dx * dx + dy * dy + dz * dz <= radius * radius;
        }

        public bool Contains(Sphere other)
        {
            double dx = other.cx - cx;
            double dy = other.cy - cy;
            double dz = other.cz - cz;
            double distance = Math.Sqrt(dx * dx + dy * dy + dz * dz);
            return distance + other.radius <= radius;
        }

        public Sphere Expand(double x, double y, double z)
        {
            double dx = x - cx;
            double dy = y - cy;
            double dz = z - cz;

            double squareDistance = dx * dx + dy * dy + dz * dz;
            if (squareDistance <= radius * radius) return this;
            return new Sphere(cx, cy, cz, Math.Sqrt(squareDistance));
        }

        public Sphere Expand(Sphere other)
        {
            double dx = other.cx - cx;
            double dy = other.cy - cy;
            double dz = other.cz - cz;

            double squareDistance = dx * dx + dy * dy + dz * dz;
            double distance = Math.Sqrt(squareDistance);
            double newRadius = Math.Max(radius, distance + other.radius);
            return new Sphere(cx, cy, cz, newRadius);
        }

        public bool Intersects(Sphere other)
        {
            double dx = other.cx - cx;
            double dy = other.cy - cy;
            double dz = other.cz - cz;
            double distance = Math.Sqrt(dx * dx + dy * dy + dz * dz);

            if (distance + other.radius < radius) return false;
            if (distance > radius + other.radius) return false;
            return true;
        }
    }
}
