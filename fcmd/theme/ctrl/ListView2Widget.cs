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

        public PointedItem() : base(0, 0, "", null, null, null)
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

        //public PointedItem PointedItem {[DebuggerStepThrough] get { return DataObj.PointedItem as PointedItem; } set { DataObj.PointedItem = value; } }
        //IPointedItem IListView2.PointedItem { get { return PointedItem; } set { PointedItem = value as PointedItem; } }

        public int SelectedRow { get; set; }

        IList<ListView2ItemWpf> IListView2<ListView2ItemWpf>.DataItems {  get { return DataObj.DataItems; } }
        // ICollection IListView2.Items { get { return base.Items; } }

        // IEnumerable<pluginner.Widgets.ListView2Item> IUIListingView.ChoosedRows

        // Enumerable ItemsSource -> ItemsControl
        public IEnumerable<ListView2ItemWpf> ChoosedRows { get; set; }

        public bool? isLeftSide { get; set; }

        // public FS {get; set;}

        public TextBlock StatusBar { get; }

        public Font FontForFileNames { get; set; }

        // public CursorType Cursor {  get { return base.Cursor } } 
        // object Content { get; set; }
        // public bool Sensitive { get; set; }

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

        public int Count { get { return Items == null ? 0 : Items.Count; } } // DataObj.DataItems.Count; } }

     
        public void AddItem(params object[] data) // Data, EditableFileds, di.URL);
        {
            ;
        }

        public void SetFocus() {; }
        public void Clear() {
            if (DataObj != null && DataObj.Count > 0)
            {
                Items.Clear();
                DataObj.Clear();
            }
        }

        public void Select(ListView2ItemWpf item)
        {
            throw new NotImplementedException();
        }

        public void AddItem(IEnumerable<object> Data, IEnumerable<bool> EditableFields, string ItemTag = null)
        {
            throw new NotImplementedException();
        }

        public void Add(ListView2ItemWpf item)
        {
            throw new NotImplementedException();
        }

        public bool Contains(ListView2ItemWpf item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(ListView2ItemWpf[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(ListView2ItemWpf item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<ListView2ItemWpf> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
