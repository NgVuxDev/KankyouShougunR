namespace Shougun.Core.ElectronicManifest.TuuchiJouhouShoukai
{
    partial class UIHeaderMeisai
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
            this.cwtl_msg = new r_framework.CustomControl.CustomWindowTypeLabel();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Location = new System.Drawing.Point(9, 8);
            this.windowTypeLabel.Size = new System.Drawing.Size(81, 32);
            this.windowTypeLabel.Visible = false;
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(9, 8);
            this.lb_title.Size = new System.Drawing.Size(300, 34);
            // 
            // cwtl_msg
            // 
            this.cwtl_msg.BackColor = System.Drawing.Color.White;
            this.cwtl_msg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cwtl_msg.Location = new System.Drawing.Point(360, 9);
            this.cwtl_msg.Name = "cwtl_msg";
            this.cwtl_msg.Size = new System.Drawing.Size(792, 34);
            this.cwtl_msg.TabIndex = 389;
            this.cwtl_msg.WindowType = r_framework.Const.WINDOW_TYPE.NONE;
            // 
            // UIHeaderMeisai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 46);
            this.Controls.Add(this.cwtl_msg);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Location = new System.Drawing.Point(12, 6);
            this.Name = "UIHeaderMeisai";
            this.Text = "HeaderSample";
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.cwtl_msg, 0);
            this.ResumeLayout(false);

        }

        #endregion

        internal r_framework.CustomControl.CustomWindowTypeLabel cwtl_msg;


    }
}