namespace Shougun.Core.Master.CourseNyuryoku.APP
{
    partial class HeaderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HeaderForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.UPDATE_DATE = new r_framework.CustomControl.CustomDateInputTextBox();
            this.UPDATE_USER = new r_framework.CustomControl.CustomTextBox();
            this.CREATE_DATE = new r_framework.CustomControl.CustomDateInputTextBox();
            this.CREATE_USER = new r_framework.CustomControl.CustomTextBox();
            this.KYOTEN_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.KYOTEN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.TabIndex = 10;
            // 
            // lb_title
            // 
            this.lb_title.Size = new System.Drawing.Size(305, 34);
            this.lb_title.TabIndex = 11;
            // 
            // UPDATE_DATE
            // 
            this.UPDATE_DATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.UPDATE_DATE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UPDATE_DATE.DateFormat = r_framework.Const.DATE_FORMAT.NONE;
            this.UPDATE_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            this.UPDATE_DATE.DisplayPopUp = null;
            this.UPDATE_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_DATE.FocusOutCheckMethod")));
            this.UPDATE_DATE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.UPDATE_DATE.ForeColor = System.Drawing.Color.Black;
            this.UPDATE_DATE.IsInputErrorOccured = false;
            this.UPDATE_DATE.Location = new System.Drawing.Point(1008, 24);
            this.UPDATE_DATE.Name = "UPDATE_DATE";
            this.UPDATE_DATE.PopupAfterExecute = null;
            this.UPDATE_DATE.PopupBeforeExecute = null;
            this.UPDATE_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPDATE_DATE.PopupSearchSendParams")));
            this.UPDATE_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPDATE_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPDATE_DATE.popupWindowSetting")));
            this.UPDATE_DATE.ReadOnly = true;
            this.UPDATE_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_DATE.RegistCheckMethod")));
            this.UPDATE_DATE.Size = new System.Drawing.Size(160, 20);
            this.UPDATE_DATE.TabIndex = 23;
            this.UPDATE_DATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.UPDATE_DATE.Visible = false;
            this.UPDATE_DATE.TabStop = false;
            // 
            // UPDATE_USER
            // 
            this.UPDATE_USER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.UPDATE_USER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UPDATE_USER.DefaultBackColor = System.Drawing.Color.Empty;
            this.UPDATE_USER.DisplayPopUp = null;
            this.UPDATE_USER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_USER.FocusOutCheckMethod")));
            this.UPDATE_USER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.UPDATE_USER.ForeColor = System.Drawing.Color.Black;
            this.UPDATE_USER.IsInputErrorOccured = false;
            this.UPDATE_USER.Location = new System.Drawing.Point(849, 24);
            this.UPDATE_USER.Name = "UPDATE_USER";
            this.UPDATE_USER.PopupAfterExecute = null;
            this.UPDATE_USER.PopupBeforeExecute = null;
            this.UPDATE_USER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPDATE_USER.PopupSearchSendParams")));
            this.UPDATE_USER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPDATE_USER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPDATE_USER.popupWindowSetting")));
            this.UPDATE_USER.ReadOnly = true;
            this.UPDATE_USER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_USER.RegistCheckMethod")));
            this.UPDATE_USER.Size = new System.Drawing.Size(160, 20);
            this.UPDATE_USER.TabIndex = 22;
            this.UPDATE_USER.Visible = false;
            this.UPDATE_USER.TabStop = false;
            // 
            // CREATE_DATE
            // 
            this.CREATE_DATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CREATE_DATE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CREATE_DATE.DateFormat = r_framework.Const.DATE_FORMAT.NONE;
            this.CREATE_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            this.CREATE_DATE.DisplayPopUp = null;
            this.CREATE_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_DATE.FocusOutCheckMethod")));
            this.CREATE_DATE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CREATE_DATE.ForeColor = System.Drawing.Color.Black;
            this.CREATE_DATE.IsInputErrorOccured = false;
            this.CREATE_DATE.Location = new System.Drawing.Point(1008, 2);
            this.CREATE_DATE.Name = "CREATE_DATE";
            this.CREATE_DATE.PopupAfterExecute = null;
            this.CREATE_DATE.PopupBeforeExecute = null;
            this.CREATE_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CREATE_DATE.PopupSearchSendParams")));
            this.CREATE_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_DATE.popupWindowSetting")));
            this.CREATE_DATE.ReadOnly = true;
            this.CREATE_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_DATE.RegistCheckMethod")));
            this.CREATE_DATE.Size = new System.Drawing.Size(160, 20);
            this.CREATE_DATE.TabIndex = 20;
            this.CREATE_DATE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CREATE_DATE.Visible = false;
            this.CREATE_DATE.TabStop = false;
            // 
            // CREATE_USER
            // 
            this.CREATE_USER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CREATE_USER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CREATE_USER.DefaultBackColor = System.Drawing.Color.Empty;
            this.CREATE_USER.DisplayPopUp = null;
            this.CREATE_USER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_USER.FocusOutCheckMethod")));
            this.CREATE_USER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CREATE_USER.ForeColor = System.Drawing.Color.Black;
            this.CREATE_USER.IsInputErrorOccured = false;
            this.CREATE_USER.Location = new System.Drawing.Point(849, 2);
            this.CREATE_USER.Name = "CREATE_USER";
            this.CREATE_USER.PopupAfterExecute = null;
            this.CREATE_USER.PopupBeforeExecute = null;
            this.CREATE_USER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CREATE_USER.PopupSearchSendParams")));
            this.CREATE_USER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_USER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_USER.popupWindowSetting")));
            this.CREATE_USER.ReadOnly = true;
            this.CREATE_USER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_USER.RegistCheckMethod")));
            this.CREATE_USER.Size = new System.Drawing.Size(160, 20);
            this.CREATE_USER.TabIndex = 19;
            this.CREATE_USER.Visible = false;
            this.CREATE_USER.TabStop = false;
            // 
            // KYOTEN_NAME_RYAKU
            // 
            this.KYOTEN_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KYOTEN_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_NAME_RYAKU.DisplayPopUp = null;
            this.KYOTEN_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.FocusOutCheckMethod")));
            this.KYOTEN_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_NAME_RYAKU.IsInputErrorOccured = false;
            this.KYOTEN_NAME_RYAKU.Location = new System.Drawing.Point(562, 2);
            this.KYOTEN_NAME_RYAKU.Name = "KYOTEN_NAME_RYAKU";
            this.KYOTEN_NAME_RYAKU.PopupAfterExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupBeforeExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.PopupSearchSendParams")));
            this.KYOTEN_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOTEN_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.popupWindowSetting")));
            this.KYOTEN_NAME_RYAKU.ReadOnly = true;
            this.KYOTEN_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.RegistCheckMethod")));
            this.KYOTEN_NAME_RYAKU.Size = new System.Drawing.Size(160, 20);
            this.KYOTEN_NAME_RYAKU.TabIndex = 14;
            this.KYOTEN_NAME_RYAKU.TabStop = false;
            // 
            // KYOTEN_CD
            // 
            this.KYOTEN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.KYOTEN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_CD.CustomFormatSetting = "00";
            this.KYOTEN_CD.DBFieldsName = "KYOTEN_CD";
            this.KYOTEN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_CD.DisplayItemName = "拠点";
            this.KYOTEN_CD.DisplayPopUp = null;
            this.KYOTEN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.FocusOutCheckMethod")));
            this.KYOTEN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_CD.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_CD.FormatSetting = "カスタム";
            this.KYOTEN_CD.GetCodeMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.IsInputErrorOccured = false;
            this.KYOTEN_CD.ItemDefinedTypes = "smallint";
            this.KYOTEN_CD.Location = new System.Drawing.Point(533, 2);
            this.KYOTEN_CD.Name = "KYOTEN_CD";
            this.KYOTEN_CD.PopupAfterExecute = null;
            this.KYOTEN_CD.PopupAfterExecuteMethod = "KYOTEN_CD_TextChanged";
            this.KYOTEN_CD.PopupBeforeExecute = null;
            this.KYOTEN_CD.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_CD.PopupSearchSendParams")));
            this.KYOTEN_CD.PopupSetFormField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.KYOTEN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.KYOTEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_CD.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.KYOTEN_CD.RangeSetting = rangeSettingDto1;
            this.KYOTEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.RegistCheckMethod")));
            this.KYOTEN_CD.SetFormField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.Size = new System.Drawing.Size(30, 20);
            this.KYOTEN_CD.TabIndex = 13;
            this.KYOTEN_CD.Tag = "拠点を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.KYOTEN_CD.WordWrap = false;
            this.KYOTEN_CD.Leave += new System.EventHandler(this.KYOTEN_CD_Leave);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(418, 2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 20);
            this.label8.TabIndex = 12;
            this.label8.Text = "拠点※";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(734, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 18;
            this.label2.Text = "初回登録";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(734, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.TabIndex = 21;
            this.label3.Text = "最終更新";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Visible = false;
            // 
            // HeaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 45);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.UPDATE_USER);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.CREATE_DATE);
            this.Controls.Add(this.CREATE_USER);
            this.Controls.Add(this.KYOTEN_NAME_RYAKU);
            this.Controls.Add(this.KYOTEN_CD);
            this.Controls.Add(this.UPDATE_DATE);
            this.Name = "HeaderForm";
            this.Text = "HeaderForm";
            this.Controls.SetChildIndex(this.UPDATE_DATE, 0);
            this.Controls.SetChildIndex(this.KYOTEN_CD, 0);
            this.Controls.SetChildIndex(this.KYOTEN_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.CREATE_USER, 0);
            this.Controls.SetChildIndex(this.CREATE_DATE, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.UPDATE_USER, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        internal r_framework.CustomControl.CustomNumericTextBox2 KYOTEN_CD;
        internal r_framework.CustomControl.CustomTextBox KYOTEN_NAME_RYAKU;
        internal r_framework.CustomControl.CustomDateInputTextBox UPDATE_DATE;
        internal r_framework.CustomControl.CustomTextBox UPDATE_USER;
        internal r_framework.CustomControl.CustomDateInputTextBox CREATE_DATE;
        internal r_framework.CustomControl.CustomTextBox CREATE_USER;
    }
}