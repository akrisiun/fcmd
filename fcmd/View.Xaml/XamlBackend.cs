using fcmd.FileList;
using fcmd.Model;
using pluginner.Widgets;
using System;
using System.Windows.Input;
using System.Threading;
using System.Threading.Tasks;
using fcmd.View.Xaml;

namespace fcmd.View
{
    // Window extension for XAML Presentation framework

    public class WpfBackend : IBackend
    {
        public void Init(ICommanderWindow window)
        {
            main = window as MainWindow;
            InitMenu();
        }

        MainWindow IBackend.Window { get { return main; } }
        protected MainWindow main;

        public void Shown() { }

        public void Shown(PanelWpf panel1, PanelWpf panel2)
        {
            panel1.data.Columns.Clear();
            panel2.data.Columns.Clear();

            var data = main.WindowData as WindowDataWpf;
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            var task = Task.Factory.StartNew(
                () =>
                    data.LoadDirAsync(Environment.GetCommandLineArgs(), scheduler)
                );

            task.ContinueWith(
                (t) => main.Dispatcher.Invoke(
                    () => AfterLoadDir(panel1, panel2))
                );
        }

        void AfterLoadDir(PanelWpf panel1, PanelWpf panel2)
        {
            var view1 = panel1.PanelDataWpf.ListingView;
            
            // if (view.DataItems.Count > 0)
            view1.SetupColumns();
            view1.SelectedRow = 0;

            var view2 = panel2.PanelDataWpf.ListingView;
            view2.SetupColumns();

            panel1.PanelDataWpf.UrlBox.Text = panel1.PanelDataWpf.FS.CurrentDirectory;
            panel2.PanelDataWpf.UrlBox.Text = panel2.PanelDataWpf.FS.CurrentDirectory;

            panel2.PanelDataWpf.WindowData = main.WindowData as WindowDataWpf;
            panel2.Shown();

            panel1.PanelDataWpf.WindowData = main.WindowData as WindowDataWpf;
            panel1.Shown();
            view1.SetFocus();
            
            // KeyEventHandler  object sender, KeyEventArgs e);
            main.PreviewKeyDown 
                += (s, e) => this.KeyEvent(s, e);
        }

