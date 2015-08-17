using System;
using fcmd.Model;
using fcmd.View;
using System.IO;

#if !__MonoCS__

using Microsoft.VisualStudio.TestTools.UnitTesting;
using fcmd.ftps;
using pluginner.Toolkit;

namespace fcmd.test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Ftp_SSL_ReadDir()
        {
            var ssl = new FtpSecure();
            // ssl.Connect()
            ssl.CurrentDirectory = "ftp://localhost";

            var list = ssl.DirectoryContent;
        }


        [TestMethod]
        public void localFS_ReadLongDir()
        {
            if (!OSVersionEx.IsWindows)
                return;

            // Big directory
            var dir = Environment.GetEnvironmentVariable("SystemRoot") + @"\System32";

            fcmd.App app = new fcmd.App();
            // app.InitializeComponent();

            MainWindow.AppLoading = false;
            app.App_Startup(app);

            var main = app.MainWindow as MainWindow;
            if (main.WindowData == null)
                main.Init();

            // WindowDataWpf
            var data = main.WindowDataWpf as WindowDataWpf;
            // data.DoInit
            var backend = data.BackendWpf as WpfBackend;

            backend.args = new string[] { dir };
            Directory.SetCurrentDirectory(dir);
            backend.Shown();
            backend.LoadDirSynchonous();
        }
    }
}

#endif