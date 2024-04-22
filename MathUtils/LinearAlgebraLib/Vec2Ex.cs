namespace LinearAlgebraLib
{
    public static class Vec2Ex
    {
        public static Vec2 Between(this Vec2 a, Vec2 b)
        {
            return new Vec2((a.x + b.x) / 2, (a.y + b.y) / 2);
        }
    }
}
