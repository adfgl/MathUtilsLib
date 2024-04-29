using GeometryLib;
using LinearAlgebraLib;
using static GeometryLib.ConvexHull;

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

        [Fact]
        public void GetValidFace_FaceIsInvertedCorrectlyWhenReferencePointIsInFrontOfFace()
        {
            // Arrange
            Vec3[] points =
            [
                new Vec3(0, 100, 0),
                new Vec3(-100, -100, 0),
                new Vec3(+100, -100, 0),
            ];
            Vec3 centroid = new Vec3(0, 0, 100);
            Face expected = new Face(2, 1, 0, new Plane(new Vec3(0, 0, -1), 0));

            // Act
            Face actual = ConvexHull.GetValidFace(points, centroid, 0, 1, 2, 0);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetValidFace_FaceIsNotInvertedWhenReferencePointIsBehindFace()
        {
            // Arrange
            Vec3[] points =
            [
                new Vec3(+100, -100, 0),
                new Vec3(-100, -100, 0),
                new Vec3(0, 100, 0),
            ];
            Vec3 centroid = new Vec3(0, 0, 100);
            Face expected = new Face(0, 1, 2, new Plane(new Vec3(0, 0, -1), 0));

            // Act
            Face actual = ConvexHull.GetValidFace(points, centroid, 0, 1, 2, 0);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
