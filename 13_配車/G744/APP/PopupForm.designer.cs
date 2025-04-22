using System.Windows.Forms;
namespace Shougun.Core.Allocation.CarTransferTeiki
{
    partial class PopupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopupForm));
            this.lb_title = new System.Windows.Forms.Label();
            this.bt_func12 = new r_framework.CustomControl.CustomButton();
            this.Ichiran = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.bt_func9 = new r_framework.CustomControl.CustomButton();
            this.SAGYOU_DATE = new r_framework.CustomControl.CustomDateTimePicker();
            this.SAGYOU_DATE_LABEL = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).BeginInit();
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
            this.lb_title.TabIndex = 379;
            this.lb_title.Text = "定期配車検索";
            this.lb_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_title.TextChanged += new System.EventHandler(this.lb_title_TextChanged);
            // 
            // bt_func12
            // 
            this.bt_func12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_func12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func12.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_func12.Location = new System.Drawing.Point(332, 405);
            this.bt_func12.Name = "bt_func12";
            this.bt_func12.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func12.Size = new System.Drawing.Size(80, 35);
            this.bt_func12.TabIndex = 7;
            this.bt_func12.TabStop = false;
            this.bt_func12.Tag = "func12";
            this.bt_func12.Text = "[F12]\r\n閉じる";
            this.bt_func12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func12.UseVisualStyleBackColor = false;
            // 
            // Ichiran
            // 
            this.Ichiran.AllowUserToAddRows = false;
            this.Ichiran.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Ichiran.DefaultCellStyle = dataGridViewCellStyle2;
            this.Ichiran.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.Ichiran.EnableHeadersVisualStyles = false;
            this.Ichiran.GridColor = System.Drawing.Color.White;
            this.Ichiran.IsReload = false;
            this.Ichiran.LinkedDataPanelName = null;
            this.Ichiran.Location = new System.Drawing.Point(12, 69);
            this.Ichiran.MultiSelect = false;
            this.Ichiran.Name = "Ichiran";
            this.Ichiran.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Ichiran.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Ichiran.RowHeadersVisible = false;
            this.Ichiran.RowTemplate.Height = 21;
            this.Ichiran.ShowCellToolTips = false;
            this.Ichiran.Size = new System.Drawing.Size(400, 330);
            this.Ichiran.TabIndex = 1;
            this.Ichiran.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DetailCellDoubleClick);
            this.Ichiran.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DetailKeyDown);
            // 
            // bt_func9
            // 
            this.bt_func9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_func9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func9.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_func9.Location = new System.Drawing.Point(238, 405);
            this.bt_func9.Name = "bt_func9";
            this.bt_func9.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func9.Size = new System.Drawing.Size(80, 35);
            this.bt_func9.TabIndex = 6;
            this.bt_func9.TabStop = false;
            this.bt_func9.Tag = "";
            this.bt_func9.Text = "[F9]　　選択";
            this.bt_func9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func9.UseVisualStyleBackColor = false;
            this.bt_func9.Click += new System.EventHandler(this.Selected);
            // 
            // SAGYOU_DATE
            // 
            this.SAGYOU_DATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.SAGYOU_DATE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SAGYOU_DATE.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.SAGYOU_DATE.Checked = false;
            this.SAGYOU_DATE.DateTimeNowYear = "";
            this.SAGYOU_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            this.SAGYOU_DATE.DisplayPopUp = null;
            this.SAGYOU_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SAGYOU_DATE.FocusOutCheckMethod")));
            this.SAGYOU_DATE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SAGYOU_DATE.ForeColor = System.Drawing.Color.Black;
            this.SAGYOU_DATE.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.SAGYOU_DATE.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.SAGYOU_DATE.IsInputErrorOccured = false;
            this.SAGYOU_DATE.Location = new System.Drawing.Point(134, 43);
            this.SAGYOU_DATE.MaxLength = 10;
            this.SAGYOU_DATE.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.SAGYOU_DATE.Name = "SAGYOU_DATE";
            this.SAGYOU_DATE.NullValue = "";
            this.SAGYOU_DATE.PopupAfterExecute = null;
            this.SAGYOU_DATE.PopupBeforeExecute = null;
            this.SAGYOU_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SAGYOU_DATE.PopupSearchSendParams")));
            this.SAGYOU_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SAGYOU_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SAGYOU_DATE.popupWindowSetting")));
            this.SAGYOU_DATE.ReadOnly = true;
            this.SAGYOU_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SAGYOU_DATE.RegistCheckMethod")));
            this.SAGYOU_DATE.Size = new System.Drawing.Size(278, 20);
            this.SAGYOU_DATE.TabIndex = 10;
            this.SAGYOU_DATE.Tag = "";
            this.SAGYOU_DATE.Value = null;
            // 
            // SAGYOU_DATE_LABEL
            // 
            this.SAGYOU_DATE_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.SAGYOU_DATE_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SAGYOU_DATE_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SAGYOU_DATE_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SAGYOU_DATE_LABEL.ForeColor = System.Drawing.Color.White;
            this.SAGYOU_DATE_LABEL.Location = new System.Drawing.Point(12, 43);
            this.SAGYOU_DATE_LABEL.Name = "SAGYOU_DATE_LABEL";
            this.SAGYOU_DATE_LABEL.Size = new System.Drawing.Size(120, 20);
            this.SAGYOU_DATE_LABEL.TabIndex = 752;
            this.SAGYOU_DATE_LABEL.Text = "作業日";
            this.SAGYOU_DATE_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(424, 451);
            this.Controls.Add(this.SAGYOU_DATE);
            this.Controls.Add(this.SAGYOU_DATE_LABEL);
            this.Controls.Add(this.Ichiran);
            this.Controls.Add(this.bt_func9);
            this.Controls.Add(this.bt_func12);
            this.Controls.Add(this.lb_title);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PopupForm";
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Label lb_title;
        public r_framework.CustomControl.CustomButton bt_func12;
        public r_framework.CustomControl.CustomDataGridView Ichiran;
        public r_framework.CustomControl.CustomButton bt_func9;
        internal r_framework.CustomControl.CustomDateTimePicker SAGYOU_DATE;
        internal Label SAGYOU_DATE_LABEL;
    }
}