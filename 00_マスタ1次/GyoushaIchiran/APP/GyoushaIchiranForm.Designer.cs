namespace GyoushaIchiran.APP
{
    partial class GyoushaIchiranForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GyoushaIchiranForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.GYOUSHA_NAME1 = new r_framework.CustomControl.CustomTextBox();
            this.GYOUSHA_NAME2 = new r_framework.CustomControl.CustomTextBox();
            this.GYOUSHA_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.RYAKUSHOU = new System.Windows.Forms.Label();
            this.GYOUSHA2 = new System.Windows.Forms.Label();
            this.GYOUSHA1 = new System.Windows.Forms.Label();
            this.GYOUSHA_FURIGANA = new r_framework.CustomControl.CustomTextBox();
            this.FURIGANA_LABEL = new System.Windows.Forms.Label();
            this.GYOUSHA_ADDRESS = new r_framework.CustomControl.CustomTextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.TEKIYOU_END = new r_framework.CustomControl.CustomDateTimePicker();
            this.TEKIYOU_BEGIN = new r_framework.CustomControl.CustomDateTimePicker();
            this.TEKIYOU_LABEL = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TODOUFUKEN_NAME = new r_framework.CustomControl.CustomTextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.GYOUSHA_TODOUFUKEN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.TORIHIKISAKI_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.label06 = new System.Windows.Forms.Label();
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI = new System.Windows.Forms.CheckBox();
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED = new System.Windows.Forms.CheckBox();
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TORIHIKISAKI_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.TORIHIKISAKI_RNAME = new r_framework.CustomControl.CustomTextBox();
            this.TORIHIKISAKI_LABEL = new System.Windows.Forms.Label();
            this.TORIHIKISAKI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GYOUSHA_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.GYOUSHA_RNAME = new r_framework.CustomControl.CustomTextBox();
            this.GYOUSHA_LABEL = new System.Windows.Forms.Label();
            this.GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.searchString.Location = new System.Drawing.Point(4, 4);
            this.searchString.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.searchString.ReadOnly = true;
            this.searchString.Size = new System.Drawing.Size(1328, 188);
            this.searchString.TabIndex = 121;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Location = new System.Drawing.Point(5, 569);
            this.bt_ptn1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.bt_ptn1.Size = new System.Drawing.Size(259, 32);
            this.bt_ptn1.TabIndex = 201;
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.bt_ptn2.Size = new System.Drawing.Size(259, 32);
            this.bt_ptn2.TabIndex = 202;
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Location = new System.Drawing.Point(539, 569);
            this.bt_ptn3.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.bt_ptn3.Size = new System.Drawing.Size(259, 32);
            this.bt_ptn3.TabIndex = 203;
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Location = new System.Drawing.Point(805, 569);
            this.bt_ptn4.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.bt_ptn4.Size = new System.Drawing.Size(259, 32);
            this.bt_ptn4.TabIndex = 204;
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Location = new System.Drawing.Point(1072, 569);
            this.bt_ptn5.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.bt_ptn5.Size = new System.Drawing.Size(259, 32);
            this.bt_ptn5.TabIndex = 205;
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.Location = new System.Drawing.Point(5, 201);
            this.customSortHeader1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.customSortHeader1.Size = new System.Drawing.Size(1325, 40);
            this.customSortHeader1.TabIndex = 120;
            // 
            // customSearchHeader1
            // 
            this.customSearchHeader1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.customSearchHeader1.Size = new System.Drawing.Size(1772, 35);
            // 
            // GYOUSHA_NAME1
            // 
            this.GYOUSHA_NAME1.BackColor = System.Drawing.SystemColors.Window;
            this.GYOUSHA_NAME1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_NAME1.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.GYOUSHA_NAME1.CopyAutoSetControl = "";
            this.GYOUSHA_NAME1.DBFieldsName = "GYOUSHA_NAME1";
            this.GYOUSHA_NAME1.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_NAME1.DisplayItemName = "業者名1";
            this.GYOUSHA_NAME1.DisplayPopUp = null;
            this.GYOUSHA_NAME1.ErrorMessage = "";
            this.GYOUSHA_NAME1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME1.FocusOutCheckMethod")));
            this.GYOUSHA_NAME1.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GYOUSHA_NAME1.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_NAME1.FuriganaAutoSetControl = "";
            this.GYOUSHA_NAME1.GetCodeMasterField = "";
            this.GYOUSHA_NAME1.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.GYOUSHA_NAME1.IsInputErrorOccured = false;
            this.GYOUSHA_NAME1.ItemDefinedTypes = "varchar";
            this.GYOUSHA_NAME1.Location = new System.Drawing.Point(171, 96);
            this.GYOUSHA_NAME1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GYOUSHA_NAME1.MaxLength = 40;
            this.GYOUSHA_NAME1.Name = "GYOUSHA_NAME1";
            this.GYOUSHA_NAME1.PopupAfterExecute = null;
            this.GYOUSHA_NAME1.PopupBeforeExecute = null;
            this.GYOUSHA_NAME1.PopupGetMasterField = "";
            this.GYOUSHA_NAME1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_NAME1.PopupSearchSendParams")));
            this.GYOUSHA_NAME1.PopupSetFormField = "";
            this.GYOUSHA_NAME1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_NAME1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_NAME1.popupWindowSetting")));
            this.GYOUSHA_NAME1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME1.RegistCheckMethod")));
            this.GYOUSHA_NAME1.SetFormField = "";
            this.GYOUSHA_NAME1.ShortItemName = "業者名1";
            this.GYOUSHA_NAME1.Size = new System.Drawing.Size(447, 24);
            this.GYOUSHA_NAME1.TabIndex = 105;
            this.GYOUSHA_NAME1.Tag = "全角２０文字以内で入力してください";
            // 
            // GYOUSHA_NAME2
            // 
            this.GYOUSHA_NAME2.BackColor = System.Drawing.SystemColors.Window;
            this.GYOUSHA_NAME2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_NAME2.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.GYOUSHA_NAME2.DBFieldsName = "GYOUSHA_NAME2";
            this.GYOUSHA_NAME2.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_NAME2.DisplayItemName = "業者名2";
            this.GYOUSHA_NAME2.DisplayPopUp = null;
            this.GYOUSHA_NAME2.ErrorMessage = "";
            this.GYOUSHA_NAME2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME2.FocusOutCheckMethod")));
            this.GYOUSHA_NAME2.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GYOUSHA_NAME2.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_NAME2.GetCodeMasterField = "";
            this.GYOUSHA_NAME2.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.GYOUSHA_NAME2.IsInputErrorOccured = false;
            this.GYOUSHA_NAME2.ItemDefinedTypes = "varchar";
            this.GYOUSHA_NAME2.Location = new System.Drawing.Point(171, 124);
            this.GYOUSHA_NAME2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GYOUSHA_NAME2.MaxLength = 40;
            this.GYOUSHA_NAME2.Name = "GYOUSHA_NAME2";
            this.GYOUSHA_NAME2.PopupAfterExecute = null;
            this.GYOUSHA_NAME2.PopupBeforeExecute = null;
            this.GYOUSHA_NAME2.PopupGetMasterField = "";
            this.GYOUSHA_NAME2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_NAME2.PopupSearchSendParams")));
            this.GYOUSHA_NAME2.PopupSetFormField = "";
            this.GYOUSHA_NAME2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_NAME2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_NAME2.popupWindowSetting")));
            this.GYOUSHA_NAME2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME2.RegistCheckMethod")));
            this.GYOUSHA_NAME2.SetFormField = "";
            this.GYOUSHA_NAME2.ShortItemName = "業者名2";
            this.GYOUSHA_NAME2.Size = new System.Drawing.Size(447, 24);
            this.GYOUSHA_NAME2.TabIndex = 107;
            this.GYOUSHA_NAME2.Tag = "全角２０文字以内で入力してください";
            // 
            // GYOUSHA_NAME_RYAKU
            // 
            this.GYOUSHA_NAME_RYAKU.BackColor = System.Drawing.SystemColors.Window;
            this.GYOUSHA_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.GYOUSHA_NAME_RYAKU.DBFieldsName = "GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_NAME_RYAKU.DisplayItemName = "略称名";
            this.GYOUSHA_NAME_RYAKU.DisplayPopUp = null;
            this.GYOUSHA_NAME_RYAKU.ErrorMessage = "";
            this.GYOUSHA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.FocusOutCheckMethod")));
            this.GYOUSHA_NAME_RYAKU.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GYOUSHA_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_NAME_RYAKU.GetCodeMasterField = "";
            this.GYOUSHA_NAME_RYAKU.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.GYOUSHA_NAME_RYAKU.IsInputErrorOccured = false;
            this.GYOUSHA_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.GYOUSHA_NAME_RYAKU.Location = new System.Drawing.Point(828, 40);
            this.GYOUSHA_NAME_RYAKU.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GYOUSHA_NAME_RYAKU.MaxLength = 40;
            this.GYOUSHA_NAME_RYAKU.Name = "GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_NAME_RYAKU.PopupAfterExecute = null;
            this.GYOUSHA_NAME_RYAKU.PopupBeforeExecute = null;
            this.GYOUSHA_NAME_RYAKU.PopupGetMasterField = "";
            this.GYOUSHA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.PopupSearchSendParams")));
            this.GYOUSHA_NAME_RYAKU.PopupSetFormField = "";
            this.GYOUSHA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.popupWindowSetting")));
            this.GYOUSHA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.RegistCheckMethod")));
            this.GYOUSHA_NAME_RYAKU.SetFormField = "";
            this.GYOUSHA_NAME_RYAKU.ShortItemName = "略称名";
            this.GYOUSHA_NAME_RYAKU.Size = new System.Drawing.Size(379, 24);
            this.GYOUSHA_NAME_RYAKU.TabIndex = 110;
            this.GYOUSHA_NAME_RYAKU.Tag = "全角２０文字以内で入力してください";
            // 
            // RYAKUSHOU
            // 
            this.RYAKUSHOU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.RYAKUSHOU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RYAKUSHOU.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.RYAKUSHOU.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.RYAKUSHOU.ForeColor = System.Drawing.Color.White;
            this.RYAKUSHOU.Location = new System.Drawing.Point(673, 40);
            this.RYAKUSHOU.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.RYAKUSHOU.Name = "RYAKUSHOU";
            this.RYAKUSHOU.Size = new System.Drawing.Size(146, 26);
            this.RYAKUSHOU.TabIndex = 108;
            this.RYAKUSHOU.Text = "業者略称";
            this.RYAKUSHOU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GYOUSHA2
            // 
            this.GYOUSHA2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.GYOUSHA2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GYOUSHA2.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GYOUSHA2.ForeColor = System.Drawing.Color.White;
            this.GYOUSHA2.Location = new System.Drawing.Point(16, 124);
            this.GYOUSHA2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.GYOUSHA2.Name = "GYOUSHA2";
            this.GYOUSHA2.Size = new System.Drawing.Size(146, 26);
            this.GYOUSHA2.TabIndex = 106;
            this.GYOUSHA2.Text = "業者名２";
            this.GYOUSHA2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GYOUSHA1
            // 
            this.GYOUSHA1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.GYOUSHA1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GYOUSHA1.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GYOUSHA1.ForeColor = System.Drawing.Color.White;
            this.GYOUSHA1.Location = new System.Drawing.Point(16, 96);
            this.GYOUSHA1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.GYOUSHA1.Name = "GYOUSHA1";
            this.GYOUSHA1.Size = new System.Drawing.Size(146, 26);
            this.GYOUSHA1.TabIndex = 104;
            this.GYOUSHA1.Text = "業者名１";
            this.GYOUSHA1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GYOUSHA_FURIGANA
            // 
            this.GYOUSHA_FURIGANA.BackColor = System.Drawing.SystemColors.Window;
            this.GYOUSHA_FURIGANA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_FURIGANA.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.GYOUSHA_FURIGANA.DBFieldsName = "GYOUSHA_FURIGANA";
            this.GYOUSHA_FURIGANA.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_FURIGANA.DisplayItemName = "フリガナ";
            this.GYOUSHA_FURIGANA.DisplayPopUp = null;
            this.GYOUSHA_FURIGANA.ErrorMessage = "";
            this.GYOUSHA_FURIGANA.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_FURIGANA.FocusOutCheckMethod")));
            this.GYOUSHA_FURIGANA.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GYOUSHA_FURIGANA.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_FURIGANA.GetCodeMasterField = "";
            this.GYOUSHA_FURIGANA.ImeMode = System.Windows.Forms.ImeMode.Katakana;
            this.GYOUSHA_FURIGANA.IsInputErrorOccured = false;
            this.GYOUSHA_FURIGANA.ItemDefinedTypes = "varchar";
            this.GYOUSHA_FURIGANA.Location = new System.Drawing.Point(171, 68);
            this.GYOUSHA_FURIGANA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GYOUSHA_FURIGANA.MaxLength = 80;
            this.GYOUSHA_FURIGANA.Name = "GYOUSHA_FURIGANA";
            this.GYOUSHA_FURIGANA.PopupAfterExecute = null;
            this.GYOUSHA_FURIGANA.PopupBeforeExecute = null;
            this.GYOUSHA_FURIGANA.PopupGetMasterField = "";
            this.GYOUSHA_FURIGANA.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_FURIGANA.PopupSearchSendParams")));
            this.GYOUSHA_FURIGANA.PopupSetFormField = "";
            this.GYOUSHA_FURIGANA.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_FURIGANA.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_FURIGANA.popupWindowSetting")));
            this.GYOUSHA_FURIGANA.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_FURIGANA.RegistCheckMethod")));
            this.GYOUSHA_FURIGANA.SetFormField = "";
            this.GYOUSHA_FURIGANA.ShortItemName = "フリガナ";
            this.GYOUSHA_FURIGANA.Size = new System.Drawing.Size(447, 24);
            this.GYOUSHA_FURIGANA.TabIndex = 103;
            this.GYOUSHA_FURIGANA.Tag = "全角４０文字以内で入力してください";
            // 
            // FURIGANA_LABEL
            // 
            this.FURIGANA_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.FURIGANA_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FURIGANA_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FURIGANA_LABEL.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FURIGANA_LABEL.ForeColor = System.Drawing.Color.White;
            this.FURIGANA_LABEL.Location = new System.Drawing.Point(16, 68);
            this.FURIGANA_LABEL.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.FURIGANA_LABEL.Name = "FURIGANA_LABEL";
            this.FURIGANA_LABEL.Size = new System.Drawing.Size(146, 26);
            this.FURIGANA_LABEL.TabIndex = 102;
            this.FURIGANA_LABEL.Text = "フリガナ";
            this.FURIGANA_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GYOUSHA_ADDRESS
            // 
            this.GYOUSHA_ADDRESS.BackColor = System.Drawing.SystemColors.Window;
            this.GYOUSHA_ADDRESS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_ADDRESS.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.GYOUSHA_ADDRESS.DBFieldsName = "GYOUSHA_ADDRESS";
            this.GYOUSHA_ADDRESS.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_ADDRESS.DisplayItemName = "業者住所";
            this.GYOUSHA_ADDRESS.DisplayPopUp = null;
            this.GYOUSHA_ADDRESS.ErrorMessage = "";
            this.GYOUSHA_ADDRESS.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_ADDRESS.FocusOutCheckMethod")));
            this.GYOUSHA_ADDRESS.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GYOUSHA_ADDRESS.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_ADDRESS.GetCodeMasterField = "";
            this.GYOUSHA_ADDRESS.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.GYOUSHA_ADDRESS.IsInputErrorOccured = false;
            this.GYOUSHA_ADDRESS.ItemDefinedTypes = "varchar";
            this.GYOUSHA_ADDRESS.Location = new System.Drawing.Point(828, 96);
            this.GYOUSHA_ADDRESS.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GYOUSHA_ADDRESS.MaxLength = 80;
            this.GYOUSHA_ADDRESS.Name = "GYOUSHA_ADDRESS";
            this.GYOUSHA_ADDRESS.PopupAfterExecute = null;
            this.GYOUSHA_ADDRESS.PopupBeforeExecute = null;
            this.GYOUSHA_ADDRESS.PopupGetMasterField = "";
            this.GYOUSHA_ADDRESS.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_ADDRESS.PopupSearchSendParams")));
            this.GYOUSHA_ADDRESS.PopupSetFormField = "";
            this.GYOUSHA_ADDRESS.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_ADDRESS.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_ADDRESS.popupWindowSetting")));
            this.GYOUSHA_ADDRESS.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_ADDRESS.RegistCheckMethod")));
            this.GYOUSHA_ADDRESS.SetFormField = "";
            this.GYOUSHA_ADDRESS.ShortItemName = "業者住所";
            this.GYOUSHA_ADDRESS.Size = new System.Drawing.Size(447, 24);
            this.GYOUSHA_ADDRESS.TabIndex = 114;
            this.GYOUSHA_ADDRESS.Tag = "全角４０文字以内で入力してください";
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label26.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label26.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label26.ForeColor = System.Drawing.Color.White;
            this.label26.Location = new System.Drawing.Point(673, 96);
            this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(146, 26);
            this.label26.TabIndex = 113;
            this.label26.Text = "住所";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TEKIYOU_END
            // 
            this.TEKIYOU_END.BackColor = System.Drawing.SystemColors.Window;
            this.TEKIYOU_END.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TEKIYOU_END.CalendarFont = new System.Drawing.Font("MS Gothic", 9F);
            this.TEKIYOU_END.Checked = false;
            this.TEKIYOU_END.DateTimeNowYear = "";
            this.TEKIYOU_END.DBFieldsName = "TEKIYOU_END";
            this.TEKIYOU_END.DefaultBackColor = System.Drawing.Color.Empty;
            this.TEKIYOU_END.DisplayItemName = "適用終了";
            this.TEKIYOU_END.DisplayPopUp = null;
            this.TEKIYOU_END.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TEKIYOU_END.FocusOutCheckMethod")));
            this.TEKIYOU_END.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.TEKIYOU_END.ForeColor = System.Drawing.Color.Black;
            this.TEKIYOU_END.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TEKIYOU_END.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TEKIYOU_END.IsInputErrorOccured = false;
            this.TEKIYOU_END.ItemDefinedTypes = "datetime";
            this.TEKIYOU_END.Location = new System.Drawing.Point(1037, 124);
            this.TEKIYOU_END.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TEKIYOU_END.MaxLength = 10;
            this.TEKIYOU_END.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.TEKIYOU_END.Name = "TEKIYOU_END";
            this.TEKIYOU_END.NullValue = "";
            this.TEKIYOU_END.PopupAfterExecute = null;
            this.TEKIYOU_END.PopupBeforeExecute = null;
            this.TEKIYOU_END.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TEKIYOU_END.PopupSearchSendParams")));
            this.TEKIYOU_END.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TEKIYOU_END.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TEKIYOU_END.popupWindowSetting")));
            this.TEKIYOU_END.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TEKIYOU_END.RegistCheckMethod")));
            this.TEKIYOU_END.ShortItemName = "適用終了";
            this.TEKIYOU_END.Size = new System.Drawing.Size(165, 24);
            this.TEKIYOU_END.TabIndex = 118;
            this.TEKIYOU_END.Tag = "適用終了日を入力してください";
            this.TEKIYOU_END.Value = null;
            // 
            // TEKIYOU_BEGIN
            // 
            this.TEKIYOU_BEGIN.BackColor = System.Drawing.SystemColors.Window;
            this.TEKIYOU_BEGIN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TEKIYOU_BEGIN.CalendarFont = new System.Drawing.Font("MS Gothic", 9F);
            this.TEKIYOU_BEGIN.Checked = false;
            this.TEKIYOU_BEGIN.DateTimeNowYear = "";
            this.TEKIYOU_BEGIN.DBFieldsName = "TEKIYOU_BEGIN";
            this.TEKIYOU_BEGIN.DefaultBackColor = System.Drawing.Color.Empty;
            this.TEKIYOU_BEGIN.DisplayItemName = "適用開始";
            this.TEKIYOU_BEGIN.DisplayPopUp = null;
            this.TEKIYOU_BEGIN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TEKIYOU_BEGIN.FocusOutCheckMethod")));
            this.TEKIYOU_BEGIN.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.TEKIYOU_BEGIN.ForeColor = System.Drawing.Color.Black;
            this.TEKIYOU_BEGIN.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TEKIYOU_BEGIN.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TEKIYOU_BEGIN.IsInputErrorOccured = false;
            this.TEKIYOU_BEGIN.ItemDefinedTypes = "datetime";
            this.TEKIYOU_BEGIN.Location = new System.Drawing.Point(828, 124);
            this.TEKIYOU_BEGIN.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TEKIYOU_BEGIN.MaxLength = 10;
            this.TEKIYOU_BEGIN.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.TEKIYOU_BEGIN.Name = "TEKIYOU_BEGIN";
            this.TEKIYOU_BEGIN.NullValue = "";
            this.TEKIYOU_BEGIN.PopupAfterExecute = null;
            this.TEKIYOU_BEGIN.PopupBeforeExecute = null;
            this.TEKIYOU_BEGIN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TEKIYOU_BEGIN.PopupSearchSendParams")));
            this.TEKIYOU_BEGIN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TEKIYOU_BEGIN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TEKIYOU_BEGIN.popupWindowSetting")));
            this.TEKIYOU_BEGIN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TEKIYOU_BEGIN.RegistCheckMethod")));
            this.TEKIYOU_BEGIN.ShortItemName = "適用開始";
            this.TEKIYOU_BEGIN.Size = new System.Drawing.Size(165, 24);
            this.TEKIYOU_BEGIN.TabIndex = 116;
            this.TEKIYOU_BEGIN.Tag = "適用開始日を入力してください";
            this.TEKIYOU_BEGIN.Value = null;
            // 
            // TEKIYOU_LABEL
            // 
            this.TEKIYOU_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.TEKIYOU_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TEKIYOU_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TEKIYOU_LABEL.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TEKIYOU_LABEL.ForeColor = System.Drawing.Color.White;
            this.TEKIYOU_LABEL.Location = new System.Drawing.Point(673, 124);
            this.TEKIYOU_LABEL.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TEKIYOU_LABEL.Name = "TEKIYOU_LABEL";
            this.TEKIYOU_LABEL.Size = new System.Drawing.Size(146, 26);
            this.TEKIYOU_LABEL.TabIndex = 115;
            this.TEKIYOU_LABEL.Text = "適用期間";
            this.TEKIYOU_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.label1.Location = new System.Drawing.Point(1001, 131);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(26, 17);
            this.label1.TabIndex = 117;
            this.label1.Text = "～";
            // 
            // TODOUFUKEN_NAME
            // 
            this.TODOUFUKEN_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.TODOUFUKEN_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TODOUFUKEN_NAME.CharactersNumber = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.TODOUFUKEN_NAME.DBFieldsName = "TODOUFUKEN_NAME";
            this.TODOUFUKEN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.TODOUFUKEN_NAME.DisplayItemName = "都道府県名";
            this.TODOUFUKEN_NAME.DisplayPopUp = null;
            this.TODOUFUKEN_NAME.Enabled = false;
            this.TODOUFUKEN_NAME.ErrorMessage = "";
            this.TODOUFUKEN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TODOUFUKEN_NAME.FocusOutCheckMethod")));
            this.TODOUFUKEN_NAME.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TODOUFUKEN_NAME.ForeColor = System.Drawing.Color.Black;
            this.TODOUFUKEN_NAME.GetCodeMasterField = "";
            this.TODOUFUKEN_NAME.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TODOUFUKEN_NAME.IsInputErrorOccured = false;
            this.TODOUFUKEN_NAME.ItemDefinedTypes = "varchar";
            this.TODOUFUKEN_NAME.Location = new System.Drawing.Point(853, 68);
            this.TODOUFUKEN_NAME.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TODOUFUKEN_NAME.MaxLength = 0;
            this.TODOUFUKEN_NAME.Name = "TODOUFUKEN_NAME";
            this.TODOUFUKEN_NAME.PopupAfterExecute = null;
            this.TODOUFUKEN_NAME.PopupBeforeExecute = null;
            this.TODOUFUKEN_NAME.PopupGetMasterField = "";
            this.TODOUFUKEN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TODOUFUKEN_NAME.PopupSearchSendParams")));
            this.TODOUFUKEN_NAME.PopupSetFormField = "";
            this.TODOUFUKEN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TODOUFUKEN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TODOUFUKEN_NAME.popupWindowSetting")));
            this.TODOUFUKEN_NAME.ReadOnly = true;
            this.TODOUFUKEN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TODOUFUKEN_NAME.RegistCheckMethod")));
            this.TODOUFUKEN_NAME.SetFormField = "";
            this.TODOUFUKEN_NAME.ShortItemName = "都道府県名";
            this.TODOUFUKEN_NAME.Size = new System.Drawing.Size(86, 24);
            this.TODOUFUKEN_NAME.TabIndex = 112;
            this.TODOUFUKEN_NAME.TabStop = false;
            this.TODOUFUKEN_NAME.Tag = "都道府県名が表示されます";
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label25.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label25.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label25.ForeColor = System.Drawing.Color.White;
            this.label25.Location = new System.Drawing.Point(673, 68);
            this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(146, 26);
            this.label25.TabIndex = 110;
            this.label25.Text = "都道府県";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GYOUSHA_TODOUFUKEN_CD
            // 
            this.GYOUSHA_TODOUFUKEN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.GYOUSHA_TODOUFUKEN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_TODOUFUKEN_CD.CustomFormatSetting = "00";
            this.GYOUSHA_TODOUFUKEN_CD.DBFieldsName = "GYOUSHA_TODOUFUKEN_CD";
            this.GYOUSHA_TODOUFUKEN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_TODOUFUKEN_CD.DisplayItemName = "都道府県CD";
            this.GYOUSHA_TODOUFUKEN_CD.DisplayPopUp = null;
            this.GYOUSHA_TODOUFUKEN_CD.ErrorMessage = "";
            this.GYOUSHA_TODOUFUKEN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_TODOUFUKEN_CD.FocusOutCheckMethod")));
            this.GYOUSHA_TODOUFUKEN_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GYOUSHA_TODOUFUKEN_CD.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_TODOUFUKEN_CD.FormatSetting = "カスタム";
            this.GYOUSHA_TODOUFUKEN_CD.GetCodeMasterField = "TODOUFUKEN_CD,TODOUFUKEN_NAME";
            this.GYOUSHA_TODOUFUKEN_CD.IsInputErrorOccured = false;
            this.GYOUSHA_TODOUFUKEN_CD.ItemDefinedTypes = "smallint";
            this.GYOUSHA_TODOUFUKEN_CD.Location = new System.Drawing.Point(828, 68);
            this.GYOUSHA_TODOUFUKEN_CD.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GYOUSHA_TODOUFUKEN_CD.Name = "GYOUSHA_TODOUFUKEN_CD";
            this.GYOUSHA_TODOUFUKEN_CD.PopupAfterExecute = null;
            this.GYOUSHA_TODOUFUKEN_CD.PopupBeforeExecute = null;
            this.GYOUSHA_TODOUFUKEN_CD.PopupGetMasterField = "TODOUFUKEN_CD,TODOUFUKEN_NAME";
            this.GYOUSHA_TODOUFUKEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_TODOUFUKEN_CD.PopupSearchSendParams")));
            this.GYOUSHA_TODOUFUKEN_CD.PopupSetFormField = "GYOUSHA_TODOUFUKEN_CD,TODOUFUKEN_NAME";
            this.GYOUSHA_TODOUFUKEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_TODOUFUKEN;
            this.GYOUSHA_TODOUFUKEN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.GYOUSHA_TODOUFUKEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_TODOUFUKEN_CD.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.GYOUSHA_TODOUFUKEN_CD.RangeSetting = rangeSettingDto1;
            this.GYOUSHA_TODOUFUKEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_TODOUFUKEN_CD.RegistCheckMethod")));
            this.GYOUSHA_TODOUFUKEN_CD.SetFormField = "GYOUSHA_TODOUFUKEN_CD,TODOUFUKEN_NAME";
            this.GYOUSHA_TODOUFUKEN_CD.ShortItemName = "都道府県CD";
            this.GYOUSHA_TODOUFUKEN_CD.Size = new System.Drawing.Size(26, 24);
            this.GYOUSHA_TODOUFUKEN_CD.TabIndex = 111;
            this.GYOUSHA_TODOUFUKEN_CD.Tag = "都道府県を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GYOUSHA_TODOUFUKEN_CD.WordWrap = false;
            // 
            // TORIHIKISAKI_NAME_RYAKU
            // 
            this.TORIHIKISAKI_NAME_RYAKU.BackColor = System.Drawing.SystemColors.Window;
            this.TORIHIKISAKI_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.TORIHIKISAKI_NAME_RYAKU.DBFieldsName = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_NAME_RYAKU.DisplayItemName = "取引先略称名";
            this.TORIHIKISAKI_NAME_RYAKU.DisplayPopUp = null;
            this.TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TORIHIKISAKI_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_NAME_RYAKU.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.TORIHIKISAKI_NAME_RYAKU.IsInputErrorOccured = false;
            this.TORIHIKISAKI_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_NAME_RYAKU.Location = new System.Drawing.Point(828, 12);
            this.TORIHIKISAKI_NAME_RYAKU.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TORIHIKISAKI_NAME_RYAKU.MaxLength = 40;
            this.TORIHIKISAKI_NAME_RYAKU.Name = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_RYAKU.PopupAfterExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU.PopupBeforeExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams")));
            this.TORIHIKISAKI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.popupWindowSetting")));
            this.TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU.ShortItemName = "取引先略称名";
            this.TORIHIKISAKI_NAME_RYAKU.Size = new System.Drawing.Size(379, 24);
            this.TORIHIKISAKI_NAME_RYAKU.TabIndex = 109;
            this.TORIHIKISAKI_NAME_RYAKU.Tag = "全角２０文字以内で入力してください";
            // 
            // label06
            // 
            this.label06.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label06.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label06.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label06.ForeColor = System.Drawing.Color.White;
            this.label06.Location = new System.Drawing.Point(673, 12);
            this.label06.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label06.Name = "label06";
            this.label06.Size = new System.Drawing.Size(146, 26);
            this.label06.TabIndex = 100;
            this.label06.Text = "取引先略称";
            this.label06.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Location = new System.Drawing.Point(988, 155);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Name = "ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Size = new System.Drawing.Size(100, 21);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.TabIndex = 122;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Text = "適用期間外";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.UseVisualStyleBackColor = true;
            // 
            // ICHIRAN_HYOUJI_JOUKEN_DELETED
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Location = new System.Drawing.Point(916, 155);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Name = "ICHIRAN_HYOUJI_JOUKEN_DELETED";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Size = new System.Drawing.Size(58, 21);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.TabIndex = 121;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Text = "削除";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.UseVisualStyleBackColor = true;
            // 
            // ICHIRAN_HYOUJI_JOUKEN_TEKIYOU
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Location = new System.Drawing.Point(828, 155);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Name = "ICHIRAN_HYOUJI_JOUKEN_TEKIYOU";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Size = new System.Drawing.Size(72, 21);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.TabIndex = 120;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Text = "適用中";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(673, 152);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 26);
            this.label2.TabIndex = 420;
            this.label2.Text = "表示条件";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.TORIHIKISAKI_SEARCH_BUTTON.Location = new System.Drawing.Point(625, 11);
            this.TORIHIKISAKI_SEARCH_BUTTON.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TORIHIKISAKI_SEARCH_BUTTON.Name = "TORIHIKISAKI_SEARCH_BUTTON";
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupAfterExecute = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupGetMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_SEARCH_BUTTON.PopupSearchSendParams")));
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupSetFormField = "TORIHIKISAKI_CD, TORIHIKISAKI_RNAME";
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupWindowName = "検索共通ポップアップ";
            this.TORIHIKISAKI_SEARCH_BUTTON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_SEARCH_BUTTON.popupWindowSetting")));
            this.TORIHIKISAKI_SEARCH_BUTTON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_SEARCH_BUTTON.RegistCheckMethod")));
            this.TORIHIKISAKI_SEARCH_BUTTON.SearchDisplayFlag = 0;
            this.TORIHIKISAKI_SEARCH_BUTTON.SetFormField = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.ShortItemName = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.Size = new System.Drawing.Size(29, 29);
            this.TORIHIKISAKI_SEARCH_BUTTON.TabIndex = 759;
            this.TORIHIKISAKI_SEARCH_BUTTON.TabStop = false;
            this.TORIHIKISAKI_SEARCH_BUTTON.UseVisualStyleBackColor = false;
            this.TORIHIKISAKI_SEARCH_BUTTON.ZeroPaddengFlag = false;
            // 
            // TORIHIKISAKI_RNAME
            // 
            this.TORIHIKISAKI_RNAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.TORIHIKISAKI_RNAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_RNAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.TORIHIKISAKI_RNAME.DBFieldsName = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_RNAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_RNAME.DisplayPopUp = null;
            this.TORIHIKISAKI_RNAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_RNAME.FocusOutCheckMethod")));
            this.TORIHIKISAKI_RNAME.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.TORIHIKISAKI_RNAME.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_RNAME.IsInputErrorOccured = false;
            this.TORIHIKISAKI_RNAME.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_RNAME.Location = new System.Drawing.Point(236, 12);
            this.TORIHIKISAKI_RNAME.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TORIHIKISAKI_RNAME.MaxLength = 40;
            this.TORIHIKISAKI_RNAME.Name = "TORIHIKISAKI_RNAME";
            this.TORIHIKISAKI_RNAME.PopupAfterExecute = null;
            this.TORIHIKISAKI_RNAME.PopupBeforeExecute = null;
            this.TORIHIKISAKI_RNAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_RNAME.PopupSearchSendParams")));
            this.TORIHIKISAKI_RNAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_RNAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_RNAME.popupWindowSetting")));
            this.TORIHIKISAKI_RNAME.ReadOnly = true;
            this.TORIHIKISAKI_RNAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_RNAME.RegistCheckMethod")));
            this.TORIHIKISAKI_RNAME.Size = new System.Drawing.Size(382, 24);
            this.TORIHIKISAKI_RNAME.TabIndex = 758;
            this.TORIHIKISAKI_RNAME.TabStop = false;
            this.TORIHIKISAKI_RNAME.Tag = " ";
            // 
            // TORIHIKISAKI_LABEL
            // 
            this.TORIHIKISAKI_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.TORIHIKISAKI_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TORIHIKISAKI_LABEL.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.TORIHIKISAKI_LABEL.ForeColor = System.Drawing.Color.White;
            this.TORIHIKISAKI_LABEL.Location = new System.Drawing.Point(16, 12);
            this.TORIHIKISAKI_LABEL.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TORIHIKISAKI_LABEL.Name = "TORIHIKISAKI_LABEL";
            this.TORIHIKISAKI_LABEL.Size = new System.Drawing.Size(146, 26);
            this.TORIHIKISAKI_LABEL.TabIndex = 760;
            this.TORIHIKISAKI_LABEL.Text = "取引先";
            this.TORIHIKISAKI_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TORIHIKISAKI_CD
            // 
            this.TORIHIKISAKI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.TORIHIKISAKI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_CD.ChangeUpperCase = true;
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
            this.TORIHIKISAKI_CD.GetCodeMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TORIHIKISAKI_CD.IsInputErrorOccured = false;
            this.TORIHIKISAKI_CD.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_CD.Location = new System.Drawing.Point(171, 12);
            this.TORIHIKISAKI_CD.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TORIHIKISAKI_CD.MaxLength = 6;
            this.TORIHIKISAKI_CD.Name = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.PopupAfterExecute = null;
            this.TORIHIKISAKI_CD.PopupAfterExecuteMethod = "";
            this.TORIHIKISAKI_CD.PopupBeforeExecute = null;
            this.TORIHIKISAKI_CD.PopupGetMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_CD.PopupSearchSendParams")));
            this.TORIHIKISAKI_CD.PopupSetFormField = "TORIHIKISAKI_CD, TORIHIKISAKI_RNAME";
            this.TORIHIKISAKI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TORIHIKISAKI_CD.PopupWindowName = "検索共通ポップアップ";
            this.TORIHIKISAKI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_CD.popupWindowSetting")));
            this.TORIHIKISAKI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.RegistCheckMethod")));
            this.TORIHIKISAKI_CD.SetFormField = "TORIHIKISAKI_CD, TORIHIKISAKI_RNAME";
            this.TORIHIKISAKI_CD.Size = new System.Drawing.Size(66, 24);
            this.TORIHIKISAKI_CD.TabIndex = 102;
            this.TORIHIKISAKI_CD.Tag = "取引先を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.TORIHIKISAKI_CD.ZeroPaddengFlag = true;
            this.TORIHIKISAKI_CD.Validating += new System.ComponentModel.CancelEventHandler(this.TORIHIKISAKI_CD_Validating);
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
            this.GYOUSHA_SEARCH_BUTTON.Location = new System.Drawing.Point(625, 39);
            this.GYOUSHA_SEARCH_BUTTON.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GYOUSHA_SEARCH_BUTTON.Name = "GYOUSHA_SEARCH_BUTTON";
            this.GYOUSHA_SEARCH_BUTTON.PopupAfterExecute = null;
            this.GYOUSHA_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.GYOUSHA_SEARCH_BUTTON.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams")));
            this.GYOUSHA_SEARCH_BUTTON.PopupSetFormField = "GYOUSHA_CD, GYOUSHA_RNAME";
            this.GYOUSHA_SEARCH_BUTTON.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_SEARCH_BUTTON.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_SEARCH_BUTTON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.popupWindowSetting")));
            this.GYOUSHA_SEARCH_BUTTON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.RegistCheckMethod")));
            this.GYOUSHA_SEARCH_BUTTON.SearchDisplayFlag = 0;
            this.GYOUSHA_SEARCH_BUTTON.SetFormField = null;
            this.GYOUSHA_SEARCH_BUTTON.ShortItemName = null;
            this.GYOUSHA_SEARCH_BUTTON.Size = new System.Drawing.Size(29, 29);
            this.GYOUSHA_SEARCH_BUTTON.TabIndex = 763;
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
            this.GYOUSHA_RNAME.Location = new System.Drawing.Point(236, 40);
            this.GYOUSHA_RNAME.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GYOUSHA_RNAME.MaxLength = 40;
            this.GYOUSHA_RNAME.Name = "GYOUSHA_RNAME";
            this.GYOUSHA_RNAME.PopupAfterExecute = null;
            this.GYOUSHA_RNAME.PopupBeforeExecute = null;
            this.GYOUSHA_RNAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_RNAME.PopupSearchSendParams")));
            this.GYOUSHA_RNAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_RNAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_RNAME.popupWindowSetting")));
            this.GYOUSHA_RNAME.ReadOnly = true;
            this.GYOUSHA_RNAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_RNAME.RegistCheckMethod")));
            this.GYOUSHA_RNAME.Size = new System.Drawing.Size(382, 24);
            this.GYOUSHA_RNAME.TabIndex = 762;
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
            this.GYOUSHA_LABEL.Location = new System.Drawing.Point(16, 40);
            this.GYOUSHA_LABEL.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.GYOUSHA_LABEL.Name = "GYOUSHA_LABEL";
            this.GYOUSHA_LABEL.Size = new System.Drawing.Size(146, 26);
            this.GYOUSHA_LABEL.TabIndex = 764;
            this.GYOUSHA_LABEL.Text = "業者";
            this.GYOUSHA_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.GYOUSHA_CD.DisplayItemName = "業者";
            this.GYOUSHA_CD.DisplayPopUp = null;
            this.GYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.FocusOutCheckMethod")));
            this.GYOUSHA_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.GYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_CD.GetCodeMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GYOUSHA_CD.IsInputErrorOccured = false;
            this.GYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.GYOUSHA_CD.Location = new System.Drawing.Point(171, 40);
            this.GYOUSHA_CD.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.GYOUSHA_CD.MaxLength = 6;
            this.GYOUSHA_CD.Name = "GYOUSHA_CD";
            this.GYOUSHA_CD.PopupAfterExecute = null;
            this.GYOUSHA_CD.PopupAfterExecuteMethod = "";
            this.GYOUSHA_CD.PopupBeforeExecute = null;
            this.GYOUSHA_CD.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_CD.PopupSearchSendParams")));
            this.GYOUSHA_CD.PopupSetFormField = "GYOUSHA_CD, GYOUSHA_RNAME";
            this.GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_CD.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_CD.popupWindowSetting")));
            this.GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.RegistCheckMethod")));
            this.GYOUSHA_CD.SetFormField = "GYOUSHA_CD, GYOUSHA_RNAME";
            this.GYOUSHA_CD.Size = new System.Drawing.Size(66, 24);
            this.GYOUSHA_CD.TabIndex = 101;
            this.GYOUSHA_CD.Tag = "業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GYOUSHA_CD.ZeroPaddengFlag = true;
            this.GYOUSHA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.GYOUSHA_CD_Validating);
            // 
            // ISNOT_NEED_DELETE_FLG
            // 
            this.ISNOT_NEED_DELETE_FLG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ISNOT_NEED_DELETE_FLG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ISNOT_NEED_DELETE_FLG.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.ISNOT_NEED_DELETE_FLG.DBFieldsName = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.ISNOT_NEED_DELETE_FLG.DisplayPopUp = null;
            this.ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ISNOT_NEED_DELETE_FLG.ForeColor = System.Drawing.Color.Black;
            this.ISNOT_NEED_DELETE_FLG.IsInputErrorOccured = false;
            this.ISNOT_NEED_DELETE_FLG.ItemDefinedTypes = "bit";
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(539, 149);
            this.ISNOT_NEED_DELETE_FLG.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ISNOT_NEED_DELETE_FLG.MaxLength = 20;
            this.ISNOT_NEED_DELETE_FLG.Name = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.PopupAfterExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupBeforeExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.PopupSearchSendParams")));
            this.ISNOT_NEED_DELETE_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ISNOT_NEED_DELETE_FLG.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.popupWindowSetting")));
            this.ISNOT_NEED_DELETE_FLG.ReadOnly = true;
            this.ISNOT_NEED_DELETE_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.RegistCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.Size = new System.Drawing.Size(53, 24);
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 765;
            this.ISNOT_NEED_DELETE_FLG.TabStop = false;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // GyoushaIchiranForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.ClientSize = new System.Drawing.Size(1344, 611);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.GYOUSHA_SEARCH_BUTTON);
            this.Controls.Add(this.GYOUSHA_RNAME);
            this.Controls.Add(this.GYOUSHA_LABEL);
            this.Controls.Add(this.GYOUSHA_CD);
            this.Controls.Add(this.TORIHIKISAKI_SEARCH_BUTTON);
            this.Controls.Add(this.TORIHIKISAKI_RNAME);
            this.Controls.Add(this.TORIHIKISAKI_LABEL);
            this.Controls.Add(this.TORIHIKISAKI_CD);
            this.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI);
            this.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_DELETED);
            this.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TORIHIKISAKI_NAME_RYAKU);
            this.Controls.Add(this.label06);
            this.Controls.Add(this.TODOUFUKEN_NAME);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.GYOUSHA_TODOUFUKEN_CD);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TEKIYOU_END);
            this.Controls.Add(this.TEKIYOU_BEGIN);
            this.Controls.Add(this.TEKIYOU_LABEL);
            this.Controls.Add(this.GYOUSHA_ADDRESS);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.GYOUSHA_NAME1);
            this.Controls.Add(this.GYOUSHA_NAME2);
            this.Controls.Add(this.GYOUSHA_NAME_RYAKU);
            this.Controls.Add(this.RYAKUSHOU);
            this.Controls.Add(this.GYOUSHA2);
            this.Controls.Add(this.GYOUSHA1);
            this.Controls.Add(this.GYOUSHA_FURIGANA);
            this.Controls.Add(this.FURIGANA_LABEL);
            this.Margin = new System.Windows.Forms.Padding(7, 7, 7, 7);
            this.Name = "GyoushaIchiranForm";
            this.Text = "TorihikisakiIchiran";
            this.Shown += new System.EventHandler(this.GyoushaIchiranForm_Shown);
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.FURIGANA_LABEL, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_FURIGANA, 0);
            this.Controls.SetChildIndex(this.GYOUSHA1, 0);
            this.Controls.SetChildIndex(this.GYOUSHA2, 0);
            this.Controls.SetChildIndex(this.RYAKUSHOU, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_NAME2, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_NAME1, 0);
            this.Controls.SetChildIndex(this.label26, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_ADDRESS, 0);
            this.Controls.SetChildIndex(this.TEKIYOU_LABEL, 0);
            this.Controls.SetChildIndex(this.TEKIYOU_BEGIN, 0);
            this.Controls.SetChildIndex(this.TEKIYOU_END, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_TODOUFUKEN_CD, 0);
            this.Controls.SetChildIndex(this.label25, 0);
            this.Controls.SetChildIndex(this.TODOUFUKEN_NAME, 0);
            this.Controls.SetChildIndex(this.label06, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU, 0);
            this.Controls.SetChildIndex(this.ICHIRAN_HYOUJI_JOUKEN_DELETED, 0);
            this.Controls.SetChildIndex(this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_CD, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_LABEL, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_RNAME, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_SEARCH_BUTTON, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_CD, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_LABEL, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_RNAME, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_SEARCH_BUTTON, 0);
            this.Controls.SetChildIndex(this.ISNOT_NEED_DELETE_FLG, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomTextBox GYOUSHA_NAME1;
        internal r_framework.CustomControl.CustomTextBox GYOUSHA_NAME2;
        internal r_framework.CustomControl.CustomTextBox GYOUSHA_NAME_RYAKU;
        internal System.Windows.Forms.Label RYAKUSHOU;
        internal System.Windows.Forms.Label GYOUSHA2;
        internal System.Windows.Forms.Label GYOUSHA1;
        internal r_framework.CustomControl.CustomTextBox GYOUSHA_FURIGANA;
        internal System.Windows.Forms.Label FURIGANA_LABEL;
        internal r_framework.CustomControl.CustomTextBox GYOUSHA_ADDRESS;
        internal System.Windows.Forms.Label label26;
        internal r_framework.CustomControl.CustomDateTimePicker TEKIYOU_END;
        internal r_framework.CustomControl.CustomDateTimePicker TEKIYOU_BEGIN;
        internal System.Windows.Forms.Label TEKIYOU_LABEL;
        private System.Windows.Forms.Label label1;
        internal r_framework.CustomControl.CustomTextBox TODOUFUKEN_NAME;
        internal System.Windows.Forms.Label label25;
        internal r_framework.CustomControl.CustomNumericTextBox2 GYOUSHA_TODOUFUKEN_CD;
        internal r_framework.CustomControl.CustomTextBox TORIHIKISAKI_NAME_RYAKU;
        private System.Windows.Forms.Label label06;
        public System.Windows.Forms.CheckBox ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI;
        public System.Windows.Forms.CheckBox ICHIRAN_HYOUJI_JOUKEN_DELETED;
        public System.Windows.Forms.CheckBox ICHIRAN_HYOUJI_JOUKEN_TEKIYOU;
        public System.Windows.Forms.Label label2;
        internal r_framework.CustomControl.CustomPopupOpenButton TORIHIKISAKI_SEARCH_BUTTON;
        internal r_framework.CustomControl.CustomTextBox TORIHIKISAKI_RNAME;
        internal System.Windows.Forms.Label TORIHIKISAKI_LABEL;
        internal r_framework.CustomControl.CustomAlphaNumTextBox TORIHIKISAKI_CD;
        internal r_framework.CustomControl.CustomPopupOpenButton GYOUSHA_SEARCH_BUTTON;
        internal r_framework.CustomControl.CustomTextBox GYOUSHA_RNAME;
        internal System.Windows.Forms.Label GYOUSHA_LABEL;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GYOUSHA_CD;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
    }
}
