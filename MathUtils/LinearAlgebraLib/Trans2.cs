namespace LinearAlgebraLib
{
    public class Trans2
    {
        bool s_inverseIsValid = false;

        Mat3 _source;
        Mat3 _inverse;
        bool _isDirty;

        public Trans2()
        {
            Reset();
        }

        public Mat3 Source => _source;
        public bool IsDirty => _isDirty;

        public void Reset()
        {
            _source = Mat3.Identity;
            _inverse = Mat3.Identity;
            _isDirty = false;
        }

        void OnSourceMatrixChanged()
        {
            _isDirty = true;
        }

        public Trans2 Translate(double x, double y)
        {
            _source = Mat3.Multiply(_source, Mat3.Translation(x, y));
            OnSourceMatrixChanged();
            return this;
        }

        public Trans2 Scale(double scaleX, double scaleY)
        {
            _source = Mat3.Multiply(_source, Mat3.Scale(scaleX, scaleY));
            OnSourceMatrixChanged();
            return this;
        }
        public Trans2 ScaleAt(double x, double y, double scaleX, double scaleY)
        {
            _source = Mat3.Multiply(_source, Mat3.Translation(x, y));
            _source = Mat3.Multiply(_source, Mat3.Scale(scaleX, scaleY));
            _source = Mat3.Multiply(_source, Mat3.Translation(-x, -y));
            OnSourceMatrixChanged();
            return this;
        }

        public Trans2 Rotate(double rad)
        {
            _source = Mat3.Multiply(_source, Mat3.Rotation(rad));
            OnSourceMatrixChanged();
            return this;
        }
        public Trans2 RotateAt(double x, double y, double rad)
        {
            _source = Mat3.Multiply(_source, Mat3.Translation(x, y));
            _source = Mat3.Multiply(_source, Mat3.Rotation(rad));
            _source = Mat3.Multiply(_source, Mat3.Translation(-x, -y));
            OnSourceMatrixChanged();
            return this;
        }

        public Vec2 Forward(Vec2 vec)
        {
            if (false == _isDirty) return vec;
            return _source * vec;
        }

        public Vec2 Backward(Vec2 vec)
        {
            if (false == _isDirty) return vec;

            if (false == s_inverseIsValid)
            {
                if (false == _source.Inverse(out _inverse))
                {
                    throw new DivideByZeroException("Can't invert matrix. Determinant is zero.");
                }
                s_inverseIsValid = true;
            }
            return _inverse * vec;
        }
    }
}
