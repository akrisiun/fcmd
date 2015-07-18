/* The File Commander - internal Viewer/Editor
 * The main window of the viewer/editor (VE)
 * (C) The File Commander Team - https://github.com/atauenis/fcmd
 * (C) 2013-14, Alexander Tauenis (atauenis@yandex.ru)
 * (C) 2014, Evgeny Akhtimirov (wilbit@me.com)
 * Contributors should place own signs here.
 */
using System;
using System.Linq;
using System.Text;
using fcmd.base_plugins.ve;
using fcmd.Properties;
// using mucss;
using pluginner;
using pluginner.Toolkit;
using Xwt;
using KeyEventArgs = Xwt.KeyEventArgs;
using Menu = Xwt.Menu;
using MenuItem = Xwt.MenuItem;

namespace fcmd
{
    /// <summary>Viewer-Editor</summary>
    class VEd
    {
        // Stylist s = new Stylist(Settings.Default.UserTheme);
        bool CanBeShowed = true; //if any errors occur, this variable prevents broken VE window to show
        bool IsEditor;
        private string FileNameForTitle;

        protected IVEPlugin Plugin { get; set; }
        protected IFSPlugin FSPlugin { get; set; }
        protected VEdWindow Visual { get; set; }

        // Visual backed with XWT
        public class VEdWindow : Window
        {

            //Xwt.Menu MainMenu = new Xwt.Menu();
            public MenuItem mnuFile = new MenuItem { Tag = "mnuFile" };
            public MenuItem mnuFileNew = new MenuItem { Tag = "mnuFileNew" };
            public MenuItem mnuFileOpen = new MenuItem { Tag = "mnuFileOpen" };
            public MenuItem mnuFileReload = new MenuItem { Tag = "mnuFileReload" };
            public MenuItem mnuFileSave = new MenuItem { Tag = "mnuFileSave" };
            public MenuItem mnuFilePrint = new MenuItem { Tag = "mnuFilePrint" };
            public MenuItem mnuFilePrintSettings = new MenuItem { Tag = "mnuFilePrintSettings" };
            public MenuItem mnuFilePrintPreview = new MenuItem { Tag = "mnuFilePrintPreview" };
            public MenuItem mnuFileClose = new MenuItem { Tag = "mnuFileClose" };

            public MenuItem mnuEdit = new MenuItem { Tag = "mnuEdit", UseMnemonic = true };
            public MenuItem mnuEditCut = new MenuItem { Tag = "mnuEditCut" };
            public MenuItem mnuEditCopy = new MenuItem { Tag = "mnuEditCopy" };
            public MenuItem mnuEditPaste = new MenuItem { Tag = "mnuEditPaste" };
            public MenuItem mnuEditSelectAll = new MenuItem { Tag = "mnuEditSelAll" };
            public MenuItem mnuEditFindReplace = new MenuItem { Tag = "mnuEditSearch" };
            public MenuItem mnuEditFindNext = new MenuItem { Tag = "mnuEditSearchNext" };

            public MenuItem mnuView = new MenuItem();
            public MenuItem mnuViewSettings = new MenuItem { Tag = "mnuViewSettings" };
            public MenuItem mnuFormat = new MenuItem();

            public MenuItem mnuHelp = new MenuItem();
            public MenuItem mnuHelpHelpme = new MenuItem { Tag = "mnuHelpHelpme" };
            public MenuItem mnuHelpAbout = new MenuItem { Tag = "mnuHelpAbout" };

            public VBox Layout = new VBox();
            public Widget PluginBody;

            public TextEntry CommandBox = new TextEntry();
            public HBox KeyBoardHelp = new HBox();
            public KeyboardHelpButton[] KeybHelpButtons = new KeyboardHelpButton[11]; //одна лишняя, которая [0]

