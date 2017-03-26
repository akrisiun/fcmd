using fcmd.View.ctrl;
using pluginner.Widgets;
using System;
using System.Diagnostics;
using fcmd.View;
using fcmd.Controller;
using pluginner;
using System.IO;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Threading;
using System.Collections.ObjectModel;
using fcmd.Model;
using fs = fcmd.base_plugins.fs;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Input;
using fcmd.base_plugins.fs;

namespace fcmd.View.Xaml
{
    public class FileListPanelWpf : FileListPanel<ListItemXaml>
    {
        #region Data

        public PanelSide Side { get { return ListingViewWpf.Side; } }
        public PanelWpf Parent { get; set; }
        public WindowDataWpf WindowData { [DebuggerStepThrough] get; set; }

        public override IButton GoRoot { get; protected set; }
        public override IButton GoUp { get; protected set; }
        public override ITextEntry UrlBox { get { return Parent.path; } }

        public override string ToString()
        {
            return String.Format("side {0} Url={1}", Side, Parent.path);
        }

        protected EventHandler onFocus;
        protected bool onFocusSet;

#pragma warning disable 0649, 0414, 0067  // is assigned but never used
        public override event TypedEvent<string> Navigate;
        public override event TypedEvent<string> OpenFile;
#pragma warning restore 0649, 0414

        public override event EventHandler GotFocus { add { onFocusSet = true; onFocus += value; } remove { onFocus += value; } }

        #endregion

        // original constructor
        // public FileListPanelXaml(string BookmarkXML = null, string CSS = null,
        //    string InfobarText1 = "{Name}", string InfobarText2 = "F: {FileS}, D: {DirS}")

        public FileListPanelWpf(PanelWpf parent)
        {
            Parent = parent;
        }

        #region Properties

        public ListView2DataGrid ListingWidget { get { return ListingViewWpf; } }
        private ListView2DataGrid ListingViewWpf;

        public override IListingView<ListItemXaml> ListingView { get { return ListingViewWpf.DataObj as ListFiltered2Xaml; } }

        public override void Initialize(PanelSide side)
        {
            if (df == null)
            {
                df = DataFieldNumbers.Default();
                onFocus = new EventHandler(Focused);
                GoUp = Parent.cdUp;
                GoRoot = Parent.cdRoot;
            }

            ListingViewWpf = Parent.data;
            if (ListingViewWpf == null)
                return;

            ListingViewWpf.Panel = Parent as Xaml.PanelWpf;
            ListingViewWpf.FileList = this;

            ListingViewWpf.Side = side;
        }

        public void Focused(object sender, EventArgs e)
        {
            var side = Parent.Side;
            if (WindowData != null)
                WindowData.OnSideFocus(side);

            ListingWidget.Bind();   // additional bind (after loading error)
        }

        // ExpandoObject data
        public override TItem GetValue<TItem>(int Field)
        {
            if (ListingView.PointedItem.Data == null)
                return default(TItem);

            return (TItem)ListingView.PointedItem.Data[Field];
        }

        public override string GetValue(int Field)
        {
            return (string)ListingView.PointedItem.Data[Field];
        }
        #endregion

        public void BuildUI(string BookmarkXML = null) { }

        #region StatusBar

        protected void WriteDefaultStatusLabel()
        {
            //StatusProgressbar.Visible = false;
            //if (ListingView.SelectedItems.Count < 1)
            //    StatusBar.Text = MakeStatusbarText(SBtext1);
            //else
            //    StatusBar.Text = MakeStatusbarText(SBtext2);
        }

        protected string MakeStatusbarText(string Template)
        {
            //string txt = Template;
            //if (ListingView.PointedItem != null)
            //{
            //    DirItem di = (DirItem)ListingView.PointedItem.Data[df.DirItem];
            //    txt = txt.Replace("{FullName}", di.TextToShow);
            //    txt = txt.Replace("{AutoSize}", di.Size.KiloMegaGigabyteConvert(Shorten.KB, Shorten.MB, Shorten.GB)); // CurShortenKB, CurShortenMB, CurShortenMB));
            //    txt = txt.Replace("{Date}", di.Date.ToShortDateString());
            //    txt = txt.Replace("{Time}", di.Date.ToLocalTime().ToShortTimeString());
            //    txt = txt.Replace("{SelectedItems}", ListingView.SelectedItems.Count.ToString());
            //    //todo: add masks SizeB, SizeKB, SizeMB, TimeUTC, Name, Extension
            //}
            //return txt;
            return string.Empty;
        }

