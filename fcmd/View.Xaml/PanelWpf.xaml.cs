
using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using fcmd.Controller;
using fcmd.Model;

namespace fcmd.View.Xaml
{
    /// <summary>
    /// Interaction logic for PanelWpf.xaml
    /// </summary>
    public partial class PanelWpf : UserControl, IPanel
    {
        public FileListPanel PanelData {[DebuggerStepThrough] get { return PanelDataWpf; } }
        public PanelSide Side { get; set; }

        public FileListPanelWpf PanelDataWpf { get; private set; }

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

            PanelDataWpf = new FileListPanelWpf(this);
            DataContext = PanelDataWpf;

            GotFocus += (s, e) =>
            {
                IsActive = true;
                PanelDataWpf.OnFocus();
            };

        }

        public void Shown() { }

        protected void SetStyle()
        {
            var act = this.active;
            if (act.HasValue)
            {
                var style = this.Resources[act.Value ? "ActiveStyle" : "PasiveStyle"] as Style;
                var textStyle = this.Resources[act.Value ? "TextActive" : "TextPasive"] as Style;
                if (style != null) // && Panel.Style != style)
                {
                    path.Style = textStyle;
                    Panel.Style = style;
                }
            }
        }
    }

}