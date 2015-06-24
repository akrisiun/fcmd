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
            foreach (Control item in EditItems())
            {
                item.Width = 140;
                edit.Items.Add(item);
            }
        }

        public static IEnumerable<Control> EditItems()
        {
            /* TODO: Edit: 
                Cut Copy Paste Delete
                SelectAll Mark.. UnMark
                Properties Find Replace
                New Open Edit PrintPreview Save SaveAs
            */

            yield return new MenuItem { Header = "_Cut", InputGestureText = "Ctrl+X", Command = ApplicationCommands.Cut };
            yield return new MenuItem { Header = "_Copy", InputGestureText = "Ctrl+C", Command = ApplicationCommands.Copy };
            yield return new MenuItem { Header = "_Paste", InputGestureText = "Ctrl+P", Command = ApplicationCommands.Paste };
            yield return new Separator();

            yield return new MenuItem { Header = "Select all", Command = ApplicationCommands.SelectAll };
            yield return new MenuItem { Header = "Unselect .." };
            yield return new Separator();

            yield return new MenuItem { Header = "Find", InputGestureText = "Ctrl+F", Command = ApplicationCommands.Find };
            yield return new MenuItem { Header = "Replace", InputGestureText = "Ctrl+H", Command = ApplicationCommands.Replace };
            yield return new MenuItem { Header = "Properties", InputGestureText = "Shift+F10", Command = ApplicationCommands.Properties };
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


//InitializeComponent();
//    EventManager.RegisterClassHandler(typeof(Button), MouseDownEvent, new RoutedEventHandler(OnMouseDown));
//private void OnMouseDown(object sender, RoutedEventArgs e) {
//    var element = sender as ContentControl;
//    if (element != null)
//        ShowLocation(element);
//private void ShowLocation(ContentControl element) {
//    var location = element.PointToScreen(new Point(0, 0));
//MessageBox.Show(string.Format("{2}'s location is ({0}, {1})", location.X, location.Y, element.Content));

//mia = new MenuItem();
//mia.Header = "_Cut";
//mia.InputGestureText = "Ctrl+X";
