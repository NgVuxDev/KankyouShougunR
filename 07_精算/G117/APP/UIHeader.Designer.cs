namespace Shougun.Core.Adjustment.Shiharaicheckhyo
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
            this.lblUserKyoten = new System.Windows.Forms.Label();
            this.txtUserKyotenName = new r_framework.CustomControl.CustomTextBox();
            this.txtUserKyotenCD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Location = new System.Drawing.Point(8, 8);
            // 
            // lb_title
            // 
            this.lb_title.Size = new System.Drawing.Size(333, 34);
            this.lb_title.Text = "支払チェック表";
            // 
            // lblUserKyoten
            // 
            this.lblUserKyoten.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblUserKyoten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUserKyoten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblUserKyoten.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblUserKyoten.ForeColor = System.Drawing.Color.White;
            this.lblUserKyoten.Location = new System.Drawing.Point(864, 2);
            this.lblUserKyoten.Name = "lblUserKyoten";
            this.lblUserKyoten.Size = new System.Drawing.Size(110, 20);
            this.lblUserKyoten.TabIndex = 499;
            this.lblUserKyoten.Text = "拠点";
            this.lblUserKyoten.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblUserKyoten.Visible = false;
            // 
            // txtUserKyotenName
            // 
            this.txtUserKyotenName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtUserKyotenName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserKyotenName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txtUserKyotenName.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtUserKyotenName.DisplayPopUp = null;
            this.txtUserKyotenName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtUserKyotenName.FocusOutCheckMethod")));
            this.txtUserKyotenName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtUserKyotenName.ForeColor = System.Drawing.Color.Black;
            this.txtUserKyotenName.IsInputErrorOccured = false;
            this.txtUserKyotenName.Location = new System.Drawing.Point(1008, 2);
            this.txtUserKyotenName.MaxLength = 0;
            this.txtUserKyotenName.Name = "txtUserKyotenName";
            this.txtUserKyotenName.PopupAfterExecute = null;
            this.txtUserKyotenName.PopupBeforeExecute = null;
            this.txtUserKyotenName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtUserKyotenName.PopupSearchSendParams")));
            this.txtUserKyotenName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtUserKyotenName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtUserKyotenName.popupWindowSetting")));
            this.txtUserKyotenName.prevText = null;
            this.txtUserKyotenName.ReadOnly = true;
            this.txtUserKyotenName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtUserKyotenName.RegistCheckMethod")));
            this.txtUserKyotenName.Size = new System.Drawing.Size(160, 20);
            this.txtUserKyotenName.TabIndex = 501;
            this.txtUserKyotenName.TabStop = false;
            this.txtUserKyotenName.Tag = " は 0 文字以内で入力してください。";
            this.txtUserKyotenName.Visible = false;
            // 
            // txtUserKyotenCD
            // 
            this.txtUserKyotenCD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtUserKyotenCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserKyotenCD.CharacterLimitList = null;
            this.txtUserKyotenCD.CharactersNumber = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.txtUserKyotenCD.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtUserKyotenCD.DisplayPopUp = null;
            this.txtUserKyotenCD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtUserKyotenCD.FocusOutCheckMethod")));
            this.txtUserKyotenCD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtUserKyotenCD.ForeColor = System.Drawing.Color.Black;
            this.txtUserKyotenCD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtUserKyotenCD.IsInputErrorOccured = false;
            this.txtUserKyotenCD.Location = new System.Drawing.Point(979, 2);
            this.txtUserKyotenCD.Name = "txtUserKyotenCD";
            this.txtUserKyotenCD.PopupAfterExecute = null;
            this.txtUserKyotenCD.PopupBeforeExecute = null;
            this.txtUserKyotenCD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtUserKyotenCD.PopupSearchSendParams")));
            this.txtUserKyotenCD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtUserKyotenCD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtUserKyotenCD.popupWindowSetting")));
            this.txtUserKyotenCD.prevText = null;
            this.txtUserKyotenCD.ReadOnly = true;
            this.txtUserKyotenCD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtUserKyotenCD.RegistCheckMethod")));
            this.txtUserKyotenCD.Size = new System.Drawing.Size(30, 20);
            this.txtUserKyotenCD.TabIndex = 505;
            this.txtUserKyotenCD.TabStop = false;
            this.txtUserKyotenCD.Tag = "";
            this.txtUserKyotenCD.Visible = false;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.txtUserKyotenCD);
            this.Controls.Add(this.txtUserKyotenName);
            this.Controls.Add(this.lblUserKyoten);
            this.Name = "UIHeader";
            this.Text = "HeaderSample";
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.lblUserKyoten, 0);
            this.Controls.SetChildIndex(this.txtUserKyotenName, 0);
            this.Controls.SetChildIndex(this.txtUserKyotenCD, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblUserKyoten;
        public r_framework.CustomControl.CustomTextBox txtUserKyotenName;
        public r_framework.CustomControl.CustomAlphaNumTextBox txtUserKyotenCD;

    }
}