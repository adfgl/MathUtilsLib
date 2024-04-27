namespace LinearAlgebraLib
{
    public static class VecEx
    {
        public static Vec3 Between(this Vec3 a, Vec3 b)
        {
            return new Vec3((a.x + b.x) / 2, (a.y + b.y) / 2, (a.z + b.z) / 2);
        }
        public static Vec2 Between(this Vec2 a, Vec2 b)
        {
            return new Vec2((a.x + b.x) / 2, (a.y + b.y) / 2);
        }

        public static bool Parallel(this Vec2 a, Vec2 b, double tolerance)
        {
            return MathHelper.IsZero(a.Cross(b), tolerance);
        }
        public static bool Parallel(this Vec3 a, Vec3 b, double tolerance)
        {
            return MathHelper.IsZero(a.Cross(b).Length(), tolerance);
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
