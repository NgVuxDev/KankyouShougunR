namespace r_framework.APP.Base
{
    partial class IchiranSuperForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IchiranSuperForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.searchString = new r_framework.CustomControl.CustomTextBox();
            this.bt_ptn1 = new r_framework.CustomControl.CustomButton();
            this.bt_ptn2 = new r_framework.CustomControl.CustomButton();
            this.bt_ptn3 = new r_framework.CustomControl.CustomButton();
            this.bt_ptn4 = new r_framework.CustomControl.CustomButton();
            this.bt_ptn5 = new r_framework.CustomControl.CustomButton();
            this.customDataGridView1 = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.customSortHeader1 = new r_framework.CustomControl.DataGridCustomControl.CustomSortHeader();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.SystemColors.Window;
            this.searchString.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.FocusOutCheckMethod")));
            this.searchString.Location = new System.Drawing.Point(0, 2);
            this.searchString.Multiline = true;
            this.searchString.Name = "searchString";
            this.searchString.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("searchString.PopupSearchSendParams")));
            this.searchString.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.searchString.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("searchString.popupWindowSetting")));
            this.searchString.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.RegistCheckMethod")));
            this.searchString.Size = new System.Drawing.Size(997, 135);
            this.searchString.TabIndex = 0;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Location = new System.Drawing.Point(3, 427);
            this.bt_ptn1.Name = "bt_ptn1";
            this.bt_ptn1.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_ptn1.Size = new System.Drawing.Size(158, 24);
            this.bt_ptn1.TabIndex = 2;
            this.bt_ptn1.UseVisualStyleBackColor = true;
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Location = new System.Drawing.Point(167, 427);
            this.bt_ptn2.Name = "bt_ptn2";
            this.bt_ptn2.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_ptn2.Size = new System.Drawing.Size(158, 24);
            this.bt_ptn2.TabIndex = 2;
            this.bt_ptn2.UseVisualStyleBackColor = true;
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Location = new System.Drawing.Point(331, 427);
            this.bt_ptn3.Name = "bt_ptn3";
            this.bt_ptn3.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_ptn3.Size = new System.Drawing.Size(158, 24);
            this.bt_ptn3.TabIndex = 2;
            this.bt_ptn3.UseVisualStyleBackColor = true;
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Location = new System.Drawing.Point(495, 427);
            this.bt_ptn4.Name = "bt_ptn4";
            this.bt_ptn4.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_ptn4.Size = new System.Drawing.Size(158, 24);
            this.bt_ptn4.TabIndex = 2;
            this.bt_ptn4.UseVisualStyleBackColor = true;
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Location = new System.Drawing.Point(659, 427);
            this.bt_ptn5.Name = "bt_ptn5";
            this.bt_ptn5.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_ptn5.Size = new System.Drawing.Size(158, 24);
            this.bt_ptn5.TabIndex = 2;
            this.bt_ptn5.UseVisualStyleBackColor = true;
            // 
            // customDataGridView1
            // 
            this.customDataGridView1.AllowUserToAddRows = false;
            this.customDataGridView1.AllowUserToDeleteRows = false;
            this.customDataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.customDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridView1.DefaultCellStyle = dataGridViewCellStyle5;
            this.customDataGridView1.EnableHeadersVisualStyles = false;
            this.customDataGridView1.GridColor = System.Drawing.Color.White;
            this.customDataGridView1.IsReload = false;
            this.customDataGridView1.LinkedDataPanelName = "customSortHeader1";
            this.customDataGridView1.Location = new System.Drawing.Point(3, 183);
            this.customDataGridView1.Name = "customDataGridView1";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.customDataGridView1.RowHeadersVisible = false;
            this.customDataGridView1.RowTemplate.Height = 21;
            this.customDataGridView1.Size = new System.Drawing.Size(997, 238);
            this.customDataGridView1.TabIndex = 3;
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.customSortHeader1.LinkedDataGridViewName = "customDataGridView1";
            this.customSortHeader1.Location = new System.Drawing.Point(4, 158);
            this.customSortHeader1.Name = "customSortHeader1";
            this.customSortHeader1.Size = new System.Drawing.Size(997, 26);
            this.customSortHeader1.SortFlag = false;
            this.customSortHeader1.TabIndex = 4;
            this.customSortHeader1.TabStop = false;
            // 
            // IchiranSuperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1014, 458);
            this.Controls.Add(this.customDataGridView1);
            this.Controls.Add(this.customSortHeader1);
            this.Controls.Add(this.bt_ptn5);
            this.Controls.Add(this.bt_ptn4);
            this.Controls.Add(this.bt_ptn3);
            this.Controls.Add(this.bt_ptn2);
            this.Controls.Add(this.bt_ptn1);
            this.Controls.Add(this.searchString);
            this.Name = "IchiranSuperForm";
            this.Text = "IchiranSuperForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IchiranSuperForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public CustomControl.CustomTextBox searchString;
        public CustomControl.CustomButton bt_ptn1;
        public CustomControl.CustomButton bt_ptn2;
        public CustomControl.CustomButton bt_ptn3;
        public CustomControl.CustomButton bt_ptn4;
        public CustomControl.CustomButton bt_ptn5;
        public CustomControl.CustomDataGridView customDataGridView1;
        public CustomControl.DataGridCustomControl.CustomSortHeader customSortHeader1;

    }
}