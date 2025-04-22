namespace Shougun.Core.Allocation.HannyuusakiKyuudouNyuuryoku
{
    partial class UIHeader
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
            this.txt_nengappi = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Location = new System.Drawing.Point(1, 2);
            this.windowTypeLabel.Size = new System.Drawing.Size(25, 29);
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(32, 2);
            this.lb_title.Size = new System.Drawing.Size(269, 34);
            // 
            // txt_nengappi
            // 
            this.txt_nengappi.Font = new System.Drawing.Font("ＭＳ ゴシック", 18F);
            this.txt_nengappi.Location = new System.Drawing.Point(344, 5);
            this.txt_nengappi.Name = "txt_nengappi";
            this.txt_nengappi.ReadOnly = true;
            this.txt_nengappi.Size = new System.Drawing.Size(136, 31);
            this.txt_nengappi.TabIndex = 389;
            this.txt_nengappi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(681, 46);
            this.Controls.Add(this.txt_nengappi);
            this.Name = "UIHeader";
            this.Text = "UIHeader";
            this.Controls.SetChildIndex(this.txt_nengappi, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txt_nengappi;


    }
}