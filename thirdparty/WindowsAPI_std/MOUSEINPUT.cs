using System;
using System.Runtime.InteropServices;

namespace luigi.thirdparty.WindowsAPI
{
    /// <summary>
    /// Contains information about a simulated mouse event.
    /// https://docs.microsoft.com/en-us/windows/desktop/api/winuser/ns-winuser-tagmouseinput
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public uint mouseData;
        public MouseEventFlags dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }
}