            public void Init(EventHandler KeyboardButtonClicked)
            {

                for (int i = 1; i < 11; i++)
                {
                    KeybHelpButtons[i] = new KeyboardHelpButton();
                    KeybHelpButtons[i].FKey = "F" + i;
                    KeybHelpButtons[i].Text = Localizator.GetString("FCVE_F" + i);
                    KeybHelpButtons[i].CanGetFocus = false;
                    KeybHelpButtons[i].Clicked += KeyboardButtonClicked; // KeyboardHelpButton_Clicked;
                    KeybHelpButtons[i].Tag = i;
                    KeyBoardHelp.PackStart(KeybHelpButtons[i], true, WidgetPlacement.Fill, WidgetPlacement.Fill, 0, 1, 0, 1);
                }

                Title = "File Commander VE";
                Content = Layout;

                mnuFile.SubMenu = new Xwt.Menu();
                mnuFile.SubMenu.Items.Add(mnuFileNew);
                mnuFile.SubMenu.Items.Add(mnuFileOpen);
                mnuFile.SubMenu.Items.Add(mnuFileReload);
                mnuFile.SubMenu.Items.Add(mnuFileSave);
                mnuFile.SubMenu.Items.Add(new SeparatorMenuItem());
                mnuFile.SubMenu.Items.Add(mnuFilePrint);
                mnuFile.SubMenu.Items.Add(mnuFilePrintPreview);
                mnuFile.SubMenu.Items.Add(mnuFilePrintSettings);
                mnuFile.SubMenu.Items.Add(new SeparatorMenuItem());
                mnuFile.SubMenu.Items.Add(mnuFileClose);

                mnuEdit.SubMenu = new Xwt.Menu();
                mnuEdit.SubMenu.Items.Add(mnuEditCut);
                mnuEdit.SubMenu.Items.Add(mnuEditCopy);
                mnuEdit.SubMenu.Items.Add(mnuEditPaste);
                mnuEdit.SubMenu.Items.Add(new SeparatorMenuItem());
                mnuEdit.SubMenu.Items.Add(mnuEditSelectAll);
                mnuEdit.SubMenu.Items.Add(new SeparatorMenuItem());
                mnuEdit.SubMenu.Items.Add(mnuEditFindReplace);
                mnuEdit.SubMenu.Items.Add(mnuEditFindNext);

                mnuViewSettings.Clicked += (o, ea) =>
                {
                    new VEsettings().Run();
                    // Plugin.ShowToolbar = Settings.Default.VE_ShowToolbar;
                    KeyBoardHelp.Visible = Settings.Default.ShowKeybrdHelp;
                    CommandBox.Visible = Settings.Default.VE_ShowCmdBar;
                };
                mnuView.SubMenu = new Xwt.Menu();
                mnuView.SubMenu.Items.Add(mnuViewSettings);

                mnuHelp.SubMenu = new Xwt.Menu();
                mnuHelp.SubMenu.Items.Add(mnuHelpHelpme);
                mnuHelp.SubMenu.Items.Add(mnuHelpAbout);

                MainMenu = new Xwt.Menu();
                MainMenu.Items.Add(mnuFile);
                MainMenu.Items.Add(mnuEdit);
                MainMenu.Items.Add(mnuView);
                MainMenu.Items.Add(mnuFormat);
                MainMenu.Items.Add(mnuHelp);

                PluginBody = new Spinner { Animate = true };
            }

        }

