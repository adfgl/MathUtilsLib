using LinearAlgebraLib;

namespace GeometryLib
{
    public static class PointsGenerator
    {
        public static Vec3[] RandomizedSphere(int numPoints, double radius)
        {
            List<Vec3> pointCloud = new List<Vec3>();
            Random random = new Random();

            for (int i = 0; i < numPoints; i++)
            {
                // Generate random spherical coordinates
                double theta = random.NextDouble() * 2 * Math.PI; // Azimuthal angle
                double phi = Math.Acos(2 * random.NextDouble() - 1); // Polar angle

                // Convert spherical coordinates to Cartesian coordinates
                double x = radius * Math.Sin(phi) * Math.Cos(theta);
                double y = radius * Math.Sin(phi) * Math.Sin(theta);
                double z = radius * Math.Cos(phi);

                pointCloud.Add(new Vec3(x, y, z));
            }

            return pointCloud.ToArray();
        }

        public static Vec3[] RandomPointCloud(int numPoints, double minX, double maxX, double minY, double maxY, double minZ, double maxZ)
        {
            Random random = new Random();
            Vec3[] pointCloud = new Vec3[numPoints];
            for (int i = 0; i < numPoints; i++)
            {
                double x = random.NextDouble() * (maxX - minX) + minX;
                double y = random.NextDouble() * (maxY - minY) + minY;
                double z = random.NextDouble() * (maxZ - minZ) + minZ;

                pointCloud[i] = new Vec3(x, y, z);
            }
            return pointCloud;
        }

        public static Vec3[] RandomPointCloud(int numPoints, double boxSize)
        {
            double min = -boxSize;
            double max = +boxSize;
            return RandomPointCloud(numPoints, min, max, min, max, min, max);
        }

        public static Vec3[] Cuboid(double width, double height, double length)
        {
            double halfWidth = width / 2;
            double halfHeight = height / 2;
            double halfLength = length / 2;

            // FRONT
            //    F +--------+ G
            //     /|       /|
            //    / |      / |
            // B +--------+ C|
            //   |  |     |  |
            //   |E +--------+ H
            //   | /      | /
            //   |/       |/
            // A +--------+ D


            // BACK
            //    C +--------+ B
            //     /|       /|
            //    / |      / |
            // G +--------+ F|
            //   |  |     |  |
            //   |D +--------+ A
            //   | /      | /
            //   |/       |/
            // H +--------+ E

            Vec3[] vertices = new Vec3[]
            {
                new Vec3(-halfWidth, -halfHeight, -halfLength), // Vertex A (0)
                new Vec3(-halfWidth, -halfHeight, halfLength),  // Vertex B (1)
                new Vec3(halfWidth, -halfHeight, halfLength),   // Vertex C (2)
                new Vec3(halfWidth, -halfHeight, -halfLength),  // Vertex D (3)
                new Vec3(-halfWidth, halfHeight, -halfLength),  // Vertex E (4)
                new Vec3(-halfWidth, halfHeight, halfLength),   // Vertex F (5)
                new Vec3(halfWidth, halfHeight, halfLength),    // Vertex G (6)
                new Vec3(halfWidth, halfHeight, -halfLength)    // Vertex H (7)
            };

            return vertices;
        }

        public static Vec3[] Cube(double size)
        {
            return Cuboid(size, size, size);
        }

        public static Vec3[] Sphere(int subdivisions, double radius)
        {
            Vec3[] vertices = new Vec3[(subdivisions + 1) * (subdivisions + 1)];

            int count = 0;
            for (int i = 0; i <= subdivisions; i++)
            {
                double phi = Math.PI * i / subdivisions;

                double sinPhi = Math.Sin(phi);
                double cosPhi = Math.Cos(phi);

                for (int j = 0; j <= subdivisions; j++)
                {
                    double theta = 2.0f * Math.PI * j / subdivisions;

                    double x = radius * sinPhi * Math.Cos(theta);
                    double y = radius * sinPhi * Math.Sin(theta);
                    double z = radius * cosPhi;

                    vertices[count++] = new Vec3(x, y, z);
                }
            }
            return vertices;
        }
    }
}
