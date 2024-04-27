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
    }
}
