using System;

namespace fcmd.Platform
{
#if GTK
    public interface IRelayCommand
    {

    }
#else

    public interface IRelayCommand : System.Windows.Input.ICommand
    {

    }
#endif
}
