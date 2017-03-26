using pluginner.Widgets;
using System;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

//#if !VS13
//[assembly: NeutralResourcesLanguageAttribute("en", UltimateResourceFallbackLocation.MainAssembly)]
//#endif

namespace fcmd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IComponentConnector, IWin32Window, IDisposable, IControl, SharpShell.IWin32Window
    {
        public static bool AppLoading { get; set; }
        public static MainWindow ActiveWindow { get; private set; }
        public Tuple<int, int> PointToScreen(int X, int Y)
        {
            return SharpShell.Win32Control.PointToScreen(this, this.Handle, X, Y);
        }

        object IUIDispacher.Dispacher { get { return (this as DispatcherObject).Dispatcher; } }
        bool IUIDispacher.CheckAccess() { return Dispatcher.CheckAccess(); }
        public bool? Visible { get; set; }

        static MainWindow() { AppLoading = true; AllowShutdown = true; }

        public IntPtr Handle { get; set; }

        public MainWindow()
        {
            ActiveWindow = this;

            ResourceManager rm = new ResourceManager("Resources", typeof(MainWindow).Assembly);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            InitializeComponent();

            Title = "FC loading .. " + Environment.CurrentDirectory;
            try
            {   // var compileCheck = GetType().Assembly.GetManifestResourceNames();
                System.IO.Stream iconStream = GetType().Assembly.GetManifestResourceStream("fcmd.FavIcon.ico");
                Icon = IconBitmapDecoder.Create(iconStream, BitmapCreateOptions.None, BitmapCacheOption.None).Frames[0];
            }
            catch { }

            App.ConsoleWriteLine("Xwt InitializeApp");
            Xwt.Application.Initialize(Xwt.ToolkitType.Wpf);
            Xwt.Toolkit.Load(Xwt.ToolkitType.Wpf)
                .Invoke(() => InitializeXwt());
            // Initialize() being your own custom method.This is actually what the MixedGtkMacTest does.
            App.ConsoleWriteLine("Xwt InitializeApp Stage#2");

            App.ConsoleWriteLine("MainWindow Init");
            this.Init();    // View.WindowDataStatic.Init(this);

            Closed += MainWindow_Closed;
            Loaded += OnLoaded;
        }

        void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            this.Handle = new WindowInteropHelper(this).Handle;
            //HwndSource source = (HwndSource)HwndSource.FromVisual(lst);
        }

        public virtual void Dispose() { Handle = IntPtr.Zero; }

        public static bool AllowShutdown { get; set; }    // for test unit or multiple MainWindow

        void MainWindow_Closed(object sender, EventArgs e)
        {
            if (App.Instance != null && !AllowShutdown)
                return;
            Platform.Application.Current.Shutdown();
        }

#if !VS || __MonoCS__
        // http://developer.xamarin.com/guides/android/under_the_hood/build_process/
        // System.Windows.Markup.IComponentConnector

        internal fcmd.View.Xaml.MenuPanelWpf Menu;
        internal fcmd.View.Xaml.PanelCmd PanelCmd;
        // internal fcmd.View.Xaml.FooterCmd FooterCmd;

        internal fcmd.View.Xaml.PanelWpf LeftPanel;
        internal fcmd.View.Xaml.PanelWpf RightPanel;
        internal System.Windows.Controls.GridSplitter panelSplitter;

        private bool _contentLoaded;


        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;

            System.Uri resourceLocator = new System.Uri("/fcmdp;component/mainwindow.xaml", System.UriKind.Relative);
            System.Windows.Application.LoadComponent(this, resourceLocator);
	        //   at System.Resources.ResourceManager.InternalGetResourceSet(CultureInfo culture, Boolean createIfNotExists, Boolean tryParents)
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
                    this.Menu = ((fcmd.View.Xaml.MenuPanelWpf)(target));
                    return;
                case 2:
                    this.PanelCmd = ((fcmd.View.Xaml.PanelCmd)(target));
                    return;
                case 3:
                    this.LeftPanel = ((fcmd.View.Xaml.PanelWpf)(target));
                    return;
                case 4:
                    this.RightPanel = ((fcmd.View.Xaml.PanelWpf)(target));
                    return;
                case 5:
                    this.panelSplitter = ((System.Windows.Controls.GridSplitter)(target));
                    return;
            }
            this._contentLoaded = true;
        }
#endif

        void InitializeXwt()
        {
            // Xwt init success
            App.ConsoleWriteLine("InitializeXwt Stage#3");
        }
    }
}