        public VEd()
        {
            Visual = new VEdWindow();

            Visual.Init(this.KeyboardHelpButton_Clicked);
            Localizator.LocalizationChanged +=
                (o, ea) => Localize();

            Visual.CommandBox.KeyReleased += CommandBox_KeyReleased;
            Visual.Layout.KeyReleased += Layout_KeyReleased;

            // Events defined here
            Visual.mnuFileOpen.Clicked += (o, ea) => OpenFile();
            Visual.mnuFilePrint.Clicked += (o, ea) => SendCommand("print");
            Visual.mnuFilePrintSettings.Clicked += (o, ea) => SendCommand("pagesetup");
            Visual.mnuFilePrintPreview.Clicked += (o, ea) => SendCommand("print preview");
            Visual.mnuFileClose.Clicked += (o, ea) => Exit();
            Visual.mnuEditCut.Clicked += (o, ea) => SendCommand("cut selected");
            Visual.mnuEditCopy.Clicked += (o, ea) => SendCommand("copy selected");
            Visual.mnuEditPaste.Clicked += (o, ea) => SendCommand("paste clipboard");
            Visual.mnuEditSelectAll.Clicked += (o, ea) => SendCommand("select *");
            Visual.mnuEditFindReplace.Clicked += (o, ea) => SendCommand("findreplace");
            Visual.mnuEditFindNext.Clicked += (o, ea) => SendCommand("findreplace last");
            Visual.mnuHelpAbout.Clicked += mnuHelpAbout_Clicked;
            Visual.CloseRequested += VEd_CloseRequested;

            BuildLayout();
            Localize();
        }

        #region Methods

        void Layout_KeyReleased(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Visual.CommandBox.SetFocus();
                    return;
                case Key.F10:
                case Key.q:
                    Visual.Close();
                    return;
            }
        }

        protected void KeyboardHelpButton_Clicked(object sender, EventArgs e)
        {
            switch (Convert.ToInt32(((KeyboardHelpButton)sender).Tag))
            {
                case 1:
                    Layout_KeyReleased(sender, new KeyEventArgs(Key.F1, ModifierKeys.None, false, 0));
                    break;
                case 2:
                    Layout_KeyReleased(sender, new KeyEventArgs(Key.F2, ModifierKeys.None, false, 0));
                    break;
                case 3:
                    Layout_KeyReleased(sender, new KeyEventArgs(Key.F3, ModifierKeys.None, false, 0));
                    break;
                case 4:
                    Layout_KeyReleased(sender, new KeyEventArgs(Key.F4, ModifierKeys.None, false, 0));
                    break;
                case 5:
                    Layout_KeyReleased(sender, new KeyEventArgs(Key.F5, ModifierKeys.None, false, 0));
                    break;
                case 6:
                    Layout_KeyReleased(sender, new KeyEventArgs(Key.F6, ModifierKeys.None, false, 0));
                    break;
                case 7:
                    Layout_KeyReleased(sender, new KeyEventArgs(Key.F7, ModifierKeys.None, false, 0));
                    break;
                case 8:
                    Layout_KeyReleased(sender, new KeyEventArgs(Key.F8, ModifierKeys.None, false, 0));
                    break;
                case 9:
                    Layout_KeyReleased(sender, new KeyEventArgs(Key.F9, ModifierKeys.None, false, 0));
                    break;
                case 10:
                    Layout_KeyReleased(sender, new KeyEventArgs(Key.F10, ModifierKeys.None, false, 0));
                    break;

            }
        }

        void VEd_CloseRequested(object sender, CloseRequestedEventArgs args)
        {
            Settings.Default.VEWinHeight = Visual.Height;
            Settings.Default.VEWinWidth = Visual.Width;

            try
            {
                SendCommand("unload");
            }
            catch (Exception e) { MessageDialog.ShowError(e.Message, e.StackTrace + "\n   on " + Plugin.Name + " (" + Plugin.GetType() + ")"); }
        }

        public void Show()
        {
            if (CanBeShowed)
                Visual.Show();
        }

        protected void mnuHelpAbout_Clicked(object sender, EventArgs e)
        {
            string AboutString1 = String.Format(Localizator.GetString("FCVEVer1"),
                                                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
            string AboutString2 = string.Format(Localizator.GetString("FCVEVer2"), Plugin.Name, Plugin.Version, "\n", Plugin.Author);
            MessageDialog.ShowMessage(AboutString1, AboutString2);
        }

        protected void CommandBox_KeyReleased(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (Visual.CommandBox.Text == "q")
                {
                    Visual.Close();
                    return;
                }
                if (Visual.CommandBox.Text == "q!")
                {
                    Plugin.SaveFile();
                    Visual.Close(); return;
                }
                SendCommand(Visual.CommandBox.Text);
            }
        }

