using System;
using System.Threading;
using System.Threading.Tasks;

namespace EcoQoS.Test.WPF.workloads
{
    public class LightWorkLoad : IWorkLoad
    {
        public int Run(CancellationTokenSource tokenSource, Action<int, int> progressCallback)
        {
            int cycles = 0;
            while (true)
            {
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(10);
                    progressCallback?.Invoke(cycles, i);
                    if (tokenSource.IsCancellationRequested)
                    {
                        break;
                    }
                }
                if (tokenSource.IsCancellationRequested)
                {
                    break;
                }
                cycles++;
            }
            return cycles;
        }

        public async Task<int> RunAsync(CancellationTokenSource tokenSource, Action<int, int> progressCallback)
        {
            return await Task.Run(() => Run(tokenSource, progressCallback), tokenSource.Token);
        }
    }
}
