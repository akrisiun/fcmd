﻿/* The File Commander - InputBox
 * The dialog box for asking the user (like VBA InputBox)
 * (C) The File Commander Team - https://github.com/atauenis/fcmd
 * (C) 2013-14, Alexander Tauenis (atauenis@yandex.ru)
 * Contributors should place own signs here.
 */
namespace fcmd{
    /// <summary>The dialog box for asking the user</summary>
    public class InputBox : Xwt.Dialog{
        Xwt.Label lblQuestion = new Xwt.Label();
        Xwt.TextEntry txtAnwser = new Xwt.TextEntry();
        Xwt.VBox box = new Xwt.VBox();


        public InputBox(string AskText)
        {
            lblQuestion.Text = AskText;

            this.Buttons.Add(Xwt.Command.Ok);
            this.Buttons.Add(Xwt.Command.Cancel);
        }

        public InputBox(string AskText, string DefaultValue)
        {
            lblQuestion.Text = AskText;
            txtAnwser.Text = DefaultValue;

            this.Buttons.Add(Xwt.Command.Ok);
            this.Buttons.Add(Xwt.Command.Cancel);
        }

        public InputBox(string AskText, string DefaultValue, Xwt.Command[] Buttons)
        {
            lblQuestion.Text = AskText;
            txtAnwser.Text = DefaultValue;
            
            this.Buttons.Add(Buttons); 
        }

        /// <summary>Shows dialog</summary>
        /// <returns><value>True</value> if user want to proceed current operation, and <value>False</value> if user don't.</returns>
        public bool ShowDialog()
        {
            Build();
            Xwt.Command DialogResult = this.Run();//4beginners: xwtdialog.Run() = winform.ShowDialog() = winform.Show(vbModal);

            switch (DialogResult.Id)
            {
                case "Add":
                case "Apply":
                case "Clear":
                case "Copy":
                case "Cut":
                case "Delete":
                case "Ok":
                case "Paste":
                case "Remove":
                case "Save":
                case "SaveAs":
                case "Yes":
                    return true;
            }
            return false;
        }

        /// <summary>Builds the InputBox dialog</summary>
        private void Build()
        {
            box.PackStart(lblQuestion);
            box.PackStart(txtAnwser);
            this.Content = box;
            this.ShowInTaskbar = false;
            this.Resizable = false;
            this.Title = System.Windows.Forms.Application.ProductName;
            this.CloseRequested += (o, ea) => { this.Hide(); };
            foreach (Xwt.DialogButton dbtn in this.Buttons)
            {
                dbtn.Clicked += (o, ea) => { this.Hide(); };
            }
        }

        /// <summary>Which text user entered</summary>
        public string Result{
            get{return txtAnwser.Text;}
        }

        /// <summary>What button user clicked</summary>
        public Xwt.Command Command{
            get { return this.Command; }
        }
    }
}
