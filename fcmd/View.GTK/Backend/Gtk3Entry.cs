using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xwt.Backends;

namespace fcmd.View.GTK.Backend
{
    [BackendType(typeof(Gtk3EntryBackend))]
    public class Gtk3Entry : Xwt.TextEntry
    {
        protected override BackendHost CreateBackendHost()
        {
            backend = new Gtk3EntryBackend();
            return new WidgetBackendHost();
        }

        protected Gtk3EntryBackend backend;
        ITextEntryBackend Backend
        {
            get { return (ITextEntryBackend)backend; }
        }

    }

    public class Gtk3EntryBackend : Xwt.GtkBackend.TextEntryBackend // , ITextEntryBackend
    {

    }
}