        public void InitMenu()
        {
            //    main.KeyBoardHelp.Visible = false;

            //    //build user interface
            //    main.MainMenu.Items.Add(main.mnuFile);
            //    main.MainMenu.Items.Add(main.mnuView);
            //    main.MainMenu.Items.Add(main.mnuNavigate);
            //    main.MainMenu.Items.Add(main.mnuTools);
            //    main.MainMenu.Items.Add(main.mnuHelp);

            //    var mnuFile = main.mnuFile;
            //    mnuFile.SubMenu = new Xwt.Menu();
            //    mnuFile.SubMenu.Items.Add(main.mnuFileUserMenu);
            //    mnuFile.SubMenu.Items.Add(main.mnuFileView);
            //    mnuFile.SubMenu.Items.Add(main.mnuFileEdit);
            //    mnuFile.SubMenu.Items.Add(main.mnuFileCompare);
            //    mnuFile.SubMenu.Items.Add(main.mnuFileCopy);
            //    mnuFile.SubMenu.Items.Add(main.mnuFileMove);
            //    mnuFile.SubMenu.Items.Add(main.mnuFileNewDir);
            //    mnuFile.SubMenu.Items.Add(main.mnuFileRemove);

            //    mnuFile.SubMenu.Items.Add(new Xwt.SeparatorMenuItem());
            //    mnuFile.SubMenu.Items.Add(main.mnuFileAtributes);
            //    mnuFile.SubMenu.Items.Add(new Xwt.SeparatorMenuItem());
            //    mnuFile.SubMenu.Items.Add(main.mnuFileQuickSelect);
            //    mnuFile.SubMenu.Items.Add(main.mnuFileQuickUnselect);
            //    mnuFile.SubMenu.Items.Add(main.mnuFileSelectAll);
            //    mnuFile.SubMenu.Items.Add(main.mnuFileUnselect);
            //    mnuFile.SubMenu.Items.Add(main.mnuFileInvertSelection);
            //    mnuFile.SubMenu.Items.Add(new Xwt.SeparatorMenuItem());
            //    mnuFile.SubMenu.Items.Add(main.mnuFileExit);

            //    var mnuView = main.mnuView;
            //    mnuView.SubMenu = new Xwt.Menu();
            //    mnuView.SubMenu.Items.Add(main.mnuViewShort);
            //    mnuView.SubMenu.Items.Add(main.mnuViewDetails);
            //    mnuView.SubMenu.Items.Add(main.mnuViewIcons);
            //    mnuView.SubMenu.Items.Add(main.mnuViewThumbs);
            //    mnuView.SubMenu.Items.Add(new Xwt.SeparatorMenuItem());
            //    mnuView.SubMenu.Items.Add(main.mnuViewQuickView);
            //    mnuView.SubMenu.Items.Add(main.mnuViewTree);
            //    mnuView.SubMenu.Items.Add(main.mnuViewPCPCconnect);
            //    mnuView.SubMenu.Items.Add(main.mnuViewPCNETPCconnect);
            //    mnuView.SubMenu.Items.Add(new Xwt.SeparatorMenuItem());
            //    mnuView.SubMenu.Items.Add(main.mnuViewByName);
            //    mnuView.SubMenu.Items.Add(main.mnuViewByType);
            //    mnuView.SubMenu.Items.Add(main.mnuViewByDate);
            //    mnuView.SubMenu.Items.Add(main.mnuViewBySize);
            //    mnuView.SubMenu.Items.Add(new Xwt.SeparatorMenuItem());
            //    mnuView.SubMenu.Items.Add(main.mnuViewNoFilter);
            //    mnuView.SubMenu.Items.Add(main.mnuViewWithFilter);
            //    mnuView.SubMenu.Items.Add(new Xwt.SeparatorMenuItem());
            //    mnuView.SubMenu.Items.Add(main.mnuViewKeybrdHelp); //these checkboxes don't work, because no code was written
            //    mnuView.SubMenu.Items.Add(main.mnuViewInfobar);
            //    mnuView.SubMenu.Items.Add(main.mnuViewDiskButtons);

            //    var mnuNavigate = main.mnuNavigate;
            //    mnuNavigate.SubMenu = new Xwt.Menu();
            //    mnuNavigate.SubMenu.Items.Add(main.mnuNavigateTree);
            //    mnuNavigate.SubMenu.Items.Add(main.mnuNavigateHistory);
            //    mnuNavigate.SubMenu.Items.Add(main.mnuNavigateFind);
            //    mnuNavigate.SubMenu.Items.Add(main.mnuNavigateReload);

            //    var mnuTools = main.mnuTools;
            //    mnuTools.SubMenu = new Xwt.Menu();
            //    mnuTools.SubMenu.Items.Add(main.mnuToolsOptions);
            //    mnuTools.SubMenu.Items.Add(main.mnuToolsPluginManager);
            //    mnuTools.SubMenu.Items.Add(main.mnuToolsEditUserMenu);
            //    mnuTools.SubMenu.Items.Add(main.mnuToolsKeychains);
            //    mnuTools.SubMenu.Items.Add(main.mnuToolsConfigEdit);
            //    mnuTools.SubMenu.Items.Add(new Xwt.SeparatorMenuItem());
            //    mnuTools.SubMenu.Items.Add(main.mnuToolsDiskLabel);
            //    mnuTools.SubMenu.Items.Add(main.mnuToolsFormat);
            //    mnuTools.SubMenu.Items.Add(main.mnuToolsSysInfo);

            //    var mnuHelp = main.mnuHelp;
            //    mnuHelp.SubMenu = new Xwt.Menu();
            //    mnuHelp.SubMenu.Items.Add(main.mnuHelpHelpMe);
            //    mnuHelp.SubMenu.Items.Add(main.mnuHelpDebug);
            //    mnuHelp.SubMenu.Items.Add(main.mnuHelpAbout);

        }

        public void Localize()
        {
            //main.WindowData.TranslateMenu();
            //     // main.WindowData.M  .DataContext as CommanderData).TranslateMenu(main.MainMenu);

            //    for (int i = 1; i < 11; i++)
            //    {
            //        main.KeybHelpButtons[i].FKey = "F" + i;
            //        main.KeybHelpButtons[i].Text = Localizator.GetString("FCF" + i);
            //    }

            //    var LVCols = main.LVCols;
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

            //    main.p1.ListingView.SetColumns(LVCols);
            //    main.p2.ListingView.SetColumns(LVCols);
        }

