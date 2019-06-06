using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAPI
{
    public static class User32
    {
        /// <summary>
        /// Synthesizes a keystroke. The system can use such a synthesized keystroke to generate a WM_KEYUP or WM_KEYDOWN message. The keyboard driver's interrupt handler calls the keybd_event function.
        /// https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-keybd_event
        /// </summary>
        /// <param name="bVk">A virtual-key code. The code must be a value in the range 1 to 254</param>
        /// <param name="bScan">A hardware scan code for the key.</param>
        /// <param name="dwFlags">Controls various aspects of function operation. This parameter can be one or more of the following values.</param>
        /// <param name="dwExtraInfo">An additional value associated with the key stroke.</param>
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        /// <summary>
        /// Translates a character to the corresponding virtual-key code and shift state for the current keyboard.
        /// https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-vkkeyscana
        /// </summary>
        /// <param name="ch">The character to be translated into a virtual-key code.</param>
        /// <returns>
        /// If the function succeeds, the low-order byte of the return value contains the virtual-key code and the high-order byte contains the shift state,
        /// which can be a combination of the following flag bits.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern short VkKeyScan(char ch);

        /// <summary>
        /// Synthesizes keystrokes, mouse motions, and button clicks.
        /// https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-sendinput
        /// </summary>
        /// <param name="nInputs">The number of structures in the pInputs array.</param>
        /// <param name="pInputs">An array of INPUT structures. Each structure represents an event to be inserted into the keyboard or mouse input stream.</param>
        /// <param name="cbSize">The size, in bytes, of an INPUT structure. If cbSize is not the size of an INPUT structure, the function fails.</param>
        /// <returns>
        /// The function returns the number of events that it successfully inserted into the keyboard or mouse input stream. If the function returns zero, the input was already blocked by another thread. 
        /// To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] INPUT[] pInputs, int cbSize);

        /// <summary>
        /// Retrieves the extra message information for the current thread. Extra message information is an application- or driver-defined value associated with the current thread's message queue.
        /// https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-getmessageextrainfo
        /// /// </summary>
        /// <returns>
        /// The return value specifies the extra information. The meaning of the extra information is device specific.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetMessageExtraInfo();

        /// <summary>
        /// The mouse_event function synthesizes mouse motion and button clicks.
        /// https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-mouse_event
        /// </summary>
        /// <param name="dwFlags">Controls various aspects of mouse motion and button clicking. This parameter can be certain combinations of the following values.</param>
        /// <param name="dx">The mouse's absolute position along the x-axis or its amount of motion since the last mouse event was generated, depending on the setting of MOUSEEVENTF_ABSOLUTE.</param>
        /// <param name="dy">The mouse's absolute position along the y-axis or its amount of motion since the last mouse event was generated, depending on the setting of MOUSEEVENTF_ABSOLUTE. </param>
        /// <param name="dwData">DWORD</param>
        /// <param name="dwExtraInfo">An additional value associated with the mouse event. An application calls GetMessageExtraInfo to obtain this extra information.</param>
        /// <returns>This function has no return value.</returns>
        [DllImport("user32")]
        public static extern int mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
    }
}
