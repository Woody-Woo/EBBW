using System;

namespace ClickerSDK
{
    public class MouseClickEventArgs : EventArgs
    {
        public MouseClickEventArgs()
        {
        }

        public MouseClickEventArgs(MouseButton mouseButton, int x, int y)
        {
            MouseButton = mouseButton;
            X = x;
            Y = y;
        }

        public MouseButton MouseButton { get; }

        public int X { get;  }

        public int Y { get; }
    }
}