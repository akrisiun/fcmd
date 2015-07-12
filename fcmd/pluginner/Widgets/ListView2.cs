/* The File Commander - plugin API - ListView2
 * The enhanced ListView widget
 * (C) The File Commander Team - https://github.com/atauenis/fcmd
 * (C) 2014, Alexander Tauenis (atauenis@yandex.ru)
 * (C) 2014, Evgeny Akhtimirov (wilbit@me.com)
* Contributors should place own signs here.
 */

using System;
using System.Diagnostics;
using System.Collections.Generic;
using pluginner.Toolkit;
using Xwt;
using Xwt.Drawing;
using System.Collections;

namespace pluginner.Widgets
{

    // File

#if XWT
    /// <summary>Modern listview widget</summary>
    public class ListView2 : Widget, IListView2
    {

        private VBox Layout = new VBox();
        private HBox ColumnRow = new HBox();

        public HeavyScroller ScrollerIn = new HeavyScroller(); //vertical scroller
        public ScrollView ScrollerOut = new ScrollView();   //horizontal scroller
        private List<Label> ColumnTitles = new List<Label>();
        private Table Grid = new Table();
#else 

    public abstract class ListView2<T> : ListView2, IListingView,   // , -> ListView2Widget
        IListView2<T>, ICollection<T>, IDisposable
        where T : class, IListView2Visual
    {
        public abstract object Content { get; set; }
        public abstract IList<T> DataItems { get; } // protected set

        // public abstract void SetupColumns();
        public abstract void Dispose();

        public IUIListingView<T> Parent { get; protected set; }

        public ListView2(IUIListingView<T> parent) : base()
        {
            Parent = parent;
        }

        public int Count { get { return DataItems.Count; } }
        public bool IsReadOnly { get { return DataItems.IsReadOnly; } }

        public object Tag { get; set; }
        public int SelectedRow { get { new NotImplementedException("no SelectedRow"); return -1; } set {; } }

        public bool Sensitive { get; set; }
#if WPF
        public System.Windows.Input.CursorType Cursor { get; set; }
#endif

        /// <summary>Add a new item</summary>
        /// <param name="Data">The item's content</param>
        /// <param name="EditableFields">List of editable fields</param>
        /// <param name="ItemTag">The tag for the new item (optional)</param>
        public void AddItem(IEnumerable<Object> Data, IEnumerable<Boolean> EditableFields, string ItemTag = null)
        {
            T lvi = Activator.CreateInstance<T>();
            if (lvi == null)
                return;
            lvi.Content = Data;
            AddItem(lvi);
        }

        #region T type implement

        /// <summary>The pointed item</summary>
        public IPointedItem<T> PointedItem { get; set; }
        //public IPointedItem // IListView2<T> // PointedItem {[DebuggerStepThrough] get { return PointedItem; } set { PointedItem = value as T; } }

        /// <summary>The list of selected DataItems</summary>
        public List<T> SelectedItems = new List<T>();

        /// <summary>The rows that are allowed to be pointed by keyboard OR null if all rows are allowed</summary>
        /// <summary>Gets the list of the rows that currently are choosed by the user</summary>
        public IEnumerable<T> ChoosedRowsTyped
        {
            get
            {
                if (SelectedItems.Count == 0)
                {
                    List<T> list_one = new List<T>(PointedItem.Pointed); // { PointedItem.Item };
                    return list_one;
                }
                // ReSharper disable once RedundantIfElseBlock //to ease readability
                else
                {
                    return SelectedItems;
                }
            }
        }

        public abstract IEnumerable ChoosedRows { get; }

        public abstract bool Contains(T item);
        public abstract void Add(T item);
        public abstract bool Remove(T item);
        public abstract void CopyTo(T[] item, int arrayIndex);
        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
        public IEnumerator<T> GetEnumerator() { return DataItems.GetEnumerator(); }


        /// <summary>Add a new T into this ListView2</summary>
        /// <param name="Item">The new T</param>
        private void AddItem(T Item)
        {
            Add(Item);
#if XWT
            //if (Color2)
            //{
            //    Item.NormalBgColor = NormalBgColor2;
            //    Item.NormalFgColor = NormalFgColor1;
            //}
            //else
            //{
            //    Item.NormalBgColor = NormalBgColor1;
            //    Item.NormalFgColor = NormalFgColor1;
            //}

            //Color2 = !Color2;


            //Grid.Add(Item, LastCol, LastRow, 1, 1, true);
            //Item.ButtonPressed += Item_ButtonPressed;
            //Item.EditComplete += sender =>
            //{
            //    var handler = EditComplete;
            //    if (handler != null)
            //    {
            //        handler(sender, this);
            //    }
            //};
#endif
            //Item.CanGetFocus = true;
            //if (LastRow == 0) _SetPoint(Item);
            //LastRow++;
        }

        /// <summary>Removes the specifed item from the list</summary>
        /// <param name="Item">The item</param>
        public void RemoveItem(T Item)
        {
            //Note that the removing item is simply hided.
            //To remove it completely, call Clear() sub-programm. But all other rows will be also removed.
            Item.Visible = false;
        }

        /// <summary>Gets pointer for the T at specifed row №</summary>
        /// <param name="Row">The row's number</param>
        /// <returns>A pointer to the ListView2 Item</returns>
        public T GetItem(int Row)
        {
            return DataItems[Row];
        }

        /// <summary>Purges the ListView2 (deletes all DataItems from display and memory). Useful when memory leaks are happen.</summary>
        public virtual void Clear()
        {
            //Grid.Clear();
            DataItems.Clear();
            // LastRow = LastCol = 0;
            PointedItem = null;
        }

        #endregion

        #region Selection methods

        void IListingView.Unselect() { Unselect(null); }
        void IListingView.Select(object item)
        {
            this.Select(item as T);
        }

        //             void IListingView.SetFocus()


        /// <summary>Clear selection of row</summary>
        /// <param name="Item">The row or null if need to unselect all</param>
        public void Unselect(T Item = null)
        {
            if (Item == null)
            {
                foreach (T lvi in SelectedItems)
                {
                    lvi.State = ItemStates.Default;
                }
                SelectedItems.Clear();
            }
            else
            {
                Item.State = ItemStates.Default;
                SelectedItems.Remove(Item);
            }

            RaiseSelectionChanged(SelectedItems);
        }

        /// <summary>Selects an row</summary>
        /// <param name="Item">The row or null if need to select all rows</param>
        public void Select(T Item = null)
        {
            if (Item != null)
            {
                _SelectItem(Item);
                return;
            }

            SelectedItems.Clear();
            foreach (T lvi in DataItems)
            {
                if (lvi.State == ItemStates.Pointed || lvi.State == ItemStates.PointedAndSelected)
                    lvi.State = ItemStates.PointedAndSelected;
                else
                    lvi.State = ItemStates.Selected;

                SelectedItems.Add(lvi);
            }

            RaiseSelectionChanged(SelectedItems);
        }

        /// <summary>Inverts selection of items (like the "[*] gray" key)</summary>
        public void InvertSelection()
        {
            foreach (T lvi in DataItems)
            {
                if ((int)lvi.State >= 2)
                {
                    _UnselectItem(lvi);
                }
                else
                {
                    _SelectItem(lvi);
                }
            }
            RaiseSelectionChanged(SelectedItems);
        }

        /// <summary>Scrolls the internal scroll view to the specifed row</summary>
        /// <param name="rowno">The row's number</param>
        public void ScrollToRow(int rowno)
        {
            //double Y = DataItems[0].Surface.GetPreferredSize().Height * rowno;
            //ScrollerIn.ScrollTo(Y);
        }

        #endregion

        #region PUBLIC EVENTS

        public event TypedEvent<T> PointerMoved;
        public event TypedEvent<List<T>> SelectionChanged;
        public event TypedEvent<T> PointedItemDoubleClicked;
        // public event TypedEvent<EditableLabel, ListView2> EditComplete;

        protected void RaiseSelectionChanged(List<T> data)
        {
            var handler = SelectionChanged;
            if (handler != null)
            {
                handler(data);
            }
        }

        #endregion
        #region PUBLIC PROPERTIES

        /// <summary>Sets column configuration</summary>
        public void SetColumns(IEnumerable<ColumnInfo> columns)
        {
            _columns.Clear();
            //ColumnTitles.Clear();
            //ColumnRow.Clear();
            //foreach (ColumnInfo ci in columns)
            //{
            //    _columns.Add(ci);
            //    ColumnTitles.Add(new Label(ci.Title) { WidthRequest = ci.Width, Visible = ci.Visible });
            //    ColumnRow.PackStart(ColumnTitles[ColumnTitles.Count - 1]);
            //}
        }

        /// <summary>Defines visiblity of the widget's border</summary>
        //public bool BorderVisible
        //{
        //    get { return ScrollerOut.BorderVisible; }
        //    set { ScrollerOut.BorderVisible = value; }
        //}

        /// <summary>Selected row's number</summary>
        //public int SelectedRow
        //{
        //    get { return PointedItem.RowNo; }
        //    set { _SetPoint(DataItems[value]); }
        //}

        // TODO: int MaxRow (для переноса при режиме Small Icons)
        private List<ColumnInfo> _columns = new List<ColumnInfo>();

        #endregion
        #region SUB-PROGRAMS

        /// <summary>
        /// Sets the pointer to an item by defined condition.
        /// </summary>
        /// <param name='Condition'>
        /// Условие (на сколько строк переместиться)
        /// </param>
        private void _SetPointerByCondition(int Condition)
        {
            /*ОПИСАНИЕ: Перенос курсора выше или ниже.
			  ПРИНЦИП: При наличии списка допущенных к выбору строк (массив номеров строк AllowedToPoint),
			  курсор прыгает в ближайшую допущенную строку в прямом направлении. При выходе из сего списка,
			  курсор может идти в том же направлении дальше без ограничений.
			  */
            //int NewRow;

            //if (Condition > 0)
            //{
            //    //move bottom
            //    NewRow = PointedItem.RowNo + Condition;
            //    foreach (int r in AllowedToPoint)
            //    {
            //        if (r > NewRow - 1)
            //        {
            //            NewRow = r; break;
            //        }
            //    }

            //    if (NewRow < LastRow)
            //        _SetPoint(DataItems[NewRow]);
            //}
            //else if (Condition < 0)
            //{
            //    //move up
            //    NewRow = PointedItem.RowNo - -Condition;
            //    for (int i = AllowedToPoint.Count - 1; i > 0; i--)
            //    {
            //        int r = AllowedToPoint[i];
            //        if (r < NewRow)
            //        {
            //            NewRow = r; break;
            //        }
            //    }
            //    if (NewRow >= 0)
            //        _SetPoint(DataItems[NewRow]);
            //}
        }

        /// <summary>Inverts selection of an item</summary>
        /// <param name="lvi">The requested T</param>
        private void _SelectItem(T lvi)
        {
            switch (lvi.State)
            {
                case ItemStates.Default:
                    lvi.State = ItemStates.Selected;
                    SelectedItems.Add(lvi);
                    break;
                case ItemStates.Pointed:
                    lvi.State = ItemStates.PointedAndSelected;
                    SelectedItems.Add(lvi);
                    break;
                case ItemStates.Selected:
                case ItemStates.PointedAndSelected:
                    _UnselectItem(lvi);
                    break;
            }
            RaiseSelectionChanged(SelectedItems);
        }

        /// <summary>Removes selection of an item</summary>
        /// <param name="lvi">The requested T</param>
        private void _UnselectItem(T lvi)
        {
            SelectedItems.Remove(lvi);
            //if (lvi.State == ItemStates.PointedAndSelected)
            //    lvi.State = ItemStates.Pointed;
            //else
            //    lvi.State = ItemStates.Default;
            RaiseSelectionChanged(SelectedItems);
        }

        #endregion
    }

#endif

