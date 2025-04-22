namespace Shougun.Core.ElectronicManifest.DenshiCSVTorikomu
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbl_ManiKBN = new System.Windows.Forms.Label();
            this.ctxt_FilePath = new r_framework.CustomControl.CustomTextBox();
            this.cbtn_Sansyou = new r_framework.CustomControl.CustomButton();
            this.customDataGridView1 = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.マニフェスト番号 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.Column2 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.Column3 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.Column4 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.Column5 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.Column6 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.Column7 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.Column8 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.Column9 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.Column10 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.Column11 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.Column12 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.Column13 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.Column14 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.Column15 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.Column16 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.Column17 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.Column18 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.Column19 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.Column20 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.Column21 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.Column22 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.Column23 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.Column24 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.Column25 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.Column26 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CSV_INDEX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_ManiKBN
            // 
            this.lbl_ManiKBN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_ManiKBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_ManiKBN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_ManiKBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_ManiKBN.ForeColor = System.Drawing.Color.White;
            this.lbl_ManiKBN.Location = new System.Drawing.Point(2, 10);
            this.lbl_ManiKBN.Name = "lbl_ManiKBN";
            this.lbl_ManiKBN.Size = new System.Drawing.Size(124, 22);
            this.lbl_ManiKBN.TabIndex = 1;
            this.lbl_ManiKBN.Text = "取込先";
            this.lbl_ManiKBN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ctxt_FilePath
            // 
            this.ctxt_FilePath.AutoChangeBackColorEnabled = true;
            this.ctxt_FilePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_FilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_FilePath.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_FilePath.DisplayPopUp = null;
            this.ctxt_FilePath.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_FilePath.FocusOutCheckMethod")));
            this.ctxt_FilePath.IsInputErrorOccured = false;
            this.ctxt_FilePath.Location = new System.Drawing.Point(132, 11);
            this.ctxt_FilePath.Name = "ctxt_FilePath";
            this.ctxt_FilePath.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_FilePath.PopupSearchSendParams")));
            this.ctxt_FilePath.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_FilePath.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_FilePath.popupWindowSetting")));
            this.ctxt_FilePath.ReadOnly = true;
            this.ctxt_FilePath.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_FilePath.RegistCheckMethod")));
            this.ctxt_FilePath.Size = new System.Drawing.Size(283, 20);
            this.ctxt_FilePath.TabIndex = 10;
            this.ctxt_FilePath.TabStop = false;
            // 
            // cbtn_Sansyou
            // 
            this.cbtn_Sansyou.DefaultBackColor = System.Drawing.Color.Empty;
            this.cbtn_Sansyou.Location = new System.Drawing.Point(419, 10);
            this.cbtn_Sansyou.Name = "cbtn_Sansyou";
            this.cbtn_Sansyou.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.cbtn_Sansyou.Size = new System.Drawing.Size(71, 22);
            this.cbtn_Sansyou.TabIndex = 6;
            this.cbtn_Sansyou.Text = "参照";
            this.cbtn_Sansyou.UseVisualStyleBackColor = true;
            this.cbtn_Sansyou.Click += new System.EventHandler(this.cbtn_Sansyou_Click);
            // 
            // customDataGridView1
            // 
            this.customDataGridView1.AllowUserToAddRows = false;
            this.customDataGridView1.AllowUserToDeleteRows = false;
            this.customDataGridView1.AllowUserToResizeRows = false;
            this.customDataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.customDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.マニフェスト番号,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column14,
            this.Column15,
            this.Column16,
            this.Column17,
            this.Column18,
            this.Column19,
            this.Column20,
            this.Column21,
            this.Column22,
            this.Column23,
            this.Column24,
            this.Column25,
            this.Column26,
            this.CSV_INDEX});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.customDataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.customDataGridView1.EnableHeadersVisualStyles = false;
            this.customDataGridView1.GridColor = System.Drawing.Color.White;
            this.customDataGridView1.IsReload = false;
            this.customDataGridView1.LinkedDataPanelName = null;
            this.customDataGridView1.Location = new System.Drawing.Point(2, 38);
            this.customDataGridView1.Name = "customDataGridView1";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.customDataGridView1.RowHeadersVisible = false;
            this.customDataGridView1.RowTemplate.Height = 21;
            this.customDataGridView1.ShowCellToolTips = false;
            this.customDataGridView1.Size = new System.Drawing.Size(997, 415);
            this.customDataGridView1.TabIndex = 7;
            this.customDataGridView1.IsBrowsePurpose = true;
            // 
            // マニフェスト番号
            // 
            this.マニフェスト番号.DefaultBackColor = System.Drawing.Color.Empty;
            this.マニフェスト番号.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("マニフェスト番号.FocusOutCheckMethod")));
            this.マニフェスト番号.FormatSetting = "";
            this.マニフェスト番号.HeaderText = "マニフェスト番号";
            this.マニフェスト番号.MaxInputLength = 150;
            this.マニフェスト番号.Name = "マニフェスト番号";
            this.マニフェスト番号.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("マニフェスト番号.PopupSearchSendParams")));
            this.マニフェスト番号.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.マニフェスト番号.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("マニフェスト番号.popupWindowSetting")));
            this.マニフェスト番号.ReadOnly = true;
            this.マニフェスト番号.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("マニフェスト番号.RegistCheckMethod")));
            this.マニフェスト番号.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.マニフェスト番号.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.マニフェスト番号.Width = 170;
            // 
            // Column2
            // 
            this.Column2.DefaultBackColor = System.Drawing.Color.Empty;
            this.Column2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column2.FocusOutCheckMethod")));
            this.Column2.HeaderText = "引渡日";
            this.Column2.MaxInputLength = 100;
            this.Column2.Name = "Column2";
            this.Column2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Column2.PopupSearchSendParams")));
            this.Column2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Column2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Column2.popupWindowSetting")));
            this.Column2.ReadOnly = true;
            this.Column2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column2.RegistCheckMethod")));
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 80;
            // 
            // Column3
            // 
            this.Column3.DefaultBackColor = System.Drawing.Color.Empty;
            this.Column3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column3.FocusOutCheckMethod")));
            this.Column3.HeaderText = "排出業者名";
            this.Column3.MaxInputLength = 100;
            this.Column3.Name = "Column3";
            this.Column3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Column3.PopupSearchSendParams")));
            this.Column3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Column3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Column3.popupWindowSetting")));
            this.Column3.ReadOnly = true;
            this.Column3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column3.RegistCheckMethod")));
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 120;
            // 
            // Column4
            // 
            this.Column4.DefaultBackColor = System.Drawing.Color.Empty;
            this.Column4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column4.FocusOutCheckMethod")));
            this.Column4.HeaderText = "排出事業場名";
            this.Column4.MaxInputLength = 100;
            this.Column4.Name = "Column4";
            this.Column4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Column4.PopupSearchSendParams")));
            this.Column4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Column4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Column4.popupWindowSetting")));
            this.Column4.ReadOnly = true;
            this.Column4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column4.RegistCheckMethod")));
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 150;
            // 
            // Column5
            // 
            this.Column5.DefaultBackColor = System.Drawing.Color.Empty;
            this.Column5.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column5.FocusOutCheckMethod")));
            this.Column5.HeaderText = "連絡番号1";
            this.Column5.MaxInputLength = 100;
            this.Column5.Name = "Column5";
            this.Column5.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Column5.PopupSearchSendParams")));
            this.Column5.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Column5.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Column5.popupWindowSetting")));
            this.Column5.ReadOnly = true;
            this.Column5.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column5.RegistCheckMethod")));
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column6
            // 
            this.Column6.DefaultBackColor = System.Drawing.Color.Empty;
            this.Column6.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column6.FocusOutCheckMethod")));
            this.Column6.HeaderText = "連絡番号2";
            this.Column6.MaxInputLength = 100;
            this.Column6.Name = "Column6";
            this.Column6.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Column6.PopupSearchSendParams")));
            this.Column6.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Column6.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Column6.popupWindowSetting")));
            this.Column6.ReadOnly = true;
            this.Column6.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column6.RegistCheckMethod")));
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column7
            // 
            this.Column7.DefaultBackColor = System.Drawing.Color.Empty;
            this.Column7.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column7.FocusOutCheckMethod")));
            this.Column7.HeaderText = "連絡番号3";
            this.Column7.MaxInputLength = 100;
            this.Column7.Name = "Column7";
            this.Column7.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Column7.PopupSearchSendParams")));
            this.Column7.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Column7.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Column7.popupWindowSetting")));
            this.Column7.ReadOnly = true;
            this.Column7.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column7.RegistCheckMethod")));
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column8
            // 
            this.Column8.DefaultBackColor = System.Drawing.Color.Empty;
            this.Column8.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column8.FocusOutCheckMethod")));
            this.Column8.HeaderText = "廃棄物の種類";
            this.Column8.MaxInputLength = 100;
            this.Column8.Name = "Column8";
            this.Column8.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Column8.PopupSearchSendParams")));
            this.Column8.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Column8.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Column8.popupWindowSetting")));
            this.Column8.ReadOnly = true;
            this.Column8.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column8.RegistCheckMethod")));
            this.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column8.Width = 120;
            // 
            // Column9
            // 
            this.Column9.DefaultBackColor = System.Drawing.Color.Empty;
            this.Column9.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column9.FocusOutCheckMethod")));
            this.Column9.HeaderText = "廃棄物の名称";
            this.Column9.MaxInputLength = 100;
            this.Column9.Name = "Column9";
            this.Column9.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Column9.PopupSearchSendParams")));
            this.Column9.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Column9.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Column9.popupWindowSetting")));
            this.Column9.ReadOnly = true;
            this.Column9.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column9.RegistCheckMethod")));
            this.Column9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column9.Width = 120;
            // 
            // Column10
            // 
            this.Column10.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.NullValue = null;
            this.Column10.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column10.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column10.FocusOutCheckMethod")));
            this.Column10.FormatSetting = "";
            this.Column10.HeaderText = "廃棄物数量";
            this.Column10.MaxInputLength = 100;
            this.Column10.Name = "Column10";
            this.Column10.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Column10.PopupSearchSendParams")));
            this.Column10.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Column10.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Column10.popupWindowSetting")));
            this.Column10.ReadOnly = true;
            this.Column10.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column10.RegistCheckMethod")));
            this.Column10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column10.Width = 120;
            // 
            // Column11
            // 
            this.Column11.DefaultBackColor = System.Drawing.Color.Empty;
            this.Column11.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column11.FocusOutCheckMethod")));
            this.Column11.HeaderText = "廃棄物数量単位";
            this.Column11.MaxInputLength = 100;
            this.Column11.Name = "Column11";
            this.Column11.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Column11.PopupSearchSendParams")));
            this.Column11.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Column11.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Column11.popupWindowSetting")));
            this.Column11.ReadOnly = true;
            this.Column11.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column11.RegistCheckMethod")));
            this.Column11.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column11.Width = 140;
            // 
            // Column12
            // 
            this.Column12.DefaultBackColor = System.Drawing.Color.Empty;
            this.Column12.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column12.FocusOutCheckMethod")));
            this.Column12.HeaderText = "荷姿";
            this.Column12.MaxInputLength = 100;
            this.Column12.Name = "Column12";
            this.Column12.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Column12.PopupSearchSendParams")));
            this.Column12.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Column12.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Column12.popupWindowSetting")));
            this.Column12.ReadOnly = true;
            this.Column12.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column12.RegistCheckMethod")));
            this.Column12.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column13
            // 
            this.Column13.DefaultBackColor = System.Drawing.Color.Empty;
            this.Column13.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column13.FocusOutCheckMethod")));
            this.Column13.HeaderText = "数量確定者";
            this.Column13.MaxInputLength = 100;
            this.Column13.Name = "Column13";
            this.Column13.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Column13.PopupSearchSendParams")));
            this.Column13.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Column13.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Column13.popupWindowSetting")));
            this.Column13.ReadOnly = true;
            this.Column13.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column13.RegistCheckMethod")));
            this.Column13.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column13.Width = 180;
            // 
            // Column14
            // 
            this.Column14.DefaultBackColor = System.Drawing.Color.Empty;
            this.Column14.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column14.FocusOutCheckMethod")));
            this.Column14.HeaderText = "有害物質1";
            this.Column14.MaxInputLength = 100;
            this.Column14.Name = "Column14";
            this.Column14.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Column14.PopupSearchSendParams")));
            this.Column14.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Column14.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Column14.popupWindowSetting")));
            this.Column14.ReadOnly = true;
            this.Column14.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column14.RegistCheckMethod")));
            this.Column14.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column15
            // 
            this.Column15.DefaultBackColor = System.Drawing.Color.Empty;
            this.Column15.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column15.FocusOutCheckMethod")));
            this.Column15.HeaderText = "収集運搬業者";
            this.Column15.MaxInputLength = 100;
            this.Column15.Name = "Column15";
            this.Column15.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Column15.PopupSearchSendParams")));
            this.Column15.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Column15.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Column15.popupWindowSetting")));
            this.Column15.ReadOnly = true;
            this.Column15.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column15.RegistCheckMethod")));
            this.Column15.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column15.Width = 150;
            // 
            // Column16
            // 
            this.Column16.DefaultBackColor = System.Drawing.Color.Empty;
            this.Column16.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column16.FocusOutCheckMethod")));
            this.Column16.HeaderText = "運搬方法";
            this.Column16.Name = "Column16";
            this.Column16.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Column16.PopupSearchSendParams")));
            this.Column16.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Column16.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Column16.popupWindowSetting")));
            this.Column16.ReadOnly = true;
            this.Column16.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column16.RegistCheckMethod")));
            this.Column16.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column16.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column17
            // 
            this.Column17.DefaultBackColor = System.Drawing.Color.Empty;
            this.Column17.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column17.FocusOutCheckMethod")));
            this.Column17.HeaderText = "車輌番号";
            this.Column17.MaxInputLength = 100;
            this.Column17.Name = "Column17";
            this.Column17.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Column17.PopupSearchSendParams")));
            this.Column17.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Column17.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Column17.popupWindowSetting")));
            this.Column17.ReadOnly = true;
            this.Column17.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column17.RegistCheckMethod")));
            this.Column17.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column17.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column18
            // 
            this.Column18.DefaultBackColor = System.Drawing.Color.Empty;
            this.Column18.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column18.FocusOutCheckMethod")));
            this.Column18.HeaderText = "処分業者";
            this.Column18.MaxInputLength = 100;
            this.Column18.Name = "Column18";
            this.Column18.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Column18.PopupSearchSendParams")));
            this.Column18.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Column18.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Column18.popupWindowSetting")));
            this.Column18.ReadOnly = true;
            this.Column18.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column18.RegistCheckMethod")));
            this.Column18.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column18.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column19
            // 
            this.Column19.DefaultBackColor = System.Drawing.Color.Empty;
            this.Column19.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column19.FocusOutCheckMethod")));
            this.Column19.HeaderText = "処分事業場";
            this.Column19.Name = "Column19";
            this.Column19.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Column19.PopupSearchSendParams")));
            this.Column19.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Column19.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Column19.popupWindowSetting")));
            this.Column19.ReadOnly = true;
            this.Column19.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column19.RegistCheckMethod")));
            this.Column19.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column19.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column19.Width = 120;
            // 
            // Column20
            // 
            this.Column20.DefaultBackColor = System.Drawing.Color.Empty;
            this.Column20.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column20.FocusOutCheckMethod")));
            this.Column20.HeaderText = "処分方法";
            this.Column20.Name = "Column20";
            this.Column20.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Column20.PopupSearchSendParams")));
            this.Column20.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Column20.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Column20.popupWindowSetting")));
            this.Column20.ReadOnly = true;
            this.Column20.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column20.RegistCheckMethod")));
            this.Column20.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column20.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column21
            // 
            this.Column21.DefaultBackColor = System.Drawing.Color.Empty;
            this.Column21.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column21.FocusOutCheckMethod")));
            this.Column21.HeaderText = "最終処分事業場";
            this.Column21.Name = "Column21";
            this.Column21.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Column21.PopupSearchSendParams")));
            this.Column21.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Column21.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Column21.popupWindowSetting")));
            this.Column21.ReadOnly = true;
            this.Column21.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column21.RegistCheckMethod")));
            this.Column21.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column21.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column21.Width = 150;
            // 
            // Column22
            // 
            this.Column22.DefaultBackColor = System.Drawing.Color.Empty;
            this.Column22.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column22.FocusOutCheckMethod")));
            this.Column22.HeaderText = "備考1";
            this.Column22.Name = "Column22";
            this.Column22.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Column22.PopupSearchSendParams")));
            this.Column22.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Column22.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Column22.popupWindowSetting")));
            this.Column22.ReadOnly = true;
            this.Column22.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column22.RegistCheckMethod")));
            this.Column22.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column22.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column23
            // 
            this.Column23.DefaultBackColor = System.Drawing.Color.Empty;
            this.Column23.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column23.FocusOutCheckMethod")));
            this.Column23.HeaderText = "備考2";
            this.Column23.Name = "Column23";
            this.Column23.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Column23.PopupSearchSendParams")));
            this.Column23.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Column23.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Column23.popupWindowSetting")));
            this.Column23.ReadOnly = true;
            this.Column23.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column23.RegistCheckMethod")));
            this.Column23.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column23.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column24
            // 
            this.Column24.DefaultBackColor = System.Drawing.Color.Empty;
            this.Column24.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column24.FocusOutCheckMethod")));
            this.Column24.HeaderText = "備考3";
            this.Column24.Name = "Column24";
            this.Column24.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Column24.PopupSearchSendParams")));
            this.Column24.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Column24.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Column24.popupWindowSetting")));
            this.Column24.ReadOnly = true;
            this.Column24.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column24.RegistCheckMethod")));
            this.Column24.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column25
            // 
            this.Column25.DefaultBackColor = System.Drawing.Color.Empty;
            this.Column25.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column25.FocusOutCheckMethod")));
            this.Column25.HeaderText = "備考4";
            this.Column25.Name = "Column25";
            this.Column25.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Column25.PopupSearchSendParams")));
            this.Column25.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Column25.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Column25.popupWindowSetting")));
            this.Column25.ReadOnly = true;
            this.Column25.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column25.RegistCheckMethod")));
            this.Column25.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column25.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column26
            // 
            this.Column26.DefaultBackColor = System.Drawing.Color.Empty;
            this.Column26.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column26.FocusOutCheckMethod")));
            this.Column26.HeaderText = "備考5";
            this.Column26.Name = "Column26";
            this.Column26.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Column26.PopupSearchSendParams")));
            this.Column26.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Column26.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Column26.popupWindowSetting")));
            this.Column26.ReadOnly = true;
            this.Column26.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Column26.RegistCheckMethod")));
            this.Column26.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column26.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CSV_INDEX
            // 
            this.CSV_INDEX.HeaderText = "Column1";
            this.CSV_INDEX.Name = "CSV_INDEX";
            this.CSV_INDEX.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CSV_INDEX.Visible = false;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1300, 642);
            this.Controls.Add(this.customDataGridView1);
            this.Controls.Add(this.cbtn_Sansyou);
            this.Controls.Add(this.ctxt_FilePath);
            this.Controls.Add(this.lbl_ManiKBN);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Name = "UIForm";
            this.Text = "UIForm";
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lbl_ManiKBN;
        private r_framework.CustomControl.CustomButton cbtn_Sansyou;
        internal r_framework.CustomControl.CustomDataGridView customDataGridView1;
        public r_framework.CustomControl.CustomTextBox ctxt_FilePath;
        private r_framework.CustomControl.DgvCustomTextBoxColumn マニフェスト番号;
        private r_framework.CustomControl.DgvCustomTextBoxColumn Column2;
        private r_framework.CustomControl.DgvCustomTextBoxColumn Column3;
        private r_framework.CustomControl.DgvCustomTextBoxColumn Column4;
        private r_framework.CustomControl.DgvCustomTextBoxColumn Column5;
        private r_framework.CustomControl.DgvCustomTextBoxColumn Column6;
        private r_framework.CustomControl.DgvCustomTextBoxColumn Column7;
        private r_framework.CustomControl.DgvCustomTextBoxColumn Column8;
        private r_framework.CustomControl.DgvCustomTextBoxColumn Column9;
        private r_framework.CustomControl.DgvCustomTextBoxColumn Column10;
        private r_framework.CustomControl.DgvCustomTextBoxColumn Column11;
        private r_framework.CustomControl.DgvCustomTextBoxColumn Column12;
        private r_framework.CustomControl.DgvCustomTextBoxColumn Column13;
        private r_framework.CustomControl.DgvCustomTextBoxColumn Column14;
        private r_framework.CustomControl.DgvCustomTextBoxColumn Column15;
        private r_framework.CustomControl.DgvCustomTextBoxColumn Column16;
        private r_framework.CustomControl.DgvCustomTextBoxColumn Column17;
        private r_framework.CustomControl.DgvCustomTextBoxColumn Column18;
        private r_framework.CustomControl.DgvCustomTextBoxColumn Column19;
        private r_framework.CustomControl.DgvCustomTextBoxColumn Column20;
        private r_framework.CustomControl.DgvCustomTextBoxColumn Column21;
        private r_framework.CustomControl.DgvCustomTextBoxColumn Column22;
        private r_framework.CustomControl.DgvCustomTextBoxColumn Column23;
        private r_framework.CustomControl.DgvCustomTextBoxColumn Column24;
        private r_framework.CustomControl.DgvCustomTextBoxColumn Column25;
        private r_framework.CustomControl.DgvCustomTextBoxColumn Column26;
        private System.Windows.Forms.DataGridViewTextBoxColumn CSV_INDEX;

    }
}