namespace Shougun.Core.Inspection.KenshuMeisaiNyuryoku
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
            this.KENSHU_DENPYOU_DATE = new r_framework.CustomControl.CustomDateTimePicker();
            this.KENSHU_DENPYOU_DATE_LBL = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Location = new System.Drawing.Point(1, 4);
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(77, 2);
            this.lb_title.Size = new System.Drawing.Size(405, 34);
            this.lb_title.Text = "検収入力";
            // 
            // KENSHU_DENPYOU_DATE
            // 
            this.KENSHU_DENPYOU_DATE.BackColor = System.Drawing.SystemColors.Window;
            this.KENSHU_DENPYOU_DATE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KENSHU_DENPYOU_DATE.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.KENSHU_DENPYOU_DATE.Checked = false;
            this.KENSHU_DENPYOU_DATE.CustomFormat = "yyyy/MM/dd(ddd)";
            this.KENSHU_DENPYOU_DATE.DateTimeNowYear = "";
            this.KENSHU_DENPYOU_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            this.KENSHU_DENPYOU_DATE.DisplayItemName = "検収伝票日付";
            this.KENSHU_DENPYOU_DATE.DisplayPopUp = null;
            this.KENSHU_DENPYOU_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KENSHU_DENPYOU_DATE.FocusOutCheckMethod")));
            this.KENSHU_DENPYOU_DATE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KENSHU_DENPYOU_DATE.ForeColor = System.Drawing.Color.Black;
            this.KENSHU_DENPYOU_DATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.KENSHU_DENPYOU_DATE.IsInputErrorOccured = false;
            this.KENSHU_DENPYOU_DATE.Location = new System.Drawing.Point(625, 11);
            this.KENSHU_DENPYOU_DATE.MaxLength = 10;
            this.KENSHU_DENPYOU_DATE.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.KENSHU_DENPYOU_DATE.Name = "KENSHU_DENPYOU_DATE";
            this.KENSHU_DENPYOU_DATE.NullValue = "";
            this.KENSHU_DENPYOU_DATE.PopupAfterExecute = null;
            this.KENSHU_DENPYOU_DATE.PopupBeforeExecute = null;
            this.KENSHU_DENPYOU_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KENSHU_DENPYOU_DATE.PopupSearchSendParams")));
            this.KENSHU_DENPYOU_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KENSHU_DENPYOU_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KENSHU_DENPYOU_DATE.popupWindowSetting")));
            this.KENSHU_DENPYOU_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KENSHU_DENPYOU_DATE.RegistCheckMethod")));
            this.KENSHU_DENPYOU_DATE.Size = new System.Drawing.Size(167, 20);
            this.KENSHU_DENPYOU_DATE.TabIndex = 390;
            this.KENSHU_DENPYOU_DATE.Tag = "検収伝票日付を指定してください";
            this.KENSHU_DENPYOU_DATE.Text = "2013/11/10(日)";
            this.KENSHU_DENPYOU_DATE.Value = new System.DateTime(2013, 11, 10, 0, 0, 0, 0);
            // 
            // KENSHU_DENPYOU_DATE_LBL
            // 
            this.KENSHU_DENPYOU_DATE_LBL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.KENSHU_DENPYOU_DATE_LBL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KENSHU_DENPYOU_DATE_LBL.ForeColor = System.Drawing.Color.White;
            this.KENSHU_DENPYOU_DATE_LBL.Location = new System.Drawing.Point(499, 11);
            this.KENSHU_DENPYOU_DATE_LBL.Name = "KENSHU_DENPYOU_DATE_LBL";
            this.KENSHU_DENPYOU_DATE_LBL.Size = new System.Drawing.Size(121, 20);
            this.KENSHU_DENPYOU_DATE_LBL.TabIndex = 389;
            this.KENSHU_DENPYOU_DATE_LBL.Text = "検収伝票日付※";
            this.KENSHU_DENPYOU_DATE_LBL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 46);
            this.Controls.Add(this.KENSHU_DENPYOU_DATE);
            this.Controls.Add(this.KENSHU_DENPYOU_DATE_LBL);
            this.Name = "UIHeader";
            this.Text = "UIHeader";
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.KENSHU_DENPYOU_DATE_LBL, 0);
            this.Controls.SetChildIndex(this.KENSHU_DENPYOU_DATE, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomDateTimePicker KENSHU_DENPYOU_DATE;
        public System.Windows.Forms.Label KENSHU_DENPYOU_DATE_LBL;


    }
}