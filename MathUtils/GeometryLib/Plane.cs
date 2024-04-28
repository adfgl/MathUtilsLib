using LinearAlgebraLib;

namespace GeometryLib
{
    public readonly struct Plane
    {
        public readonly Vec3 normal;
        public readonly double distanceToOrigin;

        public Plane(Vec3 normal, double distanceToOrigin)
        {
            this.normal = normal.Normalize();
            this.distanceToOrigin = distanceToOrigin;
        }

        public Plane(Vec3 normal, Vec3 point)
        {
            this.normal = normal.Normalize();
            this.distanceToOrigin = normal.Dot(point);
        }

        public Plane(Vec3 a, Vec3 b, Vec3 c)
        {
            this.normal = (b - a).Cross(c - a).Normalize();
            this.distanceToOrigin = normal.Dot(a);
        }

        /// <summary>
        /// Calculates the signed distance from a specified point to the plane.
        /// </summary>
        /// <param name="point"></param>
        /// <returns>The distance is <c>positive</c> if the point is in the direction of the plane's normal, <c>negative</c> otherwise.</returns>
        public double SignedDistanceTo(Vec3 point)
        {
            return normal.Dot(point) - distanceToOrigin;
        }

        /// <summary>
        /// Checks whether the plane is valid by verifiying if the normal is non-zero.
        /// </summary>
        /// <returns></returns>
        public bool IsValid() => normal.SquareLength() > 0.0;

        public Plane Flip() => new Plane(-normal, -distanceToOrigin);
    }
}
