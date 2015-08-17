using System;
using fcmd.Platform;
using fcmd.View;
using fcmd.Model;
using Application = System.Windows.Application;

namespace fcmd
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IApplication
    {
        static App()
        {
            // debugger entry init
            CommanderBackend.Startup();
        }

        public App() {
            this.Startup += App_Startup;
        }

        public void App_Startup(object sender, System.Windows.StartupEventArgs e = null)
        {
            this.MainWindow = new MainWindow();
            if (!fcmd.MainWindow.AppLoading)
                return;    // test unit case

            this.MainWindow.Show();
        }

        ICommanderWindow IApplication.MainWindow {  get { return MainWindow as ICommanderWindow; } }


#if !VS || __MonoCS__
        public void InitializeComponent() {
            this.StartupUri = new System.Uri("MainWindow.xaml", System.UriKind.Relative);
        }
        
        [System.STAThreadAttribute()]
        // [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static void Main() {
            fcmd.App app = new fcmd.App();
            app.InitializeComponent();
            app.Run();
        }
#endif

    }
}
