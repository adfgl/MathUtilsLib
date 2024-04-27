using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinearAlgebraLib
{
    public class Trans3
    {
        bool s_backwardIsValid;
        bool s_forwardIsValid;
        Stack<Mat4> s_matrices;

        Mat4 _source;
        Mat4 _inverse;
        bool _isDirty;

        public Trans3(int capacity = 5)
        {
            s_matrices = new Stack<Mat4>(capacity);
            Reset();
        }

        public bool IsDirty => _isDirty;

        public Mat4 Source
        {
            get
            {
                UpdateForward();
                return _source;
            }
        }

        public void Reset()
        {
            _source = Mat4.Identity;
            _inverse = Mat4.Identity;
            _isDirty = false;
            s_matrices.Clear();
            s_backwardIsValid = true;
            s_forwardIsValid = true;
        }

        void OnSourceMatrixChanged()
        {
            _isDirty = true;
            s_backwardIsValid = false;
            s_forwardIsValid = false;
        }

        void UpdateForward()
        {
            if (s_forwardIsValid) return;
            while (s_matrices.Count > 0)
            {
                _source *= s_matrices.Pop();
            }
            s_forwardIsValid = true;
        }

        void UpdateBackward()
        {
            if (s_backwardIsValid) return;
            if (false == _source.Inverse(out _inverse))
            {
                throw new DivideByZeroException("Can't invert matrix. Determinant is zero.");
            }
            s_backwardIsValid = true;
        }

    }
}
