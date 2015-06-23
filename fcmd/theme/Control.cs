using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fcmd.theme
{
    public interface ITheme
    {
        void Init(MainWindow window);

#if WPF
        void KeyEvent(MainWindow window, System.Windows.Input.KeyEventArgs key);
#else
        void KeyEvent(MainWindow window, Xwt.KeyEventArgs key);
#endif
    }

    public class Control
    {
        public static ITheme Theme = new wpf();
    }
}
