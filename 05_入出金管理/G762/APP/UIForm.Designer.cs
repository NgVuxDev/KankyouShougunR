namespace ShukkinDataShutsuryoku
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
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            this.BANK_SHITEN_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SEARCH_BANK_SHITEN = new r_framework.CustomControl.CustomPopupOpenButton();
            this.SEARCH_BANK = new r_framework.CustomControl.CustomPopupOpenButton();
            this.KOUZA_NO = new r_framework.CustomControl.CustomTextBox();
            this.KOUZA_SHURUI = new r_framework.CustomControl.CustomTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.BANK_SHITEN_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.BANK_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.BANK_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlSHUTSURYOKU_JOUKYOU = new r_framework.CustomControl.CustomPanel();
            this.SHUTSURYOKU_JOUKYOU3 = new r_framework.CustomControl.CustomRadioButton();
            this.SHUTSURYOKU_JOUKYOU2 = new r_framework.CustomControl.CustomRadioButton();
            this.SHUTSURYOKU_JOUKYOU = new r_framework.CustomControl.CustomNumericTextBox2();
            this.SHUTSURYOKU_JOUKYOU1 = new r_framework.CustomControl.CustomRadioButton();
            this.lblSHUTSURYOKU_JOUKYOU = new System.Windows.Forms.Label();
            this.SEARCH_SHUTSURYOKU_SAKI = new r_framework.CustomControl.CustomButton();
            this.SHUTSURYOKU_SAKI = new r_framework.CustomControl.CustomTextBox();
            this.lblSHUTSURYOKU_SAKI = new System.Windows.Forms.Label();
            this.Ichiran = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.COL_FURIKOMI_BANK_CD = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.COL_FURIKOMI_BANK_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.COL_FURIKOMI_BANK_SHITEN_CD = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.COL_FURIKOMI_BANK_SHITEN_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.COL_FURIKOMI_KOUZA_SHURUI_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.COL_FURIKOMI_KOUZA_NO = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.COL_FURIKOMI_KOUZA_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.COL_TORIHIKISAKI_CD = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.COL_TORIHIKISAKI_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.COL_FURIKOMI_KINGAKU = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.lblKINGAKU_GOUKEI = new System.Windows.Forms.Label();
            this.KINGAKU_GOUKEI = new r_framework.CustomControl.CustomTextBox();
            this.KENSUU_GOUKEI = new r_framework.CustomControl.CustomTextBox();
            this.lblKENSUU_GOUKEI = new System.Windows.Forms.Label();
            this.FURIKOMI_DATE = new r_framework.CustomControl.CustomDateTimePicker();
            this.lblFURIKOMI_DATE = new System.Windows.Forms.Label();
            this.pnlKINGAKU = new r_framework.CustomControl.CustomPanel();
            this.pnlSHUTSURYOKU_JOUKYOU.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).BeginInit();
            this.pnlKINGAKU.SuspendLayout();
            this.SuspendLayout();
            // 
            // BANK_SHITEN_CD
            // 
            this.BANK_SHITEN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.BANK_SHITEN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BANK_SHITEN_CD.ChangeUpperCase = true;
            this.BANK_SHITEN_CD.CharacterLimitList = null;
            this.BANK_SHITEN_CD.CharactersNumber = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.BANK_SHITEN_CD.DBFieldsName = "";
            this.BANK_SHITEN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.BANK_SHITEN_CD.DisplayItemName = "銀行支店";
            this.BANK_SHITEN_CD.DisplayPopUp = null;
            this.BANK_SHITEN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_SHITEN_CD.FocusOutCheckMethod")));
            this.BANK_SHITEN_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.BANK_SHITEN_CD.ForeColor = System.Drawing.Color.Black;
            this.BANK_SHITEN_CD.GetCodeMasterField = ", ,BANK_SHITEN_CD,BANK_SHIETN_NAME_RYAKU,KOUZA_SHURUI,KOUZA_NO";
            this.BANK_SHITEN_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.BANK_SHITEN_CD.IsInputErrorOccured = false;
            this.BANK_SHITEN_CD.ItemDefinedTypes = "";
            this.BANK_SHITEN_CD.Location = new System.Drawing.Point(113, 29);
            this.BANK_SHITEN_CD.MaxLength = 3;
            this.BANK_SHITEN_CD.Name = "BANK_SHITEN_CD";
            this.BANK_SHITEN_CD.PopupAfterExecute = null;
            this.BANK_SHITEN_CD.PopupAfterExecuteMethod = "BankShitenPopupAfter";
            this.BANK_SHITEN_CD.PopupBeforeExecute = null;
            this.BANK_SHITEN_CD.PopupBeforeExecuteMethod = "BankShitenPopupBefore";
            this.BANK_SHITEN_CD.PopupGetMasterField = "BANK_CD,BANK_NAME_RYAKU,BANK_SHITEN_CD,BANK_SHIETN_NAME_RYAKU,KOUZA_SHURUI,KOUZA_" +
                "NO";
            this.BANK_SHITEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BANK_SHITEN_CD.PopupSearchSendParams")));
            this.BANK_SHITEN_CD.PopupSetFormField = "BANK_CD,BANK_NAME_RYAKU,BANK_SHITEN_CD,BANK_SHITEN_NAME_RYAKU,KOUZA_SHURUI,KOUZA_" +
                "NO";
            this.BANK_SHITEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_BANK_SHITEN;
            this.BANK_SHITEN_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.BANK_SHITEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BANK_SHITEN_CD.popupWindowSetting")));
            this.BANK_SHITEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_SHITEN_CD.RegistCheckMethod")));
            this.BANK_SHITEN_CD.SetFormField = ", ,BANK_SHITEN_CD,BANK_SHITEN_NAME_RYAKU,KOUZA_SHURUI,KOUZA_NO";
            this.BANK_SHITEN_CD.Size = new System.Drawing.Size(36, 20);
            this.BANK_SHITEN_CD.TabIndex = 10;
            this.BANK_SHITEN_CD.Tag = "銀行支店を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.BANK_SHITEN_CD.ZeroPaddengFlag = true;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.Font = new System.Drawing.Font("MS Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(183, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "口座番号";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SEARCH_BANK_SHITEN
            // 
            this.SEARCH_BANK_SHITEN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.SEARCH_BANK_SHITEN.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SEARCH_BANK_SHITEN.DBFieldsName = null;
            this.SEARCH_BANK_SHITEN.DefaultBackColor = System.Drawing.Color.Empty;
            this.SEARCH_BANK_SHITEN.DisplayItemName = "";
            this.SEARCH_BANK_SHITEN.DisplayPopUp = null;
            this.SEARCH_BANK_SHITEN.ErrorMessage = null;
            this.SEARCH_BANK_SHITEN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEARCH_BANK_SHITEN.FocusOutCheckMethod")));
            this.SEARCH_BANK_SHITEN.Font = new System.Drawing.Font("MS Gothic", 11.25F);
            this.SEARCH_BANK_SHITEN.GetCodeMasterField = null;
            this.SEARCH_BANK_SHITEN.Image = ((System.Drawing.Image)(resources.GetObject("SEARCH_BANK_SHITEN.Image")));
            this.SEARCH_BANK_SHITEN.ItemDefinedTypes = null;
            this.SEARCH_BANK_SHITEN.LinkedSettingTextBox = null;
            this.SEARCH_BANK_SHITEN.LinkedTextBoxs = null;
            this.SEARCH_BANK_SHITEN.Location = new System.Drawing.Point(436, 28);
            this.SEARCH_BANK_SHITEN.Name = "SEARCH_BANK_SHITEN";
            this.SEARCH_BANK_SHITEN.PopupAfterExecute = null;
            this.SEARCH_BANK_SHITEN.PopupAfterExecuteMethod = "BankShitenPopupAfter";
            this.SEARCH_BANK_SHITEN.PopupBeforeExecute = null;
            this.SEARCH_BANK_SHITEN.PopupBeforeExecuteMethod = "BankShitenPopupBefore";
            this.SEARCH_BANK_SHITEN.PopupGetMasterField = "BANK_CD,BANK_NAME_RYAKU,BANK_SHITEN_CD,BANK_SHIETN_NAME_RYAKU,KOUZA_SHURUI,KOUZA_" +
                "NO";
            this.SEARCH_BANK_SHITEN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEARCH_BANK_SHITEN.PopupSearchSendParams")));
            this.SEARCH_BANK_SHITEN.PopupSetFormField = "BANK_CD,BANK_NAME_RYAKU,BANK_SHITEN_CD,BANK_SHITEN_NAME_RYAKU,KOUZA_SHURUI,KOUZA_" +
                "NO";
            this.SEARCH_BANK_SHITEN.PopupWindowId = r_framework.Const.WINDOW_ID.M_BANK_SHITEN;
            this.SEARCH_BANK_SHITEN.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.SEARCH_BANK_SHITEN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEARCH_BANK_SHITEN.popupWindowSetting")));
            this.SEARCH_BANK_SHITEN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEARCH_BANK_SHITEN.RegistCheckMethod")));
            this.SEARCH_BANK_SHITEN.SearchDisplayFlag = 0;
            this.SEARCH_BANK_SHITEN.SetFormField = "BANK_CD,BANK_NAME_RYAKU,BANK_SHITEN_CD,BANK_SHITEN_NAME_RYAKU,KOUZA_SHURUI,KOUZA_" +
                "NO";
            this.SEARCH_BANK_SHITEN.ShortItemName = "";
            this.SEARCH_BANK_SHITEN.Size = new System.Drawing.Size(22, 22);
            this.SEARCH_BANK_SHITEN.TabIndex = 15;
            this.SEARCH_BANK_SHITEN.TabStop = false;
            this.SEARCH_BANK_SHITEN.UseVisualStyleBackColor = false;
            this.SEARCH_BANK_SHITEN.ZeroPaddengFlag = false;
            // 
            // SEARCH_BANK
            // 
            this.SEARCH_BANK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.SEARCH_BANK.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SEARCH_BANK.DBFieldsName = null;
            this.SEARCH_BANK.DefaultBackColor = System.Drawing.Color.Empty;
            this.SEARCH_BANK.DisplayItemName = "取引先CD";
            this.SEARCH_BANK.DisplayPopUp = null;
            this.SEARCH_BANK.ErrorMessage = null;
            this.SEARCH_BANK.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEARCH_BANK.FocusOutCheckMethod")));
            this.SEARCH_BANK.Font = new System.Drawing.Font("MS Gothic", 11.25F);
            this.SEARCH_BANK.GetCodeMasterField = null;
            this.SEARCH_BANK.Image = ((System.Drawing.Image)(resources.GetObject("SEARCH_BANK.Image")));
            this.SEARCH_BANK.ItemDefinedTypes = null;
            this.SEARCH_BANK.LinkedSettingTextBox = null;
            this.SEARCH_BANK.LinkedTextBoxs = null;
            this.SEARCH_BANK.Location = new System.Drawing.Point(436, 5);
            this.SEARCH_BANK.Name = "SEARCH_BANK";
            this.SEARCH_BANK.PopupAfterExecute = null;
            this.SEARCH_BANK.PopupAfterExecuteMethod = "BankPopupAfter";
            this.SEARCH_BANK.PopupBeforeExecute = null;
            this.SEARCH_BANK.PopupBeforeExecuteMethod = "BankPopupBefore";
            this.SEARCH_BANK.PopupGetMasterField = "BANK_CD,BANK_NAME_RYAKU";
            this.SEARCH_BANK.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEARCH_BANK.PopupSearchSendParams")));
            this.SEARCH_BANK.PopupSetFormField = "BANK_CD,BANK_NAME_RYAKU";
            this.SEARCH_BANK.PopupWindowId = r_framework.Const.WINDOW_ID.M_BANK;
            this.SEARCH_BANK.PopupWindowName = "マスタ共通ポップアップ";
            this.SEARCH_BANK.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEARCH_BANK.popupWindowSetting")));
            this.SEARCH_BANK.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEARCH_BANK.RegistCheckMethod")));
            this.SEARCH_BANK.SearchDisplayFlag = 0;
            this.SEARCH_BANK.SetFormField = "BANK_CD,BANK_NAME_RYAKU";
            this.SEARCH_BANK.ShortItemName = "取引先CD";
            this.SEARCH_BANK.Size = new System.Drawing.Size(22, 22);
            this.SEARCH_BANK.TabIndex = 5;
            this.SEARCH_BANK.TabStop = false;
            this.SEARCH_BANK.UseVisualStyleBackColor = false;
            this.SEARCH_BANK.ZeroPaddengFlag = false;
            // 
            // KOUZA_NO
            // 
            this.KOUZA_NO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KOUZA_NO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KOUZA_NO.DBFieldsName = "";
            this.KOUZA_NO.DefaultBackColor = System.Drawing.Color.Empty;
            this.KOUZA_NO.DisplayItemName = "";
            this.KOUZA_NO.DisplayPopUp = null;
            this.KOUZA_NO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_NO.FocusOutCheckMethod")));
            this.KOUZA_NO.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.KOUZA_NO.ForeColor = System.Drawing.Color.Black;
            this.KOUZA_NO.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.KOUZA_NO.IsInputErrorOccured = false;
            this.KOUZA_NO.ItemDefinedTypes = "";
            this.KOUZA_NO.Location = new System.Drawing.Point(295, 52);
            this.KOUZA_NO.Name = "KOUZA_NO";
            this.KOUZA_NO.PopupAfterExecute = null;
            this.KOUZA_NO.PopupBeforeExecute = null;
            this.KOUZA_NO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KOUZA_NO.PopupSearchSendParams")));
            this.KOUZA_NO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KOUZA_NO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KOUZA_NO.popupWindowSetting")));
            this.KOUZA_NO.ReadOnly = true;
            this.KOUZA_NO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_NO.RegistCheckMethod")));
            this.KOUZA_NO.Size = new System.Drawing.Size(64, 20);
            this.KOUZA_NO.TabIndex = 11;
            this.KOUZA_NO.TabStop = false;
            this.KOUZA_NO.Tag = " ";
            // 
            // KOUZA_SHURUI
            // 
            this.KOUZA_SHURUI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KOUZA_SHURUI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KOUZA_SHURUI.DBFieldsName = "KOUZA_SHURUI";
            this.KOUZA_SHURUI.DefaultBackColor = System.Drawing.Color.Empty;
            this.KOUZA_SHURUI.DisplayItemName = "口座種類";
            this.KOUZA_SHURUI.DisplayPopUp = null;
            this.KOUZA_SHURUI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_SHURUI.FocusOutCheckMethod")));
            this.KOUZA_SHURUI.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.KOUZA_SHURUI.ForeColor = System.Drawing.Color.Black;
            this.KOUZA_SHURUI.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.KOUZA_SHURUI.IsInputErrorOccured = false;
            this.KOUZA_SHURUI.ItemDefinedTypes = "VARCHAR";
            this.KOUZA_SHURUI.Location = new System.Drawing.Point(113, 52);
            this.KOUZA_SHURUI.Name = "KOUZA_SHURUI";
            this.KOUZA_SHURUI.PopupAfterExecute = null;
            this.KOUZA_SHURUI.PopupBeforeExecute = null;
            this.KOUZA_SHURUI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KOUZA_SHURUI.PopupSearchSendParams")));
            this.KOUZA_SHURUI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KOUZA_SHURUI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KOUZA_SHURUI.popupWindowSetting")));
            this.KOUZA_SHURUI.ReadOnly = true;
            this.KOUZA_SHURUI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_SHURUI.RegistCheckMethod")));
            this.KOUZA_SHURUI.Size = new System.Drawing.Size(63, 20);
            this.KOUZA_SHURUI.TabIndex = 9;
            this.KOUZA_SHURUI.TabStop = false;
            this.KOUZA_SHURUI.Tag = " ";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label6.Font = new System.Drawing.Font("MS Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(3, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 20);
            this.label6.TabIndex = 8;
            this.label6.Text = "口座種類";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BANK_SHITEN_NAME_RYAKU
            // 
            this.BANK_SHITEN_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.BANK_SHITEN_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BANK_SHITEN_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.BANK_SHITEN_NAME_RYAKU.DisplayPopUp = null;
            this.BANK_SHITEN_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_SHITEN_NAME_RYAKU.FocusOutCheckMethod")));
            this.BANK_SHITEN_NAME_RYAKU.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.BANK_SHITEN_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.BANK_SHITEN_NAME_RYAKU.IsInputErrorOccured = false;
            this.BANK_SHITEN_NAME_RYAKU.Location = new System.Drawing.Point(148, 29);
            this.BANK_SHITEN_NAME_RYAKU.Name = "BANK_SHITEN_NAME_RYAKU";
            this.BANK_SHITEN_NAME_RYAKU.PopupAfterExecute = null;
            this.BANK_SHITEN_NAME_RYAKU.PopupBeforeExecute = null;
            this.BANK_SHITEN_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BANK_SHITEN_NAME_RYAKU.PopupSearchSendParams")));
            this.BANK_SHITEN_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BANK_SHITEN_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BANK_SHITEN_NAME_RYAKU.popupWindowSetting")));
            this.BANK_SHITEN_NAME_RYAKU.ReadOnly = true;
            this.BANK_SHITEN_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_SHITEN_NAME_RYAKU.RegistCheckMethod")));
            this.BANK_SHITEN_NAME_RYAKU.Size = new System.Drawing.Size(286, 20);
            this.BANK_SHITEN_NAME_RYAKU.TabIndex = 6;
            this.BANK_SHITEN_NAME_RYAKU.TabStop = false;
            this.BANK_SHITEN_NAME_RYAKU.Tag = "";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "銀行支店";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BANK_CD
            // 
            this.BANK_CD.BackColor = System.Drawing.SystemColors.Window;
            this.BANK_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BANK_CD.ChangeUpperCase = true;
            this.BANK_CD.CharacterLimitList = null;
            this.BANK_CD.CharactersNumber = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.BANK_CD.DBFieldsName = "BANK_CD";
            this.BANK_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.BANK_CD.DisplayItemName = "銀行";
            this.BANK_CD.DisplayPopUp = null;
            this.BANK_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_CD.FocusOutCheckMethod")));
            this.BANK_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.BANK_CD.ForeColor = System.Drawing.Color.Black;
            this.BANK_CD.GetCodeMasterField = "BANK_CD,BANK_NAME_RYAKU";
            this.BANK_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.BANK_CD.IsInputErrorOccured = false;
            this.BANK_CD.ItemDefinedTypes = "varchar";
            this.BANK_CD.Location = new System.Drawing.Point(113, 6);
            this.BANK_CD.MaxLength = 4;
            this.BANK_CD.Name = "BANK_CD";
            this.BANK_CD.PopupAfterExecute = null;
            this.BANK_CD.PopupAfterExecuteMethod = "BankPopupAfter";
            this.BANK_CD.PopupBeforeExecute = null;
            this.BANK_CD.PopupBeforeExecuteMethod = "BankPopupBefore";
            this.BANK_CD.PopupGetMasterField = "BANK_CD,BANK_NAME_RYAKU";
            this.BANK_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BANK_CD.PopupSearchSendParams")));
            this.BANK_CD.PopupSetFormField = "BANK_CD,BANK_NAME_RYAKU";
            this.BANK_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_BANK;
            this.BANK_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.BANK_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BANK_CD.popupWindowSetting")));
            this.BANK_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_CD.RegistCheckMethod")));
            this.BANK_CD.SetFormField = "BANK_CD,BANK_NAME_RYAKU";
            this.BANK_CD.Size = new System.Drawing.Size(36, 20);
            this.BANK_CD.TabIndex = 1;
            this.BANK_CD.Tag = "銀行を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.BANK_CD.ZeroPaddengFlag = true;
            // 
            // BANK_NAME_RYAKU
            // 
            this.BANK_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.BANK_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BANK_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.BANK_NAME_RYAKU.DisplayPopUp = null;
            this.BANK_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_NAME_RYAKU.FocusOutCheckMethod")));
            this.BANK_NAME_RYAKU.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.BANK_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.BANK_NAME_RYAKU.IsInputErrorOccured = false;
            this.BANK_NAME_RYAKU.Location = new System.Drawing.Point(148, 6);
            this.BANK_NAME_RYAKU.Name = "BANK_NAME_RYAKU";
            this.BANK_NAME_RYAKU.PopupAfterExecute = null;
            this.BANK_NAME_RYAKU.PopupBeforeExecute = null;
            this.BANK_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BANK_NAME_RYAKU.PopupSearchSendParams")));
            this.BANK_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BANK_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BANK_NAME_RYAKU.popupWindowSetting")));
            this.BANK_NAME_RYAKU.ReadOnly = true;
            this.BANK_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_NAME_RYAKU.RegistCheckMethod")));
            this.BANK_NAME_RYAKU.Size = new System.Drawing.Size(286, 20);
            this.BANK_NAME_RYAKU.TabIndex = 2;
            this.BANK_NAME_RYAKU.TabStop = false;
            this.BANK_NAME_RYAKU.Tag = "";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "銀行";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlSHUTSURYOKU_JOUKYOU
            // 
            this.pnlSHUTSURYOKU_JOUKYOU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSHUTSURYOKU_JOUKYOU.Controls.Add(this.SHUTSURYOKU_JOUKYOU3);
            this.pnlSHUTSURYOKU_JOUKYOU.Controls.Add(this.SHUTSURYOKU_JOUKYOU2);
            this.pnlSHUTSURYOKU_JOUKYOU.Controls.Add(this.SHUTSURYOKU_JOUKYOU);
            this.pnlSHUTSURYOKU_JOUKYOU.Controls.Add(this.SHUTSURYOKU_JOUKYOU1);
            this.pnlSHUTSURYOKU_JOUKYOU.Location = new System.Drawing.Point(113, 98);
            this.pnlSHUTSURYOKU_JOUKYOU.Name = "pnlSHUTSURYOKU_JOUKYOU";
            this.pnlSHUTSURYOKU_JOUKYOU.Size = new System.Drawing.Size(345, 20);
            this.pnlSHUTSURYOKU_JOUKYOU.TabIndex = 30;
            // 
            // SHUTSURYOKU_JOUKYOU3
            // 
            this.SHUTSURYOKU_JOUKYOU3.AutoSize = true;
            this.SHUTSURYOKU_JOUKYOU3.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUTSURYOKU_JOUKYOU3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUTSURYOKU_JOUKYOU3.FocusOutCheckMethod")));
            this.SHUTSURYOKU_JOUKYOU3.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.SHUTSURYOKU_JOUKYOU3.LinkedTextBox = "SHUTSURYOKU_JOUKYOU";
            this.SHUTSURYOKU_JOUKYOU3.Location = new System.Drawing.Point(207, 0);
            this.SHUTSURYOKU_JOUKYOU3.Name = "SHUTSURYOKU_JOUKYOU3";
            this.SHUTSURYOKU_JOUKYOU3.PopupAfterExecute = null;
            this.SHUTSURYOKU_JOUKYOU3.PopupBeforeExecute = null;
            this.SHUTSURYOKU_JOUKYOU3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUTSURYOKU_JOUKYOU3.PopupSearchSendParams")));
            this.SHUTSURYOKU_JOUKYOU3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUTSURYOKU_JOUKYOU3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUTSURYOKU_JOUKYOU3.popupWindowSetting")));
            this.SHUTSURYOKU_JOUKYOU3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUTSURYOKU_JOUKYOU3.RegistCheckMethod")));
            this.SHUTSURYOKU_JOUKYOU3.Size = new System.Drawing.Size(67, 17);
            this.SHUTSURYOKU_JOUKYOU3.TabIndex = 70;
            this.SHUTSURYOKU_JOUKYOU3.Tag = "出力状況が「3.全て」の場合には、チェックを付けてください";
            this.SHUTSURYOKU_JOUKYOU3.Text = "3.全て";
            this.SHUTSURYOKU_JOUKYOU3.UseVisualStyleBackColor = true;
            this.SHUTSURYOKU_JOUKYOU3.Value = "3";
            // 
            // SHUTSURYOKU_JOUKYOU2
            // 
            this.SHUTSURYOKU_JOUKYOU2.AutoSize = true;
            this.SHUTSURYOKU_JOUKYOU2.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUTSURYOKU_JOUKYOU2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUTSURYOKU_JOUKYOU2.FocusOutCheckMethod")));
            this.SHUTSURYOKU_JOUKYOU2.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.SHUTSURYOKU_JOUKYOU2.LinkedTextBox = "SHUTSURYOKU_JOUKYOU";
            this.SHUTSURYOKU_JOUKYOU2.Location = new System.Drawing.Point(117, 0);
            this.SHUTSURYOKU_JOUKYOU2.Name = "SHUTSURYOKU_JOUKYOU2";
            this.SHUTSURYOKU_JOUKYOU2.PopupAfterExecute = null;
            this.SHUTSURYOKU_JOUKYOU2.PopupBeforeExecute = null;
            this.SHUTSURYOKU_JOUKYOU2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUTSURYOKU_JOUKYOU2.PopupSearchSendParams")));
            this.SHUTSURYOKU_JOUKYOU2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUTSURYOKU_JOUKYOU2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUTSURYOKU_JOUKYOU2.popupWindowSetting")));
            this.SHUTSURYOKU_JOUKYOU2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUTSURYOKU_JOUKYOU2.RegistCheckMethod")));
            this.SHUTSURYOKU_JOUKYOU2.Size = new System.Drawing.Size(81, 17);
            this.SHUTSURYOKU_JOUKYOU2.TabIndex = 60;
            this.SHUTSURYOKU_JOUKYOU2.Tag = "出力状況が「2.出力済」の場合には、チェックを付けてください";
            this.SHUTSURYOKU_JOUKYOU2.Text = "2.出力済";
            this.SHUTSURYOKU_JOUKYOU2.UseVisualStyleBackColor = true;
            this.SHUTSURYOKU_JOUKYOU2.Value = "2";
            // 
            // SHUTSURYOKU_JOUKYOU
            // 
            this.SHUTSURYOKU_JOUKYOU.BackColor = System.Drawing.SystemColors.Window;
            this.SHUTSURYOKU_JOUKYOU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHUTSURYOKU_JOUKYOU.CharacterLimitList = new char[] {
        '1',
        '2',
        '3'};
            this.SHUTSURYOKU_JOUKYOU.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUTSURYOKU_JOUKYOU.DisplayItemName = "出力状況";
            this.SHUTSURYOKU_JOUKYOU.DisplayPopUp = null;
            this.SHUTSURYOKU_JOUKYOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUTSURYOKU_JOUKYOU.FocusOutCheckMethod")));
            this.SHUTSURYOKU_JOUKYOU.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.SHUTSURYOKU_JOUKYOU.ForeColor = System.Drawing.Color.Black;
            this.SHUTSURYOKU_JOUKYOU.IsInputErrorOccured = false;
            this.SHUTSURYOKU_JOUKYOU.LinkedRadioButtonArray = new string[] {
        "SHUTSURYOKU_JOUKYOU1",
        "SHUTSURYOKU_JOUKYOU2",
        "SHUTSURYOKU_JOUKYOU3"};
            this.SHUTSURYOKU_JOUKYOU.Location = new System.Drawing.Point(-1, -1);
            this.SHUTSURYOKU_JOUKYOU.Name = "SHUTSURYOKU_JOUKYOU";
            this.SHUTSURYOKU_JOUKYOU.PopupAfterExecute = null;
            this.SHUTSURYOKU_JOUKYOU.PopupBeforeExecute = null;
            this.SHUTSURYOKU_JOUKYOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUTSURYOKU_JOUKYOU.PopupSearchSendParams")));
            this.SHUTSURYOKU_JOUKYOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUTSURYOKU_JOUKYOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUTSURYOKU_JOUKYOU.popupWindowSetting")));
            rangeSettingDto4.Max = new decimal(new int[] {
            3,
            0,
            0,
            0});
            rangeSettingDto4.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SHUTSURYOKU_JOUKYOU.RangeSetting = rangeSettingDto4;
            this.SHUTSURYOKU_JOUKYOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUTSURYOKU_JOUKYOU.RegistCheckMethod")));
            this.SHUTSURYOKU_JOUKYOU.ShortItemName = "出力状況";
            this.SHUTSURYOKU_JOUKYOU.Size = new System.Drawing.Size(20, 20);
            this.SHUTSURYOKU_JOUKYOU.TabIndex = 40;
            this.SHUTSURYOKU_JOUKYOU.Tag = "【1～3】のいずれかで入力してください";
            this.SHUTSURYOKU_JOUKYOU.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.SHUTSURYOKU_JOUKYOU.WordWrap = false;
            // 
            // SHUTSURYOKU_JOUKYOU1
            // 
            this.SHUTSURYOKU_JOUKYOU1.AutoSize = true;
            this.SHUTSURYOKU_JOUKYOU1.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUTSURYOKU_JOUKYOU1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUTSURYOKU_JOUKYOU1.FocusOutCheckMethod")));
            this.SHUTSURYOKU_JOUKYOU1.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.SHUTSURYOKU_JOUKYOU1.LinkedTextBox = "SHUTSURYOKU_JOUKYOU";
            this.SHUTSURYOKU_JOUKYOU1.Location = new System.Drawing.Point(25, 0);
            this.SHUTSURYOKU_JOUKYOU1.Name = "SHUTSURYOKU_JOUKYOU1";
            this.SHUTSURYOKU_JOUKYOU1.PopupAfterExecute = null;
            this.SHUTSURYOKU_JOUKYOU1.PopupBeforeExecute = null;
            this.SHUTSURYOKU_JOUKYOU1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUTSURYOKU_JOUKYOU1.PopupSearchSendParams")));
            this.SHUTSURYOKU_JOUKYOU1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUTSURYOKU_JOUKYOU1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUTSURYOKU_JOUKYOU1.popupWindowSetting")));
            this.SHUTSURYOKU_JOUKYOU1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUTSURYOKU_JOUKYOU1.RegistCheckMethod")));
            this.SHUTSURYOKU_JOUKYOU1.Size = new System.Drawing.Size(81, 17);
            this.SHUTSURYOKU_JOUKYOU1.TabIndex = 50;
            this.SHUTSURYOKU_JOUKYOU1.Tag = "出力状況が「1.未出力」の場合には、チェックを付けてください";
            this.SHUTSURYOKU_JOUKYOU1.Text = "1.未出力";
            this.SHUTSURYOKU_JOUKYOU1.UseVisualStyleBackColor = true;
            this.SHUTSURYOKU_JOUKYOU1.Value = "1";
            // 
            // lblSHUTSURYOKU_JOUKYOU
            // 
            this.lblSHUTSURYOKU_JOUKYOU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblSHUTSURYOKU_JOUKYOU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSHUTSURYOKU_JOUKYOU.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.lblSHUTSURYOKU_JOUKYOU.ForeColor = System.Drawing.Color.White;
            this.lblSHUTSURYOKU_JOUKYOU.Location = new System.Drawing.Point(3, 98);
            this.lblSHUTSURYOKU_JOUKYOU.Name = "lblSHUTSURYOKU_JOUKYOU";
            this.lblSHUTSURYOKU_JOUKYOU.Size = new System.Drawing.Size(106, 20);
            this.lblSHUTSURYOKU_JOUKYOU.TabIndex = 14;
            this.lblSHUTSURYOKU_JOUKYOU.Text = "出力状況※";
            this.lblSHUTSURYOKU_JOUKYOU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SEARCH_SHUTSURYOKU_SAKI
            // 
            this.SEARCH_SHUTSURYOKU_SAKI.DefaultBackColor = System.Drawing.Color.Empty;
            this.SEARCH_SHUTSURYOKU_SAKI.Location = new System.Drawing.Point(460, 120);
            this.SEARCH_SHUTSURYOKU_SAKI.Name = "SEARCH_SHUTSURYOKU_SAKI";
            this.SEARCH_SHUTSURYOKU_SAKI.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.SEARCH_SHUTSURYOKU_SAKI.Size = new System.Drawing.Size(50, 22);
            this.SEARCH_SHUTSURYOKU_SAKI.TabIndex = 90;
            this.SEARCH_SHUTSURYOKU_SAKI.Tag = "";
            this.SEARCH_SHUTSURYOKU_SAKI.Text = "参照";
            this.SEARCH_SHUTSURYOKU_SAKI.UseVisualStyleBackColor = true;
            // 
            // SHUTSURYOKU_SAKI
            // 
            this.SHUTSURYOKU_SAKI.BackColor = System.Drawing.SystemColors.Window;
            this.SHUTSURYOKU_SAKI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHUTSURYOKU_SAKI.CharactersNumber = new decimal(new int[] {
            65,
            0,
            0,
            0});
            this.SHUTSURYOKU_SAKI.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUTSURYOKU_SAKI.DisplayItemName = "出力先";
            this.SHUTSURYOKU_SAKI.DisplayPopUp = null;
            this.SHUTSURYOKU_SAKI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUTSURYOKU_SAKI.FocusOutCheckMethod")));
            this.SHUTSURYOKU_SAKI.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.SHUTSURYOKU_SAKI.ForeColor = System.Drawing.Color.Black;
            this.SHUTSURYOKU_SAKI.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.SHUTSURYOKU_SAKI.IsInputErrorOccured = false;
            this.SHUTSURYOKU_SAKI.Location = new System.Drawing.Point(113, 121);
            this.SHUTSURYOKU_SAKI.MaxLength = 65;
            this.SHUTSURYOKU_SAKI.Name = "SHUTSURYOKU_SAKI";
            this.SHUTSURYOKU_SAKI.PopupAfterExecute = null;
            this.SHUTSURYOKU_SAKI.PopupBeforeExecute = null;
            this.SHUTSURYOKU_SAKI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUTSURYOKU_SAKI.PopupSearchSendParams")));
            this.SHUTSURYOKU_SAKI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUTSURYOKU_SAKI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUTSURYOKU_SAKI.popupWindowSetting")));
            this.SHUTSURYOKU_SAKI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUTSURYOKU_SAKI.RegistCheckMethod")));
            this.SHUTSURYOKU_SAKI.Size = new System.Drawing.Size(345, 20);
            this.SHUTSURYOKU_SAKI.TabIndex = 80;
            this.SHUTSURYOKU_SAKI.Tag = " ";
            // 
            // lblSHUTSURYOKU_SAKI
            // 
            this.lblSHUTSURYOKU_SAKI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblSHUTSURYOKU_SAKI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSHUTSURYOKU_SAKI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSHUTSURYOKU_SAKI.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSHUTSURYOKU_SAKI.ForeColor = System.Drawing.Color.White;
            this.lblSHUTSURYOKU_SAKI.Location = new System.Drawing.Point(3, 121);
            this.lblSHUTSURYOKU_SAKI.Name = "lblSHUTSURYOKU_SAKI";
            this.lblSHUTSURYOKU_SAKI.Size = new System.Drawing.Size(106, 20);
            this.lblSHUTSURYOKU_SAKI.TabIndex = 16;
            this.lblSHUTSURYOKU_SAKI.Text = "出力先※";
            this.lblSHUTSURYOKU_SAKI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Ichiran
            // 
            this.Ichiran.AllowUserToAddRows = false;
            this.Ichiran.AllowUserToDeleteRows = false;
            this.Ichiran.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle27.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle27.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle27.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle27.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Ichiran.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle27;
            this.Ichiran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Ichiran.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.COL_FURIKOMI_BANK_CD,
            this.COL_FURIKOMI_BANK_NAME,
            this.COL_FURIKOMI_BANK_SHITEN_CD,
            this.COL_FURIKOMI_BANK_SHITEN_NAME,
            this.COL_FURIKOMI_KOUZA_SHURUI_NAME,
            this.COL_FURIKOMI_KOUZA_NO,
            this.COL_FURIKOMI_KOUZA_NAME,
            this.COL_TORIHIKISAKI_CD,
            this.COL_TORIHIKISAKI_NAME,
            this.COL_FURIKOMI_KINGAKU});
            dataGridViewCellStyle38.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle38.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle38.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle38.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle38.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle38.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle38.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Ichiran.DefaultCellStyle = dataGridViewCellStyle38;
            this.Ichiran.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.Ichiran.EnableHeadersVisualStyles = false;
            this.Ichiran.GridColor = System.Drawing.Color.White;
            this.Ichiran.IsReload = false;
            this.Ichiran.LinkedDataPanelName = "customSortHeader1";
            this.Ichiran.Location = new System.Drawing.Point(3, 147);
            this.Ichiran.MultiSelect = false;
            this.Ichiran.Name = "Ichiran";
            dataGridViewCellStyle39.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle39.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle39.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle39.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle39.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle39.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Ichiran.RowHeadersDefaultCellStyle = dataGridViewCellStyle39;
            this.Ichiran.RowHeadersVisible = false;
            this.Ichiran.RowTemplate.Height = 21;
            this.Ichiran.ShowCellToolTips = false;
            this.Ichiran.Size = new System.Drawing.Size(992, 286);
            this.Ichiran.TabIndex = 100;
            // 
            // COL_FURIKOMI_BANK_CD
            // 
            this.COL_FURIKOMI_BANK_CD.DataPropertyName = "FURIKOMI_BANK_CD";
            this.COL_FURIKOMI_BANK_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle28.SelectionForeColor = System.Drawing.Color.Black;
            this.COL_FURIKOMI_BANK_CD.DefaultCellStyle = dataGridViewCellStyle28;
            this.COL_FURIKOMI_BANK_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COL_FURIKOMI_BANK_CD.FocusOutCheckMethod")));
            this.COL_FURIKOMI_BANK_CD.HeaderText = "振込先銀行CD";
            this.COL_FURIKOMI_BANK_CD.Name = "COL_FURIKOMI_BANK_CD";
            this.COL_FURIKOMI_BANK_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("COL_FURIKOMI_BANK_CD.PopupSearchSendParams")));
            this.COL_FURIKOMI_BANK_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.COL_FURIKOMI_BANK_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("COL_FURIKOMI_BANK_CD.popupWindowSetting")));
            this.COL_FURIKOMI_BANK_CD.ReadOnly = true;
            this.COL_FURIKOMI_BANK_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COL_FURIKOMI_BANK_CD.RegistCheckMethod")));
            this.COL_FURIKOMI_BANK_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // COL_FURIKOMI_BANK_NAME
            // 
            this.COL_FURIKOMI_BANK_NAME.DataPropertyName = "FURIKOMI_BANK_NAME";
            this.COL_FURIKOMI_BANK_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle29.SelectionForeColor = System.Drawing.Color.Black;
            this.COL_FURIKOMI_BANK_NAME.DefaultCellStyle = dataGridViewCellStyle29;
            this.COL_FURIKOMI_BANK_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COL_FURIKOMI_BANK_NAME.FocusOutCheckMethod")));
            this.COL_FURIKOMI_BANK_NAME.HeaderText = "振込先銀行名";
            this.COL_FURIKOMI_BANK_NAME.Name = "COL_FURIKOMI_BANK_NAME";
            this.COL_FURIKOMI_BANK_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("COL_FURIKOMI_BANK_NAME.PopupSearchSendParams")));
            this.COL_FURIKOMI_BANK_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.COL_FURIKOMI_BANK_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("COL_FURIKOMI_BANK_NAME.popupWindowSetting")));
            this.COL_FURIKOMI_BANK_NAME.ReadOnly = true;
            this.COL_FURIKOMI_BANK_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COL_FURIKOMI_BANK_NAME.RegistCheckMethod")));
            this.COL_FURIKOMI_BANK_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.COL_FURIKOMI_BANK_NAME.Width = 150;
            // 
            // COL_FURIKOMI_BANK_SHITEN_CD
            // 
            this.COL_FURIKOMI_BANK_SHITEN_CD.DataPropertyName = "FURIKOMI_BANK_SHITEN_CD";
            this.COL_FURIKOMI_BANK_SHITEN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle30.SelectionForeColor = System.Drawing.Color.Black;
            this.COL_FURIKOMI_BANK_SHITEN_CD.DefaultCellStyle = dataGridViewCellStyle30;
            this.COL_FURIKOMI_BANK_SHITEN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COL_FURIKOMI_BANK_SHITEN_CD.FocusOutCheckMethod")));
            this.COL_FURIKOMI_BANK_SHITEN_CD.HeaderText = "振込先支店CD";
            this.COL_FURIKOMI_BANK_SHITEN_CD.Name = "COL_FURIKOMI_BANK_SHITEN_CD";
            this.COL_FURIKOMI_BANK_SHITEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("COL_FURIKOMI_BANK_SHITEN_CD.PopupSearchSendParams")));
            this.COL_FURIKOMI_BANK_SHITEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.COL_FURIKOMI_BANK_SHITEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("COL_FURIKOMI_BANK_SHITEN_CD.popupWindowSetting")));
            this.COL_FURIKOMI_BANK_SHITEN_CD.ReadOnly = true;
            this.COL_FURIKOMI_BANK_SHITEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COL_FURIKOMI_BANK_SHITEN_CD.RegistCheckMethod")));
            this.COL_FURIKOMI_BANK_SHITEN_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // COL_FURIKOMI_BANK_SHITEN_NAME
            // 
            this.COL_FURIKOMI_BANK_SHITEN_NAME.DataPropertyName = "FURIKOMI_BANK_SHITEN_NAME";
            this.COL_FURIKOMI_BANK_SHITEN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle31.SelectionForeColor = System.Drawing.Color.Black;
            this.COL_FURIKOMI_BANK_SHITEN_NAME.DefaultCellStyle = dataGridViewCellStyle31;
            this.COL_FURIKOMI_BANK_SHITEN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COL_FURIKOMI_BANK_SHITEN_NAME.FocusOutCheckMethod")));
            this.COL_FURIKOMI_BANK_SHITEN_NAME.HeaderText = "振込先支店名";
            this.COL_FURIKOMI_BANK_SHITEN_NAME.Name = "COL_FURIKOMI_BANK_SHITEN_NAME";
            this.COL_FURIKOMI_BANK_SHITEN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("COL_FURIKOMI_BANK_SHITEN_NAME.PopupSearchSendParams")));
            this.COL_FURIKOMI_BANK_SHITEN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.COL_FURIKOMI_BANK_SHITEN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("COL_FURIKOMI_BANK_SHITEN_NAME.popupWindowSetting")));
            this.COL_FURIKOMI_BANK_SHITEN_NAME.ReadOnly = true;
            this.COL_FURIKOMI_BANK_SHITEN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COL_FURIKOMI_BANK_SHITEN_NAME.RegistCheckMethod")));
            this.COL_FURIKOMI_BANK_SHITEN_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.COL_FURIKOMI_BANK_SHITEN_NAME.Width = 150;
            // 
            // COL_FURIKOMI_KOUZA_SHURUI_NAME
            // 
            this.COL_FURIKOMI_KOUZA_SHURUI_NAME.DataPropertyName = "FURIKOMI_KOUZA_SHURUI_NAME";
            this.COL_FURIKOMI_KOUZA_SHURUI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle32.SelectionForeColor = System.Drawing.Color.Black;
            this.COL_FURIKOMI_KOUZA_SHURUI_NAME.DefaultCellStyle = dataGridViewCellStyle32;
            this.COL_FURIKOMI_KOUZA_SHURUI_NAME.FocusOutCheckMethod = null;
            this.COL_FURIKOMI_KOUZA_SHURUI_NAME.HeaderText = "振込先口座種類";
            this.COL_FURIKOMI_KOUZA_SHURUI_NAME.Name = "COL_FURIKOMI_KOUZA_SHURUI_NAME";
            this.COL_FURIKOMI_KOUZA_SHURUI_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("COL_FURIKOMI_KOUZA_SHURUI_NAME.PopupSearchSendParams")));
            this.COL_FURIKOMI_KOUZA_SHURUI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.COL_FURIKOMI_KOUZA_SHURUI_NAME.popupWindowSetting = null;
            this.COL_FURIKOMI_KOUZA_SHURUI_NAME.ReadOnly = true;
            this.COL_FURIKOMI_KOUZA_SHURUI_NAME.RegistCheckMethod = null;
            this.COL_FURIKOMI_KOUZA_SHURUI_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.COL_FURIKOMI_KOUZA_SHURUI_NAME.Width = 120;
            // 
            // COL_FURIKOMI_KOUZA_NO
            // 
            this.COL_FURIKOMI_KOUZA_NO.DataPropertyName = "FURIKOMI_KOUZA_NO";
            this.COL_FURIKOMI_KOUZA_NO.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle33.SelectionForeColor = System.Drawing.Color.Black;
            this.COL_FURIKOMI_KOUZA_NO.DefaultCellStyle = dataGridViewCellStyle33;
            this.COL_FURIKOMI_KOUZA_NO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COL_FURIKOMI_KOUZA_NO.FocusOutCheckMethod")));
            this.COL_FURIKOMI_KOUZA_NO.HeaderText = "振込先口座番号";
            this.COL_FURIKOMI_KOUZA_NO.Name = "COL_FURIKOMI_KOUZA_NO";
            this.COL_FURIKOMI_KOUZA_NO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("COL_FURIKOMI_KOUZA_NO.PopupSearchSendParams")));
            this.COL_FURIKOMI_KOUZA_NO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.COL_FURIKOMI_KOUZA_NO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("COL_FURIKOMI_KOUZA_NO.popupWindowSetting")));
            this.COL_FURIKOMI_KOUZA_NO.ReadOnly = true;
            this.COL_FURIKOMI_KOUZA_NO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COL_FURIKOMI_KOUZA_NO.RegistCheckMethod")));
            this.COL_FURIKOMI_KOUZA_NO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.COL_FURIKOMI_KOUZA_NO.Width = 120;
            // 
            // COL_FURIKOMI_KOUZA_NAME
            // 
            this.COL_FURIKOMI_KOUZA_NAME.DataPropertyName = "FURIKOMI_KOUZA_NAME";
            this.COL_FURIKOMI_KOUZA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle34.SelectionForeColor = System.Drawing.Color.Black;
            this.COL_FURIKOMI_KOUZA_NAME.DefaultCellStyle = dataGridViewCellStyle34;
            this.COL_FURIKOMI_KOUZA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COL_FURIKOMI_KOUZA_NAME.FocusOutCheckMethod")));
            this.COL_FURIKOMI_KOUZA_NAME.HeaderText = "振込先口座名";
            this.COL_FURIKOMI_KOUZA_NAME.Name = "COL_FURIKOMI_KOUZA_NAME";
            this.COL_FURIKOMI_KOUZA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("COL_FURIKOMI_KOUZA_NAME.PopupSearchSendParams")));
            this.COL_FURIKOMI_KOUZA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.COL_FURIKOMI_KOUZA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("COL_FURIKOMI_KOUZA_NAME.popupWindowSetting")));
            this.COL_FURIKOMI_KOUZA_NAME.ReadOnly = true;
            this.COL_FURIKOMI_KOUZA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COL_FURIKOMI_KOUZA_NAME.RegistCheckMethod")));
            this.COL_FURIKOMI_KOUZA_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.COL_FURIKOMI_KOUZA_NAME.Width = 150;
            // 
            // COL_TORIHIKISAKI_CD
            // 
            this.COL_TORIHIKISAKI_CD.DataPropertyName = "TORIHIKISAKI_CD";
            this.COL_TORIHIKISAKI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle35.SelectionForeColor = System.Drawing.Color.Black;
            this.COL_TORIHIKISAKI_CD.DefaultCellStyle = dataGridViewCellStyle35;
            this.COL_TORIHIKISAKI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COL_TORIHIKISAKI_CD.FocusOutCheckMethod")));
            this.COL_TORIHIKISAKI_CD.HeaderText = "取引先CD";
            this.COL_TORIHIKISAKI_CD.Name = "COL_TORIHIKISAKI_CD";
            this.COL_TORIHIKISAKI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("COL_TORIHIKISAKI_CD.PopupSearchSendParams")));
            this.COL_TORIHIKISAKI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.COL_TORIHIKISAKI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("COL_TORIHIKISAKI_CD.popupWindowSetting")));
            this.COL_TORIHIKISAKI_CD.ReadOnly = true;
            this.COL_TORIHIKISAKI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COL_TORIHIKISAKI_CD.RegistCheckMethod")));
            this.COL_TORIHIKISAKI_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.COL_TORIHIKISAKI_CD.Width = 80;
            // 
            // COL_TORIHIKISAKI_NAME
            // 
            this.COL_TORIHIKISAKI_NAME.DataPropertyName = "TORIHIKISAKI_NAME";
            this.COL_TORIHIKISAKI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle36.SelectionForeColor = System.Drawing.Color.Black;
            this.COL_TORIHIKISAKI_NAME.DefaultCellStyle = dataGridViewCellStyle36;
            this.COL_TORIHIKISAKI_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COL_TORIHIKISAKI_NAME.FocusOutCheckMethod")));
            this.COL_TORIHIKISAKI_NAME.HeaderText = "取引先名";
            this.COL_TORIHIKISAKI_NAME.Name = "COL_TORIHIKISAKI_NAME";
            this.COL_TORIHIKISAKI_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("COL_TORIHIKISAKI_NAME.PopupSearchSendParams")));
            this.COL_TORIHIKISAKI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.COL_TORIHIKISAKI_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("COL_TORIHIKISAKI_NAME.popupWindowSetting")));
            this.COL_TORIHIKISAKI_NAME.ReadOnly = true;
            this.COL_TORIHIKISAKI_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COL_TORIHIKISAKI_NAME.RegistCheckMethod")));
            this.COL_TORIHIKISAKI_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.COL_TORIHIKISAKI_NAME.Width = 150;
            // 
            // COL_FURIKOMI_KINGAKU
            // 
            this.COL_FURIKOMI_KINGAKU.CustomFormatSetting = "#,##0";
            this.COL_FURIKOMI_KINGAKU.DataPropertyName = "FURIKOMI_KINGAKU";
            this.COL_FURIKOMI_KINGAKU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle37.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle37.SelectionForeColor = System.Drawing.Color.Black;
            this.COL_FURIKOMI_KINGAKU.DefaultCellStyle = dataGridViewCellStyle37;
            this.COL_FURIKOMI_KINGAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COL_FURIKOMI_KINGAKU.FocusOutCheckMethod")));
            this.COL_FURIKOMI_KINGAKU.FormatSetting = "カスタム";
            this.COL_FURIKOMI_KINGAKU.HeaderText = "金額";
            this.COL_FURIKOMI_KINGAKU.Name = "COL_FURIKOMI_KINGAKU";
            this.COL_FURIKOMI_KINGAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("COL_FURIKOMI_KINGAKU.PopupSearchSendParams")));
            this.COL_FURIKOMI_KINGAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.COL_FURIKOMI_KINGAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("COL_FURIKOMI_KINGAKU.popupWindowSetting")));
            this.COL_FURIKOMI_KINGAKU.RangeSetting = rangeSettingDto3;
            this.COL_FURIKOMI_KINGAKU.ReadOnly = true;
            this.COL_FURIKOMI_KINGAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COL_FURIKOMI_KINGAKU.RegistCheckMethod")));
            this.COL_FURIKOMI_KINGAKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // lblKINGAKU_GOUKEI
            // 
            this.lblKINGAKU_GOUKEI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblKINGAKU_GOUKEI.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.lblKINGAKU_GOUKEI.ForeColor = System.Drawing.Color.White;
            this.lblKINGAKU_GOUKEI.Location = new System.Drawing.Point(120, 0);
            this.lblKINGAKU_GOUKEI.Name = "lblKINGAKU_GOUKEI";
            this.lblKINGAKU_GOUKEI.Size = new System.Drawing.Size(120, 20);
            this.lblKINGAKU_GOUKEI.TabIndex = 21;
            this.lblKINGAKU_GOUKEI.Text = "金額合計";
            this.lblKINGAKU_GOUKEI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KINGAKU_GOUKEI
            // 
            this.KINGAKU_GOUKEI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KINGAKU_GOUKEI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KINGAKU_GOUKEI.DBFieldsName = "";
            this.KINGAKU_GOUKEI.DefaultBackColor = System.Drawing.Color.Empty;
            this.KINGAKU_GOUKEI.DisplayItemName = "金額合計";
            this.KINGAKU_GOUKEI.DisplayPopUp = null;
            this.KINGAKU_GOUKEI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KINGAKU_GOUKEI.FocusOutCheckMethod")));
            this.KINGAKU_GOUKEI.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.KINGAKU_GOUKEI.ForeColor = System.Drawing.Color.Black;
            this.KINGAKU_GOUKEI.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.KINGAKU_GOUKEI.IsInputErrorOccured = false;
            this.KINGAKU_GOUKEI.ItemDefinedTypes = "";
            this.KINGAKU_GOUKEI.Location = new System.Drawing.Point(120, 20);
            this.KINGAKU_GOUKEI.Name = "KINGAKU_GOUKEI";
            this.KINGAKU_GOUKEI.PopupAfterExecute = null;
            this.KINGAKU_GOUKEI.PopupBeforeExecute = null;
            this.KINGAKU_GOUKEI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KINGAKU_GOUKEI.PopupSearchSendParams")));
            this.KINGAKU_GOUKEI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KINGAKU_GOUKEI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KINGAKU_GOUKEI.popupWindowSetting")));
            this.KINGAKU_GOUKEI.ReadOnly = true;
            this.KINGAKU_GOUKEI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KINGAKU_GOUKEI.RegistCheckMethod")));
            this.KINGAKU_GOUKEI.Size = new System.Drawing.Size(120, 20);
            this.KINGAKU_GOUKEI.TabIndex = 120;
            this.KINGAKU_GOUKEI.TabStop = false;
            this.KINGAKU_GOUKEI.Tag = " ";
            this.KINGAKU_GOUKEI.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // KENSUU_GOUKEI
            // 
            this.KENSUU_GOUKEI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KENSUU_GOUKEI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KENSUU_GOUKEI.DBFieldsName = "KOUZA_SHURUI";
            this.KENSUU_GOUKEI.DefaultBackColor = System.Drawing.Color.Empty;
            this.KENSUU_GOUKEI.DisplayItemName = "件数合計";
            this.KENSUU_GOUKEI.DisplayPopUp = null;
            this.KENSUU_GOUKEI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KENSUU_GOUKEI.FocusOutCheckMethod")));
            this.KENSUU_GOUKEI.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.KENSUU_GOUKEI.ForeColor = System.Drawing.Color.Black;
            this.KENSUU_GOUKEI.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.KENSUU_GOUKEI.IsInputErrorOccured = false;
            this.KENSUU_GOUKEI.ItemDefinedTypes = "VARCHAR";
            this.KENSUU_GOUKEI.Location = new System.Drawing.Point(1, 20);
            this.KENSUU_GOUKEI.Name = "KENSUU_GOUKEI";
            this.KENSUU_GOUKEI.PopupAfterExecute = null;
            this.KENSUU_GOUKEI.PopupBeforeExecute = null;
            this.KENSUU_GOUKEI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KENSUU_GOUKEI.PopupSearchSendParams")));
            this.KENSUU_GOUKEI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KENSUU_GOUKEI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KENSUU_GOUKEI.popupWindowSetting")));
            this.KENSUU_GOUKEI.ReadOnly = true;
            this.KENSUU_GOUKEI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KENSUU_GOUKEI.RegistCheckMethod")));
            this.KENSUU_GOUKEI.Size = new System.Drawing.Size(120, 20);
            this.KENSUU_GOUKEI.TabIndex = 110;
            this.KENSUU_GOUKEI.TabStop = false;
            this.KENSUU_GOUKEI.Tag = " ";
            this.KENSUU_GOUKEI.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblKENSUU_GOUKEI
            // 
            this.lblKENSUU_GOUKEI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblKENSUU_GOUKEI.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.lblKENSUU_GOUKEI.ForeColor = System.Drawing.Color.White;
            this.lblKENSUU_GOUKEI.Location = new System.Drawing.Point(1, 0);
            this.lblKENSUU_GOUKEI.Name = "lblKENSUU_GOUKEI";
            this.lblKENSUU_GOUKEI.Size = new System.Drawing.Size(120, 20);
            this.lblKENSUU_GOUKEI.TabIndex = 20;
            this.lblKENSUU_GOUKEI.Text = "件数合計";
            this.lblKENSUU_GOUKEI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FURIKOMI_DATE
            // 
            this.FURIKOMI_DATE.BackColor = System.Drawing.SystemColors.Window;
            this.FURIKOMI_DATE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FURIKOMI_DATE.CalendarFont = new System.Drawing.Font("MS Gothic", 9F);
            this.FURIKOMI_DATE.Checked = false;
            this.FURIKOMI_DATE.CustomFormat = "yyyy/MM/dd(ddd)";
            this.FURIKOMI_DATE.DateTimeNowYear = "";
            this.FURIKOMI_DATE.DBFieldsName = "";
            this.FURIKOMI_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            this.FURIKOMI_DATE.DisplayItemName = "振込日";
            this.FURIKOMI_DATE.DisplayPopUp = null;
            this.FURIKOMI_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FURIKOMI_DATE.FocusOutCheckMethod")));
            this.FURIKOMI_DATE.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.FURIKOMI_DATE.ForeColor = System.Drawing.Color.Black;
            this.FURIKOMI_DATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FURIKOMI_DATE.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.FURIKOMI_DATE.IsInputErrorOccured = false;
            this.FURIKOMI_DATE.ItemDefinedTypes = "";
            this.FURIKOMI_DATE.Location = new System.Drawing.Point(113, 75);
            this.FURIKOMI_DATE.MaxLength = 10;
            this.FURIKOMI_DATE.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.FURIKOMI_DATE.Name = "FURIKOMI_DATE";
            this.FURIKOMI_DATE.NullValue = "";
            this.FURIKOMI_DATE.PopupAfterExecute = null;
            this.FURIKOMI_DATE.PopupBeforeExecute = null;
            this.FURIKOMI_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("FURIKOMI_DATE.PopupSearchSendParams")));
            this.FURIKOMI_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.FURIKOMI_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("FURIKOMI_DATE.popupWindowSetting")));
            this.FURIKOMI_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FURIKOMI_DATE.RegistCheckMethod")));
            this.FURIKOMI_DATE.ShortItemName = "預入日(From)";
            this.FURIKOMI_DATE.Size = new System.Drawing.Size(138, 20);
            this.FURIKOMI_DATE.TabIndex = 20;
            this.FURIKOMI_DATE.Tag = "日付を選択してください";
            this.FURIKOMI_DATE.Text = "2013/10/29(火)";
            this.FURIKOMI_DATE.Value = new System.DateTime(2013, 10, 29, 0, 0, 0, 0);
            // 
            // lblFURIKOMI_DATE
            // 
            this.lblFURIKOMI_DATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblFURIKOMI_DATE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFURIKOMI_DATE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblFURIKOMI_DATE.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblFURIKOMI_DATE.ForeColor = System.Drawing.Color.White;
            this.lblFURIKOMI_DATE.Location = new System.Drawing.Point(3, 75);
            this.lblFURIKOMI_DATE.Name = "lblFURIKOMI_DATE";
            this.lblFURIKOMI_DATE.Size = new System.Drawing.Size(106, 20);
            this.lblFURIKOMI_DATE.TabIndex = 12;
            this.lblFURIKOMI_DATE.Text = "振込日※";
            this.lblFURIKOMI_DATE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlKINGAKU
            // 
            this.pnlKINGAKU.Controls.Add(this.lblKENSUU_GOUKEI);
            this.pnlKINGAKU.Controls.Add(this.KENSUU_GOUKEI);
            this.pnlKINGAKU.Controls.Add(this.KINGAKU_GOUKEI);
            this.pnlKINGAKU.Controls.Add(this.lblKINGAKU_GOUKEI);
            this.pnlKINGAKU.Location = new System.Drawing.Point(3, 437);
            this.pnlKINGAKU.Name = "pnlKINGAKU";
            this.pnlKINGAKU.Size = new System.Drawing.Size(249, 42);
            this.pnlKINGAKU.TabIndex = 121;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 480);
            this.Controls.Add(this.pnlKINGAKU);
            this.Controls.Add(this.FURIKOMI_DATE);
            this.Controls.Add(this.lblFURIKOMI_DATE);
            this.Controls.Add(this.Ichiran);
            this.Controls.Add(this.SEARCH_SHUTSURYOKU_SAKI);
            this.Controls.Add(this.SHUTSURYOKU_SAKI);
            this.Controls.Add(this.lblSHUTSURYOKU_SAKI);
            this.Controls.Add(this.pnlSHUTSURYOKU_JOUKYOU);
            this.Controls.Add(this.lblSHUTSURYOKU_JOUKYOU);
            this.Controls.Add(this.BANK_SHITEN_CD);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SEARCH_BANK_SHITEN);
            this.Controls.Add(this.SEARCH_BANK);
            this.Controls.Add(this.KOUZA_NO);
            this.Controls.Add(this.KOUZA_SHURUI);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.BANK_SHITEN_NAME_RYAKU);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.BANK_CD);
            this.Controls.Add(this.BANK_NAME_RYAKU);
            this.Controls.Add(this.label3);
            this.Name = "UIForm";
            this.Text = "UIForm";
            this.pnlSHUTSURYOKU_JOUKYOU.ResumeLayout(false);
            this.pnlSHUTSURYOKU_JOUKYOU.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).EndInit();
            this.pnlKINGAKU.ResumeLayout(false);
            this.pnlKINGAKU.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label1;
        internal r_framework.CustomControl.CustomPopupOpenButton SEARCH_BANK_SHITEN;
        internal r_framework.CustomControl.CustomPopupOpenButton SEARCH_BANK;
        internal r_framework.CustomControl.CustomTextBox KOUZA_NO;
        internal r_framework.CustomControl.CustomTextBox KOUZA_SHURUI;
        public System.Windows.Forms.Label label6;
        internal r_framework.CustomControl.CustomTextBox BANK_SHITEN_NAME_RYAKU;
        public System.Windows.Forms.Label label4;
        internal r_framework.CustomControl.CustomTextBox BANK_NAME_RYAKU;
        public System.Windows.Forms.Label label3;
        private r_framework.CustomControl.CustomPanel pnlSHUTSURYOKU_JOUKYOU;
        public r_framework.CustomControl.CustomRadioButton SHUTSURYOKU_JOUKYOU3;
        public r_framework.CustomControl.CustomRadioButton SHUTSURYOKU_JOUKYOU2;
        public r_framework.CustomControl.CustomNumericTextBox2 SHUTSURYOKU_JOUKYOU;
        public r_framework.CustomControl.CustomRadioButton SHUTSURYOKU_JOUKYOU1;
        private System.Windows.Forms.Label lblSHUTSURYOKU_JOUKYOU;
        internal r_framework.CustomControl.CustomButton SEARCH_SHUTSURYOKU_SAKI;
        internal r_framework.CustomControl.CustomTextBox SHUTSURYOKU_SAKI;
        private System.Windows.Forms.Label lblSHUTSURYOKU_SAKI;
        public r_framework.CustomControl.CustomDataGridView Ichiran;
        public System.Windows.Forms.Label lblKINGAKU_GOUKEI;
        internal r_framework.CustomControl.CustomTextBox KINGAKU_GOUKEI;
        internal r_framework.CustomControl.CustomTextBox KENSUU_GOUKEI;
        public System.Windows.Forms.Label lblKENSUU_GOUKEI;
        internal r_framework.CustomControl.CustomDateTimePicker FURIKOMI_DATE;
        private System.Windows.Forms.Label lblFURIKOMI_DATE;
        internal r_framework.CustomControl.CustomPanel pnlKINGAKU;
        public r_framework.CustomControl.CustomAlphaNumTextBox BANK_SHITEN_CD;
        public r_framework.CustomControl.CustomAlphaNumTextBox BANK_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn COL_FURIKOMI_BANK_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn COL_FURIKOMI_BANK_NAME;
        private r_framework.CustomControl.DgvCustomTextBoxColumn COL_FURIKOMI_BANK_SHITEN_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn COL_FURIKOMI_BANK_SHITEN_NAME;
        private r_framework.CustomControl.DgvCustomTextBoxColumn COL_FURIKOMI_KOUZA_SHURUI_NAME;
        private r_framework.CustomControl.DgvCustomTextBoxColumn COL_FURIKOMI_KOUZA_NO;
        private r_framework.CustomControl.DgvCustomTextBoxColumn COL_FURIKOMI_KOUZA_NAME;
        private r_framework.CustomControl.DgvCustomTextBoxColumn COL_TORIHIKISAKI_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn COL_TORIHIKISAKI_NAME;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column COL_FURIKOMI_KINGAKU;
    }
}