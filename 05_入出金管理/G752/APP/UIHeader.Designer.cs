namespace Shougun.Core.ReceiptPayManagement.ShukkinKeshikomi
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
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            this.lbl_Kyoten = new System.Windows.Forms.Label();
            this.txtNum_HidukeSentaku = new r_framework.CustomControl.CustomNumericTextBox2();
            this.radbtnDenpyouHiduke = new r_framework.CustomControl.CustomRadioButton();
            this.radbtnNyuuryokuHiduke = new r_framework.CustomControl.CustomRadioButton();
            this.lbl_DenpyoDate = new System.Windows.Forms.Label();
            this.HIDUKE_FROM = new r_framework.CustomControl.CustomDateTimePicker();
            this.label38 = new System.Windows.Forms.Label();
            this.HIDUKE_TO = new r_framework.CustomControl.CustomDateTimePicker();
            this.lbl_YomikomiDataKensu = new System.Windows.Forms.Label();
            this.lbl_AlertKensu = new System.Windows.Forms.Label();
            this.ReadDataNumber = new r_framework.CustomControl.CustomNumericTextBox2();
            this.alertNumber = new r_framework.CustomControl.CustomNumericTextBox2();
            this.KYOTEN_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.panel1 = new r_framework.CustomControl.CustomPanel();
            this.panel2 = new r_framework.CustomControl.CustomPanel();
            this.radbtnHidukeNasi = new r_framework.CustomControl.CustomRadioButton();
            this.KYOTEN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Visible = false;
            // 
            // lb_title
            // 
            this.lb_title.Size = new System.Drawing.Size(259, 34);
            this.lb_title.Text = "出金消込一覧";
            // 
            // lbl_Kyoten
            // 
            this.lbl_Kyoten.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Kyoten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Kyoten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Kyoten.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Kyoten.ForeColor = System.Drawing.Color.White;
            this.lbl_Kyoten.Location = new System.Drawing.Point(277, 2);
            this.lbl_Kyoten.Name = "lbl_Kyoten";
            this.lbl_Kyoten.Size = new System.Drawing.Size(110, 20);
            this.lbl_Kyoten.TabIndex = 517;
            this.lbl_Kyoten.Text = "拠点";
            this.lbl_Kyoten.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNum_HidukeSentaku
            // 
            this.txtNum_HidukeSentaku.BackColor = System.Drawing.SystemColors.Window;
            this.txtNum_HidukeSentaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNum_HidukeSentaku.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtNum_HidukeSentaku.DisplayPopUp = null;
            this.txtNum_HidukeSentaku.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_HidukeSentaku.FocusOutCheckMethod")));
            this.txtNum_HidukeSentaku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtNum_HidukeSentaku.ForeColor = System.Drawing.Color.Black;
            this.txtNum_HidukeSentaku.IsInputErrorOccured = false;
            this.txtNum_HidukeSentaku.LinkedRadioButtonArray = new string[] {
        "radbtnDenpyouHiduke",
        "radbtnNyuuryokuHiduke"};
            this.txtNum_HidukeSentaku.Location = new System.Drawing.Point(0, 2);
            this.txtNum_HidukeSentaku.Name = "txtNum_HidukeSentaku";
            this.txtNum_HidukeSentaku.PopupAfterExecute = null;
            this.txtNum_HidukeSentaku.PopupBeforeExecute = null;
            this.txtNum_HidukeSentaku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtNum_HidukeSentaku.PopupSearchSendParams")));
            this.txtNum_HidukeSentaku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtNum_HidukeSentaku.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtNum_HidukeSentaku.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            3,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtNum_HidukeSentaku.RangeSetting = rangeSettingDto1;
            this.txtNum_HidukeSentaku.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_HidukeSentaku.RegistCheckMethod")));
            this.txtNum_HidukeSentaku.Size = new System.Drawing.Size(20, 20);
            this.txtNum_HidukeSentaku.TabIndex = 3;
            this.txtNum_HidukeSentaku.TabStop = false;
            this.txtNum_HidukeSentaku.Tag = "【1～3】のいずれかで入力してください";
            this.txtNum_HidukeSentaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNum_HidukeSentaku.WordWrap = false;
            // 
            // radbtnDenpyouHiduke
            // 
            this.radbtnDenpyouHiduke.AutoSize = true;
            this.radbtnDenpyouHiduke.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnDenpyouHiduke.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnDenpyouHiduke.FocusOutCheckMethod")));
            this.radbtnDenpyouHiduke.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtnDenpyouHiduke.LinkedTextBox = "txtNum_HidukeSentaku";
            this.radbtnDenpyouHiduke.Location = new System.Drawing.Point(4, 0);
            this.radbtnDenpyouHiduke.Name = "radbtnDenpyouHiduke";
            this.radbtnDenpyouHiduke.PopupAfterExecute = null;
            this.radbtnDenpyouHiduke.PopupBeforeExecute = null;
            this.radbtnDenpyouHiduke.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnDenpyouHiduke.PopupSearchSendParams")));
            this.radbtnDenpyouHiduke.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnDenpyouHiduke.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnDenpyouHiduke.popupWindowSetting")));
            this.radbtnDenpyouHiduke.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnDenpyouHiduke.RegistCheckMethod")));
            this.radbtnDenpyouHiduke.Size = new System.Drawing.Size(95, 17);
            this.radbtnDenpyouHiduke.TabIndex = 1;
            this.radbtnDenpyouHiduke.Tag = "「1.伝票日付」の場合にはチェックを付けてください";
            this.radbtnDenpyouHiduke.Text = "1.伝票日付";
            this.radbtnDenpyouHiduke.UseVisualStyleBackColor = true;
            this.radbtnDenpyouHiduke.Value = "1";
            // 
            // radbtnNyuuryokuHiduke
            // 
            this.radbtnNyuuryokuHiduke.AutoSize = true;
            this.radbtnNyuuryokuHiduke.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnNyuuryokuHiduke.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnNyuuryokuHiduke.FocusOutCheckMethod")));
            this.radbtnNyuuryokuHiduke.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtnNyuuryokuHiduke.LinkedTextBox = "txtNum_HidukeSentaku";
            this.radbtnNyuuryokuHiduke.Location = new System.Drawing.Point(103, 0);
            this.radbtnNyuuryokuHiduke.Name = "radbtnNyuuryokuHiduke";
            this.radbtnNyuuryokuHiduke.PopupAfterExecute = null;
            this.radbtnNyuuryokuHiduke.PopupBeforeExecute = null;
            this.radbtnNyuuryokuHiduke.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnNyuuryokuHiduke.PopupSearchSendParams")));
            this.radbtnNyuuryokuHiduke.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnNyuuryokuHiduke.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnNyuuryokuHiduke.popupWindowSetting")));
            this.radbtnNyuuryokuHiduke.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnNyuuryokuHiduke.RegistCheckMethod")));
            this.radbtnNyuuryokuHiduke.Size = new System.Drawing.Size(95, 17);
            this.radbtnNyuuryokuHiduke.TabIndex = 2;
            this.radbtnNyuuryokuHiduke.Tag = "「2.精算日付」の場合にはチェックを付けてください";
            this.radbtnNyuuryokuHiduke.Text = "2.精算日付";
            this.radbtnNyuuryokuHiduke.UseVisualStyleBackColor = true;
            this.radbtnNyuuryokuHiduke.Value = "2";
            // 
            // lbl_DenpyoDate
            // 
            this.lbl_DenpyoDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_DenpyoDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_DenpyoDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_DenpyoDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_DenpyoDate.ForeColor = System.Drawing.Color.White;
            this.lbl_DenpyoDate.Location = new System.Drawing.Point(0, 24);
            this.lbl_DenpyoDate.Name = "lbl_DenpyoDate";
            this.lbl_DenpyoDate.Size = new System.Drawing.Size(80, 20);
            this.lbl_DenpyoDate.TabIndex = 528;
            this.lbl_DenpyoDate.Text = "伝票日付";
            this.lbl_DenpyoDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.HIDUKE_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_FROM.FocusOutCheckMethod")));
            this.HIDUKE_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HIDUKE_FROM.ForeColor = System.Drawing.Color.Black;
            this.HIDUKE_FROM.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.HIDUKE_FROM.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.HIDUKE_FROM.IsInputErrorOccured = false;
            this.HIDUKE_FROM.Location = new System.Drawing.Point(85, 24);
            this.HIDUKE_FROM.MaxLength = 10;
            this.HIDUKE_FROM.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.HIDUKE_FROM.Name = "HIDUKE_FROM";
            this.HIDUKE_FROM.NullValue = "";
            this.HIDUKE_FROM.PopupAfterExecute = null;
            this.HIDUKE_FROM.PopupBeforeExecute = null;
            this.HIDUKE_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIDUKE_FROM.PopupSearchSendParams")));
            this.HIDUKE_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIDUKE_FROM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HIDUKE_FROM.popupWindowSetting")));
            this.HIDUKE_FROM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_FROM.RegistCheckMethod")));
            this.HIDUKE_FROM.Size = new System.Drawing.Size(138, 20);
            this.HIDUKE_FROM.TabIndex = 4;
            this.HIDUKE_FROM.TabStop = false;
            this.HIDUKE_FROM.Tag = "日付を選択してください";
            this.HIDUKE_FROM.Text = "2013/12/05(木)";
            this.HIDUKE_FROM.Value = new System.DateTime(2013, 12, 5, 0, 0, 0, 0);
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.Transparent;
            this.label38.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label38.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.label38.ForeColor = System.Drawing.Color.Black;
            this.label38.Location = new System.Drawing.Point(223, 24);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(20, 20);
            this.label38.TabIndex = 530;
            this.label38.Text = "～";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.HIDUKE_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_TO.FocusOutCheckMethod")));
            this.HIDUKE_TO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HIDUKE_TO.ForeColor = System.Drawing.Color.Black;
            this.HIDUKE_TO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.HIDUKE_TO.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.HIDUKE_TO.IsInputErrorOccured = false;
            this.HIDUKE_TO.Location = new System.Drawing.Point(243, 24);
            this.HIDUKE_TO.MaxLength = 10;
            this.HIDUKE_TO.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.HIDUKE_TO.Name = "HIDUKE_TO";
            this.HIDUKE_TO.NullValue = "";
            this.HIDUKE_TO.PopupAfterExecute = null;
            this.HIDUKE_TO.PopupBeforeExecute = null;
            this.HIDUKE_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIDUKE_TO.PopupSearchSendParams")));
            this.HIDUKE_TO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIDUKE_TO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HIDUKE_TO.popupWindowSetting")));
            this.HIDUKE_TO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_TO.RegistCheckMethod")));
            this.HIDUKE_TO.Size = new System.Drawing.Size(138, 20);
            this.HIDUKE_TO.TabIndex = 5;
            this.HIDUKE_TO.TabStop = false;
            this.HIDUKE_TO.Tag = "日付を選択してください";
            this.HIDUKE_TO.Text = "2013/12/05(木)";
            this.HIDUKE_TO.Value = new System.DateTime(2013, 12, 5, 0, 0, 0, 0);
            // 
            // lbl_YomikomiDataKensu
            // 
            this.lbl_YomikomiDataKensu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_YomikomiDataKensu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_YomikomiDataKensu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_YomikomiDataKensu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_YomikomiDataKensu.ForeColor = System.Drawing.Color.White;
            this.lbl_YomikomiDataKensu.Location = new System.Drawing.Point(973, 24);
            this.lbl_YomikomiDataKensu.Name = "lbl_YomikomiDataKensu";
            this.lbl_YomikomiDataKensu.Size = new System.Drawing.Size(110, 20);
            this.lbl_YomikomiDataKensu.TabIndex = 532;
            this.lbl_YomikomiDataKensu.Text = "読込データ件数";
            this.lbl_YomikomiDataKensu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_AlertKensu
            // 
            this.lbl_AlertKensu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_AlertKensu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_AlertKensu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_AlertKensu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_AlertKensu.ForeColor = System.Drawing.Color.White;
            this.lbl_AlertKensu.Location = new System.Drawing.Point(973, 2);
            this.lbl_AlertKensu.Name = "lbl_AlertKensu";
            this.lbl_AlertKensu.Size = new System.Drawing.Size(110, 20);
            this.lbl_AlertKensu.TabIndex = 533;
            this.lbl_AlertKensu.Text = "アラート件数";
            this.lbl_AlertKensu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_AlertKensu.Visible = false;
            // 
            // ReadDataNumber
            // 
            this.ReadDataNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ReadDataNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReadDataNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.ReadDataNumber.DisplayPopUp = null;
            this.ReadDataNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ReadDataNumber.FocusOutCheckMethod")));
            this.ReadDataNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ReadDataNumber.ForeColor = System.Drawing.Color.Black;
            this.ReadDataNumber.IsInputErrorOccured = false;
            this.ReadDataNumber.Location = new System.Drawing.Point(1088, 24);
            this.ReadDataNumber.Name = "ReadDataNumber";
            this.ReadDataNumber.PopupAfterExecute = null;
            this.ReadDataNumber.PopupBeforeExecute = null;
            this.ReadDataNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ReadDataNumber.PopupSearchSendParams")));
            this.ReadDataNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ReadDataNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ReadDataNumber.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ReadDataNumber.RangeSetting = rangeSettingDto2;
            this.ReadDataNumber.ReadOnly = true;
            this.ReadDataNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ReadDataNumber.RegistCheckMethod")));
            this.ReadDataNumber.Size = new System.Drawing.Size(80, 20);
            this.ReadDataNumber.TabIndex = 534;
            this.ReadDataNumber.TabStop = false;
            this.ReadDataNumber.Tag = "検索結果の総件数が表示されます";
            this.ReadDataNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ReadDataNumber.WordWrap = false;
            // 
            // alertNumber
            // 
            this.alertNumber.BackColor = System.Drawing.SystemColors.Window;
            this.alertNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.alertNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.alertNumber.DisplayPopUp = null;
            this.alertNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("alertNumber.FocusOutCheckMethod")));
            this.alertNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.alertNumber.ForeColor = System.Drawing.Color.Black;
            this.alertNumber.IsInputErrorOccured = false;
            this.alertNumber.Location = new System.Drawing.Point(1088, 2);
            this.alertNumber.Name = "alertNumber";
            this.alertNumber.PopupAfterExecute = null;
            this.alertNumber.PopupBeforeExecute = null;
            this.alertNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("alertNumber.PopupSearchSendParams")));
            this.alertNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.alertNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("alertNumber.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.alertNumber.RangeSetting = rangeSettingDto3;
            this.alertNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("alertNumber.RegistCheckMethod")));
            this.alertNumber.Size = new System.Drawing.Size(80, 20);
            this.alertNumber.TabIndex = 6;
            this.alertNumber.TabStop = false;
            this.alertNumber.Tag = "検索結果の総件数でアラートメッセージを表示させたい上限数を入力してください";
            this.alertNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.alertNumber.Visible = false;
            this.alertNumber.WordWrap = false;
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
            this.KYOTEN_NAME_RYAKU.DisplayPopUp = null;
            this.KYOTEN_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.FocusOutCheckMethod")));
            this.KYOTEN_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_NAME_RYAKU.IsInputErrorOccured = false;
            this.KYOTEN_NAME_RYAKU.Location = new System.Drawing.Point(421, 2);
            this.KYOTEN_NAME_RYAKU.MaxLength = 0;
            this.KYOTEN_NAME_RYAKU.Name = "KYOTEN_NAME_RYAKU";
            this.KYOTEN_NAME_RYAKU.PopupAfterExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupBeforeExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.PopupSearchSendParams")));
            this.KYOTEN_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOTEN_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.popupWindowSetting")));
            this.KYOTEN_NAME_RYAKU.ReadOnly = true;
            this.KYOTEN_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.RegistCheckMethod")));
            this.KYOTEN_NAME_RYAKU.Size = new System.Drawing.Size(160, 20);
            this.KYOTEN_NAME_RYAKU.TabIndex = 536;
            this.KYOTEN_NAME_RYAKU.TabStop = false;
            this.KYOTEN_NAME_RYAKU.Tag = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtNum_HidukeSentaku);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.HIDUKE_TO);
            this.panel1.Controls.Add(this.label38);
            this.panel1.Controls.Add(this.lbl_DenpyoDate);
            this.panel1.Controls.Add(this.HIDUKE_FROM);
            this.panel1.Location = new System.Drawing.Point(587, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(381, 46);
            this.panel1.TabIndex = 538;
            this.panel1.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.radbtnHidukeNasi);
            this.panel2.Controls.Add(this.radbtnNyuuryokuHiduke);
            this.panel2.Controls.Add(this.radbtnDenpyouHiduke);
            this.panel2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.panel2.Location = new System.Drawing.Point(19, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(303, 20);
            this.panel2.TabIndex = 531;
            // 
            // radbtnHidukeNasi
            // 
            this.radbtnHidukeNasi.AutoSize = true;
            this.radbtnHidukeNasi.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnHidukeNasi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnHidukeNasi.FocusOutCheckMethod")));
            this.radbtnHidukeNasi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtnHidukeNasi.LinkedTextBox = "txtNum_HidukeSentaku";
            this.radbtnHidukeNasi.Location = new System.Drawing.Point(202, 0);
            this.radbtnHidukeNasi.Name = "radbtnHidukeNasi";
            this.radbtnHidukeNasi.PopupAfterExecute = null;
            this.radbtnHidukeNasi.PopupBeforeExecute = null;
            this.radbtnHidukeNasi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnHidukeNasi.PopupSearchSendParams")));
            this.radbtnHidukeNasi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnHidukeNasi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnHidukeNasi.popupWindowSetting")));
            this.radbtnHidukeNasi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnHidukeNasi.RegistCheckMethod")));
            this.radbtnHidukeNasi.Size = new System.Drawing.Size(95, 17);
            this.radbtnHidukeNasi.TabIndex = 3;
            this.radbtnHidukeNasi.Tag = "「3.日付なし」の場合にはチェックを付けてください";
            this.radbtnHidukeNasi.Text = "3.日付なし";
            this.radbtnHidukeNasi.UseVisualStyleBackColor = true;
            this.radbtnHidukeNasi.Value = "3";
            // 
            // KYOTEN_CD
            // 
            this.KYOTEN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.KYOTEN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_CD.CustomFormatSetting = "00";
            this.KYOTEN_CD.DBFieldsName = "KYOTEN_CD";
            this.KYOTEN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_CD.DisplayPopUp = null;
            this.KYOTEN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.FocusOutCheckMethod")));
            this.KYOTEN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_CD.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_CD.FormatSetting = "カスタム";
            this.KYOTEN_CD.GetCodeMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.IsInputErrorOccured = false;
            this.KYOTEN_CD.ItemDefinedTypes = "varchar";
            this.KYOTEN_CD.Location = new System.Drawing.Point(392, 2);
            this.KYOTEN_CD.Name = "KYOTEN_CD";
            this.KYOTEN_CD.PopupAfterExecute = null;
            this.KYOTEN_CD.PopupBeforeExecute = null;
            this.KYOTEN_CD.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_CD.PopupSearchSendParams")));
            this.KYOTEN_CD.PopupSetFormField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.KYOTEN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.KYOTEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_CD.popupWindowSetting")));
            rangeSettingDto4.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.KYOTEN_CD.RangeSetting = rangeSettingDto4;
            this.KYOTEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.RegistCheckMethod")));
            this.KYOTEN_CD.SetFormField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.Size = new System.Drawing.Size(30, 20);
            this.KYOTEN_CD.TabIndex = 1;
            this.KYOTEN_CD.Tag = "拠点を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.KYOTEN_CD.WordWrap = false;
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
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(517, 24);
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
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 673;
            this.ISNOT_NEED_DELETE_FLG.TabStop = false;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.KYOTEN_CD);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.KYOTEN_NAME_RYAKU);
            this.Controls.Add(this.alertNumber);
            this.Controls.Add(this.ReadDataNumber);
            this.Controls.Add(this.lbl_AlertKensu);
            this.Controls.Add(this.lbl_YomikomiDataKensu);
            this.Controls.Add(this.lbl_Kyoten);
            this.Name = "UIHeader";
            this.Text = "HeaderSample";
            this.Controls.SetChildIndex(this.lbl_Kyoten, 0);
            this.Controls.SetChildIndex(this.lbl_YomikomiDataKensu, 0);
            this.Controls.SetChildIndex(this.lbl_AlertKensu, 0);
            this.Controls.SetChildIndex(this.ReadDataNumber, 0);
            this.Controls.SetChildIndex(this.alertNumber, 0);
            this.Controls.SetChildIndex(this.KYOTEN_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.KYOTEN_CD, 0);
            this.Controls.SetChildIndex(this.ISNOT_NEED_DELETE_FLG, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lbl_Kyoten;
        public System.Windows.Forms.Label lbl_DenpyoDate;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label lbl_YomikomiDataKensu;
        private System.Windows.Forms.Label lbl_AlertKensu;
        public r_framework.CustomControl.CustomTextBox KYOTEN_NAME_RYAKU;
        private r_framework.CustomControl.CustomPanel panel1;
        public r_framework.CustomControl.CustomNumericTextBox2 txtNum_HidukeSentaku;
        public r_framework.CustomControl.CustomDateTimePicker HIDUKE_FROM;
        public r_framework.CustomControl.CustomDateTimePicker HIDUKE_TO;
        public r_framework.CustomControl.CustomRadioButton radbtnDenpyouHiduke;
        public r_framework.CustomControl.CustomRadioButton radbtnNyuuryokuHiduke;
        internal r_framework.CustomControl.CustomNumericTextBox2 KYOTEN_CD;
        private r_framework.CustomControl.CustomPanel panel2;
        internal r_framework.CustomControl.CustomNumericTextBox2 alertNumber;
        internal r_framework.CustomControl.CustomNumericTextBox2 ReadDataNumber;
        public r_framework.CustomControl.CustomRadioButton radbtnHidukeNasi;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
    }
}