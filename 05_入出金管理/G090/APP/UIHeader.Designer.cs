namespace Shougun.Core.ReceiptPayManagement.Syukinnyuryoku
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
            this.txtKyotenCd = new r_framework.CustomControl.CustomNumericTextBox2();
            this.txtKyotenName = new r_framework.CustomControl.CustomTextBox();
            this.lblKyoten = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lb_title
            // 
            this.lb_title.Size = new System.Drawing.Size(400, 34);
            this.lb_title.Text = "出金入力(取引先)";
            // 
            // txtKyotenCd
            // 
            this.txtKyotenCd.BackColor = System.Drawing.SystemColors.Window;
            this.txtKyotenCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKyotenCd.CustomFormatSetting = "00";
            this.txtKyotenCd.DBFieldsName = "KYOTEN_CD";
            this.txtKyotenCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtKyotenCd.DisplayItemName = "拠点";
            this.txtKyotenCd.DisplayPopUp = null;
            this.txtKyotenCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenCd.FocusOutCheckMethod")));
            this.txtKyotenCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtKyotenCd.ForeColor = System.Drawing.Color.Black;
            this.txtKyotenCd.FormatSetting = "カスタム";
            this.txtKyotenCd.GetCodeMasterField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.txtKyotenCd.IsInputErrorOccured = false;
            this.txtKyotenCd.ItemDefinedTypes = "smallint";
            this.txtKyotenCd.Location = new System.Drawing.Point(715, 2);
            this.txtKyotenCd.Name = "txtKyotenCd";
            this.txtKyotenCd.PopupAfterExecute = null;
            this.txtKyotenCd.PopupBeforeExecute = null;
            this.txtKyotenCd.PopupGetMasterField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.txtKyotenCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtKyotenCd.PopupSearchSendParams")));
            this.txtKyotenCd.PopupSetFormField = "txtKyotenCd,txtKyotenName";
            this.txtKyotenCd.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.txtKyotenCd.PopupWindowName = "マスタ共通ポップアップ";
            this.txtKyotenCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtKyotenCd.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.txtKyotenCd.RangeSetting = rangeSettingDto1;
            this.txtKyotenCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenCd.RegistCheckMethod")));
            this.txtKyotenCd.SetFormField = "txtKyotenCd,txtKyotenName";
            this.txtKyotenCd.Size = new System.Drawing.Size(30, 20);
            this.txtKyotenCd.TabIndex = 0;
            this.txtKyotenCd.Tag = "拠点を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.txtKyotenCd.WordWrap = false;
            // 
            // txtKyotenName
            // 
            this.txtKyotenName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtKyotenName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKyotenName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txtKyotenName.DBFieldsName = "KYOTEN_NAME_RYAKU";
            this.txtKyotenName.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtKyotenName.DisplayPopUp = null;
            this.txtKyotenName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenName.FocusOutCheckMethod")));
            this.txtKyotenName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtKyotenName.ForeColor = System.Drawing.Color.Black;
            this.txtKyotenName.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtKyotenName.IsInputErrorOccured = false;
            this.txtKyotenName.ItemDefinedTypes = "varchar";
            this.txtKyotenName.Location = new System.Drawing.Point(744, 2);
            this.txtKyotenName.MaxLength = 0;
            this.txtKyotenName.Name = "txtKyotenName";
            this.txtKyotenName.PopupAfterExecute = null;
            this.txtKyotenName.PopupBeforeExecute = null;
            this.txtKyotenName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtKyotenName.PopupSearchSendParams")));
            this.txtKyotenName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtKyotenName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtKyotenName.popupWindowSetting")));
            this.txtKyotenName.ReadOnly = true;
            this.txtKyotenName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenName.RegistCheckMethod")));
            this.txtKyotenName.Size = new System.Drawing.Size(160, 20);
            this.txtKyotenName.TabIndex = 509;
            this.txtKyotenName.TabStop = false;
            this.txtKyotenName.Tag = "";
            // 
            // lblKyoten
            // 
            this.lblKyoten.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblKyoten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKyoten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblKyoten.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblKyoten.ForeColor = System.Drawing.Color.White;
            this.lblKyoten.Location = new System.Drawing.Point(600, 2);
            this.lblKyoten.Name = "lblKyoten";
            this.lblKyoten.Size = new System.Drawing.Size(110, 20);
            this.lblKyoten.TabIndex = 508;
            this.lblKyoten.Text = "拠点※";
            this.lblKyoten.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.txtKyotenCd);
            this.Controls.Add(this.txtKyotenName);
            this.Controls.Add(this.lblKyoten);
            this.Name = "UIHeader";
            this.Text = "HeaderSample";
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.lblKyoten, 0);
            this.Controls.SetChildIndex(this.txtKyotenName, 0);
            this.Controls.SetChildIndex(this.txtKyotenCd, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomNumericTextBox2 txtKyotenCd;
        public r_framework.CustomControl.CustomTextBox txtKyotenName;
        public System.Windows.Forms.Label lblKyoten;


    }
}