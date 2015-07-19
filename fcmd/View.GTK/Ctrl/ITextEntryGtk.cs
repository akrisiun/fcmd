using pluginner.Widgets;
using System;

namespace fcmd.View.GTK.Ctrl
{
    public interface ITextEntryGtk : ITextEntry
    {
        Xwt.Drawing.Color BackgroundColor { get; set; }
    }
}
