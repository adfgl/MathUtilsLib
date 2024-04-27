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

        public Trans3 Translate(double x, double y, double z)
        {
            s_matrices.Push(Mat4.Translation(x, y, z));
            OnSourceMatrixChanged();
            return this;
        }

        public Trans3 RotateX(double rad)
        {
            s_matrices.Push(Mat4.RotationX(rad));
            OnSourceMatrixChanged();
            return this;
        }

        public Trans3 RotateY(double rad)
        {
            s_matrices.Push(Mat4.RotationY(rad));
            OnSourceMatrixChanged();
            return this;
        }

        public Trans3 RotateZ(double rad)
        {
            s_matrices.Push(Mat4.RotationZ(rad));
            OnSourceMatrixChanged();
            return this;
        }

        public Trans3 Scale(double scaleX, double scaleY, double scaleZ)
        {
            s_matrices.Push(Mat4.Scale(scaleX, scaleY, scaleZ));
            OnSourceMatrixChanged();
            return this;
        }

        public Trans3 Scale(double scale) => Scale(scale, scale, scale);

        public Vec3 Forward(Vec3 v)
        {
            if (false == _isDirty) return v;
            UpdateForward();
            return _source * v;
        }

        public Vec3 Backward(Vec3 v)
        {
            if (false == _isDirty) return v;
            UpdateForward();
            UpdateBackward();
            return _inverse * v;
        }
    }
}
