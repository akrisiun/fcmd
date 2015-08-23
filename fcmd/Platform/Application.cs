using fcmd.Model;
using pluginner.Toolkit;
using System;

namespace fcmd.Platform
{
    public enum OS
    {
        Windows = 1,
        Unix = 4,
        MacOSX = 6
    }

    public interface IApplication
    {
        void Shutdown();

        ICommanderWindow MainWindow { get; }

        IBackend Backend { get; }
    }

    public class Application
    {
#if WPF
        public static IApplication Current { get { return System.Windows.Application.Current as IApplication; } }
#endif

        public Xwt.ToolkitType ToolkitType { get { return OSVersionEx.GetToolkitType(); } }
        public PlatformID PlatformID { get { return OSVersionEx.Platform; } }
        public OS OS
        {
            get
            {
                var p = OSVersionEx.Platform; //  Environment.OSVersion.Platform;
                return p == PlatformID.Unix ? OS.Unix
                    : p == PlatformID.MacOSX ? OS.MacOSX
                         : OS.Windows;
            }
        }

        public static bool IsWPF { get; set; }
        public static bool IsGTK3 { get; set; }
        public static bool Is64Bit { get; set; }
    }
}
