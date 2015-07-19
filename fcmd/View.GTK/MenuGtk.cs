using pluginner.Widgets;
using Xwt;
using fcmd.View;

namespace fcmd.Menu
{
    public class MenuGtk : Xwt.Menu, IMenu
    {
        #region Items

        public MenuItemWithKey mnuFile = new MenuItemWithKey { Tag = "mnuFile", Label = "File" };
        public MenuItemWithKey mnuFileUserMenu = new MenuItemWithKey { Tag = "mnuFileUserMenu" };
        public MenuItemWithKey mnuFileView = new MenuItemWithKey { Tag = "mnuFileView" };
        public MenuItemWithKey mnuFileEdit = new MenuItemWithKey { Tag = "mnuFileEdit" };
        public MenuItemWithKey mnuFileCompare = new MenuItemWithKey { Tag = "mnuFileCompare" };
        public MenuItemWithKey mnuFileCopy = new MenuItemWithKey { Tag = "mnuFileCopy", Key = "F5", Label = "Copy" };
        public MenuItemWithKey mnuFileMove = new MenuItemWithKey { Tag = "mnuFileMove", Key = "F6" };
        public MenuItemWithKey mnuFileNewDir = new MenuItemWithKey { Tag = "mnuFileNewDir", Key = "F7" };
        public MenuItemWithKey mnuFileRemove = new MenuItemWithKey { Tag = "mnuFileRemove", Key = "F8" };

        public MenuItemWithKey mnuFileAtributes = new MenuItemWithKey { Tag = "mnuFileAtributes" };
        public MenuItemWithKey mnuFileQuickSelect = new MenuItemWithKey { Tag = "mnuFileQuickSelect" };
        public MenuItemWithKey mnuFileQuickUnselect = new MenuItemWithKey { Tag = "mnuFileQuickUnselect" };
        public MenuItemWithKey mnuFileSelectAll = new MenuItemWithKey { Tag = "mnuFileSelectAll" };
        public MenuItemWithKey mnuFileUnselect = new MenuItemWithKey { Tag = "mnuFileUnselect" };
        public MenuItemWithKey mnuFileInvertSelection = new MenuItemWithKey { Tag = "mnuFileInvertSelection" };
        public MenuItemWithKey mnuFileExit = new MenuItemWithKey { Tag = "mnuFileExit", Label = "Exit", Key = "F10" };

        public MenuItemWithKey mnuView = new MenuItemWithKey { Tag = "mnuView", Label = "View" };
        public MenuItemWithKey mnuViewShort = new MenuItemWithKey { Tag = "mnuViewShort" };
        public MenuItemWithKey mnuViewDetails = new MenuItemWithKey { Tag = "mnuViewDetails" };
        public MenuItemWithKey mnuViewIcons = new MenuItemWithKey { Tag = "mnuViewIcons" };
        public MenuItemWithKey mnuViewThumbs = new MenuItemWithKey { Tag = "mnuViewThumbs" };
        public MenuItemWithKey mnuViewQuickView = new MenuItemWithKey { Tag = "mnuViewQuickView" };
        public MenuItemWithKey mnuViewTree = new MenuItemWithKey { Tag = "mnuViewTree" };
        public MenuItemWithKey mnuViewPCPCconnect = new MenuItemWithKey { Tag = "mnuViewPCPCconnect" };
        public MenuItemWithKey mnuViewPCNETPCconnect = new MenuItemWithKey { Tag = "mnuViewPCNETPCconnect" };
        public MenuItemWithKey mnuViewByName = new MenuItemWithKey { Tag = "mnuViewByName" };
        public MenuItemWithKey mnuViewByType = new MenuItemWithKey { Tag = "mnuViewByType" };
        public MenuItemWithKey mnuViewByDate = new MenuItemWithKey { Tag = "mnuViewByDate" };
        public MenuItemWithKey mnuViewBySize = new MenuItemWithKey { Tag = "mnuViewBySize" };
        public MenuItemWithKey mnuViewNoFilter = new MenuItemWithKey { Tag = "mnuViewNoFilter" };
        public MenuItemWithKey mnuViewWithFilter = new MenuItemWithKey { Tag = "mnuViewWithFilter" };

        public MenuItemWithKey mnuNavigate = new MenuItemWithKey { Tag = "mnuNav" };
        public MenuItemWithKey mnuNavigateTree = new MenuItemWithKey { Tag = "mnuNavigateTree" };
        public MenuItemWithKey mnuNavigateFind = new MenuItemWithKey { Tag = "mnuNavigateFind" };
        public MenuItemWithKey mnuNavigateHistory = new MenuItemWithKey { Tag = "mnuNavigateHistory" };
        public MenuItemWithKey mnuNavigateReload = new MenuItemWithKey { Tag = "mnuNavigateReload" };

