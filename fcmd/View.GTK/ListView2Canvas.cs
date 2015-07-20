/* The File Commander - plugin API - ListView2
 * The ListView2 item widget
 * (C) The File Commander Team - https://github.com/atauenis/fcmd
 * (C) 2014, Alexander Tauenis (atauenis@yandex.ru)
 * (C) 2014, Evgeny Akhtimirov (wilbit@me.com)
 * Contributors should place own signs here.
 * (C) 2015, Andrius Krisiunas (akrisiun@gmail.com)
 */

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
//using Xwt;
using Xwt.Drawing;
//using System.Drawing;
using System.Collections;
using pluginner.Widgets;
using System.IO;
using Xwt;

//using pluginner.Widgets;
//using System;
//using Xwt;

namespace fcmd.View.GTK
{
    public class ListView2Canvas : Canvas, IListView2Visual
    {
        /* not Impement

        public string fldFile
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string fldModified
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string fldSize
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int? RowIndex
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

        public ListView2.ItemStates State
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

            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
        */

        object IListView2Visual.Content
        {
            get { return base.Content; }
            set { base.Content = value as Widget; }
        }

        #region Row data and state

        /// <summary>Data store</summary>
        protected Object[] _Values;

        public object[] Data
        {
            get { return _Values; }
            set { _Values = value; }
        }

        public int? RowIndex { get; set; }
        // public bool Visible { get; set; }

        public ListView2.ColumnInfo[] ColumnData {[DebuggerStepThrough] get { return _Cols; } set { _Cols = value; } }

        // + ListView2.ItemStates State

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

        /// <summary>"Is the Field Editable" data store</summary>
        private bool[] _Editables;

        //public object Content
        //{
        //    get { return _Values; }
        //    set
        //    {
        //        if (value == null)
        //        {
        //            _Values = null;
        //            return;
        //        }
        //        var type = value.GetType();
        //        if (type.IsArray)
        //            _Values = value as Object[];
        //        else if (value is IEnumerable)
        //            _Values = Enumerable.ToArray<object>(value as IEnumerable<Object>);
        //        else
        //            _Values = value as Object[];
        //    }
        //}

        #endregion


        // public class ListView2Canvas : Canvas, IListView2Visual
        // paramless ctor
        public ListView2Canvas() : this(-1, -1, null, null, null, font: null) { }

        /// <summary>Creates a new ListView2Item</summary>
        /// <param name="rowNumber">Number of owning row</param>
        /// <param name="colNumber">Number of owning column</param>
        /// <param name="rowTag">The item's tag</param>
        /// <param name="columns">Array of column information</param>
        /// <param name="data">The data that should be shown in this LV2I</param>
        /// <param name="font">Which font should be used</param>
        public ListView2Canvas(int rowNumber, int colNumber, string rowTag,
               ListView2.ColumnInfo[] columns, IEnumerable<Object> data, Font font)
        {
            MinHeight = 16;
            HeightRequest = 16;
            MinWidth = 500;
            ExpandHorizontal = true;
            ExpandVertical = true;
            //RowNo = rowNumber;
            //ColNo = colNumber;
            // _FontToDraw = font;

            if (data == null)
                return;

            _Values = data.ToArray();
            _Cols = columns;
            Tag = rowTag;
            QueueDraw();
        }

        #region Properties 

        //public bool CanGetFocus { get { return true; } set {; } }

        /// <summary>Set column list</summary>
        public ListView2.ColumnInfo[] Columns
        {
            get { return _Cols; }
            set { _Cols = value; QueueDraw(); }
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
                QueueDraw();
            }
        }

        public object Tag { get; set; }

        //#if !WPF
        // object IListView2Visual.Content
        // public object[] Data
        // public int? RowIndex
        // public ListView2.ColumnInfo[] ColumnData


        // Cross plaform WPF case
        // protected void QueueDraw() { }

        public void OnDblClick()
        {
            OnButtonPressed(new ButtonEventArgs { MultiplePress = 2 });
        }

        /// <summary>
        /// Get or set the data. Note that the data should be written fully.
        /// </summary>
        //  was:public List<Object>
        public IEnumerable<object> DataL
        {
            get { return _Values.AsEnumerable<object>(); } // .ToList(); }
            set
            {
                if (_Cols == null)
                {
                    throw new Exception("Please set columns first!");
                }
                _Values = Enumerable.ToArray<object>(value); // . value.ToArray();
                QueueDraw();
            }
        }

        // [Obsolete("Not obsolete, but not implemented yet, do not use at now!")]
        // public event TypedEvent<EditableLabel> EditComplete = null;

        #endregion