    public abstract class ListView2 : IListView2
    {
        public abstract System.Drawing.Font FontForFileNames { get; set; }

        // abstract GUI events
        public abstract void SetFocus();
        public abstract void SetupColumns();

        // private int LastRow;
        // private int LastCol;
        private Views _View = Views.Details;

        // private bool Color2; //для обеспечения чередования цветов строк
        private DateTime PointedItemLastClickTime = DateTime.Now.AddDays(-1); //for double click detecting

        public static double MillisecondsForDoubleClick = SysInfo.DoubleClickTime; //Depends on user settings

        ////Color sheme
        //public Color NormalBgColor1 = Colors.White;
        //public Color NormalBgColor2 = Colors.WhiteSmoke;
        //public Color NormalFgColor1 = Colors.Black;
        //public Color NormalFgColor2 = Colors.Black;
        //public Color PointedBgColor = Colors.LightGray;
        //public Color PointedFgColor = Colors.Black;
        //public Color SelectedBgColor = Colors.White;
        //public Color SelectedFgColor = Colors.Red;

        // public Font FontForFileNames = Font.SystemFont;

        //For virtual mode
        int VisibleItemsByY = -1;
        // int VisibleItemsByX = -1;

        /// <summary>List of items. Please do not edit directly! Please use the AddItem and RemoveItem functions.</summary>
        // List<T> IListView2.DataItems = new List<T>();

