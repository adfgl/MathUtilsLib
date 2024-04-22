using LinearAlgebraLib;
using Newtonsoft.Json;
using System;

namespace MathUtilsConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var v = new Vec2(200, 200);
            var n = v.Normalize();
            Console.WriteLine(n.Length());
            Console.WriteLine(n.SquareLength());
        }
    }
}
