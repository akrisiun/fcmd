using fcmd.Controller;
using pluginner.Widgets;
using System;
using System.Collections.Generic;

namespace fcmd.Model
{
    public interface ICommanderWindow
    {
        IFileListPanel p1 { get; set; }
        IFileListPanel p2 { get; set; }

#if WPF
        IList<IColumnInfo> LVCols { get; set; } // = new List<ListView2.ColumnInfo>();

#endif

        // Visual
        string Title { get; set; }
        int Height { get; set; }
        int Width { get; set; }

        CommanderData WindowData { get; }
    }

    public interface ICommanderWindow<T> : ICommanderWindow where T : class, IListView2Visual
    {

#if WPF
        IFileListPanel<T> ActivePanel { get; set; }
        IFileListPanel<T> PassivePanel { get; set; }
#endif
    }

}


//        // public MainWindow Window { get; set; }

//        public MenuPanelWpf MainMenu { get { return Window.Menu; } }

//public FileListPanelWpf ActivePanel { get { return Window.ActivePanelWpf as FileListPanelWpf; } }
//public FileListPanelWpf PassivePanel { get { return Window.PassivePanelWpf as FileListPanelWpf; } }
