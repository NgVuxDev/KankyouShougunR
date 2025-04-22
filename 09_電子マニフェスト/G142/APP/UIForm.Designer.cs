namespace Shougun.Core.ElectronicManifest.SousinhoryuuTouroku
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
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            this.lbl_Hikiwatasihi = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new r_framework.CustomControl.CustomPanel();
            this.cDtPicker_EndDay = new r_framework.CustomControl.CustomDateTimePicker();
            this.cDtPicker_StartDay = new r_framework.CustomControl.CustomDateTimePicker();
            this.lbl_Manifesutobangou = new System.Windows.Forms.Label();
            this.panel2 = new r_framework.CustomControl.CustomPanel();
            this.cntxt_JyoutaikunbunCd = new r_framework.CustomControl.CustomNumericTextBox2();
            this.customRadioButton5 = new r_framework.CustomControl.CustomRadioButton();
            this.customRadioButton4 = new r_framework.CustomControl.CustomRadioButton();
            this.customRadioButton3 = new r_framework.CustomControl.CustomRadioButton();
            this.customRadioButton2 = new r_framework.CustomControl.CustomRadioButton();
            this.customRadioButton1 = new r_framework.CustomControl.CustomRadioButton();
            this.lbl_ManifesutoFlg = new System.Windows.Forms.Label();
            this.customPanel2 = new r_framework.CustomControl.CustomPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.Manifesutobangou_To = new r_framework.CustomControl.CustomNumericTextBox2();
            this.Manifesutobangou_From = new r_framework.CustomControl.CustomNumericTextBox2();
            this.customRadioButton10 = new r_framework.CustomControl.CustomRadioButton();
            this.customRadioButton9 = new r_framework.CustomControl.CustomRadioButton();
            this.customRadioButton8 = new r_framework.CustomControl.CustomRadioButton();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.cntxt_ManifesutokubunCd = new r_framework.CustomControl.CustomNumericTextBox2();
            this.ctxt_HaisyutujigyousyaName = new r_framework.CustomControl.CustomTextBox();
            this.lbl_Haisyutujigyousya = new System.Windows.Forms.Label();
            this.cantxt_Renrakubangou1 = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.lbl_Renrakubangou1 = new System.Windows.Forms.Label();
            this.lbl_jyoutaikubun = new System.Windows.Forms.Label();
            this.cantxt_HaisyutuGyoushaCd = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.casbtn_HaisyutuGyousyaName = new r_framework.CustomControl.CustomPopupOpenButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.customPanel2.SuspendLayout();
            this.customPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.searchString.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.FocusOutCheckMethod")));
            this.searchString.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.searchString.Location = new System.Drawing.Point(1, 74);
            this.searchString.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("searchString.PopupSearchSendParams")));
            this.searchString.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("searchString.popupWindowSetting")));
            this.searchString.ReadOnly = true;
            this.searchString.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.RegistCheckMethod")));
            this.searchString.Size = new System.Drawing.Size(999, 85);
            this.searchString.TabIndex = 30;
            this.searchString.TabStop = false;
            this.searchString.Tag = "検索条件設定画面で設定した条件が表示されます";
            this.searchString.Visible = false;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn1.Location = new System.Drawing.Point(1, 460);
            this.bt_ptn1.Size = new System.Drawing.Size(198, 22);
            this.bt_ptn1.TabIndex = 33;
            this.bt_ptn1.Text = "パターン1";
            this.bt_ptn1.Click += new System.EventHandler(this.bt_ptn1_Click);
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn2.Location = new System.Drawing.Point(201, 460);
            this.bt_ptn2.Size = new System.Drawing.Size(198, 22);
            this.bt_ptn2.TabIndex = 34;
            this.bt_ptn2.Text = "パターン2";
            this.bt_ptn2.Click += new System.EventHandler(this.bt_ptn2_Click);
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn3.Location = new System.Drawing.Point(401, 460);
            this.bt_ptn3.Size = new System.Drawing.Size(198, 22);
            this.bt_ptn3.TabIndex = 35;
            this.bt_ptn3.Text = "パターン3";
            this.bt_ptn3.Click += new System.EventHandler(this.bt_ptn3_Click);
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn4.Location = new System.Drawing.Point(601, 460);
            this.bt_ptn4.Size = new System.Drawing.Size(198, 22);
            this.bt_ptn4.TabIndex = 36;
            this.bt_ptn4.Text = "パターン4";
            this.bt_ptn4.Click += new System.EventHandler(this.bt_ptn4_Click);
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn5.Location = new System.Drawing.Point(801, 460);
            this.bt_ptn5.Size = new System.Drawing.Size(198, 22);
            this.bt_ptn5.TabIndex = 37;
            this.bt_ptn5.Text = "パターン5";
            this.bt_ptn5.Click += new System.EventHandler(this.bt_ptn5_Click);
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.Location = new System.Drawing.Point(3, 74);
            this.customSortHeader1.Size = new System.Drawing.Size(997, 24);
            this.customSortHeader1.TabIndex = 31;
            // 
            // lbl_Hikiwatasihi
            // 
            this.lbl_Hikiwatasihi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Hikiwatasihi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Hikiwatasihi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Hikiwatasihi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_Hikiwatasihi.ForeColor = System.Drawing.Color.White;
            this.lbl_Hikiwatasihi.Location = new System.Drawing.Point(1, 25);
            this.lbl_Hikiwatasihi.Name = "lbl_Hikiwatasihi";
            this.lbl_Hikiwatasihi.Size = new System.Drawing.Size(125, 20);
            this.lbl_Hikiwatasihi.TabIndex = 10;
            this.lbl_Hikiwatasihi.Text = "引渡し日";
            this.lbl_Hikiwatasihi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(165, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "～";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cDtPicker_EndDay);
            this.panel1.Controls.Add(this.cDtPicker_StartDay);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(131, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(343, 20);
            this.panel1.TabIndex = 11;
            // 
            // cDtPicker_EndDay
            // 
            this.cDtPicker_EndDay.BackColor = System.Drawing.SystemColors.Window;
            this.cDtPicker_EndDay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cDtPicker_EndDay.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.cDtPicker_EndDay.Checked = false;
            this.cDtPicker_EndDay.CustomFormat = "yyyy/MM/dd(ddd)";
            this.cDtPicker_EndDay.DateTimeNowYear = "";
            this.cDtPicker_EndDay.DefaultBackColor = System.Drawing.Color.Empty;
            this.cDtPicker_EndDay.DisplayItemName = "引渡し日TO";
            this.cDtPicker_EndDay.DisplayPopUp = null;
            this.cDtPicker_EndDay.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cDtPicker_EndDay.FocusOutCheckMethod")));
            this.cDtPicker_EndDay.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cDtPicker_EndDay.ForeColor = System.Drawing.Color.Black;
            this.cDtPicker_EndDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cDtPicker_EndDay.IsInputErrorOccured = false;
            this.cDtPicker_EndDay.Location = new System.Drawing.Point(205, 0);
            this.cDtPicker_EndDay.MaxLength = 10;
            this.cDtPicker_EndDay.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.cDtPicker_EndDay.Name = "cDtPicker_EndDay";
            this.cDtPicker_EndDay.NullValue = "";
            this.cDtPicker_EndDay.PopupAfterExecute = null;
            this.cDtPicker_EndDay.PopupBeforeExecute = null;
            this.cDtPicker_EndDay.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cDtPicker_EndDay.PopupSearchSendParams")));
            this.cDtPicker_EndDay.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cDtPicker_EndDay.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cDtPicker_EndDay.popupWindowSetting")));
            this.cDtPicker_EndDay.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cDtPicker_EndDay.RegistCheckMethod")));
            this.cDtPicker_EndDay.Size = new System.Drawing.Size(138, 20);
            this.cDtPicker_EndDay.TabIndex = 14;
            this.cDtPicker_EndDay.Tag = "日付を選択してください";
            this.cDtPicker_EndDay.Text = "2013/12/10(火)";
            this.cDtPicker_EndDay.Value = new System.DateTime(2013, 12, 10, 0, 0, 0, 0);
            this.cDtPicker_EndDay.Leave += new System.EventHandler(this.cDtPicker_EndDay_Leave);
            // 
            // cDtPicker_StartDay
            // 
            this.cDtPicker_StartDay.BackColor = System.Drawing.SystemColors.Window;
            this.cDtPicker_StartDay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cDtPicker_StartDay.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.cDtPicker_StartDay.Checked = false;
            this.cDtPicker_StartDay.CustomFormat = "yyyy/MM/dd(ddd)";
            this.cDtPicker_StartDay.DateTimeNowYear = "";
            this.cDtPicker_StartDay.DefaultBackColor = System.Drawing.Color.Empty;
            this.cDtPicker_StartDay.DisplayItemName = "引渡し日FROM";
            this.cDtPicker_StartDay.DisplayPopUp = null;
            this.cDtPicker_StartDay.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cDtPicker_StartDay.FocusOutCheckMethod")));
            this.cDtPicker_StartDay.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cDtPicker_StartDay.ForeColor = System.Drawing.Color.Black;
            this.cDtPicker_StartDay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cDtPicker_StartDay.IsInputErrorOccured = false;
            this.cDtPicker_StartDay.Location = new System.Drawing.Point(0, 0);
            this.cDtPicker_StartDay.MaxLength = 10;
            this.cDtPicker_StartDay.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.cDtPicker_StartDay.Name = "cDtPicker_StartDay";
            this.cDtPicker_StartDay.NullValue = "";
            this.cDtPicker_StartDay.PopupAfterExecute = null;
            this.cDtPicker_StartDay.PopupBeforeExecute = null;
            this.cDtPicker_StartDay.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cDtPicker_StartDay.PopupSearchSendParams")));
            this.cDtPicker_StartDay.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cDtPicker_StartDay.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cDtPicker_StartDay.popupWindowSetting")));
            this.cDtPicker_StartDay.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cDtPicker_StartDay.RegistCheckMethod")));
            this.cDtPicker_StartDay.Size = new System.Drawing.Size(138, 20);
            this.cDtPicker_StartDay.TabIndex = 12;
            this.cDtPicker_StartDay.Tag = "日付を選択してください";
            this.cDtPicker_StartDay.Text = "2013/12/10(火)";
            this.cDtPicker_StartDay.Value = new System.DateTime(2013, 12, 10, 0, 0, 0, 0);
            this.cDtPicker_StartDay.Leave += new System.EventHandler(this.cDtPicker_StartDay_Leave);
            // 
            // lbl_Manifesutobangou
            // 
            this.lbl_Manifesutobangou.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Manifesutobangou.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Manifesutobangou.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Manifesutobangou.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_Manifesutobangou.ForeColor = System.Drawing.Color.White;
            this.lbl_Manifesutobangou.Location = new System.Drawing.Point(1, 47);
            this.lbl_Manifesutobangou.Name = "lbl_Manifesutobangou";
            this.lbl_Manifesutobangou.Size = new System.Drawing.Size(125, 20);
            this.lbl_Manifesutobangou.TabIndex = 17;
            this.lbl_Manifesutobangou.Text = "マニフェスト番号";
            this.lbl_Manifesutobangou.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.cntxt_JyoutaikunbunCd);
            this.panel2.Controls.Add(this.customRadioButton5);
            this.panel2.Controls.Add(this.customRadioButton4);
            this.panel2.Controls.Add(this.customRadioButton3);
            this.panel2.Controls.Add(this.customRadioButton2);
            this.panel2.Controls.Add(this.customRadioButton1);
            this.panel2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.panel2.Location = new System.Drawing.Point(615, 47);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(385, 20);
            this.panel2.TabIndex = 23;
            // 
            // cntxt_JyoutaikunbunCd
            // 
            this.cntxt_JyoutaikunbunCd.BackColor = System.Drawing.SystemColors.Window;
            this.cntxt_JyoutaikunbunCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cntxt_JyoutaikunbunCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.cntxt_JyoutaikunbunCd.DisplayPopUp = null;
            this.cntxt_JyoutaikunbunCd.FocusOutCheckMethod = null;
            this.cntxt_JyoutaikunbunCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cntxt_JyoutaikunbunCd.ForeColor = System.Drawing.Color.Black;
            this.cntxt_JyoutaikunbunCd.IsInputErrorOccured = false;
            this.cntxt_JyoutaikunbunCd.LinkedRadioButtonArray = new string[] {
        "customRadioButton1",
        "customRadioButton2",
        "customRadioButton3",
        "customRadioButton4",
        "customRadioButton5"};
            this.cntxt_JyoutaikunbunCd.Location = new System.Drawing.Point(-1, -1);
            this.cntxt_JyoutaikunbunCd.Name = "cntxt_JyoutaikunbunCd";
            this.cntxt_JyoutaikunbunCd.PopupAfterExecute = null;
            this.cntxt_JyoutaikunbunCd.PopupBeforeExecute = null;
            this.cntxt_JyoutaikunbunCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cntxt_JyoutaikunbunCd.PopupSearchSendParams")));
            this.cntxt_JyoutaikunbunCd.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cntxt_JyoutaikunbunCd.popupWindowSetting = null;
            rangeSettingDto1.Max = new decimal(new int[] {
            5,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cntxt_JyoutaikunbunCd.RangeSetting = rangeSettingDto1;
            this.cntxt_JyoutaikunbunCd.RegistCheckMethod = null;
            this.cntxt_JyoutaikunbunCd.Size = new System.Drawing.Size(23, 20);
            this.cntxt_JyoutaikunbunCd.TabIndex = 24;
            this.cntxt_JyoutaikunbunCd.Tag = "状態区分を選択してください";
            this.cntxt_JyoutaikunbunCd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cntxt_JyoutaikunbunCd.WordWrap = false;
            // 
            // customRadioButton5
            // 
            this.customRadioButton5.AutoSize = true;
            this.customRadioButton5.DefaultBackColor = System.Drawing.Color.Empty;
            this.customRadioButton5.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customRadioButton5.FocusOutCheckMethod")));
            this.customRadioButton5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.customRadioButton5.LinkedTextBox = "cntxt_JyoutaikunbunCd";
            this.customRadioButton5.Location = new System.Drawing.Point(285, 1);
            this.customRadioButton5.Name = "customRadioButton5";
            this.customRadioButton5.PopupAfterExecute = null;
            this.customRadioButton5.PopupBeforeExecute = null;
            this.customRadioButton5.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customRadioButton5.PopupSearchSendParams")));
            this.customRadioButton5.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.customRadioButton5.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customRadioButton5.popupWindowSetting")));
            this.customRadioButton5.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customRadioButton5.RegistCheckMethod")));
            this.customRadioButton5.Size = new System.Drawing.Size(95, 17);
            this.customRadioButton5.TabIndex = 29;
            this.customRadioButton5.Tag = "状態区分を選択してください";
            this.customRadioButton5.Text = "5.予約確定";
            this.customRadioButton5.UseVisualStyleBackColor = true;
            this.customRadioButton5.Value = "5";
            // 
            // customRadioButton4
            // 
            this.customRadioButton4.AutoSize = true;
            this.customRadioButton4.DefaultBackColor = System.Drawing.Color.Empty;
            this.customRadioButton4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customRadioButton4.FocusOutCheckMethod")));
            this.customRadioButton4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.customRadioButton4.LinkedTextBox = "cntxt_JyoutaikunbunCd";
            this.customRadioButton4.Location = new System.Drawing.Point(221, 1);
            this.customRadioButton4.Name = "customRadioButton4";
            this.customRadioButton4.PopupAfterExecute = null;
            this.customRadioButton4.PopupBeforeExecute = null;
            this.customRadioButton4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customRadioButton4.PopupSearchSendParams")));
            this.customRadioButton4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.customRadioButton4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customRadioButton4.popupWindowSetting")));
            this.customRadioButton4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customRadioButton4.RegistCheckMethod")));
            this.customRadioButton4.Size = new System.Drawing.Size(67, 17);
            this.customRadioButton4.TabIndex = 28;
            this.customRadioButton4.Tag = "状態区分を選択してください";
            this.customRadioButton4.Text = "4.取消";
            this.customRadioButton4.UseVisualStyleBackColor = true;
            this.customRadioButton4.Value = "4";
            // 
            // customRadioButton3
            // 
            this.customRadioButton3.AutoSize = true;
            this.customRadioButton3.DefaultBackColor = System.Drawing.Color.Empty;
            this.customRadioButton3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customRadioButton3.FocusOutCheckMethod")));
            this.customRadioButton3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.customRadioButton3.LinkedTextBox = "cntxt_JyoutaikunbunCd";
            this.customRadioButton3.Location = new System.Drawing.Point(157, 1);
            this.customRadioButton3.Name = "customRadioButton3";
            this.customRadioButton3.PopupAfterExecute = null;
            this.customRadioButton3.PopupBeforeExecute = null;
            this.customRadioButton3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customRadioButton3.PopupSearchSendParams")));
            this.customRadioButton3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.customRadioButton3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customRadioButton3.popupWindowSetting")));
            this.customRadioButton3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customRadioButton3.RegistCheckMethod")));
            this.customRadioButton3.Size = new System.Drawing.Size(67, 17);
            this.customRadioButton3.TabIndex = 27;
            this.customRadioButton3.Tag = "状態区分を選択してください";
            this.customRadioButton3.Text = "3.修正";
            this.customRadioButton3.UseVisualStyleBackColor = true;
            this.customRadioButton3.Value = "3";
            // 
            // customRadioButton2
            // 
            this.customRadioButton2.AutoSize = true;
            this.customRadioButton2.DefaultBackColor = System.Drawing.Color.Empty;
            this.customRadioButton2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customRadioButton2.FocusOutCheckMethod")));
            this.customRadioButton2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.customRadioButton2.LinkedTextBox = "cntxt_JyoutaikunbunCd";
            this.customRadioButton2.Location = new System.Drawing.Point(93, 1);
            this.customRadioButton2.Name = "customRadioButton2";
            this.customRadioButton2.PopupAfterExecute = null;
            this.customRadioButton2.PopupBeforeExecute = null;
            this.customRadioButton2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customRadioButton2.PopupSearchSendParams")));
            this.customRadioButton2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.customRadioButton2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customRadioButton2.popupWindowSetting")));
            this.customRadioButton2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customRadioButton2.RegistCheckMethod")));
            this.customRadioButton2.Size = new System.Drawing.Size(67, 17);
            this.customRadioButton2.TabIndex = 26;
            this.customRadioButton2.Tag = "状態区分を選択してください";
            this.customRadioButton2.Text = "2.登録";
            this.customRadioButton2.UseVisualStyleBackColor = true;
            this.customRadioButton2.Value = "2";
            // 
            // customRadioButton1
            // 
            this.customRadioButton1.AutoSize = true;
            this.customRadioButton1.Checked = true;
            this.customRadioButton1.DefaultBackColor = System.Drawing.Color.Empty;
            this.customRadioButton1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customRadioButton1.FocusOutCheckMethod")));
            this.customRadioButton1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.customRadioButton1.LinkedTextBox = "cntxt_JyoutaikunbunCd";
            this.customRadioButton1.Location = new System.Drawing.Point(29, 1);
            this.customRadioButton1.Name = "customRadioButton1";
            this.customRadioButton1.PopupAfterExecute = null;
            this.customRadioButton1.PopupBeforeExecute = null;
            this.customRadioButton1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customRadioButton1.PopupSearchSendParams")));
            this.customRadioButton1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.customRadioButton1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customRadioButton1.popupWindowSetting")));
            this.customRadioButton1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customRadioButton1.RegistCheckMethod")));
            this.customRadioButton1.Size = new System.Drawing.Size(67, 17);
            this.customRadioButton1.TabIndex = 25;
            this.customRadioButton1.Tag = "状態区分を選択してください";
            this.customRadioButton1.Text = "1.全て";
            this.customRadioButton1.UseVisualStyleBackColor = true;
            this.customRadioButton1.Value = "1";
            // 
            // lbl_ManifesutoFlg
            // 
            this.lbl_ManifesutoFlg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_ManifesutoFlg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_ManifesutoFlg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_ManifesutoFlg.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_ManifesutoFlg.ForeColor = System.Drawing.Color.White;
            this.lbl_ManifesutoFlg.Location = new System.Drawing.Point(1, 3);
            this.lbl_ManifesutoFlg.Name = "lbl_ManifesutoFlg";
            this.lbl_ManifesutoFlg.Size = new System.Drawing.Size(125, 20);
            this.lbl_ManifesutoFlg.TabIndex = 0;
            this.lbl_ManifesutoFlg.Text = "マニフェスト区分";
            this.lbl_ManifesutoFlg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel2
            // 
            this.customPanel2.Controls.Add(this.label2);
            this.customPanel2.Controls.Add(this.Manifesutobangou_To);
            this.customPanel2.Controls.Add(this.Manifesutobangou_From);
            this.customPanel2.Location = new System.Drawing.Point(131, 47);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(343, 20);
            this.customPanel2.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(165, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 20);
            this.label2.TabIndex = 20;
            this.label2.Text = "～";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Manifesutobangou_To
            // 
            this.Manifesutobangou_To.BackColor = System.Drawing.SystemColors.Window;
            this.Manifesutobangou_To.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Manifesutobangou_To.CustomFormatSetting = "00000000000";
            this.Manifesutobangou_To.DBFieldsName = "";
            this.Manifesutobangou_To.DefaultBackColor = System.Drawing.Color.Empty;
            this.Manifesutobangou_To.DisplayPopUp = null;
            this.Manifesutobangou_To.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Manifesutobangou_To.FocusOutCheckMethod")));
            this.Manifesutobangou_To.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Manifesutobangou_To.ForeColor = System.Drawing.Color.Black;
            this.Manifesutobangou_To.FormatSetting = "カスタム";
            this.Manifesutobangou_To.GetCodeMasterField = "";
            this.Manifesutobangou_To.IsInputErrorOccured = false;
            this.Manifesutobangou_To.ItemDefinedTypes = "varchar";
            this.Manifesutobangou_To.Location = new System.Drawing.Point(205, 0);
            this.Manifesutobangou_To.Name = "Manifesutobangou_To";
            this.Manifesutobangou_To.PopupAfterExecute = null;
            this.Manifesutobangou_To.PopupBeforeExecute = null;
            this.Manifesutobangou_To.PopupGetMasterField = "";
            this.Manifesutobangou_To.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Manifesutobangou_To.PopupSearchSendParams")));
            this.Manifesutobangou_To.PopupSetFormField = "";
            this.Manifesutobangou_To.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Manifesutobangou_To.PopupWindowName = "";
            this.Manifesutobangou_To.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Manifesutobangou_To.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.Manifesutobangou_To.RangeSetting = rangeSettingDto2;
            this.Manifesutobangou_To.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Manifesutobangou_To.RegistCheckMethod")));
            this.Manifesutobangou_To.SetFormField = "";
            this.Manifesutobangou_To.Size = new System.Drawing.Size(138, 20);
            this.Manifesutobangou_To.TabIndex = 21;
            this.Manifesutobangou_To.Tag = "マニフェスト番号を入力してください";
            this.Manifesutobangou_To.WordWrap = false;
            // 
            // Manifesutobangou_From
            // 
            this.Manifesutobangou_From.BackColor = System.Drawing.SystemColors.Window;
            this.Manifesutobangou_From.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Manifesutobangou_From.CustomFormatSetting = "00000000000";
            this.Manifesutobangou_From.DBFieldsName = "";
            this.Manifesutobangou_From.DefaultBackColor = System.Drawing.Color.Empty;
            this.Manifesutobangou_From.DisplayPopUp = null;
            this.Manifesutobangou_From.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Manifesutobangou_From.FocusOutCheckMethod")));
            this.Manifesutobangou_From.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Manifesutobangou_From.ForeColor = System.Drawing.Color.Black;
            this.Manifesutobangou_From.FormatSetting = "カスタム";
            this.Manifesutobangou_From.GetCodeMasterField = "";
            this.Manifesutobangou_From.IsInputErrorOccured = false;
            this.Manifesutobangou_From.ItemDefinedTypes = "varchar";
            this.Manifesutobangou_From.Location = new System.Drawing.Point(0, 0);
            this.Manifesutobangou_From.Name = "Manifesutobangou_From";
            this.Manifesutobangou_From.PopupAfterExecute = null;
            this.Manifesutobangou_From.PopupBeforeExecute = null;
            this.Manifesutobangou_From.PopupGetMasterField = "";
            this.Manifesutobangou_From.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Manifesutobangou_From.PopupSearchSendParams")));
            this.Manifesutobangou_From.PopupSetFormField = "";
            this.Manifesutobangou_From.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Manifesutobangou_From.PopupWindowName = "";
            this.Manifesutobangou_From.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Manifesutobangou_From.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.Manifesutobangou_From.RangeSetting = rangeSettingDto3;
            this.Manifesutobangou_From.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Manifesutobangou_From.RegistCheckMethod")));
            this.Manifesutobangou_From.SetFormField = "";
            this.Manifesutobangou_From.Size = new System.Drawing.Size(138, 20);
            this.Manifesutobangou_From.TabIndex = 19;
            this.Manifesutobangou_From.Tag = "マニフェスト番号を入力してください";
            this.Manifesutobangou_From.WordWrap = false;
            // 
            // customRadioButton10
            // 
            this.customRadioButton10.AutoSize = true;
            this.customRadioButton10.DefaultBackColor = System.Drawing.Color.Empty;
            this.customRadioButton10.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customRadioButton10.FocusOutCheckMethod")));
            this.customRadioButton10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.customRadioButton10.LinkedTextBox = "cntxt_ManifesutokubunCd";
            this.customRadioButton10.Location = new System.Drawing.Point(29, 1);
            this.customRadioButton10.Name = "customRadioButton10";
            this.customRadioButton10.PopupAfterExecute = null;
            this.customRadioButton10.PopupBeforeExecute = null;
            this.customRadioButton10.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customRadioButton10.PopupSearchSendParams")));
            this.customRadioButton10.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.customRadioButton10.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customRadioButton10.popupWindowSetting")));
            this.customRadioButton10.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customRadioButton10.RegistCheckMethod")));
            this.customRadioButton10.Size = new System.Drawing.Size(151, 17);
            this.customRadioButton10.TabIndex = 3;
            this.customRadioButton10.Tag = "マニフェスト区分を選択してください";
            this.customRadioButton10.Text = "1.マニフェスト情報";
            this.customRadioButton10.UseVisualStyleBackColor = true;
            this.customRadioButton10.Value = "1";
            // 
            // customRadioButton9
            // 
            this.customRadioButton9.AutoSize = true;
            this.customRadioButton9.DefaultBackColor = System.Drawing.Color.Empty;
            this.customRadioButton9.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customRadioButton9.FocusOutCheckMethod")));
            this.customRadioButton9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.customRadioButton9.LinkedTextBox = "cntxt_ManifesutokubunCd";
            this.customRadioButton9.Location = new System.Drawing.Point(179, 1);
            this.customRadioButton9.Name = "customRadioButton9";
            this.customRadioButton9.PopupAfterExecute = null;
            this.customRadioButton9.PopupBeforeExecute = null;
            this.customRadioButton9.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customRadioButton9.PopupSearchSendParams")));
            this.customRadioButton9.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.customRadioButton9.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customRadioButton9.popupWindowSetting")));
            this.customRadioButton9.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customRadioButton9.RegistCheckMethod")));
            this.customRadioButton9.Size = new System.Drawing.Size(95, 17);
            this.customRadioButton9.TabIndex = 4;
            this.customRadioButton9.Tag = "マニフェスト区分を選択してください";
            this.customRadioButton9.Text = "2.予約情報";
            this.customRadioButton9.UseVisualStyleBackColor = true;
            this.customRadioButton9.Value = "2";
            // 
            // customRadioButton8
            // 
            this.customRadioButton8.AutoSize = true;
            this.customRadioButton8.DefaultBackColor = System.Drawing.Color.Empty;
            this.customRadioButton8.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customRadioButton8.FocusOutCheckMethod")));
            this.customRadioButton8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.customRadioButton8.LinkedTextBox = "cntxt_ManifesutokubunCd";
            this.customRadioButton8.Location = new System.Drawing.Point(272, 1);
            this.customRadioButton8.Name = "customRadioButton8";
            this.customRadioButton8.PopupAfterExecute = null;
            this.customRadioButton8.PopupBeforeExecute = null;
            this.customRadioButton8.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customRadioButton8.PopupSearchSendParams")));
            this.customRadioButton8.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.customRadioButton8.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customRadioButton8.popupWindowSetting")));
            this.customRadioButton8.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customRadioButton8.RegistCheckMethod")));
            this.customRadioButton8.Size = new System.Drawing.Size(67, 17);
            this.customRadioButton8.TabIndex = 5;
            this.customRadioButton8.Tag = "マニフェスト区分を選択してください";
            this.customRadioButton8.Text = "3.全て";
            this.customRadioButton8.UseVisualStyleBackColor = true;
            this.customRadioButton8.Value = "3";
            // 
            // customPanel1
            // 
            this.customPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel1.Controls.Add(this.cntxt_ManifesutokubunCd);
            this.customPanel1.Controls.Add(this.customRadioButton8);
            this.customPanel1.Controls.Add(this.customRadioButton9);
            this.customPanel1.Controls.Add(this.customRadioButton10);
            this.customPanel1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.customPanel1.Location = new System.Drawing.Point(131, 3);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(343, 20);
            this.customPanel1.TabIndex = 1;
            // 
            // cntxt_ManifesutokubunCd
            // 
            this.cntxt_ManifesutokubunCd.BackColor = System.Drawing.SystemColors.Window;
            this.cntxt_ManifesutokubunCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cntxt_ManifesutokubunCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.cntxt_ManifesutokubunCd.DisplayPopUp = null;
            this.cntxt_ManifesutokubunCd.FocusOutCheckMethod = null;
            this.cntxt_ManifesutokubunCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cntxt_ManifesutokubunCd.ForeColor = System.Drawing.Color.Black;
            this.cntxt_ManifesutokubunCd.IsInputErrorOccured = false;
            this.cntxt_ManifesutokubunCd.LinkedRadioButtonArray = new string[] {
        "customRadioButton10",
        "customRadioButton9",
        "customRadioButton8"};
            this.cntxt_ManifesutokubunCd.Location = new System.Drawing.Point(-1, -1);
            this.cntxt_ManifesutokubunCd.Name = "cntxt_ManifesutokubunCd";
            this.cntxt_ManifesutokubunCd.PopupAfterExecute = null;
            this.cntxt_ManifesutokubunCd.PopupBeforeExecute = null;
            this.cntxt_ManifesutokubunCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cntxt_ManifesutokubunCd.PopupSearchSendParams")));
            this.cntxt_ManifesutokubunCd.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cntxt_ManifesutokubunCd.popupWindowSetting = null;
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
            this.cntxt_ManifesutokubunCd.RangeSetting = rangeSettingDto4;
            this.cntxt_ManifesutokubunCd.RegistCheckMethod = null;
            this.cntxt_ManifesutokubunCd.Size = new System.Drawing.Size(23, 20);
            this.cntxt_ManifesutokubunCd.TabIndex = 2;
            this.cntxt_ManifesutokubunCd.Tag = "マニフェスト区分を選択してください";
            this.cntxt_ManifesutokubunCd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cntxt_ManifesutokubunCd.WordWrap = false;
            // 
            // ctxt_HaisyutujigyousyaName
            // 
            this.ctxt_HaisyutujigyousyaName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_HaisyutujigyousyaName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_HaisyutujigyousyaName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ctxt_HaisyutujigyousyaName.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_HaisyutujigyousyaName.DisplayPopUp = null;
            this.ctxt_HaisyutujigyousyaName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_HaisyutujigyousyaName.FocusOutCheckMethod")));
            this.ctxt_HaisyutujigyousyaName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ctxt_HaisyutujigyousyaName.ForeColor = System.Drawing.Color.Black;
            this.ctxt_HaisyutujigyousyaName.IsInputErrorOccured = false;
            this.ctxt_HaisyutujigyousyaName.Location = new System.Drawing.Point(682, 3);
            this.ctxt_HaisyutujigyousyaName.MaxLength = 0;
            this.ctxt_HaisyutujigyousyaName.Name = "ctxt_HaisyutujigyousyaName";
            this.ctxt_HaisyutujigyousyaName.PopupAfterExecute = null;
            this.ctxt_HaisyutujigyousyaName.PopupBeforeExecute = null;
            this.ctxt_HaisyutujigyousyaName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_HaisyutujigyousyaName.PopupSearchSendParams")));
            this.ctxt_HaisyutujigyousyaName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_HaisyutujigyousyaName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_HaisyutujigyousyaName.popupWindowSetting")));
            this.ctxt_HaisyutujigyousyaName.ReadOnly = true;
            this.ctxt_HaisyutujigyousyaName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_HaisyutujigyousyaName.RegistCheckMethod")));
            this.ctxt_HaisyutujigyousyaName.Size = new System.Drawing.Size(155, 20);
            this.ctxt_HaisyutujigyousyaName.TabIndex = 8;
            this.ctxt_HaisyutujigyousyaName.TabStop = false;
            this.ctxt_HaisyutujigyousyaName.Tag = " ";
            // 
            // lbl_Haisyutujigyousya
            // 
            this.lbl_Haisyutujigyousya.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Haisyutujigyousya.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Haisyutujigyousya.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Haisyutujigyousya.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_Haisyutujigyousya.ForeColor = System.Drawing.Color.White;
            this.lbl_Haisyutujigyousya.Location = new System.Drawing.Point(500, 3);
            this.lbl_Haisyutujigyousya.Name = "lbl_Haisyutujigyousya";
            this.lbl_Haisyutujigyousya.Size = new System.Drawing.Size(110, 20);
            this.lbl_Haisyutujigyousya.TabIndex = 6;
            this.lbl_Haisyutujigyousya.Text = "排出事業者";
            this.lbl_Haisyutujigyousya.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cantxt_Renrakubangou1
            // 
            this.cantxt_Renrakubangou1.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_Renrakubangou1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_Renrakubangou1.CharacterLimitList = null;
            this.cantxt_Renrakubangou1.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.cantxt_Renrakubangou1.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_Renrakubangou1.DisplayPopUp = null;
            this.cantxt_Renrakubangou1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_Renrakubangou1.FocusOutCheckMethod")));
            this.cantxt_Renrakubangou1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cantxt_Renrakubangou1.ForeColor = System.Drawing.Color.Black;
            this.cantxt_Renrakubangou1.GetCodeMasterField = "";
            this.cantxt_Renrakubangou1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cantxt_Renrakubangou1.IsInputErrorOccured = false;
            this.cantxt_Renrakubangou1.Location = new System.Drawing.Point(615, 25);
            this.cantxt_Renrakubangou1.MaxLength = 20;
            this.cantxt_Renrakubangou1.Name = "cantxt_Renrakubangou1";
            this.cantxt_Renrakubangou1.PopupAfterExecute = null;
            this.cantxt_Renrakubangou1.PopupBeforeExecute = null;
            this.cantxt_Renrakubangou1.PopupGetMasterField = "";
            this.cantxt_Renrakubangou1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_Renrakubangou1.PopupSearchSendParams")));
            this.cantxt_Renrakubangou1.PopupSetFormField = "";
            this.cantxt_Renrakubangou1.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
            this.cantxt_Renrakubangou1.PopupWindowName = "";
            this.cantxt_Renrakubangou1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_Renrakubangou1.popupWindowSetting")));
            this.cantxt_Renrakubangou1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_Renrakubangou1.RegistCheckMethod")));
            this.cantxt_Renrakubangou1.SetFormField = "";
            this.cantxt_Renrakubangou1.Size = new System.Drawing.Size(222, 20);
            this.cantxt_Renrakubangou1.TabIndex = 16;
            this.cantxt_Renrakubangou1.Tag = "連絡番号1を入力してください";
            // 
            // lbl_Renrakubangou1
            // 
            this.lbl_Renrakubangou1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Renrakubangou1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Renrakubangou1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Renrakubangou1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_Renrakubangou1.ForeColor = System.Drawing.Color.White;
            this.lbl_Renrakubangou1.Location = new System.Drawing.Point(500, 25);
            this.lbl_Renrakubangou1.Name = "lbl_Renrakubangou1";
            this.lbl_Renrakubangou1.Size = new System.Drawing.Size(110, 20);
            this.lbl_Renrakubangou1.TabIndex = 15;
            this.lbl_Renrakubangou1.Text = "連絡番号1";
            this.lbl_Renrakubangou1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_jyoutaikubun
            // 
            this.lbl_jyoutaikubun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_jyoutaikubun.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_jyoutaikubun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_jyoutaikubun.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_jyoutaikubun.ForeColor = System.Drawing.Color.White;
            this.lbl_jyoutaikubun.Location = new System.Drawing.Point(500, 47);
            this.lbl_jyoutaikubun.Name = "lbl_jyoutaikubun";
            this.lbl_jyoutaikubun.Size = new System.Drawing.Size(110, 20);
            this.lbl_jyoutaikubun.TabIndex = 22;
            this.lbl_jyoutaikubun.Text = "状態区分※";
            this.lbl_jyoutaikubun.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cantxt_HaisyutuGyoushaCd
            // 
            this.cantxt_HaisyutuGyoushaCd.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_HaisyutuGyoushaCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_HaisyutuGyoushaCd.ChangeUpperCase = true;
            this.cantxt_HaisyutuGyoushaCd.CharacterLimitList = null;
            this.cantxt_HaisyutuGyoushaCd.CharactersNumber = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.cantxt_HaisyutuGyoushaCd.CustomFormatSetting = "0000000";
            this.cantxt_HaisyutuGyoushaCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_HaisyutuGyoushaCd.DisplayPopUp = null;
            this.cantxt_HaisyutuGyoushaCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_HaisyutuGyoushaCd.FocusOutCheckMethod")));
            this.cantxt_HaisyutuGyoushaCd.ForeColor = System.Drawing.Color.Black;
            this.cantxt_HaisyutuGyoushaCd.FormatSetting = "カスタム";
            this.cantxt_HaisyutuGyoushaCd.GetCodeMasterField = "EDI_MEMBER_ID,JIGYOUSHA_NAME";
            this.cantxt_HaisyutuGyoushaCd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cantxt_HaisyutuGyoushaCd.IsInputErrorOccured = false;
            this.cantxt_HaisyutuGyoushaCd.Location = new System.Drawing.Point(615, 3);
            this.cantxt_HaisyutuGyoushaCd.MaxLength = 7;
            this.cantxt_HaisyutuGyoushaCd.Name = "cantxt_HaisyutuGyoushaCd";
            this.cantxt_HaisyutuGyoushaCd.PopupAfterExecute = null;
            this.cantxt_HaisyutuGyoushaCd.PopupAfterExecuteMethod = "";
            this.cantxt_HaisyutuGyoushaCd.PopupBeforeExecute = null;
            this.cantxt_HaisyutuGyoushaCd.PopupGetMasterField = "EDI_MEMBER_ID,JIGYOUSHA_NAME";
            this.cantxt_HaisyutuGyoushaCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_HaisyutuGyoushaCd.PopupSearchSendParams")));
            this.cantxt_HaisyutuGyoushaCd.PopupSetFormField = "cantxt_HaisyutuGyoushaCd,ctxt_HaisyutujigyousyaName";
            this.cantxt_HaisyutuGyoushaCd.PopupWindowId = r_framework.Const.WINDOW_ID.M_DENSHI_JIGYOUSHA;
            this.cantxt_HaisyutuGyoushaCd.PopupWindowName = "マスタ共通ポップアップ";
            this.cantxt_HaisyutuGyoushaCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_HaisyutuGyoushaCd.popupWindowSetting")));
            this.cantxt_HaisyutuGyoushaCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_HaisyutuGyoushaCd.RegistCheckMethod")));
            this.cantxt_HaisyutuGyoushaCd.SetFormField = "cantxt_HaisyutuGyoushaCd,ctxt_HaisyutujigyousyaName";
            this.cantxt_HaisyutuGyoushaCd.Size = new System.Drawing.Size(68, 20);
            this.cantxt_HaisyutuGyoushaCd.TabIndex = 7;
            this.cantxt_HaisyutuGyoushaCd.Tag = "排出事業者CDを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.cantxt_HaisyutuGyoushaCd.ZeroPaddengFlag = true;
            this.cantxt_HaisyutuGyoushaCd.Validating += new System.ComponentModel.CancelEventHandler(this.cantxt_HaisyutuGyoushaCd_Validating);
            // 
            // casbtn_HaisyutuGyousyaName
            // 
            this.casbtn_HaisyutuGyousyaName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.casbtn_HaisyutuGyousyaName.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.casbtn_HaisyutuGyousyaName.DBFieldsName = null;
            this.casbtn_HaisyutuGyousyaName.DefaultBackColor = System.Drawing.Color.Empty;
            this.casbtn_HaisyutuGyousyaName.DisplayItemName = null;
            this.casbtn_HaisyutuGyousyaName.DisplayPopUp = null;
            this.casbtn_HaisyutuGyousyaName.ErrorMessage = null;
            this.casbtn_HaisyutuGyousyaName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("casbtn_HaisyutuGyousyaName.FocusOutCheckMethod")));
            this.casbtn_HaisyutuGyousyaName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.casbtn_HaisyutuGyousyaName.GetCodeMasterField = null;
            this.casbtn_HaisyutuGyousyaName.Image = ((System.Drawing.Image)(resources.GetObject("casbtn_HaisyutuGyousyaName.Image")));
            this.casbtn_HaisyutuGyousyaName.ItemDefinedTypes = null;
            this.casbtn_HaisyutuGyousyaName.LinkedSettingTextBox = null;
            this.casbtn_HaisyutuGyousyaName.LinkedTextBoxs = new string[0];
            this.casbtn_HaisyutuGyousyaName.Location = new System.Drawing.Point(838, 2);
            this.casbtn_HaisyutuGyousyaName.Name = "casbtn_HaisyutuGyousyaName";
            this.casbtn_HaisyutuGyousyaName.PopupAfterExecute = null;
            this.casbtn_HaisyutuGyousyaName.PopupBeforeExecute = null;
            this.casbtn_HaisyutuGyousyaName.PopupGetMasterField = "EDI_MEMBER_ID,JIGYOUSHA_NAME";
            this.casbtn_HaisyutuGyousyaName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("casbtn_HaisyutuGyousyaName.PopupSearchSendParams")));
            this.casbtn_HaisyutuGyousyaName.PopupSetFormField = "cantxt_HaisyutuGyoushaCd,ctxt_HaisyutujigyousyaName";
            this.casbtn_HaisyutuGyousyaName.PopupWindowId = r_framework.Const.WINDOW_ID.M_DENSHI_JIGYOUSHA;
            this.casbtn_HaisyutuGyousyaName.PopupWindowName = "マスタ共通ポップアップ";
            this.casbtn_HaisyutuGyousyaName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("casbtn_HaisyutuGyousyaName.popupWindowSetting")));
            this.casbtn_HaisyutuGyousyaName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("casbtn_HaisyutuGyousyaName.RegistCheckMethod")));
            this.casbtn_HaisyutuGyousyaName.SearchDisplayFlag = 0;
            this.casbtn_HaisyutuGyousyaName.SetFormField = "";
            this.casbtn_HaisyutuGyousyaName.ShortItemName = null;
            this.casbtn_HaisyutuGyousyaName.Size = new System.Drawing.Size(22, 22);
            this.casbtn_HaisyutuGyousyaName.TabIndex = 9;
            this.casbtn_HaisyutuGyousyaName.TabStop = false;
            this.casbtn_HaisyutuGyousyaName.Tag = "排出事業者の検索を行います";
            this.casbtn_HaisyutuGyousyaName.Text = " ";
            this.casbtn_HaisyutuGyousyaName.UseVisualStyleBackColor = false;
            this.casbtn_HaisyutuGyousyaName.ZeroPaddengFlag = false;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 490);
            this.Controls.Add(this.casbtn_HaisyutuGyousyaName);
            this.Controls.Add(this.cantxt_HaisyutuGyoushaCd);
            this.Controls.Add(this.lbl_jyoutaikubun);
            this.Controls.Add(this.cantxt_Renrakubangou1);
            this.Controls.Add(this.lbl_Renrakubangou1);
            this.Controls.Add(this.ctxt_HaisyutujigyousyaName);
            this.Controls.Add(this.lbl_Haisyutujigyousya);
            this.Controls.Add(this.customPanel2);
            this.Controls.Add(this.customPanel1);
            this.Controls.Add(this.lbl_ManifesutoFlg);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lbl_Manifesutobangou);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbl_Hikiwatasihi);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Name = "UIForm";
            this.Text = "UIForm";
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.lbl_Hikiwatasihi, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.lbl_Manifesutobangou, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.lbl_ManifesutoFlg, 0);
            this.Controls.SetChildIndex(this.customPanel1, 0);
            this.Controls.SetChildIndex(this.customPanel2, 0);
            this.Controls.SetChildIndex(this.lbl_Haisyutujigyousya, 0);
            this.Controls.SetChildIndex(this.ctxt_HaisyutujigyousyaName, 0);
            this.Controls.SetChildIndex(this.lbl_Renrakubangou1, 0);
            this.Controls.SetChildIndex(this.cantxt_Renrakubangou1, 0);
            this.Controls.SetChildIndex(this.lbl_jyoutaikubun, 0);
            this.Controls.SetChildIndex(this.cantxt_HaisyutuGyoushaCd, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.casbtn_HaisyutuGyousyaName, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.customPanel2.ResumeLayout(false);
            this.customPanel2.PerformLayout();
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Hikiwatasihi;
        private System.Windows.Forms.Label label3;
        private r_framework.CustomControl.CustomPanel panel1;
        private System.Windows.Forms.Label lbl_Manifesutobangou;
        private r_framework.CustomControl.CustomPanel panel2;
        internal r_framework.CustomControl.CustomRadioButton customRadioButton1;
        internal r_framework.CustomControl.CustomRadioButton customRadioButton5;
        internal r_framework.CustomControl.CustomRadioButton customRadioButton4;
        internal r_framework.CustomControl.CustomRadioButton customRadioButton3;
        internal r_framework.CustomControl.CustomRadioButton customRadioButton2;
        private System.Windows.Forms.Label lbl_ManifesutoFlg;
        private r_framework.CustomControl.CustomPanel customPanel2;
        internal r_framework.CustomControl.CustomRadioButton customRadioButton10;
        internal r_framework.CustomControl.CustomRadioButton customRadioButton9;
        internal r_framework.CustomControl.CustomRadioButton customRadioButton8;
        private r_framework.CustomControl.CustomPanel customPanel1;
        public r_framework.CustomControl.CustomTextBox ctxt_HaisyutujigyousyaName;
        private System.Windows.Forms.Label lbl_Haisyutujigyousya;
        public r_framework.CustomControl.CustomAlphaNumTextBox cantxt_Renrakubangou1;
        private System.Windows.Forms.Label lbl_Renrakubangou1;
        private System.Windows.Forms.Label lbl_jyoutaikubun;
        internal r_framework.CustomControl.CustomDateTimePicker cDtPicker_EndDay;
        internal r_framework.CustomControl.CustomDateTimePicker cDtPicker_StartDay;
        private System.Windows.Forms.Label label2;
        internal r_framework.CustomControl.CustomNumericTextBox2 Manifesutobangou_To;
        internal r_framework.CustomControl.CustomNumericTextBox2 Manifesutobangou_From;
        internal r_framework.CustomControl.CustomAlphaNumTextBox cantxt_HaisyutuGyoushaCd;
        internal r_framework.CustomControl.CustomPopupOpenButton casbtn_HaisyutuGyousyaName;
        internal r_framework.CustomControl.CustomNumericTextBox2 cntxt_JyoutaikunbunCd;
        internal r_framework.CustomControl.CustomNumericTextBox2 cntxt_ManifesutokubunCd;
    }
}