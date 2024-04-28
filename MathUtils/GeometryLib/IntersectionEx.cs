using LinearAlgebraLib;

namespace GeometryLib
{
    public static class IntersectionEx
    {
        public static bool Intersects(this Box box, Plane plane)
        {
            /* check if at least two points are on different sides of the plane */
            Vec3[] points = box.GetPoints();
            double dist = plane.SignedDistanceTo(points[0]);
            double minDistance = dist, maxDistance = dist; 
            for (int i = 1; i < 8; i++)
            {
                dist = plane.SignedDistanceTo(points[i]);
                if (minDistance > dist) minDistance = dist;
                if (maxDistance < dist) maxDistance = dist;

                if (Math.Sign(minDistance) != Math.Sign(maxDistance)) return true;
            }
            return false;
        }

        public static bool Intersect(this Plane plane, Ray ray, out Vec3 intersection, double tolerance = 0)
        {
            /* 
                R(t) = O + t * D
                +-> O is the origin of the ray
                +-> D is the direction of the ray
                +-> t is a scalar value allowing to get point along the ray

                n * p = D0
                +-> n is the normal of the plane
                +-> p is the point on the plane
                +-> D0 distance from the origin to the plane

                The goal is to find such 't' which will satisfy this condition:

                1. x = O + t * D
                2. x = D0 / n
                3. O + t * D = D0 / n
                4. t = (D0 / n - O) / D >> (D0 - O * n) / (D * n)
            */

            intersection = Vec3.NaN;

            double signedDistance = plane.SignedDistanceTo(ray.origin);
            if (MathHelper.IsZero(signedDistance, tolerance))
            {
                intersection = ray.origin;
                return true;
            }
         
            double dot = plane.normal.Dot(ray.direction); // which is 'D * n'
            if (MathHelper.IsZero(dot, tolerance) // perpendicular
                || 
                (Math.Sign(dot) == Math.Sign(signedDistance))) // pointing away
            {
                return false;
            }

            double t = -signedDistance / dot;
            intersection = ray.PointAlong(t);
            return true;
        }
    }
}
