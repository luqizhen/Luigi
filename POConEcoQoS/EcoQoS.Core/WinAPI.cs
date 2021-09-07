using System.Runtime.InteropServices;

using BOOL = System.Int32;
using HANDLE = System.IntPtr;
using LPVOID = System.IntPtr;
using DWORD = System.Int32;
using ULONG = System.UInt32;

namespace EcoQoS.Core
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

        public const int PROCESS_POWER_THROTTLING_CURRENT_VERSION = 0x1;
        public const int PROCESS_POWER_THROTTLING_EXECUTION_SPEED = 0x1;
        public const int PROCESS_POWER_THROTTLING_IGNORE_TIMER_RESOLUTION = 0x4;

        public struct PROCESS_POWER_THROTTLING_STATE
        {
            public ULONG Version;
            public ULONG ControlMask;
            public ULONG StateMask;

            public override string ToString()
            {
                return $"PROCESS_POWER_THROTTLING_STATE:\n" +
                    $"\tVersion: {Version}\n" +
                    $"\tControlMask:{ControlMask}\n" +
                    $"\tStateMask:{StateMask}\n";
            }
        }

        public struct MEMORY_PRIORITY_INFORMATION
        {
            public ULONG MemoryPriority;

            public override string ToString()
            {
                return $"MEMORY_PRIORITY_INFORMATION:\n" +
                    $"\tMemoryPriority: {MemoryPriority}\n";
            }
        }
    }
}
