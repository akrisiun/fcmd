
using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Markup;
using fcmd.Controller;
using fcmd.Model;
using fcmd.View.ctrl;

namespace fcmd.View.Xaml
{
    /// <summary>
    /// Interaction logic for PanelWpf.xaml
    /// </summary>
    public partial class PanelWpf : UserControl, IPanel, IComponentConnector
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

            App.ConsoleWriteLine("PanelWpf ctor");

            InitializeComponent();
            this.path.Text = "";    // Loading...

            PanelDataWpf = new FileListPanelWpf(this);
            DataContext = PanelDataWpf;
        }

#if !VS 

        internal System.Windows.Controls.DockPanel Panel;
        internal fcmd.View.ctrl.ComboWidget combo;
        internal fcmd.View.ctrl.TextEntry path;
        internal fcmd.View.ctrl.ButtonWidget cdRoot;
        internal fcmd.View.ctrl.ButtonWidget cdUp;
        internal fcmd.View.ctrl.ButtonWidget cdFavorites;
        internal fcmd.View.ctrl.ListView2Widget data;

        private bool _contentLoaded;
        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/fcmdp;component/view.xaml/panelwpf.xaml", System.UriKind.Relative);
            System.Windows.Application.LoadComponent(this, resourceLocater);
        }

        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler)
        {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }

        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.Panel = ((System.Windows.Controls.DockPanel)(target));
                    return;
                case 2:
                    this.combo = ((fcmd.View.ctrl.ComboWidget)(target));
                    return;
                case 3:
                    this.path = ((fcmd.View.ctrl.TextEntry)(target));
                    return;
                case 4:
                    this.cdRoot = ((fcmd.View.ctrl.ButtonWidget)(target));
                    return;
                case 5:
                    this.cdUp = ((fcmd.View.ctrl.ButtonWidget)(target));
                    return;
                case 6:
                    this.cdFavorites = ((fcmd.View.ctrl.ButtonWidget)(target));
                    return;
                case 7:
                    this.data = ((fcmd.View.ctrl.ListView2Widget)(target));
                    return;
            }
            this._contentLoaded = true;
        }
#endif

        public void Shown()
        {
            ListView2DataGrid.DataGridColumnWidths(this.data);

            Bind.PanelDirCombo(this.combo, this, this.Side);

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

        public void Update()
        {
            PanelDataWpf.UrlBox.Text = PanelDataWpf.FS.CurrentDirectory;

            Bind.PanelDirUpdate(this.combo, this, this.Side);
        }
    }

}