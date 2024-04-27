using LinearAlgebraLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearAlgebraTests
{
    public class Mat4Tests
    {
        [Fact]
        public void IdentityReturnsCorrectValue()
        {
            Mat4 m = Mat4.Identity;
            Assert.Equal(1, m.m11);
            Assert.Equal(0, m.m12);
            Assert.Equal(0, m.m13);
            Assert.Equal(0, m.m14);

            Assert.Equal(0, m.m21);
            Assert.Equal(1, m.m22);
            Assert.Equal(0, m.m23);
            Assert.Equal(0, m.m24);

            Assert.Equal(0, m.m31);
            Assert.Equal(0, m.m32);
            Assert.Equal(1, m.m33);
            Assert.Equal(0, m.m34);

            Assert.Equal(0, m.m41);
            Assert.Equal(0, m.m42);
            Assert.Equal(0, m.m43);
            Assert.Equal(1, m.m44);
        }

        [Fact]
        public void GetThrowsIndexOutOfRangeException()
        {
            var m = new Mat4(
                1, 2, 3, 4, 
                5, 6, 7, 8, 
                9, 10, 11, 12, 
                13, 14, 15, 16);

            Assert.Throws<IndexOutOfRangeException>(() => m.Get(4, 0));
            Assert.Throws<IndexOutOfRangeException>(() => m.Get(0, 4));
            Assert.Throws<IndexOutOfRangeException>(() => m.Get(-1, 0));
            Assert.Throws<IndexOutOfRangeException>(() => m.Get(0, -1));
        }

        [Fact]
        public void GetReturnsCorrectCellValue()
        {
            Mat4 m = new Mat4(
                1, 2, 3, 4, 
                5, 6, 7, 8, 
                9, 10, 11, 12, 
                13, 14, 15, 16);

            Assert.Equal(m.m11, m.Get(0, 0));
            Assert.Equal(m.m12, m.Get(0, 1));
            Assert.Equal(m.m13, m.Get(0, 2));
            Assert.Equal(m.m14, m.Get(0, 3));

            Assert.Equal(m.m21, m.Get(1, 0));
            Assert.Equal(m.m22, m.Get(1, 1));
            Assert.Equal(m.m23, m.Get(1, 2));
            Assert.Equal(m.m24, m.Get(1, 3));

            Assert.Equal(m.m31, m.Get(2, 0));
            Assert.Equal(m.m32, m.Get(2, 1));
            Assert.Equal(m.m33, m.Get(2, 2));
            Assert.Equal(m.m34, m.Get(2, 3));

            Assert.Equal(m.m41, m.Get(3, 0));
            Assert.Equal(m.m42, m.Get(3, 1));
            Assert.Equal(m.m43, m.Get(3, 2));
            Assert.Equal(m.m44, m.Get(3, 3));
        }

        [Fact]
        public void DeterminantReturnsCorrectValue()
        {
            Mat4 m = new Mat4(
                55, 2, 3, 4, 
                5, 556, 7, 8, 
                9, 10, 11, 12, 
                13, 14, 15, 16);

            Assert.Equal(-118800, m.Determinant());
        }

        [Fact]
        public void InverseDoesNotThrow()
        {
            // Arrange
            Mat4 m = new Mat4(
                1, 2, 3, 4, 
                5, 6, 7, 8, 
                9, 10, 11, 12, 
                13, 14, 15, 16);
            
            // Act
            bool managedToInvert = m.Inverse(out Mat4 inverse);

            // Assert
            Assert.False(managedToInvert);
        }

        [Fact]
        public void InverseReturnsCorrectValue()
        {
            // Arrange
            Mat4 m = new Mat4(
                55, 2, 3, 4, 
                5, 0, 7, 8, 
                9, 10, 11, 12, 
                13, 14, 15, 16);
            
            Mat4 expected = new Mat4(
                0.01851851852, 0.0, -0.05555555556, 0.03703703704,
                0.0, -0.1666666667, 0.3333333333, -0.1666666667,
                -0.05555555556, 0.3333333333, -4.500000000, 3.222222222,
                0.03703703704, -0.1666666667, 3.972222222, -2.842592593);

            // Act
            bool managedToInvert = m.Inverse(out Mat4 actual);

            // Assert
            Assert.True(managedToInvert);

            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    Assert.Equal(expected[r, c], actual[r, c], 6);
                }
            }
        }

        [Fact]
        public void TransposeReturnsCorrectValue()
        {
            // Arrange
            Mat4 m = new Mat4(
                1, 5, 9, 13, 
                2, 6, 10, 14, 
                3, 7, 11, 15, 
                4, 8, 12, 16);

            Mat4 expected = new Mat4(
                    1, 2, 3, 4, 
                    5, 6, 7, 8, 
                    9, 10, 11, 12, 
                    13, 14, 15, 16);

            // Act
            Mat4 actual = Mat4.Transpose(m);

            // Assert
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    Assert.Equal(expected[r, c], actual[r, c], 6);
                }
            }
        }

        [Fact]
        public void MatrixMultiplicationReturnsCorrectValue()
        {
            // Arrange
            Mat4 a = new Mat4(
                1, 2, 3, 4, 
                5, 6, 7, 8, 
                9, 10, 11, 12, 
                13, 14, 15, 16);

            Mat4 expected = new Mat4(
                90, 100, 110, 120, 
                202, 228, 254, 280, 
                314, 356, 398, 440, 
                426, 484, 542, 600);

            // Act
            Mat4 actual = Mat4.Multiply(a, a);

            // Assert
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    Assert.Equal(expected[r, c], actual[r, c], 6);
                }
            }
        }

        [Fact]
        public void VectorMultiplicationReturnsCorrectValue()
        {
            // Arrange
            Mat4 a = new Mat4(
                1, 2, 3, 4, 
                5, 6, 7, 8, 
                9, 10, 11, 12, 
                13, 14, 15, 16);

            Vec3 v = new Vec3(1, 2, 3, 4);

            // Act
            Vec3 actual = Mat4.Multiply(a, v);

            // Assert
            Assert.Equal(30, actual.x);
            Assert.Equal(70, actual.y);
            Assert.Equal(110, actual.z);
            Assert.Equal(150, actual.w);
        }

        [Fact]
        public void ScalarMultiplicationReturnsCorrectValue()
        {
            // Arrange
            Mat4 a = new Mat4(
                1, 2, 3, 4, 
                5, 6, 7, 8, 
                9, 10, 11, 12, 
                13, 14, 15, 16);

            Mat4 expected = new Mat4(
                2, 4, 6, 8, 
                10, 12, 14, 16, 
                18, 20, 22, 24, 
                26, 28, 30, 32);

            // Act
            Mat4 actual = Mat4.Multiply(a, 2);

            // Assert
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    Assert.Equal(expected[r, c], actual[r, c], 6);
                }
            }
        }

        [Fact]
        public void ScalarDivisionReturnsCorrectValue()
        {
            // Arrange
            Mat4 a = new Mat4(
                2, 4, 6, 8, 
                10, 12, 14, 16, 
                18, 20, 22, 24, 
                26, 28, 30, 32);

            Mat4 expected = new Mat4(
                1, 2, 3, 4, 
                5, 6, 7, 8, 
                9, 10, 11, 12, 
                13, 14, 15, 16);

            // Act
            Mat4 actual = Mat4.Divide(a, 2);

            // Assert
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    Assert.Equal(expected[r, c], actual[r, c], 6);
                }
            }
        }
    }
}
