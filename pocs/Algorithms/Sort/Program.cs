using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using utilities;

namespace Algorithms.Sort
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<ISort> Sorts = new List<ISort>();
            Sorts.Add(new FastSort());
            Sorts.Add(new RandomizedFastSort());
            Sorts.Add(new CountingSort());

            string cmd = "y";
            while (cmd == "Y" || cmd == "y")
            {
                int N = 1000000;
                Utils.Print("The length of array: " + N + ".");
                Utils.Print("");

                DateTime start = DateTime.Now;

                Sorts.ForEach((sort) =>
                {
                    (new Thread(() =>
                    {
                        int[] array = new int[N];
                        Common.ResetCounters();
                        array = Common.CreateRandomArray(N, N);
                        sort.Sort(array);
                        Common.ShowCounters();
                    })).Start();
                });
                cmd = Console.ReadLine();
            }
        }
    }
}
