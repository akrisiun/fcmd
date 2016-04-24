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
    public static class BindCommands
    {
        public static void Loaded(this MainWindow main, FooterCmd panel)
        {
            CommandList cmd = panel.CmdList;

            panel.IsEnabled = true;
            panel.f1.IsEnabled = false;
            panel.f2.IsEnabled = false;
            panel.f3.IsEnabled = false;
            var f4 = panel.f4;
            f4.Content = "F4 - Edit";
            f4.Command = cmd.cmdEdit as EdCommand;

            var f7 = panel.f7;
            f7.Content = "F7 - MkDir";
            cmd.cmdMkDir.Enabled = true;
            f7.Command = cmd.cmdMkDir;

            var f5 = panel.f5;
            f5.Content = "F5 - Copy";
            cmd.cmdCp.Enabled = true;
            f5.Command = cmd.cmdCp;
            var f6 = panel.f6;
            f6.Content = "F6 - Move";
            f6.Command = cmd.cmdMv;
            cmd.cmdMv.Enabled = true;

            var f8 = panel.f8;
            f8.Content = "F8 - Del";
            f8.Command = cmd.cmdRm;
            cmd.cmdRm.Enabled = false;

            panel.f9.IsEnabled = false;
            panel.f10.IsEnabled = false;
            panel.f11.IsEnabled = false;
            panel.f12.IsEnabled = false;
        }

        public static void PanelCmdUpdate(PanelWpf filePanel)
        {
            // combo.Items.Clear();
        }
         
    }
}
