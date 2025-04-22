namespace Shougun.Core.Billing.SeikyuShimeShori
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
            this.lbl_Kyoten = new System.Windows.Forms.Label();
            this.KYOTEN_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.KYOTEN_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.pnlBARCODE_KBN = new r_framework.CustomControl.CustomPanel();
            this.BARCODE_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.BARCODE_KBN1 = new r_framework.CustomControl.CustomRadioButton();
            this.BARCODE_KBN2 = new r_framework.CustomControl.CustomRadioButton();
            this.lblBARCODE_KBN = new System.Windows.Forms.Label();
            this.pnlINVOICE_KBN = new r_framework.CustomControl.CustomPanel();
            this.INVOICE_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.INVOICE_KBN1 = new r_framework.CustomControl.CustomRadioButton();
            this.INVOICE_KBN2 = new r_framework.CustomControl.CustomRadioButton();
            this.lblINVOICE_KBN = new System.Windows.Forms.Label();
            this.pnlBARCODE_KBN.SuspendLayout();
            this.pnlINVOICE_KBN.SuspendLayout();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Location = new System.Drawing.Point(318, 6);
            this.windowTypeLabel.Visible = false;
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(30, 6);
            this.lb_title.Size = new System.Drawing.Size(334, 34);
            this.lb_title.Text = "　　期間請求締処理　　";
            // 
            // lbl_Kyoten
            // 
            this.lbl_Kyoten.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Kyoten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Kyoten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Kyoten.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Kyoten.ForeColor = System.Drawing.Color.White;
            this.lbl_Kyoten.Location = new System.Drawing.Point(741, 15);
            this.lbl_Kyoten.Name = "lbl_Kyoten";
            this.lbl_Kyoten.Size = new System.Drawing.Size(110, 20);
            this.lbl_Kyoten.TabIndex = 532;
            this.lbl_Kyoten.Text = "拠点";
            this.lbl_Kyoten.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_Kyoten.Visible = false;
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
            this.KYOTEN_NAME_RYAKU.Location = new System.Drawing.Point(908, 15);
            this.KYOTEN_NAME_RYAKU.MaxLength = 0;
            this.KYOTEN_NAME_RYAKU.Name = "KYOTEN_NAME_RYAKU";
            this.KYOTEN_NAME_RYAKU.PopupAfterExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupBeforeExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.PopupSearchSendParams")));
            this.KYOTEN_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOTEN_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.popupWindowSetting")));
            this.KYOTEN_NAME_RYAKU.ReadOnly = true;
            this.KYOTEN_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.RegistCheckMethod")));
            this.KYOTEN_NAME_RYAKU.Size = new System.Drawing.Size(212, 20);
            this.KYOTEN_NAME_RYAKU.TabIndex = 541;
            this.KYOTEN_NAME_RYAKU.TabStop = false;
            this.KYOTEN_NAME_RYAKU.Tag = "";
            this.KYOTEN_NAME_RYAKU.Visible = false;
            // 
            // KYOTEN_CD
            // 
            this.KYOTEN_CD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KYOTEN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_CD.CharacterLimitList = null;
            this.KYOTEN_CD.DBFieldsName = "KYOTEN_CD";
            this.KYOTEN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_CD.DisplayItemName = "";
            this.KYOTEN_CD.DisplayPopUp = null;
            this.KYOTEN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.FocusOutCheckMethod")));
            this.KYOTEN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KYOTEN_CD.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_CD.GetCodeMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.KYOTEN_CD.IsInputErrorOccured = false;
            this.KYOTEN_CD.ItemDefinedTypes = "smallint";
            this.KYOTEN_CD.Location = new System.Drawing.Point(856, 15);
            this.KYOTEN_CD.Name = "KYOTEN_CD";
            this.KYOTEN_CD.PopupAfterExecute = null;
            this.KYOTEN_CD.PopupBeforeExecute = null;
            this.KYOTEN_CD.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_CD.PopupSearchSendParams")));
            this.KYOTEN_CD.PopupSetFormField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOTEN_CD.PopupWindowName = "";
            this.KYOTEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_CD.popupWindowSetting")));
            this.KYOTEN_CD.ReadOnly = true;
            this.KYOTEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.RegistCheckMethod")));
            this.KYOTEN_CD.SetFormField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.ShortItemName = "";
            this.KYOTEN_CD.Size = new System.Drawing.Size(53, 20);
            this.KYOTEN_CD.TabIndex = 539;
            this.KYOTEN_CD.TabStop = false;
            this.KYOTEN_CD.Tag = "";
            this.KYOTEN_CD.Visible = false;
            // 
            // pnlBARCODE_KBN
            // 
            this.pnlBARCODE_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBARCODE_KBN.Controls.Add(this.BARCODE_KBN);
            this.pnlBARCODE_KBN.Controls.Add(this.BARCODE_KBN1);
            this.pnlBARCODE_KBN.Controls.Add(this.BARCODE_KBN2);
            this.pnlBARCODE_KBN.Location = new System.Drawing.Point(502, 24);
            this.pnlBARCODE_KBN.Name = "pnlBARCODE_KBN";
            this.pnlBARCODE_KBN.Size = new System.Drawing.Size(188, 20);
            this.pnlBARCODE_KBN.TabIndex = 1;
            // 
            // BARCODE_KBN
            // 
            this.BARCODE_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.BARCODE_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BARCODE_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.BARCODE_KBN.DisplayItemName = "バーコード区分";
            this.BARCODE_KBN.DisplayPopUp = null;
            this.BARCODE_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BARCODE_KBN.FocusOutCheckMethod")));
            this.BARCODE_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.BARCODE_KBN.ForeColor = System.Drawing.Color.Black;
            this.BARCODE_KBN.IsInputErrorOccured = false;
            this.BARCODE_KBN.LinkedRadioButtonArray = new string[] {
        "BARCODE_KBN1",
        "BARCODE_KBN2"};
            this.BARCODE_KBN.Location = new System.Drawing.Point(-1, -1);
            this.BARCODE_KBN.Name = "BARCODE_KBN";
            this.BARCODE_KBN.PopupAfterExecute = null;
            this.BARCODE_KBN.PopupBeforeExecute = null;
            this.BARCODE_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BARCODE_KBN.PopupSearchSendParams")));
            this.BARCODE_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BARCODE_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BARCODE_KBN.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.BARCODE_KBN.RangeSetting = rangeSettingDto1;
            this.BARCODE_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BARCODE_KBN.RegistCheckMethod")));
            this.BARCODE_KBN.Size = new System.Drawing.Size(20, 20);
            this.BARCODE_KBN.TabIndex = 2;
            this.BARCODE_KBN.Tag = "【1～2】のいずれかで入力してください";
            this.BARCODE_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.BARCODE_KBN.WordWrap = false;
            // 
            // BARCODE_KBN1
            // 
            this.BARCODE_KBN1.AutoSize = true;
            this.BARCODE_KBN1.DefaultBackColor = System.Drawing.Color.Empty;
            this.BARCODE_KBN1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BARCODE_KBN1.FocusOutCheckMethod")));
            this.BARCODE_KBN1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.BARCODE_KBN1.LinkedTextBox = "BARCODE_KBN";
            this.BARCODE_KBN1.Location = new System.Drawing.Point(23, 0);
            this.BARCODE_KBN1.Name = "BARCODE_KBN1";
            this.BARCODE_KBN1.PopupAfterExecute = null;
            this.BARCODE_KBN1.PopupBeforeExecute = null;
            this.BARCODE_KBN1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BARCODE_KBN1.PopupSearchSendParams")));
            this.BARCODE_KBN1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BARCODE_KBN1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BARCODE_KBN1.popupWindowSetting")));
            this.BARCODE_KBN1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BARCODE_KBN1.RegistCheckMethod")));
            this.BARCODE_KBN1.Size = new System.Drawing.Size(67, 17);
            this.BARCODE_KBN1.TabIndex = 0;
            this.BARCODE_KBN1.Tag = "バーコード区分が「1.オン」の場合にはチェックを付けてください";
            this.BARCODE_KBN1.Text = "1.オン";
            this.BARCODE_KBN1.UseVisualStyleBackColor = true;
            this.BARCODE_KBN1.Value = "1";
            // 
            // BARCODE_KBN2
            // 
            this.BARCODE_KBN2.AutoSize = true;
            this.BARCODE_KBN2.DefaultBackColor = System.Drawing.Color.Empty;
            this.BARCODE_KBN2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BARCODE_KBN2.FocusOutCheckMethod")));
            this.BARCODE_KBN2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.BARCODE_KBN2.LinkedTextBox = "BARCODE_KBN";
            this.BARCODE_KBN2.Location = new System.Drawing.Point(103, 0);
            this.BARCODE_KBN2.Name = "BARCODE_KBN2";
            this.BARCODE_KBN2.PopupAfterExecute = null;
            this.BARCODE_KBN2.PopupBeforeExecute = null;
            this.BARCODE_KBN2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BARCODE_KBN2.PopupSearchSendParams")));
            this.BARCODE_KBN2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BARCODE_KBN2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BARCODE_KBN2.popupWindowSetting")));
            this.BARCODE_KBN2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BARCODE_KBN2.RegistCheckMethod")));
            this.BARCODE_KBN2.Size = new System.Drawing.Size(67, 17);
            this.BARCODE_KBN2.TabIndex = 1;
            this.BARCODE_KBN2.Tag = "バーコード区分が「2.オフ」の場合にはチェックを付けてください";
            this.BARCODE_KBN2.Text = "2.オフ";
            this.BARCODE_KBN2.UseVisualStyleBackColor = true;
            this.BARCODE_KBN2.Value = "2";
            // 
            // lblBARCODE_KBN
            // 
            this.lblBARCODE_KBN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblBARCODE_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBARCODE_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblBARCODE_KBN.ForeColor = System.Drawing.Color.White;
            this.lblBARCODE_KBN.Location = new System.Drawing.Point(372, 24);
            this.lblBARCODE_KBN.Name = "lblBARCODE_KBN";
            this.lblBARCODE_KBN.Size = new System.Drawing.Size(125, 20);
            this.lblBARCODE_KBN.TabIndex = 542;
            this.lblBARCODE_KBN.Text = "バーコード区分※";
            this.lblBARCODE_KBN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlINVOICE_KBN
            // 
            this.pnlINVOICE_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlINVOICE_KBN.Controls.Add(this.INVOICE_KBN);
            this.pnlINVOICE_KBN.Controls.Add(this.INVOICE_KBN1);
            this.pnlINVOICE_KBN.Controls.Add(this.INVOICE_KBN2);
            this.pnlINVOICE_KBN.Location = new System.Drawing.Point(502, 3);
            this.pnlINVOICE_KBN.Name = "pnlINVOICE_KBN";
            this.pnlINVOICE_KBN.Size = new System.Drawing.Size(242, 20);
            this.pnlINVOICE_KBN.TabIndex = 543;
            // 
            // INVOICE_KBN
            // 
            this.INVOICE_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.INVOICE_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.INVOICE_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.INVOICE_KBN.DisplayItemName = "レイアウト区分";
            this.INVOICE_KBN.DisplayPopUp = null;
            this.INVOICE_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("INVOICE_KBN.FocusOutCheckMethod")));
            this.INVOICE_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.INVOICE_KBN.ForeColor = System.Drawing.Color.Black;
            this.INVOICE_KBN.IsInputErrorOccured = false;
            this.INVOICE_KBN.LinkedRadioButtonArray = new string[] {
        "INVOICE_KBN1",
        "INVOICE_KBN2"};
            this.INVOICE_KBN.Location = new System.Drawing.Point(-1, -1);
            this.INVOICE_KBN.Name = "INVOICE_KBN";
            this.INVOICE_KBN.PopupAfterExecute = null;
            this.INVOICE_KBN.PopupBeforeExecute = null;
            this.INVOICE_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("INVOICE_KBN.PopupSearchSendParams")));
            this.INVOICE_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.INVOICE_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("INVOICE_KBN.popupWindowSetting")));
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
            this.INVOICE_KBN.RangeSetting = rangeSettingDto2;
            this.INVOICE_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("INVOICE_KBN.RegistCheckMethod")));
            this.INVOICE_KBN.Size = new System.Drawing.Size(20, 20);
            this.INVOICE_KBN.TabIndex = 2;
            this.INVOICE_KBN.Tag = "【1～2】のいずれかで入力してください";
            this.INVOICE_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.INVOICE_KBN.WordWrap = false;
            // 
            // INVOICE_KBN1
            // 
            this.INVOICE_KBN1.AutoSize = true;
            this.INVOICE_KBN1.DefaultBackColor = System.Drawing.Color.Empty;
            this.INVOICE_KBN1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("INVOICE_KBN1.FocusOutCheckMethod")));
            this.INVOICE_KBN1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.INVOICE_KBN1.LinkedTextBox = "INVOICE_KBN";
            this.INVOICE_KBN1.Location = new System.Drawing.Point(23, 0);
            this.INVOICE_KBN1.Name = "INVOICE_KBN1";
            this.INVOICE_KBN1.PopupAfterExecute = null;
            this.INVOICE_KBN1.PopupBeforeExecute = null;
            this.INVOICE_KBN1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("INVOICE_KBN1.PopupSearchSendParams")));
            this.INVOICE_KBN1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.INVOICE_KBN1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("INVOICE_KBN1.popupWindowSetting")));
            this.INVOICE_KBN1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("INVOICE_KBN1.RegistCheckMethod")));
            this.INVOICE_KBN1.Size = new System.Drawing.Size(109, 17);
            this.INVOICE_KBN1.TabIndex = 0;
            this.INVOICE_KBN1.Tag = "レイアウト区分が「1.適格請求書」の場合にはチェックを付けてください";
            this.INVOICE_KBN1.Text = "1.適格請求書";
            this.INVOICE_KBN1.UseVisualStyleBackColor = true;
            this.INVOICE_KBN1.Value = "1";
            // 
            // INVOICE_KBN2
            // 
            this.INVOICE_KBN2.AutoSize = true;
            this.INVOICE_KBN2.DefaultBackColor = System.Drawing.Color.Empty;
            this.INVOICE_KBN2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("INVOICE_KBN2.FocusOutCheckMethod")));
            this.INVOICE_KBN2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.INVOICE_KBN2.LinkedTextBox = "INVOICE_KBN";
            this.INVOICE_KBN2.Location = new System.Drawing.Point(140, 0);
            this.INVOICE_KBN2.Name = "INVOICE_KBN2";
            this.INVOICE_KBN2.PopupAfterExecute = null;
            this.INVOICE_KBN2.PopupBeforeExecute = null;
            this.INVOICE_KBN2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("INVOICE_KBN2.PopupSearchSendParams")));
            this.INVOICE_KBN2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.INVOICE_KBN2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("INVOICE_KBN2.popupWindowSetting")));
            this.INVOICE_KBN2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("INVOICE_KBN2.RegistCheckMethod")));
            this.INVOICE_KBN2.Size = new System.Drawing.Size(95, 17);
            this.INVOICE_KBN2.TabIndex = 1;
            this.INVOICE_KBN2.Tag = "レイアウト区分が「2.旧請求書」の場合にはチェックを付けてください";
            this.INVOICE_KBN2.Text = "2.旧請求書";
            this.INVOICE_KBN2.UseVisualStyleBackColor = true;
            this.INVOICE_KBN2.Value = "2";
            // 
            // lblINVOICE_KBN
            // 
            this.lblINVOICE_KBN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblINVOICE_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblINVOICE_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblINVOICE_KBN.ForeColor = System.Drawing.Color.White;
            this.lblINVOICE_KBN.Location = new System.Drawing.Point(372, 3);
            this.lblINVOICE_KBN.Name = "lblINVOICE_KBN";
            this.lblINVOICE_KBN.Size = new System.Drawing.Size(125, 20);
            this.lblINVOICE_KBN.TabIndex = 544;
            this.lblINVOICE_KBN.Text = "レイアウト区分※";
            this.lblINVOICE_KBN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 46);
            this.Controls.Add(this.pnlINVOICE_KBN);
            this.Controls.Add(this.pnlBARCODE_KBN);
            this.Controls.Add(this.lblINVOICE_KBN);
            this.Controls.Add(this.lblBARCODE_KBN);
            this.Controls.Add(this.KYOTEN_NAME_RYAKU);
            this.Controls.Add(this.KYOTEN_CD);
            this.Controls.Add(this.lbl_Kyoten);
            this.Name = "UIHeader";
            this.Text = "HeaderSample";
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.lbl_Kyoten, 0);
            this.Controls.SetChildIndex(this.KYOTEN_CD, 0);
            this.Controls.SetChildIndex(this.KYOTEN_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.lblBARCODE_KBN, 0);
            this.Controls.SetChildIndex(this.lblINVOICE_KBN, 0);
            this.Controls.SetChildIndex(this.pnlBARCODE_KBN, 0);
            this.Controls.SetChildIndex(this.pnlINVOICE_KBN, 0);
            this.pnlBARCODE_KBN.ResumeLayout(false);
            this.pnlBARCODE_KBN.PerformLayout();
            this.pnlINVOICE_KBN.ResumeLayout(false);
            this.pnlINVOICE_KBN.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lbl_Kyoten;
        public r_framework.CustomControl.CustomTextBox KYOTEN_NAME_RYAKU;
        public r_framework.CustomControl.CustomAlphaNumTextBox KYOTEN_CD;
        public r_framework.CustomControl.CustomNumericTextBox2 BARCODE_KBN;
        public r_framework.CustomControl.CustomRadioButton BARCODE_KBN1;
        public r_framework.CustomControl.CustomRadioButton BARCODE_KBN2;
        public r_framework.CustomControl.CustomPanel pnlBARCODE_KBN;
        public System.Windows.Forms.Label lblBARCODE_KBN;
        public r_framework.CustomControl.CustomPanel pnlINVOICE_KBN;
        public r_framework.CustomControl.CustomNumericTextBox2 INVOICE_KBN;
        public r_framework.CustomControl.CustomRadioButton INVOICE_KBN1;
        public r_framework.CustomControl.CustomRadioButton INVOICE_KBN2;
        public System.Windows.Forms.Label lblINVOICE_KBN;

    }
}