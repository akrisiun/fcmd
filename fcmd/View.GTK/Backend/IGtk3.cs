using Xwt.Backends;

namespace fcmd.View.GTK.Backend
{
    public interface IGtk3
    {
        IWidgetBackend Backend { get; }
    }

    public static class Gtk3Methods
    {
        public static Gtk.Widget GetNativeWidget(this Xwt.Widget widget)
        {
            var gtkWidget = Xwt.Toolkit.CurrentEngine.GetNativeWidget(widget) as Gtk.Widget;
            return gtkWidget;
        }

        public static IWidgetBackend GetBackend(this Xwt.Widget w)
        {
            var end = Xwt.Toolkit.CurrentEngine.GetSafeBackend(w) as IWidgetBackend;
            return end;
            //if (w != null && w.BackendHost is XwtWidgetBackend)
            //    return GetBackend((XwtWidgetBackend)w.Backend);
            //return w != null ? w.Backend : null;
        }
    }
}