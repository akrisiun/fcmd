using System;

namespace fcmd.View
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
