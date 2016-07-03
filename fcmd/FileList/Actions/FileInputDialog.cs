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
    public class FileInputDialog : FileProcessDialog
    {
        public TextEntry text;
        public Button cmdOk;

        public FileInputDialog(IWin32Window window) : this(null, window) { }

        /// <summary>Initialize FileProcessDialog with four-row label</summary>
        public FileInputDialog(Component parent, IWin32Window window) : base(parent, window)
        {
            pbrProgress.Visible = false;
        }

        public override void ContentInit()
        {
            if (cmdOk == null)
            {
                cmdOk = new Button { Label = "OK" };
                text = new TextEntry { Text = "" };
            }

            //Title = Localizator.GetString("FileProgressDialogTitle");
            cmdCancel.Label = Localizator.GetString("Cancel");

            Layout.PackStart(cmdOk, false, false);

            base.ContentInit();

            //this.Decorated = false;
            Resizable = true;
            InitialLocation = WindowLocation.CenterParent; // .Manual;
        }

    }
}
