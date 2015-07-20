using fcmd.Platform;
using pluginner.Widgets;
using System;

namespace fcmd.Controller
{
    public enum PanelSide
    {
        Undefined = 0,
        Left = 1,
        Right = 2
    }

    public class ExitCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) { return true; }

        public void Execute(object parameter)
        {
            CanExecuteChanged = null;
#if WPF
            Application.Current.Shutdown();
#else 
            Xwt.Application.Exit();
#endif
        }
    }

    public class EventCommand : ICommand
    {
        public Action<object> ExecuteCmd { get; set; }
        public Object Target { get; set; }

        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) { return true; }

        public void Execute(object parameter)
        {
            ExecuteCmd(parameter);
        }
    }
}
