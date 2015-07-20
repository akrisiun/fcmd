using pluginner.Widgets;

namespace fcmd.Platform
{
    // public override IListingView<ListView2ItemWpf> ListingView { get { return ListingViewWpf.DataObj as ListView2Xaml; } }

    public interface IVisualSensitive : IListView2 // IListingView // class ListView3Xaml : ListView3
    {
        bool Sensitive { get; set; }

#if WPF
        System.Windows.Input.Cursor Cursor { get; set; }
#else
        Xwt.CursorType Cursor { get; set; } // = CursorType.Wait;
#endif

    }

}