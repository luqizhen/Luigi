using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.DataStucture
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                List.List list = new List.List();
                list.Add(10);
                list.Add(2.33);
                list.Add("Hello World");
                list.Add(-4);
                Console.WriteLine(list.ToString());
                list.Insert("Insert", 2);
                list.Insert("start", 0);
                Console.WriteLine(list.Length);
                Console.WriteLine(list.ToString());
                Console.WriteLine(list.PopHead());
                Console.WriteLine(list.ToString());
                Console.WriteLine(list.PopTail());
                Console.WriteLine(list.ToString());
                Console.WriteLine(list.Contains("Hello World"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


            Console.ReadLine();
        }
    }
}
