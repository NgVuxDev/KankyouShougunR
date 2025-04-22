namespace Shougun.Core.Common.Login
{
    partial class UIForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            this.lb_title = new System.Windows.Forms.Label();
            this.LoginNameTextbox = new r_framework.CustomControl.CustomTextBox();
            this.IdLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.PasswordSaveCheckbox = new r_framework.CustomControl.CustomCheckBox();
            this.DatabaseLabel = new System.Windows.Forms.Label();
            this.DatabaseCombobox = new r_framework.CustomControl.CustomComboBox();
            this.OKButton = new r_framework.CustomControl.CustomButton();
            this.CancelButton = new r_framework.CustomControl.CustomButton();
            this.LoginIDTextbox = new r_framework.CustomControl.CustomTextBox();
            this.PassWordTextbox = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.DeleteButton = new r_framework.CustomControl.CustomButton();
            this.SHAIN_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.SuspendLayout();
            // 
            // lb_title
            // 
            this.lb_title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lb_title.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_title.Font = new System.Drawing.Font("ＭＳ ゴシック", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lb_title.ForeColor = System.Drawing.Color.White;
            this.lb_title.Location = new System.Drawing.Point(9, 12);
            this.lb_title.Name = "lb_title";
            this.lb_title.Size = new System.Drawing.Size(407, 35);
            this.lb_title.TabIndex = 0;
            this.lb_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LoginNameTextbox
            // 
            this.LoginNameTextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.LoginNameTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LoginNameTextbox.DBFieldsName = "SHAIN_NAME_RYAKU";
            this.LoginNameTextbox.DefaultBackColor = System.Drawing.Color.Empty;
            this.LoginNameTextbox.DisplayPopUp = null;
            this.LoginNameTextbox.FocusOutCheckMethod = null;
            this.LoginNameTextbox.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.LoginNameTextbox.ForeColor = System.Drawing.Color.Black;
            this.LoginNameTextbox.IsInputErrorOccured = false;
            this.LoginNameTextbox.Location = new System.Drawing.Point(263, 72);
            this.LoginNameTextbox.MaxLength = 0;
            this.LoginNameTextbox.Name = "LoginNameTextbox";
            this.LoginNameTextbox.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("LoginNameTextbox.PopupSearchSendParams")));
            this.LoginNameTextbox.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.LoginNameTextbox.popupWindowSetting = null;
            this.LoginNameTextbox.ReadOnly = true;
            this.LoginNameTextbox.RegistCheckMethod = null;
            this.LoginNameTextbox.Size = new System.Drawing.Size(132, 20);
            this.LoginNameTextbox.TabIndex = 3;
            this.LoginNameTextbox.TabStop = false;
            this.LoginNameTextbox.Tag = " ";
            // 
            // IdLabel
            // 
            this.IdLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.IdLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IdLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.IdLabel.ForeColor = System.Drawing.Color.White;
            this.IdLabel.Location = new System.Drawing.Point(9, 72);
            this.IdLabel.Name = "IdLabel";
            this.IdLabel.Size = new System.Drawing.Size(152, 20);
            this.IdLabel.TabIndex = 1;
            this.IdLabel.Text = "ログインユーザー";
            this.IdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.PasswordLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PasswordLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PasswordLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PasswordLabel.ForeColor = System.Drawing.Color.White;
            this.PasswordLabel.Location = new System.Drawing.Point(9, 109);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(152, 20);
            this.PasswordLabel.TabIndex = 4;
            this.PasswordLabel.Text = "パスワード";
            this.PasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PasswordSaveCheckbox
            // 
            this.PasswordSaveCheckbox.AutoSize = true;
            this.PasswordSaveCheckbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.PasswordSaveCheckbox.DefaultBackColor = System.Drawing.Color.Empty;
            this.PasswordSaveCheckbox.FocusOutCheckMethod = null;
            this.PasswordSaveCheckbox.Location = new System.Drawing.Point(166, 135);
            this.PasswordSaveCheckbox.Name = "PasswordSaveCheckbox";
            this.PasswordSaveCheckbox.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PasswordSaveCheckbox.PopupSearchSendParams")));
            this.PasswordSaveCheckbox.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PasswordSaveCheckbox.popupWindowSetting = null;
            this.PasswordSaveCheckbox.RegistCheckMethod = null;
            this.PasswordSaveCheckbox.Size = new System.Drawing.Size(123, 16);
            this.PasswordSaveCheckbox.TabIndex = 6;
            this.PasswordSaveCheckbox.Text = "パスワードを保存する";
            this.PasswordSaveCheckbox.UseVisualStyleBackColor = false;
            this.PasswordSaveCheckbox.TabStop = false;
            // 
            // DatabaseLabel
            // 
            this.DatabaseLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.DatabaseLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DatabaseLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DatabaseLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DatabaseLabel.ForeColor = System.Drawing.Color.White;
            this.DatabaseLabel.Location = new System.Drawing.Point(9, 165);
            this.DatabaseLabel.Name = "DatabaseLabel";
            this.DatabaseLabel.Size = new System.Drawing.Size(152, 20);
            this.DatabaseLabel.TabIndex = 7;
            this.DatabaseLabel.Text = "接続先データベース";
            this.DatabaseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DatabaseCombobox
            // 
            this.DatabaseCombobox.BackColor = System.Drawing.SystemColors.Window;
            this.DatabaseCombobox.DefaultBackColor = System.Drawing.Color.Empty;
            this.DatabaseCombobox.DisplayPopUp = null;
            this.DatabaseCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DatabaseCombobox.FocusOutCheckMethod = null;
            this.DatabaseCombobox.FormattingEnabled = true;
            this.DatabaseCombobox.IsInputErrorOccured = false;
            this.DatabaseCombobox.Location = new System.Drawing.Point(166, 165);
            this.DatabaseCombobox.Name = "DatabaseCombobox";
            this.DatabaseCombobox.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DatabaseCombobox.PopupSearchSendParams")));
            this.DatabaseCombobox.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DatabaseCombobox.popupWindowSetting = null;
            this.DatabaseCombobox.RegistCheckMethod = null;
            this.DatabaseCombobox.Size = new System.Drawing.Size(249, 20);
            this.DatabaseCombobox.TabIndex = 8;
            this.DatabaseCombobox.Tag = "";
            this.DatabaseCombobox.TabStop = false;
            // 
            // OKButton
            // 
            this.OKButton.DefaultBackColor = System.Drawing.Color.Empty;
            this.OKButton.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.OKButton.Location = new System.Drawing.Point(191, 220);
            this.OKButton.Name = "OKButton";
            this.OKButton.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.OKButton.Size = new System.Drawing.Size(110, 35);
            this.OKButton.TabIndex = 9;
            this.OKButton.Text = "ログイン";
            this.OKButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            this.CancelButton.DefaultBackColor = System.Drawing.Color.Empty;
            this.CancelButton.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CancelButton.Location = new System.Drawing.Point(306, 220);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.CancelButton.Size = new System.Drawing.Size(110, 35);
            this.CancelButton.TabIndex = 10;
            this.CancelButton.Text = "キャンセル";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.TabStop = false;
            // 
            // LoginIDTextbox
            // 
            this.LoginIDTextbox.BackColor = System.Drawing.SystemColors.Window;
            this.LoginIDTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LoginIDTextbox.DBFieldsName = "SHAIN_CD";
            this.LoginIDTextbox.DefaultBackColor = System.Drawing.Color.Empty;
            this.LoginIDTextbox.DisplayPopUp = null;
            this.LoginIDTextbox.FocusOutCheckMethod = null;
            this.LoginIDTextbox.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.LoginIDTextbox.ForeColor = System.Drawing.Color.Black;
            this.LoginIDTextbox.GetCodeMasterField = "";
            this.LoginIDTextbox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.LoginIDTextbox.IsInputErrorOccured = false;
            this.LoginIDTextbox.ItemDefinedTypes = "varchar";
            this.LoginIDTextbox.Location = new System.Drawing.Point(166, 72);
            this.LoginIDTextbox.MaxLength = 24;
            this.LoginIDTextbox.Name = "LoginIDTextbox";
            this.LoginIDTextbox.PopupGetMasterField = "";
            this.LoginIDTextbox.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("LoginIDTextbox.PopupSearchSendParams")));
            this.LoginIDTextbox.PopupSetFormField = "LoginIDTextbox,LoginNameTextbox";
            this.LoginIDTextbox.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHAIN;
            this.LoginIDTextbox.PopupWindowName = "マスタ共通ポップアップ";
            this.LoginIDTextbox.popupWindowSetting = null;
            this.LoginIDTextbox.RegistCheckMethod = null;
            this.LoginIDTextbox.SetFormField = "LoginIDTextbox,LoginNameTextbox";
            this.LoginIDTextbox.Size = new System.Drawing.Size(98, 20);
            this.LoginIDTextbox.TabIndex = 2;
            this.LoginIDTextbox.Tag = "";
            // 
            // PassWordTextbox
            // 
            this.PassWordTextbox.BackColor = System.Drawing.SystemColors.Window;
            this.PassWordTextbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PassWordTextbox.CharacterLimitList = null;
            this.PassWordTextbox.DefaultBackColor = System.Drawing.Color.Empty;
            this.PassWordTextbox.DisplayPopUp = null;
            this.PassWordTextbox.FocusOutCheckMethod = null;
            this.PassWordTextbox.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.PassWordTextbox.ForeColor = System.Drawing.Color.Black;
            this.PassWordTextbox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.PassWordTextbox.IsInputErrorOccured = false;
            this.PassWordTextbox.Location = new System.Drawing.Point(166, 109);
            this.PassWordTextbox.Name = "PassWordTextbox";
            this.PassWordTextbox.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PassWordTextbox.PopupSearchSendParams")));
            this.PassWordTextbox.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PassWordTextbox.popupWindowSetting = null;
            this.PassWordTextbox.RegistCheckMethod = null;
            this.PassWordTextbox.Size = new System.Drawing.Size(249, 20);
            this.PassWordTextbox.TabIndex = 5;
            this.PassWordTextbox.Tag = "";
            // 
            // DeleteButton
            // 
            this.DeleteButton.DefaultBackColor = System.Drawing.Color.Empty;
            this.DeleteButton.Location = new System.Drawing.Point(9, 220);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.DeleteButton.Size = new System.Drawing.Size(44, 35);
            this.DeleteButton.TabIndex = 11;
            this.DeleteButton.Text = "切断";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            this.DeleteButton.TabStop = false;
            // 
            // SHAIN_SEARCH_BUTTON
            // 
            this.SHAIN_SEARCH_BUTTON.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.SHAIN_SEARCH_BUTTON.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SHAIN_SEARCH_BUTTON.DBFieldsName = null;
            this.SHAIN_SEARCH_BUTTON.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHAIN_SEARCH_BUTTON.DisplayItemName = null;
            this.SHAIN_SEARCH_BUTTON.DisplayPopUp = null;
            this.SHAIN_SEARCH_BUTTON.ErrorMessage = null;
            this.SHAIN_SEARCH_BUTTON.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHAIN_SEARCH_BUTTON.FocusOutCheckMethod")));
            this.SHAIN_SEARCH_BUTTON.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHAIN_SEARCH_BUTTON.GetCodeMasterField = null;
            this.SHAIN_SEARCH_BUTTON.Image = ((System.Drawing.Image)(resources.GetObject("SHAIN_SEARCH_BUTTON.Image")));
            this.SHAIN_SEARCH_BUTTON.ItemDefinedTypes = null;
            this.SHAIN_SEARCH_BUTTON.LinkedSettingTextBox = null;
            this.SHAIN_SEARCH_BUTTON.LinkedTextBoxs = new string[0];
            this.SHAIN_SEARCH_BUTTON.Location = new System.Drawing.Point(395, 71);
            this.SHAIN_SEARCH_BUTTON.Name = "SHAIN_SEARCH_BUTTON";
            this.SHAIN_SEARCH_BUTTON.PopupAfterExecute = null;
            this.SHAIN_SEARCH_BUTTON.PopupAfterExecuteMethod = "BtnPopupMethod";
            this.SHAIN_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.SHAIN_SEARCH_BUTTON.PopupGetMasterField = "LOGIN_ID,SHAIN_NAME_RYAKU";
            this.SHAIN_SEARCH_BUTTON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHAIN_SEARCH_BUTTON.PopupSearchSendParams")));
            this.SHAIN_SEARCH_BUTTON.PopupSetFormField = "LoginIDTextbox,LoginNameTextbox";
            this.SHAIN_SEARCH_BUTTON.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHAIN;
            this.SHAIN_SEARCH_BUTTON.PopupWindowName = "マスタ共通ポップアップ";
            this.SHAIN_SEARCH_BUTTON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHAIN_SEARCH_BUTTON.popupWindowSetting")));
            this.SHAIN_SEARCH_BUTTON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHAIN_SEARCH_BUTTON.RegistCheckMethod")));
            this.SHAIN_SEARCH_BUTTON.SearchDisplayFlag = 0;
            this.SHAIN_SEARCH_BUTTON.SetFormField = "LoginIDTextbox,LoginNameTextbox";
            this.SHAIN_SEARCH_BUTTON.ShortItemName = null;
            this.SHAIN_SEARCH_BUTTON.Size = new System.Drawing.Size(22, 22);
            this.SHAIN_SEARCH_BUTTON.TabIndex = 327;
            this.SHAIN_SEARCH_BUTTON.TabStop = false;
            this.SHAIN_SEARCH_BUTTON.Tag = "";
            this.SHAIN_SEARCH_BUTTON.UseVisualStyleBackColor = false;
            this.SHAIN_SEARCH_BUTTON.ZeroPaddengFlag = false;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(427, 267);
            this.Controls.Add(this.SHAIN_SEARCH_BUTTON);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.PassWordTextbox);
            this.Controls.Add(this.LoginIDTextbox);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.DatabaseCombobox);
            this.Controls.Add(this.DatabaseLabel);
            this.Controls.Add(this.PasswordSaveCheckbox);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.LoginNameTextbox);
            this.Controls.Add(this.IdLabel);
            this.Controls.Add(this.lb_title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UIForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "環境将軍R　ログイン";
            this.WindowId = r_framework.Const.WINDOW_ID.LOGIN;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lb_title;
        public r_framework.CustomControl.CustomTextBox LoginNameTextbox;
        public System.Windows.Forms.Label IdLabel;
        public System.Windows.Forms.Label PasswordLabel;
        private r_framework.CustomControl.CustomCheckBox PasswordSaveCheckbox;
        public System.Windows.Forms.Label DatabaseLabel;
        private r_framework.CustomControl.CustomComboBox DatabaseCombobox;
        private r_framework.CustomControl.CustomButton OKButton;
        private r_framework.CustomControl.CustomButton CancelButton;
        private r_framework.CustomControl.CustomTextBox LoginIDTextbox;
        private r_framework.CustomControl.CustomAlphaNumTextBox PassWordTextbox;
        private r_framework.CustomControl.CustomButton DeleteButton;
        internal r_framework.CustomControl.CustomPopupOpenButton SHAIN_SEARCH_BUTTON;
    }
}