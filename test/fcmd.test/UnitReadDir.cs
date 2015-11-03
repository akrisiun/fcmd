using System;
using fcmd.Model;
using fcmd.View;
using System.IO;
using System.Windows;
using fcmd.View.ctrl;
using fcmd.View.Xaml;

#if !__MonoCS__
using Microsoft.VisualStudio.TestTools.UnitTesting;
using fcmd.http;
using pluginner.Toolkit;

namespace fcmd.test
{
    [TestClass]
    public class UnitReadDir
    {
        public string dir { get; set; }

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
            dir = Environment.GetEnvironmentVariable("SystemRoot") + @"\System32";

            TestApp.MainWindowShow();

            // WindowDataWpf
            var main = MainWindow.ActiveWindow;
            var data = main.WindowDataWpf as WindowDataWpf;
            var backend = data.BackendWpf as WpfBackend;

            backend.args = new string[] { dir };
            Directory.SetCurrentDirectory(dir);
            backend.Shown();

            backend.LoadDirSynchonous();
            TestApp.AppRun(data);
            Console.WriteLine("success");
            main.Close();
        }

        [TestMethod]
        public void localFS_AccessDenied()
        {
            dir = @"C:\ProgramData\Desktop";

            TestApp.MainWindowShow();
            var app = TestApp.App;
            var main = MainWindow.ActiveWindow;

            // Directory.SetCurrentDirectory(dir);
            var data = main.WindowDataWpf as WindowDataWpf;
            var backend = data.BackendWpf as WpfBackend;

            backend.args = new string[] { dir };
            backend.Shown();
            backend.LoadDirSynchonous();
            if (app != null)
            {
                app.MainWindow = main;
                main.Show();
            }

            var left = main.p1Wpf;
            var cmdUp = left.GoUp.Command;
            cmdUp.Execute(null);

            if (!main.IsVisible)
                TestApp.AppRun();

            MainWindow.AllowShutdown = true;
            app.Run();
            if (app == null)
                return;
            app.MainWindow = null;
            main.Close();
        }

        [TestMethod]
        [Ignore]
        public void localFS_NetworkUNC()
        {
            dir = @"\\192.168.1.12\C$\";
            Directory.SetCurrentDirectory(dir);
            var cur = Environment.CurrentDirectory;

            TestApp.MainWindowShow();
            var main = MainWindow.ActiveWindow;
            var data = main.WindowDataWpf as WindowDataWpf;
            var backend = data.BackendWpf as WpfBackend;

            backend.args = new string[] { dir };
            backend.LoadDirSynchonous();
            var listing = data.ActivePanel.ListingWidget as ListView2DataGrid; // .ListingView as ListFiltered2Xaml;
            listing.SetupColumns();

            backend.Shown();

            TestApp.AppRun();
            if (main != null)
                main.Close();
        }
    }
}

#endif