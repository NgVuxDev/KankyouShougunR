namespace Shougun.Core.SalesManagement.MotochoHaniJokenPopUp.APP
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
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            this.TITLE_LABEL = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TORIHIKISAKI_NAME_END = new r_framework.CustomControl.CustomTextBox();
            this.TORIHIKISAKI_NAME_START = new r_framework.CustomControl.CustomTextBox();
            this.OUTPUT_KBN_LABEL = new System.Windows.Forms.Label();
            this.TORIHIKISAKI_LABEL = new System.Windows.Forms.Label();
            this.DATE_HANI_LABEL = new System.Windows.Forms.Label();
            this.DATE_HANI_END = new r_framework.CustomControl.CustomDateTimePicker();
            this.DATE_HANI_START = new r_framework.CustomControl.CustomDateTimePicker();
            this.OUTPUT_KBN_ALL = new r_framework.CustomControl.CustomRadioButton();
            this.OUTPUT_KBN_ONLY = new r_framework.CustomControl.CustomRadioButton();
            this.OUTPUT_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TORIHIKISAKI_CD_START = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.TORIHIKISAKI_CD_END = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.TORIHIKISAKI_SEARCH_START = new r_framework.CustomControl.CustomPopupOpenButton();
            this.TORIHIKISAKI_SEARCH_END = new r_framework.CustomControl.CustomPopupOpenButton();
            this.bt_func8 = new r_framework.CustomControl.CustomButton();
            this.bt_func12 = new r_framework.CustomControl.CustomButton();
            this.MOTOTYOU_KBN_LABEL = new System.Windows.Forms.Label();
            this.TYUUSYUTU_KBN__LABEL = new System.Windows.Forms.Label();
            this.SHIMEBI_LABEL = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.TORIHIKI_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.KAKE_MOTOTYOU = new r_framework.CustomControl.CustomRadioButton();
            this.GENKIN_MOTOTYOU = new r_framework.CustomControl.CustomRadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.TYUUSYUTU_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.DENPYOU_DATE_MATU = new r_framework.CustomControl.CustomRadioButton();
            this.URIAGE_DATE_SHIMEBI = new r_framework.CustomControl.CustomRadioButton();
            this.cb_shimebi = new r_framework.CustomControl.CustomComboBox();
            this.btn_jigetsu = new r_framework.CustomControl.CustomButton();
            this.btn_zengetsu = new r_framework.CustomControl.CustomButton();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // TITLE_LABEL
            // 
            this.TITLE_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.TITLE_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TITLE_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TITLE_LABEL.ForeColor = System.Drawing.Color.White;
            this.TITLE_LABEL.Location = new System.Drawing.Point(12, 9);
            this.TITLE_LABEL.Name = "TITLE_LABEL";
            this.TITLE_LABEL.Size = new System.Drawing.Size(364, 31);
            this.TITLE_LABEL.TabIndex = 380;
            this.TITLE_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(502, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 613;
            this.label4.Text = "～";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(273, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 612;
            this.label3.Text = "～";
            // 
            // TORIHIKISAKI_NAME_END
            // 
            this.TORIHIKISAKI_NAME_END.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.TORIHIKISAKI_NAME_END.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_NAME_END.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.TORIHIKISAKI_NAME_END.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_NAME_END.DisplayPopUp = null;
            this.TORIHIKISAKI_NAME_END.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_END.FocusOutCheckMethod")));
            this.TORIHIKISAKI_NAME_END.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TORIHIKISAKI_NAME_END.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_NAME_END.IsInputErrorOccured = false;
            this.TORIHIKISAKI_NAME_END.Location = new System.Drawing.Point(588, 160);
            this.TORIHIKISAKI_NAME_END.MaxLength = 0;
            this.TORIHIKISAKI_NAME_END.Name = "TORIHIKISAKI_NAME_END";
            this.TORIHIKISAKI_NAME_END.PopupAfterExecute = null;
            this.TORIHIKISAKI_NAME_END.PopupBeforeExecute = null;
            this.TORIHIKISAKI_NAME_END.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_NAME_END.PopupSearchSendParams")));
            this.TORIHIKISAKI_NAME_END.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_NAME_END.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_NAME_END.popupWindowSetting")));
            this.TORIHIKISAKI_NAME_END.ReadOnly = true;
            this.TORIHIKISAKI_NAME_END.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_END.RegistCheckMethod")));
            this.TORIHIKISAKI_NAME_END.Size = new System.Drawing.Size(286, 20);
            this.TORIHIKISAKI_NAME_END.TabIndex = 609;
            this.TORIHIKISAKI_NAME_END.TabStop = false;
            this.TORIHIKISAKI_NAME_END.Tag = "";
            // 
            // TORIHIKISAKI_NAME_START
            // 
            this.TORIHIKISAKI_NAME_START.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.TORIHIKISAKI_NAME_START.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_NAME_START.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.TORIHIKISAKI_NAME_START.DBFieldsName = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_START.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_NAME_START.DisplayPopUp = null;
            this.TORIHIKISAKI_NAME_START.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_START.FocusOutCheckMethod")));
            this.TORIHIKISAKI_NAME_START.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TORIHIKISAKI_NAME_START.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_NAME_START.IsInputErrorOccured = false;
            this.TORIHIKISAKI_NAME_START.Location = new System.Drawing.Point(182, 160);
            this.TORIHIKISAKI_NAME_START.MaxLength = 0;
            this.TORIHIKISAKI_NAME_START.Name = "TORIHIKISAKI_NAME_START";
            this.TORIHIKISAKI_NAME_START.PopupAfterExecute = null;
            this.TORIHIKISAKI_NAME_START.PopupBeforeExecute = null;
            this.TORIHIKISAKI_NAME_START.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_NAME_START.PopupSearchSendParams")));
            this.TORIHIKISAKI_NAME_START.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_NAME_START.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_NAME_START.popupWindowSetting")));
            this.TORIHIKISAKI_NAME_START.ReadOnly = true;
            this.TORIHIKISAKI_NAME_START.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_START.RegistCheckMethod")));
            this.TORIHIKISAKI_NAME_START.Size = new System.Drawing.Size(286, 20);
            this.TORIHIKISAKI_NAME_START.TabIndex = 606;
            this.TORIHIKISAKI_NAME_START.TabStop = false;
            this.TORIHIKISAKI_NAME_START.Tag = "";
            // 
            // OUTPUT_KBN_LABEL
            // 
            this.OUTPUT_KBN_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.OUTPUT_KBN_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OUTPUT_KBN_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OUTPUT_KBN_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OUTPUT_KBN_LABEL.ForeColor = System.Drawing.Color.White;
            this.OUTPUT_KBN_LABEL.Location = new System.Drawing.Point(12, 185);
            this.OUTPUT_KBN_LABEL.Name = "OUTPUT_KBN_LABEL";
            this.OUTPUT_KBN_LABEL.Size = new System.Drawing.Size(110, 20);
            this.OUTPUT_KBN_LABEL.TabIndex = 603;
            this.OUTPUT_KBN_LABEL.Text = "出力内容※";
            this.OUTPUT_KBN_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TORIHIKISAKI_LABEL
            // 
            this.TORIHIKISAKI_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.TORIHIKISAKI_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TORIHIKISAKI_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TORIHIKISAKI_LABEL.ForeColor = System.Drawing.Color.White;
            this.TORIHIKISAKI_LABEL.Location = new System.Drawing.Point(12, 159);
            this.TORIHIKISAKI_LABEL.Name = "TORIHIKISAKI_LABEL";
            this.TORIHIKISAKI_LABEL.Size = new System.Drawing.Size(110, 20);
            this.TORIHIKISAKI_LABEL.TabIndex = 0;
            this.TORIHIKISAKI_LABEL.Text = "取引先";
            this.TORIHIKISAKI_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DATE_HANI_LABEL
            // 
            this.DATE_HANI_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.DATE_HANI_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DATE_HANI_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DATE_HANI_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DATE_HANI_LABEL.ForeColor = System.Drawing.Color.White;
            this.DATE_HANI_LABEL.Location = new System.Drawing.Point(12, 133);
            this.DATE_HANI_LABEL.Name = "DATE_HANI_LABEL";
            this.DATE_HANI_LABEL.Size = new System.Drawing.Size(110, 20);
            this.DATE_HANI_LABEL.TabIndex = 601;
            this.DATE_HANI_LABEL.Text = "日付範囲指定※";
            this.DATE_HANI_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DATE_HANI_END
            // 
            this.DATE_HANI_END.BackColor = System.Drawing.SystemColors.Window;
            this.DATE_HANI_END.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DATE_HANI_END.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.DATE_HANI_END.Checked = false;
            this.DATE_HANI_END.CustomFormat = "yyyy/MM/dd(ddd)";
            this.DATE_HANI_END.DateTimeNowYear = "";
            this.DATE_HANI_END.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_HANI_END.DisplayItemName = "終了伝票日付";
            this.DATE_HANI_END.DisplayPopUp = null;
            this.DATE_HANI_END.Enabled = false;
            this.DATE_HANI_END.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_HANI_END.FocusOutCheckMethod")));
            this.DATE_HANI_END.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DATE_HANI_END.ForeColor = System.Drawing.Color.Black;
            this.DATE_HANI_END.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DATE_HANI_END.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.DATE_HANI_END.IsInputErrorOccured = false;
            this.DATE_HANI_END.Location = new System.Drawing.Point(300, 134);
            this.DATE_HANI_END.MaxLength = 10;
            this.DATE_HANI_END.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.DATE_HANI_END.Name = "DATE_HANI_END";
            this.DATE_HANI_END.NullValue = "";
            this.DATE_HANI_END.PopupAfterExecute = null;
            this.DATE_HANI_END.PopupBeforeExecute = null;
            this.DATE_HANI_END.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DATE_HANI_END.PopupSearchSendParams")));
            this.DATE_HANI_END.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DATE_HANI_END.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DATE_HANI_END.popupWindowSetting")));
            this.DATE_HANI_END.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_HANI_END.RegistCheckMethod")));
            this.DATE_HANI_END.ShortItemName = "終了伝票日付";
            this.DATE_HANI_END.Size = new System.Drawing.Size(138, 20);
            this.DATE_HANI_END.TabIndex = 4;
            this.DATE_HANI_END.Tag = "終了日付を入力してください";
            this.DATE_HANI_END.Text = "2013/12/04(水)";
            this.DATE_HANI_END.Value = new System.DateTime(2013, 12, 4, 0, 0, 0, 0);
            this.DATE_HANI_END.Leave += new System.EventHandler(this.DATE_HANI_END_Leave);
            // 
            // DATE_HANI_START
            // 
            this.DATE_HANI_START.BackColor = System.Drawing.SystemColors.Window;
            this.DATE_HANI_START.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DATE_HANI_START.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.DATE_HANI_START.Checked = false;
            this.DATE_HANI_START.CustomFormat = "yyyy/MM/dd(ddd)";
            this.DATE_HANI_START.DateTimeNowYear = "";
            this.DATE_HANI_START.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_HANI_START.DisplayItemName = "開始伝票日付";
            this.DATE_HANI_START.DisplayPopUp = null;
            this.DATE_HANI_START.Enabled = false;
            this.DATE_HANI_START.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_HANI_START.FocusOutCheckMethod")));
            this.DATE_HANI_START.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DATE_HANI_START.ForeColor = System.Drawing.Color.Black;
            this.DATE_HANI_START.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DATE_HANI_START.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.DATE_HANI_START.IsInputErrorOccured = false;
            this.DATE_HANI_START.Location = new System.Drawing.Point(128, 134);
            this.DATE_HANI_START.MaxLength = 10;
            this.DATE_HANI_START.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.DATE_HANI_START.Name = "DATE_HANI_START";
            this.DATE_HANI_START.NullValue = "";
            this.DATE_HANI_START.PopupAfterExecute = null;
            this.DATE_HANI_START.PopupBeforeExecute = null;
            this.DATE_HANI_START.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DATE_HANI_START.PopupSearchSendParams")));
            this.DATE_HANI_START.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DATE_HANI_START.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DATE_HANI_START.popupWindowSetting")));
            this.DATE_HANI_START.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_HANI_START.RegistCheckMethod")));
            this.DATE_HANI_START.ShortItemName = "開始伝票日付";
            this.DATE_HANI_START.Size = new System.Drawing.Size(138, 20);
            this.DATE_HANI_START.TabIndex = 3;
            this.DATE_HANI_START.Tag = "開始日付を入力してください";
            this.DATE_HANI_START.Text = "2013/12/04(水)";
            this.DATE_HANI_START.Value = new System.DateTime(2013, 12, 4, 0, 0, 0, 0);
            this.DATE_HANI_START.Leave += new System.EventHandler(this.DATE_HANI_START_Leave);
            // 
            // OUTPUT_KBN_ALL
            // 
            this.OUTPUT_KBN_ALL.AutoSize = true;
            this.OUTPUT_KBN_ALL.DefaultBackColor = System.Drawing.Color.Empty;
            this.OUTPUT_KBN_ALL.DisplayItemName = "しない";
            this.OUTPUT_KBN_ALL.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("OUTPUT_KBN_ALL.FocusOutCheckMethod")));
            this.OUTPUT_KBN_ALL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OUTPUT_KBN_ALL.LinkedTextBox = "OUTPUT_KBN";
            this.OUTPUT_KBN_ALL.Location = new System.Drawing.Point(137, 0);
            this.OUTPUT_KBN_ALL.Name = "OUTPUT_KBN_ALL";
            this.OUTPUT_KBN_ALL.PopupAfterExecute = null;
            this.OUTPUT_KBN_ALL.PopupBeforeExecute = null;
            this.OUTPUT_KBN_ALL.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("OUTPUT_KBN_ALL.PopupSearchSendParams")));
            this.OUTPUT_KBN_ALL.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.OUTPUT_KBN_ALL.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("OUTPUT_KBN_ALL.popupWindowSetting")));
            this.OUTPUT_KBN_ALL.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("OUTPUT_KBN_ALL.RegistCheckMethod")));
            this.OUTPUT_KBN_ALL.ShortItemName = "しない";
            this.OUTPUT_KBN_ALL.Size = new System.Drawing.Size(123, 17);
            this.OUTPUT_KBN_ALL.TabIndex = 3;
            this.OUTPUT_KBN_ALL.Tag = "出力内容を発生なし含むにする場合にはチェックを付けてください";
            this.OUTPUT_KBN_ALL.Text = "2.発生なし含む";
            this.OUTPUT_KBN_ALL.UseVisualStyleBackColor = true;
            this.OUTPUT_KBN_ALL.Value = "2";
            // 
            // OUTPUT_KBN_ONLY
            // 
            this.OUTPUT_KBN_ONLY.AutoSize = true;
            this.OUTPUT_KBN_ONLY.DefaultBackColor = System.Drawing.Color.Empty;
            this.OUTPUT_KBN_ONLY.DisplayItemName = "する";
            this.OUTPUT_KBN_ONLY.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("OUTPUT_KBN_ONLY.FocusOutCheckMethod")));
            this.OUTPUT_KBN_ONLY.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OUTPUT_KBN_ONLY.LinkedTextBox = "OUTPUT_KBN";
            this.OUTPUT_KBN_ONLY.Location = new System.Drawing.Point(29, 0);
            this.OUTPUT_KBN_ONLY.Name = "OUTPUT_KBN_ONLY";
            this.OUTPUT_KBN_ONLY.PopupAfterExecute = null;
            this.OUTPUT_KBN_ONLY.PopupBeforeExecute = null;
            this.OUTPUT_KBN_ONLY.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("OUTPUT_KBN_ONLY.PopupSearchSendParams")));
            this.OUTPUT_KBN_ONLY.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.OUTPUT_KBN_ONLY.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("OUTPUT_KBN_ONLY.popupWindowSetting")));
            this.OUTPUT_KBN_ONLY.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("OUTPUT_KBN_ONLY.RegistCheckMethod")));
            this.OUTPUT_KBN_ONLY.ShortItemName = "する";
            this.OUTPUT_KBN_ONLY.Size = new System.Drawing.Size(95, 17);
            this.OUTPUT_KBN_ONLY.TabIndex = 2;
            this.OUTPUT_KBN_ONLY.Tag = "出力内容を発生のみにする場合にはチェックを付けてください";
            this.OUTPUT_KBN_ONLY.Text = "1.発生のみ";
            this.OUTPUT_KBN_ONLY.UseVisualStyleBackColor = true;
            this.OUTPUT_KBN_ONLY.Value = "1";
            // 
            // OUTPUT_KBN
            // 
            this.OUTPUT_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.OUTPUT_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OUTPUT_KBN.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.OUTPUT_KBN.DBFieldsName = "";
            this.OUTPUT_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.OUTPUT_KBN.DisplayItemName = "出力区分";
            this.OUTPUT_KBN.DisplayPopUp = null;
            this.OUTPUT_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("OUTPUT_KBN.FocusOutCheckMethod")));
            this.OUTPUT_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.OUTPUT_KBN.ForeColor = System.Drawing.Color.Black;
            this.OUTPUT_KBN.IsInputErrorOccured = false;
            this.OUTPUT_KBN.ItemDefinedTypes = "smallint";
            this.OUTPUT_KBN.LinkedRadioButtonArray = new string[] {
        "OUTPUT_KBN_ONLY",
        "OUTPUT_KBN_ALL"};
            this.OUTPUT_KBN.Location = new System.Drawing.Point(-1, -1);
            this.OUTPUT_KBN.Name = "OUTPUT_KBN";
            this.OUTPUT_KBN.PopupAfterExecute = null;
            this.OUTPUT_KBN.PopupBeforeExecute = null;
            this.OUTPUT_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("OUTPUT_KBN.PopupSearchSendParams")));
            this.OUTPUT_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.OUTPUT_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("OUTPUT_KBN.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.OUTPUT_KBN.RangeSetting = rangeSettingDto1;
            this.OUTPUT_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("OUTPUT_KBN.RegistCheckMethod")));
            this.OUTPUT_KBN.ShortItemName = "出力区分";
            this.OUTPUT_KBN.Size = new System.Drawing.Size(20, 20);
            this.OUTPUT_KBN.TabIndex = 0;
            this.OUTPUT_KBN.Tag = "【1、2】のいずれかで入力してください";
            this.OUTPUT_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.OUTPUT_KBN.WordWrap = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.OUTPUT_KBN);
            this.panel1.Controls.Add(this.OUTPUT_KBN_ONLY);
            this.panel1.Controls.Add(this.OUTPUT_KBN_ALL);
            this.panel1.Location = new System.Drawing.Point(128, 185);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(274, 20);
            this.panel1.TabIndex = 7;
            this.panel1.TabStop = true;
            // 
            // TORIHIKISAKI_CD_START
            // 
            this.TORIHIKISAKI_CD_START.BackColor = System.Drawing.SystemColors.Window;
            this.TORIHIKISAKI_CD_START.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_CD_START.ChangeUpperCase = true;
            this.TORIHIKISAKI_CD_START.CharacterLimitList = null;
            this.TORIHIKISAKI_CD_START.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.TORIHIKISAKI_CD_START.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TORIHIKISAKI_CD_START.DBFieldsName = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD_START.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_CD_START.DisplayItemName = "開始取引先CD";
            this.TORIHIKISAKI_CD_START.DisplayPopUp = null;
            this.TORIHIKISAKI_CD_START.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD_START.FocusOutCheckMethod")));
            this.TORIHIKISAKI_CD_START.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TORIHIKISAKI_CD_START.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_CD_START.GetCodeMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD_START.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TORIHIKISAKI_CD_START.IsInputErrorOccured = false;
            this.TORIHIKISAKI_CD_START.ItemDefinedTypes = "";
            this.TORIHIKISAKI_CD_START.Location = new System.Drawing.Point(128, 160);
            this.TORIHIKISAKI_CD_START.MaxLength = 6;
            this.TORIHIKISAKI_CD_START.Name = "TORIHIKISAKI_CD_START";
            this.TORIHIKISAKI_CD_START.PopupAfterExecute = null;
            this.TORIHIKISAKI_CD_START.PopupBeforeExecute = null;
            this.TORIHIKISAKI_CD_START.PopupGetMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD_START.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_CD_START.PopupSearchSendParams")));
            this.TORIHIKISAKI_CD_START.PopupSendParams = new string[0];
            this.TORIHIKISAKI_CD_START.PopupSetFormField = "TORIHIKISAKI_CD_START,TORIHIKISAKI_NAME_START";
            this.TORIHIKISAKI_CD_START.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TORIHIKISAKI_CD_START.PopupWindowName = "検索共通ポップアップ";
            this.TORIHIKISAKI_CD_START.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_CD_START.popupWindowSetting")));
            this.TORIHIKISAKI_CD_START.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD_START.RegistCheckMethod")));
            this.TORIHIKISAKI_CD_START.SetFormField = "TORIHIKISAKI_CD_START,TORIHIKISAKI_NAME_START";
            this.TORIHIKISAKI_CD_START.ShortItemName = "開始取引先";
            this.TORIHIKISAKI_CD_START.Size = new System.Drawing.Size(55, 20);
            this.TORIHIKISAKI_CD_START.TabIndex = 5;
            this.TORIHIKISAKI_CD_START.Tag = "開始取引先を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.TORIHIKISAKI_CD_START.ZeroPaddengFlag = true;
            this.TORIHIKISAKI_CD_START.Validating += new System.ComponentModel.CancelEventHandler(this.TORIHIKISAKI_CD_START_Validating);
            // 
            // TORIHIKISAKI_CD_END
            // 
            this.TORIHIKISAKI_CD_END.BackColor = System.Drawing.SystemColors.Window;
            this.TORIHIKISAKI_CD_END.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_CD_END.ChangeUpperCase = true;
            this.TORIHIKISAKI_CD_END.CharacterLimitList = null;
            this.TORIHIKISAKI_CD_END.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.TORIHIKISAKI_CD_END.DBFieldsName = "";
            this.TORIHIKISAKI_CD_END.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_CD_END.DisplayItemName = "終了取引先CD";
            this.TORIHIKISAKI_CD_END.DisplayPopUp = null;
            this.TORIHIKISAKI_CD_END.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD_END.FocusOutCheckMethod")));
            this.TORIHIKISAKI_CD_END.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TORIHIKISAKI_CD_END.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_CD_END.GetCodeMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD_END.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TORIHIKISAKI_CD_END.IsInputErrorOccured = false;
            this.TORIHIKISAKI_CD_END.ItemDefinedTypes = "";
            this.TORIHIKISAKI_CD_END.Location = new System.Drawing.Point(534, 160);
            this.TORIHIKISAKI_CD_END.MaxLength = 6;
            this.TORIHIKISAKI_CD_END.Name = "TORIHIKISAKI_CD_END";
            this.TORIHIKISAKI_CD_END.PopupAfterExecute = null;
            this.TORIHIKISAKI_CD_END.PopupBeforeExecute = null;
            this.TORIHIKISAKI_CD_END.PopupGetMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD_END.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_CD_END.PopupSearchSendParams")));
            this.TORIHIKISAKI_CD_END.PopupSetFormField = "TORIHIKISAKI_CD_END, TORIHIKISAKI_NAME_END";
            this.TORIHIKISAKI_CD_END.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TORIHIKISAKI_CD_END.PopupWindowName = "検索共通ポップアップ";
            this.TORIHIKISAKI_CD_END.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_CD_END.popupWindowSetting")));
            this.TORIHIKISAKI_CD_END.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD_END.RegistCheckMethod")));
            this.TORIHIKISAKI_CD_END.SetFormField = "TORIHIKISAKI_CD_END, TORIHIKISAKI_NAME_END";
            this.TORIHIKISAKI_CD_END.ShortItemName = "終了取引先";
            this.TORIHIKISAKI_CD_END.Size = new System.Drawing.Size(55, 20);
            this.TORIHIKISAKI_CD_END.TabIndex = 6;
            this.TORIHIKISAKI_CD_END.Tag = "終了取引先を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.TORIHIKISAKI_CD_END.ZeroPaddengFlag = true;
            this.TORIHIKISAKI_CD_END.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TORIHIKISAKI_CD_END_MouseDoubleClick);
            this.TORIHIKISAKI_CD_END.Validating += new System.ComponentModel.CancelEventHandler(this.TORIHIKISAKI_CD_END_Validating);
            // 
            // TORIHIKISAKI_SEARCH_START
            // 
            this.TORIHIKISAKI_SEARCH_START.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.TORIHIKISAKI_SEARCH_START.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TORIHIKISAKI_SEARCH_START.DBFieldsName = null;
            this.TORIHIKISAKI_SEARCH_START.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_SEARCH_START.DisplayItemName = "取引先検索";
            this.TORIHIKISAKI_SEARCH_START.DisplayPopUp = null;
            this.TORIHIKISAKI_SEARCH_START.ErrorMessage = null;
            this.TORIHIKISAKI_SEARCH_START.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_SEARCH_START.FocusOutCheckMethod")));
            this.TORIHIKISAKI_SEARCH_START.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TORIHIKISAKI_SEARCH_START.GetCodeMasterField = null;
            this.TORIHIKISAKI_SEARCH_START.Image = ((System.Drawing.Image)(resources.GetObject("TORIHIKISAKI_SEARCH_START.Image")));
            this.TORIHIKISAKI_SEARCH_START.ItemDefinedTypes = null;
            this.TORIHIKISAKI_SEARCH_START.LinkedSettingTextBox = null;
            this.TORIHIKISAKI_SEARCH_START.LinkedTextBoxs = null;
            this.TORIHIKISAKI_SEARCH_START.Location = new System.Drawing.Point(470, 159);
            this.TORIHIKISAKI_SEARCH_START.Name = "TORIHIKISAKI_SEARCH_START";
            this.TORIHIKISAKI_SEARCH_START.PopupAfterExecute = null;
            this.TORIHIKISAKI_SEARCH_START.PopupAfterExecuteMethod = "FromCDPopupAfterUpdate";
            this.TORIHIKISAKI_SEARCH_START.PopupBeforeExecute = null;
            this.TORIHIKISAKI_SEARCH_START.PopupGetMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_SEARCH_START.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_SEARCH_START.PopupSearchSendParams")));
            this.TORIHIKISAKI_SEARCH_START.PopupSetFormField = "TORIHIKISAKI_CD_START,TORIHIKISAKI_NAME_START";
            this.TORIHIKISAKI_SEARCH_START.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TORIHIKISAKI_SEARCH_START.PopupWindowName = "検索共通ポップアップ";
            this.TORIHIKISAKI_SEARCH_START.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_SEARCH_START.popupWindowSetting")));
            this.TORIHIKISAKI_SEARCH_START.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_SEARCH_START.RegistCheckMethod")));
            this.TORIHIKISAKI_SEARCH_START.SearchDisplayFlag = 0;
            this.TORIHIKISAKI_SEARCH_START.SetFormField = "TORIHIKISAKI_CD_START,TORIHIKISAKI_NAME_START";
            this.TORIHIKISAKI_SEARCH_START.ShortItemName = "取引先検索";
            this.TORIHIKISAKI_SEARCH_START.Size = new System.Drawing.Size(22, 22);
            this.TORIHIKISAKI_SEARCH_START.TabIndex = 694;
            this.TORIHIKISAKI_SEARCH_START.TabStop = false;
            this.TORIHIKISAKI_SEARCH_START.Tag = "検索画面を表示します";
            this.TORIHIKISAKI_SEARCH_START.UseVisualStyleBackColor = false;
            this.TORIHIKISAKI_SEARCH_START.ZeroPaddengFlag = false;
            // 
            // TORIHIKISAKI_SEARCH_END
            // 
            this.TORIHIKISAKI_SEARCH_END.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.TORIHIKISAKI_SEARCH_END.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TORIHIKISAKI_SEARCH_END.DBFieldsName = null;
            this.TORIHIKISAKI_SEARCH_END.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_SEARCH_END.DisplayItemName = "取引先検索";
            this.TORIHIKISAKI_SEARCH_END.DisplayPopUp = null;
            this.TORIHIKISAKI_SEARCH_END.ErrorMessage = null;
            this.TORIHIKISAKI_SEARCH_END.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_SEARCH_END.FocusOutCheckMethod")));
            this.TORIHIKISAKI_SEARCH_END.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TORIHIKISAKI_SEARCH_END.GetCodeMasterField = null;
            this.TORIHIKISAKI_SEARCH_END.Image = ((System.Drawing.Image)(resources.GetObject("TORIHIKISAKI_SEARCH_END.Image")));
            this.TORIHIKISAKI_SEARCH_END.ItemDefinedTypes = null;
            this.TORIHIKISAKI_SEARCH_END.LinkedSettingTextBox = null;
            this.TORIHIKISAKI_SEARCH_END.LinkedTextBoxs = null;
            this.TORIHIKISAKI_SEARCH_END.Location = new System.Drawing.Point(877, 159);
            this.TORIHIKISAKI_SEARCH_END.Name = "TORIHIKISAKI_SEARCH_END";
            this.TORIHIKISAKI_SEARCH_END.PopupAfterExecute = null;
            this.TORIHIKISAKI_SEARCH_END.PopupAfterExecuteMethod = "ToCDPopupAfterUpdate";
            this.TORIHIKISAKI_SEARCH_END.PopupBeforeExecute = null;
            this.TORIHIKISAKI_SEARCH_END.PopupGetMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_SEARCH_END.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_SEARCH_END.PopupSearchSendParams")));
            this.TORIHIKISAKI_SEARCH_END.PopupSetFormField = "TORIHIKISAKI_CD_END, TORIHIKISAKI_NAME_END";
            this.TORIHIKISAKI_SEARCH_END.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TORIHIKISAKI_SEARCH_END.PopupWindowName = "検索共通ポップアップ";
            this.TORIHIKISAKI_SEARCH_END.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_SEARCH_END.popupWindowSetting")));
            this.TORIHIKISAKI_SEARCH_END.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_SEARCH_END.RegistCheckMethod")));
            this.TORIHIKISAKI_SEARCH_END.SearchDisplayFlag = 0;
            this.TORIHIKISAKI_SEARCH_END.SetFormField = "TORIHIKISAKI_CD_END, TORIHIKISAKI_NAME_END";
            this.TORIHIKISAKI_SEARCH_END.ShortItemName = "取引先検索";
            this.TORIHIKISAKI_SEARCH_END.Size = new System.Drawing.Size(22, 22);
            this.TORIHIKISAKI_SEARCH_END.TabIndex = 695;
            this.TORIHIKISAKI_SEARCH_END.TabStop = false;
            this.TORIHIKISAKI_SEARCH_END.Tag = "検索画面を表示します";
            this.TORIHIKISAKI_SEARCH_END.UseVisualStyleBackColor = false;
            this.TORIHIKISAKI_SEARCH_END.ZeroPaddengFlag = false;
            // 
            // bt_func8
            // 
            this.bt_func8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func8.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_func8.Location = new System.Drawing.Point(713, 226);
            this.bt_func8.Name = "bt_func8";
            this.bt_func8.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func8.Size = new System.Drawing.Size(90, 35);
            this.bt_func8.TabIndex = 9;
            this.bt_func8.TabStop = false;
            this.bt_func8.Tag = "検索を実行します";
            this.bt_func8.Text = "[F8]\r\n検索実行";
            this.bt_func8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func8.UseVisualStyleBackColor = false;
            // 
            // bt_func12
            // 
            this.bt_func12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func12.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_func12.Location = new System.Drawing.Point(809, 226);
            this.bt_func12.Name = "bt_func12";
            this.bt_func12.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func12.Size = new System.Drawing.Size(90, 35);
            this.bt_func12.TabIndex = 10;
            this.bt_func12.TabStop = false;
            this.bt_func12.Tag = "画面を閉じます";
            this.bt_func12.Text = "[F12]\r\nキャンセル";
            this.bt_func12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func12.UseVisualStyleBackColor = false;
            // 
            // MOTOTYOU_KBN_LABEL
            // 
            this.MOTOTYOU_KBN_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.MOTOTYOU_KBN_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MOTOTYOU_KBN_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MOTOTYOU_KBN_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MOTOTYOU_KBN_LABEL.ForeColor = System.Drawing.Color.White;
            this.MOTOTYOU_KBN_LABEL.Location = new System.Drawing.Point(12, 55);
            this.MOTOTYOU_KBN_LABEL.Name = "MOTOTYOU_KBN_LABEL";
            this.MOTOTYOU_KBN_LABEL.Size = new System.Drawing.Size(110, 20);
            this.MOTOTYOU_KBN_LABEL.TabIndex = 697;
            this.MOTOTYOU_KBN_LABEL.Text = "元帳種類※";
            this.MOTOTYOU_KBN_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TYUUSYUTU_KBN__LABEL
            // 
            this.TYUUSYUTU_KBN__LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.TYUUSYUTU_KBN__LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TYUUSYUTU_KBN__LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TYUUSYUTU_KBN__LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TYUUSYUTU_KBN__LABEL.ForeColor = System.Drawing.Color.White;
            this.TYUUSYUTU_KBN__LABEL.Location = new System.Drawing.Point(12, 81);
            this.TYUUSYUTU_KBN__LABEL.Name = "TYUUSYUTU_KBN__LABEL";
            this.TYUUSYUTU_KBN__LABEL.Size = new System.Drawing.Size(110, 20);
            this.TYUUSYUTU_KBN__LABEL.TabIndex = 696;
            this.TYUUSYUTU_KBN__LABEL.Text = "抽出方法※";
            this.TYUUSYUTU_KBN__LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SHIMEBI_LABEL
            // 
            this.SHIMEBI_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.SHIMEBI_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHIMEBI_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SHIMEBI_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHIMEBI_LABEL.ForeColor = System.Drawing.Color.White;
            this.SHIMEBI_LABEL.Location = new System.Drawing.Point(12, 107);
            this.SHIMEBI_LABEL.Name = "SHIMEBI_LABEL";
            this.SHIMEBI_LABEL.Size = new System.Drawing.Size(110, 20);
            this.SHIMEBI_LABEL.TabIndex = 698;
            this.SHIMEBI_LABEL.Text = "締日※";
            this.SHIMEBI_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.TORIHIKI_KBN);
            this.panel2.Controls.Add(this.KAKE_MOTOTYOU);
            this.panel2.Controls.Add(this.GENKIN_MOTOTYOU);
            this.panel2.Location = new System.Drawing.Point(128, 55);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(274, 20);
            this.panel2.TabIndex = 0;
            this.panel2.TabStop = true;
            // 
            // TORIHIKI_KBN
            // 
            this.TORIHIKI_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.TORIHIKI_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKI_KBN.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TORIHIKI_KBN.DBFieldsName = "";
            this.TORIHIKI_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKI_KBN.DisplayItemName = "元帳区分";
            this.TORIHIKI_KBN.DisplayPopUp = null;
            this.TORIHIKI_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKI_KBN.FocusOutCheckMethod")));
            this.TORIHIKI_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TORIHIKI_KBN.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKI_KBN.IsInputErrorOccured = false;
            this.TORIHIKI_KBN.ItemDefinedTypes = "smallint";
            this.TORIHIKI_KBN.LinkedRadioButtonArray = new string[] {
        "KAKE_MOTOTYOU",
        "GENKIN_MOTOTYOU"};
            this.TORIHIKI_KBN.Location = new System.Drawing.Point(-1, -1);
            this.TORIHIKI_KBN.Name = "TORIHIKI_KBN";
            this.TORIHIKI_KBN.PopupAfterExecute = null;
            this.TORIHIKI_KBN.PopupBeforeExecute = null;
            this.TORIHIKI_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKI_KBN.PopupSearchSendParams")));
            this.TORIHIKI_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKI_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKI_KBN.popupWindowSetting")));
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
            this.TORIHIKI_KBN.RangeSetting = rangeSettingDto2;
            this.TORIHIKI_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKI_KBN.RegistCheckMethod")));
            this.TORIHIKI_KBN.ShortItemName = "元帳区分";
            this.TORIHIKI_KBN.Size = new System.Drawing.Size(20, 20);
            this.TORIHIKI_KBN.TabIndex = 0;
            this.TORIHIKI_KBN.Tag = "【1、2】のいずれかで入力してください";
            this.TORIHIKI_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TORIHIKI_KBN.WordWrap = false;
            this.TORIHIKI_KBN.TextChanged += new System.EventHandler(this.MOTOTYOU_KBN_TextChanged);
            // 
            // KAKE_MOTOTYOU
            // 
            this.KAKE_MOTOTYOU.AutoSize = true;
            this.KAKE_MOTOTYOU.DefaultBackColor = System.Drawing.Color.Empty;
            this.KAKE_MOTOTYOU.DisplayItemName = "する";
            this.KAKE_MOTOTYOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KAKE_MOTOTYOU.FocusOutCheckMethod")));
            this.KAKE_MOTOTYOU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KAKE_MOTOTYOU.LinkedTextBox = "TORIHIKI_KBN";
            this.KAKE_MOTOTYOU.Location = new System.Drawing.Point(29, 0);
            this.KAKE_MOTOTYOU.Name = "KAKE_MOTOTYOU";
            this.KAKE_MOTOTYOU.PopupAfterExecute = null;
            this.KAKE_MOTOTYOU.PopupBeforeExecute = null;
            this.KAKE_MOTOTYOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KAKE_MOTOTYOU.PopupSearchSendParams")));
            this.KAKE_MOTOTYOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KAKE_MOTOTYOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KAKE_MOTOTYOU.popupWindowSetting")));
            this.KAKE_MOTOTYOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KAKE_MOTOTYOU.RegistCheckMethod")));
            this.KAKE_MOTOTYOU.ShortItemName = "する";
            this.KAKE_MOTOTYOU.Size = new System.Drawing.Size(81, 17);
            this.KAKE_MOTOTYOU.TabIndex = 2;
            this.KAKE_MOTOTYOU.Tag = "元帳の種類を選択してください";
            this.KAKE_MOTOTYOU.Text = "1.掛元帳";
            this.KAKE_MOTOTYOU.UseVisualStyleBackColor = true;
            this.KAKE_MOTOTYOU.Value = "1";
            // 
            // GENKIN_MOTOTYOU
            // 
            this.GENKIN_MOTOTYOU.AutoSize = true;
            this.GENKIN_MOTOTYOU.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENKIN_MOTOTYOU.DisplayItemName = "しない";
            this.GENKIN_MOTOTYOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENKIN_MOTOTYOU.FocusOutCheckMethod")));
            this.GENKIN_MOTOTYOU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GENKIN_MOTOTYOU.LinkedTextBox = "TORIHIKI_KBN";
            this.GENKIN_MOTOTYOU.Location = new System.Drawing.Point(137, 0);
            this.GENKIN_MOTOTYOU.Name = "GENKIN_MOTOTYOU";
            this.GENKIN_MOTOTYOU.PopupAfterExecute = null;
            this.GENKIN_MOTOTYOU.PopupBeforeExecute = null;
            this.GENKIN_MOTOTYOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENKIN_MOTOTYOU.PopupSearchSendParams")));
            this.GENKIN_MOTOTYOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENKIN_MOTOTYOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENKIN_MOTOTYOU.popupWindowSetting")));
            this.GENKIN_MOTOTYOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENKIN_MOTOTYOU.RegistCheckMethod")));
            this.GENKIN_MOTOTYOU.ShortItemName = "しない";
            this.GENKIN_MOTOTYOU.Size = new System.Drawing.Size(95, 17);
            this.GENKIN_MOTOTYOU.TabIndex = 3;
            this.GENKIN_MOTOTYOU.Tag = "元帳の種類を選択してください";
            this.GENKIN_MOTOTYOU.Text = "2.現金元帳";
            this.GENKIN_MOTOTYOU.UseVisualStyleBackColor = true;
            this.GENKIN_MOTOTYOU.Value = "2";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.TYUUSYUTU_KBN);
            this.panel3.Controls.Add(this.DENPYOU_DATE_MATU);
            this.panel3.Controls.Add(this.URIAGE_DATE_SHIMEBI);
            this.panel3.Location = new System.Drawing.Point(128, 81);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(274, 20);
            this.panel3.TabIndex = 1;
            this.panel3.TabStop = true;
            // 
            // TYUUSYUTU_KBN
            // 
            this.TYUUSYUTU_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.TYUUSYUTU_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TYUUSYUTU_KBN.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TYUUSYUTU_KBN.DBFieldsName = "";
            this.TYUUSYUTU_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.TYUUSYUTU_KBN.DisplayItemName = "抽出区分";
            this.TYUUSYUTU_KBN.DisplayPopUp = null;
            this.TYUUSYUTU_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TYUUSYUTU_KBN.FocusOutCheckMethod")));
            this.TYUUSYUTU_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TYUUSYUTU_KBN.ForeColor = System.Drawing.Color.Black;
            this.TYUUSYUTU_KBN.IsInputErrorOccured = false;
            this.TYUUSYUTU_KBN.ItemDefinedTypes = "smallint";
            this.TYUUSYUTU_KBN.LinkedRadioButtonArray = new string[] {
        "URIAGE_DATE_SHIMEBI",
        "DENPYOU_DATE_MATU"};
            this.TYUUSYUTU_KBN.Location = new System.Drawing.Point(-1, -1);
            this.TYUUSYUTU_KBN.Name = "TYUUSYUTU_KBN";
            this.TYUUSYUTU_KBN.PopupAfterExecute = null;
            this.TYUUSYUTU_KBN.PopupBeforeExecute = null;
            this.TYUUSYUTU_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TYUUSYUTU_KBN.PopupSearchSendParams")));
            this.TYUUSYUTU_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TYUUSYUTU_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TYUUSYUTU_KBN.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto3.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.TYUUSYUTU_KBN.RangeSetting = rangeSettingDto3;
            this.TYUUSYUTU_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TYUUSYUTU_KBN.RegistCheckMethod")));
            this.TYUUSYUTU_KBN.ShortItemName = "抽出区分";
            this.TYUUSYUTU_KBN.Size = new System.Drawing.Size(20, 20);
            this.TYUUSYUTU_KBN.TabIndex = 1;
            this.TYUUSYUTU_KBN.Tag = "【1、2】のいずれかで入力してください";
            this.TYUUSYUTU_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TYUUSYUTU_KBN.WordWrap = false;
            this.TYUUSYUTU_KBN.TextChanged += new System.EventHandler(this.TYUUSYUTU_KBN_TextChanged);
            // 
            // DENPYOU_DATE_MATU
            // 
            this.DENPYOU_DATE_MATU.AutoSize = true;
            this.DENPYOU_DATE_MATU.DefaultBackColor = System.Drawing.Color.Empty;
            this.DENPYOU_DATE_MATU.DisplayItemName = "する";
            this.DENPYOU_DATE_MATU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_DATE_MATU.FocusOutCheckMethod")));
            this.DENPYOU_DATE_MATU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DENPYOU_DATE_MATU.LinkedTextBox = "TYUUSYUTU_KBN";
            this.DENPYOU_DATE_MATU.Location = new System.Drawing.Point(29, 0);
            this.DENPYOU_DATE_MATU.Name = "DENPYOU_DATE_MATU";
            this.DENPYOU_DATE_MATU.PopupAfterExecute = null;
            this.DENPYOU_DATE_MATU.PopupBeforeExecute = null;
            this.DENPYOU_DATE_MATU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DENPYOU_DATE_MATU.PopupSearchSendParams")));
            this.DENPYOU_DATE_MATU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DENPYOU_DATE_MATU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DENPYOU_DATE_MATU.popupWindowSetting")));
            this.DENPYOU_DATE_MATU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_DATE_MATU.RegistCheckMethod")));
            this.DENPYOU_DATE_MATU.ShortItemName = "する";
            this.DENPYOU_DATE_MATU.Size = new System.Drawing.Size(179, 17);
            this.DENPYOU_DATE_MATU.TabIndex = 2;
            this.DENPYOU_DATE_MATU.Tag = "抽出方法を選択してください";
            this.DENPYOU_DATE_MATU.Text = "1.伝票日付";
            this.DENPYOU_DATE_MATU.UseVisualStyleBackColor = true;
            this.DENPYOU_DATE_MATU.Value = "1";
            // 
            // URIAGE_DATE_SHIMEBI
            // 
            this.URIAGE_DATE_SHIMEBI.AutoSize = true;
            this.URIAGE_DATE_SHIMEBI.DefaultBackColor = System.Drawing.Color.Empty;
            this.URIAGE_DATE_SHIMEBI.DisplayItemName = "しない";
            this.URIAGE_DATE_SHIMEBI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("URIAGE_DATE_SHIMEBI.FocusOutCheckMethod")));
            this.URIAGE_DATE_SHIMEBI.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.URIAGE_DATE_SHIMEBI.LinkedTextBox = "TYUUSYUTU_KBN";
            this.URIAGE_DATE_SHIMEBI.Location = new System.Drawing.Point(137, 0);
            this.URIAGE_DATE_SHIMEBI.Name = "URIAGE_DATE_SHIMEBI";
            this.URIAGE_DATE_SHIMEBI.PopupAfterExecute = null;
            this.URIAGE_DATE_SHIMEBI.PopupBeforeExecute = null;
            this.URIAGE_DATE_SHIMEBI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("URIAGE_DATE_SHIMEBI.PopupSearchSendParams")));
            this.URIAGE_DATE_SHIMEBI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.URIAGE_DATE_SHIMEBI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("URIAGE_DATE_SHIMEBI.popupWindowSetting")));
            this.URIAGE_DATE_SHIMEBI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("URIAGE_DATE_SHIMEBI.RegistCheckMethod")));
            this.URIAGE_DATE_SHIMEBI.ShortItemName = "しない";
            this.URIAGE_DATE_SHIMEBI.Size = new System.Drawing.Size(137, 17);
            this.URIAGE_DATE_SHIMEBI.TabIndex = 3;
            this.URIAGE_DATE_SHIMEBI.Tag = "抽出方法を選択してください";
            this.URIAGE_DATE_SHIMEBI.Text = "2.売上日付";
            this.URIAGE_DATE_SHIMEBI.UseVisualStyleBackColor = true;
            this.URIAGE_DATE_SHIMEBI.Value = "2";
            // 
            // cb_shimebi
            // 
            this.cb_shimebi.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cb_shimebi.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cb_shimebi.BackColor = System.Drawing.SystemColors.Window;
            this.cb_shimebi.DefaultBackColor = System.Drawing.Color.Empty;
            this.cb_shimebi.DisplayItemName = "締日";
            this.cb_shimebi.DisplayPopUp = null;
            this.cb_shimebi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_shimebi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cb_shimebi.FocusOutCheckMethod")));
            this.cb_shimebi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
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
            this.cb_shimebi.Location = new System.Drawing.Point(128, 107);
            this.cb_shimebi.Name = "cb_shimebi";
            this.cb_shimebi.PopupAfterExecute = null;
            this.cb_shimebi.PopupBeforeExecute = null;
            this.cb_shimebi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cb_shimebi.PopupSearchSendParams")));
            this.cb_shimebi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cb_shimebi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cb_shimebi.popupWindowSetting")));
            this.cb_shimebi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cb_shimebi.RegistCheckMethod")));
            this.cb_shimebi.ShortItemName = "締日";
            this.cb_shimebi.Size = new System.Drawing.Size(40, 21);
            this.cb_shimebi.TabIndex = 2;
            this.cb_shimebi.Tag = "締日を入力してください";
            this.cb_shimebi.SelectedIndexChanged += new System.EventHandler(this.cb_shimebi_SelectedIndexChanged);
            // 
            // btn_jigetsu
            // 
            this.btn_jigetsu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btn_jigetsu.DefaultBackColor = System.Drawing.Color.Empty;
            this.btn_jigetsu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.btn_jigetsu.Location = new System.Drawing.Point(107, 226);
            this.btn_jigetsu.Name = "btn_jigetsu";
            this.btn_jigetsu.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btn_jigetsu.Size = new System.Drawing.Size(90, 35);
            this.btn_jigetsu.TabIndex = 8;
            this.btn_jigetsu.TabStop = false;
            this.btn_jigetsu.Tag = "日付範囲指定の開始／終了を次月に変更します";
            this.btn_jigetsu.Text = "[F2]\r\n次月";
            this.btn_jigetsu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_jigetsu.UseVisualStyleBackColor = false;
            // 
            // btn_zengetsu
            // 
            this.btn_zengetsu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btn_zengetsu.DefaultBackColor = System.Drawing.Color.Empty;
            this.btn_zengetsu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.btn_zengetsu.Location = new System.Drawing.Point(12, 226);
            this.btn_zengetsu.Name = "btn_zengetsu";
            this.btn_zengetsu.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btn_zengetsu.Size = new System.Drawing.Size(90, 35);
            this.btn_zengetsu.TabIndex = 7;
            this.btn_zengetsu.TabStop = false;
            this.btn_zengetsu.Tag = "日付範囲指定の開始／終了を前月に変更します";
            this.btn_zengetsu.Text = "[F1]\r\n前月";
            this.btn_zengetsu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_zengetsu.UseVisualStyleBackColor = false;
            // 
            // ISNOT_NEED_DELETE_FLG
            // 
            this.ISNOT_NEED_DELETE_FLG.BackColor = System.Drawing.SystemColors.Window;
            this.ISNOT_NEED_DELETE_FLG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ISNOT_NEED_DELETE_FLG.CharactersNumber = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.ISNOT_NEED_DELETE_FLG.DBFieldsName = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.ISNOT_NEED_DELETE_FLG.DisplayItemName = "";
            this.ISNOT_NEED_DELETE_FLG.DisplayPopUp = null;
            this.ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ISNOT_NEED_DELETE_FLG.ForeColor = System.Drawing.Color.Black;
            this.ISNOT_NEED_DELETE_FLG.GetCodeMasterField = "";
            this.ISNOT_NEED_DELETE_FLG.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ISNOT_NEED_DELETE_FLG.IsInputErrorOccured = false;
            this.ISNOT_NEED_DELETE_FLG.ItemDefinedTypes = "bit";
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(840, 130);
            this.ISNOT_NEED_DELETE_FLG.MaxLength = 2;
            this.ISNOT_NEED_DELETE_FLG.Name = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.PopupAfterExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupBeforeExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupGetMasterField = "";
            this.ISNOT_NEED_DELETE_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.PopupSearchSendParams")));
            this.ISNOT_NEED_DELETE_FLG.PopupSetFormField = "";
            this.ISNOT_NEED_DELETE_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ISNOT_NEED_DELETE_FLG.PopupWindowName = "";
            this.ISNOT_NEED_DELETE_FLG.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.popupWindowSetting")));
            this.ISNOT_NEED_DELETE_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.RegistCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.SetFormField = "";
            this.ISNOT_NEED_DELETE_FLG.ShortItemName = "";
            this.ISNOT_NEED_DELETE_FLG.Size = new System.Drawing.Size(59, 20);
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 699;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "True";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            this.ISNOT_NEED_DELETE_FLG.ZeroPaddengFlag = true;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(914, 279);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.btn_jigetsu);
            this.Controls.Add(this.btn_zengetsu);
            this.Controls.Add(this.cb_shimebi);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.MOTOTYOU_KBN_LABEL);
            this.Controls.Add(this.TYUUSYUTU_KBN__LABEL);
            this.Controls.Add(this.SHIMEBI_LABEL);
            this.Controls.Add(this.TITLE_LABEL);
            this.Controls.Add(this.DATE_HANI_LABEL);
            this.Controls.Add(this.DATE_HANI_START);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DATE_HANI_END);
            this.Controls.Add(this.TORIHIKISAKI_LABEL);
            this.Controls.Add(this.TORIHIKISAKI_CD_START);
            this.Controls.Add(this.TORIHIKISAKI_NAME_START);
            this.Controls.Add(this.TORIHIKISAKI_SEARCH_START);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TORIHIKISAKI_CD_END);
            this.Controls.Add(this.TORIHIKISAKI_NAME_END);
            this.Controls.Add(this.TORIHIKISAKI_SEARCH_END);
            this.Controls.Add(this.OUTPUT_KBN_LABEL);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bt_func12);
            this.Controls.Add(this.bt_func8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UIForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MotochoHaniJokenPopupForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

		public System.Windows.Forms.Label TITLE_LABEL;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		public System.Windows.Forms.Label OUTPUT_KBN_LABEL;
		public System.Windows.Forms.Label TORIHIKISAKI_LABEL;
		public System.Windows.Forms.Label DATE_HANI_LABEL;
		internal r_framework.CustomControl.CustomRadioButton OUTPUT_KBN_ALL;
		internal r_framework.CustomControl.CustomRadioButton OUTPUT_KBN_ONLY;
		internal System.Windows.Forms.Panel panel1;
		internal r_framework.CustomControl.CustomPopupOpenButton TORIHIKISAKI_SEARCH_START;
		internal r_framework.CustomControl.CustomPopupOpenButton TORIHIKISAKI_SEARCH_END;
		public r_framework.CustomControl.CustomButton bt_func8;
		public r_framework.CustomControl.CustomButton bt_func12;
		public r_framework.CustomControl.CustomDateTimePicker DATE_HANI_END;
		public r_framework.CustomControl.CustomDateTimePicker DATE_HANI_START;
		public r_framework.CustomControl.CustomNumericTextBox2 OUTPUT_KBN;
		public r_framework.CustomControl.CustomAlphaNumTextBox TORIHIKISAKI_CD_START;
		public r_framework.CustomControl.CustomAlphaNumTextBox TORIHIKISAKI_CD_END;
		public r_framework.CustomControl.CustomTextBox TORIHIKISAKI_NAME_END;
		public r_framework.CustomControl.CustomTextBox TORIHIKISAKI_NAME_START;
        public System.Windows.Forms.Label MOTOTYOU_KBN_LABEL;
        public System.Windows.Forms.Label TYUUSYUTU_KBN__LABEL;
        public System.Windows.Forms.Label SHIMEBI_LABEL;
        internal System.Windows.Forms.Panel panel2;
        public r_framework.CustomControl.CustomNumericTextBox2 TORIHIKI_KBN;
        internal r_framework.CustomControl.CustomRadioButton KAKE_MOTOTYOU;
        internal r_framework.CustomControl.CustomRadioButton GENKIN_MOTOTYOU;
        internal System.Windows.Forms.Panel panel3;
        public r_framework.CustomControl.CustomNumericTextBox2 TYUUSYUTU_KBN;
        internal r_framework.CustomControl.CustomRadioButton DENPYOU_DATE_MATU;
        internal r_framework.CustomControl.CustomRadioButton URIAGE_DATE_SHIMEBI;
        internal r_framework.CustomControl.CustomComboBox cb_shimebi;
        public r_framework.CustomControl.CustomButton btn_jigetsu;
        public r_framework.CustomControl.CustomButton btn_zengetsu;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
    }
}