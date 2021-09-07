using System;
using System.Threading;
using System.Threading.Tasks;

namespace EcoQoS.Test.WPF
{
    internal class TaskRunner
    {
        private DateTime _startTimeTaskA, _startTimeTaskB;
        private int _cyclesTaskA, _cyclesTaskB;
        private string _msgTaskA, _msgTaskB;
        private CancellationTokenSource _tokenSourceA, _tokenSourceB;

        private readonly Random _rand = new Random(((int)DateTime.Now.Ticks));

        internal void StartTaskA(Action<int, int> progressCallback = null)
        {
            _startTimeTaskA = DateTime.Now;
            _tokenSourceA = new CancellationTokenSource();
            Task.Run(() =>
            {
                RunTaskA(progressCallback);
            }, _tokenSourceA.Token);
        }

        internal void StartTaskB(Action<int, int> progressCallback = null)
        {
            _startTimeTaskB = DateTime.Now;
            _tokenSourceB = new CancellationTokenSource();
            Task.Run(() =>
            {
                RunTaskB(progressCallback);
            }, _tokenSourceB.Token);
        }

        internal Record StopTaskA()
        {
            _tokenSourceA.Cancel();
            _msgTaskA = string.Empty;
            return new Record() { TaskName = "TASK A", StartTime = _startTimeTaskA, Duration = DateTime.Now- _startTimeTaskA, Cycles = _cyclesTaskA, Message = _msgTaskA };
        }

        internal Record StopTaskB()
        {
            _tokenSourceB.Cancel();
            _msgTaskB = string.Empty;
            return new Record() { TaskName = "TASK B", StartTime = _startTimeTaskB, Duration = DateTime.Now- _startTimeTaskB, Cycles = _cyclesTaskB, Message = _msgTaskB };
        }

        private void RunTaskA(Action<int, int> progressCallback)
        {
            _cyclesTaskA = 0;
            while (true)
            {
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(10);
                    progressCallback?.Invoke(_cyclesTaskA, i);
                    if (_tokenSourceA.IsCancellationRequested)
                    {
                        break;
                    }
                }
                if (_tokenSourceA.IsCancellationRequested)
                {
                    break;
                }
                _cyclesTaskA++;
            }
        }

        private void RunTaskB(Action<int, int> progressCallback)
        {
            _cyclesTaskB = 0;
            while (true)
            {
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(50);
                    progressCallback?.Invoke(_cyclesTaskB, i);
                    if (_tokenSourceB.IsCancellationRequested)
                    {
                        break;
                    }
                }
                if (_tokenSourceB.IsCancellationRequested)
                {
                    break;
                }
                _cyclesTaskB++;
            }
        }
    }
}