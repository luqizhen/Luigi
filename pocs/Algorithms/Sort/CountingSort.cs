using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sort
{
    class CountingSort : ISort
    {
        public void Sort(int[] array)
        {
            var length = array.Length;
            var max = array.Max();
            var tempArray = new int[max + 1];
            var outArray = new int[length];

            for (int i = 0; i < length; i++)
            {
                tempArray[array[i]] += 1;
            }

            for (int i = 0; i < max; i++)
            {
                tempArray[i + 1] += tempArray[i];
            }

            for (int i = length - 1; i >= 0; i--)
            {
                outArray[tempArray[array[i]] - 1] = array[i];
                tempArray[array[i]] -= 1;
            }

            array = outArray;
        }
    }
}
