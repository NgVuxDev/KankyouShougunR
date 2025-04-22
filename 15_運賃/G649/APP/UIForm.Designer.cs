namespace Shougun.Core.Carriage.UnchinDaichouHaniJokenPopUp.APP
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
            this.TITLE_LABEL = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.UNPAN_GYOUSHA_NAME_END = new r_framework.CustomControl.CustomTextBox();
            this.UNPAN_GYOUSHA_NAME_FROM = new r_framework.CustomControl.CustomTextBox();
            this.TORIHIKISAKI_LABEL = new System.Windows.Forms.Label();
            this.DATE_HANI_LABEL = new System.Windows.Forms.Label();
            this.DATE_HANI_END = new r_framework.CustomControl.CustomDateTimePicker();
            this.DATE_HANI_START = new r_framework.CustomControl.CustomDateTimePicker();
            this.UNPAN_GYOUSHA_CD_FROM = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.UNPAN_GYOUSHA_CD_END = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.UNPAN_GYOUSHA_SEARCH_START = new r_framework.CustomControl.CustomPopupOpenButton();
            this.UNPAN_GYOUSHA_SEARCH_END = new r_framework.CustomControl.CustomPopupOpenButton();
            this.bt_func8 = new r_framework.CustomControl.CustomButton();
            this.bt_func12 = new r_framework.CustomControl.CustomButton();
            this.btn_jigetsu = new r_framework.CustomControl.CustomButton();
            this.btn_zengetsu = new r_framework.CustomControl.CustomButton();
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
            this.TITLE_LABEL.Text = "運賃台帳 - 範囲条件指定";
            this.TITLE_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.Location = new System.Drawing.Point(502, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 613;
            this.label4.Text = "～";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(273, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 612;
            this.label3.Text = "～";
            // 
            // UNPAN_GYOUSHA_NAME_END
            // 
            this.UNPAN_GYOUSHA_NAME_END.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.UNPAN_GYOUSHA_NAME_END.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UNPAN_GYOUSHA_NAME_END.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.UNPAN_GYOUSHA_NAME_END.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNPAN_GYOUSHA_NAME_END.DisplayPopUp = null;
            this.UNPAN_GYOUSHA_NAME_END.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_NAME_END.FocusOutCheckMethod")));
            this.UNPAN_GYOUSHA_NAME_END.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UNPAN_GYOUSHA_NAME_END.ForeColor = System.Drawing.Color.Black;
            this.UNPAN_GYOUSHA_NAME_END.IsInputErrorOccured = false;
            this.UNPAN_GYOUSHA_NAME_END.Location = new System.Drawing.Point(588, 81);
            this.UNPAN_GYOUSHA_NAME_END.MaxLength = 0;
            this.UNPAN_GYOUSHA_NAME_END.Name = "UNPAN_GYOUSHA_NAME_END";
            this.UNPAN_GYOUSHA_NAME_END.PopupAfterExecute = null;
            this.UNPAN_GYOUSHA_NAME_END.PopupBeforeExecute = null;
            this.UNPAN_GYOUSHA_NAME_END.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNPAN_GYOUSHA_NAME_END.PopupSearchSendParams")));
            this.UNPAN_GYOUSHA_NAME_END.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UNPAN_GYOUSHA_NAME_END.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNPAN_GYOUSHA_NAME_END.popupWindowSetting")));
            this.UNPAN_GYOUSHA_NAME_END.prevText = null;
            this.UNPAN_GYOUSHA_NAME_END.PrevText = null;
            this.UNPAN_GYOUSHA_NAME_END.ReadOnly = true;
            this.UNPAN_GYOUSHA_NAME_END.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_NAME_END.RegistCheckMethod")));
            this.UNPAN_GYOUSHA_NAME_END.Size = new System.Drawing.Size(286, 20);
            this.UNPAN_GYOUSHA_NAME_END.TabIndex = 609;
            this.UNPAN_GYOUSHA_NAME_END.TabStop = false;
            this.UNPAN_GYOUSHA_NAME_END.Tag = "";
            // 
            // UNPAN_GYOUSHA_NAME_FROM
            // 
            this.UNPAN_GYOUSHA_NAME_FROM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.UNPAN_GYOUSHA_NAME_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UNPAN_GYOUSHA_NAME_FROM.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.UNPAN_GYOUSHA_NAME_FROM.DBFieldsName = "UNPAN_GYOUSHA_NAME_FROM";
            this.UNPAN_GYOUSHA_NAME_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNPAN_GYOUSHA_NAME_FROM.DisplayPopUp = null;
            this.UNPAN_GYOUSHA_NAME_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_NAME_FROM.FocusOutCheckMethod")));
            this.UNPAN_GYOUSHA_NAME_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UNPAN_GYOUSHA_NAME_FROM.ForeColor = System.Drawing.Color.Black;
            this.UNPAN_GYOUSHA_NAME_FROM.IsInputErrorOccured = false;
            this.UNPAN_GYOUSHA_NAME_FROM.Location = new System.Drawing.Point(182, 81);
            this.UNPAN_GYOUSHA_NAME_FROM.MaxLength = 0;
            this.UNPAN_GYOUSHA_NAME_FROM.Name = "UNPAN_GYOUSHA_NAME_FROM";
            this.UNPAN_GYOUSHA_NAME_FROM.PopupAfterExecute = null;
            this.UNPAN_GYOUSHA_NAME_FROM.PopupBeforeExecute = null;
            this.UNPAN_GYOUSHA_NAME_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNPAN_GYOUSHA_NAME_FROM.PopupSearchSendParams")));
            this.UNPAN_GYOUSHA_NAME_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UNPAN_GYOUSHA_NAME_FROM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNPAN_GYOUSHA_NAME_FROM.popupWindowSetting")));
            this.UNPAN_GYOUSHA_NAME_FROM.prevText = null;
            this.UNPAN_GYOUSHA_NAME_FROM.PrevText = null;
            this.UNPAN_GYOUSHA_NAME_FROM.ReadOnly = true;
            this.UNPAN_GYOUSHA_NAME_FROM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_NAME_FROM.RegistCheckMethod")));
            this.UNPAN_GYOUSHA_NAME_FROM.Size = new System.Drawing.Size(286, 20);
            this.UNPAN_GYOUSHA_NAME_FROM.TabIndex = 606;
            this.UNPAN_GYOUSHA_NAME_FROM.TabStop = false;
            this.UNPAN_GYOUSHA_NAME_FROM.Tag = "";
            // 
            // TORIHIKISAKI_LABEL
            // 
            this.TORIHIKISAKI_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.TORIHIKISAKI_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TORIHIKISAKI_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TORIHIKISAKI_LABEL.ForeColor = System.Drawing.Color.White;
            this.TORIHIKISAKI_LABEL.Location = new System.Drawing.Point(12, 80);
            this.TORIHIKISAKI_LABEL.Name = "TORIHIKISAKI_LABEL";
            this.TORIHIKISAKI_LABEL.Size = new System.Drawing.Size(110, 20);
            this.TORIHIKISAKI_LABEL.TabIndex = 0;
            this.TORIHIKISAKI_LABEL.Text = "運搬業者";
            this.TORIHIKISAKI_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DATE_HANI_LABEL
            // 
            this.DATE_HANI_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.DATE_HANI_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DATE_HANI_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DATE_HANI_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DATE_HANI_LABEL.ForeColor = System.Drawing.Color.White;
            this.DATE_HANI_LABEL.Location = new System.Drawing.Point(12, 54);
            this.DATE_HANI_LABEL.Name = "DATE_HANI_LABEL";
            this.DATE_HANI_LABEL.Size = new System.Drawing.Size(110, 20);
            this.DATE_HANI_LABEL.TabIndex = 601;
            this.DATE_HANI_LABEL.Text = "伝票日付※";
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
            this.DATE_HANI_END.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_HANI_END.FocusOutCheckMethod")));
            this.DATE_HANI_END.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DATE_HANI_END.ForeColor = System.Drawing.Color.Black;
            this.DATE_HANI_END.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DATE_HANI_END.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.DATE_HANI_END.IsInputErrorOccured = false;
            this.DATE_HANI_END.Location = new System.Drawing.Point(300, 55);
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
            this.DATE_HANI_END.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DATE_HANI_END_MouseDoubleClick);
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
            this.DATE_HANI_START.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_HANI_START.FocusOutCheckMethod")));
            this.DATE_HANI_START.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DATE_HANI_START.ForeColor = System.Drawing.Color.Black;
            this.DATE_HANI_START.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DATE_HANI_START.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.DATE_HANI_START.IsInputErrorOccured = false;
            this.DATE_HANI_START.Location = new System.Drawing.Point(128, 55);
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
            // UNPAN_GYOUSHA_CD_FROM
            // 
            this.UNPAN_GYOUSHA_CD_FROM.BackColor = System.Drawing.SystemColors.Window;
            this.UNPAN_GYOUSHA_CD_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UNPAN_GYOUSHA_CD_FROM.ChangeUpperCase = true;
            this.UNPAN_GYOUSHA_CD_FROM.CharacterLimitList = null;
            this.UNPAN_GYOUSHA_CD_FROM.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.UNPAN_GYOUSHA_CD_FROM.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.UNPAN_GYOUSHA_CD_FROM.DBFieldsName = "";
            this.UNPAN_GYOUSHA_CD_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNPAN_GYOUSHA_CD_FROM.DisplayItemName = "開始運搬業者CD";
            this.UNPAN_GYOUSHA_CD_FROM.DisplayPopUp = null;
            this.UNPAN_GYOUSHA_CD_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_CD_FROM.FocusOutCheckMethod")));
            this.UNPAN_GYOUSHA_CD_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UNPAN_GYOUSHA_CD_FROM.ForeColor = System.Drawing.Color.Black;
            this.UNPAN_GYOUSHA_CD_FROM.GetCodeMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.UNPAN_GYOUSHA_CD_FROM.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.UNPAN_GYOUSHA_CD_FROM.IsInputErrorOccured = false;
            this.UNPAN_GYOUSHA_CD_FROM.ItemDefinedTypes = "";
            this.UNPAN_GYOUSHA_CD_FROM.Location = new System.Drawing.Point(128, 81);
            this.UNPAN_GYOUSHA_CD_FROM.MaxLength = 6;
            this.UNPAN_GYOUSHA_CD_FROM.Name = "UNPAN_GYOUSHA_CD_FROM";
            this.UNPAN_GYOUSHA_CD_FROM.PopupAfterExecute = null;
            this.UNPAN_GYOUSHA_CD_FROM.PopupBeforeExecute = null;
            this.UNPAN_GYOUSHA_CD_FROM.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.UNPAN_GYOUSHA_CD_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNPAN_GYOUSHA_CD_FROM.PopupSearchSendParams")));
            this.UNPAN_GYOUSHA_CD_FROM.PopupSendParams = new string[0];
            this.UNPAN_GYOUSHA_CD_FROM.PopupSetFormField = "UNPAN_GYOUSHA_CD_FROM, UNPAN_GYOUSHA_NAME_FROM";
            this.UNPAN_GYOUSHA_CD_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.UNPAN_GYOUSHA_CD_FROM.PopupWindowName = "検索共通ポップアップ";
            this.UNPAN_GYOUSHA_CD_FROM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNPAN_GYOUSHA_CD_FROM.popupWindowSetting")));
            this.UNPAN_GYOUSHA_CD_FROM.prevText = null;
            this.UNPAN_GYOUSHA_CD_FROM.PrevText = null;
            this.UNPAN_GYOUSHA_CD_FROM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_CD_FROM.RegistCheckMethod")));
            this.UNPAN_GYOUSHA_CD_FROM.SetFormField = "UNPAN_GYOUSHA_CD_FROM, UNPAN_GYOUSHA_NAME_FROM";
            this.UNPAN_GYOUSHA_CD_FROM.ShortItemName = "開始運搬業者";
            this.UNPAN_GYOUSHA_CD_FROM.Size = new System.Drawing.Size(55, 20);
            this.UNPAN_GYOUSHA_CD_FROM.TabIndex = 5;
            this.UNPAN_GYOUSHA_CD_FROM.Tag = "開始運搬業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.UNPAN_GYOUSHA_CD_FROM.ZeroPaddengFlag = true;
            this.UNPAN_GYOUSHA_CD_FROM.Validated += new System.EventHandler(this.UNPAN_GYOUSHA_CD_FROM_Validated);
            // 
            // UNPAN_GYOUSHA_CD_END
            // 
            this.UNPAN_GYOUSHA_CD_END.BackColor = System.Drawing.SystemColors.Window;
            this.UNPAN_GYOUSHA_CD_END.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UNPAN_GYOUSHA_CD_END.ChangeUpperCase = true;
            this.UNPAN_GYOUSHA_CD_END.CharacterLimitList = null;
            this.UNPAN_GYOUSHA_CD_END.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.UNPAN_GYOUSHA_CD_END.DBFieldsName = "";
            this.UNPAN_GYOUSHA_CD_END.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNPAN_GYOUSHA_CD_END.DisplayItemName = "終了運搬業者CD";
            this.UNPAN_GYOUSHA_CD_END.DisplayPopUp = null;
            this.UNPAN_GYOUSHA_CD_END.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_CD_END.FocusOutCheckMethod")));
            this.UNPAN_GYOUSHA_CD_END.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UNPAN_GYOUSHA_CD_END.ForeColor = System.Drawing.Color.Black;
            this.UNPAN_GYOUSHA_CD_END.GetCodeMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.UNPAN_GYOUSHA_CD_END.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.UNPAN_GYOUSHA_CD_END.IsInputErrorOccured = false;
            this.UNPAN_GYOUSHA_CD_END.ItemDefinedTypes = "";
            this.UNPAN_GYOUSHA_CD_END.Location = new System.Drawing.Point(534, 81);
            this.UNPAN_GYOUSHA_CD_END.MaxLength = 6;
            this.UNPAN_GYOUSHA_CD_END.Name = "UNPAN_GYOUSHA_CD_END";
            this.UNPAN_GYOUSHA_CD_END.PopupAfterExecute = null;
            this.UNPAN_GYOUSHA_CD_END.PopupBeforeExecute = null;
            this.UNPAN_GYOUSHA_CD_END.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.UNPAN_GYOUSHA_CD_END.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNPAN_GYOUSHA_CD_END.PopupSearchSendParams")));
            this.UNPAN_GYOUSHA_CD_END.PopupSetFormField = "UNPAN_GYOUSHA_CD_END, UNPAN_GYOUSHA_NAME_END";
            this.UNPAN_GYOUSHA_CD_END.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.UNPAN_GYOUSHA_CD_END.PopupWindowName = "検索共通ポップアップ";
            this.UNPAN_GYOUSHA_CD_END.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNPAN_GYOUSHA_CD_END.popupWindowSetting")));
            this.UNPAN_GYOUSHA_CD_END.prevText = null;
            this.UNPAN_GYOUSHA_CD_END.PrevText = null;
            this.UNPAN_GYOUSHA_CD_END.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_CD_END.RegistCheckMethod")));
            this.UNPAN_GYOUSHA_CD_END.SetFormField = "UNPAN_GYOUSHA_CD_END, UNPAN_GYOUSHA_NAME_END";
            this.UNPAN_GYOUSHA_CD_END.ShortItemName = "終了運搬業者";
            this.UNPAN_GYOUSHA_CD_END.Size = new System.Drawing.Size(55, 20);
            this.UNPAN_GYOUSHA_CD_END.TabIndex = 6;
            this.UNPAN_GYOUSHA_CD_END.Tag = "終了運搬業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.UNPAN_GYOUSHA_CD_END.ZeroPaddengFlag = true;
            this.UNPAN_GYOUSHA_CD_END.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TORIHIKISAKI_CD_END_MouseDoubleClick);
            this.UNPAN_GYOUSHA_CD_END.Validated += new System.EventHandler(this.UNPAN_GYOUSHA_CD_END_Validated);
            // 
            // UNPAN_GYOUSHA_SEARCH_START
            // 
            this.UNPAN_GYOUSHA_SEARCH_START.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.UNPAN_GYOUSHA_SEARCH_START.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.UNPAN_GYOUSHA_SEARCH_START.DBFieldsName = null;
            this.UNPAN_GYOUSHA_SEARCH_START.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNPAN_GYOUSHA_SEARCH_START.DisplayItemName = "運搬業者検索";
            this.UNPAN_GYOUSHA_SEARCH_START.DisplayPopUp = null;
            this.UNPAN_GYOUSHA_SEARCH_START.ErrorMessage = null;
            this.UNPAN_GYOUSHA_SEARCH_START.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_SEARCH_START.FocusOutCheckMethod")));
            this.UNPAN_GYOUSHA_SEARCH_START.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UNPAN_GYOUSHA_SEARCH_START.GetCodeMasterField = null;
            this.UNPAN_GYOUSHA_SEARCH_START.Image = ((System.Drawing.Image)(resources.GetObject("UNPAN_GYOUSHA_SEARCH_START.Image")));
            this.UNPAN_GYOUSHA_SEARCH_START.ItemDefinedTypes = null;
            this.UNPAN_GYOUSHA_SEARCH_START.LinkedTextBoxs = null;
            this.UNPAN_GYOUSHA_SEARCH_START.Location = new System.Drawing.Point(470, 80);
            this.UNPAN_GYOUSHA_SEARCH_START.Name = "UNPAN_GYOUSHA_SEARCH_START";
            this.UNPAN_GYOUSHA_SEARCH_START.PopupAfterExecute = null;
            this.UNPAN_GYOUSHA_SEARCH_START.PopupAfterExecuteMethod = "FromCDPopupAfterUpdate";
            this.UNPAN_GYOUSHA_SEARCH_START.PopupBeforeExecute = null;
            this.UNPAN_GYOUSHA_SEARCH_START.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.UNPAN_GYOUSHA_SEARCH_START.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNPAN_GYOUSHA_SEARCH_START.PopupSearchSendParams")));
            this.UNPAN_GYOUSHA_SEARCH_START.PopupSetFormField = "UNPAN_GYOUSHA_CD_FROM, UNPAN_GYOUSHA_NAME_FROM";
            this.UNPAN_GYOUSHA_SEARCH_START.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.UNPAN_GYOUSHA_SEARCH_START.PopupWindowName = "検索共通ポップアップ";
            this.UNPAN_GYOUSHA_SEARCH_START.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNPAN_GYOUSHA_SEARCH_START.popupWindowSetting")));
            this.UNPAN_GYOUSHA_SEARCH_START.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_SEARCH_START.RegistCheckMethod")));
            this.UNPAN_GYOUSHA_SEARCH_START.SearchDisplayFlag = 0;
            this.UNPAN_GYOUSHA_SEARCH_START.SetFormField = "UNPAN_GYOUSHA_CD_FROM, UNPAN_GYOUSHA_NAME_FROM";
            this.UNPAN_GYOUSHA_SEARCH_START.ShortItemName = "運搬業者検索";
            this.UNPAN_GYOUSHA_SEARCH_START.Size = new System.Drawing.Size(22, 22);
            this.UNPAN_GYOUSHA_SEARCH_START.TabIndex = 694;
            this.UNPAN_GYOUSHA_SEARCH_START.TabStop = false;
            this.UNPAN_GYOUSHA_SEARCH_START.Tag = "検索画面を表示します";
            this.UNPAN_GYOUSHA_SEARCH_START.UseVisualStyleBackColor = false;
            this.UNPAN_GYOUSHA_SEARCH_START.ZeroPaddengFlag = false;
            // 
            // UNPAN_GYOUSHA_SEARCH_END
            // 
            this.UNPAN_GYOUSHA_SEARCH_END.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.UNPAN_GYOUSHA_SEARCH_END.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.UNPAN_GYOUSHA_SEARCH_END.DBFieldsName = null;
            this.UNPAN_GYOUSHA_SEARCH_END.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNPAN_GYOUSHA_SEARCH_END.DisplayItemName = "取引先検索";
            this.UNPAN_GYOUSHA_SEARCH_END.DisplayPopUp = null;
            this.UNPAN_GYOUSHA_SEARCH_END.ErrorMessage = null;
            this.UNPAN_GYOUSHA_SEARCH_END.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_SEARCH_END.FocusOutCheckMethod")));
            this.UNPAN_GYOUSHA_SEARCH_END.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UNPAN_GYOUSHA_SEARCH_END.GetCodeMasterField = null;
            this.UNPAN_GYOUSHA_SEARCH_END.Image = ((System.Drawing.Image)(resources.GetObject("UNPAN_GYOUSHA_SEARCH_END.Image")));
            this.UNPAN_GYOUSHA_SEARCH_END.ItemDefinedTypes = null;
            this.UNPAN_GYOUSHA_SEARCH_END.LinkedTextBoxs = null;
            this.UNPAN_GYOUSHA_SEARCH_END.Location = new System.Drawing.Point(877, 80);
            this.UNPAN_GYOUSHA_SEARCH_END.Name = "UNPAN_GYOUSHA_SEARCH_END";
            this.UNPAN_GYOUSHA_SEARCH_END.PopupAfterExecute = null;
            this.UNPAN_GYOUSHA_SEARCH_END.PopupAfterExecuteMethod = "ToCDPopupAfterUpdate";
            this.UNPAN_GYOUSHA_SEARCH_END.PopupBeforeExecute = null;
            this.UNPAN_GYOUSHA_SEARCH_END.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.UNPAN_GYOUSHA_SEARCH_END.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNPAN_GYOUSHA_SEARCH_END.PopupSearchSendParams")));
            this.UNPAN_GYOUSHA_SEARCH_END.PopupSetFormField = "UNPAN_GYOUSHA_CD_END, UNPAN_GYOUSHA_NAME_END";
            this.UNPAN_GYOUSHA_SEARCH_END.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.UNPAN_GYOUSHA_SEARCH_END.PopupWindowName = "検索共通ポップアップ";
            this.UNPAN_GYOUSHA_SEARCH_END.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNPAN_GYOUSHA_SEARCH_END.popupWindowSetting")));
            this.UNPAN_GYOUSHA_SEARCH_END.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_SEARCH_END.RegistCheckMethod")));
            this.UNPAN_GYOUSHA_SEARCH_END.SearchDisplayFlag = 0;
            this.UNPAN_GYOUSHA_SEARCH_END.SetFormField = "UNPAN_GYOUSHA_CD_END, UNPAN_GYOUSHA_NAME_END";
            this.UNPAN_GYOUSHA_SEARCH_END.ShortItemName = "取引先検索";
            this.UNPAN_GYOUSHA_SEARCH_END.Size = new System.Drawing.Size(22, 22);
            this.UNPAN_GYOUSHA_SEARCH_END.TabIndex = 695;
            this.UNPAN_GYOUSHA_SEARCH_END.TabStop = false;
            this.UNPAN_GYOUSHA_SEARCH_END.Tag = "検索画面を表示します";
            this.UNPAN_GYOUSHA_SEARCH_END.UseVisualStyleBackColor = false;
            this.UNPAN_GYOUSHA_SEARCH_END.ZeroPaddengFlag = false;
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
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(914, 279);
            this.Controls.Add(this.btn_jigetsu);
            this.Controls.Add(this.btn_zengetsu);
            this.Controls.Add(this.TITLE_LABEL);
            this.Controls.Add(this.DATE_HANI_LABEL);
            this.Controls.Add(this.DATE_HANI_START);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DATE_HANI_END);
            this.Controls.Add(this.TORIHIKISAKI_LABEL);
            this.Controls.Add(this.UNPAN_GYOUSHA_CD_FROM);
            this.Controls.Add(this.UNPAN_GYOUSHA_SEARCH_START);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.UNPAN_GYOUSHA_CD_END);
            this.Controls.Add(this.UNPAN_GYOUSHA_SEARCH_END);
            this.Controls.Add(this.bt_func12);
            this.Controls.Add(this.bt_func8);
            this.Controls.Add(this.UNPAN_GYOUSHA_NAME_FROM);
            this.Controls.Add(this.UNPAN_GYOUSHA_NAME_END);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UIForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UnchinDaichouHaniJokenPopUp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

		public System.Windows.Forms.Label TITLE_LABEL;
		private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
		public System.Windows.Forms.Label TORIHIKISAKI_LABEL;
        public System.Windows.Forms.Label DATE_HANI_LABEL;
		internal r_framework.CustomControl.CustomPopupOpenButton UNPAN_GYOUSHA_SEARCH_START;
		internal r_framework.CustomControl.CustomPopupOpenButton UNPAN_GYOUSHA_SEARCH_END;
		public r_framework.CustomControl.CustomButton bt_func8;
		public r_framework.CustomControl.CustomButton bt_func12;
		public r_framework.CustomControl.CustomDateTimePicker DATE_HANI_END;
        public r_framework.CustomControl.CustomDateTimePicker DATE_HANI_START;
		public r_framework.CustomControl.CustomAlphaNumTextBox UNPAN_GYOUSHA_CD_FROM;
		public r_framework.CustomControl.CustomAlphaNumTextBox UNPAN_GYOUSHA_CD_END;
		public r_framework.CustomControl.CustomTextBox UNPAN_GYOUSHA_NAME_END;
        public r_framework.CustomControl.CustomTextBox UNPAN_GYOUSHA_NAME_FROM;
        public r_framework.CustomControl.CustomButton btn_jigetsu;
        public r_framework.CustomControl.CustomButton btn_zengetsu;
    }
}