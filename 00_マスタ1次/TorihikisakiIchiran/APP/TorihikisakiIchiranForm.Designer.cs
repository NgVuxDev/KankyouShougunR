namespace TorihikisakiIchiran.APP
{
    partial class TorihikisakiIchiranForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TorihikisakiIchiranForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.TORIHIKISAKI_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.TORIHIKISAKI_NAME2 = new r_framework.CustomControl.CustomTextBox();
            this.TORIHIKISAKI_NAME1 = new r_framework.CustomControl.CustomTextBox();
            this.label06 = new System.Windows.Forms.Label();
            this.label05 = new System.Windows.Forms.Label();
            this.label04 = new System.Windows.Forms.Label();
            this.label03 = new System.Windows.Forms.Label();
            this.TORIHIKISAKI_ADDRESS = new r_framework.CustomControl.CustomTextBox();
            this.TORIHIKISAKI_TODOUFUKEN_NAME = new r_framework.CustomControl.CustomTextBox();
            this.label003 = new System.Windows.Forms.Label();
            this.label001 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TEKIYOU_END = new r_framework.CustomControl.CustomDateTimePicker();
            this.TEKIYOU_BEGIN = new r_framework.CustomControl.CustomDateTimePicker();
            this.TEKIYOU_LABEL = new System.Windows.Forms.Label();
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI = new System.Windows.Forms.CheckBox();
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED = new System.Windows.Forms.CheckBox();
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TORIHIKISAKI_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.TORIHIKISAKI_RNAME = new r_framework.CustomControl.CustomTextBox();
            this.TORIHIKISAKI_LABEL = new System.Windows.Forms.Label();
            this.TORIHIKISAKI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.TORIHIKISAKI_TODOUFUKEN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.TORIHIKISAKI_FURIGANA = new r_framework.CustomControl.CustomTextBox();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.searchString.Location = new System.Drawing.Point(3, 3);
            this.searchString.ReadOnly = true;
            this.searchString.Size = new System.Drawing.Size(997, 139);
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
            this.customSortHeader1.Location = new System.Drawing.Point(4, 148);
            this.customSortHeader1.Size = new System.Drawing.Size(994, 33);
            this.customSortHeader1.TabIndex = 118;
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
            this.TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod = null;
            this.TORIHIKISAKI_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TORIHIKISAKI_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_NAME_RYAKU.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.TORIHIKISAKI_NAME_RYAKU.IsInputErrorOccured = false;
            this.TORIHIKISAKI_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_NAME_RYAKU.Location = new System.Drawing.Point(621, 9);
            this.TORIHIKISAKI_NAME_RYAKU.MaxLength = 40;
            this.TORIHIKISAKI_NAME_RYAKU.Name = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_RYAKU.PopupAfterExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU.PopupBeforeExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams")));
            this.TORIHIKISAKI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_NAME_RYAKU.popupWindowSetting = null;
            this.TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod = null;
            this.TORIHIKISAKI_NAME_RYAKU.ShortItemName = "取引先略称名";
            this.TORIHIKISAKI_NAME_RYAKU.Size = new System.Drawing.Size(285, 20);
            this.TORIHIKISAKI_NAME_RYAKU.TabIndex = 108;
            this.TORIHIKISAKI_NAME_RYAKU.Tag = "全角2０文字以内で入力してください";
            // 
            // TORIHIKISAKI_NAME2
            // 
            this.TORIHIKISAKI_NAME2.BackColor = System.Drawing.SystemColors.Window;
            this.TORIHIKISAKI_NAME2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_NAME2.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.TORIHIKISAKI_NAME2.DBFieldsName = "TORIHIKISAKI_NAME2";
            this.TORIHIKISAKI_NAME2.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_NAME2.DisplayItemName = "取引先名2";
            this.TORIHIKISAKI_NAME2.DisplayPopUp = null;
            this.TORIHIKISAKI_NAME2.FocusOutCheckMethod = null;
            this.TORIHIKISAKI_NAME2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TORIHIKISAKI_NAME2.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_NAME2.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.TORIHIKISAKI_NAME2.IsInputErrorOccured = false;
            this.TORIHIKISAKI_NAME2.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_NAME2.Location = new System.Drawing.Point(128, 75);
            this.TORIHIKISAKI_NAME2.MaxLength = 40;
            this.TORIHIKISAKI_NAME2.Name = "TORIHIKISAKI_NAME2";
            this.TORIHIKISAKI_NAME2.PopupAfterExecute = null;
            this.TORIHIKISAKI_NAME2.PopupBeforeExecute = null;
            this.TORIHIKISAKI_NAME2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_NAME2.PopupSearchSendParams")));
            this.TORIHIKISAKI_NAME2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_NAME2.popupWindowSetting = null;
            this.TORIHIKISAKI_NAME2.RegistCheckMethod = null;
            this.TORIHIKISAKI_NAME2.ShortItemName = "取引先名2";
            this.TORIHIKISAKI_NAME2.Size = new System.Drawing.Size(336, 20);
            this.TORIHIKISAKI_NAME2.TabIndex = 106;
            this.TORIHIKISAKI_NAME2.Tag = "全角２０文字以内で入力してください";
            // 
            // TORIHIKISAKI_NAME1
            // 
            this.TORIHIKISAKI_NAME1.BackColor = System.Drawing.SystemColors.Window;
            this.TORIHIKISAKI_NAME1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_NAME1.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.TORIHIKISAKI_NAME1.CopyAutoSetControl = "";
            this.TORIHIKISAKI_NAME1.DBFieldsName = "TORIHIKISAKI_NAME1";
            this.TORIHIKISAKI_NAME1.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_NAME1.DisplayItemName = "取引先名1";
            this.TORIHIKISAKI_NAME1.DisplayPopUp = null;
            this.TORIHIKISAKI_NAME1.FocusOutCheckMethod = null;
            this.TORIHIKISAKI_NAME1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TORIHIKISAKI_NAME1.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_NAME1.FuriganaAutoSetControl = "";
            this.TORIHIKISAKI_NAME1.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.TORIHIKISAKI_NAME1.IsInputErrorOccured = false;
            this.TORIHIKISAKI_NAME1.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_NAME1.Location = new System.Drawing.Point(128, 53);
            this.TORIHIKISAKI_NAME1.MaxLength = 40;
            this.TORIHIKISAKI_NAME1.Name = "TORIHIKISAKI_NAME1";
            this.TORIHIKISAKI_NAME1.PopupAfterExecute = null;
            this.TORIHIKISAKI_NAME1.PopupBeforeExecute = null;
            this.TORIHIKISAKI_NAME1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_NAME1.PopupSearchSendParams")));
            this.TORIHIKISAKI_NAME1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_NAME1.popupWindowSetting = null;
            this.TORIHIKISAKI_NAME1.RegistCheckMethod = null;
            this.TORIHIKISAKI_NAME1.ShortItemName = "取引先名1";
            this.TORIHIKISAKI_NAME1.Size = new System.Drawing.Size(336, 20);
            this.TORIHIKISAKI_NAME1.TabIndex = 104;
            this.TORIHIKISAKI_NAME1.Tag = "全角２０文字以内で入力してください";
            // 
            // label06
            // 
            this.label06.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label06.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label06.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label06.ForeColor = System.Drawing.Color.White;
            this.label06.Location = new System.Drawing.Point(505, 9);
            this.label06.Name = "label06";
            this.label06.Size = new System.Drawing.Size(110, 20);
            this.label06.TabIndex = 107;
            this.label06.Text = "取引先略称";
            this.label06.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label05
            // 
            this.label05.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label05.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label05.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label05.ForeColor = System.Drawing.Color.White;
            this.label05.Location = new System.Drawing.Point(12, 75);
            this.label05.Name = "label05";
            this.label05.Size = new System.Drawing.Size(110, 20);
            this.label05.TabIndex = 105;
            this.label05.Text = "取引先名２";
            this.label05.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label04
            // 
            this.label04.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label04.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label04.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label04.ForeColor = System.Drawing.Color.White;
            this.label04.Location = new System.Drawing.Point(12, 53);
            this.label04.Name = "label04";
            this.label04.Size = new System.Drawing.Size(110, 20);
            this.label04.TabIndex = 103;
            this.label04.Text = "取引先名１";
            this.label04.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label03
            // 
            this.label03.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label03.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label03.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label03.ForeColor = System.Drawing.Color.White;
            this.label03.Location = new System.Drawing.Point(12, 31);
            this.label03.Name = "label03";
            this.label03.Size = new System.Drawing.Size(110, 20);
            this.label03.TabIndex = 101;
            this.label03.Text = "フリガナ";
            this.label03.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TORIHIKISAKI_ADDRESS
            // 
            this.TORIHIKISAKI_ADDRESS.BackColor = System.Drawing.SystemColors.Window;
            this.TORIHIKISAKI_ADDRESS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_ADDRESS.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.TORIHIKISAKI_ADDRESS.DBFieldsName = "TORIHIKISAKI_ADDRESS";
            this.TORIHIKISAKI_ADDRESS.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_ADDRESS.DisplayItemName = "取引先住所1";
            this.TORIHIKISAKI_ADDRESS.DisplayPopUp = null;
            this.TORIHIKISAKI_ADDRESS.FocusOutCheckMethod = null;
            this.TORIHIKISAKI_ADDRESS.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TORIHIKISAKI_ADDRESS.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_ADDRESS.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.TORIHIKISAKI_ADDRESS.IsInputErrorOccured = false;
            this.TORIHIKISAKI_ADDRESS.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_ADDRESS.Location = new System.Drawing.Point(621, 53);
            this.TORIHIKISAKI_ADDRESS.MaxLength = 80;
            this.TORIHIKISAKI_ADDRESS.Name = "TORIHIKISAKI_ADDRESS";
            this.TORIHIKISAKI_ADDRESS.PopupAfterExecute = null;
            this.TORIHIKISAKI_ADDRESS.PopupBeforeExecute = null;
            this.TORIHIKISAKI_ADDRESS.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_ADDRESS.PopupSearchSendParams")));
            this.TORIHIKISAKI_ADDRESS.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_ADDRESS.popupWindowSetting = null;
            this.TORIHIKISAKI_ADDRESS.RegistCheckMethod = null;
            this.TORIHIKISAKI_ADDRESS.ShortItemName = "取引先住所";
            this.TORIHIKISAKI_ADDRESS.Size = new System.Drawing.Size(336, 20);
            this.TORIHIKISAKI_ADDRESS.TabIndex = 113;
            this.TORIHIKISAKI_ADDRESS.Tag = "全角４０文字以内で入力してください";
            // 
            // TORIHIKISAKI_TODOUFUKEN_NAME
            // 
            this.TORIHIKISAKI_TODOUFUKEN_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.TORIHIKISAKI_TODOUFUKEN_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_TODOUFUKEN_NAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.TORIHIKISAKI_TODOUFUKEN_NAME.DBFieldsName = "TODOUFUKEN_NAME_RYAKU";
            this.TORIHIKISAKI_TODOUFUKEN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_TODOUFUKEN_NAME.DisplayItemName = "都道府県略称";
            this.TORIHIKISAKI_TODOUFUKEN_NAME.DisplayPopUp = null;
            this.TORIHIKISAKI_TODOUFUKEN_NAME.FocusOutCheckMethod = null;
            this.TORIHIKISAKI_TODOUFUKEN_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TORIHIKISAKI_TODOUFUKEN_NAME.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_TODOUFUKEN_NAME.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TORIHIKISAKI_TODOUFUKEN_NAME.IsInputErrorOccured = false;
            this.TORIHIKISAKI_TODOUFUKEN_NAME.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_TODOUFUKEN_NAME.Location = new System.Drawing.Point(640, 31);
            this.TORIHIKISAKI_TODOUFUKEN_NAME.MaxLength = 0;
            this.TORIHIKISAKI_TODOUFUKEN_NAME.Name = "TORIHIKISAKI_TODOUFUKEN_NAME";
            this.TORIHIKISAKI_TODOUFUKEN_NAME.PopupAfterExecute = null;
            this.TORIHIKISAKI_TODOUFUKEN_NAME.PopupBeforeExecute = null;
            this.TORIHIKISAKI_TODOUFUKEN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_TODOUFUKEN_NAME.PopupSearchSendParams")));
            this.TORIHIKISAKI_TODOUFUKEN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_TODOUFUKEN_NAME.popupWindowSetting = null;
            this.TORIHIKISAKI_TODOUFUKEN_NAME.ReadOnly = true;
            this.TORIHIKISAKI_TODOUFUKEN_NAME.RegistCheckMethod = null;
            this.TORIHIKISAKI_TODOUFUKEN_NAME.ShortItemName = "都道府県略称";
            this.TORIHIKISAKI_TODOUFUKEN_NAME.Size = new System.Drawing.Size(65, 20);
            this.TORIHIKISAKI_TODOUFUKEN_NAME.TabIndex = 111;
            this.TORIHIKISAKI_TODOUFUKEN_NAME.TabStop = false;
            this.TORIHIKISAKI_TODOUFUKEN_NAME.Tag = "都道府県名が表示されます";
            // 
            // label003
            // 
            this.label003.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label003.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label003.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label003.ForeColor = System.Drawing.Color.White;
            this.label003.Location = new System.Drawing.Point(505, 53);
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
            this.label001.Location = new System.Drawing.Point(505, 31);
            this.label001.Name = "label001";
            this.label001.Size = new System.Drawing.Size(110, 20);
            this.label001.TabIndex = 109;
            this.label001.Text = "都道府県";
            this.label001.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.Location = new System.Drawing.Point(751, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 116;
            this.label3.Text = "～";
            // 
            // TEKIYOU_END
            // 
            this.TEKIYOU_END.BackColor = System.Drawing.SystemColors.Window;
            this.TEKIYOU_END.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TEKIYOU_END.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.TEKIYOU_END.Checked = false;
            this.TEKIYOU_END.DateTimeNowYear = "";
            this.TEKIYOU_END.DBFieldsName = "TEKIYOU_END";
            this.TEKIYOU_END.DefaultBackColor = System.Drawing.Color.Empty;
            this.TEKIYOU_END.DisplayItemName = "適用終了";
            this.TEKIYOU_END.DisplayPopUp = null;
            this.TEKIYOU_END.FocusOutCheckMethod = null;
            this.TEKIYOU_END.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TEKIYOU_END.ForeColor = System.Drawing.Color.Black;
            this.TEKIYOU_END.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TEKIYOU_END.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TEKIYOU_END.IsInputErrorOccured = false;
            this.TEKIYOU_END.ItemDefinedTypes = "datetime";
            this.TEKIYOU_END.Location = new System.Drawing.Point(778, 75);
            this.TEKIYOU_END.MaxLength = 10;
            this.TEKIYOU_END.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.TEKIYOU_END.Name = "TEKIYOU_END";
            this.TEKIYOU_END.NullValue = "";
            this.TEKIYOU_END.PopupAfterExecute = null;
            this.TEKIYOU_END.PopupBeforeExecute = null;
            this.TEKIYOU_END.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TEKIYOU_END.PopupSearchSendParams")));
            this.TEKIYOU_END.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TEKIYOU_END.popupWindowSetting = null;
            this.TEKIYOU_END.RegistCheckMethod = null;
            this.TEKIYOU_END.ShortItemName = "適用終了";
            this.TEKIYOU_END.Size = new System.Drawing.Size(124, 20);
            this.TEKIYOU_END.TabIndex = 117;
            this.TEKIYOU_END.Tag = "適用終了日を入力してください";
            this.TEKIYOU_END.Value = null;
            // 
            // TEKIYOU_BEGIN
            // 
            this.TEKIYOU_BEGIN.BackColor = System.Drawing.SystemColors.Window;
            this.TEKIYOU_BEGIN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TEKIYOU_BEGIN.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.TEKIYOU_BEGIN.Checked = false;
            this.TEKIYOU_BEGIN.DateTimeNowYear = "";
            this.TEKIYOU_BEGIN.DBFieldsName = "TEKIYOU_BEGIN";
            this.TEKIYOU_BEGIN.DefaultBackColor = System.Drawing.Color.Empty;
            this.TEKIYOU_BEGIN.DisplayItemName = "適用開始";
            this.TEKIYOU_BEGIN.DisplayPopUp = null;
            this.TEKIYOU_BEGIN.FocusOutCheckMethod = null;
            this.TEKIYOU_BEGIN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TEKIYOU_BEGIN.ForeColor = System.Drawing.Color.Black;
            this.TEKIYOU_BEGIN.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TEKIYOU_BEGIN.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TEKIYOU_BEGIN.IsInputErrorOccured = false;
            this.TEKIYOU_BEGIN.ItemDefinedTypes = "datetime";
            this.TEKIYOU_BEGIN.Location = new System.Drawing.Point(621, 75);
            this.TEKIYOU_BEGIN.MaxLength = 10;
            this.TEKIYOU_BEGIN.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.TEKIYOU_BEGIN.Name = "TEKIYOU_BEGIN";
            this.TEKIYOU_BEGIN.NullValue = "";
            this.TEKIYOU_BEGIN.PopupAfterExecute = null;
            this.TEKIYOU_BEGIN.PopupBeforeExecute = null;
            this.TEKIYOU_BEGIN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TEKIYOU_BEGIN.PopupSearchSendParams")));
            this.TEKIYOU_BEGIN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TEKIYOU_BEGIN.popupWindowSetting = null;
            this.TEKIYOU_BEGIN.RegistCheckMethod = null;
            this.TEKIYOU_BEGIN.ShortItemName = "適用開始";
            this.TEKIYOU_BEGIN.Size = new System.Drawing.Size(124, 20);
            this.TEKIYOU_BEGIN.TabIndex = 115;
            this.TEKIYOU_BEGIN.Tag = "適用開始日を入力してください";
            this.TEKIYOU_BEGIN.Value = null;
            // 
            // TEKIYOU_LABEL
            // 
            this.TEKIYOU_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.TEKIYOU_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TEKIYOU_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TEKIYOU_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TEKIYOU_LABEL.ForeColor = System.Drawing.Color.White;
            this.TEKIYOU_LABEL.Location = new System.Drawing.Point(505, 75);
            this.TEKIYOU_LABEL.Name = "TEKIYOU_LABEL";
            this.TEKIYOU_LABEL.Size = new System.Drawing.Size(110, 20);
            this.TEKIYOU_LABEL.TabIndex = 114;
            this.TEKIYOU_LABEL.Text = "適用期間";
            this.TEKIYOU_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Location = new System.Drawing.Point(741, 99);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Name = "ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Size = new System.Drawing.Size(84, 16);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.TabIndex = 121;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Text = "適用期間外";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.UseVisualStyleBackColor = true;
            // 
            // ICHIRAN_HYOUJI_JOUKEN_DELETED
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Location = new System.Drawing.Point(687, 99);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Name = "ICHIRAN_HYOUJI_JOUKEN_DELETED";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Size = new System.Drawing.Size(48, 16);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.TabIndex = 120;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Text = "削除";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.UseVisualStyleBackColor = true;
            // 
            // ICHIRAN_HYOUJI_JOUKEN_TEKIYOU
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Location = new System.Drawing.Point(621, 99);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Name = "ICHIRAN_HYOUJI_JOUKEN_TEKIYOU";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Size = new System.Drawing.Size(60, 16);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.TabIndex = 119;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Text = "適用中";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(505, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 420;
            this.label1.Text = "表示条件";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.TORIHIKISAKI_SEARCH_BUTTON.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TORIHIKISAKI_SEARCH_BUTTON.GetCodeMasterField = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.Image = ((System.Drawing.Image)(resources.GetObject("TORIHIKISAKI_SEARCH_BUTTON.Image")));
            this.TORIHIKISAKI_SEARCH_BUTTON.ItemDefinedTypes = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.LinkedSettingTextBox = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.LinkedTextBoxs = new string[0];
            this.TORIHIKISAKI_SEARCH_BUTTON.Location = new System.Drawing.Point(469, 8);
            this.TORIHIKISAKI_SEARCH_BUTTON.Name = "TORIHIKISAKI_SEARCH_BUTTON";
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupAfterExecute = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupBeforeExecuteMethod = "";
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
            this.TORIHIKISAKI_SEARCH_BUTTON.Size = new System.Drawing.Size(22, 22);
            this.TORIHIKISAKI_SEARCH_BUTTON.TabIndex = 755;
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
            this.TORIHIKISAKI_RNAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TORIHIKISAKI_RNAME.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_RNAME.IsInputErrorOccured = false;
            this.TORIHIKISAKI_RNAME.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_RNAME.Location = new System.Drawing.Point(177, 9);
            this.TORIHIKISAKI_RNAME.MaxLength = 40;
            this.TORIHIKISAKI_RNAME.Name = "TORIHIKISAKI_RNAME";
            this.TORIHIKISAKI_RNAME.PopupAfterExecute = null;
            this.TORIHIKISAKI_RNAME.PopupBeforeExecute = null;
            this.TORIHIKISAKI_RNAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_RNAME.PopupSearchSendParams")));
            this.TORIHIKISAKI_RNAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_RNAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_RNAME.popupWindowSetting")));
            this.TORIHIKISAKI_RNAME.ReadOnly = true;
            this.TORIHIKISAKI_RNAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_RNAME.RegistCheckMethod")));
            this.TORIHIKISAKI_RNAME.Size = new System.Drawing.Size(287, 20);
            this.TORIHIKISAKI_RNAME.TabIndex = 754;
            this.TORIHIKISAKI_RNAME.TabStop = false;
            this.TORIHIKISAKI_RNAME.Tag = " ";
            // 
            // TORIHIKISAKI_LABEL
            // 
            this.TORIHIKISAKI_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.TORIHIKISAKI_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TORIHIKISAKI_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TORIHIKISAKI_LABEL.ForeColor = System.Drawing.Color.White;
            this.TORIHIKISAKI_LABEL.Location = new System.Drawing.Point(12, 9);
            this.TORIHIKISAKI_LABEL.Name = "TORIHIKISAKI_LABEL";
            this.TORIHIKISAKI_LABEL.Size = new System.Drawing.Size(110, 20);
            this.TORIHIKISAKI_LABEL.TabIndex = 756;
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
            this.TORIHIKISAKI_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TORIHIKISAKI_CD.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_CD.GetCodeMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TORIHIKISAKI_CD.IsInputErrorOccured = false;
            this.TORIHIKISAKI_CD.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_CD.Location = new System.Drawing.Point(128, 9);
            this.TORIHIKISAKI_CD.MaxLength = 6;
            this.TORIHIKISAKI_CD.Name = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.PopupAfterExecute = null;
            this.TORIHIKISAKI_CD.PopupAfterExecuteMethod = "";
            this.TORIHIKISAKI_CD.PopupBeforeExecute = null;
            this.TORIHIKISAKI_CD.PopupBeforeExecuteMethod = "";
            this.TORIHIKISAKI_CD.PopupGetMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_CD.PopupSearchSendParams")));
            this.TORIHIKISAKI_CD.PopupSetFormField = "TORIHIKISAKI_CD, TORIHIKISAKI_RNAME";
            this.TORIHIKISAKI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TORIHIKISAKI_CD.PopupWindowName = "検索共通ポップアップ";
            this.TORIHIKISAKI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_CD.popupWindowSetting")));
            this.TORIHIKISAKI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.RegistCheckMethod")));
            this.TORIHIKISAKI_CD.SetFormField = "TORIHIKISAKI_CD, TORIHIKISAKI_RNAME";
            this.TORIHIKISAKI_CD.Size = new System.Drawing.Size(50, 20);
            this.TORIHIKISAKI_CD.TabIndex = 101;
            this.TORIHIKISAKI_CD.Tag = "取引先を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.TORIHIKISAKI_CD.ZeroPaddengFlag = true;
            this.TORIHIKISAKI_CD.Validating += new System.ComponentModel.CancelEventHandler(this.TORIHIKISAKI_CD_Validating);
            // 
            // TORIHIKISAKI_TODOUFUKEN_CD
            // 
            this.TORIHIKISAKI_TODOUFUKEN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.TORIHIKISAKI_TODOUFUKEN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_TODOUFUKEN_CD.CustomFormatSetting = "00";
            this.TORIHIKISAKI_TODOUFUKEN_CD.DBFieldsName = "TORIHIKISAKI_TODOUFUKEN_CD";
            this.TORIHIKISAKI_TODOUFUKEN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_TODOUFUKEN_CD.DisplayItemName = "都道府県CD";
            this.TORIHIKISAKI_TODOUFUKEN_CD.DisplayPopUp = null;
            this.TORIHIKISAKI_TODOUFUKEN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_TODOUFUKEN_CD.FocusOutCheckMethod")));
            this.TORIHIKISAKI_TODOUFUKEN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TORIHIKISAKI_TODOUFUKEN_CD.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_TODOUFUKEN_CD.FormatSetting = "カスタム";
            this.TORIHIKISAKI_TODOUFUKEN_CD.GetCodeMasterField = "TODOUFUKEN_CD,TODOUFUKEN_NAME";
            this.TORIHIKISAKI_TODOUFUKEN_CD.IsInputErrorOccured = false;
            this.TORIHIKISAKI_TODOUFUKEN_CD.ItemDefinedTypes = "smallint";
            this.TORIHIKISAKI_TODOUFUKEN_CD.Location = new System.Drawing.Point(621, 31);
            this.TORIHIKISAKI_TODOUFUKEN_CD.Name = "TORIHIKISAKI_TODOUFUKEN_CD";
            this.TORIHIKISAKI_TODOUFUKEN_CD.PopupAfterExecute = null;
            this.TORIHIKISAKI_TODOUFUKEN_CD.PopupBeforeExecute = null;
            this.TORIHIKISAKI_TODOUFUKEN_CD.PopupGetMasterField = "TODOUFUKEN_CD,TODOUFUKEN_NAME";
            this.TORIHIKISAKI_TODOUFUKEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_TODOUFUKEN_CD.PopupSearchSendParams")));
            this.TORIHIKISAKI_TODOUFUKEN_CD.PopupSetFormField = "TORIHIKISAKI_TODOUFUKEN_CD,TORIHIKISAKI_TODOUFUKEN_NAME";
            this.TORIHIKISAKI_TODOUFUKEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_TODOUFUKEN;
            this.TORIHIKISAKI_TODOUFUKEN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.TORIHIKISAKI_TODOUFUKEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_TODOUFUKEN_CD.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.TORIHIKISAKI_TODOUFUKEN_CD.RangeSetting = rangeSettingDto1;
            this.TORIHIKISAKI_TODOUFUKEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_TODOUFUKEN_CD.RegistCheckMethod")));
            this.TORIHIKISAKI_TODOUFUKEN_CD.SetFormField = "TORIHIKISAKI_TODOUFUKEN_CD,TORIHIKISAKI_TODOUFUKEN_NAME";
            this.TORIHIKISAKI_TODOUFUKEN_CD.ShortItemName = "都道府県略CD";
            this.TORIHIKISAKI_TODOUFUKEN_CD.Size = new System.Drawing.Size(20, 20);
            this.TORIHIKISAKI_TODOUFUKEN_CD.TabIndex = 110;
            this.TORIHIKISAKI_TODOUFUKEN_CD.Tag = "都道府県を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.TORIHIKISAKI_TODOUFUKEN_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TORIHIKISAKI_TODOUFUKEN_CD.WordWrap = false;
            // 
            // TORIHIKISAKI_FURIGANA
            // 
            this.TORIHIKISAKI_FURIGANA.BackColor = System.Drawing.SystemColors.Window;
            this.TORIHIKISAKI_FURIGANA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_FURIGANA.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.TORIHIKISAKI_FURIGANA.DBFieldsName = "TORIHIKISAKI_FURIGANA";
            this.TORIHIKISAKI_FURIGANA.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_FURIGANA.DisplayItemName = "取引先ふりがな";
            this.TORIHIKISAKI_FURIGANA.DisplayPopUp = null;
            this.TORIHIKISAKI_FURIGANA.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_FURIGANA.FocusOutCheckMethod")));
            this.TORIHIKISAKI_FURIGANA.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TORIHIKISAKI_FURIGANA.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_FURIGANA.ImeMode = System.Windows.Forms.ImeMode.Katakana;
            this.TORIHIKISAKI_FURIGANA.IsInputErrorOccured = false;
            this.TORIHIKISAKI_FURIGANA.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_FURIGANA.Location = new System.Drawing.Point(128, 31);
            this.TORIHIKISAKI_FURIGANA.MaxLength = 80;
            this.TORIHIKISAKI_FURIGANA.Name = "TORIHIKISAKI_FURIGANA";
            this.TORIHIKISAKI_FURIGANA.PopupAfterExecute = null;
            this.TORIHIKISAKI_FURIGANA.PopupBeforeExecute = null;
            this.TORIHIKISAKI_FURIGANA.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_FURIGANA.PopupSearchSendParams")));
            this.TORIHIKISAKI_FURIGANA.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_FURIGANA.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_FURIGANA.popupWindowSetting")));
            this.TORIHIKISAKI_FURIGANA.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_FURIGANA.RegistCheckMethod")));
            this.TORIHIKISAKI_FURIGANA.ShortItemName = "取引先ふりがな";
            this.TORIHIKISAKI_FURIGANA.Size = new System.Drawing.Size(336, 20);
            this.TORIHIKISAKI_FURIGANA.TabIndex = 102;
            this.TORIHIKISAKI_FURIGANA.Tag = "全角４０文字以内で入力してください";
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
            this.ISNOT_NEED_DELETE_FLG.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ISNOT_NEED_DELETE_FLG.ForeColor = System.Drawing.Color.Black;
            this.ISNOT_NEED_DELETE_FLG.IsInputErrorOccured = false;
            this.ISNOT_NEED_DELETE_FLG.ItemDefinedTypes = "bit";
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(358, 109);
            this.ISNOT_NEED_DELETE_FLG.MaxLength = 20;
            this.ISNOT_NEED_DELETE_FLG.Name = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.PopupAfterExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupBeforeExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.PopupSearchSendParams")));
            this.ISNOT_NEED_DELETE_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ISNOT_NEED_DELETE_FLG.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.popupWindowSetting")));
            this.ISNOT_NEED_DELETE_FLG.ReadOnly = true;
            this.ISNOT_NEED_DELETE_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.RegistCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.Size = new System.Drawing.Size(40, 20);
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 778;
            this.ISNOT_NEED_DELETE_FLG.TabStop = false;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // TorihikisakiIchiranForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.ClientSize = new System.Drawing.Size(1008, 458);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.TORIHIKISAKI_FURIGANA);
            this.Controls.Add(this.TORIHIKISAKI_TODOUFUKEN_CD);
            this.Controls.Add(this.TORIHIKISAKI_SEARCH_BUTTON);
            this.Controls.Add(this.TORIHIKISAKI_RNAME);
            this.Controls.Add(this.TORIHIKISAKI_LABEL);
            this.Controls.Add(this.TORIHIKISAKI_CD);
            this.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI);
            this.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_DELETED);
            this.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TEKIYOU_END);
            this.Controls.Add(this.TEKIYOU_BEGIN);
            this.Controls.Add(this.TEKIYOU_LABEL);
            this.Controls.Add(this.TORIHIKISAKI_ADDRESS);
            this.Controls.Add(this.TORIHIKISAKI_TODOUFUKEN_NAME);
            this.Controls.Add(this.label003);
            this.Controls.Add(this.label001);
            this.Controls.Add(this.TORIHIKISAKI_NAME_RYAKU);
            this.Controls.Add(this.TORIHIKISAKI_NAME2);
            this.Controls.Add(this.TORIHIKISAKI_NAME1);
            this.Controls.Add(this.label06);
            this.Controls.Add(this.label05);
            this.Controls.Add(this.label04);
            this.Controls.Add(this.label03);
            this.Name = "TorihikisakiIchiranForm";
            this.Text = "TorihikisakiIchiran";
            this.Shown += new System.EventHandler(this.TorihikisakiIchiranForm_Shown);
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
            this.Controls.SetChildIndex(this.TORIHIKISAKI_NAME1, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_NAME2, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.label001, 0);
            this.Controls.SetChildIndex(this.label003, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_TODOUFUKEN_NAME, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_ADDRESS, 0);
            this.Controls.SetChildIndex(this.TEKIYOU_LABEL, 0);
            this.Controls.SetChildIndex(this.TEKIYOU_BEGIN, 0);
            this.Controls.SetChildIndex(this.TEKIYOU_END, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU, 0);
            this.Controls.SetChildIndex(this.ICHIRAN_HYOUJI_JOUKEN_DELETED, 0);
            this.Controls.SetChildIndex(this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_CD, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_LABEL, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_RNAME, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_SEARCH_BUTTON, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_TODOUFUKEN_CD, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_FURIGANA, 0);
            this.Controls.SetChildIndex(this.ISNOT_NEED_DELETE_FLG, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomTextBox TORIHIKISAKI_NAME_RYAKU;
        internal r_framework.CustomControl.CustomTextBox TORIHIKISAKI_NAME2;
        internal r_framework.CustomControl.CustomTextBox TORIHIKISAKI_NAME1;
        private System.Windows.Forms.Label label06;
        private System.Windows.Forms.Label label05;
        private System.Windows.Forms.Label label04;
        private System.Windows.Forms.Label label03;
        internal r_framework.CustomControl.CustomTextBox TORIHIKISAKI_ADDRESS;
        internal r_framework.CustomControl.CustomTextBox TORIHIKISAKI_TODOUFUKEN_NAME;
        private System.Windows.Forms.Label label003;
        private System.Windows.Forms.Label label001;
        private System.Windows.Forms.Label label3;
        internal r_framework.CustomControl.CustomDateTimePicker TEKIYOU_END;
        internal r_framework.CustomControl.CustomDateTimePicker TEKIYOU_BEGIN;
        internal System.Windows.Forms.Label TEKIYOU_LABEL;
        public System.Windows.Forms.CheckBox ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI;
        public System.Windows.Forms.CheckBox ICHIRAN_HYOUJI_JOUKEN_DELETED;
        public System.Windows.Forms.CheckBox ICHIRAN_HYOUJI_JOUKEN_TEKIYOU;
        public System.Windows.Forms.Label label1;
        internal r_framework.CustomControl.CustomPopupOpenButton TORIHIKISAKI_SEARCH_BUTTON;
        internal r_framework.CustomControl.CustomTextBox TORIHIKISAKI_RNAME;
        internal System.Windows.Forms.Label TORIHIKISAKI_LABEL;
        internal r_framework.CustomControl.CustomAlphaNumTextBox TORIHIKISAKI_CD;
        internal r_framework.CustomControl.CustomNumericTextBox2 TORIHIKISAKI_TODOUFUKEN_CD;
        internal r_framework.CustomControl.CustomTextBox TORIHIKISAKI_FURIGANA;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
    }
}
