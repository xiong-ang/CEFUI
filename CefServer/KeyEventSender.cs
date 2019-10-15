using CefSharp;
using CefSharp.Wpf;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace CefServer
{
    public class KeyEventSender
    {

        [DllImport("user32.dll")]
        public static extern int ToUnicode(uint virtualKeyCode, uint scanCode,
            byte[] keyboardState,
            [Out, MarshalAs(UnmanagedType.LPWStr, SizeConst = 64)]
            StringBuilder receivingBuffer,
            int bufferSize, uint flags);

        /// <summary>
        /// https://stackoverflow.com/a/6949520/450141
        /// </summary>
        static string GetCharsFromKeys(Keys keys, bool shift, bool altGr)
        {
            var buf = new StringBuilder(256);
            var keyboardState = new byte[256];
            if (shift)
                keyboardState[(int)Keys.ShiftKey] = 0xff;
            if (altGr)
            {
                keyboardState[(int)Keys.ControlKey] = 0xff;
                keyboardState[(int)Keys.Menu] = 0xff;
            }
            ToUnicode((uint)keys, 0, keyboardState, buf, 256, 0);
            return buf.ToString();
        }

        public static void SendKey(ChromiumWebBrowser browser)
        {
            KeyEvent eventKey = new KeyEvent() { FocusOnEditableField = true, WindowsKeyCode = GetCharsFromKeys(Keys.V, false, false)[0], Modifiers = CefEventFlags.ControlDown, Type = KeyEventType.Char, IsSystemKey = false};
            browser.GetBrowser().GetHost().SendKeyEvent(eventKey);
        }
    }
}
