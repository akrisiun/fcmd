/* The File Commander - plugin API - ListView2
 * The ListView2 item widget, Presentation framework version
 * (C) The File Commander Team - https://github.com/atauenis/fcmd
 * (C) 2014, Alexander Tauenis (atauenis@yandex.ru)
 * (C) 2014, Evgeny Akhtimirov (wilbit@me.com)
 * Contributors should place own signs here.
 * (C) 2015, Andrius Krisiunas (akrisiun@gmail.com) https://github.com/akrisiun/fcmd
 */

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Collections;
using pluginner.Widgets;
using fcmd.base_plugins.fs;

namespace fcmd.View.Xaml
{
    // FileList rows data
    // XWT : public class ListView2Canvas : Canvas, IListView2Visual

    public class ListItemXaml : IListView2Visual
    {
        #region ctor

        // paramless ctor
        public ListItemXaml() : this(-1, -1, null, null, null) { }

        public static ListItemXaml FileItem(int rowIndex, object[] Data, IEnumerable<bool> EditableFields, string ItemTag = null)
        {
            return new ListItemXaml(ItemTag, Data, null, -1);
        }

        public static ListItemXaml DirectoryItem(int rowIndex, object[] Data, IEnumerable<bool> EditableFields, string ItemTag = null)
        {
            Debug.Assert((bool)(Data[FileListPanelWpf.idxIsDirectory]) == true);

            return new ListItemXaml(ItemTag, Data, null, -1);
        }

        /// <summary>Creates a new ListView2Item</summary>
        /// <param name="rowNumber">Number of owning row</param>
        /// <param name="colNumber">Number of owning column</param>
        /// <param name="rowTag">The item's tag</param>
        /// <param name="columns">Array of column information</param>
        /// <param name="data">The data that should be shown in this LV2I</param>
        /// <param name="font">Which font should be used</param>
        public ListItemXaml(int rowNumber, int colNumber, string rowTag,
            ListView2.ColumnInfo[] columns, IEnumerable<Object> data)
        {
            if (data == null)
                return;

            _Values = new object[FileListPanelWpf.idxCOUNT];   // == 6
            if (data is ICollection)
                (data as ICollection).CopyTo(_Values, 0);
            else
                // ToArray() -- not efective Linq.Enumerable
                _Values = System.Linq.Enumerable.ToArray<object>(data);

            _Cols = columns;
            Tag = rowTag;
        }

        public ListItemXaml(string rowTag, object[] data,
            ListView2.ColumnInfo[] columns = null, int rowNumber = -1)
        {
            _Values = data;
            _Cols = columns;
            Tag = rowTag;
        }

        #endregion

        #region Row data and state

        /// <summary>Data store</summary>
        protected Object[] _Values;

        public string FullPath {
            get {
                return // fldFile == ".." ? fldPath :
                       // (IsDirectory) ? fldPath :
                       (fldPath.StartsWith(LocalFileSystem.FilePrefix) ?
                            fldPath.Substring(LocalFileSystem.FilePrefix.Length) : fldPath);
                //Path.Combine(
                //    fldFile);
            }
        }

        // Protocol = "file://"; -> localFileSystem.FilePrefix

        public string fldPath { get { return Data[FileListPanelWpf.idxUrl] as string; } }

        public string fldFile { get { return Data[FileListPanelWpf.idxName] as string; } }
        public string fldSize { get { return Data[FileListPanelWpf.idxSize].ToString(); } }
        public Int64 SizeBytes { get { return (Int64)Data[FileListPanelWpf.idxSizeBytes]; } }
        public string fldModified { get { return Data[FileListPanelWpf.idxDatetime].ToString(); } }

        public bool IsDirectory { get { return (bool)Data[4]; } }

        // df.
        /// <summary>Column info store</summary>
        private ListView2.ColumnInfo[] _Cols;
        /// <summary>Selection state</summary>
		private ListView2.ItemStates _State;

        #endregion

        #region implement WPF state

        public bool CanGetFocus { get { return true; } set {; } }
        public object Content {
            get { return _Values; }
            set {
                if (value == null)
                {
                    _Values = null;
                    return;
                }
                var type = value.GetType();
                if (type.IsArray)
                    _Values = value as Object[];
                else if (value is IEnumerable)
                    _Values = Enumerable.ToArray<object>(value as IEnumerable<Object>);
                else
                    _Values = value as Object[];
            }
        }
        public object[] Data {
            get { return _Values; }
            set { _Values = value; }
        }
        public int? RowIndex { get; set; }
        public bool Visible { get; set; }
        public ListView2.ColumnInfo[] ColumnData { [DebuggerStepThrough] get { return _Cols; } set { _Cols = value; } }
        // + ListView2.ItemStates State

        #endregion

        #region Properties 

        /// <summary>Set column list</summary>
        public ListView2.ColumnInfo[] Columns {
            get { return _Cols; }
            set {
                _Cols = value;
                // gtk: QueueDraw()
            }
        }

        /// <summary>Status of the item selection</summary>
        public ListView2.ItemStates State {
            get { return _State; }
            set {
                _State = value;
                switch (value)
                {
                    //case ListView2.ItemStates.Pointed:
                    //    BackgroundColor = PointerBgColor;
                    //    CurFgColor = PointFgColor;
                    //    break;
                    //case ListView2.ItemStates.Selected:
                    //    BackgroundColor = SelBgColor;
                    //    CurFgColor = SelFgColor;
                    //    break;
                    case ListView2.ItemStates.PointedAndSelected:
                        break;
                    default:
                        //BackgroundColor = DefBgColor;
                        //CurFgColor = DefFgColor;
                        break;
                }
            }
        }

        public object Tag { get; set; }

        public override string ToString()
        {
            return _Values.Length > 0 ? _Values[0] as string : Tag as string;
        }

        #endregion

        #region Appeareance

        // TODO: dynamic recalculate
        public struct ItemAppeareance
        {
            public Color BackgroundColor { get { return default(Color); } set {; } }
            public Color Foreground { get { return default(Color); } set {; } }
        }

        #endregion

    }

}
