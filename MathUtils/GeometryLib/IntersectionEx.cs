using LinearAlgebraLib;

namespace GeometryLib
{
    public static class IntersectionEx
    {
        public static bool Intersect(this Mesh3 mesh, Ray ray, out Vec3 intersection, double tolerance = 0)
        {
            intersection = Vec3.NaN;

            Vec3 p1, p2, p3;
            Plane plane;
            for (int i = 0; i < mesh.Triangles.Count; i++)
            {
                mesh.Triangles.GetTriangle(i, out int a, out int b, out int c, out int color);

                p1 = mesh.Vertices.GetVertex(a);
                p2 = mesh.Vertices.GetVertex(b);
                p3 = mesh.Vertices.GetVertex(c);

                plane = new Plane(p1, p2, p3);
                if (plane.Intersect(ray, out intersection, tolerance))
                {
                   if (IsPointInTriangle(intersection, p1, p2, p3)) return true; 
                }
            }
            return false;
        }

        public static bool IsPointInTriangle(Vec3 point, Vec3 a, Vec3 b, Vec3 c)
        {
            // Compute vectors
            Vec3 v0 = c - a;
            Vec3 v1 = b - a;
            Vec3 v2 = point - a;

            // Compute dot products
            double dot00 = v0.Dot(v0);
            double dot01 = v0.Dot(v1);
            double dot02 = v0.Dot(v2);
            double dot11 = v1.Dot(v1);
            double dot12 = v1.Dot(v2);

            // Compute barycentric coordinates
            double invDenom = 1 / (dot00 * dot11 - dot01 * dot01);
            double u = (dot11 * dot02 - dot01 * dot12) * invDenom;
            double v = (dot00 * dot12 - dot01 * dot02) * invDenom;

            // Check if point is in triangle
            return (u >= 0) && (v >= 0) && (u + v <= 1);
        }

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

        public static bool Intersect(this Plane plane, Vec3 start, Vec3 end, out Vec3 intersection, double tolerance = 0)
        {
            intersection = Vec3.NaN;

            double startDistance = plane.SignedDistanceTo(start);
            if (MathHelper.IsZero(startDistance, tolerance))
            {
                intersection = start;
                return true;
            }

            double endDistance = plane.SignedDistanceTo(end);
            if (MathHelper.IsZero(endDistance, tolerance))
            {
                intersection = end;
                return true;
            }

            if (Math.Sign(startDistance) == Math.Sign(endDistance))
            {
                return false;
            }

            Vec3 lineDir = (end - start).Normalize();
            double dot = plane.normal.Dot(lineDir);

            double t = -startDistance / dot;
            intersection = start + lineDir * t;
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
