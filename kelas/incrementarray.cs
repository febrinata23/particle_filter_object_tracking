using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace vidplay.kelas
{
    class incrementarray
    {
        public static void updatematrix(ref int[] value, int inc)
        {
            int[] array = new int[value.Length + inc];
            value.CopyTo(array, 0);
            value = array;
        }

        public static void updatematrix(ref long[] value, long inc)
        {
            long[] array = new long[value.Length + inc];
            value.CopyTo(array, 0);
            value = array;
        }

        internal static void updatematrix(long[] value, long inc,int x)
        {
            long[] array = new long[value.Length + x];
            value.CopyTo(array, 0);
            array[value.Length + x] = inc;
            value = array;
        }

        internal static void updatematrix(int[] value, int inc, int x)
        {
            int[] array = new int[value.Length + x];
            value.CopyTo(array, 0);
            array[value.Length + x] = inc;
            value = array;
        }
    }
}
