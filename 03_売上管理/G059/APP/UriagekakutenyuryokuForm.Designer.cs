using System.Windows.Forms;
using System;

namespace Shougun.Core.SalesPayment.Uriagekakutenyuryoku
{
    partial class UriagekakutenyuryokuForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UriagekakutenyuryokuForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.lab_DenpyouKind = new System.Windows.Forms.Label();
            this.lblKakuteiKbn = new System.Windows.Forms.Label();
            this.customPanel2 = new r_framework.CustomControl.CustomPanel();
            this.CheckBox_Uriageshiharai = new r_framework.CustomControl.CustomCheckBox();
            this.CheckBox_Syuka = new r_framework.CustomControl.CustomCheckBox();
            this.CheckBox_Jyunyu = new r_framework.CustomControl.CustomCheckBox();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.txtNum_HidukeSentaku = new r_framework.CustomControl.CustomNumericTextBox2();
            this.radbtn_Kakute = new r_framework.CustomControl.CustomRadioButton();
            this.radbtn_Mikakute = new r_framework.CustomControl.CustomRadioButton();
            this.radbtn_Subete = new r_framework.CustomControl.CustomRadioButton();
            this.customPanel2.SuspendLayout();
            this.customPanel1.SuspendLayout();
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
            this.searchString.Tag = "検索条件設定画面で設定した条件が表示されます";
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
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
            this.customSortHeader1.AutoSize = true;
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
            this.lab_DenpyouKind.TabIndex = 393;
            this.lab_DenpyouKind.Text = "伝票種類";
            this.lab_DenpyouKind.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.lblKakuteiKbn.TabIndex = 397;
            this.lblKakuteiKbn.Text = "確定区分";
            this.lblKakuteiKbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel2
            // 
            this.customPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel2.Controls.Add(this.CheckBox_Uriageshiharai);
            this.customPanel2.Controls.Add(this.CheckBox_Syuka);
            this.customPanel2.Controls.Add(this.CheckBox_Jyunyu);
            this.customPanel2.Location = new System.Drawing.Point(726, 64);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(272, 20);
            this.customPanel2.TabIndex = 5;
            // 
            // CheckBox_Uriageshiharai
            // 
            this.CheckBox_Uriageshiharai.AllowDrop = true;
            this.CheckBox_Uriageshiharai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.CheckBox_Uriageshiharai.DefaultBackColor = System.Drawing.Color.Empty;
            this.CheckBox_Uriageshiharai.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CheckBox_Uriageshiharai.FocusOutCheckMethod")));
            this.CheckBox_Uriageshiharai.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CheckBox_Uriageshiharai.Location = new System.Drawing.Point(176, 1);
            this.CheckBox_Uriageshiharai.Name = "CheckBox_Uriageshiharai";
            this.CheckBox_Uriageshiharai.PopupAfterExecute = null;
            this.CheckBox_Uriageshiharai.PopupBeforeExecute = null;
            this.CheckBox_Uriageshiharai.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CheckBox_Uriageshiharai.PopupSearchSendParams")));
            this.CheckBox_Uriageshiharai.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CheckBox_Uriageshiharai.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CheckBox_Uriageshiharai.popupWindowSetting")));
            this.CheckBox_Uriageshiharai.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CheckBox_Uriageshiharai.RegistCheckMethod")));
            this.CheckBox_Uriageshiharai.Size = new System.Drawing.Size(90, 17);
            this.CheckBox_Uriageshiharai.TabIndex = 7;
            this.CheckBox_Uriageshiharai.Text = "売上/支払";
            this.CheckBox_Uriageshiharai.UseVisualStyleBackColor = false;
            // 
            // CheckBox_Syuka
            // 
            this.CheckBox_Syuka.AllowDrop = true;
            this.CheckBox_Syuka.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.CheckBox_Syuka.DefaultBackColor = System.Drawing.Color.Empty;
            this.CheckBox_Syuka.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CheckBox_Syuka.FocusOutCheckMethod")));
            this.CheckBox_Syuka.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CheckBox_Syuka.Location = new System.Drawing.Point(88, 1);
            this.CheckBox_Syuka.Name = "CheckBox_Syuka";
            this.CheckBox_Syuka.PopupAfterExecute = null;
            this.CheckBox_Syuka.PopupBeforeExecute = null;
            this.CheckBox_Syuka.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CheckBox_Syuka.PopupSearchSendParams")));
            this.CheckBox_Syuka.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CheckBox_Syuka.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CheckBox_Syuka.popupWindowSetting")));
            this.CheckBox_Syuka.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CheckBox_Syuka.RegistCheckMethod")));
            this.CheckBox_Syuka.Size = new System.Drawing.Size(54, 17);
            this.CheckBox_Syuka.TabIndex = 6;
            this.CheckBox_Syuka.Text = "出荷";
            this.CheckBox_Syuka.UseVisualStyleBackColor = false;
            // 
            // CheckBox_Jyunyu
            // 
            this.CheckBox_Jyunyu.AllowDrop = true;
            this.CheckBox_Jyunyu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.CheckBox_Jyunyu.DefaultBackColor = System.Drawing.Color.Empty;
            this.CheckBox_Jyunyu.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CheckBox_Jyunyu.FocusOutCheckMethod")));
            this.CheckBox_Jyunyu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CheckBox_Jyunyu.Location = new System.Drawing.Point(20, 1);
            this.CheckBox_Jyunyu.Name = "CheckBox_Jyunyu";
            this.CheckBox_Jyunyu.PopupAfterExecute = null;
            this.CheckBox_Jyunyu.PopupBeforeExecute = null;
            this.CheckBox_Jyunyu.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CheckBox_Jyunyu.PopupSearchSendParams")));
            this.CheckBox_Jyunyu.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CheckBox_Jyunyu.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CheckBox_Jyunyu.popupWindowSetting")));
            this.CheckBox_Jyunyu.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CheckBox_Jyunyu.RegistCheckMethod")));
            this.CheckBox_Jyunyu.Size = new System.Drawing.Size(56, 17);
            this.CheckBox_Jyunyu.TabIndex = 5;
            this.CheckBox_Jyunyu.Text = "受入";
            this.CheckBox_Jyunyu.UseVisualStyleBackColor = false;
            // 
            // customPanel1
            // 
            this.customPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel1.Controls.Add(this.txtNum_HidukeSentaku);
            this.customPanel1.Controls.Add(this.radbtn_Kakute);
            this.customPanel1.Controls.Add(this.radbtn_Mikakute);
            this.customPanel1.Controls.Add(this.radbtn_Subete);
            this.customPanel1.Location = new System.Drawing.Point(726, 26);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(272, 20);
            this.customPanel1.TabIndex = 1;
            // 
            // txtNum_HidukeSentaku
            // 
            this.txtNum_HidukeSentaku.BackColor = System.Drawing.SystemColors.Window;
            this.txtNum_HidukeSentaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNum_HidukeSentaku.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtNum_HidukeSentaku.DisplayPopUp = null;
            this.txtNum_HidukeSentaku.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_HidukeSentaku.FocusOutCheckMethod")));
            this.txtNum_HidukeSentaku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtNum_HidukeSentaku.ForeColor = System.Drawing.Color.Black;
            this.txtNum_HidukeSentaku.IsInputErrorOccured = false;
            this.txtNum_HidukeSentaku.LinkedRadioButtonArray = new string[] {
        "radbtn_Subete",
        "radbtn_Mikakute",
        "radbtn_Kakute"};
            this.txtNum_HidukeSentaku.Location = new System.Drawing.Point(-1, -1);
            this.txtNum_HidukeSentaku.Name = "txtNum_HidukeSentaku";
            this.txtNum_HidukeSentaku.PopupAfterExecute = null;
            this.txtNum_HidukeSentaku.PopupBeforeExecute = null;
            this.txtNum_HidukeSentaku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtNum_HidukeSentaku.PopupSearchSendParams")));
            this.txtNum_HidukeSentaku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtNum_HidukeSentaku.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtNum_HidukeSentaku.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.txtNum_HidukeSentaku.RangeSetting = rangeSettingDto1;
            this.txtNum_HidukeSentaku.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_HidukeSentaku.RegistCheckMethod")));
            this.txtNum_HidukeSentaku.Size = new System.Drawing.Size(20, 20);
            this.txtNum_HidukeSentaku.TabIndex = 1;
            this.txtNum_HidukeSentaku.Tag = "確定区分を入力してください";
            this.txtNum_HidukeSentaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNum_HidukeSentaku.WordWrap = false;
            // 
            // radbtn_Kakute
            // 
            this.radbtn_Kakute.AutoSize = true;
            this.radbtn_Kakute.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Kakute.DisplayItemName = "asdasd";
            this.radbtn_Kakute.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Kakute.FocusOutCheckMethod")));
            this.radbtn_Kakute.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_Kakute.LinkedTextBox = "txtNum_HidukeSentaku";
            this.radbtn_Kakute.Location = new System.Drawing.Point(176, 0);
            this.radbtn_Kakute.Name = "radbtn_Kakute";
            this.radbtn_Kakute.PopupAfterExecute = null;
            this.radbtn_Kakute.PopupBeforeExecute = null;
            this.radbtn_Kakute.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Kakute.PopupSearchSendParams")));
            this.radbtn_Kakute.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Kakute.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Kakute.popupWindowSetting")));
            this.radbtn_Kakute.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Kakute.RegistCheckMethod")));
            this.radbtn_Kakute.Size = new System.Drawing.Size(95, 17);
            this.radbtn_Kakute.TabIndex = 4;
            this.radbtn_Kakute.Tag = "確定区分が確定済みのみ対象とする場合にはチェックを付けてください";
            this.radbtn_Kakute.Text = "3.確定済み";
            this.radbtn_Kakute.UseVisualStyleBackColor = true;
            this.radbtn_Kakute.Value = "3";
            // 
            // radbtn_Mikakute
            // 
            this.radbtn_Mikakute.AutoSize = true;
            this.radbtn_Mikakute.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Mikakute.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Mikakute.FocusOutCheckMethod")));
            this.radbtn_Mikakute.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_Mikakute.LinkedTextBox = "txtNum_HidukeSentaku";
            this.radbtn_Mikakute.Location = new System.Drawing.Point(88, 0);
            this.radbtn_Mikakute.Name = "radbtn_Mikakute";
            this.radbtn_Mikakute.PopupAfterExecute = null;
            this.radbtn_Mikakute.PopupBeforeExecute = null;
            this.radbtn_Mikakute.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Mikakute.PopupSearchSendParams")));
            this.radbtn_Mikakute.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Mikakute.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Mikakute.popupWindowSetting")));
            this.radbtn_Mikakute.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Mikakute.RegistCheckMethod")));
            this.radbtn_Mikakute.Size = new System.Drawing.Size(81, 17);
            this.radbtn_Mikakute.TabIndex = 3;
            this.radbtn_Mikakute.Tag = "確定区分が未確定のみ対象とする場合にはチェックを付けてください";
            this.radbtn_Mikakute.Text = "2.未確定";
            this.radbtn_Mikakute.UseVisualStyleBackColor = true;
            this.radbtn_Mikakute.Value = "2";
            // 
            // radbtn_Subete
            // 
            this.radbtn_Subete.AutoSize = true;
            this.radbtn_Subete.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Subete.DisplayItemName = "asdasd";
            this.radbtn_Subete.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Subete.FocusOutCheckMethod")));
            this.radbtn_Subete.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_Subete.LinkedTextBox = "txtNum_HidukeSentaku";
            this.radbtn_Subete.Location = new System.Drawing.Point(20, 0);
            this.radbtn_Subete.Name = "radbtn_Subete";
            this.radbtn_Subete.PopupAfterExecute = null;
            this.radbtn_Subete.PopupBeforeExecute = null;
            this.radbtn_Subete.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Subete.PopupSearchSendParams")));
            this.radbtn_Subete.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Subete.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Subete.popupWindowSetting")));
            this.radbtn_Subete.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Subete.RegistCheckMethod")));
            this.radbtn_Subete.Size = new System.Drawing.Size(67, 17);
            this.radbtn_Subete.TabIndex = 2;
            this.radbtn_Subete.Tag = "確定区分全てを対象とする場合にはチェックを付けてください";
            this.radbtn_Subete.Text = "1.全て";
            this.radbtn_Subete.UseVisualStyleBackColor = true;
            this.radbtn_Subete.Value = "1";
            // 
            // UriagekakutenyuryokuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.customPanel2);
            this.Controls.Add(this.customPanel1);
            this.Controls.Add(this.lblKakuteiKbn);
            this.Controls.Add(this.lab_DenpyouKind);
            this.Name = "UriagekakutenyuryokuForm";
            this.Text = "UIForm";
            this.Controls.SetChildIndex(this.lab_DenpyouKind, 0);
            this.Controls.SetChildIndex(this.lblKakuteiKbn, 0);
            this.Controls.SetChildIndex(this.customPanel1, 0);
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.customPanel2, 0);
            this.customPanel2.ResumeLayout(false);
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal Label lab_DenpyouKind;
        internal Label lblKakuteiKbn;
        private r_framework.CustomControl.CustomPanel customPanel2;
        private r_framework.CustomControl.CustomPanel customPanel1;
        public r_framework.CustomControl.CustomCheckBox CheckBox_Uriageshiharai;
        internal r_framework.CustomControl.CustomCheckBox CheckBox_Syuka;
        public r_framework.CustomControl.CustomCheckBox CheckBox_Jyunyu;
        public r_framework.CustomControl.CustomRadioButton radbtn_Kakute;
        public r_framework.CustomControl.CustomRadioButton radbtn_Mikakute;
        public r_framework.CustomControl.CustomRadioButton radbtn_Subete;
        public r_framework.CustomControl.CustomNumericTextBox2 txtNum_HidukeSentaku;
    }
}