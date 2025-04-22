namespace Shougun.Core.Common.Login.APP
{
    partial class VersionInfoDialogDiff
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
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.spConDiff = new System.Windows.Forms.SplitContainer();
            this.lblOldConf = new System.Windows.Forms.Label();
            this.rtbOldConf = new System.Windows.Forms.RichTextBox();
            this.lblNewConf = new System.Windows.Forms.Label();
            this.rtbNewConf = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.spConDiff)).BeginInit();
            this.spConDiff.Panel1.SuspendLayout();
            this.spConDiff.Panel2.SuspendLayout();
            this.spConDiff.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInsert
            // 
            this.btnInsert.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnInsert.Location = new System.Drawing.Point(395, 358);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(120, 33);
            this.btnInsert.TabIndex = 2;
            this.btnInsert.Text = "[F11]\r\n更新する";
            this.btnInsert.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(537, 358);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(115, 33);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "[F12]\r\nキャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // spConDiff
            // 
            this.spConDiff.Location = new System.Drawing.Point(12, 12);
            this.spConDiff.Name = "spConDiff";
            // 
            // spConDiff.Panel1
            // 
            this.spConDiff.Panel1.Controls.Add(this.lblOldConf);
            this.spConDiff.Panel1.Controls.Add(this.rtbOldConf);
            // 
            // spConDiff.Panel2
            // 
            this.spConDiff.Panel2.Controls.Add(this.lblNewConf);
            this.spConDiff.Panel2.Controls.Add(this.rtbNewConf);
            this.spConDiff.Size = new System.Drawing.Size(643, 330);
            this.spConDiff.SplitterDistance = 319;
            this.spConDiff.TabIndex = 4;
            // 
            // lblOldConf
            // 
            this.lblOldConf.AutoSize = true;
            this.lblOldConf.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblOldConf.Location = new System.Drawing.Point(3, 10);
            this.lblOldConf.Name = "lblOldConf";
            this.lblOldConf.Size = new System.Drawing.Size(116, 15);
            this.lblOldConf.TabIndex = 2;
            this.lblOldConf.Text = "現在の構成情報";
            // 
            // rtbOldConf
            // 
            this.rtbOldConf.BackColor = System.Drawing.SystemColors.Window;
            this.rtbOldConf.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.rtbOldConf.Location = new System.Drawing.Point(3, 46);
            this.rtbOldConf.Name = "rtbOldConf";
            this.rtbOldConf.ReadOnly = true;
            this.rtbOldConf.Size = new System.Drawing.Size(313, 281);
            this.rtbOldConf.TabIndex = 0;
            this.rtbOldConf.Text = "";
            // 
            // lblNewConf
            // 
            this.lblNewConf.AutoSize = true;
            this.lblNewConf.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNewConf.Location = new System.Drawing.Point(3, 10);
            this.lblNewConf.Name = "lblNewConf";
            this.lblNewConf.Size = new System.Drawing.Size(197, 15);
            this.lblNewConf.TabIndex = 2;
            this.lblNewConf.Text = "更新しようとしている構成情報";
            // 
            // rtbNewConf
            // 
            this.rtbNewConf.BackColor = System.Drawing.SystemColors.Window;
            this.rtbNewConf.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.rtbNewConf.Location = new System.Drawing.Point(3, 46);
            this.rtbNewConf.Name = "rtbNewConf";
            this.rtbNewConf.ReadOnly = true;
            this.rtbNewConf.Size = new System.Drawing.Size(314, 281);
            this.rtbNewConf.TabIndex = 1;
            this.rtbNewConf.Tag = "New";
            this.rtbNewConf.Text = "";
            // 
            // VersionInfoDialogDiff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(667, 403);
            this.Controls.Add(this.spConDiff);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnInsert);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Name = "VersionInfoDialogDiff";
            this.Text = "環境将軍R バージョン情報差分";
            this.spConDiff.Panel1.ResumeLayout(false);
            this.spConDiff.Panel1.PerformLayout();
            this.spConDiff.Panel2.ResumeLayout(false);
            this.spConDiff.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spConDiff)).EndInit();
            this.spConDiff.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.SplitContainer spConDiff;
        private System.Windows.Forms.Label lblOldConf;
        private System.Windows.Forms.RichTextBox rtbOldConf;
        private System.Windows.Forms.Label lblNewConf;
        private System.Windows.Forms.RichTextBox rtbNewConf;
    }
}