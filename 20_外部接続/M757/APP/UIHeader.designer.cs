namespace Shougun.Core.ExternalConnection.RakurakuMasutaIchiran.APP
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
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.ARI_KBN_1 = new r_framework.CustomControl.CustomRadioButton();
            this.NASHI_KBN_2 = new r_framework.CustomControl.CustomRadioButton();
            this.customPanel_Mod = new r_framework.CustomControl.CustomPanel();
            this.RAKURAKU_MEISAI_RENKEI = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label200 = new System.Windows.Forms.Label();
            this.customPanel_Mod.SuspendLayout();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Location = new System.Drawing.Point(1, 2);
            this.windowTypeLabel.Size = new System.Drawing.Size(25, 29);
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(32, 2);
            this.lb_title.Size = new System.Drawing.Size(450, 34);
            // 
            // ARI_KBN_1
            // 
            this.ARI_KBN_1.AutoSize = true;
            this.ARI_KBN_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.ARI_KBN_1.FocusOutCheckMethod = null;
            this.ARI_KBN_1.LinkedTextBox = "RAKURAKU_MEISAI_RENKEI";
            this.ARI_KBN_1.Location = new System.Drawing.Point(12, 1);
            this.ARI_KBN_1.Name = "ARI_KBN_1";
            this.ARI_KBN_1.PopupAfterExecute = null;
            this.ARI_KBN_1.PopupBeforeExecute = null;
            this.ARI_KBN_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ARI_KBN_1.popupWindowSetting = null;
            this.ARI_KBN_1.RegistCheckMethod = null;
            this.ARI_KBN_1.Size = new System.Drawing.Size(74, 17);
            this.ARI_KBN_1.TabIndex = 389;
            this.ARI_KBN_1.Tag = " ";
            this.ARI_KBN_1.Text = "1. 有り";
            this.ARI_KBN_1.UseVisualStyleBackColor = true;
            this.ARI_KBN_1.Value = "1";
            // 
            // NASHI_KBN_2
            // 
            this.NASHI_KBN_2.AutoSize = true;
            this.NASHI_KBN_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.NASHI_KBN_2.FocusOutCheckMethod = null;
            this.NASHI_KBN_2.LinkedTextBox = "RAKURAKU_MEISAI_RENKEI";
            this.NASHI_KBN_2.Location = new System.Drawing.Point(114, 1);
            this.NASHI_KBN_2.Name = "NASHI_KBN_2";
            this.NASHI_KBN_2.PopupAfterExecute = null;
            this.NASHI_KBN_2.PopupBeforeExecute = null;
            this.NASHI_KBN_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NASHI_KBN_2.popupWindowSetting = null;
            this.NASHI_KBN_2.RegistCheckMethod = null;
            this.NASHI_KBN_2.Size = new System.Drawing.Size(74, 17);
            this.NASHI_KBN_2.TabIndex = 390;
            this.NASHI_KBN_2.Tag = " ";
            this.NASHI_KBN_2.Text = "2. 無し";
            this.NASHI_KBN_2.UseVisualStyleBackColor = true;
            this.NASHI_KBN_2.Value = "2";
            // 
            // customPanel_Mod
            // 
            this.customPanel_Mod.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel_Mod.Controls.Add(this.ARI_KBN_1);
            this.customPanel_Mod.Controls.Add(this.NASHI_KBN_2);
            this.customPanel_Mod.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.customPanel_Mod.Location = new System.Drawing.Point(714, 2);
            this.customPanel_Mod.Name = "customPanel_Mod";
            this.customPanel_Mod.Size = new System.Drawing.Size(223, 20);
            this.customPanel_Mod.TabIndex = 391;
            // 
            // RAKURAKU_MEISAI_RENKEI
            // 
            this.RAKURAKU_MEISAI_RENKEI.BackColor = System.Drawing.SystemColors.Window;
            this.RAKURAKU_MEISAI_RENKEI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RAKURAKU_MEISAI_RENKEI.CharacterLimitList = new char[] {
        '1',
        '2'};
            this.RAKURAKU_MEISAI_RENKEI.DBFieldsName = "RAKURAKU_MEISAI_RENKEI";
            this.RAKURAKU_MEISAI_RENKEI.DefaultBackColor = System.Drawing.Color.Empty;
            this.RAKURAKU_MEISAI_RENKEI.DisplayItemName = "楽楽明細連携";
            this.RAKURAKU_MEISAI_RENKEI.DisplayPopUp = null;
            this.RAKURAKU_MEISAI_RENKEI.FocusOutCheckMethod = null;
            this.RAKURAKU_MEISAI_RENKEI.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.RAKURAKU_MEISAI_RENKEI.ForeColor = System.Drawing.Color.Black;
            this.RAKURAKU_MEISAI_RENKEI.IsInputErrorOccured = false;
            this.RAKURAKU_MEISAI_RENKEI.ItemDefinedTypes = "smallint";
            this.RAKURAKU_MEISAI_RENKEI.LinkedRadioButtonArray = new string[] {
        "ARI_KBN_1",
        "NASHI_KBN_2"};
            this.RAKURAKU_MEISAI_RENKEI.Location = new System.Drawing.Point(690, 2);
            this.RAKURAKU_MEISAI_RENKEI.Name = "RAKURAKU_MEISAI_RENKEI";
            this.RAKURAKU_MEISAI_RENKEI.PopupAfterExecute = null;
            this.RAKURAKU_MEISAI_RENKEI.PopupBeforeExecute = null;
            this.RAKURAKU_MEISAI_RENKEI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.RAKURAKU_MEISAI_RENKEI.popupWindowSetting = null;
            rangeSettingDto1.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RAKURAKU_MEISAI_RENKEI.RangeSetting = rangeSettingDto1;
            this.RAKURAKU_MEISAI_RENKEI.RegistCheckMethod = null;
            this.RAKURAKU_MEISAI_RENKEI.Size = new System.Drawing.Size(25, 20);
            this.RAKURAKU_MEISAI_RENKEI.TabIndex = 0;
            this.RAKURAKU_MEISAI_RENKEI.Tag = "【1、2】のいずれかで入力してください";
            this.RAKURAKU_MEISAI_RENKEI.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.RAKURAKU_MEISAI_RENKEI.WordWrap = false;
            // 
            // label200
            // 
            this.label200.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label200.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label200.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label200.ForeColor = System.Drawing.Color.White;
            this.label200.Location = new System.Drawing.Point(554, 2);
            this.label200.Name = "label200";
            this.label200.Size = new System.Drawing.Size(130, 20);
            this.label200.TabIndex = 451;
            this.label200.Text = "楽楽明細連携";
            this.label200.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1149, 47);
            this.Controls.Add(this.label200);
            this.Controls.Add(this.RAKURAKU_MEISAI_RENKEI);
            this.Controls.Add(this.customPanel_Mod);
            this.Name = "UIHeader";
            this.Text = "UIHeader";
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.customPanel_Mod, 0);
            this.Controls.SetChildIndex(this.RAKURAKU_MEISAI_RENKEI, 0);
            this.Controls.SetChildIndex(this.label200, 0);
            this.customPanel_Mod.ResumeLayout(false);
            this.customPanel_Mod.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private r_framework.CustomControl.CustomRadioButton ARI_KBN_1;
        private r_framework.CustomControl.CustomRadioButton NASHI_KBN_2;
        private r_framework.CustomControl.CustomPanel customPanel_Mod;
        private System.Windows.Forms.Label label200;
        public r_framework.CustomControl.CustomNumericTextBox2 RAKURAKU_MEISAI_RENKEI;
    }
}