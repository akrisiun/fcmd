using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Markup;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using DrawingColor = System.Drawing.Color;
using System.IO;

using pluginner.Widgets;
using fcmd.View.Xaml;
using fcmd.Controller;
using fcmd.Model;
using System.Windows.Threading;

namespace fcmd.View.ctrl
{
    public class PointedItem : IPointedItem<ListItemXaml> // <ListView2Item>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PointedItem() { } // : base(0, 0, "", null, null) // , null)

        public int Index { get; set; }
        public ListItemXaml Item { get; set; }

        /// Pointed items
        public IEnumerable<ListItemXaml> Pointed { get; set; }

        public object[] Data { get { return Item.Data; } set { Item.Data = value; } }

        /// <summary>
        ///   returns Full path 
        /// </summary>
        public override string ToString()
        {
            return Item.FullPath;
        }
    }

    public static class ColorConvert
    {
        public static DrawingColor To(this System.Windows.Media.Color mediaColor)
        {
            return default(DrawingColor);   // TODO
        }

        public static DrawingColor To(this System.Windows.Media.Brush mediaBrush)
        {
            // mediaBrush.
            return default(DrawingColor);   // TODO
        }

        public static System.Windows.Media.Brush From(this DrawingColor drawingColor)
        {
            return default(System.Windows.Media.Brush);
        }
    }
}
