using GeometryLib;
using LinearAlgebraLib;

namespace GeometryTests
{
    public class ConvexHullTests
    {
        [Fact]
        public void GetValidFace_ThrowsErrorWhenCentroidLiesOnFace()
        {
            // Arrange
            Vec3[] points =
            [
                new Vec3(0, 100, 0),
                new Vec3(-100, -100, 0),
                new Vec3(+100, -100, 0),
            ];

            // Act
            Action act = () => ConvexHull.GetValidFace(points, Vec3.Zero, 0, 1, 2, 0);

            // Assert
            Assert.Throws<InvalidOperationException>(act);
        }
    }
}
