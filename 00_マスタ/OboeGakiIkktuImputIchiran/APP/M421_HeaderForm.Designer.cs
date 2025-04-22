namespace OboeGakiIkktuImputIchiran
{
    partial class M421HeaderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(M421HeaderForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.label_YOMOKOMI_KENSU = new System.Windows.Forms.Label();
            this.customNumericTextBox_YOMIKOMI_KENSU = new r_framework.CustomControl.CustomNumericTextBox();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Visible = false;
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(60, 1);
            this.lb_title.Size = new System.Drawing.Size(304, 34);
            this.lb_title.Text = "覚書一括入力一覧";
            // 
            // label_YOMOKOMI_KENSU
            // 
            this.label_YOMOKOMI_KENSU.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label_YOMOKOMI_KENSU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label_YOMOKOMI_KENSU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label_YOMOKOMI_KENSU.ForeColor = System.Drawing.Color.White;
            this.label_YOMOKOMI_KENSU.Location = new System.Drawing.Point(955, 3);
            this.label_YOMOKOMI_KENSU.Name = "label_YOMOKOMI_KENSU";
            this.label_YOMOKOMI_KENSU.Size = new System.Drawing.Size(110, 20);
            this.label_YOMOKOMI_KENSU.TabIndex = 389;
            this.label_YOMOKOMI_KENSU.Text = "読込データ件数";
            this.label_YOMOKOMI_KENSU.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customNumericTextBox_YOMIKOMI_KENSU
            // 
            this.customNumericTextBox_YOMIKOMI_KENSU.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.customNumericTextBox_YOMIKOMI_KENSU.BackColor = System.Drawing.SystemColors.Window;
            this.customNumericTextBox_YOMIKOMI_KENSU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customNumericTextBox_YOMIKOMI_KENSU.CharacterLimitList = new char[0];
            this.customNumericTextBox_YOMIKOMI_KENSU.DefaultBackColor = System.Drawing.Color.Empty;
            this.customNumericTextBox_YOMIKOMI_KENSU.DisplayPopUp = null;
            this.customNumericTextBox_YOMIKOMI_KENSU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customNumericTextBox_YOMIKOMI_KENSU.FocusOutCheckMethod")));
            this.customNumericTextBox_YOMIKOMI_KENSU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.customNumericTextBox_YOMIKOMI_KENSU.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.customNumericTextBox_YOMIKOMI_KENSU.LinkedRadioButtonArray = new string[0];
            this.customNumericTextBox_YOMIKOMI_KENSU.Location = new System.Drawing.Point(1070, 3);
            this.customNumericTextBox_YOMIKOMI_KENSU.MinusEnableFlag = false;
            this.customNumericTextBox_YOMIKOMI_KENSU.Name = "customNumericTextBox_YOMIKOMI_KENSU";
            this.customNumericTextBox_YOMIKOMI_KENSU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customNumericTextBox_YOMIKOMI_KENSU.PopupSearchSendParams")));
            this.customNumericTextBox_YOMIKOMI_KENSU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.customNumericTextBox_YOMIKOMI_KENSU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customNumericTextBox_YOMIKOMI_KENSU.popupWindowSetting")));
            this.customNumericTextBox_YOMIKOMI_KENSU.PrevText = "";
            rangeSettingDto1.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.customNumericTextBox_YOMIKOMI_KENSU.RangeSetting = rangeSettingDto1;
            this.customNumericTextBox_YOMIKOMI_KENSU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customNumericTextBox_YOMIKOMI_KENSU.RegistCheckMethod")));
            this.customNumericTextBox_YOMIKOMI_KENSU.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.customNumericTextBox_YOMIKOMI_KENSU.Size = new System.Drawing.Size(80, 20);
            this.customNumericTextBox_YOMIKOMI_KENSU.TabIndex = 391;
            this.customNumericTextBox_YOMIKOMI_KENSU.Tag = "検索結果の総件数が表示されます";
            // 
            // M421HeaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.label_YOMOKOMI_KENSU);
            this.Controls.Add(this.customNumericTextBox_YOMIKOMI_KENSU);
            this.Location = new System.Drawing.Point(12, 6);
            this.Name = "M421HeaderForm";
            this.Text = "M421HeaderForm";
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.customNumericTextBox_YOMIKOMI_KENSU, 0);
            this.Controls.SetChildIndex(this.label_YOMOKOMI_KENSU, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_YOMOKOMI_KENSU;
        public r_framework.CustomControl.CustomNumericTextBox customNumericTextBox_YOMIKOMI_KENSU;
    }
}