        public List<int> AllowedToPoint = new List<int>();

        public ListView2()
        {
#if XWT
            Layout.Spacing = 0;
            Grid.DefaultRowSpacing = 0;
            Content = ScrollerOut;

            ScrollerOut.Content = Layout;
            ScrollerOut.VerticalScrollPolicy = ScrollPolicy.Never;

            Content = ScrollerIn;
            ScrollerIn.Content = Grid;
            ScrollerIn.CanScrollByX = false;// ScrollPolicy.Never;
            Layout.PackStart(ColumnRow);

            Layout.KeyPressed += Layout_KeyPressed;
            Layout.CanGetFocus = true;
            CanGetFocus = true;
            KeyPressed += Layout_KeyPressed;

            BoundsChanged += ListView2_BoundsChanged;

            ScrollerIn.BackgroundColor = Colors.White;
#endif
        }

#if XWT

        //EVENT HANDLERS

        void ListView2_BoundsChanged(object sender, EventArgs e)
        {
            //if the calculation is possible
            if (Items != null && Items.Count > 0)
            {
                Size mySize = Size;
                Size oneItemSize = Items[0].Size;

                if (_View == Views.Details)
                {
                    //table mode
                    double visibleHeight = mySize.Height;
                    double itemHeight = oneItemSize.Height;

                    for (double i = 0; i < visibleHeight; i += itemHeight)
                    {
                        VisibleItemsByY++;
                    }

                    // VisibleItemsByX = 0;
                }
            }
        }

