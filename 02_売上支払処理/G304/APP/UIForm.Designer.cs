using System.Windows.Forms;
using System;
using SyaryoSentaku.Const;
namespace SyaryoSentaku.App
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            this.CarIchiran = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.OUTPUT_KBN = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.customSortHeader1 = new r_framework.CustomControl.DataGridCustomControl.CustomSortHeader();
            this.lbl_kensaku = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.KENCONDITION_ITEM = new r_framework.CustomControl.CustomNumericTextBox2();
            this.KENCONDITION1 = new r_framework.CustomControl.CustomRadioButton();
            this.KENCONDITION2 = new r_framework.CustomControl.CustomRadioButton();
            this.KENCONDITION3 = new r_framework.CustomControl.CustomRadioButton();
            this.KENCONDITION4 = new r_framework.CustomControl.CustomRadioButton();
            this.KENCONDITION5 = new r_framework.CustomControl.CustomRadioButton();
            this.KENCONDITION_VALUE = new r_framework.CustomControl.CustomTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.CarIchiran)).BeginInit();
            this.pn_foot.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CarIchiran
            // 
            this.CarIchiran.AllowUserToAddRows = false;
            this.CarIchiran.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CarIchiran.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CarIchiran.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.CarIchiran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CarIchiran.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OUTPUT_KBN});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.CarIchiran.DefaultCellStyle = dataGridViewCellStyle2;
            this.CarIchiran.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.CarIchiran.EnableHeadersVisualStyles = false;
            this.CarIchiran.GridColor = System.Drawing.Color.White;
            this.CarIchiran.IsReload = false;
            this.CarIchiran.LinkedDataPanelName = "customSortHeader1";
            this.CarIchiran.Location = new System.Drawing.Point(12, 110);
            this.CarIchiran.MultiSelect = false;
            this.CarIchiran.Name = "CarIchiran";
            this.CarIchiran.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.CarIchiran.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.CarIchiran.RowHeadersVisible = false;
            this.CarIchiran.RowTemplate.Height = 21;
            this.CarIchiran.ShowCellToolTips = false;
            this.CarIchiran.Size = new System.Drawing.Size(992, 412);
            this.CarIchiran.TabIndex = 1;
            this.CarIchiran.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CarIchiran_CellDoubleClick);
            this.CarIchiran.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.CarIchiran_CellFormatting);
            // 
            // OUTPUT_KBN
            // 
            this.OUTPUT_KBN.HeaderText = "outPutKbn";
            this.OUTPUT_KBN.Name = "OUTPUT_KBN";
            this.OUTPUT_KBN.ReadOnly = true;
            this.OUTPUT_KBN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OUTPUT_KBN.Visible = false;
            // 
            // lb_title
            // 
            this.lb_title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lb_title.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_title.Font = new System.Drawing.Font("ＭＳ ゴシック", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lb_title.ForeColor = System.Drawing.Color.White;
            this.lb_title.Location = new System.Drawing.Point(12, 9);
            this.lb_title.Name = "lb_title";
            this.lb_title.Size = new System.Drawing.Size(282, 35);
            this.lb_title.TabIndex = 388;
            this.lb_title.Text = "車輌選択";
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
            this.bt_func12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.lb_hint.Font = new System.Drawing.Font("メイリオ", 9.75F);
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
            this.bt_func11.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.bt_func10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.bt_func9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.bt_func8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.bt_func7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.bt_func6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.bt_func5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.bt_func4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.bt_func3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.bt_func2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.bt_func1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            // customSortHeader1
            // 
            this.customSortHeader1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.customSortHeader1.LinkedDataGridViewName = "CarIchiran";
            this.customSortHeader1.Location = new System.Drawing.Point(12, 82);
            this.customSortHeader1.Name = "customSortHeader1";
            this.customSortHeader1.Size = new System.Drawing.Size(992, 26);
            this.customSortHeader1.SortFlag = false;
            this.customSortHeader1.TabIndex = 1;
            this.customSortHeader1.TabStop = false;
            // 
            // lbl_kensaku
            // 
            this.lbl_kensaku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_kensaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_kensaku.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_kensaku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_kensaku.ForeColor = System.Drawing.Color.White;
            this.lbl_kensaku.Location = new System.Drawing.Point(12, 52);
            this.lbl_kensaku.Name = "lbl_kensaku";
            this.lbl_kensaku.Size = new System.Drawing.Size(108, 22);
            this.lbl_kensaku.TabIndex = 406;
            this.lbl_kensaku.Text = "検索条件";
            this.lbl_kensaku.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.KENCONDITION_VALUE);
            this.panel1.Controls.Add(this.KENCONDITION5);
            this.panel1.Controls.Add(this.KENCONDITION4);
            this.panel1.Controls.Add(this.KENCONDITION3);
            this.panel1.Controls.Add(this.KENCONDITION2);
            this.panel1.Controls.Add(this.KENCONDITION1);
            this.panel1.Controls.Add(this.KENCONDITION_ITEM);
            this.panel1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.panel1.Location = new System.Drawing.Point(126, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(878, 22);
            this.panel1.TabIndex = 2;
            // 
            // KENCONDITION_ITEM
            // 
            this.KENCONDITION_ITEM.BackColor = System.Drawing.SystemColors.Window;
            this.KENCONDITION_ITEM.CharactersNumber = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.KENCONDITION_ITEM.DefaultBackColor = System.Drawing.Color.Empty;
            this.KENCONDITION_ITEM.DisplayPopUp = null;
            this.KENCONDITION_ITEM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KENCONDITION_ITEM.FocusOutCheckMethod")));
            this.KENCONDITION_ITEM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KENCONDITION_ITEM.ForeColor = System.Drawing.Color.Black;
            this.KENCONDITION_ITEM.IsInputErrorOccured = false;
            this.KENCONDITION_ITEM.LinkedRadioButtonArray = new string[] {
        "KENCONDITION1",
        "KENCONDITION2",
        "KENCONDITION3",
        "KENCONDITION4",
        "KENCONDITION5"};
            this.KENCONDITION_ITEM.Location = new System.Drawing.Point(3, 0);
            this.KENCONDITION_ITEM.Name = "KENCONDITION_ITEM";
            this.KENCONDITION_ITEM.PopupAfterExecute = null;
            this.KENCONDITION_ITEM.PopupBeforeExecute = null;
            this.KENCONDITION_ITEM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KENCONDITION_ITEM.PopupSearchSendParams")));
            this.KENCONDITION_ITEM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KENCONDITION_ITEM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KENCONDITION_ITEM.popupWindowSetting")));
            this.KENCONDITION_ITEM.prevText = null;
            this.KENCONDITION_ITEM.PrevText = "";
            rangeSettingDto1.Max = new decimal(new int[] {
            5,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.KENCONDITION_ITEM.RangeSetting = rangeSettingDto1;
            this.KENCONDITION_ITEM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KENCONDITION_ITEM.RegistCheckMethod")));
            this.KENCONDITION_ITEM.Size = new System.Drawing.Size(33, 20);
            this.KENCONDITION_ITEM.TabIndex = 0;
            this.KENCONDITION_ITEM.Tag = "【1～5】のいずれかで入力してください";
            this.KENCONDITION_ITEM.TextChanged += new System.EventHandler(this.PARENT_KENCONDITION_ITEM_TextChanged);
            // 
            // KENCONDITION1
            // 
            this.KENCONDITION1.AutoSize = true;
            this.KENCONDITION1.DefaultBackColor = System.Drawing.Color.Empty;
            this.KENCONDITION1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KENCONDITION1.FocusOutCheckMethod")));
            this.KENCONDITION1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KENCONDITION1.LinkedTextBox = "KENCONDITION_ITEM";
            this.KENCONDITION1.Location = new System.Drawing.Point(56, 1);
            this.KENCONDITION1.Name = "KENCONDITION1";
            this.KENCONDITION1.PopupAfterExecute = null;
            this.KENCONDITION1.PopupBeforeExecute = null;
            this.KENCONDITION1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KENCONDITION1.PopupSearchSendParams")));
            this.KENCONDITION1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KENCONDITION1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KENCONDITION1.popupWindowSetting")));
            this.KENCONDITION1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KENCONDITION1.RegistCheckMethod")));
            this.KENCONDITION1.Size = new System.Drawing.Size(88, 17);
            this.KENCONDITION1.TabIndex = 1;
            this.KENCONDITION1.Tag = "車輌CDが対象の場合チェックを付けてください";
            this.KENCONDITION1.Text = "1. 車輌CD";
            this.KENCONDITION1.UseVisualStyleBackColor = true;
            this.KENCONDITION1.Value = "1";
            // 
            // KENCONDITION2
            // 
            this.KENCONDITION2.AutoSize = true;
            this.KENCONDITION2.DefaultBackColor = System.Drawing.Color.Empty;
            this.KENCONDITION2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KENCONDITION2.FocusOutCheckMethod")));
            this.KENCONDITION2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KENCONDITION2.LinkedTextBox = "KENCONDITION_ITEM";
            this.KENCONDITION2.Location = new System.Drawing.Point(150, 1);
            this.KENCONDITION2.Name = "KENCONDITION2";
            this.KENCONDITION2.PopupAfterExecute = null;
            this.KENCONDITION2.PopupBeforeExecute = null;
            this.KENCONDITION2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KENCONDITION2.PopupSearchSendParams")));
            this.KENCONDITION2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KENCONDITION2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KENCONDITION2.popupWindowSetting")));
            this.KENCONDITION2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KENCONDITION2.RegistCheckMethod")));
            this.KENCONDITION2.Size = new System.Drawing.Size(88, 17);
            this.KENCONDITION2.TabIndex = 2;
            this.KENCONDITION2.Tag = "車輌名が対象の場合チェックを付けてください";
            this.KENCONDITION2.Text = "2. 車輌名";
            this.KENCONDITION2.UseVisualStyleBackColor = true;
            this.KENCONDITION2.Value = "2";
            // 
            // KENCONDITION3
            // 
            this.KENCONDITION3.AutoSize = true;
            this.KENCONDITION3.DefaultBackColor = System.Drawing.Color.Empty;
            this.KENCONDITION3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KENCONDITION3.FocusOutCheckMethod")));
            this.KENCONDITION3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KENCONDITION3.LinkedTextBox = "KENCONDITION_ITEM";
            this.KENCONDITION3.Location = new System.Drawing.Point(244, 1);
            this.KENCONDITION3.Name = "KENCONDITION3";
            this.KENCONDITION3.PopupAfterExecute = null;
            this.KENCONDITION3.PopupBeforeExecute = null;
            this.KENCONDITION3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KENCONDITION3.PopupSearchSendParams")));
            this.KENCONDITION3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KENCONDITION3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KENCONDITION3.popupWindowSetting")));
            this.KENCONDITION3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KENCONDITION3.RegistCheckMethod")));
            this.KENCONDITION3.Size = new System.Drawing.Size(88, 17);
            this.KENCONDITION3.TabIndex = 3;
            this.KENCONDITION3.Tag = "業者CDが対象の場合チェックを付けてください";
            this.KENCONDITION3.Text = "3. 業者CD";
            this.KENCONDITION3.UseVisualStyleBackColor = true;
            this.KENCONDITION3.Value = "3";
            // 
            // KENCONDITION4
            // 
            this.KENCONDITION4.AutoSize = true;
            this.KENCONDITION4.DefaultBackColor = System.Drawing.Color.Empty;
            this.KENCONDITION4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KENCONDITION4.FocusOutCheckMethod")));
            this.KENCONDITION4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KENCONDITION4.LinkedTextBox = "KENCONDITION_ITEM";
            this.KENCONDITION4.Location = new System.Drawing.Point(348, 1);
            this.KENCONDITION4.Name = "KENCONDITION4";
            this.KENCONDITION4.PopupAfterExecute = null;
            this.KENCONDITION4.PopupBeforeExecute = null;
            this.KENCONDITION4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KENCONDITION4.PopupSearchSendParams")));
            this.KENCONDITION4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KENCONDITION4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KENCONDITION4.popupWindowSetting")));
            this.KENCONDITION4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KENCONDITION4.RegistCheckMethod")));
            this.KENCONDITION4.Size = new System.Drawing.Size(88, 17);
            this.KENCONDITION4.TabIndex = 4;
            this.KENCONDITION4.Tag = "業者名が対象の場合チェックを付けてください";
            this.KENCONDITION4.Text = "4. 業者名";
            this.KENCONDITION4.UseVisualStyleBackColor = true;
            this.KENCONDITION4.Value = "4";
            // 
            // KENCONDITION5
            // 
            this.KENCONDITION5.AutoSize = true;
            this.KENCONDITION5.DefaultBackColor = System.Drawing.Color.Empty;
            this.KENCONDITION5.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KENCONDITION5.FocusOutCheckMethod")));
            this.KENCONDITION5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KENCONDITION5.LinkedTextBox = "KENCONDITION_ITEM";
            this.KENCONDITION5.Location = new System.Drawing.Point(457, 1);
            this.KENCONDITION5.Name = "KENCONDITION5";
            this.KENCONDITION5.PopupAfterExecute = null;
            this.KENCONDITION5.PopupBeforeExecute = null;
            this.KENCONDITION5.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KENCONDITION5.PopupSearchSendParams")));
            this.KENCONDITION5.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KENCONDITION5.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KENCONDITION5.popupWindowSetting")));
            this.KENCONDITION5.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KENCONDITION5.RegistCheckMethod")));
            this.KENCONDITION5.Size = new System.Drawing.Size(88, 17);
            this.KENCONDITION5.TabIndex = 5;
            this.KENCONDITION5.Tag = " フリーが対象の場合チェックを付けてください";
            this.KENCONDITION5.Text = "5. フリー";
            this.KENCONDITION5.UseVisualStyleBackColor = true;
            this.KENCONDITION5.Value = "5";
            // 
            // KENCONDITION_VALUE
            // 
            this.KENCONDITION_VALUE.BackColor = System.Drawing.SystemColors.Window;
            this.KENCONDITION_VALUE.DefaultBackColor = System.Drawing.Color.Empty;
            this.KENCONDITION_VALUE.DisplayPopUp = null;
            this.KENCONDITION_VALUE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KENCONDITION_VALUE.FocusOutCheckMethod")));
            this.KENCONDITION_VALUE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KENCONDITION_VALUE.ForeColor = System.Drawing.Color.Black;
            this.KENCONDITION_VALUE.IsInputErrorOccured = false;
            this.KENCONDITION_VALUE.Location = new System.Drawing.Point(544, 1);
            this.KENCONDITION_VALUE.Name = "KENCONDITION_VALUE";
            this.KENCONDITION_VALUE.PopupAfterExecute = null;
            this.KENCONDITION_VALUE.PopupBeforeExecute = null;
            this.KENCONDITION_VALUE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KENCONDITION_VALUE.PopupSearchSendParams")));
            this.KENCONDITION_VALUE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KENCONDITION_VALUE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KENCONDITION_VALUE.popupWindowSetting")));
            this.KENCONDITION_VALUE.prevText = null;
            this.KENCONDITION_VALUE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KENCONDITION_VALUE.RegistCheckMethod")));
            this.KENCONDITION_VALUE.Size = new System.Drawing.Size(316, 20);
            this.KENCONDITION_VALUE.TabIndex = 6;
            this.KENCONDITION_VALUE.Tag = "検索文字列を入力してください";
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1018, 616);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbl_kensaku);
            this.Controls.Add(this.customSortHeader1);
            this.Controls.Add(this.pn_foot);
            this.Controls.Add(this.lb_title);
            this.Controls.Add(this.CarIchiran);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UIForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "車輌選択";
            this.WindowId = r_framework.Const.WINDOW_ID.T_SYARYOU_SENTAKU;
            ((System.ComponentModel.ISupportInitialize)(this.CarIchiran)).EndInit();
            this.pn_foot.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private void TextBoxDec_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void TextBoxText_KeyPress(object sender, KeyPressEventArgs e)
        {
            //0～9と、バックスペース以外の時は、イベントをキャンセルする
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = false;
            }

        }

        //private void dataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        //{
        //    if (e.Control is DataGridViewTextBoxEditingControl)
        //    {
        //        e.Control.KeyPress -= new KeyPressEventHandler(TextBoxDec_KeyPress);
        //        e.Control.KeyPress -= new KeyPressEventHandler(TextBoxText_KeyPress);

        //        if (this.CarIchiran.CurrentCell.ColumnIndex == 2)
        //        {
        //            e.Control.ImeMode = ImeMode.Alpha;
        //            e.Control.KeyPress += new KeyPressEventHandler(TextBoxDec_KeyPress);
        //        }

        //        if (this.CarIchiran.CurrentCell.ColumnIndex == 3 || this.CarIchiran.CurrentCell.ColumnIndex == 4)
        //        {
        //            e.Control.ImeMode = ImeMode.Hiragana;
        //            e.Control.KeyPress += new KeyPressEventHandler(TextBoxText_KeyPress);
        //        }
        //    }
        //}
        internal r_framework.CustomControl.CustomTextBox CONDITION_ITEM;
        public r_framework.CustomControl.CustomDataGridView CarIchiran;
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
        private r_framework.CustomControl.DataGridCustomControl.CustomSortHeader customSortHeader1;
        private DataGridViewTextBoxColumn OUTPUT_KBN;
        internal Label lbl_kensaku;
        internal r_framework.CustomControl.CustomRadioButton KENCONDITION1;
        internal r_framework.CustomControl.CustomRadioButton KENCONDITION3;
        internal r_framework.CustomControl.CustomRadioButton KENCONDITION2;
        internal r_framework.CustomControl.CustomRadioButton KENCONDITION4;
        internal r_framework.CustomControl.CustomRadioButton KENCONDITION5;
        internal r_framework.CustomControl.CustomTextBox KENCONDITION_VALUE;
        public r_framework.CustomControl.CustomNumericTextBox2 KENCONDITION_ITEM;
        internal Panel panel1;
    }
}