using System;
using MonoMac.CoreText;
using MonoMac.Foundation;

namespace Xwt.Gtk.Mac
{
    public class GtkMacFontBackendHandler : Xwt.GtkBackend.GtkFontBackendHandler
    {
        protected override bool AddFontFile (string fontPath)
        {
            return CTFontManager.RegisterFontsForUrl (NSUrl.FromFilename (fontPath), CTFontManagerScope.Process) == null;
        }
    }
}

