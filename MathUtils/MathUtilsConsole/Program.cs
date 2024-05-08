using DataPoolLib;
using GeometryLib.DataStrcutures;
using LinearAlgebraLib;
using System.Collections;
using static GeometryLib.DataStrcutures.HalfEdgeMesh2;
using static GeometryLib.DataStrcutures.HalfEdgeMesh3;

namespace MathUtilsConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var mesh = new HalfEdgeMesh2();

            // Add vertices
            mesh.AddVertex(new Vec2(41.625, 104.128));
            mesh.AddVertex(new Vec2(115.676, 170.504));
            mesh.AddVertex(new Vec2(210.657, 154.673));
            mesh.AddVertex(new Vec2(133.425, 87.036));
            mesh.AddVertex(new Vec2(233.224, 87.791));
            mesh.AddVertex(new Vec2(85.284, 43.658));
            mesh.AddVertex(new Vec2(186.021, 11.998));

            // Add faces
            mesh.AddFace(mesh.Vertices[0], mesh.Vertices[1], mesh.Vertices[3]);
            mesh.AddFace(mesh.Vertices[3], mesh.Vertices[1], mesh.Vertices[2]);
            mesh.AddFace(mesh.Vertices[5], mesh.Vertices[0], mesh.Vertices[3]);
            mesh.AddFace(mesh.Vertices[3], mesh.Vertices[2], mesh.Vertices[4]);
            mesh.AddFace(mesh.Vertices[5], mesh.Vertices[3], mesh.Vertices[6]);
            mesh.AddFace(mesh.Vertices[3], mesh.Vertices[4], mesh.Vertices[6]);


            foreach (var item in mesh.Faces)
            {
                Console.WriteLine(item);
                foreach (var d in item)
                {
                    Console.WriteLine($"'{d.Twin}'");
                }
                Console.WriteLine();
            }

            Vec2 p = new Vec2(133.425, 87.036);

            var edge = mesh.ContainedEdge(p, mesh.Faces[0], out EContainment containment);
            Console.WriteLine(edge.Face);
            Console.WriteLine(edge);
            Console.WriteLine(containment);

        }

    }


}
