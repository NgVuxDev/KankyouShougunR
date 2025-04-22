namespace Shougun.Core.Stock.ZaikoIdou
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.IDOU_NUMBER = new r_framework.CustomControl.CustomNumericTextBox2();
            this.NEXT_BUTTON = new System.Windows.Forms.Button();
            this.PREV_BUTTON = new System.Windows.Forms.Button();
            this.lblIdouNumber = new System.Windows.Forms.Label();
            this.IDOU_DATE = new r_framework.CustomControl.CustomDateTimePicker();
            this.lblNyuuryokuDate = new System.Windows.Forms.Label();
            this.GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GYOUSHA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.lblGyousha = new System.Windows.Forms.Label();
            this.GYOUSHA_POPUP = new r_framework.CustomControl.CustomPopupOpenButton();
            this.DETAIL_Ichiran = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.MEISAI_GENBA_CD = new r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn();
            this.MEISAI_GENBA_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.MEISAI_IDOU_BEFORE_ZAIKO_RYOU = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.lbl2 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.MEISAI_IDOU_RYOU = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.lbl1 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.MEISAI_IDOU_AFTER_ZAIKO_RYOU = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.GENBA_POPUP = new r_framework.CustomControl.CustomPopupOpenButton();
            this.GENBA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GENBA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.lblGenba = new System.Windows.Forms.Label();
            this.ZAIKO_HINMEI_POPUP = new r_framework.CustomControl.CustomPopupOpenButton();
            this.ZAIKO_HINMEI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.ZAIKO_HINMEI_NAME = new r_framework.CustomControl.CustomTextBox();
            this.lblZaikoHinmei = new System.Windows.Forms.Label();
            this.lblBeforeIdou = new System.Windows.Forms.Label();
            this.IDOU_BEFORE_ZAIKO_RYOU = new r_framework.CustomControl.CustomNumericTextBox2();
            this.IDOU_RYOU_GOUKEI = new r_framework.CustomControl.CustomNumericTextBox2();
            this.lblIdouGoukei = new System.Windows.Forms.Label();
            this.IDOU_AFTER_ZAIKO_RYOU = new r_framework.CustomControl.CustomNumericTextBox2();
            this.lblAferIdou = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DETAIL_Ichiran)).BeginInit();
            this.SuspendLayout();
            // 
            // IDOU_NUMBER
            // 
            this.IDOU_NUMBER.BackColor = System.Drawing.SystemColors.Window;
            this.IDOU_NUMBER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IDOU_NUMBER.DefaultBackColor = System.Drawing.Color.Empty;
            this.IDOU_NUMBER.DisplayPopUp = null;
            this.IDOU_NUMBER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("IDOU_NUMBER.FocusOutCheckMethod")));
            this.IDOU_NUMBER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.IDOU_NUMBER.ForeColor = System.Drawing.Color.Black;
            this.IDOU_NUMBER.FormatSetting = "数値(#)フォーマット";
            this.IDOU_NUMBER.IsInputErrorOccured = false;
            this.IDOU_NUMBER.Location = new System.Drawing.Point(355, 6);
            this.IDOU_NUMBER.Name = "IDOU_NUMBER";
            this.IDOU_NUMBER.PopupAfterExecute = null;
            this.IDOU_NUMBER.PopupBeforeExecute = null;
            this.IDOU_NUMBER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("IDOU_NUMBER.PopupSearchSendParams")));
            this.IDOU_NUMBER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.IDOU_NUMBER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("IDOU_NUMBER.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.IDOU_NUMBER.RangeSetting = rangeSettingDto1;
            this.IDOU_NUMBER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("IDOU_NUMBER.RegistCheckMethod")));
            this.IDOU_NUMBER.Size = new System.Drawing.Size(84, 20);
            this.IDOU_NUMBER.TabIndex = 1;
            this.IDOU_NUMBER.TabStop = false;
            this.IDOU_NUMBER.Tag = "半角10文字以内で入力してください。";
            this.IDOU_NUMBER.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.IDOU_NUMBER.WordWrap = false;
            this.IDOU_NUMBER.Validated += new System.EventHandler(this.IDOU_NUMBER_Validated);
            // 
            // NEXT_BUTTON
            // 
            this.NEXT_BUTTON.BackColor = System.Drawing.SystemColors.Control;
            this.NEXT_BUTTON.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.NEXT_BUTTON.Location = new System.Drawing.Point(466, 5);
            this.NEXT_BUTTON.Name = "NEXT_BUTTON";
            this.NEXT_BUTTON.Size = new System.Drawing.Size(22, 22);
            this.NEXT_BUTTON.TabIndex = 3;
            this.NEXT_BUTTON.TabStop = false;
            this.NEXT_BUTTON.Tag = "次の伝票を表示します";
            this.NEXT_BUTTON.Text = "次";
            this.NEXT_BUTTON.UseVisualStyleBackColor = false;
            // 
            // PREV_BUTTON
            // 
            this.PREV_BUTTON.BackColor = System.Drawing.SystemColors.Control;
            this.PREV_BUTTON.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.PREV_BUTTON.Location = new System.Drawing.Point(443, 5);
            this.PREV_BUTTON.Name = "PREV_BUTTON";
            this.PREV_BUTTON.Size = new System.Drawing.Size(22, 22);
            this.PREV_BUTTON.TabIndex = 2;
            this.PREV_BUTTON.TabStop = false;
            this.PREV_BUTTON.Tag = "前の伝票を表示します";
            this.PREV_BUTTON.Text = "前";
            this.PREV_BUTTON.UseVisualStyleBackColor = false;
            // 
            // lblIdouNumber
            // 
            this.lblIdouNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblIdouNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIdouNumber.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblIdouNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblIdouNumber.ForeColor = System.Drawing.Color.White;
            this.lblIdouNumber.Location = new System.Drawing.Point(240, 6);
            this.lblIdouNumber.Name = "lblIdouNumber";
            this.lblIdouNumber.Size = new System.Drawing.Size(110, 20);
            this.lblIdouNumber.TabIndex = 0;
            this.lblIdouNumber.Tag = "";
            this.lblIdouNumber.Text = "移動番号";
            this.lblIdouNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IDOU_DATE
            // 
            this.IDOU_DATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.IDOU_DATE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IDOU_DATE.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.IDOU_DATE.Checked = false;
            this.IDOU_DATE.CustomFormat = "yyyy/MM/dd(ddd)";
            this.IDOU_DATE.DateTimeNowYear = "";
            this.IDOU_DATE.DBFieldsName = "IDOU_DATE";
            this.IDOU_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            this.IDOU_DATE.DisplayItemName = "移動日付";
            this.IDOU_DATE.DisplayPopUp = null;
            this.IDOU_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("IDOU_DATE.FocusOutCheckMethod")));
            this.IDOU_DATE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.IDOU_DATE.ForeColor = System.Drawing.Color.Black;
            this.IDOU_DATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.IDOU_DATE.IsInputErrorOccured = false;
            this.IDOU_DATE.ItemDefinedTypes = "datetime";
            this.IDOU_DATE.Location = new System.Drawing.Point(120, 7);
            this.IDOU_DATE.MaxLength = 10;
            this.IDOU_DATE.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.IDOU_DATE.Name = "IDOU_DATE";
            this.IDOU_DATE.NullValue = "";
            this.IDOU_DATE.PopupAfterExecute = null;
            this.IDOU_DATE.PopupBeforeExecute = null;
            this.IDOU_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("IDOU_DATE.PopupSearchSendParams")));
            this.IDOU_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.IDOU_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("IDOU_DATE.popupWindowSetting")));
            this.IDOU_DATE.ReadOnly = true;
            this.IDOU_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("IDOU_DATE.RegistCheckMethod")));
            this.IDOU_DATE.Size = new System.Drawing.Size(109, 20);
            this.IDOU_DATE.TabIndex = 10013;
            this.IDOU_DATE.TabStop = false;
            this.IDOU_DATE.Tag = "入力日付が表示されます";
            this.IDOU_DATE.Text = "2013/12/03(火)";
            this.IDOU_DATE.Value = new System.DateTime(2013, 12, 3, 0, 0, 0, 0);
            // 
            // lblNyuuryokuDate
            // 
            this.lblNyuuryokuDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblNyuuryokuDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNyuuryokuDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblNyuuryokuDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNyuuryokuDate.ForeColor = System.Drawing.Color.White;
            this.lblNyuuryokuDate.Location = new System.Drawing.Point(5, 7);
            this.lblNyuuryokuDate.Name = "lblNyuuryokuDate";
            this.lblNyuuryokuDate.Size = new System.Drawing.Size(110, 20);
            this.lblNyuuryokuDate.TabIndex = 10014;
            this.lblNyuuryokuDate.Text = "入力日付";
            this.lblNyuuryokuDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GYOUSHA_CD
            // 
            this.GYOUSHA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.GYOUSHA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_CD.ChangeUpperCase = true;
            this.GYOUSHA_CD.CharacterLimitList = null;
            this.GYOUSHA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.GYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_CD.DisplayItemName = "業者CD";
            this.GYOUSHA_CD.DisplayPopUp = null;
            this.GYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.FocusOutCheckMethod")));
            this.GYOUSHA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_CD.GetCodeMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GYOUSHA_CD.IsInputErrorOccured = false;
            this.GYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.GYOUSHA_CD.Location = new System.Drawing.Point(120, 33);
            this.GYOUSHA_CD.MaxLength = 6;
            this.GYOUSHA_CD.Name = "GYOUSHA_CD";
            this.GYOUSHA_CD.PopupAfterExecute = null;
            this.GYOUSHA_CD.PopupAfterExecuteMethod = "GyoushaPopupAfter";
            this.GYOUSHA_CD.PopupBeforeExecute = null;
            this.GYOUSHA_CD.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_CD.PopupSearchSendParams")));
            this.GYOUSHA_CD.PopupSetFormField = "GYOUSHA_CD, GYOUSHA_NAME";
            this.GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_CD.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_CD.popupWindowSetting")));
            this.GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.RegistCheckMethod")));
            this.GYOUSHA_CD.SetFormField = "GYOUSHA_CD, GYOUSHA_NAME";
            this.GYOUSHA_CD.Size = new System.Drawing.Size(60, 20);
            this.GYOUSHA_CD.TabIndex = 4;
            this.GYOUSHA_CD.Tag = "半角6桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.GYOUSHA_CD.ZeroPaddengFlag = true;
            this.GYOUSHA_CD.Enter += new System.EventHandler(this.GYOUSHA_CD_Enter);
            this.GYOUSHA_CD.Validated += new System.EventHandler(this.GYOUSHA_CD_Validated);
            // 
            // GYOUSHA_NAME
            // 
            this.GYOUSHA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GYOUSHA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_NAME.DBFieldsName = "";
            this.GYOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_NAME.DisplayItemName = "";
            this.GYOUSHA_NAME.DisplayPopUp = null;
            this.GYOUSHA_NAME.ErrorMessage = "";
            this.GYOUSHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME.FocusOutCheckMethod")));
            this.GYOUSHA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GYOUSHA_NAME.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_NAME.GetCodeMasterField = "";
            this.GYOUSHA_NAME.IsInputErrorOccured = false;
            this.GYOUSHA_NAME.ItemDefinedTypes = "";
            this.GYOUSHA_NAME.Location = new System.Drawing.Point(179, 33);
            this.GYOUSHA_NAME.MaxLength = 0;
            this.GYOUSHA_NAME.Name = "GYOUSHA_NAME";
            this.GYOUSHA_NAME.PopupAfterExecute = null;
            this.GYOUSHA_NAME.PopupBeforeExecute = null;
            this.GYOUSHA_NAME.PopupGetMasterField = "";
            this.GYOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_NAME.PopupSearchSendParams")));
            this.GYOUSHA_NAME.PopupSetFormField = "";
            this.GYOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_NAME.popupWindowSetting")));
            this.GYOUSHA_NAME.ReadOnly = true;
            this.GYOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME.RegistCheckMethod")));
            this.GYOUSHA_NAME.SetFormField = "";
            this.GYOUSHA_NAME.Size = new System.Drawing.Size(285, 20);
            this.GYOUSHA_NAME.TabIndex = 8;
            this.GYOUSHA_NAME.TabStop = false;
            this.GYOUSHA_NAME.Tag = "　";
            // 
            // lblGyousha
            // 
            this.lblGyousha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblGyousha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGyousha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblGyousha.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblGyousha.ForeColor = System.Drawing.Color.White;
            this.lblGyousha.Location = new System.Drawing.Point(5, 33);
            this.lblGyousha.Name = "lblGyousha";
            this.lblGyousha.Size = new System.Drawing.Size(110, 20);
            this.lblGyousha.TabIndex = 6;
            this.lblGyousha.Text = "業者※";
            this.lblGyousha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GYOUSHA_POPUP
            // 
            this.GYOUSHA_POPUP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.GYOUSHA_POPUP.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.GYOUSHA_POPUP.DBFieldsName = null;
            this.GYOUSHA_POPUP.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_POPUP.DisplayItemName = null;
            this.GYOUSHA_POPUP.DisplayPopUp = null;
            this.GYOUSHA_POPUP.ErrorMessage = null;
            this.GYOUSHA_POPUP.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_POPUP.FocusOutCheckMethod")));
            this.GYOUSHA_POPUP.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.GYOUSHA_POPUP.GetCodeMasterField = null;
            this.GYOUSHA_POPUP.Image = global::Shougun.Core.Stock.ZaikoIdou.Properties.Resources.虫眼鏡;
            this.GYOUSHA_POPUP.ItemDefinedTypes = null;
            this.GYOUSHA_POPUP.LinkedSettingTextBox = null;
            this.GYOUSHA_POPUP.LinkedTextBoxs = null;
            this.GYOUSHA_POPUP.Location = new System.Drawing.Point(466, 32);
            this.GYOUSHA_POPUP.Name = "GYOUSHA_POPUP";
            this.GYOUSHA_POPUP.PopupAfterExecute = null;
            this.GYOUSHA_POPUP.PopupAfterExecuteMethod = "GyoushaPopupAfter";
            this.GYOUSHA_POPUP.PopupBeforeExecute = null;
            this.GYOUSHA_POPUP.PopupBeforeExecuteMethod = "GyoushaPopupBefore";
            this.GYOUSHA_POPUP.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_POPUP.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_POPUP.PopupSearchSendParams")));
            this.GYOUSHA_POPUP.PopupSetFormField = "GYOUSHA_CD, GYOUSHA_NAME";
            this.GYOUSHA_POPUP.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_POPUP.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_POPUP.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_POPUP.popupWindowSetting")));
            this.GYOUSHA_POPUP.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_POPUP.RegistCheckMethod")));
            this.GYOUSHA_POPUP.SearchDisplayFlag = 0;
            this.GYOUSHA_POPUP.SetFormField = "GYOUSHA_CD, GYOUSHA_NAME";
            this.GYOUSHA_POPUP.ShortItemName = null;
            this.GYOUSHA_POPUP.Size = new System.Drawing.Size(22, 22);
            this.GYOUSHA_POPUP.TabIndex = 5;
            this.GYOUSHA_POPUP.TabStop = false;
            this.GYOUSHA_POPUP.Tag = "業者検索画面を表示します";
            this.GYOUSHA_POPUP.UseVisualStyleBackColor = false;
            this.GYOUSHA_POPUP.ZeroPaddengFlag = false;
            // 
            // DETAIL_Ichiran
            // 
            this.DETAIL_Ichiran.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DETAIL_Ichiran.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DETAIL_Ichiran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DETAIL_Ichiran.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MEISAI_GENBA_CD,
            this.MEISAI_GENBA_NAME,
            this.MEISAI_IDOU_BEFORE_ZAIKO_RYOU,
            this.lbl2,
            this.MEISAI_IDOU_RYOU,
            this.lbl1,
            this.MEISAI_IDOU_AFTER_ZAIKO_RYOU});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DETAIL_Ichiran.DefaultCellStyle = dataGridViewCellStyle9;
            this.DETAIL_Ichiran.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DETAIL_Ichiran.EnableHeadersVisualStyles = false;
            this.DETAIL_Ichiran.GridColor = System.Drawing.Color.White;
            this.DETAIL_Ichiran.IsReload = false;
            this.DETAIL_Ichiran.LinkedDataPanelName = null;
            this.DETAIL_Ichiran.Location = new System.Drawing.Point(5, 173);
            this.DETAIL_Ichiran.MultiSelect = false;
            this.DETAIL_Ichiran.Name = "DETAIL_Ichiran";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DETAIL_Ichiran.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.DETAIL_Ichiran.RowHeadersVisible = false;
            this.DETAIL_Ichiran.RowTemplate.Height = 21;
            this.DETAIL_Ichiran.ShowCellToolTips = false;
            this.DETAIL_Ichiran.Size = new System.Drawing.Size(806, 292);
            this.DETAIL_Ichiran.TabIndex = 50;
            this.DETAIL_Ichiran.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DETAIL_Ichiran_CellEnter);
            this.DETAIL_Ichiran.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.DETAIL_Ichiran_CellValidating);
            // 
            // MEISAI_GENBA_CD
            // 
            this.MEISAI_GENBA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.MEISAI_GENBA_CD.DBFieldsName = "現場CD";
            this.MEISAI_GENBA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.MEISAI_GENBA_CD.DefaultCellStyle = dataGridViewCellStyle2;
            this.MEISAI_GENBA_CD.DisplayItemName = "現場CD";
            this.MEISAI_GENBA_CD.FocusOutCheckMethod = null;
            this.MEISAI_GENBA_CD.GetCodeMasterField = "GENBA_CD,GENBA_NAME_RYAKU";
            this.MEISAI_GENBA_CD.HeaderText = "現場CD※";
            this.MEISAI_GENBA_CD.MaxInputLength = 6;
            this.MEISAI_GENBA_CD.Name = "MEISAI_GENBA_CD";
            this.MEISAI_GENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MEISAI_GENBA_CD.PopupSearchSendParams")));
            this.MEISAI_GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.MEISAI_GENBA_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.MEISAI_GENBA_CD.popupWindowSetting = null;
            this.MEISAI_GENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MEISAI_GENBA_CD.RegistCheckMethod")));
            this.MEISAI_GENBA_CD.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MEISAI_GENBA_CD.SetFormField = "MEISAI_GENBA_CD,MEISAI_GENBA_NAME";
            this.MEISAI_GENBA_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MEISAI_GENBA_CD.ToolTipText = "半角6桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.MEISAI_GENBA_CD.ZeroPaddengFlag = true;
            // 
            // MEISAI_GENBA_NAME
            // 
            this.MEISAI_GENBA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.MEISAI_GENBA_NAME.DefaultCellStyle = dataGridViewCellStyle3;
            this.MEISAI_GENBA_NAME.FocusOutCheckMethod = null;
            this.MEISAI_GENBA_NAME.HeaderText = "現場名";
            this.MEISAI_GENBA_NAME.Name = "MEISAI_GENBA_NAME";
            this.MEISAI_GENBA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MEISAI_GENBA_NAME.PopupSearchSendParams")));
            this.MEISAI_GENBA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MEISAI_GENBA_NAME.popupWindowSetting = null;
            this.MEISAI_GENBA_NAME.ReadOnly = true;
            this.MEISAI_GENBA_NAME.RegistCheckMethod = null;
            this.MEISAI_GENBA_NAME.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MEISAI_GENBA_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MEISAI_GENBA_NAME.Width = 300;
            // 
            // MEISAI_IDOU_BEFORE_ZAIKO_RYOU
            // 
            this.MEISAI_IDOU_BEFORE_ZAIKO_RYOU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.MEISAI_IDOU_BEFORE_ZAIKO_RYOU.DefaultCellStyle = dataGridViewCellStyle4;
            this.MEISAI_IDOU_BEFORE_ZAIKO_RYOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MEISAI_IDOU_BEFORE_ZAIKO_RYOU.FocusOutCheckMethod")));
            this.MEISAI_IDOU_BEFORE_ZAIKO_RYOU.FormatSetting = "システム設定(重量書式)";
            this.MEISAI_IDOU_BEFORE_ZAIKO_RYOU.HeaderText = "移動前在庫量";
            this.MEISAI_IDOU_BEFORE_ZAIKO_RYOU.Name = "MEISAI_IDOU_BEFORE_ZAIKO_RYOU";
            this.MEISAI_IDOU_BEFORE_ZAIKO_RYOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MEISAI_IDOU_BEFORE_ZAIKO_RYOU.PopupSearchSendParams")));
            this.MEISAI_IDOU_BEFORE_ZAIKO_RYOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MEISAI_IDOU_BEFORE_ZAIKO_RYOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("MEISAI_IDOU_BEFORE_ZAIKO_RYOU.popupWindowSetting")));
            this.MEISAI_IDOU_BEFORE_ZAIKO_RYOU.ReadOnly = true;
            this.MEISAI_IDOU_BEFORE_ZAIKO_RYOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MEISAI_IDOU_BEFORE_ZAIKO_RYOU.RegistCheckMethod")));
            this.MEISAI_IDOU_BEFORE_ZAIKO_RYOU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // lbl2
            // 
            this.lbl2.CustomFormatSetting = "→";
            this.lbl2.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Format = "→";
            dataGridViewCellStyle5.NullValue = "→";
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.lbl2.DefaultCellStyle = dataGridViewCellStyle5;
            this.lbl2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("lbl2.FocusOutCheckMethod")));
            this.lbl2.FormatSetting = "カスタム";
            this.lbl2.HeaderText = "→";
            this.lbl2.Name = "lbl2";
            this.lbl2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("lbl2.PopupSearchSendParams")));
            this.lbl2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.lbl2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("lbl2.popupWindowSetting")));
            this.lbl2.ReadOnly = true;
            this.lbl2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("lbl2.RegistCheckMethod")));
            this.lbl2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.lbl2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.lbl2.Width = 25;
            // 
            // MEISAI_IDOU_RYOU
            // 
            this.MEISAI_IDOU_RYOU.DBFieldsName = "移動量";
            this.MEISAI_IDOU_RYOU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.MEISAI_IDOU_RYOU.DefaultCellStyle = dataGridViewCellStyle6;
            this.MEISAI_IDOU_RYOU.DisplayItemName = "移動量";
            this.MEISAI_IDOU_RYOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MEISAI_IDOU_RYOU.FocusOutCheckMethod")));
            this.MEISAI_IDOU_RYOU.HeaderText = "移動量※";
            this.MEISAI_IDOU_RYOU.Name = "MEISAI_IDOU_RYOU";
            this.MEISAI_IDOU_RYOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MEISAI_IDOU_RYOU.PopupSearchSendParams")));
            this.MEISAI_IDOU_RYOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MEISAI_IDOU_RYOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("MEISAI_IDOU_RYOU.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            1410065407,
            2,
            0,
            196608});
            this.MEISAI_IDOU_RYOU.RangeSetting = rangeSettingDto2;
            this.MEISAI_IDOU_RYOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MEISAI_IDOU_RYOU.RegistCheckMethod")));
            this.MEISAI_IDOU_RYOU.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MEISAI_IDOU_RYOU.ShortItemName = "移動量";
            this.MEISAI_IDOU_RYOU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MEISAI_IDOU_RYOU.ToolTipText = "整数部分は半角7桁で入力してください。小数点以下はシステム設定を参照します。";
            // 
            // lbl1
            // 
            this.lbl1.CustomFormatSetting = "→";
            this.lbl1.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Format = "→";
            dataGridViewCellStyle7.NullValue = "→";
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.lbl1.DefaultCellStyle = dataGridViewCellStyle7;
            this.lbl1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("lbl1.FocusOutCheckMethod")));
            this.lbl1.FormatSetting = "カスタム";
            this.lbl1.HeaderText = "→";
            this.lbl1.Name = "lbl1";
            this.lbl1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("lbl1.PopupSearchSendParams")));
            this.lbl1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.lbl1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("lbl1.popupWindowSetting")));
            this.lbl1.ReadOnly = true;
            this.lbl1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("lbl1.RegistCheckMethod")));
            this.lbl1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.lbl1.Width = 25;
            // 
            // MEISAI_IDOU_AFTER_ZAIKO_RYOU
            // 
            this.MEISAI_IDOU_AFTER_ZAIKO_RYOU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.MEISAI_IDOU_AFTER_ZAIKO_RYOU.DefaultCellStyle = dataGridViewCellStyle8;
            this.MEISAI_IDOU_AFTER_ZAIKO_RYOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MEISAI_IDOU_AFTER_ZAIKO_RYOU.FocusOutCheckMethod")));
            this.MEISAI_IDOU_AFTER_ZAIKO_RYOU.FormatSetting = "システム設定(重量書式)";
            this.MEISAI_IDOU_AFTER_ZAIKO_RYOU.HeaderText = "移動後在庫量";
            this.MEISAI_IDOU_AFTER_ZAIKO_RYOU.Name = "MEISAI_IDOU_AFTER_ZAIKO_RYOU";
            this.MEISAI_IDOU_AFTER_ZAIKO_RYOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MEISAI_IDOU_AFTER_ZAIKO_RYOU.PopupSearchSendParams")));
            this.MEISAI_IDOU_AFTER_ZAIKO_RYOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MEISAI_IDOU_AFTER_ZAIKO_RYOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("MEISAI_IDOU_AFTER_ZAIKO_RYOU.popupWindowSetting")));
            this.MEISAI_IDOU_AFTER_ZAIKO_RYOU.ReadOnly = true;
            this.MEISAI_IDOU_AFTER_ZAIKO_RYOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MEISAI_IDOU_AFTER_ZAIKO_RYOU.RegistCheckMethod")));
            this.MEISAI_IDOU_AFTER_ZAIKO_RYOU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // GENBA_POPUP
            // 
            this.GENBA_POPUP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.GENBA_POPUP.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.GENBA_POPUP.DBFieldsName = null;
            this.GENBA_POPUP.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_POPUP.DisplayItemName = null;
            this.GENBA_POPUP.DisplayPopUp = null;
            this.GENBA_POPUP.ErrorMessage = null;
            this.GENBA_POPUP.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_POPUP.FocusOutCheckMethod")));
            this.GENBA_POPUP.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.GENBA_POPUP.GetCodeMasterField = null;
            this.GENBA_POPUP.Image = global::Shougun.Core.Stock.ZaikoIdou.Properties.Resources.虫眼鏡;
            this.GENBA_POPUP.ItemDefinedTypes = null;
            this.GENBA_POPUP.LinkedSettingTextBox = null;
            this.GENBA_POPUP.LinkedTextBoxs = null;
            this.GENBA_POPUP.Location = new System.Drawing.Point(466, 58);
            this.GENBA_POPUP.Name = "GENBA_POPUP";
            this.GENBA_POPUP.PopupAfterExecute = null;
            this.GENBA_POPUP.PopupAfterExecuteMethod = "GenbaPopupAfter";
            this.GENBA_POPUP.PopupBeforeExecute = null;
            this.GENBA_POPUP.PopupBeforeExecuteMethod = "GenbaPopupBefore";
            this.GENBA_POPUP.PopupGetMasterField = "GENBA_CD, GENBA_NAME_RYAKU, GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GENBA_POPUP.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_POPUP.PopupSearchSendParams")));
            this.GENBA_POPUP.PopupSetFormField = "GENBA_CD, GENBA_NAME, GYOUSHA_CD, GYOUSHA_NAME";
            this.GENBA_POPUP.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_POPUP.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_POPUP.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_POPUP.popupWindowSetting")));
            this.GENBA_POPUP.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_POPUP.RegistCheckMethod")));
            this.GENBA_POPUP.SearchDisplayFlag = 0;
            this.GENBA_POPUP.SetFormField = "GENBA_CD, GENBA_NAME, GYOUSHA_CD, GYOUSHA_NAME";
            this.GENBA_POPUP.ShortItemName = null;
            this.GENBA_POPUP.Size = new System.Drawing.Size(22, 22);
            this.GENBA_POPUP.TabIndex = 7;
            this.GENBA_POPUP.TabStop = false;
            this.GENBA_POPUP.Tag = "現場検索画面を表示します";
            this.GENBA_POPUP.UseVisualStyleBackColor = false;
            this.GENBA_POPUP.ZeroPaddengFlag = false;
            // 
            // GENBA_CD
            // 
            this.GENBA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.GENBA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_CD.ChangeUpperCase = true;
            this.GENBA_CD.CharacterLimitList = null;
            this.GENBA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.GENBA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_CD.DisplayItemName = "現場CD";
            this.GENBA_CD.DisplayPopUp = null;
            this.GENBA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.FocusOutCheckMethod")));
            this.GENBA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GENBA_CD.ForeColor = System.Drawing.Color.Black;
            this.GENBA_CD.GetCodeMasterField = "GENBA_CD, GENBA_NAME_RYAKU, GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GENBA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GENBA_CD.IsInputErrorOccured = false;
            this.GENBA_CD.ItemDefinedTypes = "varchar";
            this.GENBA_CD.Location = new System.Drawing.Point(120, 59);
            this.GENBA_CD.MaxLength = 6;
            this.GENBA_CD.Name = "GENBA_CD";
            this.GENBA_CD.PopupAfterExecute = null;
            this.GENBA_CD.PopupAfterExecuteMethod = "GenbaPopupAfter";
            this.GENBA_CD.PopupBeforeExecute = null;
            this.GENBA_CD.PopupGetMasterField = "GENBA_CD, GENBA_NAME_RYAKU, GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_CD.PopupSearchSendParams")));
            this.GENBA_CD.PopupSetFormField = "GENBA_CD, GENBA_NAME,GYOUSHA_CD, GYOUSHA_NAME";
            this.GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_CD.popupWindowSetting")));
            this.GENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.RegistCheckMethod")));
            this.GENBA_CD.SetFormField = "GENBA_CD, GENBA_NAME,GYOUSHA_CD, GYOUSHA_NAME";
            this.GENBA_CD.Size = new System.Drawing.Size(60, 20);
            this.GENBA_CD.TabIndex = 6;
            this.GENBA_CD.Tag = "半角6桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.GENBA_CD.ZeroPaddengFlag = true;
            this.GENBA_CD.Enter += new System.EventHandler(this.GENBA_CD_Enter);
            this.GENBA_CD.Validated += new System.EventHandler(this.GENBA_CD_Validated);
            // 
            // GENBA_NAME
            // 
            this.GENBA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GENBA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_NAME.DBFieldsName = "";
            this.GENBA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_NAME.DisplayItemName = "";
            this.GENBA_NAME.DisplayPopUp = null;
            this.GENBA_NAME.ErrorMessage = "";
            this.GENBA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME.FocusOutCheckMethod")));
            this.GENBA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GENBA_NAME.ForeColor = System.Drawing.Color.Black;
            this.GENBA_NAME.GetCodeMasterField = "";
            this.GENBA_NAME.IsInputErrorOccured = false;
            this.GENBA_NAME.ItemDefinedTypes = "";
            this.GENBA_NAME.Location = new System.Drawing.Point(179, 59);
            this.GENBA_NAME.MaxLength = 0;
            this.GENBA_NAME.Name = "GENBA_NAME";
            this.GENBA_NAME.PopupAfterExecute = null;
            this.GENBA_NAME.PopupBeforeExecute = null;
            this.GENBA_NAME.PopupGetMasterField = "";
            this.GENBA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_NAME.PopupSearchSendParams")));
            this.GENBA_NAME.PopupSetFormField = "";
            this.GENBA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_NAME.popupWindowSetting")));
            this.GENBA_NAME.ReadOnly = true;
            this.GENBA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME.RegistCheckMethod")));
            this.GENBA_NAME.SetFormField = "";
            this.GENBA_NAME.Size = new System.Drawing.Size(285, 20);
            this.GENBA_NAME.TabIndex = 54;
            this.GENBA_NAME.TabStop = false;
            this.GENBA_NAME.Tag = "　";
            // 
            // lblGenba
            // 
            this.lblGenba.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblGenba.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGenba.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblGenba.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblGenba.ForeColor = System.Drawing.Color.White;
            this.lblGenba.Location = new System.Drawing.Point(5, 59);
            this.lblGenba.Name = "lblGenba";
            this.lblGenba.Size = new System.Drawing.Size(110, 20);
            this.lblGenba.TabIndex = 52;
            this.lblGenba.Text = "現場※";
            this.lblGenba.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ZAIKO_HINMEI_POPUP
            // 
            this.ZAIKO_HINMEI_POPUP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.ZAIKO_HINMEI_POPUP.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ZAIKO_HINMEI_POPUP.DBFieldsName = null;
            this.ZAIKO_HINMEI_POPUP.DefaultBackColor = System.Drawing.Color.Empty;
            this.ZAIKO_HINMEI_POPUP.DisplayItemName = null;
            this.ZAIKO_HINMEI_POPUP.DisplayPopUp = null;
            this.ZAIKO_HINMEI_POPUP.ErrorMessage = null;
            this.ZAIKO_HINMEI_POPUP.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_POPUP.FocusOutCheckMethod")));
            this.ZAIKO_HINMEI_POPUP.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.ZAIKO_HINMEI_POPUP.GetCodeMasterField = null;
            this.ZAIKO_HINMEI_POPUP.Image = global::Shougun.Core.Stock.ZaikoIdou.Properties.Resources.虫眼鏡;
            this.ZAIKO_HINMEI_POPUP.ItemDefinedTypes = null;
            this.ZAIKO_HINMEI_POPUP.LinkedSettingTextBox = null;
            this.ZAIKO_HINMEI_POPUP.LinkedTextBoxs = null;
            this.ZAIKO_HINMEI_POPUP.Location = new System.Drawing.Point(466, 84);
            this.ZAIKO_HINMEI_POPUP.Name = "ZAIKO_HINMEI_POPUP";
            this.ZAIKO_HINMEI_POPUP.PopupAfterExecute = null;
            this.ZAIKO_HINMEI_POPUP.PopupAfterExecuteMethod = "ZaikoHinmeiPopupAfter";
            this.ZAIKO_HINMEI_POPUP.PopupBeforeExecute = null;
            this.ZAIKO_HINMEI_POPUP.PopupBeforeExecuteMethod = "ZaikoHinmeiPopupBefore";
            this.ZAIKO_HINMEI_POPUP.PopupGetMasterField = "ZAIKO_HINMEI_CD, ZAIKO_HINMEINAME_RYAKU";
            this.ZAIKO_HINMEI_POPUP.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZAIKO_HINMEI_POPUP.PopupSearchSendParams")));
            this.ZAIKO_HINMEI_POPUP.PopupSetFormField = "ZAIKO_HINMEI_CD, ZAIKO_HINMEI_NAME";
            this.ZAIKO_HINMEI_POPUP.PopupWindowId = r_framework.Const.WINDOW_ID.M_ZAIKO_HINMEI;
            this.ZAIKO_HINMEI_POPUP.PopupWindowName = "マスタ共通ポップアップ";
            this.ZAIKO_HINMEI_POPUP.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZAIKO_HINMEI_POPUP.popupWindowSetting")));
            this.ZAIKO_HINMEI_POPUP.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_POPUP.RegistCheckMethod")));
            this.ZAIKO_HINMEI_POPUP.SearchDisplayFlag = 0;
            this.ZAIKO_HINMEI_POPUP.SetFormField = "ZAIKO_HINMEI_CD, ZAIKO_HINMEI_NAME";
            this.ZAIKO_HINMEI_POPUP.ShortItemName = null;
            this.ZAIKO_HINMEI_POPUP.Size = new System.Drawing.Size(22, 22);
            this.ZAIKO_HINMEI_POPUP.TabIndex = 9;
            this.ZAIKO_HINMEI_POPUP.TabStop = false;
            this.ZAIKO_HINMEI_POPUP.Tag = "在庫品名検索画面を表示します";
            this.ZAIKO_HINMEI_POPUP.UseVisualStyleBackColor = false;
            this.ZAIKO_HINMEI_POPUP.ZeroPaddengFlag = false;
            // 
            // ZAIKO_HINMEI_CD
            // 
            this.ZAIKO_HINMEI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.ZAIKO_HINMEI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ZAIKO_HINMEI_CD.ChangeUpperCase = true;
            this.ZAIKO_HINMEI_CD.CharacterLimitList = null;
            this.ZAIKO_HINMEI_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.ZAIKO_HINMEI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.ZAIKO_HINMEI_CD.DisplayItemName = "在庫品名CD";
            this.ZAIKO_HINMEI_CD.DisplayPopUp = null;
            this.ZAIKO_HINMEI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_CD.FocusOutCheckMethod")));
            this.ZAIKO_HINMEI_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ZAIKO_HINMEI_CD.ForeColor = System.Drawing.Color.Black;
            this.ZAIKO_HINMEI_CD.GetCodeMasterField = "ZAIKO_HINMEI_CD, ZAIKO_HINMEI_NAME_RYAKU";
            this.ZAIKO_HINMEI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ZAIKO_HINMEI_CD.IsInputErrorOccured = false;
            this.ZAIKO_HINMEI_CD.ItemDefinedTypes = "varchar";
            this.ZAIKO_HINMEI_CD.Location = new System.Drawing.Point(120, 85);
            this.ZAIKO_HINMEI_CD.MaxLength = 6;
            this.ZAIKO_HINMEI_CD.Name = "ZAIKO_HINMEI_CD";
            this.ZAIKO_HINMEI_CD.PopupAfterExecute = null;
            this.ZAIKO_HINMEI_CD.PopupAfterExecuteMethod = "ZaikoHinmeiPopupAfter";
            this.ZAIKO_HINMEI_CD.PopupBeforeExecute = null;
            this.ZAIKO_HINMEI_CD.PopupGetMasterField = "ZAIKO_HINMEI_CD, ZAIKO_HINMEI_NAME_RYAKU";
            this.ZAIKO_HINMEI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZAIKO_HINMEI_CD.PopupSearchSendParams")));
            this.ZAIKO_HINMEI_CD.PopupSetFormField = "ZAIKO_HINMEI_CD, ZAIKO_HINMEI_NAME";
            this.ZAIKO_HINMEI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_ZAIKO_HINMEI;
            this.ZAIKO_HINMEI_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.ZAIKO_HINMEI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZAIKO_HINMEI_CD.popupWindowSetting")));
            this.ZAIKO_HINMEI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_CD.RegistCheckMethod")));
            this.ZAIKO_HINMEI_CD.SetFormField = "ZAIKO_HINMEI_CD, ZAIKO_HINMEI_NAME";
            this.ZAIKO_HINMEI_CD.Size = new System.Drawing.Size(60, 20);
            this.ZAIKO_HINMEI_CD.TabIndex = 8;
            this.ZAIKO_HINMEI_CD.Tag = "半角6桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.ZAIKO_HINMEI_CD.ZeroPaddengFlag = true;
            this.ZAIKO_HINMEI_CD.Enter += new System.EventHandler(this.ZAIKO_HINMEI_CD_Enter);
            this.ZAIKO_HINMEI_CD.Validated += new System.EventHandler(this.ZAIKO_HINMEI_CD_Validated);
            // 
            // ZAIKO_HINMEI_NAME
            // 
            this.ZAIKO_HINMEI_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ZAIKO_HINMEI_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ZAIKO_HINMEI_NAME.DBFieldsName = "";
            this.ZAIKO_HINMEI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.ZAIKO_HINMEI_NAME.DisplayItemName = "";
            this.ZAIKO_HINMEI_NAME.DisplayPopUp = null;
            this.ZAIKO_HINMEI_NAME.ErrorMessage = "";
            this.ZAIKO_HINMEI_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_NAME.FocusOutCheckMethod")));
            this.ZAIKO_HINMEI_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ZAIKO_HINMEI_NAME.ForeColor = System.Drawing.Color.Black;
            this.ZAIKO_HINMEI_NAME.GetCodeMasterField = "";
            this.ZAIKO_HINMEI_NAME.IsInputErrorOccured = false;
            this.ZAIKO_HINMEI_NAME.ItemDefinedTypes = "";
            this.ZAIKO_HINMEI_NAME.Location = new System.Drawing.Point(179, 85);
            this.ZAIKO_HINMEI_NAME.MaxLength = 0;
            this.ZAIKO_HINMEI_NAME.Name = "ZAIKO_HINMEI_NAME";
            this.ZAIKO_HINMEI_NAME.PopupAfterExecute = null;
            this.ZAIKO_HINMEI_NAME.PopupBeforeExecute = null;
            this.ZAIKO_HINMEI_NAME.PopupGetMasterField = "";
            this.ZAIKO_HINMEI_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZAIKO_HINMEI_NAME.PopupSearchSendParams")));
            this.ZAIKO_HINMEI_NAME.PopupSetFormField = "";
            this.ZAIKO_HINMEI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ZAIKO_HINMEI_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZAIKO_HINMEI_NAME.popupWindowSetting")));
            this.ZAIKO_HINMEI_NAME.ReadOnly = true;
            this.ZAIKO_HINMEI_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_NAME.RegistCheckMethod")));
            this.ZAIKO_HINMEI_NAME.SetFormField = "";
            this.ZAIKO_HINMEI_NAME.Size = new System.Drawing.Size(285, 20);
            this.ZAIKO_HINMEI_NAME.TabIndex = 58;
            this.ZAIKO_HINMEI_NAME.TabStop = false;
            this.ZAIKO_HINMEI_NAME.Tag = "　";
            // 
            // lblZaikoHinmei
            // 
            this.lblZaikoHinmei.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblZaikoHinmei.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblZaikoHinmei.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblZaikoHinmei.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblZaikoHinmei.ForeColor = System.Drawing.Color.White;
            this.lblZaikoHinmei.Location = new System.Drawing.Point(5, 85);
            this.lblZaikoHinmei.Name = "lblZaikoHinmei";
            this.lblZaikoHinmei.Size = new System.Drawing.Size(110, 20);
            this.lblZaikoHinmei.TabIndex = 56;
            this.lblZaikoHinmei.Text = "在庫品名※";
            this.lblZaikoHinmei.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBeforeIdou
            // 
            this.lblBeforeIdou.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblBeforeIdou.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBeforeIdou.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblBeforeIdou.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblBeforeIdou.ForeColor = System.Drawing.Color.White;
            this.lblBeforeIdou.Location = new System.Drawing.Point(5, 117);
            this.lblBeforeIdou.Name = "lblBeforeIdou";
            this.lblBeforeIdou.Size = new System.Drawing.Size(110, 20);
            this.lblBeforeIdou.TabIndex = 60;
            this.lblBeforeIdou.Text = "移動前在庫量";
            this.lblBeforeIdou.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IDOU_BEFORE_ZAIKO_RYOU
            // 
            this.IDOU_BEFORE_ZAIKO_RYOU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.IDOU_BEFORE_ZAIKO_RYOU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IDOU_BEFORE_ZAIKO_RYOU.DBFieldsName = "";
            this.IDOU_BEFORE_ZAIKO_RYOU.DefaultBackColor = System.Drawing.Color.Empty;
            this.IDOU_BEFORE_ZAIKO_RYOU.DisplayItemName = "";
            this.IDOU_BEFORE_ZAIKO_RYOU.DisplayPopUp = null;
            this.IDOU_BEFORE_ZAIKO_RYOU.ErrorMessage = "";
            this.IDOU_BEFORE_ZAIKO_RYOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("IDOU_BEFORE_ZAIKO_RYOU.FocusOutCheckMethod")));
            this.IDOU_BEFORE_ZAIKO_RYOU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.IDOU_BEFORE_ZAIKO_RYOU.ForeColor = System.Drawing.Color.Black;
            this.IDOU_BEFORE_ZAIKO_RYOU.FormatSetting = "システム設定(重量書式)";
            this.IDOU_BEFORE_ZAIKO_RYOU.GetCodeMasterField = "";
            this.IDOU_BEFORE_ZAIKO_RYOU.IsInputErrorOccured = false;
            this.IDOU_BEFORE_ZAIKO_RYOU.ItemDefinedTypes = "";
            this.IDOU_BEFORE_ZAIKO_RYOU.Location = new System.Drawing.Point(5, 137);
            this.IDOU_BEFORE_ZAIKO_RYOU.MaxLength = 0;
            this.IDOU_BEFORE_ZAIKO_RYOU.Name = "IDOU_BEFORE_ZAIKO_RYOU";
            this.IDOU_BEFORE_ZAIKO_RYOU.PopupAfterExecute = null;
            this.IDOU_BEFORE_ZAIKO_RYOU.PopupBeforeExecute = null;
            this.IDOU_BEFORE_ZAIKO_RYOU.PopupGetMasterField = "";
            this.IDOU_BEFORE_ZAIKO_RYOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("IDOU_BEFORE_ZAIKO_RYOU.PopupSearchSendParams")));
            this.IDOU_BEFORE_ZAIKO_RYOU.PopupSetFormField = "";
            this.IDOU_BEFORE_ZAIKO_RYOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.IDOU_BEFORE_ZAIKO_RYOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("IDOU_BEFORE_ZAIKO_RYOU.popupWindowSetting")));
            this.IDOU_BEFORE_ZAIKO_RYOU.ReadOnly = true;
            this.IDOU_BEFORE_ZAIKO_RYOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("IDOU_BEFORE_ZAIKO_RYOU.RegistCheckMethod")));
            this.IDOU_BEFORE_ZAIKO_RYOU.SetFormField = "";
            this.IDOU_BEFORE_ZAIKO_RYOU.Size = new System.Drawing.Size(110, 20);
            this.IDOU_BEFORE_ZAIKO_RYOU.TabIndex = 61;
            this.IDOU_BEFORE_ZAIKO_RYOU.TabStop = false;
            this.IDOU_BEFORE_ZAIKO_RYOU.Tag = "　";
            this.IDOU_BEFORE_ZAIKO_RYOU.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // IDOU_RYOU_GOUKEI
            // 
            this.IDOU_RYOU_GOUKEI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.IDOU_RYOU_GOUKEI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IDOU_RYOU_GOUKEI.DBFieldsName = "";
            this.IDOU_RYOU_GOUKEI.DefaultBackColor = System.Drawing.Color.Empty;
            this.IDOU_RYOU_GOUKEI.DisplayItemName = "";
            this.IDOU_RYOU_GOUKEI.DisplayPopUp = null;
            this.IDOU_RYOU_GOUKEI.ErrorMessage = "";
            this.IDOU_RYOU_GOUKEI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("IDOU_RYOU_GOUKEI.FocusOutCheckMethod")));
            this.IDOU_RYOU_GOUKEI.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.IDOU_RYOU_GOUKEI.ForeColor = System.Drawing.Color.Black;
            this.IDOU_RYOU_GOUKEI.FormatSetting = "システム設定(重量書式)";
            this.IDOU_RYOU_GOUKEI.GetCodeMasterField = "";
            this.IDOU_RYOU_GOUKEI.IsInputErrorOccured = false;
            this.IDOU_RYOU_GOUKEI.ItemDefinedTypes = "";
            this.IDOU_RYOU_GOUKEI.Location = new System.Drawing.Point(142, 137);
            this.IDOU_RYOU_GOUKEI.MaxLength = 0;
            this.IDOU_RYOU_GOUKEI.Name = "IDOU_RYOU_GOUKEI";
            this.IDOU_RYOU_GOUKEI.PopupAfterExecute = null;
            this.IDOU_RYOU_GOUKEI.PopupBeforeExecute = null;
            this.IDOU_RYOU_GOUKEI.PopupGetMasterField = "";
            this.IDOU_RYOU_GOUKEI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("IDOU_RYOU_GOUKEI.PopupSearchSendParams")));
            this.IDOU_RYOU_GOUKEI.PopupSetFormField = "";
            this.IDOU_RYOU_GOUKEI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.IDOU_RYOU_GOUKEI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("IDOU_RYOU_GOUKEI.popupWindowSetting")));
            this.IDOU_RYOU_GOUKEI.ReadOnly = true;
            this.IDOU_RYOU_GOUKEI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("IDOU_RYOU_GOUKEI.RegistCheckMethod")));
            this.IDOU_RYOU_GOUKEI.SetFormField = "";
            this.IDOU_RYOU_GOUKEI.Size = new System.Drawing.Size(110, 20);
            this.IDOU_RYOU_GOUKEI.TabIndex = 63;
            this.IDOU_RYOU_GOUKEI.TabStop = false;
            this.IDOU_RYOU_GOUKEI.Tag = "　";
            this.IDOU_RYOU_GOUKEI.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblIdouGoukei
            // 
            this.lblIdouGoukei.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblIdouGoukei.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIdouGoukei.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblIdouGoukei.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblIdouGoukei.ForeColor = System.Drawing.Color.White;
            this.lblIdouGoukei.Location = new System.Drawing.Point(142, 117);
            this.lblIdouGoukei.Name = "lblIdouGoukei";
            this.lblIdouGoukei.Size = new System.Drawing.Size(110, 20);
            this.lblIdouGoukei.TabIndex = 62;
            this.lblIdouGoukei.Text = "移動量合計";
            this.lblIdouGoukei.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IDOU_AFTER_ZAIKO_RYOU
            // 
            this.IDOU_AFTER_ZAIKO_RYOU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.IDOU_AFTER_ZAIKO_RYOU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IDOU_AFTER_ZAIKO_RYOU.DBFieldsName = "";
            this.IDOU_AFTER_ZAIKO_RYOU.DefaultBackColor = System.Drawing.Color.Empty;
            this.IDOU_AFTER_ZAIKO_RYOU.DisplayItemName = "";
            this.IDOU_AFTER_ZAIKO_RYOU.DisplayPopUp = null;
            this.IDOU_AFTER_ZAIKO_RYOU.ErrorMessage = "";
            this.IDOU_AFTER_ZAIKO_RYOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("IDOU_AFTER_ZAIKO_RYOU.FocusOutCheckMethod")));
            this.IDOU_AFTER_ZAIKO_RYOU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.IDOU_AFTER_ZAIKO_RYOU.ForeColor = System.Drawing.Color.Black;
            this.IDOU_AFTER_ZAIKO_RYOU.FormatSetting = "システム設定(重量書式)";
            this.IDOU_AFTER_ZAIKO_RYOU.GetCodeMasterField = "";
            this.IDOU_AFTER_ZAIKO_RYOU.IsInputErrorOccured = false;
            this.IDOU_AFTER_ZAIKO_RYOU.ItemDefinedTypes = "";
            this.IDOU_AFTER_ZAIKO_RYOU.Location = new System.Drawing.Point(279, 137);
            this.IDOU_AFTER_ZAIKO_RYOU.MaxLength = 0;
            this.IDOU_AFTER_ZAIKO_RYOU.Name = "IDOU_AFTER_ZAIKO_RYOU";
            this.IDOU_AFTER_ZAIKO_RYOU.PopupAfterExecute = null;
            this.IDOU_AFTER_ZAIKO_RYOU.PopupBeforeExecute = null;
            this.IDOU_AFTER_ZAIKO_RYOU.PopupGetMasterField = "";
            this.IDOU_AFTER_ZAIKO_RYOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("IDOU_AFTER_ZAIKO_RYOU.PopupSearchSendParams")));
            this.IDOU_AFTER_ZAIKO_RYOU.PopupSetFormField = "";
            this.IDOU_AFTER_ZAIKO_RYOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.IDOU_AFTER_ZAIKO_RYOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("IDOU_AFTER_ZAIKO_RYOU.popupWindowSetting")));
            this.IDOU_AFTER_ZAIKO_RYOU.ReadOnly = true;
            this.IDOU_AFTER_ZAIKO_RYOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("IDOU_AFTER_ZAIKO_RYOU.RegistCheckMethod")));
            this.IDOU_AFTER_ZAIKO_RYOU.SetFormField = "";
            this.IDOU_AFTER_ZAIKO_RYOU.Size = new System.Drawing.Size(110, 20);
            this.IDOU_AFTER_ZAIKO_RYOU.TabIndex = 65;
            this.IDOU_AFTER_ZAIKO_RYOU.TabStop = false;
            this.IDOU_AFTER_ZAIKO_RYOU.Tag = "　";
            this.IDOU_AFTER_ZAIKO_RYOU.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblAferIdou
            // 
            this.lblAferIdou.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblAferIdou.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAferIdou.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblAferIdou.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblAferIdou.ForeColor = System.Drawing.Color.White;
            this.lblAferIdou.Location = new System.Drawing.Point(279, 117);
            this.lblAferIdou.Name = "lblAferIdou";
            this.lblAferIdou.Size = new System.Drawing.Size(110, 20);
            this.lblAferIdou.TabIndex = 64;
            this.lblAferIdou.Text = "移動後在庫量";
            this.lblAferIdou.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label2.Location = new System.Drawing.Point(258, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 20);
            this.label2.TabIndex = 10011;
            this.label2.Text = "→";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.Location = new System.Drawing.Point(121, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 20);
            this.label1.TabIndex = 10012;
            this.label1.Text = "→";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 477);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.IDOU_AFTER_ZAIKO_RYOU);
            this.Controls.Add(this.lblAferIdou);
            this.Controls.Add(this.IDOU_RYOU_GOUKEI);
            this.Controls.Add(this.lblIdouGoukei);
            this.Controls.Add(this.IDOU_BEFORE_ZAIKO_RYOU);
            this.Controls.Add(this.lblBeforeIdou);
            this.Controls.Add(this.ZAIKO_HINMEI_POPUP);
            this.Controls.Add(this.ZAIKO_HINMEI_CD);
            this.Controls.Add(this.ZAIKO_HINMEI_NAME);
            this.Controls.Add(this.lblZaikoHinmei);
            this.Controls.Add(this.GENBA_POPUP);
            this.Controls.Add(this.GENBA_CD);
            this.Controls.Add(this.GENBA_NAME);
            this.Controls.Add(this.lblGenba);
            this.Controls.Add(this.DETAIL_Ichiran);
            this.Controls.Add(this.GYOUSHA_POPUP);
            this.Controls.Add(this.GYOUSHA_CD);
            this.Controls.Add(this.GYOUSHA_NAME);
            this.Controls.Add(this.lblGyousha);
            this.Controls.Add(this.IDOU_DATE);
            this.Controls.Add(this.lblNyuuryokuDate);
            this.Controls.Add(this.IDOU_NUMBER);
            this.Controls.Add(this.NEXT_BUTTON);
            this.Controls.Add(this.PREV_BUTTON);
            this.Controls.Add(this.lblIdouNumber);
            this.Location = new System.Drawing.Point(161, 0);
            this.Name = "UIForm";
            this.Text = "1.請求データ作成時";
            ((System.ComponentModel.ISupportInitialize)(this.DETAIL_Ichiran)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomNumericTextBox2 IDOU_NUMBER;
        internal System.Windows.Forms.Button NEXT_BUTTON;
        internal System.Windows.Forms.Button PREV_BUTTON;
        internal System.Windows.Forms.Label lblIdouNumber;
        internal r_framework.CustomControl.CustomDateTimePicker IDOU_DATE;
        internal System.Windows.Forms.Label lblNyuuryokuDate;
        public r_framework.CustomControl.CustomAlphaNumTextBox GYOUSHA_CD;
        internal r_framework.CustomControl.CustomTextBox GYOUSHA_NAME;
        internal System.Windows.Forms.Label lblGyousha;
        internal r_framework.CustomControl.CustomPopupOpenButton GYOUSHA_POPUP;
        internal r_framework.CustomControl.CustomDataGridView DETAIL_Ichiran;
        internal r_framework.CustomControl.CustomPopupOpenButton GENBA_POPUP;
        public r_framework.CustomControl.CustomAlphaNumTextBox GENBA_CD;
        internal r_framework.CustomControl.CustomTextBox GENBA_NAME;
        internal System.Windows.Forms.Label lblGenba;
        internal r_framework.CustomControl.CustomPopupOpenButton ZAIKO_HINMEI_POPUP;
        public r_framework.CustomControl.CustomAlphaNumTextBox ZAIKO_HINMEI_CD;
        internal r_framework.CustomControl.CustomTextBox ZAIKO_HINMEI_NAME;
        internal System.Windows.Forms.Label lblZaikoHinmei;
        internal System.Windows.Forms.Label lblBeforeIdou;
        internal r_framework.CustomControl.CustomNumericTextBox2 IDOU_BEFORE_ZAIKO_RYOU;
        internal r_framework.CustomControl.CustomNumericTextBox2 IDOU_RYOU_GOUKEI;
        internal System.Windows.Forms.Label lblIdouGoukei;
        internal r_framework.CustomControl.CustomNumericTextBox2 IDOU_AFTER_ZAIKO_RYOU;
        internal System.Windows.Forms.Label lblAferIdou;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn MEISAI_GENBA_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn MEISAI_GENBA_NAME;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column MEISAI_IDOU_BEFORE_ZAIKO_RYOU;
        private r_framework.CustomControl.DgvCustomTextBoxColumn lbl2;
        private r_framework.CustomControl.DgvCustomTextBoxColumn lbl1;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column MEISAI_IDOU_AFTER_ZAIKO_RYOU;
        internal r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column MEISAI_IDOU_RYOU;
    }
}