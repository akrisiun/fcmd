using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xwt;

namespace pluginner.Widgets
{
    public interface IItemsControl : IControl
    {
        bool HasItems { get; }
        IEnumerable ItemsSource { get; set; }
        ItemCollection Items { get; }
    }

    public interface IComboWidget : IItemsControl
    {
        object SelectedItem { get; set; }
        int SelectedIndex { get; set; }
    }
}
