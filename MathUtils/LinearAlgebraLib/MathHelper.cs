namespace LinearAlgebraLib
{
    public static class MathHelper
    {
        public static bool IsZero(double number, double tolerance)
        {
            return number >= -tolerance && number <= tolerance;
        }

 
    }
}
