using fcmd.Controller;
using fcmd.View.Xaml;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace fcmd.Platform
{
    public abstract class Command : ICommand, pluginner.Widgets.IFcmdCommand, IRelayCommand
    {
        public bool Enabled { get; set; }
        public Action Action { get; set; }
        public FrameworkElement Target { get; set; }

#pragma warning disable 0649, 0067, 0414
        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter = null) { return Enabled; } //  && Target != null && Target.CheckAccess(); }
        public virtual void Execute(object parameter = null) { if (Action != null) Action(); }
    }
}
