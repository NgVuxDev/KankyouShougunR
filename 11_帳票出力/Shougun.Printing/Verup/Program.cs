using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Shougun.Printing.Verup
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string [] argv)
        {

            var mutexSingleton = new System.Threading.Mutex(false, typeof(Program).FullName);
            if (mutexSingleton.WaitOne(0, false))
            {
                try
                {
                    if (argv.Length > 0)
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);

                        var verupform = new VerupForm();
                        verupform.argv = argv;
                        Application.Run(verupform);
                    }

                }
                finally
                {
                    mutexSingleton.ReleaseMutex();
                }
            }
            mutexSingleton.Close();
        }
    }
}
