using LinearAlgebraLib;
using Newtonsoft.Json;
using System;

namespace MathUtilsConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var m = Mat4.Identity;
            m.ToConsole();

            var v = Vec3.UnitY;
            v.ToConsole();
        }
    }
}
