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

namespace fcmd.View.ctrl
{
    public class PointedItem : IPointedItem<ListItemXaml> // <ListView2Item>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PointedItem() { } // : base(0, 0, "", null, null) // , null)

        public int Index { get; set; }
        public ListItemXaml Item { get; set; }

        /// Pointed items
        public IEnumerable<ListItemXaml> Pointed { get; set; }

        public object[] Data { get { return Item.Data; } set { Item.Data = value; } }

        /// <summary>
        ///   returns Full path 
        /// </summary>
        public override string ToString()
        {
            return Item.FullPath;
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

    public class ListView2Widget : DataGrid, IListingContainer<ListItemXaml>, IAddChild, IContainItemStorage, IControl, IUIDispacher
    {
        // Non visual data container
        public ListFiltered2Xaml DataObj { get; protected set; }

        public PanelWpf Panel { get; set; }
        public FileListPanelWpf FileList { get; set; }

        public ListView2Widget()
        {
            DataObj = new ListFiltered2Xaml(this);
            DataObj.PointedItem = new PointedItem() { Index = -1, Item = null }; // <ListView2Item>();
            DataContext = DataObj;
        }

        object IUIDispacher.Dispacher { get { return this.Dispatcher as Dispatcher; } }
        bool IUIDispacher.CheckAccess() { return (this as DispatcherObject).CheckAccess(); }

        bool bound = false;
        public virtual void Bind()
        {
            if (this.ItemsSource == null)
                this.ItemsSource = DataObj.DataItems;

            if (bound) return;

            // DataGrid bind
            this.BindGridEvents();
            bound = true;
        }
        public virtual void UnBind()
        {
            if (!bound) return;
            bound = false;
            this.UnBindGridEvents();
        }

        public virtual void CloneList(ListView2Widget source)
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

        public void Clear()
        {
            // if (DataObj != null && DataObj.Count > 0)
        }

        public void Dispose()
        {
            if (DataObj != null)
                DataObj.Clear();
        }

        public void SetFocus()
        {
            // if (!MainWindow.AppLoading) { }
            if (!CanGetFocus)
                return;

            // DataGrid got focus
            Focus();
        }

        string fileProcol { get { return base_plugins.fs.localFileSystem.FilePrefix; } } // => "file://"

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

        public void LoadDir(string path)
        {
            UnBind();
            string fullpath = null;
            try
            {
                fullpath = path.StartsWith(fileProcol)
                        ? Path.GetFullPath(path.Substring(fileProcol.Length)) : Path.GetFullPath(path);
                Directory.SetCurrentDirectory(fullpath);
            }
            catch (Exception ex) { MessageDialog.ShowError(ex.Message); }

            if (string.IsNullOrWhiteSpace(fullpath))
                return;

            App.ConsoleWriteLine("Widget:LoadDir " + fullpath);

            this.ItemsSource = null;

            var PanelDataWpf = (this.Panel as PanelWpf).PanelDataWpf as FileListPanelWpf;
            PanelDataWpf.UrlBox.Text = this.FileList.FS.Prefix + fullpath;

            this.FileList.LoadDirThen(fileProcol + fullpath, null,
                () => Bind());
        }

        public bool ColumnsSet { get; private set; } //  return this.Columns.Count > 1; } }

        public void SetupColumns()
        {
            var items = DataObj.DataItems;

            if (!ColumnsSet)
            {
                ColumnsSet = true;
                ListView2.ColumnInfo[] definitions = DefineColumns(null);
                this.ToDataSource<object>(items, definitions);

                this.Bind();
            }
            else
                this.ItemsSource = items;
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

    }

}
