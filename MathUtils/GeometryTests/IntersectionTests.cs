using GeometryLib;
using LinearAlgebraLib;

namespace GeometryTests
{
    public class IntersectionTests
    {
        [Theory]
        // behind plane
        [InlineData(50, 0, -50, 0, 1, 0, true, 50, 50, -50)] // same direction
        [InlineData(50, 0, -50, 0, -1, 0, false, 0, 0, 0)] // opposite direction
        [InlineData(50, 0, -50, 1, 0, 0, false, 0, 0, 0)] // parallel

        // in front of plane
        [InlineData(50, 100, -50, 0, -1, 0, true, 50, 50, -50)] // same direction
        [InlineData(50, 100, -50, 0, 1, 0, false, 0, 0, 0)] // opposite direction
        [InlineData(50, 100, -50, 0, 0, 1, false, 0, 0, 0)] // parallel

        // on plane
        [InlineData(50, 50, -50, 0, 1, 0, true, 50, 50, -50)] // same direction
        [InlineData(50, 50, -50, 0, -1, 0, true, 50, 50, -50)] // opposite direction
        [InlineData(50, 50, -50, 1, 0, 0, true, 50, 50, -50)] // parallel
        public void PlaneRayIntersectionWorksCorrectly_UnitY_50_Plane(double px, double py, double pz, double nx, double ny, double nz, bool expected, double x, double y, double z)
        {
            // Arrange
            Plane plane = new Plane(Vec3.UnitY, 50);
            Ray3 ray = new Ray3(new Vec3(px, py, pz), new Vec3(nx, ny, nz));

            // Act
            bool actual = plane.Intersect(ray, out Vec3 intersection);

            // Assert
            Assert.Equal(expected, actual);
            if (expected)
            {
                Assert.Equal(x, intersection.x, 6);
                Assert.Equal(y, intersection.y, 6);
                Assert.Equal(z, intersection.z, 6);
            }
        }

        [Theory]
        // parallel
        [InlineData(50, 50, 50, 0, 1, 0, false, 0, 0, 0)]
        [InlineData(50, 50, 50, 0, -1, 0, false, 0, 0, 0)]
        [InlineData(0, 50, 0, 1, 1, 0, true, 0, 50, 0)] // coincide

        // perpendicular
        [InlineData(-50, 50, 0, 1, 0, 0, true, 0, 50, 0)]
        [InlineData(50, 50, 50, -1, 0, 0, true, 0, 50, 0)]
        [InlineData(50, 50, -50, -1, 0, 0, true, 0, 50, 0)]
        public void RayRayIntersectionWorksCorrectly_UnitY_Zero_Ray(double px, double py, double pz, double nx, double ny, double nz, bool expected, double x, double y, double z)
        {
            // Arrange
            Ray3 ray1 = new Ray3(new Vec3(0, 0, 0), Vec3.UnitY);
            Ray3 ray2 = new Ray3(new Vec3(px, py, pz), new Vec3(nx, ny, nz));

            // Act
            bool actual = ray1.Intersect(ray2, out Vec3 intersection);

            // Assert
            Assert.Equal(expected, actual);
            if (expected)
            {
                Assert.Equal(x, intersection.x, 6);
                Assert.Equal(y, intersection.y, 6);
                Assert.Equal(z, intersection.z, 6);
            }
        }

        [Theory]
        [InlineData(50, 100, 50, -60, 200, 30, false, 0, 0, 0)] // both in front
        [InlineData(50, -100, 50, -60, -200, 30, false, 0, 0, 0)] // both behind
        [InlineData(50, 100, 50, 50, -200, 50, true, 50, 50, 50)] // one in front, one behind
        [InlineData(100, 200, 300, 0, 50, 0, true, 0, 50, 0)] // one in front, one directly on
        [InlineData(100, -200, 300, 0, 50, 0, true, 0, 50, 0)] // one behind, one directly on
        [InlineData(0, 50, 0, 100, 50, 300, true, 0, 50, 0)] // both directly on
        public void PlaneLineIntersection_UnitY_50_Plane(double x1, double y1, double z1, double x2, double y2, double z2, bool expected, double x, double y, double z)
        {
            // Arrange
            Plane plane = new Plane(Vec3.UnitY, 50);
            Vec3 start = new Vec3(x1, y1, z1);
            Vec3 end = new Vec3(x2, y2, z2);

            // Act
            bool actual = plane.Intersect(start, end, out Vec3 intersection);

            // Assert
            Assert.Equal(expected, actual);
            if (expected)
            {
                Assert.Equal(x, intersection.x, 6);
                Assert.Equal(y, intersection.y, 6);
                Assert.Equal(z, intersection.z, 6);
            }
        }
    }
}
