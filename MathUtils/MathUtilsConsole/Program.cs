using LinearAlgebraLib;
using Newtonsoft.Json;
using System;

namespace MathUtilsConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Vec2[] vertices = [new Vec2(-35.468, 187.877)];
            foreach (var item in vertices)
            {
                Console.WriteLine(item);
            }

            Trans2 trans2 = new Trans2().RotateAt(Math.PI / 4, 176.275, 249.714);

            Console.WriteLine();

            foreach (var item in vertices)
            {
                Console.WriteLine(trans2.Forward(item));
            }
        }
    }
}
