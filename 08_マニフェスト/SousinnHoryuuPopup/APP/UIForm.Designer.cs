namespace Shougun.Core.PaperManifest.SousinnHoryuuPopup
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
            this.bt_JWNETsend = new r_framework.CustomControl.CustomButton();
            this.bt_horyuhozonn = new r_framework.CustomControl.CustomButton();
            this.bt_cancer = new r_framework.CustomControl.CustomButton();
            this.customDataGridView1 = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.MANIFEST_ID = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.HST_SHA_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.HST_JOU_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.HAIKI_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
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
            this.lb_title.Size = new System.Drawing.Size(755, 34);
            this.lb_title.TabIndex = 380;
            this.lb_title.Text = "電子マニフェスト（１次）最終処分終了報告";
            this.lb_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_JWNETsend
            // 
            this.bt_JWNETsend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_JWNETsend.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_JWNETsend.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_JWNETsend.Location = new System.Drawing.Point(521, 329);
            this.bt_JWNETsend.Name = "bt_JWNETsend";
            this.bt_JWNETsend.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_JWNETsend.Size = new System.Drawing.Size(80, 35);
            this.bt_JWNETsend.TabIndex = 621;
            this.bt_JWNETsend.TabStop = false;
            this.bt_JWNETsend.Tag = "func9";
            this.bt_JWNETsend.Text = "[F9]\r\nJWNET送信";
            this.bt_JWNETsend.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_JWNETsend.UseVisualStyleBackColor = false;
            this.bt_JWNETsend.Click += new System.EventHandler(this.bt_JWNETsend_Click);
            // 
            // bt_horyuhozonn
            // 
            this.bt_horyuhozonn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_horyuhozonn.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_horyuhozonn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_horyuhozonn.Location = new System.Drawing.Point(602, 329);
            this.bt_horyuhozonn.Name = "bt_horyuhozonn";
            this.bt_horyuhozonn.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_horyuhozonn.Size = new System.Drawing.Size(80, 35);
            this.bt_horyuhozonn.TabIndex = 621;
            this.bt_horyuhozonn.TabStop = false;
            this.bt_horyuhozonn.Tag = "func10";
            this.bt_horyuhozonn.Text = "[F10]\r\n保留保存";
            this.bt_horyuhozonn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_horyuhozonn.UseVisualStyleBackColor = false;
            this.bt_horyuhozonn.Click += new System.EventHandler(this.bt_horyuhozonn_Click);
            // 
            // bt_cancer
            // 
            this.bt_cancer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_cancer.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_cancer.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_cancer.Location = new System.Drawing.Point(683, 329);
            this.bt_cancer.Name = "bt_cancer";
            this.bt_cancer.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_cancer.Size = new System.Drawing.Size(80, 35);
            this.bt_cancer.TabIndex = 621;
            this.bt_cancer.TabStop = false;
            this.bt_cancer.Tag = "func11";
            this.bt_cancer.Text = "[F11]\r\nｷｬﾝｾﾙ";
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
            this.MANIFEST_ID,
            this.HST_SHA_NAME,
            this.HST_JOU_NAME,
            this.HAIKI_NAME});
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
            this.customDataGridView1.Size = new System.Drawing.Size(755, 269);
            this.customDataGridView1.TabIndex = 622;
            // 
            // MANIFEST_ID
            // 
            this.MANIFEST_ID.DataPropertyName = "MANIFEST_ID";
            this.MANIFEST_ID.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.MANIFEST_ID.DefaultCellStyle = dataGridViewCellStyle2;
            this.MANIFEST_ID.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MANIFEST_ID.FocusOutCheckMethod")));
            this.MANIFEST_ID.HeaderText = "マニフェスト番号";
            this.MANIFEST_ID.Name = "MANIFEST_ID";
            this.MANIFEST_ID.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MANIFEST_ID.PopupSearchSendParams")));
            this.MANIFEST_ID.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MANIFEST_ID.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("MANIFEST_ID.popupWindowSetting")));
            this.MANIFEST_ID.ReadOnly = true;
            this.MANIFEST_ID.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MANIFEST_ID.RegistCheckMethod")));
            this.MANIFEST_ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MANIFEST_ID.Width = 130;
            // 
            // HST_SHA_NAME
            // 
            this.HST_SHA_NAME.DataPropertyName = "HST_SHA_NAME";
            this.HST_SHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HST_SHA_NAME.DefaultCellStyle = dataGridViewCellStyle3;
            this.HST_SHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HST_SHA_NAME.FocusOutCheckMethod")));
            this.HST_SHA_NAME.HeaderText = "排出事業者名称";
            this.HST_SHA_NAME.Name = "HST_SHA_NAME";
            this.HST_SHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HST_SHA_NAME.PopupSearchSendParams")));
            this.HST_SHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HST_SHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HST_SHA_NAME.popupWindowSetting")));
            this.HST_SHA_NAME.ReadOnly = true;
            this.HST_SHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HST_SHA_NAME.RegistCheckMethod")));
            this.HST_SHA_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.HST_SHA_NAME.Width = 200;
            // 
            // HST_JOU_NAME
            // 
            this.HST_JOU_NAME.DataPropertyName = "HST_JOU_NAME";
            this.HST_JOU_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HST_JOU_NAME.DefaultCellStyle = dataGridViewCellStyle4;
            this.HST_JOU_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HST_JOU_NAME.FocusOutCheckMethod")));
            this.HST_JOU_NAME.HeaderText = "排出事業場名称";
            this.HST_JOU_NAME.Name = "HST_JOU_NAME";
            this.HST_JOU_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HST_JOU_NAME.PopupSearchSendParams")));
            this.HST_JOU_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HST_JOU_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HST_JOU_NAME.popupWindowSetting")));
            this.HST_JOU_NAME.ReadOnly = true;
            this.HST_JOU_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HST_JOU_NAME.RegistCheckMethod")));
            this.HST_JOU_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.HST_JOU_NAME.Width = 200;
            // 
            // HAIKI_NAME
            // 
            this.HAIKI_NAME.DataPropertyName = "HAIKI_NAME";
            this.HAIKI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HAIKI_NAME.DefaultCellStyle = dataGridViewCellStyle5;
            this.HAIKI_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAIKI_NAME.FocusOutCheckMethod")));
            this.HAIKI_NAME.HeaderText = "廃棄物の名称";
            this.HAIKI_NAME.Name = "HAIKI_NAME";
            this.HAIKI_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HAIKI_NAME.PopupSearchSendParams")));
            this.HAIKI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HAIKI_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HAIKI_NAME.popupWindowSetting")));
            this.HAIKI_NAME.ReadOnly = true;
            this.HAIKI_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAIKI_NAME.RegistCheckMethod")));
            this.HAIKI_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.HAIKI_NAME.Width = 200;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 376);
            this.Controls.Add(this.customDataGridView1);
            this.Controls.Add(this.bt_cancer);
            this.Controls.Add(this.bt_horyuhozonn);
            this.Controls.Add(this.bt_JWNETsend);
            this.Controls.Add(this.lb_title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UIForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "電子マニフェスト（１次）最終処分終了報告";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.UIForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lb_title;
        public r_framework.CustomControl.CustomButton bt_JWNETsend;
        public r_framework.CustomControl.CustomButton bt_horyuhozonn;
        public r_framework.CustomControl.CustomButton bt_cancer;
        private r_framework.CustomControl.DgvCustomTextBoxColumn MANIFEST_ID;
        private r_framework.CustomControl.DgvCustomTextBoxColumn HST_SHA_NAME;
        private r_framework.CustomControl.DgvCustomTextBoxColumn HST_JOU_NAME;
        private r_framework.CustomControl.DgvCustomTextBoxColumn HAIKI_NAME;
        internal r_framework.CustomControl.CustomDataGridView customDataGridView1;
    }
}