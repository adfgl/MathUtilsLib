namespace LinearAlgebraLib
{
    public static class MathHelper
    {
        public static bool IsZero(double number, double tolerance)
        {
            return number >= -tolerance && number <= tolerance;
        }

        public static bool IsOne(double number, double tolerance)
        {
            return IsZero(number - 1.0, tolerance);
        }
    }
}
