using System;
using System.Windows;
using System.Windows.Controls;
using pluginner.Widgets;
using System.Drawing;
using System.Windows.Threading;

namespace fcmd.View.ctrl
{
    // Xaml TextEntry

    public class TextEntry : TextBox, ITextEntry, IInputElement
    {
        public bool CanGetFocus { get { return IsEnabled; } set { IsEnabled = value; } }

        public Color BackgroundColor
        {
            get { return ColorConvert.To(Background); }
            set { throw new NotImplementedException("no Background for TextEntry"); }
        }

        object IControl.Content { get { return this.DataContext; } set { this.DataContext = value; } }
        object IUIDispacher.Dispacher { get { return this.Dispatcher as Dispatcher; } }
        bool IUIDispacher.CheckAccess() { return (this as DispatcherObject).CheckAccess(); }
    }

    // Xaml ComboBox

    public class ComboWidget : ComboBox, ITextEntry, IInputElement
    {
        public bool CanGetFocus { get { return IsEnabled; } set { IsEnabled = value; } }

        public Color BackgroundColor
        {
            get { return ColorConvert.To(Background); }
            set { throw new NotImplementedException("no Background for TextEntry"); }
        }

        object IControl.Content { get { return this.DataContext; } set { this.DataContext = value; } }
        object IUIDispacher.Dispacher { get { return this.Dispatcher as Dispatcher; } }
        bool IUIDispacher.CheckAccess() { return (this as DispatcherObject).CheckAccess(); }

    }

}