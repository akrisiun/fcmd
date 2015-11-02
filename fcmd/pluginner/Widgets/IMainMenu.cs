using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pluginner.Widgets
{
    public interface IMainMenu : IMenu
    {

    }

    public interface IMenuItem
    {
        string Key { get; set; }
        IFcmdCommand Command { get; }
    }

}
