﻿using LinearAlgebraLib;
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
            
            // Act
            bool managedToInvert = m.Inverse(out Mat4 inverse);

            // Assert
            Assert.True(managedToInvert);

            double det = 1.0 / 108;

            Assert.Equal(det * 2, inverse.m11);
            Assert.Equal(det * 0, inverse.m12);
            Assert.Equal(det * -6, inverse.m13);
            Assert.Equal(det * 4, inverse.m14);

            Assert.Equal(det * 0, inverse.m21);
            Assert.Equal(det * -18, inverse.m22);
            Assert.Equal(det * 36, inverse.m23);
            Assert.Equal(det * -18, inverse.m24);

            Assert.Equal(det * -6, inverse.m31);
            Assert.Equal(det * 36, inverse.m32);
            Assert.Equal(det * -486, inverse.m33);
            Assert.Equal(det * 348, inverse.m34);

            Assert.Equal(det * 4, inverse.m41);
            Assert.Equal(det * -18, inverse.m42);
            Assert.Equal(det * 429, inverse.m43);
            Assert.Equal(det * -307, inverse.m44);
        }

        [Fact]
        public void TransposeReturnsCorrectValue()
        {
            Mat4 m = new Mat4(
                1, 5, 9, 13, 
                2, 6, 10, 14, 
                3, 7, 11, 15, 
                4, 8, 12, 16);

            Mat4 transposed = Mat4.Transpose(m);

            Assert.Equal(m.m11, transposed.m11);
            Assert.Equal(m.m12, transposed.m21);
            Assert.Equal(m.m13, transposed.m31);
            Assert.Equal(m.m14, transposed.m41);

            Assert.Equal(m.m21, transposed.m12);
            Assert.Equal(m.m22, transposed.m22);
            Assert.Equal(m.m23, transposed.m32);
            Assert.Equal(m.m24, transposed.m42);

            Assert.Equal(m.m31, transposed.m13);
            Assert.Equal(m.m32, transposed.m23);
            Assert.Equal(m.m33, transposed.m33);
            Assert.Equal(m.m34, transposed.m43);

            Assert.Equal(m.m41, transposed.m14);
            Assert.Equal(m.m42, transposed.m24);
            Assert.Equal(m.m43, transposed.m34);
            Assert.Equal(m.m44, transposed.m44);
        }

        [Fact]
        public void MatrixMultiplicationReturnsCorrectValue()
        {
            Mat4 a = new Mat4(
                    1, 2, 3, 4, 
                    5, 6, 7, 8, 
                    9, 10, 11, 12, 
                    13, 14, 15, 16);


            Mat4 c = Mat4.Multiply(a, a);

            Assert.Equal(90, c.m11);
            Assert.Equal(100, c.m12);
            Assert.Equal(110, c.m13);
            Assert.Equal(120, c.m14);

            Assert.Equal(202, c.m21);
            Assert.Equal(228, c.m22);
            Assert.Equal(254, c.m23);
            Assert.Equal(280, c.m24);

            Assert.Equal(314, c.m31);
            Assert.Equal(356, c.m32);
            Assert.Equal(398, c.m33);
            Assert.Equal(440, c.m34);

            Assert.Equal(426, c.m41);
            Assert.Equal(484, c.m42);
            Assert.Equal(542, c.m43);
            Assert.Equal(600, c.m44);
        }

        [Fact]
        public void VectorMultiplicationReturnsCorrectValue()
        {
            Mat4 a = new Mat4(
                1, 2, 3, 4, 
                5, 6, 7, 8, 
                9, 10, 11, 12, 
                13, 14, 15, 16);

            Vec3 v = new Vec3(1, 2, 3, 4);

            Vec3 actual = Mat4.Multiply(a, v);

            Assert.Equal(30, actual.x);
            Assert.Equal(70, actual.y);
            Assert.Equal(110, actual.z);
            Assert.Equal(150, actual.w);
        }

        [Fact]
        public void ScalarMultiplicationReturnsCorrectValue()
        {
            Mat4 a = new Mat4(
                1, 2, 3, 4, 
                5, 6, 7, 8, 
                9, 10, 11, 12, 
                13, 14, 15, 16);

            Mat4 actual = Mat4.Multiply(a, 2);

            Assert.Equal(2, actual.m11);
            Assert.Equal(4, actual.m12);
            Assert.Equal(6, actual.m13);
            Assert.Equal(8, actual.m14);

            Assert.Equal(10, actual.m21);
            Assert.Equal(12, actual.m22);
            Assert.Equal(14, actual.m23);
            Assert.Equal(16, actual.m24);

            Assert.Equal(18, actual.m31);
            Assert.Equal(20, actual.m32);
            Assert.Equal(22, actual.m33);
            Assert.Equal(24, actual.m34);

            Assert.Equal(26, actual.m41);
            Assert.Equal(28, actual.m42);
            Assert.Equal(30, actual.m43);
            Assert.Equal(32, actual.m44);
        }
    }
}
