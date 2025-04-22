namespace Shougun.Core.Common.BusinessCommon.App
{
    partial class PasswordPopupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PasswordPopupForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.okButton = new r_framework.CustomControl.CustomButton();
            this.cancleButton = new r_framework.CustomControl.CustomButton();
            this.ErrorLabel1 = new r_framework.CustomControl.CustomWindowTypeLabel();
            this.customTextBox1 = new r_framework.CustomControl.CustomNumericTextBox2();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.DefaultBackColor = System.Drawing.Color.Empty;
            this.okButton.Location = new System.Drawing.Point(249, 114);
            this.okButton.Name = "okButton";
            this.okButton.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.okButton.Size = new System.Drawing.Size(91, 33);
            this.okButton.TabIndex = 1;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // cancleButton
            // 
            this.cancleButton.DefaultBackColor = System.Drawing.Color.Empty;
            this.cancleButton.Location = new System.Drawing.Point(357, 113);
            this.cancleButton.Name = "cancleButton";
            this.cancleButton.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.cancleButton.Size = new System.Drawing.Size(91, 33);
            this.cancleButton.TabIndex = 2;
            this.cancleButton.Text = "キャンセル";
            this.cancleButton.UseVisualStyleBackColor = true;
            this.cancleButton.Click += new System.EventHandler(this.CancleButton_Click);
            // 
            // ErrorLabel1
            // 
            this.ErrorLabel1.Location = new System.Drawing.Point(24, 62);
            this.ErrorLabel1.Name = "ErrorLabel1";
            this.ErrorLabel1.Size = new System.Drawing.Size(400, 41);
            this.ErrorLabel1.TabIndex = 3;
            this.ErrorLabel1.WindowType = r_framework.Const.WINDOW_TYPE.NONE;
            // 
            // customTextBox1
            // 
            this.customTextBox1.AutoChangeBackColorEnabled = true;
            this.customTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.customTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customTextBox1.DefaultBackColor = System.Drawing.Color.Empty;
            this.customTextBox1.DisplayPopUp = null;
            this.customTextBox1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customTextBox1.FocusOutCheckMethod")));
            this.customTextBox1.Font = new System.Drawing.Font("MS UI Gothic", 14F);
            this.customTextBox1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.customTextBox1.IsInputErrorOccured = false;
            this.customTextBox1.LinkedRadioButtonArray = new string[0];
            this.customTextBox1.Location = new System.Drawing.Point(21, 12);
            this.customTextBox1.MaxLength = 6;
            this.customTextBox1.Name = "customTextBox1";
            this.customTextBox1.PasswordChar = '●';
            this.customTextBox1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customTextBox1.PopupSearchSendParams")));
            this.customTextBox1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.customTextBox1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customTextBox1.popupWindowSetting")));
            this.customTextBox1.PrevText = "";
            rangeSettingDto1.Max = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.customTextBox1.RangeSetting = rangeSettingDto1;
            this.customTextBox1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customTextBox1.RegistCheckMethod")));
            this.customTextBox1.Size = new System.Drawing.Size(407, 26);
            this.customTextBox1.TabIndex = 0;
            this.customTextBox1.Tag = "";
            // 
            // PasswordPopupForm
            // 
            this.AccessibleDescription = "1";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 159);
            this.Controls.Add(this.customTextBox1);
            this.Controls.Add(this.ErrorLabel1);
            this.Controls.Add(this.cancleButton);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PasswordPopupForm";
            this.Text = "売上情報確定解除パスワードを入力";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private r_framework.CustomControl.CustomButton okButton;
        private r_framework.CustomControl.CustomButton cancleButton;
        private r_framework.CustomControl.CustomWindowTypeLabel ErrorLabel1;
        private r_framework.CustomControl.CustomNumericTextBox2 customTextBox1;
    }
}