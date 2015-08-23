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
            ConsoleWriteLine("App load");
            // debugger entry init
            CommanderBackend.Startup();
        }

        public static App Instance { get; set; }

        public static fcmd.View.WpfBackend BackendWpf { get { return CommanderBackend.Current as WpfBackend; } }

        public App()
        {
            Instance = this;
            this.Startup += App_Startup;
        }

        public void App_Startup(object sender, System.Windows.StartupEventArgs e = null)
        {
            ConsoleWriteLine("App Startup");

            var main = new MainWindow();
            this.MainWindow = main;
            if (!fcmd.MainWindow.AppLoading)
                return;    // test unit case

            var task = main.PreloadAsync();

            ConsoleWriteLine("App Show");
            main.Show();

            if (task.Exception != null)
                App.ConsoleWriteLine("Error " + task.Exception.Message
                    + Environment.NewLine + task.Exception.StackTrace);

            ConsoleWriteLine("App AfterShow");
        }

        ICommanderWindow IApplication.MainWindow { get { return MainWindow as ICommanderWindow; } }

        public IBackend Backend { get { return CommanderBackend.Current; } }

        public static void ConsoleWriteLine(string line)
        {
#if DEBUG
            Console.WriteLine(String.Format("{0:HH:mm:ss.fff} {1}", DateTime.Now, line));
#endif
        }

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
