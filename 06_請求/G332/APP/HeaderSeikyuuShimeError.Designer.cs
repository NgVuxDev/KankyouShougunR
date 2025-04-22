namespace Shougun.Core.Billing.SeikyuuShimeError.APP
{
    partial class HeaderSeikyuuShimeError
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HeaderSeikyuuShimeError));
            this.txtKyotenCd = new r_framework.CustomControl.CustomTextBox();
            this.txtKyotenName = new r_framework.CustomControl.CustomTextBox();
            this.lblKyoten = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Location = new System.Drawing.Point(-1, 7);
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(0, 6);
            this.lb_title.Size = new System.Drawing.Size(421, 34);
            this.lb_title.Text = "請求締処理エラー一覧";
            // 
            // txtKyotenCd
            // 
            this.txtKyotenCd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtKyotenCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKyotenCd.CharactersNumber = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.txtKyotenCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtKyotenCd.DisplayPopUp = null;
            this.txtKyotenCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenCd.FocusOutCheckMethod")));
            this.txtKyotenCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtKyotenCd.ForeColor = System.Drawing.Color.Black;
            this.txtKyotenCd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtKyotenCd.IsInputErrorOccured = false;
            this.txtKyotenCd.Location = new System.Drawing.Point(800, 2);
            this.txtKyotenCd.MaxLength = 2;
            this.txtKyotenCd.Name = "txtKyotenCd";
            this.txtKyotenCd.PopupAfterExecute = null;
            this.txtKyotenCd.PopupBeforeExecute = null;
            this.txtKyotenCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtKyotenCd.PopupSearchSendParams")));
            this.txtKyotenCd.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtKyotenCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtKyotenCd.popupWindowSetting")));
            this.txtKyotenCd.prevText = null;
            this.txtKyotenCd.ReadOnly = true;
            this.txtKyotenCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenCd.RegistCheckMethod")));
            this.txtKyotenCd.Size = new System.Drawing.Size(30, 20);
            this.txtKyotenCd.TabIndex = 419;
            this.txtKyotenCd.TabStop = false;
            this.txtKyotenCd.Tag = "";
            this.txtKyotenCd.ZeroPaddengFlag = true;
            // 
            // txtKyotenName
            // 
            this.txtKyotenName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtKyotenName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKyotenName.CharactersNumber = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.txtKyotenName.DBFieldsName = "";
            this.txtKyotenName.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtKyotenName.DisplayItemName = "検索条件";
            this.txtKyotenName.DisplayPopUp = null;
            this.txtKyotenName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenName.FocusOutCheckMethod")));
            this.txtKyotenName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtKyotenName.ForeColor = System.Drawing.Color.Black;
            this.txtKyotenName.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtKyotenName.IsInputErrorOccured = false;
            this.txtKyotenName.Location = new System.Drawing.Point(829, 2);
            this.txtKyotenName.Name = "txtKyotenName";
            this.txtKyotenName.PopupAfterExecute = null;
            this.txtKyotenName.PopupBeforeExecute = null;
            this.txtKyotenName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtKyotenName.PopupSearchSendParams")));
            this.txtKyotenName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtKyotenName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtKyotenName.popupWindowSetting")));
            this.txtKyotenName.prevText = null;
            this.txtKyotenName.ReadOnly = true;
            this.txtKyotenName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenName.RegistCheckMethod")));
            this.txtKyotenName.Size = new System.Drawing.Size(160, 20);
            this.txtKyotenName.TabIndex = 413;
            this.txtKyotenName.TabStop = false;
            this.txtKyotenName.Tag = "";
            // 
            // lblKyoten
            // 
            this.lblKyoten.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblKyoten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKyoten.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblKyoten.ForeColor = System.Drawing.Color.White;
            this.lblKyoten.Location = new System.Drawing.Point(685, 2);
            this.lblKyoten.Name = "lblKyoten";
            this.lblKyoten.Size = new System.Drawing.Size(110, 20);
            this.lblKyoten.TabIndex = 412;
            this.lblKyoten.Text = "拠点";
            this.lblKyoten.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HeaderSeikyuuShimeError
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 46);
            this.Controls.Add(this.txtKyotenCd);
            this.Controls.Add(this.txtKyotenName);
            this.Controls.Add(this.lblKyoten);
            this.Name = "HeaderSeikyuuShimeError";
            this.Text = "HeaderSeikyuushoHakkou";
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.lblKyoten, 0);
            this.Controls.SetChildIndex(this.txtKyotenName, 0);
            this.Controls.SetChildIndex(this.txtKyotenCd, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomTextBox txtKyotenCd;
        internal r_framework.CustomControl.CustomTextBox txtKyotenName;
        private System.Windows.Forms.Label lblKyoten;

    }
}