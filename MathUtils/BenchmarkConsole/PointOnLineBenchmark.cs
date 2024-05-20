using BenchmarkDotNet.Attributes;
using LinearAlgebraLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenchmarkConsole
{
    public class PointOnLineBenchmark
    {

        static int m_steps = 10000;

        Vec2 a = new Vec2(-98.592, 95.411);
        Vec2 b = new Vec2(-79.528, 123.371);
        Vec2 c = new Vec2(-89.06, 109.391);

        [Benchmark]
        public bool Roots()
        {
            bool result = false;
            for (int i = 0; i < m_steps; i++)
            {
                result = PointOnLine(c, a, b);
            }
            return result;
        }

        [Benchmark]
        public bool Direct()
        {
            bool result = false;
            for (int i = 0; i < m_steps; i++)
            {
                result = PointOnLine2(c, a, b);
            }
            return result;
        }

        public static bool PointOnLine(Vec2 p, Vec2 s, Vec2 e, double tolerance = 1e-6)
        {
            Vec2 sp = p - s;
            Vec2 se = e - s;
            double cross = sp.Cross(se);
            if (Math.Abs(cross) > tolerance)
            {
                return false;
            }

            double dot = sp.Dot(se);
            if (dot < 0 || dot > se.SquareLength())
            {
                return false;
            }
            return true;
        }

        public static bool PointOnLine2(Vec2 p, Vec2 s, Vec2 e, double tolerance = 1e-6)
        {
            double d1 = (p - s).Length() + (p - e).Length();
            double d2 = (s - e).Length();
            return Math.Abs(d1 - d2) < tolerance;
        }
    }
}
