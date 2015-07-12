using fcmd.View.ctrl;
using pluginner.Widgets;
using System.Windows.Input;

namespace fcmd.Model
{
    public static class GridEvents
    {
        public static void BindGridEvents(this ListView2Widget @this)
        {
            @this.SelectionChanged += (s, e) =>
            {
                var list = e.AddedItems;
                foreach (ListView2ItemWpf item in list)
                    @this.Select(item);
            };

            @this.PreviewMouseDoubleClick += (s, e) =>
            {
                if (@this.SelectEnter(@this.SelectedItem as ListView2ItemWpf))
                    e.Handled = true;
            };

            @this.Panel.path.KeyDown += (s, e) =>
            {
                if (e.Key == Key.Enter)
                {
                    var path = (e.Source as TextEntry).Text.Replace(ListView2Widget.fileProcol, "");
                    @this.LoadDir(path);
                    e.Handled = true;
                }
            };

            @this.Panel.cdUp.PreviewMouseDown += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    var FS = @this.FileList.FS;
                    var path = FS.CurrentDirectory + FS.DirSeparator + "..";
                    @this.LoadDir(path);
                    e.Handled = true;
                }
            };
        }


    }

}
