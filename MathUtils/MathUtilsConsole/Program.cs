using DataPoolLib;
using GeometryLib;
using LinearAlgebraLib;
using System.Collections;
using static GeometryLib.HalfEdgeMesh3;

namespace MathUtilsConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var mesh = new HalfEdgeMesh3();

            // Add vertices
            mesh.AddVertex(new Vec3(41.625, 104.128, 0));
            mesh.AddVertex(new Vec3(115.676, 170.504, 0));
            mesh.AddVertex(new Vec3(210.657, 154.673, 0));
            mesh.AddVertex(new Vec3(133.425, 87.036, 0));

            mesh.AddVertex(new Vec3(233.224, 87.791, 0));
            mesh.AddVertex(new Vec3(85.284, 43.658, 0));
            mesh.AddVertex(new Vec3(186.021, 11.998, 0));

            // Add faces
            mesh.AddFace(mesh.Vertices[0], mesh.Vertices[1], mesh.Vertices[3]);
            mesh.AddFace(mesh.Vertices[3], mesh.Vertices[1], mesh.Vertices[2]);
            mesh.AddFace(mesh.Vertices[5], mesh.Vertices[0], mesh.Vertices[3]);
            mesh.AddFace(mesh.Vertices[3], mesh.Vertices[2], mesh.Vertices[4]);
            mesh.AddFace(mesh.Vertices[5], mesh.Vertices[3], mesh.Vertices[6]);
            mesh.AddFace(mesh.Vertices[3], mesh.Vertices[4], mesh.Vertices[6]);

            foreach (Face3 face in mesh.Faces)
            {
                Console.WriteLine();
                Console.WriteLine(face);
                foreach (HalfEdge3 edge in face)
                {
                    Console.WriteLine(edge);
                    //if (edge.Twin != null)
                    //{
                    //    Console.WriteLine(edge.Twin + $" [{edge.Twin.Face.Index}]");
                    //}
                }
            }
        }
    }


}
