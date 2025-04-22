namespace Shougun.Core.ElectronicManifest.DenshiManifestNyuryoku
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
            this.pnl_ModifyInfo = new r_framework.CustomControl.CustomPanel();
            this.ctxt_LastModifyDate = new r_framework.CustomControl.CustomTextBox();
            this.ctxt_FirstRegistDate = new r_framework.CustomControl.CustomTextBox();
            this.ctxt_Lastctxt_LastModifySha = new r_framework.CustomControl.CustomTextBox();
            this.lbl_LastUpdateSha = new System.Windows.Forms.Label();
            this.ctxt_FirstRegistSha = new r_framework.CustomControl.CustomTextBox();
            this.lbl_FirstRegistSha = new System.Windows.Forms.Label();
            this.pnl_ModifyInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_ModifyInfo
            // 
            this.pnl_ModifyInfo.Controls.Add(this.ctxt_LastModifyDate);
            this.pnl_ModifyInfo.Controls.Add(this.ctxt_FirstRegistDate);
            this.pnl_ModifyInfo.Controls.Add(this.ctxt_Lastctxt_LastModifySha);
            this.pnl_ModifyInfo.Controls.Add(this.lbl_LastUpdateSha);
            this.pnl_ModifyInfo.Controls.Add(this.ctxt_FirstRegistSha);
            this.pnl_ModifyInfo.Controls.Add(this.lbl_FirstRegistSha);
            this.pnl_ModifyInfo.Location = new System.Drawing.Point(762, 0);
            this.pnl_ModifyInfo.Name = "pnl_ModifyInfo";
            this.pnl_ModifyInfo.Size = new System.Drawing.Size(353, 46);
            this.pnl_ModifyInfo.TabIndex = 389;
            // 
            // ctxt_LastModifyDate
            // 
            this.ctxt_LastModifyDate.AutoChangeBackColorEnabled = true;
            this.ctxt_LastModifyDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_LastModifyDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_LastModifyDate.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_LastModifyDate.DisplayPopUp = null;
            this.ctxt_LastModifyDate.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_LastModifyDate.FocusOutCheckMethod")));
            this.ctxt_LastModifyDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ctxt_LastModifyDate.ImeMode = System.Windows.Forms.ImeMode.On;
            this.ctxt_LastModifyDate.IsInputErrorOccured = false;
            this.ctxt_LastModifyDate.Location = new System.Drawing.Point(206, 24);
            this.ctxt_LastModifyDate.MaxLength = 12;
            this.ctxt_LastModifyDate.Name = "ctxt_LastModifyDate";
            this.ctxt_LastModifyDate.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_LastModifyDate.PopupSearchSendParams")));
            this.ctxt_LastModifyDate.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
            this.ctxt_LastModifyDate.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_LastModifyDate.popupWindowSetting")));
            this.ctxt_LastModifyDate.ReadOnly = true;
            this.ctxt_LastModifyDate.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_LastModifyDate.RegistCheckMethod")));
            this.ctxt_LastModifyDate.Size = new System.Drawing.Size(145, 20);
            this.ctxt_LastModifyDate.TabIndex = 29;
            this.ctxt_LastModifyDate.Tag = "最終更新日が表示されます";
            // 
            // ctxt_FirstRegistDate
            // 
            this.ctxt_FirstRegistDate.AutoChangeBackColorEnabled = true;
            this.ctxt_FirstRegistDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_FirstRegistDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_FirstRegistDate.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_FirstRegistDate.DisplayPopUp = null;
            this.ctxt_FirstRegistDate.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_FirstRegistDate.FocusOutCheckMethod")));
            this.ctxt_FirstRegistDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ctxt_FirstRegistDate.ImeMode = System.Windows.Forms.ImeMode.On;
            this.ctxt_FirstRegistDate.IsInputErrorOccured = false;
            this.ctxt_FirstRegistDate.Location = new System.Drawing.Point(206, 2);
            this.ctxt_FirstRegistDate.MaxLength = 12;
            this.ctxt_FirstRegistDate.Name = "ctxt_FirstRegistDate";
            this.ctxt_FirstRegistDate.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_FirstRegistDate.PopupSearchSendParams")));
            this.ctxt_FirstRegistDate.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
            this.ctxt_FirstRegistDate.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_FirstRegistDate.popupWindowSetting")));
            this.ctxt_FirstRegistDate.ReadOnly = true;
            this.ctxt_FirstRegistDate.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_FirstRegistDate.RegistCheckMethod")));
            this.ctxt_FirstRegistDate.Size = new System.Drawing.Size(145, 20);
            this.ctxt_FirstRegistDate.TabIndex = 28;
            this.ctxt_FirstRegistDate.Tag = "初回登録日が表示されます";
            // 
            // ctxt_Lastctxt_LastModifySha
            // 
            this.ctxt_Lastctxt_LastModifySha.AutoChangeBackColorEnabled = true;
            this.ctxt_Lastctxt_LastModifySha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_Lastctxt_LastModifySha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_Lastctxt_LastModifySha.CharactersNumber = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.ctxt_Lastctxt_LastModifySha.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_Lastctxt_LastModifySha.DisplayPopUp = null;
            this.ctxt_Lastctxt_LastModifySha.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_Lastctxt_LastModifySha.FocusOutCheckMethod")));
            this.ctxt_Lastctxt_LastModifySha.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ctxt_Lastctxt_LastModifySha.ImeMode = System.Windows.Forms.ImeMode.On;
            this.ctxt_Lastctxt_LastModifySha.IsInputErrorOccured = false;
            this.ctxt_Lastctxt_LastModifySha.Location = new System.Drawing.Point(91, 24);
            this.ctxt_Lastctxt_LastModifySha.MaxLength = 12;
            this.ctxt_Lastctxt_LastModifySha.Name = "ctxt_Lastctxt_LastModifySha";
            this.ctxt_Lastctxt_LastModifySha.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_Lastctxt_LastModifySha.PopupSearchSendParams")));
            this.ctxt_Lastctxt_LastModifySha.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
            this.ctxt_Lastctxt_LastModifySha.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_Lastctxt_LastModifySha.popupWindowSetting")));
            this.ctxt_Lastctxt_LastModifySha.ReadOnly = true;
            this.ctxt_Lastctxt_LastModifySha.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_Lastctxt_LastModifySha.RegistCheckMethod")));
            this.ctxt_Lastctxt_LastModifySha.Size = new System.Drawing.Size(116, 20);
            this.ctxt_Lastctxt_LastModifySha.TabIndex = 26;
            this.ctxt_Lastctxt_LastModifySha.Tag = "最終更新者が表示されます";
            // 
            // lbl_LastUpdateSha
            // 
            this.lbl_LastUpdateSha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_LastUpdateSha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_LastUpdateSha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_LastUpdateSha.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_LastUpdateSha.ForeColor = System.Drawing.Color.White;
            this.lbl_LastUpdateSha.Location = new System.Drawing.Point(-2, 24);
            this.lbl_LastUpdateSha.Name = "lbl_LastUpdateSha";
            this.lbl_LastUpdateSha.Size = new System.Drawing.Size(88, 20);
            this.lbl_LastUpdateSha.TabIndex = 27;
            this.lbl_LastUpdateSha.Text = "最終更新";
            this.lbl_LastUpdateSha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ctxt_FirstRegistSha
            // 
            this.ctxt_FirstRegistSha.AutoChangeBackColorEnabled = true;
            this.ctxt_FirstRegistSha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_FirstRegistSha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_FirstRegistSha.CharactersNumber = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.ctxt_FirstRegistSha.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_FirstRegistSha.DisplayPopUp = null;
            this.ctxt_FirstRegistSha.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_FirstRegistSha.FocusOutCheckMethod")));
            this.ctxt_FirstRegistSha.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ctxt_FirstRegistSha.ImeMode = System.Windows.Forms.ImeMode.On;
            this.ctxt_FirstRegistSha.IsInputErrorOccured = false;
            this.ctxt_FirstRegistSha.Location = new System.Drawing.Point(91, 2);
            this.ctxt_FirstRegistSha.MaxLength = 12;
            this.ctxt_FirstRegistSha.Name = "ctxt_FirstRegistSha";
            this.ctxt_FirstRegistSha.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_FirstRegistSha.PopupSearchSendParams")));
            this.ctxt_FirstRegistSha.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
            this.ctxt_FirstRegistSha.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_FirstRegistSha.popupWindowSetting")));
            this.ctxt_FirstRegistSha.ReadOnly = true;
            this.ctxt_FirstRegistSha.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_FirstRegistSha.RegistCheckMethod")));
            this.ctxt_FirstRegistSha.Size = new System.Drawing.Size(116, 20);
            this.ctxt_FirstRegistSha.TabIndex = 1;
            this.ctxt_FirstRegistSha.Tag = "初回登録者が表示されます";
            // 
            // lbl_FirstRegistSha
            // 
            this.lbl_FirstRegistSha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_FirstRegistSha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_FirstRegistSha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_FirstRegistSha.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_FirstRegistSha.ForeColor = System.Drawing.Color.White;
            this.lbl_FirstRegistSha.Location = new System.Drawing.Point(-2, 2);
            this.lbl_FirstRegistSha.Name = "lbl_FirstRegistSha";
            this.lbl_FirstRegistSha.Size = new System.Drawing.Size(88, 20);
            this.lbl_FirstRegistSha.TabIndex = 25;
            this.lbl_FirstRegistSha.Text = "初回登録";
            this.lbl_FirstRegistSha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.pnl_ModifyInfo);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Location = new System.Drawing.Point(12, 6);
            this.Name = "UIHeader";
            this.Text = "HeaderSample";
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.pnl_ModifyInfo, 0);
            this.pnl_ModifyInfo.ResumeLayout(false);
            this.pnl_ModifyInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal r_framework.CustomControl.CustomPanel pnl_ModifyInfo;
        internal r_framework.CustomControl.CustomTextBox ctxt_LastModifyDate;
        internal r_framework.CustomControl.CustomTextBox ctxt_FirstRegistDate;
        internal r_framework.CustomControl.CustomTextBox ctxt_Lastctxt_LastModifySha;
        internal System.Windows.Forms.Label lbl_LastUpdateSha;
        internal r_framework.CustomControl.CustomTextBox ctxt_FirstRegistSha;
        internal System.Windows.Forms.Label lbl_FirstRegistSha;


    }
}