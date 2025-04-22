namespace Shougun.Core.ReceiptPayManagement.NyuuSyutuKinIchiran
{
    partial class HeaderSample
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HeaderSample));
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            this.HIDUKE_TO = new r_framework.CustomControl.CustomDateTimePicker();
            this.HIDUKE_FROM = new r_framework.CustomControl.CustomDateTimePicker();
            this.lab_HidukeNyuuryoku = new System.Windows.Forms.Label();
            this.radbtnDenpyouHiduke = new r_framework.CustomControl.CustomRadioButton();
            this.radbtnNyuuryokuHiduke = new r_framework.CustomControl.CustomRadioButton();
            this.txtNum_HidukeSentaku = new r_framework.CustomControl.CustomNumericTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.KYOTEN_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.BUMON_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.KYOTEN_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.BUMON_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ICHIRAN_ALERT_KENSUU = new r_framework.CustomControl.CustomTextBox();
            this.DATA_KENSUU = new r_framework.CustomControl.CustomTextBox();
            this.SuspendLayout();
            // 
            // lb_title
            // 
            this.lb_title.Size = new System.Drawing.Size(192, 35);
            this.lb_title.Text = "入出金一覧";
            // 
            // HIDUKE_TO
            // 
            this.HIDUKE_TO.BackColor = System.Drawing.SystemColors.Window;
            this.HIDUKE_TO.CustomFormat = "yyyy/MM/dd　ddd";
            this.HIDUKE_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_TO.FocusOutCheckMethod")));
            this.HIDUKE_TO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.HIDUKE_TO.Location = new System.Drawing.Point(813, 20);
            this.HIDUKE_TO.Name = "HIDUKE_TO";
            this.HIDUKE_TO.NullValue = "";
            this.HIDUKE_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIDUKE_TO.PopupSearchSendParams")));
            this.HIDUKE_TO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIDUKE_TO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HIDUKE_TO.popupWindowSetting")));
            this.HIDUKE_TO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_TO.RegistCheckMethod")));
            this.HIDUKE_TO.Size = new System.Drawing.Size(129, 19);
            this.HIDUKE_TO.TabIndex = 394;
            this.HIDUKE_TO.Tag = "日付を選択してください";
            this.HIDUKE_TO.Value = new System.DateTime(2013, 8, 27, 17, 6, 9, 67);
            // 
            // HIDUKE_FROM
            // 
            this.HIDUKE_FROM.BackColor = System.Drawing.SystemColors.Window;
            this.HIDUKE_FROM.CustomFormat = "yyyy/MM/dd　ddd";
            this.HIDUKE_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_FROM.FocusOutCheckMethod")));
            this.HIDUKE_FROM.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.HIDUKE_FROM.Location = new System.Drawing.Point(672, 20);
            this.HIDUKE_FROM.Name = "HIDUKE_FROM";
            this.HIDUKE_FROM.NullValue = "";
            this.HIDUKE_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIDUKE_FROM.PopupSearchSendParams")));
            this.HIDUKE_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIDUKE_FROM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HIDUKE_FROM.popupWindowSetting")));
            this.HIDUKE_FROM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_FROM.RegistCheckMethod")));
            this.HIDUKE_FROM.Size = new System.Drawing.Size(119, 19);
            this.HIDUKE_FROM.TabIndex = 393;
            this.HIDUKE_FROM.Tag = "日付を選択してください";
            this.HIDUKE_FROM.Value = new System.DateTime(2013, 8, 27, 17, 6, 9, 67);
            // 
            // lab_HidukeNyuuryoku
            // 
            this.lab_HidukeNyuuryoku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lab_HidukeNyuuryoku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lab_HidukeNyuuryoku.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lab_HidukeNyuuryoku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lab_HidukeNyuuryoku.ForeColor = System.Drawing.Color.White;
            this.lab_HidukeNyuuryoku.Location = new System.Drawing.Point(588, 20);
            this.lab_HidukeNyuuryoku.Name = "lab_HidukeNyuuryoku";
            this.lab_HidukeNyuuryoku.Size = new System.Drawing.Size(83, 20);
            this.lab_HidukeNyuuryoku.TabIndex = 392;
            this.lab_HidukeNyuuryoku.Text = "伝票日付";
            this.lab_HidukeNyuuryoku.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radbtnDenpyouHiduke
            // 
            this.radbtnDenpyouHiduke.AutoSize = true;
            this.radbtnDenpyouHiduke.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnDenpyouHiduke.FocusOutCheckMethod")));
            this.radbtnDenpyouHiduke.Location = new System.Drawing.Point(616, 3);
            this.radbtnDenpyouHiduke.Name = "radbtnDenpyouHiduke";
            this.radbtnDenpyouHiduke.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnDenpyouHiduke.PopupSearchSendParams")));
            this.radbtnDenpyouHiduke.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnDenpyouHiduke.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnDenpyouHiduke.popupWindowSetting")));
            this.radbtnDenpyouHiduke.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnDenpyouHiduke.RegistCheckMethod")));
            this.radbtnDenpyouHiduke.Size = new System.Drawing.Size(79, 16);
            this.radbtnDenpyouHiduke.TabIndex = 390;
            this.radbtnDenpyouHiduke.Text = "1.伝票日付";
            this.radbtnDenpyouHiduke.UseVisualStyleBackColor = true;
            // 
            // radbtnNyuuryokuHiduke
            // 
            this.radbtnNyuuryokuHiduke.AutoSize = true;
            this.radbtnNyuuryokuHiduke.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnNyuuryokuHiduke.FocusOutCheckMethod")));
            this.radbtnNyuuryokuHiduke.Location = new System.Drawing.Point(704, 3);
            this.radbtnNyuuryokuHiduke.Name = "radbtnNyuuryokuHiduke";
            this.radbtnNyuuryokuHiduke.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnNyuuryokuHiduke.PopupSearchSendParams")));
            this.radbtnNyuuryokuHiduke.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnNyuuryokuHiduke.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnNyuuryokuHiduke.popupWindowSetting")));
            this.radbtnNyuuryokuHiduke.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnNyuuryokuHiduke.RegistCheckMethod")));
            this.radbtnNyuuryokuHiduke.Size = new System.Drawing.Size(79, 16);
            this.radbtnNyuuryokuHiduke.TabIndex = 391;
            this.radbtnNyuuryokuHiduke.Text = "2.入力日付";
            this.radbtnNyuuryokuHiduke.UseVisualStyleBackColor = true;
            this.radbtnNyuuryokuHiduke.CheckedChanged += new System.EventHandler(this.radbtnNyuuryokuHiduke_CheckedChanged);
            // 
            // txtNum_HidukeSentaku
            // 
            this.txtNum_HidukeSentaku.BackColor = System.Drawing.SystemColors.Window;
            this.txtNum_HidukeSentaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNum_HidukeSentaku.CharacterLimitList = new char[0];
            this.txtNum_HidukeSentaku.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_HidukeSentaku.FocusOutCheckMethod")));
            this.txtNum_HidukeSentaku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtNum_HidukeSentaku.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtNum_HidukeSentaku.LinkedRadioButtonArray = new string[0];
            this.txtNum_HidukeSentaku.Location = new System.Drawing.Point(588, 0);
            this.txtNum_HidukeSentaku.MaxLength = 1;
            this.txtNum_HidukeSentaku.MinusEnableFlag = false;
            this.txtNum_HidukeSentaku.Name = "txtNum_HidukeSentaku";
            this.txtNum_HidukeSentaku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtNum_HidukeSentaku.PopupSearchSendParams")));
            this.txtNum_HidukeSentaku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtNum_HidukeSentaku.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtNum_HidukeSentaku.popupWindowSetting")));
            this.txtNum_HidukeSentaku.PrevText = "";
            rangeSettingDto4.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            rangeSettingDto4.Min = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtNum_HidukeSentaku.RangeSetting = rangeSettingDto4;
            this.txtNum_HidukeSentaku.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_HidukeSentaku.RegistCheckMethod")));
            this.txtNum_HidukeSentaku.Size = new System.Drawing.Size(23, 20);
            this.txtNum_HidukeSentaku.TabIndex = 389;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(794, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
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
            this.label1.Location = new System.Drawing.Point(342, -1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 396;
            this.label1.Text = "拠点";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(342, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 20);
            this.label3.TabIndex = 397;
            this.label3.Text = "部門";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KYOTEN_CD
            // 
            this.KYOTEN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.KYOTEN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.FocusOutCheckMethod")));
            this.KYOTEN_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.KYOTEN_CD.Location = new System.Drawing.Point(426, 0);
            this.KYOTEN_CD.MaxLength = 2;
            this.KYOTEN_CD.Name = "KYOTEN_CD";
            this.KYOTEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_CD.PopupSearchSendParams")));
            this.KYOTEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOTEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_CD.popupWindowSetting")));
            this.KYOTEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.RegistCheckMethod")));
            this.KYOTEN_CD.Size = new System.Drawing.Size(55, 19);
            this.KYOTEN_CD.TabIndex = 398;
            // 
            // BUMON_CD
            // 
            this.BUMON_CD.BackColor = System.Drawing.SystemColors.Window;
            this.BUMON_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BUMON_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BUMON_CD.FocusOutCheckMethod")));
            this.BUMON_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.BUMON_CD.Location = new System.Drawing.Point(426, 20);
            this.BUMON_CD.MaxLength = 3;
            this.BUMON_CD.Name = "BUMON_CD";
            this.BUMON_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BUMON_CD.PopupSearchSendParams")));
            this.BUMON_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BUMON_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BUMON_CD.popupWindowSetting")));
            this.BUMON_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BUMON_CD.RegistCheckMethod")));
            this.BUMON_CD.Size = new System.Drawing.Size(55, 19);
            this.BUMON_CD.TabIndex = 399;
            // 
            // KYOTEN_NAME_RYAKU
            // 
            this.KYOTEN_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KYOTEN_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.FocusOutCheckMethod")));
            this.KYOTEN_NAME_RYAKU.Location = new System.Drawing.Point(482, 0);
            this.KYOTEN_NAME_RYAKU.Name = "KYOTEN_NAME_RYAKU";
            this.KYOTEN_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.PopupSearchSendParams")));
            this.KYOTEN_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOTEN_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.popupWindowSetting")));
            this.KYOTEN_NAME_RYAKU.ReadOnly = true;
            this.KYOTEN_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.RegistCheckMethod")));
            this.KYOTEN_NAME_RYAKU.Size = new System.Drawing.Size(100, 19);
            this.KYOTEN_NAME_RYAKU.TabIndex = 400;
            // 
            // BUMON_NAME_RYAKU
            // 
            this.BUMON_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.BUMON_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BUMON_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BUMON_NAME_RYAKU.FocusOutCheckMethod")));
            this.BUMON_NAME_RYAKU.Location = new System.Drawing.Point(482, 20);
            this.BUMON_NAME_RYAKU.Name = "BUMON_NAME_RYAKU";
            this.BUMON_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BUMON_NAME_RYAKU.PopupSearchSendParams")));
            this.BUMON_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BUMON_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BUMON_NAME_RYAKU.popupWindowSetting")));
            this.BUMON_NAME_RYAKU.ReadOnly = true;
            this.BUMON_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BUMON_NAME_RYAKU.RegistCheckMethod")));
            this.BUMON_NAME_RYAKU.Size = new System.Drawing.Size(100, 19);
            this.BUMON_NAME_RYAKU.TabIndex = 401;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(945, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 20);
            this.label4.TabIndex = 403;
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
            this.label5.Location = new System.Drawing.Point(945, -2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 20);
            this.label5.TabIndex = 402;
            this.label5.Text = "読込データ件数";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ICHIRAN_ALERT_KENSUU
            // 
            this.ICHIRAN_ALERT_KENSUU.BackColor = System.Drawing.SystemColors.Window;
            this.ICHIRAN_ALERT_KENSUU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ICHIRAN_ALERT_KENSUU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ICHIRAN_ALERT_KENSUU.FocusOutCheckMethod")));
            this.ICHIRAN_ALERT_KENSUU.Location = new System.Drawing.Point(1062, 20);
            this.ICHIRAN_ALERT_KENSUU.MaxLength = 5;
            this.ICHIRAN_ALERT_KENSUU.Name = "ICHIRAN_ALERT_KENSUU";
            this.ICHIRAN_ALERT_KENSUU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ICHIRAN_ALERT_KENSUU.PopupSearchSendParams")));
            this.ICHIRAN_ALERT_KENSUU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ICHIRAN_ALERT_KENSUU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ICHIRAN_ALERT_KENSUU.popupWindowSetting")));
            this.ICHIRAN_ALERT_KENSUU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ICHIRAN_ALERT_KENSUU.RegistCheckMethod")));
            this.ICHIRAN_ALERT_KENSUU.Size = new System.Drawing.Size(100, 19);
            this.ICHIRAN_ALERT_KENSUU.TabIndex = 405;
            // 
            // DATA_KENSUU
            // 
            this.DATA_KENSUU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.DATA_KENSUU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DATA_KENSUU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATA_KENSUU.FocusOutCheckMethod")));
            this.DATA_KENSUU.Location = new System.Drawing.Point(1062, 0);
            this.DATA_KENSUU.Name = "DATA_KENSUU";
            this.DATA_KENSUU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DATA_KENSUU.PopupSearchSendParams")));
            this.DATA_KENSUU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DATA_KENSUU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DATA_KENSUU.popupWindowSetting")));
            this.DATA_KENSUU.ReadOnly = true;
            this.DATA_KENSUU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATA_KENSUU.RegistCheckMethod")));
            this.DATA_KENSUU.Size = new System.Drawing.Size(100, 19);
            this.DATA_KENSUU.TabIndex = 404;
            // 
            // HeaderSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 40);
            this.Controls.Add(this.ICHIRAN_ALERT_KENSUU);
            this.Controls.Add(this.DATA_KENSUU);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.BUMON_NAME_RYAKU);
            this.Controls.Add(this.KYOTEN_NAME_RYAKU);
            this.Controls.Add(this.BUMON_CD);
            this.Controls.Add(this.KYOTEN_CD);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.HIDUKE_TO);
            this.Controls.Add(this.HIDUKE_FROM);
            this.Controls.Add(this.lab_HidukeNyuuryoku);
            this.Controls.Add(this.radbtnDenpyouHiduke);
            this.Controls.Add(this.radbtnNyuuryokuHiduke);
            this.Controls.Add(this.txtNum_HidukeSentaku);
            this.Controls.Add(this.label2);
            this.Name = "HeaderSample";
            this.Text = "HeaderSample";
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.txtNum_HidukeSentaku, 0);
            this.Controls.SetChildIndex(this.radbtnNyuuryokuHiduke, 0);
            this.Controls.SetChildIndex(this.radbtnDenpyouHiduke, 0);
            this.Controls.SetChildIndex(this.lab_HidukeNyuuryoku, 0);
            this.Controls.SetChildIndex(this.HIDUKE_FROM, 0);
            this.Controls.SetChildIndex(this.HIDUKE_TO, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.KYOTEN_CD, 0);
            this.Controls.SetChildIndex(this.BUMON_CD, 0);
            this.Controls.SetChildIndex(this.KYOTEN_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.BUMON_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.DATA_KENSUU, 0);
            this.Controls.SetChildIndex(this.ICHIRAN_ALERT_KENSUU, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomDateTimePicker HIDUKE_TO;
        public r_framework.CustomControl.CustomDateTimePicker HIDUKE_FROM;
        internal System.Windows.Forms.Label lab_HidukeNyuuryoku;
        public r_framework.CustomControl.CustomRadioButton radbtnDenpyouHiduke;
        public r_framework.CustomControl.CustomRadioButton radbtnNyuuryokuHiduke;
        public r_framework.CustomControl.CustomNumericTextBox txtNum_HidukeSentaku;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label3;
        private r_framework.CustomControl.CustomAlphaNumTextBox KYOTEN_CD;
        private r_framework.CustomControl.CustomAlphaNumTextBox BUMON_CD;
        private r_framework.CustomControl.CustomTextBox KYOTEN_NAME_RYAKU;
        private r_framework.CustomControl.CustomTextBox BUMON_NAME_RYAKU;
        internal System.Windows.Forms.Label label4;
        private r_framework.CustomControl.CustomTextBox ICHIRAN_ALERT_KENSUU;
        private r_framework.CustomControl.CustomTextBox DATA_KENSUU;
        public System.Windows.Forms.Label label5;

    }
}