using fcmd.View.ctrl;
using fcmd.View.Xaml;
using pluginner.Widgets;
using System.Windows;
using System.Windows.Input;
using fcmd.base_plugins.fs;
using System;

namespace fcmd.Model
{
    using ICommand = System.Windows.Input.ICommand;
    using fcmd.Platform;

    public abstract class Command : ICommand, IRelayCommand 
    {
        public bool Enabled { get; set; }
        public Action Action { get; set; }
        public FrameworkElement Target { get; set; }

#pragma warning disable 0649, 0067, 0414
        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute(object parameter = null) { return Enabled; } //  && Target != null && Target.CheckAccess(); }
        public virtual void Execute(object parameter = null) { if (Action != null) Action(); }
    }

    public class ChdirUpCommand : Command { }

    public class ChRootCommand : Command { }

    public static class GridEvents
    {
        public static void BindGridEvents(this ListView2DataGrid @this)
        {
            @this.SelectionChanged += SelectionChanged;
            @this.PreviewMouseDoubleClick += PreviewMouseDoubleClick;

            var panel = @this.Panel;
            panel.path.DataContext = @this;
            //panel.cdUp.DataContext = @this;
            //panel.cdRoot.DataContext = @this;
            panel.path.KeyDown += Path_KeyDown;

            if (panel.cdUp.Command == null)
            {
                panel.cdUp.Command = new ChdirUpCommand { Target = @this, Action = () => CdUp_PreviewMouseDown(@this), Enabled = true };
                //panel.cdUp.PreviewMouseDown += (s, e) =>
                //{
                //    if (@this.Panel.cdUp.Command.CanExecute(null))
                //    {
                //        e.Handled = true;
                //        CdUp_PreviewMouseDown(@this);
                //    };
                //};
            }
            else
                (panel.cdUp.Command as Command).Enabled = true;

            if (panel.cdRoot.Command == null)
            {
                panel.cdRoot.Command =
                    new ChRootCommand { Target = @this, Action = () => CdRoot_PreviewMouseLeftButtonDown(@this), Enabled = true };

                //panel.cdRoot.Clicked
            }
            else
                (panel.cdRoot.Command as Command).Enabled = true;

            panel.data.Tag = @this;
            panel.data.PreviewKeyDown += Data_PreviewKeyDown;
        }

        public static void UnBindGridEvents(this ListView2DataGrid @this)
        {
            @this.SelectionChanged -= SelectionChanged;
            @this.PreviewMouseDoubleClick -= PreviewMouseDoubleClick;

            var panel = @this.Panel;
            panel.path.KeyDown -= Path_KeyDown;
            //@this.Panel.cdUp.PreviewMouseLeftButtonDown -= CdUp_PreviewMouseDown;
            //@this.Panel.cdRoot.PreviewMouseLeftButtonDown -= CdRoot_PreviewMouseLeftButtonDown;
            (panel.cdUp.Command as Command).Enabled = false;
            (panel.cdRoot.Command as Command).Enabled = false;

            panel.data.PreviewKeyDown -= Data_PreviewKeyDown;
        }

        private static void CdRoot_PreviewMouseLeftButtonDown(ListView2DataGrid @this) // object sender, MouseButtonEventArgs e)
        {
            // var @this = (sender as FrameworkElement).DataContext as ListView2Widget;
            var FS = @this.FileList.FS;

            var path = FS.NoPrefix(FS.RootDirectory);
            if (path == null || path.Length == 0)
            {
                MessageBox.Show("directory Root error");
                return;
            }
            @this.LoadDir(path);
            // e.Handled = true;
        }

        private static void CdUp_PreviewMouseDown(ListView2DataGrid @this) // object sender, MouseButtonEventArgs e)
        {
            // var @this = (sender as FrameworkElement).DataContext as ListView2Widget;
            var FS = @this.FileList.FS;
            var path = FS.NoPrefix(FS.CurrentDirectory + FS.DirSeparator + "..");
            @this.LoadDir(path);
            //  e.Handled = true;
        }

        private static void Path_KeyDown(object sender, KeyEventArgs e)
        {
            var @this = (sender as FrameworkElement).DataContext as ListView2DataGrid;
            if (e.Key == Key.Enter)
            {
                var path = (e.Source as TextEntry).Text.Replace(localFileSystem.FilePrefix, "");
                @this.LoadDir(path);
                e.Handled = true;
            }
        }

        static void PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var @this = sender as ListView2DataGrid;
            if (@this.SelectEnter(@this.SelectedItem as ListItemXaml))
                e.Handled = true;
        }


        private static void Data_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var @this = (sender as FrameworkElement).Tag as ListView2DataGrid;
            if (e.Key == Key.Enter && @this.SelectEnter(@this.SelectedItem as ListItemXaml))
                e.Handled = true;
        }


        static void SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var @this = sender as ListView2DataGrid;
            var list = e.AddedItems;
            ListItemXaml lastItem = null;
            foreach (ListItemXaml item in list)
            {
                @this.Select(item);
                lastItem = item;
            }

            if (lastItem != null)
                @this.SelectLast(lastItem);
        }

    }

}
