namespace Shougun.Core.Adjustment.Shiharaiichiran
{
    /// <summary>
    /// ヘッダクラス
    /// </summary>
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
            this.txtKyotenNameRyaku = new r_framework.CustomControl.CustomTextBox();
            this.lblKyoten = new System.Windows.Forms.Label();
            this.lblAlertNum = new System.Windows.Forms.Label();
            this.lblYomikomiDataNum = new System.Windows.Forms.Label();
            this.txtReadDataCnt = new r_framework.CustomControl.CustomTextBox();
            this.txtAlertNum = new r_framework.CustomControl.CustomNumericTextBox2();
            this.txtKyotenCd = new r_framework.CustomControl.CustomNumericTextBox2();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Location = new System.Drawing.Point(8, 6);
            this.windowTypeLabel.Size = new System.Drawing.Size(19, 17);
            this.windowTypeLabel.Visible = false;
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(8, 6);
            this.lb_title.Size = new System.Drawing.Size(310, 35);
            this.lb_title.Text = "支払明細一覧";
            // 
            // txtKyotenNameRyaku
            // 
            this.txtKyotenNameRyaku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtKyotenNameRyaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKyotenNameRyaku.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txtKyotenNameRyaku.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtKyotenNameRyaku.DisplayPopUp = null;
            this.txtKyotenNameRyaku.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenNameRyaku.FocusOutCheckMethod")));
            this.txtKyotenNameRyaku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtKyotenNameRyaku.ForeColor = System.Drawing.Color.Black;
            this.txtKyotenNameRyaku.IsInputErrorOccured = false;
            this.txtKyotenNameRyaku.Location = new System.Drawing.Point(805, 2);
            this.txtKyotenNameRyaku.MaxLength = 0;
            this.txtKyotenNameRyaku.Name = "txtKyotenNameRyaku";
            this.txtKyotenNameRyaku.PopupAfterExecute = null;
            this.txtKyotenNameRyaku.PopupBeforeExecute = null;
            this.txtKyotenNameRyaku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtKyotenNameRyaku.PopupSearchSendParams")));
            this.txtKyotenNameRyaku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtKyotenNameRyaku.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtKyotenNameRyaku.popupWindowSetting")));
            this.txtKyotenNameRyaku.ReadOnly = true;
            this.txtKyotenNameRyaku.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenNameRyaku.RegistCheckMethod")));
            this.txtKyotenNameRyaku.Size = new System.Drawing.Size(160, 20);
            this.txtKyotenNameRyaku.TabIndex = 534;
            this.txtKyotenNameRyaku.TabStop = false;
            this.txtKyotenNameRyaku.Tag = "  ";
            // 
            // lblKyoten
            // 
            this.lblKyoten.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblKyoten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKyoten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblKyoten.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblKyoten.ForeColor = System.Drawing.Color.White;
            this.lblKyoten.Location = new System.Drawing.Point(661, 2);
            this.lblKyoten.Name = "lblKyoten";
            this.lblKyoten.Size = new System.Drawing.Size(110, 20);
            this.lblKyoten.TabIndex = 532;
            this.lblKyoten.Text = "拠点";
            this.lblKyoten.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAlertNum
            // 
            this.lblAlertNum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblAlertNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAlertNum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblAlertNum.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblAlertNum.ForeColor = System.Drawing.Color.White;
            this.lblAlertNum.Location = new System.Drawing.Point(973, 2);
            this.lblAlertNum.Name = "lblAlertNum";
            this.lblAlertNum.Size = new System.Drawing.Size(110, 20);
            this.lblAlertNum.TabIndex = 530;
            this.lblAlertNum.Text = "アラート件数";
            this.lblAlertNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblAlertNum.Visible = false;
            // 
            // lblYomikomiDataNum
            // 
            this.lblYomikomiDataNum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblYomikomiDataNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblYomikomiDataNum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblYomikomiDataNum.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblYomikomiDataNum.ForeColor = System.Drawing.Color.White;
            this.lblYomikomiDataNum.Location = new System.Drawing.Point(973, 24);
            this.lblYomikomiDataNum.Name = "lblYomikomiDataNum";
            this.lblYomikomiDataNum.Size = new System.Drawing.Size(110, 20);
            this.lblYomikomiDataNum.TabIndex = 528;
            this.lblYomikomiDataNum.Text = "読込データ件数";
            this.lblYomikomiDataNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtReadDataCnt
            // 
            this.txtReadDataCnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtReadDataCnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReadDataCnt.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtReadDataCnt.DisplayPopUp = null;
            this.txtReadDataCnt.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtReadDataCnt.FocusOutCheckMethod")));
            this.txtReadDataCnt.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtReadDataCnt.ForeColor = System.Drawing.Color.Black;
            this.txtReadDataCnt.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtReadDataCnt.IsInputErrorOccured = false;
            this.txtReadDataCnt.Location = new System.Drawing.Point(1088, 24);
            this.txtReadDataCnt.Name = "txtReadDataCnt";
            this.txtReadDataCnt.PopupAfterExecute = null;
            this.txtReadDataCnt.PopupBeforeExecute = null;
            this.txtReadDataCnt.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtReadDataCnt.PopupSearchSendParams")));
            this.txtReadDataCnt.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtReadDataCnt.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtReadDataCnt.popupWindowSetting")));
            this.txtReadDataCnt.ReadOnly = true;
            this.txtReadDataCnt.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtReadDataCnt.RegistCheckMethod")));
            this.txtReadDataCnt.Size = new System.Drawing.Size(80, 20);
            this.txtReadDataCnt.TabIndex = 410;
            this.txtReadDataCnt.TabStop = false;
            this.txtReadDataCnt.Tag = "検索結果の総件数が表示されます";
            this.txtReadDataCnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtAlertNum
            // 
            this.txtAlertNum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtAlertNum.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAlertNum.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtAlertNum.DisplayPopUp = null;
            this.txtAlertNum.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtAlertNum.FocusOutCheckMethod")));
            this.txtAlertNum.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtAlertNum.ForeColor = System.Drawing.Color.Black;
            this.txtAlertNum.IsInputErrorOccured = false;
            this.txtAlertNum.Location = new System.Drawing.Point(1088, 2);
            this.txtAlertNum.Name = "txtAlertNum";
            this.txtAlertNum.PopupAfterExecute = null;
            this.txtAlertNum.PopupBeforeExecute = null;
            this.txtAlertNum.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtAlertNum.PopupSearchSendParams")));
            this.txtAlertNum.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtAlertNum.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtAlertNum.popupWindowSetting")));
            this.txtAlertNum.RangeSetting = rangeSettingDto1;
            this.txtAlertNum.ReadOnly = true;
            this.txtAlertNum.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtAlertNum.RegistCheckMethod")));
            this.txtAlertNum.Size = new System.Drawing.Size(80, 20);
            this.txtAlertNum.TabIndex = 539;
            this.txtAlertNum.TabStop = false;
            this.txtAlertNum.Tag = "検索結果の総件数でアラートメッセージを表示させたい上限数を入力してください";
            this.txtAlertNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAlertNum.Visible = false;
            this.txtAlertNum.WordWrap = false;
            // 
            // txtKyotenCd
            // 
            this.txtKyotenCd.BackColor = System.Drawing.SystemColors.Window;
            this.txtKyotenCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKyotenCd.CustomFormatSetting = "00";
            this.txtKyotenCd.DBFieldsName = "KYOTEN_CD";
            this.txtKyotenCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtKyotenCd.DisplayItemName = "拠点CD";
            this.txtKyotenCd.DisplayPopUp = null;
            this.txtKyotenCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenCd.FocusOutCheckMethod")));
            this.txtKyotenCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtKyotenCd.ForeColor = System.Drawing.Color.Black;
            this.txtKyotenCd.FormatSetting = "カスタム";
            this.txtKyotenCd.GetCodeMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.txtKyotenCd.IsInputErrorOccured = false;
            this.txtKyotenCd.ItemDefinedTypes = "smallint";
            this.txtKyotenCd.Location = new System.Drawing.Point(776, 2);
            this.txtKyotenCd.Name = "txtKyotenCd";
            this.txtKyotenCd.PopupAfterExecute = null;
            this.txtKyotenCd.PopupBeforeExecute = null;
            this.txtKyotenCd.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.txtKyotenCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtKyotenCd.PopupSearchSendParams")));
            this.txtKyotenCd.PopupSetFormField = "txtKyotenCd,txtKyotenNameRyaku";
            this.txtKyotenCd.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.txtKyotenCd.PopupWindowName = "マスタ共通ポップアップ";
            this.txtKyotenCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtKyotenCd.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.txtKyotenCd.RangeSetting = rangeSettingDto2;
            this.txtKyotenCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenCd.RegistCheckMethod")));
            this.txtKyotenCd.SetFormField = "txtKyotenCd,txtKyotenNameRyaku";
            this.txtKyotenCd.ShortItemName = "拠点CD";
            this.txtKyotenCd.Size = new System.Drawing.Size(30, 20);
            this.txtKyotenCd.TabIndex = 1;
            this.txtKyotenCd.TabStop = false;
            this.txtKyotenCd.Tag = "拠点を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.txtKyotenCd.WordWrap = false;
            // 
            // ISNOT_NEED_DELETE_FLG
            // 
            this.ISNOT_NEED_DELETE_FLG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ISNOT_NEED_DELETE_FLG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ISNOT_NEED_DELETE_FLG.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.ISNOT_NEED_DELETE_FLG.DBFieldsName = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.ISNOT_NEED_DELETE_FLG.DisplayPopUp = null;
            this.ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ISNOT_NEED_DELETE_FLG.ForeColor = System.Drawing.Color.Black;
            this.ISNOT_NEED_DELETE_FLG.IsInputErrorOccured = false;
            this.ISNOT_NEED_DELETE_FLG.ItemDefinedTypes = "bit";
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(567, 13);
            this.ISNOT_NEED_DELETE_FLG.MaxLength = 20;
            this.ISNOT_NEED_DELETE_FLG.Name = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.PopupAfterExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupBeforeExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.PopupSearchSendParams")));
            this.ISNOT_NEED_DELETE_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ISNOT_NEED_DELETE_FLG.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.popupWindowSetting")));
            this.ISNOT_NEED_DELETE_FLG.ReadOnly = true;
            this.ISNOT_NEED_DELETE_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.RegistCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.Size = new System.Drawing.Size(40, 20);
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 675;
            this.ISNOT_NEED_DELETE_FLG.TabStop = false;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.ClientSize = new System.Drawing.Size(1174, 46);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.txtKyotenCd);
            this.Controls.Add(this.txtAlertNum);
            this.Controls.Add(this.txtReadDataCnt);
            this.Controls.Add(this.txtKyotenNameRyaku);
            this.Controls.Add(this.lblKyoten);
            this.Controls.Add(this.lblAlertNum);
            this.Controls.Add(this.lblYomikomiDataNum);
            this.Name = "UIHeader";
            this.Text = "HeaderSample";
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.lblYomikomiDataNum, 0);
            this.Controls.SetChildIndex(this.lblAlertNum, 0);
            this.Controls.SetChildIndex(this.lblKyoten, 0);
            this.Controls.SetChildIndex(this.txtKyotenNameRyaku, 0);
            this.Controls.SetChildIndex(this.txtReadDataCnt, 0);
            this.Controls.SetChildIndex(this.txtAlertNum, 0);
            this.Controls.SetChildIndex(this.txtKyotenCd, 0);
            this.Controls.SetChildIndex(this.ISNOT_NEED_DELETE_FLG, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomTextBox txtKyotenNameRyaku;
        public System.Windows.Forms.Label lblKyoten;
        private System.Windows.Forms.Label lblAlertNum;
        private System.Windows.Forms.Label lblYomikomiDataNum;
        public r_framework.CustomControl.CustomTextBox txtReadDataCnt;
        public r_framework.CustomControl.CustomNumericTextBox2 txtAlertNum;
        public r_framework.CustomControl.CustomNumericTextBox2 txtKyotenCd;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;

    }
}