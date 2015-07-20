using Xwt;
using Xwt.Backends;

namespace fcmd.View.GTK.Backend
{
    [BackendType(typeof(GtkBackend))] // default: IWindowBackend))]
    public class Gtk3WindowFrame : Window
    {

        public Gtk3WindowFrame() : base()
        {
            Padding = 0;
            //backend = new GtkBackend();
        }

        public Gtk.Window gtkWindow { get { return this.backend.Window; } }

        protected class GtkWindowBackendHost : WindowFrame.WindowBackendHost { }

        protected override BackendHost CreateBackendHost()
        {
            var host = new GtkWindowBackendHost();
            if (backend == null)
                backend = new GtkBackend();

            host.Parent = this;
            host.SetCustomBackend(backend);
            return host;
        }

        private GtkBackend backend;

        protected new GtkWindowBackendHost BackendHost
        {
            get { return (GtkWindowBackendHost)base.BackendHost; }
        }


        public void ShowAll()
        {
            backend.Window.ShowAll();
        }
    }

    // Window with properties 

    public class WindowX : Gtk.Window
    {
        public WindowX(Gtk.WindowType type = Gtk.WindowType.Toplevel) : base(type)
        {
        }

        public new void SetProperty(string property, GLib.Value obj)
        {
            base.SetProperty(property, obj);
        }

        public new GLib.Value GetProperty(string property)
        {
            return base.GetProperty(property);
        }
    }

    // Backend with fixed MainBox

    public class GtkBackend : Xwt.GtkBackend.WindowBackend
    {
        public GtkBackend() : base() { }

        public override void Initialize()
        {
            var x = new WindowX();
            Window = x;

            CreateMainLayout(); // alignment = new RootWindowAlignment(this);

            var vbox = new Gtk.VBox(false, 0);
            mainBox = vbox;
            Window.Add(vbox);

            GLib.Value val = new GLib.Value(vbox);
            x.SetProperty("child", val);
            val.Dispose();
        }
    }

}
