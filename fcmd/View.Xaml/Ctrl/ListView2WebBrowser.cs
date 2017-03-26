using System;
using System.Windows;
using System.Windows.Controls;
using pluginner.Widgets;
using System.Diagnostics;
using System.Windows.Threading;
using SharpShell;

namespace fcmd.View.ctrl
{
    /// <summary>
    /// .http plugin visual object class
    /// </summary>
    public class ListView2WebBrowser : UIElement, IControl
    {
        public WebBrowser Browser { [DebuggerStepThrough] get; protected set; }
        public IntPtr Handle { get; set; }
        public Tuple<int, int> PointToScreen(int X, int Y) { return Win32Control.PointToScreen(this, this.Handle, X, Y); }

        public bool? Visible {
            get { return Browser == null ? (bool?)null : (Browser.Visibility == Visibility.Visible); }
            set { VisibleSet.Value(Browser, value); }
        }
        object IUIDispacher.Dispacher { get { return this.Browser.Dispatcher as Dispatcher ?? Dispatcher; } }
        bool IUIDispacher.CheckAccess() { return (this.Browser as DispatcherObject).CheckAccess(); }

        public WebBrowser CreateControl(FrameworkElement container = null)
        {
            Browser = new WebBrowser();

            Browser.Visibility = Visibility.Visible;
            return Browser;
        }

        public string Url { get; set; }
        public void Navigate(string url)
        {
            Url = url;
            Browser.Navigate(url);
        }

        public object Content { get { return Browser == null ? null : Browser.DataContext; } set { } }
        public virtual void Dispose() { }
    }

}
