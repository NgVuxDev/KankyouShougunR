namespace Shougun.Core.Billing.SeikyuushoHakkou.APP
{
    partial class HeaderSeikyuushoHakkou
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HeaderSeikyuushoHakkou));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.USER_KYOTEN_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.USER_KYOTEN_NAME = new r_framework.CustomControl.CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DenpyouHidukeFrom = new r_framework.CustomControl.CustomDateTimePicker();
            this.DenpyouHidukeTo = new r_framework.CustomControl.CustomDateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ALART_COUNT = new r_framework.CustomControl.CustomNumericTextBox2();
            this.YOMIKOMI_COUNT = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label2 = new System.Windows.Forms.Label();
            this.ZeiRate_Chk = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(3, 6);
            this.lb_title.Size = new System.Drawing.Size(208, 35);
            // 
            // USER_KYOTEN_CD
            // 
            this.USER_KYOTEN_CD.AlphabetLimitFlag = false;
            this.USER_KYOTEN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.USER_KYOTEN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.USER_KYOTEN_CD.CharacterLimitList = null;
            this.USER_KYOTEN_CD.CharactersNumber = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.USER_KYOTEN_CD.DBFieldsName = "";
            this.USER_KYOTEN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.USER_KYOTEN_CD.DisplayItemName = "";
            this.USER_KYOTEN_CD.DisplayPopUp = null;
            this.USER_KYOTEN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("USER_KYOTEN_CD.FocusOutCheckMethod")));
            this.USER_KYOTEN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.USER_KYOTEN_CD.ForeColor = System.Drawing.Color.Black;
            this.USER_KYOTEN_CD.GetCodeMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.USER_KYOTEN_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.USER_KYOTEN_CD.IsInputErrorOccured = false;
            this.USER_KYOTEN_CD.ItemDefinedTypes = "";
            this.USER_KYOTEN_CD.Location = new System.Drawing.Point(372, 2);
            this.USER_KYOTEN_CD.MaxLength = 2;
            this.USER_KYOTEN_CD.Name = "USER_KYOTEN_CD";
            this.USER_KYOTEN_CD.PopupAfterExecute = null;
            this.USER_KYOTEN_CD.PopupBeforeExecute = null;
            this.USER_KYOTEN_CD.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.USER_KYOTEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("USER_KYOTEN_CD.PopupSearchSendParams")));
            this.USER_KYOTEN_CD.PopupSetFormField = "USER_KYOTEN_CD,USER_KYOTEN_NAME";
            this.USER_KYOTEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.USER_KYOTEN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.USER_KYOTEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("USER_KYOTEN_CD.popupWindowSetting")));
            this.USER_KYOTEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("USER_KYOTEN_CD.RegistCheckMethod")));
            this.USER_KYOTEN_CD.SetFormField = "USER_KYOTEN_CD,USER_KYOTEN_NAME";
            this.USER_KYOTEN_CD.ShortItemName = "";
            this.USER_KYOTEN_CD.Size = new System.Drawing.Size(30, 20);
            this.USER_KYOTEN_CD.TabIndex = 1;
            this.USER_KYOTEN_CD.Tag = "";
            this.USER_KYOTEN_CD.ZeroPaddengFlag = true;
            // 
            // USER_KYOTEN_NAME
            // 
            this.USER_KYOTEN_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.USER_KYOTEN_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.USER_KYOTEN_NAME.CharactersNumber = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.USER_KYOTEN_NAME.DBFieldsName = "";
            this.USER_KYOTEN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.USER_KYOTEN_NAME.DisplayItemName = "検索条件";
            this.USER_KYOTEN_NAME.DisplayPopUp = null;
            this.USER_KYOTEN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("USER_KYOTEN_NAME.FocusOutCheckMethod")));
            this.USER_KYOTEN_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.USER_KYOTEN_NAME.ForeColor = System.Drawing.Color.Black;
            this.USER_KYOTEN_NAME.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.USER_KYOTEN_NAME.IsInputErrorOccured = false;
            this.USER_KYOTEN_NAME.Location = new System.Drawing.Point(401, 2);
            this.USER_KYOTEN_NAME.Name = "USER_KYOTEN_NAME";
            this.USER_KYOTEN_NAME.PopupAfterExecute = null;
            this.USER_KYOTEN_NAME.PopupBeforeExecute = null;
            this.USER_KYOTEN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("USER_KYOTEN_NAME.PopupSearchSendParams")));
            this.USER_KYOTEN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.USER_KYOTEN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("USER_KYOTEN_NAME.popupWindowSetting")));
            this.USER_KYOTEN_NAME.ReadOnly = true;
            this.USER_KYOTEN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("USER_KYOTEN_NAME.RegistCheckMethod")));
            this.USER_KYOTEN_NAME.Size = new System.Drawing.Size(160, 20);
            this.USER_KYOTEN_NAME.TabIndex = 390;
            this.USER_KYOTEN_NAME.TabStop = false;
            this.USER_KYOTEN_NAME.Tag = "";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(257, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 389;
            this.label1.Text = "拠点";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(566, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.TabIndex = 389;
            this.label3.Text = "請求日付";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DenpyouHidukeFrom
            // 
            this.DenpyouHidukeFrom.BackColor = System.Drawing.SystemColors.Window;
            this.DenpyouHidukeFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DenpyouHidukeFrom.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.DenpyouHidukeFrom.Checked = false;
            this.DenpyouHidukeFrom.CustomFormat = "yyyy/MM/dd(ddd)";
            this.DenpyouHidukeFrom.DateTimeNowYear = "";
            this.DenpyouHidukeFrom.DefaultBackColor = System.Drawing.Color.Empty;
            this.DenpyouHidukeFrom.DisplayItemName = "開始日付";
            this.DenpyouHidukeFrom.DisplayPopUp = null;
            this.DenpyouHidukeFrom.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DenpyouHidukeFrom.FocusOutCheckMethod")));
            this.DenpyouHidukeFrom.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.DenpyouHidukeFrom.ForeColor = System.Drawing.Color.Black;
            this.DenpyouHidukeFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DenpyouHidukeFrom.IsInputErrorOccured = false;
            this.DenpyouHidukeFrom.Location = new System.Drawing.Point(681, 24);
            this.DenpyouHidukeFrom.MaxLength = 10;
            this.DenpyouHidukeFrom.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.DenpyouHidukeFrom.Name = "DenpyouHidukeFrom";
            this.DenpyouHidukeFrom.NullValue = "";
            this.DenpyouHidukeFrom.PopupAfterExecute = null;
            this.DenpyouHidukeFrom.PopupBeforeExecute = null;
            this.DenpyouHidukeFrom.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DenpyouHidukeFrom.PopupSearchSendParams")));
            this.DenpyouHidukeFrom.PopupSetFormField = "";
            this.DenpyouHidukeFrom.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DenpyouHidukeFrom.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DenpyouHidukeFrom.popupWindowSetting")));
            this.DenpyouHidukeFrom.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DenpyouHidukeFrom.RegistCheckMethod")));
            this.DenpyouHidukeFrom.SetFormField = "";
            this.DenpyouHidukeFrom.ShortItemName = "伝票日付FROM";
            this.DenpyouHidukeFrom.Size = new System.Drawing.Size(138, 20);
            this.DenpyouHidukeFrom.TabIndex = 3;
            this.DenpyouHidukeFrom.Tag = "日付を選択してください";
            this.DenpyouHidukeFrom.Text = "2013/10/30(水)";
            this.DenpyouHidukeFrom.Value = new System.DateTime(2013, 10, 30, 0, 0, 0, 0);
            this.DenpyouHidukeFrom.Leave += new System.EventHandler(this.DenpyouHidukeFrom_Leave);
            // 
            // DenpyouHidukeTo
            // 
            this.DenpyouHidukeTo.BackColor = System.Drawing.SystemColors.Window;
            this.DenpyouHidukeTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DenpyouHidukeTo.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.DenpyouHidukeTo.Checked = false;
            this.DenpyouHidukeTo.CustomFormat = "yyyy/MM/dd(ddd)";
            this.DenpyouHidukeTo.DateTimeNowYear = "";
            this.DenpyouHidukeTo.DefaultBackColor = System.Drawing.Color.Empty;
            this.DenpyouHidukeTo.DisplayItemName = "終了日付";
            this.DenpyouHidukeTo.DisplayPopUp = null;
            this.DenpyouHidukeTo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DenpyouHidukeTo.FocusOutCheckMethod")));
            this.DenpyouHidukeTo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.DenpyouHidukeTo.ForeColor = System.Drawing.Color.Black;
            this.DenpyouHidukeTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DenpyouHidukeTo.IsInputErrorOccured = false;
            this.DenpyouHidukeTo.Location = new System.Drawing.Point(839, 24);
            this.DenpyouHidukeTo.MaxLength = 10;
            this.DenpyouHidukeTo.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.DenpyouHidukeTo.Name = "DenpyouHidukeTo";
            this.DenpyouHidukeTo.NullValue = "";
            this.DenpyouHidukeTo.PopupAfterExecute = null;
            this.DenpyouHidukeTo.PopupBeforeExecute = null;
            this.DenpyouHidukeTo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DenpyouHidukeTo.PopupSearchSendParams")));
            this.DenpyouHidukeTo.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DenpyouHidukeTo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DenpyouHidukeTo.popupWindowSetting")));
            this.DenpyouHidukeTo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DenpyouHidukeTo.RegistCheckMethod")));
            this.DenpyouHidukeTo.Size = new System.Drawing.Size(138, 20);
            this.DenpyouHidukeTo.TabIndex = 4;
            this.DenpyouHidukeTo.Tag = "日付を選択してください";
            this.DenpyouHidukeTo.Text = "2013/10/30(水)";
            this.DenpyouHidukeTo.Value = new System.DateTime(2013, 10, 30, 0, 0, 0, 0);
            this.DenpyouHidukeTo.Leave += new System.EventHandler(this.DenpyouHidukeTo_Leave);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(982, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 389;
            this.label4.Text = "読込データ件数";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(982, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 20);
            this.label5.TabIndex = 389;
            this.label5.Text = "アラート件数";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label5.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(819, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 13);
            this.label6.TabIndex = 393;
            this.label6.Text = "～";
            // 
            // ALART_COUNT
            // 
            this.ALART_COUNT.BackColor = System.Drawing.SystemColors.Window;
            this.ALART_COUNT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ALART_COUNT.CustomFormatSetting = "#,##0";
            this.ALART_COUNT.DefaultBackColor = System.Drawing.Color.Empty;
            this.ALART_COUNT.DisplayPopUp = null;
            this.ALART_COUNT.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ALART_COUNT.FocusOutCheckMethod")));
            this.ALART_COUNT.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ALART_COUNT.ForeColor = System.Drawing.Color.Black;
            this.ALART_COUNT.FormatSetting = "カスタム";
            this.ALART_COUNT.IsInputErrorOccured = false;
            this.ALART_COUNT.Location = new System.Drawing.Point(1097, 2);
            this.ALART_COUNT.Name = "ALART_COUNT";
            this.ALART_COUNT.PopupAfterExecute = null;
            this.ALART_COUNT.PopupBeforeExecute = null;
            this.ALART_COUNT.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ALART_COUNT.PopupSearchSendParams")));
            this.ALART_COUNT.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ALART_COUNT.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ALART_COUNT.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.ALART_COUNT.RangeSetting = rangeSettingDto1;
            this.ALART_COUNT.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ALART_COUNT.RegistCheckMethod")));
            this.ALART_COUNT.Size = new System.Drawing.Size(80, 20);
            this.ALART_COUNT.TabIndex = 5;
            this.ALART_COUNT.TabStop = false;
            this.ALART_COUNT.Tag = "検索結果の総件数でアラートメッセージを表示させたい上限数を入力してください";
            this.ALART_COUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ALART_COUNT.Visible = false;
            this.ALART_COUNT.WordWrap = false;
            // 
            // YOMIKOMI_COUNT
            // 
            this.YOMIKOMI_COUNT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.YOMIKOMI_COUNT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.YOMIKOMI_COUNT.CustomFormatSetting = "#,##0";
            this.YOMIKOMI_COUNT.DefaultBackColor = System.Drawing.Color.Empty;
            this.YOMIKOMI_COUNT.DisplayPopUp = null;
            this.YOMIKOMI_COUNT.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("YOMIKOMI_COUNT.FocusOutCheckMethod")));
            this.YOMIKOMI_COUNT.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.YOMIKOMI_COUNT.ForeColor = System.Drawing.Color.Black;
            this.YOMIKOMI_COUNT.FormatSetting = "カスタム";
            this.YOMIKOMI_COUNT.IsInputErrorOccured = false;
            this.YOMIKOMI_COUNT.Location = new System.Drawing.Point(1097, 24);
            this.YOMIKOMI_COUNT.Name = "YOMIKOMI_COUNT";
            this.YOMIKOMI_COUNT.PopupAfterExecute = null;
            this.YOMIKOMI_COUNT.PopupBeforeExecute = null;
            this.YOMIKOMI_COUNT.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("YOMIKOMI_COUNT.PopupSearchSendParams")));
            this.YOMIKOMI_COUNT.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.YOMIKOMI_COUNT.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("YOMIKOMI_COUNT.popupWindowSetting")));
            this.YOMIKOMI_COUNT.RangeSetting = rangeSettingDto2;
            this.YOMIKOMI_COUNT.ReadOnly = true;
            this.YOMIKOMI_COUNT.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("YOMIKOMI_COUNT.RegistCheckMethod")));
            this.YOMIKOMI_COUNT.Size = new System.Drawing.Size(80, 20);
            this.YOMIKOMI_COUNT.TabIndex = 395;
            this.YOMIKOMI_COUNT.TabStop = false;
            this.YOMIKOMI_COUNT.Tag = "検索結果の総件数が表示されます";
            this.YOMIKOMI_COUNT.Text = "0";
            this.YOMIKOMI_COUNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.YOMIKOMI_COUNT.WordWrap = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(566, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 399;
            this.label2.Text = "税率表示";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ZeiRate_Chk
            // 
            this.ZeiRate_Chk.AutoSize = true;
            this.ZeiRate_Chk.Location = new System.Drawing.Point(681, 4);
            this.ZeiRate_Chk.Margin = new System.Windows.Forms.Padding(2);
            this.ZeiRate_Chk.Name = "ZeiRate_Chk";
            this.ZeiRate_Chk.Size = new System.Drawing.Size(54, 17);
            this.ZeiRate_Chk.TabIndex = 398;
            this.ZeiRate_Chk.Tag = "適格請求書に税率を印刷する場合は、チェックをしてください";
            this.ZeiRate_Chk.Text = "表示";
            this.ZeiRate_Chk.UseVisualStyleBackColor = true;
            // 
            // HeaderSeikyuushoHakkou
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 46);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ZeiRate_Chk);
            this.Controls.Add(this.YOMIKOMI_COUNT);
            this.Controls.Add(this.ALART_COUNT);
            this.Controls.Add(this.DenpyouHidukeTo);
            this.Controls.Add(this.DenpyouHidukeFrom);
            this.Controls.Add(this.USER_KYOTEN_CD);
            this.Controls.Add(this.USER_KYOTEN_NAME);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Name = "HeaderSeikyuushoHakkou";
            this.Text = "HeaderSeikyuushoHakkou";
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.USER_KYOTEN_NAME, 0);
            this.Controls.SetChildIndex(this.USER_KYOTEN_CD, 0);
            this.Controls.SetChildIndex(this.DenpyouHidukeFrom, 0);
            this.Controls.SetChildIndex(this.DenpyouHidukeTo, 0);
            this.Controls.SetChildIndex(this.ALART_COUNT, 0);
            this.Controls.SetChildIndex(this.YOMIKOMI_COUNT, 0);
            this.Controls.SetChildIndex(this.ZeiRate_Chk, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomAlphaNumTextBox USER_KYOTEN_CD;
        public r_framework.CustomControl.CustomTextBox USER_KYOTEN_NAME;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        public r_framework.CustomControl.CustomDateTimePicker DenpyouHidukeFrom;
        public r_framework.CustomControl.CustomDateTimePicker DenpyouHidukeTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        public r_framework.CustomControl.CustomNumericTextBox2 ALART_COUNT;
        public r_framework.CustomControl.CustomNumericTextBox2 YOMIKOMI_COUNT;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.CheckBox ZeiRate_Chk;
    }
}