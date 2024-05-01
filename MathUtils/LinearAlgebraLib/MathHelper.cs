namespace LinearAlgebraLib
{
    public static class MathHelper
    {
        public const double PI = Math.PI;
        public const double HALF_PI = 0.5 * PI;
        public const double ONE_AND_HALF_PI = 1.5 * PI;
        public const double TWO_PI = 2 * PI;

        public const double DEG2RAD = PI / 180;
        public const double RAD2DEG = 180 / PI;

        public const double INFINITY = 99999;

        public static bool IsInfinity(double value)
        {
            return Math.Abs(value) >= INFINITY;
        }

        public static bool IsZero(double number, double tolerance)
        {
#if DEBUG
            if (tolerance < 0) throw new ArgumentException($"Tolerance must be non-negative ({tolerance})", nameof(tolerance));
#endif
            return number >= -tolerance && number <= tolerance;
        }

        public static bool IsOne(double number, double tolerance)
        {
            return IsZero(number - 1, tolerance);
        }

        public static bool AreEqual(double a, double b, double tolerance)
        {
            return IsZero(a - b, tolerance);
        }

        public static double ResultantAngle(double x, double y)
        {
            if (y == 0)
            {
                if (x > 0) return 0;
                return PI;
            }

            if (x == 0)
            {
                if (y > 0) return HALF_PI;
                return ONE_AND_HALF_PI;
            }

            double atan = Math.Atan(Math.Abs(y / x));
            if (x > 0)
            {
                if (y > 0) return atan;
                if (y < 0) return TWO_PI - atan;
            }
            else
            {
                if (y > 0) return PI - atan;
                if (y < 0) return PI + atan;
            }

            if (y > 0)
            {
                if (x > 0) return atan;
                if (x < 0) return PI - atan;
            }
            else
            {
                if (x > 0) return TWO_PI - atan;
                if (x < 0) return PI + atan;
            }
            return 0;
        }
    }
}