        #endregion

        #region Load

        public override void LoadFs(string URL, ShortenPolicies Shorten)
        {
            bool checkAccess = (UrlBox as DispatcherObject).CheckAccess();
            if (checkAccess)
                UrlBox.Text = URL;

            string updir = URL + FS.DirSeparator + "..";
            string rootdir = FS.GetMetadata(URL).RootDirectory;
            FS.RootDirectory = FS.Prefix + FS.NoPrefix(rootdir);

            var resetHandle = new AutoResetEvent(false);
            var lv = ListingView as ListFiltered2Xaml;

            Thread DirLoadingThread =
                new Thread(delegate()
                {
                    IEnumerable<DirItem> dis;
                    if (FS is fs.LocalFileSystem)
                    {
                        var fsLoc = FS as fs.LocalFileSystem;
                        dis = fsLoc.GetDirectoryContent(new FileSystemOperationStatus());
                    }
                    else
                        dis = FS.GetDirectoryContent(new FileSystemOperationStatus());

                    var num = dis.GetEnumerator();
                    if (num != null && num.MoveNext())     // if first item found
                    {
                        // after first item
                        if (lv.Count > 0)
                            lv.ClearData(); // origin clear

                        FS.CurrentDirectory = Environment.CurrentDirectory;
                        var url = FS.CurrentDirectory;
                        do
                        {
                            DirItem di = num.Current;
                            object[] data = new object[idxCOUNT];
                            FillItem(ref data, di, Shorten);

                            lv.AddData(data);
                        }
                        while (num.MoveNext());
                    }
                    resetHandle.Set();
                });

            DirLoadingThread.Start();
            var success = resetHandle.WaitOne(timeout: TimeSpan.FromSeconds(10)); // 10 secs

            lv.Finish();

            var finalURL = FS.CurrentDirectory;
            if (checkAccess)
                UrlBox.Text = finalURL;
        }

        public void LoadPluginFs(IFSPlugin plugin, string URL, Action then = null)
        {
            FS = plugin;
            var lv = ListingView as ListFiltered2Xaml;
            if (lv == null || plugin.DirectoryContent == null)
                return;

            var num = plugin.DirectoryContent.GetEnumerator();
            do
            {
                DirItem di = num.Current;
                object[] data = new object[idxCOUNT];
                FillItem(ref data, di, ShortenPolicies.Empty);

                lv.AddData(data);
            }
            while (num.MoveNext());

            lv.Finish();

            var finalURL = FS.CurrentDirectory;
            if (UrlBox.CheckAccess())
            {
                UrlBox.Text = finalURL;
                if (then != null)
                    then();
            }
        }

        void FillItem(ref object[] Data, DirItem di, ShortenPolicies Shorten)
        {
            SizeDisplayPolicy ShortenKB = Shorten.KB;
            SizeDisplayPolicy ShortenMB = Shorten.MB;
            SizeDisplayPolicy ShortenGB = Shorten.GB;

            // Data[]: URL, Name, Size, DateTime, IsDirectory, (optional #6 DirItem)
            Data[idxUrl] = di.URL;
            Data[idxName] = di.TextToShow;

            if (di.TextToShow == "..")
            { //parent dir
                Data[idxSize] = "<↑ UP>";
                Data[idxDatetime] = FS.GetMetadata(di.URL).LastWriteTimeUTC.ToLocalTime();
                Data[idxIsDirectory] = true;
                Data[idxSizeBytes] = (Int64)0;
            }
            else if (di.IsDirectory)
            {//dir
                Data[idxSize] = "<DIR>";
                Data[idxDatetime] = di.Date;
                Data[idxIsDirectory] = true;
                Data[idxSizeBytes] = (Int64)0;
            }
            else
            {
                Data[idxSize] = di.Size.KiloMegaGigabyteConvert(ShortenKB, ShortenMB, ShortenGB);
                Data[idxDatetime] = di.Date;
                Data[idxIsDirectory] = false;
                Data[idxSizeBytes] = (Int64)di.Size;
            }
        }

