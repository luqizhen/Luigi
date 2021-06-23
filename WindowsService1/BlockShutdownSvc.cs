using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Vanara.PInvoke;

namespace WindowsService1
{
    public partial class BlockShutdownSvc : ServiceBase
    {
        private int count = 0;
        private PreventShutdownContext _context;

        const int SERVICE_ACCEPT_PRESHUTDOWN = 0x100;
        const int SERVICE_CONTROL_PRESHUTDOWN = 0xf;

        public BlockShutdownSvc()
        {
            InitializeComponent();

            //this.CanShutdown = true;

            eventLog1 = new EventLog();
            if (!EventLog.SourceExists("MySource"))
            {
                EventLog.CreateEventSource("MySource", "MyLog");
            }
            eventLog1.Source = "MySource";
            eventLog1.Log = "MyLog";


            FieldInfo acceptedCommandsFieldInfo = typeof(ServiceBase).GetField("acceptedCommands", BindingFlags.Instance | BindingFlags.NonPublic);
            if (acceptedCommandsFieldInfo == null)
                throw new Exception("acceptedCommands field not found");

            int value = (int)acceptedCommandsFieldInfo.GetValue(this);
            acceptedCommandsFieldInfo.SetValue(this, value | SERVICE_ACCEPT_PRESHUTDOWN);

            // Set up a timer that triggers every minute.
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 60000; // 60 seconds
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer.Start();
        }

        protected override void OnCustomCommand(int command)
        {
            eventLog1.WriteEntry("OnCustomCommand: " + command);
            if (command == SERVICE_CONTROL_PRESHUTDOWN)
            {
                // do the clean-up here
                eventLog1.WriteEntry("SERVICE_CONTROL_PRESHUTDOWN");
                Thread.Sleep(30 * 1000);
            }
            else
            {
                base.OnCustomCommand(command);
            }
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            eventLog1.WriteEntry("Triggered: " + count++);
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("Enter OnStart");
            
            //_context = new PreventShutdownContext(this, this.ServiceHandle, "Warning! Don't shutdown forcely.");
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("Enter OnStop");
            //_context?.Dispose();
        }

        protected override void OnContinue()
        {
            eventLog1.WriteEntry("Enter OnContinue");
        }

    }
}
