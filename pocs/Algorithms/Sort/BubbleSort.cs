using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sort
{
    public class BubbleSort : ISort
    {
        public void Sort(int[] array)
        {
            for (int j = 1; j < array.Length - 1; j++)
            {
                for (int i = 0; i < array.Length - j; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        Common.Swap(ref array[i], ref array[i + 1]);
                    }
                }
            }
        }
    }
}
