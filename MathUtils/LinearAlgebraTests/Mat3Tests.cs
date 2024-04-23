using LinearAlgebraLib;

namespace LinearAlgebraTests
{
    public class Mat3Tests
    {
        /* https://www.wolframalpha.com/input?i2d=true&i=%7B%7B1%2C2%2C3%7D%2C%7B0%2C1%2C4%7D%2C%7B5%2C6%2C0%7D%7D */

        [Fact]
        public void IdentityReturnsCorrectValue()
        {
            Mat3 m = Mat3.Identity;
            Assert.Equal(1, m.m11);
            Assert.Equal(0, m.m12);
            Assert.Equal(0, m.m13);

            Assert.Equal(0, m.m21);
            Assert.Equal(1, m.m22);
            Assert.Equal(0, m.m23);

            Assert.Equal(0, m.m31);
            Assert.Equal(0, m.m32);
            Assert.Equal(1, m.m33);
        }

        [Fact]
        public void GetThrowsIndexOutOfRangeException()
        {
            var m = new Mat3(1, 2, 3, 4, 5, 6, 7, 8, 9);
            Assert.Throws<IndexOutOfRangeException>(() => m.Get(3, 0));
            Assert.Throws<IndexOutOfRangeException>(() => m.Get(0, 3));
            Assert.Throws<IndexOutOfRangeException>(() => m.Get(-1, 0));
            Assert.Throws<IndexOutOfRangeException>(() => m.Get(0, -1));
        }

        [Fact]
        public void GetReturnsCorrectCellValue()
        {
            Mat3 m = new Mat3(1, 2, 3, 4, 5, 6, 7, 8, 9);
            Assert.Equal(m.m11, m.Get(0, 0));
            Assert.Equal(m.m12, m.Get(0, 1));
            Assert.Equal(m.m13, m.Get(0, 2));

            Assert.Equal(m.m21, m.Get(1, 0));
            Assert.Equal(m.m22, m.Get(1, 1));
            Assert.Equal(m.m23, m.Get(1, 2));

            Assert.Equal(m.m31, m.Get(2, 0));
            Assert.Equal(m.m32, m.Get(2, 1));
            Assert.Equal(m.m33, m.Get(2, 2));
        }

        [Fact]
        public void DeterminantReturnsCorrectValue()
        {
            Mat3 m = new Mat3(1, 2, 3, 4, 5, 6, 7, 8, 9);
            Assert.Equal(0, m.Determinant());
        }

        [Fact]
        public void InverseDoesNotThrow()
        {
            // Arrange
            Mat3 m = new Mat3(1, 2, 3, 4, 5, 6, 7, 8, 9);
            
            // Act
            bool managedToInvert = m.Inverse(out Mat3 inverse);

            // Assert
            Assert.False(managedToInvert);
        }

        [Fact]
        public void InverseReturnsCorrectValue()
        {
            // Arrange
            Mat3 m = new Mat3(1, 2, 3, 0, 1, 4, 5, 6, 0);
            Mat3 expected = new Mat3(-24, 18, 5, 20, -15, -4, -5, 4, 1);
            
            // Act
            bool managedToInvert = m.Inverse(out Mat3 actual);
            
            // Assert
            Assert.True(managedToInvert);
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    Assert.Equal(expected[r, c], actual[r, c], 6);
                }
            }
        }

        [Fact]
        public void TransposeReturnsCorrectValue()
        {
            // Arrange
            Mat3 m = new Mat3(1, 2, 3, 0, 1, 4, 5, 6, 0);
            Mat3 expected = new Mat3(1, 0, 5, 2, 1, 6, 3, 4, 0);

            // Act
            Mat3 actual = m.Transpose();

            // Assert
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    Assert.Equal(expected[r, c], actual[r, c], 6);
                }
            }
        }

        [Fact]
        public void MultiplyVectorReturnsCorrectValue()
        {
            // Arrange
            Mat3 m1 = new Mat3(1, 2, 3, 4, 5, 6, 7, 8, 9);
            Mat3 m2 = new Mat3(9, 8, 7, 6, 5, 4, 3, 2, 1);
            Mat3 expected = new Mat3(30, 24, 18, 84, 69, 54, 138, 114, 90);

            // Act
            Mat3 actual = Mat3.Multiply(m1, m2);
            Mat3 actualOperator = m1 * m2;

            // Assert
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    Assert.Equal(expected[r, c], actual[r, c], 6);
                    Assert.Equal(expected[r, c], actualOperator[r, c], 6);
                }
            }
        }

        [Fact]
        public void MultiplyVectorReturnsCorrectValue2()
        {
            // Arrange
            Mat3 m = new Mat3(1, 2, 3, 4, 5, 6, 7, 8, 9);
            Vec2 v = new Vec2(9, 8, 7);
            Vec2 expected = new Vec2(46, 118, 190);

            // Act
            Vec2 actual = Mat3.Multiply(m, v);
            Vec2 actualOperator1 = m * v;
            Vec2 actualOperator2 = v * m;

            // Assert
            for (int i = 0; i < 3; i++)
            {
                Assert.Equal(expected[i], actual[i], 6);
                Assert.Equal(expected[i], actualOperator1[i], 6);
                Assert.Equal(expected[i], actualOperator2[i], 6);
            }
        }

        [Fact]
        public void MultiplyByScalarReturnsCorrectValue()
        {
            // Arrange
            Mat3 m = new Mat3(1, 2, 3, 4, 5, 6, 7, 8, 9);
            double scalar = 2;
            Mat3 expected = new Mat3(2, 4, 6, 8, 10, 12, 14, 16, 18);

            // Act
            Mat3 actual = Mat3.Multiply(m, scalar);
            Mat3 actualOperator1 = m * scalar;
            Mat3 actualOperator2 = scalar * m;

            // Assert
            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 3; c++)
                {
                    Assert.Equal(expected[r, c], actual[r, c], 6);
                    Assert.Equal(expected[r, c], actualOperator1[r, c], 6);
                    Assert.Equal(expected[r, c], actualOperator2[r, c], 6);
                }
            }
        }
    }
}
