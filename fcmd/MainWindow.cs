/* The File Commander main window
 * The main file manager window
 * (C) The File Commander Team - https://github.com/atauenis/fcmd
 * (C) 2013-14, Alexander Tauenis (atauenis@yandex.ru)
 * (C) 2014, Evgeny Akhtimirov (wilbit@me.com)
 * Contributors should place own signs here.
 */

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.IO;
using pluginner.Toolkit;
using pluginner.Widgets;
using fcmd.Menu;

namespace fcmd
{
    partial class MainWindow : Xwt.Window
    {
        static string ProductVersion
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        #region Properties

        public Stylist stylist;
        public FcmdMenu WindowMenu = new Menu.FcmdMenu();

        public MenuItemWithKey mnuFile = new MenuItemWithKey { Tag = "mnuFile" };
        public MenuItemWithKey mnuFileUserMenu = new MenuItemWithKey { Tag = "mnuFileUserMenu" };
        public MenuItemWithKey mnuFileView = new MenuItemWithKey { Tag = "mnuFileView" };
        public MenuItemWithKey mnuFileEdit = new MenuItemWithKey { Tag = "mnuFileEdit" };
        public MenuItemWithKey mnuFileCompare = new MenuItemWithKey { Tag = "mnuFileCompare" };
        public MenuItemWithKey mnuFileCopy = new MenuItemWithKey { Tag = "mnuFileCopy", Key="F5", Label = "Copy F5" };
        public MenuItemWithKey mnuFileMove = new MenuItemWithKey { Tag = "mnuFileMove" };
        public MenuItemWithKey mnuFileNewDir = new MenuItemWithKey { Tag = "mnuFileNewDir" };
        public MenuItemWithKey mnuFileRemove = new MenuItemWithKey { Tag = "mnuFileRemove" };
        public MenuItemWithKey mnuFileAtributes = new MenuItemWithKey { Tag = "mnuFileAtributes" };
        public MenuItemWithKey mnuFileQuickSelect = new MenuItemWithKey { Tag = "mnuFileQuickSelect" };
        public MenuItemWithKey mnuFileQuickUnselect = new MenuItemWithKey { Tag = "mnuFileQuickUnselect" };
        public MenuItemWithKey mnuFileSelectAll = new MenuItemWithKey { Tag = "mnuFileSelectAll" };
        public MenuItemWithKey mnuFileUnselect = new MenuItemWithKey { Tag = "mnuFileUnselect" };
        public MenuItemWithKey mnuFileInvertSelection = new MenuItemWithKey { Tag = "mnuFileInvertSelection" };
        public MenuItemWithKey mnuFileExit = new MenuItemWithKey { Tag = "mnuFileExit" };

        public MenuItemWithKey mnuView = new MenuItemWithKey { Tag = "mnuView" };
        public MenuItemWithKey mnuViewShort = new MenuItemWithKey { Tag = "mnuViewShort" };
        public MenuItemWithKey mnuViewDetails = new MenuItemWithKey { Tag = "mnuViewDetails" };
        public MenuItemWithKey mnuViewIcons = new MenuItemWithKey { Tag = "mnuViewIcons" };
        public MenuItemWithKey mnuViewThumbs = new MenuItemWithKey { Tag = "mnuViewThumbs" };
        public MenuItemWithKey mnuViewQuickView = new MenuItemWithKey { Tag = "mnuViewQuickView" };
        public MenuItemWithKey mnuViewTree = new MenuItemWithKey { Tag = "mnuViewTree" };
        public MenuItemWithKey mnuViewPCPCconnect = new MenuItemWithKey { Tag = "mnuViewPCPCconnect" };
        public MenuItemWithKey mnuViewPCNETPCconnect = new MenuItemWithKey { Tag = "mnuViewPCNETPCconnect" };
        public MenuItemWithKey mnuViewByName = new MenuItemWithKey { Tag = "mnuViewByName" };
        public MenuItemWithKey mnuViewByType = new MenuItemWithKey { Tag = "mnuViewByType" };
        public MenuItemWithKey mnuViewByDate = new MenuItemWithKey { Tag = "mnuViewByDate" };
        public MenuItemWithKey mnuViewBySize = new MenuItemWithKey { Tag = "mnuViewBySize" };
        public MenuItemWithKey mnuViewNoFilter = new MenuItemWithKey { Tag = "mnuViewNoFilter" };
        public MenuItemWithKey mnuViewWithFilter = new MenuItemWithKey { Tag = "mnuViewWithFilter" };

