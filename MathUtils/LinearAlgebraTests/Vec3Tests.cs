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

        [Fact]
        public void LengthReturnsCorrectValue()
        {
            // Arrange
            Vec3 v = new Vec3(3, 4, 5);

            // Act
            double actual = v.Length();

            // Assert
            Assert.Equal(7.071, actual, 3);
        }

        [Fact]
        public void NormalizeReturnsCorrectValue()
        {
            // Arrange
            Vec3 v = new Vec3(3, 4, 5);

            // Act
            Vec3 actual = v.Normalize();

            // Assert
            Assert.Equal(0.424, actual.x, 3);
            Assert.Equal(0.566, actual.y, 3);
            Assert.Equal(0.707, actual.z, 3);
        }

        [Fact]
        public void NormalizeReturnsSameValueWhenAlreadyNormalized()
        {
            // Arrange
            Vec3 v = new Vec3(3, 4, 5, 1, true);

            // Act
            Vec3 actual = v.Normalize();

            // Assert
            Assert.Equal(v.x, actual.x);
            Assert.Equal(v.y, actual.y);
            Assert.Equal(v.z, actual.z);
        }

        [Fact]
        public void AbsReturnsCorrectValue()
        {
            // Arrange
            Vec3 v = new Vec3(-3, 4, -5);

            // Act
            Vec3 actual = v.Abs();

            // Assert
            Assert.Equal(3, actual.x);
            Assert.Equal(4, actual.y);
            Assert.Equal(5, actual.z);
        }

        [Fact]
        public void DotReturnsCorrectValue()
        {
            // Arrange
            Vec3 a = new Vec3(1, 2, 3);
            Vec3 b = new Vec3(4, 5, 6);

            // Act
            double actual = a.Dot(b);

            // Assert
            Assert.Equal(32, actual);
        }

        [Fact]
        public void CrossReturnsCorrectValue()
        {
            // Arrange
            Vec3 a = new Vec3(1, 2, 3);
            Vec3 b = new Vec3(4, 5, 6);

            // Act
            Vec3 actual = a.Cross(b);

            // Assert
            Assert.Equal(-3, actual.x);
            Assert.Equal(6, actual.y);
            Assert.Equal(-3, actual.z);
        }

        [Fact]
        public void SummationOperatorReturnsCorrectValue()
        {
            // Arrange
            Vec3 a = new Vec3(1, 2, 3);
            Vec3 b = new Vec3(4, 5, 6);

            // Act
            Vec3 actual = a + b;

            // Assert
            Assert.Equal(5, actual.x);
            Assert.Equal(7, actual.y);
            Assert.Equal(9, actual.z);
        }

        [Fact]
        public void SubtractionOperatorReturnsCorrectValue()
        {
            // Arrange
            Vec3 a = new Vec3(1, 2, 3);
            Vec3 b = new Vec3(4, 5, 6);

            // Act
            Vec3 actual = a - b;

            // Assert
            Assert.Equal(-3, actual.x);
            Assert.Equal(-3, actual.y);
            Assert.Equal(-3, actual.z);
        }

        [Fact]
        public void UnaryMinusOperatorReturnsCorrectValue()
        {
            // Arrange
            Vec3 a = new Vec3(1, 2, 3);

            // Act
            Vec3 actual = -a;

            // Assert
            Assert.Equal(-1, actual.x);
            Assert.Equal(-2, actual.y);
            Assert.Equal(-3, actual.z);
        }

        [Fact]
        public void ScalarMultiplicationOperatorReturnsCorrectValue()
        {
            // Arrange
            Vec3 a = new Vec3(1, 2, 3);
            double scalar = 2;

            // Act
            Vec3 actual1 = a * scalar;
            Vec3 actual2 = scalar * a;

            // Assert
            Assert.Equal(2, actual1.x);
            Assert.Equal(4, actual1.y);
            Assert.Equal(6, actual1.z);
      
            Assert.Equal(actual1.x, actual2.x);
            Assert.Equal(actual1.y, actual2.y);
            Assert.Equal(actual1.z, actual2.z);
        }
    }
}
