using DataPoolLib;
using LinearAlgebraLib;

namespace GeometryLib
{
    public class Mesh3
    {
        bool s_boundingBoxCalculated = false;

        TriPool _triangles;
        Vec3Pool _vertices;
        Vec3Pool _normals;
        Box s_boundingBox;

        public Mesh3(int expectedTriangles, int expectedVertices)
        {
            _triangles = new TriPool(expectedTriangles);
            _normals = new Vec3Pool(expectedTriangles);
            _vertices = new Vec3Pool(expectedVertices);
        }

        public TriPool Triangles { get { return _triangles; } }
        public Vec3Pool Vertices { get { return _vertices; } }
        public Vec3Pool Normals { get { return _normals; } }

        public Box BoundingBox
        {
            get
            {
                if (false == s_boundingBoxCalculated)
                {
                    s_boundingBox = Box.FromPoints(_vertices);
                    s_boundingBoxCalculated = true;
                }
                return s_boundingBox;
            }
        }

        public int AddTriangle(Vec3 p1, Vec3 p2, Vec3 p3, int color, double tolerance = 0)
        {
            int a = _vertices.IndexOf(p1, tolerance);
            int b = _vertices.IndexOf(p2, tolerance);
            int c = _vertices.IndexOf(p3, tolerance);

            if (a == -1)
            {
                _vertices.AddVertex(p1);
                s_boundingBoxCalculated = false;
            }
            if (b == -1)
            {
                _vertices.AddVertex(p2);
                s_boundingBoxCalculated = false;
            }
            if (c == -1)
            {
                _vertices.AddVertex(p3);
                s_boundingBoxCalculated = false;
            }

            _triangles.AddTriangle(a, b, c, color);
            _normals.AddVertex((p2 - p1).Cross(p3 - p1).Normalize());

            return _triangles.Count - 1;
        }

        public void Clear()
        {
            _triangles.Clear();
            _vertices.Clear();
            _normals.Clear();
            s_boundingBoxCalculated = true;
            s_boundingBox = new Box(0, 0, 0, 0, 0, 0);
        }
    }
}
