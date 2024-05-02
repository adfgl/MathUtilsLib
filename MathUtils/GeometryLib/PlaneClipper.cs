using DataPoolLib;
using LinearAlgebraLib;
using System.Drawing;

namespace GeometryLib
{
    public static class PlaneClipper
    {
        static Vec3 Normal(Vec3 a, Vec3 b, Vec3 c)
        {
            return (b - a).Cross(c - a);
        }

        public static Mesh3 Clip(this Mesh3 mesh, Plane[] planes, double tolerance = 0)
        {
            Plane[] planesToUse = new Plane[planes.Length];
            int planesToUseCount = 0;

            foreach (Plane plane in planes)
            {
                if (plane.IsValid() && mesh.BoundingBox.Intersects(plane))
                {
                    planesToUse[planesToUseCount++] = plane;
                }
            }

            if (planesToUseCount == 0) return mesh;

            Mesh3 clipped = new Mesh3(mesh.Triangles.Count, mesh.Vertices.Count);
            OctreePoints3 octree = new OctreePoints3(mesh.BoundingBox, mesh.Vertices.Count);

            Vec3[] newVertices = new Vec3[6];

            Stack<Vec3[]> listTriangles = new Stack<Vec3[]>();
            for (int i = 0; i < mesh.Triangles.Count; i++)
            {
                listTriangles.Clear();

                mesh.Triangles.GetTriangle(i, out int a, out int b, out int c, out int color);
                listTriangles.Push([mesh.Vertices.GetVertex(a), mesh.Vertices.GetVertex(b), mesh.Vertices.GetVertex(c)]);

                int newTriangles = 1;
                for (int j = 0; j < planesToUseCount; j++)
                {
                    while (newTriangles > 0)
                    {
                        Vec3[] test = listTriangles.Pop();
                        newTriangles--;

                        int count = ClipTriangle(ref newVertices, planesToUse[j], test, tolerance);
                        for (int k = 0; k < count; k+=3)
                        {
                            listTriangles.Push([newVertices[k], newVertices[k + 1], newVertices[k + 2]]);
                        }
                    }
                    newTriangles = listTriangles.Count;
                }

                foreach (Vec3[] triangle in listTriangles)
                {
                    clipped.AddTriangle(octree, triangle[0], triangle[1], triangle[2], color, tolerance);
                }
            }
            return clipped;
        }

        public static int ClipTriangle(ref Vec3[] newTriangles, Plane plane, Vec3[] triangle, double tolerance = 0)
        {
            int frontCount = 0, behindCount = 0;
            Vec3[] front = new Vec3[3];
            Vec3[] behind = new Vec3[3];

            for (int i = 0; i < 3; i++)
            {
                Vec3 point = triangle[i];
                double signedDistance = plane.SignedDistanceTo(point);
                if (signedDistance > tolerance)
                {
                    front[frontCount++] = point;
                }
                else if (signedDistance < -tolerance)
                {
                    behind[behindCount++] = point;
                }
            }

            if (behindCount == 3) return 0;

            Vec3 a = triangle[0];
            Vec3 b = triangle[1];
            Vec3 c = triangle[2];

            if (frontCount == 3)
            {
                newTriangles[0] = a;
                newTriangles[1] = b;
                newTriangles[2] = c;
                return 1;
            }

            Vec3 normal = Normal(a, b, c);

            int fixOrientation(ref Vec3[] newTriangles, int count)
            {
                int swaps = 0;
                for (int i = 0; i < count; i+=3)
                {
                    int a = i;
                    int b = i + 1;
                    int c = i + 2;

                    if (Normal(newTriangles[a], newTriangles[b], newTriangles[c]).Dot(normal) < 0)
                    {
                        Vec3 t = newTriangles[a];
                        newTriangles[a] = newTriangles[c];
                        newTriangles[c] = t;
                        swaps++;
                    }
                }
                return swaps;
            }

            Vec3 v1, v2, v3, v4;
            if (frontCount == 1 && behindCount == 2)
            {
                /*     
                       2
                       /\
                   1  /  \  3
                  ---+----+---
                */

                v2 = front[0];
                plane.Intersect(v2, behind[0], out v1, tolerance);
                plane.Intersect(v2, behind[1], out v3, tolerance);

                newTriangles[0] = v1;
                newTriangles[1] = v2;
                newTriangles[2] = v3;

                fixOrientation(ref newTriangles, 1);
                return 1;
            }

            if (frontCount == 2 && behindCount == 1)
            {
                /*
                  3+------+ 4
                    \  + /  
                  ---+--+---
                     1  2
                 */

                v3 = front[0];
                v4 = front[1];
                plane.Intersect(v3, behind[0], out v1, tolerance);
                plane.Intersect(v4, behind[0], out v2, tolerance);

                newTriangles[0] = v1;
                newTriangles[1] = v3;
                newTriangles[2] = v4;

                newTriangles[3] = v1;
                newTriangles[4] = v4;
                newTriangles[5] = v2;

                fixOrientation(ref newTriangles, 2);
                return 2;
            }

            throw new Exception("LOGIC ERROR: Invalid triangle configuration.");
        }
    }
}
