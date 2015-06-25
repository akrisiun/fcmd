using pluginner.Widgets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace fcmd.theme.ctrl
{
    // FilePanel non Visual

    public class ListView2List : ListView2<ListView2ItemWpf>
    {
        public ListView2List(IUIListingView<ListView2ItemWpf> parent) : base(parent)
        {
            _Items = new List<ListView2ItemWpf>();
        }

        #region Items array

        public override object Content { get { return null; } set {; } }
        public override Font FontForFileNames { get; set; }

        protected List<ListView2ItemWpf> _Items;
        public override IList<ListView2ItemWpf> DataItems { get { return _Items; } }

        public IEnumerable<object> ItemsForGrid()
        {
            var numerator = _Items.GetEnumerator();
            while (numerator.MoveNext())
            {
                ListView2ItemWpf item = numerator.Current;
                yield return new { fldFile = item.fldFile, fldSize = item.fldSize, fldModified = item.fldModified };
            }
        }

        public override void Add(ListView2ItemWpf item)
        {
            item.RowIndex = _Items.Count;
            _Items.Add(item);
        }

        public override bool Contains(ListView2ItemWpf item)
        {
            return _Items.Contains(item);
        }

        public override void CopyTo(ListView2ItemWpf[] item, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public override bool Remove(ListView2ItemWpf item)
        {
            return _Items.Remove(item);
        }

        #endregion

        // Route Events to Parent

        public override void SetFocus() { Parent.SetFocus(); }
        public override void SetupColumns() { (Parent as ListView2Widget).SetupColumns(); }

        public override void Dispose()
        {
            _Items = null;
        }
    }


    public class ListView2Data : ListView2List
    {

        public ListView2Data(ListView2Widget parent) : base(parent)
        {
            if (Parent == null)
                throw new NullReferenceException("check parent");
        }

    }

}
