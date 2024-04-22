namespace LinearAlgebraLib
{
    public static class Vec2Ex
    {
        public static Vec2 Between(this Vec2 a, Vec2 b)
        {
            return new Vec2((a.x + b.x) / 2, (a.y + b.y) / 2);
        }

        public static bool Parallel(this Vec2 a, Vec2 b, double tolerance)
        {
            return MathHelper.IsZero(a.Cross(b), tolerance);
        }

        public static bool Perpendicular(this Vec2 a, Vec2 b, double tolerance)
        {
            return MathHelper.IsZero(a.Dot(b), tolerance);
        }

        public static double Distance(this Vec2 a, Vec2 b)
        {
            return (b - a).Length();
        }
    }
}
