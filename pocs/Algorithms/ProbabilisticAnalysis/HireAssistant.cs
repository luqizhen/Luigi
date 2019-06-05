using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.ProbabilisticAnalysis
{
    class HireAssistant
    {
        Random random = new Random();
        const int _interviewCost = 1;
        const int _hireCost = 10;

        double CreateNewAssist()
        {
            return random.NextDouble();
        }

        public int PredictCost(int n)
        {
            int cost = 0;
            double assist = CreateNewAssist();
            double newAssist;
            for(int i = 0; i < n; i++)
            {
                newAssist = CreateNewAssist();
                cost += _interviewCost;
                if (newAssist > assist)
                {
                    assist = newAssist;
                    cost += _hireCost;
                }
            }
            return cost;
        }
    }
}