        public void KeyEvent(object sender, System.Windows.Input.KeyEventArgs e)
        {
            var ActivePanel = main.ActivePanelWpf;
            var PassivePanel = main.PassivePanelWpf;

            string URL1;
            if (ActivePanel.ListingView.SelectedRow > -1)
            {
                URL1 = ActivePanel.GetValue(ActivePanel.df.URL);
            }
            else
            {
                URL1 = null;
            }
            pluginner.IFSPlugin FS1 = ActivePanel.FS;

            /* string URL2;
            if (PassivePanel.ListingView.SelectedRow > -1)
            { URL2 = PassivePanel.GetValue(PassivePanel.df.URL); }
            else
            { URL2 = null; }
            pluginner.IFSPlugin FS2 = PassivePanel.FS;
			*/

            switch (e.Key)
            {
                // case Xwt.Key.NumPadAdd: //[+] gray - add selection
                case Key.OemPlus:
                    string Filter = @"*.*";

                    InputBox ibx_qs = new InputBox(Localizator.GetString("QuickSelect"), Filter);
                    Xwt.CheckBox chkRegExp = new Xwt.CheckBox(Localizator.GetString("NameFilterUseRegExp"));
                    //ibx_qs.OtherWidgets.Add(chkRegExp, 0, 0);
                    if (!ibx_qs.ShowDialog())
                    {
                        e.Handled = false;
                        return;
                    }

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

                    e.Handled = true;
                    return;

                // case Xwt.Key.NumPadSubtract: //[-] gray - add selection
                case Key.OemMinus:
                    string Filter_qus = @"*.*";

                    InputBox ibx_qus = new InputBox(Localizator.GetString("QuickUnselect"), Filter_qus);
                    Xwt.CheckBox chkRegExp_qus = new Xwt.CheckBox(Localizator.GetString("NameFilterUseRegExp"));
                    //ibx_qus.OtherWidgets.Add(chkRegExp_qus, 0, 0);
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
                    if (URL1 == null)
                        return;

                    if (!FS1.FileExists(URL1))
                    {
                        Xwt.MessageDialog.ShowWarning(string.Format(Localizator.GetString("FileNotFound"), ActivePanel.GetValue(ActivePanel.df.DisplayName)));
                        return;
                    }

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

                    //   TODO: handle Ctrl+F3 (Sort by name).
                    return;

                // case Xwt.Key.F4: //F4: Edit. Shift+F4: Edit as txt.
                case Key.F4:

                    if (URL1 == null)
                        return;

                    if (!FS1.FileExists(URL1))
                    {
                        Xwt.MessageDialog.ShowWarning(string.Format(Localizator.GetString("FileNotFound"), ActivePanel.GetValue(ActivePanel.df.DisplayName)));
                        return;
                    }

                    VEd E = new VEd();
                    //if (e.Modifiers == Xwt.ModifierKeys.None)
                    //{ E.LoadFile(URL1, FS1, true); E.Show(); }
                    //else if (e.Modifiers == Xwt.ModifierKeys.Shift)
                    //{ E.LoadFile(URL1, FS1, new base_plugins.ve.PlainText(), true); E.Show(); }
                    //todo: handle Ctrl+F4 (Sort by extension).

                    e.Handled = true;
                    return;

                case Key.F5: //F5: Copy.
                    if (URL1 == null)
                        return;
                    main.Cp();

                    //todo: handle Ctrl+F5 (Sort by timestamp).
                    return;

                case Key.F6: //F6: Move/Rename.
                    if (URL1 == null)
                        return;
                    main.Mv();
                    //todo: handle Ctrl+F6 (Sort by size).
                    return;

                case Key.F7: //F7: New directory.

                    InputBox ibx = new InputBox(Localizator.GetString("NewDirURL"), ActivePanel.FS.CurrentDirectory + Localizator.GetString("NewDirTemplate"));
                    if (ibx.ShowDialog())
                        main.MkDir(ibx.Result);
                    return;

                case Key.F8: //F8: delete
                    if (URL1 == null)
                        return;
                    main.Rm();
                    //todo: move to trash can/recycle bin & handle Shit+F8 (remove completely)
                    return;

                case Key.F10: //F10: Exit
                              //todo: ask user, are it really want to close FC?
                              // Xwt.Application.Exit();
                    System.Windows.Application.Current.Shutdown();
                    //todo: handle Alt+F10 (directory tree)
                    return;
            }
        }

    }
}
