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

namespace fcmd.View.ctrl
{
    public class PointedItem : IPointedItem<ListView2ItemWpf> // <ListView2Item>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PointedItem() { } // : base(0, 0, "", null, null) // , null)

        public int Index { get; set; }
        public ListView2ItemWpf Item { get; set; }

        /// Pointed items
        public IEnumerable<ListView2ItemWpf> Pointed { get; set; }

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

    public class ListView2Widget : DataGrid, IListingContainer<ListView2ItemWpf>, IAddChild, IContainItemStorage
    {
        // Non visual data container
        public ListView2Xaml DataObj { get; protected set; }

        // Xwt.CursorType IListingContainer.Cursor { get; set; } // = CursorType.Wait;
        // System.Windows.Input.Cursor Cursor { get; set; } // = CursorType.Wait;

        public PanelWpf Panel { get; set; }
        public FileListPanelWpf FileList { get; set; }

        public ListView2Widget()
        {
            DataObj = new ListView2Xaml(this);
            DataObj.PointedItem = new PointedItem() { Index = -1, Item = null }; // <ListView2Item>();
            DataContext = DataObj;
        }

        bool bound = false;
        public virtual void Bind()
        {
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

        public IList<ListView2ItemWpf> DataItems { get { return DataObj.DataItems; } }

        // Enumerable ItemsSource -> ItemsControl
        public IEnumerable<ListView2ItemWpf> ChoosedRows { get; set; }
        public IPointedItem<ListView2ItemWpf> PointedItem { get; set; }

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

        public void Select(ListView2ItemWpf item)
        {
            this.SelectedRow = DataItems.IndexOf(item);
        }

        public void SelectLast(ListView2ItemWpf item)
        {
            PointedItem = new PointedItem() { Item = item, Index = this.SelectedRow };
            (Panel.WindowData as WindowDataWpf).OnSelectedItem(PointedItem);
        }

        public bool SelectEnter(ListView2ItemWpf item)
        {
            var fullpath = item.FullPath.StartsWith(fileProcol)
                    ? Path.GetFullPath(item.FullPath.Substring(fileProcol.Length)) : 
                      (item.RowIndex == 0 ?  item.FullPath : null);
            if (fullpath != null && Directory.Exists(fullpath))
            {
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
            App.ConsoleWriteLine("Widget:LoadDir " + path);

            UnBind();
            //try {

                this.ItemsSource = null;
                //if (path.Contains("://") && !path.Contains(fileProcol))
                //    this.FileList.LoadDir(path, null);
                //else
                //{
                    var fullpath = path.StartsWith(fileProcol)
                        ? Path.GetFullPath(path.Substring(fileProcol.Length)) : Path.GetFullPath(path);
                    Directory.SetCurrentDirectory(fullpath);

                    this.FileList.LoadDirThen(fileProcol + fullpath, null, () => Bind());
                //}
                // Bind();

            //}
            //catch (Exception ex) { MessageDialog.ShowError(ex.Message); }
        }

        public void SetupColumns()
        {
            var items = DataObj.DataItems;  // .ItemsForGrid();

            if (this.Columns.Count == 0)
            {
                ListView2.ColumnInfo[] definitions = DefineColumns(null);
                this.ToDataSource<object>(items, definitions);

                this.Bind();
            }
            else
            {
                try
                {
                    this.ItemsSource = items;
                }
                catch (Exception) { }
            }
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

        //public IEnumerator<ListView2ItemWpf> GetEnumerator()
        //{
        //    return DataObj.GetEnumerator();
        //}
        // IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        #endregion

    }

}
