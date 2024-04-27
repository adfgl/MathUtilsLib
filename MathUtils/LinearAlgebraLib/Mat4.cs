using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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

        public double Get(int row, int col)
        {
            return row switch
            {
                0 => col switch
                {
                    0 => m11,
                    1 => m12,
                    2 => m13,
                    3 => m14,
                    _ => throw new IndexOutOfRangeException($"Invalid column index {col}. Valid column indices are 0, 1, 2 and 3."),
                },
                1 => col switch
                {
                    0 => m21,
                    1 => m22,
                    2 => m23,
                    3 => m24,
                    _ => throw new IndexOutOfRangeException($"Invalid column index {col}. Valid column indices are 0, 1, 2 and 3."),
                },
                2 => col switch
                {
                    0 => m31,
                    1 => m32,
                    2 => m33,
                    3 => m34,
                    _ => throw new IndexOutOfRangeException($"Invalid column index {col}. Valid column indices are 0, 1, 2 and 3."),
                },
                3 => col switch
                {
                    0 => m41,
                    1 => m42,
                    2 => m43,
                    3 => m44,
                    _ => throw new IndexOutOfRangeException($"Invalid column index {col}. Valid column indices are 0, 1, 2 and 3."),
                },
                _ => throw new IndexOutOfRangeException($"Invalid row index {row}. Valid row indices are 0, 1, 2 and 3."),
            };
        }
        public double this[int row, int col] => Get(row, col);

        public double Determinant()
        {
            return
                m11 * (m22 * (m33 * m44 - m34 * m43) - m23 * (m32 * m44 - m34 * m42) + m24 * (m32 * m43 - m33 * m42)) -
                m12 * (m21 * (m33 * m44 - m34 * m43) - m23 * (m31 * m44 - m34 * m41) + m24 * (m31 * m43 - m33 * m41)) +
                m13 * (m21 * (m32 * m44 - m34 * m42) - m22 * (m31 * m44 - m34 * m41) + m24 * (m31 * m42 - m32 * m41)) -
                m14 * (m21 * (m32 * m43 - m33 * m42) - m22 * (m31 * m43 - m33 * m41) + m23 * (m31 * m42 - m32 * m41));
        }

        public bool Inverse(out Mat4 inverse)
        {
            double det = Determinant();
            if (det == 0)
            {
                inverse = new Mat4();
                return false;
            }

            det = 1 / det;

            inverse = new Mat4(
                det * +(m22 * (m33 * m44 - m34 * m43) - m23 * (m32 * m44 - m34 * m42) + m24 * (m32 * m43 - m33 * m42)),
                det * -(m12 * (m33 * m44 - m34 * m43) - m13 * (m32 * m44 - m34 * m42) + m14 * (m32 * m43 - m33 * m42)),
                det * +(m12 * (m23 * m44 - m24 * m43) - m13 * (m22 * m44 - m24 * m42) + m14 * (m22 * m43 - m23 * m42)),
                det * -(m12 * (m23 * m34 - m24 * m33) - m13 * (m22 * m34 - m24 * m32) + m14 * (m22 * m33 - m23 * m32)),

                det * -(m21 * (m33 * m44 - m34 * m43) - m23 * (m31 * m44 - m34 * m41) + m24 * (m31 * m43 - m33 * m41)),
                det * +(m11 * (m33 * m44 - m34 * m43) - m13 * (m31 * m44 - m34 * m41) + m14 * (m31 * m43 - m33 * m41)),
                det * -(m11 * (m23 * m44 - m24 * m43) - m13 * (m21 * m44 - m24 * m41) + m14 * (m21 * m43 - m23 * m41)),
                det * +(m11 * (m23 * m34 - m24 * m33) - m13 * (m21 * m34 - m24 * m31) + m14 * (m21 * m33 - m23 * m31)),

                det * +(m21 * (m32 * m44 - m34 * m42) - m22 * (m31 * m44 - m34 * m41) + m24 * (m31 * m42 - m32 * m41)),
                det * -(m11 * (m32 * m44 - m34 * m42) - m12 * (m31 * m44 - m34 * m41) + m14 * (m31 * m42 - m32 * m41)),
                det * +(m11 * (m22 * m44 - m24 * m42) - m12 * (m21 * m44 - m24 * m41) + m14 * (m21 * m42 - m22 * m41)),
                det * -(m11 * (m22 * m34 - m24 * m32) - m12 * (m21 * m34 - m24 * m31) + m14 * (m21 * m32 - m22 * m31)),

                det * -(m21 * (m32 * m43 - m33 * m42) - m22 * (m31 * m43 - m33 * m41) + m23 * (m31 * m42 - m32 * m41)),
                det * +(m11 * (m32 * m43 - m33 * m42) - m12 * (m31 * m43 - m33 * m41) + m13 * (m31 * m42 - m32 * m41)),
                det * -(m11 * (m22 * m43 - m23 * m42) - m12 * (m21 * m43 - m23 * m41) + m13 * (m21 * m42 - m22 * m41)),
                det * +(m11 * (m22 * m33 - m23 * m32) - m12 * (m21 * m33 - m23 * m31) + m13 * (m21 * m32 - m22 * m31)));
            return true;
        }
    }
}
