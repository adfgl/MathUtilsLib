using LinearAlgebraLib;

namespace MathUtilsConsole
{
    internal class Program
    {

        static void Main(string[] args)
        {
            List<Vec3> points = new List<Vec3>()
            {
                new Vec3(0, 0, 0),
                new Vec3(1, 0, 0),
                new Vec3(0, 1, 0),
                new Vec3(0, 0, 1),
                new Vec3(1, 1, 0),
                new Vec3(1, 0, 1),
                new Vec3(1, 0, 1),
            };

            TriMesh mesh = new TriMesh(points);
            mesh.Add(0, 1, 2);
            mesh.Add(3, 1, 2);

            mesh.Add(2, 4, 3);
            mesh.Add(5, 4, 2);
            mesh.Add(0, 2, 5);
            mesh.Add(0, 6, 5);

            mesh.Kill(4);


            foreach (Face item in mesh.Triangles)
            {
                Console.WriteLine(item);
            }
        }
    }
}
