using fcmd.Controller;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fcmd.View.Xaml
{
    public class CommandList : Collection<pluginner.Widgets.IFcmdCommand>
    {
        MkDirCommand cmdMkDir;
        CpCommand cmdCp;

        public CommandList()
        {
            cmdMkDir = new MkDirCommand() { Target = MainWindow.ActiveWindow };
            cmdCp = new CpCommand();

            Add(cmdMkDir);
            Add(cmdCp);
        }

        public void Bind(IXamlMenu menu)
        {
            menu.mnuCommandsMkDir.Command = this.cmdMkDir;
        }
    }

}
