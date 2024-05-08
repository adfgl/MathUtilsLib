﻿using DataPoolLib;
using GeometryLib;
using LinearAlgebraLib;
using System.Collections;

namespace MathUtilsConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var mesh = new Mesh();

            // Add vertices
            mesh.AddVertex(new Vec3(41.625, 104.128, 0));
            mesh.AddVertex(new Vec3(115.676, 170.504, 0));
            mesh.AddVertex(new Vec3(210.657, 154.673, 0));
            mesh.AddVertex(new Vec3(133.425, 87.036, 0));

            // Add faces
            mesh.AddFace(mesh.Vertices[3], mesh.Vertices[0], mesh.Vertices[1]);
            mesh.AddFace(mesh.Vertices[3], mesh.Vertices[1], mesh.Vertices[2]);
        }
    }

    public class HEDS3
    {
        readonly List<Vertex3> _vertices = new List<Vertex3>();
        readonly List<Face3> _faces = new List<Face3>();

        public class Mesh3
        {

        }

        public Vertex3 AddVertex(Vec3 position)
        {
            Vertex3 vertex = new Vertex3()
            {
                Index = _vertices.Count,
                Position = position,
                HalfEdge = null
            };
            _vertices.Add(vertex);
            return vertex;
        }

        public Face3 AddFace(Vertex3 a, Vertex3 b, Vertex3 c)
        {
            Face3 face = new Face3();

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
