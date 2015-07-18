/* The File Commander VE base plugins
 * Plain text viewer & editor
 * (C) The File Commander Team - https://github.com/atauenis/fcmd
 * (C) 2013-14, Alexander Tauenis (atauenis@yandex.ru)
 * Contributors should place own signs here.
 */
using System;
using System.Diagnostics;
using System.Text;
using pluginner.Toolkit;
using pluginner.Widgets;

namespace fcmd.base_plugins.ve
{
    public class BodyWidget : Xwt.Table, IWidget
    {
    }

    public class FormatMenu : Xwt.Menu, IMenu
    {
    }

    internal class PlainText : pluginner.IVEPlugin
    {
        #region Metadata
        public string Name { get { return Localizator.GetString("VEptxtVer"); } }
        public string Version { get { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(); } }
        public string Author { get { return "Alexander Tauenis"; } }
        public System.Configuration.Configuration FCConfig { set { } } //it can be a placeholder because the PT can use the fcmd.Properties.Settings...

        public event pluginner.TypedEvent<object[]> APICallHost = null;

        #endregion

        #region Visual Backed

        public PlainBackendXWT Backend {[DebuggerStepThrough] get; set; }
        public IWidget Body {[DebuggerStepThrough] get { return Backend.Layout; } }

        /// <summary>The Xwt menu for configuring file decoding mode in FCVE's menu "Format"</summary>
        public IMenu FormatMenu {[DebuggerStepThrough] get { return Backend.mnuFormat; } }

        public class PlainBackendXWT
        {
            public FormatMenu mnuFormat = new FormatMenu();
            public BodyWidget Layout = new BodyWidget { DefaultRowSpacing = 0 };

            public Xwt.RichTextView RTV = new Xwt.RichTextView();
            public Xwt.ScrollView ScrollBox;
            public Xwt.Label lblFileName = new Xwt.Label("file name");
            public Xwt.MenuButton mbMode = new Xwt.MenuButton("Text") { Sensitive = false, Type = Xwt.ButtonType.Normal, Style = Xwt.ButtonStyle.Flat };
            public Xwt.MenuButton mbCodepage = new Xwt.MenuButton("codepage") { Type = Xwt.ButtonType.DropDown, Style = Xwt.ButtonStyle.Flat };

            // Stylist s = new Stylist(fcmd.Properties.Settings.Default.UserTheme);

            public void Init(EventHandler Codepage_Clicked)
            {

                ScrollBox = new Xwt.ScrollView(RTV);
                Layout.Add(ScrollBox, 0, 1, 1, 3, true, true);

                Layout.Add(lblFileName, 0, 0);
                Layout.Add(mbMode, 1, 0);
                Layout.Add(mbCodepage, 2, 0);

                foreach (EncodingInfo cp in Encoding.GetEncodings())
                {
                    Xwt.MenuItem mi = new Xwt.MenuItem();
                    mi.Tag = cp.CodePage;
                    mi.Label = "CP" + cp.CodePage + " - " + cp.DisplayName;
                    mi.Clicked += Codepage_Clicked;
                    mnuFormat.Items.Add(mi);
                }
                mbCodepage.Menu = mnuFormat;
            }
        }

        #endregion

        protected int Codepage = Encoding.Default.CodePage;
        protected byte[] fileContent;
        protected string Txt = "";

        public PlainText() //constructor
        {
            Backend = new PlainBackendXWT();

            Backend.Init(this.Codepage_Clicked);

        }

        void Codepage_Clicked(object sender, EventArgs e)
        {
            Xwt.MenuItem MI = (Xwt.MenuItem)sender;
            ChangeCodepage(Convert.ToInt32(MI.Tag));
        }

        void ChangeCodepage(int CP)
        {
            Codepage = Convert.ToInt32(CP);
            Txt = Encoding.GetEncoding(Codepage).GetString(fileContent ?? new byte[] { 0, 0 });

            Backend.RTV.LoadText(Txt, new Xwt.Formats.PlainTextFormat());
            Backend.mbCodepage.Label = Encoding.GetEncoding(Codepage).EncodingName;
        }

        public int[] APICompatibility
        {
            get
            {
                int[] fapiver = { 0, 1, 0, 0, 1, 0 };
                return fapiver;
            }
        }

        public object APICallPlugin(string Command, params object[] Arguments)
        {
            switch (Command)
            {
                //todo: add vim-style commands together with VBScript like ones
                case "i": //i - insert (edit) mode
                    Backend.RTV.SetFocus();
                    break;
                case "codepage":
                    ChangeCodepage(Convert.ToInt32(Arguments[1]));
                    break;
                case "findreplace": break;
                case "cut": break;
                case "copy": break;
                case "paste": break;
                case "select": break;
                case "print": break;
                case "pagesetup": break;
                default: Xwt.MessageDialog.ShowWarning("Unknown command:", Command); break;
            }
            return null;
        }

        public void OpenFile(string url, pluginner.IFSPlugin fsplugin)
        {
            Backend.lblFileName.Text = url;
            fileContent = fsplugin.GetFileContent(url);
            ChangeCodepage(Codepage);
        }

        public void SaveFile(bool SaveAs = false) { }


        public bool ReadOnly { get { return false; } set { } } //todo

        public bool CanEdit { get { return false; } }//todo (needs to edit xwt rtv or te)

        public bool ShowToolbar
        {
            set
            {
                Backend.lblFileName.Visible = value;
                Backend.mbMode.Visible = value;
                Backend.mbCodepage.Visible = value;
            }
        }

#if XWT
  //      public Stylist Stylist
		//{
		//	set
		//	{
		//		s = value;

		//		s.Stylize(RTV, "VEWorkingArea");
		//		s.Stylize(Layout, "VEToolbar");
		//		/*foreach (Xwt.Widget w in Layout.Children)
		//		{
		//			s.Stylize(w, "VEToolbar");
		//		}*/
		//		s.Stylize(lblFileName,"VEToolbarLabel");
		//		s.Stylize(mbMode,"VEToolbarButton");
		//		s.Stylize(mbCodepage,"VEToolbarButton");
		//	}
		//}
#endif

    }
}
