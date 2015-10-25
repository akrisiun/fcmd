using mshtml;
using System;
using System.Runtime.InteropServices;
using System.Windows.Controls;

namespace fcmd.http
{
    public delegate void DocHTMLEvent(HTMLDocument doc, IHTMLEventObj e);
    public delegate void DHTMLEvent(IHTMLEventObj e);

    // http://en.efreedom.net/Question/1-6754968/Add-Event-Listener-Button-Created-CSharp-IE-BHO
    // These attributes may be optional, depending on the project configuration.
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class DHTMLEventHandler : IDisposable
    {
        public DHTMLEvent Handler;
        public HTMLDocument Document { get; set; }
        public WebBrowser browser { get; set; }

        public DHTMLEventHandler(HTMLDocument doc)
        {
            this.Document = doc;
        }

        [DispId(0)]
        public void Call()
        {
            IHTMLEventObj evt = Document.parentWindow.@event;
            Handler(evt);
        }

        public void Dispose()
        {
            Handler = null;
        }
    }
}