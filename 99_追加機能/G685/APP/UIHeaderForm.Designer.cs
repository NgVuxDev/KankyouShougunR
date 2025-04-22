namespace Shougun.Core.BusinessManagement.DenpyouDetailIkkatuUpdate.APP
{
    partial class UIHeaderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIHeaderForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.label = new System.Windows.Forms.Label();
            this.txtCsvOutputKbn = new r_framework.CustomControl.CustomNumericTextBox2();
            this.pnlDenpyouDateKbn = new r_framework.CustomControl.CustomPanel();
            this.rdoCsvOutputKbn2 = new r_framework.CustomControl.CustomRadioButton();
            this.rdoCsvOutputKbn1 = new r_framework.CustomControl.CustomRadioButton();
            this.alertNumber = new r_framework.CustomControl.CustomTextBox();
            this.ReadDataNumber = new r_framework.CustomControl.CustomTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbx_Tanka = new r_framework.CustomControl.CustomCheckBox();
            this.cbx_Suuryou = new r_framework.CustomControl.CustomCheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbx_Hinmei = new r_framework.CustomControl.CustomCheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbx_MeisaiBikou = new r_framework.CustomControl.CustomCheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbx_Unit = new r_framework.CustomControl.CustomCheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pnlDenpyouDateKbn.SuspendLayout();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.TabIndex = 10;
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(0, 6);
            this.lb_title.Size = new System.Drawing.Size(285, 34);
            this.lb_title.TabIndex = 20;
            this.lb_title.Text = "伝票明細一括更新";
            // 
            // label
            // 
            this.label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label.ForeColor = System.Drawing.Color.White;
            this.label.Location = new System.Drawing.Point(661, 2);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(123, 20);
            this.label.TabIndex = 1;
            this.label.Text = "更新CSV出力※";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCsvOutputKbn
            // 
            this.txtCsvOutputKbn.BackColor = System.Drawing.SystemColors.Window;
            this.txtCsvOutputKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCsvOutputKbn.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtCsvOutputKbn.DisplayItemName = "更新CSV出力";
            this.txtCsvOutputKbn.DisplayPopUp = null;
            this.txtCsvOutputKbn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtCsvOutputKbn.FocusOutCheckMethod")));
            this.txtCsvOutputKbn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtCsvOutputKbn.ForeColor = System.Drawing.Color.Black;
            this.txtCsvOutputKbn.IsInputErrorOccured = false;
            this.txtCsvOutputKbn.LinkedRadioButtonArray = new string[] {
        "rdoCsvOutputKbn1",
        "rdoCsvOutputKbn2"};
            this.txtCsvOutputKbn.Location = new System.Drawing.Point(788, 2);
            this.txtCsvOutputKbn.Name = "txtCsvOutputKbn";
            this.txtCsvOutputKbn.PopupAfterExecute = null;
            this.txtCsvOutputKbn.PopupBeforeExecute = null;
            this.txtCsvOutputKbn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtCsvOutputKbn.PopupSearchSendParams")));
            this.txtCsvOutputKbn.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtCsvOutputKbn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtCsvOutputKbn.popupWindowSetting")));
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
            this.txtCsvOutputKbn.RangeSetting = rangeSettingDto1;
            this.txtCsvOutputKbn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtCsvOutputKbn.RegistCheckMethod")));
            this.txtCsvOutputKbn.ShortItemName = "更新CSV出力";
            this.txtCsvOutputKbn.Size = new System.Drawing.Size(20, 20);
            this.txtCsvOutputKbn.TabIndex = 6;
            this.txtCsvOutputKbn.Tag = "【1～2】のいずれかで入力してください";
            this.txtCsvOutputKbn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCsvOutputKbn.WordWrap = false;
            // 
            // pnlDenpyouDateKbn
            // 
            this.pnlDenpyouDateKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDenpyouDateKbn.Controls.Add(this.rdoCsvOutputKbn2);
            this.pnlDenpyouDateKbn.Controls.Add(this.rdoCsvOutputKbn1);
            this.pnlDenpyouDateKbn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.pnlDenpyouDateKbn.Location = new System.Drawing.Point(807, 2);
            this.pnlDenpyouDateKbn.Name = "pnlDenpyouDateKbn";
            this.pnlDenpyouDateKbn.Size = new System.Drawing.Size(165, 20);
            this.pnlDenpyouDateKbn.TabIndex = 7;
            // 
            // rdoCsvOutputKbn2
            // 
            this.rdoCsvOutputKbn2.AutoSize = true;
            this.rdoCsvOutputKbn2.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoCsvOutputKbn2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoCsvOutputKbn2.FocusOutCheckMethod")));
            this.rdoCsvOutputKbn2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoCsvOutputKbn2.LinkedTextBox = "txtCsvOutputKbn";
            this.rdoCsvOutputKbn2.Location = new System.Drawing.Point(78, 0);
            this.rdoCsvOutputKbn2.Name = "rdoCsvOutputKbn2";
            this.rdoCsvOutputKbn2.PopupAfterExecute = null;
            this.rdoCsvOutputKbn2.PopupBeforeExecute = null;
            this.rdoCsvOutputKbn2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoCsvOutputKbn2.PopupSearchSendParams")));
            this.rdoCsvOutputKbn2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoCsvOutputKbn2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoCsvOutputKbn2.popupWindowSetting")));
            this.rdoCsvOutputKbn2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoCsvOutputKbn2.RegistCheckMethod")));
            this.rdoCsvOutputKbn2.Size = new System.Drawing.Size(81, 17);
            this.rdoCsvOutputKbn2.TabIndex = 5;
            this.rdoCsvOutputKbn2.Tag = "更新CSVが出力しない場合にはチェックを付けてください";
            this.rdoCsvOutputKbn2.Text = "2.しない";
            this.rdoCsvOutputKbn2.UseVisualStyleBackColor = true;
            this.rdoCsvOutputKbn2.Value = "2";
            // 
            // rdoCsvOutputKbn1
            // 
            this.rdoCsvOutputKbn1.AutoSize = true;
            this.rdoCsvOutputKbn1.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoCsvOutputKbn1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoCsvOutputKbn1.FocusOutCheckMethod")));
            this.rdoCsvOutputKbn1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoCsvOutputKbn1.LinkedTextBox = "txtCsvOutputKbn";
            this.rdoCsvOutputKbn1.Location = new System.Drawing.Point(6, 0);
            this.rdoCsvOutputKbn1.Name = "rdoCsvOutputKbn1";
            this.rdoCsvOutputKbn1.PopupAfterExecute = null;
            this.rdoCsvOutputKbn1.PopupBeforeExecute = null;
            this.rdoCsvOutputKbn1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoCsvOutputKbn1.PopupSearchSendParams")));
            this.rdoCsvOutputKbn1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoCsvOutputKbn1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoCsvOutputKbn1.popupWindowSetting")));
            this.rdoCsvOutputKbn1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoCsvOutputKbn1.RegistCheckMethod")));
            this.rdoCsvOutputKbn1.Size = new System.Drawing.Size(67, 17);
            this.rdoCsvOutputKbn1.TabIndex = 4;
            this.rdoCsvOutputKbn1.Tag = "更新CSVが出力する場合にはチェックを付けてください";
            this.rdoCsvOutputKbn1.Text = "1.する";
            this.rdoCsvOutputKbn1.UseVisualStyleBackColor = true;
            this.rdoCsvOutputKbn1.Value = "1";
            // 
            // alertNumber
            // 
            this.alertNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.alertNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.alertNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.alertNumber.DisplayPopUp = null;
            this.alertNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("alertNumber.FocusOutCheckMethod")));
            this.alertNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.alertNumber.ForeColor = System.Drawing.Color.Black;
            this.alertNumber.IsInputErrorOccured = false;
            this.alertNumber.Location = new System.Drawing.Point(1094, 2);
            this.alertNumber.MaxLength = 5;
            this.alertNumber.Name = "alertNumber";
            this.alertNumber.PopupAfterExecute = null;
            this.alertNumber.PopupBeforeExecute = null;
            this.alertNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("alertNumber.PopupSearchSendParams")));
            this.alertNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.alertNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("alertNumber.popupWindowSetting")));
            this.alertNumber.ReadOnly = true;
            this.alertNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("alertNumber.RegistCheckMethod")));
            this.alertNumber.Size = new System.Drawing.Size(80, 20);
            this.alertNumber.TabIndex = 24;
            this.alertNumber.TabStop = false;
            this.alertNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.alertNumber.Visible = false;
            // 
            // ReadDataNumber
            // 
            this.ReadDataNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ReadDataNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReadDataNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.ReadDataNumber.DisplayPopUp = null;
            this.ReadDataNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ReadDataNumber.FocusOutCheckMethod")));
            this.ReadDataNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ReadDataNumber.ForeColor = System.Drawing.Color.Black;
            this.ReadDataNumber.IsInputErrorOccured = false;
            this.ReadDataNumber.Location = new System.Drawing.Point(1094, 24);
            this.ReadDataNumber.Name = "ReadDataNumber";
            this.ReadDataNumber.PopupAfterExecute = null;
            this.ReadDataNumber.PopupBeforeExecute = null;
            this.ReadDataNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ReadDataNumber.PopupSearchSendParams")));
            this.ReadDataNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ReadDataNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ReadDataNumber.popupWindowSetting")));
            this.ReadDataNumber.ReadOnly = true;
            this.ReadDataNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ReadDataNumber.RegistCheckMethod")));
            this.ReadDataNumber.Size = new System.Drawing.Size(80, 20);
            this.ReadDataNumber.TabIndex = 22;
            this.ReadDataNumber.TabStop = false;
            this.ReadDataNumber.Tag = "検索結果の総件数が表示されます";
            this.ReadDataNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(979, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 23;
            this.label4.Text = "アラート件数";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(979, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 20);
            this.label5.TabIndex = 21;
            this.label5.Text = "読込データ件数";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(543, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 20);
            this.label2.TabIndex = 25;
            this.label2.Text = "単価・金額";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbx_Tanka
            // 
            this.cbx_Tanka.AutoSize = true;
            this.cbx_Tanka.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.cbx_Tanka.DefaultBackColor = System.Drawing.Color.Empty;
            this.cbx_Tanka.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbx_Tanka.FocusOutCheckMethod")));
            this.cbx_Tanka.Location = new System.Drawing.Point(632, 6);
            this.cbx_Tanka.Name = "cbx_Tanka";
            this.cbx_Tanka.PopupAfterExecute = null;
            this.cbx_Tanka.PopupBeforeExecute = null;
            this.cbx_Tanka.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cbx_Tanka.PopupSearchSendParams")));
            this.cbx_Tanka.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cbx_Tanka.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cbx_Tanka.popupWindowSetting")));
            this.cbx_Tanka.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbx_Tanka.RegistCheckMethod")));
            this.cbx_Tanka.Size = new System.Drawing.Size(15, 14);
            this.cbx_Tanka.TabIndex = 3;
            this.cbx_Tanka.UseVisualStyleBackColor = false;
            // 
            // cbx_Suuryou
            // 
            this.cbx_Suuryou.AutoSize = true;
            this.cbx_Suuryou.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.cbx_Suuryou.DefaultBackColor = System.Drawing.Color.Empty;
            this.cbx_Suuryou.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbx_Suuryou.FocusOutCheckMethod")));
            this.cbx_Suuryou.Location = new System.Drawing.Point(515, 6);
            this.cbx_Suuryou.Name = "cbx_Suuryou";
            this.cbx_Suuryou.PopupAfterExecute = null;
            this.cbx_Suuryou.PopupBeforeExecute = null;
            this.cbx_Suuryou.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cbx_Suuryou.PopupSearchSendParams")));
            this.cbx_Suuryou.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cbx_Suuryou.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cbx_Suuryou.popupWindowSetting")));
            this.cbx_Suuryou.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbx_Suuryou.RegistCheckMethod")));
            this.cbx_Suuryou.Size = new System.Drawing.Size(15, 14);
            this.cbx_Suuryou.TabIndex = 2;
            this.cbx_Suuryou.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(426, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 20);
            this.label1.TabIndex = 27;
            this.label1.Text = "数量";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbx_Hinmei
            // 
            this.cbx_Hinmei.AutoSize = true;
            this.cbx_Hinmei.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.cbx_Hinmei.DefaultBackColor = System.Drawing.Color.Empty;
            this.cbx_Hinmei.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbx_Hinmei.FocusOutCheckMethod")));
            this.cbx_Hinmei.Location = new System.Drawing.Point(397, 6);
            this.cbx_Hinmei.Name = "cbx_Hinmei";
            this.cbx_Hinmei.PopupAfterExecute = null;
            this.cbx_Hinmei.PopupBeforeExecute = null;
            this.cbx_Hinmei.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cbx_Hinmei.PopupSearchSendParams")));
            this.cbx_Hinmei.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cbx_Hinmei.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cbx_Hinmei.popupWindowSetting")));
            this.cbx_Hinmei.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbx_Hinmei.RegistCheckMethod")));
            this.cbx_Hinmei.Size = new System.Drawing.Size(15, 14);
            this.cbx_Hinmei.TabIndex = 1;
            this.cbx_Hinmei.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(308, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 20);
            this.label3.TabIndex = 29;
            this.label3.Text = "品名";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbx_MeisaiBikou
            // 
            this.cbx_MeisaiBikou.AutoSize = true;
            this.cbx_MeisaiBikou.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.cbx_MeisaiBikou.DefaultBackColor = System.Drawing.Color.Empty;
            this.cbx_MeisaiBikou.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbx_MeisaiBikou.FocusOutCheckMethod")));
            this.cbx_MeisaiBikou.Location = new System.Drawing.Point(397, 28);
            this.cbx_MeisaiBikou.Name = "cbx_MeisaiBikou";
            this.cbx_MeisaiBikou.PopupAfterExecute = null;
            this.cbx_MeisaiBikou.PopupBeforeExecute = null;
            this.cbx_MeisaiBikou.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cbx_MeisaiBikou.PopupSearchSendParams")));
            this.cbx_MeisaiBikou.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cbx_MeisaiBikou.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cbx_MeisaiBikou.popupWindowSetting")));
            this.cbx_MeisaiBikou.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbx_MeisaiBikou.RegistCheckMethod")));
            this.cbx_MeisaiBikou.Size = new System.Drawing.Size(15, 14);
            this.cbx_MeisaiBikou.TabIndex = 4;
            this.cbx_MeisaiBikou.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(308, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 20);
            this.label6.TabIndex = 33;
            this.label6.Text = "明細備考";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbx_Unit
            // 
            this.cbx_Unit.AutoSize = true;
            this.cbx_Unit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.cbx_Unit.DefaultBackColor = System.Drawing.Color.Empty;
            this.cbx_Unit.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbx_Unit.FocusOutCheckMethod")));
            this.cbx_Unit.Location = new System.Drawing.Point(515, 28);
            this.cbx_Unit.Name = "cbx_Unit";
            this.cbx_Unit.PopupAfterExecute = null;
            this.cbx_Unit.PopupBeforeExecute = null;
            this.cbx_Unit.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cbx_Unit.PopupSearchSendParams")));
            this.cbx_Unit.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cbx_Unit.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cbx_Unit.popupWindowSetting")));
            this.cbx_Unit.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbx_Unit.RegistCheckMethod")));
            this.cbx_Unit.Size = new System.Drawing.Size(15, 14);
            this.cbx_Unit.TabIndex = 5;
            this.cbx_Unit.UseVisualStyleBackColor = false;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(426, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 20);
            this.label7.TabIndex = 31;
            this.label7.Text = "単位";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UIHeaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.cbx_MeisaiBikou);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbx_Unit);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbx_Hinmei);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbx_Suuryou);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbx_Tanka);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.alertNumber);
            this.Controls.Add(this.ReadDataNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtCsvOutputKbn);
            this.Controls.Add(this.pnlDenpyouDateKbn);
            this.Controls.Add(this.label);
            this.Location = new System.Drawing.Point(12, 6);
            this.Name = "UIHeaderForm";
            this.Controls.SetChildIndex(this.label, 0);
            this.Controls.SetChildIndex(this.pnlDenpyouDateKbn, 0);
            this.Controls.SetChildIndex(this.txtCsvOutputKbn, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.ReadDataNumber, 0);
            this.Controls.SetChildIndex(this.alertNumber, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.cbx_Tanka, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.cbx_Suuryou, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.cbx_Hinmei, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.cbx_Unit, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.cbx_MeisaiBikou, 0);
            this.pnlDenpyouDateKbn.ResumeLayout(false);
            this.pnlDenpyouDateKbn.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label label;
        internal r_framework.CustomControl.CustomNumericTextBox2 txtCsvOutputKbn;
        internal r_framework.CustomControl.CustomPanel pnlDenpyouDateKbn;
        internal r_framework.CustomControl.CustomRadioButton rdoCsvOutputKbn2;
        internal r_framework.CustomControl.CustomRadioButton rdoCsvOutputKbn1;
        public r_framework.CustomControl.CustomTextBox alertNumber;
        public r_framework.CustomControl.CustomTextBox ReadDataNumber;
        internal System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Label label7;
        internal r_framework.CustomControl.CustomCheckBox cbx_Tanka;
        internal r_framework.CustomControl.CustomCheckBox cbx_Suuryou;
        internal r_framework.CustomControl.CustomCheckBox cbx_MeisaiBikou;
        internal r_framework.CustomControl.CustomCheckBox cbx_Unit;
        internal r_framework.CustomControl.CustomCheckBox cbx_Hinmei;

    }
}