using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearAlgebraLib
{
    public readonly struct Mat4
    {
        public readonly double m11, m12, m13, m14;
        public readonly double m21, m22, m23, m24;
        public readonly double m31, m32, m33, m34;
        public readonly double m41, m42, m43, m44;

        public static Mat4 Identity => new(
            1, 0, 0, 0,
            0, 1, 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1);

        public Mat4(
              double m11, double m12, double m13, double m14,
              double m21, double m22, double m23, double m24,
              double m31, double m32, double m33, double m34,
              double m41, double m42, double m43, double m44)
        {
            this.m11 = m11; this.m12 = m12; this.m13 = m13; this.m14 = m14;
            this.m21 = m21; this.m22 = m22; this.m23 = m23; this.m24 = m24;
            this.m31 = m31; this.m32 = m32; this.m33 = m33; this.m34 = m34;
            this.m41 = m41; this.m42 = m42; this.m43 = m43; this.m44 = m44;
        }
    }
}
