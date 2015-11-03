/* The File Commander main window
 * The main file manager window
 * (C) The File Commander Team - https://github.com/atauenis/fcmd
 * (C) 2013-14, Alexander Tauenis (atauenis@yandex.ru)
 * (C) 2014, Evgeny Akhtimirov (wilbit@me.com)
 * Contributors should place own signs here.
 * (C) 2015, Andrius Krisiunas (akrisiun@gmail.com)
 */

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;
using System.Text;
using System.IO;
using pluginner.Toolkit;
using fcmd.Menu;
using fcmd.View.GTK;
using fcmd.Model;
using pluginner;
using fcmd.Platform;
using Xwt;
using Xwt.Backends;
using fcmd.View.GTK.Backend;
using pluginner.Widgets;

namespace fcmd
{

    partial class MainWindow : Gtk3WindowFrame, ICommanderWindow<ListView2Canvas>, IControl
    {
        public static string ProductVersion
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        // public Stylist stylist;
        public MenuGtk WindowMenu = new MenuGtk();

        #region Layout panels

        public FileListPanel<ListView2Canvas> p1;
        public FileListPanel<ListView2Canvas> p2;

        public FileListPanelGtk p1Gtk {[DebuggerStepThrough] get { return p1 as FileListPanelGtk; } }
        public FileListPanelGtk p2Gtk {[DebuggerStepThrough] get { return p2 as FileListPanelGtk; } }

        /// <summary>The current active panel</summary>
        public FileListPanel<ListView2Canvas> ActivePanel {[DebuggerStepThrough]get; protected set; }
        /// <summary>The current inactive panel</summary>
        public FileListPanel<ListView2Canvas> PassivePanel {[DebuggerStepThrough] get; protected set; }

        public MainGtkVisual Visual;

        public Xwt.GtkBackend.WindowBackend BackEndGtk  // WindowFrameBackend 
        {[DebuggerStepThrough]  get { return this.BackendHost.Backend as Xwt.GtkBackend.WindowBackend; } }

        public Gtk.VBox MainBox
        {[DebuggerStepThrough] get { return BackEndGtk.MainBox; } }

        object IControl.Content { get { return this.Content; } set { } }
        bool? IControl.Visible { get { return this.Visible; } set { this.Visible = value ?? false; } }
        object IUIDispacher.Dispacher { get { return null; } }
        public bool CheckAccess() { return true; }  // check UI thread

        public class MainGtkVisual
        {
            public Xwt.VBox Layout = new Xwt.VBox();
            public Xwt.HPaned PanelLayout;

            public Xwt.HBox KeyBoardHelp;
            public KeyboardHelpButton[] KeybHelpButtons = new KeyboardHelpButton[11]; //одна лишняя, которая нумбер [0]

            // public List<ListView2.ColumnInfo> LVCols = new List<ListView2.ColumnInfo>();

            public Widget Panel1Content { get { return PanelLayout.Panel1.Content; } }
            public Widget Panel2Content { get { return PanelLayout.Panel2.Content; } }

            public void Init(MainWindow window, string BookmarksStoreXml)
            {
                PanelLayout = new Xwt.HPaned();
                KeyBoardHelp = new Xwt.HBox();

                // build panels
                var pan1 = new FileListPanelGtk(BookmarksStoreXml,
                        fcmd.Properties.Settings.Default.UserTheme, Properties.Settings.Default.InfoBarContent1,
                        Properties.Settings.Default.InfoBarContent2); //Левая, правая где сторона? Улица, улица, ты, брат, пьяна!
                var pan2 = new FileListPanelGtk(BookmarksStoreXml,
                    fcmd.Properties.Settings.Default.UserTheme, Properties.Settings.Default.InfoBarContent1,
                    Properties.Settings.Default.InfoBarContent2);

                PanelLayout.Panel1.Content = pan1;
                PanelLayout.Panel2.Content = pan2;

                window.p1 = pan1;
                window.p2 = pan2;
            }
        }

        #endregion

        public string[] argv;

        public static MainWindow Current { get; set; }

