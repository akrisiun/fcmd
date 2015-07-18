using System;
using System.Diagnostics;
using System.Collections.Generic;

using fcmd.Controller;
using fcmd.View.Xaml;
using pluginner;
using pluginner.Widgets;
using System.Threading.Tasks;
using System.Windows.Threading;
using fcmd.View;

namespace fcmd.Model
{
    public class WindowDataWpf : CommanderData
    {
        public override PanelSide? ActiveSide
        {
            get
            {
                return PanelLayout == null || !PanelLayout.Panel1.IsActive.HasValue ? PanelSide.Undefined
                     : PanelLayout.Panel1.IsActive.Value ? PanelSide.Left : PanelSide.Right;
            }
            //set
            //{
            //    bool isLeft = (value == PanelSide.Left);
            //    PanelLayout.Panel1.IsActive = isLeft;
            //    PanelLayout.Panel2.IsActive = !isLeft;
            //}
        }

        public class PanelLayoutClass : IPanelLayout
        {
            public IPanel Panel1 {[DebuggerStepThrough] get { return wpfPanel1; } }
            public IPanel Panel2 {[DebuggerStepThrough] get { return wpfPanel2; } }

            public PanelWpf wpfPanel1 { get; protected set; }
            public PanelWpf wpfPanel2 { get; protected set; }

            public static PanelLayoutClass Create(MainWindow w)
            {
                // Initialize Undefined sides
                w.LeftPanel.PanelDataWpf.Initialize(PanelSide.Left);
                w.RightPanel.PanelDataWpf.Initialize(PanelSide.Right);

                // left and right
                w.ActivePanelWpf = w.LeftPanel.PanelDataWpf as FileListPanelWpf;
                w.PassivePanelWpf = w.RightPanel.PanelDataWpf as FileListPanelWpf;

                w.p1 = w.ActivePanelWpf;
                w.p2 = w.PassivePanelWpf;
                w.RightPanel.Side = PanelSide.Right;

                return new PanelLayoutClass { wpfPanel1 = w.LeftPanel, wpfPanel2 = w.RightPanel };
            }

        }

        /// <summary>Switches the active panel</summary>
        protected override void SwitchPanel(FileListPanel NewPanel)
        {
            if (NewPanel == Window.ActivePanel) return;

            Window.PassivePanelWpf = WindowWpf.ActivePanelWpf;
            Window.ActivePanelWpf = NewPanel as FileListPanelWpf;

            bool isLeft = (NewPanel == Window.p1);
#if DEBUG
            string PanelName = isLeft ? "LEFT" : "RIGHT";
            // Console.WriteLine("FOCUS DEBUG: The " + PanelName + " panel (" + NewPanel.FS.CurrentDirectory + ") got focus");
#endif

            var passive = Window.PassivePanelWpf.Parent as PanelWpf;
            passive.IsActive = false;
  
            // AssemblyName an = Assembly.GetExecutingAssembly().GetName();
            Window.Title = string.Format(
                "{0} - {1}",
                "FC",//System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, //todo: add the ProductName w/o WinForms usage
                NewPanel.FS.CurrentDirectory
            );
        }

        protected override void LayoutInit()
        {
            // base.LayoutInit();
            var p1 = Window.p1 as FileListPanelWpf;
            var p2 = Window.p2 as FileListPanelWpf;

            var openFileHandler = new pluginner.TypedEvent<string>(Panel_OpenFile);
            p1.OpenFile += openFileHandler;
            p2.OpenFile += openFileHandler;

            // Ankr
            var LVCols = Window.LVCols;
            //LVCols.Add(new ListView2.ColumnInfo { Title = "", Tag = "Icon", Width = 16, Visible = true });
            //LVCols.Add(new ListView2.ColumnInfo { Title = "URL", Tag = "Path", Width = 0, Visible = false });
            //LVCols.Add(new ListView2.ColumnInfo
            //{ Title = Localizator.GetString("FName"), Tag = "FName", Width = 100, Visible = true });
            //LVCols.Add(new ListView2.ColumnInfo
            //{ Title = Localizator.GetString("FSize"), Tag = "FSize", Width = 50, Visible = true });
            //LVCols.Add(new ListView2.ColumnInfo
            //{ Title = Localizator.GetString("FDate"), Tag = "FDate", Width = 50, Visible = true });
            //LVCols.Add(new ListView2.ColumnInfo
            //{ Title = "Directory item info", Tag = "DirItem", Width = 0, Visible = false });

            p1.FS = new base_plugins.fs.localFileSystem();
            p2.FS = new base_plugins.fs.localFileSystem();
            p1.GotFocus += (o, ea) => SwitchPanel(p1);
            p2.GotFocus += (o, ea) => SwitchPanel(p2);
        }

