using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sort
{
    class HeapSort : ISort
    {
        int Parent(int i)
        {
            return (i + 1) / 2 - 1;
        }

        int Left(int i)
        {
            return 2 * (i + 1) - 1;
        }

        int Right(int i)
        {
            return 2 * (i + 1);
        }

        public void Sort(int[] array)
        {
            BuildMaxHeap(array, array.Length);
            for(int i = array.Length - 1; i > 0; i--)
            {
                Common.Swap(ref array[0], ref array[i]);
                MaxHeapify(array, i, 0);
            }
        }

        void MaxHeapify(int[] array, int heapSize, int i)
        {
            Common.RecursiveCounter++;
            int iLeft = Left(i);
            int iRight = Right(i);

            int left = (heapSize > iLeft) ? array[iLeft] : int.MinValue;
            int right = (heapSize > iRight) ? array[iRight] : int.MinValue;

            if (array[i] < ((left > right) ? left : right))
            {
                Common.Swap(ref array[i], ref array[iLeft]);
                MaxHeapify(array, heapSize, iLeft);
            }
        }
        
        void BuildMaxHeap(int[] array, int heapSize)
        {
            for (int i = array.Length / 2 - 1; i >= 0; i--)
            {
                MaxHeapify(array, heapSize, i);
            }
        }
    }
}
