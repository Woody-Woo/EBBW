using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ClickerSDK.Keyboard
{
    internal static class KeyboardHelper
    {
        const UInt32 WM_KEYDOWN = 0x0100;
        const int VK_F5 = 0x74;

        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(IntPtr hWnd);

        internal static void SendKeyPress(Process process, string key)
        {
            SetForegroundWindow(process.MainWindowHandle);
            SendKeys.SendWait(key);
        }

        internal static KeyModifers GetKeyMod(int code)
        {
            try
            {
                return (KeyModifers)code;
            }
            catch
            {
                return KeyModifers.Unknow;
            }
        }

    }
}