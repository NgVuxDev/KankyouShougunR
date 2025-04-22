namespace r_framework.APP.PopUp.Base
{
    public partial class SuperPopupForm
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
            // SuperPopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(632, 483);
            this.Name = "SuperPopupForm";
            this.Text = "BasePopupForm";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SuperForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SuperForm_KeyUp);
            this.KeyPreview = true;
            this.ResumeLayout(false);

        }

        #endregion

        protected r_framework.Components.ImeStatus imeStatus;

    }
}