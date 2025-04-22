namespace Shougun.Core.PaperManifest.HenkyakuIchiran
{
    partial class HeaderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HeaderForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.label1 = new System.Windows.Forms.Label();
            this.KYOTEN_NAME = new r_framework.CustomControl.CustomTextBox();
            this.KYOTEN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label2 = new System.Windows.Forms.Label();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.customPanel6 = new r_framework.CustomControl.CustomPanel();
            this.radbtn_Barcode_Off = new r_framework.CustomControl.CustomRadioButton();
            this.radbtn_Barcode_On = new r_framework.CustomControl.CustomRadioButton();
            this.txtNum_BarcodeKubun = new r_framework.CustomControl.CustomNumericTextBox2();
            this.customPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Location = new System.Drawing.Point(2, 2);
            this.windowTypeLabel.Visible = false;
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(0, 2);
            this.lb_title.Size = new System.Drawing.Size(260, 31);
            this.lb_title.Text = "１２返却日一覧９０";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(279, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 20);
            this.label1.TabIndex = 991;
            this.label1.Text = "バーコード区分";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KYOTEN_NAME
            // 
            this.KYOTEN_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KYOTEN_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_NAME.DBFieldsName = "KYOTEN_NAME";
            this.KYOTEN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_NAME.DisplayPopUp = null;
            this.KYOTEN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME.FocusOutCheckMethod")));
            this.KYOTEN_NAME.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.KYOTEN_NAME.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_NAME.IsInputErrorOccured = false;
            this.KYOTEN_NAME.Location = new System.Drawing.Point(737, 11);
            this.KYOTEN_NAME.Name = "KYOTEN_NAME";
            this.KYOTEN_NAME.PopupAfterExecute = null;
            this.KYOTEN_NAME.PopupBeforeExecute = null;
            this.KYOTEN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_NAME.PopupSearchSendParams")));
            this.KYOTEN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOTEN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_NAME.popupWindowSetting")));
            this.KYOTEN_NAME.ReadOnly = true;
            this.KYOTEN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME.RegistCheckMethod")));
            this.KYOTEN_NAME.Size = new System.Drawing.Size(160, 20);
            this.KYOTEN_NAME.TabIndex = 10015;
            this.KYOTEN_NAME.TabStop = false;
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
            this.KYOTEN_CD.ItemDefinedTypes = "varchar";
            this.KYOTEN_CD.Location = new System.Drawing.Point(708, 11);
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
            this.KYOTEN_CD.ShortItemName = "拠点CD";
            this.KYOTEN_CD.Size = new System.Drawing.Size(30, 20);
            this.KYOTEN_CD.TabIndex = 1;
            this.KYOTEN_CD.Tag = "半角2桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.KYOTEN_CD.WordWrap = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(593, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 10014;
            this.label2.Text = "拠点※";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ISNOT_NEED_DELETE_FLG
            // 
            this.ISNOT_NEED_DELETE_FLG.BackColor = System.Drawing.SystemColors.Window;
            this.ISNOT_NEED_DELETE_FLG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ISNOT_NEED_DELETE_FLG.DBFieldsName = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.ISNOT_NEED_DELETE_FLG.DisplayPopUp = null;
            this.ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod = null;
            this.ISNOT_NEED_DELETE_FLG.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ISNOT_NEED_DELETE_FLG.ForeColor = System.Drawing.Color.Black;
            this.ISNOT_NEED_DELETE_FLG.IsInputErrorOccured = false;
            this.ISNOT_NEED_DELETE_FLG.ItemDefinedTypes = "bit";
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(1016, 1);
            this.ISNOT_NEED_DELETE_FLG.Name = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.PopupAfterExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupBeforeExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.PopupSearchSendParams")));
            this.ISNOT_NEED_DELETE_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ISNOT_NEED_DELETE_FLG.popupWindowSetting = null;
            this.ISNOT_NEED_DELETE_FLG.RegistCheckMethod = null;
            this.ISNOT_NEED_DELETE_FLG.Size = new System.Drawing.Size(20, 20);
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 10016;
            this.ISNOT_NEED_DELETE_FLG.TabStop = false;
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // customPanel6
            // 
            this.customPanel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel6.Controls.Add(this.radbtn_Barcode_Off);
            this.customPanel6.Controls.Add(this.radbtn_Barcode_On);
            this.customPanel6.Controls.Add(this.txtNum_BarcodeKubun);
            this.customPanel6.Location = new System.Drawing.Point(410, 11);
            this.customPanel6.Name = "customPanel6";
            this.customPanel6.Size = new System.Drawing.Size(172, 20);
            this.customPanel6.TabIndex = 10017;
            // 
            // radbtn_Barcode_Off
            // 
            this.radbtn_Barcode_Off.AutoSize = true;
            this.radbtn_Barcode_Off.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Barcode_Off.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Barcode_Off.FocusOutCheckMethod")));
            this.radbtn_Barcode_Off.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.radbtn_Barcode_Off.LinkedTextBox = "txtNum_BarcodeKubun";
            this.radbtn_Barcode_Off.Location = new System.Drawing.Point(97, 0);
            this.radbtn_Barcode_Off.Name = "radbtn_Barcode_Off";
            this.radbtn_Barcode_Off.PopupAfterExecute = null;
            this.radbtn_Barcode_Off.PopupBeforeExecute = null;
            this.radbtn_Barcode_Off.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Barcode_Off.PopupSearchSendParams")));
            this.radbtn_Barcode_Off.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Barcode_Off.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Barcode_Off.popupWindowSetting")));
            this.radbtn_Barcode_Off.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Barcode_Off.RegistCheckMethod")));
            this.radbtn_Barcode_Off.Size = new System.Drawing.Size(67, 17);
            this.radbtn_Barcode_Off.TabIndex = 102;
            this.radbtn_Barcode_Off.Tag = " ";
            this.radbtn_Barcode_Off.Text = "2.オフ";
            this.radbtn_Barcode_Off.UseVisualStyleBackColor = true;
            this.radbtn_Barcode_Off.Value = "2";
            // 
            // radbtn_Barcode_On
            // 
            this.radbtn_Barcode_On.AutoSize = true;
            this.radbtn_Barcode_On.Checked = true;
            this.radbtn_Barcode_On.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Barcode_On.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Barcode_On.FocusOutCheckMethod")));
            this.radbtn_Barcode_On.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.radbtn_Barcode_On.LinkedTextBox = "txtNum_BarcodeKubun";
            this.radbtn_Barcode_On.Location = new System.Drawing.Point(23, 0);
            this.radbtn_Barcode_On.Name = "radbtn_Barcode_On";
            this.radbtn_Barcode_On.PopupAfterExecute = null;
            this.radbtn_Barcode_On.PopupBeforeExecute = null;
            this.radbtn_Barcode_On.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Barcode_On.PopupSearchSendParams")));
            this.radbtn_Barcode_On.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Barcode_On.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Barcode_On.popupWindowSetting")));
            this.radbtn_Barcode_On.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Barcode_On.RegistCheckMethod")));
            this.radbtn_Barcode_On.Size = new System.Drawing.Size(67, 17);
            this.radbtn_Barcode_On.TabIndex = 101;
            this.radbtn_Barcode_On.Tag = "checkManifestUse";
            this.radbtn_Barcode_On.Text = "1.オン";
            this.radbtn_Barcode_On.UseVisualStyleBackColor = true;
            this.radbtn_Barcode_On.Value = "1";
            // 
            // txtNum_BarcodeKubun
            // 
            this.txtNum_BarcodeKubun.BackColor = System.Drawing.SystemColors.Window;
            this.txtNum_BarcodeKubun.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNum_BarcodeKubun.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtNum_BarcodeKubun.DisplayItemName = "締処理状況";
            this.txtNum_BarcodeKubun.DisplayPopUp = null;
            this.txtNum_BarcodeKubun.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_BarcodeKubun.FocusOutCheckMethod")));
            this.txtNum_BarcodeKubun.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.txtNum_BarcodeKubun.ForeColor = System.Drawing.Color.Black;
            this.txtNum_BarcodeKubun.IsInputErrorOccured = false;
            this.txtNum_BarcodeKubun.LinkedRadioButtonArray = new string[] {
        "radbtn_Barcode_On",
        "radbtn_Barcode_Off"};
            this.txtNum_BarcodeKubun.Location = new System.Drawing.Point(-1, -1);
            this.txtNum_BarcodeKubun.Name = "txtNum_BarcodeKubun";
            this.txtNum_BarcodeKubun.PopupAfterExecute = null;
            this.txtNum_BarcodeKubun.PopupBeforeExecute = null;
            this.txtNum_BarcodeKubun.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtNum_BarcodeKubun.PopupSearchSendParams")));
            this.txtNum_BarcodeKubun.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtNum_BarcodeKubun.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtNum_BarcodeKubun.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            3,
            0,
            0,
            0});
            rangeSettingDto2.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtNum_BarcodeKubun.RangeSetting = rangeSettingDto2;
            this.txtNum_BarcodeKubun.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_BarcodeKubun.RegistCheckMethod")));
            this.txtNum_BarcodeKubun.ShortItemName = "";
            this.txtNum_BarcodeKubun.Size = new System.Drawing.Size(20, 20);
            this.txtNum_BarcodeKubun.TabIndex = 0;
            this.txtNum_BarcodeKubun.Tag = "【1～2】のいずれかで入力してください";
            this.txtNum_BarcodeKubun.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNum_BarcodeKubun.WordWrap = false;
            // 
            // HeaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.customPanel6);
            this.Controls.Add(this.KYOTEN_NAME);
            this.Controls.Add(this.KYOTEN_CD);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.label1);
            this.Name = "HeaderForm";
            this.Text = "Form1";
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.ISNOT_NEED_DELETE_FLG, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.KYOTEN_CD, 0);
            this.Controls.SetChildIndex(this.KYOTEN_NAME, 0);
            this.Controls.SetChildIndex(this.customPanel6, 0);
            this.customPanel6.ResumeLayout(false);
            this.customPanel6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label1;
        internal r_framework.CustomControl.CustomTextBox KYOTEN_NAME;
        internal r_framework.CustomControl.CustomNumericTextBox2 KYOTEN_CD;
        public System.Windows.Forms.Label label2;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
        internal r_framework.CustomControl.CustomPanel customPanel6;
        internal r_framework.CustomControl.CustomRadioButton radbtn_Barcode_Off;
        internal r_framework.CustomControl.CustomRadioButton radbtn_Barcode_On;
        internal r_framework.CustomControl.CustomNumericTextBox2 txtNum_BarcodeKubun;

    }
}