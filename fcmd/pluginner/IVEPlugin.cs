/* The File Commander - plugin API
 * Interface for Viewer&Editor plugins
 * (C) The File Commander Team - https://github.com/atauenis/fcmd
 * (C) 2013-14, Alexander Tauenis (atauenis@yandex.ru)
 
 * Contributors should place own signs here.
 * 2015, Andrius Krisiunas (akrisiun@gmail.com)
 */

using System;
using pluginner.Widgets;
// using Xwt;
// using pluginner.Toolkit;

namespace pluginner
{
    /// <summary>Interface for FC Viewer/Editor plugins</summary>
    public interface IVEPlugin : IPlugin
    {
        /// <summary>Open the file</summary>
        /// <param name="url">URL of the file</param>
        /// <param name="fsplugin">The FS plugin, which be used to access the file</param>
        void OpenFile(string url, IFSPlugin fsplugin);

        /// <summary>Save the current file</summary>
        /// <param name="SaveAs">(optional) Save with new URL ("save as").</param>
        void SaveFile(bool SaveAs = false);

        // not XWT abstract

        /// <summary>The body of the displayed part of the plugin</summary>
        IWidget Body { get; }

        /// <summary>The Xwt menu for configuring file decoding mode in FCVE's menu "Format"</summary>
        IMenu FormatMenu { get; }

        /// <summary>Disables content editing in the plugin..</summary>
        bool ReadOnly { get; set; }

        /// <summary>Defines, can the plugin edit files (set to false if the plugin is readonly)</summary>
        bool CanEdit { get; }

        /// <summary>Defines, should this plugin display a toolbar at the top of the box or not</summary>
        bool ShowToolbar { set; }

#if XWT
        /// <summary>UI theme applicator.</summary>
        // Stylist Stylist { set; }
        //note that the plugin color scheme must rely on VEWorkingArea selector
#endif
    }
}
