using fcmd.Model;
using pluginner.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fcmd.FileList
{
    public class DiskCombo : IDriveCombo, IBackend
    {
        public IControl Target { get; set; }

        public void Populate()
        {
            DiskBox.Populate(this);
        }

        public void Init(ICommanderWindow window)
        {

        }

        public MainWindow Window { get { return MainWindow.ActiveWindow; } }

        #region Backed
        public void KeyEvent(object sender, System.Windows.Input.KeyEventArgs key)
        {
        }

        public void Localize() { }
        public void Shown() { }

        public void ShowMessage(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void ShowError(Exception error, string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public bool? ShowConfirm(string message, Xwt.ConfirmationMessage details)
        {
            throw new NotImplementedException();
        }
        #endregion

    }

    // TODO:
    public static class DiskBox
    {
        public static void Populate(this IBackend view)
        {

            //DiskBox.Content = DiskList;
            //DiskBox.CanScrollByY = false;

            //GoRoot.ExpandHorizontal = GoUp.ExpandHorizontal = BookmarksButton.ExpandHorizontal = HistoryButton.ExpandHorizontal = false;
            //GoRoot.Style = GoUp.Style = BookmarksButton.Style = HistoryButton.Style = ButtonStyle.Flat;
            // HistoryButton.Menu = new Xwt.Menu();
            //DefaultColumnSpacing = 0;
            //DefaultRowSpacing = 0;

            //Add(DiskBox, 0, 0, 1, 1, true, false, WidgetPlacement.Fill);
            //Add(GoRoot, 1, 0, 1, 1, false, false, WidgetPlacement.Fill);
            //Add(GoUp, 2, 0, 1, 1, false, false, WidgetPlacement.Fill);
            //Add(UrlBox, 0, 1, 1, 1, true, false, WidgetPlacement.Fill);
            //Add(BookmarksButton, 1, 1, 1, 1, false, false, WidgetPlacement.Start);
            //Add(HistoryButton, 2, 1, 1, 1, false, false, WidgetPlacement.Start);
            //Add(ListingView, 0, 2, 1, 3, false, true); //hexpand will be = 'true' without seeing to this 'false'
            //Add(QuickSearchBox, 0, 3, 1, 3);
            //Add(StatusBar, 0, 4, 1, 3);
            //Add(StatusProgressbar, 0, 5, 1, 3);
            //Add(CLIoutput, 0, 6, 1, 3);
            //Add(CLIprompt, 0, 7, 1, 3);

            //WriteDefaultStatusLabel();
            //CLIprompt.KeyReleased += CLIprompt_KeyReleased;

            //QuickSearchText.GotFocus += (o, ea) => { OnGotFocus(ea); };
            //QuickSearchText.KeyPressed += QuickSearchText_KeyPressed;
            //QuickSearchBox.PackStart(QuickSearchText, true, true);
            //QuickSearchBox.Visible = false;
        }
    }

}