using fcmd.Controller;
using pluginner.Widgets;
using System;
using System.Collections.ObjectModel;

namespace fcmd.Platform
{
#if WPF
    using fcmd.View.Xaml;
    using System.Windows;
    using System.Windows.Input;

    public abstract class Command : ICommand, pluginner.Widgets.IFcmdCommand, IRelayCommand
    {
        public FrameworkElement Target { get; set; }
#else
    public abstract class Command : pluginner.Widgets.IFcmdCommand, IRelayCommand
    {
        public IControl Target { get; set; }
#endif
        public bool Enabled { get; set; }
        public Action Action { get; set; }

#pragma warning disable 0649, 0067, 0414
        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter = null) { return Enabled; } //  && Target != null && Target.CheckAccess(); }
        public virtual void Execute(object parameter = null) { if (Action != null) Action(); }
    }
}
