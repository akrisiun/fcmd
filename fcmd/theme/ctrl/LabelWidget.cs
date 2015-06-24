using System;
using System.Windows.Controls;

namespace fcmd.theme.ctrl
{
    public class LabelWidget : Label
    {
        public string Text { get { return Content as string; } set { Content = value; } }
    }
}
