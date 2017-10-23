using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ClickerSDK.Wind
{
    public static  class WindHandle
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string strClassName, string strWindowName);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        public struct Rect
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }

        public static void ActiveWindow(Process p)
        {
            SetForegroundWindow(p.MainWindowHandle);
        }

        public static Rect GetWindowRect(Process p)
        {
            IntPtr ptr = p.MainWindowHandle;
            Rect notepadRect = new Rect();
            GetWindowRect(ptr, ref notepadRect);

            return notepadRect;
        }
    }
}