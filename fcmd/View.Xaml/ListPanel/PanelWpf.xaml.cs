
using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Markup;
using fcmd.Controller;
using fcmd.Model;
using fcmd.View.ctrl;
using pluginner.Widgets;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using fcmd.View.Xaml.cmd;

namespace fcmd.View.Xaml
{
    /// <summary>
    /// Interaction logic for PanelWpf.xaml
    /// </summary>
    public partial class PanelWpf : UserControl, IPanel, IComponentConnector
    {
        #region Properties

        public FileListPanel PanelData {[DebuggerStepThrough] get { return PanelDataWpf; } }
        public PanelSide Side {[DebuggerStepThrough] get; set; }
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
        
        #endregion

        public PanelWpf()
        {
            active = null;

            App.ConsoleWriteLine("PanelWpf ctor");

            InitializeComponent();
            this.path.Text = "";    // Loading...

            PanelDataWpf = new FileListPanelWpf(this);
            DataContext = PanelDataWpf;

            this.Visuals = new Collection<PluginsVisual>();

            data = CreateDataGrid();
        }
     
#if !VS || __MonoCS__

        internal System.Windows.Controls.DockPanel Panel;
        internal fcmd.View.ctrl.ComboWidget combo;
        internal fcmd.View.ctrl.TextEntry path;
        internal fcmd.View.ctrl.ButtonWidget cdRoot;
        internal fcmd.View.ctrl.ButtonWidget cdUp;
        internal fcmd.View.ctrl.ButtonWidget cdFavorites;

		// #line 90 "..\..\..\..\View.Xaml\ListPanel\PanelWpf.xaml"
		internal System.Windows.Controls.ContentControl contentPanel;

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

        void IComponentConnector.Connect(int connectionId, object target)
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
					this.contentPanel = ((System.Windows.Controls.ContentControl)(target));
					return;
            }
            this._contentLoaded = true;
        }
#endif

        public void Shown()
        {
            if (this.data != null)
                ListView2DataGrid.DataGridColumnWidths(this.data);

            DiskBoxCombo.PanelDirCombo(this.combo, this, this.Side);

            GotFocus += (s, e) =>
            {
                PanelDataWpf.Focused(s, e);
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

            BindPanel.PanelDirUpdate(this.combo, this, this.Side);
        }

        public void LoadUrl(string url)
        {
            WpfContent.Load(this, url);
        }

        public ICollection<PluginsVisual> Visuals { get; set; }

        public class PluginsVisual
        {
            public IControl Control { get; set; }
            public string[] Protocols { get; set; }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            bool isDesign = (bool)this.GetValue(DesignerProperties.IsInDesignModeProperty);

            browser = new ListView2WebBrowser();
            contentPanel.Content = browser.CreateControl(contentPanel);
            browser.Visible = false;
            contentPanel.Content = null;

            if (isDesign)
                browser.Browser.Navigate("http://referencesource.microsoft.com/#");

            if (data == null)
            {
                data = CreateDataGrid();
                PanelDataWpf.Initialize(this.Side);
            }
            if (data != null)
            {
                contentPanel.Content = data;
                WpfContent.AddVisual(this, data, new[] { "file://", "ftp://", "ftps://" });
            }
            if (!isDesign)
                this.BindPanel();
        }

        public ListView2WebBrowser browser {[DebuggerStepThrough] get; set; }
        public ListView2DataGrid data {[DebuggerStepThrough] get; set; }

        protected ListView2DataGrid CreateDataGrid()
        {
            data = new ListView2DataGrid();
            contentPanel.Content = data;

            data.IsReadOnly = true;
            data.RowHeight = 23.0;
            data.SelectionMode = DataGridSelectionMode.Extended;
            data.SelectionUnit = DataGridSelectionUnit.FullRow;
            data.ClipboardCopyMode = DataGridClipboardCopyMode.ExcludeHeader;
            data.EnableRowVirtualization = true;
            data.AutoGenerateColumns = false;
            data.FrozenColumnCount = 0;
            data.HeadersVisibility = DataGridHeadersVisibility.Column;
            data.SetValue(VirtualizingPanel.IsVirtualizingProperty, true);
            data.SetValue(VirtualizingPanel.IsVirtualizingWhenGroupingProperty, true);
            data.SetValue(VirtualizingPanel.VirtualizationModeProperty, VirtualizationMode.Recycling);

            data.VerticalGridLinesBrush = new SolidColorBrush(HtmlMedia.ConvertFromString("#F0F0F0"));
            data.HorizontalGridLinesBrush = data.VerticalGridLinesBrush;

            var columns = data.Columns;
            if (columns.Count != 3)
            {
                columns.Add(new DataGridTextColumn { Header = "Loading..", MinWidth = 80, Width = 120.0 });
                columns.Add(new DataGridTextColumn { Header = "", MinWidth = 50, Width = 60.0 });
                columns.Add(new DataGridTextColumn { Header = "", MinWidth = 70, Width = 80.0 });
            }

            return data;
        }
    }

}
 