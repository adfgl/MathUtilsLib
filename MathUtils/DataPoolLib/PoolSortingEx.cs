using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataPoolLib
{
    public static class PoolSortingEx
    {
        public static void SortByColumn(PoolBase<double> matrix, int columnIndex, ESort sort)
        {
            if (sort == ESort.Undefined) throw new InvalidOperationException();

            int rows = matrix.Count;
            int cols = matrix.NumComponents;

            if (columnIndex >= cols) throw new InvalidOperationException();

            matrix.s_sortState = sort;
            matrix.s_sortColumn = columnIndex;

            for (int i = 0; i < rows - 1; i++)
            {
                int targetIndex = i;
                for (int j = i + 1; j < rows; j++)
                {
                    bool condition = sort == ESort.Ascending ?
                                   matrix[j, columnIndex] < matrix[targetIndex, columnIndex] :
                                   matrix[j, columnIndex] > matrix[targetIndex, columnIndex];

                    if (condition)
                    {
                        targetIndex = j;
                    }
                }
                if (targetIndex != i)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        var temp = matrix[i, j];
                        matrix[i, j] = matrix[targetIndex, j];
                        matrix[targetIndex, j] = temp;
                    }
                }
            }
        }

        public static void SortByColumn(PoolBase<int> matrix, int columnIndex, ESort sort)
        {
            if (sort == ESort.Undefined) throw new InvalidOperationException();

            int rows = matrix.Count;
            int cols = matrix.NumComponents;

            if (columnIndex >= cols) throw new InvalidOperationException();

            matrix.s_sortState = sort;
            matrix.s_sortColumn = columnIndex;

            for (int i = 0; i < rows - 1; i++)
            {
                int targetIndex = i;
                for (int j = i + 1; j < rows; j++)
                {
                    bool condition = sort == ESort.Ascending ?
                                   matrix[j, columnIndex] < matrix[targetIndex, columnIndex] :
                                   matrix[j, columnIndex] > matrix[targetIndex, columnIndex];

                    if (condition)
                    {
                        targetIndex = j;
                    }
                }
                if (targetIndex != i)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        var temp = matrix[i, j];
                        matrix[i, j] = matrix[targetIndex, j];
                        matrix[targetIndex, j] = temp;
                    }
                }
            }
        }
    }
}
