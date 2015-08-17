using fcmd.View.ctrl;
using fcmd.View.Xaml;
using pluginner.Widgets;
using System.Windows;
using System.Windows.Input;
using fcmd.base_plugins.fs;

namespace fcmd.Model
{
    public static class GridEvents
    {
        public static void BindGridEvents(this ListView2Widget @this)
        {
            @this.SelectionChanged += SelectionChanged;
            @this.PreviewMouseDoubleClick += PreviewMouseDoubleClick;

            @this.Panel.path.DataContext = @this;
            @this.Panel.cdUp.DataContext = @this;
            @this.Panel.cdRoot.DataContext = @this;
            @this.Panel.path.KeyDown += Path_KeyDown;
            @this.Panel.cdUp.PreviewMouseDown += CdUp_PreviewMouseDown;
            @this.Panel.cdRoot.PreviewMouseLeftButtonDown += CdRoot_PreviewMouseLeftButtonDown;

            @this.Panel.data.Tag = @this;
            @this.Panel.data.PreviewKeyDown += Data_PreviewKeyDown;
        }

        public static void UnBindGridEvents(this ListView2Widget @this)
        {
            @this.SelectionChanged -= SelectionChanged;
            @this.PreviewMouseDoubleClick -= PreviewMouseDoubleClick;

            @this.Panel.path.KeyDown -= Path_KeyDown;
            @this.Panel.cdUp.PreviewMouseLeftButtonDown -= CdUp_PreviewMouseDown;
            @this.Panel.cdRoot.PreviewMouseLeftButtonDown -= CdRoot_PreviewMouseLeftButtonDown;
            @this.Panel.data.PreviewKeyDown -= Data_PreviewKeyDown;
        }

        private static void CdRoot_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var @this = (sender as FrameworkElement).DataContext as ListView2Widget;
            var FS = @this.FileList.FS;

            var path = FS.NoPrefix(FS.RootDirectory);
            if (path.Length == 0)
            {
                MessageBox.Show("directory Root error");
                return;
            }
            @this.LoadDir(path);
            e.Handled = true;
        }

        private static void CdUp_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var @this = (sender as FrameworkElement).DataContext as ListView2Widget;
            var FS = @this.FileList.FS;
            var path = FS.NoPrefix(FS.CurrentDirectory + FS.DirSeparator + "..");
            @this.LoadDir(path);
            e.Handled = true;
        }

        private static void Path_KeyDown(object sender, KeyEventArgs e)
        {
            var @this = (sender as FrameworkElement).DataContext as ListView2Widget;
            if (e.Key == Key.Enter)
            {
                var path = (e.Source as TextEntry).Text.Replace(localFileSystem.FilePrefix, "");
                @this.LoadDir(path);
                e.Handled = true;
            }
        }

        static void PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var @this = sender as ListView2Widget;
            if (@this.SelectEnter(@this.SelectedItem as ListView2ItemWpf))
                e.Handled = true;
        }


        private static void Data_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var @this = (sender as FrameworkElement).Tag as ListView2Widget;
            if (e.Key == Key.Enter && @this.SelectEnter(@this.SelectedItem as ListView2ItemWpf))
                e.Handled = true;
        }


        static void SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var @this = sender as ListView2Widget;
            var list = e.AddedItems;
            ListView2ItemWpf lastItem = null;
            foreach (ListView2ItemWpf item in list)
            {
                @this.Select(item);
                lastItem = item;
            }

            if (lastItem != null)
                @this.SelectLast(lastItem);
        }

    }

}
