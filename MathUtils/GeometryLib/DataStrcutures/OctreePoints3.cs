using LinearAlgebraLib;
using System.Diagnostics;

namespace GeometryLib.DataStrcutures
{
    public interface IPointsContainer
    {
        Vec3 GetVertex(int index);
        int AddVertex(Vec3 vertex);
        int Count { get; }
    }

    public class OctreePoints3
    {
        public const int MIN_LEVEL = 2;
        public const int MAX_LEVEL = 10;

        OctreeNode s_root;
        double _initialSize;
        int _maxLevel;

        public OctreePoints3(Box box, int expectedNumberOfPoints)
        {
            var (min, max) = box;
            Vec3 center = (min + max) * 0.5;
            double maxSize = double.MinValue;
            for (int i = 0; i < 3; i++)
            {
                double size = max[i] - min[i];
                if (size > maxSize) maxSize = size;
            }

            s_root = new OctreeNode(center.x, center.y, center.z, 0);
            _initialSize = maxSize * 1.1;
            _maxLevel = CalculateMaxLevel(expectedNumberOfPoints);
        }

        public static int CalculateMaxLevel(int count)
        {
            int levels = (int)Math.Pow(count, 0.125);
            return Math.Clamp(levels, MIN_LEVEL, MAX_LEVEL);
        }

        public int Insert(IPointsContainer container, Vec3 point, double tolerance)
        {
            if (false == IsPointWithinBounds(point))
            {
                throw new InvalidOperationException("Point must be inside bounds.");
            }

            int index = Insert(container, s_root, point, _initialSize, 0, tolerance);
            if (index >= container.Count)
            {
                container.AddVertex(point);
            }
            return index;
        }

        public int IndexOf(IPointsContainer container, Vec3 point, double tolerance)
        {
            int index = IndexOf(container, s_root, point, tolerance);
            return index;
        }

        public bool Contains(IPointsContainer container, Vec3 point, double tolerance)
        {
            return IndexOf(container, s_root, point, tolerance) != -1;
        }

        bool IsPointWithinBounds(Vec3 point)
        {
            double halfSize = _initialSize / 2.0;
            return
                Math.Abs(point.x) <= halfSize &&
                Math.Abs(point.y) <= halfSize &&
                Math.Abs(point.z) <= halfSize;
        }

        int GetPointIndexInsideNode(IPointsContainer container, OctreeNode node, Vec3 point, double tolerance)
        {
            int index = -1;
            for (int i = 0; i < node.Indices.Count; i++)
            {
                if (container.GetVertex(node.Indices[i]).AlmostEquals(point, tolerance))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        int IndexOf(IPointsContainer container, OctreeNode node, Vec3 point, double tolerance)
        {
            if (node.IsLeaf)
            {
                int existingIndex = GetPointIndexInsideNode(container, node, point, tolerance);
                if (existingIndex != -1)
                {
                    return node.Indices[existingIndex];
                }
                return -1;
            }
            else
            {
                int childIndex = GetChildIndex(node, point);
                return IndexOf(container, node.Children[childIndex], point, tolerance);
            }
        }

        int Insert(IPointsContainer container, OctreeNode node, Vec3 point, double size, int level, double tolerance)
        {
            if (node.IsLeaf)
            {
                if (level < _maxLevel)
                {
                    Split(node, size, level);
                    int childIndex = GetChildIndex(node, point);
                    return Insert(container, node.Children[childIndex], point, size / 2, level + 1, tolerance);
                }
                else
                {
                    int existingIndex = GetPointIndexInsideNode(container, node, point, tolerance);
                    if (existingIndex == -1)
                    {
                        int newIndex = container.AddVertex(point);
                        node.Indices.Add(newIndex);
                        return newIndex;
                    }
                    return node.Indices[existingIndex];
                }
            }
            else
            {
                int childIndex = GetChildIndex(node, point);
                return Insert(container, node.Children[childIndex], point, size / 2, level + 1, tolerance);
            }
        }

        void Split(OctreeNode node, double size, int level)
        {
            if (node.Level == _maxLevel) return;

            double newSize = size / 2;
            double offset = newSize / 2;
            int newLevel = level + 1;

            // front
            node.Children[0] = new OctreeNode(node.X - offset, node.Y - offset, node.Z - offset, newLevel);
            node.Children[1] = new OctreeNode(node.X + offset, node.Y - offset, node.Z - offset, newLevel);
            node.Children[2] = new OctreeNode(node.X - offset, node.Y + offset, node.Z - offset, newLevel);
            node.Children[3] = new OctreeNode(node.X + offset, node.Y + offset, node.Z - offset, newLevel);

            // back
            node.Children[4] = new OctreeNode(node.X - offset, node.Y - offset, node.Z + offset, newLevel);
            node.Children[5] = new OctreeNode(node.X + offset, node.Y - offset, node.Z + offset, newLevel);
            node.Children[6] = new OctreeNode(node.X - offset, node.Y + offset, node.Z + offset, newLevel);
            node.Children[7] = new OctreeNode(node.X + offset, node.Y + offset, node.Z + offset, newLevel);
        }

        int GetChildIndex(OctreeNode node, Vec3 point)
        {
            int index = 0;
            if (point.x >= node.X) index |= 1;
            if (point.y >= node.Y) index |= 2;
            if (point.z >= node.Z) index |= 4;
            return index;
        }

        [DebuggerDisplay("{X} {Y} {Z} ({Indices.Count})")]
        class OctreeNode
        {
            public OctreeNode(double x, double y, double z, int level)
            {
                X = x;
                Y = y;
                Z = z;
                Level = level;
                Children = new OctreeNode[8];
                Indices = new List<int>();
            }

            public double X { get; }
            public double Y { get; }
            public double Z { get; }
            public int Level { get; }
            public OctreeNode[] Children { get; }

            public List<int> Indices { get; }

            public bool IsLeaf => Children[0] == null;
        }
    }
}
