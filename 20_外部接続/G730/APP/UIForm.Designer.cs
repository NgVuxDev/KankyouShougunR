namespace Shougun.Core.ExternalConnection.FileUpload
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cRdbtn_HyoujiKbn_1 = new r_framework.CustomControl.CustomRadioButton();
            this.cRdbtn_HyoujiKbn_2 = new r_framework.CustomControl.CustomRadioButton();
            this.pnl_HyoujiKbn = new r_framework.CustomControl.CustomPanel();
            this.lbl_HyoujiKbn = new System.Windows.Forms.Label();
            this.lbl_FileName = new System.Windows.Forms.Label();
            this.cntxt_HyoujiKbn = new r_framework.CustomControl.CustomNumericTextBox2();
            this.txt_FileName = new r_framework.CustomControl.CustomTextBox();
            this.upJisshiKbn = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.filePath = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.hidden_fileId = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.pnl_HyoujiKbn.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.Location = new System.Drawing.Point(946, 64);
            this.searchString.Size = new System.Drawing.Size(44, 45);
            this.searchString.Visible = false;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Enabled = false;
            this.bt_ptn1.Location = new System.Drawing.Point(3, 393);
            this.bt_ptn1.Visible = false;
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Enabled = false;
            this.bt_ptn2.Location = new System.Drawing.Point(204, 393);
            this.bt_ptn2.Visible = false;
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Enabled = false;
            this.bt_ptn3.Location = new System.Drawing.Point(405, 393);
            this.bt_ptn3.Visible = false;
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Enabled = false;
            this.bt_ptn4.Location = new System.Drawing.Point(606, 393);
            this.bt_ptn4.Visible = false;
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Enabled = false;
            this.bt_ptn5.Location = new System.Drawing.Point(807, 393);
            this.bt_ptn5.Visible = false;
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.Enabled = false;
            this.customSortHeader1.Location = new System.Drawing.Point(2, 96);
            this.customSortHeader1.Size = new System.Drawing.Size(870, 26);
            this.customSortHeader1.Visible = false;
            // 
            // customSearchHeader1
            // 
            this.customSearchHeader1.Enabled = false;
            this.customSearchHeader1.Location = new System.Drawing.Point(2, 64);
            this.customSearchHeader1.Size = new System.Drawing.Size(640, 26);
            // 
            // cRdbtn_HyoujiKbn_1
            // 
            this.cRdbtn_HyoujiKbn_1.AutoSize = true;
            this.cRdbtn_HyoujiKbn_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.cRdbtn_HyoujiKbn_1.FocusOutCheckMethod = null;
            this.cRdbtn_HyoujiKbn_1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cRdbtn_HyoujiKbn_1.LinkedTextBox = "cntxt_HyoujiKbn";
            this.cRdbtn_HyoujiKbn_1.Location = new System.Drawing.Point(3, 0);
            this.cRdbtn_HyoujiKbn_1.Name = "cRdbtn_HyoujiKbn_1";
            this.cRdbtn_HyoujiKbn_1.PopupAfterExecute = null;
            this.cRdbtn_HyoujiKbn_1.PopupBeforeExecute = null;
            this.cRdbtn_HyoujiKbn_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cRdbtn_HyoujiKbn_1.PopupSearchSendParams")));
            this.cRdbtn_HyoujiKbn_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cRdbtn_HyoujiKbn_1.popupWindowSetting = null;
            this.cRdbtn_HyoujiKbn_1.RegistCheckMethod = null;
            this.cRdbtn_HyoujiKbn_1.Size = new System.Drawing.Size(165, 17);
            this.cRdbtn_HyoujiKbn_1.TabIndex = 3;
            this.cRdbtn_HyoujiKbn_1.Text = "1.アップ済みファイル";
            this.cRdbtn_HyoujiKbn_1.UseVisualStyleBackColor = true;
            this.cRdbtn_HyoujiKbn_1.Value = "1";
            // 
            // cRdbtn_HyoujiKbn_2
            // 
            this.cRdbtn_HyoujiKbn_2.AutoSize = true;
            this.cRdbtn_HyoujiKbn_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.cRdbtn_HyoujiKbn_2.FocusOutCheckMethod = null;
            this.cRdbtn_HyoujiKbn_2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cRdbtn_HyoujiKbn_2.LinkedTextBox = "cntxt_HyoujiKbn";
            this.cRdbtn_HyoujiKbn_2.Location = new System.Drawing.Point(172, 0);
            this.cRdbtn_HyoujiKbn_2.Name = "cRdbtn_HyoujiKbn_2";
            this.cRdbtn_HyoujiKbn_2.PopupAfterExecute = null;
            this.cRdbtn_HyoujiKbn_2.PopupBeforeExecute = null;
            this.cRdbtn_HyoujiKbn_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cRdbtn_HyoujiKbn_2.PopupSearchSendParams")));
            this.cRdbtn_HyoujiKbn_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cRdbtn_HyoujiKbn_2.popupWindowSetting = null;
            this.cRdbtn_HyoujiKbn_2.RegistCheckMethod = null;
            this.cRdbtn_HyoujiKbn_2.Size = new System.Drawing.Size(151, 17);
            this.cRdbtn_HyoujiKbn_2.TabIndex = 4;
            this.cRdbtn_HyoujiKbn_2.Text = "2.ローカルファイル";
            this.cRdbtn_HyoujiKbn_2.UseVisualStyleBackColor = true;
            this.cRdbtn_HyoujiKbn_2.Value = "2";
            // 
            // pnl_HyoujiKbn
            // 
            this.pnl_HyoujiKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_HyoujiKbn.Controls.Add(this.cRdbtn_HyoujiKbn_2);
            this.pnl_HyoujiKbn.Controls.Add(this.cRdbtn_HyoujiKbn_1);
            this.pnl_HyoujiKbn.Location = new System.Drawing.Point(134, 38);
            this.pnl_HyoujiKbn.Name = "pnl_HyoujiKbn";
            this.pnl_HyoujiKbn.Size = new System.Drawing.Size(347, 20);
            this.pnl_HyoujiKbn.TabIndex = 448;
            // 
            // lbl_HyoujiKbn
            // 
            this.lbl_HyoujiKbn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_HyoujiKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_HyoujiKbn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_HyoujiKbn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_HyoujiKbn.ForeColor = System.Drawing.Color.White;
            this.lbl_HyoujiKbn.Location = new System.Drawing.Point(2, 38);
            this.lbl_HyoujiKbn.Name = "lbl_HyoujiKbn";
            this.lbl_HyoujiKbn.Size = new System.Drawing.Size(110, 20);
            this.lbl_HyoujiKbn.TabIndex = 443;
            this.lbl_HyoujiKbn.Text = "表示区分";
            this.lbl_HyoujiKbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_FileName
            // 
            this.lbl_FileName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_FileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_FileName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_FileName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_FileName.ForeColor = System.Drawing.Color.White;
            this.lbl_FileName.Location = new System.Drawing.Point(3, 12);
            this.lbl_FileName.Name = "lbl_FileName";
            this.lbl_FileName.Size = new System.Drawing.Size(110, 20);
            this.lbl_FileName.TabIndex = 457;
            this.lbl_FileName.Text = "ファイル名";
            this.lbl_FileName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cntxt_HyoujiKbn
            // 
            this.cntxt_HyoujiKbn.BackColor = System.Drawing.SystemColors.Window;
            this.cntxt_HyoujiKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cntxt_HyoujiKbn.DefaultBackColor = System.Drawing.Color.Empty;
            this.cntxt_HyoujiKbn.DisplayPopUp = null;
            this.cntxt_HyoujiKbn.FocusOutCheckMethod = null;
            this.cntxt_HyoujiKbn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cntxt_HyoujiKbn.ForeColor = System.Drawing.Color.Black;
            this.cntxt_HyoujiKbn.IsInputErrorOccured = false;
            this.cntxt_HyoujiKbn.LinkedRadioButtonArray = new string[] {
        "cRdbtn_HyoujiKbn_1",
        "cRdbtn_HyoujiKbn_2"};
            this.cntxt_HyoujiKbn.Location = new System.Drawing.Point(115, 38);
            this.cntxt_HyoujiKbn.Name = "cntxt_HyoujiKbn";
            this.cntxt_HyoujiKbn.PopupAfterExecute = null;
            this.cntxt_HyoujiKbn.PopupBeforeExecute = null;
            this.cntxt_HyoujiKbn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cntxt_HyoujiKbn.PopupSearchSendParams")));
            this.cntxt_HyoujiKbn.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cntxt_HyoujiKbn.popupWindowSetting = null;
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
            this.cntxt_HyoujiKbn.RangeSetting = rangeSettingDto1;
            this.cntxt_HyoujiKbn.RegistCheckMethod = null;
            this.cntxt_HyoujiKbn.Size = new System.Drawing.Size(20, 20);
            this.cntxt_HyoujiKbn.TabIndex = 2;
            this.cntxt_HyoujiKbn.Tag = "【1～2】のいずれかで入力してください";
            this.cntxt_HyoujiKbn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cntxt_HyoujiKbn.WordWrap = false;
            // 
            // txt_FileName
            // 
            this.txt_FileName.BackColor = System.Drawing.SystemColors.Window;
            this.txt_FileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_FileName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txt_FileName.DBFieldsName = "FILE_NAME";
            this.txt_FileName.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_FileName.DisplayItemName = "ファイル名";
            this.txt_FileName.DisplayPopUp = null;
            this.txt_FileName.FocusOutCheckMethod = null;
            this.txt_FileName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_FileName.ForeColor = System.Drawing.Color.Black;
            this.txt_FileName.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.txt_FileName.IsInputErrorOccured = false;
            this.txt_FileName.ItemDefinedTypes = "varchar";
            this.txt_FileName.Location = new System.Drawing.Point(116, 12);
            this.txt_FileName.MaxLength = 40;
            this.txt_FileName.Name = "txt_FileName";
            this.txt_FileName.PopupAfterExecute = null;
            this.txt_FileName.PopupBeforeExecute = null;
            this.txt_FileName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_FileName.PopupSearchSendParams")));
            this.txt_FileName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_FileName.popupWindowSetting = null;
            this.txt_FileName.RegistCheckMethod = null;
            this.txt_FileName.ShortItemName = "ファイル名";
            this.txt_FileName.Size = new System.Drawing.Size(525, 20);
            this.txt_FileName.TabIndex = 1;
            this.txt_FileName.Tag = "ファイル名を入力してください。";

            //
            // customDataGridView1
            //
            this.customDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.upJisshiKbn,
            this.filePath,
            this.hidden_fileId});

            // 
            // upJisshiKbn
            // 
            this.upJisshiKbn.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.upJisshiKbn.DefaultCellStyle = dataGridViewCellStyle1;
            this.upJisshiKbn.FocusOutCheckMethod = null;
            this.upJisshiKbn.HeaderText = "";
            this.upJisshiKbn.Name = "upJisshiKbn";
            this.upJisshiKbn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("upJisshiKbn.PopupSearchSendParams")));
            this.upJisshiKbn.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.upJisshiKbn.popupWindowSetting = null;
            this.upJisshiKbn.ReadOnly = true;
            this.upJisshiKbn.RegistCheckMethod = null;
            this.upJisshiKbn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.upJisshiKbn.Width = 25;
            // 
            // filePath
            // 
            this.filePath.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.filePath.DefaultCellStyle = dataGridViewCellStyle2;
            this.filePath.FocusOutCheckMethod = null;
            this.filePath.HeaderText = "ファイルパス/ファイル名";
            this.filePath.Name = "filePath";
            this.filePath.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("filePath.PopupSearchSendParams")));
            this.filePath.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.filePath.popupWindowSetting = null;
            this.filePath.ReadOnly = true;
            this.filePath.RegistCheckMethod = null;
            this.filePath.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.filePath.Width = 930;
            // 
            // hidden_fileId
            // 
            this.hidden_fileId.DefaultBackColor = System.Drawing.Color.Empty;
            this.hidden_fileId.FocusOutCheckMethod = null;
            this.hidden_fileId.HeaderText = "";
            this.hidden_fileId.Name = "hidden_fileId";
            this.hidden_fileId.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("hidden_fileId.PopupSearchSendParams")));
            this.hidden_fileId.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.hidden_fileId.popupWindowSetting = null;
            this.hidden_fileId.ReadOnly = true;
            this.hidden_fileId.RegistCheckMethod = null;
            this.hidden_fileId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.hidden_fileId.Visible = false;
            this.hidden_fileId.Width = 25;

            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 567);
            this.Controls.Add(this.txt_FileName);
            this.Controls.Add(this.cntxt_HyoujiKbn);
            this.Controls.Add(this.lbl_FileName);
            this.Controls.Add(this.lbl_HyoujiKbn);
            this.Controls.Add(this.pnl_HyoujiKbn);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Name = "UIForm";
            this.Text = " ";
            this.Shown += new System.EventHandler(this.UIForm_Shown);
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.pnl_HyoujiKbn, 0);
            this.Controls.SetChildIndex(this.lbl_HyoujiKbn, 0);
            this.Controls.SetChildIndex(this.lbl_FileName, 0);
            this.Controls.SetChildIndex(this.cntxt_HyoujiKbn, 0);
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.txt_FileName, 0);
            this.pnl_HyoujiKbn.ResumeLayout(false);
            this.pnl_HyoujiKbn.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private r_framework.CustomControl.CustomRadioButton cRdbtn_HyoujiKbn_1;
        private r_framework.CustomControl.CustomRadioButton cRdbtn_HyoujiKbn_2;
        private r_framework.CustomControl.CustomPanel pnl_HyoujiKbn;
        public System.Windows.Forms.Label lbl_HyoujiKbn;
        public System.Windows.Forms.Label lbl_FileName;
        internal r_framework.CustomControl.CustomNumericTextBox2 cntxt_HyoujiKbn;
        internal r_framework.CustomControl.CustomTextBox txt_FileName;
        private r_framework.CustomControl.DgvCustomTextBoxColumn upJisshiKbn;
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
        private r_framework.CustomControl.DgvCustomTextBoxColumn filePath;
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
        private r_framework.CustomControl.DgvCustomTextBoxColumn hidden_fileId;
    }
}