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
using System.IO;

namespace fcmd.View.Xaml
{
    // FileList rows data
    // XWT : public class ListView2Canvas : Canvas, IListView2Visual

    public class ListView2ItemWpf : IListView2Visual
    {
        #region ctor

        // paramless ctor
        public ListView2ItemWpf() : this(-1, -1, null, null, null) { }

        /// <summary>Creates a new ListView2Item</summary>
        /// <param name="rowNumber">Number of owning row</param>
        /// <param name="colNumber">Number of owning column</param>
        /// <param name="rowTag">The item's tag</param>
        /// <param name="columns">Array of column information</param>
        /// <param name="data">The data that should be shown in this LV2I</param>
        /// <param name="font">Which font should be used</param>
        /// XWT : ListView2Canvas(int rowNumber, int colNumber, string rowTag,
        ///                ListView2.ColumnInfo[] columns, IEnumerable<Object> data, Font font)
        public ListView2ItemWpf(int rowNumber, int colNumber, string rowTag,
            ListView2.ColumnInfo[] columns, IEnumerable<Object> data)
        {
            if (data == null)
                return;

            _Values = data.ToArray();
            _Cols = columns;
            Tag = rowTag;
            // QueueDraw();
        }

        #endregion

        #region Row data and state

        /// <summary>Data store</summary>
        protected Object[] _Values;

        public string FullPath
        {
            get
            {
                return // fldFile == ".." ? fldPath :
                       Path.Combine(
                            fldPath.StartsWith(Protocol) ? fldPath.Substring(Protocol.Length) : fldPath,
                            fldFile);
            }
        }

        public const string Protocol = "file://";

        public string fldPath { get { return Data[0] as string; } }

        public string fldFile { get { return Data[1] as string; } }
        public string fldSize { get { return Data[2].ToString(); } }
        public string fldModified { get { return Data[3].ToString(); } }
        // df.

        /// <summary>Column info store</summary>
        private ListView2.ColumnInfo[] _Cols;
        /// <summary>Selection state</summary>
		private ListView2.ItemStates _State;

        #endregion

        #region implement WPF state

        public bool CanGetFocus { get { return true; } set {; } }
        public object Content
        {
            get { return _Values; }
            set
            {
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
        public object[] Data
        {
            get { return _Values; }
            set { _Values = value; }
        }
        public int? RowIndex { get; set; }
        public bool Visible { get; set; }
        public ListView2.ColumnInfo[] ColumnData {[DebuggerStepThrough] get { return _Cols; } set { _Cols = value; } }
        // + ListView2.ItemStates State

        #endregion

        #region Properties 

        /// <summary>Set column list</summary>
        public ListView2.ColumnInfo[] Columns
        {
            get { return _Cols; }
            set
            {
                _Cols = value; // QueueDraw(); 
            }
        }

        /// <summary>Status of the item selection</summary>
        public ListView2.ItemStates State
        {
            get { return _State; }
            set
            {
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

                        //todo: replace this buggy algorythm with better one
                        //дело в том, что xwt немного путает одинаковые цвета,
                        //на минимальные доли, но этого достаточно для color1!=color2

                        //if (PointBgColor == NormalBgColor)
                        //{
                        //    BackgroundColor = SelectionBgColor;
                        //    CurFgColor = SelectionFgColor;
                        //}
                        //else
                        //{
                        //    BackgroundColor =
                        //        SelBgColor.BlendWith(
                        //        PointBgColor, 0.5
                        //        );
                        //    CurFgColor =
                        //        SelFgColor.BlendWith(
                        //        PointFgColor, 0.5
                        //        );

                        //}
                        break;
                    default:
                        //BackgroundColor = DefBgColor;
                        //CurFgColor = DefFgColor;
                        break;
                }
                // QueueDraw();
            }
        }

        public object Tag { get; set; }

        // Cross plaform WPF case
        // protected void QueueDraw() { }
        // public void OnDblClick()
        //    OnButtonPressed(new ButtonEventArgs { MultiplePress = 2 });

        /// <summary>
        /// Get or set the data. Note that the data should be written fully.
        /// </summary>
        //  was:public List<Object>
        //public IEnumerable<object> DataL
        //{
        //    get { return _Values.AsEnumerable<object>(); } // .ToList(); }
        //    set
        //    {
        //        if (_Cols == null)
        //        {
        //            throw new Exception("Please set columns first!");
        //        }
        //        _Values = Enumerable.ToArray<object>(value); // . value.ToArray();
        //        QueueDraw();
        //    }
        //}

        // [Obsolete("Not obsolete, but not implemented yet, do not use at now!")]
        // public event TypedEvent<EditableLabel> EditComplete = null;

        #endregion

        #region Appeareance

        // protected override void OnDraw(Context ctx, Rectangle dirtyRect)
        // Draw a information on the ListView2Item
        // private void Draw(object What, double Where, Context On, double MaxWidth, Color TextColor, Font WhatFont)

        // TODO: dynamic recalculate
        public struct ItemAppeareance
        {
            /// <summary>Font the values will be written in</summary>
            // protected Font _FontToDraw;

            public Color BackgroundColor { get { return default(Color); } set {; } }
            public Color Foreground { get { return default(Color); } set {; } }

            // public int RowNo = -1;
            // public int ColNo = -1;

            // public string Tag; //don't forgetting that the lv2 is used only for file list, so the tag can be only a string

        }

        #endregion

    }

}
