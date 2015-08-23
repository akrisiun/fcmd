using fcmd.View.Xaml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace fcmd.View.ctrl
{
    public class ListObservable : ObservableCollection<ListItemXaml>, IList<ListItemXaml>, IEnumerable<ListItemXaml>,
        IEnumerable, IEnumerator<ListItemXaml>, IDisposable
    {
        IEnumerator IEnumerable.GetEnumerator()
        {
            numerator = this.GetEnumerator();
            return numerator;
        }

        object IEnumerator.Current { get { return Current; } }

        protected IEnumerator<ListItemXaml> numerator;

        public void Dispose() { numerator.Dispose(); }
        public void Reset() { GetEnumerator(); }

        public ListItemXaml Current { get { return numerator == null ? null : numerator.Current; } }
        public bool MoveNext() { return numerator.MoveNext(); }
    }
}