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

namespace fcmd.View.Xaml
{
    // pluginner.Widgets.ListView2ItemWpf
    //   ListView2ItemWpf(int rowNumber, int colNumber, string rowTag, ListView2.ColumnInfo[] columns, List<object> data, Font font) 

    public class FileListPanelWpf : FileListPanel<ListView2ItemWpf>
    {
        public PanelWpf Parent { get; set; }
        public WindowDataWpf WindowData { [DebuggerStepThrough] get; set; }

        protected EventHandler onFocus;
        protected bool onFocusSet;

        public override event TypedEvent<string> Navigate;
        public override event TypedEvent<string> OpenFile;

        public override event EventHandler GotFocus { add { onFocusSet = true; onFocus += value; } remove { onFocus += value; } }

        // original constructor
        //public FileListPanelXaml(string BookmarkXML = null, string CSS = null,
        //    string InfobarText1 = "{Name}", string InfobarText2 = "F: {FileS}, D: {DirS}")

        public FileListPanelWpf(PanelWpf parent)
        {
            Parent = parent;
        }

        public ListView2Widget ListingViewWpf { get; protected set; }

        public override IListingView<ListView2ItemWpf> ListingView { get { return ListingViewWpf.DataObj; } }

        public override IListingContainer ListingWidget { get { return ListingViewWpf; } }

        public override void Initialize(PanelSide side)
        {
            onFocus = new EventHandler(Focused);

            df = DataFieldNumbers.Default();

            GoUp = Parent.cdUp;
            GoRoot = Parent.cdRoot;
            UrlBox = Parent.path;

            ListingViewWpf = Parent.data;
            ListingViewWpf.Panel = Parent as Xaml.PanelWpf;
            ListingViewWpf.FileList = this;

            ListingViewWpf.Side = side;
        }

        public void Focused(object sender, EventArgs e)
        {
            var side = Parent.Side;
            if (WindowData != null)
                WindowData.OnSideFocus(side);
        }

        // ExpandoObject data
        public override TItem GetValue<TItem>(int Field)
        {
            return (TItem)ListingView.PointedItem.Data[Field];
        }

        public override string GetValue(int Field)
        {
            return (string)ListingView.PointedItem.Data[Field];
        }

        public void BuildUI(string BookmarkXML = null) { }

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

        public override void LoadFs(string URL, ShortenPolicies Shorten)
        {
            // string URL, SizeDisplayPolicy ShortenKB, SizeDisplayPolicy ShortenMB, SizeDisplayPolicy ShortenGB)
            // new { KB = ShortenKB, MB = ShortenMB, GB
            SizeDisplayPolicy ShortenKB = Shorten.KB;
            SizeDisplayPolicy ShortenMB = Shorten.MB;
            SizeDisplayPolicy ShortenGB = Shorten.GB;

            bool checkAccess = (UrlBox as DispatcherObject).CheckAccess();
            if (checkAccess)
                UrlBox.Text = URL;

            string updir = URL + FS.DirSeparator + "..";
            string rootdir = FS.GetMetadata(URL).RootDirectory;

            List<DirItem> dis = new List<DirItem>();
            //dis = FS.DirectoryContent;

            var resetHandle = new AutoResetEvent(false);
            Thread DirLoadingThread =
                new Thread(delegate ()
                {
                    FS.GetDirectoryContent(ref dis, new FileSystemOperationStatus());
                    resetHandle.Set();
                });

            DirLoadingThread.Start();
            var sucess = resetHandle.WaitOne(timeout: TimeSpan.FromSeconds(10)); // 10 secs
                                                                                 // wait loop: do { } while (DirLoadingThread.ThreadState == ThreadState.Running);
            if (dis.Count == 0)
                return;

            uint counter = 0;

            foreach (DirItem di in dis)
            {
                ICollection<object> Data = new Collection<Object>();
                List<Boolean> EditableFileds = new List<bool>();
                //Data.Add(di.IconSmall ?? Image.FromResource("fcmd.Resources.image-missing.png"));

                EditableFileds.Add(false);
                Data.Add(di.URL); EditableFileds.Add(false);
                Data.Add(di.TextToShow); EditableFileds.Add(true);
                if (di.TextToShow == "..")
                {//parent dir
                    Data.Add("<↑ UP>"); EditableFileds.Add(false);
                    EditableFileds[2] = false;
                    Data.Add(
                        FS.GetMetadata(di.URL).LastWriteTimeUTC.ToLocalTime());

                    EditableFileds.Add(false);
                    updir = di.URL;
                }
                else if (di.IsDirectory)
                {//dir
                    Data.Add("<DIR>"); EditableFileds.Add(false);
                    Data.Add(di.Date); EditableFileds.Add(false);
                }
                else
                {//file
                    Data.Add(
                        di.Size.KiloMegaGigabyteConvert(
                            ShortenKB, ShortenMB, ShortenGB));

                    EditableFileds.Add(false);
                    Data.Add(di.Date); EditableFileds.Add(false);
                }
                Data.Add(di);

                (ListingView as ListView2Xaml).AddItem(Data, EditableFileds, di.URL);
                ++counter;
            }

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

            //неспешное TODO:придумать, куда лучше закорячить; не забываем, что во время работы 
            //FS может меняться полностью
            //FS.CLIstdoutDataReceived += FS_CLIstdoutDataReceived;
            //FS.CLIstderrDataReceived += (stderr) => { CLIoutput.Text += "\n" + stderr; Utilities.ShowWarning(stderr); };
            //FS.CLIpromptChanged += FS_CLIpromptChanged;

            if (FS.CurrentDirectory == null)
            {
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
                LoadDir(
                    "file://" + Directory.GetCurrentDirectory(),
                    Shorten // new { KB = ShortenKB, MB = ShortenMB, GB = ShortenGB }
                );
                return;
            }

            ListingWidget.Sensitive = false;

            string oldCurDir = FS.CurrentDirectory;

            bool loadPlugin = false;
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
                        ex.StackTrace + "\nInner exception: " + ex.InnerException.Message ?? "none");
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
                    LoadDir(URL, Shorten);
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

            if ((Parent as DispatcherObject).CheckAccess())
            {
                var view = ListingView;
                if (view.DataItems.Count > 0)
                {
                    view.SetupColumns();
                    view.SelectedRow = 0;
                }
                view.SetFocus();
            }

            ListingWidget.Sensitive = true;
        }

        public override void LoadDir()
        {
            LoadDir(null, null);
        }
    }
}
