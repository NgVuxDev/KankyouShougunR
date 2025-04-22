namespace Shougun.Core.Common.HanyoCSVShutsuryoku.APP
{
    partial class PatternForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatternForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto5 = new r_framework.Dto.RangeSettingDto();
            this.lblPatternNm = new System.Windows.Forms.Label();
            this.txtPatternNm = new r_framework.CustomControl.CustomTextBox();
            this.txtPatternBikou = new r_framework.CustomControl.CustomTextBox();
            this.lblPatternBikou = new System.Windows.Forms.Label();
            this.pnlPatternDetail = new System.Windows.Forms.Panel();
            this.dgvSelect = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.dgcOutputKbnSelect = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.dgcIdSelect = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.dgcDispNameSelect = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.btnCondition = new r_framework.CustomControl.CustomButton();
            this.dgvOutput = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.dgcOutputKbnOutput = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.dgcIdOutput = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.dgcDispNameOutput = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.txtCondition = new r_framework.CustomControl.CustomTextBox();
            this.lblCondition = new System.Windows.Forms.Label();
            this.pnlOutputKbn = new System.Windows.Forms.Panel();
            this.txtOutputKbn = new r_framework.CustomControl.CustomNumericTextBox2();
            this.rdoOutputKbn2 = new r_framework.CustomControl.CustomRadioButton();
            this.rdoOutputKbn1 = new r_framework.CustomControl.CustomRadioButton();
            this.lblOutput = new System.Windows.Forms.Label();
            this.btnDown = new r_framework.CustomControl.CustomButton();
            this.btnUp = new r_framework.CustomControl.CustomButton();
            this.btnAdd = new r_framework.CustomControl.CustomButton();
            this.btnRemove = new r_framework.CustomControl.CustomButton();
            this.lblSelect = new System.Windows.Forms.Label();
            this.lblOutputKbn = new System.Windows.Forms.Label();
            this.pnlPatternDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutput)).BeginInit();
            this.pnlOutputKbn.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPatternNm
            // 
            this.lblPatternNm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblPatternNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPatternNm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPatternNm.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblPatternNm.ForeColor = System.Drawing.Color.White;
            this.lblPatternNm.Location = new System.Drawing.Point(0, 439);
            this.lblPatternNm.Name = "lblPatternNm";
            this.lblPatternNm.Size = new System.Drawing.Size(110, 20);
            this.lblPatternNm.TabIndex = 450;
            this.lblPatternNm.Text = "パターン名※";
            this.lblPatternNm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPatternNm
            // 
            this.txtPatternNm.BackColor = System.Drawing.SystemColors.Window;
            this.txtPatternNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPatternNm.CharactersNumber = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.txtPatternNm.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtPatternNm.DisplayItemName = "パターン名";
            this.txtPatternNm.DisplayPopUp = null;
            this.txtPatternNm.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtPatternNm.FocusOutCheckMethod")));
            this.txtPatternNm.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtPatternNm.ForeColor = System.Drawing.Color.Black;
            this.txtPatternNm.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.txtPatternNm.IsInputErrorOccured = false;
            this.txtPatternNm.Location = new System.Drawing.Point(115, 439);
            this.txtPatternNm.MaxLength = 60;
            this.txtPatternNm.Name = "txtPatternNm";
            this.txtPatternNm.PopupAfterExecute = null;
            this.txtPatternNm.PopupBeforeExecute = null;
            this.txtPatternNm.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtPatternNm.PopupSearchSendParams")));
            this.txtPatternNm.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtPatternNm.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtPatternNm.popupWindowSetting")));
            this.txtPatternNm.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtPatternNm.RegistCheckMethod")));
            this.txtPatternNm.ShortItemName = "パターン名";
            this.txtPatternNm.Size = new System.Drawing.Size(465, 20);
            this.txtPatternNm.TabIndex = 460;
            this.txtPatternNm.Tag = "パターン名を設定します";
            // 
            // txtPatternBikou
            // 
            this.txtPatternBikou.BackColor = System.Drawing.SystemColors.Window;
            this.txtPatternBikou.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPatternBikou.CharactersNumber = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.txtPatternBikou.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtPatternBikou.DisplayItemName = "パターン備考";
            this.txtPatternBikou.DisplayPopUp = null;
            this.txtPatternBikou.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtPatternBikou.FocusOutCheckMethod")));
            this.txtPatternBikou.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtPatternBikou.ForeColor = System.Drawing.Color.Black;
            this.txtPatternBikou.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.txtPatternBikou.IsInputErrorOccured = false;
            this.txtPatternBikou.Location = new System.Drawing.Point(115, 461);
            this.txtPatternBikou.MaxLength = 60;
            this.txtPatternBikou.Name = "txtPatternBikou";
            this.txtPatternBikou.PopupAfterExecute = null;
            this.txtPatternBikou.PopupBeforeExecute = null;
            this.txtPatternBikou.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtPatternBikou.PopupSearchSendParams")));
            this.txtPatternBikou.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtPatternBikou.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtPatternBikou.popupWindowSetting")));
            this.txtPatternBikou.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtPatternBikou.RegistCheckMethod")));
            this.txtPatternBikou.ShortItemName = "パターン備考";
            this.txtPatternBikou.Size = new System.Drawing.Size(465, 20);
            this.txtPatternBikou.TabIndex = 480;
            this.txtPatternBikou.Tag = "パターン備考を設定します";
            // 
            // lblPatternBikou
            // 
            this.lblPatternBikou.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblPatternBikou.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPatternBikou.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblPatternBikou.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblPatternBikou.ForeColor = System.Drawing.Color.White;
            this.lblPatternBikou.Location = new System.Drawing.Point(0, 461);
            this.lblPatternBikou.Name = "lblPatternBikou";
            this.lblPatternBikou.Size = new System.Drawing.Size(110, 20);
            this.lblPatternBikou.TabIndex = 470;
            this.lblPatternBikou.Text = "パターン備考";
            this.lblPatternBikou.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlPatternDetail
            // 
            this.pnlPatternDetail.Controls.Add(this.dgvSelect);
            this.pnlPatternDetail.Controls.Add(this.btnCondition);
            this.pnlPatternDetail.Controls.Add(this.dgvOutput);
            this.pnlPatternDetail.Controls.Add(this.txtCondition);
            this.pnlPatternDetail.Controls.Add(this.lblCondition);
            this.pnlPatternDetail.Controls.Add(this.pnlOutputKbn);
            this.pnlPatternDetail.Controls.Add(this.lblOutput);
            this.pnlPatternDetail.Controls.Add(this.btnDown);
            this.pnlPatternDetail.Controls.Add(this.btnUp);
            this.pnlPatternDetail.Controls.Add(this.btnAdd);
            this.pnlPatternDetail.Controls.Add(this.btnRemove);
            this.pnlPatternDetail.Controls.Add(this.lblSelect);
            this.pnlPatternDetail.Controls.Add(this.lblOutputKbn);
            this.pnlPatternDetail.Location = new System.Drawing.Point(0, 0);
            this.pnlPatternDetail.Margin = new System.Windows.Forms.Padding(0);
            this.pnlPatternDetail.MinimumSize = new System.Drawing.Size(0, 216);
            this.pnlPatternDetail.Name = "pnlPatternDetail";
            this.pnlPatternDetail.Size = new System.Drawing.Size(1000, 436);
            this.pnlPatternDetail.TabIndex = 299;
            // 
            // dgvSelect
            // 
            this.dgvSelect.AllowUserToAddRows = false;
            this.dgvSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSelect.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSelect.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSelect.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSelect.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcOutputKbnSelect,
            this.dgcIdSelect,
            this.dgcDispNameSelect});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSelect.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvSelect.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvSelect.EnableHeadersVisualStyles = false;
            this.dgvSelect.GridColor = System.Drawing.Color.White;
            this.dgvSelect.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dgvSelect.IsBrowsePurpose = true;
            this.dgvSelect.IsReload = false;
            this.dgvSelect.LinkedDataPanelName = null;
            this.dgvSelect.Location = new System.Drawing.Point(0, 66);
            this.dgvSelect.MultiSelect = false;
            this.dgvSelect.Name = "dgvSelect";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSelect.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvSelect.RowHeadersVisible = false;
            this.dgvSelect.RowTemplate.Height = 21;
            this.dgvSelect.ShowCellToolTips = false;
            this.dgvSelect.Size = new System.Drawing.Size(420, 370);
            this.dgvSelect.TabIndex = 380;
            // 
            // dgcOutputKbnSelect
            // 
            this.dgcOutputKbnSelect.DataPropertyName = "OUTPUT_KBN";
            this.dgcOutputKbnSelect.DBFieldsName = "OUTPUT_KBN";
            this.dgcOutputKbnSelect.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.dgcOutputKbnSelect.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgcOutputKbnSelect.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgcOutputKbnSelect.FocusOutCheckMethod")));
            this.dgcOutputKbnSelect.HeaderText = "出力区分";
            this.dgcOutputKbnSelect.Name = "dgcOutputKbnSelect";
            this.dgcOutputKbnSelect.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgcOutputKbnSelect.PopupSearchSendParams")));
            this.dgcOutputKbnSelect.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgcOutputKbnSelect.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgcOutputKbnSelect.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.dgcOutputKbnSelect.RangeSetting = rangeSettingDto1;
            this.dgcOutputKbnSelect.ReadOnly = true;
            this.dgcOutputKbnSelect.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgcOutputKbnSelect.RegistCheckMethod")));
            this.dgcOutputKbnSelect.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgcOutputKbnSelect.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgcOutputKbnSelect.Visible = false;
            this.dgcOutputKbnSelect.Width = 5;
            // 
            // dgcIdSelect
            // 
            this.dgcIdSelect.DataPropertyName = "ID";
            this.dgcIdSelect.DBFieldsName = "ID";
            this.dgcIdSelect.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.dgcIdSelect.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgcIdSelect.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgcIdSelect.FocusOutCheckMethod")));
            this.dgcIdSelect.HeaderText = "項目ID";
            this.dgcIdSelect.Name = "dgcIdSelect";
            this.dgcIdSelect.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgcIdSelect.PopupSearchSendParams")));
            this.dgcIdSelect.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgcIdSelect.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgcIdSelect.popupWindowSetting")));
            this.dgcIdSelect.RangeSetting = rangeSettingDto2;
            this.dgcIdSelect.ReadOnly = true;
            this.dgcIdSelect.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgcIdSelect.RegistCheckMethod")));
            this.dgcIdSelect.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgcIdSelect.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgcIdSelect.Visible = false;
            this.dgcIdSelect.Width = 5;
            // 
            // dgcDispNameSelect
            // 
            this.dgcDispNameSelect.DataPropertyName = "DISP_NAME";
            this.dgcDispNameSelect.DBFieldsName = "DISP_NAME";
            this.dgcDispNameSelect.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.dgcDispNameSelect.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgcDispNameSelect.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgcDispNameSelect.FocusOutCheckMethod")));
            this.dgcDispNameSelect.HeaderText = "項目";
            this.dgcDispNameSelect.Name = "dgcDispNameSelect";
            this.dgcDispNameSelect.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgcDispNameSelect.PopupSearchSendParams")));
            this.dgcDispNameSelect.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgcDispNameSelect.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgcDispNameSelect.popupWindowSetting")));
            this.dgcDispNameSelect.ReadOnly = true;
            this.dgcDispNameSelect.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgcDispNameSelect.RegistCheckMethod")));
            this.dgcDispNameSelect.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgcDispNameSelect.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgcDispNameSelect.Width = 400;
            // 
            // btnCondition
            // 
            this.btnCondition.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnCondition.Location = new System.Drawing.Point(426, 21);
            this.btnCondition.Name = "btnCondition";
            this.btnCondition.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btnCondition.Size = new System.Drawing.Size(76, 23);
            this.btnCondition.TabIndex = 360;
            this.btnCondition.TabStop = false;
            this.btnCondition.Text = "項目検索";
            this.btnCondition.UseVisualStyleBackColor = true;
            this.btnCondition.Click += new System.EventHandler(this.bt_func8_Click);
            // 
            // dgvOutput
            // 
            this.dgvOutput.AllowUserToAddRows = false;
            this.dgvOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvOutput.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOutput.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOutput.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcOutputKbnOutput,
            this.dgcIdOutput,
            this.dgcDispNameOutput});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvOutput.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgvOutput.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvOutput.EnableHeadersVisualStyles = false;
            this.dgvOutput.GridColor = System.Drawing.Color.White;
            this.dgvOutput.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dgvOutput.IsBrowsePurpose = true;
            this.dgvOutput.IsReload = false;
            this.dgvOutput.LinkedDataPanelName = null;
            this.dgvOutput.Location = new System.Drawing.Point(580, 66);
            this.dgvOutput.MultiSelect = false;
            this.dgvOutput.Name = "dgvOutput";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOutput.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvOutput.RowHeadersVisible = false;
            this.dgvOutput.RowTemplate.Height = 21;
            this.dgvOutput.ShowCellToolTips = false;
            this.dgvOutput.Size = new System.Drawing.Size(420, 370);
            this.dgvOutput.TabIndex = 400;
            // 
            // dgcOutputKbnOutput
            // 
            this.dgcOutputKbnOutput.DataPropertyName = "OUTPUT_KBN";
            this.dgcOutputKbnOutput.DBFieldsName = "OUTPUT_KBN";
            this.dgcOutputKbnOutput.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.dgcOutputKbnOutput.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgcOutputKbnOutput.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgcOutputKbnOutput.FocusOutCheckMethod")));
            this.dgcOutputKbnOutput.HeaderText = "出力区分";
            this.dgcOutputKbnOutput.Name = "dgcOutputKbnOutput";
            this.dgcOutputKbnOutput.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgcOutputKbnOutput.PopupSearchSendParams")));
            this.dgcOutputKbnOutput.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgcOutputKbnOutput.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgcOutputKbnOutput.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto3.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.dgcOutputKbnOutput.RangeSetting = rangeSettingDto3;
            this.dgcOutputKbnOutput.ReadOnly = true;
            this.dgcOutputKbnOutput.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgcOutputKbnOutput.RegistCheckMethod")));
            this.dgcOutputKbnOutput.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgcOutputKbnOutput.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgcOutputKbnOutput.Visible = false;
            this.dgcOutputKbnOutput.Width = 5;
            // 
            // dgcIdOutput
            // 
            this.dgcIdOutput.DataPropertyName = "ID";
            this.dgcIdOutput.DBFieldsName = "ID";
            this.dgcIdOutput.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            this.dgcIdOutput.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgcIdOutput.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgcIdOutput.FocusOutCheckMethod")));
            this.dgcIdOutput.HeaderText = "項目ID";
            this.dgcIdOutput.Name = "dgcIdOutput";
            this.dgcIdOutput.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgcIdOutput.PopupSearchSendParams")));
            this.dgcIdOutput.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgcIdOutput.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgcIdOutput.popupWindowSetting")));
            this.dgcIdOutput.RangeSetting = rangeSettingDto4;
            this.dgcIdOutput.ReadOnly = true;
            this.dgcIdOutput.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgcIdOutput.RegistCheckMethod")));
            this.dgcIdOutput.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgcIdOutput.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgcIdOutput.Visible = false;
            this.dgcIdOutput.Width = 5;
            // 
            // dgcDispNameOutput
            // 
            this.dgcDispNameOutput.DataPropertyName = "DISP_NAME";
            this.dgcDispNameOutput.DBFieldsName = "DISP_NAME";
            this.dgcDispNameOutput.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            this.dgcDispNameOutput.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgcDispNameOutput.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgcDispNameOutput.FocusOutCheckMethod")));
            this.dgcDispNameOutput.HeaderText = "項目";
            this.dgcDispNameOutput.Name = "dgcDispNameOutput";
            this.dgcDispNameOutput.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgcDispNameOutput.PopupSearchSendParams")));
            this.dgcDispNameOutput.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgcDispNameOutput.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgcDispNameOutput.popupWindowSetting")));
            this.dgcDispNameOutput.ReadOnly = true;
            this.dgcDispNameOutput.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgcDispNameOutput.RegistCheckMethod")));
            this.dgcDispNameOutput.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgcDispNameOutput.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgcDispNameOutput.Width = 400;
            // 
            // txtCondition
            // 
            this.txtCondition.BackColor = System.Drawing.SystemColors.Window;
            this.txtCondition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCondition.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.txtCondition.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtCondition.DisplayItemName = "選択項目検索";
            this.txtCondition.DisplayPopUp = null;
            this.txtCondition.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtCondition.FocusOutCheckMethod")));
            this.txtCondition.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtCondition.ForeColor = System.Drawing.Color.Black;
            this.txtCondition.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.txtCondition.IsInputErrorOccured = false;
            this.txtCondition.Location = new System.Drawing.Point(116, 22);
            this.txtCondition.MaxLength = 20;
            this.txtCondition.Name = "txtCondition";
            this.txtCondition.PopupAfterExecute = null;
            this.txtCondition.PopupBeforeExecute = null;
            this.txtCondition.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtCondition.PopupSearchSendParams")));
            this.txtCondition.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtCondition.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtCondition.popupWindowSetting")));
            this.txtCondition.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtCondition.RegistCheckMethod")));
            this.txtCondition.ShortItemName = "選択項目検索";
            this.txtCondition.Size = new System.Drawing.Size(304, 20);
            this.txtCondition.TabIndex = 350;
            this.txtCondition.Tag = "検索する文字を入力してください";
            // 
            // lblCondition
            // 
            this.lblCondition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblCondition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCondition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCondition.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblCondition.ForeColor = System.Drawing.Color.White;
            this.lblCondition.Location = new System.Drawing.Point(0, 22);
            this.lblCondition.Name = "lblCondition";
            this.lblCondition.Size = new System.Drawing.Size(110, 20);
            this.lblCondition.TabIndex = 340;
            this.lblCondition.Text = "選択項目検索";
            this.lblCondition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlOutputKbn
            // 
            this.pnlOutputKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOutputKbn.Controls.Add(this.txtOutputKbn);
            this.pnlOutputKbn.Controls.Add(this.rdoOutputKbn2);
            this.pnlOutputKbn.Controls.Add(this.rdoOutputKbn1);
            this.pnlOutputKbn.Location = new System.Drawing.Point(116, 0);
            this.pnlOutputKbn.Name = "pnlOutputKbn";
            this.pnlOutputKbn.Size = new System.Drawing.Size(182, 20);
            this.pnlOutputKbn.TabIndex = 315;
            // 
            // txtOutputKbn
            // 
            this.txtOutputKbn.BackColor = System.Drawing.SystemColors.Window;
            this.txtOutputKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOutputKbn.ChangeUpperCase = true;
            this.txtOutputKbn.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtOutputKbn.DisplayItemName = "出力区分";
            this.txtOutputKbn.DisplayPopUp = null;
            this.txtOutputKbn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtOutputKbn.FocusOutCheckMethod")));
            this.txtOutputKbn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtOutputKbn.ForeColor = System.Drawing.Color.Black;
            this.txtOutputKbn.IsInputErrorOccured = false;
            this.txtOutputKbn.LinkedRadioButtonArray = new string[] {
        "rdoOutputKbn1",
        "rdoOutputKbn2"};
            this.txtOutputKbn.Location = new System.Drawing.Point(-1, -1);
            this.txtOutputKbn.Name = "txtOutputKbn";
            this.txtOutputKbn.PopupAfterExecute = null;
            this.txtOutputKbn.PopupBeforeExecute = null;
            this.txtOutputKbn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtOutputKbn.PopupSearchSendParams")));
            this.txtOutputKbn.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtOutputKbn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtOutputKbn.popupWindowSetting")));
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
            this.txtOutputKbn.RangeSetting = rangeSettingDto5;
            this.txtOutputKbn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtOutputKbn.RegistCheckMethod")));
            this.txtOutputKbn.ShortItemName = "出力区分";
            this.txtOutputKbn.Size = new System.Drawing.Size(20, 20);
            this.txtOutputKbn.TabIndex = 310;
            this.txtOutputKbn.Tag = "【１、２】のいずれかで入力してください";
            this.txtOutputKbn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtOutputKbn.WordWrap = false;
            this.txtOutputKbn.TextChanged += new System.EventHandler(this.txtOutputKbn_TextChanged);
            // 
            // rdoOutputKbn2
            // 
            this.rdoOutputKbn2.AutoSize = true;
            this.rdoOutputKbn2.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoOutputKbn2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoOutputKbn2.FocusOutCheckMethod")));
            this.rdoOutputKbn2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoOutputKbn2.LinkedTextBox = "txtOutputKbn";
            this.rdoOutputKbn2.Location = new System.Drawing.Point(98, 1);
            this.rdoOutputKbn2.Name = "rdoOutputKbn2";
            this.rdoOutputKbn2.PopupAfterExecute = null;
            this.rdoOutputKbn2.PopupBeforeExecute = null;
            this.rdoOutputKbn2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoOutputKbn2.PopupSearchSendParams")));
            this.rdoOutputKbn2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoOutputKbn2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoOutputKbn2.popupWindowSetting")));
            this.rdoOutputKbn2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoOutputKbn2.RegistCheckMethod")));
            this.rdoOutputKbn2.Size = new System.Drawing.Size(67, 17);
            this.rdoOutputKbn2.TabIndex = 330;
            this.rdoOutputKbn2.Tag = "出力区分を選択します";
            this.rdoOutputKbn2.Text = "2.明細";
            this.rdoOutputKbn2.UseVisualStyleBackColor = true;
            this.rdoOutputKbn2.Value = "2";
            // 
            // rdoOutputKbn1
            // 
            this.rdoOutputKbn1.AutoSize = true;
            this.rdoOutputKbn1.CausesValidation = false;
            this.rdoOutputKbn1.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoOutputKbn1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoOutputKbn1.FocusOutCheckMethod")));
            this.rdoOutputKbn1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoOutputKbn1.LinkedTextBox = "txtOutputKbn";
            this.rdoOutputKbn1.Location = new System.Drawing.Point(25, 1);
            this.rdoOutputKbn1.Name = "rdoOutputKbn1";
            this.rdoOutputKbn1.PopupAfterExecute = null;
            this.rdoOutputKbn1.PopupBeforeExecute = null;
            this.rdoOutputKbn1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoOutputKbn1.PopupSearchSendParams")));
            this.rdoOutputKbn1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoOutputKbn1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoOutputKbn1.popupWindowSetting")));
            this.rdoOutputKbn1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoOutputKbn1.RegistCheckMethod")));
            this.rdoOutputKbn1.Size = new System.Drawing.Size(67, 17);
            this.rdoOutputKbn1.TabIndex = 320;
            this.rdoOutputKbn1.Tag = "出力区分を選択します";
            this.rdoOutputKbn1.Text = "1.伝票";
            this.rdoOutputKbn1.UseVisualStyleBackColor = true;
            this.rdoOutputKbn1.Value = "1";
            // 
            // lblOutput
            // 
            this.lblOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOutput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblOutput.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblOutput.ForeColor = System.Drawing.Color.White;
            this.lblOutput.Location = new System.Drawing.Point(580, 44);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(420, 20);
            this.lblOutput.TabIndex = 390;
            this.lblOutput.Text = "出力項目※";
            this.lblOutput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDown.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnDown.Location = new System.Drawing.Point(534, 283);
            this.btnDown.Name = "btnDown";
            this.btnDown.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btnDown.Size = new System.Drawing.Size(40, 22);
            this.btnDown.TabIndex = 440;
            this.btnDown.TabStop = false;
            this.btnDown.Tag = "出力項目で選択中の項目の表示順番を入れ替えます";
            this.btnDown.Text = "↓";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.bt_func4_Click);
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUp.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnUp.Location = new System.Drawing.Point(534, 255);
            this.btnUp.Name = "btnUp";
            this.btnUp.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btnUp.Size = new System.Drawing.Size(40, 22);
            this.btnUp.TabIndex = 430;
            this.btnUp.TabStop = false;
            this.btnUp.Tag = "出力項目で選択中の項目の表示順番を入れ替えます";
            this.btnUp.Text = "↑";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.bt_func3_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnAdd.Location = new System.Drawing.Point(534, 205);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btnAdd.Size = new System.Drawing.Size(40, 22);
            this.btnAdd.TabIndex = 420;
            this.btnAdd.TabStop = false;
            this.btnAdd.Tag = "選択中の項目を出力項目に追加します";
            this.btnAdd.Text = ">";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.bt_func2_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnRemove.Location = new System.Drawing.Point(426, 205);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btnRemove.Size = new System.Drawing.Size(40, 22);
            this.btnRemove.TabIndex = 410;
            this.btnRemove.TabStop = false;
            this.btnRemove.Tag = "選択中の項目を出力項目から除外します";
            this.btnRemove.Text = "<";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.bt_func1_Click);
            // 
            // lblSelect
            // 
            this.lblSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblSelect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSelect.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblSelect.ForeColor = System.Drawing.Color.White;
            this.lblSelect.Location = new System.Drawing.Point(0, 44);
            this.lblSelect.Name = "lblSelect";
            this.lblSelect.Size = new System.Drawing.Size(420, 20);
            this.lblSelect.TabIndex = 370;
            this.lblSelect.Text = "選択項目";
            this.lblSelect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOutputKbn
            // 
            this.lblOutputKbn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblOutputKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOutputKbn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblOutputKbn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblOutputKbn.ForeColor = System.Drawing.Color.White;
            this.lblOutputKbn.Location = new System.Drawing.Point(0, 0);
            this.lblOutputKbn.Name = "lblOutputKbn";
            this.lblOutputKbn.Size = new System.Drawing.Size(110, 20);
            this.lblOutputKbn.TabIndex = 300;
            this.lblOutputKbn.Text = "出力区分※";
            this.lblOutputKbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PatternForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.pnlPatternDetail);
            this.Controls.Add(this.txtPatternBikou);
            this.Controls.Add(this.lblPatternBikou);
            this.Controls.Add(this.txtPatternNm);
            this.Controls.Add(this.lblPatternNm);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Name = "PatternForm";
            this.Text = "UIForm";
            this.pnlPatternDetail.ResumeLayout(false);
            this.pnlPatternDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutput)).EndInit();
            this.pnlOutputKbn.ResumeLayout(false);
            this.pnlOutputKbn.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPatternNm;
        internal r_framework.CustomControl.CustomTextBox txtPatternNm;
        internal r_framework.CustomControl.CustomTextBox txtPatternBikou;
        private System.Windows.Forms.Label lblPatternBikou;
        private System.Windows.Forms.Panel pnlPatternDetail;
        internal r_framework.CustomControl.CustomTextBox txtCondition;
        private System.Windows.Forms.Label lblCondition;
        private System.Windows.Forms.Panel pnlOutputKbn;
        internal r_framework.CustomControl.CustomRadioButton rdoOutputKbn1;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.Label lblSelect;
        private System.Windows.Forms.Label lblOutputKbn;
        internal r_framework.CustomControl.CustomNumericTextBox2 txtOutputKbn;
        internal r_framework.CustomControl.CustomButton btnCondition;
        internal r_framework.CustomControl.CustomRadioButton rdoOutputKbn2;
        internal r_framework.CustomControl.CustomButton btnDown;
        internal r_framework.CustomControl.CustomButton btnUp;
        internal r_framework.CustomControl.CustomButton btnAdd;
        internal r_framework.CustomControl.CustomButton btnRemove;
        internal r_framework.CustomControl.CustomDataGridView dgvSelect;
        internal r_framework.CustomControl.CustomDataGridView dgvOutput;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column dgcOutputKbnSelect;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column dgcIdSelect;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgcDispNameSelect;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column dgcOutputKbnOutput;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column dgcIdOutput;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgcDispNameOutput;
    }
}