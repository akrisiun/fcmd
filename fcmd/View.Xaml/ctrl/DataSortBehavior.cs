using fcmd.View.Xaml;
using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data; 

namespace fcmd.View.ctrl
{
    // http://stackoverflow.com/questions/18122751/wpf-datagrid-customsort-for-each-column/18218963#
    // http://mikestedman.blogspot.lt/2012/07/wpf-datagrid-custom-column-sorting.html
    public interface IDirectionComparer : IComparer
    {
        ListSortDirection SortDirection { get; set; }
    }

    public static class SortBehavior
    {
        static SortBehavior() { EventHandler = new DataGridSortingEventHandler(Handler); }
        public static DataGridSortingEventHandler EventHandler { get; private set; }

        private static void Handler(object sender, DataGridSortingEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            if (dataGrid == null) // || !GetAllowCustomSort(dataGrid)) 
                return;
            var source = dataGrid.GetValue(ItemsControl.ItemsSourceProperty);
            if (source == null)
                return;
            Handle(dataGrid, source, e);
        }

        public static void Handle(DataGrid dataGrid, object source, DataGridSortingEventArgs e)
        {
            ListCollectionView listColView = (ListCollectionView)CollectionViewSource.GetDefaultView(source);

            // Sanity check <ListItemXaml>
            var path = e.Column.SortMemberPath;
            int index = path == "fldSize" ? 1 : path == "fldModified" ? 2 : 0;
            var direction = (e.Column.SortDirection != ListSortDirection.Ascending)
                                ? ListSortDirection.Ascending
                                : ListSortDirection.Descending;

            IDirectionComparer sorter = new FilterComparer { SortDirection = direction, Index = index };
            e.Handled = true;

            e.Column.SortDirection = direction;
            listColView.CustomSort = sorter;
        }
    }


    public class FilterComparer : IDirectionComparer
    {
        public ListSortDirection SortDirection { get; set; }
        public int Index { get; set; }

        public int Compare(object a, object b)
        {
            var objA = a as ListItemXaml;
            if (objA.Tag as string == "..")
                return -1;
            var objB = b as ListItemXaml;

            int index = (Index == 0) ? 0 : (Index + 1);
            int result = 0;
            if (objA.IsDirectory != objB.IsDirectory || objB.Tag as string == "..")
            {
                // if (SortDirection == ListSortDirection.Ascending)
                result = objB.IsDirectory ? 1 : -1;
            }
            if (result != 0)
                return result;

            if (index == FileListPanelWpf.idxSize)
            {
                Int64 diff = objB.SizeBytes - objA.SizeBytes;
                if (diff > Int32.MaxValue)
                    result = Int32.MaxValue;
                else if (diff < Int32.MinValue)
                    result = Int32.MinValue;
                else
                    result = Convert.ToInt32(diff);
            }
            else
            {
                string valueA = objA.Data[index].ToString();
                string valueB = objB.Data[index].ToString();
                if (SortDirection == ListSortDirection.Ascending)
                {
                    // returns: A 32-bit signed integer that indicates the lexical relationship between the
                    //     two comparands.Value Condition Less than zero strA is less than strB. Zero
                    //     strA equals strB. Greater than zero strA is greater than strB.
                    result = string.Compare(valueA, valueB);
                }
                else
                {
                    result = string.Compare(valueB, valueA);
                }

                if (result != 0)
                    return result;

                valueA = objA.ToString();
                valueB = objB.ToString();
                result = string.Compare(valueA, valueB);
            }
            if (SortDirection == ListSortDirection.Descending)
                result = result * -1;

            return result;
        }
    }
}
