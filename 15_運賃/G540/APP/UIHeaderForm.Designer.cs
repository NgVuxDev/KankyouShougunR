namespace Shougun.Core.Carriage.UnchinSyuukeihyou
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIHeaderForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.KYOTEN_LABEL = new System.Windows.Forms.Label();
            this.KYOTEN_NAME = new r_framework.CustomControl.CustomTextBox();
            this.ALERT_NUMBER = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.READ_DATA_NUMBER = new r_framework.CustomControl.CustomNumericTextBox2();
            this.CORP_RYAKU_NAME = new r_framework.CustomControl.CustomTextBox();
            this.CORP_LABEL = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Visible = false;
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(69, 6);
            this.lb_title.Size = new System.Drawing.Size(320, 34);
            // 
            // KYOTEN_LABEL
            // 
            this.KYOTEN_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.KYOTEN_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.KYOTEN_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_LABEL.ForeColor = System.Drawing.Color.White;
            this.KYOTEN_LABEL.Location = new System.Drawing.Point(683, 2);
            this.KYOTEN_LABEL.Name = "KYOTEN_LABEL";
            this.KYOTEN_LABEL.Size = new System.Drawing.Size(110, 20);
            this.KYOTEN_LABEL.TabIndex = 517;
            this.KYOTEN_LABEL.Text = "拠点";
            this.KYOTEN_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KYOTEN_NAME
            // 
            this.KYOTEN_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KYOTEN_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_NAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.KYOTEN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_NAME.DisplayItemName = "KYOTEN_NAME_RYAKU";
            this.KYOTEN_NAME.DisplayPopUp = null;
            this.KYOTEN_NAME.ErrorMessage = "";
            this.KYOTEN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME.FocusOutCheckMethod")));
            this.KYOTEN_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_NAME.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_NAME.GetCodeMasterField = "";
            this.KYOTEN_NAME.IsInputErrorOccured = false;
            this.KYOTEN_NAME.ItemDefinedTypes = "varchar";
            this.KYOTEN_NAME.Location = new System.Drawing.Point(798, 2);
            this.KYOTEN_NAME.MaxLength = 0;
            this.KYOTEN_NAME.Name = "KYOTEN_NAME";
            this.KYOTEN_NAME.PopupAfterExecute = null;
            this.KYOTEN_NAME.PopupBeforeExecute = null;
            this.KYOTEN_NAME.PopupGetMasterField = "";
            this.KYOTEN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_NAME.PopupSearchSendParams")));
            this.KYOTEN_NAME.PopupSetFormField = "";
            this.KYOTEN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOTEN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_NAME.popupWindowSetting")));
            this.KYOTEN_NAME.prevText = null;
            this.KYOTEN_NAME.ReadOnly = true;
            this.KYOTEN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME.RegistCheckMethod")));
            this.KYOTEN_NAME.SetFormField = "";
            this.KYOTEN_NAME.Size = new System.Drawing.Size(160, 20);
            this.KYOTEN_NAME.TabIndex = 10;
            this.KYOTEN_NAME.TabStop = false;
            this.KYOTEN_NAME.Tag = "";
            // 
            // ALERT_NUMBER
            // 
            this.ALERT_NUMBER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ALERT_NUMBER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ALERT_NUMBER.CustomFormatSetting = "#,##0";
            this.ALERT_NUMBER.DefaultBackColor = System.Drawing.Color.Empty;
            this.ALERT_NUMBER.DisplayItemName = "アラート件数";
            this.ALERT_NUMBER.DisplayPopUp = null;
            this.ALERT_NUMBER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ALERT_NUMBER.FocusOutCheckMethod")));
            this.ALERT_NUMBER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ALERT_NUMBER.ForeColor = System.Drawing.Color.Black;
            this.ALERT_NUMBER.FormatSetting = "カスタム";
            this.ALERT_NUMBER.IsInputErrorOccured = false;
            this.ALERT_NUMBER.LinkedRadioButtonArray = new string[0];
            this.ALERT_NUMBER.Location = new System.Drawing.Point(1094, 2);
            this.ALERT_NUMBER.Name = "ALERT_NUMBER";
            this.ALERT_NUMBER.PopupAfterExecute = null;
            this.ALERT_NUMBER.PopupBeforeExecute = null;
            this.ALERT_NUMBER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ALERT_NUMBER.PopupSearchSendParams")));
            this.ALERT_NUMBER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ALERT_NUMBER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ALERT_NUMBER.popupWindowSetting")));
            this.ALERT_NUMBER.prevText = null;
            this.ALERT_NUMBER.PrevText = "";
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
            this.ALERT_NUMBER.RangeSetting = rangeSettingDto1;
            this.ALERT_NUMBER.ReadOnly = true;
            this.ALERT_NUMBER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ALERT_NUMBER.RegistCheckMethod")));
            this.ALERT_NUMBER.Size = new System.Drawing.Size(80, 20);
            this.ALERT_NUMBER.TabIndex = 1020;
            this.ALERT_NUMBER.TabStop = false;
            this.ALERT_NUMBER.Tag = "";
            this.ALERT_NUMBER.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(979, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 1019;
            this.label4.Text = "アラート件数";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(979, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 20);
            this.label5.TabIndex = 1021;
            this.label5.Text = "読込データ件数";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // READ_DATA_NUMBER
            // 
            this.READ_DATA_NUMBER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.READ_DATA_NUMBER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.READ_DATA_NUMBER.CustomFormatSetting = "#,##0";
            this.READ_DATA_NUMBER.DefaultBackColor = System.Drawing.Color.Empty;
            this.READ_DATA_NUMBER.DisplayItemName = "アラート件数";
            this.READ_DATA_NUMBER.DisplayPopUp = null;
            this.READ_DATA_NUMBER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("READ_DATA_NUMBER.FocusOutCheckMethod")));
            this.READ_DATA_NUMBER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.READ_DATA_NUMBER.ForeColor = System.Drawing.Color.Black;
            this.READ_DATA_NUMBER.FormatSetting = "カスタム";
            this.READ_DATA_NUMBER.IsInputErrorOccured = false;
            this.READ_DATA_NUMBER.LinkedRadioButtonArray = new string[0];
            this.READ_DATA_NUMBER.Location = new System.Drawing.Point(1094, 24);
            this.READ_DATA_NUMBER.Name = "READ_DATA_NUMBER";
            this.READ_DATA_NUMBER.PopupAfterExecute = null;
            this.READ_DATA_NUMBER.PopupBeforeExecute = null;
            this.READ_DATA_NUMBER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("READ_DATA_NUMBER.PopupSearchSendParams")));
            this.READ_DATA_NUMBER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.READ_DATA_NUMBER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("READ_DATA_NUMBER.popupWindowSetting")));
            this.READ_DATA_NUMBER.prevText = null;
            this.READ_DATA_NUMBER.PrevText = "";
            rangeSettingDto2.Max = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            rangeSettingDto2.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.READ_DATA_NUMBER.RangeSetting = rangeSettingDto2;
            this.READ_DATA_NUMBER.ReadOnly = true;
            this.READ_DATA_NUMBER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("READ_DATA_NUMBER.RegistCheckMethod")));
            this.READ_DATA_NUMBER.Size = new System.Drawing.Size(80, 20);
            this.READ_DATA_NUMBER.TabIndex = 1022;
            this.READ_DATA_NUMBER.TabStop = false;
            this.READ_DATA_NUMBER.Tag = "";
            this.READ_DATA_NUMBER.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CORP_RYAKU_NAME
            // 
            this.CORP_RYAKU_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CORP_RYAKU_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CORP_RYAKU_NAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CORP_RYAKU_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.CORP_RYAKU_NAME.DisplayItemName = "KYOTEN_NAME_RYAKU";
            this.CORP_RYAKU_NAME.DisplayPopUp = null;
            this.CORP_RYAKU_NAME.ErrorMessage = "";
            this.CORP_RYAKU_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CORP_RYAKU_NAME.FocusOutCheckMethod")));
            this.CORP_RYAKU_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CORP_RYAKU_NAME.ForeColor = System.Drawing.Color.Black;
            this.CORP_RYAKU_NAME.GetCodeMasterField = "";
            this.CORP_RYAKU_NAME.IsInputErrorOccured = false;
            this.CORP_RYAKU_NAME.ItemDefinedTypes = "varchar";
            this.CORP_RYAKU_NAME.Location = new System.Drawing.Point(491, 2);
            this.CORP_RYAKU_NAME.MaxLength = 0;
            this.CORP_RYAKU_NAME.Name = "CORP_RYAKU_NAME";
            this.CORP_RYAKU_NAME.PopupAfterExecute = null;
            this.CORP_RYAKU_NAME.PopupBeforeExecute = null;
            this.CORP_RYAKU_NAME.PopupGetMasterField = "";
            this.CORP_RYAKU_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CORP_RYAKU_NAME.PopupSearchSendParams")));
            this.CORP_RYAKU_NAME.PopupSetFormField = "";
            this.CORP_RYAKU_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CORP_RYAKU_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CORP_RYAKU_NAME.popupWindowSetting")));
            this.CORP_RYAKU_NAME.prevText = null;
            this.CORP_RYAKU_NAME.ReadOnly = true;
            this.CORP_RYAKU_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CORP_RYAKU_NAME.RegistCheckMethod")));
            this.CORP_RYAKU_NAME.SetFormField = "";
            this.CORP_RYAKU_NAME.Size = new System.Drawing.Size(160, 20);
            this.CORP_RYAKU_NAME.TabIndex = 1024;
            this.CORP_RYAKU_NAME.TabStop = false;
            this.CORP_RYAKU_NAME.Tag = "";
            // 
            // CORP_LABEL
            // 
            this.CORP_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.CORP_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CORP_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CORP_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CORP_LABEL.ForeColor = System.Drawing.Color.White;
            this.CORP_LABEL.Location = new System.Drawing.Point(376, 2);
            this.CORP_LABEL.Name = "CORP_LABEL";
            this.CORP_LABEL.Size = new System.Drawing.Size(110, 20);
            this.CORP_LABEL.TabIndex = 1025;
            this.CORP_LABEL.Text = "会社";
            this.CORP_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UIHeaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.CORP_RYAKU_NAME);
            this.Controls.Add(this.CORP_LABEL);
            this.Controls.Add(this.READ_DATA_NUMBER);
            this.Controls.Add(this.ALERT_NUMBER);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.KYOTEN_NAME);
            this.Controls.Add(this.KYOTEN_LABEL);
            this.Name = "UIHeaderForm";
            this.Text = "UIHeaderForm";
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.KYOTEN_LABEL, 0);
            this.Controls.SetChildIndex(this.KYOTEN_NAME, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.ALERT_NUMBER, 0);
            this.Controls.SetChildIndex(this.READ_DATA_NUMBER, 0);
            this.Controls.SetChildIndex(this.CORP_LABEL, 0);
            this.Controls.SetChildIndex(this.CORP_RYAKU_NAME, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label KYOTEN_LABEL;
        public r_framework.CustomControl.CustomTextBox KYOTEN_NAME;
        internal r_framework.CustomControl.CustomNumericTextBox2 ALERT_NUMBER;
        internal System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label5;
        internal r_framework.CustomControl.CustomNumericTextBox2 READ_DATA_NUMBER;
        public r_framework.CustomControl.CustomTextBox CORP_RYAKU_NAME;
        public System.Windows.Forms.Label CORP_LABEL;
    }
}