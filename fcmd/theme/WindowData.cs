using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Input;
using pluginner.Toolkit;
using pluginner.Widgets;
using fcmd;
using fcmd.theme.ctrl;
using ColorDrawing = System.Drawing.Color;
using fcmd.theme;

namespace fcmd
{
    public interface ICommanderWindow
    {
        IFileListPanel p1 { get; set; }
        IFileListPanel p2 { get; set; }

        IFileListPanel ActivePanel { get; }
        IFileListPanel PassivePanel { get; }
        IList<IColumnInfo> LVCols { get; set; } // = new List<ListView2.ColumnInfo>();
    }

    public partial class MainWindow : ICommanderWindow
    {
        public string ProductVersion = "v 0.1a";

        public IFileListPanel p1 { get; set; }
        public IFileListPanel p2 { get; set; }

        public IList<IColumnInfo> LVCols { get; set; }

        /// <summary>The current active panel</summary>
        public FileListPanelWpf ActivePanelWpf { get; set; }
        /// <summary>The current inactive panel</summary>
        public FileListPanelWpf PassivePanelWpf { get; set; }

        IFileListPanel ICommanderWindow.ActivePanel { get { return ActivePanelWpf; } }   // set { ActivePanelWpf = value as FileListPanelWpf; } }
        IFileListPanel ICommanderWindow.PassivePanel { get { return PassivePanelWpf; } } // set { PassivePanelWpf = value as FileListPanelWpf; } }
    }
}

namespace fcmd.theme
{
    public static class WindowDataStatic
    {
        public static void Init(this MainWindow main)
        {
            main.DataContext = new WindowData
            {
                Window = main
            };
            var data = main.DataContext as WindowData;
            data.Initialize();

            main.Loaded += (s, e) => data.OnShown();
        }

        //InitializeComponent();
        //    EventManager.RegisterClassHandler(typeof(Button), MouseDownEvent, new RoutedEventHandler(OnMouseDown));
        //private void OnMouseDown(object sender, RoutedEventArgs e) {
        //    var element = sender as ContentControl;
        //    if (element != null)
        //        ShowLocation(element);
        //private void ShowLocation(ContentControl element) {
        //    var location = element.PointToScreen(new Point(0, 0));
        //MessageBox.Show(string.Format("{2}'s location is ({0}, {1})", location.X, location.Y, element.Content));
    }


    public class WindowData
    {
        public class PanelLayoutClass
        {
            public PanelWpf Panel1 { get; protected set; }
            public PanelWpf Panel2 { get; protected set; }
        }

        public MainWindow Window { get; set; }
        public MenuWpf MainMenu { get { return Window.Menu; } }

        public FileListPanelWpf ActivePanel { get { return Window.ActivePanelWpf as FileListPanelWpf; } }
        public FileListPanelWpf PassivePanel { get { return Window.PassivePanelWpf as FileListPanelWpf; } }

        public int Height { get { return (int)Window.Height; } }
        public int Width { get { return (int)Window.Width; } }

        // public Xwt.HBox KeyBoardHelp = new Xwt.HBox();
        // public KeyboardHelpButton[] KeybHelpButtons = new KeyboardHelpButton[11];//одна лишняя, которая нумбер [0]
        // public Xwt.VBox Layout = new Xwt.VBox();
        // public Xwt.HPaned PanelLayout = new Xwt.HPaned();
        public PanelLayoutClass PanelLayout { get; protected set; }
        // StatusBar 

        protected theme.ITheme ThemeInstance { get { return theme.Control.Theme; } } // as theme.wpf;

        public WindowData() { }

        public void Initialize()
        {
            // argv = System.Environment.GetCommandLineArgs();
            Window.Title = "File Commander";
            // PanelLayout = new PanelLayout { Panel1 = Window.LeftPanel, Panel2 = Window.RightPanel };

            Window.LVCols = new List<IColumnInfo>();

            // this.MainMenu = WindowMenu;
            //this.PaddingLeft = PaddingRight = PaddingTop = 0;
            //PaddingBottom = PaddingBottom / 3;

            ThemeInstance.Init(Window);

            TranslateMenu(MainMenu);
            BindMenu();
            Window.Menu.itemAbout.Click += (s, e) 
                => mnuHelpAbout_Clicked(s, e);

            LayoutInit();
            KeyBoardHelpInit();

            Localizator.LocalizationChanged += (o, ea) => Localize();
            Localize();

            //apply user's settings
            //window size
            Window.Width = fcmd.Properties.Settings.Default.WinWidth;
            Window.Height = fcmd.Properties.Settings.Default.WinHeight;

#if DEBUG
            Console.WriteLine(@"DEBUG: MainWindow initialization has been completed.");
#endif
        }

