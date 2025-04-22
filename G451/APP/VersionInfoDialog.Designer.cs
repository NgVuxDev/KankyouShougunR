namespace Shougun.Core.Common.Login.APP
{
    partial class VersionInfoDialog
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
            this.rtbVersionInfo = new System.Windows.Forms.RichTextBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnLicense = new System.Windows.Forms.Button();
            this.btnMail = new System.Windows.Forms.Button();
            this.btnSupport = new System.Windows.Forms.Button();
            this.btnProtect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtbVersionInfo
            // 
            this.rtbVersionInfo.BackColor = System.Drawing.SystemColors.Window;
            this.rtbVersionInfo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.rtbVersionInfo.Location = new System.Drawing.Point(12, 12);
            this.rtbVersionInfo.Name = "rtbVersionInfo";
            this.rtbVersionInfo.ReadOnly = true;
            this.rtbVersionInfo.Size = new System.Drawing.Size(600, 422);
            this.rtbVersionInfo.TabIndex = 0;
            this.rtbVersionInfo.TabStop = false;
            this.rtbVersionInfo.Text = "";
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(390, 451);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(100, 33);
            this.btnCopy.TabIndex = 2;
            this.btnCopy.Text = "[F11]\r\nコピーする";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnEnd
            // 
            this.btnEnd.Location = new System.Drawing.Point(510, 451);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(100, 33);
            this.btnEnd.TabIndex = 3;
            this.btnEnd.Text = "[F12]\r\n閉じる";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnLicense
            // 
            this.btnLicense.Location = new System.Drawing.Point(270, 451);
            this.btnLicense.Name = "btnLicense";
            this.btnLicense.Size = new System.Drawing.Size(100, 33);
            this.btnLicense.TabIndex = 1;
            this.btnLicense.Text = "[F10]\r\nライセンス更新";
            this.btnLicense.UseVisualStyleBackColor = true;
            this.btnLicense.Click += new System.EventHandler(this.btnLicense_Click);
            // 
            // btnMail
            // 
            this.btnMail.Enabled = false;
            this.btnMail.Location = new System.Drawing.Point(150, 451);
            this.btnMail.Name = "btnMail";
            this.btnMail.Size = new System.Drawing.Size(100, 33);
            this.btnMail.TabIndex = 4;
            this.btnMail.Text = "[F9]\r\nメール送信";
            this.btnMail.UseVisualStyleBackColor = true;
            this.btnMail.Visible = false;
            this.btnMail.Click += new System.EventHandler(this.btnMail_Click);
            // 
            // btnSupport
            // 
            this.btnSupport.Location = new System.Drawing.Point(150, 451);
            this.btnSupport.Name = "btnSupport";
            this.btnSupport.Size = new System.Drawing.Size(100, 33);
            this.btnSupport.TabIndex = 5;
            this.btnSupport.Text = "[F9]\r\nサポートツール";
            this.btnSupport.UseVisualStyleBackColor = true;
            this.btnSupport.Click += new System.EventHandler(this.btnSupport_Click);
            // 
            // btnProtect
            // 
            this.btnProtect.Location = new System.Drawing.Point(30, 451);
            this.btnProtect.Name = "btnProtect";
            this.btnProtect.Size = new System.Drawing.Size(100, 33);
            this.btnProtect.TabIndex = 6;
            this.btnProtect.Text = "[F8]\r\nプロテクト解除";
            this.btnProtect.UseVisualStyleBackColor = true;
            this.btnProtect.Click += new System.EventHandler(this.btnProtect_Click);
            // 
            // VersionInfoDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(624, 496);
            this.Controls.Add(this.btnProtect);
            this.Controls.Add(this.btnSupport);
            this.Controls.Add(this.btnMail);
            this.Controls.Add(this.btnLicense);
            this.Controls.Add(this.btnEnd);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.rtbVersionInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Name = "VersionInfoDialog";
            this.Text = "環境将軍R バージョン情報";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbVersionInfo;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnLicense;
        private System.Windows.Forms.Button btnMail;
        private System.Windows.Forms.Button btnSupport;
        private System.Windows.Forms.Button btnProtect;
    }
}