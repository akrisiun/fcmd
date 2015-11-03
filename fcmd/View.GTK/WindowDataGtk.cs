

using System;
using System.Diagnostics;
using fcmd.Controller;
using fcmd.View;
// using fcmd.View.Xaml;
using pluginner.Widgets;
using System.Collections.Generic;
using pluginner;
using fcmd.Menu;
using fcmd.View.GTK;

namespace fcmd.Model
{
    public class WindowDataGtk : CommanderData
    {
        protected PanelSide? activeSide;
        public override PanelSide? ActiveSide { get { return activeSide; } }

        public override ICollection<IFcmdCommand> CommandList { get { return null; } }

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

            public FileListPanelGtk Panel1Gtk { get; protected set; }
            public FileListPanelGtk Panel2Gtk { get; protected set; }

            public static PanelLayoutClass Create(MainWindow w)
            {
                Xwt.HPaned PanelLayout = w.Visual.PanelLayout;
                var panel1Gtk = PanelLayout.Panel1.Content as FileListPanelGtk; // BodyGtk;
                var panel2Gtk = PanelLayout.Panel1.Content as FileListPanelGtk;

                // Initialize Undefined sides
                //w.LeftPanel.PanelData.Initialize(PanelSide.Left);
                //w.RightPanel.PanelData.Initialize(PanelSide.Right);

                //// left and right
                //w.ActivePanelGtk = w.LeftPanel.PanelData as FileListPanelGtk;
                //w.PassivePanelGtk = w.RightPanel.PanelData as FileListPanelGtk;

                //w.p1 = w.ActivePanelGtk;
                //w.p2 = w.PassivePanelGtk;
                (w.WindowData as WindowDataGtk).activeSide = PanelSide.Left;

                return new PanelLayoutClass()
                { Panel1Gtk = panel1Gtk, Panel2Gtk = panel2Gtk };
            }

            //public int Height { get { return (int)Window.Height; } }
            //public int Width { get { return (int)Window.Width; } }

            // public Xwt.HBox KeyBoardHelp = new Xwt.HBox();
            // public KeyboardHelpButton[] KeybHelpButtons = new KeyboardHelpButton[11];//одна лишняя, которая нумбер [0]
            // public Xwt.VBox Layout = new Xwt.VBox();
            // public Xwt.HPaned PanelLayout = new Xwt.HPaned();

        }

        #region Events 

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

        public override void OnSideFocus(PanelSide newSide)
        {
            if (ActiveSide == newSide)
                return;

            // SwitchPanel(newSide == PanelSide.Left ? this.PanelLayout.Panel1 : this.PanelLayout.Panel2);
        }

        public override void OnSelectedItem(IPointedItem item)
        {
        }

        #endregion

        #region Properties

        public MainWindow WindowGtk {[DebuggerStepThrough] get { return Window as MainWindow; } }

        public MenuGtk MainMenu
        { get { return WindowGtk.WindowMenu; } }

        public FileListPanelGtk ActivePanel
        { get { return PanelLayoutGtk.Panel1Gtk; } }
        public FileListPanelGtk PassivePanel
        { get { return PanelLayoutGtk.Panel2Gtk; } }

        public override IPanelLayout PanelLayout { get { return PanelLayoutGtk; } }
        public PanelLayoutClass PanelLayoutGtk { get; protected set; }

        public CommanderStatusBar StatusBar { get; protected set; }

        // public object KeybHelpButtons

        #endregion

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

            // Populate
            //GoRoot.ExpandHorizontal = GoUp.ExpandHorizontal = BookmarksButton.ExpandHorizontal = HistoryButton.ExpandHorizontal = false;
            //GoRoot.Style = GoUp.Style = BookmarksButton.Style = HistoryButton.Style = ButtonStyle.Flat;
            // HistoryButton.Menu = new Xwt.Menu();
            //DefaultColumnSpacing = 0;
            //DefaultRowSpacing = 0;

            //Add(DiskBox, 0, 0, 1, 1, true, false, WidgetPlacement.Fill);
            //Add(GoRoot, 1, 0, 1, 1, false, false, WidgetPlacement.Fill);
            //Add(GoUp, 2, 0, 1, 1, false, false, WidgetPlacement.Fill);
            //Add(UrlBox, 0, 1, 1, 1, true, false, WidgetPlacement.Fill);
            //Add(BookmarksButton, 1, 1, 1, 1, false, false, WidgetPlacement.Start);
            //Add(HistoryButton, 2, 1, 1, 1, false, false, WidgetPlacement.Start);
            //Add(ListingView, 0, 2, 1, 3, false, true); //hexpand will be = 'true' without seeing to this 'false'
            //Add(QuickSearchBox, 0, 3, 1, 3);
            //Add(StatusBar, 0, 4, 1, 3);
            //Add(StatusProgressbar, 0, 5, 1, 3);
            //Add(CLIoutput, 0, 6, 1, 3);
            //Add(CLIprompt, 0, 7, 1, 3);

            //WriteDefaultStatusLabel();
            //CLIprompt.KeyReleased += CLIprompt_KeyReleased;

            //QuickSearchText.GotFocus += (o, ea) => { OnGotFocus(ea); };
            //QuickSearchText.KeyPressed += QuickSearchText_KeyPressed;
            //QuickSearchBox.PackStart(QuickSearchText, true, true);
            //QuickSearchBox.Visible = false;
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