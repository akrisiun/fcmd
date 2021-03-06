﻿using pluginner.Widgets;
using System;
#if WPF
using System.Windows.Input;
using Key = System.Windows.Input.Key;
#else
using Key = Xwt.Key;
#endif
using fcmd.Model;
using Xwt;
using fcmd.FileList;

namespace fcmd.View
{
    // Window extension for WPF

    public class GtkBackend : IBackend
    {
        public void Init(ICommanderWindow window)
        {
            this.main = window as MainWindow;
            InitMenu(main);
        }

        public MainWindow Window { get { return main; } }
        private MainWindow main;

        public void Shown(MainWindow main)
        {
            main.p2.Visible = false;

            main.LoadDir(main.argv);

            var listing = main.ActivePanel.ListingView;
            //#else
            //            @this.LeftPanel.data.Columns.Clear();
            //            @this.RightPanel.data.Columns.Clear();

            //            @this.WindowData.LoadDir(Environment.GetCommandLineArgs());
        }

        public void InitMenu(MainWindow @this)
        {
            //    @this.KeyBoardHelp.Visible = false;

            //    //build user interface
            //    @this.MainMenu.Items.Add(@this.mnuFile);
            //    @this.MainMenu.Items.Add(@this.mnuView);
            //    @this.MainMenu.Items.Add(@this.mnuNavigate);
            //    @this.MainMenu.Items.Add(@this.mnuTools);
            //    @this.MainMenu.Items.Add(@this.mnuHelp);

            //    var mnuFile = @this.mnuFile;
            //    mnuFile.SubMenu = new Xwt.Menu();
            //    mnuFile.SubMenu.Items.Add(@this.mnuFileUserMenu);
            //    mnuFile.SubMenu.Items.Add(@this.mnuFileView);
            //    mnuFile.SubMenu.Items.Add(@this.mnuFileEdit);
            //    mnuFile.SubMenu.Items.Add(@this.mnuFileCompare);
            //    mnuFile.SubMenu.Items.Add(@this.mnuFileCopy);
            //    mnuFile.SubMenu.Items.Add(@this.mnuFileMove);
            //    mnuFile.SubMenu.Items.Add(@this.mnuFileNewDir);
            //    mnuFile.SubMenu.Items.Add(@this.mnuFileRemove);

            //    mnuFile.SubMenu.Items.Add(new Xwt.SeparatorMenuItem());
            //    mnuFile.SubMenu.Items.Add(@this.mnuFileAtributes);
            //    mnuFile.SubMenu.Items.Add(new Xwt.SeparatorMenuItem());
            //    mnuFile.SubMenu.Items.Add(@this.mnuFileQuickSelect);
            //    mnuFile.SubMenu.Items.Add(@this.mnuFileQuickUnselect);
            //    mnuFile.SubMenu.Items.Add(@this.mnuFileSelectAll);
            //    mnuFile.SubMenu.Items.Add(@this.mnuFileUnselect);
            //    mnuFile.SubMenu.Items.Add(@this.mnuFileInvertSelection);
            //    mnuFile.SubMenu.Items.Add(new Xwt.SeparatorMenuItem());
            //    mnuFile.SubMenu.Items.Add(@this.mnuFileExit);

            //    var mnuView = @this.mnuView;
            //    mnuView.SubMenu = new Xwt.Menu();
            //    mnuView.SubMenu.Items.Add(@this.mnuViewShort);
            //    mnuView.SubMenu.Items.Add(@this.mnuViewDetails);
            //    mnuView.SubMenu.Items.Add(@this.mnuViewIcons);
            //    mnuView.SubMenu.Items.Add(@this.mnuViewThumbs);
            //    mnuView.SubMenu.Items.Add(new Xwt.SeparatorMenuItem());
            //    mnuView.SubMenu.Items.Add(@this.mnuViewQuickView);
            //    mnuView.SubMenu.Items.Add(@this.mnuViewTree);
            //    mnuView.SubMenu.Items.Add(@this.mnuViewPCPCconnect);
            //    mnuView.SubMenu.Items.Add(@this.mnuViewPCNETPCconnect);
            //    mnuView.SubMenu.Items.Add(new Xwt.SeparatorMenuItem());
            //    mnuView.SubMenu.Items.Add(@this.mnuViewByName);
            //    mnuView.SubMenu.Items.Add(@this.mnuViewByType);
            //    mnuView.SubMenu.Items.Add(@this.mnuViewByDate);
            //    mnuView.SubMenu.Items.Add(@this.mnuViewBySize);
            //    mnuView.SubMenu.Items.Add(new Xwt.SeparatorMenuItem());
            //    mnuView.SubMenu.Items.Add(@this.mnuViewNoFilter);
            //    mnuView.SubMenu.Items.Add(@this.mnuViewWithFilter);
            //    mnuView.SubMenu.Items.Add(new Xwt.SeparatorMenuItem());
            //    mnuView.SubMenu.Items.Add(@this.mnuViewKeybrdHelp); //these checkboxes don't work, because no code was written
            //    mnuView.SubMenu.Items.Add(@this.mnuViewInfobar);
            //    mnuView.SubMenu.Items.Add(@this.mnuViewDiskButtons);

            //    var mnuNavigate = @this.mnuNavigate;
            //    mnuNavigate.SubMenu = new Xwt.Menu();
            //    mnuNavigate.SubMenu.Items.Add(@this.mnuNavigateTree);
            //    mnuNavigate.SubMenu.Items.Add(@this.mnuNavigateHistory);
            //    mnuNavigate.SubMenu.Items.Add(@this.mnuNavigateFind);
            //    mnuNavigate.SubMenu.Items.Add(@this.mnuNavigateReload);

            //    var mnuTools = @this.mnuTools;
            //    mnuTools.SubMenu = new Xwt.Menu();
            //    mnuTools.SubMenu.Items.Add(@this.mnuToolsOptions);
            //    mnuTools.SubMenu.Items.Add(@this.mnuToolsPluginManager);
            //    mnuTools.SubMenu.Items.Add(@this.mnuToolsEditUserMenu);
            //    mnuTools.SubMenu.Items.Add(@this.mnuToolsKeychains);
            //    mnuTools.SubMenu.Items.Add(@this.mnuToolsConfigEdit);
            //    mnuTools.SubMenu.Items.Add(new Xwt.SeparatorMenuItem());
            //    mnuTools.SubMenu.Items.Add(@this.mnuToolsDiskLabel);
            //    mnuTools.SubMenu.Items.Add(@this.mnuToolsFormat);
            //    mnuTools.SubMenu.Items.Add(@this.mnuToolsSysInfo);

            //    var mnuHelp = @this.mnuHelp;
            //    mnuHelp.SubMenu = new Xwt.Menu();
            //    mnuHelp.SubMenu.Items.Add(@this.mnuHelpHelpMe);
            //    mnuHelp.SubMenu.Items.Add(@this.mnuHelpDebug);
            //    mnuHelp.SubMenu.Items.Add(@this.mnuHelpAbout);

        }

