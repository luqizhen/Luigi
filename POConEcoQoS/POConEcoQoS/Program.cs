using System;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;

namespace POConEcoQoS
{
    class Program
    {
        static void Main(string[] args)
        {
            var process = Process.GetCurrentProcess();
            Console.WriteLine(
                $"Handle: {process.Handle}\n" +
                $"Name: {process.ProcessName}\n" +
                $"Priority: {process.PriorityClass}\n"
                );

            object processInfo;

            var rs = ProcessInformationWrapper.GetProcessInfo(process.Handle, WinAPI.PROCESS_INFORMATION_CLASS.ProcessPowerThrottling, out processInfo);
            Console.WriteLine("GetProcessInfo(ProcessPowerThrottling): " + rs);
            Console.WriteLine(processInfo);

            WinAPI.PROCESS_POWER_THROTTLING_STATE pi = new WinAPI.PROCESS_POWER_THROTTLING_STATE { ControlMask = 1, StateMask = 1 };
            rs = ProcessInformationWrapper.SetProcessInfo(process.Handle, WinAPI.PROCESS_INFORMATION_CLASS.ProcessPowerThrottling, pi);
            Console.WriteLine("SetProcessInfo(ProcessPowerThrottling): " + rs);

            //ProcessInformationWrapper.GetProcessInfo(process.Handle, WinAPI.PROCESS_INFORMATION_CLASS.ProcessMemoryPriority, out processInfo);
            //Console.WriteLine(processInfo);
        }
    }
}
