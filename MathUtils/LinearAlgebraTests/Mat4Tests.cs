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
    }
}
