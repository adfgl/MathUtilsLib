using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryLib
{
    public class Mesh3Raw
    {
        public Mesh3Raw()
        {

        }

        public Mesh3Raw(Mesh3 mesh)
        {
            Vertices = mesh.Vertices.ToBufferArray();
            Triangles = mesh.Triangles.ToBufferArray();
            Normals = mesh.Normals.ToBufferArray();
        }

        public Mesh3Raw(double[] points3, int[] indices, double[]? normals)
        {
            if (points3.Length % 3 != 0)
                throw new ArgumentException("Points3 must be a multiple of 3");

            if (indices.Length % 3 != 0)
                throw new ArgumentException("Indices must be a multiple of 3");

            Vertices = points3;
            Triangles = indices;

            if (normals is not null && normals.Length != 0)
            {
                if (normals.Length % 3 != 0 || normals.Length != indices.Length)
                    throw new ArgumentException("Normals must be a multiple of 3");

                Normals = normals;
            }
        }

        public int PointsCount => Triangles.Length / 3;
        public int TriangleCount => Triangles.Length / 3;

        public double[] Vertices { get; set; } = Array.Empty<double>();
        public int[] Triangles { get; set; } = Array.Empty<int>();
        public double[] Normals { get; set; } = Array.Empty<double>();
    }
}
