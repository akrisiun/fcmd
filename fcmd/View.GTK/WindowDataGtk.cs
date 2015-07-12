

using fcmd.Controller;
using fcmd.View;
// using fcmd.View.Xaml;
using pluginner.Widgets;
using System;
using System.Collections.Generic;

namespace fcmd.Model
{
    public class WindowDataGtk : CommanderData
    {
        public class PanelLayoutClass
        {
            //public PanelWpf Panel1 { get; protected set; }
            //public PanelWpf Panel2 { get; protected set; }

            public static PanelLayoutClass Create(MainWindow w)
            {
                // Initialize Undefined sides
                //w.LeftPanel.PanelData.Initialize(PanelSide.Left);
                //w.RightPanel.PanelData.Initialize(PanelSide.Right);

                //// left and right
                //w.ActivePanelWpf = w.LeftPanel.PanelData as FileListPanelWpf;
                //w.PassivePanelWpf = w.RightPanel.PanelData as FileListPanelWpf;

                //w.p1 = w.ActivePanelWpf;
                //w.p2 = w.PassivePanelWpf;
                //w.WindowData.ActiveSide = PanelSide.Left;

                return new PanelLayoutClass();
                // { Panel1 = w.LeftPanel, Panel2 = w.RightPanel };
            }
        }

        protected override void Initialize()
        {
            // argv = System.Environment.GetCommandLineArgs();
            var window = Window as MainWindow;
            window.Title = "File Commander";

            //window.LVCols = new List<IColumnInfo>();
            Backend.Init(window);
            PanelLayout = PanelLayoutClass.Create(window);

            TranslateMenu(MainMenu);
            BindMenu();
            //MainMenu.itemAbout.Click += (s, e)
            //         => this.mnuHelpAbout_Clicked(s, e);

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
            var visual = this.Backend;
            visual.Shown();
#if DEBUG
            Console.WriteLine(@"DEBUG: MainWindow initialization has been completed.");
#endif
        }

        public MainWindow WindowWpf { get { return Window as MainWindow; } }
        public
            object // MenuPanelWpf 
            MainMenu
                { get { return null; } } // (Window as MainWindow).Menu; } }

        public object // FileListPanelWpf 
            ActivePanel
                { get { return null; } } // return WindowWpf.ActivePanelWpf as FileListPanelWpf; } }
        public object // FileListPanelWpf 
            PassivePanel
                { get { return null; } } //  WindowWpf.PassivePanelWpf as FileListPanelWpf; } }

        //public int Height { get { return (int)Window.Height; } }
        //public int Width { get { return (int)Window.Width; } }

        // public Xwt.HBox KeyBoardHelp = new Xwt.HBox();
        // public KeyboardHelpButton[] KeybHelpButtons = new KeyboardHelpButton[11];//одна лишняя, которая нумбер [0]
        // public Xwt.VBox Layout = new Xwt.VBox();
        // public Xwt.HPaned PanelLayout = new Xwt.HPaned();

        public PanelLayoutClass PanelLayout { get; protected set; }

        public CommanderStatusBar StatusBar { get; protected set; }

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

            object p1 = null; // WindowWpf.p1 as FileListPanelWpf;
            //if (p1 == null)
                return;

            //object p2 = null; // WindowWpf.p2 as FileListPanelWpf;

            ////load last directory or the current directory if the last directory hasn't remembered
            //if (Properties.Settings.Default.Panel1URL.Length != 0)
            //{
            //    p1.LoadDir(Properties.Settings.Default.Panel1URL, Shorten);
            //}
            //else
            //{
            //    p1.LoadDir("file://" + System.IO.Directory.GetCurrentDirectory(), Shorten);
            //    // ConvertSDP(Policies[0]), ConvertSDP(Policies[1]), ConvertSDP(Policies[2]));
            //}

            //if (Properties.Settings.Default.Panel2URL.Length != 0)
            //{
            //    p2.LoadDir(Properties.Settings.Default.Panel2URL,
            //        Shorten); // ConvertSDP(Policies[0]), ConvertSDP(Policies[1]), ConvertSDP(Policies[2]));
            //}
            //else
            //{
            //    p2.LoadDir("file://" + System.IO.Directory.GetCurrentDirectory(),
            //        Shorten); // ConvertSDP(Policies[0]), ConvertSDP(Policies[1]), ConvertSDP(Policies[2]));
            //}

            ////default panel
            //var window = Window;
            //switch (fcmd.Properties.Settings.Default.LastActivePanel)
            //{
            //    case 1:
            //        p1.ListingView.SetFocus();
            //        window.ActivePanel = p1;
            //        window.PassivePanel = p2;
            //        if (argv.Length == 1 && !argv[0].EndsWith(".exe"))
            //            p1.LoadDir(argv[0]);
            //        break;
            //    case 2:
            //        p2.ListingView.SetFocus();
            //        window.ActivePanel = p2;
            //        window.PassivePanel = p1;
            //        if (argv.Length == 1) p2.LoadDir(argv[0]);
            //        break;
            //    default:
            //        p1.ListingView.SetFocus();
            //        window.ActivePanel = p1;
            //        window.PassivePanel = p2;
            //        if (argv.Length == 1) p1.LoadDir(argv[0]);
            //        break;
            //}
        }

    }

}