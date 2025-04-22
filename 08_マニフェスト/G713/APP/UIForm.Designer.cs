namespace Shougun.Core.PaperManifest.ManifestoJissekiIchiran
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
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            this.label5 = new System.Windows.Forms.Label();
            this.KOUFU_DATE_FROM = new r_framework.CustomControl.CustomDateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.KOUFU_DATE_TO = new r_framework.CustomControl.CustomDateTimePicker();
            this.panel1 = new r_framework.CustomControl.CustomPanel();
            this.KOUFU_DATE_KBN_4 = new r_framework.CustomControl.CustomRadioButton();
            this.KOUFU_DATE_KBN_3 = new r_framework.CustomControl.CustomRadioButton();
            this.KOUFU_DATE_KBN_2 = new r_framework.CustomControl.CustomRadioButton();
            this.KOUFU_DATE_KBN_1 = new r_framework.CustomControl.CustomRadioButton();
            this.KOUFU_DATE_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cbtn_HaisyutuGyousyaSan = new r_framework.CustomControl.CustomPopupOpenButton();
            this.cantxt_HaisyutuGyousyaCd = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.ctxt_HaisyutuGyousyaName = new r_framework.CustomControl.CustomTextBox();
            this.cbtn_HaisyutuJigyoubaSan = new r_framework.CustomControl.CustomPopupOpenButton();
            this.cantxt_HaisyutuJigyoubaName = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.ctxt_HaisyutuJigyoubaName = new r_framework.CustomControl.CustomTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbtn_UnpanJyutaku1San = new r_framework.CustomControl.CustomPopupOpenButton();
            this.cantxt_UnpanJyutakuNameCd = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.ctxt_UnpanJyutakuName = new r_framework.CustomControl.CustomTextBox();
            this.cbtn_SyobunJyutakuSan = new r_framework.CustomControl.CustomPopupOpenButton();
            this.cantxt_SyobunJyutakuNameCd = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.ctxt_SyobunJyutakuName = new r_framework.CustomControl.CustomTextBox();
            this.cbtn_UnpanJyugyobaSan = new r_framework.CustomControl.CustomPopupOpenButton();
            this.cantxt_UnpanJyugyobaNameCd = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.ctxt_UnpanJyugyobaName = new r_framework.CustomControl.CustomTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cbtn_HokokushoBunruiSan = new r_framework.CustomControl.CustomPopupOpenButton();
            this.cantxt_HokokushoBunrui = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.ctxt_HokokushoBunrui = new r_framework.CustomControl.CustomTextBox();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SHOBUN_HOUHOU_MI = new r_framework.CustomControl.CustomCheckBox();
            this.SHOBUN_HOUHOU_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.SHOBUN_HOUHOU_NAME = new r_framework.CustomControl.CustomTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.JISHA_UNPAN_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.JISHA_UNPAN_KBN_3 = new r_framework.CustomControl.CustomRadioButton();
            this.JISHA_UNPAN_KBN_2 = new r_framework.CustomControl.CustomRadioButton();
            this.JISHA_UNPAN_KBN_1 = new r_framework.CustomControl.CustomRadioButton();
            this.panel1.SuspendLayout();
            this.customPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.searchString.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchString.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.FocusOutCheckMethod")));
            this.searchString.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.searchString.Location = new System.Drawing.Point(974, 68);
            this.searchString.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("searchString.PopupSearchSendParams")));
            this.searchString.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("searchString.popupWindowSetting")));
            this.searchString.ReadOnly = true;
            this.searchString.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.RegistCheckMethod")));
            this.searchString.Size = new System.Drawing.Size(28, 15);
            this.searchString.TabIndex = 30;
            this.searchString.TabStop = false;
            this.searchString.Tag = "検索条件設定画面で設定した条件が表示されます";
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn1.Location = new System.Drawing.Point(3, 415);
            this.bt_ptn1.Size = new System.Drawing.Size(194, 22);
            this.bt_ptn1.TabIndex = 45;
            this.bt_ptn1.Text = "パターン1";
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Location = new System.Drawing.Point(204, 415);
            this.bt_ptn2.TabIndex = 46;
            this.bt_ptn2.Text = "パターン2";
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Location = new System.Drawing.Point(405, 415);
            this.bt_ptn3.TabIndex = 47;
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Location = new System.Drawing.Point(606, 415);
            this.bt_ptn4.TabIndex = 48;
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Location = new System.Drawing.Point(807, 415);
            this.bt_ptn5.TabIndex = 49;
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.Location = new System.Drawing.Point(3, 161);
            this.customSortHeader1.Size = new System.Drawing.Size(997, 25);
            this.customSortHeader1.TabIndex = 40;
            // 
            // customSearchHeader1
            // 
            this.customSearchHeader1.Location = new System.Drawing.Point(3, 137);
            this.customSearchHeader1.TabIndex = 39;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "日付※";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KOUFU_DATE_FROM
            // 
            this.KOUFU_DATE_FROM.BackColor = System.Drawing.SystemColors.Window;
            this.KOUFU_DATE_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KOUFU_DATE_FROM.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.KOUFU_DATE_FROM.Checked = false;
            this.KOUFU_DATE_FROM.CustomFormat = "yyyy/MM/dd(ddd)";
            this.KOUFU_DATE_FROM.DateTimeNowYear = "";
            this.KOUFU_DATE_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.KOUFU_DATE_FROM.DisplayItemName = "交付年月日FROM";
            this.KOUFU_DATE_FROM.DisplayPopUp = null;
            this.KOUFU_DATE_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUFU_DATE_FROM.FocusOutCheckMethod")));
            this.KOUFU_DATE_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KOUFU_DATE_FROM.ForeColor = System.Drawing.Color.Black;
            this.KOUFU_DATE_FROM.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.KOUFU_DATE_FROM.IsInputErrorOccured = false;
            this.KOUFU_DATE_FROM.Location = new System.Drawing.Point(701, 0);
            this.KOUFU_DATE_FROM.MaxLength = 10;
            this.KOUFU_DATE_FROM.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.KOUFU_DATE_FROM.Name = "KOUFU_DATE_FROM";
            this.KOUFU_DATE_FROM.NullValue = "";
            this.KOUFU_DATE_FROM.PopupAfterExecute = null;
            this.KOUFU_DATE_FROM.PopupBeforeExecute = null;
            this.KOUFU_DATE_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KOUFU_DATE_FROM.PopupSearchSendParams")));
            this.KOUFU_DATE_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KOUFU_DATE_FROM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KOUFU_DATE_FROM.popupWindowSetting")));
            this.KOUFU_DATE_FROM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUFU_DATE_FROM.RegistCheckMethod")));
            this.KOUFU_DATE_FROM.Size = new System.Drawing.Size(138, 20);
            this.KOUFU_DATE_FROM.TabIndex = 3;
            this.KOUFU_DATE_FROM.Tag = "日付を選択してください";
            this.KOUFU_DATE_FROM.Text = "2013/12/09(月)";
            this.KOUFU_DATE_FROM.Value = new System.DateTime(2013, 12, 9, 0, 0, 0, 0);
            this.KOUFU_DATE_FROM.ValueChanged += new System.EventHandler(this.KOUFU_DATE_FROM_ValueChanged);
            this.KOUFU_DATE_FROM.Leave += new System.EventHandler(this.KOUFU_DATE_FROM_Leave);
            this.KOUFU_DATE_FROM.MouseDown += new System.Windows.Forms.MouseEventHandler(this.KOUFU_DATE_FROM_MouseDown);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(839, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "～";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KOUFU_DATE_TO
            // 
            this.KOUFU_DATE_TO.BackColor = System.Drawing.SystemColors.Window;
            this.KOUFU_DATE_TO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KOUFU_DATE_TO.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.KOUFU_DATE_TO.Checked = false;
            this.KOUFU_DATE_TO.CustomFormat = "yyyy/MM/dd(ddd)";
            this.KOUFU_DATE_TO.DateTimeNowYear = "";
            this.KOUFU_DATE_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.KOUFU_DATE_TO.DisplayItemName = "交付年月日TO";
            this.KOUFU_DATE_TO.DisplayPopUp = null;
            this.KOUFU_DATE_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUFU_DATE_TO.FocusOutCheckMethod")));
            this.KOUFU_DATE_TO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KOUFU_DATE_TO.ForeColor = System.Drawing.Color.Black;
            this.KOUFU_DATE_TO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.KOUFU_DATE_TO.IsInputErrorOccured = false;
            this.KOUFU_DATE_TO.Location = new System.Drawing.Point(859, 0);
            this.KOUFU_DATE_TO.MaxLength = 10;
            this.KOUFU_DATE_TO.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.KOUFU_DATE_TO.Name = "KOUFU_DATE_TO";
            this.KOUFU_DATE_TO.NullValue = "";
            this.KOUFU_DATE_TO.PopupAfterExecute = null;
            this.KOUFU_DATE_TO.PopupBeforeExecute = null;
            this.KOUFU_DATE_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KOUFU_DATE_TO.PopupSearchSendParams")));
            this.KOUFU_DATE_TO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KOUFU_DATE_TO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KOUFU_DATE_TO.popupWindowSetting")));
            this.KOUFU_DATE_TO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUFU_DATE_TO.RegistCheckMethod")));
            this.KOUFU_DATE_TO.Size = new System.Drawing.Size(138, 20);
            this.KOUFU_DATE_TO.TabIndex = 5;
            this.KOUFU_DATE_TO.Tag = "日付を選択してください";
            this.KOUFU_DATE_TO.Text = "2013/12/09(月)";
            this.KOUFU_DATE_TO.Value = new System.DateTime(2013, 12, 9, 0, 0, 0, 0);
            this.KOUFU_DATE_TO.ValueChanged += new System.EventHandler(this.KOUFU_DATE_TO_ValueChanged);
            this.KOUFU_DATE_TO.Leave += new System.EventHandler(this.KOUFU_DATE_TO_Leave);
            this.KOUFU_DATE_TO.MouseDown += new System.Windows.Forms.MouseEventHandler(this.KOUFU_DATE_TO_MouseDown);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.KOUFU_DATE_KBN_4);
            this.panel1.Controls.Add(this.KOUFU_DATE_KBN_3);
            this.panel1.Controls.Add(this.KOUFU_DATE_KBN_2);
            this.panel1.Controls.Add(this.KOUFU_DATE_KBN_1);
            this.panel1.Location = new System.Drawing.Point(137, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(558, 20);
            this.panel1.TabIndex = 2;
            // 
            // KOUFU_DATE_KBN_4
            // 
            this.KOUFU_DATE_KBN_4.AutoSize = true;
            this.KOUFU_DATE_KBN_4.DefaultBackColor = System.Drawing.Color.Empty;
            this.KOUFU_DATE_KBN_4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUFU_DATE_KBN_4.FocusOutCheckMethod")));
            this.KOUFU_DATE_KBN_4.LinkedTextBox = "KOUFU_DATE_KBN";
            this.KOUFU_DATE_KBN_4.Location = new System.Drawing.Point(412, 1);
            this.KOUFU_DATE_KBN_4.Name = "KOUFU_DATE_KBN_4";
            this.KOUFU_DATE_KBN_4.PopupAfterExecute = null;
            this.KOUFU_DATE_KBN_4.PopupBeforeExecute = null;
            this.KOUFU_DATE_KBN_4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KOUFU_DATE_KBN_4.PopupSearchSendParams")));
            this.KOUFU_DATE_KBN_4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KOUFU_DATE_KBN_4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KOUFU_DATE_KBN_4.popupWindowSetting")));
            this.KOUFU_DATE_KBN_4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUFU_DATE_KBN_4.RegistCheckMethod")));
            this.KOUFU_DATE_KBN_4.Size = new System.Drawing.Size(137, 17);
            this.KOUFU_DATE_KBN_4.TabIndex = 4;
            this.KOUFU_DATE_KBN_4.Tag = "抽出日付を選択してください";
            this.KOUFU_DATE_KBN_4.Text = "4.最終処分終了日";
            this.KOUFU_DATE_KBN_4.UseVisualStyleBackColor = true;
            this.KOUFU_DATE_KBN_4.Value = "4";
            // 
            // KOUFU_DATE_KBN_3
            // 
            this.KOUFU_DATE_KBN_3.AutoSize = true;
            this.KOUFU_DATE_KBN_3.DefaultBackColor = System.Drawing.Color.Empty;
            this.KOUFU_DATE_KBN_3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUFU_DATE_KBN_3.FocusOutCheckMethod")));
            this.KOUFU_DATE_KBN_3.LinkedTextBox = "KOUFU_DATE_KBN";
            this.KOUFU_DATE_KBN_3.Location = new System.Drawing.Point(276, 1);
            this.KOUFU_DATE_KBN_3.Name = "KOUFU_DATE_KBN_3";
            this.KOUFU_DATE_KBN_3.PopupAfterExecute = null;
            this.KOUFU_DATE_KBN_3.PopupBeforeExecute = null;
            this.KOUFU_DATE_KBN_3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KOUFU_DATE_KBN_3.PopupSearchSendParams")));
            this.KOUFU_DATE_KBN_3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KOUFU_DATE_KBN_3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KOUFU_DATE_KBN_3.popupWindowSetting")));
            this.KOUFU_DATE_KBN_3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUFU_DATE_KBN_3.RegistCheckMethod")));
            this.KOUFU_DATE_KBN_3.Size = new System.Drawing.Size(109, 17);
            this.KOUFU_DATE_KBN_3.TabIndex = 3;
            this.KOUFU_DATE_KBN_3.Tag = "抽出日付を選択してください";
            this.KOUFU_DATE_KBN_3.Text = "3.処分終了日";
            this.KOUFU_DATE_KBN_3.UseVisualStyleBackColor = true;
            this.KOUFU_DATE_KBN_3.Value = "3";
            // 
            // KOUFU_DATE_KBN_2
            // 
            this.KOUFU_DATE_KBN_2.AutoSize = true;
            this.KOUFU_DATE_KBN_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.KOUFU_DATE_KBN_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUFU_DATE_KBN_2.FocusOutCheckMethod")));
            this.KOUFU_DATE_KBN_2.LinkedTextBox = "KOUFU_DATE_KBN";
            this.KOUFU_DATE_KBN_2.Location = new System.Drawing.Point(140, 0);
            this.KOUFU_DATE_KBN_2.Name = "KOUFU_DATE_KBN_2";
            this.KOUFU_DATE_KBN_2.PopupAfterExecute = null;
            this.KOUFU_DATE_KBN_2.PopupBeforeExecute = null;
            this.KOUFU_DATE_KBN_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KOUFU_DATE_KBN_2.PopupSearchSendParams")));
            this.KOUFU_DATE_KBN_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KOUFU_DATE_KBN_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KOUFU_DATE_KBN_2.popupWindowSetting")));
            this.KOUFU_DATE_KBN_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUFU_DATE_KBN_2.RegistCheckMethod")));
            this.KOUFU_DATE_KBN_2.Size = new System.Drawing.Size(109, 17);
            this.KOUFU_DATE_KBN_2.TabIndex = 1;
            this.KOUFU_DATE_KBN_2.Tag = "抽出日付を選択してください";
            this.KOUFU_DATE_KBN_2.Text = "2.運搬終了日";
            this.KOUFU_DATE_KBN_2.UseVisualStyleBackColor = true;
            this.KOUFU_DATE_KBN_2.Value = "2";
            // 
            // KOUFU_DATE_KBN_1
            // 
            this.KOUFU_DATE_KBN_1.AutoSize = true;
            this.KOUFU_DATE_KBN_1.Checked = true;
            this.KOUFU_DATE_KBN_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.KOUFU_DATE_KBN_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUFU_DATE_KBN_1.FocusOutCheckMethod")));
            this.KOUFU_DATE_KBN_1.LinkedTextBox = "KOUFU_DATE_KBN";
            this.KOUFU_DATE_KBN_1.Location = new System.Drawing.Point(4, 0);
            this.KOUFU_DATE_KBN_1.Name = "KOUFU_DATE_KBN_1";
            this.KOUFU_DATE_KBN_1.PopupAfterExecute = null;
            this.KOUFU_DATE_KBN_1.PopupBeforeExecute = null;
            this.KOUFU_DATE_KBN_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KOUFU_DATE_KBN_1.PopupSearchSendParams")));
            this.KOUFU_DATE_KBN_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KOUFU_DATE_KBN_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KOUFU_DATE_KBN_1.popupWindowSetting")));
            this.KOUFU_DATE_KBN_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUFU_DATE_KBN_1.RegistCheckMethod")));
            this.KOUFU_DATE_KBN_1.Size = new System.Drawing.Size(109, 17);
            this.KOUFU_DATE_KBN_1.TabIndex = 0;
            this.KOUFU_DATE_KBN_1.Tag = "抽出日付を選択してください";
            this.KOUFU_DATE_KBN_1.Text = "1.交付年月日";
            this.KOUFU_DATE_KBN_1.UseVisualStyleBackColor = true;
            this.KOUFU_DATE_KBN_1.Value = "1";
            // 
            // KOUFU_DATE_KBN
            // 
            this.KOUFU_DATE_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.KOUFU_DATE_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KOUFU_DATE_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.KOUFU_DATE_KBN.DisplayItemName = "日付";
            this.KOUFU_DATE_KBN.DisplayPopUp = null;
            this.KOUFU_DATE_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUFU_DATE_KBN.FocusOutCheckMethod")));
            this.KOUFU_DATE_KBN.ForeColor = System.Drawing.Color.Black;
            this.KOUFU_DATE_KBN.IsInputErrorOccured = false;
            this.KOUFU_DATE_KBN.LinkedRadioButtonArray = new string[] {
        "KOUFU_DATE_KBN_1",
        "KOUFU_DATE_KBN_2",
        "KOUFU_DATE_KBN_3",
        "KOUFU_DATE_KBN_4"};
            this.KOUFU_DATE_KBN.Location = new System.Drawing.Point(118, 0);
            this.KOUFU_DATE_KBN.Name = "KOUFU_DATE_KBN";
            this.KOUFU_DATE_KBN.PopupAfterExecute = null;
            this.KOUFU_DATE_KBN.PopupBeforeExecute = null;
            this.KOUFU_DATE_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KOUFU_DATE_KBN.PopupSearchSendParams")));
            this.KOUFU_DATE_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KOUFU_DATE_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KOUFU_DATE_KBN.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            4,
            0,
            0,
            0});
            rangeSettingDto2.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.KOUFU_DATE_KBN.RangeSetting = rangeSettingDto2;
            this.KOUFU_DATE_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUFU_DATE_KBN.RegistCheckMethod")));
            this.KOUFU_DATE_KBN.Size = new System.Drawing.Size(20, 20);
            this.KOUFU_DATE_KBN.TabIndex = 1;
            this.KOUFU_DATE_KBN.Tag = "【1～4】のいずれかで入力してください";
            this.KOUFU_DATE_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.KOUFU_DATE_KBN.WordWrap = false;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(3, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 20);
            this.label6.TabIndex = 6;
            this.label6.Text = "排出事業者";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(3, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "運搬受託者※";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(3, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 20);
            this.label8.TabIndex = 22;
            this.label8.Text = "処分受託者※";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(3, 92);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(110, 20);
            this.label10.TabIndex = 31;
            this.label10.Text = "報告書分類";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbtn_HaisyutuGyousyaSan
            // 
            this.cbtn_HaisyutuGyousyaSan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.cbtn_HaisyutuGyousyaSan.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cbtn_HaisyutuGyousyaSan.DBFieldsName = null;
            this.cbtn_HaisyutuGyousyaSan.DefaultBackColor = System.Drawing.Color.Empty;
            this.cbtn_HaisyutuGyousyaSan.DisplayItemName = null;
            this.cbtn_HaisyutuGyousyaSan.DisplayPopUp = null;
            this.cbtn_HaisyutuGyousyaSan.ErrorMessage = null;
            this.cbtn_HaisyutuGyousyaSan.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_HaisyutuGyousyaSan.FocusOutCheckMethod")));
            this.cbtn_HaisyutuGyousyaSan.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.cbtn_HaisyutuGyousyaSan.GetCodeMasterField = null;
            this.cbtn_HaisyutuGyousyaSan.Image = ((System.Drawing.Image)(resources.GetObject("cbtn_HaisyutuGyousyaSan.Image")));
            this.cbtn_HaisyutuGyousyaSan.ItemDefinedTypes = null;
            this.cbtn_HaisyutuGyousyaSan.LinkedSettingTextBox = null;
            this.cbtn_HaisyutuGyousyaSan.LinkedTextBoxs = null;
            this.cbtn_HaisyutuGyousyaSan.Location = new System.Drawing.Point(463, 22);
            this.cbtn_HaisyutuGyousyaSan.Name = "cbtn_HaisyutuGyousyaSan";
            this.cbtn_HaisyutuGyousyaSan.PopupAfterExecute = null;
            this.cbtn_HaisyutuGyousyaSan.PopupAfterExecuteMethod = "cantxt_HaisyutuGyousyaCd_PopupAfterExecuteMethod";
            this.cbtn_HaisyutuGyousyaSan.PopupBeforeExecute = null;
            this.cbtn_HaisyutuGyousyaSan.PopupBeforeExecuteMethod = "cantxt_HaisyutuGyousyaCd_PopupBeforeExecuteMethod";
            this.cbtn_HaisyutuGyousyaSan.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GYOUSHA_POST,GYOUSHA_TEL,GYOUSHA_ADDRESS1";
            this.cbtn_HaisyutuGyousyaSan.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cbtn_HaisyutuGyousyaSan.PopupSearchSendParams")));
            this.cbtn_HaisyutuGyousyaSan.PopupSetFormField = "cantxt_HaisyutuGyousyaCd,ctxt_HaisyutuGyousyaName,cnt_HaisyutuGyousyaZip,cnt_Hais" +
    "yutuGyousyaTel,ctxt_HaisyutuGyousyaAdd";
            this.cbtn_HaisyutuGyousyaSan.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.cbtn_HaisyutuGyousyaSan.PopupWindowName = "検索共通ポップアップ";
            this.cbtn_HaisyutuGyousyaSan.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cbtn_HaisyutuGyousyaSan.popupWindowSetting")));
            this.cbtn_HaisyutuGyousyaSan.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_HaisyutuGyousyaSan.RegistCheckMethod")));
            this.cbtn_HaisyutuGyousyaSan.SearchDisplayFlag = 0;
            this.cbtn_HaisyutuGyousyaSan.SetFormField = "cantxt_HaisyutuGyousyaCd,ctxt_HaisyutuGyousyaName,cnt_HaisyutuGyousyaZip,cnt_Hais" +
    "yutuGyousyaTel,ctxt_HaisyutuGyousyaAdd";
            this.cbtn_HaisyutuGyousyaSan.ShortItemName = null;
            this.cbtn_HaisyutuGyousyaSan.Size = new System.Drawing.Size(22, 22);
            this.cbtn_HaisyutuGyousyaSan.TabIndex = 9;
            this.cbtn_HaisyutuGyousyaSan.TabStop = false;
            this.cbtn_HaisyutuGyousyaSan.Tag = "排出事業者の検索を行います";
            this.cbtn_HaisyutuGyousyaSan.Text = " ";
            this.cbtn_HaisyutuGyousyaSan.UseVisualStyleBackColor = false;
            this.cbtn_HaisyutuGyousyaSan.ZeroPaddengFlag = false;
            // 
            // cantxt_HaisyutuGyousyaCd
            // 
            this.cantxt_HaisyutuGyousyaCd.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_HaisyutuGyousyaCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_HaisyutuGyousyaCd.ChangeUpperCase = true;
            this.cantxt_HaisyutuGyousyaCd.CharacterLimitList = null;
            this.cantxt_HaisyutuGyousyaCd.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.cantxt_HaisyutuGyousyaCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_HaisyutuGyousyaCd.DisplayPopUp = null;
            this.cantxt_HaisyutuGyousyaCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_HaisyutuGyousyaCd.FocusOutCheckMethod")));
            this.cantxt_HaisyutuGyousyaCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cantxt_HaisyutuGyousyaCd.ForeColor = System.Drawing.Color.Black;
            this.cantxt_HaisyutuGyousyaCd.GetCodeMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.cantxt_HaisyutuGyousyaCd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cantxt_HaisyutuGyousyaCd.IsInputErrorOccured = false;
            this.cantxt_HaisyutuGyousyaCd.Location = new System.Drawing.Point(118, 23);
            this.cantxt_HaisyutuGyousyaCd.MaxLength = 6;
            this.cantxt_HaisyutuGyousyaCd.Name = "cantxt_HaisyutuGyousyaCd";
            this.cantxt_HaisyutuGyousyaCd.PopupAfterExecute = null;
            this.cantxt_HaisyutuGyousyaCd.PopupAfterExecuteMethod = "cantxt_HaisyutuGyousyaCd_PopupAfterExecuteMethod";
            this.cantxt_HaisyutuGyousyaCd.PopupBeforeExecute = null;
            this.cantxt_HaisyutuGyousyaCd.PopupBeforeExecuteMethod = "cantxt_HaisyutuGyousyaCd_PopupBeforeExecuteMethod";
            this.cantxt_HaisyutuGyousyaCd.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.cantxt_HaisyutuGyousyaCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_HaisyutuGyousyaCd.PopupSearchSendParams")));
            this.cantxt_HaisyutuGyousyaCd.PopupSendParams = new string[0];
            this.cantxt_HaisyutuGyousyaCd.PopupSetFormField = "cantxt_HaisyutuGyousyaCd,ctxt_HaisyutuGyousyaName";
            this.cantxt_HaisyutuGyousyaCd.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.cantxt_HaisyutuGyousyaCd.PopupWindowName = "検索共通ポップアップ";
            this.cantxt_HaisyutuGyousyaCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_HaisyutuGyousyaCd.popupWindowSetting")));
            this.cantxt_HaisyutuGyousyaCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_HaisyutuGyousyaCd.RegistCheckMethod")));
            this.cantxt_HaisyutuGyousyaCd.SetFormField = "cantxt_HaisyutuGyousyaCd,ctxt_HaisyutuGyousyaName";
            this.cantxt_HaisyutuGyousyaCd.Size = new System.Drawing.Size(60, 20);
            this.cantxt_HaisyutuGyousyaCd.TabIndex = 7;
            this.cantxt_HaisyutuGyousyaCd.Tag = "半角6桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.cantxt_HaisyutuGyousyaCd.ZeroPaddengFlag = true;
            this.cantxt_HaisyutuGyousyaCd.Validated += new System.EventHandler(this.cantxt_HaisyutuGyousyaCd_Validated);
            // 
            // ctxt_HaisyutuGyousyaName
            // 
            this.ctxt_HaisyutuGyousyaName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_HaisyutuGyousyaName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_HaisyutuGyousyaName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ctxt_HaisyutuGyousyaName.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_HaisyutuGyousyaName.DisplayPopUp = null;
            this.ctxt_HaisyutuGyousyaName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_HaisyutuGyousyaName.FocusOutCheckMethod")));
            this.ctxt_HaisyutuGyousyaName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ctxt_HaisyutuGyousyaName.ForeColor = System.Drawing.Color.Black;
            this.ctxt_HaisyutuGyousyaName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctxt_HaisyutuGyousyaName.IsInputErrorOccured = false;
            this.ctxt_HaisyutuGyousyaName.Location = new System.Drawing.Point(177, 23);
            this.ctxt_HaisyutuGyousyaName.MaxLength = 0;
            this.ctxt_HaisyutuGyousyaName.Name = "ctxt_HaisyutuGyousyaName";
            this.ctxt_HaisyutuGyousyaName.PopupAfterExecute = null;
            this.ctxt_HaisyutuGyousyaName.PopupBeforeExecute = null;
            this.ctxt_HaisyutuGyousyaName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_HaisyutuGyousyaName.PopupSearchSendParams")));
            this.ctxt_HaisyutuGyousyaName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_HaisyutuGyousyaName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_HaisyutuGyousyaName.popupWindowSetting")));
            this.ctxt_HaisyutuGyousyaName.ReadOnly = true;
            this.ctxt_HaisyutuGyousyaName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_HaisyutuGyousyaName.RegistCheckMethod")));
            this.ctxt_HaisyutuGyousyaName.Size = new System.Drawing.Size(285, 20);
            this.ctxt_HaisyutuGyousyaName.TabIndex = 8;
            this.ctxt_HaisyutuGyousyaName.TabStop = false;
            this.ctxt_HaisyutuGyousyaName.Tag = "";
            // 
            // cbtn_HaisyutuJigyoubaSan
            // 
            this.cbtn_HaisyutuJigyoubaSan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.cbtn_HaisyutuJigyoubaSan.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cbtn_HaisyutuJigyoubaSan.DBFieldsName = null;
            this.cbtn_HaisyutuJigyoubaSan.DefaultBackColor = System.Drawing.Color.Empty;
            this.cbtn_HaisyutuJigyoubaSan.DisplayItemName = null;
            this.cbtn_HaisyutuJigyoubaSan.DisplayPopUp = null;
            this.cbtn_HaisyutuJigyoubaSan.ErrorMessage = null;
            this.cbtn_HaisyutuJigyoubaSan.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_HaisyutuJigyoubaSan.FocusOutCheckMethod")));
            this.cbtn_HaisyutuJigyoubaSan.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.cbtn_HaisyutuJigyoubaSan.GetCodeMasterField = null;
            this.cbtn_HaisyutuJigyoubaSan.Image = ((System.Drawing.Image)(resources.GetObject("cbtn_HaisyutuJigyoubaSan.Image")));
            this.cbtn_HaisyutuJigyoubaSan.ItemDefinedTypes = null;
            this.cbtn_HaisyutuJigyoubaSan.LinkedSettingTextBox = null;
            this.cbtn_HaisyutuJigyoubaSan.LinkedTextBoxs = null;
            this.cbtn_HaisyutuJigyoubaSan.Location = new System.Drawing.Point(951, 22);
            this.cbtn_HaisyutuJigyoubaSan.Name = "cbtn_HaisyutuJigyoubaSan";
            this.cbtn_HaisyutuJigyoubaSan.PopupAfterExecute = null;
            this.cbtn_HaisyutuJigyoubaSan.PopupAfterExecuteMethod = "";
            this.cbtn_HaisyutuJigyoubaSan.PopupBeforeExecute = null;
            this.cbtn_HaisyutuJigyoubaSan.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.cbtn_HaisyutuJigyoubaSan.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cbtn_HaisyutuJigyoubaSan.PopupSearchSendParams")));
            this.cbtn_HaisyutuJigyoubaSan.PopupSetFormField = "cantxt_HaisyutuJigyoubaName,ctxt_HaisyutuJigyoubaName,cantxt_HaisyutuGyousyaCd,ct" +
    "xt_HaisyutuGyousyaName";
            this.cbtn_HaisyutuJigyoubaSan.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.cbtn_HaisyutuJigyoubaSan.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.cbtn_HaisyutuJigyoubaSan.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cbtn_HaisyutuJigyoubaSan.popupWindowSetting")));
            this.cbtn_HaisyutuJigyoubaSan.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_HaisyutuJigyoubaSan.RegistCheckMethod")));
            this.cbtn_HaisyutuJigyoubaSan.SearchDisplayFlag = 0;
            this.cbtn_HaisyutuJigyoubaSan.SetFormField = null;
            this.cbtn_HaisyutuJigyoubaSan.ShortItemName = null;
            this.cbtn_HaisyutuJigyoubaSan.Size = new System.Drawing.Size(22, 22);
            this.cbtn_HaisyutuJigyoubaSan.TabIndex = 13;
            this.cbtn_HaisyutuJigyoubaSan.TabStop = false;
            this.cbtn_HaisyutuJigyoubaSan.Tag = "排出事業場の検索を行います";
            this.cbtn_HaisyutuJigyoubaSan.Text = " ";
            this.cbtn_HaisyutuJigyoubaSan.UseVisualStyleBackColor = false;
            this.cbtn_HaisyutuJigyoubaSan.ZeroPaddengFlag = false;
            // 
            // cantxt_HaisyutuJigyoubaName
            // 
            this.cantxt_HaisyutuJigyoubaName.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_HaisyutuJigyoubaName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_HaisyutuJigyoubaName.ChangeUpperCase = true;
            this.cantxt_HaisyutuJigyoubaName.CharacterLimitList = null;
            this.cantxt_HaisyutuJigyoubaName.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.cantxt_HaisyutuJigyoubaName.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_HaisyutuJigyoubaName.DisplayPopUp = null;
            this.cantxt_HaisyutuJigyoubaName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_HaisyutuJigyoubaName.FocusOutCheckMethod")));
            this.cantxt_HaisyutuJigyoubaName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cantxt_HaisyutuJigyoubaName.ForeColor = System.Drawing.Color.Black;
            this.cantxt_HaisyutuJigyoubaName.GetCodeMasterField = "GENBA_CD,GENBA_NAME_RYAKU";
            this.cantxt_HaisyutuJigyoubaName.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cantxt_HaisyutuJigyoubaName.IsInputErrorOccured = false;
            this.cantxt_HaisyutuJigyoubaName.Location = new System.Drawing.Point(606, 23);
            this.cantxt_HaisyutuJigyoubaName.MaxLength = 6;
            this.cantxt_HaisyutuJigyoubaName.Name = "cantxt_HaisyutuJigyoubaName";
            this.cantxt_HaisyutuJigyoubaName.PopupAfterExecute = null;
            this.cantxt_HaisyutuJigyoubaName.PopupAfterExecuteMethod = "";
            this.cantxt_HaisyutuJigyoubaName.PopupBeforeExecute = null;
            this.cantxt_HaisyutuJigyoubaName.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.cantxt_HaisyutuJigyoubaName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_HaisyutuJigyoubaName.PopupSearchSendParams")));
            this.cantxt_HaisyutuJigyoubaName.PopupSendParams = new string[0];
            this.cantxt_HaisyutuJigyoubaName.PopupSetFormField = "cantxt_HaisyutuJigyoubaName,ctxt_HaisyutuJigyoubaName,cantxt_HaisyutuGyousyaCd,ct" +
    "xt_HaisyutuGyousyaName";
            this.cantxt_HaisyutuJigyoubaName.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.cantxt_HaisyutuJigyoubaName.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.cantxt_HaisyutuJigyoubaName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_HaisyutuJigyoubaName.popupWindowSetting")));
            this.cantxt_HaisyutuJigyoubaName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_HaisyutuJigyoubaName.RegistCheckMethod")));
            this.cantxt_HaisyutuJigyoubaName.SetFormField = "cantxt_HaisyutuJigyoubaName,ctxt_HaisyutuJigyoubaName,cantxt_HaisyutuGyousyaCd,ct" +
    "xt_HaisyutuGyousyaName";
            this.cantxt_HaisyutuJigyoubaName.Size = new System.Drawing.Size(60, 20);
            this.cantxt_HaisyutuJigyoubaName.TabIndex = 11;
            this.cantxt_HaisyutuJigyoubaName.Tag = "半角6桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.cantxt_HaisyutuJigyoubaName.ZeroPaddengFlag = true;
            this.cantxt_HaisyutuJigyoubaName.Validated += new System.EventHandler(this.cantxt_HaisyutuJigyoubaName_Validated);
            // 
            // ctxt_HaisyutuJigyoubaName
            // 
            this.ctxt_HaisyutuJigyoubaName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_HaisyutuJigyoubaName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_HaisyutuJigyoubaName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ctxt_HaisyutuJigyoubaName.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_HaisyutuJigyoubaName.DisplayPopUp = null;
            this.ctxt_HaisyutuJigyoubaName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_HaisyutuJigyoubaName.FocusOutCheckMethod")));
            this.ctxt_HaisyutuJigyoubaName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ctxt_HaisyutuJigyoubaName.ForeColor = System.Drawing.Color.Black;
            this.ctxt_HaisyutuJigyoubaName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctxt_HaisyutuJigyoubaName.IsInputErrorOccured = false;
            this.ctxt_HaisyutuJigyoubaName.Location = new System.Drawing.Point(665, 23);
            this.ctxt_HaisyutuJigyoubaName.MaxLength = 0;
            this.ctxt_HaisyutuJigyoubaName.Name = "ctxt_HaisyutuJigyoubaName";
            this.ctxt_HaisyutuJigyoubaName.PopupAfterExecute = null;
            this.ctxt_HaisyutuJigyoubaName.PopupBeforeExecute = null;
            this.ctxt_HaisyutuJigyoubaName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_HaisyutuJigyoubaName.PopupSearchSendParams")));
            this.ctxt_HaisyutuJigyoubaName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_HaisyutuJigyoubaName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_HaisyutuJigyoubaName.popupWindowSetting")));
            this.ctxt_HaisyutuJigyoubaName.ReadOnly = true;
            this.ctxt_HaisyutuJigyoubaName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_HaisyutuJigyoubaName.RegistCheckMethod")));
            this.ctxt_HaisyutuJigyoubaName.Size = new System.Drawing.Size(285, 20);
            this.ctxt_HaisyutuJigyoubaName.TabIndex = 12;
            this.ctxt_HaisyutuJigyoubaName.TabStop = false;
            this.ctxt_HaisyutuJigyoubaName.Tag = "";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label11.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(490, 23);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(110, 20);
            this.label11.TabIndex = 10;
            this.label11.Text = "排出事業場";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbtn_UnpanJyutaku1San
            // 
            this.cbtn_UnpanJyutaku1San.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.cbtn_UnpanJyutaku1San.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cbtn_UnpanJyutaku1San.DBFieldsName = null;
            this.cbtn_UnpanJyutaku1San.DefaultBackColor = System.Drawing.Color.Empty;
            this.cbtn_UnpanJyutaku1San.DisplayItemName = null;
            this.cbtn_UnpanJyutaku1San.DisplayPopUp = null;
            this.cbtn_UnpanJyutaku1San.ErrorMessage = null;
            this.cbtn_UnpanJyutaku1San.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_UnpanJyutaku1San.FocusOutCheckMethod")));
            this.cbtn_UnpanJyutaku1San.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.cbtn_UnpanJyutaku1San.GetCodeMasterField = null;
            this.cbtn_UnpanJyutaku1San.Image = ((System.Drawing.Image)(resources.GetObject("cbtn_UnpanJyutaku1San.Image")));
            this.cbtn_UnpanJyutaku1San.ItemDefinedTypes = null;
            this.cbtn_UnpanJyutaku1San.LinkedSettingTextBox = null;
            this.cbtn_UnpanJyutaku1San.LinkedTextBoxs = null;
            this.cbtn_UnpanJyutaku1San.Location = new System.Drawing.Point(463, 45);
            this.cbtn_UnpanJyutaku1San.Name = "cbtn_UnpanJyutaku1San";
            this.cbtn_UnpanJyutaku1San.PopupAfterExecute = null;
            this.cbtn_UnpanJyutaku1San.PopupAfterExecuteMethod = "cantxt_UnpanJyutaku1NameCd_PopupAfterExecuteMethod";
            this.cbtn_UnpanJyutaku1San.PopupBeforeExecute = null;
            this.cbtn_UnpanJyutaku1San.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GYOUSHA_POST,GYOUSHA_TEL,GYOUSHA_ADDRESS1";
            this.cbtn_UnpanJyutaku1San.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cbtn_UnpanJyutaku1San.PopupSearchSendParams")));
            this.cbtn_UnpanJyutaku1San.PopupSetFormField = "cantxt_UnpanJyutakuNameCd,ctxt_UnpanJyutakuName,cnt_UnpanJyutaku1Zip,cnt_UnpanJyu" +
    "taku1Tel,ctxt_UnpanJyutaku1Add";
            this.cbtn_UnpanJyutaku1San.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.cbtn_UnpanJyutaku1San.PopupWindowName = "検索共通ポップアップ";
            this.cbtn_UnpanJyutaku1San.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cbtn_UnpanJyutaku1San.popupWindowSetting")));
            this.cbtn_UnpanJyutaku1San.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_UnpanJyutaku1San.RegistCheckMethod")));
            this.cbtn_UnpanJyutaku1San.SearchDisplayFlag = 0;
            this.cbtn_UnpanJyutaku1San.SetFormField = "cantxt_UnpanJyutakuNameCd,ctxt_UnpanJyutakuName,cnt_UnpanJyutaku1Zip,cnt_UnpanJyu" +
    "taku1Tel,ctxt_UnpanJyutaku1Add";
            this.cbtn_UnpanJyutaku1San.ShortItemName = null;
            this.cbtn_UnpanJyutaku1San.Size = new System.Drawing.Size(22, 22);
            this.cbtn_UnpanJyutaku1San.TabIndex = 17;
            this.cbtn_UnpanJyutaku1San.TabStop = false;
            this.cbtn_UnpanJyutaku1San.Tag = "運搬受託者の検索を行います";
            this.cbtn_UnpanJyutaku1San.Text = " ";
            this.cbtn_UnpanJyutaku1San.UseVisualStyleBackColor = false;
            this.cbtn_UnpanJyutaku1San.ZeroPaddengFlag = false;
            // 
            // cantxt_UnpanJyutakuNameCd
            // 
            this.cantxt_UnpanJyutakuNameCd.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_UnpanJyutakuNameCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_UnpanJyutakuNameCd.ChangeUpperCase = true;
            this.cantxt_UnpanJyutakuNameCd.CharacterLimitList = null;
            this.cantxt_UnpanJyutakuNameCd.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.cantxt_UnpanJyutakuNameCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_UnpanJyutakuNameCd.DisplayItemName = "運搬受託者";
            this.cantxt_UnpanJyutakuNameCd.DisplayPopUp = null;
            this.cantxt_UnpanJyutakuNameCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_UnpanJyutakuNameCd.FocusOutCheckMethod")));
            this.cantxt_UnpanJyutakuNameCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cantxt_UnpanJyutakuNameCd.ForeColor = System.Drawing.Color.Black;
            this.cantxt_UnpanJyutakuNameCd.GetCodeMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.cantxt_UnpanJyutakuNameCd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cantxt_UnpanJyutakuNameCd.IsInputErrorOccured = false;
            this.cantxt_UnpanJyutakuNameCd.Location = new System.Drawing.Point(118, 46);
            this.cantxt_UnpanJyutakuNameCd.MaxLength = 6;
            this.cantxt_UnpanJyutakuNameCd.Name = "cantxt_UnpanJyutakuNameCd";
            this.cantxt_UnpanJyutakuNameCd.PopupAfterExecute = null;
            this.cantxt_UnpanJyutakuNameCd.PopupAfterExecuteMethod = "";
            this.cantxt_UnpanJyutakuNameCd.PopupBeforeExecute = null;
            this.cantxt_UnpanJyutakuNameCd.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.cantxt_UnpanJyutakuNameCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_UnpanJyutakuNameCd.PopupSearchSendParams")));
            this.cantxt_UnpanJyutakuNameCd.PopupSetFormField = "cantxt_UnpanJyutakuNameCd,ctxt_UnpanJyutakuName";
            this.cantxt_UnpanJyutakuNameCd.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.cantxt_UnpanJyutakuNameCd.PopupWindowName = "検索共通ポップアップ";
            this.cantxt_UnpanJyutakuNameCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_UnpanJyutakuNameCd.popupWindowSetting")));
            this.cantxt_UnpanJyutakuNameCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_UnpanJyutakuNameCd.RegistCheckMethod")));
            this.cantxt_UnpanJyutakuNameCd.SetFormField = "cantxt_UnpanJyutakuNameCd,ctxt_UnpanJyutakuName";
            this.cantxt_UnpanJyutakuNameCd.Size = new System.Drawing.Size(60, 20);
            this.cantxt_UnpanJyutakuNameCd.TabIndex = 15;
            this.cantxt_UnpanJyutakuNameCd.Tag = "半角6桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.cantxt_UnpanJyutakuNameCd.ZeroPaddengFlag = true;
            this.cantxt_UnpanJyutakuNameCd.Validated += new System.EventHandler(this.cantxt_UnpanJyutakuNameCd_Validated);
            // 
            // ctxt_UnpanJyutakuName
            // 
            this.ctxt_UnpanJyutakuName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_UnpanJyutakuName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_UnpanJyutakuName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ctxt_UnpanJyutakuName.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_UnpanJyutakuName.DisplayPopUp = null;
            this.ctxt_UnpanJyutakuName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_UnpanJyutakuName.FocusOutCheckMethod")));
            this.ctxt_UnpanJyutakuName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ctxt_UnpanJyutakuName.ForeColor = System.Drawing.Color.Black;
            this.ctxt_UnpanJyutakuName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctxt_UnpanJyutakuName.IsInputErrorOccured = false;
            this.ctxt_UnpanJyutakuName.Location = new System.Drawing.Point(177, 46);
            this.ctxt_UnpanJyutakuName.MaxLength = 0;
            this.ctxt_UnpanJyutakuName.Name = "ctxt_UnpanJyutakuName";
            this.ctxt_UnpanJyutakuName.PopupAfterExecute = null;
            this.ctxt_UnpanJyutakuName.PopupBeforeExecute = null;
            this.ctxt_UnpanJyutakuName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_UnpanJyutakuName.PopupSearchSendParams")));
            this.ctxt_UnpanJyutakuName.PopupSetFormField = "";
            this.ctxt_UnpanJyutakuName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_UnpanJyutakuName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_UnpanJyutakuName.popupWindowSetting")));
            this.ctxt_UnpanJyutakuName.ReadOnly = true;
            this.ctxt_UnpanJyutakuName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_UnpanJyutakuName.RegistCheckMethod")));
            this.ctxt_UnpanJyutakuName.SetFormField = "";
            this.ctxt_UnpanJyutakuName.Size = new System.Drawing.Size(285, 20);
            this.ctxt_UnpanJyutakuName.TabIndex = 16;
            this.ctxt_UnpanJyutakuName.TabStop = false;
            this.ctxt_UnpanJyutakuName.Tag = "";
            // 
            // cbtn_SyobunJyutakuSan
            // 
            this.cbtn_SyobunJyutakuSan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.cbtn_SyobunJyutakuSan.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cbtn_SyobunJyutakuSan.DBFieldsName = null;
            this.cbtn_SyobunJyutakuSan.DefaultBackColor = System.Drawing.Color.Empty;
            this.cbtn_SyobunJyutakuSan.DisplayItemName = null;
            this.cbtn_SyobunJyutakuSan.DisplayPopUp = null;
            this.cbtn_SyobunJyutakuSan.ErrorMessage = null;
            this.cbtn_SyobunJyutakuSan.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_SyobunJyutakuSan.FocusOutCheckMethod")));
            this.cbtn_SyobunJyutakuSan.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.cbtn_SyobunJyutakuSan.GetCodeMasterField = null;
            this.cbtn_SyobunJyutakuSan.Image = ((System.Drawing.Image)(resources.GetObject("cbtn_SyobunJyutakuSan.Image")));
            this.cbtn_SyobunJyutakuSan.ItemDefinedTypes = null;
            this.cbtn_SyobunJyutakuSan.LinkedSettingTextBox = null;
            this.cbtn_SyobunJyutakuSan.LinkedTextBoxs = null;
            this.cbtn_SyobunJyutakuSan.Location = new System.Drawing.Point(463, 68);
            this.cbtn_SyobunJyutakuSan.Name = "cbtn_SyobunJyutakuSan";
            this.cbtn_SyobunJyutakuSan.PopupAfterExecute = null;
            this.cbtn_SyobunJyutakuSan.PopupAfterExecuteMethod = "cantxt_SyobunJyutakuNameCd_PopupAfterExecuteMethod";
            this.cbtn_SyobunJyutakuSan.PopupBeforeExecute = null;
            this.cbtn_SyobunJyutakuSan.PopupBeforeExecuteMethod = "cantxt_SyobunJyutakuNameCd_PopupBeforeExecuteMethod";
            this.cbtn_SyobunJyutakuSan.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GYOUSHA_POST,GYOUSHA_TEL,GYOUSHA_ADDRESS1";
            this.cbtn_SyobunJyutakuSan.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cbtn_SyobunJyutakuSan.PopupSearchSendParams")));
            this.cbtn_SyobunJyutakuSan.PopupSetFormField = "cantxt_SyobunJyutakuNameCd,ctxt_SyobunJyutakuName,cnt_SyobunJyutakuZip,cnt_Syobun" +
    "JyutakuTel,ctxt_SyobunJyutakuAdd";
            this.cbtn_SyobunJyutakuSan.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.cbtn_SyobunJyutakuSan.PopupWindowName = "検索共通ポップアップ";
            this.cbtn_SyobunJyutakuSan.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cbtn_SyobunJyutakuSan.popupWindowSetting")));
            this.cbtn_SyobunJyutakuSan.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_SyobunJyutakuSan.RegistCheckMethod")));
            this.cbtn_SyobunJyutakuSan.SearchDisplayFlag = 0;
            this.cbtn_SyobunJyutakuSan.SetFormField = "cantxt_SyobunJyutakuNameCd,ctxt_SyobunJyutakuName,cnt_SyobunJyutakuZip,cnt_Syobun" +
    "JyutakuTel,ctxt_SyobunJyutakuAdd";
            this.cbtn_SyobunJyutakuSan.ShortItemName = null;
            this.cbtn_SyobunJyutakuSan.Size = new System.Drawing.Size(22, 22);
            this.cbtn_SyobunJyutakuSan.TabIndex = 25;
            this.cbtn_SyobunJyutakuSan.TabStop = false;
            this.cbtn_SyobunJyutakuSan.Tag = "処分受託者の検索を行います";
            this.cbtn_SyobunJyutakuSan.Text = " ";
            this.cbtn_SyobunJyutakuSan.UseVisualStyleBackColor = false;
            this.cbtn_SyobunJyutakuSan.ZeroPaddengFlag = false;
            // 
            // cantxt_SyobunJyutakuNameCd
            // 
            this.cantxt_SyobunJyutakuNameCd.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_SyobunJyutakuNameCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_SyobunJyutakuNameCd.ChangeUpperCase = true;
            this.cantxt_SyobunJyutakuNameCd.CharacterLimitList = null;
            this.cantxt_SyobunJyutakuNameCd.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.cantxt_SyobunJyutakuNameCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_SyobunJyutakuNameCd.DisplayItemName = "処分受託者";
            this.cantxt_SyobunJyutakuNameCd.DisplayPopUp = null;
            this.cantxt_SyobunJyutakuNameCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_SyobunJyutakuNameCd.FocusOutCheckMethod")));
            this.cantxt_SyobunJyutakuNameCd.ForeColor = System.Drawing.Color.Black;
            this.cantxt_SyobunJyutakuNameCd.GetCodeMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.cantxt_SyobunJyutakuNameCd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cantxt_SyobunJyutakuNameCd.IsInputErrorOccured = false;
            this.cantxt_SyobunJyutakuNameCd.Location = new System.Drawing.Point(118, 69);
            this.cantxt_SyobunJyutakuNameCd.MaxLength = 6;
            this.cantxt_SyobunJyutakuNameCd.Name = "cantxt_SyobunJyutakuNameCd";
            this.cantxt_SyobunJyutakuNameCd.PopupAfterExecute = null;
            this.cantxt_SyobunJyutakuNameCd.PopupAfterExecuteMethod = "cantxt_SyobunJyutakuNameCd_PopupAfterExecuteMethod";
            this.cantxt_SyobunJyutakuNameCd.PopupBeforeExecute = null;
            this.cantxt_SyobunJyutakuNameCd.PopupBeforeExecuteMethod = "cantxt_SyobunJyutakuNameCd_PopupBeforeExecuteMethod";
            this.cantxt_SyobunJyutakuNameCd.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.cantxt_SyobunJyutakuNameCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_SyobunJyutakuNameCd.PopupSearchSendParams")));
            this.cantxt_SyobunJyutakuNameCd.PopupSetFormField = "cantxt_SyobunJyutakuNameCd,ctxt_SyobunJyutakuName";
            this.cantxt_SyobunJyutakuNameCd.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.cantxt_SyobunJyutakuNameCd.PopupWindowName = "検索共通ポップアップ";
            this.cantxt_SyobunJyutakuNameCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_SyobunJyutakuNameCd.popupWindowSetting")));
            this.cantxt_SyobunJyutakuNameCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_SyobunJyutakuNameCd.RegistCheckMethod")));
            this.cantxt_SyobunJyutakuNameCd.SetFormField = "cantxt_SyobunJyutakuNameCd,ctxt_SyobunJyutakuName";
            this.cantxt_SyobunJyutakuNameCd.Size = new System.Drawing.Size(60, 20);
            this.cantxt_SyobunJyutakuNameCd.TabIndex = 23;
            this.cantxt_SyobunJyutakuNameCd.Tag = "半角6桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.cantxt_SyobunJyutakuNameCd.ZeroPaddengFlag = true;
            this.cantxt_SyobunJyutakuNameCd.Validated += new System.EventHandler(this.cantxt_SyobunJyutakuNameCd_Validated);
            // 
            // ctxt_SyobunJyutakuName
            // 
            this.ctxt_SyobunJyutakuName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_SyobunJyutakuName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_SyobunJyutakuName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ctxt_SyobunJyutakuName.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_SyobunJyutakuName.DisplayPopUp = null;
            this.ctxt_SyobunJyutakuName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_SyobunJyutakuName.FocusOutCheckMethod")));
            this.ctxt_SyobunJyutakuName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ctxt_SyobunJyutakuName.ForeColor = System.Drawing.Color.Black;
            this.ctxt_SyobunJyutakuName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctxt_SyobunJyutakuName.IsInputErrorOccured = false;
            this.ctxt_SyobunJyutakuName.Location = new System.Drawing.Point(177, 69);
            this.ctxt_SyobunJyutakuName.MaxLength = 0;
            this.ctxt_SyobunJyutakuName.Name = "ctxt_SyobunJyutakuName";
            this.ctxt_SyobunJyutakuName.PopupAfterExecute = null;
            this.ctxt_SyobunJyutakuName.PopupBeforeExecute = null;
            this.ctxt_SyobunJyutakuName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_SyobunJyutakuName.PopupSearchSendParams")));
            this.ctxt_SyobunJyutakuName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_SyobunJyutakuName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_SyobunJyutakuName.popupWindowSetting")));
            this.ctxt_SyobunJyutakuName.ReadOnly = true;
            this.ctxt_SyobunJyutakuName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_SyobunJyutakuName.RegistCheckMethod")));
            this.ctxt_SyobunJyutakuName.Size = new System.Drawing.Size(285, 20);
            this.ctxt_SyobunJyutakuName.TabIndex = 24;
            this.ctxt_SyobunJyutakuName.TabStop = false;
            this.ctxt_SyobunJyutakuName.Tag = "";
            // 
            // cbtn_UnpanJyugyobaSan
            // 
            this.cbtn_UnpanJyugyobaSan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.cbtn_UnpanJyugyobaSan.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cbtn_UnpanJyugyobaSan.DBFieldsName = null;
            this.cbtn_UnpanJyugyobaSan.DefaultBackColor = System.Drawing.Color.Empty;
            this.cbtn_UnpanJyugyobaSan.DisplayItemName = null;
            this.cbtn_UnpanJyugyobaSan.DisplayPopUp = null;
            this.cbtn_UnpanJyugyobaSan.ErrorMessage = null;
            this.cbtn_UnpanJyugyobaSan.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_UnpanJyugyobaSan.FocusOutCheckMethod")));
            this.cbtn_UnpanJyugyobaSan.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.cbtn_UnpanJyugyobaSan.GetCodeMasterField = null;
            this.cbtn_UnpanJyugyobaSan.Image = ((System.Drawing.Image)(resources.GetObject("cbtn_UnpanJyugyobaSan.Image")));
            this.cbtn_UnpanJyugyobaSan.ItemDefinedTypes = null;
            this.cbtn_UnpanJyugyobaSan.LinkedSettingTextBox = null;
            this.cbtn_UnpanJyugyobaSan.LinkedTextBoxs = null;
            this.cbtn_UnpanJyugyobaSan.Location = new System.Drawing.Point(951, 68);
            this.cbtn_UnpanJyugyobaSan.Name = "cbtn_UnpanJyugyobaSan";
            this.cbtn_UnpanJyugyobaSan.PopupAfterExecute = null;
            this.cbtn_UnpanJyugyobaSan.PopupAfterExecuteMethod = "";
            this.cbtn_UnpanJyugyobaSan.PopupBeforeExecute = null;
            this.cbtn_UnpanJyugyobaSan.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.cbtn_UnpanJyugyobaSan.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cbtn_UnpanJyugyobaSan.PopupSearchSendParams")));
            this.cbtn_UnpanJyugyobaSan.PopupSetFormField = "cantxt_UnpanJyugyobaNameCd,ctxt_UnpanJyugyobaName,cantxt_SyobunJyutakuNameCd,ctxt" +
    "_SyobunJyutakuName";
            this.cbtn_UnpanJyugyobaSan.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.cbtn_UnpanJyugyobaSan.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.cbtn_UnpanJyugyobaSan.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cbtn_UnpanJyugyobaSan.popupWindowSetting")));
            this.cbtn_UnpanJyugyobaSan.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_UnpanJyugyobaSan.RegistCheckMethod")));
            this.cbtn_UnpanJyugyobaSan.SearchDisplayFlag = 0;
            this.cbtn_UnpanJyugyobaSan.SetFormField = "cantxt_UnpanJyugyobaNameCd,ctxt_UnpanJyugyobaName,cantxt_SyobunJyutakuNameCd,ctxt" +
    "_SyobunJyutakuName";
            this.cbtn_UnpanJyugyobaSan.ShortItemName = null;
            this.cbtn_UnpanJyugyobaSan.Size = new System.Drawing.Size(22, 22);
            this.cbtn_UnpanJyugyobaSan.TabIndex = 29;
            this.cbtn_UnpanJyugyobaSan.TabStop = false;
            this.cbtn_UnpanJyugyobaSan.Tag = "運搬先の事業場の検索を行います";
            this.cbtn_UnpanJyugyobaSan.Text = " ";
            this.cbtn_UnpanJyugyobaSan.UseVisualStyleBackColor = false;
            this.cbtn_UnpanJyugyobaSan.ZeroPaddengFlag = false;
            // 
            // cantxt_UnpanJyugyobaNameCd
            // 
            this.cantxt_UnpanJyugyobaNameCd.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_UnpanJyugyobaNameCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_UnpanJyugyobaNameCd.ChangeUpperCase = true;
            this.cantxt_UnpanJyugyobaNameCd.CharacterLimitList = null;
            this.cantxt_UnpanJyugyobaNameCd.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.cantxt_UnpanJyugyobaNameCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_UnpanJyugyobaNameCd.DisplayPopUp = null;
            this.cantxt_UnpanJyugyobaNameCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_UnpanJyugyobaNameCd.FocusOutCheckMethod")));
            this.cantxt_UnpanJyugyobaNameCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cantxt_UnpanJyugyobaNameCd.ForeColor = System.Drawing.Color.Black;
            this.cantxt_UnpanJyugyobaNameCd.GetCodeMasterField = "GENBA_CD,GENBA_NAME_RYAKU";
            this.cantxt_UnpanJyugyobaNameCd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cantxt_UnpanJyugyobaNameCd.IsInputErrorOccured = false;
            this.cantxt_UnpanJyugyobaNameCd.Location = new System.Drawing.Point(606, 69);
            this.cantxt_UnpanJyugyobaNameCd.MaxLength = 6;
            this.cantxt_UnpanJyugyobaNameCd.Name = "cantxt_UnpanJyugyobaNameCd";
            this.cantxt_UnpanJyugyobaNameCd.PopupAfterExecute = null;
            this.cantxt_UnpanJyugyobaNameCd.PopupAfterExecuteMethod = "";
            this.cantxt_UnpanJyugyobaNameCd.PopupBeforeExecute = null;
            this.cantxt_UnpanJyugyobaNameCd.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.cantxt_UnpanJyugyobaNameCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_UnpanJyugyobaNameCd.PopupSearchSendParams")));
            this.cantxt_UnpanJyugyobaNameCd.PopupSetFormField = "cantxt_UnpanJyugyobaNameCd,ctxt_UnpanJyugyobaName,cantxt_SyobunJyutakuNameCd,ctxt" +
    "_SyobunJyutakuName";
            this.cantxt_UnpanJyugyobaNameCd.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.cantxt_UnpanJyugyobaNameCd.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.cantxt_UnpanJyugyobaNameCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_UnpanJyugyobaNameCd.popupWindowSetting")));
            this.cantxt_UnpanJyugyobaNameCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_UnpanJyugyobaNameCd.RegistCheckMethod")));
            this.cantxt_UnpanJyugyobaNameCd.SetFormField = "cantxt_UnpanJyugyobaNameCd,ctxt_UnpanJyugyobaName,cantxt_SyobunJyutakuNameCd,ctxt" +
    "_SyobunJyutakuName";
            this.cantxt_UnpanJyugyobaNameCd.Size = new System.Drawing.Size(60, 20);
            this.cantxt_UnpanJyugyobaNameCd.TabIndex = 27;
            this.cantxt_UnpanJyugyobaNameCd.Tag = "半角6桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.cantxt_UnpanJyugyobaNameCd.ZeroPaddengFlag = true;
            this.cantxt_UnpanJyugyobaNameCd.Validated += new System.EventHandler(this.cantxt_UnpanJyugyobaNameCd_Validated);
            // 
            // ctxt_UnpanJyugyobaName
            // 
            this.ctxt_UnpanJyugyobaName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_UnpanJyugyobaName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_UnpanJyugyobaName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ctxt_UnpanJyugyobaName.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_UnpanJyugyobaName.DisplayPopUp = null;
            this.ctxt_UnpanJyugyobaName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_UnpanJyugyobaName.FocusOutCheckMethod")));
            this.ctxt_UnpanJyugyobaName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ctxt_UnpanJyugyobaName.ForeColor = System.Drawing.Color.Black;
            this.ctxt_UnpanJyugyobaName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctxt_UnpanJyugyobaName.IsInputErrorOccured = false;
            this.ctxt_UnpanJyugyobaName.Location = new System.Drawing.Point(665, 69);
            this.ctxt_UnpanJyugyobaName.MaxLength = 0;
            this.ctxt_UnpanJyugyobaName.Name = "ctxt_UnpanJyugyobaName";
            this.ctxt_UnpanJyugyobaName.PopupAfterExecute = null;
            this.ctxt_UnpanJyugyobaName.PopupBeforeExecute = null;
            this.ctxt_UnpanJyugyobaName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_UnpanJyugyobaName.PopupSearchSendParams")));
            this.ctxt_UnpanJyugyobaName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_UnpanJyugyobaName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_UnpanJyugyobaName.popupWindowSetting")));
            this.ctxt_UnpanJyugyobaName.ReadOnly = true;
            this.ctxt_UnpanJyugyobaName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_UnpanJyugyobaName.RegistCheckMethod")));
            this.ctxt_UnpanJyugyobaName.Size = new System.Drawing.Size(285, 20);
            this.ctxt_UnpanJyugyobaName.TabIndex = 28;
            this.ctxt_UnpanJyugyobaName.TabStop = false;
            this.ctxt_UnpanJyugyobaName.Tag = "";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(490, 69);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(110, 20);
            this.label12.TabIndex = 26;
            this.label12.Text = "処分事業場";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbtn_HokokushoBunruiSan
            // 
            this.cbtn_HokokushoBunruiSan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.cbtn_HokokushoBunruiSan.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cbtn_HokokushoBunruiSan.DBFieldsName = null;
            this.cbtn_HokokushoBunruiSan.DefaultBackColor = System.Drawing.Color.Empty;
            this.cbtn_HokokushoBunruiSan.DisplayItemName = null;
            this.cbtn_HokokushoBunruiSan.DisplayPopUp = null;
            this.cbtn_HokokushoBunruiSan.ErrorMessage = null;
            this.cbtn_HokokushoBunruiSan.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_HokokushoBunruiSan.FocusOutCheckMethod")));
            this.cbtn_HokokushoBunruiSan.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.cbtn_HokokushoBunruiSan.GetCodeMasterField = null;
            this.cbtn_HokokushoBunruiSan.Image = ((System.Drawing.Image)(resources.GetObject("cbtn_HokokushoBunruiSan.Image")));
            this.cbtn_HokokushoBunruiSan.ItemDefinedTypes = null;
            this.cbtn_HokokushoBunruiSan.LinkedSettingTextBox = null;
            this.cbtn_HokokushoBunruiSan.LinkedTextBoxs = null;
            this.cbtn_HokokushoBunruiSan.Location = new System.Drawing.Point(463, 91);
            this.cbtn_HokokushoBunruiSan.Name = "cbtn_HokokushoBunruiSan";
            this.cbtn_HokokushoBunruiSan.PopupAfterExecute = null;
            this.cbtn_HokokushoBunruiSan.PopupAfterExecuteMethod = "";
            this.cbtn_HokokushoBunruiSan.PopupBeforeExecute = null;
            this.cbtn_HokokushoBunruiSan.PopupGetMasterField = "HOUKOKUSHO_BUNRUI_CD,HOUKOKUSHO_BUNRUI_NAME_RYAKU";
            this.cbtn_HokokushoBunruiSan.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cbtn_HokokushoBunruiSan.PopupSearchSendParams")));
            this.cbtn_HokokushoBunruiSan.PopupSetFormField = "cantxt_HokokushoBunrui,ctxt_HokokushoBunrui";
            this.cbtn_HokokushoBunruiSan.PopupWindowId = r_framework.Const.WINDOW_ID.M_HOUKOKUSHO_BUNRUI;
            this.cbtn_HokokushoBunruiSan.PopupWindowName = "マスタ共通ポップアップ";
            this.cbtn_HokokushoBunruiSan.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cbtn_HokokushoBunruiSan.popupWindowSetting")));
            this.cbtn_HokokushoBunruiSan.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_HokokushoBunruiSan.RegistCheckMethod")));
            this.cbtn_HokokushoBunruiSan.SearchDisplayFlag = 0;
            this.cbtn_HokokushoBunruiSan.SetFormField = "cantxt_HokokushoBunrui,ctxt_HokokushoBunrui";
            this.cbtn_HokokushoBunruiSan.ShortItemName = null;
            this.cbtn_HokokushoBunruiSan.Size = new System.Drawing.Size(22, 22);
            this.cbtn_HokokushoBunruiSan.TabIndex = 34;
            this.cbtn_HokokushoBunruiSan.TabStop = false;
            this.cbtn_HokokushoBunruiSan.Tag = "報告書分類の検索を行います";
            this.cbtn_HokokushoBunruiSan.Text = " ";
            this.cbtn_HokokushoBunruiSan.UseVisualStyleBackColor = false;
            this.cbtn_HokokushoBunruiSan.ZeroPaddengFlag = false;
            // 
            // cantxt_HokokushoBunrui
            // 
            this.cantxt_HokokushoBunrui.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_HokokushoBunrui.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_HokokushoBunrui.ChangeUpperCase = true;
            this.cantxt_HokokushoBunrui.CharacterLimitList = null;
            this.cantxt_HokokushoBunrui.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.cantxt_HokokushoBunrui.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_HokokushoBunrui.DisplayPopUp = null;
            this.cantxt_HokokushoBunrui.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_HokokushoBunrui.FocusOutCheckMethod")));
            this.cantxt_HokokushoBunrui.ForeColor = System.Drawing.Color.Black;
            this.cantxt_HokokushoBunrui.GetCodeMasterField = "HOUKOKUSHO_BUNRUI_CD,HOUKOKUSHO_BUNRUI_NAME_RYAKU";
            this.cantxt_HokokushoBunrui.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cantxt_HokokushoBunrui.IsInputErrorOccured = false;
            this.cantxt_HokokushoBunrui.Location = new System.Drawing.Point(118, 92);
            this.cantxt_HokokushoBunrui.MaxLength = 6;
            this.cantxt_HokokushoBunrui.Name = "cantxt_HokokushoBunrui";
            this.cantxt_HokokushoBunrui.PopupAfterExecute = null;
            this.cantxt_HokokushoBunrui.PopupAfterExecuteMethod = "";
            this.cantxt_HokokushoBunrui.PopupBeforeExecute = null;
            this.cantxt_HokokushoBunrui.PopupGetMasterField = "HOUKOKUSHO_BUNRUI_CD,HOUKOKUSHO_BUNRUI_NAME_RYAKU";
            this.cantxt_HokokushoBunrui.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_HokokushoBunrui.PopupSearchSendParams")));
            this.cantxt_HokokushoBunrui.PopupSetFormField = "cantxt_HokokushoBunrui,ctxt_HokokushoBunrui";
            this.cantxt_HokokushoBunrui.PopupWindowId = r_framework.Const.WINDOW_ID.M_HOUKOKUSHO_BUNRUI;
            this.cantxt_HokokushoBunrui.PopupWindowName = "マスタ共通ポップアップ";
            this.cantxt_HokokushoBunrui.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_HokokushoBunrui.popupWindowSetting")));
            this.cantxt_HokokushoBunrui.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_HokokushoBunrui.RegistCheckMethod")));
            this.cantxt_HokokushoBunrui.SetFormField = "cantxt_HokokushoBunrui,ctxt_HokokushoBunrui";
            this.cantxt_HokokushoBunrui.Size = new System.Drawing.Size(60, 20);
            this.cantxt_HokokushoBunrui.TabIndex = 32;
            this.cantxt_HokokushoBunrui.Tag = "半角6桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.cantxt_HokokushoBunrui.ZeroPaddengFlag = true;
            this.cantxt_HokokushoBunrui.Validated += new System.EventHandler(this.cantxt_HokokushoBunrui_Validated);
            // 
            // ctxt_HokokushoBunrui
            // 
            this.ctxt_HokokushoBunrui.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_HokokushoBunrui.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_HokokushoBunrui.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ctxt_HokokushoBunrui.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_HokokushoBunrui.DisplayPopUp = null;
            this.ctxt_HokokushoBunrui.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_HokokushoBunrui.FocusOutCheckMethod")));
            this.ctxt_HokokushoBunrui.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ctxt_HokokushoBunrui.ForeColor = System.Drawing.Color.Black;
            this.ctxt_HokokushoBunrui.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctxt_HokokushoBunrui.IsInputErrorOccured = false;
            this.ctxt_HokokushoBunrui.Location = new System.Drawing.Point(177, 92);
            this.ctxt_HokokushoBunrui.MaxLength = 0;
            this.ctxt_HokokushoBunrui.Name = "ctxt_HokokushoBunrui";
            this.ctxt_HokokushoBunrui.PopupAfterExecute = null;
            this.ctxt_HokokushoBunrui.PopupBeforeExecute = null;
            this.ctxt_HokokushoBunrui.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_HokokushoBunrui.PopupSearchSendParams")));
            this.ctxt_HokokushoBunrui.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_HokokushoBunrui.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_HokokushoBunrui.popupWindowSetting")));
            this.ctxt_HokokushoBunrui.ReadOnly = true;
            this.ctxt_HokokushoBunrui.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_HokokushoBunrui.RegistCheckMethod")));
            this.ctxt_HokokushoBunrui.Size = new System.Drawing.Size(285, 20);
            this.ctxt_HokokushoBunrui.TabIndex = 33;
            this.ctxt_HokokushoBunrui.TabStop = false;
            this.ctxt_HokokushoBunrui.Tag = "";
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
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(962, 42);
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
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 21;
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
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 35;
            this.label1.Text = "処分方法";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SHOBUN_HOUHOU_MI
            // 
            this.SHOBUN_HOUHOU_MI.AutoSize = true;
            this.SHOBUN_HOUHOU_MI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.SHOBUN_HOUHOU_MI.DBFieldsName = "SHOBUN_HOUHOU_MI";
            this.SHOBUN_HOUHOU_MI.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHOBUN_HOUHOU_MI.DisplayItemName = "";
            this.SHOBUN_HOUHOU_MI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_HOUHOU_MI.FocusOutCheckMethod")));
            this.SHOBUN_HOUHOU_MI.ItemDefinedTypes = "bit";
            this.SHOBUN_HOUHOU_MI.Location = new System.Drawing.Point(352, 116);
            this.SHOBUN_HOUHOU_MI.Name = "SHOBUN_HOUHOU_MI";
            this.SHOBUN_HOUHOU_MI.PopupAfterExecute = null;
            this.SHOBUN_HOUHOU_MI.PopupBeforeExecute = null;
            this.SHOBUN_HOUHOU_MI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHOBUN_HOUHOU_MI.PopupSearchSendParams")));
            this.SHOBUN_HOUHOU_MI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHOBUN_HOUHOU_MI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHOBUN_HOUHOU_MI.popupWindowSetting")));
            this.SHOBUN_HOUHOU_MI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_HOUHOU_MI.RegistCheckMethod")));
            this.SHOBUN_HOUHOU_MI.ShortItemName = "";
            this.SHOBUN_HOUHOU_MI.Size = new System.Drawing.Size(180, 17);
            this.SHOBUN_HOUHOU_MI.TabIndex = 38;
            this.SHOBUN_HOUHOU_MI.Tag = " ";
            this.SHOBUN_HOUHOU_MI.Text = "処分方法未入力のみ表示";
            this.SHOBUN_HOUHOU_MI.UseVisualStyleBackColor = false;
            this.SHOBUN_HOUHOU_MI.CheckedChanged += new System.EventHandler(this.SHOBUN_HOUHOU_MI_CheckedChanged);
            // 
            // SHOBUN_HOUHOU_CD
            // 
            this.SHOBUN_HOUHOU_CD.BackColor = System.Drawing.SystemColors.Window;
            this.SHOBUN_HOUHOU_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHOBUN_HOUHOU_CD.ChangeUpperCase = true;
            this.SHOBUN_HOUHOU_CD.CharacterLimitList = null;
            this.SHOBUN_HOUHOU_CD.CharactersNumber = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.SHOBUN_HOUHOU_CD.DBFieldsName = "";
            this.SHOBUN_HOUHOU_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHOBUN_HOUHOU_CD.DisplayItemName = "処分方法";
            this.SHOBUN_HOUHOU_CD.DisplayPopUp = null;
            this.SHOBUN_HOUHOU_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_HOUHOU_CD.FocusOutCheckMethod")));
            this.SHOBUN_HOUHOU_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHOBUN_HOUHOU_CD.ForeColor = System.Drawing.Color.Black;
            this.SHOBUN_HOUHOU_CD.GetCodeMasterField = "SHOBUN_HOUHOU_CD,SHOBUN_HOUHOU_NAME_RYAKU";
            this.SHOBUN_HOUHOU_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SHOBUN_HOUHOU_CD.IsInputErrorOccured = false;
            this.SHOBUN_HOUHOU_CD.Location = new System.Drawing.Point(118, 115);
            this.SHOBUN_HOUHOU_CD.MaxLength = 3;
            this.SHOBUN_HOUHOU_CD.Name = "SHOBUN_HOUHOU_CD";
            this.SHOBUN_HOUHOU_CD.PopupAfterExecute = null;
            this.SHOBUN_HOUHOU_CD.PopupAfterExecuteMethod = "SHOBUN_HOUHOU_CD_PopupAfter";
            this.SHOBUN_HOUHOU_CD.PopupBeforeExecute = null;
            this.SHOBUN_HOUHOU_CD.PopupGetMasterField = "SHOBUN_HOUHOU_CD,SHOBUN_HOUHOU_NAME_RYAKU";
            this.SHOBUN_HOUHOU_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHOBUN_HOUHOU_CD.PopupSearchSendParams")));
            this.SHOBUN_HOUHOU_CD.PopupSetFormField = "SHOBUN_HOUHOU_CD,SHOBUN_HOUHOU_NAME";
            this.SHOBUN_HOUHOU_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHOBUN_HOUHOU;
            this.SHOBUN_HOUHOU_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.SHOBUN_HOUHOU_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHOBUN_HOUHOU_CD.popupWindowSetting")));
            this.SHOBUN_HOUHOU_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_HOUHOU_CD.RegistCheckMethod")));
            this.SHOBUN_HOUHOU_CD.SetFormField = "SHOBUN_HOUHOU_CD,SHOBUN_HOUHOU_NAME";
            this.SHOBUN_HOUHOU_CD.ShortItemName = "処分方法";
            this.SHOBUN_HOUHOU_CD.Size = new System.Drawing.Size(60, 20);
            this.SHOBUN_HOUHOU_CD.TabIndex = 36;
            this.SHOBUN_HOUHOU_CD.Tag = "処分方法を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.SHOBUN_HOUHOU_CD.ZeroPaddengFlag = true;
            this.SHOBUN_HOUHOU_CD.Validated += new System.EventHandler(this.SHOBUN_HOUHOU_Validated);
            // 
            // SHOBUN_HOUHOU_NAME
            // 
            this.SHOBUN_HOUHOU_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.SHOBUN_HOUHOU_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHOBUN_HOUHOU_NAME.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.SHOBUN_HOUHOU_NAME.DBFieldsName = "";
            this.SHOBUN_HOUHOU_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHOBUN_HOUHOU_NAME.DisplayPopUp = null;
            this.SHOBUN_HOUHOU_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_HOUHOU_NAME.FocusOutCheckMethod")));
            this.SHOBUN_HOUHOU_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHOBUN_HOUHOU_NAME.ForeColor = System.Drawing.Color.Black;
            this.SHOBUN_HOUHOU_NAME.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.SHOBUN_HOUHOU_NAME.IsInputErrorOccured = false;
            this.SHOBUN_HOUHOU_NAME.Location = new System.Drawing.Point(177, 115);
            this.SHOBUN_HOUHOU_NAME.MaxLength = 20;
            this.SHOBUN_HOUHOU_NAME.Name = "SHOBUN_HOUHOU_NAME";
            this.SHOBUN_HOUHOU_NAME.PopupAfterExecute = null;
            this.SHOBUN_HOUHOU_NAME.PopupBeforeExecute = null;
            this.SHOBUN_HOUHOU_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHOBUN_HOUHOU_NAME.PopupSearchSendParams")));
            this.SHOBUN_HOUHOU_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHOBUN_HOUHOU_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHOBUN_HOUHOU_NAME.popupWindowSetting")));
            this.SHOBUN_HOUHOU_NAME.ReadOnly = true;
            this.SHOBUN_HOUHOU_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_HOUHOU_NAME.RegistCheckMethod")));
            this.SHOBUN_HOUHOU_NAME.Size = new System.Drawing.Size(160, 20);
            this.SHOBUN_HOUHOU_NAME.TabIndex = 37;
            this.SHOBUN_HOUHOU_NAME.TabStop = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(490, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 18;
            this.label2.Text = "自社運搬";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // JISHA_UNPAN_KBN
            // 
            this.JISHA_UNPAN_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.JISHA_UNPAN_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JISHA_UNPAN_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.JISHA_UNPAN_KBN.DisplayItemName = "自社運搬";
            this.JISHA_UNPAN_KBN.DisplayPopUp = null;
            this.JISHA_UNPAN_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JISHA_UNPAN_KBN.FocusOutCheckMethod")));
            this.JISHA_UNPAN_KBN.ForeColor = System.Drawing.Color.Black;
            this.JISHA_UNPAN_KBN.IsInputErrorOccured = false;
            this.JISHA_UNPAN_KBN.LinkedRadioButtonArray = new string[] {
        "JISHA_UNPAN_KBN_1",
        "JISHA_UNPAN_KBN_2",
        "JISHA_UNPAN_KBN_3"};
            this.JISHA_UNPAN_KBN.Location = new System.Drawing.Point(606, 46);
            this.JISHA_UNPAN_KBN.Name = "JISHA_UNPAN_KBN";
            this.JISHA_UNPAN_KBN.PopupAfterExecute = null;
            this.JISHA_UNPAN_KBN.PopupBeforeExecute = null;
            this.JISHA_UNPAN_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JISHA_UNPAN_KBN.PopupSearchSendParams")));
            this.JISHA_UNPAN_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JISHA_UNPAN_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JISHA_UNPAN_KBN.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            3,
            0,
            0,
            0});
            rangeSettingDto3.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.JISHA_UNPAN_KBN.RangeSetting = rangeSettingDto3;
            this.JISHA_UNPAN_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JISHA_UNPAN_KBN.RegistCheckMethod")));
            this.JISHA_UNPAN_KBN.Size = new System.Drawing.Size(20, 20);
            this.JISHA_UNPAN_KBN.TabIndex = 19;
            this.JISHA_UNPAN_KBN.Tag = "【1～3】のいずれかで入力してください";
            this.JISHA_UNPAN_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.JISHA_UNPAN_KBN.WordWrap = false;
            // 
            // customPanel1
            // 
            this.customPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel1.Controls.Add(this.JISHA_UNPAN_KBN_3);
            this.customPanel1.Controls.Add(this.JISHA_UNPAN_KBN_2);
            this.customPanel1.Controls.Add(this.JISHA_UNPAN_KBN_1);
            this.customPanel1.Location = new System.Drawing.Point(625, 46);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(325, 20);
            this.customPanel1.TabIndex = 20;
            // 
            // JISHA_UNPAN_KBN_3
            // 
            this.JISHA_UNPAN_KBN_3.AutoSize = true;
            this.JISHA_UNPAN_KBN_3.Checked = true;
            this.JISHA_UNPAN_KBN_3.DefaultBackColor = System.Drawing.Color.Empty;
            this.JISHA_UNPAN_KBN_3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JISHA_UNPAN_KBN_3.FocusOutCheckMethod")));
            this.JISHA_UNPAN_KBN_3.LinkedTextBox = "JISHA_UNPAN_KBN";
            this.JISHA_UNPAN_KBN_3.Location = new System.Drawing.Point(223, 1);
            this.JISHA_UNPAN_KBN_3.Name = "JISHA_UNPAN_KBN_3";
            this.JISHA_UNPAN_KBN_3.PopupAfterExecute = null;
            this.JISHA_UNPAN_KBN_3.PopupBeforeExecute = null;
            this.JISHA_UNPAN_KBN_3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JISHA_UNPAN_KBN_3.PopupSearchSendParams")));
            this.JISHA_UNPAN_KBN_3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JISHA_UNPAN_KBN_3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JISHA_UNPAN_KBN_3.popupWindowSetting")));
            this.JISHA_UNPAN_KBN_3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JISHA_UNPAN_KBN_3.RegistCheckMethod")));
            this.JISHA_UNPAN_KBN_3.Size = new System.Drawing.Size(67, 17);
            this.JISHA_UNPAN_KBN_3.TabIndex = 7;
            this.JISHA_UNPAN_KBN_3.Tag = " ";
            this.JISHA_UNPAN_KBN_3.Text = "3.全て";
            this.JISHA_UNPAN_KBN_3.UseVisualStyleBackColor = true;
            this.JISHA_UNPAN_KBN_3.Value = "3";
            // 
            // JISHA_UNPAN_KBN_2
            // 
            this.JISHA_UNPAN_KBN_2.AutoSize = true;
            this.JISHA_UNPAN_KBN_2.Checked = true;
            this.JISHA_UNPAN_KBN_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.JISHA_UNPAN_KBN_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JISHA_UNPAN_KBN_2.FocusOutCheckMethod")));
            this.JISHA_UNPAN_KBN_2.LinkedTextBox = "JISHA_UNPAN_KBN";
            this.JISHA_UNPAN_KBN_2.Location = new System.Drawing.Point(100, 1);
            this.JISHA_UNPAN_KBN_2.Name = "JISHA_UNPAN_KBN_2";
            this.JISHA_UNPAN_KBN_2.PopupAfterExecute = null;
            this.JISHA_UNPAN_KBN_2.PopupBeforeExecute = null;
            this.JISHA_UNPAN_KBN_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JISHA_UNPAN_KBN_2.PopupSearchSendParams")));
            this.JISHA_UNPAN_KBN_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JISHA_UNPAN_KBN_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JISHA_UNPAN_KBN_2.popupWindowSetting")));
            this.JISHA_UNPAN_KBN_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JISHA_UNPAN_KBN_2.RegistCheckMethod")));
            this.JISHA_UNPAN_KBN_2.Size = new System.Drawing.Size(123, 17);
            this.JISHA_UNPAN_KBN_2.TabIndex = 6;
            this.JISHA_UNPAN_KBN_2.Tag = " ";
            this.JISHA_UNPAN_KBN_2.Text = "2.自社運搬除外";
            this.JISHA_UNPAN_KBN_2.UseVisualStyleBackColor = true;
            this.JISHA_UNPAN_KBN_2.Value = "2";
            // 
            // JISHA_UNPAN_KBN_1
            // 
            this.JISHA_UNPAN_KBN_1.AutoSize = true;
            this.JISHA_UNPAN_KBN_1.Checked = true;
            this.JISHA_UNPAN_KBN_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.JISHA_UNPAN_KBN_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JISHA_UNPAN_KBN_1.FocusOutCheckMethod")));
            this.JISHA_UNPAN_KBN_1.LinkedTextBox = "JISHA_UNPAN_KBN";
            this.JISHA_UNPAN_KBN_1.Location = new System.Drawing.Point(5, 1);
            this.JISHA_UNPAN_KBN_1.Name = "JISHA_UNPAN_KBN_1";
            this.JISHA_UNPAN_KBN_1.PopupAfterExecute = null;
            this.JISHA_UNPAN_KBN_1.PopupBeforeExecute = null;
            this.JISHA_UNPAN_KBN_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JISHA_UNPAN_KBN_1.PopupSearchSendParams")));
            this.JISHA_UNPAN_KBN_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JISHA_UNPAN_KBN_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JISHA_UNPAN_KBN_1.popupWindowSetting")));
            this.JISHA_UNPAN_KBN_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JISHA_UNPAN_KBN_1.RegistCheckMethod")));
            this.JISHA_UNPAN_KBN_1.Size = new System.Drawing.Size(95, 17);
            this.JISHA_UNPAN_KBN_1.TabIndex = 5;
            this.JISHA_UNPAN_KBN_1.Tag = " ";
            this.JISHA_UNPAN_KBN_1.Text = "1.自社運搬";
            this.JISHA_UNPAN_KBN_1.UseVisualStyleBackColor = true;
            this.JISHA_UNPAN_KBN_1.Value = "1";
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 472);
            this.Controls.Add(this.customPanel1);
            this.Controls.Add(this.JISHA_UNPAN_KBN);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SHOBUN_HOUHOU_NAME);
            this.Controls.Add(this.SHOBUN_HOUHOU_CD);
            this.Controls.Add(this.SHOBUN_HOUHOU_MI);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.cbtn_HokokushoBunruiSan);
            this.Controls.Add(this.cantxt_HokokushoBunrui);
            this.Controls.Add(this.ctxt_HokokushoBunrui);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cbtn_UnpanJyugyobaSan);
            this.Controls.Add(this.cantxt_UnpanJyugyobaNameCd);
            this.Controls.Add(this.ctxt_UnpanJyugyobaName);
            this.Controls.Add(this.cbtn_SyobunJyutakuSan);
            this.Controls.Add(this.cantxt_SyobunJyutakuNameCd);
            this.Controls.Add(this.ctxt_SyobunJyutakuName);
            this.Controls.Add(this.cbtn_UnpanJyutaku1San);
            this.Controls.Add(this.cantxt_UnpanJyutakuNameCd);
            this.Controls.Add(this.ctxt_UnpanJyutakuName);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cbtn_HaisyutuJigyoubaSan);
            this.Controls.Add(this.cantxt_HaisyutuJigyoubaName);
            this.Controls.Add(this.ctxt_HaisyutuJigyoubaName);
            this.Controls.Add(this.cbtn_HaisyutuGyousyaSan);
            this.Controls.Add(this.cantxt_HaisyutuGyousyaCd);
            this.Controls.Add(this.ctxt_HaisyutuGyousyaName);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.KOUFU_DATE_KBN);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.KOUFU_DATE_FROM);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.KOUFU_DATE_TO);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Name = "UIForm";
            this.Text = "UIForm";
            this.Controls.SetChildIndex(this.KOUFU_DATE_TO, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.KOUFU_DATE_FROM, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.KOUFU_DATE_KBN, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.ctxt_HaisyutuGyousyaName, 0);
            this.Controls.SetChildIndex(this.cantxt_HaisyutuGyousyaCd, 0);
            this.Controls.SetChildIndex(this.cbtn_HaisyutuGyousyaSan, 0);
            this.Controls.SetChildIndex(this.ctxt_HaisyutuJigyoubaName, 0);
            this.Controls.SetChildIndex(this.cantxt_HaisyutuJigyoubaName, 0);
            this.Controls.SetChildIndex(this.cbtn_HaisyutuJigyoubaSan, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.ctxt_UnpanJyutakuName, 0);
            this.Controls.SetChildIndex(this.cantxt_UnpanJyutakuNameCd, 0);
            this.Controls.SetChildIndex(this.cbtn_UnpanJyutaku1San, 0);
            this.Controls.SetChildIndex(this.ctxt_SyobunJyutakuName, 0);
            this.Controls.SetChildIndex(this.cantxt_SyobunJyutakuNameCd, 0);
            this.Controls.SetChildIndex(this.cbtn_SyobunJyutakuSan, 0);
            this.Controls.SetChildIndex(this.ctxt_UnpanJyugyobaName, 0);
            this.Controls.SetChildIndex(this.cantxt_UnpanJyugyobaNameCd, 0);
            this.Controls.SetChildIndex(this.cbtn_UnpanJyugyobaSan, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.ctxt_HokokushoBunrui, 0);
            this.Controls.SetChildIndex(this.cantxt_HokokushoBunrui, 0);
            this.Controls.SetChildIndex(this.cbtn_HokokushoBunruiSan, 0);
            this.Controls.SetChildIndex(this.ISNOT_NEED_DELETE_FLG, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.SHOBUN_HOUHOU_MI, 0);
            this.Controls.SetChildIndex(this.SHOBUN_HOUHOU_CD, 0);
            this.Controls.SetChildIndex(this.SHOBUN_HOUHOU_NAME, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.JISHA_UNPAN_KBN, 0);
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.customPanel1, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private r_framework.CustomControl.CustomPanel panel1;
        internal r_framework.CustomControl.CustomDateTimePicker KOUFU_DATE_FROM;
        internal r_framework.CustomControl.CustomDateTimePicker KOUFU_DATE_TO;
        private r_framework.CustomControl.CustomRadioButton KOUFU_DATE_KBN_2;
        private r_framework.CustomControl.CustomRadioButton KOUFU_DATE_KBN_1;
        private r_framework.CustomControl.CustomRadioButton KOUFU_DATE_KBN_3;
        private r_framework.CustomControl.CustomRadioButton KOUFU_DATE_KBN_4;
        internal r_framework.CustomControl.CustomPopupOpenButton cbtn_HaisyutuGyousyaSan;
        internal r_framework.CustomControl.CustomAlphaNumTextBox cantxt_HaisyutuGyousyaCd;
        internal r_framework.CustomControl.CustomTextBox ctxt_HaisyutuGyousyaName;
        internal r_framework.CustomControl.CustomPopupOpenButton cbtn_HaisyutuJigyoubaSan;
        internal r_framework.CustomControl.CustomAlphaNumTextBox cantxt_HaisyutuJigyoubaName;
        internal r_framework.CustomControl.CustomTextBox ctxt_HaisyutuJigyoubaName;
        internal r_framework.CustomControl.CustomPopupOpenButton cbtn_UnpanJyutaku1San;
        internal r_framework.CustomControl.CustomAlphaNumTextBox cantxt_UnpanJyutakuNameCd;
        internal r_framework.CustomControl.CustomTextBox ctxt_UnpanJyutakuName;
        internal r_framework.CustomControl.CustomPopupOpenButton cbtn_SyobunJyutakuSan;
        internal r_framework.CustomControl.CustomAlphaNumTextBox cantxt_SyobunJyutakuNameCd;
        internal r_framework.CustomControl.CustomTextBox ctxt_SyobunJyutakuName;
        internal r_framework.CustomControl.CustomPopupOpenButton cbtn_UnpanJyugyobaSan;
        internal r_framework.CustomControl.CustomAlphaNumTextBox cantxt_UnpanJyugyobaNameCd;
        internal r_framework.CustomControl.CustomTextBox ctxt_UnpanJyugyobaName;
        internal r_framework.CustomControl.CustomPopupOpenButton cbtn_HokokushoBunruiSan;
        internal r_framework.CustomControl.CustomAlphaNumTextBox cantxt_HokokushoBunrui;
        internal r_framework.CustomControl.CustomTextBox ctxt_HokokushoBunrui;
        public r_framework.CustomControl.CustomNumericTextBox2 KOUFU_DATE_KBN;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.Label label10;
        public System.Windows.Forms.Label label11;
        public System.Windows.Forms.Label label12;
        public System.Windows.Forms.Label label3;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
        public System.Windows.Forms.Label label1;
        internal r_framework.CustomControl.CustomCheckBox SHOBUN_HOUHOU_MI;
        internal r_framework.CustomControl.CustomAlphaNumTextBox SHOBUN_HOUHOU_CD;
        internal r_framework.CustomControl.CustomTextBox SHOBUN_HOUHOU_NAME;
        public System.Windows.Forms.Label label2;
        public r_framework.CustomControl.CustomNumericTextBox2 JISHA_UNPAN_KBN;
        private r_framework.CustomControl.CustomPanel customPanel1;
        private r_framework.CustomControl.CustomRadioButton JISHA_UNPAN_KBN_1;
        private r_framework.CustomControl.CustomRadioButton JISHA_UNPAN_KBN_2;
        private r_framework.CustomControl.CustomRadioButton JISHA_UNPAN_KBN_3;
    }
}