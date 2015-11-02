using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pluginner.Widgets
{
    public interface IFcmdCommand
    {
        bool CanExecute(object parameter);
        void Execute(object parameter);
    }
}
