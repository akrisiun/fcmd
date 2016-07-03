/* The File Commander - окно вывода статуса действия
 * (C) 2013-14, Alexander Tauenis (atauenis@yandex.ru)
 * Копирование кода разрешается только с письменного согласия
 * разработчика (А.Т.).
 */
using System;
using System.ComponentModel;
using System.Windows.Interop;
using Xwt;

namespace fcmd
{
    /// <summary>Operation progress dialog</summary>
    public class FileProcessDialog : Window
    {
        public MainWindow MainWindow { get; set; }

        public Label lblStatus;
        public ProgressBar pbrProgress;

        public Button cmdCancel;
        public VBox Layout;

        public FileProcessDialog(IWin32Window window) : this(null, window) { }
        public FileProcessDialog() : this(null, null) { }

        /// <summary>Initialize FileProcessDialog with four-row label</summary>
        public FileProcessDialog(Component parent, IWin32Window window)
        {
            lblStatus = new Label { TextAlignment = Alignment.Center };
            pbrProgress = new ProgressBar();

            cmdCancel = new Button { Label = "Cancel" };
            Layout = new VBox();

            Title = Localizator.GetString("FileProgressDialogTitle");
            cmdCancel.Label = Localizator.GetString("Cancel");
            //this.Decorated = false;

            Resizable = true;
            InitialLocation = WindowLocation.CenterParent; // .Manual;

            ContentInit();
        }

        public virtual void ContentInit()
        {
            var layout = this.Layout;
            layout.PackStart(lblStatus, true, true);
            layout.PackStart(pbrProgress, true, true);
            layout.PackStart(cmdCancel, false, false);
            Content = layout;
        }

        public bool? RunModal(IWin32Window ownerWindow)
        {
            var window = this;
            return window.ShowModal(ownerWindow);
        }

        /// <summary>Initialize FileProcessDialog with a custom widget inside</summary>
        /// <param name="ProgressBox">Link to the xwt widget, which should be displayed in the FileProcessDialog.</param>
        public FileProcessDialog(ref Widget ProgressBox, IWin32Window parent = null)
        {
            Title = Localizator.GetString("FileProgressDialogTitle");
            cmdCancel.Label = Localizator.GetString("Cancel");
            Decorated = false;

            Layout.PackStart(ProgressBox, true, true);
            Layout.PackStart(cmdCancel, false, false);
            Content = Layout;
        }
    }
}