        public void Localize(ICommanderWindow @this)
        {

            //    @this.TranslateMenu(@this.MainMenu);

            //    for (int i = 1; i < 11; i++)
            //    {
            //        @this.KeybHelpButtons[i].FKey = "F" + i;
            //        @this.KeybHelpButtons[i].Text = Localizator.GetString("FCF" + i);
            //    }

            //    var LVCols = @this.LVCols;
            //    LVCols.Clear();
            //    LVCols.Add(new ListView2.ColumnInfo
            //    { Title = "", Tag = "Icon", Width = 16, Visible = true });
            //    LVCols.Add(new ListView2.ColumnInfo
            //    { Title = "URL", Tag = "Path", Width = 0, Visible = false });
            //    LVCols.Add(new ListView2.ColumnInfo
            //    { Title = Localizator.GetString("FName"), Tag = "FName", Width = 200, Visible = true });
            //    LVCols.Add(new ListView2.ColumnInfo
            //    { Title = Localizator.GetString("FSize"), Tag = "FSize", Width = 50, Visible = true });
            //    LVCols.Add(new ListView2.ColumnInfo
            //    { Title = Localizator.GetString("FDate"), Tag = "FDate", Width = 170, // 50,
            //        Visible = true });
            //    LVCols.Add(new ListView2.ColumnInfo
            //    { Title = "Directory item info", Tag = "DirItem", Width = 0, Visible = false });

            //    @this.p1.ListingView.SetColumns(LVCols);
            //    @this.p2.ListingView.SetColumns(LVCols);
        }

