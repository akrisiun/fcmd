/* The File Commander
 * XML node displayer (with support for editing)
 * (C) The File Commander Team - https://github.com/atauenis/fcmd
 * (C) 2014, Alexander Tauenis (atauenis@yandex.ru)
 * Contributors should place own signs here.
 */

using Xwt;
using System.Xml.Linq;
using pluginner.Widgets;
using System.Collections;
// using Xwt.Drawing;

namespace fcmd
{
    /// <summary>Graphics XML node representer</summary>
    public class XmlDisplay
    {
        public class XmlWidget : Widget, IWidget
        {
            public Xwt.VBox layout = new VBox();

            public void Init(XElement node)
            {
                this.CanGetFocus = true;
                //if (ht != null)
                //    ht.Add(node, this); //register self in the common registry of conformity between XmlNode <--> XmlDisplay

                Xwt.Expander exp = new Expander
                {
                    Expanded = true,
                    Label = node.Value,
                    Font = Font.WithWeight(Xwt.Drawing.FontWeight.Semibold)
                };

                exp.Content = layout;
                layout.Font = Font.WithWeight(Xwt.Drawing.FontWeight.Normal); //для уверенности
                this.Content = exp;
            }

            Widget IWidget.Content { get { return layout; } set { } }
        }

        IWidget Visual { get; set; }
        XElement node;

        // XmlNode node
        public XmlDisplay(XElement node, Hashtable ht = null)
        {
            this.node = node;
#if XWT
            var @this = new XmlWidget();
            Visual = @this;

            // временный код! переписать с поддержкой редактирования и сворачивания!

            @this.Init(node);

            if (node.HasAttributes) //  .Attributes != null)
                foreach (XAttribute a in node.Attributes())
                {
                    @this.layout.PackStart(new Label(a.Name + " = " + a.Value)
                    {
                        Tag = a,
                        Font = @this.Font.WithWeight(Xwt.Drawing.FontWeight.Normal)
                    });
                }

            if (node.HasElements) // . ChildNodes != null)
            {
                if (node.FirstNode != node.LastNode) //  .ChildNodes.Count > 0)
                    foreach (XElement n in node.Elements()) // .ChildNodes)
                    {
                        XmlDisplay child_xd = new XmlDisplay(n, ht);

                        var visualChild = child_xd.Visual as XmlWidget;
                        visualChild.Tag = n;
                        visualChild.Margin = 24;

                        @this.layout.PackStart(visualChild); //обеспечивается рекурсивность
                    }
                else
                    @this.layout.PackStart(
                        new Label(node.ToString()) // .InnerText)
                        {
                            Tag = node,
                            Font = @this.Font.WithWeight(Xwt.Drawing.FontWeight.Normal)
                        });
            }
#endif
        }

        public XElement Node
        {
            get { return node; }
        }
    }
}
