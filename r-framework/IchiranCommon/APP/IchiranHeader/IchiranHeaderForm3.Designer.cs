using r_framework.CustomControl;

namespace Shougun.Core.Common.IchiranCommon.APP
{
    partial class IchiranHeaderForm3
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
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.lb_title = new System.Windows.Forms.Label();
            this.HEADER_KYOTEN_NAME = new r_framework.CustomControl.CustomTextBox();
            this.lbl_Kyoten = new System.Windows.Forms.Label();
            this.alertNumber = new r_framework.CustomControl.CustomTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.button4 = new System.Windows.Forms.Button();
            this.dtTo = new r_framework.CustomControl.CustomDateTimePicker();
            this.label38 = new System.Windows.Forms.Label();
            this.dtFrom = new r_framework.CustomControl.CustomDateTimePicker();
            this.受付日 = new System.Windows.Forms.Label();
            this.HEADER_KYOTEN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.ReadDataNumber = new r_framework.CustomControl.CustomNumericTextBox2();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_title
            // 
            this.lb_title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lb_title.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_title.Font = new System.Drawing.Font("MS Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lb_title.ForeColor = System.Drawing.Color.White;
            this.lb_title.Location = new System.Drawing.Point(0, 6);
            this.lb_title.Name = "lb_title";
            this.lb_title.Size = new System.Drawing.Size(345, 34);
            this.lb_title.TabIndex = 0;
            this.lb_title.Text = "○○○○○タイトル";
            this.lb_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_title.TextChanged += new System.EventHandler(this.lb_title_TextChanged);
            // 
            // HEADER_KYOTEN_NAME
            // 
            this.HEADER_KYOTEN_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.HEADER_KYOTEN_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HEADER_KYOTEN_NAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.HEADER_KYOTEN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.HEADER_KYOTEN_NAME.DisplayPopUp = null;
            this.HEADER_KYOTEN_NAME.FocusOutCheckMethod = null;
            this.HEADER_KYOTEN_NAME.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.HEADER_KYOTEN_NAME.ForeColor = System.Drawing.Color.Black;
            this.HEADER_KYOTEN_NAME.IsInputErrorOccured = false;
            this.HEADER_KYOTEN_NAME.Location = new System.Drawing.Point(470, 2);
            this.HEADER_KYOTEN_NAME.MaxLength = 0;
            this.HEADER_KYOTEN_NAME.Name = "HEADER_KYOTEN_NAME";
            this.HEADER_KYOTEN_NAME.PopupAfterExecute = null;
            this.HEADER_KYOTEN_NAME.PopupBeforeExecute = null;
            this.HEADER_KYOTEN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HEADER_KYOTEN_NAME.popupWindowSetting = null;
            this.HEADER_KYOTEN_NAME.ReadOnly = true;
            this.HEADER_KYOTEN_NAME.RegistCheckMethod = null;
            this.HEADER_KYOTEN_NAME.Size = new System.Drawing.Size(160, 20);
            this.HEADER_KYOTEN_NAME.TabIndex = 3;
            this.HEADER_KYOTEN_NAME.TabStop = false;
            this.HEADER_KYOTEN_NAME.Tag = " 拠点を表示します";
            // 
            // lbl_Kyoten
            // 
            this.lbl_Kyoten.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Kyoten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Kyoten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Kyoten.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Kyoten.ForeColor = System.Drawing.Color.White;
            this.lbl_Kyoten.Location = new System.Drawing.Point(351, 2);
            this.lbl_Kyoten.Name = "lbl_Kyoten";
            this.lbl_Kyoten.Size = new System.Drawing.Size(85, 20);
            this.lbl_Kyoten.TabIndex = 1;
            this.lbl_Kyoten.Text = "拠点";
            this.lbl_Kyoten.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // alertNumber
            // 
            this.alertNumber.BackColor = System.Drawing.SystemColors.Window;
            this.alertNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.alertNumber.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.alertNumber.CustomFormatSetting = "#,##0";
            this.alertNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.alertNumber.DisplayPopUp = null;
            this.alertNumber.FocusOutCheckMethod = null;
            this.alertNumber.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.alertNumber.ForeColor = System.Drawing.Color.Black;
            this.alertNumber.FormatSetting = "カスタム";
            this.alertNumber.IsInputErrorOccured = false;
            this.alertNumber.ItemDefinedTypes = "float";
            this.alertNumber.Location = new System.Drawing.Point(1100, 2);
            this.alertNumber.MaxLength = 0;
            this.alertNumber.Name = "alertNumber";
            this.alertNumber.PopupAfterExecute = null;
            this.alertNumber.PopupBeforeExecute = null;
            this.alertNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.alertNumber.popupWindowSetting = null;
            this.alertNumber.RegistCheckMethod = null;
            this.alertNumber.Size = new System.Drawing.Size(80, 20);
            this.alertNumber.TabIndex = 7;
            this.alertNumber.Tag = " 検索結果の総件数でアラートメッセージを表示させたい上限数を入力してください";
            this.alertNumber.Text = "10,000,000";
            this.alertNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(985, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "アラート件数";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(985, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "読込データ件数";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radioButton2);
            this.panel2.Controls.Add(this.radioButton1);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.dtTo);
            this.panel2.Controls.Add(this.label38);
            this.panel2.Controls.Add(this.dtFrom);
            this.panel2.Controls.Add(this.受付日);
            this.panel2.Location = new System.Drawing.Point(635, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(345, 46);
            this.panel2.TabIndex = 7;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.radioButton2.Location = new System.Drawing.Point(91, 2);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(81, 17);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "入力日付";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.radioButton1.Location = new System.Drawing.Point(4, 2);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(81, 17);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "伝票日付";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.Control;
            this.button4.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button4.Location = new System.Drawing.Point(325, 24);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(20, 20);
            this.button4.TabIndex = 7;
            this.button4.TabStop = false;
            this.button4.UseVisualStyleBackColor = false;
            // 
            // dtTo
            // 
            this.dtTo.BackColor = System.Drawing.SystemColors.Window;
            this.dtTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtTo.CalendarFont = new System.Drawing.Font("MS Gothic", 9F);
            this.dtTo.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.dtTo.Checked = false;
            this.dtTo.CustomFormat = "yyyy/MM/dd(ddd)";
            this.dtTo.DateTimeNowYear = "";
            this.dtTo.DefaultBackColor = System.Drawing.Color.Empty;
            this.dtTo.DisplayPopUp = null;
            this.dtTo.FocusOutCheckMethod = null;
            this.dtTo.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.dtTo.ForeColor = System.Drawing.Color.Black;
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtTo.IsInputErrorOccured = false;
            this.dtTo.Location = new System.Drawing.Point(215, 24);
            this.dtTo.MaxLength = 10;
            this.dtTo.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtTo.Name = "dtTo";
            this.dtTo.NullValue = "";
            this.dtTo.PopupAfterExecute = null;
            this.dtTo.PopupBeforeExecute = null;
            this.dtTo.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dtTo.popupWindowSetting = null;
            this.dtTo.RegistCheckMethod = null;
            this.dtTo.Size = new System.Drawing.Size(110, 20);
            this.dtTo.TabIndex = 6;
            this.dtTo.Tag = "（上記のラジオボタンで選択した日付）Toを入力してください";
            this.dtTo.Text = "2013/12/24(火)";
            this.dtTo.Value = new System.DateTime(2013, 12, 24, 0, 0, 0, 0);
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.Transparent;
            this.label38.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label38.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.label38.ForeColor = System.Drawing.Color.Black;
            this.label38.Location = new System.Drawing.Point(195, 24);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(20, 20);
            this.label38.TabIndex = 5;
            this.label38.Text = "～";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtFrom
            // 
            this.dtFrom.BackColor = System.Drawing.SystemColors.Window;
            this.dtFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtFrom.CalendarFont = new System.Drawing.Font("MS Gothic", 9F);
            this.dtFrom.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.dtFrom.Checked = false;
            this.dtFrom.CustomFormat = "yyyy/MM/dd(ddd)";
            this.dtFrom.DateTimeNowYear = "";
            this.dtFrom.DefaultBackColor = System.Drawing.Color.Empty;
            this.dtFrom.DisplayPopUp = null;
            this.dtFrom.FocusOutCheckMethod = null;
            this.dtFrom.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.dtFrom.ForeColor = System.Drawing.Color.Black;
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dtFrom.IsInputErrorOccured = false;
            this.dtFrom.Location = new System.Drawing.Point(85, 24);
            this.dtFrom.MaxLength = 10;
            this.dtFrom.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.NullValue = "";
            this.dtFrom.PopupAfterExecute = null;
            this.dtFrom.PopupBeforeExecute = null;
            this.dtFrom.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dtFrom.popupWindowSetting = null;
            this.dtFrom.RegistCheckMethod = null;
            this.dtFrom.Size = new System.Drawing.Size(110, 20);
            this.dtFrom.TabIndex = 5;
            this.dtFrom.Tag = "（上記のラジオボタンで選択した日付）Fromを入力してください";
            this.dtFrom.Text = "2013/12/24(火)";
            this.dtFrom.Value = new System.DateTime(2013, 12, 24, 0, 0, 0, 0);
            // 
            // 受付日
            // 
            this.受付日.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.受付日.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.受付日.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.受付日.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.受付日.ForeColor = System.Drawing.Color.White;
            this.受付日.Location = new System.Drawing.Point(0, 24);
            this.受付日.Name = "受付日";
            this.受付日.Size = new System.Drawing.Size(80, 20);
            this.受付日.TabIndex = 3;
            this.受付日.Text = "伝票日付";
            this.受付日.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HEADER_KYOTEN_CD
            // 
            this.HEADER_KYOTEN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.HEADER_KYOTEN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HEADER_KYOTEN_CD.CustomFormatSetting = "00";
            this.HEADER_KYOTEN_CD.DBFieldsName = "KYOTEN_CD";
            this.HEADER_KYOTEN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.HEADER_KYOTEN_CD.DisplayItemName = "拠点CD";
            this.HEADER_KYOTEN_CD.DisplayPopUp = null;
            this.HEADER_KYOTEN_CD.FocusOutCheckMethod = null;
            this.HEADER_KYOTEN_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.HEADER_KYOTEN_CD.ForeColor = System.Drawing.Color.Black;
            this.HEADER_KYOTEN_CD.FormatSetting = "カスタム";
            this.HEADER_KYOTEN_CD.GetCodeMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.HEADER_KYOTEN_CD.IsInputErrorOccured = false;
            this.HEADER_KYOTEN_CD.ItemDefinedTypes = "smallint";
            this.HEADER_KYOTEN_CD.Location = new System.Drawing.Point(441, 2);
            this.HEADER_KYOTEN_CD.Name = "HEADER_KYOTEN_CD";
            this.HEADER_KYOTEN_CD.PopupAfterExecute = null;
            this.HEADER_KYOTEN_CD.PopupBeforeExecute = null;
            this.HEADER_KYOTEN_CD.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.HEADER_KYOTEN_CD.PopupSetFormField = "HEADER_KYOTEN_CD,HEADER_KYOTEN_NAME";
            this.HEADER_KYOTEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.HEADER_KYOTEN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.HEADER_KYOTEN_CD.popupWindowSetting = null;
            rangeSettingDto1.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.HEADER_KYOTEN_CD.RangeSetting = rangeSettingDto1;
            this.HEADER_KYOTEN_CD.RegistCheckMethod = null;
            this.HEADER_KYOTEN_CD.SearchDisplayFlag = 2;
            this.HEADER_KYOTEN_CD.SetFormField = "HEADER_KYOTEN_CD,HEADER_KYOTEN_NAME";
            this.HEADER_KYOTEN_CD.ShortItemName = "拠点CD";
            this.HEADER_KYOTEN_CD.Size = new System.Drawing.Size(30, 20);
            this.HEADER_KYOTEN_CD.TabIndex = 1;
            this.HEADER_KYOTEN_CD.Tag = "拠点を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.HEADER_KYOTEN_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.HEADER_KYOTEN_CD.WordWrap = false;
            // 
            // ReadDataNumber
            // 
            this.ReadDataNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ReadDataNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReadDataNumber.CustomFormatSetting = "#,##0";
            this.ReadDataNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.ReadDataNumber.DisplayPopUp = null;
            this.ReadDataNumber.FocusOutCheckMethod = null;
            this.ReadDataNumber.ForeColor = System.Drawing.Color.Black;
            this.ReadDataNumber.FormatSetting = "カスタム";
            this.ReadDataNumber.IsInputErrorOccured = false;
            this.ReadDataNumber.ItemDefinedTypes = "float";
            this.ReadDataNumber.Location = new System.Drawing.Point(1100, 24);
            this.ReadDataNumber.Name = "ReadDataNumber";
            this.ReadDataNumber.PopupAfterExecute = null;
            this.ReadDataNumber.PopupBeforeExecute = null;
            this.ReadDataNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ReadDataNumber.popupWindowSetting = null;
            this.ReadDataNumber.RangeSetting = rangeSettingDto2;
            this.ReadDataNumber.ReadOnly = true;
            this.ReadDataNumber.RegistCheckMethod = null;
            this.ReadDataNumber.Size = new System.Drawing.Size(80, 20);
            this.ReadDataNumber.TabIndex = 9;
            this.ReadDataNumber.TabStop = false;
            this.ReadDataNumber.Tag = "検索結果の総件数が表示されます";
            this.ReadDataNumber.Text = "20";
            this.ReadDataNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ReadDataNumber.WordWrap = false;
            // 
            // IchiranHeaderForm3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.HEADER_KYOTEN_CD);
            this.Controls.Add(this.HEADER_KYOTEN_NAME);
            this.Controls.Add(this.lbl_Kyoten);
            this.Controls.Add(this.alertNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.ReadDataNumber);
            this.Controls.Add(this.lb_title);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "IchiranHeaderForm3";
            this.Text = "BaseForm01";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lb_title;
        public CustomTextBox HEADER_KYOTEN_NAME;
        public System.Windows.Forms.Label lbl_Kyoten;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label 受付日;
        public CustomDateTimePicker dtTo;
        public CustomDateTimePicker dtFrom;
        public CustomNumericTextBox2 HEADER_KYOTEN_CD;
        public CustomNumericTextBox2 ReadDataNumber;
        public CustomTextBox alertNumber;
        public System.Windows.Forms.Label label2;
    }
}