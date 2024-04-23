using System.Security.Principal;

namespace LinearAlgebraLib
{
    public readonly struct Mat3
    {
        public readonly double m11, m12, m13;
        public readonly double m21, m22, m23;
        public readonly double m31, m32, m33;

        public static Mat3 Identity => new Mat3(
            1, 0, 0,
            0, 1, 0,
            0, 0, 1);

        public Mat3(double m11, double m12, double m13, double m21, double m22, double m23, double m31, double m32, double m33)
        {
            this.m11 = m11; this.m12 = m12; this.m13 = m13;
            this.m21 = m21; this.m22 = m22; this.m23 = m23;
            this.m31 = m31; this.m32 = m32; this.m33 = m33;
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
                    _ => throw new IndexOutOfRangeException($"Invalid column index {col}. Valid column indices are 0, 1, and 2."),
                },
                1 => col switch
                {
                    0 => m21,
                    1 => m22,
                    2 => m23,
                    _ => throw new IndexOutOfRangeException($"Invalid column index {col}. Valid column indices are 0, 1, and 2."),
                },
                2 => col switch
                {
                    0 => m31,
                    1 => m32,
                    2 => m33,
                    _ => throw new IndexOutOfRangeException($"Invalid column index {col}. Valid column indices are 0, 1, and 2."),
                },
                _ => throw new IndexOutOfRangeException($"Invalid row index {row}. Valid row indices are 0, 1, and 2."),
            };
        }
        public double this[int row, int col] => Get(row, col);

        public double Determinant()
        {
            return
                m11 * (m22 * m33 - m23 * m32) -
                m12 * (m21 * m33 - m23 * m31) +
                m13 * (m21 * m32 - m22 * m31);
        }

        public bool Inverse(out Mat3 inverse)
        {
            double det = Determinant();
            if (det == 0)
            {
                inverse = new Mat3();
                return false;
            }

            det = 1.0 / det;
            inverse = new Mat3(
                det * (m22 * m33 - m23 * m32),
                det * (m13 * m32 - m12 * m33),
                det * (m12 * m23 - m13 * m22),

                det * (m23 * m31 - m21 * m33),
                det * (m11 * m33 - m13 * m31),
                det * (m13 * m21 - m11 * m23),

                det * (m21 * m32 - m22 * m31),
                det * (m12 * m31 - m11 * m32),
                det * (m11 * m22 - m12 * m21));
            return true;
        }
    }
}
