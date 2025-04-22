namespace OboeGakiIkktuImputIchiran
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(M421Form));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto5 = new r_framework.Dto.RangeSettingDto();
            this.customDataGridView1 = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.DENPYOU_NUMBER = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBoxColumn();
            this.MEMO_UPDATE_DATE = new r_framework.CustomControl.DgvCustomDataTimeColumn();
            this.MEMO = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.SHOBUN_PATTERN_SYSTEM_ID = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBoxColumn();
            this.SHOBUN_PATTERN_SEQ = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBoxColumn();
            this.SHOBUN_PATTERN_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBoxColumn();
            this.LAST_SHOBUN_PATTERN_SEQ = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBoxColumn();
            this.LAST_SHOBUN_PATTERN_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.SEQ = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.SYSTEM_ID = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // customDataGridView1
            // 
            this.customDataGridView1.AllowUserToAddRows = false;
            this.customDataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.customDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.customDataGridView1.EnableHeadersVisualStyles = false;
            this.customDataGridView1.GridColor = System.Drawing.Color.White;
            this.customDataGridView1.IsReload = false;
            this.customDataGridView1.LinkedDataPanelName = null;
            this.customDataGridView1.Location = new System.Drawing.Point(2, 0);
            this.customDataGridView1.MultiSelect = false;
            this.customDataGridView1.Name = "customDataGridView1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.customDataGridView1.RowHeadersVisible = false;
            this.customDataGridView1.RowHeadersWidth = 40;
            this.customDataGridView1.RowTemplate.Height = 21;
            this.customDataGridView1.Size = new System.Drawing.Size(997, 451);
            this.customDataGridView1.TabIndex = 1;
            // 
            // DENPYOU_NUMBER
            // 
            this.DENPYOU_NUMBER.CharacterLimitList = new char[0];
            this.DENPYOU_NUMBER.DefaultBackColor = System.Drawing.Color.Empty;
            this.DENPYOU_NUMBER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_NUMBER.FocusOutCheckMethod")));
            this.DENPYOU_NUMBER.HeaderText = "伝票番号";
            this.DENPYOU_NUMBER.MinusEnableFlag = false;
            this.DENPYOU_NUMBER.Name = "DENPYOU_NUMBER";
            this.DENPYOU_NUMBER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DENPYOU_NUMBER.PopupSearchSendParams")));
            this.DENPYOU_NUMBER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DENPYOU_NUMBER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DENPYOU_NUMBER.popupWindowSetting")));
            this.DENPYOU_NUMBER.PrevText = null;
            rangeSettingDto1.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.DENPYOU_NUMBER.RangeSetting = rangeSettingDto1;
            this.DENPYOU_NUMBER.ReadOnly = true;
            this.DENPYOU_NUMBER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_NUMBER.RegistCheckMethod")));
            this.DENPYOU_NUMBER.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // MEMO_UPDATE_DATE
            // 
            this.MEMO_UPDATE_DATE.FocusOutCheckMethod = null;
            this.MEMO_UPDATE_DATE.HeaderText = "更新日";
            this.MEMO_UPDATE_DATE.Name = "MEMO_UPDATE_DATE";
            this.MEMO_UPDATE_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MEMO_UPDATE_DATE.popupWindowSetting = null;
            this.MEMO_UPDATE_DATE.ReadOnly = true;
            this.MEMO_UPDATE_DATE.RegistCheckMethod = null;
            this.MEMO_UPDATE_DATE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MEMO_UPDATE_DATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // MEMO
            // 
            this.MEMO.DefaultBackColor = System.Drawing.Color.Empty;
            this.MEMO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MEMO.FocusOutCheckMethod")));
            this.MEMO.HeaderText = "覚書内容";
            this.MEMO.Name = "MEMO";
            this.MEMO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MEMO.PopupSearchSendParams")));
            this.MEMO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MEMO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("MEMO.popupWindowSetting")));
            this.MEMO.ReadOnly = true;
            this.MEMO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MEMO.RegistCheckMethod")));
            this.MEMO.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MEMO.Width = 300;
            // 
            // SHOBUN_PATTERN_SYSTEM_ID
            // 
            this.SHOBUN_PATTERN_SYSTEM_ID.CharacterLimitList = new char[0];
            this.SHOBUN_PATTERN_SYSTEM_ID.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHOBUN_PATTERN_SYSTEM_ID.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_PATTERN_SYSTEM_ID.FocusOutCheckMethod")));
            this.SHOBUN_PATTERN_SYSTEM_ID.HeaderText = "中間処分パターンシステムID";
            this.SHOBUN_PATTERN_SYSTEM_ID.MinusEnableFlag = false;
            this.SHOBUN_PATTERN_SYSTEM_ID.Name = "SHOBUN_PATTERN_SYSTEM_ID";
            this.SHOBUN_PATTERN_SYSTEM_ID.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHOBUN_PATTERN_SYSTEM_ID.PopupSearchSendParams")));
            this.SHOBUN_PATTERN_SYSTEM_ID.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHOBUN_PATTERN_SYSTEM_ID.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHOBUN_PATTERN_SYSTEM_ID.popupWindowSetting")));
            this.SHOBUN_PATTERN_SYSTEM_ID.PrevText = null;
            rangeSettingDto2.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            rangeSettingDto2.Min = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SHOBUN_PATTERN_SYSTEM_ID.RangeSetting = rangeSettingDto2;
            this.SHOBUN_PATTERN_SYSTEM_ID.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_PATTERN_SYSTEM_ID.RegistCheckMethod")));
            this.SHOBUN_PATTERN_SYSTEM_ID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SHOBUN_PATTERN_SYSTEM_ID.Visible = false;
            // 
            // SHOBUN_PATTERN_SEQ
            // 
            this.SHOBUN_PATTERN_SEQ.CharacterLimitList = new char[0];
            this.SHOBUN_PATTERN_SEQ.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHOBUN_PATTERN_SEQ.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_PATTERN_SEQ.FocusOutCheckMethod")));
            this.SHOBUN_PATTERN_SEQ.HeaderText = "中間処分パターン枝番";
            this.SHOBUN_PATTERN_SEQ.MinusEnableFlag = false;
            this.SHOBUN_PATTERN_SEQ.Name = "SHOBUN_PATTERN_SEQ";
            this.SHOBUN_PATTERN_SEQ.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHOBUN_PATTERN_SEQ.PopupSearchSendParams")));
            this.SHOBUN_PATTERN_SEQ.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHOBUN_PATTERN_SEQ.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHOBUN_PATTERN_SEQ.popupWindowSetting")));
            this.SHOBUN_PATTERN_SEQ.PrevText = null;
            rangeSettingDto3.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            rangeSettingDto3.Min = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SHOBUN_PATTERN_SEQ.RangeSetting = rangeSettingDto3;
            this.SHOBUN_PATTERN_SEQ.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_PATTERN_SEQ.RegistCheckMethod")));
            this.SHOBUN_PATTERN_SEQ.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SHOBUN_PATTERN_SEQ.Visible = false;
            // 
            // SHOBUN_PATTERN_NAME
            // 
            this.SHOBUN_PATTERN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHOBUN_PATTERN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_PATTERN_NAME.FocusOutCheckMethod")));
            this.SHOBUN_PATTERN_NAME.HeaderText = "中間処分パターン名";
            this.SHOBUN_PATTERN_NAME.Name = "SHOBUN_PATTERN_NAME";
            this.SHOBUN_PATTERN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHOBUN_PATTERN_NAME.PopupSearchSendParams")));
            this.SHOBUN_PATTERN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHOBUN_PATTERN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHOBUN_PATTERN_NAME.popupWindowSetting")));
            this.SHOBUN_PATTERN_NAME.ReadOnly = true;
            this.SHOBUN_PATTERN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_PATTERN_NAME.RegistCheckMethod")));
            this.SHOBUN_PATTERN_NAME.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SHOBUN_PATTERN_NAME.Width = 240;
            // 
            // LAST_SHOBUN_PATTERN_SYSTEM_ID
            // 
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID.CharacterLimitList = new char[0];
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID.DefaultBackColor = System.Drawing.Color.Empty;
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LAST_SHOBUN_PATTERN_SYSTEM_ID.FocusOutCheckMethod")));
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID.HeaderText = "最終処分パターンシステムID";
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID.MinusEnableFlag = false;
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID.Name = "LAST_SHOBUN_PATTERN_SYSTEM_ID";
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("LAST_SHOBUN_PATTERN_SYSTEM_ID.PopupSearchSendParams")));
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("LAST_SHOBUN_PATTERN_SYSTEM_ID.popupWindowSetting")));
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID.PrevText = null;
            rangeSettingDto4.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            rangeSettingDto4.Min = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID.RangeSetting = rangeSettingDto4;
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LAST_SHOBUN_PATTERN_SYSTEM_ID.RegistCheckMethod")));
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LAST_SHOBUN_PATTERN_SYSTEM_ID.Visible = false;
            // 
            // LAST_SHOBUN_PATTERN_SEQ
            // 
            this.LAST_SHOBUN_PATTERN_SEQ.CharacterLimitList = new char[0];
            this.LAST_SHOBUN_PATTERN_SEQ.DefaultBackColor = System.Drawing.Color.Empty;
            this.LAST_SHOBUN_PATTERN_SEQ.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LAST_SHOBUN_PATTERN_SEQ.FocusOutCheckMethod")));
            this.LAST_SHOBUN_PATTERN_SEQ.HeaderText = "最終処分パターン枝番";
            this.LAST_SHOBUN_PATTERN_SEQ.MinusEnableFlag = false;
            this.LAST_SHOBUN_PATTERN_SEQ.Name = "LAST_SHOBUN_PATTERN_SEQ";
            this.LAST_SHOBUN_PATTERN_SEQ.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("LAST_SHOBUN_PATTERN_SEQ.PopupSearchSendParams")));
            this.LAST_SHOBUN_PATTERN_SEQ.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.LAST_SHOBUN_PATTERN_SEQ.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("LAST_SHOBUN_PATTERN_SEQ.popupWindowSetting")));
            this.LAST_SHOBUN_PATTERN_SEQ.PrevText = null;
            rangeSettingDto5.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            rangeSettingDto5.Min = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.LAST_SHOBUN_PATTERN_SEQ.RangeSetting = rangeSettingDto5;
            this.LAST_SHOBUN_PATTERN_SEQ.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LAST_SHOBUN_PATTERN_SEQ.RegistCheckMethod")));
            this.LAST_SHOBUN_PATTERN_SEQ.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LAST_SHOBUN_PATTERN_SEQ.Visible = false;
            // 
            // LAST_SHOBUN_PATTERN_NAME
            // 
            this.LAST_SHOBUN_PATTERN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.LAST_SHOBUN_PATTERN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LAST_SHOBUN_PATTERN_NAME.FocusOutCheckMethod")));
            this.LAST_SHOBUN_PATTERN_NAME.HeaderText = "最終処分パターン名";
            this.LAST_SHOBUN_PATTERN_NAME.Name = "LAST_SHOBUN_PATTERN_NAME";
            this.LAST_SHOBUN_PATTERN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("LAST_SHOBUN_PATTERN_NAME.PopupSearchSendParams")));
            this.LAST_SHOBUN_PATTERN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.LAST_SHOBUN_PATTERN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("LAST_SHOBUN_PATTERN_NAME.popupWindowSetting")));
            this.LAST_SHOBUN_PATTERN_NAME.ReadOnly = true;
            this.LAST_SHOBUN_PATTERN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LAST_SHOBUN_PATTERN_NAME.RegistCheckMethod")));
            this.LAST_SHOBUN_PATTERN_NAME.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LAST_SHOBUN_PATTERN_NAME.Width = 240;
            // 
            // SEQ
            // 
            this.SEQ.DefaultBackColor = System.Drawing.Color.Empty;
            this.SEQ.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEQ.FocusOutCheckMethod")));
            this.SEQ.HeaderText = "SEQ";
            this.SEQ.Name = "SEQ";
            this.SEQ.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEQ.PopupSearchSendParams")));
            this.SEQ.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SEQ.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEQ.popupWindowSetting")));
            this.SEQ.ReadOnly = true;
            this.SEQ.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEQ.RegistCheckMethod")));
            this.SEQ.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SEQ.Visible = false;
            // 
            // SYSTEM_ID
            // 
            this.SYSTEM_ID.DefaultBackColor = System.Drawing.Color.Empty;
            this.SYSTEM_ID.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SYSTEM_ID.FocusOutCheckMethod")));
            this.SYSTEM_ID.HeaderText = "システムID";
            this.SYSTEM_ID.Name = "SYSTEM_ID";
            this.SYSTEM_ID.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SYSTEM_ID.PopupSearchSendParams")));
            this.SYSTEM_ID.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SYSTEM_ID.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SYSTEM_ID.popupWindowSetting")));
            this.SYSTEM_ID.ReadOnly = true;
            this.SYSTEM_ID.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SYSTEM_ID.RegistCheckMethod")));
            this.SYSTEM_ID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SYSTEM_ID.Visible = false;
            // 
            // M421Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(238)))), ((int)(((byte)(253)))));
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.customDataGridView1);
            this.Location = new System.Drawing.Point(10, 10);
            this.Name = "M421Form";
            this.Text = "M421Form";
            this.WindowId = r_framework.Const.WINDOW_ID.M_OBOE_IKKATSU_ICHIRAN;
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public r_framework.CustomControl.CustomDataGridView customDataGridView1;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBoxColumn DENPYOU_NUMBER;
        private r_framework.CustomControl.DgvCustomDataTimeColumn MEMO_UPDATE_DATE;
        private r_framework.CustomControl.DgvCustomTextBoxColumn MEMO;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBoxColumn SHOBUN_PATTERN_SYSTEM_ID;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBoxColumn SHOBUN_PATTERN_SEQ;
        private r_framework.CustomControl.DgvCustomTextBoxColumn SHOBUN_PATTERN_NAME;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBoxColumn LAST_SHOBUN_PATTERN_SYSTEM_ID;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBoxColumn LAST_SHOBUN_PATTERN_SEQ;
        private r_framework.CustomControl.DgvCustomTextBoxColumn LAST_SHOBUN_PATTERN_NAME;
        private r_framework.CustomControl.DgvCustomTextBoxColumn SEQ;
        private r_framework.CustomControl.DgvCustomTextBoxColumn SYSTEM_ID;
    }
}