        public const int idxUrl = 0;
        public const int idxName = 1;
        public const int idxSize = 2;
        public const int idxDirLabel = 2;

        public const int idxDatetime = 3;
        public const int idxIsDirectory = 4;
        public const int idxSizeBytes = 5;

        // public const int idxDI = 5;
        public const int idxCOUNT = 6;

        Tuple<string, object[], IEnumerable<bool>> AddItem(DirItem di, ShortenPolicies Shorten)
        {
            SizeDisplayPolicy ShortenKB = Shorten.KB;
            SizeDisplayPolicy ShortenMB = Shorten.MB;
            SizeDisplayPolicy ShortenGB = Shorten.GB;

            // ICollection<object> Data = new Collection<Object>();
            object[] Data = new object[idxCOUNT];

            List<Boolean> EditableFields = new List<bool>();
            //Data.Add(di.IconSmall ?? Image.FromResource("fcmd.Resources.image-missing.png"));

            EditableFields.Add(false);
            Data[idxUrl] = (di.URL); EditableFields.Add(false);
            Data[idxName] = (di.TextToShow); EditableFields.Add(true);

            if (di.TextToShow == "..")
            {//parent dir

                Data[idxDirLabel] = ("<↑ UP>"); EditableFields.Add(false);
                EditableFields[2] = false;
                Data[idxDatetime] = FS.GetMetadata(di.URL).LastWriteTimeUTC.ToLocalTime();
                Data[idxSizeBytes] = (Int64)0;

                EditableFields.Add(false);
                // updir = di.URL;
                // di.IsDirectory = true;
            }
            else if (di.IsDirectory)
            {//dir
                Data[idxDirLabel] = ("<DIR>"); EditableFields.Add(false);
                Data[idxSizeBytes] = (Int64)0;
                Data[idxDatetime] = (di.Date); EditableFields.Add(false);
            }
            else
            {//file
                var size = di.Size.KiloMegaGigabyteConvert(
                        ShortenKB, ShortenMB, ShortenGB);
                Data[idxSize] = (size);
                Data[idxSizeBytes] = (Int64)di.Size;

                EditableFields.Add(false);
                Data[idxDatetime] = (di.Date); EditableFields.Add(false);
            }

            Data[idxIsDirectory] = (di.IsDirectory);
            // Data[idxSize] = (di);

            object[] array = Data;
            //new object[Data.Count]; 
            //Data.CopyTo(array, 0);
            return new Tuple<string, object[], IEnumerable<bool>>(di.TextToShow, array, EditableFields);
        }

        /// <summary>
        /// Load the specifed directory with specifed content into the panel and set view options
        /// </summary>
        /// <param name="URL">The full URL of the directory (for reference needs)</param>
        /// <param name="ShortenKB">How kilobyte sizes should be humanized</param>
        /// <param name="ShortenMB">How megabyte sizes should be humanized</param>
        /// <param name="ShortenGB">How gigabyte sizes should be humanized</param> //плохой перевод? "так nбайтные размеры должны очеловечиваться"
        public override void LoadDir(string URL, ShortenPolicies? Shorten = null)
        {
            ShortenPolicy = Shorten ?? ShortenPolicy;

            if (FS == null) throw new InvalidOperationException("No filesystem is binded to this FileListPanel");

            if (FS.CurrentDirectory == null)
            {
                //TODO History

                //if this is first call in the session (the FLP is just initialized)
                //using (Xwt.Menu hm = HistoryButton.Menu)
                //{
                //    MenuItem hmi = new MenuItem(URL);
                //    hmi.Clicked += (o, ea) => { NavigateTo(URL, (int)hmi.Tag); };
                //    hmi.Tag = hm.Items.Count;
                //    hm.Items.Add(hmi);
                //}
                //FS.StatusChanged += FS_StatusChanged;
                //FS.ProgressChanged += FS_ProgressChanged;
            }

            if (URL == "." && FS.CurrentDirectory == null)
            {
                LoadDir(fs.LocalFileSystem.FilePrefix + Directory.GetCurrentDirectory(), Shorten);
                return;
            }

            string dir = FS.NoPrefix(URL);
            int pos = dir.IndexOf(":");
            if (pos == 1)
                DirectorySafe.SetCurrentDirectory(dir);

            ListingWidget.Sensitive = false;

            if (UrlBox.CheckAccess())
                UrlBox.Text = URL;

            LoadFsWithPlugin(URL);

            ListingWidget.Sensitive = true;
        }

