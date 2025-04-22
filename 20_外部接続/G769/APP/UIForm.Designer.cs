namespace Shougun.Core.ExternalConnection.SmsResult
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
            this.label3 = new System.Windows.Forms.Label();
            this.DATE_TO = new r_framework.CustomControl.CustomDateTimePicker();
            this.DATE_FROM = new r_framework.CustomControl.CustomDateTimePicker();
            this.TEKIYOU_LABEL = new System.Windows.Forms.Label();
            this.denpyouShuruiPanel = new System.Windows.Forms.Panel();
            this.rb_DENPYOU_SHURUI_9 = new r_framework.CustomControl.CustomRadioButton();
            this.rb_DENPYOU_SHURUI_6 = new r_framework.CustomControl.CustomRadioButton();
            this.rb_DENPYOU_SHURUI_5 = new r_framework.CustomControl.CustomRadioButton();
            this.rb_DENPYOU_SHURUI_4 = new r_framework.CustomControl.CustomRadioButton();
            this.rb_DENPYOU_SHURUI_3 = new r_framework.CustomControl.CustomRadioButton();
            this.SMS_DENPYOU_SHURUI = new r_framework.CustomControl.CustomNumericTextBox2();
            this.rb_DENPYOU_SHURUI_2 = new r_framework.CustomControl.CustomRadioButton();
            this.rb_DENPYOU_SHURUI_1 = new r_framework.CustomControl.CustomRadioButton();
            this.label138 = new System.Windows.Forms.Label();
            this.haishaJokyoPanel = new System.Windows.Forms.Panel();
            this.rb_RECEIVER_STATUS_9 = new r_framework.CustomControl.CustomRadioButton();
            this.rb_RECEIVER_STATUS_4 = new r_framework.CustomControl.CustomRadioButton();
            this.rb_RECEIVER_STATUS_3 = new r_framework.CustomControl.CustomRadioButton();
            this.rb_RECEIVER_STATUS_2 = new r_framework.CustomControl.CustomRadioButton();
            this.SMS_RECEIVER_STATUS = new r_framework.CustomControl.CustomNumericTextBox2();
            this.rb_RECEIVER_STATUS_1 = new r_framework.CustomControl.CustomRadioButton();
            this.rb_RECEIVER_STATUS_0 = new r_framework.CustomControl.CustomRadioButton();
            this.label139 = new System.Windows.Forms.Label();
            this.GENBA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.GENBA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.lable5 = new System.Windows.Forms.Label();
            this.GYOUSHA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.lable3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.customRadioButton1 = new r_framework.CustomControl.CustomRadioButton();
            this.DATE_SHURUI = new r_framework.CustomControl.CustomNumericTextBox2();
            this.rb_DATE_SHURUI_2 = new r_framework.CustomControl.CustomRadioButton();
            this.rb_DATE_SHURUI_1 = new r_framework.CustomControl.CustomRadioButton();
            this.denpyouShuruiPanel.SuspendLayout();
            this.haishaJokyoPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.searchString.Cursor = System.Windows.Forms.Cursors.Default;
            this.searchString.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.searchString.ForeColor = System.Drawing.SystemColors.ControlText;
            this.searchString.Location = new System.Drawing.Point(0, 0);
            this.searchString.TabStop = false;
            this.searchString.Visible = false;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Location = new System.Drawing.Point(3, 40);
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Location = new System.Drawing.Point(204, 40);
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Location = new System.Drawing.Point(405, 40);
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Location = new System.Drawing.Point(606, 40);
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Location = new System.Drawing.Point(807, 40);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.Location = new System.Drawing.Point(258, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "～";
            // 
            // DATE_TO
            // 
            this.DATE_TO.BackColor = System.Drawing.SystemColors.Window;
            this.DATE_TO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DATE_TO.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.DATE_TO.Checked = false;
            this.DATE_TO.DateTimeNowYear = "";
            this.DATE_TO.DBFieldsName = "";
            this.DATE_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_TO.DisplayItemName = "日付(終了日)";
            this.DATE_TO.DisplayPopUp = null;
            this.DATE_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_TO.FocusOutCheckMethod")));
            this.DATE_TO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.DATE_TO.ForeColor = System.Drawing.Color.Black;
            this.DATE_TO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DATE_TO.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.DATE_TO.IsInputErrorOccured = false;
            this.DATE_TO.ItemDefinedTypes = "datetime";
            this.DATE_TO.Location = new System.Drawing.Point(285, 1);
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
            this.DATE_TO.ShortItemName = "日付(終了日)";
            this.DATE_TO.Size = new System.Drawing.Size(124, 20);
            this.DATE_TO.TabIndex = 40;
            this.DATE_TO.Tag = "日付(終了日)を入力して下さい";
            this.DATE_TO.Value = null;
            this.DATE_TO.DoubleClick += new System.EventHandler(this.DATE_TO_DoubleClick);
            // 
            // DATE_FROM
            // 
            this.DATE_FROM.BackColor = System.Drawing.SystemColors.Window;
            this.DATE_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DATE_FROM.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.DATE_FROM.Checked = false;
            this.DATE_FROM.DateTimeNowYear = "";
            this.DATE_FROM.DBFieldsName = "";
            this.DATE_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_FROM.DisplayItemName = "日付(開始日)";
            this.DATE_FROM.DisplayPopUp = null;
            this.DATE_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_FROM.FocusOutCheckMethod")));
            this.DATE_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.DATE_FROM.ForeColor = System.Drawing.Color.Black;
            this.DATE_FROM.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DATE_FROM.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.DATE_FROM.IsInputErrorOccured = false;
            this.DATE_FROM.ItemDefinedTypes = "datetime";
            this.DATE_FROM.Location = new System.Drawing.Point(126, 1);
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
            this.DATE_FROM.ShortItemName = "日付(開始日)";
            this.DATE_FROM.Size = new System.Drawing.Size(124, 20);
            this.DATE_FROM.TabIndex = 20;
            this.DATE_FROM.Tag = "日付(開始日)を入力して下さい";
            this.DATE_FROM.Value = null;
            // 
            // TEKIYOU_LABEL
            // 
            this.TEKIYOU_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.TEKIYOU_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TEKIYOU_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TEKIYOU_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TEKIYOU_LABEL.ForeColor = System.Drawing.Color.White;
            this.TEKIYOU_LABEL.Location = new System.Drawing.Point(12, 1);
            this.TEKIYOU_LABEL.Name = "TEKIYOU_LABEL";
            this.TEKIYOU_LABEL.Size = new System.Drawing.Size(110, 20);
            this.TEKIYOU_LABEL.TabIndex = 10;
            this.TEKIYOU_LABEL.Text = "日付※";
            this.TEKIYOU_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // denpyouShuruiPanel
            // 
            this.denpyouShuruiPanel.Controls.Add(this.rb_DENPYOU_SHURUI_9);
            this.denpyouShuruiPanel.Controls.Add(this.rb_DENPYOU_SHURUI_6);
            this.denpyouShuruiPanel.Controls.Add(this.rb_DENPYOU_SHURUI_5);
            this.denpyouShuruiPanel.Controls.Add(this.rb_DENPYOU_SHURUI_4);
            this.denpyouShuruiPanel.Controls.Add(this.rb_DENPYOU_SHURUI_3);
            this.denpyouShuruiPanel.Controls.Add(this.SMS_DENPYOU_SHURUI);
            this.denpyouShuruiPanel.Controls.Add(this.rb_DENPYOU_SHURUI_2);
            this.denpyouShuruiPanel.Controls.Add(this.rb_DENPYOU_SHURUI_1);
            this.denpyouShuruiPanel.Location = new System.Drawing.Point(127, 24);
            this.denpyouShuruiPanel.Name = "denpyouShuruiPanel";
            this.denpyouShuruiPanel.Size = new System.Drawing.Size(657, 20);
            this.denpyouShuruiPanel.TabIndex = 873;
            // 
            // rb_DENPYOU_SHURUI_9
            // 
            this.rb_DENPYOU_SHURUI_9.AutoSize = true;
            this.rb_DENPYOU_SHURUI_9.DefaultBackColor = System.Drawing.Color.Empty;
            this.rb_DENPYOU_SHURUI_9.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_DENPYOU_SHURUI_9.FocusOutCheckMethod")));
            this.rb_DENPYOU_SHURUI_9.LinkedTextBox = "SMS_DENPYOU_SHURUI";
            this.rb_DENPYOU_SHURUI_9.Location = new System.Drawing.Point(571, 3);
            this.rb_DENPYOU_SHURUI_9.Name = "rb_DENPYOU_SHURUI_9";
            this.rb_DENPYOU_SHURUI_9.PopupAfterExecute = null;
            this.rb_DENPYOU_SHURUI_9.PopupBeforeExecute = null;
            this.rb_DENPYOU_SHURUI_9.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rb_DENPYOU_SHURUI_9.PopupSearchSendParams")));
            this.rb_DENPYOU_SHURUI_9.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rb_DENPYOU_SHURUI_9.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rb_DENPYOU_SHURUI_9.popupWindowSetting")));
            this.rb_DENPYOU_SHURUI_9.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_DENPYOU_SHURUI_9.RegistCheckMethod")));
            this.rb_DENPYOU_SHURUI_9.Size = new System.Drawing.Size(56, 16);
            this.rb_DENPYOU_SHURUI_9.TabIndex = 880;
            this.rb_DENPYOU_SHURUI_9.Tag = "伝票種類を指定しない場合にチェックを付けて下さい";
            this.rb_DENPYOU_SHURUI_9.Text = "9. 全て";
            this.rb_DENPYOU_SHURUI_9.UseVisualStyleBackColor = true;
            this.rb_DENPYOU_SHURUI_9.Value = "9";
            // 
            // rb_DENPYOU_SHURUI_6
            // 
            this.rb_DENPYOU_SHURUI_6.AutoSize = true;
            this.rb_DENPYOU_SHURUI_6.DefaultBackColor = System.Drawing.Color.Empty;
            this.rb_DENPYOU_SHURUI_6.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_DENPYOU_SHURUI_6.FocusOutCheckMethod")));
            this.rb_DENPYOU_SHURUI_6.LinkedTextBox = "SMS_DENPYOU_SHURUI";
            this.rb_DENPYOU_SHURUI_6.Location = new System.Drawing.Point(491, 3);
            this.rb_DENPYOU_SHURUI_6.Name = "rb_DENPYOU_SHURUI_6";
            this.rb_DENPYOU_SHURUI_6.PopupAfterExecute = null;
            this.rb_DENPYOU_SHURUI_6.PopupBeforeExecute = null;
            this.rb_DENPYOU_SHURUI_6.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rb_DENPYOU_SHURUI_6.PopupSearchSendParams")));
            this.rb_DENPYOU_SHURUI_6.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rb_DENPYOU_SHURUI_6.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rb_DENPYOU_SHURUI_6.popupWindowSetting")));
            this.rb_DENPYOU_SHURUI_6.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_DENPYOU_SHURUI_6.RegistCheckMethod")));
            this.rb_DENPYOU_SHURUI_6.Size = new System.Drawing.Size(59, 16);
            this.rb_DENPYOU_SHURUI_6.TabIndex = 879;
            this.rb_DENPYOU_SHURUI_6.Tag = "伝票種類が定期配車である場合にチェックを付けて下さい";
            this.rb_DENPYOU_SHURUI_6.Text = "6. 定期";
            this.rb_DENPYOU_SHURUI_6.UseVisualStyleBackColor = true;
            this.rb_DENPYOU_SHURUI_6.Value = "6";
            // 
            // rb_DENPYOU_SHURUI_5
            // 
            this.rb_DENPYOU_SHURUI_5.AutoSize = true;
            this.rb_DENPYOU_SHURUI_5.DefaultBackColor = System.Drawing.Color.Empty;
            this.rb_DENPYOU_SHURUI_5.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_DENPYOU_SHURUI_5.FocusOutCheckMethod")));
            this.rb_DENPYOU_SHURUI_5.LinkedTextBox = "SMS_DENPYOU_SHURUI";
            this.rb_DENPYOU_SHURUI_5.Location = new System.Drawing.Point(381, 3);
            this.rb_DENPYOU_SHURUI_5.Name = "rb_DENPYOU_SHURUI_5";
            this.rb_DENPYOU_SHURUI_5.PopupAfterExecute = null;
            this.rb_DENPYOU_SHURUI_5.PopupBeforeExecute = null;
            this.rb_DENPYOU_SHURUI_5.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rb_DENPYOU_SHURUI_5.PopupSearchSendParams")));
            this.rb_DENPYOU_SHURUI_5.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rb_DENPYOU_SHURUI_5.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rb_DENPYOU_SHURUI_5.popupWindowSetting")));
            this.rb_DENPYOU_SHURUI_5.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_DENPYOU_SHURUI_5.RegistCheckMethod")));
            this.rb_DENPYOU_SHURUI_5.Size = new System.Drawing.Size(89, 16);
            this.rb_DENPYOU_SHURUI_5.TabIndex = 878;
            this.rb_DENPYOU_SHURUI_5.Tag = "伝票種類が収集+持込である場合にチェックを付けて下さい";
            this.rb_DENPYOU_SHURUI_5.Text = "5. 収集+持込";
            this.rb_DENPYOU_SHURUI_5.UseVisualStyleBackColor = true;
            this.rb_DENPYOU_SHURUI_5.Value = "5";
            // 
            // rb_DENPYOU_SHURUI_4
            // 
            this.rb_DENPYOU_SHURUI_4.AutoSize = true;
            this.rb_DENPYOU_SHURUI_4.DefaultBackColor = System.Drawing.Color.Empty;
            this.rb_DENPYOU_SHURUI_4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_DENPYOU_SHURUI_4.FocusOutCheckMethod")));
            this.rb_DENPYOU_SHURUI_4.LinkedTextBox = "SMS_DENPYOU_SHURUI";
            this.rb_DENPYOU_SHURUI_4.Location = new System.Drawing.Point(271, 3);
            this.rb_DENPYOU_SHURUI_4.Name = "rb_DENPYOU_SHURUI_4";
            this.rb_DENPYOU_SHURUI_4.PopupAfterExecute = null;
            this.rb_DENPYOU_SHURUI_4.PopupBeforeExecute = null;
            this.rb_DENPYOU_SHURUI_4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rb_DENPYOU_SHURUI_4.PopupSearchSendParams")));
            this.rb_DENPYOU_SHURUI_4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rb_DENPYOU_SHURUI_4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rb_DENPYOU_SHURUI_4.popupWindowSetting")));
            this.rb_DENPYOU_SHURUI_4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_DENPYOU_SHURUI_4.RegistCheckMethod")));
            this.rb_DENPYOU_SHURUI_4.Size = new System.Drawing.Size(89, 16);
            this.rb_DENPYOU_SHURUI_4.TabIndex = 877;
            this.rb_DENPYOU_SHURUI_4.Tag = "伝票種類が収集+出荷である場合にチェックを付けて下さい";
            this.rb_DENPYOU_SHURUI_4.Text = "4. 収集+出荷";
            this.rb_DENPYOU_SHURUI_4.UseVisualStyleBackColor = true;
            this.rb_DENPYOU_SHURUI_4.Value = "4";
            // 
            // rb_DENPYOU_SHURUI_3
            // 
            this.rb_DENPYOU_SHURUI_3.AutoSize = true;
            this.rb_DENPYOU_SHURUI_3.DefaultBackColor = System.Drawing.Color.Empty;
            this.rb_DENPYOU_SHURUI_3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_DENPYOU_SHURUI_3.FocusOutCheckMethod")));
            this.rb_DENPYOU_SHURUI_3.LinkedTextBox = "SMS_DENPYOU_SHURUI";
            this.rb_DENPYOU_SHURUI_3.Location = new System.Drawing.Point(191, 3);
            this.rb_DENPYOU_SHURUI_3.Name = "rb_DENPYOU_SHURUI_3";
            this.rb_DENPYOU_SHURUI_3.PopupAfterExecute = null;
            this.rb_DENPYOU_SHURUI_3.PopupBeforeExecute = null;
            this.rb_DENPYOU_SHURUI_3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rb_DENPYOU_SHURUI_3.PopupSearchSendParams")));
            this.rb_DENPYOU_SHURUI_3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rb_DENPYOU_SHURUI_3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rb_DENPYOU_SHURUI_3.popupWindowSetting")));
            this.rb_DENPYOU_SHURUI_3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_DENPYOU_SHURUI_3.RegistCheckMethod")));
            this.rb_DENPYOU_SHURUI_3.Size = new System.Drawing.Size(59, 16);
            this.rb_DENPYOU_SHURUI_3.TabIndex = 876;
            this.rb_DENPYOU_SHURUI_3.Tag = "伝票種類が持込受付である場合にチェックを付けて下さい";
            this.rb_DENPYOU_SHURUI_3.Text = "3. 持込";
            this.rb_DENPYOU_SHURUI_3.UseVisualStyleBackColor = true;
            this.rb_DENPYOU_SHURUI_3.Value = "3";
            // 
            // SMS_DENPYOU_SHURUI
            // 
            this.SMS_DENPYOU_SHURUI.BackColor = System.Drawing.SystemColors.Window;
            this.SMS_DENPYOU_SHURUI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SMS_DENPYOU_SHURUI.DBFieldsName = "";
            this.SMS_DENPYOU_SHURUI.DefaultBackColor = System.Drawing.Color.Empty;
            this.SMS_DENPYOU_SHURUI.DisplayItemName = "伝票種類";
            this.SMS_DENPYOU_SHURUI.DisplayPopUp = null;
            this.SMS_DENPYOU_SHURUI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SMS_DENPYOU_SHURUI.FocusOutCheckMethod")));
            this.SMS_DENPYOU_SHURUI.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SMS_DENPYOU_SHURUI.ForeColor = System.Drawing.Color.Black;
            this.SMS_DENPYOU_SHURUI.IsInputErrorOccured = false;
            this.SMS_DENPYOU_SHURUI.ItemDefinedTypes = "smallint";
            this.SMS_DENPYOU_SHURUI.LinkedRadioButtonArray = new string[] {
        "rb_DENPYOU_SHURUI_1",
        "rb_DENPYOU_SHURUI_2",
        "rb_DENPYOU_SHURUI_3",
        "rb_DENPYOU_SHURUI_4",
        "rb_DENPYOU_SHURUI_5",
        "rb_DENPYOU_SHURUI_6",
        "rb_DENPYOU_SHURUI_9"};
            this.SMS_DENPYOU_SHURUI.Location = new System.Drawing.Point(4, 0);
            this.SMS_DENPYOU_SHURUI.Name = "SMS_DENPYOU_SHURUI";
            this.SMS_DENPYOU_SHURUI.PopupAfterExecute = null;
            this.SMS_DENPYOU_SHURUI.PopupBeforeExecute = null;
            this.SMS_DENPYOU_SHURUI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SMS_DENPYOU_SHURUI.PopupSearchSendParams")));
            this.SMS_DENPYOU_SHURUI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SMS_DENPYOU_SHURUI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SMS_DENPYOU_SHURUI.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            9,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SMS_DENPYOU_SHURUI.RangeSetting = rangeSettingDto1;
            this.SMS_DENPYOU_SHURUI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SMS_DENPYOU_SHURUI.RegistCheckMethod")));
            this.SMS_DENPYOU_SHURUI.Size = new System.Drawing.Size(20, 20);
            this.SMS_DENPYOU_SHURUI.TabIndex = 873;
            this.SMS_DENPYOU_SHURUI.Tag = "【1~9】のいずれかで入力して下さい";
            this.SMS_DENPYOU_SHURUI.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.SMS_DENPYOU_SHURUI.WordWrap = false;
            // 
            // rb_DENPYOU_SHURUI_2
            // 
            this.rb_DENPYOU_SHURUI_2.AutoSize = true;
            this.rb_DENPYOU_SHURUI_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.rb_DENPYOU_SHURUI_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_DENPYOU_SHURUI_2.FocusOutCheckMethod")));
            this.rb_DENPYOU_SHURUI_2.LinkedTextBox = "SMS_DENPYOU_SHURUI";
            this.rb_DENPYOU_SHURUI_2.Location = new System.Drawing.Point(111, 2);
            this.rb_DENPYOU_SHURUI_2.Name = "rb_DENPYOU_SHURUI_2";
            this.rb_DENPYOU_SHURUI_2.PopupAfterExecute = null;
            this.rb_DENPYOU_SHURUI_2.PopupBeforeExecute = null;
            this.rb_DENPYOU_SHURUI_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rb_DENPYOU_SHURUI_2.PopupSearchSendParams")));
            this.rb_DENPYOU_SHURUI_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rb_DENPYOU_SHURUI_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rb_DENPYOU_SHURUI_2.popupWindowSetting")));
            this.rb_DENPYOU_SHURUI_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_DENPYOU_SHURUI_2.RegistCheckMethod")));
            this.rb_DENPYOU_SHURUI_2.Size = new System.Drawing.Size(59, 16);
            this.rb_DENPYOU_SHURUI_2.TabIndex = 875;
            this.rb_DENPYOU_SHURUI_2.Tag = "伝票種類が出荷受付である場合にチェックを付けて下さい";
            this.rb_DENPYOU_SHURUI_2.Text = "2. 出荷";
            this.rb_DENPYOU_SHURUI_2.UseVisualStyleBackColor = true;
            this.rb_DENPYOU_SHURUI_2.Value = "2";
            // 
            // rb_DENPYOU_SHURUI_1
            // 
            this.rb_DENPYOU_SHURUI_1.AutoSize = true;
            this.rb_DENPYOU_SHURUI_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.rb_DENPYOU_SHURUI_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_DENPYOU_SHURUI_1.FocusOutCheckMethod")));
            this.rb_DENPYOU_SHURUI_1.LinkedTextBox = "SMS_DENPYOU_SHURUI";
            this.rb_DENPYOU_SHURUI_1.Location = new System.Drawing.Point(31, 3);
            this.rb_DENPYOU_SHURUI_1.Name = "rb_DENPYOU_SHURUI_1";
            this.rb_DENPYOU_SHURUI_1.PopupAfterExecute = null;
            this.rb_DENPYOU_SHURUI_1.PopupBeforeExecute = null;
            this.rb_DENPYOU_SHURUI_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rb_DENPYOU_SHURUI_1.PopupSearchSendParams")));
            this.rb_DENPYOU_SHURUI_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rb_DENPYOU_SHURUI_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rb_DENPYOU_SHURUI_1.popupWindowSetting")));
            this.rb_DENPYOU_SHURUI_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_DENPYOU_SHURUI_1.RegistCheckMethod")));
            this.rb_DENPYOU_SHURUI_1.Size = new System.Drawing.Size(59, 16);
            this.rb_DENPYOU_SHURUI_1.TabIndex = 874;
            this.rb_DENPYOU_SHURUI_1.Tag = "伝票種類が収集受付である場合にチェックを付けて下さい";
            this.rb_DENPYOU_SHURUI_1.Text = "1. 収集";
            this.rb_DENPYOU_SHURUI_1.UseVisualStyleBackColor = true;
            this.rb_DENPYOU_SHURUI_1.Value = "1";
            // 
            // label138
            // 
            this.label138.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label138.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label138.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label138.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label138.ForeColor = System.Drawing.Color.White;
            this.label138.Location = new System.Drawing.Point(12, 25);
            this.label138.Name = "label138";
            this.label138.Size = new System.Drawing.Size(110, 20);
            this.label138.TabIndex = 872;
            this.label138.Text = "伝票種類";
            this.label138.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // haishaJokyoPanel
            // 
            this.haishaJokyoPanel.Controls.Add(this.rb_RECEIVER_STATUS_9);
            this.haishaJokyoPanel.Controls.Add(this.rb_RECEIVER_STATUS_4);
            this.haishaJokyoPanel.Controls.Add(this.rb_RECEIVER_STATUS_3);
            this.haishaJokyoPanel.Controls.Add(this.rb_RECEIVER_STATUS_2);
            this.haishaJokyoPanel.Controls.Add(this.SMS_RECEIVER_STATUS);
            this.haishaJokyoPanel.Controls.Add(this.rb_RECEIVER_STATUS_1);
            this.haishaJokyoPanel.Controls.Add(this.rb_RECEIVER_STATUS_0);
            this.haishaJokyoPanel.Location = new System.Drawing.Point(127, 48);
            this.haishaJokyoPanel.Name = "haishaJokyoPanel";
            this.haishaJokyoPanel.Size = new System.Drawing.Size(592, 20);
            this.haishaJokyoPanel.TabIndex = 875;
            // 
            // rb_RECEIVER_STATUS_9
            // 
            this.rb_RECEIVER_STATUS_9.AutoSize = true;
            this.rb_RECEIVER_STATUS_9.DefaultBackColor = System.Drawing.Color.Empty;
            this.rb_RECEIVER_STATUS_9.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_RECEIVER_STATUS_9.FocusOutCheckMethod")));
            this.rb_RECEIVER_STATUS_9.LinkedTextBox = "SMS_RECEIVER_STATUS";
            this.rb_RECEIVER_STATUS_9.Location = new System.Drawing.Point(501, 2);
            this.rb_RECEIVER_STATUS_9.Name = "rb_RECEIVER_STATUS_9";
            this.rb_RECEIVER_STATUS_9.PopupAfterExecute = null;
            this.rb_RECEIVER_STATUS_9.PopupBeforeExecute = null;
            this.rb_RECEIVER_STATUS_9.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rb_RECEIVER_STATUS_9.PopupSearchSendParams")));
            this.rb_RECEIVER_STATUS_9.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rb_RECEIVER_STATUS_9.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rb_RECEIVER_STATUS_9.popupWindowSetting")));
            this.rb_RECEIVER_STATUS_9.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_RECEIVER_STATUS_9.RegistCheckMethod")));
            this.rb_RECEIVER_STATUS_9.Size = new System.Drawing.Size(56, 16);
            this.rb_RECEIVER_STATUS_9.TabIndex = 886;
            this.rb_RECEIVER_STATUS_9.Tag = "受信者状態を指定しない場合にチェックを付けて下さい";
            this.rb_RECEIVER_STATUS_9.Text = "9. 全て";
            this.rb_RECEIVER_STATUS_9.UseVisualStyleBackColor = true;
            this.rb_RECEIVER_STATUS_9.Value = "9";
            // 
            // rb_RECEIVER_STATUS_4
            // 
            this.rb_RECEIVER_STATUS_4.AutoSize = true;
            this.rb_RECEIVER_STATUS_4.DefaultBackColor = System.Drawing.Color.Empty;
            this.rb_RECEIVER_STATUS_4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_RECEIVER_STATUS_4.FocusOutCheckMethod")));
            this.rb_RECEIVER_STATUS_4.LinkedTextBox = "SMS_RECEIVER_STATUS";
            this.rb_RECEIVER_STATUS_4.Location = new System.Drawing.Point(421, 2);
            this.rb_RECEIVER_STATUS_4.Name = "rb_RECEIVER_STATUS_4";
            this.rb_RECEIVER_STATUS_4.PopupAfterExecute = null;
            this.rb_RECEIVER_STATUS_4.PopupBeforeExecute = null;
            this.rb_RECEIVER_STATUS_4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rb_RECEIVER_STATUS_4.PopupSearchSendParams")));
            this.rb_RECEIVER_STATUS_4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rb_RECEIVER_STATUS_4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rb_RECEIVER_STATUS_4.popupWindowSetting")));
            this.rb_RECEIVER_STATUS_4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_RECEIVER_STATUS_4.RegistCheckMethod")));
            this.rb_RECEIVER_STATUS_4.Size = new System.Drawing.Size(59, 16);
            this.rb_RECEIVER_STATUS_4.TabIndex = 885;
            this.rb_RECEIVER_STATUS_4.Tag = "受信者状態が不明である場合にチェックを付けて下さい";
            this.rb_RECEIVER_STATUS_4.Text = "4. 不明";
            this.rb_RECEIVER_STATUS_4.UseVisualStyleBackColor = true;
            this.rb_RECEIVER_STATUS_4.Value = "4";
            // 
            // rb_RECEIVER_STATUS_3
            // 
            this.rb_RECEIVER_STATUS_3.AutoSize = true;
            this.rb_RECEIVER_STATUS_3.DefaultBackColor = System.Drawing.Color.Empty;
            this.rb_RECEIVER_STATUS_3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_RECEIVER_STATUS_3.FocusOutCheckMethod")));
            this.rb_RECEIVER_STATUS_3.LinkedTextBox = "SMS_RECEIVER_STATUS";
            this.rb_RECEIVER_STATUS_3.Location = new System.Drawing.Point(321, 2);
            this.rb_RECEIVER_STATUS_3.Name = "rb_RECEIVER_STATUS_3";
            this.rb_RECEIVER_STATUS_3.PopupAfterExecute = null;
            this.rb_RECEIVER_STATUS_3.PopupBeforeExecute = null;
            this.rb_RECEIVER_STATUS_3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rb_RECEIVER_STATUS_3.PopupSearchSendParams")));
            this.rb_RECEIVER_STATUS_3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rb_RECEIVER_STATUS_3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rb_RECEIVER_STATUS_3.popupWindowSetting")));
            this.rb_RECEIVER_STATUS_3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_RECEIVER_STATUS_3.RegistCheckMethod")));
            this.rb_RECEIVER_STATUS_3.Size = new System.Drawing.Size(86, 16);
            this.rb_RECEIVER_STATUS_3.TabIndex = 884;
            this.rb_RECEIVER_STATUS_3.Tag = "受信者状態が圏外である場合にチェックを付けて下さい";
            this.rb_RECEIVER_STATUS_3.Text = "3. 送達エラー";
            this.rb_RECEIVER_STATUS_3.UseVisualStyleBackColor = true;
            this.rb_RECEIVER_STATUS_3.Value = "3";
            // 
            // rb_RECEIVER_STATUS_2
            // 
            this.rb_RECEIVER_STATUS_2.AutoSize = true;
            this.rb_RECEIVER_STATUS_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.rb_RECEIVER_STATUS_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_RECEIVER_STATUS_2.FocusOutCheckMethod")));
            this.rb_RECEIVER_STATUS_2.LinkedTextBox = "SMS_RECEIVER_STATUS";
            this.rb_RECEIVER_STATUS_2.Location = new System.Drawing.Point(241, 2);
            this.rb_RECEIVER_STATUS_2.Name = "rb_RECEIVER_STATUS_2";
            this.rb_RECEIVER_STATUS_2.PopupAfterExecute = null;
            this.rb_RECEIVER_STATUS_2.PopupBeforeExecute = null;
            this.rb_RECEIVER_STATUS_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rb_RECEIVER_STATUS_2.PopupSearchSendParams")));
            this.rb_RECEIVER_STATUS_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rb_RECEIVER_STATUS_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rb_RECEIVER_STATUS_2.popupWindowSetting")));
            this.rb_RECEIVER_STATUS_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_RECEIVER_STATUS_2.RegistCheckMethod")));
            this.rb_RECEIVER_STATUS_2.Size = new System.Drawing.Size(59, 16);
            this.rb_RECEIVER_STATUS_2.TabIndex = 883;
            this.rb_RECEIVER_STATUS_2.Tag = "受信者状態が圏外である場合にチェックを付けて下さい";
            this.rb_RECEIVER_STATUS_2.Text = "2. 圏外";
            this.rb_RECEIVER_STATUS_2.UseVisualStyleBackColor = true;
            this.rb_RECEIVER_STATUS_2.Value = "2";
            // 
            // SMS_RECEIVER_STATUS
            // 
            this.SMS_RECEIVER_STATUS.BackColor = System.Drawing.SystemColors.Window;
            this.SMS_RECEIVER_STATUS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SMS_RECEIVER_STATUS.DBFieldsName = "";
            this.SMS_RECEIVER_STATUS.DefaultBackColor = System.Drawing.Color.Empty;
            this.SMS_RECEIVER_STATUS.DisplayItemName = "受信者状態";
            this.SMS_RECEIVER_STATUS.DisplayPopUp = null;
            this.SMS_RECEIVER_STATUS.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SMS_RECEIVER_STATUS.FocusOutCheckMethod")));
            this.SMS_RECEIVER_STATUS.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SMS_RECEIVER_STATUS.ForeColor = System.Drawing.Color.Black;
            this.SMS_RECEIVER_STATUS.IsInputErrorOccured = false;
            this.SMS_RECEIVER_STATUS.ItemDefinedTypes = "smallint";
            this.SMS_RECEIVER_STATUS.LinkedRadioButtonArray = new string[] {
        "rb_RECEIVER_STATUS_0",
        "rb_RECEIVER_STATUS_1",
        "rb_RECEIVER_STATUS_2",
        "rb_RECEIVER_STATUS_3",
        "rb_RECEIVER_STATUS_4",
        "rb_RECEIVER_STATUS_9"};
            this.SMS_RECEIVER_STATUS.Location = new System.Drawing.Point(4, 0);
            this.SMS_RECEIVER_STATUS.Name = "SMS_RECEIVER_STATUS";
            this.SMS_RECEIVER_STATUS.PopupAfterExecute = null;
            this.SMS_RECEIVER_STATUS.PopupBeforeExecute = null;
            this.SMS_RECEIVER_STATUS.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SMS_RECEIVER_STATUS.PopupSearchSendParams")));
            this.SMS_RECEIVER_STATUS.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SMS_RECEIVER_STATUS.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SMS_RECEIVER_STATUS.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.SMS_RECEIVER_STATUS.RangeSetting = rangeSettingDto2;
            this.SMS_RECEIVER_STATUS.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SMS_RECEIVER_STATUS.RegistCheckMethod")));
            this.SMS_RECEIVER_STATUS.Size = new System.Drawing.Size(20, 20);
            this.SMS_RECEIVER_STATUS.TabIndex = 880;
            this.SMS_RECEIVER_STATUS.Tag = "【0~9】のいずれかで入力して下さい";
            this.SMS_RECEIVER_STATUS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.SMS_RECEIVER_STATUS.WordWrap = false;
            // 
            // rb_RECEIVER_STATUS_1
            // 
            this.rb_RECEIVER_STATUS_1.AutoSize = true;
            this.rb_RECEIVER_STATUS_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.rb_RECEIVER_STATUS_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_RECEIVER_STATUS_1.FocusOutCheckMethod")));
            this.rb_RECEIVER_STATUS_1.LinkedTextBox = "SMS_RECEIVER_STATUS";
            this.rb_RECEIVER_STATUS_1.Location = new System.Drawing.Point(151, 2);
            this.rb_RECEIVER_STATUS_1.Name = "rb_RECEIVER_STATUS_1";
            this.rb_RECEIVER_STATUS_1.PopupAfterExecute = null;
            this.rb_RECEIVER_STATUS_1.PopupBeforeExecute = null;
            this.rb_RECEIVER_STATUS_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rb_RECEIVER_STATUS_1.PopupSearchSendParams")));
            this.rb_RECEIVER_STATUS_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rb_RECEIVER_STATUS_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rb_RECEIVER_STATUS_1.popupWindowSetting")));
            this.rb_RECEIVER_STATUS_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_RECEIVER_STATUS_1.RegistCheckMethod")));
            this.rb_RECEIVER_STATUS_1.Size = new System.Drawing.Size(71, 16);
            this.rb_RECEIVER_STATUS_1.TabIndex = 882;
            this.rb_RECEIVER_STATUS_1.Tag = "受信者状態が着信済である場合にチェックを付けて下さい";
            this.rb_RECEIVER_STATUS_1.Text = "1. 着信済";
            this.rb_RECEIVER_STATUS_1.UseVisualStyleBackColor = true;
            this.rb_RECEIVER_STATUS_1.Value = "1";
            // 
            // rb_RECEIVER_STATUS_0
            // 
            this.rb_RECEIVER_STATUS_0.AutoSize = true;
            this.rb_RECEIVER_STATUS_0.DefaultBackColor = System.Drawing.Color.Empty;
            this.rb_RECEIVER_STATUS_0.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_RECEIVER_STATUS_0.FocusOutCheckMethod")));
            this.rb_RECEIVER_STATUS_0.LinkedTextBox = "SMS_RECEIVER_STATUS";
            this.rb_RECEIVER_STATUS_0.Location = new System.Drawing.Point(31, 3);
            this.rb_RECEIVER_STATUS_0.Name = "rb_RECEIVER_STATUS_0";
            this.rb_RECEIVER_STATUS_0.PopupAfterExecute = null;
            this.rb_RECEIVER_STATUS_0.PopupBeforeExecute = null;
            this.rb_RECEIVER_STATUS_0.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rb_RECEIVER_STATUS_0.PopupSearchSendParams")));
            this.rb_RECEIVER_STATUS_0.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rb_RECEIVER_STATUS_0.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rb_RECEIVER_STATUS_0.popupWindowSetting")));
            this.rb_RECEIVER_STATUS_0.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_RECEIVER_STATUS_0.RegistCheckMethod")));
            this.rb_RECEIVER_STATUS_0.Size = new System.Drawing.Size(102, 16);
            this.rb_RECEIVER_STATUS_0.TabIndex = 881;
            this.rb_RECEIVER_STATUS_0.Tag = "受信者状態が送達結果なしである場合にチェックを付けて下さい";
            this.rb_RECEIVER_STATUS_0.Text = "0. 送達結果なし";
            this.rb_RECEIVER_STATUS_0.UseVisualStyleBackColor = true;
            this.rb_RECEIVER_STATUS_0.Value = "0";
            // 
            // label139
            // 
            this.label139.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label139.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label139.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label139.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label139.ForeColor = System.Drawing.Color.White;
            this.label139.Location = new System.Drawing.Point(12, 49);
            this.label139.Name = "label139";
            this.label139.Size = new System.Drawing.Size(110, 20);
            this.label139.TabIndex = 874;
            this.label139.Text = "受信者状態";
            this.label139.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GENBA_NAME
            // 
            this.GENBA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GENBA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_NAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.GENBA_NAME.DBFieldsName = "";
            this.GENBA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_NAME.DisplayPopUp = null;
            this.GENBA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME.FocusOutCheckMethod")));
            this.GENBA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GENBA_NAME.ForeColor = System.Drawing.Color.Black;
            this.GENBA_NAME.IsInputErrorOccured = false;
            this.GENBA_NAME.ItemDefinedTypes = "varchar";
            this.GENBA_NAME.Location = new System.Drawing.Point(646, 73);
            this.GENBA_NAME.MaxLength = 0;
            this.GENBA_NAME.Name = "GENBA_NAME";
            this.GENBA_NAME.PopupAfterExecute = null;
            this.GENBA_NAME.PopupBeforeExecute = null;
            this.GENBA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_NAME.PopupSearchSendParams")));
            this.GENBA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_NAME.popupWindowSetting")));
            this.GENBA_NAME.ReadOnly = true;
            this.GENBA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME.RegistCheckMethod")));
            this.GENBA_NAME.Size = new System.Drawing.Size(286, 20);
            this.GENBA_NAME.TabIndex = 881;
            this.GENBA_NAME.TabStop = false;
            this.GENBA_NAME.Tag = " ";
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
            this.GENBA_CD.Location = new System.Drawing.Point(597, 73);
            this.GENBA_CD.MaxLength = 6;
            this.GENBA_CD.Name = "GENBA_CD";
            this.GENBA_CD.PopupAfterExecute = null;
            this.GENBA_CD.PopupBeforeExecute = null;
            this.GENBA_CD.PopupGetMasterField = "GENBA_CD, GENBA_NAME_RYAKU, GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_CD.PopupSearchSendParams")));
            this.GENBA_CD.PopupSetFormField = "GENBA_CD, GENBA_NAME,GYOUSHA_CD, GYOUSHA_NAME";
            this.GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_CD.popupWindowSetting")));
            this.GENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.RegistCheckMethod")));
            this.GENBA_CD.SetFormField = "";
            this.GENBA_CD.Size = new System.Drawing.Size(50, 20);
            this.GENBA_CD.TabIndex = 878;
            this.GENBA_CD.Tag = "現場を指定して下さい（スペースキー押下にて、検索画面を表示します）";
            this.GENBA_CD.ZeroPaddengFlag = true;
            this.GENBA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.GENBA_CD_Validating);
            // 
            // lable5
            // 
            this.lable5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lable5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lable5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lable5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lable5.ForeColor = System.Drawing.Color.White;
            this.lable5.Location = new System.Drawing.Point(482, 73);
            this.lable5.Name = "lable5";
            this.lable5.Size = new System.Drawing.Size(110, 20);
            this.lable5.TabIndex = 879;
            this.lable5.Text = "現場";
            this.lable5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GYOUSHA_NAME
            // 
            this.GYOUSHA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GYOUSHA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_NAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.GYOUSHA_NAME.DBFieldsName = "";
            this.GYOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_NAME.DisplayPopUp = null;
            this.GYOUSHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME.FocusOutCheckMethod")));
            this.GYOUSHA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GYOUSHA_NAME.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_NAME.IsInputErrorOccured = false;
            this.GYOUSHA_NAME.ItemDefinedTypes = "varchar";
            this.GYOUSHA_NAME.Location = new System.Drawing.Point(176, 73);
            this.GYOUSHA_NAME.MaxLength = 0;
            this.GYOUSHA_NAME.Name = "GYOUSHA_NAME";
            this.GYOUSHA_NAME.PopupAfterExecute = null;
            this.GYOUSHA_NAME.PopupBeforeExecute = null;
            this.GYOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_NAME.PopupSearchSendParams")));
            this.GYOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_NAME.popupWindowSetting")));
            this.GYOUSHA_NAME.ReadOnly = true;
            this.GYOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME.RegistCheckMethod")));
            this.GYOUSHA_NAME.Size = new System.Drawing.Size(286, 20);
            this.GYOUSHA_NAME.TabIndex = 880;
            this.GYOUSHA_NAME.TabStop = false;
            this.GYOUSHA_NAME.Tag = " ";
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
            this.GYOUSHA_CD.Location = new System.Drawing.Point(127, 73);
            this.GYOUSHA_CD.MaxLength = 6;
            this.GYOUSHA_CD.Name = "GYOUSHA_CD";
            this.GYOUSHA_CD.PopupAfterExecute = null;
            this.GYOUSHA_CD.PopupAfterExecuteMethod = "GyoushaCdPopUpAfter";
            this.GYOUSHA_CD.PopupBeforeExecute = null;
            this.GYOUSHA_CD.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_CD.PopupSearchSendParams")));
            this.GYOUSHA_CD.PopupSetFormField = "GYOUSHA_CD, GYOUSHA_NAME";
            this.GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_CD.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_CD.popupWindowSetting")));
            this.GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.RegistCheckMethod")));
            this.GYOUSHA_CD.SetFormField = "";
            this.GYOUSHA_CD.Size = new System.Drawing.Size(50, 20);
            this.GYOUSHA_CD.TabIndex = 877;
            this.GYOUSHA_CD.Tag = "業者を指定して下さい（スペースキー押下にて、検索画面を表示します）";
            this.GYOUSHA_CD.ZeroPaddengFlag = true;
            this.GYOUSHA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.GYOUSHA_CD_Validating);
            // 
            // lable3
            // 
            this.lable3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lable3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lable3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lable3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lable3.ForeColor = System.Drawing.Color.White;
            this.lable3.Location = new System.Drawing.Point(12, 73);
            this.lable3.Name = "lable3";
            this.lable3.Size = new System.Drawing.Size(110, 20);
            this.lable3.TabIndex = 876;
            this.lable3.Text = "業者";
            this.lable3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.customRadioButton1);
            this.panel1.Controls.Add(this.DATE_SHURUI);
            this.panel1.Controls.Add(this.rb_DATE_SHURUI_2);
            this.panel1.Controls.Add(this.rb_DATE_SHURUI_1);
            this.panel1.Location = new System.Drawing.Point(425, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(269, 20);
            this.panel1.TabIndex = 885;
            // 
            // customRadioButton1
            // 
            this.customRadioButton1.AutoSize = true;
            this.customRadioButton1.DefaultBackColor = System.Drawing.Color.Empty;
            this.customRadioButton1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customRadioButton1.FocusOutCheckMethod")));
            this.customRadioButton1.LinkedTextBox = "SMS_DENPYOU_SHURUI";
            this.customRadioButton1.Location = new System.Drawing.Point(571, 3);
            this.customRadioButton1.Name = "customRadioButton1";
            this.customRadioButton1.PopupAfterExecute = null;
            this.customRadioButton1.PopupBeforeExecute = null;
            this.customRadioButton1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customRadioButton1.PopupSearchSendParams")));
            this.customRadioButton1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.customRadioButton1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customRadioButton1.popupWindowSetting")));
            this.customRadioButton1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customRadioButton1.RegistCheckMethod")));
            this.customRadioButton1.Size = new System.Drawing.Size(56, 16);
            this.customRadioButton1.TabIndex = 880;
            this.customRadioButton1.Tag = "伝票種類を指定しない場合にチェックを付けて下さい";
            this.customRadioButton1.Text = "9. 全て";
            this.customRadioButton1.UseVisualStyleBackColor = true;
            this.customRadioButton1.Value = "9";
            // 
            // DATE_SHURUI
            // 
            this.DATE_SHURUI.BackColor = System.Drawing.SystemColors.Window;
            this.DATE_SHURUI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DATE_SHURUI.DBFieldsName = "";
            this.DATE_SHURUI.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_SHURUI.DisplayItemName = "日付";
            this.DATE_SHURUI.DisplayPopUp = null;
            this.DATE_SHURUI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SHURUI.FocusOutCheckMethod")));
            this.DATE_SHURUI.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DATE_SHURUI.ForeColor = System.Drawing.Color.Black;
            this.DATE_SHURUI.IsInputErrorOccured = false;
            this.DATE_SHURUI.ItemDefinedTypes = "smallint";
            this.DATE_SHURUI.LinkedRadioButtonArray = new string[] {
        "rb_DATE_SHURUI_1",
        "rb_DATE_SHURUI_2"};
            this.DATE_SHURUI.Location = new System.Drawing.Point(4, 0);
            this.DATE_SHURUI.Name = "DATE_SHURUI";
            this.DATE_SHURUI.PopupAfterExecute = null;
            this.DATE_SHURUI.PopupBeforeExecute = null;
            this.DATE_SHURUI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DATE_SHURUI.PopupSearchSendParams")));
            this.DATE_SHURUI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DATE_SHURUI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DATE_SHURUI.popupWindowSetting")));
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
            this.DATE_SHURUI.RangeSetting = rangeSettingDto3;
            this.DATE_SHURUI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SHURUI.RegistCheckMethod")));
            this.DATE_SHURUI.Size = new System.Drawing.Size(20, 20);
            this.DATE_SHURUI.TabIndex = 873;
            this.DATE_SHURUI.Tag = "【1、2】のいずれかで入力して下さい";
            this.DATE_SHURUI.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.DATE_SHURUI.WordWrap = false;
            // 
            // rb_DATE_SHURUI_2
            // 
            this.rb_DATE_SHURUI_2.AutoSize = true;
            this.rb_DATE_SHURUI_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.rb_DATE_SHURUI_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_DATE_SHURUI_2.FocusOutCheckMethod")));
            this.rb_DATE_SHURUI_2.LinkedTextBox = "DATE_SHURUI";
            this.rb_DATE_SHURUI_2.Location = new System.Drawing.Point(121, 2);
            this.rb_DATE_SHURUI_2.Name = "rb_DATE_SHURUI_2";
            this.rb_DATE_SHURUI_2.PopupAfterExecute = null;
            this.rb_DATE_SHURUI_2.PopupBeforeExecute = null;
            this.rb_DATE_SHURUI_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rb_DATE_SHURUI_2.PopupSearchSendParams")));
            this.rb_DATE_SHURUI_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rb_DATE_SHURUI_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rb_DATE_SHURUI_2.popupWindowSetting")));
            this.rb_DATE_SHURUI_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_DATE_SHURUI_2.RegistCheckMethod")));
            this.rb_DATE_SHURUI_2.Size = new System.Drawing.Size(116, 16);
            this.rb_DATE_SHURUI_2.TabIndex = 875;
            this.rb_DATE_SHURUI_2.Tag = "メッセージ送信日で検索する場合にチェックを付けて下さい";
            this.rb_DATE_SHURUI_2.Text = "2. メッセージ送信日";
            this.rb_DATE_SHURUI_2.UseVisualStyleBackColor = true;
            this.rb_DATE_SHURUI_2.Value = "2";
            // 
            // rb_DATE_SHURUI_1
            // 
            this.rb_DATE_SHURUI_1.AutoSize = true;
            this.rb_DATE_SHURUI_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.rb_DATE_SHURUI_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_DATE_SHURUI_1.FocusOutCheckMethod")));
            this.rb_DATE_SHURUI_1.LinkedTextBox = "DATE_SHURUI";
            this.rb_DATE_SHURUI_1.Location = new System.Drawing.Point(31, 3);
            this.rb_DATE_SHURUI_1.Name = "rb_DATE_SHURUI_1";
            this.rb_DATE_SHURUI_1.PopupAfterExecute = null;
            this.rb_DATE_SHURUI_1.PopupBeforeExecute = null;
            this.rb_DATE_SHURUI_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rb_DATE_SHURUI_1.PopupSearchSendParams")));
            this.rb_DATE_SHURUI_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rb_DATE_SHURUI_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rb_DATE_SHURUI_1.popupWindowSetting")));
            this.rb_DATE_SHURUI_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_DATE_SHURUI_1.RegistCheckMethod")));
            this.rb_DATE_SHURUI_1.Size = new System.Drawing.Size(71, 16);
            this.rb_DATE_SHURUI_1.TabIndex = 874;
            this.rb_DATE_SHURUI_1.Tag = "作業日で検索する場合にチェックを付けて下さい";
            this.rb_DATE_SHURUI_1.Text = "1. 作業日";
            this.rb_DATE_SHURUI_1.UseVisualStyleBackColor = true;
            this.rb_DATE_SHURUI_1.Value = "1";
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 692);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.GENBA_NAME);
            this.Controls.Add(this.GENBA_CD);
            this.Controls.Add(this.lable5);
            this.Controls.Add(this.GYOUSHA_NAME);
            this.Controls.Add(this.GYOUSHA_CD);
            this.Controls.Add(this.lable3);
            this.Controls.Add(this.haishaJokyoPanel);
            this.Controls.Add(this.label139);
            this.Controls.Add(this.denpyouShuruiPanel);
            this.Controls.Add(this.label138);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DATE_TO);
            this.Controls.Add(this.DATE_FROM);
            this.Controls.Add(this.TEKIYOU_LABEL);
            this.Name = "UIForm";
            this.Text = "UIForm";
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.TEKIYOU_LABEL, 0);
            this.Controls.SetChildIndex(this.DATE_FROM, 0);
            this.Controls.SetChildIndex(this.DATE_TO, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label138, 0);
            this.Controls.SetChildIndex(this.denpyouShuruiPanel, 0);
            this.Controls.SetChildIndex(this.label139, 0);
            this.Controls.SetChildIndex(this.haishaJokyoPanel, 0);
            this.Controls.SetChildIndex(this.lable3, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_CD, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_NAME, 0);
            this.Controls.SetChildIndex(this.lable5, 0);
            this.Controls.SetChildIndex(this.GENBA_CD, 0);
            this.Controls.SetChildIndex(this.GENBA_NAME, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.denpyouShuruiPanel.ResumeLayout(false);
            this.denpyouShuruiPanel.PerformLayout();
            this.haishaJokyoPanel.ResumeLayout(false);
            this.haishaJokyoPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        internal r_framework.CustomControl.CustomDateTimePicker DATE_TO;
        internal r_framework.CustomControl.CustomDateTimePicker DATE_FROM;
        internal System.Windows.Forms.Label TEKIYOU_LABEL;
        //public r_framework.CustomControl.DataGridCustomControl.CustomSortHeader customSortHeader1;
        //public r_framework.CustomControl.DataGridCustomControl.CustomSearchHeader customSearchHeader1;
        internal r_framework.CustomControl.CustomRadioButton rb_DENPYOU_SHURUI_6;
        internal r_framework.CustomControl.CustomRadioButton rb_DENPYOU_SHURUI_5;
        internal r_framework.CustomControl.CustomRadioButton rb_DENPYOU_SHURUI_4;
        internal r_framework.CustomControl.CustomRadioButton rb_DENPYOU_SHURUI_3;
        internal r_framework.CustomControl.CustomNumericTextBox2 SMS_DENPYOU_SHURUI;
        internal r_framework.CustomControl.CustomRadioButton rb_DENPYOU_SHURUI_2;
        internal r_framework.CustomControl.CustomRadioButton rb_DENPYOU_SHURUI_1;
        private System.Windows.Forms.Label label138;
        internal r_framework.CustomControl.CustomRadioButton rb_RECEIVER_STATUS_9;
        internal r_framework.CustomControl.CustomRadioButton rb_RECEIVER_STATUS_4;
        internal r_framework.CustomControl.CustomRadioButton rb_RECEIVER_STATUS_3;
        internal r_framework.CustomControl.CustomRadioButton rb_RECEIVER_STATUS_2;
        internal r_framework.CustomControl.CustomNumericTextBox2 SMS_RECEIVER_STATUS;
        internal r_framework.CustomControl.CustomRadioButton rb_RECEIVER_STATUS_1;
        internal r_framework.CustomControl.CustomRadioButton rb_RECEIVER_STATUS_0;
        private System.Windows.Forms.Label label139;
        internal r_framework.CustomControl.CustomTextBox GENBA_NAME;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GENBA_CD;
        private System.Windows.Forms.Label lable5;
        internal r_framework.CustomControl.CustomTextBox GYOUSHA_NAME;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GYOUSHA_CD;
        private System.Windows.Forms.Label lable3;
        internal System.Windows.Forms.Panel denpyouShuruiPanel;
        internal System.Windows.Forms.Panel haishaJokyoPanel;
        internal r_framework.CustomControl.CustomRadioButton rb_DENPYOU_SHURUI_9;
        internal System.Windows.Forms.Panel panel1;
        internal r_framework.CustomControl.CustomRadioButton customRadioButton1;
        internal r_framework.CustomControl.CustomNumericTextBox2 DATE_SHURUI;
        internal r_framework.CustomControl.CustomRadioButton rb_DATE_SHURUI_2;
        internal r_framework.CustomControl.CustomRadioButton rb_DATE_SHURUI_1;
    }
}