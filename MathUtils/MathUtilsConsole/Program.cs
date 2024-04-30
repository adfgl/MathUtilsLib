using DataPoolLib;
using GeometryLib;
using LinearAlgebraLib;

namespace MathUtilsConsole
{
    internal class Program
    {

        static void Main(string[] args)
        {
            List<Vec3> points = new List<Vec3>()
            {
                new Vec3(-100, -100, 0),
                new Vec3(0, 100, 0),
                new Vec3(100, -100, 0),
                new Vec3(0, 0, 150),
            };

            var a = points[0];
            var b = points[1];
            var c = points[2];

            var plane = new Plane(a, b, c);

            //ConvexHull.Mesh mesh = ConvexHull.Calculate(points.ToArray());
            //foreach (ConvexHull.Face item in mesh.Faces)
            //{
            //    Console.WriteLine(item);
            //}

        }
    }
}
