namespace LinearAlgebraLib
{
    public readonly struct Mat3
    {
        public readonly double m11, m12, m13;
        public readonly double m21, m22, m23;
        public readonly double m31, m32, m33;

        public Mat3(double m11, double m12, double m13, double m21, double m22, double m23, double m31, double m32, double m33)
        {
            this.m11 = m11; this.m12 = m12; this.m13 = m13;
            this.m21 = m21; this.m22 = m22; this.m23 = m23;
            this.m31 = m31; this.m32 = m32; this.m33 = m33;
        }

    }
}
