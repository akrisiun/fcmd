using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fcmd.FileList
{
    public class InputBox
    {
        //Xwt.Label lblQuestion = new Xwt.Label();
        //Xwt.TextEntry txtAnwser = new Xwt.TextEntry();
        //Xwt.VBox box = new Xwt.VBox();
        //public Xwt.Table OtherWidgets = new Xwt.Table();

        public static object Backend { get { return null; }}

        public string Answer { get; set; }
        public string Question { get; set; }


        public InputBox(string AskText)
        {
            //lblQuestion.Text
            Question = AskText;

            //this.Buttons.Add(Xwt.Command.Ok);
            //this.Buttons.Add(Xwt.Command.Cancel);
        }

        public InputBox(string AskText, string DefaultValue)
        {
            //lblQuestion.Text = AskText;
            //txtAnwser.Text = 
            Answer = DefaultValue;

            //this.Buttons.Add(Xwt.Command.Ok);
            //this.Buttons.Add(Xwt.Command.Cancel);
        }

        public InputBox(string AskText, string DefaultValue, Xwt.Command[] Buttons)
        {

        }

        public bool ShowDialog(object Parent = null) // Xwt.WindowFrame parent = null)
        {

            return false;
        }

        public string Result
        {
            get { return Answer; } //  txtAnwser.Text; }
        }

        /// <summary>What button user clicked</summary>
        public Xwt.Command Command
        {
            get { return this.Command; }
        }

    }
}