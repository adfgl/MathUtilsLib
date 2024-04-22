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
            Action act = () => v.GetValue(4);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }

        [Fact]
        public void GetValueReturnsCorrectValue()
        {
            // Arrange
            Vec2 v = new Vec2(12, 34, -2);

            // Act
            double actualX = v.GetValue(0);
            double actualY = v.GetValue(1);
            double actualW = v.GetValue(2);

            // Assert
            Assert.Equal(v.x, actualX);
            Assert.Equal(v.y, actualY);
            Assert.Equal(v.w, actualW);
        }
    }
}