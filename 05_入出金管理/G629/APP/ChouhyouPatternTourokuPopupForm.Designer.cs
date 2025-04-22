namespace Shougun.Core.ReceiptPayManagement.ShukkinShuukeiChouhyou
{
    partial class ChouhyouPatternTourokuPopupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChouhyouPatternTourokuPopupForm));
            this.btnF9 = new r_framework.CustomControl.CustomButton();
            this.btnF12 = new r_framework.CustomControl.CustomButton();
            this.TITLE_LABEL = new System.Windows.Forms.Label();
            this.PATTERN_NAME = new r_framework.CustomControl.CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SHUUKEI_KOUMOKU_1 = new r_framework.CustomControl.CustomComboBox();
            this.SHUUKEI_FLAG_1 = new r_framework.CustomControl.CustomCheckBox();
            this.SHUUKEI_KOUMOKU_2 = new r_framework.CustomControl.CustomComboBox();
            this.SHUUKEI_FLAG_2 = new r_framework.CustomControl.CustomCheckBox();
            this.SuspendLayout();
            // 
            // btnF9
            // 
            this.btnF9.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnF9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnF9.Location = new System.Drawing.Point(10, 188);
            this.btnF9.Name = "btnF9";
            this.btnF9.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btnF9.Size = new System.Drawing.Size(80, 35);
            this.btnF9.TabIndex = 100;
            this.btnF9.Text = "[F9]\r\n登録";
            this.btnF9.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnF9.UseVisualStyleBackColor = true;
            this.btnF9.Click += new System.EventHandler(this.btnF9_Click);
            // 
            // btnF12
            // 
            this.btnF12.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnF12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnF12.Location = new System.Drawing.Point(276, 188);
            this.btnF12.Name = "btnF12";
            this.btnF12.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btnF12.Size = new System.Drawing.Size(80, 35);
            this.btnF12.TabIndex = 200;
            this.btnF12.Text = "[F12]\r\n閉じる";
            this.btnF12.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnF12.UseVisualStyleBackColor = true;
            this.btnF12.Click += new System.EventHandler(this.btnF12_Click);
            // 
            // TITLE_LABEL
            // 
            this.TITLE_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.TITLE_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TITLE_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 20F, System.Drawing.FontStyle.Bold);
            this.TITLE_LABEL.ForeColor = System.Drawing.Color.White;
            this.TITLE_LABEL.Location = new System.Drawing.Point(10, 8);
            this.TITLE_LABEL.Name = "TITLE_LABEL";
            this.TITLE_LABEL.Size = new System.Drawing.Size(359, 34);
            this.TITLE_LABEL.TabIndex = 9999;
            this.TITLE_LABEL.Text = "***集計表";
            this.TITLE_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PATTERN_NAME
            // 
            this.PATTERN_NAME.BackColor = System.Drawing.SystemColors.Window;
            this.PATTERN_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PATTERN_NAME.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.PATTERN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.PATTERN_NAME.DisplayPopUp = null;
            this.PATTERN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PATTERN_NAME.FocusOutCheckMethod")));
            this.PATTERN_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PATTERN_NAME.ForeColor = System.Drawing.Color.Black;
            this.PATTERN_NAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.PATTERN_NAME.IsInputErrorOccured = false;
            this.PATTERN_NAME.Location = new System.Drawing.Point(119, 63);
            this.PATTERN_NAME.MaxLength = 20;
            this.PATTERN_NAME.Name = "PATTERN_NAME";
            this.PATTERN_NAME.PopupAfterExecute = null;
            this.PATTERN_NAME.PopupBeforeExecute = null;
            this.PATTERN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PATTERN_NAME.PopupSearchSendParams")));
            this.PATTERN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PATTERN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("PATTERN_NAME.popupWindowSetting")));
            this.PATTERN_NAME.prevText = null;
            this.PATTERN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PATTERN_NAME.RegistCheckMethod")));
            this.PATTERN_NAME.Size = new System.Drawing.Size(250, 20);
            this.PATTERN_NAME.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(10, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 18);
            this.label1.TabIndex = 9999;
            this.label1.Text = "帳票名";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(10, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 18);
            this.label2.TabIndex = 9999;
            this.label2.Text = "集計項目";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SHUUKEI_KOUMOKU_1
            // 
            this.SHUUKEI_KOUMOKU_1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.SHUUKEI_KOUMOKU_1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SHUUKEI_KOUMOKU_1.BackColor = System.Drawing.SystemColors.Window;
            this.SHUUKEI_KOUMOKU_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUUKEI_KOUMOKU_1.DisplayPopUp = null;
            this.SHUUKEI_KOUMOKU_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SHUUKEI_KOUMOKU_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_KOUMOKU_1.FocusOutCheckMethod")));
            this.SHUUKEI_KOUMOKU_1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHUUKEI_KOUMOKU_1.FormattingEnabled = true;
            this.SHUUKEI_KOUMOKU_1.IsInputErrorOccured = false;
            this.SHUUKEI_KOUMOKU_1.Location = new System.Drawing.Point(119, 96);
            this.SHUUKEI_KOUMOKU_1.Name = "SHUUKEI_KOUMOKU_1";
            this.SHUUKEI_KOUMOKU_1.PopupAfterExecute = null;
            this.SHUUKEI_KOUMOKU_1.PopupBeforeExecute = null;
            this.SHUUKEI_KOUMOKU_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUUKEI_KOUMOKU_1.PopupSearchSendParams")));
            this.SHUUKEI_KOUMOKU_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUUKEI_KOUMOKU_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUUKEI_KOUMOKU_1.popupWindowSetting")));
            this.SHUUKEI_KOUMOKU_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_KOUMOKU_1.RegistCheckMethod")));
            this.SHUUKEI_KOUMOKU_1.Size = new System.Drawing.Size(104, 20);
            this.SHUUKEI_KOUMOKU_1.TabIndex = 20;
            this.SHUUKEI_KOUMOKU_1.Tag = "";
            this.SHUUKEI_KOUMOKU_1.SelectedIndexChanged += new System.EventHandler(this.SHUUKEI_KOUMOKU_1_TextChanged);
            this.SHUUKEI_KOUMOKU_1.Enter += new System.EventHandler(this.SHUUKEI_KOUMOKU_1_Enter);
            // 
            // SHUUKEI_FLAG_1
            // 
            this.SHUUKEI_FLAG_1.AutoSize = true;
            this.SHUUKEI_FLAG_1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.SHUUKEI_FLAG_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUUKEI_FLAG_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_FLAG_1.FocusOutCheckMethod")));
            this.SHUUKEI_FLAG_1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHUUKEI_FLAG_1.Location = new System.Drawing.Point(233, 98);
            this.SHUUKEI_FLAG_1.Name = "SHUUKEI_FLAG_1";
            this.SHUUKEI_FLAG_1.PopupAfterExecute = null;
            this.SHUUKEI_FLAG_1.PopupBeforeExecute = null;
            this.SHUUKEI_FLAG_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUUKEI_FLAG_1.PopupSearchSendParams")));
            this.SHUUKEI_FLAG_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUUKEI_FLAG_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUUKEI_FLAG_1.popupWindowSetting")));
            this.SHUUKEI_FLAG_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_FLAG_1.RegistCheckMethod")));
            this.SHUUKEI_FLAG_1.Size = new System.Drawing.Size(54, 17);
            this.SHUUKEI_FLAG_1.TabIndex = 21;
            this.SHUUKEI_FLAG_1.Text = "集計";
            this.SHUUKEI_FLAG_1.UseVisualStyleBackColor = false;
            // 
            // SHUUKEI_KOUMOKU_2
            // 
            this.SHUUKEI_KOUMOKU_2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.SHUUKEI_KOUMOKU_2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SHUUKEI_KOUMOKU_2.BackColor = System.Drawing.SystemColors.Window;
            this.SHUUKEI_KOUMOKU_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUUKEI_KOUMOKU_2.DisplayPopUp = null;
            this.SHUUKEI_KOUMOKU_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SHUUKEI_KOUMOKU_2.Enabled = false;
            this.SHUUKEI_KOUMOKU_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_KOUMOKU_2.FocusOutCheckMethod")));
            this.SHUUKEI_KOUMOKU_2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHUUKEI_KOUMOKU_2.FormattingEnabled = true;
            this.SHUUKEI_KOUMOKU_2.IsInputErrorOccured = false;
            this.SHUUKEI_KOUMOKU_2.Location = new System.Drawing.Point(119, 125);
            this.SHUUKEI_KOUMOKU_2.Name = "SHUUKEI_KOUMOKU_2";
            this.SHUUKEI_KOUMOKU_2.PopupAfterExecute = null;
            this.SHUUKEI_KOUMOKU_2.PopupBeforeExecute = null;
            this.SHUUKEI_KOUMOKU_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUUKEI_KOUMOKU_2.PopupSearchSendParams")));
            this.SHUUKEI_KOUMOKU_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUUKEI_KOUMOKU_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUUKEI_KOUMOKU_2.popupWindowSetting")));
            this.SHUUKEI_KOUMOKU_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_KOUMOKU_2.RegistCheckMethod")));
            this.SHUUKEI_KOUMOKU_2.Size = new System.Drawing.Size(104, 20);
            this.SHUUKEI_KOUMOKU_2.TabIndex = 30;
            this.SHUUKEI_KOUMOKU_2.Tag = "";
            this.SHUUKEI_KOUMOKU_2.SelectedIndexChanged += new System.EventHandler(this.SHUUKEI_KOUMOKU_2_TextChanged);
            this.SHUUKEI_KOUMOKU_2.Enter += new System.EventHandler(this.SHUUKEI_KOUMOKU_2_Enter);
            // 
            // SHUUKEI_FLAG_2
            // 
            this.SHUUKEI_FLAG_2.AutoSize = true;
            this.SHUUKEI_FLAG_2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.SHUUKEI_FLAG_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUUKEI_FLAG_2.Enabled = false;
            this.SHUUKEI_FLAG_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_FLAG_2.FocusOutCheckMethod")));
            this.SHUUKEI_FLAG_2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHUUKEI_FLAG_2.Location = new System.Drawing.Point(233, 127);
            this.SHUUKEI_FLAG_2.Name = "SHUUKEI_FLAG_2";
            this.SHUUKEI_FLAG_2.PopupAfterExecute = null;
            this.SHUUKEI_FLAG_2.PopupBeforeExecute = null;
            this.SHUUKEI_FLAG_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUUKEI_FLAG_2.PopupSearchSendParams")));
            this.SHUUKEI_FLAG_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUUKEI_FLAG_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUUKEI_FLAG_2.popupWindowSetting")));
            this.SHUUKEI_FLAG_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_FLAG_2.RegistCheckMethod")));
            this.SHUUKEI_FLAG_2.Size = new System.Drawing.Size(54, 17);
            this.SHUUKEI_FLAG_2.TabIndex = 31;
            this.SHUUKEI_FLAG_2.Text = "集計";
            this.SHUUKEI_FLAG_2.UseVisualStyleBackColor = false;
            // 
            // ChouhyouPatternTourokuPopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 234);
            this.Controls.Add(this.SHUUKEI_FLAG_2);
            this.Controls.Add(this.SHUUKEI_KOUMOKU_2);
            this.Controls.Add(this.SHUUKEI_FLAG_1);
            this.Controls.Add(this.SHUUKEI_KOUMOKU_1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PATTERN_NAME);
            this.Controls.Add(this.TITLE_LABEL);
            this.Controls.Add(this.btnF12);
            this.Controls.Add(this.btnF9);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChouhyouPatternTourokuPopupForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "帳票パターン登録";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ChouhyouPatternSentakuPopupForm_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private r_framework.CustomControl.CustomButton btnF9;
        private r_framework.CustomControl.CustomButton btnF12;
        public System.Windows.Forms.Label TITLE_LABEL;
        private r_framework.CustomControl.CustomTextBox PATTERN_NAME;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private r_framework.CustomControl.CustomComboBox SHUUKEI_KOUMOKU_1;
        private r_framework.CustomControl.CustomCheckBox SHUUKEI_FLAG_1;
        private r_framework.CustomControl.CustomComboBox SHUUKEI_KOUMOKU_2;
        private r_framework.CustomControl.CustomCheckBox SHUUKEI_FLAG_2;
    }
}