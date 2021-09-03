using System.Runtime.InteropServices;

using BOOL = System.Int32;
using HANDLE = System.IntPtr;
using LPVOID = System.IntPtr;
using DWORD = System.Int32;
using ULONG = System.UInt32;

namespace POConEcoQoS
{
    public static class WinAPI
    {
        [DllImport("kernel32.dll")]
        public extern static BOOL GetProcessInformation(
            HANDLE hProcess,
            PROCESS_INFORMATION_CLASS ProcessInformationClass,
            LPVOID ProcessInformation,
            DWORD ProcessInformationSize);

        [DllImport("kernel32.dll")]
        public extern static BOOL SetProcessInformation(
            HANDLE hProcess,
            PROCESS_INFORMATION_CLASS ProcessInformationClass,
            LPVOID ProcessInformation,
            DWORD ProcessInformationSize);

        public enum PROCESS_INFORMATION_CLASS
        {
            ProcessMemoryPriority,
            ProcessMemoryExhaustionInfo,
            ProcessAppMemoryInfo,
            ProcessInPrivateInfo,
            ProcessPowerThrottling,
            ProcessReservedValue1,
            ProcessTelemetryCoverageInfo,
            ProcessProtectionLevelInfo,
            ProcessLeapSecondInfo,
            ProcessMachineTypeInfo,
            ProcessInformationClassMax
        };

        public struct PROCESS_POWER_THROTTLING_STATE
        {
            public ULONG Version;
            public ULONG ControlMask;
            public ULONG StateMask;
        }

        public struct MEMORY_PRIORITY_INFORMATION
        {
            public ULONG MemoryPriority;
        }
    }
}
