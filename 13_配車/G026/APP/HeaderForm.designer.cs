namespace Shougun.Core.Allocation.HaishaWariateDay
{
    partial class HeaderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HeaderForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.KYOTEN_NAME = new r_framework.CustomControl.CustomTextBox();
            this.KYOTEN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.lblKyoten = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbTotalCount = new r_framework.CustomControl.CustomTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMihaishaCount = new r_framework.CustomControl.CustomTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbHaishaCount = new r_framework.CustomControl.CustomTextBox();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.TabIndex = 0;
            // 
            // lb_title
            // 
            this.lb_title.Size = new System.Drawing.Size(259, 34);
            this.lb_title.TabIndex = 1;
            // 
            // KYOTEN_NAME
            // 
            this.KYOTEN_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KYOTEN_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_NAME.DisplayPopUp = null;
            this.KYOTEN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME.FocusOutCheckMethod")));
            this.KYOTEN_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_NAME.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_NAME.IsInputErrorOccured = false;
            this.KYOTEN_NAME.Location = new System.Drawing.Point(478, 14);
            this.KYOTEN_NAME.Name = "KYOTEN_NAME";
            this.KYOTEN_NAME.PopupAfterExecute = null;
            this.KYOTEN_NAME.PopupBeforeExecute = null;
            this.KYOTEN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_NAME.PopupSearchSendParams")));
            this.KYOTEN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOTEN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_NAME.popupWindowSetting")));
            this.KYOTEN_NAME.prevText = null;
            this.KYOTEN_NAME.PrevText = null;
            this.KYOTEN_NAME.ReadOnly = true;
            this.KYOTEN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME.RegistCheckMethod")));
            this.KYOTEN_NAME.Size = new System.Drawing.Size(141, 20);
            this.KYOTEN_NAME.TabIndex = 4;
            this.KYOTEN_NAME.TabStop = false;
            this.KYOTEN_NAME.Tag = "";
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
            this.KYOTEN_CD.GetCodeMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.IsInputErrorOccured = false;
            this.KYOTEN_CD.ItemDefinedTypes = "varchar";
            this.KYOTEN_CD.LinkedRadioButtonArray = new string[0];
            this.KYOTEN_CD.Location = new System.Drawing.Point(429, 14);
            this.KYOTEN_CD.Name = "KYOTEN_CD";
            this.KYOTEN_CD.PopupAfterExecute = null;
            this.KYOTEN_CD.PopupBeforeExecute = null;
            this.KYOTEN_CD.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_CD.PopupSearchSendParams")));
            this.KYOTEN_CD.PopupSendParams = new string[0];
            this.KYOTEN_CD.PopupSetFormField = "KYOTEN_CD,KYOTEN_NAME";
            this.KYOTEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.KYOTEN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.KYOTEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_CD.popupWindowSetting")));
            this.KYOTEN_CD.prevText = "";
            this.KYOTEN_CD.PrevText = "";
            rangeSettingDto1.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.KYOTEN_CD.RangeSetting = rangeSettingDto1;
            this.KYOTEN_CD.ReadOnly = true;
            this.KYOTEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.RegistCheckMethod")));
            this.KYOTEN_CD.SetFormField = "KYOTEN_CD,KYOTEN_NAME";
            this.KYOTEN_CD.Size = new System.Drawing.Size(50, 20);
            this.KYOTEN_CD.TabIndex = 3;
            this.KYOTEN_CD.Tag = "拠点を指定してください";
            this.KYOTEN_CD.WordWrap = false;
            // 
            // lblKyoten
            // 
            this.lblKyoten.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblKyoten.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblKyoten.ForeColor = System.Drawing.Color.White;
            this.lblKyoten.Location = new System.Drawing.Point(351, 14);
            this.lblKyoten.Name = "lblKyoten";
            this.lblKyoten.Size = new System.Drawing.Size(76, 20);
            this.lblKyoten.TabIndex = 2;
            this.lblKyoten.Text = "拠点※";
            this.lblKyoten.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(783, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "件";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(627, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "受注件数";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbTotalCount
            // 
            this.tbTotalCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.tbTotalCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbTotalCount.CharactersNumber = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.tbTotalCount.DBFieldsName = "";
            this.tbTotalCount.DefaultBackColor = System.Drawing.Color.Empty;
            this.tbTotalCount.DisplayItemName = "";
            this.tbTotalCount.DisplayPopUp = null;
            this.tbTotalCount.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("tbTotalCount.FocusOutCheckMethod")));
            this.tbTotalCount.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.tbTotalCount.ForeColor = System.Drawing.Color.Black;
            this.tbTotalCount.GetCodeMasterField = "";
            this.tbTotalCount.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.tbTotalCount.IsInputErrorOccured = false;
            this.tbTotalCount.ItemDefinedTypes = "varchar";
            this.tbTotalCount.Location = new System.Drawing.Point(719, 14);
            this.tbTotalCount.Name = "tbTotalCount";
            this.tbTotalCount.PopupAfterExecute = null;
            this.tbTotalCount.PopupBeforeExecute = null;
            this.tbTotalCount.PopupGetMasterField = "";
            this.tbTotalCount.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("tbTotalCount.PopupSearchSendParams")));
            this.tbTotalCount.PopupSendParams = new string[0];
            this.tbTotalCount.PopupSetFormField = "";
            this.tbTotalCount.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.tbTotalCount.PopupWindowName = "";
            this.tbTotalCount.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("tbTotalCount.popupWindowSetting")));
            this.tbTotalCount.prevText = null;
            this.tbTotalCount.PrevText = null;
            this.tbTotalCount.ReadOnly = true;
            this.tbTotalCount.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("tbTotalCount.RegistCheckMethod")));
            this.tbTotalCount.SetFormField = "";
            this.tbTotalCount.Size = new System.Drawing.Size(60, 20);
            this.tbTotalCount.TabIndex = 9;
            this.tbTotalCount.TabStop = false;
            this.tbTotalCount.Tag = "無反応検出間隔を入力してください";
            this.tbTotalCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(963, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "件";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(808, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "未配車件数";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbMihaishaCount
            // 
            this.tbMihaishaCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.tbMihaishaCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbMihaishaCount.CharactersNumber = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.tbMihaishaCount.DBFieldsName = "";
            this.tbMihaishaCount.DefaultBackColor = System.Drawing.Color.Empty;
            this.tbMihaishaCount.DisplayItemName = "";
            this.tbMihaishaCount.DisplayPopUp = null;
            this.tbMihaishaCount.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("tbMihaishaCount.FocusOutCheckMethod")));
            this.tbMihaishaCount.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.tbMihaishaCount.ForeColor = System.Drawing.Color.Black;
            this.tbMihaishaCount.GetCodeMasterField = "";
            this.tbMihaishaCount.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.tbMihaishaCount.IsInputErrorOccured = false;
            this.tbMihaishaCount.ItemDefinedTypes = "varchar";
            this.tbMihaishaCount.Location = new System.Drawing.Point(900, 14);
            this.tbMihaishaCount.Name = "tbMihaishaCount";
            this.tbMihaishaCount.PopupAfterExecute = null;
            this.tbMihaishaCount.PopupBeforeExecute = null;
            this.tbMihaishaCount.PopupGetMasterField = "";
            this.tbMihaishaCount.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("tbMihaishaCount.PopupSearchSendParams")));
            this.tbMihaishaCount.PopupSendParams = new string[0];
            this.tbMihaishaCount.PopupSetFormField = "";
            this.tbMihaishaCount.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.tbMihaishaCount.PopupWindowName = "";
            this.tbMihaishaCount.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("tbMihaishaCount.popupWindowSetting")));
            this.tbMihaishaCount.prevText = null;
            this.tbMihaishaCount.PrevText = null;
            this.tbMihaishaCount.ReadOnly = true;
            this.tbMihaishaCount.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("tbMihaishaCount.RegistCheckMethod")));
            this.tbMihaishaCount.SetFormField = "";
            this.tbMihaishaCount.Size = new System.Drawing.Size(60, 20);
            this.tbMihaishaCount.TabIndex = 12;
            this.tbMihaishaCount.TabStop = false;
            this.tbMihaishaCount.Tag = "無反応検出間隔を入力してください";
            this.tbMihaishaCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1142, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "件";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(989, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "配車済件数";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbHaishaCount
            // 
            this.tbHaishaCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.tbHaishaCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbHaishaCount.DBFieldsName = "";
            this.tbHaishaCount.DefaultBackColor = System.Drawing.Color.Empty;
            this.tbHaishaCount.DisplayItemName = "";
            this.tbHaishaCount.DisplayPopUp = null;
            this.tbHaishaCount.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("tbHaishaCount.FocusOutCheckMethod")));
            this.tbHaishaCount.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.tbHaishaCount.ForeColor = System.Drawing.Color.Black;
            this.tbHaishaCount.GetCodeMasterField = "";
            this.tbHaishaCount.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.tbHaishaCount.IsInputErrorOccured = false;
            this.tbHaishaCount.ItemDefinedTypes = "varchar";
            this.tbHaishaCount.Location = new System.Drawing.Point(1081, 14);
            this.tbHaishaCount.Name = "tbHaishaCount";
            this.tbHaishaCount.PopupAfterExecute = null;
            this.tbHaishaCount.PopupBeforeExecute = null;
            this.tbHaishaCount.PopupGetMasterField = "";
            this.tbHaishaCount.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("tbHaishaCount.PopupSearchSendParams")));
            this.tbHaishaCount.PopupSendParams = new string[0];
            this.tbHaishaCount.PopupSetFormField = "";
            this.tbHaishaCount.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.tbHaishaCount.PopupWindowName = "";
            this.tbHaishaCount.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("tbHaishaCount.popupWindowSetting")));
            this.tbHaishaCount.prevText = null;
            this.tbHaishaCount.PrevText = null;
            this.tbHaishaCount.ReadOnly = true;
            this.tbHaishaCount.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("tbHaishaCount.RegistCheckMethod")));
            this.tbHaishaCount.SetFormField = "";
            this.tbHaishaCount.Size = new System.Drawing.Size(60, 20);
            this.tbHaishaCount.TabIndex = 15;
            this.tbHaishaCount.TabStop = false;
            this.tbHaishaCount.Tag = "無反応検出間隔を入力してください";
            this.tbHaishaCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // HeaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbHaishaCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbMihaishaCount);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbTotalCount);
            this.Controls.Add(this.KYOTEN_NAME);
            this.Controls.Add(this.KYOTEN_CD);
            this.Controls.Add(this.lblKyoten);
            this.Name = "HeaderForm";
            this.Controls.SetChildIndex(this.lblKyoten, 0);
            this.Controls.SetChildIndex(this.KYOTEN_CD, 0);
            this.Controls.SetChildIndex(this.KYOTEN_NAME, 0);
            this.Controls.SetChildIndex(this.tbTotalCount, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.tbMihaishaCount, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.tbHaishaCount, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomTextBox KYOTEN_NAME;
        public r_framework.CustomControl.CustomNumericTextBox2 KYOTEN_CD;
        public System.Windows.Forms.Label lblKyoten;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label label3;
        public r_framework.CustomControl.CustomTextBox tbTotalCount;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label4;
        public r_framework.CustomControl.CustomTextBox tbMihaishaCount;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label7;
        public r_framework.CustomControl.CustomTextBox tbHaishaCount;
    }
}
