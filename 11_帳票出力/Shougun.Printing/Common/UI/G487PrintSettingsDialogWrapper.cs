using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Shougun.Printing.Common.UI
{
    public partial class G487PrintSettingsDialogWrapper : Form
    {
        public G487PrintSettingsDialogWrapper()
        {
            this.SuspendLayout();
            this.Name = "G487PrintSettingsDialogWrapper";
            this.Opacity = 0D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "G487PrintSettingsDialogWrapper";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.ResumeLayout(false);

        }

        protected override void OnLoad( EventArgs e)
        {
            LastError.Clear();
            this.Hide();
            switch (Initializer.ProcessMode)
            {
                case ProcessMode.OnPremisesFrontProcess:
                    {
                        var dialog = new UI.ReportSettingsDialog();
                        dialog.ShowDialog();
                        dialog.Dispose();
                        dialog = null;
                    }
                    break;

                case ProcessMode.CloudServerSideProcess:
                    {
                        var dialog = new UI.ServerPrintSettingsDialog();
                        dialog.ShowDialog();
                        dialog.Dispose();
                        dialog = null;
                    }
                    break;

                default:
                    LastError.Message = "ProcessModes is invalid.";
                    break;
            }

            if (LastError.HasError)
            {
                UI.ErrorMessageBox.ShowLastError();
            }

            this.Close();
            // this.Dispose(); FormManager側でする
        }
    }
}
