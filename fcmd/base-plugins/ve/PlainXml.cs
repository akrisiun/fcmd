/* The File Commander VE base plugins
 * XML viewer & editor
 * (C) The File Commander Team - https://github.com/atauenis/fcmd
 * (C) 2014, Alexander Tauenis (atauenis@yandex.ru)
 * Contributors should place own signs here.
 */
using System;
using System.Text;

using pluginner;
using Xwt;
using System.Xml.Linq;
using pluginner.Widgets;
using fcmd.Menu;
#if !WPF
    using fcmd.View.GTK;
#endif

namespace fcmd.base_plugins.ve
{
    class PlainXml : IVEPlugin
    {
        #region Name, version & other nonsense

        public string Name
        {
            get { return "eXtendable Markup Language"; }
        }

        public string Version
        {
            get { return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        public string Author
        {
            get { return "Alexander Tauenis"; }
        }

        public int[] APICompatibility
        {
            get { int[] v = { 0, 1, 0, 0, 1, 0 }; return v; }
        }

        public System.Configuration.Configuration FCConfig { set { } } //it can be a placeholder because this 'plugin' can use the fcmd.Properties.Settings...
        #endregion

        // XmlDocument doc = new XmlDocument();
        XDocument doc = null;

        public PlainXml()
        {
        }

        public void OpenFile(string url, IFSPlugin fsplugin)
        {
            doc = XDocument.Load(
                Encoding.Default.GetString(fsplugin.GetFileContent(url))
                );

#if XWT
   //         if (doc.ChildNodes.Count > 0)
			//{
			//	int deep = 0;
			//	foreach (XmlNode n in doc.ChildNodes)
			//	{
			//		if (n.NodeType != XmlNodeType.XmlDeclaration) //skip "<?xml version=... codepage=..."
			//		{
			//			XmlDisplay child_xd = new XmlDisplay(n, ht) { Tag = n, MarginLeft = ((deep > 0) ? 12 : 0) };
			//			layout.PackStart(child_xd); //обеспечивается рекурсивность
			//		}
			//	}
			//	sw = new ScrollView(layout);
			//}
			//else
			//{
			//	XmlDisplay xd = new XmlDisplay(doc.ChildNodes[0], ht);
			//	sw = new ScrollView(xd);
			//}
#endif

        }

        public void SaveFile(bool SaveAs = false)
        {
            throw new NotImplementedException();
        }

#if !WPF

        // ScrollView 
        protected BodyGtk sw;
        protected VBox layout = new VBox();
        protected System.Collections.Hashtable ht = new System.Collections.Hashtable();

        public IWidget Body { get { return sw ?? (sw = new BodyGtk()); } } // BodyWidget

        public IMenu FormatMenu {  get { return new MenuGtk(); } }
        // protected MenuGtk MenuGtk
#else
        // WPF version

        public IWidget Body
        {
            get { return null; }
        }

        public IMenu FormatMenu
        {
            get { return null; }
        }

#endif

        public bool ReadOnly
        {
            get
            {
                return true;
            }
            set
            {
                //UNDONE: сделать правку!!!
            }
        }

        public bool CanEdit
        {
            get { return false; }
        }

        public bool ShowToolbar
        {
            set { /*UNDONE!!!*/ }
        }

        //public Stylist Stylist
        //{
        //	set { /*UNDONE*/ }
        //}


        public object APICallPlugin(string call, params object[] arguments)
        {
            throw new NotImplementedException();
        }

        // public event TypedEvent<object[]> APICallHost = null;
    }
}
