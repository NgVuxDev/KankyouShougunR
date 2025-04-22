namespace Shougun.Core.ExternalConnection.HaisouKeikakuNyuuryoku
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
            this.ReadDataNumber = new r_framework.CustomControl.CustomTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CARRY_OVER_NEXT_DAY = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label4 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.rb_CARRY_OVER_NEXT_DAY_2 = new r_framework.CustomControl.CustomRadioButton();
            this.rb_CARRY_OVER_NEXT_DAY_1 = new r_framework.CustomControl.CustomRadioButton();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Location = new System.Drawing.Point(1, 5);
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(82, 2);
            this.lb_title.Size = new System.Drawing.Size(285, 34);
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
            this.ReadDataNumber.Location = new System.Drawing.Point(1096, 24);
            this.ReadDataNumber.Name = "ReadDataNumber";
            this.ReadDataNumber.PopupAfterExecute = null;
            this.ReadDataNumber.PopupBeforeExecute = null;
            this.ReadDataNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ReadDataNumber.PopupSearchSendParams")));
            this.ReadDataNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ReadDataNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ReadDataNumber.popupWindowSetting")));
            this.ReadDataNumber.ReadOnly = true;
            this.ReadDataNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ReadDataNumber.RegistCheckMethod")));
            this.ReadDataNumber.Size = new System.Drawing.Size(80, 20);
            this.ReadDataNumber.TabIndex = 390;
            this.ReadDataNumber.TabStop = false;
            this.ReadDataNumber.Tag = "検索結果の総件数が表示されます";
            this.ReadDataNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ReadDataNumber.Visible = false;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(981, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 20);
            this.label5.TabIndex = 389;
            this.label5.Text = "読込データ件数";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label5.Visible = false;
            // 
            // CARRY_OVER_NEXT_DAY
            // 
            this.CARRY_OVER_NEXT_DAY.BackColor = System.Drawing.SystemColors.Window;
            this.CARRY_OVER_NEXT_DAY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CARRY_OVER_NEXT_DAY.DefaultBackColor = System.Drawing.Color.Empty;
            this.CARRY_OVER_NEXT_DAY.DisplayItemName = "翌日持越区分";
            this.CARRY_OVER_NEXT_DAY.DisplayPopUp = null;
            this.CARRY_OVER_NEXT_DAY.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CARRY_OVER_NEXT_DAY.FocusOutCheckMethod")));
            this.CARRY_OVER_NEXT_DAY.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CARRY_OVER_NEXT_DAY.ForeColor = System.Drawing.Color.Black;
            this.CARRY_OVER_NEXT_DAY.IsInputErrorOccured = false;
            this.CARRY_OVER_NEXT_DAY.LinkedRadioButtonArray = new string[] {
        "rb_CARRY_OVER_NEXT_DAY_1",
        "rb_CARRY_OVER_NEXT_DAY_2"};
            this.CARRY_OVER_NEXT_DAY.Location = new System.Drawing.Point(576, 14);
            this.CARRY_OVER_NEXT_DAY.Name = "CARRY_OVER_NEXT_DAY";
            this.CARRY_OVER_NEXT_DAY.PopupAfterExecute = null;
            this.CARRY_OVER_NEXT_DAY.PopupBeforeExecute = null;
            this.CARRY_OVER_NEXT_DAY.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CARRY_OVER_NEXT_DAY.PopupSearchSendParams")));
            this.CARRY_OVER_NEXT_DAY.PopupSetFormField = "";
            this.CARRY_OVER_NEXT_DAY.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CARRY_OVER_NEXT_DAY.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CARRY_OVER_NEXT_DAY.popupWindowSetting")));
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
            this.CARRY_OVER_NEXT_DAY.RangeSetting = rangeSettingDto1;
            this.CARRY_OVER_NEXT_DAY.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CARRY_OVER_NEXT_DAY.RegistCheckMethod")));
            this.CARRY_OVER_NEXT_DAY.SetFormField = "";
            this.CARRY_OVER_NEXT_DAY.Size = new System.Drawing.Size(20, 20);
            this.CARRY_OVER_NEXT_DAY.TabIndex = 635;
            this.CARRY_OVER_NEXT_DAY.Tag = "【1、2】のいずれかで入力してください";
            this.CARRY_OVER_NEXT_DAY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CARRY_OVER_NEXT_DAY.WordWrap = false;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(467, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 20);
            this.label4.TabIndex = 637;
            this.label4.Text = "翌日持越区分";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.rb_CARRY_OVER_NEXT_DAY_2);
            this.panel5.Controls.Add(this.rb_CARRY_OVER_NEXT_DAY_1);
            this.panel5.Location = new System.Drawing.Point(595, 14);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(276, 20);
            this.panel5.TabIndex = 636;
            // 
            // rb_CARRY_OVER_NEXT_DAY_2
            // 
            this.rb_CARRY_OVER_NEXT_DAY_2.AutoSize = true;
            this.rb_CARRY_OVER_NEXT_DAY_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.rb_CARRY_OVER_NEXT_DAY_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_CARRY_OVER_NEXT_DAY_2.FocusOutCheckMethod")));
            this.rb_CARRY_OVER_NEXT_DAY_2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rb_CARRY_OVER_NEXT_DAY_2.LinkedTextBox = "CARRY_OVER_NEXT_DAY";
            this.rb_CARRY_OVER_NEXT_DAY_2.Location = new System.Drawing.Point(140, 1);
            this.rb_CARRY_OVER_NEXT_DAY_2.Name = "rb_CARRY_OVER_NEXT_DAY_2";
            this.rb_CARRY_OVER_NEXT_DAY_2.PopupAfterExecute = null;
            this.rb_CARRY_OVER_NEXT_DAY_2.PopupBeforeExecute = null;
            this.rb_CARRY_OVER_NEXT_DAY_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rb_CARRY_OVER_NEXT_DAY_2.PopupSearchSendParams")));
            this.rb_CARRY_OVER_NEXT_DAY_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rb_CARRY_OVER_NEXT_DAY_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rb_CARRY_OVER_NEXT_DAY_2.popupWindowSetting")));
            this.rb_CARRY_OVER_NEXT_DAY_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_CARRY_OVER_NEXT_DAY_2.RegistCheckMethod")));
            this.rb_CARRY_OVER_NEXT_DAY_2.Size = new System.Drawing.Size(130, 17);
            this.rb_CARRY_OVER_NEXT_DAY_2.TabIndex = 20;
            this.rb_CARRY_OVER_NEXT_DAY_2.Tag = "翌日持越無しの場合チェックを付けてください";
            this.rb_CARRY_OVER_NEXT_DAY_2.Text = "2. 翌日持越無し";
            this.rb_CARRY_OVER_NEXT_DAY_2.UseVisualStyleBackColor = true;
            this.rb_CARRY_OVER_NEXT_DAY_2.Value = "2";
            // 
            // rb_CARRY_OVER_NEXT_DAY_1
            // 
            this.rb_CARRY_OVER_NEXT_DAY_1.AutoSize = true;
            this.rb_CARRY_OVER_NEXT_DAY_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.rb_CARRY_OVER_NEXT_DAY_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_CARRY_OVER_NEXT_DAY_1.FocusOutCheckMethod")));
            this.rb_CARRY_OVER_NEXT_DAY_1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rb_CARRY_OVER_NEXT_DAY_1.LinkedTextBox = "CARRY_OVER_NEXT_DAY";
            this.rb_CARRY_OVER_NEXT_DAY_1.Location = new System.Drawing.Point(4, 1);
            this.rb_CARRY_OVER_NEXT_DAY_1.Name = "rb_CARRY_OVER_NEXT_DAY_1";
            this.rb_CARRY_OVER_NEXT_DAY_1.PopupAfterExecute = null;
            this.rb_CARRY_OVER_NEXT_DAY_1.PopupBeforeExecute = null;
            this.rb_CARRY_OVER_NEXT_DAY_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rb_CARRY_OVER_NEXT_DAY_1.PopupSearchSendParams")));
            this.rb_CARRY_OVER_NEXT_DAY_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rb_CARRY_OVER_NEXT_DAY_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rb_CARRY_OVER_NEXT_DAY_1.popupWindowSetting")));
            this.rb_CARRY_OVER_NEXT_DAY_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_CARRY_OVER_NEXT_DAY_1.RegistCheckMethod")));
            this.rb_CARRY_OVER_NEXT_DAY_1.Size = new System.Drawing.Size(130, 17);
            this.rb_CARRY_OVER_NEXT_DAY_1.TabIndex = 10;
            this.rb_CARRY_OVER_NEXT_DAY_1.Tag = "翌日持越有りの場合チェックを付けてください";
            this.rb_CARRY_OVER_NEXT_DAY_1.Text = "1. 翌日持越有り";
            this.rb_CARRY_OVER_NEXT_DAY_1.UseVisualStyleBackColor = true;
            this.rb_CARRY_OVER_NEXT_DAY_1.Value = "1";
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.CARRY_OVER_NEXT_DAY);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.ReadDataNumber);
            this.Controls.Add(this.label5);
            this.Name = "UIHeader";
            this.Text = "UIHeader";
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.ReadDataNumber, 0);
            this.Controls.SetChildIndex(this.panel5, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.CARRY_OVER_NEXT_DAY, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomTextBox ReadDataNumber;
        public System.Windows.Forms.Label label5;
        internal r_framework.CustomControl.CustomNumericTextBox2 CARRY_OVER_NEXT_DAY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel5;
        internal r_framework.CustomControl.CustomRadioButton rb_CARRY_OVER_NEXT_DAY_2;
        internal r_framework.CustomControl.CustomRadioButton rb_CARRY_OVER_NEXT_DAY_1;
    }
}