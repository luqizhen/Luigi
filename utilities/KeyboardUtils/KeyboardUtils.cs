using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WindowsAPI;
using static WindowsAPI.User32;

namespace luigi.utilities
{
    public partial class KeyboardUtils
    {
        /// <summary>
        /// Press screenshot
        /// </summary>
        public static void PressScreenshot()
        {
            keybd_event((byte)Key.SNAPSHOT, 0x45, (uint)KeyEventFlags.KEYEVENTF_EXTENDEDKEY, UIntPtr.Zero);
            keybd_event((byte)Key.SNAPSHOT, 0x45, (uint)(KeyEventFlags.KEYEVENTF_EXTENDEDKEY | KeyEventFlags.KEYEVENTF_KEYUP), UIntPtr.Zero);
        }

        /// <summary>
        /// When the keyboard button is pressed
        /// </summary>
        /// <param name="key">key</param>
        public static void PressDown(Key key)
        {
            keybd_event((byte)key, 0, (uint)KeyEventFlags.KEYEVENTF_KEYDOWN, UIntPtr.Zero);
        }

        /// <summary>
        /// When the keyboard button is released
        /// </summary>
        /// <param name="key">key</param>
        public static void PressUp(Key key)
        {
            keybd_event((byte)key, 0, (uint)KeyEventFlags.KEYEVENTF_KEYUP, UIntPtr.Zero);
        }

        /// <summary>
        /// Press the keyboard button
        /// </summary>
        /// <param name="key">key</param>
        public static void Press(Key key)
        {
            PressDown(key);
            PressUp(key);
        }
        
        /// <summary>
        /// Type String
        /// </summary>
        /// <param name="text">text</param>
        public static void TypeString(string text)
        {
            foreach (char character in text)
            {
                TypeChar(character);
            }
        }

        /// <summary>
        /// Type Char
        /// </summary>
        /// <param name="character">character</param>
        public static void TypeChar(char character)
        {
            short code = User32.VkKeyScan(character);
            var low = (byte)(code & 0xff);
            SendInput(low, true);
            SendInput(low, false);

        }

        /// <summary>
        /// Press Ctrl、Alt and a letter Combination Keys
        /// </summary>
        /// <param name="character">character</param>
        public static void PressCtrlAndAlt(char character)
        {
            List<byte> keys = new List<byte>();
            keys.Add((byte)Key.CONTROL);
            keys.Add((byte)Key.ALT);
            keys.Add(CharToByte(character));
            PressCombinationKey(keys);
        }

        /// <summary>
        /// PressKeys
        /// </summary>
        /// <param name="keys">Key parameter  byte[]</param>
        public static void PressKeys(params Key[] keys)
        {
            keys.ToList().ForEach(key => keybd_event((byte)key, 0, (byte)KeyEventFlags.KEYEVENTF_KEYDOWN, UIntPtr.Zero));
            keys.Reverse().ToList().ForEach(key => keybd_event((byte)key, 0, (byte)KeyEventFlags.KEYEVENTF_KEYUP, UIntPtr.Zero));
            Thread.Sleep(250);
        }

        /// <summary>
        /// CharToByte
        /// </summary>
        /// <param name="character">character</param>
        internal static byte CharToByte(char character)
        {
            short code = User32.VkKeyScan(character);
            return (byte)(code & 0xff);
        }

        /// <summary>
        /// Press Combination Keys
        /// </summary>
        /// <param name="keys">Key parameter List<byte></param>
        public static void PressCombinationKey(List<byte> keys)
        {
            foreach (Key b in keys)
            {
                PressDown(b);
            }
            foreach (Key b in keys)
            {
                PressUp(b);
            }
        }

        /// <summary>
        /// Press Ctrl+Alt+T to switch to the next language.
        /// </summary>
        public static void SwitchLanguage()
        {
            PressDown(Key.LCTRL);
            PressDown(Key.LALT);
            PressDown(Key.T);
            PressUp(Key.T);
            PressUp(Key.LALT);
            PressUp(Key.LCTRL);
        }

        /// <summary>
        /// Press Ctrl+Shift+P to captrue a screen shot.
        /// </summary>
        public static void CaptureScreenShot()
        {
            PressDown(Key.LCTRL);
            PressDown(Key.LSHIFT);
            PressDown(Key.P);
            PressUp(Key.P);
            PressUp(Key.LSHIFT);
            PressUp(Key.LCTRL);

            Thread.Sleep(2500);
        }

        /// <summary>
        /// Synthesizes keystrokes, mouse motions, and button clicks.
        /// </summary>
        /// <param name="keyCode">Key code</param>
        /// <param name="isKeyDown">Whether the button is pressed</param>
        public static void SendInput(ushort keyCode, bool isKeyDown)
        {
            KEYBDINPUT keyboardInput = new KEYBDINPUT
            {
                time = 0,
                dwExtraInfo = User32.GetMessageExtraInfo()
            };

            if (isKeyDown == false) keyboardInput.dwFlags |= KeyEventFlags.KEYEVENTF_KEYUP;

            keyboardInput.wVk = keyCode;

            var input = INPUT.KeyboardInput(keyboardInput);

            User32.SendInput(1, new[] { input }, INPUT.Size);
        }
    }
}