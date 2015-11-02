using fcmd.FileList;
using fcmd.Model;
using fcmd.View.ctrl;
using pluginner.Toolkit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Controls;

namespace fcmd.View.Xaml.cmd
{
    public static class DiskBoxCombo
    {
        public static void PanelDirCombo(ComboWidget combo, PanelWpf filePanel, PanelSide side)
        {
            var bookList = BookmarkTools.AddSysDrives().GetEnumerator();
            var drives = new Collection<string>();

            while (bookList.MoveNext())
            {
                var item = bookList.Current;

                drives.Add(item.title);
            }

            drives.Add("http://");
            drives.Add("ftp://");

            combo.ItemsSource = drives;
            combo.SelectionChanged += (s, e) => SelectedDrive(combo, e, filePanel, side);
        }

        public static void SelectedDrive(ComboBox box, SelectionChangedEventArgs e,
            PanelWpf filePanel, PanelSide side)
        {
            if (e.AddedItems == null || e.AddedItems.Count == 0)
                return;

            e.Handled = true;
            var item = e.AddedItems[0];
            var text = item.ToString();
            if (text.Contains("//"))
            {
                filePanel.path.Text = text;
            }
            else
            {
                var dir = "file://" + text;
                filePanel.path.Text = dir;

                var data = filePanel.PanelDataWpf;
                data.LoadDir(dir);
            }

            filePanel.Update();
        }

    }
}
