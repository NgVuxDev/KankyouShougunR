namespace Shougun.Core.ExternalConnection.DenshiKeiyakuShinseiKeiro.APP
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.CONDITION_VALUE = new r_framework.CustomControl.CustomTextBox();
            this.CONDITION_ITEM = new r_framework.CustomControl.CustomTextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.Ichiran = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME = new r_framework.CustomControl.CustomTextBox();
            this.popBtn_Bpzkhm = new r_framework.CustomControl.CustomPopupOpenButton();
            this.dgvCustomCheckBoxColumn1 = new r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn();
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.chb_delete = new r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn();
            this.SHAIN_CD = new r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn();
            this.SHAIN_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.UPDATE_USER = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.UPDATE_DATE = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.UPDATE_PC = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CREATE_USER = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CREATE_DATE = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CREATE_PC = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).BeginInit();
            this.SuspendLayout();
            // 
            // CONDITION_VALUE
            // 
            this.CONDITION_VALUE.BackColor = System.Drawing.SystemColors.Window;
            this.CONDITION_VALUE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CONDITION_VALUE.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.CONDITION_VALUE.DBFieldsName = "";
            this.CONDITION_VALUE.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION_VALUE.DisplayItemName = "検索条件";
            this.CONDITION_VALUE.DisplayPopUp = null;
            this.CONDITION_VALUE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_VALUE.FocusOutCheckMethod")));
            this.CONDITION_VALUE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CONDITION_VALUE.ForeColor = System.Drawing.Color.Black;
            this.CONDITION_VALUE.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.CONDITION_VALUE.IsInputErrorOccured = false;
            this.CONDITION_VALUE.ItemDefinedTypes = "";
            this.CONDITION_VALUE.Location = new System.Drawing.Point(248, 32);
            this.CONDITION_VALUE.MaxLength = 20;
            this.CONDITION_VALUE.Name = "CONDITION_VALUE";
            this.CONDITION_VALUE.PopupAfterExecute = null;
            this.CONDITION_VALUE.PopupBeforeExecute = null;
            this.CONDITION_VALUE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONDITION_VALUE.PopupSearchSendParams")));
            this.CONDITION_VALUE.PopupSetFormField = "";
            this.CONDITION_VALUE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION_VALUE.PopupWindowName = "";
            this.CONDITION_VALUE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONDITION_VALUE.popupWindowSetting")));
            this.CONDITION_VALUE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_VALUE.RegistCheckMethod")));
            this.CONDITION_VALUE.SetFormField = "";
            this.CONDITION_VALUE.ShortItemName = "検索条件";
            this.CONDITION_VALUE.Size = new System.Drawing.Size(290, 20);
            this.CONDITION_VALUE.TabIndex = 3;
            this.CONDITION_VALUE.Tag = "検索する文字を入力してください";
            this.CONDITION_VALUE.Visible = false;
            // 
            // CONDITION_ITEM
            // 
            this.CONDITION_ITEM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CONDITION_ITEM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CONDITION_ITEM.ChangeUpperCase = true;
            this.CONDITION_ITEM.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.CONDITION_ITEM.DBFieldsName = "";
            this.CONDITION_ITEM.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION_ITEM.DisplayItemName = "検索条件";
            this.CONDITION_ITEM.DisplayPopUp = null;
            this.CONDITION_ITEM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_ITEM.FocusOutCheckMethod")));
            this.CONDITION_ITEM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CONDITION_ITEM.ForeColor = System.Drawing.Color.Black;
            this.CONDITION_ITEM.IsInputErrorOccured = false;
            this.CONDITION_ITEM.Location = new System.Drawing.Point(129, 32);
            this.CONDITION_ITEM.MaxLength = 20;
            this.CONDITION_ITEM.Name = "CONDITION_ITEM";
            this.CONDITION_ITEM.PopupAfterExecute = null;
            this.CONDITION_ITEM.PopupAfterExecuteMethod = "CONDITION_ITEM_PopupAfterExecuteMethod";
            this.CONDITION_ITEM.PopupBeforeExecute = null;
            this.CONDITION_ITEM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONDITION_ITEM.PopupSearchSendParams")));
            this.CONDITION_ITEM.PopupSendParams = new string[] {
        "Ichiran"};
            this.CONDITION_ITEM.PopupSetFormField = "CONDITION_ITEM,CONDITION_VALUE";
            this.CONDITION_ITEM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION_ITEM.PopupWindowName = "マスタ検索項目ポップアップ";
            this.CONDITION_ITEM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONDITION_ITEM.popupWindowSetting")));
            this.CONDITION_ITEM.ReadOnly = true;
            this.CONDITION_ITEM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_ITEM.RegistCheckMethod")));
            this.CONDITION_ITEM.SetFormField = "CONDITION_ITEM,CONDITION_VALUE";
            this.CONDITION_ITEM.ShortItemName = "検索条件";
            this.CONDITION_ITEM.Size = new System.Drawing.Size(120, 20);
            this.CONDITION_ITEM.TabIndex = 2;
            this.CONDITION_ITEM.Tag = "検索条件項目を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.CONDITION_ITEM.Visible = false;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label16.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(0, 32);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(125, 20);
            this.label16.TabIndex = 0;
            this.label16.Text = "検索条件";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label16.Visible = false;
            // 
            // Ichiran
            // 
            this.Ichiran.AllowUserToDeleteRows = false;
            this.Ichiran.AllowUserToResizeColumns = false;
            this.Ichiran.AllowUserToResizeRows = false;
            this.Ichiran.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Ichiran.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Ichiran.ColumnHeadersHeight = 21;
            this.Ichiran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.Ichiran.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chb_delete,
            this.SHAIN_CD,
            this.SHAIN_NAME,
            this.UPDATE_USER,
            this.UPDATE_DATE,
            this.UPDATE_PC,
            this.CREATE_USER,
            this.CREATE_DATE,
            this.CREATE_PC,
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Ichiran.DefaultCellStyle = dataGridViewCellStyle12;
            this.Ichiran.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.Ichiran.EnableHeadersVisualStyles = false;
            this.Ichiran.GridColor = System.Drawing.Color.White;
            this.Ichiran.IsReload = false;
            this.Ichiran.LinkedDataPanelName = null;
            this.Ichiran.Location = new System.Drawing.Point(0, 55);
            this.Ichiran.MultiSelect = false;
            this.Ichiran.Name = "Ichiran";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle13.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Ichiran.RowHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.Ichiran.RowHeadersVisible = false;
            this.Ichiran.RowTemplate.Height = 21;
            this.Ichiran.ShowCellToolTips = false;
            this.Ichiran.Size = new System.Drawing.Size(978, 357);
            this.Ichiran.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "申請経路名※";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DENSHI_KEIYAKU_SHANAI_KEIRO_NAME
            // 
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.DisplayPopUp = null;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.FocusOutCheckMethod")));
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.ForeColor = System.Drawing.Color.Black;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.IsInputErrorOccured = false;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.Location = new System.Drawing.Point(159, 10);
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.Name = "DENSHI_KEIYAKU_SHANAI_KEIRO_NAME";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.PopupAfterExecute = null;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.PopupBeforeExecute = null;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.PopupSearchSendParams")));
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.popupWindowSetting")));
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.ReadOnly = true;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.RegistCheckMethod")));
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.Size = new System.Drawing.Size(352, 20);
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.TabIndex = 10;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.TabStop = false;
            // 
            // popBtn_Bpzkhm
            // 
            this.popBtn_Bpzkhm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.popBtn_Bpzkhm.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.popBtn_Bpzkhm.DBFieldsName = null;
            this.popBtn_Bpzkhm.DefaultBackColor = System.Drawing.Color.Empty;
            this.popBtn_Bpzkhm.DisplayItemName = "社内経路名CD";
            this.popBtn_Bpzkhm.DisplayPopUp = null;
            this.popBtn_Bpzkhm.ErrorMessage = null;
            this.popBtn_Bpzkhm.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("popBtn_Bpzkhm.FocusOutCheckMethod")));
            this.popBtn_Bpzkhm.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.popBtn_Bpzkhm.GetCodeMasterField = null;
            this.popBtn_Bpzkhm.Image = ((System.Drawing.Image)(resources.GetObject("popBtn_Bpzkhm.Image")));
            this.popBtn_Bpzkhm.ItemDefinedTypes = null;
            this.popBtn_Bpzkhm.LinkedSettingTextBox = null;
            this.popBtn_Bpzkhm.LinkedTextBoxs = null;
            this.popBtn_Bpzkhm.Location = new System.Drawing.Point(516, 9);
            this.popBtn_Bpzkhm.Name = "popBtn_Bpzkhm";
            this.popBtn_Bpzkhm.PopupAfterExecute = null;
            this.popBtn_Bpzkhm.PopupAfterExecuteMethod = "popBtn_Bpzkhm_PopupAfterExecuteMethod";
            this.popBtn_Bpzkhm.PopupBeforeExecute = null;
            this.popBtn_Bpzkhm.PopupBeforeExecuteMethod = "popBtn_Bpzkhm_PopupBeforeExecuteMethod";
            this.popBtn_Bpzkhm.PopupGetMasterField = "DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD,DENSHI_KEIYAKU_SHANAI_KEIRO_NAME";
            this.popBtn_Bpzkhm.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("popBtn_Bpzkhm.PopupSearchSendParams")));
            this.popBtn_Bpzkhm.PopupSetFormField = "DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD,DENSHI_KEIYAKU_SHANAI_KEIRO_NAME";
            this.popBtn_Bpzkhm.PopupWindowId = r_framework.Const.WINDOW_ID.M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME;
            this.popBtn_Bpzkhm.PopupWindowName = "マスタ共通ポップアップ";
            this.popBtn_Bpzkhm.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("popBtn_Bpzkhm.popupWindowSetting")));
            this.popBtn_Bpzkhm.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("popBtn_Bpzkhm.RegistCheckMethod")));
            this.popBtn_Bpzkhm.SearchDisplayFlag = 0;
            this.popBtn_Bpzkhm.SetFormField = "DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD,DENSHI_KEIYAKU_SHANAI_KEIRO_NAME";
            this.popBtn_Bpzkhm.ShortItemName = "社内経路名CD";
            this.popBtn_Bpzkhm.Size = new System.Drawing.Size(22, 22);
            this.popBtn_Bpzkhm.TabIndex = 1;
            this.popBtn_Bpzkhm.TabStop = false;
            this.popBtn_Bpzkhm.UseVisualStyleBackColor = false;
            this.popBtn_Bpzkhm.ZeroPaddengFlag = false;
            // 
            // dgvCustomCheckBoxColumn1
            // 
            this.dgvCustomCheckBoxColumn1.DataPropertyName = "DELETE_FLG";
            this.dgvCustomCheckBoxColumn1.DBFieldsName = "";
            this.dgvCustomCheckBoxColumn1.FillWeight = 43F;
            this.dgvCustomCheckBoxColumn1.FocusOutCheckMethod = null;
            this.dgvCustomCheckBoxColumn1.HeaderText = "削除";
            this.dgvCustomCheckBoxColumn1.ItemDefinedTypes = "bit";
            this.dgvCustomCheckBoxColumn1.Name = "dgvCustomCheckBoxColumn1";
            this.dgvCustomCheckBoxColumn1.RegistCheckMethod = null;
            this.dgvCustomCheckBoxColumn1.ToolTipText = "削除する場合、チェックしてください";
            this.dgvCustomCheckBoxColumn1.TrueValue = "true";
            this.dgvCustomCheckBoxColumn1.Width = 43;
            // 
            // DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD
            // 
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.AlphabetLimitFlag = false;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.BackColor = System.Drawing.SystemColors.Window;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.ChangeUpperCase = true;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.CharacterLimitList = null;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.CharactersNumber = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.DBFieldsName = "DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.DisplayItemName = "社内経路名CD";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.DisplayPopUp = null;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.FocusOutCheckMethod")));
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.ForeColor = System.Drawing.Color.Black;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.GetCodeMasterField = "DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD,DENSHI_KEIYAKU_SHANAI_KEIRO_NAME";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.IsInputErrorOccured = false;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Location = new System.Drawing.Point(129, 10);
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.MaxLength = 2;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Name = "DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.PopupAfterExecute = null;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.PopupBeforeExecute = null;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.PopupGetMasterField = "DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD,DENSHI_KEIYAKU_SHANAI_KEIRO_NAME";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.PopupSearchSendParams")));
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.PopupSetFormField = "DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD,DENSHI_KEIYAKU_SHANAI_KEIRO_NAME";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_DENSHI_KEIYAKU_SHANAI_KEIRO_NAME;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.popupWindowSetting")));
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.RegistCheckMethod")));
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.SetFormField = "DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD,DENSHI_KEIYAKU_SHANAI_KEIRO_NAME";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Size = new System.Drawing.Size(31, 20);
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.TabIndex = 0;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Tag = "社内経路名CDを入力してください（スペースキー押下にて、検索画面を表示します）";
            // 
            // chb_delete
            // 
            this.chb_delete.DBFieldsName = "DELETE_FLG";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = false;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.chb_delete.DefaultCellStyle = dataGridViewCellStyle2;
            this.chb_delete.FillWeight = 43F;
            this.chb_delete.FocusOutCheckMethod = null;
            this.chb_delete.Frozen = true;
            this.chb_delete.HeaderText = "削除";
            this.chb_delete.ItemDefinedTypes = "bit";
            this.chb_delete.Name = "chb_delete";
            this.chb_delete.RegistCheckMethod = null;
            this.chb_delete.ToolTipText = "削除する場合にはチェックを付けてください";
            this.chb_delete.TrueValue = "true";
            this.chb_delete.ViewSearchItem = false;
            this.chb_delete.Width = 43;
            // 
            // SHAIN_CD
            // 
            this.SHAIN_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.SHAIN_CD.DBFieldsName = "SHAIN_CD";
            this.SHAIN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.SHAIN_CD.DefaultCellStyle = dataGridViewCellStyle3;
            this.SHAIN_CD.DisplayItemName = "社員CD";
            this.SHAIN_CD.FillWeight = 60F;
            this.SHAIN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHAIN_CD.FocusOutCheckMethod")));
            this.SHAIN_CD.GetCodeMasterField = "SHAIN_CD,SHAIN_NAME";
            this.SHAIN_CD.HeaderText = "社員CD※";
            this.SHAIN_CD.ItemDefinedTypes = "varchar";
            this.SHAIN_CD.MaxInputLength = 6;
            this.SHAIN_CD.Name = "SHAIN_CD";
            this.SHAIN_CD.PopupAfterExecuteMethod = "";
            this.SHAIN_CD.PopupBeforeExecuteMethod = "SetShainPopupProperty";
            this.SHAIN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHAIN_CD.PopupSearchSendParams")));
            this.SHAIN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHAIN;
            this.SHAIN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.SHAIN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHAIN_CD.popupWindowSetting")));
            this.SHAIN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHAIN_CD.RegistCheckMethod")));
            this.SHAIN_CD.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SHAIN_CD.SetFormField = "SHAIN_CD,SHAIN_NAME";
            this.SHAIN_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SHAIN_CD.ToolTipText = "社員を指定してください（スペースキーを押下して、検索画面を表示します）";
            this.SHAIN_CD.Width = 70;
            this.SHAIN_CD.ZeroPaddengFlag = true;
            // 
            // SHAIN_NAME
            // 
            this.SHAIN_NAME.DBFieldsName = "SHAIN_NAME";
            this.SHAIN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.SHAIN_NAME.DefaultCellStyle = dataGridViewCellStyle4;
            this.SHAIN_NAME.FillWeight = 60F;
            this.SHAIN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHAIN_NAME.FocusOutCheckMethod")));
            this.SHAIN_NAME.HeaderText = "社員名";
            this.SHAIN_NAME.ItemDefinedTypes = "varchar";
            this.SHAIN_NAME.MaxInputLength = 8;
            this.SHAIN_NAME.Name = "SHAIN_NAME";
            this.SHAIN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHAIN_NAME.PopupSearchSendParams")));
            this.SHAIN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHAIN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHAIN_NAME.popupWindowSetting")));
            this.SHAIN_NAME.ReadOnly = true;
            this.SHAIN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHAIN_NAME.RegistCheckMethod")));
            this.SHAIN_NAME.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SHAIN_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SHAIN_NAME.Width = 170;
            // 
            // UPDATE_USER
            // 
            this.UPDATE_USER.DBFieldsName = "UPDATE_USER";
            this.UPDATE_USER.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.UPDATE_USER.DefaultCellStyle = dataGridViewCellStyle5;
            this.UPDATE_USER.FillWeight = 60F;
            this.UPDATE_USER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_USER.FocusOutCheckMethod")));
            this.UPDATE_USER.FormatSetting = "";
            this.UPDATE_USER.HeaderText = "更新者";
            this.UPDATE_USER.ItemDefinedTypes = "varchar";
            this.UPDATE_USER.MaxInputLength = 16;
            this.UPDATE_USER.Name = "UPDATE_USER";
            this.UPDATE_USER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPDATE_USER.PopupSearchSendParams")));
            this.UPDATE_USER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPDATE_USER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPDATE_USER.popupWindowSetting")));
            this.UPDATE_USER.ReadOnly = true;
            this.UPDATE_USER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_USER.RegistCheckMethod")));
            this.UPDATE_USER.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.UPDATE_USER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UPDATE_USER.ToolTipText = "最終更新者が表示されます";
            this.UPDATE_USER.Width = 125;
            // 
            // UPDATE_DATE
            // 
            this.UPDATE_DATE.DBFieldsName = "UPDATE_DATE";
            this.UPDATE_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle6.Format = "G";
            dataGridViewCellStyle6.NullValue = null;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.UPDATE_DATE.DefaultCellStyle = dataGridViewCellStyle6;
            this.UPDATE_DATE.FillWeight = 60F;
            this.UPDATE_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_DATE.FocusOutCheckMethod")));
            this.UPDATE_DATE.HeaderText = "更新日";
            this.UPDATE_DATE.ItemDefinedTypes = "datetime";
            this.UPDATE_DATE.Name = "UPDATE_DATE";
            this.UPDATE_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPDATE_DATE.PopupSearchSendParams")));
            this.UPDATE_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPDATE_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPDATE_DATE.popupWindowSetting")));
            this.UPDATE_DATE.ReadOnly = true;
            this.UPDATE_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_DATE.RegistCheckMethod")));
            this.UPDATE_DATE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.UPDATE_DATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UPDATE_DATE.ToolTipText = "最終更新日が表示されます";
            this.UPDATE_DATE.Width = 150;
            // 
            // UPDATE_PC
            // 
            this.UPDATE_PC.DBFieldsName = "UPDATE_PC";
            this.UPDATE_PC.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.UPDATE_PC.DefaultCellStyle = dataGridViewCellStyle7;
            this.UPDATE_PC.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_PC.FocusOutCheckMethod")));
            this.UPDATE_PC.HeaderText = "更新PC";
            this.UPDATE_PC.ItemDefinedTypes = "varchar";
            this.UPDATE_PC.MaxInputLength = 50;
            this.UPDATE_PC.Name = "UPDATE_PC";
            this.UPDATE_PC.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPDATE_PC.PopupSearchSendParams")));
            this.UPDATE_PC.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPDATE_PC.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPDATE_PC.popupWindowSetting")));
            this.UPDATE_PC.ReadOnly = true;
            this.UPDATE_PC.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_PC.RegistCheckMethod")));
            this.UPDATE_PC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UPDATE_PC.Visible = false;
            // 
            // CREATE_USER
            // 
            this.CREATE_USER.DBFieldsName = "CREATE_USER";
            this.CREATE_USER.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CREATE_USER.DefaultCellStyle = dataGridViewCellStyle8;
            this.CREATE_USER.FillWeight = 60F;
            this.CREATE_USER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_USER.FocusOutCheckMethod")));
            this.CREATE_USER.HeaderText = "作成者";
            this.CREATE_USER.ItemDefinedTypes = "varchar";
            this.CREATE_USER.MaxInputLength = 16;
            this.CREATE_USER.Name = "CREATE_USER";
            this.CREATE_USER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CREATE_USER.PopupSearchSendParams")));
            this.CREATE_USER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_USER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_USER.popupWindowSetting")));
            this.CREATE_USER.ReadOnly = true;
            this.CREATE_USER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_USER.RegistCheckMethod")));
            this.CREATE_USER.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CREATE_USER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CREATE_USER.ToolTipText = "初回作成者が表示されます";
            this.CREATE_USER.Width = 125;
            // 
            // CREATE_DATE
            // 
            this.CREATE_DATE.DBFieldsName = "CREATE_DATE";
            this.CREATE_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle9.Format = "G";
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CREATE_DATE.DefaultCellStyle = dataGridViewCellStyle9;
            this.CREATE_DATE.FillWeight = 60F;
            this.CREATE_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_DATE.FocusOutCheckMethod")));
            this.CREATE_DATE.HeaderText = "作成日";
            this.CREATE_DATE.ItemDefinedTypes = "datetime";
            this.CREATE_DATE.Name = "CREATE_DATE";
            this.CREATE_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CREATE_DATE.PopupSearchSendParams")));
            this.CREATE_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_DATE.popupWindowSetting")));
            this.CREATE_DATE.ReadOnly = true;
            this.CREATE_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_DATE.RegistCheckMethod")));
            this.CREATE_DATE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CREATE_DATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CREATE_DATE.ToolTipText = "初回作成日が表示されます";
            this.CREATE_DATE.Width = 150;
            // 
            // CREATE_PC
            // 
            this.CREATE_PC.DBFieldsName = "CREATE_PC";
            this.CREATE_PC.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            this.CREATE_PC.DefaultCellStyle = dataGridViewCellStyle10;
            this.CREATE_PC.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_PC.FocusOutCheckMethod")));
            this.CREATE_PC.HeaderText = "作成PC";
            this.CREATE_PC.ItemDefinedTypes = "varchar";
            this.CREATE_PC.MaxInputLength = 50;
            this.CREATE_PC.Name = "CREATE_PC";
            this.CREATE_PC.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CREATE_PC.PopupSearchSendParams")));
            this.CREATE_PC.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_PC.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_PC.popupWindowSetting")));
            this.CREATE_PC.ReadOnly = true;
            this.CREATE_PC.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_PC.RegistCheckMethod")));
            this.CREATE_PC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CREATE_PC.Visible = false;
            // 
            // DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO
            // 
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO.DefaultCellStyle = dataGridViewCellStyle11;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO.FocusOutCheckMethod")));
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO.HeaderText = "DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO.Name = "DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO.PopupSearchSendParams")));
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO.popupWindowSetting")));
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO.RegistCheckMethod")));
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO.ViewSearchItem = false;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO.Visible = false;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1034, 423);
            this.Controls.Add(this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD);
            this.Controls.Add(this.popBtn_Bpzkhm);
            this.Controls.Add(this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Ichiran);
            this.Controls.Add(this.CONDITION_VALUE);
            this.Controls.Add(this.CONDITION_ITEM);
            this.Controls.Add(this.label16);
            this.Name = "UIForm";
            this.Text = "DenshiKeiyakuShanaiKeiroForm";
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomTextBox CONDITION_VALUE;
        internal r_framework.CustomControl.CustomTextBox CONDITION_ITEM;
        internal System.Windows.Forms.Label label16;
        public r_framework.CustomControl.CustomDataGridView Ichiran;
        internal System.Windows.Forms.Label label2;
        public r_framework.CustomControl.CustomPopupOpenButton popBtn_Bpzkhm;
        public r_framework.CustomControl.CustomTextBox DENSHI_KEIYAKU_SHANAI_KEIRO_NAME;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn dgvCustomCheckBoxColumn1;
        public r_framework.CustomControl.CustomAlphaNumTextBox DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn chb_delete;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn SHAIN_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn SHAIN_NAME;
        private r_framework.CustomControl.DgvCustomTextBoxColumn UPDATE_USER;
        private r_framework.CustomControl.DgvCustomTextBoxColumn UPDATE_DATE;
        private r_framework.CustomControl.DgvCustomTextBoxColumn UPDATE_PC;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CREATE_USER;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CREATE_DATE;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CREATE_PC;
        private r_framework.CustomControl.DgvCustomTextBoxColumn DENSHI_KEIYAKU_SHANAI_KEIRO_ROW_NO;
    }
}