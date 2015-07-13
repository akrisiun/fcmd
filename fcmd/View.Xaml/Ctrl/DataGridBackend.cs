using pluginner.Widgets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;

namespace fcmd.View.ctrl
{
    public static class DataGridBackend
    {
        public static IEnumerable ToDataSource<T>(this DataGrid grid,
               IEnumerable<T> list, ListView2.ColumnInfo[] columnInfo)
        {
            ObservableCollection<DataGridColumn> columns = grid.Columns;
            if (columns.Count > 0)
                columns.Clear();
            foreach (var c in columnInfo)
            {
                var item = new DataGridTextColumn()
                {
                    Header = c.Title,
                    Binding = new Binding(c.Tag.ToString())
                };
                columns.Add(item);
            }

            // ItemsControl
            IEnumerable result = list as IEnumerable;

            grid.AutoGenerateColumns = false;
            grid.IsReadOnly = grid.IsReadOnly;
            try
            {
                grid.ItemsSource = result;
            }
            catch (Exception) {; } // Invalid Operation in PresentationFramework.dll

            return result;
        }

    }
}
