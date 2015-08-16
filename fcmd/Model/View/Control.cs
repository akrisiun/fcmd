using fcmd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fcmd.View
{

    public static class CommanderBackend
    {
        public static IBackend Current { get; private set; }

        public static void Startup()
        {
            AppDomain.CurrentDomain.UnhandledException +=
                (s, e) => UnhandledException(s, e);

#if WPF
            Current = new fcmd.View.WpfBackend();
#else
            Current = new fcmd.View.GtkBackend();
#endif

        }

        static void UnhandledException(object sender, UnhandledExceptionEventArgs evt)
        {
            var ex = evt.ExceptionObject as Exception;
            System.Console.WriteLine(ex.Message);
#if WPF
            System.Windows.MessageBox.Show(ex.Message);
#endif
        }
    }

}
