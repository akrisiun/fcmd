using System;
using System.Windows;
using System.Windows.Controls;
using pluginner.Widgets;
using System.Drawing;

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

    }

}