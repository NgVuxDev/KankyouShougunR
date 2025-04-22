namespace KensakuKyoutsuuPopup1.APP
{
    partial class KensakuKyoutsuuPopupForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            this.lb_title = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.CONDITION_VALUE = new r_framework.CustomControl.CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            this.customDataGridView1 = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.customSortHeader1 = new r_framework.CustomControl.DataGridCustomControl.CustomSortHeader();
            this.FILTER_SHIIN_VALUE = new r_framework.CustomControl.CustomNumericTextBox2();
            this.SHIIN1 = new r_framework.CustomControl.CustomRadioButton();
            this.SHIIN2 = new r_framework.CustomControl.CustomRadioButton();
            this.SHIIN3 = new r_framework.CustomControl.CustomRadioButton();
            this.SHIIN4 = new r_framework.CustomControl.CustomRadioButton();
            this.SHIIN5 = new r_framework.CustomControl.CustomRadioButton();
            this.FILTER_BOIN_VALUE = new r_framework.CustomControl.CustomNumericTextBox2();
            this.BOIN1 = new r_framework.CustomControl.CustomRadioButton();
            this.BOIN2 = new r_framework.CustomControl.CustomRadioButton();
            this.BOIN3 = new r_framework.CustomControl.CustomRadioButton();
            this.BOIN4 = new r_framework.CustomControl.CustomRadioButton();
            this.BOIN5 = new r_framework.CustomControl.CustomRadioButton();
            this.BOIN6 = new r_framework.CustomControl.CustomRadioButton();
            this.BOIN7 = new r_framework.CustomControl.CustomRadioButton();
            this.BOIN8 = new r_framework.CustomControl.CustomRadioButton();
            this.BOIN9 = new r_framework.CustomControl.CustomRadioButton();
            this.BOIN10 = new r_framework.CustomControl.CustomRadioButton();
            this.BOIN11 = new r_framework.CustomControl.CustomRadioButton();
            this.BOIN12 = new r_framework.CustomControl.CustomRadioButton();
            this.CONDITION_ITEM = new r_framework.CustomControl.CustomNumericTextBox2();
            this.CONDITION1 = new r_framework.CustomControl.CustomRadioButton();
            this.CONDITION2 = new r_framework.CustomControl.CustomRadioButton();
            this.CONDITION3 = new r_framework.CustomControl.CustomRadioButton();
            this.CONDITION4 = new r_framework.CustomControl.CustomRadioButton();
            this.CONDITION5 = new r_framework.CustomControl.CustomRadioButton();
            this.CONDITION6 = new r_framework.CustomControl.CustomRadioButton();
            this.CONDITION7 = new r_framework.CustomControl.CustomRadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.imeStatus = new r_framework.Components.ImeStatus(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_title
            // 
            this.lb_title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lb_title.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_title.Font = new System.Drawing.Font("ＭＳ ゴシック", 20F, System.Drawing.FontStyle.Bold);
            this.lb_title.ForeColor = System.Drawing.Color.White;
            this.lb_title.Location = new System.Drawing.Point(12, 9);
            this.lb_title.Name = "lb_title";
            this.lb_title.Size = new System.Drawing.Size(232, 31);
            this.lb_title.TabIndex = 380;
            this.lb_title.Text = "○○○検索";
            this.lb_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label16.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(12, 54);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(93, 20);
            this.label16.TabIndex = 401;
            this.label16.Text = "検索条件";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CONDITION_VALUE
            // 
            this.CONDITION_VALUE.BackColor = System.Drawing.SystemColors.Window;
            this.CONDITION_VALUE.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION_VALUE.DisplayPopUp = null;
            this.CONDITION_VALUE.Location = new System.Drawing.Point(517, 3);
            this.CONDITION_VALUE.Name = "CONDITION_VALUE";
            this.CONDITION_VALUE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION_VALUE.Size = new System.Drawing.Size(260, 19);
            this.CONDITION_VALUE.TabIndex = 9;
            this.CONDITION_VALUE.Tag = "検索文字列を入力してください";
            this.CONDITION_VALUE.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CONDITION_VALUE_KeyUp);
            this.CONDITION_VALUE.Leave += new System.EventHandler(this.CONDITION_VALUE_OnLeave);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 20);
            this.label1.TabIndex = 401;
            this.label1.Text = "ﾌﾘｶﾞﾅ頭文字(母音)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 20);
            this.label2.TabIndex = 401;
            this.label2.Text = "ﾌﾘｶﾞﾅ頭文字(子音)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_func12
            // 
            this.bt_func12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func12.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func12.Enabled = false;
            this.bt_func12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_func12.Location = new System.Drawing.Point(921, 583);
            this.bt_func12.Name = "bt_func12";
            this.bt_func12.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func12.Size = new System.Drawing.Size(80, 35);
            this.bt_func12.TabIndex = 913;
            this.bt_func12.TabStop = false;
            this.bt_func12.Tag = "";
            this.bt_func12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func12.UseVisualStyleBackColor = false;
            this.bt_func12.Click += new System.EventHandler(this.Close);
            // 
            // lb_hint
            // 
            this.lb_hint.BackColor = System.Drawing.Color.Black;
            this.lb_hint.Font = new System.Drawing.Font("メイリオ", 9.75F);
            this.lb_hint.ForeColor = System.Drawing.Color.Yellow;
            this.lb_hint.Location = new System.Drawing.Point(12, 558);
            this.lb_hint.Name = "lb_hint";
            this.lb_hint.Size = new System.Drawing.Size(989, 21);
            this.lb_hint.TabIndex = 428;
            this.lb_hint.Text = "１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０";
            this.lb_hint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bt_func11
            // 
            this.bt_func11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func11.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func11.Enabled = false;
            this.bt_func11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_func11.Location = new System.Drawing.Point(840, 583);
            this.bt_func11.Name = "bt_func11";
            this.bt_func11.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func11.Size = new System.Drawing.Size(80, 35);
            this.bt_func11.TabIndex = 912;
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
            this.bt_func10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_func10.Location = new System.Drawing.Point(759, 583);
            this.bt_func10.Name = "bt_func10";
            this.bt_func10.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func10.Size = new System.Drawing.Size(80, 35);
            this.bt_func10.TabIndex = 911;
            this.bt_func10.TabStop = false;
            this.bt_func10.Tag = "";
            this.bt_func10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func10.UseVisualStyleBackColor = false;
            this.bt_func10.Click += new System.EventHandler(this.MoveToSort);
            // 
            // bt_func9
            // 
            this.bt_func9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func9.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func9.Enabled = false;
            this.bt_func9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_func9.Location = new System.Drawing.Point(678, 583);
            this.bt_func9.Name = "bt_func9";
            this.bt_func9.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func9.Size = new System.Drawing.Size(80, 35);
            this.bt_func9.TabIndex = 910;
            this.bt_func9.TabStop = false;
            this.bt_func9.Tag = "";
            this.bt_func9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func9.UseVisualStyleBackColor = false;
            this.bt_func9.Click += new System.EventHandler(this.Selected);
            // 
            // bt_func8
            // 
            this.bt_func8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func8.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func8.Enabled = false;
            this.bt_func8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_func8.Location = new System.Drawing.Point(588, 583);
            this.bt_func8.Name = "bt_func8";
            this.bt_func8.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func8.Size = new System.Drawing.Size(80, 35);
            this.bt_func8.TabIndex = 908;
            this.bt_func8.TabStop = false;
            this.bt_func8.Tag = "";
            this.bt_func8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func8.UseVisualStyleBackColor = false;
            this.bt_func8.Click += new System.EventHandler(this.Search);
            // 
            // bt_func7
            // 
            this.bt_func7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func7.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func7.Enabled = false;
            this.bt_func7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_func7.Location = new System.Drawing.Point(507, 583);
            this.bt_func7.Name = "bt_func7";
            this.bt_func7.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func7.Size = new System.Drawing.Size(80, 35);
            this.bt_func7.TabIndex = 907;
            this.bt_func7.TabStop = false;
            this.bt_func7.Tag = "";
            this.bt_func7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func7.UseVisualStyleBackColor = false;
            this.bt_func7.Click += new System.EventHandler(this.Clear);
            // 
            // bt_func6
            // 
            this.bt_func6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func6.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func6.Enabled = false;
            this.bt_func6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_func6.Location = new System.Drawing.Point(426, 583);
            this.bt_func6.Name = "bt_func6";
            this.bt_func6.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func6.Size = new System.Drawing.Size(80, 35);
            this.bt_func6.TabIndex = 906;
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
            this.bt_func5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_func5.Location = new System.Drawing.Point(345, 583);
            this.bt_func5.Name = "bt_func5";
            this.bt_func5.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func5.Size = new System.Drawing.Size(80, 35);
            this.bt_func5.TabIndex = 905;
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
            this.bt_func4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_func4.Location = new System.Drawing.Point(255, 583);
            this.bt_func4.Name = "bt_func4";
            this.bt_func4.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func4.Size = new System.Drawing.Size(80, 35);
            this.bt_func4.TabIndex = 904;
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
            this.bt_func3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_func3.Location = new System.Drawing.Point(174, 583);
            this.bt_func3.Name = "bt_func3";
            this.bt_func3.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func3.Size = new System.Drawing.Size(80, 35);
            this.bt_func3.TabIndex = 903;
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
            this.bt_func2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_func2.Location = new System.Drawing.Point(93, 583);
            this.bt_func2.Name = "bt_func2";
            this.bt_func2.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func2.Size = new System.Drawing.Size(80, 35);
            this.bt_func2.TabIndex = 902;
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
            this.bt_func1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_func1.Location = new System.Drawing.Point(12, 583);
            this.bt_func1.Name = "bt_func1";
            this.bt_func1.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func1.Size = new System.Drawing.Size(80, 35);
            this.bt_func1.TabIndex = 901;
            this.bt_func1.TabStop = false;
            this.bt_func1.Tag = "";
            this.bt_func1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func1.UseVisualStyleBackColor = false;
            // 
            // customDataGridView1
            // 
            this.customDataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.customDataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.customDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
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
            this.customDataGridView1.LinkedDataPanelName = "customSortHeader1";
            this.customDataGridView1.Location = new System.Drawing.Point(12, 198);
            this.customDataGridView1.Name = "customDataGridView1";
            this.customDataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.customDataGridView1.RowHeadersVisible = false;
            this.customDataGridView1.RowTemplate.Height = 21;
            this.customDataGridView1.Size = new System.Drawing.Size(985, 357);
            this.customDataGridView1.TabIndex = 31;
            this.customDataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DetailCellDoubleClick);
            this.customDataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DetailKeyDown);
            this.customDataGridView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DetailKeyUp);
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.LinkedDataGridViewName = "customDataGridView1";
            this.customSortHeader1.Location = new System.Drawing.Point(12, 150);
            this.customSortHeader1.Name = "customSortHeader1";
            this.customSortHeader1.Size = new System.Drawing.Size(985, 25);
            this.customSortHeader1.SortFlag = false;
            this.customSortHeader1.TabIndex = 499;
            this.customSortHeader1.TabStop = false;
            // 
            // FILTER_SHIIN_VALUE
            // 
            this.FILTER_SHIIN_VALUE.BackColor = System.Drawing.SystemColors.Window;
            this.FILTER_SHIIN_VALUE.CharacterLimitList = new char[] {
        '1',
        '2',
        '3',
        '4',
        '5'};
            this.FILTER_SHIIN_VALUE.CharactersNumber = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FILTER_SHIIN_VALUE.DefaultBackColor = System.Drawing.Color.Empty;
            this.FILTER_SHIIN_VALUE.DisplayPopUp = null;
            this.FILTER_SHIIN_VALUE.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.FILTER_SHIIN_VALUE.LinkedRadioButtonArray = new string[] {
        "SHIIN1",
        "SHIIN2",
        "SHIIN3",
        "SHIIN4",
        "SHIIN5"};
            this.FILTER_SHIIN_VALUE.Location = new System.Drawing.Point(2, 4);
            this.FILTER_SHIIN_VALUE.MinusEnableFlag = false;
            this.FILTER_SHIIN_VALUE.Name = "FILTER_SHIIN_VALUE";
            this.FILTER_SHIIN_VALUE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.FILTER_SHIIN_VALUE.PrevText = "";
            this.FILTER_SHIIN_VALUE.RangeLimitFlag = true;
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
            this.FILTER_SHIIN_VALUE.RangeSetting = rangeSettingDto1;
            this.FILTER_SHIIN_VALUE.Size = new System.Drawing.Size(29, 19);
            this.FILTER_SHIIN_VALUE.TabIndex = 25;
            this.FILTER_SHIIN_VALUE.Tag = "【1～5】のいずれかで入力して下さい";
            this.FILTER_SHIIN_VALUE.ModifiedChanged += new System.EventHandler(this.FILTER_SHIIN_VALUE_Modified);
            this.FILTER_SHIIN_VALUE.TextChanged += new System.EventHandler(this.InitialSort);
            // 
            // SHIIN1
            // 
            this.SHIIN1.AutoSize = true;
            this.SHIIN1.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHIIN1.LinkedTextBox = "FILTER_SHIIN_VALUE";
            this.SHIIN1.Location = new System.Drawing.Point(37, 3);
            this.SHIIN1.Name = "SHIIN1";
            this.SHIIN1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHIIN1.Size = new System.Drawing.Size(31, 16);
            this.SHIIN1.TabIndex = 26;
            this.SHIIN1.Text = "1.";
            this.SHIIN1.UseVisualStyleBackColor = true;
            this.SHIIN1.Value = "1";
            // 
            // SHIIN2
            // 
            this.SHIIN2.AutoSize = true;
            this.SHIIN2.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHIIN2.LinkedTextBox = "FILTER_SHIIN_VALUE";
            this.SHIIN2.Location = new System.Drawing.Point(95, 3);
            this.SHIIN2.Name = "SHIIN2";
            this.SHIIN2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHIIN2.Size = new System.Drawing.Size(31, 16);
            this.SHIIN2.TabIndex = 27;
            this.SHIIN2.Text = "2.";
            this.SHIIN2.UseVisualStyleBackColor = true;
            this.SHIIN2.Value = "2";
            // 
            // SHIIN3
            // 
            this.SHIIN3.AutoSize = true;
            this.SHIIN3.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHIIN3.LinkedTextBox = "FILTER_SHIIN_VALUE";
            this.SHIIN3.Location = new System.Drawing.Point(149, 3);
            this.SHIIN3.Name = "SHIIN3";
            this.SHIIN3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHIIN3.Size = new System.Drawing.Size(31, 16);
            this.SHIIN3.TabIndex = 28;
            this.SHIIN3.Text = "3.";
            this.SHIIN3.UseVisualStyleBackColor = true;
            this.SHIIN3.Value = "3";
            // 
            // SHIIN4
            // 
            this.SHIIN4.AutoSize = true;
            this.SHIIN4.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHIIN4.LinkedTextBox = "FILTER_SHIIN_VALUE";
            this.SHIIN4.Location = new System.Drawing.Point(204, 3);
            this.SHIIN4.Name = "SHIIN4";
            this.SHIIN4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHIIN4.Size = new System.Drawing.Size(31, 16);
            this.SHIIN4.TabIndex = 29;
            this.SHIIN4.Text = "4.";
            this.SHIIN4.UseVisualStyleBackColor = true;
            this.SHIIN4.Value = "4";
            // 
            // SHIIN5
            // 
            this.SHIIN5.AutoSize = true;
            this.SHIIN5.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHIIN5.LinkedTextBox = "FILTER_SHIIN_VALUE";
            this.SHIIN5.Location = new System.Drawing.Point(257, 3);
            this.SHIIN5.Name = "SHIIN5";
            this.SHIIN5.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHIIN5.Size = new System.Drawing.Size(31, 16);
            this.SHIIN5.TabIndex = 30;
            this.SHIIN5.Text = "5.";
            this.SHIIN5.UseVisualStyleBackColor = true;
            this.SHIIN5.Value = "5";
            // 
            // FILTER_BOIN_VALUE
            // 
            this.FILTER_BOIN_VALUE.BackColor = System.Drawing.SystemColors.Window;
            this.FILTER_BOIN_VALUE.CharacterLimitList = new char[] {
        '0',
        '1',
        '2',
        '3',
        '4',
        '5',
        '6',
        '7',
        '8',
        '9'};
            this.FILTER_BOIN_VALUE.CharactersNumber = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.FILTER_BOIN_VALUE.DefaultBackColor = System.Drawing.Color.Empty;
            this.FILTER_BOIN_VALUE.DisplayPopUp = null;
            this.FILTER_BOIN_VALUE.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.FILTER_BOIN_VALUE.LinkedRadioButtonArray = new string[] {
        "BOIN1",
        "BOIN2",
        "BOIN3",
        "BOIN4",
        "BOIN5",
        "BOIN6",
        "BOIN7",
        "BOIN8",
        "BOIN9",
        "BOIN10",
        "BOIN11",
        "BOIN12"};
            this.FILTER_BOIN_VALUE.Location = new System.Drawing.Point(2, 6);
            this.FILTER_BOIN_VALUE.MinusEnableFlag = false;
            this.FILTER_BOIN_VALUE.Name = "FILTER_BOIN_VALUE";
            this.FILTER_BOIN_VALUE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.FILTER_BOIN_VALUE.PrevText = "";
            this.FILTER_BOIN_VALUE.RangeLimitFlag = true;
            rangeSettingDto2.Max = new decimal(new int[] {
            12,
            0,
            0,
            0});
            rangeSettingDto2.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FILTER_BOIN_VALUE.RangeSetting = rangeSettingDto2;
            this.FILTER_BOIN_VALUE.Size = new System.Drawing.Size(29, 19);
            this.FILTER_BOIN_VALUE.TabIndex = 11;
            this.FILTER_BOIN_VALUE.Tag = "【1～12】のいずれかで入力して下さい";
            this.FILTER_BOIN_VALUE.ModifiedChanged += new System.EventHandler(this.FILTER_BOIN_VALUE_Modified);
            this.FILTER_BOIN_VALUE.TextChanged += new System.EventHandler(this.FILTER_BOIN_VALUE_Changed);
            // 
            // BOIN1
            // 
            this.BOIN1.AutoSize = true;
            this.BOIN1.DefaultBackColor = System.Drawing.Color.Empty;
            this.BOIN1.LinkedTextBox = "FILTER_BOIN_VALUE";
            this.BOIN1.Location = new System.Drawing.Point(37, 6);
            this.BOIN1.Name = "BOIN1";
            this.BOIN1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BOIN1.Size = new System.Drawing.Size(48, 16);
            this.BOIN1.TabIndex = 12;
            this.BOIN1.Tag = "ア行が対象の場合チェックを付けて下さい";
            this.BOIN1.Text = "1.　ア";
            this.BOIN1.UseVisualStyleBackColor = true;
            this.BOIN1.Value = "1";
            // 
            // BOIN2
            // 
            this.BOIN2.AutoSize = true;
            this.BOIN2.DefaultBackColor = System.Drawing.Color.Empty;
            this.BOIN2.LinkedTextBox = "FILTER_BOIN_VALUE";
            this.BOIN2.Location = new System.Drawing.Point(95, 6);
            this.BOIN2.Name = "BOIN2";
            this.BOIN2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BOIN2.Size = new System.Drawing.Size(48, 16);
            this.BOIN2.TabIndex = 13;
            this.BOIN2.Tag = "カ行が対象の場合チェックを付けて下さい";
            this.BOIN2.Text = "2.　カ";
            this.BOIN2.UseVisualStyleBackColor = true;
            this.BOIN2.Value = "2";
            // 
            // BOIN3
            // 
            this.BOIN3.AutoSize = true;
            this.BOIN3.DefaultBackColor = System.Drawing.Color.Empty;
            this.BOIN3.LinkedTextBox = "FILTER_BOIN_VALUE";
            this.BOIN3.Location = new System.Drawing.Point(149, 6);
            this.BOIN3.Name = "BOIN3";
            this.BOIN3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BOIN3.Size = new System.Drawing.Size(49, 16);
            this.BOIN3.TabIndex = 14;
            this.BOIN3.Tag = "サ行が対象の場合チェックを付けて下さい";
            this.BOIN3.Text = "3.　サ";
            this.BOIN3.UseVisualStyleBackColor = true;
            this.BOIN3.Value = "3";
            // 
            // BOIN4
            // 
            this.BOIN4.AutoSize = true;
            this.BOIN4.DefaultBackColor = System.Drawing.Color.Empty;
            this.BOIN4.LinkedTextBox = "FILTER_BOIN_VALUE";
            this.BOIN4.Location = new System.Drawing.Point(204, 6);
            this.BOIN4.Name = "BOIN4";
            this.BOIN4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BOIN4.Size = new System.Drawing.Size(47, 16);
            this.BOIN4.TabIndex = 15;
            this.BOIN4.Tag = "タ行が対象の場合チェックを付けて下さい";
            this.BOIN4.Text = "4.　タ";
            this.BOIN4.UseVisualStyleBackColor = true;
            this.BOIN4.Value = "4";
            // 
            // BOIN5
            // 
            this.BOIN5.AutoSize = true;
            this.BOIN5.DefaultBackColor = System.Drawing.Color.Empty;
            this.BOIN5.LinkedTextBox = "FILTER_BOIN_VALUE";
            this.BOIN5.Location = new System.Drawing.Point(257, 6);
            this.BOIN5.Name = "BOIN5";
            this.BOIN5.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BOIN5.Size = new System.Drawing.Size(49, 16);
            this.BOIN5.TabIndex = 16;
            this.BOIN5.Tag = "ナ行が対象の場合チェックを付けて下さい";
            this.BOIN5.Text = "5.　ナ";
            this.BOIN5.UseVisualStyleBackColor = true;
            this.BOIN5.Value = "5";
            // 
            // BOIN6
            // 
            this.BOIN6.AutoSize = true;
            this.BOIN6.DefaultBackColor = System.Drawing.Color.Empty;
            this.BOIN6.LinkedTextBox = "FILTER_BOIN_VALUE";
            this.BOIN6.Location = new System.Drawing.Point(312, 6);
            this.BOIN6.Name = "BOIN6";
            this.BOIN6.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BOIN6.Size = new System.Drawing.Size(49, 16);
            this.BOIN6.TabIndex = 17;
            this.BOIN6.Tag = "ハ行が対象の場合チェックを付けて下さい";
            this.BOIN6.Text = "6.　ハ";
            this.BOIN6.UseVisualStyleBackColor = true;
            this.BOIN6.Value = "6";
            // 
            // BOIN7
            // 
            this.BOIN7.AutoSize = true;
            this.BOIN7.DefaultBackColor = System.Drawing.Color.Empty;
            this.BOIN7.LinkedTextBox = "FILTER_BOIN_VALUE";
            this.BOIN7.Location = new System.Drawing.Point(367, 6);
            this.BOIN7.Name = "BOIN7";
            this.BOIN7.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BOIN7.Size = new System.Drawing.Size(48, 16);
            this.BOIN7.TabIndex = 18;
            this.BOIN7.Tag = "マ行が対象の場合チェックを付けて下さい";
            this.BOIN7.Text = "7.　マ";
            this.BOIN7.UseVisualStyleBackColor = true;
            this.BOIN7.Value = "7";
            // 
            // BOIN8
            // 
            this.BOIN8.AutoSize = true;
            this.BOIN8.DefaultBackColor = System.Drawing.Color.Empty;
            this.BOIN8.LinkedTextBox = "FILTER_BOIN_VALUE";
            this.BOIN8.Location = new System.Drawing.Point(421, 6);
            this.BOIN8.Name = "BOIN8";
            this.BOIN8.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BOIN8.Size = new System.Drawing.Size(49, 16);
            this.BOIN8.TabIndex = 19;
            this.BOIN8.Tag = "ヤ行が対象の場合チェックを付けて下さい";
            this.BOIN8.Text = "8.　ヤ";
            this.BOIN8.UseVisualStyleBackColor = true;
            this.BOIN8.Value = "8";
            // 
            // BOIN9
            // 
            this.BOIN9.AutoSize = true;
            this.BOIN9.DefaultBackColor = System.Drawing.Color.Empty;
            this.BOIN9.LinkedTextBox = "FILTER_BOIN_VALUE";
            this.BOIN9.Location = new System.Drawing.Point(476, 6);
            this.BOIN9.Name = "BOIN9";
            this.BOIN9.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BOIN9.Size = new System.Drawing.Size(47, 16);
            this.BOIN9.TabIndex = 20;
            this.BOIN9.Tag = "ラ行が対象の場合チェックを付けて下さい";
            this.BOIN9.Text = "9.　ラ";
            this.BOIN9.UseVisualStyleBackColor = true;
            this.BOIN9.Value = "9";
            // 
            // BOIN10
            // 
            this.BOIN10.AutoSize = true;
            this.BOIN10.DefaultBackColor = System.Drawing.Color.Empty;
            this.BOIN10.LinkedTextBox = "FILTER_BOIN_VALUE";
            this.BOIN10.Location = new System.Drawing.Point(529, 6);
            this.BOIN10.Name = "BOIN10";
            this.BOIN10.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BOIN10.Size = new System.Drawing.Size(54, 16);
            this.BOIN10.TabIndex = 21;
            this.BOIN10.Tag = "ワ行が対象の場合チェックを付けて下さい";
            this.BOIN10.Text = "10.　ワ";
            this.BOIN10.UseVisualStyleBackColor = true;
            this.BOIN10.Value = "10";
            // 
            // BOIN11
            // 
            this.BOIN11.AutoSize = true;
            this.BOIN11.DefaultBackColor = System.Drawing.Color.Empty;
            this.BOIN11.LinkedTextBox = "FILTER_BOIN_VALUE";
            this.BOIN11.Location = new System.Drawing.Point(589, 6);
            this.BOIN11.Name = "BOIN11";
            this.BOIN11.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BOIN11.Size = new System.Drawing.Size(57, 16);
            this.BOIN11.TabIndex = 22;
            this.BOIN11.Tag = "英字が対象の場合チェックを付けて下さい";
            this.BOIN11.Text = "11.　英";
            this.BOIN11.UseVisualStyleBackColor = true;
            this.BOIN11.Value = "11";
            // 
            // BOIN12
            // 
            this.BOIN12.AutoSize = true;
            this.BOIN12.DefaultBackColor = System.Drawing.Color.Empty;
            this.BOIN12.LinkedTextBox = "FILTER_BOIN_VALUE";
            this.BOIN12.Location = new System.Drawing.Point(652, 6);
            this.BOIN12.Name = "BOIN12";
            this.BOIN12.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BOIN12.Size = new System.Drawing.Size(57, 16);
            this.BOIN12.TabIndex = 23;
            this.BOIN12.Tag = "いずれにも該当しない場合チェックを付けて下さい";
            this.BOIN12.Text = "12.　他";
            this.BOIN12.UseVisualStyleBackColor = true;
            this.BOIN12.Value = "12";
            // 
            // CONDITION_ITEM
            // 
            this.CONDITION_ITEM.BackColor = System.Drawing.SystemColors.Window;
            this.CONDITION_ITEM.CharacterLimitList = new char[] {
        '1',
        '2',
        '3',
        '4',
        '5',
        '6',
        '7'};
            this.CONDITION_ITEM.CharactersNumber = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CONDITION_ITEM.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION_ITEM.DisplayPopUp = null;
            this.CONDITION_ITEM.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.CONDITION_ITEM.LinkedRadioButtonArray = new string[] {
        "CONDITION1",
        "CONDITION2",
        "CONDITION3",
        "CONDITION4",
        "CONDITION5",
        "CONDITION6",
        "CONDITION7"};
            this.CONDITION_ITEM.Location = new System.Drawing.Point(3, 5);
            this.CONDITION_ITEM.MinusEnableFlag = false;
            this.CONDITION_ITEM.Name = "CONDITION_ITEM";
            this.CONDITION_ITEM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION_ITEM.PrevText = "";
            this.CONDITION_ITEM.RangeLimitFlag = true;
            rangeSettingDto3.Max = new decimal(new int[] {
            7,
            0,
            0,
            0});
            rangeSettingDto3.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CONDITION_ITEM.RangeSetting = rangeSettingDto3;
            this.CONDITION_ITEM.Size = new System.Drawing.Size(29, 19);
            this.CONDITION_ITEM.TabIndex = 1;
            this.CONDITION_ITEM.Tag = "【1～7】のいずれかで入力して下さい";
            this.CONDITION_ITEM.ModifiedChanged += new System.EventHandler(this.CONDITION_ITEM_Modified);
            this.CONDITION_ITEM.TextChanged += new System.EventHandler(this.PARENT_CONDITION_ITEM_TextChanged);
            // 
            // CONDITION1
            // 
            this.CONDITION1.AutoSize = true;
            this.CONDITION1.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION1.LinkedTextBox = "CONDITION_ITEM";
            this.CONDITION1.Location = new System.Drawing.Point(38, 5);
            this.CONDITION1.Name = "CONDITION1";
            this.CONDITION1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION1.Size = new System.Drawing.Size(58, 16);
            this.CONDITION1.TabIndex = 2;
            this.CONDITION1.Tag = "コードが対象の場合チェックを付けて下さい";
            this.CONDITION1.Text = "1. ｺｰﾄﾞ";
            this.CONDITION1.UseVisualStyleBackColor = true;
            this.CONDITION1.Value = "1";
            // 
            // CONDITION2
            // 
            this.CONDITION2.AutoSize = true;
            this.CONDITION2.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION2.LinkedTextBox = "CONDITION_ITEM";
            this.CONDITION2.Location = new System.Drawing.Point(102, 5);
            this.CONDITION2.Name = "CONDITION2";
            this.CONDITION2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION2.Size = new System.Drawing.Size(59, 16);
            this.CONDITION2.TabIndex = 3;
            this.CONDITION2.Tag = "略称が対象の場合チェックを付けて下さい";
            this.CONDITION2.Text = "2. 略称";
            this.CONDITION2.UseVisualStyleBackColor = true;
            this.CONDITION2.Value = "2";
            // 
            // CONDITION3
            // 
            this.CONDITION3.AutoSize = true;
            this.CONDITION3.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION3.LinkedTextBox = "CONDITION_ITEM";
            this.CONDITION3.Location = new System.Drawing.Point(167, 5);
            this.CONDITION3.Name = "CONDITION3";
            this.CONDITION3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION3.Size = new System.Drawing.Size(66, 16);
            this.CONDITION3.TabIndex = 4;
            this.CONDITION3.Tag = "ﾌﾘｶﾞﾅが対象の場合チェックを付けて下さい";
            this.CONDITION3.Text = "3. ﾌﾘｶﾞﾅ";
            this.CONDITION3.UseVisualStyleBackColor = true;
            this.CONDITION3.Value = "3";
            // 
            // CONDITION4
            // 
            this.CONDITION4.AutoSize = true;
            this.CONDITION4.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION4.LinkedTextBox = "CONDITION_ITEM";
            this.CONDITION4.Location = new System.Drawing.Point(239, 5);
            this.CONDITION4.Name = "CONDITION4";
            this.CONDITION4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION4.Size = new System.Drawing.Size(83, 16);
            this.CONDITION4.TabIndex = 5;
            this.CONDITION4.Tag = "都道府県が対象の場合チェックを付けて下さい";
            this.CONDITION4.Text = "4. 都道府県";
            this.CONDITION4.UseVisualStyleBackColor = true;
            this.CONDITION4.Value = "4";
            // 
            // CONDITION5
            // 
            this.CONDITION5.AutoSize = true;
            this.CONDITION5.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION5.LinkedTextBox = "CONDITION_ITEM";
            this.CONDITION5.Location = new System.Drawing.Point(326, 5);
            this.CONDITION5.Name = "CONDITION5";
            this.CONDITION5.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION5.Size = new System.Drawing.Size(59, 16);
            this.CONDITION5.TabIndex = 6;
            this.CONDITION5.Tag = "住所が対象の場合チェックを付けて下さい";
            this.CONDITION5.Text = "5. 住所";
            this.CONDITION5.UseVisualStyleBackColor = true;
            this.CONDITION5.Value = "5";
            // 
            // CONDITION6
            // 
            this.CONDITION6.AutoSize = true;
            this.CONDITION6.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION6.LinkedTextBox = "CONDITION_ITEM";
            this.CONDITION6.Location = new System.Drawing.Point(391, 5);
            this.CONDITION6.Name = "CONDITION6";
            this.CONDITION6.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION6.Size = new System.Drawing.Size(59, 16);
            this.CONDITION6.TabIndex = 7;
            this.CONDITION6.Tag = "電話が対象の場合チェックを付けて下さい";
            this.CONDITION6.Text = "6. 電話";
            this.CONDITION6.UseVisualStyleBackColor = true;
            this.CONDITION6.Value = "6";
            // 
            // CONDITION7
            // 
            this.CONDITION7.AutoSize = true;
            this.CONDITION7.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION7.LinkedTextBox = "CONDITION_ITEM";
            this.CONDITION7.Location = new System.Drawing.Point(456, 6);
            this.CONDITION7.Name = "CONDITION7";
            this.CONDITION7.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION7.Size = new System.Drawing.Size(56, 16);
            this.CONDITION7.TabIndex = 8;
            this.CONDITION7.Tag = "フリーが対象の場合チェックを付けて下さい";
            this.CONDITION7.Text = "7. ﾌﾘｰ";
            this.CONDITION7.UseVisualStyleBackColor = true;
            this.CONDITION7.Value = "7";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CONDITION7);
            this.panel1.Controls.Add(this.CONDITION6);
            this.panel1.Controls.Add(this.CONDITION5);
            this.panel1.Controls.Add(this.CONDITION4);
            this.panel1.Controls.Add(this.CONDITION3);
            this.panel1.Controls.Add(this.CONDITION2);
            this.panel1.Controls.Add(this.CONDITION1);
            this.panel1.Controls.Add(this.CONDITION_ITEM);
            this.panel1.Controls.Add(this.CONDITION_VALUE);
            this.panel1.Location = new System.Drawing.Point(108, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(812, 25);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.BOIN12);
            this.panel2.Controls.Add(this.BOIN11);
            this.panel2.Controls.Add(this.BOIN10);
            this.panel2.Controls.Add(this.BOIN9);
            this.panel2.Controls.Add(this.BOIN8);
            this.panel2.Controls.Add(this.BOIN7);
            this.panel2.Controls.Add(this.BOIN6);
            this.panel2.Controls.Add(this.BOIN5);
            this.panel2.Controls.Add(this.BOIN4);
            this.panel2.Controls.Add(this.BOIN3);
            this.panel2.Controls.Add(this.BOIN2);
            this.panel2.Controls.Add(this.BOIN1);
            this.panel2.Controls.Add(this.FILTER_BOIN_VALUE);
            this.panel2.Location = new System.Drawing.Point(165, 80);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(720, 25);
            this.panel2.TabIndex = 10;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.SHIIN5);
            this.panel3.Controls.Add(this.SHIIN4);
            this.panel3.Controls.Add(this.SHIIN3);
            this.panel3.Controls.Add(this.SHIIN2);
            this.panel3.Controls.Add(this.SHIIN1);
            this.panel3.Controls.Add(this.FILTER_SHIIN_VALUE);
            this.panel3.Location = new System.Drawing.Point(165, 112);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(312, 32);
            this.panel3.TabIndex = 24;
            // 
            // KensakuKyoutsuuPopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.ClientSize = new System.Drawing.Size(1036, 627);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.customDataGridView1);
            this.Controls.Add(this.customSortHeader1);
            this.Controls.Add(this.bt_func12);
            this.Controls.Add(this.lb_hint);
            this.Controls.Add(this.bt_func11);
            this.Controls.Add(this.bt_func10);
            this.Controls.Add(this.bt_func9);
            this.Controls.Add(this.bt_func8);
            this.Controls.Add(this.bt_func7);
            this.Controls.Add(this.bt_func6);
            this.Controls.Add(this.bt_func5);
            this.Controls.Add(this.bt_func4);
            this.Controls.Add(this.bt_func3);
            this.Controls.Add(this.bt_func2);
            this.Controls.Add(this.bt_func1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.lb_title);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "KensakuKyoutsuuPopupForm";
            this.Text = "KensakuKyoutsuuPopupForm";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KensakuKyoutsuuPopupForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KensakuKyoutsuuPopupForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lb_title;
        internal System.Windows.Forms.Label label16;
        internal r_framework.CustomControl.CustomTextBox CONDITION_VALUE;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label2;
        public r_framework.CustomControl.CustomButton bt_func12;
        public System.Windows.Forms.Label lb_hint;
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
        internal r_framework.CustomControl.CustomDataGridView customDataGridView1;
        private r_framework.CustomControl.DataGridCustomControl.CustomSortHeader customSortHeader1;
        internal r_framework.CustomControl.CustomNumericTextBox2 FILTER_SHIIN_VALUE;
        private r_framework.CustomControl.CustomRadioButton SHIIN1;
        private r_framework.CustomControl.CustomRadioButton SHIIN2;
        private r_framework.CustomControl.CustomRadioButton SHIIN3;
        private r_framework.CustomControl.CustomRadioButton SHIIN4;
        private r_framework.CustomControl.CustomRadioButton SHIIN5;
        internal r_framework.CustomControl.CustomNumericTextBox2 FILTER_BOIN_VALUE;
        private r_framework.CustomControl.CustomRadioButton BOIN1;
        private r_framework.CustomControl.CustomRadioButton BOIN2;
        private r_framework.CustomControl.CustomRadioButton BOIN3;
        private r_framework.CustomControl.CustomRadioButton BOIN4;
        private r_framework.CustomControl.CustomRadioButton BOIN5;
        private r_framework.CustomControl.CustomRadioButton BOIN6;
        private r_framework.CustomControl.CustomRadioButton BOIN7;
        private r_framework.CustomControl.CustomRadioButton BOIN8;
        private r_framework.CustomControl.CustomRadioButton BOIN9;
        private r_framework.CustomControl.CustomRadioButton BOIN10;
        private r_framework.CustomControl.CustomRadioButton BOIN11;
        private r_framework.CustomControl.CustomRadioButton BOIN12;
        internal r_framework.CustomControl.CustomNumericTextBox2 CONDITION_ITEM;
        internal r_framework.CustomControl.CustomRadioButton CONDITION1;
        internal r_framework.CustomControl.CustomRadioButton CONDITION2;
        internal r_framework.CustomControl.CustomRadioButton CONDITION3;
        internal r_framework.CustomControl.CustomRadioButton CONDITION4;
        internal r_framework.CustomControl.CustomRadioButton CONDITION5;
        internal r_framework.CustomControl.CustomRadioButton CONDITION6;
        internal r_framework.CustomControl.CustomRadioButton CONDITION7;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.Panel panel3;
        private r_framework.Components.ImeStatus imeStatus;
    }
}
