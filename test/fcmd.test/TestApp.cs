using fcmd.Model;
using fcmd.View;
using System;
using System.Windows;

namespace fcmd.test
{
    class TestApp
    {
        public static fcmd.App App { get; private set;}
        
        public static MainWindow MainWindowShow()
        {
            MainWindow.AppLoading = false;

            if (fcmd.App.Instance != null)
                App = fcmd.App.Instance;
            else
            {
                try
                {
                    App = new fcmd.App();
                    App.Exit += App_Exit;
                    App.App_Startup(App);
                }
                catch (Exception)
                {
                    var dom = AppDomain.CreateDomain("second");

                    App = new fcmd.App();
                    // app.Exit += App_Exit;
                    App.App_Startup(App); ;
                }
            }

            App.MainWindow = null;
            new MainWindow();
            if (MainWindow.ActiveWindow == null)
                throw new ArgumentNullException("MainWindow.ActiveWindow error");

            MainWindow.AllowShutdown = false;
            if (MainWindow.ActiveWindow.WindowData == null)
                MainWindow.ActiveWindow.Init();

            App.MainWindow = MainWindow.ActiveWindow;
            return MainWindow.ActiveWindow;
        }

        public static void AppRun(WindowDataWpf backendData)
        {
            backendData.DoShown();
            var main = MainWindow.ActiveWindow;
            main.Show();
            AppRun();
        }

        public static void AppRun()
        {
            if (App.MainWindow != null)
            {
                App.MainWindow.Visibility = Visibility.Visible;
                App.MainWindow.Focus();
            }
        }

        static void App_Exit(object sender, ExitEventArgs e)
        {
            App = null;
            fcmd.App.Instance = null;
        }
    }
}
