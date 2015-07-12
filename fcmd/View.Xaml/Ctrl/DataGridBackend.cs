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
               IEnumerable<T> list, ListView2List.ColumnInfo[] columnInfo)
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

//        DataGrid's auto-generate columns feature isn't quite suitable for displaying jagged array data
//        Either define it from XAML, for example, assuming you always have two "columns" in the array :
//<DataGrid Name = "_dataGrid" Grid.Row="0" AutoGenerateColumns="False">
//    <DataGrid.Columns>
//        <DataGridTextColumn Header = "Column 1" Binding="{Binding [0]}"/>
//        <DataGridTextColumn Header = "Column 2" Binding="{Binding [1]}"/>
//    </DataGrid.Columns>
//</DataGrid>

    }
}
