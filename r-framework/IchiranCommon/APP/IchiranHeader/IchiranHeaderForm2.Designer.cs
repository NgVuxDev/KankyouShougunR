using r_framework.CustomControl;

namespace Shougun.Core.Common.IchiranCommon.APP
{
    partial class IchiranHeaderForm2
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
            this.lb_title = new System.Windows.Forms.Label();
            this.lbl_アラート件数 = new System.Windows.Forms.Label();
            this.lbl_読込データ件数 = new System.Windows.Forms.Label();
            this.alertNumber = new r_framework.CustomControl.CustomNumericTextBox2();
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
            this.lb_title.TabIndex = 200;
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
            this.lbl_アラート件数.TabIndex = 373;
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
            this.lbl_読込データ件数.TabIndex = 375;
            this.lbl_読込データ件数.Text = "読込データ件数";
            this.lbl_読込データ件数.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.alertNumber.TabIndex = 380;
            this.alertNumber.Tag = "検索結果の総件数でアラートメッセージを表示させたい上限数を入力してください";
            this.alertNumber.Text = "0";
            this.alertNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.alertNumber.WordWrap = false;
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
            this.ReadDataNumber.RangeSetting = rangeSettingDto1;
            this.ReadDataNumber.ReadOnly = true;
            this.ReadDataNumber.RegistCheckMethod = null;
            this.ReadDataNumber.Size = new System.Drawing.Size(80, 20);
            this.ReadDataNumber.TabIndex = 379;
            this.ReadDataNumber.TabStop = false;
            this.ReadDataNumber.Tag = "検索結果の総件数が表示されます";
            this.ReadDataNumber.Text = "0";
            this.ReadDataNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ReadDataNumber.WordWrap = false;
            // 
            // IchiranHeaderForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.alertNumber);
            this.Controls.Add(this.ReadDataNumber);
            this.Controls.Add(this.lbl_読込データ件数);
            this.Controls.Add(this.lbl_アラート件数);
            this.Controls.Add(this.lb_title);
            this.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "IchiranHeaderForm2";
            this.Text = "BaseForm01";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lb_title;
        private System.Windows.Forms.Label lbl_読込データ件数;
        public CustomNumericTextBox2 alertNumber;
        public CustomNumericTextBox2 ReadDataNumber;
        public System.Windows.Forms.Label lbl_アラート件数;
    }
}