using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EcoQoS.Test.WPF.workloads
{
    public interface IWorkLoad
    {
        Task<int> RunAsync(CancellationTokenSource tokenSource, Action<int, int> progressCallback);

        int Run(CancellationTokenSource tokenSource, Action<int, int> progressCallback);
    }
}
