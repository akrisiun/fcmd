using fcmd.Platform;
using fcmd.View.Xaml;
using pluginner.Widgets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xwt.Drawing;

namespace fcmd.View.ctrl
{

    public class ListView2Xaml : pluginner.Widgets.Xaml.ListView2Xaml<ListItemXaml>, IListingView<ListItemXaml>, IVisualSensitive, IListingContainer
    {
        public ListView2Xaml(IListingContainer<ListItemXaml> parent) : base(parent)
        {
            _Items = new ListObservable();
        }

        public void AddItemDirectory(object[] Data, IEnumerable<bool> EditableFields, string ItemTag)
        {
            var lvi = ListItemXaml.DirectoryItem(_Items.Count, Data, EditableFields, ItemTag);
            Add(lvi);
        }

        public void AddItemFile(object[] Data, IEnumerable<bool> EditableFields, string ItemTag)
        {
            var lvi = ListItemXaml.FileItem(_Items.Count, Data, EditableFields, ItemTag);
            Add(lvi);
        }

        public override void AddItem(IEnumerable<object> Data, IEnumerable<bool> EditableFields, string ItemTag)
        {
            // base.AddItem : Data, EditableFields, ItemTag
            var lvi = ListItemXaml.FileItem(_Items.Count, Data as object[], EditableFields, ItemTag);
            Add(lvi);
        }

        #region Items array

        public System.Windows.Input.Cursor Cursor { get; set; }

        public override object Content { get { return null; } set {; } }

        public override Font FontForFileNames { get; set; }

        protected ListObservable _Items;
        public override ObservableCollection<ListItemXaml> DataItems {[DebuggerStepThrough] get { return _Items; } }

        IEnumerable IListingContainer.ItemsSource { get { return _Items; } set { _Items = value as ListObservable; } }
        object IListingContainer.Content
        {
            get { return this.Content; }
            set { } // readonly
        }

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
                ListItemXaml item = numerator.Current;
                yield return new { fldFile = item.fldFile, fldSize = item.fldSize, fldModified = item.fldModified };
            }
        }

        public override void Add(ListItemXaml item)
        {
            item.RowIndex = _Items.Count;
            _Items.Add(item);
        }

        public override bool Contains(ListItemXaml item)
        {
            return _Items.Contains(item);
        }

        public override void CopyTo(ListItemXaml[] item, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public override bool Remove(ListItemXaml item)
        {
            return _Items.Remove(item);
        }

        #endregion

        #region Route Events to Parent

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

        #endregion

    }

}
