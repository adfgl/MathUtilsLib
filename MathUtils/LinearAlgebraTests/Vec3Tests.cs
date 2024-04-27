using LinearAlgebraLib;

namespace LinearAlgebraTests
{
    public class Vec3Tests
    {
        [Fact]
        public void DeconstructWorksCorrectly()
        {
            // Arrange
            Vec3 v = new Vec3(12, 34, -2);

            // Act
            v.Deconstruct(out double actualX, out double actualY, out double actualZ);

            // Assert
            Assert.Equal(v.x, actualX);
            Assert.Equal(v.y, actualY);
            Assert.Equal(v.z, actualZ);
        }

        [Fact]
        public void GetValueThrowsErrorWhenIndexIsOutOfRange()
        {
            // Arrange
            Vec3 v = new Vec3(12, 34, -2);

            // Act
            Action act = () => v.Get(5);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }

        [Fact]
        public void GetValueReturnsCorrectValue()
        {
            // Arrange
            Vec3 v = new Vec3(12, 34, -2, 33);

            // Act
            double actualX = v.Get(0);
            double actualY = v.Get(1);
            double actualZ = v.Get(2);
            double actualW = v.Get(3);

            // Assert
            Assert.Equal(v.x, actualX);
            Assert.Equal(v.y, actualY);
            Assert.Equal(v.z, actualZ);
            Assert.Equal(v.w, actualW);
        }

        [Theory]
        [InlineData(0, 0, 0, false)]
        [InlineData(double.NaN, 0, 0, true)]
        [InlineData(0, double.NaN, 0, true)]
        [InlineData(0, 0, double.NaN, true)]
        [InlineData(double.NaN, double.NaN, double.NaN, true)]
        public void IsNaNReturnsCorrectValue(double x, double y, double z, bool expected)
        {
            // Arrange
            Vec3 v = new Vec3(x, y, z);

            // Act
            bool actual = v.IsNaN();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SquareLengthReturnsCorrectValue()
        {
            // Arrange
            Vec3 v = new Vec3(3, 4, 5);

            // Act
            double actual = v.SquareLength();

            // Assert
            Assert.Equal(50, actual);
        }
    }
}
