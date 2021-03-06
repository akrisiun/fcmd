﻿using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace pluginner.Widgets
{
    public interface IListingView<T> : IListView2
    {
        ObservableCollection<T> DataItems { get; }
        IEnumerable<T> ChoosedRows { get; }

        void Select(object items);
        void Unselect();
        void InvertSelection();
        int SelectedRow { get; set; }

        void AddItem(IEnumerable<object> Data, IEnumerable<bool> EditableFields, string ItemTag = null);

        // List<Object> Data, List<Boolean> EditableFields, string ItemTag = null)
        // IEnumerable<object> Data, IEnumerable<bool> EditableFields, string ItemTag = null)
        // void LoadDir();

        IPointedItem<T> PointedItem { get; set; }
    }

}
