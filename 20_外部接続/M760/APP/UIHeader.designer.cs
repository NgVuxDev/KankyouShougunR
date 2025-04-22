namespace Shougun.Core.ExternalConnection.DenshiBunshoHoshu
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
            this.CONTROL_NUMBER = new r_framework.CustomControl.CustomTextBox();
            this.DOCUMENT_ID = new r_framework.CustomControl.CustomTextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.REGISTERED_USER_NAME = new r_framework.CustomControl.CustomTextBox();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.TabIndex = 0;
            // 
            // lb_title
            // 
            this.lb_title.Size = new System.Drawing.Size(304, 35);
            this.lb_title.TabIndex = 1;
            this.lb_title.Text = "電子文書詳細入力";
            // 
            // CONTROL_NUMBER
            // 
            this.CONTROL_NUMBER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CONTROL_NUMBER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CONTROL_NUMBER.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONTROL_NUMBER.DisplayPopUp = null;
            this.CONTROL_NUMBER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTROL_NUMBER.FocusOutCheckMethod")));
            this.CONTROL_NUMBER.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CONTROL_NUMBER.ForeColor = System.Drawing.Color.Black;
            this.CONTROL_NUMBER.GetCodeMasterField = "";
            this.CONTROL_NUMBER.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CONTROL_NUMBER.IsInputErrorOccured = false;
            this.CONTROL_NUMBER.ItemDefinedTypes = "";
            this.CONTROL_NUMBER.Location = new System.Drawing.Point(518, 2);
            this.CONTROL_NUMBER.Name = "CONTROL_NUMBER";
            this.CONTROL_NUMBER.PopupAfterExecute = null;
            this.CONTROL_NUMBER.PopupBeforeExecute = null;
            this.CONTROL_NUMBER.PopupGetMasterField = "";
            this.CONTROL_NUMBER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONTROL_NUMBER.PopupSearchSendParams")));
            this.CONTROL_NUMBER.PopupSetFormField = "";
            this.CONTROL_NUMBER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONTROL_NUMBER.PopupWindowName = "";
            this.CONTROL_NUMBER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONTROL_NUMBER.popupWindowSetting")));
            this.CONTROL_NUMBER.ReadOnly = true;
            this.CONTROL_NUMBER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTROL_NUMBER.RegistCheckMethod")));
            this.CONTROL_NUMBER.SetFormField = "";
            this.CONTROL_NUMBER.ShortItemName = "";
            this.CONTROL_NUMBER.Size = new System.Drawing.Size(145, 20);
            this.CONTROL_NUMBER.TabIndex = 4;
            this.CONTROL_NUMBER.TabStop = false;
            this.CONTROL_NUMBER.Tag = "";
            // 
            // DOCUMENT_ID
            // 
            this.DOCUMENT_ID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.DOCUMENT_ID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DOCUMENT_ID.DefaultBackColor = System.Drawing.Color.Empty;
            this.DOCUMENT_ID.DisplayPopUp = null;
            this.DOCUMENT_ID.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DOCUMENT_ID.FocusOutCheckMethod")));
            this.DOCUMENT_ID.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DOCUMENT_ID.ForeColor = System.Drawing.Color.Black;
            this.DOCUMENT_ID.IsInputErrorOccured = false;
            this.DOCUMENT_ID.Location = new System.Drawing.Point(518, 24);
            this.DOCUMENT_ID.Name = "DOCUMENT_ID";
            this.DOCUMENT_ID.PopupAfterExecute = null;
            this.DOCUMENT_ID.PopupBeforeExecute = null;
            this.DOCUMENT_ID.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DOCUMENT_ID.PopupSearchSendParams")));
            this.DOCUMENT_ID.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DOCUMENT_ID.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DOCUMENT_ID.popupWindowSetting")));
            this.DOCUMENT_ID.ReadOnly = true;
            this.DOCUMENT_ID.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DOCUMENT_ID.RegistCheckMethod")));
            this.DOCUMENT_ID.Size = new System.Drawing.Size(145, 20);
            this.DOCUMENT_ID.TabIndex = 6;
            this.DOCUMENT_ID.TabStop = false;
            this.DOCUMENT_ID.Tag = "";
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(160)))));
            this.label35.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label35.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label35.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.label35.ForeColor = System.Drawing.Color.White;
            this.label35.Location = new System.Drawing.Point(404, 24);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(110, 20);
            this.label35.TabIndex = 386;
            this.label35.Text = "DocumentID";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(160)))));
            this.label36.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label36.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label36.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label36.ForeColor = System.Drawing.Color.White;
            this.label36.Location = new System.Drawing.Point(404, 2);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(110, 20);
            this.label36.TabIndex = 385;
            this.label36.Text = "関連コード";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(160)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(669, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 20);
            this.label1.TabIndex = 388;
            this.label1.Text = "電子契約登録者";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // REGISTERED_USER_NAME
            // 
            this.REGISTERED_USER_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.REGISTERED_USER_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.REGISTERED_USER_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.REGISTERED_USER_NAME.DisplayPopUp = null;
            this.REGISTERED_USER_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("REGISTERED_USER_NAME.FocusOutCheckMethod")));
            this.REGISTERED_USER_NAME.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.REGISTERED_USER_NAME.ForeColor = System.Drawing.Color.Black;
            this.REGISTERED_USER_NAME.GetCodeMasterField = "";
            this.REGISTERED_USER_NAME.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.REGISTERED_USER_NAME.IsInputErrorOccured = false;
            this.REGISTERED_USER_NAME.ItemDefinedTypes = "";
            this.REGISTERED_USER_NAME.Location = new System.Drawing.Point(813, 2);
            this.REGISTERED_USER_NAME.Name = "REGISTERED_USER_NAME";
            this.REGISTERED_USER_NAME.PopupAfterExecute = null;
            this.REGISTERED_USER_NAME.PopupBeforeExecute = null;
            this.REGISTERED_USER_NAME.PopupGetMasterField = "";
            this.REGISTERED_USER_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("REGISTERED_USER_NAME.PopupSearchSendParams")));
            this.REGISTERED_USER_NAME.PopupSetFormField = "";
            this.REGISTERED_USER_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.REGISTERED_USER_NAME.PopupWindowName = "";
            this.REGISTERED_USER_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("REGISTERED_USER_NAME.popupWindowSetting")));
            this.REGISTERED_USER_NAME.ReadOnly = true;
            this.REGISTERED_USER_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("REGISTERED_USER_NAME.RegistCheckMethod")));
            this.REGISTERED_USER_NAME.SetFormField = "";
            this.REGISTERED_USER_NAME.ShortItemName = "";
            this.REGISTERED_USER_NAME.Size = new System.Drawing.Size(215, 20);
            this.REGISTERED_USER_NAME.TabIndex = 387;
            this.REGISTERED_USER_NAME.TabStop = false;
            this.REGISTERED_USER_NAME.Tag = "";
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.REGISTERED_USER_NAME);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.DOCUMENT_ID);
            this.Controls.Add(this.CONTROL_NUMBER);
            this.Name = "UIHeader";
            this.Text = "電子文書詳細入力";
            this.Controls.SetChildIndex(this.CONTROL_NUMBER, 0);
            this.Controls.SetChildIndex(this.DOCUMENT_ID, 0);
            this.Controls.SetChildIndex(this.label36, 0);
            this.Controls.SetChildIndex(this.label35, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.REGISTERED_USER_NAME, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomTextBox DOCUMENT_ID;
        public System.Windows.Forms.Label label35;
        public System.Windows.Forms.Label label36;
        public r_framework.CustomControl.CustomTextBox CONTROL_NUMBER;
        public System.Windows.Forms.Label label1;
        public r_framework.CustomControl.CustomTextBox REGISTERED_USER_NAME;
    }
}