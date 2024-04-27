using LinearAlgebraLib;

namespace LinearAlgebraTests
{
    public class VecExTests
    {
        [Fact]
        public void Vec2BetweenReturnsCorrectValue()
        {
            // Arrange
            Vec2 a = new Vec2(0, 0);
            Vec2 b = new Vec2(80, 80);
            Vec2 expected = new Vec2(40, 40);

            // Act
            Vec2 actual = a.Between(b);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Vec3BetweenReturnsCorrectValue()
        {
            // Arrange
            Vec3 a = new Vec3(0, 0, 0);
            Vec3 b = new Vec3(80, 80, 80);
            Vec3 expected = new Vec3(40, 40, 40);

            // Act
            Vec3 actual = a.Between(b);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 0, 1, 0, true)]
        [InlineData(1, 1, 0, 1, false)]
        public void Vec2ParallelReturnsCorrectValue(double ax, double ay, double bx, double by, bool expected)
        {
            // Arrange
            Vec2 a = new Vec2(ax, ay);
            Vec2 b = new Vec2(bx, by);

            // Act
            bool actual = a.Parallel(b, 0.0001);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 0, 0, 1, 0, 0, true)]
        [InlineData(0, 1, 0, 1, 0, 0, false)]
        public void Vec3ParallelReturnsCorrectValue(double ax, double ay, double az, double bx, double by, double bz, bool expected)
        {
            // Arrange
            Vec3 a = new Vec3(ax, ay, az);
            Vec3 b = new Vec3(bx, by, bz);

            // Act
            bool actual = a.Parallel(b, 0.0001);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1, 0, 0, 1, true)]
        [InlineData(1, 1, 0, 1, false)]
        public void PerpendicularReturnsCorrectValue(double ax, double ay, double bx, double by, bool expected)
        {
            // Arrange
            Vec2 a = new Vec2(ax, ay);
            Vec2 b = new Vec2(bx, by);

            // Act
            bool actual = a.Perpendicular(b, 0.0001);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DistanceReturnsCorrectValue()
        {
            // Arrange
            Vec2 a = new Vec2(0, 0);
            Vec2 b = new Vec2(3, 4);
            double expected = 5;

            // Act
            double actual = a.Distance(b);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
