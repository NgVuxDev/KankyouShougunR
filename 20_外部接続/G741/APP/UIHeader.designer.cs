namespace Shougun.Core.ExternalConnection.GenbamemoNyuryoku
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
            this.HIHYOUJI = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.HIHYOUJI_DATE = new r_framework.CustomControl.CustomNumericTextBox2();
            this.HIHYOUJI_DATE_LABEL = new System.Windows.Forms.Label();
            this.HIHYOUJI_TOUROKUSHA_NAME = new r_framework.CustomControl.CustomNumericTextBox2();
            this.HIHYOUJI_TOUROKUSHA_NAME_LABEL = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Location = new System.Drawing.Point(1, 8);
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(77, 1);
            this.lb_title.Size = new System.Drawing.Size(240, 34);
            // 
            // HIHYOUJI
            // 
            this.HIHYOUJI.AutoSize = true;
            this.HIHYOUJI.Location = new System.Drawing.Point(455, 20);
            this.HIHYOUJI.Name = "HIHYOUJI";
            this.HIHYOUJI.Size = new System.Drawing.Size(15, 14);
            this.HIHYOUJI.TabIndex = 389;
            this.HIHYOUJI.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Red;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(367, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 20);
            this.label2.TabIndex = 390;
            this.label2.Text = "非表示";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HIHYOUJI_DATE
            // 
            this.HIHYOUJI_DATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.HIHYOUJI_DATE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HIHYOUJI_DATE.DBFieldsName = "HIHYOUJI_DATE";
            this.HIHYOUJI_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            this.HIHYOUJI_DATE.DisplayItemName = "";
            this.HIHYOUJI_DATE.DisplayPopUp = null;
            this.HIHYOUJI_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIHYOUJI_DATE.FocusOutCheckMethod")));
            this.HIHYOUJI_DATE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HIHYOUJI_DATE.ForeColor = System.Drawing.Color.Black;
            this.HIHYOUJI_DATE.IsInputErrorOccured = false;
            this.HIHYOUJI_DATE.Location = new System.Drawing.Point(843, 15);
            this.HIHYOUJI_DATE.MaxLength = 10;
            this.HIHYOUJI_DATE.Name = "HIHYOUJI_DATE";
            this.HIHYOUJI_DATE.PopupAfterExecute = null;
            this.HIHYOUJI_DATE.PopupBeforeExecute = null;
            this.HIHYOUJI_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIHYOUJI_DATE.PopupSearchSendParams")));
            this.HIHYOUJI_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIHYOUJI_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HIHYOUJI_DATE.popupWindowSetting")));
            this.HIHYOUJI_DATE.ReadOnly = true;
            this.HIHYOUJI_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIHYOUJI_DATE.RegistCheckMethod")));
            this.HIHYOUJI_DATE.Size = new System.Drawing.Size(144, 20);
            this.HIHYOUJI_DATE.TabIndex = 781;
            this.HIHYOUJI_DATE.Tag = "";
            this.HIHYOUJI_DATE.WordWrap = false;
            // 
            // HIHYOUJI_DATE_LABEL
            // 
            this.HIHYOUJI_DATE_LABEL.BackColor = System.Drawing.Color.Red;
            this.HIHYOUJI_DATE_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HIHYOUJI_DATE_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HIHYOUJI_DATE_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HIHYOUJI_DATE_LABEL.ForeColor = System.Drawing.Color.White;
            this.HIHYOUJI_DATE_LABEL.Location = new System.Drawing.Point(762, 15);
            this.HIHYOUJI_DATE_LABEL.Name = "HIHYOUJI_DATE_LABEL";
            this.HIHYOUJI_DATE_LABEL.Size = new System.Drawing.Size(79, 20);
            this.HIHYOUJI_DATE_LABEL.TabIndex = 780;
            this.HIHYOUJI_DATE_LABEL.Text = "非表示日";
            this.HIHYOUJI_DATE_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HIHYOUJI_TOUROKUSHA_NAME
            // 
            this.HIHYOUJI_TOUROKUSHA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.HIHYOUJI_TOUROKUSHA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HIHYOUJI_TOUROKUSHA_NAME.DBFieldsName = "HIHYOUJI_TOUROKUSHA_NAME";
            this.HIHYOUJI_TOUROKUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.HIHYOUJI_TOUROKUSHA_NAME.DisplayItemName = "";
            this.HIHYOUJI_TOUROKUSHA_NAME.DisplayPopUp = null;
            this.HIHYOUJI_TOUROKUSHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIHYOUJI_TOUROKUSHA_NAME.FocusOutCheckMethod")));
            this.HIHYOUJI_TOUROKUSHA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HIHYOUJI_TOUROKUSHA_NAME.ForeColor = System.Drawing.Color.Black;
            this.HIHYOUJI_TOUROKUSHA_NAME.IsInputErrorOccured = false;
            this.HIHYOUJI_TOUROKUSHA_NAME.Location = new System.Drawing.Point(602, 15);
            this.HIHYOUJI_TOUROKUSHA_NAME.Name = "HIHYOUJI_TOUROKUSHA_NAME";
            this.HIHYOUJI_TOUROKUSHA_NAME.PopupAfterExecute = null;
            this.HIHYOUJI_TOUROKUSHA_NAME.PopupBeforeExecute = null;
            this.HIHYOUJI_TOUROKUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIHYOUJI_TOUROKUSHA_NAME.PopupSearchSendParams")));
            this.HIHYOUJI_TOUROKUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIHYOUJI_TOUROKUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HIHYOUJI_TOUROKUSHA_NAME.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            1316134911,
            2328,
            0,
            0});
            this.HIHYOUJI_TOUROKUSHA_NAME.RangeSetting = rangeSettingDto1;
            this.HIHYOUJI_TOUROKUSHA_NAME.ReadOnly = true;
            this.HIHYOUJI_TOUROKUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIHYOUJI_TOUROKUSHA_NAME.RegistCheckMethod")));
            this.HIHYOUJI_TOUROKUSHA_NAME.Size = new System.Drawing.Size(144, 20);
            this.HIHYOUJI_TOUROKUSHA_NAME.TabIndex = 783;
            this.HIHYOUJI_TOUROKUSHA_NAME.Tag = "";
            this.HIHYOUJI_TOUROKUSHA_NAME.WordWrap = false;
            // 
            // HIHYOUJI_TOUROKUSHA_NAME_LABEL
            // 
            this.HIHYOUJI_TOUROKUSHA_NAME_LABEL.BackColor = System.Drawing.Color.Red;
            this.HIHYOUJI_TOUROKUSHA_NAME_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HIHYOUJI_TOUROKUSHA_NAME_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HIHYOUJI_TOUROKUSHA_NAME_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HIHYOUJI_TOUROKUSHA_NAME_LABEL.ForeColor = System.Drawing.Color.White;
            this.HIHYOUJI_TOUROKUSHA_NAME_LABEL.Location = new System.Drawing.Point(496, 15);
            this.HIHYOUJI_TOUROKUSHA_NAME_LABEL.Name = "HIHYOUJI_TOUROKUSHA_NAME_LABEL";
            this.HIHYOUJI_TOUROKUSHA_NAME_LABEL.Size = new System.Drawing.Size(103, 20);
            this.HIHYOUJI_TOUROKUSHA_NAME_LABEL.TabIndex = 782;
            this.HIHYOUJI_TOUROKUSHA_NAME_LABEL.Text = "非表示登録者";
            this.HIHYOUJI_TOUROKUSHA_NAME_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 42);
            this.Controls.Add(this.HIHYOUJI_TOUROKUSHA_NAME);
            this.Controls.Add(this.HIHYOUJI_TOUROKUSHA_NAME_LABEL);
            this.Controls.Add(this.HIHYOUJI_DATE);
            this.Controls.Add(this.HIHYOUJI_DATE_LABEL);
            this.Controls.Add(this.HIHYOUJI);
            this.Controls.Add(this.label2);
            this.Name = "UIHeader";
            this.Text = "UIHeader";
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.HIHYOUJI, 0);
            this.Controls.SetChildIndex(this.HIHYOUJI_DATE_LABEL, 0);
            this.Controls.SetChildIndex(this.HIHYOUJI_DATE, 0);
            this.Controls.SetChildIndex(this.HIHYOUJI_TOUROKUSHA_NAME_LABEL, 0);
            this.Controls.SetChildIndex(this.HIHYOUJI_TOUROKUSHA_NAME, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.CheckBox HIHYOUJI;
        internal System.Windows.Forms.Label label2;
        internal r_framework.CustomControl.CustomNumericTextBox2 HIHYOUJI_DATE;
        internal System.Windows.Forms.Label HIHYOUJI_DATE_LABEL;
        internal r_framework.CustomControl.CustomNumericTextBox2 HIHYOUJI_TOUROKUSHA_NAME;
        internal System.Windows.Forms.Label HIHYOUJI_TOUROKUSHA_NAME_LABEL;


    }
}