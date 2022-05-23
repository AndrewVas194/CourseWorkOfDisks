using System.IO;
using System.Diagnostics;

namespace RazdelDiscov
{
    class ExecuteCmd
    {
        public static string ExecuteCmdCommand(string Command, ref int ExitCode)
        {
            ProcessStartInfo ProcessInfo;
            Process Process = new Process();
            string myString = string.Empty;
            ProcessInfo = new ProcessStartInfo("cmd.exe", "/C " + Command);

            ProcessInfo.CreateNoWindow = false;
            ProcessInfo.WindowStyle = ProcessWindowStyle.Normal;
            ProcessInfo.UseShellExecute = false;
            ProcessInfo.RedirectStandardOutput = true;
            Process.StartInfo = ProcessInfo;
            Process = Process.Start(ProcessInfo);
            StreamReader myStreamReader = Process.StandardOutput;
            myString = myStreamReader.ReadToEnd();

            ExitCode = Process.ExitCode;
            Process.Close();

            return myString;
        }
    }
}
