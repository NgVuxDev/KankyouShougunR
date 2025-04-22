namespace Shougun.Core.ExternalConnection.GaibuRenkeiGenbaIchiran
{
    partial class GaibuRenkeiGenbaIchiranForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GaibuRenkeiGenbaIchiranForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.label2 = new System.Windows.Forms.Label();
            this.GENBA_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.GENBA_NAME2 = new r_framework.CustomControl.CustomTextBox();
            this.GENBA_NAME1 = new r_framework.CustomControl.CustomTextBox();
            this.GENBA_FURIGANA = new r_framework.CustomControl.CustomTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TEKIYOU_END = new r_framework.CustomControl.CustomDateTimePicker();
            this.TEKIYOU_BEGIN = new r_framework.CustomControl.CustomDateTimePicker();
            this.TEKIYOU_LABEL = new System.Windows.Forms.Label();
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI = new System.Windows.Forms.CheckBox();
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED = new System.Windows.Forms.CheckBox();
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GYOUSHA_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.GYOUSHA_RNAME = new r_framework.CustomControl.CustomTextBox();
            this.GYOUSHA_LABEL = new System.Windows.Forms.Label();
            this.GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GENBA_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.GENBA_RNAME = new r_framework.CustomControl.CustomTextBox();
            this.GENBA_LABEL = new System.Windows.Forms.Label();
            this.GENBA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.DIGI_MAP_NAME = new r_framework.CustomControl.CustomTextBox();
            this.DIGI_POINT_NAME = new r_framework.CustomControl.CustomTextBox();
            this.DIGI_POINT_KANA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel_RenkeiKouho = new r_framework.CustomControl.CustomPanel();
            this.radbtnAll = new r_framework.CustomControl.CustomRadioButton();
            this.radbtnMirenkei = new r_framework.CustomControl.CustomRadioButton();
            this.radbtnAPIRenkei = new r_framework.CustomControl.CustomRadioButton();
            this.txtNum_RenkeiJyoukyou = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label7 = new System.Windows.Forms.Label();
            this.panel_RenkeiKouho.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.searchString.Location = new System.Drawing.Point(841, 74);
            this.searchString.ReadOnly = true;
            this.searchString.Size = new System.Drawing.Size(155, 28);
            this.searchString.TabIndex = 121;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Location = new System.Drawing.Point(3, 450);
            this.bt_ptn1.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn1.TabIndex = 240;
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Location = new System.Drawing.Point(203, 450);
            this.bt_ptn2.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn2.TabIndex = 250;
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Location = new System.Drawing.Point(403, 450);
            this.bt_ptn3.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn3.TabIndex = 260;
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Location = new System.Drawing.Point(603, 450);
            this.bt_ptn4.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn4.TabIndex = 270;
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Location = new System.Drawing.Point(803, 450);
            this.bt_ptn5.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn5.TabIndex = 280;
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.customSortHeader1.Location = new System.Drawing.Point(4, 159);
            this.customSortHeader1.Size = new System.Drawing.Size(994, 23);
            this.customSortHeader1.TabIndex = 122;
            // 
            // customSearchHeader1
            // 
            this.customSearchHeader1.Location = new System.Drawing.Point(4, 137);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(4, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 110;
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
            this.GENBA_NAME_RYAKU.Location = new System.Drawing.Point(117, 130);
            this.GENBA_NAME_RYAKU.MaxLength = 40;
            this.GENBA_NAME_RYAKU.Name = "GENBA_NAME_RYAKU";
            this.GENBA_NAME_RYAKU.PopupAfterExecute = null;
            this.GENBA_NAME_RYAKU.PopupBeforeExecute = null;
            this.GENBA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_NAME_RYAKU.PopupSearchSendParams")));
            this.GENBA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_NAME_RYAKU.popupWindowSetting")));
            this.GENBA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_RYAKU.RegistCheckMethod")));
            this.GENBA_NAME_RYAKU.ShortItemName = "現場略称";
            this.GENBA_NAME_RYAKU.Size = new System.Drawing.Size(285, 20);
            this.GENBA_NAME_RYAKU.TabIndex = 150;
            this.GENBA_NAME_RYAKU.Tag = "全角２０文字以内で入力してください";
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
            this.GENBA_NAME2.Location = new System.Drawing.Point(117, 109);
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
            this.GENBA_NAME2.TabIndex = 140;
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
            this.GENBA_NAME1.Location = new System.Drawing.Point(117, 88);
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
            this.GENBA_NAME1.TabIndex = 130;
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
            this.GENBA_FURIGANA.Location = new System.Drawing.Point(117, 67);
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
            this.GENBA_FURIGANA.TabIndex = 120;
            this.GENBA_FURIGANA.Tag = "全角４０文字以内で入力してください";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(4, 109);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(110, 20);
            this.label11.TabIndex = 108;
            this.label11.Text = "現場名２";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(4, 88);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(110, 20);
            this.label10.TabIndex = 106;
            this.label10.Text = "現場名１";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(4, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 20);
            this.label9.TabIndex = 104;
            this.label9.Text = "フリガナ";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.Location = new System.Drawing.Point(751, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 119;
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
            this.TEKIYOU_END.Location = new System.Drawing.Point(778, 108);
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
            this.TEKIYOU_END.TabIndex = 200;
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
            this.TEKIYOU_BEGIN.Location = new System.Drawing.Point(619, 108);
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
            this.TEKIYOU_BEGIN.TabIndex = 190;
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
            this.TEKIYOU_LABEL.Location = new System.Drawing.Point(505, 108);
            this.TEKIYOU_LABEL.Name = "TEKIYOU_LABEL";
            this.TEKIYOU_LABEL.Size = new System.Drawing.Size(110, 20);
            this.TEKIYOU_LABEL.TabIndex = 117;
            this.TEKIYOU_LABEL.Text = "適用期間";
            this.TEKIYOU_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Location = new System.Drawing.Point(741, 131);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Name = "ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Size = new System.Drawing.Size(84, 16);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.TabIndex = 230;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Text = "適用期間外";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.UseVisualStyleBackColor = true;
            // 
            // ICHIRAN_HYOUJI_JOUKEN_DELETED
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Location = new System.Drawing.Point(687, 131);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Name = "ICHIRAN_HYOUJI_JOUKEN_DELETED";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Size = new System.Drawing.Size(48, 16);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.TabIndex = 220;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Text = "削除";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.UseVisualStyleBackColor = true;
            // 
            // ICHIRAN_HYOUJI_JOUKEN_TEKIYOU
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Location = new System.Drawing.Point(621, 131);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Name = "ICHIRAN_HYOUJI_JOUKEN_TEKIYOU";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Size = new System.Drawing.Size(60, 16);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.TabIndex = 210;
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
            this.label1.Location = new System.Drawing.Point(505, 129);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 416;
            this.label1.Text = "表示条件";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.GYOUSHA_SEARCH_BUTTON.Location = new System.Drawing.Point(458, 24);
            this.GYOUSHA_SEARCH_BUTTON.Name = "GYOUSHA_SEARCH_BUTTON";
            this.GYOUSHA_SEARCH_BUTTON.PopupAfterExecute = null;
            this.GYOUSHA_SEARCH_BUTTON.PopupAfterExecuteMethod = "GYOUSHA_PopupAfterExecuteMethod";
            this.GYOUSHA_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.GYOUSHA_SEARCH_BUTTON.PopupBeforeExecuteMethod = "GYOUSHA_PopupBeforeExecuteMethod";
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
            this.GYOUSHA_SEARCH_BUTTON.TabIndex = 80;
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
            this.GYOUSHA_RNAME.Location = new System.Drawing.Point(166, 25);
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
            this.GYOUSHA_RNAME.TabIndex = 70;
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
            this.GYOUSHA_LABEL.Location = new System.Drawing.Point(4, 25);
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
            this.GYOUSHA_CD.GetCodeMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GYOUSHA_CD.IsInputErrorOccured = false;
            this.GYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.GYOUSHA_CD.Location = new System.Drawing.Point(117, 25);
            this.GYOUSHA_CD.MaxLength = 6;
            this.GYOUSHA_CD.Name = "GYOUSHA_CD";
            this.GYOUSHA_CD.PopupAfterExecute = null;
            this.GYOUSHA_CD.PopupAfterExecuteMethod = "GYOUSHA_PopupAfterExecuteMethod";
            this.GYOUSHA_CD.PopupBeforeExecute = null;
            this.GYOUSHA_CD.PopupBeforeExecuteMethod = "GYOUSHA_PopupBeforeExecuteMethod";
            this.GYOUSHA_CD.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_CD.PopupSearchSendParams")));
            this.GYOUSHA_CD.PopupSetFormField = "GYOUSHA_CD,GYOUSHA_RNAME";
            this.GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_CD.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_CD.popupWindowSetting")));
            this.GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.RegistCheckMethod")));
            this.GYOUSHA_CD.SetFormField = "GYOUSHA_CD,GYOUSHA_RNAME";
            this.GYOUSHA_CD.Size = new System.Drawing.Size(50, 20);
            this.GYOUSHA_CD.TabIndex = 60;
            this.GYOUSHA_CD.Tag = "業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GYOUSHA_CD.ZeroPaddengFlag = true;
            this.GYOUSHA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.GYOUSHA_CD_Validating);
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
            this.GENBA_SEARCH_BUTTON.Location = new System.Drawing.Point(458, 45);
            this.GENBA_SEARCH_BUTTON.Name = "GENBA_SEARCH_BUTTON";
            this.GENBA_SEARCH_BUTTON.PopupAfterExecute = null;
            this.GENBA_SEARCH_BUTTON.PopupAfterExecuteMethod = "AfterPopupGenba";
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
            this.GENBA_SEARCH_BUTTON.TabIndex = 110;
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
            this.GENBA_RNAME.Location = new System.Drawing.Point(166, 46);
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
            this.GENBA_RNAME.TabIndex = 100;
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
            this.GENBA_LABEL.Location = new System.Drawing.Point(4, 46);
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
            this.GENBA_CD.GetCodeMasterField = "GENBA_CD,GENBA_NAME_RYAKU";
            this.GENBA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GENBA_CD.IsInputErrorOccured = false;
            this.GENBA_CD.ItemDefinedTypes = "varchar";
            this.GENBA_CD.Location = new System.Drawing.Point(117, 46);
            this.GENBA_CD.MaxLength = 6;
            this.GENBA_CD.Name = "GENBA_CD";
            this.GENBA_CD.PopupAfterExecute = null;
            this.GENBA_CD.PopupAfterExecuteMethod = "";
            this.GENBA_CD.PopupBeforeExecute = null;
            this.GENBA_CD.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_CD.PopupSearchSendParams")));
            this.GENBA_CD.PopupSetFormField = "GENBA_CD,GENBA_RNAME,GYOUSHA_CD,GYOUSHA_RNAME";
            this.GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_CD.popupWindowSetting")));
            this.GENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.RegistCheckMethod")));
            this.GENBA_CD.SetFormField = "GENBA_CD,GENBA_RNAME,GYOUSHA_CD,GYOUSHA_RNAME";
            this.GENBA_CD.Size = new System.Drawing.Size(50, 20);
            this.GENBA_CD.TabIndex = 90;
            this.GENBA_CD.Tag = "現場を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GENBA_CD.ZeroPaddengFlag = true;
            this.GENBA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.GENBA_CD_Validating);
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
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(424, 130);
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
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 777;
            this.ISNOT_NEED_DELETE_FLG.TabStop = false;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // DIGI_MAP_NAME
            // 
            this.DIGI_MAP_NAME.BackColor = System.Drawing.SystemColors.Window;
            this.DIGI_MAP_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DIGI_MAP_NAME.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.DIGI_MAP_NAME.DBFieldsName = "MAP_NAME";
            this.DIGI_MAP_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.DIGI_MAP_NAME.DisplayItemName = "外部地点表示名";
            this.DIGI_MAP_NAME.DisplayPopUp = null;
            this.DIGI_MAP_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DIGI_MAP_NAME.FocusOutCheckMethod")));
            this.DIGI_MAP_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DIGI_MAP_NAME.ForeColor = System.Drawing.Color.Black;
            this.DIGI_MAP_NAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.DIGI_MAP_NAME.IsInputErrorOccured = false;
            this.DIGI_MAP_NAME.ItemDefinedTypes = "varchar";
            this.DIGI_MAP_NAME.Location = new System.Drawing.Point(619, 46);
            this.DIGI_MAP_NAME.MaxLength = 40;
            this.DIGI_MAP_NAME.Name = "DIGI_MAP_NAME";
            this.DIGI_MAP_NAME.PopupAfterExecute = null;
            this.DIGI_MAP_NAME.PopupBeforeExecute = null;
            this.DIGI_MAP_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DIGI_MAP_NAME.PopupSearchSendParams")));
            this.DIGI_MAP_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DIGI_MAP_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DIGI_MAP_NAME.popupWindowSetting")));
            this.DIGI_MAP_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DIGI_MAP_NAME.RegistCheckMethod")));
            this.DIGI_MAP_NAME.ShortItemName = "外部地図表示氏名";
            this.DIGI_MAP_NAME.Size = new System.Drawing.Size(192, 20);
            this.DIGI_MAP_NAME.TabIndex = 180;
            this.DIGI_MAP_NAME.Tag = "全角１０文字以内で入力してください";
            // 
            // DIGI_POINT_NAME
            // 
            this.DIGI_POINT_NAME.BackColor = System.Drawing.SystemColors.Window;
            this.DIGI_POINT_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DIGI_POINT_NAME.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.DIGI_POINT_NAME.CopyAutoSetControl = "";
            this.DIGI_POINT_NAME.DBFieldsName = "POINT_NAME";
            this.DIGI_POINT_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.DIGI_POINT_NAME.DisplayItemName = "外部地点名";
            this.DIGI_POINT_NAME.DisplayPopUp = null;
            this.DIGI_POINT_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DIGI_POINT_NAME.FocusOutCheckMethod")));
            this.DIGI_POINT_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DIGI_POINT_NAME.ForeColor = System.Drawing.Color.Black;
            this.DIGI_POINT_NAME.FuriganaAutoSetControl = "GenbaFurigana";
            this.DIGI_POINT_NAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.DIGI_POINT_NAME.IsInputErrorOccured = false;
            this.DIGI_POINT_NAME.ItemDefinedTypes = "varchar";
            this.DIGI_POINT_NAME.Location = new System.Drawing.Point(619, 25);
            this.DIGI_POINT_NAME.MaxLength = 40;
            this.DIGI_POINT_NAME.Name = "DIGI_POINT_NAME";
            this.DIGI_POINT_NAME.PopupAfterExecute = null;
            this.DIGI_POINT_NAME.PopupBeforeExecute = null;
            this.DIGI_POINT_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DIGI_POINT_NAME.PopupSearchSendParams")));
            this.DIGI_POINT_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DIGI_POINT_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DIGI_POINT_NAME.popupWindowSetting")));
            this.DIGI_POINT_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DIGI_POINT_NAME.RegistCheckMethod")));
            this.DIGI_POINT_NAME.ShortItemName = "現場名1";
            this.DIGI_POINT_NAME.Size = new System.Drawing.Size(336, 20);
            this.DIGI_POINT_NAME.TabIndex = 170;
            this.DIGI_POINT_NAME.Tag = "全角２０文字以内で入力してください";
            // 
            // DIGI_POINT_KANA_NAME
            // 
            this.DIGI_POINT_KANA_NAME.BackColor = System.Drawing.SystemColors.Window;
            this.DIGI_POINT_KANA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DIGI_POINT_KANA_NAME.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.DIGI_POINT_KANA_NAME.DBFieldsName = "POINT_KANA_NAME";
            this.DIGI_POINT_KANA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.DIGI_POINT_KANA_NAME.DisplayItemName = "外部地点カナ";
            this.DIGI_POINT_KANA_NAME.DisplayPopUp = null;
            this.DIGI_POINT_KANA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DIGI_POINT_KANA_NAME.FocusOutCheckMethod")));
            this.DIGI_POINT_KANA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DIGI_POINT_KANA_NAME.ForeColor = System.Drawing.Color.Black;
            this.DIGI_POINT_KANA_NAME.ImeMode = System.Windows.Forms.ImeMode.Katakana;
            this.DIGI_POINT_KANA_NAME.IsInputErrorOccured = false;
            this.DIGI_POINT_KANA_NAME.ItemDefinedTypes = "varchar";
            this.DIGI_POINT_KANA_NAME.Location = new System.Drawing.Point(619, 4);
            this.DIGI_POINT_KANA_NAME.MaxLength = 40;
            this.DIGI_POINT_KANA_NAME.Name = "DIGI_POINT_KANA_NAME";
            this.DIGI_POINT_KANA_NAME.PopupAfterExecute = null;
            this.DIGI_POINT_KANA_NAME.PopupBeforeExecute = null;
            this.DIGI_POINT_KANA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DIGI_POINT_KANA_NAME.PopupSearchSendParams")));
            this.DIGI_POINT_KANA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DIGI_POINT_KANA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DIGI_POINT_KANA_NAME.popupWindowSetting")));
            this.DIGI_POINT_KANA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DIGI_POINT_KANA_NAME.RegistCheckMethod")));
            this.DIGI_POINT_KANA_NAME.ShortItemName = "現場フリガナ";
            this.DIGI_POINT_KANA_NAME.Size = new System.Drawing.Size(336, 20);
            this.DIGI_POINT_KANA_NAME.TabIndex = 160;
            this.DIGI_POINT_KANA_NAME.Tag = "全角４０文字以内で入力してください";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(505, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 782;
            this.label4.Text = "外部地図表示名";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(505, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 20);
            this.label5.TabIndex = 780;
            this.label5.Text = "外部地点名";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(505, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 20);
            this.label6.TabIndex = 778;
            this.label6.Text = "外部地点カナ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_RenkeiKouho
            // 
            this.panel_RenkeiKouho.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_RenkeiKouho.Controls.Add(this.radbtnAll);
            this.panel_RenkeiKouho.Controls.Add(this.radbtnMirenkei);
            this.panel_RenkeiKouho.Controls.Add(this.radbtnAPIRenkei);
            this.panel_RenkeiKouho.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.panel_RenkeiKouho.Location = new System.Drawing.Point(136, 4);
            this.panel_RenkeiKouho.Name = "panel_RenkeiKouho";
            this.panel_RenkeiKouho.Size = new System.Drawing.Size(317, 20);
            this.panel_RenkeiKouho.TabIndex = 786;
            // 
            // radbtnAll
            // 
            this.radbtnAll.AutoSize = true;
            this.radbtnAll.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnAll.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnAll.FocusOutCheckMethod")));
            this.radbtnAll.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtnAll.LinkedTextBox = "txtNum_RenkeiJyoukyou";
            this.radbtnAll.Location = new System.Drawing.Point(195, 1);
            this.radbtnAll.Name = "radbtnAll";
            this.radbtnAll.PopupAfterExecute = null;
            this.radbtnAll.PopupBeforeExecute = null;
            this.radbtnAll.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnAll.PopupSearchSendParams")));
            this.radbtnAll.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnAll.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnAll.popupWindowSetting")));
            this.radbtnAll.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnAll.RegistCheckMethod")));
            this.radbtnAll.Size = new System.Drawing.Size(67, 17);
            this.radbtnAll.TabIndex = 50;
            this.radbtnAll.Tag = "全てで検索する場合チェックを付けてください";
            this.radbtnAll.Text = "3.全て";
            this.radbtnAll.UseVisualStyleBackColor = true;
            this.radbtnAll.Value = "3";
            // 
            // radbtnMirenkei
            // 
            this.radbtnMirenkei.AutoSize = true;
            this.radbtnMirenkei.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnMirenkei.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnMirenkei.FocusOutCheckMethod")));
            this.radbtnMirenkei.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtnMirenkei.LinkedTextBox = "txtNum_RenkeiJyoukyou";
            this.radbtnMirenkei.Location = new System.Drawing.Point(3, 1);
            this.radbtnMirenkei.Name = "radbtnMirenkei";
            this.radbtnMirenkei.PopupAfterExecute = null;
            this.radbtnMirenkei.PopupBeforeExecute = null;
            this.radbtnMirenkei.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnMirenkei.PopupSearchSendParams")));
            this.radbtnMirenkei.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnMirenkei.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnMirenkei.popupWindowSetting")));
            this.radbtnMirenkei.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnMirenkei.RegistCheckMethod")));
            this.radbtnMirenkei.Size = new System.Drawing.Size(81, 17);
            this.radbtnMirenkei.TabIndex = 20;
            this.radbtnMirenkei.Tag = "未連携で検索する場合チェックを付けてください";
            this.radbtnMirenkei.Text = "1.未連携";
            this.radbtnMirenkei.UseVisualStyleBackColor = true;
            this.radbtnMirenkei.Value = "1";
            // 
            // radbtnAPIRenkei
            // 
            this.radbtnAPIRenkei.AutoSize = true;
            this.radbtnAPIRenkei.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnAPIRenkei.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnAPIRenkei.FocusOutCheckMethod")));
            this.radbtnAPIRenkei.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtnAPIRenkei.LinkedTextBox = "txtNum_RenkeiJyoukyou";
            this.radbtnAPIRenkei.Location = new System.Drawing.Point(96, 1);
            this.radbtnAPIRenkei.Name = "radbtnAPIRenkei";
            this.radbtnAPIRenkei.PopupAfterExecute = null;
            this.radbtnAPIRenkei.PopupBeforeExecute = null;
            this.radbtnAPIRenkei.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnAPIRenkei.PopupSearchSendParams")));
            this.radbtnAPIRenkei.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnAPIRenkei.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnAPIRenkei.popupWindowSetting")));
            this.radbtnAPIRenkei.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnAPIRenkei.RegistCheckMethod")));
            this.radbtnAPIRenkei.Size = new System.Drawing.Size(81, 17);
            this.radbtnAPIRenkei.TabIndex = 30;
            this.radbtnAPIRenkei.Tag = "連携済で検索する場合チェックを付けてください";
            this.radbtnAPIRenkei.Text = "2.連携済";
            this.radbtnAPIRenkei.UseVisualStyleBackColor = true;
            this.radbtnAPIRenkei.Value = "2";
            // 
            // txtNum_RenkeiJyoukyou
            // 
            this.txtNum_RenkeiJyoukyou.BackColor = System.Drawing.SystemColors.Window;
            this.txtNum_RenkeiJyoukyou.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNum_RenkeiJyoukyou.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtNum_RenkeiJyoukyou.DisplayItemName = "連携候補";
            this.txtNum_RenkeiJyoukyou.DisplayPopUp = null;
            this.txtNum_RenkeiJyoukyou.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_RenkeiJyoukyou.FocusOutCheckMethod")));
            this.txtNum_RenkeiJyoukyou.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtNum_RenkeiJyoukyou.ForeColor = System.Drawing.Color.Black;
            this.txtNum_RenkeiJyoukyou.IsInputErrorOccured = false;
            this.txtNum_RenkeiJyoukyou.LinkedRadioButtonArray = new string[] {
        "radbtnMirenkei",
        "radbtnAPIRenkei",
        "radbtnAll"};
            this.txtNum_RenkeiJyoukyou.Location = new System.Drawing.Point(117, 4);
            this.txtNum_RenkeiJyoukyou.Name = "txtNum_RenkeiJyoukyou";
            this.txtNum_RenkeiJyoukyou.PopupAfterExecute = null;
            this.txtNum_RenkeiJyoukyou.PopupBeforeExecute = null;
            this.txtNum_RenkeiJyoukyou.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtNum_RenkeiJyoukyou.PopupSearchSendParams")));
            this.txtNum_RenkeiJyoukyou.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtNum_RenkeiJyoukyou.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtNum_RenkeiJyoukyou.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            3,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtNum_RenkeiJyoukyou.RangeSetting = rangeSettingDto1;
            this.txtNum_RenkeiJyoukyou.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_RenkeiJyoukyou.RegistCheckMethod")));
            this.txtNum_RenkeiJyoukyou.Size = new System.Drawing.Size(20, 20);
            this.txtNum_RenkeiJyoukyou.TabIndex = 10;
            this.txtNum_RenkeiJyoukyou.Tag = "【1～3】のいずれかで入力してください";
            this.txtNum_RenkeiJyoukyou.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNum_RenkeiJyoukyou.WordWrap = false;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(4, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 20);
            this.label7.TabIndex = 785;
            this.label7.Text = "連携状況";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GaibuRenkeiGenbaIchiranForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.ClientSize = new System.Drawing.Size(1008, 474);
            this.Controls.Add(this.panel_RenkeiKouho);
            this.Controls.Add(this.txtNum_RenkeiJyoukyou);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.DIGI_MAP_NAME);
            this.Controls.Add(this.DIGI_POINT_NAME);
            this.Controls.Add(this.DIGI_POINT_KANA_NAME);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.GENBA_SEARCH_BUTTON);
            this.Controls.Add(this.GENBA_RNAME);
            this.Controls.Add(this.GENBA_LABEL);
            this.Controls.Add(this.GENBA_CD);
            this.Controls.Add(this.GYOUSHA_SEARCH_BUTTON);
            this.Controls.Add(this.GYOUSHA_RNAME);
            this.Controls.Add(this.GYOUSHA_LABEL);
            this.Controls.Add(this.GYOUSHA_CD);
            this.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI);
            this.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_DELETED);
            this.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TEKIYOU_END);
            this.Controls.Add(this.TEKIYOU_BEGIN);
            this.Controls.Add(this.TEKIYOU_LABEL);
            this.Controls.Add(this.GENBA_NAME2);
            this.Controls.Add(this.GENBA_NAME1);
            this.Controls.Add(this.GENBA_FURIGANA);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.GENBA_NAME_RYAKU);
            this.Name = "GaibuRenkeiGenbaIchiranForm";
            this.Text = "TorihikisakiIchiran";
            this.Shown += new System.EventHandler(this.GenbaIchiranForm_Shown);
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.GENBA_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.GENBA_FURIGANA, 0);
            this.Controls.SetChildIndex(this.GENBA_NAME1, 0);
            this.Controls.SetChildIndex(this.GENBA_NAME2, 0);
            this.Controls.SetChildIndex(this.TEKIYOU_LABEL, 0);
            this.Controls.SetChildIndex(this.TEKIYOU_BEGIN, 0);
            this.Controls.SetChildIndex(this.TEKIYOU_END, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU, 0);
            this.Controls.SetChildIndex(this.ICHIRAN_HYOUJI_JOUKEN_DELETED, 0);
            this.Controls.SetChildIndex(this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_CD, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_LABEL, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_RNAME, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_SEARCH_BUTTON, 0);
            this.Controls.SetChildIndex(this.GENBA_CD, 0);
            this.Controls.SetChildIndex(this.GENBA_LABEL, 0);
            this.Controls.SetChildIndex(this.GENBA_RNAME, 0);
            this.Controls.SetChildIndex(this.GENBA_SEARCH_BUTTON, 0);
            this.Controls.SetChildIndex(this.ISNOT_NEED_DELETE_FLG, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.DIGI_POINT_KANA_NAME, 0);
            this.Controls.SetChildIndex(this.DIGI_POINT_NAME, 0);
            this.Controls.SetChildIndex(this.DIGI_MAP_NAME, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.txtNum_RenkeiJyoukyou, 0);
            this.Controls.SetChildIndex(this.panel_RenkeiKouho, 0);
            this.panel_RenkeiKouho.ResumeLayout(false);
            this.panel_RenkeiKouho.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        internal r_framework.CustomControl.CustomTextBox GENBA_NAME_RYAKU;
        internal r_framework.CustomControl.CustomTextBox GENBA_NAME2;
        internal r_framework.CustomControl.CustomTextBox GENBA_NAME1;
        internal r_framework.CustomControl.CustomTextBox GENBA_FURIGANA;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        internal r_framework.CustomControl.CustomDateTimePicker TEKIYOU_END;
        internal r_framework.CustomControl.CustomDateTimePicker TEKIYOU_BEGIN;
        internal System.Windows.Forms.Label TEKIYOU_LABEL;
        public System.Windows.Forms.CheckBox ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI;
        public System.Windows.Forms.CheckBox ICHIRAN_HYOUJI_JOUKEN_DELETED;
        public System.Windows.Forms.CheckBox ICHIRAN_HYOUJI_JOUKEN_TEKIYOU;
        public System.Windows.Forms.Label label1;
        internal r_framework.CustomControl.CustomPopupOpenButton GYOUSHA_SEARCH_BUTTON;
        internal r_framework.CustomControl.CustomTextBox GYOUSHA_RNAME;
        internal System.Windows.Forms.Label GYOUSHA_LABEL;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GYOUSHA_CD;
        internal r_framework.CustomControl.CustomPopupOpenButton GENBA_SEARCH_BUTTON;
        internal r_framework.CustomControl.CustomTextBox GENBA_RNAME;
        internal System.Windows.Forms.Label GENBA_LABEL;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GENBA_CD;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
        internal r_framework.CustomControl.CustomTextBox DIGI_MAP_NAME;
        internal r_framework.CustomControl.CustomTextBox DIGI_POINT_NAME;
        internal r_framework.CustomControl.CustomTextBox DIGI_POINT_KANA_NAME;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private r_framework.CustomControl.CustomPanel panel_RenkeiKouho;
        public r_framework.CustomControl.CustomRadioButton radbtnAll;
        public r_framework.CustomControl.CustomRadioButton radbtnMirenkei;
        public r_framework.CustomControl.CustomRadioButton radbtnAPIRenkei;
        public r_framework.CustomControl.CustomNumericTextBox2 txtNum_RenkeiJyoukyou;
        public System.Windows.Forms.Label label7;
    }
}
