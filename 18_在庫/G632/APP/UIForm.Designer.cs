namespace Shougun.Core.Stock.ZaikoIdouIchiran
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
            this.GyoushaPopupButton = new r_framework.CustomControl.CustomPopupOpenButton();
            this.GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GYOUSHA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.GYOUSHA_LABEL = new System.Windows.Forms.Label();
            this.GENBA_LABEL = new System.Windows.Forms.Label();
            this.ZAIKOHINRMI_LABEL = new System.Windows.Forms.Label();
            this.GenbaPopupButton = new r_framework.CustomControl.CustomPopupOpenButton();
            this.GENBA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GENBA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.ZaikoHinmeiPopupButton = new r_framework.CustomControl.CustomPopupOpenButton();
            this.ZAIKO_HINMEI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.ZAIKO_HINMEI_NAME = new r_framework.CustomControl.CustomTextBox();
            this.HIDUKE_FROM = new r_framework.CustomControl.CustomDateTimePicker();
            this.lblNyuuryokuDate = new System.Windows.Forms.Label();
            this.HIDUKE_TO = new r_framework.CustomControl.CustomDateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.searchString.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchString.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.FocusOutCheckMethod")));
            this.searchString.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.searchString.Location = new System.Drawing.Point(0, 1);
            this.searchString.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("searchString.PopupSearchSendParams")));
            this.searchString.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("searchString.popupWindowSetting")));
            this.searchString.ReadOnly = true;
            this.searchString.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.RegistCheckMethod")));
            this.searchString.Size = new System.Drawing.Size(1000, 95);
            this.searchString.TabIndex = 1;
            this.searchString.Visible = false;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn1.Location = new System.Drawing.Point(2, 339);
            this.bt_ptn1.Size = new System.Drawing.Size(192, 24);
            this.bt_ptn1.TabIndex = 4;
            this.bt_ptn1.Text = "パターン１";
            this.bt_ptn1.Visible = false;
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn2.Location = new System.Drawing.Point(203, 339);
            this.bt_ptn2.Size = new System.Drawing.Size(192, 24);
            this.bt_ptn2.TabIndex = 5;
            this.bt_ptn2.Text = "パターン２";
            this.bt_ptn2.Visible = false;
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn3.Location = new System.Drawing.Point(405, 339);
            this.bt_ptn3.Size = new System.Drawing.Size(192, 24);
            this.bt_ptn3.TabIndex = 6;
            this.bt_ptn3.Text = "パターン３";
            this.bt_ptn3.Visible = false;
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn4.Location = new System.Drawing.Point(607, 339);
            this.bt_ptn4.Size = new System.Drawing.Size(192, 24);
            this.bt_ptn4.TabIndex = 7;
            this.bt_ptn4.Text = "パターン４";
            this.bt_ptn4.Visible = false;
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn5.Location = new System.Drawing.Point(808, 339);
            this.bt_ptn5.Size = new System.Drawing.Size(192, 24);
            this.bt_ptn5.TabIndex = 8;
            this.bt_ptn5.Text = "パターン５";
            this.bt_ptn5.Visible = false;
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.Location = new System.Drawing.Point(2, 120);
            this.customSortHeader1.Size = new System.Drawing.Size(997, 24);
            // 
            // customSearchHeader1
            // 
            this.customSearchHeader1.Location = new System.Drawing.Point(2, 97);
            this.customSearchHeader1.Visible = true;
            // 
            // GyoushaPopupButton
            // 
            this.GyoushaPopupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.GyoushaPopupButton.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.GyoushaPopupButton.DBFieldsName = null;
            this.GyoushaPopupButton.DefaultBackColor = System.Drawing.Color.Empty;
            this.GyoushaPopupButton.DisplayItemName = "業者CD";
            this.GyoushaPopupButton.DisplayPopUp = null;
            this.GyoushaPopupButton.ErrorMessage = null;
            this.GyoushaPopupButton.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GyoushaPopupButton.FocusOutCheckMethod")));
            this.GyoushaPopupButton.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.GyoushaPopupButton.GetCodeMasterField = null;
            this.GyoushaPopupButton.Image = ((System.Drawing.Image)(resources.GetObject("GyoushaPopupButton.Image")));
            this.GyoushaPopupButton.ItemDefinedTypes = null;
            this.GyoushaPopupButton.LinkedTextBoxs = null;
            this.GyoushaPopupButton.Location = new System.Drawing.Point(461, 24);
            this.GyoushaPopupButton.Name = "GyoushaPopupButton";
            this.GyoushaPopupButton.PopupAfterExecute = null;
            this.GyoushaPopupButton.PopupAfterExecuteMethod = "GyoushaPopupAfter";
            this.GyoushaPopupButton.PopupBeforeExecute = null;
            this.GyoushaPopupButton.PopupBeforeExecuteMethod = "GyoushaPopupBefore";
            this.GyoushaPopupButton.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GyoushaPopupButton.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GyoushaPopupButton.PopupSearchSendParams")));
            this.GyoushaPopupButton.PopupSetFormField = "GYOUSHA_CD,GYOUSHA_NAME";
            this.GyoushaPopupButton.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GyoushaPopupButton.PopupWindowName = "検索共通ポップアップ";
            this.GyoushaPopupButton.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GyoushaPopupButton.popupWindowSetting")));
            this.GyoushaPopupButton.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GyoushaPopupButton.RegistCheckMethod")));
            this.GyoushaPopupButton.SearchDisplayFlag = 0;
            this.GyoushaPopupButton.SetFormField = "GYOUSHA_CD,GYOUSHA_NAME";
            this.GyoushaPopupButton.ShortItemName = "業者CD";
            this.GyoushaPopupButton.Size = new System.Drawing.Size(22, 22);
            this.GyoushaPopupButton.TabIndex = 371;
            this.GyoushaPopupButton.TabStop = false;
            this.GyoushaPopupButton.UseVisualStyleBackColor = false;
            this.GyoushaPopupButton.ZeroPaddengFlag = false;
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
            this.GYOUSHA_CD.DBFieldsName = "GYOUSHA_CD";
            this.GYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_CD.DisplayItemName = "業者CD";
            this.GYOUSHA_CD.DisplayPopUp = null;
            this.GYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.FocusOutCheckMethod")));
            this.GYOUSHA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_CD.GetCodeMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GYOUSHA_CD.IsInputErrorOccured = false;
            this.GYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.GYOUSHA_CD.Location = new System.Drawing.Point(115, 25);
            this.GYOUSHA_CD.MaxLength = 6;
            this.GYOUSHA_CD.Name = "GYOUSHA_CD";
            this.GYOUSHA_CD.PopupAfterExecute = null;
            this.GYOUSHA_CD.PopupAfterExecuteMethod = "GyoushaPopupAfter";
            this.GYOUSHA_CD.PopupBeforeExecute = null;
            this.GYOUSHA_CD.PopupBeforeExecuteMethod = "GyoushaPopupBefore";
            this.GYOUSHA_CD.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_CD.PopupSearchSendParams")));
            this.GYOUSHA_CD.PopupSetFormField = "GYOUSHA_CD,GYOUSHA_NAME";
            this.GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_CD.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_CD.popupWindowSetting")));
            this.GYOUSHA_CD.prevText = null;
            this.GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.RegistCheckMethod")));
            this.GYOUSHA_CD.SetFormField = "GYOUSHA_CD,GYOUSHA_NAME";
            this.GYOUSHA_CD.ShortItemName = "業者CD";
            this.GYOUSHA_CD.Size = new System.Drawing.Size(60, 20);
            this.GYOUSHA_CD.TabIndex = 1;
            this.GYOUSHA_CD.Tag = "業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GYOUSHA_CD.ZeroPaddengFlag = true;
            this.GYOUSHA_CD.Enter += new System.EventHandler(this.GYOUSHA_CD_Enter);
            this.GYOUSHA_CD.Validated += new System.EventHandler(this.GYOUSHA_CD_Validated);
            // 
            // GYOUSHA_NAME
            // 
            this.GYOUSHA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GYOUSHA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_NAME.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.GYOUSHA_NAME.DBFieldsName = "GYOUSHA_NAME";
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
            this.GYOUSHA_NAME.Location = new System.Drawing.Point(174, 25);
            this.GYOUSHA_NAME.MaxLength = 20;
            this.GYOUSHA_NAME.Name = "GYOUSHA_NAME";
            this.GYOUSHA_NAME.PopupAfterExecute = null;
            this.GYOUSHA_NAME.PopupBeforeExecute = null;
            this.GYOUSHA_NAME.PopupGetMasterField = "";
            this.GYOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_NAME.PopupSearchSendParams")));
            this.GYOUSHA_NAME.PopupSetFormField = "";
            this.GYOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_NAME.popupWindowSetting")));
            this.GYOUSHA_NAME.prevText = null;
            this.GYOUSHA_NAME.ReadOnly = true;
            this.GYOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME.RegistCheckMethod")));
            this.GYOUSHA_NAME.SetFormField = "";
            this.GYOUSHA_NAME.Size = new System.Drawing.Size(285, 20);
            this.GYOUSHA_NAME.TabIndex = 373;
            this.GYOUSHA_NAME.TabStop = false;
            this.GYOUSHA_NAME.Tag = "　";
            // 
            // GYOUSHA_LABEL
            // 
            this.GYOUSHA_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.GYOUSHA_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GYOUSHA_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GYOUSHA_LABEL.ForeColor = System.Drawing.Color.White;
            this.GYOUSHA_LABEL.Location = new System.Drawing.Point(3, 25);
            this.GYOUSHA_LABEL.Name = "GYOUSHA_LABEL";
            this.GYOUSHA_LABEL.Size = new System.Drawing.Size(110, 20);
            this.GYOUSHA_LABEL.TabIndex = 372;
            this.GYOUSHA_LABEL.Text = "業者";
            this.GYOUSHA_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GENBA_LABEL
            // 
            this.GENBA_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.GENBA_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GENBA_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GENBA_LABEL.ForeColor = System.Drawing.Color.White;
            this.GENBA_LABEL.Location = new System.Drawing.Point(3, 47);
            this.GENBA_LABEL.Name = "GENBA_LABEL";
            this.GENBA_LABEL.Size = new System.Drawing.Size(110, 20);
            this.GENBA_LABEL.TabIndex = 375;
            this.GENBA_LABEL.Text = "現場";
            this.GENBA_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ZAIKOHINRMI_LABEL
            // 
            this.ZAIKOHINRMI_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.ZAIKOHINRMI_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ZAIKOHINRMI_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ZAIKOHINRMI_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ZAIKOHINRMI_LABEL.ForeColor = System.Drawing.Color.White;
            this.ZAIKOHINRMI_LABEL.Location = new System.Drawing.Point(3, 69);
            this.ZAIKOHINRMI_LABEL.Name = "ZAIKOHINRMI_LABEL";
            this.ZAIKOHINRMI_LABEL.Size = new System.Drawing.Size(110, 20);
            this.ZAIKOHINRMI_LABEL.TabIndex = 377;
            this.ZAIKOHINRMI_LABEL.Text = "在庫品名";
            this.ZAIKOHINRMI_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GenbaPopupButton
            // 
            this.GenbaPopupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.GenbaPopupButton.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.GenbaPopupButton.DBFieldsName = null;
            this.GenbaPopupButton.DefaultBackColor = System.Drawing.Color.Empty;
            this.GenbaPopupButton.DisplayItemName = "現場CD";
            this.GenbaPopupButton.DisplayPopUp = null;
            this.GenbaPopupButton.ErrorMessage = null;
            this.GenbaPopupButton.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GenbaPopupButton.FocusOutCheckMethod")));
            this.GenbaPopupButton.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.GenbaPopupButton.GetCodeMasterField = null;
            this.GenbaPopupButton.Image = ((System.Drawing.Image)(resources.GetObject("GenbaPopupButton.Image")));
            this.GenbaPopupButton.ItemDefinedTypes = null;
            this.GenbaPopupButton.LinkedTextBoxs = null;
            this.GenbaPopupButton.Location = new System.Drawing.Point(461, 46);
            this.GenbaPopupButton.Name = "GenbaPopupButton";
            this.GenbaPopupButton.PopupAfterExecute = null;
            this.GenbaPopupButton.PopupAfterExecuteMethod = "GenbaPopupAfter";
            this.GenbaPopupButton.PopupBeforeExecute = null;
            this.GenbaPopupButton.PopupBeforeExecuteMethod = "GenbaPopupBefore";
            this.GenbaPopupButton.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GenbaPopupButton.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GenbaPopupButton.PopupSearchSendParams")));
            this.GenbaPopupButton.PopupSetFormField = "GENBA_CD,GENBA_NAME,GYOUSHA_CD, GYOUSHA_NAME";
            this.GenbaPopupButton.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GenbaPopupButton.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GenbaPopupButton.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GenbaPopupButton.popupWindowSetting")));
            this.GenbaPopupButton.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GenbaPopupButton.RegistCheckMethod")));
            this.GenbaPopupButton.SearchDisplayFlag = 0;
            this.GenbaPopupButton.SetFormField = "GENBA_CD,GENBA_NAME,GYOUSHA_CD, GYOUSHA_NAME";
            this.GenbaPopupButton.ShortItemName = "業者CD";
            this.GenbaPopupButton.Size = new System.Drawing.Size(22, 22);
            this.GenbaPopupButton.TabIndex = 379;
            this.GenbaPopupButton.TabStop = false;
            this.GenbaPopupButton.UseVisualStyleBackColor = false;
            this.GenbaPopupButton.ZeroPaddengFlag = false;
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
            this.GENBA_CD.DBFieldsName = "GENBA_CD";
            this.GENBA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_CD.DisplayItemName = "現場CD";
            this.GENBA_CD.DisplayPopUp = null;
            this.GENBA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.FocusOutCheckMethod")));
            this.GENBA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GENBA_CD.ForeColor = System.Drawing.Color.Black;
            this.GENBA_CD.GetCodeMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GENBA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GENBA_CD.IsInputErrorOccured = false;
            this.GENBA_CD.ItemDefinedTypes = "varchar";
            this.GENBA_CD.Location = new System.Drawing.Point(115, 47);
            this.GENBA_CD.MaxLength = 6;
            this.GENBA_CD.Name = "GENBA_CD";
            this.GENBA_CD.PopupAfterExecute = null;
            this.GENBA_CD.PopupAfterExecuteMethod = "GenbaPopupAfter";
            this.GENBA_CD.PopupBeforeExecute = null;
            this.GENBA_CD.PopupBeforeExecuteMethod = "GenbaPopupBefore";
            this.GENBA_CD.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_CD.PopupSearchSendParams")));
            this.GENBA_CD.PopupSetFormField = "GENBA_CD,GENBA_NAME,GYOUSHA_CD, GYOUSHA_NAME";
            this.GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_CD.popupWindowSetting")));
            this.GENBA_CD.prevText = null;
            this.GENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.RegistCheckMethod")));
            this.GENBA_CD.SetFormField = "GENBA_CD,GENBA_NAME,GYOUSHA_CD, GYOUSHA_NAME";
            this.GENBA_CD.ShortItemName = "現場CD";
            this.GENBA_CD.Size = new System.Drawing.Size(60, 20);
            this.GENBA_CD.TabIndex = 2;
            this.GENBA_CD.Tag = "現場を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GENBA_CD.ZeroPaddengFlag = true;
            this.GENBA_CD.Enter += new System.EventHandler(this.GENBA_CD_Enter);
            this.GENBA_CD.Validated += new System.EventHandler(this.GENBA_CD_Validated);
            // 
            // GENBA_NAME
            // 
            this.GENBA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GENBA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_NAME.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.GENBA_NAME.DBFieldsName = "GENBA_NAME";
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
            this.GENBA_NAME.Location = new System.Drawing.Point(174, 47);
            this.GENBA_NAME.MaxLength = 20;
            this.GENBA_NAME.Name = "GENBA_NAME";
            this.GENBA_NAME.PopupAfterExecute = null;
            this.GENBA_NAME.PopupBeforeExecute = null;
            this.GENBA_NAME.PopupGetMasterField = "";
            this.GENBA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_NAME.PopupSearchSendParams")));
            this.GENBA_NAME.PopupSetFormField = "";
            this.GENBA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_NAME.popupWindowSetting")));
            this.GENBA_NAME.prevText = null;
            this.GENBA_NAME.ReadOnly = true;
            this.GENBA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME.RegistCheckMethod")));
            this.GENBA_NAME.SetFormField = "";
            this.GENBA_NAME.Size = new System.Drawing.Size(285, 20);
            this.GENBA_NAME.TabIndex = 380;
            this.GENBA_NAME.TabStop = false;
            this.GENBA_NAME.Tag = "　";
            // 
            // ZaikoHinmeiPopupButton
            // 
            this.ZaikoHinmeiPopupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.ZaikoHinmeiPopupButton.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ZaikoHinmeiPopupButton.DBFieldsName = null;
            this.ZaikoHinmeiPopupButton.DefaultBackColor = System.Drawing.Color.Empty;
            this.ZaikoHinmeiPopupButton.DisplayItemName = "在庫品名CD";
            this.ZaikoHinmeiPopupButton.DisplayPopUp = null;
            this.ZaikoHinmeiPopupButton.ErrorMessage = null;
            this.ZaikoHinmeiPopupButton.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZaikoHinmeiPopupButton.FocusOutCheckMethod")));
            this.ZaikoHinmeiPopupButton.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.ZaikoHinmeiPopupButton.GetCodeMasterField = null;
            this.ZaikoHinmeiPopupButton.Image = ((System.Drawing.Image)(resources.GetObject("ZaikoHinmeiPopupButton.Image")));
            this.ZaikoHinmeiPopupButton.ItemDefinedTypes = null;
            this.ZaikoHinmeiPopupButton.LinkedTextBoxs = null;
            this.ZaikoHinmeiPopupButton.Location = new System.Drawing.Point(461, 68);
            this.ZaikoHinmeiPopupButton.Name = "ZaikoHinmeiPopupButton";
            this.ZaikoHinmeiPopupButton.PopupAfterExecute = null;
            this.ZaikoHinmeiPopupButton.PopupAfterExecuteMethod = "ZaikoHinmeiPopupAfter";
            this.ZaikoHinmeiPopupButton.PopupBeforeExecute = null;
            this.ZaikoHinmeiPopupButton.PopupBeforeExecuteMethod = "ZaikoHinmeiPopupBefore";
            this.ZaikoHinmeiPopupButton.PopupGetMasterField = "ZAIKO_HINMEI_CD,ZAIKO_HINMEI_NAME_RYAKU";
            this.ZaikoHinmeiPopupButton.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZaikoHinmeiPopupButton.PopupSearchSendParams")));
            this.ZaikoHinmeiPopupButton.PopupSetFormField = "ZAIKO_HINMEI_CD,ZAIKO_HINMEI_NAME";
            this.ZaikoHinmeiPopupButton.PopupWindowId = r_framework.Const.WINDOW_ID.M_ZAIKO_HINMEI;
            this.ZaikoHinmeiPopupButton.PopupWindowName = "マスタ共通ポップアップ";
            this.ZaikoHinmeiPopupButton.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZaikoHinmeiPopupButton.popupWindowSetting")));
            this.ZaikoHinmeiPopupButton.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZaikoHinmeiPopupButton.RegistCheckMethod")));
            this.ZaikoHinmeiPopupButton.SearchDisplayFlag = 0;
            this.ZaikoHinmeiPopupButton.SetFormField = "ZAIKO_HINMEI_CD,ZAIKO_HINMEI_NAME";
            this.ZaikoHinmeiPopupButton.ShortItemName = "在庫品名CD";
            this.ZaikoHinmeiPopupButton.Size = new System.Drawing.Size(22, 22);
            this.ZaikoHinmeiPopupButton.TabIndex = 382;
            this.ZaikoHinmeiPopupButton.TabStop = false;
            this.ZaikoHinmeiPopupButton.UseVisualStyleBackColor = false;
            this.ZaikoHinmeiPopupButton.ZeroPaddengFlag = false;
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
            this.ZAIKO_HINMEI_CD.DBFieldsName = "GYOUSHA_CD";
            this.ZAIKO_HINMEI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.ZAIKO_HINMEI_CD.DisplayItemName = "在庫品名CD";
            this.ZAIKO_HINMEI_CD.DisplayPopUp = null;
            this.ZAIKO_HINMEI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_CD.FocusOutCheckMethod")));
            this.ZAIKO_HINMEI_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ZAIKO_HINMEI_CD.ForeColor = System.Drawing.Color.Black;
            this.ZAIKO_HINMEI_CD.GetCodeMasterField = "ZAIKO_HINMEI_CD,ZAIKO_HINMEI_NAME_RYAKU";
            this.ZAIKO_HINMEI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ZAIKO_HINMEI_CD.IsInputErrorOccured = false;
            this.ZAIKO_HINMEI_CD.ItemDefinedTypes = "varchar";
            this.ZAIKO_HINMEI_CD.Location = new System.Drawing.Point(115, 69);
            this.ZAIKO_HINMEI_CD.MaxLength = 6;
            this.ZAIKO_HINMEI_CD.Name = "ZAIKO_HINMEI_CD";
            this.ZAIKO_HINMEI_CD.PopupAfterExecute = null;
            this.ZAIKO_HINMEI_CD.PopupAfterExecuteMethod = "ZaikoHinmeiPopupAfter";
            this.ZAIKO_HINMEI_CD.PopupBeforeExecute = null;
            this.ZAIKO_HINMEI_CD.PopupBeforeExecuteMethod = "ZaikoHinmeiPopupBefore";
            this.ZAIKO_HINMEI_CD.PopupGetMasterField = "ZAIKO_HINMEI_CD,ZAIKO_HINMEI_NAME_RYAKU";
            this.ZAIKO_HINMEI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZAIKO_HINMEI_CD.PopupSearchSendParams")));
            this.ZAIKO_HINMEI_CD.PopupSetFormField = "ZAIKO_HINMEI_CD,ZAIKO_HINMEI_NAME";
            this.ZAIKO_HINMEI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_ZAIKO_HINMEI;
            this.ZAIKO_HINMEI_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.ZAIKO_HINMEI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZAIKO_HINMEI_CD.popupWindowSetting")));
            this.ZAIKO_HINMEI_CD.prevText = null;
            this.ZAIKO_HINMEI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_CD.RegistCheckMethod")));
            this.ZAIKO_HINMEI_CD.SetFormField = "ZAIKO_HINMEI_CD,ZAIKO_HINMEI_NAME";
            this.ZAIKO_HINMEI_CD.ShortItemName = "在庫品名CD";
            this.ZAIKO_HINMEI_CD.Size = new System.Drawing.Size(60, 20);
            this.ZAIKO_HINMEI_CD.TabIndex = 3;
            this.ZAIKO_HINMEI_CD.Tag = "在庫品名を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.ZAIKO_HINMEI_CD.ZeroPaddengFlag = true;
            this.ZAIKO_HINMEI_CD.Validated += new System.EventHandler(this.ZAIKO_HINMEI_CD_Validated);
            // 
            // ZAIKO_HINMEI_NAME
            // 
            this.ZAIKO_HINMEI_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ZAIKO_HINMEI_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ZAIKO_HINMEI_NAME.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.ZAIKO_HINMEI_NAME.DBFieldsName = "ZAIKO_HINMEI_NAME";
            this.ZAIKO_HINMEI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.ZAIKO_HINMEI_NAME.DisplayItemName = "";
            this.ZAIKO_HINMEI_NAME.DisplayPopUp = null;
            this.ZAIKO_HINMEI_NAME.ErrorMessage = "";
            this.ZAIKO_HINMEI_NAME.FocusOutCheckMethod = null;
            this.ZAIKO_HINMEI_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ZAIKO_HINMEI_NAME.ForeColor = System.Drawing.Color.Black;
            this.ZAIKO_HINMEI_NAME.GetCodeMasterField = "";
            this.ZAIKO_HINMEI_NAME.IsInputErrorOccured = false;
            this.ZAIKO_HINMEI_NAME.ItemDefinedTypes = "";
            this.ZAIKO_HINMEI_NAME.Location = new System.Drawing.Point(174, 69);
            this.ZAIKO_HINMEI_NAME.MaxLength = 20;
            this.ZAIKO_HINMEI_NAME.Name = "ZAIKO_HINMEI_NAME";
            this.ZAIKO_HINMEI_NAME.PopupAfterExecute = null;
            this.ZAIKO_HINMEI_NAME.PopupBeforeExecute = null;
            this.ZAIKO_HINMEI_NAME.PopupGetMasterField = "";
            this.ZAIKO_HINMEI_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZAIKO_HINMEI_NAME.PopupSearchSendParams")));
            this.ZAIKO_HINMEI_NAME.PopupSetFormField = "";
            this.ZAIKO_HINMEI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ZAIKO_HINMEI_NAME.popupWindowSetting = null;
            this.ZAIKO_HINMEI_NAME.prevText = null;
            this.ZAIKO_HINMEI_NAME.ReadOnly = true;
            this.ZAIKO_HINMEI_NAME.RegistCheckMethod = null;
            this.ZAIKO_HINMEI_NAME.SetFormField = "";
            this.ZAIKO_HINMEI_NAME.Size = new System.Drawing.Size(285, 20);
            this.ZAIKO_HINMEI_NAME.TabIndex = 383;
            this.ZAIKO_HINMEI_NAME.TabStop = false;
            this.ZAIKO_HINMEI_NAME.Tag = "　";
            // 
            // HIDUKE_FROM
            // 
            this.HIDUKE_FROM.BackColor = System.Drawing.SystemColors.Window;
            this.HIDUKE_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HIDUKE_FROM.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.HIDUKE_FROM.Checked = false;
            this.HIDUKE_FROM.CustomFormat = "yyyy/MM/dd(ddd)";
            this.HIDUKE_FROM.DateTimeNowYear = "";
            this.HIDUKE_FROM.DBFieldsName = "IDOU_DATE";
            this.HIDUKE_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.HIDUKE_FROM.DisplayItemName = "移動日付";
            this.HIDUKE_FROM.DisplayPopUp = null;
            this.HIDUKE_FROM.FocusOutCheckMethod = null;
            this.HIDUKE_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HIDUKE_FROM.ForeColor = System.Drawing.Color.Black;
            this.HIDUKE_FROM.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.HIDUKE_FROM.IsInputErrorOccured = false;
            this.HIDUKE_FROM.ItemDefinedTypes = "datetime";
            this.HIDUKE_FROM.Location = new System.Drawing.Point(616, 25);
            this.HIDUKE_FROM.MaxLength = 10;
            this.HIDUKE_FROM.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.HIDUKE_FROM.Name = "HIDUKE_FROM";
            this.HIDUKE_FROM.NullValue = "";
            this.HIDUKE_FROM.PopupAfterExecute = null;
            this.HIDUKE_FROM.PopupBeforeExecute = null;
            this.HIDUKE_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIDUKE_FROM.PopupSearchSendParams")));
            this.HIDUKE_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIDUKE_FROM.popupWindowSetting = null;
            this.HIDUKE_FROM.RegistCheckMethod = null;
            this.HIDUKE_FROM.Size = new System.Drawing.Size(109, 20);
            this.HIDUKE_FROM.TabIndex = 4;
            this.HIDUKE_FROM.Tag = "";
            this.HIDUKE_FROM.Text = "2013/12/03(火)";
            this.HIDUKE_FROM.Value = new System.DateTime(2013, 12, 3, 0, 0, 0, 0);
            // 
            // lblNyuuryokuDate
            // 
            this.lblNyuuryokuDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblNyuuryokuDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNyuuryokuDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblNyuuryokuDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNyuuryokuDate.ForeColor = System.Drawing.Color.White;
            this.lblNyuuryokuDate.Location = new System.Drawing.Point(501, 25);
            this.lblNyuuryokuDate.Name = "lblNyuuryokuDate";
            this.lblNyuuryokuDate.Size = new System.Drawing.Size(110, 20);
            this.lblNyuuryokuDate.TabIndex = 10016;
            this.lblNyuuryokuDate.Text = "入力日付";
            this.lblNyuuryokuDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HIDUKE_TO
            // 
            this.HIDUKE_TO.BackColor = System.Drawing.SystemColors.Window;
            this.HIDUKE_TO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HIDUKE_TO.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.HIDUKE_TO.Checked = false;
            this.HIDUKE_TO.CustomFormat = "yyyy/MM/dd(ddd)";
            this.HIDUKE_TO.DateTimeNowYear = "";
            this.HIDUKE_TO.DBFieldsName = "IDOU_DATE";
            this.HIDUKE_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.HIDUKE_TO.DisplayItemName = "移動日付";
            this.HIDUKE_TO.DisplayPopUp = null;
            this.HIDUKE_TO.FocusOutCheckMethod = null;
            this.HIDUKE_TO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HIDUKE_TO.ForeColor = System.Drawing.Color.Black;
            this.HIDUKE_TO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.HIDUKE_TO.IsInputErrorOccured = false;
            this.HIDUKE_TO.ItemDefinedTypes = "datetime";
            this.HIDUKE_TO.Location = new System.Drawing.Point(752, 25);
            this.HIDUKE_TO.MaxLength = 10;
            this.HIDUKE_TO.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.HIDUKE_TO.Name = "HIDUKE_TO";
            this.HIDUKE_TO.NullValue = "";
            this.HIDUKE_TO.PopupAfterExecute = null;
            this.HIDUKE_TO.PopupBeforeExecute = null;
            this.HIDUKE_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIDUKE_TO.PopupSearchSendParams")));
            this.HIDUKE_TO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIDUKE_TO.popupWindowSetting = null;
            this.HIDUKE_TO.RegistCheckMethod = null;
            this.HIDUKE_TO.Size = new System.Drawing.Size(109, 20);
            this.HIDUKE_TO.TabIndex = 5;
            this.HIDUKE_TO.Tag = "";
            this.HIDUKE_TO.Text = "2013/12/03(火)";
            this.HIDUKE_TO.Value = new System.DateTime(2013, 12, 3, 0, 0, 0, 0);
            this.HIDUKE_TO.DoubleClick += new System.EventHandler(this.HIDUKE_TO_DoubleClick);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.Location = new System.Drawing.Point(731, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 20);
            this.label1.TabIndex = 10018;
            this.label1.Text = "～";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1000, 507);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.HIDUKE_TO);
            this.Controls.Add(this.HIDUKE_FROM);
            this.Controls.Add(this.lblNyuuryokuDate);
            this.Controls.Add(this.ZaikoHinmeiPopupButton);
            this.Controls.Add(this.ZAIKO_HINMEI_CD);
            this.Controls.Add(this.ZAIKO_HINMEI_NAME);
            this.Controls.Add(this.GenbaPopupButton);
            this.Controls.Add(this.GENBA_CD);
            this.Controls.Add(this.GENBA_NAME);
            this.Controls.Add(this.ZAIKOHINRMI_LABEL);
            this.Controls.Add(this.GENBA_LABEL);
            this.Controls.Add(this.GyoushaPopupButton);
            this.Controls.Add(this.GYOUSHA_CD);
            this.Controls.Add(this.GYOUSHA_NAME);
            this.Controls.Add(this.GYOUSHA_LABEL);
            this.Name = "UIForm";
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_LABEL, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_NAME, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_CD, 0);
            this.Controls.SetChildIndex(this.GyoushaPopupButton, 0);
            this.Controls.SetChildIndex(this.GENBA_LABEL, 0);
            this.Controls.SetChildIndex(this.ZAIKOHINRMI_LABEL, 0);
            this.Controls.SetChildIndex(this.GENBA_NAME, 0);
            this.Controls.SetChildIndex(this.GENBA_CD, 0);
            this.Controls.SetChildIndex(this.GenbaPopupButton, 0);
            this.Controls.SetChildIndex(this.ZAIKO_HINMEI_NAME, 0);
            this.Controls.SetChildIndex(this.ZAIKO_HINMEI_CD, 0);
            this.Controls.SetChildIndex(this.ZaikoHinmeiPopupButton, 0);
            this.Controls.SetChildIndex(this.lblNyuuryokuDate, 0);
            this.Controls.SetChildIndex(this.HIDUKE_FROM, 0);
            this.Controls.SetChildIndex(this.HIDUKE_TO, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomPopupOpenButton GyoushaPopupButton;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GYOUSHA_CD;
        internal r_framework.CustomControl.CustomTextBox GYOUSHA_NAME;
        internal System.Windows.Forms.Label GYOUSHA_LABEL;
        internal System.Windows.Forms.Label GENBA_LABEL;
        internal System.Windows.Forms.Label ZAIKOHINRMI_LABEL;
        internal r_framework.CustomControl.CustomPopupOpenButton GenbaPopupButton;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GENBA_CD;
        internal r_framework.CustomControl.CustomTextBox GENBA_NAME;
        internal r_framework.CustomControl.CustomPopupOpenButton ZaikoHinmeiPopupButton;
        internal r_framework.CustomControl.CustomAlphaNumTextBox ZAIKO_HINMEI_CD;
        internal r_framework.CustomControl.CustomTextBox ZAIKO_HINMEI_NAME;
        internal r_framework.CustomControl.CustomDateTimePicker HIDUKE_FROM;
        internal System.Windows.Forms.Label lblNyuuryokuDate;
        internal r_framework.CustomControl.CustomDateTimePicker HIDUKE_TO;
        private System.Windows.Forms.Label label1;

    }
}