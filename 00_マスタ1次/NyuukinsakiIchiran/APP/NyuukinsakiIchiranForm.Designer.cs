namespace NyuukinsakiIchiran.APP
{
    partial class NyuukinsakiIchiranForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NyuukinsakiIchiranForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.Nyuukinsaki_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.Nyuukinsaki_NAME2 = new r_framework.CustomControl.CustomTextBox();
            this.Nyuukinsaki_NAME1 = new r_framework.CustomControl.CustomTextBox();
            this.Nyuukinsaki_FURIGANA = new r_framework.CustomControl.CustomTextBox();
            this.label06 = new System.Windows.Forms.Label();
            this.label05 = new System.Windows.Forms.Label();
            this.label04 = new System.Windows.Forms.Label();
            this.label03 = new System.Windows.Forms.Label();
            this.Nyuukinsaki_ADDRESS = new r_framework.CustomControl.CustomTextBox();
            this.Nyuukinsaki_TODOUFUKEN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.Nyuukinsaki_TODOUFUKEN_NAME = new r_framework.CustomControl.CustomTextBox();
            this.label003 = new System.Windows.Forms.Label();
            this.label001 = new System.Windows.Forms.Label();
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.searchString.Location = new System.Drawing.Point(3, 3);
            this.searchString.ReadOnly = true;
            this.searchString.Size = new System.Drawing.Size(997, 123);
            this.searchString.TabIndex = 100;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Location = new System.Drawing.Point(4, 427);
            this.bt_ptn1.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn1.TabIndex = 201;
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn2.TabIndex = 202;
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Location = new System.Drawing.Point(404, 427);
            this.bt_ptn3.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn3.TabIndex = 203;
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Location = new System.Drawing.Point(604, 427);
            this.bt_ptn4.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn4.TabIndex = 204;
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Location = new System.Drawing.Point(804, 427);
            this.bt_ptn5.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn5.TabIndex = 205;
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.Location = new System.Drawing.Point(4, 131);
            this.customSortHeader1.Size = new System.Drawing.Size(994, 50);
            this.customSortHeader1.TabIndex = 118;
            // 
            // Nyuukinsaki_NAME_RYAKU
            // 
            this.Nyuukinsaki_NAME_RYAKU.BackColor = System.Drawing.SystemColors.Window;
            this.Nyuukinsaki_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Nyuukinsaki_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.Nyuukinsaki_NAME_RYAKU.DBFieldsName = "Nyuukinsaki_NAME_RYAKU";
            this.Nyuukinsaki_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.Nyuukinsaki_NAME_RYAKU.DisplayItemName = "入金先略称名";
            this.Nyuukinsaki_NAME_RYAKU.DisplayPopUp = null;
            this.Nyuukinsaki_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Nyuukinsaki_NAME_RYAKU.FocusOutCheckMethod")));
            this.Nyuukinsaki_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Nyuukinsaki_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.Nyuukinsaki_NAME_RYAKU.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.Nyuukinsaki_NAME_RYAKU.IsInputErrorOccured = false;
            this.Nyuukinsaki_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.Nyuukinsaki_NAME_RYAKU.Location = new System.Drawing.Point(128, 88);
            this.Nyuukinsaki_NAME_RYAKU.MaxLength = 40;
            this.Nyuukinsaki_NAME_RYAKU.Name = "Nyuukinsaki_NAME_RYAKU";
            this.Nyuukinsaki_NAME_RYAKU.PopupAfterExecute = null;
            this.Nyuukinsaki_NAME_RYAKU.PopupBeforeExecute = null;
            this.Nyuukinsaki_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Nyuukinsaki_NAME_RYAKU.PopupSearchSendParams")));
            this.Nyuukinsaki_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Nyuukinsaki_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Nyuukinsaki_NAME_RYAKU.popupWindowSetting")));
            this.Nyuukinsaki_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Nyuukinsaki_NAME_RYAKU.RegistCheckMethod")));
            this.Nyuukinsaki_NAME_RYAKU.ShortItemName = "入金先略称名";
            this.Nyuukinsaki_NAME_RYAKU.Size = new System.Drawing.Size(290, 20);
            this.Nyuukinsaki_NAME_RYAKU.TabIndex = 108;
            this.Nyuukinsaki_NAME_RYAKU.Tag = "全角２０文字以内で入力してください";
            // 
            // Nyuukinsaki_NAME2
            // 
            this.Nyuukinsaki_NAME2.BackColor = System.Drawing.SystemColors.Window;
            this.Nyuukinsaki_NAME2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Nyuukinsaki_NAME2.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.Nyuukinsaki_NAME2.DBFieldsName = "Nyuukinsaki_NAME2";
            this.Nyuukinsaki_NAME2.DefaultBackColor = System.Drawing.Color.Empty;
            this.Nyuukinsaki_NAME2.DisplayItemName = "入金先名2";
            this.Nyuukinsaki_NAME2.DisplayPopUp = null;
            this.Nyuukinsaki_NAME2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Nyuukinsaki_NAME2.FocusOutCheckMethod")));
            this.Nyuukinsaki_NAME2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Nyuukinsaki_NAME2.ForeColor = System.Drawing.Color.Black;
            this.Nyuukinsaki_NAME2.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.Nyuukinsaki_NAME2.IsInputErrorOccured = false;
            this.Nyuukinsaki_NAME2.ItemDefinedTypes = "varchar";
            this.Nyuukinsaki_NAME2.Location = new System.Drawing.Point(128, 66);
            this.Nyuukinsaki_NAME2.MaxLength = 40;
            this.Nyuukinsaki_NAME2.Name = "Nyuukinsaki_NAME2";
            this.Nyuukinsaki_NAME2.PopupAfterExecute = null;
            this.Nyuukinsaki_NAME2.PopupBeforeExecute = null;
            this.Nyuukinsaki_NAME2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Nyuukinsaki_NAME2.PopupSearchSendParams")));
            this.Nyuukinsaki_NAME2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Nyuukinsaki_NAME2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Nyuukinsaki_NAME2.popupWindowSetting")));
            this.Nyuukinsaki_NAME2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Nyuukinsaki_NAME2.RegistCheckMethod")));
            this.Nyuukinsaki_NAME2.ShortItemName = "入金先名2";
            this.Nyuukinsaki_NAME2.Size = new System.Drawing.Size(290, 20);
            this.Nyuukinsaki_NAME2.TabIndex = 106;
            this.Nyuukinsaki_NAME2.Tag = "全角２０文字以内で入力してください";
            // 
            // Nyuukinsaki_NAME1
            // 
            this.Nyuukinsaki_NAME1.BackColor = System.Drawing.SystemColors.Window;
            this.Nyuukinsaki_NAME1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Nyuukinsaki_NAME1.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.Nyuukinsaki_NAME1.CopyAutoSetControl = "";
            this.Nyuukinsaki_NAME1.DBFieldsName = "Nyuukinsaki_NAME1";
            this.Nyuukinsaki_NAME1.DefaultBackColor = System.Drawing.Color.Empty;
            this.Nyuukinsaki_NAME1.DisplayItemName = "入金先名1";
            this.Nyuukinsaki_NAME1.DisplayPopUp = null;
            this.Nyuukinsaki_NAME1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Nyuukinsaki_NAME1.FocusOutCheckMethod")));
            this.Nyuukinsaki_NAME1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Nyuukinsaki_NAME1.ForeColor = System.Drawing.Color.Black;
            this.Nyuukinsaki_NAME1.FuriganaAutoSetControl = "";
            this.Nyuukinsaki_NAME1.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.Nyuukinsaki_NAME1.IsInputErrorOccured = false;
            this.Nyuukinsaki_NAME1.ItemDefinedTypes = "varchar";
            this.Nyuukinsaki_NAME1.Location = new System.Drawing.Point(128, 44);
            this.Nyuukinsaki_NAME1.MaxLength = 40;
            this.Nyuukinsaki_NAME1.Name = "Nyuukinsaki_NAME1";
            this.Nyuukinsaki_NAME1.PopupAfterExecute = null;
            this.Nyuukinsaki_NAME1.PopupBeforeExecute = null;
            this.Nyuukinsaki_NAME1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Nyuukinsaki_NAME1.PopupSearchSendParams")));
            this.Nyuukinsaki_NAME1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Nyuukinsaki_NAME1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Nyuukinsaki_NAME1.popupWindowSetting")));
            this.Nyuukinsaki_NAME1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Nyuukinsaki_NAME1.RegistCheckMethod")));
            this.Nyuukinsaki_NAME1.ShortItemName = "入金先名1";
            this.Nyuukinsaki_NAME1.Size = new System.Drawing.Size(290, 20);
            this.Nyuukinsaki_NAME1.TabIndex = 104;
            this.Nyuukinsaki_NAME1.Tag = "全角２０文字以内で入力してください";
            // 
            // Nyuukinsaki_FURIGANA
            // 
            this.Nyuukinsaki_FURIGANA.BackColor = System.Drawing.SystemColors.Window;
            this.Nyuukinsaki_FURIGANA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Nyuukinsaki_FURIGANA.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.Nyuukinsaki_FURIGANA.DBFieldsName = "Nyuukinsaki_FURIGANA";
            this.Nyuukinsaki_FURIGANA.DefaultBackColor = System.Drawing.Color.Empty;
            this.Nyuukinsaki_FURIGANA.DisplayItemName = "入金先ふりがな";
            this.Nyuukinsaki_FURIGANA.DisplayPopUp = null;
            this.Nyuukinsaki_FURIGANA.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Nyuukinsaki_FURIGANA.FocusOutCheckMethod")));
            this.Nyuukinsaki_FURIGANA.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Nyuukinsaki_FURIGANA.ForeColor = System.Drawing.Color.Black;
            this.Nyuukinsaki_FURIGANA.ImeMode = System.Windows.Forms.ImeMode.Katakana;
            this.Nyuukinsaki_FURIGANA.IsInputErrorOccured = false;
            this.Nyuukinsaki_FURIGANA.ItemDefinedTypes = "varchar";
            this.Nyuukinsaki_FURIGANA.Location = new System.Drawing.Point(128, 9);
            this.Nyuukinsaki_FURIGANA.MaxLength = 80;
            this.Nyuukinsaki_FURIGANA.Multiline = true;
            this.Nyuukinsaki_FURIGANA.Name = "Nyuukinsaki_FURIGANA";
            this.Nyuukinsaki_FURIGANA.PopupAfterExecute = null;
            this.Nyuukinsaki_FURIGANA.PopupBeforeExecute = null;
            this.Nyuukinsaki_FURIGANA.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Nyuukinsaki_FURIGANA.PopupSearchSendParams")));
            this.Nyuukinsaki_FURIGANA.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Nyuukinsaki_FURIGANA.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Nyuukinsaki_FURIGANA.popupWindowSetting")));
            this.Nyuukinsaki_FURIGANA.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Nyuukinsaki_FURIGANA.RegistCheckMethod")));
            this.Nyuukinsaki_FURIGANA.ShortItemName = "入金先ふりがな";
            this.Nyuukinsaki_FURIGANA.Size = new System.Drawing.Size(290, 32);
            this.Nyuukinsaki_FURIGANA.TabIndex = 102;
            this.Nyuukinsaki_FURIGANA.Tag = "全角４０文字以内で入力してください";
            // 
            // label06
            // 
            this.label06.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label06.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label06.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label06.ForeColor = System.Drawing.Color.White;
            this.label06.Location = new System.Drawing.Point(12, 88);
            this.label06.Name = "label06";
            this.label06.Size = new System.Drawing.Size(110, 20);
            this.label06.TabIndex = 107;
            this.label06.Text = "入金先略称";
            this.label06.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label05
            // 
            this.label05.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label05.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label05.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label05.ForeColor = System.Drawing.Color.White;
            this.label05.Location = new System.Drawing.Point(12, 66);
            this.label05.Name = "label05";
            this.label05.Size = new System.Drawing.Size(110, 20);
            this.label05.TabIndex = 105;
            this.label05.Text = "入金先名２";
            this.label05.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label04
            // 
            this.label04.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label04.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label04.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label04.ForeColor = System.Drawing.Color.White;
            this.label04.Location = new System.Drawing.Point(12, 44);
            this.label04.Name = "label04";
            this.label04.Size = new System.Drawing.Size(110, 20);
            this.label04.TabIndex = 103;
            this.label04.Text = "入金先名１";
            this.label04.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label03
            // 
            this.label03.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label03.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label03.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label03.ForeColor = System.Drawing.Color.White;
            this.label03.Location = new System.Drawing.Point(12, 9);
            this.label03.Name = "label03";
            this.label03.Size = new System.Drawing.Size(110, 20);
            this.label03.TabIndex = 101;
            this.label03.Text = "フリガナ";
            this.label03.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Nyuukinsaki_ADDRESS
            // 
            this.Nyuukinsaki_ADDRESS.BackColor = System.Drawing.SystemColors.Window;
            this.Nyuukinsaki_ADDRESS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Nyuukinsaki_ADDRESS.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.Nyuukinsaki_ADDRESS.DBFieldsName = "Nyuukinsaki_ADDRESS";
            this.Nyuukinsaki_ADDRESS.DefaultBackColor = System.Drawing.Color.Empty;
            this.Nyuukinsaki_ADDRESS.DisplayItemName = "入金先住所1";
            this.Nyuukinsaki_ADDRESS.DisplayPopUp = null;
            this.Nyuukinsaki_ADDRESS.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Nyuukinsaki_ADDRESS.FocusOutCheckMethod")));
            this.Nyuukinsaki_ADDRESS.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Nyuukinsaki_ADDRESS.ForeColor = System.Drawing.Color.Black;
            this.Nyuukinsaki_ADDRESS.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.Nyuukinsaki_ADDRESS.IsInputErrorOccured = false;
            this.Nyuukinsaki_ADDRESS.ItemDefinedTypes = "varchar";
            this.Nyuukinsaki_ADDRESS.Location = new System.Drawing.Point(560, 31);
            this.Nyuukinsaki_ADDRESS.MaxLength = 80;
            this.Nyuukinsaki_ADDRESS.Multiline = true;
            this.Nyuukinsaki_ADDRESS.Name = "Nyuukinsaki_ADDRESS";
            this.Nyuukinsaki_ADDRESS.PopupAfterExecute = null;
            this.Nyuukinsaki_ADDRESS.PopupBeforeExecute = null;
            this.Nyuukinsaki_ADDRESS.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Nyuukinsaki_ADDRESS.PopupSearchSendParams")));
            this.Nyuukinsaki_ADDRESS.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Nyuukinsaki_ADDRESS.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Nyuukinsaki_ADDRESS.popupWindowSetting")));
            this.Nyuukinsaki_ADDRESS.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Nyuukinsaki_ADDRESS.RegistCheckMethod")));
            this.Nyuukinsaki_ADDRESS.ShortItemName = "入金先住所";
            this.Nyuukinsaki_ADDRESS.Size = new System.Drawing.Size(290, 32);
            this.Nyuukinsaki_ADDRESS.TabIndex = 113;
            this.Nyuukinsaki_ADDRESS.Tag = "全角４０文字以内で入力してください";
            // 
            // Nyuukinsaki_TODOUFUKEN_CD
            // 
            this.Nyuukinsaki_TODOUFUKEN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.Nyuukinsaki_TODOUFUKEN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Nyuukinsaki_TODOUFUKEN_CD.CustomFormatSetting = "00";
            this.Nyuukinsaki_TODOUFUKEN_CD.DBFieldsName = "Nyuukinsaki_TODOUFUKEN_CD";
            this.Nyuukinsaki_TODOUFUKEN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.Nyuukinsaki_TODOUFUKEN_CD.DisplayItemName = "都道府県CD";
            this.Nyuukinsaki_TODOUFUKEN_CD.DisplayPopUp = null;
            this.Nyuukinsaki_TODOUFUKEN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Nyuukinsaki_TODOUFUKEN_CD.FocusOutCheckMethod")));
            this.Nyuukinsaki_TODOUFUKEN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Nyuukinsaki_TODOUFUKEN_CD.ForeColor = System.Drawing.Color.Black;
            this.Nyuukinsaki_TODOUFUKEN_CD.FormatSetting = "カスタム";
            this.Nyuukinsaki_TODOUFUKEN_CD.GetCodeMasterField = "TODOUFUKEN_CD,TODOUFUKEN_NAME";
            this.Nyuukinsaki_TODOUFUKEN_CD.IsInputErrorOccured = false;
            this.Nyuukinsaki_TODOUFUKEN_CD.ItemDefinedTypes = "smallint";
            this.Nyuukinsaki_TODOUFUKEN_CD.Location = new System.Drawing.Point(560, 9);
            this.Nyuukinsaki_TODOUFUKEN_CD.Name = "Nyuukinsaki_TODOUFUKEN_CD";
            this.Nyuukinsaki_TODOUFUKEN_CD.PopupAfterExecute = null;
            this.Nyuukinsaki_TODOUFUKEN_CD.PopupBeforeExecute = null;
            this.Nyuukinsaki_TODOUFUKEN_CD.PopupGetMasterField = "TODOUFUKEN_CD,TODOUFUKEN_NAME";
            this.Nyuukinsaki_TODOUFUKEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Nyuukinsaki_TODOUFUKEN_CD.PopupSearchSendParams")));
            this.Nyuukinsaki_TODOUFUKEN_CD.PopupSetFormField = "Nyuukinsaki_TODOUFUKEN_CD,Nyuukinsaki_TODOUFUKEN_NAME";
            this.Nyuukinsaki_TODOUFUKEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_TODOUFUKEN;
            this.Nyuukinsaki_TODOUFUKEN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.Nyuukinsaki_TODOUFUKEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Nyuukinsaki_TODOUFUKEN_CD.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.Nyuukinsaki_TODOUFUKEN_CD.RangeSetting = rangeSettingDto1;
            this.Nyuukinsaki_TODOUFUKEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Nyuukinsaki_TODOUFUKEN_CD.RegistCheckMethod")));
            this.Nyuukinsaki_TODOUFUKEN_CD.SetFormField = "Nyuukinsaki_TODOUFUKEN_CD,Nyuukinsaki_TODOUFUKEN_NAME";
            this.Nyuukinsaki_TODOUFUKEN_CD.ShortItemName = "都道府県略CD";
            this.Nyuukinsaki_TODOUFUKEN_CD.Size = new System.Drawing.Size(20, 20);
            this.Nyuukinsaki_TODOUFUKEN_CD.TabIndex = 110;
            this.Nyuukinsaki_TODOUFUKEN_CD.Tag = "都道府県を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.Nyuukinsaki_TODOUFUKEN_CD.WordWrap = false;
            // 
            // Nyuukinsaki_TODOUFUKEN_NAME
            // 
            this.Nyuukinsaki_TODOUFUKEN_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.Nyuukinsaki_TODOUFUKEN_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Nyuukinsaki_TODOUFUKEN_NAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.Nyuukinsaki_TODOUFUKEN_NAME.DBFieldsName = "TODOUFUKEN_NAME_RYAKU";
            this.Nyuukinsaki_TODOUFUKEN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.Nyuukinsaki_TODOUFUKEN_NAME.DisplayItemName = "都道府県略称";
            this.Nyuukinsaki_TODOUFUKEN_NAME.DisplayPopUp = null;
            this.Nyuukinsaki_TODOUFUKEN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Nyuukinsaki_TODOUFUKEN_NAME.FocusOutCheckMethod")));
            this.Nyuukinsaki_TODOUFUKEN_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Nyuukinsaki_TODOUFUKEN_NAME.ForeColor = System.Drawing.Color.Black;
            this.Nyuukinsaki_TODOUFUKEN_NAME.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Nyuukinsaki_TODOUFUKEN_NAME.IsInputErrorOccured = false;
            this.Nyuukinsaki_TODOUFUKEN_NAME.ItemDefinedTypes = "varchar";
            this.Nyuukinsaki_TODOUFUKEN_NAME.Location = new System.Drawing.Point(579, 9);
            this.Nyuukinsaki_TODOUFUKEN_NAME.MaxLength = 0;
            this.Nyuukinsaki_TODOUFUKEN_NAME.Name = "Nyuukinsaki_TODOUFUKEN_NAME";
            this.Nyuukinsaki_TODOUFUKEN_NAME.PopupAfterExecute = null;
            this.Nyuukinsaki_TODOUFUKEN_NAME.PopupBeforeExecute = null;
            this.Nyuukinsaki_TODOUFUKEN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Nyuukinsaki_TODOUFUKEN_NAME.PopupSearchSendParams")));
            this.Nyuukinsaki_TODOUFUKEN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Nyuukinsaki_TODOUFUKEN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Nyuukinsaki_TODOUFUKEN_NAME.popupWindowSetting")));
            this.Nyuukinsaki_TODOUFUKEN_NAME.ReadOnly = true;
            this.Nyuukinsaki_TODOUFUKEN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Nyuukinsaki_TODOUFUKEN_NAME.RegistCheckMethod")));
            this.Nyuukinsaki_TODOUFUKEN_NAME.ShortItemName = "都道府県略称";
            this.Nyuukinsaki_TODOUFUKEN_NAME.Size = new System.Drawing.Size(65, 20);
            this.Nyuukinsaki_TODOUFUKEN_NAME.TabIndex = 111;
            this.Nyuukinsaki_TODOUFUKEN_NAME.TabStop = false;
            this.Nyuukinsaki_TODOUFUKEN_NAME.Tag = "都道府県名が表示されます";
            // 
            // label003
            // 
            this.label003.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label003.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label003.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label003.ForeColor = System.Drawing.Color.White;
            this.label003.Location = new System.Drawing.Point(444, 31);
            this.label003.Name = "label003";
            this.label003.Size = new System.Drawing.Size(110, 20);
            this.label003.TabIndex = 112;
            this.label003.Text = "住所";
            this.label003.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label001
            // 
            this.label001.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label001.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label001.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label001.ForeColor = System.Drawing.Color.White;
            this.label001.Location = new System.Drawing.Point(444, 9);
            this.label001.Name = "label001";
            this.label001.Size = new System.Drawing.Size(110, 20);
            this.label001.TabIndex = 109;
            this.label001.Text = "都道府県";
            this.label001.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ICHIRAN_HYOUJI_JOUKEN_DELETED
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Location = new System.Drawing.Point(560, 68);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Name = "ICHIRAN_HYOUJI_JOUKEN_DELETED";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Size = new System.Drawing.Size(145, 16);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.TabIndex = 418;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Text = "削除済も含めて全て表示";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(444, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 420;
            this.label1.Text = "表示条件";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NyuukinsakiIchiranForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.ClientSize = new System.Drawing.Size(1008, 458);
            this.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_DELETED);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Nyuukinsaki_ADDRESS);
            this.Controls.Add(this.Nyuukinsaki_TODOUFUKEN_CD);
            this.Controls.Add(this.Nyuukinsaki_TODOUFUKEN_NAME);
            this.Controls.Add(this.label003);
            this.Controls.Add(this.label001);
            this.Controls.Add(this.Nyuukinsaki_NAME_RYAKU);
            this.Controls.Add(this.Nyuukinsaki_NAME2);
            this.Controls.Add(this.Nyuukinsaki_NAME1);
            this.Controls.Add(this.Nyuukinsaki_FURIGANA);
            this.Controls.Add(this.label06);
            this.Controls.Add(this.label05);
            this.Controls.Add(this.label04);
            this.Controls.Add(this.label03);
            this.Name = "NyuukinsakiIchiranForm";
            this.Text = "NyuukinsakiIchiran";
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.label03, 0);
            this.Controls.SetChildIndex(this.label04, 0);
            this.Controls.SetChildIndex(this.label05, 0);
            this.Controls.SetChildIndex(this.label06, 0);
            this.Controls.SetChildIndex(this.Nyuukinsaki_FURIGANA, 0);
            this.Controls.SetChildIndex(this.Nyuukinsaki_NAME1, 0);
            this.Controls.SetChildIndex(this.Nyuukinsaki_NAME2, 0);
            this.Controls.SetChildIndex(this.Nyuukinsaki_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.label001, 0);
            this.Controls.SetChildIndex(this.label003, 0);
            this.Controls.SetChildIndex(this.Nyuukinsaki_TODOUFUKEN_NAME, 0);
            this.Controls.SetChildIndex(this.Nyuukinsaki_TODOUFUKEN_CD, 0);
            this.Controls.SetChildIndex(this.Nyuukinsaki_ADDRESS, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.ICHIRAN_HYOUJI_JOUKEN_DELETED, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomTextBox Nyuukinsaki_NAME_RYAKU;
        internal r_framework.CustomControl.CustomTextBox Nyuukinsaki_NAME2;
        internal r_framework.CustomControl.CustomTextBox Nyuukinsaki_NAME1;
        internal r_framework.CustomControl.CustomTextBox Nyuukinsaki_FURIGANA;
        private System.Windows.Forms.Label label06;
        private System.Windows.Forms.Label label05;
        private System.Windows.Forms.Label label04;
        private System.Windows.Forms.Label label03;
        internal r_framework.CustomControl.CustomTextBox Nyuukinsaki_ADDRESS;
        internal r_framework.CustomControl.CustomNumericTextBox2 Nyuukinsaki_TODOUFUKEN_CD;
        internal r_framework.CustomControl.CustomTextBox Nyuukinsaki_TODOUFUKEN_NAME;
        private System.Windows.Forms.Label label003;
        private System.Windows.Forms.Label label001;
        public System.Windows.Forms.CheckBox ICHIRAN_HYOUJI_JOUKEN_DELETED;
        public System.Windows.Forms.Label label1;
    }
}
