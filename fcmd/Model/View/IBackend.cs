using fcmd.Model;

namespace fcmd.View
{
    public interface IBackend
    {
        void Init(ICommanderWindow window);

        MainWindow Window { get; }

#if WPF
        void KeyEvent(MainWindow window, System.Windows.Input.KeyEventArgs key);
#else
        void KeyEvent(MainWindow window, Xwt.KeyEventArgs key);
#endif

        void Localize();
        void Shown();
    }
}