using System;
using System.Windows;
using System.Windows.Controls;

namespace fcmd.theme
{
    public static class MessageDialog
    {
        public static void ShowWarning(string message, string caption = null)
        {
            // MessageBox.Show(message);
            Xwt.MessageDialog.ShowWarning(message);
        }

        public static void ShowError(string message, string caption = null)
        {
            // MessageBox.Show(message);
            Xwt.MessageDialog.ShowWarning(message);
        }

    }
}
