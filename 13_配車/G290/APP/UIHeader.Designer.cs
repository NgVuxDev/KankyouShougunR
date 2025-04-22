namespace Shougun.Core.Allocation.TeikiHaisyaJisekiIchiran.APP
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
            this.HIDUKE_TO = new r_framework.CustomControl.CustomDateTimePicker();
            this.lab_HidukeNyuuryoku = new System.Windows.Forms.Label();
            this.radbtnDenpyouHiduke = new r_framework.CustomControl.CustomRadioButton();
            this.radbtnNyuuryokuHiduke = new r_framework.CustomControl.CustomRadioButton();
            this.txtNum_HidukeSentaku = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.KYOTEN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.KYOTEN_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.readDataNumber = new r_framework.CustomControl.CustomTextBox();
            this.HIDUKE_FROM = new r_framework.CustomControl.CustomDateTimePicker();
            this.label38 = new System.Windows.Forms.Label();
            this.alertNumber = new r_framework.CustomControl.CustomNumericTextBox2();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.customPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.TabIndex = 0;
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(0, 6);
            this.lb_title.Size = new System.Drawing.Size(250, 34);
            this.lb_title.TabIndex = 1;
            this.lb_title.Text = "定期配車実績一覧";
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
            this.HIDUKE_TO.DisplayItemName = "終了日";
            this.HIDUKE_TO.DisplayPopUp = null;
            this.HIDUKE_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_TO.FocusOutCheckMethod")));
            this.HIDUKE_TO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HIDUKE_TO.ForeColor = System.Drawing.Color.Black;
            this.HIDUKE_TO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.HIDUKE_TO.IsInputErrorOccured = false;
            this.HIDUKE_TO.Location = new System.Drawing.Point(839, 24);
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
            this.HIDUKE_TO.TabIndex = 12;
            this.HIDUKE_TO.Tag = "日付を選択してください";
            this.HIDUKE_TO.Text = "2013/12/10(火)";
            this.HIDUKE_TO.Value = new System.DateTime(2013, 12, 10, 0, 0, 0, 0);
            this.HIDUKE_TO.Leave += new System.EventHandler(this.HIDUKE_TO_Leave);
            // 
            // lab_HidukeNyuuryoku
            // 
            this.lab_HidukeNyuuryoku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lab_HidukeNyuuryoku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lab_HidukeNyuuryoku.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lab_HidukeNyuuryoku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lab_HidukeNyuuryoku.ForeColor = System.Drawing.Color.White;
            this.lab_HidukeNyuuryoku.Location = new System.Drawing.Point(564, 24);
            this.lab_HidukeNyuuryoku.Name = "lab_HidukeNyuuryoku";
            this.lab_HidukeNyuuryoku.Size = new System.Drawing.Size(110, 20);
            this.lab_HidukeNyuuryoku.TabIndex = 9;
            this.lab_HidukeNyuuryoku.Text = "作業日";
            this.lab_HidukeNyuuryoku.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radbtnDenpyouHiduke
            // 
            this.radbtnDenpyouHiduke.AutoSize = true;
            this.radbtnDenpyouHiduke.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnDenpyouHiduke.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnDenpyouHiduke.FocusOutCheckMethod")));
            this.radbtnDenpyouHiduke.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtnDenpyouHiduke.LinkedTextBox = "txtNum_HidukeSentaku";
            this.radbtnDenpyouHiduke.Location = new System.Drawing.Point(39, 1);
            this.radbtnDenpyouHiduke.Name = "radbtnDenpyouHiduke";
            this.radbtnDenpyouHiduke.PopupAfterExecute = null;
            this.radbtnDenpyouHiduke.PopupBeforeExecute = null;
            this.radbtnDenpyouHiduke.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnDenpyouHiduke.PopupSearchSendParams")));
            this.radbtnDenpyouHiduke.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnDenpyouHiduke.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnDenpyouHiduke.popupWindowSetting")));
            this.radbtnDenpyouHiduke.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnDenpyouHiduke.RegistCheckMethod")));
            this.radbtnDenpyouHiduke.Size = new System.Drawing.Size(81, 17);
            this.radbtnDenpyouHiduke.TabIndex = 6;
            this.radbtnDenpyouHiduke.Tag = "日付種類が「作業日」の場合にはチェックを付けてください";
            this.radbtnDenpyouHiduke.Text = "1.作業日";
            this.radbtnDenpyouHiduke.UseVisualStyleBackColor = true;
            this.radbtnDenpyouHiduke.Value = "1";
            this.radbtnDenpyouHiduke.CheckedChanged += new System.EventHandler(this.radbtnDenpyouHiduke_CheckedChanged);
            // 
            // radbtnNyuuryokuHiduke
            // 
            this.radbtnNyuuryokuHiduke.AutoSize = true;
            this.radbtnNyuuryokuHiduke.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnNyuuryokuHiduke.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnNyuuryokuHiduke.FocusOutCheckMethod")));
            this.radbtnNyuuryokuHiduke.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtnNyuuryokuHiduke.LinkedTextBox = "txtNum_HidukeSentaku";
            this.radbtnNyuuryokuHiduke.Location = new System.Drawing.Point(139, 1);
            this.radbtnNyuuryokuHiduke.Name = "radbtnNyuuryokuHiduke";
            this.radbtnNyuuryokuHiduke.PopupAfterExecute = null;
            this.radbtnNyuuryokuHiduke.PopupBeforeExecute = null;
            this.radbtnNyuuryokuHiduke.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnNyuuryokuHiduke.PopupSearchSendParams")));
            this.radbtnNyuuryokuHiduke.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnNyuuryokuHiduke.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnNyuuryokuHiduke.popupWindowSetting")));
            this.radbtnNyuuryokuHiduke.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnNyuuryokuHiduke.RegistCheckMethod")));
            this.radbtnNyuuryokuHiduke.Size = new System.Drawing.Size(95, 17);
            this.radbtnNyuuryokuHiduke.TabIndex = 7;
            this.radbtnNyuuryokuHiduke.Tag = "日付種類が「入力日付」の場合にはチェックを付けてください";
            this.radbtnNyuuryokuHiduke.Text = "2.入力日付";
            this.radbtnNyuuryokuHiduke.UseVisualStyleBackColor = true;
            this.radbtnNyuuryokuHiduke.Value = "2";
            this.radbtnNyuuryokuHiduke.CheckedChanged += new System.EventHandler(this.radbtnNyuuryokuHiduke_CheckedChanged);
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
            this.txtNum_HidukeSentaku.Location = new System.Drawing.Point(-1, -1);
            this.txtNum_HidukeSentaku.Name = "txtNum_HidukeSentaku";
            this.txtNum_HidukeSentaku.PopupAfterExecute = null;
            this.txtNum_HidukeSentaku.PopupBeforeExecute = null;
            this.txtNum_HidukeSentaku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtNum_HidukeSentaku.PopupSearchSendParams")));
            this.txtNum_HidukeSentaku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtNum_HidukeSentaku.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtNum_HidukeSentaku.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            2,
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
            this.txtNum_HidukeSentaku.TabIndex = 5;
            this.txtNum_HidukeSentaku.Tag = "【1～2】のいずれかで入力してください";
            this.txtNum_HidukeSentaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNum_HidukeSentaku.WordWrap = false;
            this.txtNum_HidukeSentaku.Validating += new System.ComponentModel.CancelEventHandler(this.txtNum_HidukeSentaku_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(774, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 395;
            this.label2.Text = "～";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(255, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "拠点※";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.KYOTEN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_CD.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_CD.FormatSetting = "カスタム";
            this.KYOTEN_CD.GetCodeMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.IsInputErrorOccured = false;
            this.KYOTEN_CD.ItemDefinedTypes = "smallint";
            this.KYOTEN_CD.Location = new System.Drawing.Point(370, 2);
            this.KYOTEN_CD.Name = "KYOTEN_CD";
            this.KYOTEN_CD.PopupAfterExecute = null;
            this.KYOTEN_CD.PopupBeforeExecute = null;
            this.KYOTEN_CD.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_CD.PopupSearchSendParams")));
            this.KYOTEN_CD.PopupSetFormField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
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
            this.KYOTEN_CD.SetFormField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.Size = new System.Drawing.Size(30, 20);
            this.KYOTEN_CD.TabIndex = 3;
            this.KYOTEN_CD.Tag = "拠点を指定してください（スペースキー押下にて、検索画面を表示します）\n";
            this.KYOTEN_CD.WordWrap = false;
            // 
            // KYOTEN_NAME_RYAKU
            // 
            this.KYOTEN_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KYOTEN_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_NAME_RYAKU.DisplayPopUp = null;
            this.KYOTEN_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.FocusOutCheckMethod")));
            this.KYOTEN_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_NAME_RYAKU.GetCodeMasterField = "";
            this.KYOTEN_NAME_RYAKU.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.KYOTEN_NAME_RYAKU.IsInputErrorOccured = false;
            this.KYOTEN_NAME_RYAKU.ItemDefinedTypes = "";
            this.KYOTEN_NAME_RYAKU.Location = new System.Drawing.Point(399, 2);
            this.KYOTEN_NAME_RYAKU.Name = "KYOTEN_NAME_RYAKU";
            this.KYOTEN_NAME_RYAKU.PopupAfterExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupBeforeExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupGetMasterField = "";
            this.KYOTEN_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.PopupSearchSendParams")));
            this.KYOTEN_NAME_RYAKU.PopupSetFormField = "";
            this.KYOTEN_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOTEN_NAME_RYAKU.PopupWindowName = "";
            this.KYOTEN_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.popupWindowSetting")));
            this.KYOTEN_NAME_RYAKU.ReadOnly = true;
            this.KYOTEN_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.RegistCheckMethod")));
            this.KYOTEN_NAME_RYAKU.SetFormField = "";
            this.KYOTEN_NAME_RYAKU.ShortItemName = "";
            this.KYOTEN_NAME_RYAKU.Size = new System.Drawing.Size(160, 20);
            this.KYOTEN_NAME_RYAKU.TabIndex = 4;
            this.KYOTEN_NAME_RYAKU.TabStop = false;
            this.KYOTEN_NAME_RYAKU.Tag = "";
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
            this.label4.TabIndex = 13;
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
            this.label5.Location = new System.Drawing.Point(983, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 20);
            this.label5.TabIndex = 15;
            this.label5.Text = "読込データ件数";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.readDataNumber.PopupAfterExecute = null;
            this.readDataNumber.PopupBeforeExecute = null;
            this.readDataNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("readDataNumber.PopupSearchSendParams")));
            this.readDataNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.readDataNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("readDataNumber.popupWindowSetting")));
            this.readDataNumber.ReadOnly = true;
            this.readDataNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("readDataNumber.RegistCheckMethod")));
            this.readDataNumber.Size = new System.Drawing.Size(80, 20);
            this.readDataNumber.TabIndex = 16;
            this.readDataNumber.TabStop = false;
            this.readDataNumber.Tag = "検索結果の総件数が表示されます";
            this.readDataNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.HIDUKE_FROM.DisplayItemName = "開始日";
            this.HIDUKE_FROM.DisplayPopUp = null;
            this.HIDUKE_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_FROM.FocusOutCheckMethod")));
            this.HIDUKE_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HIDUKE_FROM.ForeColor = System.Drawing.Color.Black;
            this.HIDUKE_FROM.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.HIDUKE_FROM.IsInputErrorOccured = false;
            this.HIDUKE_FROM.Location = new System.Drawing.Point(679, 24);
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
            this.HIDUKE_FROM.TabIndex = 10;
            this.HIDUKE_FROM.Tag = "日付を選択してください";
            this.HIDUKE_FROM.Text = "2013/12/10(火)";
            this.HIDUKE_FROM.Value = new System.DateTime(2013, 12, 10, 0, 0, 0, 0);
            this.HIDUKE_FROM.Leave += new System.EventHandler(this.HIDUKE_FROM_Leave);
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.Transparent;
            this.label38.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label38.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label38.ForeColor = System.Drawing.Color.Black;
            this.label38.Location = new System.Drawing.Point(818, 24);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(20, 20);
            this.label38.TabIndex = 11;
            this.label38.Text = "～";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.alertNumber.Location = new System.Drawing.Point(1098, 2);
            this.alertNumber.Name = "alertNumber";
            this.alertNumber.PopupAfterExecute = null;
            this.alertNumber.PopupBeforeExecute = null;
            this.alertNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("alertNumber.PopupSearchSendParams")));
            this.alertNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.alertNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("alertNumber.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.alertNumber.RangeSetting = rangeSettingDto3;
            this.alertNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("alertNumber.RegistCheckMethod")));
            this.alertNumber.Size = new System.Drawing.Size(80, 20);
            this.alertNumber.TabIndex = 14;
            this.alertNumber.TabStop = false;
            this.alertNumber.Tag = "検索結果の総件数でアラートメッセージを表示させたい上限数を入力してください";
            this.alertNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.alertNumber.WordWrap = false;
            // 
            // customPanel1
            // 
            this.customPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel1.Controls.Add(this.txtNum_HidukeSentaku);
            this.customPanel1.Controls.Add(this.radbtnNyuuryokuHiduke);
            this.customPanel1.Controls.Add(this.radbtnDenpyouHiduke);
            this.customPanel1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.customPanel1.Location = new System.Drawing.Point(564, 2);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(253, 20);
            this.customPanel1.TabIndex = 8;
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
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(467, 28);
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
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 808;
            this.ISNOT_NEED_DELETE_FLG.TabStop = false;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.customPanel1);
            this.Controls.Add(this.alertNumber);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.HIDUKE_FROM);
            this.Controls.Add(this.readDataNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.KYOTEN_NAME_RYAKU);
            this.Controls.Add(this.KYOTEN_CD);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.HIDUKE_TO);
            this.Controls.Add(this.lab_HidukeNyuuryoku);
            this.Controls.Add(this.label2);
            this.Name = "UIHeader";
            this.Text = "HeaderSample";
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.lab_HidukeNyuuryoku, 0);
            this.Controls.SetChildIndex(this.HIDUKE_TO, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.KYOTEN_CD, 0);
            this.Controls.SetChildIndex(this.KYOTEN_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.readDataNumber, 0);
            this.Controls.SetChildIndex(this.HIDUKE_FROM, 0);
            this.Controls.SetChildIndex(this.label38, 0);
            this.Controls.SetChildIndex(this.alertNumber, 0);
            this.Controls.SetChildIndex(this.customPanel1, 0);
            this.Controls.SetChildIndex(this.ISNOT_NEED_DELETE_FLG, 0);
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomDateTimePicker HIDUKE_TO;
        internal System.Windows.Forms.Label lab_HidukeNyuuryoku;
        public r_framework.CustomControl.CustomRadioButton radbtnDenpyouHiduke;
        public r_framework.CustomControl.CustomRadioButton radbtnNyuuryokuHiduke;
        public r_framework.CustomControl.CustomNumericTextBox2 txtNum_HidukeSentaku;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label5;
        public r_framework.CustomControl.CustomTextBox readDataNumber;
        public r_framework.CustomControl.CustomNumericTextBox2 KYOTEN_CD;
        public r_framework.CustomControl.CustomTextBox KYOTEN_NAME_RYAKU;
        public r_framework.CustomControl.CustomDateTimePicker HIDUKE_FROM;
        private System.Windows.Forms.Label label38;
        public r_framework.CustomControl.CustomNumericTextBox2 alertNumber;
        private r_framework.CustomControl.CustomPanel customPanel1;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;

    }
}