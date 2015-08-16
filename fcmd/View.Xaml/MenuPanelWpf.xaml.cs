using System.Windows.Controls;
using System.Windows.Markup;

namespace fcmd.View.Xaml
{
    /// <summary>
    /// Interaction logic for MenuWpf.xaml
    /// </summary>
    public partial class MenuPanelWpf : UserControl, IComponentConnector
    {
        public MenuPanelWpf()
        {
            InitializeComponent();
        }

#if !VS ||  __MonoCS__
        internal System.Windows.Controls.Menu menuBar;
        internal System.Windows.Controls.MenuItem mnuLeft;
        internal System.Windows.Controls.MenuItem mnuCommands;
        internal System.Windows.Controls.MenuItem itemExit;
        internal System.Windows.Controls.MenuItem mnuOptions;
        internal System.Windows.Controls.MenuItem itemAbout;
        internal System.Windows.Controls.MenuItem mnuRight;

        private bool _contentLoaded;

        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/fcmdp;component/view.xaml/menupanelwpf.xaml", System.UriKind.Relative);
            System.Windows.Application.LoadComponent(this, resourceLocater);
        }

        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.menuBar = ((System.Windows.Controls.Menu)(target));
                    return;
                case 2:
                    this.mnuLeft = ((System.Windows.Controls.MenuItem)(target));
                    return;
                case 3:
                    this.mnuCommands = ((System.Windows.Controls.MenuItem)(target));
                    return;
                case 4:
                    this.itemExit = ((System.Windows.Controls.MenuItem)(target));
                    return;
                case 5:
                    this.mnuOptions = ((System.Windows.Controls.MenuItem)(target));
                    return;
                case 6:
                    this.itemAbout = ((System.Windows.Controls.MenuItem)(target));
                    return;
                case 7:
                    this.mnuRight = ((System.Windows.Controls.MenuItem)(target));
                    return;
            }
            this._contentLoaded = true;
        }
#endif

    }
}