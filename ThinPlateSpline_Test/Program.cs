using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinPlateSpline_DotNet;

namespace ThinPlateSpline_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTest();
        }

        private static void RunTest()
        {
            int row_count = 30;
            int col_count = 30;
            UInt32[] control_rows = new UInt32[] { 1, 2, 3, 2, 10, 13, 29 };
            UInt32[] control_cols = new UInt32[] { 9, 7, 2, 13, 20, 2, 6 };
            double[] control_vals = new double[] { 10.3, 10.5, 9.9, 11.1, 8.9, 10.9, 8.4 };

            double[][] result = ThinPlateSpline.Calc_TPS(control_rows, control_cols, control_vals, (UInt32)col_count, (UInt32)row_count);

            for (int r = 0; r < row_count; r++)
            {
                for (int c = 0; c < col_count; c++)
                {
                    Console.WriteLine("R" + r + "C" + c + ": " + result[r][c]);
                }
            }
        }
    }
}