        public MenuItemWithKey mnuNavigate = new MenuItemWithKey { Tag = "mnuNav" };
        public MenuItemWithKey mnuNavigateTree = new MenuItemWithKey { Tag = "mnuNavigateTree" };
        public MenuItemWithKey mnuNavigateFind = new MenuItemWithKey { Tag = "mnuNavigateFind" };
        public MenuItemWithKey mnuNavigateHistory = new MenuItemWithKey { Tag = "mnuNavigateHistory" };
        public MenuItemWithKey mnuNavigateReload = new MenuItemWithKey { Tag = "mnuNavigateReload" };

        public MenuItemWithKey mnuTools = new MenuItemWithKey { Tag = "mnuTools" };
        public MenuItemWithKey mnuToolsOptions = new MenuItemWithKey { Tag = "mnuToolsOptions" };
        public MenuItemWithKey mnuToolsPluginManager = new MenuItemWithKey { Tag = "mnuToolsPluginManager" };
        public MenuItemWithKey mnuToolsEditUserMenu = new MenuItemWithKey { Tag = "mnuToolsEditUserMenu" };
        public MenuItemWithKey mnuToolsKeychains = new MenuItemWithKey { Tag = "mnuToolsKeychains" };
        public MenuItemWithKey mnuToolsConfigEdit = new MenuItemWithKey { Tag = "mnuToolsConfigEdit" };
        public Xwt.CheckBoxMenuItem mnuViewKeybrdHelp = new Xwt.CheckBoxMenuItem { Tag = "mnuViewKeybrdHelp" };
        public Xwt.CheckBoxMenuItem mnuViewInfobar = new Xwt.CheckBoxMenuItem { Tag = "mnuViewInfobar" };
        public Xwt.CheckBoxMenuItem mnuViewDiskButtons = new Xwt.CheckBoxMenuItem { Tag = "mnuViewDiskButtons" };
        public MenuItemWithKey mnuToolsDiskLabel = new MenuItemWithKey { Tag = "mnuToolsDiskLabel" };
        public MenuItemWithKey mnuToolsFormat = new MenuItemWithKey { Tag = "mnuToolsFormat" };
        public MenuItemWithKey mnuToolsSysInfo = new MenuItemWithKey { Tag = "mnuToolsSysInfo" };

        public MenuItemWithKey mnuHelp = new MenuItemWithKey { Tag = "mnuHelp" };
        public MenuItemWithKey mnuHelpHelpMe = new MenuItemWithKey { Tag = "mnuHelpHelpMe" };
        public MenuItemWithKey mnuHelpDebug = new MenuItemWithKey { Tag = "mnuHelpDebug" };
        public MenuItemWithKey mnuHelpAbout = new MenuItemWithKey { Tag = "mnuHelpAbout" };

        public Xwt.VBox Layout = new Xwt.VBox();
        public Xwt.HPaned PanelLayout = new Xwt.HPaned();

        public FileListPanel p1;
        public FileListPanel p2;

        public List<ListView2.ColumnInfo> LVCols = new List<ListView2.ColumnInfo>();

        /// <summary>The current active panel</summary>
        public FileListPanel ActivePanel;
        /// <summary>The current inactive panel</summary>
        public FileListPanel PassivePanel;

        public Xwt.HBox KeyBoardHelp = new Xwt.HBox();
        public KeyboardHelpButton[] KeybHelpButtons = new KeyboardHelpButton[11];//одна лишняя, которая нумбер [0]

        #endregion

        public MainWindow(string[] argv)
        {
            this.Title = "File Commander";
            this.MainMenu = WindowMenu;
            this.PaddingLeft = PaddingRight = PaddingTop = 0;
            PaddingBottom = PaddingBottom / 3;

            var themeInstance = theme.Control.Theme as theme.wpf;
            themeInstance.Init(this);

            TranslateMenu(MainMenu);
            BindMenu();
            LayoutInit();
            KeyBoardHelpInit();

            Localizator.LocalizationChanged += (o, ea) => Localize();
            Localize();

            //apply user's settings
            //window size
            this.Width = fcmd.Properties.Settings.Default.WinWidth;
            this.Height = fcmd.Properties.Settings.Default.WinHeight;

            LoadDir(argv);
            // this.PassivePanel.Visible = false;

#if DEBUG
            Console.WriteLine(@"DEBUG: MainWindow initialization has been completed.");
#endif
        }

        #region Methods

        private void Localize()
        {
            var themeInstance = theme.Control.Theme as theme.wpf;
            themeInstance.Localize(this);

        }

        private void mnuViewWithFilter_Clicked(object sender, EventArgs e)
        {
            string Filter = @"*.*";

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
					);*/
                ActivePanel.LoadDir(
                    ActivePanel.FS.CurrentDirectory,
                    new Shorten
                    {
                        KB = ActivePanel.CurShortenKB,
                        MB = ActivePanel.CurShortenMB,
                        GB = ActivePanel.CurShortenGB
                    }
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
            new SettingsWindow().Run();
            ActivePanel.LoadDir();
            PassivePanel.LoadDir();
        }

