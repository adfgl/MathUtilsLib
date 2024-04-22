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
            string json = JsonConvert.SerializeObject(v);
            Console.WriteLine(json);
            Vec2 person = JsonConvert.DeserializeObject<Vec2>(json);
        }
    }
}