        public void KeyEvent(MainWindow main, Xwt.KeyEventArgs e)
        // public void KeyEvent(MainWindow @this, System.Windows.Input.KeyEventArgs e)
        {
            var ActivePanel = main.ActivePanel;
            var PassivePanel = main.PassivePanel;

            //string URL1;
            //if (ActivePanel.ListingView.SelectedRow > -1)
            //{ URL1 = ActivePanel.GetValue(ActivePanel.df.URL); }
            //else
            //{ URL1 = null; }
            //pluginner.IFSPlugin FS1 = ActivePanel.FS;

            /* string URL2;
            if (PassivePanel.ListingView.SelectedRow > -1)
            { URL2 = PassivePanel.GetValue(PassivePanel.df.URL); }
            else
            { URL2 = null; }
            pluginner.IFSPlugin FS2 = PassivePanel.FS;
			*/

            switch (e.Key)
            {
#if !WPF
                case Xwt.Key.NumPadAdd: //[+] gray - add selection
#else
                case Key.OemPlus:
#endif

                    string Filter = @"*.*";

                    InputBox ibx_qs = new InputBox(Localizator.GetString("QuickSelect"), Filter);
                    Xwt.CheckBox chkRegExp = new Xwt.CheckBox(Localizator.GetString("NameFilterUseRegExp"));
                    // ibx_qs.OtherWidgets.Add(chkRegExp, 0, 0);

                    if (!ibx_qs.ShowDialog()) return;
                    Filter = ibx_qs.Result;
                    if (chkRegExp.State == Xwt.CheckBoxState.Off)
                    {
                        Filter = Filter.Replace(".", @"\.");
                        Filter = Filter.Replace("*", ".*");
                        Filter = Filter.Replace("?", ".");
                    }
                    try
                    {
                        System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(Filter);

                        int Count = 0;
                        //foreach (ListView2Item lvi in ActivePanel.ListingView.Items)
                        //{
                        //    if (re.IsMatch(lvi.Data[1].ToString()))
                        //    {
                        //        ActivePanel.ListingView.Select(lvi);
                        //        Count++;
                        //    }
                        //}

                        ActivePanel.StatusBar.Text = string.Format(Localizator.GetString("NameFilterFound"), Filter, Count);
                    }
                    catch (Exception ex)
                    {
                        Xwt.MessageDialog.ShowError(Localizator.GetString("NameFilterError"), ex.Message);
                    }
                    return;

#if XWT
                case Xwt.Key.NumPadSubtract: //[-] gray - add selection
#else
                case Key.OemMinus:
#endif
                    string Filter_qus = @"*.*";

                    InputBox ibx_qus = new InputBox(Localizator.GetString("QuickUnselect"), Filter_qus);
                    Xwt.CheckBox chkRegExp_qus = new Xwt.CheckBox(Localizator.GetString("NameFilterUseRegExp"));
                    // ibx_qus.OtherWidgets.Add(chkRegExp_qus, 0, 0);

                    if (!ibx_qus.ShowDialog()) return;
                    Filter_qus = ibx_qus.Result;
                    if (chkRegExp_qus.State == Xwt.CheckBoxState.Off)
                    {
                        Filter_qus = Filter_qus.Replace(".", @"\.");
                        Filter_qus = Filter_qus.Replace("*", ".*");
                        Filter_qus = Filter_qus.Replace("?", ".");
                    }
                    try
                    {
                        System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(Filter_qus);

                        //int Count_qus = 0;
                        //foreach (ListView2Item lvi in ActivePanel.ListingView.Items)
                        //{
                        //    if (re.IsMatch(lvi.Data[1].ToString()))
                        //    {
                        //        ActivePanel.ListingView.Unselect(lvi);
                        //        Count_qus++;
                        //    }
                        //}
                    }
                    catch (Exception ex)
                    {
                        Xwt.MessageDialog.ShowError(Localizator.GetString("NameFilterError"), ex.Message);
                    }
                    return;

                //F KEYS
                case Key.F3: //F3: View. Shift+F3: View as text.

                    //#if XWT
                    //                    if (URL1 == null)
                    //                        return;

                    //                    if (!FS1.FileExists(URL1))
                    //                    {
                    //                        Xwt.MessageDialog.ShowWarning(string.Format(Localizator.GetString("FileNotFound"), 
                    //                            ActivePanel.GetValue(ActivePanel.df.DisplayName)));
                    //                        return;
                    //                    }

                    VEd V = new VEd();
                    //if (e.Modifiers == Xwt.ModifierKeys.None)
                    //{
                    //    V.LoadFile(URL1, FS1, false);
                    //    V.Show();
                    //}
                    //else if (e.Modifiers == Xwt.ModifierKeys.Shift)
                    //{
                    //    V.LoadFile(URL1, FS1, new base_plugins.ve.PlainText(), false);
                    //    V.Show();
                    //}

                    //todo: handle Ctrl+F3 (Sort by name).
                    return;

                // case Xwt.Key.F4: //F4: Edit. Shift+F4: Edit as txt.
                case Key.F4:

                    //#if XWT
                    //                    if (URL1 == null)
                    //                        return;

                    //                    if (!FS1.FileExists(URL1))
                    //                    {
                    //                        Xwt.MessageDialog.ShowWarning(string.Format(Localizator.GetString("FileNotFound"), ActivePanel.GetValue(ActivePanel.df.DisplayName)));
                    //                        return;
                    //                    }

                    VEd E = new VEd();
                    //if (e.Modifiers == Xwt.ModifierKeys.None)
                    //{ E.LoadFile(URL1, FS1, true); E.Show(); }
                    //else if (e.Modifiers == Xwt.ModifierKeys.Shift)
                    //{ E.LoadFile(URL1, FS1, new base_plugins.ve.PlainText(), true); E.Show(); }
                    //todo: handle Ctrl+F4 (Sort by extension).
                    return;

                case Key.F5: //F5: Copy.

                    //if (URL1 == null)
                    //    return;
                    main.Cp();
                    //todo: handle Ctrl+F5 (Sort by timestamp).
                    return;

                case Key.F6: //F6: Move/Rename.

                    //if (URL1 == null)
                    //    return;
                    main.Mv();
                    //todo: handle Ctrl+F6 (Sort by size).
                    return;
                case Key.F7: //F7: New directory.
                    InputBox ibx = new InputBox(Localizator.GetString("NewDirURL"), ActivePanel.FS.CurrentDirectory + Localizator.GetString("NewDirTemplate"));
                    if (ibx.ShowDialog())
                        main.MkDir(ibx.Result);
                    return;

                case Key.F8: //F8: delete
                    //if (URL1 == null)
                    //    return;
                    //main.Rm();
                    //todo: move to trash can/recycle bin & handle Shit+F8 (remove completely)
                    return;

                case Key.F10: //F10: Exit
                              //todo: ask user, are it really want to close FC?

                    Xwt.Application.Exit();
                    // System.Windows.Application.Current.Shutdown();
                    //todo: handle Alt+F10 (directory tree)
                    return;
            }
        }

