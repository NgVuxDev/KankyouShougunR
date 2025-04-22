namespace Shougun.Core.Inspection.KensyuuIchiran
{
    partial class KensyuuIchiranHeader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KensyuuIchiranHeader));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.KYOTEN_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.KYOTEN_CD = new r_framework.CustomControl.CustomTextBox();
            this.lbl_Kyoten = new System.Windows.Forms.Label();
            this.ctxt_ReadDataNumber = new r_framework.CustomControl.CustomNumericTextBox2();
            this.ctxt_AlertNumber = new r_framework.CustomControl.CustomNumericTextBox2();
            this.SuspendLayout();
            // 
            // lb_title
            // 
            this.lb_title.Size = new System.Drawing.Size(387, 34);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(917, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 538;
            this.label2.Text = "アラート件数";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(917, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 536;
            this.label1.Text = "読込データ件数";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.KYOTEN_NAME_RYAKU.DisplayPopUp = null;
            this.KYOTEN_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.FocusOutCheckMethod")));
            this.KYOTEN_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KYOTEN_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_NAME_RYAKU.IsInputErrorOccured = false;
            this.KYOTEN_NAME_RYAKU.Location = new System.Drawing.Point(704, 6);
            this.KYOTEN_NAME_RYAKU.MaxLength = 0;
            this.KYOTEN_NAME_RYAKU.Name = "KYOTEN_NAME_RYAKU";
            this.KYOTEN_NAME_RYAKU.PopupAfterExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupBeforeExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.PopupSearchSendParams")));
            this.KYOTEN_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOTEN_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.popupWindowSetting")));
            this.KYOTEN_NAME_RYAKU.ReadOnly = true;
            this.KYOTEN_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.RegistCheckMethod")));
            this.KYOTEN_NAME_RYAKU.Size = new System.Drawing.Size(208, 20);
            this.KYOTEN_NAME_RYAKU.TabIndex = 532;
            this.KYOTEN_NAME_RYAKU.TabStop = false;
            this.KYOTEN_NAME_RYAKU.Tag = "";
            // 
            // KYOTEN_CD
            // 
            this.KYOTEN_CD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KYOTEN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_CD.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.KYOTEN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_CD.DisplayPopUp = null;
            this.KYOTEN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.FocusOutCheckMethod")));
            this.KYOTEN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_CD.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_CD.IsInputErrorOccured = false;
            this.KYOTEN_CD.Location = new System.Drawing.Point(652, 6);
            this.KYOTEN_CD.MaxLength = 0;
            this.KYOTEN_CD.Name = "KYOTEN_CD";
            this.KYOTEN_CD.PopupAfterExecute = null;
            this.KYOTEN_CD.PopupBeforeExecute = null;
            this.KYOTEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_CD.PopupSearchSendParams")));
            this.KYOTEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOTEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_CD.popupWindowSetting")));
            this.KYOTEN_CD.ReadOnly = true;
            this.KYOTEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.RegistCheckMethod")));
            this.KYOTEN_CD.Size = new System.Drawing.Size(53, 20);
            this.KYOTEN_CD.TabIndex = 1;
            this.KYOTEN_CD.TabStop = false;
            this.KYOTEN_CD.Tag = "";
            this.KYOTEN_CD.ZeroPaddengFlag = true;
            // 
            // lbl_Kyoten
            // 
            this.lbl_Kyoten.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Kyoten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Kyoten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Kyoten.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Kyoten.ForeColor = System.Drawing.Color.White;
            this.lbl_Kyoten.Location = new System.Drawing.Point(588, 6);
            this.lbl_Kyoten.Name = "lbl_Kyoten";
            this.lbl_Kyoten.Size = new System.Drawing.Size(62, 20);
            this.lbl_Kyoten.TabIndex = 530;
            this.lbl_Kyoten.Text = "拠点";
            this.lbl_Kyoten.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ctxt_ReadDataNumber
            // 
            this.ctxt_ReadDataNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_ReadDataNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_ReadDataNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_ReadDataNumber.DisplayPopUp = null;
            this.ctxt_ReadDataNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_ReadDataNumber.FocusOutCheckMethod")));
            this.ctxt_ReadDataNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ctxt_ReadDataNumber.ForeColor = System.Drawing.Color.Black;
            this.ctxt_ReadDataNumber.GetCodeMasterField = "";
            this.ctxt_ReadDataNumber.IsInputErrorOccured = false;
            this.ctxt_ReadDataNumber.Location = new System.Drawing.Point(1029, 28);
            this.ctxt_ReadDataNumber.Name = "ctxt_ReadDataNumber";
            this.ctxt_ReadDataNumber.PopupAfterExecute = null;
            this.ctxt_ReadDataNumber.PopupBeforeExecute = null;
            this.ctxt_ReadDataNumber.PopupGetMasterField = "";
            this.ctxt_ReadDataNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_ReadDataNumber.PopupSearchSendParams")));
            this.ctxt_ReadDataNumber.PopupSetFormField = "";
            this.ctxt_ReadDataNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_ReadDataNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_ReadDataNumber.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ctxt_ReadDataNumber.RangeSetting = rangeSettingDto1;
            this.ctxt_ReadDataNumber.ReadOnly = true;
            this.ctxt_ReadDataNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_ReadDataNumber.RegistCheckMethod")));
            this.ctxt_ReadDataNumber.SetFormField = "";
            this.ctxt_ReadDataNumber.Size = new System.Drawing.Size(105, 20);
            this.ctxt_ReadDataNumber.TabIndex = 4;
            this.ctxt_ReadDataNumber.TabStop = false;
            this.ctxt_ReadDataNumber.Tag = "";
            this.ctxt_ReadDataNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ctxt_ReadDataNumber.WordWrap = false;
            // 
            // ctxt_AlertNumber
            // 
            this.ctxt_AlertNumber.BackColor = System.Drawing.SystemColors.Window;
            this.ctxt_AlertNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_AlertNumber.CustomFormatSetting = "#,##0";
            this.ctxt_AlertNumber.DBFieldsName = "";
            this.ctxt_AlertNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_AlertNumber.DisplayItemName = "";
            this.ctxt_AlertNumber.DisplayPopUp = null;
            this.ctxt_AlertNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_AlertNumber.FocusOutCheckMethod")));
            this.ctxt_AlertNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ctxt_AlertNumber.ForeColor = System.Drawing.Color.Black;
            this.ctxt_AlertNumber.FormatSetting = "カスタム";
            this.ctxt_AlertNumber.IsInputErrorOccured = false;
            this.ctxt_AlertNumber.ItemDefinedTypes = "";
            this.ctxt_AlertNumber.Location = new System.Drawing.Point(1029, 6);
            this.ctxt_AlertNumber.Name = "ctxt_AlertNumber";
            this.ctxt_AlertNumber.PopupAfterExecute = null;
            this.ctxt_AlertNumber.PopupBeforeExecute = null;
            this.ctxt_AlertNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_AlertNumber.PopupSearchSendParams")));
            this.ctxt_AlertNumber.PopupSetFormField = "";
            this.ctxt_AlertNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_AlertNumber.PopupWindowName = "";
            this.ctxt_AlertNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_AlertNumber.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.ctxt_AlertNumber.RangeSetting = rangeSettingDto2;
            this.ctxt_AlertNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_AlertNumber.RegistCheckMethod")));
            this.ctxt_AlertNumber.SetFormField = "";
            this.ctxt_AlertNumber.Size = new System.Drawing.Size(105, 20);
            this.ctxt_AlertNumber.TabIndex = 2;
            this.ctxt_AlertNumber.Tag = "検索結果の総件数でアラートメッセージを表示させたい上限数を入力してください";
            this.ctxt_AlertNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ctxt_AlertNumber.WordWrap = false;
            // 
            // KensyuuIchiranHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 62);
            this.Controls.Add(this.ctxt_AlertNumber);
            this.Controls.Add(this.ctxt_ReadDataNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.KYOTEN_NAME_RYAKU);
            this.Controls.Add(this.KYOTEN_CD);
            this.Controls.Add(this.lbl_Kyoten);
            this.Name = "KensyuuIchiranHeader";
            this.Text = "HeaderForm";
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.lbl_Kyoten, 0);
            this.Controls.SetChildIndex(this.KYOTEN_CD, 0);
            this.Controls.SetChildIndex(this.KYOTEN_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.ctxt_ReadDataNumber, 0);
            this.Controls.SetChildIndex(this.ctxt_AlertNumber, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public r_framework.CustomControl.CustomTextBox KYOTEN_NAME_RYAKU;
        public r_framework.CustomControl.CustomTextBox KYOTEN_CD;
        public System.Windows.Forms.Label lbl_Kyoten;
        public r_framework.CustomControl.CustomNumericTextBox2 ctxt_ReadDataNumber;
        internal r_framework.CustomControl.CustomNumericTextBox2 ctxt_AlertNumber;
    }
}