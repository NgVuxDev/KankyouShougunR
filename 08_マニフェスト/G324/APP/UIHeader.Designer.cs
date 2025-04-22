namespace Shougun.Core.PaperManifest.HensoSakiAnnaisho
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
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            this.alertNumber = new r_framework.CustomControl.CustomNumericTextBox2();
            this.readDataNumber = new r_framework.CustomControl.CustomTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.KYOTEN_NAME = new r_framework.CustomControl.CustomTextBox();
            this.KYOTEN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label1 = new System.Windows.Forms.Label();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.HAKKOU_KYOTEN_NAME = new r_framework.CustomControl.CustomTextBox();
            this.HAKKOU_KYOTEN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Visible = false;
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(0, 6);
            this.lb_title.Size = new System.Drawing.Size(225, 35);
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
            this.alertNumber.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.alertNumber.ForeColor = System.Drawing.Color.Black;
            this.alertNumber.FormatSetting = "カスタム";
            this.alertNumber.IsInputErrorOccured = false;
            this.alertNumber.Location = new System.Drawing.Point(1098, 2);
            this.alertNumber.Name = "alertNumber";
            this.alertNumber.PopupAfterExecute = null;
            this.alertNumber.PopupBeforeExecute = null;
            this.alertNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("alertNumber.PopupSearchSendParams")));
            this.alertNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.alertNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("alertNumber.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            99999,
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
            this.alertNumber.WordWrap = false;
            // 
            // readDataNumber
            // 
            this.readDataNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.readDataNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.readDataNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.readDataNumber.DisplayPopUp = null;
            this.readDataNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("readDataNumber.FocusOutCheckMethod")));
            this.readDataNumber.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.readDataNumber.ForeColor = System.Drawing.Color.Black;
            this.readDataNumber.IsInputErrorOccured = false;
            this.readDataNumber.Location = new System.Drawing.Point(1098, 24);
            this.readDataNumber.Name = "readDataNumber";
            this.readDataNumber.PopupAfterExecute = null;
            this.readDataNumber.PopupBeforeExecute = null;
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
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(983, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 390;
            this.label4.Text = "アラート件数";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(983, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 20);
            this.label5.TabIndex = 392;
            this.label5.Text = "読込データ件数";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KYOTEN_NAME
            // 
            this.KYOTEN_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KYOTEN_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_NAME.DBFieldsName = "KYOTEN_NAME";
            this.KYOTEN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_NAME.DisplayPopUp = null;
            this.KYOTEN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME.FocusOutCheckMethod")));
            this.KYOTEN_NAME.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.KYOTEN_NAME.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_NAME.IsInputErrorOccured = false;
            this.KYOTEN_NAME.Location = new System.Drawing.Point(405, 4);
            this.KYOTEN_NAME.Name = "KYOTEN_NAME";
            this.KYOTEN_NAME.PopupAfterExecute = null;
            this.KYOTEN_NAME.PopupBeforeExecute = null;
            this.KYOTEN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_NAME.PopupSearchSendParams")));
            this.KYOTEN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOTEN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_NAME.popupWindowSetting")));
            this.KYOTEN_NAME.ReadOnly = true;
            this.KYOTEN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME.RegistCheckMethod")));
            this.KYOTEN_NAME.Size = new System.Drawing.Size(160, 20);
            this.KYOTEN_NAME.TabIndex = 10011;
            this.KYOTEN_NAME.TabStop = false;
            // 
            // KYOTEN_CD
            // 
            this.KYOTEN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.KYOTEN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_CD.CustomFormatSetting = "00";
            this.KYOTEN_CD.DBFieldsName = "KYOTEN_CD";
            this.KYOTEN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_CD.DisplayItemName = "拠点CD";
            this.KYOTEN_CD.DisplayPopUp = null;
            this.KYOTEN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.FocusOutCheckMethod")));
            this.KYOTEN_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.KYOTEN_CD.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_CD.FormatSetting = "カスタム";
            this.KYOTEN_CD.GetCodeMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.IsInputErrorOccured = false;
            this.KYOTEN_CD.ItemDefinedTypes = "varchar";
            this.KYOTEN_CD.Location = new System.Drawing.Point(376, 4);
            this.KYOTEN_CD.Name = "KYOTEN_CD";
            this.KYOTEN_CD.PopupAfterExecute = null;
            this.KYOTEN_CD.PopupBeforeExecute = null;
            this.KYOTEN_CD.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_CD.PopupSearchSendParams")));
            this.KYOTEN_CD.PopupSetFormField = "KYOTEN_CD,KYOTEN_NAME";
            this.KYOTEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.KYOTEN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.KYOTEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_CD.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.KYOTEN_CD.RangeSetting = rangeSettingDto2;
            this.KYOTEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.RegistCheckMethod")));
            this.KYOTEN_CD.SetFormField = "KYOTEN_CD,KYOTEN_NAME";
            this.KYOTEN_CD.ShortItemName = "拠点CD";
            this.KYOTEN_CD.Size = new System.Drawing.Size(30, 20);
            this.KYOTEN_CD.TabIndex = 10009;
            this.KYOTEN_CD.Tag = "半角2桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.KYOTEN_CD.WordWrap = false;
            this.KYOTEN_CD.Validated += new System.EventHandler(this.KYOTEN_CD_Validated);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(261, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 10010;
            this.label1.Text = "拠点※";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ISNOT_NEED_DELETE_FLG
            // 
            this.ISNOT_NEED_DELETE_FLG.BackColor = System.Drawing.SystemColors.Window;
            this.ISNOT_NEED_DELETE_FLG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ISNOT_NEED_DELETE_FLG.DBFieldsName = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.ISNOT_NEED_DELETE_FLG.DisplayPopUp = null;
            this.ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod = null;
            this.ISNOT_NEED_DELETE_FLG.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ISNOT_NEED_DELETE_FLG.ForeColor = System.Drawing.Color.Black;
            this.ISNOT_NEED_DELETE_FLG.IsInputErrorOccured = false;
            this.ISNOT_NEED_DELETE_FLG.ItemDefinedTypes = "bit";
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(613, 2);
            this.ISNOT_NEED_DELETE_FLG.Name = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.PopupAfterExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupBeforeExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.PopupSearchSendParams")));
            this.ISNOT_NEED_DELETE_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ISNOT_NEED_DELETE_FLG.popupWindowSetting = null;
            this.ISNOT_NEED_DELETE_FLG.RegistCheckMethod = null;
            this.ISNOT_NEED_DELETE_FLG.Size = new System.Drawing.Size(20, 20);
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 10012;
            this.ISNOT_NEED_DELETE_FLG.TabStop = false;
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // HAKKOU_KYOTEN_NAME
            // 
            this.HAKKOU_KYOTEN_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.HAKKOU_KYOTEN_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HAKKOU_KYOTEN_NAME.DBFieldsName = "KYOTEN_NAME";
            this.HAKKOU_KYOTEN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.HAKKOU_KYOTEN_NAME.DisplayPopUp = null;
            this.HAKKOU_KYOTEN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAKKOU_KYOTEN_NAME.FocusOutCheckMethod")));
            this.HAKKOU_KYOTEN_NAME.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.HAKKOU_KYOTEN_NAME.ForeColor = System.Drawing.Color.Black;
            this.HAKKOU_KYOTEN_NAME.IsInputErrorOccured = false;
            this.HAKKOU_KYOTEN_NAME.Location = new System.Drawing.Point(405, 26);
            this.HAKKOU_KYOTEN_NAME.Name = "HAKKOU_KYOTEN_NAME";
            this.HAKKOU_KYOTEN_NAME.PopupAfterExecute = null;
            this.HAKKOU_KYOTEN_NAME.PopupBeforeExecute = null;
            this.HAKKOU_KYOTEN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HAKKOU_KYOTEN_NAME.PopupSearchSendParams")));
            this.HAKKOU_KYOTEN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HAKKOU_KYOTEN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HAKKOU_KYOTEN_NAME.popupWindowSetting")));
            this.HAKKOU_KYOTEN_NAME.ReadOnly = true;
            this.HAKKOU_KYOTEN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAKKOU_KYOTEN_NAME.RegistCheckMethod")));
            this.HAKKOU_KYOTEN_NAME.Size = new System.Drawing.Size(160, 20);
            this.HAKKOU_KYOTEN_NAME.TabIndex = 10015;
            this.HAKKOU_KYOTEN_NAME.TabStop = false;
            // 
            // HAKKOU_KYOTEN_CD
            // 
            this.HAKKOU_KYOTEN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.HAKKOU_KYOTEN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HAKKOU_KYOTEN_CD.CustomFormatSetting = "00";
            this.HAKKOU_KYOTEN_CD.DBFieldsName = "KYOTEN_CD";
            this.HAKKOU_KYOTEN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.HAKKOU_KYOTEN_CD.DisplayItemName = "発行拠点";
            this.HAKKOU_KYOTEN_CD.DisplayPopUp = null;
            this.HAKKOU_KYOTEN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAKKOU_KYOTEN_CD.FocusOutCheckMethod")));
            this.HAKKOU_KYOTEN_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.HAKKOU_KYOTEN_CD.ForeColor = System.Drawing.Color.Black;
            this.HAKKOU_KYOTEN_CD.FormatSetting = "カスタム";
            this.HAKKOU_KYOTEN_CD.GetCodeMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.HAKKOU_KYOTEN_CD.IsInputErrorOccured = false;
            this.HAKKOU_KYOTEN_CD.ItemDefinedTypes = "varchar";
            this.HAKKOU_KYOTEN_CD.Location = new System.Drawing.Point(376, 26);
            this.HAKKOU_KYOTEN_CD.Name = "HAKKOU_KYOTEN_CD";
            this.HAKKOU_KYOTEN_CD.PopupAfterExecute = null;
            this.HAKKOU_KYOTEN_CD.PopupBeforeExecute = null;
            this.HAKKOU_KYOTEN_CD.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.HAKKOU_KYOTEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HAKKOU_KYOTEN_CD.PopupSearchSendParams")));
            this.HAKKOU_KYOTEN_CD.PopupSetFormField = "HAKKOU_KYOTEN_CD,HAKKOU_KYOTEN_NAME";
            this.HAKKOU_KYOTEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.HAKKOU_KYOTEN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.HAKKOU_KYOTEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HAKKOU_KYOTEN_CD.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.HAKKOU_KYOTEN_CD.RangeSetting = rangeSettingDto3;
            this.HAKKOU_KYOTEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAKKOU_KYOTEN_CD.RegistCheckMethod")));
            this.HAKKOU_KYOTEN_CD.SetFormField = "HAKKOU_KYOTEN_CD,HAKKOU_KYOTEN_NAME";
            this.HAKKOU_KYOTEN_CD.ShortItemName = "発行拠点";
            this.HAKKOU_KYOTEN_CD.Size = new System.Drawing.Size(30, 20);
            this.HAKKOU_KYOTEN_CD.TabIndex = 10014;
            this.HAKKOU_KYOTEN_CD.Tag = "半角2桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.HAKKOU_KYOTEN_CD.WordWrap = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(261, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 10013;
            this.label2.Text = "発行拠点※";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.HAKKOU_KYOTEN_NAME);
            this.Controls.Add(this.HAKKOU_KYOTEN_CD);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.KYOTEN_NAME);
            this.Controls.Add(this.KYOTEN_CD);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
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
            this.Controls.SetChildIndex(this.ISNOT_NEED_DELETE_FLG, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.KYOTEN_CD, 0);
            this.Controls.SetChildIndex(this.KYOTEN_NAME, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.HAKKOU_KYOTEN_CD, 0);
            this.Controls.SetChildIndex(this.HAKKOU_KYOTEN_NAME, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomNumericTextBox2 alertNumber;
        public r_framework.CustomControl.CustomTextBox readDataNumber;
        internal System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label5;
        internal r_framework.CustomControl.CustomTextBox KYOTEN_NAME;
        internal r_framework.CustomControl.CustomNumericTextBox2 KYOTEN_CD;
        public System.Windows.Forms.Label label1;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
        internal r_framework.CustomControl.CustomTextBox HAKKOU_KYOTEN_NAME;
        internal r_framework.CustomControl.CustomNumericTextBox2 HAKKOU_KYOTEN_CD;
        public System.Windows.Forms.Label label2;
    }
}