using fcmd.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
// using System.Windows.Controls;
using System.Windows.Input;
using Xwt;

namespace fcmd.Menu
{
    public static class MenuWpf
    {
        public static void Bind(MainWindow window)
        {
            //MenuPanelWpf menu = window.Menu;

            //menu.itemExit.Command = cmdExit.Command;    // F10
            //menu.itemExit.InputGestureText = "F10";
            //menu.itemExit.Header = "_Exit";
            //// menu.itemExit.Click += (s, e) => cmdExit.Command.Execute(e);

            //var bar = menu.menuBar;
            //var edit = new MenuItem { Header = "_Edit" };
            //bar.Items.Insert(1, edit);

            //foreach (Control item in EditItems())
            //{
            //    item.Width = 200;
            //    edit.Items.Add(item);
            //}
        }

        //public static IEnumerable<Control> EditItems()
        //{
        //    /* TODO: Edit: 
        //        Cut Copy Paste Delete
        //        SelectAll Mark.. UnMark
        //        Properties Find Replace
        //        New Open Edit PrintPreview Save SaveAs
        //    */

//    yield return new MenuItem { Header = "_Cut", InputGestureText = "Ctrl+X", Command = ApplicationCommands.Cut };
//    yield return new MenuItem { Header = "_Copy", InputGestureText = "Ctrl+C", Command = ApplicationCommands.Copy };
//    yield return new MenuItem { Header = "_Paste", InputGestureText = "Ctrl+P", Command = ApplicationCommands.Paste };
//    yield return new Separator();

//    yield return new MenuItem { Header = "Select all", Command = ApplicationCommands.SelectAll };
//    yield return new MenuItem { Header = "Unselect .." };
//    yield return new Separator();

//    yield return new MenuItem { Header = "Find", InputGestureText = "Ctrl+F", Command = ApplicationCommands.Find };
//    yield return new MenuItem { Header = "Replace", InputGestureText = "Ctrl+H", Command = ApplicationCommands.Replace };
//    yield return new MenuItem { Header = "Properties", InputGestureText = "Shift+F10", Command = ApplicationCommands.Properties };
//}

#if WPF
        private static MainWindow MainWindow {  get {
                return System.Windows.Application.Current.MainWindow as MainWindow; } }

        public static CommandBinding cmdExit = null;
            //new CommandBinding { Command = new ExitCommand() };
#endif

        //public static void ExitExecuted(object sender, ExecutedRoutedEventArgs e)
        ////{ string str = e.Parameter as string;
        //    // Mvvm_Variable.Action(Input: str);
        //    e.Handled = true;
    }

}