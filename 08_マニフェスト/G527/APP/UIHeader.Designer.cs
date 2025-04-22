namespace Shougun.Core.PaperManifest.ManifestsuiihyoIchiran
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIHeader));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.alertNumber = new r_framework.CustomControl.CustomNumericTextBox2();
            this.readDataNumber = new r_framework.CustomControl.CustomTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Visible = false;
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(0, 6);
            this.lb_title.Size = new System.Drawing.Size(456, 35);
            this.lb_title.Text = "返送案内書";
            // 
            // alertNumber
            // 
            this.alertNumber.BackColor = System.Drawing.SystemColors.Window;
            this.alertNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.alertNumber.CustomFormatSetting = "#,##0";
            this.alertNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.alertNumber.DisplayPopUp = null;
            this.alertNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("alertNumber.FocusOutCheckMethod")));
            this.alertNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.alertNumber.ForeColor = System.Drawing.Color.Black;
            this.alertNumber.FormatSetting = "カスタム";
            this.alertNumber.IsInputErrorOccured = false;
            this.alertNumber.LinkedRadioButtonArray = new string[0];
            this.alertNumber.Location = new System.Drawing.Point(1098, 2);
            this.alertNumber.MaxLength = 5;
            this.alertNumber.Name = "alertNumber";
            this.alertNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("alertNumber.PopupSearchSendParams")));
            this.alertNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.alertNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("alertNumber.popupWindowSetting")));
            this.alertNumber.PrevText = "";
            rangeSettingDto1.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.alertNumber.RangeSetting = rangeSettingDto1;
            this.alertNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("alertNumber.RegistCheckMethod")));
            this.alertNumber.Size = new System.Drawing.Size(80, 20);
            this.alertNumber.TabIndex = 391;
            this.alertNumber.TabStop = false;
            this.alertNumber.Tag = "検索結果の総件数でアラートメッセージを表示させたい上限数を入力してください";
            this.alertNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.alertNumber.Visible = false;
            // 
            // readDataNumber
            // 
            this.readDataNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.readDataNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.readDataNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.readDataNumber.DisplayPopUp = null;
            this.readDataNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("readDataNumber.FocusOutCheckMethod")));
            this.readDataNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.readDataNumber.ForeColor = System.Drawing.Color.Black;
            this.readDataNumber.IsInputErrorOccured = false;
            this.readDataNumber.Location = new System.Drawing.Point(1098, 24);
            this.readDataNumber.Name = "readDataNumber";
            this.readDataNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("readDataNumber.PopupSearchSendParams")));
            this.readDataNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.readDataNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("readDataNumber.popupWindowSetting")));
            this.readDataNumber.ReadOnly = true;
            this.readDataNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("readDataNumber.RegistCheckMethod")));
            this.readDataNumber.Size = new System.Drawing.Size(80, 20);
            this.readDataNumber.TabIndex = 393;
            this.readDataNumber.TabStop = false;
            this.readDataNumber.Tag = "検索結果の総件数が表示されます";
            this.readDataNumber.Text = "0";
            this.readDataNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.readDataNumber.Visible = false;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(983, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 390;
            this.label4.Text = "アラート件数";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(983, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 20);
            this.label5.TabIndex = 392;
            this.label5.Text = "読込データ件数";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label5.Visible = false;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.alertNumber);
            this.Controls.Add(this.readDataNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Name = "UIHeader";
            this.Text = "HeaderSample";
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.readDataNumber, 0);
            this.Controls.SetChildIndex(this.alertNumber, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomNumericTextBox2 alertNumber;
        public r_framework.CustomControl.CustomTextBox readDataNumber;
        internal System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label5;


    }
}