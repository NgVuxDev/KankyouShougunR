namespace Shougun.Core.Master.OboeGakiIkkatuIchiran
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
            this.lbl_YOMOKOMI_KENSU = new System.Windows.Forms.Label();
            this.txt_YOMIKOMI_KENSU = new r_framework.CustomControl.CustomNumericTextBox2();
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
            // lbl_YOMOKOMI_KENSU
            // 
            this.lbl_YOMOKOMI_KENSU.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_YOMOKOMI_KENSU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_YOMOKOMI_KENSU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_YOMOKOMI_KENSU.ForeColor = System.Drawing.Color.White;
            this.lbl_YOMOKOMI_KENSU.Location = new System.Drawing.Point(955, 3);
            this.lbl_YOMOKOMI_KENSU.Name = "lbl_YOMOKOMI_KENSU";
            this.lbl_YOMOKOMI_KENSU.Size = new System.Drawing.Size(110, 20);
            this.lbl_YOMOKOMI_KENSU.TabIndex = 389;
            this.lbl_YOMOKOMI_KENSU.Text = "読込データ件数";
            this.lbl_YOMOKOMI_KENSU.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_YOMIKOMI_KENSU
            // 
            this.txt_YOMIKOMI_KENSU.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_YOMIKOMI_KENSU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txt_YOMIKOMI_KENSU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_YOMIKOMI_KENSU.CustomFormatSetting = "#,##0";
            this.txt_YOMIKOMI_KENSU.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_YOMIKOMI_KENSU.DisplayPopUp = null;
            this.txt_YOMIKOMI_KENSU.FocusOutCheckMethod = null;
            this.txt_YOMIKOMI_KENSU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txt_YOMIKOMI_KENSU.ForeColor = System.Drawing.Color.Black;
            this.txt_YOMIKOMI_KENSU.FormatSetting = "カスタム";
            this.txt_YOMIKOMI_KENSU.IsInputErrorOccured = false;
            this.txt_YOMIKOMI_KENSU.LinkedRadioButtonArray = new string[0];
            this.txt_YOMIKOMI_KENSU.Location = new System.Drawing.Point(1070, 3);
            this.txt_YOMIKOMI_KENSU.Name = "txt_YOMIKOMI_KENSU";
            this.txt_YOMIKOMI_KENSU.PopupAfterExecute = null;
            this.txt_YOMIKOMI_KENSU.PopupBeforeExecute = null;
            this.txt_YOMIKOMI_KENSU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_YOMIKOMI_KENSU.PopupSearchSendParams")));
            this.txt_YOMIKOMI_KENSU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_YOMIKOMI_KENSU.popupWindowSetting = null;
            this.txt_YOMIKOMI_KENSU.prevText = null;
            this.txt_YOMIKOMI_KENSU.PrevText = null;
            this.txt_YOMIKOMI_KENSU.RangeSetting = rangeSettingDto1;
            this.txt_YOMIKOMI_KENSU.ReadOnly = true;
            this.txt_YOMIKOMI_KENSU.RegistCheckMethod = null;
            this.txt_YOMIKOMI_KENSU.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txt_YOMIKOMI_KENSU.Size = new System.Drawing.Size(80, 20);
            this.txt_YOMIKOMI_KENSU.TabIndex = 391;
            this.txt_YOMIKOMI_KENSU.Tag = "検索結果の総件数が表示されます";
            this.txt_YOMIKOMI_KENSU.WordWrap = false;
            // 
            // M421HeaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.lbl_YOMOKOMI_KENSU);
            this.Controls.Add(this.txt_YOMIKOMI_KENSU);
            this.Location = new System.Drawing.Point(12, 6);
            this.Name = "M421HeaderForm";
            this.Text = "M421HeaderForm";
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.txt_YOMIKOMI_KENSU, 0);
            this.Controls.SetChildIndex(this.lbl_YOMOKOMI_KENSU, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_YOMOKOMI_KENSU;
        public r_framework.CustomControl.CustomNumericTextBox2 txt_YOMIKOMI_KENSU;
    }
}