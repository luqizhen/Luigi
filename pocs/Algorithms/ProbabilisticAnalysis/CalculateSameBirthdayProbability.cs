using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.ProbabilisticAnalysis
{
    class CalculateSameBirthdayProbability
    {
        public static decimal Calculate(int n)
        {
            if (n == 1 || n == 0)
            {
                return 0;
            }
            else
            {
                decimal probabilityDiffDay = 1;
                for(int i = 0; i < n; i++)
                {
                    probabilityDiffDay *= (decimal)(365 - i) / (decimal)365;
                }
                return 1 - probabilityDiffDay;
            }
        }
    }
}
