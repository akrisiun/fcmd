using pluginner.Widgets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

#if WPF
using ListView2Canvas = pluginner.Widgets.ListView2ItemWpf;
#else
using fcmd.View.GTK;
#endif

namespace fcmd.View.ctrl
{
    // FilePanel non Visual

    public class ListView2List : ListView2<ListView2Canvas>, IListingView<ListView2Canvas>, IListingView 
    {
        public ListView2List(IUIListingView<ListView2Canvas> parent) : base(parent)
        {
            _Items = new List<ListView2Canvas>();
        }

        #region Items array

        public override object Content { get { return null; } set {; } }
        public override Font FontForFileNames { get; set; }

        protected List<ListView2Canvas> _Items;
        public override IList<ListView2Canvas> DataItems { get { return _Items; } }

        public override IEnumerable ChoosedRows
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<object> ItemsForGrid()
        {
            var numerator = _Items.GetEnumerator();
            while (numerator.MoveNext())
            {
                ListView2Canvas item = numerator.Current;
                yield return new { fldFile = item.fldFile, fldSize = item.fldSize, fldModified = item.fldModified };
            }
        }

        public override void Add(ListView2Canvas item)
        {
            item.RowIndex = _Items.Count;
            _Items.Add(item);
        }

        public override bool Contains(ListView2Canvas item)
        {
            return _Items.Contains(item);
        }

        public override void CopyTo(ListView2Canvas[] item, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public override bool Remove(ListView2Canvas item)
        {
            return _Items.Remove(item);
        }

        #endregion

        // Route Events to Parent

        public override void SetFocus() { Parent.SetFocus(); }
        public override void SetupColumns() {
#if WPF
            (Parent as ListView2Widget).SetupColumns(); 
#endif
        }

        public override void Dispose()
        {
            _Items = null;
        }
    }

}
