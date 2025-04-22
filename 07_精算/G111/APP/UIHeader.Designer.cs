namespace Shougun.Core.Adjustment.Shiharaimeisaishokakunin
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
            this.lbl_kyoten = new System.Windows.Forms.Label();
            this.txtKyotenMei = new r_framework.CustomControl.CustomTextBox();
            this.txtKyotenCd = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ZeiRate_Chk = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Location = new System.Drawing.Point(8, 8);
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(84, 6);
            this.lb_title.Size = new System.Drawing.Size(333, 34);
            this.lb_title.Text = "支払明細書確認";
            // 
            // lbl_kyoten
            // 
            this.lbl_kyoten.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_kyoten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_kyoten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_kyoten.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_kyoten.ForeColor = System.Drawing.Color.White;
            this.lbl_kyoten.Location = new System.Drawing.Point(625, 0);
            this.lbl_kyoten.Name = "lbl_kyoten";
            this.lbl_kyoten.Size = new System.Drawing.Size(110, 20);
            this.lbl_kyoten.TabIndex = 499;
            this.lbl_kyoten.Text = "拠点";
            this.lbl_kyoten.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtKyotenMei
            // 
            this.txtKyotenMei.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtKyotenMei.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKyotenMei.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txtKyotenMei.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtKyotenMei.DisplayPopUp = null;
            this.txtKyotenMei.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenMei.FocusOutCheckMethod")));
            this.txtKyotenMei.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtKyotenMei.ForeColor = System.Drawing.Color.Black;
            this.txtKyotenMei.IsInputErrorOccured = false;
            this.txtKyotenMei.Location = new System.Drawing.Point(792, 0);
            this.txtKyotenMei.MaxLength = 0;
            this.txtKyotenMei.Name = "txtKyotenMei";
            this.txtKyotenMei.PopupAfterExecute = null;
            this.txtKyotenMei.PopupBeforeExecute = null;
            this.txtKyotenMei.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtKyotenMei.PopupSearchSendParams")));
            this.txtKyotenMei.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtKyotenMei.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtKyotenMei.popupWindowSetting")));
            this.txtKyotenMei.ReadOnly = true;
            this.txtKyotenMei.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenMei.RegistCheckMethod")));
            this.txtKyotenMei.Size = new System.Drawing.Size(212, 20);
            this.txtKyotenMei.TabIndex = 501;
            this.txtKyotenMei.TabStop = false;
            this.txtKyotenMei.Tag = " は 0 文字以内で入力してください。";
            // 
            // txtKyotenCd
            // 
            this.txtKyotenCd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtKyotenCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKyotenCd.CharacterLimitList = null;
            this.txtKyotenCd.DBFieldsName = "";
            this.txtKyotenCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtKyotenCd.DisplayItemName = "";
            this.txtKyotenCd.DisplayPopUp = null;
            this.txtKyotenCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenCd.FocusOutCheckMethod")));
            this.txtKyotenCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtKyotenCd.ForeColor = System.Drawing.Color.Black;
            this.txtKyotenCd.GetCodeMasterField = "";
            this.txtKyotenCd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtKyotenCd.IsInputErrorOccured = false;
            this.txtKyotenCd.ItemDefinedTypes = "";
            this.txtKyotenCd.Location = new System.Drawing.Point(740, 0);
            this.txtKyotenCd.MaxLength = 6;
            this.txtKyotenCd.Name = "txtKyotenCd";
            this.txtKyotenCd.PopupAfterExecute = null;
            this.txtKyotenCd.PopupBeforeExecute = null;
            this.txtKyotenCd.PopupGetMasterField = "";
            this.txtKyotenCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtKyotenCd.PopupSearchSendParams")));
            this.txtKyotenCd.PopupSetFormField = "";
            this.txtKyotenCd.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtKyotenCd.PopupWindowName = "";
            this.txtKyotenCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtKyotenCd.popupWindowSetting")));
            this.txtKyotenCd.ReadOnly = true;
            this.txtKyotenCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenCd.RegistCheckMethod")));
            this.txtKyotenCd.SetFormField = "";
            this.txtKyotenCd.ShortItemName = "";
            this.txtKyotenCd.Size = new System.Drawing.Size(53, 20);
            this.txtKyotenCd.TabIndex = 507;
            this.txtKyotenCd.TabStop = false;
            this.txtKyotenCd.Tag = "";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(625, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 513;
            this.label1.Text = "税率表示";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ZeiRate_Chk
            // 
            this.ZeiRate_Chk.AutoSize = true;
            this.ZeiRate_Chk.Location = new System.Drawing.Point(740, 24);
            this.ZeiRate_Chk.Margin = new System.Windows.Forms.Padding(2);
            this.ZeiRate_Chk.Name = "ZeiRate_Chk";
            this.ZeiRate_Chk.Size = new System.Drawing.Size(54, 17);
            this.ZeiRate_Chk.TabIndex = 512;
            this.ZeiRate_Chk.Tag = "適格請求書に税率を印刷する場合は、チェックをしてください";
            this.ZeiRate_Chk.Text = "表示";
            this.ZeiRate_Chk.UseVisualStyleBackColor = true;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 46);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ZeiRate_Chk);
            this.Controls.Add(this.txtKyotenCd);
            this.Controls.Add(this.txtKyotenMei);
            this.Controls.Add(this.lbl_kyoten);
            this.Name = "UIHeader";
            this.Text = "HeaderSample";
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.lbl_kyoten, 0);
            this.Controls.SetChildIndex(this.txtKyotenMei, 0);
            this.Controls.SetChildIndex(this.txtKyotenCd, 0);
            this.Controls.SetChildIndex(this.ZeiRate_Chk, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lbl_kyoten;
        public r_framework.CustomControl.CustomTextBox txtKyotenMei;
        public r_framework.CustomControl.CustomAlphaNumTextBox txtKyotenCd;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox ZeiRate_Chk;

    }
}