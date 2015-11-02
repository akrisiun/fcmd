using pluginner.Widgets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
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

            grid.AutoGenerateColumns = false;

            var rightAlign = new Style(typeof(DataGridCell), grid.CellStyle);
            rightAlign.Setters.Add(new Setter() { Property = FrameworkElement.HorizontalAlignmentProperty, Value = HorizontalAlignment.Right });
            // rightAlign.Setters.Add(new Setter() { Property = FrameworkElement.OverridesDefaultStyleProperty, Value = true });

            foreach (var c in columnInfo)
            {
                var item = new DataGridTextColumn()
                {
                    Header = c.Title,
                    Binding = new Binding(c.Tag.ToString()),
                    MinWidth = c.Width
                };
                if (c.VerticalAlign == 1)
                    item.CellStyle = rightAlign;

                columns.Add(item);
            }

            grid.IsReadOnly = grid.IsReadOnly;

            return list as IEnumerable;
        }

        public static DataGrid SetItemsSource(this DataGrid dataGrid, IEnumerable list)
        {
            // ItemsControl
            IEnumerable result = list;
            if (result == null)
                return dataGrid;

            try
            {
                dataGrid.SetValue(ItemsControl.ItemsSourceProperty, result);
            }
            catch (Exception) { ; } // Invalid Operation in PresentationFramework.dll

            return dataGrid;
        }

    }
}
