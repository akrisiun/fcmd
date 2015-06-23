using System;
using System.Windows;
using System.Windows.Media.Imaging;

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
            InitializeComponent();

            Title = "FC loading .. " + Environment.CurrentDirectory;
            try
            {   // var compileCheck = GetType().Assembly.GetManifestResourceNames();
                System.IO.Stream iconStream = GetType().Assembly.GetManifestResourceStream("fcmd.FavIcon.ico");
                Icon = IconBitmapDecoder.Create(iconStream, BitmapCreateOptions.None, BitmapCacheOption.None).Frames[0];
            }
            catch { }

            theme.WindowDataStatic.Init(this);

            this.Closed += (s, e) => 
                Application.Current.Shutdown();
        }
    }
}
