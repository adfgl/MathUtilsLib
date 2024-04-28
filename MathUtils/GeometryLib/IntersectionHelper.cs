using LinearAlgebraLib;

namespace GeometryLib
{
    public static class IntersectionHelper
    {
        public static bool Intersect(Plane plane, Box box)
        {
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
    }
}
