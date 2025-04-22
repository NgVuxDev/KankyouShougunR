namespace Shougun.Printing.Common.UI
{
    partial class ReportSettingsDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.reportNamesListBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.printerSettingButton = new System.Windows.Forms.Button();
            this.settingsDescriptionLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.marginEditButton = new System.Windows.Forms.Button();
            this.marginLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.printerNamesListBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.defualtPrinterNameLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.storeButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.settingClearButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "印刷設定";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // reportNamesListBox
            // 
            this.reportNamesListBox.Font = new System.Drawing.Font("ＭＳ ゴシック", 10F);
            this.reportNamesListBox.FormattingEnabled = true;
            this.reportNamesListBox.Location = new System.Drawing.Point(12, 107);
            this.reportNamesListBox.Name = "reportNamesListBox";
            this.reportNamesListBox.Size = new System.Drawing.Size(241, 277);
            this.reportNamesListBox.TabIndex = 1;
            this.reportNamesListBox.SelectedIndexChanged += new System.EventHandler(this.reportNameListBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(12, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(241, 22);
            this.label2.TabIndex = 0;
            this.label2.Text = "帳票名";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // printerSettingButton
            // 
            this.printerSettingButton.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.printerSettingButton.Location = new System.Drawing.Point(345, 16);
            this.printerSettingButton.Name = "printerSettingButton";
            this.printerSettingButton.Size = new System.Drawing.Size(75, 23);
            this.printerSettingButton.TabIndex = 3;
            this.printerSettingButton.Text = "設定";
            this.printerSettingButton.UseVisualStyleBackColor = true;
            this.printerSettingButton.Click += new System.EventHandler(this.printerSettingButton_Click);
            // 
            // settingsDescriptionLabel
            // 
            this.settingsDescriptionLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 10F);
            this.settingsDescriptionLabel.Location = new System.Drawing.Point(7, 54);
            this.settingsDescriptionLabel.Name = "settingsDescriptionLabel";
            this.settingsDescriptionLabel.Size = new System.Drawing.Size(413, 124);
            this.settingsDescriptionLabel.TabIndex = 0;
            this.settingsDescriptionLabel.Text = "（ここに設定内容の概要が表示されます）";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.marginEditButton);
            this.groupBox1.Controls.Add(this.marginLabel);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.printerNamesListBox);
            this.groupBox1.Controls.Add(this.settingsDescriptionLabel);
            this.groupBox1.Controls.Add(this.printerSettingButton);
            this.groupBox1.Location = new System.Drawing.Point(263, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(436, 218);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // marginEditButton
            // 
            this.marginEditButton.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.marginEditButton.Location = new System.Drawing.Point(345, 186);
            this.marginEditButton.Name = "marginEditButton";
            this.marginEditButton.Size = new System.Drawing.Size(75, 23);
            this.marginEditButton.TabIndex = 4;
            this.marginEditButton.Text = "変更";
            this.marginEditButton.UseVisualStyleBackColor = true;
            this.marginEditButton.Click += new System.EventHandler(this.marginEditButton_Click);
            // 
            // marginLabel
            // 
            this.marginLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.marginLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.marginLabel.Location = new System.Drawing.Point(112, 186);
            this.marginLabel.Name = "marginLabel";
            this.marginLabel.Size = new System.Drawing.Size(227, 23);
            this.marginLabel.TabIndex = 0;
            this.marginLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(6, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 22);
            this.label4.TabIndex = 0;
            this.label4.Text = "余白調整(mm)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // printerNamesListBox
            // 
            this.printerNamesListBox.CausesValidation = false;
            this.printerNamesListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.printerNamesListBox.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.printerNamesListBox.Location = new System.Drawing.Point(6, 18);
            this.printerNamesListBox.Name = "printerNamesListBox";
            this.printerNamesListBox.Size = new System.Drawing.Size(333, 21);
            this.printerNamesListBox.TabIndex = 2;
            this.printerNamesListBox.SelectedIndexChanged += new System.EventHandler(this.printerNamesListBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(260, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 22);
            this.label3.TabIndex = 0;
            this.label3.Text = "通常使うプリンタ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // defualtPrinterNameLabel
            // 
            this.defualtPrinterNameLabel.AutoSize = true;
            this.defualtPrinterNameLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 10F);
            this.defualtPrinterNameLabel.Location = new System.Drawing.Point(405, 24);
            this.defualtPrinterNameLabel.Name = "defualtPrinterNameLabel";
            this.defualtPrinterNameLabel.Size = new System.Drawing.Size(203, 14);
            this.defualtPrinterNameLabel.TabIndex = 0;
            this.defualtPrinterNameLabel.Text = "（通常使うプリンタ名を表示）";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(263, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(377, 22);
            this.label5.TabIndex = 0;
            this.label5.Text = "印刷設定";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // storeButton
            // 
            this.storeButton.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.storeButton.Location = new System.Drawing.Point(497, 346);
            this.storeButton.Name = "storeButton";
            this.storeButton.Size = new System.Drawing.Size(88, 38);
            this.storeButton.TabIndex = 0;
            this.storeButton.TabStop = false;
            this.storeButton.Text = "[F9]  \r\n保存";
            this.storeButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.storeButton.UseVisualStyleBackColor = true;
            this.storeButton.Click += new System.EventHandler(this.storeButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.closeButton.Location = new System.Drawing.Point(611, 346);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(88, 38);
            this.closeButton.TabIndex = 0;
            this.closeButton.TabStop = false;
            this.closeButton.Text = "[F12]\r\n終了";
            this.closeButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // settingClearButton
            // 
            this.settingClearButton.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.settingClearButton.Location = new System.Drawing.Point(269, 346);
            this.settingClearButton.Name = "settingClearButton";
            this.settingClearButton.Size = new System.Drawing.Size(88, 38);
            this.settingClearButton.TabIndex = 3;
            this.settingClearButton.TabStop = false;
            this.settingClearButton.Text = "[F1]  \r\n設定クリア";
            this.settingClearButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.settingClearButton.UseVisualStyleBackColor = true;
            this.settingClearButton.Click += new System.EventHandler(this.settingClearButton_Click);
            // 
            // ReportSettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(711, 401);
            this.Controls.Add(this.settingClearButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.storeButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.defualtPrinterNameLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.reportNamesListBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReportSettingsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "印刷設定";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox reportNamesListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button printerSettingButton;
        private System.Windows.Forms.Label settingsDescriptionLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label defualtPrinterNameLabel;
        private System.Windows.Forms.ComboBox printerNamesListBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button storeButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label marginLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button marginEditButton;
        private System.Windows.Forms.Button settingClearButton;
    }
}