using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Shougun.Printing.Common;
using System.Threading;

namespace Shougun.Printing.Client
{
    public static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args )
        {
            bool signaled = false;
            using (var mutexSingleton = new System.Threading.Mutex(false, typeof(Program).FullName))
            {
                try
                {
                    if (mutexSingleton.WaitOne(1000, false))
                    {
                        signaled = true;
                        Program.main2(args);
                    }
                }
                catch (AbandonedMutexException)
                {
                    signaled = true;
                    Program.main2(args);
                }
                finally
                {
                    if (signaled)
                    {
                        mutexSingleton.ReleaseMutex();
                    }
                }
            }
        }

        static void main2(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (SystemInformation.TerminalServerSession)
            {
                LastError.Message = "ターミナルセッションでは動作できません。";
                Common.UI.ErrorMessageBox.ShowLastError();
                return;
            }

            // 
            var processMode = ProcessMode.NotSet;

            if (args.Length > 0 && args[0].Equals(ProcessMode.OnPremisesBackProcess.ToString()))
            {
                processMode = ProcessMode.OnPremisesBackProcess;
            }
            else
            {
                processMode = ProcessMode.CloudClientSideProcess;
            }

            if (Initializer.Initialize(processMode))
            {
                if (Initializer.IsPrintingMode)
                {
                    Shougun.Printing.Viewer.Initializer.Initialize();
                }

                var backgorundForm = new BackgroundForm();
                Application.Run(backgorundForm);
            }
            else
            {
                Common.UI.ErrorMessageBox.ShowLastError();
            }
        }
    }
}
