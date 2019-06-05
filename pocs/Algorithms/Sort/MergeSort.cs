using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sort
{
    class MergeSort : ISort
    {
        public void Sort(int[] array)
        {
            Sort(array, 0, array.Length - 1);
        }

        private void Sort(int[] array, int startIndex, int EndIndex)
        {
            if (EndIndex - startIndex > 0)
            {
                int midIndex = startIndex + (EndIndex - startIndex + 1) / 2;
                Sort(array, startIndex, midIndex - 1);
                Sort(array, midIndex, EndIndex);

                //merge two parts
                Common.RecursiveCounter++;
                int i = startIndex, j = midIndex, k = 0;
                int[] tempArray = new int[EndIndex - startIndex + 1];
                while (i < midIndex && j <= EndIndex)
                {
                    if (array[i] <= array[j])
                    {
                        tempArray[k++] = array[i];
                        i++;
                    }
                    else
                    {
                        tempArray[k++] = array[j];
                        j++;
                    }
                }

                if (i == midIndex)
                {
                    k = 0;
                    for(i = startIndex; i < j; i++)
                    {
                        array[i] = tempArray[k++];
                    }
                }
                else
                {
                    for (; i < midIndex; i++)
                    {
                        tempArray[k++] = array[i];
                    }
                    k = 0;
                    for (i = startIndex; i <= EndIndex; i++)
                    {
                        array[i] = tempArray[k++];
                    }
                }
            }

        }
    }
}
