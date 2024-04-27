using LinearAlgebraLib;
using Newtonsoft.Json;
using System;

namespace MathUtilsConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Vec2[] vertices = [new Vec2(95.831, 48.003), new Vec2(170.185, 165.529)];

            foreach (var item in vertices)
            {
                Console.WriteLine(item);
            }
        }
    }
}
