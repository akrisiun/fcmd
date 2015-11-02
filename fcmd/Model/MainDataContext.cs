using fcmd.Model;
using pluginner.Widgets;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

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
    public partial class MainWindow // : Window, ICommanderWindow<ListView2Canvas>
    {
        public static bool AppLoading { get; set; }
        static MainWindow() { AppLoading = true; }

        // GTK
        public CommanderData WindowData { get; private set; }

        IFileListPanel ICommanderWindow.p1 { get; set; }
        IFileListPanel ICommanderWindow.p2 { get; set; }

#else 

    public partial class MainWindow : Window, ICommanderWindow<ListItemXaml> // ListView2Canvas>
    {

        // WPF
        public CommanderData WindowData {[DebuggerStepThrough] get { return DataContext as CommanderData; } }
        public WindowDataWpf WindowDataWpf {[DebuggerStepThrough] get { return DataContext as WindowDataWpf; } }

        public IFileListPanel p1 {[DebuggerStepThrough] get; set; }
        public IFileListPanel p2 {[DebuggerStepThrough] get; set; }

        public FileListPanelWpf p1Wpf {[DebuggerStepThrough] get { return p1 as FileListPanelWpf; } }
        public FileListPanelWpf p2Wpf {[DebuggerStepThrough] get { return p2 as FileListPanelWpf; } }

        public static string ProductVersion { get { return "v 0.1b"; } }

        public IList<IColumnInfo> LVCols { get; set; }

        /// <summary>The current active panel</summary>
        public FileListPanelWpf ActivePanelWpf { get; set; }
        /// <summary>The current inactive panel</summary>
        public FileListPanelWpf PassivePanelWpf { get; set; }

        public IFileListPanel<ListItemXaml> ActivePanel
        {
            [DebuggerStepThrough] get { return ActivePanelWpf; }
            set { ActivePanelWpf = value as FileListPanelWpf; }
        }
        public IFileListPanel<ListItemXaml> PassivePanel
        {
            [DebuggerStepThrough] get { return PassivePanelWpf; }
            set { ActivePanelWpf = value as FileListPanelWpf; }
        }
#endif

        int ICommanderWindow.Width { get { return (int)Width; } set { Width = value; } }
        int ICommanderWindow.Height { get { return (int)Height; } set { Height = value; } }

#if WPF
        public void Init()
        {
            MainWindow main = this;

            var data = new WindowDataWpf
            {
                Window = main
            };
            main.DataContext = data;
            data.DoInit();

            main.Loaded += Main_Loaded;

        }

        public Task PreloadAsync()
        {
            var data = DataContext as WindowDataWpf;
            return data.Preload();
        }

        void Main_Loaded(object sender, RoutedEventArgs e)
        {
            var data = DataContext as WindowDataWpf;
            data.DoShown();
        }
#endif

    }

}
