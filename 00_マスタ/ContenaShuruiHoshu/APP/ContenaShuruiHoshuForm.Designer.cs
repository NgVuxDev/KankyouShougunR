namespace ContenaShuruiHoshu.APP
{
    partial class ContenaShuruiHoshuForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContenaShuruiHoshuForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI = new System.Windows.Forms.CheckBox();
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED = new System.Windows.Forms.CheckBox();
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU = new System.Windows.Forms.CheckBox();
            this.CONDITION_VALUE = new r_framework.CustomControl.CustomTextBox();
            this.CONDITION_ITEM = new r_framework.CustomControl.CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.Ichiran = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.chb_delete = new r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn();
            this.CONTENA_SHURUI_CD = new r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn();
            this.CONTENA_SHURUI_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CONTENA_SHURUI_NAME_RYAKU = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CONTENA_SHURUI_FURIGANA = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CONTENA_SHURUI_BIKOU = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.TEKIYOU_BEGIN = new r_framework.CustomControl.DgvCustomDataTimeColumn();
            this.TEKIYOU_END = new r_framework.CustomControl.DgvCustomDataTimeColumn();
            this.UPDATE_USER = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.UPDATE_DATE = new r_framework.CustomControl.DgvCustomDataTimeColumn();
            this.CREATE_USER = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CREATE_DATE = new r_framework.CustomControl.DgvCustomDataTimeColumn();
            this.CREATE_PC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DELETE_FLG = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.UPDATE_PC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CONDITION_TYPE = new r_framework.CustomControl.CustomTextBox();
            this.CONDITION_DBFIELD = new r_framework.CustomControl.CustomTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).BeginInit();
            this.SuspendLayout();
            // 
            // ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Location = new System.Drawing.Point(771, 12);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Name = "ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Size = new System.Drawing.Size(84, 16);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.TabIndex = 413;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Text = "適用期間外";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.UseVisualStyleBackColor = true;
            // 
            // ICHIRAN_HYOUJI_JOUKEN_DELETED
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Location = new System.Drawing.Point(717, 12);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Name = "ICHIRAN_HYOUJI_JOUKEN_DELETED";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Size = new System.Drawing.Size(48, 16);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.TabIndex = 412;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Text = "削除";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.UseVisualStyleBackColor = true;
            // 
            // ICHIRAN_HYOUJI_JOUKEN_TEKIYOU
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Location = new System.Drawing.Point(651, 12);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Name = "ICHIRAN_HYOUJI_JOUKEN_TEKIYOU";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Size = new System.Drawing.Size(60, 16);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.TabIndex = 411;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Text = "適用中";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.UseVisualStyleBackColor = true;
            // 
            // CONDITION_VALUE
            // 
            this.CONDITION_VALUE.BackColor = System.Drawing.SystemColors.Window;
            this.CONDITION_VALUE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CONDITION_VALUE.ChangeUpperCase = true;
            this.CONDITION_VALUE.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.CONDITION_VALUE.DBFieldsName = "";
            this.CONDITION_VALUE.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION_VALUE.DisplayItemName = "検索条件";
            this.CONDITION_VALUE.DisplayPopUp = null;
            this.CONDITION_VALUE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_VALUE.FocusOutCheckMethod")));
            this.CONDITION_VALUE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CONDITION_VALUE.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.CONDITION_VALUE.ItemDefinedTypes = "";
            this.CONDITION_VALUE.Location = new System.Drawing.Point(258, 10);
            this.CONDITION_VALUE.MaxLength = 20;
            this.CONDITION_VALUE.Name = "CONDITION_VALUE";
            this.CONDITION_VALUE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONDITION_VALUE.PopupSearchSendParams")));
            this.CONDITION_VALUE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION_VALUE.PopupWindowName = "";
            this.CONDITION_VALUE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONDITION_VALUE.popupWindowSetting")));
            this.CONDITION_VALUE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_VALUE.RegistCheckMethod")));
            this.CONDITION_VALUE.SetFormField = "";
            this.CONDITION_VALUE.ShortItemName = "検索条件";
            this.CONDITION_VALUE.Size = new System.Drawing.Size(290, 20);
            this.CONDITION_VALUE.TabIndex = 410;
            this.CONDITION_VALUE.Tag = "検索する文字を入力して下さい";
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
            this.CONDITION_ITEM.Location = new System.Drawing.Point(109, 10);
            this.CONDITION_ITEM.MaxLength = 10;
            this.CONDITION_ITEM.Name = "CONDITION_ITEM";
            this.CONDITION_ITEM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONDITION_ITEM.PopupSearchSendParams")));
            this.CONDITION_ITEM.PopupSendParams = new string[] {
        "Ichiran"};
            this.CONDITION_ITEM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION_ITEM.PopupWindowName = "マスタ検索項目ポップアップ";
            this.CONDITION_ITEM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONDITION_ITEM.popupWindowSetting")));
            this.CONDITION_ITEM.ReadOnly = true;
            this.CONDITION_ITEM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_ITEM.RegistCheckMethod")));
            this.CONDITION_ITEM.SetFormField = "CONDITION_ITEM,CONDITION_TYPE,CONDITION_DBFIELD";
            this.CONDITION_ITEM.ShortItemName = "検索条件";
            this.CONDITION_ITEM.Size = new System.Drawing.Size(150, 20);
            this.CONDITION_ITEM.TabIndex = 409;
            this.CONDITION_ITEM.Tag = "検索条件を指定して下さい（スペースキー押下にて、検索画面を表示します）";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(554, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 20);
            this.label1.TabIndex = 408;
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
            this.label16.Location = new System.Drawing.Point(10, 10);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(93, 20);
            this.label16.TabIndex = 407;
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
            this.CONTENA_SHURUI_CD,
            this.CONTENA_SHURUI_NAME,
            this.CONTENA_SHURUI_NAME_RYAKU,
            this.CONTENA_SHURUI_FURIGANA,
            this.CONTENA_SHURUI_BIKOU,
            this.TEKIYOU_BEGIN,
            this.TEKIYOU_END,
            this.UPDATE_USER,
            this.UPDATE_DATE,
            this.CREATE_USER,
            this.CREATE_DATE,
            this.CREATE_PC,
            this.DELETE_FLG,
            this.UPDATE_PC});
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Ichiran.DefaultCellStyle = dataGridViewCellStyle13;
            this.Ichiran.EnableHeadersVisualStyles = false;
            this.Ichiran.GridColor = System.Drawing.Color.White;
            this.Ichiran.IsReload = false;
            this.Ichiran.LinkedDataPanelName = null;
            this.Ichiran.Location = new System.Drawing.Point(10, 37);
            this.Ichiran.MultiSelect = false;
            this.Ichiran.Name = "Ichiran";
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle14.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Ichiran.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.Ichiran.RowHeadersVisible = false;
            this.Ichiran.RowTemplate.Height = 21;
            this.Ichiran.Size = new System.Drawing.Size(980, 433);
            this.Ichiran.TabIndex = 1228;
            this.Ichiran.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.Ichiran_CellBeginEdit);
            this.Ichiran.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Ichiran_CellEnter);
            this.Ichiran.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.Ichiran_CellValidating);
            this.Ichiran.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.Ichiran_EditingControlShowing);
            // 
            // chb_delete
            // 
            this.chb_delete.DataPropertyName = "DELETE_FLG";
            this.chb_delete.DBFieldsName = "";
            this.chb_delete.FillWeight = 43F;
            this.chb_delete.FocusOutCheckMethod = null;
            this.chb_delete.HeaderText = "削除";
            this.chb_delete.Name = "chb_delete";
            this.chb_delete.RegistCheckMethod = null;
            this.chb_delete.ToolTipText = "削除する場合は選択してください。";
            this.chb_delete.TrueValue = "true";
            this.chb_delete.Width = 43;
            // 
            // CONTENA_SHURUI_CD
            // 
            this.CONTENA_SHURUI_CD.CharactersNumber = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.CONTENA_SHURUI_CD.DataPropertyName = "CONTENA_SHURUI_CD";
            this.CONTENA_SHURUI_CD.DBFieldsName = "CONTENA_SHURUI_CD";
            this.CONTENA_SHURUI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CONTENA_SHURUI_CD.DefaultCellStyle = dataGridViewCellStyle2;
            this.CONTENA_SHURUI_CD.DisplayItemName = "";
            this.CONTENA_SHURUI_CD.FillWeight = 60F;
            this.CONTENA_SHURUI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_CD.FocusOutCheckMethod")));
            this.CONTENA_SHURUI_CD.HeaderText = "コンテナ種類CD ※";
            this.CONTENA_SHURUI_CD.MaxInputLength = 3;
            this.CONTENA_SHURUI_CD.Name = "CONTENA_SHURUI_CD";
            this.CONTENA_SHURUI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONTENA_SHURUI_CD.PopupSearchSendParams")));
            this.CONTENA_SHURUI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONTENA_SHURUI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONTENA_SHURUI_CD.popupWindowSetting")));
            this.CONTENA_SHURUI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_CD.RegistCheckMethod")));
            this.CONTENA_SHURUI_CD.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CONTENA_SHURUI_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CONTENA_SHURUI_CD.ToolTipText = "コンテナ種類CDを入力してください。";
            this.CONTENA_SHURUI_CD.Width = 135;
            this.CONTENA_SHURUI_CD.ZeroPaddengFlag = true;
            // 
            // CONTENA_SHURUI_NAME
            // 
            this.CONTENA_SHURUI_NAME.DataPropertyName = "CONTENA_SHURUI_NAME";
            this.CONTENA_SHURUI_NAME.DBFieldsName = "CONTENA_SHURUI_NAME";
            this.CONTENA_SHURUI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CONTENA_SHURUI_NAME.DefaultCellStyle = dataGridViewCellStyle3;
            this.CONTENA_SHURUI_NAME.FillWeight = 60F;
            this.CONTENA_SHURUI_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_NAME.FocusOutCheckMethod")));
            this.CONTENA_SHURUI_NAME.FuriganaAutoSetControl = "";
            this.CONTENA_SHURUI_NAME.HeaderText = "コンテナ種類名 ※";
            this.CONTENA_SHURUI_NAME.MaxInputLength = 10;
            this.CONTENA_SHURUI_NAME.Name = "CONTENA_SHURUI_NAME";
            this.CONTENA_SHURUI_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONTENA_SHURUI_NAME.PopupSearchSendParams")));
            this.CONTENA_SHURUI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONTENA_SHURUI_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONTENA_SHURUI_NAME.popupWindowSetting")));
            this.CONTENA_SHURUI_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_NAME.RegistCheckMethod")));
            this.CONTENA_SHURUI_NAME.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CONTENA_SHURUI_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CONTENA_SHURUI_NAME.ToolTipText = "コンテナ種類名を入力してください。";
            this.CONTENA_SHURUI_NAME.Width = 150;
            // 
            // CONTENA_SHURUI_NAME_RYAKU
            // 
            this.CONTENA_SHURUI_NAME_RYAKU.DataPropertyName = "CONTENA_SHURUI_NAME_RYAKU";
            this.CONTENA_SHURUI_NAME_RYAKU.DBFieldsName = "CONTENA_SHURUI_NAME_RYAKU";
            this.CONTENA_SHURUI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CONTENA_SHURUI_NAME_RYAKU.DefaultCellStyle = dataGridViewCellStyle4;
            this.CONTENA_SHURUI_NAME_RYAKU.FillWeight = 60F;
            this.CONTENA_SHURUI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_NAME_RYAKU.FocusOutCheckMethod")));
            this.CONTENA_SHURUI_NAME_RYAKU.HeaderText = "略称 ※";
            this.CONTENA_SHURUI_NAME_RYAKU.MaxInputLength = 5;
            this.CONTENA_SHURUI_NAME_RYAKU.Name = "CONTENA_SHURUI_NAME_RYAKU";
            this.CONTENA_SHURUI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONTENA_SHURUI_NAME_RYAKU.PopupSearchSendParams")));
            this.CONTENA_SHURUI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONTENA_SHURUI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONTENA_SHURUI_NAME_RYAKU.popupWindowSetting")));
            this.CONTENA_SHURUI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_NAME_RYAKU.RegistCheckMethod")));
            this.CONTENA_SHURUI_NAME_RYAKU.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CONTENA_SHURUI_NAME_RYAKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CONTENA_SHURUI_NAME_RYAKU.ToolTipText = "コンテナ種類の略称を入力してください。";
            this.CONTENA_SHURUI_NAME_RYAKU.Width = 80;
            // 
            // CONTENA_SHURUI_FURIGANA
            // 
            this.CONTENA_SHURUI_FURIGANA.DataPropertyName = "CONTENA_SHURUI_FURIGANA";
            this.CONTENA_SHURUI_FURIGANA.DBFieldsName = "CONTENA_SHURUI_FURIGANA";
            this.CONTENA_SHURUI_FURIGANA.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CONTENA_SHURUI_FURIGANA.DefaultCellStyle = dataGridViewCellStyle5;
            this.CONTENA_SHURUI_FURIGANA.FillWeight = 60F;
            this.CONTENA_SHURUI_FURIGANA.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_FURIGANA.FocusOutCheckMethod")));
            this.CONTENA_SHURUI_FURIGANA.HeaderText = "フリガナ ※";
            this.CONTENA_SHURUI_FURIGANA.MaxInputLength = 10;
            this.CONTENA_SHURUI_FURIGANA.Name = "CONTENA_SHURUI_FURIGANA";
            this.CONTENA_SHURUI_FURIGANA.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONTENA_SHURUI_FURIGANA.PopupSearchSendParams")));
            this.CONTENA_SHURUI_FURIGANA.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONTENA_SHURUI_FURIGANA.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONTENA_SHURUI_FURIGANA.popupWindowSetting")));
            this.CONTENA_SHURUI_FURIGANA.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_FURIGANA.RegistCheckMethod")));
            this.CONTENA_SHURUI_FURIGANA.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CONTENA_SHURUI_FURIGANA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CONTENA_SHURUI_FURIGANA.ToolTipText = "コンテナ種類のフリガナを入力してください。";
            this.CONTENA_SHURUI_FURIGANA.Width = 150;
            // 
            // CONTENA_SHURUI_BIKOU
            // 
            this.CONTENA_SHURUI_BIKOU.DataPropertyName = "CONTENA_SHURUI_BIKOU";
            this.CONTENA_SHURUI_BIKOU.DBFieldsName = "CONTENA_SHURUI_BIKOU";
            this.CONTENA_SHURUI_BIKOU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CONTENA_SHURUI_BIKOU.DefaultCellStyle = dataGridViewCellStyle6;
            this.CONTENA_SHURUI_BIKOU.FillWeight = 60F;
            this.CONTENA_SHURUI_BIKOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_BIKOU.FocusOutCheckMethod")));
            this.CONTENA_SHURUI_BIKOU.HeaderText = "備考";
            this.CONTENA_SHURUI_BIKOU.MaxInputLength = 10;
            this.CONTENA_SHURUI_BIKOU.Name = "CONTENA_SHURUI_BIKOU";
            this.CONTENA_SHURUI_BIKOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONTENA_SHURUI_BIKOU.PopupSearchSendParams")));
            this.CONTENA_SHURUI_BIKOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONTENA_SHURUI_BIKOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONTENA_SHURUI_BIKOU.popupWindowSetting")));
            this.CONTENA_SHURUI_BIKOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_BIKOU.RegistCheckMethod")));
            this.CONTENA_SHURUI_BIKOU.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CONTENA_SHURUI_BIKOU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CONTENA_SHURUI_BIKOU.ToolTipText = "備考を入力してください。";
            this.CONTENA_SHURUI_BIKOU.Width = 150;
            // 
            // TEKIYOU_BEGIN
            // 
            this.TEKIYOU_BEGIN.DataPropertyName = "TEKIYOU_BEGIN";
            this.TEKIYOU_BEGIN.DBFieldsName = "TEKIYOU_BEGIN";
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.TEKIYOU_BEGIN.DefaultCellStyle = dataGridViewCellStyle7;
            this.TEKIYOU_BEGIN.FillWeight = 60F;
            this.TEKIYOU_BEGIN.FocusOutCheckMethod = null;
            this.TEKIYOU_BEGIN.HeaderText = "適用開始日 ※";
            this.TEKIYOU_BEGIN.Name = "TEKIYOU_BEGIN";
            this.TEKIYOU_BEGIN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TEKIYOU_BEGIN.popupWindowSetting = null;
            this.TEKIYOU_BEGIN.RegistCheckMethod = null;
            this.TEKIYOU_BEGIN.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TEKIYOU_BEGIN.ToolTipText = "適用を開始する日付を入力してください。";
            this.TEKIYOU_BEGIN.Width = 105;
            // 
            // TEKIYOU_END
            // 
            this.TEKIYOU_END.DataPropertyName = "TEKIYOU_END";
            this.TEKIYOU_END.DBFieldsName = "TEKIYOU_END";
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.TEKIYOU_END.DefaultCellStyle = dataGridViewCellStyle8;
            this.TEKIYOU_END.FillWeight = 60F;
            this.TEKIYOU_END.FocusOutCheckMethod = null;
            this.TEKIYOU_END.HeaderText = "適用終了日";
            this.TEKIYOU_END.Name = "TEKIYOU_END";
            this.TEKIYOU_END.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TEKIYOU_END.popupWindowSetting = null;
            this.TEKIYOU_END.RegistCheckMethod = null;
            this.TEKIYOU_END.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.TEKIYOU_END.ToolTipText = "適用を終了する日付を入力してください。";
            this.TEKIYOU_END.Width = 90;
            // 
            // UPDATE_USER
            // 
            this.UPDATE_USER.DataPropertyName = "UPDATE_USER";
            this.UPDATE_USER.DBFieldsName = "UPDATE_USER";
            this.UPDATE_USER.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.UPDATE_USER.DefaultCellStyle = dataGridViewCellStyle9;
            this.UPDATE_USER.FillWeight = 60F;
            this.UPDATE_USER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_USER.FocusOutCheckMethod")));
            this.UPDATE_USER.HeaderText = "更新者";
            this.UPDATE_USER.Name = "UPDATE_USER";
            this.UPDATE_USER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPDATE_USER.PopupSearchSendParams")));
            this.UPDATE_USER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPDATE_USER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPDATE_USER.popupWindowSetting")));
            this.UPDATE_USER.ReadOnly = true;
            this.UPDATE_USER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_USER.RegistCheckMethod")));
            this.UPDATE_USER.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.UPDATE_USER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UPDATE_USER.Width = 125;
            // 
            // UPDATE_DATE
            // 
            this.UPDATE_DATE.DataPropertyName = "UPDATE_DATE";
            this.UPDATE_DATE.DBFieldsName = "UPDATE_DATE";
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.UPDATE_DATE.DefaultCellStyle = dataGridViewCellStyle10;
            this.UPDATE_DATE.FillWeight = 60F;
            this.UPDATE_DATE.FocusOutCheckMethod = null;
            this.UPDATE_DATE.HeaderText = "更新日";
            this.UPDATE_DATE.Name = "UPDATE_DATE";
            this.UPDATE_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPDATE_DATE.popupWindowSetting = null;
            this.UPDATE_DATE.ReadOnly = true;
            this.UPDATE_DATE.RegistCheckMethod = null;
            this.UPDATE_DATE.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.UPDATE_DATE.Width = 90;
            // 
            // CREATE_USER
            // 
            this.CREATE_USER.DataPropertyName = "CREATE_USER";
            this.CREATE_USER.DBFieldsName = "CREATE_USER";
            this.CREATE_USER.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CREATE_USER.DefaultCellStyle = dataGridViewCellStyle11;
            this.CREATE_USER.FillWeight = 60F;
            this.CREATE_USER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_USER.FocusOutCheckMethod")));
            this.CREATE_USER.HeaderText = "作成者";
            this.CREATE_USER.Name = "CREATE_USER";
            this.CREATE_USER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CREATE_USER.PopupSearchSendParams")));
            this.CREATE_USER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_USER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_USER.popupWindowSetting")));
            this.CREATE_USER.ReadOnly = true;
            this.CREATE_USER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_USER.RegistCheckMethod")));
            this.CREATE_USER.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CREATE_USER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CREATE_USER.Width = 125;
            // 
            // CREATE_DATE
            // 
            this.CREATE_DATE.DataPropertyName = "CREATE_DATE";
            this.CREATE_DATE.DBFieldsName = "CREATE_DATE";
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CREATE_DATE.DefaultCellStyle = dataGridViewCellStyle12;
            this.CREATE_DATE.FillWeight = 60F;
            this.CREATE_DATE.FocusOutCheckMethod = null;
            this.CREATE_DATE.HeaderText = "作成日";
            this.CREATE_DATE.Name = "CREATE_DATE";
            this.CREATE_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_DATE.popupWindowSetting = null;
            this.CREATE_DATE.ReadOnly = true;
            this.CREATE_DATE.RegistCheckMethod = null;
            this.CREATE_DATE.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CREATE_DATE.Width = 90;
            // 
            // CREATE_PC
            // 
            this.CREATE_PC.DataPropertyName = "CREATE_PC";
            this.CREATE_PC.HeaderText = "CREATE_PC";
            this.CREATE_PC.Name = "CREATE_PC";
            this.CREATE_PC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CREATE_PC.Visible = false;
            // 
            // DELETE_FLG
            // 
            this.DELETE_FLG.DataPropertyName = "DELETE_FLG";
            this.DELETE_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.DELETE_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DELETE_FLG.FocusOutCheckMethod")));
            this.DELETE_FLG.HeaderText = "DELETE_FLG";
            this.DELETE_FLG.Name = "DELETE_FLG";
            this.DELETE_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DELETE_FLG.PopupSearchSendParams")));
            this.DELETE_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DELETE_FLG.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DELETE_FLG.popupWindowSetting")));
            this.DELETE_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DELETE_FLG.RegistCheckMethod")));
            this.DELETE_FLG.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DELETE_FLG.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DELETE_FLG.Visible = false;
            // 
            // UPDATE_PC
            // 
            this.UPDATE_PC.DataPropertyName = "UPDATE_PC";
            this.UPDATE_PC.HeaderText = "UPDATE_PC";
            this.UPDATE_PC.Name = "UPDATE_PC";
            this.UPDATE_PC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UPDATE_PC.Visible = false;
            // 
            // CONDITION_TYPE
            // 
            this.CONDITION_TYPE.BackColor = System.Drawing.SystemColors.Window;
            this.CONDITION_TYPE.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION_TYPE.DisplayPopUp = null;
            this.CONDITION_TYPE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_TYPE.FocusOutCheckMethod")));
            this.CONDITION_TYPE.Location = new System.Drawing.Point(12, 476);
            this.CONDITION_TYPE.Name = "CONDITION_TYPE";
            this.CONDITION_TYPE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONDITION_TYPE.PopupSearchSendParams")));
            this.CONDITION_TYPE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION_TYPE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONDITION_TYPE.popupWindowSetting")));
            this.CONDITION_TYPE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_TYPE.RegistCheckMethod")));
            this.CONDITION_TYPE.Size = new System.Drawing.Size(100, 19);
            this.CONDITION_TYPE.TabIndex = 414;
            this.CONDITION_TYPE.Visible = false;
            // 
            // CONDITION_DBFIELD
            // 
            this.CONDITION_DBFIELD.BackColor = System.Drawing.SystemColors.Window;
            this.CONDITION_DBFIELD.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION_DBFIELD.DisplayPopUp = null;
            this.CONDITION_DBFIELD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_DBFIELD.FocusOutCheckMethod")));
            this.CONDITION_DBFIELD.Location = new System.Drawing.Point(118, 476);
            this.CONDITION_DBFIELD.Name = "CONDITION_DBFIELD";
            this.CONDITION_DBFIELD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONDITION_DBFIELD.PopupSearchSendParams")));
            this.CONDITION_DBFIELD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION_DBFIELD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONDITION_DBFIELD.popupWindowSetting")));
            this.CONDITION_DBFIELD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_DBFIELD.RegistCheckMethod")));
            this.CONDITION_DBFIELD.Size = new System.Drawing.Size(100, 19);
            this.CONDITION_DBFIELD.TabIndex = 414;
            this.CONDITION_DBFIELD.Visible = false;
            // 
            // ContenaShuruiHoshuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(990, 493);
            this.Controls.Add(this.CONDITION_DBFIELD);
            this.Controls.Add(this.CONDITION_TYPE);
            this.Controls.Add(this.Ichiran);
            this.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI);
            this.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_DELETED);
            this.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU);
            this.Controls.Add(this.CONDITION_VALUE);
            this.Controls.Add(this.CONDITION_ITEM);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label16);
            this.Name = "ContenaShuruiHoshuForm";
            this.Text = "ContenaShuruiHoshuForm";
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.CheckBox ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI;
        internal System.Windows.Forms.CheckBox ICHIRAN_HYOUJI_JOUKEN_DELETED;
        internal System.Windows.Forms.CheckBox ICHIRAN_HYOUJI_JOUKEN_TEKIYOU;
        internal r_framework.CustomControl.CustomTextBox CONDITION_VALUE;
        internal r_framework.CustomControl.CustomTextBox CONDITION_ITEM;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label16;
        public r_framework.CustomControl.CustomDataGridView Ichiran;
        internal r_framework.CustomControl.CustomTextBox CONDITION_TYPE;
        internal r_framework.CustomControl.CustomTextBox CONDITION_DBFIELD;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn chb_delete;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn CONTENA_SHURUI_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CONTENA_SHURUI_NAME;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CONTENA_SHURUI_NAME_RYAKU;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CONTENA_SHURUI_FURIGANA;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CONTENA_SHURUI_BIKOU;
        private r_framework.CustomControl.DgvCustomDataTimeColumn TEKIYOU_BEGIN;
        private r_framework.CustomControl.DgvCustomDataTimeColumn TEKIYOU_END;
        private r_framework.CustomControl.DgvCustomTextBoxColumn UPDATE_USER;
        private r_framework.CustomControl.DgvCustomDataTimeColumn UPDATE_DATE;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CREATE_USER;
        private r_framework.CustomControl.DgvCustomDataTimeColumn CREATE_DATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CREATE_PC;
        private r_framework.CustomControl.DgvCustomTextBoxColumn DELETE_FLG;
        private System.Windows.Forms.DataGridViewTextBoxColumn UPDATE_PC;
    }
}