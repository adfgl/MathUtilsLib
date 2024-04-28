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

        public static bool Intersect(this Ray ray1, Ray ray2, out Vec3 intersection, double tolerance = 0)
        {
            /* 
             R1(t) = O1 + t * D1
             R2(s) = O2 + s * D2

            The goal is to find such 't' and 's' which will satisfy this condition:
             O1 + t * D1 = O2 + s * D2

            Let's break the equtions into components and solve them for evey plane:
            > x = x1 + t * Dx1 = x2 + s * Dx2
            > y = y1 + t * Dy1 = y2 + s * Dy2
            > z = z1 + t * Dz1 = z2 + s * Dz2
             */

            // nominal coordiante pairs (X,Y), (Y,Z), (Z,X)
            double x1, y1, x2, y2; // ray origin
            double a1, a2, b1, b2; // ray direction

            /* Projections XY, YZ, ZX */
            int next;
            double denominator;
            for (int curr = 0; curr < 3; curr++)
            {
                next = (curr + 1) % 3;

                /*
                    R1(t) = O1 + t * D1
                    R2(s) = O2 + s * D2 

                    x = x1 + t * a1 = x2 + s * b1 >> t = (x2 + s * b1 - x1) / a1
                    y = y1 + t * a2 = y2 + s * b2 >> s = (y1 + t * a2 - y2) / b2
                */

                x1 = ray1.origin[curr];
                y1 = ray1.origin[next];

                a1 = ray1.direction[curr];
                a2 = ray1.direction[next];

                x2 = ray2.origin[curr];
                y2 = ray2.origin[next];

                b1 = ray2.direction[curr];
                b2 = ray2.direction[next];

                denominator = a1 * b2 - a2 * b1;
                if (false == MathHelper.IsZero(denominator, tolerance))
                {
                    double numerator = b2 * (x2 - x1) - b1 * (y2 - y1);
                    intersection = ray1.PointAlong(numerator / denominator);
                    return true;
                }
            }

            intersection = Vec3.NaN;
            return false;
        }
    }
}
