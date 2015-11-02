using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using pluginner.Widgets;
using System.Windows;
using System.Drawing;
using fcmd.View.ctrl;
using System.Windows.Threading;

namespace fcmd.View.ctrl
{
    // Xaml ComboBox

    public class ComboWidget : ComboBox, IComboWidget,
        ITextEntry, IInputElement
    {
        public bool CanGetFocus { get { return IsEnabled; } set { IsEnabled = value; } }

        public Color BackgroundColor
        {
            get { return ColorConvert.To(Background); }
            set { throw new NotImplementedException("no Background for TextEntry"); }
        }

        public bool? Visible { get { return Visibility == Visibility.Visible; } set { VisibleSet.Value(this, value); } }
        object IControl.Content { get { return this.DataContext; } set { this.DataContext = value; } }
        object IUIDispacher.Dispacher { get { return this.Dispatcher as Dispatcher; } }
        bool IUIDispacher.CheckAccess() { return (this as DispatcherObject).CheckAccess(); }

        public new Xwt.ItemCollection Items
        {
            get { throw new NotImplementedException(); }
        }

    }
}
