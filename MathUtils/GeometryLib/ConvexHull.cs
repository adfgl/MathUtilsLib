using LinearAlgebraLib;
using System.Diagnostics;

namespace GeometryLib
{
    public static class ConvexHull
    {
        public static void Calculate(Vec3[] uniquePoints, double tolerance)
        {
            Tuple<Face[], int[]> tetrahedron = InitialTetrahedron(uniquePoints, tolerance);
        
        }

        public static Tuple<Face[], int[]> InitialTetrahedron(Vec3[] uniquePoints, double tolerance)
        {
            if (uniquePoints.Length < 4) throw new InvalidOperationException("Convex hull requires at least 4 points.");

            int p1 = 0;
            int p2 = 1;
            int p3 = -1;
            int p4 = -1;

            Vec3 a = uniquePoints[p1];
            Vec3 b = uniquePoints[p2];
            Vec3 c;
            if (a == b) throw new InvalidOperationException();

            Vec3 ab = b - a;
            for (int i = 0; i < uniquePoints.Length; i++)
            {
                if (i == p1 || i == p2) continue;

                c = uniquePoints[i];

                // check if points are coliniar
                if (false == MathHelper.IsZero(ab.Cross(c - a).SquareLength(), tolerance))
                {
                    p3 = i;
                    break;
                }
            }

            if (p3 == -1) throw new InvalidOperationException("All points are colliniar.");

            Plane plane = new Plane(uniquePoints[p1], uniquePoints[p2], uniquePoints[p3]);
            for (int i = 0; i < uniquePoints.Length; i++)
            {
                if (i == p1 || i == p2 || i == p3) continue;

                if (false == MathHelper.IsZero(plane.SignedDistanceTo(uniquePoints[i]), tolerance))
                {
                    p4 = i;
                    break;
                }
            }

            if (p4 == -1) throw new InvalidOperationException("All points are on the same plane.");

            Vec3 centroid = (uniquePoints[p1] + uniquePoints[p2] + uniquePoints[p3] + uniquePoints[p4]) / 4;

            Face[] faces =
            [
                GetValidFace(uniquePoints, centroid, p1, p2, p3, tolerance),
                GetValidFace(uniquePoints, centroid, p1, p3, p4, tolerance),
                GetValidFace(uniquePoints, centroid, p1, p4, p2, tolerance),
                GetValidFace(uniquePoints, centroid, p2, p4, p3, tolerance)
            ];
            return new Tuple<Face[], int[]>(faces, [p1, p2, p3, p4]);
        }

        public static Face GetValidFace(Vec3[] points, Vec3 centroid, int indexA, int indexB, int indexC, double tolerance)
        {
            Plane plane = new Plane(points[indexA], points[indexB], points[indexC]);
            Face face = new Face(indexA, indexB, indexC, plane);

            double distance = plane.SignedDistanceTo(centroid);
            if (distance > tolerance)
            {
                return face.Invert();
            }
            else if (distance < -tolerance)
            {
                return face;
            }
            throw new InvalidOperationException("Logic error.");
        }

        [DebuggerDisplay("{a} {b} {c}")]
        public readonly struct Face
        {
            public readonly int a;
            public readonly int b;
            public readonly int c;
            public readonly Plane plane;

            public Face(int a, int b, int c, Plane plane)
            {
                this.a = a;
                this.b = b;
                this.c = c;
                this.plane = plane;
            }

            public Face Invert()
            {
                return new Face(c, b, a, plane.Flip());
            }
        }
    }
}
