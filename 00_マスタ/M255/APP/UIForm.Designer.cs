namespace Shougun.Core.Master.ManiFestTeHaiHoshu.APP
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED = new r_framework.CustomControl.CustomCheckBox();
            this.CONDITION_VALUE = new r_framework.CustomControl.CustomTextBox();
            this.CONDITION_ITEM = new r_framework.CustomControl.CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.Ichiran = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.chb_delete = new r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn();
            this.MANIFEST_TEHAI_CD = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.MANIFEST_TEHAI_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.MANIFEST_TEHAI_NAME_RYAKU = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.MANIFEST_TEHAI_BIKOU = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.UPDATE_USER = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.UPDATE_DATE = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CREATE_USER = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CREATE_DATE = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.DELETE_FLG = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CREATE_PC = new r_framework.CustomControl.DgvCustomTextBoxColumn();
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
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Location = new System.Drawing.Point(641, 12);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Name = "ICHIRAN_HYOUJI_JOUKEN_DELETED";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.PopupAfterExecute = null;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.PopupBeforeExecute = null;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ICHIRAN_HYOUJI_JOUKEN_DELETED.PopupSearchSendParams")));
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ICHIRAN_HYOUJI_JOUKEN_DELETED.popupWindowSetting")));
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ICHIRAN_HYOUJI_JOUKEN_DELETED.RegistCheckMethod")));
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Size = new System.Drawing.Size(145, 16);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.TabIndex = 419;
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
            this.CONDITION_VALUE.Location = new System.Drawing.Point(248, 10);
            this.CONDITION_VALUE.MaxLength = 20;
            this.CONDITION_VALUE.Name = "CONDITION_VALUE";
            this.CONDITION_VALUE.PopupAfterExecute = null;
            this.CONDITION_VALUE.PopupBeforeExecute = null;
            this.CONDITION_VALUE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONDITION_VALUE.PopupSearchSendParams")));
            this.CONDITION_VALUE.PopupSetFormField = "";
            this.CONDITION_VALUE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION_VALUE.PopupWindowName = "";
            this.CONDITION_VALUE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONDITION_VALUE.popupWindowSetting")));
            this.CONDITION_VALUE.prevText = null;
            this.CONDITION_VALUE.PrevText = null;
            this.CONDITION_VALUE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_VALUE.RegistCheckMethod")));
            this.CONDITION_VALUE.SetFormField = "";
            this.CONDITION_VALUE.ShortItemName = "検索条件";
            this.CONDITION_VALUE.Size = new System.Drawing.Size(290, 20);
            this.CONDITION_VALUE.TabIndex = 417;
            this.CONDITION_VALUE.Tag = "検索する文字を入力してください";
            this.CONDITION_VALUE.Enter += new System.EventHandler(this.CONDITION_VALUE_Enter);
            this.CONDITION_VALUE.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CONDITION_VALUE_KeyPress);
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
            this.CONDITION_ITEM.Location = new System.Drawing.Point(99, 10);
            this.CONDITION_ITEM.MaxLength = 10;
            this.CONDITION_ITEM.Name = "CONDITION_ITEM";
            this.CONDITION_ITEM.PopupAfterExecute = null;
            this.CONDITION_ITEM.PopupAfterExecuteMethod = "clearConditionValue";
            this.CONDITION_ITEM.PopupBeforeExecute = null;
            this.CONDITION_ITEM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONDITION_ITEM.PopupSearchSendParams")));
            this.CONDITION_ITEM.PopupSendParams = new string[] {
        "Ichiran"};
            this.CONDITION_ITEM.PopupSetFormField = "CONDITION_ITEM,CONDITION_VALUE";
            this.CONDITION_ITEM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION_ITEM.PopupWindowName = "マスタ検索項目ポップアップ";
            this.CONDITION_ITEM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONDITION_ITEM.popupWindowSetting")));
            this.CONDITION_ITEM.prevText = null;
            this.CONDITION_ITEM.PrevText = null;
            this.CONDITION_ITEM.ReadOnly = true;
            this.CONDITION_ITEM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_ITEM.RegistCheckMethod")));
            this.CONDITION_ITEM.SetFormField = "CONDITION_ITEM,CONDITION_VALUE";
            this.CONDITION_ITEM.ShortItemName = "検索条件";
            this.CONDITION_ITEM.Size = new System.Drawing.Size(150, 20);
            this.CONDITION_ITEM.TabIndex = 416;
            this.CONDITION_ITEM.Tag = "検索条件項目を指定してください（スペースキー押下にて、検索画面を表示します）";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(544, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 20);
            this.label1.TabIndex = 415;
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
            this.label16.Location = new System.Drawing.Point(0, 10);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(93, 20);
            this.label16.TabIndex = 414;
            this.label16.Text = "検索条件";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Ichiran
            // 
            this.Ichiran.AllowUserToDeleteRows = false;
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
            this.MANIFEST_TEHAI_CD,
            this.MANIFEST_TEHAI_NAME,
            this.MANIFEST_TEHAI_NAME_RYAKU,
            this.MANIFEST_TEHAI_BIKOU,
            this.UPDATE_USER,
            this.UPDATE_DATE,
            this.CREATE_USER,
            this.CREATE_DATE,
            this.DELETE_FLG,
            this.CREATE_PC,
            this.UPDATE_PC,
            this.TIME_STAMP});
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Ichiran.DefaultCellStyle = dataGridViewCellStyle15;
            this.Ichiran.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.Ichiran.EnableHeadersVisualStyles = false;
            this.Ichiran.GridColor = System.Drawing.Color.White;
            this.Ichiran.IsReload = false;
            this.Ichiran.LinkedDataPanelName = null;
            this.Ichiran.Location = new System.Drawing.Point(0, 37);
            this.Ichiran.MultiSelect = false;
            this.Ichiran.Name = "Ichiran";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle16.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Ichiran.RowHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.Ichiran.RowHeadersVisible = false;
            this.Ichiran.RowTemplate.Height = 21;
            this.Ichiran.ShowCellToolTips = false;
            this.Ichiran.Size = new System.Drawing.Size(990, 410);
            this.Ichiran.TabIndex = 1421;
            this.Ichiran.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Ichiran_CellEnter);
            this.Ichiran.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.Ichiran_CellParsing);
            this.Ichiran.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.Ichiran_CellValidating);
            this.Ichiran.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.Ichiran_DefaultValuesNeeded);
            this.Ichiran.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.Ichiran_EditingControlShowing);
            // 
            // chb_delete
            // 
            this.chb_delete.DataPropertyName = "DELETE_FLG";
            this.chb_delete.DBFieldsName = "DELETE_FLG";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = false;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Cyan;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.chb_delete.DefaultCellStyle = dataGridViewCellStyle2;
            this.chb_delete.DisplayItemName = "DELETE_FLG";
            this.chb_delete.FocusOutCheckMethod = null;
            this.chb_delete.HeaderText = "削除";
            this.chb_delete.ItemDefinedTypes = "bit";
            this.chb_delete.Name = "chb_delete";
            this.chb_delete.RegistCheckMethod = null;
            this.chb_delete.ToolTipText = "削除する場合、チェックしてください";
            this.chb_delete.ViewSearchItem = false;
            this.chb_delete.Width = 43;
            // 
            // MANIFEST_TEHAI_CD
            // 
            this.MANIFEST_TEHAI_CD.DataPropertyName = "MANIFEST_TEHAI_CD";
            this.MANIFEST_TEHAI_CD.DBFieldsName = "MANIFEST_TEHAI_CD";
            this.MANIFEST_TEHAI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Cyan;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.MANIFEST_TEHAI_CD.DefaultCellStyle = dataGridViewCellStyle3;
            this.MANIFEST_TEHAI_CD.DisplayItemName = "マニフェスト手配CD";
            this.MANIFEST_TEHAI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MANIFEST_TEHAI_CD.FocusOutCheckMethod")));
            this.MANIFEST_TEHAI_CD.HeaderText = "マニフェスト手配CD※";
            this.MANIFEST_TEHAI_CD.ItemDefinedTypes = "smallint";
            this.MANIFEST_TEHAI_CD.Name = "MANIFEST_TEHAI_CD";
            this.MANIFEST_TEHAI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MANIFEST_TEHAI_CD.PopupSearchSendParams")));
            this.MANIFEST_TEHAI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MANIFEST_TEHAI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("MANIFEST_TEHAI_CD.popupWindowSetting")));
            this.MANIFEST_TEHAI_CD.PrevText = null;
            rangeSettingDto1.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.MANIFEST_TEHAI_CD.RangeSetting = rangeSettingDto1;
            this.MANIFEST_TEHAI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MANIFEST_TEHAI_CD.RegistCheckMethod")));
            this.MANIFEST_TEHAI_CD.ShortItemName = "マニフェスト手配CD";
            this.MANIFEST_TEHAI_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MANIFEST_TEHAI_CD.ToolTipText = "半角2桁以内で入力してください";
            this.MANIFEST_TEHAI_CD.Width = 160;
            // 
            // MANIFEST_TEHAI_NAME
            // 
            this.MANIFEST_TEHAI_NAME.CharactersNumber = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.MANIFEST_TEHAI_NAME.CopyAutoSetControl = "MANIFEST_TEHAI_NAME_RYAKU";
            this.MANIFEST_TEHAI_NAME.DataPropertyName = "MANIFEST_TEHAI_NAME";
            this.MANIFEST_TEHAI_NAME.DBFieldsName = "MANIFEST_TEHAI_NAME";
            this.MANIFEST_TEHAI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.MANIFEST_TEHAI_NAME.DefaultCellStyle = dataGridViewCellStyle4;
            this.MANIFEST_TEHAI_NAME.DisplayItemName = "マニフェスト手配名";
            this.MANIFEST_TEHAI_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MANIFEST_TEHAI_NAME.FocusOutCheckMethod")));
            this.MANIFEST_TEHAI_NAME.HeaderText = "マニフェスト手配名※";
            this.MANIFEST_TEHAI_NAME.ItemDefinedTypes = "varchar";
            this.MANIFEST_TEHAI_NAME.MaxInputLength = 4;
            this.MANIFEST_TEHAI_NAME.Name = "MANIFEST_TEHAI_NAME";
            this.MANIFEST_TEHAI_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MANIFEST_TEHAI_NAME.PopupSearchSendParams")));
            this.MANIFEST_TEHAI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MANIFEST_TEHAI_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("MANIFEST_TEHAI_NAME.popupWindowSetting")));
            this.MANIFEST_TEHAI_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MANIFEST_TEHAI_NAME.RegistCheckMethod")));
            this.MANIFEST_TEHAI_NAME.ShortItemName = "マニフェスト手配名";
            this.MANIFEST_TEHAI_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MANIFEST_TEHAI_NAME.ToolTipText = "全角４桁以内で入力してください";
            this.MANIFEST_TEHAI_NAME.Width = 160;
            // 
            // MANIFEST_TEHAI_NAME_RYAKU
            // 
            this.MANIFEST_TEHAI_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.MANIFEST_TEHAI_NAME_RYAKU.DataPropertyName = "MANIFEST_TEHAI_NAME_RYAKU";
            this.MANIFEST_TEHAI_NAME_RYAKU.DBFieldsName = "MANIFEST_TEHAI_NAME_RYAKU";
            this.MANIFEST_TEHAI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.MANIFEST_TEHAI_NAME_RYAKU.DefaultCellStyle = dataGridViewCellStyle5;
            this.MANIFEST_TEHAI_NAME_RYAKU.DisplayItemName = "略称";
            this.MANIFEST_TEHAI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MANIFEST_TEHAI_NAME_RYAKU.FocusOutCheckMethod")));
            this.MANIFEST_TEHAI_NAME_RYAKU.HeaderText = "略称※";
            this.MANIFEST_TEHAI_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.MANIFEST_TEHAI_NAME_RYAKU.MaxInputLength = 4;
            this.MANIFEST_TEHAI_NAME_RYAKU.Name = "MANIFEST_TEHAI_NAME_RYAKU";
            this.MANIFEST_TEHAI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MANIFEST_TEHAI_NAME_RYAKU.PopupSearchSendParams")));
            this.MANIFEST_TEHAI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MANIFEST_TEHAI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("MANIFEST_TEHAI_NAME_RYAKU.popupWindowSetting")));
            this.MANIFEST_TEHAI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MANIFEST_TEHAI_NAME_RYAKU.RegistCheckMethod")));
            this.MANIFEST_TEHAI_NAME_RYAKU.ShortItemName = "略称";
            this.MANIFEST_TEHAI_NAME_RYAKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MANIFEST_TEHAI_NAME_RYAKU.ToolTipText = "全角４桁以内で入力してください";
            // 
            // MANIFEST_TEHAI_BIKOU
            // 
            this.MANIFEST_TEHAI_BIKOU.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.MANIFEST_TEHAI_BIKOU.DataPropertyName = "MANIFEST_TEHAI_BIKOU";
            this.MANIFEST_TEHAI_BIKOU.DBFieldsName = "MANIFEST_TEHAI_BIKOU";
            this.MANIFEST_TEHAI_BIKOU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.MANIFEST_TEHAI_BIKOU.DefaultCellStyle = dataGridViewCellStyle6;
            this.MANIFEST_TEHAI_BIKOU.DisplayItemName = "備考";
            this.MANIFEST_TEHAI_BIKOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MANIFEST_TEHAI_BIKOU.FocusOutCheckMethod")));
            this.MANIFEST_TEHAI_BIKOU.HeaderText = "備考";
            this.MANIFEST_TEHAI_BIKOU.ItemDefinedTypes = "varchar";
            this.MANIFEST_TEHAI_BIKOU.MaxInputLength = 10;
            this.MANIFEST_TEHAI_BIKOU.Name = "MANIFEST_TEHAI_BIKOU";
            this.MANIFEST_TEHAI_BIKOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MANIFEST_TEHAI_BIKOU.PopupSearchSendParams")));
            this.MANIFEST_TEHAI_BIKOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MANIFEST_TEHAI_BIKOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("MANIFEST_TEHAI_BIKOU.popupWindowSetting")));
            this.MANIFEST_TEHAI_BIKOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MANIFEST_TEHAI_BIKOU.RegistCheckMethod")));
            this.MANIFEST_TEHAI_BIKOU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MANIFEST_TEHAI_BIKOU.ToolTipText = "全角１０桁以内で入力してください";
            this.MANIFEST_TEHAI_BIKOU.Width = 180;
            // 
            // UPDATE_USER
            // 
            this.UPDATE_USER.DataPropertyName = "UPDATE_USER";
            this.UPDATE_USER.DBFieldsName = "UPDATE_USER";
            this.UPDATE_USER.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.UPDATE_USER.DefaultCellStyle = dataGridViewCellStyle7;
            this.UPDATE_USER.DisplayItemName = "更新者";
            this.UPDATE_USER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_USER.FocusOutCheckMethod")));
            this.UPDATE_USER.HeaderText = "更新者";
            this.UPDATE_USER.ItemDefinedTypes = "varchar";
            this.UPDATE_USER.MaxInputLength = 8;
            this.UPDATE_USER.Name = "UPDATE_USER";
            this.UPDATE_USER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPDATE_USER.PopupSearchSendParams")));
            this.UPDATE_USER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPDATE_USER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPDATE_USER.popupWindowSetting")));
            this.UPDATE_USER.ReadOnly = true;
            this.UPDATE_USER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_USER.RegistCheckMethod")));
            this.UPDATE_USER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // UPDATE_DATE
            // 
            this.UPDATE_DATE.DataPropertyName = "UPDATE_DATE";
            this.UPDATE_DATE.DBFieldsName = "UPDATE_DATE";
            this.UPDATE_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle8.Format = "G";
            dataGridViewCellStyle8.NullValue = null;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.UPDATE_DATE.DefaultCellStyle = dataGridViewCellStyle8;
            this.UPDATE_DATE.DisplayItemName = "更新日";
            this.UPDATE_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_DATE.FocusOutCheckMethod")));
            this.UPDATE_DATE.HeaderText = "更新日";
            this.UPDATE_DATE.ItemDefinedTypes = "datetime";
            this.UPDATE_DATE.MaxInputLength = 18;
            this.UPDATE_DATE.Name = "UPDATE_DATE";
            this.UPDATE_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPDATE_DATE.PopupSearchSendParams")));
            this.UPDATE_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPDATE_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPDATE_DATE.popupWindowSetting")));
            this.UPDATE_DATE.ReadOnly = true;
            this.UPDATE_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_DATE.RegistCheckMethod")));
            this.UPDATE_DATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UPDATE_DATE.Width = 145;
            // 
            // CREATE_USER
            // 
            this.CREATE_USER.DataPropertyName = "CREATE_USER";
            this.CREATE_USER.DBFieldsName = "CREATE_USER";
            this.CREATE_USER.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            this.CREATE_USER.DefaultCellStyle = dataGridViewCellStyle9;
            this.CREATE_USER.DisplayItemName = "作成者";
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
            this.CREATE_USER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CREATE_DATE
            // 
            this.CREATE_DATE.DataPropertyName = "CREATE_DATE";
            this.CREATE_DATE.DBFieldsName = "CREATE_DATE";
            this.CREATE_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle10.Format = "G";
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            this.CREATE_DATE.DefaultCellStyle = dataGridViewCellStyle10;
            this.CREATE_DATE.DisplayItemName = "作成日";
            this.CREATE_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_DATE.FocusOutCheckMethod")));
            this.CREATE_DATE.HeaderText = "作成日";
            this.CREATE_DATE.ItemDefinedTypes = "datetime";
            this.CREATE_DATE.Name = "CREATE_DATE";
            this.CREATE_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CREATE_DATE.PopupSearchSendParams")));
            this.CREATE_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_DATE.popupWindowSetting")));
            this.CREATE_DATE.ReadOnly = true;
            this.CREATE_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_DATE.RegistCheckMethod")));
            this.CREATE_DATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CREATE_DATE.Width = 145;
            // 
            // DELETE_FLG
            // 
            this.DELETE_FLG.DataPropertyName = "DELETE_FLG";
            this.DELETE_FLG.DBFieldsName = "DELETE_FLG";
            this.DELETE_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            this.DELETE_FLG.DefaultCellStyle = dataGridViewCellStyle11;
            this.DELETE_FLG.DisplayItemName = "DELETE_FLG";
            this.DELETE_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DELETE_FLG.FocusOutCheckMethod")));
            this.DELETE_FLG.HeaderText = "DELETE_FLG";
            this.DELETE_FLG.ItemDefinedTypes = "bit";
            this.DELETE_FLG.Name = "DELETE_FLG";
            this.DELETE_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DELETE_FLG.PopupSearchSendParams")));
            this.DELETE_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DELETE_FLG.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DELETE_FLG.popupWindowSetting")));
            this.DELETE_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DELETE_FLG.RegistCheckMethod")));
            this.DELETE_FLG.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DELETE_FLG.Visible = false;
            // 
            // CREATE_PC
            // 
            this.CREATE_PC.DataPropertyName = "CREATE_PC";
            this.CREATE_PC.DBFieldsName = "CREATE_PC";
            this.CREATE_PC.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            this.CREATE_PC.DefaultCellStyle = dataGridViewCellStyle12;
            this.CREATE_PC.DisplayItemName = "CREATE_PC";
            this.CREATE_PC.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_PC.FocusOutCheckMethod")));
            this.CREATE_PC.HeaderText = "CREATE_PC";
            this.CREATE_PC.ItemDefinedTypes = "varchar";
            this.CREATE_PC.Name = "CREATE_PC";
            this.CREATE_PC.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CREATE_PC.PopupSearchSendParams")));
            this.CREATE_PC.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_PC.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_PC.popupWindowSetting")));
            this.CREATE_PC.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_PC.RegistCheckMethod")));
            this.CREATE_PC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CREATE_PC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CREATE_PC.Visible = false;
            // 
            // UPDATE_PC
            // 
            this.UPDATE_PC.DataPropertyName = "UPDATE_PC";
            this.UPDATE_PC.DBFieldsName = "UPDATE_PC";
            this.UPDATE_PC.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Black;
            this.UPDATE_PC.DefaultCellStyle = dataGridViewCellStyle13;
            this.UPDATE_PC.DisplayItemName = "UPDATE_PC";
            this.UPDATE_PC.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_PC.FocusOutCheckMethod")));
            this.UPDATE_PC.HeaderText = "UPDATE_PC";
            this.UPDATE_PC.ItemDefinedTypes = "varchar";
            this.UPDATE_PC.Name = "UPDATE_PC";
            this.UPDATE_PC.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPDATE_PC.PopupSearchSendParams")));
            this.UPDATE_PC.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPDATE_PC.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPDATE_PC.popupWindowSetting")));
            this.UPDATE_PC.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_PC.RegistCheckMethod")));
            this.UPDATE_PC.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.UPDATE_PC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UPDATE_PC.Visible = false;
            // 
            // TIME_STAMP
            // 
            this.TIME_STAMP.DataPropertyName = "TIME_STAMP";
            this.TIME_STAMP.DBFieldsName = "TIME_STAMP";
            this.TIME_STAMP.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black;
            this.TIME_STAMP.DefaultCellStyle = dataGridViewCellStyle14;
            this.TIME_STAMP.DisplayItemName = "TIME_STAMP";
            this.TIME_STAMP.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TIME_STAMP.FocusOutCheckMethod")));
            this.TIME_STAMP.HeaderText = "TIME_STAMP";
            this.TIME_STAMP.ItemDefinedTypes = "timestamp";
            this.TIME_STAMP.Name = "TIME_STAMP";
            this.TIME_STAMP.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TIME_STAMP.PopupSearchSendParams")));
            this.TIME_STAMP.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TIME_STAMP.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TIME_STAMP.popupWindowSetting")));
            this.TIME_STAMP.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TIME_STAMP.RegistCheckMethod")));
            this.TIME_STAMP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TIME_STAMP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TIME_STAMP.Visible = false;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.Ichiran);
            this.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_DELETED);
            this.Controls.Add(this.CONDITION_VALUE);
            this.Controls.Add(this.CONDITION_ITEM);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label16);
            this.Name = "UIForm";
            this.Text = "UIForm";
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
        internal r_framework.CustomControl.CustomDataGridView Ichiran;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn chb_delete;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column MANIFEST_TEHAI_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn MANIFEST_TEHAI_NAME;
        private r_framework.CustomControl.DgvCustomTextBoxColumn MANIFEST_TEHAI_NAME_RYAKU;
        private r_framework.CustomControl.DgvCustomTextBoxColumn MANIFEST_TEHAI_BIKOU;
        private r_framework.CustomControl.DgvCustomTextBoxColumn UPDATE_USER;
        private r_framework.CustomControl.DgvCustomTextBoxColumn UPDATE_DATE;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CREATE_USER;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CREATE_DATE;
        private r_framework.CustomControl.DgvCustomTextBoxColumn DELETE_FLG;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CREATE_PC;
        private r_framework.CustomControl.DgvCustomTextBoxColumn UPDATE_PC;
        private r_framework.CustomControl.DgvCustomTextBoxColumn TIME_STAMP;
    }
}