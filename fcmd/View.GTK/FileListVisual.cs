using System;
using System.Collections.Generic;
using System.IO;
using pluginner.Widgets;
using Xwt;
using pluginner;
using fcmd.View.GTK.Ctrl;

namespace fcmd.View.GTK
{

    // GTK visual
    public abstract class FileListVisual<T> : FileListPanel<T> where T : class, IListView2Visual
    {
        #region Properties

        public override IButton GoRoot { get; protected set; }
        public override IButton GoUp { get; protected set; }
        public override ITextEntryGtk UrlBox { get; protected set; }

        protected EventHandler goRootDelegate = null;
        protected EventHandler goUpDelegate = null;

        //public MenuButton BookmarksButton = new MenuButton(Image.FromResource("fcmd.Resources.bookmarks.png"));
        //public MenuButton HistoryButton = new MenuButton(Image.FromResource("fcmd.Resources.history.png"));

        public LightScroller DiskBox = new LightScroller();
        public HBox DiskList = new HBox();
        public List<Button> DiskButtons = new List<Button>();

        public HBox QuickSearchBox = new HBox();
        public TextEntry QuickSearchText = new TextEntry();//по возможность заменить на SearchTextEntry (не раб. на wpf, see xwt bug 330)
        public Table StatusTable = new Table();

        public ProgressBar StatusProgressbar = new ProgressBar();
        TextEntry CLIoutput = new TextEntry { MultiLine = true, ShowFrame = true, Visible = false, HeightRequest = 50 };
        TextEntry CLIprompt = new TextEntry();

        /// <summary>User navigates into another directory</summary>
        public override event TypedEvent<string> Navigate;
        /// <summary>User tried to open the highlighted file</summary>
        public override event TypedEvent<string> OpenFile;

        protected string SBtext1, SBtext2;
        // private Stylist s;

        #endregion

        #region ctor

        /// <summary>Initialize the FLP</summary>
        /// <param name="BookmarkXML">The bookmark database</param>
        /// <param name="CSS">The user theme (or null if it's need to use internal theme)</param>
        /// <param name="InfobarText1">The mask for infobar text when a file is selected</param>
        /// <param name="InfobarText2">The mask for infobar text when no files are selected</param>
        public FileListVisual(string BookmarkXML = null, string CSS = null,
            string InfobarText1 = "{Name}", string InfobarText2 = "F: {FileS}, D: {DirS}")
        {
            //StatusBar = new CommanderStatusBar() { Content = ("Information bar") };
            //// StatusBar.Visibility = System.Windows.Visibility.Hidden;
            //StatusBar.Visible = true;

            SBtext1 = InfobarText1;
            SBtext2 = InfobarText2;
            this.BookmarkXML = BookmarkXML;
            // -> Initialize();
        }

        protected string BookmarkXML;
        protected void PostInitialize()
        {
            // s = new Stylist(CSS);
            BuildUI(BookmarkXML);

            GoRoot.CanGetFocus = true; // TODO: GoUp.CanGetFocus = BookmarksButton.CanGetFocus = HistoryButton.CanGetFocus = false;
            string fontFamily = fcmd.Properties.Settings.Default.UserFileListFontFamily;

            //ListingView.FontForFileNames = String.IsNullOrWhiteSpace(fontFamily) ? FontWpf.SystemFont : FontWpf.FromName(fontFamily);
            // DiskBox.Add ...
        }

        #endregion

        #region Events, Methods

        public void Focused(ButtonEventArgs ea)
        {
            // base.OnGotFocus(ea);
        }

        protected void QuickSearchText_KeyPressed(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Escape)
            //{
            //    QuickSearchText.Text = "";
            //    QuickSearchBox.Visible = false;
            //    ListingView.AllowedToPoint.Clear();
            //    return;
            //}

            ////search for good items
            //ListingView.Sensitive = false;
            //ListingView.AllowedToPoint.Clear();
            //foreach (ListView2Item lvi in ListingView.Items)
            //{
            //    if (lvi.Data[1].ToString().StartsWith(QuickSearchText.Text))
            //    {
            //        ListingView.AllowedToPoint.Add(lvi.RowNo);
            //    }
            //}
            //ListingView.Sensitive = true;

