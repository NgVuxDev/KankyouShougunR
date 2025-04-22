namespace Shougun.Core.BusinessManagement.EigyouYojitsuKanrihyou.APP
{
    partial class F18_G275UIHeaderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F18_G275UIHeaderForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.ReadDataNumber = new r_framework.CustomControl.CustomTextBox();
            this.lbl_アラート件数 = new System.Windows.Forms.Label();
            this.lbl_読込データ件数 = new System.Windows.Forms.Label();
            this.tb_nendo = new r_framework.CustomControl.CustomDateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.alertNumber = new r_framework.CustomControl.CustomNumericTextBox2();
            this.rdbGetuji = new r_framework.CustomControl.CustomRadioButton();
            this.rdbNenji = new r_framework.CustomControl.CustomRadioButton();
            this.txtKeisiki = new r_framework.CustomControl.CustomNumericTextBox2();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.TabIndex = 1;
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(1, 3);
            this.lb_title.Size = new System.Drawing.Size(309, 34);
            this.lb_title.TabIndex = 0;
            // 
            // ReadDataNumber
            // 
            this.ReadDataNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ReadDataNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReadDataNumber.CustomFormatSetting = "#,##0";
            this.ReadDataNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.ReadDataNumber.DisplayPopUp = null;
            this.ReadDataNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ReadDataNumber.FocusOutCheckMethod")));
            this.ReadDataNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ReadDataNumber.ForeColor = System.Drawing.Color.Black;
            this.ReadDataNumber.FormatSetting = "カスタム";
            this.ReadDataNumber.IsInputErrorOccured = false;
            this.ReadDataNumber.Location = new System.Drawing.Point(1088, 24);
            this.ReadDataNumber.Name = "ReadDataNumber";
            this.ReadDataNumber.PopupAfterExecute = null;
            this.ReadDataNumber.PopupBeforeExecute = null;
            this.ReadDataNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ReadDataNumber.PopupSearchSendParams")));
            this.ReadDataNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ReadDataNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ReadDataNumber.popupWindowSetting")));
            this.ReadDataNumber.ReadOnly = true;
            this.ReadDataNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ReadDataNumber.RegistCheckMethod")));
            this.ReadDataNumber.Size = new System.Drawing.Size(80, 20);
            this.ReadDataNumber.TabIndex = 16;
            this.ReadDataNumber.TabStop = false;
            this.ReadDataNumber.Tag = "検索結果の総件数が表示されます";
            this.ReadDataNumber.Text = "0";
            this.ReadDataNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl_アラート件数
            // 
            this.lbl_アラート件数.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_アラート件数.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_アラート件数.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_アラート件数.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_アラート件数.ForeColor = System.Drawing.Color.White;
            this.lbl_アラート件数.Location = new System.Drawing.Point(973, 2);
            this.lbl_アラート件数.Name = "lbl_アラート件数";
            this.lbl_アラート件数.Size = new System.Drawing.Size(110, 20);
            this.lbl_アラート件数.TabIndex = 13;
            this.lbl_アラート件数.Text = "アラート件数";
            this.lbl_アラート件数.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_読込データ件数
            // 
            this.lbl_読込データ件数.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_読込データ件数.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_読込データ件数.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_読込データ件数.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_読込データ件数.ForeColor = System.Drawing.Color.White;
            this.lbl_読込データ件数.Location = new System.Drawing.Point(973, 24);
            this.lbl_読込データ件数.Name = "lbl_読込データ件数";
            this.lbl_読込データ件数.Size = new System.Drawing.Size(110, 20);
            this.lbl_読込データ件数.TabIndex = 15;
            this.lbl_読込データ件数.Text = "読込データ件数";
            this.lbl_読込データ件数.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tb_nendo
            // 
            this.tb_nendo.BackColor = System.Drawing.SystemColors.Window;
            this.tb_nendo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_nendo.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.tb_nendo.CharactersNumber = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.tb_nendo.Checked = false;
            this.tb_nendo.CustomFormat = "";
            this.tb_nendo.DateTimeNowYear = "";
            this.tb_nendo.DefaultBackColor = System.Drawing.Color.Empty;
            this.tb_nendo.DisplayOnlyYear = true;
            this.tb_nendo.DisplayPopUp = null;
            this.tb_nendo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("tb_nendo.FocusOutCheckMethod")));
            this.tb_nendo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.tb_nendo.ForeColor = System.Drawing.Color.Black;
            this.tb_nendo.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.tb_nendo.IsInputErrorOccured = false;
            this.tb_nendo.Location = new System.Drawing.Point(688, 24);
            this.tb_nendo.MaxLength = 4;
            this.tb_nendo.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.tb_nendo.Name = "tb_nendo";
            this.tb_nendo.NullValue = "";
            this.tb_nendo.PopupAfterExecute = null;
            this.tb_nendo.PopupBeforeExecute = null;
            this.tb_nendo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("tb_nendo.PopupSearchSendParams")));
            this.tb_nendo.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.tb_nendo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("tb_nendo.popupWindowSetting")));
            this.tb_nendo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("tb_nendo.RegistCheckMethod")));
            this.tb_nendo.Size = new System.Drawing.Size(108, 20);
            this.tb_nendo.TabIndex = 4;
            this.tb_nendo.Tag = "年度を入力してください";
            this.tb_nendo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tb_nendo.Value = null;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(589, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 20);
            this.label2.TabIndex = 8;
            this.label2.Tag = "年度を指定してください。";
            this.label2.Text = "年度※";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // alertNumber
            // 
            this.alertNumber.BackColor = System.Drawing.SystemColors.Window;
            this.alertNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.alertNumber.CustomFormatSetting = "#,##0";
            this.alertNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.alertNumber.DisplayPopUp = null;
            this.alertNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("alertNumber.FocusOutCheckMethod")));
            this.alertNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.alertNumber.ForeColor = System.Drawing.Color.Black;
            this.alertNumber.FormatSetting = "カスタム";
            this.alertNumber.IsInputErrorOccured = false;
            this.alertNumber.ItemDefinedTypes = "varchar";
            this.alertNumber.Location = new System.Drawing.Point(1088, 2);
            this.alertNumber.Name = "alertNumber";
            this.alertNumber.PopupAfterExecute = null;
            this.alertNumber.PopupBeforeExecute = null;
            this.alertNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("alertNumber.PopupSearchSendParams")));
            this.alertNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.alertNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("alertNumber.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.alertNumber.RangeSetting = rangeSettingDto1;
            this.alertNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("alertNumber.RegistCheckMethod")));
            this.alertNumber.Size = new System.Drawing.Size(80, 20);
            this.alertNumber.TabIndex = 14;
            this.alertNumber.Tag = "検索結果の総件数でアラートメッセージを表示させたい上限数を入力してください";
            this.alertNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.alertNumber.WordWrap = false;
            // 
            // rdbGetuji
            // 
            this.rdbGetuji.AutoSize = true;
            this.rdbGetuji.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdbGetuji.DisplayItemName = "しない";
            this.rdbGetuji.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdbGetuji.FocusOutCheckMethod")));
            this.rdbGetuji.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdbGetuji.LinkedTextBox = "txtKeisiki";
            this.rdbGetuji.Location = new System.Drawing.Point(886, 26);
            this.rdbGetuji.Name = "rdbGetuji";
            this.rdbGetuji.PopupAfterExecute = null;
            this.rdbGetuji.PopupBeforeExecute = null;
            this.rdbGetuji.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdbGetuji.PopupSearchSendParams")));
            this.rdbGetuji.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdbGetuji.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdbGetuji.popupWindowSetting")));
            this.rdbGetuji.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdbGetuji.RegistCheckMethod")));
            this.rdbGetuji.ShortItemName = "月次";
            this.rdbGetuji.Size = new System.Drawing.Size(67, 17);
            this.rdbGetuji.TabIndex = 12;
            this.rdbGetuji.Tag = "月次形式で表示する場合はチェックを付けてください。";
            this.rdbGetuji.Text = "2:月次";
            this.rdbGetuji.UseVisualStyleBackColor = true;
            this.rdbGetuji.Value = "2";
            // 
            // rdbNenji
            // 
            this.rdbNenji.AutoSize = true;
            this.rdbNenji.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdbNenji.DisplayItemName = "する";
            this.rdbNenji.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdbNenji.FocusOutCheckMethod")));
            this.rdbNenji.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdbNenji.LinkedTextBox = "txtKeisiki";
            this.rdbNenji.Location = new System.Drawing.Point(823, 26);
            this.rdbNenji.Name = "rdbNenji";
            this.rdbNenji.PopupAfterExecute = null;
            this.rdbNenji.PopupBeforeExecute = null;
            this.rdbNenji.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdbNenji.PopupSearchSendParams")));
            this.rdbNenji.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdbNenji.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdbNenji.popupWindowSetting")));
            this.rdbNenji.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdbNenji.RegistCheckMethod")));
            this.rdbNenji.ShortItemName = "年次";
            this.rdbNenji.Size = new System.Drawing.Size(67, 17);
            this.rdbNenji.TabIndex = 11;
            this.rdbNenji.Tag = "年次形式で表示する場合はチェックを付けてください。";
            this.rdbNenji.Text = "1:年次";
            this.rdbNenji.UseVisualStyleBackColor = true;
            this.rdbNenji.Value = "1";
            // 
            // txtKeisiki
            // 
            this.txtKeisiki.BackColor = System.Drawing.SystemColors.Window;
            this.txtKeisiki.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKeisiki.DBFieldsName = "SHOBUN_UPDATE_KBN";
            this.txtKeisiki.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtKeisiki.DisplayItemName = "出力形式設定";
            this.txtKeisiki.DisplayPopUp = null;
            this.txtKeisiki.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKeisiki.FocusOutCheckMethod")));
            this.txtKeisiki.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtKeisiki.ForeColor = System.Drawing.Color.Black;
            this.txtKeisiki.IsInputErrorOccured = false;
            this.txtKeisiki.ItemDefinedTypes = "smallint";
            this.txtKeisiki.LinkedRadioButtonArray = new string[] {
        "rdbNenji",
        "rdbGetuji"};
            this.txtKeisiki.Location = new System.Drawing.Point(799, 24);
            this.txtKeisiki.Name = "txtKeisiki";
            this.txtKeisiki.PopupAfterExecute = null;
            this.txtKeisiki.PopupAfterExecuteMethod = "PopupAfterShobunKbn";
            this.txtKeisiki.PopupBeforeExecute = null;
            this.txtKeisiki.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtKeisiki.PopupSearchSendParams")));
            this.txtKeisiki.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtKeisiki.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtKeisiki.popupWindowSetting")));
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
            this.txtKeisiki.RangeSetting = rangeSettingDto2;
            this.txtKeisiki.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKeisiki.RegistCheckMethod")));
            this.txtKeisiki.ShortItemName = "出力形式";
            this.txtKeisiki.Size = new System.Drawing.Size(20, 20);
            this.txtKeisiki.TabIndex = 10;
            this.txtKeisiki.Tag = "出力形式を指定してください。";
            this.txtKeisiki.WordWrap = false;
            // 
            // F18_G275UIHeaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rdbGetuji);
            this.Controls.Add(this.rdbNenji);
            this.Controls.Add(this.txtKeisiki);
            this.Controls.Add(this.alertNumber);
            this.Controls.Add(this.tb_nendo);
            this.Controls.Add(this.lbl_アラート件数);
            this.Controls.Add(this.ReadDataNumber);
            this.Controls.Add(this.lbl_読込データ件数);
            this.Name = "F18_G275UIHeaderForm";
            this.Text = "HeaderForm";
            this.Load += new System.EventHandler(this.F18_G275UIHeaderForm_Load);
            this.Controls.SetChildIndex(this.lbl_読込データ件数, 0);
            this.Controls.SetChildIndex(this.ReadDataNumber, 0);
            this.Controls.SetChildIndex(this.lbl_アラート件数, 0);
            this.Controls.SetChildIndex(this.tb_nendo, 0);
            this.Controls.SetChildIndex(this.alertNumber, 0);
            this.Controls.SetChildIndex(this.txtKeisiki, 0);
            this.Controls.SetChildIndex(this.rdbNenji, 0);
            this.Controls.SetChildIndex(this.rdbGetuji, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomTextBox ReadDataNumber;
        internal System.Windows.Forms.Label lbl_アラート件数;
        public System.Windows.Forms.Label lbl_読込データ件数;
        internal r_framework.CustomControl.CustomDateTimePicker tb_nendo;
        //internal r_framework.CustomControl.CustomNumericTextBox2 tb_nendo;
        private System.Windows.Forms.Label label2;
        public r_framework.CustomControl.CustomNumericTextBox2 alertNumber;
        internal r_framework.CustomControl.CustomRadioButton rdbGetuji;
        internal r_framework.CustomControl.CustomRadioButton rdbNenji;
        internal r_framework.CustomControl.CustomNumericTextBox2 txtKeisiki;
    }
}