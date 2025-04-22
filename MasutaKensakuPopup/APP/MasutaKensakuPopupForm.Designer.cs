namespace MasutaKensakuPopup.APP
{
    partial class MasutaKensakuPopupForm
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
            this.lb_title = new System.Windows.Forms.Label();
            this.bt_func12 = new r_framework.CustomControl.CustomButton();
            this.MasterItem = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.Text = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImeMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DBFieldsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemDefinedTypes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.MasterItem)).BeginInit();
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
            this.lb_title.Text = "検索条件";
            this.lb_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_func12
            // 
            this.bt_func12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func12.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_func12.Location = new System.Drawing.Point(244, 375);
            this.bt_func12.Name = "bt_func12";
            this.bt_func12.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func12.Size = new System.Drawing.Size(80, 35);
            this.bt_func12.TabIndex = 391;
            this.bt_func12.TabStop = false;
            this.bt_func12.Tag = "func12";
            this.bt_func12.Text = "[F12]\r\n閉じる";
            this.bt_func12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func12.UseVisualStyleBackColor = false;
            this.bt_func12.Click += new System.EventHandler(this.FormClose);
            // 
            // MasterItem
            // 
            this.MasterItem.AllowUserToAddRows = false;
            this.MasterItem.AllowUserToDeleteRows = false;
            this.MasterItem.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MasterItem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.MasterItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MasterItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Text,
            this.ImeMode,
            this.DBFieldsName,
            this.ItemDefinedTypes});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.MasterItem.DefaultCellStyle = dataGridViewCellStyle2;
            this.MasterItem.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.MasterItem.EnableHeadersVisualStyles = false;
            this.MasterItem.GridColor = System.Drawing.Color.White;
            this.MasterItem.IsReload = false;
            this.MasterItem.LinkedDataPanelName = null;
            this.MasterItem.Location = new System.Drawing.Point(12, 43);
            this.MasterItem.MultiSelect = false;
            this.MasterItem.Name = "MasterItem";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MasterItem.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.MasterItem.RowHeadersVisible = false;
            this.MasterItem.RowTemplate.Height = 21;
            this.MasterItem.ShowCellToolTips = false;
            this.MasterItem.Size = new System.Drawing.Size(291, 299);
            this.MasterItem.TabIndex = 392;
            this.MasterItem.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MasterItem_CellDoubleClick);
            this.MasterItem.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ItemKeyUp);
            // 
            // Text
            // 
            this.Text.DataPropertyName = "Item";
            this.Text.HeaderText = "項目名";
            this.Text.Name = "Text";
            this.Text.ReadOnly = true;
            this.Text.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Text.Width = 220;
            // 
            // ImeMode
            // 
            this.ImeMode.DataPropertyName = "ImeMode";
            this.ImeMode.HeaderText = "ImeMode";
            this.ImeMode.Name = "ImeMode";
            this.ImeMode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ImeMode.Visible = false;
            // 
            // DBFieldsName
            // 
            this.DBFieldsName.DataPropertyName = "DBFieldsName";
            this.DBFieldsName.HeaderText = "DBFieldsName";
            this.DBFieldsName.Name = "DBFieldsName";
            this.DBFieldsName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DBFieldsName.Visible = false;
            // 
            // ItemDefinedTypes
            // 
            this.ItemDefinedTypes.DataPropertyName = "ItemDefinedTypes";
            this.ItemDefinedTypes.HeaderText = "ItemDefinedTypes";
            this.ItemDefinedTypes.Name = "ItemDefinedTypes";
            this.ItemDefinedTypes.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ItemDefinedTypes.Visible = false;
            // 
            // MasutaKensakuPopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(336, 422);
            this.Controls.Add(this.MasterItem);
            this.Controls.Add(this.bt_func12);
            this.Controls.Add(this.lb_title);
            this.Name = "MasutaKensakuPopupForm";
            ((System.ComponentModel.ISupportInitialize)(this.MasterItem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lb_title;
        public r_framework.CustomControl.CustomButton bt_func12;
        public r_framework.CustomControl.CustomDataGridView MasterItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn Text;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImeMode;
        private System.Windows.Forms.DataGridViewTextBoxColumn DBFieldsName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemDefinedTypes;
    }
}