        private void mnuHelpAbout_Clicked(object sender, EventArgs e)
        {
            System.Configuration.Configuration conf = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.PerUserRoamingAndLocal);
            string AboutString = string.Format(
                Localizator.GetString("FileCommanderVer"),
                "File Commander",
                ProductVersion + "-virtualmode",
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
            Properties.Settings.Default.Panel1URL = p1.FS.CurrentDirectory;
            Properties.Settings.Default.Panel2URL = p2.FS.CurrentDirectory;
            Properties.Settings.Default.LastActivePanel = (ActivePanel == p1) ? (byte)1 : (byte)2;
            Properties.Settings.Default.Save();
            Xwt.Application.Exit();
        }

        /// <summary>The entry form's keyboard keypress handler (except commandbar keypresses)</summary>
        private void PanelLayout_KeyReleased(object sender, Xwt.KeyEventArgs e)
        {
#if DEBUG
            FileListPanel p1 = (PanelLayout.Panel1.Content as FileListPanel);
            FileListPanel p2 = (PanelLayout.Panel2.Content as FileListPanel);
            Console.WriteLine("KEYBOARD DEBUG: " + e.Modifiers + "+" + e.Key + " was pressed. Panels focuses: " + (ActivePanel == p1) + " | " + (ActivePanel == p2));
#endif
            if (e.Key == Xwt.Key.Return) return;//ENTER presses are handled by other event

            var control = theme.Control.Theme as theme.wpf;
            control.Key(this, e);

#if DEBUG
            Console.WriteLine("KEYBOARD DEBUG: the key wasn't handled");
#endif
            e.Handled = true;
        }

