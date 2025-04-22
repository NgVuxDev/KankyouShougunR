namespace Shougun.Core.PaperManifest.JissekiHokokuSisetsu
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.HOKOKU_TANTO_NAME = new r_framework.CustomControl.CustomTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDENMANI_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.rdo_fukumu = new r_framework.CustomControl.CustomRadioButton();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.rdo_fukumanai = new r_framework.CustomControl.CustomRadioButton();
            this.txtNum_Syukeiari = new r_framework.CustomControl.CustomNumericTextBox2();
            this.customPanel2 = new r_framework.CustomControl.CustomPanel();
            this.rdo_musi = new r_framework.CustomControl.CustomRadioButton();
            this.rdo_ari = new r_framework.CustomControl.CustomRadioButton();
            this.customPanel1.SuspendLayout();
            this.customPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Size = new System.Drawing.Size(25, 30);
            this.windowTypeLabel.Visible = false;
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(31, 5);
            this.lb_title.Size = new System.Drawing.Size(419, 34);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(468, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 20);
            this.label2.TabIndex = 530;
            this.label2.Text = "報告担当者名";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(799, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 528;
            this.label1.Text = "ＣＳＶ出力";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HOKOKU_TANTO_NAME
            // 
            this.HOKOKU_TANTO_NAME.BackColor = System.Drawing.SystemColors.Window;
            this.HOKOKU_TANTO_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HOKOKU_TANTO_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.HOKOKU_TANTO_NAME.DisplayPopUp = null;
            this.HOKOKU_TANTO_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOKOKU_TANTO_NAME.FocusOutCheckMethod")));
            this.HOKOKU_TANTO_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HOKOKU_TANTO_NAME.ForeColor = System.Drawing.Color.Black;
            this.HOKOKU_TANTO_NAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.HOKOKU_TANTO_NAME.IsInputErrorOccured = false;
            this.HOKOKU_TANTO_NAME.Location = new System.Drawing.Point(599, 2);
            this.HOKOKU_TANTO_NAME.MaxLength = 10;
            this.HOKOKU_TANTO_NAME.Name = "HOKOKU_TANTO_NAME";
            this.HOKOKU_TANTO_NAME.PopupAfterExecute = null;
            this.HOKOKU_TANTO_NAME.PopupBeforeExecute = null;
            this.HOKOKU_TANTO_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HOKOKU_TANTO_NAME.PopupSearchSendParams")));
            this.HOKOKU_TANTO_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HOKOKU_TANTO_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HOKOKU_TANTO_NAME.popupWindowSetting")));
            this.HOKOKU_TANTO_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOKOKU_TANTO_NAME.RegistCheckMethod")));
            this.HOKOKU_TANTO_NAME.Size = new System.Drawing.Size(146, 20);
            this.HOKOKU_TANTO_NAME.TabIndex = 537;
            this.HOKOKU_TANTO_NAME.TabStop = false;
            this.HOKOKU_TANTO_NAME.Tag = "報告担当者名を入力してください";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(468, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 20);
            this.label3.TabIndex = 536;
            this.label3.Text = "電子マニフェスト";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDENMANI_KBN
            // 
            this.txtDENMANI_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.txtDENMANI_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDENMANI_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtDENMANI_KBN.DisplayItemName = "電子マニフェスト区分";
            this.txtDENMANI_KBN.DisplayPopUp = null;
            this.txtDENMANI_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtDENMANI_KBN.FocusOutCheckMethod")));
            this.txtDENMANI_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtDENMANI_KBN.ForeColor = System.Drawing.Color.Black;
            this.txtDENMANI_KBN.IsInputErrorOccured = false;
            this.txtDENMANI_KBN.LinkedRadioButtonArray = new string[] {
        "rdo_fukumu",
        "rdo_fukumanai"};
            this.txtDENMANI_KBN.Location = new System.Drawing.Point(599, 24);
            this.txtDENMANI_KBN.Name = "txtDENMANI_KBN";
            this.txtDENMANI_KBN.PopupAfterExecute = null;
            this.txtDENMANI_KBN.PopupBeforeExecute = null;
            this.txtDENMANI_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtDENMANI_KBN.PopupSearchSendParams")));
            this.txtDENMANI_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtDENMANI_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtDENMANI_KBN.popupWindowSetting")));
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
            this.txtDENMANI_KBN.RangeSetting = rangeSettingDto1;
            this.txtDENMANI_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtDENMANI_KBN.RegistCheckMethod")));
            this.txtDENMANI_KBN.Size = new System.Drawing.Size(20, 20);
            this.txtDENMANI_KBN.TabIndex = 539;
            this.txtDENMANI_KBN.Tag = "実績データに電子マニフェストを含めるか選択してください";
            this.txtDENMANI_KBN.WordWrap = false;
            // 
            // rdo_fukumu
            // 
            this.rdo_fukumu.AutoSize = true;
            this.rdo_fukumu.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdo_fukumu.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_fukumu.FocusOutCheckMethod")));
            this.rdo_fukumu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdo_fukumu.LinkedTextBox = "txtDENMANI_KBN";
            this.rdo_fukumu.Location = new System.Drawing.Point(4, 1);
            this.rdo_fukumu.Name = "rdo_fukumu";
            this.rdo_fukumu.PopupAfterExecute = null;
            this.rdo_fukumu.PopupBeforeExecute = null;
            this.rdo_fukumu.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdo_fukumu.PopupSearchSendParams")));
            this.rdo_fukumu.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdo_fukumu.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdo_fukumu.popupWindowSetting")));
            this.rdo_fukumu.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_fukumu.RegistCheckMethod")));
            this.rdo_fukumu.Size = new System.Drawing.Size(67, 17);
            this.rdo_fukumu.TabIndex = 303;
            this.rdo_fukumu.Tag = "電子マニフェストを含める場合にチェックをつけてください";
            this.rdo_fukumu.Text = "1.含む";
            this.rdo_fukumu.UseVisualStyleBackColor = true;
            this.rdo_fukumu.Value = "1";
            // 
            // customPanel1
            // 
            this.customPanel1.Controls.Add(this.rdo_fukumanai);
            this.customPanel1.Controls.Add(this.rdo_fukumu);
            this.customPanel1.Location = new System.Drawing.Point(618, 24);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(174, 20);
            this.customPanel1.TabIndex = 540;
            // 
            // rdo_fukumanai
            // 
            this.rdo_fukumanai.AutoSize = true;
            this.rdo_fukumanai.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdo_fukumanai.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_fukumanai.FocusOutCheckMethod")));
            this.rdo_fukumanai.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdo_fukumanai.LinkedTextBox = "txtDENMANI_KBN";
            this.rdo_fukumanai.Location = new System.Drawing.Point(72, 1);
            this.rdo_fukumanai.Name = "rdo_fukumanai";
            this.rdo_fukumanai.PopupAfterExecute = null;
            this.rdo_fukumanai.PopupBeforeExecute = null;
            this.rdo_fukumanai.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdo_fukumanai.PopupSearchSendParams")));
            this.rdo_fukumanai.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdo_fukumanai.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdo_fukumanai.popupWindowSetting")));
            this.rdo_fukumanai.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_fukumanai.RegistCheckMethod")));
            this.rdo_fukumanai.Size = new System.Drawing.Size(95, 17);
            this.rdo_fukumanai.TabIndex = 304;
            this.rdo_fukumanai.Tag = "電子マニフェストを含まない場合にチェックをつけてください";
            this.rdo_fukumanai.Text = "2.含まない";
            this.rdo_fukumanai.UseVisualStyleBackColor = true;
            this.rdo_fukumanai.Value = "2";
            // 
            // txtNum_Syukeiari
            // 
            this.txtNum_Syukeiari.BackColor = System.Drawing.SystemColors.Window;
            this.txtNum_Syukeiari.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNum_Syukeiari.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtNum_Syukeiari.DisplayItemName = "CSV出力";
            this.txtNum_Syukeiari.DisplayPopUp = null;
            this.txtNum_Syukeiari.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_Syukeiari.FocusOutCheckMethod")));
            this.txtNum_Syukeiari.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtNum_Syukeiari.ForeColor = System.Drawing.Color.Black;
            this.txtNum_Syukeiari.IsInputErrorOccured = false;
            this.txtNum_Syukeiari.LinkedRadioButtonArray = new string[] {
        "rdo_ari",
        "rdo_musi"};
            this.txtNum_Syukeiari.Location = new System.Drawing.Point(799, 24);
            this.txtNum_Syukeiari.Name = "txtNum_Syukeiari";
            this.txtNum_Syukeiari.PopupAfterExecute = null;
            this.txtNum_Syukeiari.PopupBeforeExecute = null;
            this.txtNum_Syukeiari.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtNum_Syukeiari.PopupSearchSendParams")));
            this.txtNum_Syukeiari.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtNum_Syukeiari.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtNum_Syukeiari.popupWindowSetting")));
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
            this.txtNum_Syukeiari.RangeSetting = rangeSettingDto2;
            this.txtNum_Syukeiari.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_Syukeiari.RegistCheckMethod")));
            this.txtNum_Syukeiari.Size = new System.Drawing.Size(20, 20);
            this.txtNum_Syukeiari.TabIndex = 541;
            this.txtNum_Syukeiari.Tag = "1,2のいずれかを選択してください";
            this.txtNum_Syukeiari.WordWrap = false;
            // 
            // customPanel2
            // 
            this.customPanel2.Controls.Add(this.rdo_musi);
            this.customPanel2.Controls.Add(this.rdo_ari);
            this.customPanel2.Location = new System.Drawing.Point(818, 24);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(198, 20);
            this.customPanel2.TabIndex = 542;
            // 
            // rdo_musi
            // 
            this.rdo_musi.AutoSize = true;
            this.rdo_musi.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdo_musi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_musi.FocusOutCheckMethod")));
            this.rdo_musi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdo_musi.LinkedTextBox = "txtNum_Syukeiari";
            this.rdo_musi.Location = new System.Drawing.Point(102, 1);
            this.rdo_musi.Name = "rdo_musi";
            this.rdo_musi.PopupAfterExecute = null;
            this.rdo_musi.PopupBeforeExecute = null;
            this.rdo_musi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdo_musi.PopupSearchSendParams")));
            this.rdo_musi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdo_musi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdo_musi.popupWindowSetting")));
            this.rdo_musi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_musi.RegistCheckMethod")));
            this.rdo_musi.Size = new System.Drawing.Size(95, 17);
            this.rdo_musi.TabIndex = 304;
            this.rdo_musi.Tag = "集計せずに出力する場合に選択してください";
            this.rdo_musi.Text = "2.集計無し";
            this.rdo_musi.UseVisualStyleBackColor = true;
            this.rdo_musi.Value = "2";
            // 
            // rdo_ari
            // 
            this.rdo_ari.AutoSize = true;
            this.rdo_ari.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdo_ari.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_ari.FocusOutCheckMethod")));
            this.rdo_ari.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdo_ari.LinkedTextBox = "txtNum_Syukeiari";
            this.rdo_ari.Location = new System.Drawing.Point(6, 1);
            this.rdo_ari.Name = "rdo_ari";
            this.rdo_ari.PopupAfterExecute = null;
            this.rdo_ari.PopupBeforeExecute = null;
            this.rdo_ari.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdo_ari.PopupSearchSendParams")));
            this.rdo_ari.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdo_ari.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdo_ari.popupWindowSetting")));
            this.rdo_ari.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_ari.RegistCheckMethod")));
            this.rdo_ari.Size = new System.Drawing.Size(95, 17);
            this.rdo_ari.TabIndex = 303;
            this.rdo_ari.Tag = "集計されたデータを出力する場合に選択してください";
            this.rdo_ari.Text = "1.集計有り";
            this.rdo_ari.UseVisualStyleBackColor = true;
            this.rdo_ari.Value = "1";
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 46);
            this.Controls.Add(this.txtDENMANI_KBN);
            this.Controls.Add(this.customPanel1);
            this.Controls.Add(this.txtNum_Syukeiari);
            this.Controls.Add(this.HOKOKU_TANTO_NAME);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.customPanel2);
            this.Controls.Add(this.label1);
            this.Name = "UIHeader";
            this.Text = "HeaderSample";
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.customPanel2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.HOKOKU_TANTO_NAME, 0);
            this.Controls.SetChildIndex(this.txtNum_Syukeiari, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.customPanel1, 0);
            this.Controls.SetChildIndex(this.txtDENMANI_KBN, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.customPanel2.ResumeLayout(false);
            this.customPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomTextBox HOKOKU_TANTO_NAME;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label3;
        public r_framework.CustomControl.CustomNumericTextBox2 txtDENMANI_KBN;
        public r_framework.CustomControl.CustomRadioButton rdo_fukumu;
        private r_framework.CustomControl.CustomPanel customPanel1;
        public r_framework.CustomControl.CustomRadioButton rdo_fukumanai;
        public r_framework.CustomControl.CustomNumericTextBox2 txtNum_Syukeiari;
        private r_framework.CustomControl.CustomPanel customPanel2;
        public r_framework.CustomControl.CustomRadioButton rdo_musi;
        public r_framework.CustomControl.CustomRadioButton rdo_ari;

    }
}