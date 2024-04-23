using LinearAlgebraLib;

namespace LinearAlgebraTests
{
    public class Mat3Tests
    {
        /* https://www.wolframalpha.com/input?i2d=true&i=%7B%7B1%2C2%2C3%7D%2C%7B0%2C1%2C4%7D%2C%7B5%2C6%2C0%7D%7D */

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
    }
}
