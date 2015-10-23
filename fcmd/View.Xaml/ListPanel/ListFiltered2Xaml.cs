using fcmd.Platform;
using fcmd.View.Xaml;
using pluginner.Widgets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Threading;
using System.Threading;
using System.Threading.Tasks;
using Xwt.Drawing;
using System.ComponentModel;
using System.Globalization;

namespace fcmd.View.ctrl
{
    // clone of ListView2Xaml : filtered list (top 1000 items)

    // TODO
    public abstract class ListSearchResult2Xaml : pluginner.Widgets.Xaml.ListView2Xaml<ListItemXaml> // IListingView<ListItemXaml>, IVisualSensitive, IListingContainer
    {
        public ListSearchResult2Xaml(IListingContainer<ListItemXaml> parent) : base(parent) { }
    }

    public class ListFiltered2Xaml : pluginner.Widgets.Xaml.ListView2Xaml<ListItemXaml>,
        ICollectionView,
        IListingView<ListItemXaml>, IVisualSensitive, IListingContainer
    {
        public ListFiltered2Xaml(IListingContainer<ListItemXaml> parent)
            : base(parent)
        {
            filtered = new ListObservable();
            origin = new Collection<object[]>();
        }

        #region Filter List

        public void Unbind()
        {
            origin.Clear();
        }

        public void AddData(object[] dirData)
        {
            // Data[]: URL, Name, Size, DateTime, IsDirectory, (optional #6 DirItem)
            origin.Add(dirData);
        }

        public void Finish()
        {
            // UI tread
            if (this.Parent.CheckAccess())
                Requery.Take(this, origin);
            else
                (this.Parent.Dispacher as System.Windows.Threading.Dispatcher).Invoke(
                    () => Requery.Take(this, this.origin));
        }

        public void Clone(ListFiltered2Xaml source)
        {
            origin.Clear();

            var sourceOrigin = source.origin.GetEnumerator();
            while (sourceOrigin.MoveNext())
                origin.Add(sourceOrigin.Current);
        }

        public const int maxWPFRecords = 4000;  // for DataGrid WPF
        protected IList<object[]> origin;
        // public IEnumerator<object[]> Source { get { return origin == null ? null : origin.GetEnumerator(); } }

        internal class Requery
        {
            public static void Take(ListFiltered2Xaml list, object newList) { }

            public static void Take(ListFiltered2Xaml list, IList<object[]> origin,
                int maxCount = maxWPFRecords)
            {
                list.Parent.ItemsSource = null;
                list.filtered.Clear();

                var numer = origin.GetEnumerator();
                int index = -1;
                while (numer.MoveNext() && index < maxCount)
                {
                    index++;
                    object[] rec = numer.Current;

                    string tag = rec[FileListPanelWpf.idxName] as string;       // TODO: for search multiple folder use idxURL
                    bool isDirectory = (bool)rec[FileListPanelWpf.idxIsDirectory];

                    if (isDirectory)
                        AddItemDirectory(list, rec, null, tag);
                    else
                        AddItemFile(list, rec, null, tag);
                }

                var parent = list.Parent as ListView2DataGrid;
                parent.ItemsSource = null;
                if (!parent.ColumnsSet)
                    parent.SetupColumns();
                else
                    parent.Bind();

                parent.Panel.Update();
            }

            static void AddItemDirectory(ListFiltered2Xaml list, object[] Data, IEnumerable<bool> EditableFields, string ItemTag)
            {
                var filtered = list.filtered;
                var lvi = ListItemXaml.DirectoryItem(filtered.Count, Data, EditableFields, ItemTag);
                filtered.Add(lvi);
            }

            static void AddItemFile(ListFiltered2Xaml list, object[] Data, IEnumerable<bool> EditableFields, string ItemTag)
            {
                var filtered = list.filtered;
                var lvi = ListItemXaml.FileItem(filtered.Count, Data, EditableFields, ItemTag);
                filtered.Add(lvi);
            }

        }

        #endregion

        #region Items array

        public System.Windows.Input.Cursor Cursor { get; set; }

        public override object Content { get { return filtered; } set { ; } }
        public override Font FontForFileNames { get; set; }

        protected ListObservable filtered;
        public override ObservableCollection<ListItemXaml> DataItems
        {
            [DebuggerStepThrough]
            get { return filtered; }
        }

        IEnumerable IListingContainer.ItemsSource { get { return filtered; } set { } }

        //TODO:
        //public IPointedItem<ListView2ItemWpf> PointedItem { get; set; }
        //public override Xwt.Drawing.Font FontForFileNames
        //public int SelectedRow { get; set; } 
        //public int Count

        public override void AddItem(IEnumerable<object> Data, IEnumerable<bool> EditableFields, string ItemTag)
        {
            // base.AddItem : Data, EditableFields, ItemTag
            var lvi = ListItemXaml.FileItem(filtered.Count, Data as object[], EditableFields, ItemTag);
            Add(lvi);
        }

        public IEnumerable<object> ItemsForGrid()
        {
            var numerator = filtered.GetEnumerator();
            while (numerator.MoveNext())
            {
                ListItemXaml item = numerator.Current;
                yield return new { fldFile = item.fldFile, fldSize = item.fldSize, fldModified = item.fldModified };
            }
        }

        public override void Add(ListItemXaml item)
        {
            item.RowIndex = filtered.Count;
            filtered.Add(item);
        }

        public override bool Contains(ListItemXaml item)
        {
            return filtered.Contains(item);
        }

        public override void CopyTo(ListItemXaml[] item, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public override bool Remove(ListItemXaml item)
        {
            return filtered.Remove(item);
        }

        #endregion

        #region Route Events to Parent

        public override void SetFocus() { Parent.SetFocus(); }

        public override bool ColumnsSet { get { return this.Parent.ColumnsSet; } }
        public override void SetupColumns()
        {
#if WPF
            var parent = Parent as ListView2DataGrid;
            if (!parent.ColumnsSet)
                parent.SetupColumns();
#endif
        }

        public override void Dispose()
        {
            filtered = null;
        }


        public override ListView2.ColumnInfo[] DefineColumns(DataFieldNumbers df)
        {
            // TODO: dynamic columns size
            return ListFiltered2Xaml.DefaultXamlColumns();
        }

        #endregion

        #region WPF ICollectionView

        public bool CanFilter { get { return true; } }
        public bool CanGroup { get { return false; } }
        public bool CanSort { get { return true; } }

        public bool Contains(object item)
        {
            return this.filtered.Contains(item as ListItemXaml);
        }

        public System.Globalization.CultureInfo Culture
        {
            get { return CultureInfo.DefaultThreadCurrentUICulture; }
            set { CultureInfo.DefaultThreadCurrentUICulture = value; }
        }

#pragma warning disable 0649, 0414, 0067  // is assigned but never used
        public event EventHandler CurrentChanged;
        public event CurrentChangingEventHandler CurrentChanging;

        public object CurrentItem
        {
            get { return this.filtered[CurrentPosition]; }
        }

        public int CurrentPosition { get; protected set; }

        public IDisposable DeferRefresh()
        {
            return filtered;
        }

        public Predicate<object> Filter { get; set; }
        public ObservableCollection<GroupDescription> GroupDescriptions { get { return null; } }
        public ReadOnlyObservableCollection<object> Groups { get { return null; } }

        public bool IsCurrentAfterLast
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsCurrentBeforeFirst
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsEmpty
        {
            get { return !filtered.GetEnumerator().MoveNext(); }
        }

        public bool MoveCurrentTo(object item)
        {
            return true;
        }

        public bool MoveCurrentToFirst()
        {
            return true;
        }

        public bool MoveCurrentToLast()
        {
            return true;
        }

        public bool MoveCurrentToNext()
        {
            return true;
        }

        public bool MoveCurrentToPosition(int position)
        {
            return true;
        }

        public bool MoveCurrentToPrevious()
        {
            return true;
        }

        public void Refresh()
        {

        }

        public SortDescriptionCollection SortDescriptions
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable SourceCollection
        {
            get { return this.filtered; }
        }

        //public new IEnumerator GetEnumerator()
        //{
        //    throw new NotImplementedException();
        //}

        public event System.Collections.Specialized.NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion
    }

}

