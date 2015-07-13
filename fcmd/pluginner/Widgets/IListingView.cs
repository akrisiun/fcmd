using System;
using System.Collections;
using System.Collections.Generic;

namespace pluginner.Widgets
{
    public interface IListingView<T> : IListView2 // , IPointedItem<T>
    {
        IList<T> DataItems { get; }
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

    //public interface IListingView2<T> : IListingView<T>, IListView2<T>, IEnumerable
    //    where T : IListView2Visual
    //{
    //    // ListView2ItemGTK
    //}

}
