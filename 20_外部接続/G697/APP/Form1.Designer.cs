namespace Shougun.Core.ExternalConnection.HaisouKeikakuTeiki
{
    partial class Form1
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
            this.NaviAlertMsg = new System.Windows.Forms.TextBox();
            this.bt_func12 = new r_framework.CustomControl.CustomButton();
            this.bt_csv = new r_framework.CustomControl.CustomButton();
            this.SuspendLayout();
            // 
            // NaviAlertMsg
            // 
            this.NaviAlertMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.NaviAlertMsg.BackColor = System.Drawing.SystemColors.Window;
            this.NaviAlertMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NaviAlertMsg.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.NaviAlertMsg.ForeColor = System.Drawing.Color.Black;
            this.NaviAlertMsg.Location = new System.Drawing.Point(12, 12);
            this.NaviAlertMsg.Multiline = true;
            this.NaviAlertMsg.Name = "NaviAlertMsg";
            this.NaviAlertMsg.ReadOnly = true;
            this.NaviAlertMsg.Size = new System.Drawing.Size(446, 340);
            this.NaviAlertMsg.TabIndex = 294;
            // 
            // bt_func12
            // 
            this.bt_func12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_func12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func12.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_func12.Location = new System.Drawing.Point(378, 358);
            this.bt_func12.Name = "bt_func12";
            this.bt_func12.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func12.Size = new System.Drawing.Size(80, 35);
            this.bt_func12.TabIndex = 295;
            this.bt_func12.TabStop = false;
            this.bt_func12.Tag = "func12";
            this.bt_func12.Text = "閉じる";
            this.bt_func12.UseVisualStyleBackColor = false;
            // 
            // bt_csv
            // 
            this.bt_csv.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_csv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_csv.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_csv.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_csv.Location = new System.Drawing.Point(292, 358);
            this.bt_csv.Name = "bt_csv";
            this.bt_csv.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_csv.Size = new System.Drawing.Size(80, 35);
            this.bt_csv.TabIndex = 296;
            this.bt_csv.TabStop = false;
            this.bt_csv.Tag = "func12";
            this.bt_csv.Text = "CSV出力";
            this.bt_csv.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 404);
            this.Controls.Add(this.bt_csv);
            this.Controls.Add(this.bt_func12);
            this.Controls.Add(this.NaviAlertMsg);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "エラー情報";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NaviAlertMsg;
        public r_framework.CustomControl.CustomButton bt_func12;
        public r_framework.CustomControl.CustomButton bt_csv;
    }
}