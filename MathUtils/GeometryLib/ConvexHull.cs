using LinearAlgebraLib;
using System.Diagnostics;

namespace GeometryLib
{
    public static class ConvexHull
    {
        public const double EPS = 1e-8;

        public static Mesh Calculate(Vec3[] points)
        {
            Tuple<Mesh, int[], double> tetrahedron = InitialTetrahedron(points);

            Mesh mesh = tetrahedron.Item1;
            double tolerance = tetrahedron.Item3;
            int p1 = tetrahedron.Item2[0];
            int p2 = tetrahedron.Item2[1];
            int p3 = tetrahedron.Item2[2];
            int p4 = tetrahedron.Item2[3];

            for (int i = 0; i < points.Length; i++)
            {
                if (i == p1 || i == p2 || i == p3 || i == p4) continue;

                KillVisibleFaces(mesh, i, tolerance);
                AddNewTriangles(mesh, i);
            }
            return mesh;
        }

        public static void AddNewTriangles(Mesh mesh, int newIndex)
        {
            int length = mesh.Faces.Count;
            for (int i = 0; i < length; i++)
            {
                Face face = mesh.Faces[i];
                for (int j = 0; j < 3; j++)
                {
                    if (face.Adjacent[j] is null)
                    {
                        int a = face.Indices[j];
                        int b = face.Indices[(j + 1) % 3];
                        mesh.Add(a, b, newIndex);
                    }
                }
            }
        }

        public static void KillVisibleFaces(Mesh mesh, int newIndex, double tolerance)
        {
            Vec3 point = mesh.Points[newIndex];
            for (int i = mesh.Faces.Count - 1; i >= 0; i--)
            {
                Face face = mesh.Faces[i];
                double distance = face.Plane.SignedDistanceTo(point);
                if (distance > tolerance)
                {
                    mesh.Kill(i);
                }
            }
        }

        public static Tuple<Mesh, int[], double> InitialTetrahedron(Vec3[] points)
        {
            if (points.Length < 4) throw new InvalidOperationException("Convex hull requires at least 4 points.");

            int p1, p2, p3, p4;
            GetFirstTwoTetrahedronPoints(points, out p1, out p2, out double tolerance);
            GetThirdTetrahedronPoint(points, p1, p2, out p3);
            GetForthTetrahedronPoint(points, p1, p2, p3, out p4);

            Vec3 centroid = (points[p1] + points[p2] + points[p3] + points[p4]) / 4;

            Mesh mesh = new Mesh(points);
            mesh.Add(p1, p2, p3);
            mesh.Add(p1, p3, p4);
            mesh.Add(p1, p4, p2);
            mesh.Add(p2, p4, p3);

            foreach (Face face in mesh.Faces)
            {
                double distance = face.Plane.SignedDistanceTo(centroid);

                if (distance < -tolerance) continue;
                if (distance > tolerance)
                {
                    face.Flip();
                }
                else
                {
                    throw new Exception("LOGIC ERROR: Point lies directly on plane.");
                }
            }
            return new Tuple<Mesh, int[], double>(mesh, [p1, p2, p3, p4], tolerance);
        }

        /// <summary>
        /// Picks point furthest from the plane defined by p1, p2 and p3.
        /// </summary>
        /// <param name="points"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public static void GetForthTetrahedronPoint(Vec3[] points, int p1, int p2, int p3, out int p4)
        {
            if (p1 == -1 || p2 == -1 || p3 == -1) throw new ArgumentException();

            p4 = -1;

            Plane plane = new Plane(points[p1], points[p2], points[p3]);

            double maxDistance = double.MinValue;
            for (int i = 0; i < points.Length; i++)
            {
                if (i == p1 || i == p2 || i == p3) continue;

                double distance = Math.Abs(plane.SignedDistanceTo(points[i]));
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    p4 = i;
                }
            }

            if (p4 == -1) throw new Exception("LOGIC ERROR: Point not found");
        }

        /// <summary>
        /// Picking point furthest from the line defined by p1 and p2.
        /// </summary>
        /// <param name="points"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public static void GetThirdTetrahedronPoint(Vec3[] points, int p1, int p2, out int p3)
        {
            if (p1 == -1 || p2 == -1) throw new ArgumentException();

            p3 = -1;

            Vec3 a = points[p1];
            Vec3 b = points[p2];
            Vec3 ba = b - a;
            Vec3 pa;
            double em = ba.SquareLength();

            double maxDistance = double.MinValue;
            for (int i = 0; i < points.Length; i++)
            {
                if (i == p1 || i == p2) continue;

                pa = points[i] - a;
                double distance = pa.Cross(ba).SquareLength() / em;
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    p3 = i;
                }
            }

