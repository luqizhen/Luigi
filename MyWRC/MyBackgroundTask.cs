using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace MyWRC
{
    public sealed class MyBackgroundTask : IBackgroundTask
    {
        private BackgroundTaskDeferral _deferral;

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();

            for (int i = 0; i < 100; i++)
            {
                taskInstance.Progress = (uint)i;
                await Task.Delay(1000);
            }

            _deferral.Complete();
        }
    }
}
