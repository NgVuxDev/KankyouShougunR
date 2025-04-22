namespace Shougun.Core.Common.InsatsuSettei
{
    partial class UIHeaderForm
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
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Location = new System.Drawing.Point(465, 3);
            this.windowTypeLabel.Size = new System.Drawing.Size(10, 10);
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(-1, 3);
            this.lb_title.Size = new System.Drawing.Size(460, 35);
            this.lb_title.Text = "印刷設定";
            // 
            // UIHeaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.ClientSize = new System.Drawing.Size(1174, 40);
            this.Name = "UIHeaderForm";
            this.Text = "HeaderSample";
            this.ResumeLayout(false);

        }

        #endregion


    }
}