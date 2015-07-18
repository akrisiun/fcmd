using System;
using System.Collections.ObjectModel;
using Xwt;
using Xwt.Backends;

#if !WPF
using Xwt.GtkBackend;
#else
// using Xwt.WPFBackend;
#endif

namespace fcmd.Menu
{
    [BackendType(typeof(IMenuBackend))]
    public class FcmdMenu : Xwt.Menu
    {
        public FcmdMenu() : base()
        {
            // TODO paint Menu with Keyboard shortcuts
        }

        #region Properties

        public Collection<MenuItemWithKey> ItemsKeys
        {
            get { return null; } // TODO: base.Items as Collection<MenuItemWithKey>; }
        }

#if WPF
        public IMenuBackend Backend
        {
            get { return (IMenuBackend)BackendHost.Backend; }
        }

        protected void InsertItem(int n, MenuItem item)
        {
#if XWT
            (Backend as Xwt.WPFBackend.MenuBackend).InsertItem(n, (IMenuItemBackend)BackendHost.ToolkitEngine.GetSafeBackend(item));
#endif
        }

        protected void RemoveItem(MenuItem item)
        {
#if XWT
            (Backend as Xwt.WPFBackend.MenuBackend).RemoveItem((IMenuItemBackend)BackendHost.ToolkitEngine.GetSafeBackend(item));
#endif
        }
#endif

#endregion

            /// <summary>
            /// Shows the menu at the current position of the cursor
            /// </summary>
            //public new void Popup()
            //{
            //    Backend.Popup();
            //}

            /// <summary>
            /// Shows the menu at the specified location
            /// </summary>
            /// <param name="parentWidget">Widget upon which to show the menu</param>
            /// <param name="x">The x coordinate, relative to the widget origin</param>
            /// <param name="y">The y coordinate, relative to the widget origin</param>
        public new void Popup(Widget parentWidget, double x, double y)
        {
            //Backend.Popup((IWidgetBackend)BackendHost.ToolkitEngine.GetSafeBackend(parentWidget), x, y);
        }

        /// <summary>
        /// Removes all separators of the menu which follow another separator
        /// </summary>
        public new void CollapseSeparators()
        {
            bool wasSeparator = true;
            for (int n = 0; n < Items.Count; n++)
            {
                if (Items[n] is SeparatorMenuItem)
                {
                    if (wasSeparator)
                        Items.RemoveAt(n--);
                    else
                        wasSeparator = true;
                }
                else
                    wasSeparator = false;
            }
            if (Items.Count > 0 && Items[Items.Count - 1] is SeparatorMenuItem)
                Items.RemoveAt(Items.Count - 1);
        }

    }
}
