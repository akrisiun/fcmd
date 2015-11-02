using System;
using System.Windows;
using System.Windows.Controls;
using pluginner.Widgets;
using System.Windows.Threading;
using System.Windows.Input;
using fcmd.Platform;
using System.Windows.Media;

namespace fcmd.View.ctrl
{
    public static class VisibleSet
    {
        public static bool? Value(UIElement element, bool? value)
        {
            if (value == null)
                element.Visibility = Visibility.Hidden;
            else if (value ?? false)
                element.Visibility = Visibility.Visible;
            else
                element.Visibility = Visibility.Collapsed;

            return value;
        }
    }

    public static class HtmlMedia
    {
        public static Color ConvertFromString(this string htmlColor)
        {
            return (Color)ColorConverter.ConvertFromString(htmlColor);
        }
    }

    public class ButtonWidget : Button, IButton
    {
        public ButtonWidget() : this(null, null) { }

        public ButtonWidget(object parent = null, string Text = null)
        {
            if (Text != null)
                Content = Text;
        }

        pluginner.Widgets.IFcmdCommand IButton.Command { 
            get { return this.Command as pluginner.Widgets.IFcmdCommand; } 
            set { base.Command = value as IRelayCommand; } 
        }

        object IUIDispacher.Dispacher { get { return this.Dispatcher as Dispatcher; } }
        bool IUIDispacher.CheckAccess() { return (this as DispatcherObject).CheckAccess(); }
        public bool? Visible { get { return Visibility == Visibility.Visible; } set { VisibleSet.Value(this, value); } }

        public string Text
        {
            get { return Content as string ?? string.Empty; } // null safe
            set { Content = value; }
        }

        public bool CanGetFocus { get { return IsEnabled; } set { IsEnabled = value; } }

        protected override void OnClick()
        {
            if (clicked != null)
                clicked(this, EventArgs.Empty);

            if (Command != null && Command.CanExecute(null))
                Command.Execute(null);

            base.OnClick();
        }

        EventHandler clicked;
        public event EventHandler Clicked
        {
            add
            {
                // base.BackendHost.OnBeforeEventAdd(MenuItemEvent.Clicked, clicked);
                clicked += value;
            }
            remove
            {
                clicked -= value;
            }
        }
    }
}
