using LinearAlgebraLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GeometryLib.DataStrcutures.HalfEdgeMesh3;

namespace GeometryLib.DataStrcutures
{
    public class HalfEdgeMesh2
    {
        readonly List<Vertex2> _vertices;
        readonly List<Face2> _faces;

        public HalfEdgeMesh2()
        {
            _vertices = new List<Vertex2>();
            _faces = new List<Face2>();
        }

        public List<Vertex2> Vertices => _vertices;
        public List<Face2> Faces => _faces;

        public Vertex2 AddVertex(Vec2 position)
        {
            foreach (Vertex2 v in _vertices)
            {
                if (v.Position.AlmostEquals(position, 0))
                {
                    return v;
                }
            }

            Vertex2 vertex = new Vertex2()
            {
                Index = _vertices.Count,
                Position = position,
                HalfEdge = null
            };
            _vertices.Add(vertex);
            return vertex;
        }

        public Face2 AddFace(Vertex2 a, Vertex2 b, Vertex2 c)
        {
            Face2 face = new Face2() { Index = _faces.Count };

            HalfEdge2 ab = new HalfEdge2(a, face);
            HalfEdge2 bc = new HalfEdge2(b, face);
            HalfEdge2 ca = new HalfEdge2(c, face);

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

        HalfEdge2? FindTwinEdge(Vertex2 start, Vertex2 end)
        {
            foreach (Face2 face in _faces)
            {
                for (int i = 0; i < 3; i++)
                {
                    HalfEdge2 edge = face.GetEdge(i);
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

        public enum EContainment
        {
            Undefined,
            Outside,
            Inside,
            OnEdge,
            OnVertex
        }

        public HalfEdge2 ContainedEdge(Vec2 point, Face2 startFace, out EContainment containment, double tolerance = 1e-6)
        {
            containment = EContainment.Inside;

            int numEdges = _faces.Count * 3;

            // STRICTLY clockwise orientation
            int visited = 0;
            HalfEdge2 current = startFace.HalfEdge;

            int toTheRightCount = 0;
            while (toTheRightCount != 3)
            {
                visited++;

                if (visited > numEdges)
                {
                    throw new Exception("Infinite loop detected.");
                }

                if (VertexOverlap(current, point, tolerance))
                {
                    containment = EContainment.OnVertex;
                    break;
                }

                if (VertexOnEdge(current, point, tolerance))
                {
                    containment = EContainment.OnEdge;
                    break;
                }

                if (ToTheRight(current.Vertex.Position, current.Next.Vertex.Position, point))
                {
                    toTheRightCount++;
                    current = current.Next;
                }
                else
                {
                    if (current.Twin is null)
                    {
                        if (toTheRightCount == 2)
                        {
                            containment = EContainment.Outside;
                            break;
                        }
                        current = current.Next;
                    }
                    else
                    {
                        current = current.Twin.Next;
                    }
                    toTheRightCount = 0;
                }
            }

            Console.WriteLine($"Steps: {visited}/{_faces.Count * 3}");
            return current;
        }

        bool ToTheRight(Vec2 start, Vec2 end, Vec2 point)
        {
            return (start - end).Cross(point - start) > 0;
        }

        bool VertexOverlap(HalfEdge2 edge, Vec2 point, double tolerance)
        {
            return 
                edge.Vertex.Position.AlmostEquals(point, tolerance) 
                || 
                edge.Next.Vertex.Position.AlmostEquals(point, tolerance);
        }

        bool VertexOnEdge(HalfEdge2 edge, Vec2 point, double tolerance)
        {
            Vec2 start = edge.Vertex.Position;
            Vec2 end = edge.Next.Vertex.Position;

            double cross = (end - start).Cross(point - start);
            double distance = cross * cross / (end - start).SquareLength();
            return MathHelper.IsZero(distance, tolerance);
        }

        public void Split(Face2 face, Vec2 point)
        {
            for (int i = 0; i < 3; i++)
            {
                HalfEdge2 edge = face.GetEdge(i);

        
            }

            // 3 triangles must be formed

        }

        public class Vertex2
        {
            public int Index { get; set; }
            public Vec2 Position { get; set; }
            public HalfEdge2 HalfEdge { get; set; }
        }

        public class HalfEdge2
        {
            public HalfEdge2(Vertex2 vertex, Face2 face)
            {
                Vertex = vertex;
                Face = face;
                vertex.HalfEdge = this;
            }

            public Vertex2 Vertex { get; set; }
            public Face2 Face { get; set; }
            public HalfEdge2 Next { get; set; } = null!;
            public HalfEdge2? Twin { get; set; } = null;

            public Vertex2 GetVertex(int index)
            {
                switch (index)
                {
                    case 0: return Vertex;
                    case 1: return Next.Vertex;
                    default:
                        throw new IndexOutOfRangeException($"Invalid index '{index}'. Edge has only two vertices.");
                }
            }

            public int IndexOf(Vertex2 vertex)
            {
                if (Vertex == vertex) return 0;
                if (Next.Vertex == vertex) return 1;
                return -1;
            }

            public bool Contains(Vertex2 vertex)
            {
                return IndexOf(vertex) != -1;
            }

            public override string ToString()
            {
                return $"{Vertex.Index},{Next.Vertex.Index}";
            }
        }

        public class Face2 : IEnumerable<HalfEdge2>
        {
            public int Index { get; set; }
            public HalfEdge2 HalfEdge { get; set; }

            public int IndexOf(Vertex2 vertex)
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

            public bool Contains(Vertex2 vertex)
            {
                return IndexOf(vertex) != -1;
            }

            public Vertex2 GetVertex(int index)
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

            public HalfEdge2 GetEdge(int index)
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

            public IEnumerator<HalfEdge2> GetEnumerator()
            {
                HalfEdge2 current = HalfEdge;
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
                return $"({Index}) {string.Join(',', indices)}";
            }
        }
    }
}
