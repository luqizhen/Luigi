using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sort
{
    public class Common
    {
        public static ulong SwapCounter = 0;
        public static object lockShowCounters = new object();
        public static ulong RecursiveCounter = 0;
        private static DateTime _recordDateTime;

        public static TimeSpan TimeCounter
        {
            get
            {
                return DateTime.Now - _recordDateTime;
            }
        }

        public static void Swap(ref int v1, ref int v2)
        {
            SwapCounter++;
            int temp = v1;
            v1 = v2;
            v2 = temp;
        }

        internal static void ResetCounters()
        {
            SwapCounter = 0;
            _recordDateTime = DateTime.Now;
            RecursiveCounter = 0;
        }

        public static void ShowCounters()
        {
            lock (lockShowCounters)
            {
                Console.Write('\n');
                Console.WriteLine($"The Algorithm cost \t\t<{Common.TimeCounter.TotalSeconds}>");
                Console.WriteLine($"The swap happens \t\t<{Common.SwapCounter}>");
                Console.WriteLine($"The Recursive happens \t\t<{Common.RecursiveCounter}>");
                Console.Write('\n');
            }
        }

        public static void PrintArray(int[] array)
        {
            Console.Write('\n');
            foreach (var element in array)
            {
                Console.Write(element + "\t");
            }
            Console.Write('\n');
        }

        /// <summary>
        /// create a random array with length n
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int[] CreateRandomArray(int n, int max)
        {
            Random random = new Random();
            int[] array = new int[n];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(0, max);
            }
            return array;
        }
    }
}
