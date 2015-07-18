

using System;
using System.Diagnostics;
using fcmd.Controller;
using fcmd.View;
// using fcmd.View.Xaml;
using pluginner.Widgets;
using System.Collections.Generic;
using pluginner;

namespace fcmd.Model
{
    public class WindowDataGtk : CommanderData
    {
        public override PanelSide? ActiveSide
        {
            get
            {
                return null; // TODO
            }
        }

        public class PanelLayoutClass : IPanelLayout
        {
            public IPanel Panel1
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            public IPanel Panel2
            {
                get
                {
                    throw new NotImplementedException();
                }
            }

            //public PanelGtk Panel1 { get; protected set; }
            //public PanelGtk Panel2 { get; protected set; }

            public static PanelLayoutClass Create(MainWindow w)
            {
                // Initialize Undefined sides
                //w.LeftPanel.PanelData.Initialize(PanelSide.Left);
                //w.RightPanel.PanelData.Initialize(PanelSide.Right);

                //// left and right
                //w.ActivePanelGtk = w.LeftPanel.PanelData as FileListPanelGtk;
                //w.PassivePanelGtk = w.RightPanel.PanelData as FileListPanelGtk;

                //w.p1 = w.ActivePanelGtk;
                //w.p2 = w.PassivePanelGtk;
                //w.WindowData.ActiveSide = PanelSide.Left;

                return new PanelLayoutClass();
                // { Panel1 = w.LeftPanel, Panel2 = w.RightPanel };
            }


            //public int Height { get { return (int)Window.Height; } }
            //public int Width { get { return (int)Window.Width; } }

            // public Xwt.HBox KeyBoardHelp = new Xwt.HBox();
            // public KeyboardHelpButton[] KeybHelpButtons = new KeyboardHelpButton[11];//одна лишняя, которая нумбер [0]
            // public Xwt.VBox Layout = new Xwt.VBox();
            // public Xwt.HPaned PanelLayout = new Xwt.HPaned();

        }

