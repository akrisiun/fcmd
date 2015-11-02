using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Markup;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using DrawingColor = System.Drawing.Color;
using System.IO;

using pluginner.Widgets;
using fcmd.View.Xaml;
using fcmd.Controller;
using fcmd.Model;
using System.Windows.Threading;
using System.Windows;
using System.Collections;
using System.Diagnostics.Contracts;
using System.ComponentModel;

namespace fcmd.View.ctrl
{
    /// <summary>
    /// File system visual data content, Xaml backend DataGrid control
    /// </summary>
    public class ListView2DataGrid : DataGrid, IListingContainer<ListItemXaml>, IAddChild, IContainItemStorage, IControl, IUIDispacher
    {
        #region ctor

        // Non visual data container
        public ListFiltered2Xaml DataObj {[DebuggerStepThrough] get; protected set; }
        public PanelWpf Panel {[DebuggerStepThrough] get; set; }
        public FileListPanelWpf FileList {[DebuggerStepThrough] get; set; }

        public ListView2DataGrid()
        {
            DataObj = new ListFiltered2Xaml(this);
            DataObj.PointedItem = new PointedItem() { Index = -1, Item = null }; // <ListView2Item>();
            DataContext = DataObj;
        }

        public bool? Visible { get { return Visibility == Visibility.Visible; } set { VisibleSet.Value(this, value); } }
        object IUIDispacher.Dispacher { get { return this.Dispatcher as Dispatcher; } }
        bool IUIDispacher.CheckAccess() { return (this as DispatcherObject).CheckAccess(); }

        //public override void OnApplyTemplate()
        //    base.OnApplyTemplate();

        #endregion

        #region Bind, Properties

        bool bound = false;
        public virtual void Bind()
        {
            if (bound && this.ItemsSource == null)
            {
                Contract.Assert(DataObj.DataSource == this);
                DataObj.BindUpdate(); // this.ItemsSource = DataObj.DataItems;
            }

            if (bound) return;

            DataObj.BindGrid(this);

            // DataGrid bind
            this.BindGridEvents();
            bound = true;
        }

        public virtual void UnBind()
        {
            if (!bound) return;
            bound = false;

            DataObj.UnBindGrid();
            this.UnBindGridEvents();
        }

        public virtual void CloneList(ListView2DataGrid source)
        {
            var target = this.DataItems;
            if (target.Count > 0)
                target.Clear();

            var dataObj = DataObj;
            dataObj.Clone(source.DataObj);
            dataObj.Finish();
        }

        // Visual properties
        public object Content { get; set; }
        public bool CanGetFocus { get { return IsEnabled; } set { IsEnabled = value; } }
        public bool Sensitive { get { return DataObj.Sensitive; } set { DataObj.Sensitive = value; } }

        public DrawingColor BackgroundColor
        {
            get { return (Background as SolidColorBrush).Color.To(); }
            set { Background = value.From(); }
        }

        public int SelectedRow { get; set; }

        public IList<ListItemXaml> DataItems { get { return DataObj.DataItems; } }

        // Enumerable ItemsSource -> ItemsControl
        public IEnumerable<ListItemXaml> ChoosedRows { get; set; }
        public IPointedItem<ListItemXaml> PointedItem { get; set; }

        public PanelSide Side { get; set; }
        public string urlFull { get { return FileList.FS.CurrentDirectory; } }
        public string urlDir
        {
            get
            {
                var dir = FileList.FS.CurrentDirectory;
                return (dir.StartsWith(fileProcol)) ? dir.Substring(fileProcol.Length) : null;
            }
        }

        // public FS {get; set;}
        // public TextBlock StatusBar { get; }
        public Xwt.Drawing.Font FontForFileNames { get; set; }

        public int Count { get { return Items == null ? 0 : Items.Count; } }

        #endregion

        #region Base methods, Focus

        public void Clear() { }

        public void Dispose()
        {
            if (DataObj != null)
                DataObj.Clear();
        }

        public void SetFocus()
        {
            if (!CanGetFocus)
                return;

            // DataGrid got focus
            Focus();
        }

