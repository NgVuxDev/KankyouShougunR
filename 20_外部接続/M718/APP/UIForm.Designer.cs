namespace Shougun.Core.ExternalConnection.DenshiKeiyakuShinseiKeiroName.APP
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle44 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle45 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle40 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle41 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle42 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle43 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED = new r_framework.CustomControl.CustomCheckBox();
            this.CONDITION_VALUE = new r_framework.CustomControl.CustomTextBox();
            this.CONDITION_ITEM = new r_framework.CustomControl.CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.Ichiran = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.chb_delete = new r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn();
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.UPDATE_USER = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.UPDATE_DATE = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CREATE_USER = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CREATE_DATE = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CREATE_PC = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.DELETE_FLG = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.UPDATE_PC = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.TIME_STAMP = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).BeginInit();
            this.SuspendLayout();
            // 
            // ICHIRAN_HYOUJI_JOUKEN_DELETED
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.DefaultBackColor = System.Drawing.Color.Empty;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ICHIRAN_HYOUJI_JOUKEN_DELETED.FocusOutCheckMethod")));
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Location = new System.Drawing.Point(640, 15);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Name = "ICHIRAN_HYOUJI_JOUKEN_DELETED";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.PopupAfterExecute = null;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.PopupBeforeExecute = null;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ICHIRAN_HYOUJI_JOUKEN_DELETED.PopupSearchSendParams")));
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ICHIRAN_HYOUJI_JOUKEN_DELETED.popupWindowSetting")));
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ICHIRAN_HYOUJI_JOUKEN_DELETED.RegistCheckMethod")));
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Size = new System.Drawing.Size(180, 17);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.TabIndex = 9;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Tag = "削除されたデータを対象とする場合チェックを付けてください";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Text = "削除済も含めて全て表示";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.UseVisualStyleBackColor = false;
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
            this.CONDITION_VALUE.Location = new System.Drawing.Point(233, 13);
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
            this.CONDITION_VALUE.TabIndex = 7;
            this.CONDITION_VALUE.Tag = "検索する文字を入力してください";
            this.CONDITION_VALUE.Enter += new System.EventHandler(this.CONDITION_VALUE_Enter);
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
            this.CONDITION_ITEM.Location = new System.Drawing.Point(114, 13);
            this.CONDITION_ITEM.MaxLength = 20;
            this.CONDITION_ITEM.Name = "CONDITION_ITEM";
            this.CONDITION_ITEM.PopupAfterExecute = null;
            this.CONDITION_ITEM.PopupAfterExecuteMethod = "PopupAfter";
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
            this.CONDITION_ITEM.TabIndex = 6;
            this.CONDITION_ITEM.Tag = "検索条件項目を指定してください（スペースキー押下にて、検索画面を表示します）";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(528, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "表示条件";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label16.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(0, 13);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(110, 20);
            this.label16.TabIndex = 5;
            this.label16.Text = "検索条件";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Ichiran
            // 
            this.Ichiran.AllowUserToDeleteRows = false;
            this.Ichiran.AllowUserToResizeColumns = false;
            this.Ichiran.AllowUserToResizeRows = false;
            this.Ichiran.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle31.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle31.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle31.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle31.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle31.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Ichiran.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle31;
            this.Ichiran.ColumnHeadersHeight = 21;
            this.Ichiran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.Ichiran.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chb_delete,
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD,
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME,
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA,
            this.UPDATE_USER,
            this.UPDATE_DATE,
            this.CREATE_USER,
            this.CREATE_DATE,
            this.CREATE_PC,
            this.DELETE_FLG,
            this.UPDATE_PC,
            this.TIME_STAMP});
            dataGridViewCellStyle44.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle44.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle44.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle44.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle44.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle44.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle44.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Ichiran.DefaultCellStyle = dataGridViewCellStyle44;
            this.Ichiran.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.Ichiran.EnableHeadersVisualStyles = false;
            this.Ichiran.GridColor = System.Drawing.Color.White;
            this.Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Ichiran.IsReload = false;
            this.Ichiran.LinkedDataPanelName = null;
            this.Ichiran.Location = new System.Drawing.Point(0, 36);
            this.Ichiran.MultiSelect = false;
            this.Ichiran.Name = "Ichiran";
            dataGridViewCellStyle45.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle45.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle45.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle45.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle45.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle45.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle45.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Ichiran.RowHeadersDefaultCellStyle = dataGridViewCellStyle45;
            this.Ichiran.RowHeadersVisible = false;
            this.Ichiran.RowTemplate.Height = 21;
            this.Ichiran.ShowCellToolTips = false;
            this.Ichiran.Size = new System.Drawing.Size(978, 442);
            this.Ichiran.TabIndex = 10;
            this.Ichiran.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Ichiran_CellEnter);
            this.Ichiran.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.Ichiran_CellParsing);
            this.Ichiran.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.Ichiran_CellValidating);
            this.Ichiran.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.Ichiran_ColumnWidthChanged);
            this.Ichiran.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.Ichiran_DefaultValuesNeeded);
            // 
            // chb_delete
            // 
            this.chb_delete.DataPropertyName = "DELETE_FLG";
            this.chb_delete.DBFieldsName = "DELETE_FLG";
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle32.NullValue = false;
            dataGridViewCellStyle32.SelectionForeColor = System.Drawing.Color.Black;
            this.chb_delete.DefaultCellStyle = dataGridViewCellStyle32;
            this.chb_delete.FillWeight = 43F;
            this.chb_delete.FocusOutCheckMethod = null;
            this.chb_delete.HeaderText = "削除";
            this.chb_delete.ItemDefinedTypes = "bit";
            this.chb_delete.Name = "chb_delete";
            this.chb_delete.RegistCheckMethod = null;
            this.chb_delete.ToolTipText = "削除する場合、チェックしてください";
            this.chb_delete.TrueValue = "true";
            this.chb_delete.ViewSearchItem = false;
            this.chb_delete.Width = 43;
            // 
            // DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD
            // 
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.DataPropertyName = "DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.DBFieldsName = "DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle33.Format = "N0";
            dataGridViewCellStyle33.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle33.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.DefaultCellStyle = dataGridViewCellStyle33;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.DisplayItemName = "社内経路名CD";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.FillWeight = 60F;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.FocusOutCheckMethod")));
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.HeaderText = "社内経路名CD※";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.ItemDefinedTypes = "smallint";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Name = "DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.PopupSearchSendParams")));
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.RangeSetting = rangeSettingDto3;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.RegistCheckMethod")));
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.ShortItemName = "社内経路名CD";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.ToolTipText = "半角2桁以内で入力してください";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD.Width = 140;
            // 
            // DENSHI_KEIYAKU_SHANAI_KEIRO_NAME
            // 
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.CopyAutoSetControl = "";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.DataPropertyName = "DENSHI_KEIYAKU_SHANAI_KEIRO_NAME";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.DBFieldsName = "DENSHI_KEIYAKU_SHANAI_KEIRO_NAME";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle34.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle34.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.DefaultCellStyle = dataGridViewCellStyle34;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.DisplayItemName = "社内経路名";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.FillWeight = 60F;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.FocusOutCheckMethod")));
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.FuriganaAutoSetControl = "DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.HeaderText = "社内経路名※";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.ItemDefinedTypes = "varchar";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.MaxInputLength = 20;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.Name = "DENSHI_KEIYAKU_SHANAI_KEIRO_NAME";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.PopupSearchSendParams")));
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.popupWindowSetting")));
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.RegistCheckMethod")));
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.ToolTipText = "全角10文字以内で入力してください";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME.Width = 200;
            // 
            // DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA
            // 
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA.DataPropertyName = "DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA.DBFieldsName = "DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle35.SelectionForeColor = System.Drawing.Color.Black;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA.DefaultCellStyle = dataGridViewCellStyle35;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA.DisplayItemName = "フリガナ";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA.FocusOutCheckMethod")));
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA.HeaderText = "フリガナ※";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA.ItemDefinedTypes = "varchar";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA.MaxInputLength = 20;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA.Name = "DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA.PopupSearchSendParams")));
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA.popupWindowSetting")));
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA.RegistCheckMethod")));
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA.ToolTipText = "全角10文字以内で入力してください";
            this.DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA.Width = 200;
            // 
            // UPDATE_USER
            // 
            this.UPDATE_USER.DataPropertyName = "UPDATE_USER";
            this.UPDATE_USER.DBFieldsName = "UPDATE_USER";
            this.UPDATE_USER.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle36.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle36.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.UPDATE_USER.DefaultCellStyle = dataGridViewCellStyle36;
            this.UPDATE_USER.DisplayItemName = "更新者";
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
            this.UPDATE_DATE.DataPropertyName = "UPDATE_DATE";
            this.UPDATE_DATE.DBFieldsName = "UPDATE_DATE";
            this.UPDATE_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle37.Format = "G";
            dataGridViewCellStyle37.NullValue = null;
            dataGridViewCellStyle37.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle37.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.UPDATE_DATE.DefaultCellStyle = dataGridViewCellStyle37;
            this.UPDATE_DATE.DisplayItemName = "更新日";
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
            // CREATE_USER
            // 
            this.CREATE_USER.DataPropertyName = "CREATE_USER";
            this.CREATE_USER.DBFieldsName = "CREATE_USER";
            this.CREATE_USER.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle38.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle38.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CREATE_USER.DefaultCellStyle = dataGridViewCellStyle38;
            this.CREATE_USER.DisplayItemName = "作成者";
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
            this.CREATE_DATE.DataPropertyName = "CREATE_DATE";
            this.CREATE_DATE.DBFieldsName = "CREATE_DATE";
            this.CREATE_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle39.Format = "G";
            dataGridViewCellStyle39.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle39.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CREATE_DATE.DefaultCellStyle = dataGridViewCellStyle39;
            this.CREATE_DATE.DisplayItemName = "作成日";
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
            this.CREATE_PC.DataPropertyName = "CREATE_PC";
            this.CREATE_PC.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle40.SelectionForeColor = System.Drawing.Color.Black;
            this.CREATE_PC.DefaultCellStyle = dataGridViewCellStyle40;
            this.CREATE_PC.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_PC.FocusOutCheckMethod")));
            this.CREATE_PC.HeaderText = "CREATE_PC";
            this.CREATE_PC.Name = "CREATE_PC";
            this.CREATE_PC.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CREATE_PC.PopupSearchSendParams")));
            this.CREATE_PC.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_PC.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_PC.popupWindowSetting")));
            this.CREATE_PC.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_PC.RegistCheckMethod")));
            this.CREATE_PC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CREATE_PC.ViewSearchItem = false;
            this.CREATE_PC.Visible = false;
            // 
            // DELETE_FLG
            // 
            this.DELETE_FLG.DataPropertyName = "DELETE_FLG";
            this.DELETE_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle41.SelectionForeColor = System.Drawing.Color.Black;
            this.DELETE_FLG.DefaultCellStyle = dataGridViewCellStyle41;
            this.DELETE_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DELETE_FLG.FocusOutCheckMethod")));
            this.DELETE_FLG.HeaderText = "DELETE_FLG";
            this.DELETE_FLG.Name = "DELETE_FLG";
            this.DELETE_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DELETE_FLG.PopupSearchSendParams")));
            this.DELETE_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DELETE_FLG.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DELETE_FLG.popupWindowSetting")));
            this.DELETE_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DELETE_FLG.RegistCheckMethod")));
            this.DELETE_FLG.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DELETE_FLG.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DELETE_FLG.ViewSearchItem = false;
            this.DELETE_FLG.Visible = false;
            // 
            // UPDATE_PC
            // 
            this.UPDATE_PC.DataPropertyName = "UPDATE_PC";
            this.UPDATE_PC.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle42.SelectionForeColor = System.Drawing.Color.Black;
            this.UPDATE_PC.DefaultCellStyle = dataGridViewCellStyle42;
            this.UPDATE_PC.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_PC.FocusOutCheckMethod")));
            this.UPDATE_PC.HeaderText = "UPDATE_PC";
            this.UPDATE_PC.Name = "UPDATE_PC";
            this.UPDATE_PC.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPDATE_PC.PopupSearchSendParams")));
            this.UPDATE_PC.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPDATE_PC.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPDATE_PC.popupWindowSetting")));
            this.UPDATE_PC.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_PC.RegistCheckMethod")));
            this.UPDATE_PC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UPDATE_PC.ViewSearchItem = false;
            this.UPDATE_PC.Visible = false;
            // 
            // TIME_STAMP
            // 
            this.TIME_STAMP.DataPropertyName = "TIME_STAMP";
            this.TIME_STAMP.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle43.SelectionForeColor = System.Drawing.Color.Black;
            this.TIME_STAMP.DefaultCellStyle = dataGridViewCellStyle43;
            this.TIME_STAMP.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TIME_STAMP.FocusOutCheckMethod")));
            this.TIME_STAMP.HeaderText = "TIME_STAMP";
            this.TIME_STAMP.Name = "TIME_STAMP";
            this.TIME_STAMP.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TIME_STAMP.PopupSearchSendParams")));
            this.TIME_STAMP.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TIME_STAMP.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TIME_STAMP.popupWindowSetting")));
            this.TIME_STAMP.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TIME_STAMP.RegistCheckMethod")));
            this.TIME_STAMP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TIME_STAMP.ViewSearchItem = false;
            this.TIME_STAMP.Visible = false;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1192, 593);
            this.Controls.Add(this.Ichiran);
            this.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_DELETED);
            this.Controls.Add(this.CONDITION_VALUE);
            this.Controls.Add(this.CONDITION_ITEM);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label16);
            this.Name = "UIForm";
            this.Text = "UIForm";
            this.Shown += new System.EventHandler(this.UIForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomCheckBox ICHIRAN_HYOUJI_JOUKEN_DELETED;
        internal r_framework.CustomControl.CustomTextBox CONDITION_VALUE;
        internal r_framework.CustomControl.CustomTextBox CONDITION_ITEM;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label16;
        public r_framework.CustomControl.CustomDataGridView Ichiran;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn chb_delete;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn DENSHI_KEIYAKU_SHANAI_KEIRO_NAME;
        private r_framework.CustomControl.DgvCustomTextBoxColumn DENSHI_KEIYAKU_SHANAI_KEIRO_NAME_FURIGANA;
        private r_framework.CustomControl.DgvCustomTextBoxColumn UPDATE_USER;
        private r_framework.CustomControl.DgvCustomTextBoxColumn UPDATE_DATE;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CREATE_USER;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CREATE_DATE;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CREATE_PC;
        private r_framework.CustomControl.DgvCustomTextBoxColumn DELETE_FLG;
        private r_framework.CustomControl.DgvCustomTextBoxColumn UPDATE_PC;
        private r_framework.CustomControl.DgvCustomTextBoxColumn TIME_STAMP;
    }
}