            if (p3 == -1) throw new Exception("LOGIC ERROR: Point not found");
        }

        /// <summary>
        /// Picks two points furtherst from each other alon X, Y or Z axis.
        /// </summary>
        /// <param name="points"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <exception cref="Exception"></exception>
        public static void GetFirstTwoTetrahedronPoints(Vec3[] points, out int p1, out int p2, out double tolerance)
        {
            double minX, minY, minZ, maxX, maxY, maxZ;
            minX = minY = minZ = double.MaxValue;
            maxX = maxY = maxZ = double.MinValue;

            int[] min = new int[3];
            int[] max = new int[3];

            for (int i = 0; i < points.Length; i++)
            {
                var (x, y, z) = points[i];

                if (x < minX)
                {
                    minX = x;
                    min[0] = i;
                }

                if (y < minY)
                {
                    minY = y;
                    min[1] = i;
                }

                if (z < minZ)
                {
                    minZ = z;
                    min[2] = i;
                }

                if (x > maxX)
                {
                    maxX = x;
                    max[0] = i;
                }

                if (y > maxY)
                {
                    maxY = y;
                    max[1] = i;
                }

                if (z > maxZ)
                {
                    maxZ = z;
                    max[2] = i;
                }
            }
    
            double maxDistance = double.MinValue;
            int index = -1;
            for (int i = 0; i < 3; i++)
            {
                double maxComponent = points[max[i]][i];
                double minComponent = points[min[i]][i];
                double difference = maxComponent - minComponent;
                if (maxDistance < difference)
                {
                    maxDistance = difference;
                    index = i;
                }
            }

            p1 = min[index];
            p2 = max[index];

            if (p1 == p2) throw new Exception("LOGIC ERROR: Points are the same");

            tolerance = 3 * EPS * (
                   Math.Max(Math.Abs(minX), Math.Abs(maxX)) +
                   Math.Max(Math.Abs(minY), Math.Abs(maxY)) +
                   Math.Max(Math.Abs(minZ), Math.Abs(maxZ)));
        }

        public class Mesh
        {
            List<Face> _triangles = new List<Face>();
            readonly Vec3[] _points;

            public Mesh(Vec3[] points)
            {
                _points = points;
            }

            public Mesh(Mesh mesh)
            {
                _points = mesh._points;
                _triangles = new List<Face>(mesh._triangles);
            }

            public List<Face> Faces => _triangles;
            public Vec3[] Points => _points;

            public void Add(int a, int b, int c)
            {
                Plane plane = new Plane(_points[a], _points[b], _points[c]);
                Face tri = new Face(a, b, c, plane);
                ConnectNeighbours(tri);
                _triangles.Add(tri);
            }

            public void Kill(int index)
            {
                Face tri = _triangles[index];
                for (int i = 0; i < 3; i++)
                {
                    Face? adj = tri.Adjacent[i];
                    if (adj != null)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (adj.Adjacent[j] == tri)
                            {
                                adj.Adjacent[j] = null;
                                break;
                            }
                        }
                    }
                }
                _triangles.RemoveAt(index);
            }

            void ConnectNeighbours(Face tri)
            {
                bool needsToBeFlipped = false;
                for (int i = 0; i < 3; i++)
                {
                    int thisNext = (i + 1) % 3;

                    int a1 = tri.Indices[i];
                    int b1 = tri.Indices[thisNext];

                    for (int j = 0; j < _triangles.Count; j++)
                    {
                        if (tri == _triangles[j]) continue;

                        Face triTest = _triangles[j];

                        for (int k = 0; k < 3; k++)
                        {
                            int testNext = (k + 1) % 3;

                            int a2 = triTest.Indices[k];
                            int b2 = triTest.Indices[testNext];

                            if (a1 == b2 && b1 == a2)
                            {
                                tri.Adjacent[i] = triTest;
                                triTest.Adjacent[k] = tri;
                                break;
                            }

                            if (a1 == a2 && b1 == b2)
                            {
                                needsToBeFlipped = true;
                                tri.Adjacent[i] = triTest;
                                triTest.Adjacent[k] = tri;
                                break;
                            }
                        }
                    }
                }

                if (needsToBeFlipped)
                {
                    tri.Flip();
                }
            }
        }

        [DebuggerDisplay("{_indices[0]} {_indices[1]} {_indices[2]}")]
        public class Face
        {
            readonly int[] _indices = new int[3];
            readonly Face?[] _adjacent = new Face?[3];
            Plane _plane;

            public Face(int a, int b, int c, Plane plane)
            {
                _plane = plane;

                _indices[0] = a;
                _indices[1] = b;
                _indices[2] = c;

                _adjacent[0] = null;
                _adjacent[1] = null;
                _adjacent[2] = null;
            }

            public int[] Indices => _indices;
            public Face?[] Adjacent => _adjacent;
            public Plane Plane => _plane;

            public void Flip()
            {
                _plane = Plane.Flip();

                int temp = _indices[0];
                _indices[0] = _indices[2];
                _indices[2] = temp;

                Face? tf = _adjacent[0];
                _adjacent[0] = _adjacent[1];
                _adjacent[1] = tf;
            }
        }
    }
}
