namespace Shougun.Core.ReportOutput.CommonChouhyouViewer
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
            this.KYOTEN_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.KYOTEN_LABEL = new System.Windows.Forms.Label();
            this.KYOTEN_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.ALERT_NUMBER = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.READ_DATA_NUMBER = new r_framework.CustomControl.CustomNumericTextBox2();
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
            // KYOTEN_CD
            // 
            this.KYOTEN_CD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KYOTEN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_CD.CharacterLimitList = null;
            this.KYOTEN_CD.DBFieldsName = "KYOTEN_CD";
            this.KYOTEN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_CD.DisplayItemName = "拠点";
            this.KYOTEN_CD.DisplayPopUp = null;
            this.KYOTEN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.FocusOutCheckMethod")));
            this.KYOTEN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_CD.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_CD.GetCodeMasterField = "";
            this.KYOTEN_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.KYOTEN_CD.IsInputErrorOccured = false;
            this.KYOTEN_CD.ItemDefinedTypes = "smallint";
            this.KYOTEN_CD.Location = new System.Drawing.Point(516, 2);
            this.KYOTEN_CD.Name = "KYOTEN_CD";
            this.KYOTEN_CD.PopupAfterExecute = null;
            this.KYOTEN_CD.PopupBeforeExecute = null;
            this.KYOTEN_CD.PopupGetMasterField = "";
            this.KYOTEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_CD.PopupSearchSendParams")));
            this.KYOTEN_CD.PopupSetFormField = "";
            this.KYOTEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.KYOTEN_CD.PopupWindowName = "";
            this.KYOTEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_CD.popupWindowSetting")));
            this.KYOTEN_CD.prevText = null;
            this.KYOTEN_CD.ReadOnly = true;
            this.KYOTEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.RegistCheckMethod")));
            this.KYOTEN_CD.SetFormField = "";
            this.KYOTEN_CD.Size = new System.Drawing.Size(30, 20);
            this.KYOTEN_CD.TabIndex = 0;
            this.KYOTEN_CD.TabStop = false;
            this.KYOTEN_CD.Tag = "";
            this.KYOTEN_CD.ZeroPaddengFlag = true;
            // 
            // KYOTEN_LABEL
            // 
            this.KYOTEN_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.KYOTEN_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.KYOTEN_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_LABEL.ForeColor = System.Drawing.Color.White;
            this.KYOTEN_LABEL.Location = new System.Drawing.Point(401, 2);
            this.KYOTEN_LABEL.Name = "KYOTEN_LABEL";
            this.KYOTEN_LABEL.Size = new System.Drawing.Size(110, 20);
            this.KYOTEN_LABEL.TabIndex = 517;
            this.KYOTEN_LABEL.Text = "拠点";
            this.KYOTEN_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KYOTEN_NAME_RYAKU
            // 
            this.KYOTEN_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KYOTEN_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.KYOTEN_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_NAME_RYAKU.DisplayItemName = "KYOTEN_NAME_RYAKU";
            this.KYOTEN_NAME_RYAKU.DisplayPopUp = null;
            this.KYOTEN_NAME_RYAKU.ErrorMessage = "";
            this.KYOTEN_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.FocusOutCheckMethod")));
            this.KYOTEN_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_NAME_RYAKU.GetCodeMasterField = "";
            this.KYOTEN_NAME_RYAKU.IsInputErrorOccured = false;
            this.KYOTEN_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.KYOTEN_NAME_RYAKU.Location = new System.Drawing.Point(545, 2);
            this.KYOTEN_NAME_RYAKU.MaxLength = 0;
            this.KYOTEN_NAME_RYAKU.Name = "KYOTEN_NAME_RYAKU";
            this.KYOTEN_NAME_RYAKU.PopupAfterExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupBeforeExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupGetMasterField = "";
            this.KYOTEN_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.PopupSearchSendParams")));
            this.KYOTEN_NAME_RYAKU.PopupSetFormField = "";
            this.KYOTEN_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOTEN_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.popupWindowSetting")));
            this.KYOTEN_NAME_RYAKU.prevText = null;
            this.KYOTEN_NAME_RYAKU.ReadOnly = true;
            this.KYOTEN_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.RegistCheckMethod")));
            this.KYOTEN_NAME_RYAKU.SetFormField = "";
            this.KYOTEN_NAME_RYAKU.Size = new System.Drawing.Size(160, 20);
            this.KYOTEN_NAME_RYAKU.TabIndex = 10;
            this.KYOTEN_NAME_RYAKU.TabStop = false;
            this.KYOTEN_NAME_RYAKU.Tag = "";
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
            this.ALERT_NUMBER.Location = new System.Drawing.Point(1093, 2);
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
            this.READ_DATA_NUMBER.FormatSetting = "カスタム";;
            this.READ_DATA_NUMBER.IsInputErrorOccured = false;
            this.READ_DATA_NUMBER.LinkedRadioButtonArray = new string[0];
            this.READ_DATA_NUMBER.Location = new System.Drawing.Point(1093, 24);
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
            // UIHeaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.READ_DATA_NUMBER);
            this.Controls.Add(this.ALERT_NUMBER);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.KYOTEN_NAME_RYAKU);
            this.Controls.Add(this.KYOTEN_CD);
            this.Controls.Add(this.KYOTEN_LABEL);
            this.Name = "UIHeaderForm";
            this.Text = "UIHeaderForm";
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.KYOTEN_LABEL, 0);
            this.Controls.SetChildIndex(this.KYOTEN_CD, 0);
            this.Controls.SetChildIndex(this.KYOTEN_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.ALERT_NUMBER, 0);
            this.Controls.SetChildIndex(this.READ_DATA_NUMBER, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomAlphaNumTextBox KYOTEN_CD;
        public System.Windows.Forms.Label KYOTEN_LABEL;
        public r_framework.CustomControl.CustomTextBox KYOTEN_NAME_RYAKU;
        internal r_framework.CustomControl.CustomNumericTextBox2 ALERT_NUMBER;
        internal System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label5;
        internal r_framework.CustomControl.CustomNumericTextBox2 READ_DATA_NUMBER;
    }
}