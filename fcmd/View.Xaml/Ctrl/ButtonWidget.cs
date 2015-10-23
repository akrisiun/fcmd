using System;
using System.Windows;
using System.Windows.Controls;
using pluginner.Widgets;
using System.Windows.Threading;
using System.Windows.Input;
using fcmd.Platform;

namespace fcmd.View.ctrl
{
    public class ButtonWidget : Button, IButton
    {
        public ButtonWidget() : this(null, null) { }

        public ButtonWidget(object parent = null, string Text = null)
        {
            if (Text != null)
                Content = Text;
        }

        pluginner.Widgets.ICommand IButton.Command { 
            get { return this.Command as pluginner.Widgets.ICommand; } 
            set { base.Command = value as IRelayCommand; } 
        }

        object IUIDispacher.Dispacher { get { return this.Dispatcher as Dispatcher; } }
        bool IUIDispacher.CheckAccess() { return (this as DispatcherObject).CheckAccess(); }

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
