using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace EcoQoS.Core
{
    public class ProcessInformationWrapper
    {
        public static bool GetProcessInfo(IntPtr handle, WinAPI.PROCESS_INFORMATION_CLASS piClass, out object processInfo)
        {
            Type infoType = null;
            switch (piClass)
            {
                case WinAPI.PROCESS_INFORMATION_CLASS.ProcessMemoryPriority:
                    infoType = typeof(WinAPI.MEMORY_PRIORITY_INFORMATION);
                    break;
                case WinAPI.PROCESS_INFORMATION_CLASS.ProcessPowerThrottling:
                    infoType = typeof(WinAPI.PROCESS_POWER_THROTTLING_STATE);
                    break;
                default:
                    break;
            }

            if (infoType != null)
            {
                int sizeOfProcessInfo = Marshal.SizeOf(infoType);
                var pProcessInfo = Marshal.AllocHGlobal(sizeOfProcessInfo);
                var result = WinAPI.GetProcessInformation(handle, piClass, pProcessInfo, sizeOfProcessInfo);
                processInfo = Marshal.PtrToStructure(pProcessInfo, infoType);
                Marshal.FreeHGlobal(pProcessInfo);
                return result != 0;
            }

            processInfo = null;
            return false;
        }

        public static bool SetProcessInfo(IntPtr handle, WinAPI.PROCESS_INFORMATION_CLASS piClass, object processInfo)
        {
            Type infoType = null;
            switch (piClass)
            {
                case WinAPI.PROCESS_INFORMATION_CLASS.ProcessMemoryPriority:
                    infoType = typeof(WinAPI.MEMORY_PRIORITY_INFORMATION);
                    break;
                case WinAPI.PROCESS_INFORMATION_CLASS.ProcessPowerThrottling:
                    infoType = typeof(WinAPI.PROCESS_POWER_THROTTLING_STATE);
                    break;
                default:
                    break;
            }

            if (infoType != null)
            {
                int sizeOfProcessInfo = Marshal.SizeOf(infoType);

                var pProcessInfo = Marshal.AllocHGlobal(sizeOfProcessInfo);
                Marshal.StructureToPtr(processInfo, pProcessInfo, false);
                var result = WinAPI.SetProcessInformation(handle, piClass, pProcessInfo, sizeOfProcessInfo);
                Marshal.FreeHGlobal(pProcessInfo);
                return result != 0;
            }

            return false;
        }

        public static bool SwitchToEcoQoS()
        {
            var process = Process.GetCurrentProcess();
            WinAPI.PROCESS_POWER_THROTTLING_STATE pi = new WinAPI.PROCESS_POWER_THROTTLING_STATE
            {
                Version = WinAPI.PROCESS_POWER_THROTTLING_CURRENT_VERSION,
                ControlMask = WinAPI.PROCESS_POWER_THROTTLING_EXECUTION_SPEED,
                StateMask = WinAPI.PROCESS_POWER_THROTTLING_EXECUTION_SPEED
            };

            return SetProcessInfo(process.Handle, WinAPI.PROCESS_INFORMATION_CLASS.ProcessPowerThrottling, pi);
        }

        public static bool SwitchToHighQoS()
        {
            var process = Process.GetCurrentProcess();
            WinAPI.PROCESS_POWER_THROTTLING_STATE pi = new WinAPI.PROCESS_POWER_THROTTLING_STATE
            {
                Version = WinAPI.PROCESS_POWER_THROTTLING_CURRENT_VERSION,
                ControlMask = WinAPI.PROCESS_POWER_THROTTLING_EXECUTION_SPEED,
                StateMask = 0
            };

            return SetProcessInfo(process.Handle, WinAPI.PROCESS_INFORMATION_CLASS.ProcessPowerThrottling, pi);
        }
    }
}