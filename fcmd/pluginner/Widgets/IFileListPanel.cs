﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Windows;
using fcmd;
using System.Drawing;
using System.Windows.Input;

namespace pluginner.Widgets
{
    // intefaces

    public interface IFileListPanel
    {
        IButton GoRoot { get; }
        IButton GoUp { get; }

        ITextEntry UrlBox { get; set; }

        IFSPlugin FS { get; }

        void LoadDir(string currentDirectory = null, ShortenPolicies? Shorten = null);
        // void LoadDir();

        DataFieldNumbers df { get; set; }
        string GetValue(int index);
        T GetValue<T>(int index);

        event TypedEvent<string> Navigate;
        event TypedEvent<string> OpenFile;

        event EventHandler GotFocus; // += (o, ea) => SwitchPanel(p1);
    }

    public interface IFileListPanel<T> : IFileListPanel where T : class, IListView2Item
    {
        IListView2<T> ListingView { get; }
        IUIListingView ListingWidget { get; }
    }

    // Visual data container
    public interface IUIListingView : IControl, IListView2
    {
#if WPF
        System.Windows.Input.Cursor Cursor { get; set; } // = CursorType.Wait;

#else
        Xwt.CursorType Cursor { get; set; } // = CursorType.Wait;
#endif
        bool Sensitive { get; set; }
        object Content { get; set; }

        IEnumerable ItemsSource { get; set; }
    }

    public interface IUIListingView<T> : IUIListingView
    { 
        // IEnumerable<T> ChoosedRows { get; set; }
        // object[]
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
        IEnumerable<object> DataL { get; set; }
        object Content { get; }
    }

    public interface IListView2
    {
        Font FontForFileNames { get; set; }
        void SetFocus();
    }

    // no visual data container
    public interface IListView2<T> : IListView2, ICollection<T> where T : IListView2Item
    {
        IPointedItem<T> PointedItem { get; set; }
        int SelectedRow { get; set; }
        void Select(T item);
        IEnumerable<T> ChoosedRows { get; } // set; }

        IList<T> DataItems { get; }
        // void Clear();

        // void AddItem(params object[] data); // Data, EditableFileds, di.URL);
        void AddItem(IEnumerable<Object> Data, IEnumerable<Boolean> EditableFields, string ItemTag = null);
        // void AddItem(T item); -> Add()
    }

    // Visible FileItem abstraction for WPF or XWT
    public interface IListView2Item
    {
        object Content { get; set; }
        bool CanGetFocus { get; set; }

        ListView2.ColumnInfo[] ColumnData { get; set; }
        ListView2.ItemStates State { get; set; }
        bool Visible { get; set; }
    }

    public interface IColumnInfo
    {
        object Content { get; set; }
    }

    public interface ITextEntry : IControl
    {
        string Text { get; set; }
    }

    public interface IButton // : IRelayCommand  ICommand
    {
        object Content { get; set; }
        string Text { get; set; }

        event EventHandler Clicked; // { add; remove; } // ; set; }

        bool CanGetFocus { get; set; }
    }

    public interface IControl : IInputElement // UIElement, IFrameworkInputElement
    {
        bool CanGetFocus { get; set; }  // -> IsEnabled
        Color BackgroundColor { get; set; }
    }

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


namespace fcmd
{

    public struct ShortenPolicies
    {
        // FileListPanel.SizeDisplayPolicy
        public SizeDisplayPolicy KB { get; set; }
        public SizeDisplayPolicy MB { get; set; }
        public SizeDisplayPolicy GB { get; set; }

        public static ShortenPolicies Empty { get; private set; }
        static ShortenPolicies()
        {
            Empty = new ShortenPolicies { KB = 0, MB = 0, GB = 0 };
        }
    }

    /// <summary>Defines the size shortening policy</summary>
    public enum SizeDisplayPolicy
    {
        DontShorten = 0, OneNumeral = 1, TwoNumeral = 2
        //2048 B, 2 KB, 2.0 KB
    }

}
