namespace Shougun.Core.ReceiptPayManagement.NyuukinDataTorikomi
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
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.lbl_YomikomiDataKensu = new System.Windows.Forms.Label();
            this.ReadDataNumber = new r_framework.CustomControl.CustomNumericTextBox2();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Visible = false;
            // 
            // lb_title
            // 
            this.lb_title.Size = new System.Drawing.Size(259, 34);
            this.lb_title.Text = "入金データ取込";
            // 
            // lbl_YomikomiDataKensu
            // 
            this.lbl_YomikomiDataKensu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_YomikomiDataKensu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_YomikomiDataKensu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_YomikomiDataKensu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_YomikomiDataKensu.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_YomikomiDataKensu.ForeColor = System.Drawing.Color.White;
            this.lbl_YomikomiDataKensu.Location = new System.Drawing.Point(973, 14);
            this.lbl_YomikomiDataKensu.Name = "lbl_YomikomiDataKensu";
            this.lbl_YomikomiDataKensu.Size = new System.Drawing.Size(110, 20);
            this.lbl_YomikomiDataKensu.TabIndex = 532;
            this.lbl_YomikomiDataKensu.Text = "読込データ件数";
            this.lbl_YomikomiDataKensu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ReadDataNumber
            // 
            this.ReadDataNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ReadDataNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ReadDataNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReadDataNumber.CharacterLimitList = new char[0];
            this.ReadDataNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.ReadDataNumber.DisplayPopUp = null;
            this.ReadDataNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ReadDataNumber.FocusOutCheckMethod")));
            this.ReadDataNumber.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.ReadDataNumber.ForeColor = System.Drawing.Color.Black;
            this.ReadDataNumber.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ReadDataNumber.IsInputErrorOccured = false;
            this.ReadDataNumber.LinkedRadioButtonArray = new string[0];
            this.ReadDataNumber.Location = new System.Drawing.Point(1088, 14);
            //this.ReadDataNumber.MinusEnableFlag = false;
            this.ReadDataNumber.Name = "ReadDataNumber";
            this.ReadDataNumber.PopupAfterExecute = null;
            this.ReadDataNumber.PopupBeforeExecute = null;
            this.ReadDataNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ReadDataNumber.PopupSearchSendParams")));
            this.ReadDataNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ReadDataNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ReadDataNumber.popupWindowSetting")));
            this.ReadDataNumber.prevText = null;
            this.ReadDataNumber.PrevText = "";
            rangeSettingDto2.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            rangeSettingDto2.Min = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ReadDataNumber.RangeSetting = rangeSettingDto2;
            this.ReadDataNumber.ReadOnly = true;
            this.ReadDataNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ReadDataNumber.RegistCheckMethod")));
            this.ReadDataNumber.Size = new System.Drawing.Size(80, 20);
            this.ReadDataNumber.TabIndex = 534;
            this.ReadDataNumber.TabStop = false;
            this.ReadDataNumber.Tag = "検索結果の総件数が表示されます";
            this.ReadDataNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.ReadDataNumber);
            this.Controls.Add(this.lbl_YomikomiDataKensu);
            this.Name = "UIHeader";
            this.Text = "HeaderSample";
            this.Controls.SetChildIndex(this.lbl_YomikomiDataKensu, 0);
            this.Controls.SetChildIndex(this.ReadDataNumber, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_YomikomiDataKensu;
        internal r_framework.CustomControl.CustomNumericTextBox2 ReadDataNumber;
    }
}