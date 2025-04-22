namespace Shougun.Core.ExternalConnection.CourseSaitekikaNyuuryoku
{
    partial class UIHeader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIHeader));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.KYOTEN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.KYOTEN_LABEL = new System.Windows.Forms.Label();
            this.KYOTEN_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.NAVI_GET_TIME_PANEL = new r_framework.CustomControl.CustomPanel();
            this.NAVI_GET_TIME_1 = new r_framework.CustomControl.CustomRadioButton();
            this.NAVI_GET_TIME_2 = new r_framework.CustomControl.CustomRadioButton();
            this.NAVI_GET_TIME = new r_framework.CustomControl.CustomNumericTextBox2();
            this.NAVI_GET_TIME_PANEL.SuspendLayout();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.TabIndex = 10;
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(2, 6);
            this.lb_title.Size = new System.Drawing.Size(360, 34);
            this.lb_title.TabIndex = 20;
            // 
            // KYOTEN_CD
            // 
            this.KYOTEN_CD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KYOTEN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_CD.CustomFormatSetting = "00";
            this.KYOTEN_CD.DBFieldsName = "KYOTEN_CD";
            this.KYOTEN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_CD.DisplayItemName = "拠点";
            this.KYOTEN_CD.DisplayPopUp = null;
            this.KYOTEN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.FocusOutCheckMethod")));
            this.KYOTEN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_CD.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_CD.FormatSetting = "カスタム";
            this.KYOTEN_CD.GetCodeMasterField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.IsInputErrorOccured = false;
            this.KYOTEN_CD.ItemDefinedTypes = "smallint";
            this.KYOTEN_CD.Location = new System.Drawing.Point(545, 13);
            this.KYOTEN_CD.Name = "KYOTEN_CD";
            this.KYOTEN_CD.PopupAfterExecute = null;
            this.KYOTEN_CD.PopupBeforeExecute = null;
            this.KYOTEN_CD.PopupGetMasterField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_CD.PopupSearchSendParams")));
            this.KYOTEN_CD.PopupSetFormField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.KYOTEN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.KYOTEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_CD.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.KYOTEN_CD.RangeSetting = rangeSettingDto1;
            this.KYOTEN_CD.ReadOnly = true;
            this.KYOTEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.RegistCheckMethod")));
            this.KYOTEN_CD.SetFormField = "KYOTEN_CD, KYOTEN_NAME_RYAKU,COURSE_NAME_CD, COURSE_NAME_RYAKU";
            this.KYOTEN_CD.Size = new System.Drawing.Size(20, 20);
            this.KYOTEN_CD.TabIndex = 40;
            this.KYOTEN_CD.TabStop = false;
            this.KYOTEN_CD.Tag = "拠点を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.KYOTEN_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.KYOTEN_CD.WordWrap = false;
            // 
            // KYOTEN_LABEL
            // 
            this.KYOTEN_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.KYOTEN_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.KYOTEN_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_LABEL.ForeColor = System.Drawing.Color.White;
            this.KYOTEN_LABEL.Location = new System.Drawing.Point(440, 13);
            this.KYOTEN_LABEL.Name = "KYOTEN_LABEL";
            this.KYOTEN_LABEL.Size = new System.Drawing.Size(100, 20);
            this.KYOTEN_LABEL.TabIndex = 30;
            this.KYOTEN_LABEL.Text = "拠点";
            this.KYOTEN_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KYOTEN_NAME_RYAKU
            // 
            this.KYOTEN_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KYOTEN_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.KYOTEN_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_NAME_RYAKU.DisplayItemName = "KYOTEN_NAME_RYAKU";
            this.KYOTEN_NAME_RYAKU.DisplayPopUp = null;
            this.KYOTEN_NAME_RYAKU.ErrorMessage = "";
            this.KYOTEN_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.FocusOutCheckMethod")));
            this.KYOTEN_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_NAME_RYAKU.GetCodeMasterField = "";
            this.KYOTEN_NAME_RYAKU.IsInputErrorOccured = false;
            this.KYOTEN_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.KYOTEN_NAME_RYAKU.Location = new System.Drawing.Point(564, 13);
            this.KYOTEN_NAME_RYAKU.MaxLength = 0;
            this.KYOTEN_NAME_RYAKU.Name = "KYOTEN_NAME_RYAKU";
            this.KYOTEN_NAME_RYAKU.PopupAfterExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupBeforeExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupGetMasterField = "";
            this.KYOTEN_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.PopupSearchSendParams")));
            this.KYOTEN_NAME_RYAKU.PopupSetFormField = "";
            this.KYOTEN_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOTEN_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.popupWindowSetting")));
            this.KYOTEN_NAME_RYAKU.ReadOnly = true;
            this.KYOTEN_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.RegistCheckMethod")));
            this.KYOTEN_NAME_RYAKU.SetFormField = "";
            this.KYOTEN_NAME_RYAKU.Size = new System.Drawing.Size(80, 20);
            this.KYOTEN_NAME_RYAKU.TabIndex = 50;
            this.KYOTEN_NAME_RYAKU.TabStop = false;
            this.KYOTEN_NAME_RYAKU.Tag = "";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(694, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 20);
            this.label1.TabIndex = 110;
            this.label1.Text = "時間取得";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NAVI_GET_TIME_PANEL
            // 
            this.NAVI_GET_TIME_PANEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NAVI_GET_TIME_PANEL.Controls.Add(this.NAVI_GET_TIME_1);
            this.NAVI_GET_TIME_PANEL.Controls.Add(this.NAVI_GET_TIME_2);
            this.NAVI_GET_TIME_PANEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.NAVI_GET_TIME_PANEL.Location = new System.Drawing.Point(818, 13);
            this.NAVI_GET_TIME_PANEL.Name = "NAVI_GET_TIME_PANEL";
            this.NAVI_GET_TIME_PANEL.Size = new System.Drawing.Size(142, 20);
            this.NAVI_GET_TIME_PANEL.TabIndex = 130;
            // 
            // NAVI_GET_TIME_1
            // 
            this.NAVI_GET_TIME_1.AutoSize = true;
            this.NAVI_GET_TIME_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.NAVI_GET_TIME_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NAVI_GET_TIME_1.FocusOutCheckMethod")));
            this.NAVI_GET_TIME_1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.NAVI_GET_TIME_1.LinkedTextBox = "NAVI_GET_TIME";
            this.NAVI_GET_TIME_1.Location = new System.Drawing.Point(8, 1);
            this.NAVI_GET_TIME_1.Name = "NAVI_GET_TIME_1";
            this.NAVI_GET_TIME_1.PopupAfterExecute = null;
            this.NAVI_GET_TIME_1.PopupBeforeExecute = null;
            this.NAVI_GET_TIME_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NAVI_GET_TIME_1.PopupSearchSendParams")));
            this.NAVI_GET_TIME_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NAVI_GET_TIME_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NAVI_GET_TIME_1.popupWindowSetting")));
            this.NAVI_GET_TIME_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NAVI_GET_TIME_1.RegistCheckMethod")));
            this.NAVI_GET_TIME_1.Size = new System.Drawing.Size(53, 17);
            this.NAVI_GET_TIME_1.TabIndex = 140;
            this.NAVI_GET_TIME_1.Tag = "要の場合チェックを付けてください";
            this.NAVI_GET_TIME_1.Text = "1.要";
            this.NAVI_GET_TIME_1.UseVisualStyleBackColor = true;
            this.NAVI_GET_TIME_1.Value = "1";
            // 
            // NAVI_GET_TIME_2
            // 
            this.NAVI_GET_TIME_2.AutoSize = true;
            this.NAVI_GET_TIME_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.NAVI_GET_TIME_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NAVI_GET_TIME_2.FocusOutCheckMethod")));
            this.NAVI_GET_TIME_2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.NAVI_GET_TIME_2.LinkedTextBox = "NAVI_GET_TIME";
            this.NAVI_GET_TIME_2.Location = new System.Drawing.Point(65, 1);
            this.NAVI_GET_TIME_2.Name = "NAVI_GET_TIME_2";
            this.NAVI_GET_TIME_2.PopupAfterExecute = null;
            this.NAVI_GET_TIME_2.PopupBeforeExecute = null;
            this.NAVI_GET_TIME_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NAVI_GET_TIME_2.PopupSearchSendParams")));
            this.NAVI_GET_TIME_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NAVI_GET_TIME_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NAVI_GET_TIME_2.popupWindowSetting")));
            this.NAVI_GET_TIME_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NAVI_GET_TIME_2.RegistCheckMethod")));
            this.NAVI_GET_TIME_2.Size = new System.Drawing.Size(67, 17);
            this.NAVI_GET_TIME_2.TabIndex = 150;
            this.NAVI_GET_TIME_2.Tag = "不要の場合チェックを付けてください";
            this.NAVI_GET_TIME_2.Text = "2.不要";
            this.NAVI_GET_TIME_2.UseVisualStyleBackColor = true;
            this.NAVI_GET_TIME_2.Value = "2";
            // 
            // NAVI_GET_TIME
            // 
            this.NAVI_GET_TIME.BackColor = System.Drawing.SystemColors.Window;
            this.NAVI_GET_TIME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NAVI_GET_TIME.DBFieldsName = "NAVI_GET_TIME";
            this.NAVI_GET_TIME.DefaultBackColor = System.Drawing.Color.Empty;
            this.NAVI_GET_TIME.DisplayItemName = "時間取得";
            this.NAVI_GET_TIME.DisplayPopUp = null;
            this.NAVI_GET_TIME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NAVI_GET_TIME.FocusOutCheckMethod")));
            this.NAVI_GET_TIME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.NAVI_GET_TIME.ForeColor = System.Drawing.Color.Black;
            this.NAVI_GET_TIME.IsInputErrorOccured = false;
            this.NAVI_GET_TIME.ItemDefinedTypes = "smallint";
            this.NAVI_GET_TIME.LinkedRadioButtonArray = new string[] {
        "NAVI_GET_TIME_1",
        "NAVI_GET_TIME_2"};
            this.NAVI_GET_TIME.Location = new System.Drawing.Point(799, 13);
            this.NAVI_GET_TIME.Name = "NAVI_GET_TIME";
            this.NAVI_GET_TIME.PopupAfterExecute = null;
            this.NAVI_GET_TIME.PopupBeforeExecute = null;
            this.NAVI_GET_TIME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NAVI_GET_TIME.PopupSearchSendParams")));
            this.NAVI_GET_TIME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NAVI_GET_TIME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NAVI_GET_TIME.popupWindowSetting")));
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
            this.NAVI_GET_TIME.RangeSetting = rangeSettingDto2;
            this.NAVI_GET_TIME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NAVI_GET_TIME.RegistCheckMethod")));
            this.NAVI_GET_TIME.ShortItemName = "時間取得";
            this.NAVI_GET_TIME.Size = new System.Drawing.Size(20, 20);
            this.NAVI_GET_TIME.TabIndex = 120;
            this.NAVI_GET_TIME.Tag = "【1～2】のいずれかで入力してください";
            this.NAVI_GET_TIME.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.NAVI_GET_TIME.WordWrap = false;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 46);
            this.Controls.Add(this.NAVI_GET_TIME);
            this.Controls.Add(this.NAVI_GET_TIME_PANEL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.KYOTEN_CD);
            this.Controls.Add(this.KYOTEN_LABEL);
            this.Controls.Add(this.KYOTEN_NAME_RYAKU);
            this.Name = "UIHeader";
            this.Text = "UIHeader";
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.KYOTEN_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.KYOTEN_LABEL, 0);
            this.Controls.SetChildIndex(this.KYOTEN_CD, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.NAVI_GET_TIME_PANEL, 0);
            this.Controls.SetChildIndex(this.NAVI_GET_TIME, 0);
            this.NAVI_GET_TIME_PANEL.ResumeLayout(false);
            this.NAVI_GET_TIME_PANEL.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomNumericTextBox2 KYOTEN_CD;
        public System.Windows.Forms.Label KYOTEN_LABEL;
        public r_framework.CustomControl.CustomTextBox KYOTEN_NAME_RYAKU;
        public System.Windows.Forms.Label label1;
        internal r_framework.CustomControl.CustomPanel NAVI_GET_TIME_PANEL;
        public r_framework.CustomControl.CustomRadioButton NAVI_GET_TIME_1;
        public r_framework.CustomControl.CustomRadioButton NAVI_GET_TIME_2;
        public r_framework.CustomControl.CustomNumericTextBox2 NAVI_GET_TIME;
    }
}