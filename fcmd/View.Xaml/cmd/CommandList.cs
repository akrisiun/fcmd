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
        public MkDirCommand cmdMkDir;
        public CpCommand cmdCp;
        public MvCommand cmdMv;

        public RmCommand cmdRm;

        public EdCommand cmdEdit;
        public VwCommand cmdView;

        public CommandList()
        {
            cmdMkDir = new MkDirCommand() { Target = MainWindow.ActiveWindow };
            Add(cmdMkDir);

            Add((cmdCp = new CpCommand()));
            Add((cmdMv = new MvCommand()));
            Add((cmdRm = new RmCommand()));

            Add((cmdEdit = new EdCommand()));
            Add((cmdView = new VwCommand()));
        }

        public void Bind(IXamlMenu menu)
        {
            menu.mnuCommandsMkDir.Command = this.cmdMkDir;
        }
    }

}
