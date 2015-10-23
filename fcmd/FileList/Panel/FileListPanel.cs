/* The File Commander - plugin API
 * The file list widget
 * (C) The File Commander Team - https://github.com/atauenis/fcmd
 * (C) 2013-15, Alexander Tauenis (atauenis@yandex.ru)
 * (C) 2014, Zhigunov Andrew (breakneck11@gmail.com)
 * (C) 2014, Evgeny Akhtimirov (wilbit@me.com)
 * (C) 2015, Andrius Krisiunas (akrisiun@gmail.com)
 * Contributors should place own signs here.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Collections.ObjectModel;
using MessageDialog = fcmd.View.MessageDialog;
using Xwt;

using pluginner;
using pluginner.Widgets;
using fcmd.Controller;
using System.Diagnostics;

namespace fcmd
{

#if XWT
    // GTK3 Backend

    using Xwt.Drawing;
    using ColorDrawing = Xwt.Drawing;
    using fcmd.View.GTK.Ctrl;

    public class CommanderStatusBar : Xwt.Label, IContent //  LabelWidget
    {
        object IContent.Content { get { return base.Content; } set { base.Content = value as Xwt.Widget; } }
        // public abstract string Text { get; set; }

        public bool Condensed { get { return false; } set { } } // only for WPF
    }

    public abstract class FileListPanel : Table, IFileListPanel
    {
        //Data Field Numbers
        //they aren't const because they may change when the columns are reordered
        public DataFieldNumbers df { get; set; }
        // public int dfDirItem = 5;

        public IStatusBar StatusBar { get; set; }

        public ShortenPolicies ShortenPolicy { get; set; }

        public IFSPlugin FS { get; set; }

        public abstract IButton GoRoot {[DebuggerStepThrough] get; protected set; }
        public abstract IButton GoUp {[DebuggerStepThrough] get; protected set; }
        public abstract ITextEntryGtk UrlBox {[DebuggerStepThrough] get; protected set; }
        ITextEntry IFileListPanel.UrlBox { get { return UrlBox; } }

        public abstract event TypedEvent<string> Navigate;
        public abstract event TypedEvent<string> OpenFile;

        // T GetValue<T>(int Field)
        public abstract T GetValue<T>(int field);
        public abstract string GetValue(int field);

        public abstract void LoadDir(string Url, ShortenPolicies? shortenPolicy);
        public abstract void LoadDir();
    }
#else

    // no mono Backend with Presentation framework (Xaml)
    using ColorDrawing = System.Drawing;
    // using CursorType = System.Windows.Input.Cursor;
    using System.Windows;
    using fcmd.Model;

    public abstract class CommanderStatusBar : UIElement, IInputElement, IContent, IStatusBar
    {
        public abstract object Content { get; set; }
        public abstract string Text { get; set; }

        public abstract bool Visible { get; set; }
        public abstract bool Condensed { get; set; }    // when not Expanded (like XAML control)
    }

    public abstract class FileListPanel : IFileListPanel
    {
        //Data Field Numbers
        //they aren't const because they may change when the columns are reordered
        public DataFieldNumbers df { get; set; }
        // public int dfDirItem = 5;

        public IStatusBar StatusBar { get; set; }

        public ShortenPolicies ShortenPolicy { get; set; }

        public IFSPlugin FS {[DebuggerStepThrough] get; set; }

        // T GetValue<T>(int Field)
        public abstract T GetValue<T>(int field);
        public abstract string GetValue(int field);

        public abstract void LoadDir(string Url, ShortenPolicies? shortenPolicy);
        public abstract void LoadDir();

        // WPF abstract
        public abstract IButton GoRoot { get; protected set; }
        public abstract IButton GoUp { get; protected set; }
        public abstract ITextEntry UrlBox { get; }

        public abstract event TypedEvent<string> Navigate;
        public abstract event TypedEvent<string> OpenFile;
        public abstract event EventHandler GotFocus;
    }

#endif

    public abstract class FileListPanel<T> : FileListPanel, IFileListPanel<T> where T : class, IListView2Visual
    {
        public abstract IListingView<T> ListingView { get; }

        // public abstract IListingContainer Container { get; }

        public abstract void Initialize(PanelSide side);

        public abstract void LoadFs(string URL, ShortenPolicies Shorten);
    }

    public static class ShortenText
    {
        /// <summary>Converts the file size (in bytes) to human-readable string</summary>
        /// <param name="Input">The input value</param>
        /// <returns>Human-readable string (xxx yB)</returns>
        public static string KiloMegaGigabyteConvert(this long Input, SizeDisplayPolicy ShortenKB, SizeDisplayPolicy ShortenMB, SizeDisplayPolicy ShortenGB)
        {
            double ShortenedSize; //here will be writed the decimal value of the hum. readable size

            //TeraByte (will be shortened everywhen)
            if (Input > 1099511627776) return (Input / 1099511627776) + " TB";

            //GigaByte
            if (Input > 1073741824)
            {
                ShortenedSize = Input / 1073741824;
                switch (ShortenGB)
                {
                    case SizeDisplayPolicy.OneNumeral:
                        return string.Format("{0:0.#} GB", ShortenedSize);
                    case SizeDisplayPolicy.TwoNumeral:
                        return string.Format("{0:0.##} GB", ShortenedSize);
                }
            }

            //MegaByte
            if (Input > 1048576)
            {
                ShortenedSize = Input / 1048576;
                switch (ShortenMB)
                {
                    case SizeDisplayPolicy.OneNumeral:
                        return string.Format("{0:0.#} MB", ShortenedSize);
                    case SizeDisplayPolicy.TwoNumeral:
                        return string.Format("{0:0.##} MB", ShortenedSize);
                }
            }

            //KiloByte
            if (Input > 1024)
            {
                ShortenedSize = Input / 1024;
                switch (ShortenKB)
                {
                    case SizeDisplayPolicy.OneNumeral:
                        return string.Format("{0:0.#} KB", ShortenedSize);
                    case SizeDisplayPolicy.TwoNumeral:
                        return string.Format("{0:0.##} KB", ShortenedSize);
                }
            }

            return Input + " B"; //if Input is less than 1k or shortening is disallowed
        }

    }

}