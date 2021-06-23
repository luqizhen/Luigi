using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;

namespace UWPApp
{
    public class MyBackgroundTaskManager
    {
        private const string Name = "MyWRC.MyBackgroundTask";
        private const string EntryPoint = "MyWRC.MyBackgroundTask";
        private ToastNotification _toast;

        internal void Register()
        {
            var taskRegistered = false;
            var exampleTaskName = "ExampleBackgroundTask";

            foreach (var task in BackgroundTaskRegistration.AllTasks)
            {
                if (task.Value.Name == exampleTaskName)
                {
                    taskRegistered = true;
                    break;
                }
            }

            if (!taskRegistered)
            {
                var builder = new BackgroundTaskBuilder();
                builder.Name = Name;
                builder.TaskEntryPoint = EntryPoint;
                builder.SetTrigger(new TimeTrigger(15, true));
                builder.AddCondition(new SystemCondition(SystemConditionType.UserPresent));
                BackgroundTaskRegistration reg = builder.Register();
                reg.Progress += Reg_Progress;
                reg.Completed += Reg_Completed;

                var toastContent = new ToastContentBuilder()
                    .AddText("Background task running...")
                    .AddVisualChild(new AdaptiveProgressBar()
                    {
                        Title = "Task progress",
                        Value = new BindableProgressBarValue("progressValue"),
                        ValueStringOverride = new BindableString("progressValueString"),
                        Status = new BindableString("progressStatus")
                    })
                    .GetToastContent();

                _toast = new ToastNotification(toastContent.GetXml());
                _toast.Tag = "bg-test";
                _toast.Group = "test";
            }
        }

        private void UpdateNotification(uint progressValue, string statusString)
        {
            if(_toast != null)
            {
                if(_toast.Data == null)
                {
                    _toast.Data = new NotificationData();
                    _toast.Data.Values["progressValue"] = (progressValue / 100.0).ToString();
                    _toast.Data.Values["progressValueString"] = $"{progressValue}/100";
                    _toast.Data.Values["progressStatus"] = statusString;
                    _toast.Data.SequenceNumber = progressValue;
                    ToastNotificationManager.CreateToastNotifier().Show(_toast);
                }
                else
                {
                    var data = new NotificationData
                    {
                        SequenceNumber = progressValue
                    };
                    data.Values["progressValue"] = (progressValue / 100.0).ToString();
                    data.Values["progressValueString"] = $"{progressValue}/100";
                    data.Values["progressStatus"] = statusString;
                    ToastNotificationManager.CreateToastNotifier().Update(data, _toast.Tag, _toast.Group);
                }
            }
        }

        private void Reg_Progress(BackgroundTaskRegistration sender, BackgroundTaskProgressEventArgs args)
        {
            var status = string.Empty;
            if(args.Progress < 10)
            {
                status = "Starting...";
            }
            else if(args.Progress < 30)
            {
                status = "Downloading...";
            }
            else if(args.Progress < 90)
            {
                status = "Applying...";
            }
            else
            {
                status = "Almost done...";
            }
            UpdateNotification(args.Progress, status);
        }

        private void Reg_Completed(BackgroundTaskRegistration sender, BackgroundTaskCompletedEventArgs args)
        {
            var toast = new ToastNotification(
                new ToastContentBuilder()
                .AddText("Task Completed!")
                .AddButton(new ToastButton().SetContent("Got it!"))
                .GetToastContent()
                .GetXml());
            toast.Tag = "bg-test";
            toast.Group = "test";
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