        public void OnShown()
        {
            (ThemeInstance as theme.wpf).Shown(Window);
            // LoadDir(argv);
#if DEBUG
            Console.WriteLine(@"DEBUG: MainWindow initialization has been completed.");
#endif
        }

        #region Methods

        private void Localize()
        {
            var themeInstance = theme.Control.Theme as theme.wpf;
            themeInstance.Localize(Window);

        }

        private void mnuViewWithFilter_Clicked(object sender, EventArgs e)
        {
            string Filter = @"*.*";
            var ActivePanel = Window.ActivePanelWpf as FileListPanelWpf;

            InputBox ibx = new InputBox(Localizator.GetString("NameFilterQuestion"), Filter);
            Xwt.CheckBox chkRegExp = new Xwt.CheckBox(Localizator.GetString("NameFilterUseRegExp"));
            ibx.OtherWidgets.Add(chkRegExp, 0, 0);
            if (!ibx.ShowDialog()) return;
            Filter = ibx.Result;
            if (chkRegExp.State == Xwt.CheckBoxState.Off)
            {
                Filter = Filter.Replace(".", @"\.");
                Filter = Filter.Replace("*", ".*");
                Filter = Filter.Replace("?", ".");
            }
            try
            {
                System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(Filter);

                List<pluginner.DirItem> GoodItems = new List<pluginner.DirItem>();
                foreach (pluginner.DirItem di in ActivePanel.FS.DirectoryContent)
                {
                    if (re.IsMatch(di.TextToShow))
                        GoodItems.Add(di);
                }

                /*ActivePanel.LoadDir(
					ActivePanel.FS.CurrentDirectory,
					GoodItems,
					ActivePanel.CurShortenKB,
					ActivePanel.CurShortenMB,
					ActivePanel.CurShortenGB
					);
                */
                ActivePanel.LoadDir(
                    ActivePanel.FS.CurrentDirectory, ActivePanel.CurShorten);
                //new Shorten
                //{
                //    KB = ActivePanel.CurShortenKB,
                //    MB = ActivePanel.CurShortenMB,
                //    GB = ActivePanel.CurShortenGB
                //}
                //);  //undone!

                ActivePanel.StatusBar.Text = string.Format(Localizator.GetString("NameFilterFound"), Filter, GoodItems.Count);
            }
            catch (Exception ex)
            {
                Xwt.MessageDialog.ShowError(Localizator.GetString("NameFilterError"), ex.Message);
            }
        }

        private void mnuNavigateReload_Clicked(object sender, EventArgs e)
        {
            ActivePanel.LoadDir();
        }

        private void Panel_OpenFile(string data)
        {
            if (data.StartsWith("file://") && System.IO.File.Exists(data.Replace("file://", "")))
            {
                try
                {
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.StartInfo.FileName = data.Replace("file://", "");
                    proc.StartInfo.UseShellExecute = true;
                    proc.Start();
                }
                catch (Exception ex)
                {
                    Xwt.MessageDialog.ShowMessage(ex.Message);
                }
            }//todo: else {download to HDD and open locally, if modified, upload back after closing the editor}
        }

