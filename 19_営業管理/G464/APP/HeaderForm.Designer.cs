namespace Shougun.Core.BusinessManagement.JuchuuMokuhyouKensuuNyuuryoku
{
    partial class HeaderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HeaderForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.UPDATE_USER = new r_framework.CustomControl.CustomTextBox();
            this.CREATE_USER = new r_framework.CustomControl.CustomTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CREATE_DATE = new r_framework.CustomControl.CustomTextBox();
            this.UPDATE_DATE = new r_framework.CustomControl.CustomTextBox();
            this.SuspendLayout();
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(5, 3);
            this.lb_title.Size = new System.Drawing.Size(330, 34);
            // 
            // UPDATE_USER
            // 
            this.UPDATE_USER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.UPDATE_USER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UPDATE_USER.DefaultBackColor = System.Drawing.Color.Empty;
            this.UPDATE_USER.DisplayPopUp = null;
            this.UPDATE_USER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_USER.FocusOutCheckMethod")));
            this.UPDATE_USER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.UPDATE_USER.ForeColor = System.Drawing.Color.Black;
            this.UPDATE_USER.IsInputErrorOccured = false;
            this.UPDATE_USER.Location = new System.Drawing.Point(899, 23);
            this.UPDATE_USER.Name = "UPDATE_USER";
            this.UPDATE_USER.PopupAfterExecute = null;
            this.UPDATE_USER.PopupBeforeExecute = null;
            this.UPDATE_USER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPDATE_USER.PopupSearchSendParams")));
            this.UPDATE_USER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPDATE_USER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPDATE_USER.popupWindowSetting")));
            this.UPDATE_USER.prevText = null;
            this.UPDATE_USER.PrevText = null;
            this.UPDATE_USER.ReadOnly = true;
            this.UPDATE_USER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_USER.RegistCheckMethod")));
            this.UPDATE_USER.Size = new System.Drawing.Size(124, 20);
            this.UPDATE_USER.TabIndex = 406;
            this.UPDATE_USER.TabStop = false;
            this.UPDATE_USER.Tag = "最終更新者が表示されます";
            this.UPDATE_USER.Visible = false;
            // 
            // CREATE_USER
            // 
            this.CREATE_USER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CREATE_USER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CREATE_USER.DefaultBackColor = System.Drawing.Color.Empty;
            this.CREATE_USER.DisplayPopUp = null;
            this.CREATE_USER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_USER.FocusOutCheckMethod")));
            this.CREATE_USER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CREATE_USER.ForeColor = System.Drawing.Color.Black;
            this.CREATE_USER.IsInputErrorOccured = false;
            this.CREATE_USER.Location = new System.Drawing.Point(899, 1);
            this.CREATE_USER.Name = "CREATE_USER";
            this.CREATE_USER.PopupAfterExecute = null;
            this.CREATE_USER.PopupBeforeExecute = null;
            this.CREATE_USER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CREATE_USER.PopupSearchSendParams")));
            this.CREATE_USER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_USER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_USER.popupWindowSetting")));
            this.CREATE_USER.prevText = null;
            this.CREATE_USER.PrevText = null;
            this.CREATE_USER.ReadOnly = true;
            this.CREATE_USER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_USER.RegistCheckMethod")));
            this.CREATE_USER.Size = new System.Drawing.Size(124, 20);
            this.CREATE_USER.TabIndex = 404;
            this.CREATE_USER.TabStop = false;
            this.CREATE_USER.Tag = "初回登録者が表示されます";
            this.CREATE_USER.Visible = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(820, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 20);
            this.label2.TabIndex = 415;
            this.label2.Text = "初回登録";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(820, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 20);
            this.label3.TabIndex = 416;
            this.label3.Text = "最終更新";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Visible = false;
            // 
            // CREATE_DATE
            // 
            this.CREATE_DATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CREATE_DATE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CREATE_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            this.CREATE_DATE.DisplayPopUp = null;
            this.CREATE_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_DATE.FocusOutCheckMethod")));
            this.CREATE_DATE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CREATE_DATE.ForeColor = System.Drawing.Color.Black;
            this.CREATE_DATE.IsInputErrorOccured = false;
            this.CREATE_DATE.Location = new System.Drawing.Point(1022, 1);
            this.CREATE_DATE.Name = "CREATE_DATE";
            this.CREATE_DATE.PopupAfterExecute = null;
            this.CREATE_DATE.PopupBeforeExecute = null;
            this.CREATE_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CREATE_DATE.PopupSearchSendParams")));
            this.CREATE_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_DATE.popupWindowSetting")));
            this.CREATE_DATE.prevText = null;
            this.CREATE_DATE.PrevText = null;
            this.CREATE_DATE.ReadOnly = true;
            this.CREATE_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_DATE.RegistCheckMethod")));
            this.CREATE_DATE.Size = new System.Drawing.Size(145, 20);
            this.CREATE_DATE.TabIndex = 405;
            this.CREATE_DATE.TabStop = false;
            this.CREATE_DATE.Tag = "初回登録日が表示されます";
            this.CREATE_DATE.Visible = false;
            // 
            // UPDATE_DATE
            // 
            this.UPDATE_DATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.UPDATE_DATE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UPDATE_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            this.UPDATE_DATE.DisplayPopUp = null;
            this.UPDATE_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_DATE.FocusOutCheckMethod")));
            this.UPDATE_DATE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.UPDATE_DATE.ForeColor = System.Drawing.Color.Black;
            this.UPDATE_DATE.IsInputErrorOccured = false;
            this.UPDATE_DATE.Location = new System.Drawing.Point(1022, 23);
            this.UPDATE_DATE.Name = "UPDATE_DATE";
            this.UPDATE_DATE.PopupAfterExecute = null;
            this.UPDATE_DATE.PopupBeforeExecute = null;
            this.UPDATE_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPDATE_DATE.PopupSearchSendParams")));
            this.UPDATE_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPDATE_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPDATE_DATE.popupWindowSetting")));
            this.UPDATE_DATE.prevText = null;
            this.UPDATE_DATE.PrevText = null;
            this.UPDATE_DATE.ReadOnly = true;
            this.UPDATE_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_DATE.RegistCheckMethod")));
            this.UPDATE_DATE.Size = new System.Drawing.Size(145, 20);
            this.UPDATE_DATE.TabIndex = 407;
            this.UPDATE_DATE.TabStop = false;
            this.UPDATE_DATE.Tag = "最終更新日が表示されます";
            this.UPDATE_DATE.Visible = false;
            // 
            // HeaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.UPDATE_DATE);
            this.Controls.Add(this.CREATE_DATE);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.UPDATE_USER);
            this.Controls.Add(this.CREATE_USER);
            this.Name = "HeaderForm";
            this.Tag = "";
            this.Text = "HeaderForm";
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.CREATE_USER, 0);
            this.Controls.SetChildIndex(this.UPDATE_USER, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.CREATE_DATE, 0);
            this.Controls.SetChildIndex(this.UPDATE_DATE, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        internal r_framework.CustomControl.CustomTextBox UPDATE_USER;
        internal r_framework.CustomControl.CustomTextBox CREATE_USER;
        internal r_framework.CustomControl.CustomTextBox CREATE_DATE;
        internal r_framework.CustomControl.CustomTextBox UPDATE_DATE;
    }
}