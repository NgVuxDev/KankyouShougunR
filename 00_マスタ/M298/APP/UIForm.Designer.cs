namespace Shougun.Core.Master.BankIkkatsu
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.BANK_NAME_AFTER = new r_framework.CustomControl.CustomTextBox();
            this.BANK_CD_AFTER = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.labelBANK_AFTER = new System.Windows.Forms.Label();
            this.BANK_SHITEN_NAME = new r_framework.CustomControl.CustomTextBox();
            this.BANK_SHITEN_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.BANK_NAME = new r_framework.CustomControl.CustomTextBox();
            this.labelBANK_SHITEN = new System.Windows.Forms.Label();
            this.BANK_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.labelBANK = new System.Windows.Forms.Label();
            this.dgvBank = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.TORIHIKISAKI_CDColumn = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.TORIHIKISAKI_NAME1Column = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.FURIKOMI_BANK_CDColumn = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.BANK_NAMEColumn = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.FURIKOMI_BANK_SHITEN_CDColumn = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.BANK_SHITEN_NAMEColumn = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.FURIKOMI_BANK_CD_AFTERColumn = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.BANK_NAME_AFTERColumn = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.FURIKOMI_BANK_SHITEN_CD_AFTERColumn = new r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn();
            this.BANK_SHITEN_NAME_AFTERColumn = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.KOUZA_SHURUI_AFTERColumn = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.KOUZA_NO_AFTERColumn = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.KOUZA_NAME_AFTERColumn = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.labelBANK_SHITEN_AFTER = new System.Windows.Forms.Label();
            this.BANK_SHITEN_NAME_AFTER = new r_framework.CustomControl.CustomTextBox();
            this.BANK_SHITEN_CD_AFTER = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.KOUZA_SHURUI = new r_framework.CustomControl.CustomTextBox();
            this.KOUZA_NO = new r_framework.CustomControl.CustomTextBox();
            this.KOUZA_SHURUI_AFTER = new r_framework.CustomControl.CustomTextBox();
            this.KOUZA_NO_AFTER = new r_framework.CustomControl.CustomTextBox();
            this.KOUZA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.KOUZA_NAME_AFTER = new r_framework.CustomControl.CustomTextBox();
            this.KOUZA_SHURUI_GRID = new r_framework.CustomControl.CustomTextBox();
            this.KOUZA_NO_GRID = new r_framework.CustomControl.CustomTextBox();
            this.KOUZA_NAME_GRID = new r_framework.CustomControl.CustomTextBox();
            this.BANK_SHITEN_CD_GRID = new r_framework.CustomControl.CustomTextBox();
            this.BANK_SHITEN_CD_OLD = new r_framework.CustomControl.CustomTextBox();
            this.BANK_SHITEN_CD_AFTER_OLD = new r_framework.CustomControl.CustomTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBank)).BeginInit();
            this.SuspendLayout();
            // 
            // BANK_NAME_AFTER
            // 
            this.BANK_NAME_AFTER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.BANK_NAME_AFTER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BANK_NAME_AFTER.DefaultBackColor = System.Drawing.Color.Empty;
            this.BANK_NAME_AFTER.DisplayPopUp = null;
            this.BANK_NAME_AFTER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_NAME_AFTER.FocusOutCheckMethod")));
            this.BANK_NAME_AFTER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.BANK_NAME_AFTER.ForeColor = System.Drawing.Color.Black;
            this.BANK_NAME_AFTER.IsInputErrorOccured = false;
            this.BANK_NAME_AFTER.Location = new System.Drawing.Point(171, 416);
            this.BANK_NAME_AFTER.Name = "BANK_NAME_AFTER";
            this.BANK_NAME_AFTER.PopupAfterExecute = null;
            this.BANK_NAME_AFTER.PopupBeforeExecute = null;
            this.BANK_NAME_AFTER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BANK_NAME_AFTER.PopupSearchSendParams")));
            this.BANK_NAME_AFTER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BANK_NAME_AFTER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BANK_NAME_AFTER.popupWindowSetting")));
            this.BANK_NAME_AFTER.prevText = null;
            this.BANK_NAME_AFTER.ReadOnly = true;
            this.BANK_NAME_AFTER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_NAME_AFTER.RegistCheckMethod")));
            this.BANK_NAME_AFTER.Size = new System.Drawing.Size(320, 20);
            this.BANK_NAME_AFTER.TabIndex = 9;
            this.BANK_NAME_AFTER.TabStop = false;
            this.BANK_NAME_AFTER.Tag = "";
            // 
            // BANK_CD_AFTER
            // 
            this.BANK_CD_AFTER.BackColor = System.Drawing.SystemColors.Window;
            this.BANK_CD_AFTER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BANK_CD_AFTER.ChangeUpperCase = true;
            this.BANK_CD_AFTER.CharacterLimitList = null;
            this.BANK_CD_AFTER.CharactersNumber = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.BANK_CD_AFTER.DBFieldsName = "BANK_CD";
            this.BANK_CD_AFTER.DefaultBackColor = System.Drawing.Color.Empty;
            this.BANK_CD_AFTER.DisplayItemName = "BANK_NAME";
            this.BANK_CD_AFTER.DisplayPopUp = null;
            this.BANK_CD_AFTER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_CD_AFTER.FocusOutCheckMethod")));
            this.BANK_CD_AFTER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.BANK_CD_AFTER.ForeColor = System.Drawing.Color.Black;
            this.BANK_CD_AFTER.GetCodeMasterField = "BANK_CD,BANK_NAME";
            this.BANK_CD_AFTER.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.BANK_CD_AFTER.IsInputErrorOccured = false;
            this.BANK_CD_AFTER.ItemDefinedTypes = "varchar";
            this.BANK_CD_AFTER.Location = new System.Drawing.Point(132, 416);
            this.BANK_CD_AFTER.MaxLength = 4;
            this.BANK_CD_AFTER.Name = "BANK_CD_AFTER";
            this.BANK_CD_AFTER.PopupAfterExecute = null;
            this.BANK_CD_AFTER.PopupBeforeExecute = null;
            this.BANK_CD_AFTER.PopupGetMasterField = "BANK_CD,BANK_NAME";
            this.BANK_CD_AFTER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BANK_CD_AFTER.PopupSearchSendParams")));
            this.BANK_CD_AFTER.PopupSendParams = new string[0];
            this.BANK_CD_AFTER.PopupSetFormField = "BANK_CD_AFTER,BANK_NAME_AFTER";
            this.BANK_CD_AFTER.PopupWindowId = r_framework.Const.WINDOW_ID.M_BANK;
            this.BANK_CD_AFTER.PopupWindowName = "マスタ共通ポップアップ";
            this.BANK_CD_AFTER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BANK_CD_AFTER.popupWindowSetting")));
            this.BANK_CD_AFTER.prevText = null;
            this.BANK_CD_AFTER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_CD_AFTER.RegistCheckMethod")));
            this.BANK_CD_AFTER.SetFormField = "BANK_CD_AFTER,BANK_NAME_AFTER";
            this.BANK_CD_AFTER.Size = new System.Drawing.Size(40, 20);
            this.BANK_CD_AFTER.TabIndex = 8;
            this.BANK_CD_AFTER.Tag = "銀行CDを入力してください（スペースキー押下にて、検索画面を表示します）";
            this.BANK_CD_AFTER.ZeroPaddengFlag = true;
            this.BANK_CD_AFTER.TextChanged += new System.EventHandler(this.BANK_CD_AFTER_TextChanged);
            // 
            // labelBANK_AFTER
            // 
            this.labelBANK_AFTER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.labelBANK_AFTER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.labelBANK_AFTER.ForeColor = System.Drawing.Color.White;
            this.labelBANK_AFTER.Location = new System.Drawing.Point(2, 416);
            this.labelBANK_AFTER.Name = "labelBANK_AFTER";
            this.labelBANK_AFTER.Size = new System.Drawing.Size(125, 20);
            this.labelBANK_AFTER.TabIndex = 7;
            this.labelBANK_AFTER.Text = "銀行（変更後）";
            this.labelBANK_AFTER.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BANK_SHITEN_NAME
            // 
            this.BANK_SHITEN_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.BANK_SHITEN_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BANK_SHITEN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.BANK_SHITEN_NAME.DisplayPopUp = null;
            this.BANK_SHITEN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_SHITEN_NAME.FocusOutCheckMethod")));
            this.BANK_SHITEN_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.BANK_SHITEN_NAME.ForeColor = System.Drawing.Color.Black;
            this.BANK_SHITEN_NAME.IsInputErrorOccured = false;
            this.BANK_SHITEN_NAME.Location = new System.Drawing.Point(171, 28);
            this.BANK_SHITEN_NAME.Name = "BANK_SHITEN_NAME";
            this.BANK_SHITEN_NAME.PopupAfterExecute = null;
            this.BANK_SHITEN_NAME.PopupBeforeExecute = null;
            this.BANK_SHITEN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BANK_SHITEN_NAME.PopupSearchSendParams")));
            this.BANK_SHITEN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BANK_SHITEN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BANK_SHITEN_NAME.popupWindowSetting")));
            this.BANK_SHITEN_NAME.prevText = null;
            this.BANK_SHITEN_NAME.ReadOnly = true;
            this.BANK_SHITEN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_SHITEN_NAME.RegistCheckMethod")));
            this.BANK_SHITEN_NAME.Size = new System.Drawing.Size(506, 20);
            this.BANK_SHITEN_NAME.TabIndex = 5;
            this.BANK_SHITEN_NAME.TabStop = false;
            this.BANK_SHITEN_NAME.Tag = "";
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
            this.BANK_SHITEN_CD.DBFieldsName = "BANK_SHITEN_CD";
            this.BANK_SHITEN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.BANK_SHITEN_CD.DisplayPopUp = null;
            this.BANK_SHITEN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_SHITEN_CD.FocusOutCheckMethod")));
            this.BANK_SHITEN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.BANK_SHITEN_CD.ForeColor = System.Drawing.Color.Black;
            this.BANK_SHITEN_CD.GetCodeMasterField = "BANK_CD,BANK_NAME_RYAKU,BANK_SHITEN_CD,BANK_SHIETN_NAME_RYAKU,BANK_SHITEN_CD,KOUZ" +
                "A_SHURUI,KOUZA_NO,KOUZA_NAME";
            this.BANK_SHITEN_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.BANK_SHITEN_CD.IsInputErrorOccured = false;
            this.BANK_SHITEN_CD.ItemDefinedTypes = "varchar";
            this.BANK_SHITEN_CD.Location = new System.Drawing.Point(132, 28);
            this.BANK_SHITEN_CD.MaxLength = 3;
            this.BANK_SHITEN_CD.Name = "BANK_SHITEN_CD";
            this.BANK_SHITEN_CD.PopupAfterExecute = null;
            this.BANK_SHITEN_CD.PopupBeforeExecute = null;
            this.BANK_SHITEN_CD.PopupGetMasterField = "BANK_CD,BANK_NAME_RYAKU,BANK_SHITEN_CD,BANK_SHIETN_NAME_RYAKU,BANK_SHITEN_CD,KOUZ" +
                "A_SHURUI,KOUZA_NO,KOUZA_NAME";
            this.BANK_SHITEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BANK_SHITEN_CD.PopupSearchSendParams")));
            this.BANK_SHITEN_CD.PopupSendParams = new string[0];
            this.BANK_SHITEN_CD.PopupSetFormField = "BANK_CD,BANK_NAME,BANK_SHITEN_CD,BANK_SHITEN_NAME,BANK_SHITEN_CD_OLD,KOUZA_SHURUI" +
                ",KOUZA_NO,KOUZA_NAME";
            this.BANK_SHITEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_BANK_SHITEN;
            this.BANK_SHITEN_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.BANK_SHITEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BANK_SHITEN_CD.popupWindowSetting")));
            this.BANK_SHITEN_CD.prevText = null;
            this.BANK_SHITEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_SHITEN_CD.RegistCheckMethod")));
            this.BANK_SHITEN_CD.SetFormField = "BANK_CD,BANK_NAME,BANK_SHITEN_CD,BANK_SHITEN_NAME,BANK_SHITEN_CD_OLD,KOUZA_SHURUI" +
                ",KOUZA_NO,KOUZA_NAME";
            this.BANK_SHITEN_CD.Size = new System.Drawing.Size(40, 20);
            this.BANK_SHITEN_CD.TabIndex = 4;
            this.BANK_SHITEN_CD.Tag = "銀行支店CDを入力してください（スペースキー押下にて、検索画面を表示します）";
            this.BANK_SHITEN_CD.ZeroPaddengFlag = true;
            this.BANK_SHITEN_CD.Validating += new System.ComponentModel.CancelEventHandler(this.BANK_SHITEN_CD_Validating);
            // 
            // BANK_NAME
            // 
            this.BANK_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.BANK_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BANK_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.BANK_NAME.DisplayPopUp = null;
            this.BANK_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_NAME.FocusOutCheckMethod")));
            this.BANK_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.BANK_NAME.ForeColor = System.Drawing.Color.Black;
            this.BANK_NAME.IsInputErrorOccured = false;
            this.BANK_NAME.Location = new System.Drawing.Point(171, 2);
            this.BANK_NAME.Name = "BANK_NAME";
            this.BANK_NAME.PopupAfterExecute = null;
            this.BANK_NAME.PopupBeforeExecute = null;
            this.BANK_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BANK_NAME.PopupSearchSendParams")));
            this.BANK_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BANK_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BANK_NAME.popupWindowSetting")));
            this.BANK_NAME.prevText = null;
            this.BANK_NAME.ReadOnly = true;
            this.BANK_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_NAME.RegistCheckMethod")));
            this.BANK_NAME.Size = new System.Drawing.Size(160, 20);
            this.BANK_NAME.TabIndex = 2;
            this.BANK_NAME.TabStop = false;
            this.BANK_NAME.Tag = "";
            // 
            // labelBANK_SHITEN
            // 
            this.labelBANK_SHITEN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.labelBANK_SHITEN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.labelBANK_SHITEN.ForeColor = System.Drawing.Color.White;
            this.labelBANK_SHITEN.Location = new System.Drawing.Point(2, 28);
            this.labelBANK_SHITEN.Name = "labelBANK_SHITEN";
            this.labelBANK_SHITEN.Size = new System.Drawing.Size(125, 20);
            this.labelBANK_SHITEN.TabIndex = 3;
            this.labelBANK_SHITEN.Text = "銀行口座";
            this.labelBANK_SHITEN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.BANK_CD.DisplayItemName = "BANK_NAME_RYAKU";
            this.BANK_CD.DisplayPopUp = null;
            this.BANK_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_CD.FocusOutCheckMethod")));
            this.BANK_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.BANK_CD.ForeColor = System.Drawing.Color.Black;
            this.BANK_CD.GetCodeMasterField = "BANK_CD,BANK_NAME_RYAKU";
            this.BANK_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.BANK_CD.IsInputErrorOccured = false;
            this.BANK_CD.ItemDefinedTypes = "varchar";
            this.BANK_CD.Location = new System.Drawing.Point(132, 2);
            this.BANK_CD.MaxLength = 4;
            this.BANK_CD.Name = "BANK_CD";
            this.BANK_CD.PopupAfterExecute = null;
            this.BANK_CD.PopupBeforeExecute = null;
            this.BANK_CD.PopupGetMasterField = "BANK_CD,BANK_NAME_RYAKU";
            this.BANK_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BANK_CD.PopupSearchSendParams")));
            this.BANK_CD.PopupSendParams = new string[0];
            this.BANK_CD.PopupSetFormField = "BANK_CD,BANK_NAME";
            this.BANK_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_BANK;
            this.BANK_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.BANK_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BANK_CD.popupWindowSetting")));
            this.BANK_CD.prevText = null;
            this.BANK_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_CD.RegistCheckMethod")));
            this.BANK_CD.SetFormField = "BANK_CD,BANK_NAME";
            this.BANK_CD.Size = new System.Drawing.Size(40, 20);
            this.BANK_CD.TabIndex = 1;
            this.BANK_CD.Tag = "銀行CDを入力してください（スペースキー押下にて、検索画面を表示します）";
            this.BANK_CD.ZeroPaddengFlag = true;
            this.BANK_CD.TextChanged += new System.EventHandler(this.BANK_CD_TextChanged);
            // 
            // labelBANK
            // 
            this.labelBANK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.labelBANK.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.labelBANK.ForeColor = System.Drawing.Color.White;
            this.labelBANK.Location = new System.Drawing.Point(2, 2);
            this.labelBANK.Name = "labelBANK";
            this.labelBANK.Size = new System.Drawing.Size(125, 20);
            this.labelBANK.TabIndex = 0;
            this.labelBANK.Text = "銀行";
            this.labelBANK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvBank
            // 
            this.dgvBank.AllowUserToAddRows = false;
            this.dgvBank.AllowUserToDeleteRows = false;
            this.dgvBank.AllowUserToResizeColumns = false;
            this.dgvBank.AllowUserToResizeRows = false;
            this.dgvBank.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBank.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBank.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvBank.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TORIHIKISAKI_CDColumn,
            this.TORIHIKISAKI_NAME1Column,
            this.FURIKOMI_BANK_CDColumn,
            this.BANK_NAMEColumn,
            this.FURIKOMI_BANK_SHITEN_CDColumn,
            this.BANK_SHITEN_NAMEColumn,
            this.FURIKOMI_BANK_CD_AFTERColumn,
            this.BANK_NAME_AFTERColumn,
            this.FURIKOMI_BANK_SHITEN_CD_AFTERColumn,
            this.BANK_SHITEN_NAME_AFTERColumn,
            this.KOUZA_SHURUI_AFTERColumn,
            this.KOUZA_NO_AFTERColumn,
            this.KOUZA_NAME_AFTERColumn});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBank.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvBank.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvBank.EnableHeadersVisualStyles = false;
            this.dgvBank.GridColor = System.Drawing.Color.White;
            this.dgvBank.IsReload = false;
            this.dgvBank.LinkedDataPanelName = null;
            this.dgvBank.Location = new System.Drawing.Point(3, 54);
            this.dgvBank.MultiSelect = false;
            this.dgvBank.Name = "dgvBank";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBank.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvBank.RowHeadersVisible = false;
            this.dgvBank.RowHeadersWidth = 20;
            this.dgvBank.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvBank.RowTemplate.Height = 21;
            this.dgvBank.ShowCellToolTips = false;
            this.dgvBank.Size = new System.Drawing.Size(995, 351);
            this.dgvBank.TabIndex = 6;
            this.dgvBank.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvBank_CellPainting);
            this.dgvBank.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvBank_CellValidating);
            this.dgvBank.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBank_CellValueChanged);
            this.dgvBank.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBank_RowEnter);
            this.dgvBank.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dgvBank_Scroll);
            // 
            // TORIHIKISAKI_CDColumn
            // 
            this.TORIHIKISAKI_CDColumn.DataPropertyName = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CDColumn.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_CDColumn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CDColumn.FocusOutCheckMethod")));
            this.TORIHIKISAKI_CDColumn.HeaderText = "取引先CD";
            this.TORIHIKISAKI_CDColumn.Name = "TORIHIKISAKI_CDColumn";
            this.TORIHIKISAKI_CDColumn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_CDColumn.PopupSearchSendParams")));
            this.TORIHIKISAKI_CDColumn.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_CDColumn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_CDColumn.popupWindowSetting")));
            this.TORIHIKISAKI_CDColumn.ReadOnly = true;
            this.TORIHIKISAKI_CDColumn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CDColumn.RegistCheckMethod")));
            this.TORIHIKISAKI_CDColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TORIHIKISAKI_CDColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TORIHIKISAKI_NAME1Column
            // 
            this.TORIHIKISAKI_NAME1Column.DataPropertyName = "TORIHIKISAKI_NAME1";
            this.TORIHIKISAKI_NAME1Column.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_NAME1Column.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME1Column.FocusOutCheckMethod")));
            this.TORIHIKISAKI_NAME1Column.HeaderText = "取引先名";
            this.TORIHIKISAKI_NAME1Column.Name = "TORIHIKISAKI_NAME1Column";
            this.TORIHIKISAKI_NAME1Column.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_NAME1Column.PopupSearchSendParams")));
            this.TORIHIKISAKI_NAME1Column.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_NAME1Column.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_NAME1Column.popupWindowSetting")));
            this.TORIHIKISAKI_NAME1Column.ReadOnly = true;
            this.TORIHIKISAKI_NAME1Column.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME1Column.RegistCheckMethod")));
            this.TORIHIKISAKI_NAME1Column.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TORIHIKISAKI_NAME1Column.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TORIHIKISAKI_NAME1Column.Width = 250;
            // 
            // FURIKOMI_BANK_CDColumn
            // 
            this.FURIKOMI_BANK_CDColumn.DataPropertyName = "FURIKOMI_BANK_CD";
            this.FURIKOMI_BANK_CDColumn.DefaultBackColor = System.Drawing.Color.Empty;
            this.FURIKOMI_BANK_CDColumn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FURIKOMI_BANK_CDColumn.FocusOutCheckMethod")));
            this.FURIKOMI_BANK_CDColumn.HeaderText = "銀行";
            this.FURIKOMI_BANK_CDColumn.Name = "FURIKOMI_BANK_CDColumn";
            this.FURIKOMI_BANK_CDColumn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("FURIKOMI_BANK_CDColumn.PopupSearchSendParams")));
            this.FURIKOMI_BANK_CDColumn.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.FURIKOMI_BANK_CDColumn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("FURIKOMI_BANK_CDColumn.popupWindowSetting")));
            this.FURIKOMI_BANK_CDColumn.ReadOnly = true;
            this.FURIKOMI_BANK_CDColumn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FURIKOMI_BANK_CDColumn.RegistCheckMethod")));
            this.FURIKOMI_BANK_CDColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.FURIKOMI_BANK_CDColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FURIKOMI_BANK_CDColumn.Width = 60;
            // 
            // BANK_NAMEColumn
            // 
            this.BANK_NAMEColumn.DataPropertyName = "BANK_NAME";
            this.BANK_NAMEColumn.DefaultBackColor = System.Drawing.Color.Empty;
            this.BANK_NAMEColumn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_NAMEColumn.FocusOutCheckMethod")));
            this.BANK_NAMEColumn.HeaderText = "変更前";
            this.BANK_NAMEColumn.Name = "BANK_NAMEColumn";
            this.BANK_NAMEColumn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BANK_NAMEColumn.PopupSearchSendParams")));
            this.BANK_NAMEColumn.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BANK_NAMEColumn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BANK_NAMEColumn.popupWindowSetting")));
            this.BANK_NAMEColumn.ReadOnly = true;
            this.BANK_NAMEColumn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_NAMEColumn.RegistCheckMethod")));
            this.BANK_NAMEColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.BANK_NAMEColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BANK_NAMEColumn.Width = 250;
            // 
            // FURIKOMI_BANK_SHITEN_CDColumn
            // 
            this.FURIKOMI_BANK_SHITEN_CDColumn.DataPropertyName = "FURIKOMI_BANK_SHITEN_CD";
            this.FURIKOMI_BANK_SHITEN_CDColumn.DefaultBackColor = System.Drawing.Color.Empty;
            this.FURIKOMI_BANK_SHITEN_CDColumn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FURIKOMI_BANK_SHITEN_CDColumn.FocusOutCheckMethod")));
            this.FURIKOMI_BANK_SHITEN_CDColumn.HeaderText = "銀行口座";
            this.FURIKOMI_BANK_SHITEN_CDColumn.Name = "FURIKOMI_BANK_SHITEN_CDColumn";
            this.FURIKOMI_BANK_SHITEN_CDColumn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("FURIKOMI_BANK_SHITEN_CDColumn.PopupSearchSendParams")));
            this.FURIKOMI_BANK_SHITEN_CDColumn.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.FURIKOMI_BANK_SHITEN_CDColumn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("FURIKOMI_BANK_SHITEN_CDColumn.popupWindowSetting")));
            this.FURIKOMI_BANK_SHITEN_CDColumn.ReadOnly = true;
            this.FURIKOMI_BANK_SHITEN_CDColumn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FURIKOMI_BANK_SHITEN_CDColumn.RegistCheckMethod")));
            this.FURIKOMI_BANK_SHITEN_CDColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.FURIKOMI_BANK_SHITEN_CDColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // BANK_SHITEN_NAMEColumn
            // 
            this.BANK_SHITEN_NAMEColumn.DataPropertyName = "BANK_SHITEN_NAME";
            this.BANK_SHITEN_NAMEColumn.DefaultBackColor = System.Drawing.Color.Empty;
            this.BANK_SHITEN_NAMEColumn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_SHITEN_NAMEColumn.FocusOutCheckMethod")));
            this.BANK_SHITEN_NAMEColumn.HeaderText = "変更前";
            this.BANK_SHITEN_NAMEColumn.Name = "BANK_SHITEN_NAMEColumn";
            this.BANK_SHITEN_NAMEColumn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BANK_SHITEN_NAMEColumn.PopupSearchSendParams")));
            this.BANK_SHITEN_NAMEColumn.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BANK_SHITEN_NAMEColumn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BANK_SHITEN_NAMEColumn.popupWindowSetting")));
            this.BANK_SHITEN_NAMEColumn.ReadOnly = true;
            this.BANK_SHITEN_NAMEColumn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_SHITEN_NAMEColumn.RegistCheckMethod")));
            this.BANK_SHITEN_NAMEColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.BANK_SHITEN_NAMEColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BANK_SHITEN_NAMEColumn.Width = 250;
            // 
            // FURIKOMI_BANK_CD_AFTERColumn
            // 
            this.FURIKOMI_BANK_CD_AFTERColumn.CharactersNumber = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.FURIKOMI_BANK_CD_AFTERColumn.DataPropertyName = "FURIKOMI_BANK_CD_AFTER";
            this.FURIKOMI_BANK_CD_AFTERColumn.DefaultBackColor = System.Drawing.Color.Empty;
            this.FURIKOMI_BANK_CD_AFTERColumn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FURIKOMI_BANK_CD_AFTERColumn.FocusOutCheckMethod")));
            this.FURIKOMI_BANK_CD_AFTERColumn.GetCodeMasterField = "BANK_CD,BANK_NAME";
            this.FURIKOMI_BANK_CD_AFTERColumn.HeaderText = "銀行";
            this.FURIKOMI_BANK_CD_AFTERColumn.Name = "FURIKOMI_BANK_CD_AFTERColumn";
            this.FURIKOMI_BANK_CD_AFTERColumn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("FURIKOMI_BANK_CD_AFTERColumn.PopupSearchSendParams")));
            this.FURIKOMI_BANK_CD_AFTERColumn.PopupWindowId = r_framework.Const.WINDOW_ID.M_BANK;
            this.FURIKOMI_BANK_CD_AFTERColumn.PopupWindowName = "マスタ共通ポップアップ";
            this.FURIKOMI_BANK_CD_AFTERColumn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("FURIKOMI_BANK_CD_AFTERColumn.popupWindowSetting")));
            this.FURIKOMI_BANK_CD_AFTERColumn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FURIKOMI_BANK_CD_AFTERColumn.RegistCheckMethod")));
            this.FURIKOMI_BANK_CD_AFTERColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.FURIKOMI_BANK_CD_AFTERColumn.SetFormField = "FURIKOMI_BANK_CD_AFTERColumn,BANK_NAME_AFTERColumn";
            this.FURIKOMI_BANK_CD_AFTERColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FURIKOMI_BANK_CD_AFTERColumn.ToolTipText = "半角4桁以内で入力してください";
            this.FURIKOMI_BANK_CD_AFTERColumn.Width = 60;
            this.FURIKOMI_BANK_CD_AFTERColumn.ZeroPaddengFlag = true;
            // 
            // BANK_NAME_AFTERColumn
            // 
            this.BANK_NAME_AFTERColumn.DataPropertyName = "BANK_NAME_AFTER";
            this.BANK_NAME_AFTERColumn.DefaultBackColor = System.Drawing.Color.Empty;
            this.BANK_NAME_AFTERColumn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_NAME_AFTERColumn.FocusOutCheckMethod")));
            this.BANK_NAME_AFTERColumn.HeaderText = "変更後";
            this.BANK_NAME_AFTERColumn.Name = "BANK_NAME_AFTERColumn";
            this.BANK_NAME_AFTERColumn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BANK_NAME_AFTERColumn.PopupSearchSendParams")));
            this.BANK_NAME_AFTERColumn.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BANK_NAME_AFTERColumn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BANK_NAME_AFTERColumn.popupWindowSetting")));
            this.BANK_NAME_AFTERColumn.ReadOnly = true;
            this.BANK_NAME_AFTERColumn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_NAME_AFTERColumn.RegistCheckMethod")));
            this.BANK_NAME_AFTERColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.BANK_NAME_AFTERColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BANK_NAME_AFTERColumn.Width = 250;
            // 
            // FURIKOMI_BANK_SHITEN_CD_AFTERColumn
            // 
            this.FURIKOMI_BANK_SHITEN_CD_AFTERColumn.CharactersNumber = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.FURIKOMI_BANK_SHITEN_CD_AFTERColumn.DataPropertyName = "FURIKOMI_BANK_SHITEN_CD_AFTER";
            this.FURIKOMI_BANK_SHITEN_CD_AFTERColumn.DefaultBackColor = System.Drawing.Color.Empty;
            this.FURIKOMI_BANK_SHITEN_CD_AFTERColumn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FURIKOMI_BANK_SHITEN_CD_AFTERColumn.FocusOutCheckMethod")));
            this.FURIKOMI_BANK_SHITEN_CD_AFTERColumn.GetCodeMasterField = "BANK_CD,BANK_NAME_RYAKU,BANK_SHITEN_CD,BANK_SHIETN_NAME_RYAKU,BANK_SHITEN_CD,KOUZ" +
                "A_SHURUI,KOUZA_NO,KOUZA_NAME";
            this.FURIKOMI_BANK_SHITEN_CD_AFTERColumn.HeaderText = "銀行口座";
            this.FURIKOMI_BANK_SHITEN_CD_AFTERColumn.Name = "FURIKOMI_BANK_SHITEN_CD_AFTERColumn";
            this.FURIKOMI_BANK_SHITEN_CD_AFTERColumn.PopupAfterExecuteMethod = "FURIKOMI_BANK_SHITEN_CD_AFTERColumn_PopupAfter";
            this.FURIKOMI_BANK_SHITEN_CD_AFTERColumn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("FURIKOMI_BANK_SHITEN_CD_AFTERColumn.PopupSearchSendParams")));
            this.FURIKOMI_BANK_SHITEN_CD_AFTERColumn.PopupWindowId = r_framework.Const.WINDOW_ID.M_BANK_SHITEN;
            this.FURIKOMI_BANK_SHITEN_CD_AFTERColumn.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.FURIKOMI_BANK_SHITEN_CD_AFTERColumn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("FURIKOMI_BANK_SHITEN_CD_AFTERColumn.popupWindowSetting")));
            this.FURIKOMI_BANK_SHITEN_CD_AFTERColumn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FURIKOMI_BANK_SHITEN_CD_AFTERColumn.RegistCheckMethod")));
            this.FURIKOMI_BANK_SHITEN_CD_AFTERColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.FURIKOMI_BANK_SHITEN_CD_AFTERColumn.SetFormField = "FURIKOMI_BANK_CD_AFTERColumn,BANK_NAME_AFTERColumn,FURIKOMI_BANK_SHITEN_CD_AFTERC" +
                "olumn,BANK_SHITEN_NAME_AFTERColumn,BANK_SHITEN_CD_GRID,KOUZA_SHURUI_GRID,KOUZA_N" +
                "O_GRID,KOUZA_NAME_GRID";
            this.FURIKOMI_BANK_SHITEN_CD_AFTERColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FURIKOMI_BANK_SHITEN_CD_AFTERColumn.ToolTipText = "半角3桁以内で入力してください";
            this.FURIKOMI_BANK_SHITEN_CD_AFTERColumn.ZeroPaddengFlag = true;
            // 
            // BANK_SHITEN_NAME_AFTERColumn
            // 
            this.BANK_SHITEN_NAME_AFTERColumn.DataPropertyName = "BANK_SHITEN_NAME_AFTER";
            this.BANK_SHITEN_NAME_AFTERColumn.DefaultBackColor = System.Drawing.Color.Empty;
            this.BANK_SHITEN_NAME_AFTERColumn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_SHITEN_NAME_AFTERColumn.FocusOutCheckMethod")));
            this.BANK_SHITEN_NAME_AFTERColumn.HeaderText = "変更後";
            this.BANK_SHITEN_NAME_AFTERColumn.Name = "BANK_SHITEN_NAME_AFTERColumn";
            this.BANK_SHITEN_NAME_AFTERColumn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BANK_SHITEN_NAME_AFTERColumn.PopupSearchSendParams")));
            this.BANK_SHITEN_NAME_AFTERColumn.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BANK_SHITEN_NAME_AFTERColumn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BANK_SHITEN_NAME_AFTERColumn.popupWindowSetting")));
            this.BANK_SHITEN_NAME_AFTERColumn.ReadOnly = true;
            this.BANK_SHITEN_NAME_AFTERColumn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_SHITEN_NAME_AFTERColumn.RegistCheckMethod")));
            this.BANK_SHITEN_NAME_AFTERColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.BANK_SHITEN_NAME_AFTERColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BANK_SHITEN_NAME_AFTERColumn.Width = 250;
            // 
            // KOUZA_SHURUI_AFTERColumn
            // 
            this.KOUZA_SHURUI_AFTERColumn.DataPropertyName = "KOUZA_SHURUI_AFTER";
            this.KOUZA_SHURUI_AFTERColumn.DefaultBackColor = System.Drawing.Color.Empty;
            this.KOUZA_SHURUI_AFTERColumn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_SHURUI_AFTERColumn.FocusOutCheckMethod")));
            this.KOUZA_SHURUI_AFTERColumn.HeaderText = "口座種類";
            this.KOUZA_SHURUI_AFTERColumn.Name = "KOUZA_SHURUI_AFTERColumn";
            this.KOUZA_SHURUI_AFTERColumn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KOUZA_SHURUI_AFTERColumn.PopupSearchSendParams")));
            this.KOUZA_SHURUI_AFTERColumn.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KOUZA_SHURUI_AFTERColumn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KOUZA_SHURUI_AFTERColumn.popupWindowSetting")));
            this.KOUZA_SHURUI_AFTERColumn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_SHURUI_AFTERColumn.RegistCheckMethod")));
            this.KOUZA_SHURUI_AFTERColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.KOUZA_SHURUI_AFTERColumn.Visible = false;
            // 
            // KOUZA_NO_AFTERColumn
            // 
            this.KOUZA_NO_AFTERColumn.DataPropertyName = "KOUZA_NO_AFTER";
            this.KOUZA_NO_AFTERColumn.DefaultBackColor = System.Drawing.Color.Empty;
            this.KOUZA_NO_AFTERColumn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_NO_AFTERColumn.FocusOutCheckMethod")));
            this.KOUZA_NO_AFTERColumn.HeaderText = "口座番号";
            this.KOUZA_NO_AFTERColumn.Name = "KOUZA_NO_AFTERColumn";
            this.KOUZA_NO_AFTERColumn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KOUZA_NO_AFTERColumn.PopupSearchSendParams")));
            this.KOUZA_NO_AFTERColumn.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KOUZA_NO_AFTERColumn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KOUZA_NO_AFTERColumn.popupWindowSetting")));
            this.KOUZA_NO_AFTERColumn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_NO_AFTERColumn.RegistCheckMethod")));
            this.KOUZA_NO_AFTERColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.KOUZA_NO_AFTERColumn.Visible = false;
            // 
            // KOUZA_NAME_AFTERColumn
            // 
            this.KOUZA_NAME_AFTERColumn.DataPropertyName = "KOUZA_NAME_AFTER";
            this.KOUZA_NAME_AFTERColumn.DefaultBackColor = System.Drawing.Color.Empty;
            this.KOUZA_NAME_AFTERColumn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_NAME_AFTERColumn.FocusOutCheckMethod")));
            this.KOUZA_NAME_AFTERColumn.HeaderText = "口座名義";
            this.KOUZA_NAME_AFTERColumn.Name = "KOUZA_NAME_AFTERColumn";
            this.KOUZA_NAME_AFTERColumn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KOUZA_NAME_AFTERColumn.PopupSearchSendParams")));
            this.KOUZA_NAME_AFTERColumn.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KOUZA_NAME_AFTERColumn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KOUZA_NAME_AFTERColumn.popupWindowSetting")));
            this.KOUZA_NAME_AFTERColumn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_NAME_AFTERColumn.RegistCheckMethod")));
            this.KOUZA_NAME_AFTERColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.KOUZA_NAME_AFTERColumn.Visible = false;
            // 
            // labelBANK_SHITEN_AFTER
            // 
            this.labelBANK_SHITEN_AFTER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.labelBANK_SHITEN_AFTER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.labelBANK_SHITEN_AFTER.ForeColor = System.Drawing.Color.White;
            this.labelBANK_SHITEN_AFTER.Location = new System.Drawing.Point(499, 416);
            this.labelBANK_SHITEN_AFTER.Name = "labelBANK_SHITEN_AFTER";
            this.labelBANK_SHITEN_AFTER.Size = new System.Drawing.Size(135, 20);
            this.labelBANK_SHITEN_AFTER.TabIndex = 10;
            this.labelBANK_SHITEN_AFTER.Text = "銀行口座（変更後）";
            this.labelBANK_SHITEN_AFTER.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BANK_SHITEN_NAME_AFTER
            // 
            this.BANK_SHITEN_NAME_AFTER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.BANK_SHITEN_NAME_AFTER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BANK_SHITEN_NAME_AFTER.DefaultBackColor = System.Drawing.Color.Empty;
            this.BANK_SHITEN_NAME_AFTER.DisplayPopUp = null;
            this.BANK_SHITEN_NAME_AFTER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_SHITEN_NAME_AFTER.FocusOutCheckMethod")));
            this.BANK_SHITEN_NAME_AFTER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.BANK_SHITEN_NAME_AFTER.ForeColor = System.Drawing.Color.Black;
            this.BANK_SHITEN_NAME_AFTER.IsInputErrorOccured = false;
            this.BANK_SHITEN_NAME_AFTER.Location = new System.Drawing.Point(676, 416);
            this.BANK_SHITEN_NAME_AFTER.Name = "BANK_SHITEN_NAME_AFTER";
            this.BANK_SHITEN_NAME_AFTER.PopupAfterExecute = null;
            this.BANK_SHITEN_NAME_AFTER.PopupBeforeExecute = null;
            this.BANK_SHITEN_NAME_AFTER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BANK_SHITEN_NAME_AFTER.PopupSearchSendParams")));
            this.BANK_SHITEN_NAME_AFTER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BANK_SHITEN_NAME_AFTER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BANK_SHITEN_NAME_AFTER.popupWindowSetting")));
            this.BANK_SHITEN_NAME_AFTER.prevText = null;
            this.BANK_SHITEN_NAME_AFTER.ReadOnly = true;
            this.BANK_SHITEN_NAME_AFTER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_SHITEN_NAME_AFTER.RegistCheckMethod")));
            this.BANK_SHITEN_NAME_AFTER.Size = new System.Drawing.Size(320, 20);
            this.BANK_SHITEN_NAME_AFTER.TabIndex = 12;
            this.BANK_SHITEN_NAME_AFTER.TabStop = false;
            this.BANK_SHITEN_NAME_AFTER.Tag = "";
            // 
            // BANK_SHITEN_CD_AFTER
            // 
            this.BANK_SHITEN_CD_AFTER.BackColor = System.Drawing.SystemColors.Window;
            this.BANK_SHITEN_CD_AFTER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BANK_SHITEN_CD_AFTER.ChangeUpperCase = true;
            this.BANK_SHITEN_CD_AFTER.CharacterLimitList = null;
            this.BANK_SHITEN_CD_AFTER.CharactersNumber = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.BANK_SHITEN_CD_AFTER.DBFieldsName = "BANK_SHITEN_CD";
            this.BANK_SHITEN_CD_AFTER.DefaultBackColor = System.Drawing.Color.Empty;
            this.BANK_SHITEN_CD_AFTER.DisplayPopUp = null;
            this.BANK_SHITEN_CD_AFTER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_SHITEN_CD_AFTER.FocusOutCheckMethod")));
            this.BANK_SHITEN_CD_AFTER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.BANK_SHITEN_CD_AFTER.ForeColor = System.Drawing.Color.Black;
            this.BANK_SHITEN_CD_AFTER.GetCodeMasterField = "BANK_CD,BANK_NAME_RYAKU,BANK_SHITEN_CD,BANK_SHIETN_NAME_RYAKU,BANK_SHITEN_CD,KOUZ" +
                "A_SHURUI,KOUZA_NO,KOUZA_NAME";
            this.BANK_SHITEN_CD_AFTER.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.BANK_SHITEN_CD_AFTER.IsInputErrorOccured = false;
            this.BANK_SHITEN_CD_AFTER.ItemDefinedTypes = "varchar";
            this.BANK_SHITEN_CD_AFTER.Location = new System.Drawing.Point(637, 416);
            this.BANK_SHITEN_CD_AFTER.MaxLength = 3;
            this.BANK_SHITEN_CD_AFTER.Name = "BANK_SHITEN_CD_AFTER";
            this.BANK_SHITEN_CD_AFTER.PopupAfterExecute = null;
            this.BANK_SHITEN_CD_AFTER.PopupBeforeExecute = null;
            this.BANK_SHITEN_CD_AFTER.PopupGetMasterField = "BANK_CD,BANK_NAME_RYAKU,BANK_SHITEN_CD,BANK_SHIETN_NAME_RYAKU,BANK_SHITEN_CD,KOUZ" +
                "A_SHURUI,KOUZA_NO,KOUZA_NAME";
            this.BANK_SHITEN_CD_AFTER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BANK_SHITEN_CD_AFTER.PopupSearchSendParams")));
            this.BANK_SHITEN_CD_AFTER.PopupSendParams = new string[0];
            this.BANK_SHITEN_CD_AFTER.PopupSetFormField = "BANK_CD_AFTER,BANK_NAME_AFTER,BANK_SHITEN_CD_AFTER,BANK_SHITEN_NAME_AFTER,BANK_SH" +
                "ITEN_CD_AFTER_OLD,KOUZA_SHURUI_AFTER,KOUZA_NO_AFTER,KOUZA_NAME_AFTER";
            this.BANK_SHITEN_CD_AFTER.PopupWindowId = r_framework.Const.WINDOW_ID.M_BANK_SHITEN;
            this.BANK_SHITEN_CD_AFTER.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.BANK_SHITEN_CD_AFTER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BANK_SHITEN_CD_AFTER.popupWindowSetting")));
            this.BANK_SHITEN_CD_AFTER.prevText = null;
            this.BANK_SHITEN_CD_AFTER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_SHITEN_CD_AFTER.RegistCheckMethod")));
            this.BANK_SHITEN_CD_AFTER.SetFormField = "BANK_CD_AFTER,BANK_NAME_AFTER,BANK_SHITEN_CD_AFTER,BANK_SHITEN_NAME_AFTER,BANK_SH" +
                "ITEN_CD_AFTER_OLD,KOUZA_SHURUI_AFTER,KOUZA_NO_AFTER,KOUZA_NAME_AFTER";
            this.BANK_SHITEN_CD_AFTER.Size = new System.Drawing.Size(40, 20);
            this.BANK_SHITEN_CD_AFTER.TabIndex = 11;
            this.BANK_SHITEN_CD_AFTER.Tag = "銀行支店CDを入力してください（スペースキー押下にて、検索画面を表示します）";
            this.BANK_SHITEN_CD_AFTER.ZeroPaddengFlag = true;
            this.BANK_SHITEN_CD_AFTER.Validating += new System.ComponentModel.CancelEventHandler(this.BANK_SHITEN_CD_Validating);
            // 
            // KOUZA_SHURUI
            // 
            this.KOUZA_SHURUI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KOUZA_SHURUI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KOUZA_SHURUI.DefaultBackColor = System.Drawing.Color.Empty;
            this.KOUZA_SHURUI.DisplayPopUp = null;
            this.KOUZA_SHURUI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_SHURUI.FocusOutCheckMethod")));
            this.KOUZA_SHURUI.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KOUZA_SHURUI.ForeColor = System.Drawing.Color.Black;
            this.KOUZA_SHURUI.IsInputErrorOccured = false;
            this.KOUZA_SHURUI.Location = new System.Drawing.Point(502, 2);
            this.KOUZA_SHURUI.Name = "KOUZA_SHURUI";
            this.KOUZA_SHURUI.PopupAfterExecute = null;
            this.KOUZA_SHURUI.PopupBeforeExecute = null;
            this.KOUZA_SHURUI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KOUZA_SHURUI.PopupSearchSendParams")));
            this.KOUZA_SHURUI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KOUZA_SHURUI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KOUZA_SHURUI.popupWindowSetting")));
            this.KOUZA_SHURUI.prevText = null;
            this.KOUZA_SHURUI.ReadOnly = true;
            this.KOUZA_SHURUI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_SHURUI.RegistCheckMethod")));
            this.KOUZA_SHURUI.Size = new System.Drawing.Size(76, 20);
            this.KOUZA_SHURUI.TabIndex = 14;
            this.KOUZA_SHURUI.TabStop = false;
            this.KOUZA_SHURUI.Tag = "";
            this.KOUZA_SHURUI.Visible = false;
            // 
            // KOUZA_NO
            // 
            this.KOUZA_NO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KOUZA_NO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KOUZA_NO.DefaultBackColor = System.Drawing.Color.Empty;
            this.KOUZA_NO.DisplayPopUp = null;
            this.KOUZA_NO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_NO.FocusOutCheckMethod")));
            this.KOUZA_NO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KOUZA_NO.ForeColor = System.Drawing.Color.Black;
            this.KOUZA_NO.IsInputErrorOccured = false;
            this.KOUZA_NO.Location = new System.Drawing.Point(584, 2);
            this.KOUZA_NO.Name = "KOUZA_NO";
            this.KOUZA_NO.PopupAfterExecute = null;
            this.KOUZA_NO.PopupBeforeExecute = null;
            this.KOUZA_NO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KOUZA_NO.PopupSearchSendParams")));
            this.KOUZA_NO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KOUZA_NO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KOUZA_NO.popupWindowSetting")));
            this.KOUZA_NO.prevText = null;
            this.KOUZA_NO.ReadOnly = true;
            this.KOUZA_NO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_NO.RegistCheckMethod")));
            this.KOUZA_NO.Size = new System.Drawing.Size(76, 20);
            this.KOUZA_NO.TabIndex = 15;
            this.KOUZA_NO.TabStop = false;
            this.KOUZA_NO.Tag = "";
            this.KOUZA_NO.Visible = false;
            // 
            // KOUZA_SHURUI_AFTER
            // 
            this.KOUZA_SHURUI_AFTER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KOUZA_SHURUI_AFTER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KOUZA_SHURUI_AFTER.DefaultBackColor = System.Drawing.Color.Empty;
            this.KOUZA_SHURUI_AFTER.DisplayPopUp = null;
            this.KOUZA_SHURUI_AFTER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_SHURUI_AFTER.FocusOutCheckMethod")));
            this.KOUZA_SHURUI_AFTER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KOUZA_SHURUI_AFTER.ForeColor = System.Drawing.Color.Black;
            this.KOUZA_SHURUI_AFTER.IsInputErrorOccured = false;
            this.KOUZA_SHURUI_AFTER.Location = new System.Drawing.Point(637, 441);
            this.KOUZA_SHURUI_AFTER.Name = "KOUZA_SHURUI_AFTER";
            this.KOUZA_SHURUI_AFTER.PopupAfterExecute = null;
            this.KOUZA_SHURUI_AFTER.PopupBeforeExecute = null;
            this.KOUZA_SHURUI_AFTER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KOUZA_SHURUI_AFTER.PopupSearchSendParams")));
            this.KOUZA_SHURUI_AFTER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KOUZA_SHURUI_AFTER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KOUZA_SHURUI_AFTER.popupWindowSetting")));
            this.KOUZA_SHURUI_AFTER.prevText = null;
            this.KOUZA_SHURUI_AFTER.ReadOnly = true;
            this.KOUZA_SHURUI_AFTER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_SHURUI_AFTER.RegistCheckMethod")));
            this.KOUZA_SHURUI_AFTER.Size = new System.Drawing.Size(76, 20);
            this.KOUZA_SHURUI_AFTER.TabIndex = 22;
            this.KOUZA_SHURUI_AFTER.TabStop = false;
            this.KOUZA_SHURUI_AFTER.Tag = "";
            this.KOUZA_SHURUI_AFTER.Visible = false;
            // 
            // KOUZA_NO_AFTER
            // 
            this.KOUZA_NO_AFTER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KOUZA_NO_AFTER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KOUZA_NO_AFTER.DefaultBackColor = System.Drawing.Color.Empty;
            this.KOUZA_NO_AFTER.DisplayPopUp = null;
            this.KOUZA_NO_AFTER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_NO_AFTER.FocusOutCheckMethod")));
            this.KOUZA_NO_AFTER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KOUZA_NO_AFTER.ForeColor = System.Drawing.Color.Black;
            this.KOUZA_NO_AFTER.IsInputErrorOccured = false;
            this.KOUZA_NO_AFTER.Location = new System.Drawing.Point(722, 440);
            this.KOUZA_NO_AFTER.Name = "KOUZA_NO_AFTER";
            this.KOUZA_NO_AFTER.PopupAfterExecute = null;
            this.KOUZA_NO_AFTER.PopupBeforeExecute = null;
            this.KOUZA_NO_AFTER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KOUZA_NO_AFTER.PopupSearchSendParams")));
            this.KOUZA_NO_AFTER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KOUZA_NO_AFTER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KOUZA_NO_AFTER.popupWindowSetting")));
            this.KOUZA_NO_AFTER.prevText = null;
            this.KOUZA_NO_AFTER.ReadOnly = true;
            this.KOUZA_NO_AFTER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_NO_AFTER.RegistCheckMethod")));
            this.KOUZA_NO_AFTER.Size = new System.Drawing.Size(76, 20);
            this.KOUZA_NO_AFTER.TabIndex = 23;
            this.KOUZA_NO_AFTER.TabStop = false;
            this.KOUZA_NO_AFTER.Tag = "";
            this.KOUZA_NO_AFTER.Visible = false;
            // 
            // KOUZA_NAME
            // 
            this.KOUZA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KOUZA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KOUZA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.KOUZA_NAME.DisplayPopUp = null;
            this.KOUZA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_NAME.FocusOutCheckMethod")));
            this.KOUZA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KOUZA_NAME.ForeColor = System.Drawing.Color.Black;
            this.KOUZA_NAME.IsInputErrorOccured = false;
            this.KOUZA_NAME.Location = new System.Drawing.Point(666, 2);
            this.KOUZA_NAME.Name = "KOUZA_NAME";
            this.KOUZA_NAME.PopupAfterExecute = null;
            this.KOUZA_NAME.PopupBeforeExecute = null;
            this.KOUZA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KOUZA_NAME.PopupSearchSendParams")));
            this.KOUZA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KOUZA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KOUZA_NAME.popupWindowSetting")));
            this.KOUZA_NAME.prevText = null;
            this.KOUZA_NAME.ReadOnly = true;
            this.KOUZA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_NAME.RegistCheckMethod")));
            this.KOUZA_NAME.Size = new System.Drawing.Size(76, 20);
            this.KOUZA_NAME.TabIndex = 16;
            this.KOUZA_NAME.TabStop = false;
            this.KOUZA_NAME.Tag = "";
            this.KOUZA_NAME.Visible = false;
            // 
            // KOUZA_NAME_AFTER
            // 
            this.KOUZA_NAME_AFTER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KOUZA_NAME_AFTER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KOUZA_NAME_AFTER.DefaultBackColor = System.Drawing.Color.Empty;
            this.KOUZA_NAME_AFTER.DisplayPopUp = null;
            this.KOUZA_NAME_AFTER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_NAME_AFTER.FocusOutCheckMethod")));
            this.KOUZA_NAME_AFTER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KOUZA_NAME_AFTER.ForeColor = System.Drawing.Color.Black;
            this.KOUZA_NAME_AFTER.IsInputErrorOccured = false;
            this.KOUZA_NAME_AFTER.Location = new System.Drawing.Point(804, 440);
            this.KOUZA_NAME_AFTER.Name = "KOUZA_NAME_AFTER";
            this.KOUZA_NAME_AFTER.PopupAfterExecute = null;
            this.KOUZA_NAME_AFTER.PopupBeforeExecute = null;
            this.KOUZA_NAME_AFTER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KOUZA_NAME_AFTER.PopupSearchSendParams")));
            this.KOUZA_NAME_AFTER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KOUZA_NAME_AFTER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KOUZA_NAME_AFTER.popupWindowSetting")));
            this.KOUZA_NAME_AFTER.prevText = null;
            this.KOUZA_NAME_AFTER.ReadOnly = true;
            this.KOUZA_NAME_AFTER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_NAME_AFTER.RegistCheckMethod")));
            this.KOUZA_NAME_AFTER.Size = new System.Drawing.Size(76, 20);
            this.KOUZA_NAME_AFTER.TabIndex = 24;
            this.KOUZA_NAME_AFTER.TabStop = false;
            this.KOUZA_NAME_AFTER.Tag = "";
            this.KOUZA_NAME_AFTER.Visible = false;
            // 
            // KOUZA_SHURUI_GRID
            // 
            this.KOUZA_SHURUI_GRID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KOUZA_SHURUI_GRID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KOUZA_SHURUI_GRID.DefaultBackColor = System.Drawing.Color.Empty;
            this.KOUZA_SHURUI_GRID.DisplayPopUp = null;
            this.KOUZA_SHURUI_GRID.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_SHURUI_GRID.FocusOutCheckMethod")));
            this.KOUZA_SHURUI_GRID.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KOUZA_SHURUI_GRID.ForeColor = System.Drawing.Color.Black;
            this.KOUZA_SHURUI_GRID.IsInputErrorOccured = false;
            this.KOUZA_SHURUI_GRID.Location = new System.Drawing.Point(171, 442);
            this.KOUZA_SHURUI_GRID.Name = "KOUZA_SHURUI_GRID";
            this.KOUZA_SHURUI_GRID.PopupAfterExecute = null;
            this.KOUZA_SHURUI_GRID.PopupBeforeExecute = null;
            this.KOUZA_SHURUI_GRID.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KOUZA_SHURUI_GRID.PopupSearchSendParams")));
            this.KOUZA_SHURUI_GRID.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KOUZA_SHURUI_GRID.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KOUZA_SHURUI_GRID.popupWindowSetting")));
            this.KOUZA_SHURUI_GRID.prevText = null;
            this.KOUZA_SHURUI_GRID.ReadOnly = true;
            this.KOUZA_SHURUI_GRID.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_SHURUI_GRID.RegistCheckMethod")));
            this.KOUZA_SHURUI_GRID.Size = new System.Drawing.Size(76, 20);
            this.KOUZA_SHURUI_GRID.TabIndex = 18;
            this.KOUZA_SHURUI_GRID.TabStop = false;
            this.KOUZA_SHURUI_GRID.Tag = "";
            this.KOUZA_SHURUI_GRID.Visible = false;
            // 
            // KOUZA_NO_GRID
            // 
            this.KOUZA_NO_GRID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KOUZA_NO_GRID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KOUZA_NO_GRID.DefaultBackColor = System.Drawing.Color.Empty;
            this.KOUZA_NO_GRID.DisplayPopUp = null;
            this.KOUZA_NO_GRID.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_NO_GRID.FocusOutCheckMethod")));
            this.KOUZA_NO_GRID.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KOUZA_NO_GRID.ForeColor = System.Drawing.Color.Black;
            this.KOUZA_NO_GRID.IsInputErrorOccured = false;
            this.KOUZA_NO_GRID.Location = new System.Drawing.Point(256, 441);
            this.KOUZA_NO_GRID.Name = "KOUZA_NO_GRID";
            this.KOUZA_NO_GRID.PopupAfterExecute = null;
            this.KOUZA_NO_GRID.PopupBeforeExecute = null;
            this.KOUZA_NO_GRID.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KOUZA_NO_GRID.PopupSearchSendParams")));
            this.KOUZA_NO_GRID.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KOUZA_NO_GRID.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KOUZA_NO_GRID.popupWindowSetting")));
            this.KOUZA_NO_GRID.prevText = null;
            this.KOUZA_NO_GRID.ReadOnly = true;
            this.KOUZA_NO_GRID.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_NO_GRID.RegistCheckMethod")));
            this.KOUZA_NO_GRID.Size = new System.Drawing.Size(76, 20);
            this.KOUZA_NO_GRID.TabIndex = 19;
            this.KOUZA_NO_GRID.TabStop = false;
            this.KOUZA_NO_GRID.Tag = "";
            this.KOUZA_NO_GRID.Visible = false;
            // 
            // KOUZA_NAME_GRID
            // 
            this.KOUZA_NAME_GRID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KOUZA_NAME_GRID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KOUZA_NAME_GRID.DefaultBackColor = System.Drawing.Color.Empty;
            this.KOUZA_NAME_GRID.DisplayPopUp = null;
            this.KOUZA_NAME_GRID.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_NAME_GRID.FocusOutCheckMethod")));
            this.KOUZA_NAME_GRID.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KOUZA_NAME_GRID.ForeColor = System.Drawing.Color.Black;
            this.KOUZA_NAME_GRID.IsInputErrorOccured = false;
            this.KOUZA_NAME_GRID.Location = new System.Drawing.Point(338, 441);
            this.KOUZA_NAME_GRID.Name = "KOUZA_NAME_GRID";
            this.KOUZA_NAME_GRID.PopupAfterExecute = null;
            this.KOUZA_NAME_GRID.PopupBeforeExecute = null;
            this.KOUZA_NAME_GRID.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KOUZA_NAME_GRID.PopupSearchSendParams")));
            this.KOUZA_NAME_GRID.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KOUZA_NAME_GRID.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KOUZA_NAME_GRID.popupWindowSetting")));
            this.KOUZA_NAME_GRID.prevText = null;
            this.KOUZA_NAME_GRID.ReadOnly = true;
            this.KOUZA_NAME_GRID.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_NAME_GRID.RegistCheckMethod")));
            this.KOUZA_NAME_GRID.Size = new System.Drawing.Size(76, 20);
            this.KOUZA_NAME_GRID.TabIndex = 20;
            this.KOUZA_NAME_GRID.TabStop = false;
            this.KOUZA_NAME_GRID.Tag = "";
            this.KOUZA_NAME_GRID.Visible = false;
            // 
            // BANK_SHITEN_CD_GRID
            // 
            this.BANK_SHITEN_CD_GRID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.BANK_SHITEN_CD_GRID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BANK_SHITEN_CD_GRID.DefaultBackColor = System.Drawing.Color.Empty;
            this.BANK_SHITEN_CD_GRID.DisplayPopUp = null;
            this.BANK_SHITEN_CD_GRID.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_SHITEN_CD_GRID.FocusOutCheckMethod")));
            this.BANK_SHITEN_CD_GRID.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.BANK_SHITEN_CD_GRID.ForeColor = System.Drawing.Color.Black;
            this.BANK_SHITEN_CD_GRID.IsInputErrorOccured = false;
            this.BANK_SHITEN_CD_GRID.Location = new System.Drawing.Point(89, 442);
            this.BANK_SHITEN_CD_GRID.Name = "BANK_SHITEN_CD_GRID";
            this.BANK_SHITEN_CD_GRID.PopupAfterExecute = null;
            this.BANK_SHITEN_CD_GRID.PopupBeforeExecute = null;
            this.BANK_SHITEN_CD_GRID.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BANK_SHITEN_CD_GRID.PopupSearchSendParams")));
            this.BANK_SHITEN_CD_GRID.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BANK_SHITEN_CD_GRID.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BANK_SHITEN_CD_GRID.popupWindowSetting")));
            this.BANK_SHITEN_CD_GRID.prevText = null;
            this.BANK_SHITEN_CD_GRID.ReadOnly = true;
            this.BANK_SHITEN_CD_GRID.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_SHITEN_CD_GRID.RegistCheckMethod")));
            this.BANK_SHITEN_CD_GRID.Size = new System.Drawing.Size(76, 20);
            this.BANK_SHITEN_CD_GRID.TabIndex = 17;
            this.BANK_SHITEN_CD_GRID.TabStop = false;
            this.BANK_SHITEN_CD_GRID.Tag = "";
            this.BANK_SHITEN_CD_GRID.Visible = false;
            // 
            // BANK_SHITEN_CD_OLD
            // 
            this.BANK_SHITEN_CD_OLD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.BANK_SHITEN_CD_OLD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BANK_SHITEN_CD_OLD.DefaultBackColor = System.Drawing.Color.Empty;
            this.BANK_SHITEN_CD_OLD.DisplayPopUp = null;
            this.BANK_SHITEN_CD_OLD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_SHITEN_CD_OLD.FocusOutCheckMethod")));
            this.BANK_SHITEN_CD_OLD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.BANK_SHITEN_CD_OLD.ForeColor = System.Drawing.Color.Black;
            this.BANK_SHITEN_CD_OLD.IsInputErrorOccured = false;
            this.BANK_SHITEN_CD_OLD.Location = new System.Drawing.Point(420, 2);
            this.BANK_SHITEN_CD_OLD.Name = "BANK_SHITEN_CD_OLD";
            this.BANK_SHITEN_CD_OLD.PopupAfterExecute = null;
            this.BANK_SHITEN_CD_OLD.PopupBeforeExecute = null;
            this.BANK_SHITEN_CD_OLD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BANK_SHITEN_CD_OLD.PopupSearchSendParams")));
            this.BANK_SHITEN_CD_OLD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BANK_SHITEN_CD_OLD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BANK_SHITEN_CD_OLD.popupWindowSetting")));
            this.BANK_SHITEN_CD_OLD.prevText = null;
            this.BANK_SHITEN_CD_OLD.ReadOnly = true;
            this.BANK_SHITEN_CD_OLD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_SHITEN_CD_OLD.RegistCheckMethod")));
            this.BANK_SHITEN_CD_OLD.Size = new System.Drawing.Size(76, 20);
            this.BANK_SHITEN_CD_OLD.TabIndex = 13;
            this.BANK_SHITEN_CD_OLD.TabStop = false;
            this.BANK_SHITEN_CD_OLD.Tag = "";
            this.BANK_SHITEN_CD_OLD.Visible = false;
            // 
            // BANK_SHITEN_CD_AFTER_OLD
            // 
            this.BANK_SHITEN_CD_AFTER_OLD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.BANK_SHITEN_CD_AFTER_OLD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BANK_SHITEN_CD_AFTER_OLD.DefaultBackColor = System.Drawing.Color.Empty;
            this.BANK_SHITEN_CD_AFTER_OLD.DisplayPopUp = null;
            this.BANK_SHITEN_CD_AFTER_OLD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_SHITEN_CD_AFTER_OLD.FocusOutCheckMethod")));
            this.BANK_SHITEN_CD_AFTER_OLD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.BANK_SHITEN_CD_AFTER_OLD.ForeColor = System.Drawing.Color.Black;
            this.BANK_SHITEN_CD_AFTER_OLD.IsInputErrorOccured = false;
            this.BANK_SHITEN_CD_AFTER_OLD.Location = new System.Drawing.Point(555, 441);
            this.BANK_SHITEN_CD_AFTER_OLD.Name = "BANK_SHITEN_CD_AFTER_OLD";
            this.BANK_SHITEN_CD_AFTER_OLD.PopupAfterExecute = null;
            this.BANK_SHITEN_CD_AFTER_OLD.PopupBeforeExecute = null;
            this.BANK_SHITEN_CD_AFTER_OLD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BANK_SHITEN_CD_AFTER_OLD.PopupSearchSendParams")));
            this.BANK_SHITEN_CD_AFTER_OLD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BANK_SHITEN_CD_AFTER_OLD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BANK_SHITEN_CD_AFTER_OLD.popupWindowSetting")));
            this.BANK_SHITEN_CD_AFTER_OLD.prevText = null;
            this.BANK_SHITEN_CD_AFTER_OLD.ReadOnly = true;
            this.BANK_SHITEN_CD_AFTER_OLD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_SHITEN_CD_AFTER_OLD.RegistCheckMethod")));
            this.BANK_SHITEN_CD_AFTER_OLD.Size = new System.Drawing.Size(76, 20);
            this.BANK_SHITEN_CD_AFTER_OLD.TabIndex = 21;
            this.BANK_SHITEN_CD_AFTER_OLD.Tag = "";
            this.BANK_SHITEN_CD_AFTER_OLD.Visible = false;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.BANK_SHITEN_NAME_AFTER);
            this.Controls.Add(this.BANK_NAME_AFTER);
            this.Controls.Add(this.labelBANK_SHITEN_AFTER);
            this.Controls.Add(this.BANK_CD_AFTER);
            this.Controls.Add(this.labelBANK_AFTER);
            this.Controls.Add(this.BANK_SHITEN_CD_AFTER);
            this.Controls.Add(this.BANK_SHITEN_NAME);
            this.Controls.Add(this.BANK_SHITEN_CD);
            this.Controls.Add(this.KOUZA_NAME_GRID);
            this.Controls.Add(this.KOUZA_NO_GRID);
            this.Controls.Add(this.KOUZA_NAME_AFTER);
            this.Controls.Add(this.KOUZA_NO_AFTER);
            this.Controls.Add(this.KOUZA_NAME);
            this.Controls.Add(this.BANK_SHITEN_CD_GRID);
            this.Controls.Add(this.KOUZA_SHURUI_GRID);
            this.Controls.Add(this.KOUZA_NO);
            this.Controls.Add(this.BANK_SHITEN_CD_AFTER_OLD);
            this.Controls.Add(this.KOUZA_SHURUI_AFTER);
            this.Controls.Add(this.BANK_SHITEN_CD_OLD);
            this.Controls.Add(this.KOUZA_SHURUI);
            this.Controls.Add(this.BANK_NAME);
            this.Controls.Add(this.labelBANK_SHITEN);
            this.Controls.Add(this.BANK_CD);
            this.Controls.Add(this.labelBANK);
            this.Controls.Add(this.dgvBank);
            this.Name = "UIForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UIForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBank)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomTextBox BANK_NAME_AFTER;
        public r_framework.CustomControl.CustomAlphaNumTextBox BANK_CD_AFTER;
        public System.Windows.Forms.Label labelBANK_AFTER;
        public r_framework.CustomControl.CustomTextBox BANK_SHITEN_NAME;
        public r_framework.CustomControl.CustomAlphaNumTextBox BANK_SHITEN_CD;
        public r_framework.CustomControl.CustomTextBox BANK_NAME;
        public System.Windows.Forms.Label labelBANK_SHITEN;
        public r_framework.CustomControl.CustomAlphaNumTextBox BANK_CD;
        public System.Windows.Forms.Label labelBANK;
        public r_framework.CustomControl.CustomDataGridView dgvBank;
        public System.Windows.Forms.Label labelBANK_SHITEN_AFTER;
        public r_framework.CustomControl.CustomTextBox BANK_SHITEN_NAME_AFTER;
        public r_framework.CustomControl.CustomAlphaNumTextBox BANK_SHITEN_CD_AFTER;
        public r_framework.CustomControl.CustomTextBox KOUZA_SHURUI;
        public r_framework.CustomControl.CustomTextBox KOUZA_NO;
        public r_framework.CustomControl.CustomTextBox KOUZA_SHURUI_AFTER;
        public r_framework.CustomControl.CustomTextBox KOUZA_NO_AFTER;
        public r_framework.CustomControl.CustomTextBox KOUZA_NAME;
        public r_framework.CustomControl.CustomTextBox KOUZA_NAME_AFTER;
        public r_framework.CustomControl.CustomTextBox KOUZA_SHURUI_GRID;
        public r_framework.CustomControl.CustomTextBox KOUZA_NO_GRID;
        public r_framework.CustomControl.CustomTextBox KOUZA_NAME_GRID;
        public r_framework.CustomControl.CustomTextBox BANK_SHITEN_CD_GRID;
        public r_framework.CustomControl.CustomTextBox BANK_SHITEN_CD_OLD;
        public r_framework.CustomControl.CustomTextBox BANK_SHITEN_CD_AFTER_OLD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn TORIHIKISAKI_CDColumn;
        private r_framework.CustomControl.DgvCustomTextBoxColumn TORIHIKISAKI_NAME1Column;
        private r_framework.CustomControl.DgvCustomTextBoxColumn FURIKOMI_BANK_CDColumn;
        private r_framework.CustomControl.DgvCustomTextBoxColumn BANK_NAMEColumn;
        private r_framework.CustomControl.DgvCustomTextBoxColumn FURIKOMI_BANK_SHITEN_CDColumn;
        private r_framework.CustomControl.DgvCustomTextBoxColumn BANK_SHITEN_NAMEColumn;
        private r_framework.CustomControl.DgvCustomTextBoxColumn FURIKOMI_BANK_CD_AFTERColumn;
        private r_framework.CustomControl.DgvCustomTextBoxColumn BANK_NAME_AFTERColumn;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn FURIKOMI_BANK_SHITEN_CD_AFTERColumn;
        private r_framework.CustomControl.DgvCustomTextBoxColumn BANK_SHITEN_NAME_AFTERColumn;
        private r_framework.CustomControl.DgvCustomTextBoxColumn KOUZA_SHURUI_AFTERColumn;
        private r_framework.CustomControl.DgvCustomTextBoxColumn KOUZA_NO_AFTERColumn;
        private r_framework.CustomControl.DgvCustomTextBoxColumn KOUZA_NAME_AFTERColumn;
    }
}