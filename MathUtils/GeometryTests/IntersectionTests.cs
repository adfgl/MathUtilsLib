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
        public void PlaneRayIntersectionWorksCorrectly(double px, double py, double pz, double nx, double ny, double nz, bool expected, double x, double y, double z)
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
    }
}
