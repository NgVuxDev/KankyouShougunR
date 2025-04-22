namespace Shougun.Core.BusinessManagement.DenpyouDetailIkkatuUpdate.APP
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
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.ShortcutKeyManager shortcutKeyManager1 = new GrapeCity.Win.MultiRow.ShortcutKeyManager();
            r_framework.Dto.RangeSettingDto rangeSettingDto5 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto6 = new r_framework.Dto.RangeSettingDto();
            this.btnGenbaSrch = new r_framework.CustomControl.CustomPopupOpenButton();
            this.GENBA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GENBA_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.lblGenba = new System.Windows.Forms.Label();
            this.btnGyoushaSrch = new r_framework.CustomControl.CustomPopupOpenButton();
            this.GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GYOUSHA_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.lblGyousha = new System.Windows.Forms.Label();
            this.btnTorihikisakiSrch = new r_framework.CustomControl.CustomPopupOpenButton();
            this.TORIHIKISAKI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.TORIHIKISAKI_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.lblTorihikisaki = new System.Windows.Forms.Label();
            this.HIDUKE_FROM = new r_framework.CustomControl.CustomDateTimePicker();
            this.HIDUKE_TO = new r_framework.CustomControl.CustomDateTimePicker();
            this.lblHiduke = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.KYOTEN_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.KYOTEN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.lblKyoten = new System.Windows.Forms.Label();
            this.btnKyotenSrch = new r_framework.CustomControl.CustomPopupOpenButton();
            this.txtKakuteiKbn = new r_framework.CustomControl.CustomNumericTextBox2();
            this.pnlDenpyouDateKbn = new r_framework.CustomControl.CustomPanel();
            this.rdoKakuteiKbn3 = new r_framework.CustomControl.CustomRadioButton();
            this.rdoKakuteiKbn2 = new r_framework.CustomControl.CustomRadioButton();
            this.rdoKakuteiKbn1 = new r_framework.CustomControl.CustomRadioButton();
            this.lblKakutei = new System.Windows.Forms.Label();
            this.txtDenshuKbn = new r_framework.CustomControl.CustomNumericTextBox2();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.rdoDenshuKbn3 = new r_framework.CustomControl.CustomRadioButton();
            this.rdoDenshuKbn2 = new r_framework.CustomControl.CustomRadioButton();
            this.rdoDenshuKbn1 = new r_framework.CustomControl.CustomRadioButton();
            this.lblDenshu = new System.Windows.Forms.Label();
            this.btnUpnGyoushaSrch = new r_framework.CustomControl.CustomPopupOpenButton();
            this.UPN_GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.UPN_GYOUSHA_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.lblUpnGyousha = new System.Windows.Forms.Label();
            this.btnNizumiGenbaSrch = new r_framework.CustomControl.CustomPopupOpenButton();
            this.NIZUMI_GENBA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.NIZUMI_GENBA_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.lblNizumiGenba = new System.Windows.Forms.Label();
            this.btnNizumiGyoushaSrch = new r_framework.CustomControl.CustomPopupOpenButton();
            this.NIZUMI_GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.NIZUMI_GYOUSHA_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.lblNitumiGyousha = new System.Windows.Forms.Label();
            this.btnNioroshiGenbaSrch = new r_framework.CustomControl.CustomPopupOpenButton();
            this.NIOROSHI_GENBA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.NIOROSHI_GENBA_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.lblNioroshiGenba = new System.Windows.Forms.Label();
            this.btnNioroshiGyoushaSrch = new r_framework.CustomControl.CustomPopupOpenButton();
            this.NIOROSHI_GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.NIOROSHI_GYOUSHA_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.lblNioroshiGyousha = new System.Windows.Forms.Label();
            this.btnHinmeiSrch = new r_framework.CustomControl.CustomPopupOpenButton();
            this.HINMEI_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.HINMEI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.lblHinmei = new System.Windows.Forms.Label();
            this.btnShuruiSrch = new r_framework.CustomControl.CustomPopupOpenButton();
            this.SHURUI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.SHURUI_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.lblShurui = new System.Windows.Forms.Label();
            this.btnBunruiSrch = new r_framework.CustomControl.CustomPopupOpenButton();
            this.BUNRUI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.BUNRUI_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.lblBunrui = new System.Windows.Forms.Label();
            this.btnUnitSrch = new r_framework.CustomControl.CustomPopupOpenButton();
            this.UNIT_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.UNIT_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.lblUnit = new System.Windows.Forms.Label();
            this.mrDetail = new r_framework.CustomControl.GcCustomMultiRow(this.components);
            this.mrDetailUkeireTemplate = new Shougun.Core.BusinessManagement.DenpyouDetailIkkatuUpdate.MultiRowTemplate.DetailTemplateUkeire();
            this.mrDetailShukkaTemplate = new Shougun.Core.BusinessManagement.DenpyouDetailIkkatuUpdate.MultiRowTemplate.DetailTemplateShukka();
            this.mrDetailUrshTemplate = new Shougun.Core.BusinessManagement.DenpyouDetailIkkatuUpdate.MultiRowTemplate.DetailTemplateUrsh();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.lblDenpyouKbn = new System.Windows.Forms.Label();
            this.DENPYOU_KBN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.DENPYOU_KBN_NAME = new r_framework.CustomControl.CustomTextBox();
            this.lblTorihikiKbn = new System.Windows.Forms.Label();
            this.TORIHIKI_KBN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.TORIHIKI_KBN_NAME = new r_framework.CustomControl.CustomTextBox();
            this.pnlDenpyouDateKbn.SuspendLayout();
            this.customPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mrDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGenbaSrch
            // 
            this.btnGenbaSrch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnGenbaSrch.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.btnGenbaSrch.DBFieldsName = null;
            this.btnGenbaSrch.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnGenbaSrch.DisplayItemName = null;
            this.btnGenbaSrch.DisplayPopUp = null;
            this.btnGenbaSrch.ErrorMessage = null;
            this.btnGenbaSrch.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnGenbaSrch.FocusOutCheckMethod")));
            this.btnGenbaSrch.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.btnGenbaSrch.GetCodeMasterField = null;
            this.btnGenbaSrch.Image = ((System.Drawing.Image)(resources.GetObject("btnGenbaSrch.Image")));
            this.btnGenbaSrch.ItemDefinedTypes = null;
            this.btnGenbaSrch.LinkedSettingTextBox = null;
            this.btnGenbaSrch.LinkedTextBoxs = null;
            this.btnGenbaSrch.Location = new System.Drawing.Point(455, 134);
            this.btnGenbaSrch.Name = "btnGenbaSrch";
            this.btnGenbaSrch.PopupAfterExecute = null;
            this.btnGenbaSrch.PopupAfterExecuteMethod = "GENBA_CD_PopupAfter";
            this.btnGenbaSrch.PopupBeforeExecute = null;
            this.btnGenbaSrch.PopupBeforeExecuteMethod = "";
            this.btnGenbaSrch.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU, GENBA_CD,GENBA_NAME_RYAKU";
            this.btnGenbaSrch.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("btnGenbaSrch.PopupSearchSendParams")));
            this.btnGenbaSrch.PopupSendParams = new string[] {
        "UKEIRE_GYOUSHA_CD,UKEIRE_GENBA_CD"};
            this.btnGenbaSrch.PopupSetFormField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU, GENBA_CD,GENBA_NAME_RYAKU";
            this.btnGenbaSrch.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.btnGenbaSrch.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.btnGenbaSrch.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("btnGenbaSrch.popupWindowSetting")));
            this.btnGenbaSrch.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnGenbaSrch.RegistCheckMethod")));
            this.btnGenbaSrch.SearchDisplayFlag = 0;
            this.btnGenbaSrch.SetFormField = null;
            this.btnGenbaSrch.ShortItemName = null;
            this.btnGenbaSrch.Size = new System.Drawing.Size(22, 22);
            this.btnGenbaSrch.TabIndex = 17;
            this.btnGenbaSrch.TabStop = false;
            this.btnGenbaSrch.Tag = "";
            this.btnGenbaSrch.UseVisualStyleBackColor = false;
            this.btnGenbaSrch.ZeroPaddengFlag = false;
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
            this.GENBA_CD.DBFieldsName = "";
            this.GENBA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_CD.DisplayItemName = "現場";
            this.GENBA_CD.DisplayPopUp = null;
            this.GENBA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.FocusOutCheckMethod")));
            this.GENBA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GENBA_CD.ForeColor = System.Drawing.Color.Black;
            this.GENBA_CD.GetCodeMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU, GENBA_CD,GENBA_NAME_RYAKU";
            this.GENBA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GENBA_CD.IsInputErrorOccured = false;
            this.GENBA_CD.ItemDefinedTypes = "varchar";
            this.GENBA_CD.Location = new System.Drawing.Point(116, 135);
            this.GENBA_CD.MaxLength = 6;
            this.GENBA_CD.Name = "GENBA_CD";
            this.GENBA_CD.PopupAfterExecute = null;
            this.GENBA_CD.PopupAfterExecuteMethod = "GENBA_CD_PopupAfter";
            this.GENBA_CD.PopupBeforeExecute = null;
            this.GENBA_CD.PopupBeforeExecuteMethod = "";
            this.GENBA_CD.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU, GENBA_CD,GENBA_NAME_RYAKU";
            this.GENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_CD.PopupSearchSendParams")));
            this.GENBA_CD.PopupSendParams = new string[0];
            this.GENBA_CD.PopupSetFormField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU, GENBA_CD,GENBA_NAME_RYAKU";
            this.GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_CD.popupWindowSetting")));
            this.GENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.RegistCheckMethod")));
            this.GENBA_CD.SetFormField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU, GENBA_CD,GENBA_NAME_RYAKU";
            this.GENBA_CD.Size = new System.Drawing.Size(50, 20);
            this.GENBA_CD.TabIndex = 15;
            this.GENBA_CD.Tag = "現場を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GENBA_CD.ZeroPaddengFlag = true;
            // 
            // GENBA_NAME_RYAKU
            // 
            this.GENBA_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GENBA_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.GENBA_NAME_RYAKU.DBFieldsName = "GENBA_NAME_RYAKU";
            this.GENBA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_NAME_RYAKU.DisplayPopUp = null;
            this.GENBA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_RYAKU.FocusOutCheckMethod")));
            this.GENBA_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GENBA_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.GENBA_NAME_RYAKU.IsInputErrorOccured = false;
            this.GENBA_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.GENBA_NAME_RYAKU.Location = new System.Drawing.Point(165, 135);
            this.GENBA_NAME_RYAKU.MaxLength = 0;
            this.GENBA_NAME_RYAKU.Name = "GENBA_NAME_RYAKU";
            this.GENBA_NAME_RYAKU.PopupAfterExecute = null;
            this.GENBA_NAME_RYAKU.PopupBeforeExecute = null;
            this.GENBA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_NAME_RYAKU.PopupSearchSendParams")));
            this.GENBA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_NAME_RYAKU.popupWindowSetting")));
            this.GENBA_NAME_RYAKU.ReadOnly = true;
            this.GENBA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_RYAKU.RegistCheckMethod")));
            this.GENBA_NAME_RYAKU.Size = new System.Drawing.Size(290, 20);
            this.GENBA_NAME_RYAKU.TabIndex = 16;
            this.GENBA_NAME_RYAKU.TabStop = false;
            // 
            // lblGenba
            // 
            this.lblGenba.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblGenba.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGenba.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblGenba.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblGenba.ForeColor = System.Drawing.Color.White;
            this.lblGenba.Location = new System.Drawing.Point(1, 135);
            this.lblGenba.Name = "lblGenba";
            this.lblGenba.Size = new System.Drawing.Size(110, 20);
            this.lblGenba.TabIndex = 52;
            this.lblGenba.Text = "現場";
            this.lblGenba.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnGyoushaSrch
            // 
            this.btnGyoushaSrch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnGyoushaSrch.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.btnGyoushaSrch.DBFieldsName = null;
            this.btnGyoushaSrch.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnGyoushaSrch.DisplayItemName = null;
            this.btnGyoushaSrch.DisplayPopUp = null;
            this.btnGyoushaSrch.ErrorMessage = null;
            this.btnGyoushaSrch.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnGyoushaSrch.FocusOutCheckMethod")));
            this.btnGyoushaSrch.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.btnGyoushaSrch.GetCodeMasterField = null;
            this.btnGyoushaSrch.Image = ((System.Drawing.Image)(resources.GetObject("btnGyoushaSrch.Image")));
            this.btnGyoushaSrch.ItemDefinedTypes = null;
            this.btnGyoushaSrch.LinkedSettingTextBox = null;
            this.btnGyoushaSrch.LinkedTextBoxs = null;
            this.btnGyoushaSrch.Location = new System.Drawing.Point(455, 112);
            this.btnGyoushaSrch.Name = "btnGyoushaSrch";
            this.btnGyoushaSrch.PopupAfterExecute = null;
            this.btnGyoushaSrch.PopupAfterExecuteMethod = "GYOUSHA_CD_PopupAfter";
            this.btnGyoushaSrch.PopupBeforeExecute = null;
            this.btnGyoushaSrch.PopupBeforeExecuteMethod = "GYOUSHA_CD_PopupBefore";
            this.btnGyoushaSrch.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.btnGyoushaSrch.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("btnGyoushaSrch.PopupSearchSendParams")));
            this.btnGyoushaSrch.PopupSetFormField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.btnGyoushaSrch.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.btnGyoushaSrch.PopupWindowName = "検索共通ポップアップ";
            this.btnGyoushaSrch.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("btnGyoushaSrch.popupWindowSetting")));
            this.btnGyoushaSrch.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnGyoushaSrch.RegistCheckMethod")));
            this.btnGyoushaSrch.SearchDisplayFlag = 0;
            this.btnGyoushaSrch.SetFormField = "";
            this.btnGyoushaSrch.ShortItemName = null;
            this.btnGyoushaSrch.Size = new System.Drawing.Size(22, 22);
            this.btnGyoushaSrch.TabIndex = 14;
            this.btnGyoushaSrch.TabStop = false;
            this.btnGyoushaSrch.Tag = "";
            this.btnGyoushaSrch.UseVisualStyleBackColor = false;
            this.btnGyoushaSrch.ZeroPaddengFlag = false;
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
            this.GYOUSHA_CD.DBFieldsName = "";
            this.GYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_CD.DisplayItemName = "業者";
            this.GYOUSHA_CD.DisplayPopUp = null;
            this.GYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.FocusOutCheckMethod")));
            this.GYOUSHA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_CD.GetCodeMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GYOUSHA_CD.IsInputErrorOccured = false;
            this.GYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.GYOUSHA_CD.Location = new System.Drawing.Point(116, 113);
            this.GYOUSHA_CD.MaxLength = 6;
            this.GYOUSHA_CD.Name = "GYOUSHA_CD";
            this.GYOUSHA_CD.PopupAfterExecute = null;
            this.GYOUSHA_CD.PopupAfterExecuteMethod = "GYOUSHA_CD_PopupAfter";
            this.GYOUSHA_CD.PopupBeforeExecute = null;
            this.GYOUSHA_CD.PopupBeforeExecuteMethod = "GYOUSHA_CD_PopupBefore";
            this.GYOUSHA_CD.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_CD.PopupSearchSendParams")));
            this.GYOUSHA_CD.PopupSetFormField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_CD.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_CD.popupWindowSetting")));
            this.GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.RegistCheckMethod")));
            this.GYOUSHA_CD.SetFormField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.Size = new System.Drawing.Size(50, 20);
            this.GYOUSHA_CD.TabIndex = 12;
            this.GYOUSHA_CD.Tag = "業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GYOUSHA_CD.ZeroPaddengFlag = true;
            // 
            // GYOUSHA_NAME_RYAKU
            // 
            this.GYOUSHA_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GYOUSHA_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.GYOUSHA_NAME_RYAKU.DBFieldsName = "GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_NAME_RYAKU.DisplayPopUp = null;
            this.GYOUSHA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.FocusOutCheckMethod")));
            this.GYOUSHA_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GYOUSHA_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_NAME_RYAKU.IsInputErrorOccured = false;
            this.GYOUSHA_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.GYOUSHA_NAME_RYAKU.Location = new System.Drawing.Point(165, 113);
            this.GYOUSHA_NAME_RYAKU.MaxLength = 0;
            this.GYOUSHA_NAME_RYAKU.Name = "GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_NAME_RYAKU.PopupAfterExecute = null;
            this.GYOUSHA_NAME_RYAKU.PopupBeforeExecute = null;
            this.GYOUSHA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.PopupSearchSendParams")));
            this.GYOUSHA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.popupWindowSetting")));
            this.GYOUSHA_NAME_RYAKU.ReadOnly = true;
            this.GYOUSHA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.RegistCheckMethod")));
            this.GYOUSHA_NAME_RYAKU.Size = new System.Drawing.Size(290, 20);
            this.GYOUSHA_NAME_RYAKU.TabIndex = 13;
            this.GYOUSHA_NAME_RYAKU.TabStop = false;
            // 
            // lblGyousha
            // 
            this.lblGyousha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblGyousha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGyousha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblGyousha.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblGyousha.ForeColor = System.Drawing.Color.White;
            this.lblGyousha.Location = new System.Drawing.Point(1, 113);
            this.lblGyousha.Name = "lblGyousha";
            this.lblGyousha.Size = new System.Drawing.Size(110, 20);
            this.lblGyousha.TabIndex = 51;
            this.lblGyousha.Text = "業者";
            this.lblGyousha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnTorihikisakiSrch
            // 
            this.btnTorihikisakiSrch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnTorihikisakiSrch.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.btnTorihikisakiSrch.DBFieldsName = null;
            this.btnTorihikisakiSrch.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnTorihikisakiSrch.DisplayItemName = null;
            this.btnTorihikisakiSrch.DisplayPopUp = null;
            this.btnTorihikisakiSrch.ErrorMessage = null;
            this.btnTorihikisakiSrch.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnTorihikisakiSrch.FocusOutCheckMethod")));
            this.btnTorihikisakiSrch.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.btnTorihikisakiSrch.GetCodeMasterField = null;
            this.btnTorihikisakiSrch.Image = ((System.Drawing.Image)(resources.GetObject("btnTorihikisakiSrch.Image")));
            this.btnTorihikisakiSrch.ItemDefinedTypes = null;
            this.btnTorihikisakiSrch.LinkedSettingTextBox = null;
            this.btnTorihikisakiSrch.LinkedTextBoxs = null;
            this.btnTorihikisakiSrch.Location = new System.Drawing.Point(455, 90);
            this.btnTorihikisakiSrch.Name = "btnTorihikisakiSrch";
            this.btnTorihikisakiSrch.PopupAfterExecute = null;
            this.btnTorihikisakiSrch.PopupAfterExecuteMethod = "TORIHIKISAKI_CD_PopupAfter";
            this.btnTorihikisakiSrch.PopupBeforeExecute = null;
            this.btnTorihikisakiSrch.PopupGetMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.btnTorihikisakiSrch.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("btnTorihikisakiSrch.PopupSearchSendParams")));
            this.btnTorihikisakiSrch.PopupSetFormField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.btnTorihikisakiSrch.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.btnTorihikisakiSrch.PopupWindowName = "検索共通ポップアップ";
            this.btnTorihikisakiSrch.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("btnTorihikisakiSrch.popupWindowSetting")));
            this.btnTorihikisakiSrch.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnTorihikisakiSrch.RegistCheckMethod")));
            this.btnTorihikisakiSrch.SearchDisplayFlag = 0;
            this.btnTorihikisakiSrch.SetFormField = "";
            this.btnTorihikisakiSrch.ShortItemName = null;
            this.btnTorihikisakiSrch.Size = new System.Drawing.Size(22, 22);
            this.btnTorihikisakiSrch.TabIndex = 11;
            this.btnTorihikisakiSrch.TabStop = false;
            this.btnTorihikisakiSrch.Tag = "";
            this.btnTorihikisakiSrch.UseVisualStyleBackColor = false;
            this.btnTorihikisakiSrch.ZeroPaddengFlag = false;
            // 
            // TORIHIKISAKI_CD
            // 
            this.TORIHIKISAKI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.TORIHIKISAKI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_CD.ChangeUpperCase = true;
            this.TORIHIKISAKI_CD.CharacterLimitList = null;
            this.TORIHIKISAKI_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.TORIHIKISAKI_CD.DBFieldsName = "";
            this.TORIHIKISAKI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_CD.DisplayItemName = "取引先";
            this.TORIHIKISAKI_CD.DisplayPopUp = null;
            this.TORIHIKISAKI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.FocusOutCheckMethod")));
            this.TORIHIKISAKI_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TORIHIKISAKI_CD.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_CD.GetCodeMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TORIHIKISAKI_CD.IsInputErrorOccured = false;
            this.TORIHIKISAKI_CD.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_CD.Location = new System.Drawing.Point(116, 91);
            this.TORIHIKISAKI_CD.MaxLength = 6;
            this.TORIHIKISAKI_CD.Name = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.PopupAfterExecute = null;
            this.TORIHIKISAKI_CD.PopupAfterExecuteMethod = "TORIHIKISAKI_CD_PopupAfter";
            this.TORIHIKISAKI_CD.PopupBeforeExecute = null;
            this.TORIHIKISAKI_CD.PopupGetMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_CD.PopupSearchSendParams")));
            this.TORIHIKISAKI_CD.PopupSetFormField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TORIHIKISAKI_CD.PopupWindowName = "検索共通ポップアップ";
            this.TORIHIKISAKI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_CD.popupWindowSetting")));
            this.TORIHIKISAKI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.RegistCheckMethod")));
            this.TORIHIKISAKI_CD.SetFormField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.Size = new System.Drawing.Size(50, 20);
            this.TORIHIKISAKI_CD.TabIndex = 9;
            this.TORIHIKISAKI_CD.Tag = "取引先を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.TORIHIKISAKI_CD.ZeroPaddengFlag = true;
            // 
            // TORIHIKISAKI_NAME_RYAKU
            // 
            this.TORIHIKISAKI_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.TORIHIKISAKI_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.TORIHIKISAKI_NAME_RYAKU.DBFieldsName = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_NAME_RYAKU.DisplayPopUp = null;
            this.TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TORIHIKISAKI_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_NAME_RYAKU.IsInputErrorOccured = false;
            this.TORIHIKISAKI_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_NAME_RYAKU.Location = new System.Drawing.Point(165, 91);
            this.TORIHIKISAKI_NAME_RYAKU.MaxLength = 0;
            this.TORIHIKISAKI_NAME_RYAKU.Name = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_RYAKU.PopupAfterExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU.PopupBeforeExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams")));
            this.TORIHIKISAKI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.popupWindowSetting")));
            this.TORIHIKISAKI_NAME_RYAKU.ReadOnly = true;
            this.TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU.Size = new System.Drawing.Size(290, 20);
            this.TORIHIKISAKI_NAME_RYAKU.TabIndex = 10;
            this.TORIHIKISAKI_NAME_RYAKU.TabStop = false;
            // 
            // lblTorihikisaki
            // 
            this.lblTorihikisaki.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblTorihikisaki.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTorihikisaki.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTorihikisaki.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTorihikisaki.ForeColor = System.Drawing.Color.White;
            this.lblTorihikisaki.Location = new System.Drawing.Point(1, 91);
            this.lblTorihikisaki.Name = "lblTorihikisaki";
            this.lblTorihikisaki.Size = new System.Drawing.Size(110, 20);
            this.lblTorihikisaki.TabIndex = 50;
            this.lblTorihikisaki.Text = "取引先";
            this.lblTorihikisaki.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HIDUKE_FROM
            // 
            this.HIDUKE_FROM.BackColor = System.Drawing.SystemColors.Window;
            this.HIDUKE_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HIDUKE_FROM.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.HIDUKE_FROM.Checked = false;
            this.HIDUKE_FROM.CustomFormat = "yyyy/MM/dd(ddd)";
            this.HIDUKE_FROM.DateTimeNowYear = "";
            this.HIDUKE_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.HIDUKE_FROM.DisplayItemName = "開始日付";
            this.HIDUKE_FROM.DisplayPopUp = null;
            this.HIDUKE_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_FROM.FocusOutCheckMethod")));
            this.HIDUKE_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HIDUKE_FROM.ForeColor = System.Drawing.Color.Black;
            this.HIDUKE_FROM.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.HIDUKE_FROM.IsInputErrorOccured = false;
            this.HIDUKE_FROM.Location = new System.Drawing.Point(116, 3);
            this.HIDUKE_FROM.MaxLength = 10;
            this.HIDUKE_FROM.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.HIDUKE_FROM.Name = "HIDUKE_FROM";
            this.HIDUKE_FROM.NullValue = "";
            this.HIDUKE_FROM.PopupAfterExecute = null;
            this.HIDUKE_FROM.PopupBeforeExecute = null;
            this.HIDUKE_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIDUKE_FROM.PopupSearchSendParams")));
            this.HIDUKE_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIDUKE_FROM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HIDUKE_FROM.popupWindowSetting")));
            this.HIDUKE_FROM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_FROM.RegistCheckMethod")));
            this.HIDUKE_FROM.Size = new System.Drawing.Size(138, 20);
            this.HIDUKE_FROM.TabIndex = 0;
            this.HIDUKE_FROM.Tag = "日付を選択してください";
            this.HIDUKE_FROM.Text = "2013/10/31(木)";
            this.HIDUKE_FROM.Value = new System.DateTime(2013, 10, 31, 0, 0, 0, 0);
            this.HIDUKE_FROM.Leave += new System.EventHandler(this.HIDUKE_FROM_Leave);
            // 
            // HIDUKE_TO
            // 
            this.HIDUKE_TO.BackColor = System.Drawing.SystemColors.Window;
            this.HIDUKE_TO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HIDUKE_TO.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.HIDUKE_TO.Checked = false;
            this.HIDUKE_TO.CustomFormat = "yyyy/MM/dd(ddd)";
            this.HIDUKE_TO.DateTimeNowYear = "";
            this.HIDUKE_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.HIDUKE_TO.DisplayItemName = "終了日付";
            this.HIDUKE_TO.DisplayPopUp = null;
            this.HIDUKE_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_TO.FocusOutCheckMethod")));
            this.HIDUKE_TO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HIDUKE_TO.ForeColor = System.Drawing.Color.Black;
            this.HIDUKE_TO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.HIDUKE_TO.IsInputErrorOccured = false;
            this.HIDUKE_TO.Location = new System.Drawing.Point(274, 3);
            this.HIDUKE_TO.MaxLength = 10;
            this.HIDUKE_TO.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.HIDUKE_TO.Name = "HIDUKE_TO";
            this.HIDUKE_TO.NullValue = "";
            this.HIDUKE_TO.PopupAfterExecute = null;
            this.HIDUKE_TO.PopupBeforeExecute = null;
            this.HIDUKE_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIDUKE_TO.PopupSearchSendParams")));
            this.HIDUKE_TO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIDUKE_TO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HIDUKE_TO.popupWindowSetting")));
            this.HIDUKE_TO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_TO.RegistCheckMethod")));
            this.HIDUKE_TO.Size = new System.Drawing.Size(138, 20);
            this.HIDUKE_TO.TabIndex = 1;
            this.HIDUKE_TO.Tag = "日付を選択してください";
            this.HIDUKE_TO.Text = "2013/10/31(木)";
            this.HIDUKE_TO.Value = new System.DateTime(2013, 10, 31, 0, 0, 0, 0);
            this.HIDUKE_TO.Leave += new System.EventHandler(this.HIDUKE_TO_Leave);
            this.HIDUKE_TO.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.HIDUKE_TO_MouseDoubleClick);
            // 
            // lblHiduke
            // 
            this.lblHiduke.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblHiduke.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHiduke.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblHiduke.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblHiduke.ForeColor = System.Drawing.Color.White;
            this.lblHiduke.Location = new System.Drawing.Point(1, 3);
            this.lblHiduke.Name = "lblHiduke";
            this.lblHiduke.Size = new System.Drawing.Size(110, 20);
            this.lblHiduke.TabIndex = 46;
            this.lblHiduke.Text = "伝票日付※";
            this.lblHiduke.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(254, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 399;
            this.label2.Text = "～";
            // 
            // KYOTEN_NAME_RYAKU
            // 
            this.KYOTEN_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KYOTEN_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_NAME_RYAKU.DisplayPopUp = null;
            this.KYOTEN_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.FocusOutCheckMethod")));
            this.KYOTEN_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_NAME_RYAKU.GetCodeMasterField = "";
            this.KYOTEN_NAME_RYAKU.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.KYOTEN_NAME_RYAKU.IsInputErrorOccured = false;
            this.KYOTEN_NAME_RYAKU.ItemDefinedTypes = "";
            this.KYOTEN_NAME_RYAKU.Location = new System.Drawing.Point(165, 25);
            this.KYOTEN_NAME_RYAKU.Name = "KYOTEN_NAME_RYAKU";
            this.KYOTEN_NAME_RYAKU.PopupAfterExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupBeforeExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupGetMasterField = "";
            this.KYOTEN_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.PopupSearchSendParams")));
            this.KYOTEN_NAME_RYAKU.PopupSetFormField = "";
            this.KYOTEN_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOTEN_NAME_RYAKU.PopupWindowName = "";
            this.KYOTEN_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.popupWindowSetting")));
            this.KYOTEN_NAME_RYAKU.ReadOnly = true;
            this.KYOTEN_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.RegistCheckMethod")));
            this.KYOTEN_NAME_RYAKU.SetFormField = "";
            this.KYOTEN_NAME_RYAKU.ShortItemName = "";
            this.KYOTEN_NAME_RYAKU.Size = new System.Drawing.Size(290, 20);
            this.KYOTEN_NAME_RYAKU.TabIndex = 3;
            this.KYOTEN_NAME_RYAKU.TabStop = false;
            this.KYOTEN_NAME_RYAKU.Tag = "検索する文字を入力してください";
            // 
            // KYOTEN_CD
            // 
            this.KYOTEN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.KYOTEN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_CD.CustomFormatSetting = "00";
            this.KYOTEN_CD.DBFieldsName = "";
            this.KYOTEN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_CD.DisplayItemName = "拠点CD";
            this.KYOTEN_CD.DisplayPopUp = null;
            this.KYOTEN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.FocusOutCheckMethod")));
            this.KYOTEN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_CD.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_CD.FormatSetting = "カスタム";
            this.KYOTEN_CD.GetCodeMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.IsInputErrorOccured = false;
            this.KYOTEN_CD.ItemDefinedTypes = "smallint";
            this.KYOTEN_CD.Location = new System.Drawing.Point(116, 25);
            this.KYOTEN_CD.Name = "KYOTEN_CD";
            this.KYOTEN_CD.PopupAfterExecute = null;
            this.KYOTEN_CD.PopupBeforeExecute = null;
            this.KYOTEN_CD.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_CD.PopupSearchSendParams")));
            this.KYOTEN_CD.PopupSetFormField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
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
            this.KYOTEN_CD.SetFormField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.Size = new System.Drawing.Size(50, 20);
            this.KYOTEN_CD.TabIndex = 2;
            this.KYOTEN_CD.Tag = "半角2桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.KYOTEN_CD.WordWrap = false;
            // 
            // lblKyoten
            // 
            this.lblKyoten.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblKyoten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKyoten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblKyoten.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblKyoten.ForeColor = System.Drawing.Color.White;
            this.lblKyoten.Location = new System.Drawing.Point(1, 25);
            this.lblKyoten.Name = "lblKyoten";
            this.lblKyoten.Size = new System.Drawing.Size(110, 20);
            this.lblKyoten.TabIndex = 47;
            this.lblKyoten.Text = "拠点※";
            this.lblKyoten.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnKyotenSrch
            // 
            this.btnKyotenSrch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnKyotenSrch.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.btnKyotenSrch.DBFieldsName = null;
            this.btnKyotenSrch.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnKyotenSrch.DisplayItemName = null;
            this.btnKyotenSrch.DisplayPopUp = null;
            this.btnKyotenSrch.ErrorMessage = null;
            this.btnKyotenSrch.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnKyotenSrch.FocusOutCheckMethod")));
            this.btnKyotenSrch.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.btnKyotenSrch.GetCodeMasterField = null;
            this.btnKyotenSrch.Image = ((System.Drawing.Image)(resources.GetObject("btnKyotenSrch.Image")));
            this.btnKyotenSrch.ItemDefinedTypes = null;
            this.btnKyotenSrch.LinkedSettingTextBox = null;
            this.btnKyotenSrch.LinkedTextBoxs = null;
            this.btnKyotenSrch.Location = new System.Drawing.Point(455, 24);
            this.btnKyotenSrch.Name = "btnKyotenSrch";
            this.btnKyotenSrch.PopupAfterExecute = null;
            this.btnKyotenSrch.PopupAfterExecuteMethod = "";
            this.btnKyotenSrch.PopupBeforeExecute = null;
            this.btnKyotenSrch.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.btnKyotenSrch.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("btnKyotenSrch.PopupSearchSendParams")));
            this.btnKyotenSrch.PopupSetFormField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.btnKyotenSrch.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.btnKyotenSrch.PopupWindowName = "マスタ共通ポップアップ";
            this.btnKyotenSrch.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("btnKyotenSrch.popupWindowSetting")));
            this.btnKyotenSrch.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnKyotenSrch.RegistCheckMethod")));
            this.btnKyotenSrch.SearchDisplayFlag = 0;
            this.btnKyotenSrch.SetFormField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.btnKyotenSrch.ShortItemName = null;
            this.btnKyotenSrch.Size = new System.Drawing.Size(22, 22);
            this.btnKyotenSrch.TabIndex = 4;
            this.btnKyotenSrch.TabStop = false;
            this.btnKyotenSrch.Tag = "";
            this.btnKyotenSrch.UseVisualStyleBackColor = false;
            this.btnKyotenSrch.ZeroPaddengFlag = false;
            // 
            // txtKakuteiKbn
            // 
            this.txtKakuteiKbn.BackColor = System.Drawing.SystemColors.Window;
            this.txtKakuteiKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKakuteiKbn.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtKakuteiKbn.DisplayItemName = "確定区分";
            this.txtKakuteiKbn.DisplayPopUp = null;
            this.txtKakuteiKbn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKakuteiKbn.FocusOutCheckMethod")));
            this.txtKakuteiKbn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtKakuteiKbn.ForeColor = System.Drawing.Color.Black;
            this.txtKakuteiKbn.IsInputErrorOccured = false;
            this.txtKakuteiKbn.LinkedRadioButtonArray = new string[] {
        "rdoKakuteiKbn1",
        "rdoKakuteiKbn2",
        "rdoKakuteiKbn3"};
            this.txtKakuteiKbn.Location = new System.Drawing.Point(116, 47);
            this.txtKakuteiKbn.Name = "txtKakuteiKbn";
            this.txtKakuteiKbn.PopupAfterExecute = null;
            this.txtKakuteiKbn.PopupBeforeExecute = null;
            this.txtKakuteiKbn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtKakuteiKbn.PopupSearchSendParams")));
            this.txtKakuteiKbn.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtKakuteiKbn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtKakuteiKbn.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            3,
            0,
            0,
            0});
            rangeSettingDto2.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtKakuteiKbn.RangeSetting = rangeSettingDto2;
            this.txtKakuteiKbn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKakuteiKbn.RegistCheckMethod")));
            this.txtKakuteiKbn.ShortItemName = "確定区分";
            this.txtKakuteiKbn.Size = new System.Drawing.Size(20, 20);
            this.txtKakuteiKbn.TabIndex = 5;
            this.txtKakuteiKbn.Tag = "【1～3】のいずれかで入力してください";
            this.txtKakuteiKbn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtKakuteiKbn.WordWrap = false;
            // 
            // pnlDenpyouDateKbn
            // 
            this.pnlDenpyouDateKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDenpyouDateKbn.Controls.Add(this.rdoKakuteiKbn3);
            this.pnlDenpyouDateKbn.Controls.Add(this.rdoKakuteiKbn2);
            this.pnlDenpyouDateKbn.Controls.Add(this.rdoKakuteiKbn1);
            this.pnlDenpyouDateKbn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.pnlDenpyouDateKbn.Location = new System.Drawing.Point(135, 47);
            this.pnlDenpyouDateKbn.Name = "pnlDenpyouDateKbn";
            this.pnlDenpyouDateKbn.Size = new System.Drawing.Size(320, 20);
            this.pnlDenpyouDateKbn.TabIndex = 6;
            this.pnlDenpyouDateKbn.TabStop = true;
            // 
            // rdoKakuteiKbn3
            // 
            this.rdoKakuteiKbn3.AutoSize = true;
            this.rdoKakuteiKbn3.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoKakuteiKbn3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoKakuteiKbn3.FocusOutCheckMethod")));
            this.rdoKakuteiKbn3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoKakuteiKbn3.LinkedTextBox = "txtKakuteiKbn";
            this.rdoKakuteiKbn3.Location = new System.Drawing.Point(207, 0);
            this.rdoKakuteiKbn3.Name = "rdoKakuteiKbn3";
            this.rdoKakuteiKbn3.PopupAfterExecute = null;
            this.rdoKakuteiKbn3.PopupBeforeExecute = null;
            this.rdoKakuteiKbn3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoKakuteiKbn3.PopupSearchSendParams")));
            this.rdoKakuteiKbn3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoKakuteiKbn3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoKakuteiKbn3.popupWindowSetting")));
            this.rdoKakuteiKbn3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoKakuteiKbn3.RegistCheckMethod")));
            this.rdoKakuteiKbn3.Size = new System.Drawing.Size(81, 17);
            this.rdoKakuteiKbn3.TabIndex = 6;
            this.rdoKakuteiKbn3.Tag = "全てを抽出したい場合にはチェックを付けてください";
            this.rdoKakuteiKbn3.Text = "3.すべて";
            this.rdoKakuteiKbn3.UseVisualStyleBackColor = true;
            this.rdoKakuteiKbn3.Value = "3";
            // 
            // rdoKakuteiKbn2
            // 
            this.rdoKakuteiKbn2.AutoSize = true;
            this.rdoKakuteiKbn2.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoKakuteiKbn2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoKakuteiKbn2.FocusOutCheckMethod")));
            this.rdoKakuteiKbn2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoKakuteiKbn2.LinkedTextBox = "txtKakuteiKbn";
            this.rdoKakuteiKbn2.Location = new System.Drawing.Point(101, 0);
            this.rdoKakuteiKbn2.Name = "rdoKakuteiKbn2";
            this.rdoKakuteiKbn2.PopupAfterExecute = null;
            this.rdoKakuteiKbn2.PopupBeforeExecute = null;
            this.rdoKakuteiKbn2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoKakuteiKbn2.PopupSearchSendParams")));
            this.rdoKakuteiKbn2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoKakuteiKbn2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoKakuteiKbn2.popupWindowSetting")));
            this.rdoKakuteiKbn2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoKakuteiKbn2.RegistCheckMethod")));
            this.rdoKakuteiKbn2.Size = new System.Drawing.Size(81, 17);
            this.rdoKakuteiKbn2.TabIndex = 5;
            this.rdoKakuteiKbn2.Tag = "確定区分が「未確定」の場合にはチェックを付けてください";
            this.rdoKakuteiKbn2.Text = "2.未確定";
            this.rdoKakuteiKbn2.UseVisualStyleBackColor = true;
            this.rdoKakuteiKbn2.Value = "2";
            // 
            // rdoKakuteiKbn1
            // 
            this.rdoKakuteiKbn1.AutoSize = true;
            this.rdoKakuteiKbn1.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoKakuteiKbn1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoKakuteiKbn1.FocusOutCheckMethod")));
            this.rdoKakuteiKbn1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoKakuteiKbn1.LinkedTextBox = "txtKakuteiKbn";
            this.rdoKakuteiKbn1.Location = new System.Drawing.Point(6, 0);
            this.rdoKakuteiKbn1.Name = "rdoKakuteiKbn1";
            this.rdoKakuteiKbn1.PopupAfterExecute = null;
            this.rdoKakuteiKbn1.PopupBeforeExecute = null;
            this.rdoKakuteiKbn1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoKakuteiKbn1.PopupSearchSendParams")));
            this.rdoKakuteiKbn1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoKakuteiKbn1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoKakuteiKbn1.popupWindowSetting")));
            this.rdoKakuteiKbn1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoKakuteiKbn1.RegistCheckMethod")));
            this.rdoKakuteiKbn1.Size = new System.Drawing.Size(67, 17);
            this.rdoKakuteiKbn1.TabIndex = 4;
            this.rdoKakuteiKbn1.Tag = "確定区分が「確定」の場合にはチェックを付けてください";
            this.rdoKakuteiKbn1.Text = "1.確定";
            this.rdoKakuteiKbn1.UseVisualStyleBackColor = true;
            this.rdoKakuteiKbn1.Value = "1";
            // 
            // lblKakutei
            // 
            this.lblKakutei.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblKakutei.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKakutei.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblKakutei.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblKakutei.ForeColor = System.Drawing.Color.White;
            this.lblKakutei.Location = new System.Drawing.Point(1, 47);
            this.lblKakutei.Name = "lblKakutei";
            this.lblKakutei.Size = new System.Drawing.Size(110, 20);
            this.lblKakutei.TabIndex = 48;
            this.lblKakutei.Text = "確定区分※";
            this.lblKakutei.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDenshuKbn
            // 
            this.txtDenshuKbn.BackColor = System.Drawing.SystemColors.Window;
            this.txtDenshuKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDenshuKbn.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtDenshuKbn.DisplayItemName = "伝種";
            this.txtDenshuKbn.DisplayPopUp = null;
            this.txtDenshuKbn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtDenshuKbn.FocusOutCheckMethod")));
            this.txtDenshuKbn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtDenshuKbn.ForeColor = System.Drawing.Color.Black;
            this.txtDenshuKbn.IsInputErrorOccured = false;
            this.txtDenshuKbn.LinkedRadioButtonArray = new string[] {
        "rdoDenshuKbn1",
        "rdoDenshuKbn2",
        "rdoDenshuKbn3"};
            this.txtDenshuKbn.Location = new System.Drawing.Point(116, 69);
            this.txtDenshuKbn.Name = "txtDenshuKbn";
            this.txtDenshuKbn.PopupAfterExecute = null;
            this.txtDenshuKbn.PopupBeforeExecute = null;
            this.txtDenshuKbn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtDenshuKbn.PopupSearchSendParams")));
            this.txtDenshuKbn.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtDenshuKbn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtDenshuKbn.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            3,
            0,
            0,
            0});
            rangeSettingDto3.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtDenshuKbn.RangeSetting = rangeSettingDto3;
            this.txtDenshuKbn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtDenshuKbn.RegistCheckMethod")));
            this.txtDenshuKbn.ShortItemName = "伝種";
            this.txtDenshuKbn.Size = new System.Drawing.Size(20, 20);
            this.txtDenshuKbn.TabIndex = 7;
            this.txtDenshuKbn.Tag = "【1～3】のいずれかで入力してください";
            this.txtDenshuKbn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDenshuKbn.WordWrap = false;
            // 
            // customPanel1
            // 
            this.customPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel1.Controls.Add(this.rdoDenshuKbn3);
            this.customPanel1.Controls.Add(this.rdoDenshuKbn2);
            this.customPanel1.Controls.Add(this.rdoDenshuKbn1);
            this.customPanel1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.customPanel1.Location = new System.Drawing.Point(135, 69);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(320, 20);
            this.customPanel1.TabIndex = 8;
            this.customPanel1.TabStop = true;
            // 
            // rdoDenshuKbn3
            // 
            this.rdoDenshuKbn3.AutoSize = true;
            this.rdoDenshuKbn3.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoDenshuKbn3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoDenshuKbn3.FocusOutCheckMethod")));
            this.rdoDenshuKbn3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoDenshuKbn3.LinkedTextBox = "txtDenshuKbn";
            this.rdoDenshuKbn3.Location = new System.Drawing.Point(207, 0);
            this.rdoDenshuKbn3.Name = "rdoDenshuKbn3";
            this.rdoDenshuKbn3.PopupAfterExecute = null;
            this.rdoDenshuKbn3.PopupBeforeExecute = null;
            this.rdoDenshuKbn3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoDenshuKbn3.PopupSearchSendParams")));
            this.rdoDenshuKbn3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoDenshuKbn3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoDenshuKbn3.popupWindowSetting")));
            this.rdoDenshuKbn3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoDenshuKbn3.RegistCheckMethod")));
            this.rdoDenshuKbn3.Size = new System.Drawing.Size(95, 17);
            this.rdoDenshuKbn3.TabIndex = 7;
            this.rdoDenshuKbn3.Tag = "伝種が売上支払の場合にはチェックを付けてください";
            this.rdoDenshuKbn3.Text = "3.売上支払";
            this.rdoDenshuKbn3.UseVisualStyleBackColor = true;
            this.rdoDenshuKbn3.Value = "3";
            // 
            // rdoDenshuKbn2
            // 
            this.rdoDenshuKbn2.AutoSize = true;
            this.rdoDenshuKbn2.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoDenshuKbn2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoDenshuKbn2.FocusOutCheckMethod")));
            this.rdoDenshuKbn2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoDenshuKbn2.LinkedTextBox = "txtDenshuKbn";
            this.rdoDenshuKbn2.Location = new System.Drawing.Point(101, 0);
            this.rdoDenshuKbn2.Name = "rdoDenshuKbn2";
            this.rdoDenshuKbn2.PopupAfterExecute = null;
            this.rdoDenshuKbn2.PopupBeforeExecute = null;
            this.rdoDenshuKbn2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoDenshuKbn2.PopupSearchSendParams")));
            this.rdoDenshuKbn2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoDenshuKbn2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoDenshuKbn2.popupWindowSetting")));
            this.rdoDenshuKbn2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoDenshuKbn2.RegistCheckMethod")));
            this.rdoDenshuKbn2.Size = new System.Drawing.Size(67, 17);
            this.rdoDenshuKbn2.TabIndex = 5;
            this.rdoDenshuKbn2.Tag = "伝種が出荷の場合にはチェックを付けてください";
            this.rdoDenshuKbn2.Text = "2.出荷";
            this.rdoDenshuKbn2.UseVisualStyleBackColor = true;
            this.rdoDenshuKbn2.Value = "2";
            // 
            // rdoDenshuKbn1
            // 
            this.rdoDenshuKbn1.AutoSize = true;
            this.rdoDenshuKbn1.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoDenshuKbn1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoDenshuKbn1.FocusOutCheckMethod")));
            this.rdoDenshuKbn1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoDenshuKbn1.LinkedTextBox = "txtDenshuKbn";
            this.rdoDenshuKbn1.Location = new System.Drawing.Point(6, 0);
            this.rdoDenshuKbn1.Name = "rdoDenshuKbn1";
            this.rdoDenshuKbn1.PopupAfterExecute = null;
            this.rdoDenshuKbn1.PopupBeforeExecute = null;
            this.rdoDenshuKbn1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoDenshuKbn1.PopupSearchSendParams")));
            this.rdoDenshuKbn1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoDenshuKbn1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoDenshuKbn1.popupWindowSetting")));
            this.rdoDenshuKbn1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoDenshuKbn1.RegistCheckMethod")));
            this.rdoDenshuKbn1.Size = new System.Drawing.Size(67, 17);
            this.rdoDenshuKbn1.TabIndex = 4;
            this.rdoDenshuKbn1.Tag = "伝種が受入の場合にはチェックを付けてください";
            this.rdoDenshuKbn1.Text = "1.受入";
            this.rdoDenshuKbn1.UseVisualStyleBackColor = true;
            this.rdoDenshuKbn1.Value = "1";
            // 
            // lblDenshu
            // 
            this.lblDenshu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblDenshu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDenshu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDenshu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblDenshu.ForeColor = System.Drawing.Color.White;
            this.lblDenshu.Location = new System.Drawing.Point(1, 69);
            this.lblDenshu.Name = "lblDenshu";
            this.lblDenshu.Size = new System.Drawing.Size(110, 20);
            this.lblDenshu.TabIndex = 49;
            this.lblDenshu.Text = "伝種※";
            this.lblDenshu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnUpnGyoushaSrch
            // 
            this.btnUpnGyoushaSrch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnUpnGyoushaSrch.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.btnUpnGyoushaSrch.DBFieldsName = null;
            this.btnUpnGyoushaSrch.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnUpnGyoushaSrch.DisplayItemName = null;
            this.btnUpnGyoushaSrch.DisplayPopUp = null;
            this.btnUpnGyoushaSrch.ErrorMessage = null;
            this.btnUpnGyoushaSrch.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnUpnGyoushaSrch.FocusOutCheckMethod")));
            this.btnUpnGyoushaSrch.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.btnUpnGyoushaSrch.GetCodeMasterField = null;
            this.btnUpnGyoushaSrch.Image = ((System.Drawing.Image)(resources.GetObject("btnUpnGyoushaSrch.Image")));
            this.btnUpnGyoushaSrch.ItemDefinedTypes = null;
            this.btnUpnGyoushaSrch.LinkedSettingTextBox = null;
            this.btnUpnGyoushaSrch.LinkedTextBoxs = null;
            this.btnUpnGyoushaSrch.Location = new System.Drawing.Point(455, 156);
            this.btnUpnGyoushaSrch.Name = "btnUpnGyoushaSrch";
            this.btnUpnGyoushaSrch.PopupAfterExecute = null;
            this.btnUpnGyoushaSrch.PopupAfterExecuteMethod = "UPN_GYOUSHA_CD_PopupAfter";
            this.btnUpnGyoushaSrch.PopupBeforeExecute = null;
            this.btnUpnGyoushaSrch.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU,TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.btnUpnGyoushaSrch.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("btnUpnGyoushaSrch.PopupSearchSendParams")));
            this.btnUpnGyoushaSrch.PopupSetFormField = "UPN_GYOUSHA_CD, UPN_GYOUSHA_NAME_RYAKU";
            this.btnUpnGyoushaSrch.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.btnUpnGyoushaSrch.PopupWindowName = "検索共通ポップアップ";
            this.btnUpnGyoushaSrch.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("btnUpnGyoushaSrch.popupWindowSetting")));
            this.btnUpnGyoushaSrch.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnUpnGyoushaSrch.RegistCheckMethod")));
            this.btnUpnGyoushaSrch.SearchDisplayFlag = 0;
            this.btnUpnGyoushaSrch.SetFormField = "";
            this.btnUpnGyoushaSrch.ShortItemName = null;
            this.btnUpnGyoushaSrch.Size = new System.Drawing.Size(22, 22);
            this.btnUpnGyoushaSrch.TabIndex = 20;
            this.btnUpnGyoushaSrch.TabStop = false;
            this.btnUpnGyoushaSrch.Tag = "";
            this.btnUpnGyoushaSrch.UseVisualStyleBackColor = false;
            this.btnUpnGyoushaSrch.ZeroPaddengFlag = false;
            // 
            // UPN_GYOUSHA_CD
            // 
            this.UPN_GYOUSHA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.UPN_GYOUSHA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UPN_GYOUSHA_CD.ChangeUpperCase = true;
            this.UPN_GYOUSHA_CD.CharacterLimitList = null;
            this.UPN_GYOUSHA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.UPN_GYOUSHA_CD.DBFieldsName = "";
            this.UPN_GYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.UPN_GYOUSHA_CD.DisplayItemName = "運搬業者";
            this.UPN_GYOUSHA_CD.DisplayPopUp = null;
            this.UPN_GYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPN_GYOUSHA_CD.FocusOutCheckMethod")));
            this.UPN_GYOUSHA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.UPN_GYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.UPN_GYOUSHA_CD.GetCodeMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.UPN_GYOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.UPN_GYOUSHA_CD.IsInputErrorOccured = false;
            this.UPN_GYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.UPN_GYOUSHA_CD.Location = new System.Drawing.Point(116, 157);
            this.UPN_GYOUSHA_CD.MaxLength = 6;
            this.UPN_GYOUSHA_CD.Name = "UPN_GYOUSHA_CD";
            this.UPN_GYOUSHA_CD.PopupAfterExecute = null;
            this.UPN_GYOUSHA_CD.PopupAfterExecuteMethod = "UPN_GYOUSHA_CD_PopupAfter";
            this.UPN_GYOUSHA_CD.PopupBeforeExecute = null;
            this.UPN_GYOUSHA_CD.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.UPN_GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPN_GYOUSHA_CD.PopupSearchSendParams")));
            this.UPN_GYOUSHA_CD.PopupSetFormField = "UPN_GYOUSHA_CD, UPN_GYOUSHA_NAME_RYAKU";
            this.UPN_GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.UPN_GYOUSHA_CD.PopupWindowName = "検索共通ポップアップ";
            this.UPN_GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPN_GYOUSHA_CD.popupWindowSetting")));
            this.UPN_GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPN_GYOUSHA_CD.RegistCheckMethod")));
            this.UPN_GYOUSHA_CD.SetFormField = "UPN_GYOUSHA_CD, UPN_GYOUSHA_NAME_RYAKU";
            this.UPN_GYOUSHA_CD.Size = new System.Drawing.Size(50, 20);
            this.UPN_GYOUSHA_CD.TabIndex = 18;
            this.UPN_GYOUSHA_CD.Tag = "業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.UPN_GYOUSHA_CD.ZeroPaddengFlag = true;
            // 
            // UPN_GYOUSHA_NAME_RYAKU
            // 
            this.UPN_GYOUSHA_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.UPN_GYOUSHA_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UPN_GYOUSHA_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.UPN_GYOUSHA_NAME_RYAKU.DBFieldsName = "GYOUSHA_NAME_RYAKU";
            this.UPN_GYOUSHA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.UPN_GYOUSHA_NAME_RYAKU.DisplayPopUp = null;
            this.UPN_GYOUSHA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPN_GYOUSHA_NAME_RYAKU.FocusOutCheckMethod")));
            this.UPN_GYOUSHA_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.UPN_GYOUSHA_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.UPN_GYOUSHA_NAME_RYAKU.IsInputErrorOccured = false;
            this.UPN_GYOUSHA_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.UPN_GYOUSHA_NAME_RYAKU.Location = new System.Drawing.Point(165, 157);
            this.UPN_GYOUSHA_NAME_RYAKU.MaxLength = 0;
            this.UPN_GYOUSHA_NAME_RYAKU.Name = "UPN_GYOUSHA_NAME_RYAKU";
            this.UPN_GYOUSHA_NAME_RYAKU.PopupAfterExecute = null;
            this.UPN_GYOUSHA_NAME_RYAKU.PopupBeforeExecute = null;
            this.UPN_GYOUSHA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPN_GYOUSHA_NAME_RYAKU.PopupSearchSendParams")));
            this.UPN_GYOUSHA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPN_GYOUSHA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPN_GYOUSHA_NAME_RYAKU.popupWindowSetting")));
            this.UPN_GYOUSHA_NAME_RYAKU.ReadOnly = true;
            this.UPN_GYOUSHA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPN_GYOUSHA_NAME_RYAKU.RegistCheckMethod")));
            this.UPN_GYOUSHA_NAME_RYAKU.Size = new System.Drawing.Size(290, 20);
            this.UPN_GYOUSHA_NAME_RYAKU.TabIndex = 19;
            this.UPN_GYOUSHA_NAME_RYAKU.TabStop = false;
            // 
            // lblUpnGyousha
            // 
            this.lblUpnGyousha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblUpnGyousha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUpnGyousha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblUpnGyousha.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblUpnGyousha.ForeColor = System.Drawing.Color.White;
            this.lblUpnGyousha.Location = new System.Drawing.Point(1, 157);
            this.lblUpnGyousha.Name = "lblUpnGyousha";
            this.lblUpnGyousha.Size = new System.Drawing.Size(110, 20);
            this.lblUpnGyousha.TabIndex = 53;
            this.lblUpnGyousha.Text = "運搬業者";
            this.lblUpnGyousha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNizumiGenbaSrch
            // 
            this.btnNizumiGenbaSrch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnNizumiGenbaSrch.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.btnNizumiGenbaSrch.DBFieldsName = null;
            this.btnNizumiGenbaSrch.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnNizumiGenbaSrch.DisplayItemName = null;
            this.btnNizumiGenbaSrch.DisplayPopUp = null;
            this.btnNizumiGenbaSrch.ErrorMessage = null;
            this.btnNizumiGenbaSrch.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnNizumiGenbaSrch.FocusOutCheckMethod")));
            this.btnNizumiGenbaSrch.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.btnNizumiGenbaSrch.GetCodeMasterField = null;
            this.btnNizumiGenbaSrch.Image = ((System.Drawing.Image)(resources.GetObject("btnNizumiGenbaSrch.Image")));
            this.btnNizumiGenbaSrch.ItemDefinedTypes = null;
            this.btnNizumiGenbaSrch.LinkedSettingTextBox = null;
            this.btnNizumiGenbaSrch.LinkedTextBoxs = null;
            this.btnNizumiGenbaSrch.Location = new System.Drawing.Point(938, 24);
            this.btnNizumiGenbaSrch.Name = "btnNizumiGenbaSrch";
            this.btnNizumiGenbaSrch.PopupAfterExecute = null;
            this.btnNizumiGenbaSrch.PopupAfterExecuteMethod = "NIZUMI_GENBA_CD_PopupAfter";
            this.btnNizumiGenbaSrch.PopupBeforeExecute = null;
            this.btnNizumiGenbaSrch.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU, GENBA_CD,GENBA_NAME_RYAKU";
            this.btnNizumiGenbaSrch.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("btnNizumiGenbaSrch.PopupSearchSendParams")));
            this.btnNizumiGenbaSrch.PopupSendParams = new string[] {
        "UKEIRE_GYOUSHA_CD,UKEIRE_GENBA_CD"};
            this.btnNizumiGenbaSrch.PopupSetFormField = "NIZUMI_GYOUSHA_CD, NIZUMI_GYOUSHA_NAME_RYAKU, NIZUMI_GENBA_CD,NIZUMI_GENBA_NAME_R" +
    "YAKU";
            this.btnNizumiGenbaSrch.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.btnNizumiGenbaSrch.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.btnNizumiGenbaSrch.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("btnNizumiGenbaSrch.popupWindowSetting")));
            this.btnNizumiGenbaSrch.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnNizumiGenbaSrch.RegistCheckMethod")));
            this.btnNizumiGenbaSrch.SearchDisplayFlag = 0;
            this.btnNizumiGenbaSrch.SetFormField = "";
            this.btnNizumiGenbaSrch.ShortItemName = null;
            this.btnNizumiGenbaSrch.Size = new System.Drawing.Size(22, 22);
            this.btnNizumiGenbaSrch.TabIndex = 26;
            this.btnNizumiGenbaSrch.TabStop = false;
            this.btnNizumiGenbaSrch.Tag = "";
            this.btnNizumiGenbaSrch.UseVisualStyleBackColor = false;
            this.btnNizumiGenbaSrch.ZeroPaddengFlag = false;
            // 
            // NIZUMI_GENBA_CD
            // 
            this.NIZUMI_GENBA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.NIZUMI_GENBA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NIZUMI_GENBA_CD.ChangeUpperCase = true;
            this.NIZUMI_GENBA_CD.CharacterLimitList = null;
            this.NIZUMI_GENBA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.NIZUMI_GENBA_CD.DBFieldsName = "";
            this.NIZUMI_GENBA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.NIZUMI_GENBA_CD.DisplayItemName = "荷積現場";
            this.NIZUMI_GENBA_CD.DisplayPopUp = null;
            this.NIZUMI_GENBA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NIZUMI_GENBA_CD.FocusOutCheckMethod")));
            this.NIZUMI_GENBA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.NIZUMI_GENBA_CD.ForeColor = System.Drawing.Color.Black;
            this.NIZUMI_GENBA_CD.GetCodeMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU, GENBA_CD,GENBA_NAME_RYAKU";
            this.NIZUMI_GENBA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.NIZUMI_GENBA_CD.IsInputErrorOccured = false;
            this.NIZUMI_GENBA_CD.ItemDefinedTypes = "varchar";
            this.NIZUMI_GENBA_CD.Location = new System.Drawing.Point(599, 25);
            this.NIZUMI_GENBA_CD.MaxLength = 6;
            this.NIZUMI_GENBA_CD.Name = "NIZUMI_GENBA_CD";
            this.NIZUMI_GENBA_CD.PopupAfterExecute = null;
            this.NIZUMI_GENBA_CD.PopupAfterExecuteMethod = "NIZUMI_GENBA_CD_PopupAfter";
            this.NIZUMI_GENBA_CD.PopupBeforeExecute = null;
            this.NIZUMI_GENBA_CD.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU, GENBA_CD,GENBA_NAME_RYAKU";
            this.NIZUMI_GENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NIZUMI_GENBA_CD.PopupSearchSendParams")));
            this.NIZUMI_GENBA_CD.PopupSendParams = new string[0];
            this.NIZUMI_GENBA_CD.PopupSetFormField = "NIZUMI_GYOUSHA_CD, NIZUMI_GYOUSHA_NAME_RYAKU, NIZUMI_GENBA_CD,NIZUMI_GENBA_NAME_R" +
    "YAKU";
            this.NIZUMI_GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.NIZUMI_GENBA_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.NIZUMI_GENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NIZUMI_GENBA_CD.popupWindowSetting")));
            this.NIZUMI_GENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NIZUMI_GENBA_CD.RegistCheckMethod")));
            this.NIZUMI_GENBA_CD.SetFormField = "NIZUMI_GYOUSHA_CD, NIZUMI_GYOUSHA_NAME_RYAKU, NIZUMI_GENBA_CD,NIZUMI_GENBA_NAME_R" +
    "YAKU";
            this.NIZUMI_GENBA_CD.Size = new System.Drawing.Size(50, 20);
            this.NIZUMI_GENBA_CD.TabIndex = 24;
            this.NIZUMI_GENBA_CD.Tag = "荷積現場を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.NIZUMI_GENBA_CD.ZeroPaddengFlag = true;
            // 
            // NIZUMI_GENBA_NAME_RYAKU
            // 
            this.NIZUMI_GENBA_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.NIZUMI_GENBA_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NIZUMI_GENBA_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.NIZUMI_GENBA_NAME_RYAKU.DBFieldsName = "GENBA_NAME_RYAKU";
            this.NIZUMI_GENBA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.NIZUMI_GENBA_NAME_RYAKU.DisplayPopUp = null;
            this.NIZUMI_GENBA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NIZUMI_GENBA_NAME_RYAKU.FocusOutCheckMethod")));
            this.NIZUMI_GENBA_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.NIZUMI_GENBA_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.NIZUMI_GENBA_NAME_RYAKU.IsInputErrorOccured = false;
            this.NIZUMI_GENBA_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.NIZUMI_GENBA_NAME_RYAKU.Location = new System.Drawing.Point(648, 25);
            this.NIZUMI_GENBA_NAME_RYAKU.MaxLength = 0;
            this.NIZUMI_GENBA_NAME_RYAKU.Name = "NIZUMI_GENBA_NAME_RYAKU";
            this.NIZUMI_GENBA_NAME_RYAKU.PopupAfterExecute = null;
            this.NIZUMI_GENBA_NAME_RYAKU.PopupBeforeExecute = null;
            this.NIZUMI_GENBA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NIZUMI_GENBA_NAME_RYAKU.PopupSearchSendParams")));
            this.NIZUMI_GENBA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NIZUMI_GENBA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NIZUMI_GENBA_NAME_RYAKU.popupWindowSetting")));
            this.NIZUMI_GENBA_NAME_RYAKU.ReadOnly = true;
            this.NIZUMI_GENBA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NIZUMI_GENBA_NAME_RYAKU.RegistCheckMethod")));
            this.NIZUMI_GENBA_NAME_RYAKU.Size = new System.Drawing.Size(290, 20);
            this.NIZUMI_GENBA_NAME_RYAKU.TabIndex = 25;
            this.NIZUMI_GENBA_NAME_RYAKU.TabStop = false;
            // 
            // lblNizumiGenba
            // 
            this.lblNizumiGenba.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblNizumiGenba.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNizumiGenba.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblNizumiGenba.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNizumiGenba.ForeColor = System.Drawing.Color.White;
            this.lblNizumiGenba.Location = new System.Drawing.Point(484, 25);
            this.lblNizumiGenba.Name = "lblNizumiGenba";
            this.lblNizumiGenba.Size = new System.Drawing.Size(110, 20);
            this.lblNizumiGenba.TabIndex = 55;
            this.lblNizumiGenba.Text = "荷積現場";
            this.lblNizumiGenba.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNizumiGyoushaSrch
            // 
            this.btnNizumiGyoushaSrch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnNizumiGyoushaSrch.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.btnNizumiGyoushaSrch.DBFieldsName = null;
            this.btnNizumiGyoushaSrch.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnNizumiGyoushaSrch.DisplayItemName = null;
            this.btnNizumiGyoushaSrch.DisplayPopUp = null;
            this.btnNizumiGyoushaSrch.ErrorMessage = null;
            this.btnNizumiGyoushaSrch.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnNizumiGyoushaSrch.FocusOutCheckMethod")));
            this.btnNizumiGyoushaSrch.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.btnNizumiGyoushaSrch.GetCodeMasterField = null;
            this.btnNizumiGyoushaSrch.Image = ((System.Drawing.Image)(resources.GetObject("btnNizumiGyoushaSrch.Image")));
            this.btnNizumiGyoushaSrch.ItemDefinedTypes = null;
            this.btnNizumiGyoushaSrch.LinkedSettingTextBox = null;
            this.btnNizumiGyoushaSrch.LinkedTextBoxs = null;
            this.btnNizumiGyoushaSrch.Location = new System.Drawing.Point(938, 2);
            this.btnNizumiGyoushaSrch.Name = "btnNizumiGyoushaSrch";
            this.btnNizumiGyoushaSrch.PopupAfterExecute = null;
            this.btnNizumiGyoushaSrch.PopupAfterExecuteMethod = "NIZUMI_GYOUSHA_CD_PopupAfter";
            this.btnNizumiGyoushaSrch.PopupBeforeExecute = null;
            this.btnNizumiGyoushaSrch.PopupBeforeExecuteMethod = "NIZUMI_GYOUSHA_CD_PopupBefore";
            this.btnNizumiGyoushaSrch.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU,TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.btnNizumiGyoushaSrch.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("btnNizumiGyoushaSrch.PopupSearchSendParams")));
            this.btnNizumiGyoushaSrch.PopupSetFormField = "NIZUMI_GYOUSHA_CD, NIZUMI_GYOUSHA_NAME_RYAKU";
            this.btnNizumiGyoushaSrch.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.btnNizumiGyoushaSrch.PopupWindowName = "検索共通ポップアップ";
            this.btnNizumiGyoushaSrch.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("btnNizumiGyoushaSrch.popupWindowSetting")));
            this.btnNizumiGyoushaSrch.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnNizumiGyoushaSrch.RegistCheckMethod")));
            this.btnNizumiGyoushaSrch.SearchDisplayFlag = 0;
            this.btnNizumiGyoushaSrch.SetFormField = "";
            this.btnNizumiGyoushaSrch.ShortItemName = null;
            this.btnNizumiGyoushaSrch.Size = new System.Drawing.Size(22, 22);
            this.btnNizumiGyoushaSrch.TabIndex = 23;
            this.btnNizumiGyoushaSrch.TabStop = false;
            this.btnNizumiGyoushaSrch.Tag = "";
            this.btnNizumiGyoushaSrch.UseVisualStyleBackColor = false;
            this.btnNizumiGyoushaSrch.ZeroPaddengFlag = false;
            // 
            // NIZUMI_GYOUSHA_CD
            // 
            this.NIZUMI_GYOUSHA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.NIZUMI_GYOUSHA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NIZUMI_GYOUSHA_CD.ChangeUpperCase = true;
            this.NIZUMI_GYOUSHA_CD.CharacterLimitList = null;
            this.NIZUMI_GYOUSHA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.NIZUMI_GYOUSHA_CD.DBFieldsName = "";
            this.NIZUMI_GYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.NIZUMI_GYOUSHA_CD.DisplayItemName = "荷積業者";
            this.NIZUMI_GYOUSHA_CD.DisplayPopUp = null;
            this.NIZUMI_GYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NIZUMI_GYOUSHA_CD.FocusOutCheckMethod")));
            this.NIZUMI_GYOUSHA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.NIZUMI_GYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.NIZUMI_GYOUSHA_CD.GetCodeMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.NIZUMI_GYOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.NIZUMI_GYOUSHA_CD.IsInputErrorOccured = false;
            this.NIZUMI_GYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.NIZUMI_GYOUSHA_CD.Location = new System.Drawing.Point(599, 3);
            this.NIZUMI_GYOUSHA_CD.MaxLength = 6;
            this.NIZUMI_GYOUSHA_CD.Name = "NIZUMI_GYOUSHA_CD";
            this.NIZUMI_GYOUSHA_CD.PopupAfterExecute = null;
            this.NIZUMI_GYOUSHA_CD.PopupAfterExecuteMethod = "NIZUMI_GYOUSHA_CD_PopupAfter";
            this.NIZUMI_GYOUSHA_CD.PopupBeforeExecute = null;
            this.NIZUMI_GYOUSHA_CD.PopupBeforeExecuteMethod = "NIZUMI_GYOUSHA_CD_PopupBefore";
            this.NIZUMI_GYOUSHA_CD.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.NIZUMI_GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NIZUMI_GYOUSHA_CD.PopupSearchSendParams")));
            this.NIZUMI_GYOUSHA_CD.PopupSetFormField = "NIZUMI_GYOUSHA_CD, NIZUMI_GYOUSHA_NAME_RYAKU";
            this.NIZUMI_GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.NIZUMI_GYOUSHA_CD.PopupWindowName = "検索共通ポップアップ";
            this.NIZUMI_GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NIZUMI_GYOUSHA_CD.popupWindowSetting")));
            this.NIZUMI_GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NIZUMI_GYOUSHA_CD.RegistCheckMethod")));
            this.NIZUMI_GYOUSHA_CD.SetFormField = "NIZUMI_GYOUSHA_CD, NIZUMI_GYOUSHA_NAME_RYAKU";
            this.NIZUMI_GYOUSHA_CD.Size = new System.Drawing.Size(50, 20);
            this.NIZUMI_GYOUSHA_CD.TabIndex = 21;
            this.NIZUMI_GYOUSHA_CD.Tag = "荷積業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.NIZUMI_GYOUSHA_CD.ZeroPaddengFlag = true;
            // 
            // NIZUMI_GYOUSHA_NAME_RYAKU
            // 
            this.NIZUMI_GYOUSHA_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.NIZUMI_GYOUSHA_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NIZUMI_GYOUSHA_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.NIZUMI_GYOUSHA_NAME_RYAKU.DBFieldsName = "GYOUSHA_NAME_RYAKU";
            this.NIZUMI_GYOUSHA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.NIZUMI_GYOUSHA_NAME_RYAKU.DisplayPopUp = null;
            this.NIZUMI_GYOUSHA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NIZUMI_GYOUSHA_NAME_RYAKU.FocusOutCheckMethod")));
            this.NIZUMI_GYOUSHA_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.NIZUMI_GYOUSHA_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.NIZUMI_GYOUSHA_NAME_RYAKU.IsInputErrorOccured = false;
            this.NIZUMI_GYOUSHA_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.NIZUMI_GYOUSHA_NAME_RYAKU.Location = new System.Drawing.Point(648, 3);
            this.NIZUMI_GYOUSHA_NAME_RYAKU.MaxLength = 0;
            this.NIZUMI_GYOUSHA_NAME_RYAKU.Name = "NIZUMI_GYOUSHA_NAME_RYAKU";
            this.NIZUMI_GYOUSHA_NAME_RYAKU.PopupAfterExecute = null;
            this.NIZUMI_GYOUSHA_NAME_RYAKU.PopupBeforeExecute = null;
            this.NIZUMI_GYOUSHA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NIZUMI_GYOUSHA_NAME_RYAKU.PopupSearchSendParams")));
            this.NIZUMI_GYOUSHA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NIZUMI_GYOUSHA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NIZUMI_GYOUSHA_NAME_RYAKU.popupWindowSetting")));
            this.NIZUMI_GYOUSHA_NAME_RYAKU.ReadOnly = true;
            this.NIZUMI_GYOUSHA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NIZUMI_GYOUSHA_NAME_RYAKU.RegistCheckMethod")));
            this.NIZUMI_GYOUSHA_NAME_RYAKU.Size = new System.Drawing.Size(290, 20);
            this.NIZUMI_GYOUSHA_NAME_RYAKU.TabIndex = 22;
            this.NIZUMI_GYOUSHA_NAME_RYAKU.TabStop = false;
            // 
            // lblNitumiGyousha
            // 
            this.lblNitumiGyousha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblNitumiGyousha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNitumiGyousha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblNitumiGyousha.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNitumiGyousha.ForeColor = System.Drawing.Color.White;
            this.lblNitumiGyousha.Location = new System.Drawing.Point(484, 3);
            this.lblNitumiGyousha.Name = "lblNitumiGyousha";
            this.lblNitumiGyousha.Size = new System.Drawing.Size(110, 20);
            this.lblNitumiGyousha.TabIndex = 54;
            this.lblNitumiGyousha.Text = "荷積業者";
            this.lblNitumiGyousha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNioroshiGenbaSrch
            // 
            this.btnNioroshiGenbaSrch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnNioroshiGenbaSrch.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.btnNioroshiGenbaSrch.DBFieldsName = null;
            this.btnNioroshiGenbaSrch.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnNioroshiGenbaSrch.DisplayItemName = null;
            this.btnNioroshiGenbaSrch.DisplayPopUp = null;
            this.btnNioroshiGenbaSrch.ErrorMessage = null;
            this.btnNioroshiGenbaSrch.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnNioroshiGenbaSrch.FocusOutCheckMethod")));
            this.btnNioroshiGenbaSrch.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.btnNioroshiGenbaSrch.GetCodeMasterField = null;
            this.btnNioroshiGenbaSrch.Image = ((System.Drawing.Image)(resources.GetObject("btnNioroshiGenbaSrch.Image")));
            this.btnNioroshiGenbaSrch.ItemDefinedTypes = null;
            this.btnNioroshiGenbaSrch.LinkedSettingTextBox = null;
            this.btnNioroshiGenbaSrch.LinkedTextBoxs = null;
            this.btnNioroshiGenbaSrch.Location = new System.Drawing.Point(938, 68);
            this.btnNioroshiGenbaSrch.Name = "btnNioroshiGenbaSrch";
            this.btnNioroshiGenbaSrch.PopupAfterExecute = null;
            this.btnNioroshiGenbaSrch.PopupAfterExecuteMethod = "NIOROSHI_GENBA_CD_PopupAfter";
            this.btnNioroshiGenbaSrch.PopupBeforeExecute = null;
            this.btnNioroshiGenbaSrch.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU, GENBA_CD,GENBA_NAME_RYAKU";
            this.btnNioroshiGenbaSrch.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("btnNioroshiGenbaSrch.PopupSearchSendParams")));
            this.btnNioroshiGenbaSrch.PopupSendParams = new string[] {
        "UKEIRE_GYOUSHA_CD,UKEIRE_GENBA_CD"};
            this.btnNioroshiGenbaSrch.PopupSetFormField = "NIOROSHI_GYOUSHA_CD, NIOROSHI_GYOUSHA_NAME_RYAKU, NIOROSHI_GENBA_CD,NIOROSHI_GENB" +
    "A_NAME_RYAKU";
            this.btnNioroshiGenbaSrch.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.btnNioroshiGenbaSrch.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.btnNioroshiGenbaSrch.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("btnNioroshiGenbaSrch.popupWindowSetting")));
            this.btnNioroshiGenbaSrch.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnNioroshiGenbaSrch.RegistCheckMethod")));
            this.btnNioroshiGenbaSrch.SearchDisplayFlag = 0;
            this.btnNioroshiGenbaSrch.SetFormField = "";
            this.btnNioroshiGenbaSrch.ShortItemName = null;
            this.btnNioroshiGenbaSrch.Size = new System.Drawing.Size(22, 22);
            this.btnNioroshiGenbaSrch.TabIndex = 32;
            this.btnNioroshiGenbaSrch.TabStop = false;
            this.btnNioroshiGenbaSrch.Tag = "";
            this.btnNioroshiGenbaSrch.UseVisualStyleBackColor = false;
            this.btnNioroshiGenbaSrch.ZeroPaddengFlag = false;
            // 
            // NIOROSHI_GENBA_CD
            // 
            this.NIOROSHI_GENBA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.NIOROSHI_GENBA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NIOROSHI_GENBA_CD.ChangeUpperCase = true;
            this.NIOROSHI_GENBA_CD.CharacterLimitList = null;
            this.NIOROSHI_GENBA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.NIOROSHI_GENBA_CD.DBFieldsName = "";
            this.NIOROSHI_GENBA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.NIOROSHI_GENBA_CD.DisplayItemName = "荷降現場";
            this.NIOROSHI_GENBA_CD.DisplayPopUp = null;
            this.NIOROSHI_GENBA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NIOROSHI_GENBA_CD.FocusOutCheckMethod")));
            this.NIOROSHI_GENBA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.NIOROSHI_GENBA_CD.ForeColor = System.Drawing.Color.Black;
            this.NIOROSHI_GENBA_CD.GetCodeMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU, GENBA_CD,GENBA_NAME_RYAKU";
            this.NIOROSHI_GENBA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.NIOROSHI_GENBA_CD.IsInputErrorOccured = false;
            this.NIOROSHI_GENBA_CD.ItemDefinedTypes = "varchar";
            this.NIOROSHI_GENBA_CD.Location = new System.Drawing.Point(599, 69);
            this.NIOROSHI_GENBA_CD.MaxLength = 6;
            this.NIOROSHI_GENBA_CD.Name = "NIOROSHI_GENBA_CD";
            this.NIOROSHI_GENBA_CD.PopupAfterExecute = null;
            this.NIOROSHI_GENBA_CD.PopupAfterExecuteMethod = "NIOROSHI_GENBA_CD_PopupAfter";
            this.NIOROSHI_GENBA_CD.PopupBeforeExecute = null;
            this.NIOROSHI_GENBA_CD.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU, GENBA_CD,GENBA_NAME_RYAKU";
            this.NIOROSHI_GENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NIOROSHI_GENBA_CD.PopupSearchSendParams")));
            this.NIOROSHI_GENBA_CD.PopupSendParams = new string[0];
            this.NIOROSHI_GENBA_CD.PopupSetFormField = "NIOROSHI_GYOUSHA_CD, NIOROSHI_GYOUSHA_NAME_RYAKU, NIOROSHI_GENBA_CD,NIOROSHI_GENB" +
    "A_NAME_RYAKU";
            this.NIOROSHI_GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.NIOROSHI_GENBA_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.NIOROSHI_GENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NIOROSHI_GENBA_CD.popupWindowSetting")));
            this.NIOROSHI_GENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NIOROSHI_GENBA_CD.RegistCheckMethod")));
            this.NIOROSHI_GENBA_CD.SetFormField = "NIOROSHI_GYOUSHA_CD, NIOROSHI_GYOUSHA_NAME_RYAKU, NIOROSHI_GENBA_CD,NIOROSHI_GENB" +
    "A_NAME_RYAKU";
            this.NIOROSHI_GENBA_CD.Size = new System.Drawing.Size(50, 20);
            this.NIOROSHI_GENBA_CD.TabIndex = 30;
            this.NIOROSHI_GENBA_CD.Tag = "荷降現場を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.NIOROSHI_GENBA_CD.ZeroPaddengFlag = true;
            // 
            // NIOROSHI_GENBA_NAME_RYAKU
            // 
            this.NIOROSHI_GENBA_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.NIOROSHI_GENBA_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NIOROSHI_GENBA_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.NIOROSHI_GENBA_NAME_RYAKU.DBFieldsName = "GENBA_NAME_RYAKU";
            this.NIOROSHI_GENBA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.NIOROSHI_GENBA_NAME_RYAKU.DisplayPopUp = null;
            this.NIOROSHI_GENBA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NIOROSHI_GENBA_NAME_RYAKU.FocusOutCheckMethod")));
            this.NIOROSHI_GENBA_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.NIOROSHI_GENBA_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.NIOROSHI_GENBA_NAME_RYAKU.IsInputErrorOccured = false;
            this.NIOROSHI_GENBA_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.NIOROSHI_GENBA_NAME_RYAKU.Location = new System.Drawing.Point(648, 69);
            this.NIOROSHI_GENBA_NAME_RYAKU.MaxLength = 0;
            this.NIOROSHI_GENBA_NAME_RYAKU.Name = "NIOROSHI_GENBA_NAME_RYAKU";
            this.NIOROSHI_GENBA_NAME_RYAKU.PopupAfterExecute = null;
            this.NIOROSHI_GENBA_NAME_RYAKU.PopupBeforeExecute = null;
            this.NIOROSHI_GENBA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NIOROSHI_GENBA_NAME_RYAKU.PopupSearchSendParams")));
            this.NIOROSHI_GENBA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NIOROSHI_GENBA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NIOROSHI_GENBA_NAME_RYAKU.popupWindowSetting")));
            this.NIOROSHI_GENBA_NAME_RYAKU.ReadOnly = true;
            this.NIOROSHI_GENBA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NIOROSHI_GENBA_NAME_RYAKU.RegistCheckMethod")));
            this.NIOROSHI_GENBA_NAME_RYAKU.Size = new System.Drawing.Size(290, 20);
            this.NIOROSHI_GENBA_NAME_RYAKU.TabIndex = 31;
            this.NIOROSHI_GENBA_NAME_RYAKU.TabStop = false;
            // 
            // lblNioroshiGenba
            // 
            this.lblNioroshiGenba.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblNioroshiGenba.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNioroshiGenba.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblNioroshiGenba.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNioroshiGenba.ForeColor = System.Drawing.Color.White;
            this.lblNioroshiGenba.Location = new System.Drawing.Point(484, 69);
            this.lblNioroshiGenba.Name = "lblNioroshiGenba";
            this.lblNioroshiGenba.Size = new System.Drawing.Size(110, 20);
            this.lblNioroshiGenba.TabIndex = 57;
            this.lblNioroshiGenba.Text = "荷降現場";
            this.lblNioroshiGenba.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNioroshiGyoushaSrch
            // 
            this.btnNioroshiGyoushaSrch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnNioroshiGyoushaSrch.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.btnNioroshiGyoushaSrch.DBFieldsName = null;
            this.btnNioroshiGyoushaSrch.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnNioroshiGyoushaSrch.DisplayItemName = null;
            this.btnNioroshiGyoushaSrch.DisplayPopUp = null;
            this.btnNioroshiGyoushaSrch.ErrorMessage = null;
            this.btnNioroshiGyoushaSrch.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnNioroshiGyoushaSrch.FocusOutCheckMethod")));
            this.btnNioroshiGyoushaSrch.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.btnNioroshiGyoushaSrch.GetCodeMasterField = null;
            this.btnNioroshiGyoushaSrch.Image = ((System.Drawing.Image)(resources.GetObject("btnNioroshiGyoushaSrch.Image")));
            this.btnNioroshiGyoushaSrch.ItemDefinedTypes = null;
            this.btnNioroshiGyoushaSrch.LinkedSettingTextBox = null;
            this.btnNioroshiGyoushaSrch.LinkedTextBoxs = null;
            this.btnNioroshiGyoushaSrch.Location = new System.Drawing.Point(938, 46);
            this.btnNioroshiGyoushaSrch.Name = "btnNioroshiGyoushaSrch";
            this.btnNioroshiGyoushaSrch.PopupAfterExecute = null;
            this.btnNioroshiGyoushaSrch.PopupAfterExecuteMethod = "NIOROSHI_GYOUSHA_CD_PopupAfter";
            this.btnNioroshiGyoushaSrch.PopupBeforeExecute = null;
            this.btnNioroshiGyoushaSrch.PopupBeforeExecuteMethod = "NIOROSHI_GYOUSHA_CD_PopupBefore";
            this.btnNioroshiGyoushaSrch.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU,TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.btnNioroshiGyoushaSrch.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("btnNioroshiGyoushaSrch.PopupSearchSendParams")));
            this.btnNioroshiGyoushaSrch.PopupSetFormField = "NIOROSHI_GYOUSHA_CD, NIOROSHI_GYOUSHA_NAME_RYAKU";
            this.btnNioroshiGyoushaSrch.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.btnNioroshiGyoushaSrch.PopupWindowName = "検索共通ポップアップ";
            this.btnNioroshiGyoushaSrch.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("btnNioroshiGyoushaSrch.popupWindowSetting")));
            this.btnNioroshiGyoushaSrch.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnNioroshiGyoushaSrch.RegistCheckMethod")));
            this.btnNioroshiGyoushaSrch.SearchDisplayFlag = 0;
            this.btnNioroshiGyoushaSrch.SetFormField = "";
            this.btnNioroshiGyoushaSrch.ShortItemName = null;
            this.btnNioroshiGyoushaSrch.Size = new System.Drawing.Size(22, 22);
            this.btnNioroshiGyoushaSrch.TabIndex = 29;
            this.btnNioroshiGyoushaSrch.TabStop = false;
            this.btnNioroshiGyoushaSrch.Tag = "";
            this.btnNioroshiGyoushaSrch.UseVisualStyleBackColor = false;
            this.btnNioroshiGyoushaSrch.ZeroPaddengFlag = false;
            // 
            // NIOROSHI_GYOUSHA_CD
            // 
            this.NIOROSHI_GYOUSHA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.NIOROSHI_GYOUSHA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NIOROSHI_GYOUSHA_CD.ChangeUpperCase = true;
            this.NIOROSHI_GYOUSHA_CD.CharacterLimitList = null;
            this.NIOROSHI_GYOUSHA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.NIOROSHI_GYOUSHA_CD.DBFieldsName = "";
            this.NIOROSHI_GYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.NIOROSHI_GYOUSHA_CD.DisplayItemName = "荷降業者";
            this.NIOROSHI_GYOUSHA_CD.DisplayPopUp = null;
            this.NIOROSHI_GYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NIOROSHI_GYOUSHA_CD.FocusOutCheckMethod")));
            this.NIOROSHI_GYOUSHA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.NIOROSHI_GYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.NIOROSHI_GYOUSHA_CD.GetCodeMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.NIOROSHI_GYOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.NIOROSHI_GYOUSHA_CD.IsInputErrorOccured = false;
            this.NIOROSHI_GYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.NIOROSHI_GYOUSHA_CD.Location = new System.Drawing.Point(599, 47);
            this.NIOROSHI_GYOUSHA_CD.MaxLength = 6;
            this.NIOROSHI_GYOUSHA_CD.Name = "NIOROSHI_GYOUSHA_CD";
            this.NIOROSHI_GYOUSHA_CD.PopupAfterExecute = null;
            this.NIOROSHI_GYOUSHA_CD.PopupAfterExecuteMethod = "NIOROSHI_GYOUSHA_CD_PopupAfter";
            this.NIOROSHI_GYOUSHA_CD.PopupBeforeExecute = null;
            this.NIOROSHI_GYOUSHA_CD.PopupBeforeExecuteMethod = "NIOROSHI_GYOUSHA_CD_PopupBefore";
            this.NIOROSHI_GYOUSHA_CD.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.NIOROSHI_GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NIOROSHI_GYOUSHA_CD.PopupSearchSendParams")));
            this.NIOROSHI_GYOUSHA_CD.PopupSetFormField = "NIOROSHI_GYOUSHA_CD, NIOROSHI_GYOUSHA_NAME_RYAKU";
            this.NIOROSHI_GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.NIOROSHI_GYOUSHA_CD.PopupWindowName = "検索共通ポップアップ";
            this.NIOROSHI_GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NIOROSHI_GYOUSHA_CD.popupWindowSetting")));
            this.NIOROSHI_GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NIOROSHI_GYOUSHA_CD.RegistCheckMethod")));
            this.NIOROSHI_GYOUSHA_CD.SetFormField = "NIOROSHI_GYOUSHA_CD, NIOROSHI_GYOUSHA_NAME_RYAKU";
            this.NIOROSHI_GYOUSHA_CD.Size = new System.Drawing.Size(50, 20);
            this.NIOROSHI_GYOUSHA_CD.TabIndex = 27;
            this.NIOROSHI_GYOUSHA_CD.Tag = "荷降業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.NIOROSHI_GYOUSHA_CD.ZeroPaddengFlag = true;
            // 
            // NIOROSHI_GYOUSHA_NAME_RYAKU
            // 
            this.NIOROSHI_GYOUSHA_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.NIOROSHI_GYOUSHA_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NIOROSHI_GYOUSHA_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.NIOROSHI_GYOUSHA_NAME_RYAKU.DBFieldsName = "GYOUSHA_NAME_RYAKU";
            this.NIOROSHI_GYOUSHA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.NIOROSHI_GYOUSHA_NAME_RYAKU.DisplayPopUp = null;
            this.NIOROSHI_GYOUSHA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NIOROSHI_GYOUSHA_NAME_RYAKU.FocusOutCheckMethod")));
            this.NIOROSHI_GYOUSHA_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.NIOROSHI_GYOUSHA_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.NIOROSHI_GYOUSHA_NAME_RYAKU.IsInputErrorOccured = false;
            this.NIOROSHI_GYOUSHA_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.NIOROSHI_GYOUSHA_NAME_RYAKU.Location = new System.Drawing.Point(648, 47);
            this.NIOROSHI_GYOUSHA_NAME_RYAKU.MaxLength = 0;
            this.NIOROSHI_GYOUSHA_NAME_RYAKU.Name = "NIOROSHI_GYOUSHA_NAME_RYAKU";
            this.NIOROSHI_GYOUSHA_NAME_RYAKU.PopupAfterExecute = null;
            this.NIOROSHI_GYOUSHA_NAME_RYAKU.PopupBeforeExecute = null;
            this.NIOROSHI_GYOUSHA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NIOROSHI_GYOUSHA_NAME_RYAKU.PopupSearchSendParams")));
            this.NIOROSHI_GYOUSHA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NIOROSHI_GYOUSHA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NIOROSHI_GYOUSHA_NAME_RYAKU.popupWindowSetting")));
            this.NIOROSHI_GYOUSHA_NAME_RYAKU.ReadOnly = true;
            this.NIOROSHI_GYOUSHA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NIOROSHI_GYOUSHA_NAME_RYAKU.RegistCheckMethod")));
            this.NIOROSHI_GYOUSHA_NAME_RYAKU.Size = new System.Drawing.Size(290, 20);
            this.NIOROSHI_GYOUSHA_NAME_RYAKU.TabIndex = 28;
            this.NIOROSHI_GYOUSHA_NAME_RYAKU.TabStop = false;
            // 
            // lblNioroshiGyousha
            // 
            this.lblNioroshiGyousha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblNioroshiGyousha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNioroshiGyousha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblNioroshiGyousha.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblNioroshiGyousha.ForeColor = System.Drawing.Color.White;
            this.lblNioroshiGyousha.Location = new System.Drawing.Point(484, 47);
            this.lblNioroshiGyousha.Name = "lblNioroshiGyousha";
            this.lblNioroshiGyousha.Size = new System.Drawing.Size(110, 20);
            this.lblNioroshiGyousha.TabIndex = 56;
            this.lblNioroshiGyousha.Text = "荷降業者";
            this.lblNioroshiGyousha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnHinmeiSrch
            // 
            this.btnHinmeiSrch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnHinmeiSrch.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.btnHinmeiSrch.DBFieldsName = null;
            this.btnHinmeiSrch.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnHinmeiSrch.DisplayItemName = null;
            this.btnHinmeiSrch.DisplayPopUp = null;
            this.btnHinmeiSrch.ErrorMessage = null;
            this.btnHinmeiSrch.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnHinmeiSrch.FocusOutCheckMethod")));
            this.btnHinmeiSrch.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.btnHinmeiSrch.GetCodeMasterField = null;
            this.btnHinmeiSrch.Image = ((System.Drawing.Image)(resources.GetObject("btnHinmeiSrch.Image")));
            this.btnHinmeiSrch.ItemDefinedTypes = null;
            this.btnHinmeiSrch.LinkedSettingTextBox = null;
            this.btnHinmeiSrch.LinkedTextBoxs = null;
            this.btnHinmeiSrch.Location = new System.Drawing.Point(938, 90);
            this.btnHinmeiSrch.Name = "btnHinmeiSrch";
            this.btnHinmeiSrch.PopupAfterExecute = null;
            this.btnHinmeiSrch.PopupAfterExecuteMethod = "HINMEI_CD_PopupAfter";
            this.btnHinmeiSrch.PopupBeforeExecute = null;
            this.btnHinmeiSrch.PopupGetMasterField = "HINMEI_CD, HINMEI_NAME_RYAKU";
            this.btnHinmeiSrch.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("btnHinmeiSrch.PopupSearchSendParams")));
            this.btnHinmeiSrch.PopupSetFormField = "HINMEI_CD, HINMEI_NAME_RYAKU";
            this.btnHinmeiSrch.PopupWindowId = r_framework.Const.WINDOW_ID.M_HINMEI;
            this.btnHinmeiSrch.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.btnHinmeiSrch.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("btnHinmeiSrch.popupWindowSetting")));
            this.btnHinmeiSrch.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnHinmeiSrch.RegistCheckMethod")));
            this.btnHinmeiSrch.SearchDisplayFlag = 0;
            this.btnHinmeiSrch.SetFormField = "";
            this.btnHinmeiSrch.ShortItemName = null;
            this.btnHinmeiSrch.Size = new System.Drawing.Size(22, 22);
            this.btnHinmeiSrch.TabIndex = 35;
            this.btnHinmeiSrch.TabStop = false;
            this.btnHinmeiSrch.Tag = "";
            this.btnHinmeiSrch.UseVisualStyleBackColor = false;
            this.btnHinmeiSrch.ZeroPaddengFlag = false;
            // 
            // HINMEI_NAME_RYAKU
            // 
            this.HINMEI_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.HINMEI_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HINMEI_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.HINMEI_NAME_RYAKU.DBFieldsName = "HINMEI_NAME_RYAKU";
            this.HINMEI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.HINMEI_NAME_RYAKU.DisplayPopUp = null;
            this.HINMEI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_NAME_RYAKU.FocusOutCheckMethod")));
            this.HINMEI_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HINMEI_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.HINMEI_NAME_RYAKU.IsInputErrorOccured = false;
            this.HINMEI_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.HINMEI_NAME_RYAKU.Location = new System.Drawing.Point(648, 91);
            this.HINMEI_NAME_RYAKU.MaxLength = 0;
            this.HINMEI_NAME_RYAKU.Name = "HINMEI_NAME_RYAKU";
            this.HINMEI_NAME_RYAKU.PopupAfterExecute = null;
            this.HINMEI_NAME_RYAKU.PopupBeforeExecute = null;
            this.HINMEI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HINMEI_NAME_RYAKU.PopupSearchSendParams")));
            this.HINMEI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HINMEI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HINMEI_NAME_RYAKU.popupWindowSetting")));
            this.HINMEI_NAME_RYAKU.ReadOnly = true;
            this.HINMEI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_NAME_RYAKU.RegistCheckMethod")));
            this.HINMEI_NAME_RYAKU.Size = new System.Drawing.Size(290, 20);
            this.HINMEI_NAME_RYAKU.TabIndex = 34;
            this.HINMEI_NAME_RYAKU.TabStop = false;
            // 
            // HINMEI_CD
            // 
            this.HINMEI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.HINMEI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HINMEI_CD.ChangeUpperCase = true;
            this.HINMEI_CD.CharacterLimitList = null;
            this.HINMEI_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.HINMEI_CD.DBFieldsName = "";
            this.HINMEI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.HINMEI_CD.DisplayItemName = "品名";
            this.HINMEI_CD.DisplayPopUp = null;
            this.HINMEI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_CD.FocusOutCheckMethod")));
            this.HINMEI_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HINMEI_CD.ForeColor = System.Drawing.Color.Black;
            this.HINMEI_CD.GetCodeMasterField = "HINMEI_CD, HINMEI_NAME_RYAKU";
            this.HINMEI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.HINMEI_CD.IsInputErrorOccured = false;
            this.HINMEI_CD.ItemDefinedTypes = "varchar";
            this.HINMEI_CD.Location = new System.Drawing.Point(599, 91);
            this.HINMEI_CD.MaxLength = 6;
            this.HINMEI_CD.Name = "HINMEI_CD";
            this.HINMEI_CD.PopupAfterExecute = null;
            this.HINMEI_CD.PopupAfterExecuteMethod = "";
            this.HINMEI_CD.PopupBeforeExecute = null;
            this.HINMEI_CD.PopupGetMasterField = "HINMEI_CD, HINMEI_NAME_RYAKU";
            this.HINMEI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HINMEI_CD.PopupSearchSendParams")));
            this.HINMEI_CD.PopupSetFormField = "HINMEI_CD, HINMEI_NAME_RYAKU";
            this.HINMEI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_HINMEI;
            this.HINMEI_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.HINMEI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HINMEI_CD.popupWindowSetting")));
            this.HINMEI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_CD.RegistCheckMethod")));
            this.HINMEI_CD.SetFormField = "HINMEI_CD, HINMEI_NAME_RYAKU";
            this.HINMEI_CD.Size = new System.Drawing.Size(50, 20);
            this.HINMEI_CD.TabIndex = 33;
            this.HINMEI_CD.Tag = "品名を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.HINMEI_CD.ZeroPaddengFlag = true;
            // 
            // lblHinmei
            // 
            this.lblHinmei.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblHinmei.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHinmei.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblHinmei.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblHinmei.ForeColor = System.Drawing.Color.White;
            this.lblHinmei.Location = new System.Drawing.Point(484, 91);
            this.lblHinmei.Name = "lblHinmei";
            this.lblHinmei.Size = new System.Drawing.Size(110, 20);
            this.lblHinmei.TabIndex = 58;
            this.lblHinmei.Text = "品名";
            this.lblHinmei.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnShuruiSrch
            // 
            this.btnShuruiSrch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnShuruiSrch.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.btnShuruiSrch.DBFieldsName = null;
            this.btnShuruiSrch.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnShuruiSrch.DisplayItemName = null;
            this.btnShuruiSrch.DisplayPopUp = null;
            this.btnShuruiSrch.ErrorMessage = null;
            this.btnShuruiSrch.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnShuruiSrch.FocusOutCheckMethod")));
            this.btnShuruiSrch.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.btnShuruiSrch.GetCodeMasterField = null;
            this.btnShuruiSrch.Image = ((System.Drawing.Image)(resources.GetObject("btnShuruiSrch.Image")));
            this.btnShuruiSrch.ItemDefinedTypes = null;
            this.btnShuruiSrch.LinkedSettingTextBox = null;
            this.btnShuruiSrch.LinkedTextBoxs = null;
            this.btnShuruiSrch.Location = new System.Drawing.Point(938, 134);
            this.btnShuruiSrch.Name = "btnShuruiSrch";
            this.btnShuruiSrch.PopupAfterExecute = null;
            this.btnShuruiSrch.PopupAfterExecuteMethod = "SHURUI_CD_PopupAfter";
            this.btnShuruiSrch.PopupBeforeExecute = null;
            this.btnShuruiSrch.PopupGetMasterField = "SHURUI_CD, SHURUI_NAME_RYAKU";
            this.btnShuruiSrch.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("btnShuruiSrch.PopupSearchSendParams")));
            this.btnShuruiSrch.PopupSetFormField = "SHURUI_CD, SHURUI_NAME_RYAKU";
            this.btnShuruiSrch.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHURUI;
            this.btnShuruiSrch.PopupWindowName = "マスタ共通ポップアップ";
            this.btnShuruiSrch.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("btnShuruiSrch.popupWindowSetting")));
            this.btnShuruiSrch.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnShuruiSrch.RegistCheckMethod")));
            this.btnShuruiSrch.SearchDisplayFlag = 0;
            this.btnShuruiSrch.SetFormField = "";
            this.btnShuruiSrch.ShortItemName = null;
            this.btnShuruiSrch.Size = new System.Drawing.Size(22, 22);
            this.btnShuruiSrch.TabIndex = 41;
            this.btnShuruiSrch.TabStop = false;
            this.btnShuruiSrch.Tag = "";
            this.btnShuruiSrch.UseVisualStyleBackColor = false;
            this.btnShuruiSrch.ZeroPaddengFlag = false;
            // 
            // SHURUI_CD
            // 
            this.SHURUI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.SHURUI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHURUI_CD.ChangeUpperCase = true;
            this.SHURUI_CD.CharacterLimitList = null;
            this.SHURUI_CD.CharactersNumber = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.SHURUI_CD.DBFieldsName = "";
            this.SHURUI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHURUI_CD.DisplayItemName = "種類";
            this.SHURUI_CD.DisplayPopUp = null;
            this.SHURUI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHURUI_CD.FocusOutCheckMethod")));
            this.SHURUI_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHURUI_CD.ForeColor = System.Drawing.Color.Black;
            this.SHURUI_CD.GetCodeMasterField = "SHURUI_CD, SHURUI_NAME_RYAKU";
            this.SHURUI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SHURUI_CD.IsInputErrorOccured = false;
            this.SHURUI_CD.ItemDefinedTypes = "varchar";
            this.SHURUI_CD.Location = new System.Drawing.Point(599, 135);
            this.SHURUI_CD.MaxLength = 3;
            this.SHURUI_CD.Name = "SHURUI_CD";
            this.SHURUI_CD.PopupAfterExecute = null;
            this.SHURUI_CD.PopupAfterExecuteMethod = "";
            this.SHURUI_CD.PopupBeforeExecute = null;
            this.SHURUI_CD.PopupGetMasterField = "SHURUI_CD, SHURUI_NAME_RYAKU";
            this.SHURUI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHURUI_CD.PopupSearchSendParams")));
            this.SHURUI_CD.PopupSetFormField = "SHURUI_CD, SHURUI_NAME_RYAKU";
            this.SHURUI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHURUI;
            this.SHURUI_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.SHURUI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHURUI_CD.popupWindowSetting")));
            this.SHURUI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHURUI_CD.RegistCheckMethod")));
            this.SHURUI_CD.SetFormField = "SHURUI_CD, SHURUI_NAME_RYAKU";
            this.SHURUI_CD.Size = new System.Drawing.Size(50, 20);
            this.SHURUI_CD.TabIndex = 39;
            this.SHURUI_CD.Tag = "種類を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.SHURUI_CD.ZeroPaddengFlag = true;
            // 
            // SHURUI_NAME_RYAKU
            // 
            this.SHURUI_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.SHURUI_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHURUI_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.SHURUI_NAME_RYAKU.DBFieldsName = "SHURUI_NAME_RYAKU";
            this.SHURUI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHURUI_NAME_RYAKU.DisplayPopUp = null;
            this.SHURUI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHURUI_NAME_RYAKU.FocusOutCheckMethod")));
            this.SHURUI_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHURUI_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.SHURUI_NAME_RYAKU.IsInputErrorOccured = false;
            this.SHURUI_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.SHURUI_NAME_RYAKU.Location = new System.Drawing.Point(648, 135);
            this.SHURUI_NAME_RYAKU.MaxLength = 0;
            this.SHURUI_NAME_RYAKU.Name = "SHURUI_NAME_RYAKU";
            this.SHURUI_NAME_RYAKU.PopupAfterExecute = null;
            this.SHURUI_NAME_RYAKU.PopupBeforeExecute = null;
            this.SHURUI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHURUI_NAME_RYAKU.PopupSearchSendParams")));
            this.SHURUI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHURUI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHURUI_NAME_RYAKU.popupWindowSetting")));
            this.SHURUI_NAME_RYAKU.ReadOnly = true;
            this.SHURUI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHURUI_NAME_RYAKU.RegistCheckMethod")));
            this.SHURUI_NAME_RYAKU.Size = new System.Drawing.Size(290, 20);
            this.SHURUI_NAME_RYAKU.TabIndex = 40;
            this.SHURUI_NAME_RYAKU.TabStop = false;
            // 
            // lblShurui
            // 
            this.lblShurui.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblShurui.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblShurui.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblShurui.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblShurui.ForeColor = System.Drawing.Color.White;
            this.lblShurui.Location = new System.Drawing.Point(484, 135);
            this.lblShurui.Name = "lblShurui";
            this.lblShurui.Size = new System.Drawing.Size(110, 20);
            this.lblShurui.TabIndex = 59;
            this.lblShurui.Text = "種類";
            this.lblShurui.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnBunruiSrch
            // 
            this.btnBunruiSrch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnBunruiSrch.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.btnBunruiSrch.DBFieldsName = null;
            this.btnBunruiSrch.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnBunruiSrch.DisplayItemName = null;
            this.btnBunruiSrch.DisplayPopUp = null;
            this.btnBunruiSrch.ErrorMessage = null;
            this.btnBunruiSrch.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnBunruiSrch.FocusOutCheckMethod")));
            this.btnBunruiSrch.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.btnBunruiSrch.GetCodeMasterField = null;
            this.btnBunruiSrch.Image = ((System.Drawing.Image)(resources.GetObject("btnBunruiSrch.Image")));
            this.btnBunruiSrch.ItemDefinedTypes = null;
            this.btnBunruiSrch.LinkedSettingTextBox = null;
            this.btnBunruiSrch.LinkedTextBoxs = null;
            this.btnBunruiSrch.Location = new System.Drawing.Point(938, 156);
            this.btnBunruiSrch.Name = "btnBunruiSrch";
            this.btnBunruiSrch.PopupAfterExecute = null;
            this.btnBunruiSrch.PopupAfterExecuteMethod = "BUNRUI_CD_PopupAfter";
            this.btnBunruiSrch.PopupBeforeExecute = null;
            this.btnBunruiSrch.PopupGetMasterField = "BUNRUI_CD, BUNRUI_NAME_RYAKU";
            this.btnBunruiSrch.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("btnBunruiSrch.PopupSearchSendParams")));
            this.btnBunruiSrch.PopupSetFormField = "BUNRUI_CD, BUNRUI_NAME_RYAKU";
            this.btnBunruiSrch.PopupWindowId = r_framework.Const.WINDOW_ID.M_BUNRUI;
            this.btnBunruiSrch.PopupWindowName = "マスタ共通ポップアップ";
            this.btnBunruiSrch.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("btnBunruiSrch.popupWindowSetting")));
            this.btnBunruiSrch.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnBunruiSrch.RegistCheckMethod")));
            this.btnBunruiSrch.SearchDisplayFlag = 0;
            this.btnBunruiSrch.SetFormField = "";
            this.btnBunruiSrch.ShortItemName = null;
            this.btnBunruiSrch.Size = new System.Drawing.Size(22, 22);
            this.btnBunruiSrch.TabIndex = 44;
            this.btnBunruiSrch.TabStop = false;
            this.btnBunruiSrch.Tag = "";
            this.btnBunruiSrch.UseVisualStyleBackColor = false;
            this.btnBunruiSrch.ZeroPaddengFlag = false;
            // 
            // BUNRUI_CD
            // 
            this.BUNRUI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.BUNRUI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BUNRUI_CD.ChangeUpperCase = true;
            this.BUNRUI_CD.CharacterLimitList = null;
            this.BUNRUI_CD.CharactersNumber = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.BUNRUI_CD.DBFieldsName = "";
            this.BUNRUI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.BUNRUI_CD.DisplayItemName = "分類";
            this.BUNRUI_CD.DisplayPopUp = null;
            this.BUNRUI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BUNRUI_CD.FocusOutCheckMethod")));
            this.BUNRUI_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.BUNRUI_CD.ForeColor = System.Drawing.Color.Black;
            this.BUNRUI_CD.GetCodeMasterField = "BUNRUI_CD, BUNRUI_NAME_RYAKU";
            this.BUNRUI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.BUNRUI_CD.IsInputErrorOccured = false;
            this.BUNRUI_CD.ItemDefinedTypes = "varchar";
            this.BUNRUI_CD.Location = new System.Drawing.Point(599, 157);
            this.BUNRUI_CD.MaxLength = 3;
            this.BUNRUI_CD.Name = "BUNRUI_CD";
            this.BUNRUI_CD.PopupAfterExecute = null;
            this.BUNRUI_CD.PopupAfterExecuteMethod = "";
            this.BUNRUI_CD.PopupBeforeExecute = null;
            this.BUNRUI_CD.PopupGetMasterField = "BUNRUI_CD, BUNRUI_NAME_RYAKU";
            this.BUNRUI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BUNRUI_CD.PopupSearchSendParams")));
            this.BUNRUI_CD.PopupSetFormField = "BUNRUI_CD, BUNRUI_NAME_RYAKU";
            this.BUNRUI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_BUNRUI;
            this.BUNRUI_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.BUNRUI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BUNRUI_CD.popupWindowSetting")));
            this.BUNRUI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BUNRUI_CD.RegistCheckMethod")));
            this.BUNRUI_CD.SetFormField = "BUNRUI_CD, BUNRUI_NAME_RYAKU";
            this.BUNRUI_CD.Size = new System.Drawing.Size(50, 20);
            this.BUNRUI_CD.TabIndex = 42;
            this.BUNRUI_CD.Tag = "分類を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.BUNRUI_CD.ZeroPaddengFlag = true;
            // 
            // BUNRUI_NAME_RYAKU
            // 
            this.BUNRUI_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.BUNRUI_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BUNRUI_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.BUNRUI_NAME_RYAKU.DBFieldsName = "HINMEI_NAME_RYAKU";
            this.BUNRUI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.BUNRUI_NAME_RYAKU.DisplayPopUp = null;
            this.BUNRUI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BUNRUI_NAME_RYAKU.FocusOutCheckMethod")));
            this.BUNRUI_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.BUNRUI_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.BUNRUI_NAME_RYAKU.IsInputErrorOccured = false;
            this.BUNRUI_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.BUNRUI_NAME_RYAKU.Location = new System.Drawing.Point(648, 157);
            this.BUNRUI_NAME_RYAKU.MaxLength = 0;
            this.BUNRUI_NAME_RYAKU.Name = "BUNRUI_NAME_RYAKU";
            this.BUNRUI_NAME_RYAKU.PopupAfterExecute = null;
            this.BUNRUI_NAME_RYAKU.PopupBeforeExecute = null;
            this.BUNRUI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BUNRUI_NAME_RYAKU.PopupSearchSendParams")));
            this.BUNRUI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BUNRUI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BUNRUI_NAME_RYAKU.popupWindowSetting")));
            this.BUNRUI_NAME_RYAKU.ReadOnly = true;
            this.BUNRUI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BUNRUI_NAME_RYAKU.RegistCheckMethod")));
            this.BUNRUI_NAME_RYAKU.Size = new System.Drawing.Size(290, 20);
            this.BUNRUI_NAME_RYAKU.TabIndex = 43;
            this.BUNRUI_NAME_RYAKU.TabStop = false;
            // 
            // lblBunrui
            // 
            this.lblBunrui.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblBunrui.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBunrui.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblBunrui.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblBunrui.ForeColor = System.Drawing.Color.White;
            this.lblBunrui.Location = new System.Drawing.Point(484, 157);
            this.lblBunrui.Name = "lblBunrui";
            this.lblBunrui.Size = new System.Drawing.Size(110, 20);
            this.lblBunrui.TabIndex = 60;
            this.lblBunrui.Text = "分類";
            this.lblBunrui.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnUnitSrch
            // 
            this.btnUnitSrch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnUnitSrch.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.btnUnitSrch.DBFieldsName = null;
            this.btnUnitSrch.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnUnitSrch.DisplayItemName = null;
            this.btnUnitSrch.DisplayPopUp = null;
            this.btnUnitSrch.ErrorMessage = null;
            this.btnUnitSrch.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnUnitSrch.FocusOutCheckMethod")));
            this.btnUnitSrch.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.btnUnitSrch.GetCodeMasterField = null;
            this.btnUnitSrch.Image = ((System.Drawing.Image)(resources.GetObject("btnUnitSrch.Image")));
            this.btnUnitSrch.ItemDefinedTypes = null;
            this.btnUnitSrch.LinkedSettingTextBox = null;
            this.btnUnitSrch.LinkedTextBoxs = null;
            this.btnUnitSrch.Location = new System.Drawing.Point(698, 112);
            this.btnUnitSrch.Name = "btnUnitSrch";
            this.btnUnitSrch.PopupAfterExecute = null;
            this.btnUnitSrch.PopupAfterExecuteMethod = "UNIT_CD_PopupAfter";
            this.btnUnitSrch.PopupBeforeExecute = null;
            this.btnUnitSrch.PopupGetMasterField = "UNIT_CD, UNIT_NAME_RYAKU";
            this.btnUnitSrch.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("btnUnitSrch.PopupSearchSendParams")));
            this.btnUnitSrch.PopupSetFormField = "UNIT_CD, UNIT_NAME_RYAKU";
            this.btnUnitSrch.PopupWindowId = r_framework.Const.WINDOW_ID.M_UNIT;
            this.btnUnitSrch.PopupWindowName = "マスタ共通ポップアップ";
            this.btnUnitSrch.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("btnUnitSrch.popupWindowSetting")));
            this.btnUnitSrch.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnUnitSrch.RegistCheckMethod")));
            this.btnUnitSrch.SearchDisplayFlag = 0;
            this.btnUnitSrch.SetFormField = "";
            this.btnUnitSrch.ShortItemName = null;
            this.btnUnitSrch.Size = new System.Drawing.Size(22, 22);
            this.btnUnitSrch.TabIndex = 38;
            this.btnUnitSrch.TabStop = false;
            this.btnUnitSrch.Tag = "";
            this.btnUnitSrch.UseVisualStyleBackColor = false;
            this.btnUnitSrch.ZeroPaddengFlag = false;
            // 
            // UNIT_CD
            // 
            this.UNIT_CD.BackColor = System.Drawing.SystemColors.Window;
            this.UNIT_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UNIT_CD.ChangeUpperCase = true;
            this.UNIT_CD.DBFieldsName = "";
            this.UNIT_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNIT_CD.DisplayItemName = "単位";
            this.UNIT_CD.DisplayPopUp = null;
            this.UNIT_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNIT_CD.FocusOutCheckMethod")));
            this.UNIT_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.UNIT_CD.ForeColor = System.Drawing.Color.Black;
            this.UNIT_CD.GetCodeMasterField = "UNIT_CD, UNIT_NAME_RYAKU";
            this.UNIT_CD.IsInputErrorOccured = false;
            this.UNIT_CD.ItemDefinedTypes = "varchar";
            this.UNIT_CD.Location = new System.Drawing.Point(599, 113);
            this.UNIT_CD.Name = "UNIT_CD";
            this.UNIT_CD.PopupAfterExecute = null;
            this.UNIT_CD.PopupAfterExecuteMethod = "";
            this.UNIT_CD.PopupBeforeExecute = null;
            this.UNIT_CD.PopupGetMasterField = "UNIT_CD, UNIT_NAME_RYAKU";
            this.UNIT_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNIT_CD.PopupSearchSendParams")));
            this.UNIT_CD.PopupSetFormField = "UNIT_CD, UNIT_NAME_RYAKU";
            this.UNIT_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_UNIT;
            this.UNIT_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.UNIT_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNIT_CD.popupWindowSetting")));
            rangeSettingDto4.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.UNIT_CD.RangeSetting = rangeSettingDto4;
            this.UNIT_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNIT_CD.RegistCheckMethod")));
            this.UNIT_CD.SetFormField = "UNIT_CD, UNIT_NAME_RYAKU";
            this.UNIT_CD.Size = new System.Drawing.Size(50, 20);
            this.UNIT_CD.TabIndex = 36;
            this.UNIT_CD.Tag = "単位を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.UNIT_CD.WordWrap = false;
            // 
            // UNIT_NAME_RYAKU
            // 
            this.UNIT_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.UNIT_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UNIT_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.UNIT_NAME_RYAKU.DBFieldsName = "HINMEI_NAME_RYAKU";
            this.UNIT_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNIT_NAME_RYAKU.DisplayPopUp = null;
            this.UNIT_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNIT_NAME_RYAKU.FocusOutCheckMethod")));
            this.UNIT_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.UNIT_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.UNIT_NAME_RYAKU.IsInputErrorOccured = false;
            this.UNIT_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.UNIT_NAME_RYAKU.Location = new System.Drawing.Point(648, 113);
            this.UNIT_NAME_RYAKU.MaxLength = 0;
            this.UNIT_NAME_RYAKU.Name = "UNIT_NAME_RYAKU";
            this.UNIT_NAME_RYAKU.PopupAfterExecute = null;
            this.UNIT_NAME_RYAKU.PopupBeforeExecute = null;
            this.UNIT_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNIT_NAME_RYAKU.PopupSearchSendParams")));
            this.UNIT_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UNIT_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNIT_NAME_RYAKU.popupWindowSetting")));
            this.UNIT_NAME_RYAKU.ReadOnly = true;
            this.UNIT_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNIT_NAME_RYAKU.RegistCheckMethod")));
            this.UNIT_NAME_RYAKU.Size = new System.Drawing.Size(50, 20);
            this.UNIT_NAME_RYAKU.TabIndex = 37;
            this.UNIT_NAME_RYAKU.TabStop = false;
            // 
            // lblUnit
            // 
            this.lblUnit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUnit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblUnit.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblUnit.ForeColor = System.Drawing.Color.White;
            this.lblUnit.Location = new System.Drawing.Point(484, 113);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Size = new System.Drawing.Size(110, 20);
            this.lblUnit.TabIndex = 61;
            this.lblUnit.Text = "単位";
            this.lblUnit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mrDetail
            // 
            this.mrDetail.AllowUserToAddRows = false;
            this.mrDetail.AllowUserToDeleteRows = false;
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.mrDetail.ColumnHeadersDefaultHeaderCellStyle = cellStyle1;
            this.mrDetail.CurrentRowBorderLine = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Medium, System.Drawing.Color.Red);
            this.mrDetail.EditMode = GrapeCity.Win.MultiRow.EditMode.EditOnEnter;
            this.mrDetail.FreezeLeftCellName = "CHECK";
            this.mrDetail.Location = new System.Drawing.Point(1, 179);
            this.mrDetail.MouseWheelCount = 1;
            this.mrDetail.MultiSelect = false;
            this.mrDetail.Name = "mrDetail";
            this.mrDetail.SelectRowWhenClickBlankArea = true;
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
            this.mrDetail.ShortcutKeyManager = shortcutKeyManager1;
            this.mrDetail.ShowCellToolTips = false;
            this.mrDetail.Size = new System.Drawing.Size(963, 253);
            this.mrDetail.TabIndex = 45;
            this.mrDetail.Template = this.mrDetailUkeireTemplate;
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
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(409, 25);
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
            // lblDenpyouKbn
            // 
            this.lblDenpyouKbn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblDenpyouKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDenpyouKbn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDenpyouKbn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblDenpyouKbn.ForeColor = System.Drawing.Color.White;
            this.lblDenpyouKbn.Location = new System.Drawing.Point(979, 3);
            this.lblDenpyouKbn.Name = "lblDenpyouKbn";
            this.lblDenpyouKbn.Size = new System.Drawing.Size(110, 20);
            this.lblDenpyouKbn.TabIndex = 672;
            this.lblDenpyouKbn.Text = "伝票区分";
            this.lblDenpyouKbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DENPYOU_KBN_CD
            // 
            this.DENPYOU_KBN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.DENPYOU_KBN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DENPYOU_KBN_CD.DBFieldsName = "DENPYOU_KBN_CD";
            this.DENPYOU_KBN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.DENPYOU_KBN_CD.DisplayPopUp = null;
            this.DENPYOU_KBN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_KBN_CD.FocusOutCheckMethod")));
            this.DENPYOU_KBN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DENPYOU_KBN_CD.ForeColor = System.Drawing.Color.Black;
            this.DENPYOU_KBN_CD.IsInputErrorOccured = false;
            this.DENPYOU_KBN_CD.Location = new System.Drawing.Point(979, 25);
            this.DENPYOU_KBN_CD.Name = "DENPYOU_KBN_CD";
            this.DENPYOU_KBN_CD.PopupAfterExecute = null;
            this.DENPYOU_KBN_CD.PopupBeforeExecute = null;
            this.DENPYOU_KBN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DENPYOU_KBN_CD.PopupSearchSendParams")));
            this.DENPYOU_KBN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DENPYOU_KBN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DENPYOU_KBN_CD.popupWindowSetting")));
            rangeSettingDto5.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto5.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DENPYOU_KBN_CD.RangeSetting = rangeSettingDto5;
            this.DENPYOU_KBN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_KBN_CD.RegistCheckMethod")));
            this.DENPYOU_KBN_CD.Size = new System.Drawing.Size(30, 20);
            this.DENPYOU_KBN_CD.TabIndex = 673;
            this.DENPYOU_KBN_CD.Tag = "伝票区分を指定してください。　【1．売上　2．支払】";
            this.DENPYOU_KBN_CD.WordWrap = false;
            this.DENPYOU_KBN_CD.Validated += new System.EventHandler(this.DENPYOU_KBN_CD_Validated);
            // 
            // DENPYOU_KBN_NAME
            // 
            this.DENPYOU_KBN_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.DENPYOU_KBN_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DENPYOU_KBN_NAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.DENPYOU_KBN_NAME.DBFieldsName = "DENPYOU_KBN_NAME";
            this.DENPYOU_KBN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.DENPYOU_KBN_NAME.DisplayPopUp = null;
            this.DENPYOU_KBN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_KBN_NAME.FocusOutCheckMethod")));
            this.DENPYOU_KBN_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.DENPYOU_KBN_NAME.ForeColor = System.Drawing.Color.Black;
            this.DENPYOU_KBN_NAME.IsInputErrorOccured = false;
            this.DENPYOU_KBN_NAME.ItemDefinedTypes = "varchar";
            this.DENPYOU_KBN_NAME.Location = new System.Drawing.Point(1008, 25);
            this.DENPYOU_KBN_NAME.MaxLength = 0;
            this.DENPYOU_KBN_NAME.Name = "DENPYOU_KBN_NAME";
            this.DENPYOU_KBN_NAME.PopupAfterExecute = null;
            this.DENPYOU_KBN_NAME.PopupBeforeExecute = null;
            this.DENPYOU_KBN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DENPYOU_KBN_NAME.PopupSearchSendParams")));
            this.DENPYOU_KBN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DENPYOU_KBN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DENPYOU_KBN_NAME.popupWindowSetting")));
            this.DENPYOU_KBN_NAME.ReadOnly = true;
            this.DENPYOU_KBN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_KBN_NAME.RegistCheckMethod")));
            this.DENPYOU_KBN_NAME.Size = new System.Drawing.Size(81, 20);
            this.DENPYOU_KBN_NAME.TabIndex = 674;
            this.DENPYOU_KBN_NAME.TabStop = false;
            // 
            // lblTorihikiKbn
            // 
            this.lblTorihikiKbn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblTorihikiKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTorihikiKbn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTorihikiKbn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTorihikiKbn.ForeColor = System.Drawing.Color.White;
            this.lblTorihikiKbn.Location = new System.Drawing.Point(979, 69);
            this.lblTorihikiKbn.Name = "lblTorihikiKbn";
            this.lblTorihikiKbn.Size = new System.Drawing.Size(110, 20);
            this.lblTorihikiKbn.TabIndex = 675;
            this.lblTorihikiKbn.Text = "取引区分";
            this.lblTorihikiKbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TORIHIKI_KBN_CD
            // 
            this.TORIHIKI_KBN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.TORIHIKI_KBN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKI_KBN_CD.DBFieldsName = "TORIHIKI_KBN_CD";
            this.TORIHIKI_KBN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKI_KBN_CD.DisplayPopUp = null;
            this.TORIHIKI_KBN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKI_KBN_CD.FocusOutCheckMethod")));
            this.TORIHIKI_KBN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TORIHIKI_KBN_CD.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKI_KBN_CD.IsInputErrorOccured = false;
            this.TORIHIKI_KBN_CD.Location = new System.Drawing.Point(979, 91);
            this.TORIHIKI_KBN_CD.Name = "TORIHIKI_KBN_CD";
            this.TORIHIKI_KBN_CD.PopupAfterExecute = null;
            this.TORIHIKI_KBN_CD.PopupBeforeExecute = null;
            this.TORIHIKI_KBN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKI_KBN_CD.PopupSearchSendParams")));
            this.TORIHIKI_KBN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKI_KBN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKI_KBN_CD.popupWindowSetting")));
            rangeSettingDto6.Max = new decimal(new int[] {
            3,
            0,
            0,
            0});
            rangeSettingDto6.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.TORIHIKI_KBN_CD.RangeSetting = rangeSettingDto6;
            this.TORIHIKI_KBN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKI_KBN_CD.RegistCheckMethod")));
            this.TORIHIKI_KBN_CD.Size = new System.Drawing.Size(30, 20);
            this.TORIHIKI_KBN_CD.TabIndex = 676;
            this.TORIHIKI_KBN_CD.Tag = "取引区分を指定してください。　【1．掛け　2．現金、３．全て】";
            this.TORIHIKI_KBN_CD.WordWrap = false;
            this.TORIHIKI_KBN_CD.Validated += new System.EventHandler(this.TORIHIKI_KBN_CD_Validated);
            // 
            // TORIHIKI_KBN_NAME
            // 
            this.TORIHIKI_KBN_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.TORIHIKI_KBN_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKI_KBN_NAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.TORIHIKI_KBN_NAME.DBFieldsName = "TORIHIKI_KBN_NAME";
            this.TORIHIKI_KBN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKI_KBN_NAME.DisplayPopUp = null;
            this.TORIHIKI_KBN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKI_KBN_NAME.FocusOutCheckMethod")));
            this.TORIHIKI_KBN_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TORIHIKI_KBN_NAME.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKI_KBN_NAME.IsInputErrorOccured = false;
            this.TORIHIKI_KBN_NAME.ItemDefinedTypes = "varchar";
            this.TORIHIKI_KBN_NAME.Location = new System.Drawing.Point(1008, 91);
            this.TORIHIKI_KBN_NAME.MaxLength = 0;
            this.TORIHIKI_KBN_NAME.Name = "TORIHIKI_KBN_NAME";
            this.TORIHIKI_KBN_NAME.PopupAfterExecute = null;
            this.TORIHIKI_KBN_NAME.PopupBeforeExecute = null;
            this.TORIHIKI_KBN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKI_KBN_NAME.PopupSearchSendParams")));
            this.TORIHIKI_KBN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKI_KBN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKI_KBN_NAME.popupWindowSetting")));
            this.TORIHIKI_KBN_NAME.ReadOnly = true;
            this.TORIHIKI_KBN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKI_KBN_NAME.RegistCheckMethod")));
            this.TORIHIKI_KBN_NAME.Size = new System.Drawing.Size(81, 20);
            this.TORIHIKI_KBN_NAME.TabIndex = 677;
            this.TORIHIKI_KBN_NAME.TabStop = false;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1100, 440);
            this.Controls.Add(this.TORIHIKI_KBN_NAME);
            this.Controls.Add(this.TORIHIKI_KBN_CD);
            this.Controls.Add(this.lblTorihikiKbn);
            this.Controls.Add(this.DENPYOU_KBN_NAME);
            this.Controls.Add(this.DENPYOU_KBN_CD);
            this.Controls.Add(this.lblDenpyouKbn);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.btnUnitSrch);
            this.Controls.Add(this.UNIT_CD);
            this.Controls.Add(this.UNIT_NAME_RYAKU);
            this.Controls.Add(this.lblUnit);
            this.Controls.Add(this.btnBunruiSrch);
            this.Controls.Add(this.BUNRUI_CD);
            this.Controls.Add(this.BUNRUI_NAME_RYAKU);
            this.Controls.Add(this.lblBunrui);
            this.Controls.Add(this.btnShuruiSrch);
            this.Controls.Add(this.SHURUI_CD);
            this.Controls.Add(this.SHURUI_NAME_RYAKU);
            this.Controls.Add(this.lblShurui);
            this.Controls.Add(this.btnHinmeiSrch);
            this.Controls.Add(this.HINMEI_CD);
            this.Controls.Add(this.HINMEI_NAME_RYAKU);
            this.Controls.Add(this.lblHinmei);
            this.Controls.Add(this.btnNioroshiGenbaSrch);
            this.Controls.Add(this.NIOROSHI_GENBA_CD);
            this.Controls.Add(this.NIOROSHI_GENBA_NAME_RYAKU);
            this.Controls.Add(this.lblNioroshiGenba);
            this.Controls.Add(this.btnNioroshiGyoushaSrch);
            this.Controls.Add(this.NIOROSHI_GYOUSHA_CD);
            this.Controls.Add(this.NIOROSHI_GYOUSHA_NAME_RYAKU);
            this.Controls.Add(this.lblNioroshiGyousha);
            this.Controls.Add(this.btnNizumiGenbaSrch);
            this.Controls.Add(this.NIZUMI_GENBA_CD);
            this.Controls.Add(this.NIZUMI_GENBA_NAME_RYAKU);
            this.Controls.Add(this.lblNizumiGenba);
            this.Controls.Add(this.btnNizumiGyoushaSrch);
            this.Controls.Add(this.NIZUMI_GYOUSHA_CD);
            this.Controls.Add(this.NIZUMI_GYOUSHA_NAME_RYAKU);
            this.Controls.Add(this.lblNitumiGyousha);
            this.Controls.Add(this.btnUpnGyoushaSrch);
            this.Controls.Add(this.UPN_GYOUSHA_CD);
            this.Controls.Add(this.UPN_GYOUSHA_NAME_RYAKU);
            this.Controls.Add(this.lblUpnGyousha);
            this.Controls.Add(this.txtDenshuKbn);
            this.Controls.Add(this.customPanel1);
            this.Controls.Add(this.lblDenshu);
            this.Controls.Add(this.txtKakuteiKbn);
            this.Controls.Add(this.pnlDenpyouDateKbn);
            this.Controls.Add(this.lblKakutei);
            this.Controls.Add(this.btnKyotenSrch);
            this.Controls.Add(this.KYOTEN_NAME_RYAKU);
            this.Controls.Add(this.KYOTEN_CD);
            this.Controls.Add(this.lblKyoten);
            this.Controls.Add(this.HIDUKE_FROM);
            this.Controls.Add(this.HIDUKE_TO);
            this.Controls.Add(this.lblHiduke);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnGenbaSrch);
            this.Controls.Add(this.GENBA_CD);
            this.Controls.Add(this.GENBA_NAME_RYAKU);
            this.Controls.Add(this.lblGenba);
            this.Controls.Add(this.btnGyoushaSrch);
            this.Controls.Add(this.GYOUSHA_CD);
            this.Controls.Add(this.GYOUSHA_NAME_RYAKU);
            this.Controls.Add(this.lblGyousha);
            this.Controls.Add(this.btnTorihikisakiSrch);
            this.Controls.Add(this.TORIHIKISAKI_CD);
            this.Controls.Add(this.TORIHIKISAKI_NAME_RYAKU);
            this.Controls.Add(this.lblTorihikisaki);
            this.Controls.Add(this.mrDetail);
            this.Name = "UIForm";
            this.Text = "";
            this.pnlDenpyouDateKbn.ResumeLayout(false);
            this.pnlDenpyouDateKbn.PerformLayout();
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mrDetail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomPopupOpenButton btnGenbaSrch;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GENBA_CD;
        internal r_framework.CustomControl.CustomTextBox GENBA_NAME_RYAKU;
        internal System.Windows.Forms.Label lblGenba;
        internal r_framework.CustomControl.CustomPopupOpenButton btnGyoushaSrch;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GYOUSHA_CD;
        internal r_framework.CustomControl.CustomTextBox GYOUSHA_NAME_RYAKU;
        internal System.Windows.Forms.Label lblGyousha;
        internal r_framework.CustomControl.CustomPopupOpenButton btnTorihikisakiSrch;
        internal r_framework.CustomControl.CustomAlphaNumTextBox TORIHIKISAKI_CD;
        internal r_framework.CustomControl.CustomTextBox TORIHIKISAKI_NAME_RYAKU;
        internal System.Windows.Forms.Label lblTorihikisaki;
        internal r_framework.CustomControl.GcCustomMultiRow mrDetail;
        private Shougun.Core.BusinessManagement.DenpyouDetailIkkatuUpdate.MultiRowTemplate.DetailTemplateUkeire mrDetailUkeireTemplate;
        private Shougun.Core.BusinessManagement.DenpyouDetailIkkatuUpdate.MultiRowTemplate.DetailTemplateShukka mrDetailShukkaTemplate;
        private Shougun.Core.BusinessManagement.DenpyouDetailIkkatuUpdate.MultiRowTemplate.DetailTemplateUrsh mrDetailUrshTemplate;
        public r_framework.CustomControl.CustomDateTimePicker HIDUKE_FROM;
        public r_framework.CustomControl.CustomDateTimePicker HIDUKE_TO;
        internal System.Windows.Forms.Label lblHiduke;
        private System.Windows.Forms.Label label2;
        public r_framework.CustomControl.CustomTextBox KYOTEN_NAME_RYAKU;
        public r_framework.CustomControl.CustomNumericTextBox2 KYOTEN_CD;
        internal System.Windows.Forms.Label lblKyoten;
        internal r_framework.CustomControl.CustomPopupOpenButton btnKyotenSrch;
        internal r_framework.CustomControl.CustomNumericTextBox2 txtKakuteiKbn;
        internal r_framework.CustomControl.CustomPanel pnlDenpyouDateKbn;
        internal r_framework.CustomControl.CustomRadioButton rdoKakuteiKbn2;
        internal r_framework.CustomControl.CustomRadioButton rdoKakuteiKbn1;
        internal System.Windows.Forms.Label lblKakutei;
        internal r_framework.CustomControl.CustomNumericTextBox2 txtDenshuKbn;
        internal r_framework.CustomControl.CustomPanel customPanel1;
        internal r_framework.CustomControl.CustomRadioButton rdoDenshuKbn2;
        internal r_framework.CustomControl.CustomRadioButton rdoDenshuKbn1;
        internal System.Windows.Forms.Label lblDenshu;
        internal r_framework.CustomControl.CustomRadioButton rdoKakuteiKbn3;
        internal r_framework.CustomControl.CustomRadioButton rdoDenshuKbn3;
        internal r_framework.CustomControl.CustomPopupOpenButton btnUpnGyoushaSrch;
        internal r_framework.CustomControl.CustomAlphaNumTextBox UPN_GYOUSHA_CD;
        internal r_framework.CustomControl.CustomTextBox UPN_GYOUSHA_NAME_RYAKU;
        internal System.Windows.Forms.Label lblUpnGyousha;
        internal r_framework.CustomControl.CustomPopupOpenButton btnNizumiGenbaSrch;
        internal r_framework.CustomControl.CustomAlphaNumTextBox NIZUMI_GENBA_CD;
        internal r_framework.CustomControl.CustomTextBox NIZUMI_GENBA_NAME_RYAKU;
        internal System.Windows.Forms.Label lblNizumiGenba;
        internal r_framework.CustomControl.CustomPopupOpenButton btnNizumiGyoushaSrch;
        internal r_framework.CustomControl.CustomAlphaNumTextBox NIZUMI_GYOUSHA_CD;
        internal r_framework.CustomControl.CustomTextBox NIZUMI_GYOUSHA_NAME_RYAKU;
        internal System.Windows.Forms.Label lblNitumiGyousha;
        internal r_framework.CustomControl.CustomPopupOpenButton btnNioroshiGenbaSrch;
        internal r_framework.CustomControl.CustomAlphaNumTextBox NIOROSHI_GENBA_CD;
        internal r_framework.CustomControl.CustomTextBox NIOROSHI_GENBA_NAME_RYAKU;
        internal System.Windows.Forms.Label lblNioroshiGenba;
        internal r_framework.CustomControl.CustomPopupOpenButton btnNioroshiGyoushaSrch;
        internal r_framework.CustomControl.CustomAlphaNumTextBox NIOROSHI_GYOUSHA_CD;
        internal r_framework.CustomControl.CustomTextBox NIOROSHI_GYOUSHA_NAME_RYAKU;
        internal System.Windows.Forms.Label lblNioroshiGyousha;
        internal r_framework.CustomControl.CustomPopupOpenButton btnHinmeiSrch;
        internal r_framework.CustomControl.CustomTextBox HINMEI_NAME_RYAKU;
        internal r_framework.CustomControl.CustomAlphaNumTextBox HINMEI_CD;
        internal System.Windows.Forms.Label lblHinmei;
        internal r_framework.CustomControl.CustomPopupOpenButton btnShuruiSrch;
        internal r_framework.CustomControl.CustomAlphaNumTextBox SHURUI_CD;
        internal r_framework.CustomControl.CustomTextBox SHURUI_NAME_RYAKU;
        internal System.Windows.Forms.Label lblShurui;
        internal r_framework.CustomControl.CustomPopupOpenButton btnBunruiSrch;
        internal r_framework.CustomControl.CustomAlphaNumTextBox BUNRUI_CD;
        internal r_framework.CustomControl.CustomTextBox BUNRUI_NAME_RYAKU;
        internal System.Windows.Forms.Label lblBunrui;
        internal r_framework.CustomControl.CustomPopupOpenButton btnUnitSrch;
        internal r_framework.CustomControl.CustomNumericTextBox2 UNIT_CD;
        internal r_framework.CustomControl.CustomTextBox UNIT_NAME_RYAKU;
        internal System.Windows.Forms.Label lblUnit;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
        internal System.Windows.Forms.Label lblDenpyouKbn;
        internal r_framework.CustomControl.CustomTextBox DENPYOU_KBN_NAME;
        internal System.Windows.Forms.Label lblTorihikiKbn;
        internal r_framework.CustomControl.CustomTextBox TORIHIKI_KBN_NAME;
        internal r_framework.CustomControl.CustomNumericTextBox2 DENPYOU_KBN_CD;
        internal r_framework.CustomControl.CustomNumericTextBox2 TORIHIKI_KBN_CD;
    }
}