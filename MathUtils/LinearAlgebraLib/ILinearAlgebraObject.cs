namespace LinearAlgebraLib
{
    public interface ILinearAlgebraObject
    {
        /// <summary>
        /// Returns the value of item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public double Get(int row, int col);

        int Rows { get; }
        int Columns { get; }
    }
}
