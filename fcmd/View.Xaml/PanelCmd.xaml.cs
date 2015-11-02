using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Markup;

namespace fcmd.View.Xaml
{
    /// <summary>
    /// Interaction logic for PanelCmd.xaml
    /// </summary>
    public partial class PanelCmd : UserControl, IComponentConnector
    {
        public PanelCmd()
        {
            InitializeComponent();

            Loaded += (s, e) => BindPanel.PanelCmd(this);
        }

#if !VS || __MonoCS__
        internal System.Windows.Controls.TextBox selected;
        internal System.Windows.Controls.ComboBox cmd;

        private bool _contentLoaded;

        public void InitializeComponent()
        {
            if (_contentLoaded)
                return;
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/fcmdp;component/view.xaml/panelcmd.xaml", System.UriKind.Relative);
            System.Windows.Application.LoadComponent(this, resourceLocater);
        }

        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.selected = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 2:
                    this.cmd = ((System.Windows.Controls.ComboBox)(target));
                    return;
            }
            this._contentLoaded = true;
        }
#endif 

    }
}
