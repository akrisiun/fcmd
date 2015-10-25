using mshtml;
using System;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace fcmd.http
{
    // C:\Program Files (x86)\Microsoft.NET\Primary Interop Assemblies\Microsoft.mshtml.dll
    public static class MsHtmlHelper
    {
        public static WebBrowserEvents Prepare(this WebBrowser browser)
        {
            var helper = new WebBrowserEvents(browser);
            return helper;
        }
        public static void Prepare(this WebBrowser browser, Action<WebBrowser, mshtml.IHTMLElement> onReady)
        {
            var helper = new WebBrowserEvents(browser);
            if (onReady != null)
                browser.Navigated += new NavigatedEventHandler((s, e) =>
                {
                    if (browser.Document != null)
                    {
                        var doc = browser.Document as mshtml.IHTMLDocument3;
                        if (doc != null)
                            onReady(browser, doc.documentElement);
                    }
                });
        }

        // OnDocumentComplete method (also in same namespace as above [extra info for novices])

        public static DHTMLEventHandler Bind(this WebBrowser browser, Action<IHTMLEventObj> call, string method = "onreadystatechange")
        {
            var doc = browser.Document as HTMLDocument;
            DHTMLEventHandler handler = new DHTMLEventHandler(doc);
            handler.browser = browser;
            handler.Handler += (e) => call(e);

            string @event = method;
            doc.attachEvent(@event, handler);
            return handler;
        }

        public static string outerHTML(this WebBrowser browser)
        {
            var doc = browser.Document as mshtml.IHTMLDocument3;
            string outerHTML = doc == null ? null : doc.documentElement.outerHTML;
            return outerHTML;
        }

        public static string GetHtml(this WebBrowser browser)
        {
            var data = browser.DataContext;
            var html = browser.Document as mshtml.HTMLDocument;

            var ready = html.readyState;
            var body = html.body.outerHTML;
            return body;
        }

        public static mshtml.HTMLDocument SetHtml(this WebBrowser browser, string html, string xpath = null)
        {
            var htmlDoc = browser.Document as mshtml.HTMLDocument;
            var state = htmlDoc.readyState;

            htmlDoc.body.innerHTML = html;
            return htmlDoc;
        }

        //getHtml
        //var state = html.readyState;
        //  HRESULT IHTMLDocument2::get_onreadystatechange(VARIANT *p);HRESULT IHTMLDocument2::put_onreadystatechange(VARIANT v);
        //var ready = html.onreadystatechange;
        // [DispId(-2147412087)] dynamic onreadystatechange { get; set; }
        // HTMLDocument readyState
        // System.Windows.Application.Current
        //var body = html.body.innerHTML;
        // htmlDoc.getElementsByTagName () or htmlDoc.getElementByID()

        //  public  HTMLDocumentClass GetHtmlDocument(FileInfo f)
        //  {
        //     HTMLDocumentClass doc = null;
        //  try
        //  {
        //    doc = new HTMLDocumentClass();
        //    UCOMIPersistFile persistFile = (UCOMIPersistFile)doc;
        //    persistFile.Load( f.FullName, 0 );
        //    int start = Environment.TickCount;
        //    while( doc.readyState != "complete" )
        //   { 
        //      System.Windows.Forms.Application.DoEvents();
        //      if ( Environment.TickCount - start > 10000 )
        //      {
        //        throw new Exception( string.Format( "The document {0} timed out while loading", f.Name ) );
        //      }
        //    }
        //  }
    }

    public class WebBrowserEvents
    {
        public WebBrowserEvents(WebBrowser browser)
        {
            browser.Navigated += browser_Navigated;
            browser.Navigate("about:blank");
            // browser.Navigating += browser_Navigating;
            // browser.LoadCompleted += browser_LoadCompleted;
        }

        // void browser_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        // void browser_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)

        void browser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            var browser = sender as WebBrowser;
            var doc = browser.Document as mshtml.IHTMLDocument3;

            //  Microsoft.mshtml.dll property : [DispId(-2147417084)] string outerHTML { get; set; }
            var body = doc.documentElement.outerHTML;
        }

        public void SetReady(WebBrowser browser, mshtml.HTMLDocument doc, Action<mshtml.IHTMLEventObj> onReady)
        {
            var handler = MsHtmlHelper.Bind(browser, onReady, "onreadystatechange");
        }
    }
}
