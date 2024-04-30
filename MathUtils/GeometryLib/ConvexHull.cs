using LinearAlgebraLib;
using System.Diagnostics;

namespace GeometryLib
{
    public static class ConvexHull
    {
        public const int NO_ADJACENT = -1;

        public static void Calculate(Vec3[] uniquePoints, double tolerance)
        {
         
        }

        
        public class Face
        {
            readonly int[] _indices = new int[3];
            readonly int[] _adjacent = new int[3];
            Plane _plane;

            public Face(int a, int b, int c, Plane plane)
            {
                _plane = plane;

                _indices[0] = a;
                _indices[1] = b;
                _indices[2] = c;

                _adjacent[0] = NO_ADJACENT;
                _adjacent[1] = NO_ADJACENT;
                _adjacent[2] = NO_ADJACENT;
            }

            public int[] Indices => _indices;
            public int[] Adjacent => _adjacent;
            public Plane Plane => _plane;

            public void Flip()
            {
                _plane = Plane.Flip();

                int temp = _indices[0];
                _indices[0] = _indices[2];
                _indices[2] = temp;

                temp = _adjacent[0];
                _adjacent[0] = _adjacent[1];
                _adjacent[1] = temp;
            }

            public override string ToString()
            {
                return $"pts: {_indices[0]} {_indices[1]} {_indices[2]} adj: {_adjacent[0]} {_adjacent[1]} {_adjacent[2]}";
            }
        }
    }
}
