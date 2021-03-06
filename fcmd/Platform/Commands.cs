﻿using fcmd.Platform;
using pluginner.Widgets;
using System;

namespace fcmd.Controller
{
    public class ExitCommand : IRelayCommand
    {
#pragma warning disable 0649, 0067, 0414  // is assigned but never used
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

    public class EventCommand : IRelayCommand
    {
        public Action<object> ExecuteCmd { get; set; }
        public Object Target { get; set; }

#pragma warning disable 0649, 0414  // is assigned but never used
        public event EventHandler CanExecuteChanged;
#pragma warning restore 0649, 0414  

        public bool CanExecute(object parameter) { return true; }

        public void Execute(object parameter)
        {
            ExecuteCmd(parameter);
        }
    }
}
