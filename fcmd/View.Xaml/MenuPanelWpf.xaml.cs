using System;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Diagnostics;

namespace fcmd.View.Xaml
{
    public interface IXamlMenu 
    {
        MenuItem mnuCommands {get;}

        MenuItem mnuCommandsView {get;}
        MenuItem mnuCommandsEdit {get;}

        MenuItem mnuCommandsCopy  {get;}
		MenuItem mnuCommandsMove {get;}
		MenuItem mnuCommandsMkDir {get;}
		MenuItem mnuCommandsDelete {get;}
		MenuItem mnuCommandsFindFiles {get;}
        MenuItem itemExit {get;}

        MenuItem mnuOptions {get;}
    }


    /// <summary>
    /// Interaction logic for MenuWpf.xaml
    /// </summary>
	public partial class MenuPanelWpf : UserControl, IXamlMenu, IComponentConnector
    {
        MenuItem IXamlMenu.mnuCommands { [DebuggerStepThrough] get { return this.mnuCommands; } }

        MenuItem IXamlMenu.mnuCommandsView { [DebuggerStepThrough] get { return this.mnuCommandsView; } }
        MenuItem IXamlMenu.mnuCommandsEdit { [DebuggerStepThrough] get { return this.mnuCommandsEdit; } }

        MenuItem IXamlMenu.mnuCommandsCopy { [DebuggerStepThrough] get { return this.mnuCommandsCopy; } }
        MenuItem IXamlMenu.mnuCommandsMove { [DebuggerStepThrough] get { return this.mnuCommandsMove; } }
        MenuItem IXamlMenu.mnuCommandsMkDir { [DebuggerStepThrough] get { return this.mnuCommandsMkDir; } }
        MenuItem IXamlMenu.mnuCommandsDelete { [DebuggerStepThrough] get { return this.mnuCommandsDelete; } }
        MenuItem IXamlMenu.mnuCommandsFindFiles { [DebuggerStepThrough] get { return this.mnuCommandsFindFiles; } }
        MenuItem IXamlMenu.itemExit { [DebuggerStepThrough] get { return this.itemExit; } }

        MenuItem IXamlMenu.mnuOptions { [DebuggerStepThrough] get { return this.mnuOptions; } }

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

		internal System.Windows.Controls.MenuItem mnuCommandsView;
		internal System.Windows.Controls.MenuItem mnuCommandsEdit;
		internal System.Windows.Controls.MenuItem mnuCommandsCopy;
		internal System.Windows.Controls.MenuItem mnuCommandsMove;
		internal System.Windows.Controls.MenuItem mnuCommandsMkDir;
		internal System.Windows.Controls.MenuItem mnuCommandsDelete;
		internal System.Windows.Controls.MenuItem mnuCommandsFindFiles;

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
				this.mnuCommandsView = ((System.Windows.Controls.MenuItem)(target));
				return;
				case 5:
				this.mnuCommandsEdit = ((System.Windows.Controls.MenuItem)(target));
				return;
				case 6:
				this.mnuCommandsCopy = ((System.Windows.Controls.MenuItem)(target));
				return;
				case 7:
				this.mnuCommandsMove = ((System.Windows.Controls.MenuItem)(target));
				return;
				case 8:
				this.mnuCommandsMkDir = ((System.Windows.Controls.MenuItem)(target));
				return;
				case 9:
				this.mnuCommandsDelete = ((System.Windows.Controls.MenuItem)(target));
				return;
				case 10:
				this.mnuCommandsFindFiles = ((System.Windows.Controls.MenuItem)(target));
				return;
				case 11:
				this.itemExit = ((System.Windows.Controls.MenuItem)(target));
				return;
				case 12:
				this.mnuOptions = ((System.Windows.Controls.MenuItem)(target));
				return;
				case 13:
				this.itemAbout = ((System.Windows.Controls.MenuItem)(target));
				return;
				case 14:
				this.mnuRight = ((System.Windows.Controls.MenuItem)(target));
				return;
			}
            this._contentLoaded = true;
        }
#endif

    }
}