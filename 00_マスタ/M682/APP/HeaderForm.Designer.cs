namespace NyuukinsakiIchiran.APP
{
    partial class HeaderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HeaderForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.ReadDataNumber = new r_framework.CustomControl.CustomNumericTextBox2();
            this.AlertNumber = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ReadDataNumber
            // 
            this.ReadDataNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ReadDataNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReadDataNumber.CustomFormatSetting = "#,##0";
            this.ReadDataNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.ReadDataNumber.DisplayPopUp = null;
            this.ReadDataNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ReadDataNumber.FocusOutCheckMethod")));
            this.ReadDataNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ReadDataNumber.ForeColor = System.Drawing.Color.Black;
            this.ReadDataNumber.FormatSetting = "カスタム";
            this.ReadDataNumber.IsInputErrorOccured = false;
            this.ReadDataNumber.LinkedRadioButtonArray = new string[0];
            this.ReadDataNumber.Location = new System.Drawing.Point(1096, 24);
            this.ReadDataNumber.Name = "ReadDataNumber";
            this.ReadDataNumber.PopupAfterExecute = null;
            this.ReadDataNumber.PopupBeforeExecute = null;
            this.ReadDataNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ReadDataNumber.PopupSearchSendParams")));
            this.ReadDataNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ReadDataNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ReadDataNumber.popupWindowSetting")));
            this.ReadDataNumber.prevText = null;
            this.ReadDataNumber.PrevText = null;
            this.ReadDataNumber.RangeSetting = rangeSettingDto1;
            this.ReadDataNumber.ReadOnly = true;
            this.ReadDataNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ReadDataNumber.RegistCheckMethod")));
            this.ReadDataNumber.Size = new System.Drawing.Size(80, 20);
            this.ReadDataNumber.TabIndex = 536;
            this.ReadDataNumber.TabStop = false;
            this.ReadDataNumber.Tag = "検索結果の総件数が表示されます";
            this.ReadDataNumber.Text = "0";
            this.ReadDataNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ReadDataNumber.WordWrap = false;
            // 
            // AlertNumber
            // 
            this.AlertNumber.BackColor = System.Drawing.SystemColors.Window;
            this.AlertNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AlertNumber.CustomFormatSetting = "#,##0";
            this.AlertNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.AlertNumber.DisplayPopUp = null;
            this.AlertNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("AlertNumber.FocusOutCheckMethod")));
            this.AlertNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.AlertNumber.ForeColor = System.Drawing.Color.Black;
            this.AlertNumber.FormatSetting = "カスタム";
            this.AlertNumber.IsInputErrorOccured = false;
            this.AlertNumber.LinkedRadioButtonArray = new string[0];
            this.AlertNumber.Location = new System.Drawing.Point(1096, 2);
            this.AlertNumber.Name = "AlertNumber";
            this.AlertNumber.PopupAfterExecute = null;
            this.AlertNumber.PopupBeforeExecute = null;
            this.AlertNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("AlertNumber.PopupSearchSendParams")));
            this.AlertNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.AlertNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("AlertNumber.popupWindowSetting")));
            this.AlertNumber.prevText = null;
            this.AlertNumber.PrevText = null;
            rangeSettingDto2.Max = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.AlertNumber.RangeSetting = rangeSettingDto2;
            this.AlertNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("AlertNumber.RegistCheckMethod")));
            this.AlertNumber.Size = new System.Drawing.Size(80, 20);
            this.AlertNumber.TabIndex = 533;
            this.AlertNumber.Tag = "検索結果の総件数でアラートメッセージを表示させたい上限数を入力してください";
            this.AlertNumber.Text = "0";
            this.AlertNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.AlertNumber.WordWrap = false;
            this.AlertNumber.Validated += new System.EventHandler(this.AlertNumber_Validated);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(981, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 535;
            this.label2.Text = "アラート件数";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(981, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 534;
            this.label1.Text = "読込データ件数";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HeaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.ReadDataNumber);
            this.Controls.Add(this.AlertNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "HeaderForm";
            this.Text = "HeaderForm";
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.AlertNumber, 0);
            this.Controls.SetChildIndex(this.ReadDataNumber, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomNumericTextBox2 ReadDataNumber;
        internal r_framework.CustomControl.CustomNumericTextBox2 AlertNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}