        private void ShowDebugInfo(object sender, EventArgs e)
        {
            var confLR = ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.PerUserRoamingAndLocal);
            var confR = ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.PerUserRoaming);
            var confEXE = ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);

            var ActivePanel = Window.ActivePanelWpf;
            var PassivePanel = Window.PassivePanelWpf;
            var p1 = Window.p1;
            var p2 = Window.p2;

            Xwt.Dialog Fcdbg = new Xwt.Dialog();
            Fcdbg.Buttons.Add(Xwt.Command.Close);
            Fcdbg.Buttons[0].Clicked += (o, ea) => { Fcdbg.Hide(); };
            Fcdbg.Title = "FC debug output";
            string txt = "" +
                "===THE FILE COMMANDER, VERSION " + Window.ProductVersion + (Environment.Is64BitProcess ? " 64-BIT" : " 32-BIT") + "===\n" +
                Environment.CommandLine + " @ .NET fw " + Environment.Version + (Environment.Is64BitOperatingSystem ? " 64-bit" : " 32-bit") + " on " + Environment.MachineName + "-" + Environment.OSVersion + " (" + OSVersionEx.Platform + " v" + Environment.OSVersion.Version.Major + "." + Environment.OSVersion.Version.Minor + ")\n" +
                "The current drawing toolkit is " + Xwt.Toolkit.CurrentEngine.GetSafeBackend(this) + "\n" +
                "\nCONFIGuration files:\n---------\n" +
                "Local: " + confLR.FilePath + " (exists? " + b2s(confLR.HasFile) + ")\n" +
                "Roaming: " + confR.FilePath + " (exists? " + b2s(confR.HasFile) + ")\n" +
                "Overall: " + confEXE.FilePath + " (exists? " + b2s(confEXE.HasFile) + ")\n" +
                "\nPanel debug:\n---------\n" +
                "The active panel is: " + ((ActivePanel == p1) ? "LEFT\n" : "RIGHT\n") +
                "The passive panel is: " + ((ActivePanel == p2) ? "LEFT\n" : "RIGHT\n") +
                "They are different? " + b2s(ActivePanel != PassivePanel) + " (should be yes)\n" +
                "The LEFT filesystem: " + p1.FS + " at \"" + p1.FS.CurrentDirectory + "\"\n" +
                "The RIGHT filesystem: " + p2.FS + " at \"" + p2.FS.CurrentDirectory + "\"\n" +
                "Filesystems are same by type? " + b2s(p1.FS.GetType() == p2.FS.GetType()) + ".\n" +
                "Filesystems are identically? " + b2s(p1.FS == p2.FS) + " (should be no).\n" +
                "\nTheme debug:\n---------\n" +
                "Using external theme? " + b2s(!string.IsNullOrEmpty(fcmd.Properties.Settings.Default.UserTheme)) + "\n" +
                "Theme's cascade style sheet file: \"" + fcmd.Properties.Settings.Default.UserTheme
                + "\"\n\nIf you having some troubles, please report this to https://github.com/atauenis/fcmd bug tracker or http://atauenis.ru/phpBB3/viewtopic.php?f=4&t=211 topic. \nThe End.";

            Xwt.RichTextView rtv = new Xwt.RichTextView();
            rtv.LoadText(txt, new Xwt.Formats.PlainTextFormat());
            Xwt.ScrollView sv = new Xwt.ScrollView(rtv);
            Fcdbg.Content = sv;
            Fcdbg.Width = 500;
            Fcdbg.Run();
        }

        // for ShowDebugInfo bool-value displaing purposes
        private string b2s(bool b)
        {
            return (b == true) ? "YES" : "NO";
        }

        private void mnuToolsOptions_Clicked(object sender, EventArgs e)
        {
            new SettingsWindow().Run();
            Window.ActivePanelWpf.LoadDir();
            Window.PassivePanelWpf.LoadDir();
        }

        private void mnuHelpAbout_Clicked(object sender, EventArgs e)
        {
            System.Configuration.Configuration conf = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.PerUserRoamingAndLocal);
            string AboutString = string.Format(
                Localizator.GetString("FileCommanderVer"),
                "File Commander",
                Window.ProductVersion + "-virtualmode",
                "\nhttps://github.com/atauenis/fcmd",
                conf.FilePath,
                Environment.OSVersion,
                Environment.Version + (Environment.Is64BitProcess ? " x86-64" : " x86")
                );
            Xwt.MessageDialog.ShowMessage(AboutString);
        }

        private void MainWindow_CloseRequested(object sender, Xwt.CloseRequestedEventArgs args)
        {
            //save settings bcos zi form is closing
            Properties.Settings.Default.WinHeight = this.Height;
            Properties.Settings.Default.WinWidth = this.Width;
            Properties.Settings.Default.Panel1URL = Window.p1.FS.CurrentDirectory;
            Properties.Settings.Default.Panel2URL = Window.p2.FS.CurrentDirectory;
            Properties.Settings.Default.LastActivePanel = (ActivePanel == Window.p1) ? (byte)1 : (byte)2;
            Properties.Settings.Default.Save();
            Xwt.Application.Exit();
        }

        /// <summary>The entry form's keyboard keypress handler (except commandbar keypresses)</summary>
        private void PanelLayout_KeyReleased(object sender, KeyEventArgs e) // Xwt.KeyEventArgs e)
        {
#if DEBUG
            FileListPanelWpf p1 = (PanelLayout.Panel1.Content as FileListPanelWpf);
            FileListPanelWpf p2 = (PanelLayout.Panel2.Content as FileListPanelWpf);
            // Console.WriteLine("KEYBOARD DEBUG: " + e.Modifiers + "+" + e.Key + " was pressed. Panels focuses: " + (ActivePanel == p1) + " | " + (ActivePanel == p2));
#endif
            if (e.Key == Key.Return) return;//ENTER presses are handled by other event

            var control = theme.Control.Theme as theme.wpf;
            control.KeyEvent(Window, e);

#if DEBUG
            Console.WriteLine("KEYBOARD DEBUG: the key wasn't handled");
#endif
            e.Handled = true;
        }

        /// <summary>Switches the active panel</summary>
        /// <param name="NewPanel">The new active panel</param>
        private void SwitchPanel(FileListPanelWpf NewPanel)
        {
            if (NewPanel == ActivePanel) return;

            Window.PassivePanelWpf = Window.ActivePanelWpf;
            Window.ActivePanelWpf = NewPanel as FileListPanelWpf;
#if DEBUG
            string PanelName = (NewPanel == Window.p1) ? "LEFT" : "RIGHT";
            Console.WriteLine("FOCUS DEBUG: The " + PanelName + " panel (" + NewPanel.FS.CurrentDirectory + ") got focus");
#endif
            // AssemblyName an = Assembly.GetExecutingAssembly().GetName();
            Window.Title = string.Format(
                "{0} - {1}",
                "FC",//System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, //todo: add the ProductName w/o WinForms usage
                NewPanel.FS.CurrentDirectory
            );

            Window.PassivePanelWpf.UrlBox.BackgroundColor = ColorDrawing.LightBlue;
            Window.ActivePanelWpf.UrlBox.BackgroundColor = ColorDrawing.DodgerBlue;
        }

        /// <summary>Converts size display policy (as string) to FLP.SizeDisplayPolicy</summary>
        private SizeDisplayPolicy ConvertSDP(char sizeDisplayPolicy)
        {
            switch (sizeDisplayPolicy.ToString())
            {
                case "0":
                    return SizeDisplayPolicy.DontShorten;
                case "1":
                    return SizeDisplayPolicy.OneNumeral;
                case "2":
                    return SizeDisplayPolicy.TwoNumeral;
                default:
                    return SizeDisplayPolicy.OneNumeral;
            }
        }

        /// <summary>Translates the <paramref name="mnu"/> into the current UI language</summary>
        public void TranslateMenu(object mnu)
        {
            // if (mnu is Xwt.Menu) // mnu)
            //try
            //{   //dirty hack...i don't know why, but "if(mnu.Items == null) return;" raises NullReferenceException...
            //    foreach (Xwt.MenuItem currentMenuItem in mnu.Items)
            //    {
            //        if (currentMenuItem.GetType() != typeof(Xwt.SeparatorMenuItem))
            //        { //skip separators
            //            currentMenuItem.Label = Localizator.GetString("FC" + currentMenuItem.Tag);
            //            TranslateMenu(currentMenuItem.SubMenu);
            //        }
            //    }
            //}
            //catch { }
        }

        #endregion

        #region Bind

        private void BindMenu()
        {
            Menu.MenuWpf.Bind(Window);


            //this.CloseRequested += MainWindow_CloseRequested;
            //PanelLayout.KeyReleased += PanelLayout_KeyReleased;
            //mnuFileView.Clicked += (o, ea) => { PanelLayout_KeyReleased(o, new KeyEventArgs(Key.F3, Xwt.ModifierKeys.None, false, 0)); };
            //mnuFileEdit.Clicked += (o, ea) => { PanelLayout_KeyReleased(o, new KeyEventArgs(Key.F4, Xwt.ModifierKeys.None, false, 0)); };
            //mnuFileCopy.Clicked += (o, ea) => { PanelLayout_KeyReleased(o, new KeyEventArgs(Key.F5, Xwt.ModifierKeys.None, false, 0)); };
            //mnuFileMove.Clicked += (o, ea) => { PanelLayout_KeyReleased(o, new KeyEventArgs(Key.F6, Xwt.ModifierKeys.None, false, 0)); };
            //mnuFileNewDir.Clicked += (o, ea) => { PanelLayout_KeyReleased(o, new KeyEventArgs(Key.F7, Xwt.ModifierKeys.None, false, 0)); };
            //mnuFileRemove.Clicked += (o, ea) => { PanelLayout_KeyReleased(o, new KeyEventArgs(Key.F8, Xwt.ModifierKeys.None, false, 0)); };
            //mnuFileSelectAll.Clicked += (o, ea) => { ActivePanel.ListingView.Select(null); };
            //mnuFileUnselect.Clicked += (o, ea) => { ActivePanel.ListingView.Unselect(); };
            //mnuFileInvertSelection.Clicked += (o, ea) => { ActivePanel.ListingView.InvertSelection(); };
            //mnuFileQuickSelect.Clicked += (o, ea) => { PanelLayout_KeyReleased(o, new KeyEventArgs(Key.NumPadAdd, Xwt.ModifierKeys.None, false, 0)); };
            //mnuFileQuickUnselect.Clicked += (o, ea) => { PanelLayout_KeyReleased(o, new KeyEventArgs(Key.NumPadSubtract, Xwt.ModifierKeys.None, false, 0)); };
            //mnuFileExit.Clicked += (o, ea) => { this.Close(); };
            //mnuViewNoFilter.Clicked += (o, ea) => { ActivePanel.LoadDir(); };
            //mnuViewWithFilter.Clicked += mnuViewWithFilter_Clicked;
            //mnuNavigateReload.Clicked += mnuNavigateReload_Clicked;
            //mnuToolsOptions.Clicked += mnuToolsOptions_Clicked;
            //mnuHelpDebug.Clicked += ShowDebugInfo;
            //mnuHelpAbout.Clicked += mnuHelpAbout_Clicked;
        }

        private void LayoutInit()
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
            //PanelLayout.Panel1.Content = new FileListPanel(BookmarksStore, fcmd.Properties.Settings.Default.UserTheme,
            //    Properties.Settings.Default.InfoBarContent1, Properties.Settings.Default.InfoBarContent2); //Левая, правая где сторона? Улица, улица, ты, брат, пьяна!
            //PanelLayout.Panel2.Content = new FileListPanel(BookmarksStore, fcmd.Properties.Settings.Default.UserTheme, 
            //    Properties.Settings.Default.InfoBarContent1, Properties.Settings.Default.InfoBarContent2);

            Window.p1 = PanelLayout.Panel1.Content as FileListPanelWpf;
            Window.p2 = PanelLayout.Panel2.Content as FileListPanelWpf;

            var openFileHandler = new pluginner.TypedEvent<string>(Panel_OpenFile);

            var p1 = Window.p1 as FileListPanelWpf;
            var p2 = Window.p2 as FileListPanelWpf;

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
            // */

            p1.FS = new base_plugins.fs.localFileSystem();
            p2.FS = new base_plugins.fs.localFileSystem();

            p1.GotFocus += (o, ea) => SwitchPanel(p1);
            p2.GotFocus += (o, ea) => SwitchPanel(p2);
        }

        private void KeyBoardHelpInit()
        {
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

        public void LoadDir(string[] argv)
        {
            //file size display policy
            char[] Policies = fcmd.Properties.Settings.Default.SizeShorteningPolicy.ToCharArray();
            var Shorten = new ShortenPolicies
            {
                KB = ConvertSDP(Policies[0]),
                MB = ConvertSDP(Policies[1]),
                GB = ConvertSDP(Policies[2])
            };

            var p1 = Window.p1 as FileListPanelWpf;
            var p2 = Window.p2 as FileListPanelWpf;

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
            switch (fcmd.Properties.Settings.Default.LastActivePanel)
            {
                case 1:
                    p1.ListingView.SetFocus();
                    Window.ActivePanelWpf = p1;
                    Window.PassivePanelWpf = p2;
                    if (argv.Length == 1) p1.LoadDir(argv[0]);
                    break;
                case 2:
                    p2.ListingView.SetFocus();
                    Window.ActivePanelWpf = p2;
                    Window.PassivePanelWpf = p1;
                    if (argv.Length == 1) p2.LoadDir(argv[0]);
                    break;
                default:
                    p1.ListingView.SetFocus();
                    Window.ActivePanelWpf = p1;
                    Window.PassivePanelWpf = p2;
                    if (argv.Length == 1) p1.LoadDir(argv[0]);
                    break;
            }
        }

        #endregion
    }

}