        public MainWindow(string[] argv)
        {
            Current = this;

            this.argv = argv;
            this.Title = "File Commander";
            this.Visual = new MainGtkVisual();

            // this.Icon = Images
            this.PaddingLeft = PaddingRight = PaddingTop = 0;
            this.PaddingBottom = PaddingBottom / 3;

            BindMenu();
            // TranslateMenu(WindowMenu);
            this.MainMenu = WindowMenu;

            LayoutInit();
            if (Visual.PanelLayout != null)
                Visual.PanelLayout.KeyReleased += PanelLayout_KeyReleased;

            // KeyBoardHelpInit();

            Localizator.LocalizationChanged += (o, ea) => Localize();
            Localize();

            // second
            this.Width = fcmd.Properties.Settings.Default.WinWidth;
            this.Height = fcmd.Properties.Settings.Default.WinHeight;

#if DEBUG
            Console.WriteLine(@"DEBUG: MainWindow initialization has been completed.");
#endif
        }


        private void LayoutInit()
        {
            // var Layout = Visual.Layout;
            var mainBox = this.MainBox;

            // this.Content = new Gtk3Box(mainBox);
            var Layout = this.Content as Gtk3Box; //  .BackEndGtk  .BackEndGtk.MainBox   new Gtk3Box(mainBox, Orientation.Vertical);
            // Layout.gtkBox.Parent = this.gtkWindow as Gtk.Window;

            var frame = this.gtkWindow;
            frame.SetDefaultSize(600, 200);

            // fcmd.DemoPanes.Test(this, frame);
            // return;
            Layout.MinWidth = 400;
            Layout.MinHeight = 300;
            //mainBox.PackStart(new Gtk.Entry { Text = "this is Entry" }, false, false, 0);
            // return;

            //// var PanelLayout = Visual.PanelLayout as Xwt.HPaned;
            // Layout.PackStart(PanelLayout, false, Xwt.WidgetPlacement.Fill, Xwt.WidgetPlacement.Fill, 0, 0, 0, 0);

            var text = new TextEntry { Text = "Hello world", MarginLeft = 5, MarginRight = 5, MinHeight = 50, MinWidth = 200 };
            // Layout.PackStart(text, false, false); //  WidgetPlacement.Center);
            // return;

            var PanelLayout = new Gtk3HPaned();
            var hpaned = PanelLayout.XBackend().NativeWidget as Gtk.HPaned;

            // box.Add(native);
            // 
            Layout.PackStart(PanelLayout, false, WidgetPlacement.Fill, WidgetPlacement.Fill, 0, 0, 0, 0);

            var nativeText = text.XBackend().NativeWidget as Gtk.Widget;
            hpaned.Add1(nativeText);

            //var KeyBoardHelp = Visual.KeyBoardHelp;
            //// Layout.PackStart(KeyBoardHelp, false, Xwt.WidgetPlacement.End, Xwt.WidgetPlacement.Fill, 1, 3, 1, 2);


            Gtk.Frame frame1 = new Gtk.Frame() { ShadowType = Gtk.ShadowType.In };
            frame1.SetSizeRequest(60, 60);
            hpaned.Add2(frame1);

            frame1.Add(new Gtk.Entry { Text = "Panel2" });
            return;

            var host = this.BackendHost; // as Xwt.Window.WindowBackendHost;
            var windowEnd = host.Backend as Xwt.GtkBackend.WindowBackend; // .WidgetBackend;
            var gtkWindow = windowEnd.Window as Gtk.Window;

             fcmd.DemoPanes.Test(this, gtkWindow);

            return;
            CheckTheme();

            //load bookmarks
            string BookmarksStore = null;
            if (fcmd.Properties.Settings.Default.BookmarksFile != null && fcmd.Properties.Settings.Default.BookmarksFile.Length > 0)
            {
                BookmarksStore = File.ReadAllText(fcmd.Properties.Settings.Default.BookmarksFile, Encoding.UTF8);
            }

            Visual.Init(this, BookmarksStore);

            var openFileHandler = new pluginner.TypedEvent<string>(Panel_OpenFile);
            p1.OpenFile += openFileHandler;
            p2.OpenFile += openFileHandler;

            // Ankr
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

            try
            {

                p1.FS = new base_plugins.fs.localFileSystem();
                p2.FS = new base_plugins.fs.localFileSystem();

                p1.GotFocus += (o, ea) => SwitchPanel(p1Gtk);
                p2.GotFocus += (o, ea) => SwitchPanel(p2Gtk);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        //public void ShowAll()
        //{
        //    var end = BackendHost.Backend as Xwt.GtkBackend.WindowBackend;
        //    if (end != null)
        //    {
        //        var gtkWindow = end.Window as Gtk.Window;
        //        gtkWindow.ShowAll();
        //    }
        //    else
        //        Show();
        //}

        protected override void OnShown()
        {
            base.OnShown();
            // LoadDir(argv);
#if DEBUG
            Console.WriteLine(@"DEBUG: MainWindow shown.");
#endif
        }

        #region Methods

        private void Localize()
        {
            // TODO
        }

        private void mnuViewWithFilter_Clicked(object sender, EventArgs e)
        {
            string Filter = @"*.*";

            InputBoxGtk ibx = new InputBoxGtk(Localizator.GetString("NameFilterQuestion"), Filter);
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
					);*/
                ActivePanel.LoadDir(
                    ActivePanel.FS.CurrentDirectory,
                    ActivePanel.ShortenPolicy
                    //new Shorten
                    //{
                    //    KB = ActivePanel.CurShortenKB,
                    //    MB = ActivePanel.CurShortenMB,
                    //    GB = ActivePanel.CurShortenGB
                    //}
                    );  //undone!

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
            System.Configuration.Configuration confLR = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.PerUserRoamingAndLocal);
            System.Configuration.Configuration confR = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.PerUserRoaming);
            System.Configuration.Configuration confEXE = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);

