namespace Shougun.Core.SalesManagement.Shiharaikakuteinyuryoku
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
            this.txtKyotenNameRyaku = new r_framework.CustomControl.CustomTextBox();
            this.txtKyotenCd = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDateFrom = new r_framework.CustomControl.CustomDateTimePicker();
            this.dtpDateTo = new r_framework.CustomControl.CustomDateTimePicker();
            this.lblHidukeNyuuryoku = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtReadDataCnt = new r_framework.CustomControl.CustomTextBox();
            this.txtAlertNumber = new r_framework.CustomControl.CustomNumericTextBox2();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.rdoDenpyouHiduke = new r_framework.CustomControl.CustomRadioButton();
            this.rdoNyuuryokuHiduke = new r_framework.CustomControl.CustomRadioButton();
            this.txtHidukeSentaku = new r_framework.CustomControl.CustomNumericTextBox2();
            this.customPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(0, 6);
            this.lb_title.Size = new System.Drawing.Size(248, 34);
            this.lb_title.Text = "支払確定入力";
            // 
            // txtKyotenNameRyaku
            // 
            this.txtKyotenNameRyaku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtKyotenNameRyaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKyotenNameRyaku.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtKyotenNameRyaku.DisplayPopUp = null;
            this.txtKyotenNameRyaku.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenNameRyaku.FocusOutCheckMethod")));
            this.txtKyotenNameRyaku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtKyotenNameRyaku.ForeColor = System.Drawing.Color.Black;
            this.txtKyotenNameRyaku.GetCodeMasterField = "";
            this.txtKyotenNameRyaku.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtKyotenNameRyaku.IsInputErrorOccured = false;
            this.txtKyotenNameRyaku.ItemDefinedTypes = "";
            this.txtKyotenNameRyaku.Location = new System.Drawing.Point(393, 2);
            this.txtKyotenNameRyaku.Name = "txtKyotenNameRyaku";
            this.txtKyotenNameRyaku.PopupAfterExecute = null;
            this.txtKyotenNameRyaku.PopupBeforeExecute = null;
            this.txtKyotenNameRyaku.PopupGetMasterField = "";
            this.txtKyotenNameRyaku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtKyotenNameRyaku.PopupSearchSendParams")));
            this.txtKyotenNameRyaku.PopupSetFormField = "";
            this.txtKyotenNameRyaku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtKyotenNameRyaku.PopupWindowName = "";
            this.txtKyotenNameRyaku.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtKyotenNameRyaku.popupWindowSetting")));
            this.txtKyotenNameRyaku.ReadOnly = true;
            this.txtKyotenNameRyaku.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenNameRyaku.RegistCheckMethod")));
            this.txtKyotenNameRyaku.SetFormField = "";
            this.txtKyotenNameRyaku.ShortItemName = "";
            this.txtKyotenNameRyaku.Size = new System.Drawing.Size(160, 20);
            this.txtKyotenNameRyaku.TabIndex = 406;
            this.txtKyotenNameRyaku.TabStop = false;
            this.txtKyotenNameRyaku.Tag = "  ";
            // 
            // txtKyotenCd
            // 
            this.txtKyotenCd.BackColor = System.Drawing.SystemColors.Window;
            this.txtKyotenCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKyotenCd.CustomFormatSetting = "00";
            this.txtKyotenCd.DBFieldsName = "KYOTEN_CD";
            this.txtKyotenCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtKyotenCd.DisplayItemName = "拠点CD";
            this.txtKyotenCd.DisplayPopUp = null;
            this.txtKyotenCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenCd.FocusOutCheckMethod")));
            this.txtKyotenCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtKyotenCd.ForeColor = System.Drawing.Color.Black;
            this.txtKyotenCd.FormatSetting = "カスタム";
            this.txtKyotenCd.GetCodeMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.txtKyotenCd.IsInputErrorOccured = false;
            this.txtKyotenCd.ItemDefinedTypes = "smallint";
            this.txtKyotenCd.Location = new System.Drawing.Point(364, 2);
            this.txtKyotenCd.Name = "txtKyotenCd";
            this.txtKyotenCd.PopupAfterExecute = null;
            this.txtKyotenCd.PopupBeforeExecute = null;
            this.txtKyotenCd.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.txtKyotenCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtKyotenCd.PopupSearchSendParams")));
            this.txtKyotenCd.PopupSetFormField = "txtKyotenCd,txtKyotenNameRyaku";
            this.txtKyotenCd.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.txtKyotenCd.PopupWindowName = "マスタ共通ポップアップ";
            this.txtKyotenCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtKyotenCd.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.txtKyotenCd.RangeSetting = rangeSettingDto1;
            this.txtKyotenCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenCd.RegistCheckMethod")));
            this.txtKyotenCd.SetFormField = "txtKyotenCd,txtKyotenNameRyaku";
            this.txtKyotenCd.Size = new System.Drawing.Size(30, 20);
            this.txtKyotenCd.TabIndex = 402;
            this.txtKyotenCd.TabStop = false;
            this.txtKyotenCd.Tag = "拠点を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.txtKyotenCd.WordWrap = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(249, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 404;
            this.label1.Text = "拠点";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.BackColor = System.Drawing.SystemColors.Window;
            this.dtpDateFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtpDateFrom.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.dtpDateFrom.Checked = false;
            this.dtpDateFrom.CustomFormat = "yyyy/MM/dd(ddd)";
            this.dtpDateFrom.DateTimeNowYear = "";
            this.dtpDateFrom.DefaultBackColor = System.Drawing.Color.Empty;
            this.dtpDateFrom.DisplayPopUp = null;
            this.dtpDateFrom.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtpDateFrom.FocusOutCheckMethod")));
            this.dtpDateFrom.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.dtpDateFrom.ForeColor = System.Drawing.Color.Black;
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateFrom.IsInputErrorOccured = false;
            this.dtpDateFrom.Location = new System.Drawing.Point(676, 24);
            this.dtpDateFrom.MaxLength = 10;
            this.dtpDateFrom.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.NullValue = "";
            this.dtpDateFrom.PopupAfterExecute = null;
            this.dtpDateFrom.PopupBeforeExecute = null;
            this.dtpDateFrom.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dtpDateFrom.PopupSearchSendParams")));
            this.dtpDateFrom.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dtpDateFrom.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dtpDateFrom.popupWindowSetting")));
            this.dtpDateFrom.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtpDateFrom.RegistCheckMethod")));
            this.dtpDateFrom.Size = new System.Drawing.Size(138, 20);
            this.dtpDateFrom.TabIndex = 414;
            this.dtpDateFrom.TabStop = false;
            this.dtpDateFrom.Tag = "日付を選択してください";
            this.dtpDateFrom.Text = "2013/11/26(火)";
            this.dtpDateFrom.Value = new System.DateTime(2013, 11, 26, 0, 0, 0, 0);
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.BackColor = System.Drawing.SystemColors.Window;
            this.dtpDateTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtpDateTo.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.dtpDateTo.Checked = false;
            this.dtpDateTo.CustomFormat = "yyyy/MM/dd(ddd)";
            this.dtpDateTo.DateTimeNowYear = "";
            this.dtpDateTo.DefaultBackColor = System.Drawing.Color.Empty;
            this.dtpDateTo.DisplayPopUp = null;
            this.dtpDateTo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtpDateTo.FocusOutCheckMethod")));
            this.dtpDateTo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.dtpDateTo.ForeColor = System.Drawing.Color.Black;
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateTo.IsInputErrorOccured = false;
            this.dtpDateTo.Location = new System.Drawing.Point(835, 24);
            this.dtpDateTo.MaxLength = 10;
            this.dtpDateTo.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.NullValue = "";
            this.dtpDateTo.PopupAfterExecute = null;
            this.dtpDateTo.PopupBeforeExecute = null;
            this.dtpDateTo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dtpDateTo.PopupSearchSendParams")));
            this.dtpDateTo.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dtpDateTo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dtpDateTo.popupWindowSetting")));
            this.dtpDateTo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtpDateTo.RegistCheckMethod")));
            this.dtpDateTo.Size = new System.Drawing.Size(138, 20);
            this.dtpDateTo.TabIndex = 412;
            this.dtpDateTo.TabStop = false;
            this.dtpDateTo.Tag = "日付を選択してください";
            this.dtpDateTo.Text = "2013/11/26(火)";
            this.dtpDateTo.Value = new System.DateTime(2013, 11, 26, 0, 0, 0, 0);
            // 
            // lblHidukeNyuuryoku
            // 
            this.lblHidukeNyuuryoku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblHidukeNyuuryoku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHidukeNyuuryoku.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblHidukeNyuuryoku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblHidukeNyuuryoku.ForeColor = System.Drawing.Color.White;
            this.lblHidukeNyuuryoku.Location = new System.Drawing.Point(561, 24);
            this.lblHidukeNyuuryoku.Name = "lblHidukeNyuuryoku";
            this.lblHidukeNyuuryoku.Size = new System.Drawing.Size(110, 20);
            this.lblHidukeNyuuryoku.TabIndex = 411;
            this.lblHidukeNyuuryoku.Text = "伝票日付";
            this.lblHidukeNyuuryoku.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(815, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 413;
            this.label2.Text = "～";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(981, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 416;
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
            this.label5.Location = new System.Drawing.Point(981, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 20);
            this.label5.TabIndex = 415;
            this.label5.Text = "読込データ件数";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtReadDataCnt
            // 
            this.txtReadDataCnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtReadDataCnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReadDataCnt.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtReadDataCnt.DisplayPopUp = null;
            this.txtReadDataCnt.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtReadDataCnt.FocusOutCheckMethod")));
            this.txtReadDataCnt.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtReadDataCnt.ForeColor = System.Drawing.Color.Black;
            this.txtReadDataCnt.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtReadDataCnt.IsInputErrorOccured = false;
            this.txtReadDataCnt.Location = new System.Drawing.Point(1096, 24);
            this.txtReadDataCnt.Name = "txtReadDataCnt";
            this.txtReadDataCnt.PopupAfterExecute = null;
            this.txtReadDataCnt.PopupBeforeExecute = null;
            this.txtReadDataCnt.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtReadDataCnt.PopupSearchSendParams")));
            this.txtReadDataCnt.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtReadDataCnt.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtReadDataCnt.popupWindowSetting")));
            this.txtReadDataCnt.ReadOnly = true;
            this.txtReadDataCnt.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtReadDataCnt.RegistCheckMethod")));
            this.txtReadDataCnt.Size = new System.Drawing.Size(80, 20);
            this.txtReadDataCnt.TabIndex = 419;
            this.txtReadDataCnt.TabStop = false;
            this.txtReadDataCnt.Tag = "検索結果の総件数が表示されます";
            this.txtReadDataCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtAlertNumber
            // 
            this.txtAlertNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtAlertNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAlertNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtAlertNumber.DisplayPopUp = null;
            this.txtAlertNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtAlertNumber.FocusOutCheckMethod")));
            this.txtAlertNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtAlertNumber.ForeColor = System.Drawing.Color.Black;
            this.txtAlertNumber.IsInputErrorOccured = false;
            this.txtAlertNumber.Location = new System.Drawing.Point(1096, 2);
            this.txtAlertNumber.Name = "txtAlertNumber";
            this.txtAlertNumber.PopupAfterExecute = null;
            this.txtAlertNumber.PopupBeforeExecute = null;
            this.txtAlertNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtAlertNumber.PopupSearchSendParams")));
            this.txtAlertNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtAlertNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtAlertNumber.popupWindowSetting")));
            this.txtAlertNumber.RangeSetting = rangeSettingDto2;
            this.txtAlertNumber.ReadOnly = true;
            this.txtAlertNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtAlertNumber.RegistCheckMethod")));
            this.txtAlertNumber.Size = new System.Drawing.Size(80, 20);
            this.txtAlertNumber.TabIndex = 420;
            this.txtAlertNumber.TabStop = false;
            this.txtAlertNumber.Tag = "";
            this.txtAlertNumber.Visible = false;
            this.txtAlertNumber.WordWrap = false;
            // 
            // customPanel1
            // 
            this.customPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel1.Controls.Add(this.rdoDenpyouHiduke);
            this.customPanel1.Controls.Add(this.rdoNyuuryokuHiduke);
            this.customPanel1.Controls.Add(this.txtHidukeSentaku);
            this.customPanel1.Location = new System.Drawing.Point(561, 2);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(253, 20);
            this.customPanel1.TabIndex = 408;
            // 
            // rdoDenpyouHiduke
            // 
            this.rdoDenpyouHiduke.AutoSize = true;
            this.rdoDenpyouHiduke.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoDenpyouHiduke.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoDenpyouHiduke.FocusOutCheckMethod")));
            this.rdoDenpyouHiduke.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoDenpyouHiduke.LinkedTextBox = "txtHidukeSentaku";
            this.rdoDenpyouHiduke.Location = new System.Drawing.Point(23, 0);
            this.rdoDenpyouHiduke.Name = "rdoDenpyouHiduke";
            this.rdoDenpyouHiduke.PopupAfterExecute = null;
            this.rdoDenpyouHiduke.PopupBeforeExecute = null;
            this.rdoDenpyouHiduke.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoDenpyouHiduke.PopupSearchSendParams")));
            this.rdoDenpyouHiduke.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoDenpyouHiduke.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoDenpyouHiduke.popupWindowSetting")));
            this.rdoDenpyouHiduke.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoDenpyouHiduke.RegistCheckMethod")));
            this.rdoDenpyouHiduke.Size = new System.Drawing.Size(95, 17);
            this.rdoDenpyouHiduke.TabIndex = 412;
            this.rdoDenpyouHiduke.Text = "1.伝票日付";
            this.rdoDenpyouHiduke.UseVisualStyleBackColor = true;
            this.rdoDenpyouHiduke.Value = "1";
            // 
            // rdoNyuuryokuHiduke
            // 
            this.rdoNyuuryokuHiduke.AutoSize = true;
            this.rdoNyuuryokuHiduke.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoNyuuryokuHiduke.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoNyuuryokuHiduke.FocusOutCheckMethod")));
            this.rdoNyuuryokuHiduke.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoNyuuryokuHiduke.LinkedTextBox = "txtHidukeSentaku";
            this.rdoNyuuryokuHiduke.Location = new System.Drawing.Point(124, 0);
            this.rdoNyuuryokuHiduke.Name = "rdoNyuuryokuHiduke";
            this.rdoNyuuryokuHiduke.PopupAfterExecute = null;
            this.rdoNyuuryokuHiduke.PopupBeforeExecute = null;
            this.rdoNyuuryokuHiduke.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoNyuuryokuHiduke.PopupSearchSendParams")));
            this.rdoNyuuryokuHiduke.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoNyuuryokuHiduke.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoNyuuryokuHiduke.popupWindowSetting")));
            this.rdoNyuuryokuHiduke.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoNyuuryokuHiduke.RegistCheckMethod")));
            this.rdoNyuuryokuHiduke.Size = new System.Drawing.Size(95, 17);
            this.rdoNyuuryokuHiduke.TabIndex = 413;
            this.rdoNyuuryokuHiduke.Text = "2.入力日付";
            this.rdoNyuuryokuHiduke.UseVisualStyleBackColor = true;
            this.rdoNyuuryokuHiduke.Value = "2";
            // 
            // txtHidukeSentaku
            // 
            this.txtHidukeSentaku.BackColor = System.Drawing.SystemColors.Window;
            this.txtHidukeSentaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHidukeSentaku.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtHidukeSentaku.DisplayPopUp = null;
            this.txtHidukeSentaku.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtHidukeSentaku.FocusOutCheckMethod")));
            this.txtHidukeSentaku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtHidukeSentaku.ForeColor = System.Drawing.Color.Black;
            this.txtHidukeSentaku.IsInputErrorOccured = false;
            this.txtHidukeSentaku.LinkedRadioButtonArray = new string[] {
        "rdoDenpyouHiduke",
        "rdoNyuuryokuHiduke"};
            this.txtHidukeSentaku.Location = new System.Drawing.Point(-1, -1);
            this.txtHidukeSentaku.Name = "txtHidukeSentaku";
            this.txtHidukeSentaku.PopupAfterExecute = null;
            this.txtHidukeSentaku.PopupBeforeExecute = null;
            this.txtHidukeSentaku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtHidukeSentaku.PopupSearchSendParams")));
            this.txtHidukeSentaku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtHidukeSentaku.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtHidukeSentaku.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto3.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtHidukeSentaku.RangeSetting = rangeSettingDto3;
            this.txtHidukeSentaku.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtHidukeSentaku.RegistCheckMethod")));
            this.txtHidukeSentaku.Size = new System.Drawing.Size(20, 20);
            this.txtHidukeSentaku.TabIndex = 411;
            this.txtHidukeSentaku.TabStop = false;
            this.txtHidukeSentaku.Tag = "日付区分を入力してください";
            this.txtHidukeSentaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtHidukeSentaku.WordWrap = false;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.customPanel1);
            this.Controls.Add(this.txtAlertNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtReadDataCnt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpDateFrom);
            this.Controls.Add(this.dtpDateTo);
            this.Controls.Add(this.lblHidukeNyuuryoku);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtKyotenNameRyaku);
            this.Controls.Add(this.txtKyotenCd);
            this.Controls.Add(this.label1);
            this.Name = "UIHeader";
            this.Text = "HeaderSample";
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.txtKyotenCd, 0);
            this.Controls.SetChildIndex(this.txtKyotenNameRyaku, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.lblHidukeNyuuryoku, 0);
            this.Controls.SetChildIndex(this.dtpDateTo, 0);
            this.Controls.SetChildIndex(this.dtpDateFrom, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.txtReadDataCnt, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.txtAlertNumber, 0);
            this.Controls.SetChildIndex(this.customPanel1, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomTextBox txtKyotenNameRyaku;
        public r_framework.CustomControl.CustomNumericTextBox2 txtKyotenCd;
        internal System.Windows.Forms.Label label1;
        public r_framework.CustomControl.CustomDateTimePicker dtpDateFrom;
        public r_framework.CustomControl.CustomDateTimePicker dtpDateTo;
        internal System.Windows.Forms.Label lblHidukeNyuuryoku;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label5;
        public r_framework.CustomControl.CustomTextBox txtReadDataCnt;
        public r_framework.CustomControl.CustomNumericTextBox2 txtAlertNumber;
        private r_framework.CustomControl.CustomPanel customPanel1;
        public r_framework.CustomControl.CustomRadioButton rdoDenpyouHiduke;
        public r_framework.CustomControl.CustomRadioButton rdoNyuuryokuHiduke;
        public r_framework.CustomControl.CustomNumericTextBox2 txtHidukeSentaku;



    }
}