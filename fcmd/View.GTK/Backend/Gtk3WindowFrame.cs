using Xwt;
using Xwt.Backends;
using Xwt.GtkBackend;

namespace fcmd.View.GTK.Backend
{
    [BackendType(typeof(GtkBackend))] // default: IWindowBackend))]
    public class Gtk3WindowFrame : Window
    {
        public Gtk3WindowFrame() : base()
        {
            Padding = 0;
            Content = new Gtk3Box(backend.MainBox);
        }

        public Gtk.Window gtkWindow { get { return this.backend.Window; } }

        #region Host

        protected class GtkWindowBackendHost : WindowFrame.WindowBackendHost {

            public override void OnShown()
            {
                base.OnShown();
                (Parent as Gtk3WindowFrame).IsLoaded = true;
            }

        }

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

        #endregion

        protected override void OnShown()
        {
            base.OnShown();
        }

        public bool IsLoaded { get; set; }

        //protected WindowFrame Frontend
        //public ApplicationContext ApplicationContext

        public void ShowAll()
        {
            if (!gtkWindow.Visible)
                backend.Window.ShowAll();
            else
                backend.Window.Show();
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

    public class GtkBackend : WindowBackend //, IWindowBackend // IWidgetBackend 
    {
        public GtkBackend() : base() { }

        public override void Initialize()
        {
            var x = new WindowX();
            Window = x;

            // base.Initialize();
            this.mainBox = new Gtk.VBox(false, 0);
            Window.Add(this.mainBox);

            var vbox = this.mainBox; //  CreateMainLayout() as  Gtk.VBox; // alignment = new RootWindowAlignment(this);

            //this.alignment = new RootWindowAlignment(this);
            //alignment = new RootWindowAlignment(this);
            //mainBox.PackStart(alignment, true, true, 0);
            //alignment.Show();
            //return mainBox;


            // vbox.Parent = this.alignment;

            // var vbox =  new Gtk.VBox(false, 0);
            //  var vboxEnd = new Gtk3Box(vbox);
            // mainBox = vbox;
            // Window.Add(vbox);
            // this.SetChild(vboxEnd.GetBackend());

            // GtkAlign = this.alignment;
            //GLib.Value val = new GLib.Value(vbox);
            //x.SetProperty("child", val);
            //val.Dispose();
        }

        // public Gtk.Alignment GtkAlign { get; protected set; }

    }

}
