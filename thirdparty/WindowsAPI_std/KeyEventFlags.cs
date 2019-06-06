using System;

namespace luigi.thirdparty.WindowsAPI
{
    /// <summary>
    /// Specifies various aspects of a keystroke. This member can be certain combinations of the following values.
    /// https://docs.microsoft.com/en-us/windows/desktop/api/winuser/ns-winuser-tagkeybdinput
    /// </summary>
    [Flags]
    public enum KeyEventFlags : uint
    {
        KEYEVENTF_KEYDOWN = 0x0000,
        KEYEVENTF_EXTENDEDKEY = 0x0001,
        KEYEVENTF_KEYUP = 0x0002,
        KEYEVENTF_UNICODE = 0x0004,
        KEYEVENTF_SCANCODE = 0x0008
    }
}
