namespace Shougun.Core.PaperManifest.JissekiHokokuUnpan
{
    partial class UIHeader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIHeader));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.txt_HoukokuTantoushaName = new r_framework.CustomControl.CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.radbtn_Uninclude = new r_framework.CustomControl.CustomRadioButton();
            this.txt_DenshiManiKbn = new r_framework.CustomControl.CustomNumericTextBox2();
            this.radbtn_Include = new r_framework.CustomControl.CustomRadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.customPanel2 = new r_framework.CustomControl.CustomPanel();
            this.radbtn_NotSum = new r_framework.CustomControl.CustomRadioButton();
            this.txt_CSVOutput = new r_framework.CustomControl.CustomNumericTextBox2();
            this.radbtn_Sum = new r_framework.CustomControl.CustomRadioButton();
            this.customPanel1.SuspendLayout();
            this.customPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Location = new System.Drawing.Point(9, 1);
            this.windowTypeLabel.Visible = false;
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(0, 3);
            this.lb_title.Size = new System.Drawing.Size(375, 34);
            this.lb_title.Text = "実績報告書（運搬実績）";
            // 
            // txt_HoukokuTantoushaName
            // 
            this.txt_HoukokuTantoushaName.BackColor = System.Drawing.SystemColors.Window;
            this.txt_HoukokuTantoushaName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_HoukokuTantoushaName.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.txt_HoukokuTantoushaName.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_HoukokuTantoushaName.DisplayItemName = "報告担当者名";
            this.txt_HoukokuTantoushaName.DisplayPopUp = null;
            this.txt_HoukokuTantoushaName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_HoukokuTantoushaName.FocusOutCheckMethod")));
            this.txt_HoukokuTantoushaName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.txt_HoukokuTantoushaName.ForeColor = System.Drawing.Color.Black;
            this.txt_HoukokuTantoushaName.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.txt_HoukokuTantoushaName.IsInputErrorOccured = false;
            this.txt_HoukokuTantoushaName.Location = new System.Drawing.Point(525, 3);
            this.txt_HoukokuTantoushaName.MaxLength = 10;
            this.txt_HoukokuTantoushaName.Name = "txt_HoukokuTantoushaName";
            this.txt_HoukokuTantoushaName.PopupAfterExecute = null;
            this.txt_HoukokuTantoushaName.PopupBeforeExecute = null;
            this.txt_HoukokuTantoushaName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_HoukokuTantoushaName.PopupSearchSendParams")));
            this.txt_HoukokuTantoushaName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_HoukokuTantoushaName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_HoukokuTantoushaName.popupWindowSetting")));
            this.txt_HoukokuTantoushaName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_HoukokuTantoushaName.RegistCheckMethod")));
            this.txt_HoukokuTantoushaName.ShortItemName = "報告担当者名";
            this.txt_HoukokuTantoushaName.Size = new System.Drawing.Size(134, 20);
            this.txt_HoukokuTantoushaName.TabIndex = 0;
            this.txt_HoukokuTantoushaName.Tag = "報告担当者名を入力してください";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(387, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 20);
            this.label1.TabIndex = 390;
            this.label1.Text = "報告担当者";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(387, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 20);
            this.label2.TabIndex = 391;
            this.label2.Text = "電子マニフェスト";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel1
            // 
            this.customPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel1.Controls.Add(this.radbtn_Uninclude);
            this.customPanel1.Controls.Add(this.txt_DenshiManiKbn);
            this.customPanel1.Controls.Add(this.radbtn_Include);
            this.customPanel1.Location = new System.Drawing.Point(525, 25);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(188, 20);
            this.customPanel1.TabIndex = 1;
            // 
            // radbtn_Uninclude
            // 
            this.radbtn_Uninclude.AutoSize = true;
            this.radbtn_Uninclude.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Uninclude.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Uninclude.FocusOutCheckMethod")));
            this.radbtn_Uninclude.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_Uninclude.LinkedTextBox = "txt_DenshiManiKbn";
            this.radbtn_Uninclude.Location = new System.Drawing.Point(89, 1);
            this.radbtn_Uninclude.Name = "radbtn_Uninclude";
            this.radbtn_Uninclude.PopupAfterExecute = null;
            this.radbtn_Uninclude.PopupBeforeExecute = null;
            this.radbtn_Uninclude.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Uninclude.PopupSearchSendParams")));
            this.radbtn_Uninclude.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Uninclude.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Uninclude.popupWindowSetting")));
            this.radbtn_Uninclude.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Uninclude.RegistCheckMethod")));
            this.radbtn_Uninclude.Size = new System.Drawing.Size(95, 17);
            this.radbtn_Uninclude.TabIndex = 2;
            this.radbtn_Uninclude.Tag = "電子マニフェストを含まない場合にチェックをつけてください";
            this.radbtn_Uninclude.Text = "2.含まない";
            this.radbtn_Uninclude.UseVisualStyleBackColor = true;
            this.radbtn_Uninclude.Value = "2";
            // 
            // txt_DenshiManiKbn
            // 
            this.txt_DenshiManiKbn.BackColor = System.Drawing.SystemColors.Window;
            this.txt_DenshiManiKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_DenshiManiKbn.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_DenshiManiKbn.DisplayItemName = "電子マニフェスト区分";
            this.txt_DenshiManiKbn.DisplayPopUp = null;
            this.txt_DenshiManiKbn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_DenshiManiKbn.FocusOutCheckMethod")));
            this.txt_DenshiManiKbn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txt_DenshiManiKbn.ForeColor = System.Drawing.Color.Black;
            this.txt_DenshiManiKbn.IsInputErrorOccured = false;
            this.txt_DenshiManiKbn.LinkedRadioButtonArray = new string[] {
        "radbtn_Include",
        "radbtn_Uninclude"};
            this.txt_DenshiManiKbn.Location = new System.Drawing.Point(-1, -1);
            this.txt_DenshiManiKbn.Name = "txt_DenshiManiKbn";
            this.txt_DenshiManiKbn.PopupAfterExecute = null;
            this.txt_DenshiManiKbn.PopupBeforeExecute = null;
            this.txt_DenshiManiKbn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_DenshiManiKbn.PopupSearchSendParams")));
            this.txt_DenshiManiKbn.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_DenshiManiKbn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_DenshiManiKbn.popupWindowSetting")));
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
            this.txt_DenshiManiKbn.RangeSetting = rangeSettingDto1;
            this.txt_DenshiManiKbn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_DenshiManiKbn.RegistCheckMethod")));
            this.txt_DenshiManiKbn.ShortItemName = "電子マニフェスト区分";
            this.txt_DenshiManiKbn.Size = new System.Drawing.Size(20, 20);
            this.txt_DenshiManiKbn.TabIndex = 0;
            this.txt_DenshiManiKbn.Tag = "実績データに電子マニフェストを含めるか選択してください";
            this.txt_DenshiManiKbn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_DenshiManiKbn.WordWrap = false;
            // 
            // radbtn_Include
            // 
            this.radbtn_Include.AutoSize = true;
            this.radbtn_Include.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Include.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Include.FocusOutCheckMethod")));
            this.radbtn_Include.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_Include.LinkedTextBox = "txt_DenshiManiKbn";
            this.radbtn_Include.Location = new System.Drawing.Point(22, 1);
            this.radbtn_Include.Name = "radbtn_Include";
            this.radbtn_Include.PopupAfterExecute = null;
            this.radbtn_Include.PopupBeforeExecute = null;
            this.radbtn_Include.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Include.PopupSearchSendParams")));
            this.radbtn_Include.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Include.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Include.popupWindowSetting")));
            this.radbtn_Include.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Include.RegistCheckMethod")));
            this.radbtn_Include.Size = new System.Drawing.Size(67, 17);
            this.radbtn_Include.TabIndex = 1;
            this.radbtn_Include.Tag = "電子マニフェストを含める場合にチェックをつけてください";
            this.radbtn_Include.Text = "1.含む";
            this.radbtn_Include.UseVisualStyleBackColor = true;
            this.radbtn_Include.Value = "1";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(719, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(230, 20);
            this.label3.TabIndex = 393;
            this.label3.Text = "ＣＳＶ出力";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel2
            // 
            this.customPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel2.Controls.Add(this.radbtn_NotSum);
            this.customPanel2.Controls.Add(this.txt_CSVOutput);
            this.customPanel2.Controls.Add(this.radbtn_Sum);
            this.customPanel2.Location = new System.Drawing.Point(719, 25);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(230, 20);
            this.customPanel2.TabIndex = 2;
            // 
            // radbtn_NotSum
            // 
            this.radbtn_NotSum.AutoSize = true;
            this.radbtn_NotSum.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_NotSum.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_NotSum.FocusOutCheckMethod")));
            this.radbtn_NotSum.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_NotSum.LinkedTextBox = "txt_CSVOutput";
            this.radbtn_NotSum.Location = new System.Drawing.Point(124, 1);
            this.radbtn_NotSum.Name = "radbtn_NotSum";
            this.radbtn_NotSum.PopupAfterExecute = null;
            this.radbtn_NotSum.PopupBeforeExecute = null;
            this.radbtn_NotSum.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_NotSum.PopupSearchSendParams")));
            this.radbtn_NotSum.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_NotSum.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_NotSum.popupWindowSetting")));
            this.radbtn_NotSum.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_NotSum.RegistCheckMethod")));
            this.radbtn_NotSum.Size = new System.Drawing.Size(95, 17);
            this.radbtn_NotSum.TabIndex = 2;
            this.radbtn_NotSum.Tag = "集計せずに出力する場合に選択してください";
            this.radbtn_NotSum.Text = "2.集計無し";
            this.radbtn_NotSum.UseVisualStyleBackColor = true;
            this.radbtn_NotSum.Value = "2";
            // 
            // txt_CSVOutput
            // 
            this.txt_CSVOutput.BackColor = System.Drawing.SystemColors.Window;
            this.txt_CSVOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_CSVOutput.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_CSVOutput.DisplayItemName = "CSV出力区分";
            this.txt_CSVOutput.DisplayPopUp = null;
            this.txt_CSVOutput.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_CSVOutput.FocusOutCheckMethod")));
            this.txt_CSVOutput.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txt_CSVOutput.ForeColor = System.Drawing.Color.Black;
            this.txt_CSVOutput.IsInputErrorOccured = false;
            this.txt_CSVOutput.LinkedRadioButtonArray = new string[] {
        "radbtn_Sum",
        "radbtn_NotSum"};
            this.txt_CSVOutput.Location = new System.Drawing.Point(-1, -1);
            this.txt_CSVOutput.Name = "txt_CSVOutput";
            this.txt_CSVOutput.PopupAfterExecute = null;
            this.txt_CSVOutput.PopupBeforeExecute = null;
            this.txt_CSVOutput.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_CSVOutput.PopupSearchSendParams")));
            this.txt_CSVOutput.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_CSVOutput.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_CSVOutput.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto2.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txt_CSVOutput.RangeSetting = rangeSettingDto2;
            this.txt_CSVOutput.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_CSVOutput.RegistCheckMethod")));
            this.txt_CSVOutput.ShortItemName = "CSV出力区分";
            this.txt_CSVOutput.Size = new System.Drawing.Size(20, 20);
            this.txt_CSVOutput.TabIndex = 0;
            this.txt_CSVOutput.Tag = "1,2のいずれかを選択してください";
            this.txt_CSVOutput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_CSVOutput.WordWrap = false;
            // 
            // radbtn_Sum
            // 
            this.radbtn_Sum.AutoSize = true;
            this.radbtn_Sum.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Sum.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Sum.FocusOutCheckMethod")));
            this.radbtn_Sum.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_Sum.LinkedTextBox = "txt_CSVOutput";
            this.radbtn_Sum.Location = new System.Drawing.Point(22, 1);
            this.radbtn_Sum.Name = "radbtn_Sum";
            this.radbtn_Sum.PopupAfterExecute = null;
            this.radbtn_Sum.PopupBeforeExecute = null;
            this.radbtn_Sum.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Sum.PopupSearchSendParams")));
            this.radbtn_Sum.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Sum.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Sum.popupWindowSetting")));
            this.radbtn_Sum.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Sum.RegistCheckMethod")));
            this.radbtn_Sum.Size = new System.Drawing.Size(95, 17);
            this.radbtn_Sum.TabIndex = 1;
            this.radbtn_Sum.Tag = "集計されたデータを出力する場合に選択してください";
            this.radbtn_Sum.Text = "1.集計有り";
            this.radbtn_Sum.UseVisualStyleBackColor = true;
            this.radbtn_Sum.Value = "1";
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.customPanel2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.customPanel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_HoukokuTantoushaName);
            this.Name = "UIHeader";
            this.Text = "建廃マニフェスト";
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.txt_HoukokuTantoushaName, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.customPanel1, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.customPanel2, 0);
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.customPanel2.ResumeLayout(false);
            this.customPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomTextBox txt_HoukokuTantoushaName;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label2;
        private r_framework.CustomControl.CustomPanel customPanel1;
        public r_framework.CustomControl.CustomRadioButton radbtn_Uninclude;
        public r_framework.CustomControl.CustomNumericTextBox2 txt_DenshiManiKbn;
        public r_framework.CustomControl.CustomRadioButton radbtn_Include;
        internal System.Windows.Forms.Label label3;
        private r_framework.CustomControl.CustomPanel customPanel2;
        public r_framework.CustomControl.CustomRadioButton radbtn_NotSum;
        public r_framework.CustomControl.CustomNumericTextBox2 txt_CSVOutput;
        public r_framework.CustomControl.CustomRadioButton radbtn_Sum;



    }
}