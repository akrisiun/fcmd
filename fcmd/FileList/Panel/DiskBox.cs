using fcmd.Model;
using pluginner.Widgets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fcmd.FileList
{
    public class DiskCombo : IDriveCombo // , IBackend
    {
        public IControl Target { get; set; }

        public void Populate()
        {
            DiskBox.Populate(this.Target as IComboWidget);
        }

        public void Init(ICommanderWindow window)
        {
        }

        public MainWindow Window { get { return MainWindow.ActiveWindow; } }

    }

    // TODO:
    public static class DiskBox
    {
        public static void Populate(this IComboWidget DiskBox)
        {
            string[] DiskList = null;
            DiskBox.ItemsSource = DiskList;
            // AddSysDrives

            //DiskBox.Content = DiskList;
            //DiskBox.CanScrollByY = false;
        }

        static IEnumerable<Bookmark> AddSysDrives()
        {
            // List<Bookmark> bms = new List<Bookmark>();
            foreach (DriveInfo di in DriveInfo.GetDrives())
            {
                Bookmark bm = new Bookmark();
                bm.title = di.Name;
                bm.url = "file://" + di.Name;
                /*if (di.IsReady)
				{
					NewBtn.TooltipText = di.VolumeLabel + " (" + di.DriveFormat + ")";
				}*/

                //switch (di.DriveType)
                //{
                //    case DriveType.Fixed:
                //        bm.Icon = "(internal)drive-harddisk.png";
                //        break; 

                string d = di.Name;
                //OS-specific icons
                //if (d.StartsWith("A:")) bm.Icon = "(internal)media-floppy.png";
                //if (d.StartsWith("B:")) bm.Icon = "(internal)media-floppy.png";
                //if (d.StartsWith("/dev")) bm.Icon = "(internal)preferences-desktop-peripherals.png";
                //if (d.StartsWith("/proc")) bm.Icon = "(internal)emblem-system.png";
                //if (d == "/") bm.Icon = "(internal)root-folder.png";

                yield return bm;
            }
        }

    }

    /// <summary>Represents a item in the bookmark DB</summary>
    public class Bookmark
    {
        /// <summary>The URL of the bookmark</summary>
        public string url { get; set; }
        /// <summary>The label (caption, title, mark) of the bookmark</summary>
        public string title { get; set; }

        public ICollection<Bookmark> SubMenu;

        // private Image i = Image.FromResource("pluginner.Resources.folder.png");
        // private bool imageIsSet;

        /// <summary>Get the icon of the bookmark (or default icon if the bookmark hasn't an icon)</summary>
        // public Image GetIcon() 
    }
}