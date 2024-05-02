using DataPoolLib;
using LinearAlgebraLib;
using System.Collections;

namespace GeometryLib
{
    public class Vec3Pool : PoolBase<double>, IEnumerable<Vec3>, IPointsContainer
    {
        public Vec3Pool(double[] values) : base(values, 3)
        {

        }

        public Vec3Pool(Vec3Pool other) : base(other)
        {

        }

        public Vec3Pool(int expectedLength) : base(expectedLength, 3)
        {
        }

        public int RemoveDuplicates()
        {
            PoolBase<double> unique = PoolDuplicatesEx.RemoveDuplicates(this);
            int difference = this.Count - unique.Count;
            if (difference != 0)
            {
                Clear();
                AddRange(unique);
            }
            return difference;
        }

        public void SortByColumn(int index, ESort sort)
        {
            PoolSortingEx.SortByColumn(this, index, sort);
        }

        public int AddVertex(Vec3 vertex)
        {
            return AddItem(vertex.x, vertex.y, vertex.z);
        }

        public Vec3 GetVertex(int index)
        {
            GetItem(index, out double x, out double y, out double z);
            return new Vec3(x, y, z);
        }

        public void SetVertex(int index, Vec3 v)
        {
            SetItem(index, v.x, v.y, v.z);
        }

        public int IndexOf(Vec3 vertex, double tolerance = 0)
        {
            for (int i = 0; i < Count; i++)
            {
                if (vertex.AlmostEquals(GetVertex(i), tolerance)) return i;
            }
            return -1;
        }

        public bool Contains(Vec3 v, double tolerance = 0)
        {
            return IndexOf(v, tolerance) != -1;
        }

        public Vec3 this[int index]
        {
            get { return GetVertex(index); }
            set { SetVertex(index, value); }
        }

        public IEnumerator<Vec3> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return GetVertex(i);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
