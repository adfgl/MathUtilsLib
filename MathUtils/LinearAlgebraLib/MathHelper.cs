namespace LinearAlgebraLib
{
    public static class MathHelper
    {
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
    }
}
