using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Reflection;
using System.Configuration;
using System.Diagnostics;

namespace Shougun.Printing.Verup
{
    public static class Launcher
    {

        public static bool ExistsVerupServerSide(out int serverRev, out int clientRev)
        {
            serverRev = 0;
            clientRev = 0;

            try
            {
                var printingDirectory = VerupIO.GetClientPrintingDirectory();
                if (printingDirectory != null)
                {
                    if (!VerupIO.ExistsVerupFlagFile(printingDirectory))
                    {
                        var clientVerupDirectory = VerupIO.GetClientVerupDirectory();
                        if (Directory.Exists(clientVerupDirectory))
                        {
                            serverRev = VerupIO.ReadRevisionFile(VerupIO.GetLocalApplicationDirectory());
                            clientRev = VerupIO.ReadRevisionFile(clientVerupDirectory);
                            return (serverRev > clientRev);
                        }
                    }
                }
            }
            catch
            {
            }
            return false;
        }


        public static bool LaunchVerupServerSide()
        {
            try
            {
                Process.Start("Shougun.Printing.Verup.exe", "CloudServerSideProcess");
                return true;
            }
            catch
            {
            }
            return false;
        }

        public static bool ExistsVerupClientSide(out int serverRev, out int clientRev)
        {
            serverRev = 0;
            clientRev = 0;

            try
            {
                var printingDirectory = VerupIO.GetLocalPrintingDirectory();
                if (VerupIO.ExistsVerupFlagFile(printingDirectory))
                {
                    var varupDirectory = VerupIO.GetLocalVerupDirectory();
                    if (VerupIO.ExistsVerupProgram(varupDirectory))
                    {
                        serverRev = VerupIO.ReadRevisionFile(VerupIO.GetLocalVerupDirectory());
                        clientRev = VerupIO.ReadRevisionFile(VerupIO.GetLocalApplicationDirectory());
                        if (serverRev > clientRev)
                        {
                            return true;
                        }
                    }
                    VerupIO.DeleteVerupFlagFile(printingDirectory);
                }
            }
            catch
            {
            }
            return false;
        }

        public static bool LaunchVerupClientSide()
        {
            try
            {
                var installDir = VerupIO.GetLocalApplicationDirectory();
                var processId = Process.GetCurrentProcess().Id.ToString();
                var arguments = string.Format("{0} {1} \"{2}\"",
                            "CloudClientSideProcess", processId, installDir);

                var startInfo = new ProcessStartInfo();
                startInfo.FileName = Path.Combine(VerupIO.GetLocalVerupDirectory(), VerupIO.VerupProgramName);
                startInfo.Verb = "RunAs";
                startInfo.Arguments = arguments;
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = true;
                using (var process = Process.Start(startInfo))
                {
                }
                return true;
            }
            catch (System.ComponentModel.Win32Exception)
            {
                // UACダイアログでユーザーキャンセルなど
            }
            catch
            {
            }
            return false;
        }

        public static void CancelVerupClientSide()
        {
            // 下記の2つのファイルを削除すれば次回将軍起動時にまた更新の準備をしてくれる

            // Verupのリビジョンファイルを削除
            File.Delete(Path.Combine(VerupIO.GetLocalVerupDirectory(), VerupIO.RevisionFileName));

            // Printingのフラグファイル削除
            var printingDirectory = VerupIO.GetLocalPrintingDirectory();
            if (VerupIO.ExistsVerupFlagFile(printingDirectory))
            {
                VerupIO.DeleteVerupFlagFile(printingDirectory);
            }
        }

        public static void LaunchBackgroundPrintingProcess(string directory, bool reBoot, bool onpremises)
        {
            var processName = Path.GetFileNameWithoutExtension(VerupIO.ClientProgramName);
            var processes = Process.GetProcessesByName(processName);
            if (processes.Length > 0)
            {
                if (reBoot)
                {
                    Launcher.TerminateBackgroundPrintingProcess();
                }
                else
                {
                    return; // 既に起動済み
                }
            }

            var startInfo = new ProcessStartInfo();
            startInfo.FileName = Path.Combine(directory, VerupIO.ClientProgramName);
            if (onpremises)
            {
                startInfo.Arguments = "OnPremisesBackProcess";
            }
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;  //最小化

            using (var process = Process.Start(startInfo))
            {
            }
        }

        public static void TerminateBackgroundPrintingProcess()
        {
            var processName = Path.GetFileNameWithoutExtension(VerupIO.ClientProgramName);
            var processes = Process.GetProcessesByName(processName);
            foreach (var process in processes)
            {
                if (!process.CloseMainWindow())
                {
                    process.Kill();
                }
            }
        }
    }
}
