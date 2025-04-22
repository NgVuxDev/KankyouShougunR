using System.Windows.Forms;
namespace Shougun.Core.Stock.ZaikoTyouseiNyuuryoku
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
            this.TYOUSEI_NUMBER = new r_framework.CustomControl.CustomNumericTextBox2();
            this.NEXT_BUTTON = new System.Windows.Forms.Button();
            this.PREV_BUTTON = new System.Windows.Forms.Button();
            this.lblIdouNumber = new System.Windows.Forms.Label();
            this.TYOUSEI_DATE = new r_framework.CustomControl.CustomDateTimePicker();
            this.lblNyuuryokuDate = new System.Windows.Forms.Label();
            this.GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GYOUSHA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.lblGyousha = new System.Windows.Forms.Label();
            this.GYOUSHA_POPUP = new r_framework.CustomControl.CustomPopupOpenButton();
            this.DETAIL_Ichiran = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.GENBA_POPUP = new r_framework.CustomControl.CustomPopupOpenButton();
            this.GENBA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GENBA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.lblGenba = new System.Windows.Forms.Label();
            this.ZAIKO_HINMEI_POPUP = new r_framework.CustomControl.CustomPopupOpenButton();
            this.ZAIKO_HINMEI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.ZAIKO_HINMEI_NAME = new r_framework.CustomControl.CustomTextBox();
            this.lblZaikoHinmei = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TYOUSEI_BIKOU1 = new r_framework.CustomControl.CustomTextBox();
            this.TYOUSEI_BIKOU2 = new r_framework.CustomControl.CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TYOUSEI_BIKOU3 = new r_framework.CustomControl.CustomTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TYOUSEI_BEFORE_GOUKEI = new r_framework.CustomControl.CustomNumericTextBox2();
            this.TYOUSEI_AFTER_GOUKEI = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label5 = new System.Windows.Forms.Label();
            this.MEISAI_ZAIKO_HINMEI_CD = new r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn();
            this.MEISAI_ZAIKO_HINMEI_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.lbl2 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.MEISAI_TYOUSEI_RYOU = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.lbl1 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            ((System.ComponentModel.ISupportInitialize)(this.DETAIL_Ichiran)).BeginInit();
            this.SuspendLayout();
            // 
            // TYOUSEI_NUMBER
            // 
            this.TYOUSEI_NUMBER.BackColor = System.Drawing.SystemColors.Window;
            this.TYOUSEI_NUMBER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TYOUSEI_NUMBER.DefaultBackColor = System.Drawing.Color.Empty;
            this.TYOUSEI_NUMBER.DisplayPopUp = null;
            this.TYOUSEI_NUMBER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TYOUSEI_NUMBER.FocusOutCheckMethod")));
            this.TYOUSEI_NUMBER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TYOUSEI_NUMBER.ForeColor = System.Drawing.Color.Black;
            this.TYOUSEI_NUMBER.FormatSetting = "数値(#)フォーマット";
            this.TYOUSEI_NUMBER.IsInputErrorOccured = false;
            this.TYOUSEI_NUMBER.Location = new System.Drawing.Point(355, 6);
            this.TYOUSEI_NUMBER.Name = "TYOUSEI_NUMBER";
            this.TYOUSEI_NUMBER.PopupAfterExecute = null;
            this.TYOUSEI_NUMBER.PopupBeforeExecute = null;
            this.TYOUSEI_NUMBER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TYOUSEI_NUMBER.PopupSearchSendParams")));
            this.TYOUSEI_NUMBER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TYOUSEI_NUMBER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TYOUSEI_NUMBER.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.TYOUSEI_NUMBER.RangeSetting = rangeSettingDto1;
            this.TYOUSEI_NUMBER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TYOUSEI_NUMBER.RegistCheckMethod")));
            this.TYOUSEI_NUMBER.Size = new System.Drawing.Size(84, 20);
            this.TYOUSEI_NUMBER.TabIndex = 1;
            this.TYOUSEI_NUMBER.TabStop = false;
            this.TYOUSEI_NUMBER.Tag = "半角10文字以内で入力してください。";
            this.TYOUSEI_NUMBER.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TYOUSEI_NUMBER.WordWrap = false;
            this.TYOUSEI_NUMBER.Validated += new System.EventHandler(this.TYOUSEI_NUMBER_Validated);
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
            this.lblIdouNumber.Text = "調整番号";
            this.lblIdouNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TYOUSEI_DATE
            // 
            this.TYOUSEI_DATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.TYOUSEI_DATE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TYOUSEI_DATE.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.TYOUSEI_DATE.Checked = false;
            this.TYOUSEI_DATE.CustomFormat = "yyyy/MM/dd(ddd)";
            this.TYOUSEI_DATE.DateTimeNowYear = "";
            this.TYOUSEI_DATE.DBFieldsName = "IDOU_DATE";
            this.TYOUSEI_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            this.TYOUSEI_DATE.DisplayItemName = "移動日付";
            this.TYOUSEI_DATE.DisplayPopUp = null;
            this.TYOUSEI_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TYOUSEI_DATE.FocusOutCheckMethod")));
            this.TYOUSEI_DATE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TYOUSEI_DATE.ForeColor = System.Drawing.Color.Black;
            this.TYOUSEI_DATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TYOUSEI_DATE.IsInputErrorOccured = false;
            this.TYOUSEI_DATE.ItemDefinedTypes = "datetime";
            this.TYOUSEI_DATE.Location = new System.Drawing.Point(120, 7);
            this.TYOUSEI_DATE.MaxLength = 10;
            this.TYOUSEI_DATE.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.TYOUSEI_DATE.Name = "TYOUSEI_DATE";
            this.TYOUSEI_DATE.NullValue = "";
            this.TYOUSEI_DATE.PopupAfterExecute = null;
            this.TYOUSEI_DATE.PopupBeforeExecute = null;
            this.TYOUSEI_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TYOUSEI_DATE.PopupSearchSendParams")));
            this.TYOUSEI_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TYOUSEI_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TYOUSEI_DATE.popupWindowSetting")));
            this.TYOUSEI_DATE.ReadOnly = true;
            this.TYOUSEI_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TYOUSEI_DATE.RegistCheckMethod")));
            this.TYOUSEI_DATE.Size = new System.Drawing.Size(109, 20);
            this.TYOUSEI_DATE.TabIndex = 10013;
            this.TYOUSEI_DATE.TabStop = false;
            this.TYOUSEI_DATE.Tag = "";
            this.TYOUSEI_DATE.Text = "2013/12/03(火)";
            this.TYOUSEI_DATE.Value = new System.DateTime(2013, 12, 3, 0, 0, 0, 0);
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
            this.GYOUSHA_CD.Tag = "業者を指定してください（スペースキー押下にて、検索画面を表示します）";
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
            this.GYOUSHA_POPUP.Image = global::Shougun.Core.Stock.ZaikoTyouseiNyuuryoku.Properties.Resources.虫眼鏡;
            this.GYOUSHA_POPUP.ItemDefinedTypes = null;
            this.GYOUSHA_POPUP.LinkedSettingTextBox = null;
            this.GYOUSHA_POPUP.LinkedTextBoxs = null;
            this.GYOUSHA_POPUP.Location = new System.Drawing.Point(469, 32);
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
            this.MEISAI_ZAIKO_HINMEI_CD,
            this.MEISAI_ZAIKO_HINMEI_NAME,
            this.MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU,
            this.lbl2,
            this.MEISAI_TYOUSEI_RYOU,
            this.lbl1,
            this.MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU});
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
            this.DETAIL_Ichiran.Location = new System.Drawing.Point(5, 129);
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
            this.DETAIL_Ichiran.Size = new System.Drawing.Size(606, 291);
            this.DETAIL_Ichiran.TabIndex = 50;
            this.DETAIL_Ichiran.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DETAIL_Ichiran_CellEnter);
            this.DETAIL_Ichiran.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.DETAIL_Ichiran_CellValidating);
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
            this.GENBA_POPUP.Image = global::Shougun.Core.Stock.ZaikoTyouseiNyuuryoku.Properties.Resources.虫眼鏡;
            this.GENBA_POPUP.ItemDefinedTypes = null;
            this.GENBA_POPUP.LinkedSettingTextBox = null;
            this.GENBA_POPUP.LinkedTextBoxs = null;
            this.GENBA_POPUP.Location = new System.Drawing.Point(469, 58);
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
            this.GENBA_CD.Tag = "現場を指定してください（スペースキー押下にて、検索画面を表示します）";
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
            this.ZAIKO_HINMEI_POPUP.Image = global::Shougun.Core.Stock.ZaikoTyouseiNyuuryoku.Properties.Resources.虫眼鏡;
            this.ZAIKO_HINMEI_POPUP.ItemDefinedTypes = null;
            this.ZAIKO_HINMEI_POPUP.LinkedSettingTextBox = null;
            this.ZAIKO_HINMEI_POPUP.LinkedTextBoxs = null;
            this.ZAIKO_HINMEI_POPUP.Location = new System.Drawing.Point(469, 101);
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
            this.ZAIKO_HINMEI_POPUP.TabIndex = 12;
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
            this.ZAIKO_HINMEI_CD.Location = new System.Drawing.Point(120, 102);
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
            this.ZAIKO_HINMEI_CD.TabIndex = 11;
            this.ZAIKO_HINMEI_CD.TabStop = false;
            this.ZAIKO_HINMEI_CD.Tag = "在庫品名を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.ZAIKO_HINMEI_CD.ZeroPaddengFlag = true;
            this.ZAIKO_HINMEI_CD.Enter += new System.EventHandler(this.ZAIKO_HINMEI_CD_Enter);
            this.ZAIKO_HINMEI_CD.Validating += new System.ComponentModel.CancelEventHandler(this.ZAIKO_HINMEI_CD_Validating);
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
            this.ZAIKO_HINMEI_NAME.Location = new System.Drawing.Point(179, 102);
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
            this.lblZaikoHinmei.Location = new System.Drawing.Point(5, 102);
            this.lblZaikoHinmei.Name = "lblZaikoHinmei";
            this.lblZaikoHinmei.Size = new System.Drawing.Size(110, 20);
            this.lblZaikoHinmei.TabIndex = 56;
            this.lblZaikoHinmei.Text = "在庫品名";
            this.lblZaikoHinmei.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(558, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.TabIndex = 10015;
            this.label3.Tag = "";
            this.label3.Text = "備考１";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TYOUSEI_BIKOU1
            // 
            this.TYOUSEI_BIKOU1.BackColor = System.Drawing.SystemColors.Window;
            this.TYOUSEI_BIKOU1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TYOUSEI_BIKOU1.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.TYOUSEI_BIKOU1.DefaultBackColor = System.Drawing.Color.Empty;
            this.TYOUSEI_BIKOU1.DisplayPopUp = null;
            this.TYOUSEI_BIKOU1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TYOUSEI_BIKOU1.FocusOutCheckMethod")));
            this.TYOUSEI_BIKOU1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TYOUSEI_BIKOU1.ForeColor = System.Drawing.Color.Black;
            this.TYOUSEI_BIKOU1.FormatSetting = "";
            this.TYOUSEI_BIKOU1.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.TYOUSEI_BIKOU1.IsInputErrorOccured = false;
            this.TYOUSEI_BIKOU1.Location = new System.Drawing.Point(665, 34);
            this.TYOUSEI_BIKOU1.MaxLength = 40;
            this.TYOUSEI_BIKOU1.Name = "TYOUSEI_BIKOU1";
            this.TYOUSEI_BIKOU1.PopupAfterExecute = null;
            this.TYOUSEI_BIKOU1.PopupBeforeExecute = null;
            this.TYOUSEI_BIKOU1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TYOUSEI_BIKOU1.PopupSearchSendParams")));
            this.TYOUSEI_BIKOU1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TYOUSEI_BIKOU1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TYOUSEI_BIKOU1.popupWindowSetting")));
            this.TYOUSEI_BIKOU1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TYOUSEI_BIKOU1.RegistCheckMethod")));
            this.TYOUSEI_BIKOU1.Size = new System.Drawing.Size(298, 20);
            this.TYOUSEI_BIKOU1.TabIndex = 8;
            this.TYOUSEI_BIKOU1.TabStop = false;
            this.TYOUSEI_BIKOU1.Tag = "全角20文字以内で入力してください。";
            // 
            // TYOUSEI_BIKOU2
            // 
            this.TYOUSEI_BIKOU2.BackColor = System.Drawing.SystemColors.Window;
            this.TYOUSEI_BIKOU2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TYOUSEI_BIKOU2.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.TYOUSEI_BIKOU2.DefaultBackColor = System.Drawing.Color.Empty;
            this.TYOUSEI_BIKOU2.DisplayPopUp = null;
            this.TYOUSEI_BIKOU2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TYOUSEI_BIKOU2.FocusOutCheckMethod")));
            this.TYOUSEI_BIKOU2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TYOUSEI_BIKOU2.ForeColor = System.Drawing.Color.Black;
            this.TYOUSEI_BIKOU2.FormatSetting = "";
            this.TYOUSEI_BIKOU2.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.TYOUSEI_BIKOU2.IsInputErrorOccured = false;
            this.TYOUSEI_BIKOU2.Location = new System.Drawing.Point(665, 60);
            this.TYOUSEI_BIKOU2.MaxLength = 40;
            this.TYOUSEI_BIKOU2.Name = "TYOUSEI_BIKOU2";
            this.TYOUSEI_BIKOU2.PopupAfterExecute = null;
            this.TYOUSEI_BIKOU2.PopupBeforeExecute = null;
            this.TYOUSEI_BIKOU2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TYOUSEI_BIKOU2.PopupSearchSendParams")));
            this.TYOUSEI_BIKOU2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TYOUSEI_BIKOU2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TYOUSEI_BIKOU2.popupWindowSetting")));
            this.TYOUSEI_BIKOU2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TYOUSEI_BIKOU2.RegistCheckMethod")));
            this.TYOUSEI_BIKOU2.Size = new System.Drawing.Size(298, 20);
            this.TYOUSEI_BIKOU2.TabIndex = 9;
            this.TYOUSEI_BIKOU2.TabStop = false;
            this.TYOUSEI_BIKOU2.Tag = "全角20文字以内で入力してください。";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(558, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 10017;
            this.label1.Tag = "";
            this.label1.Text = "備考２";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TYOUSEI_BIKOU3
            // 
            this.TYOUSEI_BIKOU3.BackColor = System.Drawing.SystemColors.Window;
            this.TYOUSEI_BIKOU3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TYOUSEI_BIKOU3.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.TYOUSEI_BIKOU3.DefaultBackColor = System.Drawing.Color.Empty;
            this.TYOUSEI_BIKOU3.DisplayPopUp = null;
            this.TYOUSEI_BIKOU3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TYOUSEI_BIKOU3.FocusOutCheckMethod")));
            this.TYOUSEI_BIKOU3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TYOUSEI_BIKOU3.ForeColor = System.Drawing.Color.Black;
            this.TYOUSEI_BIKOU3.FormatSetting = "";
            this.TYOUSEI_BIKOU3.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.TYOUSEI_BIKOU3.IsInputErrorOccured = false;
            this.TYOUSEI_BIKOU3.Location = new System.Drawing.Point(665, 86);
            this.TYOUSEI_BIKOU3.MaxLength = 40;
            this.TYOUSEI_BIKOU3.Name = "TYOUSEI_BIKOU3";
            this.TYOUSEI_BIKOU3.PopupAfterExecute = null;
            this.TYOUSEI_BIKOU3.PopupBeforeExecute = null;
            this.TYOUSEI_BIKOU3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TYOUSEI_BIKOU3.PopupSearchSendParams")));
            this.TYOUSEI_BIKOU3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TYOUSEI_BIKOU3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TYOUSEI_BIKOU3.popupWindowSetting")));
            this.TYOUSEI_BIKOU3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TYOUSEI_BIKOU3.RegistCheckMethod")));
            this.TYOUSEI_BIKOU3.Size = new System.Drawing.Size(298, 20);
            this.TYOUSEI_BIKOU3.TabIndex = 10;
            this.TYOUSEI_BIKOU3.TabStop = false;
            this.TYOUSEI_BIKOU3.Tag = "全角20文字以内で入力してください。";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(558, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 10019;
            this.label2.Tag = "";
            this.label2.Text = "備考３";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(99, 433);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 20);
            this.label4.TabIndex = 10021;
            this.label4.Tag = "";
            this.label4.Text = "調整前在庫量合計";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TYOUSEI_BEFORE_GOUKEI
            // 
            this.TYOUSEI_BEFORE_GOUKEI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.TYOUSEI_BEFORE_GOUKEI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TYOUSEI_BEFORE_GOUKEI.DBFieldsName = "";
            this.TYOUSEI_BEFORE_GOUKEI.DefaultBackColor = System.Drawing.Color.Empty;
            this.TYOUSEI_BEFORE_GOUKEI.DisplayItemName = "";
            this.TYOUSEI_BEFORE_GOUKEI.DisplayPopUp = null;
            this.TYOUSEI_BEFORE_GOUKEI.ErrorMessage = "";
            this.TYOUSEI_BEFORE_GOUKEI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TYOUSEI_BEFORE_GOUKEI.FocusOutCheckMethod")));
            this.TYOUSEI_BEFORE_GOUKEI.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TYOUSEI_BEFORE_GOUKEI.ForeColor = System.Drawing.Color.Black;
            this.TYOUSEI_BEFORE_GOUKEI.FormatSetting = "システム設定(重量書式)";
            this.TYOUSEI_BEFORE_GOUKEI.GetCodeMasterField = "";
            this.TYOUSEI_BEFORE_GOUKEI.IsInputErrorOccured = false;
            this.TYOUSEI_BEFORE_GOUKEI.ItemDefinedTypes = "";
            this.TYOUSEI_BEFORE_GOUKEI.Location = new System.Drawing.Point(220, 433);
            this.TYOUSEI_BEFORE_GOUKEI.MaxLength = 0;
            this.TYOUSEI_BEFORE_GOUKEI.Name = "TYOUSEI_BEFORE_GOUKEI";
            this.TYOUSEI_BEFORE_GOUKEI.PopupAfterExecute = null;
            this.TYOUSEI_BEFORE_GOUKEI.PopupBeforeExecute = null;
            this.TYOUSEI_BEFORE_GOUKEI.PopupGetMasterField = "";
            this.TYOUSEI_BEFORE_GOUKEI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TYOUSEI_BEFORE_GOUKEI.PopupSearchSendParams")));
            this.TYOUSEI_BEFORE_GOUKEI.PopupSetFormField = "";
            this.TYOUSEI_BEFORE_GOUKEI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TYOUSEI_BEFORE_GOUKEI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TYOUSEI_BEFORE_GOUKEI.popupWindowSetting")));
            this.TYOUSEI_BEFORE_GOUKEI.ReadOnly = true;
            this.TYOUSEI_BEFORE_GOUKEI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TYOUSEI_BEFORE_GOUKEI.RegistCheckMethod")));
            this.TYOUSEI_BEFORE_GOUKEI.SetFormField = "";
            this.TYOUSEI_BEFORE_GOUKEI.Size = new System.Drawing.Size(135, 20);
            this.TYOUSEI_BEFORE_GOUKEI.TabIndex = 10022;
            this.TYOUSEI_BEFORE_GOUKEI.TabStop = false;
            this.TYOUSEI_BEFORE_GOUKEI.Tag = "　";
            this.TYOUSEI_BEFORE_GOUKEI.TextAlign = HorizontalAlignment.Right;
            // 
            // TYOUSEI_AFTER_GOUKEI
            // 
            this.TYOUSEI_AFTER_GOUKEI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.TYOUSEI_AFTER_GOUKEI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TYOUSEI_AFTER_GOUKEI.DBFieldsName = "";
            this.TYOUSEI_AFTER_GOUKEI.DefaultBackColor = System.Drawing.Color.Empty;
            this.TYOUSEI_AFTER_GOUKEI.DisplayItemName = "";
            this.TYOUSEI_AFTER_GOUKEI.DisplayPopUp = null;
            this.TYOUSEI_AFTER_GOUKEI.ErrorMessage = "";
            this.TYOUSEI_AFTER_GOUKEI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TYOUSEI_AFTER_GOUKEI.FocusOutCheckMethod")));
            this.TYOUSEI_AFTER_GOUKEI.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TYOUSEI_AFTER_GOUKEI.ForeColor = System.Drawing.Color.Black;
            this.TYOUSEI_AFTER_GOUKEI.FormatSetting = "システム設定(重量書式)";
            this.TYOUSEI_AFTER_GOUKEI.GetCodeMasterField = "";
            this.TYOUSEI_AFTER_GOUKEI.IsInputErrorOccured = false;
            this.TYOUSEI_AFTER_GOUKEI.ItemDefinedTypes = "";
            this.TYOUSEI_AFTER_GOUKEI.Location = new System.Drawing.Point(476, 433);
            this.TYOUSEI_AFTER_GOUKEI.MaxLength = 0;
            this.TYOUSEI_AFTER_GOUKEI.Name = "TYOUSEI_AFTER_GOUKEI";
            this.TYOUSEI_AFTER_GOUKEI.PopupAfterExecute = null;
            this.TYOUSEI_AFTER_GOUKEI.PopupBeforeExecute = null;
            this.TYOUSEI_AFTER_GOUKEI.PopupGetMasterField = "";
            this.TYOUSEI_AFTER_GOUKEI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TYOUSEI_AFTER_GOUKEI.PopupSearchSendParams")));
            this.TYOUSEI_AFTER_GOUKEI.PopupSetFormField = "";
            this.TYOUSEI_AFTER_GOUKEI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TYOUSEI_AFTER_GOUKEI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TYOUSEI_AFTER_GOUKEI.popupWindowSetting")));
            this.TYOUSEI_AFTER_GOUKEI.ReadOnly = true;
            this.TYOUSEI_AFTER_GOUKEI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TYOUSEI_AFTER_GOUKEI.RegistCheckMethod")));
            this.TYOUSEI_AFTER_GOUKEI.SetFormField = "";
            this.TYOUSEI_AFTER_GOUKEI.Size = new System.Drawing.Size(135, 20);
            this.TYOUSEI_AFTER_GOUKEI.TabIndex = 10024;
            this.TYOUSEI_AFTER_GOUKEI.TabStop = false;
            this.TYOUSEI_AFTER_GOUKEI.Tag = "　";
            this.TYOUSEI_AFTER_GOUKEI.TextAlign = HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(355, 433);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 20);
            this.label5.TabIndex = 10023;
            this.label5.Tag = "";
            this.label5.Text = "調整後在庫量合計";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MEISAI_ZAIKO_HINMEI_CD
            // 
            this.MEISAI_ZAIKO_HINMEI_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.MEISAI_ZAIKO_HINMEI_CD.DBFieldsName = "在庫品名CD";
            this.MEISAI_ZAIKO_HINMEI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.MEISAI_ZAIKO_HINMEI_CD.DefaultCellStyle = dataGridViewCellStyle2;
            this.MEISAI_ZAIKO_HINMEI_CD.DisplayItemName = "在庫品名CD";
            this.MEISAI_ZAIKO_HINMEI_CD.FocusOutCheckMethod = null;
            this.MEISAI_ZAIKO_HINMEI_CD.FormatSetting = "";
            this.MEISAI_ZAIKO_HINMEI_CD.GetCodeMasterField = "ZAIKO_HINMEI_CD,ZAIKO_HINMEI_NAME_RYAKU";
            this.MEISAI_ZAIKO_HINMEI_CD.HeaderText = "在庫品名CD※";
            this.MEISAI_ZAIKO_HINMEI_CD.MaxInputLength = 6;
            this.MEISAI_ZAIKO_HINMEI_CD.Name = "MEISAI_ZAIKO_HINMEI_CD";
            this.MEISAI_ZAIKO_HINMEI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MEISAI_ZAIKO_HINMEI_CD.PopupSearchSendParams")));
            this.MEISAI_ZAIKO_HINMEI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_ZAIKO_HINMEI;
            this.MEISAI_ZAIKO_HINMEI_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.MEISAI_ZAIKO_HINMEI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("MEISAI_ZAIKO_HINMEI_CD.popupWindowSetting")));
            this.MEISAI_ZAIKO_HINMEI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MEISAI_ZAIKO_HINMEI_CD.RegistCheckMethod")));
            this.MEISAI_ZAIKO_HINMEI_CD.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MEISAI_ZAIKO_HINMEI_CD.SetFormField = "MEISAI_ZAIKO_HINMEI_CD,MEISAI_ZAIKO_HINMEI_NAME";
            this.MEISAI_ZAIKO_HINMEI_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MEISAI_ZAIKO_HINMEI_CD.ToolTipText = "半角6桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.MEISAI_ZAIKO_HINMEI_CD.ZeroPaddengFlag = true;
            // 
            // MEISAI_ZAIKO_HINMEI_NAME
            // 
            this.MEISAI_ZAIKO_HINMEI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.MEISAI_ZAIKO_HINMEI_NAME.DefaultCellStyle = dataGridViewCellStyle3;
            this.MEISAI_ZAIKO_HINMEI_NAME.FocusOutCheckMethod = null;
            this.MEISAI_ZAIKO_HINMEI_NAME.HeaderText = "在庫品名";
            this.MEISAI_ZAIKO_HINMEI_NAME.Name = "MEISAI_ZAIKO_HINMEI_NAME";
            this.MEISAI_ZAIKO_HINMEI_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MEISAI_ZAIKO_HINMEI_NAME.PopupSearchSendParams")));
            this.MEISAI_ZAIKO_HINMEI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MEISAI_ZAIKO_HINMEI_NAME.popupWindowSetting = null;
            this.MEISAI_ZAIKO_HINMEI_NAME.ReadOnly = true;
            this.MEISAI_ZAIKO_HINMEI_NAME.RegistCheckMethod = null;
            this.MEISAI_ZAIKO_HINMEI_NAME.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MEISAI_ZAIKO_HINMEI_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MEISAI_ZAIKO_HINMEI_NAME.Width = 150;
            // 
            // MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU
            // 
            this.MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU.DefaultCellStyle = dataGridViewCellStyle4;
            this.MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU.FocusOutCheckMethod")));
            this.MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU.FormatSetting = "システム設定(重量書式)";
            this.MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU.HeaderText = "調整前在庫量";
            this.MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU.Name = "MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU";
            this.MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU.PopupSearchSendParams")));
            this.MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU.popupWindowSetting")));
            this.MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU.ReadOnly = true;
            this.MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU.RegistCheckMethod")));
            this.MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            // MEISAI_TYOUSEI_RYOU
            // 
            this.MEISAI_TYOUSEI_RYOU.DBFieldsName = "調整量";
            this.MEISAI_TYOUSEI_RYOU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.MEISAI_TYOUSEI_RYOU.DefaultCellStyle = dataGridViewCellStyle6;
            this.MEISAI_TYOUSEI_RYOU.DisplayItemName = "調整量";
            this.MEISAI_TYOUSEI_RYOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MEISAI_TYOUSEI_RYOU.FocusOutCheckMethod")));
            this.MEISAI_TYOUSEI_RYOU.FormatSetting = "システム設定(重量書式)";
            this.MEISAI_TYOUSEI_RYOU.HeaderText = "調整量※";
            this.MEISAI_TYOUSEI_RYOU.Name = "MEISAI_TYOUSEI_RYOU";
            this.MEISAI_TYOUSEI_RYOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MEISAI_TYOUSEI_RYOU.PopupSearchSendParams")));
            this.MEISAI_TYOUSEI_RYOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MEISAI_TYOUSEI_RYOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("MEISAI_TYOUSEI_RYOU.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            1410065407,
            2,
            0,
            196608});
            rangeSettingDto2.Min = new decimal(new int[] {
            1410065407,
            2,
            0,
            -2147287040});
            this.MEISAI_TYOUSEI_RYOU.RangeSetting = rangeSettingDto2;
            this.MEISAI_TYOUSEI_RYOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MEISAI_TYOUSEI_RYOU.RegistCheckMethod")));
            this.MEISAI_TYOUSEI_RYOU.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MEISAI_TYOUSEI_RYOU.ShortItemName = "調整量";
            this.MEISAI_TYOUSEI_RYOU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MEISAI_TYOUSEI_RYOU.ToolTipText = "整数部分は半角7桁で入力してください。小数点以下はシステム設定を参照します。";
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
            // MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU
            // 
            this.MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU.DefaultCellStyle = dataGridViewCellStyle8;
            this.MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU.FocusOutCheckMethod")));
            this.MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU.FormatSetting = "システム設定(重量書式)";
            this.MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU.HeaderText = "調整後在庫量";
            this.MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU.Name = "MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU";
            this.MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU.PopupSearchSendParams")));
            this.MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU.popupWindowSetting")));
            this.MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU.ReadOnly = true;
            this.MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU.RegistCheckMethod")));
            this.MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 477);
            this.Controls.Add(this.TYOUSEI_AFTER_GOUKEI);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TYOUSEI_BEFORE_GOUKEI);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TYOUSEI_BIKOU3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TYOUSEI_BIKOU2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TYOUSEI_BIKOU1);
            this.Controls.Add(this.label3);
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
            this.Controls.Add(this.TYOUSEI_DATE);
            this.Controls.Add(this.lblNyuuryokuDate);
            this.Controls.Add(this.TYOUSEI_NUMBER);
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

        internal r_framework.CustomControl.CustomNumericTextBox2 TYOUSEI_NUMBER;
        internal System.Windows.Forms.Button NEXT_BUTTON;
        internal System.Windows.Forms.Button PREV_BUTTON;
        internal System.Windows.Forms.Label lblIdouNumber;
        internal r_framework.CustomControl.CustomDateTimePicker TYOUSEI_DATE;
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
        internal System.Windows.Forms.Label label3;
        internal r_framework.CustomControl.CustomTextBox TYOUSEI_BIKOU1;
        internal r_framework.CustomControl.CustomTextBox TYOUSEI_BIKOU2;
        internal System.Windows.Forms.Label label1;
        internal r_framework.CustomControl.CustomTextBox TYOUSEI_BIKOU3;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label4;
        internal r_framework.CustomControl.CustomNumericTextBox2 TYOUSEI_BEFORE_GOUKEI;
        internal r_framework.CustomControl.CustomNumericTextBox2 TYOUSEI_AFTER_GOUKEI;
        internal System.Windows.Forms.Label label5;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn MEISAI_ZAIKO_HINMEI_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn MEISAI_ZAIKO_HINMEI_NAME;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column MEISAI_TYOUSEI_BEFORE_ZAIKO_RYOU;
        private r_framework.CustomControl.DgvCustomTextBoxColumn lbl2;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column MEISAI_TYOUSEI_RYOU;
        private r_framework.CustomControl.DgvCustomTextBoxColumn lbl1;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column MEISAI_TYOUSEI_AFTER_ZAIKO_RYOU;
    }
}