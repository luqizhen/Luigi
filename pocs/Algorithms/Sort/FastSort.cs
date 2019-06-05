using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using utilities;

namespace Algorithms.Sort
{
    class RandomizedFastSort : ISort
    {
        private int _length;
        private Random _random;

        public void Sort(int[] array)
        {
            _length = array.Length;
            _random = new Random();

            Sort(array, 0, _length - 1);
        }

        private void Sort(int[] array, int left, int right)
        {
            Common.RecursiveCounter++;
            if (right > left)
            {
                Exchange(array, _random.Next(left, right), right);
                int mid = array[right];
                
                // exchange
                int iter = left;
                int flag = left;
                while (iter < right)
                {
                    if (array[iter] < mid)
                    {
                        Exchange(array, iter, flag);
                        flag++;
                    }
                    iter++;
                }
                Exchange(array, right, flag);

                // divide and loop
                Sort(array, left, flag - 1);
                Sort(array, flag + 1, right);
            }
        }

        private void Exchange(int[] array, int iter, int flag)
        {
            Common.SwapCounter++;
            int temp = array[flag];
            array[flag] = array[iter];
            array[iter] = temp;
        }
    }

    class FastSort : ISort
    {
        private int _length;

        public void Sort(int[] array)
        {
            _length = array.Length;
            
            Sort(array, 0, _length - 1);
        }

        private void Sort(int[] array, int left, int right)
        {
            Common.RecursiveCounter++;
            if (right > left)
            {
                int mid = array[right];
                // exchange
                int iter = left;
                int flag = left;
                while(iter < right)
                {
                    if(array[iter] < mid)
                    {
                        Exchange(array, iter, flag);
                        flag++;
                    }
                    iter++;
                }
                Exchange(array, right, flag);
                
                // divide and loop
                Sort(array, left, flag - 1);
                Sort(array, flag + 1, right);
            }
        }

        private void Exchange(int[] array, int iter, int flag)
        {
            Common.SwapCounter++;
            int temp = array[flag];
            array[flag] = array[iter];
            array[iter] = temp;
        }
    }
}
