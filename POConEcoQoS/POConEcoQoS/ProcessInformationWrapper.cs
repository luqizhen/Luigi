using System;
using System.Runtime.InteropServices;

namespace POConEcoQoS
{
    internal class ProcessInformationWrapper
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

        public static int SetProcessInfo(IntPtr handle, WinAPI.PROCESS_INFORMATION_CLASS piClass, object processInfo)
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
                return result;
            }

            processInfo = null;
            return 0;
        }
    }
}