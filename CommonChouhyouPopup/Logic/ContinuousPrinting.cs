using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonChouhyouPopup.Logic
{
    static public class ContinuousPrinting
    {
        public static bool IsRunning { get; private set; }
        private static string _printingDirectory = null;

        public static void Initialize()
        {
            ContinuousPrinting.IsRunning = false;
            AsyncXpsTransferThread.Start();
            AsyncXpsExportThread.Start();
        }

        public static void Terminalte()
        {
            AsyncXpsExportThread.Terminate();
            AsyncXpsTransferThread.Terminate();
        }
    
        public static bool Begin()
        {
            bool success = false;

            if (ContinuousPrinting.IsRunning)
            {
                System.Windows.Forms.MessageBox.Show("現在、他の帳票の連続印刷中です。\r\n他の帳票の印刷が完了してからリトライしてください", "印刷");
            }
            else
            {
                success = Shougun.Printing.Common.AbortPrinting.BeginSequence();
                if (success)
                {
                    ContinuousPrinting._printingDirectory = 
                        Shougun.Printing.Common.Initializer.GetXpsFilePrintingDirectory();
                    ContinuousPrinting.IsRunning = true;
                }
            }
            return success;
        }

        public static string GetXpsPrintingDirectory()
        {
            string directory = null;
            if (ContinuousPrinting.IsRunning)
            {
                directory = ContinuousPrinting._printingDirectory;
            }
            else
            {
                directory = Shougun.Printing.Common.Initializer.GetXpsFilePrintingDirectory();
            }
            return directory;
        }

        public static bool IsAbortRequired
        {
            get
            {
                if (ContinuousPrinting.IsRunning)
                {
                    return Shougun.Printing.Common.AbortPrinting.IsAbortRequired();
                }
                return false;
            }
        }

        public static void End(bool abort)
        {
            if (ContinuousPrinting.IsRunning)
            {
                if (abort)
                {
                    AsyncXpsExportThread.Abort();
                    AsyncXpsTransferThread.Abort();
                }

                Shougun.Printing.Common.AbortPrinting.TerminateSequence();
                ContinuousPrinting.IsRunning = false;
            }
        }
    }
}
