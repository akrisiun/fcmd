using Xwt;
using Xwt.Backends;

namespace fcmd.View.GTK.Backend
{

    public class GtkBackend : Xwt.GtkBackend.WindowBackend
    {
        public GtkBackend() : base()
        {
        }

        public override void Initialize()
        {
            Window = new Gtk.Window("");
            // Window.Add(CreateMainLayout());

            CreateMainLayout();
            // alignment = new RootWindowAlignment(this);

            var vbox = new Gtk.VBox(false, 0);
            mainBox = vbox;

            Window.Add(vbox);
        }
    }

    [BackendType(typeof(GtkBackend))] // default: IWindowBackend))]
    public class GtkWindowFrame : Window
    {

        public GtkWindowFrame() : base()
        {
            Padding = 0;
        }

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

}