        protected override void Initialize()
        {
            // argv = System.Environment.GetCommandLineArgs();
            var window = Window as MainWindow;
            window.Title = "File Commander";

            //window.LVCols = new List<IColumnInfo>();
            Backend.Init(window);
            PanelLayoutGtk = PanelLayoutClass.Create(window);

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
            Console.WriteLine(@"DEBUG: GTK MainWindow initialization has been completed.");
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

        public MainWindow WindowGtk { get { return Window as MainWindow; } }

        public
            object // MenuPanelGtk 
            MainMenu
                { get { return null; } } // (Window as MainWindow).Menu; } }

        public object // FileListPanelGtk 
            ActivePanel
                { get { return null; } } // return WindowGtk.ActivePanelGtk as FileListPanelGtk; } }
        public object // FileListPanelGtk 
            PassivePanel
                { get { return null; } } //  WindowGtk.PassivePanelGtk as FileListPanelGtk; } }

        public override IPanelLayout PanelLayout { get { return PanelLayoutGtk; } }
        public PanelLayoutClass PanelLayoutGtk { get; protected set; }

        public CommanderStatusBar StatusBar { get; protected set; }

        public override object Layout
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override object KeybHelpButtons
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        protected override void SwitchPanel(FileListPanel NewPanel)
        {
            throw new NotImplementedException();
        }

        protected override void KeyBoardHelpInit()
        {
            // TODO: if !this.KeyBar.Visible 

            //build keyboard help bar
            //for (int i = 1; i < 11; i++)
            //{
            //    KeybHelpButtons[i] = new KeyboardHelpButton { CanGetFocus = false };
            //    KeyBoardHelp.PackStart(KeybHelpButtons[i],
            //       true, Xwt.WidgetPlacement.Fill, Xwt.WidgetPlacement.Fill, 0, -6, 0, -3);
            //}

            //KeybHelpButtons[1].Clicked += (o, ea) =>
            //{ this.PanelLayout_KeyReleased(this, new KeyEventArgs(Key.F1, Xwt.ModifierKeys.None, false, 0)); };
            //KeybHelpButtons[2].Clicked += (o, ea) =>
            //{ this.PanelLayout_KeyReleased(this, new KeyEventArgs(Key.F2, Xwt.ModifierKeys.None, false, 0)); };
            //KeybHelpButtons[3].Clicked += (o, ea) =>
            //{ this.PanelLayout_KeyReleased(this, new KeyEventArgs(Key.F3, Xwt.ModifierKeys.None, false, 0)); };
            //KeybHelpButtons[4].Clicked += (o, ea) =>
            //{ this.PanelLayout_KeyReleased(this, new KeyEventArgs(Key.F4, Xwt.ModifierKeys.None, false, 0)); };
            //KeybHelpButtons[5].Clicked += (o, ea) =>
            //{ this.PanelLayout_KeyReleased(this, new KeyEventArgs(Key.F5, Xwt.ModifierKeys.None, false, 0)); };
            //KeybHelpButtons[6].Clicked += (o, ea) =>
            //{ this.PanelLayout_KeyReleased(this, new KeyEventArgs(Key.F6, Xwt.ModifierKeys.None, false, 0)); };
            //KeybHelpButtons[7].Clicked += (o, ea) =>
            //{ this.PanelLayout_KeyReleased(this, new KeyEventArgs(Key.F7, Xwt.ModifierKeys.None, false, 0)); };
            //KeybHelpButtons[8].Clicked += (o, ea) =>
            //{ this.PanelLayout_KeyReleased(this, new KeyEventArgs(Key.F8, Xwt.ModifierKeys.None, false, 0)); };
            //KeybHelpButtons[9].Clicked += (o, ea) =>
            //{ this.PanelLayout_KeyReleased(this, new KeyEventArgs(Key.F9, Xwt.ModifierKeys.None, false, 0)); };
            //KeybHelpButtons[10].Clicked += (o, ea) =>
            //{ this.PanelLayout_KeyReleased(this, new KeyEventArgs(Key.F10, Xwt.ModifierKeys.None, false, 0)); };
            //todo: replace this shit-code with huge using of KeybHelpButtons[n].Tag property (note that it's difficult to be realized due to c# restrictions)
        }

        protected override void LayoutInit()
        {
            //Layout.PackStart(PanelLayout, true, Xwt.WidgetPlacement.Fill, Xwt.WidgetPlacement.Fill, 0, 0, 0, 0);
            //Layout.PackStart(KeyBoardHelp, false, Xwt.WidgetPlacement.End, Xwt.WidgetPlacement.Fill, 1, 3, 1, 2);
            //this.Content = Layout;

            //check settings
            //if (fcmd.Properties.Settings.Default.UserTheme != null)
            //{
            //    if (fcmd.Properties.Settings.Default.UserTheme != "")
            //    {
            //        //if (File.Exists(fcmd.Properties.Settings.Default.UserTheme))
            //        //    stylist = new Stylist(fcmd.Properties.Settings.Default.UserTheme);
            //        //else
            //        //{
            //        //    Xwt.MessageDialog.ShowError(Localizator.GetString("ThemeNotFound"), fcmd.Properties.Settings.Default.UserTheme);
            //        //    Xwt.Application.Exit();
            //        //}
            //    }
            //}

            ////load bookmarks
            //string BookmarksStore = null;
            //if (fcmd.Properties.Settings.Default.BookmarksFile != null && fcmd.Properties.Settings.Default.BookmarksFile.Length > 0)
            //{
            //    BookmarksStore = File.ReadAllText(fcmd.Properties.Settings.Default.BookmarksFile, Encoding.UTF8);
            //}

            ////build panels
            //Window.p1 = PanelLayout.Panel1.DataContext as FileListPanelWpf;
            //Window.p2 = PanelLayout.Panel2.DataContext as FileListPanelWpf;

            //p1.FS = new base_plugins.fs.localFileSystem();
            //p2.FS = new base_plugins.fs.localFileSystem();
            //p1.GotFocus += (o, ea) => SwitchPanel(p1);
            //p2.GotFocus += (o, ea) => SwitchPanel(p2);
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

            // object p1 = null; // WindowGtk.p1 as FileListPanelGtk;
                              //if (p1 == null)
            return;

            //object p2 = null; // WindowGtk.p2 as FileListPanelGtk;

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