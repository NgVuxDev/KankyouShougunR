namespace Shougun.Core.BusinessManagement.MitsumoriNyuryoku
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
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.KYOTEN_NAME_RYAKU = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.LastUpdateDate = new r_framework.CustomControl.CustomTextBox();
            this.LastUpdateUser = new r_framework.CustomControl.CustomTextBox();
            this.CreateDate = new r_framework.CustomControl.CustomTextBox();
            this.CreateUser = new r_framework.CustomControl.CustomTextBox();
            this.KYOTEN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.SuspendLayout();
            // 
            // lb_title
            // 
            this.lb_title.Size = new System.Drawing.Size(255, 34);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(497, 1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 20);
            this.label8.TabIndex = 413;
            this.label8.Text = "拠点※";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(773, 1);
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
            this.label3.Location = new System.Drawing.Point(773, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 20);
            this.label3.TabIndex = 416;
            this.label3.Text = "最終更新";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Visible = false;
            // 
            // KYOTEN_NAME_RYAKU
            // 
            this.KYOTEN_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KYOTEN_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_NAME_RYAKU.CharacterLimitList = null;
            this.KYOTEN_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.KYOTEN_NAME_RYAKU.DBFieldsName = "KYOTEN_NAME_RYAKU";
            this.KYOTEN_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_NAME_RYAKU.DisplayPopUp = null;
            this.KYOTEN_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.FocusOutCheckMethod")));
            this.KYOTEN_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_NAME_RYAKU.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.KYOTEN_NAME_RYAKU.IsInputErrorOccured = false;
            this.KYOTEN_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.KYOTEN_NAME_RYAKU.Location = new System.Drawing.Point(604, 1);
            this.KYOTEN_NAME_RYAKU.MaxLength = 0;
            this.KYOTEN_NAME_RYAKU.Name = "KYOTEN_NAME_RYAKU";
            this.KYOTEN_NAME_RYAKU.PopupAfterExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupBeforeExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.PopupSearchSendParams")));
            this.KYOTEN_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOTEN_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.popupWindowSetting")));
            this.KYOTEN_NAME_RYAKU.ReadOnly = true;
            this.KYOTEN_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.RegistCheckMethod")));
            this.KYOTEN_NAME_RYAKU.Size = new System.Drawing.Size(160, 20);
            this.KYOTEN_NAME_RYAKU.TabIndex = 522;
            this.KYOTEN_NAME_RYAKU.TabStop = false;
            this.KYOTEN_NAME_RYAKU.Tag = " ";
            // 
            // LastUpdateDate
            // 
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
            this.LastUpdateDate.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LastUpdateDate.FocusOutCheckMethod")));
            this.LastUpdateDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.LastUpdateDate.ForeColor = System.Drawing.Color.Black;
            this.LastUpdateDate.GetCodeMasterField = "";
            this.LastUpdateDate.IsInputErrorOccured = false;
            this.LastUpdateDate.Location = new System.Drawing.Point(1008, 23);
            this.LastUpdateDate.MaxLength = 0;
            this.LastUpdateDate.Name = "LastUpdateDate";
            this.LastUpdateDate.PopupAfterExecute = null;
            this.LastUpdateDate.PopupBeforeExecute = null;
            this.LastUpdateDate.PopupGetMasterField = "";
            this.LastUpdateDate.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("LastUpdateDate.PopupSearchSendParams")));
            this.LastUpdateDate.PopupSetFormField = "";
            this.LastUpdateDate.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.LastUpdateDate.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("LastUpdateDate.popupWindowSetting")));
            this.LastUpdateDate.ReadOnly = true;
            this.LastUpdateDate.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LastUpdateDate.RegistCheckMethod")));
            this.LastUpdateDate.SetFormField = "";
            this.LastUpdateDate.Size = new System.Drawing.Size(160, 20);
            this.LastUpdateDate.TabIndex = 526;
            this.LastUpdateDate.TabStop = false;
            this.LastUpdateDate.Tag = "最終更新日が表示されます。";
            this.LastUpdateDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.LastUpdateDate.Visible = false;
            // 
            // LastUpdateUser
            // 
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
            this.LastUpdateUser.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LastUpdateUser.FocusOutCheckMethod")));
            this.LastUpdateUser.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.LastUpdateUser.ForeColor = System.Drawing.Color.Black;
            this.LastUpdateUser.GetCodeMasterField = "";
            this.LastUpdateUser.IsInputErrorOccured = false;
            this.LastUpdateUser.Location = new System.Drawing.Point(849, 23);
            this.LastUpdateUser.MaxLength = 0;
            this.LastUpdateUser.Name = "LastUpdateUser";
            this.LastUpdateUser.PopupAfterExecute = null;
            this.LastUpdateUser.PopupBeforeExecute = null;
            this.LastUpdateUser.PopupGetMasterField = "";
            this.LastUpdateUser.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("LastUpdateUser.PopupSearchSendParams")));
            this.LastUpdateUser.PopupSetFormField = "";
            this.LastUpdateUser.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.LastUpdateUser.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("LastUpdateUser.popupWindowSetting")));
            this.LastUpdateUser.ReadOnly = true;
            this.LastUpdateUser.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LastUpdateUser.RegistCheckMethod")));
            this.LastUpdateUser.SetFormField = "";
            this.LastUpdateUser.Size = new System.Drawing.Size(160, 20);
            this.LastUpdateUser.TabIndex = 525;
            this.LastUpdateUser.TabStop = false;
            this.LastUpdateUser.Tag = "最終更新者が表示されます。";
            this.LastUpdateUser.Visible = false;
            // 
            // CreateDate
            // 
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
            this.CreateDate.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CreateDate.FocusOutCheckMethod")));
            this.CreateDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CreateDate.ForeColor = System.Drawing.Color.Black;
            this.CreateDate.GetCodeMasterField = "";
            this.CreateDate.IsInputErrorOccured = false;
            this.CreateDate.Location = new System.Drawing.Point(1008, 1);
            this.CreateDate.MaxLength = 0;
            this.CreateDate.Name = "CreateDate";
            this.CreateDate.PopupAfterExecute = null;
            this.CreateDate.PopupBeforeExecute = null;
            this.CreateDate.PopupGetMasterField = "";
            this.CreateDate.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CreateDate.PopupSearchSendParams")));
            this.CreateDate.PopupSetFormField = "";
            this.CreateDate.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CreateDate.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CreateDate.popupWindowSetting")));
            this.CreateDate.ReadOnly = true;
            this.CreateDate.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CreateDate.RegistCheckMethod")));
            this.CreateDate.SetFormField = "";
            this.CreateDate.Size = new System.Drawing.Size(160, 20);
            this.CreateDate.TabIndex = 524;
            this.CreateDate.TabStop = false;
            this.CreateDate.Tag = "初回登録日が表示されます。";
            this.CreateDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CreateDate.Visible = false;
            // 
            // CreateUser
            // 
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
            this.CreateUser.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CreateUser.FocusOutCheckMethod")));
            this.CreateUser.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CreateUser.ForeColor = System.Drawing.Color.Black;
            this.CreateUser.GetCodeMasterField = "";
            this.CreateUser.IsInputErrorOccured = false;
            this.CreateUser.Location = new System.Drawing.Point(849, 1);
            this.CreateUser.MaxLength = 0;
            this.CreateUser.Name = "CreateUser";
            this.CreateUser.PopupAfterExecute = null;
            this.CreateUser.PopupBeforeExecute = null;
            this.CreateUser.PopupGetMasterField = "";
            this.CreateUser.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CreateUser.PopupSearchSendParams")));
            this.CreateUser.PopupSetFormField = "";
            this.CreateUser.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CreateUser.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CreateUser.popupWindowSetting")));
            this.CreateUser.ReadOnly = true;
            this.CreateUser.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CreateUser.RegistCheckMethod")));
            this.CreateUser.SetFormField = "";
            this.CreateUser.Size = new System.Drawing.Size(160, 20);
            this.CreateUser.TabIndex = 523;
            this.CreateUser.TabStop = false;
            this.CreateUser.Tag = "初回登録者が表示されます。";
            this.CreateUser.Visible = false;
            // 
            // KYOTEN_CD
            // 
            this.KYOTEN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.KYOTEN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_CD.CustomFormatSetting = "00";
            this.KYOTEN_CD.DBFieldsName = "KYOTEN_CD";
            this.KYOTEN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_CD.DisplayItemName = "拠点";
            this.KYOTEN_CD.DisplayPopUp = null;
            this.KYOTEN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.FocusOutCheckMethod")));
            this.KYOTEN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_CD.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_CD.FormatSetting = "カスタム";
            this.KYOTEN_CD.GetCodeMasterField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.IsInputErrorOccured = false;
            this.KYOTEN_CD.ItemDefinedTypes = "smallint";
            this.KYOTEN_CD.Location = new System.Drawing.Point(573, 1);
            this.KYOTEN_CD.Name = "KYOTEN_CD";
            this.KYOTEN_CD.PopupAfterExecute = null;
            this.KYOTEN_CD.PopupBeforeExecute = null;
            this.KYOTEN_CD.PopupGetMasterField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_CD.PopupSearchSendParams")));
            this.KYOTEN_CD.PopupSetFormField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.KYOTEN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.KYOTEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_CD.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.KYOTEN_CD.RangeSetting = rangeSettingDto1;
            this.KYOTEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.RegistCheckMethod")));
            this.KYOTEN_CD.SetFormField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.Size = new System.Drawing.Size(30, 20);
            this.KYOTEN_CD.TabIndex = 521;
            this.KYOTEN_CD.Tag = "拠点を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.KYOTEN_CD.WordWrap = false;
            // 
            // HeaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 45);
            this.Controls.Add(this.KYOTEN_CD);
            this.Controls.Add(this.LastUpdateDate);
            this.Controls.Add(this.LastUpdateUser);
            this.Controls.Add(this.CreateDate);
            this.Controls.Add(this.CreateUser);
            this.Controls.Add(this.KYOTEN_NAME_RYAKU);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label8);
            this.Name = "HeaderForm";
            this.Text = "HeaderForm";
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.KYOTEN_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.CreateUser, 0);
            this.Controls.SetChildIndex(this.CreateDate, 0);
            this.Controls.SetChildIndex(this.LastUpdateUser, 0);
            this.Controls.SetChildIndex(this.LastUpdateDate, 0);
            this.Controls.SetChildIndex(this.KYOTEN_CD, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public r_framework.CustomControl.CustomAlphaNumTextBox KYOTEN_NAME_RYAKU;
        public r_framework.CustomControl.CustomTextBox LastUpdateDate;
        public r_framework.CustomControl.CustomTextBox LastUpdateUser;
        public r_framework.CustomControl.CustomTextBox CreateDate;
        public r_framework.CustomControl.CustomTextBox CreateUser;
        public r_framework.CustomControl.CustomNumericTextBox2 KYOTEN_CD;
    }
}