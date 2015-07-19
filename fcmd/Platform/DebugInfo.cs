using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fcmd.Platform
{
    public static class DebugInfo
    {
        public static string AboutString { get
            {
                var productVersion = MainWindow.ProductVersion;
                var conf = ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.PerUserRoamingAndLocal);

                string aboutString = string.Format(
                    Localizator.GetString("FileCommanderVer"),
                    "File Commander",
                    productVersion + "-virtualmode",
                    "\nhttps://github.com/atauenis/fcmd",
                    conf.FilePath,
                    Environment.OSVersion,
                    Environment.Version + (Environment.Is64BitProcess ? " x86-64" : " x86")
                    );

                return aboutString;
            }
        }

        public static void Show()
        {
        }
    }
}
