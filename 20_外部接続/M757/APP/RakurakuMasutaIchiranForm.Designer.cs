using r_framework.Dto;

namespace Shougun.Core.ExternalConnection.RakurakuMasutaIchiran.APP
{
    partial class RakurakuMasutaIchiranForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RakurakuMasutaIchiranForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.label06 = new System.Windows.Forms.Label();
            this.GYOUSHA_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.GYOUSHA_RNAME = new r_framework.CustomControl.CustomTextBox();
            this.GYOUSHA_LABEL = new System.Windows.Forms.Label();
            this.GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.TORIHIKISAKI_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.TORIHIKISAKI_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.TORIHIKISAKI_LABEL = new System.Windows.Forms.Label();
            this.TORIHIKISAKI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GENBA_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.GENBA_RNAME = new r_framework.CustomControl.CustomTextBox();
            this.GENBA_LABEL = new System.Windows.Forms.Label();
            this.GENBA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.panel13 = new System.Windows.Forms.Panel();
            this.SEIKYUU_DAIHYOU_PRINT_KBN_2 = new r_framework.CustomControl.CustomRadioButton();
            this.SEIKYUU_DAIHYOU_PRINT_KBN_1 = new r_framework.CustomControl.CustomRadioButton();
            this.label23 = new System.Windows.Forms.Label();
            this.cb_shimebi = new r_framework.CustomControl.CustomComboBox();
            this.lb_shimebi1 = new System.Windows.Forms.Label();
            this.customPanel2 = new r_framework.CustomControl.CustomPanel();
            this.rad9 = new r_framework.CustomControl.CustomRadioButton();
            this.rad3 = new r_framework.CustomControl.CustomRadioButton();
            this.SEIKYUU_SHO_SHOSHIKI_1 = new r_framework.CustomControl.CustomNumericTextBox2();
            this.rad2 = new r_framework.CustomControl.CustomRadioButton();
            this.rad1 = new r_framework.CustomControl.CustomRadioButton();
            this.RAKURAKU_CSV_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.panel13.SuspendLayout();
            this.customPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.searchString.Location = new System.Drawing.Point(3, 2);
            this.searchString.ReadOnly = true;
            this.searchString.Size = new System.Drawing.Size(997, 127);
            this.searchString.TabIndex = 121;
            this.searchString.TabStop = false;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Location = new System.Drawing.Point(4, 421);
            this.bt_ptn1.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn1.TabIndex = 201;
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Location = new System.Drawing.Point(204, 421);
            this.bt_ptn2.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn2.TabIndex = 202;
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Location = new System.Drawing.Point(404, 421);
            this.bt_ptn3.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn3.TabIndex = 203;
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Location = new System.Drawing.Point(604, 421);
            this.bt_ptn4.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn4.TabIndex = 204;
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Location = new System.Drawing.Point(804, 421);
            this.bt_ptn5.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn5.TabIndex = 205;
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.customSortHeader1.Location = new System.Drawing.Point(4, 152);
            this.customSortHeader1.Size = new System.Drawing.Size(994, 23);
            this.customSortHeader1.TabIndex = 122;
            // 
            // customSearchHeader1
            // 
            this.customSearchHeader1.Location = new System.Drawing.Point(4, 129);
            this.customSearchHeader1.TabIndex = 111;
            this.customSearchHeader1.TabStop = false;
            this.customSearchHeader1.Visible = true;
            // 
            // label06
            // 
            this.label06.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label06.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label06.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label06.ForeColor = System.Drawing.Color.White;
            this.label06.Location = new System.Drawing.Point(516, 8);
            this.label06.Name = "label06";
            this.label06.Size = new System.Drawing.Size(110, 20);
            this.label06.TabIndex = 100;
            this.label06.Text = "請求書書式１";
            this.label06.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.GYOUSHA_SEARCH_BUTTON.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.GYOUSHA_SEARCH_BUTTON.GetCodeMasterField = null;
            this.GYOUSHA_SEARCH_BUTTON.Image = ((System.Drawing.Image)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.Image")));
            this.GYOUSHA_SEARCH_BUTTON.ItemDefinedTypes = null;
            this.GYOUSHA_SEARCH_BUTTON.LinkedSettingTextBox = null;
            this.GYOUSHA_SEARCH_BUTTON.LinkedTextBoxs = null;
            this.GYOUSHA_SEARCH_BUTTON.Location = new System.Drawing.Point(469, 53);
            this.GYOUSHA_SEARCH_BUTTON.Name = "GYOUSHA_SEARCH_BUTTON";
            this.GYOUSHA_SEARCH_BUTTON.PopupAfterExecute = null;
            this.GYOUSHA_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.GYOUSHA_SEARCH_BUTTON.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams")));
            this.GYOUSHA_SEARCH_BUTTON.PopupSetFormField = "GYOUSHA_CD,GYOUSHA_RNAME";
            this.GYOUSHA_SEARCH_BUTTON.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_SEARCH_BUTTON.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_SEARCH_BUTTON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.popupWindowSetting")));
            this.GYOUSHA_SEARCH_BUTTON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.RegistCheckMethod")));
            this.GYOUSHA_SEARCH_BUTTON.SearchDisplayFlag = 0;
            this.GYOUSHA_SEARCH_BUTTON.SetFormField = null;
            this.GYOUSHA_SEARCH_BUTTON.ShortItemName = null;
            this.GYOUSHA_SEARCH_BUTTON.Size = new System.Drawing.Size(22, 22);
            this.GYOUSHA_SEARCH_BUTTON.TabIndex = 771;
            this.GYOUSHA_SEARCH_BUTTON.TabStop = false;
            this.GYOUSHA_SEARCH_BUTTON.UseVisualStyleBackColor = false;
            this.GYOUSHA_SEARCH_BUTTON.ZeroPaddengFlag = false;
            // 
            // GYOUSHA_RNAME
            // 
            this.GYOUSHA_RNAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GYOUSHA_RNAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_RNAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.GYOUSHA_RNAME.DBFieldsName = "GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_RNAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_RNAME.DisplayPopUp = null;
            this.GYOUSHA_RNAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_RNAME.FocusOutCheckMethod")));
            this.GYOUSHA_RNAME.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.GYOUSHA_RNAME.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_RNAME.IsInputErrorOccured = false;
            this.GYOUSHA_RNAME.ItemDefinedTypes = "varchar";
            this.GYOUSHA_RNAME.Location = new System.Drawing.Point(177, 54);
            this.GYOUSHA_RNAME.MaxLength = 40;
            this.GYOUSHA_RNAME.Name = "GYOUSHA_RNAME";
            this.GYOUSHA_RNAME.PopupAfterExecute = null;
            this.GYOUSHA_RNAME.PopupBeforeExecute = null;
            this.GYOUSHA_RNAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_RNAME.PopupSearchSendParams")));
            this.GYOUSHA_RNAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_RNAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_RNAME.popupWindowSetting")));
            this.GYOUSHA_RNAME.ReadOnly = true;
            this.GYOUSHA_RNAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_RNAME.RegistCheckMethod")));
            this.GYOUSHA_RNAME.Size = new System.Drawing.Size(287, 20);
            this.GYOUSHA_RNAME.TabIndex = 770;
            this.GYOUSHA_RNAME.TabStop = false;
            this.GYOUSHA_RNAME.Tag = " ";
            // 
            // GYOUSHA_LABEL
            // 
            this.GYOUSHA_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.GYOUSHA_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GYOUSHA_LABEL.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.GYOUSHA_LABEL.ForeColor = System.Drawing.Color.White;
            this.GYOUSHA_LABEL.Location = new System.Drawing.Point(12, 54);
            this.GYOUSHA_LABEL.Name = "GYOUSHA_LABEL";
            this.GYOUSHA_LABEL.Size = new System.Drawing.Size(110, 20);
            this.GYOUSHA_LABEL.TabIndex = 772;
            this.GYOUSHA_LABEL.Text = "業者";
            this.GYOUSHA_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GYOUSHA_CD
            // 
            this.GYOUSHA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.GYOUSHA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_CD.CharacterLimitList = null;
            this.GYOUSHA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.GYOUSHA_CD.DBFieldsName = "GYOUSHA_CD";
            this.GYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_CD.DisplayItemName = "業者";
            this.GYOUSHA_CD.DisplayPopUp = null;
            this.GYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.FocusOutCheckMethod")));
            this.GYOUSHA_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.GYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_CD.GetCodeMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GYOUSHA_CD.IsInputErrorOccured = false;
            this.GYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.GYOUSHA_CD.Location = new System.Drawing.Point(128, 54);
            this.GYOUSHA_CD.MaxLength = 6;
            this.GYOUSHA_CD.Name = "GYOUSHA_CD";
            this.GYOUSHA_CD.PopupAfterExecute = null;
            this.GYOUSHA_CD.PopupAfterExecuteMethod = "";
            this.GYOUSHA_CD.PopupBeforeExecute = null;
            this.GYOUSHA_CD.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_CD.PopupSearchSendParams")));
            this.GYOUSHA_CD.PopupSetFormField = "GYOUSHA_CD,GYOUSHA_RNAME";
            this.GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_CD.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_CD.popupWindowSetting")));
            this.GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.RegistCheckMethod")));
            this.GYOUSHA_CD.SetFormField = "GYOUSHA_CD,GYOUSHA_RNAME";
            this.GYOUSHA_CD.Size = new System.Drawing.Size(50, 20);
            this.GYOUSHA_CD.TabIndex = 3;
            this.GYOUSHA_CD.Tag = "半角6桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.GYOUSHA_CD.ZeroPaddengFlag = true;
            this.GYOUSHA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.GYOUSHA_CD_Validating);
            // 
            // TORIHIKISAKI_SEARCH_BUTTON
            // 
            this.TORIHIKISAKI_SEARCH_BUTTON.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.TORIHIKISAKI_SEARCH_BUTTON.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TORIHIKISAKI_SEARCH_BUTTON.DBFieldsName = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_SEARCH_BUTTON.DisplayItemName = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.DisplayPopUp = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.ErrorMessage = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_SEARCH_BUTTON.FocusOutCheckMethod")));
            this.TORIHIKISAKI_SEARCH_BUTTON.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.TORIHIKISAKI_SEARCH_BUTTON.GetCodeMasterField = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.Image = ((System.Drawing.Image)(resources.GetObject("TORIHIKISAKI_SEARCH_BUTTON.Image")));
            this.TORIHIKISAKI_SEARCH_BUTTON.ItemDefinedTypes = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.LinkedSettingTextBox = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.LinkedTextBoxs = new string[0];
            this.TORIHIKISAKI_SEARCH_BUTTON.Location = new System.Drawing.Point(469, 30);
            this.TORIHIKISAKI_SEARCH_BUTTON.Name = "TORIHIKISAKI_SEARCH_BUTTON";
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupAfterExecute = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupGetMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_SEARCH_BUTTON.PopupSearchSendParams")));
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupSetFormField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupWindowName = "検索共通ポップアップ";
            this.TORIHIKISAKI_SEARCH_BUTTON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_SEARCH_BUTTON.popupWindowSetting")));
            this.TORIHIKISAKI_SEARCH_BUTTON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_SEARCH_BUTTON.RegistCheckMethod")));
            this.TORIHIKISAKI_SEARCH_BUTTON.SearchDisplayFlag = 0;
            this.TORIHIKISAKI_SEARCH_BUTTON.SetFormField = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.ShortItemName = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.Size = new System.Drawing.Size(22, 22);
            this.TORIHIKISAKI_SEARCH_BUTTON.TabIndex = 767;
            this.TORIHIKISAKI_SEARCH_BUTTON.TabStop = false;
            this.TORIHIKISAKI_SEARCH_BUTTON.UseVisualStyleBackColor = false;
            this.TORIHIKISAKI_SEARCH_BUTTON.ZeroPaddengFlag = false;
            // 
            // TORIHIKISAKI_NAME_RYAKU
            // 
            this.TORIHIKISAKI_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.TORIHIKISAKI_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.TORIHIKISAKI_NAME_RYAKU.DBFieldsName = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_NAME_RYAKU.DisplayPopUp = null;
            this.TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.TORIHIKISAKI_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_NAME_RYAKU.IsInputErrorOccured = false;
            this.TORIHIKISAKI_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_NAME_RYAKU.Location = new System.Drawing.Point(177, 31);
            this.TORIHIKISAKI_NAME_RYAKU.MaxLength = 40;
            this.TORIHIKISAKI_NAME_RYAKU.Name = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_RYAKU.PopupAfterExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU.PopupBeforeExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams")));
            this.TORIHIKISAKI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.popupWindowSetting")));
            this.TORIHIKISAKI_NAME_RYAKU.ReadOnly = true;
            this.TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU.Size = new System.Drawing.Size(287, 20);
            this.TORIHIKISAKI_NAME_RYAKU.TabIndex = 766;
            this.TORIHIKISAKI_NAME_RYAKU.TabStop = false;
            this.TORIHIKISAKI_NAME_RYAKU.Tag = " ";
            // 
            // TORIHIKISAKI_LABEL
            // 
            this.TORIHIKISAKI_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.TORIHIKISAKI_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TORIHIKISAKI_LABEL.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.TORIHIKISAKI_LABEL.ForeColor = System.Drawing.Color.White;
            this.TORIHIKISAKI_LABEL.Location = new System.Drawing.Point(12, 31);
            this.TORIHIKISAKI_LABEL.Name = "TORIHIKISAKI_LABEL";
            this.TORIHIKISAKI_LABEL.Size = new System.Drawing.Size(110, 20);
            this.TORIHIKISAKI_LABEL.TabIndex = 768;
            this.TORIHIKISAKI_LABEL.Text = "取引先";
            this.TORIHIKISAKI_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TORIHIKISAKI_CD
            // 
            this.TORIHIKISAKI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.TORIHIKISAKI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_CD.CharacterLimitList = null;
            this.TORIHIKISAKI_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.TORIHIKISAKI_CD.DBFieldsName = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_CD.DisplayItemName = "取引先";
            this.TORIHIKISAKI_CD.DisplayPopUp = null;
            this.TORIHIKISAKI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.FocusOutCheckMethod")));
            this.TORIHIKISAKI_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.TORIHIKISAKI_CD.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_CD.GetCodeMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TORIHIKISAKI_CD.IsInputErrorOccured = false;
            this.TORIHIKISAKI_CD.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_CD.Location = new System.Drawing.Point(128, 31);
            this.TORIHIKISAKI_CD.MaxLength = 6;
            this.TORIHIKISAKI_CD.Name = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.PopupAfterExecute = null;
            this.TORIHIKISAKI_CD.PopupAfterExecuteMethod = "";
            this.TORIHIKISAKI_CD.PopupBeforeExecute = null;
            this.TORIHIKISAKI_CD.PopupGetMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_CD.PopupSearchSendParams")));
            this.TORIHIKISAKI_CD.PopupSetFormField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TORIHIKISAKI_CD.PopupWindowName = "検索共通ポップアップ";
            this.TORIHIKISAKI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_CD.popupWindowSetting")));
            this.TORIHIKISAKI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.RegistCheckMethod")));
            this.TORIHIKISAKI_CD.SetFormField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.Size = new System.Drawing.Size(50, 20);
            this.TORIHIKISAKI_CD.TabIndex = 2;
            this.TORIHIKISAKI_CD.Tag = "半角6桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.TORIHIKISAKI_CD.ZeroPaddengFlag = true;
            // 
            // GENBA_SEARCH_BUTTON
            // 
            this.GENBA_SEARCH_BUTTON.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.GENBA_SEARCH_BUTTON.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.GENBA_SEARCH_BUTTON.DBFieldsName = null;
            this.GENBA_SEARCH_BUTTON.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_SEARCH_BUTTON.DisplayItemName = null;
            this.GENBA_SEARCH_BUTTON.DisplayPopUp = null;
            this.GENBA_SEARCH_BUTTON.ErrorMessage = null;
            this.GENBA_SEARCH_BUTTON.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_SEARCH_BUTTON.FocusOutCheckMethod")));
            this.GENBA_SEARCH_BUTTON.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.GENBA_SEARCH_BUTTON.GetCodeMasterField = null;
            this.GENBA_SEARCH_BUTTON.Image = ((System.Drawing.Image)(resources.GetObject("GENBA_SEARCH_BUTTON.Image")));
            this.GENBA_SEARCH_BUTTON.ItemDefinedTypes = null;
            this.GENBA_SEARCH_BUTTON.LinkedSettingTextBox = null;
            this.GENBA_SEARCH_BUTTON.LinkedTextBoxs = null;
            this.GENBA_SEARCH_BUTTON.Location = new System.Drawing.Point(469, 76);
            this.GENBA_SEARCH_BUTTON.Name = "GENBA_SEARCH_BUTTON";
            this.GENBA_SEARCH_BUTTON.PopupAfterExecute = null;
            this.GENBA_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.GENBA_SEARCH_BUTTON.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GENBA_CD,GENBA_NAME_RYAKU";
            this.GENBA_SEARCH_BUTTON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_SEARCH_BUTTON.PopupSearchSendParams")));
            this.GENBA_SEARCH_BUTTON.PopupSetFormField = "GYOUSHA_CD,GYOUSHA_RNAME,GENBA_CD,GENBA_RNAME";
            this.GENBA_SEARCH_BUTTON.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_SEARCH_BUTTON.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_SEARCH_BUTTON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_SEARCH_BUTTON.popupWindowSetting")));
            this.GENBA_SEARCH_BUTTON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_SEARCH_BUTTON.RegistCheckMethod")));
            this.GENBA_SEARCH_BUTTON.SearchDisplayFlag = 0;
            this.GENBA_SEARCH_BUTTON.SetFormField = null;
            this.GENBA_SEARCH_BUTTON.ShortItemName = null;
            this.GENBA_SEARCH_BUTTON.Size = new System.Drawing.Size(22, 22);
            this.GENBA_SEARCH_BUTTON.TabIndex = 775;
            this.GENBA_SEARCH_BUTTON.TabStop = false;
            this.GENBA_SEARCH_BUTTON.UseVisualStyleBackColor = false;
            this.GENBA_SEARCH_BUTTON.ZeroPaddengFlag = false;
            // 
            // GENBA_RNAME
            // 
            this.GENBA_RNAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GENBA_RNAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_RNAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.GENBA_RNAME.DBFieldsName = "GENBA_NAME_RYAKU";
            this.GENBA_RNAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_RNAME.DisplayPopUp = null;
            this.GENBA_RNAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_RNAME.FocusOutCheckMethod")));
            this.GENBA_RNAME.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.GENBA_RNAME.ForeColor = System.Drawing.Color.Black;
            this.GENBA_RNAME.IsInputErrorOccured = false;
            this.GENBA_RNAME.ItemDefinedTypes = "varchar";
            this.GENBA_RNAME.Location = new System.Drawing.Point(177, 77);
            this.GENBA_RNAME.MaxLength = 40;
            this.GENBA_RNAME.Name = "GENBA_RNAME";
            this.GENBA_RNAME.PopupAfterExecute = null;
            this.GENBA_RNAME.PopupBeforeExecute = null;
            this.GENBA_RNAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_RNAME.PopupSearchSendParams")));
            this.GENBA_RNAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_RNAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_RNAME.popupWindowSetting")));
            this.GENBA_RNAME.ReadOnly = true;
            this.GENBA_RNAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_RNAME.RegistCheckMethod")));
            this.GENBA_RNAME.Size = new System.Drawing.Size(287, 20);
            this.GENBA_RNAME.TabIndex = 774;
            this.GENBA_RNAME.TabStop = false;
            this.GENBA_RNAME.Tag = " ";
            // 
            // GENBA_LABEL
            // 
            this.GENBA_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.GENBA_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GENBA_LABEL.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.GENBA_LABEL.ForeColor = System.Drawing.Color.White;
            this.GENBA_LABEL.Location = new System.Drawing.Point(12, 77);
            this.GENBA_LABEL.Name = "GENBA_LABEL";
            this.GENBA_LABEL.Size = new System.Drawing.Size(110, 20);
            this.GENBA_LABEL.TabIndex = 776;
            this.GENBA_LABEL.Text = "現場";
            this.GENBA_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GENBA_CD
            // 
            this.GENBA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.GENBA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_CD.CharacterLimitList = null;
            this.GENBA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.GENBA_CD.DBFieldsName = "GENBA_CD";
            this.GENBA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_CD.DisplayPopUp = null;
            this.GENBA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.FocusOutCheckMethod")));
            this.GENBA_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.GENBA_CD.ForeColor = System.Drawing.Color.Black;
            this.GENBA_CD.GetCodeMasterField = "";
            this.GENBA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GENBA_CD.IsInputErrorOccured = false;
            this.GENBA_CD.ItemDefinedTypes = "varchar";
            this.GENBA_CD.Location = new System.Drawing.Point(128, 77);
            this.GENBA_CD.MaxLength = 6;
            this.GENBA_CD.Name = "GENBA_CD";
            this.GENBA_CD.PopupAfterExecute = null;
            this.GENBA_CD.PopupAfterExecuteMethod = "PopupAfterGenba";
            this.GENBA_CD.PopupBeforeExecute = null;
            this.GENBA_CD.PopupBeforeExecuteMethod = "";
            this.GENBA_CD.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_CD.PopupSearchSendParams")));
            this.GENBA_CD.PopupSetFormField = "GENBA_CD,GENBA_RNAME,GYOUSHA_CD,GYOUSHA_RNAME";
            this.GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_CD.popupWindowSetting")));
            this.GENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.RegistCheckMethod")));
            this.GENBA_CD.SetFormField = "";
            this.GENBA_CD.Size = new System.Drawing.Size(50, 20);
            this.GENBA_CD.TabIndex = 4;
            this.GENBA_CD.Tag = "半角6桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.GENBA_CD.ZeroPaddengFlag = true;
            this.GENBA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.GENBA_CD_Validating);
            // 
            // panel13
            // 
            this.panel13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel13.Controls.Add(this.SEIKYUU_DAIHYOU_PRINT_KBN_2);
            this.panel13.Controls.Add(this.SEIKYUU_DAIHYOU_PRINT_KBN_1);
            this.panel13.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.panel13.Location = new System.Drawing.Point(152, 8);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(182, 20);
            this.panel13.TabIndex = 221;
            // 
            // SEIKYUU_DAIHYOU_PRINT_KBN_2
            // 
            this.SEIKYUU_DAIHYOU_PRINT_KBN_2.AutoSize = true;
            this.SEIKYUU_DAIHYOU_PRINT_KBN_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.SEIKYUU_DAIHYOU_PRINT_KBN_2.DisplayItemName = "印字しない";
            this.SEIKYUU_DAIHYOU_PRINT_KBN_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEIKYUU_DAIHYOU_PRINT_KBN_2.FocusOutCheckMethod")));
            this.SEIKYUU_DAIHYOU_PRINT_KBN_2.LinkedTextBox = "RAKURAKU_CSV_KBN";
            this.SEIKYUU_DAIHYOU_PRINT_KBN_2.Location = new System.Drawing.Point(95, 0);
            this.SEIKYUU_DAIHYOU_PRINT_KBN_2.Name = "SEIKYUU_DAIHYOU_PRINT_KBN_2";
            this.SEIKYUU_DAIHYOU_PRINT_KBN_2.PopupAfterExecute = null;
            this.SEIKYUU_DAIHYOU_PRINT_KBN_2.PopupBeforeExecute = null;
            this.SEIKYUU_DAIHYOU_PRINT_KBN_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEIKYUU_DAIHYOU_PRINT_KBN_2.PopupSearchSendParams")));
            this.SEIKYUU_DAIHYOU_PRINT_KBN_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SEIKYUU_DAIHYOU_PRINT_KBN_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEIKYUU_DAIHYOU_PRINT_KBN_2.popupWindowSetting")));
            this.SEIKYUU_DAIHYOU_PRINT_KBN_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEIKYUU_DAIHYOU_PRINT_KBN_2.RegistCheckMethod")));
            this.SEIKYUU_DAIHYOU_PRINT_KBN_2.ShortItemName = "印字しない";
            this.SEIKYUU_DAIHYOU_PRINT_KBN_2.Size = new System.Drawing.Size(78, 17);
            this.SEIKYUU_DAIHYOU_PRINT_KBN_2.TabIndex = 1;
            this.SEIKYUU_DAIHYOU_PRINT_KBN_2.Tag = " ";
            this.SEIKYUU_DAIHYOU_PRINT_KBN_2.Text = "2. 未出力";
            this.SEIKYUU_DAIHYOU_PRINT_KBN_2.UseVisualStyleBackColor = true;
            this.SEIKYUU_DAIHYOU_PRINT_KBN_2.Value = "2";
            // 
            // SEIKYUU_DAIHYOU_PRINT_KBN_1
            // 
            this.SEIKYUU_DAIHYOU_PRINT_KBN_1.AutoSize = true;
            this.SEIKYUU_DAIHYOU_PRINT_KBN_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.SEIKYUU_DAIHYOU_PRINT_KBN_1.DisplayItemName = "印字する";
            this.SEIKYUU_DAIHYOU_PRINT_KBN_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEIKYUU_DAIHYOU_PRINT_KBN_1.FocusOutCheckMethod")));
            this.SEIKYUU_DAIHYOU_PRINT_KBN_1.LinkedTextBox = "RAKURAKU_CSV_KBN";
            this.SEIKYUU_DAIHYOU_PRINT_KBN_1.Location = new System.Drawing.Point(11, 0);
            this.SEIKYUU_DAIHYOU_PRINT_KBN_1.Name = "SEIKYUU_DAIHYOU_PRINT_KBN_1";
            this.SEIKYUU_DAIHYOU_PRINT_KBN_1.PopupAfterExecute = null;
            this.SEIKYUU_DAIHYOU_PRINT_KBN_1.PopupBeforeExecute = null;
            this.SEIKYUU_DAIHYOU_PRINT_KBN_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEIKYUU_DAIHYOU_PRINT_KBN_1.PopupSearchSendParams")));
            this.SEIKYUU_DAIHYOU_PRINT_KBN_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SEIKYUU_DAIHYOU_PRINT_KBN_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEIKYUU_DAIHYOU_PRINT_KBN_1.popupWindowSetting")));
            this.SEIKYUU_DAIHYOU_PRINT_KBN_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEIKYUU_DAIHYOU_PRINT_KBN_1.RegistCheckMethod")));
            this.SEIKYUU_DAIHYOU_PRINT_KBN_1.ShortItemName = "印字する";
            this.SEIKYUU_DAIHYOU_PRINT_KBN_1.Size = new System.Drawing.Size(78, 17);
            this.SEIKYUU_DAIHYOU_PRINT_KBN_1.TabIndex = 0;
            this.SEIKYUU_DAIHYOU_PRINT_KBN_1.Tag = " ";
            this.SEIKYUU_DAIHYOU_PRINT_KBN_1.Text = "1. 出力済";
            this.SEIKYUU_DAIHYOU_PRINT_KBN_1.UseVisualStyleBackColor = true;
            this.SEIKYUU_DAIHYOU_PRINT_KBN_1.Value = "1";
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label23.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.label23.ForeColor = System.Drawing.Color.White;
            this.label23.Location = new System.Drawing.Point(12, 8);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(110, 20);
            this.label23.TabIndex = 778;
            this.label23.Text = "楽楽CSV";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cb_shimebi
            // 
            this.cb_shimebi.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cb_shimebi.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cb_shimebi.BackColor = System.Drawing.SystemColors.Window;
            this.cb_shimebi.DefaultBackColor = System.Drawing.Color.Empty;
            this.cb_shimebi.DisplayPopUp = null;
            this.cb_shimebi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_shimebi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cb_shimebi.FocusOutCheckMethod")));
            this.cb_shimebi.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cb_shimebi.FormattingEnabled = true;
            this.cb_shimebi.IsInputErrorOccured = false;
            this.cb_shimebi.Items.AddRange(new object[] {
            "",
            "0",
            "5",
            "10",
            "15",
            "20",
            "25",
            "31"});
            this.cb_shimebi.Location = new System.Drawing.Point(470, 8);
            this.cb_shimebi.Name = "cb_shimebi";
            this.cb_shimebi.PopupAfterExecute = null;
            this.cb_shimebi.PopupBeforeExecute = null;
            this.cb_shimebi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cb_shimebi.PopupSearchSendParams")));
            this.cb_shimebi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cb_shimebi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cb_shimebi.popupWindowSetting")));
            this.cb_shimebi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cb_shimebi.RegistCheckMethod")));
            this.cb_shimebi.Size = new System.Drawing.Size(40, 21);
            this.cb_shimebi.TabIndex = 5;
            this.cb_shimebi.Tag = "請求締日を入力してください";
            // 
            // lb_shimebi1
            // 
            this.lb_shimebi1.BackColor = System.Drawing.Color.DarkGreen;
            this.lb_shimebi1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_shimebi1.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lb_shimebi1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lb_shimebi1.Location = new System.Drawing.Point(354, 8);
            this.lb_shimebi1.Name = "lb_shimebi1";
            this.lb_shimebi1.Size = new System.Drawing.Size(110, 20);
            this.lb_shimebi1.TabIndex = 780;
            this.lb_shimebi1.Text = "締日";
            this.lb_shimebi1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel2
            // 
            this.customPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel2.Controls.Add(this.rad9);
            this.customPanel2.Controls.Add(this.rad3);
            this.customPanel2.Controls.Add(this.SEIKYUU_SHO_SHOSHIKI_1);
            this.customPanel2.Controls.Add(this.rad2);
            this.customPanel2.Controls.Add(this.rad1);
            this.customPanel2.Location = new System.Drawing.Point(516, 30);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(369, 46);
            this.customPanel2.TabIndex = 6;
            this.customPanel2.TabStop = true;
            // 
            // rad9
            // 
            this.rad9.AutoSize = true;
            this.rad9.DefaultBackColor = System.Drawing.Color.Empty;
            this.rad9.DisplayItemName = "全て";
            this.rad9.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rad9.FocusOutCheckMethod")));
            this.rad9.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.rad9.LinkedTextBox = "SEIKYUU_SHO_SHOSHIKI_1";
            this.rad9.Location = new System.Drawing.Point(30, 23);
            this.rad9.Name = "rad9";
            this.rad9.PopupAfterExecute = null;
            this.rad9.PopupBeforeExecute = null;
            this.rad9.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rad9.PopupSearchSendParams")));
            this.rad9.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rad9.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rad9.popupWindowSetting")));
            this.rad9.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rad9.RegistCheckMethod")));
            this.rad9.Size = new System.Drawing.Size(67, 17);
            this.rad9.TabIndex = 17;
            this.rad9.Tag = " ";
            this.rad9.Text = "9.全て";
            this.rad9.UseVisualStyleBackColor = true;
            this.rad9.Value = "9";
            // 
            // rad3
            // 
            this.rad3.AutoSize = true;
            this.rad3.DefaultBackColor = System.Drawing.Color.Empty;
            this.rad3.DisplayItemName = "現場別";
            this.rad3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rad3.FocusOutCheckMethod")));
            this.rad3.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.rad3.LinkedTextBox = "SEIKYUU_SHO_SHOSHIKI_1";
            this.rad3.Location = new System.Drawing.Point(218, 2);
            this.rad3.Name = "rad3";
            this.rad3.PopupAfterExecute = null;
            this.rad3.PopupBeforeExecute = null;
            this.rad3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rad3.PopupSearchSendParams")));
            this.rad3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rad3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rad3.popupWindowSetting")));
            this.rad3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rad3.RegistCheckMethod")));
            this.rad3.Size = new System.Drawing.Size(81, 17);
            this.rad3.TabIndex = 16;
            this.rad3.Tag = " ";
            this.rad3.Text = "3.現場別";
            this.rad3.UseVisualStyleBackColor = true;
            this.rad3.Value = "3";
            // 
            // SEIKYUU_SHO_SHOSHIKI_1
            // 
            this.SEIKYUU_SHO_SHOSHIKI_1.BackColor = System.Drawing.SystemColors.Window;
            this.SEIKYUU_SHO_SHOSHIKI_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SEIKYUU_SHO_SHOSHIKI_1.CharacterLimitList = new char[] {
        '1',
        '2',
        '3',
        '9'};
            this.SEIKYUU_SHO_SHOSHIKI_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.SEIKYUU_SHO_SHOSHIKI_1.DisplayItemName = "請求書書式１";
            this.SEIKYUU_SHO_SHOSHIKI_1.DisplayPopUp = null;
            this.SEIKYUU_SHO_SHOSHIKI_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEIKYUU_SHO_SHOSHIKI_1.FocusOutCheckMethod")));
            this.SEIKYUU_SHO_SHOSHIKI_1.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.SEIKYUU_SHO_SHOSHIKI_1.ForeColor = System.Drawing.Color.Black;
            this.SEIKYUU_SHO_SHOSHIKI_1.IsInputErrorOccured = false;
            this.SEIKYUU_SHO_SHOSHIKI_1.LinkedRadioButtonArray = new string[] {
        "rad1",
        "rad2",
        "rad3",
        "rad9"};
            this.SEIKYUU_SHO_SHOSHIKI_1.Location = new System.Drawing.Point(-1, -1);
            this.SEIKYUU_SHO_SHOSHIKI_1.Name = "SEIKYUU_SHO_SHOSHIKI_1";
            this.SEIKYUU_SHO_SHOSHIKI_1.PopupAfterExecute = null;
            this.SEIKYUU_SHO_SHOSHIKI_1.PopupBeforeExecute = null;
            this.SEIKYUU_SHO_SHOSHIKI_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEIKYUU_SHO_SHOSHIKI_1.PopupSearchSendParams")));
            this.SEIKYUU_SHO_SHOSHIKI_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SEIKYUU_SHO_SHOSHIKI_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEIKYUU_SHO_SHOSHIKI_1.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            9,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SEIKYUU_SHO_SHOSHIKI_1.RangeSetting = rangeSettingDto1;
            this.SEIKYUU_SHO_SHOSHIKI_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEIKYUU_SHO_SHOSHIKI_1.RegistCheckMethod")));
            this.SEIKYUU_SHO_SHOSHIKI_1.Size = new System.Drawing.Size(25, 20);
            this.SEIKYUU_SHO_SHOSHIKI_1.TabIndex = 10;
            this.SEIKYUU_SHO_SHOSHIKI_1.Tag = "【1、2、3、9】のいずれかで入力してください";
            this.SEIKYUU_SHO_SHOSHIKI_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.SEIKYUU_SHO_SHOSHIKI_1.WordWrap = false;
            this.SEIKYUU_SHO_SHOSHIKI_1.TextChanged += new System.EventHandler(this.SEIKYUU_SHO_SHOSHIKI_1_TextChanged);
            // 
            // rad2
            // 
            this.rad2.AutoSize = true;
            this.rad2.DefaultBackColor = System.Drawing.Color.Empty;
            this.rad2.DisplayItemName = "業者別";
            this.rad2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rad2.FocusOutCheckMethod")));
            this.rad2.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.rad2.LinkedTextBox = "SEIKYUU_SHO_SHOSHIKI_1";
            this.rad2.Location = new System.Drawing.Point(131, 1);
            this.rad2.Name = "rad2";
            this.rad2.PopupAfterExecute = null;
            this.rad2.PopupBeforeExecute = null;
            this.rad2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rad2.PopupSearchSendParams")));
            this.rad2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rad2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rad2.popupWindowSetting")));
            this.rad2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rad2.RegistCheckMethod")));
            this.rad2.Size = new System.Drawing.Size(81, 17);
            this.rad2.TabIndex = 15;
            this.rad2.Tag = " ";
            this.rad2.Text = "2.業者別";
            this.rad2.UseVisualStyleBackColor = true;
            this.rad2.Value = "2";
            // 
            // rad1
            // 
            this.rad1.AutoSize = true;
            this.rad1.DefaultBackColor = System.Drawing.Color.Empty;
            this.rad1.DisplayItemName = "請求先別";
            this.rad1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rad1.FocusOutCheckMethod")));
            this.rad1.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.rad1.LinkedTextBox = "SEIKYUU_SHO_SHOSHIKI_1";
            this.rad1.Location = new System.Drawing.Point(30, 1);
            this.rad1.Name = "rad1";
            this.rad1.PopupAfterExecute = null;
            this.rad1.PopupBeforeExecute = null;
            this.rad1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rad1.PopupSearchSendParams")));
            this.rad1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rad1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rad1.popupWindowSetting")));
            this.rad1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rad1.RegistCheckMethod")));
            this.rad1.Size = new System.Drawing.Size(95, 17);
            this.rad1.TabIndex = 14;
            this.rad1.Tag = " ";
            this.rad1.Text = "1.請求先別";
            this.rad1.UseVisualStyleBackColor = true;
            this.rad1.Value = "1";
            // 
            // RAKURAKU_CSV_KBN
            // 
            this.RAKURAKU_CSV_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.RAKURAKU_CSV_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RAKURAKU_CSV_KBN.CharacterLimitList = new char[] {
        '1',
        '2'};
            this.RAKURAKU_CSV_KBN.DBFieldsName = "";
            this.RAKURAKU_CSV_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.RAKURAKU_CSV_KBN.DisplayItemName = "楽楽CSV";
            this.RAKURAKU_CSV_KBN.DisplayPopUp = null;
            this.RAKURAKU_CSV_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("RAKURAKU_CSV_KBN.FocusOutCheckMethod")));
            this.RAKURAKU_CSV_KBN.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.RAKURAKU_CSV_KBN.ForeColor = System.Drawing.Color.Black;
            this.RAKURAKU_CSV_KBN.IsInputErrorOccured = false;
            this.RAKURAKU_CSV_KBN.ItemDefinedTypes = "smallint";
            this.RAKURAKU_CSV_KBN.LinkedRadioButtonArray = new string[] {
        "SEIKYUU_DAIHYOU_PRINT_KBN_1",
        "SEIKYUU_DAIHYOU_PRINT_KBN_2"};
            this.RAKURAKU_CSV_KBN.Location = new System.Drawing.Point(128, 8);
            this.RAKURAKU_CSV_KBN.Name = "RAKURAKU_CSV_KBN";
            this.RAKURAKU_CSV_KBN.PopupAfterExecute = null;
            this.RAKURAKU_CSV_KBN.PopupBeforeExecute = null;
            this.RAKURAKU_CSV_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("RAKURAKU_CSV_KBN.PopupSearchSendParams")));
            this.RAKURAKU_CSV_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.RAKURAKU_CSV_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("RAKURAKU_CSV_KBN.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto2.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RAKURAKU_CSV_KBN.RangeSetting = rangeSettingDto2;
            this.RAKURAKU_CSV_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("RAKURAKU_CSV_KBN.RegistCheckMethod")));
            this.RAKURAKU_CSV_KBN.ShortItemName = "楽楽CSV";
            this.RAKURAKU_CSV_KBN.Size = new System.Drawing.Size(25, 20);
            this.RAKURAKU_CSV_KBN.TabIndex = 1;
            this.RAKURAKU_CSV_KBN.Tag = "【1、2】のいずれかで入力してください";
            this.RAKURAKU_CSV_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.RAKURAKU_CSV_KBN.WordWrap = false;
            this.RAKURAKU_CSV_KBN.TextChanged += new System.EventHandler(this.RAKURAKU_CSV_KBN_TextChanged);
            this.RAKURAKU_CSV_KBN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.RAKURAKU_CSV_KBN_KeyPress);
            // 
            // RakurakuMasutaIchiranForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 458);
            this.Controls.Add(this.RAKURAKU_CSV_KBN);
            this.Controls.Add(this.customPanel2);
            this.Controls.Add(this.cb_shimebi);
            this.Controls.Add(this.lb_shimebi1);
            this.Controls.Add(this.panel13);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.GENBA_SEARCH_BUTTON);
            this.Controls.Add(this.GENBA_RNAME);
            this.Controls.Add(this.GENBA_LABEL);
            this.Controls.Add(this.GENBA_CD);
            this.Controls.Add(this.GYOUSHA_SEARCH_BUTTON);
            this.Controls.Add(this.GYOUSHA_RNAME);
            this.Controls.Add(this.GYOUSHA_LABEL);
            this.Controls.Add(this.GYOUSHA_CD);
            this.Controls.Add(this.TORIHIKISAKI_SEARCH_BUTTON);
            this.Controls.Add(this.TORIHIKISAKI_NAME_RYAKU);
            this.Controls.Add(this.TORIHIKISAKI_LABEL);
            this.Controls.Add(this.TORIHIKISAKI_CD);
            this.Controls.Add(this.label06);
            this.Name = "RakurakuMasutaIchiranForm";
            this.Text = "F";
            this.Shown += new System.EventHandler(this.GenbaIchiranForm_Shown);
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.label06, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_CD, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_LABEL, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_SEARCH_BUTTON, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_CD, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_LABEL, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_RNAME, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_SEARCH_BUTTON, 0);
            this.Controls.SetChildIndex(this.GENBA_CD, 0);
            this.Controls.SetChildIndex(this.GENBA_LABEL, 0);
            this.Controls.SetChildIndex(this.GENBA_RNAME, 0);
            this.Controls.SetChildIndex(this.GENBA_SEARCH_BUTTON, 0);
            this.Controls.SetChildIndex(this.label23, 0);
            this.Controls.SetChildIndex(this.panel13, 0);
            this.Controls.SetChildIndex(this.lb_shimebi1, 0);
            this.Controls.SetChildIndex(this.cb_shimebi, 0);
            this.Controls.SetChildIndex(this.customPanel2, 0);
            this.Controls.SetChildIndex(this.RAKURAKU_CSV_KBN, 0);
            this.panel13.ResumeLayout(false);
            this.panel13.PerformLayout();
            this.customPanel2.ResumeLayout(false);
            this.customPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private r_framework.Dto.SelectCheckDto selectCheckDto1;
        private r_framework.Dto.SelectCheckDto selectCheckDto2;
        private r_framework.Dto.SelectCheckDto selectCheckDto3;
        private r_framework.Dto.SelectCheckDto selectCheckDto4;
        private r_framework.Dto.SelectCheckDto selectCheckDto5;
        private r_framework.Dto.SelectCheckDto selectCheckDto6;
        private r_framework.Dto.SelectCheckDto selectCheckDto7;
        private r_framework.Dto.SelectCheckDto selectCheckDto8;
        private r_framework.Dto.SelectCheckDto selectCheckDto9;
        private r_framework.Dto.SelectCheckDto selectCheckDto10;
        private r_framework.Dto.SelectCheckDto selectCheckDto11;
        private System.Windows.Forms.Label label06;
        internal r_framework.CustomControl.CustomPopupOpenButton GYOUSHA_SEARCH_BUTTON;
        internal r_framework.CustomControl.CustomTextBox GYOUSHA_RNAME;
        internal System.Windows.Forms.Label GYOUSHA_LABEL;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GYOUSHA_CD;
        internal r_framework.CustomControl.CustomPopupOpenButton TORIHIKISAKI_SEARCH_BUTTON;
        internal r_framework.CustomControl.CustomTextBox TORIHIKISAKI_NAME_RYAKU;
        internal System.Windows.Forms.Label TORIHIKISAKI_LABEL;
        internal r_framework.CustomControl.CustomAlphaNumTextBox TORIHIKISAKI_CD;
        internal r_framework.CustomControl.CustomPopupOpenButton GENBA_SEARCH_BUTTON;
        internal r_framework.CustomControl.CustomTextBox GENBA_RNAME;
        internal System.Windows.Forms.Label GENBA_LABEL;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GENBA_CD;
        internal System.Windows.Forms.Panel panel13;
        internal r_framework.CustomControl.CustomRadioButton SEIKYUU_DAIHYOU_PRINT_KBN_2;
        internal r_framework.CustomControl.CustomRadioButton SEIKYUU_DAIHYOU_PRINT_KBN_1;
        internal System.Windows.Forms.Label label23;
        internal r_framework.CustomControl.CustomComboBox cb_shimebi;
        private System.Windows.Forms.Label lb_shimebi1;
        private r_framework.CustomControl.CustomPanel customPanel2;
        public r_framework.CustomControl.CustomRadioButton rad3;
        public r_framework.CustomControl.CustomNumericTextBox2 SEIKYUU_SHO_SHOSHIKI_1;
        public r_framework.CustomControl.CustomRadioButton rad2;
        public r_framework.CustomControl.CustomRadioButton rad1;
        public r_framework.CustomControl.CustomRadioButton rad9;
        public SelectCheckDto selectCheckDto12;
        public r_framework.CustomControl.CustomNumericTextBox2 RAKURAKU_CSV_KBN;
    }
}