        //public void Init(ICommanderWindow window)
        //{
        //    throw new NotImplementedException();
        //}

        //public void KeyEvent(MainWindow window, KeyEventArgs key)
        //{
        //    throw new NotImplementedException();
        //}

        public void Localize()
        {
            throw new NotImplementedException();
        }

        public void Shown()
        {
            throw new NotImplementedException();
        }

        public void ShowMessage(string message, params object[] args)
        {
            var msgText = string.Format(message, args);
            Xwt.MessageDialog.ShowMessage(msgText);
        }
        public void ShowError(Exception error, string message, params object[] args)
        {
            string errorText = String.Concat(message ?? string.Empty, error.Message);
            System.Console.WriteLine(errorText);
            var lines = error.StackTrace.Split(Environment.NewLine.ToCharArray());
            var top5 = System.Linq.Enumerable.Take(lines, 5).GetEnumerator();
            while (top5.MoveNext())
                Console.WriteLine(top5.Current as string);

            Xwt.MessageDialog.ShowError(errorText);
        }

        public bool? ShowConfirm(string message, Xwt.ConfirmationMessage details = null)
        {
            Xwt.MessageDialog.RootWindow = fcmd.MainWindow.Current;
            var msg = details ?? new Xwt.ConfirmationMessage { Text = message };
            bool yes = Xwt.MessageDialog.Confirm(msg);
            return yes;
        }
    }
}
