
using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using fcmd.Controller;
using fcmd.Model;
using fcmd.View.ctrl;

namespace fcmd.View.Xaml
{
    /// <summary>
    /// Interaction logic for PanelWpf.xaml
    /// </summary>
    public partial class PanelWpf : UserControl, IPanel
    {
        public FileListPanel PanelData {[DebuggerStepThrough] get { return PanelDataWpf; } }
        public PanelSide Side { get; set; }
        public WindowDataWpf WindowData {[DebuggerStepThrough] get { return PanelDataWpf.WindowData; } }

        public FileListPanelWpf PanelDataWpf {[DebuggerStepThrough] get; private set; }

        private bool? active;
        public bool? IsActive
        {
            get { return active; }
            set
            {
                active = value;
                if (value != null)
                    SetStyle();
            }
        }

        public PanelWpf()
        {
            active = null;
            InitializeComponent();
            this.path.Text = "";    // Loading...

            PanelDataWpf = new FileListPanelWpf(this);
            DataContext = PanelDataWpf;
        }

        public void Shown()
        {
            GotFocus += (s, e) =>
            {
                PanelDataWpf.Focused(s,e);
                IsActive = true;
            };
        }

        protected void SetStyle()
        {
            var act = this.active;
            if (act.HasValue)
            {
                var style = this.Resources[act.Value ? "ActiveStyle" : "PassiveStyle"] as Style;
                var textStyle = this.Resources[act.Value ? "TextActive" : "TextPassive"] as Style;
                if (style != null) // && Panel.Style != style)
                {
                    path.Style = textStyle;
                    Panel.Style = style;
                }
            }
        }

        public override string ToString()
        {
            return "Side=" + Side.ToString() + " Url=" + PanelDataWpf.FS.CurrentDirectory;
        }
    }

}