using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace fcmd.Menu
{
    public static class MenuWpf
    {
        public static void Bind(MainWindow window)
        {
            theme.MenuWpf menu = window.Menu;

            menu.itemExit.Command = cmdExit.Command;    // F10
            menu.itemExit.InputGestureText = "F10";
            menu.itemExit.Header = "_Exit";
            // menu.itemExit.Click += (s, e) => cmdExit.Command.Execute(e);

            var bar = menu.menuBar;
            var edit = new MenuItem { Header = "_Edit" };
            bar.Items.Insert(1, edit);

            //var resort = new ObservableCollection<MenuItem>(bar.ItemsSource as IEnumerable<MenuItem>);
            //resort.Insert(1, edit);

            edit.Items.Add(new MenuItem { Width = 140, Header = "_Cut", InputGestureText = "Ctrl+X", Command = ApplicationCommands.Cut });
            edit.Items.Add(new MenuItem { Width = 140, Header = "_Copy", InputGestureText = "Ctrl+C", Command = ApplicationCommands.Copy });
            edit.Items.Add(new MenuItem { Width = 140, Header = "_Paste", InputGestureText = "Ctrl+P", Command = ApplicationCommands.Paste });
        }

        private static MainWindow MainWindow {  get { return Application.Current.MainWindow as MainWindow; } }

        public static CommandBinding cmdExit = new CommandBinding { Command = new ExitCommand() };

        //public static void ExitExecuted(object sender, ExecutedRoutedEventArgs e)
        ////{ string str = e.Parameter as string;
        //    // Mvvm_Variable.Action(Input: str);
        //    e.Handled = true;
    }

    public class ExitCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) { return true; }

        public void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }
    }

    public class EventCommand : ICommand
    {
        public Action<RoutedEventArgs> ExecuteCmd {get; set;}
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter) { return true; }

        public void Execute(object parameter)
        {
            ExecuteCmd(parameter as RoutedEventArgs);
        }
    }
}

//<Window.CommandBindings>
//        <CommandBinding Command = "Exit" Executed="ExitExecuted" />
//</Window.CommandBindings>

//mia = new MenuItem();
//mia.Header = "_Cut";
//mia.InputGestureText = "Ctrl+X";
//mi.Items.Add(mia);

/* TODO

public static RoutedUICommand Cut { get; }
public static RoutedUICommand Copy { get; }
public static RoutedUICommand Delete { get; }
public static RoutedUICommand Paste { get; }

//     The command.Default ValuesKey GestureF4UI TextProperties
public static RoutedUICommand Select ..
public static RoutedUICommand Mark..
public static RoutedUICommand UnMark..
public static RoutedUICommand SelectAll { get; }

public static RoutedUICommand Properties { get; }
public static RoutedUICommand Find { get; }
public static RoutedUICommand Replace { get; }

public static RoutedUICommand New { get; }
public static RoutedUICommand Open { get; }
public static RoutedUICommand PrintPreview { get; }

public static RoutedUICommand Save { get; }
public static RoutedUICommand SaveAs { get; }

*/
  
