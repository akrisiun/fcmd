using pluginner.Widgets;
using System;
using Xwt;
using Xwt.Backends;

namespace fcmd.Menu
{
    public class MenuItemWithKey : MenuItem, IMenuItem
    {
        public string Key { get; set; }

        protected override BackendHost CreateBackendHost()
        {
            return base.CreateBackendHost();
        }

        // protected void LoadCommandProperties(Command command);
#if WPF
        protected void LoadCommandKey(Command command)
        {
            base.LoadCommandProperties(command);
        }
#endif

        public IFcmdCommand Command { get { return this.Command as IFcmdCommand; } }
    }
}
