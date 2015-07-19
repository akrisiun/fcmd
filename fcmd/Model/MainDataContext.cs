using fcmd.Model;
using pluginner.Widgets;
using System.Collections.Generic;
using System.Diagnostics;

#if WPF
using System.Windows;
using fcmd.View.Xaml;
#else
using Xwt;
using fcmd.View.GTK;
#endif

namespace fcmd
{
    // Window model data

#if !WPF
    public partial class MainWindow : Window, ICommanderWindow<ListView2Canvas>
    {
        // GTK

        public CommanderData WindowData { get; private set; }

        IFileListPanel ICommanderWindow.p1 { get; set; }
        IFileListPanel ICommanderWindow.p2 { get; set; }

#else 

    public partial class MainWindow : Window, ICommanderWindow<ListView2ItemWpf> // ListView2Canvas>
    {

        // WPF
        public CommanderData WindowData {[DebuggerStepThrough] get { return DataContext as CommanderData; } }

        public IFileListPanel p1 {[DebuggerStepThrough] get; set; }
        public IFileListPanel p2 {[DebuggerStepThrough] get; set; }

        public FileListPanelWpf p1Wpf {[DebuggerStepThrough] get { return p1 as FileListPanelWpf; } }
        public FileListPanelWpf p2Wpf {[DebuggerStepThrough] get { return p2 as FileListPanelWpf; } }

        public static string ProductVersion { get { return "v 0.1a"; } }

        public IList<IColumnInfo> LVCols { get; set; }

        /// <summary>The current active panel</summary>
        public FileListPanelWpf ActivePanelWpf { get; set; }
        /// <summary>The current inactive panel</summary>
        public FileListPanelWpf PassivePanelWpf { get; set; }

        public IFileListPanel<ListView2ItemWpf> ActivePanel
        {
            [DebuggerStepThrough] get { return ActivePanelWpf; }
            set { ActivePanelWpf = value as FileListPanelWpf; }
        }
        public IFileListPanel<ListView2ItemWpf> PassivePanel
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
