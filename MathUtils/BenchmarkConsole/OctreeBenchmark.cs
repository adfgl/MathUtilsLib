using BenchmarkDotNet.Attributes;
using GeometryLib;
using LinearAlgebraLib;
using System.Drawing;

namespace BenchmarkConsole
{
    public class OctreeBenchmark
    {
        // Search test: prepopulated with 1000 random points
        // | Method     | Mean        | Error     | StdDev    |
        // |----------- |------------:|----------:|----------:|
        // | BruteForce | 16,705.0 ns | 172.77 ns | 144.27 ns |
        // | Octree     |    335.6 ns |   5.69 ns |   5.04 ns |

        readonly static Random s_random = new Random(42);
        readonly static int s_iterations = 100;
        readonly static double s_size = 1000;
        readonly static int s_numPoints = 1000;

        PointsContainer s_pointsContainer;
        Octree s_octtreeContainer;
        List<Vec3> s_points;

        public OctreeBenchmark()
        {
            s_points =  GenerateRandomPoints(s_numPoints, -s_size, s_size, -s_size, s_size, -s_size, s_size);
            s_pointsContainer = new PointsContainer();
            s_octtreeContainer = new Octree(s_pointsContainer, Vec3.Zero, 2.5 * s_size);
            foreach (Vec3 point in s_points)
            {
                s_pointsContainer.AddPoint(point);

            }

            foreach (Vec3 point in s_points)
            {
                s_octtreeContainer.Insert(point, 0);
            }
        }

        [Benchmark]
        public int BruteForce()
        {
            Vec3 p = s_points.Last();
            int index = -1;
            for (int i = 0; i < s_iterations; i++)
            {
                index = s_pointsContainer.IndexOf(p, 0);
            }
            return index;
        }

        [Benchmark]
        public int Octree()
        {
            Vec3 p = s_points.Last();
            int index = -1;
            for (int i = 0; i < s_iterations; i++)
            {
                index = s_pointsContainer.IndexOf(p, 0);
            }
            return index;
        }

        public class PointsContainer : IPointsContainer
        {
            public List<Vec3> Points { get; set; } = new List<Vec3>();

            public int IndexOf(Vec3 point, double tolerance)
            {
                int index = -1;
                int steps = 0;
                for (int i = 0; i < Points.Count; i++)
                {
                    steps++;
                    Vec3 p = Points[i];
                    if (p.AlmostEquals(point, tolerance))
                    {
                        index = i;
                        break;
                    }
                }
                return index;
            }

            public Vec3 GetPoint(int index)
            {
                return Points[index];
            }

            public int AddPoint(Vec3 point)
            {
                for (int i = 0; i < Points.Count; i++)
                {
                    Vec3 p = Points[i];
                    if (p.AlmostEquals(point, 0))
                    {
                        return i;
                    }
                }
                Points.Add(point);
                return Points.Count;
            }

            public int Count => Points.Count;
        }

        public static List<Vec3> GenerateRandomPoints(int count, double minX, double maxX, double minY, double maxY, double minZ, double maxZ)
        {
            List<Vec3> points = new List<Vec3>();

            for (int i = 0; i < count; i++)
            {
                double randomX = s_random.NextDouble() * (maxX - minX) + minX;
                double randomY = s_random.NextDouble() * (maxY - minY) + minY;
                double randomZ = s_random.NextDouble() * (maxZ - minZ) + minZ;

                points.Add(new Vec3(randomX, randomY, randomZ));
            }

            return points;
        }
    }
}
