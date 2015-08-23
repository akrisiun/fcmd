using System;
using fcmd.Model;
using fcmd.View;
using System.IO;
using System.Windows;
using fcmd.View.ctrl;
using fcmd.View.Xaml;

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

        static fcmd.App app;
        MainWindow main;

        void MainWindowShow()
        {
            MainWindow.AppLoading = false;

            if (fcmd.App.Instance != null)
                app = fcmd.App.Instance;
            else
            {
                try
                {
                    app = new fcmd.App();
                    app.Exit += App_Exit;
                    app.App_Startup(app);
                }
                catch (Exception)
                {
                    var dom =  AppDomain.CreateDomain("second");
                    //  dom.DomainManager.InitializeNewDomain(new AppDomainSetup() { ApplicationName = "Second " });

                    app = new fcmd.App();
                    app.Exit += App_Exit;
                    app.App_Startup(app); ;
                }
            }

            app.MainWindow = null;

            main = new MainWindow();

            MainWindow.AllowShutdown = false;
            if (main.WindowData == null)
                main.Init();
            app.MainWindow = main;
        }

        void AppRun()
        {
            if (app.MainWindow != null)
            {
                app.MainWindow.Visibility = Visibility.Visible;
                app.MainWindow.Focus();
                //  app.Run();
            }
        }

        void App_Exit(object sender, ExitEventArgs e)
        {
            app = null;
            fcmd.App.Instance = null;
        }

        public string dir { get; set; }

        [TestMethod]
        public void localFS_ReadLongDir()
        {
            if (!OSVersionEx.IsWindows)
                return;

            // Big directory
            dir = Environment.GetEnvironmentVariable("SystemRoot") + @"\System32";

            MainWindowShow();

            // WindowDataWpf
            var data = main.WindowDataWpf as WindowDataWpf;
            // data.DoInit
            var backend = data.BackendWpf as WpfBackend;

            backend.args = new string[] { dir };
            Directory.SetCurrentDirectory(dir);
            backend.Shown();

            backend.LoadDirSynchonous();
            data.DoShown();
            main.Show();

            AppRun();
            Console.WriteLine("success");
            main.Close();
        }

        [TestMethod]
        public void localFS_AccessDenied()
        {
            dir = @"C:\ProgramData\Desktop";

            MainWindowShow();

            Directory.SetCurrentDirectory(dir);
            var data = main.WindowDataWpf as WindowDataWpf;
            var backend = data.BackendWpf as WpfBackend;

            backend.args = new string[] { dir };
            backend.Shown();
            backend.LoadDirSynchonous();
            app.MainWindow = main;
            main.Show();

            var left = main.p1Wpf;
            var cmdUp = left.GoUp.Command;
            cmdUp.Execute(null);

            if (!main.IsVisible)
                AppRun();

            MainWindow.AllowShutdown = true;
            app.Run();
            if (app == null)
                return;
            app.MainWindow = null;
            main.Close();
        }

        [TestMethod]
        public void localFS_NetworkUNC()
        {
            dir = @"\\192.168.1.12\C$\";
            Directory.SetCurrentDirectory(dir);
            var cur = Environment.CurrentDirectory;

            MainWindowShow();
            var data = main.WindowDataWpf as WindowDataWpf;
            var backend = data.BackendWpf as WpfBackend;

            backend.args = new string[] { dir };
            backend.LoadDirSynchonous();
            var listing = data.ActivePanel.ListingWidget as ListView2Widget; // .ListingView as ListFiltered2Xaml;
            listing.SetupColumns();

            backend.Shown();
            AppRun();
            if (main != null)
                main.Close();
        }
    }
}

#endif