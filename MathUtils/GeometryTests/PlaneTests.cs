using GeometryLib;
using LinearAlgebraLib;

namespace GeometryTests
{
    public class PlaneTests
    {
        [Theory]
        [InlineData(200, 400, -50, 350)] // in front of plane
        [InlineData(200, 0, -50, -50)] // behind plane
        [InlineData(200, 50, -50, 0)] // on plane
        public void SignedDistanceToReturnsCorrectValue(double x, double y, double z, double expected)
        {
            // Arrange
            Plane plane = new Plane(new Vec3(0, 1, 0), 50);
            Vec3 point = new Vec3(x, y, z);

            // Act
            double actual = plane.SignedDistanceTo(point);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void IsValidReturnsCorrectValue()
        {
            // Arrange
            Plane valid = new Plane(new Vec3(0, 1, 0), 50);
            Plane invalid = new Plane(new Vec3(0, 0, 0), 0);

            // Act
            bool actualValid = valid.IsValid();
            bool actualInvalid = invalid.IsValid();

            // Assert
            Assert.True(actualValid);
            Assert.False(actualInvalid);
        }

        [Fact]
        public void FlipReturnsCorrectValue()
        {
            // Arrange
            Plane plane = new Plane(new Vec3(0, 1, 0), 50);
            Plane expected = new Plane(new Vec3(0, -1, 0), -50);

            // Act
            Plane flipped = plane.Flip();

            // Assert
            Assert.Equal(expected.normal, flipped.normal);
            Assert.Equal(expected.distanceToOrigin, flipped.distanceToOrigin);
        }
    }
}
