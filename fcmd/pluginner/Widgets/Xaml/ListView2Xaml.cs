using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pluginner.Widgets.Xaml
{
    public abstract class ListView2Xaml<T> : ListView3, IListingView<T>,   // , -> ListView2Widget
            IListView2<T>, ICollection<T>, IDisposable
            where T : class, IListView2Visual
    {
        public abstract object Content { get; set; }
        public abstract IList<T> DataItems { get; } // protected set
        
        // public abstract IEnumerable<T> ChoosedRows { get; }

        public abstract void Dispose();

        public IListingContainer<T> Parent { get; protected set; }

        public ListView2Xaml(IListingContainer<T> parent) : base()
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
        public IEnumerable<T> ChoosedRows
        {
            get
            {
                if (SelectedItems.Count == 0)
                {
                    List<T> list_one = new List<T>(PointedItem.Pointed);
                    return list_one;
                }
                // ReSharper disable once RedundantIfElseBlock //to ease readability
                else
                {
                    return SelectedItems;
                }
            }
        }

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

        public void Unselect() { Unselect(null); }
        // void IListingView<T>.

        public void Select(object item)
        {
            this.Select(item as T);
        }

        //   void IListingView.SetFocus()

        /// <summary>Clear selection of row</summary>
        /// <param name="Item">The row or null if need to unselect all</param>
        public void Unselect(T Item = null)
        {
            if (Item == null)
            {
                foreach (T lvi in SelectedItems)
                {
                    lvi.State = ListView2.ItemStates.Default;
                }
                SelectedItems.Clear();
            }
            else
            {
                Item.State = ListView2.ItemStates.Default;
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
                if (lvi.State == ListView2.ItemStates.Pointed || lvi.State == ListView2.ItemStates.PointedAndSelected)
                    lvi.State = ListView2.ItemStates.PointedAndSelected;
                else
                    lvi.State = ListView2.ItemStates.Selected;

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
        public void SetColumns(IEnumerable<ListView2.ColumnInfo> columns)
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
        private List<ListView2.ColumnInfo> _columns = new List<ListView2.ColumnInfo>();

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
                case ListView2.ItemStates.Default:
                    lvi.State = ListView2.ItemStates.Selected;
                    SelectedItems.Add(lvi);
                    break;
                case ListView2.ItemStates.Pointed:
                    lvi.State = ListView2.ItemStates.PointedAndSelected;
                    SelectedItems.Add(lvi);
                    break;
                case ListView2.ItemStates.Selected:
                case ListView2.ItemStates.PointedAndSelected:
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


        // ListView2Xaml
        public static ListView2.ColumnInfo[] DefaultXamlColumns()
        {
            return new ListView2.ColumnInfo[] {
                new ListView2.ColumnInfo { Index = 0, Tag = "fldFile", Title= "File", Width=200, Visible=true },
                new ListView2.ColumnInfo { Index = 1, Tag = "fldSize", Title= "Size", Width=90, Visible=true },
                new ListView2.ColumnInfo { Index = 2, Tag = "fldModified", Title= "Modified", Width=110, Visible=true }
            };
        }
    }

}