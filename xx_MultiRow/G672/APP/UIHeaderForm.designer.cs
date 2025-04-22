namespace Shougun.Core.Scale.KeiryouNyuuryoku
{
    partial class UIHeaderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIHeaderForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            this.KYOTEN_NAME_RYAKU = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.KYOTEN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.KYOTEN_LABEL = new System.Windows.Forms.Label();
            this.lb_LastUpdate = new System.Windows.Forms.Label();
            this.LastUpdateDate = new r_framework.CustomControl.CustomTextBox();
            this.LastUpdateUser = new r_framework.CustomControl.CustomTextBox();
            this.lb_Create = new System.Windows.Forms.Label();
            this.CreateDate = new r_framework.CustomControl.CustomTextBox();
            this.CreateUser = new r_framework.CustomControl.CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.KEIZOKU_NYUURYOKU_VALUE = new r_framework.CustomControl.CustomNumericTextBox2();
            this.KEIZOKU_NYUURYOKU_OFF = new r_framework.CustomControl.CustomRadioButton();
            this.KEIZOKU_NYUURYOKU_ON = new r_framework.CustomControl.CustomRadioButton();
            this.KEIZOKU_NYUURYOKU_LABEL = new System.Windows.Forms.Label();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.UKEIRE_LABEL = new System.Windows.Forms.Label();
            this.SHUKKA_LABEL = new System.Windows.Forms.Label();
            this.KIHON_KEIRYOU = new r_framework.CustomControl.CustomNumericTextBox2();
            this.PRINT_KBN_VALUE = new r_framework.CustomControl.CustomNumericTextBox2();
            this.PRINT_KBN_LABLE = new System.Windows.Forms.Label();
            this.customPanel2 = new r_framework.CustomControl.CustomPanel();
            this.PRINT_KBN_OFF = new r_framework.CustomControl.CustomRadioButton();
            this.PRINT_KBN_ON = new r_framework.CustomControl.CustomRadioButton();
            this.customPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.TabIndex = 8000;
            // 
            // lb_title
            // 
            this.lb_title.Size = new System.Drawing.Size(178, 34);
            this.lb_title.TabIndex = 8010;
            this.lb_title.Text = "計量入力";
            // 
            // KYOTEN_NAME_RYAKU
            // 
            this.KYOTEN_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KYOTEN_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_NAME_RYAKU.CharacterLimitList = null;
            this.KYOTEN_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.KYOTEN_NAME_RYAKU.DBFieldsName = "KYOTEN_NAME_RYAKU";
            this.KYOTEN_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_NAME_RYAKU.DisplayPopUp = null;
            this.KYOTEN_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.FocusOutCheckMethod")));
            this.KYOTEN_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 14F);
            this.KYOTEN_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_NAME_RYAKU.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.KYOTEN_NAME_RYAKU.IsInputErrorOccured = false;
            this.KYOTEN_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.KYOTEN_NAME_RYAKU.Location = new System.Drawing.Point(616, 2);
            this.KYOTEN_NAME_RYAKU.MaxLength = 0;
            this.KYOTEN_NAME_RYAKU.Name = "KYOTEN_NAME_RYAKU";
            this.KYOTEN_NAME_RYAKU.PopupAfterExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupBeforeExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.PopupSearchSendParams")));
            this.KYOTEN_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOTEN_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.popupWindowSetting")));
            this.KYOTEN_NAME_RYAKU.ReadOnly = true;
            this.KYOTEN_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.RegistCheckMethod")));
            this.KYOTEN_NAME_RYAKU.Size = new System.Drawing.Size(171, 26);
            this.KYOTEN_NAME_RYAKU.TabIndex = 1050;
            this.KYOTEN_NAME_RYAKU.TabStop = false;
            this.KYOTEN_NAME_RYAKU.Tag = " ";
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
            this.KYOTEN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 14F);
            this.KYOTEN_CD.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_CD.FormatSetting = "カスタム";
            this.KYOTEN_CD.GetCodeMasterField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.IsInputErrorOccured = false;
            this.KYOTEN_CD.ItemDefinedTypes = "smallint";
            this.KYOTEN_CD.Location = new System.Drawing.Point(587, 2);
            this.KYOTEN_CD.Name = "KYOTEN_CD";
            this.KYOTEN_CD.PopupAfterExecute = null;
            this.KYOTEN_CD.PopupBeforeExecute = null;
            this.KYOTEN_CD.PopupGetMasterField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_CD.PopupSearchSendParams")));
            this.KYOTEN_CD.PopupSetFormField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.KYOTEN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.KYOTEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_CD.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.KYOTEN_CD.RangeSetting = rangeSettingDto1;
            this.KYOTEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.RegistCheckMethod")));
            this.KYOTEN_CD.SetFormField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.Size = new System.Drawing.Size(30, 26);
            this.KYOTEN_CD.TabIndex = 8040;
            this.KYOTEN_CD.Tag = "拠点CDを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.KYOTEN_CD.WordWrap = false;
            // 
            // KYOTEN_LABEL
            // 
            this.KYOTEN_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.KYOTEN_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.KYOTEN_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 14F);
            this.KYOTEN_LABEL.ForeColor = System.Drawing.Color.White;
            this.KYOTEN_LABEL.Location = new System.Drawing.Point(510, 2);
            this.KYOTEN_LABEL.Name = "KYOTEN_LABEL";
            this.KYOTEN_LABEL.Size = new System.Drawing.Size(71, 26);
            this.KYOTEN_LABEL.TabIndex = 8030;
            this.KYOTEN_LABEL.Text = "拠点※";
            this.KYOTEN_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_LastUpdate
            // 
            this.lb_LastUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lb_LastUpdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_LastUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lb_LastUpdate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lb_LastUpdate.ForeColor = System.Drawing.Color.White;
            this.lb_LastUpdate.Location = new System.Drawing.Point(769, 52);
            this.lb_LastUpdate.Name = "lb_LastUpdate";
            this.lb_LastUpdate.Size = new System.Drawing.Size(75, 20);
            this.lb_LastUpdate.TabIndex = 8160;
            this.lb_LastUpdate.Text = "最終更新";
            this.lb_LastUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_LastUpdate.Visible = false;
            // 
            // LastUpdateDate
            // 
            this.LastUpdateDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.LastUpdateDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LastUpdateDate.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.LastUpdateDate.DefaultBackColor = System.Drawing.Color.Empty;
            this.LastUpdateDate.DisplayPopUp = null;
            this.LastUpdateDate.ErrorMessage = "";
            this.LastUpdateDate.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LastUpdateDate.FocusOutCheckMethod")));
            this.LastUpdateDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.LastUpdateDate.ForeColor = System.Drawing.Color.Black;
            this.LastUpdateDate.GetCodeMasterField = "";
            this.LastUpdateDate.IsInputErrorOccured = false;
            this.LastUpdateDate.Location = new System.Drawing.Point(1008, 52);
            this.LastUpdateDate.MaxLength = 0;
            this.LastUpdateDate.Name = "LastUpdateDate";
            this.LastUpdateDate.PopupAfterExecute = null;
            this.LastUpdateDate.PopupBeforeExecute = null;
            this.LastUpdateDate.PopupGetMasterField = "";
            this.LastUpdateDate.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("LastUpdateDate.PopupSearchSendParams")));
            this.LastUpdateDate.PopupSetFormField = "";
            this.LastUpdateDate.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.LastUpdateDate.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("LastUpdateDate.popupWindowSetting")));
            this.LastUpdateDate.ReadOnly = true;
            this.LastUpdateDate.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LastUpdateDate.RegistCheckMethod")));
            this.LastUpdateDate.SetFormField = "";
            this.LastUpdateDate.Size = new System.Drawing.Size(160, 20);
            this.LastUpdateDate.TabIndex = 8180;
            this.LastUpdateDate.TabStop = false;
            this.LastUpdateDate.Tag = "最終更新日が表示されます。";
            this.LastUpdateDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.LastUpdateDate.Visible = false;
            // 
            // LastUpdateUser
            // 
            this.LastUpdateUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.LastUpdateUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LastUpdateUser.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.LastUpdateUser.DefaultBackColor = System.Drawing.Color.Empty;
            this.LastUpdateUser.DisplayPopUp = null;
            this.LastUpdateUser.ErrorMessage = "";
            this.LastUpdateUser.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LastUpdateUser.FocusOutCheckMethod")));
            this.LastUpdateUser.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.LastUpdateUser.ForeColor = System.Drawing.Color.Black;
            this.LastUpdateUser.GetCodeMasterField = "";
            this.LastUpdateUser.IsInputErrorOccured = false;
            this.LastUpdateUser.Location = new System.Drawing.Point(849, 52);
            this.LastUpdateUser.MaxLength = 0;
            this.LastUpdateUser.Name = "LastUpdateUser";
            this.LastUpdateUser.PopupAfterExecute = null;
            this.LastUpdateUser.PopupBeforeExecute = null;
            this.LastUpdateUser.PopupGetMasterField = "";
            this.LastUpdateUser.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("LastUpdateUser.PopupSearchSendParams")));
            this.LastUpdateUser.PopupSetFormField = "";
            this.LastUpdateUser.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.LastUpdateUser.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("LastUpdateUser.popupWindowSetting")));
            this.LastUpdateUser.ReadOnly = true;
            this.LastUpdateUser.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LastUpdateUser.RegistCheckMethod")));
            this.LastUpdateUser.SetFormField = "";
            this.LastUpdateUser.Size = new System.Drawing.Size(160, 20);
            this.LastUpdateUser.TabIndex = 8170;
            this.LastUpdateUser.TabStop = false;
            this.LastUpdateUser.Tag = "最終更新者が表示されます。";
            this.LastUpdateUser.Visible = false;
            // 
            // lb_Create
            // 
            this.lb_Create.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lb_Create.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_Create.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lb_Create.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lb_Create.ForeColor = System.Drawing.Color.White;
            this.lb_Create.Location = new System.Drawing.Point(769, 30);
            this.lb_Create.Name = "lb_Create";
            this.lb_Create.Size = new System.Drawing.Size(75, 20);
            this.lb_Create.TabIndex = 8130;
            this.lb_Create.Text = "初回登録";
            this.lb_Create.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_Create.Visible = false;
            // 
            // CreateDate
            // 
            this.CreateDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CreateDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CreateDate.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CreateDate.DefaultBackColor = System.Drawing.Color.Empty;
            this.CreateDate.DisplayPopUp = null;
            this.CreateDate.ErrorMessage = "";
            this.CreateDate.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CreateDate.FocusOutCheckMethod")));
            this.CreateDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CreateDate.ForeColor = System.Drawing.Color.Black;
            this.CreateDate.GetCodeMasterField = "";
            this.CreateDate.IsInputErrorOccured = false;
            this.CreateDate.Location = new System.Drawing.Point(1008, 30);
            this.CreateDate.MaxLength = 0;
            this.CreateDate.Name = "CreateDate";
            this.CreateDate.PopupAfterExecute = null;
            this.CreateDate.PopupBeforeExecute = null;
            this.CreateDate.PopupGetMasterField = "";
            this.CreateDate.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CreateDate.PopupSearchSendParams")));
            this.CreateDate.PopupSetFormField = "";
            this.CreateDate.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CreateDate.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CreateDate.popupWindowSetting")));
            this.CreateDate.ReadOnly = true;
            this.CreateDate.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CreateDate.RegistCheckMethod")));
            this.CreateDate.SetFormField = "";
            this.CreateDate.Size = new System.Drawing.Size(160, 20);
            this.CreateDate.TabIndex = 8150;
            this.CreateDate.TabStop = false;
            this.CreateDate.Tag = "初回登録日が表示されます。";
            this.CreateDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CreateDate.Visible = false;
            // 
            // CreateUser
            // 
            this.CreateUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CreateUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CreateUser.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CreateUser.DefaultBackColor = System.Drawing.Color.Empty;
            this.CreateUser.DisplayPopUp = null;
            this.CreateUser.ErrorMessage = "";
            this.CreateUser.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CreateUser.FocusOutCheckMethod")));
            this.CreateUser.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CreateUser.ForeColor = System.Drawing.Color.Black;
            this.CreateUser.GetCodeMasterField = "";
            this.CreateUser.IsInputErrorOccured = false;
            this.CreateUser.Location = new System.Drawing.Point(849, 30);
            this.CreateUser.MaxLength = 0;
            this.CreateUser.Name = "CreateUser";
            this.CreateUser.PopupAfterExecute = null;
            this.CreateUser.PopupBeforeExecute = null;
            this.CreateUser.PopupGetMasterField = "";
            this.CreateUser.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CreateUser.PopupSearchSendParams")));
            this.CreateUser.PopupSetFormField = "";
            this.CreateUser.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CreateUser.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CreateUser.popupWindowSetting")));
            this.CreateUser.ReadOnly = true;
            this.CreateUser.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CreateUser.RegistCheckMethod")));
            this.CreateUser.SetFormField = "";
            this.CreateUser.Size = new System.Drawing.Size(160, 20);
            this.CreateUser.TabIndex = 8140;
            this.CreateUser.TabStop = false;
            this.CreateUser.Tag = "初回登録者が表示されます。";
            this.CreateUser.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 40F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Aqua;
            this.label1.Location = new System.Drawing.Point(270, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 62);
            this.label1.TabIndex = 8020;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // KEIZOKU_NYUURYOKU_VALUE
            // 
            this.KEIZOKU_NYUURYOKU_VALUE.BackColor = System.Drawing.SystemColors.Window;
            this.KEIZOKU_NYUURYOKU_VALUE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KEIZOKU_NYUURYOKU_VALUE.DefaultBackColor = System.Drawing.Color.Empty;
            this.KEIZOKU_NYUURYOKU_VALUE.DisplayItemName = "継続入力";
            this.KEIZOKU_NYUURYOKU_VALUE.DisplayPopUp = null;
            this.KEIZOKU_NYUURYOKU_VALUE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIZOKU_NYUURYOKU_VALUE.FocusOutCheckMethod")));
            this.KEIZOKU_NYUURYOKU_VALUE.Font = new System.Drawing.Font("ＭＳ ゴシック", 14F);
            this.KEIZOKU_NYUURYOKU_VALUE.ForeColor = System.Drawing.Color.Black;
            this.KEIZOKU_NYUURYOKU_VALUE.IsInputErrorOccured = false;
            this.KEIZOKU_NYUURYOKU_VALUE.LinkedRadioButtonArray = new string[] {
        "KEIZOKU_NYUURYOKU_ON",
        "KEIZOKU_NYUURYOKU_OFF"};
            this.KEIZOKU_NYUURYOKU_VALUE.Location = new System.Drawing.Point(904, 3);
            this.KEIZOKU_NYUURYOKU_VALUE.Name = "KEIZOKU_NYUURYOKU_VALUE";
            this.KEIZOKU_NYUURYOKU_VALUE.PopupAfterExecute = null;
            this.KEIZOKU_NYUURYOKU_VALUE.PopupBeforeExecute = null;
            this.KEIZOKU_NYUURYOKU_VALUE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KEIZOKU_NYUURYOKU_VALUE.PopupSearchSendParams")));
            this.KEIZOKU_NYUURYOKU_VALUE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KEIZOKU_NYUURYOKU_VALUE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KEIZOKU_NYUURYOKU_VALUE.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto2.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.KEIZOKU_NYUURYOKU_VALUE.RangeSetting = rangeSettingDto2;
            this.KEIZOKU_NYUURYOKU_VALUE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIZOKU_NYUURYOKU_VALUE.RegistCheckMethod")));
            this.KEIZOKU_NYUURYOKU_VALUE.Size = new System.Drawing.Size(18, 26);
            this.KEIZOKU_NYUURYOKU_VALUE.TabIndex = 8080;
            this.KEIZOKU_NYUURYOKU_VALUE.TabStop = false;
            this.KEIZOKU_NYUURYOKU_VALUE.Tag = "1(する)を選択した場合、登録後に新規モードで再描画します。2(しない)を選択した場合、登録後に画面を閉じます。";
            this.KEIZOKU_NYUURYOKU_VALUE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.KEIZOKU_NYUURYOKU_VALUE.WordWrap = false;
            // 
            // KEIZOKU_NYUURYOKU_OFF
            // 
            this.KEIZOKU_NYUURYOKU_OFF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.KEIZOKU_NYUURYOKU_OFF.AutoSize = true;
            this.KEIZOKU_NYUURYOKU_OFF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.KEIZOKU_NYUURYOKU_OFF.DefaultBackColor = System.Drawing.Color.Empty;
            this.KEIZOKU_NYUURYOKU_OFF.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIZOKU_NYUURYOKU_OFF.FocusOutCheckMethod")));
            this.KEIZOKU_NYUURYOKU_OFF.Font = new System.Drawing.Font("ＭＳ ゴシック", 12F);
            this.KEIZOKU_NYUURYOKU_OFF.LinkedTextBox = "KEIZOKU_NYUURYOKU_VALUE";
            this.KEIZOKU_NYUURYOKU_OFF.Location = new System.Drawing.Point(1009, 6);
            this.KEIZOKU_NYUURYOKU_OFF.Name = "KEIZOKU_NYUURYOKU_OFF";
            this.KEIZOKU_NYUURYOKU_OFF.PopupAfterExecute = null;
            this.KEIZOKU_NYUURYOKU_OFF.PopupBeforeExecute = null;
            this.KEIZOKU_NYUURYOKU_OFF.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KEIZOKU_NYUURYOKU_OFF.PopupSearchSendParams")));
            this.KEIZOKU_NYUURYOKU_OFF.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KEIZOKU_NYUURYOKU_OFF.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KEIZOKU_NYUURYOKU_OFF.popupWindowSetting")));
            this.KEIZOKU_NYUURYOKU_OFF.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIZOKU_NYUURYOKU_OFF.RegistCheckMethod")));
            this.KEIZOKU_NYUURYOKU_OFF.Size = new System.Drawing.Size(90, 20);
            this.KEIZOKU_NYUURYOKU_OFF.TabIndex = 20;
            this.KEIZOKU_NYUURYOKU_OFF.Text = "2.しない";
            this.KEIZOKU_NYUURYOKU_OFF.UseVisualStyleBackColor = false;
            this.KEIZOKU_NYUURYOKU_OFF.Value = "2";
            // 
            // KEIZOKU_NYUURYOKU_ON
            // 
            this.KEIZOKU_NYUURYOKU_ON.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.KEIZOKU_NYUURYOKU_ON.AutoSize = true;
            this.KEIZOKU_NYUURYOKU_ON.DefaultBackColor = System.Drawing.Color.Empty;
            this.KEIZOKU_NYUURYOKU_ON.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIZOKU_NYUURYOKU_ON.FocusOutCheckMethod")));
            this.KEIZOKU_NYUURYOKU_ON.Font = new System.Drawing.Font("ＭＳ ゴシック", 12F);
            this.KEIZOKU_NYUURYOKU_ON.LinkedTextBox = "KEIZOKU_NYUURYOKU_VALUE";
            this.KEIZOKU_NYUURYOKU_ON.Location = new System.Drawing.Point(929, 6);
            this.KEIZOKU_NYUURYOKU_ON.Name = "KEIZOKU_NYUURYOKU_ON";
            this.KEIZOKU_NYUURYOKU_ON.PopupAfterExecute = null;
            this.KEIZOKU_NYUURYOKU_ON.PopupBeforeExecute = null;
            this.KEIZOKU_NYUURYOKU_ON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KEIZOKU_NYUURYOKU_ON.PopupSearchSendParams")));
            this.KEIZOKU_NYUURYOKU_ON.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KEIZOKU_NYUURYOKU_ON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KEIZOKU_NYUURYOKU_ON.popupWindowSetting")));
            this.KEIZOKU_NYUURYOKU_ON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIZOKU_NYUURYOKU_ON.RegistCheckMethod")));
            this.KEIZOKU_NYUURYOKU_ON.Size = new System.Drawing.Size(74, 20);
            this.KEIZOKU_NYUURYOKU_ON.TabIndex = 10;
            this.KEIZOKU_NYUURYOKU_ON.Text = "1.する";
            this.KEIZOKU_NYUURYOKU_ON.UseVisualStyleBackColor = true;
            this.KEIZOKU_NYUURYOKU_ON.Value = "1";
            // 
            // KEIZOKU_NYUURYOKU_LABEL
            // 
            this.KEIZOKU_NYUURYOKU_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.KEIZOKU_NYUURYOKU_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KEIZOKU_NYUURYOKU_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.KEIZOKU_NYUURYOKU_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 14F);
            this.KEIZOKU_NYUURYOKU_LABEL.ForeColor = System.Drawing.Color.White;
            this.KEIZOKU_NYUURYOKU_LABEL.Location = new System.Drawing.Point(800, 3);
            this.KEIZOKU_NYUURYOKU_LABEL.Name = "KEIZOKU_NYUURYOKU_LABEL";
            this.KEIZOKU_NYUURYOKU_LABEL.Size = new System.Drawing.Size(98, 25);
            this.KEIZOKU_NYUURYOKU_LABEL.TabIndex = 8070;
            this.KEIZOKU_NYUURYOKU_LABEL.Text = "継続入力";
            this.KEIZOKU_NYUURYOKU_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel1
            // 
            this.customPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel1.Location = new System.Drawing.Point(921, 3);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(191, 26);
            this.customPanel1.TabIndex = 8090;
            // 
            // UKEIRE_LABEL
            // 
            this.UKEIRE_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.UKEIRE_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UKEIRE_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UKEIRE_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 20.25F);
            this.UKEIRE_LABEL.ForeColor = System.Drawing.Color.Cyan;
            this.UKEIRE_LABEL.Location = new System.Drawing.Point(510, 33);
            this.UKEIRE_LABEL.Name = "UKEIRE_LABEL";
            this.UKEIRE_LABEL.Size = new System.Drawing.Size(100, 34);
            this.UKEIRE_LABEL.TabIndex = 8100;
            this.UKEIRE_LABEL.Text = "受入";
            this.UKEIRE_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.UKEIRE_LABEL.Click += new System.EventHandler(this.UKEIRE_LABEL_Click);
            // 
            // SHUKKA_LABEL
            // 
            this.SHUKKA_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.SHUKKA_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHUKKA_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SHUKKA_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 20.25F);
            this.SHUKKA_LABEL.ForeColor = System.Drawing.Color.Silver;
            this.SHUKKA_LABEL.Location = new System.Drawing.Point(616, 33);
            this.SHUKKA_LABEL.Name = "SHUKKA_LABEL";
            this.SHUKKA_LABEL.Size = new System.Drawing.Size(100, 34);
            this.SHUKKA_LABEL.TabIndex = 8110;
            this.SHUKKA_LABEL.Text = "出荷";
            this.SHUKKA_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SHUKKA_LABEL.Click += new System.EventHandler(this.SHUKKA_LABEL_Click);
            // 
            // KIHON_KEIRYOU
            // 
            this.KIHON_KEIRYOU.BackColor = System.Drawing.SystemColors.Window;
            this.KIHON_KEIRYOU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KIHON_KEIRYOU.DBFieldsName = "KIHON_KEIRYOU";
            this.KIHON_KEIRYOU.DefaultBackColor = System.Drawing.Color.Empty;
            this.KIHON_KEIRYOU.DisplayItemName = "基本計量";
            this.KIHON_KEIRYOU.DisplayPopUp = null;
            this.KIHON_KEIRYOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KIHON_KEIRYOU.FocusOutCheckMethod")));
            this.KIHON_KEIRYOU.Font = new System.Drawing.Font("ＭＳ ゴシック", 14F);
            this.KIHON_KEIRYOU.ForeColor = System.Drawing.Color.Black;
            this.KIHON_KEIRYOU.IsInputErrorOccured = false;
            this.KIHON_KEIRYOU.Location = new System.Drawing.Point(722, 34);
            this.KIHON_KEIRYOU.Name = "KIHON_KEIRYOU";
            this.KIHON_KEIRYOU.PopupAfterExecute = null;
            this.KIHON_KEIRYOU.PopupBeforeExecute = null;
            this.KIHON_KEIRYOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KIHON_KEIRYOU.PopupSearchSendParams")));
            this.KIHON_KEIRYOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KIHON_KEIRYOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KIHON_KEIRYOU.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto3.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.KIHON_KEIRYOU.RangeSetting = rangeSettingDto3;
            this.KIHON_KEIRYOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KIHON_KEIRYOU.RegistCheckMethod")));
            this.KIHON_KEIRYOU.Size = new System.Drawing.Size(18, 26);
            this.KIHON_KEIRYOU.TabIndex = 8111;
            this.KIHON_KEIRYOU.TabStop = false;
            this.KIHON_KEIRYOU.Tag = "";
            this.KIHON_KEIRYOU.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.KIHON_KEIRYOU.Visible = false;
            this.KIHON_KEIRYOU.WordWrap = false;
            // 
            // PRINT_KBN_VALUE
            // 
            this.PRINT_KBN_VALUE.BackColor = System.Drawing.SystemColors.Window;
            this.PRINT_KBN_VALUE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PRINT_KBN_VALUE.DefaultBackColor = System.Drawing.Color.Empty;
            this.PRINT_KBN_VALUE.DisplayItemName = "計量票出力";
            this.PRINT_KBN_VALUE.DisplayPopUp = null;
            this.PRINT_KBN_VALUE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PRINT_KBN_VALUE.FocusOutCheckMethod")));
            this.PRINT_KBN_VALUE.Font = new System.Drawing.Font("ＭＳ ゴシック", 14F);
            this.PRINT_KBN_VALUE.ForeColor = System.Drawing.Color.Black;
            this.PRINT_KBN_VALUE.IsInputErrorOccured = false;
            this.PRINT_KBN_VALUE.LinkedRadioButtonArray = new string[] {
        "PRINT_KBN_ON",
        "PRINT_KBN_OFF"};
            this.PRINT_KBN_VALUE.Location = new System.Drawing.Point(904, 37);
            this.PRINT_KBN_VALUE.Name = "PRINT_KBN_VALUE";
            this.PRINT_KBN_VALUE.PopupAfterExecute = null;
            this.PRINT_KBN_VALUE.PopupBeforeExecute = null;
            this.PRINT_KBN_VALUE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PRINT_KBN_VALUE.PopupSearchSendParams")));
            this.PRINT_KBN_VALUE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PRINT_KBN_VALUE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("PRINT_KBN_VALUE.popupWindowSetting")));
            rangeSettingDto4.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto4.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.PRINT_KBN_VALUE.RangeSetting = rangeSettingDto4;
            this.PRINT_KBN_VALUE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PRINT_KBN_VALUE.RegistCheckMethod")));
            this.PRINT_KBN_VALUE.Size = new System.Drawing.Size(18, 26);
            this.PRINT_KBN_VALUE.TabIndex = 8182;
            this.PRINT_KBN_VALUE.TabStop = false;
            this.PRINT_KBN_VALUE.Tag = "1(する)を選択した場合、登録時に計量票を出力する。2(しない)を選択した場合、登録時に計量票を出力しない。";
            this.PRINT_KBN_VALUE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PRINT_KBN_VALUE.WordWrap = false;
            // 
            // PRINT_KBN_LABLE
            // 
            this.PRINT_KBN_LABLE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.PRINT_KBN_LABLE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PRINT_KBN_LABLE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PRINT_KBN_LABLE.Font = new System.Drawing.Font("ＭＳ ゴシック", 12F);
            this.PRINT_KBN_LABLE.ForeColor = System.Drawing.Color.White;
            this.PRINT_KBN_LABLE.Location = new System.Drawing.Point(800, 37);
            this.PRINT_KBN_LABLE.Name = "PRINT_KBN_LABLE";
            this.PRINT_KBN_LABLE.Size = new System.Drawing.Size(98, 25);
            this.PRINT_KBN_LABLE.TabIndex = 8181;
            this.PRINT_KBN_LABLE.Text = "計量票出力";
            this.PRINT_KBN_LABLE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel2
            // 
            this.customPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel2.Controls.Add(this.PRINT_KBN_OFF);
            this.customPanel2.Controls.Add(this.PRINT_KBN_ON);
            this.customPanel2.Location = new System.Drawing.Point(921, 37);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(191, 26);
            this.customPanel2.TabIndex = 8183;
            // 
            // PRINT_KBN_OFF
            // 
            this.PRINT_KBN_OFF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PRINT_KBN_OFF.AutoSize = true;
            this.PRINT_KBN_OFF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.PRINT_KBN_OFF.DefaultBackColor = System.Drawing.Color.Empty;
            this.PRINT_KBN_OFF.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PRINT_KBN_OFF.FocusOutCheckMethod")));
            this.PRINT_KBN_OFF.Font = new System.Drawing.Font("ＭＳ ゴシック", 12F);
            this.PRINT_KBN_OFF.LinkedTextBox = "PRINT_KBN_VALUE";
            this.PRINT_KBN_OFF.Location = new System.Drawing.Point(89, 2);
            this.PRINT_KBN_OFF.Name = "PRINT_KBN_OFF";
            this.PRINT_KBN_OFF.PopupAfterExecute = null;
            this.PRINT_KBN_OFF.PopupBeforeExecute = null;
            this.PRINT_KBN_OFF.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PRINT_KBN_OFF.PopupSearchSendParams")));
            this.PRINT_KBN_OFF.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PRINT_KBN_OFF.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("PRINT_KBN_OFF.popupWindowSetting")));
            this.PRINT_KBN_OFF.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PRINT_KBN_OFF.RegistCheckMethod")));
            this.PRINT_KBN_OFF.Size = new System.Drawing.Size(90, 20);
            this.PRINT_KBN_OFF.TabIndex = 22;
            this.PRINT_KBN_OFF.Text = "2.しない";
            this.PRINT_KBN_OFF.UseVisualStyleBackColor = false;
            this.PRINT_KBN_OFF.Value = "2";
            // 
            // PRINT_KBN_ON
            // 
            this.PRINT_KBN_ON.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PRINT_KBN_ON.AutoSize = true;
            this.PRINT_KBN_ON.DefaultBackColor = System.Drawing.Color.Empty;
            this.PRINT_KBN_ON.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PRINT_KBN_ON.FocusOutCheckMethod")));
            this.PRINT_KBN_ON.Font = new System.Drawing.Font("ＭＳ ゴシック", 12F);
            this.PRINT_KBN_ON.LinkedTextBox = "PRINT_KBN_VALUE";
            this.PRINT_KBN_ON.Location = new System.Drawing.Point(9, 2);
            this.PRINT_KBN_ON.Name = "PRINT_KBN_ON";
            this.PRINT_KBN_ON.PopupAfterExecute = null;
            this.PRINT_KBN_ON.PopupBeforeExecute = null;
            this.PRINT_KBN_ON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PRINT_KBN_ON.PopupSearchSendParams")));
            this.PRINT_KBN_ON.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PRINT_KBN_ON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("PRINT_KBN_ON.popupWindowSetting")));
            this.PRINT_KBN_ON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PRINT_KBN_ON.RegistCheckMethod")));
            this.PRINT_KBN_ON.Size = new System.Drawing.Size(74, 20);
            this.PRINT_KBN_ON.TabIndex = 21;
            this.PRINT_KBN_ON.Text = "1.する";
            this.PRINT_KBN_ON.UseVisualStyleBackColor = true;
            this.PRINT_KBN_ON.Value = "1";
            // 
            // UIHeaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1180, 73);
            this.Controls.Add(this.PRINT_KBN_VALUE);
            this.Controls.Add(this.PRINT_KBN_LABLE);
            this.Controls.Add(this.customPanel2);
            this.Controls.Add(this.KIHON_KEIRYOU);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SHUKKA_LABEL);
            this.Controls.Add(this.UKEIRE_LABEL);
            this.Controls.Add(this.KEIZOKU_NYUURYOKU_VALUE);
            this.Controls.Add(this.KEIZOKU_NYUURYOKU_OFF);
            this.Controls.Add(this.KEIZOKU_NYUURYOKU_ON);
            this.Controls.Add(this.KEIZOKU_NYUURYOKU_LABEL);
            this.Controls.Add(this.KYOTEN_NAME_RYAKU);
            this.Controls.Add(this.KYOTEN_CD);
            this.Controls.Add(this.KYOTEN_LABEL);
            this.Controls.Add(this.lb_LastUpdate);
            this.Controls.Add(this.LastUpdateDate);
            this.Controls.Add(this.LastUpdateUser);
            this.Controls.Add(this.lb_Create);
            this.Controls.Add(this.CreateDate);
            this.Controls.Add(this.CreateUser);
            this.Controls.Add(this.customPanel1);
            this.Name = "UIHeaderForm";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            this.Controls.SetChildIndex(this.customPanel1, 0);
            this.Controls.SetChildIndex(this.CreateUser, 0);
            this.Controls.SetChildIndex(this.CreateDate, 0);
            this.Controls.SetChildIndex(this.lb_Create, 0);
            this.Controls.SetChildIndex(this.LastUpdateUser, 0);
            this.Controls.SetChildIndex(this.LastUpdateDate, 0);
            this.Controls.SetChildIndex(this.lb_LastUpdate, 0);
            this.Controls.SetChildIndex(this.KYOTEN_LABEL, 0);
            this.Controls.SetChildIndex(this.KYOTEN_CD, 0);
            this.Controls.SetChildIndex(this.KYOTEN_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.KEIZOKU_NYUURYOKU_LABEL, 0);
            this.Controls.SetChildIndex(this.KEIZOKU_NYUURYOKU_ON, 0);
            this.Controls.SetChildIndex(this.KEIZOKU_NYUURYOKU_OFF, 0);
            this.Controls.SetChildIndex(this.KEIZOKU_NYUURYOKU_VALUE, 0);
            this.Controls.SetChildIndex(this.UKEIRE_LABEL, 0);
            this.Controls.SetChildIndex(this.SHUKKA_LABEL, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.KIHON_KEIRYOU, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.customPanel2, 0);
            this.Controls.SetChildIndex(this.PRINT_KBN_LABLE, 0);
            this.Controls.SetChildIndex(this.PRINT_KBN_VALUE, 0);
            this.customPanel2.ResumeLayout(false);
            this.customPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomAlphaNumTextBox KYOTEN_NAME_RYAKU;
        public r_framework.CustomControl.CustomNumericTextBox2 KYOTEN_CD;
        public System.Windows.Forms.Label KYOTEN_LABEL;
        public System.Windows.Forms.Label lb_LastUpdate;
        public r_framework.CustomControl.CustomTextBox LastUpdateDate;
        public r_framework.CustomControl.CustomTextBox LastUpdateUser;
        public System.Windows.Forms.Label lb_Create;
        public r_framework.CustomControl.CustomTextBox CreateDate;
        public r_framework.CustomControl.CustomTextBox CreateUser;
        public System.Windows.Forms.Label label1;
        internal r_framework.CustomControl.CustomNumericTextBox2 KEIZOKU_NYUURYOKU_VALUE;
        private r_framework.CustomControl.CustomRadioButton KEIZOKU_NYUURYOKU_OFF;
        private r_framework.CustomControl.CustomRadioButton KEIZOKU_NYUURYOKU_ON;
        internal System.Windows.Forms.Label KEIZOKU_NYUURYOKU_LABEL;
        private r_framework.CustomControl.CustomPanel customPanel1;
        public System.Windows.Forms.Label UKEIRE_LABEL;
        public System.Windows.Forms.Label SHUKKA_LABEL;
        internal r_framework.CustomControl.CustomNumericTextBox2 KIHON_KEIRYOU;
        internal r_framework.CustomControl.CustomNumericTextBox2 PRINT_KBN_VALUE;
        internal System.Windows.Forms.Label PRINT_KBN_LABLE;
        private r_framework.CustomControl.CustomPanel customPanel2;
        private r_framework.CustomControl.CustomRadioButton PRINT_KBN_OFF;
        private r_framework.CustomControl.CustomRadioButton PRINT_KBN_ON;
    }
}
