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

        public ConvexHullBenchmark()
        {
            points = PointsGenerator.Sphere(20, 500);
            points = PointsGenerator.RandomPointCloud(1000, 500);
            points = Onion.Points;
        }

        [Benchmark]
        public Mesh OldMethod()
        {
            Mesh hull = ConvexHull.Calculate(points);
            return hull;
        }

        [Benchmark]
        public ConvexHull2 NewMethod()
        {
            ConvexHull2 hull = new ConvexHull2(points).Triangulate();
            return hull;
        }
    }
}
