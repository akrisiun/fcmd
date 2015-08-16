/* The File Commander
 * OSVersionEx, decorator for Environment.OSVersion
 * (C) The File Commander Team - https://github.com/atauenis/fcmd
 * (C) 2014, Evgeny Akhtimirov (wilbit@me.com)
 * Contributors should place own signs here.
 */
using System;
using System.ComponentModel;
using System.Diagnostics;
using Xwt;

namespace pluginner.Toolkit
{

    public enum OS
    {
        Windows,
        Linux,
        Mac,
        Android,
        IOS,
    }

    public enum Architecture
    {
        x64,
        x86,
        arm,
        mips,
    }

    // ReSharper disable once InconsistentNaming
    public static class OSVersionEx
    {
        public static ToolkitType GetToolkitType()
        {
            var platform = Platform;
            switch (platform)
            {
                case PlatformID.Win32NT:
                    return ToolkitType.Wpf;
                case PlatformID.Unix:
                    return ToolkitType.Gtk3;
                case PlatformID.MacOSX:
                    return ToolkitType.Cocoa;
                default:
                    throw new NotSupportedException(
                        string.Format("Not supported value {0} for {1} type", platform.ToString(), typeof(PlatformID)));
            }
        }

        public static bool IsWindows
        {
            get { return Environment.OSVersion.Platform != PlatformID.Unix && Environment.OSVersion.Platform != PlatformID.MacOSX; }
        }

        public static bool IsPosix { get { return !IsWindows; } }
        // public static OS OS;

        //public static void HideConsole()
        //{
        //    IntPtr hwnd = InternalWindows.GetConsoleWindow();
        //    InternalWindows.ShowWindow(hwnd, SW_HIDE);
        //}

        //[DllImport("libc")]
        //static extern int uname(IntPtr buf);

        public static bool IsMono { get { return Type.GetType("Mono.Runtime") != null; } }

        public static bool Is64Bit { get { return Environment.Is64BitProcess; } }
        public static bool Is32Bit { get { return !Environment.Is64BitProcess; } }


        public static bool PlatformX86 { get { return Architecture.Contains("AMD") || Architecture.Contains("X86"); } }
        public static bool PlatformARM { get { return false; } }    // TODO
        public static string Architecture
        {
            get
            {
                string _Architecture = string.Empty;
                if (!IsPosix)
                    _Architecture = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE");

                // else
                // {
                //		try
                //		{
                //			var Result = ProcessUtils.ExecuteCommand("uname", "-m");
                //                _Architecture = Result.OutputString;
                return _Architecture;
            }
        }

        public static bool Is64BitProcess
        {
            get
            {
                return Environment.Is64BitProcess;
            }
        }

        public static PlatformID Platform
        {
            get
            {
                var platformId = Environment.OSVersion.Platform;
                if (platformId != PlatformID.Unix)
                {
                    return platformId;
                }

                var platformHasDarwinKernel = GetPlatformHasDarwinKernel();
                return platformHasDarwinKernel
                    ? PlatformID.MacOSX
                    : PlatformID.Unix;
            }
        }

        public static string ServicePack
        {
            get { return Environment.OSVersion.ServicePack; }
        }

        public static Version Version
        {
            get { return Environment.OSVersion.Version; }
        }

        public static string VersionString
        {
            get { return Environment.OSVersion.VersionString; }
        }

        private static bool GetPlatformHasDarwinKernel()
        {
            if (_platformHasDarwinKernel != null)
            {
                return _platformHasDarwinKernel.Value;
            }

            var process = new Process { StartInfo = { FileName = "uname", UseShellExecute = false, RedirectStandardOutput = true } };
            try
            {
                process.Start();
                var processOutput = process.StandardOutput.ReadToEnd();
                _platformHasDarwinKernel = processOutput.StartsWith("Darwin", StringComparison.InvariantCultureIgnoreCase);
                process.WaitForExit();
            }
            catch (Win32Exception)
            {
                //when "uname" not found
                _platformHasDarwinKernel = false;
            }
            finally
            {
                process.Dispose();
            }

            return _platformHasDarwinKernel.Value;
        }

        private static bool? _platformHasDarwinKernel;
    }
}
