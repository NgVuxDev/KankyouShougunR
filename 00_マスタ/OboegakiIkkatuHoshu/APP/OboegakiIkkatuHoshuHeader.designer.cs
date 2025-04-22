namespace OboegakiIkkatuHoshu.APP
{
    partial class OboegakiIkkatuHoshuHeader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OboegakiIkkatuHoshuHeader));
            this.LastUpdateDate = new r_framework.CustomControl.CustomTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.alertNumber = new r_framework.CustomControl.CustomTextBox();
            this.ReadDataNumber = new r_framework.CustomControl.CustomTextBox();
            this.CreateUser = new r_framework.CustomControl.CustomTextBox();
            this.LastUpdateUser = new r_framework.CustomControl.CustomTextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.maniFestShuRuiHoshuDetail1 = new ManiFestShuRuiHoshu.MultiRowTemplate.ManiFestShuRuiHoshuDetail();
            this.CreateDate = new r_framework.CustomControl.CustomTextBox();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.TabIndex = 0;
            // 
            // lb_title
            // 
            this.lb_title.Size = new System.Drawing.Size(192, 35);
            this.lb_title.TabIndex = 1;
            this.lb_title.Text = "覚書一括入力";
            // 
            // LastUpdateDate
            // 
            this.LastUpdateDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.LastUpdateDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LastUpdateDate.DefaultBackColor = System.Drawing.Color.Empty;
            this.LastUpdateDate.DisplayPopUp = null;
            this.LastUpdateDate.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LastUpdateDate.FocusOutCheckMethod")));
            this.LastUpdateDate.Location = new System.Drawing.Point(801, 22);
            this.LastUpdateDate.Name = "LastUpdateDate";
            this.LastUpdateDate.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("LastUpdateDate.PopupSearchSendParams")));
            this.LastUpdateDate.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.LastUpdateDate.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("LastUpdateDate.popupWindowSetting")));
            this.LastUpdateDate.ReadOnly = true;
            this.LastUpdateDate.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LastUpdateDate.RegistCheckMethod")));
            this.LastUpdateDate.Size = new System.Drawing.Size(160, 20);
            this.LastUpdateDate.TabIndex = 6;
            this.LastUpdateDate.Tag = "最終更新日が表示されます。";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(971, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "アラート件数";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(971, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "読込データ件数";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // alertNumber
            // 
            this.alertNumber.BackColor = System.Drawing.SystemColors.Window;
            this.alertNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.alertNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.alertNumber.DisplayPopUp = null;
            this.alertNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("alertNumber.FocusOutCheckMethod")));
            this.alertNumber.Location = new System.Drawing.Point(1085, 22);
            this.alertNumber.MaxLength = 5;
            this.alertNumber.Name = "alertNumber";
            this.alertNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("alertNumber.PopupSearchSendParams")));
            this.alertNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.alertNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("alertNumber.popupWindowSetting")));
            this.alertNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("alertNumber.RegistCheckMethod")));
            this.alertNumber.Size = new System.Drawing.Size(80, 20);
            this.alertNumber.TabIndex = 15;
            this.alertNumber.Tag = "検索結果の総件数でアラートメッセージを表示させたい上限数を入力してください。";
            this.alertNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.alertNumber.Validated += new System.EventHandler(this.alertNumber_Validated);
            // 
            // ReadDataNumber
            // 
            this.ReadDataNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ReadDataNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReadDataNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.ReadDataNumber.DisplayPopUp = null;
            this.ReadDataNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ReadDataNumber.FocusOutCheckMethod")));
            this.ReadDataNumber.Location = new System.Drawing.Point(1085, 3);
            this.ReadDataNumber.Name = "ReadDataNumber";
            this.ReadDataNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ReadDataNumber.PopupSearchSendParams")));
            this.ReadDataNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ReadDataNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ReadDataNumber.popupWindowSetting")));
            this.ReadDataNumber.ReadOnly = true;
            this.ReadDataNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ReadDataNumber.RegistCheckMethod")));
            this.ReadDataNumber.Size = new System.Drawing.Size(80, 20);
            this.ReadDataNumber.TabIndex = 13;
            this.ReadDataNumber.Tag = "検索結果の総件数が表示されます。";
            this.ReadDataNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CreateUser
            // 
            this.CreateUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CreateUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CreateUser.DefaultBackColor = System.Drawing.Color.Empty;
            this.CreateUser.DisplayPopUp = null;
            this.CreateUser.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CreateUser.FocusOutCheckMethod")));
            this.CreateUser.GetCodeMasterField = "";
            this.CreateUser.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CreateUser.ItemDefinedTypes = "";
            this.CreateUser.Location = new System.Drawing.Point(641, 2);
            this.CreateUser.Name = "CreateUser";
            this.CreateUser.PopupGetMasterField = "";
            this.CreateUser.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CreateUser.PopupSearchSendParams")));
            this.CreateUser.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CreateUser.PopupWindowName = "";
            this.CreateUser.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CreateUser.popupWindowSetting")));
            this.CreateUser.ReadOnly = true;
            this.CreateUser.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CreateUser.RegistCheckMethod")));
            this.CreateUser.SetFormField = "";
            this.CreateUser.ShortItemName = "";
            this.CreateUser.Size = new System.Drawing.Size(160, 20);
            this.CreateUser.TabIndex = 4;
            this.CreateUser.Tag = "初回登録者が表示されます。";
            // 
            // LastUpdateUser
            // 
            this.LastUpdateUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.LastUpdateUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LastUpdateUser.DefaultBackColor = System.Drawing.Color.Empty;
            this.LastUpdateUser.DisplayPopUp = null;
            this.LastUpdateUser.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LastUpdateUser.FocusOutCheckMethod")));
            this.LastUpdateUser.Location = new System.Drawing.Point(641, 22);
            this.LastUpdateUser.Name = "LastUpdateUser";
            this.LastUpdateUser.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("LastUpdateUser.PopupSearchSendParams")));
            this.LastUpdateUser.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.LastUpdateUser.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("LastUpdateUser.popupWindowSetting")));
            this.LastUpdateUser.ReadOnly = true;
            this.LastUpdateUser.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LastUpdateUser.RegistCheckMethod")));
            this.LastUpdateUser.Size = new System.Drawing.Size(160, 20);
            this.LastUpdateUser.TabIndex = 6;
            this.LastUpdateUser.Tag = "最終更新者が表示されます。";
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label35.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label35.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label35.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label35.ForeColor = System.Drawing.Color.White;
            this.label35.Location = new System.Drawing.Point(527, 23);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(110, 20);
            this.label35.TabIndex = 386;
            this.label35.Text = "最終更新";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label36.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label36.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label36.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label36.ForeColor = System.Drawing.Color.White;
            this.label36.Location = new System.Drawing.Point(527, 3);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(110, 20);
            this.label36.TabIndex = 385;
            this.label36.Text = "初回登録";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CreateDate
            // 
            this.CreateDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CreateDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CreateDate.DefaultBackColor = System.Drawing.Color.Empty;
            this.CreateDate.DisplayPopUp = null;
            this.CreateDate.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CreateDate.FocusOutCheckMethod")));
            this.CreateDate.Location = new System.Drawing.Point(801, 2);
            this.CreateDate.Name = "CreateDate";
            this.CreateDate.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CreateDate.PopupSearchSendParams")));
            this.CreateDate.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CreateDate.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CreateDate.popupWindowSetting")));
            this.CreateDate.ReadOnly = true;
            this.CreateDate.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CreateDate.RegistCheckMethod")));
            this.CreateDate.Size = new System.Drawing.Size(160, 20);
            this.CreateDate.TabIndex = 6;
            this.CreateDate.Tag = "初回登録日が表示されます。";
            // 
            // OboegakiIkkatuHoshuHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.alertNumber);
            this.Controls.Add(this.ReadDataNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CreateDate);
            this.Controls.Add(this.LastUpdateUser);
            this.Controls.Add(this.LastUpdateDate);
            this.Controls.Add(this.CreateUser);
            this.Name = "OboegakiIkkatuHoshuHeader";
            this.Text = "HeaderSample";
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.CreateUser, 0);
            this.Controls.SetChildIndex(this.LastUpdateDate, 0);
            this.Controls.SetChildIndex(this.LastUpdateUser, 0);
            this.Controls.SetChildIndex(this.CreateDate, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.ReadDataNumber, 0);
            this.Controls.SetChildIndex(this.alertNumber, 0);
            this.Controls.SetChildIndex(this.label36, 0);
            this.Controls.SetChildIndex(this.label35, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label label4;
        public r_framework.CustomControl.CustomTextBox alertNumber;
        public System.Windows.Forms.Label label5;
        public r_framework.CustomControl.CustomTextBox ReadDataNumber;
        public r_framework.CustomControl.CustomTextBox LastUpdateDate;
        public r_framework.CustomControl.CustomTextBox LastUpdateUser;
        public System.Windows.Forms.Label label35;
        public System.Windows.Forms.Label label36;
        private ManiFestShuRuiHoshu.MultiRowTemplate.ManiFestShuRuiHoshuDetail maniFestShuRuiHoshuDetail1;
        internal r_framework.CustomControl.CustomTextBox CreateUser;
        public r_framework.CustomControl.CustomTextBox CreateDate;

    }
}