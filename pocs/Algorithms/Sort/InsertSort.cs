using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sort
{
    class InsertSort : ISort
    {
        public void Sort(int[] array)
        {
            for(int i = 1; i < array.Length; i++)
            {
                for(int j = 0; j < i; j++)
                {
                    if (array[i] < array[j])
                    {
                        int temp = array[i];
                        for(int k = i; k > j; k--)
                        {
                            array[k] = array[k - 1];
                        }
                        array[j] = temp;
                    }
                }
            }
        }
    }
}
