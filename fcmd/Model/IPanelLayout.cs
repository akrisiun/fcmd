using fcmd.Controller;

namespace fcmd.Model
{
    // non visual, cross platform ready interfaces

    public interface IPanel
    {
        FileListPanel PanelData { get; }
        // FileListPanelWpf :FileListPanel, IFileListPanel<T> where T : class, IListView2Visual

        PanelSide Side { get; set; }
        bool? IsActive { get; set; }

        void Shown();
    }

    public interface IPanelLayout
    {
        IPanel Panel1 { get; }
        IPanel Panel2 { get; }
    }

}
