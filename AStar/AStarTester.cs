using luigi.algorithms;
using System;

namespace luigi.algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            int seed = 1027;
            Puzzle initPuzzle = Puzzle.Random(seed, 3, complexity:300);
            Console.WriteLine(initPuzzle.ToString());
            AStar<Puzzle> engine = new AStar<Puzzle>(initPuzzle, Puzzle.Default(3));
            engine.SetNextStepsRule(Puzzle.NextStepsRule);
            engine.SetCostFunction(null, dstCostFunc);
            engine.Run();
            engine.Steps.ForEach(e => Console.WriteLine(e.ToString()));
            Console.WriteLine("Steps: " + engine.NSteps);
        }

        private static double srcCostFunc(Puzzle src, Puzzle now)
        {
            throw new NotImplementedException();
        }

        private static double dstCostFunc(Puzzle now, Puzzle dst)
        {
            int width = now.Width;
            int height = now.Height;
            int diff = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if(now[x,y] != dst[x, y])
                    {
                        diff++;
                    }
                }
            }
            return diff;
        }
    }
}
