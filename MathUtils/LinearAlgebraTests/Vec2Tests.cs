using LinearAlgebraLib;

namespace LinearAlgebraTests
{
    public class Vec2Tests
    {
        [Fact]
        public void DeconstructorWorksCorrectly()
        {
            // Arrange
            Vec2 v = new Vec2(12, 34, -2);

            // Act
            v.Deconstruct(out double actualX, out double actualY);

            // Assert
            Assert.Equal(v.x, actualX);
            Assert.Equal(v.y, actualY);
        }

        [Fact]
        public void GetValueThrowsErrorWhenIndexIsOutOfRange()
        {
            // Arrange
            Vec2 v = new Vec2(12, 34, -2);

            // Act
            Action act = () => v.Get(4);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }

        [Fact]
        public void GetValueReturnsCorrectValue()
        {
            // Arrange
            Vec2 v = new Vec2(12, 34, -2);

            // Act
            double actualX = v.Get(0);
            double actualY = v.Get(1);
            double actualW = v.Get(2);

            // Assert
            Assert.Equal(v.x, actualX);
            Assert.Equal(v.y, actualY);
            Assert.Equal(v.w, actualW);
        }

        [Theory]
        [InlineData(0, 0, false)]
        [InlineData(double.NaN, 0, true)]
        [InlineData(0, double.NaN, true)]
        [InlineData(double.NaN, double.NaN, true)]
        public void IsNaNReturnsCorrectValue(double x, double y, bool expected)
        {
            // Arrange
            Vec2 v = new Vec2(x, y);

            // Act
            bool actual = v.IsNaN();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}