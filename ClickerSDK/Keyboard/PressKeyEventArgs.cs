using System;

namespace ClickerSDK.Keyboard
{
    public class PressKeyEventArgs : EventArgs
    {
        public PressKeyEventArgs(ConsoleKey consoleKeys, KeyModifers keyModifer)
        {
            ConsoleKeys = consoleKeys;
            KeyModifer = keyModifer;
        }

        public ConsoleKey ConsoleKeys { get; }

        public KeyModifers KeyModifer { get; }
    }
}