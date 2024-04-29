using DataPoolLib;
using GeometryLib;
using LinearAlgebraLib;
using Newtonsoft.Json;
using System;
using System.Drawing;

namespace MathUtilsConsole
{
    internal class Program
    {
        static Random random = new Random(42);

        static void Main(string[] args)
        {
            int points = 5;
            int size = 1000;
            var s_points = GenerateRandomPoints(points, -size, size, -size, size, -size, size);

            PointsContainer s_pointsContainer = new PointsContainer();
            Octree s_octtreeContainer = new Octree(s_pointsContainer, Vec3.Zero, 2.5 * size, s_points.Count);

            foreach (var item in s_points)
            {
                s_octtreeContainer.Insert(item, 0);
            }
        }
        public class PointsContainer : IPointsContainer
        {
            public List<Vec3> Points { get; set; } = new List<Vec3>();

            public Vec3 GetPoint(int index)
            {
                return Points[index];
            }

            public int AddPoint(Vec3 point)
            {
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
                double randomX = random.NextDouble() * (maxX - minX) + minX;
                double randomY = random.NextDouble() * (maxY - minY) + minY;
                double randomZ = random.NextDouble() * (maxZ - minZ) + minZ;

                points.Add(new Vec3(randomX, randomY, randomZ));
            }

            return points;
        }
    }
}
