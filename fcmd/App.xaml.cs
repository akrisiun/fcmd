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

        ICommanderWindow IApplication.MainWindow {  get { return MainWindow as ICommanderWindow; } }
    }
}
