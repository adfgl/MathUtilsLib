using LinearAlgebraLib;
using System.Diagnostics;

namespace DataPoolLib
{
    public interface IPointsContainer
    {
        Vec3 GetVertex(int index);
        int AddVertex(Vec3 vertex);
        int Count { get; }
    }

    public class Octree
    {
        public const int MIN_LEVEL = 2;
        public const int MAX_LEVEL = 10;

        readonly IPointsContainer m_pool;
        OctreeNode s_root;
        double _initialSize;
        int _maxLevel;

        public Octree(IPointsContainer container, Vec3 center, double size, int expectedNumberOfPoints)
        {
            m_pool = container;
            s_root = new OctreeNode(center.x, center.y, center.z, 0);
            _initialSize = size;
            _maxLevel = CalculateMaxLevel(expectedNumberOfPoints);
        }

        public static int CalculateMaxLevel(int count)
        {
            int levels = (int)Math.Pow(count, 0.125);
            return Math.Clamp(levels, MIN_LEVEL, MAX_LEVEL);
        }

        public int Insert(Vec3 point, double tolerance)
        {
            int index = Insert(s_root, point, _initialSize, 0, tolerance);
            if (index >= m_pool.Count)
            {
                m_pool.AddVertex(point);
            }
            return index;
        }

        public int IndexOf(Vec3 point, double tolerance)
        {
            int index = IndexOf(s_root, point, tolerance);
            return index;
        }

        public bool Contains(Vec3 point, double tolerance)
        {
            return IndexOf(s_root, point, tolerance) != -1;
        }

        int GetPointIndexInsideNode(OctreeNode node, Vec3 point, double tolerance)
        {
            int index = -1;
            for (int i = 0; i < node.Indices.Count; i++)
            {
                if (m_pool.GetVertex(node.Indices[i]).AlmostEquals(point, tolerance))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        int IndexOf(OctreeNode node, Vec3 point, double tolerance)
        {
            if (node.IsLeaf)
            {
                int existingIndex = GetPointIndexInsideNode(node, point, tolerance);
                if (existingIndex != -1)
                {
                    return node.Indices[existingIndex];
                }
                return -1;
            }
            else
            {
                int childIndex = GetChildIndex(node, point);
                return IndexOf(node.Children[childIndex], point, tolerance);
            }
        }

        int Insert(OctreeNode node, Vec3 point, double size, int level, double tolerance)
        {
            if (node.IsLeaf)
            {
                if (level < _maxLevel)
                {
                    Split(node, size, level);
                    int childIndex = GetChildIndex(node, point);
                    return Insert(node.Children[childIndex], point, size / 2, level + 1, tolerance);
                }
                else
                {
                    int existingIndex = GetPointIndexInsideNode(node, point, tolerance);
                    if (existingIndex == -1)
                    {
                        int newIndex = m_pool.AddVertex(point);
                        node.Indices.Add(newIndex);
                        return newIndex;
                    }
                    return node.Indices[existingIndex];
                }
            }
            else
            {
                int childIndex = GetChildIndex(node, point);
                return Insert(node.Children[childIndex], point, size / 2, level + 1, tolerance);
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
