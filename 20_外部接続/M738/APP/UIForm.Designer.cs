namespace Shougun.Core.ExternalConnection.ContenaKeikaDate
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.CONDITION_VALUE = new r_framework.CustomControl.CustomTextBox();
            this.CONDITION_ITEM = new r_framework.CustomControl.CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.Ichiran = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.CONDITION_DBFIELD = new r_framework.CustomControl.CustomTextBox();
            this.CONDITION_TYPE = new r_framework.CustomControl.CustomTextBox();
            this.dgvCustomTextBoxColumn1 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn2 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dataGridViewColourPickerColumn1 = new Shougun.Core.ExternalConnection.ContenaKeikaDate.CustomDataGridViewColumn.DataGridViewColourPickerColumn();
            this.dgvCustomTextBoxColumn3 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn4 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn5 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn6 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn7 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCustomCheckBoxColumn1 = new r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn();
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED = new r_framework.CustomControl.CustomCheckBox();
            this.chb_delete = new r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn();
            this.CONTENA_KEIKA_DATE = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.CONTENA_KEIKA_BACK_COLOR = new Shougun.Core.ExternalConnection.ContenaKeikaDate.CustomDataGridViewColumn.DataGridViewColourPickerColumn();
            this.CONTENA_KEIKA_BIKOU = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.UPDATE_USER = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.UPDATE_DATE = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CREATE_USER = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CREATE_DATE = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).BeginInit();
            this.SuspendLayout();
            // 
            // CONDITION_VALUE
            // 
            this.CONDITION_VALUE.BackColor = System.Drawing.SystemColors.Window;
            this.CONDITION_VALUE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CONDITION_VALUE.ChangeUpperCase = true;
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
            this.CONDITION_VALUE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CONDITION_VALUE.ForeColor = System.Drawing.Color.Black;
            this.CONDITION_VALUE.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.CONDITION_VALUE.IsInputErrorOccured = false;
            this.CONDITION_VALUE.ItemDefinedTypes = "";
            this.CONDITION_VALUE.Location = new System.Drawing.Point(247, 10);
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
            this.CONDITION_VALUE.TabIndex = 2;
            this.CONDITION_VALUE.Tag = "検索する文字を入力してください";
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
            this.CONDITION_ITEM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CONDITION_ITEM.ForeColor = System.Drawing.Color.Black;
            this.CONDITION_ITEM.IsInputErrorOccured = false;
            this.CONDITION_ITEM.Location = new System.Drawing.Point(98, 10);
            this.CONDITION_ITEM.MaxLength = 20;
            this.CONDITION_ITEM.Name = "CONDITION_ITEM";
            this.CONDITION_ITEM.PopupAfterExecute = null;
            this.CONDITION_ITEM.PopupAfterExecuteMethod = "PupupAfterExecuteFuncByConditonChange";
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
            this.CONDITION_ITEM.Size = new System.Drawing.Size(150, 20);
            this.CONDITION_ITEM.TabIndex = 1;
            this.CONDITION_ITEM.Tag = "検索条件項目を指定してください（スペースキー押下にて、検索画面を表示します）";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(542, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "表示条件";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label16.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(0, 10);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(93, 20);
            this.label16.TabIndex = 0;
            this.label16.Text = "検索条件";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Ichiran
            // 
            this.Ichiran.AllowUserToDeleteRows = false;
            this.Ichiran.AllowUserToResizeColumns = false;
            this.Ichiran.AllowUserToResizeRows = false;
            this.Ichiran.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Ichiran.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Ichiran.ColumnHeadersHeight = 22;
            this.Ichiran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.Ichiran.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chb_delete,
            this.CONTENA_KEIKA_DATE,
            this.CONTENA_KEIKA_BACK_COLOR,
            this.CONTENA_KEIKA_BIKOU,
            this.UPDATE_USER,
            this.UPDATE_DATE,
            this.CREATE_USER,
            this.CREATE_DATE});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Ichiran.DefaultCellStyle = dataGridViewCellStyle10;
            this.Ichiran.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.Ichiran.EnableHeadersVisualStyles = false;
            this.Ichiran.GridColor = System.Drawing.Color.White;
            this.Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Ichiran.IsReload = false;
            this.Ichiran.LinkedDataPanelName = null;
            this.Ichiran.Location = new System.Drawing.Point(0, 37);
            this.Ichiran.MultiSelect = false;
            this.Ichiran.Name = "Ichiran";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Ichiran.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.Ichiran.RowHeadersVisible = false;
            this.Ichiran.RowTemplate.Height = 21;
            this.Ichiran.ShowCellToolTips = false;
            this.Ichiran.Size = new System.Drawing.Size(990, 410);
            this.Ichiran.TabIndex = 5;
            this.Ichiran.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Ichiran_CellEnter);
            this.Ichiran.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.Ichiran_CellParsing);
            this.Ichiran.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.Ichiran_CellValidating);
            this.Ichiran.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.Ichiran_DefaultValuesNeeded);
            this.Ichiran.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.Ichiran_EditingControlShowing);
            // 
            // CONDITION_DBFIELD
            // 
            this.CONDITION_DBFIELD.BackColor = System.Drawing.SystemColors.Window;
            this.CONDITION_DBFIELD.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION_DBFIELD.DisplayPopUp = null;
            this.CONDITION_DBFIELD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_DBFIELD.FocusOutCheckMethod")));
            this.CONDITION_DBFIELD.ForeColor = System.Drawing.Color.Black;
            this.CONDITION_DBFIELD.IsInputErrorOccured = false;
            this.CONDITION_DBFIELD.Location = new System.Drawing.Point(194, 449);
            this.CONDITION_DBFIELD.Name = "CONDITION_DBFIELD";
            this.CONDITION_DBFIELD.PopupAfterExecute = null;
            this.CONDITION_DBFIELD.PopupBeforeExecute = null;
            this.CONDITION_DBFIELD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONDITION_DBFIELD.PopupSearchSendParams")));
            this.CONDITION_DBFIELD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION_DBFIELD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONDITION_DBFIELD.popupWindowSetting")));
            this.CONDITION_DBFIELD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_DBFIELD.RegistCheckMethod")));
            this.CONDITION_DBFIELD.Size = new System.Drawing.Size(242, 19);
            this.CONDITION_DBFIELD.TabIndex = 7;
            this.CONDITION_DBFIELD.Tag = "";
            this.CONDITION_DBFIELD.Visible = false;
            // 
            // CONDITION_TYPE
            // 
            this.CONDITION_TYPE.BackColor = System.Drawing.SystemColors.Window;
            this.CONDITION_TYPE.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION_TYPE.DisplayPopUp = null;
            this.CONDITION_TYPE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_TYPE.FocusOutCheckMethod")));
            this.CONDITION_TYPE.ForeColor = System.Drawing.Color.Black;
            this.CONDITION_TYPE.IsInputErrorOccured = false;
            this.CONDITION_TYPE.Location = new System.Drawing.Point(2, 449);
            this.CONDITION_TYPE.Name = "CONDITION_TYPE";
            this.CONDITION_TYPE.PopupAfterExecute = null;
            this.CONDITION_TYPE.PopupBeforeExecute = null;
            this.CONDITION_TYPE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONDITION_TYPE.PopupSearchSendParams")));
            this.CONDITION_TYPE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION_TYPE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONDITION_TYPE.popupWindowSetting")));
            this.CONDITION_TYPE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_TYPE.RegistCheckMethod")));
            this.CONDITION_TYPE.Size = new System.Drawing.Size(186, 19);
            this.CONDITION_TYPE.TabIndex = 6;
            this.CONDITION_TYPE.Tag = "";
            this.CONDITION_TYPE.Visible = false;
            // 
            // dgvCustomTextBoxColumn1
            // 
            this.dgvCustomTextBoxColumn1.CopyAutoSetControl = "GENCHAKU_TIME_NAME_RYAKU";
            this.dgvCustomTextBoxColumn1.DataPropertyName = "GENCHAKU_TIME_NAME";
            this.dgvCustomTextBoxColumn1.DBFieldsName = "GENCHAKU_TIME_NAME";
            this.dgvCustomTextBoxColumn1.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCustomTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvCustomTextBoxColumn1.DisplayItemName = "現着時間名";
            this.dgvCustomTextBoxColumn1.FillWeight = 150F;
            this.dgvCustomTextBoxColumn1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn1.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn1.HeaderText = "現着時間名※";
            this.dgvCustomTextBoxColumn1.ItemDefinedTypes = "string";
            this.dgvCustomTextBoxColumn1.MaxInputLength = 10;
            this.dgvCustomTextBoxColumn1.Name = "dgvCustomTextBoxColumn1";
            this.dgvCustomTextBoxColumn1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn1.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn1.popupWindowSetting")));
            this.dgvCustomTextBoxColumn1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn1.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCustomTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCustomTextBoxColumn1.ToolTipText = "全角１０桁以内で入力してください";
            this.dgvCustomTextBoxColumn1.Width = 150;
            // 
            // dgvCustomTextBoxColumn2
            // 
            this.dgvCustomTextBoxColumn2.DataPropertyName = "GENCHAKU_TIME_NAME_RYAKU";
            this.dgvCustomTextBoxColumn2.DBFieldsName = "GENCHAKU_TIME_NAME_RYAKU";
            this.dgvCustomTextBoxColumn2.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCustomTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgvCustomTextBoxColumn2.DisplayItemName = "略称";
            this.dgvCustomTextBoxColumn2.FillWeight = 150F;
            this.dgvCustomTextBoxColumn2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn2.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn2.HeaderText = "略称※";
            this.dgvCustomTextBoxColumn2.ItemDefinedTypes = "string";
            this.dgvCustomTextBoxColumn2.MaxInputLength = 10;
            this.dgvCustomTextBoxColumn2.Name = "dgvCustomTextBoxColumn2";
            this.dgvCustomTextBoxColumn2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn2.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn2.popupWindowSetting")));
            this.dgvCustomTextBoxColumn2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn2.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCustomTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCustomTextBoxColumn2.ToolTipText = "全角１０桁以内で入力してください";
            this.dgvCustomTextBoxColumn2.Width = 150;
            // 
            // dataGridViewColourPickerColumn1
            // 
            this.dataGridViewColourPickerColumn1.DataPropertyName = "GENCHAKU_BACK_COLOR";
            this.dataGridViewColourPickerColumn1.DBFieldsName = "GENCHAKU_BACK_COLOR";
            this.dataGridViewColourPickerColumn1.DefaultBackColor = System.Drawing.Color.Empty;
            this.dataGridViewColourPickerColumn1.DisplayItemName = "背景色";
            this.dataGridViewColourPickerColumn1.FillWeight = 120F;
            this.dataGridViewColourPickerColumn1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dataGridViewColourPickerColumn1.FocusOutCheckMethod")));
            this.dataGridViewColourPickerColumn1.HeaderText = "背景色※";
            this.dataGridViewColourPickerColumn1.ItemDefinedTypes = "SqlInt32";
            this.dataGridViewColourPickerColumn1.Name = "dataGridViewColourPickerColumn1";
            this.dataGridViewColourPickerColumn1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dataGridViewColourPickerColumn1.PopupSearchSendParams")));
            this.dataGridViewColourPickerColumn1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dataGridViewColourPickerColumn1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dataGridViewColourPickerColumn1.popupWindowSetting")));
            this.dataGridViewColourPickerColumn1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dataGridViewColourPickerColumn1.RegistCheckMethod")));
            this.dataGridViewColourPickerColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewColourPickerColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewColourPickerColumn1.ToolTipText = "背景色の指定をしてください";
            this.dataGridViewColourPickerColumn1.ViewSearchItem = false;
            this.dataGridViewColourPickerColumn1.Width = 120;
            // 
            // dgvCustomTextBoxColumn3
            // 
            this.dgvCustomTextBoxColumn3.DataPropertyName = "GENCHAKU_TIME_BIKOU";
            this.dgvCustomTextBoxColumn3.DBFieldsName = "GENCHAKU_TIME_BIKOU";
            this.dgvCustomTextBoxColumn3.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCustomTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgvCustomTextBoxColumn3.FillWeight = 60F;
            this.dgvCustomTextBoxColumn3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn3.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn3.HeaderText = "備考";
            this.dgvCustomTextBoxColumn3.ItemDefinedTypes = "string";
            this.dgvCustomTextBoxColumn3.MaxInputLength = 10;
            this.dgvCustomTextBoxColumn3.Name = "dgvCustomTextBoxColumn3";
            this.dgvCustomTextBoxColumn3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn3.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn3.popupWindowSetting")));
            this.dgvCustomTextBoxColumn3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn3.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCustomTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCustomTextBoxColumn3.ToolTipText = "全角１０桁以内で入力してください";
            this.dgvCustomTextBoxColumn3.Width = 150;
            // 
            // dgvCustomTextBoxColumn4
            // 
            this.dgvCustomTextBoxColumn4.DataPropertyName = "UPDATE_USER";
            this.dgvCustomTextBoxColumn4.DBFieldsName = "UPDATE_USER";
            this.dgvCustomTextBoxColumn4.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCustomTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle15;
            this.dgvCustomTextBoxColumn4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn4.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn4.HeaderText = "更新者";
            this.dgvCustomTextBoxColumn4.ItemDefinedTypes = "string";
            this.dgvCustomTextBoxColumn4.Name = "dgvCustomTextBoxColumn4";
            this.dgvCustomTextBoxColumn4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn4.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn4.popupWindowSetting")));
            this.dgvCustomTextBoxColumn4.ReadOnly = true;
            this.dgvCustomTextBoxColumn4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn4.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCustomTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCustomTextBoxColumn4.Width = 125;
            // 
            // dgvCustomTextBoxColumn5
            // 
            this.dgvCustomTextBoxColumn5.DataPropertyName = "UPDATE_DATE";
            this.dgvCustomTextBoxColumn5.DBFieldsName = "UPDATE_DATE";
            this.dgvCustomTextBoxColumn5.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle16.Format = "G";
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCustomTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle16;
            this.dgvCustomTextBoxColumn5.FillWeight = 145F;
            this.dgvCustomTextBoxColumn5.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn5.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn5.HeaderText = "更新日";
            this.dgvCustomTextBoxColumn5.ItemDefinedTypes = "SqlDateTime";
            this.dgvCustomTextBoxColumn5.Name = "dgvCustomTextBoxColumn5";
            this.dgvCustomTextBoxColumn5.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn5.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn5.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn5.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn5.popupWindowSetting")));
            this.dgvCustomTextBoxColumn5.ReadOnly = true;
            this.dgvCustomTextBoxColumn5.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn5.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCustomTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCustomTextBoxColumn5.Width = 145;
            // 
            // dgvCustomTextBoxColumn6
            // 
            this.dgvCustomTextBoxColumn6.DataPropertyName = "CREATE_USER";
            this.dgvCustomTextBoxColumn6.DBFieldsName = "CREATE_USER";
            this.dgvCustomTextBoxColumn6.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCustomTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle17;
            this.dgvCustomTextBoxColumn6.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn6.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn6.HeaderText = "作成者";
            this.dgvCustomTextBoxColumn6.ItemDefinedTypes = "string";
            this.dgvCustomTextBoxColumn6.Name = "dgvCustomTextBoxColumn6";
            this.dgvCustomTextBoxColumn6.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn6.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn6.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn6.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn6.popupWindowSetting")));
            this.dgvCustomTextBoxColumn6.ReadOnly = true;
            this.dgvCustomTextBoxColumn6.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn6.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCustomTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCustomTextBoxColumn6.Width = 125;
            // 
            // dgvCustomTextBoxColumn7
            // 
            this.dgvCustomTextBoxColumn7.DataPropertyName = "CREATE_DATE";
            this.dgvCustomTextBoxColumn7.DBFieldsName = "CREATE_DATE";
            this.dgvCustomTextBoxColumn7.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle18.Format = "G";
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCustomTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle18;
            this.dgvCustomTextBoxColumn7.FillWeight = 145F;
            this.dgvCustomTextBoxColumn7.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn7.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn7.HeaderText = "作成日";
            this.dgvCustomTextBoxColumn7.ItemDefinedTypes = "SqlDateTime";
            this.dgvCustomTextBoxColumn7.Name = "dgvCustomTextBoxColumn7";
            this.dgvCustomTextBoxColumn7.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn7.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn7.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn7.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn7.popupWindowSetting")));
            this.dgvCustomTextBoxColumn7.ReadOnly = true;
            this.dgvCustomTextBoxColumn7.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn7.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCustomTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCustomTextBoxColumn7.Width = 145;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "GENCHAKU_TIME_BIKOU";
            this.dataGridViewTextBoxColumn1.FillWeight = 120F;
            this.dataGridViewTextBoxColumn1.HeaderText = "現時時間備考";
            this.dataGridViewTextBoxColumn1.MaxInputLength = 20;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ToolTipText = "現時時間備考を入力してください";
            this.dataGridViewTextBoxColumn1.Width = 120;
            // 
            // dgvCustomCheckBoxColumn1
            // 
            this.dgvCustomCheckBoxColumn1.DataPropertyName = "DELETE_FLG";
            this.dgvCustomCheckBoxColumn1.DBFieldsName = "DELETE_FLG";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle19.NullValue = false;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.Color.Cyan;
            this.dgvCustomCheckBoxColumn1.DefaultCellStyle = dataGridViewCellStyle19;
            this.dgvCustomCheckBoxColumn1.FillWeight = 43F;
            this.dgvCustomCheckBoxColumn1.FocusOutCheckMethod = null;
            this.dgvCustomCheckBoxColumn1.HeaderText = "削除";
            this.dgvCustomCheckBoxColumn1.ItemDefinedTypes = "SqlBoolean";
            this.dgvCustomCheckBoxColumn1.Name = "dgvCustomCheckBoxColumn1";
            this.dgvCustomCheckBoxColumn1.RegistCheckMethod = null;
            this.dgvCustomCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCustomCheckBoxColumn1.ToolTipText = "削除する場合、チェックしてください";
            this.dgvCustomCheckBoxColumn1.ViewSearchItem = false;
            this.dgvCustomCheckBoxColumn1.Width = 43;
            // 
            // ICHIRAN_HYOUJI_JOUKEN_DELETED
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.DefaultBackColor = System.Drawing.Color.Empty;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ICHIRAN_HYOUJI_JOUKEN_DELETED.FocusOutCheckMethod")));
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Location = new System.Drawing.Point(639, 12);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Name = "ICHIRAN_HYOUJI_JOUKEN_DELETED";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.PopupAfterExecute = null;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.PopupBeforeExecute = null;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ICHIRAN_HYOUJI_JOUKEN_DELETED.PopupSearchSendParams")));
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ICHIRAN_HYOUJI_JOUKEN_DELETED.popupWindowSetting")));
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ICHIRAN_HYOUJI_JOUKEN_DELETED.RegistCheckMethod")));
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Size = new System.Drawing.Size(185, 17);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.TabIndex = 3;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Tag = "削除されたデータを対象とする場合チェックを付けてください";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Text = "削除済も含めて全て表示";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.UseVisualStyleBackColor = false;
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
            this.chb_delete.FillWeight = 43F;
            this.chb_delete.FocusOutCheckMethod = null;
            this.chb_delete.HeaderText = "削除";
            this.chb_delete.ItemDefinedTypes = "SqlBoolean";
            this.chb_delete.Name = "chb_delete";
            this.chb_delete.RegistCheckMethod = null;
            this.chb_delete.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.chb_delete.ToolTipText = "削除する場合、チェックしてください";
            this.chb_delete.ViewSearchItem = false;
            this.chb_delete.Width = 43;
            // 
            // CONTENA_KEIKA_DATE
            // 
            this.CONTENA_KEIKA_DATE.CustomFormatSetting = "0";
            this.CONTENA_KEIKA_DATE.DataPropertyName = "CONTENA_KEIKA_DATE";
            this.CONTENA_KEIKA_DATE.DBFieldsName = "CONTENA_KEIKA_DATE";
            this.CONTENA_KEIKA_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.CONTENA_KEIKA_DATE.DefaultCellStyle = dataGridViewCellStyle3;
            this.CONTENA_KEIKA_DATE.DisplayItemName = "日数";
            this.CONTENA_KEIKA_DATE.FillWeight = 110F;
            this.CONTENA_KEIKA_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_KEIKA_DATE.FocusOutCheckMethod")));
            this.CONTENA_KEIKA_DATE.FormatSetting = "カスタム";
            this.CONTENA_KEIKA_DATE.HeaderText = "設置経過日数（以内）※";
            this.CONTENA_KEIKA_DATE.ItemDefinedTypes = "SqlInt16";
            this.CONTENA_KEIKA_DATE.Name = "CONTENA_KEIKA_DATE";
            this.CONTENA_KEIKA_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONTENA_KEIKA_DATE.PopupSearchSendParams")));
            this.CONTENA_KEIKA_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONTENA_KEIKA_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONTENA_KEIKA_DATE.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.CONTENA_KEIKA_DATE.RangeSetting = rangeSettingDto1;
            this.CONTENA_KEIKA_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_KEIKA_DATE.RegistCheckMethod")));
            this.CONTENA_KEIKA_DATE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CONTENA_KEIKA_DATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CONTENA_KEIKA_DATE.ToolTipText = "数値4桁以内で入力してください";
            this.CONTENA_KEIKA_DATE.Width = 200;
            // 
            // CONTENA_KEIKA_BACK_COLOR
            // 
            this.CONTENA_KEIKA_BACK_COLOR.DataPropertyName = "CONTENA_KEIKA_BACK_COLOR";
            this.CONTENA_KEIKA_BACK_COLOR.DBFieldsName = "CONTENA_KEIKA_BACK_COLOR";
            this.CONTENA_KEIKA_BACK_COLOR.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.CONTENA_KEIKA_BACK_COLOR.DefaultCellStyle = dataGridViewCellStyle4;
            this.CONTENA_KEIKA_BACK_COLOR.DisplayItemName = "表示色";
            this.CONTENA_KEIKA_BACK_COLOR.FillWeight = 120F;
            this.CONTENA_KEIKA_BACK_COLOR.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_KEIKA_BACK_COLOR.FocusOutCheckMethod")));
            this.CONTENA_KEIKA_BACK_COLOR.HeaderText = "表示色※";
            this.CONTENA_KEIKA_BACK_COLOR.ItemDefinedTypes = "SqlInt32";
            this.CONTENA_KEIKA_BACK_COLOR.Name = "CONTENA_KEIKA_BACK_COLOR";
            this.CONTENA_KEIKA_BACK_COLOR.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONTENA_KEIKA_BACK_COLOR.PopupSearchSendParams")));
            this.CONTENA_KEIKA_BACK_COLOR.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONTENA_KEIKA_BACK_COLOR.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONTENA_KEIKA_BACK_COLOR.popupWindowSetting")));
            this.CONTENA_KEIKA_BACK_COLOR.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_KEIKA_BACK_COLOR.RegistCheckMethod")));
            this.CONTENA_KEIKA_BACK_COLOR.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CONTENA_KEIKA_BACK_COLOR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CONTENA_KEIKA_BACK_COLOR.ToolTipText = "表示色の指定をしてください";
            this.CONTENA_KEIKA_BACK_COLOR.ViewSearchItem = false;
            this.CONTENA_KEIKA_BACK_COLOR.Width = 120;
            // 
            // CONTENA_KEIKA_BIKOU
            // 
            this.CONTENA_KEIKA_BIKOU.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.CONTENA_KEIKA_BIKOU.DataPropertyName = "CONTENA_KEIKA_BIKOU";
            this.CONTENA_KEIKA_BIKOU.DBFieldsName = "CONTENA_KEIKA_BIKOU";
            this.CONTENA_KEIKA_BIKOU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CONTENA_KEIKA_BIKOU.DefaultCellStyle = dataGridViewCellStyle5;
            this.CONTENA_KEIKA_BIKOU.FillWeight = 60F;
            this.CONTENA_KEIKA_BIKOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_KEIKA_BIKOU.FocusOutCheckMethod")));
            this.CONTENA_KEIKA_BIKOU.HeaderText = "備考";
            this.CONTENA_KEIKA_BIKOU.ItemDefinedTypes = "string";
            this.CONTENA_KEIKA_BIKOU.MaxInputLength = 40;
            this.CONTENA_KEIKA_BIKOU.Name = "CONTENA_KEIKA_BIKOU";
            this.CONTENA_KEIKA_BIKOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONTENA_KEIKA_BIKOU.PopupSearchSendParams")));
            this.CONTENA_KEIKA_BIKOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONTENA_KEIKA_BIKOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONTENA_KEIKA_BIKOU.popupWindowSetting")));
            this.CONTENA_KEIKA_BIKOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_KEIKA_BIKOU.RegistCheckMethod")));
            this.CONTENA_KEIKA_BIKOU.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CONTENA_KEIKA_BIKOU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CONTENA_KEIKA_BIKOU.ToolTipText = "全角２０桁以内で入力してください";
            this.CONTENA_KEIKA_BIKOU.Width = 150;
            // 
            // UPDATE_USER
            // 
            this.UPDATE_USER.DataPropertyName = "UPDATE_USER";
            this.UPDATE_USER.DBFieldsName = "UPDATE_USER";
            this.UPDATE_USER.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.UPDATE_USER.DefaultCellStyle = dataGridViewCellStyle6;
            this.UPDATE_USER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_USER.FocusOutCheckMethod")));
            this.UPDATE_USER.HeaderText = "更新者";
            this.UPDATE_USER.ItemDefinedTypes = "string";
            this.UPDATE_USER.Name = "UPDATE_USER";
            this.UPDATE_USER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPDATE_USER.PopupSearchSendParams")));
            this.UPDATE_USER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPDATE_USER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPDATE_USER.popupWindowSetting")));
            this.UPDATE_USER.ReadOnly = true;
            this.UPDATE_USER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_USER.RegistCheckMethod")));
            this.UPDATE_USER.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.UPDATE_USER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UPDATE_USER.Width = 125;
            // 
            // UPDATE_DATE
            // 
            this.UPDATE_DATE.DataPropertyName = "UPDATE_DATE";
            this.UPDATE_DATE.DBFieldsName = "UPDATE_DATE";
            this.UPDATE_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle7.Format = "G";
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.UPDATE_DATE.DefaultCellStyle = dataGridViewCellStyle7;
            this.UPDATE_DATE.FillWeight = 145F;
            this.UPDATE_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_DATE.FocusOutCheckMethod")));
            this.UPDATE_DATE.HeaderText = "更新日";
            this.UPDATE_DATE.ItemDefinedTypes = "SqlDateTime";
            this.UPDATE_DATE.Name = "UPDATE_DATE";
            this.UPDATE_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPDATE_DATE.PopupSearchSendParams")));
            this.UPDATE_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPDATE_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPDATE_DATE.popupWindowSetting")));
            this.UPDATE_DATE.ReadOnly = true;
            this.UPDATE_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_DATE.RegistCheckMethod")));
            this.UPDATE_DATE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.UPDATE_DATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UPDATE_DATE.Width = 145;
            // 
            // CREATE_USER
            // 
            this.CREATE_USER.DataPropertyName = "CREATE_USER";
            this.CREATE_USER.DBFieldsName = "CREATE_USER";
            this.CREATE_USER.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CREATE_USER.DefaultCellStyle = dataGridViewCellStyle8;
            this.CREATE_USER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_USER.FocusOutCheckMethod")));
            this.CREATE_USER.HeaderText = "作成者";
            this.CREATE_USER.ItemDefinedTypes = "string";
            this.CREATE_USER.Name = "CREATE_USER";
            this.CREATE_USER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CREATE_USER.PopupSearchSendParams")));
            this.CREATE_USER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_USER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_USER.popupWindowSetting")));
            this.CREATE_USER.ReadOnly = true;
            this.CREATE_USER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_USER.RegistCheckMethod")));
            this.CREATE_USER.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CREATE_USER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CREATE_USER.Width = 125;
            // 
            // CREATE_DATE
            // 
            this.CREATE_DATE.DataPropertyName = "CREATE_DATE";
            this.CREATE_DATE.DBFieldsName = "CREATE_DATE";
            this.CREATE_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle9.Format = "G";
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CREATE_DATE.DefaultCellStyle = dataGridViewCellStyle9;
            this.CREATE_DATE.FillWeight = 145F;
            this.CREATE_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_DATE.FocusOutCheckMethod")));
            this.CREATE_DATE.HeaderText = "作成日";
            this.CREATE_DATE.ItemDefinedTypes = "SqlDateTime";
            this.CREATE_DATE.Name = "CREATE_DATE";
            this.CREATE_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CREATE_DATE.PopupSearchSendParams")));
            this.CREATE_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_DATE.popupWindowSetting")));
            this.CREATE_DATE.ReadOnly = true;
            this.CREATE_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_DATE.RegistCheckMethod")));
            this.CREATE_DATE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CREATE_DATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CREATE_DATE.Width = 145;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_DELETED);
            this.Controls.Add(this.CONDITION_DBFIELD);
            this.Controls.Add(this.CONDITION_TYPE);
            this.Controls.Add(this.Ichiran);
            this.Controls.Add(this.CONDITION_VALUE);
            this.Controls.Add(this.CONDITION_ITEM);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label16);
            this.Name = "UIForm";
            this.Text = "GenchakuJikanHoshuForm";
            this.Shown += new System.EventHandler(this.GenchakuJikanHoshuForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomTextBox CONDITION_VALUE;
        internal r_framework.CustomControl.CustomTextBox CONDITION_ITEM;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label16;
        public r_framework.CustomControl.CustomDataGridView Ichiran;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private CustomDataGridViewColumn.DataGridViewColourPickerColumn dataGridViewColourPickerColumn1;
        internal r_framework.CustomControl.CustomTextBox CONDITION_DBFIELD;
        internal r_framework.CustomControl.CustomTextBox CONDITION_TYPE;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn1;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn2;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn3;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn4;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn5;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn6;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn7;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn dgvCustomCheckBoxColumn1;
        internal r_framework.CustomControl.CustomCheckBox ICHIRAN_HYOUJI_JOUKEN_DELETED;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn chb_delete;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column CONTENA_KEIKA_DATE;
        private CustomDataGridViewColumn.DataGridViewColourPickerColumn CONTENA_KEIKA_BACK_COLOR;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CONTENA_KEIKA_BIKOU;
        private r_framework.CustomControl.DgvCustomTextBoxColumn UPDATE_USER;
        private r_framework.CustomControl.DgvCustomTextBoxColumn UPDATE_DATE;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CREATE_USER;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CREATE_DATE;
    }
}