using System;

namespace fcmd.Platform
{
#if GTK
    public interface IRelayCommand : System.Windows.Input.ICommand
    {

    }
#else

    public interface IRelayCommand : System.Windows.Input.ICommand
    {

    }
#endif
}
