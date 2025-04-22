namespace ItakuKeiyakushoKyokashoKigenHoshu.APP
{
    partial class ItakuKeiyakushoKyokashoKigenHoshuForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItakuKeiyakushoKyokashoKigenHoshuForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.ShortcutKeyManager shortcutKeyManager1 = new GrapeCity.Win.MultiRow.ShortcutKeyManager();
            this.btnSearchGenba = new r_framework.CustomControl.CustomPopupOpenButton();
            this.btnSearchGyousha = new r_framework.CustomControl.CustomPopupOpenButton();
            this.KigenTo = new r_framework.CustomControl.CustomDateTimePicker();
            this.KigenFrom = new r_framework.CustomControl.CustomDateTimePicker();
            this.GyouseikyokaKubunName = new r_framework.CustomControl.CustomTextBox();
            this.GyouseikyokaKubunCode = new r_framework.CustomControl.CustomNumericTextBox2();
            this.KyokaNo = new r_framework.CustomControl.CustomNumericTextBox2();
            this.ChiikiName = new r_framework.CustomControl.CustomTextBox();
            this.GenbaName = new r_framework.CustomControl.CustomTextBox();
            this.ChiikiCode = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GenbaCode = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GyoushaName = new r_framework.CustomControl.CustomTextBox();
            this.Jigyoushakubun = new r_framework.CustomControl.CustomNumericTextBox2();
            this.GyoushaCode = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.Jigyoushakubun4 = new r_framework.CustomControl.CustomRadioButton();
            this.Jigyoushakubun3 = new r_framework.CustomControl.CustomRadioButton();
            this.Jigyoushakubun2 = new r_framework.CustomControl.CustomRadioButton();
            this.Jigyoushakubun1 = new r_framework.CustomControl.CustomRadioButton();
            this.label24 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.Ichiran = new r_framework.CustomControl.GcCustomMultiRow(this.components);
            this.itakuKeiyakushoKyokashoKigenHoshuDetail2 = new ItakuKeiyakushoKyokashoKigenHoshu.MultiRowTemplate.ItakuKeiyakushoKyokashoKigenHoshuDetail();
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearchGenba
            // 
            this.btnSearchGenba.BackColor = System.Drawing.SystemColors.Control;
            this.btnSearchGenba.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.btnSearchGenba.DBFieldsName = null;
            this.btnSearchGenba.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnSearchGenba.DisplayItemName = null;
            this.btnSearchGenba.DisplayPopUp = null;
            this.btnSearchGenba.ErrorMessage = null;
            this.btnSearchGenba.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnSearchGenba.FocusOutCheckMethod")));
            this.btnSearchGenba.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSearchGenba.GetCodeMasterField = null;
            this.btnSearchGenba.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchGenba.Image")));
            this.btnSearchGenba.ItemDefinedTypes = null;
            this.btnSearchGenba.LinkedSettingTextBox = null;
            this.btnSearchGenba.LinkedTextBoxs = new string[] {
        "GenbaCode"};
            this.btnSearchGenba.Location = new System.Drawing.Point(441, 65);
            this.btnSearchGenba.Name = "btnSearchGenba";
            this.btnSearchGenba.PopupAfterExecute = null;
            this.btnSearchGenba.PopupBeforeExecute = null;
            this.btnSearchGenba.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GENBA_CD,GENBA_NAME_RYAKU";
            this.btnSearchGenba.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("btnSearchGenba.PopupSearchSendParams")));
            this.btnSearchGenba.PopupSetFormField = "GyoushaCode,GyoushaName,GenbaCode,GenbaName";
            this.btnSearchGenba.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.btnSearchGenba.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.btnSearchGenba.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("btnSearchGenba.popupWindowSetting")));
            this.btnSearchGenba.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnSearchGenba.RegistCheckMethod")));
            this.btnSearchGenba.SearchDisplayFlag = 0;
            this.btnSearchGenba.SetFormField = null;
            this.btnSearchGenba.ShortItemName = null;
            this.btnSearchGenba.Size = new System.Drawing.Size(22, 22);
            this.btnSearchGenba.TabIndex = 10;
            this.btnSearchGenba.TabStop = false;
            this.btnSearchGenba.UseVisualStyleBackColor = false;
            this.btnSearchGenba.ZeroPaddengFlag = false;
            // 
            // btnSearchGyousha
            // 
            this.btnSearchGyousha.BackColor = System.Drawing.SystemColors.Control;
            this.btnSearchGyousha.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.btnSearchGyousha.DBFieldsName = null;
            this.btnSearchGyousha.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnSearchGyousha.DisplayItemName = null;
            this.btnSearchGyousha.DisplayPopUp = null;
            this.btnSearchGyousha.ErrorMessage = null;
            this.btnSearchGyousha.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnSearchGyousha.FocusOutCheckMethod")));
            this.btnSearchGyousha.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnSearchGyousha.GetCodeMasterField = null;
            this.btnSearchGyousha.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchGyousha.Image")));
            this.btnSearchGyousha.ItemDefinedTypes = null;
            this.btnSearchGyousha.LinkedSettingTextBox = null;
            this.btnSearchGyousha.LinkedTextBoxs = new string[] {
        "GyoushaCode"};
            this.btnSearchGyousha.Location = new System.Drawing.Point(441, 39);
            this.btnSearchGyousha.Name = "btnSearchGyousha";
            this.btnSearchGyousha.PopupAfterExecute = null;
            this.btnSearchGyousha.PopupAfterExecuteMethod = "GyoushaCheck";
            this.btnSearchGyousha.PopupBeforeExecute = null;
            this.btnSearchGyousha.PopupBeforeExecuteMethod = "Gyousha_PopupBeforeExecuteMethod";
            this.btnSearchGyousha.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.btnSearchGyousha.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("btnSearchGyousha.PopupSearchSendParams")));
            this.btnSearchGyousha.PopupSetFormField = "GyoushaCode,GyoushaName";
            this.btnSearchGyousha.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.btnSearchGyousha.PopupWindowName = "検索共通ポップアップ";
            this.btnSearchGyousha.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("btnSearchGyousha.popupWindowSetting")));
            this.btnSearchGyousha.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnSearchGyousha.RegistCheckMethod")));
            this.btnSearchGyousha.SearchDisplayFlag = 0;
            this.btnSearchGyousha.SetFormField = null;
            this.btnSearchGyousha.ShortItemName = null;
            this.btnSearchGyousha.Size = new System.Drawing.Size(22, 22);
            this.btnSearchGyousha.TabIndex = 7;
            this.btnSearchGyousha.TabStop = false;
            this.btnSearchGyousha.UseVisualStyleBackColor = false;
            this.btnSearchGyousha.ZeroPaddengFlag = false;
            // 
            // KigenTo
            // 
            this.KigenTo.BackColor = System.Drawing.SystemColors.Window;
            this.KigenTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KigenTo.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.KigenTo.Checked = false;
            this.KigenTo.CustomFormat = "yyyy/MM/dd ddd";
            this.KigenTo.DateTimeNowYear = "";
            this.KigenTo.DefaultBackColor = System.Drawing.Color.Empty;
            this.KigenTo.DisplayItemName = "期限終了日";
            this.KigenTo.DisplayPopUp = null;
            this.KigenTo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KigenTo.FocusOutCheckMethod")));
            this.KigenTo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KigenTo.ForeColor = System.Drawing.Color.Black;
            this.KigenTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.KigenTo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.KigenTo.IsInputErrorOccured = false;
            this.KigenTo.Location = new System.Drawing.Point(259, 144);
            this.KigenTo.MaxLength = 10;
            this.KigenTo.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.KigenTo.Name = "KigenTo";
            this.KigenTo.NullValue = "";
            this.KigenTo.PopupAfterExecute = null;
            this.KigenTo.PopupBeforeExecute = null;
            this.KigenTo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KigenTo.PopupSearchSendParams")));
            this.KigenTo.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KigenTo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KigenTo.popupWindowSetting")));
            this.KigenTo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KigenTo.RegistCheckMethod")));
            this.KigenTo.Size = new System.Drawing.Size(126, 20);
            this.KigenTo.TabIndex = 16;
            this.KigenTo.Tag = "期間終了日を入力してください";
            this.KigenTo.Text = "2013/09/13(金)";
            this.KigenTo.Value = new System.DateTime(2013, 9, 13, 0, 0, 0, 0);
            this.KigenTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KigenTo_KeyDown);
            // 
            // KigenFrom
            // 
            this.KigenFrom.BackColor = System.Drawing.SystemColors.Window;
            this.KigenFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KigenFrom.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.KigenFrom.Checked = false;
            this.KigenFrom.CustomFormat = "yyyy/MM/dd ddd";
            this.KigenFrom.DateTimeNowYear = "";
            this.KigenFrom.DefaultBackColor = System.Drawing.Color.Empty;
            this.KigenFrom.DisplayItemName = "期限開始日";
            this.KigenFrom.DisplayPopUp = null;
            this.KigenFrom.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KigenFrom.FocusOutCheckMethod")));
            this.KigenFrom.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KigenFrom.ForeColor = System.Drawing.Color.Black;
            this.KigenFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.KigenFrom.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.KigenFrom.IsInputErrorOccured = false;
            this.KigenFrom.Location = new System.Drawing.Point(100, 144);
            this.KigenFrom.MaxLength = 10;
            this.KigenFrom.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.KigenFrom.Name = "KigenFrom";
            this.KigenFrom.NullValue = "";
            this.KigenFrom.PopupAfterExecute = null;
            this.KigenFrom.PopupBeforeExecute = null;
            this.KigenFrom.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KigenFrom.PopupSearchSendParams")));
            this.KigenFrom.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KigenFrom.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KigenFrom.popupWindowSetting")));
            this.KigenFrom.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KigenFrom.RegistCheckMethod")));
            this.KigenFrom.Size = new System.Drawing.Size(126, 20);
            this.KigenFrom.TabIndex = 15;
            this.KigenFrom.Tag = "期間開始日を入力してください";
            this.KigenFrom.Text = "2013/09/13(金)";
            this.KigenFrom.Value = new System.DateTime(2013, 9, 13, 0, 0, 0, 0);
            this.KigenFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KigenFrom_KeyDown);
            // 
            // GyouseikyokaKubunName
            // 
            this.GyouseikyokaKubunName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GyouseikyokaKubunName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GyouseikyokaKubunName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.GyouseikyokaKubunName.DefaultBackColor = System.Drawing.Color.Empty;
            this.GyouseikyokaKubunName.DisplayPopUp = null;
            this.GyouseikyokaKubunName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GyouseikyokaKubunName.FocusOutCheckMethod")));
            this.GyouseikyokaKubunName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GyouseikyokaKubunName.ForeColor = System.Drawing.Color.Black;
            this.GyouseikyokaKubunName.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GyouseikyokaKubunName.IsInputErrorOccured = false;
            this.GyouseikyokaKubunName.Location = new System.Drawing.Point(119, 118);
            this.GyouseikyokaKubunName.MaxLength = 0;
            this.GyouseikyokaKubunName.Name = "GyouseikyokaKubunName";
            this.GyouseikyokaKubunName.PopupAfterExecute = null;
            this.GyouseikyokaKubunName.PopupBeforeExecute = null;
            this.GyouseikyokaKubunName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GyouseikyokaKubunName.PopupSearchSendParams")));
            this.GyouseikyokaKubunName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GyouseikyokaKubunName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GyouseikyokaKubunName.popupWindowSetting")));
            this.GyouseikyokaKubunName.ReadOnly = true;
            this.GyouseikyokaKubunName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GyouseikyokaKubunName.RegistCheckMethod")));
            this.GyouseikyokaKubunName.Size = new System.Drawing.Size(95, 20);
            this.GyouseikyokaKubunName.TabIndex = 14;
            this.GyouseikyokaKubunName.TabStop = false;
            this.GyouseikyokaKubunName.Tag = "行政許可区分名が表示されます";
            // 
            // GyouseikyokaKubunCode
            // 
            this.GyouseikyokaKubunCode.BackColor = System.Drawing.SystemColors.Window;
            this.GyouseikyokaKubunCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GyouseikyokaKubunCode.DefaultBackColor = System.Drawing.Color.Empty;
            this.GyouseikyokaKubunCode.DisplayPopUp = null;
            this.GyouseikyokaKubunCode.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GyouseikyokaKubunCode.FocusOutCheckMethod")));
            this.GyouseikyokaKubunCode.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GyouseikyokaKubunCode.ForeColor = System.Drawing.Color.Black;
            this.GyouseikyokaKubunCode.IsInputErrorOccured = false;
            this.GyouseikyokaKubunCode.ItemDefinedTypes = "smallint";
            this.GyouseikyokaKubunCode.Location = new System.Drawing.Point(100, 118);
            this.GyouseikyokaKubunCode.Multiline = true;
            this.GyouseikyokaKubunCode.Name = "GyouseikyokaKubunCode";
            this.GyouseikyokaKubunCode.PopupAfterExecute = null;
            this.GyouseikyokaKubunCode.PopupBeforeExecute = null;
            this.GyouseikyokaKubunCode.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GyouseikyokaKubunCode.PopupSearchSendParams")));
            this.GyouseikyokaKubunCode.PopupSetFormField = "";
            this.GyouseikyokaKubunCode.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GyouseikyokaKubunCode.PopupWindowName = "";
            this.GyouseikyokaKubunCode.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GyouseikyokaKubunCode.popupWindowSetting")));
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
            this.GyouseikyokaKubunCode.RangeSetting = rangeSettingDto1;
            this.GyouseikyokaKubunCode.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GyouseikyokaKubunCode.RegistCheckMethod")));
            this.GyouseikyokaKubunCode.SetFormField = "";
            this.GyouseikyokaKubunCode.Size = new System.Drawing.Size(20, 20);
            this.GyouseikyokaKubunCode.TabIndex = 13;
            this.GyouseikyokaKubunCode.Tag = "行政許可区分を指定してください（1：普通、2：特別）";
            this.GyouseikyokaKubunCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.GyouseikyokaKubunCode.WordWrap = false;
            this.GyouseikyokaKubunCode.TextChanged += new System.EventHandler(this.GyouseikyokaKubunCode_TextChanged);
            // 
            // KyokaNo
            // 
            this.KyokaNo.BackColor = System.Drawing.SystemColors.Window;
            this.KyokaNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KyokaNo.DBFieldsName = "KYOKA_NO";
            this.KyokaNo.DefaultBackColor = System.Drawing.Color.Empty;
            this.KyokaNo.DisplayItemName = "許可番号";
            this.KyokaNo.DisplayPopUp = null;
            this.KyokaNo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KyokaNo.FocusOutCheckMethod")));
            this.KyokaNo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KyokaNo.ForeColor = System.Drawing.Color.Black;
            this.KyokaNo.IsInputErrorOccured = false;
            this.KyokaNo.Location = new System.Drawing.Point(100, 169);
            this.KyokaNo.Name = "KyokaNo";
            this.KyokaNo.PopupAfterExecute = null;
            this.KyokaNo.PopupBeforeExecute = null;
            this.KyokaNo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KyokaNo.PopupSearchSendParams")));
            this.KyokaNo.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KyokaNo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KyokaNo.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.KyokaNo.RangeSetting = rangeSettingDto2;
            this.KyokaNo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KyokaNo.RegistCheckMethod")));
            this.KyokaNo.ShortItemName = "許可番号";
            this.KyokaNo.Size = new System.Drawing.Size(85, 20);
            this.KyokaNo.TabIndex = 17;
            this.KyokaNo.Tag = "半角１１文字以内で入力してください";
            this.KyokaNo.WordWrap = false;
            // 
            // ChiikiName
            // 
            this.ChiikiName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ChiikiName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChiikiName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ChiikiName.DefaultBackColor = System.Drawing.Color.Empty;
            this.ChiikiName.DisplayPopUp = null;
            this.ChiikiName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ChiikiName.FocusOutCheckMethod")));
            this.ChiikiName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ChiikiName.ForeColor = System.Drawing.Color.Black;
            this.ChiikiName.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ChiikiName.IsInputErrorOccured = false;
            this.ChiikiName.Location = new System.Drawing.Point(153, 92);
            this.ChiikiName.MaxLength = 0;
            this.ChiikiName.Name = "ChiikiName";
            this.ChiikiName.PopupAfterExecute = null;
            this.ChiikiName.PopupBeforeExecute = null;
            this.ChiikiName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ChiikiName.PopupSearchSendParams")));
            this.ChiikiName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ChiikiName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ChiikiName.popupWindowSetting")));
            this.ChiikiName.ReadOnly = true;
            this.ChiikiName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ChiikiName.RegistCheckMethod")));
            this.ChiikiName.Size = new System.Drawing.Size(150, 20);
            this.ChiikiName.TabIndex = 12;
            this.ChiikiName.TabStop = false;
            this.ChiikiName.Tag = "地域が表示されます";
            // 
            // GenbaName
            // 
            this.GenbaName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GenbaName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GenbaName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.GenbaName.DefaultBackColor = System.Drawing.Color.Empty;
            this.GenbaName.DisplayPopUp = null;
            this.GenbaName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GenbaName.FocusOutCheckMethod")));
            this.GenbaName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GenbaName.ForeColor = System.Drawing.Color.Black;
            this.GenbaName.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GenbaName.IsInputErrorOccured = false;
            this.GenbaName.Location = new System.Drawing.Point(153, 66);
            this.GenbaName.MaxLength = 0;
            this.GenbaName.Name = "GenbaName";
            this.GenbaName.PopupAfterExecute = null;
            this.GenbaName.PopupBeforeExecute = null;
            this.GenbaName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GenbaName.PopupSearchSendParams")));
            this.GenbaName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GenbaName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GenbaName.popupWindowSetting")));
            this.GenbaName.ReadOnly = true;
            this.GenbaName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GenbaName.RegistCheckMethod")));
            this.GenbaName.Size = new System.Drawing.Size(285, 20);
            this.GenbaName.TabIndex = 9;
            this.GenbaName.TabStop = false;
            this.GenbaName.Tag = "現場が表示されます";
            // 
            // ChiikiCode
            // 
            this.ChiikiCode.BackColor = System.Drawing.SystemColors.Window;
            this.ChiikiCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChiikiCode.ChangeUpperCase = true;
            this.ChiikiCode.CharacterLimitList = null;
            this.ChiikiCode.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.ChiikiCode.DBFieldsName = "CHIIKI_CD";
            this.ChiikiCode.DefaultBackColor = System.Drawing.Color.Empty;
            this.ChiikiCode.DisplayItemName = "地域CD";
            this.ChiikiCode.DisplayPopUp = null;
            this.ChiikiCode.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ChiikiCode.FocusOutCheckMethod")));
            this.ChiikiCode.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ChiikiCode.ForeColor = System.Drawing.Color.Black;
            this.ChiikiCode.GetCodeMasterField = "CHIIKI_CD,CHIIKI_NAME_RYAKU";
            this.ChiikiCode.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ChiikiCode.IsInputErrorOccured = false;
            this.ChiikiCode.ItemDefinedTypes = "varchar";
            this.ChiikiCode.Location = new System.Drawing.Point(100, 92);
            this.ChiikiCode.MaxLength = 6;
            this.ChiikiCode.Name = "ChiikiCode";
            this.ChiikiCode.PopupAfterExecute = null;
            this.ChiikiCode.PopupBeforeExecute = null;
            this.ChiikiCode.PopupGetMasterField = "CHIIKI_CD,CHIIKI_NAME_RYAKU";
            this.ChiikiCode.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ChiikiCode.PopupSearchSendParams")));
            this.ChiikiCode.PopupSetFormField = "ChiikiCode,ChiikiName";
            this.ChiikiCode.PopupWindowId = r_framework.Const.WINDOW_ID.M_CHIIKI;
            this.ChiikiCode.PopupWindowName = "マスタ共通ポップアップ";
            this.ChiikiCode.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ChiikiCode.popupWindowSetting")));
            this.ChiikiCode.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ChiikiCode.RegistCheckMethod")));
            this.ChiikiCode.SetFormField = "ChiikiCode,ChiikiName";
            this.ChiikiCode.ShortItemName = "地域CD";
            this.ChiikiCode.Size = new System.Drawing.Size(55, 20);
            this.ChiikiCode.TabIndex = 11;
            this.ChiikiCode.Tag = "地域を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.ChiikiCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ChiikiCode.ZeroPaddengFlag = true;
            // 
            // GenbaCode
            // 
            this.GenbaCode.BackColor = System.Drawing.SystemColors.Window;
            this.GenbaCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GenbaCode.ChangeUpperCase = true;
            this.GenbaCode.CharacterLimitList = null;
            this.GenbaCode.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.GenbaCode.DBFieldsName = "GENBA_CD";
            this.GenbaCode.DefaultBackColor = System.Drawing.Color.Empty;
            this.GenbaCode.DisplayItemName = "現場CD";
            this.GenbaCode.DisplayPopUp = null;
            this.GenbaCode.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GenbaCode.FocusOutCheckMethod")));
            this.GenbaCode.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GenbaCode.ForeColor = System.Drawing.Color.Black;
            this.GenbaCode.GetCodeMasterField = "GENBA_CD,GENBA_NAME_RYAKU";
            this.GenbaCode.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GenbaCode.IsInputErrorOccured = false;
            this.GenbaCode.ItemDefinedTypes = "varchar";
            this.GenbaCode.Location = new System.Drawing.Point(100, 66);
            this.GenbaCode.MaxLength = 6;
            this.GenbaCode.Name = "GenbaCode";
            this.GenbaCode.PopupAfterExecute = null;
            this.GenbaCode.PopupBeforeExecute = null;
            this.GenbaCode.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GENBA_CD,GENBA_NAME_RYAKU";
            this.GenbaCode.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GenbaCode.PopupSearchSendParams")));
            this.GenbaCode.PopupSetFormField = "GyoushaCode,GyoushaName,GenbaCode,GenbaName";
            this.GenbaCode.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GenbaCode.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GenbaCode.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GenbaCode.popupWindowSetting")));
            this.GenbaCode.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GenbaCode.RegistCheckMethod")));
            this.GenbaCode.SetFormField = "GenbaCode,GenbaName";
            this.GenbaCode.ShortItemName = "現場CD";
            this.GenbaCode.Size = new System.Drawing.Size(55, 20);
            this.GenbaCode.TabIndex = 8;
            this.GenbaCode.Tag = "現場を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GenbaCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.GenbaCode.ZeroPaddengFlag = true;
            this.GenbaCode.Validated += new System.EventHandler(this.GenbaCode_Validated);
            // 
            // GyoushaName
            // 
            this.GyoushaName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GyoushaName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GyoushaName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.GyoushaName.DefaultBackColor = System.Drawing.Color.Empty;
            this.GyoushaName.DisplayPopUp = null;
            this.GyoushaName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GyoushaName.FocusOutCheckMethod")));
            this.GyoushaName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GyoushaName.ForeColor = System.Drawing.Color.Black;
            this.GyoushaName.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GyoushaName.IsInputErrorOccured = false;
            this.GyoushaName.Location = new System.Drawing.Point(154, 40);
            this.GyoushaName.MaxLength = 0;
            this.GyoushaName.Name = "GyoushaName";
            this.GyoushaName.PopupAfterExecute = null;
            this.GyoushaName.PopupBeforeExecute = null;
            this.GyoushaName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GyoushaName.PopupSearchSendParams")));
            this.GyoushaName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GyoushaName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GyoushaName.popupWindowSetting")));
            this.GyoushaName.ReadOnly = true;
            this.GyoushaName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GyoushaName.RegistCheckMethod")));
            this.GyoushaName.Size = new System.Drawing.Size(285, 20);
            this.GyoushaName.TabIndex = 6;
            this.GyoushaName.TabStop = false;
            this.GyoushaName.Tag = "業者が表示されます";
            // 
            // Jigyoushakubun
            // 
            this.Jigyoushakubun.BackColor = System.Drawing.SystemColors.Window;
            this.Jigyoushakubun.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Jigyoushakubun.DefaultBackColor = System.Drawing.Color.Empty;
            this.Jigyoushakubun.DisplayPopUp = null;
            this.Jigyoushakubun.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Jigyoushakubun.FocusOutCheckMethod")));
            this.Jigyoushakubun.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Jigyoushakubun.ForeColor = System.Drawing.Color.Black;
            this.Jigyoushakubun.IsInputErrorOccured = false;
            this.Jigyoushakubun.LinkedRadioButtonArray = new string[] {
        "Jigyoushakubun1",
        "Jigyoushakubun2",
        "Jigyoushakubun3",
        "Jigyoushakubun4"};
            this.Jigyoushakubun.Location = new System.Drawing.Point(101, 12);
            this.Jigyoushakubun.Name = "Jigyoushakubun";
            this.Jigyoushakubun.PopupAfterExecute = null;
            this.Jigyoushakubun.PopupBeforeExecute = null;
            this.Jigyoushakubun.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Jigyoushakubun.PopupSearchSendParams")));
            this.Jigyoushakubun.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Jigyoushakubun.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Jigyoushakubun.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            4,
            0,
            0,
            0});
            rangeSettingDto3.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Jigyoushakubun.RangeSetting = rangeSettingDto3;
            this.Jigyoushakubun.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Jigyoushakubun.RegistCheckMethod")));
            this.Jigyoushakubun.Size = new System.Drawing.Size(22, 20);
            this.Jigyoushakubun.TabIndex = 0;
            this.Jigyoushakubun.Tag = "【1、2、3、4】のいずれかで入力してください";
            this.Jigyoushakubun.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Jigyoushakubun.WordWrap = false;
            // 
            // GyoushaCode
            // 
            this.GyoushaCode.BackColor = System.Drawing.SystemColors.Window;
            this.GyoushaCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GyoushaCode.ChangeUpperCase = true;
            this.GyoushaCode.CharacterLimitList = null;
            this.GyoushaCode.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.GyoushaCode.DBFieldsName = "GYOUSHA_CD";
            this.GyoushaCode.DefaultBackColor = System.Drawing.Color.Empty;
            this.GyoushaCode.DisplayItemName = "業者CD";
            this.GyoushaCode.DisplayPopUp = null;
            this.GyoushaCode.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GyoushaCode.FocusOutCheckMethod")));
            this.GyoushaCode.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GyoushaCode.ForeColor = System.Drawing.Color.Black;
            this.GyoushaCode.GetCodeMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GyoushaCode.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GyoushaCode.IsInputErrorOccured = false;
            this.GyoushaCode.ItemDefinedTypes = "varchar";
            this.GyoushaCode.Location = new System.Drawing.Point(100, 40);
            this.GyoushaCode.MaxLength = 6;
            this.GyoushaCode.Name = "GyoushaCode";
            this.GyoushaCode.PopupAfterExecute = null;
            this.GyoushaCode.PopupAfterExecuteMethod = "GyoushaCheck";
            this.GyoushaCode.PopupBeforeExecute = null;
            this.GyoushaCode.PopupBeforeExecuteMethod = "Gyousha_PopupBeforeExecuteMethod";
            this.GyoushaCode.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GyoushaCode.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GyoushaCode.PopupSearchSendParams")));
            this.GyoushaCode.PopupSetFormField = "GyoushaCode,GyoushaName";
            this.GyoushaCode.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GyoushaCode.PopupWindowName = "検索共通ポップアップ";
            this.GyoushaCode.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GyoushaCode.popupWindowSetting")));
            this.GyoushaCode.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GyoushaCode.RegistCheckMethod")));
            this.GyoushaCode.SetFormField = "GyoushaCode,GyoushaName";
            this.GyoushaCode.ShortItemName = "業者CD";
            this.GyoushaCode.Size = new System.Drawing.Size(55, 20);
            this.GyoushaCode.TabIndex = 5;
            this.GyoushaCode.Tag = "業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GyoushaCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.GyoushaCode.ZeroPaddengFlag = true;
            this.GyoushaCode.Enter += new System.EventHandler(this.GyoushaCode_Enter);
            this.GyoushaCode.Validated += new System.EventHandler(this.GyoushaCode_Validated);
            // 
            // Jigyoushakubun4
            // 
            this.Jigyoushakubun4.AutoSize = true;
            this.Jigyoushakubun4.Checked = true;
            this.Jigyoushakubun4.DefaultBackColor = System.Drawing.Color.Empty;
            this.Jigyoushakubun4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Jigyoushakubun4.FocusOutCheckMethod")));
            this.Jigyoushakubun4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Jigyoushakubun4.LinkedTextBox = "Jigyoushakubun";
            this.Jigyoushakubun4.Location = new System.Drawing.Point(332, 14);
            this.Jigyoushakubun4.Name = "Jigyoushakubun4";
            this.Jigyoushakubun4.PopupAfterExecute = null;
            this.Jigyoushakubun4.PopupBeforeExecute = null;
            this.Jigyoushakubun4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Jigyoushakubun4.PopupSearchSendParams")));
            this.Jigyoushakubun4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Jigyoushakubun4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Jigyoushakubun4.popupWindowSetting")));
            this.Jigyoushakubun4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Jigyoushakubun4.RegistCheckMethod")));
            this.Jigyoushakubun4.Size = new System.Drawing.Size(67, 17);
            this.Jigyoushakubun4.TabIndex = 4;
            this.Jigyoushakubun4.Tag = "委託契約対象区分が「全て」の場合にはチェックを付けてください";
            this.Jigyoushakubun4.Text = "4.全て";
            this.Jigyoushakubun4.UseVisualStyleBackColor = true;
            this.Jigyoushakubun4.Value = "4";
            // 
            // Jigyoushakubun3
            // 
            this.Jigyoushakubun3.AutoSize = true;
            this.Jigyoushakubun3.DefaultBackColor = System.Drawing.Color.Empty;
            this.Jigyoushakubun3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Jigyoushakubun3.FocusOutCheckMethod")));
            this.Jigyoushakubun3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Jigyoushakubun3.LinkedTextBox = "Jigyoushakubun";
            this.Jigyoushakubun3.Location = new System.Drawing.Point(262, 14);
            this.Jigyoushakubun3.Name = "Jigyoushakubun3";
            this.Jigyoushakubun3.PopupAfterExecute = null;
            this.Jigyoushakubun3.PopupBeforeExecute = null;
            this.Jigyoushakubun3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Jigyoushakubun3.PopupSearchSendParams")));
            this.Jigyoushakubun3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Jigyoushakubun3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Jigyoushakubun3.popupWindowSetting")));
            this.Jigyoushakubun3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Jigyoushakubun3.RegistCheckMethod")));
            this.Jigyoushakubun3.Size = new System.Drawing.Size(67, 17);
            this.Jigyoushakubun3.TabIndex = 3;
            this.Jigyoushakubun3.Tag = "委託契約対象区分が「最終」の場合にはチェックを付けてください";
            this.Jigyoushakubun3.Text = "3.最終";
            this.Jigyoushakubun3.UseVisualStyleBackColor = true;
            this.Jigyoushakubun3.Value = "3";
            // 
            // Jigyoushakubun2
            // 
            this.Jigyoushakubun2.AutoSize = true;
            this.Jigyoushakubun2.DefaultBackColor = System.Drawing.Color.Empty;
            this.Jigyoushakubun2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Jigyoushakubun2.FocusOutCheckMethod")));
            this.Jigyoushakubun2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Jigyoushakubun2.LinkedTextBox = "Jigyoushakubun";
            this.Jigyoushakubun2.Location = new System.Drawing.Point(197, 14);
            this.Jigyoushakubun2.Name = "Jigyoushakubun2";
            this.Jigyoushakubun2.PopupAfterExecute = null;
            this.Jigyoushakubun2.PopupBeforeExecute = null;
            this.Jigyoushakubun2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Jigyoushakubun2.PopupSearchSendParams")));
            this.Jigyoushakubun2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Jigyoushakubun2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Jigyoushakubun2.popupWindowSetting")));
            this.Jigyoushakubun2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Jigyoushakubun2.RegistCheckMethod")));
            this.Jigyoushakubun2.Size = new System.Drawing.Size(67, 17);
            this.Jigyoushakubun2.TabIndex = 2;
            this.Jigyoushakubun2.Tag = "委託契約対象区分が「処分」の場合にはチェックを付けてください";
            this.Jigyoushakubun2.Text = "2.処分";
            this.Jigyoushakubun2.UseVisualStyleBackColor = true;
            this.Jigyoushakubun2.Value = "2";
            // 
            // Jigyoushakubun1
            // 
            this.Jigyoushakubun1.AutoSize = true;
            this.Jigyoushakubun1.DefaultBackColor = System.Drawing.Color.Empty;
            this.Jigyoushakubun1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Jigyoushakubun1.FocusOutCheckMethod")));
            this.Jigyoushakubun1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Jigyoushakubun1.LinkedTextBox = "Jigyoushakubun";
            this.Jigyoushakubun1.Location = new System.Drawing.Point(129, 14);
            this.Jigyoushakubun1.Name = "Jigyoushakubun1";
            this.Jigyoushakubun1.PopupAfterExecute = null;
            this.Jigyoushakubun1.PopupBeforeExecute = null;
            this.Jigyoushakubun1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Jigyoushakubun1.PopupSearchSendParams")));
            this.Jigyoushakubun1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Jigyoushakubun1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Jigyoushakubun1.popupWindowSetting")));
            this.Jigyoushakubun1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Jigyoushakubun1.RegistCheckMethod")));
            this.Jigyoushakubun1.Size = new System.Drawing.Size(67, 17);
            this.Jigyoushakubun1.TabIndex = 1;
            this.Jigyoushakubun1.Tag = "委託契約対象区分が「運搬」の場合にはチェックを付けてください";
            this.Jigyoushakubun1.Text = "1.運搬";
            this.Jigyoushakubun1.UseVisualStyleBackColor = true;
            this.Jigyoushakubun1.Value = "1";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label24.Location = new System.Drawing.Point(232, 147);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(21, 13);
            this.label24.TabIndex = 624;
            this.label24.Text = "～";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(1, 169);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 20);
            this.label6.TabIndex = 623;
            this.label6.Text = "許可番号";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(1, 143);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 20);
            this.label5.TabIndex = 622;
            this.label5.Text = "期限";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(1, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 20);
            this.label4.TabIndex = 621;
            this.label4.Text = "行政許可区分";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(1, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 20);
            this.label3.TabIndex = 620;
            this.label3.Text = "地域";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(1, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 20);
            this.label2.TabIndex = 619;
            this.label2.Text = "現場";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 20);
            this.label1.TabIndex = 618;
            this.label1.Text = "業者";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ISNOT_NEED_DELETE_FLG
            // 
            this.ISNOT_NEED_DELETE_FLG.BackColor = System.Drawing.SystemColors.Window;
            this.ISNOT_NEED_DELETE_FLG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ISNOT_NEED_DELETE_FLG.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ISNOT_NEED_DELETE_FLG.DBFieldsName = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.ISNOT_NEED_DELETE_FLG.DisplayPopUp = null;
            this.ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ISNOT_NEED_DELETE_FLG.ForeColor = System.Drawing.Color.Black;
            this.ISNOT_NEED_DELETE_FLG.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ISNOT_NEED_DELETE_FLG.IsInputErrorOccured = false;
            this.ISNOT_NEED_DELETE_FLG.ItemDefinedTypes = "BIT";
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(965, 169);
            this.ISNOT_NEED_DELETE_FLG.MaxLength = 0;
            this.ISNOT_NEED_DELETE_FLG.Name = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.PopupAfterExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupBeforeExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.PopupSearchSendParams")));
            this.ISNOT_NEED_DELETE_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ISNOT_NEED_DELETE_FLG.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.popupWindowSetting")));
            this.ISNOT_NEED_DELETE_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.RegistCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.Size = new System.Drawing.Size(33, 20);
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 625;
            this.ISNOT_NEED_DELETE_FLG.TabStop = false;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "true";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // Ichiran
            // 
            this.Ichiran.AllowUserToAddRows = false;
            this.Ichiran.AllowUserToDeleteRows = false;
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.Ichiran.ColumnHeadersDefaultHeaderCellStyle = cellStyle1;
            this.Ichiran.CurrentRowBorderLine = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Medium, System.Drawing.Color.Red);
            this.Ichiran.EditMode = GrapeCity.Win.MultiRow.EditMode.EditOnEnter;
            this.Ichiran.Location = new System.Drawing.Point(1, 195);
            this.Ichiran.MultiSelect = false;
            this.Ichiran.Name = "Ichiran";
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveUp)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveDown)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveLeft)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveRight)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftLeft)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftRight)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCellInRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCellInRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCellInRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCellInRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.PageUp)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Next)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Space)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.CommitRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Clear)), System.Windows.Forms.Keys.Delete));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.NumPad0)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), System.Windows.Forms.Keys.F4));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollUp)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollDown)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollLeft)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollRight)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToFirstPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToLastPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToPreviousPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Space)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToNextPage)), System.Windows.Forms.Keys.Space));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToFirstPage)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToFirstPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToLastPage)), System.Windows.Forms.Keys.End));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToLastPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ReverseSelectCurrentRow)), System.Windows.Forms.Keys.Space));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToPreviousPage)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToNextPage)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToPreviousRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToPreviousRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToNextRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToNextRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.PageUp)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Next)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.CommitRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Clear)), System.Windows.Forms.Keys.Delete));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.NumPad0)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), System.Windows.Forms.Keys.F4));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            this.Ichiran.ShortcutKeyManager = shortcutKeyManager1;
            this.Ichiran.ShowCellToolTips = false;
            this.Ichiran.Size = new System.Drawing.Size(997, 253);
            this.Ichiran.TabIndex = 18;
            this.Ichiran.Template = this.itakuKeiyakushoKyokashoKigenHoshuDetail2;
            this.Ichiran.Text = "gcMultiRow8";
            // 
            // ItakuKeiyakushoKyokashoKigenHoshuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(999, 530);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.btnSearchGenba);
            this.Controls.Add(this.btnSearchGyousha);
            this.Controls.Add(this.KigenTo);
            this.Controls.Add(this.KigenFrom);
            this.Controls.Add(this.GyouseikyokaKubunName);
            this.Controls.Add(this.GyouseikyokaKubunCode);
            this.Controls.Add(this.KyokaNo);
            this.Controls.Add(this.ChiikiName);
            this.Controls.Add(this.GenbaName);
            this.Controls.Add(this.ChiikiCode);
            this.Controls.Add(this.GenbaCode);
            this.Controls.Add(this.GyoushaName);
            this.Controls.Add(this.Jigyoushakubun);
            this.Controls.Add(this.GyoushaCode);
            this.Controls.Add(this.Jigyoushakubun4);
            this.Controls.Add(this.Jigyoushakubun3);
            this.Controls.Add(this.Jigyoushakubun2);
            this.Controls.Add(this.Jigyoushakubun1);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Ichiran);
            this.Name = "ItakuKeiyakushoKyokashoKigenHoshuForm";
            this.Text = "ItakukeiyakushokyokaShoukigenKanriForm";
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        internal r_framework.CustomControl.CustomAlphaNumTextBox ChiikiCode;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GenbaCode;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GyoushaCode;
        internal r_framework.CustomControl.CustomNumericTextBox2 GyouseikyokaKubunCode;
        private ItakuKeiyakushoKyokashoKigenHoshu.MultiRowTemplate.ItakuKeiyakushoKyokashoKigenHoshuDetail itakuKeiyakushoKyokashoKigenHoshuDetail1;
        internal r_framework.CustomControl.CustomNumericTextBox2 Jigyoushakubun;
        internal r_framework.CustomControl.CustomDateTimePicker KigenTo;
        internal r_framework.CustomControl.CustomNumericTextBox2 KyokaNo;
        internal r_framework.CustomControl.CustomDateTimePicker KigenFrom;
        private MultiRowTemplate.ItakuKeiyakushoKyokashoKigenHoshuDetail itakuKeiyakushoKyokashoKigenHoshuDetail2;
        internal r_framework.CustomControl.CustomTextBox GyoushaName;
        internal r_framework.CustomControl.CustomTextBox GyouseikyokaKubunName;
        internal r_framework.CustomControl.CustomTextBox GenbaName;
        internal r_framework.CustomControl.CustomTextBox ChiikiName;
        internal r_framework.CustomControl.GcCustomMultiRow Ichiran;
        internal r_framework.CustomControl.CustomPopupOpenButton btnSearchGenba;
        internal r_framework.CustomControl.CustomPopupOpenButton btnSearchGyousha;
        internal r_framework.CustomControl.CustomRadioButton Jigyoushakubun4;
        internal r_framework.CustomControl.CustomRadioButton Jigyoushakubun3;
        internal r_framework.CustomControl.CustomRadioButton Jigyoushakubun2;
        internal r_framework.CustomControl.CustomRadioButton Jigyoushakubun1;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
    }
}
