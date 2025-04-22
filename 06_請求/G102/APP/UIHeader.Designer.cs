namespace Shougun.Core.Billing.Seikyushokakunin
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
            this.lbl_kyoten = new System.Windows.Forms.Label();
            this.KYOTEN_NM = new r_framework.CustomControl.CustomTextBox();
            this.KYOTEN_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.customRadioButton13 = new r_framework.CustomControl.CustomRadioButton();
            this.customRadioButton14 = new r_framework.CustomControl.CustomRadioButton();
            this.SEIKYU_HAKKOU = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ZeiRate_Chk = new System.Windows.Forms.CheckBox();
            this.customPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Location = new System.Drawing.Point(8, 8);
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(84, 6);
            this.lb_title.Size = new System.Drawing.Size(275, 34);
            this.lb_title.Text = "請求書確認";
            // 
            // lbl_kyoten
            // 
            this.lbl_kyoten.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_kyoten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_kyoten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_kyoten.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_kyoten.ForeColor = System.Drawing.Color.White;
            this.lbl_kyoten.Location = new System.Drawing.Point(363, 24);
            this.lbl_kyoten.Name = "lbl_kyoten";
            this.lbl_kyoten.Size = new System.Drawing.Size(110, 20);
            this.lbl_kyoten.TabIndex = 499;
            this.lbl_kyoten.Text = "拠点";
            this.lbl_kyoten.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KYOTEN_NM
            // 
            this.KYOTEN_NM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KYOTEN_NM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_NM.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.KYOTEN_NM.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_NM.DisplayPopUp = null;
            this.KYOTEN_NM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NM.FocusOutCheckMethod")));
            this.KYOTEN_NM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_NM.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_NM.IsInputErrorOccured = false;
            this.KYOTEN_NM.Location = new System.Drawing.Point(530, 24);
            this.KYOTEN_NM.MaxLength = 0;
            this.KYOTEN_NM.Name = "KYOTEN_NM";
            this.KYOTEN_NM.PopupAfterExecute = null;
            this.KYOTEN_NM.PopupBeforeExecute = null;
            this.KYOTEN_NM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_NM.PopupSearchSendParams")));
            this.KYOTEN_NM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOTEN_NM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_NM.popupWindowSetting")));
            this.KYOTEN_NM.ReadOnly = true;
            this.KYOTEN_NM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NM.RegistCheckMethod")));
            this.KYOTEN_NM.Size = new System.Drawing.Size(212, 20);
            this.KYOTEN_NM.TabIndex = 501;
            this.KYOTEN_NM.TabStop = false;
            this.KYOTEN_NM.Tag = "";
            // 
            // KYOTEN_CD
            // 
            this.KYOTEN_CD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KYOTEN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_CD.CharacterLimitList = null;
            this.KYOTEN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_CD.DisplayPopUp = null;
            this.KYOTEN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.FocusOutCheckMethod")));
            this.KYOTEN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_CD.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.KYOTEN_CD.IsInputErrorOccured = false;
            this.KYOTEN_CD.Location = new System.Drawing.Point(478, 24);
            this.KYOTEN_CD.Name = "KYOTEN_CD";
            this.KYOTEN_CD.PopupAfterExecute = null;
            this.KYOTEN_CD.PopupBeforeExecute = null;
            this.KYOTEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_CD.PopupSearchSendParams")));
            this.KYOTEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOTEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_CD.popupWindowSetting")));
            this.KYOTEN_CD.ReadOnly = true;
            this.KYOTEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.RegistCheckMethod")));
            this.KYOTEN_CD.Size = new System.Drawing.Size(53, 20);
            this.KYOTEN_CD.TabIndex = 505;
            this.KYOTEN_CD.TabStop = false;
            this.KYOTEN_CD.Tag = "";
            // 
            // customPanel1
            // 
            this.customPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel1.Controls.Add(this.customRadioButton13);
            this.customPanel1.Controls.Add(this.customRadioButton14);
            this.customPanel1.Controls.Add(this.SEIKYU_HAKKOU);
            this.customPanel1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.customPanel1.Location = new System.Drawing.Point(478, 2);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(264, 20);
            this.customPanel1.TabIndex = 506;
            // 
            // customRadioButton13
            // 
            this.customRadioButton13.AutoSize = true;
            this.customRadioButton13.DefaultBackColor = System.Drawing.Color.Empty;
            this.customRadioButton13.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customRadioButton13.FocusOutCheckMethod")));
            this.customRadioButton13.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.customRadioButton13.LinkedTextBox = "SEIKYU_HAKKOU";
            this.customRadioButton13.Location = new System.Drawing.Point(127, 0);
            this.customRadioButton13.Name = "customRadioButton13";
            this.customRadioButton13.PopupAfterExecute = null;
            this.customRadioButton13.PopupBeforeExecute = null;
            this.customRadioButton13.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customRadioButton13.PopupSearchSendParams")));
            this.customRadioButton13.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.customRadioButton13.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customRadioButton13.popupWindowSetting")));
            this.customRadioButton13.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customRadioButton13.RegistCheckMethod")));
            this.customRadioButton13.Size = new System.Drawing.Size(109, 17);
            this.customRadioButton13.TabIndex = 17;
            this.customRadioButton13.Tag = "発行日を印字しない場合にはチェックを付けてください";
            this.customRadioButton13.Text = "2.印刷しない";
            this.customRadioButton13.UseVisualStyleBackColor = true;
            this.customRadioButton13.Value = "2";
            // 
            // customRadioButton14
            // 
            this.customRadioButton14.AutoSize = true;
            this.customRadioButton14.DefaultBackColor = System.Drawing.Color.Empty;
            this.customRadioButton14.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customRadioButton14.FocusOutCheckMethod")));
            this.customRadioButton14.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.customRadioButton14.LinkedTextBox = "SEIKYU_HAKKOU";
            this.customRadioButton14.Location = new System.Drawing.Point(26, 0);
            this.customRadioButton14.Name = "customRadioButton14";
            this.customRadioButton14.PopupAfterExecute = null;
            this.customRadioButton14.PopupBeforeExecute = null;
            this.customRadioButton14.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customRadioButton14.PopupSearchSendParams")));
            this.customRadioButton14.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.customRadioButton14.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customRadioButton14.popupWindowSetting")));
            this.customRadioButton14.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customRadioButton14.RegistCheckMethod")));
            this.customRadioButton14.Size = new System.Drawing.Size(95, 17);
            this.customRadioButton14.TabIndex = 16;
            this.customRadioButton14.Tag = "発行日を印字する場合にはチェックを付けてください";
            this.customRadioButton14.Text = "1.印刷する";
            this.customRadioButton14.UseVisualStyleBackColor = true;
            this.customRadioButton14.Value = "1";
            // 
            // SEIKYU_HAKKOU
            // 
            this.SEIKYU_HAKKOU.BackColor = System.Drawing.SystemColors.Window;
            this.SEIKYU_HAKKOU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SEIKYU_HAKKOU.DefaultBackColor = System.Drawing.Color.Empty;
            this.SEIKYU_HAKKOU.DisplayPopUp = null;
            this.SEIKYU_HAKKOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEIKYU_HAKKOU.FocusOutCheckMethod")));
            this.SEIKYU_HAKKOU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SEIKYU_HAKKOU.ForeColor = System.Drawing.Color.Black;
            this.SEIKYU_HAKKOU.IsInputErrorOccured = false;
            this.SEIKYU_HAKKOU.LinkedRadioButtonArray = new string[] {
        "customRadioButton13",
        "customRadioButton14"};
            this.SEIKYU_HAKKOU.Location = new System.Drawing.Point(-1, -1);
            this.SEIKYU_HAKKOU.Name = "SEIKYU_HAKKOU";
            this.SEIKYU_HAKKOU.PopupAfterExecute = null;
            this.SEIKYU_HAKKOU.PopupBeforeExecute = null;
            this.SEIKYU_HAKKOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEIKYU_HAKKOU.PopupSearchSendParams")));
            this.SEIKYU_HAKKOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SEIKYU_HAKKOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEIKYU_HAKKOU.popupWindowSetting")));
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
            this.SEIKYU_HAKKOU.RangeSetting = rangeSettingDto1;
            this.SEIKYU_HAKKOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEIKYU_HAKKOU.RegistCheckMethod")));
            this.SEIKYU_HAKKOU.Size = new System.Drawing.Size(20, 20);
            this.SEIKYU_HAKKOU.TabIndex = 1;
            this.SEIKYU_HAKKOU.Tag = "【1、2】のいずれかで入力してください";
            this.SEIKYU_HAKKOU.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.SEIKYU_HAKKOU.WordWrap = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(363, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 507;
            this.label2.Text = "請求書発行日";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(748, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 511;
            this.label1.Text = "税率表示";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ZeiRate_Chk
            // 
            this.ZeiRate_Chk.AutoSize = true;
            this.ZeiRate_Chk.Location = new System.Drawing.Point(863, 5);
            this.ZeiRate_Chk.Margin = new System.Windows.Forms.Padding(2);
            this.ZeiRate_Chk.Name = "ZeiRate_Chk";
            this.ZeiRate_Chk.Size = new System.Drawing.Size(54, 17);
            this.ZeiRate_Chk.TabIndex = 510;
            this.ZeiRate_Chk.Tag = "適格請求書に税率を印刷する場合は、チェックをしてください";
            this.ZeiRate_Chk.Text = "表示";
            this.ZeiRate_Chk.UseVisualStyleBackColor = true;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1191, 46);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ZeiRate_Chk);
            this.Controls.Add(this.customPanel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.KYOTEN_CD);
            this.Controls.Add(this.KYOTEN_NM);
            this.Controls.Add(this.lbl_kyoten);
            this.Name = "UIHeader";
            this.Text = "HeaderSample";
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.lbl_kyoten, 0);
            this.Controls.SetChildIndex(this.KYOTEN_NM, 0);
            this.Controls.SetChildIndex(this.KYOTEN_CD, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.customPanel1, 0);
            this.Controls.SetChildIndex(this.ZeiRate_Chk, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lbl_kyoten;
        public r_framework.CustomControl.CustomTextBox KYOTEN_NM;
        public r_framework.CustomControl.CustomAlphaNumTextBox KYOTEN_CD;
        private r_framework.CustomControl.CustomPanel customPanel1;
        public r_framework.CustomControl.CustomRadioButton customRadioButton13;
        public r_framework.CustomControl.CustomRadioButton customRadioButton14;
        public r_framework.CustomControl.CustomNumericTextBox2 SEIKYU_HAKKOU;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox ZeiRate_Chk;

    }
}