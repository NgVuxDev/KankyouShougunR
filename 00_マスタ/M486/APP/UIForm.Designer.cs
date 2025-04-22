namespace Shougun.Core.Master.HikiaiGenbaIchiran.APP
{
    partial class UIForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.GYOUSHA_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.GYOUSHA_RYAKUSHOU = new System.Windows.Forms.Label();
            this.TORIHIKISAKI_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.label06 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TEKIYOU_END = new r_framework.CustomControl.CustomDateTimePicker();
            this.TEKIYOU_BEGIN = new r_framework.CustomControl.CustomDateTimePicker();
            this.TEKIYOU_LABEL = new System.Windows.Forms.Label();
            this.GENBA_ADDRESS = new r_framework.CustomControl.CustomTextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.GENBA_TODOUFUKEN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.GenbaTodoufukenNameRyaku = new r_framework.CustomControl.CustomTextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.GENBA_NAME2 = new r_framework.CustomControl.CustomTextBox();
            this.GENBA_NAME1 = new r_framework.CustomControl.CustomTextBox();
            this.GENBA_FURIGANA = new r_framework.CustomControl.CustomTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.GENBA_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.HYOUJI_JOUKEN_LABEL = new System.Windows.Forms.Label();
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU = new System.Windows.Forms.CheckBox();
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED = new System.Windows.Forms.CheckBox();
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI = new System.Windows.Forms.CheckBox();
            this.GENBA_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.GENBA_RNAME = new r_framework.CustomControl.CustomTextBox();
            this.GENBA_LABEL = new System.Windows.Forms.Label();
            this.GENBA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GYOUSHA_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.GYOUSHA_RNAME = new r_framework.CustomControl.CustomTextBox();
            this.GYOUSHA_LABEL = new System.Windows.Forms.Label();
            this.GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.TORIHIKISAKI_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.TORIHIKISAKI_RNAME = new r_framework.CustomControl.CustomTextBox();
            this.TORIHIKISAKI_LABEL = new System.Windows.Forms.Label();
            this.TORIHIKISAKI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.HIKIAI_TORIHIKISAKI_USE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.HIKIAI_GYOUSHA_USE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.searchString.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.FocusOutCheckMethod")));
            this.searchString.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.searchString.Location = new System.Drawing.Point(3, 3);
            this.searchString.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("searchString.PopupSearchSendParams")));
            this.searchString.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("searchString.popupWindowSetting")));
            this.searchString.ReadOnly = true;
            this.searchString.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.RegistCheckMethod")));
            this.searchString.Size = new System.Drawing.Size(997, 155);
            this.searchString.Tag = "詳細検索条件を表示します";
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_ptn1.Location = new System.Drawing.Point(4, 427);
            this.bt_ptn1.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn1.TabIndex = 3;
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_ptn2.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn2.TabIndex = 4;
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_ptn3.Location = new System.Drawing.Point(404, 427);
            this.bt_ptn3.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn3.TabIndex = 5;
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_ptn4.Location = new System.Drawing.Point(604, 427);
            this.bt_ptn4.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn4.TabIndex = 6;
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_ptn5.Location = new System.Drawing.Point(804, 427);
            this.bt_ptn5.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn5.TabIndex = 7;
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.Location = new System.Drawing.Point(4, 157);
            this.customSortHeader1.Size = new System.Drawing.Size(994, 23);
            this.customSortHeader1.TabIndex = 1;
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
            this.GYOUSHA_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GYOUSHA_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_NAME_RYAKU.GetCodeMasterField = "";
            this.GYOUSHA_NAME_RYAKU.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.GYOUSHA_NAME_RYAKU.IsInputErrorOccured = false;
            this.GYOUSHA_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.GYOUSHA_NAME_RYAKU.Location = new System.Drawing.Point(621, 29);
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
            this.GYOUSHA_NAME_RYAKU.Size = new System.Drawing.Size(336, 20);
            this.GYOUSHA_NAME_RYAKU.TabIndex = 114;
            this.GYOUSHA_NAME_RYAKU.Tag = "全角２０文字以内で入力してください";
            // 
            // GYOUSHA_RYAKUSHOU
            // 
            this.GYOUSHA_RYAKUSHOU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.GYOUSHA_RYAKUSHOU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_RYAKUSHOU.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GYOUSHA_RYAKUSHOU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GYOUSHA_RYAKUSHOU.ForeColor = System.Drawing.Color.White;
            this.GYOUSHA_RYAKUSHOU.Location = new System.Drawing.Point(505, 29);
            this.GYOUSHA_RYAKUSHOU.Name = "GYOUSHA_RYAKUSHOU";
            this.GYOUSHA_RYAKUSHOU.Size = new System.Drawing.Size(110, 20);
            this.GYOUSHA_RYAKUSHOU.TabIndex = 123;
            this.GYOUSHA_RYAKUSHOU.Text = "業者略称";
            this.GYOUSHA_RYAKUSHOU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.TORIHIKISAKI_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TORIHIKISAKI_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_NAME_RYAKU.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.TORIHIKISAKI_NAME_RYAKU.IsInputErrorOccured = false;
            this.TORIHIKISAKI_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_NAME_RYAKU.Location = new System.Drawing.Point(621, 8);
            this.TORIHIKISAKI_NAME_RYAKU.MaxLength = 40;
            this.TORIHIKISAKI_NAME_RYAKU.Name = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_RYAKU.PopupAfterExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU.PopupBeforeExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams")));
            this.TORIHIKISAKI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.popupWindowSetting")));
            this.TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU.ShortItemName = "取引先略称名";
            this.TORIHIKISAKI_NAME_RYAKU.Size = new System.Drawing.Size(336, 20);
            this.TORIHIKISAKI_NAME_RYAKU.TabIndex = 113;
            this.TORIHIKISAKI_NAME_RYAKU.Tag = "全角２０文字以内で入力してください";
            // 
            // label06
            // 
            this.label06.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label06.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label06.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label06.ForeColor = System.Drawing.Color.White;
            this.label06.Location = new System.Drawing.Point(505, 8);
            this.label06.Name = "label06";
            this.label06.Size = new System.Drawing.Size(110, 20);
            this.label06.TabIndex = 121;
            this.label06.Text = "取引先略称";
            this.label06.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.Location = new System.Drawing.Point(751, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 140;
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
            this.TEKIYOU_END.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TEKIYOU_END.FocusOutCheckMethod")));
            this.TEKIYOU_END.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TEKIYOU_END.ForeColor = System.Drawing.Color.Black;
            this.TEKIYOU_END.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TEKIYOU_END.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TEKIYOU_END.IsInputErrorOccured = false;
            this.TEKIYOU_END.ItemDefinedTypes = "datetime";
            this.TEKIYOU_END.Location = new System.Drawing.Point(778, 113);
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
            this.TEKIYOU_END.Size = new System.Drawing.Size(124, 20);
            this.TEKIYOU_END.TabIndex = 120;
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
            this.TEKIYOU_BEGIN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TEKIYOU_BEGIN.FocusOutCheckMethod")));
            this.TEKIYOU_BEGIN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TEKIYOU_BEGIN.ForeColor = System.Drawing.Color.Black;
            this.TEKIYOU_BEGIN.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TEKIYOU_BEGIN.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TEKIYOU_BEGIN.IsInputErrorOccured = false;
            this.TEKIYOU_BEGIN.ItemDefinedTypes = "datetime";
            this.TEKIYOU_BEGIN.Location = new System.Drawing.Point(621, 113);
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
            this.TEKIYOU_BEGIN.Size = new System.Drawing.Size(124, 20);
            this.TEKIYOU_BEGIN.TabIndex = 119;
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
            this.TEKIYOU_LABEL.Location = new System.Drawing.Point(505, 113);
            this.TEKIYOU_LABEL.Name = "TEKIYOU_LABEL";
            this.TEKIYOU_LABEL.Size = new System.Drawing.Size(110, 20);
            this.TEKIYOU_LABEL.TabIndex = 138;
            this.TEKIYOU_LABEL.Text = "適用期間";
            this.TEKIYOU_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GENBA_ADDRESS
            // 
            this.GENBA_ADDRESS.BackColor = System.Drawing.SystemColors.Window;
            this.GENBA_ADDRESS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_ADDRESS.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.GENBA_ADDRESS.DBFieldsName = "GENBA_ADDRESS";
            this.GENBA_ADDRESS.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_ADDRESS.DisplayItemName = "現場住所";
            this.GENBA_ADDRESS.DisplayPopUp = null;
            this.GENBA_ADDRESS.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_ADDRESS.FocusOutCheckMethod")));
            this.GENBA_ADDRESS.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GENBA_ADDRESS.ForeColor = System.Drawing.Color.Black;
            this.GENBA_ADDRESS.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.GENBA_ADDRESS.IsInputErrorOccured = false;
            this.GENBA_ADDRESS.ItemDefinedTypes = "varchar";
            this.GENBA_ADDRESS.Location = new System.Drawing.Point(621, 92);
            this.GENBA_ADDRESS.MaxLength = 80;
            this.GENBA_ADDRESS.Name = "GENBA_ADDRESS";
            this.GENBA_ADDRESS.PopupAfterExecute = null;
            this.GENBA_ADDRESS.PopupBeforeExecute = null;
            this.GENBA_ADDRESS.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_ADDRESS.PopupSearchSendParams")));
            this.GENBA_ADDRESS.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_ADDRESS.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_ADDRESS.popupWindowSetting")));
            this.GENBA_ADDRESS.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_ADDRESS.RegistCheckMethod")));
            this.GENBA_ADDRESS.ShortItemName = "現場住所";
            this.GENBA_ADDRESS.Size = new System.Drawing.Size(336, 20);
            this.GENBA_ADDRESS.TabIndex = 118;
            this.GENBA_ADDRESS.Tag = "全角４０文字以内で入力してください";
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label24.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label24.ForeColor = System.Drawing.Color.White;
            this.label24.Location = new System.Drawing.Point(505, 92);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(110, 20);
            this.label24.TabIndex = 136;
            this.label24.Text = "住所";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GENBA_TODOUFUKEN_CD
            // 
            this.GENBA_TODOUFUKEN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.GENBA_TODOUFUKEN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_TODOUFUKEN_CD.CustomFormatSetting = "00";
            this.GENBA_TODOUFUKEN_CD.DBFieldsName = "GENBA_TODOUFUKEN_CD";
            this.GENBA_TODOUFUKEN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_TODOUFUKEN_CD.DisplayItemName = "現場都道府県CD";
            this.GENBA_TODOUFUKEN_CD.DisplayPopUp = null;
            this.GENBA_TODOUFUKEN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_TODOUFUKEN_CD.FocusOutCheckMethod")));
            this.GENBA_TODOUFUKEN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GENBA_TODOUFUKEN_CD.ForeColor = System.Drawing.Color.Black;
            this.GENBA_TODOUFUKEN_CD.FormatSetting = "カスタム";
            this.GENBA_TODOUFUKEN_CD.GetCodeMasterField = "TODOUFUKEN_CD,TODOUFUKEN_NAME";
            this.GENBA_TODOUFUKEN_CD.IsInputErrorOccured = false;
            this.GENBA_TODOUFUKEN_CD.ItemDefinedTypes = "smallint";
            this.GENBA_TODOUFUKEN_CD.Location = new System.Drawing.Point(621, 71);
            this.GENBA_TODOUFUKEN_CD.Name = "GENBA_TODOUFUKEN_CD";
            this.GENBA_TODOUFUKEN_CD.PopupAfterExecute = null;
            this.GENBA_TODOUFUKEN_CD.PopupBeforeExecute = null;
            this.GENBA_TODOUFUKEN_CD.PopupGetMasterField = "TODOUFUKEN_CD,TODOUFUKEN_NAME";
            this.GENBA_TODOUFUKEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_TODOUFUKEN_CD.PopupSearchSendParams")));
            this.GENBA_TODOUFUKEN_CD.PopupSetFormField = "GENBA_TODOUFUKEN_CD,GenbaTodoufukenNameRyaku";
            this.GENBA_TODOUFUKEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_TODOUFUKEN;
            this.GENBA_TODOUFUKEN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.GENBA_TODOUFUKEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_TODOUFUKEN_CD.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.GENBA_TODOUFUKEN_CD.RangeSetting = rangeSettingDto1;
            this.GENBA_TODOUFUKEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_TODOUFUKEN_CD.RegistCheckMethod")));
            this.GENBA_TODOUFUKEN_CD.SetFormField = "GENBA_TODOUFUKEN_CD,GenbaTodoufukenNameRyaku";
            this.GENBA_TODOUFUKEN_CD.ShortItemName = "現場都道府県CD";
            this.GENBA_TODOUFUKEN_CD.Size = new System.Drawing.Size(20, 20);
            this.GENBA_TODOUFUKEN_CD.TabIndex = 116;
            this.GENBA_TODOUFUKEN_CD.Tag = "都道府県を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GENBA_TODOUFUKEN_CD.WordWrap = false;
            // 
            // GenbaTodoufukenNameRyaku
            // 
            this.GenbaTodoufukenNameRyaku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GenbaTodoufukenNameRyaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GenbaTodoufukenNameRyaku.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.GenbaTodoufukenNameRyaku.DBFieldsName = "TODOUFUKEN_NAME_RYAKU";
            this.GenbaTodoufukenNameRyaku.DefaultBackColor = System.Drawing.Color.Empty;
            this.GenbaTodoufukenNameRyaku.DisplayItemName = "都道府県略名";
            this.GenbaTodoufukenNameRyaku.DisplayPopUp = null;
            this.GenbaTodoufukenNameRyaku.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GenbaTodoufukenNameRyaku.FocusOutCheckMethod")));
            this.GenbaTodoufukenNameRyaku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GenbaTodoufukenNameRyaku.ForeColor = System.Drawing.Color.Black;
            this.GenbaTodoufukenNameRyaku.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GenbaTodoufukenNameRyaku.IsInputErrorOccured = false;
            this.GenbaTodoufukenNameRyaku.ItemDefinedTypes = "varchar";
            this.GenbaTodoufukenNameRyaku.Location = new System.Drawing.Point(640, 71);
            this.GenbaTodoufukenNameRyaku.MaxLength = 0;
            this.GenbaTodoufukenNameRyaku.Name = "GenbaTodoufukenNameRyaku";
            this.GenbaTodoufukenNameRyaku.PopupAfterExecute = null;
            this.GenbaTodoufukenNameRyaku.PopupBeforeExecute = null;
            this.GenbaTodoufukenNameRyaku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GenbaTodoufukenNameRyaku.PopupSearchSendParams")));
            this.GenbaTodoufukenNameRyaku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GenbaTodoufukenNameRyaku.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GenbaTodoufukenNameRyaku.popupWindowSetting")));
            this.GenbaTodoufukenNameRyaku.ReadOnly = true;
            this.GenbaTodoufukenNameRyaku.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GenbaTodoufukenNameRyaku.RegistCheckMethod")));
            this.GenbaTodoufukenNameRyaku.ShortItemName = "都道府県略名";
            this.GenbaTodoufukenNameRyaku.Size = new System.Drawing.Size(65, 20);
            this.GenbaTodoufukenNameRyaku.TabIndex = 117;
            this.GenbaTodoufukenNameRyaku.TabStop = false;
            this.GenbaTodoufukenNameRyaku.Tag = "都道府県名が表示されます";
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label23.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label23.ForeColor = System.Drawing.Color.White;
            this.label23.Location = new System.Drawing.Point(505, 71);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(110, 20);
            this.label23.TabIndex = 133;
            this.label23.Text = "都道府県";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GENBA_NAME2
            // 
            this.GENBA_NAME2.BackColor = System.Drawing.SystemColors.Window;
            this.GENBA_NAME2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_NAME2.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.GENBA_NAME2.DBFieldsName = "GENBA_NAME2";
            this.GENBA_NAME2.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_NAME2.DisplayItemName = "現場名称2";
            this.GENBA_NAME2.DisplayPopUp = null;
            this.GENBA_NAME2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME2.FocusOutCheckMethod")));
            this.GENBA_NAME2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GENBA_NAME2.ForeColor = System.Drawing.Color.Black;
            this.GENBA_NAME2.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.GENBA_NAME2.IsInputErrorOccured = false;
            this.GENBA_NAME2.ItemDefinedTypes = "varchar";
            this.GENBA_NAME2.Location = new System.Drawing.Point(128, 114);
            this.GENBA_NAME2.MaxLength = 40;
            this.GENBA_NAME2.Name = "GENBA_NAME2";
            this.GENBA_NAME2.PopupAfterExecute = null;
            this.GENBA_NAME2.PopupBeforeExecute = null;
            this.GENBA_NAME2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_NAME2.PopupSearchSendParams")));
            this.GENBA_NAME2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_NAME2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_NAME2.popupWindowSetting")));
            this.GENBA_NAME2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME2.RegistCheckMethod")));
            this.GENBA_NAME2.ShortItemName = "現場名称2";
            this.GENBA_NAME2.Size = new System.Drawing.Size(336, 20);
            this.GENBA_NAME2.TabIndex = 19;
            this.GENBA_NAME2.Tag = "全角２０文字以内で入力してください";
            // 
            // GENBA_NAME1
            // 
            this.GENBA_NAME1.BackColor = System.Drawing.SystemColors.Window;
            this.GENBA_NAME1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_NAME1.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.GENBA_NAME1.CopyAutoSetControl = "GenbaNameRyaku";
            this.GENBA_NAME1.DBFieldsName = "GENBA_NAME1";
            this.GENBA_NAME1.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_NAME1.DisplayItemName = "現場名1";
            this.GENBA_NAME1.DisplayPopUp = null;
            this.GENBA_NAME1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME1.FocusOutCheckMethod")));
            this.GENBA_NAME1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GENBA_NAME1.ForeColor = System.Drawing.Color.Black;
            this.GENBA_NAME1.FuriganaAutoSetControl = "GenbaFurigana";
            this.GENBA_NAME1.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.GENBA_NAME1.IsInputErrorOccured = false;
            this.GENBA_NAME1.ItemDefinedTypes = "varchar";
            this.GENBA_NAME1.Location = new System.Drawing.Point(128, 93);
            this.GENBA_NAME1.MaxLength = 40;
            this.GENBA_NAME1.Name = "GENBA_NAME1";
            this.GENBA_NAME1.PopupAfterExecute = null;
            this.GENBA_NAME1.PopupBeforeExecute = null;
            this.GENBA_NAME1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_NAME1.PopupSearchSendParams")));
            this.GENBA_NAME1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_NAME1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_NAME1.popupWindowSetting")));
            this.GENBA_NAME1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME1.RegistCheckMethod")));
            this.GENBA_NAME1.ShortItemName = "現場名1";
            this.GENBA_NAME1.Size = new System.Drawing.Size(336, 20);
            this.GENBA_NAME1.TabIndex = 18;
            this.GENBA_NAME1.Tag = "全角２０文字以内で入力してください";
            // 
            // GENBA_FURIGANA
            // 
            this.GENBA_FURIGANA.BackColor = System.Drawing.SystemColors.Window;
            this.GENBA_FURIGANA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_FURIGANA.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.GENBA_FURIGANA.DBFieldsName = "GENBA_FURIGANA";
            this.GENBA_FURIGANA.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_FURIGANA.DisplayItemName = "現場フリガナ";
            this.GENBA_FURIGANA.DisplayPopUp = null;
            this.GENBA_FURIGANA.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_FURIGANA.FocusOutCheckMethod")));
            this.GENBA_FURIGANA.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GENBA_FURIGANA.ForeColor = System.Drawing.Color.Black;
            this.GENBA_FURIGANA.ImeMode = System.Windows.Forms.ImeMode.Katakana;
            this.GENBA_FURIGANA.IsInputErrorOccured = false;
            this.GENBA_FURIGANA.ItemDefinedTypes = "varchar";
            this.GENBA_FURIGANA.Location = new System.Drawing.Point(128, 72);
            this.GENBA_FURIGANA.MaxLength = 40;
            this.GENBA_FURIGANA.Name = "GENBA_FURIGANA";
            this.GENBA_FURIGANA.PopupAfterExecute = null;
            this.GENBA_FURIGANA.PopupBeforeExecute = null;
            this.GENBA_FURIGANA.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_FURIGANA.PopupSearchSendParams")));
            this.GENBA_FURIGANA.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_FURIGANA.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_FURIGANA.popupWindowSetting")));
            this.GENBA_FURIGANA.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_FURIGANA.RegistCheckMethod")));
            this.GENBA_FURIGANA.ShortItemName = "現場フリガナ";
            this.GENBA_FURIGANA.Size = new System.Drawing.Size(336, 20);
            this.GENBA_FURIGANA.TabIndex = 17;
            this.GENBA_FURIGANA.Tag = "全角４０文字以内で入力してください";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(12, 114);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(110, 20);
            this.label11.TabIndex = 129;
            this.label11.Text = "現場名２";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(12, 93);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(110, 20);
            this.label10.TabIndex = 127;
            this.label10.Text = "現場名１";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(12, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 20);
            this.label9.TabIndex = 125;
            this.label9.Text = "フリガナ";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(505, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 131;
            this.label2.Text = "現場略称";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GENBA_NAME_RYAKU
            // 
            this.GENBA_NAME_RYAKU.BackColor = System.Drawing.SystemColors.Window;
            this.GENBA_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.GENBA_NAME_RYAKU.DBFieldsName = "GENBA_NAME_RYAKU";
            this.GENBA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_NAME_RYAKU.DisplayItemName = "現場略称";
            this.GENBA_NAME_RYAKU.DisplayPopUp = null;
            this.GENBA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_RYAKU.FocusOutCheckMethod")));
            this.GENBA_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GENBA_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.GENBA_NAME_RYAKU.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.GENBA_NAME_RYAKU.IsInputErrorOccured = false;
            this.GENBA_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.GENBA_NAME_RYAKU.Location = new System.Drawing.Point(621, 50);
            this.GENBA_NAME_RYAKU.MaxLength = 40;
            this.GENBA_NAME_RYAKU.Name = "GENBA_NAME_RYAKU";
            this.GENBA_NAME_RYAKU.PopupAfterExecute = null;
            this.GENBA_NAME_RYAKU.PopupBeforeExecute = null;
            this.GENBA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_NAME_RYAKU.PopupSearchSendParams")));
            this.GENBA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_NAME_RYAKU.popupWindowSetting")));
            this.GENBA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_RYAKU.RegistCheckMethod")));
            this.GENBA_NAME_RYAKU.ShortItemName = "現場略称";
            this.GENBA_NAME_RYAKU.Size = new System.Drawing.Size(336, 20);
            this.GENBA_NAME_RYAKU.TabIndex = 115;
            this.GENBA_NAME_RYAKU.Tag = "全角２０文字以内で入力してください";
            // 
            // HYOUJI_JOUKEN_LABEL
            // 
            this.HYOUJI_JOUKEN_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.HYOUJI_JOUKEN_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HYOUJI_JOUKEN_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HYOUJI_JOUKEN_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HYOUJI_JOUKEN_LABEL.ForeColor = System.Drawing.Color.White;
            this.HYOUJI_JOUKEN_LABEL.Location = new System.Drawing.Point(505, 134);
            this.HYOUJI_JOUKEN_LABEL.Name = "HYOUJI_JOUKEN_LABEL";
            this.HYOUJI_JOUKEN_LABEL.Size = new System.Drawing.Size(110, 20);
            this.HYOUJI_JOUKEN_LABEL.TabIndex = 142;
            this.HYOUJI_JOUKEN_LABEL.Text = "表示条件";
            this.HYOUJI_JOUKEN_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ICHIRAN_HYOUJI_JOUKEN_TEKIYOU
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Location = new System.Drawing.Point(621, 136);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Name = "ICHIRAN_HYOUJI_JOUKEN_TEKIYOU";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Size = new System.Drawing.Size(68, 17);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.TabIndex = 121;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Text = "適用中";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.UseVisualStyleBackColor = true;
            // 
            // ICHIRAN_HYOUJI_JOUKEN_DELETED
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Location = new System.Drawing.Point(695, 136);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Name = "ICHIRAN_HYOUJI_JOUKEN_DELETED";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Size = new System.Drawing.Size(54, 17);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.TabIndex = 122;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Text = "削除";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.UseVisualStyleBackColor = true;
            // 
            // ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Location = new System.Drawing.Point(755, 136);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Name = "ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Size = new System.Drawing.Size(96, 17);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.TabIndex = 123;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Text = "適用期間外";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.UseVisualStyleBackColor = true;
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
            this.GENBA_SEARCH_BUTTON.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GENBA_SEARCH_BUTTON.GetCodeMasterField = null;
            this.GENBA_SEARCH_BUTTON.Image = ((System.Drawing.Image)(resources.GetObject("GENBA_SEARCH_BUTTON.Image")));
            this.GENBA_SEARCH_BUTTON.ItemDefinedTypes = null;
            this.GENBA_SEARCH_BUTTON.LinkedSettingTextBox = null;
            this.GENBA_SEARCH_BUTTON.LinkedTextBoxs = null;
            this.GENBA_SEARCH_BUTTON.Location = new System.Drawing.Point(469, 50);
            this.GENBA_SEARCH_BUTTON.Name = "GENBA_SEARCH_BUTTON";
            this.GENBA_SEARCH_BUTTON.PopupAfterExecute = null;
            this.GENBA_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.GENBA_SEARCH_BUTTON.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GYOUSHA_HIKIAI_FLG,GENBA_CD,GENBA_NAME_RYAKU";
            this.GENBA_SEARCH_BUTTON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_SEARCH_BUTTON.PopupSearchSendParams")));
            this.GENBA_SEARCH_BUTTON.PopupSetFormField = "GYOUSHA_CD,GYOUSHA_RNAME,HIKIAI_GYOUSHA_USE_FLG,GENBA_CD,GENBA_RNAME";
            this.GENBA_SEARCH_BUTTON.PopupWindowId = r_framework.Const.WINDOW_ID.M_HIKIAI_GENBA_NYUURYOKU;
            this.GENBA_SEARCH_BUTTON.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_SEARCH_BUTTON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_SEARCH_BUTTON.popupWindowSetting")));
            this.GENBA_SEARCH_BUTTON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_SEARCH_BUTTON.RegistCheckMethod")));
            this.GENBA_SEARCH_BUTTON.SearchDisplayFlag = 0;
            this.GENBA_SEARCH_BUTTON.SetFormField = null;
            this.GENBA_SEARCH_BUTTON.ShortItemName = null;
            this.GENBA_SEARCH_BUTTON.Size = new System.Drawing.Size(22, 22);
            this.GENBA_SEARCH_BUTTON.TabIndex = 109;
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
            this.GENBA_RNAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GENBA_RNAME.ForeColor = System.Drawing.Color.Black;
            this.GENBA_RNAME.IsInputErrorOccured = false;
            this.GENBA_RNAME.ItemDefinedTypes = "varchar";
            this.GENBA_RNAME.Location = new System.Drawing.Point(177, 51);
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
            this.GENBA_RNAME.TabIndex = 16;
            this.GENBA_RNAME.TabStop = false;
            this.GENBA_RNAME.Tag = " ";
            // 
            // GENBA_LABEL
            // 
            this.GENBA_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.GENBA_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GENBA_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GENBA_LABEL.ForeColor = System.Drawing.Color.White;
            this.GENBA_LABEL.Location = new System.Drawing.Point(12, 51);
            this.GENBA_LABEL.Name = "GENBA_LABEL";
            this.GENBA_LABEL.Size = new System.Drawing.Size(110, 20);
            this.GENBA_LABEL.TabIndex = 788;
            this.GENBA_LABEL.Text = "現場";
            this.GENBA_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GENBA_CD
            // 
            this.GENBA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.GENBA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_CD.ChangeUpperCase = true;
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
            this.GENBA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GENBA_CD.ForeColor = System.Drawing.Color.Black;
            this.GENBA_CD.GetCodeMasterField = "";
            this.GENBA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GENBA_CD.IsInputErrorOccured = false;
            this.GENBA_CD.ItemDefinedTypes = "varchar";
            this.GENBA_CD.Location = new System.Drawing.Point(128, 51);
            this.GENBA_CD.MaxLength = 6;
            this.GENBA_CD.Name = "GENBA_CD";
            this.GENBA_CD.PopupAfterExecute = null;
            this.GENBA_CD.PopupAfterExecuteMethod = "";
            this.GENBA_CD.PopupBeforeExecute = null;
            this.GENBA_CD.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GYOUSHA_HIKIAI_FLG,GENBA_CD,GENBA_NAME_RYAKU";
            this.GENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_CD.PopupSearchSendParams")));
            this.GENBA_CD.PopupSetFormField = "GYOUSHA_CD,GYOUSHA_RNAME,HIKIAI_GYOUSHA_USE_FLG,GENBA_CD,GENBA_RNAME";
            this.GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_HIKIAI_GENBA_NYUURYOKU;
            this.GENBA_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_CD.popupWindowSetting")));
            this.GENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.RegistCheckMethod")));
            this.GENBA_CD.SetFormField = "";
            this.GENBA_CD.Size = new System.Drawing.Size(50, 20);
            this.GENBA_CD.TabIndex = 15;
            this.GENBA_CD.Tag = "引合現場を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GENBA_CD.ZeroPaddengFlag = true;
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
            this.GYOUSHA_SEARCH_BUTTON.Location = new System.Drawing.Point(469, 29);
            this.GYOUSHA_SEARCH_BUTTON.Name = "GYOUSHA_SEARCH_BUTTON";
            this.GYOUSHA_SEARCH_BUTTON.PopupAfterExecute = null;
            this.GYOUSHA_SEARCH_BUTTON.PopupAfterExecuteMethod = "PopupAfterGyoushaCd";
            this.GYOUSHA_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.GYOUSHA_SEARCH_BUTTON.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GYOUSHA_HIKIAI_FLG";
            this.GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams")));
            this.GYOUSHA_SEARCH_BUTTON.PopupSetFormField = "GYOUSHA_CD,GYOUSHA_RNAME,HIKIAI_GYOUSHA_USE_FLG";
            this.GYOUSHA_SEARCH_BUTTON.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_SEARCH_BUTTON.PopupWindowName = "引合既存用検索ポップアップ";
            this.GYOUSHA_SEARCH_BUTTON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.popupWindowSetting")));
            this.GYOUSHA_SEARCH_BUTTON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.RegistCheckMethod")));
            this.GYOUSHA_SEARCH_BUTTON.SearchDisplayFlag = 0;
            this.GYOUSHA_SEARCH_BUTTON.SetFormField = null;
            this.GYOUSHA_SEARCH_BUTTON.ShortItemName = null;
            this.GYOUSHA_SEARCH_BUTTON.Size = new System.Drawing.Size(22, 22);
            this.GYOUSHA_SEARCH_BUTTON.TabIndex = 106;
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
            this.GYOUSHA_RNAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GYOUSHA_RNAME.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_RNAME.IsInputErrorOccured = false;
            this.GYOUSHA_RNAME.ItemDefinedTypes = "varchar";
            this.GYOUSHA_RNAME.Location = new System.Drawing.Point(177, 30);
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
            this.GYOUSHA_RNAME.TabIndex = 14;
            this.GYOUSHA_RNAME.TabStop = false;
            this.GYOUSHA_RNAME.Tag = " ";
            // 
            // GYOUSHA_LABEL
            // 
            this.GYOUSHA_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.GYOUSHA_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GYOUSHA_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GYOUSHA_LABEL.ForeColor = System.Drawing.Color.White;
            this.GYOUSHA_LABEL.Location = new System.Drawing.Point(12, 30);
            this.GYOUSHA_LABEL.Name = "GYOUSHA_LABEL";
            this.GYOUSHA_LABEL.Size = new System.Drawing.Size(110, 20);
            this.GYOUSHA_LABEL.TabIndex = 785;
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
            this.GYOUSHA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_CD.GetCodeMasterField = "";
            this.GYOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GYOUSHA_CD.IsInputErrorOccured = false;
            this.GYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.GYOUSHA_CD.Location = new System.Drawing.Point(128, 30);
            this.GYOUSHA_CD.MaxLength = 6;
            this.GYOUSHA_CD.Name = "GYOUSHA_CD";
            this.GYOUSHA_CD.PopupAfterExecute = null;
            this.GYOUSHA_CD.PopupAfterExecuteMethod = "PopupAfterGyoushaCd";
            this.GYOUSHA_CD.PopupBeforeExecute = null;
            this.GYOUSHA_CD.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_HIKIAI_FLG";
            this.GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_CD.PopupSearchSendParams")));
            this.GYOUSHA_CD.PopupSetFormField = "GYOUSHA_CD,HIKIAI_GYOUSHA_USE_FLG";
            this.GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_CD.PopupWindowName = "引合既存用検索ポップアップ";
            this.GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_CD.popupWindowSetting")));
            this.GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.RegistCheckMethod")));
            this.GYOUSHA_CD.SetFormField = "";
            this.GYOUSHA_CD.Size = new System.Drawing.Size(50, 20);
            this.GYOUSHA_CD.TabIndex = 13;
            this.GYOUSHA_CD.Tag = "業者または引合業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GYOUSHA_CD.ZeroPaddengFlag = true;
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
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupAfterExecuteMethod = "PopupAfterTorihikisakiCd";
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupGetMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_HIKIAI_FLG";
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_SEARCH_BUTTON.PopupSearchSendParams")));
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupSetFormField = "TORIHIKISAKI_CD,HIKIAI_TORIHIKISAKI_USE_FLG";
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupWindowName = "引合既存用検索ポップアップ";
            this.TORIHIKISAKI_SEARCH_BUTTON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_SEARCH_BUTTON.popupWindowSetting")));
            this.TORIHIKISAKI_SEARCH_BUTTON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_SEARCH_BUTTON.RegistCheckMethod")));
            this.TORIHIKISAKI_SEARCH_BUTTON.SearchDisplayFlag = 0;
            this.TORIHIKISAKI_SEARCH_BUTTON.SetFormField = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.ShortItemName = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.Size = new System.Drawing.Size(22, 22);
            this.TORIHIKISAKI_SEARCH_BUTTON.TabIndex = 103;
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
            this.TORIHIKISAKI_RNAME.TabIndex = 12;
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
            this.TORIHIKISAKI_LABEL.TabIndex = 782;
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
            this.TORIHIKISAKI_CD.GetCodeMasterField = "";
            this.TORIHIKISAKI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TORIHIKISAKI_CD.IsInputErrorOccured = false;
            this.TORIHIKISAKI_CD.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_CD.Location = new System.Drawing.Point(128, 9);
            this.TORIHIKISAKI_CD.MaxLength = 6;
            this.TORIHIKISAKI_CD.Name = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.PopupAfterExecute = null;
            this.TORIHIKISAKI_CD.PopupAfterExecuteMethod = "PopupAfterTorihikisakiCd";
            this.TORIHIKISAKI_CD.PopupBeforeExecute = null;
            this.TORIHIKISAKI_CD.PopupGetMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_HIKIAI_FLG";
            this.TORIHIKISAKI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_CD.PopupSearchSendParams")));
            this.TORIHIKISAKI_CD.PopupSetFormField = "TORIHIKISAKI_CD,HIKIAI_TORIHIKISAKI_USE_FLG";
            this.TORIHIKISAKI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TORIHIKISAKI_CD.PopupWindowName = "引合既存用検索ポップアップ";
            this.TORIHIKISAKI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_CD.popupWindowSetting")));
            this.TORIHIKISAKI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.RegistCheckMethod")));
            this.TORIHIKISAKI_CD.SetFormField = "";
            this.TORIHIKISAKI_CD.Size = new System.Drawing.Size(50, 20);
            this.TORIHIKISAKI_CD.TabIndex = 11;
            this.TORIHIKISAKI_CD.Tag = "取引先または引合取引先を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.TORIHIKISAKI_CD.ZeroPaddengFlag = true;
            // 
            // HIKIAI_TORIHIKISAKI_USE_FLG
            // 
            this.HIKIAI_TORIHIKISAKI_USE_FLG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.HIKIAI_TORIHIKISAKI_USE_FLG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HIKIAI_TORIHIKISAKI_USE_FLG.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.HIKIAI_TORIHIKISAKI_USE_FLG.CopyAutoSetControl = "";
            this.HIKIAI_TORIHIKISAKI_USE_FLG.DBFieldsName = "HIKIAI_TORIHIKISAKI_USE_FLG";
            this.HIKIAI_TORIHIKISAKI_USE_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.HIKIAI_TORIHIKISAKI_USE_FLG.DisplayItemName = "引合取引先利用フラグ";
            this.HIKIAI_TORIHIKISAKI_USE_FLG.DisplayPopUp = null;
            this.HIKIAI_TORIHIKISAKI_USE_FLG.Enabled = false;
            this.HIKIAI_TORIHIKISAKI_USE_FLG.ErrorMessage = "";
            this.HIKIAI_TORIHIKISAKI_USE_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIKIAI_TORIHIKISAKI_USE_FLG.FocusOutCheckMethod")));
            this.HIKIAI_TORIHIKISAKI_USE_FLG.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HIKIAI_TORIHIKISAKI_USE_FLG.ForeColor = System.Drawing.Color.Black;
            this.HIKIAI_TORIHIKISAKI_USE_FLG.FuriganaAutoSetControl = "";
            this.HIKIAI_TORIHIKISAKI_USE_FLG.GetCodeMasterField = "";
            this.HIKIAI_TORIHIKISAKI_USE_FLG.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.HIKIAI_TORIHIKISAKI_USE_FLG.IsInputErrorOccured = false;
            this.HIKIAI_TORIHIKISAKI_USE_FLG.ItemDefinedTypes = "bit";
            this.HIKIAI_TORIHIKISAKI_USE_FLG.Location = new System.Drawing.Point(972, 10);
            this.HIKIAI_TORIHIKISAKI_USE_FLG.MaxLength = 0;
            this.HIKIAI_TORIHIKISAKI_USE_FLG.Name = "HIKIAI_TORIHIKISAKI_USE_FLG";
            this.HIKIAI_TORIHIKISAKI_USE_FLG.PopupAfterExecute = null;
            this.HIKIAI_TORIHIKISAKI_USE_FLG.PopupBeforeExecute = null;
            this.HIKIAI_TORIHIKISAKI_USE_FLG.PopupGetMasterField = "";
            this.HIKIAI_TORIHIKISAKI_USE_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIKIAI_TORIHIKISAKI_USE_FLG.PopupSearchSendParams")));
            this.HIKIAI_TORIHIKISAKI_USE_FLG.PopupSetFormField = "";
            this.HIKIAI_TORIHIKISAKI_USE_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIKIAI_TORIHIKISAKI_USE_FLG.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HIKIAI_TORIHIKISAKI_USE_FLG.popupWindowSetting")));
            this.HIKIAI_TORIHIKISAKI_USE_FLG.ReadOnly = true;
            this.HIKIAI_TORIHIKISAKI_USE_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIKIAI_TORIHIKISAKI_USE_FLG.RegistCheckMethod")));
            this.HIKIAI_TORIHIKISAKI_USE_FLG.SetFormField = "";
            this.HIKIAI_TORIHIKISAKI_USE_FLG.ShortItemName = "";
            this.HIKIAI_TORIHIKISAKI_USE_FLG.Size = new System.Drawing.Size(10, 20);
            this.HIKIAI_TORIHIKISAKI_USE_FLG.TabIndex = 789;
            this.HIKIAI_TORIHIKISAKI_USE_FLG.TabStop = false;
            this.HIKIAI_TORIHIKISAKI_USE_FLG.Tag = "";
            this.HIKIAI_TORIHIKISAKI_USE_FLG.Visible = false;
            // 
            // HIKIAI_GYOUSHA_USE_FLG
            // 
            this.HIKIAI_GYOUSHA_USE_FLG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.HIKIAI_GYOUSHA_USE_FLG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HIKIAI_GYOUSHA_USE_FLG.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.HIKIAI_GYOUSHA_USE_FLG.CopyAutoSetControl = "";
            this.HIKIAI_GYOUSHA_USE_FLG.DBFieldsName = "HIKIAI_GYOUSHA_USE_FLG";
            this.HIKIAI_GYOUSHA_USE_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.HIKIAI_GYOUSHA_USE_FLG.DisplayItemName = "引合業者利用フラグ";
            this.HIKIAI_GYOUSHA_USE_FLG.DisplayPopUp = null;
            this.HIKIAI_GYOUSHA_USE_FLG.Enabled = false;
            this.HIKIAI_GYOUSHA_USE_FLG.ErrorMessage = "";
            this.HIKIAI_GYOUSHA_USE_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIKIAI_GYOUSHA_USE_FLG.FocusOutCheckMethod")));
            this.HIKIAI_GYOUSHA_USE_FLG.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HIKIAI_GYOUSHA_USE_FLG.ForeColor = System.Drawing.Color.Black;
            this.HIKIAI_GYOUSHA_USE_FLG.FuriganaAutoSetControl = "";
            this.HIKIAI_GYOUSHA_USE_FLG.GetCodeMasterField = "";
            this.HIKIAI_GYOUSHA_USE_FLG.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.HIKIAI_GYOUSHA_USE_FLG.IsInputErrorOccured = false;
            this.HIKIAI_GYOUSHA_USE_FLG.ItemDefinedTypes = "bit";
            this.HIKIAI_GYOUSHA_USE_FLG.Location = new System.Drawing.Point(986, 10);
            this.HIKIAI_GYOUSHA_USE_FLG.MaxLength = 0;
            this.HIKIAI_GYOUSHA_USE_FLG.Name = "HIKIAI_GYOUSHA_USE_FLG";
            this.HIKIAI_GYOUSHA_USE_FLG.PopupAfterExecute = null;
            this.HIKIAI_GYOUSHA_USE_FLG.PopupBeforeExecute = null;
            this.HIKIAI_GYOUSHA_USE_FLG.PopupGetMasterField = "";
            this.HIKIAI_GYOUSHA_USE_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIKIAI_GYOUSHA_USE_FLG.PopupSearchSendParams")));
            this.HIKIAI_GYOUSHA_USE_FLG.PopupSetFormField = "";
            this.HIKIAI_GYOUSHA_USE_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIKIAI_GYOUSHA_USE_FLG.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HIKIAI_GYOUSHA_USE_FLG.popupWindowSetting")));
            this.HIKIAI_GYOUSHA_USE_FLG.ReadOnly = true;
            this.HIKIAI_GYOUSHA_USE_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIKIAI_GYOUSHA_USE_FLG.RegistCheckMethod")));
            this.HIKIAI_GYOUSHA_USE_FLG.SetFormField = "";
            this.HIKIAI_GYOUSHA_USE_FLG.ShortItemName = "";
            this.HIKIAI_GYOUSHA_USE_FLG.Size = new System.Drawing.Size(10, 20);
            this.HIKIAI_GYOUSHA_USE_FLG.TabIndex = 790;
            this.HIKIAI_GYOUSHA_USE_FLG.TabStop = false;
            this.HIKIAI_GYOUSHA_USE_FLG.Tag = "";
            this.HIKIAI_GYOUSHA_USE_FLG.Visible = false;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.ClientSize = new System.Drawing.Size(1008, 458);
            this.Controls.Add(this.HIKIAI_GYOUSHA_USE_FLG);
            this.Controls.Add(this.HIKIAI_TORIHIKISAKI_USE_FLG);
            this.Controls.Add(this.GENBA_SEARCH_BUTTON);
            this.Controls.Add(this.GENBA_RNAME);
            this.Controls.Add(this.GENBA_LABEL);
            this.Controls.Add(this.GENBA_CD);
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
            this.Controls.Add(this.HYOUJI_JOUKEN_LABEL);
            this.Controls.Add(this.GYOUSHA_NAME_RYAKU);
            this.Controls.Add(this.GYOUSHA_RYAKUSHOU);
            this.Controls.Add(this.TORIHIKISAKI_NAME_RYAKU);
            this.Controls.Add(this.label06);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TEKIYOU_END);
            this.Controls.Add(this.TEKIYOU_BEGIN);
            this.Controls.Add(this.TEKIYOU_LABEL);
            this.Controls.Add(this.GENBA_ADDRESS);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.GENBA_TODOUFUKEN_CD);
            this.Controls.Add(this.GenbaTodoufukenNameRyaku);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.GENBA_NAME2);
            this.Controls.Add(this.GENBA_NAME1);
            this.Controls.Add(this.GENBA_FURIGANA);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.GENBA_NAME_RYAKU);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "UIForm";
            this.Text = "TorihikisakiIchiran";
            this.Shown += new System.EventHandler(this.UIForm_Shown);
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.GENBA_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.GENBA_FURIGANA, 0);
            this.Controls.SetChildIndex(this.GENBA_NAME1, 0);
            this.Controls.SetChildIndex(this.GENBA_NAME2, 0);
            this.Controls.SetChildIndex(this.label23, 0);
            this.Controls.SetChildIndex(this.GenbaTodoufukenNameRyaku, 0);
            this.Controls.SetChildIndex(this.GENBA_TODOUFUKEN_CD, 0);
            this.Controls.SetChildIndex(this.label24, 0);
            this.Controls.SetChildIndex(this.GENBA_ADDRESS, 0);
            this.Controls.SetChildIndex(this.TEKIYOU_LABEL, 0);
            this.Controls.SetChildIndex(this.TEKIYOU_BEGIN, 0);
            this.Controls.SetChildIndex(this.TEKIYOU_END, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label06, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_RYAKUSHOU, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.HYOUJI_JOUKEN_LABEL, 0);
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
            this.Controls.SetChildIndex(this.GENBA_CD, 0);
            this.Controls.SetChildIndex(this.GENBA_LABEL, 0);
            this.Controls.SetChildIndex(this.GENBA_RNAME, 0);
            this.Controls.SetChildIndex(this.GENBA_SEARCH_BUTTON, 0);
            this.Controls.SetChildIndex(this.HIKIAI_TORIHIKISAKI_USE_FLG, 0);
            this.Controls.SetChildIndex(this.HIKIAI_GYOUSHA_USE_FLG, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomTextBox GYOUSHA_NAME_RYAKU;
        internal System.Windows.Forms.Label GYOUSHA_RYAKUSHOU;
        internal r_framework.CustomControl.CustomTextBox TORIHIKISAKI_NAME_RYAKU;
        private System.Windows.Forms.Label label06;
        private System.Windows.Forms.Label label3;
        internal r_framework.CustomControl.CustomDateTimePicker TEKIYOU_END;
        internal r_framework.CustomControl.CustomDateTimePicker TEKIYOU_BEGIN;
        internal System.Windows.Forms.Label TEKIYOU_LABEL;
        internal r_framework.CustomControl.CustomTextBox GENBA_ADDRESS;
        private System.Windows.Forms.Label label24;
        internal r_framework.CustomControl.CustomNumericTextBox2 GENBA_TODOUFUKEN_CD;
        internal r_framework.CustomControl.CustomTextBox GenbaTodoufukenNameRyaku;
        private System.Windows.Forms.Label label23;
        internal r_framework.CustomControl.CustomTextBox GENBA_NAME2;
        internal r_framework.CustomControl.CustomTextBox GENBA_NAME1;
        internal r_framework.CustomControl.CustomTextBox GENBA_FURIGANA;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        internal r_framework.CustomControl.CustomTextBox GENBA_NAME_RYAKU;
        internal System.Windows.Forms.Label HYOUJI_JOUKEN_LABEL;
        internal System.Windows.Forms.CheckBox ICHIRAN_HYOUJI_JOUKEN_TEKIYOU;
        internal System.Windows.Forms.CheckBox ICHIRAN_HYOUJI_JOUKEN_DELETED;
        internal System.Windows.Forms.CheckBox ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI;
        internal r_framework.CustomControl.CustomPopupOpenButton GENBA_SEARCH_BUTTON;
        internal r_framework.CustomControl.CustomTextBox GENBA_RNAME;
        internal System.Windows.Forms.Label GENBA_LABEL;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GENBA_CD;
        internal r_framework.CustomControl.CustomPopupOpenButton GYOUSHA_SEARCH_BUTTON;
        internal r_framework.CustomControl.CustomTextBox GYOUSHA_RNAME;
        internal System.Windows.Forms.Label GYOUSHA_LABEL;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GYOUSHA_CD;
        internal r_framework.CustomControl.CustomPopupOpenButton TORIHIKISAKI_SEARCH_BUTTON;
        internal r_framework.CustomControl.CustomTextBox TORIHIKISAKI_RNAME;
        internal System.Windows.Forms.Label TORIHIKISAKI_LABEL;
        internal r_framework.CustomControl.CustomAlphaNumTextBox TORIHIKISAKI_CD;
        internal r_framework.CustomControl.CustomTextBox HIKIAI_TORIHIKISAKI_USE_FLG;
        internal r_framework.CustomControl.CustomTextBox HIKIAI_GYOUSHA_USE_FLG;
    }
}