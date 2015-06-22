using System;
using Xwt;

namespace fcmd.Menu
{
    public class MenuItemWithKey : MenuItem
    {
        public string Key { get; set; }

        protected override void LoadCommandProperties(Command command)
        {
            base.LoadCommandProperties(command);
        }
    }
}
