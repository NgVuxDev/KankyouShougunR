namespace Shougun.Core.ElectronicManifest.UnpanShuryouHoukoku
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
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            this.Date_Label = new System.Windows.Forms.Label();
            this.DATE_FROM = new r_framework.CustomControl.CustomDateTimePicker();
            this.DATE_TO = new r_framework.CustomControl.CustomDateTimePicker();
            this.SyoriKubun_Label = new System.Windows.Forms.Label();
            this.SyoriKubun_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.panel2 = new r_framework.CustomControl.CustomPanel();
            this.SyoriKubun_Radio3 = new r_framework.CustomControl.CustomRadioButton();
            this.SyoriKubun_Radio2 = new r_framework.CustomControl.CustomRadioButton();
            this.SyoriKubun_Radio1 = new r_framework.CustomControl.CustomRadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.ManifestNo_Label = new System.Windows.Forms.Label();
            this.ManifestNoFrom = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.ManifestNoTo = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Syuruyi_Btn = new r_framework.CustomControl.CustomPopupOpenButton();
            this.HAIKI_KBN_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.HAIKI_SHURUI_NAME = new r_framework.CustomControl.CustomTextBox();
            this.HAIKI_LABEL = new System.Windows.Forms.Label();
            this.Jigyousha_Btn = new r_framework.CustomControl.CustomPopupOpenButton();
            this.Jigyousya_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.JIGYOUSHA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.Jigyousya_Label = new System.Windows.Forms.Label();
            this.Unpan_Btn = new r_framework.CustomControl.CustomPopupOpenButton();
            this.Unpan_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.Unpan_Name = new r_framework.CustomControl.CustomTextBox();
            this.Unpan_Label = new System.Windows.Forms.Label();
            this.Unpansha_Btn = new r_framework.CustomControl.CustomPopupOpenButton();
            this.Unpansha_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.Unpansha_Name = new r_framework.CustomControl.CustomTextBox();
            this.Unpansha_Label = new System.Windows.Forms.Label();
            this.Unpanba_Btn = new r_framework.CustomControl.CustomPopupOpenButton();
            this.Unpanba_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.Unpanba_Name = new r_framework.CustomControl.CustomTextBox();
            this.Unpanba_Label = new System.Windows.Forms.Label();
            this.Jigyoujou_Btn = new r_framework.CustomControl.CustomPopupOpenButton();
            this.Jigyoujou_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.JIGYOUBA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.Jigyouba_Label = new System.Windows.Forms.Label();
            this.IchiranDgv1 = new ShougunUI.APP.DgvCustom1();
            this.customPanel3 = new r_framework.CustomControl.CustomPanel();
            this.crdbtn_TashaEDI3 = new r_framework.CustomControl.CustomRadioButton();
            this.crdbtn_TashaEDI2 = new r_framework.CustomControl.CustomRadioButton();
            this.crdbtn_TashaEDI1 = new r_framework.CustomControl.CustomRadioButton();
            this.cntb_TashaEDI_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.lbl_TashaEDI = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IchiranDgv1)).BeginInit();
            this.customPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.searchString.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchString.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.FocusOutCheckMethod")));
            this.searchString.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.searchString.Location = new System.Drawing.Point(3, 371);
            this.searchString.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("searchString.PopupSearchSendParams")));
            this.searchString.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("searchString.popupWindowSetting")));
            this.searchString.ReadOnly = true;
            this.searchString.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.RegistCheckMethod")));
            this.searchString.Size = new System.Drawing.Size(1000, 72);
            this.searchString.TabStop = false;
            this.searchString.Tag = "検索条件設定画面で設定した条件が表示されます";
            this.searchString.Visible = false;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn1.Location = new System.Drawing.Point(3, 469);
            this.bt_ptn1.Size = new System.Drawing.Size(198, 22);
            this.bt_ptn1.TabIndex = 101;
            this.bt_ptn1.Text = "パターン1";
            this.bt_ptn1.Click += new System.EventHandler(this.bt_ptn1_Click);
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn2.Location = new System.Drawing.Point(203, 469);
            this.bt_ptn2.Size = new System.Drawing.Size(198, 22);
            this.bt_ptn2.TabIndex = 102;
            this.bt_ptn2.Text = "パターン2";
            this.bt_ptn2.Click += new System.EventHandler(this.bt_ptn2_Click);
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn3.Location = new System.Drawing.Point(403, 469);
            this.bt_ptn3.Size = new System.Drawing.Size(198, 22);
            this.bt_ptn3.TabIndex = 103;
            this.bt_ptn3.Text = "パターン3";
            this.bt_ptn3.Click += new System.EventHandler(this.bt_ptn3_Click);
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn4.Location = new System.Drawing.Point(603, 469);
            this.bt_ptn4.Size = new System.Drawing.Size(198, 22);
            this.bt_ptn4.TabIndex = 104;
            this.bt_ptn4.Text = "パターン4";
            this.bt_ptn4.Click += new System.EventHandler(this.bt_ptn4_Click);
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn5.Location = new System.Drawing.Point(803, 469);
            this.bt_ptn5.Size = new System.Drawing.Size(198, 22);
            this.bt_ptn5.TabIndex = 105;
            this.bt_ptn5.Text = "パターン5";
            this.bt_ptn5.Click += new System.EventHandler(this.bt_ptn5_Click);
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.LinkedDataGridViewName = "IchiranDgv1";
            this.customSortHeader1.Location = new System.Drawing.Point(3, 116);
            this.customSortHeader1.Size = new System.Drawing.Size(996, 24);
            this.customSortHeader1.TabIndex = 3;
            // 
            // Date_Label
            // 
            this.Date_Label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.Date_Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Date_Label.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Date_Label.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Date_Label.ForeColor = System.Drawing.Color.White;
            this.Date_Label.Location = new System.Drawing.Point(3, 45);
            this.Date_Label.Name = "Date_Label";
            this.Date_Label.Size = new System.Drawing.Size(126, 20);
            this.Date_Label.TabIndex = 547;
            this.Date_Label.Text = "引渡し日";
            this.Date_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DATE_FROM
            // 
            this.DATE_FROM.BackColor = System.Drawing.SystemColors.Window;
            this.DATE_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DATE_FROM.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.DATE_FROM.Checked = false;
            this.DATE_FROM.CustomFormat = "yyyy/MM/dd(ddd)";
            this.DATE_FROM.DateTimeNowYear = "";
            this.DATE_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_FROM.DisplayItemName = "交付年月日FROM";
            this.DATE_FROM.DisplayPopUp = null;
            this.DATE_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_FROM.FocusOutCheckMethod")));
            this.DATE_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.DATE_FROM.ForeColor = System.Drawing.Color.Black;
            this.DATE_FROM.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DATE_FROM.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.DATE_FROM.IsInputErrorOccured = false;
            this.DATE_FROM.Location = new System.Drawing.Point(134, 45);
            this.DATE_FROM.MaxLength = 10;
            this.DATE_FROM.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.DATE_FROM.Name = "DATE_FROM";
            this.DATE_FROM.NullValue = "";
            this.DATE_FROM.PopupAfterExecute = null;
            this.DATE_FROM.PopupBeforeExecute = null;
            this.DATE_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DATE_FROM.PopupSearchSendParams")));
            this.DATE_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DATE_FROM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DATE_FROM.popupWindowSetting")));
            this.DATE_FROM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_FROM.RegistCheckMethod")));
            this.DATE_FROM.Size = new System.Drawing.Size(138, 20);
            this.DATE_FROM.TabIndex = 2;
            this.DATE_FROM.Tag = "日付を選択してください";
            this.DATE_FROM.Text = "2013/12/10(火)";
            this.DATE_FROM.Value = new System.DateTime(2013, 12, 10, 0, 0, 0, 0);
            this.DATE_FROM.Leave += new System.EventHandler(this.DATE_FROM_Leave);
            // 
            // DATE_TO
            // 
            this.DATE_TO.BackColor = System.Drawing.SystemColors.Window;
            this.DATE_TO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DATE_TO.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.DATE_TO.Checked = false;
            this.DATE_TO.CustomFormat = "yyyy/MM/dd(ddd)";
            this.DATE_TO.DateTimeNowYear = "";
            this.DATE_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_TO.DisplayItemName = "交付年月日TO";
            this.DATE_TO.DisplayPopUp = null;
            this.DATE_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_TO.FocusOutCheckMethod")));
            this.DATE_TO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.DATE_TO.ForeColor = System.Drawing.Color.Black;
            this.DATE_TO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DATE_TO.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.DATE_TO.IsInputErrorOccured = false;
            this.DATE_TO.Location = new System.Drawing.Point(294, 45);
            this.DATE_TO.MaxLength = 10;
            this.DATE_TO.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.DATE_TO.Name = "DATE_TO";
            this.DATE_TO.NullValue = "";
            this.DATE_TO.PopupAfterExecute = null;
            this.DATE_TO.PopupBeforeExecute = null;
            this.DATE_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DATE_TO.PopupSearchSendParams")));
            this.DATE_TO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DATE_TO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DATE_TO.popupWindowSetting")));
            this.DATE_TO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_TO.RegistCheckMethod")));
            this.DATE_TO.Size = new System.Drawing.Size(138, 20);
            this.DATE_TO.TabIndex = 3;
            this.DATE_TO.Tag = "日付を選択してください";
            this.DATE_TO.Text = "2013/12/10(火)";
            this.DATE_TO.Value = new System.DateTime(2013, 12, 10, 0, 0, 0, 0);
            this.DATE_TO.Leave += new System.EventHandler(this.DATE_TO_Leave);
            // 
            // SyoriKubun_Label
            // 
            this.SyoriKubun_Label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.SyoriKubun_Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SyoriKubun_Label.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SyoriKubun_Label.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SyoriKubun_Label.ForeColor = System.Drawing.Color.White;
            this.SyoriKubun_Label.Location = new System.Drawing.Point(3, 22);
            this.SyoriKubun_Label.Name = "SyoriKubun_Label";
            this.SyoriKubun_Label.Size = new System.Drawing.Size(126, 20);
            this.SyoriKubun_Label.TabIndex = 552;
            this.SyoriKubun_Label.Text = "処理区分";
            this.SyoriKubun_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SyoriKubun_CD
            // 
            this.SyoriKubun_CD.BackColor = System.Drawing.SystemColors.Window;
            this.SyoriKubun_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SyoriKubun_CD.CharactersNumber = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SyoriKubun_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.SyoriKubun_CD.DisplayPopUp = null;
            this.SyoriKubun_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SyoriKubun_CD.FocusOutCheckMethod")));
            this.SyoriKubun_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SyoriKubun_CD.ForeColor = System.Drawing.Color.Black;
            this.SyoriKubun_CD.IsInputErrorOccured = false;
            this.SyoriKubun_CD.LinkedRadioButtonArray = new string[0];
            this.SyoriKubun_CD.Location = new System.Drawing.Point(134, 22);
            this.SyoriKubun_CD.MaxLength = 1;
            this.SyoriKubun_CD.Name = "SyoriKubun_CD";
            this.SyoriKubun_CD.PopupAfterExecute = null;
            this.SyoriKubun_CD.PopupBeforeExecute = null;
            this.SyoriKubun_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SyoriKubun_CD.PopupSearchSendParams")));
            this.SyoriKubun_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SyoriKubun_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SyoriKubun_CD.popupWindowSetting")));
            this.SyoriKubun_CD.prevText = null;
            this.SyoriKubun_CD.PrevText = "1";
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
            this.SyoriKubun_CD.RangeSetting = rangeSettingDto1;
            this.SyoriKubun_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SyoriKubun_CD.RegistCheckMethod")));
            this.SyoriKubun_CD.Size = new System.Drawing.Size(20, 20);
            this.SyoriKubun_CD.TabIndex = 1;
            this.SyoriKubun_CD.Tag = "処理区分を選択してください";
            this.SyoriKubun_CD.Text = "1";
            this.SyoriKubun_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.SyoriKubun_CD.TextChanged += new System.EventHandler(this.SyoriKubun_CD_TextChanged);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.SyoriKubun_Radio3);
            this.panel2.Controls.Add(this.SyoriKubun_Radio2);
            this.panel2.Controls.Add(this.SyoriKubun_Radio1);
            this.panel2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.panel2.Location = new System.Drawing.Point(153, 22);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(439, 20);
            this.panel2.TabIndex = 1;
            // 
            // SyoriKubun_Radio3
            // 
            this.SyoriKubun_Radio3.AutoSize = true;
            this.SyoriKubun_Radio3.DefaultBackColor = System.Drawing.Color.Empty;
            this.SyoriKubun_Radio3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SyoriKubun_Radio3.FocusOutCheckMethod")));
            this.SyoriKubun_Radio3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SyoriKubun_Radio3.LinkedTextBox = "SyoriKubun_CD";
            this.SyoriKubun_Radio3.Location = new System.Drawing.Point(259, 1);
            this.SyoriKubun_Radio3.Name = "SyoriKubun_Radio3";
            this.SyoriKubun_Radio3.PopupAfterExecute = null;
            this.SyoriKubun_Radio3.PopupBeforeExecute = null;
            this.SyoriKubun_Radio3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SyoriKubun_Radio3.PopupSearchSendParams")));
            this.SyoriKubun_Radio3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SyoriKubun_Radio3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SyoriKubun_Radio3.popupWindowSetting")));
            this.SyoriKubun_Radio3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SyoriKubun_Radio3.RegistCheckMethod")));
            this.SyoriKubun_Radio3.Size = new System.Drawing.Size(179, 17);
            this.SyoriKubun_Radio3.TabIndex = 3;
            this.SyoriKubun_Radio3.Tag = "処理区分を選択してください";
            this.SyoriKubun_Radio3.Text = "3.終了報告の修正・取消";
            this.SyoriKubun_Radio3.UseVisualStyleBackColor = true;
            this.SyoriKubun_Radio3.Value = "3";
            this.SyoriKubun_Radio3.CheckedChanged += new System.EventHandler(this.SyoriKubun_Radio3_CheckedChanged);
            // 
            // SyoriKubun_Radio2
            // 
            this.SyoriKubun_Radio2.AutoSize = true;
            this.SyoriKubun_Radio2.DefaultBackColor = System.Drawing.Color.Empty;
            this.SyoriKubun_Radio2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SyoriKubun_Radio2.FocusOutCheckMethod")));
            this.SyoriKubun_Radio2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SyoriKubun_Radio2.LinkedTextBox = "SyoriKubun_CD";
            this.SyoriKubun_Radio2.Location = new System.Drawing.Point(95, 1);
            this.SyoriKubun_Radio2.Name = "SyoriKubun_Radio2";
            this.SyoriKubun_Radio2.PopupAfterExecute = null;
            this.SyoriKubun_Radio2.PopupBeforeExecute = null;
            this.SyoriKubun_Radio2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SyoriKubun_Radio2.PopupSearchSendParams")));
            this.SyoriKubun_Radio2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SyoriKubun_Radio2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SyoriKubun_Radio2.popupWindowSetting")));
            this.SyoriKubun_Radio2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SyoriKubun_Radio2.RegistCheckMethod")));
            this.SyoriKubun_Radio2.Size = new System.Drawing.Size(165, 17);
            this.SyoriKubun_Radio2.TabIndex = 2;
            this.SyoriKubun_Radio2.Tag = "処理区分を選択してください";
            this.SyoriKubun_Radio2.Text = "2.終了報告の送信保留";
            this.SyoriKubun_Radio2.UseVisualStyleBackColor = true;
            this.SyoriKubun_Radio2.Value = "2";
            this.SyoriKubun_Radio2.CheckedChanged += new System.EventHandler(this.SyoriKubun_Radio2_CheckedChanged);
            // 
            // SyoriKubun_Radio1
            // 
            this.SyoriKubun_Radio1.AutoSize = true;
            this.SyoriKubun_Radio1.Checked = true;
            this.SyoriKubun_Radio1.DefaultBackColor = System.Drawing.Color.Empty;
            this.SyoriKubun_Radio1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SyoriKubun_Radio1.FocusOutCheckMethod")));
            this.SyoriKubun_Radio1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SyoriKubun_Radio1.LinkedTextBox = "SyoriKubun_CD";
            this.SyoriKubun_Radio1.Location = new System.Drawing.Point(2, 1);
            this.SyoriKubun_Radio1.Name = "SyoriKubun_Radio1";
            this.SyoriKubun_Radio1.PopupAfterExecute = null;
            this.SyoriKubun_Radio1.PopupBeforeExecute = null;
            this.SyoriKubun_Radio1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SyoriKubun_Radio1.PopupSearchSendParams")));
            this.SyoriKubun_Radio1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SyoriKubun_Radio1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SyoriKubun_Radio1.popupWindowSetting")));
            this.SyoriKubun_Radio1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SyoriKubun_Radio1.RegistCheckMethod")));
            this.SyoriKubun_Radio1.Size = new System.Drawing.Size(95, 17);
            this.SyoriKubun_Radio1.TabIndex = 1;
            this.SyoriKubun_Radio1.Tag = "処理区分を選択してください";
            this.SyoriKubun_Radio1.Text = "1.終了報告";
            this.SyoriKubun_Radio1.UseVisualStyleBackColor = true;
            this.SyoriKubun_Radio1.Value = "1";
            this.SyoriKubun_Radio1.CheckedChanged += new System.EventHandler(this.SyoriKubun_Radio1_CheckedChanged);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(274, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 20);
            this.label3.TabIndex = 553;
            this.label3.Text = "～";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ManifestNo_Label
            // 
            this.ManifestNo_Label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.ManifestNo_Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ManifestNo_Label.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ManifestNo_Label.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ManifestNo_Label.ForeColor = System.Drawing.Color.White;
            this.ManifestNo_Label.Location = new System.Drawing.Point(3, 67);
            this.ManifestNo_Label.Name = "ManifestNo_Label";
            this.ManifestNo_Label.Size = new System.Drawing.Size(126, 20);
            this.ManifestNo_Label.TabIndex = 554;
            this.ManifestNo_Label.Text = "マニフェスト番号";
            this.ManifestNo_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ManifestNoFrom
            // 
            this.ManifestNoFrom.AlphabetLimitFlag = false;
            this.ManifestNoFrom.BackColor = System.Drawing.SystemColors.Window;
            this.ManifestNoFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ManifestNoFrom.CharactersNumber = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.ManifestNoFrom.DBFieldsName = "";
            this.ManifestNoFrom.DefaultBackColor = System.Drawing.Color.Empty;
            this.ManifestNoFrom.DisplayPopUp = null;
            this.ManifestNoFrom.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ManifestNoFrom.FocusOutCheckMethod")));
            this.ManifestNoFrom.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ManifestNoFrom.ForeColor = System.Drawing.Color.Black;
            this.ManifestNoFrom.GetCodeMasterField = "";
            this.ManifestNoFrom.IsInputErrorOccured = false;
            this.ManifestNoFrom.ItemDefinedTypes = "varchar";
            this.ManifestNoFrom.Location = new System.Drawing.Point(134, 67);
            this.ManifestNoFrom.MaxLength = 11;
            this.ManifestNoFrom.Name = "ManifestNoFrom";
            this.ManifestNoFrom.PopupAfterExecute = null;
            this.ManifestNoFrom.PopupBeforeExecute = null;
            this.ManifestNoFrom.PopupGetMasterField = "";
            this.ManifestNoFrom.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ManifestNoFrom.PopupSearchSendParams")));
            this.ManifestNoFrom.PopupSetFormField = "";
            this.ManifestNoFrom.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ManifestNoFrom.PopupWindowName = "";
            this.ManifestNoFrom.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ManifestNoFrom.popupWindowSetting")));
            this.ManifestNoFrom.prevText = null;
            this.ManifestNoFrom.PrevText = "";
            rangeSettingDto2.Max = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            rangeSettingDto2.Min = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ManifestNoFrom.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ManifestNoFrom.RegistCheckMethod")));
            this.ManifestNoFrom.SetFormField = "";
            this.ManifestNoFrom.Size = new System.Drawing.Size(138, 20);
            this.ManifestNoFrom.TabIndex = 4;
            this.ManifestNoFrom.Tag = "半角11桁以内で入力してください";
            // 
            // ManifestNoTo
            // 
            this.ManifestNoTo.AlphabetLimitFlag = false;
            this.ManifestNoTo.BackColor = System.Drawing.SystemColors.Window;
            this.ManifestNoTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ManifestNoTo.CharactersNumber = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.ManifestNoTo.DBFieldsName = "";
            this.ManifestNoTo.DefaultBackColor = System.Drawing.Color.Empty;
            this.ManifestNoTo.DisplayPopUp = null;
            this.ManifestNoTo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ManifestNoTo.FocusOutCheckMethod")));
            this.ManifestNoTo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ManifestNoTo.ForeColor = System.Drawing.Color.Black;
            this.ManifestNoTo.GetCodeMasterField = "";
            this.ManifestNoTo.IsInputErrorOccured = false;
            this.ManifestNoTo.ItemDefinedTypes = "varchar";
            this.ManifestNoTo.Location = new System.Drawing.Point(294, 67);
            this.ManifestNoTo.MaxLength = 11;
            this.ManifestNoTo.Name = "ManifestNoTo";
            this.ManifestNoTo.PopupAfterExecute = null;
            this.ManifestNoTo.PopupBeforeExecute = null;
            this.ManifestNoTo.PopupGetMasterField = "";
            this.ManifestNoTo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ManifestNoTo.PopupSearchSendParams")));
            this.ManifestNoTo.PopupSetFormField = "";
            this.ManifestNoTo.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ManifestNoTo.PopupWindowName = "";
            this.ManifestNoTo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ManifestNoTo.popupWindowSetting")));
            this.ManifestNoTo.prevText = null;
            this.ManifestNoTo.PrevText = "";
            rangeSettingDto3.Max = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            rangeSettingDto3.Min = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ManifestNoTo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ManifestNoTo.RegistCheckMethod")));
            this.ManifestNoTo.SetFormField = "";
            this.ManifestNoTo.Size = new System.Drawing.Size(138, 20);
            this.ManifestNoTo.TabIndex = 5;
            this.ManifestNoTo.Tag = "半角11桁以内で入力してください";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(274, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 20);
            this.label2.TabIndex = 557;
            this.label2.Text = "～";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Syuruyi_Btn
            // 
            this.Syuruyi_Btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.Syuruyi_Btn.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Syuruyi_Btn.DBFieldsName = null;
            this.Syuruyi_Btn.DefaultBackColor = System.Drawing.Color.Empty;
            this.Syuruyi_Btn.DisplayItemName = "";
            this.Syuruyi_Btn.DisplayPopUp = null;
            this.Syuruyi_Btn.ErrorMessage = null;
            this.Syuruyi_Btn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Syuruyi_Btn.FocusOutCheckMethod")));
            this.Syuruyi_Btn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Syuruyi_Btn.GetCodeMasterField = null;
            this.Syuruyi_Btn.Image = ((System.Drawing.Image)(resources.GetObject("Syuruyi_Btn.Image")));
            this.Syuruyi_Btn.ItemDefinedTypes = null;
            this.Syuruyi_Btn.LinkedTextBoxs = null;
            this.Syuruyi_Btn.Location = new System.Drawing.Point(436, 88);
            this.Syuruyi_Btn.Name = "Syuruyi_Btn";
            this.Syuruyi_Btn.PopupAfterExecute = null;
            this.Syuruyi_Btn.PopupBeforeExecute = null;
            this.Syuruyi_Btn.PopupGetMasterField = "HAIKI_KBN_CD,HAIKI_SHURUI_NAME";
            this.Syuruyi_Btn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Syuruyi_Btn.PopupSearchSendParams")));
            this.Syuruyi_Btn.PopupSetFormField = "HAIKI_KBN_CD,HAIKI_SHURUI_NAME";
            this.Syuruyi_Btn.PopupWindowId = r_framework.Const.WINDOW_ID.M_HAIKI_SHURUI;
            this.Syuruyi_Btn.PopupWindowName = "マスタ共通ポップアップ";
            this.Syuruyi_Btn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Syuruyi_Btn.popupWindowSetting")));
            this.Syuruyi_Btn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Syuruyi_Btn.RegistCheckMethod")));
            this.Syuruyi_Btn.SearchDisplayFlag = 0;
            this.Syuruyi_Btn.SetFormField = "HAIKI_KBN_CD,HAIKI_SHURUI_NAME";
            this.Syuruyi_Btn.ShortItemName = "";
            this.Syuruyi_Btn.Size = new System.Drawing.Size(22, 22);
            this.Syuruyi_Btn.TabIndex = 6;
            this.Syuruyi_Btn.TabStop = false;
            this.Syuruyi_Btn.UseVisualStyleBackColor = false;
            this.Syuruyi_Btn.ZeroPaddengFlag = false;
            // 
            // HAIKI_KBN_CD
            // 
            this.HAIKI_KBN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.HAIKI_KBN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HAIKI_KBN_CD.ChangeUpperCase = true;
            this.HAIKI_KBN_CD.CharacterLimitList = null;
            this.HAIKI_KBN_CD.CharactersNumber = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.HAIKI_KBN_CD.DBFieldsName = "HAIKI_KBN_CD";
            this.HAIKI_KBN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.HAIKI_KBN_CD.DisplayItemName = "";
            this.HAIKI_KBN_CD.DisplayPopUp = null;
            this.HAIKI_KBN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAIKI_KBN_CD.FocusOutCheckMethod")));
            this.HAIKI_KBN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HAIKI_KBN_CD.ForeColor = System.Drawing.Color.Black;
            this.HAIKI_KBN_CD.GetCodeMasterField = "HAIKI_KBN_CD,HAIKI_SHURUI_NAME";
            this.HAIKI_KBN_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.HAIKI_KBN_CD.IsInputErrorOccured = false;
            this.HAIKI_KBN_CD.ItemDefinedTypes = "varchar";
            this.HAIKI_KBN_CD.Location = new System.Drawing.Point(134, 89);
            this.HAIKI_KBN_CD.MaxLength = 4;
            this.HAIKI_KBN_CD.Name = "HAIKI_KBN_CD";
            this.HAIKI_KBN_CD.PopupAfterExecute = null;
            this.HAIKI_KBN_CD.PopupBeforeExecute = null;
            this.HAIKI_KBN_CD.PopupGetMasterField = "HAIKI_KBN_CD,HAIKI_SHURUI_NAME";
            this.HAIKI_KBN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HAIKI_KBN_CD.PopupSearchSendParams")));
            this.HAIKI_KBN_CD.PopupSetFormField = "HAIKI_KBN_CD,HAIKI_SHURUI_NAME";
            this.HAIKI_KBN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_DENSHI_HAIKI_SHURUI;
            this.HAIKI_KBN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.HAIKI_KBN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HAIKI_KBN_CD.popupWindowSetting")));
            this.HAIKI_KBN_CD.prevText = null;
            this.HAIKI_KBN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAIKI_KBN_CD.RegistCheckMethod")));
            this.HAIKI_KBN_CD.SetFormField = "HAIKI_KBN_CD,HAIKI_SHURUI_NAME";
            this.HAIKI_KBN_CD.Size = new System.Drawing.Size(60, 20);
            this.HAIKI_KBN_CD.TabIndex = 6;
            this.HAIKI_KBN_CD.Tag = "廃棄物種類を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.HAIKI_KBN_CD.ZeroPaddengFlag = true;
            // 
            // HAIKI_SHURUI_NAME
            // 
            this.HAIKI_SHURUI_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.HAIKI_SHURUI_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HAIKI_SHURUI_NAME.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.HAIKI_SHURUI_NAME.DBFieldsName = "TORIHIKISAKI_NAME_RYAKU";
            this.HAIKI_SHURUI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.HAIKI_SHURUI_NAME.DisplayItemName = "";
            this.HAIKI_SHURUI_NAME.DisplayPopUp = null;
            this.HAIKI_SHURUI_NAME.ErrorMessage = "";
            this.HAIKI_SHURUI_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAIKI_SHURUI_NAME.FocusOutCheckMethod")));
            this.HAIKI_SHURUI_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HAIKI_SHURUI_NAME.ForeColor = System.Drawing.Color.Black;
            this.HAIKI_SHURUI_NAME.GetCodeMasterField = "";
            this.HAIKI_SHURUI_NAME.IsInputErrorOccured = false;
            this.HAIKI_SHURUI_NAME.ItemDefinedTypes = "";
            this.HAIKI_SHURUI_NAME.Location = new System.Drawing.Point(192, 89);
            this.HAIKI_SHURUI_NAME.MaxLength = 20;
            this.HAIKI_SHURUI_NAME.Name = "HAIKI_SHURUI_NAME";
            this.HAIKI_SHURUI_NAME.PopupAfterExecute = null;
            this.HAIKI_SHURUI_NAME.PopupBeforeExecute = null;
            this.HAIKI_SHURUI_NAME.PopupGetMasterField = "";
            this.HAIKI_SHURUI_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HAIKI_SHURUI_NAME.PopupSearchSendParams")));
            this.HAIKI_SHURUI_NAME.PopupSetFormField = "";
            this.HAIKI_SHURUI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HAIKI_SHURUI_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HAIKI_SHURUI_NAME.popupWindowSetting")));
            this.HAIKI_SHURUI_NAME.prevText = null;
            this.HAIKI_SHURUI_NAME.ReadOnly = true;
            this.HAIKI_SHURUI_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAIKI_SHURUI_NAME.RegistCheckMethod")));
            this.HAIKI_SHURUI_NAME.SetFormField = "";
            this.HAIKI_SHURUI_NAME.Size = new System.Drawing.Size(240, 20);
            this.HAIKI_SHURUI_NAME.TabIndex = 561;
            this.HAIKI_SHURUI_NAME.TabStop = false;
            this.HAIKI_SHURUI_NAME.Tag = "　";
            // 
            // HAIKI_LABEL
            // 
            this.HAIKI_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.HAIKI_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HAIKI_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HAIKI_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HAIKI_LABEL.ForeColor = System.Drawing.Color.White;
            this.HAIKI_LABEL.Location = new System.Drawing.Point(3, 89);
            this.HAIKI_LABEL.Name = "HAIKI_LABEL";
            this.HAIKI_LABEL.Size = new System.Drawing.Size(126, 20);
            this.HAIKI_LABEL.TabIndex = 560;
            this.HAIKI_LABEL.Text = "廃棄物種類";
            this.HAIKI_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Jigyousha_Btn
            // 
            this.Jigyousha_Btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.Jigyousha_Btn.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Jigyousha_Btn.DBFieldsName = null;
            this.Jigyousha_Btn.DefaultBackColor = System.Drawing.Color.Empty;
            this.Jigyousha_Btn.DisplayItemName = "取引先CD";
            this.Jigyousha_Btn.DisplayPopUp = null;
            this.Jigyousha_Btn.ErrorMessage = null;
            this.Jigyousha_Btn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Jigyousha_Btn.FocusOutCheckMethod")));
            this.Jigyousha_Btn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Jigyousha_Btn.GetCodeMasterField = null;
            this.Jigyousha_Btn.Image = ((System.Drawing.Image)(resources.GetObject("Jigyousha_Btn.Image")));
            this.Jigyousha_Btn.ItemDefinedTypes = null;
            this.Jigyousha_Btn.LinkedTextBoxs = null;
            this.Jigyousha_Btn.Location = new System.Drawing.Point(978, -1);
            this.Jigyousha_Btn.Name = "Jigyousha_Btn";
            this.Jigyousha_Btn.PopupAfterExecute = null;
            this.Jigyousha_Btn.PopupAfterExecuteMethod = "Jigyousya_PopupAfterExecuteMethod";
            this.Jigyousha_Btn.PopupBeforeExecute = null;
            this.Jigyousha_Btn.PopupBeforeExecuteMethod = "Jigyousya_PopupBeforeExecuteMethod";
            this.Jigyousha_Btn.PopupGetMasterField = "EDI_MEMBER_ID,JIGYOUSHA_NAME";
            this.Jigyousha_Btn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Jigyousha_Btn.PopupSearchSendParams")));
            this.Jigyousha_Btn.PopupSetFormField = "Jigyousya_CD,JIGYOUSHA_NAME";
            this.Jigyousha_Btn.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHAIN;
            this.Jigyousha_Btn.PopupWindowName = "マスタ共通ポップアップ";
            this.Jigyousha_Btn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Jigyousha_Btn.popupWindowSetting")));
            this.Jigyousha_Btn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Jigyousha_Btn.RegistCheckMethod")));
            this.Jigyousha_Btn.SearchDisplayFlag = 0;
            this.Jigyousha_Btn.SetFormField = "EDI_MEMBER_ID,JIGYOUSHA_NAME";
            this.Jigyousha_Btn.ShortItemName = "";
            this.Jigyousha_Btn.Size = new System.Drawing.Size(22, 22);
            this.Jigyousha_Btn.TabIndex = 8;
            this.Jigyousha_Btn.TabStop = false;
            this.Jigyousha_Btn.UseVisualStyleBackColor = false;
            this.Jigyousha_Btn.ZeroPaddengFlag = false;
            // 
            // Jigyousya_CD
            // 
            this.Jigyousya_CD.BackColor = System.Drawing.SystemColors.Window;
            this.Jigyousya_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Jigyousya_CD.ChangeUpperCase = true;
            this.Jigyousya_CD.CharacterLimitList = null;
            this.Jigyousya_CD.CharactersNumber = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.Jigyousya_CD.DBFieldsName = "";
            this.Jigyousya_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.Jigyousya_CD.DisplayItemName = "";
            this.Jigyousya_CD.DisplayPopUp = null;
            this.Jigyousya_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Jigyousya_CD.FocusOutCheckMethod")));
            this.Jigyousya_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Jigyousya_CD.ForeColor = System.Drawing.Color.Black;
            this.Jigyousya_CD.GetCodeMasterField = "EDI_MEMBER_ID,JIGYOUSHA_NAME";
            this.Jigyousya_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Jigyousya_CD.IsInputErrorOccured = false;
            this.Jigyousya_CD.ItemDefinedTypes = "varchar";
            this.Jigyousya_CD.Location = new System.Drawing.Point(734, 0);
            this.Jigyousya_CD.MaxLength = 7;
            this.Jigyousya_CD.Name = "Jigyousya_CD";
            this.Jigyousya_CD.PopupAfterExecute = null;
            this.Jigyousya_CD.PopupAfterExecuteMethod = "Jigyousya_PopupAfterExecuteMethod";
            this.Jigyousya_CD.PopupBeforeExecute = null;
            this.Jigyousya_CD.PopupBeforeExecuteMethod = "Jigyousya_PopupBeforeExecuteMethod";
            this.Jigyousya_CD.PopupGetMasterField = "EDI_MEMBER_ID,JIGYOUSHA_NAME";
            this.Jigyousya_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Jigyousya_CD.PopupSearchSendParams")));
            this.Jigyousya_CD.PopupSetFormField = "Jigyousya_CD,JIGYOUSHA_NAME";
            this.Jigyousya_CD.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
            this.Jigyousya_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.Jigyousya_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Jigyousya_CD.popupWindowSetting")));
            this.Jigyousya_CD.prevText = null;
            this.Jigyousya_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Jigyousya_CD.RegistCheckMethod")));
            this.Jigyousya_CD.SetFormField = "EDI_MEMBER_ID,JIGYOUSHA_NAME";
            this.Jigyousya_CD.Size = new System.Drawing.Size(74, 20);
            this.Jigyousya_CD.TabIndex = 7;
            this.Jigyousya_CD.Tag = "排出事業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.Jigyousya_CD.ZeroPaddengFlag = true;
            // 
            // JIGYOUSHA_NAME
            // 
            this.JIGYOUSHA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.JIGYOUSHA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JIGYOUSHA_NAME.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.JIGYOUSHA_NAME.DBFieldsName = "TORIHIKISAKI_NAME_RYAKU";
            this.JIGYOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUSHA_NAME.DisplayItemName = "";
            this.JIGYOUSHA_NAME.DisplayPopUp = null;
            this.JIGYOUSHA_NAME.ErrorMessage = "";
            this.JIGYOUSHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_NAME.FocusOutCheckMethod")));
            this.JIGYOUSHA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.JIGYOUSHA_NAME.ForeColor = System.Drawing.Color.Black;
            this.JIGYOUSHA_NAME.GetCodeMasterField = "";
            this.JIGYOUSHA_NAME.IsInputErrorOccured = false;
            this.JIGYOUSHA_NAME.ItemDefinedTypes = "";
            this.JIGYOUSHA_NAME.Location = new System.Drawing.Point(807, 0);
            this.JIGYOUSHA_NAME.MaxLength = 20;
            this.JIGYOUSHA_NAME.Name = "JIGYOUSHA_NAME";
            this.JIGYOUSHA_NAME.PopupAfterExecute = null;
            this.JIGYOUSHA_NAME.PopupBeforeExecute = null;
            this.JIGYOUSHA_NAME.PopupGetMasterField = "";
            this.JIGYOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JIGYOUSHA_NAME.PopupSearchSendParams")));
            this.JIGYOUSHA_NAME.PopupSetFormField = "";
            this.JIGYOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JIGYOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JIGYOUSHA_NAME.popupWindowSetting")));
            this.JIGYOUSHA_NAME.prevText = null;
            this.JIGYOUSHA_NAME.ReadOnly = true;
            this.JIGYOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_NAME.RegistCheckMethod")));
            this.JIGYOUSHA_NAME.SetFormField = "";
            this.JIGYOUSHA_NAME.Size = new System.Drawing.Size(167, 20);
            this.JIGYOUSHA_NAME.TabIndex = 565;
            this.JIGYOUSHA_NAME.TabStop = false;
            this.JIGYOUSHA_NAME.Tag = "　";
            // 
            // Jigyousya_Label
            // 
            this.Jigyousya_Label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.Jigyousya_Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Jigyousya_Label.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Jigyousya_Label.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Jigyousya_Label.ForeColor = System.Drawing.Color.White;
            this.Jigyousya_Label.Location = new System.Drawing.Point(603, 0);
            this.Jigyousya_Label.Name = "Jigyousya_Label";
            this.Jigyousya_Label.Size = new System.Drawing.Size(126, 20);
            this.Jigyousya_Label.TabIndex = 564;
            this.Jigyousya_Label.Text = "排出事業者";
            this.Jigyousya_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Unpan_Btn
            // 
            this.Unpan_Btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.Unpan_Btn.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Unpan_Btn.DBFieldsName = null;
            this.Unpan_Btn.DefaultBackColor = System.Drawing.Color.Empty;
            this.Unpan_Btn.DisplayItemName = "取引先CD";
            this.Unpan_Btn.DisplayPopUp = null;
            this.Unpan_Btn.ErrorMessage = null;
            this.Unpan_Btn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Unpan_Btn.FocusOutCheckMethod")));
            this.Unpan_Btn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Unpan_Btn.GetCodeMasterField = null;
            this.Unpan_Btn.Image = ((System.Drawing.Image)(resources.GetObject("Unpan_Btn.Image")));
            this.Unpan_Btn.ItemDefinedTypes = null;
            this.Unpan_Btn.LinkedTextBoxs = null;
            this.Unpan_Btn.Location = new System.Drawing.Point(978, 44);
            this.Unpan_Btn.Name = "Unpan_Btn";
            this.Unpan_Btn.PopupAfterExecute = null;
            this.Unpan_Btn.PopupBeforeExecute = null;
            this.Unpan_Btn.PopupGetMasterField = "EDI_MEMBER_ID,JIGYOUSHA_NAME";
            this.Unpan_Btn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Unpan_Btn.PopupSearchSendParams")));
            this.Unpan_Btn.PopupSetFormField = "Unpan_CD,Unpan_Name";
            this.Unpan_Btn.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
            this.Unpan_Btn.PopupWindowName = "マスタ共通ポップアップ";
            this.Unpan_Btn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Unpan_Btn.popupWindowSetting")));
            this.Unpan_Btn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Unpan_Btn.RegistCheckMethod")));
            this.Unpan_Btn.SearchDisplayFlag = 0;
            this.Unpan_Btn.SetFormField = "EIGYOUTANTOU_NO,EIGYOUTANTOU_NAME";
            this.Unpan_Btn.ShortItemName = "";
            this.Unpan_Btn.Size = new System.Drawing.Size(22, 22);
            this.Unpan_Btn.TabIndex = 12;
            this.Unpan_Btn.TabStop = false;
            this.Unpan_Btn.UseVisualStyleBackColor = false;
            this.Unpan_Btn.ZeroPaddengFlag = false;
            // 
            // Unpan_CD
            // 
            this.Unpan_CD.BackColor = System.Drawing.SystemColors.Window;
            this.Unpan_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Unpan_CD.ChangeUpperCase = true;
            this.Unpan_CD.CharacterLimitList = null;
            this.Unpan_CD.CharactersNumber = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.Unpan_CD.DBFieldsName = "";
            this.Unpan_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.Unpan_CD.DisplayItemName = "社員CD";
            this.Unpan_CD.DisplayPopUp = null;
            this.Unpan_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Unpan_CD.FocusOutCheckMethod")));
            this.Unpan_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Unpan_CD.ForeColor = System.Drawing.Color.Black;
            this.Unpan_CD.GetCodeMasterField = "";
            this.Unpan_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Unpan_CD.IsInputErrorOccured = false;
            this.Unpan_CD.ItemDefinedTypes = "varchar";
            this.Unpan_CD.Location = new System.Drawing.Point(734, 45);
            this.Unpan_CD.MaxLength = 7;
            this.Unpan_CD.Name = "Unpan_CD";
            this.Unpan_CD.PopupAfterExecute = null;
            this.Unpan_CD.PopupBeforeExecute = null;
            this.Unpan_CD.PopupGetMasterField = "EDI_MEMBER_ID,JIGYOUSHA_NAME";
            this.Unpan_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Unpan_CD.PopupSearchSendParams")));
            this.Unpan_CD.PopupSetFormField = "Unpan_CD,Unpan_Name";
            this.Unpan_CD.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
            this.Unpan_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.Unpan_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Unpan_CD.popupWindowSetting")));
            this.Unpan_CD.prevText = null;
            this.Unpan_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Unpan_CD.RegistCheckMethod")));
            this.Unpan_CD.SetFormField = "";
            this.Unpan_CD.Size = new System.Drawing.Size(74, 20);
            this.Unpan_CD.TabIndex = 11;
            this.Unpan_CD.Tag = "報告収集運搬を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.Unpan_CD.ZeroPaddengFlag = true;
            // 
            // Unpan_Name
            // 
            this.Unpan_Name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.Unpan_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Unpan_Name.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.Unpan_Name.DBFieldsName = "";
            this.Unpan_Name.DefaultBackColor = System.Drawing.Color.Empty;
            this.Unpan_Name.DisplayItemName = "";
            this.Unpan_Name.DisplayPopUp = null;
            this.Unpan_Name.ErrorMessage = "";
            this.Unpan_Name.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Unpan_Name.FocusOutCheckMethod")));
            this.Unpan_Name.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Unpan_Name.ForeColor = System.Drawing.Color.Black;
            this.Unpan_Name.GetCodeMasterField = "";
            this.Unpan_Name.IsInputErrorOccured = false;
            this.Unpan_Name.ItemDefinedTypes = "";
            this.Unpan_Name.Location = new System.Drawing.Point(807, 45);
            this.Unpan_Name.MaxLength = 20;
            this.Unpan_Name.Name = "Unpan_Name";
            this.Unpan_Name.PopupAfterExecute = null;
            this.Unpan_Name.PopupBeforeExecute = null;
            this.Unpan_Name.PopupGetMasterField = "";
            this.Unpan_Name.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Unpan_Name.PopupSearchSendParams")));
            this.Unpan_Name.PopupSetFormField = "";
            this.Unpan_Name.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Unpan_Name.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Unpan_Name.popupWindowSetting")));
            this.Unpan_Name.prevText = null;
            this.Unpan_Name.ReadOnly = true;
            this.Unpan_Name.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Unpan_Name.RegistCheckMethod")));
            this.Unpan_Name.SetFormField = "";
            this.Unpan_Name.Size = new System.Drawing.Size(167, 20);
            this.Unpan_Name.TabIndex = 569;
            this.Unpan_Name.TabStop = false;
            this.Unpan_Name.Tag = "　";
            // 
            // Unpan_Label
            // 
            this.Unpan_Label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.Unpan_Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Unpan_Label.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Unpan_Label.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Unpan_Label.ForeColor = System.Drawing.Color.White;
            this.Unpan_Label.Location = new System.Drawing.Point(603, 45);
            this.Unpan_Label.Name = "Unpan_Label";
            this.Unpan_Label.Size = new System.Drawing.Size(126, 20);
            this.Unpan_Label.TabIndex = 580;
            this.Unpan_Label.Text = "報告収集運搬";
            this.Unpan_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Unpansha_Btn
            // 
            this.Unpansha_Btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.Unpansha_Btn.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Unpansha_Btn.DBFieldsName = null;
            this.Unpansha_Btn.DefaultBackColor = System.Drawing.Color.Empty;
            this.Unpansha_Btn.DisplayItemName = "取引先CD";
            this.Unpansha_Btn.DisplayPopUp = null;
            this.Unpansha_Btn.ErrorMessage = null;
            this.Unpansha_Btn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Unpansha_Btn.FocusOutCheckMethod")));
            this.Unpansha_Btn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Unpansha_Btn.GetCodeMasterField = null;
            this.Unpansha_Btn.Image = ((System.Drawing.Image)(resources.GetObject("Unpansha_Btn.Image")));
            this.Unpansha_Btn.ItemDefinedTypes = null;
            this.Unpansha_Btn.LinkedTextBoxs = null;
            this.Unpansha_Btn.Location = new System.Drawing.Point(978, 66);
            this.Unpansha_Btn.Name = "Unpansha_Btn";
            this.Unpansha_Btn.PopupAfterExecute = null;
            this.Unpansha_Btn.PopupAfterExecuteMethod = "Unpansha_PopupAfterExecuteMethod";
            this.Unpansha_Btn.PopupBeforeExecute = null;
            this.Unpansha_Btn.PopupBeforeExecuteMethod = "Unpansha_PopupBeforeExecuteMethod";
            this.Unpansha_Btn.PopupGetMasterField = "SHAIN_CD,SHAIN_NAME_RYAKU";
            this.Unpansha_Btn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Unpansha_Btn.PopupSearchSendParams")));
            this.Unpansha_Btn.PopupSetFormField = "Unpansha_CD,Unpansha_Name";
            this.Unpansha_Btn.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHAIN;
            this.Unpansha_Btn.PopupWindowName = "マスタ共通ポップアップ";
            this.Unpansha_Btn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Unpansha_Btn.popupWindowSetting")));
            this.Unpansha_Btn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Unpansha_Btn.RegistCheckMethod")));
            this.Unpansha_Btn.SearchDisplayFlag = 0;
            this.Unpansha_Btn.SetFormField = "EIGYOUTANTOU_NO,EIGYOUTANTOU_NAME";
            this.Unpansha_Btn.ShortItemName = "";
            this.Unpansha_Btn.Size = new System.Drawing.Size(22, 22);
            this.Unpansha_Btn.TabIndex = 14;
            this.Unpansha_Btn.TabStop = false;
            this.Unpansha_Btn.UseVisualStyleBackColor = false;
            this.Unpansha_Btn.ZeroPaddengFlag = false;
            // 
            // Unpansha_CD
            // 
            this.Unpansha_CD.BackColor = System.Drawing.SystemColors.Window;
            this.Unpansha_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Unpansha_CD.ChangeUpperCase = true;
            this.Unpansha_CD.CharacterLimitList = null;
            this.Unpansha_CD.CharactersNumber = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.Unpansha_CD.DBFieldsName = "";
            this.Unpansha_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.Unpansha_CD.DisplayItemName = "";
            this.Unpansha_CD.DisplayPopUp = null;
            this.Unpansha_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Unpansha_CD.FocusOutCheckMethod")));
            this.Unpansha_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Unpansha_CD.ForeColor = System.Drawing.Color.Black;
            this.Unpansha_CD.GetCodeMasterField = "";
            this.Unpansha_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Unpansha_CD.IsInputErrorOccured = false;
            this.Unpansha_CD.ItemDefinedTypes = "varchar";
            this.Unpansha_CD.Location = new System.Drawing.Point(734, 67);
            this.Unpansha_CD.MaxLength = 7;
            this.Unpansha_CD.Name = "Unpansha_CD";
            this.Unpansha_CD.PopupAfterExecute = null;
            this.Unpansha_CD.PopupAfterExecuteMethod = "Unpansha_PopupAfterExecuteMethod";
            this.Unpansha_CD.PopupBeforeExecute = null;
            this.Unpansha_CD.PopupBeforeExecuteMethod = "Unpansha_PopupBeforeExecuteMethod";
            this.Unpansha_CD.PopupGetMasterField = "EDI_MEMBER_ID,JIGYOUSHA_NAME";
            this.Unpansha_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Unpansha_CD.PopupSearchSendParams")));
            this.Unpansha_CD.PopupSetFormField = "Unpansha_CD,Unpansha_Name";
            this.Unpansha_CD.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
            this.Unpansha_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.Unpansha_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Unpansha_CD.popupWindowSetting")));
            this.Unpansha_CD.prevText = null;
            this.Unpansha_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Unpansha_CD.RegistCheckMethod")));
            this.Unpansha_CD.SetFormField = "";
            this.Unpansha_CD.Size = new System.Drawing.Size(74, 20);
            this.Unpansha_CD.TabIndex = 13;
            this.Unpansha_CD.Tag = "運搬先事業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.Unpansha_CD.ZeroPaddengFlag = true;
            // 
            // Unpansha_Name
            // 
            this.Unpansha_Name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.Unpansha_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Unpansha_Name.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.Unpansha_Name.DBFieldsName = "";
            this.Unpansha_Name.DefaultBackColor = System.Drawing.Color.Empty;
            this.Unpansha_Name.DisplayItemName = "";
            this.Unpansha_Name.DisplayPopUp = null;
            this.Unpansha_Name.ErrorMessage = "";
            this.Unpansha_Name.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Unpansha_Name.FocusOutCheckMethod")));
            this.Unpansha_Name.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Unpansha_Name.ForeColor = System.Drawing.Color.Black;
            this.Unpansha_Name.GetCodeMasterField = "";
            this.Unpansha_Name.IsInputErrorOccured = false;
            this.Unpansha_Name.ItemDefinedTypes = "";
            this.Unpansha_Name.Location = new System.Drawing.Point(807, 67);
            this.Unpansha_Name.MaxLength = 20;
            this.Unpansha_Name.Name = "Unpansha_Name";
            this.Unpansha_Name.PopupAfterExecute = null;
            this.Unpansha_Name.PopupBeforeExecute = null;
            this.Unpansha_Name.PopupGetMasterField = "";
            this.Unpansha_Name.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Unpansha_Name.PopupSearchSendParams")));
            this.Unpansha_Name.PopupSetFormField = "";
            this.Unpansha_Name.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Unpansha_Name.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Unpansha_Name.popupWindowSetting")));
            this.Unpansha_Name.prevText = null;
            this.Unpansha_Name.ReadOnly = true;
            this.Unpansha_Name.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Unpansha_Name.RegistCheckMethod")));
            this.Unpansha_Name.SetFormField = "";
            this.Unpansha_Name.Size = new System.Drawing.Size(167, 20);
            this.Unpansha_Name.TabIndex = 573;
            this.Unpansha_Name.TabStop = false;
            this.Unpansha_Name.Tag = "　";
            // 
            // Unpansha_Label
            // 
            this.Unpansha_Label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.Unpansha_Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Unpansha_Label.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Unpansha_Label.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Unpansha_Label.ForeColor = System.Drawing.Color.White;
            this.Unpansha_Label.Location = new System.Drawing.Point(603, 67);
            this.Unpansha_Label.Name = "Unpansha_Label";
            this.Unpansha_Label.Size = new System.Drawing.Size(126, 20);
            this.Unpansha_Label.TabIndex = 579;
            this.Unpansha_Label.Text = "運搬先事業者";
            this.Unpansha_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Unpanba_Btn
            // 
            this.Unpanba_Btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.Unpanba_Btn.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Unpanba_Btn.DBFieldsName = null;
            this.Unpanba_Btn.DefaultBackColor = System.Drawing.Color.Empty;
            this.Unpanba_Btn.DisplayItemName = "";
            this.Unpanba_Btn.DisplayPopUp = null;
            this.Unpanba_Btn.ErrorMessage = null;
            this.Unpanba_Btn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Unpanba_Btn.FocusOutCheckMethod")));
            this.Unpanba_Btn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Unpanba_Btn.GetCodeMasterField = null;
            this.Unpanba_Btn.Image = ((System.Drawing.Image)(resources.GetObject("Unpanba_Btn.Image")));
            this.Unpanba_Btn.ItemDefinedTypes = null;
            this.Unpanba_Btn.LinkedTextBoxs = null;
            this.Unpanba_Btn.Location = new System.Drawing.Point(978, 88);
            this.Unpanba_Btn.Name = "Unpanba_Btn";
            this.Unpanba_Btn.PopupAfterExecute = null;
            this.Unpanba_Btn.PopupBeforeExecute = null;
            this.Unpanba_Btn.PopupGetMasterField = "SHAIN_CD,SHAIN_NAME_RYAKU";
            this.Unpanba_Btn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Unpanba_Btn.PopupSearchSendParams")));
            this.Unpanba_Btn.PopupSetFormField = "Unpanba_CD,Unpanba_Name";
            this.Unpanba_Btn.PopupWindowId = r_framework.Const.WINDOW_ID.M_DENSHI_JIGYOUJOU;
            this.Unpanba_Btn.PopupWindowName = "";
            this.Unpanba_Btn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Unpanba_Btn.popupWindowSetting")));
            this.Unpanba_Btn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Unpanba_Btn.RegistCheckMethod")));
            this.Unpanba_Btn.SearchDisplayFlag = 0;
            this.Unpanba_Btn.SetFormField = "EIGYOUTANTOU_NO,EIGYOUTANTOU_NAME";
            this.Unpanba_Btn.ShortItemName = "";
            this.Unpanba_Btn.Size = new System.Drawing.Size(22, 22);
            this.Unpanba_Btn.TabIndex = 16;
            this.Unpanba_Btn.TabStop = false;
            this.Unpanba_Btn.UseVisualStyleBackColor = false;
            this.Unpanba_Btn.ZeroPaddengFlag = false;
            // 
            // Unpanba_CD
            // 
            this.Unpanba_CD.BackColor = System.Drawing.SystemColors.Window;
            this.Unpanba_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Unpanba_CD.ChangeUpperCase = true;
            this.Unpanba_CD.CharacterLimitList = null;
            this.Unpanba_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.Unpanba_CD.DBFieldsName = "";
            this.Unpanba_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.Unpanba_CD.DisplayItemName = "";
            this.Unpanba_CD.DisplayPopUp = null;
            this.Unpanba_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Unpanba_CD.FocusOutCheckMethod")));
            this.Unpanba_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Unpanba_CD.ForeColor = System.Drawing.Color.Black;
            this.Unpanba_CD.GetCodeMasterField = "";
            this.Unpanba_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Unpanba_CD.IsInputErrorOccured = false;
            this.Unpanba_CD.ItemDefinedTypes = "varchar";
            this.Unpanba_CD.Location = new System.Drawing.Point(734, 89);
            this.Unpanba_CD.MaxLength = 6;
            this.Unpanba_CD.Name = "Unpanba_CD";
            this.Unpanba_CD.PopupAfterExecute = null;
            this.Unpanba_CD.PopupBeforeExecute = null;
            this.Unpanba_CD.PopupGetMasterField = "GENBA_CD,JIGYOUSHA_NAME";
            this.Unpanba_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Unpanba_CD.PopupSearchSendParams")));
            this.Unpanba_CD.PopupSetFormField = "Unpanba_CD,Unpanba_Name";
            this.Unpanba_CD.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
            this.Unpanba_CD.PopupWindowName = "";
            this.Unpanba_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Unpanba_CD.popupWindowSetting")));
            this.Unpanba_CD.prevText = null;
            this.Unpanba_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Unpanba_CD.RegistCheckMethod")));
            this.Unpanba_CD.SetFormField = "";
            this.Unpanba_CD.Size = new System.Drawing.Size(74, 20);
            this.Unpanba_CD.TabIndex = 15;
            this.Unpanba_CD.Tag = "運搬先事業場を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.Unpanba_CD.ZeroPaddengFlag = true;
            // 
            // Unpanba_Name
            // 
            this.Unpanba_Name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.Unpanba_Name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Unpanba_Name.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.Unpanba_Name.DBFieldsName = "TORIHIKISAKI_NAME_RYAKU";
            this.Unpanba_Name.DefaultBackColor = System.Drawing.Color.Empty;
            this.Unpanba_Name.DisplayItemName = "";
            this.Unpanba_Name.DisplayPopUp = null;
            this.Unpanba_Name.ErrorMessage = "";
            this.Unpanba_Name.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Unpanba_Name.FocusOutCheckMethod")));
            this.Unpanba_Name.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Unpanba_Name.ForeColor = System.Drawing.Color.Black;
            this.Unpanba_Name.GetCodeMasterField = "";
            this.Unpanba_Name.IsInputErrorOccured = false;
            this.Unpanba_Name.ItemDefinedTypes = "";
            this.Unpanba_Name.Location = new System.Drawing.Point(807, 89);
            this.Unpanba_Name.MaxLength = 20;
            this.Unpanba_Name.Name = "Unpanba_Name";
            this.Unpanba_Name.PopupAfterExecute = null;
            this.Unpanba_Name.PopupBeforeExecute = null;
            this.Unpanba_Name.PopupGetMasterField = "";
            this.Unpanba_Name.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Unpanba_Name.PopupSearchSendParams")));
            this.Unpanba_Name.PopupSetFormField = "";
            this.Unpanba_Name.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Unpanba_Name.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Unpanba_Name.popupWindowSetting")));
            this.Unpanba_Name.prevText = null;
            this.Unpanba_Name.ReadOnly = true;
            this.Unpanba_Name.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Unpanba_Name.RegistCheckMethod")));
            this.Unpanba_Name.SetFormField = "";
            this.Unpanba_Name.Size = new System.Drawing.Size(167, 20);
            this.Unpanba_Name.TabIndex = 577;
            this.Unpanba_Name.TabStop = false;
            this.Unpanba_Name.Tag = "　";
            // 
            // Unpanba_Label
            // 
            this.Unpanba_Label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.Unpanba_Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Unpanba_Label.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Unpanba_Label.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Unpanba_Label.ForeColor = System.Drawing.Color.White;
            this.Unpanba_Label.Location = new System.Drawing.Point(603, 89);
            this.Unpanba_Label.Name = "Unpanba_Label";
            this.Unpanba_Label.Size = new System.Drawing.Size(126, 20);
            this.Unpanba_Label.TabIndex = 578;
            this.Unpanba_Label.Text = "運搬先事業場";
            this.Unpanba_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Jigyoujou_Btn
            // 
            this.Jigyoujou_Btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.Jigyoujou_Btn.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Jigyoujou_Btn.DBFieldsName = null;
            this.Jigyoujou_Btn.DefaultBackColor = System.Drawing.Color.Empty;
            this.Jigyoujou_Btn.DisplayItemName = "取引先CD";
            this.Jigyoujou_Btn.DisplayPopUp = null;
            this.Jigyoujou_Btn.ErrorMessage = null;
            this.Jigyoujou_Btn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Jigyoujou_Btn.FocusOutCheckMethod")));
            this.Jigyoujou_Btn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Jigyoujou_Btn.GetCodeMasterField = null;
            this.Jigyoujou_Btn.Image = ((System.Drawing.Image)(resources.GetObject("Jigyoujou_Btn.Image")));
            this.Jigyoujou_Btn.ItemDefinedTypes = null;
            this.Jigyoujou_Btn.LinkedTextBoxs = null;
            this.Jigyoujou_Btn.Location = new System.Drawing.Point(978, 21);
            this.Jigyoujou_Btn.Name = "Jigyoujou_Btn";
            this.Jigyoujou_Btn.PopupAfterExecute = null;
            this.Jigyoujou_Btn.PopupBeforeExecute = null;
            this.Jigyoujou_Btn.PopupGetMasterField = "JIGYOUJOU_CD,JIGYOUJOU_NAME";
            this.Jigyoujou_Btn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Jigyoujou_Btn.PopupSearchSendParams")));
            this.Jigyoujou_Btn.PopupSetFormField = "Jigyoujou_CD,JIGYOUJOU_NAME";
            this.Jigyoujou_Btn.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHAIN;
            this.Jigyoujou_Btn.PopupWindowName = "マスタ共通ポップアップ";
            this.Jigyoujou_Btn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Jigyoujou_Btn.popupWindowSetting")));
            this.Jigyoujou_Btn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Jigyoujou_Btn.RegistCheckMethod")));
            this.Jigyoujou_Btn.SearchDisplayFlag = 0;
            this.Jigyoujou_Btn.SetFormField = "JIGYOUJOU_CD,JIGYOUJOU_NAME";
            this.Jigyoujou_Btn.ShortItemName = "";
            this.Jigyoujou_Btn.Size = new System.Drawing.Size(22, 22);
            this.Jigyoujou_Btn.TabIndex = 10;
            this.Jigyoujou_Btn.TabStop = false;
            this.Jigyoujou_Btn.UseVisualStyleBackColor = false;
            this.Jigyoujou_Btn.ZeroPaddengFlag = false;
            // 
            // Jigyoujou_CD
            // 
            this.Jigyoujou_CD.BackColor = System.Drawing.SystemColors.Window;
            this.Jigyoujou_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Jigyoujou_CD.ChangeUpperCase = true;
            this.Jigyoujou_CD.CharacterLimitList = null;
            this.Jigyoujou_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.Jigyoujou_CD.DBFieldsName = "";
            this.Jigyoujou_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.Jigyoujou_CD.DisplayItemName = "";
            this.Jigyoujou_CD.DisplayPopUp = null;
            this.Jigyoujou_CD.FocusOutCheckMethod = null;
            this.Jigyoujou_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Jigyoujou_CD.ForeColor = System.Drawing.Color.Black;
            this.Jigyoujou_CD.GetCodeMasterField = "JIGYOUJOU_CD,JIGYOUJOU_NAME";
            this.Jigyoujou_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Jigyoujou_CD.IsInputErrorOccured = false;
            this.Jigyoujou_CD.ItemDefinedTypes = "varchar";
            this.Jigyoujou_CD.Location = new System.Drawing.Point(734, 22);
            this.Jigyoujou_CD.MaxLength = 6;
            this.Jigyoujou_CD.Name = "Jigyoujou_CD";
            this.Jigyoujou_CD.PopupAfterExecute = null;
            this.Jigyoujou_CD.PopupBeforeExecute = null;
            this.Jigyoujou_CD.PopupGetMasterField = "JIGYOUJOU_CD,JIGYOUJOU_NAME";
            this.Jigyoujou_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Jigyoujou_CD.PopupSearchSendParams")));
            this.Jigyoujou_CD.PopupSetFormField = "Jigyoujou_CD,JIGYOUJOU_NAME";
            this.Jigyoujou_CD.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
            this.Jigyoujou_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.Jigyoujou_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Jigyoujou_CD.popupWindowSetting")));
            this.Jigyoujou_CD.prevText = null;
            this.Jigyoujou_CD.RegistCheckMethod = null;
            this.Jigyoujou_CD.SetFormField = "JIGYOUJOU_CD,JIGYOUJOU_NAME";
            this.Jigyoujou_CD.Size = new System.Drawing.Size(74, 20);
            this.Jigyoujou_CD.TabIndex = 9;
            this.Jigyoujou_CD.Tag = "排出事業場を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.Jigyoujou_CD.ZeroPaddengFlag = true;
            // 
            // JIGYOUBA_NAME
            // 
            this.JIGYOUBA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.JIGYOUBA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JIGYOUBA_NAME.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.JIGYOUBA_NAME.DBFieldsName = "GENBA_NAME_RYAKU";
            this.JIGYOUBA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUBA_NAME.DisplayItemName = "";
            this.JIGYOUBA_NAME.DisplayPopUp = null;
            this.JIGYOUBA_NAME.ErrorMessage = "";
            this.JIGYOUBA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUBA_NAME.FocusOutCheckMethod")));
            this.JIGYOUBA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.JIGYOUBA_NAME.ForeColor = System.Drawing.Color.Black;
            this.JIGYOUBA_NAME.GetCodeMasterField = "";
            this.JIGYOUBA_NAME.IsInputErrorOccured = false;
            this.JIGYOUBA_NAME.ItemDefinedTypes = "";
            this.JIGYOUBA_NAME.Location = new System.Drawing.Point(807, 22);
            this.JIGYOUBA_NAME.MaxLength = 20;
            this.JIGYOUBA_NAME.Name = "JIGYOUBA_NAME";
            this.JIGYOUBA_NAME.PopupAfterExecute = null;
            this.JIGYOUBA_NAME.PopupBeforeExecute = null;
            this.JIGYOUBA_NAME.PopupGetMasterField = "";
            this.JIGYOUBA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JIGYOUBA_NAME.PopupSearchSendParams")));
            this.JIGYOUBA_NAME.PopupSetFormField = "";
            this.JIGYOUBA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JIGYOUBA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JIGYOUBA_NAME.popupWindowSetting")));
            this.JIGYOUBA_NAME.prevText = null;
            this.JIGYOUBA_NAME.ReadOnly = true;
            this.JIGYOUBA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUBA_NAME.RegistCheckMethod")));
            this.JIGYOUBA_NAME.SetFormField = "";
            this.JIGYOUBA_NAME.Size = new System.Drawing.Size(167, 20);
            this.JIGYOUBA_NAME.TabIndex = 585;
            this.JIGYOUBA_NAME.TabStop = false;
            this.JIGYOUBA_NAME.Tag = "　";
            // 
            // Jigyouba_Label
            // 
            this.Jigyouba_Label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.Jigyouba_Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Jigyouba_Label.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Jigyouba_Label.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Jigyouba_Label.ForeColor = System.Drawing.Color.White;
            this.Jigyouba_Label.Location = new System.Drawing.Point(603, 22);
            this.Jigyouba_Label.Name = "Jigyouba_Label";
            this.Jigyouba_Label.Size = new System.Drawing.Size(126, 20);
            this.Jigyouba_Label.TabIndex = 584;
            this.Jigyouba_Label.Text = "排出事業場";
            this.Jigyouba_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IchiranDgv1
            // 
            this.IchiranDgv1.AllowUserToAddRows = false;
            this.IchiranDgv1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.IchiranDgv1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.IchiranDgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.IchiranDgv1.DefaultCellStyle = dataGridViewCellStyle2;
            this.IchiranDgv1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.IchiranDgv1.EnableHeadersVisualStyles = false;
            this.IchiranDgv1.GridColor = System.Drawing.Color.White;
            this.IchiranDgv1.IsReload = false;
            this.IchiranDgv1.LinkedDataPanelName = "customSortHeader1";
            this.IchiranDgv1.Location = new System.Drawing.Point(3, 146);
            this.IchiranDgv1.MultiSelect = false;
            this.IchiranDgv1.Name = "IchiranDgv1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.IchiranDgv1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.IchiranDgv1.RowHeadersVisible = false;
            this.IchiranDgv1.RowTemplate.Height = 21;
            this.IchiranDgv1.ShowCellToolTips = false;
            this.IchiranDgv1.Size = new System.Drawing.Size(996, 297);
            this.IchiranDgv1.TabIndex = 100;
            this.IchiranDgv1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.IchiranDgv1_CellClick);
            this.IchiranDgv1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.IchiranDgv1_CellPainting);
            this.IchiranDgv1.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.IchiranDgv1_CellValidated);
            // 
            // customPanel3
            // 
            this.customPanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel3.Controls.Add(this.crdbtn_TashaEDI3);
            this.customPanel3.Controls.Add(this.crdbtn_TashaEDI2);
            this.customPanel3.Controls.Add(this.crdbtn_TashaEDI1);
            this.customPanel3.Location = new System.Drawing.Point(153, 0);
            this.customPanel3.Name = "customPanel3";
            this.customPanel3.Size = new System.Drawing.Size(330, 20);
            this.customPanel3.TabIndex = 606;
            // 
            // crdbtn_TashaEDI3
            // 
            this.crdbtn_TashaEDI3.AutoSize = true;
            this.crdbtn_TashaEDI3.DefaultBackColor = System.Drawing.Color.Empty;
            this.crdbtn_TashaEDI3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("crdbtn_TashaEDI3.FocusOutCheckMethod")));
            this.crdbtn_TashaEDI3.LinkedTextBox = "cntb_TashaEDI_KBN";
            this.crdbtn_TashaEDI3.Location = new System.Drawing.Point(262, 1);
            this.crdbtn_TashaEDI3.Name = "crdbtn_TashaEDI3";
            this.crdbtn_TashaEDI3.PopupAfterExecute = null;
            this.crdbtn_TashaEDI3.PopupBeforeExecute = null;
            this.crdbtn_TashaEDI3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("crdbtn_TashaEDI3.PopupSearchSendParams")));
            this.crdbtn_TashaEDI3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.crdbtn_TashaEDI3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("crdbtn_TashaEDI3.popupWindowSetting")));
            this.crdbtn_TashaEDI3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("crdbtn_TashaEDI3.RegistCheckMethod")));
            this.crdbtn_TashaEDI3.Size = new System.Drawing.Size(67, 17);
            this.crdbtn_TashaEDI3.TabIndex = 16;
            this.crdbtn_TashaEDI3.Tag = "";
            this.crdbtn_TashaEDI3.Text = "3.全て";
            this.crdbtn_TashaEDI3.UseVisualStyleBackColor = true;
            this.crdbtn_TashaEDI3.Value = "3";
            // 
            // crdbtn_TashaEDI2
            // 
            this.crdbtn_TashaEDI2.AutoSize = true;
            this.crdbtn_TashaEDI2.DefaultBackColor = System.Drawing.Color.Empty;
            this.crdbtn_TashaEDI2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("crdbtn_TashaEDI2.FocusOutCheckMethod")));
            this.crdbtn_TashaEDI2.LinkedTextBox = "cntb_TashaEDI_KBN";
            this.crdbtn_TashaEDI2.Location = new System.Drawing.Point(126, 0);
            this.crdbtn_TashaEDI2.Name = "crdbtn_TashaEDI2";
            this.crdbtn_TashaEDI2.PopupAfterExecute = null;
            this.crdbtn_TashaEDI2.PopupBeforeExecute = null;
            this.crdbtn_TashaEDI2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("crdbtn_TashaEDI2.PopupSearchSendParams")));
            this.crdbtn_TashaEDI2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.crdbtn_TashaEDI2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("crdbtn_TashaEDI2.popupWindowSetting")));
            this.crdbtn_TashaEDI2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("crdbtn_TashaEDI2.RegistCheckMethod")));
            this.crdbtn_TashaEDI2.Size = new System.Drawing.Size(137, 17);
            this.crdbtn_TashaEDI2.TabIndex = 15;
            this.crdbtn_TashaEDI2.Tag = "";
            this.crdbtn_TashaEDI2.Text = "2.未使用排出表示";
            this.crdbtn_TashaEDI2.UseVisualStyleBackColor = true;
            this.crdbtn_TashaEDI2.Value = "2";
            // 
            // crdbtn_TashaEDI1
            // 
            this.crdbtn_TashaEDI1.AutoSize = true;
            this.crdbtn_TashaEDI1.DefaultBackColor = System.Drawing.Color.Empty;
            this.crdbtn_TashaEDI1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("crdbtn_TashaEDI1.FocusOutCheckMethod")));
            this.crdbtn_TashaEDI1.LinkedTextBox = "cntb_TashaEDI_KBN";
            this.crdbtn_TashaEDI1.Location = new System.Drawing.Point(2, 0);
            this.crdbtn_TashaEDI1.Name = "crdbtn_TashaEDI1";
            this.crdbtn_TashaEDI1.PopupAfterExecute = null;
            this.crdbtn_TashaEDI1.PopupBeforeExecute = null;
            this.crdbtn_TashaEDI1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("crdbtn_TashaEDI1.PopupSearchSendParams")));
            this.crdbtn_TashaEDI1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.crdbtn_TashaEDI1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("crdbtn_TashaEDI1.popupWindowSetting")));
            this.crdbtn_TashaEDI1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("crdbtn_TashaEDI1.RegistCheckMethod")));
            this.crdbtn_TashaEDI1.Size = new System.Drawing.Size(123, 17);
            this.crdbtn_TashaEDI1.TabIndex = 14;
            this.crdbtn_TashaEDI1.Tag = "";
            this.crdbtn_TashaEDI1.Text = "1.使用排出表示";
            this.crdbtn_TashaEDI1.UseVisualStyleBackColor = true;
            this.crdbtn_TashaEDI1.Value = "1";
            // 
            // cntb_TashaEDI_KBN
            // 
            this.cntb_TashaEDI_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.cntb_TashaEDI_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cntb_TashaEDI_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.cntb_TashaEDI_KBN.DisplayItemName = "";
            this.cntb_TashaEDI_KBN.DisplayPopUp = null;
            this.cntb_TashaEDI_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cntb_TashaEDI_KBN.FocusOutCheckMethod")));
            this.cntb_TashaEDI_KBN.ForeColor = System.Drawing.Color.Black;
            this.cntb_TashaEDI_KBN.IsInputErrorOccured = false;
            this.cntb_TashaEDI_KBN.LinkedRadioButtonArray = new string[] {
        "crdbtn_TashaEDI1",
        "crdbtn_TashaEDI2",
        "crdbtn_TashaEDI3"};
            this.cntb_TashaEDI_KBN.Location = new System.Drawing.Point(134, 0);
            this.cntb_TashaEDI_KBN.Name = "cntb_TashaEDI_KBN";
            this.cntb_TashaEDI_KBN.PopupAfterExecute = null;
            this.cntb_TashaEDI_KBN.PopupBeforeExecute = null;
            this.cntb_TashaEDI_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cntb_TashaEDI_KBN.PopupSearchSendParams")));
            this.cntb_TashaEDI_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cntb_TashaEDI_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cntb_TashaEDI_KBN.popupWindowSetting")));
            rangeSettingDto4.Max = new decimal(new int[] {
            3,
            0,
            0,
            0});
            rangeSettingDto4.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cntb_TashaEDI_KBN.RangeSetting = rangeSettingDto4;
            this.cntb_TashaEDI_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cntb_TashaEDI_KBN.RegistCheckMethod")));
            this.cntb_TashaEDI_KBN.Size = new System.Drawing.Size(20, 20);
            this.cntb_TashaEDI_KBN.TabIndex = 0;
            this.cntb_TashaEDI_KBN.Tag = "【１．使用排出表示、２．未使用排出表示、 3.全て】のいずれかを選択してください";
            this.cntb_TashaEDI_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cntb_TashaEDI_KBN.WordWrap = false;
            // 
            // lbl_TashaEDI
            // 
            this.lbl_TashaEDI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_TashaEDI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_TashaEDI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_TashaEDI.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_TashaEDI.ForeColor = System.Drawing.Color.White;
            this.lbl_TashaEDI.Location = new System.Drawing.Point(3, 0);
            this.lbl_TashaEDI.Name = "lbl_TashaEDI";
            this.lbl_TashaEDI.Size = new System.Drawing.Size(126, 20);
            this.lbl_TashaEDI.TabIndex = 607;
            this.lbl_TashaEDI.Text = "他社EDI使用";
            this.lbl_TashaEDI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 507);
            this.Controls.Add(this.customPanel3);
            this.Controls.Add(this.cntb_TashaEDI_KBN);
            this.Controls.Add(this.lbl_TashaEDI);
            this.Controls.Add(this.Jigyoujou_Btn);
            this.Controls.Add(this.Jigyoujou_CD);
            this.Controls.Add(this.JIGYOUBA_NAME);
            this.Controls.Add(this.Jigyouba_Label);
            this.Controls.Add(this.IchiranDgv1);
            this.Controls.Add(this.Unpanba_Btn);
            this.Controls.Add(this.Unpanba_CD);
            this.Controls.Add(this.Unpanba_Name);
            this.Controls.Add(this.Unpanba_Label);
            this.Controls.Add(this.Unpansha_Btn);
            this.Controls.Add(this.Unpansha_CD);
            this.Controls.Add(this.Unpansha_Name);
            this.Controls.Add(this.Unpansha_Label);
            this.Controls.Add(this.Unpan_Btn);
            this.Controls.Add(this.Unpan_CD);
            this.Controls.Add(this.Unpan_Name);
            this.Controls.Add(this.Unpan_Label);
            this.Controls.Add(this.Jigyousha_Btn);
            this.Controls.Add(this.Jigyousya_CD);
            this.Controls.Add(this.JIGYOUSHA_NAME);
            this.Controls.Add(this.Jigyousya_Label);
            this.Controls.Add(this.Syuruyi_Btn);
            this.Controls.Add(this.HAIKI_KBN_CD);
            this.Controls.Add(this.HAIKI_SHURUI_NAME);
            this.Controls.Add(this.HAIKI_LABEL);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ManifestNoTo);
            this.Controls.Add(this.ManifestNoFrom);
            this.Controls.Add(this.ManifestNo_Label);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.SyoriKubun_Label);
            this.Controls.Add(this.Date_Label);
            this.Controls.Add(this.SyoriKubun_CD);
            this.Controls.Add(this.DATE_TO);
            this.Controls.Add(this.DATE_FROM);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Name = "UIForm";
            this.Text = "UIForm";
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.DATE_FROM, 0);
            this.Controls.SetChildIndex(this.DATE_TO, 0);
            this.Controls.SetChildIndex(this.SyoriKubun_CD, 0);
            this.Controls.SetChildIndex(this.Date_Label, 0);
            this.Controls.SetChildIndex(this.SyoriKubun_Label, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.ManifestNo_Label, 0);
            this.Controls.SetChildIndex(this.ManifestNoFrom, 0);
            this.Controls.SetChildIndex(this.ManifestNoTo, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.HAIKI_LABEL, 0);
            this.Controls.SetChildIndex(this.HAIKI_SHURUI_NAME, 0);
            this.Controls.SetChildIndex(this.HAIKI_KBN_CD, 0);
            this.Controls.SetChildIndex(this.Syuruyi_Btn, 0);
            this.Controls.SetChildIndex(this.Jigyousya_Label, 0);
            this.Controls.SetChildIndex(this.JIGYOUSHA_NAME, 0);
            this.Controls.SetChildIndex(this.Jigyousya_CD, 0);
            this.Controls.SetChildIndex(this.Jigyousha_Btn, 0);
            this.Controls.SetChildIndex(this.Unpan_Label, 0);
            this.Controls.SetChildIndex(this.Unpan_Name, 0);
            this.Controls.SetChildIndex(this.Unpan_CD, 0);
            this.Controls.SetChildIndex(this.Unpan_Btn, 0);
            this.Controls.SetChildIndex(this.Unpansha_Label, 0);
            this.Controls.SetChildIndex(this.Unpansha_Name, 0);
            this.Controls.SetChildIndex(this.Unpansha_CD, 0);
            this.Controls.SetChildIndex(this.Unpansha_Btn, 0);
            this.Controls.SetChildIndex(this.Unpanba_Label, 0);
            this.Controls.SetChildIndex(this.Unpanba_Name, 0);
            this.Controls.SetChildIndex(this.Unpanba_CD, 0);
            this.Controls.SetChildIndex(this.Unpanba_Btn, 0);
            this.Controls.SetChildIndex(this.IchiranDgv1, 0);
            this.Controls.SetChildIndex(this.Jigyouba_Label, 0);
            this.Controls.SetChildIndex(this.JIGYOUBA_NAME, 0);
            this.Controls.SetChildIndex(this.Jigyoujou_CD, 0);
            this.Controls.SetChildIndex(this.Jigyoujou_Btn, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.lbl_TashaEDI, 0);
            this.Controls.SetChildIndex(this.cntb_TashaEDI_KBN, 0);
            this.Controls.SetChildIndex(this.customPanel3, 0);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IchiranDgv1)).EndInit();
            this.customPanel3.ResumeLayout(false);
            this.customPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Date_Label;
        private System.Windows.Forms.Label SyoriKubun_Label;
        private r_framework.CustomControl.CustomPanel panel2;
        internal r_framework.CustomControl.CustomRadioButton SyoriKubun_Radio1;
        internal r_framework.CustomControl.CustomRadioButton SyoriKubun_Radio3;
        internal r_framework.CustomControl.CustomRadioButton SyoriKubun_Radio2;
        internal r_framework.CustomControl.CustomDateTimePicker DATE_FROM;
        internal r_framework.CustomControl.CustomDateTimePicker DATE_TO;
        internal r_framework.CustomControl.CustomNumericTextBox2 SyoriKubun_CD;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label ManifestNo_Label;
        internal r_framework.CustomControl.CustomAlphaNumTextBox ManifestNoFrom;
        internal r_framework.CustomControl.CustomAlphaNumTextBox ManifestNoTo;
        private System.Windows.Forms.Label label2;
        internal r_framework.CustomControl.CustomPopupOpenButton Syuruyi_Btn;
        public r_framework.CustomControl.CustomAlphaNumTextBox HAIKI_KBN_CD;
        internal r_framework.CustomControl.CustomTextBox HAIKI_SHURUI_NAME;
        internal System.Windows.Forms.Label HAIKI_LABEL;
        internal r_framework.CustomControl.CustomPopupOpenButton Jigyousha_Btn;
        public r_framework.CustomControl.CustomAlphaNumTextBox Jigyousya_CD;
        internal r_framework.CustomControl.CustomTextBox JIGYOUSHA_NAME;
        internal System.Windows.Forms.Label Jigyousya_Label;
        internal r_framework.CustomControl.CustomPopupOpenButton Unpan_Btn;
        public r_framework.CustomControl.CustomAlphaNumTextBox Unpan_CD;
        internal r_framework.CustomControl.CustomTextBox Unpan_Name;
        internal System.Windows.Forms.Label Unpan_Label;
        internal r_framework.CustomControl.CustomPopupOpenButton Unpansha_Btn;
        public r_framework.CustomControl.CustomAlphaNumTextBox Unpansha_CD;
        internal r_framework.CustomControl.CustomTextBox Unpansha_Name;
        internal System.Windows.Forms.Label Unpansha_Label;
        internal r_framework.CustomControl.CustomPopupOpenButton Unpanba_Btn;
        public r_framework.CustomControl.CustomAlphaNumTextBox Unpanba_CD;
        internal r_framework.CustomControl.CustomTextBox Unpanba_Name;
        internal System.Windows.Forms.Label Unpanba_Label;
        internal ShougunUI.APP.DgvCustom1 IchiranDgv1;
        internal r_framework.CustomControl.CustomPopupOpenButton Jigyoujou_Btn;
        public r_framework.CustomControl.CustomAlphaNumTextBox Jigyoujou_CD;
        internal r_framework.CustomControl.CustomTextBox JIGYOUBA_NAME;
        internal System.Windows.Forms.Label Jigyouba_Label;
        private r_framework.CustomControl.CustomPanel customPanel3;
        private r_framework.CustomControl.CustomRadioButton crdbtn_TashaEDI3;
        private r_framework.CustomControl.CustomRadioButton crdbtn_TashaEDI2;
        private r_framework.CustomControl.CustomRadioButton crdbtn_TashaEDI1;
        public r_framework.CustomControl.CustomNumericTextBox2 cntb_TashaEDI_KBN;
        private System.Windows.Forms.Label lbl_TashaEDI;
    }
}