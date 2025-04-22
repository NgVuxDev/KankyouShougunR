namespace Shougun.Core.Master.SaishuShobunBasyoPatternIchiran.APP
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
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.lb_alertNumber = new System.Windows.Forms.Label();
            this.lb_readDataNumber = new System.Windows.Forms.Label();
            this.alertNumber = new r_framework.CustomControl.CustomNumericTextBox2();
            this.readDataNumber = new r_framework.CustomControl.CustomNumericTextBox2();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Location = new System.Drawing.Point(319, 8);
            this.windowTypeLabel.TabIndex = 200;
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(0, 6);
            this.lb_title.Size = new System.Drawing.Size(500, 34);
            this.lb_title.TabIndex = 100;
            this.lb_title.Text = "処分場所パターン一覧";
            // 
            // lb_alertNumber
            // 
            this.lb_alertNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lb_alertNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_alertNumber.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lb_alertNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lb_alertNumber.ForeColor = System.Drawing.Color.White;
            this.lb_alertNumber.Location = new System.Drawing.Point(978, 2);
            this.lb_alertNumber.Name = "lb_alertNumber";
            this.lb_alertNumber.Size = new System.Drawing.Size(110, 20);
            this.lb_alertNumber.TabIndex = 700;
            this.lb_alertNumber.Text = "アラート件数";
            this.lb_alertNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_readDataNumber
            // 
            this.lb_readDataNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lb_readDataNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_readDataNumber.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lb_readDataNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lb_readDataNumber.ForeColor = System.Drawing.Color.White;
            this.lb_readDataNumber.Location = new System.Drawing.Point(978, 24);
            this.lb_readDataNumber.Name = "lb_readDataNumber";
            this.lb_readDataNumber.Size = new System.Drawing.Size(110, 20);
            this.lb_readDataNumber.TabIndex = 500;
            this.lb_readDataNumber.Text = "読込データ件数";
            this.lb_readDataNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // alertNumber
            // 
            this.alertNumber.BackColor = System.Drawing.SystemColors.Window;
            this.alertNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.alertNumber.CustomFormatSetting = "#,##0";
            this.alertNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.alertNumber.DisplayItemName = "アラート件数";
            this.alertNumber.DisplayPopUp = null;
            this.alertNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("alertNumber.FocusOutCheckMethod")));
            this.alertNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.alertNumber.ForeColor = System.Drawing.Color.Black;
            this.alertNumber.FormatSetting = "カスタム";
            this.alertNumber.IsInputErrorOccured = false;
            this.alertNumber.ItemDefinedTypes = "long";
            this.alertNumber.LinkedRadioButtonArray = new string[0];
            this.alertNumber.Location = new System.Drawing.Point(1093, 2);
            this.alertNumber.Name = "alertNumber";
            this.alertNumber.PopupAfterExecute = null;
            this.alertNumber.PopupBeforeExecute = null;
            this.alertNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("alertNumber.PopupSearchSendParams")));
            this.alertNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.alertNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("alertNumber.popupWindowSetting")));
            this.alertNumber.prevText = null;
            this.alertNumber.PrevText = null;
            rangeSettingDto1.Max = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.alertNumber.RangeSetting = rangeSettingDto1;
            this.alertNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("alertNumber.RegistCheckMethod")));
            this.alertNumber.Size = new System.Drawing.Size(80, 20);
            this.alertNumber.TabIndex = 2;
            this.alertNumber.Tag = "検索結果の総件数でアラートメッセージを表示させたい上限数を入力してください";
            this.alertNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.alertNumber.WordWrap = false;
            this.alertNumber.Validating += new System.ComponentModel.CancelEventHandler(this.alertNumber_Validating);
            // 
            // readDataNumber
            // 
            this.readDataNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.readDataNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.readDataNumber.CustomFormatSetting = "#,##0";
            this.readDataNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.readDataNumber.DisplayPopUp = null;
            this.readDataNumber.FocusOutCheckMethod = null;
            this.readDataNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.readDataNumber.ForeColor = System.Drawing.Color.Black;
            this.readDataNumber.FormatSetting = "カスタム";
            this.readDataNumber.IsInputErrorOccured = false;
            this.readDataNumber.ItemDefinedTypes = "long";
            this.readDataNumber.LinkedRadioButtonArray = new string[0];
            this.readDataNumber.Location = new System.Drawing.Point(1093, 24);
            this.readDataNumber.Name = "readDataNumber";
            this.readDataNumber.PopupAfterExecute = null;
            this.readDataNumber.PopupBeforeExecute = null;
            this.readDataNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("readDataNumber.PopupSearchSendParams")));
            this.readDataNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.readDataNumber.popupWindowSetting = null;
            this.readDataNumber.prevText = null;
            this.readDataNumber.PrevText = null;
            this.readDataNumber.RangeSetting = rangeSettingDto2;
            this.readDataNumber.ReadOnly = true;
            this.readDataNumber.RegistCheckMethod = null;
            this.readDataNumber.Size = new System.Drawing.Size(80, 20);
            this.readDataNumber.TabIndex = 600;
            this.readDataNumber.Tag = "";
            this.readDataNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.readDataNumber.WordWrap = false;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.readDataNumber);
            this.Controls.Add(this.alertNumber);
            this.Controls.Add(this.lb_alertNumber);
            this.Controls.Add(this.lb_readDataNumber);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Name = "UIHeader";
            this.Text = "";
            this.Controls.SetChildIndex(this.lb_readDataNumber, 0);
            this.Controls.SetChildIndex(this.lb_alertNumber, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.alertNumber, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.readDataNumber, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_alertNumber;
        private System.Windows.Forms.Label lb_readDataNumber;
        internal r_framework.CustomControl.CustomNumericTextBox2 alertNumber;
        public r_framework.CustomControl.CustomNumericTextBox2 readDataNumber;
    }
}