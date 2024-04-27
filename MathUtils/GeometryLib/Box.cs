using LinearAlgebraLib;

namespace GeometryLib
{
    public readonly struct Box
    {
        public readonly double minX, minY, minZ;
        public readonly double maxX, maxY, maxZ;

        /// <summary>
        /// Inverted constructor to create a bounding box with invalid bounds.
        /// Useful for initializing a bounding box that will be expanded by a series of points.
        /// </summary>
        public Box()
        {
            minX = minY = minZ = double.MaxValue;
            maxX = maxY = maxZ = double.MinValue;
        }

        public Box(double minX, double minY, double minZ, double maxX, double maxY, double maxZ)
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

            this.minZ = minZ;
            this.maxZ = maxZ;
            if (minZ > maxZ)
            {
                double t = minZ;
                this.minZ = maxZ;
                this.maxZ = t;
            }
        }

        public void Deconstruct(out Vec3 min, out Vec3 max)
        {
            min = new Vec3(minX, minY, minZ);
            max = new Vec3(maxX, maxY, maxZ);
        }

        public static Box FromPoints(IEnumerable<Vec3> points)
        {
            Box box = new Box();
            foreach (Vec3 point in points)
            {
                box = box.Expand(point.x, point.y, point.z);
            }
            return box;
        }

        public bool Contains(double x, double y, double z)
        {
            return
                 minX <= x && x <= maxX &&
                 minY <= y && y <= maxY &&
                 minZ <= z && z <= maxZ;
        }

        public bool Contains(Box other)
        {
            return
                minX <= other.minX &&
                minY <= other.minY &&
                minZ <= other.minZ &&
                maxX >= other.maxX &&
                maxY >= other.maxY &&
                maxZ >= other.maxZ;
        }

        public Box Expand(double x, double y, double z)
        {
            return new Box(
                x < minX ? x : minX,
                y < minY ? y : minY,
                z < minZ ? z : minZ,

                x > maxX ? x : maxX,
                y > maxY ? y : maxY,
                z > maxZ ? z : maxZ);
        }

        public Box Expand(Box other)
        {
            return new Box(
              other.minX < minX ? other.minX : minX,
              other.minY < minY ? other.minY : minY,
              other.minZ < minZ ? other.minZ : minZ,

              other.maxX > maxX ? other.maxX : maxX,
              other.maxY > maxY ? other.maxY : maxY,
              other.maxZ > maxZ ? other.maxZ : maxZ);
        }

        public Box Intersect(Box other)
        {
            return new Box(
                other.minX > minX ? other.minX : minX,
                other.minY > minY ? other.minY : minY,
                other.minZ > minZ ? other.minZ : minZ,

                other.maxX < maxX ? other.maxX : maxX,
                other.maxY < maxY ? other.maxY : maxY,
                other.maxZ < maxZ ? other.maxZ : maxZ);
        }
    }
}
