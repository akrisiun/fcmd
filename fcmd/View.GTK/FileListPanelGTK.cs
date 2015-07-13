using fcmd.Controller;
using pluginner.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xwt;

namespace fcmd.View.GTK
{
    public class FileListPanelGTK : FileListPanel<ListView2Canvas>
    {
        public FileListPanelGTK(string BookmarkXML = null, string CSS = null,
            string InfobarText1 = "{Name}", string InfobarText2 = "F: {FileS}, D: {DirS}")
            : base(BookmarkXML, CSS, InfobarText1, InfobarText2)
        {

        }

        // dublicate backe methods
        //public void OnGotFocus(ButtonEventArgs ea)
        //{
        //    // #if XWT
        //    base.OnGotFocus(ea);
        //}

        public override IListingContainer ListingWidget
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override IListingView<ListView2Canvas> ListingView
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override void Initialize(PanelSide side)
        {
            throw new NotImplementedException();
        }

        //public void LoadDir()
        //{
        //    throw new NotImplementedException();
        //}

        public override void LoadDir(string Url, ShortenPolicies? Shorten)
        {
            throw new NotImplementedException();
        }

        protected override string MakeStatusbarText(string Template)
        {
            throw new NotImplementedException();
        }

        protected override void WriteDefaultStatusLabel()
        {
            throw new NotImplementedException();
        }

    }
}
