using System.Windows.Forms;
using System;

namespace Shougun.Core.Carriage.Unchinichiran
{
    partial class UnchinichiranForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnchinichiranForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.label3 = new System.Windows.Forms.Label();
            this.radbtn_Shuka = new r_framework.CustomControl.CustomRadioButton();
            this.radbtn_Ukeire = new r_framework.CustomControl.CustomRadioButton();
            this.radbtn_Uriageshiharai = new r_framework.CustomControl.CustomRadioButton();
            this.txtNum_DenpyoKind = new r_framework.CustomControl.CustomNumericTextBox2();
            this.radbtn_Unchin = new r_framework.CustomControl.CustomRadioButton();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.radbtn_All = new r_framework.CustomControl.CustomRadioButton();
            this.radbtn_Dainou = new r_framework.CustomControl.CustomRadioButton();
            this.customPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.searchString.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.FocusOutCheckMethod")));
            this.searchString.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.searchString.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("searchString.PopupSearchSendParams")));
            this.searchString.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("searchString.popupWindowSetting")));
            this.searchString.ReadOnly = true;
            this.searchString.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.RegistCheckMethod")));
            this.searchString.Size = new System.Drawing.Size(740, 65);
            this.searchString.TabIndex = 1;
            this.searchString.TabStop = false;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Location = new System.Drawing.Point(4, 427);
            this.bt_ptn1.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn1.TabIndex = 24;
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn2.TabIndex = 25;
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Location = new System.Drawing.Point(404, 427);
            this.bt_ptn3.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn3.TabIndex = 26;
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Location = new System.Drawing.Point(604, 427);
            this.bt_ptn4.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn4.TabIndex = 27;
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Location = new System.Drawing.Point(804, 427);
            this.bt_ptn5.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn5.TabIndex = 28;
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.AutoSize = true;
            this.customSortHeader1.Location = new System.Drawing.Point(4, 157);
            this.customSortHeader1.TabIndex = 23;
            // 
            // customSearchHeader1
            // 
            this.customSearchHeader1.Visible = true;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(676, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "伝種区分※";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radbtn_Shuka
            // 
            this.radbtn_Shuka.AutoSize = true;
            this.radbtn_Shuka.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Shuka.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Shuka.FocusOutCheckMethod")));
            this.radbtn_Shuka.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_Shuka.LinkedTextBox = "txtNum_DenpyoKind";
            this.radbtn_Shuka.Location = new System.Drawing.Point(94, 1);
            this.radbtn_Shuka.Name = "radbtn_Shuka";
            this.radbtn_Shuka.PopupAfterExecute = null;
            this.radbtn_Shuka.PopupBeforeExecute = null;
            this.radbtn_Shuka.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Shuka.PopupSearchSendParams")));
            this.radbtn_Shuka.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Shuka.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Shuka.popupWindowSetting")));
            this.radbtn_Shuka.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Shuka.RegistCheckMethod")));
            this.radbtn_Shuka.Size = new System.Drawing.Size(67, 17);
            this.radbtn_Shuka.TabIndex = 15;
            this.radbtn_Shuka.Tag = "伝票種類が「2.出荷」の場合にはチェックを付けてください";
            this.radbtn_Shuka.Text = "2.出荷";
            this.radbtn_Shuka.UseVisualStyleBackColor = true;
            this.radbtn_Shuka.Value = "2";
            // 
            // radbtn_Ukeire
            // 
            this.radbtn_Ukeire.AutoSize = true;
            this.radbtn_Ukeire.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Ukeire.DisplayItemName = "asdasd";
            this.radbtn_Ukeire.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Ukeire.FocusOutCheckMethod")));
            this.radbtn_Ukeire.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_Ukeire.LinkedTextBox = "txtNum_DenpyoKind";
            this.radbtn_Ukeire.Location = new System.Drawing.Point(22, 1);
            this.radbtn_Ukeire.Name = "radbtn_Ukeire";
            this.radbtn_Ukeire.PopupAfterExecute = null;
            this.radbtn_Ukeire.PopupBeforeExecute = null;
            this.radbtn_Ukeire.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Ukeire.PopupSearchSendParams")));
            this.radbtn_Ukeire.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Ukeire.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Ukeire.popupWindowSetting")));
            this.radbtn_Ukeire.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Ukeire.RegistCheckMethod")));
            this.radbtn_Ukeire.Size = new System.Drawing.Size(67, 17);
            this.radbtn_Ukeire.TabIndex = 14;
            this.radbtn_Ukeire.Tag = "伝票種類が「1.受入」の場合にはチェックを付けてください";
            this.radbtn_Ukeire.Text = "1.受入";
            this.radbtn_Ukeire.UseVisualStyleBackColor = true;
            this.radbtn_Ukeire.Value = "1";
            // 
            // radbtn_Uriageshiharai
            // 
            this.radbtn_Uriageshiharai.AutoSize = true;
            this.radbtn_Uriageshiharai.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Uriageshiharai.DisplayItemName = "asdasd";
            this.radbtn_Uriageshiharai.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Uriageshiharai.FocusOutCheckMethod")));
            this.radbtn_Uriageshiharai.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_Uriageshiharai.LinkedTextBox = "txtNum_DenpyoKind";
            this.radbtn_Uriageshiharai.Location = new System.Drawing.Point(161, 1);
            this.radbtn_Uriageshiharai.Name = "radbtn_Uriageshiharai";
            this.radbtn_Uriageshiharai.PopupAfterExecute = null;
            this.radbtn_Uriageshiharai.PopupBeforeExecute = null;
            this.radbtn_Uriageshiharai.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Uriageshiharai.PopupSearchSendParams")));
            this.radbtn_Uriageshiharai.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Uriageshiharai.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Uriageshiharai.popupWindowSetting")));
            this.radbtn_Uriageshiharai.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Uriageshiharai.RegistCheckMethod")));
            this.radbtn_Uriageshiharai.Size = new System.Drawing.Size(102, 17);
            this.radbtn_Uriageshiharai.TabIndex = 16;
            this.radbtn_Uriageshiharai.Tag = "伝票種類が「3.売上/支払」の場合にはチェックを付けてください";
            this.radbtn_Uriageshiharai.Text = "3.売上/支払";
            this.radbtn_Uriageshiharai.UseVisualStyleBackColor = true;
            this.radbtn_Uriageshiharai.Value = "3";
            // 
            // txtNum_DenpyoKind
            // 
            this.txtNum_DenpyoKind.BackColor = System.Drawing.SystemColors.Window;
            this.txtNum_DenpyoKind.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNum_DenpyoKind.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtNum_DenpyoKind.DisplayItemName = "伝種区分";
            this.txtNum_DenpyoKind.DisplayPopUp = null;
            this.txtNum_DenpyoKind.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_DenpyoKind.FocusOutCheckMethod")));
            this.txtNum_DenpyoKind.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtNum_DenpyoKind.ForeColor = System.Drawing.Color.Black;
            this.txtNum_DenpyoKind.IsInputErrorOccured = false;
            this.txtNum_DenpyoKind.LinkedRadioButtonArray = new string[] {
        "radbtn_Ukeire",
        "radbtn_Shuka",
        "radbtn_Uriageshiharai",
        "radbtn_Dainou",
        "radbtn_Unchin",
        "radbtn_All"};
            this.txtNum_DenpyoKind.Location = new System.Drawing.Point(0, 0);
            this.txtNum_DenpyoKind.Name = "txtNum_DenpyoKind";
            this.txtNum_DenpyoKind.PopupAfterExecute = null;
            this.txtNum_DenpyoKind.PopupBeforeExecute = null;
            this.txtNum_DenpyoKind.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtNum_DenpyoKind.PopupSearchSendParams")));
            this.txtNum_DenpyoKind.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtNum_DenpyoKind.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtNum_DenpyoKind.popupWindowSetting")));
            this.txtNum_DenpyoKind.prevText = "";
            this.txtNum_DenpyoKind.PrevText = "";
            rangeSettingDto1.Max = new decimal(new int[] {
            6,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtNum_DenpyoKind.RangeSetting = rangeSettingDto1;
            this.txtNum_DenpyoKind.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_DenpyoKind.RegistCheckMethod")));
            this.txtNum_DenpyoKind.Size = new System.Drawing.Size(20, 20);
            this.txtNum_DenpyoKind.TabIndex = 0;
            this.txtNum_DenpyoKind.Tag = "【1～6】のいずれかで入力してください";
            this.txtNum_DenpyoKind.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNum_DenpyoKind.WordWrap = false;
            // 
            // radbtn_Unchin
            // 
            this.radbtn_Unchin.AutoSize = true;
            this.radbtn_Unchin.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Unchin.DisplayItemName = "asdasd";
            this.radbtn_Unchin.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Unchin.FocusOutCheckMethod")));
            this.radbtn_Unchin.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_Unchin.LinkedTextBox = "txtNum_DenpyoKind";
            this.radbtn_Unchin.Location = new System.Drawing.Point(94, 21);
            this.radbtn_Unchin.Name = "radbtn_Unchin";
            this.radbtn_Unchin.PopupAfterExecute = null;
            this.radbtn_Unchin.PopupBeforeExecute = null;
            this.radbtn_Unchin.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Unchin.PopupSearchSendParams")));
            this.radbtn_Unchin.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Unchin.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Unchin.popupWindowSetting")));
            this.radbtn_Unchin.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Unchin.RegistCheckMethod")));
            this.radbtn_Unchin.Size = new System.Drawing.Size(67, 17);
            this.radbtn_Unchin.TabIndex = 17;
            this.radbtn_Unchin.Tag = "伝票種類が「5.運賃」の場合にはチェックを付けてください";
            this.radbtn_Unchin.Text = "5.運賃";
            this.radbtn_Unchin.UseVisualStyleBackColor = true;
            this.radbtn_Unchin.Value = "5";
            // 
            // customPanel1
            // 
            this.customPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel1.Controls.Add(this.radbtn_All);
            this.customPanel1.Controls.Add(this.radbtn_Dainou);
            this.customPanel1.Controls.Add(this.radbtn_Ukeire);
            this.customPanel1.Controls.Add(this.radbtn_Shuka);
            this.customPanel1.Controls.Add(this.radbtn_Uriageshiharai);
            this.customPanel1.Controls.Add(this.txtNum_DenpyoKind);
            this.customPanel1.Controls.Add(this.radbtn_Unchin);
            this.customPanel1.Location = new System.Drawing.Point(676, 24);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(326, 40);
            this.customPanel1.TabIndex = 13;
            // 
            // radbtn_All
            // 
            this.radbtn_All.AutoSize = true;
            this.radbtn_All.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_All.DisplayItemName = "asdasd";
            this.radbtn_All.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_All.FocusOutCheckMethod")));
            this.radbtn_All.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_All.LinkedTextBox = "txtNum_DenpyoKind";
            this.radbtn_All.Location = new System.Drawing.Point(161, 21);
            this.radbtn_All.Name = "radbtn_All";
            this.radbtn_All.PopupAfterExecute = null;
            this.radbtn_All.PopupBeforeExecute = null;
            this.radbtn_All.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_All.PopupSearchSendParams")));
            this.radbtn_All.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_All.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_All.popupWindowSetting")));
            this.radbtn_All.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_All.RegistCheckMethod")));
            this.radbtn_All.Size = new System.Drawing.Size(67, 17);
            this.radbtn_All.TabIndex = 20;
            this.radbtn_All.Tag = "伝票種類が「6.全て」の場合にはチェックを付けてください";
            this.radbtn_All.Text = "6.全て";
            this.radbtn_All.UseVisualStyleBackColor = true;
            this.radbtn_All.Value = "6";
            // 
            // radbtn_Dainou
            // 
            this.radbtn_Dainou.AutoSize = true;
            this.radbtn_Dainou.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Dainou.DisplayItemName = "asdasd";
            this.radbtn_Dainou.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Dainou.FocusOutCheckMethod")));
            this.radbtn_Dainou.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_Dainou.LinkedTextBox = "txtNum_DenpyoKind";
            this.radbtn_Dainou.Location = new System.Drawing.Point(22, 21);
            this.radbtn_Dainou.Name = "radbtn_Dainou";
            this.radbtn_Dainou.PopupAfterExecute = null;
            this.radbtn_Dainou.PopupBeforeExecute = null;
            this.radbtn_Dainou.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Dainou.PopupSearchSendParams")));
            this.radbtn_Dainou.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Dainou.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Dainou.popupWindowSetting")));
            this.radbtn_Dainou.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Dainou.RegistCheckMethod")));
            this.radbtn_Dainou.Size = new System.Drawing.Size(67, 17);
            this.radbtn_Dainou.TabIndex = 19;
            this.radbtn_Dainou.Tag = "伝票種類が「4.代納」の場合にはチェックを付けてください";
            this.radbtn_Dainou.Text = "4.代納";
            this.radbtn_Dainou.UseVisualStyleBackColor = true;
            this.radbtn_Dainou.Value = "4";
            // 
            // UnchinichiranForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 458);
            this.Controls.Add(this.customPanel1);
            this.Controls.Add(this.label3);
            this.Name = "UnchinichiranForm";
            this.Text = "UIForm";
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.customPanel1, 0);
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal Label label3;
        public r_framework.CustomControl.CustomRadioButton radbtn_Shuka;
        public r_framework.CustomControl.CustomRadioButton radbtn_Ukeire;
        public r_framework.CustomControl.CustomRadioButton radbtn_Uriageshiharai;
        public r_framework.CustomControl.CustomNumericTextBox2 txtNum_DenpyoKind;
        public r_framework.CustomControl.CustomRadioButton radbtn_Unchin;
        private r_framework.CustomControl.CustomPanel customPanel1;
        public r_framework.CustomControl.CustomRadioButton radbtn_Dainou;
        public r_framework.CustomControl.CustomRadioButton radbtn_All;
    }
}