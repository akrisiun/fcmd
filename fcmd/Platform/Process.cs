using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fcmd.Platform
{
    public class ProcessCmd
    {
        public static async void Command(string command, object box)
        {
            var info = new ProcessStartInfo {
                RedirectStandardError = false, // true,
                RedirectStandardInput = false,
                RedirectStandardOutput = false,
                CreateNoWindow = false,
                WorkingDirectory = Environment.CurrentDirectory,
                UseShellExecute = true,
                Arguments = "/C " + command
            };
            info.FileName = "cmd.exe";

            var task = Task.Factory.StartNew(() => RunProcess(info));
            await task;
        }

        public static void RunProcess(ProcessStartInfo info)
        {
            try {
                using (var proc = new Process() { StartInfo = info })
                {
                    proc.Start();
                    //proc.WaitForExit();
                    //proc.Dispose()
                }
            } 
            catch (Exception ex) { Console.WriteLine(ex); }
        }
    }
}
