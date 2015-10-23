using System;
using System.Windows.Input;
using fcmd.Platform;
using System.Windows.Controls;
using fcmd.Controller;
using System.Collections.ObjectModel;
using System.IO;
using fcmd.Model;

namespace fcmd.View.Xaml
{
    public static class Bind
    {
        public static void PanelCmd(this PanelCmd panel)
        {
            panel.cmd.KeyDown += Cmd_KeyDown;
        }

        public static void PanelDirCombo(ComboBox combo, PanelWpf filePanel, PanelSide side)
        {
            var drives = new Collection<string>();
            drives.Add("C:");
            if (Directory.Exists("D:"))
                drives.Add("D:");
            if (Directory.Exists("E:"))
                drives.Add("E:");

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
            var dir = "file://" + item.ToString();

            filePanel.path.Text = dir;

            var data = filePanel.PanelDataWpf;
            data.LoadDir(dir);

            filePanel.Update();
        }

        public static void PanelDirUpdate(ComboBox combo, PanelWpf filePanel, PanelSide side)
        {
            // combo.Items.Clear();
        }

        static void Cmd_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var box = sender as ComboBox;
                string command = box.Text;
                if (string.IsNullOrWhiteSpace(command))
                    return;

                e.Handled = true;
                ProcessCmd.Command(command, box);
            }
        }
    }
}
