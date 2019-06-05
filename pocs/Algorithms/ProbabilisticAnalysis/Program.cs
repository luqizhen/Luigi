using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.ProbabilisticAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            var hireAssistant = new HireAssistant();
            for(int i = 2; i < 50; i++)
            {
                
                Console.WriteLine(i+"\t"+CalculateSameBirthdayProbability.Calculate(i));
            }
            Console.ReadKey();

        }
    }
}
