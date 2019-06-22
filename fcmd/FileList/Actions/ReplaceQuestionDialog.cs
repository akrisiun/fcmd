/* The File Commander
 * The "The file xxx already exists. Replace? Replace all? Skip? Skip all?" dialog window
 * (C) The File Commander Team - https://github.com/atauenis/fcmd
 * (C) 2014, Alexander Tauenis (atauenis@yandex.ru)
 * Contributors should place own signs here.
 */
using System;

namespace fcmd
{
    public class ReplaceQuestionDialog
    {
        // abstact non visual
        public ReplaceVisualXWT Dialog { get; set; }
        public ClickedButton ChoosedButton { get { return Dialog.ChoosedButton; } }

        // WXT visual backend
        public class ReplaceVisualXWT : Xwt.Dialog
        {
            public Xwt.Table Layout = new Xwt.Table();
            public Xwt.Label lblAsk = new Xwt.Label();
            public Xwt.Button cmdReplace = new Xwt.Button();
            public Xwt.Button cmdReplaceAll = new Xwt.Button();
            public Xwt.Button cmdReplaceOld = new Xwt.Button();
            public Xwt.Button cmdSkip = new Xwt.Button();
            public Xwt.Button cmdSkipAll = new Xwt.Button();
            public Xwt.Button cmdCompare = new Xwt.Button { Sensitive = false };
            public ClickedButton ChoosedButton;

            public void Init(string filename)
            {

                this.Content = Layout;
                Layout.Add(cmdReplace, 0, 1);
                Layout.Add(cmdReplaceAll, 0, 2);
                Layout.Add(cmdSkip, 1, 1);
                Layout.Add(cmdSkipAll, 1, 2);
                Layout.Add(cmdReplaceOld, 2, 2);
                Layout.Add(cmdCompare, 2, 1);
                Layout.Add(lblAsk, 0, 0, 1, 3);
                this.Buttons.Add(Xwt.Command.Cancel);

                Title = Localizator.GetString("ReplaceQDTitle");
                lblAsk.Text = String.Format(Localizator.GetString("ReplaceQDText"), filename);
                cmdReplace.Label = Localizator.GetString("ReplaceQDReplace");
                cmdReplaceAll.Label = Localizator.GetString("ReplaceQDReplaceAll");
                cmdReplaceOld.Label = Localizator.GetString("ReplaceQDReplaceOld");
                cmdSkip.Label = Localizator.GetString("ReplaceQDSkip");
                cmdSkipAll.Label = Localizator.GetString("ReplaceQDSkipAll");
                cmdCompare.Label = Localizator.GetString("ReplaceQDCompare");

            }

            public void DoCommandActivated(Xwt.Command cmd = null)
            {
                this.OnCommandActivated(cmd ??  Xwt.Command.Ok);
            }
        }

        /// <summary>Initialize RPD. Please be careful with threads - run only in the UI thread! Otherwise there will be bugs</summary>
        /// <param name="filename">The both files' name</param>
		public ReplaceQuestionDialog(string filename)
        {
            /* Why the warning about threads? When calling the RPD form thread, different than that where it's created,
             * an exception throws (due to illegal cross-thread call). If the RPD is created in a thread,
             * that is not the program's main thread (UI thread), the RPD works, but the window's or widgets'
             * sizes may be (and, at many times, are) invalid. To prevent this, the best practice is
             * to create & use the RPD instances only in the UI thread. A.T. 14 jun 2014. */

            Dialog = new ReplaceVisualXWT { Content = null };

            Dialog.Init(filename);

            Dialog.cmdReplace.Clicked += (o, ea) => Choose(ClickedButton.Replace);
            Dialog.cmdReplaceAll.Clicked += (o, ea) => Choose(ClickedButton.ReplaceAll);

            Dialog.cmdReplaceOld.Clicked += (o, ea) => Choose(ClickedButton.ReplaceOld);

            Dialog.cmdSkip.Clicked += (o, ea) => Choose(ClickedButton.Skip);
            Dialog.cmdSkipAll.Clicked += (o, ea) => Choose(ClickedButton.SkipAll);
            Dialog.Buttons[0].Clicked += (o, ea) => Choose(ClickedButton.Cancel);
        }

        public enum ClickedButton
        {
            Replace, ReplaceAll, ReplaceOld, Skip, SkipAll, Cancel
        }

        public ClickedButton Run()
        {
            Dialog.Run();
            return Dialog.ChoosedButton;
        }

        private void Choose(ClickedButton cb)
        {
            Dialog.ChoosedButton = cb;
            this.Dialog.DoCommandActivated(Xwt.Command.Ok);
            this.Dialog.Hide();
        }
    }
}