            ////set pointer to the first good item (if need)
            //if (ListingView.AllowedToPoint.Count > 0)
            //{
            //    if (ListingView.SelectedRow < ListingView.AllowedToPoint[0]
            //        ||
            //        ListingView.SelectedRow > ListingView.AllowedToPoint[ListingView.AllowedToPoint.Count - 1]
            //        )
            //    {
            //        ListingView.SelectedRow = ListingView.AllowedToPoint[0];
            //        ListingView.ScrollToRow(ListingView.AllowedToPoint[0]);
            //    }
            //}
        }

        protected void CLIprompt_KeyReleased(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                //if (Regex.Match(CLIprompt.Text, "cd|chdir|md|rd|del|deltree|move|copy|cls").Success)
                //{
                //    CLIprompt.Text = "";
                //    //todo: обработка встроенных комманд
                //    return;
                //}

                //CLIoutput.Visible = true;
                //string stdin = CLIprompt.Text;
                //CLIprompt.Text = "";
                //CLIoutput.Text += stdin;
                //FS.CLIstdinWriteLine(stdin);
            }
        }

        protected void FS_CLIpromptChanged(string data)
        {
            //CLIprompt.PlaceholderText = data;
        }

        protected void FS_CLIstdoutDataReceived(string data)
        {
            Xwt.Application.Invoke(delegate
            {
                //CLIoutput.Text += "\n" + data;
            });
        }

        #endregion

        #region ListingView 

        /// <summary>Make the panel's widgets</summary>
        /// <param name="BookmarkXML">Bookmark list XML data</param>
        public void BuildUI(string BookmarkXML = null)
        {
            //URL BOX

            //UrlBox.ShowFrame = false;

            //UrlBox.GotFocus += (o, ea) => { OnGotFocus(ea); };
            //UrlBox.KeyReleased += UrlBox_KeyReleased;

            //BookmarkTools bmt = new BookmarkTools(BookmarkXML, "QuickAccessBar");
            //bmt.DisplayBookmarks(
            //    DiskList,
            //    (url => NavigateTo(url)),
            //    s
            //);

            //bmt = new BookmarkTools(BookmarkXML);
            //BookmarksButton.Menu = new Xwt.Menu();
            //bmt.DisplayBookmarks(
            //    BookmarksButton.Menu,
            //    (url => NavigateTo(url))
            //);

            //foreach (Button b in DiskButtons)
            //{
            //    s.Stylize(b);
            //}
            //s.Stylize(DiskBox);
            //s.Stylize(UrlBox);
            //s.Stylize(ListingView);
            //s.Stylize(QuickSearchBox);
            //s.Stylize(CLIoutput, "TerminalOutput");
            //s.Stylize(CLIprompt, "TerminalPrompt");
            //s.Stylize(StatusTable);

            //ListingView.KeyReleased += ListingView_KeyReleased;
            //ListingView.GotFocus += (o, ea) =>
            //    OnGotFocus(ea);

            //ListingView.PointerMoved += ListingView_PointerMoved;
            //ListingView.SelectionChanged += ListingView_SelectionChanged;
            //ListingView.PointedItemDoubleClicked += pointed_item => { OpenPointedItem(); };
            //ListingView.EditComplete += ListingView_EditComplete;
            //StatusBar.Wrap = WrapMode.Word;

        }

        //void ListingView_EditComplete(EditableLabel el, ListView2 lv)
        //{
        //    string Url1 = FS.CurrentDirectory + FS.DirSeparator + ListingView.PointedItem.Data[df.DisplayName];
        //    string Url2 = FS.CurrentDirectory + FS.DirSeparator + el.Text;
        //    try
        //    {
        //        if (FS.DirectoryExists(Url1))
        //            FS.MoveDirectory(Url1, Url2);
        //        else
        //            FS.MoveFile(Url1, Url2);
        //        StatusBar.Text = ListingView.PointedItem.Data[df.DisplayName] + " → " + el.Text;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageDialog.ShowWarning(ex.Message);
        //        el.Text = ListingView.PointedItem.Data[df.DisplayName].ToString();
        //    }
        //}

        //void ListingView_SelectionChanged(List<ListView2Item> data)
        //{
        //    WriteDefaultStatusLabel();
        //}

        //void ListingView_PointerMoved(ListView2Item data)
        //{
        //    WriteDefaultStatusLabel();
        //}

        void UrlBox_KeyReleased(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Return)
            //{
            //    LoadDir(UrlBox.Text);
            //}
        }

        void ListingView_KeyReleased(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Return && ListingView.SelectedRow > -1)
            //{
            //    OpenPointedItem();
            //    return;
            //}

            //if ((int)e.Key < 65000) //keys before 65000th are characters, numbers & other human stuff
            //{
            //    QuickSearchText.Text += e.Key.ToString();
            //    QuickSearchBox.Visible = true;
            //    QuickSearchText.SetFocus();
            //    return;
            //}
            //if (Utilities.GetXwtBackendName() == "WPF")
            //    ListingView.OnKeyPressed(e);
        }

        void OpenPointedItem()
        {
            //NavigateTo(ListingView.PointedItem.Data[df.URL].ToString());
        }

        /// <summary>Open the FS item at <paramref name="url"/> (if it's file, load; if it's directory, go to)</summary>
        /// <param name="url">The URL of the filesystem entry</param>
        /// <param name="ClearHistory">The number of the history entrie after that all entries should be removed</param>
        protected void NavigateTo(string url, int? ClearHistory = null)
        {
            if (!url.Contains("://"))
            {
                //the path is relative
                NavigateTo(FS.CurrentDirectory + FS.DirSeparator + url);
            }

            //var hm = HistoryButton.Menu;

            //if (ClearHistory == null)
            //{
            //    //register current directory in history
            //    MenuItem hmi = new MenuItem(url);
            //    hmi.Clicked += (o, ea) => { NavigateTo(url, (int)hmi.Tag); };
            //    hmi.Tag = hm.Items.Count;
            //    hm.Items.Add(hmi);
            //}
            //if (ClearHistory != null)
            //{
            //    //loading from history menu, thus don't making duplicates.
            //}


            try
            {
                if (FS.DirectoryExists(url))
                {//it's directory
                    var navigate = Navigate;
                    if (navigate != null)
                    {
                        navigate(url); //raise event
                    }
                    else
                    {
                        Console.WriteLine("WARNING: the event FLP.Navigate was not handled by the host");
                    }

                    LoadDir(url);
                    return;
                }
                else
                {//it's file
                    var openFile = OpenFile;
                    if (openFile != null)
                    {
                        openFile(url); //raise event
                    }
                    else
                    {
                        Console.WriteLine("WARNING: the event FLP.OpenFile was not handled by the host");
                    }
                }
            }
            catch (PleaseSwitchPluginException)
            {
                throw; //delegate authority to the mainwindow (it is it's jurisdiction).
            }
            catch (Exception ex)
            {
                ListingWidget.Sensitive = true;
                // ListingWidget.Cursor = CursorType.Arrow;

                View.MessageDialog.ShowError(ex.Message);
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
                WriteDefaultStatusLabel();
            }
        }

        #endregion

        #region FS

        public virtual void OnFocus()
        {
#if XWT
            base.OnGotFocus(EventArgs.Empty);
#else
        if (this.onFocusSet)
            this.onFocus(null, EventArgs.Empty);
#endif
        }

        // public abstract void LoadFs(string URL, ShortenPolicies Shorten);

        protected void FS_StatusChanged(string data)
        {
            Xwt.Application.Invoke(delegate ()
            {
                if (data.Length == 0)
                    WriteDefaultStatusLabel();
                //else
                //    StatusBar.Text = data;
            }
            );
        }

        protected void FS_ProgressChanged(double data)
        {
            Xwt.Application.Invoke(delegate ()
            {
                //if (data > 0 && data <= 1)
                //{
                //    StatusProgressbar.Visible = true;
                //    StatusProgressbar.Fraction = data;
                //}
                //else
                //{
                //    StatusProgressbar.Visible = false;
                //}
            });
        }

        /// <summary>
        /// Reloads the current directory
        /// </summary>
        public override void LoadDir()
        {
            LoadDir(FS.CurrentDirectory);
        }

        /// <summary>
        /// Load the directory into the panel
        /// </summary>
        /// <param name="URL">Full path of the directory</param>
        public void LoadDir(string URL)
        {
            LoadDir(URL, ShortenPolicy);
        }

        #endregion

        /// <summary>
        /// Gets the selected row's value from the column №<paramref name="Field"/>
        /// </summary>
        /// <typeparam name="T">The type of the data</typeparam>
        /// <param name="Field">The field number</param>
        /// <returns>The value</returns>
        public override TItem GetValue<TItem>(int Field)
        {
            return default(TItem); //return (T)ListingView.PointedItem.Data[Field];
        }

        public override string GetValue(int Field)
        {
            return string.Empty; //return (string)ListingView.PointedItem.Data[Field];
        }

        #region Drives, Mounts

        /// <summary>Add autobookmark "system disks" onto disk toolbar</summary>
        protected void AddSysDrives()
        {
            foreach (DriveInfo di in DriveInfo.GetDrives())
            {
                string d = di.Name;
                //Xwt.ButtonWidget NewBtn = new Xwt.ButtonWidget(null, d);

                //NewBtn.Clicked += (o, ea) => { NavigateTo("file://" + d); };
                //NewBtn.CanGetFocus = false;
                ////NewBtn.Style = ButtonStyle.Flat;
                //NewBtn.Margin = -3;
                //NewBtn.Cursor = CursorType.Hand;
                //NewBtn.Sensitive = di.IsReady;
                //if (di.IsReady)
                //{
                //    NewBtn.TooltipText = di.VolumeLabel + " (" + di.DriveFormat + ")";
                //}

                /* todo: rewrite the code; possibly change the XWT to allow
                 * change the internal padding of the button.
                 */
                //switch (di.DriveType)
                //{
                //    case DriveType.Fixed:
                //        NewBtn.Image = Image.FromResource(GetType(), "fcmd.Resources.drive-harddisk.png");
                //        break;
                //    case DriveType.CDRom:
                //        NewBtn.Image = Image.FromResource(GetType(), "fcmd.Resources.drive-optical.png");
                //        break;
                //    case DriveType.Removable:
                //        NewBtn.Image = Image.FromResource(GetType(), "fcmd.Resources.drive-removable-media.png");
                //        break;
                //    case DriveType.Network:
                //        NewBtn.Image = Image.FromResource(GetType(), "fcmd.Resources.network-server.png");
                //        break;
                //    case DriveType.Ram:
                //        NewBtn.Image = Image.FromResource(GetType(), "fcmd.Resources.emblem-system.png");
                //        break;
                //    case DriveType.Unknown:
                //        NewBtn.Image = Image.FromResource(GetType(), "fcmd.Resources.image-missing.png");
                //        break;
                //}

                ////OS-specific icons
                //if (d.StartsWith("A:")) NewBtn.Image = Image.FromResource(GetType(), "fcmd.Resources.media-floppy.png");
                //if (d.StartsWith("B:")) NewBtn.Image = Image.FromResource(GetType(), "fcmd.Resources.media-floppy.png");
                //if (d.StartsWith("/dev")) NewBtn.Image = Image.FromResource(GetType(), "fcmd.Resources.preferences-desktop-peripherals.png");
                //if (d.StartsWith("/proc")) NewBtn.Image = Image.FromResource(GetType(), "fcmd.Resources.emblem-system.png");
                //if (d == "/") NewBtn.Image = Image.FromResource(GetType(), "fcmd.Resources.root-folder.png");

                //s.Stylize(NewBtn);
                //DiskList.PackStart(NewBtn);
            }
        }

        /// <summary>Add buttons of mounted medias (*nix)</summary>
        protected void AddLinuxMounts()
        {
            if (Directory.Exists(@"/mnt"))
            {
                foreach (string dir in Directory.GetDirectories(@"/mnt/"))
                {
                    //var NewBtn = new ButtonWidget(null, dir.Replace("/mnt/", ""));
                    //NewBtn.Clicked += (o, ea) => { NavigateTo("file://" + dir); };
                    //NewBtn.CanGetFocus = false;
                    //NewBtn.Style = ButtonStyle.Flat;
                    //NewBtn.Margin = -3;
                    //NewBtn.Cursor = CursorType.Hand;
                    //NewBtn.Image = Image.FromResource(GetType(), "fcmd.Resources.drive-removable-media.png");

                    //s.Stylize(NewBtn);
                    //DiskList.PackStart(NewBtn);
                }
            }
            else AddSysDrives(); //fallback for Windows
        }

        #endregion

        /// <summary>
        /// Writes to statusbar the default text
        /// </summary>
        protected abstract void WriteDefaultStatusLabel();
        protected abstract string MakeStatusbarText(string Template);

    }
}
