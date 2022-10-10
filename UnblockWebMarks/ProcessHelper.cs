using System;
using System.Diagnostics;

namespace UnblockWebMarks
{
    public class ProcessHelper
    {
        public static void ExecuteCommands(string exeFilePath, string args, string[] commands = null, DataReceivedEventHandler errorEventHandler = null)
        {
            using (Process proc = new Process())
            {
                if (errorEventHandler != null)
                {
                    proc.ErrorDataReceived += errorEventHandler;
                }

                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.FileName = exeFilePath;
                proc.StartInfo.Arguments = args;
                proc.StartInfo.CreateNoWindow = true;

                proc.Start();

                if (commands != null && commands.Length > 0)
                {
                    foreach (var cmd in commands)
                    {
                        proc.StandardInput.WriteLine(cmd);
                    }
                }

                proc.Kill();
            }
        }
    }
}
