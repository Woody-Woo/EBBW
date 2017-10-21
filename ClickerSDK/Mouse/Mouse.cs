using System;

namespace ClickerSDK
{
    public class Mouse : IDisposable
    {
        private MouseHook _mouseHook;

        public static Mouse _Instanse;

        public static Mouse Instanse => _Instanse ?? (_Instanse = new Mouse()); 

        private Mouse()
        {
            _mouseHook = new MouseHook();
            _mouseHook.OnMouseClick += (sender, args) => RaiseOnClick(args);
        }

        public event EventHandler<MouseClickEventArgs> OnClick;

        private void RaiseOnClick(MouseClickEventArgs args)
        {
            OnClick?.Invoke(this, args);
        }

        public void MouseClick(MouseButton button, int X, int Y)
        {
            MouseHelper.MoveMouse(X,Y);
            MouseHelper.MouseClick(button);
        }

        public void Dispose()
        {
            _mouseHook.Dispose();
        }
    }
}