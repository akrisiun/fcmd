using pluginner.Widgets;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;

namespace fcmd.http
{
    /// <summary>
    /// Interaction logic for HttpContent.xaml
    /// </summary>
    public class HttpContent : UserControl, IControl
    {
        static HttpContent() { }

        public static HttpContent Create()
        {
            // Resources issues
            // [assembly: NeutralResourcesLanguageAttribute("en-US", UltimateResourceFallbackLocation.MainAssembly)]

            AppDomain.CurrentDomain.AssemblyResolve += ResolveAssemblies;
            HttpContent content = null;
            try
            {
                content = new HttpContent();
            }
            catch (Exception ex) { LastError = ex; }
            AppDomain.CurrentDomain.AssemblyResolve -= ResolveAssemblies;

            return content;
        }

        static Assembly ResolveAssemblies(object sender, ResolveEventArgs args)
        {
            //if (tempAssembly != null && Thread.CurrentThread.GetHashCode() == threadCode)
            //    return tempAssembly.GetReferencedAssembly(args.Name);

            var name = args.Name;
            if (name.Contains(".http"))
                return typeof(HttpPlugin).Assembly;

            return null;
        }

        public HttpContent()
        {
            // InitializeComponent();
            //System.Uri resourceLocater = new System.Uri("/fcmd.http;component/httpcontent.xaml", System.UriKind.Relative);
            //System.Windows.Application.LoadComponent(this, resourceLocater);
            Browser = new WebBrowser();
            Browser.SetValue(Grid.VerticalAlignmentProperty, VerticalAlignment.Stretch);
            Browser.SetValue(Grid.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);
            this.AddLogicalChild(Browser);
            // MsHtmlHelper.Prepare(Browser);
        }

        public WebBrowser Browser { get; protected set; }

        public void Navigate(string url)
        {
            var browser = Browser;
            browser.Navigate(url);
        }

        bool IUIDispacher.CheckAccess()
        {
            return Browser.CheckAccess();
        }

        public object Dispacher { get { return this.Dispacher; } }
        public bool? Visible
        {
            get { return Visibility == Visibility.Visible; } // == null Hidden, == false Collapsed
            set { this.Visibility = (value.HasValue) ? Visibility.Visible : Visibility.Collapsed; }
        }
    }
}
