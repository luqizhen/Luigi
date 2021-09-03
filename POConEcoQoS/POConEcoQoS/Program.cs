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
            Console.WriteLine("Hello!");

            var process = Process.GetProcessById(15336);
            Console.WriteLine($"Handle: {process.Handle}");
            Console.WriteLine($"Name: {process.ProcessName}");
            Console.WriteLine($"Priority: {process.PriorityClass}");

            {
                var sizeOfProcessInfo = Marshal.SizeOf(typeof(WinAPI.PROCESS_POWER_THROTTLING_STATE));
                var pProcessInfo = Marshal.AllocHGlobal(sizeOfProcessInfo);
                var rs = WinAPI.GetProcessInformation(process.Handle, WinAPI.PROCESS_INFORMATION_CLASS.ProcessPowerThrottling, pProcessInfo, sizeOfProcessInfo);
                var processInfo = Marshal.PtrToStructure<WinAPI.PROCESS_POWER_THROTTLING_STATE>(pProcessInfo);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"GetProcessInformation Result: {rs}");
                sb.AppendLine($"Version: {processInfo.Version}");
                sb.AppendLine($"ControlMask: {processInfo.ControlMask}");
                sb.AppendLine($"StateMask: {processInfo.StateMask}");
                Console.WriteLine(sb.ToString());
                Marshal.FreeHGlobal(pProcessInfo);
            }

            {
                var sizeOfProcessInfo = Marshal.SizeOf(typeof(WinAPI.MEMORY_PRIORITY_INFORMATION));
                var pProcessInfo = Marshal.AllocHGlobal(sizeOfProcessInfo);
                var rs = WinAPI.GetProcessInformation(process.Handle, WinAPI.PROCESS_INFORMATION_CLASS.ProcessMemoryPriority, pProcessInfo, sizeOfProcessInfo);
                var processInfo = Marshal.PtrToStructure<WinAPI.MEMORY_PRIORITY_INFORMATION>(pProcessInfo);
                var sb = new StringBuilder();
                sb.AppendLine($"GetProcessInformation Result: {rs}");
                sb.AppendLine($"MemoryPriority: {processInfo.MemoryPriority}");
                Console.WriteLine(sb.ToString());
                Marshal.FreeHGlobal(pProcessInfo);
            }

            Console.WriteLine("Goodbye!");
        }
    }
}
