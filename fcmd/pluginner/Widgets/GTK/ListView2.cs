/* The File Commander - plugin API - ListView2
 * The enhanced ListView widget
 * (C) The File Commander Team - https://github.com/atauenis/fcmd
 * (C) 2014, Alexander Tauenis (atauenis@yandex.ru)
 * (C) 2014, Evgeny Akhtimirov (wilbit@me.com)
* Contributors should place own signs here.
 */

using System;
using System.Collections.Generic;
using pluginner.Toolkit;
using Xwt;
using Xwt.Drawing;
using System.Collections;
using System.Drawing;

namespace pluginner.Widgets
{
    // File

    /// <summary>abstract listview widget</summary>
    public abstract class ListView2 : Widget, IListView2
    {

#if XWT
        private VBox Layout = new VBox();
        private HBox ColumnRow = new HBox();

        public HeavyScroller ScrollerIn = new HeavyScroller(); //vertical scroller
        public ScrollView ScrollerOut = new ScrollView();   //horizontal scroller
        private List<Label> ColumnTitles = new List<Label>();
        private Table Grid = new Table();

        // ListView2.ItemStates State { get; set; }
        public int RowNo { get; set; }
        public Xwt.Drawing.Font FontForFileNames { get; set; }

        public ItemStates State { get; set; }
        public abstract ColumnInfo[] DefineColumns(DataFieldNumbers df);
        public abstract void SetupColumns();
        public abstract bool ColumnsSet { get; }
#endif

        #region ENUMS & STRUCTS

        /// <summary>
        /// Defines how the items are displayed in the control.
        /// </summary>
        public enum Views
        {
            SmallIcons, LargeIcons, Details
        }

        /// <summary>
        /// Enumeration of items' selection statuses
        /// </summary>
        public enum ItemStates
        {
            /// <summary>Default item state (not selected nor pointed)</summary>
            Default = 0,
            /// <summary>The item is pointed, but not selected</summary>
            Pointed = 1,
            /// <summary>The item is selected</summary>
            Selected = 2,
            /// <summary>The item is pointed and selected</summary>
            PointedAndSelected = 3
        }

        /// <summary>
        /// Structure, that contains information about columns
        /// </summary>
        public struct ColumnInfo
        {
            public string Title;
            public object Tag;
            public double Width;
            public bool Visible;
            public int Index;
            public int VerticalAlign;
        }

        #endregion

    }

    // for Xaml
    public abstract class ListView3 : IListView2
    {
        public abstract Xwt.Drawing.Font FontForFileNames { get; set; }
        public abstract void SetFocus();
        public abstract void SetupColumns();
        public abstract bool ColumnsSet { get; }

        public abstract ListView2.ColumnInfo[] DefineColumns(DataFieldNumbers df);

        public ListView2.ItemStates State { get; set; }

    }

}
