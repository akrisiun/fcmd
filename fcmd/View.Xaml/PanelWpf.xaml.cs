using fcmd.Controller;
using fcmd.View.Xaml;
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

namespace fcmd.View.Xaml
{
    /// <summary>
    /// Interaction logic for PanelWpf.xaml
    /// </summary>
    public partial class PanelWpf : UserControl
    {
        public FileListPanelWpf PanelData { get; private set; }
        public PanelSide Side { get; set; }

        public PanelWpf()
        {
            InitializeComponent();

            PanelData = new FileListPanelWpf(this);
            DataContext = PanelData;
        }
    }
}
