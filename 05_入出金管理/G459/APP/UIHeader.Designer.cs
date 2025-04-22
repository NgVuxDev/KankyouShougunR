namespace Shougun.Core.ReceiptPayManagement.NyukinNyuryoku2
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
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.txtKyotenCd = new r_framework.CustomControl.CustomNumericTextBox2();
            this.txtKyotenName = new r_framework.CustomControl.CustomTextBox();
            this.lblKyoten = new System.Windows.Forms.Label();
            this.lblLastUpdateUser = new System.Windows.Forms.Label();
            this.txtLastUpdateDate = new r_framework.CustomControl.CustomTextBox();
            this.txtLastUpdateUser = new r_framework.CustomControl.CustomTextBox();
            this.lblCreateUser = new System.Windows.Forms.Label();
            this.txtCreateDate = new r_framework.CustomControl.CustomTextBox();
            this.txtCreateUser = new r_framework.CustomControl.CustomTextBox();
            this.SuspendLayout();
            // 
            // lb_title
            // 
            this.lb_title.Size = new System.Drawing.Size(288, 34);
            this.lb_title.Text = "入金入力(入金先)";
            // 
            // txtKyotenCd
            // 
            this.txtKyotenCd.BackColor = System.Drawing.SystemColors.Window;
            this.txtKyotenCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKyotenCd.CustomFormatSetting = "00";
            this.txtKyotenCd.DBFieldsName = "KYOTEN_CD";
            this.txtKyotenCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtKyotenCd.DisplayItemName = "拠点";
            this.txtKyotenCd.DisplayPopUp = null;
            this.txtKyotenCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenCd.FocusOutCheckMethod")));
            this.txtKyotenCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtKyotenCd.ForeColor = System.Drawing.Color.Black;
            this.txtKyotenCd.FormatSetting = "カスタム";
            this.txtKyotenCd.GetCodeMasterField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.txtKyotenCd.IsInputErrorOccured = false;
            this.txtKyotenCd.ItemDefinedTypes = "smallint";
            this.txtKyotenCd.Location = new System.Drawing.Point(535, 2);
            this.txtKyotenCd.Name = "txtKyotenCd";
            this.txtKyotenCd.PopupAfterExecute = null;
            this.txtKyotenCd.PopupBeforeExecute = null;
            this.txtKyotenCd.PopupGetMasterField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.txtKyotenCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtKyotenCd.PopupSearchSendParams")));
            this.txtKyotenCd.PopupSetFormField = "txtKyotenCd,txtKyotenName";
            this.txtKyotenCd.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.txtKyotenCd.PopupWindowName = "マスタ共通ポップアップ";
            this.txtKyotenCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtKyotenCd.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.txtKyotenCd.RangeSetting = rangeSettingDto1;
            this.txtKyotenCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenCd.RegistCheckMethod")));
            this.txtKyotenCd.SetFormField = "txtKyotenCd,txtKyotenName";
            this.txtKyotenCd.Size = new System.Drawing.Size(30, 20);
            this.txtKyotenCd.TabIndex = 0;
            this.txtKyotenCd.Tag = "拠点を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.txtKyotenCd.WordWrap = false;
            // 
            // txtKyotenName
            // 
            this.txtKyotenName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtKyotenName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKyotenName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txtKyotenName.DBFieldsName = "KYOTEN_NAME_RYAKU";
            this.txtKyotenName.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtKyotenName.DisplayPopUp = null;
            this.txtKyotenName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenName.FocusOutCheckMethod")));
            this.txtKyotenName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtKyotenName.ForeColor = System.Drawing.Color.Black;
            this.txtKyotenName.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtKyotenName.IsInputErrorOccured = false;
            this.txtKyotenName.ItemDefinedTypes = "varchar";
            this.txtKyotenName.Location = new System.Drawing.Point(564, 2);
            this.txtKyotenName.MaxLength = 0;
            this.txtKyotenName.Name = "txtKyotenName";
            this.txtKyotenName.PopupAfterExecute = null;
            this.txtKyotenName.PopupBeforeExecute = null;
            this.txtKyotenName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtKyotenName.PopupSearchSendParams")));
            this.txtKyotenName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtKyotenName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtKyotenName.popupWindowSetting")));
            this.txtKyotenName.ReadOnly = true;
            this.txtKyotenName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenName.RegistCheckMethod")));
            this.txtKyotenName.Size = new System.Drawing.Size(160, 20);
            this.txtKyotenName.TabIndex = 509;
            this.txtKyotenName.TabStop = false;
            this.txtKyotenName.Tag = "";
            // 
            // lblKyoten
            // 
            this.lblKyoten.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblKyoten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKyoten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblKyoten.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblKyoten.ForeColor = System.Drawing.Color.White;
            this.lblKyoten.Location = new System.Drawing.Point(420, 2);
            this.lblKyoten.Name = "lblKyoten";
            this.lblKyoten.Size = new System.Drawing.Size(110, 20);
            this.lblKyoten.TabIndex = 508;
            this.lblKyoten.Text = "拠点※";
            this.lblKyoten.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLastUpdateUser
            // 
            this.lblLastUpdateUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblLastUpdateUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLastUpdateUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblLastUpdateUser.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblLastUpdateUser.ForeColor = System.Drawing.Color.White;
            this.lblLastUpdateUser.Location = new System.Drawing.Point(734, 24);
            this.lblLastUpdateUser.Name = "lblLastUpdateUser";
            this.lblLastUpdateUser.Size = new System.Drawing.Size(110, 20);
            this.lblLastUpdateUser.TabIndex = 505;
            this.lblLastUpdateUser.Text = "最終更新";
            this.lblLastUpdateUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLastUpdateUser.Visible = false;
            // 
            // txtLastUpdateDate
            // 
            this.txtLastUpdateDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtLastUpdateDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLastUpdateDate.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txtLastUpdateDate.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtLastUpdateDate.DisplayPopUp = null;
            this.txtLastUpdateDate.ErrorMessage = "";
            this.txtLastUpdateDate.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtLastUpdateDate.FocusOutCheckMethod")));
            this.txtLastUpdateDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtLastUpdateDate.ForeColor = System.Drawing.Color.Black;
            this.txtLastUpdateDate.GetCodeMasterField = "";
            this.txtLastUpdateDate.IsInputErrorOccured = false;
            this.txtLastUpdateDate.Location = new System.Drawing.Point(1008, 24);
            this.txtLastUpdateDate.MaxLength = 0;
            this.txtLastUpdateDate.Name = "txtLastUpdateDate";
            this.txtLastUpdateDate.PopupAfterExecute = null;
            this.txtLastUpdateDate.PopupBeforeExecute = null;
            this.txtLastUpdateDate.PopupGetMasterField = "";
            this.txtLastUpdateDate.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtLastUpdateDate.PopupSearchSendParams")));
            this.txtLastUpdateDate.PopupSetFormField = "";
            this.txtLastUpdateDate.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtLastUpdateDate.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtLastUpdateDate.popupWindowSetting")));
            this.txtLastUpdateDate.ReadOnly = true;
            this.txtLastUpdateDate.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtLastUpdateDate.RegistCheckMethod")));
            this.txtLastUpdateDate.SetFormField = "";
            this.txtLastUpdateDate.Size = new System.Drawing.Size(160, 20);
            this.txtLastUpdateDate.TabIndex = 504;
            this.txtLastUpdateDate.TabStop = false;
            this.txtLastUpdateDate.Tag = "";
            this.txtLastUpdateDate.Visible = false;
            // 
            // txtLastUpdateUser
            // 
            this.txtLastUpdateUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtLastUpdateUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLastUpdateUser.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txtLastUpdateUser.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtLastUpdateUser.DisplayPopUp = null;
            this.txtLastUpdateUser.ErrorMessage = "";
            this.txtLastUpdateUser.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtLastUpdateUser.FocusOutCheckMethod")));
            this.txtLastUpdateUser.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtLastUpdateUser.ForeColor = System.Drawing.Color.Black;
            this.txtLastUpdateUser.GetCodeMasterField = "";
            this.txtLastUpdateUser.IsInputErrorOccured = false;
            this.txtLastUpdateUser.Location = new System.Drawing.Point(849, 24);
            this.txtLastUpdateUser.MaxLength = 0;
            this.txtLastUpdateUser.Name = "txtLastUpdateUser";
            this.txtLastUpdateUser.PopupAfterExecute = null;
            this.txtLastUpdateUser.PopupBeforeExecute = null;
            this.txtLastUpdateUser.PopupGetMasterField = "";
            this.txtLastUpdateUser.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtLastUpdateUser.PopupSearchSendParams")));
            this.txtLastUpdateUser.PopupSetFormField = "";
            this.txtLastUpdateUser.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtLastUpdateUser.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtLastUpdateUser.popupWindowSetting")));
            this.txtLastUpdateUser.ReadOnly = true;
            this.txtLastUpdateUser.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtLastUpdateUser.RegistCheckMethod")));
            this.txtLastUpdateUser.SetFormField = "";
            this.txtLastUpdateUser.Size = new System.Drawing.Size(160, 20);
            this.txtLastUpdateUser.TabIndex = 503;
            this.txtLastUpdateUser.TabStop = false;
            this.txtLastUpdateUser.Tag = "";
            this.txtLastUpdateUser.Visible = false;
            // 
            // lblCreateUser
            // 
            this.lblCreateUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblCreateUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCreateUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCreateUser.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblCreateUser.ForeColor = System.Drawing.Color.White;
            this.lblCreateUser.Location = new System.Drawing.Point(734, 2);
            this.lblCreateUser.Name = "lblCreateUser";
            this.lblCreateUser.Size = new System.Drawing.Size(110, 20);
            this.lblCreateUser.TabIndex = 502;
            this.lblCreateUser.Text = "初回登録";
            this.lblCreateUser.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCreateUser.Visible = false;
            // 
            // txtCreateDate
            // 
            this.txtCreateDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtCreateDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCreateDate.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txtCreateDate.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtCreateDate.DisplayPopUp = null;
            this.txtCreateDate.ErrorMessage = "";
            this.txtCreateDate.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtCreateDate.FocusOutCheckMethod")));
            this.txtCreateDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtCreateDate.ForeColor = System.Drawing.Color.Black;
            this.txtCreateDate.GetCodeMasterField = "";
            this.txtCreateDate.IsInputErrorOccured = false;
            this.txtCreateDate.Location = new System.Drawing.Point(1008, 2);
            this.txtCreateDate.MaxLength = 0;
            this.txtCreateDate.Name = "txtCreateDate";
            this.txtCreateDate.PopupAfterExecute = null;
            this.txtCreateDate.PopupBeforeExecute = null;
            this.txtCreateDate.PopupGetMasterField = "";
            this.txtCreateDate.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtCreateDate.PopupSearchSendParams")));
            this.txtCreateDate.PopupSetFormField = "";
            this.txtCreateDate.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtCreateDate.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtCreateDate.popupWindowSetting")));
            this.txtCreateDate.ReadOnly = true;
            this.txtCreateDate.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtCreateDate.RegistCheckMethod")));
            this.txtCreateDate.SetFormField = "";
            this.txtCreateDate.Size = new System.Drawing.Size(160, 20);
            this.txtCreateDate.TabIndex = 501;
            this.txtCreateDate.TabStop = false;
            this.txtCreateDate.Tag = "";
            this.txtCreateDate.Visible = false;
            // 
            // txtCreateUser
            // 
            this.txtCreateUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtCreateUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCreateUser.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txtCreateUser.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtCreateUser.DisplayPopUp = null;
            this.txtCreateUser.ErrorMessage = "";
            this.txtCreateUser.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtCreateUser.FocusOutCheckMethod")));
            this.txtCreateUser.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtCreateUser.ForeColor = System.Drawing.Color.Black;
            this.txtCreateUser.GetCodeMasterField = "";
            this.txtCreateUser.IsInputErrorOccured = false;
            this.txtCreateUser.Location = new System.Drawing.Point(849, 2);
            this.txtCreateUser.MaxLength = 0;
            this.txtCreateUser.Name = "txtCreateUser";
            this.txtCreateUser.PopupAfterExecute = null;
            this.txtCreateUser.PopupBeforeExecute = null;
            this.txtCreateUser.PopupGetMasterField = "";
            this.txtCreateUser.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtCreateUser.PopupSearchSendParams")));
            this.txtCreateUser.PopupSetFormField = "";
            this.txtCreateUser.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtCreateUser.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtCreateUser.popupWindowSetting")));
            this.txtCreateUser.ReadOnly = true;
            this.txtCreateUser.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtCreateUser.RegistCheckMethod")));
            this.txtCreateUser.SetFormField = "";
            this.txtCreateUser.Size = new System.Drawing.Size(160, 20);
            this.txtCreateUser.TabIndex = 500;
            this.txtCreateUser.TabStop = false;
            this.txtCreateUser.Tag = "";
            this.txtCreateUser.Visible = false;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.txtKyotenCd);
            this.Controls.Add(this.txtKyotenName);
            this.Controls.Add(this.lblKyoten);
            this.Controls.Add(this.lblLastUpdateUser);
            this.Controls.Add(this.txtLastUpdateDate);
            this.Controls.Add(this.txtLastUpdateUser);
            this.Controls.Add(this.lblCreateUser);
            this.Controls.Add(this.txtCreateDate);
            this.Controls.Add(this.txtCreateUser);
            this.Name = "UIHeader";
            this.Text = "HeaderSample";
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.txtCreateUser, 0);
            this.Controls.SetChildIndex(this.txtCreateDate, 0);
            this.Controls.SetChildIndex(this.lblCreateUser, 0);
            this.Controls.SetChildIndex(this.txtLastUpdateUser, 0);
            this.Controls.SetChildIndex(this.txtLastUpdateDate, 0);
            this.Controls.SetChildIndex(this.lblLastUpdateUser, 0);
            this.Controls.SetChildIndex(this.lblKyoten, 0);
            this.Controls.SetChildIndex(this.txtKyotenName, 0);
            this.Controls.SetChildIndex(this.txtKyotenCd, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomNumericTextBox2 txtKyotenCd;
        public r_framework.CustomControl.CustomTextBox txtKyotenName;
        public System.Windows.Forms.Label lblKyoten;
        public System.Windows.Forms.Label lblLastUpdateUser;
        public r_framework.CustomControl.CustomTextBox txtLastUpdateDate;
        public r_framework.CustomControl.CustomTextBox txtLastUpdateUser;
        public System.Windows.Forms.Label lblCreateUser;
        public r_framework.CustomControl.CustomTextBox txtCreateDate;
        public r_framework.CustomControl.CustomTextBox txtCreateUser;


    }
}