        protected override void OnDraw(Context ctx, Rectangle dirtyRect)
        {
            base.OnDraw(ctx, dirtyRect);
            if (_Values.Length > _Cols.Length) return; //if the column count is less than the count of columns in the data, НАХУЙ ТАКУЮ РАБОТУ

            double PosByX = 0;
            for (int i = 0; i < _Values.Length; i++)
            {
                var Value = _Values[i];
                //if (_Cols[i].Visible)
                //    Draw(Value, PosByX, ctx, _Cols[i].Width, CurFgColor, _FontToDraw);

                if (_Cols.Length > i && i != _Cols.Length - 1)
                {
                    PosByX += _Cols[i].Width;
                }
            }
        }

        /// <summary>
        /// Draw a information on the ListView2Item
        /// </summary>
        /// <param name="What">What should be drawed</param>
        /// <param name="Where">Where (position by X) should be drawed</param>
        /// <param name="On">On what Drawing.Context the information should be drawed</param>
        /// <param name="MaxWidth">The limit of the picture's width</param>
        /// <param name="TextColor">The text foreground color</param>
        /// <param name="WhatFont">Which font is used to draw the onject</param>
        private void Draw(object What, double Where, Context On, double MaxWidth, Color TextColor, Font WhatFont)
        {
            if (What.GetType() != typeof(Image)
                && What.GetType() != typeof(pluginner.DirItem))
            {
                TextLayout tl = new TextLayout(this)
                {
                    Text = What.ToString(),
                    Font = WhatFont,
                    Width = MaxWidth,
                    Trimming = TextTrimming.WordElipsis
                };
                On.SetColor(TextColor);
                On.DrawTextLayout(tl, Where + 4, 0);
            }
            if (What is Image)
            {
                //On.DrawImage(What as Image, Where + 2, 0); 
                //undone: need to fix thread violation (presumably, WPF doesn't like that the image is 'assigned' to non-UI thread, and because of this sabotages drawing process)
            }
        }

        #region Appeareance

        // Appearance Append to ItemData {
        //    Font = Font.SystemSansSerifFont.WithWeight(FontWeight.Heavy),
        //    PointerBgColor = PointedBgColor,
        //    PointerFgColor = PointedFgColor,
        //    SelectionBgColor = SelectedBgColor,
        //    SelectionFgColor = SelectedFgColor,
        //    State = ItemStates.Default
        //};

        // TODO: dynamic recalculate
        public struct ItemAppeareance
        {

            /// <summary>Font the values will be written in</summary>
            //protected Font _FontToDraw;

            //protected Color DefBgColor;
            //protected Color DefFgColor;
            //protected Color PointBgColor;
            //protected Color PointFgColor;
            //protected Color SelBgColor;
            //protected Color SelFgColor;

            //protected Color CurFgColor;

            public Color BackgroundColor { get { return default(Color); } set {; } }
            public Color Foreground { get { return default(Color); } set {; } }

            //public int RowNo = -1;
            //public int ColNo = -1;
            public string Tag; //don't forgetting that the lv2 is used only for file list, so the tag can be only a string

        }

        //public Color NormalBgColor
        //{
        //    get { return DefBgColor; }
        //    set
        //    {
        //        DefBgColor = value;
        //        if (State == ListView2.ItemStates.Default)
        //            BackgroundColor = DefBgColor;
        //    }
        //}

        //public Color NormalFgColor
        //{
        //    get { return DefFgColor; }
        //    set
        //    {
        //        DefFgColor = value;
        //        if (State == ListView2.ItemStates.Default)
        //        {
        //            CurFgColor = value;
        //            QueueDraw();
        //        }
        //    }
        //}

        //public Color PointerBgColor
        //{
        //    get { return PointBgColor; }
        //    set
        //    {
        //        PointBgColor = value;
        //        if ((int)State == 1)
        //        {
        //            BackgroundColor = value;
        //        }
        //    }
        //}

        //public Color PointerFgColor
        //{
        //    get { return PointFgColor; }
        //    set
        //    {
        //        PointFgColor = value;
        //        if ((int)State == 1)
        //        {
        //            this.BackgroundColor = value;
        //        }
        //    }
        //}

        //public Color SelectionBgColor
        //{
        //    get { return SelBgColor; }
        //    set
        //    {
        //        SelBgColor = value;
        //        if ((int)State >= 2)
        //        {
        //            BackgroundColor = value;
        //        }
        //    }
        //}

        //public Color SelectionFgColor
        //{
        //    get { return SelFgColor; }
        //    set
        //    {
        //        SelFgColor = value;
        //        if ((int)State >= 2)
        //        {
        //            BackgroundColor = value;
        //        }
        //    }
        //}

        //        /// <summary>Set the font of the row</summary>
        //        public
        //#if XWT
        //            new 
        //#endif
        //            Font Font
        //        {
        //            get; set;
        //        }

        #endregion

    }

}