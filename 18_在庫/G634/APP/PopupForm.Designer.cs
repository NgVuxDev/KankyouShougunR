namespace Shougun.Core.Stock.ZaikoKanriHyo
{
    partial class PopupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.lbl_hanishitei = new System.Windows.Forms.Label();
            this.lbl_syuturyokunaiyou = new System.Windows.Forms.Label();
            this.lbl_hidukehanishitei = new System.Windows.Forms.Label();
            this.lbl_gyousha = new System.Windows.Forms.Label();
            this.OUTPUT_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.rdobtn_gyoushaGenba = new r_framework.CustomControl.CustomRadioButton();
            this.rdobtn_zaikoHinmei = new r_framework.CustomControl.CustomRadioButton();
            this.DATE_FROM = new r_framework.CustomControl.CustomDateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.DATE_TO = new r_framework.CustomControl.CustomDateTimePicker();
            this.GYOUSHA_NAME_FROM = new r_framework.CustomControl.CustomTextBox();
            this.GYOUSHA_CD_FROM = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GYOUSHA_CD_TO = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GYOUSHA_NAME_TO = new r_framework.CustomControl.CustomTextBox();
            this.btn_zengetsu = new r_framework.CustomControl.CustomButton();
            this.btn_jigetsu = new r_framework.CustomControl.CustomButton();
            this.btn_kensakujikkou = new r_framework.CustomControl.CustomButton();
            this.btn_cancel = new r_framework.CustomControl.CustomButton();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.customPanel3 = new r_framework.CustomControl.CustomPanel();
            this.customPanel4 = new r_framework.CustomControl.CustomPanel();
            this.GYOUSHA_POPUP_TO = new r_framework.CustomControl.CustomPopupOpenButton();
            this.GYOUSHA_POPUP_FROM = new r_framework.CustomControl.CustomPopupOpenButton();
            this.customPanel2 = new r_framework.CustomControl.CustomPanel();
            this.GENBA_POPUP_TO = new r_framework.CustomControl.CustomPopupOpenButton();
            this.GENBA_POPUP_FROM = new r_framework.CustomControl.CustomPopupOpenButton();
            this.GENBA_CD_TO = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GENBA_NAME_TO = new r_framework.CustomControl.CustomTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.GENBA_CD_FROM = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GENBA_NAME_FROM = new r_framework.CustomControl.CustomTextBox();
            this.lbl_genba = new System.Windows.Forms.Label();
            this.customPanel5 = new r_framework.CustomControl.CustomPanel();
            this.ZAIKO_HINMEI_POPUP_TO = new r_framework.CustomControl.CustomPopupOpenButton();
            this.ZAIKO_HINMEI_POPUP_FROM = new r_framework.CustomControl.CustomPopupOpenButton();
            this.ZAIKO_HINMEI_CD_TO = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.ZAIKO_HINMEI_NAME_TO = new r_framework.CustomControl.CustomTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ZAIKO_HINMEI_CD_FROM = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.ZAIKO_HINMEI_NAME_FROM = new r_framework.CustomControl.CustomTextBox();
            this.lbl_zaikoHinmei = new System.Windows.Forms.Label();
            this.customPanel1.SuspendLayout();
            this.customPanel3.SuspendLayout();
            this.customPanel4.SuspendLayout();
            this.customPanel2.SuspendLayout();
            this.customPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_hanishitei
            // 
            this.lbl_hanishitei.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_hanishitei.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_hanishitei.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_hanishitei.Font = new System.Drawing.Font("ＭＳ ゴシック", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_hanishitei.ForeColor = System.Drawing.Color.White;
            this.lbl_hanishitei.Location = new System.Drawing.Point(12, 9);
            this.lbl_hanishitei.Name = "lbl_hanishitei";
            this.lbl_hanishitei.Size = new System.Drawing.Size(463, 35);
            this.lbl_hanishitei.TabIndex = 500;
            this.lbl_hanishitei.Text = "在庫管理表 - 範囲条件指定";
            this.lbl_hanishitei.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_syuturyokunaiyou
            // 
            this.lbl_syuturyokunaiyou.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_syuturyokunaiyou.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_syuturyokunaiyou.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_syuturyokunaiyou.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_syuturyokunaiyou.ForeColor = System.Drawing.Color.White;
            this.lbl_syuturyokunaiyou.Location = new System.Drawing.Point(12, 115);
            this.lbl_syuturyokunaiyou.Name = "lbl_syuturyokunaiyou";
            this.lbl_syuturyokunaiyou.Size = new System.Drawing.Size(110, 20);
            this.lbl_syuturyokunaiyou.TabIndex = 501;
            this.lbl_syuturyokunaiyou.Text = "出力内容※";
            this.lbl_syuturyokunaiyou.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_hidukehanishitei
            // 
            this.lbl_hidukehanishitei.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_hidukehanishitei.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_hidukehanishitei.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_hidukehanishitei.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_hidukehanishitei.ForeColor = System.Drawing.Color.White;
            this.lbl_hidukehanishitei.Location = new System.Drawing.Point(12, 64);
            this.lbl_hidukehanishitei.Name = "lbl_hidukehanishitei";
            this.lbl_hidukehanishitei.Size = new System.Drawing.Size(110, 20);
            this.lbl_hidukehanishitei.TabIndex = 504;
            this.lbl_hidukehanishitei.Text = "伝票日付範囲※";
            this.lbl_hidukehanishitei.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_gyousha
            // 
            this.lbl_gyousha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_gyousha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_gyousha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_gyousha.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_gyousha.ForeColor = System.Drawing.Color.White;
            this.lbl_gyousha.Location = new System.Drawing.Point(12, 166);
            this.lbl_gyousha.Name = "lbl_gyousha";
            this.lbl_gyousha.Size = new System.Drawing.Size(110, 20);
            this.lbl_gyousha.TabIndex = 505;
            this.lbl_gyousha.Text = "業者";
            this.lbl_gyousha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OUTPUT_KBN
            // 
            this.OUTPUT_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.OUTPUT_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OUTPUT_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.OUTPUT_KBN.DisplayItemName = "出力内容";
            this.OUTPUT_KBN.DisplayPopUp = null;
            this.OUTPUT_KBN.FocusOutCheckMethod = null;
            this.OUTPUT_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.OUTPUT_KBN.ForeColor = System.Drawing.Color.Black;
            this.OUTPUT_KBN.IsInputErrorOccured = false;
            this.OUTPUT_KBN.LinkedRadioButtonArray = new string[] {
        "rdobtn_gyoushaGenba",
        "rdobtn_zaikoHinmei"};
            this.OUTPUT_KBN.Location = new System.Drawing.Point(-1, -1);
            this.OUTPUT_KBN.Name = "OUTPUT_KBN";
            this.OUTPUT_KBN.PopupAfterExecute = null;
            this.OUTPUT_KBN.PopupBeforeExecute = null;
            this.OUTPUT_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("OUTPUT_KBN.PopupSearchSendParams")));
            this.OUTPUT_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.OUTPUT_KBN.popupWindowSetting = null;
            this.OUTPUT_KBN.prevText = "";
            this.OUTPUT_KBN.PrevText = "";
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
            this.OUTPUT_KBN.RangeSetting = rangeSettingDto1;
            this.OUTPUT_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("OUTPUT_KBN.RegistCheckMethod")));
            this.OUTPUT_KBN.Size = new System.Drawing.Size(20, 20);
            this.OUTPUT_KBN.TabIndex = 1;
            this.OUTPUT_KBN.Tag = "【1、2】のいずれかで入力してください";
            this.OUTPUT_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.OUTPUT_KBN.WordWrap = false;
            // 
            // rdobtn_gyoushaGenba
            // 
            this.rdobtn_gyoushaGenba.AutoSize = true;
            this.rdobtn_gyoushaGenba.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdobtn_gyoushaGenba.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdobtn_gyoushaGenba.FocusOutCheckMethod")));
            this.rdobtn_gyoushaGenba.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdobtn_gyoushaGenba.LinkedTextBox = "OUTPUT_KBN";
            this.rdobtn_gyoushaGenba.Location = new System.Drawing.Point(25, 0);
            this.rdobtn_gyoushaGenba.Name = "rdobtn_gyoushaGenba";
            this.rdobtn_gyoushaGenba.PopupAfterExecute = null;
            this.rdobtn_gyoushaGenba.PopupBeforeExecute = null;
            this.rdobtn_gyoushaGenba.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdobtn_gyoushaGenba.PopupSearchSendParams")));
            this.rdobtn_gyoushaGenba.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdobtn_gyoushaGenba.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdobtn_gyoushaGenba.popupWindowSetting")));
            this.rdobtn_gyoushaGenba.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdobtn_gyoushaGenba.RegistCheckMethod")));
            this.rdobtn_gyoushaGenba.Size = new System.Drawing.Size(116, 17);
            this.rdobtn_gyoushaGenba.TabIndex = 2;
            this.rdobtn_gyoushaGenba.Tag = "出力内容を「業者/現場別」にする場合にはチェックを付けてください";
            this.rdobtn_gyoushaGenba.Text = "1.業者/現場別";
            this.rdobtn_gyoushaGenba.UseVisualStyleBackColor = true;
            this.rdobtn_gyoushaGenba.Value = "1";
            this.rdobtn_gyoushaGenba.CheckedChanged += new System.EventHandler(this.rdobtn_gyoushaGenba_CheckedChanged);
            // 
            // rdobtn_zaikoHinmei
            // 
            this.rdobtn_zaikoHinmei.AutoSize = true;
            this.rdobtn_zaikoHinmei.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdobtn_zaikoHinmei.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdobtn_zaikoHinmei.FocusOutCheckMethod")));
            this.rdobtn_zaikoHinmei.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdobtn_zaikoHinmei.LinkedTextBox = "OUTPUT_KBN";
            this.rdobtn_zaikoHinmei.Location = new System.Drawing.Point(172, 0);
            this.rdobtn_zaikoHinmei.Name = "rdobtn_zaikoHinmei";
            this.rdobtn_zaikoHinmei.PopupAfterExecute = null;
            this.rdobtn_zaikoHinmei.PopupBeforeExecute = null;
            this.rdobtn_zaikoHinmei.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdobtn_zaikoHinmei.PopupSearchSendParams")));
            this.rdobtn_zaikoHinmei.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdobtn_zaikoHinmei.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdobtn_zaikoHinmei.popupWindowSetting")));
            this.rdobtn_zaikoHinmei.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdobtn_zaikoHinmei.RegistCheckMethod")));
            this.rdobtn_zaikoHinmei.Size = new System.Drawing.Size(109, 17);
            this.rdobtn_zaikoHinmei.TabIndex = 3;
            this.rdobtn_zaikoHinmei.Tag = "出力内容を「在庫品名別」にする場合にはチェックを付けてください";
            this.rdobtn_zaikoHinmei.Text = "2.在庫品名別";
            this.rdobtn_zaikoHinmei.UseVisualStyleBackColor = true;
            this.rdobtn_zaikoHinmei.Value = "2";
            this.rdobtn_zaikoHinmei.CheckedChanged += new System.EventHandler(this.rdobtn_zaikoHinmei_CheckedChanged);
            // 
            // DATE_FROM
            // 
            this.DATE_FROM.BackColor = System.Drawing.SystemColors.Window;
            this.DATE_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DATE_FROM.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.DATE_FROM.Checked = false;
            this.DATE_FROM.CustomFormat = "yyyy/MM/dd(ddd)";
            this.DATE_FROM.DateTimeNowYear = "";
            this.DATE_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_FROM.DisplayItemName = "開始日付";
            this.DATE_FROM.DisplayPopUp = null;
            this.DATE_FROM.Enabled = false;
            this.DATE_FROM.FocusOutCheckMethod = null;
            this.DATE_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.DATE_FROM.ForeColor = System.Drawing.Color.Black;
            this.DATE_FROM.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DATE_FROM.IsInputErrorOccured = false;
            this.DATE_FROM.Location = new System.Drawing.Point(5, 6);
            this.DATE_FROM.MaxLength = 10;
            this.DATE_FROM.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.DATE_FROM.Name = "DATE_FROM";
            this.DATE_FROM.NullValue = "";
            this.DATE_FROM.PopupAfterExecute = null;
            this.DATE_FROM.PopupBeforeExecute = null;
            this.DATE_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DATE_FROM.PopupSearchSendParams")));
            this.DATE_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DATE_FROM.popupWindowSetting = null;
            this.DATE_FROM.RegistCheckMethod = null;
            this.DATE_FROM.ShortItemName = "伝票日付FROM";
            this.DATE_FROM.Size = new System.Drawing.Size(138, 20);
            this.DATE_FROM.TabIndex = 9;
            this.DATE_FROM.Tag = "「日付範囲指定-開始日付」を入力してください";
            this.DATE_FROM.Text = "2013/12/09(月)";
            this.DATE_FROM.Value = new System.DateTime(2013, 12, 9, 0, 0, 0, 0);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label6.Location = new System.Drawing.Point(156, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 13);
            this.label6.TabIndex = 533;
            this.label6.Text = "～";
            // 
            // DATE_TO
            // 
            this.DATE_TO.BackColor = System.Drawing.SystemColors.Window;
            this.DATE_TO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DATE_TO.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.DATE_TO.Checked = false;
            this.DATE_TO.CustomFormat = "yyyy/MM/dd(ddd)";
            this.DATE_TO.DateTimeNowYear = "";
            this.DATE_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_TO.DisplayItemName = "終了日付";
            this.DATE_TO.DisplayPopUp = null;
            this.DATE_TO.Enabled = false;
            this.DATE_TO.FocusOutCheckMethod = null;
            this.DATE_TO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.DATE_TO.ForeColor = System.Drawing.Color.Black;
            this.DATE_TO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DATE_TO.IsInputErrorOccured = false;
            this.DATE_TO.Location = new System.Drawing.Point(188, 6);
            this.DATE_TO.MaxLength = 10;
            this.DATE_TO.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.DATE_TO.Name = "DATE_TO";
            this.DATE_TO.NullValue = "";
            this.DATE_TO.PopupAfterExecute = null;
            this.DATE_TO.PopupBeforeExecute = null;
            this.DATE_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DATE_TO.PopupSearchSendParams")));
            this.DATE_TO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DATE_TO.popupWindowSetting = null;
            this.DATE_TO.RegistCheckMethod = null;
            this.DATE_TO.Size = new System.Drawing.Size(138, 20);
            this.DATE_TO.TabIndex = 10;
            this.DATE_TO.Tag = "「日付範囲指定-終了日付」を入力してください";
            this.DATE_TO.Text = "2013/12/09(月)";
            this.DATE_TO.Value = new System.DateTime(2013, 12, 9, 0, 0, 0, 0);
            // 
            // GYOUSHA_NAME_FROM
            // 
            this.GYOUSHA_NAME_FROM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GYOUSHA_NAME_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_NAME_FROM.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.GYOUSHA_NAME_FROM.DBFieldsName = "GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_NAME_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_NAME_FROM.DisplayPopUp = null;
            this.GYOUSHA_NAME_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME_FROM.FocusOutCheckMethod")));
            this.GYOUSHA_NAME_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GYOUSHA_NAME_FROM.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_NAME_FROM.IsInputErrorOccured = false;
            this.GYOUSHA_NAME_FROM.Location = new System.Drawing.Point(57, 8);
            this.GYOUSHA_NAME_FROM.MaxLength = 20;
            this.GYOUSHA_NAME_FROM.Name = "GYOUSHA_NAME_FROM";
            this.GYOUSHA_NAME_FROM.PopupAfterExecute = null;
            this.GYOUSHA_NAME_FROM.PopupBeforeExecute = null;
            this.GYOUSHA_NAME_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_NAME_FROM.PopupSearchSendParams")));
            this.GYOUSHA_NAME_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_NAME_FROM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_NAME_FROM.popupWindowSetting")));
            this.GYOUSHA_NAME_FROM.prevText = null;
            this.GYOUSHA_NAME_FROM.PrevText = null;
            this.GYOUSHA_NAME_FROM.ReadOnly = true;
            this.GYOUSHA_NAME_FROM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME_FROM.RegistCheckMethod")));
            this.GYOUSHA_NAME_FROM.Size = new System.Drawing.Size(286, 20);
            this.GYOUSHA_NAME_FROM.TabIndex = 12;
            this.GYOUSHA_NAME_FROM.TabStop = false;
            this.GYOUSHA_NAME_FROM.Tag = "";
            // 
            // GYOUSHA_CD_FROM
            // 
            this.GYOUSHA_CD_FROM.BackColor = System.Drawing.SystemColors.Window;
            this.GYOUSHA_CD_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_CD_FROM.ChangeUpperCase = true;
            this.GYOUSHA_CD_FROM.CharacterLimitList = null;
            this.GYOUSHA_CD_FROM.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.GYOUSHA_CD_FROM.DBFieldsName = "GYOUSHA_CD";
            this.GYOUSHA_CD_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_CD_FROM.DisplayItemName = "業者CD";
            this.GYOUSHA_CD_FROM.DisplayPopUp = null;
            this.GYOUSHA_CD_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD_FROM.FocusOutCheckMethod")));
            this.GYOUSHA_CD_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GYOUSHA_CD_FROM.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_CD_FROM.GetCodeMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD_FROM.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GYOUSHA_CD_FROM.IsInputErrorOccured = false;
            this.GYOUSHA_CD_FROM.ItemDefinedTypes = "varchar";
            this.GYOUSHA_CD_FROM.Location = new System.Drawing.Point(5, 8);
            this.GYOUSHA_CD_FROM.MaxLength = 6;
            this.GYOUSHA_CD_FROM.Name = "GYOUSHA_CD_FROM";
            this.GYOUSHA_CD_FROM.PopupAfterExecute = null;
            this.GYOUSHA_CD_FROM.PopupAfterExecuteMethod = "gyoushaCdFromPopupAfter";
            this.GYOUSHA_CD_FROM.PopupBeforeExecute = null;
            this.GYOUSHA_CD_FROM.PopupBeforeExecuteMethod = "gyoushaCdFromPopupBefore";
            this.GYOUSHA_CD_FROM.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_CD_FROM.PopupSearchSendParams")));
            this.GYOUSHA_CD_FROM.PopupSetFormField = "GYOUSHA_CD_FROM,GYOUSHA_NAME_FROM";
            this.GYOUSHA_CD_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_CD_FROM.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_CD_FROM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_CD_FROM.popupWindowSetting")));
            this.GYOUSHA_CD_FROM.prevText = null;
            this.GYOUSHA_CD_FROM.PrevText = null;
            this.GYOUSHA_CD_FROM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD_FROM.RegistCheckMethod")));
            this.GYOUSHA_CD_FROM.SetFormField = "GYOUSHA_CD_FROM,GYOUSHA_NAME_FROM";
            this.GYOUSHA_CD_FROM.ShortItemName = "業者CD";
            this.GYOUSHA_CD_FROM.Size = new System.Drawing.Size(53, 20);
            this.GYOUSHA_CD_FROM.TabIndex = 11;
            this.GYOUSHA_CD_FROM.Tag = "業者Fromを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GYOUSHA_CD_FROM.ZeroPaddengFlag = true;
            this.GYOUSHA_CD_FROM.Enter += new System.EventHandler(this.GYOUSHA_CD_FROM_Enter);
            this.GYOUSHA_CD_FROM.Validated += new System.EventHandler(this.GYOUSHA_CD_FROM_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.Location = new System.Drawing.Point(377, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 599;
            this.label1.Text = "～";
            // 
            // GYOUSHA_CD_TO
            // 
            this.GYOUSHA_CD_TO.BackColor = System.Drawing.SystemColors.Window;
            this.GYOUSHA_CD_TO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_CD_TO.ChangeUpperCase = true;
            this.GYOUSHA_CD_TO.CharacterLimitList = null;
            this.GYOUSHA_CD_TO.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.GYOUSHA_CD_TO.DBFieldsName = "GYOUSHA_CD";
            this.GYOUSHA_CD_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_CD_TO.DisplayItemName = "業者CD";
            this.GYOUSHA_CD_TO.DisplayPopUp = null;
            this.GYOUSHA_CD_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD_TO.FocusOutCheckMethod")));
            this.GYOUSHA_CD_TO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GYOUSHA_CD_TO.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_CD_TO.GetCodeMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD_TO.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GYOUSHA_CD_TO.IsInputErrorOccured = false;
            this.GYOUSHA_CD_TO.ItemDefinedTypes = "varchar";
            this.GYOUSHA_CD_TO.Location = new System.Drawing.Point(406, 8);
            this.GYOUSHA_CD_TO.MaxLength = 6;
            this.GYOUSHA_CD_TO.Name = "GYOUSHA_CD_TO";
            this.GYOUSHA_CD_TO.PopupAfterExecute = null;
            this.GYOUSHA_CD_TO.PopupAfterExecuteMethod = "gyoushaCdToPopupAfter";
            this.GYOUSHA_CD_TO.PopupBeforeExecute = null;
            this.GYOUSHA_CD_TO.PopupBeforeExecuteMethod = "gyoushaCdToPopupBefore";
            this.GYOUSHA_CD_TO.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_CD_TO.PopupSearchSendParams")));
            this.GYOUSHA_CD_TO.PopupSetFormField = "GYOUSHA_CD_TO,GYOUSHA_NAME_TO";
            this.GYOUSHA_CD_TO.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_CD_TO.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_CD_TO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_CD_TO.popupWindowSetting")));
            this.GYOUSHA_CD_TO.prevText = null;
            this.GYOUSHA_CD_TO.PrevText = null;
            this.GYOUSHA_CD_TO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD_TO.RegistCheckMethod")));
            this.GYOUSHA_CD_TO.SetFormField = "GYOUSHA_CD_TO,GYOUSHA_NAME_TO";
            this.GYOUSHA_CD_TO.ShortItemName = "業者CD";
            this.GYOUSHA_CD_TO.Size = new System.Drawing.Size(53, 20);
            this.GYOUSHA_CD_TO.TabIndex = 14;
            this.GYOUSHA_CD_TO.Tag = "業者Toを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GYOUSHA_CD_TO.ZeroPaddengFlag = true;
            this.GYOUSHA_CD_TO.Enter += new System.EventHandler(this.GYOUSHA_CD_TO_Enter);
            this.GYOUSHA_CD_TO.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GyoushaToDoubleClick);
            this.GYOUSHA_CD_TO.Validated += new System.EventHandler(this.GYOUSHA_CD_TO_Validated);
            // 
            // GYOUSHA_NAME_TO
            // 
            this.GYOUSHA_NAME_TO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GYOUSHA_NAME_TO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_NAME_TO.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.GYOUSHA_NAME_TO.DBFieldsName = "GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_NAME_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_NAME_TO.DisplayPopUp = null;
            this.GYOUSHA_NAME_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME_TO.FocusOutCheckMethod")));
            this.GYOUSHA_NAME_TO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GYOUSHA_NAME_TO.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_NAME_TO.IsInputErrorOccured = false;
            this.GYOUSHA_NAME_TO.Location = new System.Drawing.Point(458, 8);
            this.GYOUSHA_NAME_TO.MaxLength = 20;
            this.GYOUSHA_NAME_TO.Name = "GYOUSHA_NAME_TO";
            this.GYOUSHA_NAME_TO.PopupAfterExecute = null;
            this.GYOUSHA_NAME_TO.PopupBeforeExecute = null;
            this.GYOUSHA_NAME_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_NAME_TO.PopupSearchSendParams")));
            this.GYOUSHA_NAME_TO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_NAME_TO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_NAME_TO.popupWindowSetting")));
            this.GYOUSHA_NAME_TO.prevText = null;
            this.GYOUSHA_NAME_TO.PrevText = null;
            this.GYOUSHA_NAME_TO.ReadOnly = true;
            this.GYOUSHA_NAME_TO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME_TO.RegistCheckMethod")));
            this.GYOUSHA_NAME_TO.Size = new System.Drawing.Size(286, 20);
            this.GYOUSHA_NAME_TO.TabIndex = 15;
            this.GYOUSHA_NAME_TO.TabStop = false;
            this.GYOUSHA_NAME_TO.Tag = "";
            // 
            // btn_zengetsu
            // 
            this.btn_zengetsu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btn_zengetsu.DefaultBackColor = System.Drawing.Color.Empty;
            this.btn_zengetsu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.btn_zengetsu.Location = new System.Drawing.Point(12, 266);
            this.btn_zengetsu.Name = "btn_zengetsu";
            this.btn_zengetsu.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btn_zengetsu.Size = new System.Drawing.Size(90, 35);
            this.btn_zengetsu.TabIndex = 17;
            this.btn_zengetsu.TabStop = false;
            this.btn_zengetsu.Tag = "日付範囲指定の開始／終了を前月に変更します";
            this.btn_zengetsu.Text = "[F1]\r\n前月";
            this.btn_zengetsu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_zengetsu.UseVisualStyleBackColor = false;
            // 
            // btn_jigetsu
            // 
            this.btn_jigetsu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btn_jigetsu.DefaultBackColor = System.Drawing.Color.Empty;
            this.btn_jigetsu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.btn_jigetsu.Location = new System.Drawing.Point(107, 266);
            this.btn_jigetsu.Name = "btn_jigetsu";
            this.btn_jigetsu.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btn_jigetsu.Size = new System.Drawing.Size(90, 35);
            this.btn_jigetsu.TabIndex = 18;
            this.btn_jigetsu.TabStop = false;
            this.btn_jigetsu.Tag = "日付範囲指定の開始／終了を次月に変更します";
            this.btn_jigetsu.Text = "[F2]\r\n次月";
            this.btn_jigetsu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_jigetsu.UseVisualStyleBackColor = false;
            // 
            // btn_kensakujikkou
            // 
            this.btn_kensakujikkou.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btn_kensakujikkou.DefaultBackColor = System.Drawing.Color.Empty;
            this.btn_kensakujikkou.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.btn_kensakujikkou.Location = new System.Drawing.Point(728, 266);
            this.btn_kensakujikkou.Name = "btn_kensakujikkou";
            this.btn_kensakujikkou.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btn_kensakujikkou.Size = new System.Drawing.Size(90, 35);
            this.btn_kensakujikkou.TabIndex = 19;
            this.btn_kensakujikkou.TabStop = false;
            this.btn_kensakujikkou.Tag = "検索を実行します";
            this.btn_kensakujikkou.Text = "[F8]\r\n検索実行";
            this.btn_kensakujikkou.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_kensakujikkou.UseVisualStyleBackColor = false;
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btn_cancel.DefaultBackColor = System.Drawing.Color.Empty;
            this.btn_cancel.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.btn_cancel.Location = new System.Drawing.Point(822, 266);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btn_cancel.Size = new System.Drawing.Size(90, 35);
            this.btn_cancel.TabIndex = 20;
            this.btn_cancel.TabStop = false;
            this.btn_cancel.Tag = "画面を閉じます";
            this.btn_cancel.Text = "[F12]\r\nキャンセル";
            this.btn_cancel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_cancel.UseVisualStyleBackColor = false;
            // 
            // customPanel1
            // 
            this.customPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel1.Controls.Add(this.rdobtn_zaikoHinmei);
            this.customPanel1.Controls.Add(this.rdobtn_gyoushaGenba);
            this.customPanel1.Controls.Add(this.OUTPUT_KBN);
            this.customPanel1.Location = new System.Drawing.Point(127, 115);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(321, 20);
            this.customPanel1.TabIndex = 0;
            // 
            // customPanel3
            // 
            this.customPanel3.Controls.Add(this.DATE_TO);
            this.customPanel3.Controls.Add(this.label6);
            this.customPanel3.Controls.Add(this.DATE_FROM);
            this.customPanel3.Location = new System.Drawing.Point(122, 58);
            this.customPanel3.Name = "customPanel3";
            this.customPanel3.Size = new System.Drawing.Size(353, 38);
            this.customPanel3.TabIndex = 3;
            // 
            // customPanel4
            // 
            this.customPanel4.Controls.Add(this.GYOUSHA_POPUP_TO);
            this.customPanel4.Controls.Add(this.GYOUSHA_POPUP_FROM);
            this.customPanel4.Controls.Add(this.GYOUSHA_CD_TO);
            this.customPanel4.Controls.Add(this.GYOUSHA_NAME_TO);
            this.customPanel4.Controls.Add(this.label1);
            this.customPanel4.Controls.Add(this.GYOUSHA_CD_FROM);
            this.customPanel4.Controls.Add(this.GYOUSHA_NAME_FROM);
            this.customPanel4.Location = new System.Drawing.Point(122, 158);
            this.customPanel4.Name = "customPanel4";
            this.customPanel4.Size = new System.Drawing.Size(790, 36);
            this.customPanel4.TabIndex = 4;
            // 
            // GYOUSHA_POPUP_TO
            // 
            this.GYOUSHA_POPUP_TO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.GYOUSHA_POPUP_TO.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.GYOUSHA_POPUP_TO.DBFieldsName = null;
            this.GYOUSHA_POPUP_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_POPUP_TO.DisplayItemName = null;
            this.GYOUSHA_POPUP_TO.DisplayPopUp = null;
            this.GYOUSHA_POPUP_TO.ErrorMessage = null;
            this.GYOUSHA_POPUP_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_POPUP_TO.FocusOutCheckMethod")));
            this.GYOUSHA_POPUP_TO.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.GYOUSHA_POPUP_TO.GetCodeMasterField = null;
            this.GYOUSHA_POPUP_TO.Image = global::Shougun.Core.Stock.ZaikoKanriHyo.Properties.Resources.虫眼鏡;
            this.GYOUSHA_POPUP_TO.ItemDefinedTypes = null;
            this.GYOUSHA_POPUP_TO.LinkedSettingTextBox = null;
            this.GYOUSHA_POPUP_TO.LinkedTextBoxs = null;
            this.GYOUSHA_POPUP_TO.Location = new System.Drawing.Point(747, 7);
            this.GYOUSHA_POPUP_TO.Name = "GYOUSHA_POPUP_TO";
            this.GYOUSHA_POPUP_TO.PopupAfterExecute = null;
            this.GYOUSHA_POPUP_TO.PopupAfterExecuteMethod = "gyoushaCdToPopupAfter";
            this.GYOUSHA_POPUP_TO.PopupBeforeExecute = null;
            this.GYOUSHA_POPUP_TO.PopupBeforeExecuteMethod = "gyoushaCdToPopupBefore";
            this.GYOUSHA_POPUP_TO.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_POPUP_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_POPUP_TO.PopupSearchSendParams")));
            this.GYOUSHA_POPUP_TO.PopupSetFormField = "GYOUSHA_CD_TO,GYOUSHA_NAME_TO";
            this.GYOUSHA_POPUP_TO.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_POPUP_TO.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_POPUP_TO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_POPUP_TO.popupWindowSetting")));
            this.GYOUSHA_POPUP_TO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_POPUP_TO.RegistCheckMethod")));
            this.GYOUSHA_POPUP_TO.SearchDisplayFlag = 0;
            this.GYOUSHA_POPUP_TO.SetFormField = "GYOUSHA_CD_TO,GYOUSHA_NAME_TO";
            this.GYOUSHA_POPUP_TO.ShortItemName = null;
            this.GYOUSHA_POPUP_TO.Size = new System.Drawing.Size(22, 22);
            this.GYOUSHA_POPUP_TO.TabIndex = 16;
            this.GYOUSHA_POPUP_TO.TabStop = false;
            this.GYOUSHA_POPUP_TO.Tag = "検索画面を表示します";
            this.GYOUSHA_POPUP_TO.UseVisualStyleBackColor = false;
            this.GYOUSHA_POPUP_TO.ZeroPaddengFlag = false;
            // 
            // GYOUSHA_POPUP_FROM
            // 
            this.GYOUSHA_POPUP_FROM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.GYOUSHA_POPUP_FROM.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.GYOUSHA_POPUP_FROM.DBFieldsName = null;
            this.GYOUSHA_POPUP_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_POPUP_FROM.DisplayItemName = null;
            this.GYOUSHA_POPUP_FROM.DisplayPopUp = null;
            this.GYOUSHA_POPUP_FROM.ErrorMessage = null;
            this.GYOUSHA_POPUP_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_POPUP_FROM.FocusOutCheckMethod")));
            this.GYOUSHA_POPUP_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.GYOUSHA_POPUP_FROM.GetCodeMasterField = null;
            this.GYOUSHA_POPUP_FROM.Image = global::Shougun.Core.Stock.ZaikoKanriHyo.Properties.Resources.虫眼鏡;
            this.GYOUSHA_POPUP_FROM.ItemDefinedTypes = null;
            this.GYOUSHA_POPUP_FROM.LinkedSettingTextBox = null;
            this.GYOUSHA_POPUP_FROM.LinkedTextBoxs = null;
            this.GYOUSHA_POPUP_FROM.Location = new System.Drawing.Point(346, 7);
            this.GYOUSHA_POPUP_FROM.Name = "GYOUSHA_POPUP_FROM";
            this.GYOUSHA_POPUP_FROM.PopupAfterExecute = null;
            this.GYOUSHA_POPUP_FROM.PopupAfterExecuteMethod = "gyoushaCdFromPopupAfter";
            this.GYOUSHA_POPUP_FROM.PopupBeforeExecute = null;
            this.GYOUSHA_POPUP_FROM.PopupBeforeExecuteMethod = "gyoushaCdFromPopupBefore";
            this.GYOUSHA_POPUP_FROM.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_POPUP_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_POPUP_FROM.PopupSearchSendParams")));
            this.GYOUSHA_POPUP_FROM.PopupSetFormField = "GYOUSHA_CD_FROM,GYOUSHA_NAME_FROM";
            this.GYOUSHA_POPUP_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_POPUP_FROM.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_POPUP_FROM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_POPUP_FROM.popupWindowSetting")));
            this.GYOUSHA_POPUP_FROM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_POPUP_FROM.RegistCheckMethod")));
            this.GYOUSHA_POPUP_FROM.SearchDisplayFlag = 0;
            this.GYOUSHA_POPUP_FROM.SetFormField = "GYOUSHA_CD_FROM,GYOUSHA_NAME_FROM";
            this.GYOUSHA_POPUP_FROM.ShortItemName = null;
            this.GYOUSHA_POPUP_FROM.Size = new System.Drawing.Size(22, 22);
            this.GYOUSHA_POPUP_FROM.TabIndex = 13;
            this.GYOUSHA_POPUP_FROM.TabStop = false;
            this.GYOUSHA_POPUP_FROM.Tag = "検索画面を表示します";
            this.GYOUSHA_POPUP_FROM.UseVisualStyleBackColor = false;
            this.GYOUSHA_POPUP_FROM.ZeroPaddengFlag = false;
            // 
            // customPanel2
            // 
            this.customPanel2.Controls.Add(this.GENBA_POPUP_TO);
            this.customPanel2.Controls.Add(this.GENBA_POPUP_FROM);
            this.customPanel2.Controls.Add(this.GENBA_CD_TO);
            this.customPanel2.Controls.Add(this.GENBA_NAME_TO);
            this.customPanel2.Controls.Add(this.label2);
            this.customPanel2.Controls.Add(this.GENBA_CD_FROM);
            this.customPanel2.Controls.Add(this.GENBA_NAME_FROM);
            this.customPanel2.Location = new System.Drawing.Point(122, 189);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(790, 36);
            this.customPanel2.TabIndex = 506;
            // 
            // GENBA_POPUP_TO
            // 
            this.GENBA_POPUP_TO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.GENBA_POPUP_TO.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.GENBA_POPUP_TO.DBFieldsName = null;
            this.GENBA_POPUP_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_POPUP_TO.DisplayItemName = null;
            this.GENBA_POPUP_TO.DisplayPopUp = null;
            this.GENBA_POPUP_TO.ErrorMessage = null;
            this.GENBA_POPUP_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_POPUP_TO.FocusOutCheckMethod")));
            this.GENBA_POPUP_TO.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.GENBA_POPUP_TO.GetCodeMasterField = null;
            this.GENBA_POPUP_TO.Image = global::Shougun.Core.Stock.ZaikoKanriHyo.Properties.Resources.虫眼鏡;
            this.GENBA_POPUP_TO.ItemDefinedTypes = null;
            this.GENBA_POPUP_TO.LinkedSettingTextBox = null;
            this.GENBA_POPUP_TO.LinkedTextBoxs = null;
            this.GENBA_POPUP_TO.Location = new System.Drawing.Point(747, 7);
            this.GENBA_POPUP_TO.Name = "GENBA_POPUP_TO";
            this.GENBA_POPUP_TO.PopupAfterExecute = null;
            this.GENBA_POPUP_TO.PopupAfterExecuteMethod = "genbaCdToPopupAfter";
            this.GENBA_POPUP_TO.PopupBeforeExecute = null;
            this.GENBA_POPUP_TO.PopupBeforeExecuteMethod = "genbaCdToPopupBefore";
            this.GENBA_POPUP_TO.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU";
            this.GENBA_POPUP_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_POPUP_TO.PopupSearchSendParams")));
            this.GENBA_POPUP_TO.PopupSetFormField = "GENBA_CD_TO,GENBA_NAME_TO";
            this.GENBA_POPUP_TO.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_POPUP_TO.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_POPUP_TO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_POPUP_TO.popupWindowSetting")));
            this.GENBA_POPUP_TO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_POPUP_TO.RegistCheckMethod")));
            this.GENBA_POPUP_TO.SearchDisplayFlag = 0;
            this.GENBA_POPUP_TO.SetFormField = "GENBA_CD_TO,GENBA_NAME_TO";
            this.GENBA_POPUP_TO.ShortItemName = null;
            this.GENBA_POPUP_TO.Size = new System.Drawing.Size(22, 22);
            this.GENBA_POPUP_TO.TabIndex = 16;
            this.GENBA_POPUP_TO.TabStop = false;
            this.GENBA_POPUP_TO.Tag = "検索画面を表示します";
            this.GENBA_POPUP_TO.UseVisualStyleBackColor = false;
            this.GENBA_POPUP_TO.ZeroPaddengFlag = false;
            // 
            // GENBA_POPUP_FROM
            // 
            this.GENBA_POPUP_FROM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.GENBA_POPUP_FROM.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.GENBA_POPUP_FROM.DBFieldsName = null;
            this.GENBA_POPUP_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_POPUP_FROM.DisplayItemName = null;
            this.GENBA_POPUP_FROM.DisplayPopUp = null;
            this.GENBA_POPUP_FROM.ErrorMessage = null;
            this.GENBA_POPUP_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_POPUP_FROM.FocusOutCheckMethod")));
            this.GENBA_POPUP_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.GENBA_POPUP_FROM.GetCodeMasterField = null;
            this.GENBA_POPUP_FROM.Image = global::Shougun.Core.Stock.ZaikoKanriHyo.Properties.Resources.虫眼鏡;
            this.GENBA_POPUP_FROM.ItemDefinedTypes = null;
            this.GENBA_POPUP_FROM.LinkedSettingTextBox = null;
            this.GENBA_POPUP_FROM.LinkedTextBoxs = null;
            this.GENBA_POPUP_FROM.Location = new System.Drawing.Point(346, 7);
            this.GENBA_POPUP_FROM.Name = "GENBA_POPUP_FROM";
            this.GENBA_POPUP_FROM.PopupAfterExecute = null;
            this.GENBA_POPUP_FROM.PopupAfterExecuteMethod = "genbaCdFromPopupAfter";
            this.GENBA_POPUP_FROM.PopupBeforeExecute = null;
            this.GENBA_POPUP_FROM.PopupBeforeExecuteMethod = "genbaCdFromPopupBefore";
            this.GENBA_POPUP_FROM.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU";
            this.GENBA_POPUP_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_POPUP_FROM.PopupSearchSendParams")));
            this.GENBA_POPUP_FROM.PopupSetFormField = "GENBA_CD_FROM,GENBA_NAME_FROM";
            this.GENBA_POPUP_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_POPUP_FROM.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_POPUP_FROM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_POPUP_FROM.popupWindowSetting")));
            this.GENBA_POPUP_FROM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_POPUP_FROM.RegistCheckMethod")));
            this.GENBA_POPUP_FROM.SearchDisplayFlag = 0;
            this.GENBA_POPUP_FROM.SetFormField = "GENBA_CD_FROM,GENBA_NAME_FROM";
            this.GENBA_POPUP_FROM.ShortItemName = null;
            this.GENBA_POPUP_FROM.Size = new System.Drawing.Size(22, 22);
            this.GENBA_POPUP_FROM.TabIndex = 13;
            this.GENBA_POPUP_FROM.TabStop = false;
            this.GENBA_POPUP_FROM.Tag = "検索画面を表示します";
            this.GENBA_POPUP_FROM.UseVisualStyleBackColor = false;
            this.GENBA_POPUP_FROM.ZeroPaddengFlag = false;
            // 
            // GENBA_CD_TO
            // 
            this.GENBA_CD_TO.BackColor = System.Drawing.SystemColors.Window;
            this.GENBA_CD_TO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_CD_TO.ChangeUpperCase = true;
            this.GENBA_CD_TO.CharacterLimitList = null;
            this.GENBA_CD_TO.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.GENBA_CD_TO.DBFieldsName = "GENBA_CD";
            this.GENBA_CD_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_CD_TO.DisplayItemName = "現場CD";
            this.GENBA_CD_TO.DisplayPopUp = null;
            this.GENBA_CD_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD_TO.FocusOutCheckMethod")));
            this.GENBA_CD_TO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GENBA_CD_TO.ForeColor = System.Drawing.Color.Black;
            this.GENBA_CD_TO.GetCodeMasterField = "GENBA_CD,GENBA_NAME_RYAKU";
            this.GENBA_CD_TO.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GENBA_CD_TO.IsInputErrorOccured = false;
            this.GENBA_CD_TO.ItemDefinedTypes = "varchar";
            this.GENBA_CD_TO.Location = new System.Drawing.Point(406, 8);
            this.GENBA_CD_TO.MaxLength = 6;
            this.GENBA_CD_TO.Name = "GENBA_CD_TO";
            this.GENBA_CD_TO.PopupAfterExecute = null;
            this.GENBA_CD_TO.PopupAfterExecuteMethod = "genbaCdToPopupAfter";
            this.GENBA_CD_TO.PopupBeforeExecute = null;
            this.GENBA_CD_TO.PopupBeforeExecuteMethod = "genbaCdToPopupBefore";
            this.GENBA_CD_TO.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU";
            this.GENBA_CD_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_CD_TO.PopupSearchSendParams")));
            this.GENBA_CD_TO.PopupSetFormField = "GENBA_CD_TO,GENBA_NAME_TO";
            this.GENBA_CD_TO.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_CD_TO.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_CD_TO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_CD_TO.popupWindowSetting")));
            this.GENBA_CD_TO.prevText = null;
            this.GENBA_CD_TO.PrevText = null;
            this.GENBA_CD_TO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD_TO.RegistCheckMethod")));
            this.GENBA_CD_TO.SetFormField = "GENBA_CD_TO,GENBA_NAME_TO";
            this.GENBA_CD_TO.ShortItemName = "現場CD";
            this.GENBA_CD_TO.Size = new System.Drawing.Size(53, 20);
            this.GENBA_CD_TO.TabIndex = 14;
            this.GENBA_CD_TO.Tag = "現場Toを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GENBA_CD_TO.ZeroPaddengFlag = true;
            this.GENBA_CD_TO.Enter += new System.EventHandler(this.GENBA_CD_TO_Enter);
            this.GENBA_CD_TO.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GenbaToDoubleClick);
            this.GENBA_CD_TO.Validated += new System.EventHandler(this.GENBA_CD_TO_Validated);
            // 
            // GENBA_NAME_TO
            // 
            this.GENBA_NAME_TO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GENBA_NAME_TO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_NAME_TO.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.GENBA_NAME_TO.DBFieldsName = "GENBA_NAME_RYAKU";
            this.GENBA_NAME_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_NAME_TO.DisplayPopUp = null;
            this.GENBA_NAME_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_TO.FocusOutCheckMethod")));
            this.GENBA_NAME_TO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GENBA_NAME_TO.ForeColor = System.Drawing.Color.Black;
            this.GENBA_NAME_TO.IsInputErrorOccured = false;
            this.GENBA_NAME_TO.Location = new System.Drawing.Point(458, 8);
            this.GENBA_NAME_TO.MaxLength = 20;
            this.GENBA_NAME_TO.Name = "GENBA_NAME_TO";
            this.GENBA_NAME_TO.PopupAfterExecute = null;
            this.GENBA_NAME_TO.PopupBeforeExecute = null;
            this.GENBA_NAME_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_NAME_TO.PopupSearchSendParams")));
            this.GENBA_NAME_TO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_NAME_TO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_NAME_TO.popupWindowSetting")));
            this.GENBA_NAME_TO.prevText = null;
            this.GENBA_NAME_TO.PrevText = null;
            this.GENBA_NAME_TO.ReadOnly = true;
            this.GENBA_NAME_TO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_TO.RegistCheckMethod")));
            this.GENBA_NAME_TO.Size = new System.Drawing.Size(286, 20);
            this.GENBA_NAME_TO.TabIndex = 15;
            this.GENBA_NAME_TO.TabStop = false;
            this.GENBA_NAME_TO.Tag = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label2.Location = new System.Drawing.Point(377, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 599;
            this.label2.Text = "～";
            // 
            // GENBA_CD_FROM
            // 
            this.GENBA_CD_FROM.BackColor = System.Drawing.SystemColors.Window;
            this.GENBA_CD_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_CD_FROM.ChangeUpperCase = true;
            this.GENBA_CD_FROM.CharacterLimitList = null;
            this.GENBA_CD_FROM.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.GENBA_CD_FROM.DBFieldsName = "GENBA_CD";
            this.GENBA_CD_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_CD_FROM.DisplayItemName = "現場CD";
            this.GENBA_CD_FROM.DisplayPopUp = null;
            this.GENBA_CD_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD_FROM.FocusOutCheckMethod")));
            this.GENBA_CD_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GENBA_CD_FROM.ForeColor = System.Drawing.Color.Black;
            this.GENBA_CD_FROM.GetCodeMasterField = "GENBA_CD,GENBA_NAME_RYAKU";
            this.GENBA_CD_FROM.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GENBA_CD_FROM.IsInputErrorOccured = false;
            this.GENBA_CD_FROM.ItemDefinedTypes = "varchar";
            this.GENBA_CD_FROM.Location = new System.Drawing.Point(5, 8);
            this.GENBA_CD_FROM.MaxLength = 6;
            this.GENBA_CD_FROM.Name = "GENBA_CD_FROM";
            this.GENBA_CD_FROM.PopupAfterExecute = null;
            this.GENBA_CD_FROM.PopupAfterExecuteMethod = "genbaCdFromPopupAfter";
            this.GENBA_CD_FROM.PopupBeforeExecute = null;
            this.GENBA_CD_FROM.PopupBeforeExecuteMethod = "genbaCdFromPopupBefore";
            this.GENBA_CD_FROM.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU";
            this.GENBA_CD_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_CD_FROM.PopupSearchSendParams")));
            this.GENBA_CD_FROM.PopupSetFormField = "GENBA_CD_FROM,GENBA_NAME_FROM";
            this.GENBA_CD_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_CD_FROM.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_CD_FROM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_CD_FROM.popupWindowSetting")));
            this.GENBA_CD_FROM.prevText = null;
            this.GENBA_CD_FROM.PrevText = null;
            this.GENBA_CD_FROM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD_FROM.RegistCheckMethod")));
            this.GENBA_CD_FROM.SetFormField = "GENBA_CD_FROM,GENBA_NAME_FROM";
            this.GENBA_CD_FROM.ShortItemName = "現場CD";
            this.GENBA_CD_FROM.Size = new System.Drawing.Size(53, 20);
            this.GENBA_CD_FROM.TabIndex = 11;
            this.GENBA_CD_FROM.Tag = "現場Fromを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GENBA_CD_FROM.ZeroPaddengFlag = true;
            this.GENBA_CD_FROM.Enter += new System.EventHandler(this.GENBA_CD_FROM_Enter);
            this.GENBA_CD_FROM.Validated += new System.EventHandler(this.GENBA_CD_FROM_Validated);
            // 
            // GENBA_NAME_FROM
            // 
            this.GENBA_NAME_FROM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GENBA_NAME_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_NAME_FROM.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.GENBA_NAME_FROM.DBFieldsName = "GENBA_NAME_RYAKU";
            this.GENBA_NAME_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_NAME_FROM.DisplayPopUp = null;
            this.GENBA_NAME_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_FROM.FocusOutCheckMethod")));
            this.GENBA_NAME_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GENBA_NAME_FROM.ForeColor = System.Drawing.Color.Black;
            this.GENBA_NAME_FROM.IsInputErrorOccured = false;
            this.GENBA_NAME_FROM.Location = new System.Drawing.Point(57, 8);
            this.GENBA_NAME_FROM.MaxLength = 20;
            this.GENBA_NAME_FROM.Name = "GENBA_NAME_FROM";
            this.GENBA_NAME_FROM.PopupAfterExecute = null;
            this.GENBA_NAME_FROM.PopupBeforeExecute = null;
            this.GENBA_NAME_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_NAME_FROM.PopupSearchSendParams")));
            this.GENBA_NAME_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_NAME_FROM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_NAME_FROM.popupWindowSetting")));
            this.GENBA_NAME_FROM.prevText = null;
            this.GENBA_NAME_FROM.PrevText = null;
            this.GENBA_NAME_FROM.ReadOnly = true;
            this.GENBA_NAME_FROM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_FROM.RegistCheckMethod")));
            this.GENBA_NAME_FROM.Size = new System.Drawing.Size(286, 20);
            this.GENBA_NAME_FROM.TabIndex = 12;
            this.GENBA_NAME_FROM.TabStop = false;
            this.GENBA_NAME_FROM.Tag = "";
            // 
            // lbl_genba
            // 
            this.lbl_genba.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_genba.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_genba.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_genba.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_genba.ForeColor = System.Drawing.Color.White;
            this.lbl_genba.Location = new System.Drawing.Point(12, 197);
            this.lbl_genba.Name = "lbl_genba";
            this.lbl_genba.Size = new System.Drawing.Size(110, 20);
            this.lbl_genba.TabIndex = 507;
            this.lbl_genba.Text = "現場";
            this.lbl_genba.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel5
            // 
            this.customPanel5.Controls.Add(this.ZAIKO_HINMEI_POPUP_TO);
            this.customPanel5.Controls.Add(this.ZAIKO_HINMEI_POPUP_FROM);
            this.customPanel5.Controls.Add(this.ZAIKO_HINMEI_CD_TO);
            this.customPanel5.Controls.Add(this.ZAIKO_HINMEI_NAME_TO);
            this.customPanel5.Controls.Add(this.label4);
            this.customPanel5.Controls.Add(this.ZAIKO_HINMEI_CD_FROM);
            this.customPanel5.Controls.Add(this.ZAIKO_HINMEI_NAME_FROM);
            this.customPanel5.Location = new System.Drawing.Point(122, 220);
            this.customPanel5.Name = "customPanel5";
            this.customPanel5.Size = new System.Drawing.Size(528, 36);
            this.customPanel5.TabIndex = 508;
            // 
            // ZAIKO_HINMEI_POPUP_TO
            // 
            this.ZAIKO_HINMEI_POPUP_TO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.ZAIKO_HINMEI_POPUP_TO.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ZAIKO_HINMEI_POPUP_TO.DBFieldsName = null;
            this.ZAIKO_HINMEI_POPUP_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.ZAIKO_HINMEI_POPUP_TO.DisplayItemName = null;
            this.ZAIKO_HINMEI_POPUP_TO.DisplayPopUp = null;
            this.ZAIKO_HINMEI_POPUP_TO.ErrorMessage = null;
            this.ZAIKO_HINMEI_POPUP_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_POPUP_TO.FocusOutCheckMethod")));
            this.ZAIKO_HINMEI_POPUP_TO.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.ZAIKO_HINMEI_POPUP_TO.GetCodeMasterField = null;
            this.ZAIKO_HINMEI_POPUP_TO.Image = global::Shougun.Core.Stock.ZaikoKanriHyo.Properties.Resources.虫眼鏡;
            this.ZAIKO_HINMEI_POPUP_TO.ItemDefinedTypes = null;
            this.ZAIKO_HINMEI_POPUP_TO.LinkedSettingTextBox = null;
            this.ZAIKO_HINMEI_POPUP_TO.LinkedTextBoxs = null;
            this.ZAIKO_HINMEI_POPUP_TO.Location = new System.Drawing.Point(491, 7);
            this.ZAIKO_HINMEI_POPUP_TO.Name = "ZAIKO_HINMEI_POPUP_TO";
            this.ZAIKO_HINMEI_POPUP_TO.PopupAfterExecute = null;
            this.ZAIKO_HINMEI_POPUP_TO.PopupAfterExecuteMethod = "zaikoHinmeiCdFromPopupAfter";
            this.ZAIKO_HINMEI_POPUP_TO.PopupBeforeExecute = null;
            this.ZAIKO_HINMEI_POPUP_TO.PopupBeforeExecuteMethod = "zaikoHinmeiCdFromPopupBefore";
            this.ZAIKO_HINMEI_POPUP_TO.PopupGetMasterField = "ZAIKO_HINMEI_CD,ZAIKO_HINMEI_NAME_RYAKU";
            this.ZAIKO_HINMEI_POPUP_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZAIKO_HINMEI_POPUP_TO.PopupSearchSendParams")));
            this.ZAIKO_HINMEI_POPUP_TO.PopupSetFormField = "ZAIKO_HINMEI_CD_TO,ZAIKO_HINMEI_NAME_TO";
            this.ZAIKO_HINMEI_POPUP_TO.PopupWindowId = r_framework.Const.WINDOW_ID.M_ZAIKO_HINMEI;
            this.ZAIKO_HINMEI_POPUP_TO.PopupWindowName = "マスタ共通ポップアップ";
            this.ZAIKO_HINMEI_POPUP_TO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZAIKO_HINMEI_POPUP_TO.popupWindowSetting")));
            this.ZAIKO_HINMEI_POPUP_TO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_POPUP_TO.RegistCheckMethod")));
            this.ZAIKO_HINMEI_POPUP_TO.SearchDisplayFlag = 0;
            this.ZAIKO_HINMEI_POPUP_TO.SetFormField = "ZAIKO_HINMEI_CD_TO,ZAIKO_HINMEI_NAME_TO";
            this.ZAIKO_HINMEI_POPUP_TO.ShortItemName = null;
            this.ZAIKO_HINMEI_POPUP_TO.Size = new System.Drawing.Size(22, 22);
            this.ZAIKO_HINMEI_POPUP_TO.TabIndex = 16;
            this.ZAIKO_HINMEI_POPUP_TO.TabStop = false;
            this.ZAIKO_HINMEI_POPUP_TO.Tag = "検索画面を表示します";
            this.ZAIKO_HINMEI_POPUP_TO.UseVisualStyleBackColor = false;
            this.ZAIKO_HINMEI_POPUP_TO.ZeroPaddengFlag = false;
            // 
            // ZAIKO_HINMEI_POPUP_FROM
            // 
            this.ZAIKO_HINMEI_POPUP_FROM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.ZAIKO_HINMEI_POPUP_FROM.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ZAIKO_HINMEI_POPUP_FROM.DBFieldsName = null;
            this.ZAIKO_HINMEI_POPUP_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.ZAIKO_HINMEI_POPUP_FROM.DisplayItemName = null;
            this.ZAIKO_HINMEI_POPUP_FROM.DisplayPopUp = null;
            this.ZAIKO_HINMEI_POPUP_FROM.ErrorMessage = null;
            this.ZAIKO_HINMEI_POPUP_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_POPUP_FROM.FocusOutCheckMethod")));
            this.ZAIKO_HINMEI_POPUP_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.ZAIKO_HINMEI_POPUP_FROM.GetCodeMasterField = null;
            this.ZAIKO_HINMEI_POPUP_FROM.Image = global::Shougun.Core.Stock.ZaikoKanriHyo.Properties.Resources.虫眼鏡;
            this.ZAIKO_HINMEI_POPUP_FROM.ItemDefinedTypes = null;
            this.ZAIKO_HINMEI_POPUP_FROM.LinkedSettingTextBox = null;
            this.ZAIKO_HINMEI_POPUP_FROM.LinkedTextBoxs = null;
            this.ZAIKO_HINMEI_POPUP_FROM.Location = new System.Drawing.Point(220, 7);
            this.ZAIKO_HINMEI_POPUP_FROM.Name = "ZAIKO_HINMEI_POPUP_FROM";
            this.ZAIKO_HINMEI_POPUP_FROM.PopupAfterExecute = null;
            this.ZAIKO_HINMEI_POPUP_FROM.PopupAfterExecuteMethod = "zaikoHinmeiCdFromPopupAfter";
            this.ZAIKO_HINMEI_POPUP_FROM.PopupBeforeExecute = null;
            this.ZAIKO_HINMEI_POPUP_FROM.PopupBeforeExecuteMethod = "zaikoHinmeiCdFromPopupBefore";
            this.ZAIKO_HINMEI_POPUP_FROM.PopupGetMasterField = "ZAIKO_HINMEI_CD,ZAIKO_HINMEI_NAME_RYAKU";
            this.ZAIKO_HINMEI_POPUP_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZAIKO_HINMEI_POPUP_FROM.PopupSearchSendParams")));
            this.ZAIKO_HINMEI_POPUP_FROM.PopupSetFormField = "ZAIKO_HINMEI_CD_FROM,ZAIKO_HINMEI_NAME_FROM";
            this.ZAIKO_HINMEI_POPUP_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.M_ZAIKO_HINMEI;
            this.ZAIKO_HINMEI_POPUP_FROM.PopupWindowName = "マスタ共通ポップアップ";
            this.ZAIKO_HINMEI_POPUP_FROM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZAIKO_HINMEI_POPUP_FROM.popupWindowSetting")));
            this.ZAIKO_HINMEI_POPUP_FROM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_POPUP_FROM.RegistCheckMethod")));
            this.ZAIKO_HINMEI_POPUP_FROM.SearchDisplayFlag = 0;
            this.ZAIKO_HINMEI_POPUP_FROM.SetFormField = "ZAIKO_HINMEI_CD_FROM,ZAIKO_HINMEI_NAME_FROM";
            this.ZAIKO_HINMEI_POPUP_FROM.ShortItemName = null;
            this.ZAIKO_HINMEI_POPUP_FROM.Size = new System.Drawing.Size(22, 22);
            this.ZAIKO_HINMEI_POPUP_FROM.TabIndex = 13;
            this.ZAIKO_HINMEI_POPUP_FROM.TabStop = false;
            this.ZAIKO_HINMEI_POPUP_FROM.Tag = "検索画面を表示します";
            this.ZAIKO_HINMEI_POPUP_FROM.UseVisualStyleBackColor = false;
            this.ZAIKO_HINMEI_POPUP_FROM.ZeroPaddengFlag = false;
            // 
            // ZAIKO_HINMEI_CD_TO
            // 
            this.ZAIKO_HINMEI_CD_TO.BackColor = System.Drawing.SystemColors.Window;
            this.ZAIKO_HINMEI_CD_TO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ZAIKO_HINMEI_CD_TO.ChangeUpperCase = true;
            this.ZAIKO_HINMEI_CD_TO.CharacterLimitList = null;
            this.ZAIKO_HINMEI_CD_TO.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.ZAIKO_HINMEI_CD_TO.DBFieldsName = "ZAIKO_HINMEI_CD";
            this.ZAIKO_HINMEI_CD_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.ZAIKO_HINMEI_CD_TO.DisplayItemName = "在庫品名CD";
            this.ZAIKO_HINMEI_CD_TO.DisplayPopUp = null;
            this.ZAIKO_HINMEI_CD_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_CD_TO.FocusOutCheckMethod")));
            this.ZAIKO_HINMEI_CD_TO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ZAIKO_HINMEI_CD_TO.ForeColor = System.Drawing.Color.Black;
            this.ZAIKO_HINMEI_CD_TO.GetCodeMasterField = "ZAIKO_HINMEI_CD,ZAIKO_HINMEI_NAME_RYAKU";
            this.ZAIKO_HINMEI_CD_TO.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ZAIKO_HINMEI_CD_TO.IsInputErrorOccured = false;
            this.ZAIKO_HINMEI_CD_TO.ItemDefinedTypes = "varchar";
            this.ZAIKO_HINMEI_CD_TO.Location = new System.Drawing.Point(276, 8);
            this.ZAIKO_HINMEI_CD_TO.MaxLength = 6;
            this.ZAIKO_HINMEI_CD_TO.Name = "ZAIKO_HINMEI_CD_TO";
            this.ZAIKO_HINMEI_CD_TO.PopupAfterExecute = null;
            this.ZAIKO_HINMEI_CD_TO.PopupAfterExecuteMethod = "zaikoHinmeiCdToPopupAfter";
            this.ZAIKO_HINMEI_CD_TO.PopupBeforeExecute = null;
            this.ZAIKO_HINMEI_CD_TO.PopupBeforeExecuteMethod = "zaikoHinmeiCdToPopupBefore";
            this.ZAIKO_HINMEI_CD_TO.PopupGetMasterField = "ZAIKO_HINMEI_CD,ZAIKO_HINMEI_NAME_RYAKU";
            this.ZAIKO_HINMEI_CD_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZAIKO_HINMEI_CD_TO.PopupSearchSendParams")));
            this.ZAIKO_HINMEI_CD_TO.PopupSetFormField = "ZAIKO_HINMEI_CD_TO,ZAIKO_HINMEI_NAME_TO";
            this.ZAIKO_HINMEI_CD_TO.PopupWindowId = r_framework.Const.WINDOW_ID.M_ZAIKO_HINMEI;
            this.ZAIKO_HINMEI_CD_TO.PopupWindowName = "マスタ共通ポップアップ";
            this.ZAIKO_HINMEI_CD_TO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZAIKO_HINMEI_CD_TO.popupWindowSetting")));
            this.ZAIKO_HINMEI_CD_TO.prevText = null;
            this.ZAIKO_HINMEI_CD_TO.PrevText = null;
            this.ZAIKO_HINMEI_CD_TO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_CD_TO.RegistCheckMethod")));
            this.ZAIKO_HINMEI_CD_TO.SetFormField = "ZAIKO_HINMEI_CD_TO,ZAIKO_HINMEI_NAME_TO";
            this.ZAIKO_HINMEI_CD_TO.ShortItemName = "在庫品名CD";
            this.ZAIKO_HINMEI_CD_TO.Size = new System.Drawing.Size(53, 20);
            this.ZAIKO_HINMEI_CD_TO.TabIndex = 14;
            this.ZAIKO_HINMEI_CD_TO.Tag = "在庫品名Toを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.ZAIKO_HINMEI_CD_TO.ZeroPaddengFlag = true;
            this.ZAIKO_HINMEI_CD_TO.Enter += new System.EventHandler(this.ZAIKO_HINMEI_CD_TO_Enter);
            this.ZAIKO_HINMEI_CD_TO.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ZaikoHinmeiToDoubleClick);
            this.ZAIKO_HINMEI_CD_TO.Validated += new System.EventHandler(this.ZAIKO_HINMEI_CD_TO_Validated);
            // 
            // ZAIKO_HINMEI_NAME_TO
            // 
            this.ZAIKO_HINMEI_NAME_TO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ZAIKO_HINMEI_NAME_TO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ZAIKO_HINMEI_NAME_TO.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ZAIKO_HINMEI_NAME_TO.DBFieldsName = "ZAIKO_HINMEI_NAME_RYAKU";
            this.ZAIKO_HINMEI_NAME_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.ZAIKO_HINMEI_NAME_TO.DisplayPopUp = null;
            this.ZAIKO_HINMEI_NAME_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_NAME_TO.FocusOutCheckMethod")));
            this.ZAIKO_HINMEI_NAME_TO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ZAIKO_HINMEI_NAME_TO.ForeColor = System.Drawing.Color.Black;
            this.ZAIKO_HINMEI_NAME_TO.IsInputErrorOccured = false;
            this.ZAIKO_HINMEI_NAME_TO.Location = new System.Drawing.Point(328, 8);
            this.ZAIKO_HINMEI_NAME_TO.MaxLength = 20;
            this.ZAIKO_HINMEI_NAME_TO.Name = "ZAIKO_HINMEI_NAME_TO";
            this.ZAIKO_HINMEI_NAME_TO.PopupAfterExecute = null;
            this.ZAIKO_HINMEI_NAME_TO.PopupBeforeExecute = null;
            this.ZAIKO_HINMEI_NAME_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZAIKO_HINMEI_NAME_TO.PopupSearchSendParams")));
            this.ZAIKO_HINMEI_NAME_TO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ZAIKO_HINMEI_NAME_TO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZAIKO_HINMEI_NAME_TO.popupWindowSetting")));
            this.ZAIKO_HINMEI_NAME_TO.prevText = null;
            this.ZAIKO_HINMEI_NAME_TO.PrevText = null;
            this.ZAIKO_HINMEI_NAME_TO.ReadOnly = true;
            this.ZAIKO_HINMEI_NAME_TO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_NAME_TO.RegistCheckMethod")));
            this.ZAIKO_HINMEI_NAME_TO.Size = new System.Drawing.Size(160, 20);
            this.ZAIKO_HINMEI_NAME_TO.TabIndex = 15;
            this.ZAIKO_HINMEI_NAME_TO.TabStop = false;
            this.ZAIKO_HINMEI_NAME_TO.Tag = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label4.Location = new System.Drawing.Point(247, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 599;
            this.label4.Text = "～";
            // 
            // ZAIKO_HINMEI_CD_FROM
            // 
            this.ZAIKO_HINMEI_CD_FROM.BackColor = System.Drawing.SystemColors.Window;
            this.ZAIKO_HINMEI_CD_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ZAIKO_HINMEI_CD_FROM.ChangeUpperCase = true;
            this.ZAIKO_HINMEI_CD_FROM.CharacterLimitList = null;
            this.ZAIKO_HINMEI_CD_FROM.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.ZAIKO_HINMEI_CD_FROM.DBFieldsName = "ZAIKO_HINMEI_CD";
            this.ZAIKO_HINMEI_CD_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.ZAIKO_HINMEI_CD_FROM.DisplayItemName = "在庫品名CD";
            this.ZAIKO_HINMEI_CD_FROM.DisplayPopUp = null;
            this.ZAIKO_HINMEI_CD_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_CD_FROM.FocusOutCheckMethod")));
            this.ZAIKO_HINMEI_CD_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ZAIKO_HINMEI_CD_FROM.ForeColor = System.Drawing.Color.Black;
            this.ZAIKO_HINMEI_CD_FROM.GetCodeMasterField = "ZAIKO_HINMEI_CD,ZAIKO_HINMEI_NAME_RYAKU";
            this.ZAIKO_HINMEI_CD_FROM.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ZAIKO_HINMEI_CD_FROM.IsInputErrorOccured = false;
            this.ZAIKO_HINMEI_CD_FROM.ItemDefinedTypes = "varchar";
            this.ZAIKO_HINMEI_CD_FROM.Location = new System.Drawing.Point(5, 8);
            this.ZAIKO_HINMEI_CD_FROM.MaxLength = 6;
            this.ZAIKO_HINMEI_CD_FROM.Name = "ZAIKO_HINMEI_CD_FROM";
            this.ZAIKO_HINMEI_CD_FROM.PopupAfterExecute = null;
            this.ZAIKO_HINMEI_CD_FROM.PopupAfterExecuteMethod = "zaikoHinmeiCdFromPopupAfter";
            this.ZAIKO_HINMEI_CD_FROM.PopupBeforeExecute = null;
            this.ZAIKO_HINMEI_CD_FROM.PopupBeforeExecuteMethod = "zaikoHinmeiCdFromPopupBefore";
            this.ZAIKO_HINMEI_CD_FROM.PopupGetMasterField = "ZAIKO_HINMEI_CD,ZAIKO_HINMEI_NAME_RYAKU";
            this.ZAIKO_HINMEI_CD_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZAIKO_HINMEI_CD_FROM.PopupSearchSendParams")));
            this.ZAIKO_HINMEI_CD_FROM.PopupSetFormField = "ZAIKO_HINMEI_CD_FROM,ZAIKO_HINMEI_NAME_FROM";
            this.ZAIKO_HINMEI_CD_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.M_ZAIKO_HINMEI;
            this.ZAIKO_HINMEI_CD_FROM.PopupWindowName = "マスタ共通ポップアップ";
            this.ZAIKO_HINMEI_CD_FROM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZAIKO_HINMEI_CD_FROM.popupWindowSetting")));
            this.ZAIKO_HINMEI_CD_FROM.prevText = null;
            this.ZAIKO_HINMEI_CD_FROM.PrevText = null;
            this.ZAIKO_HINMEI_CD_FROM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_CD_FROM.RegistCheckMethod")));
            this.ZAIKO_HINMEI_CD_FROM.SetFormField = "ZAIKO_HINMEI_CD_FROM,ZAIKO_HINMEI_NAME_FROM";
            this.ZAIKO_HINMEI_CD_FROM.ShortItemName = "在庫品名CD";
            this.ZAIKO_HINMEI_CD_FROM.Size = new System.Drawing.Size(53, 20);
            this.ZAIKO_HINMEI_CD_FROM.TabIndex = 11;
            this.ZAIKO_HINMEI_CD_FROM.Tag = "在庫品名Fromを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.ZAIKO_HINMEI_CD_FROM.ZeroPaddengFlag = true;
            this.ZAIKO_HINMEI_CD_FROM.Enter += new System.EventHandler(this.ZAIKO_HINMEI_CD_FROM_Enter);
            this.ZAIKO_HINMEI_CD_FROM.Validated += new System.EventHandler(this.ZAIKO_HINMEI_CD_FROM_Validated);
            // 
            // ZAIKO_HINMEI_NAME_FROM
            // 
            this.ZAIKO_HINMEI_NAME_FROM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ZAIKO_HINMEI_NAME_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ZAIKO_HINMEI_NAME_FROM.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ZAIKO_HINMEI_NAME_FROM.DBFieldsName = "ZAIKO_HINMEI_NAME_RYAKU";
            this.ZAIKO_HINMEI_NAME_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.ZAIKO_HINMEI_NAME_FROM.DisplayPopUp = null;
            this.ZAIKO_HINMEI_NAME_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_NAME_FROM.FocusOutCheckMethod")));
            this.ZAIKO_HINMEI_NAME_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ZAIKO_HINMEI_NAME_FROM.ForeColor = System.Drawing.Color.Black;
            this.ZAIKO_HINMEI_NAME_FROM.IsInputErrorOccured = false;
            this.ZAIKO_HINMEI_NAME_FROM.Location = new System.Drawing.Point(57, 8);
            this.ZAIKO_HINMEI_NAME_FROM.MaxLength = 20;
            this.ZAIKO_HINMEI_NAME_FROM.Name = "ZAIKO_HINMEI_NAME_FROM";
            this.ZAIKO_HINMEI_NAME_FROM.PopupAfterExecute = null;
            this.ZAIKO_HINMEI_NAME_FROM.PopupBeforeExecute = null;
            this.ZAIKO_HINMEI_NAME_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZAIKO_HINMEI_NAME_FROM.PopupSearchSendParams")));
            this.ZAIKO_HINMEI_NAME_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ZAIKO_HINMEI_NAME_FROM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZAIKO_HINMEI_NAME_FROM.popupWindowSetting")));
            this.ZAIKO_HINMEI_NAME_FROM.prevText = null;
            this.ZAIKO_HINMEI_NAME_FROM.PrevText = null;
            this.ZAIKO_HINMEI_NAME_FROM.ReadOnly = true;
            this.ZAIKO_HINMEI_NAME_FROM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_NAME_FROM.RegistCheckMethod")));
            this.ZAIKO_HINMEI_NAME_FROM.Size = new System.Drawing.Size(160, 20);
            this.ZAIKO_HINMEI_NAME_FROM.TabIndex = 12;
            this.ZAIKO_HINMEI_NAME_FROM.TabStop = false;
            this.ZAIKO_HINMEI_NAME_FROM.Tag = "";
            // 
            // lbl_zaikoHinmei
            // 
            this.lbl_zaikoHinmei.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_zaikoHinmei.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_zaikoHinmei.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_zaikoHinmei.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_zaikoHinmei.ForeColor = System.Drawing.Color.White;
            this.lbl_zaikoHinmei.Location = new System.Drawing.Point(12, 228);
            this.lbl_zaikoHinmei.Name = "lbl_zaikoHinmei";
            this.lbl_zaikoHinmei.Size = new System.Drawing.Size(110, 20);
            this.lbl_zaikoHinmei.TabIndex = 509;
            this.lbl_zaikoHinmei.Text = "在庫品名";
            this.lbl_zaikoHinmei.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(924, 312);
            this.Controls.Add(this.customPanel5);
            this.Controls.Add(this.lbl_zaikoHinmei);
            this.Controls.Add(this.customPanel2);
            this.Controls.Add(this.lbl_genba);
            this.Controls.Add(this.customPanel4);
            this.Controls.Add(this.customPanel3);
            this.Controls.Add(this.customPanel1);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_kensakujikkou);
            this.Controls.Add(this.btn_jigetsu);
            this.Controls.Add(this.btn_zengetsu);
            this.Controls.Add(this.lbl_gyousha);
            this.Controls.Add(this.lbl_hidukehanishitei);
            this.Controls.Add(this.lbl_syuturyokunaiyou);
            this.Controls.Add(this.lbl_hanishitei);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PopupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "在庫管理表 - 範囲条件指定";
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.customPanel3.ResumeLayout(false);
            this.customPanel3.PerformLayout();
            this.customPanel4.ResumeLayout(false);
            this.customPanel4.PerformLayout();
            this.customPanel2.ResumeLayout(false);
            this.customPanel2.PerformLayout();
            this.customPanel5.ResumeLayout(false);
            this.customPanel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lbl_hanishitei;
        public System.Windows.Forms.Label lbl_syuturyokunaiyou;
        public System.Windows.Forms.Label lbl_hidukehanishitei;
        public System.Windows.Forms.Label lbl_gyousha;
        private r_framework.CustomControl.CustomRadioButton rdobtn_gyoushaGenba;
        private r_framework.CustomControl.CustomRadioButton rdobtn_zaikoHinmei;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private r_framework.CustomControl.CustomPanel customPanel1;
        private r_framework.CustomControl.CustomPanel customPanel3;
        private r_framework.CustomControl.CustomPanel customPanel4;
        public r_framework.CustomControl.CustomDateTimePicker DATE_FROM;
        public r_framework.CustomControl.CustomDateTimePicker DATE_TO;
        public r_framework.CustomControl.CustomNumericTextBox2 OUTPUT_KBN;
        public r_framework.CustomControl.CustomButton btn_kensakujikkou;
        public r_framework.CustomControl.CustomButton btn_cancel;
        public r_framework.CustomControl.CustomButton btn_zengetsu;
        public r_framework.CustomControl.CustomButton btn_jigetsu;
        internal r_framework.CustomControl.CustomPopupOpenButton GYOUSHA_POPUP_FROM;
        internal r_framework.CustomControl.CustomPopupOpenButton GYOUSHA_POPUP_TO;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GYOUSHA_CD_FROM;
        internal r_framework.CustomControl.CustomTextBox GYOUSHA_NAME_FROM;
        internal r_framework.CustomControl.CustomTextBox GYOUSHA_NAME_TO;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GYOUSHA_CD_TO;
        private r_framework.CustomControl.CustomPanel customPanel2;
        internal r_framework.CustomControl.CustomPopupOpenButton GENBA_POPUP_TO;
        internal r_framework.CustomControl.CustomPopupOpenButton GENBA_POPUP_FROM;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GENBA_CD_TO;
        internal r_framework.CustomControl.CustomTextBox GENBA_NAME_TO;
        private System.Windows.Forms.Label label2;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GENBA_CD_FROM;
        internal r_framework.CustomControl.CustomTextBox GENBA_NAME_FROM;
        public System.Windows.Forms.Label lbl_genba;
        private r_framework.CustomControl.CustomPanel customPanel5;
        internal r_framework.CustomControl.CustomPopupOpenButton ZAIKO_HINMEI_POPUP_TO;
        internal r_framework.CustomControl.CustomPopupOpenButton ZAIKO_HINMEI_POPUP_FROM;
        internal r_framework.CustomControl.CustomAlphaNumTextBox ZAIKO_HINMEI_CD_TO;
        internal r_framework.CustomControl.CustomTextBox ZAIKO_HINMEI_NAME_TO;
        private System.Windows.Forms.Label label4;
        internal r_framework.CustomControl.CustomAlphaNumTextBox ZAIKO_HINMEI_CD_FROM;
        internal r_framework.CustomControl.CustomTextBox ZAIKO_HINMEI_NAME_FROM;
        public System.Windows.Forms.Label lbl_zaikoHinmei;
    }
}