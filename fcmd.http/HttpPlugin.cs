// ankr : ftp over SSL plugin

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Net;
using Xwt;
using pluginner;
using pluginner.Toolkit;
using pluginner.Widgets;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;

namespace fcmd.http
{
    //IEnumerable<DirItem> DirectoryContent { get; }
    //IEnumerable<DirItem> GetDirectoryContent(FileSystemOperationStatus FSOS);

    //string CurrentDirectory { get; set; }
    //string RootDirectory { get; set; }
    //string Prefix { get; }
    //string NoPrefix(string dir);
    //bool FileExists(string URL);
    //bool DirectoryExists(string URL);
    //bool CanBeRead(string URL);
    //FSEntryMetadata GetMetadata(string URL);

    //Exception LastError { get; set; }
    //byte[] GetFileContent(string URL);
    //Stream GetFileStream(string URL, bool Write = false);
    //void WriteFileContent(string URL, Int32 Start, byte[] Content);
    //void Touch(FSEntryMetadata metadata);
    //void Touch(string URL);
    //void DeleteFile(string URL);
    //void MoveFile(string oldURL, string newURL);
    //void DeleteDirectory(string URL, bool TrySafe);
    //void CreateDirectory(string URL);
    //void MoveDirectory(string OldURL, string NewURL);
    //string DirSeparator { get; }

    //void CLIstdinWriteLine(string StdIn);

    //event TypedEvent<string> StatusChanged;
    //event TypedEvent<double> ProgressChanged;
    //event TypedEvent<string> CLIstdoutDataReceived;
    //event TypedEvent<string> CLIstderrDataReceived;
    //event TypedEvent<string> CLIpromptChanged;


    public class HttpPlugin : IVisualPlugin
    {
        #region Properties

        protected WebBrowser Browser;
        protected bool IsHttps = false;
        protected bool success = false;

        private List<DirItem> dirContent = new List<DirItem>();
        private string currentDirectory = "";
        // private string hostname;

        public Exception LastError { get; set; }

        public IControl UIControl {[DebuggerStepThrough] get; protected set; }
        public IFileListPanel Panel { get; protected set; }
        public string HtmlContent { get { return Browser == null ? null : Browser.GetHtml(); } }

        #endregion
        #region ctor

        public HttpPlugin() : this(null, null) { }
        public HttpPlugin(System.Windows.Controls.Panel parent, IControl control)
        {
            Prefix = "http://";
            if (parent == null)
                return;

            UIControl = control;
            Browser = new WebBrowser();

            UIElementCollection Children = parent.Children;
            Children.Add(Browser);
            Browser.Loaded += HttpContent_Loaded;
        }

        void HttpContent_Loaded(object sender, RoutedEventArgs e)
        {
            var browser = this.Browser;
            MsHtmlHelper.Prepare(browser);

            var url = currentDirectory;
            if (url.StartsWith("http"))
                browser.Navigate(url);
        }
        #endregion

        public IControl AttachToPanel(IFileListPanel panel, IControl uiControl, object element = null)
        {
            if (Panel == panel && Browser != null)
                return uiControl;

            this.UIControl = uiControl;
            this.Browser = element as WebBrowser;
            if (Browser != null)
            {
                //Browser.Visibility = Visibility.Hidden;
                //Browser = null;
            }

            Panel = panel;
            // uiContent.Loaded += HttpContent_Loaded;

            uiControl.Visible = true;
            return uiControl;
        }

        protected void LoadDir(string url)
        {
            var split = url.Split(new[] { ':' });
            if (split.Length >= 2 && split[0] != Prefix)
                Prefix = split[0] + "://";

            currentDirectory = url;
            RootDirectory = Prefix + "/";

            try {
                Browser.Navigate(url);
            }
            catch (Exception ex) { LastError = ex; }

            // _CheckProtocol(url);
        }

        public void Connect(string url)
        {
        }

        #region Content

        public IEnumerable<DirItem> DirectoryContent
        {
            get { return dirContent; }
        }

        public IEnumerable<pluginner.DirItem> GetDirectoryContent(FileSystemOperationStatus FSOS)
        {
            var output = dirContent;
            return output;
        }

        public string CurrentDirectory
        {
            get { return currentDirectory; }
            set { LoadDir(value); }
        }

        public string Prefix { get; set; } // { return "http://"; } }   // TODO
        public string RootDirectory { get; set; }

        public string NoPrefix(string dir)
        {
            if (dir != null && dir.StartsWith(Prefix))
                return dir.Substring(Prefix.Length);

            return dir;
        }

        #endregion
        
        #region Events

        public bool FileExists(string URL)
        {
            return dirContent.Any(di => di.URL == URL);
        }

        public bool DirectoryExists(string URL)
        {
            return dirContent.Any(di => di.URL == URL);
        }

        public bool CanBeRead(string URL)
        {
            throw new NotImplementedException();
        }

        public FSEntryMetadata GetMetadata(string URL)
        {
            return new FSEntryMetadata();
        }

        public byte[] GetFileContent(string URL)
        {
            throw new NotImplementedException();
        }

        public Stream GetFileStream(string URL, bool Write = false)
        {
            Uri URI = new Uri(URL);
            throw new NotImplementedException("no GetFileStream");
        }

        public void WriteFileContent(string URL, int Start, byte[] Content)
        {
            throw new NotImplementedException();
        }

        public void Touch(FSEntryMetadata metadata)
        {
            throw new NotImplementedException();
        }

        public void Touch(string URL)
        {
            throw new NotImplementedException();
        }

        public void DeleteFile(string URL) { }

        public void MoveFile(string oldURL, string newURL)
        {
            throw new NotImplementedException();
        }

        public void DeleteDirectory(string URL, bool TrySafe) { }

        public void CreateDirectory(string URL) { }

        public void MoveDirectory(string OldURL, string NewURL) { }

        public string DirSeparator
        {
            get { return "/"; }
        }

        #endregion

        #region Plugin

#pragma warning disable 0649, 0414  // is assigned but never used
        public event TypedEvent<string> StatusChanged = null;
        public event TypedEvent<double> ProgressChanged = null;

        public void CLIstdinWriteLine(string StdIn)
        {
            throw new NotImplementedException();
        }

        public event TypedEvent<string> CLIstdoutDataReceived;

        public event TypedEvent<string> CLIstderrDataReceived;

        public event TypedEvent<string> CLIpromptChanged;

        public int[] APICompatibility
        {
            get
            {
                int[] fapiver = { 0, 1, 0, 0, 1, 0 };
                return fapiver;
            }
        }

        public object APICallPlugin(string call, params object[] arguments)
        {
            return null;
        }

        public event TypedEvent<object[]> APICallHost;

        public System.Configuration.Configuration FCConfig
        {
            set { /*not used because the plugin is internal*/ }
        }

        public string Name { get { return "Web content plugin"; } }
        public string Version { get { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(); } }
        public string Author { get { return "akrisiun"; } }

        #endregion

    }
}