using pluginner.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xwt;
using Xwt.Drawing;

namespace pluginner.Widgets.GTK
{
    public class ListView2Canvas<T> : ListView2, IListView2
    {

        // abstract GUI events

        //public abstract void SetFocus();
        //public abstract void SetupColumns();

        //private int LastRow;
        //private int LastCol;
        private ListView2.Views _View = ListView2.Views.Details;

        // private bool Color2; //для обеспечения чередования цветов строк
        private DateTime PointedItemLastClickTime = DateTime.Now.AddDays(-1); //for double click detecting

        public static double MillisecondsForDoubleClick = SysInfo.DoubleClickTime; //Depends on user settings

        ////Color sheme
        public Color NormalBgColor1 = Colors.White;
        public Color NormalBgColor2 = Colors.WhiteSmoke;
        public Color NormalFgColor1 = Colors.Black;
        public Color NormalFgColor2 = Colors.Black;
        public Color PointedBgColor = Colors.LightGray;
        public Color PointedFgColor = Colors.Black;
        public Color SelectedBgColor = Colors.White;
        public Color SelectedFgColor = Colors.Red;

        // public Font FontForFileNames = null;

        //For virtual mode
        int VisibleItemsByY = -1;
        int VisibleItemsByX = -1;

        /// <summary>List of items. Please do not edit directly! Please use the AddItem and RemoveItem functions.</summary>
        //  List<T> IListView2.DataItems = new List<T>();

        public List<int> AllowedToPoint = new List<int>();

        public ListView2Canvas()
        {
            FontForFileNames = Font.SystemFont;

#if XWT_TODO
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

#if XWT_TODO

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
#if GTK_TODO
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
#endif
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
            private void _SetPoint<T>(T lvi)
            {
#if GTK_TODO
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
#endif
                //todo: add smooth scrolling
            }

            //PUBLIC MEMBERS

            /// <summary>Imitates a press of a keyboard key</summary>
            /// <param name="kea">The key to be "pressed"</param>
            //public new void OnKeyPressed(KeyEventArgs kea)
            //{
            //    base.OnKeyPressed(kea);
            //}
#endif

        public override ListView2.ColumnInfo[] DefineColumns(DataFieldNumbers df)
        {
            //public int DisplayName = 2;      // dfDisplayName
            //public int Size = 3;
            //public int Changed = 4;

            return new ListView2.ColumnInfo[] {
                new ListView2.ColumnInfo { Index = 0, Tag = "fldFile", Title= "File", Width=200, Visible=true },
                new ListView2.ColumnInfo { Index = 1, Tag = "fldSize", Title= "Size", Width=90, Visible=true },
                new ListView2.ColumnInfo { Index = 2, Tag = "fldModified", Title= "Modified", Width=110, Visible=true }
            };
        }

        public override void SetupColumns()
        {
            throw new NotImplementedException();
        }

    }

}
