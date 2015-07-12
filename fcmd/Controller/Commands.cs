using fcmd.Platform;
using fcmd.View.ctrl;
using pluginner.Widgets;
using System;
//using System.Windows;
//using System.Windows.Input;

namespace fcmd.Controller
{
    public enum PanelSide
    {
        Undefined = 0,
        Left = 1,
        Right = 2
    }

    public interface ICommand
    { }


    public class ExitCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) { return true; }

        public void Execute(object parameter)
        {
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
