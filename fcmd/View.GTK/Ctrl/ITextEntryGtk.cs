using pluginner.Widgets;
using System;

namespace fcmd.View.GTK.Ctrl
{
    public interface ITextEntryGtk : ITextEntry, IControl
    {
        Xwt.Drawing.Color BackgroundColor { get; set; }
    }
}
