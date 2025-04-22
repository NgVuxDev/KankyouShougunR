namespace Shougun.Core.SalesPayment.Tairyuichiran
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
            this.txtKyotenCD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.txtKyotenNameRyaku = new r_framework.CustomControl.CustomTextBox();
            this.lblKyoten = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
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
            this.lb_title.Size = new System.Drawing.Size(220, 35);
            this.lb_title.Text = "滞留一覧";
            // 
            // txtKyotenCD
            // 
            this.txtKyotenCD.BackColor = System.Drawing.SystemColors.Window;
            this.txtKyotenCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKyotenCD.CustomFormatSetting = "00";
            this.txtKyotenCD.DBFieldsName = "KYOTEN_CD";
            this.txtKyotenCD.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtKyotenCD.DisplayItemName = "拠点CD";
            this.txtKyotenCD.DisplayPopUp = null;
            this.txtKyotenCD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenCD.FocusOutCheckMethod")));
            this.txtKyotenCD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtKyotenCD.ForeColor = System.Drawing.Color.Black;
            this.txtKyotenCD.FormatSetting = "カスタム";
            this.txtKyotenCD.GetCodeMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.txtKyotenCD.IsInputErrorOccured = false;
            this.txtKyotenCD.ItemDefinedTypes = "smallint";
            this.txtKyotenCD.Location = new System.Drawing.Point(979, 2);
            this.txtKyotenCD.Name = "txtKyotenCD";
            this.txtKyotenCD.PopupAfterExecute = null;
            this.txtKyotenCD.PopupBeforeExecute = null;
            this.txtKyotenCD.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.txtKyotenCD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtKyotenCD.PopupSearchSendParams")));
            this.txtKyotenCD.PopupSetFormField = "txtKyotenCD,txtKyotenNameRyaku";
            this.txtKyotenCD.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.txtKyotenCD.PopupWindowName = "マスタ共通ポップアップ";
            this.txtKyotenCD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtKyotenCD.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.txtKyotenCD.RangeSetting = rangeSettingDto1;
            this.txtKyotenCD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenCD.RegistCheckMethod")));
            this.txtKyotenCD.SetFormField = "txtKyotenCD,txtKyotenNameRyaku";
            this.txtKyotenCD.Size = new System.Drawing.Size(30, 20);
            this.txtKyotenCD.TabIndex = 511;
            this.txtKyotenCD.TabStop = false;
            this.txtKyotenCD.Tag = "拠点を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.txtKyotenCD.WordWrap = false;
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
            this.txtKyotenNameRyaku.ForeColor = System.Drawing.Color.Black;
            this.txtKyotenNameRyaku.IsInputErrorOccured = false;
            this.txtKyotenNameRyaku.Location = new System.Drawing.Point(1008, 2);
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
            this.txtKyotenNameRyaku.TabIndex = 508;
            this.txtKyotenNameRyaku.TabStop = false;
            this.txtKyotenNameRyaku.Tag = "";
            // 
            // lblKyoten
            // 
            this.lblKyoten.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblKyoten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKyoten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblKyoten.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblKyoten.ForeColor = System.Drawing.Color.White;
            this.lblKyoten.Location = new System.Drawing.Point(864, 2);
            this.lblKyoten.Name = "lblKyoten";
            this.lblKyoten.Size = new System.Drawing.Size(110, 20);
            this.lblKyoten.TabIndex = 507;
            this.lblKyoten.Text = "拠点";
            this.lblKyoten.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 20.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Aqua;
            this.label1.Location = new System.Drawing.Point(654, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 35);
            this.label1.TabIndex = 513;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(570, 13);
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
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 678;
            this.ISNOT_NEED_DELETE_FLG.TabStop = false;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtKyotenCD);
            this.Controls.Add(this.txtKyotenNameRyaku);
            this.Controls.Add(this.lblKyoten);
            this.Name = "UIHeader";
            this.Text = "HeaderSample";
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.lblKyoten, 0);
            this.Controls.SetChildIndex(this.txtKyotenNameRyaku, 0);
            this.Controls.SetChildIndex(this.txtKyotenCD, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.ISNOT_NEED_DELETE_FLG, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomNumericTextBox2 txtKyotenCD;
        public r_framework.CustomControl.CustomTextBox txtKyotenNameRyaku;
        public System.Windows.Forms.Label lblKyoten;
        public System.Windows.Forms.Label label1;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;


    }
}