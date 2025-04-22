namespace Shougun.Core.Billing.Seikyucheckhyo
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
            this.txt_KyotenNameHeader = new r_framework.CustomControl.CustomTextBox();
            this.txt_KyotenCDHeader = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.SuspendLayout();
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(88, 6);
            this.lb_title.Size = new System.Drawing.Size(333, 34);
            this.lb_title.Text = "請求チェック表";
            // 
            // lbl_kyoten
            // 
            this.lbl_kyoten.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_kyoten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_kyoten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_kyoten.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_kyoten.ForeColor = System.Drawing.Color.White;
            this.lbl_kyoten.Location = new System.Drawing.Point(864, 2);
            this.lbl_kyoten.Name = "lbl_kyoten";
            this.lbl_kyoten.Size = new System.Drawing.Size(110, 20);
            this.lbl_kyoten.TabIndex = 499;
            this.lbl_kyoten.Text = "拠点";
            this.lbl_kyoten.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_kyoten.Visible = false;
            // 
            // txt_KyotenNameHeader
            // 
            this.txt_KyotenNameHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txt_KyotenNameHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_KyotenNameHeader.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txt_KyotenNameHeader.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_KyotenNameHeader.DisplayPopUp = null;
            this.txt_KyotenNameHeader.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_KyotenNameHeader.FocusOutCheckMethod")));
            this.txt_KyotenNameHeader.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_KyotenNameHeader.ForeColor = System.Drawing.Color.Black;
            this.txt_KyotenNameHeader.IsInputErrorOccured = false;
            this.txt_KyotenNameHeader.Location = new System.Drawing.Point(1008, 2);
            this.txt_KyotenNameHeader.MaxLength = 0;
            this.txt_KyotenNameHeader.Name = "txt_KyotenNameHeader";
            this.txt_KyotenNameHeader.PopupAfterExecute = null;
            this.txt_KyotenNameHeader.PopupBeforeExecute = null;
            this.txt_KyotenNameHeader.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_KyotenNameHeader.PopupSearchSendParams")));
            this.txt_KyotenNameHeader.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_KyotenNameHeader.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_KyotenNameHeader.popupWindowSetting")));
            this.txt_KyotenNameHeader.prevText = null;
            this.txt_KyotenNameHeader.ReadOnly = true;
            this.txt_KyotenNameHeader.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_KyotenNameHeader.RegistCheckMethod")));
            this.txt_KyotenNameHeader.Size = new System.Drawing.Size(160, 20);
            this.txt_KyotenNameHeader.TabIndex = 501;
            this.txt_KyotenNameHeader.TabStop = false;
            this.txt_KyotenNameHeader.Tag = "";
            this.txt_KyotenNameHeader.Visible = false;
            // 
            // txt_KyotenCDHeader
            // 
            this.txt_KyotenCDHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txt_KyotenCDHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_KyotenCDHeader.CharacterLimitList = null;
            this.txt_KyotenCDHeader.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_KyotenCDHeader.DisplayPopUp = null;
            this.txt_KyotenCDHeader.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_KyotenCDHeader.FocusOutCheckMethod")));
            this.txt_KyotenCDHeader.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_KyotenCDHeader.ForeColor = System.Drawing.Color.Black;
            this.txt_KyotenCDHeader.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txt_KyotenCDHeader.IsInputErrorOccured = false;
            this.txt_KyotenCDHeader.Location = new System.Drawing.Point(979, 2);
            this.txt_KyotenCDHeader.Name = "txt_KyotenCDHeader";
            this.txt_KyotenCDHeader.PopupAfterExecute = null;
            this.txt_KyotenCDHeader.PopupBeforeExecute = null;
            this.txt_KyotenCDHeader.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_KyotenCDHeader.PopupSearchSendParams")));
            this.txt_KyotenCDHeader.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_KyotenCDHeader.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_KyotenCDHeader.popupWindowSetting")));
            this.txt_KyotenCDHeader.prevText = null;
            this.txt_KyotenCDHeader.ReadOnly = true;
            this.txt_KyotenCDHeader.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_KyotenCDHeader.RegistCheckMethod")));
            this.txt_KyotenCDHeader.Size = new System.Drawing.Size(30, 20);
            this.txt_KyotenCDHeader.TabIndex = 505;
            this.txt_KyotenCDHeader.TabStop = false;
            this.txt_KyotenCDHeader.Tag = "";
            this.txt_KyotenCDHeader.Visible = false;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.txt_KyotenCDHeader);
            this.Controls.Add(this.txt_KyotenNameHeader);
            this.Controls.Add(this.lbl_kyoten);
            this.Name = "UIHeader";
            this.Text = "HeaderSample";
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.lbl_kyoten, 0);
            this.Controls.SetChildIndex(this.txt_KyotenNameHeader, 0);
            this.Controls.SetChildIndex(this.txt_KyotenCDHeader, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lbl_kyoten;
        public r_framework.CustomControl.CustomTextBox txt_KyotenNameHeader;
        public r_framework.CustomControl.CustomAlphaNumTextBox txt_KyotenCDHeader;

    }
}