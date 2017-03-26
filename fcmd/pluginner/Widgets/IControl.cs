using SharpShell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pluginner.Widgets
{
#if WPF
    public interface IControl : IInputElement // UIElement, IFrameworkInputElement
    {
        bool CanGetFocus { get; set; }  // -> IsEnabled
        Color BackgroundColor { get; set; }
    }

    public interface IUIDispacher
    {
        object Dispacher { get; }
    }


#else

    public interface IUIDispacher
    {
        object Dispacher { get; }
        bool CheckAccess();  // check UI thread
    }

    public interface IControl : IUIDispacher, IWin32Window, IDisposable
    {
        object Content { get; set; }
        bool? Visible { get; set; }
    }
#endif
}