        /// <summary>Switches the active panel</summary>
        /// <param name="NewPanel">The new active panel</param>
        private void SwitchPanel(FileListPanel NewPanel)
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
        private FileListPanel.SizeDisplayPolicy ConvertSDP(char SizeDisplayPolicy)
        {
            switch (SizeDisplayPolicy.ToString())
            {
                case "0":
                    return FileListPanel.SizeDisplayPolicy.DontShorten;
                case "1":
                    return FileListPanel.SizeDisplayPolicy.OneNumeral;
                case "2":
                    return FileListPanel.SizeDisplayPolicy.TwoNumeral;
                default:
                    return FileListPanel.SizeDisplayPolicy.OneNumeral;
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
            PanelLayout.KeyReleased += PanelLayout_KeyReleased;
            mnuFileView.Clicked += (o, ea) => { PanelLayout_KeyReleased(o, new Xwt.KeyEventArgs(Xwt.Key.F3, Xwt.ModifierKeys.None, false, 0)); };
            mnuFileEdit.Clicked += (o, ea) => { PanelLayout_KeyReleased(o, new Xwt.KeyEventArgs(Xwt.Key.F4, Xwt.ModifierKeys.None, false, 0)); };
            mnuFileCopy.Clicked += (o, ea) => { PanelLayout_KeyReleased(o, new Xwt.KeyEventArgs(Xwt.Key.F5, Xwt.ModifierKeys.None, false, 0)); };
            mnuFileMove.Clicked += (o, ea) => { PanelLayout_KeyReleased(o, new Xwt.KeyEventArgs(Xwt.Key.F6, Xwt.ModifierKeys.None, false, 0)); };
            mnuFileNewDir.Clicked += (o, ea) => { PanelLayout_KeyReleased(o, new Xwt.KeyEventArgs(Xwt.Key.F7, Xwt.ModifierKeys.None, false, 0)); };
            mnuFileRemove.Clicked += (o, ea) => { PanelLayout_KeyReleased(o, new Xwt.KeyEventArgs(Xwt.Key.F8, Xwt.ModifierKeys.None, false, 0)); };
            mnuFileSelectAll.Clicked += (o, ea) => { ActivePanel.ListingView.Select(null); };
            mnuFileUnselect.Clicked += (o, ea) => { ActivePanel.ListingView.Unselect(); };
            mnuFileInvertSelection.Clicked += (o, ea) => { ActivePanel.ListingView.InvertSelection(); };
            mnuFileQuickSelect.Clicked += (o, ea) => { PanelLayout_KeyReleased(o, new Xwt.KeyEventArgs(Xwt.Key.NumPadAdd, Xwt.ModifierKeys.None, false, 0)); };
            mnuFileQuickUnselect.Clicked += (o, ea) => { PanelLayout_KeyReleased(o, new Xwt.KeyEventArgs(Xwt.Key.NumPadSubtract, Xwt.ModifierKeys.None, false, 0)); };
            mnuFileExit.Clicked += (o, ea) => { this.Close(); };
            mnuViewNoFilter.Clicked += (o, ea) => { ActivePanel.LoadDir(); };
            mnuViewWithFilter.Clicked += mnuViewWithFilter_Clicked;
            mnuNavigateReload.Clicked += mnuNavigateReload_Clicked;
            mnuToolsOptions.Clicked += mnuToolsOptions_Clicked;
            mnuHelpDebug.Clicked += ShowDebugInfo;
            mnuHelpAbout.Clicked += mnuHelpAbout_Clicked;
        }

        private void LayoutInit()
        {
            Layout.PackStart(PanelLayout, true, Xwt.WidgetPlacement.Fill, Xwt.WidgetPlacement.Fill, 0, 0, 0, 0);
            Layout.PackStart(KeyBoardHelp, false, Xwt.WidgetPlacement.End, Xwt.WidgetPlacement.Fill, 1, 3, 1, 2);

            this.Content = Layout;

            //check settings
            if (fcmd.Properties.Settings.Default.UserTheme != null)
            {
                if (fcmd.Properties.Settings.Default.UserTheme != "")
                {
                    if (File.Exists(fcmd.Properties.Settings.Default.UserTheme))
                        stylist = new Stylist(fcmd.Properties.Settings.Default.UserTheme);
                    else
                    {
                        Xwt.MessageDialog.ShowError(Localizator.GetString("ThemeNotFound"), fcmd.Properties.Settings.Default.UserTheme);
                        Xwt.Application.Exit();
                    }
                }
            }

            //load bookmarks
            string BookmarksStore = null;
            if (fcmd.Properties.Settings.Default.BookmarksFile != null && fcmd.Properties.Settings.Default.BookmarksFile.Length > 0)
            {
                BookmarksStore = File.ReadAllText(fcmd.Properties.Settings.Default.BookmarksFile, Encoding.UTF8);
            }

            //build panels
            PanelLayout.Panel1.Content = new FileListPanel(BookmarksStore, fcmd.Properties.Settings.Default.UserTheme, Properties.Settings.Default.InfoBarContent1, Properties.Settings.Default.InfoBarContent2); //Левая, правая где сторона? Улица, улица, ты, брат, пьяна!
            PanelLayout.Panel2.Content = new FileListPanel(BookmarksStore, fcmd.Properties.Settings.Default.UserTheme, Properties.Settings.Default.InfoBarContent1, Properties.Settings.Default.InfoBarContent2);

            p1 = PanelLayout.Panel1.Content as FileListPanel;
            p2 = PanelLayout.Panel2.Content as FileListPanel;
            var openFileHandler = new pluginner.TypedEvent<string>(Panel_OpenFile);
            p1.OpenFile += openFileHandler;
            p2.OpenFile += openFileHandler;

            // Ankr
            LVCols.Add(new ListView2.ColumnInfo { Title = "", Tag = "Icon", Width = 16, Visible = true });
            LVCols.Add(new ListView2.ColumnInfo { Title = "URL", Tag = "Path", Width = 0, Visible = false });
            LVCols.Add(new ListView2.ColumnInfo
            { Title = Localizator.GetString("FName"), Tag = "FName", Width = 100, Visible = true });
            LVCols.Add(new ListView2.ColumnInfo
            { Title = Localizator.GetString("FSize"), Tag = "FSize", Width = 50, Visible = true });
            LVCols.Add(new ListView2.ColumnInfo
            { Title = Localizator.GetString("FDate"), Tag = "FDate", Width = 50, Visible = true });
            LVCols.Add(new ListView2.ColumnInfo
            { Title = "Directory item info", Tag = "DirItem", Width = 0, Visible = false });
            // */

            p1.FS = new base_plugins.fs.localFileSystem();
            p2.FS = new base_plugins.fs.localFileSystem();

            p1.GotFocus += (o, ea) => SwitchPanel(p1);
            p2.GotFocus += (o, ea) => SwitchPanel(p2);
        }

        private void KeyBoardHelpInit()
        {
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

        private void LoadDir(string[] argv)
        {
            //file size display policy
            char[] Policies = fcmd.Properties.Settings.Default.SizeShorteningPolicy.ToCharArray();
            var Shorten = new Shorten
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
                    if (argv.Length == 1) p1.LoadDir(argv[0]);
                    break;
                case 2:
                    p2.ListingView.SetFocus();
                    ActivePanel = p2; PassivePanel = p1;
                    if (argv.Length == 1) p2.LoadDir(argv[0]);
                    break;
                default:
                    p1.ListingView.SetFocus();
                    ActivePanel = p1; PassivePanel = p2;
                    if (argv.Length == 1) p1.LoadDir(argv[0]);
                    break;
            }
        }

        #endregion
    }

    public struct Shorten
    {
        public FileListPanel.SizeDisplayPolicy KB { get; set; }
        public FileListPanel.SizeDisplayPolicy MB { get; set; }
        public FileListPanel.SizeDisplayPolicy GB { get; set; }
    }
}
