using System.Runtime.InteropServices;

namespace luigi.thirdparty.WindowsAPI
{
    /// <summary>
    /// The information about a simulated event.
    /// https://docs.microsoft.com/en-us/windows/desktop/api/winuser/ns-winuser-taginput
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct INPUTUNION
    {
        [FieldOffset(0)]
        public MOUSEINPUT MInput;

        [FieldOffset(0)]
        public KEYBDINPUT KInput;
    }
}
