namespace Shougun.Core.SalesPayment.HannyushutsuIchiran
{
    /// <summary>
    /// ヘッダクラス
    /// </summary>
    partial class HannyushutsuIchiranHeader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HannyushutsuIchiranHeader));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.txtKyotenNameRyaku = new r_framework.CustomControl.CustomTextBox();
            this.lblKyoten = new System.Windows.Forms.Label();
            this.lblAlertNum = new System.Windows.Forms.Label();
            this.lblYomikomiDataNum = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.lblSagyoDate = new System.Windows.Forms.Label();
            this.txtReadDataCnt = new r_framework.CustomControl.CustomTextBox();
            this.txtAlertNum = new r_framework.CustomControl.CustomNumericTextBox2();
            this.dtpSagyouDateFrom = new r_framework.CustomControl.CustomDateTimePicker();
            this.dtpSagyouDateTo = new r_framework.CustomControl.CustomDateTimePicker();
            this.txtKyotenCd = new r_framework.CustomControl.CustomNumericTextBox2();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Location = new System.Drawing.Point(8, 6);
            this.windowTypeLabel.Size = new System.Drawing.Size(19, 17);
            this.windowTypeLabel.Visible = false;
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(8, 6);
            this.lb_title.Size = new System.Drawing.Size(227, 35);
            this.lb_title.Text = "搬入出予定一覧";
            // 
            // txtKyotenNameRyaku
            // 
            this.txtKyotenNameRyaku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtKyotenNameRyaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKyotenNameRyaku.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txtKyotenNameRyaku.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtKyotenNameRyaku.DisplayPopUp = null;
            this.txtKyotenNameRyaku.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenNameRyaku.FocusOutCheckMethod")));
            this.txtKyotenNameRyaku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtKyotenNameRyaku.ForeColor = System.Drawing.Color.Black;
            this.txtKyotenNameRyaku.IsInputErrorOccured = false;
            this.txtKyotenNameRyaku.Location = new System.Drawing.Point(385, 2);
            this.txtKyotenNameRyaku.MaxLength = 0;
            this.txtKyotenNameRyaku.Name = "txtKyotenNameRyaku";
            this.txtKyotenNameRyaku.PopupAfterExecute = null;
            this.txtKyotenNameRyaku.PopupBeforeExecute = null;
            this.txtKyotenNameRyaku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtKyotenNameRyaku.PopupSearchSendParams")));
            this.txtKyotenNameRyaku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtKyotenNameRyaku.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtKyotenNameRyaku.popupWindowSetting")));
            this.txtKyotenNameRyaku.ReadOnly = true;
            this.txtKyotenNameRyaku.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenNameRyaku.RegistCheckMethod")));
            this.txtKyotenNameRyaku.Size = new System.Drawing.Size(160, 20);
            this.txtKyotenNameRyaku.TabIndex = 534;
            this.txtKyotenNameRyaku.TabStop = false;
            this.txtKyotenNameRyaku.Tag = "  ";
            // 
            // lblKyoten
            // 
            this.lblKyoten.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblKyoten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKyoten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblKyoten.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblKyoten.ForeColor = System.Drawing.Color.White;
            this.lblKyoten.Location = new System.Drawing.Point(241, 2);
            this.lblKyoten.Name = "lblKyoten";
            this.lblKyoten.Size = new System.Drawing.Size(110, 20);
            this.lblKyoten.TabIndex = 532;
            this.lblKyoten.Text = "拠点";
            this.lblKyoten.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAlertNum
            // 
            this.lblAlertNum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblAlertNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAlertNum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblAlertNum.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblAlertNum.ForeColor = System.Drawing.Color.White;
            this.lblAlertNum.Location = new System.Drawing.Point(973, 2);
            this.lblAlertNum.Name = "lblAlertNum";
            this.lblAlertNum.Size = new System.Drawing.Size(110, 20);
            this.lblAlertNum.TabIndex = 530;
            this.lblAlertNum.Text = "アラート件数";
            this.lblAlertNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblYomikomiDataNum
            // 
            this.lblYomikomiDataNum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblYomikomiDataNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblYomikomiDataNum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblYomikomiDataNum.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblYomikomiDataNum.ForeColor = System.Drawing.Color.White;
            this.lblYomikomiDataNum.Location = new System.Drawing.Point(973, 24);
            this.lblYomikomiDataNum.Name = "lblYomikomiDataNum";
            this.lblYomikomiDataNum.Size = new System.Drawing.Size(110, 20);
            this.lblYomikomiDataNum.TabIndex = 528;
            this.lblYomikomiDataNum.Text = "読込データ件数";
            this.lblYomikomiDataNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.Transparent;
            this.label38.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label38.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label38.ForeColor = System.Drawing.Color.Black;
            this.label38.Location = new System.Drawing.Point(807, 24);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(19, 22);
            this.label38.TabIndex = 407;
            this.label38.Text = "～";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSagyoDate
            // 
            this.lblSagyoDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblSagyoDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSagyoDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSagyoDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSagyoDate.ForeColor = System.Drawing.Color.White;
            this.lblSagyoDate.Location = new System.Drawing.Point(553, 24);
            this.lblSagyoDate.Name = "lblSagyoDate";
            this.lblSagyoDate.Size = new System.Drawing.Size(110, 20);
            this.lblSagyoDate.TabIndex = 405;
            this.lblSagyoDate.Text = "作業日";
            this.lblSagyoDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtReadDataCnt
            // 
            this.txtReadDataCnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtReadDataCnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReadDataCnt.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtReadDataCnt.DisplayPopUp = null;
            this.txtReadDataCnt.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtReadDataCnt.FocusOutCheckMethod")));
            this.txtReadDataCnt.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtReadDataCnt.ForeColor = System.Drawing.Color.Black;
            this.txtReadDataCnt.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtReadDataCnt.IsInputErrorOccured = false;
            this.txtReadDataCnt.Location = new System.Drawing.Point(1088, 24);
            this.txtReadDataCnt.Name = "txtReadDataCnt";
            this.txtReadDataCnt.PopupAfterExecute = null;
            this.txtReadDataCnt.PopupBeforeExecute = null;
            this.txtReadDataCnt.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtReadDataCnt.PopupSearchSendParams")));
            this.txtReadDataCnt.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtReadDataCnt.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtReadDataCnt.popupWindowSetting")));
            this.txtReadDataCnt.ReadOnly = true;
            this.txtReadDataCnt.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtReadDataCnt.RegistCheckMethod")));
            this.txtReadDataCnt.Size = new System.Drawing.Size(80, 20);
            this.txtReadDataCnt.TabIndex = 410;
            this.txtReadDataCnt.TabStop = false;
            this.txtReadDataCnt.Tag = "検索結果の総件数が表示されます";
            this.txtReadDataCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtAlertNum
            // 
            this.txtAlertNum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtAlertNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAlertNum.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtAlertNum.DisplayPopUp = null;
            this.txtAlertNum.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtAlertNum.FocusOutCheckMethod")));
            this.txtAlertNum.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtAlertNum.ForeColor = System.Drawing.Color.Black;
            this.txtAlertNum.IsInputErrorOccured = false;
            this.txtAlertNum.Location = new System.Drawing.Point(1088, 2);
            this.txtAlertNum.Name = "txtAlertNum";
            this.txtAlertNum.PopupAfterExecute = null;
            this.txtAlertNum.PopupBeforeExecute = null;
            this.txtAlertNum.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtAlertNum.PopupSearchSendParams")));
            this.txtAlertNum.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtAlertNum.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtAlertNum.popupWindowSetting")));
            this.txtAlertNum.RangeSetting = rangeSettingDto1;
            this.txtAlertNum.ReadOnly = true;
            this.txtAlertNum.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtAlertNum.RegistCheckMethod")));
            this.txtAlertNum.Size = new System.Drawing.Size(80, 20);
            this.txtAlertNum.TabIndex = 539;
            this.txtAlertNum.TabStop = false;
            this.txtAlertNum.Tag = "検索結果の総件数でアラートメッセージを表示させたい上限数を入力してください";
            this.txtAlertNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAlertNum.WordWrap = false;
            // 
            // dtpSagyouDateFrom
            // 
            this.dtpSagyouDateFrom.BackColor = System.Drawing.SystemColors.Window;
            this.dtpSagyouDateFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtpSagyouDateFrom.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.dtpSagyouDateFrom.Checked = false;
            this.dtpSagyouDateFrom.CustomFormat = "yyyy/MM/dd(ddd)";
            this.dtpSagyouDateFrom.DateTimeNowYear = "";
            this.dtpSagyouDateFrom.DefaultBackColor = System.Drawing.Color.Empty;
            this.dtpSagyouDateFrom.DisplayPopUp = null;
            this.dtpSagyouDateFrom.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtpSagyouDateFrom.FocusOutCheckMethod")));
            this.dtpSagyouDateFrom.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.dtpSagyouDateFrom.ForeColor = System.Drawing.Color.Black;
            this.dtpSagyouDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSagyouDateFrom.IsInputErrorOccured = false;
            this.dtpSagyouDateFrom.Location = new System.Drawing.Point(668, 24);
            this.dtpSagyouDateFrom.MaxLength = 10;
            this.dtpSagyouDateFrom.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpSagyouDateFrom.Name = "dtpSagyouDateFrom";
            this.dtpSagyouDateFrom.NullValue = "";
            this.dtpSagyouDateFrom.PopupAfterExecute = null;
            this.dtpSagyouDateFrom.PopupBeforeExecute = null;
            this.dtpSagyouDateFrom.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dtpSagyouDateFrom.PopupSearchSendParams")));
            this.dtpSagyouDateFrom.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dtpSagyouDateFrom.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dtpSagyouDateFrom.popupWindowSetting")));
            this.dtpSagyouDateFrom.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtpSagyouDateFrom.RegistCheckMethod")));
            this.dtpSagyouDateFrom.Size = new System.Drawing.Size(138, 20);
            this.dtpSagyouDateFrom.TabIndex = 3;
            this.dtpSagyouDateFrom.TabStop = false;
            this.dtpSagyouDateFrom.Tag = "作業日FROMを選択してください";
            this.dtpSagyouDateFrom.Text = "2013/11/11(月)";
            this.dtpSagyouDateFrom.Value = new System.DateTime(2013, 11, 11, 0, 0, 0, 0);
            // 
            // dtpSagyouDateTo
            // 
            this.dtpSagyouDateTo.BackColor = System.Drawing.SystemColors.Window;
            this.dtpSagyouDateTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtpSagyouDateTo.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.dtpSagyouDateTo.Checked = false;
            this.dtpSagyouDateTo.CustomFormat = "yyyy/MM/dd(ddd)";
            this.dtpSagyouDateTo.DateTimeNowYear = "";
            this.dtpSagyouDateTo.DefaultBackColor = System.Drawing.Color.Empty;
            this.dtpSagyouDateTo.DisplayPopUp = null;
            this.dtpSagyouDateTo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtpSagyouDateTo.FocusOutCheckMethod")));
            this.dtpSagyouDateTo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.dtpSagyouDateTo.ForeColor = System.Drawing.Color.Black;
            this.dtpSagyouDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSagyouDateTo.IsInputErrorOccured = false;
            this.dtpSagyouDateTo.Location = new System.Drawing.Point(826, 24);
            this.dtpSagyouDateTo.MaxLength = 10;
            this.dtpSagyouDateTo.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpSagyouDateTo.Name = "dtpSagyouDateTo";
            this.dtpSagyouDateTo.NullValue = "";
            this.dtpSagyouDateTo.PopupAfterExecute = null;
            this.dtpSagyouDateTo.PopupBeforeExecute = null;
            this.dtpSagyouDateTo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dtpSagyouDateTo.PopupSearchSendParams")));
            this.dtpSagyouDateTo.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dtpSagyouDateTo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dtpSagyouDateTo.popupWindowSetting")));
            this.dtpSagyouDateTo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtpSagyouDateTo.RegistCheckMethod")));
            this.dtpSagyouDateTo.Size = new System.Drawing.Size(138, 20);
            this.dtpSagyouDateTo.TabIndex = 4;
            this.dtpSagyouDateTo.TabStop = false;
            this.dtpSagyouDateTo.Tag = "作業日TOを選択してください";
            this.dtpSagyouDateTo.Text = "2013/11/11(月)";
            this.dtpSagyouDateTo.Value = new System.DateTime(2013, 11, 11, 0, 0, 0, 0);
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
            this.txtKyotenCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtKyotenCd.ForeColor = System.Drawing.Color.Black;
            this.txtKyotenCd.FormatSetting = "カスタム";
            this.txtKyotenCd.GetCodeMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.txtKyotenCd.IsInputErrorOccured = false;
            this.txtKyotenCd.ItemDefinedTypes = "smallint";
            this.txtKyotenCd.Location = new System.Drawing.Point(356, 2);
            this.txtKyotenCd.Name = "txtKyotenCd";
            this.txtKyotenCd.PopupAfterExecute = null;
            this.txtKyotenCd.PopupBeforeExecute = null;
            this.txtKyotenCd.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.txtKyotenCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtKyotenCd.PopupSearchSendParams")));
            this.txtKyotenCd.PopupSetFormField = "txtKyotenCd,txtKyotenNameRyaku";
            this.txtKyotenCd.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.txtKyotenCd.PopupWindowName = "マスタ共通ポップアップ";
            this.txtKyotenCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtKyotenCd.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.txtKyotenCd.RangeSetting = rangeSettingDto2;
            this.txtKyotenCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenCd.RegistCheckMethod")));
            this.txtKyotenCd.SetFormField = "txtKyotenCd,txtKyotenNameRyaku";
            this.txtKyotenCd.ShortItemName = "拠点CD";
            this.txtKyotenCd.Size = new System.Drawing.Size(30, 20);
            this.txtKyotenCd.TabIndex = 1;
            this.txtKyotenCd.TabStop = false;
            this.txtKyotenCd.Tag = "拠点を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.txtKyotenCd.WordWrap = false;
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
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(492, 22);
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
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 671;
            this.ISNOT_NEED_DELETE_FLG.TabStop = false;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // HannyushutsuIchiranHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.txtKyotenCd);
            this.Controls.Add(this.dtpSagyouDateTo);
            this.Controls.Add(this.dtpSagyouDateFrom);
            this.Controls.Add(this.txtAlertNum);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.txtReadDataCnt);
            this.Controls.Add(this.lblSagyoDate);
            this.Controls.Add(this.txtKyotenNameRyaku);
            this.Controls.Add(this.lblKyoten);
            this.Controls.Add(this.lblAlertNum);
            this.Controls.Add(this.lblYomikomiDataNum);
            this.Name = "HannyushutsuIchiranHeader";
            this.Text = "HeaderSample";
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.lblYomikomiDataNum, 0);
            this.Controls.SetChildIndex(this.lblAlertNum, 0);
            this.Controls.SetChildIndex(this.lblKyoten, 0);
            this.Controls.SetChildIndex(this.txtKyotenNameRyaku, 0);
            this.Controls.SetChildIndex(this.lblSagyoDate, 0);
            this.Controls.SetChildIndex(this.txtReadDataCnt, 0);
            this.Controls.SetChildIndex(this.label38, 0);
            this.Controls.SetChildIndex(this.txtAlertNum, 0);
            this.Controls.SetChildIndex(this.dtpSagyouDateFrom, 0);
            this.Controls.SetChildIndex(this.dtpSagyouDateTo, 0);
            this.Controls.SetChildIndex(this.txtKyotenCd, 0);
            this.Controls.SetChildIndex(this.ISNOT_NEED_DELETE_FLG, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomTextBox txtKyotenNameRyaku;
        public System.Windows.Forms.Label lblKyoten;
        private System.Windows.Forms.Label lblAlertNum;
        private System.Windows.Forms.Label lblYomikomiDataNum;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label lblSagyoDate;
        public r_framework.CustomControl.CustomDateTimePicker dtpSagyouDateFrom;
        public r_framework.CustomControl.CustomTextBox txtReadDataCnt;
        public r_framework.CustomControl.CustomNumericTextBox2 txtAlertNum;
        public r_framework.CustomControl.CustomDateTimePicker dtpSagyouDateTo;
        public r_framework.CustomControl.CustomNumericTextBox2 txtKyotenCd;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;

    }
}