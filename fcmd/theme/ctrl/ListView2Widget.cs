using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Markup;
using System.Windows.Controls.Primitives;
using pluginner.Widgets;
using System.Drawing;
using System.Windows.Media;
using DrawingColor = System.Drawing.Color;
using System.Windows.Input;
using System.Collections;

namespace fcmd.theme.ctrl
{
    public class PointedItem : pluginner.Widgets.ListView2ItemWpf, IPointedItem // <ListView2Item>
    {
        // public object[] Data { get; set; }

        public PointedItem() : base(0, 0, "", null, null) // , null)
        {
        }

    }

    public static class ColorConvert
    {
        public static DrawingColor To(this System.Windows.Media.Color mediaColor)
        {
            return default(DrawingColor);   // TODO
        }

        public static DrawingColor To(this System.Windows.Media.Brush mediaBrush)
        {
            // mediaBrush.
            return default(DrawingColor);   // TODO
        }

        public static System.Windows.Media.Brush From(this DrawingColor drawingColor)
        {
            return default(System.Windows.Media.Brush);
        }
    }

    public class ListView2Widget : DataGrid, IListView2<ListView2ItemWpf>, IUIListingView<ListView2ItemWpf>
               , IAddChild, IContainItemStorage  // WPF
    {
        // no visual data container
        public ListView2Data DataObj { get; protected set; }

        public ListView2Widget()
        {
            DataObj = new ListView2Data(this);
            // TODO: DataObj.PointedItem = new PointedItem(); // <ListView2Item>();
            DataContext = DataObj;
        }

        // Visual properties
        public object Content { get; set; }
        public bool CanGetFocus { get { return IsEnabled; } set { IsEnabled = value; } }
        public bool Sensitive { get { return DataObj.Sensitive; } set { DataObj.Sensitive = value; } }
        // public CursorType Cursor { get { return base.Cursor ; set; }

        public DrawingColor BackgroundColor { get { return (Background as SolidColorBrush).Color.To(); } set { Background = value.From(); } }

        public int SelectedRow { get; set; }

        IList<ListView2ItemWpf> IListView2<ListView2ItemWpf>.DataItems { get { return DataObj.DataItems; } }

        // Enumerable ItemsSource -> ItemsControl
        public IEnumerable<ListView2ItemWpf> ChoosedRows { get; set; }

        public PanelSide Side  { get; set; }

        // public FS {get; set;}

        public TextBlock StatusBar { get; }

        public Font FontForFileNames { get; set; }

        public IPointedItem<ListView2ItemWpf> PointedItem
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int Count { get { return Items == null ? 0 : Items.Count; } }

        public void Clear()
        {
            if (DataObj != null && DataObj.Count > 0)
            {
                // Items.Clear();
                // DataObj.Clear();
            }
        }

        public void Dispose()
        {
            if (DataObj != null)
                DataObj.Clear();
        }

        public void SetFocus() { }

        public void Select(ListView2ItemWpf item)
        {
            throw new NotImplementedException();
        }

        public void SetupColumns()
        {
            if (this.Columns.Count == 0)
            {
                ListView2.ColumnInfo[] definitions = DataObj.DefineColumns(null);
                this.ToDataSource<ListView2ItemWpf>(DataObj.DataItems, definitions);
            }
        }

        // TODO
        public ListView2.ColumnInfo[] DefineColumns(DataFieldNumbers df)
        { return null; }

        #region ICollection

        public void AddItem(IEnumerable<object> Data, IEnumerable<bool> EditableFields, string ItemTag = null)
        {
            DataObj.AddItem(Data, EditableFields, ItemTag);
        }

        public void Add(ListView2ItemWpf item)
        {
            DataObj.Add(item);
        }

        public bool Contains(ListView2ItemWpf item)
        {
            return DataObj.Contains(item);
        }

        public void CopyTo(ListView2ItemWpf[] array, int arrayIndex)
        {
            DataObj.CopyTo(array, arrayIndex);
        }

        public bool Remove(ListView2ItemWpf item)
        {
            return DataObj.Remove(item);
        }

        public IEnumerator<ListView2ItemWpf> GetEnumerator()
        {
            return DataObj.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        #endregion

    }

    public enum PanelSide
    {
        Undefined = 0,
        Left = 1,
        Right = 2
    }

}
