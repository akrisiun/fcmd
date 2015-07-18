/* The File Commander Settings window
 * FC settings window
 * (C) The File Commander Team - https://github.com/atauenis/fcmd
 * (C) 2013-14, Alexander Tauenis (atauenis@yandex.ru)
 * Contributors should place own signs here.
 */
using System;
using fcmd.SettingsWindowTabs;
using Xwt;

namespace fcmd
{
    /// <summary>The settings window (window, where user switches the program's settings)</summary>
    class SettingsWindow
    {
        public object Dialog { get; set; }

        public SettingsWindow()
        {
            Build();
        }

#if !XWT
        public virtual Command RunCommand()
        {
            // TODO
            return null;
        }

        private void Build()
        {

        }
#else

        protected class SettingsDialog : Dialog
        {
            public HPaned Layout = new HPaned();
            public ListBox TabList = new ListBox();

            public void Init(EventHandler cmdOk_Clicked, EventHandler TabList_SelectionChanged)
            {
                Title = Localizator.GetString("FCS_Title");

                Layout.Panel1.Content = TabList;
                Content = Layout;
                ShowInTaskbar = false;

                Buttons.Clear();
                Buttons.Add(Command.Save, Command.Cancel);
                Buttons[0].Clicked += cmdOk_Clicked;
                Buttons[1].Clicked += (o, e) => Hide();

                TabList.Items.Clear();
                // TabList.SelectionChanged -= TabList_SelectionChanged;
                TabList.MinHeight = 388; TabList.MinWidth = 128;
                TabList.Items.Add(new swtMainWindow(), Localizator.GetString("swtMainWindow"));
                TabList.Items.Add(new swtMainWindowColumns(), Localizator.GetString("swtMainWindowColumns"));
                TabList.Items.Add(new swtMainWindowInfobar(), Localizator.GetString("SWTMWinfobar"));
                TabList.Items.Add(new swtMainWindowThemes(), Localizator.GetString("swtMainWindowThemes"));
                TabList.Items.Add(new swtViewerEditor(), Localizator.GetString("swtViewerEditor"));
                TabList.Items.Add(new swtMainWindowFonts(), Localizator.GetString("swtFonts"));

                TabList.SelectionChanged += TabList_SelectionChanged;
                TabList.SelectRow(0); //wpf hack (row №0 isn't automatical selected)
            }
        }


        // XWT:
        public virtual Command RunCommand()
        {
            var dlg = this.Dialog as SettingsDialog;
            return dlg.Run(null);
        }

        private void Build()
        {
            var dlg = new SettingsDialog();
            dlg.Init(this.cmdOk_Clicked, this.TabList_SelectionChanged);

            this.Dialog = dlg;
        }

        void cmdOk_Clicked(object sender, EventArgs e)
        {
            var dlg = this.Dialog as SettingsDialog;
            foreach (ISettingsWindowTab swt in dlg.TabList.Items)
            {
                if (swt.SaveSettings()) continue;

                //if someone is unable to save settings...
                MessageDialog.ShowError(Localizator.GetString("FCS_CantSaveSettings"));
                return;
            }
            dlg.Hide();
        }

        void TabList_SelectionChanged(object sender, EventArgs e)
        {
            var dlg = this.Dialog as SettingsDialog;
            if (dlg.TabList.SelectedRow > -1)
                dlg.Layout.Panel2.Content =
                    (dlg.TabList.SelectedItem as ISettingsWindowTab).Content;
        }
#endif

    }
}
