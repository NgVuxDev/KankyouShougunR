using r_framework.CustomControl;
namespace Shougun.Core.Common.DenpyouRenkeiIchiran
{
    partial class SearchSettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchSettingForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textSearchSettingInfo = new r_framework.CustomControl.CustomTextBox();
            this.buttonF11 = new r_framework.CustomControl.CustomButton();
            this.buttonF12 = new r_framework.CustomControl.CustomButton();
            this.buttonF1 = new r_framework.CustomControl.CustomButton();
            this.grid = new r_framework.CustomControl.CustomDataGridView(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(9, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "フィルタ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(9, 283);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(596, 20);
            this.label2.TabIndex = 14;
            this.label2.Text = "条件欄は「,」区切りの入力により複数の条件検索可能です。";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(6, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(577, 39);
            this.label3.TabIndex = 15;
            this.label3.Text = "列名が「消費税率」等、一覧が％表記の「税率」の条件は下記を参考に検索願います\r\n例）消費税：5%の場合⇒「0.05」又は「5」\r\n　　　割合：10%の場合⇒「0." +
    "1」又は「10」\r\n";
            // 
            // textSearchSettingInfo
            // 
            this.textSearchSettingInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textSearchSettingInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.textSearchSettingInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textSearchSettingInfo.DefaultBackColor = System.Drawing.Color.Empty;
            this.textSearchSettingInfo.DisplayPopUp = null;
            this.textSearchSettingInfo.FocusOutCheckMethod = null;
            this.textSearchSettingInfo.ForeColor = System.Drawing.Color.Black;
            this.textSearchSettingInfo.IsInputErrorOccured = false;
            this.textSearchSettingInfo.Location = new System.Drawing.Point(104, 48);
            this.textSearchSettingInfo.Name = "textSearchSettingInfo";
            this.textSearchSettingInfo.PopupAfterExecute = null;
            this.textSearchSettingInfo.PopupBeforeExecute = null;
            this.textSearchSettingInfo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("textSearchSettingInfo.PopupSearchSendParams")));
            this.textSearchSettingInfo.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.textSearchSettingInfo.popupWindowSetting = null;
            this.textSearchSettingInfo.prevText = null;
            this.textSearchSettingInfo.ReadOnly = true;
            this.textSearchSettingInfo.RegistCheckMethod = null;
            this.textSearchSettingInfo.Size = new System.Drawing.Size(500, 20);
            this.textSearchSettingInfo.TabIndex = 13;
            this.textSearchSettingInfo.TabStop = false;
            // 
            // buttonF11
            // 
            this.buttonF11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonF11.DefaultBackColor = System.Drawing.Color.Empty;
            this.buttonF11.Location = new System.Drawing.Point(408, 309);
            this.buttonF11.Name = "buttonF11";
            this.buttonF11.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.buttonF11.Size = new System.Drawing.Size(93, 37);
            this.buttonF11.TabIndex = 11;
            this.buttonF11.TabStop = false;
            this.buttonF11.Text = "[F11]\r\nクリア";
            this.buttonF11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonF11.UseVisualStyleBackColor = true;
            this.buttonF11.Click += new System.EventHandler(this.OnFunctionButton);
            // 
            // buttonF12
            // 
            this.buttonF12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonF12.DefaultBackColor = System.Drawing.Color.Empty;
            this.buttonF12.Location = new System.Drawing.Point(512, 309);
            this.buttonF12.Name = "buttonF12";
            this.buttonF12.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.buttonF12.Size = new System.Drawing.Size(93, 37);
            this.buttonF12.TabIndex = 12;
            this.buttonF12.TabStop = false;
            this.buttonF12.Text = "[F12]\r\n閉じる";
            this.buttonF12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonF12.UseVisualStyleBackColor = true;
            this.buttonF12.Click += new System.EventHandler(this.OnFunctionButton);
            // 
            // buttonF1
            // 
            this.buttonF1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonF1.DefaultBackColor = System.Drawing.Color.Empty;
            this.buttonF1.Location = new System.Drawing.Point(9, 309);
            this.buttonF1.Name = "buttonF1";
            this.buttonF1.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.buttonF1.Size = new System.Drawing.Size(93, 37);
            this.buttonF1.TabIndex = 2;
            this.buttonF1.TabStop = false;
            this.buttonF1.Text = "[F1]\r\n実行";
            this.buttonF1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonF1.UseVisualStyleBackColor = true;
            this.buttonF1.Click += new System.EventHandler(this.OnFunctionButton);
            // 
            // grid
            // 
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid.DefaultCellStyle = dataGridViewCellStyle2;
            this.grid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.grid.EnableHeadersVisualStyles = false;
            this.grid.GridColor = System.Drawing.Color.White;
            this.grid.IsReload = false;
            this.grid.LinkedDataPanelName = null;
            this.grid.Location = new System.Drawing.Point(9, 70);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grid.RowHeadersVisible = false;
            this.grid.RowTemplate.Height = 21;
            this.grid.ShowCellToolTips = false;
            this.grid.Size = new System.Drawing.Size(595, 210);
            this.grid.TabIndex = 1;
            this.grid.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnCellValidated);
            this.grid.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.OnCellValidating);
            this.grid.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.grid_EditingControlShowing);
            // 
            // SearchSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 365);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textSearchSettingInfo);
            this.Controls.Add(this.buttonF11);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonF12);
            this.Controls.Add(this.buttonF1);
            this.Controls.Add(this.grid);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(638, 403);
            this.Name = "SearchSettingForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "絞込の設定";
            this.Activated += new System.EventHandler(this.SearchSettingForm_Activated);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomDataGridView grid;
        private CustomButton buttonF1;
        private CustomButton buttonF12;
        private System.Windows.Forms.Label label1;
        private CustomButton buttonF11;
        private CustomTextBox textSearchSettingInfo;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}