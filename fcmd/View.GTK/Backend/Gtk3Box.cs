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

        public Gtk3Box(Gtk.Box box) : 
            base(box.Orientation == Gtk.Orientation.Vertical ? Orientation.Vertical : Orientation.Horizontal)
        {
            backend = new Gtk3BoxBackend(box);
            BackendHost.Parent = this;
            BackendHost.SetCustomBackend(backend);
        }

        protected Gtk3BoxBackend backend;

        public Gtk.Box gtkBox { get { return this.backend.Widget as Gtk.Box; } }


        public new void PackStart(Widget widget, bool expand, bool fill)
        {
            // base.PackStart(widget);

            var native = widget.XBackend().NativeWidget as Gtk.Widget;
            uint padding = Padding ?? 0;
            gtkBox.PackStart(native, expand, fill, padding);
        }

        public uint? Padding { get; set; }

    }

    //public class Gtk3CustomContainer : CustomContainer
    //{
    //    public Gtk.Box Box { get; set; }
    //}

    // public class GtkBoxBackend : Xwt.GtkBackend.BoxBackend, IBoxBackend
    public class Gtk3BoxBackend : Xwt.GtkBackend.WidgetBackend, IBoxBackendFront 
    {
        public Gtk3BoxBackend()
        {
            // base.Widget = new CustomContainer() { Backend = this };
            Box = new Gtk.Box(Gtk.Orientation.Vertical, 0);
            Padding = null;
            WidgetContainer.Show();
        }

        //public Gtk3BoxBackend(CustomContainer container)
        //{
        //    container.Backend = this;
        //    base.Widget = container;
        //}

        public Gtk3BoxBackend(Gtk.Box gtkBox)
        {
            Padding = null;
            Box = gtkBox;
        }

        public Gtk.Box Box
        {
            get { return base.Widget as Gtk.Box; }
            set { base.Widget = value; }
        }

        public virtual void Pack(object child, bool expand, WidgetPlacement vpos, WidgetPlacement hpos)
        {
            var box = Box;
            Gtk.Widget widget = child as Gtk.Widget;
            if (widget != null)
                box.PackStart(widget, expand, fill: vpos == WidgetPlacement.Fill, padding: Padding ?? 0);
        }

        public uint? Padding { get; set; }

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
