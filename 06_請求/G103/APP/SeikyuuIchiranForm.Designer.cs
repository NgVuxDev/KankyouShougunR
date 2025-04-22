namespace Shougun.Core.Billing.Seikyuichiran
{
    partial class SeikyuuIchiranForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SeikyuuIchiranForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.DENPYOU_DATE_LABEL = new System.Windows.Forms.Label();
            this.HIDUKE_TO = new r_framework.CustomControl.CustomDateTimePicker();
            this.label38 = new System.Windows.Forms.Label();
            this.HIDUKE_FROM = new r_framework.CustomControl.CustomDateTimePicker();
            this.TORIHIKISAKI_LABEL = new System.Windows.Forms.Label();
            this.TORIHIKISAKI_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.SIMPLE_SEARCH_PANEL = new r_framework.CustomControl.CustomPanel();
            this.TORIHIKISAKI_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.TORIHIKISAKI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.NYUUKIN_YOTEI_DATE_FROM = new r_framework.CustomControl.CustomDateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.NYUUKIN_YOTEI_DATE_TO = new r_framework.CustomControl.CustomDateTimePicker();
            this.NYUUKIN_YOTEI_BI_HENKOU = new r_framework.CustomControl.CustomDateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.panel28 = new System.Windows.Forms.Panel();
            this.ZEI_KOMI_KBN_3 = new r_framework.CustomControl.CustomRadioButton();
            this.ZEI_KOMI_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.ZEI_KOMI_KBN_2 = new r_framework.CustomControl.CustomRadioButton();
            this.ZEI_KOMI_KBN_1 = new r_framework.CustomControl.CustomRadioButton();
            this.label61 = new System.Windows.Forms.Label();
            this.SIMPLE_SEARCH_PANEL.SuspendLayout();
            this.panel28.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.searchString.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.FocusOutCheckMethod")));
            this.searchString.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.searchString.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("searchString.PopupSearchSendParams")));
            this.searchString.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("searchString.popupWindowSetting")));
            this.searchString.ReadOnly = true;
            this.searchString.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.RegistCheckMethod")));
            this.searchString.Size = new System.Drawing.Size(500, 113);
            this.searchString.TabStop = false;
            this.searchString.Tag = "  ";
            this.searchString.Visible = false;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.bt_ptn1.Location = new System.Drawing.Point(4, 427);
            this.bt_ptn1.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn1.TabIndex = 4;
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.bt_ptn2.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn2.TabIndex = 5;
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.bt_ptn3.Location = new System.Drawing.Point(404, 427);
            this.bt_ptn3.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn3.TabIndex = 6;
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.bt_ptn4.Location = new System.Drawing.Point(604, 427);
            this.bt_ptn4.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn4.TabIndex = 7;
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.bt_ptn5.Location = new System.Drawing.Point(804, 427);
            this.bt_ptn5.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn5.TabIndex = 8;
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.Location = new System.Drawing.Point(4, 156);
            this.customSortHeader1.TabIndex = 1;
            // 
            // DENPYOU_DATE_LABEL
            // 
            this.DENPYOU_DATE_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.DENPYOU_DATE_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DENPYOU_DATE_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DENPYOU_DATE_LABEL.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DENPYOU_DATE_LABEL.ForeColor = System.Drawing.Color.White;
            this.DENPYOU_DATE_LABEL.Location = new System.Drawing.Point(0, 0);
            this.DENPYOU_DATE_LABEL.Name = "DENPYOU_DATE_LABEL";
            this.DENPYOU_DATE_LABEL.Size = new System.Drawing.Size(110, 20);
            this.DENPYOU_DATE_LABEL.TabIndex = 0;
            this.DENPYOU_DATE_LABEL.Text = "請求日付※";
            this.DENPYOU_DATE_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HIDUKE_TO
            // 
            this.HIDUKE_TO.BackColor = System.Drawing.SystemColors.Window;
            this.HIDUKE_TO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HIDUKE_TO.CalendarFont = new System.Drawing.Font("MS Gothic", 9F);
            this.HIDUKE_TO.Checked = false;
            this.HIDUKE_TO.CustomFormat = "yyyy/MM/dd(ddd)";
            this.HIDUKE_TO.DateTimeNowYear = "";
            this.HIDUKE_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.HIDUKE_TO.DisplayItemName = "終了日付";
            this.HIDUKE_TO.DisplayPopUp = null;
            this.HIDUKE_TO.FocusOutCheckMethod = null;
            this.HIDUKE_TO.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.HIDUKE_TO.ForeColor = System.Drawing.Color.Black;
            this.HIDUKE_TO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.HIDUKE_TO.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.HIDUKE_TO.IsInputErrorOccured = false;
            this.HIDUKE_TO.Location = new System.Drawing.Point(277, 0);
            this.HIDUKE_TO.MaxLength = 10;
            this.HIDUKE_TO.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.HIDUKE_TO.Name = "HIDUKE_TO";
            this.HIDUKE_TO.NullValue = "";
            this.HIDUKE_TO.PopupAfterExecute = null;
            this.HIDUKE_TO.PopupBeforeExecute = null;
            this.HIDUKE_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIDUKE_TO.PopupSearchSendParams")));
            this.HIDUKE_TO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIDUKE_TO.popupWindowSetting = null;
            this.HIDUKE_TO.RegistCheckMethod = null;
            this.HIDUKE_TO.Size = new System.Drawing.Size(138, 20);
            this.HIDUKE_TO.TabIndex = 3;
            this.HIDUKE_TO.Tag = "日付を選択してください";
            this.HIDUKE_TO.Text = "2013/10/29(火)";
            this.HIDUKE_TO.Value = new System.DateTime(2013, 10, 29, 0, 0, 0, 0);
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.Transparent;
            this.label38.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label38.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label38.ForeColor = System.Drawing.Color.Black;
            this.label38.Location = new System.Drawing.Point(255, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(20, 20);
            this.label38.TabIndex = 2;
            this.label38.Text = "～";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HIDUKE_FROM
            // 
            this.HIDUKE_FROM.BackColor = System.Drawing.SystemColors.Window;
            this.HIDUKE_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HIDUKE_FROM.CalendarFont = new System.Drawing.Font("MS Gothic", 9F);
            this.HIDUKE_FROM.Checked = false;
            this.HIDUKE_FROM.CustomFormat = "yyyy/MM/dd(ddd)";
            this.HIDUKE_FROM.DateTimeNowYear = "";
            this.HIDUKE_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.HIDUKE_FROM.DisplayItemName = "開始日付";
            this.HIDUKE_FROM.DisplayPopUp = null;
            this.HIDUKE_FROM.FocusOutCheckMethod = null;
            this.HIDUKE_FROM.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.HIDUKE_FROM.ForeColor = System.Drawing.Color.Black;
            this.HIDUKE_FROM.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.HIDUKE_FROM.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.HIDUKE_FROM.IsInputErrorOccured = false;
            this.HIDUKE_FROM.Location = new System.Drawing.Point(115, 0);
            this.HIDUKE_FROM.MaxLength = 10;
            this.HIDUKE_FROM.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.HIDUKE_FROM.Name = "HIDUKE_FROM";
            this.HIDUKE_FROM.NullValue = "";
            this.HIDUKE_FROM.PopupAfterExecute = null;
            this.HIDUKE_FROM.PopupBeforeExecute = null;
            this.HIDUKE_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIDUKE_FROM.PopupSearchSendParams")));
            this.HIDUKE_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIDUKE_FROM.popupWindowSetting = null;
            this.HIDUKE_FROM.RegistCheckMethod = null;
            this.HIDUKE_FROM.Size = new System.Drawing.Size(138, 20);
            this.HIDUKE_FROM.TabIndex = 1;
            this.HIDUKE_FROM.Tag = "日付を選択してください";
            this.HIDUKE_FROM.Text = "2013/10/29(火)";
            this.HIDUKE_FROM.Value = new System.DateTime(2013, 10, 29, 0, 0, 0, 0);
            // 
            // TORIHIKISAKI_LABEL
            // 
            this.TORIHIKISAKI_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.TORIHIKISAKI_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TORIHIKISAKI_LABEL.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TORIHIKISAKI_LABEL.ForeColor = System.Drawing.Color.White;
            this.TORIHIKISAKI_LABEL.Location = new System.Drawing.Point(0, 48);
            this.TORIHIKISAKI_LABEL.Name = "TORIHIKISAKI_LABEL";
            this.TORIHIKISAKI_LABEL.Size = new System.Drawing.Size(110, 20);
            this.TORIHIKISAKI_LABEL.TabIndex = 4;
            this.TORIHIKISAKI_LABEL.Text = "取引先";
            this.TORIHIKISAKI_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TORIHIKISAKI_NAME_RYAKU
            // 
            this.TORIHIKISAKI_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.TORIHIKISAKI_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_NAME_RYAKU.DBFieldsName = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_NAME_RYAKU.DisplayPopUp = null;
            this.TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod = null;
            this.TORIHIKISAKI_NAME_RYAKU.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.TORIHIKISAKI_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_NAME_RYAKU.IsInputErrorOccured = false;
            this.TORIHIKISAKI_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_NAME_RYAKU.Location = new System.Drawing.Point(164, 48);
            this.TORIHIKISAKI_NAME_RYAKU.MaxLength = 0;
            this.TORIHIKISAKI_NAME_RYAKU.Name = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_RYAKU.PopupAfterExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU.PopupBeforeExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams")));
            this.TORIHIKISAKI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_NAME_RYAKU.popupWindowSetting = null;
            this.TORIHIKISAKI_NAME_RYAKU.ReadOnly = true;
            this.TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod = null;
            this.TORIHIKISAKI_NAME_RYAKU.Size = new System.Drawing.Size(285, 20);
            this.TORIHIKISAKI_NAME_RYAKU.TabIndex = 6;
            this.TORIHIKISAKI_NAME_RYAKU.TabStop = false;
            // 
            // SIMPLE_SEARCH_PANEL
            // 
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.NYUUKIN_YOTEI_BI_HENKOU);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.label3);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.panel28);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.label61);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.label1);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.NYUUKIN_YOTEI_DATE_FROM);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.label2);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.NYUUKIN_YOTEI_DATE_TO);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.TORIHIKISAKI_SEARCH_BUTTON);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.DENPYOU_DATE_LABEL);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.TORIHIKISAKI_LABEL);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.HIDUKE_FROM);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.label38);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.HIDUKE_TO);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.TORIHIKISAKI_CD);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.TORIHIKISAKI_NAME_RYAKU);
            this.SIMPLE_SEARCH_PANEL.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.SIMPLE_SEARCH_PANEL.Location = new System.Drawing.Point(3, 12);
            this.SIMPLE_SEARCH_PANEL.Name = "SIMPLE_SEARCH_PANEL";
            this.SIMPLE_SEARCH_PANEL.Size = new System.Drawing.Size(1009, 84);
            this.SIMPLE_SEARCH_PANEL.TabIndex = 0;
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
            this.TORIHIKISAKI_SEARCH_BUTTON.LinkedTextBoxs = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.Location = new System.Drawing.Point(451, 47);
            this.TORIHIKISAKI_SEARCH_BUTTON.Name = "TORIHIKISAKI_SEARCH_BUTTON";
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupAfterExecute = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupGetMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_SEARCH_BUTTON.PopupSearchSendParams")));
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupSetFormField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupWindowName = "検索共通ポップアップ";
            this.TORIHIKISAKI_SEARCH_BUTTON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_SEARCH_BUTTON.popupWindowSetting")));
            this.TORIHIKISAKI_SEARCH_BUTTON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_SEARCH_BUTTON.RegistCheckMethod")));
            this.TORIHIKISAKI_SEARCH_BUTTON.SearchDisplayFlag = 0;
            this.TORIHIKISAKI_SEARCH_BUTTON.SetFormField = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.ShortItemName = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.Size = new System.Drawing.Size(22, 22);
            this.TORIHIKISAKI_SEARCH_BUTTON.TabIndex = 0;
            this.TORIHIKISAKI_SEARCH_BUTTON.TabStop = false;
            this.TORIHIKISAKI_SEARCH_BUTTON.UseVisualStyleBackColor = false;
            this.TORIHIKISAKI_SEARCH_BUTTON.ZeroPaddengFlag = false;
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
            this.TORIHIKISAKI_CD.GetCodeMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TORIHIKISAKI_CD.IsInputErrorOccured = false;
            this.TORIHIKISAKI_CD.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_CD.Location = new System.Drawing.Point(115, 48);
            this.TORIHIKISAKI_CD.MaxLength = 6;
            this.TORIHIKISAKI_CD.Name = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.PopupAfterExecute = null;
            this.TORIHIKISAKI_CD.PopupBeforeExecute = null;
            this.TORIHIKISAKI_CD.PopupGetMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_CD.PopupSearchSendParams")));
            this.TORIHIKISAKI_CD.PopupSetFormField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TORIHIKISAKI_CD.PopupWindowName = "検索共通ポップアップ";
            this.TORIHIKISAKI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_CD.popupWindowSetting")));
            this.TORIHIKISAKI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.RegistCheckMethod")));
            this.TORIHIKISAKI_CD.SetFormField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.Size = new System.Drawing.Size(50, 20);
            this.TORIHIKISAKI_CD.TabIndex = 15;
            this.TORIHIKISAKI_CD.Tag = "取引先を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.TORIHIKISAKI_CD.ZeroPaddengFlag = true;
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
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(479, 47);
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
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 675;
            this.ISNOT_NEED_DELETE_FLG.TabStop = false;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "入金予定日";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NYUUKIN_YOTEI_DATE_FROM
            // 
            this.NYUUKIN_YOTEI_DATE_FROM.BackColor = System.Drawing.SystemColors.Window;
            this.NYUUKIN_YOTEI_DATE_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NYUUKIN_YOTEI_DATE_FROM.CalendarFont = new System.Drawing.Font("MS Gothic", 9F);
            this.NYUUKIN_YOTEI_DATE_FROM.Checked = false;
            this.NYUUKIN_YOTEI_DATE_FROM.CustomFormat = "yyyy/MM/dd(ddd)";
            this.NYUUKIN_YOTEI_DATE_FROM.DateTimeNowYear = "";
            this.NYUUKIN_YOTEI_DATE_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.NYUUKIN_YOTEI_DATE_FROM.DisplayItemName = "入金予定日";
            this.NYUUKIN_YOTEI_DATE_FROM.DisplayPopUp = null;
            this.NYUUKIN_YOTEI_DATE_FROM.FocusOutCheckMethod = null;
            this.NYUUKIN_YOTEI_DATE_FROM.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.NYUUKIN_YOTEI_DATE_FROM.ForeColor = System.Drawing.Color.Black;
            this.NYUUKIN_YOTEI_DATE_FROM.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.NYUUKIN_YOTEI_DATE_FROM.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.NYUUKIN_YOTEI_DATE_FROM.IsInputErrorOccured = false;
            this.NYUUKIN_YOTEI_DATE_FROM.Location = new System.Drawing.Point(115, 24);
            this.NYUUKIN_YOTEI_DATE_FROM.MaxLength = 10;
            this.NYUUKIN_YOTEI_DATE_FROM.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.NYUUKIN_YOTEI_DATE_FROM.Name = "NYUUKIN_YOTEI_DATE_FROM";
            this.NYUUKIN_YOTEI_DATE_FROM.NullValue = "";
            this.NYUUKIN_YOTEI_DATE_FROM.PopupAfterExecute = null;
            this.NYUUKIN_YOTEI_DATE_FROM.PopupBeforeExecute = null;
            this.NYUUKIN_YOTEI_DATE_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NYUUKIN_YOTEI_DATE_FROM.PopupSearchSendParams")));
            this.NYUUKIN_YOTEI_DATE_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NYUUKIN_YOTEI_DATE_FROM.popupWindowSetting = null;
            this.NYUUKIN_YOTEI_DATE_FROM.RegistCheckMethod = null;
            this.NYUUKIN_YOTEI_DATE_FROM.Size = new System.Drawing.Size(138, 20);
            this.NYUUKIN_YOTEI_DATE_FROM.TabIndex = 12;
            this.NYUUKIN_YOTEI_DATE_FROM.Tag = "日付を選択してください";
            this.NYUUKIN_YOTEI_DATE_FROM.Text = "2013/10/29(火)";
            this.NYUUKIN_YOTEI_DATE_FROM.Value = new System.DateTime(2013, 10, 29, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(255, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "～";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NYUUKIN_YOTEI_DATE_TO
            // 
            this.NYUUKIN_YOTEI_DATE_TO.BackColor = System.Drawing.SystemColors.Window;
            this.NYUUKIN_YOTEI_DATE_TO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NYUUKIN_YOTEI_DATE_TO.CalendarFont = new System.Drawing.Font("MS Gothic", 9F);
            this.NYUUKIN_YOTEI_DATE_TO.Checked = false;
            this.NYUUKIN_YOTEI_DATE_TO.CustomFormat = "yyyy/MM/dd(ddd)";
            this.NYUUKIN_YOTEI_DATE_TO.DateTimeNowYear = "";
            this.NYUUKIN_YOTEI_DATE_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.NYUUKIN_YOTEI_DATE_TO.DisplayItemName = "入金予定日";
            this.NYUUKIN_YOTEI_DATE_TO.DisplayPopUp = null;
            this.NYUUKIN_YOTEI_DATE_TO.FocusOutCheckMethod = null;
            this.NYUUKIN_YOTEI_DATE_TO.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.NYUUKIN_YOTEI_DATE_TO.ForeColor = System.Drawing.Color.Black;
            this.NYUUKIN_YOTEI_DATE_TO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.NYUUKIN_YOTEI_DATE_TO.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.NYUUKIN_YOTEI_DATE_TO.IsInputErrorOccured = false;
            this.NYUUKIN_YOTEI_DATE_TO.Location = new System.Drawing.Point(277, 24);
            this.NYUUKIN_YOTEI_DATE_TO.MaxLength = 10;
            this.NYUUKIN_YOTEI_DATE_TO.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.NYUUKIN_YOTEI_DATE_TO.Name = "NYUUKIN_YOTEI_DATE_TO";
            this.NYUUKIN_YOTEI_DATE_TO.NullValue = "";
            this.NYUUKIN_YOTEI_DATE_TO.PopupAfterExecute = null;
            this.NYUUKIN_YOTEI_DATE_TO.PopupBeforeExecute = null;
            this.NYUUKIN_YOTEI_DATE_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NYUUKIN_YOTEI_DATE_TO.PopupSearchSendParams")));
            this.NYUUKIN_YOTEI_DATE_TO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NYUUKIN_YOTEI_DATE_TO.popupWindowSetting = null;
            this.NYUUKIN_YOTEI_DATE_TO.RegistCheckMethod = null;
            this.NYUUKIN_YOTEI_DATE_TO.Size = new System.Drawing.Size(138, 20);
            this.NYUUKIN_YOTEI_DATE_TO.TabIndex = 14;
            this.NYUUKIN_YOTEI_DATE_TO.Tag = "日付を選択してください";
            this.NYUUKIN_YOTEI_DATE_TO.Text = "2013/10/29(火)";
            this.NYUUKIN_YOTEI_DATE_TO.Value = new System.DateTime(2013, 10, 29, 0, 0, 0, 0);
            // 
            // NYUUKIN_YOTEI_BI_HENKOU
            // 
            this.NYUUKIN_YOTEI_BI_HENKOU.BackColor = System.Drawing.SystemColors.Window;
            this.NYUUKIN_YOTEI_BI_HENKOU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NYUUKIN_YOTEI_BI_HENKOU.CalendarFont = new System.Drawing.Font("MS Gothic", 9F);
            this.NYUUKIN_YOTEI_BI_HENKOU.Checked = false;
            this.NYUUKIN_YOTEI_BI_HENKOU.CustomFormat = "yyyy/MM/dd(ddd)";
            this.NYUUKIN_YOTEI_BI_HENKOU.DateTimeNowYear = "";
            this.NYUUKIN_YOTEI_BI_HENKOU.DefaultBackColor = System.Drawing.Color.Empty;
            this.NYUUKIN_YOTEI_BI_HENKOU.DisplayItemName = "変更前入金予定日";
            this.NYUUKIN_YOTEI_BI_HENKOU.DisplayPopUp = null;
            this.NYUUKIN_YOTEI_BI_HENKOU.FocusOutCheckMethod = null;
            this.NYUUKIN_YOTEI_BI_HENKOU.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.NYUUKIN_YOTEI_BI_HENKOU.ForeColor = System.Drawing.Color.Black;
            this.NYUUKIN_YOTEI_BI_HENKOU.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.NYUUKIN_YOTEI_BI_HENKOU.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.NYUUKIN_YOTEI_BI_HENKOU.IsInputErrorOccured = false;
            this.NYUUKIN_YOTEI_BI_HENKOU.Location = new System.Drawing.Point(727, 24);
            this.NYUUKIN_YOTEI_BI_HENKOU.MaxLength = 10;
            this.NYUUKIN_YOTEI_BI_HENKOU.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.NYUUKIN_YOTEI_BI_HENKOU.Name = "NYUUKIN_YOTEI_BI_HENKOU";
            this.NYUUKIN_YOTEI_BI_HENKOU.NullValue = "";
            this.NYUUKIN_YOTEI_BI_HENKOU.PopupAfterExecute = null;
            this.NYUUKIN_YOTEI_BI_HENKOU.PopupBeforeExecute = null;
            this.NYUUKIN_YOTEI_BI_HENKOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NYUUKIN_YOTEI_BI_HENKOU.PopupSearchSendParams")));
            this.NYUUKIN_YOTEI_BI_HENKOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NYUUKIN_YOTEI_BI_HENKOU.popupWindowSetting = null;
            this.NYUUKIN_YOTEI_BI_HENKOU.RegistCheckMethod = null;
            this.NYUUKIN_YOTEI_BI_HENKOU.Size = new System.Drawing.Size(115, 20);
            this.NYUUKIN_YOTEI_BI_HENKOU.TabIndex = 21;
            this.NYUUKIN_YOTEI_BI_HENKOU.Tag = "日付を選択してください";
            this.NYUUKIN_YOTEI_BI_HENKOU.Text = "2013/10/29(火)";
            this.NYUUKIN_YOTEI_BI_HENKOU.Value = new System.DateTime(2013, 10, 29, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(585, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 20);
            this.label3.TabIndex = 679;
            this.label3.Text = "変更後入金予定日";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel28
            // 
            this.panel28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel28.Controls.Add(this.ZEI_KOMI_KBN_3);
            this.panel28.Controls.Add(this.ZEI_KOMI_KBN);
            this.panel28.Controls.Add(this.ZEI_KOMI_KBN_2);
            this.panel28.Controls.Add(this.ZEI_KOMI_KBN_1);
            this.panel28.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.panel28.Location = new System.Drawing.Point(726, 0);
            this.panel28.Name = "panel28";
            this.panel28.Size = new System.Drawing.Size(282, 20);
            this.panel28.TabIndex = 20;
            // 
            // ZEI_KOMI_KBN_3
            // 
            this.ZEI_KOMI_KBN_3.AutoSize = true;
            this.ZEI_KOMI_KBN_3.DefaultBackColor = System.Drawing.Color.Empty;
            this.ZEI_KOMI_KBN_3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZEI_KOMI_KBN_3.FocusOutCheckMethod")));
            this.ZEI_KOMI_KBN_3.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ZEI_KOMI_KBN_3.LinkedTextBox = "ZEI_KOMI_KBN";
            this.ZEI_KOMI_KBN_3.Location = new System.Drawing.Point(200, -1);
            this.ZEI_KOMI_KBN_3.Name = "ZEI_KOMI_KBN_3";
            this.ZEI_KOMI_KBN_3.PopupAfterExecute = null;
            this.ZEI_KOMI_KBN_3.PopupBeforeExecute = null;
            this.ZEI_KOMI_KBN_3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZEI_KOMI_KBN_3.PopupSearchSendParams")));
            this.ZEI_KOMI_KBN_3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ZEI_KOMI_KBN_3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZEI_KOMI_KBN_3.popupWindowSetting")));
            this.ZEI_KOMI_KBN_3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZEI_KOMI_KBN_3.RegistCheckMethod")));
            this.ZEI_KOMI_KBN_3.Size = new System.Drawing.Size(67, 17);
            this.ZEI_KOMI_KBN_3.TabIndex = 505;
            this.ZEI_KOMI_KBN_3.Tag = "消込状況が「3.全て」の場合にはチェックを付けてください";
            this.ZEI_KOMI_KBN_3.Text = "3.全て";
            this.ZEI_KOMI_KBN_3.UseVisualStyleBackColor = true;
            this.ZEI_KOMI_KBN_3.Value = "3";
            // 
            // ZEI_KOMI_KBN
            // 
            this.ZEI_KOMI_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.ZEI_KOMI_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ZEI_KOMI_KBN.DBFieldsName = "ZEI_KOMI_KBN";
            this.ZEI_KOMI_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.ZEI_KOMI_KBN.DisplayItemName = "消込状況";
            this.ZEI_KOMI_KBN.DisplayPopUp = null;
            this.ZEI_KOMI_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZEI_KOMI_KBN.FocusOutCheckMethod")));
            this.ZEI_KOMI_KBN.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ZEI_KOMI_KBN.ForeColor = System.Drawing.Color.Black;
            this.ZEI_KOMI_KBN.IsInputErrorOccured = false;
            this.ZEI_KOMI_KBN.ItemDefinedTypes = "smallint";
            this.ZEI_KOMI_KBN.LinkedRadioButtonArray = new string[] {
        "ZEI_KOMI_KBN_1",
        "ZEI_KOMI_KBN_2",
        "ZEI_KOMI_KBN_3"};
            this.ZEI_KOMI_KBN.Location = new System.Drawing.Point(-1, -1);
            this.ZEI_KOMI_KBN.Name = "ZEI_KOMI_KBN";
            this.ZEI_KOMI_KBN.PopupAfterExecute = null;
            this.ZEI_KOMI_KBN.PopupBeforeExecute = null;
            this.ZEI_KOMI_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZEI_KOMI_KBN.PopupSearchSendParams")));
            this.ZEI_KOMI_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ZEI_KOMI_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZEI_KOMI_KBN.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            3,
            0,
            0,
            0});
            rangeSettingDto2.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ZEI_KOMI_KBN.RangeSetting = rangeSettingDto2;
            this.ZEI_KOMI_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZEI_KOMI_KBN.RegistCheckMethod")));
            this.ZEI_KOMI_KBN.ShortItemName = "消込状況";
            this.ZEI_KOMI_KBN.Size = new System.Drawing.Size(20, 20);
            this.ZEI_KOMI_KBN.TabIndex = 20;
            this.ZEI_KOMI_KBN.Tag = "【1～3】のいずれかで入力してください";
            this.ZEI_KOMI_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ZEI_KOMI_KBN.WordWrap = false;
            // 
            // ZEI_KOMI_KBN_2
            // 
            this.ZEI_KOMI_KBN_2.AutoSize = true;
            this.ZEI_KOMI_KBN_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.ZEI_KOMI_KBN_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZEI_KOMI_KBN_2.FocusOutCheckMethod")));
            this.ZEI_KOMI_KBN_2.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ZEI_KOMI_KBN_2.LinkedTextBox = "ZEI_KOMI_KBN";
            this.ZEI_KOMI_KBN_2.Location = new System.Drawing.Point(113, 0);
            this.ZEI_KOMI_KBN_2.Name = "ZEI_KOMI_KBN_2";
            this.ZEI_KOMI_KBN_2.PopupAfterExecute = null;
            this.ZEI_KOMI_KBN_2.PopupBeforeExecute = null;
            this.ZEI_KOMI_KBN_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZEI_KOMI_KBN_2.PopupSearchSendParams")));
            this.ZEI_KOMI_KBN_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ZEI_KOMI_KBN_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZEI_KOMI_KBN_2.popupWindowSetting")));
            this.ZEI_KOMI_KBN_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZEI_KOMI_KBN_2.RegistCheckMethod")));
            this.ZEI_KOMI_KBN_2.Size = new System.Drawing.Size(81, 17);
            this.ZEI_KOMI_KBN_2.TabIndex = 504;
            this.ZEI_KOMI_KBN_2.Tag = "消込状況が「2.消込済」の場合にはチェックを付けてください";
            this.ZEI_KOMI_KBN_2.Text = "2.消込済";
            this.ZEI_KOMI_KBN_2.UseVisualStyleBackColor = true;
            this.ZEI_KOMI_KBN_2.Value = "2";
            // 
            // ZEI_KOMI_KBN_1
            // 
            this.ZEI_KOMI_KBN_1.AutoSize = true;
            this.ZEI_KOMI_KBN_1.Checked = true;
            this.ZEI_KOMI_KBN_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.ZEI_KOMI_KBN_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZEI_KOMI_KBN_1.FocusOutCheckMethod")));
            this.ZEI_KOMI_KBN_1.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ZEI_KOMI_KBN_1.LinkedTextBox = "ZEI_KOMI_KBN";
            this.ZEI_KOMI_KBN_1.Location = new System.Drawing.Point(27, 0);
            this.ZEI_KOMI_KBN_1.Name = "ZEI_KOMI_KBN_1";
            this.ZEI_KOMI_KBN_1.PopupAfterExecute = null;
            this.ZEI_KOMI_KBN_1.PopupBeforeExecute = null;
            this.ZEI_KOMI_KBN_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZEI_KOMI_KBN_1.PopupSearchSendParams")));
            this.ZEI_KOMI_KBN_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ZEI_KOMI_KBN_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZEI_KOMI_KBN_1.popupWindowSetting")));
            this.ZEI_KOMI_KBN_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZEI_KOMI_KBN_1.RegistCheckMethod")));
            this.ZEI_KOMI_KBN_1.Size = new System.Drawing.Size(81, 17);
            this.ZEI_KOMI_KBN_1.TabIndex = 503;
            this.ZEI_KOMI_KBN_1.Tag = "消込状況が「1.未消込」の場合にはチェックを付けてください";
            this.ZEI_KOMI_KBN_1.Text = "1.未消込";
            this.ZEI_KOMI_KBN_1.UseVisualStyleBackColor = true;
            this.ZEI_KOMI_KBN_1.Value = "1";
            // 
            // label61
            // 
            this.label61.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label61.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label61.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label61.ForeColor = System.Drawing.Color.White;
            this.label61.Location = new System.Drawing.Point(585, 0);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(135, 20);
            this.label61.TabIndex = 678;
            this.label61.Text = "消込状況※";
            this.label61.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SeikyuuIchiranForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 458);
            this.Controls.Add(this.SIMPLE_SEARCH_PANEL);
            this.Name = "SeikyuuIchiranForm";
            this.Text = "SeikyuuIchiranForm";
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.SIMPLE_SEARCH_PANEL, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.SIMPLE_SEARCH_PANEL.ResumeLayout(false);
            this.SIMPLE_SEARCH_PANEL.PerformLayout();
            this.panel28.ResumeLayout(false);
            this.panel28.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label DENPYOU_DATE_LABEL;
        public r_framework.CustomControl.CustomDateTimePicker HIDUKE_TO;
        private System.Windows.Forms.Label label38;
        public r_framework.CustomControl.CustomDateTimePicker HIDUKE_FROM;
        private System.Windows.Forms.Label TORIHIKISAKI_LABEL;
        internal r_framework.CustomControl.CustomTextBox TORIHIKISAKI_NAME_RYAKU;
        internal r_framework.CustomControl.CustomPanel SIMPLE_SEARCH_PANEL;
        internal r_framework.CustomControl.CustomPopupOpenButton TORIHIKISAKI_SEARCH_BUTTON;
        internal r_framework.CustomControl.CustomAlphaNumTextBox TORIHIKISAKI_CD;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
        public r_framework.CustomControl.CustomDateTimePicker NYUUKIN_YOTEI_BI_HENKOU;
        internal System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel28;
        internal r_framework.CustomControl.CustomRadioButton ZEI_KOMI_KBN_3;
        internal r_framework.CustomControl.CustomNumericTextBox2 ZEI_KOMI_KBN;
        internal r_framework.CustomControl.CustomRadioButton ZEI_KOMI_KBN_2;
        internal r_framework.CustomControl.CustomRadioButton ZEI_KOMI_KBN_1;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label1;
        public r_framework.CustomControl.CustomDateTimePicker NYUUKIN_YOTEI_DATE_FROM;
        private System.Windows.Forms.Label label2;
        public r_framework.CustomControl.CustomDateTimePicker NYUUKIN_YOTEI_DATE_TO;
    }
}