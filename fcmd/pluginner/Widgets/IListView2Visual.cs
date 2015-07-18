using System;

namespace pluginner.Widgets
{
    // Visible FileItem abstraction for WPF or XWT
    public interface IListView2Visual
    {
        object Content { get; set; }
        object[] Data { get; set; }
        int? RowIndex { get; set; }

        bool CanGetFocus { get; set; }

        ListView2.ColumnInfo[] ColumnData { get; set; }
        ListView2.ItemStates State { get; set; }
        bool Visible { get; set; }

        string fldFile { get; }
        string fldSize { get; }
        string fldModified { get; }

    }

    public interface IColumnInfo
    {
        object Content { get; set; }
    }

    public interface ITextEntry : IControl
    {
        string Text { get; set; }
    }

    public interface LabelWidget : IControl
    {
    }

    public interface IButton // : IRelayCommand  ICommand
    {
        object Content { get; set; }
        string Text { get; set; }

        event EventHandler Clicked; // { add; remove; } // ; set; }

        bool CanGetFocus { get; set; }
    }

#if WPF
    public interface IControl : IInputElement // UIElement, IFrameworkInputElement
    {
        bool CanGetFocus { get; set; }  // -> IsEnabled
        Color BackgroundColor { get; set; }
    }
#else 
    public interface IControl
    {

    }
#endif

    public class DataFieldNumbers
    {
        //they aren't const because they may change when the columns are reordered
        public int Icon = 0;
        public int URL = 1;
        public int DisplayName = 2;      // dfDisplayName
        public int Size = 3;
        public int Changed = 4;

        public static DataFieldNumbers Default() { return new DataFieldNumbers(); }
    }

}
