using System;
#if !__MonoCS__
using Microsoft.VisualStudio.TestTools.UnitTesting;
using fcmd.http;
using pluginner.Toolkit;
using fcmd.Model;
using fcmd.View.ctrl;
using System.Windows.Controls;
using System.Windows.Input;
using System.ComponentModel;
#endif

namespace fcmd.test
{
    [TestClass]
    public class UnitDataSortTest
    {
        [TestMethod]
        public void Sort1()
        {
            TestApp.MainWindowShow();
            var main = MainWindow.ActiveWindow;
            var panel = main.ActivePanelWpf;
            var dataGrid = panel.ListingWidget;

            var data = main.WindowDataWpf as WindowDataWpf;

            var source = dataGrid.GetValue(ItemsControl.ItemsSourceProperty);
            var listingPanel = panel.ListingWidget;
            listingPanel.LoadDir(@"c:\");
            dataGrid.SetValue(ItemsControl.ItemsSourceProperty, listingPanel.DataItems);
            dataGrid.Sorting += SortBehavior.EventHandler;

            PerformSort(dataGrid, dataGrid.Columns[1]);
            PerformSort(dataGrid, dataGrid.Columns[1], ListSortDirection.Ascending);
            PerformSort(dataGrid, dataGrid.Columns[2]);

            TestApp.AppRun(data);
        }

        // http://referencesource.microsoft.com/#PresentationFramework/src/Framework/System/Windows/Controls/DataGrid.cs,26406406cca70129
        void PerformSort(DataGrid dataGrid, DataGridColumn sortColumn, ListSortDirection direction = ListSortDirection.Descending)
        {
            DataGridSortingEventArgs eventArgs = new DataGridSortingEventArgs(sortColumn);
            eventArgs.Column.SortDirection = direction;

            // OnSorting:
            DataGridSortingEventHandler Sorting = SortBehavior.EventHandler;
            eventArgs.Handled = false;

            // ListCollectionView listColView = (ListCollectionView)CollectionViewSource.GetDefaultView(source);
            if (Sorting != null)
            {
                Sorting(dataGrid, eventArgs);
            }
            // else if (!eventArgs.Handled) { 
            //         DefaultSort(eventArgs.Column, (Keyboard.Modifiers & ModifierKeys.Shift) != ModifierKeys.Shift); }

            var Items = dataGrid.Items;
            if (true) // Items.NeedsRefresh)
            {
                try
                {
                    Items.Refresh();
                }
                catch (InvalidOperationException invalidOperationException)
                {
                    Items.SortDescriptions.Clear();
                    throw new InvalidOperationException("SR_DataGrid_ProbableInvalidSortDescription", invalidOperationException);
                }
            }
        }

    }
}

// Compares two objects. An implementation of this method must return a
// value less than zero if x is less than y, zero if x is equal to y, or a
// value greater than zero if x is greater than y.

// SortedList<string, string> openWith = 
//                     new SortedList<string, string>( 
//                         StringComparer.CurrentCultureIgnoreCase);
//(al = list as ArrayList) != null)  {
//                return al.BinarySearch(index, count, value, comparer);
/*

    // List<Customer> customers = GetCustomers();
    // customers.Sort(delegate(Customer x, Customer y)
    //{
    //    if (x.Name != y.Name)
    //    {
    //        return x.Name.CompareTo(y.Name);
    //    }

    //    return x.Location.CompareTo(y.Location);
 
 * http://referencesource.microsoft.com/#PresentationFramework/src/Framework/System/Windows/Data/ListCollectionView.cs,1269
 * if (localIndex > 0 && ActiveComparer.Compare(list[localIndex-1], editItem) > 0)
    {
        // the item has moved toward the front of the list
        toIndex = list.Search(0, localIndex, editItem, ActiveComparer);
        if (toIndex < 0)
            toIndex = ~toIndex;
    }
    else if (localIndex < list.Count - 1 && ActiveComparer.Compare(editItem, list[localIndex+1]) > 0)
    {
        // the item has moved toward the back of the list
        toIndex = list.Search(localIndex+1, list.Count-localIndex-1, editItem, ActiveComparer);
        if (toIndex < 0)
            toIndex = ~toIndex;
        --toIndex;      // because the item is leaving its old position
    }

    internal static class DataExtensionMethods
    {
        // Search for value in the slice of the list starting at index with length count,
        // using the given comparer.  The list is assumed to be sorted w.r.t. the
        // comparer.  Return the index if found, or the bit-complement
        // of the index where it would belong.
        internal static int Search(this IList list, int index, int count, object value, IComparer comparer)
        {
            ArrayList al;
            LiveShapingList lsList;
 
            if ((al = list as ArrayList) != null)
            {
                return al.BinarySearch(index, count, value, comparer);
            }
            else if ((lsList = list as LiveShapingList) != null)
            {
                return lsList.Search(index, count, value);
            }
 
            // we should never get here, but the compiler doesn't know that
            Debug.Assert(false, "Unsupported list passed to Search");
            return 0;
        }
*/
