/* The File Commander
 * The entry point for the fcmd.exe
 * (C) The File Commander Team - https://github.com/atauenis/fcmd
 * (C) 2013-14, Alexander Tauenis (atauenis@yandex.ru)
 * (C) 2014, Evgeny Akhtimirov (wilbit@me.com)
 * Contributors should place own signs here.
 */
using System;
using Xwt;
using System.Reflection;
using pluginner.Toolkit;

namespace fcmd
{
    public static class Startup
    {
        static string product_version { get { return Assembly.GetExecutingAssembly().GetName().Version.ToString(); } }

        [STAThread] //it's required due to WPF restrictions (without this, the Xwt.Wpf.dll backend is unable to start)
        public static void Main(string[] Commands)
        {
            // ReSharper disable LocalizableElement
            Console.WriteLine("The File Commander, version " +
               product_version + " (" + (Environment.Is64BitProcess ? " 64bit" : "32bit") + ")" + Environment.NewLine +
@"(C) 2013-15, the File Commander development team (https://github.com/atauenis/fcmd).
The FC is licensed ""as is,"" with  no  warranties regarding product performance or non-infringement of third party intellectual property rights; 
the software may be modified without restrictions");

#if !DEBUG
            return MainRelease();
#endif
            try
            {
                //for debugging purposes you may set any ToolkitType as you need
                switch (OSVersionEx.Platform)
                {
#if !GTK
					case PlatformID.Win32NT:
						Application.Initialize(ToolkitType.Wpf);
						break;
					case PlatformID.MacOSX:
						Application.Initialize(ToolkitType.Cocoa);
						break;
#endif
                    default:
                        if (!Environment.Is64BitProcess)
                            Application.Initialize(ToolkitType.Gtk3);
                        break;
                }
            }
            catch (Exception ex)
            {
                string errmsg = "The XWT could not be loaded:\n" + ex.InnerException.Message;

                if (ex.InnerException.InnerException != null)
                {
                    errmsg += "\n" + ex.InnerException.InnerException.Message;
                }

                Xwt.MessageDialog.ShowError(
                        errmsg + Environment.NewLine +
                    "The File Commander " + product_version + " (" + (Environment.Is64BitProcess ? "x64" : "x86") + "-DEBUG) Startup Failure"
                );
                return;
            }

            //new vmtest().Show();
            //var panes = new DemoPanes(); panes.ShowAll();
            new MainWindow(Commands).ShowAll();

            Application.Run();

        }

        public static void MainRelease()
        {
            try
            {
                var toolkitType = OSVersionEx.GetToolkitType();
                if (toolkitType == ToolkitType.Gtk)
                {
                    toolkitType = ToolkitType.Gtk3;
                }
                Application.Initialize(toolkitType);
            }
            catch (Exception ex)
            {
                Xwt.MessageDialog.ShowError(
                "The XWT could not be loaded:\n" + ex.InnerException.Message + Environment.NewLine +
                "The File Commander " + product_version + " (" + (Environment.Is64BitProcess ? "x64" : "x86") + ") Startup Failure"
                );
                return;
            }

            try
            {
                var Commands = Environment.GetCommandLineArgs();
                new MainWindow(Commands).ShowAll();

                Application.Run();
            }
            catch (Exception ex)
            {
                //startup crash handler
                string msg = "The File Commander has been crashed:\n" + ex.Message + "\n" + ex.StackTrace;
                string inex = "";
                if (ex.InnerException != null) inex = "\n Inner exception" + ex.InnerException.Message + "\n" + ex.StackTrace;
                msg += inex;

                Xwt.MessageDialog.ShowError(
                    msg + Environment.NewLine +
                    "The File Commander " + product_version + " (" + (Environment.Is64BitProcess ? "x64" : "x86") + ") Crash"
                );
                return;
            }

        }

    }
}