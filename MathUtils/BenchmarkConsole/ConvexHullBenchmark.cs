using BenchmarkDotNet.Attributes;
using CvxLib;
using GeometryLib;
using LinearAlgebraLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GeometryLib.ConvexHull;

namespace BenchmarkConsole
{
    public class ConvexHullBenchmark
    {
        Vec3[] points;
        int rounds = 10;

        public ConvexHullBenchmark()
        {
            points = PointsGenerator.Sphere(50, 500);
            points = PointsGenerator.RandomPointCloud(1000, 500);
            points = Onion.Points;
        }

        [Benchmark]
        public Mesh[] OldMethod()
        {
            Mesh[] meshes = new Mesh[rounds];
            for (int i = 0; i < meshes.Length; i++)
            {
                meshes[i] = ConvexHull.Calculate(points);
            }
            return meshes;
        }

        [Benchmark]
        public CVX[] NewMethod()
        {
            CVX[] meshes = new CVX[rounds];
            for (int i = 0; i < meshes.Length; i++)
            {
                meshes[i] = new CVX(points).Triangulate();
            }
            return meshes;
        }
    }
}
