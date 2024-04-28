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
            Ray ray = new Ray(new Vec3(px, py, pz), new Vec3(nx, ny, nz));

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
        //[InlineData(50, 50, 50, 0, 1, 0, false, 0, 0, 0)]
        //[InlineData(50, 50, 50, 0, -1, 0, false, 0, 0, 0)]
        //[InlineData(0, 50, 0, 1, 1, 0, true, 0, 50, 0)] // coincide

        // perpendicular
        [InlineData(-50, 50, 0, 1, 0, 0, true, 0, 50, 0)]
        [InlineData(50, 50, 50, -1, 0, 0, true, 0, 50, 0)]
        [InlineData(50, 50, -50, -1, 0, 0, true, 0, 50, 0)]
        public void RayRayIntersectionWorksCorrectly_UnitY_Zero_Ray(double px, double py, double pz, double nx, double ny, double nz, bool expected, double x, double y, double z)
        {
            // Arrange
            Ray ray1 = new Ray(new Vec3(0, 0, 0), Vec3.UnitY);
            Ray ray2 = new Ray(new Vec3(px, py, pz), new Vec3(nx, ny, nz));

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
    }
}
