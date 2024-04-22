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

        [Fact]
        public void SquareLengthReturnsCorrectValue()
        {
            // Arrange
            Vec2 v = new Vec2(3, 4);

            // Act
            double actual = v.SquareLength();

            // Assert
            Assert.Equal(25, actual);
        }

        [Fact]
        public void LengthReturnsCorrectValue()
        {
            // Arrange
            Vec2 v = new Vec2(3, 4);

            // Act
            double actual = v.Length();

            // Assert
            Assert.Equal(5, actual);
        }

        [Fact]
        public void NormalizeReturnsCorrectValue()
        {
            // Arrange
            Vec2 v = new Vec2(3, 4);

            // Act
            Vec2 actual = v.Normalize();

            // Assert
            Assert.True(actual.normalized);
            Assert.Equal(0.6, actual.x, 6);
            Assert.Equal(0.8, actual.y, 6);
        }

        [Fact]
        public void NormalizeReturnsSameValueWhenAlreadyNormalized()
        {
            // Arrange
            /* intentionally far from normalized to test the method */
            Vec2 v = new Vec2(55, 144, 1, true); 

            // Act
            Vec2 actual = v.Normalize();

            // Assert
            Assert.True(actual.normalized);
            Assert.Equal(v.x, actual.x, 6);
            Assert.Equal(v.y, actual.y, 6);
        }

        [Fact]
        public void AbsReturnsCorrectValue()
        {
            // Arrange
            Vec2 v = new Vec2(-3, 4);

            // Act
            Vec2 actual = v.Abs();

            // Assert
            Assert.Equal(3, actual.x);
            Assert.Equal(4, actual.y);
        }

        [Fact]
        public void MinMaxReturnsCorrectValue()
        {
            // Arrange
            Vec2 v = new Vec2(3, 4);

            // Act
            double actualMin = v.Min();
            double actualMax = v.Max();

            // Assert
            Assert.Equal(v.x, actualMin);
            Assert.Equal(v.y, actualMax);
        }

        [Fact]
        public void DotReturnsCorrectValue()
        {
            // Arrange
            Vec2 v1 = new Vec2(3, 4);
            Vec2 v2 = new Vec2(5, 6);

            // Act
            double actual = v1.Dot(v2);

            // Assert
            Assert.Equal(39, actual);
        }
    }
}