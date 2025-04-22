namespace Shougun.Core.BusinessManagement.TorihikisakiRirekiIchiran.APP
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
            this.HIDUKE_TO = new r_framework.CustomControl.CustomDateTimePicker();
            this.lab_HidukeNyuuryoku = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBox_KyotenNameRyaku = new r_framework.CustomControl.CustomTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ReadDataNumber = new r_framework.CustomControl.CustomTextBox();
            this.HIDUKE_FROM = new r_framework.CustomControl.CustomDateTimePicker();
            this.alertNumber = new r_framework.CustomControl.CustomNumericTextBox2();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.txtBox_KyotenCd = new r_framework.CustomControl.CustomNumericTextBox2();
            this.customPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Location = new System.Drawing.Point(212, 9);
            this.windowTypeLabel.Size = new System.Drawing.Size(12, 18);
            this.windowTypeLabel.TabIndex = 1;
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(0, 0);
            this.lb_title.Size = new System.Drawing.Size(260, 35);
            this.lb_title.TabIndex = 0;
            this.lb_title.Text = "取引履歴一覧";
            // 
            // HIDUKE_TO
            // 
            this.HIDUKE_TO.BackColor = System.Drawing.SystemColors.Window;
            this.HIDUKE_TO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HIDUKE_TO.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.HIDUKE_TO.Checked = false;
            this.HIDUKE_TO.CustomFormat = "yyyy/MM/dd(ddd)";
            this.HIDUKE_TO.DateTimeNowYear = "";
            this.HIDUKE_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.HIDUKE_TO.DisplayPopUp = null;
            this.HIDUKE_TO.FocusOutCheckMethod = null;
            this.HIDUKE_TO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HIDUKE_TO.ForeColor = System.Drawing.Color.Black;
            this.HIDUKE_TO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.HIDUKE_TO.IsInputErrorOccured = false;
            this.HIDUKE_TO.Location = new System.Drawing.Point(543, 22);
            this.HIDUKE_TO.MaxLength = 10;
            this.HIDUKE_TO.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.HIDUKE_TO.Name = "HIDUKE_TO";
            this.HIDUKE_TO.NullValue = "";
            this.HIDUKE_TO.PopupAfterExecute = null;
            this.HIDUKE_TO.PopupBeforeExecute = null;
            this.HIDUKE_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIDUKE_TO.PopupSearchSendParams")));
            this.HIDUKE_TO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIDUKE_TO.popupWindowSetting = null;
            this.HIDUKE_TO.RegistCheckMethod = null;
            this.HIDUKE_TO.Size = new System.Drawing.Size(129, 20);
            this.HIDUKE_TO.TabIndex = 3;
            this.HIDUKE_TO.Tag = "伝票日付を入力してください";
            this.HIDUKE_TO.Text = "2013/12/02(月)";
            this.HIDUKE_TO.Value = new System.DateTime(2013, 12, 2, 0, 0, 0, 0);
            // 
            // lab_HidukeNyuuryoku
            // 
            this.lab_HidukeNyuuryoku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lab_HidukeNyuuryoku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lab_HidukeNyuuryoku.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lab_HidukeNyuuryoku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.lab_HidukeNyuuryoku.ForeColor = System.Drawing.Color.White;
            this.lab_HidukeNyuuryoku.Location = new System.Drawing.Point(305, 22);
            this.lab_HidukeNyuuryoku.Name = "lab_HidukeNyuuryoku";
            this.lab_HidukeNyuuryoku.Size = new System.Drawing.Size(83, 20);
            this.lab_HidukeNyuuryoku.TabIndex = 0;
            this.lab_HidukeNyuuryoku.Text = "伝票日付※";
            this.lab_HidukeNyuuryoku.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(522, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "～";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "拠点";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBox_KyotenNameRyaku
            // 
            this.txtBox_KyotenNameRyaku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtBox_KyotenNameRyaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBox_KyotenNameRyaku.DBFieldsName = "KYOTEN_NAME_RYAKU";
            this.txtBox_KyotenNameRyaku.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtBox_KyotenNameRyaku.DisplayPopUp = null;
            this.txtBox_KyotenNameRyaku.FocusOutCheckMethod = null;
            this.txtBox_KyotenNameRyaku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtBox_KyotenNameRyaku.ForeColor = System.Drawing.Color.Black;
            this.txtBox_KyotenNameRyaku.GetCodeMasterField = "";
            this.txtBox_KyotenNameRyaku.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtBox_KyotenNameRyaku.IsInputErrorOccured = false;
            this.txtBox_KyotenNameRyaku.ItemDefinedTypes = "";
            this.txtBox_KyotenNameRyaku.Location = new System.Drawing.Point(145, 0);
            this.txtBox_KyotenNameRyaku.MaxLength = 0;
            this.txtBox_KyotenNameRyaku.Name = "txtBox_KyotenNameRyaku";
            this.txtBox_KyotenNameRyaku.PopupAfterExecute = null;
            this.txtBox_KyotenNameRyaku.PopupBeforeExecute = null;
            this.txtBox_KyotenNameRyaku.PopupGetMasterField = "";
            this.txtBox_KyotenNameRyaku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtBox_KyotenNameRyaku.PopupSearchSendParams")));
            this.txtBox_KyotenNameRyaku.PopupSetFormField = "";
            this.txtBox_KyotenNameRyaku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtBox_KyotenNameRyaku.PopupWindowName = "";
            this.txtBox_KyotenNameRyaku.popupWindowSetting = null;
            this.txtBox_KyotenNameRyaku.ReadOnly = true;
            this.txtBox_KyotenNameRyaku.RegistCheckMethod = null;
            this.txtBox_KyotenNameRyaku.SetFormField = "";
            this.txtBox_KyotenNameRyaku.ShortItemName = "";
            this.txtBox_KyotenNameRyaku.Size = new System.Drawing.Size(153, 20);
            this.txtBox_KyotenNameRyaku.TabIndex = 8;
            this.txtBox_KyotenNameRyaku.TabStop = false;
            this.txtBox_KyotenNameRyaku.Tag = "";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(683, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "アラート件数";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(683, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "読込データ件数";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ReadDataNumber
            // 
            this.ReadDataNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ReadDataNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReadDataNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.ReadDataNumber.DisplayPopUp = null;
            this.ReadDataNumber.FocusOutCheckMethod = null;
            this.ReadDataNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ReadDataNumber.ForeColor = System.Drawing.Color.Black;
            this.ReadDataNumber.FormatSetting = "";
            this.ReadDataNumber.IsInputErrorOccured = false;
            this.ReadDataNumber.Location = new System.Drawing.Point(803, 22);
            this.ReadDataNumber.Name = "ReadDataNumber";
            this.ReadDataNumber.PopupAfterExecute = null;
            this.ReadDataNumber.PopupBeforeExecute = null;
            this.ReadDataNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ReadDataNumber.PopupSearchSendParams")));
            this.ReadDataNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ReadDataNumber.popupWindowSetting = null;
            this.ReadDataNumber.ReadOnly = true;
            this.ReadDataNumber.RegistCheckMethod = null;
            this.ReadDataNumber.Size = new System.Drawing.Size(100, 20);
            this.ReadDataNumber.TabIndex = 5;
            this.ReadDataNumber.TabStop = false;
            this.ReadDataNumber.Tag = "検索結果の総件数が表示されます";
            this.ReadDataNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // HIDUKE_FROM
            // 
            this.HIDUKE_FROM.BackColor = System.Drawing.SystemColors.Window;
            this.HIDUKE_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HIDUKE_FROM.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.HIDUKE_FROM.Checked = false;
            this.HIDUKE_FROM.CustomFormat = "yyyy/MM/dd(ddd)";
            this.HIDUKE_FROM.DateTimeNowYear = "";
            this.HIDUKE_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.HIDUKE_FROM.DisplayPopUp = null;
            this.HIDUKE_FROM.FocusOutCheckMethod = null;
            this.HIDUKE_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HIDUKE_FROM.ForeColor = System.Drawing.Color.Black;
            this.HIDUKE_FROM.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.HIDUKE_FROM.IsInputErrorOccured = false;
            this.HIDUKE_FROM.Location = new System.Drawing.Point(393, 22);
            this.HIDUKE_FROM.MaxLength = 10;
            this.HIDUKE_FROM.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.HIDUKE_FROM.Name = "HIDUKE_FROM";
            this.HIDUKE_FROM.NullValue = "";
            this.HIDUKE_FROM.PopupAfterExecute = null;
            this.HIDUKE_FROM.PopupBeforeExecute = null;
            this.HIDUKE_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIDUKE_FROM.PopupSearchSendParams")));
            this.HIDUKE_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIDUKE_FROM.popupWindowSetting = null;
            this.HIDUKE_FROM.RegistCheckMethod = null;
            this.HIDUKE_FROM.Size = new System.Drawing.Size(126, 20);
            this.HIDUKE_FROM.TabIndex = 1;
            this.HIDUKE_FROM.Tag = "伝票日付を入力してください";
            this.HIDUKE_FROM.Text = "2013/12/02(月)";
            this.HIDUKE_FROM.Value = new System.DateTime(2013, 12, 2, 0, 0, 0, 0);
            // 
            // alertNumber
            // 
            this.alertNumber.BackColor = System.Drawing.SystemColors.Window;
            this.alertNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.alertNumber.CustomFormatSetting = "#,##0";
            this.alertNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.alertNumber.DisplayPopUp = null;
            this.alertNumber.FocusOutCheckMethod = null;
            this.alertNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.alertNumber.ForeColor = System.Drawing.Color.Black;
            this.alertNumber.FormatSetting = "カスタム";
            this.alertNumber.IsInputErrorOccured = false;
            this.alertNumber.ItemDefinedTypes = "float";
            this.alertNumber.Location = new System.Drawing.Point(803, 0);
            this.alertNumber.Name = "alertNumber";
            this.alertNumber.PopupAfterExecute = null;
            this.alertNumber.PopupBeforeExecute = null;
            this.alertNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("alertNumber.PopupSearchSendParams")));
            this.alertNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.alertNumber.popupWindowSetting = null;
            rangeSettingDto1.Max = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.alertNumber.RangeSetting = rangeSettingDto1;
            this.alertNumber.RegistCheckMethod = null;
            this.alertNumber.Size = new System.Drawing.Size(100, 20);
            this.alertNumber.TabIndex = 10;
            this.alertNumber.Tag = "検索結果の総件数でアラートメッセージを表示させたい上限数を入力してください";
            this.alertNumber.Text = "0";
            this.alertNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.alertNumber.WordWrap = false;
            // 
            // customPanel1
            // 
            this.customPanel1.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.customPanel1.Controls.Add(this.txtBox_KyotenCd);
            this.customPanel1.Controls.Add(this.label1);
            this.customPanel1.Controls.Add(this.txtBox_KyotenNameRyaku);
            this.customPanel1.Controls.Add(this.HIDUKE_TO);
            this.customPanel1.Controls.Add(this.lab_HidukeNyuuryoku);
            this.customPanel1.Controls.Add(this.ReadDataNumber);
            this.customPanel1.Controls.Add(this.label2);
            this.customPanel1.Controls.Add(this.HIDUKE_FROM);
            this.customPanel1.Controls.Add(this.label4);
            this.customPanel1.Controls.Add(this.alertNumber);
            this.customPanel1.Controls.Add(this.label5);
            this.customPanel1.Location = new System.Drawing.Point(266, 2);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(908, 46);
            this.customPanel1.TabIndex = 2;
            this.customPanel1.TabStop = true;
            // 
            // ISNOT_NEED_DELETE_FLG
            // 
            this.ISNOT_NEED_DELETE_FLG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ISNOT_NEED_DELETE_FLG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ISNOT_NEED_DELETE_FLG.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.ISNOT_NEED_DELETE_FLG.DBFieldsName = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.ISNOT_NEED_DELETE_FLG.DisplayPopUp = null;
            this.ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ISNOT_NEED_DELETE_FLG.ForeColor = System.Drawing.Color.Black;
            this.ISNOT_NEED_DELETE_FLG.IsInputErrorOccured = false;
            this.ISNOT_NEED_DELETE_FLG.ItemDefinedTypes = "bit";
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(106, 21);
            this.ISNOT_NEED_DELETE_FLG.MaxLength = 20;
            this.ISNOT_NEED_DELETE_FLG.Name = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.PopupAfterExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupBeforeExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.PopupSearchSendParams")));
            this.ISNOT_NEED_DELETE_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ISNOT_NEED_DELETE_FLG.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.popupWindowSetting")));
            this.ISNOT_NEED_DELETE_FLG.ReadOnly = true;
            this.ISNOT_NEED_DELETE_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.RegistCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.Size = new System.Drawing.Size(40, 20);
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 10028;
            this.ISNOT_NEED_DELETE_FLG.TabStop = false;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // txtBox_KyotenCd
            // 
            this.txtBox_KyotenCd.BackColor = System.Drawing.SystemColors.Window;
            this.txtBox_KyotenCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBox_KyotenCd.CustomFormatSetting = "00";
            this.txtBox_KyotenCd.DBFieldsName = "KYOTEN_CD";
            this.txtBox_KyotenCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtBox_KyotenCd.DisplayItemName = "拠点CD";
            this.txtBox_KyotenCd.DisplayPopUp = null;
            this.txtBox_KyotenCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtBox_KyotenCd.FocusOutCheckMethod")));
            this.txtBox_KyotenCd.ForeColor = System.Drawing.Color.Black;
            this.txtBox_KyotenCd.FormatSetting = "カスタム";
            this.txtBox_KyotenCd.GetCodeMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.txtBox_KyotenCd.IsInputErrorOccured = false;
            this.txtBox_KyotenCd.Location = new System.Drawing.Point(91, 0);
            this.txtBox_KyotenCd.Name = "txtBox_KyotenCd";
            this.txtBox_KyotenCd.PopupAfterExecute = null;
            this.txtBox_KyotenCd.PopupBeforeExecute = null;
            this.txtBox_KyotenCd.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.txtBox_KyotenCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtBox_KyotenCd.PopupSearchSendParams")));
            this.txtBox_KyotenCd.PopupSetFormField = "txtBox_KyotenCd,txtBox_KyotenNameRyaku";
            this.txtBox_KyotenCd.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.txtBox_KyotenCd.PopupWindowName = "マスタ共通ポップアップ";
            this.txtBox_KyotenCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtBox_KyotenCd.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.txtBox_KyotenCd.RangeSetting = rangeSettingDto2;
            this.txtBox_KyotenCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtBox_KyotenCd.RegistCheckMethod")));
            this.txtBox_KyotenCd.SetFormField = "txtBox_KyotenCd,txtBox_KyotenNameRyaku";
            this.txtBox_KyotenCd.Size = new System.Drawing.Size(55, 20);
            this.txtBox_KyotenCd.TabIndex = 7;
            this.txtBox_KyotenCd.Tag = "拠点を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.txtBox_KyotenCd.WordWrap = false;
            // 
            // HeaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 50);
            this.Controls.Add(this.customPanel1);
            this.Name = "HeaderForm";
            this.Text = "HeaderSample";
            this.Controls.SetChildIndex(this.customPanel1, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label lab_HidukeNyuuryoku;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label5;
        public r_framework.CustomControl.CustomTextBox txtBox_KyotenNameRyaku;
        internal r_framework.CustomControl.CustomDateTimePicker HIDUKE_TO;
        internal r_framework.CustomControl.CustomTextBox ReadDataNumber;
        internal r_framework.CustomControl.CustomDateTimePicker HIDUKE_FROM;
        internal r_framework.CustomControl.CustomNumericTextBox2 alertNumber;
        private r_framework.CustomControl.CustomPanel customPanel1;
        internal r_framework.CustomControl.CustomNumericTextBox2 txtBox_KyotenCd;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;

    }
}