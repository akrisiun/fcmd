using fcmd.theme.ctrl;
using pluginner.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
// using Xwt.Drawing;

namespace fcmd.theme
{
    // pluginner.Widgets.ListView2ItemWpf
    //    public ListView2ItemWpf(int rowNumber, int colNumber, string rowTag, ListView2.ColumnInfo[] columns, List<object> data, Font font) 

    public class FileListPanelWpf : FileListPanel<ListView2ItemWpf>
    {
        public PanelWpf Parent { get; set; }

        public FileListPanelWpf(PanelWpf parent) 
        {
            Parent = parent;
        }

        public ListView2Widget ListingViewWpf { get; protected set; }
        public override IListView2<ListView2ItemWpf> ListingView { get { return ListingViewWpf; } }
        public override IUIListingView ListingWidget { get { return ListingViewWpf; } }

        public override void Initialize(PanelSide side)
        {
            onFocus = new EventHandler(OnFocus);
            df = DataFieldNumbers.Default();
            GoRoot = new ButtonWidget { Content = "/" };
            // GoUp = new ButtonWidget { Content = ".." };
            // UrlBox = new TextEntry { Text = "" };
            GoUp = Parent.cdUp;
            UrlBox = Parent.path;

            ListingViewWpf = Parent.data;
            ListingViewWpf.Side = side;
            PostInitialize();
        }

        EventHandler onFocus;
        bool onFocusSet;
        protected void OnFocus(object sender, EventArgs e) {
            if (onFocusSet)
                onFocus(sender, e);

            //  base.GotFocus
        }

        public override event EventHandler GotFocus { add { onFocusSet = true; onFocus += value; } remove { onFocus += value; } }

        protected override void WriteDefaultStatusLabel()
        {
            //StatusProgressbar.Visible = false;
            //if (ListingView.SelectedItems.Count < 1)
            //    StatusBar.Text = MakeStatusbarText(SBtext1);
            //else
            //    StatusBar.Text = MakeStatusbarText(SBtext2);
        }

        protected override string MakeStatusbarText(string Template)
        {
            //string txt = Template;
            //if (ListingView.PointedItem != null)
            //{
            //    DirItem di = (DirItem)ListingView.PointedItem.Data[df.DirItem];
            //    txt = txt.Replace("{FullName}", di.TextToShow);
            //    txt = txt.Replace("{AutoSize}", di.Size.KiloMegaGigabyteConvert(Shorten.KB, Shorten.MB, Shorten.GB)); // CurShortenKB, CurShortenMB, CurShortenMB));
            //    txt = txt.Replace("{Date}", di.Date.ToShortDateString());
            //    txt = txt.Replace("{Time}", di.Date.ToLocalTime().ToShortTimeString());
            //    txt = txt.Replace("{SelectedItems}", ListingView.SelectedItems.Count.ToString());
            //    //todo: add masks SizeB, SizeKB, SizeMB, TimeUTC, Name, Extension
            //}
            //return txt;
            return string.Empty;
        }
    }
}
