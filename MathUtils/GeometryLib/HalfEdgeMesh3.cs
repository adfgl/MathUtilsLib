using LinearAlgebraLib;
using System.Collections;

namespace GeometryLib
{
    public class HalfEdgeMesh3
    {
        readonly List<Vertex3> _vertices;
        readonly List<Face3> _faces;

        public HalfEdgeMesh3()
        {
            _vertices = new List<Vertex3>();
            _faces = new List<Face3>();
        }

        public List<Vertex3> Vertices => _vertices;
        public List<Face3> Faces => _faces;

        public HalfEdgeMesh3(int expectedVertices, int expectedFaces)
        {
            _vertices = new List<Vertex3>(expectedVertices);
            _faces = new List<Face3>(expectedFaces);
        }

        public void AddVertex(Vec3 position)
        {
            Vertex3 vertex = new Vertex3()
            {
                Index = _vertices.Count,
                Position = position,
                HalfEdge = null
            };
            _vertices.Add(vertex);
        }

        public Face3 AddFace(Vertex3 a, Vertex3 b, Vertex3 c)
        {
            Face3 face = new Face3() { Index = _faces.Count };

            HalfEdge3 ab = new HalfEdge3(a, face);
            HalfEdge3 bc = new HalfEdge3(b, face);
            HalfEdge3 ca = new HalfEdge3(c, face);

            ab.Next = bc;
            bc.Next = ca;
            ca.Next = ab;

            ab.Twin = FindTwinEdge(b, a);
            bc.Twin = FindTwinEdge(c, b);
            ca.Twin = FindTwinEdge(a, c);

            face.HalfEdge = ab;
            return face;
        }

        HalfEdge3? FindTwinEdge(Vertex3 start, Vertex3 end)
        {
            foreach (Vertex3 vertex in _vertices)
            {
                if (vertex == start || vertex == end || vertex.HalfEdge is null) continue;

                HalfEdge3 current = vertex.HalfEdge;
                do
                {
                    if (current.Vertex == end && current.Next.Vertex == start)
                    {
                        return current;
                    }
                    current = current.Next;
                } while (current != vertex.HalfEdge);
            }
            return null;
        }

        public class Vertex3
        {
            public int Index { get; set; }
            public Vec3 Position { get; set; }
            public HalfEdge3 HalfEdge { get; set; }
        }

        public class HalfEdge3 : IEnumerable<Vertex3>
        {
            public HalfEdge3(Vertex3 vertex, Face3 face)
            {
                vertex.HalfEdge = this;
                Vertex = vertex;
                Face = face;
                Index = vertex.HalfEdge == null ? 0 : vertex.HalfEdge.Index + 1;
            }

            public int Index { get; set; }
            public Vertex3 Vertex { get; set; }
            public Face3 Face { get; set; }
            public HalfEdge3 Next { get; set; } = null!;
            public HalfEdge3? Twin { get; set; } = null!;

            public IEnumerator<Vertex3> GetEnumerator()
            {
                Vertex3 current = Vertex;
                do
                {
                    yield return current;
                    current = current.HalfEdge.Next.Vertex;
                } while (current != Vertex);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public class Face3 : IEnumerable<HalfEdge3>
        {
            public int Index { get; set; }
            public HalfEdge3 HalfEdge { get; set; }

            public IEnumerator<HalfEdge3> GetEnumerator()
            {
                HalfEdge3 current = HalfEdge;
                do
                {
                    yield return current;
                    current = current.Next;
                } while (current != HalfEdge);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
