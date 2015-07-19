using pluginner.Widgets;
using System;
using Xwt;

namespace fcmd.View.GTK
{
    public class BodyGtk : Xwt.Table, IWidget
    {
        // ScrollView sw;
        VBox layout = new VBox();
        System.Collections.Hashtable ht = new System.Collections.Hashtable();

        public BodyGtk()
        {
            // public Widget Content { get; set; }
        }

        Widget IWidget.Content { get { return this.Content; } set { base.Content = value; } }
    }
}
