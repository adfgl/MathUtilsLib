using LinearAlgebraLib;
using System.Collections;

namespace GeometryLib
{
    // https://jerryyin.info/geometry-processing-algorithms/half-edge/

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

        public void GetTriangle(Face3 face, out Vec3 a, out Vec3 b, out Vec3 c)
        {
            HalfEdge3 edge = face.HalfEdge;
            a = edge.Vertex.Position;
            b = edge.Next.Vertex.Position;
            c = edge.Next.Next.Vertex.Position;
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
            _faces.Add(face);
            return face;
        }

        HalfEdge3? FindTwinEdge(Vertex3 start, Vertex3 end)
        {
            foreach (Face3 face in _faces)
            {
                for (int i = 0; i < 3; i++)
                {
                    HalfEdge3 edge = face.GetEdge(i);
                    if (edge.Twin is null)
                    {
                        if (edge.Vertex == start && edge.Next.Vertex == end)
                        {
                            edge.Twin = end.HalfEdge;
                            return edge;
                        }
                    }
                }
            }
            return null;
        }

        public class Vertex3
        {
            public int Index { get; set; }
            public Vec3 Position { get; set; }
            public HalfEdge3 HalfEdge { get; set; }

            public override string ToString()
            {
                return $"({Index}) {Position.ToRoundedString(2)}";
            }
        }

        public class HalfEdge3 : IEnumerable<Vertex3>
        {
            public HalfEdge3(Vertex3 vertex, Face3 face)
            {
                Vertex = vertex;
                Face = face;
                vertex.HalfEdge = this;
            }

            public Vertex3 Vertex { get; set; }
            public Face3 Face { get; set; }
            public HalfEdge3 Next { get; set; } = null!;
            public HalfEdge3? Twin { get; set; } = null!;

            public int IndexOf(Vertex3 vertex)
            {
                if (Vertex == vertex) return 0;
                if (Next.Vertex == vertex) return 1;
                return -1;
            }

            public bool Contains(Vertex3 vertex)
            {
                return IndexOf(vertex) != -1;
            }

            public Vertex3 GetVertex(int index)
            {
                switch (index)
                {
                    case 0: return Vertex;
                    case 1: return Next.Vertex;
                    default:
                        throw new IndexOutOfRangeException($"Invalid index '{index}'. Edge has only two vertices.");
                }
            }

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

            public override string ToString()
            {
                return $"{Vertex.Index},{Next.Vertex.Index}";
            }
        }

        public class Face3 : IEnumerable<HalfEdge3>
        {
            public int Index { get; set; }
            public HalfEdge3 HalfEdge { get; set; }

            public int IndexOf(Vertex3 vertex)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (GetVertex(i) == vertex)
                    {
                        return i;
                    }
                }
                return -1;
            }

            public bool Contains(Vertex3 vertex)
            {
                return IndexOf(vertex) != -1;
            }

            public Vertex3 GetVertex(int index)
            {
                switch (index)
                {
                    case 0: return HalfEdge.Vertex;
                    case 1: return HalfEdge.Next.Vertex;
                    case 2: return HalfEdge.Next.Next.Vertex;
                    default:
                        throw new IndexOutOfRangeException($"Invalid index '{index}'. Face has only three vertices.");
                }
            }

            public HalfEdge3 GetEdge(int index)
            {
                switch (index)
                {
                    case 0: return HalfEdge;
                    case 1: return HalfEdge.Next;
                    case 2: return HalfEdge.Next.Next;
                    default:
                        throw new IndexOutOfRangeException($"Invalid index '{index}'. Face has only three edges.");
                }
            }

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

            public override string ToString()
            {
                int[] indices = this.Select(h => h.Vertex.Index).ToArray();
                return $"({Index}) {String.Join(',', indices)}";
            }
        }
    }
}
