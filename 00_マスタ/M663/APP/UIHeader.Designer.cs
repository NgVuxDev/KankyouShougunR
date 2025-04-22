namespace Shougun.Core.Master.CourseIchiran
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
            this.LBL_ALERT = new System.Windows.Forms.Label();
            this.ALERT_CNT = new r_framework.CustomControl.CustomNumericTextBox2();
            this.LBL_SEARCH = new System.Windows.Forms.Label();
            this.SEARCH_CNT = new r_framework.CustomControl.CustomNumericTextBox2();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(0, 6);
            this.lb_title.Size = new System.Drawing.Size(267, 35);
            this.lb_title.Text = "コース一覧";
            // 
            // LBL_ALERT
            // 
            this.LBL_ALERT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.LBL_ALERT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LBL_ALERT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LBL_ALERT.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.LBL_ALERT.ForeColor = System.Drawing.Color.White;
            this.LBL_ALERT.Location = new System.Drawing.Point(1004, 4);
            this.LBL_ALERT.Name = "LBL_ALERT";
            this.LBL_ALERT.Size = new System.Drawing.Size(110, 20);
            this.LBL_ALERT.TabIndex = 389;
            this.LBL_ALERT.Text = "アラート件数";
            this.LBL_ALERT.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ALERT_CNT
            // 
            this.ALERT_CNT.BackColor = System.Drawing.SystemColors.Window;
            this.ALERT_CNT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ALERT_CNT.ChangeUpperCase = true;
            this.ALERT_CNT.CharacterLimitList = null;
            this.ALERT_CNT.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.ALERT_CNT.DefaultBackColor = System.Drawing.Color.Empty;
            this.ALERT_CNT.DisplayPopUp = null;
            this.ALERT_CNT.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ALERT_CNT.FocusOutCheckMethod")));
            this.ALERT_CNT.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ALERT_CNT.ForeColor = System.Drawing.Color.Black;
            this.ALERT_CNT.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ALERT_CNT.IsInputErrorOccured = false;
            this.ALERT_CNT.Location = new System.Drawing.Point(1113, 4);
            this.ALERT_CNT.MaxLength = 6;
            this.ALERT_CNT.Name = "ALERT_CNT";
            this.ALERT_CNT.PopupAfterExecute = null;
            this.ALERT_CNT.PopupAfterExecuteMethod = "";
            this.ALERT_CNT.PopupBeforeExecute = null;
            this.ALERT_CNT.PopupGetMasterField = "";
            this.ALERT_CNT.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ALERT_CNT.PopupSearchSendParams")));
            this.ALERT_CNT.PopupSendParams = new string[0];
            this.ALERT_CNT.PopupSetFormField = "COURSE_NAME_CD,COURSE_NAME";
            this.ALERT_CNT.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ALERT_CNT.PopupWindowName = "";
            this.ALERT_CNT.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ALERT_CNT.popupWindowSetting")));
            this.ALERT_CNT.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ALERT_CNT.RegistCheckMethod")));
            this.ALERT_CNT.SetFormField = "";
            this.ALERT_CNT.Size = new System.Drawing.Size(60, 20);
            this.ALERT_CNT.TabIndex = 390;
            this.ALERT_CNT.Tag = "検索結果の総件数でアラートメッセージを表示させたい上限数を入力してください";
            this.ALERT_CNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ALERT_CNT.ZeroPaddengFlag = true;
            // 
            // LBL_SEARCH
            // 
            this.LBL_SEARCH.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.LBL_SEARCH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LBL_SEARCH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LBL_SEARCH.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.LBL_SEARCH.ForeColor = System.Drawing.Color.White;
            this.LBL_SEARCH.Location = new System.Drawing.Point(1004, 26);
            this.LBL_SEARCH.Name = "LBL_SEARCH";
            this.LBL_SEARCH.Size = new System.Drawing.Size(110, 20);
            this.LBL_SEARCH.TabIndex = 391;
            this.LBL_SEARCH.Text = "読込データ件数";
            this.LBL_SEARCH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SEARCH_CNT
            // 
            this.SEARCH_CNT.BackColor = System.Drawing.SystemColors.Window;
            this.SEARCH_CNT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SEARCH_CNT.ChangeUpperCase = true;
            this.SEARCH_CNT.CharacterLimitList = null;
            this.SEARCH_CNT.DefaultBackColor = System.Drawing.Color.Empty;
            this.SEARCH_CNT.DisplayPopUp = null;
            this.SEARCH_CNT.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEARCH_CNT.FocusOutCheckMethod")));
            this.SEARCH_CNT.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SEARCH_CNT.ForeColor = System.Drawing.Color.Black;
            this.SEARCH_CNT.GetCodeMasterField = "";
            this.SEARCH_CNT.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SEARCH_CNT.IsInputErrorOccured = false;
            this.SEARCH_CNT.Location = new System.Drawing.Point(1113, 26);
            this.SEARCH_CNT.MaxLength = 6;
            this.SEARCH_CNT.Name = "SEARCH_CNT";
            this.SEARCH_CNT.PopupAfterExecute = null;
            this.SEARCH_CNT.PopupAfterExecuteMethod = "";
            this.SEARCH_CNT.PopupBeforeExecute = null;
            this.SEARCH_CNT.PopupGetMasterField = "";
            this.SEARCH_CNT.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEARCH_CNT.PopupSearchSendParams")));
            this.SEARCH_CNT.PopupSendParams = new string[0];
            this.SEARCH_CNT.PopupSetFormField = "";
            this.SEARCH_CNT.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SEARCH_CNT.PopupWindowName = "";
            this.SEARCH_CNT.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEARCH_CNT.popupWindowSetting")));
            this.SEARCH_CNT.ReadOnly = true;
            this.SEARCH_CNT.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEARCH_CNT.RegistCheckMethod")));
            this.SEARCH_CNT.SetFormField = "";
            this.SEARCH_CNT.Size = new System.Drawing.Size(60, 20);
            this.SEARCH_CNT.TabIndex = 392;
            this.SEARCH_CNT.Tag = "検索結果の総件数が表示します";
            this.SEARCH_CNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.SEARCH_CNT.ZeroPaddengFlag = true;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.LBL_SEARCH);
            this.Controls.Add(this.SEARCH_CNT);
            this.Controls.Add(this.LBL_ALERT);
            this.Controls.Add(this.ALERT_CNT);
            this.Name = "UIHeader";
            this.Text = "コース一覧";
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.ALERT_CNT, 0);
            this.Controls.SetChildIndex(this.LBL_ALERT, 0);
            this.Controls.SetChildIndex(this.SEARCH_CNT, 0);
            this.Controls.SetChildIndex(this.LBL_SEARCH, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label LBL_ALERT;
        internal r_framework.CustomControl.CustomNumericTextBox2 ALERT_CNT;
        internal System.Windows.Forms.Label LBL_SEARCH;
        internal r_framework.CustomControl.CustomNumericTextBox2 SEARCH_CNT;

    }
}