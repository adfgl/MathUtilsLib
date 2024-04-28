using LinearAlgebraLib;

namespace GeometryLib
{
    public readonly struct Ray
    {
        public readonly Vec3 origin;
        public readonly Vec3 direction;

        public Ray(Vec3 origin, Vec3 direction)
        {
            this.origin = origin;
            this.direction = direction.Normalize();
        }

        public Vec3 PointAlong(double distance)
        {
            return origin + direction * distance;
        }
    }
}
