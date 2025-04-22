namespace Shougun.Core.Billing.GetsujiShori
{
    partial class UIForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            this.lblDate = new System.Windows.Forms.Label();
            this.GETSUJI_DATE = new r_framework.CustomControl.CustomDateTimePicker();
            this.lblShimeStatus = new System.Windows.Forms.Label();
            this.SHIME_STATUS = new r_framework.CustomControl.CustomTextBox();
            this.SuspendLayout();
            // 
            // lblDate
            // 
            this.lblDate.BackColor = System.Drawing.Color.DarkGreen;
            this.lblDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblDate.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblDate.Location = new System.Drawing.Point(5, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(110, 20);
            this.lblDate.TabIndex = 1;
            this.lblDate.Text = "月次年月";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GETSUJI_DATE
            // 
            this.GETSUJI_DATE.BackColor = System.Drawing.SystemColors.Window;
            this.GETSUJI_DATE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GETSUJI_DATE.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.GETSUJI_DATE.Checked = false;
            this.GETSUJI_DATE.CustomFormat = "yyyy/MM";
            this.GETSUJI_DATE.DateTimeNowYear = "";
            this.GETSUJI_DATE.DBFieldsName = "";
            this.GETSUJI_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            this.GETSUJI_DATE.DisplayItemName = "月次年月";
            this.GETSUJI_DATE.DisplayPopUp = null;
            this.GETSUJI_DATE.Enabled = false;
            this.GETSUJI_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GETSUJI_DATE.FocusOutCheckMethod")));
            this.GETSUJI_DATE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GETSUJI_DATE.ForeColor = System.Drawing.Color.Black;
            this.GETSUJI_DATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.GETSUJI_DATE.IsInputErrorOccured = false;
            this.GETSUJI_DATE.ItemDefinedTypes = "datetime";
            this.GETSUJI_DATE.Location = new System.Drawing.Point(120, 0);
            this.GETSUJI_DATE.MaxLength = 10;
            this.GETSUJI_DATE.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.GETSUJI_DATE.Name = "GETSUJI_DATE";
            this.GETSUJI_DATE.NullValue = "";
            this.GETSUJI_DATE.PopupAfterExecute = null;
            this.GETSUJI_DATE.PopupBeforeExecute = null;
            this.GETSUJI_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GETSUJI_DATE.PopupSearchSendParams")));
            this.GETSUJI_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GETSUJI_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GETSUJI_DATE.popupWindowSetting")));
            this.GETSUJI_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GETSUJI_DATE.RegistCheckMethod")));
            this.GETSUJI_DATE.ShowYoubi = false;
            this.GETSUJI_DATE.Size = new System.Drawing.Size(110, 20);
            this.GETSUJI_DATE.TabIndex = 5;
            this.GETSUJI_DATE.Tag = "月次処理対象年月が表示されます";
            this.GETSUJI_DATE.Text = "2014/12/01(月)";
            this.GETSUJI_DATE.Value = new System.DateTime(2014, 12, 1, 0, 0, 0, 0);
            // 
            // lblShimeStatus
            // 
            this.lblShimeStatus.BackColor = System.Drawing.Color.DarkGreen;
            this.lblShimeStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblShimeStatus.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblShimeStatus.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblShimeStatus.Location = new System.Drawing.Point(260, 0);
            this.lblShimeStatus.Name = "lblShimeStatus";
            this.lblShimeStatus.Size = new System.Drawing.Size(110, 20);
            this.lblShimeStatus.TabIndex = 6;
            this.lblShimeStatus.Text = "締状態";
            this.lblShimeStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SHIME_STATUS
            // 
            this.SHIME_STATUS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.SHIME_STATUS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHIME_STATUS.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.SHIME_STATUS.DBFieldsName = "";
            this.SHIME_STATUS.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHIME_STATUS.DisplayItemName = "ステータス";
            this.SHIME_STATUS.DisplayPopUp = null;
            this.SHIME_STATUS.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHIME_STATUS.FocusOutCheckMethod")));
            this.SHIME_STATUS.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHIME_STATUS.ForeColor = System.Drawing.Color.Black;
            this.SHIME_STATUS.IsInputErrorOccured = false;
            this.SHIME_STATUS.ItemDefinedTypes = "varchar";
            this.SHIME_STATUS.Location = new System.Drawing.Point(375, 0);
            this.SHIME_STATUS.MaxLength = 0;
            this.SHIME_STATUS.Name = "SHIME_STATUS";
            this.SHIME_STATUS.PopupAfterExecute = null;
            this.SHIME_STATUS.PopupBeforeExecute = null;
            this.SHIME_STATUS.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHIME_STATUS.PopupSearchSendParams")));
            this.SHIME_STATUS.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHIME_STATUS.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHIME_STATUS.popupWindowSetting")));
            this.SHIME_STATUS.prevText = null;
            this.SHIME_STATUS.ReadOnly = true;
            this.SHIME_STATUS.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHIME_STATUS.RegistCheckMethod")));
            this.SHIME_STATUS.Size = new System.Drawing.Size(110, 20);
            this.SHIME_STATUS.TabIndex = 16;
            this.SHIME_STATUS.TabStop = false;
            this.SHIME_STATUS.Tag = " ";
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 475);
            this.Controls.Add(this.SHIME_STATUS);
            this.Controls.Add(this.lblShimeStatus);
            this.Controls.Add(this.GETSUJI_DATE);
            this.Controls.Add(this.lblDate);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "UIForm";
            this.Text = "UIForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lblDate;
        internal r_framework.CustomControl.CustomDateTimePicker GETSUJI_DATE;
        internal System.Windows.Forms.Label lblShimeStatus;
        internal r_framework.CustomControl.CustomTextBox SHIME_STATUS;
    }
}