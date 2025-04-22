namespace DenManiJigyoushaIchiran.APP
{
    partial class DenManiJigyoushaIchiranForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DenManiJigyoushaIchiranForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.JIGYOUSHA_KBN_3 = new r_framework.CustomControl.CustomRadioButton();
            this.JIGYOUSHA_KBN_2 = new r_framework.CustomControl.CustomRadioButton();
            this.JIGYOUSHA_KBN_1 = new r_framework.CustomControl.CustomRadioButton();
            this.JIGYOUSHA_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label16 = new System.Windows.Forms.Label();
            this.JIGYOUSHA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.JIGYOUSHA_ADDRESS = new r_framework.CustomControl.CustomTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.searchString.Location = new System.Drawing.Point(3, 3);
            this.searchString.ReadOnly = true;
            this.searchString.Size = new System.Drawing.Size(595, 123);
            this.searchString.TabIndex = 104;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Location = new System.Drawing.Point(4, 427);
            this.bt_ptn1.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn1.TabIndex = 201;
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn2.TabIndex = 202;
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Location = new System.Drawing.Point(404, 427);
            this.bt_ptn3.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn3.TabIndex = 203;
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Location = new System.Drawing.Point(604, 427);
            this.bt_ptn4.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn4.TabIndex = 204;
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Location = new System.Drawing.Point(804, 427);
            this.bt_ptn5.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn5.TabIndex = 205;
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.Location = new System.Drawing.Point(4, 131);
            this.customSortHeader1.Size = new System.Drawing.Size(994, 50);
            this.customSortHeader1.TabIndex = 110;
            // 
            // JIGYOUSHA_KBN_3
            // 
            this.JIGYOUSHA_KBN_3.AutoSize = true;
            this.JIGYOUSHA_KBN_3.DBFieldsName = "";
            this.JIGYOUSHA_KBN_3.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUSHA_KBN_3.DisplayItemName = "更新種別";
            this.JIGYOUSHA_KBN_3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_KBN_3.FocusOutCheckMethod")));
            this.JIGYOUSHA_KBN_3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.JIGYOUSHA_KBN_3.LinkedTextBox = "JIGYOUSHA_KBN";
            this.JIGYOUSHA_KBN_3.Location = new System.Drawing.Point(881, 26);
            this.JIGYOUSHA_KBN_3.Name = "JIGYOUSHA_KBN_3";
            this.JIGYOUSHA_KBN_3.PopupAfterExecute = null;
            this.JIGYOUSHA_KBN_3.PopupBeforeExecute = null;
            this.JIGYOUSHA_KBN_3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JIGYOUSHA_KBN_3.PopupSearchSendParams")));
            this.JIGYOUSHA_KBN_3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JIGYOUSHA_KBN_3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JIGYOUSHA_KBN_3.popupWindowSetting")));
            this.JIGYOUSHA_KBN_3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_KBN_3.RegistCheckMethod")));
            this.JIGYOUSHA_KBN_3.ShortItemName = "更新種別";
            this.JIGYOUSHA_KBN_3.Size = new System.Drawing.Size(109, 17);
            this.JIGYOUSHA_KBN_3.TabIndex = 109;
            this.JIGYOUSHA_KBN_3.Tag = "処分事業者の場合にはチェックを付けてください";
            this.JIGYOUSHA_KBN_3.Text = "3.処分事業者";
            this.JIGYOUSHA_KBN_3.UseVisualStyleBackColor = true;
            this.JIGYOUSHA_KBN_3.Value = "3";
            // 
            // JIGYOUSHA_KBN_2
            // 
            this.JIGYOUSHA_KBN_2.AutoSize = true;
            this.JIGYOUSHA_KBN_2.DBFieldsName = "";
            this.JIGYOUSHA_KBN_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUSHA_KBN_2.DisplayItemName = "更新種別";
            this.JIGYOUSHA_KBN_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_KBN_2.FocusOutCheckMethod")));
            this.JIGYOUSHA_KBN_2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.JIGYOUSHA_KBN_2.LinkedTextBox = "JIGYOUSHA_KBN";
            this.JIGYOUSHA_KBN_2.Location = new System.Drawing.Point(752, 26);
            this.JIGYOUSHA_KBN_2.Name = "JIGYOUSHA_KBN_2";
            this.JIGYOUSHA_KBN_2.PopupAfterExecute = null;
            this.JIGYOUSHA_KBN_2.PopupBeforeExecute = null;
            this.JIGYOUSHA_KBN_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JIGYOUSHA_KBN_2.PopupSearchSendParams")));
            this.JIGYOUSHA_KBN_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JIGYOUSHA_KBN_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JIGYOUSHA_KBN_2.popupWindowSetting")));
            this.JIGYOUSHA_KBN_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_KBN_2.RegistCheckMethod")));
            this.JIGYOUSHA_KBN_2.ShortItemName = "更新種別";
            this.JIGYOUSHA_KBN_2.Size = new System.Drawing.Size(123, 17);
            this.JIGYOUSHA_KBN_2.TabIndex = 108;
            this.JIGYOUSHA_KBN_2.Tag = "収集運搬業者の場合にはチェックを付けてください";
            this.JIGYOUSHA_KBN_2.Text = "2.収集運搬業者";
            this.JIGYOUSHA_KBN_2.UseVisualStyleBackColor = true;
            this.JIGYOUSHA_KBN_2.Value = "2";
            // 
            // JIGYOUSHA_KBN_1
            // 
            this.JIGYOUSHA_KBN_1.AutoSize = true;
            this.JIGYOUSHA_KBN_1.DBFieldsName = "";
            this.JIGYOUSHA_KBN_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUSHA_KBN_1.DisplayItemName = "更新種別";
            this.JIGYOUSHA_KBN_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_KBN_1.FocusOutCheckMethod")));
            this.JIGYOUSHA_KBN_1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.JIGYOUSHA_KBN_1.LinkedTextBox = "JIGYOUSHA_KBN";
            this.JIGYOUSHA_KBN_1.Location = new System.Drawing.Point(630, 26);
            this.JIGYOUSHA_KBN_1.Name = "JIGYOUSHA_KBN_1";
            this.JIGYOUSHA_KBN_1.PopupAfterExecute = null;
            this.JIGYOUSHA_KBN_1.PopupBeforeExecute = null;
            this.JIGYOUSHA_KBN_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JIGYOUSHA_KBN_1.PopupSearchSendParams")));
            this.JIGYOUSHA_KBN_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JIGYOUSHA_KBN_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JIGYOUSHA_KBN_1.popupWindowSetting")));
            this.JIGYOUSHA_KBN_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_KBN_1.RegistCheckMethod")));
            this.JIGYOUSHA_KBN_1.ShortItemName = "更新種別";
            this.JIGYOUSHA_KBN_1.Size = new System.Drawing.Size(116, 17);
            this.JIGYOUSHA_KBN_1.TabIndex = 107;
            this.JIGYOUSHA_KBN_1.Tag = "排出業者の場合にはチェックを付けてください";
            this.JIGYOUSHA_KBN_1.Text = "1. 排出事業者";
            this.JIGYOUSHA_KBN_1.UseVisualStyleBackColor = true;
            this.JIGYOUSHA_KBN_1.Value = "1";
            // 
            // JIGYOUSHA_KBN
            // 
            this.JIGYOUSHA_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.JIGYOUSHA_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JIGYOUSHA_KBN.DBFieldsName = "";
            this.JIGYOUSHA_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUSHA_KBN.DisplayItemName = "検索条件";
            this.JIGYOUSHA_KBN.DisplayPopUp = null;
            this.JIGYOUSHA_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_KBN.FocusOutCheckMethod")));
            this.JIGYOUSHA_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.JIGYOUSHA_KBN.ForeColor = System.Drawing.Color.Black;
            this.JIGYOUSHA_KBN.IsInputErrorOccured = false;
            this.JIGYOUSHA_KBN.ItemDefinedTypes = "";
            this.JIGYOUSHA_KBN.LinkedRadioButtonArray = new string[] {
        "JIGYOUSHA_KBN_1",
        "JIGYOUSHA_KBN_2",
        "JIGYOUSHA_KBN_3"};
            this.JIGYOUSHA_KBN.Location = new System.Drawing.Point(604, 26);
            this.JIGYOUSHA_KBN.Name = "JIGYOUSHA_KBN";
            this.JIGYOUSHA_KBN.PopupAfterExecute = null;
            this.JIGYOUSHA_KBN.PopupBeforeExecute = null;
            this.JIGYOUSHA_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JIGYOUSHA_KBN.PopupSearchSendParams")));
            this.JIGYOUSHA_KBN.PopupSetFormField = "";
            this.JIGYOUSHA_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JIGYOUSHA_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JIGYOUSHA_KBN.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            3,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.JIGYOUSHA_KBN.RangeSetting = rangeSettingDto1;
            this.JIGYOUSHA_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_KBN.RegistCheckMethod")));
            this.JIGYOUSHA_KBN.SetFormField = "";
            this.JIGYOUSHA_KBN.ShortItemName = "検索条件";
            this.JIGYOUSHA_KBN.Size = new System.Drawing.Size(20, 20);
            this.JIGYOUSHA_KBN.TabIndex = 106;
            this.JIGYOUSHA_KBN.Tag = "【1～3】のいずれかで入力してください";
            this.JIGYOUSHA_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.JIGYOUSHA_KBN.WordWrap = false;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label16.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(604, 3);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(120, 20);
            this.label16.TabIndex = 105;
            this.label16.Text = "事業者区分";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // JIGYOUSHA_NAME
            // 
            this.JIGYOUSHA_NAME.BackColor = System.Drawing.SystemColors.Window;
            this.JIGYOUSHA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JIGYOUSHA_NAME.CharactersNumber = new decimal(new int[] {
            260,
            0,
            0,
            0});
            this.JIGYOUSHA_NAME.DBFieldsName = "JIGYOUSHA_NAME";
            this.JIGYOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUSHA_NAME.DisplayItemName = "事業者名称";
            this.JIGYOUSHA_NAME.DisplayPopUp = null;
            this.JIGYOUSHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_NAME.FocusOutCheckMethod")));
            this.JIGYOUSHA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.JIGYOUSHA_NAME.ForeColor = System.Drawing.Color.Black;
            this.JIGYOUSHA_NAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.JIGYOUSHA_NAME.IsInputErrorOccured = false;
            this.JIGYOUSHA_NAME.ItemDefinedTypes = "varchar";
            this.JIGYOUSHA_NAME.Location = new System.Drawing.Point(129, 3);
            this.JIGYOUSHA_NAME.MaxLength = 260;
            this.JIGYOUSHA_NAME.Multiline = true;
            this.JIGYOUSHA_NAME.Name = "JIGYOUSHA_NAME";
            this.JIGYOUSHA_NAME.PopupAfterExecute = null;
            this.JIGYOUSHA_NAME.PopupBeforeExecute = null;
            this.JIGYOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JIGYOUSHA_NAME.PopupSearchSendParams")));
            this.JIGYOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JIGYOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JIGYOUSHA_NAME.popupWindowSetting")));
            this.JIGYOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_NAME.RegistCheckMethod")));
            this.JIGYOUSHA_NAME.ShortItemName = "事業者名称";
            this.JIGYOUSHA_NAME.Size = new System.Drawing.Size(469, 48);
            this.JIGYOUSHA_NAME.TabIndex = 101;
            this.JIGYOUSHA_NAME.Tag = "全角１３０文字以内で入力してください";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 20);
            this.label4.TabIndex = 100;
            this.label4.Text = "事業者名称";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // JIGYOUSHA_ADDRESS
            // 
            this.JIGYOUSHA_ADDRESS.BackColor = System.Drawing.SystemColors.Window;
            this.JIGYOUSHA_ADDRESS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JIGYOUSHA_ADDRESS.CharactersNumber = new decimal(new int[] {
            228,
            0,
            0,
            0});
            this.JIGYOUSHA_ADDRESS.DBFieldsName = "JIGYOUSHA_ADDRESS";
            this.JIGYOUSHA_ADDRESS.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUSHA_ADDRESS.DisplayItemName = "住所";
            this.JIGYOUSHA_ADDRESS.DisplayPopUp = null;
            this.JIGYOUSHA_ADDRESS.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_ADDRESS.FocusOutCheckMethod")));
            this.JIGYOUSHA_ADDRESS.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.JIGYOUSHA_ADDRESS.ForeColor = System.Drawing.Color.Black;
            this.JIGYOUSHA_ADDRESS.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.JIGYOUSHA_ADDRESS.IsInputErrorOccured = false;
            this.JIGYOUSHA_ADDRESS.ItemDefinedTypes = "varchar";
            this.JIGYOUSHA_ADDRESS.Location = new System.Drawing.Point(129, 54);
            this.JIGYOUSHA_ADDRESS.MaxLength = 228;
            this.JIGYOUSHA_ADDRESS.Multiline = true;
            this.JIGYOUSHA_ADDRESS.Name = "JIGYOUSHA_ADDRESS";
            this.JIGYOUSHA_ADDRESS.PopupAfterExecute = null;
            this.JIGYOUSHA_ADDRESS.PopupBeforeExecute = null;
            this.JIGYOUSHA_ADDRESS.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JIGYOUSHA_ADDRESS.PopupSearchSendParams")));
            this.JIGYOUSHA_ADDRESS.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JIGYOUSHA_ADDRESS.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JIGYOUSHA_ADDRESS.popupWindowSetting")));
            this.JIGYOUSHA_ADDRESS.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_ADDRESS.RegistCheckMethod")));
            this.JIGYOUSHA_ADDRESS.ShortItemName = "住所";
            this.JIGYOUSHA_ADDRESS.Size = new System.Drawing.Size(469, 48);
            this.JIGYOUSHA_ADDRESS.TabIndex = 103;
            this.JIGYOUSHA_ADDRESS.Tag = "全角１１４文字以内で入力してください";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(3, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(120, 20);
            this.label9.TabIndex = 102;
            this.label9.Text = "住所";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DenManiJigyoushaIchiranForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.ClientSize = new System.Drawing.Size(1008, 458);
            this.Controls.Add(this.JIGYOUSHA_ADDRESS);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.JIGYOUSHA_NAME);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.JIGYOUSHA_KBN_3);
            this.Controls.Add(this.JIGYOUSHA_KBN_2);
            this.Controls.Add(this.JIGYOUSHA_KBN_1);
            this.Controls.Add(this.JIGYOUSHA_KBN);
            this.Controls.Add(this.label16);
            this.Name = "DenManiJigyoushaIchiranForm";
            this.Text = "TorihikisakiIchiran";
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.label16, 0);
            this.Controls.SetChildIndex(this.JIGYOUSHA_KBN, 0);
            this.Controls.SetChildIndex(this.JIGYOUSHA_KBN_1, 0);
            this.Controls.SetChildIndex(this.JIGYOUSHA_KBN_2, 0);
            this.Controls.SetChildIndex(this.JIGYOUSHA_KBN_3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.JIGYOUSHA_NAME, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.JIGYOUSHA_ADDRESS, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomRadioButton JIGYOUSHA_KBN_3;
        internal r_framework.CustomControl.CustomRadioButton JIGYOUSHA_KBN_2;
        internal r_framework.CustomControl.CustomRadioButton JIGYOUSHA_KBN_1;
        internal r_framework.CustomControl.CustomNumericTextBox2 JIGYOUSHA_KBN;
        internal System.Windows.Forms.Label label16;
        internal r_framework.CustomControl.CustomTextBox JIGYOUSHA_NAME;
        private System.Windows.Forms.Label label4;
        internal r_framework.CustomControl.CustomTextBox JIGYOUSHA_ADDRESS;
        private System.Windows.Forms.Label label9;
    }
}
