namespace Shougun.Core.Common.DenpyouRenkeiIchiran
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
            this.KYOTEN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.KYOTEN_NAME = new r_framework.CustomControl.CustomTextBox();
            this.lblKyoten = new System.Windows.Forms.Label();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(0, 6);
            this.lb_title.Size = new System.Drawing.Size(267, 35);
            this.lb_title.Text = "伝票連携状況一覧";
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
            this.KYOTEN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_CD.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_CD.FormatSetting = "カスタム";
            this.KYOTEN_CD.GetCodeMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.IsInputErrorOccured = false;
            this.KYOTEN_CD.ItemDefinedTypes = "smallint";
            this.KYOTEN_CD.Location = new System.Drawing.Point(388, 21);
            this.KYOTEN_CD.Name = "KYOTEN_CD";
            this.KYOTEN_CD.PopupAfterExecute = null;
            this.KYOTEN_CD.PopupBeforeExecute = null;
            this.KYOTEN_CD.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_CD.PopupSearchSendParams")));
            this.KYOTEN_CD.PopupSetFormField = "KYOTEN_CD,KYOTEN_NAME";
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
            this.KYOTEN_CD.SetFormField = "KYOTEN_CD,KYOTEN_NAME";
            this.KYOTEN_CD.Size = new System.Drawing.Size(30, 20);
            this.KYOTEN_CD.TabIndex = 0;
            this.KYOTEN_CD.TabStop = false;
            this.KYOTEN_CD.Tag = "拠点を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.KYOTEN_CD.WordWrap = false;
            // 
            // KYOTEN_NAME
            // 
            this.KYOTEN_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KYOTEN_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_NAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.KYOTEN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_NAME.DisplayPopUp = null;
            this.KYOTEN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME.FocusOutCheckMethod")));
            this.KYOTEN_NAME.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_NAME.IsInputErrorOccured = false;
            this.KYOTEN_NAME.Location = new System.Drawing.Point(417, 21);
            this.KYOTEN_NAME.MaxLength = 0;
            this.KYOTEN_NAME.Name = "KYOTEN_NAME";
            this.KYOTEN_NAME.PopupAfterExecute = null;
            this.KYOTEN_NAME.PopupBeforeExecute = null;
            this.KYOTEN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_NAME.PopupSearchSendParams")));
            this.KYOTEN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOTEN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_NAME.popupWindowSetting")));
            this.KYOTEN_NAME.ReadOnly = true;
            this.KYOTEN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME.RegistCheckMethod")));
            this.KYOTEN_NAME.Size = new System.Drawing.Size(160, 20);
            this.KYOTEN_NAME.TabIndex = 508;
            this.KYOTEN_NAME.TabStop = false;
            this.KYOTEN_NAME.Tag = "";
            // 
            // lblKyoten
            // 
            this.lblKyoten.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblKyoten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKyoten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblKyoten.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblKyoten.ForeColor = System.Drawing.Color.White;
            this.lblKyoten.Location = new System.Drawing.Point(273, 21);
            this.lblKyoten.Name = "lblKyoten";
            this.lblKyoten.Size = new System.Drawing.Size(110, 20);
            this.lblKyoten.TabIndex = 507;
            this.lblKyoten.Text = "拠点";
            this.lblKyoten.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.ISNOT_NEED_DELETE_FLG.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ISNOT_NEED_DELETE_FLG.ForeColor = System.Drawing.Color.Black;
            this.ISNOT_NEED_DELETE_FLG.GetCodeMasterField = "";
            this.ISNOT_NEED_DELETE_FLG.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ISNOT_NEED_DELETE_FLG.IsInputErrorOccured = false;
            this.ISNOT_NEED_DELETE_FLG.ItemDefinedTypes = "bit";
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(583, 20);
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
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 10018;
            this.ISNOT_NEED_DELETE_FLG.TabStop = false;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "True";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            this.ISNOT_NEED_DELETE_FLG.ZeroPaddengFlag = true;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.KYOTEN_CD);
            this.Controls.Add(this.KYOTEN_NAME);
            this.Controls.Add(this.lblKyoten);
            this.Name = "UIHeader";
            this.Text = "伝票連携状況一覧";
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.lblKyoten, 0);
            this.Controls.SetChildIndex(this.KYOTEN_NAME, 0);
            this.Controls.SetChildIndex(this.KYOTEN_CD, 0);
            this.Controls.SetChildIndex(this.ISNOT_NEED_DELETE_FLG, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomNumericTextBox2 KYOTEN_CD;
        public r_framework.CustomControl.CustomTextBox KYOTEN_NAME;
        public System.Windows.Forms.Label lblKyoten;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
    }
}