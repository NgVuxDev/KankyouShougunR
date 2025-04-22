namespace Shougun.Core.SalesManagement.Shiharaikakuteinyuryoku
{
    partial class UIForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.lab_DenpyouKind = new System.Windows.Forms.Label();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.txtKakuteiKbn = new r_framework.CustomControl.CustomNumericTextBox2();
            this.rdoKakuteizumi = new r_framework.CustomControl.CustomRadioButton();
            this.rdoMikakutei = new r_framework.CustomControl.CustomRadioButton();
            this.rdoSubete = new r_framework.CustomControl.CustomRadioButton();
            this.lblKakuteiKbn = new System.Windows.Forms.Label();
            this.customPanel2 = new r_framework.CustomControl.CustomPanel();
            this.chkUriageshiharai = new r_framework.CustomControl.CustomCheckBox();
            this.chkShukka = new r_framework.CustomControl.CustomCheckBox();
            this.chkUkeire = new r_framework.CustomControl.CustomCheckBox();
            this.customPanel1.SuspendLayout();
            this.customPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.searchString.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.FocusOutCheckMethod")));
            this.searchString.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.searchString.ForeColor = System.Drawing.Color.Black;
            this.searchString.Location = new System.Drawing.Point(0, 3);
            this.searchString.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("searchString.PopupSearchSendParams")));
            this.searchString.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("searchString.popupWindowSetting")));
            this.searchString.ReadOnly = true;
            this.searchString.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.RegistCheckMethod")));
            this.searchString.Size = new System.Drawing.Size(632, 135);
            this.searchString.TabStop = false;
            this.searchString.Tag = "    ";
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn1.Location = new System.Drawing.Point(4, 427);
            this.bt_ptn1.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn1.TabIndex = 10;
            this.bt_ptn1.Text = "パターン1";
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn2.Location = new System.Drawing.Point(206, 427);
            this.bt_ptn2.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn2.TabIndex = 11;
            this.bt_ptn2.Text = "パターン2";
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn3.Location = new System.Drawing.Point(406, 427);
            this.bt_ptn3.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn3.TabIndex = 12;
            this.bt_ptn3.Text = "パターン3";
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn4.Location = new System.Drawing.Point(606, 427);
            this.bt_ptn4.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn4.TabIndex = 13;
            this.bt_ptn4.Text = "パターン4";
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn5.Location = new System.Drawing.Point(806, 427);
            this.bt_ptn5.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn5.TabIndex = 14;
            this.bt_ptn5.Text = "パターン5";
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.Location = new System.Drawing.Point(3, 155);
            this.customSortHeader1.TabIndex = 8;
            // 
            // lab_DenpyouKind
            // 
            this.lab_DenpyouKind.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lab_DenpyouKind.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lab_DenpyouKind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lab_DenpyouKind.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lab_DenpyouKind.ForeColor = System.Drawing.Color.White;
            this.lab_DenpyouKind.Location = new System.Drawing.Point(638, 64);
            this.lab_DenpyouKind.Name = "lab_DenpyouKind";
            this.lab_DenpyouKind.Size = new System.Drawing.Size(83, 20);
            this.lab_DenpyouKind.TabIndex = 395;
            this.lab_DenpyouKind.Text = "伝票種類";
            this.lab_DenpyouKind.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel1
            // 
            this.customPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel1.Controls.Add(this.txtKakuteiKbn);
            this.customPanel1.Controls.Add(this.rdoKakuteizumi);
            this.customPanel1.Controls.Add(this.rdoMikakutei);
            this.customPanel1.Controls.Add(this.rdoSubete);
            this.customPanel1.Location = new System.Drawing.Point(726, 26);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(272, 20);
            this.customPanel1.TabIndex = 1;
            // 
            // txtKakuteiKbn
            // 
            this.txtKakuteiKbn.BackColor = System.Drawing.SystemColors.Window;
            this.txtKakuteiKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKakuteiKbn.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtKakuteiKbn.DisplayPopUp = null;
            this.txtKakuteiKbn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKakuteiKbn.FocusOutCheckMethod")));
            this.txtKakuteiKbn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtKakuteiKbn.ForeColor = System.Drawing.Color.Black;
            this.txtKakuteiKbn.IsInputErrorOccured = false;
            this.txtKakuteiKbn.LinkedRadioButtonArray = new string[] {
        "rdoSubete",
        "rdoMikakutei",
        "rdoKakuteizumi"};
            this.txtKakuteiKbn.Location = new System.Drawing.Point(-1, -1);
            this.txtKakuteiKbn.Name = "txtKakuteiKbn";
            this.txtKakuteiKbn.PopupAfterExecute = null;
            this.txtKakuteiKbn.PopupBeforeExecute = null;
            this.txtKakuteiKbn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtKakuteiKbn.PopupSearchSendParams")));
            this.txtKakuteiKbn.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtKakuteiKbn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtKakuteiKbn.popupWindowSetting")));
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
            this.txtKakuteiKbn.RangeSetting = rangeSettingDto1;
            this.txtKakuteiKbn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKakuteiKbn.RegistCheckMethod")));
            this.txtKakuteiKbn.Size = new System.Drawing.Size(20, 20);
            this.txtKakuteiKbn.TabIndex = 1;
            this.txtKakuteiKbn.Tag = "確定区分を入力してください";
            this.txtKakuteiKbn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtKakuteiKbn.WordWrap = false;
            // 
            // rdoKakuteizumi
            // 
            this.rdoKakuteizumi.AutoSize = true;
            this.rdoKakuteizumi.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoKakuteizumi.DisplayItemName = "asdasd";
            this.rdoKakuteizumi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoKakuteizumi.FocusOutCheckMethod")));
            this.rdoKakuteizumi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoKakuteizumi.LinkedTextBox = "txtKakuteiKbn";
            this.rdoKakuteizumi.Location = new System.Drawing.Point(176, 0);
            this.rdoKakuteizumi.Name = "rdoKakuteizumi";
            this.rdoKakuteizumi.PopupAfterExecute = null;
            this.rdoKakuteizumi.PopupBeforeExecute = null;
            this.rdoKakuteizumi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoKakuteizumi.PopupSearchSendParams")));
            this.rdoKakuteizumi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoKakuteizumi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoKakuteizumi.popupWindowSetting")));
            this.rdoKakuteizumi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoKakuteizumi.RegistCheckMethod")));
            this.rdoKakuteizumi.Size = new System.Drawing.Size(95, 17);
            this.rdoKakuteizumi.TabIndex = 4;
            this.rdoKakuteizumi.Tag = "確定区分が確定済みのみ対象とする場合にはチェックを付けてください";
            this.rdoKakuteizumi.Text = "3.確定済み";
            this.rdoKakuteizumi.UseVisualStyleBackColor = true;
            this.rdoKakuteizumi.Value = "3";
            // 
            // rdoMikakutei
            // 
            this.rdoMikakutei.AutoSize = true;
            this.rdoMikakutei.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoMikakutei.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoMikakutei.FocusOutCheckMethod")));
            this.rdoMikakutei.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoMikakutei.LinkedTextBox = "txtKakuteiKbn";
            this.rdoMikakutei.Location = new System.Drawing.Point(88, 0);
            this.rdoMikakutei.Name = "rdoMikakutei";
            this.rdoMikakutei.PopupAfterExecute = null;
            this.rdoMikakutei.PopupBeforeExecute = null;
            this.rdoMikakutei.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoMikakutei.PopupSearchSendParams")));
            this.rdoMikakutei.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoMikakutei.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoMikakutei.popupWindowSetting")));
            this.rdoMikakutei.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoMikakutei.RegistCheckMethod")));
            this.rdoMikakutei.Size = new System.Drawing.Size(81, 17);
            this.rdoMikakutei.TabIndex = 3;
            this.rdoMikakutei.Tag = "確定区分が未確定のみ対象とする場合にはチェックを付けてください";
            this.rdoMikakutei.Text = "2.未確定";
            this.rdoMikakutei.UseVisualStyleBackColor = true;
            this.rdoMikakutei.Value = "2";
            // 
            // rdoSubete
            // 
            this.rdoSubete.AutoSize = true;
            this.rdoSubete.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoSubete.DisplayItemName = "asdasd";
            this.rdoSubete.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoSubete.FocusOutCheckMethod")));
            this.rdoSubete.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoSubete.LinkedTextBox = "txtKakuteiKbn";
            this.rdoSubete.Location = new System.Drawing.Point(20, 0);
            this.rdoSubete.Name = "rdoSubete";
            this.rdoSubete.PopupAfterExecute = null;
            this.rdoSubete.PopupBeforeExecute = null;
            this.rdoSubete.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoSubete.PopupSearchSendParams")));
            this.rdoSubete.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoSubete.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoSubete.popupWindowSetting")));
            this.rdoSubete.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoSubete.RegistCheckMethod")));
            this.rdoSubete.Size = new System.Drawing.Size(67, 17);
            this.rdoSubete.TabIndex = 2;
            this.rdoSubete.Tag = "確定区分全てを対象とする場合にはチェックを付けてください";
            this.rdoSubete.Text = "1.全て";
            this.rdoSubete.UseVisualStyleBackColor = true;
            this.rdoSubete.Value = "1";
            // 
            // lblKakuteiKbn
            // 
            this.lblKakuteiKbn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblKakuteiKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKakuteiKbn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblKakuteiKbn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblKakuteiKbn.ForeColor = System.Drawing.Color.White;
            this.lblKakuteiKbn.Location = new System.Drawing.Point(638, 26);
            this.lblKakuteiKbn.Name = "lblKakuteiKbn";
            this.lblKakuteiKbn.Size = new System.Drawing.Size(83, 20);
            this.lblKakuteiKbn.TabIndex = 396;
            this.lblKakuteiKbn.Text = "確定区分";
            this.lblKakuteiKbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel2
            // 
            this.customPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel2.Controls.Add(this.chkUriageshiharai);
            this.customPanel2.Controls.Add(this.chkShukka);
            this.customPanel2.Controls.Add(this.chkUkeire);
            this.customPanel2.Location = new System.Drawing.Point(726, 64);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(272, 20);
            this.customPanel2.TabIndex = 5;
            // 
            // chkUriageshiharai
            // 
            this.chkUriageshiharai.AllowDrop = true;
            this.chkUriageshiharai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.chkUriageshiharai.DefaultBackColor = System.Drawing.Color.Empty;
            this.chkUriageshiharai.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("chkUriageshiharai.FocusOutCheckMethod")));
            this.chkUriageshiharai.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.chkUriageshiharai.Location = new System.Drawing.Point(176, 1);
            this.chkUriageshiharai.Name = "chkUriageshiharai";
            this.chkUriageshiharai.PopupAfterExecute = null;
            this.chkUriageshiharai.PopupBeforeExecute = null;
            this.chkUriageshiharai.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("chkUriageshiharai.PopupSearchSendParams")));
            this.chkUriageshiharai.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.chkUriageshiharai.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("chkUriageshiharai.popupWindowSetting")));
            this.chkUriageshiharai.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("chkUriageshiharai.RegistCheckMethod")));
            this.chkUriageshiharai.Size = new System.Drawing.Size(90, 17);
            this.chkUriageshiharai.TabIndex = 7;
            this.chkUriageshiharai.Text = "売上/支払";
            this.chkUriageshiharai.UseVisualStyleBackColor = false;
            // 
            // chkShukka
            // 
            this.chkShukka.AllowDrop = true;
            this.chkShukka.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.chkShukka.DefaultBackColor = System.Drawing.Color.Empty;
            this.chkShukka.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("chkShukka.FocusOutCheckMethod")));
            this.chkShukka.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.chkShukka.Location = new System.Drawing.Point(88, 1);
            this.chkShukka.Name = "chkShukka";
            this.chkShukka.PopupAfterExecute = null;
            this.chkShukka.PopupBeforeExecute = null;
            this.chkShukka.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("chkShukka.PopupSearchSendParams")));
            this.chkShukka.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.chkShukka.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("chkShukka.popupWindowSetting")));
            this.chkShukka.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("chkShukka.RegistCheckMethod")));
            this.chkShukka.Size = new System.Drawing.Size(54, 17);
            this.chkShukka.TabIndex = 6;
            this.chkShukka.Text = "出荷";
            this.chkShukka.UseVisualStyleBackColor = false;
            // 
            // chkUkeire
            // 
            this.chkUkeire.AllowDrop = true;
            this.chkUkeire.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.chkUkeire.DefaultBackColor = System.Drawing.Color.Empty;
            this.chkUkeire.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("chkUkeire.FocusOutCheckMethod")));
            this.chkUkeire.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.chkUkeire.Location = new System.Drawing.Point(20, 1);
            this.chkUkeire.Name = "chkUkeire";
            this.chkUkeire.PopupAfterExecute = null;
            this.chkUkeire.PopupBeforeExecute = null;
            this.chkUkeire.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("chkUkeire.PopupSearchSendParams")));
            this.chkUkeire.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.chkUkeire.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("chkUkeire.popupWindowSetting")));
            this.chkUkeire.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("chkUkeire.RegistCheckMethod")));
            this.chkUkeire.Size = new System.Drawing.Size(56, 17);
            this.chkUkeire.TabIndex = 5;
            this.chkUkeire.Text = "受入";
            this.chkUkeire.UseVisualStyleBackColor = false;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.customPanel2);
            this.Controls.Add(this.lblKakuteiKbn);
            this.Controls.Add(this.lab_DenpyouKind);
            this.Controls.Add(this.customPanel1);
            this.Name = "UIForm";
            this.Text = "UIForm";
            this.Controls.SetChildIndex(this.customPanel1, 0);
            this.Controls.SetChildIndex(this.lab_DenpyouKind, 0);
            this.Controls.SetChildIndex(this.lblKakuteiKbn, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.customPanel2, 0);
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.customPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lab_DenpyouKind;
        private r_framework.CustomControl.CustomPanel customPanel1;
        internal System.Windows.Forms.Label lblKakuteiKbn;
        private r_framework.CustomControl.CustomPanel customPanel2;
        public r_framework.CustomControl.CustomCheckBox chkUriageshiharai;
        public r_framework.CustomControl.CustomCheckBox chkShukka;
        public r_framework.CustomControl.CustomCheckBox chkUkeire;
        public r_framework.CustomControl.CustomNumericTextBox2 txtKakuteiKbn;
        public r_framework.CustomControl.CustomRadioButton rdoKakuteizumi;
        public r_framework.CustomControl.CustomRadioButton rdoMikakutei;
        public r_framework.CustomControl.CustomRadioButton rdoSubete;





    }
}