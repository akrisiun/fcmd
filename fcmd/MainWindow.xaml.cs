using fcmd.Platform;
using System;
using System.Windows;
using System.Windows.Media.Imaging;
using fcmd.Model;

namespace fcmd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Xwt.Application.Initialize(Xwt.ToolkitType.Wpf);
            Xwt.Toolkit.Load(Xwt.ToolkitType.Wpf).Invoke(() => InitializeXwt());
                // Initialize() being your own custom method.This is actually what the MixedGtkMacTest does.

            InitializeComponent();

            Title = "FC loading .. " + Environment.CurrentDirectory;
            try
            {   // var compileCheck = GetType().Assembly.GetManifestResourceNames();
                System.IO.Stream iconStream = GetType().Assembly.GetManifestResourceStream("fcmd.FavIcon.ico");
                Icon = IconBitmapDecoder.Create(iconStream, BitmapCreateOptions.None, BitmapCacheOption.None).Frames[0];
            }
            catch { }

            this.Init();    // View.WindowDataStatic.Init(this);

            this.Closed += (s, e) => 
                 Platform.Application.Current.Shutdown();
        }

        void InitializeXwt()
        {
            // Xwt init success
        }
    }
}
