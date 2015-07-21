using System;
using Xwt;
using Xwt.Backends;
using Xwt.GtkBackend;

namespace fcmd.View.GTK.Backend
{
    [BackendType(typeof(Gtk3BoxBackend))]
    public class Gtk3Box : Xwt.Box
    {
        public Gtk3Box(Orientation dir = Orientation.Vertical) : base(dir)
        {
            backend = new Gtk3BoxBackend();
            BackendHost.Parent = this;
            BackendHost.SetCustomBackend(backend);
        }

        public Gtk3Box(Gtk.Box box, Orientation dir = Orientation.Vertical) : base(dir)
        {
            backend = new Gtk3BoxBackend(box);
            BackendHost.Parent = this;
            BackendHost.SetCustomBackend(backend);
        }

        protected Gtk3BoxBackend backend;

        // TODO
        // public Xwt.Window ParentWindow { get { return (Xwt.WindowFrame)base.Parent; } }

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

    //public class GtkBoxBackend : Xwt.GtkBackend.BoxBackend, IBoxBackend
	public class Gtk3BoxBackend : Xwt.GtkBackend.WidgetBackend, IBoxBackendFront 
    {
        public Gtk3BoxBackend()
        {
            base.Widget = new CustomContainer() { Backend = this };
            WidgetContainer.Show();
        }

        public Gtk3BoxBackend(CustomContainer container)
        {
            container.Backend = this;
            base.Widget = container;
        }

        public Gtk3BoxBackend(Gtk.Box gtkBox)
        {
            base.Widget = gtkBox;
        }

        public Gtk.Widget GtkWidget
        {
            get { return base.Widget; }
        }

        protected CustomContainer WidgetContainer
        {
            get { return base.Widget as CustomContainer; }
            // set { base.Widget = value; }
        }

        public void Add(IWidgetBackend widget)
        {
            WidgetBackend wb = (WidgetBackend)widget;

            var container = WidgetContainer;
            if (container != null)
                container.Add(wb.Frontend, GetWidget(widget));
        }

        public void Remove(IWidgetBackend widget)
        {
            var container = WidgetContainer;
            if (container != null)
                container.Remove(GetWidget(widget));
        }

        public void SetAllocation(IWidgetBackend[] widgets, Rectangle[] rects)
        {
            bool changed = false;

            var container = WidgetContainer;
            if (container == null)
                return; 
            for (int n = 0; n < widgets.Length; n++)
            {
                var w = GetWidget(widgets[n]);
                if (container.SetAllocation(w, rects[n]))
                    changed = true;
            }
            if (changed)
                container.QueueResizeIfRequired();
        }
       
    }
}
