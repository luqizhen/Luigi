using System;
using System.Runtime.InteropServices;

namespace luigi.thirdparty.WindowsAPI
{
    /// <summary>
    /// Contains information about a simulated keyboard event.
    /// https://docs.microsoft.com/en-us/windows/desktop/api/winuser/ns-winuser-tagkeybdinput
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct KEYBDINPUT
    {
        public ushort wVk;
        public ushort wScan;
        public KeyEventFlags dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }
}
