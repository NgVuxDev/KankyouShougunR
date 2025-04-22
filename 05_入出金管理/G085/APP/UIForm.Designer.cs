namespace Shougun.Core.ReceiptPayManagement.MinyukinIchiranhyo
{
    partial class UIForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.lbl_TyohyoName = new System.Windows.Forms.Label();
            this.txt_TyohyoName = new r_framework.CustomControl.CustomTextBox();
            this.dtp_DateHaniShiteiTo = new r_framework.CustomControl.CustomDateTimePicker();
            this.label38 = new System.Windows.Forms.Label();
            this.lbl_DateHaniShitei = new System.Windows.Forms.Label();
            this.dtp_DateHaniShiteiFrom = new r_framework.CustomControl.CustomDateTimePicker();
            this.txt_DateHaniShurui = new r_framework.CustomControl.CustomNumericTextBox2();
            this.rdo_Tojitu = new r_framework.CustomControl.CustomRadioButton();
            this.rdo_Togetu = new r_framework.CustomControl.CustomRadioButton();
            this.rdo_KikanKotei = new r_framework.CustomControl.CustomRadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_KyotenCD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.txt_KyotenName = new r_framework.CustomControl.CustomTextBox();
            this.btn_KyotenSearch = new r_framework.CustomControl.CustomButton();
            this.lbl_ShukeiKoumoku = new System.Windows.Forms.Label();
            this.lbl_Komoku1 = new System.Windows.Forms.Label();
            this.cmb_Komoku1 = new r_framework.CustomControl.CustomComboBox();
            this.txt_Komoku1From = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.txt_Komoku1FromName = new r_framework.CustomControl.CustomTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Komoku1ToName = new r_framework.CustomControl.CustomTextBox();
            this.txt_Komoku1To = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.txt_Komoku2ToName = new r_framework.CustomControl.CustomTextBox();
            this.txt_Komoku2To = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Komoku2FromName = new r_framework.CustomControl.CustomTextBox();
            this.txt_Komoku2From = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.cmb_Komoku2 = new r_framework.CustomControl.CustomComboBox();
            this.lbl_Komoku2 = new System.Windows.Forms.Label();
            this.btn_Komoku1From = new r_framework.CustomControl.CustomButton();
            this.btn_Komoku2From = new r_framework.CustomControl.CustomButton();
            this.btn_Komoku1To = new r_framework.CustomControl.CustomButton();
            this.btn_Komoku2To = new r_framework.CustomControl.CustomButton();
            this.customButton1 = new r_framework.CustomControl.CustomButton();
            this.customButton2 = new r_framework.CustomControl.CustomButton();
            this.customTextBox1 = new r_framework.CustomControl.CustomTextBox();
            this.customAlphaNumTextBox1 = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.customTextBox2 = new r_framework.CustomControl.CustomTextBox();
            this.customAlphaNumTextBox2 = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.customComboBox1 = new r_framework.CustomControl.CustomComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_ShuturyokuKanoKomokuDenpyo = new System.Windows.Forms.Label();
            this.lbl_ShuturyokuKomokuDenpyo = new System.Windows.Forms.Label();
            this.lbl_ShuturyokuKanoKomokuMeisai = new System.Windows.Forms.Label();
            this.lbl_ShuturyokuKomokuMeisai = new System.Windows.Forms.Label();
            this.txt_ShuturyokuKanoKomokuDenpyo = new r_framework.CustomControl.CustomTextBox();
            this.txt_ShuturyokuKomokuDenpyo = new r_framework.CustomControl.CustomTextBox();
            this.txt_ShuturyokuKanoKomokuMeisai = new r_framework.CustomControl.CustomTextBox();
            this.txt_ShuturyokuKomokuMeisai = new r_framework.CustomControl.CustomTextBox();
            this.btn_DenpyoInsert = new r_framework.CustomControl.CustomButton();
            this.btn_DenpyoDelete = new r_framework.CustomControl.CustomButton();
            this.btn_DenpyoAllDelete = new r_framework.CustomControl.CustomButton();
            this.btn_MeisaiAllDelete = new r_framework.CustomControl.CustomButton();
            this.btn_MeisaiDelete = new r_framework.CustomControl.CustomButton();
            this.btn_MeisaiInsert = new r_framework.CustomControl.CustomButton();
            this.panel1 = new r_framework.CustomControl.CustomPanel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_TyohyoName
            // 
            this.lbl_TyohyoName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_TyohyoName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_TyohyoName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_TyohyoName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_TyohyoName.ForeColor = System.Drawing.Color.White;
            this.lbl_TyohyoName.Location = new System.Drawing.Point(12, 9);
            this.lbl_TyohyoName.Name = "lbl_TyohyoName";
            this.lbl_TyohyoName.Size = new System.Drawing.Size(110, 20);
            this.lbl_TyohyoName.TabIndex = 546;
            this.lbl_TyohyoName.Text = "帳票名";
            this.lbl_TyohyoName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_TyohyoName
            // 
            this.txt_TyohyoName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txt_TyohyoName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_TyohyoName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txt_TyohyoName.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_TyohyoName.DisplayPopUp = null;
            this.txt_TyohyoName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_TyohyoName.FocusOutCheckMethod")));
            this.txt_TyohyoName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_TyohyoName.ForeColor = System.Drawing.Color.Black;
            this.txt_TyohyoName.IsInputErrorOccured = false;
            this.txt_TyohyoName.Location = new System.Drawing.Point(128, 9);
            this.txt_TyohyoName.MaxLength = 0;
            this.txt_TyohyoName.Name = "txt_TyohyoName";
            this.txt_TyohyoName.PopupAfterExecute = null;
            this.txt_TyohyoName.PopupBeforeExecute = null;
            this.txt_TyohyoName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_TyohyoName.PopupSearchSendParams")));
            this.txt_TyohyoName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_TyohyoName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_TyohyoName.popupWindowSetting")));
            this.txt_TyohyoName.ReadOnly = true;
            this.txt_TyohyoName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_TyohyoName.RegistCheckMethod")));
            this.txt_TyohyoName.Size = new System.Drawing.Size(250, 20);
            this.txt_TyohyoName.TabIndex = 547;
            this.txt_TyohyoName.Tag = " は 0 文字以内で入力してください。";
            // 
            // dtp_DateHaniShiteiTo
            // 
            this.dtp_DateHaniShiteiTo.BackColor = System.Drawing.SystemColors.Window;
            this.dtp_DateHaniShiteiTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtp_DateHaniShiteiTo.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.dtp_DateHaniShiteiTo.Checked = false;
            this.dtp_DateHaniShiteiTo.CustomFormat = "yyyy/MM/dd ddd";
            this.dtp_DateHaniShiteiTo.DateTimeNowYear = "";
            this.dtp_DateHaniShiteiTo.DefaultBackColor = System.Drawing.Color.Empty;
            this.dtp_DateHaniShiteiTo.DisplayPopUp = null;
            this.dtp_DateHaniShiteiTo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtp_DateHaniShiteiTo.FocusOutCheckMethod")));
            this.dtp_DateHaniShiteiTo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.dtp_DateHaniShiteiTo.ForeColor = System.Drawing.Color.Black;
            this.dtp_DateHaniShiteiTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_DateHaniShiteiTo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtp_DateHaniShiteiTo.IsInputErrorOccured = false;
            this.dtp_DateHaniShiteiTo.Location = new System.Drawing.Point(274, 42);
            this.dtp_DateHaniShiteiTo.MaxLength = 10;
            this.dtp_DateHaniShiteiTo.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_DateHaniShiteiTo.Name = "dtp_DateHaniShiteiTo";
            this.dtp_DateHaniShiteiTo.NullValue = "";
            this.dtp_DateHaniShiteiTo.PopupAfterExecute = null;
            this.dtp_DateHaniShiteiTo.PopupBeforeExecute = null;
            this.dtp_DateHaniShiteiTo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dtp_DateHaniShiteiTo.PopupSearchSendParams")));
            this.dtp_DateHaniShiteiTo.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dtp_DateHaniShiteiTo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dtp_DateHaniShiteiTo.popupWindowSetting")));
            this.dtp_DateHaniShiteiTo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtp_DateHaniShiteiTo.RegistCheckMethod")));
            this.dtp_DateHaniShiteiTo.Size = new System.Drawing.Size(115, 20);
            this.dtp_DateHaniShiteiTo.TabIndex = 551;
            this.dtp_DateHaniShiteiTo.Tag = "";
            this.dtp_DateHaniShiteiTo.Text = "2013/09/02(月)";
            this.dtp_DateHaniShiteiTo.Value = new System.DateTime(2013, 9, 2, 0, 0, 0, 0);
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.Transparent;
            this.label38.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label38.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label38.ForeColor = System.Drawing.Color.Black;
            this.label38.Location = new System.Drawing.Point(249, 42);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(19, 20);
            this.label38.TabIndex = 550;
            this.label38.Text = "～";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_DateHaniShitei
            // 
            this.lbl_DateHaniShitei.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_DateHaniShitei.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_DateHaniShitei.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_DateHaniShitei.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_DateHaniShitei.ForeColor = System.Drawing.Color.White;
            this.lbl_DateHaniShitei.Location = new System.Drawing.Point(12, 41);
            this.lbl_DateHaniShitei.Name = "lbl_DateHaniShitei";
            this.lbl_DateHaniShitei.Size = new System.Drawing.Size(110, 20);
            this.lbl_DateHaniShitei.TabIndex = 548;
            this.lbl_DateHaniShitei.Text = "日付範囲指定";
            this.lbl_DateHaniShitei.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtp_DateHaniShiteiFrom
            // 
            this.dtp_DateHaniShiteiFrom.BackColor = System.Drawing.SystemColors.Window;
            this.dtp_DateHaniShiteiFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtp_DateHaniShiteiFrom.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.dtp_DateHaniShiteiFrom.Checked = false;
            this.dtp_DateHaniShiteiFrom.CustomFormat = "yyyy/MM/dd ddd";
            this.dtp_DateHaniShiteiFrom.DateTimeNowYear = "";
            this.dtp_DateHaniShiteiFrom.DefaultBackColor = System.Drawing.Color.Empty;
            this.dtp_DateHaniShiteiFrom.DisplayPopUp = null;
            this.dtp_DateHaniShiteiFrom.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtp_DateHaniShiteiFrom.FocusOutCheckMethod")));
            this.dtp_DateHaniShiteiFrom.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.dtp_DateHaniShiteiFrom.ForeColor = System.Drawing.Color.Black;
            this.dtp_DateHaniShiteiFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_DateHaniShiteiFrom.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtp_DateHaniShiteiFrom.IsInputErrorOccured = false;
            this.dtp_DateHaniShiteiFrom.Location = new System.Drawing.Point(128, 42);
            this.dtp_DateHaniShiteiFrom.MaxLength = 10;
            this.dtp_DateHaniShiteiFrom.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_DateHaniShiteiFrom.Name = "dtp_DateHaniShiteiFrom";
            this.dtp_DateHaniShiteiFrom.NullValue = "";
            this.dtp_DateHaniShiteiFrom.PopupAfterExecute = null;
            this.dtp_DateHaniShiteiFrom.PopupBeforeExecute = null;
            this.dtp_DateHaniShiteiFrom.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dtp_DateHaniShiteiFrom.PopupSearchSendParams")));
            this.dtp_DateHaniShiteiFrom.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dtp_DateHaniShiteiFrom.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dtp_DateHaniShiteiFrom.popupWindowSetting")));
            this.dtp_DateHaniShiteiFrom.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtp_DateHaniShiteiFrom.RegistCheckMethod")));
            this.dtp_DateHaniShiteiFrom.Size = new System.Drawing.Size(115, 20);
            this.dtp_DateHaniShiteiFrom.TabIndex = 549;
            this.dtp_DateHaniShiteiFrom.Tag = "";
            this.dtp_DateHaniShiteiFrom.Text = "2013/09/02(月)";
            this.dtp_DateHaniShiteiFrom.Value = new System.DateTime(2013, 9, 2, 0, 0, 0, 0);
            // 
            // txt_DateHaniShurui
            // 
            this.txt_DateHaniShurui.BackColor = System.Drawing.SystemColors.Window;
            this.txt_DateHaniShurui.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_DateHaniShurui.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_DateHaniShurui.DisplayPopUp = null;
            this.txt_DateHaniShurui.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_DateHaniShurui.FocusOutCheckMethod")));
            this.txt_DateHaniShurui.ForeColor = System.Drawing.Color.Black;
            this.txt_DateHaniShurui.IsInputErrorOccured = false;
            this.txt_DateHaniShurui.Location = new System.Drawing.Point(1, 3);
            this.txt_DateHaniShurui.Name = "txt_DateHaniShurui";
            this.txt_DateHaniShurui.PopupAfterExecute = null;
            this.txt_DateHaniShurui.PopupBeforeExecute = null;
            this.txt_DateHaniShurui.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_DateHaniShurui.PopupSearchSendParams")));
            this.txt_DateHaniShurui.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_DateHaniShurui.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_DateHaniShurui.popupWindowSetting")));
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
            this.txt_DateHaniShurui.RangeSetting = rangeSettingDto1;
            this.txt_DateHaniShurui.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_DateHaniShurui.RegistCheckMethod")));
            this.txt_DateHaniShurui.Size = new System.Drawing.Size(20, 19);
            this.txt_DateHaniShurui.TabIndex = 552;
            this.txt_DateHaniShurui.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_DateHaniShurui.WordWrap = false;
            // 
            // rdo_Tojitu
            // 
            this.rdo_Tojitu.AutoSize = true;
            this.rdo_Tojitu.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdo_Tojitu.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_Tojitu.FocusOutCheckMethod")));
            this.rdo_Tojitu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdo_Tojitu.LinkedTextBox = "txt_DateHaniShurui";
            this.rdo_Tojitu.Location = new System.Drawing.Point(27, 4);
            this.rdo_Tojitu.Name = "rdo_Tojitu";
            this.rdo_Tojitu.PopupAfterExecute = null;
            this.rdo_Tojitu.PopupBeforeExecute = null;
            this.rdo_Tojitu.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdo_Tojitu.PopupSearchSendParams")));
            this.rdo_Tojitu.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdo_Tojitu.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdo_Tojitu.popupWindowSetting")));
            this.rdo_Tojitu.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_Tojitu.RegistCheckMethod")));
            this.rdo_Tojitu.Size = new System.Drawing.Size(67, 17);
            this.rdo_Tojitu.TabIndex = 553;
            this.rdo_Tojitu.Text = "1.当日";
            this.rdo_Tojitu.UseVisualStyleBackColor = true;
            // 
            // rdo_Togetu
            // 
            this.rdo_Togetu.AutoSize = true;
            this.rdo_Togetu.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdo_Togetu.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_Togetu.FocusOutCheckMethod")));
            this.rdo_Togetu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdo_Togetu.LinkedTextBox = "txt_DateHaniShurui";
            this.rdo_Togetu.Location = new System.Drawing.Point(100, 4);
            this.rdo_Togetu.Name = "rdo_Togetu";
            this.rdo_Togetu.PopupAfterExecute = null;
            this.rdo_Togetu.PopupBeforeExecute = null;
            this.rdo_Togetu.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdo_Togetu.PopupSearchSendParams")));
            this.rdo_Togetu.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdo_Togetu.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdo_Togetu.popupWindowSetting")));
            this.rdo_Togetu.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_Togetu.RegistCheckMethod")));
            this.rdo_Togetu.Size = new System.Drawing.Size(67, 17);
            this.rdo_Togetu.TabIndex = 554;
            this.rdo_Togetu.Text = "2.当月";
            this.rdo_Togetu.UseVisualStyleBackColor = true;
            // 
            // rdo_KikanKotei
            // 
            this.rdo_KikanKotei.AutoSize = true;
            this.rdo_KikanKotei.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdo_KikanKotei.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_KikanKotei.FocusOutCheckMethod")));
            this.rdo_KikanKotei.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdo_KikanKotei.LinkedTextBox = "txt_DateHaniShurui";
            this.rdo_KikanKotei.Location = new System.Drawing.Point(173, 4);
            this.rdo_KikanKotei.Name = "rdo_KikanKotei";
            this.rdo_KikanKotei.PopupAfterExecute = null;
            this.rdo_KikanKotei.PopupBeforeExecute = null;
            this.rdo_KikanKotei.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdo_KikanKotei.PopupSearchSendParams")));
            this.rdo_KikanKotei.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdo_KikanKotei.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdo_KikanKotei.popupWindowSetting")));
            this.rdo_KikanKotei.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_KikanKotei.RegistCheckMethod")));
            this.rdo_KikanKotei.Size = new System.Drawing.Size(95, 17);
            this.rdo_KikanKotei.TabIndex = 555;
            this.rdo_KikanKotei.Text = "3.期間固定";
            this.rdo_KikanKotei.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 556;
            this.label1.Text = "拠点指定";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_KyotenCD
            // 
            this.txt_KyotenCD.BackColor = System.Drawing.SystemColors.Window;
            this.txt_KyotenCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_KyotenCD.CustomFormatSetting = "00";
            this.txt_KyotenCD.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_KyotenCD.DisplayPopUp = null;
            this.txt_KyotenCD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_KyotenCD.FocusOutCheckMethod")));
            this.txt_KyotenCD.ForeColor = System.Drawing.Color.Black;
            this.txt_KyotenCD.FormatSetting = "カスタム";
            this.txt_KyotenCD.IsInputErrorOccured = false;
            this.txt_KyotenCD.Location = new System.Drawing.Point(128, 65);
            this.txt_KyotenCD.Name = "txt_KyotenCD";
            this.txt_KyotenCD.PopupAfterExecute = null;
            this.txt_KyotenCD.PopupBeforeExecute = null;
            this.txt_KyotenCD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_KyotenCD.PopupSearchSendParams")));
            this.txt_KyotenCD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_KyotenCD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_KyotenCD.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.txt_KyotenCD.RangeSetting = rangeSettingDto2;
            this.txt_KyotenCD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_KyotenCD.RegistCheckMethod")));
            this.txt_KyotenCD.Size = new System.Drawing.Size(26, 19);
            this.txt_KyotenCD.TabIndex = 557;
            this.txt_KyotenCD.Text = "88";
            this.txt_KyotenCD.WordWrap = false;
            // 
            // txt_KyotenName
            // 
            this.txt_KyotenName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txt_KyotenName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_KyotenName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txt_KyotenName.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_KyotenName.DisplayPopUp = null;
            this.txt_KyotenName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_KyotenName.FocusOutCheckMethod")));
            this.txt_KyotenName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_KyotenName.ForeColor = System.Drawing.Color.Black;
            this.txt_KyotenName.IsInputErrorOccured = false;
            this.txt_KyotenName.Location = new System.Drawing.Point(155, 65);
            this.txt_KyotenName.MaxLength = 0;
            this.txt_KyotenName.Name = "txt_KyotenName";
            this.txt_KyotenName.PopupAfterExecute = null;
            this.txt_KyotenName.PopupBeforeExecute = null;
            this.txt_KyotenName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_KyotenName.PopupSearchSendParams")));
            this.txt_KyotenName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_KyotenName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_KyotenName.popupWindowSetting")));
            this.txt_KyotenName.ReadOnly = true;
            this.txt_KyotenName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_KyotenName.RegistCheckMethod")));
            this.txt_KyotenName.Size = new System.Drawing.Size(149, 20);
            this.txt_KyotenName.TabIndex = 558;
            this.txt_KyotenName.Tag = " は 0 文字以内で入力してください。";
            this.txt_KyotenName.Text = "１２３４５６７８９０";
            // 
            // btn_KyotenSearch
            // 
            this.btn_KyotenSearch.DefaultBackColor = System.Drawing.Color.Empty;
            this.btn_KyotenSearch.Location = new System.Drawing.Point(310, 65);
            this.btn_KyotenSearch.Name = "btn_KyotenSearch";
            this.btn_KyotenSearch.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btn_KyotenSearch.Size = new System.Drawing.Size(33, 20);
            this.btn_KyotenSearch.TabIndex = 559;
            this.btn_KyotenSearch.Text = "customButton1";
            this.btn_KyotenSearch.UseVisualStyleBackColor = true;
            // 
            // lbl_ShukeiKoumoku
            // 
            this.lbl_ShukeiKoumoku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_ShukeiKoumoku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_ShukeiKoumoku.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_ShukeiKoumoku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_ShukeiKoumoku.ForeColor = System.Drawing.Color.White;
            this.lbl_ShukeiKoumoku.Location = new System.Drawing.Point(12, 97);
            this.lbl_ShukeiKoumoku.Name = "lbl_ShukeiKoumoku";
            this.lbl_ShukeiKoumoku.Size = new System.Drawing.Size(110, 61);
            this.lbl_ShukeiKoumoku.TabIndex = 564;
            this.lbl_ShukeiKoumoku.Text = "集計項目";
            this.lbl_ShukeiKoumoku.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Komoku1
            // 
            this.lbl_Komoku1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Komoku1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Komoku1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Komoku1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Komoku1.ForeColor = System.Drawing.Color.White;
            this.lbl_Komoku1.Location = new System.Drawing.Point(128, 97);
            this.lbl_Komoku1.Name = "lbl_Komoku1";
            this.lbl_Komoku1.Size = new System.Drawing.Size(67, 20);
            this.lbl_Komoku1.TabIndex = 565;
            this.lbl_Komoku1.Text = "第１項目";
            this.lbl_Komoku1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmb_Komoku1
            // 
            this.cmb_Komoku1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmb_Komoku1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_Komoku1.BackColor = System.Drawing.SystemColors.Window;
            this.cmb_Komoku1.DefaultBackColor = System.Drawing.Color.Empty;
            this.cmb_Komoku1.DisplayPopUp = null;
            this.cmb_Komoku1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cmb_Komoku1.FocusOutCheckMethod")));
            this.cmb_Komoku1.FormattingEnabled = true;
            this.cmb_Komoku1.IsInputErrorOccured = false;
            this.cmb_Komoku1.Location = new System.Drawing.Point(201, 97);
            this.cmb_Komoku1.Name = "cmb_Komoku1";
            this.cmb_Komoku1.PopupAfterExecute = null;
            this.cmb_Komoku1.PopupBeforeExecute = null;
            this.cmb_Komoku1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cmb_Komoku1.PopupSearchSendParams")));
            this.cmb_Komoku1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cmb_Komoku1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cmb_Komoku1.popupWindowSetting")));
            this.cmb_Komoku1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cmb_Komoku1.RegistCheckMethod")));
            this.cmb_Komoku1.Size = new System.Drawing.Size(103, 20);
            this.cmb_Komoku1.TabIndex = 567;
            this.cmb_Komoku1.Tag = " は 0 文字以内で入力してください。";
            // 
            // txt_Komoku1From
            // 
            this.txt_Komoku1From.BackColor = System.Drawing.SystemColors.Window;
            this.txt_Komoku1From.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Komoku1From.CharacterLimitList = null;
            this.txt_Komoku1From.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_Komoku1From.DisplayPopUp = null;
            this.txt_Komoku1From.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Komoku1From.FocusOutCheckMethod")));
            this.txt_Komoku1From.ForeColor = System.Drawing.Color.Black;
            this.txt_Komoku1From.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txt_Komoku1From.IsInputErrorOccured = false;
            this.txt_Komoku1From.Location = new System.Drawing.Point(310, 97);
            this.txt_Komoku1From.MaxLength = 6;
            this.txt_Komoku1From.Name = "txt_Komoku1From";
            this.txt_Komoku1From.PopupAfterExecute = null;
            this.txt_Komoku1From.PopupBeforeExecute = null;
            this.txt_Komoku1From.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_Komoku1From.PopupSearchSendParams")));
            this.txt_Komoku1From.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_Komoku1From.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_Komoku1From.popupWindowSetting")));
            this.txt_Komoku1From.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Komoku1From.RegistCheckMethod")));
            this.txt_Komoku1From.Size = new System.Drawing.Size(50, 19);
            this.txt_Komoku1From.TabIndex = 568;
            this.txt_Komoku1From.Text = "123456";
            // 
            // txt_Komoku1FromName
            // 
            this.txt_Komoku1FromName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txt_Komoku1FromName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Komoku1FromName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txt_Komoku1FromName.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_Komoku1FromName.DisplayPopUp = null;
            this.txt_Komoku1FromName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Komoku1FromName.FocusOutCheckMethod")));
            this.txt_Komoku1FromName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_Komoku1FromName.ForeColor = System.Drawing.Color.Black;
            this.txt_Komoku1FromName.IsInputErrorOccured = false;
            this.txt_Komoku1FromName.Location = new System.Drawing.Point(366, 96);
            this.txt_Komoku1FromName.MaxLength = 0;
            this.txt_Komoku1FromName.Name = "txt_Komoku1FromName";
            this.txt_Komoku1FromName.PopupAfterExecute = null;
            this.txt_Komoku1FromName.PopupBeforeExecute = null;
            this.txt_Komoku1FromName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_Komoku1FromName.PopupSearchSendParams")));
            this.txt_Komoku1FromName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_Komoku1FromName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_Komoku1FromName.popupWindowSetting")));
            this.txt_Komoku1FromName.ReadOnly = true;
            this.txt_Komoku1FromName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Komoku1FromName.RegistCheckMethod")));
            this.txt_Komoku1FromName.Size = new System.Drawing.Size(149, 20);
            this.txt_Komoku1FromName.TabIndex = 569;
            this.txt_Komoku1FromName.Tag = " は 0 文字以内で入力してください。";
            this.txt_Komoku1FromName.Text = "１２３４５６７８９０";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(560, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 20);
            this.label2.TabIndex = 570;
            this.label2.Text = "～";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_Komoku1ToName
            // 
            this.txt_Komoku1ToName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txt_Komoku1ToName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Komoku1ToName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txt_Komoku1ToName.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_Komoku1ToName.DisplayPopUp = null;
            this.txt_Komoku1ToName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Komoku1ToName.FocusOutCheckMethod")));
            this.txt_Komoku1ToName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_Komoku1ToName.ForeColor = System.Drawing.Color.Black;
            this.txt_Komoku1ToName.IsInputErrorOccured = false;
            this.txt_Komoku1ToName.Location = new System.Drawing.Point(641, 96);
            this.txt_Komoku1ToName.MaxLength = 0;
            this.txt_Komoku1ToName.Name = "txt_Komoku1ToName";
            this.txt_Komoku1ToName.PopupAfterExecute = null;
            this.txt_Komoku1ToName.PopupBeforeExecute = null;
            this.txt_Komoku1ToName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_Komoku1ToName.PopupSearchSendParams")));
            this.txt_Komoku1ToName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_Komoku1ToName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_Komoku1ToName.popupWindowSetting")));
            this.txt_Komoku1ToName.ReadOnly = true;
            this.txt_Komoku1ToName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Komoku1ToName.RegistCheckMethod")));
            this.txt_Komoku1ToName.Size = new System.Drawing.Size(149, 20);
            this.txt_Komoku1ToName.TabIndex = 572;
            this.txt_Komoku1ToName.Tag = " は 0 文字以内で入力してください。";
            this.txt_Komoku1ToName.Text = "１２３４５６７８９０";
            // 
            // txt_Komoku1To
            // 
            this.txt_Komoku1To.BackColor = System.Drawing.SystemColors.Window;
            this.txt_Komoku1To.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Komoku1To.CharacterLimitList = null;
            this.txt_Komoku1To.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_Komoku1To.DisplayPopUp = null;
            this.txt_Komoku1To.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Komoku1To.FocusOutCheckMethod")));
            this.txt_Komoku1To.ForeColor = System.Drawing.Color.Black;
            this.txt_Komoku1To.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txt_Komoku1To.IsInputErrorOccured = false;
            this.txt_Komoku1To.Location = new System.Drawing.Point(585, 97);
            this.txt_Komoku1To.MaxLength = 6;
            this.txt_Komoku1To.Name = "txt_Komoku1To";
            this.txt_Komoku1To.PopupAfterExecute = null;
            this.txt_Komoku1To.PopupBeforeExecute = null;
            this.txt_Komoku1To.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_Komoku1To.PopupSearchSendParams")));
            this.txt_Komoku1To.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_Komoku1To.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_Komoku1To.popupWindowSetting")));
            this.txt_Komoku1To.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Komoku1To.RegistCheckMethod")));
            this.txt_Komoku1To.Size = new System.Drawing.Size(50, 19);
            this.txt_Komoku1To.TabIndex = 571;
            this.txt_Komoku1To.Text = "123456";
            // 
            // txt_Komoku2ToName
            // 
            this.txt_Komoku2ToName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txt_Komoku2ToName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Komoku2ToName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txt_Komoku2ToName.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_Komoku2ToName.DisplayPopUp = null;
            this.txt_Komoku2ToName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Komoku2ToName.FocusOutCheckMethod")));
            this.txt_Komoku2ToName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_Komoku2ToName.ForeColor = System.Drawing.Color.Black;
            this.txt_Komoku2ToName.IsInputErrorOccured = false;
            this.txt_Komoku2ToName.Location = new System.Drawing.Point(641, 118);
            this.txt_Komoku2ToName.MaxLength = 0;
            this.txt_Komoku2ToName.Name = "txt_Komoku2ToName";
            this.txt_Komoku2ToName.PopupAfterExecute = null;
            this.txt_Komoku2ToName.PopupBeforeExecute = null;
            this.txt_Komoku2ToName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_Komoku2ToName.PopupSearchSendParams")));
            this.txt_Komoku2ToName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_Komoku2ToName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_Komoku2ToName.popupWindowSetting")));
            this.txt_Komoku2ToName.ReadOnly = true;
            this.txt_Komoku2ToName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Komoku2ToName.RegistCheckMethod")));
            this.txt_Komoku2ToName.Size = new System.Drawing.Size(149, 20);
            this.txt_Komoku2ToName.TabIndex = 579;
            this.txt_Komoku2ToName.Tag = " は 0 文字以内で入力してください。";
            this.txt_Komoku2ToName.Text = "１２３４５６７８９０";
            // 
            // txt_Komoku2To
            // 
            this.txt_Komoku2To.BackColor = System.Drawing.SystemColors.Window;
            this.txt_Komoku2To.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Komoku2To.CharacterLimitList = null;
            this.txt_Komoku2To.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_Komoku2To.DisplayPopUp = null;
            this.txt_Komoku2To.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Komoku2To.FocusOutCheckMethod")));
            this.txt_Komoku2To.ForeColor = System.Drawing.Color.Black;
            this.txt_Komoku2To.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txt_Komoku2To.IsInputErrorOccured = false;
            this.txt_Komoku2To.Location = new System.Drawing.Point(585, 118);
            this.txt_Komoku2To.MaxLength = 6;
            this.txt_Komoku2To.Name = "txt_Komoku2To";
            this.txt_Komoku2To.PopupAfterExecute = null;
            this.txt_Komoku2To.PopupBeforeExecute = null;
            this.txt_Komoku2To.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_Komoku2To.PopupSearchSendParams")));
            this.txt_Komoku2To.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_Komoku2To.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_Komoku2To.popupWindowSetting")));
            this.txt_Komoku2To.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Komoku2To.RegistCheckMethod")));
            this.txt_Komoku2To.Size = new System.Drawing.Size(50, 19);
            this.txt_Komoku2To.TabIndex = 578;
            this.txt_Komoku2To.Text = "123456";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(560, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 20);
            this.label3.TabIndex = 577;
            this.label3.Text = "～";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_Komoku2FromName
            // 
            this.txt_Komoku2FromName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txt_Komoku2FromName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Komoku2FromName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txt_Komoku2FromName.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_Komoku2FromName.DisplayPopUp = null;
            this.txt_Komoku2FromName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Komoku2FromName.FocusOutCheckMethod")));
            this.txt_Komoku2FromName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_Komoku2FromName.ForeColor = System.Drawing.Color.Black;
            this.txt_Komoku2FromName.IsInputErrorOccured = false;
            this.txt_Komoku2FromName.Location = new System.Drawing.Point(366, 118);
            this.txt_Komoku2FromName.MaxLength = 0;
            this.txt_Komoku2FromName.Name = "txt_Komoku2FromName";
            this.txt_Komoku2FromName.PopupAfterExecute = null;
            this.txt_Komoku2FromName.PopupBeforeExecute = null;
            this.txt_Komoku2FromName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_Komoku2FromName.PopupSearchSendParams")));
            this.txt_Komoku2FromName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_Komoku2FromName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_Komoku2FromName.popupWindowSetting")));
            this.txt_Komoku2FromName.ReadOnly = true;
            this.txt_Komoku2FromName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Komoku2FromName.RegistCheckMethod")));
            this.txt_Komoku2FromName.Size = new System.Drawing.Size(149, 20);
            this.txt_Komoku2FromName.TabIndex = 576;
            this.txt_Komoku2FromName.Tag = " は 0 文字以内で入力してください。";
            this.txt_Komoku2FromName.Text = "１２３４５６７８９０";
            // 
            // txt_Komoku2From
            // 
            this.txt_Komoku2From.BackColor = System.Drawing.SystemColors.Window;
            this.txt_Komoku2From.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Komoku2From.CharacterLimitList = null;
            this.txt_Komoku2From.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_Komoku2From.DisplayPopUp = null;
            this.txt_Komoku2From.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Komoku2From.FocusOutCheckMethod")));
            this.txt_Komoku2From.ForeColor = System.Drawing.Color.Black;
            this.txt_Komoku2From.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txt_Komoku2From.IsInputErrorOccured = false;
            this.txt_Komoku2From.Location = new System.Drawing.Point(310, 118);
            this.txt_Komoku2From.MaxLength = 6;
            this.txt_Komoku2From.Name = "txt_Komoku2From";
            this.txt_Komoku2From.PopupAfterExecute = null;
            this.txt_Komoku2From.PopupBeforeExecute = null;
            this.txt_Komoku2From.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_Komoku2From.PopupSearchSendParams")));
            this.txt_Komoku2From.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_Komoku2From.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_Komoku2From.popupWindowSetting")));
            this.txt_Komoku2From.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Komoku2From.RegistCheckMethod")));
            this.txt_Komoku2From.Size = new System.Drawing.Size(50, 19);
            this.txt_Komoku2From.TabIndex = 575;
            this.txt_Komoku2From.Text = "123456";
            // 
            // cmb_Komoku2
            // 
            this.cmb_Komoku2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmb_Komoku2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_Komoku2.BackColor = System.Drawing.SystemColors.Window;
            this.cmb_Komoku2.DefaultBackColor = System.Drawing.Color.Empty;
            this.cmb_Komoku2.DisplayPopUp = null;
            this.cmb_Komoku2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cmb_Komoku2.FocusOutCheckMethod")));
            this.cmb_Komoku2.FormattingEnabled = true;
            this.cmb_Komoku2.IsInputErrorOccured = false;
            this.cmb_Komoku2.Location = new System.Drawing.Point(201, 118);
            this.cmb_Komoku2.Name = "cmb_Komoku2";
            this.cmb_Komoku2.PopupAfterExecute = null;
            this.cmb_Komoku2.PopupBeforeExecute = null;
            this.cmb_Komoku2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cmb_Komoku2.PopupSearchSendParams")));
            this.cmb_Komoku2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cmb_Komoku2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cmb_Komoku2.popupWindowSetting")));
            this.cmb_Komoku2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cmb_Komoku2.RegistCheckMethod")));
            this.cmb_Komoku2.Size = new System.Drawing.Size(103, 20);
            this.cmb_Komoku2.TabIndex = 574;
            this.cmb_Komoku2.Tag = " は 0 文字以内で入力してください。";
            // 
            // lbl_Komoku2
            // 
            this.lbl_Komoku2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Komoku2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Komoku2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Komoku2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Komoku2.ForeColor = System.Drawing.Color.White;
            this.lbl_Komoku2.Location = new System.Drawing.Point(128, 118);
            this.lbl_Komoku2.Name = "lbl_Komoku2";
            this.lbl_Komoku2.Size = new System.Drawing.Size(67, 20);
            this.lbl_Komoku2.TabIndex = 573;
            this.lbl_Komoku2.Text = "第２項目";
            this.lbl_Komoku2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Komoku1From
            // 
            this.btn_Komoku1From.DefaultBackColor = System.Drawing.Color.Empty;
            this.btn_Komoku1From.Location = new System.Drawing.Point(521, 96);
            this.btn_Komoku1From.Name = "btn_Komoku1From";
            this.btn_Komoku1From.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btn_Komoku1From.Size = new System.Drawing.Size(33, 20);
            this.btn_Komoku1From.TabIndex = 580;
            this.btn_Komoku1From.Text = "customButton1";
            this.btn_Komoku1From.UseVisualStyleBackColor = true;
            // 
            // btn_Komoku2From
            // 
            this.btn_Komoku2From.DefaultBackColor = System.Drawing.Color.Empty;
            this.btn_Komoku2From.Location = new System.Drawing.Point(521, 118);
            this.btn_Komoku2From.Name = "btn_Komoku2From";
            this.btn_Komoku2From.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btn_Komoku2From.Size = new System.Drawing.Size(33, 20);
            this.btn_Komoku2From.TabIndex = 581;
            this.btn_Komoku2From.Text = "customButton2";
            this.btn_Komoku2From.UseVisualStyleBackColor = true;
            // 
            // btn_Komoku1To
            // 
            this.btn_Komoku1To.DefaultBackColor = System.Drawing.Color.Empty;
            this.btn_Komoku1To.Location = new System.Drawing.Point(796, 96);
            this.btn_Komoku1To.Name = "btn_Komoku1To";
            this.btn_Komoku1To.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btn_Komoku1To.Size = new System.Drawing.Size(33, 20);
            this.btn_Komoku1To.TabIndex = 582;
            this.btn_Komoku1To.Text = "customButton1";
            this.btn_Komoku1To.UseVisualStyleBackColor = true;
            // 
            // btn_Komoku2To
            // 
            this.btn_Komoku2To.DefaultBackColor = System.Drawing.Color.Empty;
            this.btn_Komoku2To.Location = new System.Drawing.Point(796, 118);
            this.btn_Komoku2To.Name = "btn_Komoku2To";
            this.btn_Komoku2To.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btn_Komoku2To.Size = new System.Drawing.Size(33, 20);
            this.btn_Komoku2To.TabIndex = 583;
            this.btn_Komoku2To.Text = "customButton1";
            this.btn_Komoku2To.UseVisualStyleBackColor = true;
            // 
            // customButton1
            // 
            this.customButton1.DefaultBackColor = System.Drawing.Color.Empty;
            this.customButton1.Location = new System.Drawing.Point(796, 139);
            this.customButton1.Name = "customButton1";
            this.customButton1.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.customButton1.Size = new System.Drawing.Size(33, 20);
            this.customButton1.TabIndex = 592;
            this.customButton1.Text = "customButton1";
            this.customButton1.UseVisualStyleBackColor = true;
            // 
            // customButton2
            // 
            this.customButton2.DefaultBackColor = System.Drawing.Color.Empty;
            this.customButton2.Location = new System.Drawing.Point(521, 139);
            this.customButton2.Name = "customButton2";
            this.customButton2.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.customButton2.Size = new System.Drawing.Size(33, 20);
            this.customButton2.TabIndex = 591;
            this.customButton2.Text = "customButton2";
            this.customButton2.UseVisualStyleBackColor = true;
            // 
            // customTextBox1
            // 
            this.customTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.customTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customTextBox1.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.customTextBox1.DefaultBackColor = System.Drawing.Color.Empty;
            this.customTextBox1.DisplayPopUp = null;
            this.customTextBox1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customTextBox1.FocusOutCheckMethod")));
            this.customTextBox1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.customTextBox1.ForeColor = System.Drawing.Color.Black;
            this.customTextBox1.IsInputErrorOccured = false;
            this.customTextBox1.Location = new System.Drawing.Point(641, 139);
            this.customTextBox1.MaxLength = 0;
            this.customTextBox1.Name = "customTextBox1";
            this.customTextBox1.PopupAfterExecute = null;
            this.customTextBox1.PopupBeforeExecute = null;
            this.customTextBox1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customTextBox1.PopupSearchSendParams")));
            this.customTextBox1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.customTextBox1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customTextBox1.popupWindowSetting")));
            this.customTextBox1.ReadOnly = true;
            this.customTextBox1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customTextBox1.RegistCheckMethod")));
            this.customTextBox1.Size = new System.Drawing.Size(149, 20);
            this.customTextBox1.TabIndex = 590;
            this.customTextBox1.Tag = " は 0 文字以内で入力してください。";
            this.customTextBox1.Text = "１２３４５６７８９０";
            // 
            // customAlphaNumTextBox1
            // 
            this.customAlphaNumTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.customAlphaNumTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customAlphaNumTextBox1.CharacterLimitList = null;
            this.customAlphaNumTextBox1.DefaultBackColor = System.Drawing.Color.Empty;
            this.customAlphaNumTextBox1.DisplayPopUp = null;
            this.customAlphaNumTextBox1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customAlphaNumTextBox1.FocusOutCheckMethod")));
            this.customAlphaNumTextBox1.ForeColor = System.Drawing.Color.Black;
            this.customAlphaNumTextBox1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.customAlphaNumTextBox1.IsInputErrorOccured = false;
            this.customAlphaNumTextBox1.Location = new System.Drawing.Point(585, 139);
            this.customAlphaNumTextBox1.MaxLength = 6;
            this.customAlphaNumTextBox1.Name = "customAlphaNumTextBox1";
            this.customAlphaNumTextBox1.PopupAfterExecute = null;
            this.customAlphaNumTextBox1.PopupBeforeExecute = null;
            this.customAlphaNumTextBox1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customAlphaNumTextBox1.PopupSearchSendParams")));
            this.customAlphaNumTextBox1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.customAlphaNumTextBox1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customAlphaNumTextBox1.popupWindowSetting")));
            this.customAlphaNumTextBox1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customAlphaNumTextBox1.RegistCheckMethod")));
            this.customAlphaNumTextBox1.Size = new System.Drawing.Size(50, 19);
            this.customAlphaNumTextBox1.TabIndex = 589;
            this.customAlphaNumTextBox1.Text = "123456";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(560, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 20);
            this.label4.TabIndex = 588;
            this.label4.Text = "～";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customTextBox2
            // 
            this.customTextBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.customTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customTextBox2.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.customTextBox2.DefaultBackColor = System.Drawing.Color.Empty;
            this.customTextBox2.DisplayPopUp = null;
            this.customTextBox2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customTextBox2.FocusOutCheckMethod")));
            this.customTextBox2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.customTextBox2.ForeColor = System.Drawing.Color.Black;
            this.customTextBox2.IsInputErrorOccured = false;
            this.customTextBox2.Location = new System.Drawing.Point(366, 139);
            this.customTextBox2.MaxLength = 0;
            this.customTextBox2.Name = "customTextBox2";
            this.customTextBox2.PopupAfterExecute = null;
            this.customTextBox2.PopupBeforeExecute = null;
            this.customTextBox2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customTextBox2.PopupSearchSendParams")));
            this.customTextBox2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.customTextBox2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customTextBox2.popupWindowSetting")));
            this.customTextBox2.ReadOnly = true;
            this.customTextBox2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customTextBox2.RegistCheckMethod")));
            this.customTextBox2.Size = new System.Drawing.Size(149, 20);
            this.customTextBox2.TabIndex = 587;
            this.customTextBox2.Tag = " は 0 文字以内で入力してください。";
            this.customTextBox2.Text = "１２３４５６７８９０";
            // 
            // customAlphaNumTextBox2
            // 
            this.customAlphaNumTextBox2.BackColor = System.Drawing.SystemColors.Window;
            this.customAlphaNumTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customAlphaNumTextBox2.CharacterLimitList = null;
            this.customAlphaNumTextBox2.DefaultBackColor = System.Drawing.Color.Empty;
            this.customAlphaNumTextBox2.DisplayPopUp = null;
            this.customAlphaNumTextBox2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customAlphaNumTextBox2.FocusOutCheckMethod")));
            this.customAlphaNumTextBox2.ForeColor = System.Drawing.Color.Black;
            this.customAlphaNumTextBox2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.customAlphaNumTextBox2.IsInputErrorOccured = false;
            this.customAlphaNumTextBox2.Location = new System.Drawing.Point(310, 139);
            this.customAlphaNumTextBox2.MaxLength = 6;
            this.customAlphaNumTextBox2.Name = "customAlphaNumTextBox2";
            this.customAlphaNumTextBox2.PopupAfterExecute = null;
            this.customAlphaNumTextBox2.PopupBeforeExecute = null;
            this.customAlphaNumTextBox2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customAlphaNumTextBox2.PopupSearchSendParams")));
            this.customAlphaNumTextBox2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.customAlphaNumTextBox2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customAlphaNumTextBox2.popupWindowSetting")));
            this.customAlphaNumTextBox2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customAlphaNumTextBox2.RegistCheckMethod")));
            this.customAlphaNumTextBox2.Size = new System.Drawing.Size(50, 19);
            this.customAlphaNumTextBox2.TabIndex = 586;
            this.customAlphaNumTextBox2.Text = "123456";
            // 
            // customComboBox1
            // 
            this.customComboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.customComboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.customComboBox1.BackColor = System.Drawing.SystemColors.Window;
            this.customComboBox1.DefaultBackColor = System.Drawing.Color.Empty;
            this.customComboBox1.DisplayPopUp = null;
            this.customComboBox1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customComboBox1.FocusOutCheckMethod")));
            this.customComboBox1.FormattingEnabled = true;
            this.customComboBox1.IsInputErrorOccured = false;
            this.customComboBox1.Location = new System.Drawing.Point(201, 139);
            this.customComboBox1.Name = "customComboBox1";
            this.customComboBox1.PopupAfterExecute = null;
            this.customComboBox1.PopupBeforeExecute = null;
            this.customComboBox1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customComboBox1.PopupSearchSendParams")));
            this.customComboBox1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.customComboBox1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customComboBox1.popupWindowSetting")));
            this.customComboBox1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customComboBox1.RegistCheckMethod")));
            this.customComboBox1.Size = new System.Drawing.Size(103, 20);
            this.customComboBox1.TabIndex = 585;
            this.customComboBox1.Tag = " は 0 文字以内で入力してください。";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(128, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 20);
            this.label5.TabIndex = 584;
            this.label5.Text = "第２項目";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ShuturyokuKanoKomokuDenpyo
            // 
            this.lbl_ShuturyokuKanoKomokuDenpyo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_ShuturyokuKanoKomokuDenpyo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_ShuturyokuKanoKomokuDenpyo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_ShuturyokuKanoKomokuDenpyo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_ShuturyokuKanoKomokuDenpyo.ForeColor = System.Drawing.Color.White;
            this.lbl_ShuturyokuKanoKomokuDenpyo.Location = new System.Drawing.Point(12, 178);
            this.lbl_ShuturyokuKanoKomokuDenpyo.Name = "lbl_ShuturyokuKanoKomokuDenpyo";
            this.lbl_ShuturyokuKanoKomokuDenpyo.Size = new System.Drawing.Size(192, 20);
            this.lbl_ShuturyokuKanoKomokuDenpyo.TabIndex = 593;
            this.lbl_ShuturyokuKanoKomokuDenpyo.Text = "出力可能項目（伝票）";
            this.lbl_ShuturyokuKanoKomokuDenpyo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ShuturyokuKomokuDenpyo
            // 
            this.lbl_ShuturyokuKomokuDenpyo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_ShuturyokuKomokuDenpyo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_ShuturyokuKomokuDenpyo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_ShuturyokuKomokuDenpyo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_ShuturyokuKomokuDenpyo.ForeColor = System.Drawing.Color.White;
            this.lbl_ShuturyokuKomokuDenpyo.Location = new System.Drawing.Point(292, 178);
            this.lbl_ShuturyokuKomokuDenpyo.Name = "lbl_ShuturyokuKomokuDenpyo";
            this.lbl_ShuturyokuKomokuDenpyo.Size = new System.Drawing.Size(192, 20);
            this.lbl_ShuturyokuKomokuDenpyo.TabIndex = 594;
            this.lbl_ShuturyokuKomokuDenpyo.Text = "出力項目（伝票）";
            this.lbl_ShuturyokuKomokuDenpyo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ShuturyokuKanoKomokuMeisai
            // 
            this.lbl_ShuturyokuKanoKomokuMeisai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_ShuturyokuKanoKomokuMeisai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_ShuturyokuKanoKomokuMeisai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_ShuturyokuKanoKomokuMeisai.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_ShuturyokuKanoKomokuMeisai.ForeColor = System.Drawing.Color.White;
            this.lbl_ShuturyokuKanoKomokuMeisai.Location = new System.Drawing.Point(504, 178);
            this.lbl_ShuturyokuKanoKomokuMeisai.Name = "lbl_ShuturyokuKanoKomokuMeisai";
            this.lbl_ShuturyokuKanoKomokuMeisai.Size = new System.Drawing.Size(192, 20);
            this.lbl_ShuturyokuKanoKomokuMeisai.TabIndex = 595;
            this.lbl_ShuturyokuKanoKomokuMeisai.Text = "出力可能項目（明細）";
            this.lbl_ShuturyokuKanoKomokuMeisai.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ShuturyokuKomokuMeisai
            // 
            this.lbl_ShuturyokuKomokuMeisai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_ShuturyokuKomokuMeisai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_ShuturyokuKomokuMeisai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_ShuturyokuKomokuMeisai.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_ShuturyokuKomokuMeisai.ForeColor = System.Drawing.Color.White;
            this.lbl_ShuturyokuKomokuMeisai.Location = new System.Drawing.Point(784, 178);
            this.lbl_ShuturyokuKomokuMeisai.Name = "lbl_ShuturyokuKomokuMeisai";
            this.lbl_ShuturyokuKomokuMeisai.Size = new System.Drawing.Size(192, 20);
            this.lbl_ShuturyokuKomokuMeisai.TabIndex = 596;
            this.lbl_ShuturyokuKomokuMeisai.Text = "出力項目（明細）";
            this.lbl_ShuturyokuKomokuMeisai.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_ShuturyokuKanoKomokuDenpyo
            // 
            this.txt_ShuturyokuKanoKomokuDenpyo.BackColor = System.Drawing.SystemColors.Window;
            this.txt_ShuturyokuKanoKomokuDenpyo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ShuturyokuKanoKomokuDenpyo.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_ShuturyokuKanoKomokuDenpyo.DisplayPopUp = null;
            this.txt_ShuturyokuKanoKomokuDenpyo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_ShuturyokuKanoKomokuDenpyo.FocusOutCheckMethod")));
            this.txt_ShuturyokuKanoKomokuDenpyo.ForeColor = System.Drawing.Color.Black;
            this.txt_ShuturyokuKanoKomokuDenpyo.IsInputErrorOccured = false;
            this.txt_ShuturyokuKanoKomokuDenpyo.Location = new System.Drawing.Point(12, 201);
            this.txt_ShuturyokuKanoKomokuDenpyo.Multiline = true;
            this.txt_ShuturyokuKanoKomokuDenpyo.Name = "txt_ShuturyokuKanoKomokuDenpyo";
            this.txt_ShuturyokuKanoKomokuDenpyo.PopupAfterExecute = null;
            this.txt_ShuturyokuKanoKomokuDenpyo.PopupBeforeExecute = null;
            this.txt_ShuturyokuKanoKomokuDenpyo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_ShuturyokuKanoKomokuDenpyo.PopupSearchSendParams")));
            this.txt_ShuturyokuKanoKomokuDenpyo.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_ShuturyokuKanoKomokuDenpyo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_ShuturyokuKanoKomokuDenpyo.popupWindowSetting")));
            this.txt_ShuturyokuKanoKomokuDenpyo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_ShuturyokuKanoKomokuDenpyo.RegistCheckMethod")));
            this.txt_ShuturyokuKanoKomokuDenpyo.Size = new System.Drawing.Size(192, 223);
            this.txt_ShuturyokuKanoKomokuDenpyo.TabIndex = 597;
            // 
            // txt_ShuturyokuKomokuDenpyo
            // 
            this.txt_ShuturyokuKomokuDenpyo.BackColor = System.Drawing.SystemColors.Window;
            this.txt_ShuturyokuKomokuDenpyo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ShuturyokuKomokuDenpyo.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_ShuturyokuKomokuDenpyo.DisplayPopUp = null;
            this.txt_ShuturyokuKomokuDenpyo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_ShuturyokuKomokuDenpyo.FocusOutCheckMethod")));
            this.txt_ShuturyokuKomokuDenpyo.ForeColor = System.Drawing.Color.Black;
            this.txt_ShuturyokuKomokuDenpyo.IsInputErrorOccured = false;
            this.txt_ShuturyokuKomokuDenpyo.Location = new System.Drawing.Point(292, 201);
            this.txt_ShuturyokuKomokuDenpyo.Multiline = true;
            this.txt_ShuturyokuKomokuDenpyo.Name = "txt_ShuturyokuKomokuDenpyo";
            this.txt_ShuturyokuKomokuDenpyo.PopupAfterExecute = null;
            this.txt_ShuturyokuKomokuDenpyo.PopupBeforeExecute = null;
            this.txt_ShuturyokuKomokuDenpyo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_ShuturyokuKomokuDenpyo.PopupSearchSendParams")));
            this.txt_ShuturyokuKomokuDenpyo.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_ShuturyokuKomokuDenpyo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_ShuturyokuKomokuDenpyo.popupWindowSetting")));
            this.txt_ShuturyokuKomokuDenpyo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_ShuturyokuKomokuDenpyo.RegistCheckMethod")));
            this.txt_ShuturyokuKomokuDenpyo.Size = new System.Drawing.Size(192, 223);
            this.txt_ShuturyokuKomokuDenpyo.TabIndex = 598;
            // 
            // txt_ShuturyokuKanoKomokuMeisai
            // 
            this.txt_ShuturyokuKanoKomokuMeisai.BackColor = System.Drawing.SystemColors.Window;
            this.txt_ShuturyokuKanoKomokuMeisai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ShuturyokuKanoKomokuMeisai.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_ShuturyokuKanoKomokuMeisai.DisplayPopUp = null;
            this.txt_ShuturyokuKanoKomokuMeisai.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_ShuturyokuKanoKomokuMeisai.FocusOutCheckMethod")));
            this.txt_ShuturyokuKanoKomokuMeisai.ForeColor = System.Drawing.Color.Black;
            this.txt_ShuturyokuKanoKomokuMeisai.IsInputErrorOccured = false;
            this.txt_ShuturyokuKanoKomokuMeisai.Location = new System.Drawing.Point(504, 201);
            this.txt_ShuturyokuKanoKomokuMeisai.Multiline = true;
            this.txt_ShuturyokuKanoKomokuMeisai.Name = "txt_ShuturyokuKanoKomokuMeisai";
            this.txt_ShuturyokuKanoKomokuMeisai.PopupAfterExecute = null;
            this.txt_ShuturyokuKanoKomokuMeisai.PopupBeforeExecute = null;
            this.txt_ShuturyokuKanoKomokuMeisai.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_ShuturyokuKanoKomokuMeisai.PopupSearchSendParams")));
            this.txt_ShuturyokuKanoKomokuMeisai.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_ShuturyokuKanoKomokuMeisai.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_ShuturyokuKanoKomokuMeisai.popupWindowSetting")));
            this.txt_ShuturyokuKanoKomokuMeisai.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_ShuturyokuKanoKomokuMeisai.RegistCheckMethod")));
            this.txt_ShuturyokuKanoKomokuMeisai.Size = new System.Drawing.Size(192, 223);
            this.txt_ShuturyokuKanoKomokuMeisai.TabIndex = 599;
            // 
            // txt_ShuturyokuKomokuMeisai
            // 
            this.txt_ShuturyokuKomokuMeisai.BackColor = System.Drawing.SystemColors.Window;
            this.txt_ShuturyokuKomokuMeisai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ShuturyokuKomokuMeisai.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_ShuturyokuKomokuMeisai.DisplayPopUp = null;
            this.txt_ShuturyokuKomokuMeisai.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_ShuturyokuKomokuMeisai.FocusOutCheckMethod")));
            this.txt_ShuturyokuKomokuMeisai.ForeColor = System.Drawing.Color.Black;
            this.txt_ShuturyokuKomokuMeisai.IsInputErrorOccured = false;
            this.txt_ShuturyokuKomokuMeisai.Location = new System.Drawing.Point(784, 201);
            this.txt_ShuturyokuKomokuMeisai.Multiline = true;
            this.txt_ShuturyokuKomokuMeisai.Name = "txt_ShuturyokuKomokuMeisai";
            this.txt_ShuturyokuKomokuMeisai.PopupAfterExecute = null;
            this.txt_ShuturyokuKomokuMeisai.PopupBeforeExecute = null;
            this.txt_ShuturyokuKomokuMeisai.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_ShuturyokuKomokuMeisai.PopupSearchSendParams")));
            this.txt_ShuturyokuKomokuMeisai.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_ShuturyokuKomokuMeisai.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_ShuturyokuKomokuMeisai.popupWindowSetting")));
            this.txt_ShuturyokuKomokuMeisai.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_ShuturyokuKomokuMeisai.RegistCheckMethod")));
            this.txt_ShuturyokuKomokuMeisai.Size = new System.Drawing.Size(192, 223);
            this.txt_ShuturyokuKomokuMeisai.TabIndex = 600;
            // 
            // btn_DenpyoInsert
            // 
            this.btn_DenpyoInsert.DefaultBackColor = System.Drawing.Color.Empty;
            this.btn_DenpyoInsert.Location = new System.Drawing.Point(210, 222);
            this.btn_DenpyoInsert.Name = "btn_DenpyoInsert";
            this.btn_DenpyoInsert.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btn_DenpyoInsert.Size = new System.Drawing.Size(76, 20);
            this.btn_DenpyoInsert.TabIndex = 601;
            this.btn_DenpyoInsert.Text = "F1：　　追加";
            this.btn_DenpyoInsert.UseVisualStyleBackColor = true;
            // 
            // btn_DenpyoDelete
            // 
            this.btn_DenpyoDelete.DefaultBackColor = System.Drawing.Color.Empty;
            this.btn_DenpyoDelete.Location = new System.Drawing.Point(210, 248);
            this.btn_DenpyoDelete.Name = "btn_DenpyoDelete";
            this.btn_DenpyoDelete.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btn_DenpyoDelete.Size = new System.Drawing.Size(76, 20);
            this.btn_DenpyoDelete.TabIndex = 602;
            this.btn_DenpyoDelete.Text = "F2：　　削除";
            this.btn_DenpyoDelete.UseVisualStyleBackColor = true;
            // 
            // btn_DenpyoAllDelete
            // 
            this.btn_DenpyoAllDelete.DefaultBackColor = System.Drawing.Color.Empty;
            this.btn_DenpyoAllDelete.Location = new System.Drawing.Point(210, 274);
            this.btn_DenpyoAllDelete.Name = "btn_DenpyoAllDelete";
            this.btn_DenpyoAllDelete.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btn_DenpyoAllDelete.Size = new System.Drawing.Size(76, 20);
            this.btn_DenpyoAllDelete.TabIndex = 603;
            this.btn_DenpyoAllDelete.Text = "F3：　全削除";
            this.btn_DenpyoAllDelete.UseVisualStyleBackColor = true;
            // 
            // btn_MeisaiAllDelete
            // 
            this.btn_MeisaiAllDelete.DefaultBackColor = System.Drawing.Color.Empty;
            this.btn_MeisaiAllDelete.Location = new System.Drawing.Point(702, 274);
            this.btn_MeisaiAllDelete.Name = "btn_MeisaiAllDelete";
            this.btn_MeisaiAllDelete.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btn_MeisaiAllDelete.Size = new System.Drawing.Size(76, 20);
            this.btn_MeisaiAllDelete.TabIndex = 606;
            this.btn_MeisaiAllDelete.Text = "F6：　全削除";
            this.btn_MeisaiAllDelete.UseVisualStyleBackColor = true;
            // 
            // btn_MeisaiDelete
            // 
            this.btn_MeisaiDelete.DefaultBackColor = System.Drawing.Color.Empty;
            this.btn_MeisaiDelete.Location = new System.Drawing.Point(702, 248);
            this.btn_MeisaiDelete.Name = "btn_MeisaiDelete";
            this.btn_MeisaiDelete.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btn_MeisaiDelete.Size = new System.Drawing.Size(76, 20);
            this.btn_MeisaiDelete.TabIndex = 605;
            this.btn_MeisaiDelete.Text = "F5：　　削除";
            this.btn_MeisaiDelete.UseVisualStyleBackColor = true;
            // 
            // btn_MeisaiInsert
            // 
            this.btn_MeisaiInsert.DefaultBackColor = System.Drawing.Color.Empty;
            this.btn_MeisaiInsert.Location = new System.Drawing.Point(702, 222);
            this.btn_MeisaiInsert.Name = "btn_MeisaiInsert";
            this.btn_MeisaiInsert.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btn_MeisaiInsert.Size = new System.Drawing.Size(76, 20);
            this.btn_MeisaiInsert.TabIndex = 604;
            this.btn_MeisaiInsert.Text = "F4：　　追加";
            this.btn_MeisaiInsert.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdo_KikanKotei);
            this.panel1.Controls.Add(this.rdo_Togetu);
            this.panel1.Controls.Add(this.rdo_Tojitu);
            this.panel1.Controls.Add(this.txt_DateHaniShurui);
            this.panel1.Location = new System.Drawing.Point(394, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(278, 26);
            this.panel1.TabIndex = 607;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1014, 458);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_MeisaiAllDelete);
            this.Controls.Add(this.btn_MeisaiDelete);
            this.Controls.Add(this.btn_MeisaiInsert);
            this.Controls.Add(this.btn_DenpyoAllDelete);
            this.Controls.Add(this.btn_DenpyoDelete);
            this.Controls.Add(this.btn_DenpyoInsert);
            this.Controls.Add(this.txt_ShuturyokuKomokuMeisai);
            this.Controls.Add(this.txt_ShuturyokuKanoKomokuMeisai);
            this.Controls.Add(this.txt_ShuturyokuKomokuDenpyo);
            this.Controls.Add(this.txt_ShuturyokuKanoKomokuDenpyo);
            this.Controls.Add(this.lbl_ShuturyokuKomokuMeisai);
            this.Controls.Add(this.lbl_ShuturyokuKanoKomokuMeisai);
            this.Controls.Add(this.lbl_ShuturyokuKomokuDenpyo);
            this.Controls.Add(this.lbl_ShuturyokuKanoKomokuDenpyo);
            this.Controls.Add(this.customButton1);
            this.Controls.Add(this.customButton2);
            this.Controls.Add(this.customTextBox1);
            this.Controls.Add(this.customAlphaNumTextBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.customTextBox2);
            this.Controls.Add(this.customAlphaNumTextBox2);
            this.Controls.Add(this.customComboBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_Komoku2To);
            this.Controls.Add(this.btn_Komoku1To);
            this.Controls.Add(this.btn_Komoku2From);
            this.Controls.Add(this.btn_Komoku1From);
            this.Controls.Add(this.txt_Komoku2ToName);
            this.Controls.Add(this.txt_Komoku2To);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_Komoku2FromName);
            this.Controls.Add(this.txt_Komoku2From);
            this.Controls.Add(this.cmb_Komoku2);
            this.Controls.Add(this.lbl_Komoku2);
            this.Controls.Add(this.txt_Komoku1ToName);
            this.Controls.Add(this.txt_Komoku1To);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_Komoku1FromName);
            this.Controls.Add(this.txt_Komoku1From);
            this.Controls.Add(this.cmb_Komoku1);
            this.Controls.Add(this.lbl_Komoku1);
            this.Controls.Add(this.lbl_ShukeiKoumoku);
            this.Controls.Add(this.btn_KyotenSearch);
            this.Controls.Add(this.txt_KyotenName);
            this.Controls.Add(this.txt_KyotenCD);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtp_DateHaniShiteiTo);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.lbl_DateHaniShitei);
            this.Controls.Add(this.dtp_DateHaniShiteiFrom);
            this.Controls.Add(this.lbl_TyohyoName);
            this.Controls.Add(this.txt_TyohyoName);
            this.Name = "UIForm";
            this.Text = "UIForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_TyohyoName;
        private r_framework.CustomControl.CustomTextBox txt_TyohyoName;
        private r_framework.CustomControl.CustomDateTimePicker dtp_DateHaniShiteiTo;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label lbl_DateHaniShitei;
        private r_framework.CustomControl.CustomDateTimePicker dtp_DateHaniShiteiFrom;
        private r_framework.CustomControl.CustomNumericTextBox2 txt_DateHaniShurui;
        private r_framework.CustomControl.CustomRadioButton rdo_Tojitu;
        private r_framework.CustomControl.CustomRadioButton rdo_Togetu;
        private r_framework.CustomControl.CustomRadioButton rdo_KikanKotei;
        private System.Windows.Forms.Label label1;
        private r_framework.CustomControl.CustomNumericTextBox2 txt_KyotenCD;
        private r_framework.CustomControl.CustomTextBox txt_KyotenName;
        private r_framework.CustomControl.CustomButton btn_KyotenSearch;
        private System.Windows.Forms.Label lbl_ShukeiKoumoku;
        private System.Windows.Forms.Label lbl_Komoku1;
        private r_framework.CustomControl.CustomComboBox cmb_Komoku1;
        private r_framework.CustomControl.CustomAlphaNumTextBox txt_Komoku1From;
        private r_framework.CustomControl.CustomTextBox txt_Komoku1FromName;
        private System.Windows.Forms.Label label2;
        private r_framework.CustomControl.CustomTextBox txt_Komoku1ToName;
        private r_framework.CustomControl.CustomAlphaNumTextBox txt_Komoku1To;
        private r_framework.CustomControl.CustomTextBox txt_Komoku2ToName;
        private r_framework.CustomControl.CustomAlphaNumTextBox txt_Komoku2To;
        private System.Windows.Forms.Label label3;
        private r_framework.CustomControl.CustomTextBox txt_Komoku2FromName;
        private r_framework.CustomControl.CustomAlphaNumTextBox txt_Komoku2From;
        private r_framework.CustomControl.CustomComboBox cmb_Komoku2;
        private System.Windows.Forms.Label lbl_Komoku2;
        private r_framework.CustomControl.CustomButton btn_Komoku1From;
        private r_framework.CustomControl.CustomButton btn_Komoku2From;
        private r_framework.CustomControl.CustomButton btn_Komoku1To;
        private r_framework.CustomControl.CustomButton btn_Komoku2To;
        private r_framework.CustomControl.CustomButton customButton1;
        private r_framework.CustomControl.CustomButton customButton2;
        private r_framework.CustomControl.CustomTextBox customTextBox1;
        private r_framework.CustomControl.CustomAlphaNumTextBox customAlphaNumTextBox1;
        private System.Windows.Forms.Label label4;
        private r_framework.CustomControl.CustomTextBox customTextBox2;
        private r_framework.CustomControl.CustomAlphaNumTextBox customAlphaNumTextBox2;
        private r_framework.CustomControl.CustomComboBox customComboBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_ShuturyokuKanoKomokuDenpyo;
        private System.Windows.Forms.Label lbl_ShuturyokuKomokuDenpyo;
        private System.Windows.Forms.Label lbl_ShuturyokuKanoKomokuMeisai;
        private System.Windows.Forms.Label lbl_ShuturyokuKomokuMeisai;
        private r_framework.CustomControl.CustomTextBox txt_ShuturyokuKanoKomokuDenpyo;
        private r_framework.CustomControl.CustomTextBox txt_ShuturyokuKomokuDenpyo;
        private r_framework.CustomControl.CustomTextBox txt_ShuturyokuKanoKomokuMeisai;
        private r_framework.CustomControl.CustomTextBox txt_ShuturyokuKomokuMeisai;
        private r_framework.CustomControl.CustomButton btn_DenpyoInsert;
        private r_framework.CustomControl.CustomButton btn_DenpyoDelete;
        private r_framework.CustomControl.CustomButton btn_DenpyoAllDelete;
        private r_framework.CustomControl.CustomButton btn_MeisaiAllDelete;
        private r_framework.CustomControl.CustomButton btn_MeisaiDelete;
        private r_framework.CustomControl.CustomButton btn_MeisaiInsert;
        private r_framework.CustomControl.CustomPanel panel1;


    }
}