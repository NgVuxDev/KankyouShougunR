namespace Shougun.Core.Common.HanyoCSVShutsuryoku
{
    partial class UIForm
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

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.lblCondition = new System.Windows.Forms.Label();
            this.txtCondition = new r_framework.CustomControl.CustomTextBox();
            this.txtJokenDisp = new r_framework.CustomControl.CustomTextBox();
            this.dgvPatterns = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.dgcOutputKbnName = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgcPatternNm = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgcPatternBikou = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvSystemId = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.dgcSeq = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatterns)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCondition
            // 
            this.lblCondition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblCondition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCondition.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblCondition.ForeColor = System.Drawing.Color.White;
            this.lblCondition.Location = new System.Drawing.Point(0, 126);
            this.lblCondition.Name = "lblCondition";
            this.lblCondition.Size = new System.Drawing.Size(110, 20);
            this.lblCondition.TabIndex = 20;
            this.lblCondition.Text = "パターン名";
            this.lblCondition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCondition
            // 
            this.txtCondition.BackColor = System.Drawing.SystemColors.Window;
            this.txtCondition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCondition.CharactersNumber = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.txtCondition.DBFieldsName = "";
            this.txtCondition.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtCondition.DisplayItemName = "検索条件";
            this.txtCondition.DisplayPopUp = null;
            this.txtCondition.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtCondition.FocusOutCheckMethod")));
            this.txtCondition.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtCondition.ForeColor = System.Drawing.Color.Black;
            this.txtCondition.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.txtCondition.IsInputErrorOccured = false;
            this.txtCondition.Location = new System.Drawing.Point(115, 126);
            this.txtCondition.MaxLength = 60;
            this.txtCondition.Name = "txtCondition";
            this.txtCondition.PopupAfterExecute = null;
            this.txtCondition.PopupBeforeExecute = null;
            this.txtCondition.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtCondition.PopupSearchSendParams")));
            this.txtCondition.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtCondition.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtCondition.popupWindowSetting")));
            this.txtCondition.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtCondition.RegistCheckMethod")));
            this.txtCondition.Size = new System.Drawing.Size(686, 20);
            this.txtCondition.TabIndex = 30;
            this.txtCondition.Tag = "検索するパターン名を入力してください";
            // 
            // txtJokenDisp
            // 
            this.txtJokenDisp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtJokenDisp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJokenDisp.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.txtJokenDisp.DBFieldsName = "txtJokenDisp";
            this.txtJokenDisp.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtJokenDisp.DisplayItemName = "";
            this.txtJokenDisp.DisplayPopUp = null;
            this.txtJokenDisp.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtJokenDisp.FocusOutCheckMethod")));
            this.txtJokenDisp.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtJokenDisp.ForeColor = System.Drawing.Color.Black;
            this.txtJokenDisp.ImeMode = System.Windows.Forms.ImeMode.Katakana;
            this.txtJokenDisp.IsInputErrorOccured = false;
            this.txtJokenDisp.ItemDefinedTypes = "varchar";
            this.txtJokenDisp.Location = new System.Drawing.Point(0, 2);
            this.txtJokenDisp.Multiline = true;
            this.txtJokenDisp.Name = "txtJokenDisp";
            this.txtJokenDisp.PopupAfterExecute = null;
            this.txtJokenDisp.PopupBeforeExecute = null;
            this.txtJokenDisp.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtJokenDisp.PopupSearchSendParams")));
            this.txtJokenDisp.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtJokenDisp.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtJokenDisp.popupWindowSetting")));
            this.txtJokenDisp.ReadOnly = true;
            this.txtJokenDisp.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtJokenDisp.RegistCheckMethod")));
            this.txtJokenDisp.ShortItemName = "";
            this.txtJokenDisp.Size = new System.Drawing.Size(801, 121);
            this.txtJokenDisp.TabIndex = 10;
            this.txtJokenDisp.TabStop = false;
            this.txtJokenDisp.Tag = "出力条件が表示されます";
            // 
            // dgvPatterns
            // 
            this.dgvPatterns.AllowUserToAddRows = false;
            this.dgvPatterns.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPatterns.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPatterns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPatterns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgcOutputKbnName,
            this.dgcPatternNm,
            this.dgcPatternBikou,
            this.dgvSystemId,
            this.dgcSeq});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPatterns.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvPatterns.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvPatterns.EnableHeadersVisualStyles = false;
            this.dgvPatterns.GridColor = System.Drawing.Color.White;
            this.dgvPatterns.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dgvPatterns.IsBrowsePurpose = true;
            this.dgvPatterns.IsReload = false;
            this.dgvPatterns.LinkedDataPanelName = null;
            this.dgvPatterns.Location = new System.Drawing.Point(0, 149);
            this.dgvPatterns.MultiSelect = false;
            this.dgvPatterns.Name = "dgvPatterns";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPatterns.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvPatterns.RowHeadersVisible = false;
            this.dgvPatterns.RowTemplate.Height = 21;
            this.dgvPatterns.ShowCellToolTips = false;
            this.dgvPatterns.Size = new System.Drawing.Size(976, 329);
            this.dgvPatterns.TabIndex = 40;
            this.dgvPatterns.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvPatterns_CellMouseDoubleClick);
            // 
            // dgcOutputKbnName
            // 
            this.dgcOutputKbnName.DataPropertyName = "OUTPUT_KBN_NAME";
            this.dgcOutputKbnName.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.dgcOutputKbnName.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgcOutputKbnName.DisplayItemName = "OUTPUT_KBN_NAME";
            this.dgcOutputKbnName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgcOutputKbnName.FocusOutCheckMethod")));
            this.dgcOutputKbnName.HeaderText = "出力区分";
            this.dgcOutputKbnName.Name = "dgcOutputKbnName";
            this.dgcOutputKbnName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgcOutputKbnName.PopupSearchSendParams")));
            this.dgcOutputKbnName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgcOutputKbnName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgcOutputKbnName.popupWindowSetting")));
            this.dgcOutputKbnName.ReadOnly = true;
            this.dgcOutputKbnName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgcOutputKbnName.RegistCheckMethod")));
            this.dgcOutputKbnName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgcOutputKbnName.ToolTipText = "出力区分が表示されます";
            this.dgcOutputKbnName.Width = 80;
            // 
            // dgcPatternNm
            // 
            this.dgcPatternNm.DataPropertyName = "PATTERN_NAME";
            this.dgcPatternNm.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.dgcPatternNm.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgcPatternNm.DisplayItemName = "PATTERN_NAME";
            this.dgcPatternNm.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgcPatternNm.FocusOutCheckMethod")));
            this.dgcPatternNm.HeaderText = "パターン名";
            this.dgcPatternNm.Name = "dgcPatternNm";
            this.dgcPatternNm.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgcPatternNm.PopupSearchSendParams")));
            this.dgcPatternNm.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgcPatternNm.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgcPatternNm.popupWindowSetting")));
            this.dgcPatternNm.ReadOnly = true;
            this.dgcPatternNm.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgcPatternNm.RegistCheckMethod")));
            this.dgcPatternNm.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgcPatternNm.ToolTipText = "パターン名が表示されます";
            this.dgcPatternNm.Width = 438;
            // 
            // dgcPatternBikou
            // 
            this.dgcPatternBikou.DataPropertyName = "PATTERN_BIKOU";
            this.dgcPatternBikou.DBFieldsName = "PATTERN_BIKOU";
            this.dgcPatternBikou.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.dgcPatternBikou.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgcPatternBikou.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgcPatternBikou.FocusOutCheckMethod")));
            this.dgcPatternBikou.HeaderText = "パターン備考";
            this.dgcPatternBikou.Name = "dgcPatternBikou";
            this.dgcPatternBikou.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgcPatternBikou.PopupSearchSendParams")));
            this.dgcPatternBikou.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgcPatternBikou.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgcPatternBikou.popupWindowSetting")));
            this.dgcPatternBikou.ReadOnly = true;
            this.dgcPatternBikou.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgcPatternBikou.RegistCheckMethod")));
            this.dgcPatternBikou.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgcPatternBikou.ToolTipText = "パターン備考が表示されます";
            this.dgcPatternBikou.Width = 438;
            // 
            // dgvSystemId
            // 
            this.dgvSystemId.DataPropertyName = "SYSTEM_ID";
            this.dgvSystemId.DBFieldsName = "SYSTEM_ID";
            this.dgvSystemId.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvSystemId.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvSystemId.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvSystemId.FocusOutCheckMethod")));
            this.dgvSystemId.HeaderText = "";
            this.dgvSystemId.Name = "dgvSystemId";
            this.dgvSystemId.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvSystemId.PopupSearchSendParams")));
            this.dgvSystemId.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvSystemId.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvSystemId.popupWindowSetting")));
            this.dgvSystemId.RangeSetting = rangeSettingDto1;
            this.dgvSystemId.ReadOnly = true;
            this.dgvSystemId.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvSystemId.RegistCheckMethod")));
            this.dgvSystemId.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSystemId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvSystemId.Visible = false;
            this.dgvSystemId.Width = 5;
            // 
            // dgcSeq
            // 
            this.dgcSeq.DataPropertyName = "SEQ";
            this.dgcSeq.DBFieldsName = "SEQ";
            this.dgcSeq.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.dgcSeq.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgcSeq.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgcSeq.FocusOutCheckMethod")));
            this.dgcSeq.HeaderText = "";
            this.dgcSeq.Name = "dgcSeq";
            this.dgcSeq.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgcSeq.PopupSearchSendParams")));
            this.dgcSeq.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgcSeq.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgcSeq.popupWindowSetting")));
            this.dgcSeq.RangeSetting = rangeSettingDto2;
            this.dgcSeq.ReadOnly = true;
            this.dgcSeq.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgcSeq.RegistCheckMethod")));
            this.dgcSeq.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgcSeq.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgcSeq.Visible = false;
            this.dgcSeq.Width = 5;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(978, 490);
            this.Controls.Add(this.txtCondition);
            this.Controls.Add(this.lblCondition);
            this.Controls.Add(this.txtJokenDisp);
            this.Controls.Add(this.dgvPatterns);
            this.Name = "UIForm";
            this.Text = "UIForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPatterns)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCondition;
        internal r_framework.CustomControl.CustomTextBox txtCondition;
        internal r_framework.CustomControl.CustomTextBox txtJokenDisp;
        internal r_framework.CustomControl.CustomDataGridView dgvPatterns;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgcOutputKbnName;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgcPatternNm;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgcPatternBikou;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column dgvSystemId;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column dgcSeq;
    }
}