using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO;

namespace ThinPlateSpline_DotNet
{
    public class ThinPlateSpline
    {
        [DllImport("ThinPlateSpline_Cpp.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr interpolate_grid_using_tps(UInt32 input_arr_size, UInt32[] control_row_values, UInt32[] control_col_values, double[] control_values, UInt32 output_num_rows, UInt32 output_num_cols);

        [DllImport("ThinPlateSpline_Cpp.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr delete_2d_array(UInt32 arr_size, IntPtr arr);

        public static double[][] Calc_TPS(UInt32[] control_row_values, UInt32[] control_col_values, double[] control_values, UInt32 output_num_rows, UInt32 output_num_cols)
        {
            if (control_row_values == null) return null;
            if (control_col_values == null) return null;
            if (control_values == null) return null;
            if (control_row_values.Length != control_col_values.Length) return null;
            if (control_row_values.Length != control_values.Length) return null;

            var result = interpolate_grid_using_tps((UInt32)control_row_values.Length, control_row_values, control_col_values, control_values, output_num_rows, output_num_cols);
            var _ptr = result;
            var data = new double[output_num_rows][];
            for (int i = 0; i < output_num_rows; i++)
            {
                data[i] = new double[(int)output_num_cols];
                Marshal.Copy(Marshal.ReadIntPtr(_ptr), data[i], 0, (int)output_num_cols);
                _ptr = (IntPtr)(_ptr.ToInt64() + IntPtr.Size);
            }
            delete_2d_array(output_num_rows, result);
            return data;
        }
    }
}