        public virtual void LoadDirThen(string URL, ShortenPolicies? Shorten = null, Action then = null)
        {
            LoadDir(URL, Shorten);

            if (this.FS.LastError != null)
            {
                fcmd.App.BackendWpf.ShowError(FS.LastError);
                return;
            }

            ListBindThen(URL, then);
        }

        public virtual void SelectItem(string folder)
        {
            if (folder == null || folder.Length == 0)
                return;

            string itemName = Path.GetFileName(folder);
            ListItemXaml foundItem = null;

            var view = ListingView;
            var numData = view.DataItems.GetEnumerator();
            while (numData.MoveNext())
            {
                var item = numData.Current;

                if (item.fldFile == itemName)
                {
                    foundItem = item;
                    break;
                }
            }

            if (foundItem == null)
                return;

#if WPF
            DataGrid dataGrid = this.ListingViewWpf.DataObj.DataSource;
            // var item0 = dataGrid.Items[0];

            dataGrid.ScrollIntoView(foundItem);
            dataGrid.SelectedItem = foundItem;
            if (dataGrid.SelectedItem == null) return; // error

            DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromItem(foundItem) as DataGridRow; // .ContainerFromIndex(i);
            if (row != null)
                row.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));

            return;
#endif
        }

        public virtual void LoadFSThen(string URL, pluginner.IFSPlugin fs, Action then = null)
        {
            FS = fs;
            ListBindThen(URL, then);
        }

        public void ListBindThen(string URL, Action then = null)
        {
            var view = ListingView;
            if (!view.ColumnsSet && (Parent as DispatcherObject).CheckAccess())
            {
                App.ConsoleWriteLine("Dispacher UI " + URL);

                if (view.DataItems.Count > 0)
                {
                    view.SetupColumns();
                    view.SelectedRow = 0;
                }

                if (then != null)
                    then();

                this.Parent.Update();
                view.SetFocus();
                App.ConsoleWriteLine("Dispacher UI Focused " + URL);
            }
            else
            {
                if (then != null)
                    then();
            }
        }

        protected void LoadFsWithPlugin(string URL)
        {

            string oldCurDir = FS.CurrentDirectory;

            bool loadPlugin = false;

            if (!URL.Contains("://"))
                URL = FS.Prefix + FS.NoPrefix(URL);

            try
            {
                FS.CurrentDirectory = URL;
                // first try
                LoadFs(URL, ShortenPolicy);
            }
            catch (Exception ex)
            {
                if (ex is pluginner.PleaseSwitchPluginException)
                {
                    loadPlugin = true;
                }
                else if (ex is NullReferenceException || ex.InnerException != null)
                {
                    MessageDialog.ShowWarning(ex.Message,
                        ex.StackTrace
                        + (ex.InnerException != null ? "\nInner exception: " + ex.InnerException.Message ?? "none" : ""));
                }
                else
                {
                    MessageDialog.ShowWarning(ex.Message);
                }
            }

            if (loadPlugin)
            {
                // second chance: with plugin
                try
                {
                    pluginfinder pf = new pluginfinder();
                    FS = pf.GetFSplugin(URL);
                    LoadDir(URL, ShortenPolicy);
                }
                catch (Exception ex)
                {
                    if (ex is NullReferenceException || ex.InnerException != null)
                    {
                        MessageDialog.ShowWarning(ex.Message,
                            ex.StackTrace + "\nInner exception: " + ex.InnerException.Message ?? "none");
                    }
                    else
                    {
                        MessageDialog.ShowWarning(ex.Message);
                    }
                }
            }
        }

        public override void LoadDir(string URL) { LoadDir(URL, null); }
        public override void LoadDir() { LoadDir(null, null); }

        #endregion

    }
}
