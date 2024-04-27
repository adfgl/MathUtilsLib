﻿namespace LinearAlgebraLib
{
    public readonly struct BoundingSphere
    {
        public readonly double cx, cy, cz;
        public readonly double radius;

        public BoundingSphere(double cx, double cy, double cz, double radius)
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

        public static BoundingSphere FromPoints(IEnumerable<Vec3> points, Vec3 center)
        {
            BoundingSphere box = new BoundingSphere(center.x, center.y, center.z, 0);
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

        public bool Contains(BoundingSphere other)
        {
            double dx = other.cx - cx;
            double dy = other.cy - cy;
            double dz = other.cz - cz;
            double distance = Math.Sqrt(dx * dx + dy * dy + dz * dz);
            return distance + other.radius <= radius;
        }

        public BoundingSphere Expand(double x, double y, double z)
        {
            double dx = x - cx;
            double dy = y - cy;
            double dz = z - cz;

            double squareDistance = dx * dx + dy * dy + dz * dz;
            if (squareDistance <= radius * radius) return this;
            return new BoundingSphere(cx, cy, cz, Math.Sqrt(squareDistance));
        }

        public BoundingSphere Expand(BoundingSphere other)
        {
            double dx = other.cx - cx;
            double dy = other.cy - cy;
            double dz = other.cz - cz;

            double squareDistance = dx * dx + dy * dy + dz * dz;
            double distance = Math.Sqrt(squareDistance);
            double newRadius = Math.Max(radius, distance + other.radius);
            return new BoundingSphere(cx, cy, cz, newRadius);
        }

        public bool Intersects(BoundingSphere other)
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
