using pluginner.Widgets;
using System;

namespace fcmd.View.GTK.Ctrl
{
    public interface ITextEntryGtk : ITextEntry
    {
        Xwt.Drawing.Colors BackgroundColor { get; set; }
    }
}