            Xwt.Dialog Fcdbg = new Xwt.Dialog();
            Fcdbg.Buttons.Add(Xwt.Command.Close);
            Fcdbg.Buttons[0].Clicked += (o, ea) => { Fcdbg.Hide(); };
            Fcdbg.Title = "FC debug output";
            string txt = "" +
                "===THE FILE COMMANDER, VERSION " + ProductVersion + (Environment.Is64BitProcess ? " 64-BIT" : " 32-BIT") + "===\n" +
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
                "Theme's cascade style sheet file: \"" + fcmd.Properties.Settings.Default.UserTheme + "\"\n\nIf you having some troubles, please report this to https://github.com/atauenis/fcmd bug tracker or http://atauenis.ru/phpBB3/viewtopic.php?f=4&t=211 topic. \nThe End.";
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
            new SettingsWindow().RunCommand(); // .Run();
            ActivePanel.LoadDir(null, null);
            PassivePanel.LoadDir(null, null);
        }

        private void mnuHelpAbout_Clicked(object sender, EventArgs e)
        {
            Xwt.MessageDialog.ShowMessage(DebugInfo.AboutString);
        }

        private void MainWindow_CloseRequested(object sender, Xwt.CloseRequestedEventArgs args)
        {
            //save settings bcos zi form is closing
            Properties.Settings.Default.WinHeight = this.Height;
            Properties.Settings.Default.WinWidth = this.Width;
            if (p1 != null) // .FS
            {
                Properties.Settings.Default.Panel1URL = p1.FS.CurrentDirectory;
                Properties.Settings.Default.Panel2URL = p2.FS.CurrentDirectory;
                Properties.Settings.Default.LastActivePanel = (ActivePanel == p1) ? (byte)1 : (byte)2;
                Properties.Settings.Default.Save();
            }
            Xwt.Application.Exit();
        }

        /// <summary>The entry form's keyboard keypress handler (except commandbar keypresses)</summary>
        private void PanelLayout_KeyReleased(object sender, Xwt.KeyEventArgs e)
        {
#if DEBUG
            var PanelLayout = Visual.PanelLayout as Paned;
            FileListPanel p1 = (PanelLayout.Panel1.Content as FileListPanel);
            FileListPanel p2 = (PanelLayout.Panel2.Content as FileListPanel);

            Console.WriteLine("KEYBOARD DEBUG: " + e.Modifiers + "+" + e.Key + " was pressed. Panels focuses: " + (ActivePanel == p1) + " | " + (ActivePanel == p2));
#endif
            if (e.Key == Xwt.Key.Return) return; //ENTER presses are handled by other event

            // control.Key(this, e);
            fcmd.View.CommanderBackend.Current.KeyEvent(this, e);

#if DEBUG
            Console.WriteLine("KEYBOARD DEBUG: the key wasn't handled");
#endif
            e.Handled = true;
        }

        /// <summary>Switches the active panel</summary>
        /// <param name="NewPanel">The new active panel</param>
        private void SwitchPanel(FileListPanelGtk NewPanel)
        {
            if (NewPanel == ActivePanel) return;
            PassivePanel = ActivePanel;
            ActivePanel = NewPanel;
#if DEBUG
            string PanelName = (NewPanel == p1) ? "LEFT" : "RIGHT";
            Console.WriteLine("FOCUS DEBUG: The " + PanelName + " panel (" + NewPanel.FS.CurrentDirectory + ") got focus");
#endif
            // AssemblyName an = Assembly.GetExecutingAssembly().GetName();
            this.Title = string.Format(
                "{0} - {1}",
                "FC",//System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, //todo: add the ProductName w/o WinForms usage
                ActivePanel.FS.CurrentDirectory
            );

            PassivePanel.UrlBox.BackgroundColor = Xwt.Drawing.Colors.LightBlue;
            ActivePanel.UrlBox.BackgroundColor = Xwt.Drawing.Colors.DodgerBlue;
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
        public void TranslateMenu(Xwt.Menu mnu)
        {
            try
            { //dirty hack...i don't know why, but "if(mnu.Items == null) return;" raises NullReferenceException...
                foreach (Xwt.MenuItem currentMenuItem in mnu.Items)
                {
                    if (currentMenuItem.GetType() != typeof(Xwt.SeparatorMenuItem))
                    { //skip separators
                        currentMenuItem.Label = Localizator.GetString("FC" + currentMenuItem.Tag);
                        TranslateMenu(currentMenuItem.SubMenu);
                    }
                }
            }
            catch { }
        }

        #endregion

        #region Bind

        private void BindMenu()
        {
            this.CloseRequested += MainWindow_CloseRequested;

            WindowMenu.Build();
            WindowMenu.mnuFileView.Clicked += (o, ea) => { PanelLayout_KeyReleased(o, new Xwt.KeyEventArgs(Xwt.Key.F3, Xwt.ModifierKeys.None, false, 0)); };
            WindowMenu.mnuFileEdit.Clicked += (o, ea) => { PanelLayout_KeyReleased(o, new Xwt.KeyEventArgs(Xwt.Key.F4, Xwt.ModifierKeys.None, false, 0)); };
            WindowMenu.mnuFileCopy.Clicked += (o, ea) => { PanelLayout_KeyReleased(o, new Xwt.KeyEventArgs(Xwt.Key.F5, Xwt.ModifierKeys.None, false, 0)); };
            WindowMenu.mnuFileMove.Clicked += (o, ea) => { PanelLayout_KeyReleased(o, new Xwt.KeyEventArgs(Xwt.Key.F6, Xwt.ModifierKeys.None, false, 0)); };
            WindowMenu.mnuFileNewDir.Clicked += (o, ea) => { PanelLayout_KeyReleased(o, new Xwt.KeyEventArgs(Xwt.Key.F7, Xwt.ModifierKeys.None, false, 0)); };
            WindowMenu.mnuFileRemove.Clicked += (o, ea) => { PanelLayout_KeyReleased(o, new Xwt.KeyEventArgs(Xwt.Key.F8, Xwt.ModifierKeys.None, false, 0)); };
            WindowMenu.mnuFileSelectAll.Clicked += (o, ea) => { ActivePanel.ListingView.Select(null); };
            WindowMenu.mnuFileUnselect.Clicked += (o, ea) => { ActivePanel.ListingView.Unselect(); };
            WindowMenu.mnuFileInvertSelection.Clicked += (o, ea) => { ActivePanel.ListingView.InvertSelection(); };
            WindowMenu.mnuFileQuickSelect.Clicked += (o, ea) => { PanelLayout_KeyReleased(o, new Xwt.KeyEventArgs(Xwt.Key.NumPadAdd, Xwt.ModifierKeys.None, false, 0)); };
            WindowMenu.mnuFileQuickUnselect.Clicked += (o, ea) => { PanelLayout_KeyReleased(o, new Xwt.KeyEventArgs(Xwt.Key.NumPadSubtract, Xwt.ModifierKeys.None, false, 0)); };
            WindowMenu.mnuFileExit.Clicked += (o, ea) => { this.Close(); };
            WindowMenu.mnuViewNoFilter.Clicked += (o, ea) => { ActivePanel.LoadDir(); };
            WindowMenu.mnuViewWithFilter.Clicked += mnuViewWithFilter_Clicked;
            WindowMenu.mnuNavigateReload.Clicked += mnuNavigateReload_Clicked;
            WindowMenu.mnuToolsOptions.Clicked += mnuToolsOptions_Clicked;
            WindowMenu.mnuHelpDebug.Clicked += ShowDebugInfo;
            WindowMenu.mnuHelpAbout.Clicked += mnuHelpAbout_Clicked;
        }

        void CheckTheme()
        {
            //check settings
            if (fcmd.Properties.Settings.Default.UserTheme != null)
            {
                if (fcmd.Properties.Settings.Default.UserTheme != "")
                {
                    //if (File.Exists(fcmd.Properties.Settings.Default.UserTheme))
                    //    stylist = new Stylist(fcmd.Properties.Settings.Default.UserTheme);
                    //else
                    //{
                    //    Xwt.MessageDialog.ShowError(Localizator.GetString("ThemeNotFound"), fcmd.Properties.Settings.Default.UserTheme);
                    //    Xwt.Application.Exit();
                    //}
                }
            }
        }

        private void KeyBoardHelpInit()
        {
            var KeybHelpButtons = Visual.KeybHelpButtons;
            var KeyBoardHelp = Visual.KeyBoardHelp;

            //build keyboard help bar
            for (int i = 1; i < 11; i++)
            {
                KeybHelpButtons[i] = new KeyboardHelpButton { CanGetFocus = false };
                KeyBoardHelp.PackStart(KeybHelpButtons[i],
                   true, Xwt.WidgetPlacement.Fill, Xwt.WidgetPlacement.Fill, 0, -6, 0, -3);
            }

            KeybHelpButtons[1].Clicked += (o, ea) =>
            { this.PanelLayout_KeyReleased(this, new Xwt.KeyEventArgs(Xwt.Key.F1, Xwt.ModifierKeys.None, false, 0)); };
            KeybHelpButtons[2].Clicked += (o, ea) =>
            { this.PanelLayout_KeyReleased(this, new Xwt.KeyEventArgs(Xwt.Key.F2, Xwt.ModifierKeys.None, false, 0)); };
            KeybHelpButtons[3].Clicked += (o, ea) =>
            { this.PanelLayout_KeyReleased(this, new Xwt.KeyEventArgs(Xwt.Key.F3, Xwt.ModifierKeys.None, false, 0)); };
            KeybHelpButtons[4].Clicked += (o, ea) =>
            { this.PanelLayout_KeyReleased(this, new Xwt.KeyEventArgs(Xwt.Key.F4, Xwt.ModifierKeys.None, false, 0)); };
            KeybHelpButtons[5].Clicked += (o, ea) =>
            { this.PanelLayout_KeyReleased(this, new Xwt.KeyEventArgs(Xwt.Key.F5, Xwt.ModifierKeys.None, false, 0)); };
            KeybHelpButtons[6].Clicked += (o, ea) =>
            { this.PanelLayout_KeyReleased(this, new Xwt.KeyEventArgs(Xwt.Key.F6, Xwt.ModifierKeys.None, false, 0)); };
            KeybHelpButtons[7].Clicked += (o, ea) =>
            { this.PanelLayout_KeyReleased(this, new Xwt.KeyEventArgs(Xwt.Key.F7, Xwt.ModifierKeys.None, false, 0)); };
            KeybHelpButtons[8].Clicked += (o, ea) =>
            { this.PanelLayout_KeyReleased(this, new Xwt.KeyEventArgs(Xwt.Key.F8, Xwt.ModifierKeys.None, false, 0)); };
            KeybHelpButtons[9].Clicked += (o, ea) =>
            { this.PanelLayout_KeyReleased(this, new Xwt.KeyEventArgs(Xwt.Key.F9, Xwt.ModifierKeys.None, false, 0)); };
            KeybHelpButtons[10].Clicked += (o, ea) =>
            { this.PanelLayout_KeyReleased(this, new Xwt.KeyEventArgs(Xwt.Key.F10, Xwt.ModifierKeys.None, false, 0)); };
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
                    ActivePanel = p1; PassivePanel = p2;
                    if (argv.Length == 1) p1.LoadDir(argv[0], null);
                    break;
                case 2:
                    p2.ListingView.SetFocus();
                    ActivePanel = p2; PassivePanel = p1;
                    if (argv.Length == 1) p2.LoadDir(argv[0], null);
                    break;
                default:
                    p1.ListingView.SetFocus();
                    ActivePanel = p1; PassivePanel = p2;
                    if (argv.Length == 1) p1.LoadDir(argv[0], null);
                    break;
            }
        }

        #endregion
    }

}
