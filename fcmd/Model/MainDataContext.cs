﻿using fcmd.Model;
using pluginner.Widgets;
using System.Collections.Generic;
using System.Diagnostics;

#if WPF
using System.Windows;
using fcmd.View.Xaml;
#else
using Xwt;
#endif

namespace fcmd
{
    // Window model data

    public partial class MainWindow : Window, ICommanderWindow
    {

#if !WPF

        public CommanderData WindowData { get; private set; }

        IFileListPanel ICommanderWindow.p1 { get; set; }
        IFileListPanel ICommanderWindow.p2 { get; set; }

#else
        public CommanderData WindowData {[DebuggerStepThrough] get { return DataContext as CommanderData; } }

        public IFileListPanel p1 { get; set; }
        public IFileListPanel p2 { get; set; }

        public static string ProductVersion { get { return "v 0.1a"; } }

        public IList<IColumnInfo> LVCols { get; set; }

        /// <summary>The current active panel</summary>
        public FileListPanelWpf ActivePanelWpf { get; set; }
        /// <summary>The current inactive panel</summary>
        public FileListPanelWpf PassivePanelWpf { get; set; }

        public IFileListPanel ActivePanel
        {
            [DebuggerStepThrough] get { return ActivePanelWpf; }
            set { ActivePanelWpf = value as FileListPanelWpf; }
        }
        public IFileListPanel PassivePanel
        {
            [DebuggerStepThrough] get { return PassivePanelWpf; }
            set { ActivePanelWpf = value as FileListPanelWpf; }
        }
#endif

        int ICommanderWindow.Width { get { return (int)Width; } set { Width = value; } }
        int ICommanderWindow.Height { get { return (int)Height; } set { Height = value; } }

        public void Init()
        {
            MainWindow main = this;

#if WPF
            var data = new WindowDataWpf
            {
                Window = main
            };
            main.DataContext = data;
            data.DoInit();

            main.Loaded += (s, e)
                => data.DoShown();
#endif
        }
    }

}