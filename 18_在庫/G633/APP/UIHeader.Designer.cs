namespace Shougun.Core.Stock.ZaikoHinmeiHuriwake
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
            this.lblHinmeiName = new System.Windows.Forms.Label();
            this.txtHinmeiName = new r_framework.CustomControl.CustomTextBox();
            this.txtHinmeiCd = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.lblHinmeiCd = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(0, 6);
            this.lb_title.Size = new System.Drawing.Size(320, 34);
            this.lb_title.Text = "在庫移動入力";
            // 
            // lblHinmeiName
            // 
            this.lblHinmeiName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblHinmeiName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHinmeiName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblHinmeiName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblHinmeiName.ForeColor = System.Drawing.Color.White;
            this.lblHinmeiName.Location = new System.Drawing.Point(431, 4);
            this.lblHinmeiName.Name = "lblHinmeiName";
            this.lblHinmeiName.Size = new System.Drawing.Size(287, 19);
            this.lblHinmeiName.TabIndex = 404;
            this.lblHinmeiName.Text = "品名";
            this.lblHinmeiName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtHinmeiName
            // 
            this.txtHinmeiName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtHinmeiName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHinmeiName.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtHinmeiName.DisplayPopUp = null;
            this.txtHinmeiName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtHinmeiName.FocusOutCheckMethod")));
            this.txtHinmeiName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtHinmeiName.ForeColor = System.Drawing.Color.Black;
            this.txtHinmeiName.IsInputErrorOccured = false;
            this.txtHinmeiName.Location = new System.Drawing.Point(431, 23);
            this.txtHinmeiName.Name = "txtHinmeiName";
            this.txtHinmeiName.PopupAfterExecute = null;
            this.txtHinmeiName.PopupBeforeExecute = null;
            this.txtHinmeiName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtHinmeiName.PopupSearchSendParams")));
            this.txtHinmeiName.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
            this.txtHinmeiName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtHinmeiName.popupWindowSetting")));
            this.txtHinmeiName.prevText = null;
            this.txtHinmeiName.ReadOnly = true;
            this.txtHinmeiName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtHinmeiName.RegistCheckMethod")));
            this.txtHinmeiName.Size = new System.Drawing.Size(287, 20);
            this.txtHinmeiName.TabIndex = 406;
            this.txtHinmeiName.TabStop = false;
            // 
            // txtHinmeiCd
            // 
            this.txtHinmeiCd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtHinmeiCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHinmeiCd.CharacterLimitList = null;
            this.txtHinmeiCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtHinmeiCd.DisplayPopUp = null;
            this.txtHinmeiCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtHinmeiCd.FocusOutCheckMethod")));
            this.txtHinmeiCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtHinmeiCd.ForeColor = System.Drawing.Color.Black;
            this.txtHinmeiCd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtHinmeiCd.IsInputErrorOccured = false;
            this.txtHinmeiCd.Location = new System.Drawing.Point(355, 23);
            this.txtHinmeiCd.Name = "txtHinmeiCd";
            this.txtHinmeiCd.PopupAfterExecute = null;
            this.txtHinmeiCd.PopupBeforeExecute = null;
            this.txtHinmeiCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtHinmeiCd.PopupSearchSendParams")));
            this.txtHinmeiCd.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
            this.txtHinmeiCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtHinmeiCd.popupWindowSetting")));
            this.txtHinmeiCd.prevText = null;
            this.txtHinmeiCd.ReadOnly = true;
            this.txtHinmeiCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtHinmeiCd.RegistCheckMethod")));
            this.txtHinmeiCd.Size = new System.Drawing.Size(77, 20);
            this.txtHinmeiCd.TabIndex = 405;
            this.txtHinmeiCd.TabStop = false;
            // 
            // lblHinmeiCd
            // 
            this.lblHinmeiCd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblHinmeiCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHinmeiCd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblHinmeiCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblHinmeiCd.ForeColor = System.Drawing.Color.White;
            this.lblHinmeiCd.Location = new System.Drawing.Point(355, 4);
            this.lblHinmeiCd.Name = "lblHinmeiCd";
            this.lblHinmeiCd.Size = new System.Drawing.Size(77, 19);
            this.lblHinmeiCd.TabIndex = 403;
            this.lblHinmeiCd.Text = "品名CD";
            this.lblHinmeiCd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.lblHinmeiName);
            this.Controls.Add(this.txtHinmeiName);
            this.Controls.Add(this.txtHinmeiCd);
            this.Controls.Add(this.lblHinmeiCd);
            this.Name = "UIHeader";
            this.Text = "HeaderSample";
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.lblHinmeiCd, 0);
            this.Controls.SetChildIndex(this.txtHinmeiCd, 0);
            this.Controls.SetChildIndex(this.txtHinmeiName, 0);
            this.Controls.SetChildIndex(this.lblHinmeiName, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblHinmeiName;
        public r_framework.CustomControl.CustomTextBox txtHinmeiName;
        public r_framework.CustomControl.CustomAlphaNumTextBox txtHinmeiCd;
        public System.Windows.Forms.Label lblHinmeiCd;




    }
}