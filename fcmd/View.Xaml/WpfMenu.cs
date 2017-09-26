using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xwt.Backends;

namespace fcmd.View.Xaml
{
    // http://www.dotnetperls.com/menu

    public class WpfMenu : IMenuBackend
    {
        public void InsertItem(int index, IMenuItemBackend menuItem) { }
        public void RemoveItem(IMenuItemBackend menuItem) { }
        public void Popup() { }
        public void Popup(IWidgetBackend widget, double x, double y) { }

        public void InitializeBackend(object frontend, ApplicationContext context)
        {
            throw new NotImplementedException();
        }

        public void EnableEvent(object eventId)
        {
            throw new NotImplementedException();
        }

        public void DisableEvent(object eventId)
        {
        }
    }
}