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

        public Mesh3(Mesh3Raw raw)
        {
            _triangles = new TriPool(raw.Triangles);
            _normals = new Vec3Pool(raw.Normals);
            _vertices = new Vec3Pool(raw.Vertices);
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

        public void Invert(int index)
        {
            _triangles.Invert(index);
            _normals.SetVertex(index, -_normals.GetVertex(index));
        }

        public int AddTriangle(OctreePoints3 octree, Vec3 p1, Vec3 p2, Vec3 p3, int color, double tolerance = 0)
        {
            int a = octree.IndexOf(_vertices, p1, tolerance);
            if (a == -1)
            {
                octree.Insert(_vertices, p1, tolerance);
                s_boundingBoxCalculated = false;
            }

            int b = octree.IndexOf(_vertices, p2, tolerance);
            if (b == -1)
            {
                octree.Insert(_vertices, p2, tolerance);
                s_boundingBoxCalculated = false;
            }

            int c = octree.IndexOf(_vertices, p3, tolerance);
            if (c == -1)
            {
                octree.Insert(_vertices, p3, tolerance);
                s_boundingBoxCalculated = false;
            }

            _triangles.AddTriangle(a, b, c, color);
            _normals.AddVertex((p2 - p1).Cross(p3 - p1).Normalize());

            return _triangles.Count;
        }

        public int AddTriangle(Vec3 p1, Vec3 p2, Vec3 p3, int color, double tolerance = 0)
        {
            int a = _vertices.IndexOf(p1, tolerance);
            if (a == -1)
            {
                _vertices.AddVertex(p1);
                s_boundingBoxCalculated = false;
            }

            int b = _vertices.IndexOf(p2, tolerance);
            if (b == -1)
            {
                _vertices.AddVertex(p2);
                s_boundingBoxCalculated = false;
            }

            int c = _vertices.IndexOf(p3, tolerance);
            if (c == -1)
            {
                _vertices.AddVertex(p3);
                s_boundingBoxCalculated = false;
            }

            _triangles.AddTriangle(a, b, c, color);
            _normals.AddVertex((p2 - p1).Cross(p3 - p1).Normalize());

            return _triangles.Count;
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