        /// <summary>
        /// Sends a command to the plugin
        /// </summary>
        /// <param name="Cmd">the command in DOS/*NIX-style: NAME ARG1 ARG2 ARG3</param>
        protected void SendCommand(string Cmd)
        {
            Plugin.APICallPlugin(Cmd.Split(" ".ToCharArray())[0], Cmd.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries));
            Visual.CommandBox.Text = "";
        }

        /// <summary>Sets VE mode (view or edit)</summary>
        /// <param name="AllowEdit">Set to true if edit controls should be showed</param>
        protected void SetVEMode(bool AllowEdit)
        {
            Visual.mnuFileSave.Visible = AllowEdit;
            Visual.mnuFileNew.Visible = AllowEdit;
            Visual.mnuEditCut.Visible = AllowEdit;
            Visual.mnuEditPaste.Visible = AllowEdit;
        }

        /// <summary>Load the file in the VE (with plugin autodetection)</summary>
        /// <param name="URL">The file's URL</param>
        /// <param name="FS">The file's filesystem</param>
        /// <param name="AllowEdit">Mode of VE: true=editor, false=viewer</param>
        public void LoadFile(string URL, IFSPlugin FS, bool AllowEdit)
        {
            try
            {
                byte[] ContentBytes = FS.GetFileContent(URL);
                string content = (ContentBytes != null && ContentBytes.Length > 0) ? Encoding.UTF8.GetString(ContentBytes) : "";
                pluginfinder pf = new pluginfinder();

                string GottenHeaders;
                if (content.Length >= 20) GottenHeaders = content.Substring(0, 20);
                else GottenHeaders = content;
                LoadFile(URL, FS, pf.GetFCVEplugin("NAME=" + URL + "HEADERS=" + GottenHeaders), AllowEdit);
            }
            catch (pluginfinder.PluginNotFoundException ex)
            {
                // ReSharper disable LocalizableElement
                Console.WriteLine("ERROR: VE plugin is not loaded: " + ex.Message + "\n" + ex.StackTrace);

                MessageDialog.ShowError(Localizator.GetString("FCVE_PluginNotFound"));
                LoadFile(URL, FS, new PlainText(), AllowEdit);
            }
            catch (Exception ex)
            {
                MessageDialog.ShowError(string.Format(Localizator.GetString("FCVE_LoadError"), ex.Message));
                Console.WriteLine("ERROR: VE can't load file: " + ex.Message + "\n" + ex.StackTrace);
                // ReSharper restore LocalizableElement
            }
        }

        #endregion

        #region Load

        /// <summary>Load the file in the VE</summary>
        /// <param name="URL">The URL of the file</param>
        /// <param name="FS">The filesystem of the file</param>
        /// <param name="plugin">The VE plugin, which will be used to load this file</param>
        /// <param name="AllowEdit">Allow editing the file</param>
        public void LoadFile(string URL, IFSPlugin FS, IVEPlugin plugin, bool AllowEdit)
        {
            // check for external editor
            if (UseExternal(URL, AllowEdit))
                return;

            FileNameForTitle = URL.Substring(URL.LastIndexOf(FS.DirSeparator, StringComparison.Ordinal) + 1);
            IsEditor = AllowEdit;

            if (AllowEdit)
                Visual.Title = string.Format(Localizator.GetString("FCETitle"), FileNameForTitle);
            else
                Visual.Title = string.Format(Localizator.GetString("FCVTitle"), FileNameForTitle);

            ProgressDialog = new FileProcessDialog();
            string ProgressInitialText = String.Format(Localizator.GetString("FCVELoadingMsg"), URL);
            ProgressDialog.lblStatus.Text = ProgressInitialText;
            FS.ProgressChanged +=
                d =>
                {
                    ProgressDialog.pbrProgress.Fraction
                    = (d >= 0 && d <= 1) ? d : ProgressDialog.pbrProgress.Fraction;
                    Xwt.Application.MainLoop.DispatchPendingEvents();
                };
            FS.StatusChanged +=
                d =>
                {
                    ProgressDialog.lblStatus.Text = ProgressInitialText + "\n" + d;
                    Xwt.Application.MainLoop.DispatchPendingEvents();
                };

            ProgressDialog.cmdCancel.Clicked += (o, ea) => { CanBeShowed = false; ProgressDialog.Close(); };
            ProgressDialog.Show();
            Xwt.Application.MainLoop.DispatchPendingEvents();

            if (!CanBeShowed) return;

            PluginMode(URL, FS, plugin, AllowEdit);
            BuildLayout();

            ProgressDialog.Close();
            ProgressDialog = null;
        }

        protected FileProcessDialog ProgressDialog;

        bool UseExternal(string URL, bool AllowEdit)
        {
            try
            {
                if (Settings.Default.UseExternalEditor && AllowEdit || Settings.Default.UseExternalViewer && !AllowEdit && URL.StartsWith("file:"))
                {
                    CanBeShowed = false;
                    if (AllowEdit)
                    {
                        ExecuteProgram(Settings.Default.ExternalEditor.Replace("$", "\"" + URL));
                    }
                    else
                    {
                        ExecuteProgram(Settings.Default.ExternalViewer.Replace("$", "\"" + URL));
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageDialog.ShowError(Localizator.GetString("CantRunEXE"), ex.Message);
                CanBeShowed = false;
                return true;
            }

            return false;
        }

        protected void PluginMode(string URL, IFSPlugin FS, IVEPlugin plugin, bool AllowEdit)
        {
            try
            {
                Plugin = plugin;
                Plugin.ReadOnly = !AllowEdit;
                Plugin.OpenFile(URL, FS);
                Plugin.ShowToolbar = Settings.Default.VE_ShowToolbar;

                bool Mode = AllowEdit;

#if XWT
                //Plugin.Stylist = s;
                Visual.mnuFormat.SubMenu = Plugin.FormatMenu as Xwt.Menu;

                if (!Plugin.CanEdit && AllowEdit)
                {
                    MessageDialog.ShowWarning(String.Format(Localizator.GetString("FCVEpluginro1"),
                        Plugin.Name + " " + Plugin.Version), Localizator.GetString("FCVEpluginro2")
                        );
                    Mode = false;
                }

                FSPlugin = FS;
                Visual.PluginBody = Plugin.Body as Xwt.Widget;
#endif

                SetVEMode(Mode);
            }
            catch (Exception ex)
            {
                MessageDialog.ShowWarning(ex.Message);
                if (Visual.PluginBody.GetType() == typeof(Spinner))
                {
                    ProgressDialog.Close();
                    CanBeShowed = false;
                    return;
                }
            }
        }

        protected void OpenFile()
        {
            /*FileChooser OpenFileDialog = new FileChooser(FSPlugin);
			if (OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel || OpenFileDialog.SelectedFile == null) return;
			SendCommand("unload");
			LoadFile(OpenFileDialog.SelectedFile, OpenFileDialog.listPanel1.FSProvider, IsEditor);*/
            //UNDONE: Файл->Открыть
        }

#endregion

        /// <summary>(Re)builds the "Layout" vbox</summary>
        public void BuildLayout()
        {
            Visual.Padding = 0;
            Visual.Layout.Clear();
            Visual.Layout.PackStart(Visual.PluginBody, true, WidgetPlacement.Fill, WidgetPlacement.Fill, 0, 0, 0, 0);
            Visual.Layout.PackStart(Visual.CommandBox, false, WidgetPlacement.Fill, WidgetPlacement.Fill, 0, 0, 0, -6);
            if (Settings.Default.ShowKeybrdHelp)
                Visual.Layout.PackStart(Visual.KeyBoardHelp, false, WidgetPlacement.Fill, WidgetPlacement.Fill, 1, 3, 1, 2);

            Visual.Resizable = true; //fix for some stupid xwt toolkits.

            //Selector sel = s.CSS["VE"];
            //try
            //{
            //    Height = (sel.Declarations["height"].Value == "auto" ? Settings.Default.VEWinHeight : Convert.ToDouble(sel.Declarations["height"].Value));
            //    Width = (sel.Declarations["width"].Value == "auto" ? Settings.Default.VEWinWidth : Convert.ToDouble(sel.Declarations["width"].Value));
            //}
            //catch { } //a dirty workaround for a Xwt.WPF bug (""-1" не является допустимым значением для свойства "Width"." (System.ArgumentException))
            //if (sel.Declarations["background-color"].Value != "inherit")
            //{
            //    Layout.BackgroundColor =
            //    Utilities.GetXwtColor(
            //        sel.Declarations["background-color"].Value
            //    );
            //}
        }

#region Localize 

        /// <summary>
        /// Write UI labels dependencing on the current UI language
        /// </summary>
        protected void Localize()
        {
            if (Visual.KeybHelpButtons.Length == 11)
                for (int i = 1; i < 11; i++)
                {
                    Visual.KeybHelpButtons[i].FKey = "F" + i;
                    Visual.KeybHelpButtons[i].Text = Localizator.GetString("FCVE_F" + i);
                }

            Visual.mnuFile.Label = Localizator.GetString("FCVE_mnuFile");
            TranslateMenu(Visual.mnuFile.SubMenu);

            Visual.mnuEdit.Label = Localizator.GetString("FCVE_mnuEdit");
            TranslateMenu(Visual.mnuEdit.SubMenu);

            Visual.mnuView.Label = Localizator.GetString("FCVE_mnuView");
            TranslateMenu(Visual.mnuView.SubMenu);

            Visual.mnuFormat.Label = Localizator.GetString("FCVE_mnuFormat");
            Visual.mnuHelp.Label = Localizator.GetString("FCVE_mnuHelp");
            TranslateMenu(Visual.mnuHelp.SubMenu);

            if (IsEditor)
                Visual.Title = string.Format(Localizator.GetString("FCETitle"), FileNameForTitle);
            else
                Visual.Title = string.Format(Localizator.GetString("FCVTitle"), FileNameForTitle);
        }


        /// <summary>Translates the <paramref name="mnu"/> into the current UI language</summary>
        protected void TranslateMenu(Xwt.Menu mnu)
        {
            if (mnu == null) return;
            foreach (MenuItem currentMenuItem in mnu.Items)
            {
                if (currentMenuItem.GetType() != typeof(SeparatorMenuItem))
                { //skip separators
                    currentMenuItem.Label = Localizator.GetString("FCVE_" + currentMenuItem.Tag);
                    TranslateMenu(currentMenuItem.SubMenu);
                }
            }

            Visual.KeyBoardHelp.Visible = Settings.Default.ShowKeybrdHelp;
            Visual.CommandBox.Visible = Settings.Default.VE_ShowCmdBar;
        }

#endregion

        protected void Exit()
        {
            SendCommand("unload");
            Visual.Close();
        }

        /// <summary>
        /// Run an command line like cmd/bash works
        /// </summary>
        /// <param name="CmdLine"></param>
        protected void ExecuteProgram(string CmdLine)
        {
            //warning:this program works ugly, the file url shuld start with "file://".
            //todo: replace this shit-like parameter detection code with more goodly (taking into account the quotes)
            //	  QUICKER!!!
            string OrigCmdLine = CmdLine;
            int RazdelPo = OrigCmdLine.IndexOf(" ", StringComparison.Ordinal);
            string ExeArgs = OrigCmdLine.Substring(RazdelPo + 1).Substring(8); //cut exe filename, "file://" prefix
            string ExeName = OrigCmdLine.Substring(0, RazdelPo); //cut arguments 

            System.Diagnostics.Process proc = new System.Diagnostics.Process
            {
                StartInfo = { FileName = ExeName, Arguments = ExeArgs, ErrorDialog = true }
            };
            proc.Start();
        }

    }
}
