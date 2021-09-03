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

            //ProcessInformationWrapper.GetProcessInfo(process.Handle, WinAPI.PROCESS_INFORMATION_CLASS.ProcessPowerThrottling, out processInfo);
            //Console.WriteLine(processInfo);

            //ProcessInformationWrapper.GetProcessInfo(process.Handle, WinAPI.PROCESS_INFORMATION_CLASS.ProcessMemoryPriority, out processInfo);
            //Console.WriteLine(processInfo);

            //WinAPI.MEMORY_PRIORITY_INFORMATION pi = new WinAPI.MEMORY_PRIORITY_INFORMATION { MemoryPriority = 5 };
            //ProcessInformationWrapper.SetProcessInfo(process.Handle, WinAPI.PROCESS_INFORMATION_CLASS.ProcessMemoryPriority, pi);

            //ProcessInformationWrapper.GetProcessInfo(process.Handle, WinAPI.PROCESS_INFORMATION_CLASS.ProcessMemoryPriority, out processInfo);
            //Console.WriteLine(processInfo);
        }
    }
}
