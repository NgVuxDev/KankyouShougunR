namespace Shougun.Core.Common.KokyakuKarute.APP
{
    partial class G173HeaderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(G173HeaderForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.KYOTEN_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.KYOTEN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label2 = new System.Windows.Forms.Label();
            this.DATE_FROM = new r_framework.CustomControl.CustomDateTimePicker();
            this.DATE_TO = new r_framework.CustomControl.CustomDateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_title
            // 
            this.lb_title.Size = new System.Drawing.Size(230, 34);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(2, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 20);
            this.label3.TabIndex = 390;
            this.label3.Text = "作業日※";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(2, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 20);
            this.label1.TabIndex = 389;
            this.label1.Text = "拠点※";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KYOTEN_NAME_RYAKU
            // 
            this.KYOTEN_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KYOTEN_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_NAME_RYAKU.DisplayPopUp = null;
            this.KYOTEN_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.FocusOutCheckMethod")));
            this.KYOTEN_NAME_RYAKU.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.KYOTEN_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_NAME_RYAKU.GetCodeMasterField = "";
            this.KYOTEN_NAME_RYAKU.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.KYOTEN_NAME_RYAKU.IsInputErrorOccured = false;
            this.KYOTEN_NAME_RYAKU.ItemDefinedTypes = "";
            this.KYOTEN_NAME_RYAKU.Location = new System.Drawing.Point(196, 1);
            this.KYOTEN_NAME_RYAKU.Name = "KYOTEN_NAME_RYAKU";
            this.KYOTEN_NAME_RYAKU.PopupAfterExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupBeforeExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupGetMasterField = "";
            this.KYOTEN_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.PopupSearchSendParams")));
            this.KYOTEN_NAME_RYAKU.PopupSetFormField = "";
            this.KYOTEN_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOTEN_NAME_RYAKU.PopupWindowName = "";
            this.KYOTEN_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.popupWindowSetting")));
            this.KYOTEN_NAME_RYAKU.ReadOnly = true;
            this.KYOTEN_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.RegistCheckMethod")));
            this.KYOTEN_NAME_RYAKU.SetFormField = "";
            this.KYOTEN_NAME_RYAKU.ShortItemName = "";
            this.KYOTEN_NAME_RYAKU.Size = new System.Drawing.Size(160, 20);
            this.KYOTEN_NAME_RYAKU.TabIndex = 392;
            this.KYOTEN_NAME_RYAKU.TabStop = false;
            this.KYOTEN_NAME_RYAKU.Tag = "検索する文字を入力してください";
            // 
            // KYOTEN_CD
            // 
            this.KYOTEN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.KYOTEN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_CD.CustomFormatSetting = "00";
            this.KYOTEN_CD.DBFieldsName = "KYOTEN_CD";
            this.KYOTEN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_CD.DisplayItemName = "拠点CD";
            this.KYOTEN_CD.DisplayPopUp = null;
            this.KYOTEN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.FocusOutCheckMethod")));
            this.KYOTEN_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.KYOTEN_CD.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_CD.FormatSetting = "カスタム";
            this.KYOTEN_CD.GetCodeMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.IsInputErrorOccured = false;
            this.KYOTEN_CD.ItemDefinedTypes = "smallint";
            this.KYOTEN_CD.Location = new System.Drawing.Point(167, 1);
            this.KYOTEN_CD.Name = "KYOTEN_CD";
            this.KYOTEN_CD.PopupAfterExecute = null;
            this.KYOTEN_CD.PopupBeforeExecute = null;
            this.KYOTEN_CD.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_CD.PopupSearchSendParams")));
            this.KYOTEN_CD.PopupSetFormField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.KYOTEN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.KYOTEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_CD.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.KYOTEN_CD.RangeSetting = rangeSettingDto1;
            this.KYOTEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.RegistCheckMethod")));
            this.KYOTEN_CD.SetFormField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.Size = new System.Drawing.Size(30, 20);
            this.KYOTEN_CD.TabIndex = 391;
            this.KYOTEN_CD.TabStop = false;
            this.KYOTEN_CD.Tag = "半角2桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.KYOTEN_CD.WordWrap = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(283, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 394;
            this.label2.Text = "～";
            // 
            // DATE_FROM
            // 
            this.DATE_FROM.BackColor = System.Drawing.SystemColors.Window;
            this.DATE_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DATE_FROM.CalendarFont = new System.Drawing.Font("MS Gothic", 9F);
            this.DATE_FROM.Checked = false;
            this.DATE_FROM.CustomFormat = "yyyy/MM/dd(ddd)";
            this.DATE_FROM.DateTimeNowYear = "";
            this.DATE_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_FROM.DisplayItemName = "開始日付";
            this.DATE_FROM.DisplayPopUp = null;
            this.DATE_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_FROM.FocusOutCheckMethod")));
            this.DATE_FROM.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.DATE_FROM.ForeColor = System.Drawing.Color.Black;
            this.DATE_FROM.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DATE_FROM.IsInputErrorOccured = false;
            this.DATE_FROM.Location = new System.Drawing.Point(167, 23);
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
            this.DATE_FROM.Size = new System.Drawing.Size(110, 20);
            this.DATE_FROM.TabIndex = 393;
            this.DATE_FROM.Tag = "日付を選択してください";
            this.DATE_FROM.Text = "2013/10/31(木)";
            this.DATE_FROM.Value = new System.DateTime(2013, 10, 31, 0, 0, 0, 0);
            // 
            // DATE_TO
            // 
            this.DATE_TO.BackColor = System.Drawing.SystemColors.Window;
            this.DATE_TO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DATE_TO.CalendarFont = new System.Drawing.Font("MS Gothic", 9F);
            this.DATE_TO.Checked = false;
            this.DATE_TO.CustomFormat = "yyyy/MM/dd(ddd)";
            this.DATE_TO.DateTimeNowYear = "";
            this.DATE_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_TO.DisplayItemName = "終了日付";
            this.DATE_TO.DisplayPopUp = null;
            this.DATE_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_TO.FocusOutCheckMethod")));
            this.DATE_TO.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.DATE_TO.ForeColor = System.Drawing.Color.Black;
            this.DATE_TO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DATE_TO.IsInputErrorOccured = false;
            this.DATE_TO.Location = new System.Drawing.Point(305, 23);
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
            this.DATE_TO.Size = new System.Drawing.Size(110, 20);
            this.DATE_TO.TabIndex = 395;
            this.DATE_TO.Tag = "日付を選択してください";
            this.DATE_TO.Text = "2013/10/31(木)";
            this.DATE_TO.Value = new System.DateTime(2013, 10, 31, 0, 0, 0, 0);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.DATE_FROM);
            this.panel1.Controls.Add(this.KYOTEN_CD);
            this.panel1.Controls.Add(this.DATE_TO);
            this.panel1.Controls.Add(this.KYOTEN_NAME_RYAKU);
            this.panel1.Location = new System.Drawing.Point(318, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(427, 44);
            this.panel1.TabIndex = 396;
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
            this.ISNOT_NEED_DELETE_FLG.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.ISNOT_NEED_DELETE_FLG.ForeColor = System.Drawing.Color.Black;
            this.ISNOT_NEED_DELETE_FLG.GetCodeMasterField = "";
            this.ISNOT_NEED_DELETE_FLG.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ISNOT_NEED_DELETE_FLG.IsInputErrorOccured = false;
            this.ISNOT_NEED_DELETE_FLG.ItemDefinedTypes = "bit";
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(766, 24);
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
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 10007;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "True";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            this.ISNOT_NEED_DELETE_FLG.ZeroPaddengFlag = true;
            // 
            // G173HeaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.panel1);
            this.Location = new System.Drawing.Point(12, 6);
            this.Name = "G173HeaderForm";
            this.Text = "G173HeaderForm";
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.ISNOT_NEED_DELETE_FLG, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label1;
        public r_framework.CustomControl.CustomTextBox KYOTEN_NAME_RYAKU;
        public r_framework.CustomControl.CustomNumericTextBox2 KYOTEN_CD;
        public System.Windows.Forms.Label label2;
        internal r_framework.CustomControl.CustomDateTimePicker DATE_FROM;
        internal r_framework.CustomControl.CustomDateTimePicker DATE_TO;
        private System.Windows.Forms.Panel panel1;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
    }
}