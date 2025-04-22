namespace Shougun.Core.Master.OboeGakiIkkatuIchiran
{
    partial class M421Form
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(M421Form));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto5 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Ichiran = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.DENPYOU_NUMBER = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.MEMO_UPDATE_DATE = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.MEMO = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.SHOBUN_PATTERN_SYSTEM_ID = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.SHOBUN_PATTERN_SEQ = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.SHOBUN_PATTERN_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.LAST_SHOBUN_PATTERN_SEQ = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.LAST_SHOBUN_PATTERN_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.SEQ = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.SYSTEM_ID = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).BeginInit();
            this.SuspendLayout();
            // 
            // Ichiran
            // 
            this.Ichiran.AllowUserToAddRows = false;
            this.Ichiran.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Ichiran.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Ichiran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Ichiran.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DENPYOU_NUMBER,
            this.MEMO_UPDATE_DATE,
            this.MEMO,
            this.SHOBUN_PATTERN_SYSTEM_ID,
            this.SHOBUN_PATTERN_SEQ,
            this.SHOBUN_PATTERN_NAME,
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID,
            this.LAST_SHOBUN_PATTERN_SEQ,
            this.LAST_SHOBUN_PATTERN_NAME,
            this.SEQ,
            this.SYSTEM_ID});
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Ichiran.DefaultCellStyle = dataGridViewCellStyle13;
            this.Ichiran.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.Ichiran.EnableHeadersVisualStyles = false;
            this.Ichiran.GridColor = System.Drawing.Color.White;
            this.Ichiran.IsReload = false;
            this.Ichiran.LinkedDataPanelName = null;
            this.Ichiran.Location = new System.Drawing.Point(2, 0);
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
            this.Ichiran.RowHeadersWidth = 40;
            this.Ichiran.RowTemplate.Height = 21;
            this.Ichiran.ShowCellToolTips = false;
            this.Ichiran.Size = new System.Drawing.Size(997, 451);
            this.Ichiran.TabIndex = 1;
            // 
            // DENPYOU_NUMBER
            // 
            this.DENPYOU_NUMBER.DataPropertyName = "DENPYOU_NUMBER";
            this.DENPYOU_NUMBER.DBFieldsName = "DENPYOU_NUMBER";
            this.DENPYOU_NUMBER.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.DENPYOU_NUMBER.DefaultCellStyle = dataGridViewCellStyle2;
            this.DENPYOU_NUMBER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_NUMBER.FocusOutCheckMethod")));
            this.DENPYOU_NUMBER.HeaderText = "伝票番号";
            this.DENPYOU_NUMBER.Name = "DENPYOU_NUMBER";
            this.DENPYOU_NUMBER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DENPYOU_NUMBER.PopupSearchSendParams")));
            this.DENPYOU_NUMBER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DENPYOU_NUMBER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DENPYOU_NUMBER.popupWindowSetting")));
            this.DENPYOU_NUMBER.RangeSetting = rangeSettingDto1;
            this.DENPYOU_NUMBER.ReadOnly = true;
            this.DENPYOU_NUMBER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_NUMBER.RegistCheckMethod")));
            this.DENPYOU_NUMBER.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DENPYOU_NUMBER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DENPYOU_NUMBER.Width = 85;
            // 
            // MEMO_UPDATE_DATE
            // 
            this.MEMO_UPDATE_DATE.DataPropertyName = "MEMO_UPDATE_DATE";
            this.MEMO_UPDATE_DATE.DBFieldsName = "MEMO_UPDATE_DATE";
            this.MEMO_UPDATE_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.MEMO_UPDATE_DATE.DefaultCellStyle = dataGridViewCellStyle3;
            this.MEMO_UPDATE_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MEMO_UPDATE_DATE.FocusOutCheckMethod")));
            this.MEMO_UPDATE_DATE.HeaderText = "更新日";
            this.MEMO_UPDATE_DATE.Name = "MEMO_UPDATE_DATE";
            this.MEMO_UPDATE_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MEMO_UPDATE_DATE.PopupSearchSendParams")));
            this.MEMO_UPDATE_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MEMO_UPDATE_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("MEMO_UPDATE_DATE.popupWindowSetting")));
            this.MEMO_UPDATE_DATE.ReadOnly = true;
            this.MEMO_UPDATE_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MEMO_UPDATE_DATE.RegistCheckMethod")));
            this.MEMO_UPDATE_DATE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MEMO_UPDATE_DATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MEMO_UPDATE_DATE.Width = 85;
            // 
            // MEMO
            // 
            this.MEMO.DataPropertyName = "MEMO";
            this.MEMO.DBFieldsName = "MEMO";
            this.MEMO.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.MEMO.DefaultCellStyle = dataGridViewCellStyle4;
            this.MEMO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MEMO.FocusOutCheckMethod")));
            this.MEMO.HeaderText = "覚書内容";
            this.MEMO.Name = "MEMO";
            this.MEMO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MEMO.PopupSearchSendParams")));
            this.MEMO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MEMO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("MEMO.popupWindowSetting")));
            this.MEMO.ReadOnly = true;
            this.MEMO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MEMO.RegistCheckMethod")));
            this.MEMO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MEMO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MEMO.Width = 300;
            // 
            // SHOBUN_PATTERN_SYSTEM_ID
            // 
            this.SHOBUN_PATTERN_SYSTEM_ID.DataPropertyName = "SHOBUN_PATTERN_SYSTEM_ID";
            this.SHOBUN_PATTERN_SYSTEM_ID.DBFieldsName = "SHOBUN_PATTERN_SYSTEM_ID";
            this.SHOBUN_PATTERN_SYSTEM_ID.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.SHOBUN_PATTERN_SYSTEM_ID.DefaultCellStyle = dataGridViewCellStyle5;
            this.SHOBUN_PATTERN_SYSTEM_ID.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_PATTERN_SYSTEM_ID.FocusOutCheckMethod")));
            this.SHOBUN_PATTERN_SYSTEM_ID.HeaderText = "中間処分パターンシステムID";
            this.SHOBUN_PATTERN_SYSTEM_ID.Name = "SHOBUN_PATTERN_SYSTEM_ID";
            this.SHOBUN_PATTERN_SYSTEM_ID.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHOBUN_PATTERN_SYSTEM_ID.PopupSearchSendParams")));
            this.SHOBUN_PATTERN_SYSTEM_ID.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHOBUN_PATTERN_SYSTEM_ID.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHOBUN_PATTERN_SYSTEM_ID.popupWindowSetting")));
            this.SHOBUN_PATTERN_SYSTEM_ID.RangeSetting = rangeSettingDto2;
            this.SHOBUN_PATTERN_SYSTEM_ID.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_PATTERN_SYSTEM_ID.RegistCheckMethod")));
            this.SHOBUN_PATTERN_SYSTEM_ID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SHOBUN_PATTERN_SYSTEM_ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SHOBUN_PATTERN_SYSTEM_ID.Visible = false;
            // 
            // SHOBUN_PATTERN_SEQ
            // 
            this.SHOBUN_PATTERN_SEQ.DataPropertyName = "SHOBUN_PATTERN_SEQ";
            this.SHOBUN_PATTERN_SEQ.DBFieldsName = "SHOBUN_PATTERN_SEQ";
            this.SHOBUN_PATTERN_SEQ.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.SHOBUN_PATTERN_SEQ.DefaultCellStyle = dataGridViewCellStyle6;
            this.SHOBUN_PATTERN_SEQ.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_PATTERN_SEQ.FocusOutCheckMethod")));
            this.SHOBUN_PATTERN_SEQ.HeaderText = "中間処分パターン枝番";
            this.SHOBUN_PATTERN_SEQ.Name = "SHOBUN_PATTERN_SEQ";
            this.SHOBUN_PATTERN_SEQ.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHOBUN_PATTERN_SEQ.PopupSearchSendParams")));
            this.SHOBUN_PATTERN_SEQ.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHOBUN_PATTERN_SEQ.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHOBUN_PATTERN_SEQ.popupWindowSetting")));
            this.SHOBUN_PATTERN_SEQ.RangeSetting = rangeSettingDto3;
            this.SHOBUN_PATTERN_SEQ.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_PATTERN_SEQ.RegistCheckMethod")));
            this.SHOBUN_PATTERN_SEQ.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SHOBUN_PATTERN_SEQ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SHOBUN_PATTERN_SEQ.Visible = false;
            // 
            // SHOBUN_PATTERN_NAME
            // 
            this.SHOBUN_PATTERN_NAME.DataPropertyName = "SHOBUN_PATTERN_NAME";
            this.SHOBUN_PATTERN_NAME.DBFieldsName = "SHOBUN_PATTERN_NAME";
            this.SHOBUN_PATTERN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.SHOBUN_PATTERN_NAME.DefaultCellStyle = dataGridViewCellStyle7;
            this.SHOBUN_PATTERN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_PATTERN_NAME.FocusOutCheckMethod")));
            this.SHOBUN_PATTERN_NAME.HeaderText = "中間処分パターン名";
            this.SHOBUN_PATTERN_NAME.Name = "SHOBUN_PATTERN_NAME";
            this.SHOBUN_PATTERN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHOBUN_PATTERN_NAME.PopupSearchSendParams")));
            this.SHOBUN_PATTERN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHOBUN_PATTERN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHOBUN_PATTERN_NAME.popupWindowSetting")));
            this.SHOBUN_PATTERN_NAME.ReadOnly = true;
            this.SHOBUN_PATTERN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_PATTERN_NAME.RegistCheckMethod")));
            this.SHOBUN_PATTERN_NAME.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SHOBUN_PATTERN_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SHOBUN_PATTERN_NAME.Width = 250;
            // 
            // LAST_SHOBUN_PATTERN_SYSTEM_ID
            // 
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID.DataPropertyName = "LAST_SHOBUN_PATTERN_SYSTEM_ID";
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID.DBFieldsName = "LAST_SHOBUN_PATTERN_SYSTEM_ID";
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID.DefaultCellStyle = dataGridViewCellStyle8;
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LAST_SHOBUN_PATTERN_SYSTEM_ID.FocusOutCheckMethod")));
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID.HeaderText = "最終処分パターンシステムID";
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID.Name = "LAST_SHOBUN_PATTERN_SYSTEM_ID";
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("LAST_SHOBUN_PATTERN_SYSTEM_ID.PopupSearchSendParams")));
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("LAST_SHOBUN_PATTERN_SYSTEM_ID.popupWindowSetting")));
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID.RangeSetting = rangeSettingDto4;
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LAST_SHOBUN_PATTERN_SYSTEM_ID.RegistCheckMethod")));
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID.Visible = false;
            // 
            // LAST_SHOBUN_PATTERN_SEQ
            // 
            this.LAST_SHOBUN_PATTERN_SEQ.DataPropertyName = "LAST_SHOBUN_PATTERN_SEQ";
            this.LAST_SHOBUN_PATTERN_SEQ.DBFieldsName = "LAST_SHOBUN_PATTERN_SEQ";
            this.LAST_SHOBUN_PATTERN_SEQ.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            this.LAST_SHOBUN_PATTERN_SEQ.DefaultCellStyle = dataGridViewCellStyle9;
            this.LAST_SHOBUN_PATTERN_SEQ.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LAST_SHOBUN_PATTERN_SEQ.FocusOutCheckMethod")));
            this.LAST_SHOBUN_PATTERN_SEQ.HeaderText = "最終処分パターン枝番";
            this.LAST_SHOBUN_PATTERN_SEQ.Name = "LAST_SHOBUN_PATTERN_SEQ";
            this.LAST_SHOBUN_PATTERN_SEQ.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("LAST_SHOBUN_PATTERN_SEQ.PopupSearchSendParams")));
            this.LAST_SHOBUN_PATTERN_SEQ.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.LAST_SHOBUN_PATTERN_SEQ.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("LAST_SHOBUN_PATTERN_SEQ.popupWindowSetting")));
            this.LAST_SHOBUN_PATTERN_SEQ.RangeSetting = rangeSettingDto5;
            this.LAST_SHOBUN_PATTERN_SEQ.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LAST_SHOBUN_PATTERN_SEQ.RegistCheckMethod")));
            this.LAST_SHOBUN_PATTERN_SEQ.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LAST_SHOBUN_PATTERN_SEQ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LAST_SHOBUN_PATTERN_SEQ.Visible = false;
            // 
            // LAST_SHOBUN_PATTERN_NAME
            // 
            this.LAST_SHOBUN_PATTERN_NAME.DataPropertyName = "LAST_SHOBUN_PATTERN_NAME";
            this.LAST_SHOBUN_PATTERN_NAME.DBFieldsName = "LAST_SHOBUN_PATTERN_NAME";
            this.LAST_SHOBUN_PATTERN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            this.LAST_SHOBUN_PATTERN_NAME.DefaultCellStyle = dataGridViewCellStyle10;
            this.LAST_SHOBUN_PATTERN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LAST_SHOBUN_PATTERN_NAME.FocusOutCheckMethod")));
            this.LAST_SHOBUN_PATTERN_NAME.HeaderText = "最終処分パターン名";
            this.LAST_SHOBUN_PATTERN_NAME.Name = "LAST_SHOBUN_PATTERN_NAME";
            this.LAST_SHOBUN_PATTERN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("LAST_SHOBUN_PATTERN_NAME.PopupSearchSendParams")));
            this.LAST_SHOBUN_PATTERN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.LAST_SHOBUN_PATTERN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("LAST_SHOBUN_PATTERN_NAME.popupWindowSetting")));
            this.LAST_SHOBUN_PATTERN_NAME.ReadOnly = true;
            this.LAST_SHOBUN_PATTERN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LAST_SHOBUN_PATTERN_NAME.RegistCheckMethod")));
            this.LAST_SHOBUN_PATTERN_NAME.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LAST_SHOBUN_PATTERN_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LAST_SHOBUN_PATTERN_NAME.Width = 250;
            // 
            // SEQ
            // 
            this.SEQ.DataPropertyName = "SEQ";
            this.SEQ.DBFieldsName = "SEQ";
            this.SEQ.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            this.SEQ.DefaultCellStyle = dataGridViewCellStyle11;
            this.SEQ.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEQ.FocusOutCheckMethod")));
            this.SEQ.HeaderText = "SEQ";
            this.SEQ.Name = "SEQ";
            this.SEQ.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEQ.PopupSearchSendParams")));
            this.SEQ.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SEQ.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEQ.popupWindowSetting")));
            this.SEQ.ReadOnly = true;
            this.SEQ.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEQ.RegistCheckMethod")));
            this.SEQ.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SEQ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SEQ.Visible = false;
            // 
            // SYSTEM_ID
            // 
            this.SYSTEM_ID.DataPropertyName = "SYSTEM_ID";
            this.SYSTEM_ID.DBFieldsName = "SYSTEM_ID";
            this.SYSTEM_ID.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            this.SYSTEM_ID.DefaultCellStyle = dataGridViewCellStyle12;
            this.SYSTEM_ID.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SYSTEM_ID.FocusOutCheckMethod")));
            this.SYSTEM_ID.HeaderText = "システムID";
            this.SYSTEM_ID.Name = "SYSTEM_ID";
            this.SYSTEM_ID.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SYSTEM_ID.PopupSearchSendParams")));
            this.SYSTEM_ID.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SYSTEM_ID.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SYSTEM_ID.popupWindowSetting")));
            this.SYSTEM_ID.ReadOnly = true;
            this.SYSTEM_ID.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SYSTEM_ID.RegistCheckMethod")));
            this.SYSTEM_ID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SYSTEM_ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SYSTEM_ID.Visible = false;
            // 
            // M421Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.Ichiran);
            this.Location = new System.Drawing.Point(10, 10);
            this.Name = "M421Form";
            this.Text = "M421Form";
            this.WindowId = r_framework.Const.WINDOW_ID.M_OBOE_IKKATSU_ICHIRAN;
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public r_framework.CustomControl.CustomDataGridView Ichiran;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column DENPYOU_NUMBER;
        private r_framework.CustomControl.DgvCustomTextBoxColumn MEMO_UPDATE_DATE;
        private r_framework.CustomControl.DgvCustomTextBoxColumn MEMO;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column SHOBUN_PATTERN_SYSTEM_ID;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column SHOBUN_PATTERN_SEQ;
        private r_framework.CustomControl.DgvCustomTextBoxColumn SHOBUN_PATTERN_NAME;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column LAST_SHOBUN_PATTERN_SYSTEM_ID;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column LAST_SHOBUN_PATTERN_SEQ;
        private r_framework.CustomControl.DgvCustomTextBoxColumn LAST_SHOBUN_PATTERN_NAME;
        private r_framework.CustomControl.DgvCustomTextBoxColumn SEQ;
        private r_framework.CustomControl.DgvCustomTextBoxColumn SYSTEM_ID;
    }
}