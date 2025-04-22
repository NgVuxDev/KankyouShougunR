using r_framework.CustomControl;

namespace DenManiJigyoushaHoshu.APP
{
    partial class DenManiJigyoushaHoshuForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DenManiJigyoushaHoshuForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GYOUSHA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DUMY_JIGYOUSYA_KBN = new r_framework.CustomControl.CustomTextBox();
            this.JIGYOUSHA_FAX = new r_framework.CustomControl.CustomPhoneNumberTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.HOUKOKU_HUYOU_KBN = new r_framework.CustomControl.CustomCheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.SBN_KBN = new r_framework.CustomControl.CustomCheckBox();
            this.UPN_KBN = new r_framework.CustomControl.CustomCheckBox();
            this.HST_KBN = new r_framework.CustomControl.CustomCheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.JIGYOUSHA_TEL = new r_framework.CustomControl.CustomPhoneNumberTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.JIGYOUSHA_ADDRESS4 = new r_framework.CustomControl.CustomTextBox();
            this.JIGYOUSHA_ADDRESS3 = new r_framework.CustomControl.CustomTextBox();
            this.JIGYOUSHA_ADDRESS2 = new r_framework.CustomControl.CustomTextBox();
            this.JIGYOUSHA_ADDRESS1 = new r_framework.CustomControl.CustomTextBox();
            this.JIGYOUSHA_POST = new r_framework.CustomControl.CustomPostalCodeTextBox();
            this.EDI_PASSWORD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.EDI_MEMBER_ID = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.JIGYOUSHA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.GYOUSHA_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.JIGYOUSHA_POST_SEACRH_BUTTON = new r_framework.CustomControl.CustomButton();
            this.SIKUCHOUSON_SEARCH_BUTTON = new r_framework.CustomControl.CustomButton();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(18, 385);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 20);
            this.label1.TabIndex = 250;
            this.label1.Text = "将軍連携　業者";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label16.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(18, -1);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(200, 20);
            this.label16.TabIndex = 249;
            this.label16.Text = "電子マニフェスト　事業者";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.GYOUSHA_SEARCH_BUTTON);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.GYOUSHA_CD);
            this.panel2.Controls.Add(this.GYOUSHA_NAME);
            this.panel2.Location = new System.Drawing.Point(13, 395);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(975, 55);
            this.panel2.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(13, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 20);
            this.label10.TabIndex = 611;
            this.label10.Text = "業者";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GYOUSHA_CD
            // 
            this.GYOUSHA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.GYOUSHA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_CD.ChangeUpperCase = true;
            this.GYOUSHA_CD.CharacterLimitList = null;
            this.GYOUSHA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.GYOUSHA_CD.DBFieldsName = "GYOUSHA_CD";
            this.GYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_CD.DisplayItemName = "業者CD";
            this.GYOUSHA_CD.DisplayPopUp = null;
            this.GYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.FocusOutCheckMethod")));
            this.GYOUSHA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_CD.GetCodeMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GYOUSHA_CD.IsInputErrorOccured = false;
            this.GYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.GYOUSHA_CD.Location = new System.Drawing.Point(139, 20);
            this.GYOUSHA_CD.MaxLength = 6;
            this.GYOUSHA_CD.Name = "GYOUSHA_CD";
            this.GYOUSHA_CD.PopupAfterExecute = null;
            this.GYOUSHA_CD.PopupBeforeExecute = null;
            this.GYOUSHA_CD.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_CD.PopupSearchSendParams")));
            this.GYOUSHA_CD.PopupSendParams = new string[0];
            this.GYOUSHA_CD.PopupSetFormField = "GYOUSHA_CD,GYOUSHA_NAME";
            this.GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_CD.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_CD.popupWindowSetting")));
            this.GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.RegistCheckMethod")));
            this.GYOUSHA_CD.SetFormField = "GYOUSHA_CD,GYOUSHA_NAME";
            this.GYOUSHA_CD.ShortItemName = "業者CD";
            this.GYOUSHA_CD.Size = new System.Drawing.Size(55, 20);
            this.GYOUSHA_CD.TabIndex = 20;
            this.GYOUSHA_CD.Tag = "業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GYOUSHA_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.GYOUSHA_CD.ZeroPaddengFlag = true;
            // 
            // GYOUSHA_NAME
            // 
            this.GYOUSHA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GYOUSHA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_NAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.GYOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_NAME.DisplayPopUp = null;
            this.GYOUSHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME.FocusOutCheckMethod")));
            this.GYOUSHA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GYOUSHA_NAME.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_NAME.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GYOUSHA_NAME.IsInputErrorOccured = false;
            this.GYOUSHA_NAME.Location = new System.Drawing.Point(193, 20);
            this.GYOUSHA_NAME.MaxLength = 0;
            this.GYOUSHA_NAME.Name = "GYOUSHA_NAME";
            this.GYOUSHA_NAME.PopupAfterExecute = null;
            this.GYOUSHA_NAME.PopupBeforeExecute = null;
            this.GYOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_NAME.PopupSearchSendParams")));
            this.GYOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_NAME.popupWindowSetting")));
            this.GYOUSHA_NAME.ReadOnly = true;
            this.GYOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME.RegistCheckMethod")));
            this.GYOUSHA_NAME.Size = new System.Drawing.Size(290, 20);
            this.GYOUSHA_NAME.TabIndex = 21;
            this.GYOUSHA_NAME.TabStop = false;
            this.GYOUSHA_NAME.Tag = "業者名が表示されます";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.SIKUCHOUSON_SEARCH_BUTTON);
            this.panel1.Controls.Add(this.JIGYOUSHA_POST_SEACRH_BUTTON);
            this.panel1.Controls.Add(this.DUMY_JIGYOUSYA_KBN);
            this.panel1.Controls.Add(this.JIGYOUSHA_FAX);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.HOUKOKU_HUYOU_KBN);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.JIGYOUSHA_TEL);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.JIGYOUSHA_ADDRESS4);
            this.panel1.Controls.Add(this.JIGYOUSHA_ADDRESS3);
            this.panel1.Controls.Add(this.JIGYOUSHA_ADDRESS2);
            this.panel1.Controls.Add(this.JIGYOUSHA_ADDRESS1);
            this.panel1.Controls.Add(this.JIGYOUSHA_POST);
            this.panel1.Controls.Add(this.EDI_PASSWORD);
            this.panel1.Controls.Add(this.EDI_MEMBER_ID);
            this.panel1.Controls.Add(this.JIGYOUSHA_NAME);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(13, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(976, 365);
            this.panel1.TabIndex = 1;
            // 
            // DUMY_JIGYOUSYA_KBN
            // 
            this.DUMY_JIGYOUSYA_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.DUMY_JIGYOUSYA_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DUMY_JIGYOUSYA_KBN.CharactersNumber = new decimal(new int[] {
            260,
            0,
            0,
            0});
            this.DUMY_JIGYOUSYA_KBN.DBFieldsName = "";
            this.DUMY_JIGYOUSYA_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.DUMY_JIGYOUSYA_KBN.DisplayItemName = "事業者区分";
            this.DUMY_JIGYOUSYA_KBN.DisplayPopUp = null;
            this.DUMY_JIGYOUSYA_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DUMY_JIGYOUSYA_KBN.FocusOutCheckMethod")));
            this.DUMY_JIGYOUSYA_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DUMY_JIGYOUSYA_KBN.ForeColor = System.Drawing.Color.Black;
            this.DUMY_JIGYOUSYA_KBN.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.DUMY_JIGYOUSYA_KBN.IsInputErrorOccured = false;
            this.DUMY_JIGYOUSYA_KBN.ItemDefinedTypes = "varchar";
            this.DUMY_JIGYOUSYA_KBN.Location = new System.Drawing.Point(598, 285);
            this.DUMY_JIGYOUSYA_KBN.MaxLength = 260;
            this.DUMY_JIGYOUSYA_KBN.Multiline = true;
            this.DUMY_JIGYOUSYA_KBN.Name = "DUMY_JIGYOUSYA_KBN";
            this.DUMY_JIGYOUSYA_KBN.PopupAfterExecute = null;
            this.DUMY_JIGYOUSYA_KBN.PopupBeforeExecute = null;
            this.DUMY_JIGYOUSYA_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DUMY_JIGYOUSYA_KBN.PopupSearchSendParams")));
            this.DUMY_JIGYOUSYA_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DUMY_JIGYOUSYA_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DUMY_JIGYOUSYA_KBN.popupWindowSetting")));
            this.DUMY_JIGYOUSYA_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DUMY_JIGYOUSYA_KBN.RegistCheckMethod")));
            this.DUMY_JIGYOUSYA_KBN.ShortItemName = "事業者区分";
            this.DUMY_JIGYOUSYA_KBN.Size = new System.Drawing.Size(57, 22);
            this.DUMY_JIGYOUSYA_KBN.TabIndex = 628;
            this.DUMY_JIGYOUSYA_KBN.TabStop = false;
            this.DUMY_JIGYOUSYA_KBN.Tag = "";
            this.DUMY_JIGYOUSYA_KBN.Visible = false;
            // 
            // JIGYOUSHA_FAX
            // 
            this.JIGYOUSHA_FAX.BackColor = System.Drawing.SystemColors.Window;
            this.JIGYOUSHA_FAX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JIGYOUSHA_FAX.DBFieldsName = "JIGYOUSHA_FAX";
            this.JIGYOUSHA_FAX.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUSHA_FAX.DisplayItemName = "FAX番号";
            this.JIGYOUSHA_FAX.DisplayPopUp = null;
            this.JIGYOUSHA_FAX.FocusOutCheckMethod = null;
            this.JIGYOUSHA_FAX.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.JIGYOUSHA_FAX.ForeColor = System.Drawing.Color.Black;
            this.JIGYOUSHA_FAX.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.JIGYOUSHA_FAX.IsInputErrorOccured = false;
            this.JIGYOUSHA_FAX.ItemDefinedTypes = "varchar";
            this.JIGYOUSHA_FAX.Location = new System.Drawing.Point(139, 261);
            this.JIGYOUSHA_FAX.Name = "JIGYOUSHA_FAX";
            this.JIGYOUSHA_FAX.PopupAfterExecute = null;
            this.JIGYOUSHA_FAX.PopupBeforeExecute = null;
            this.JIGYOUSHA_FAX.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JIGYOUSHA_FAX.PopupSearchSendParams")));
            this.JIGYOUSHA_FAX.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JIGYOUSHA_FAX.popupWindowSetting = null;
            this.JIGYOUSHA_FAX.RegistCheckMethod = null;
            this.JIGYOUSHA_FAX.ShortItemName = "FAX番号";
            this.JIGYOUSHA_FAX.Size = new System.Drawing.Size(115, 20);
            this.JIGYOUSHA_FAX.TabIndex = 13;
            this.JIGYOUSHA_FAX.Tag = "半角数字（ハイフン可）１5文字以内で入力してください";
            this.JIGYOUSHA_FAX.UseParentheses = true;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label13.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(13, 261);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(120, 20);
            this.label13.TabIndex = 627;
            this.label13.Text = "FAX番号";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label15.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(13, 312);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(120, 20);
            this.label15.TabIndex = 626;
            this.label15.Text = "報告不要業者";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HOUKOKU_HUYOU_KBN
            // 
            this.HOUKOKU_HUYOU_KBN.AutoSize = true;
            this.HOUKOKU_HUYOU_KBN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.HOUKOKU_HUYOU_KBN.DBFieldsName = "HOUKOKU_HUYOU_KBN";
            this.HOUKOKU_HUYOU_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.HOUKOKU_HUYOU_KBN.DisplayItemName = "報告不要業者";
            this.HOUKOKU_HUYOU_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOUKOKU_HUYOU_KBN.FocusOutCheckMethod")));
            this.HOUKOKU_HUYOU_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HOUKOKU_HUYOU_KBN.ItemDefinedTypes = "bit";
            this.HOUKOKU_HUYOU_KBN.Location = new System.Drawing.Point(143, 315);
            this.HOUKOKU_HUYOU_KBN.Name = "HOUKOKU_HUYOU_KBN";
            this.HOUKOKU_HUYOU_KBN.PopupAfterExecute = null;
            this.HOUKOKU_HUYOU_KBN.PopupBeforeExecute = null;
            this.HOUKOKU_HUYOU_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HOUKOKU_HUYOU_KBN.PopupSearchSendParams")));
            this.HOUKOKU_HUYOU_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HOUKOKU_HUYOU_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HOUKOKU_HUYOU_KBN.popupWindowSetting")));
            this.HOUKOKU_HUYOU_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOUKOKU_HUYOU_KBN.RegistCheckMethod")));
            this.HOUKOKU_HUYOU_KBN.ShortItemName = "報告不要業者";
            this.HOUKOKU_HUYOU_KBN.Size = new System.Drawing.Size(15, 14);
            this.HOUKOKU_HUYOU_KBN.TabIndex = 18;
            this.HOUKOKU_HUYOU_KBN.Tag = "報告不要業者の場合にはチェックを付けてください";
            this.HOUKOKU_HUYOU_KBN.UseVisualStyleBackColor = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.SBN_KBN);
            this.panel3.Controls.Add(this.UPN_KBN);
            this.panel3.Controls.Add(this.HST_KBN);
            this.panel3.Location = new System.Drawing.Point(139, 285);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(344, 22);
            this.panel3.TabIndex = 14;
            // 
            // SBN_KBN
            // 
            this.SBN_KBN.AutoSize = true;
            this.SBN_KBN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.SBN_KBN.DBFieldsName = "SBN_KBN";
            this.SBN_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.SBN_KBN.DisplayItemName = "処分事業者";
            this.SBN_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SBN_KBN.FocusOutCheckMethod")));
            this.SBN_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SBN_KBN.ItemDefinedTypes = "bit";
            this.SBN_KBN.Location = new System.Drawing.Point(221, 3);
            this.SBN_KBN.Name = "SBN_KBN";
            this.SBN_KBN.PopupAfterExecute = null;
            this.SBN_KBN.PopupBeforeExecute = null;
            this.SBN_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SBN_KBN.PopupSearchSendParams")));
            this.SBN_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SBN_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SBN_KBN.popupWindowSetting")));
            this.SBN_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SBN_KBN.RegistCheckMethod")));
            this.SBN_KBN.ShortItemName = "";
            this.SBN_KBN.Size = new System.Drawing.Size(96, 17);
            this.SBN_KBN.TabIndex = 17;
            this.SBN_KBN.Tag = "処分事業者の場合にはチェックを付けてください";
            this.SBN_KBN.Text = "処分事業者";
            this.SBN_KBN.UseVisualStyleBackColor = false;
            this.SBN_KBN.CheckedChanged += new System.EventHandler(this.JigyousyaKbnChanged);
            // 
            // UPN_KBN
            // 
            this.UPN_KBN.AutoSize = true;
            this.UPN_KBN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.UPN_KBN.DBFieldsName = "UPN_KBN";
            this.UPN_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.UPN_KBN.DisplayItemName = "収集運搬業者";
            this.UPN_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPN_KBN.FocusOutCheckMethod")));
            this.UPN_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UPN_KBN.ItemDefinedTypes = "bit";
            this.UPN_KBN.Location = new System.Drawing.Point(105, 3);
            this.UPN_KBN.Name = "UPN_KBN";
            this.UPN_KBN.PopupAfterExecute = null;
            this.UPN_KBN.PopupBeforeExecute = null;
            this.UPN_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPN_KBN.PopupSearchSendParams")));
            this.UPN_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPN_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPN_KBN.popupWindowSetting")));
            this.UPN_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPN_KBN.RegistCheckMethod")));
            this.UPN_KBN.ShortItemName = "";
            this.UPN_KBN.Size = new System.Drawing.Size(110, 17);
            this.UPN_KBN.TabIndex = 16;
            this.UPN_KBN.Tag = "収集運搬業者の場合にはチェックを付けてください";
            this.UPN_KBN.Text = "収集運搬業者";
            this.UPN_KBN.UseVisualStyleBackColor = false;
            this.UPN_KBN.CheckedChanged += new System.EventHandler(this.JigyousyaKbnChanged);
            // 
            // HST_KBN
            // 
            this.HST_KBN.AutoSize = true;
            this.HST_KBN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.HST_KBN.DBFieldsName = "HST_KBN";
            this.HST_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.HST_KBN.DisplayItemName = "排出事業者";
            this.HST_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HST_KBN.FocusOutCheckMethod")));
            this.HST_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HST_KBN.ItemDefinedTypes = "bit";
            this.HST_KBN.Location = new System.Drawing.Point(3, 3);
            this.HST_KBN.Name = "HST_KBN";
            this.HST_KBN.PopupAfterExecute = null;
            this.HST_KBN.PopupBeforeExecute = null;
            this.HST_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HST_KBN.PopupSearchSendParams")));
            this.HST_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HST_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HST_KBN.popupWindowSetting")));
            this.HST_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HST_KBN.RegistCheckMethod")));
            this.HST_KBN.ShortItemName = "";
            this.HST_KBN.Size = new System.Drawing.Size(96, 17);
            this.HST_KBN.TabIndex = 15;
            this.HST_KBN.Tag = "排出事業者の場合にはチェックを付けてください";
            this.HST_KBN.Text = "排出事業者";
            this.HST_KBN.UseVisualStyleBackColor = false;
            this.HST_KBN.CheckedChanged += new System.EventHandler(this.JigyousyaKbnChanged);
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(13, 285);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(120, 20);
            this.label12.TabIndex = 619;
            this.label12.Text = "事業者区分";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // JIGYOUSHA_TEL
            // 
            this.JIGYOUSHA_TEL.BackColor = System.Drawing.SystemColors.Window;
            this.JIGYOUSHA_TEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JIGYOUSHA_TEL.DBFieldsName = "JIGYOUSHA_TEL";
            this.JIGYOUSHA_TEL.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUSHA_TEL.DisplayItemName = "電話番号";
            this.JIGYOUSHA_TEL.DisplayPopUp = null;
            this.JIGYOUSHA_TEL.FocusOutCheckMethod = null;
            this.JIGYOUSHA_TEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.JIGYOUSHA_TEL.ForeColor = System.Drawing.Color.Black;
            this.JIGYOUSHA_TEL.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.JIGYOUSHA_TEL.IsInputErrorOccured = false;
            this.JIGYOUSHA_TEL.ItemDefinedTypes = "varchar";
            this.JIGYOUSHA_TEL.Location = new System.Drawing.Point(139, 237);
            this.JIGYOUSHA_TEL.Name = "JIGYOUSHA_TEL";
            this.JIGYOUSHA_TEL.PopupAfterExecute = null;
            this.JIGYOUSHA_TEL.PopupBeforeExecute = null;
            this.JIGYOUSHA_TEL.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JIGYOUSHA_TEL.PopupSearchSendParams")));
            this.JIGYOUSHA_TEL.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JIGYOUSHA_TEL.popupWindowSetting = null;
            this.JIGYOUSHA_TEL.RegistCheckMethod = null;
            this.JIGYOUSHA_TEL.ShortItemName = "電話番号";
            this.JIGYOUSHA_TEL.Size = new System.Drawing.Size(115, 20);
            this.JIGYOUSHA_TEL.TabIndex = 12;
            this.JIGYOUSHA_TEL.Tag = "半角数字（ハイフン可）１5文字以内で入力してください";
            this.JIGYOUSHA_TEL.UseParentheses = true;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label11.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(13, 237);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(120, 20);
            this.label11.TabIndex = 617;
            this.label11.Text = "電話番号";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // JIGYOUSHA_ADDRESS4
            // 
            this.JIGYOUSHA_ADDRESS4.BackColor = System.Drawing.SystemColors.Window;
            this.JIGYOUSHA_ADDRESS4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JIGYOUSHA_ADDRESS4.CharactersNumber = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.JIGYOUSHA_ADDRESS4.DBFieldsName = "JIGYOUSHA_ADDRESS4";
            this.JIGYOUSHA_ADDRESS4.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUSHA_ADDRESS4.DisplayItemName = "詳細住所";
            this.JIGYOUSHA_ADDRESS4.DisplayPopUp = null;
            this.JIGYOUSHA_ADDRESS4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_ADDRESS4.FocusOutCheckMethod")));
            this.JIGYOUSHA_ADDRESS4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.JIGYOUSHA_ADDRESS4.ForeColor = System.Drawing.Color.Black;
            this.JIGYOUSHA_ADDRESS4.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.JIGYOUSHA_ADDRESS4.IsInputErrorOccured = false;
            this.JIGYOUSHA_ADDRESS4.ItemDefinedTypes = "varchar";
            this.JIGYOUSHA_ADDRESS4.Location = new System.Drawing.Point(139, 213);
            this.JIGYOUSHA_ADDRESS4.MaxLength = 100;
            this.JIGYOUSHA_ADDRESS4.Name = "JIGYOUSHA_ADDRESS4";
            this.JIGYOUSHA_ADDRESS4.PopupAfterExecute = null;
            this.JIGYOUSHA_ADDRESS4.PopupBeforeExecute = null;
            this.JIGYOUSHA_ADDRESS4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JIGYOUSHA_ADDRESS4.PopupSearchSendParams")));
            this.JIGYOUSHA_ADDRESS4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JIGYOUSHA_ADDRESS4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JIGYOUSHA_ADDRESS4.popupWindowSetting")));
            this.JIGYOUSHA_ADDRESS4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_ADDRESS4.RegistCheckMethod")));
            this.JIGYOUSHA_ADDRESS4.ShortItemName = "詳細住所";
            this.JIGYOUSHA_ADDRESS4.Size = new System.Drawing.Size(710, 20);
            this.JIGYOUSHA_ADDRESS4.TabIndex = 11;
            this.JIGYOUSHA_ADDRESS4.Tag = "全角５０文字以内で入力してください";
            // 
            // JIGYOUSHA_ADDRESS3
            // 
            this.JIGYOUSHA_ADDRESS3.BackColor = System.Drawing.SystemColors.Window;
            this.JIGYOUSHA_ADDRESS3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JIGYOUSHA_ADDRESS3.ChangeWideCase = true;
            this.JIGYOUSHA_ADDRESS3.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.JIGYOUSHA_ADDRESS3.DBFieldsName = "JIGYOUSHA_ADDRESS3";
            this.JIGYOUSHA_ADDRESS3.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUSHA_ADDRESS3.DisplayItemName = "町域";
            this.JIGYOUSHA_ADDRESS3.DisplayPopUp = null;
            this.JIGYOUSHA_ADDRESS3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_ADDRESS3.FocusOutCheckMethod")));
            this.JIGYOUSHA_ADDRESS3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.JIGYOUSHA_ADDRESS3.ForeColor = System.Drawing.Color.Black;
            this.JIGYOUSHA_ADDRESS3.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.JIGYOUSHA_ADDRESS3.IsInputErrorOccured = false;
            this.JIGYOUSHA_ADDRESS3.ItemDefinedTypes = "varchar";
            this.JIGYOUSHA_ADDRESS3.Location = new System.Drawing.Point(139, 189);
            this.JIGYOUSHA_ADDRESS3.MaxLength = 80;
            this.JIGYOUSHA_ADDRESS3.Name = "JIGYOUSHA_ADDRESS3";
            this.JIGYOUSHA_ADDRESS3.PopupAfterExecute = null;
            this.JIGYOUSHA_ADDRESS3.PopupBeforeExecute = null;
            this.JIGYOUSHA_ADDRESS3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JIGYOUSHA_ADDRESS3.PopupSearchSendParams")));
            this.JIGYOUSHA_ADDRESS3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JIGYOUSHA_ADDRESS3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JIGYOUSHA_ADDRESS3.popupWindowSetting")));
            this.JIGYOUSHA_ADDRESS3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_ADDRESS3.RegistCheckMethod")));
            this.JIGYOUSHA_ADDRESS3.ShortItemName = "町域";
            this.JIGYOUSHA_ADDRESS3.Size = new System.Drawing.Size(570, 20);
            this.JIGYOUSHA_ADDRESS3.TabIndex = 10;
            this.JIGYOUSHA_ADDRESS3.Tag = "全角４０文字以内で入力してください";
            // 
            // JIGYOUSHA_ADDRESS2
            // 
            this.JIGYOUSHA_ADDRESS2.BackColor = System.Drawing.SystemColors.Window;
            this.JIGYOUSHA_ADDRESS2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JIGYOUSHA_ADDRESS2.ChangeWideCase = true;
            this.JIGYOUSHA_ADDRESS2.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.JIGYOUSHA_ADDRESS2.DBFieldsName = "JIGYOUSHA_ADDRESS2";
            this.JIGYOUSHA_ADDRESS2.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUSHA_ADDRESS2.DisplayItemName = "市区町村";
            this.JIGYOUSHA_ADDRESS2.DisplayPopUp = null;
            this.JIGYOUSHA_ADDRESS2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_ADDRESS2.FocusOutCheckMethod")));
            this.JIGYOUSHA_ADDRESS2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.JIGYOUSHA_ADDRESS2.ForeColor = System.Drawing.Color.Black;
            this.JIGYOUSHA_ADDRESS2.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.JIGYOUSHA_ADDRESS2.IsInputErrorOccured = false;
            this.JIGYOUSHA_ADDRESS2.ItemDefinedTypes = "varchar";
            this.JIGYOUSHA_ADDRESS2.Location = new System.Drawing.Point(139, 165);
            this.JIGYOUSHA_ADDRESS2.MaxLength = 40;
            this.JIGYOUSHA_ADDRESS2.Name = "JIGYOUSHA_ADDRESS2";
            this.JIGYOUSHA_ADDRESS2.PopupAfterExecute = null;
            this.JIGYOUSHA_ADDRESS2.PopupBeforeExecute = null;
            this.JIGYOUSHA_ADDRESS2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JIGYOUSHA_ADDRESS2.PopupSearchSendParams")));
            this.JIGYOUSHA_ADDRESS2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JIGYOUSHA_ADDRESS2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JIGYOUSHA_ADDRESS2.popupWindowSetting")));
            this.JIGYOUSHA_ADDRESS2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_ADDRESS2.RegistCheckMethod")));
            this.JIGYOUSHA_ADDRESS2.ShortItemName = "市区町村";
            this.JIGYOUSHA_ADDRESS2.Size = new System.Drawing.Size(290, 20);
            this.JIGYOUSHA_ADDRESS2.TabIndex = 8;
            this.JIGYOUSHA_ADDRESS2.Tag = "全角２０文字以内で入力してください";
            // 
            // JIGYOUSHA_ADDRESS1
            // 
            this.JIGYOUSHA_ADDRESS1.BackColor = System.Drawing.SystemColors.Window;
            this.JIGYOUSHA_ADDRESS1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JIGYOUSHA_ADDRESS1.ChangeWideCase = true;
            this.JIGYOUSHA_ADDRESS1.CharactersNumber = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.JIGYOUSHA_ADDRESS1.DBFieldsName = "JIGYOUSHA_ADDRESS1";
            this.JIGYOUSHA_ADDRESS1.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUSHA_ADDRESS1.DisplayItemName = "都道府県";
            this.JIGYOUSHA_ADDRESS1.DisplayPopUp = null;
            this.JIGYOUSHA_ADDRESS1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_ADDRESS1.FocusOutCheckMethod")));
            this.JIGYOUSHA_ADDRESS1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.JIGYOUSHA_ADDRESS1.ForeColor = System.Drawing.Color.Black;
            this.JIGYOUSHA_ADDRESS1.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.JIGYOUSHA_ADDRESS1.IsInputErrorOccured = false;
            this.JIGYOUSHA_ADDRESS1.ItemDefinedTypes = "varchar";
            this.JIGYOUSHA_ADDRESS1.Location = new System.Drawing.Point(139, 141);
            this.JIGYOUSHA_ADDRESS1.MaxLength = 8;
            this.JIGYOUSHA_ADDRESS1.Name = "JIGYOUSHA_ADDRESS1";
            this.JIGYOUSHA_ADDRESS1.PopupAfterExecute = null;
            this.JIGYOUSHA_ADDRESS1.PopupBeforeExecute = null;
            this.JIGYOUSHA_ADDRESS1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JIGYOUSHA_ADDRESS1.PopupSearchSendParams")));
            this.JIGYOUSHA_ADDRESS1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JIGYOUSHA_ADDRESS1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JIGYOUSHA_ADDRESS1.popupWindowSetting")));
            this.JIGYOUSHA_ADDRESS1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_ADDRESS1.RegistCheckMethod")));
            this.JIGYOUSHA_ADDRESS1.ShortItemName = "都道府県";
            this.JIGYOUSHA_ADDRESS1.Size = new System.Drawing.Size(65, 20);
            this.JIGYOUSHA_ADDRESS1.TabIndex = 7;
            this.JIGYOUSHA_ADDRESS1.Tag = "全角４文字以内で入力してください";
            // 
            // JIGYOUSHA_POST
            // 
            this.JIGYOUSHA_POST.BackColor = System.Drawing.SystemColors.Window;
            this.JIGYOUSHA_POST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JIGYOUSHA_POST.DBFieldsName = "JIGYOUSHA_POST";
            this.JIGYOUSHA_POST.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUSHA_POST.DisplayItemName = "郵便番号";
            this.JIGYOUSHA_POST.DisplayPopUp = null;
            this.JIGYOUSHA_POST.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_POST.FocusOutCheckMethod")));
            this.JIGYOUSHA_POST.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.JIGYOUSHA_POST.ForeColor = System.Drawing.Color.Black;
            this.JIGYOUSHA_POST.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.JIGYOUSHA_POST.IsInputErrorOccured = false;
            this.JIGYOUSHA_POST.ItemDefinedTypes = "varchar";
            this.JIGYOUSHA_POST.Location = new System.Drawing.Point(139, 117);
            this.JIGYOUSHA_POST.Name = "JIGYOUSHA_POST";
            this.JIGYOUSHA_POST.PopupAfterExecute = null;
            this.JIGYOUSHA_POST.PopupBeforeExecute = null;
            this.JIGYOUSHA_POST.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JIGYOUSHA_POST.PopupSearchSendParams")));
            this.JIGYOUSHA_POST.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JIGYOUSHA_POST.popupWindowSetting = null;
            this.JIGYOUSHA_POST.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_POST.RegistCheckMethod")));
            this.JIGYOUSHA_POST.ShortItemName = "郵便番号";
            this.JIGYOUSHA_POST.Size = new System.Drawing.Size(65, 20);
            this.JIGYOUSHA_POST.TabIndex = 5;
            this.JIGYOUSHA_POST.Tag = "郵便番号を指定してください";
            // 
            // EDI_PASSWORD
            // 
            this.EDI_PASSWORD.AlphabetLimitFlag = false;
            this.EDI_PASSWORD.BackColor = System.Drawing.SystemColors.Window;
            this.EDI_PASSWORD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EDI_PASSWORD.CharacterLimitList = null;
            this.EDI_PASSWORD.CharactersNumber = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.EDI_PASSWORD.DBFieldsName = "EDI_PASSWORD";
            this.EDI_PASSWORD.DefaultBackColor = System.Drawing.Color.Empty;
            this.EDI_PASSWORD.DisplayItemName = "EDI利用確認キー";
            this.EDI_PASSWORD.DisplayPopUp = null;
            this.EDI_PASSWORD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("EDI_PASSWORD.FocusOutCheckMethod")));
            this.EDI_PASSWORD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.EDI_PASSWORD.ForeColor = System.Drawing.Color.Black;
            this.EDI_PASSWORD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.EDI_PASSWORD.IsInputErrorOccured = false;
            this.EDI_PASSWORD.ItemDefinedTypes = "varchar";
            this.EDI_PASSWORD.Location = new System.Drawing.Point(139, 41);
            this.EDI_PASSWORD.MaxLength = 8;
            this.EDI_PASSWORD.Name = "EDI_PASSWORD";
            this.EDI_PASSWORD.PopupAfterExecute = null;
            this.EDI_PASSWORD.PopupBeforeExecute = null;
            this.EDI_PASSWORD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("EDI_PASSWORD.PopupSearchSendParams")));
            this.EDI_PASSWORD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.EDI_PASSWORD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("EDI_PASSWORD.popupWindowSetting")));
            this.EDI_PASSWORD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("EDI_PASSWORD.RegistCheckMethod")));
            this.EDI_PASSWORD.ShortItemName = "EDI利用確認キー";
            this.EDI_PASSWORD.Size = new System.Drawing.Size(65, 20);
            this.EDI_PASSWORD.TabIndex = 3;
            this.EDI_PASSWORD.Tag = "半角８文字以内で入力してください";
            // 
            // EDI_MEMBER_ID
            // 
            this.EDI_MEMBER_ID.BackColor = System.Drawing.SystemColors.Window;
            this.EDI_MEMBER_ID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EDI_MEMBER_ID.ChangeUpperCase = true;
            this.EDI_MEMBER_ID.CharacterLimitList = null;
            this.EDI_MEMBER_ID.CharactersNumber = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.EDI_MEMBER_ID.DBFieldsName = "EDI_MEMBER_ID";
            this.EDI_MEMBER_ID.DefaultBackColor = System.Drawing.Color.Empty;
            this.EDI_MEMBER_ID.DisplayItemName = "加入者番号";
            this.EDI_MEMBER_ID.DisplayPopUp = null;
            this.EDI_MEMBER_ID.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("EDI_MEMBER_ID.FocusOutCheckMethod")));
            this.EDI_MEMBER_ID.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.EDI_MEMBER_ID.ForeColor = System.Drawing.Color.Black;
            this.EDI_MEMBER_ID.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.EDI_MEMBER_ID.IsInputErrorOccured = false;
            this.EDI_MEMBER_ID.ItemDefinedTypes = "varchar";
            this.EDI_MEMBER_ID.Location = new System.Drawing.Point(139, 17);
            this.EDI_MEMBER_ID.MaxLength = 7;
            this.EDI_MEMBER_ID.Name = "EDI_MEMBER_ID";
            this.EDI_MEMBER_ID.PopupAfterExecute = null;
            this.EDI_MEMBER_ID.PopupBeforeExecute = null;
            this.EDI_MEMBER_ID.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("EDI_MEMBER_ID.PopupSearchSendParams")));
            this.EDI_MEMBER_ID.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.EDI_MEMBER_ID.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("EDI_MEMBER_ID.popupWindowSetting")));
            this.EDI_MEMBER_ID.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("EDI_MEMBER_ID.RegistCheckMethod")));
            this.EDI_MEMBER_ID.ShortItemName = "加入者番号";
            this.EDI_MEMBER_ID.Size = new System.Drawing.Size(65, 20);
            this.EDI_MEMBER_ID.TabIndex = 2;
            this.EDI_MEMBER_ID.Tag = "半角７文字以内で入力してください";
            this.EDI_MEMBER_ID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.EDI_MEMBER_ID.ZeroPaddengFlag = true;
            this.EDI_MEMBER_ID.Validated += new System.EventHandler(this.EDI_MEMBER_ID_Validated);
            // 
            // JIGYOUSHA_NAME
            // 
            this.JIGYOUSHA_NAME.BackColor = System.Drawing.SystemColors.Window;
            this.JIGYOUSHA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JIGYOUSHA_NAME.CharactersNumber = new decimal(new int[] {
            260,
            0,
            0,
            0});
            this.JIGYOUSHA_NAME.DBFieldsName = "JIGYOUSHA_NAME";
            this.JIGYOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUSHA_NAME.DisplayItemName = "事業者名称";
            this.JIGYOUSHA_NAME.DisplayPopUp = null;
            this.JIGYOUSHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_NAME.FocusOutCheckMethod")));
            this.JIGYOUSHA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.JIGYOUSHA_NAME.ForeColor = System.Drawing.Color.Black;
            this.JIGYOUSHA_NAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.JIGYOUSHA_NAME.IsInputErrorOccured = false;
            this.JIGYOUSHA_NAME.ItemDefinedTypes = "varchar";
            this.JIGYOUSHA_NAME.Location = new System.Drawing.Point(139, 65);
            this.JIGYOUSHA_NAME.MaxLength = 260;
            this.JIGYOUSHA_NAME.Multiline = true;
            this.JIGYOUSHA_NAME.Name = "JIGYOUSHA_NAME";
            this.JIGYOUSHA_NAME.PopupAfterExecute = null;
            this.JIGYOUSHA_NAME.PopupBeforeExecute = null;
            this.JIGYOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JIGYOUSHA_NAME.PopupSearchSendParams")));
            this.JIGYOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JIGYOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JIGYOUSHA_NAME.popupWindowSetting")));
            this.JIGYOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_NAME.RegistCheckMethod")));
            this.JIGYOUSHA_NAME.ShortItemName = "事業者名称";
            this.JIGYOUSHA_NAME.Size = new System.Drawing.Size(710, 48);
            this.JIGYOUSHA_NAME.TabIndex = 4;
            this.JIGYOUSHA_NAME.Tag = "全角１３０文字以内で入力してください";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(13, 213);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(120, 20);
            this.label9.TabIndex = 253;
            this.label9.Text = "詳細住所";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(13, 189);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(120, 20);
            this.label8.TabIndex = 252;
            this.label8.Text = "町域";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(13, 165);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 20);
            this.label7.TabIndex = 251;
            this.label7.Text = "市区町村";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(13, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 20);
            this.label6.TabIndex = 250;
            this.label6.Text = "都道府県";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(13, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 20);
            this.label5.TabIndex = 249;
            this.label5.Text = "郵便番号";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(13, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 20);
            this.label4.TabIndex = 248;
            this.label4.Text = "事業者名称";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(13, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 20);
            this.label3.TabIndex = 247;
            this.label3.Text = "EDI利用確認キー";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(13, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 20);
            this.label2.TabIndex = 246;
            this.label2.Text = "加入者番号";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GYOUSHA_SEARCH_BUTTON
            // 
            this.GYOUSHA_SEARCH_BUTTON.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.GYOUSHA_SEARCH_BUTTON.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.GYOUSHA_SEARCH_BUTTON.DBFieldsName = null;
            this.GYOUSHA_SEARCH_BUTTON.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_SEARCH_BUTTON.DisplayItemName = null;
            this.GYOUSHA_SEARCH_BUTTON.DisplayPopUp = null;
            this.GYOUSHA_SEARCH_BUTTON.ErrorMessage = null;
            this.GYOUSHA_SEARCH_BUTTON.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.FocusOutCheckMethod")));
            this.GYOUSHA_SEARCH_BUTTON.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GYOUSHA_SEARCH_BUTTON.GetCodeMasterField = null;
            this.GYOUSHA_SEARCH_BUTTON.Image = ((System.Drawing.Image)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.Image")));
            this.GYOUSHA_SEARCH_BUTTON.ItemDefinedTypes = null;
            this.GYOUSHA_SEARCH_BUTTON.LinkedSettingTextBox = null;
            this.GYOUSHA_SEARCH_BUTTON.LinkedTextBoxs = null;
            this.GYOUSHA_SEARCH_BUTTON.Location = new System.Drawing.Point(486, 19);
            this.GYOUSHA_SEARCH_BUTTON.Name = "GYOUSHA_SEARCH_BUTTON";
            this.GYOUSHA_SEARCH_BUTTON.PopupAfterExecute = null;
            this.GYOUSHA_SEARCH_BUTTON.PopupAfterExecuteMethod = "";
            this.GYOUSHA_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.GYOUSHA_SEARCH_BUTTON.PopupBeforeExecuteMethod = "";
            this.GYOUSHA_SEARCH_BUTTON.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams")));
            this.GYOUSHA_SEARCH_BUTTON.PopupSetFormField = "GYOUSHA_CD,GYOUSHA_NAME";
            this.GYOUSHA_SEARCH_BUTTON.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_SEARCH_BUTTON.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_SEARCH_BUTTON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.popupWindowSetting")));
            this.GYOUSHA_SEARCH_BUTTON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.RegistCheckMethod")));
            this.GYOUSHA_SEARCH_BUTTON.SearchDisplayFlag = 0;
            this.GYOUSHA_SEARCH_BUTTON.SetFormField = null;
            this.GYOUSHA_SEARCH_BUTTON.ShortItemName = null;
            this.GYOUSHA_SEARCH_BUTTON.Size = new System.Drawing.Size(22, 22);
            this.GYOUSHA_SEARCH_BUTTON.TabIndex = 649;
            this.GYOUSHA_SEARCH_BUTTON.TabStop = false;
            this.GYOUSHA_SEARCH_BUTTON.UseVisualStyleBackColor = false;
            this.GYOUSHA_SEARCH_BUTTON.ZeroPaddengFlag = false;
            // 
            // JIGYOUSHA_POST_SEACRH_BUTTON
            // 
            this.JIGYOUSHA_POST_SEACRH_BUTTON.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUSHA_POST_SEACRH_BUTTON.Location = new System.Drawing.Point(210, 117);
            this.JIGYOUSHA_POST_SEACRH_BUTTON.Name = "JIGYOUSHA_POST_SEACRH_BUTTON";
            this.JIGYOUSHA_POST_SEACRH_BUTTON.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.JIGYOUSHA_POST_SEACRH_BUTTON.Size = new System.Drawing.Size(86, 20);
            this.JIGYOUSHA_POST_SEACRH_BUTTON.TabIndex = 6;
            this.JIGYOUSHA_POST_SEACRH_BUTTON.Tag = "指定した郵便番号から住所検索画面を表示します";
            this.JIGYOUSHA_POST_SEACRH_BUTTON.Text = "住所参照";
            this.JIGYOUSHA_POST_SEACRH_BUTTON.UseVisualStyleBackColor = true;
            this.JIGYOUSHA_POST_SEACRH_BUTTON.Click += new System.EventHandler(this.JIGYOUSHA_POST_SEACRH_BUTTON_Click);
            // 
            // SIKUCHOUSON_SEARCH_BUTTON
            // 
            this.SIKUCHOUSON_SEARCH_BUTTON.DefaultBackColor = System.Drawing.Color.Empty;
            this.SIKUCHOUSON_SEARCH_BUTTON.Image = ((System.Drawing.Image)(resources.GetObject("SIKUCHOUSON_SEARCH_BUTTON.Image")));
            this.SIKUCHOUSON_SEARCH_BUTTON.Location = new System.Drawing.Point(433, 164);
            this.SIKUCHOUSON_SEARCH_BUTTON.Name = "SIKUCHOUSON_SEARCH_BUTTON";
            this.SIKUCHOUSON_SEARCH_BUTTON.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.SIKUCHOUSON_SEARCH_BUTTON.Size = new System.Drawing.Size(22, 22);
            this.SIKUCHOUSON_SEARCH_BUTTON.TabIndex = 9;
            this.SIKUCHOUSON_SEARCH_BUTTON.Tag = "指定した都道府県、市区町村から住所検索画面を表示します";
            this.SIKUCHOUSON_SEARCH_BUTTON.UseVisualStyleBackColor = true;
            this.SIKUCHOUSON_SEARCH_BUTTON.Click += new System.EventHandler(this.SIKUCHOUSON_SEARCH_BUTTON_Click);
            // 
            // DenManiJigyoushaHoshuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScroll = false;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(999, 530);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "DenManiJigyoushaHoshuForm";
            this.ProcessKbn = r_framework.Const.PROCESS_KBN.NEW;
            this.UserRegistCheck += new r_framework.APP.Base.SuperForm.UserRegistCheckHandler(this.DenManiJigyoushaHoshuForm_UserRegistCheck);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        internal r_framework.CustomControl.CustomAlphaNumTextBox EDI_MEMBER_ID;
        internal CustomAlphaNumTextBox EDI_PASSWORD;
        internal r_framework.CustomControl.CustomTextBox JIGYOUSHA_NAME;
        internal r_framework.CustomControl.CustomPostalCodeTextBox JIGYOUSHA_POST;
        internal r_framework.CustomControl.CustomTextBox JIGYOUSHA_ADDRESS1;
        internal r_framework.CustomControl.CustomTextBox JIGYOUSHA_ADDRESS2;
        internal r_framework.CustomControl.CustomTextBox JIGYOUSHA_ADDRESS3;
        internal r_framework.CustomControl.CustomTextBox JIGYOUSHA_ADDRESS4;
        internal r_framework.CustomControl.CustomPhoneNumberTextBox JIGYOUSHA_TEL;
        internal r_framework.CustomControl.CustomPhoneNumberTextBox JIGYOUSHA_FAX;
        internal r_framework.CustomControl.CustomCheckBox HST_KBN;
        internal r_framework.CustomControl.CustomCheckBox UPN_KBN;
        internal r_framework.CustomControl.CustomCheckBox SBN_KBN;
        internal r_framework.CustomControl.CustomCheckBox HOUKOKU_HUYOU_KBN;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GYOUSHA_CD;
        internal r_framework.CustomControl.CustomTextBox GYOUSHA_NAME;
        internal CustomTextBox DUMY_JIGYOUSYA_KBN;
        internal CustomPopupOpenButton GYOUSHA_SEARCH_BUTTON;
        internal CustomButton JIGYOUSHA_POST_SEACRH_BUTTON;
        internal CustomButton SIKUCHOUSON_SEARCH_BUTTON;
    }
}
