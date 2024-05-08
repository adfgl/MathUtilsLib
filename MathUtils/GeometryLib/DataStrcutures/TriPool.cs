using DataPoolLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryLib.DataStrcutures
{
    public class TriPool : PoolBase<int>
    {
        public TriPool(int[] values) : base(values, 4)
        {

        }

        public TriPool(TriPool triangles) : base(triangles)
        {

        }

        public TriPool(int expectedLength) : base(expectedLength, 4)
        {
        }

        public int RemoveDuplicates()
        {
            PoolBase<int> unique = PoolDuplicatesEx.RemoveDuplicates(this);
            int difference = Count - unique.Count;
            if (difference != 0)
            {
                Clear();
                AddRange(unique);
            }
            return difference;
        }

        public void Invert(int index)
        {
            int a = GetComponent(index, 0);
            int c = GetComponent(index, 2);
            SetComponent(index, 0, c);
            SetComponent(index, 2, a);
        }

        public void AddTriangle(int a, int b, int c, int color)
        {
            AddItem(a, b, c, color);
        }

        public void GetTriangle(int index, out int a, out int b, out int c, out int color)
        {
            int offset = index * NumComponents;
            a = _values[offset];
            b = _values[offset + 1];
            c = _values[offset + 2];
            color = _values[offset + 3];
        }

        public void SetTriangle(int index, int a, int b, int c, int color)
        {
            int offset = index * NumComponents;
            _values[offset] = a;
            _values[offset + 1] = b;
            _values[offset + 2] = c;
            _values[offset + 3] = color;
        }
    }
}
