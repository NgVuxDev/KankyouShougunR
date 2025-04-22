namespace Shougun.Core.ExternalConnection.DenshiKeiyakuNyuryoku
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
            this.labelLastUpdate = new System.Windows.Forms.Label();
            this.LastUpdateDate = new r_framework.CustomControl.CustomTextBox();
            this.LastUpdateUser = new r_framework.CustomControl.CustomTextBox();
            this.labelInitCreate = new System.Windows.Forms.Label();
            this.CreateDate = new r_framework.CustomControl.CustomTextBox();
            this.CreateUser = new r_framework.CustomControl.CustomTextBox();
            this.labelKeiyakuDate = new System.Windows.Forms.Label();
            this.KeiyakuDate = new r_framework.CustomControl.CustomTextBox();
            this.labelCreatebi = new System.Windows.Forms.Label();
            this.Createbi = new r_framework.CustomControl.CustomTextBox();
            this.labelSendDate = new System.Windows.Forms.Label();
            this.SendDate = new r_framework.CustomControl.CustomTextBox();
            this.labelItakuSyoshiki = new System.Windows.Forms.Label();
            this.ItakuSyoshiki = new r_framework.CustomControl.CustomTextBox();
            this.labelItakuShurui = new System.Windows.Forms.Label();
            this.ItakuShurui = new r_framework.CustomControl.CustomTextBox();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Location = new System.Drawing.Point(1, 8);
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(77, 1);
            this.lb_title.Size = new System.Drawing.Size(240, 34);
            // 
            // labelLastUpdate
            // 
            this.labelLastUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.labelLastUpdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelLastUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelLastUpdate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.labelLastUpdate.ForeColor = System.Drawing.Color.White;
            this.labelLastUpdate.Location = new System.Drawing.Point(820, 2);
            this.labelLastUpdate.Name = "labelLastUpdate";
            this.labelLastUpdate.Size = new System.Drawing.Size(110, 20);
            this.labelLastUpdate.TabIndex = 394;
            this.labelLastUpdate.Text = "最終更新";
            this.labelLastUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.LastUpdateDate.FocusOutCheckMethod = null;
            this.LastUpdateDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.LastUpdateDate.ForeColor = System.Drawing.Color.Black;
            this.LastUpdateDate.GetCodeMasterField = "";
            this.LastUpdateDate.IsInputErrorOccured = false;
            this.LastUpdateDate.Location = new System.Drawing.Point(1039, 2);
            this.LastUpdateDate.MaxLength = 0;
            this.LastUpdateDate.Name = "LastUpdateDate";
            this.LastUpdateDate.PopupAfterExecute = null;
            this.LastUpdateDate.PopupBeforeExecute = null;
            this.LastUpdateDate.PopupGetMasterField = "";
            this.LastUpdateDate.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("LastUpdateDate.PopupSearchSendParams")));
            this.LastUpdateDate.PopupSetFormField = "";
            this.LastUpdateDate.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.LastUpdateDate.popupWindowSetting = null;
            this.LastUpdateDate.ReadOnly = true;
            this.LastUpdateDate.RegistCheckMethod = null;
            this.LastUpdateDate.SetFormField = "";
            this.LastUpdateDate.Size = new System.Drawing.Size(137, 20);
            this.LastUpdateDate.TabIndex = 393;
            this.LastUpdateDate.TabStop = false;
            this.LastUpdateDate.Tag = "最終更新日が表示されます。";
            this.LastUpdateDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.LastUpdateUser.FocusOutCheckMethod = null;
            this.LastUpdateUser.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.LastUpdateUser.ForeColor = System.Drawing.Color.Black;
            this.LastUpdateUser.GetCodeMasterField = "";
            this.LastUpdateUser.IsInputErrorOccured = false;
            this.LastUpdateUser.Location = new System.Drawing.Point(931, 2);
            this.LastUpdateUser.MaxLength = 0;
            this.LastUpdateUser.Name = "LastUpdateUser";
            this.LastUpdateUser.PopupAfterExecute = null;
            this.LastUpdateUser.PopupBeforeExecute = null;
            this.LastUpdateUser.PopupGetMasterField = "";
            this.LastUpdateUser.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("LastUpdateUser.PopupSearchSendParams")));
            this.LastUpdateUser.PopupSetFormField = "";
            this.LastUpdateUser.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.LastUpdateUser.popupWindowSetting = null;
            this.LastUpdateUser.ReadOnly = true;
            this.LastUpdateUser.RegistCheckMethod = null;
            this.LastUpdateUser.SetFormField = "";
            this.LastUpdateUser.Size = new System.Drawing.Size(107, 20);
            this.LastUpdateUser.TabIndex = 392;
            this.LastUpdateUser.TabStop = false;
            this.LastUpdateUser.Tag = "最終更新者が表示されます。";
            // 
            // labelInitCreate
            // 
            this.labelInitCreate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.labelInitCreate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelInitCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelInitCreate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.labelInitCreate.ForeColor = System.Drawing.Color.White;
            this.labelInitCreate.Location = new System.Drawing.Point(820, 24);
            this.labelInitCreate.Name = "labelInitCreate";
            this.labelInitCreate.Size = new System.Drawing.Size(110, 20);
            this.labelInitCreate.TabIndex = 391;
            this.labelInitCreate.Text = "初回登録";
            this.labelInitCreate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.CreateDate.FocusOutCheckMethod = null;
            this.CreateDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CreateDate.ForeColor = System.Drawing.Color.Black;
            this.CreateDate.GetCodeMasterField = "";
            this.CreateDate.IsInputErrorOccured = false;
            this.CreateDate.Location = new System.Drawing.Point(1039, 24);
            this.CreateDate.MaxLength = 0;
            this.CreateDate.Name = "CreateDate";
            this.CreateDate.PopupAfterExecute = null;
            this.CreateDate.PopupBeforeExecute = null;
            this.CreateDate.PopupGetMasterField = "";
            this.CreateDate.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CreateDate.PopupSearchSendParams")));
            this.CreateDate.PopupSetFormField = "";
            this.CreateDate.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CreateDate.popupWindowSetting = null;
            this.CreateDate.ReadOnly = true;
            this.CreateDate.RegistCheckMethod = null;
            this.CreateDate.SetFormField = "";
            this.CreateDate.Size = new System.Drawing.Size(137, 20);
            this.CreateDate.TabIndex = 390;
            this.CreateDate.TabStop = false;
            this.CreateDate.Tag = "初回登録日が表示されます。";
            this.CreateDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.CreateUser.FocusOutCheckMethod = null;
            this.CreateUser.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CreateUser.ForeColor = System.Drawing.Color.Black;
            this.CreateUser.GetCodeMasterField = "";
            this.CreateUser.IsInputErrorOccured = false;
            this.CreateUser.Location = new System.Drawing.Point(931, 24);
            this.CreateUser.MaxLength = 0;
            this.CreateUser.Name = "CreateUser";
            this.CreateUser.PopupAfterExecute = null;
            this.CreateUser.PopupBeforeExecute = null;
            this.CreateUser.PopupGetMasterField = "";
            this.CreateUser.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CreateUser.PopupSearchSendParams")));
            this.CreateUser.PopupSetFormField = "";
            this.CreateUser.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CreateUser.popupWindowSetting = null;
            this.CreateUser.ReadOnly = true;
            this.CreateUser.RegistCheckMethod = null;
            this.CreateUser.SetFormField = "";
            this.CreateUser.Size = new System.Drawing.Size(107, 20);
            this.CreateUser.TabIndex = 389;
            this.CreateUser.TabStop = false;
            this.CreateUser.Tag = "初回登録者が表示されます。";
            // 
            // labelKeiyakuDate
            // 
            this.labelKeiyakuDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.labelKeiyakuDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelKeiyakuDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelKeiyakuDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.labelKeiyakuDate.ForeColor = System.Drawing.Color.White;
            this.labelKeiyakuDate.Location = new System.Drawing.Point(600, 2);
            this.labelKeiyakuDate.Name = "labelKeiyakuDate";
            this.labelKeiyakuDate.Size = new System.Drawing.Size(110, 20);
            this.labelKeiyakuDate.TabIndex = 396;
            this.labelKeiyakuDate.Text = "契約日";
            this.labelKeiyakuDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KeiyakuDate
            // 
            this.KeiyakuDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KeiyakuDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KeiyakuDate.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.KeiyakuDate.DefaultBackColor = System.Drawing.Color.Empty;
            this.KeiyakuDate.DisplayPopUp = null;
            this.KeiyakuDate.ErrorMessage = "";
            this.KeiyakuDate.FocusOutCheckMethod = null;
            this.KeiyakuDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KeiyakuDate.ForeColor = System.Drawing.Color.Black;
            this.KeiyakuDate.GetCodeMasterField = "";
            this.KeiyakuDate.IsInputErrorOccured = false;
            this.KeiyakuDate.Location = new System.Drawing.Point(711, 2);
            this.KeiyakuDate.MaxLength = 0;
            this.KeiyakuDate.Name = "KeiyakuDate";
            this.KeiyakuDate.PopupAfterExecute = null;
            this.KeiyakuDate.PopupBeforeExecute = null;
            this.KeiyakuDate.PopupGetMasterField = "";
            this.KeiyakuDate.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KeiyakuDate.PopupSearchSendParams")));
            this.KeiyakuDate.PopupSetFormField = "";
            this.KeiyakuDate.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KeiyakuDate.popupWindowSetting = null;
            this.KeiyakuDate.ReadOnly = true;
            this.KeiyakuDate.RegistCheckMethod = null;
            this.KeiyakuDate.SetFormField = "";
            this.KeiyakuDate.Size = new System.Drawing.Size(105, 20);
            this.KeiyakuDate.TabIndex = 395;
            this.KeiyakuDate.TabStop = false;
            this.KeiyakuDate.Tag = "契約日が表示されます。";
            // 
            // labelCreatebi
            // 
            this.labelCreatebi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.labelCreatebi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelCreatebi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelCreatebi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.labelCreatebi.ForeColor = System.Drawing.Color.White;
            this.labelCreatebi.Location = new System.Drawing.Point(600, 24);
            this.labelCreatebi.Name = "labelCreatebi";
            this.labelCreatebi.Size = new System.Drawing.Size(110, 20);
            this.labelCreatebi.TabIndex = 398;
            this.labelCreatebi.Text = "作成日";
            this.labelCreatebi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Createbi
            // 
            this.Createbi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.Createbi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Createbi.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.Createbi.DefaultBackColor = System.Drawing.Color.Empty;
            this.Createbi.DisplayPopUp = null;
            this.Createbi.ErrorMessage = "";
            this.Createbi.FocusOutCheckMethod = null;
            this.Createbi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Createbi.ForeColor = System.Drawing.Color.Black;
            this.Createbi.GetCodeMasterField = "";
            this.Createbi.IsInputErrorOccured = false;
            this.Createbi.Location = new System.Drawing.Point(711, 24);
            this.Createbi.MaxLength = 0;
            this.Createbi.Name = "Createbi";
            this.Createbi.PopupAfterExecute = null;
            this.Createbi.PopupBeforeExecute = null;
            this.Createbi.PopupGetMasterField = "";
            this.Createbi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Createbi.PopupSearchSendParams")));
            this.Createbi.PopupSetFormField = "";
            this.Createbi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Createbi.popupWindowSetting = null;
            this.Createbi.ReadOnly = true;
            this.Createbi.RegistCheckMethod = null;
            this.Createbi.SetFormField = "";
            this.Createbi.Size = new System.Drawing.Size(105, 20);
            this.Createbi.TabIndex = 397;
            this.Createbi.TabStop = false;
            this.Createbi.Tag = "作成日が表示されます。";
            // 
            // labelSendDate
            // 
            this.labelSendDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.labelSendDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelSendDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelSendDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.labelSendDate.ForeColor = System.Drawing.Color.White;
            this.labelSendDate.Location = new System.Drawing.Point(600, 46);
            this.labelSendDate.Name = "labelSendDate";
            this.labelSendDate.Size = new System.Drawing.Size(110, 20);
            this.labelSendDate.TabIndex = 400;
            this.labelSendDate.Text = "送付日";
            this.labelSendDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SendDate
            // 
            this.SendDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.SendDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SendDate.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.SendDate.DefaultBackColor = System.Drawing.Color.Empty;
            this.SendDate.DisplayPopUp = null;
            this.SendDate.ErrorMessage = "";
            this.SendDate.FocusOutCheckMethod = null;
            this.SendDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SendDate.ForeColor = System.Drawing.Color.Black;
            this.SendDate.GetCodeMasterField = "";
            this.SendDate.IsInputErrorOccured = false;
            this.SendDate.Location = new System.Drawing.Point(711, 46);
            this.SendDate.MaxLength = 0;
            this.SendDate.Name = "SendDate";
            this.SendDate.PopupAfterExecute = null;
            this.SendDate.PopupBeforeExecute = null;
            this.SendDate.PopupGetMasterField = "";
            this.SendDate.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SendDate.PopupSearchSendParams")));
            this.SendDate.PopupSetFormField = "";
            this.SendDate.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SendDate.popupWindowSetting = null;
            this.SendDate.ReadOnly = true;
            this.SendDate.RegistCheckMethod = null;
            this.SendDate.SetFormField = "";
            this.SendDate.Size = new System.Drawing.Size(105, 20);
            this.SendDate.TabIndex = 399;
            this.SendDate.TabStop = false;
            this.SendDate.Tag = "送付日が表示されます。";
            // 
            // labelItakuSyoshiki
            // 
            this.labelItakuSyoshiki.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.labelItakuSyoshiki.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelItakuSyoshiki.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelItakuSyoshiki.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.labelItakuSyoshiki.ForeColor = System.Drawing.Color.White;
            this.labelItakuSyoshiki.Location = new System.Drawing.Point(329, 2);
            this.labelItakuSyoshiki.Name = "labelItakuSyoshiki";
            this.labelItakuSyoshiki.Size = new System.Drawing.Size(117, 20);
            this.labelItakuSyoshiki.TabIndex = 402;
            this.labelItakuSyoshiki.Text = "委託契約書書式";
            this.labelItakuSyoshiki.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ItakuSyoshiki
            // 
            this.ItakuSyoshiki.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ItakuSyoshiki.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ItakuSyoshiki.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ItakuSyoshiki.DefaultBackColor = System.Drawing.Color.Empty;
            this.ItakuSyoshiki.DisplayPopUp = null;
            this.ItakuSyoshiki.ErrorMessage = "";
            this.ItakuSyoshiki.FocusOutCheckMethod = null;
            this.ItakuSyoshiki.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ItakuSyoshiki.ForeColor = System.Drawing.Color.Black;
            this.ItakuSyoshiki.GetCodeMasterField = "";
            this.ItakuSyoshiki.IsInputErrorOccured = false;
            this.ItakuSyoshiki.Location = new System.Drawing.Point(447, 2);
            this.ItakuSyoshiki.MaxLength = 0;
            this.ItakuSyoshiki.Name = "ItakuSyoshiki";
            this.ItakuSyoshiki.PopupAfterExecute = null;
            this.ItakuSyoshiki.PopupBeforeExecute = null;
            this.ItakuSyoshiki.PopupGetMasterField = "";
            this.ItakuSyoshiki.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ItakuSyoshiki.PopupSearchSendParams")));
            this.ItakuSyoshiki.PopupSetFormField = "";
            this.ItakuSyoshiki.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ItakuSyoshiki.popupWindowSetting = null;
            this.ItakuSyoshiki.ReadOnly = true;
            this.ItakuSyoshiki.RegistCheckMethod = null;
            this.ItakuSyoshiki.SetFormField = "";
            this.ItakuSyoshiki.Size = new System.Drawing.Size(147, 20);
            this.ItakuSyoshiki.TabIndex = 401;
            this.ItakuSyoshiki.TabStop = false;
            this.ItakuSyoshiki.Tag = "委託契約書書式が表示されます。";
            // 
            // labelItakuShurui
            // 
            this.labelItakuShurui.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.labelItakuShurui.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelItakuShurui.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelItakuShurui.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.labelItakuShurui.ForeColor = System.Drawing.Color.White;
            this.labelItakuShurui.Location = new System.Drawing.Point(329, 24);
            this.labelItakuShurui.Name = "labelItakuShurui";
            this.labelItakuShurui.Size = new System.Drawing.Size(117, 20);
            this.labelItakuShurui.TabIndex = 404;
            this.labelItakuShurui.Text = "委託契約書種類";
            this.labelItakuShurui.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ItakuShurui
            // 
            this.ItakuShurui.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ItakuShurui.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ItakuShurui.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ItakuShurui.DefaultBackColor = System.Drawing.Color.Empty;
            this.ItakuShurui.DisplayPopUp = null;
            this.ItakuShurui.ErrorMessage = "";
            this.ItakuShurui.FocusOutCheckMethod = null;
            this.ItakuShurui.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ItakuShurui.ForeColor = System.Drawing.Color.Black;
            this.ItakuShurui.GetCodeMasterField = "";
            this.ItakuShurui.IsInputErrorOccured = false;
            this.ItakuShurui.Location = new System.Drawing.Point(447, 24);
            this.ItakuShurui.MaxLength = 0;
            this.ItakuShurui.Name = "ItakuShurui";
            this.ItakuShurui.PopupAfterExecute = null;
            this.ItakuShurui.PopupBeforeExecute = null;
            this.ItakuShurui.PopupGetMasterField = "";
            this.ItakuShurui.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ItakuShurui.PopupSearchSendParams")));
            this.ItakuShurui.PopupSetFormField = "";
            this.ItakuShurui.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ItakuShurui.popupWindowSetting = null;
            this.ItakuShurui.ReadOnly = true;
            this.ItakuShurui.RegistCheckMethod = null;
            this.ItakuShurui.SetFormField = "";
            this.ItakuShurui.Size = new System.Drawing.Size(147, 20);
            this.ItakuShurui.TabIndex = 403;
            this.ItakuShurui.TabStop = false;
            this.ItakuShurui.Tag = "委託契約書種類が表示されます。";
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 67);
            this.Controls.Add(this.labelItakuShurui);
            this.Controls.Add(this.ItakuShurui);
            this.Controls.Add(this.labelItakuSyoshiki);
            this.Controls.Add(this.ItakuSyoshiki);
            this.Controls.Add(this.labelSendDate);
            this.Controls.Add(this.SendDate);
            this.Controls.Add(this.labelCreatebi);
            this.Controls.Add(this.Createbi);
            this.Controls.Add(this.labelKeiyakuDate);
            this.Controls.Add(this.KeiyakuDate);
            this.Controls.Add(this.labelLastUpdate);
            this.Controls.Add(this.LastUpdateDate);
            this.Controls.Add(this.LastUpdateUser);
            this.Controls.Add(this.labelInitCreate);
            this.Controls.Add(this.CreateDate);
            this.Controls.Add(this.CreateUser);
            this.Name = "UIHeader";
            this.Text = "UIHeader";
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.CreateUser, 0);
            this.Controls.SetChildIndex(this.CreateDate, 0);
            this.Controls.SetChildIndex(this.labelInitCreate, 0);
            this.Controls.SetChildIndex(this.LastUpdateUser, 0);
            this.Controls.SetChildIndex(this.LastUpdateDate, 0);
            this.Controls.SetChildIndex(this.labelLastUpdate, 0);
            this.Controls.SetChildIndex(this.KeiyakuDate, 0);
            this.Controls.SetChildIndex(this.labelKeiyakuDate, 0);
            this.Controls.SetChildIndex(this.Createbi, 0);
            this.Controls.SetChildIndex(this.labelCreatebi, 0);
            this.Controls.SetChildIndex(this.SendDate, 0);
            this.Controls.SetChildIndex(this.labelSendDate, 0);
            this.Controls.SetChildIndex(this.ItakuSyoshiki, 0);
            this.Controls.SetChildIndex(this.labelItakuSyoshiki, 0);
            this.Controls.SetChildIndex(this.ItakuShurui, 0);
            this.Controls.SetChildIndex(this.labelItakuShurui, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label labelLastUpdate;
        public r_framework.CustomControl.CustomTextBox LastUpdateDate;
        public r_framework.CustomControl.CustomTextBox LastUpdateUser;
        public System.Windows.Forms.Label labelInitCreate;
        public r_framework.CustomControl.CustomTextBox CreateDate;
        public r_framework.CustomControl.CustomTextBox CreateUser;
        public System.Windows.Forms.Label labelKeiyakuDate;
        public r_framework.CustomControl.CustomTextBox KeiyakuDate;
        public System.Windows.Forms.Label labelCreatebi;
        public r_framework.CustomControl.CustomTextBox Createbi;
        public System.Windows.Forms.Label labelSendDate;
        public r_framework.CustomControl.CustomTextBox SendDate;
        public System.Windows.Forms.Label labelItakuSyoshiki;
        public r_framework.CustomControl.CustomTextBox ItakuSyoshiki;
        public System.Windows.Forms.Label labelItakuShurui;
        public r_framework.CustomControl.CustomTextBox ItakuShurui;

    }
}