using Gtk;
using System;
using Xwt.Backends;

namespace fcmd.View.GTK.Backend
{
    [BackendType(typeof(Gtk3HPanedBackend))]
    public class Gtk3HPaned : Xwt.HPaned
    {
        // : base (Xwt.Backends.Orientation.Vertical)

        public Gtk3HPaned(Paned panned = null) : base()
        {
            if (panned == null)
                backend = new Gtk3HPanedBackend();
            else
            {
                backend = new Gtk3HPanedBackend(panned);
            }
            BackendHost.Parent = this;
            BackendHost.SetCustomBackend(backend);
        }

        protected Gtk3HPanedBackend backend;

        public Gdk.Window Window { get { return gtkPaned.Window; } }

        public global::Gtk.HPaned gtkPaned { get { return this.backend.Widget as Gtk.HPaned; } }

        public Gtk.Widget gtkChild1 { get { return gtkPaned.Child1; } }
        public Gtk.Widget gtkChild2 { get { return gtkPaned.Child1; } }

        public void PackStart()
        {
        }
    }

    public class Gtk3HPanedBackend : Xwt.GtkBackend.PanedBackend, IPanedBackend, IWidgetBackend
    {
        public Gtk3HPanedBackend() : base()
        {
        }

        public Gtk3HPanedBackend(Gtk.Paned widget)
        {
            NeedsEventBox = false;
            base.Widget = widget;
        }

        public new void Initialize(Xwt.Backends.Orientation dir)
        {
            if (Widget != null)
                Widget.Show();
            else
            {
                base.Widget = new Gtk.HPaned();
                base.Widget.Show();
                // base.Initialize();
            }
        }

    }

}
