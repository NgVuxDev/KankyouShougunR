namespace Shougun.Core.Adjustment.Shiharaiichiran
{
    partial class UIForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.SIMPLE_SEARCH_PANEL = new r_framework.CustomControl.CustomPanel();
            this.SHUKKIN_YOTEI_BI_HENKOU = new r_framework.CustomControl.CustomDateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.panel28 = new System.Windows.Forms.Panel();
            this.ZEI_KOMI_KBN_3 = new r_framework.CustomControl.CustomRadioButton();
            this.ZEI_KOMI_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.ZEI_KOMI_KBN_2 = new r_framework.CustomControl.CustomRadioButton();
            this.ZEI_KOMI_KBN_1 = new r_framework.CustomControl.CustomRadioButton();
            this.label61 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SHUKKIN_YOTEI_DATE_FROM = new r_framework.CustomControl.CustomDateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.SHUKKIN_YOTEI_DATE_TO = new r_framework.CustomControl.CustomDateTimePicker();
            this.TORIHIKISAKI_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.TORIHIKISAKI_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.TORIHIKISAKI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.dtpDateTo = new r_framework.CustomControl.CustomDateTimePicker();
            this.label38 = new System.Windows.Forms.Label();
            this.dtpDateFrom = new r_framework.CustomControl.CustomDateTimePicker();
            this.TORIHIKISAKI_LABEL = new System.Windows.Forms.Label();
            this.lblDenpyoDate = new System.Windows.Forms.Label();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
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
            this.searchString.Size = new System.Drawing.Size(531, 130);
            this.searchString.TabStop = false;
            this.searchString.Tag = "検索条件設定画面で設定した条件が表示されます";
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
            // SIMPLE_SEARCH_PANEL
            // 
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.SHUKKIN_YOTEI_BI_HENKOU);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.label3);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.panel28);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.label61);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.label1);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.SHUKKIN_YOTEI_DATE_FROM);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.label2);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.SHUKKIN_YOTEI_DATE_TO);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.TORIHIKISAKI_SEARCH_BUTTON);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.TORIHIKISAKI_NAME_RYAKU);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.TORIHIKISAKI_CD);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.dtpDateTo);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.label38);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.dtpDateFrom);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.TORIHIKISAKI_LABEL);
            this.SIMPLE_SEARCH_PANEL.Controls.Add(this.lblDenpyoDate);
            this.SIMPLE_SEARCH_PANEL.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.SIMPLE_SEARCH_PANEL.Location = new System.Drawing.Point(3, 12);
            this.SIMPLE_SEARCH_PANEL.Name = "SIMPLE_SEARCH_PANEL";
            this.SIMPLE_SEARCH_PANEL.Size = new System.Drawing.Size(964, 72);
            this.SIMPLE_SEARCH_PANEL.TabIndex = 0;
            // 
            // SHUKKIN_YOTEI_BI_HENKOU
            // 
            this.SHUKKIN_YOTEI_BI_HENKOU.BackColor = System.Drawing.SystemColors.Window;
            this.SHUKKIN_YOTEI_BI_HENKOU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHUKKIN_YOTEI_BI_HENKOU.CalendarFont = new System.Drawing.Font("MS Gothic", 9F);
            this.SHUKKIN_YOTEI_BI_HENKOU.Checked = false;
            this.SHUKKIN_YOTEI_BI_HENKOU.CustomFormat = "yyyy/MM/dd(ddd)";
            this.SHUKKIN_YOTEI_BI_HENKOU.DateTimeNowYear = "";
            this.SHUKKIN_YOTEI_BI_HENKOU.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUKKIN_YOTEI_BI_HENKOU.DisplayItemName = "変更前出金予定日";
            this.SHUKKIN_YOTEI_BI_HENKOU.DisplayPopUp = null;
            this.SHUKKIN_YOTEI_BI_HENKOU.FocusOutCheckMethod = null;
            this.SHUKKIN_YOTEI_BI_HENKOU.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.SHUKKIN_YOTEI_BI_HENKOU.ForeColor = System.Drawing.Color.Black;
            this.SHUKKIN_YOTEI_BI_HENKOU.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.SHUKKIN_YOTEI_BI_HENKOU.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SHUKKIN_YOTEI_BI_HENKOU.IsInputErrorOccured = false;
            this.SHUKKIN_YOTEI_BI_HENKOU.Location = new System.Drawing.Point(676, 24);
            this.SHUKKIN_YOTEI_BI_HENKOU.MaxLength = 10;
            this.SHUKKIN_YOTEI_BI_HENKOU.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.SHUKKIN_YOTEI_BI_HENKOU.Name = "SHUKKIN_YOTEI_BI_HENKOU";
            this.SHUKKIN_YOTEI_BI_HENKOU.NullValue = "";
            this.SHUKKIN_YOTEI_BI_HENKOU.PopupAfterExecute = null;
            this.SHUKKIN_YOTEI_BI_HENKOU.PopupBeforeExecute = null;
            this.SHUKKIN_YOTEI_BI_HENKOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUKKIN_YOTEI_BI_HENKOU.PopupSearchSendParams")));
            this.SHUKKIN_YOTEI_BI_HENKOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUKKIN_YOTEI_BI_HENKOU.popupWindowSetting = null;
            this.SHUKKIN_YOTEI_BI_HENKOU.RegistCheckMethod = null;
            this.SHUKKIN_YOTEI_BI_HENKOU.Size = new System.Drawing.Size(115, 20);
            this.SHUKKIN_YOTEI_BI_HENKOU.TabIndex = 20;
            this.SHUKKIN_YOTEI_BI_HENKOU.Tag = "日付を選択してください";
            this.SHUKKIN_YOTEI_BI_HENKOU.Text = "2013/10/29(火)";
            this.SHUKKIN_YOTEI_BI_HENKOU.Value = new System.DateTime(2013, 10, 29, 0, 0, 0, 0);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(534, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 20);
            this.label3.TabIndex = 631;
            this.label3.Text = "変更後出金予定日";
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
            this.panel28.Location = new System.Drawing.Point(675, 0);
            this.panel28.Name = "panel28";
            this.panel28.Size = new System.Drawing.Size(282, 20);
            this.panel28.TabIndex = 15;
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
            this.ZEI_KOMI_KBN.RangeSetting = rangeSettingDto1;
            this.ZEI_KOMI_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZEI_KOMI_KBN.RegistCheckMethod")));
            this.ZEI_KOMI_KBN.ShortItemName = "消込状況";
            this.ZEI_KOMI_KBN.Size = new System.Drawing.Size(20, 20);
            this.ZEI_KOMI_KBN.TabIndex = 15;
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
            this.label61.Location = new System.Drawing.Point(534, 0);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(135, 20);
            this.label61.TabIndex = 630;
            this.label61.Text = "消込状況※";
            this.label61.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.label1.TabIndex = 16;
            this.label1.Text = "出金予定日";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SHUKKIN_YOTEI_DATE_FROM
            // 
            this.SHUKKIN_YOTEI_DATE_FROM.BackColor = System.Drawing.SystemColors.Window;
            this.SHUKKIN_YOTEI_DATE_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHUKKIN_YOTEI_DATE_FROM.CalendarFont = new System.Drawing.Font("MS Gothic", 9F);
            this.SHUKKIN_YOTEI_DATE_FROM.Checked = false;
            this.SHUKKIN_YOTEI_DATE_FROM.CustomFormat = "yyyy/MM/dd(ddd)";
            this.SHUKKIN_YOTEI_DATE_FROM.DateTimeNowYear = "";
            this.SHUKKIN_YOTEI_DATE_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUKKIN_YOTEI_DATE_FROM.DisplayItemName = "出金予定日";
            this.SHUKKIN_YOTEI_DATE_FROM.DisplayPopUp = null;
            this.SHUKKIN_YOTEI_DATE_FROM.FocusOutCheckMethod = null;
            this.SHUKKIN_YOTEI_DATE_FROM.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.SHUKKIN_YOTEI_DATE_FROM.ForeColor = System.Drawing.Color.Black;
            this.SHUKKIN_YOTEI_DATE_FROM.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.SHUKKIN_YOTEI_DATE_FROM.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SHUKKIN_YOTEI_DATE_FROM.IsInputErrorOccured = false;
            this.SHUKKIN_YOTEI_DATE_FROM.Location = new System.Drawing.Point(115, 24);
            this.SHUKKIN_YOTEI_DATE_FROM.MaxLength = 10;
            this.SHUKKIN_YOTEI_DATE_FROM.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.SHUKKIN_YOTEI_DATE_FROM.Name = "SHUKKIN_YOTEI_DATE_FROM";
            this.SHUKKIN_YOTEI_DATE_FROM.NullValue = "";
            this.SHUKKIN_YOTEI_DATE_FROM.PopupAfterExecute = null;
            this.SHUKKIN_YOTEI_DATE_FROM.PopupBeforeExecute = null;
            this.SHUKKIN_YOTEI_DATE_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUKKIN_YOTEI_DATE_FROM.PopupSearchSendParams")));
            this.SHUKKIN_YOTEI_DATE_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUKKIN_YOTEI_DATE_FROM.popupWindowSetting = null;
            this.SHUKKIN_YOTEI_DATE_FROM.RegistCheckMethod = null;
            this.SHUKKIN_YOTEI_DATE_FROM.Size = new System.Drawing.Size(138, 20);
            this.SHUKKIN_YOTEI_DATE_FROM.TabIndex = 4;
            this.SHUKKIN_YOTEI_DATE_FROM.Tag = "日付を選択してください";
            this.SHUKKIN_YOTEI_DATE_FROM.Text = "2013/10/29(火)";
            this.SHUKKIN_YOTEI_DATE_FROM.Value = new System.DateTime(2013, 10, 29, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(255, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 20);
            this.label2.TabIndex = 17;
            this.label2.Text = "～";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SHUKKIN_YOTEI_DATE_TO
            // 
            this.SHUKKIN_YOTEI_DATE_TO.BackColor = System.Drawing.SystemColors.Window;
            this.SHUKKIN_YOTEI_DATE_TO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHUKKIN_YOTEI_DATE_TO.CalendarFont = new System.Drawing.Font("MS Gothic", 9F);
            this.SHUKKIN_YOTEI_DATE_TO.Checked = false;
            this.SHUKKIN_YOTEI_DATE_TO.CustomFormat = "yyyy/MM/dd(ddd)";
            this.SHUKKIN_YOTEI_DATE_TO.DateTimeNowYear = "";
            this.SHUKKIN_YOTEI_DATE_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUKKIN_YOTEI_DATE_TO.DisplayItemName = "出金予定日";
            this.SHUKKIN_YOTEI_DATE_TO.DisplayPopUp = null;
            this.SHUKKIN_YOTEI_DATE_TO.FocusOutCheckMethod = null;
            this.SHUKKIN_YOTEI_DATE_TO.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.SHUKKIN_YOTEI_DATE_TO.ForeColor = System.Drawing.Color.Black;
            this.SHUKKIN_YOTEI_DATE_TO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.SHUKKIN_YOTEI_DATE_TO.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SHUKKIN_YOTEI_DATE_TO.IsInputErrorOccured = false;
            this.SHUKKIN_YOTEI_DATE_TO.Location = new System.Drawing.Point(277, 24);
            this.SHUKKIN_YOTEI_DATE_TO.MaxLength = 10;
            this.SHUKKIN_YOTEI_DATE_TO.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.SHUKKIN_YOTEI_DATE_TO.Name = "SHUKKIN_YOTEI_DATE_TO";
            this.SHUKKIN_YOTEI_DATE_TO.NullValue = "";
            this.SHUKKIN_YOTEI_DATE_TO.PopupAfterExecute = null;
            this.SHUKKIN_YOTEI_DATE_TO.PopupBeforeExecute = null;
            this.SHUKKIN_YOTEI_DATE_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUKKIN_YOTEI_DATE_TO.PopupSearchSendParams")));
            this.SHUKKIN_YOTEI_DATE_TO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUKKIN_YOTEI_DATE_TO.popupWindowSetting = null;
            this.SHUKKIN_YOTEI_DATE_TO.RegistCheckMethod = null;
            this.SHUKKIN_YOTEI_DATE_TO.Size = new System.Drawing.Size(138, 20);
            this.SHUKKIN_YOTEI_DATE_TO.TabIndex = 5;
            this.SHUKKIN_YOTEI_DATE_TO.Tag = "日付を選択してください";
            this.SHUKKIN_YOTEI_DATE_TO.Text = "2013/10/29(火)";
            this.SHUKKIN_YOTEI_DATE_TO.Value = new System.DateTime(2013, 10, 29, 0, 0, 0, 0);
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
            this.TORIHIKISAKI_SEARCH_BUTTON.Location = new System.Drawing.Point(453, 47);
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
            // TORIHIKISAKI_NAME_RYAKU
            // 
            this.TORIHIKISAKI_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.TORIHIKISAKI_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_NAME_RYAKU.DBFieldsName = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_NAME_RYAKU.DisplayPopUp = null;
            this.TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod")));
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
            this.TORIHIKISAKI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.popupWindowSetting")));
            this.TORIHIKISAKI_NAME_RYAKU.ReadOnly = true;
            this.TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU.Size = new System.Drawing.Size(285, 20);
            this.TORIHIKISAKI_NAME_RYAKU.TabIndex = 6;
            this.TORIHIKISAKI_NAME_RYAKU.TabStop = false;
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
            this.TORIHIKISAKI_CD.TabIndex = 10;
            this.TORIHIKISAKI_CD.Tag = "取引先を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.TORIHIKISAKI_CD.ZeroPaddengFlag = true;
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.BackColor = System.Drawing.SystemColors.Window;
            this.dtpDateTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtpDateTo.CalendarFont = new System.Drawing.Font("MS Gothic", 9F);
            this.dtpDateTo.Checked = false;
            this.dtpDateTo.CustomFormat = "yyyy/MM/dd(ddd)";
            this.dtpDateTo.DateTimeNowYear = "";
            this.dtpDateTo.DefaultBackColor = System.Drawing.Color.Empty;
            this.dtpDateTo.DisplayItemName = "支払日付";
            this.dtpDateTo.DisplayPopUp = null;
            this.dtpDateTo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtpDateTo.FocusOutCheckMethod")));
            this.dtpDateTo.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.dtpDateTo.ForeColor = System.Drawing.Color.Black;
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateTo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtpDateTo.IsInputErrorOccured = false;
            this.dtpDateTo.Location = new System.Drawing.Point(277, 0);
            this.dtpDateTo.MaxLength = 10;
            this.dtpDateTo.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.NullValue = "";
            this.dtpDateTo.PopupAfterExecute = null;
            this.dtpDateTo.PopupBeforeExecute = null;
            this.dtpDateTo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dtpDateTo.PopupSearchSendParams")));
            this.dtpDateTo.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dtpDateTo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dtpDateTo.popupWindowSetting")));
            this.dtpDateTo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtpDateTo.RegistCheckMethod")));
            this.dtpDateTo.ShortItemName = "支払日付";
            this.dtpDateTo.Size = new System.Drawing.Size(138, 20);
            this.dtpDateTo.TabIndex = 3;
            this.dtpDateTo.Tag = "日付を選択してください";
            this.dtpDateTo.Text = "2013/10/31(木)";
            this.dtpDateTo.Value = new System.DateTime(2013, 10, 31, 0, 0, 0, 0);
            this.dtpDateTo.Leave += new System.EventHandler(this.dtpDateTo_Leave);
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
            // dtpDateFrom
            // 
            this.dtpDateFrom.BackColor = System.Drawing.SystemColors.Window;
            this.dtpDateFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtpDateFrom.CalendarFont = new System.Drawing.Font("MS Gothic", 9F);
            this.dtpDateFrom.Checked = false;
            this.dtpDateFrom.CustomFormat = "yyyy/MM/dd(ddd)";
            this.dtpDateFrom.DateTimeNowYear = "";
            this.dtpDateFrom.DefaultBackColor = System.Drawing.Color.Empty;
            this.dtpDateFrom.DisplayItemName = "支払日付";
            this.dtpDateFrom.DisplayPopUp = null;
            this.dtpDateFrom.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtpDateFrom.FocusOutCheckMethod")));
            this.dtpDateFrom.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.dtpDateFrom.ForeColor = System.Drawing.Color.Black;
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateFrom.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtpDateFrom.IsInputErrorOccured = false;
            this.dtpDateFrom.Location = new System.Drawing.Point(115, 0);
            this.dtpDateFrom.MaxLength = 10;
            this.dtpDateFrom.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.NullValue = "";
            this.dtpDateFrom.PopupAfterExecute = null;
            this.dtpDateFrom.PopupBeforeExecute = null;
            this.dtpDateFrom.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dtpDateFrom.PopupSearchSendParams")));
            this.dtpDateFrom.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dtpDateFrom.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dtpDateFrom.popupWindowSetting")));
            this.dtpDateFrom.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtpDateFrom.RegistCheckMethod")));
            this.dtpDateFrom.ShortItemName = "支払日付";
            this.dtpDateFrom.Size = new System.Drawing.Size(138, 20);
            this.dtpDateFrom.TabIndex = 1;
            this.dtpDateFrom.Tag = "日付を選択してください";
            this.dtpDateFrom.Text = "2013/10/31(木)";
            this.dtpDateFrom.Value = new System.DateTime(2013, 10, 31, 0, 0, 0, 0);
            this.dtpDateFrom.Leave += new System.EventHandler(this.dtpDateFrom_Leave);
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
            // lblDenpyoDate
            // 
            this.lblDenpyoDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblDenpyoDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDenpyoDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDenpyoDate.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblDenpyoDate.ForeColor = System.Drawing.Color.White;
            this.lblDenpyoDate.Location = new System.Drawing.Point(0, 0);
            this.lblDenpyoDate.Name = "lblDenpyoDate";
            this.lblDenpyoDate.Size = new System.Drawing.Size(110, 20);
            this.lblDenpyoDate.TabIndex = 0;
            this.lblDenpyoDate.Text = "支払日付※";
            this.lblDenpyoDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(473, 109);
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
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 676;
            this.ISNOT_NEED_DELETE_FLG.TabStop = false;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 458);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.SIMPLE_SEARCH_PANEL);
            this.Name = "UIForm";
            this.Text = "UIForm";
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.SIMPLE_SEARCH_PANEL, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.ISNOT_NEED_DELETE_FLG, 0);
            this.SIMPLE_SEARCH_PANEL.ResumeLayout(false);
            this.SIMPLE_SEARCH_PANEL.PerformLayout();
            this.panel28.ResumeLayout(false);
            this.panel28.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomPanel SIMPLE_SEARCH_PANEL;
        private System.Windows.Forms.Label lblDenpyoDate;
        public r_framework.CustomControl.CustomDateTimePicker dtpDateFrom;
        public r_framework.CustomControl.CustomDateTimePicker dtpDateTo;
        private System.Windows.Forms.Label label38;
        internal r_framework.CustomControl.CustomAlphaNumTextBox TORIHIKISAKI_CD;
        private System.Windows.Forms.Label TORIHIKISAKI_LABEL;
        internal r_framework.CustomControl.CustomTextBox TORIHIKISAKI_NAME_RYAKU;
        internal r_framework.CustomControl.CustomPopupOpenButton TORIHIKISAKI_SEARCH_BUTTON;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
        public r_framework.CustomControl.CustomDateTimePicker SHUKKIN_YOTEI_BI_HENKOU;
        internal System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel28;
        internal r_framework.CustomControl.CustomRadioButton ZEI_KOMI_KBN_3;
        internal r_framework.CustomControl.CustomNumericTextBox2 ZEI_KOMI_KBN;
        internal r_framework.CustomControl.CustomRadioButton ZEI_KOMI_KBN_2;
        internal r_framework.CustomControl.CustomRadioButton ZEI_KOMI_KBN_1;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.Label label1;
        public r_framework.CustomControl.CustomDateTimePicker SHUKKIN_YOTEI_DATE_FROM;
        private System.Windows.Forms.Label label2;
        public r_framework.CustomControl.CustomDateTimePicker SHUKKIN_YOTEI_DATE_TO;
    }
}