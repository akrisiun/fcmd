using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pluginner.Widgets
{
    public interface IContent
    {
        object Content { get; set; }

        bool Visible { get; set; }
        bool Condensed { get; set; }    // when not Expanded (like XAML control)
    }
}