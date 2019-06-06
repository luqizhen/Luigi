using System.Runtime.InteropServices;

namespace luigi.thirdparty.WindowsAPI
{
    /// <summary>
    /// Used by SendInput to store information for synthesizing input events such as keystrokes, mouse movement, and mouse clicks.
    /// https://docs.microsoft.com/en-us/windows/desktop/api/winuser/ns-winuser-taginput
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct INPUT
    {
        /// <summary>
        /// The type of the input event. 
        /// </summary>
        public InputType TInput;

        /// <summary>
        /// The information about a simulated event.
        /// </summary>
        public INPUTUNION UInput;

        /// <summary>
        /// Size of INPUT Struct
        /// </summary>
        public static int Size { get; private set; } = Marshal.SizeOf(typeof(INPUT));

        /// <summary>
        /// Create Input Parameter For Mouse
        /// </summary>
        /// <param name="mouseInput">MOUSE INPUT</param>
        /// <returns></returns>

        public static INPUT MouseInput(MOUSEINPUT mouseInput)
        {
            return new INPUT { TInput = InputType.INPUT_MOUSE, UInput = new INPUTUNION { MInput = mouseInput } };
        }

        /// <summary>
        /// Create Input Parameter For Keyboard
        /// </summary>
        /// <param name="keyboardInput">KEYBD INPUT</param>
        /// <returns></returns>
        public static INPUT KeyboardInput(KEYBDINPUT keyboardInput)
        {
            return new INPUT { TInput = InputType.INPUT_KEYBOARD, UInput = new INPUTUNION { KInput = keyboardInput } };
        }
    }
}
