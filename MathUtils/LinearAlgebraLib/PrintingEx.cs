using System.Diagnostics;
using System.Text;

namespace LinearAlgebraLib
{
    public static class PrintingEx
    {
        public const int DEFAULT_PRECISION = 4;

        public static string ToRoundedString(this ILinearAlgebraObject obj, int precision = DEFAULT_PRECISION)
        {
            string format = $"0.{new string('0', precision)}";
            int maxColumnWidth = int.MinValue;

            int rows = obj.Rows;
            int cols = obj.Columns;

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    int columnWidth = obj.Get(row, col).ToString(format).Length;
                    if (maxColumnWidth < columnWidth)
                    {
                        maxColumnWidth = columnWidth;
                    }
                }
            }

            StringBuilder sb = new StringBuilder();
            for (int row = 0; row < rows; row++)
            {
                sb.Append("| ");
                for (int col = 0; col < cols; col++)
                {
                    string v = obj.Get(row, col).ToString(format).PadLeft(maxColumnWidth);
                    sb.Append(v);
                }
                sb.Append(" |\n");
            }

            string result = sb.ToString();
            return result;
        }

        public static void ToConsole(this ILinearAlgebraObject obj, int precision = DEFAULT_PRECISION)
        {
            Console.WriteLine(obj.ToRoundedString(precision));
        }
        public static void ToDebug(this ILinearAlgebraObject obj, int precision = DEFAULT_PRECISION)
        {
            Debug.WriteLine(obj.ToRoundedString(precision));
        }
    }
}