        // change DataGridColumn.With from Auto to Actual
        public static void DataGridColumnWidths(DataGrid dataGrid)
        {
            if (dataGrid.Columns.Count > 0)
            {
                var numerator = dataGrid.Columns.GetEnumerator();
                while (numerator.MoveNext())
                {
                    var item = numerator.Current;
                    item.Width = item.ActualWidth;
                }

                dataGrid.Columns[0].Width = 200;
            }
        }

        #endregion

        string fileProcol { [DebuggerStepThrough] get { return base_plugins.fs.localFileSystem.FilePrefix; } } // => "file://"

        public void LoadDir(string path)
        {
            UnBind();

            WpfContent.Load(this.Panel, path, this.fileProcol);
        }

        #region Select, LoadDir, Columns

        public void Select(ListItemXaml item)
        {
            this.SelectedRow = DataItems.IndexOf(item);
        }

        public void SelectLast(ListItemXaml item)
        {
            PointedItem = new PointedItem() { Item = item, Index = this.SelectedRow };
            var data = Panel.WindowData as WindowDataWpf;
            if (data != null)
                data.OnSelectedItem(PointedItem);
            else
                App.ConsoleWriteLine("found Panel.WindowData null"); // BUG!!!
        }

        public bool SelectEnter(ListItemXaml item)
        {
            if (item == null)
                return false;

            var fullpath = item.FullPath.StartsWith(fileProcol)
                    ? Path.GetFullPath(item.FullPath.Substring(fileProcol.Length)) :
                      (item.RowIndex == 0 ? item.FullPath : null);
            if (fullpath != null && Directory.Exists(fullpath))
            {
                if (!fullpath.EndsWith(Path.DirectorySeparatorChar.ToString())) //  AltDirectorySeparatorChar))
                    fullpath += Path.DirectorySeparatorChar.ToString();

                LoadDir(fullpath);
                return true;
            }
            else if (item.FullPath.Contains(Path.DirectorySeparatorChar.ToString())
                     && Directory.Exists(item.FullPath))
            {
                LoadDir(item.FullPath);
                return true;
            }
            return false;
        }

        public bool ColumnsSet { get; private set; } //  return this.Columns.Count > 1; } }

        public void SetupColumns()
        {
            if (!ColumnsSet)
            {
                ColumnsSet = true;
                ListView2.ColumnInfo[] definitions = DefineColumns(null);

                var items = DataObj;
                this.ToDataSource<object>(items, definitions);

                this.Bind();
            }
            else
                DataObj.BindUpdate();

            Contract.Assert(this.ItemsSource != null);
        }

        public ListView2.ColumnInfo[] DefineColumns(DataFieldNumbers df)
        {
            return DataObj.DefineColumns(df);
        }

        #endregion

        #region ICollection

        public void AddItem(IEnumerable<object> Data, IEnumerable<bool> EditableFields, string ItemTag = null)
        {
            DataObj.AddItem(Data, EditableFields, ItemTag);
        }

        public void Add(ListItemXaml item)
        {
            DataObj.Add(item);
        }

        public bool Contains(ListItemXaml item)
        {
            return DataObj.Contains(item);
        }

        public void CopyTo(ListItemXaml[] array, int arrayIndex)
        {
            DataObj.CopyTo(array, arrayIndex);
        }

        public bool Remove(ListItemXaml item)
        {
            return DataObj.Remove(item);
        }

        //public IEnumerator<ListView2ItemWpf> GetEnumerator()
        //{
        //    return DataObj.GetEnumerator();
        //}
        // IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        #endregion

        #region Sort

        //public static readonly DependencyProperty CustomSorterProperty =
        //      DependencyProperty.RegisterAttached("CustomSorter", typeof(ICustomSorter), typeof(DataGridColumn));
        //protected override void OnSorting(DataGridSortingEventArgs eventArgs)
          //  base.OnSorting(eventArgs);

        // SetCollectionView
        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            try
            {
                base.OnItemsSourceChanged(oldValue, newValue);
            }
            catch (Exception ex) { Console.WriteLine(ex); }
        }

        #endregion
    }

}