        protected override void Initialize()
        {
            // argv = System.Environment.GetCommandLineArgs();
            var window = Window as MainWindow;
            window.Title = "File Commander";

            window.LVCols = new List<IColumnInfo>();

            Backend.Init(window);
            _panelLayout = PanelLayoutClass.Create(window);

            TranslateMenu(MainMenu);
            BindMenu();
            MainMenu.itemAbout.Click += (s, e)
                     => this.mnuHelpAbout_Clicked(s, e);

            LayoutInit();
            KeyBoardHelpInit();

            Localizator.LocalizationChanged += (o, ea)
                => Localize();
            Localize();

            //apply user's settings
            //window size
            Window.Width = Convert.ToInt16(fcmd.Properties.Settings.Default.WinWidth);
            Window.Height = Convert.ToInt16(fcmd.Properties.Settings.Default.WinHeight);

#if DEBUG
            Console.WriteLine(@"DEBUG: MainWindow initialization has been completed.");
#endif
        }

        protected override void OnShown()
        {
            var panel1 = this.PanelLayout.Panel1 as PanelWpf;
            var panel2 = this.PanelLayout.Panel2 as PanelWpf;
            panel2.IsActive = false;
            panel1.IsActive = true;

            var visual = this.Backend as WpfBackend;
            visual.Shown(panel1, panel2);

#if DEBUG
            var active = ActiveSide; // = PanelSide.Left;
            Console.WriteLine(@"DEBUG: MainWindow initialization has been completed. Side=" + active.ToString());
#endif
        }

        public MainWindow WindowWpf { get { return Window as MainWindow; } }
        public MenuPanelWpf MainMenu { get { return (Window as MainWindow).Menu; } }

        public FileListPanelWpf ActivePanel { get { return WindowWpf.ActivePanelWpf as FileListPanelWpf; } }
        public FileListPanelWpf PassivePanel { get { return WindowWpf.PassivePanelWpf as FileListPanelWpf; } }

        protected PanelLayoutClass _panelLayout { get; set; }
        public override IPanelLayout PanelLayout { get { return _panelLayout; } }     // was: Xwt.HPaned();

        // public Xwt.HBox KeyBoardHelp = new Xwt.HBox();
        public override object KeybHelpButtons { get { return null; } }
        // KeyboardHelpButton[] KeybHelpButtons = new KeyboardHelpButton[11];//одна лишняя, которая нумбер [0]

        public override object Layout { get { return Window.Content; } }    // was: Xwt.VBox 

        public CommanderStatusBar StatusBar { get; protected set; }

        public void LoadDirAsync(string[] argv, TaskScheduler scheduler)
        {
            LoadDir(argv);
            // scheduler.D
        }

        public override void LoadDir(string[] argv)
        {
            //file size display policy
            char[] Policies = fcmd.Properties.Settings.Default.SizeShorteningPolicy.ToCharArray();
            var Shorten = new ShortenPolicies
            {
                KB = ConvertSDP(Policies[0]),
                MB = ConvertSDP(Policies[1]),
                GB = ConvertSDP(Policies[2])
            };

            var p1 = WindowWpf.p1 as FileListPanelWpf;
            if (p1 == null)
                return;

            var p2 = WindowWpf.p2 as FileListPanelWpf;

            //load last directory or the current directory if the last directory hasn't remembered
            if (Properties.Settings.Default.Panel1URL.Length != 0)
            {
                p1.LoadDir(Properties.Settings.Default.Panel1URL, Shorten);
            }
            else
            {
                p1.LoadDir("file://" + System.IO.Directory.GetCurrentDirectory(), Shorten);
                // ConvertSDP(Policies[0]), ConvertSDP(Policies[1]), ConvertSDP(Policies[2]));
            }

            if (Properties.Settings.Default.Panel2URL.Length != 0)
            {
                p2.LoadDir(Properties.Settings.Default.Panel2URL,
                    Shorten); // ConvertSDP(Policies[0]), ConvertSDP(Policies[1]), ConvertSDP(Policies[2]));
            }
            else
            {
                p2.LoadDir("file://" + System.IO.Directory.GetCurrentDirectory(),
                    Shorten); // ConvertSDP(Policies[0]), ConvertSDP(Policies[1]), ConvertSDP(Policies[2]));
            }

            //default panel
            var window = Window;
            bool isAccess = (window as DispatcherObject).CheckAccess();

            switch (fcmd.Properties.Settings.Default.LastActivePanel)
            {
                case 1:
                    if (isAccess)
                        p1.ListingView.SetFocus();
                    window.ActivePanel = p1;
                    window.PassivePanel = p2;
                    if (argv.Length == 1 && !argv[0].EndsWith(".exe"))
                        p1.LoadDir(argv[0]);
                    break;
                case 2:
                    if (isAccess)
                        p2.ListingView.SetFocus();
                    window.ActivePanel = p2;
                    window.PassivePanel = p1;
                    if (argv.Length == 1) p2.LoadDir(argv[0]);
                    break;
                default:
                    if (isAccess)
                        p1.ListingView.SetFocus();
                    window.ActivePanel = p1;
                    window.PassivePanel = p2;
                    if (argv.Length == 1) p1.LoadDir(argv[0]);
                    break;
            }
        }

        protected override void KeyBoardHelpInit() { }
    }

}