namespace Shougun.Core.ExternalConnection.SmsIchiran.APP
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
            this.readDataNumber = new r_framework.CustomControl.CustomTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.alertNumber = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label4 = new System.Windows.Forms.Label();
            this.label136 = new System.Windows.Forms.Label();
            this.panel61 = new System.Windows.Forms.Panel();
            this.rb_SEND_JOKYO_3 = new r_framework.CustomControl.CustomRadioButton();
            this.SEND_JOKYO = new r_framework.CustomControl.CustomNumericTextBox2();
            this.rb_SEND_JOKYO_2 = new r_framework.CustomControl.CustomRadioButton();
            this.rb_SEND_JOKYO_1 = new r_framework.CustomControl.CustomRadioButton();
            this.KYOTEN_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.KYOTEN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.KYOTEN_LABEL = new System.Windows.Forms.Label();
            this.panel61.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_title
            // 
            this.lb_title.Size = new System.Drawing.Size(331, 34);
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
            this.readDataNumber.Location = new System.Drawing.Point(1078, 24);
            this.readDataNumber.Name = "readDataNumber";
            this.readDataNumber.PopupAfterExecute = null;
            this.readDataNumber.PopupBeforeExecute = null;
            this.readDataNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("readDataNumber.PopupSearchSendParams")));
            this.readDataNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.readDataNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("readDataNumber.popupWindowSetting")));
            this.readDataNumber.ReadOnly = true;
            this.readDataNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("readDataNumber.RegistCheckMethod")));
            this.readDataNumber.Size = new System.Drawing.Size(80, 20);
            this.readDataNumber.TabIndex = 1019;
            this.readDataNumber.TabStop = false;
            this.readDataNumber.Tag = "検索結果の総件数が表示されます";
            this.readDataNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(964, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 20);
            this.label5.TabIndex = 1020;
            this.label5.Text = "読込データ件数";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // alertNumber
            // 
            this.alertNumber.BackColor = System.Drawing.SystemColors.Window;
            this.alertNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.alertNumber.CustomFormatSetting = "#,##0";
            this.alertNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.alertNumber.DisplayItemName = "アラート件数";
            this.alertNumber.DisplayPopUp = null;
            this.alertNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("alertNumber.FocusOutCheckMethod")));
            this.alertNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.alertNumber.ForeColor = System.Drawing.Color.Black;
            this.alertNumber.FormatSetting = "カスタム";
            this.alertNumber.IsInputErrorOccured = false;
            this.alertNumber.ItemDefinedTypes = "float";
            this.alertNumber.Location = new System.Drawing.Point(1078, 2);
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
            this.alertNumber.TabIndex = 1021;
            this.alertNumber.TabStop = false;
            this.alertNumber.Tag = "検索結果の総件数でアラートメッセージを表示させたい上限数を入力してください";
            this.alertNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.alertNumber.WordWrap = false;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(964, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 1022;
            this.label4.Text = "アラート件数";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label136
            // 
            this.label136.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label136.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label136.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label136.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label136.ForeColor = System.Drawing.Color.White;
            this.label136.Location = new System.Drawing.Point(424, 24);
            this.label136.Name = "label136";
            this.label136.Size = new System.Drawing.Size(110, 20);
            this.label136.TabIndex = 1027;
            this.label136.Text = "送信状況";
            this.label136.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel61
            // 
            this.panel61.Controls.Add(this.rb_SEND_JOKYO_3);
            this.panel61.Controls.Add(this.SEND_JOKYO);
            this.panel61.Controls.Add(this.rb_SEND_JOKYO_2);
            this.panel61.Controls.Add(this.rb_SEND_JOKYO_1);
            this.panel61.Location = new System.Drawing.Point(539, 24);
            this.panel61.Name = "panel61";
            this.panel61.Size = new System.Drawing.Size(401, 22);
            this.panel61.TabIndex = 1028;
            // 
            // rb_SEND_JOKYO_3
            // 
            this.rb_SEND_JOKYO_3.AutoSize = true;
            this.rb_SEND_JOKYO_3.DefaultBackColor = System.Drawing.Color.Empty;
            this.rb_SEND_JOKYO_3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_SEND_JOKYO_3.FocusOutCheckMethod")));
            this.rb_SEND_JOKYO_3.LinkedTextBox = "SEND_JOKYO";
            this.rb_SEND_JOKYO_3.Location = new System.Drawing.Point(233, 3);
            this.rb_SEND_JOKYO_3.Name = "rb_SEND_JOKYO_3";
            this.rb_SEND_JOKYO_3.PopupAfterExecute = null;
            this.rb_SEND_JOKYO_3.PopupBeforeExecute = null;
            this.rb_SEND_JOKYO_3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rb_SEND_JOKYO_3.PopupSearchSendParams")));
            this.rb_SEND_JOKYO_3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rb_SEND_JOKYO_3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rb_SEND_JOKYO_3.popupWindowSetting")));
            this.rb_SEND_JOKYO_3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_SEND_JOKYO_3.RegistCheckMethod")));
            this.rb_SEND_JOKYO_3.Size = new System.Drawing.Size(158, 17);
            this.rb_SEND_JOKYO_3.TabIndex = 872;
            this.rb_SEND_JOKYO_3.Tag = "送信済であるｼｮｰﾄﾒｯｾｰｼﾞを表示したい場合にチェックを付けて下さい";
            this.rb_SEND_JOKYO_3.Text = "3. 送信済み(エラー)";
            this.rb_SEND_JOKYO_3.UseVisualStyleBackColor = true;
            this.rb_SEND_JOKYO_3.Value = "3";
            // 
            // SEND_JOKYO
            // 
            this.SEND_JOKYO.BackColor = System.Drawing.SystemColors.Window;
            this.SEND_JOKYO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SEND_JOKYO.DBFieldsName = "";
            this.SEND_JOKYO.DefaultBackColor = System.Drawing.Color.Empty;
            this.SEND_JOKYO.DisplayItemName = "送信状況";
            this.SEND_JOKYO.DisplayPopUp = null;
            this.SEND_JOKYO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEND_JOKYO.FocusOutCheckMethod")));
            this.SEND_JOKYO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SEND_JOKYO.ForeColor = System.Drawing.Color.Black;
            this.SEND_JOKYO.IsInputErrorOccured = false;
            this.SEND_JOKYO.ItemDefinedTypes = "smallint";
            this.SEND_JOKYO.LinkedRadioButtonArray = new string[] {
        "rb_SEND_JOKYO_1",
        "rb_SEND_JOKYO_2",
        "rb_SEND_JOKYO_3"};
            this.SEND_JOKYO.Location = new System.Drawing.Point(4, 1);
            this.SEND_JOKYO.Name = "SEND_JOKYO";
            this.SEND_JOKYO.PopupAfterExecute = null;
            this.SEND_JOKYO.PopupBeforeExecute = null;
            this.SEND_JOKYO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEND_JOKYO.PopupSearchSendParams")));
            this.SEND_JOKYO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SEND_JOKYO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEND_JOKYO.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            3,
            0,
            0,
            0});
            rangeSettingDto2.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SEND_JOKYO.RangeSetting = rangeSettingDto2;
            this.SEND_JOKYO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEND_JOKYO.RegistCheckMethod")));
            this.SEND_JOKYO.Size = new System.Drawing.Size(20, 20);
            this.SEND_JOKYO.TabIndex = 869;
            this.SEND_JOKYO.Tag = "【1~3】のいずれかで入力して下さい";
            this.SEND_JOKYO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.SEND_JOKYO.WordWrap = false;
            this.SEND_JOKYO.TextChanged += new System.EventHandler(this.SMS_SEND_JOKYO_TextChanged);
            // 
            // rb_SEND_JOKYO_2
            // 
            this.rb_SEND_JOKYO_2.AutoSize = true;
            this.rb_SEND_JOKYO_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.rb_SEND_JOKYO_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_SEND_JOKYO_2.FocusOutCheckMethod")));
            this.rb_SEND_JOKYO_2.LinkedTextBox = "SEND_JOKYO";
            this.rb_SEND_JOKYO_2.Location = new System.Drawing.Point(125, 3);
            this.rb_SEND_JOKYO_2.Name = "rb_SEND_JOKYO_2";
            this.rb_SEND_JOKYO_2.PopupAfterExecute = null;
            this.rb_SEND_JOKYO_2.PopupBeforeExecute = null;
            this.rb_SEND_JOKYO_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rb_SEND_JOKYO_2.PopupSearchSendParams")));
            this.rb_SEND_JOKYO_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rb_SEND_JOKYO_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rb_SEND_JOKYO_2.popupWindowSetting")));
            this.rb_SEND_JOKYO_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_SEND_JOKYO_2.RegistCheckMethod")));
            this.rb_SEND_JOKYO_2.Size = new System.Drawing.Size(102, 17);
            this.rb_SEND_JOKYO_2.TabIndex = 871;
            this.rb_SEND_JOKYO_2.Tag = "送信済であるｼｮｰﾄﾒｯｾｰｼﾞを表示したい場合にチェックを付けて下さい";
            this.rb_SEND_JOKYO_2.Text = "2. 送信済み";
            this.rb_SEND_JOKYO_2.UseVisualStyleBackColor = true;
            this.rb_SEND_JOKYO_2.Value = "2";
            // 
            // rb_SEND_JOKYO_1
            // 
            this.rb_SEND_JOKYO_1.AutoSize = true;
            this.rb_SEND_JOKYO_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.rb_SEND_JOKYO_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_SEND_JOKYO_1.FocusOutCheckMethod")));
            this.rb_SEND_JOKYO_1.LinkedTextBox = "SEND_JOKYO";
            this.rb_SEND_JOKYO_1.Location = new System.Drawing.Point(31, 3);
            this.rb_SEND_JOKYO_1.Name = "rb_SEND_JOKYO_1";
            this.rb_SEND_JOKYO_1.PopupAfterExecute = null;
            this.rb_SEND_JOKYO_1.PopupBeforeExecute = null;
            this.rb_SEND_JOKYO_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rb_SEND_JOKYO_1.PopupSearchSendParams")));
            this.rb_SEND_JOKYO_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rb_SEND_JOKYO_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rb_SEND_JOKYO_1.popupWindowSetting")));
            this.rb_SEND_JOKYO_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_SEND_JOKYO_1.RegistCheckMethod")));
            this.rb_SEND_JOKYO_1.Size = new System.Drawing.Size(88, 17);
            this.rb_SEND_JOKYO_1.TabIndex = 870;
            this.rb_SEND_JOKYO_1.Tag = "未送信であるｼｮｰﾄﾒｯｾｰｼﾞを表示したい場合にチェックを付けて下さい";
            this.rb_SEND_JOKYO_1.Text = "1. 未送信";
            this.rb_SEND_JOKYO_1.UseVisualStyleBackColor = true;
            this.rb_SEND_JOKYO_1.Value = "1";
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
            this.KYOTEN_NAME_RYAKU.Location = new System.Drawing.Point(568, 2);
            this.KYOTEN_NAME_RYAKU.MaxLength = 0;
            this.KYOTEN_NAME_RYAKU.Name = "KYOTEN_NAME_RYAKU";
            this.KYOTEN_NAME_RYAKU.PopupAfterExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupBeforeExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupGetMasterField = "";
            this.KYOTEN_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.PopupSearchSendParams")));
            this.KYOTEN_NAME_RYAKU.PopupSetFormField = "";
            this.KYOTEN_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOTEN_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.popupWindowSetting")));
            this.KYOTEN_NAME_RYAKU.ReadOnly = true;
            this.KYOTEN_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.RegistCheckMethod")));
            this.KYOTEN_NAME_RYAKU.SetFormField = "";
            this.KYOTEN_NAME_RYAKU.Size = new System.Drawing.Size(160, 20);
            this.KYOTEN_NAME_RYAKU.TabIndex = 1025;
            this.KYOTEN_NAME_RYAKU.TabStop = false;
            this.KYOTEN_NAME_RYAKU.Tag = "";
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
            this.KYOTEN_CD.GetCodeMasterField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.IsInputErrorOccured = false;
            this.KYOTEN_CD.ItemDefinedTypes = "smallint";
            this.KYOTEN_CD.Location = new System.Drawing.Point(539, 2);
            this.KYOTEN_CD.Name = "KYOTEN_CD";
            this.KYOTEN_CD.PopupAfterExecute = null;
            this.KYOTEN_CD.PopupBeforeExecute = null;
            this.KYOTEN_CD.PopupGetMasterField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_CD.PopupSearchSendParams")));
            this.KYOTEN_CD.PopupSetFormField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.KYOTEN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.KYOTEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_CD.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.KYOTEN_CD.RangeSetting = rangeSettingDto3;
            this.KYOTEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.RegistCheckMethod")));
            this.KYOTEN_CD.SetFormField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.Size = new System.Drawing.Size(30, 20);
            this.KYOTEN_CD.TabIndex = 1024;
            this.KYOTEN_CD.Tag = "拠点を指定して下さい（スペースキー押下にて、検索画面を表示します）";
            this.KYOTEN_CD.WordWrap = false;
            // 
            // KYOTEN_LABEL
            // 
            this.KYOTEN_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.KYOTEN_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.KYOTEN_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_LABEL.ForeColor = System.Drawing.Color.White;
            this.KYOTEN_LABEL.Location = new System.Drawing.Point(424, 2);
            this.KYOTEN_LABEL.Name = "KYOTEN_LABEL";
            this.KYOTEN_LABEL.Size = new System.Drawing.Size(110, 20);
            this.KYOTEN_LABEL.TabIndex = 1026;
            this.KYOTEN_LABEL.Text = "拠点※";
            this.KYOTEN_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.label136);
            this.Controls.Add(this.panel61);
            this.Controls.Add(this.KYOTEN_NAME_RYAKU);
            this.Controls.Add(this.KYOTEN_CD);
            this.Controls.Add(this.KYOTEN_LABEL);
            this.Controls.Add(this.alertNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.readDataNumber);
            this.Controls.Add(this.label5);
            this.Name = "UIHeader";
            this.Text = "UIHeader";
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.readDataNumber, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.alertNumber, 0);
            this.Controls.SetChildIndex(this.KYOTEN_LABEL, 0);
            this.Controls.SetChildIndex(this.KYOTEN_CD, 0);
            this.Controls.SetChildIndex(this.KYOTEN_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.panel61, 0);
            this.Controls.SetChildIndex(this.label136, 0);
            this.panel61.ResumeLayout(false);
            this.panel61.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomTextBox readDataNumber;
        public System.Windows.Forms.Label label5;
        internal r_framework.CustomControl.CustomNumericTextBox2 alertNumber;
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label136;
        private System.Windows.Forms.Panel panel61;
        internal r_framework.CustomControl.CustomRadioButton rb_SEND_JOKYO_3;
        internal r_framework.CustomControl.CustomNumericTextBox2 SEND_JOKYO;
        internal r_framework.CustomControl.CustomRadioButton rb_SEND_JOKYO_2;
        internal r_framework.CustomControl.CustomRadioButton rb_SEND_JOKYO_1;
        public r_framework.CustomControl.CustomTextBox KYOTEN_NAME_RYAKU;
        public r_framework.CustomControl.CustomNumericTextBox2 KYOTEN_CD;
        public System.Windows.Forms.Label KYOTEN_LABEL;
    }
}