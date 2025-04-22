namespace Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.APP
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
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCsvOutputKbn = new r_framework.CustomControl.CustomNumericTextBox2();
            this.pnlDenpyouDateKbn = new r_framework.CustomControl.CustomPanel();
            this.rdoCsvOutputKbn2 = new r_framework.CustomControl.CustomRadioButton();
            this.rdoCsvOutputKbn1 = new r_framework.CustomControl.CustomRadioButton();
            this.txtUpdateMode = new r_framework.CustomControl.CustomNumericTextBox2();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.rdoUpdateModeKbn2 = new r_framework.CustomControl.CustomRadioButton();
            this.rdoUpdateModeKbn1 = new r_framework.CustomControl.CustomRadioButton();
            this.pnlDenpyouDateKbn.SuspendLayout();
            this.customPanel1.SuspendLayout();
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
            this.lb_title.Text = "○○入力";
            // 
            // label
            // 
            this.label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label.ForeColor = System.Drawing.Color.White;
            this.label.Location = new System.Drawing.Point(513, 2);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(123, 20);
            this.label.TabIndex = 1;
            this.label.Text = "更新CSV出力※";
            this.label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(513, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "マスタ単価更新※";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.txtCsvOutputKbn.Location = new System.Drawing.Point(640, 2);
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
            this.txtCsvOutputKbn.TabIndex = 2;
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
            this.pnlDenpyouDateKbn.Location = new System.Drawing.Point(659, 2);
            this.pnlDenpyouDateKbn.Name = "pnlDenpyouDateKbn";
            this.pnlDenpyouDateKbn.Size = new System.Drawing.Size(195, 20);
            this.pnlDenpyouDateKbn.TabIndex = 3;
            this.pnlDenpyouDateKbn.TabStop = true;
            // 
            // rdoCsvOutputKbn2
            // 
            this.rdoCsvOutputKbn2.AutoSize = true;
            this.rdoCsvOutputKbn2.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoCsvOutputKbn2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoCsvOutputKbn2.FocusOutCheckMethod")));
            this.rdoCsvOutputKbn2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoCsvOutputKbn2.LinkedTextBox = "txtCsvOutputKbn";
            this.rdoCsvOutputKbn2.Location = new System.Drawing.Point(101, 0);
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
            // txtUpdateMode
            // 
            this.txtUpdateMode.BackColor = System.Drawing.SystemColors.Window;
            this.txtUpdateMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUpdateMode.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtUpdateMode.DisplayItemName = "マスタ単価更新";
            this.txtUpdateMode.DisplayPopUp = null;
            this.txtUpdateMode.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtUpdateMode.FocusOutCheckMethod")));
            this.txtUpdateMode.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtUpdateMode.ForeColor = System.Drawing.Color.Black;
            this.txtUpdateMode.IsInputErrorOccured = false;
            this.txtUpdateMode.LinkedRadioButtonArray = new string[] {
        "rdoUpdateModeKbn1",
        "rdoUpdateModeKbn2"};
            this.txtUpdateMode.Location = new System.Drawing.Point(640, 24);
            this.txtUpdateMode.Name = "txtUpdateMode";
            this.txtUpdateMode.PopupAfterExecute = null;
            this.txtUpdateMode.PopupBeforeExecute = null;
            this.txtUpdateMode.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtUpdateMode.PopupSearchSendParams")));
            this.txtUpdateMode.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtUpdateMode.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtUpdateMode.popupWindowSetting")));
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
            this.txtUpdateMode.RangeSetting = rangeSettingDto2;
            this.txtUpdateMode.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtUpdateMode.RegistCheckMethod")));
            this.txtUpdateMode.ShortItemName = "マスタ単価更新";
            this.txtUpdateMode.Size = new System.Drawing.Size(20, 20);
            this.txtUpdateMode.TabIndex = 12;
            this.txtUpdateMode.Tag = "【1～2】のいずれかで入力してください";
            this.txtUpdateMode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtUpdateMode.WordWrap = false;
            // 
            // customPanel1
            // 
            this.customPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel1.Controls.Add(this.rdoUpdateModeKbn2);
            this.customPanel1.Controls.Add(this.rdoUpdateModeKbn1);
            this.customPanel1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.customPanel1.Location = new System.Drawing.Point(659, 24);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(195, 20);
            this.customPanel1.TabIndex = 13;
            // 
            // rdoUpdateModeKbn2
            // 
            this.rdoUpdateModeKbn2.AutoSize = true;
            this.rdoUpdateModeKbn2.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoUpdateModeKbn2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoUpdateModeKbn2.FocusOutCheckMethod")));
            this.rdoUpdateModeKbn2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoUpdateModeKbn2.LinkedTextBox = "txtUpdateMode";
            this.rdoUpdateModeKbn2.Location = new System.Drawing.Point(101, 0);
            this.rdoUpdateModeKbn2.Name = "rdoUpdateModeKbn2";
            this.rdoUpdateModeKbn2.PopupAfterExecute = null;
            this.rdoUpdateModeKbn2.PopupBeforeExecute = null;
            this.rdoUpdateModeKbn2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoUpdateModeKbn2.PopupSearchSendParams")));
            this.rdoUpdateModeKbn2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoUpdateModeKbn2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoUpdateModeKbn2.popupWindowSetting")));
            this.rdoUpdateModeKbn2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoUpdateModeKbn2.RegistCheckMethod")));
            this.rdoUpdateModeKbn2.Size = new System.Drawing.Size(81, 17);
            this.rdoUpdateModeKbn2.TabIndex = 15;
            this.rdoUpdateModeKbn2.Tag = "マスタ単価更新しない場合にはチェックを付けてください";
            this.rdoUpdateModeKbn2.Text = "2.しない";
            this.rdoUpdateModeKbn2.UseVisualStyleBackColor = true;
            this.rdoUpdateModeKbn2.Value = "2";
            // 
            // rdoUpdateModeKbn1
            // 
            this.rdoUpdateModeKbn1.AutoSize = true;
            this.rdoUpdateModeKbn1.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoUpdateModeKbn1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoUpdateModeKbn1.FocusOutCheckMethod")));
            this.rdoUpdateModeKbn1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoUpdateModeKbn1.LinkedTextBox = "txtUpdateMode";
            this.rdoUpdateModeKbn1.Location = new System.Drawing.Point(6, 0);
            this.rdoUpdateModeKbn1.Name = "rdoUpdateModeKbn1";
            this.rdoUpdateModeKbn1.PopupAfterExecute = null;
            this.rdoUpdateModeKbn1.PopupBeforeExecute = null;
            this.rdoUpdateModeKbn1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoUpdateModeKbn1.PopupSearchSendParams")));
            this.rdoUpdateModeKbn1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoUpdateModeKbn1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoUpdateModeKbn1.popupWindowSetting")));
            this.rdoUpdateModeKbn1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoUpdateModeKbn1.RegistCheckMethod")));
            this.rdoUpdateModeKbn1.Size = new System.Drawing.Size(67, 17);
            this.rdoUpdateModeKbn1.TabIndex = 14;
            this.rdoUpdateModeKbn1.Tag = "マスタ単価更新する場合にはチェックを付けてください";
            this.rdoUpdateModeKbn1.Text = "1.する";
            this.rdoUpdateModeKbn1.UseVisualStyleBackColor = true;
            this.rdoUpdateModeKbn1.Value = "1";
            // 
            // UIHeaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.txtUpdateMode);
            this.Controls.Add(this.customPanel1);
            this.Controls.Add(this.txtCsvOutputKbn);
            this.Controls.Add(this.pnlDenpyouDateKbn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label);
            this.Location = new System.Drawing.Point(12, 6);
            this.Name = "UIHeaderForm";
            this.Text = "";
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.label, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.pnlDenpyouDateKbn, 0);
            this.Controls.SetChildIndex(this.txtCsvOutputKbn, 0);
            this.Controls.SetChildIndex(this.customPanel1, 0);
            this.Controls.SetChildIndex(this.txtUpdateMode, 0);
            this.pnlDenpyouDateKbn.ResumeLayout(false);
            this.pnlDenpyouDateKbn.PerformLayout();
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label label;
        internal System.Windows.Forms.Label label1;
        internal r_framework.CustomControl.CustomNumericTextBox2 txtCsvOutputKbn;
        internal r_framework.CustomControl.CustomPanel pnlDenpyouDateKbn;
        internal r_framework.CustomControl.CustomRadioButton rdoCsvOutputKbn2;
        internal r_framework.CustomControl.CustomRadioButton rdoCsvOutputKbn1;
        internal r_framework.CustomControl.CustomNumericTextBox2 txtUpdateMode;
        internal r_framework.CustomControl.CustomPanel customPanel1;
        internal r_framework.CustomControl.CustomRadioButton rdoUpdateModeKbn2;
        internal r_framework.CustomControl.CustomRadioButton rdoUpdateModeKbn1;

    }
}