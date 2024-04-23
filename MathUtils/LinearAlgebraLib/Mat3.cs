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

        public Mat3 Transpose()
        {
            return new Mat3(
                    m11, m21, m31,
                    m12, m22, m32,
                    m13, m23, m33);
        }

        /* 
        1.000.000 iterations indicate that using 'in' keyword for passing parameters offers slight performance improvement over passing by value.
        Is it worth the hustle? not really. The performance difference is negligible.

        | Method    | Mean     | Error    | StdDev   | Gen0     | Gen1     | Gen2     | Allocated |
        |---------- |---------:|---------:|---------:|---------:|---------:|---------:|----------:|
        | Normal    | 16.83 ms | 53.93 ms | 2.956 ms | 296.8750 | 296.8750 | 296.8750 |  38.15 MB |
        | Reference | 14.03 ms | 16.43 ms | 0.900 ms | 312.5000 | 312.5000 | 312.5000 |  38.15 MB |
         */

        public static Mat3 Multiply(Mat3 a, Mat3 b)
        {
            return new Mat3(
                a.m11 * b.m11 + a.m12 * b.m21 + a.m13 * b.m31,
                a.m11 * b.m12 + a.m12 * b.m22 + a.m13 * b.m32,
                a.m11 * b.m13 + a.m12 * b.m23 + a.m13 * b.m33,

                a.m21 * b.m11 + a.m22 * b.m21 + a.m23 * b.m31,
                a.m21 * b.m12 + a.m22 * b.m22 + a.m23 * b.m32,
                a.m21 * b.m13 + a.m22 * b.m23 + a.m23 * b.m33,

                a.m31 * b.m11 + a.m32 * b.m21 + a.m33 * b.m31,
                a.m31 * b.m12 + a.m32 * b.m22 + a.m33 * b.m32,
                a.m31 * b.m13 + a.m32 * b.m23 + a.m33 * b.m33);
        }

        public static Vec2 Multiply(Mat3 m, Vec2 v)
        {
            return new Vec2(
                x: v.x * m.m11 + v.y * m.m12 + v.w * m.m13,
                y: v.x * m.m21 + v.y * m.m22 + v.w * m.m23,
                w: v.x * m.m31 + v.y * m.m32 + v.w * m.m33);
        }

        public static Mat3 Multiply(Mat3 m, double scalar)
        {
             return new Mat3(
                 m.m11 * scalar, m.m12 * scalar, m.m13 * scalar,
                 m.m21 * scalar, m.m22 * scalar, m.m23 * scalar,
                 m.m31 * scalar, m.m32 * scalar, m.m33 * scalar);
        }

        public static Mat3 Divide(Mat3 m, double scalar)
        {
            return new Mat3(
                m.m11 / scalar, m.m12 / scalar, m.m13 / scalar,
                m.m21 / scalar, m.m22 / scalar, m.m23 / scalar,
                m.m31 / scalar, m.m32 / scalar, m.m33 / scalar);
        }

        public static Mat3 operator *(Mat3 a, Mat3 b) => Multiply(a, b);
        public static Vec2 operator *(Mat3 m, Vec2 v) => Multiply(m, v);
        public static Vec2 operator *(Vec2 v, Mat3 m) => Multiply(m, v);
        public static Mat3 operator *(Mat3 m, double scalar) => Multiply(m, scalar);
        public static Mat3 operator *(double scalar, Mat3 m) => Multiply(m, scalar);
        public static Mat3 operator /(Mat3 m, double scalar) => Divide(m, scalar);
    }
}
