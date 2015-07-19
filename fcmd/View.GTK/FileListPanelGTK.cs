using fcmd.Controller;
using pluginner;
using pluginner.Widgets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using Xwt;

namespace fcmd.View.GTK
{
    public class FileListPanelGtk : FileListVisual<ListView2Canvas>
    {
        public FileListPanelGtk(string BookmarkXML = null, string CSS = null,
            string InfobarText1 = "{Name}", string InfobarText2 = "F: {FileS}, D: {DirS}")
            : base(BookmarkXML, CSS, InfobarText1, InfobarText2)
        {
        }

        #region Implement

        public override IListingContainer ListingWidget 
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override IListingView<ListView2Canvas> ListingView
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override void Initialize(PanelSide side)
        {
            throw new NotImplementedException();
        }

        protected override string MakeStatusbarText(string Template)
        {
            throw new NotImplementedException();
        }

        protected override void WriteDefaultStatusLabel()
        {
            throw new NotImplementedException();
        }

        #endregion

        public override void LoadFs(string URL, ShortenPolicies Shorten)
        {
            SizeDisplayPolicy ShortenKB = Shorten.KB;
            SizeDisplayPolicy ShortenMB = Shorten.MB;
            SizeDisplayPolicy ShortenGB = Shorten.GB;

            UrlBox.Text = URL;
            // ListingView.Clear();

            string updir = URL + FS.DirSeparator + "..";
            string rootdir = FS.GetMetadata(URL).RootDirectory;


            List<DirItem> dis = new List<DirItem>();
            // dis = FS.DirectoryContent;

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
                // 
                ICollection<object> Data = new Collection<Object>();
                List<Boolean> EditableFileds = new List<bool>();
                //Data.Add(di.IconSmall ??
                //    Image.FromResource("fcmd.Resources.image-missing.png"));

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

                ListingView.AddItem(Data, EditableFileds, di.URL);

                const uint per_number = ~(((~(uint)0) >> 10) << 10);
                if ((++counter & per_number) == 0)
                {
                    Application.MainLoop.DispatchPendingEvents();
                }
            }

            if (goUpDelegate != null)
            {
                GoUp.Clicked -= goUpDelegate;
            }


            // WPF problem : no UI thread synchornize
            goUpDelegate = (o, ea) =>
            {
                LoadDir(updir);
            };


            GoUp.Clicked += goUpDelegate;
            if (goRootDelegate != null)
            {
                GoRoot.Clicked -= goRootDelegate;
            }
            goRootDelegate = (o, ea) =>
            {
                LoadDir(rootdir);
            };
            GoRoot.Clicked += goRootDelegate;

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
                FS.StatusChanged += FS_StatusChanged;
                FS.ProgressChanged += FS_ProgressChanged;
            }

            if (URL == "." && FS.CurrentDirectory == null)
            {
                LoadDir(
                    "file://" + Directory.GetCurrentDirectory(),
                    Shorten // new { KB = ShortenKB, MB = ShortenMB, GB = ShortenGB }
                );
                return;
            }

            ListingWidget.Cursor = CursorType.Wait;
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

            // #if WPF
            var view = ListingView;
            if (view.DataItems.Count > 0)
            {
                view.SetupColumns();
                view.SelectedRow = 0;
                // view.ScrollerIn.ScrollTo(0, 0);
            }

            view.SetFocus();
            //one fixed bug may make many other bugs...уточнить необходимость!
            ListingWidget.Sensitive = true;
            ListingWidget.Cursor = CursorType.Arrow;
        }

    }
}
