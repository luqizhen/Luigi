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
            var process = Process.GetProcessById(29260);
            Console.WriteLine(
                $"Handle: {process.Handle}\n" +
                $"Name: {process.ProcessName}\n" +
                $"Priority: {process.PriorityClass}\n"
                );

            object processInfo = null;

            var rs_get = ProcessInformationWrapper.GetProcessInfo(process.Handle, WinAPI.PROCESS_INFORMATION_CLASS.ProcessPowerThrottling, out processInfo);
            Console.WriteLine("GetProcessInfo(ProcessPowerThrottling): " + rs_get);
            Console.WriteLine(processInfo);

            WinAPI.PROCESS_POWER_THROTTLING_STATE pi = new WinAPI.PROCESS_POWER_THROTTLING_STATE
            {
                Version = WinAPI.PROCESS_POWER_THROTTLING_CURRENT_VERSION,
                ControlMask = WinAPI.PROCESS_POWER_THROTTLING_EXECUTION_SPEED,
                StateMask = WinAPI.PROCESS_POWER_THROTTLING_EXECUTION_SPEED
            };

            var rs_set = ProcessInformationWrapper.SetProcessInfo(process.Handle, WinAPI.PROCESS_INFORMATION_CLASS.ProcessPowerThrottling, pi);
            Console.WriteLine("SetProcessInfo(ProcessPowerThrottling): " + rs_set);
            Console.WriteLine(pi);

            //ProcessInformationWrapper.GetProcessInfo(process.Handle, WinAPI.PROCESS_INFORMATION_CLASS.ProcessMemoryPriority, out processInfo);
            //Console.WriteLine(processInfo);
        }
    }
}
