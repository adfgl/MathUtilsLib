namespace LinearAlgebraLib
{
    public readonly struct BoundingBox3
    {
        public readonly double minX, minY, minZ;
        public readonly double maxX, maxY, maxZ;

        public BoundingBox3()
        {
            minX = minY = minZ = double.MaxValue;
            maxX = maxY = maxZ = double.MinValue;
        }

        public BoundingBox3(double minX, double minY, double minZ, double maxX, double maxY, double maxZ)
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

        public static BoundingBox3 FromPoints(IEnumerable<Vec3> points)
        {
            BoundingBox3 box = new BoundingBox3();
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

        public bool Contains(BoundingBox3 other)
        {
            return
                minX <= other.minX &&
                minY <= other.minY &&
                minZ <= other.minZ &&
                maxX >= other.maxX &&
                maxY >= other.maxY &&
                maxZ >= other.maxZ;
        }

        public BoundingBox3 Expand(double x, double y, double z)
        {
            return new BoundingBox3(
                x < this.minX ? x : this.minX,
                y < this.minY ? y : this.minY,
                z < this.minZ ? z : this.minZ,

                x > this.maxX ? x : this.maxX,
                y > this.maxY ? y : this.maxY,
                z > this.maxZ ? z : this.maxZ);
        }

        public BoundingBox3 Expand(BoundingBox3 other)
        {
            return new BoundingBox3(
              other.minX < this.minX ? other.minX : this.minX,
              other.minY < this.minY ? other.minY : this.minY,
              other.minZ < this.minZ ? other.minZ : this.minZ,

              other.maxX > this.maxX ? other.maxX : this.maxX,
              other.maxY > this.maxY ? other.maxY : this.maxY,
              other.maxZ > this.maxZ ? other.maxZ : this.maxZ);
        }

        public BoundingBox3 Intersect(BoundingBox3 other)
        {
            return new BoundingBox3(
                other.minX > this.minX ? other.minX : this.minX,
                other.minY > this.minY ? other.minY : this.minY,
                other.minZ > this.minZ ? other.minZ : this.minZ,

                other.maxX < this.maxX ? other.maxX : this.maxX,
                other.maxY < this.maxY ? other.maxY : this.maxY,
                other.maxZ < this.maxZ ? other.maxZ : this.maxZ);
        }
    }
}