        private void Item_ButtonPressed(object sender, ButtonEventArgs e)
        {
            SetFocus();
            T lvi = sender as T;
            //currently, the mouse click policy is same as in Total and Norton Commander
            if (e.Button == PointerButton.Right)//right click - select & do nothing
            {
                _SelectItem(lvi);
                return;
            }

            if (e.Button == PointerButton.Left)//left click - point & don't touch selection
            {
                if (lvi == PointedItem)
                {
                    double MillisecondsPassed = (DateTime.Now - PointedItemLastClickTime).TotalMilliseconds;
                    if (MillisecondsPassed < MillisecondsForDoubleClick)
                    {
                        PointedItemDoubleClicked(PointedItem);
                        // The last click was so long long ago that the next one can't be double click
                        PointedItemLastClickTime = DateTime.Now.AddDays(-1);
                    }
                    else
                    {
                        PointedItemLastClickTime = DateTime.Now;
                    }
                }
                else
                {
                    _SetPoint(lvi);
                    PointedItemLastClickTime = DateTime.Now;
                }
            }
        }

        private void Layout_KeyPressed(object sender, KeyEventArgs e)
        {
#if DEBUG
            //initiated by GH issue #10, but may give a help in the future too...
            Console.WriteLine("LV2 DEBUG: pressed {0}, repeat={1}, handled={2}", e.Key, e.IsRepeat, e.Handled);
#endif
            //currently, the keyboard feel is same as in Norton & Total Commanders
            switch (e.Key)
            {
                case Key.Up: //[↑] - move cursor up
                    _SetPointerByCondition(-1);
                    e.Handled = true;
                    return;
                case Key.Down: //[↓] - move cursor bottom
                    _SetPointerByCondition(+1);
                    e.Handled = true;
                    return;
                case Key.Insert: //[Ins] - set selection & move pointer bottom
                    _SelectItem(PointedItem);
                    _SetPointerByCondition(+1);
                    e.Handled = true;
                    return;
                case Key.Return: //[↵] - same as double click
                    PointedItem.OnDblClick();
                    e.Handled = true;
                    return;
                case Key.NumPadMultiply: //gray [*] - invert selection
                    InvertSelection();
                    e.Handled = true;
                    return;
                case Key.Home:
                    _SetPoint(Items[0]);
                    return;
                case Key.End:
                    _SetPoint(Items[Items.Count - 1]);
                    return;
            }
        }

    

