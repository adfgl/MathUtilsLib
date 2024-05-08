using DataPoolLib;
using GeometryLib;
using LinearAlgebraLib;
using System.Collections;

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

            // Add faces
            mesh.AddFace(mesh.Vertices[0], mesh.Vertices[1], mesh.Vertices[3]);
            mesh.AddFace(mesh.Vertices[3], mesh.Vertices[1], mesh.Vertices[2]);

            foreach (var item in mesh.Faces)
            {
                Console.WriteLine(item);
            }
        }
    }


}
