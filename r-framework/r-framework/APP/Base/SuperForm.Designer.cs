namespace r_framework.APP.Base
{
    partial class SuperForm
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
            this.components = new System.ComponentModel.Container();
            this.imeStatus = new r_framework.Components.ImeStatus(this.components);
            this.SuspendLayout();
            // 
            // SuperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 593);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "SuperForm";
            this.Text = "SuperForm";
            this.Load += new System.EventHandler(this.SuperForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SuperForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SuperForm_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        public r_framework.Components.ImeStatus imeStatus;


    }
}