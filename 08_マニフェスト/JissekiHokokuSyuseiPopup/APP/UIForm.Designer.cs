namespace Shougun.Core.PaperManifest.JissekiHokokuSyuseiPopup
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lb_title = new System.Windows.Forms.Label();
            this.bt_cancer = new r_framework.CustomControl.CustomButton();
            this.customDataGridView1 = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.SHURUI = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.MANIFEST_ID = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.MANI_SYSTEM_ID = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.MANI_SEQ = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.HAIKI_KBN_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DEN_MANI_KANRI_ID = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lb_title
            // 
            this.lb_title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lb_title.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_title.Font = new System.Drawing.Font("ＭＳ ゴシック", 20F, System.Drawing.FontStyle.Bold);
            this.lb_title.ForeColor = System.Drawing.Color.White;
            this.lb_title.Location = new System.Drawing.Point(8, 17);
            this.lb_title.Name = "lb_title";
            this.lb_title.Size = new System.Drawing.Size(332, 34);
            this.lb_title.TabIndex = 380;
            this.lb_title.Text = "マニフェスト明細";
            this.lb_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_cancer
            // 
            this.bt_cancer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_cancer.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_cancer.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_cancer.Location = new System.Drawing.Point(260, 329);
            this.bt_cancer.Name = "bt_cancer";
            this.bt_cancer.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_cancer.Size = new System.Drawing.Size(80, 35);
            this.bt_cancer.TabIndex = 621;
            this.bt_cancer.TabStop = false;
            this.bt_cancer.Tag = "func11";
            this.bt_cancer.Text = "[F12]\r\n閉じる";
            this.bt_cancer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_cancer.UseVisualStyleBackColor = false;
            this.bt_cancer.Click += new System.EventHandler(this.bt_cancer_Click);
            // 
            // customDataGridView1
            // 
            this.customDataGridView1.AllowUserToAddRows = false;
            this.customDataGridView1.AllowUserToDeleteRows = false;
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
            this.customDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SHURUI,
            this.MANIFEST_ID,
            this.MANI_SYSTEM_ID,
            this.MANI_SEQ,
            this.HAIKI_KBN_CD,
            this.DEN_MANI_KANRI_ID});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridView1.DefaultCellStyle = dataGridViewCellStyle6;
            this.customDataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.customDataGridView1.EnableHeadersVisualStyles = false;
            this.customDataGridView1.GridColor = System.Drawing.Color.White;
            this.customDataGridView1.IsReload = false;
            this.customDataGridView1.LinkedDataPanelName = null;
            this.customDataGridView1.Location = new System.Drawing.Point(8, 54);
            this.customDataGridView1.MultiSelect = false;
            this.customDataGridView1.Name = "customDataGridView1";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.customDataGridView1.RowHeadersVisible = false;
            this.customDataGridView1.RowTemplate.Height = 21;
            this.customDataGridView1.ShowCellToolTips = false;
            this.customDataGridView1.Size = new System.Drawing.Size(332, 269);
            this.customDataGridView1.TabIndex = 622;
            this.customDataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.customDataGridView1_CellDoubleClick);
            // 
            // SHURUI
            // 
            this.SHURUI.DataPropertyName = "SHURUI";
            this.SHURUI.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHURUI.DefaultCellStyle = dataGridViewCellStyle2;
            this.SHURUI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHURUI.FocusOutCheckMethod")));
            this.SHURUI.HeaderText = "種類";
            this.SHURUI.Name = "SHURUI";
            this.SHURUI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHURUI.PopupSearchSendParams")));
            this.SHURUI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHURUI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHURUI.popupWindowSetting")));
            this.SHURUI.ReadOnly = true;
            this.SHURUI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHURUI.RegistCheckMethod")));
            this.SHURUI.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SHURUI.Width = 130;
            // 
            // MANIFEST_ID
            // 
            this.MANIFEST_ID.DataPropertyName = "MANIFEST_ID";
            this.MANIFEST_ID.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.MANIFEST_ID.DefaultCellStyle = dataGridViewCellStyle3;
            this.MANIFEST_ID.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MANIFEST_ID.FocusOutCheckMethod")));
            this.MANIFEST_ID.HeaderText = "交付番号";
            this.MANIFEST_ID.MaxInputLength = 11;
            this.MANIFEST_ID.Name = "MANIFEST_ID";
            this.MANIFEST_ID.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MANIFEST_ID.PopupSearchSendParams")));
            this.MANIFEST_ID.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MANIFEST_ID.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("MANIFEST_ID.popupWindowSetting")));
            this.MANIFEST_ID.ReadOnly = true;
            this.MANIFEST_ID.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MANIFEST_ID.RegistCheckMethod")));
            this.MANIFEST_ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MANIFEST_ID.Width = 200;
            // 
            // MANI_SYSTEM_ID
            // 
            this.MANI_SYSTEM_ID.DataPropertyName = "MANI_SYSTEM_ID";
            this.MANI_SYSTEM_ID.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.MANI_SYSTEM_ID.DefaultCellStyle = dataGridViewCellStyle4;
            this.MANI_SYSTEM_ID.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MANI_SYSTEM_ID.FocusOutCheckMethod")));
            this.MANI_SYSTEM_ID.HeaderText = "";
            this.MANI_SYSTEM_ID.Name = "MANI_SYSTEM_ID";
            this.MANI_SYSTEM_ID.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MANI_SYSTEM_ID.PopupSearchSendParams")));
            this.MANI_SYSTEM_ID.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MANI_SYSTEM_ID.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("MANI_SYSTEM_ID.popupWindowSetting")));
            this.MANI_SYSTEM_ID.ReadOnly = true;
            this.MANI_SYSTEM_ID.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MANI_SYSTEM_ID.RegistCheckMethod")));
            this.MANI_SYSTEM_ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MANI_SYSTEM_ID.Visible = false;
            this.MANI_SYSTEM_ID.Width = 200;
            // 
            // MANI_SEQ
            // 
            this.MANI_SEQ.DataPropertyName = "MANI_SEQ";
            this.MANI_SEQ.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.MANI_SEQ.DefaultCellStyle = dataGridViewCellStyle5;
            this.MANI_SEQ.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MANI_SEQ.FocusOutCheckMethod")));
            this.MANI_SEQ.HeaderText = "";
            this.MANI_SEQ.Name = "MANI_SEQ";
            this.MANI_SEQ.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MANI_SEQ.PopupSearchSendParams")));
            this.MANI_SEQ.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MANI_SEQ.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("MANI_SEQ.popupWindowSetting")));
            this.MANI_SEQ.ReadOnly = true;
            this.MANI_SEQ.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MANI_SEQ.RegistCheckMethod")));
            this.MANI_SEQ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MANI_SEQ.Visible = false;
            this.MANI_SEQ.Width = 200;
            // 
            // HAIKI_KBN_CD
            // 
            this.HAIKI_KBN_CD.DataPropertyName = "HAIKI_KBN_CD";
            this.HAIKI_KBN_CD.HeaderText = "";
            this.HAIKI_KBN_CD.Name = "HAIKI_KBN_CD";
            this.HAIKI_KBN_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.HAIKI_KBN_CD.Visible = false;
            // 
            // DEN_MANI_KANRI_ID
            // 
            this.DEN_MANI_KANRI_ID.DataPropertyName = "DEN_MANI_KANRI_ID";
            this.DEN_MANI_KANRI_ID.DefaultBackColor = System.Drawing.Color.Empty;
            this.DEN_MANI_KANRI_ID.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DEN_MANI_KANRI_ID.FocusOutCheckMethod")));
            this.DEN_MANI_KANRI_ID.HeaderText = "";
            this.DEN_MANI_KANRI_ID.Name = "DEN_MANI_KANRI_ID";
            this.DEN_MANI_KANRI_ID.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DEN_MANI_KANRI_ID.PopupSearchSendParams")));
            this.DEN_MANI_KANRI_ID.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DEN_MANI_KANRI_ID.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DEN_MANI_KANRI_ID.popupWindowSetting")));
            this.DEN_MANI_KANRI_ID.ReadOnly = true;
            this.DEN_MANI_KANRI_ID.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DEN_MANI_KANRI_ID.RegistCheckMethod")));
            this.DEN_MANI_KANRI_ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DEN_MANI_KANRI_ID.Visible = false;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 376);
            this.Controls.Add(this.customDataGridView1);
            this.Controls.Add(this.bt_cancer);
            this.Controls.Add(this.lb_title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UIForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "実績報告書";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.UIForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lb_title;
        public r_framework.CustomControl.CustomButton bt_cancer;
        internal r_framework.CustomControl.CustomDataGridView customDataGridView1;
        private r_framework.CustomControl.DgvCustomTextBoxColumn SHURUI;
        private r_framework.CustomControl.DgvCustomTextBoxColumn MANIFEST_ID;
        private r_framework.CustomControl.DgvCustomTextBoxColumn MANI_SYSTEM_ID;
        private r_framework.CustomControl.DgvCustomTextBoxColumn MANI_SEQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn HAIKI_KBN_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn DEN_MANI_KANRI_ID;
    }
}