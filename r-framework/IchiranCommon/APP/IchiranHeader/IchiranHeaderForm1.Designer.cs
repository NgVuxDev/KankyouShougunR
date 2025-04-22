using r_framework.CustomControl;

namespace Shougun.Core.Common.IchiranCommon.APP
{
    partial class IchiranHeaderForm1
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
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            this.lb_title = new System.Windows.Forms.Label();
            this.lbl_アラート件数 = new System.Windows.Forms.Label();
            this.lbl_読込データ件数 = new System.Windows.Forms.Label();
            this.HEADER_KYOTEN_NAME = new r_framework.CustomControl.CustomTextBox();
            this.lbl_読込日時 = new System.Windows.Forms.Label();
            this.alertNumber = new r_framework.CustomControl.CustomNumericTextBox2();
            this.HEADER_KYOTEN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.ReadDataNumber = new r_framework.CustomControl.CustomNumericTextBox2();
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
            this.lb_title.Size = new System.Drawing.Size(460, 34);
            this.lb_title.TabIndex = 0;
            this.lb_title.Text = "○○○○○タイトル";
            this.lb_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_title.TextChanged += new System.EventHandler(this.lb_title_TextChanged);
            // 
            // lbl_アラート件数
            // 
            this.lbl_アラート件数.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_アラート件数.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_アラート件数.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_アラート件数.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_アラート件数.ForeColor = System.Drawing.Color.White;
            this.lbl_アラート件数.Location = new System.Drawing.Point(973, 2);
            this.lbl_アラート件数.Name = "lbl_アラート件数";
            this.lbl_アラート件数.Size = new System.Drawing.Size(110, 20);
            this.lbl_アラート件数.TabIndex = 4;
            this.lbl_アラート件数.Text = "アラート件数";
            this.lbl_アラート件数.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_読込データ件数
            // 
            this.lbl_読込データ件数.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_読込データ件数.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_読込データ件数.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_読込データ件数.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_読込データ件数.ForeColor = System.Drawing.Color.White;
            this.lbl_読込データ件数.Location = new System.Drawing.Point(973, 24);
            this.lbl_読込データ件数.Name = "lbl_読込データ件数";
            this.lbl_読込データ件数.Size = new System.Drawing.Size(110, 20);
            this.lbl_読込データ件数.TabIndex = 6;
            this.lbl_読込データ件数.Text = "読込データ件数";
            this.lbl_読込データ件数.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HEADER_KYOTEN_NAME
            // 
            this.HEADER_KYOTEN_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.HEADER_KYOTEN_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HEADER_KYOTEN_NAME.DBFieldsName = "KYOTEN_NAME";
            this.HEADER_KYOTEN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.HEADER_KYOTEN_NAME.DisplayPopUp = null;
            this.HEADER_KYOTEN_NAME.FocusOutCheckMethod = null;
            this.HEADER_KYOTEN_NAME.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.HEADER_KYOTEN_NAME.ForeColor = System.Drawing.Color.Black;
            this.HEADER_KYOTEN_NAME.IsInputErrorOccured = false;
            this.HEADER_KYOTEN_NAME.Location = new System.Drawing.Point(808, 2);
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
            this.HEADER_KYOTEN_NAME.Tag = "拠点を表示します";
            // 
            // lbl_読込日時
            // 
            this.lbl_読込日時.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_読込日時.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_読込日時.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_読込日時.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_読込日時.ForeColor = System.Drawing.Color.White;
            this.lbl_読込日時.Location = new System.Drawing.Point(664, 2);
            this.lbl_読込日時.Name = "lbl_読込日時";
            this.lbl_読込日時.Size = new System.Drawing.Size(110, 20);
            this.lbl_読込日時.TabIndex = 1;
            this.lbl_読込日時.Text = "拠点";
            this.lbl_読込日時.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // alertNumber
            // 
            this.alertNumber.BackColor = System.Drawing.SystemColors.Window;
            this.alertNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.alertNumber.CustomFormatSetting = "#,##0";
            this.alertNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.alertNumber.DisplayPopUp = null;
            this.alertNumber.FocusOutCheckMethod = null;
            this.alertNumber.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.alertNumber.ForeColor = System.Drawing.Color.Black;
            this.alertNumber.FormatSetting = "カスタム";
            this.alertNumber.IsInputErrorOccured = false;
            this.alertNumber.ItemDefinedTypes = "long";
            this.alertNumber.Location = new System.Drawing.Point(1088, 2);
            this.alertNumber.Name = "alertNumber";
            this.alertNumber.PopupAfterExecute = null;
            this.alertNumber.PopupBeforeExecute = null;
            this.alertNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.alertNumber.popupWindowSetting = null;
            rangeSettingDto1.Max = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.alertNumber.RangeSetting = rangeSettingDto1;
            this.alertNumber.RegistCheckMethod = null;
            this.alertNumber.Size = new System.Drawing.Size(80, 20);
            this.alertNumber.TabIndex = 5;
            this.alertNumber.Tag = "検索結果の総件数でアラートメッセージを表示させたい上限数を入力してください";
            this.alertNumber.Text = "0";
            this.alertNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.alertNumber.WordWrap = false;
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
            this.HEADER_KYOTEN_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HEADER_KYOTEN_CD.ForeColor = System.Drawing.Color.Black;
            this.HEADER_KYOTEN_CD.FormatSetting = "カスタム";
            this.HEADER_KYOTEN_CD.GetCodeMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.HEADER_KYOTEN_CD.IsInputErrorOccured = false;
            this.HEADER_KYOTEN_CD.ItemDefinedTypes = "smallint";
            this.HEADER_KYOTEN_CD.Location = new System.Drawing.Point(779, 2);
            this.HEADER_KYOTEN_CD.Name = "HEADER_KYOTEN_CD";
            this.HEADER_KYOTEN_CD.PopupAfterExecute = null;
            this.HEADER_KYOTEN_CD.PopupBeforeExecute = null;
            this.HEADER_KYOTEN_CD.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.HEADER_KYOTEN_CD.PopupSetFormField = "HEADER_KYOTEN_CD,HEADER_KYOTEN_NAME";
            this.HEADER_KYOTEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.HEADER_KYOTEN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.HEADER_KYOTEN_CD.popupWindowSetting = null;
            rangeSettingDto2.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.HEADER_KYOTEN_CD.RangeSetting = rangeSettingDto2;
            this.HEADER_KYOTEN_CD.RegistCheckMethod = null;
            this.HEADER_KYOTEN_CD.SearchDisplayFlag = 2;
            this.HEADER_KYOTEN_CD.SetFormField = "HEADER_KYOTEN_CD,HEADER_KYOTEN_NAME";
            this.HEADER_KYOTEN_CD.ShortItemName = "拠点CD";
            this.HEADER_KYOTEN_CD.Size = new System.Drawing.Size(30, 20);
            this.HEADER_KYOTEN_CD.TabIndex = 2;
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
            this.ReadDataNumber.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ReadDataNumber.ForeColor = System.Drawing.Color.Black;
            this.ReadDataNumber.FormatSetting = "カスタム";
            this.ReadDataNumber.IsInputErrorOccured = false;
            this.ReadDataNumber.ItemDefinedTypes = "float";
            this.ReadDataNumber.Location = new System.Drawing.Point(1088, 24);
            this.ReadDataNumber.Name = "ReadDataNumber";
            this.ReadDataNumber.PopupAfterExecute = null;
            this.ReadDataNumber.PopupBeforeExecute = null;
            this.ReadDataNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ReadDataNumber.popupWindowSetting = null;
            this.ReadDataNumber.RangeSetting = rangeSettingDto3;
            this.ReadDataNumber.ReadOnly = true;
            this.ReadDataNumber.RegistCheckMethod = null;
            this.ReadDataNumber.Size = new System.Drawing.Size(80, 20);
            this.ReadDataNumber.TabIndex = 7;
            this.ReadDataNumber.Tag = "検索結果の総件数が表示されます";
            this.ReadDataNumber.Text = "0";
            this.ReadDataNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ReadDataNumber.WordWrap = false;
            // 
            // IchiranHeaderForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.HEADER_KYOTEN_CD);
            this.Controls.Add(this.alertNumber);
            this.Controls.Add(this.ReadDataNumber);
            this.Controls.Add(this.HEADER_KYOTEN_NAME);
            this.Controls.Add(this.lbl_読込データ件数);
            this.Controls.Add(this.lbl_アラート件数);
            this.Controls.Add(this.lbl_読込日時);
            this.Controls.Add(this.lb_title);
            this.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "IchiranHeaderForm1";
            this.Text = "BaseForm01";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lb_title;
        private System.Windows.Forms.Label lbl_読込データ件数;
        private System.Windows.Forms.Label lbl_読込日時;
        public CustomTextBox HEADER_KYOTEN_NAME;
        public CustomNumericTextBox2 alertNumber;
        public CustomNumericTextBox2 ReadDataNumber;
        public CustomNumericTextBox2 HEADER_KYOTEN_CD;
        public System.Windows.Forms.Label lbl_アラート件数;
    }
}