using pluginner.Widgets;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace fcmd.http
{
    /// <summary>
    /// Interaction logic for HttpContent.xaml
    /// </summary>
    public partial class HttpContent : UserControl, IControl
    {
        static HttpContent() { }
        public HttpContent()
        {
            // InitializeComponent();
            System.Uri resourceLocater = new System.Uri("/fcmd.http;component/httpcontent.xaml", System.UriKind.Relative);
            System.Windows.Application.LoadComponent(this, resourceLocater);
        }

        public void Navigate(string url)
        {
            // this.Nav
        }

        public object Dispacher { get { return this.Dispacher; } }
        public bool? Visible {
            get { return Visibility == Visibility.Visible; } // == null Hidden, == false Collapsed
            set { this.Visibility = (value.HasValue) ? Visibility.Visible : Visibility.Collapsed; }
        }

    }
}
