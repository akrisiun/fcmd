﻿using fcmd.Platform;
using fcmd.View.Xaml;
using pluginner.Widgets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xwt.Drawing;

// #if WPF
// using ListView2Canvas = pluginner.Widgets.ListView2ItemWpf;

namespace fcmd.View.ctrl
{
    // FilePanel non Visual
    public class ListObservable : ObservableCollection<ListView2ItemWpf>, IList<ListView2ItemWpf>
    {

    }

    public class ListView2Xaml : pluginner.Widgets.Xaml.ListView2Xaml<ListView2ItemWpf>, IListingView<ListView2ItemWpf>, IVisualSensitive
    {
        public ListView2Xaml(IListingContainer<ListView2ItemWpf> parent) : base(parent)
        {
            _Items = new ListObservable();
        }

        public void AddItemDirectory(object[] Data, IEnumerable<bool> EditableFields, string ItemTag)
        {
            var lvi = ListView2ItemWpf.DirectoryItem(_Items.Count, Data, EditableFields, ItemTag);
            Add(lvi);
        }

        public void AddItemFile(object[] Data, IEnumerable<bool> EditableFields, string ItemTag)
        {
            var lvi = ListView2ItemWpf.FileItem(_Items.Count, Data, EditableFields, ItemTag);
            Add(lvi);
        }

        public override void AddItem(IEnumerable<object> Data, IEnumerable<bool> EditableFields, string ItemTag)
        {
            // base.AddItem(Data, EditableFields, ItemTag);
            var lvi = ListView2ItemWpf.FileItem(_Items.Count, Data as object[], EditableFields, ItemTag);
            Add(lvi);
        }

        #region Items array

        public System.Windows.Input.Cursor Cursor { get; set; }

        //public override object Content { get { return null; } set {; } }
        public override Font FontForFileNames { get; set; }

        protected ListObservable _Items;
        public override IList<ListView2ItemWpf> DataItems { [DebuggerStepThrough] get { return _Items; } }

        public override object Content { get; set; }

        //TODO:
        //public IPointedItem<ListView2ItemWpf> PointedItem { get; set; }
        //public override Xwt.Drawing.Font FontForFileNames
        //public int SelectedRow { get; set; } 
        //public int Count

        public IEnumerable<object> ItemsForGrid()
        {
            var numerator = _Items.GetEnumerator();
            while (numerator.MoveNext())
            {
                ListView2ItemWpf item = numerator.Current;
                yield return new { fldFile = item.fldFile, fldSize = item.fldSize, fldModified = item.fldModified };
            }
        }

        public override void Add(ListView2ItemWpf item)
        {
            item.RowIndex = _Items.Count;
            _Items.Add(item);
        }

        public override bool Contains(ListView2ItemWpf item)
        {
            return _Items.Contains(item);
        }

        public override void CopyTo(ListView2ItemWpf[] item, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public override bool Remove(ListView2ItemWpf item)
        {
            return _Items.Remove(item);
        }

        #endregion

        // Route Events to Parent

        public override void SetFocus() { Parent.SetFocus(); }
        public override void SetupColumns()
        {
#if WPF
            var parent = Parent as ListView2Widget;
            parent.SetupColumns();
#endif
        }

        public override void Dispose()
        {
            _Items = null;
        }


        public override ListView2.ColumnInfo[] DefineColumns(DataFieldNumbers df)
        {
            // TODO: dynamic columns size
            return ListView2Xaml.DefaultXamlColumns();
        }

    }

}