        /// <summary>Sets the pointer to an item</summary>
        /// <param name="lvi">The requested T</param>
        private void _SetPoint(T lvi)
        {
            //unpoint current
            if (PointedItem != null)
            {
                if ((int)PointedItem.State > 1)
                    PointedItem.State = ItemStates.Selected;
                else
                    PointedItem.State = ItemStates.Default;
            }

            //point new
            if ((int)lvi.State > 1)
                lvi.State = ItemStates.PointedAndSelected;
            else
                lvi.State = ItemStates.Pointed;
            PointedItem = lvi;

            var pointerMoved = PointerMoved;
            if (pointerMoved != null)
            {
                pointerMoved(lvi);
            }

            //if need, scroll the view
            double top = -ScrollerIn.PosY;
            double down = ScrollerIn.Size.Height;
            double newpos = lvi.Size.Height * lvi.RowNo;

            if (top > down)
            {
                //если прокручено далее первой страницы
                down = top + ScrollerIn.Size.Height;
            }

            if (newpos > down || newpos < top)
            {
                ScrollerIn.ScrollTo(-(lvi.Size.Height * lvi.RowNo));
            }

            //todo: add smooth scrolling
        }

        //PUBLIC MEMBERS

        /// <summary>Imitates a press of a keyboard key</summary>
        /// <param name="kea">The key to be "pressed"</param>
        public new void OnKeyPressed(KeyEventArgs kea)
        {
            base.OnKeyPressed(kea);
        }
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
        }

        #endregion

        public virtual ColumnInfo[] DefineColumns(DataFieldNumbers df)
        {
            //public int DisplayName = 2;      // dfDisplayName
            //public int Size = 3;
            //public int Changed = 4;

            return new ColumnInfo[] {
                new ColumnInfo { Index = 0, Tag = "fldFile", Title= "File", Width=200, Visible=true },
                new ColumnInfo { Index = 1, Tag = "fldSize", Title= "Size", Width=90, Visible=true },
                new ColumnInfo { Index = 2, Tag = "fldModified", Title= "Modified", Width=110, Visible=true }
            };
        }

    }
}
