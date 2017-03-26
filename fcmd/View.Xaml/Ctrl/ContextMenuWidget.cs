using pluginner.Widgets;
using SharpShell;
using System;
using System.Windows.Controls;

namespace fcmd.View.Xaml.ctrl
{
    public class ContextMenuWidget : ContextMenu, IWin32Window, IDisposable
    {
        public IntPtr Handle { get; set; }
        public Tuple<int, int> PointToScreen(int X, int Y) { return Win32Control.PointToScreen(this, this.Handle, X, Y); }

        public IControl Owner { get; set; }

        public virtual void Dispose() {  }

    }
}
