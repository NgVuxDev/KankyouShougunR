using r_framework.CustomControl;

namespace r_framework.APP.Base
{
    partial class BusinessBaseForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pn_foot.SuspendLayout();
            this.ProcessButtonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // txb_process
            // 
            this.txb_process.PopupGetMasterField = "";
            this.txb_process.PopupSetFormField = "";
            // 
            // BusinessBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1192, 730);
            this.Name = "BusinessBaseForm";
            this.pn_foot.ResumeLayout(false);
            this.ProcessButtonPanel.ResumeLayout(false);
            this.ProcessButtonPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

    }
}