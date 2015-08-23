using fcmd.Model;
using System;
using Xwt;

namespace fcmd
{
    public interface IBackend
    {
        void Init(ICommanderWindow window);

        MainWindow Window { get; }

#if WPF
        void KeyEvent(object sender, System.Windows.Input.KeyEventArgs key);
#else
        void KeyEvent(MainWindow window, Xwt.KeyEventArgs key);
#endif

        void Localize();
        void Shown();

        void ShowMessage(string message, params object[] args);
        void ShowError(Exception error, string message, params object[] args);
        bool? ShowConfirm(string message, Xwt.ConfirmationMessage details);
    }
}