        public MenuItemWithKey mnuTools = new MenuItemWithKey { Tag = "mnuTools" };
        public MenuItemWithKey mnuToolsOptions = new MenuItemWithKey { Tag = "mnuToolsOptions" };
        public MenuItemWithKey mnuToolsPluginManager = new MenuItemWithKey { Tag = "mnuToolsPluginManager" };
        public MenuItemWithKey mnuToolsEditUserMenu = new MenuItemWithKey { Tag = "mnuToolsEditUserMenu" };
        public MenuItemWithKey mnuToolsKeychains = new MenuItemWithKey { Tag = "mnuToolsKeychains" };
        public MenuItemWithKey mnuToolsConfigEdit = new MenuItemWithKey { Tag = "mnuToolsConfigEdit" };
        public Xwt.CheckBoxMenuItem mnuViewKeybrdHelp = new Xwt.CheckBoxMenuItem { Tag = "mnuViewKeybrdHelp" };
        public Xwt.CheckBoxMenuItem mnuViewInfobar = new Xwt.CheckBoxMenuItem { Tag = "mnuViewInfobar" };
        public Xwt.CheckBoxMenuItem mnuViewDiskButtons = new Xwt.CheckBoxMenuItem { Tag = "mnuViewDiskButtons" };
        public MenuItemWithKey mnuToolsDiskLabel = new MenuItemWithKey { Tag = "mnuToolsDiskLabel" };
        public MenuItemWithKey mnuToolsFormat = new MenuItemWithKey { Tag = "mnuToolsFormat" };
        public MenuItemWithKey mnuToolsSysInfo = new MenuItemWithKey { Tag = "mnuToolsSysInfo" };

        public MenuItemWithKey mnuHelp = new MenuItemWithKey { Tag = "mnuHelp", Label = "Help" };
        public MenuItemWithKey mnuHelpHelpMe = new MenuItemWithKey { Tag = "mnuHelpHelpMe" };
        public MenuItemWithKey mnuHelpDebug = new MenuItemWithKey { Tag = "mnuHelpDebug" };
        public MenuItemWithKey mnuHelpAbout = new MenuItemWithKey { Tag = "mnuHelpAbout", Label = "About" };

        #endregion

        public void Build()
        {
            mnuFile.SubMenu = new Xwt.Menu();
            MenuBind.AddRange<MenuItem>(mnuFile.SubMenu.Items,
                new[] {
                mnuFileUserMenu, mnuFileView, mnuFileEdit, mnuFileCompare, mnuFileCopy, mnuFileMove, mnuFileNewDir, mnuFileRemove
                , mnuFileAtributes, mnuFileQuickSelect, mnuFileQuickUnselect , mnuFileSelectAll , mnuFileUnselect , mnuFileInvertSelection
                , mnuFileExit
                });

            this.Items.Add(mnuFile);
            this.Items.Add(mnuView);
            this.Items.Add(mnuNavigate);
            this.Items.Add(mnuTools);
            this.Items.Add(mnuHelp);
        }
    }

    public static class MenuBind
    {

        public static void AddRange<T>(MenuItemCollection collection, T[] items)
        {
            foreach (var item in items)
                collection.Add(item as MenuItem);
        }

        public static void Bind(MainWindow window, MenuGtk menu)
        {
            //MenuPanelGtk menu = window.Menu;

            //menu.itemExit.Command = cmdExit.Command;    // F10
            //menu.itemExit.InputGestureText = "F10";
            //menu.itemExit.Header = "_Exit";
            //// menu.itemExit.Click += (s, e) => cmdExit.Command.Execute(e);

            //var bar = menu.menuBar;
            //var edit = new MenuItem { Header = "_Edit" };
            //bar.Items.Insert(1, edit);

            //foreach (Control item in EditItems())
            //{
            //    item.Width = 200;
            //    edit.Items.Add(item);
            //}
        }

        //public static IEnumerable<Control> EditItems()
        //{
        //    /* TODO: Edit: 
        //        Cut Copy Paste Delete
        //        SelectAll Mark.. UnMark
        //        Properties Find Replace
        //        New Open Edit PrintPreview Save SaveAs
        //    */

        //    yield return new MenuItem { Header = "_Cut", InputGestureText = "Ctrl+X", Command = ApplicationCommands.Cut };
        //    yield return new MenuItem { Header = "_Copy", InputGestureText = "Ctrl+C", Command = ApplicationCommands.Copy };
        //    yield return new MenuItem { Header = "_Paste", InputGestureText = "Ctrl+P", Command = ApplicationCommands.Paste };
        //    yield return new Separator();

        //    yield return new MenuItem { Header = "Select all", Command = ApplicationCommands.SelectAll };
        //    yield return new MenuItem { Header = "Unselect .." };
        //    yield return new Separator();

        //    yield return new MenuItem { Header = "Find", InputGestureText = "Ctrl+F", Command = ApplicationCommands.Find };
        //    yield return new MenuItem { Header = "Replace", InputGestureText = "Ctrl+H", Command = ApplicationCommands.Replace };
        //    yield return new MenuItem { Header = "Properties", InputGestureText = "Shift+F10", Command = ApplicationCommands.Properties };
        //}

        //public static void ExitExecuted(object sender, ExecutedRoutedEventArgs e)
        ////{ string str = e.Parameter as string;
        //    // Mvvm_Variable.Action(Input: str);
        //    e.Handled = true;
    }

}