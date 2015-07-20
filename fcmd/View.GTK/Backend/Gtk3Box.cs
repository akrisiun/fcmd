using System;
using Xwt.Backends;

namespace fcmd.View.GTK.Backend
{
    [BackendType(typeof(GtkBoxBackend))]
    public class Gtk3Box : Xwt.Box
    {
        public Gtk3Box(Orientation dir = Orientation.Vertical) : base(dir)
        {
            backend = new GtkBoxBackend(); // BackendHost.Backend as 
            BackendHost.Parent = this;
            BackendHost.SetCustomBackend(backend);
        }

        public Gtk3Box(Gtk.Box box, Orientation dir = Orientation.Vertical) : this(dir)
        {
            backend.Widget = box;
        }

        protected GtkBoxBackend backend;

        public Gtk.Box gtkBox { get { return this.backend.Widget as Gtk.Box; } }

        //protected class GtkWindowBackendHost : WindowFrame.WindowBackendHost { }

        //protected override BackendHost CreateBackendHost()
        //{
        //    var host = new GtkWindowBackendHost();
        //    if (backend == null)
        //        backend = new GtkBackend();

        //    host.Parent = this;
        //    host.SetCustomBackend(backend);
        //    return host;
        //}

        //private GtkBackend backend;

        //protected new GtkWindowBackendHost BackendHost
        //{
        //    get { return (GtkWindowBackendHost)base.BackendHost; }
        //}

    }

    public class GtkBoxBackend : Xwt.GtkBackend.BoxBackend, IBoxBackend
    {

    }

}
