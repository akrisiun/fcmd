using SharpShell;
using SharpShell.Helpers;
using SharpShell.SharpContextMenu;
using System;
using System.IO;
using System.Threading;

namespace pluginner.Shell
{
    public class ContextMenu
    {
        // public static System.Windows.Forms.Control Control { get; set; }

        public static IWin32Window Menu { get; set; }

        public static void ShowAsync(IWin32Window owner, string path, Action<ThreadStart> doEvents = null)
        {
            var dir = Path.GetDirectoryName(path);

            // NativePidl.PIDListFromPath
            ShellItem itemDir = ShellItem.FromPath(dir);
            ShellItem item = ShellItem.FromPath(path);

            var pidl = new ShellContextMenu(item);
            if (pidl == null)
                return;

            pidl.ShowContextMenuLoop(owner,
                 pos: new System.Drawing.Point(0, 0));
        }

        // IShellFolder GetIShellFolder(ShellItem parent)

        public static IContextMenu ExtractMenu(string path)
        {
            IContextMenu comInterface = null;

            return comInterface;
        }
    }

    public static class ShellExecuteVerb
    {
        public const string COPY = "copy";

        /// <summary>
        /// Invokes the Copy command on the shell item(s).
        /// </summary>
        public static void Invoke(IContextMenu comInterface, string verb)
        {
            ShellExec.InvokeVerb(comInterface, verb);
        }
    }

}
