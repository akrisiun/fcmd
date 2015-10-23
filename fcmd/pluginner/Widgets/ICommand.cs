using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pluginner.Widgets
{
    public interface ICommand
    {
        bool CanExecute(object parameter);
        void Execute(object parameter);
    }
}
