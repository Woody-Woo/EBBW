using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ClickerSDK.Keyboard
{
    public class Keyboard : IDisposable
    {
        private static Keyboard _Instanse;
        public static Keyboard Instanse => _Instanse ?? (_Instanse = new Keyboard());
        private readonly GlobalKeyboardHook _globalKeyboardHook;
        protected Process Process;


        private Keyboard()
        {
            _globalKeyboardHook = new GlobalKeyboardHook();
            _globalKeyboardHook.KeyboardPressed += _globalKeyboardHook_KeyboardPressed; ;
        }

        public void SetProcess(Process process)
        {
            Process = process;
        }

        private void _globalKeyboardHook_KeyboardPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            RaiseOnKeyPress(new PressKeyEventArgs((ConsoleKey)e.KeyboardData.VirtualCode, KeyboardHelper.GetKeyMod(e.KeyboardData.Flags)));
        }

        /// <summary>
        /// https://msdn.microsoft.com/en-us/library/system.windows.forms.sendkeys.send(v=vs.110).aspx
        /// </summary>
        /// <param name="key"></param>
        public void SendKeys(string key)
        {
            if (Process == null)
            {
                throw new InvalidOperationException("Process no setted. Please set process.");
            }

            KeyboardHelper.SendKeyPress(Process,key);
        }

        public event EventHandler<PressKeyEventArgs> OnKeyPress;

        protected void RaiseOnKeyPress(PressKeyEventArgs args)
        {
            OnKeyPress?.Invoke(this,args);
        }

        public void Dispose()
        {
            _globalKeyboardHook.Dispose();
        }
    }
}