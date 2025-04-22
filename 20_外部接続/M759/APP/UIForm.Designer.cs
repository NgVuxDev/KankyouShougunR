using System.Windows.Forms;
using System;
namespace DenshiKeiyakuHimodzukeHojo.App
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvWanSign = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.colWkoumokumei = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.colWCd = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.lb_title = new System.Windows.Forms.Label();
            this.pn_foot = new System.Windows.Forms.Panel();
            this.bt_func12 = new r_framework.CustomControl.CustomButton();
            this.lb_hint = new System.Windows.Forms.Label();
            this.bt_func11 = new r_framework.CustomControl.CustomButton();
            this.bt_func10 = new r_framework.CustomControl.CustomButton();
            this.bt_func9 = new r_framework.CustomControl.CustomButton();
            this.bt_func8 = new r_framework.CustomControl.CustomButton();
            this.bt_func7 = new r_framework.CustomControl.CustomButton();
            this.bt_func6 = new r_framework.CustomControl.CustomButton();
            this.bt_func5 = new r_framework.CustomControl.CustomButton();
            this.bt_func4 = new r_framework.CustomControl.CustomButton();
            this.bt_func3 = new r_framework.CustomControl.CustomButton();
            this.bt_func2 = new r_framework.CustomControl.CustomButton();
            this.bt_func1 = new r_framework.CustomControl.CustomButton();
            this.dgvShougun = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.colRKoumokkumei = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.colRCd = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.customPanel2 = new r_framework.CustomControl.CustomPanel();
            this.KENSAKU_HOUHOU1 = new r_framework.CustomControl.CustomRadioButton();
            this.KENSAKU_HOUHOU2 = new r_framework.CustomControl.CustomRadioButton();
            this.KENSAKU_HOUHOU = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvCustomTextBoxColumn1 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn2 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWanSign)).BeginInit();
            this.pn_foot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShougun)).BeginInit();
            this.customPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvWanSign
            // 
            this.dgvWanSign.AllowUserToAddRows = false;
            this.dgvWanSign.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvWanSign.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(160)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvWanSign.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvWanSign.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWanSign.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colWkoumokumei,
            this.colWCd});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvWanSign.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvWanSign.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvWanSign.EnableHeadersVisualStyles = false;
            this.dgvWanSign.GridColor = System.Drawing.Color.White;
            this.dgvWanSign.IsReload = false;
            this.dgvWanSign.LinkedDataPanelName = "customSortHeader1";
            this.dgvWanSign.Location = new System.Drawing.Point(12, 81);
            this.dgvWanSign.MultiSelect = false;
            this.dgvWanSign.Name = "dgvWanSign";
            this.dgvWanSign.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvWanSign.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvWanSign.RowHeadersVisible = false;
            this.dgvWanSign.RowTemplate.Height = 21;
            this.dgvWanSign.ShowCellToolTips = false;
            this.dgvWanSign.Size = new System.Drawing.Size(475, 440);
            this.dgvWanSign.TabIndex = 1;
            // 
            // colWkoumokumei
            // 
            this.colWkoumokumei.DataPropertyName = "WANSIGN_KOUMOKU_NAME";
            this.colWkoumokumei.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.colWkoumokumei.DefaultCellStyle = dataGridViewCellStyle2;
            this.colWkoumokumei.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("colWkoumokumei.FocusOutCheckMethod")));
            this.colWkoumokumei.HeaderText = "項目名";
            this.colWkoumokumei.MinimumWidth = 450;
            this.colWkoumokumei.Name = "colWkoumokumei";
            this.colWkoumokumei.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("colWkoumokumei.PopupSearchSendParams")));
            this.colWkoumokumei.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.colWkoumokumei.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("colWkoumokumei.popupWindowSetting")));
            this.colWkoumokumei.ReadOnly = true;
            this.colWkoumokumei.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("colWkoumokumei.RegistCheckMethod")));
            this.colWkoumokumei.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colWkoumokumei.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colWkoumokumei.Width = 450;
            // 
            // colWCd
            // 
            this.colWCd.DataPropertyName = "WANSIGN_KOUMOKU_CD";
            this.colWCd.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.colWCd.DefaultCellStyle = dataGridViewCellStyle3;
            this.colWCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("colWCd.FocusOutCheckMethod")));
            this.colWCd.HeaderText = "colCd";
            this.colWCd.Name = "colWCd";
            this.colWCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("colWCd.PopupSearchSendParams")));
            this.colWCd.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.colWCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("colWCd.popupWindowSetting")));
            this.colWCd.ReadOnly = true;
            this.colWCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("colWCd.RegistCheckMethod")));
            this.colWCd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colWCd.Visible = false;
            // 
            // lb_title
            // 
            this.lb_title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lb_title.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_title.Font = new System.Drawing.Font("MS Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lb_title.ForeColor = System.Drawing.Color.White;
            this.lb_title.Location = new System.Drawing.Point(12, 9);
            this.lb_title.Name = "lb_title";
            this.lb_title.Size = new System.Drawing.Size(330, 35);
            this.lb_title.TabIndex = 388;
            this.lb_title.Text = "電子契約紐付補助";
            this.lb_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pn_foot
            // 
            this.pn_foot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pn_foot.Controls.Add(this.bt_func12);
            this.pn_foot.Controls.Add(this.lb_hint);
            this.pn_foot.Controls.Add(this.bt_func11);
            this.pn_foot.Controls.Add(this.bt_func10);
            this.pn_foot.Controls.Add(this.bt_func9);
            this.pn_foot.Controls.Add(this.bt_func8);
            this.pn_foot.Controls.Add(this.bt_func7);
            this.pn_foot.Controls.Add(this.bt_func6);
            this.pn_foot.Controls.Add(this.bt_func5);
            this.pn_foot.Controls.Add(this.bt_func4);
            this.pn_foot.Controls.Add(this.bt_func3);
            this.pn_foot.Controls.Add(this.bt_func2);
            this.pn_foot.Controls.Add(this.bt_func1);
            this.pn_foot.Location = new System.Drawing.Point(12, 536);
            this.pn_foot.Name = "pn_foot";
            this.pn_foot.Size = new System.Drawing.Size(999, 68);
            this.pn_foot.TabIndex = 389;
            // 
            // bt_func12
            // 
            this.bt_func12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func12.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func12.Enabled = false;
            this.bt_func12.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func12.Location = new System.Drawing.Point(912, 29);
            this.bt_func12.Name = "bt_func12";
            this.bt_func12.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func12.Size = new System.Drawing.Size(80, 35);
            this.bt_func12.TabIndex = 14;
            this.bt_func12.TabStop = false;
            this.bt_func12.Tag = "";
            this.bt_func12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func12.UseVisualStyleBackColor = false;
            // 
            // lb_hint
            // 
            this.lb_hint.BackColor = System.Drawing.Color.Black;
            this.lb_hint.Font = new System.Drawing.Font("Meiryo", 9.75F);
            this.lb_hint.ForeColor = System.Drawing.Color.Yellow;
            this.lb_hint.Location = new System.Drawing.Point(3, 4);
            this.lb_hint.Name = "lb_hint";
            this.lb_hint.Size = new System.Drawing.Size(989, 21);
            this.lb_hint.TabIndex = 0;
            this.lb_hint.Text = "１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０";
            this.lb_hint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bt_func11
            // 
            this.bt_func11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func11.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func11.Enabled = false;
            this.bt_func11.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func11.Location = new System.Drawing.Point(831, 29);
            this.bt_func11.Name = "bt_func11";
            this.bt_func11.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func11.Size = new System.Drawing.Size(80, 35);
            this.bt_func11.TabIndex = 13;
            this.bt_func11.TabStop = false;
            this.bt_func11.Tag = "";
            this.bt_func11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func11.UseVisualStyleBackColor = false;
            // 
            // bt_func10
            // 
            this.bt_func10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func10.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func10.Enabled = false;
            this.bt_func10.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func10.Location = new System.Drawing.Point(750, 29);
            this.bt_func10.Name = "bt_func10";
            this.bt_func10.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func10.Size = new System.Drawing.Size(80, 35);
            this.bt_func10.TabIndex = 12;
            this.bt_func10.TabStop = false;
            this.bt_func10.Tag = "";
            this.bt_func10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func10.UseVisualStyleBackColor = false;
            // 
            // bt_func9
            // 
            this.bt_func9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func9.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func9.Enabled = false;
            this.bt_func9.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func9.Location = new System.Drawing.Point(669, 29);
            this.bt_func9.Name = "bt_func9";
            this.bt_func9.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func9.Size = new System.Drawing.Size(80, 35);
            this.bt_func9.TabIndex = 11;
            this.bt_func9.TabStop = false;
            this.bt_func9.Tag = "";
            this.bt_func9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func9.UseVisualStyleBackColor = false;
            // 
            // bt_func8
            // 
            this.bt_func8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func8.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func8.Enabled = false;
            this.bt_func8.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func8.Location = new System.Drawing.Point(579, 29);
            this.bt_func8.Name = "bt_func8";
            this.bt_func8.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func8.Size = new System.Drawing.Size(80, 35);
            this.bt_func8.TabIndex = 10;
            this.bt_func8.TabStop = false;
            this.bt_func8.Tag = "";
            this.bt_func8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func8.UseVisualStyleBackColor = false;
            // 
            // bt_func7
            // 
            this.bt_func7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func7.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func7.Enabled = false;
            this.bt_func7.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func7.Location = new System.Drawing.Point(498, 29);
            this.bt_func7.Name = "bt_func7";
            this.bt_func7.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func7.Size = new System.Drawing.Size(80, 35);
            this.bt_func7.TabIndex = 9;
            this.bt_func7.TabStop = false;
            this.bt_func7.Tag = "";
            this.bt_func7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func7.UseVisualStyleBackColor = false;
            // 
            // bt_func6
            // 
            this.bt_func6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func6.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func6.Enabled = false;
            this.bt_func6.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func6.Location = new System.Drawing.Point(417, 29);
            this.bt_func6.Name = "bt_func6";
            this.bt_func6.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func6.Size = new System.Drawing.Size(80, 35);
            this.bt_func6.TabIndex = 8;
            this.bt_func6.TabStop = false;
            this.bt_func6.Tag = "";
            this.bt_func6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func6.UseVisualStyleBackColor = false;
            // 
            // bt_func5
            // 
            this.bt_func5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func5.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func5.Enabled = false;
            this.bt_func5.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func5.Location = new System.Drawing.Point(336, 29);
            this.bt_func5.Name = "bt_func5";
            this.bt_func5.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func5.Size = new System.Drawing.Size(80, 35);
            this.bt_func5.TabIndex = 7;
            this.bt_func5.TabStop = false;
            this.bt_func5.Tag = "";
            this.bt_func5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func5.UseVisualStyleBackColor = false;
            // 
            // bt_func4
            // 
            this.bt_func4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func4.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func4.Enabled = false;
            this.bt_func4.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func4.Location = new System.Drawing.Point(246, 29);
            this.bt_func4.Name = "bt_func4";
            this.bt_func4.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func4.Size = new System.Drawing.Size(80, 35);
            this.bt_func4.TabIndex = 6;
            this.bt_func4.TabStop = false;
            this.bt_func4.Tag = "";
            this.bt_func4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func4.UseVisualStyleBackColor = false;
            // 
            // bt_func3
            // 
            this.bt_func3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func3.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func3.Enabled = false;
            this.bt_func3.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func3.Location = new System.Drawing.Point(165, 29);
            this.bt_func3.Name = "bt_func3";
            this.bt_func3.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func3.Size = new System.Drawing.Size(80, 35);
            this.bt_func3.TabIndex = 5;
            this.bt_func3.TabStop = false;
            this.bt_func3.Tag = "";
            this.bt_func3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func3.UseVisualStyleBackColor = false;
            // 
            // bt_func2
            // 
            this.bt_func2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func2.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func2.Enabled = false;
            this.bt_func2.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func2.Location = new System.Drawing.Point(84, 29);
            this.bt_func2.Name = "bt_func2";
            this.bt_func2.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func2.Size = new System.Drawing.Size(80, 35);
            this.bt_func2.TabIndex = 4;
            this.bt_func2.TabStop = false;
            this.bt_func2.Tag = "";
            this.bt_func2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func2.UseVisualStyleBackColor = false;
            // 
            // bt_func1
            // 
            this.bt_func1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func1.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func1.Enabled = false;
            this.bt_func1.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func1.Location = new System.Drawing.Point(3, 29);
            this.bt_func1.Name = "bt_func1";
            this.bt_func1.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func1.Size = new System.Drawing.Size(80, 35);
            this.bt_func1.TabIndex = 3;
            this.bt_func1.TabStop = false;
            this.bt_func1.Tag = "";
            this.bt_func1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func1.UseVisualStyleBackColor = false;
            // 
            // dgvShougun
            // 
            this.dgvShougun.AllowUserToAddRows = false;
            this.dgvShougun.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvShougun.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvShougun.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvShougun.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShougun.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRKoumokkumei,
            this.colRCd});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvShougun.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvShougun.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvShougun.EnableHeadersVisualStyles = false;
            this.dgvShougun.GridColor = System.Drawing.Color.White;
            this.dgvShougun.IsReload = false;
            this.dgvShougun.LinkedDataPanelName = "customSortHeader1";
            this.dgvShougun.Location = new System.Drawing.Point(530, 81);
            this.dgvShougun.MultiSelect = false;
            this.dgvShougun.Name = "dgvShougun";
            this.dgvShougun.ReadOnly = true;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvShougun.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvShougun.RowHeadersVisible = false;
            this.dgvShougun.RowTemplate.Height = 21;
            this.dgvShougun.ShowCellToolTips = false;
            this.dgvShougun.Size = new System.Drawing.Size(475, 440);
            this.dgvShougun.TabIndex = 390;
            // 
            // colRKoumokkumei
            // 
            this.colRKoumokkumei.DataPropertyName = "R_KOUMOKU_NAME";
            this.colRKoumokkumei.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.colRKoumokkumei.DefaultCellStyle = dataGridViewCellStyle7;
            this.colRKoumokkumei.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("colRKoumokkumei.FocusOutCheckMethod")));
            this.colRKoumokkumei.HeaderText = "項目名";
            this.colRKoumokkumei.MinimumWidth = 450;
            this.colRKoumokkumei.Name = "colRKoumokkumei";
            this.colRKoumokkumei.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("colRKoumokkumei.PopupSearchSendParams")));
            this.colRKoumokkumei.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.colRKoumokkumei.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("colRKoumokkumei.popupWindowSetting")));
            this.colRKoumokkumei.ReadOnly = true;
            this.colRKoumokkumei.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("colRKoumokkumei.RegistCheckMethod")));
            this.colRKoumokkumei.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colRKoumokkumei.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colRKoumokkumei.Width = 450;
            // 
            // colRCd
            // 
            this.colRCd.DataPropertyName = "R_KOUMOKU_CD";
            this.colRCd.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.colRCd.DefaultCellStyle = dataGridViewCellStyle8;
            this.colRCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("colRCd.FocusOutCheckMethod")));
            this.colRCd.HeaderText = "colRCd";
            this.colRCd.Name = "colRCd";
            this.colRCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("colRCd.PopupSearchSendParams")));
            this.colRCd.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.colRCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("colRCd.popupWindowSetting")));
            this.colRCd.ReadOnly = true;
            this.colRCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("colRCd.RegistCheckMethod")));
            this.colRCd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colRCd.Visible = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(160)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(475, 20);
            this.label2.TabIndex = 659;
            this.label2.Text = "電子契約（紐付条件）";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(530, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(475, 20);
            this.label3.TabIndex = 679;
            this.label3.Text = "環境将軍Ｒ（紐付条件）";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel2
            // 
            this.customPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel2.Controls.Add(this.KENSAKU_HOUHOU1);
            this.customPanel2.Controls.Add(this.KENSAKU_HOUHOU2);
            this.customPanel2.Location = new System.Drawing.Point(521, 20);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(250, 20);
            this.customPanel2.TabIndex = 716;
            // 
            // KENSAKU_HOUHOU1
            // 
            this.KENSAKU_HOUHOU1.AutoSize = true;
            this.KENSAKU_HOUHOU1.DBFieldsName = "";
            this.KENSAKU_HOUHOU1.DefaultBackColor = System.Drawing.Color.Empty;
            this.KENSAKU_HOUHOU1.DisplayItemName = "検索方法";
            this.KENSAKU_HOUHOU1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KENSAKU_HOUHOU1.FocusOutCheckMethod")));
            this.KENSAKU_HOUHOU1.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KENSAKU_HOUHOU1.LinkedTextBox = "KENSAKU_HOUHOU";
            this.KENSAKU_HOUHOU1.Location = new System.Drawing.Point(3, 1);
            this.KENSAKU_HOUHOU1.Name = "KENSAKU_HOUHOU1";
            this.KENSAKU_HOUHOU1.PopupAfterExecute = null;
            this.KENSAKU_HOUHOU1.PopupBeforeExecute = null;
            this.KENSAKU_HOUHOU1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KENSAKU_HOUHOU1.PopupSearchSendParams")));
            this.KENSAKU_HOUHOU1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KENSAKU_HOUHOU1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KENSAKU_HOUHOU1.popupWindowSetting")));
            this.KENSAKU_HOUHOU1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KENSAKU_HOUHOU1.RegistCheckMethod")));
            this.KENSAKU_HOUHOU1.ShortItemName = "検索方法";
            this.KENSAKU_HOUHOU1.Size = new System.Drawing.Size(95, 17);
            this.KENSAKU_HOUHOU1.TabIndex = 10;
            this.KENSAKU_HOUHOU1.Tag = "";
            this.KENSAKU_HOUHOU1.Text = "1.完全一致";
            this.KENSAKU_HOUHOU1.UseVisualStyleBackColor = true;
            this.KENSAKU_HOUHOU1.Value = "1";
            // 
            // KENSAKU_HOUHOU2
            // 
            this.KENSAKU_HOUHOU2.AutoSize = true;
            this.KENSAKU_HOUHOU2.DBFieldsName = "";
            this.KENSAKU_HOUHOU2.DefaultBackColor = System.Drawing.Color.Empty;
            this.KENSAKU_HOUHOU2.DisplayItemName = "検索方法";
            this.KENSAKU_HOUHOU2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KENSAKU_HOUHOU2.FocusOutCheckMethod")));
            this.KENSAKU_HOUHOU2.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KENSAKU_HOUHOU2.LinkedTextBox = "KENSAKU_HOUHOU";
            this.KENSAKU_HOUHOU2.Location = new System.Drawing.Point(113, 1);
            this.KENSAKU_HOUHOU2.Name = "KENSAKU_HOUHOU2";
            this.KENSAKU_HOUHOU2.PopupAfterExecute = null;
            this.KENSAKU_HOUHOU2.PopupBeforeExecute = null;
            this.KENSAKU_HOUHOU2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KENSAKU_HOUHOU2.PopupSearchSendParams")));
            this.KENSAKU_HOUHOU2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KENSAKU_HOUHOU2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KENSAKU_HOUHOU2.popupWindowSetting")));
            this.KENSAKU_HOUHOU2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KENSAKU_HOUHOU2.RegistCheckMethod")));
            this.KENSAKU_HOUHOU2.ShortItemName = "検索方法";
            this.KENSAKU_HOUHOU2.Size = new System.Drawing.Size(95, 17);
            this.KENSAKU_HOUHOU2.TabIndex = 20;
            this.KENSAKU_HOUHOU2.Tag = "";
            this.KENSAKU_HOUHOU2.Text = "2.部分一致";
            this.KENSAKU_HOUHOU2.UseVisualStyleBackColor = true;
            this.KENSAKU_HOUHOU2.Value = "2";
            // 
            // KENSAKU_HOUHOU
            // 
            this.KENSAKU_HOUHOU.BackColor = System.Drawing.SystemColors.Window;
            this.KENSAKU_HOUHOU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KENSAKU_HOUHOU.DBFieldsName = "";
            this.KENSAKU_HOUHOU.DefaultBackColor = System.Drawing.Color.Empty;
            this.KENSAKU_HOUHOU.DisplayItemName = "検索方法";
            this.KENSAKU_HOUHOU.DisplayPopUp = null;
            this.KENSAKU_HOUHOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KENSAKU_HOUHOU.FocusOutCheckMethod")));
            this.KENSAKU_HOUHOU.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KENSAKU_HOUHOU.ForeColor = System.Drawing.Color.Black;
            this.KENSAKU_HOUHOU.IsInputErrorOccured = false;
            this.KENSAKU_HOUHOU.ItemDefinedTypes = "";
            this.KENSAKU_HOUHOU.LinkedRadioButtonArray = new string[] {
        "KENSAKU_HOUHOU1",
        "KENSAKU_HOUHOU2"};
            this.KENSAKU_HOUHOU.Location = new System.Drawing.Point(502, 20);
            this.KENSAKU_HOUHOU.Name = "KENSAKU_HOUHOU";
            this.KENSAKU_HOUHOU.PopupAfterExecute = null;
            this.KENSAKU_HOUHOU.PopupBeforeExecute = null;
            this.KENSAKU_HOUHOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KENSAKU_HOUHOU.PopupSearchSendParams")));
            this.KENSAKU_HOUHOU.PopupSetFormField = "";
            this.KENSAKU_HOUHOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KENSAKU_HOUHOU.PopupWindowName = "";
            this.KENSAKU_HOUHOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KENSAKU_HOUHOU.popupWindowSetting")));
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
            this.KENSAKU_HOUHOU.RangeSetting = rangeSettingDto1;
            this.KENSAKU_HOUHOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KENSAKU_HOUHOU.RegistCheckMethod")));
            this.KENSAKU_HOUHOU.SetFormField = "";
            this.KENSAKU_HOUHOU.ShortItemName = "検索方法";
            this.KENSAKU_HOUHOU.Size = new System.Drawing.Size(20, 20);
            this.KENSAKU_HOUHOU.TabIndex = 1;
            this.KENSAKU_HOUHOU.Tag = "【1, 2】　のいずれかで入力してください";
            this.KENSAKU_HOUHOU.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.KENSAKU_HOUHOU.WordWrap = false;
            this.KENSAKU_HOUHOU.Validating += new System.ComponentModel.CancelEventHandler(this.KENSAKU_HOUHOU_Validating);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(387, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 715;
            this.label1.Text = "検索方法";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvCustomTextBoxColumn1
            // 
            this.dgvCustomTextBoxColumn1.DataPropertyName = "WANSIGN_KOUMOKU_NAME";
            this.dgvCustomTextBoxColumn1.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(160)))));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgvCustomTextBoxColumn1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn1.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn1.HeaderText = "項目名";
            this.dgvCustomTextBoxColumn1.MinimumWidth = 450;
            this.dgvCustomTextBoxColumn1.Name = "dgvCustomTextBoxColumn1";
            this.dgvCustomTextBoxColumn1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn1.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn1.popupWindowSetting")));
            this.dgvCustomTextBoxColumn1.ReadOnly = true;
            this.dgvCustomTextBoxColumn1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn1.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCustomTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCustomTextBoxColumn1.Width = 450;
            // 
            // dgvCustomTextBoxColumn2
            // 
            this.dgvCustomTextBoxColumn2.DataPropertyName = "R_KOUMOKU_NAME";
            this.dgvCustomTextBoxColumn2.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvCustomTextBoxColumn2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn2.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn2.HeaderText = "項目名";
            this.dgvCustomTextBoxColumn2.MinimumWidth = 450;
            this.dgvCustomTextBoxColumn2.Name = "dgvCustomTextBoxColumn2";
            this.dgvCustomTextBoxColumn2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn2.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn2.popupWindowSetting")));
            this.dgvCustomTextBoxColumn2.ReadOnly = true;
            this.dgvCustomTextBoxColumn2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn2.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCustomTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCustomTextBoxColumn2.Width = 450;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1018, 616);
            this.Controls.Add(this.customPanel2);
            this.Controls.Add(this.KENSAKU_HOUHOU);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvShougun);
            this.Controls.Add(this.pn_foot);
            this.Controls.Add(this.lb_title);
            this.Controls.Add(this.dgvWanSign);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1034, 654);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1034, 654);
            this.Name = "UIForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "電子契約紐付補助";
            ((System.ComponentModel.ISupportInitialize)(this.dgvWanSign)).EndInit();
            this.pn_foot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShougun)).EndInit();
            this.customPanel2.ResumeLayout(false);
            this.customPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

     

        internal r_framework.CustomControl.CustomTextBox CONDITION_ITEM;
        public r_framework.CustomControl.CustomDataGridView dgvWanSign;
        public Label lb_title;
        public Panel pn_foot;
        public r_framework.CustomControl.CustomButton bt_func12;
        public Label lb_hint;
        public r_framework.CustomControl.CustomButton bt_func11;
        public r_framework.CustomControl.CustomButton bt_func10;
        public r_framework.CustomControl.CustomButton bt_func9;
        public r_framework.CustomControl.CustomButton bt_func8;
        public r_framework.CustomControl.CustomButton bt_func7;
        public r_framework.CustomControl.CustomButton bt_func6;
        public r_framework.CustomControl.CustomButton bt_func5;
        public r_framework.CustomControl.CustomButton bt_func4;
        public r_framework.CustomControl.CustomButton bt_func3;
        public r_framework.CustomControl.CustomButton bt_func2;
        public r_framework.CustomControl.CustomButton bt_func1;
        public r_framework.CustomControl.CustomDataGridView dgvShougun;
        private Label label2;
        internal Label label3;
        private r_framework.CustomControl.CustomPanel customPanel2;
        internal r_framework.CustomControl.CustomRadioButton KENSAKU_HOUHOU1;
        internal r_framework.CustomControl.CustomRadioButton KENSAKU_HOUHOU2;
        internal r_framework.CustomControl.CustomNumericTextBox2 KENSAKU_HOUHOU;
        internal Label label1;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn1;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn2;
        private r_framework.CustomControl.DgvCustomTextBoxColumn colWkoumokumei;
        private r_framework.CustomControl.DgvCustomTextBoxColumn colWCd;
        private r_framework.CustomControl.DgvCustomTextBoxColumn colRKoumokkumei;
        private r_framework.CustomControl.DgvCustomTextBoxColumn colRCd;
    }
}