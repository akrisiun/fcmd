using System;
using System.Collections;
using System.Collections.Generic;
using Xwt.Drawing;

// using System.Windows;
// using System.Drawing;
// using System.Windows.Input;

namespace pluginner.Widgets
{
    // intefaces

    public interface IFileListPanel
    {
        IButton GoRoot { get; }
        IButton GoUp { get; }
        ITextEntry UrlBox { get; }
        IStatusBar StatusBar { get; }

        IFSPlugin FS { get; }

        void LoadDir(string currentDirectory = null, ShortenPolicies? Shorten = null);
        void LoadDir();

        ShortenPolicies ShortenPolicy { get; set; }
        DataFieldNumbers df { get; set; }

        string GetValue(int index);
        T GetValue<T>(int index);

        event TypedEvent<string> Navigate;
        event TypedEvent<string> OpenFile;
        event EventHandler GotFocus; // += (o, ea) => SwitchPanel(p1);
    }

    public interface IFileListPanel<T> : IFileListPanel where T : class, IListView2Visual
    {
        IListingView<T> ListingView { get; }

        // IUIListingView ListingWidget { get; }
    }

    // Visual data container
    public interface IListingContainer : IListView2  // IVisualSensitive
    {
        bool Sensitive { get; set; }

        IEnumerable ItemsSource { get; set; }
    }

    // DataGrid for Xaml
    public interface IListingContainer<T> : IListingContainer, IControl
    {
        void Bind();
    }

    public interface IPointedItem<T> : IPointedItem
    {
        T Item { get; set; }
        IEnumerable<T> Pointed { get; set; }

        // zero based
        int Index { get; }
    }

    public interface IPointedItem
    {
        object[] Data { get; set; }
        // object Content { get; }
    }

    public interface IListView2
    {
        // Xwt.Drawing.Font FontForFileNames { get; set; }
        void SetFocus();
        void SetupColumns();
        bool ColumnsSet { get; }

        ListView2.ColumnInfo[] DefineColumns(DataFieldNumbers df);
    }

    // no visual data container
    public interface IListView2<T> : IListView2, ICollection<T> where T : IListView2Visual
    {
        IPointedItem<T> PointedItem { get; set; }
        int SelectedRow { get; set; }

        void Select(T item);

        ICollection<T> DataItems { get; }

        void AddItem(IEnumerable<Object> Data, IEnumerable<Boolean> EditableFields, string ItemTag = null);
        // void AddItem(T item); -> Add()
    }


}