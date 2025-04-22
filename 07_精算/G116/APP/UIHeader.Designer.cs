namespace Shougun.Core.Adjustment.ShiharaiMeisaishoHakko
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
            this.txtHeaderKyotenCd = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.txtHeaderKyotenMei = new r_framework.CustomControl.CustomTextBox();
            this.lblKyoten = new System.Windows.Forms.Label();
            this.lblDenpyoHizuke = new System.Windows.Forms.Label();
            this.tdpDenpyouHidukeFrom = new r_framework.CustomControl.CustomDateTimePicker();
            this.tdpDenpyouHidukeTo = new r_framework.CustomControl.CustomDateTimePicker();
            this.lblYomikomiDataNum = new System.Windows.Forms.Label();
            this.lblAlertNum = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAlertNum = new r_framework.CustomControl.CustomNumericTextBox2();
            this.txtYomikomiDataNum = new r_framework.CustomControl.CustomNumericTextBox2();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ZeiRate_Chk = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Size = new System.Drawing.Size(10, 10);
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(3, 6);
            this.lb_title.Size = new System.Drawing.Size(245, 35);
            this.lb_title.Text = "支払明細書発行";
            // 
            // txtHeaderKyotenCd
            // 
            this.txtHeaderKyotenCd.AlphabetLimitFlag = false;
            this.txtHeaderKyotenCd.BackColor = System.Drawing.SystemColors.Window;
            this.txtHeaderKyotenCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHeaderKyotenCd.CharacterLimitList = null;
            this.txtHeaderKyotenCd.CharactersNumber = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.txtHeaderKyotenCd.DBFieldsName = "KYOTEN_CD";
            this.txtHeaderKyotenCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtHeaderKyotenCd.DisplayItemName = "拠点CD";
            this.txtHeaderKyotenCd.DisplayPopUp = null;
            this.txtHeaderKyotenCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtHeaderKyotenCd.FocusOutCheckMethod")));
            this.txtHeaderKyotenCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtHeaderKyotenCd.ForeColor = System.Drawing.Color.Black;
            this.txtHeaderKyotenCd.GetCodeMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.txtHeaderKyotenCd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtHeaderKyotenCd.IsInputErrorOccured = false;
            this.txtHeaderKyotenCd.ItemDefinedTypes = "";
            this.txtHeaderKyotenCd.Location = new System.Drawing.Point(372, 2);
            this.txtHeaderKyotenCd.MaxLength = 2;
            this.txtHeaderKyotenCd.Name = "txtHeaderKyotenCd";
            this.txtHeaderKyotenCd.PopupAfterExecute = null;
            this.txtHeaderKyotenCd.PopupBeforeExecute = null;
            this.txtHeaderKyotenCd.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.txtHeaderKyotenCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtHeaderKyotenCd.PopupSearchSendParams")));
            this.txtHeaderKyotenCd.PopupSetFormField = "txtHeaderKyotenCd,txtHeaderKyotenMei";
            this.txtHeaderKyotenCd.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.txtHeaderKyotenCd.PopupWindowName = "マスタ共通ポップアップ";
            this.txtHeaderKyotenCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtHeaderKyotenCd.popupWindowSetting")));
            this.txtHeaderKyotenCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtHeaderKyotenCd.RegistCheckMethod")));
            this.txtHeaderKyotenCd.SetFormField = "txtHeaderKyotenCd,txtHeaderKyotenMei";
            this.txtHeaderKyotenCd.ShortItemName = "";
            this.txtHeaderKyotenCd.Size = new System.Drawing.Size(30, 20);
            this.txtHeaderKyotenCd.TabIndex = 101;
            this.txtHeaderKyotenCd.Tag = "";
            this.txtHeaderKyotenCd.ZeroPaddengFlag = true;
            // 
            // txtHeaderKyotenMei
            // 
            this.txtHeaderKyotenMei.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtHeaderKyotenMei.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHeaderKyotenMei.CharactersNumber = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.txtHeaderKyotenMei.DBFieldsName = "";
            this.txtHeaderKyotenMei.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtHeaderKyotenMei.DisplayItemName = "検索条件";
            this.txtHeaderKyotenMei.DisplayPopUp = null;
            this.txtHeaderKyotenMei.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtHeaderKyotenMei.FocusOutCheckMethod")));
            this.txtHeaderKyotenMei.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtHeaderKyotenMei.ForeColor = System.Drawing.Color.Black;
            this.txtHeaderKyotenMei.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtHeaderKyotenMei.IsInputErrorOccured = false;
            this.txtHeaderKyotenMei.Location = new System.Drawing.Point(401, 2);
            this.txtHeaderKyotenMei.Name = "txtHeaderKyotenMei";
            this.txtHeaderKyotenMei.PopupAfterExecute = null;
            this.txtHeaderKyotenMei.PopupBeforeExecute = null;
            this.txtHeaderKyotenMei.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtHeaderKyotenMei.PopupSearchSendParams")));
            this.txtHeaderKyotenMei.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtHeaderKyotenMei.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtHeaderKyotenMei.popupWindowSetting")));
            this.txtHeaderKyotenMei.ReadOnly = true;
            this.txtHeaderKyotenMei.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtHeaderKyotenMei.RegistCheckMethod")));
            this.txtHeaderKyotenMei.Size = new System.Drawing.Size(160, 20);
            this.txtHeaderKyotenMei.TabIndex = 390;
            this.txtHeaderKyotenMei.TabStop = false;
            this.txtHeaderKyotenMei.Tag = "";
            // 
            // lblKyoten
            // 
            this.lblKyoten.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblKyoten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKyoten.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblKyoten.ForeColor = System.Drawing.Color.White;
            this.lblKyoten.Location = new System.Drawing.Point(257, 2);
            this.lblKyoten.Name = "lblKyoten";
            this.lblKyoten.Size = new System.Drawing.Size(110, 20);
            this.lblKyoten.TabIndex = 389;
            this.lblKyoten.Text = "拠点";
            this.lblKyoten.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDenpyoHizuke
            // 
            this.lblDenpyoHizuke.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblDenpyoHizuke.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDenpyoHizuke.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblDenpyoHizuke.ForeColor = System.Drawing.Color.White;
            this.lblDenpyoHizuke.Location = new System.Drawing.Point(566, 24);
            this.lblDenpyoHizuke.Name = "lblDenpyoHizuke";
            this.lblDenpyoHizuke.Size = new System.Drawing.Size(110, 20);
            this.lblDenpyoHizuke.TabIndex = 389;
            this.lblDenpyoHizuke.Text = "精算日付";
            this.lblDenpyoHizuke.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tdpDenpyouHidukeFrom
            // 
            this.tdpDenpyouHidukeFrom.BackColor = System.Drawing.SystemColors.Window;
            this.tdpDenpyouHidukeFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tdpDenpyouHidukeFrom.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.tdpDenpyouHidukeFrom.Checked = false;
            this.tdpDenpyouHidukeFrom.CustomFormat = "yyyy/MM/dd(ddd)";
            this.tdpDenpyouHidukeFrom.DateTimeNowYear = "";
            this.tdpDenpyouHidukeFrom.DefaultBackColor = System.Drawing.Color.Empty;
            this.tdpDenpyouHidukeFrom.DisplayItemName = "開始日付";
            this.tdpDenpyouHidukeFrom.DisplayPopUp = null;
            this.tdpDenpyouHidukeFrom.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("tdpDenpyouHidukeFrom.FocusOutCheckMethod")));
            this.tdpDenpyouHidukeFrom.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.tdpDenpyouHidukeFrom.ForeColor = System.Drawing.Color.Black;
            this.tdpDenpyouHidukeFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.tdpDenpyouHidukeFrom.IsInputErrorOccured = false;
            this.tdpDenpyouHidukeFrom.Location = new System.Drawing.Point(681, 24);
            this.tdpDenpyouHidukeFrom.MaxLength = 10;
            this.tdpDenpyouHidukeFrom.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.tdpDenpyouHidukeFrom.Name = "tdpDenpyouHidukeFrom";
            this.tdpDenpyouHidukeFrom.NullValue = "";
            this.tdpDenpyouHidukeFrom.PopupAfterExecute = null;
            this.tdpDenpyouHidukeFrom.PopupBeforeExecute = null;
            this.tdpDenpyouHidukeFrom.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("tdpDenpyouHidukeFrom.PopupSearchSendParams")));
            this.tdpDenpyouHidukeFrom.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.tdpDenpyouHidukeFrom.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("tdpDenpyouHidukeFrom.popupWindowSetting")));
            this.tdpDenpyouHidukeFrom.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("tdpDenpyouHidukeFrom.RegistCheckMethod")));
            this.tdpDenpyouHidukeFrom.Size = new System.Drawing.Size(138, 20);
            this.tdpDenpyouHidukeFrom.TabIndex = 2;
            this.tdpDenpyouHidukeFrom.Tag = "日付を選択してください";
            this.tdpDenpyouHidukeFrom.Text = "2013/10/30(水)";
            this.tdpDenpyouHidukeFrom.Value = new System.DateTime(2013, 10, 30, 0, 0, 0, 0);
            this.tdpDenpyouHidukeFrom.Leave += new System.EventHandler(this.tdpDenpyouHidukeFrom_Leave);
            // 
            // tdpDenpyouHidukeTo
            // 
            this.tdpDenpyouHidukeTo.BackColor = System.Drawing.SystemColors.Window;
            this.tdpDenpyouHidukeTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tdpDenpyouHidukeTo.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.tdpDenpyouHidukeTo.Checked = false;
            this.tdpDenpyouHidukeTo.CustomFormat = "yyyy/MM/dd(ddd)";
            this.tdpDenpyouHidukeTo.DateTimeNowYear = "";
            this.tdpDenpyouHidukeTo.DefaultBackColor = System.Drawing.Color.Empty;
            this.tdpDenpyouHidukeTo.DisplayItemName = "終了日付";
            this.tdpDenpyouHidukeTo.DisplayPopUp = null;
            this.tdpDenpyouHidukeTo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("tdpDenpyouHidukeTo.FocusOutCheckMethod")));
            this.tdpDenpyouHidukeTo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.tdpDenpyouHidukeTo.ForeColor = System.Drawing.Color.Black;
            this.tdpDenpyouHidukeTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.tdpDenpyouHidukeTo.IsInputErrorOccured = false;
            this.tdpDenpyouHidukeTo.Location = new System.Drawing.Point(839, 24);
            this.tdpDenpyouHidukeTo.MaxLength = 10;
            this.tdpDenpyouHidukeTo.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.tdpDenpyouHidukeTo.Name = "tdpDenpyouHidukeTo";
            this.tdpDenpyouHidukeTo.NullValue = "";
            this.tdpDenpyouHidukeTo.PopupAfterExecute = null;
            this.tdpDenpyouHidukeTo.PopupBeforeExecute = null;
            this.tdpDenpyouHidukeTo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("tdpDenpyouHidukeTo.PopupSearchSendParams")));
            this.tdpDenpyouHidukeTo.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.tdpDenpyouHidukeTo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("tdpDenpyouHidukeTo.popupWindowSetting")));
            this.tdpDenpyouHidukeTo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("tdpDenpyouHidukeTo.RegistCheckMethod")));
            this.tdpDenpyouHidukeTo.Size = new System.Drawing.Size(138, 20);
            this.tdpDenpyouHidukeTo.TabIndex = 3;
            this.tdpDenpyouHidukeTo.Tag = "日付を選択してください";
            this.tdpDenpyouHidukeTo.Text = "2013/10/30(水)";
            this.tdpDenpyouHidukeTo.Value = new System.DateTime(2013, 10, 30, 0, 0, 0, 0);
            this.tdpDenpyouHidukeTo.Leave += new System.EventHandler(this.tdpDenpyouHidukeTo_Leave);
            // 
            // lblYomikomiDataNum
            // 
            this.lblYomikomiDataNum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblYomikomiDataNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblYomikomiDataNum.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblYomikomiDataNum.ForeColor = System.Drawing.Color.White;
            this.lblYomikomiDataNum.Location = new System.Drawing.Point(982, 24);
            this.lblYomikomiDataNum.Name = "lblYomikomiDataNum";
            this.lblYomikomiDataNum.Size = new System.Drawing.Size(110, 20);
            this.lblYomikomiDataNum.TabIndex = 389;
            this.lblYomikomiDataNum.Text = "読込データ件数";
            this.lblYomikomiDataNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAlertNum
            // 
            this.lblAlertNum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblAlertNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAlertNum.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblAlertNum.ForeColor = System.Drawing.Color.White;
            this.lblAlertNum.Location = new System.Drawing.Point(982, 2);
            this.lblAlertNum.Name = "lblAlertNum";
            this.lblAlertNum.Size = new System.Drawing.Size(110, 20);
            this.lblAlertNum.TabIndex = 389;
            this.lblAlertNum.Text = "アラート件数";
            this.lblAlertNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAlertNum.Visible = false;
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
            // txtAlertNum
            // 
            this.txtAlertNum.BackColor = System.Drawing.SystemColors.Window;
            this.txtAlertNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAlertNum.CustomFormatSetting = "#,##0";
            this.txtAlertNum.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtAlertNum.DisplayPopUp = null;
            this.txtAlertNum.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtAlertNum.FocusOutCheckMethod")));
            this.txtAlertNum.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtAlertNum.ForeColor = System.Drawing.Color.Black;
            this.txtAlertNum.FormatSetting = "カスタム";
            this.txtAlertNum.IsInputErrorOccured = false;
            this.txtAlertNum.Location = new System.Drawing.Point(1097, 2);
            this.txtAlertNum.Name = "txtAlertNum";
            this.txtAlertNum.PopupAfterExecute = null;
            this.txtAlertNum.PopupBeforeExecute = null;
            this.txtAlertNum.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtAlertNum.PopupSearchSendParams")));
            this.txtAlertNum.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtAlertNum.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtAlertNum.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.txtAlertNum.RangeSetting = rangeSettingDto1;
            this.txtAlertNum.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtAlertNum.RegistCheckMethod")));
            this.txtAlertNum.Size = new System.Drawing.Size(80, 20);
            this.txtAlertNum.TabIndex = 4;
            this.txtAlertNum.TabStop = false;
            this.txtAlertNum.Tag = "検索結果の総件数でアラートメッセージを表示させたい上限数を入力してください";
            this.txtAlertNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAlertNum.Visible = false;
            this.txtAlertNum.WordWrap = false;
            // 
            // txtYomikomiDataNum
            // 
            this.txtYomikomiDataNum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtYomikomiDataNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtYomikomiDataNum.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtYomikomiDataNum.DisplayPopUp = null;
            this.txtYomikomiDataNum.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtYomikomiDataNum.FocusOutCheckMethod")));
            this.txtYomikomiDataNum.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtYomikomiDataNum.ForeColor = System.Drawing.Color.Black;
            this.txtYomikomiDataNum.IsInputErrorOccured = false;
            this.txtYomikomiDataNum.Location = new System.Drawing.Point(1097, 24);
            this.txtYomikomiDataNum.Name = "txtYomikomiDataNum";
            this.txtYomikomiDataNum.PopupAfterExecute = null;
            this.txtYomikomiDataNum.PopupBeforeExecute = null;
            this.txtYomikomiDataNum.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtYomikomiDataNum.PopupSearchSendParams")));
            this.txtYomikomiDataNum.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtYomikomiDataNum.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtYomikomiDataNum.popupWindowSetting")));
            this.txtYomikomiDataNum.RangeSetting = rangeSettingDto2;
            this.txtYomikomiDataNum.ReadOnly = true;
            this.txtYomikomiDataNum.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtYomikomiDataNum.RegistCheckMethod")));
            this.txtYomikomiDataNum.Size = new System.Drawing.Size(80, 20);
            this.txtYomikomiDataNum.TabIndex = 395;
            this.txtYomikomiDataNum.TabStop = false;
            this.txtYomikomiDataNum.Tag = "検索結果の総件数が表示されます";
            this.txtYomikomiDataNum.Text = "0";
            this.txtYomikomiDataNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtYomikomiDataNum.WordWrap = false;
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
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(560, 4);
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
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 10032;
            this.ISNOT_NEED_DELETE_FLG.TabStop = false;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
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
            this.label2.TabIndex = 10036;
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
            this.ZeiRate_Chk.TabIndex = 10035;
            this.ZeiRate_Chk.Tag = "適格請求書に税率を印刷する場合は、チェックをしてください";
            this.ZeiRate_Chk.Text = "表示";
            this.ZeiRate_Chk.UseVisualStyleBackColor = true;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 46);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ZeiRate_Chk);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.txtYomikomiDataNum);
            this.Controls.Add(this.txtAlertNum);
            this.Controls.Add(this.tdpDenpyouHidukeTo);
            this.Controls.Add(this.tdpDenpyouHidukeFrom);
            this.Controls.Add(this.txtHeaderKyotenCd);
            this.Controls.Add(this.txtHeaderKyotenMei);
            this.Controls.Add(this.lblDenpyoHizuke);
            this.Controls.Add(this.lblAlertNum);
            this.Controls.Add(this.lblYomikomiDataNum);
            this.Controls.Add(this.lblKyoten);
            this.Controls.Add(this.label6);
            this.Name = "UIHeader";
            this.Text = "HeaderShiharaiMeisaishoHakkou";
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.lblKyoten, 0);
            this.Controls.SetChildIndex(this.lblYomikomiDataNum, 0);
            this.Controls.SetChildIndex(this.lblAlertNum, 0);
            this.Controls.SetChildIndex(this.lblDenpyoHizuke, 0);
            this.Controls.SetChildIndex(this.txtHeaderKyotenMei, 0);
            this.Controls.SetChildIndex(this.txtHeaderKyotenCd, 0);
            this.Controls.SetChildIndex(this.tdpDenpyouHidukeFrom, 0);
            this.Controls.SetChildIndex(this.tdpDenpyouHidukeTo, 0);
            this.Controls.SetChildIndex(this.txtAlertNum, 0);
            this.Controls.SetChildIndex(this.txtYomikomiDataNum, 0);
            this.Controls.SetChildIndex(this.ISNOT_NEED_DELETE_FLG, 0);
            this.Controls.SetChildIndex(this.ZeiRate_Chk, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomAlphaNumTextBox txtHeaderKyotenCd;
        public r_framework.CustomControl.CustomTextBox txtHeaderKyotenMei;
        private System.Windows.Forms.Label lblKyoten;
        private System.Windows.Forms.Label lblDenpyoHizuke;
        public r_framework.CustomControl.CustomDateTimePicker tdpDenpyouHidukeFrom;
        public r_framework.CustomControl.CustomDateTimePicker tdpDenpyouHidukeTo;
        private System.Windows.Forms.Label lblYomikomiDataNum;
        private System.Windows.Forms.Label lblAlertNum;
        private System.Windows.Forms.Label label6;
        public r_framework.CustomControl.CustomNumericTextBox2 txtAlertNum;
        public r_framework.CustomControl.CustomNumericTextBox2 txtYomikomiDataNum;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.CheckBox ZeiRate_Chk;
    }
}