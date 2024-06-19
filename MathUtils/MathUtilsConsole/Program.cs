using DataPoolLib;
using GeometryLib;
using GeometryLib.DataStrcutures;
using LinearAlgebraLib;
using System.Collections;

namespace MathUtilsConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }


        public static byte AddPoint(Vec3 point, Vec3 center)
        {
            var (x0, y0, z0) = center;
            var (x, y, z) = point;

            byte container;
            if (x0 < x)
            {
                if (y0 < y)
                {
                    if (z0 < z)
                    {
                        container = 0;
                    }
                    else
                    {
                        container = 1;
                    }
                }
                else
                {
                    if (z0 < z)
                    {
                        container = 2;
                    }
                    else
                    {
                        container = 3;
                    }
                }
            }
            else
            {
                if (y0 < y)
                {
                    if (z0 < z)
                    {
                        container = 4;
                    }
                    else
                    {
                        container = 5;
                    }
                }
                else
                {
                    if (z0 < z)
                    {
                        container = 6;
                    }
                    else
                    {
                        container = 7;
                    }
                }
            }
            return container;
        }
    }

}
