using r_framework.CustomControl;

namespace r_framework.APP.Base
{
    partial class DetailedHeaderForm
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
            this.label35 = new System.Windows.Forms.Label();
            this.LastUpdateDate = new r_framework.CustomControl.CustomTextBox();
            this.LastUpdateUser = new r_framework.CustomControl.CustomTextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.CreateDate = new r_framework.CustomControl.CustomTextBox();
            this.CreateUser = new r_framework.CustomControl.CustomTextBox();
            this.lb_title = new System.Windows.Forms.Label();
            this.windowTypeLabel = new r_framework.CustomControl.CustomWindowTypeLabel();
            this.SuspendLayout();
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label35.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label35.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label35.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label35.ForeColor = System.Drawing.Color.White;
            this.label35.Location = new System.Drawing.Point(734, 2);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(110, 20);
            this.label35.TabIndex = 384;
            this.label35.Text = "最終更新";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LastUpdateDate
            // 
            this.LastUpdateDate.AutoChangeBackColorEnabled = true;
            this.LastUpdateDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.LastUpdateDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LastUpdateDate.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.LastUpdateDate.DefaultBackColor = System.Drawing.Color.Empty;
            this.LastUpdateDate.DisplayPopUp = null;
            this.LastUpdateDate.ErrorMessage = "";
            this.LastUpdateDate.FocusOutCheckMethod = null;
            this.LastUpdateDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.LastUpdateDate.GetCodeMasterField = "";
            this.LastUpdateDate.IsInputErrorOccured = false;
            this.LastUpdateDate.Location = new System.Drawing.Point(1008, 2);
            this.LastUpdateDate.MaxLength = 0;
            this.LastUpdateDate.Name = "LastUpdateDate";
            this.LastUpdateDate.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.LastUpdateDate.popupWindowSetting = null;
            this.LastUpdateDate.ReadOnly = true;
            this.LastUpdateDate.RegistCheckMethod = null;
            this.LastUpdateDate.SetFormField = "";
            this.LastUpdateDate.Size = new System.Drawing.Size(160, 20);
            this.LastUpdateDate.TabIndex = 383;
            this.LastUpdateDate.TabStop = false;
            this.LastUpdateDate.Tag = "最終更新日が表示されます。";
            this.LastUpdateDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LastUpdateUser
            // 
            this.LastUpdateUser.AutoChangeBackColorEnabled = true;
            this.LastUpdateUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.LastUpdateUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LastUpdateUser.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.LastUpdateUser.DefaultBackColor = System.Drawing.Color.Empty;
            this.LastUpdateUser.DisplayPopUp = null;
            this.LastUpdateUser.ErrorMessage = "";
            this.LastUpdateUser.FocusOutCheckMethod = null;
            this.LastUpdateUser.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.LastUpdateUser.GetCodeMasterField = "";
            this.LastUpdateUser.IsInputErrorOccured = false;
            this.LastUpdateUser.Location = new System.Drawing.Point(849, 2);
            this.LastUpdateUser.MaxLength = 0;
            this.LastUpdateUser.Name = "LastUpdateUser";
            this.LastUpdateUser.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.LastUpdateUser.popupWindowSetting = null;
            this.LastUpdateUser.ReadOnly = true;
            this.LastUpdateUser.RegistCheckMethod = null;
            this.LastUpdateUser.SetFormField = "";
            this.LastUpdateUser.Size = new System.Drawing.Size(160, 20);
            this.LastUpdateUser.TabIndex = 382;
            this.LastUpdateUser.TabStop = false;
            this.LastUpdateUser.Tag = "最終更新者が表示されます。";
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label36.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label36.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label36.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label36.ForeColor = System.Drawing.Color.White;
            this.label36.Location = new System.Drawing.Point(734, 24);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(110, 20);
            this.label36.TabIndex = 381;
            this.label36.Text = "初回登録";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CreateDate
            // 
            this.CreateDate.AutoChangeBackColorEnabled = true;
            this.CreateDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CreateDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CreateDate.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CreateDate.DefaultBackColor = System.Drawing.Color.Empty;
            this.CreateDate.DisplayPopUp = null;
            this.CreateDate.ErrorMessage = "";
            this.CreateDate.FocusOutCheckMethod = null;
            this.CreateDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CreateDate.GetCodeMasterField = "";
            this.CreateDate.IsInputErrorOccured = false;
            this.CreateDate.Location = new System.Drawing.Point(1008, 24);
            this.CreateDate.MaxLength = 0;
            this.CreateDate.Name = "CreateDate";
            this.CreateDate.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CreateDate.popupWindowSetting = null;
            this.CreateDate.ReadOnly = true;
            this.CreateDate.RegistCheckMethod = null;
            this.CreateDate.SetFormField = "";
            this.CreateDate.Size = new System.Drawing.Size(160, 20);
            this.CreateDate.TabIndex = 380;
            this.CreateDate.TabStop = false;
            this.CreateDate.Tag = "初回登録日が表示されます。";
            this.CreateDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CreateUser
            // 
            this.CreateUser.AutoChangeBackColorEnabled = true;
            this.CreateUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CreateUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CreateUser.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CreateUser.DefaultBackColor = System.Drawing.Color.Empty;
            this.CreateUser.DisplayPopUp = null;
            this.CreateUser.ErrorMessage = "";
            this.CreateUser.FocusOutCheckMethod = null;
            this.CreateUser.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CreateUser.GetCodeMasterField = "";
            this.CreateUser.IsInputErrorOccured = false;
            this.CreateUser.Location = new System.Drawing.Point(849, 24);
            this.CreateUser.MaxLength = 0;
            this.CreateUser.Name = "CreateUser";
            this.CreateUser.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CreateUser.popupWindowSetting = null;
            this.CreateUser.ReadOnly = true;
            this.CreateUser.RegistCheckMethod = null;
            this.CreateUser.SetFormField = "";
            this.CreateUser.Size = new System.Drawing.Size(160, 20);
            this.CreateUser.TabIndex = 379;
            this.CreateUser.TabStop = false;
            this.CreateUser.Tag = "初回登録者が表示されます。";
            // 
            // lb_title
            // 
            this.lb_title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lb_title.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_title.Font = new System.Drawing.Font("ＭＳ ゴシック", 20F, System.Drawing.FontStyle.Bold);
            this.lb_title.ForeColor = System.Drawing.Color.White;
            this.lb_title.Location = new System.Drawing.Point(82, 6);
            this.lb_title.Name = "lb_title";
            this.lb_title.Size = new System.Drawing.Size(460, 34);
            this.lb_title.TabIndex = 378;
            this.lb_title.Text = "○○○○○タイトル";
            this.lb_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_title.TextChanged += new System.EventHandler(this.lb_title_TextChanged);
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.BackColor = System.Drawing.Color.Cornsilk;
            this.windowTypeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.windowTypeLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.windowTypeLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 18F, System.Drawing.FontStyle.Bold);
            this.windowTypeLabel.ForeColor = System.Drawing.Color.Black;
            this.windowTypeLabel.Location = new System.Drawing.Point(0, 8);
            this.windowTypeLabel.Name = "windowTypeLabel";
            this.windowTypeLabel.Size = new System.Drawing.Size(70, 30);
            this.windowTypeLabel.TabIndex = 385;
            this.windowTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.windowTypeLabel.WindowType = r_framework.Const.WINDOW_TYPE.NONE;
            // 
            // DetailedHeaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.windowTypeLabel);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.LastUpdateDate);
            this.Controls.Add(this.LastUpdateUser);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.CreateDate);
            this.Controls.Add(this.CreateUser);
            this.Controls.Add(this.lb_title);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "DetailedHeaderForm";
            this.Text = "BaseForm01";
            this.Load += new System.EventHandler(this.DetailedHeaderForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label35;
        public CustomTextBox LastUpdateDate;
        public CustomTextBox LastUpdateUser;
        public System.Windows.Forms.Label label36;
        public CustomTextBox CreateDate;
        public CustomTextBox CreateUser;
        public System.Windows.Forms.Label lb_title;
        public CustomWindowTypeLabel windowTypeLabel;

    }
}