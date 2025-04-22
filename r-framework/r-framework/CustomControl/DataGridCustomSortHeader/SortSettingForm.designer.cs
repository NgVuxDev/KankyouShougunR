namespace r_framework.CustomControl.DataGridCustomControl
{
    partial class SortSettingForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SortSettingForm));
            this.lb_hint = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonF12 = new System.Windows.Forms.Button();
            this.buttonF1 = new System.Windows.Forms.Button();
            this.buttonF11 = new System.Windows.Forms.Button();
            this.grid = new System.Windows.Forms.DataGridView();
            this.buttonF2 = new System.Windows.Forms.Button();
            this.buttonF3 = new System.Windows.Forms.Button();
            this.textSortSettingInfo = new r_framework.CustomControl.CustomTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // lb_hint
            // 
            this.lb_hint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_hint.BackColor = System.Drawing.Color.Black;
            this.lb_hint.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lb_hint.ForeColor = System.Drawing.Color.Yellow;
            this.lb_hint.Location = new System.Drawing.Point(9, 283);
            this.lb_hint.Name = "lb_hint";
            this.lb_hint.Size = new System.Drawing.Size(496, 20);
            this.lb_hint.TabIndex = 1;
            this.lb_hint.Text = "[F2]:並替項目に選択します(３つまで)。[F3]:昇順降順を切り替えます。";
            this.lb_hint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(9, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "並び順";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonF12
            // 
            this.buttonF12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonF12.Location = new System.Drawing.Point(412, 309);
            this.buttonF12.Name = "buttonF12";
            this.buttonF12.Size = new System.Drawing.Size(93, 37);
            this.buttonF12.TabIndex = 6;
            this.buttonF12.TabStop = false;
            this.buttonF12.Text = "[F12]\r\n閉じる";
            this.buttonF12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonF12.UseVisualStyleBackColor = true;
            this.buttonF12.Click += new System.EventHandler(this.OnFunctionButton);
            // 
            // buttonF1
            // 
            this.buttonF1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonF1.Location = new System.Drawing.Point(9, 309);
            this.buttonF1.Name = "buttonF1";
            this.buttonF1.Size = new System.Drawing.Size(93, 37);
            this.buttonF1.TabIndex = 2;
            this.buttonF1.TabStop = false;
            this.buttonF1.Text = "[F1]\r\n実行";
            this.buttonF1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonF1.UseVisualStyleBackColor = true;
            this.buttonF1.Click += new System.EventHandler(this.OnFunctionButton);
            // 
            // buttonF11
            // 
            this.buttonF11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonF11.Location = new System.Drawing.Point(308, 309);
            this.buttonF11.Name = "buttonF11";
            this.buttonF11.Size = new System.Drawing.Size(93, 37);
            this.buttonF11.TabIndex = 5;
            this.buttonF11.TabStop = false;
            this.buttonF11.Text = "[F11]\r\nクリア";
            this.buttonF11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonF11.UseVisualStyleBackColor = true;
            this.buttonF11.Click += new System.EventHandler(this.OnFunctionButton);
            // 
            // grid
            // 
            this.grid.AllowUserToResizeColumns = false;
            this.grid.AllowUserToResizeRows = false;
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grid.ColumnHeadersHeight = 22;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grid.EnableHeadersVisualStyles = false;
            this.grid.Location = new System.Drawing.Point(9, 70);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersVisible = false;
            this.grid.RowTemplate.Height = 21;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(495, 210);
            this.grid.TabIndex = 0;
            this.grid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnCellDoubleClick);
            this.grid.SelectionChanged += new System.EventHandler(this.grid_SelectionChanged);
            this.grid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.grid_KeyDown);
            this.grid.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
            // 
            // buttonF2
            // 
            this.buttonF2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonF2.Location = new System.Drawing.Point(108, 309);
            this.buttonF2.Name = "buttonF2";
            this.buttonF2.Size = new System.Drawing.Size(93, 37);
            this.buttonF2.TabIndex = 3;
            this.buttonF2.TabStop = false;
            this.buttonF2.Text = "[F2]\r\n選択";
            this.buttonF2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonF2.UseVisualStyleBackColor = true;
            this.buttonF2.Click += new System.EventHandler(this.OnFunctionButton);
            // 
            // buttonF3
            // 
            this.buttonF3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonF3.Location = new System.Drawing.Point(207, 309);
            this.buttonF3.Name = "buttonF3";
            this.buttonF3.Size = new System.Drawing.Size(93, 37);
            this.buttonF3.TabIndex = 4;
            this.buttonF3.TabStop = false;
            this.buttonF3.Text = "[F3]\r\n順序";
            this.buttonF3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonF3.UseVisualStyleBackColor = true;
            this.buttonF3.Click += new System.EventHandler(this.OnFunctionButton);
            // 
            // textSortSettingInfo
            // 
            this.textSortSettingInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textSortSettingInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.textSortSettingInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textSortSettingInfo.DefaultBackColor = System.Drawing.Color.Empty;
            this.textSortSettingInfo.DisplayPopUp = null;
            this.textSortSettingInfo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("textSortSettingInfo.FocusOutCheckMethod")));
            this.textSortSettingInfo.Location = new System.Drawing.Point(104, 48);
            this.textSortSettingInfo.Name = "textSortSettingInfo";
            this.textSortSettingInfo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("textSortSettingInfo.PopupSearchSendParams")));
            this.textSortSettingInfo.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.textSortSettingInfo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("textSortSettingInfo.popupWindowSetting")));
            this.textSortSettingInfo.ReadOnly = true;
            this.textSortSettingInfo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("textSortSettingInfo.RegistCheckMethod")));
            this.textSortSettingInfo.Size = new System.Drawing.Size(400, 20);
            this.textSortSettingInfo.TabIndex = 7;
            // 
            // SortSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 365);
            this.Controls.Add(this.textSortSettingInfo);
            this.Controls.Add(this.buttonF3);
            this.Controls.Add(this.buttonF2);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.buttonF11);
            this.Controls.Add(this.buttonF1);
            this.Controls.Add(this.buttonF12);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lb_hint);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(538, 403);
            this.Name = "SortSettingForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "並び順の設定";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lb_hint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonF12;
        private System.Windows.Forms.Button buttonF1;
        private System.Windows.Forms.Button buttonF11;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Button buttonF2;
        private System.Windows.Forms.Button buttonF3;
        private CustomTextBox textSortSettingInfo;
    }
}