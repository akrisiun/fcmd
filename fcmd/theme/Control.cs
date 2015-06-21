using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fcmd.theme
{
    public interface ITheme
    {
        void Init(MainWindow window);
        void Key(MainWindow window, Xwt.KeyEventArgs key);
    }

    public class Control
    {
        public static ITheme Theme = new wpf();
    }
}
