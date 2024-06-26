﻿namespace LinearAlgebraLib
{
    public class Trans2
    {
        bool s_backwardIsValid;
        bool s_forwardIsValid;
        Stack<Mat3> s_matrices;

        Mat3 _source;
        Mat3 _inverse;
        bool _isDirty;

        public Trans2(int capacity = 5)
        {
            s_matrices = new Stack<Mat3>(capacity);
            Reset();
        }

        public bool IsDirty => _isDirty;
        
        public Mat3 Source
        {
            get
            {
                UpdateForward();
                return _source;
            }
        }

        public void Reset()
        {
            _source = Mat3.Identity;
            _inverse = Mat3.Identity;
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

        public Trans2 Translate(double x, double y)
        {
            s_matrices.Push(Mat3.Translation(x, y));
            OnSourceMatrixChanged();
            return this;
        }
        public Trans2 Translate(Vec2 v) => Translate(v.x, v.y);

        public Trans2 Rotate(double rad)
        {
            s_matrices.Push(Mat3.Rotation(rad));
            OnSourceMatrixChanged();
            return this;
        }
        public Trans2 RotateAt(double x, double y, double rad)
        {
            s_matrices.Push(Mat3.Translation(-x, -y));
            s_matrices.Push(Mat3.Rotation(rad));
            s_matrices.Push(Mat3.Translation(x, y));
            OnSourceMatrixChanged();
            return this;
        }

        public Trans2 Scale(double sx, double sy)
        {
            s_matrices.Push(Mat3.Scale(sx, sy));
            OnSourceMatrixChanged();
            return this;
        }

        public Trans2 ScaleAt(double x, double y, double sx, double sy)
        {
            s_matrices.Push(Mat3.Translation(-x, -y));
            s_matrices.Push(Mat3.Scale(sx, sy));
            s_matrices.Push(Mat3.Translation(x, y));
            OnSourceMatrixChanged();
            return this;
        }

        public Trans2 Scale(double s) => Scale(s, s);
        public Trans2 ScaleAt(double x, double y, double s) => ScaleAt(x, y, s, s);

        public Vec2 Forward(Vec2 v)
        {
            if (false == _isDirty) return v;
            UpdateForward();
            return _source * v;
        }

        public Vec2 Backward(Vec2 v)
        {
            if (false == _isDirty) return v;
            UpdateForward();
            UpdateBackward();
            return _inverse * v;
        }
    }
}
