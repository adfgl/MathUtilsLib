using GeometryLib;
using LinearAlgebraLib;

namespace MathUtilsConsole
{
    public class ConvexHull
    {

    }

    public class Face
    {
        public const int NO_ADJACENT = -1;

        readonly int[] _indices = new int[3];
        readonly int[] _adjacent = new int[3];
        Plane _plane;

        public Face(int a, int b, int c, Plane plane)
        {
            _plane = plane;

            _indices[0] = a;
            _indices[1] = b;
            _indices[2] = c;

            _adjacent[0] = NO_ADJACENT;
            _adjacent[1] = NO_ADJACENT;
            _adjacent[2] = NO_ADJACENT;
        }

        public int[] Indices => _indices;
        public int[] Adjacent => _adjacent;
        public Plane Plane => _plane;

        public void Flip()
        {
            _plane = Plane.Flip();

            int temp = _indices[0];
            _indices[0] = _indices[2];
            _indices[2] = temp;

            temp = _adjacent[0];
            _adjacent[0] = _adjacent[1];
            _adjacent[1] = temp;
        }

        public override string ToString()
        {
            return $"pts: {_indices[0]} {_indices[1]} {_indices[2]} adj: {_adjacent[0]} {_adjacent[1]} {_adjacent[2]}";
        }
    }

    public class TriMesh
    {
        List<Face> _triangles = new List<Face>();
        List<Vec3> _points = new List<Vec3>();

        public TriMesh(List<Vec3> points)
        {
            _points = points;
        }

        public List<Face> Triangles => _triangles;

        public void Add(int a, int b, int c)
        {
            Plane plane = new Plane(_points[a], _points[b], _points[c]);
            Face tri = new Face(a, b, c, plane);
            ConnectNeighbours(tri, _triangles.Count);
            _triangles.Add(tri);
        }

        public void Kill(int index)
        {
            Face tri = _triangles[index];
            for (int i = 0; i < 3; i++)
            {
                int adjIndex = tri.Adjacent[i];
                if (adjIndex != Face.NO_ADJACENT)
                {
                    Face adj = _triangles[adjIndex];
                    for (int j = 0; j < 3; j++)
                    {
                        if (adj.Adjacent[j] == index)
                        {
                            adj.Adjacent[j] = Face.NO_ADJACENT;
                            break;
                        }
                    }
                }
            }
            _triangles.RemoveAt(index);
        }

        void ConnectNeighbours(Face tri, int index)
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
                            tri.Adjacent[i] = j;
                            triTest.Adjacent[k] = index;
                            break;
                        }

                        if (a1 == a2 && b1 == b2)
                        {
                            needsToBeFlipped = true;
                            tri.Adjacent[i] = j;
                            triTest.Adjacent[k] = index;
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
}
