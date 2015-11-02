using System;
using System.Windows.Input;
using fcmd.Platform;
using System.Windows.Controls;
using fcmd.Controller;
using System.Collections.ObjectModel;
using System.IO;
using fcmd.Model;
using fcmd.FileList;

namespace fcmd.View.Xaml
{
    public static class BindPanel
    {
        public static void PanelCmd(this PanelCmd panel)
        {
            panel.cmd.KeyDown += Cmd_KeyDown;
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
