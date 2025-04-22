namespace Shougun.Core.SalesPayment.TukigimeUriageDenpyoSakusei
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.ShortcutKeyManager shortcutKeyManager1 = new GrapeCity.Win.MultiRow.ShortcutKeyManager();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.btnKyotenSearch = new r_framework.CustomControl.CustomPopupOpenButton();
            this.btnGyosyaSearch = new r_framework.CustomControl.CustomPopupOpenButton();
            this.btnGenbaSearch = new r_framework.CustomControl.CustomPopupOpenButton();
            this.btnTrihikisakiSarchButton = new r_framework.CustomControl.CustomPopupOpenButton();
            this.txtGenbaCd = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.txtGyosyaCd = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.dtpSeikyuDate = new r_framework.CustomControl.CustomDateTimePicker();
            this.lbl_SeikyuDate = new System.Windows.Forms.Label();
            this.txtGenbaName = new r_framework.CustomControl.CustomTextBox();
            this.lbl_Genba = new System.Windows.Forms.Label();
            this.txtGyosyaName = new r_framework.CustomControl.CustomTextBox();
            this.lbl_Gyosha = new System.Windows.Forms.Label();
            this.txtTorihikisakiName = new r_framework.CustomControl.CustomTextBox();
            this.txtTorihikisakiCd = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.lbl_Torihikisaki = new System.Windows.Forms.Label();
            this.dtpTaishoKikanTo = new r_framework.CustomControl.CustomDateTimePicker();
            this.label38 = new System.Windows.Forms.Label();
            this.lbl_TaishoKikan = new System.Windows.Forms.Label();
            this.dtpTaishoKikanFrom = new r_framework.CustomControl.CustomDateTimePicker();
            this.cmbShimebi = new r_framework.CustomControl.CustomComboBox();
            this.lbl_shimebi = new System.Windows.Forms.Label();
            this.txtKyotenName = new r_framework.CustomControl.CustomTextBox();
            this.lbl_Kyoten = new System.Windows.Forms.Label();
            this.chkSakusei = new r_framework.CustomControl.CustomCheckBox();
            this.txtKyotenCd = new r_framework.CustomControl.CustomNumericTextBox2();
            this.mrwTukigimeUriageDenpyo = new r_framework.CustomControl.GcCustomMultiRow(this.components);
            this.mrwMainForm1 = new Shougun.Core.SalesPayment.TukigimeUriageDenpyoSakusei.mrwMainForm();
            ((System.ComponentModel.ISupportInitialize)(this.mrwTukigimeUriageDenpyo)).BeginInit();
            this.SuspendLayout();
            // 
            // ISNOT_NEED_DELETE_FLG
            // 
            this.ISNOT_NEED_DELETE_FLG.BackColor = System.Drawing.SystemColors.Window;
            this.ISNOT_NEED_DELETE_FLG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ISNOT_NEED_DELETE_FLG.DBFieldsName = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.ISNOT_NEED_DELETE_FLG.DisplayPopUp = null;
            this.ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod = null;
            this.ISNOT_NEED_DELETE_FLG.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ISNOT_NEED_DELETE_FLG.ForeColor = System.Drawing.Color.Black;
            this.ISNOT_NEED_DELETE_FLG.IsInputErrorOccured = false;
            this.ISNOT_NEED_DELETE_FLG.ItemDefinedTypes = "bit";
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(800, 75);
            this.ISNOT_NEED_DELETE_FLG.Name = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.PopupAfterExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupBeforeExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.PopupSearchSendParams")));
            this.ISNOT_NEED_DELETE_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ISNOT_NEED_DELETE_FLG.popupWindowSetting = null;
            this.ISNOT_NEED_DELETE_FLG.RegistCheckMethod = null;
            this.ISNOT_NEED_DELETE_FLG.Size = new System.Drawing.Size(20, 20);
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 10008;
            this.ISNOT_NEED_DELETE_FLG.TabStop = false;
            this.ISNOT_NEED_DELETE_FLG.Text = "FALSE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // btnKyotenSearch
            // 
            this.btnKyotenSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnKyotenSearch.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.btnKyotenSearch.DBFieldsName = null;
            this.btnKyotenSearch.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnKyotenSearch.DisplayItemName = null;
            this.btnKyotenSearch.DisplayPopUp = null;
            this.btnKyotenSearch.ErrorMessage = null;
            this.btnKyotenSearch.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnKyotenSearch.FocusOutCheckMethod")));
            this.btnKyotenSearch.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.btnKyotenSearch.GetCodeMasterField = null;
            this.btnKyotenSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnKyotenSearch.Image")));
            this.btnKyotenSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnKyotenSearch.ItemDefinedTypes = null;
            this.btnKyotenSearch.LinkedSettingTextBox = null;
            this.btnKyotenSearch.LinkedTextBoxs = null;
            this.btnKyotenSearch.Location = new System.Drawing.Point(434, 9);
            this.btnKyotenSearch.Name = "btnKyotenSearch";
            this.btnKyotenSearch.PopupAfterExecute = null;
            this.btnKyotenSearch.PopupBeforeExecute = null;
            this.btnKyotenSearch.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME";
            this.btnKyotenSearch.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("btnKyotenSearch.PopupSearchSendParams")));
            this.btnKyotenSearch.PopupSetFormField = "txtKyotenCd,txtKyotenName";
            this.btnKyotenSearch.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.btnKyotenSearch.PopupWindowName = "マスタ共通ポップアップ";
            this.btnKyotenSearch.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("btnKyotenSearch.popupWindowSetting")));
            this.btnKyotenSearch.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnKyotenSearch.RegistCheckMethod")));
            this.btnKyotenSearch.SearchDisplayFlag = 0;
            this.btnKyotenSearch.SetFormField = "txtKyotenCd,txtKyotenName";
            this.btnKyotenSearch.ShortItemName = null;
            this.btnKyotenSearch.Size = new System.Drawing.Size(22, 22);
            this.btnKyotenSearch.TabIndex = 3;
            this.btnKyotenSearch.TabStop = false;
            this.btnKyotenSearch.Tag = "検索画面を表示します";
            this.btnKyotenSearch.UseVisualStyleBackColor = false;
            this.btnKyotenSearch.ZeroPaddengFlag = false;
            // 
            // btnGyosyaSearch
            // 
            this.btnGyosyaSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnGyosyaSearch.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.btnGyosyaSearch.DBFieldsName = null;
            this.btnGyosyaSearch.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnGyosyaSearch.DisplayItemName = null;
            this.btnGyosyaSearch.DisplayPopUp = null;
            this.btnGyosyaSearch.ErrorMessage = null;
            this.btnGyosyaSearch.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnGyosyaSearch.FocusOutCheckMethod")));
            this.btnGyosyaSearch.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.btnGyosyaSearch.GetCodeMasterField = null;
            this.btnGyosyaSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnGyosyaSearch.Image")));
            this.btnGyosyaSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGyosyaSearch.ItemDefinedTypes = null;
            this.btnGyosyaSearch.LinkedSettingTextBox = null;
            this.btnGyosyaSearch.LinkedTextBoxs = null;
            this.btnGyosyaSearch.Location = new System.Drawing.Point(919, 53);
            this.btnGyosyaSearch.Name = "btnGyosyaSearch";
            this.btnGyosyaSearch.PopupAfterExecute = null;
            this.btnGyosyaSearch.PopupBeforeExecute = null;
            this.btnGyosyaSearch.PopupGetMasterField = "GENBA_CD, GENBA_NAME_RYAKU,GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.btnGyosyaSearch.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("btnGyosyaSearch.PopupSearchSendParams")));
            this.btnGyosyaSearch.PopupSetFormField = "txtGenbaCd,txtGenbaName,txtGyosyaCd,txtGyosyaName";
            this.btnGyosyaSearch.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.btnGyosyaSearch.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.btnGyosyaSearch.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("btnGyosyaSearch.popupWindowSetting")));
            this.btnGyosyaSearch.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnGyosyaSearch.RegistCheckMethod")));
            this.btnGyosyaSearch.SearchDisplayFlag = 0;
            this.btnGyosyaSearch.SetFormField = "txtGenbaCd,txtGenbaName";
            this.btnGyosyaSearch.ShortItemName = null;
            this.btnGyosyaSearch.Size = new System.Drawing.Size(22, 22);
            this.btnGyosyaSearch.TabIndex = 15;
            this.btnGyosyaSearch.TabStop = false;
            this.btnGyosyaSearch.Tag = "検索画面を表示します";
            this.btnGyosyaSearch.UseVisualStyleBackColor = false;
            this.btnGyosyaSearch.ZeroPaddengFlag = false;
            // 
            // btnGenbaSearch
            // 
            this.btnGenbaSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnGenbaSearch.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.btnGenbaSearch.DBFieldsName = null;
            this.btnGenbaSearch.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnGenbaSearch.DisplayItemName = null;
            this.btnGenbaSearch.DisplayPopUp = null;
            this.btnGenbaSearch.ErrorMessage = null;
            this.btnGenbaSearch.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnGenbaSearch.FocusOutCheckMethod")));
            this.btnGenbaSearch.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.btnGenbaSearch.GetCodeMasterField = null;
            this.btnGenbaSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnGenbaSearch.Image")));
            this.btnGenbaSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnGenbaSearch.ItemDefinedTypes = null;
            this.btnGenbaSearch.LinkedSettingTextBox = null;
            this.btnGenbaSearch.LinkedTextBoxs = null;
            this.btnGenbaSearch.Location = new System.Drawing.Point(919, 31);
            this.btnGenbaSearch.Name = "btnGenbaSearch";
            this.btnGenbaSearch.PopupAfterExecute = null;
            this.btnGenbaSearch.PopupAfterExecuteMethod = "AfterPopupGyousha";
            this.btnGenbaSearch.PopupBeforeExecute = null;
            this.btnGenbaSearch.PopupBeforeExecuteMethod = "BeforePopupGyousha";
            this.btnGenbaSearch.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.btnGenbaSearch.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("btnGenbaSearch.PopupSearchSendParams")));
            this.btnGenbaSearch.PopupSetFormField = "txtGyosyaCd,txtGyosyaName";
            this.btnGenbaSearch.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.btnGenbaSearch.PopupWindowName = "検索共通ポップアップ";
            this.btnGenbaSearch.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("btnGenbaSearch.popupWindowSetting")));
            this.btnGenbaSearch.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnGenbaSearch.RegistCheckMethod")));
            this.btnGenbaSearch.SearchDisplayFlag = 0;
            this.btnGenbaSearch.SetFormField = "txtGyosyaCd,txtGyosyaName";
            this.btnGenbaSearch.ShortItemName = null;
            this.btnGenbaSearch.Size = new System.Drawing.Size(22, 22);
            this.btnGenbaSearch.TabIndex = 12;
            this.btnGenbaSearch.TabStop = false;
            this.btnGenbaSearch.Tag = "検索画面を表示します";
            this.btnGenbaSearch.UseVisualStyleBackColor = false;
            this.btnGenbaSearch.ZeroPaddengFlag = false;
            // 
            // btnTrihikisakiSarchButton
            // 
            this.btnTrihikisakiSarchButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnTrihikisakiSarchButton.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.btnTrihikisakiSarchButton.DBFieldsName = null;
            this.btnTrihikisakiSarchButton.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnTrihikisakiSarchButton.DisplayItemName = null;
            this.btnTrihikisakiSarchButton.DisplayPopUp = null;
            this.btnTrihikisakiSarchButton.ErrorMessage = null;
            this.btnTrihikisakiSarchButton.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnTrihikisakiSarchButton.FocusOutCheckMethod")));
            this.btnTrihikisakiSarchButton.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.btnTrihikisakiSarchButton.GetCodeMasterField = null;
            this.btnTrihikisakiSarchButton.Image = ((System.Drawing.Image)(resources.GetObject("btnTrihikisakiSarchButton.Image")));
            this.btnTrihikisakiSarchButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnTrihikisakiSarchButton.ItemDefinedTypes = null;
            this.btnTrihikisakiSarchButton.LinkedSettingTextBox = null;
            this.btnTrihikisakiSarchButton.LinkedTextBoxs = null;
            this.btnTrihikisakiSarchButton.Location = new System.Drawing.Point(919, 9);
            this.btnTrihikisakiSarchButton.Name = "btnTrihikisakiSarchButton";
            this.btnTrihikisakiSarchButton.PopupAfterExecute = null;
            this.btnTrihikisakiSarchButton.PopupBeforeExecute = null;
            this.btnTrihikisakiSarchButton.PopupGetMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.btnTrihikisakiSarchButton.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("btnTrihikisakiSarchButton.PopupSearchSendParams")));
            this.btnTrihikisakiSarchButton.PopupSetFormField = "txtTorihikisakiCd,txtTorihikisakiName";
            this.btnTrihikisakiSarchButton.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.btnTrihikisakiSarchButton.PopupWindowName = "検索共通ポップアップ";
            this.btnTrihikisakiSarchButton.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("btnTrihikisakiSarchButton.popupWindowSetting")));
            this.btnTrihikisakiSarchButton.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnTrihikisakiSarchButton.RegistCheckMethod")));
            this.btnTrihikisakiSarchButton.SearchDisplayFlag = 0;
            this.btnTrihikisakiSarchButton.SetFormField = "txtTorihikisakiCd,txtTorihikisakiName";
            this.btnTrihikisakiSarchButton.ShortItemName = null;
            this.btnTrihikisakiSarchButton.Size = new System.Drawing.Size(22, 22);
            this.btnTrihikisakiSarchButton.TabIndex = 9;
            this.btnTrihikisakiSarchButton.TabStop = false;
            this.btnTrihikisakiSarchButton.Tag = "検索画面を表示します";
            this.btnTrihikisakiSarchButton.UseVisualStyleBackColor = false;
            this.btnTrihikisakiSarchButton.ZeroPaddengFlag = false;
            // 
            // txtGenbaCd
            // 
            this.txtGenbaCd.BackColor = System.Drawing.SystemColors.Window;
            this.txtGenbaCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGenbaCd.ChangeUpperCase = true;
            this.txtGenbaCd.CharacterLimitList = null;
            this.txtGenbaCd.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.txtGenbaCd.DBFieldsName = "GENBA_CD";
            this.txtGenbaCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtGenbaCd.DisplayPopUp = null;
            this.txtGenbaCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtGenbaCd.FocusOutCheckMethod")));
            this.txtGenbaCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtGenbaCd.ForeColor = System.Drawing.Color.Black;
            this.txtGenbaCd.GetCodeMasterField = "GENBA_CD, GENBA_NAME_RYAKU";
            this.txtGenbaCd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtGenbaCd.IsInputErrorOccured = false;
            this.txtGenbaCd.ItemDefinedTypes = "varchar";
            this.txtGenbaCd.Location = new System.Drawing.Point(578, 54);
            this.txtGenbaCd.MaxLength = 6;
            this.txtGenbaCd.Name = "txtGenbaCd";
            this.txtGenbaCd.PopupAfterExecute = null;
            this.txtGenbaCd.PopupBeforeExecute = null;
            this.txtGenbaCd.PopupGetMasterField = "GENBA_CD, GENBA_NAME_RYAKU,GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.txtGenbaCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtGenbaCd.PopupSearchSendParams")));
            this.txtGenbaCd.PopupSetFormField = "txtGenbaCd,txtGenbaName,txtGyosyaCd,txtGyosyaName";
            this.txtGenbaCd.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.txtGenbaCd.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.txtGenbaCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtGenbaCd.popupWindowSetting")));
            this.txtGenbaCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtGenbaCd.RegistCheckMethod")));
            this.txtGenbaCd.SetFormField = "txtGenbaCd,txtGenbaName";
            this.txtGenbaCd.Size = new System.Drawing.Size(50, 20);
            this.txtGenbaCd.TabIndex = 13;
            this.txtGenbaCd.Tag = "現場を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.txtGenbaCd.ZeroPaddengFlag = true;
            this.txtGenbaCd.Leave += new System.EventHandler(this.txtGenbaCd_Leave);
            this.txtGenbaCd.Validated += new System.EventHandler(this.txtGenbaCd_Validated);
            // 
            // txtGyosyaCd
            // 
            this.txtGyosyaCd.BackColor = System.Drawing.SystemColors.Window;
            this.txtGyosyaCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGyosyaCd.ChangeUpperCase = true;
            this.txtGyosyaCd.CharacterLimitList = null;
            this.txtGyosyaCd.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.txtGyosyaCd.DBFieldsName = "GYOUSHA_CD";
            this.txtGyosyaCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtGyosyaCd.DisplayItemName = "業者";
            this.txtGyosyaCd.DisplayPopUp = null;
            this.txtGyosyaCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtGyosyaCd.FocusOutCheckMethod")));
            this.txtGyosyaCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtGyosyaCd.ForeColor = System.Drawing.Color.Black;
            this.txtGyosyaCd.GetCodeMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.txtGyosyaCd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtGyosyaCd.IsInputErrorOccured = false;
            this.txtGyosyaCd.ItemDefinedTypes = "varchar";
            this.txtGyosyaCd.Location = new System.Drawing.Point(578, 32);
            this.txtGyosyaCd.MaxLength = 6;
            this.txtGyosyaCd.Name = "txtGyosyaCd";
            this.txtGyosyaCd.PopupAfterExecute = null;
            this.txtGyosyaCd.PopupAfterExecuteMethod = "AfterPopupGyousha";
            this.txtGyosyaCd.PopupBeforeExecute = null;
            this.txtGyosyaCd.PopupBeforeExecuteMethod = "BeforePopupGyousha";
            this.txtGyosyaCd.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.txtGyosyaCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtGyosyaCd.PopupSearchSendParams")));
            this.txtGyosyaCd.PopupSetFormField = "txtGyosyaCd,txtGyosyaName";
            this.txtGyosyaCd.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.txtGyosyaCd.PopupWindowName = "検索共通ポップアップ";
            this.txtGyosyaCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtGyosyaCd.popupWindowSetting")));
            this.txtGyosyaCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtGyosyaCd.RegistCheckMethod")));
            this.txtGyosyaCd.SetFormField = "txtGyosyaCd,txtGyosyaName";
            this.txtGyosyaCd.Size = new System.Drawing.Size(50, 20);
            this.txtGyosyaCd.TabIndex = 10;
            this.txtGyosyaCd.Tag = "業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.txtGyosyaCd.ZeroPaddengFlag = true;
            this.txtGyosyaCd.Enter += new System.EventHandler(this.txtGyosyaCd_Enter);
            this.txtGyosyaCd.Validated += new System.EventHandler(this.txtGyosyaCd_Validated);
            // 
            // dtpSeikyuDate
            // 
            this.dtpSeikyuDate.BackColor = System.Drawing.SystemColors.Window;
            this.dtpSeikyuDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtpSeikyuDate.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.dtpSeikyuDate.Checked = false;
            this.dtpSeikyuDate.CustomFormat = "yyyy/MM/dd(ddd)";
            this.dtpSeikyuDate.DateTimeNowYear = "";
            this.dtpSeikyuDate.DefaultBackColor = System.Drawing.Color.Empty;
            this.dtpSeikyuDate.DisplayItemName = "売上日付";
            this.dtpSeikyuDate.DisplayPopUp = null;
            this.dtpSeikyuDate.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtpSeikyuDate.FocusOutCheckMethod")));
            this.dtpSeikyuDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.dtpSeikyuDate.ForeColor = System.Drawing.Color.Black;
            this.dtpSeikyuDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSeikyuDate.IsInputErrorOccured = false;
            this.dtpSeikyuDate.Location = new System.Drawing.Point(578, 76);
            this.dtpSeikyuDate.MaxLength = 10;
            this.dtpSeikyuDate.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpSeikyuDate.Name = "dtpSeikyuDate";
            this.dtpSeikyuDate.NullValue = "";
            this.dtpSeikyuDate.PopupAfterExecute = null;
            this.dtpSeikyuDate.PopupBeforeExecute = null;
            this.dtpSeikyuDate.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dtpSeikyuDate.PopupSearchSendParams")));
            this.dtpSeikyuDate.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dtpSeikyuDate.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dtpSeikyuDate.popupWindowSetting")));
            this.dtpSeikyuDate.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtpSeikyuDate.RegistCheckMethod")));
            this.dtpSeikyuDate.Size = new System.Drawing.Size(138, 20);
            this.dtpSeikyuDate.TabIndex = 16;
            this.dtpSeikyuDate.Tag = "日付を選択してください";
            this.dtpSeikyuDate.Text = "2013/11/12(火)";
            this.dtpSeikyuDate.Value = new System.DateTime(2013, 11, 12, 0, 0, 0, 0);
            this.dtpSeikyuDate.Leave += new System.EventHandler(this.dtpSeikyuDate_Leave);
            // 
            // lbl_SeikyuDate
            // 
            this.lbl_SeikyuDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_SeikyuDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_SeikyuDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_SeikyuDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_SeikyuDate.ForeColor = System.Drawing.Color.White;
            this.lbl_SeikyuDate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_SeikyuDate.Location = new System.Drawing.Point(463, 76);
            this.lbl_SeikyuDate.Name = "lbl_SeikyuDate";
            this.lbl_SeikyuDate.Size = new System.Drawing.Size(110, 20);
            this.lbl_SeikyuDate.TabIndex = 613;
            this.lbl_SeikyuDate.Text = "売上日付※";
            this.lbl_SeikyuDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtGenbaName
            // 
            this.txtGenbaName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtGenbaName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGenbaName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txtGenbaName.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtGenbaName.DisplayPopUp = null;
            this.txtGenbaName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtGenbaName.FocusOutCheckMethod")));
            this.txtGenbaName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtGenbaName.ForeColor = System.Drawing.Color.Black;
            this.txtGenbaName.IsInputErrorOccured = false;
            this.txtGenbaName.Location = new System.Drawing.Point(630, 54);
            this.txtGenbaName.MaxLength = 0;
            this.txtGenbaName.Name = "txtGenbaName";
            this.txtGenbaName.PopupAfterExecute = null;
            this.txtGenbaName.PopupBeforeExecute = null;
            this.txtGenbaName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtGenbaName.PopupSearchSendParams")));
            this.txtGenbaName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtGenbaName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtGenbaName.popupWindowSetting")));
            this.txtGenbaName.ReadOnly = true;
            this.txtGenbaName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtGenbaName.RegistCheckMethod")));
            this.txtGenbaName.Size = new System.Drawing.Size(285, 20);
            this.txtGenbaName.TabIndex = 14;
            this.txtGenbaName.TabStop = false;
            this.txtGenbaName.Tag = "";
            // 
            // lbl_Genba
            // 
            this.lbl_Genba.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Genba.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Genba.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Genba.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_Genba.ForeColor = System.Drawing.Color.White;
            this.lbl_Genba.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_Genba.Location = new System.Drawing.Point(463, 54);
            this.lbl_Genba.Name = "lbl_Genba";
            this.lbl_Genba.Size = new System.Drawing.Size(110, 20);
            this.lbl_Genba.TabIndex = 612;
            this.lbl_Genba.Text = "現場";
            this.lbl_Genba.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtGyosyaName
            // 
            this.txtGyosyaName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtGyosyaName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGyosyaName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txtGyosyaName.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtGyosyaName.DisplayPopUp = null;
            this.txtGyosyaName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtGyosyaName.FocusOutCheckMethod")));
            this.txtGyosyaName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtGyosyaName.ForeColor = System.Drawing.Color.Black;
            this.txtGyosyaName.IsInputErrorOccured = false;
            this.txtGyosyaName.Location = new System.Drawing.Point(630, 32);
            this.txtGyosyaName.MaxLength = 0;
            this.txtGyosyaName.Name = "txtGyosyaName";
            this.txtGyosyaName.PopupAfterExecute = null;
            this.txtGyosyaName.PopupBeforeExecute = null;
            this.txtGyosyaName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtGyosyaName.PopupSearchSendParams")));
            this.txtGyosyaName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtGyosyaName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtGyosyaName.popupWindowSetting")));
            this.txtGyosyaName.ReadOnly = true;
            this.txtGyosyaName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtGyosyaName.RegistCheckMethod")));
            this.txtGyosyaName.Size = new System.Drawing.Size(285, 20);
            this.txtGyosyaName.TabIndex = 11;
            this.txtGyosyaName.TabStop = false;
            this.txtGyosyaName.Tag = "";
            // 
            // lbl_Gyosha
            // 
            this.lbl_Gyosha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Gyosha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Gyosha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Gyosha.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_Gyosha.ForeColor = System.Drawing.Color.White;
            this.lbl_Gyosha.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_Gyosha.Location = new System.Drawing.Point(463, 32);
            this.lbl_Gyosha.Name = "lbl_Gyosha";
            this.lbl_Gyosha.Size = new System.Drawing.Size(110, 20);
            this.lbl_Gyosha.TabIndex = 611;
            this.lbl_Gyosha.Text = "業者";
            this.lbl_Gyosha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTorihikisakiName
            // 
            this.txtTorihikisakiName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtTorihikisakiName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTorihikisakiName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txtTorihikisakiName.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtTorihikisakiName.DisplayPopUp = null;
            this.txtTorihikisakiName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtTorihikisakiName.FocusOutCheckMethod")));
            this.txtTorihikisakiName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtTorihikisakiName.ForeColor = System.Drawing.Color.Black;
            this.txtTorihikisakiName.IsInputErrorOccured = false;
            this.txtTorihikisakiName.Location = new System.Drawing.Point(630, 10);
            this.txtTorihikisakiName.MaxLength = 0;
            this.txtTorihikisakiName.Name = "txtTorihikisakiName";
            this.txtTorihikisakiName.PopupAfterExecute = null;
            this.txtTorihikisakiName.PopupBeforeExecute = null;
            this.txtTorihikisakiName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtTorihikisakiName.PopupSearchSendParams")));
            this.txtTorihikisakiName.PopupSetFormField = "";
            this.txtTorihikisakiName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtTorihikisakiName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtTorihikisakiName.popupWindowSetting")));
            this.txtTorihikisakiName.ReadOnly = true;
            this.txtTorihikisakiName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtTorihikisakiName.RegistCheckMethod")));
            this.txtTorihikisakiName.SetFormField = "";
            this.txtTorihikisakiName.Size = new System.Drawing.Size(285, 20);
            this.txtTorihikisakiName.TabIndex = 8;
            this.txtTorihikisakiName.TabStop = false;
            this.txtTorihikisakiName.Tag = "";
            // 
            // txtTorihikisakiCd
            // 
            this.txtTorihikisakiCd.BackColor = System.Drawing.SystemColors.Window;
            this.txtTorihikisakiCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTorihikisakiCd.ChangeUpperCase = true;
            this.txtTorihikisakiCd.CharacterLimitList = null;
            this.txtTorihikisakiCd.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.txtTorihikisakiCd.DBFieldsName = "TORIHIKISAKI_CD";
            this.txtTorihikisakiCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtTorihikisakiCd.DisplayItemName = "取引先";
            this.txtTorihikisakiCd.DisplayPopUp = null;
            this.txtTorihikisakiCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtTorihikisakiCd.FocusOutCheckMethod")));
            this.txtTorihikisakiCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtTorihikisakiCd.ForeColor = System.Drawing.Color.Black;
            this.txtTorihikisakiCd.GetCodeMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.txtTorihikisakiCd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtTorihikisakiCd.IsInputErrorOccured = false;
            this.txtTorihikisakiCd.ItemDefinedTypes = "varchar";
            this.txtTorihikisakiCd.Location = new System.Drawing.Point(578, 10);
            this.txtTorihikisakiCd.MaxLength = 6;
            this.txtTorihikisakiCd.Name = "txtTorihikisakiCd";
            this.txtTorihikisakiCd.PopupAfterExecute = null;
            this.txtTorihikisakiCd.PopupBeforeExecute = null;
            this.txtTorihikisakiCd.PopupGetMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.txtTorihikisakiCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtTorihikisakiCd.PopupSearchSendParams")));
            this.txtTorihikisakiCd.PopupSetFormField = "txtTorihikisakiCd,txtTorihikisakiName";
            this.txtTorihikisakiCd.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.txtTorihikisakiCd.PopupWindowName = "検索共通ポップアップ";
            this.txtTorihikisakiCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtTorihikisakiCd.popupWindowSetting")));
            this.txtTorihikisakiCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtTorihikisakiCd.RegistCheckMethod")));
            this.txtTorihikisakiCd.SetFormField = "txtTorihikisakiCd,txtTorihikisakiName";
            this.txtTorihikisakiCd.ShortItemName = "取引先CD";
            this.txtTorihikisakiCd.Size = new System.Drawing.Size(50, 20);
            this.txtTorihikisakiCd.TabIndex = 7;
            this.txtTorihikisakiCd.Tag = "取引先を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.txtTorihikisakiCd.ZeroPaddengFlag = true;
            this.txtTorihikisakiCd.Validated += new System.EventHandler(this.txtTorihikisakiCd_Validated);
            // 
            // lbl_Torihikisaki
            // 
            this.lbl_Torihikisaki.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Torihikisaki.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Torihikisaki.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Torihikisaki.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_Torihikisaki.ForeColor = System.Drawing.Color.White;
            this.lbl_Torihikisaki.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_Torihikisaki.Location = new System.Drawing.Point(463, 10);
            this.lbl_Torihikisaki.Name = "lbl_Torihikisaki";
            this.lbl_Torihikisaki.Size = new System.Drawing.Size(110, 20);
            this.lbl_Torihikisaki.TabIndex = 610;
            this.lbl_Torihikisaki.Text = "取引先";
            this.lbl_Torihikisaki.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpTaishoKikanTo
            // 
            this.dtpTaishoKikanTo.BackColor = System.Drawing.SystemColors.Window;
            this.dtpTaishoKikanTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtpTaishoKikanTo.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.dtpTaishoKikanTo.Checked = false;
            this.dtpTaishoKikanTo.CustomFormat = "yyyy/MM/dd(ddd)";
            this.dtpTaishoKikanTo.DateTimeNowYear = "";
            this.dtpTaishoKikanTo.DefaultBackColor = System.Drawing.Color.Empty;
            this.dtpTaishoKikanTo.DisplayItemName = "対象期間TO";
            this.dtpTaishoKikanTo.DisplayPopUp = null;
            this.dtpTaishoKikanTo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtpTaishoKikanTo.FocusOutCheckMethod")));
            this.dtpTaishoKikanTo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.dtpTaishoKikanTo.ForeColor = System.Drawing.Color.Black;
            this.dtpTaishoKikanTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTaishoKikanTo.IsInputErrorOccured = false;
            this.dtpTaishoKikanTo.Location = new System.Drawing.Point(298, 55);
            this.dtpTaishoKikanTo.MaxLength = 10;
            this.dtpTaishoKikanTo.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTaishoKikanTo.Name = "dtpTaishoKikanTo";
            this.dtpTaishoKikanTo.NullValue = "";
            this.dtpTaishoKikanTo.PopupAfterExecute = null;
            this.dtpTaishoKikanTo.PopupBeforeExecute = null;
            this.dtpTaishoKikanTo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dtpTaishoKikanTo.PopupSearchSendParams")));
            this.dtpTaishoKikanTo.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dtpTaishoKikanTo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dtpTaishoKikanTo.popupWindowSetting")));
            this.dtpTaishoKikanTo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtpTaishoKikanTo.RegistCheckMethod")));
            this.dtpTaishoKikanTo.Size = new System.Drawing.Size(138, 20);
            this.dtpTaishoKikanTo.TabIndex = 6;
            this.dtpTaishoKikanTo.Tag = "日付を選択してください";
            this.dtpTaishoKikanTo.Text = "2013/11/12(火)";
            this.dtpTaishoKikanTo.Value = new System.DateTime(2013, 11, 12, 0, 0, 0, 0);
            this.dtpTaishoKikanTo.Leave += new System.EventHandler(this.dtpTaishoKikanTo_Leave);
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.Transparent;
            this.label38.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label38.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label38.ForeColor = System.Drawing.Color.Black;
            this.label38.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label38.Location = new System.Drawing.Point(271, 55);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(19, 20);
            this.label38.TabIndex = 609;
            this.label38.Text = "～";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_TaishoKikan
            // 
            this.lbl_TaishoKikan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_TaishoKikan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_TaishoKikan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_TaishoKikan.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_TaishoKikan.ForeColor = System.Drawing.Color.White;
            this.lbl_TaishoKikan.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_TaishoKikan.Location = new System.Drawing.Point(10, 55);
            this.lbl_TaishoKikan.Name = "lbl_TaishoKikan";
            this.lbl_TaishoKikan.Size = new System.Drawing.Size(110, 20);
            this.lbl_TaishoKikan.TabIndex = 608;
            this.lbl_TaishoKikan.Text = "対象期間※";
            this.lbl_TaishoKikan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpTaishoKikanFrom
            // 
            this.dtpTaishoKikanFrom.BackColor = System.Drawing.SystemColors.Window;
            this.dtpTaishoKikanFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtpTaishoKikanFrom.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.dtpTaishoKikanFrom.Checked = false;
            this.dtpTaishoKikanFrom.Cursor = System.Windows.Forms.Cursors.Default;
            this.dtpTaishoKikanFrom.CustomFormat = "yyyy/MM/dd(ddd)";
            this.dtpTaishoKikanFrom.DateTimeNowYear = "";
            this.dtpTaishoKikanFrom.DefaultBackColor = System.Drawing.Color.Empty;
            this.dtpTaishoKikanFrom.DisplayItemName = "対象期間FROM";
            this.dtpTaishoKikanFrom.DisplayPopUp = null;
            this.dtpTaishoKikanFrom.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtpTaishoKikanFrom.FocusOutCheckMethod")));
            this.dtpTaishoKikanFrom.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.dtpTaishoKikanFrom.ForeColor = System.Drawing.Color.Black;
            this.dtpTaishoKikanFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTaishoKikanFrom.IsInputErrorOccured = false;
            this.dtpTaishoKikanFrom.Location = new System.Drawing.Point(125, 55);
            this.dtpTaishoKikanFrom.MaxLength = 10;
            this.dtpTaishoKikanFrom.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpTaishoKikanFrom.Name = "dtpTaishoKikanFrom";
            this.dtpTaishoKikanFrom.NullValue = "";
            this.dtpTaishoKikanFrom.PopupAfterExecute = null;
            this.dtpTaishoKikanFrom.PopupBeforeExecute = null;
            this.dtpTaishoKikanFrom.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dtpTaishoKikanFrom.PopupSearchSendParams")));
            this.dtpTaishoKikanFrom.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dtpTaishoKikanFrom.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dtpTaishoKikanFrom.popupWindowSetting")));
            this.dtpTaishoKikanFrom.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtpTaishoKikanFrom.RegistCheckMethod")));
            this.dtpTaishoKikanFrom.Size = new System.Drawing.Size(138, 20);
            this.dtpTaishoKikanFrom.TabIndex = 5;
            this.dtpTaishoKikanFrom.Tag = "日付を選択してください";
            this.dtpTaishoKikanFrom.Text = "2013/11/12(火)";
            this.dtpTaishoKikanFrom.Value = new System.DateTime(2013, 11, 12, 0, 0, 0, 0);
            this.dtpTaishoKikanFrom.Leave += new System.EventHandler(this.dtpTaishoKikanFrom_Leave);
            // 
            // cmbShimebi
            // 
            this.cmbShimebi.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbShimebi.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbShimebi.BackColor = System.Drawing.SystemColors.Window;
            this.cmbShimebi.DefaultBackColor = System.Drawing.Color.Empty;
            this.cmbShimebi.DisplayPopUp = null;
            this.cmbShimebi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbShimebi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cmbShimebi.FocusOutCheckMethod")));
            this.cmbShimebi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cmbShimebi.FormattingEnabled = true;
            this.cmbShimebi.IsInputErrorOccured = false;
            this.cmbShimebi.Items.AddRange(new object[] {
            "0",
            "5",
            "10",
            "15",
            "20",
            "25",
            "31"});
            this.cmbShimebi.Location = new System.Drawing.Point(125, 33);
            this.cmbShimebi.Name = "cmbShimebi";
            this.cmbShimebi.PopupAfterExecute = null;
            this.cmbShimebi.PopupBeforeExecute = null;
            this.cmbShimebi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cmbShimebi.PopupSearchSendParams")));
            this.cmbShimebi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cmbShimebi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cmbShimebi.popupWindowSetting")));
            this.cmbShimebi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cmbShimebi.RegistCheckMethod")));
            this.cmbShimebi.Size = new System.Drawing.Size(40, 21);
            this.cmbShimebi.TabIndex = 4;
            this.cmbShimebi.Tag = "締日を入力してください（取引先の締日１に対する抽出条件です）";
            this.cmbShimebi.SelectedIndexChanged += new System.EventHandler(this.cmbShimebi_SelectedIndexChanged);
            // 
            // lbl_shimebi
            // 
            this.lbl_shimebi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_shimebi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_shimebi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_shimebi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_shimebi.ForeColor = System.Drawing.Color.White;
            this.lbl_shimebi.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_shimebi.Location = new System.Drawing.Point(10, 33);
            this.lbl_shimebi.Name = "lbl_shimebi";
            this.lbl_shimebi.Size = new System.Drawing.Size(110, 20);
            this.lbl_shimebi.TabIndex = 607;
            this.lbl_shimebi.Text = "締日※";
            this.lbl_shimebi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtKyotenName
            // 
            this.txtKyotenName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtKyotenName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKyotenName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txtKyotenName.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtKyotenName.DisplayPopUp = null;
            this.txtKyotenName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenName.FocusOutCheckMethod")));
            this.txtKyotenName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtKyotenName.ForeColor = System.Drawing.Color.Black;
            this.txtKyotenName.IsInputErrorOccured = false;
            this.txtKyotenName.Location = new System.Drawing.Point(177, 10);
            this.txtKyotenName.MaxLength = 0;
            this.txtKyotenName.Name = "txtKyotenName";
            this.txtKyotenName.PopupAfterExecute = null;
            this.txtKyotenName.PopupBeforeExecute = null;
            this.txtKyotenName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtKyotenName.PopupSearchSendParams")));
            this.txtKyotenName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtKyotenName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtKyotenName.popupWindowSetting")));
            this.txtKyotenName.ReadOnly = true;
            this.txtKyotenName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenName.RegistCheckMethod")));
            this.txtKyotenName.Size = new System.Drawing.Size(255, 20);
            this.txtKyotenName.TabIndex = 2;
            this.txtKyotenName.TabStop = false;
            this.txtKyotenName.Tag = "";
            // 
            // lbl_Kyoten
            // 
            this.lbl_Kyoten.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Kyoten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Kyoten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Kyoten.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_Kyoten.ForeColor = System.Drawing.Color.White;
            this.lbl_Kyoten.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_Kyoten.Location = new System.Drawing.Point(10, 10);
            this.lbl_Kyoten.Name = "lbl_Kyoten";
            this.lbl_Kyoten.Size = new System.Drawing.Size(110, 20);
            this.lbl_Kyoten.TabIndex = 605;
            this.lbl_Kyoten.Text = "拠点※";
            this.lbl_Kyoten.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkSakusei
            // 
            this.chkSakusei.AutoSize = true;
            this.chkSakusei.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.chkSakusei.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.chkSakusei.DefaultBackColor = System.Drawing.Color.Empty;
            this.chkSakusei.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("chkSakusei.FocusOutCheckMethod")));
            this.chkSakusei.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkSakusei.Location = new System.Drawing.Point(29, 139);
            this.chkSakusei.Name = "chkSakusei";
            this.chkSakusei.PopupAfterExecute = null;
            this.chkSakusei.PopupBeforeExecute = null;
            this.chkSakusei.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("chkSakusei.PopupSearchSendParams")));
            this.chkSakusei.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.chkSakusei.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("chkSakusei.popupWindowSetting")));
            this.chkSakusei.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("chkSakusei.RegistCheckMethod")));
            this.chkSakusei.Size = new System.Drawing.Size(15, 14);
            this.chkSakusei.TabIndex = 20;
            this.chkSakusei.Tag = "全伝票のチェックを設定/解除します。";
            this.chkSakusei.UseVisualStyleBackColor = false;
            this.chkSakusei.CheckedChanged += new System.EventHandler(this.checkBoxAll_CheckedChanged);
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
            this.txtKyotenCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtKyotenCd.ForeColor = System.Drawing.Color.Black;
            this.txtKyotenCd.FormatSetting = "カスタム";
            this.txtKyotenCd.GetCodeMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.txtKyotenCd.IsInputErrorOccured = false;
            this.txtKyotenCd.ItemDefinedTypes = "smallint";
            this.txtKyotenCd.Location = new System.Drawing.Point(125, 10);
            this.txtKyotenCd.Name = "txtKyotenCd";
            this.txtKyotenCd.PopupAfterExecute = null;
            this.txtKyotenCd.PopupBeforeExecute = null;
            this.txtKyotenCd.PopupGetMasterField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.txtKyotenCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtKyotenCd.PopupSearchSendParams")));
            this.txtKyotenCd.PopupSetFormField = "txtKyotenCd,txtKyotenName";
            this.txtKyotenCd.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.txtKyotenCd.PopupWindowName = "マスタ共通ポップアップ";
            this.txtKyotenCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtKyotenCd.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.txtKyotenCd.RangeSetting = rangeSettingDto1;
            this.txtKyotenCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenCd.RegistCheckMethod")));
            this.txtKyotenCd.SetFormField = "txtKyotenCd,txtKyotenName";
            this.txtKyotenCd.ShortItemName = "拠点CD";
            this.txtKyotenCd.Size = new System.Drawing.Size(50, 20);
            this.txtKyotenCd.TabIndex = 1;
            this.txtKyotenCd.Tag = "伝票に設定する拠点を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.txtKyotenCd.WordWrap = false;
            this.txtKyotenCd.TextChanged += new System.EventHandler(this.txtKyotenCd_TextChanged);
            // 
            // mrwTukigimeUriageDenpyo
            // 
            this.mrwTukigimeUriageDenpyo.AllowUserToAddRows = false;
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.mrwTukigimeUriageDenpyo.ColumnHeadersDefaultHeaderCellStyle = cellStyle1;
            this.mrwTukigimeUriageDenpyo.CurrentRowBorderLine = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Medium, System.Drawing.Color.Red);
            this.mrwTukigimeUriageDenpyo.EditMode = GrapeCity.Win.MultiRow.EditMode.EditOnEnter;
            this.mrwTukigimeUriageDenpyo.Location = new System.Drawing.Point(10, 102);
            this.mrwTukigimeUriageDenpyo.MultiSelect = false;
            this.mrwTukigimeUriageDenpyo.Name = "mrwTukigimeUriageDenpyo";
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
            this.mrwTukigimeUriageDenpyo.ShortcutKeyManager = shortcutKeyManager1;
            this.mrwTukigimeUriageDenpyo.Size = new System.Drawing.Size(899, 357);
            this.mrwTukigimeUriageDenpyo.TabIndex = 21;
            this.mrwTukigimeUriageDenpyo.Template = this.mrwMainForm1;
            this.mrwTukigimeUriageDenpyo.Text = "gcCustomMultiRow1";
            this.mrwTukigimeUriageDenpyo.CellPainting += new System.EventHandler<GrapeCity.Win.MultiRow.CellPaintingEventArgs>(this.mrwTukigimeUriageDenpyo_CellPainting);
            this.mrwTukigimeUriageDenpyo.CellClick += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.mrwTukigimeUriageDenpyo_CellClick);
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.dtpTaishoKikanFrom);
            this.Controls.Add(this.txtKyotenCd);
            this.Controls.Add(this.chkSakusei);
            this.Controls.Add(this.btnKyotenSearch);
            this.Controls.Add(this.btnGyosyaSearch);
            this.Controls.Add(this.btnGenbaSearch);
            this.Controls.Add(this.btnTrihikisakiSarchButton);
            this.Controls.Add(this.txtGenbaCd);
            this.Controls.Add(this.txtGyosyaCd);
            this.Controls.Add(this.dtpSeikyuDate);
            this.Controls.Add(this.lbl_SeikyuDate);
            this.Controls.Add(this.txtGenbaName);
            this.Controls.Add(this.lbl_Genba);
            this.Controls.Add(this.txtGyosyaName);
            this.Controls.Add(this.lbl_Gyosha);
            this.Controls.Add(this.txtTorihikisakiName);
            this.Controls.Add(this.txtTorihikisakiCd);
            this.Controls.Add(this.lbl_Torihikisaki);
            this.Controls.Add(this.dtpTaishoKikanTo);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.lbl_TaishoKikan);
            this.Controls.Add(this.cmbShimebi);
            this.Controls.Add(this.lbl_shimebi);
            this.Controls.Add(this.txtKyotenName);
            this.Controls.Add(this.lbl_Kyoten);
            this.Controls.Add(this.mrwTukigimeUriageDenpyo);
            this.Name = "UIForm";
            this.Text = "UIForm";
            ((System.ComponentModel.ISupportInitialize)(this.mrwTukigimeUriageDenpyo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private mrwMainForm mrwMainForm1;
        internal r_framework.CustomControl.GcCustomMultiRow mrwTukigimeUriageDenpyo;
        internal r_framework.CustomControl.CustomPopupOpenButton btnKyotenSearch;
        internal r_framework.CustomControl.CustomPopupOpenButton btnGyosyaSearch;
        internal r_framework.CustomControl.CustomPopupOpenButton btnGenbaSearch;
        internal r_framework.CustomControl.CustomPopupOpenButton btnTrihikisakiSarchButton;
        public r_framework.CustomControl.CustomAlphaNumTextBox txtGenbaCd;
        public r_framework.CustomControl.CustomAlphaNumTextBox txtGyosyaCd;
        public r_framework.CustomControl.CustomDateTimePicker dtpSeikyuDate;
        private System.Windows.Forms.Label lbl_SeikyuDate;
        public r_framework.CustomControl.CustomTextBox txtGenbaName;
        private System.Windows.Forms.Label lbl_Genba;
        public r_framework.CustomControl.CustomTextBox txtGyosyaName;
        private System.Windows.Forms.Label lbl_Gyosha;
        public r_framework.CustomControl.CustomTextBox txtTorihikisakiName;
        public r_framework.CustomControl.CustomAlphaNumTextBox txtTorihikisakiCd;
        private System.Windows.Forms.Label lbl_Torihikisaki;
        public r_framework.CustomControl.CustomDateTimePicker dtpTaishoKikanTo;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label lbl_TaishoKikan;
        public r_framework.CustomControl.CustomDateTimePicker dtpTaishoKikanFrom;
        public r_framework.CustomControl.CustomComboBox cmbShimebi;
        private System.Windows.Forms.Label lbl_shimebi;
        public r_framework.CustomControl.CustomTextBox txtKyotenName;
        private System.Windows.Forms.Label lbl_Kyoten;
        internal r_framework.CustomControl.CustomCheckBox chkSakusei;
        public r_framework.CustomControl.CustomNumericTextBox2 txtKyotenCd;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
    }
}