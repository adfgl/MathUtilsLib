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
    }
}
