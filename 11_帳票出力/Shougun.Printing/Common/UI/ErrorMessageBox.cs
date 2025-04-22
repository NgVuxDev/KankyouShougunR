using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shougun.Printing.Common.UI
{
    public static class ErrorMessageBox
    {
        public static void Show(string message)
        {
            MessageBox.Show(message, Application.ProductName, 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowLastError()
        {
            MessageBox.Show(LastError.FullMessage, Application.ProductName,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Show(Exception ex)
        {
            MessageBox.Show(ex.Message, Application.ProductName,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
