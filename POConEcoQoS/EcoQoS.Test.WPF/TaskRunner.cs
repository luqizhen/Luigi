using System;
using System.Threading;
using System.Threading.Tasks;
using EcoQoS.Test.WPF.workloads;

namespace EcoQoS.Test.WPF
{
    internal class TaskRunner
    {
        private CancellationTokenSource _tokenSourceA, _tokenSourceB;
        private IWorkLoad _workloadA = new LightWorkLoad();
        private IWorkLoad _workloadB = new HeavyWorkLoad();

        internal async Task<Record> RunTaskAAsync(Action<int, int> progressCallback = null)
        {
            var startTime = DateTime.Now;
            _tokenSourceA = new CancellationTokenSource();

            var cycles = await _workloadA.RunAsync(_tokenSourceA, progressCallback);

            return new Record() { TaskName = "TASK A", StartTime = startTime, Duration = DateTime.Now - startTime, Cycles = cycles, Message = string.Empty };
        }

        internal async Task<Record> RunTaskBAsync(Action<int, int> progressCallback = null)
        {
            var startTime = DateTime.Now;
            _tokenSourceB = new CancellationTokenSource();

            var cycles = await _workloadB.RunAsync(_tokenSourceB, progressCallback);

            return new Record() { TaskName = "TASK B", StartTime = startTime, Duration = DateTime.Now - startTime, Cycles = cycles, Message = string.Empty };
        }

        internal void StopTaskA()
        {
            _tokenSourceA.Cancel();
        }

        internal void StopTaskB()
        {
            _tokenSourceB.Cancel();
        }
    }
}