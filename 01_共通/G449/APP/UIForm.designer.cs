using System.Windows.Forms;
using System;

namespace Shougun.Core.Common.DenpyouHimodukeIchiran
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
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.customPanel3 = new r_framework.CustomControl.CustomPanel();
            this.cstRdoBtn10 = new r_framework.CustomControl.CustomRadioButton();
            this.cstRdoBtn9 = new r_framework.CustomControl.CustomRadioButton();
            this.cstRdoBtn8 = new r_framework.CustomControl.CustomRadioButton();
            this.cstRdoBtn7 = new r_framework.CustomControl.CustomRadioButton();
            this.cstRdoBtn6 = new r_framework.CustomControl.CustomRadioButton();
            this.cstRdoBtn5 = new r_framework.CustomControl.CustomRadioButton();
            this.cstRdoBtn1 = new r_framework.CustomControl.CustomRadioButton();
            this.cstRdoBtn2 = new r_framework.CustomControl.CustomRadioButton();
            this.cstRdoBtn3 = new r_framework.CustomControl.CustomRadioButton();
            this.cstRdoBtn4 = new r_framework.CustomControl.CustomRadioButton();
            this.numTxtBox_KsTsKn = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label6 = new System.Windows.Forms.Label();
            this.Denpyou_Date_Panel = new r_framework.CustomControl.CustomPanel();
            this.Denpyou_Date_RdoBtn4 = new r_framework.CustomControl.CustomRadioButton();
            this.Denpyou_Date_RdoBtn3 = new r_framework.CustomControl.CustomRadioButton();
            this.Denpyou_Date_RdoBtn2 = new r_framework.CustomControl.CustomRadioButton();
            this.Denpyou_Date_RdoBtn1 = new r_framework.CustomControl.CustomRadioButton();
            this.DenoyouDate_Label = new System.Windows.Forms.Label();
            this.dtpDateFrom = new r_framework.CustomControl.CustomDateTimePicker();
            this.label38 = new System.Windows.Forms.Label();
            this.dtpDateTo = new r_framework.CustomControl.CustomDateTimePicker();
            this.numTxtBox_Denpyou_Date = new r_framework.CustomControl.CustomNumericTextBox2();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.customPanel3.SuspendLayout();
            this.Denpyou_Date_Panel.SuspendLayout();
            this.customPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.searchString.Enabled = false;
            this.searchString.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.FocusOutCheckMethod")));
            this.searchString.Font = new System.Drawing.Font("MS UI Gothic", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.searchString.Location = new System.Drawing.Point(0, 0);
            this.searchString.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("searchString.PopupSearchSendParams")));
            this.searchString.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("searchString.popupWindowSetting")));
            this.searchString.ReadOnly = true;
            this.searchString.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.RegistCheckMethod")));
            this.searchString.Size = new System.Drawing.Size(715, 90);
            this.searchString.TabIndex = 4;
            this.searchString.TabStop = false;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Location = new System.Drawing.Point(0, 440);
            this.bt_ptn1.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn1.TabIndex = 4;
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Location = new System.Drawing.Point(200, 440);
            this.bt_ptn2.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn2.TabIndex = 5;
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Location = new System.Drawing.Point(400, 440);
            this.bt_ptn3.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn3.TabIndex = 6;
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Location = new System.Drawing.Point(600, 440);
            this.bt_ptn4.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn4.TabIndex = 7;
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Location = new System.Drawing.Point(800, 440);
            this.bt_ptn5.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn5.TabIndex = 8;
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.AutoSize = true;
            this.customSortHeader1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.customSortHeader1.Location = new System.Drawing.Point(5, 90);
            this.customSortHeader1.Size = new System.Drawing.Size(992, 26);
            this.customSortHeader1.TabIndex = 1;
            // 
            // customPanel3
            // 
            this.customPanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel3.Controls.Add(this.cstRdoBtn10);
            this.customPanel3.Controls.Add(this.cstRdoBtn9);
            this.customPanel3.Controls.Add(this.cstRdoBtn8);
            this.customPanel3.Controls.Add(this.cstRdoBtn7);
            this.customPanel3.Controls.Add(this.cstRdoBtn6);
            this.customPanel3.Controls.Add(this.cstRdoBtn5);
            this.customPanel3.Controls.Add(this.cstRdoBtn1);
            this.customPanel3.Controls.Add(this.cstRdoBtn2);
            this.customPanel3.Controls.Add(this.cstRdoBtn3);
            this.customPanel3.Controls.Add(this.cstRdoBtn4);
            this.customPanel3.Location = new System.Drawing.Point(19, 24);
            this.customPanel3.Name = "customPanel3";
            this.customPanel3.Size = new System.Drawing.Size(404, 62);
            this.customPanel3.TabIndex = 2;
            // 
            // cstRdoBtn10
            // 
            this.cstRdoBtn10.AutoSize = true;
            this.cstRdoBtn10.DefaultBackColor = System.Drawing.Color.Empty;
            this.cstRdoBtn10.DisplayItemName = "";
            this.cstRdoBtn10.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cstRdoBtn10.FocusOutCheckMethod")));
            this.cstRdoBtn10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cstRdoBtn10.Location = new System.Drawing.Point(110, 42);
            this.cstRdoBtn10.Name = "cstRdoBtn10";
            this.cstRdoBtn10.PopupAfterExecute = null;
            this.cstRdoBtn10.PopupBeforeExecute = null;
            this.cstRdoBtn10.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cstRdoBtn10.PopupSearchSendParams")));
            this.cstRdoBtn10.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cstRdoBtn10.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cstRdoBtn10.popupWindowSetting")));
            this.cstRdoBtn10.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cstRdoBtn10.RegistCheckMethod")));
            this.cstRdoBtn10.Size = new System.Drawing.Size(74, 17);
            this.cstRdoBtn10.TabIndex = 9;
            this.cstRdoBtn10.Tag = "出力対象機能を選択します";
            this.cstRdoBtn10.Text = "10.代納";
            this.cstRdoBtn10.UseVisualStyleBackColor = true;
            this.cstRdoBtn10.CheckedChanged += new System.EventHandler(this.cstRdoBtn10_CheckedChanged);
            // 
            // cstRdoBtn9
            // 
            this.cstRdoBtn9.AutoSize = true;
            this.cstRdoBtn9.DefaultBackColor = System.Drawing.Color.Empty;
            this.cstRdoBtn9.DisplayItemName = "";
            this.cstRdoBtn9.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cstRdoBtn9.FocusOutCheckMethod")));
            this.cstRdoBtn9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cstRdoBtn9.Location = new System.Drawing.Point(2, 42);
            this.cstRdoBtn9.Name = "cstRdoBtn9";
            this.cstRdoBtn9.PopupAfterExecute = null;
            this.cstRdoBtn9.PopupBeforeExecute = null;
            this.cstRdoBtn9.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cstRdoBtn9.PopupSearchSendParams")));
            this.cstRdoBtn9.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cstRdoBtn9.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cstRdoBtn9.popupWindowSetting")));
            this.cstRdoBtn9.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cstRdoBtn9.RegistCheckMethod")));
            this.cstRdoBtn9.Size = new System.Drawing.Size(67, 17);
            this.cstRdoBtn9.TabIndex = 8;
            this.cstRdoBtn9.Tag = "出力対象機能を選択します";
            this.cstRdoBtn9.Text = "9.運賃";
            this.cstRdoBtn9.UseVisualStyleBackColor = true;
            this.cstRdoBtn9.CheckedChanged += new System.EventHandler(this.cstRdoBtn9_CheckedChanged);
            // 
            // cstRdoBtn8
            // 
            this.cstRdoBtn8.AutoSize = true;
            this.cstRdoBtn8.DefaultBackColor = System.Drawing.Color.Empty;
            this.cstRdoBtn8.DisplayItemName = "";
            this.cstRdoBtn8.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cstRdoBtn8.FocusOutCheckMethod")));
            this.cstRdoBtn8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cstRdoBtn8.Location = new System.Drawing.Point(312, 22);
            this.cstRdoBtn8.Name = "cstRdoBtn8";
            this.cstRdoBtn8.PopupAfterExecute = null;
            this.cstRdoBtn8.PopupBeforeExecute = null;
            this.cstRdoBtn8.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cstRdoBtn8.PopupSearchSendParams")));
            this.cstRdoBtn8.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cstRdoBtn8.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cstRdoBtn8.popupWindowSetting")));
            this.cstRdoBtn8.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cstRdoBtn8.RegistCheckMethod")));
            this.cstRdoBtn8.Size = new System.Drawing.Size(81, 17);
            this.cstRdoBtn8.TabIndex = 7;
            this.cstRdoBtn8.Tag = "出力対象機能を選択します";
            this.cstRdoBtn8.Text = "8.電マニ";
            this.cstRdoBtn8.UseVisualStyleBackColor = true;
            this.cstRdoBtn8.CheckedChanged += new System.EventHandler(this.cstRdoBtn8_CheckedChanged);
            // 
            // cstRdoBtn7
            // 
            this.cstRdoBtn7.AutoSize = true;
            this.cstRdoBtn7.DefaultBackColor = System.Drawing.Color.Empty;
            this.cstRdoBtn7.DisplayItemName = "";
            this.cstRdoBtn7.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cstRdoBtn7.FocusOutCheckMethod")));
            this.cstRdoBtn7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cstRdoBtn7.Location = new System.Drawing.Point(211, 22);
            this.cstRdoBtn7.Name = "cstRdoBtn7";
            this.cstRdoBtn7.PopupAfterExecute = null;
            this.cstRdoBtn7.PopupBeforeExecute = null;
            this.cstRdoBtn7.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cstRdoBtn7.PopupSearchSendParams")));
            this.cstRdoBtn7.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cstRdoBtn7.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cstRdoBtn7.popupWindowSetting")));
            this.cstRdoBtn7.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cstRdoBtn7.RegistCheckMethod")));
            this.cstRdoBtn7.Size = new System.Drawing.Size(95, 17);
            this.cstRdoBtn7.TabIndex = 6;
            this.cstRdoBtn7.Tag = "出力対象機能を選択します";
            this.cstRdoBtn7.Text = "7.マニ２次";
            this.cstRdoBtn7.UseVisualStyleBackColor = true;
            this.cstRdoBtn7.CheckedChanged += new System.EventHandler(this.cstRdoBtn7_CheckedChanged);
            // 
            // cstRdoBtn6
            // 
            this.cstRdoBtn6.AutoSize = true;
            this.cstRdoBtn6.DefaultBackColor = System.Drawing.Color.Empty;
            this.cstRdoBtn6.DisplayItemName = "";
            this.cstRdoBtn6.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cstRdoBtn6.FocusOutCheckMethod")));
            this.cstRdoBtn6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cstRdoBtn6.Location = new System.Drawing.Point(110, 22);
            this.cstRdoBtn6.Name = "cstRdoBtn6";
            this.cstRdoBtn6.PopupAfterExecute = null;
            this.cstRdoBtn6.PopupBeforeExecute = null;
            this.cstRdoBtn6.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cstRdoBtn6.PopupSearchSendParams")));
            this.cstRdoBtn6.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cstRdoBtn6.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cstRdoBtn6.popupWindowSetting")));
            this.cstRdoBtn6.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cstRdoBtn6.RegistCheckMethod")));
            this.cstRdoBtn6.Size = new System.Drawing.Size(95, 17);
            this.cstRdoBtn6.TabIndex = 5;
            this.cstRdoBtn6.Tag = "出力対象機能を選択します";
            this.cstRdoBtn6.Text = "6.マニ１次";
            this.cstRdoBtn6.UseVisualStyleBackColor = true;
            this.cstRdoBtn6.CheckedChanged += new System.EventHandler(this.cstRdoBtn6_CheckedChanged);
            // 
            // cstRdoBtn5
            // 
            this.cstRdoBtn5.AutoSize = true;
            this.cstRdoBtn5.DefaultBackColor = System.Drawing.Color.Empty;
            this.cstRdoBtn5.DisplayItemName = "";
            this.cstRdoBtn5.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cstRdoBtn5.FocusOutCheckMethod")));
            this.cstRdoBtn5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cstRdoBtn5.Location = new System.Drawing.Point(2, 22);
            this.cstRdoBtn5.Name = "cstRdoBtn5";
            this.cstRdoBtn5.PopupAfterExecute = null;
            this.cstRdoBtn5.PopupBeforeExecute = null;
            this.cstRdoBtn5.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cstRdoBtn5.PopupSearchSendParams")));
            this.cstRdoBtn5.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cstRdoBtn5.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cstRdoBtn5.popupWindowSetting")));
            this.cstRdoBtn5.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cstRdoBtn5.RegistCheckMethod")));
            this.cstRdoBtn5.Size = new System.Drawing.Size(102, 17);
            this.cstRdoBtn5.TabIndex = 4;
            this.cstRdoBtn5.Tag = "出力対象機能を選択します";
            this.cstRdoBtn5.Text = "5.売上/支払";
            this.cstRdoBtn5.UseVisualStyleBackColor = true;
            this.cstRdoBtn5.CheckedChanged += new System.EventHandler(this.cstRdoBtn5_CheckedChanged);
            // 
            // cstRdoBtn1
            // 
            this.cstRdoBtn1.AutoSize = true;
            this.cstRdoBtn1.DefaultBackColor = System.Drawing.Color.Empty;
            this.cstRdoBtn1.DisplayItemName = "";
            this.cstRdoBtn1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cstRdoBtn1.FocusOutCheckMethod")));
            this.cstRdoBtn1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cstRdoBtn1.Location = new System.Drawing.Point(2, 2);
            this.cstRdoBtn1.Name = "cstRdoBtn1";
            this.cstRdoBtn1.PopupAfterExecute = null;
            this.cstRdoBtn1.PopupBeforeExecute = null;
            this.cstRdoBtn1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cstRdoBtn1.PopupSearchSendParams")));
            this.cstRdoBtn1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cstRdoBtn1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cstRdoBtn1.popupWindowSetting")));
            this.cstRdoBtn1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cstRdoBtn1.RegistCheckMethod")));
            this.cstRdoBtn1.Size = new System.Drawing.Size(67, 17);
            this.cstRdoBtn1.TabIndex = 0;
            this.cstRdoBtn1.Tag = "出力対象機能を選択します";
            this.cstRdoBtn1.Text = "1.受付";
            this.cstRdoBtn1.UseVisualStyleBackColor = true;
            this.cstRdoBtn1.CheckedChanged += new System.EventHandler(this.cstRdoBtn1_CheckedChanged);
            // 
            // cstRdoBtn2
            // 
            this.cstRdoBtn2.AutoSize = true;
            this.cstRdoBtn2.DefaultBackColor = System.Drawing.Color.Empty;
            this.cstRdoBtn2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cstRdoBtn2.FocusOutCheckMethod")));
            this.cstRdoBtn2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cstRdoBtn2.Location = new System.Drawing.Point(110, 2);
            this.cstRdoBtn2.Name = "cstRdoBtn2";
            this.cstRdoBtn2.PopupAfterExecute = null;
            this.cstRdoBtn2.PopupBeforeExecute = null;
            this.cstRdoBtn2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cstRdoBtn2.PopupSearchSendParams")));
            this.cstRdoBtn2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cstRdoBtn2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cstRdoBtn2.popupWindowSetting")));
            this.cstRdoBtn2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cstRdoBtn2.RegistCheckMethod")));
            this.cstRdoBtn2.Size = new System.Drawing.Size(67, 17);
            this.cstRdoBtn2.TabIndex = 1;
            this.cstRdoBtn2.Tag = "出力対象機能を選択します";
            this.cstRdoBtn2.Text = "2.計量";
            this.cstRdoBtn2.UseVisualStyleBackColor = true;
            this.cstRdoBtn2.CheckedChanged += new System.EventHandler(this.cstRdoBtn2_CheckedChanged);
            // 
            // cstRdoBtn3
            // 
            this.cstRdoBtn3.AutoSize = true;
            this.cstRdoBtn3.DefaultBackColor = System.Drawing.Color.Empty;
            this.cstRdoBtn3.DisplayItemName = "asdasd";
            this.cstRdoBtn3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cstRdoBtn3.FocusOutCheckMethod")));
            this.cstRdoBtn3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cstRdoBtn3.Location = new System.Drawing.Point(211, 2);
            this.cstRdoBtn3.Name = "cstRdoBtn3";
            this.cstRdoBtn3.PopupAfterExecute = null;
            this.cstRdoBtn3.PopupBeforeExecute = null;
            this.cstRdoBtn3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cstRdoBtn3.PopupSearchSendParams")));
            this.cstRdoBtn3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cstRdoBtn3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cstRdoBtn3.popupWindowSetting")));
            this.cstRdoBtn3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cstRdoBtn3.RegistCheckMethod")));
            this.cstRdoBtn3.Size = new System.Drawing.Size(67, 17);
            this.cstRdoBtn3.TabIndex = 2;
            this.cstRdoBtn3.Tag = "出力対象機能を選択します";
            this.cstRdoBtn3.Text = "3.受入";
            this.cstRdoBtn3.UseVisualStyleBackColor = true;
            this.cstRdoBtn3.CheckedChanged += new System.EventHandler(this.cstRdoBtn3_CheckedChanged);
            // 
            // cstRdoBtn4
            // 
            this.cstRdoBtn4.AutoSize = true;
            this.cstRdoBtn4.DefaultBackColor = System.Drawing.Color.Empty;
            this.cstRdoBtn4.DisplayItemName = "asdasd";
            this.cstRdoBtn4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cstRdoBtn4.FocusOutCheckMethod")));
            this.cstRdoBtn4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cstRdoBtn4.Location = new System.Drawing.Point(312, 2);
            this.cstRdoBtn4.Name = "cstRdoBtn4";
            this.cstRdoBtn4.PopupAfterExecute = null;
            this.cstRdoBtn4.PopupBeforeExecute = null;
            this.cstRdoBtn4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cstRdoBtn4.PopupSearchSendParams")));
            this.cstRdoBtn4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cstRdoBtn4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cstRdoBtn4.popupWindowSetting")));
            this.cstRdoBtn4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cstRdoBtn4.RegistCheckMethod")));
            this.cstRdoBtn4.Size = new System.Drawing.Size(67, 17);
            this.cstRdoBtn4.TabIndex = 3;
            this.cstRdoBtn4.Tag = "出力対象機能を選択します";
            this.cstRdoBtn4.Text = "4.出荷";
            this.cstRdoBtn4.UseVisualStyleBackColor = true;
            this.cstRdoBtn4.CheckedChanged += new System.EventHandler(this.cstRdoBtn4_CheckedChanged);
            // 
            // numTxtBox_KsTsKn
            // 
            this.numTxtBox_KsTsKn.BackColor = System.Drawing.SystemColors.Window;
            this.numTxtBox_KsTsKn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numTxtBox_KsTsKn.DefaultBackColor = System.Drawing.Color.Empty;
            this.numTxtBox_KsTsKn.DisplayItemName = "検索対象機能";
            this.numTxtBox_KsTsKn.DisplayPopUp = null;
            this.numTxtBox_KsTsKn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("numTxtBox_KsTsKn.FocusOutCheckMethod")));
            this.numTxtBox_KsTsKn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.numTxtBox_KsTsKn.ForeColor = System.Drawing.Color.Black;
            this.numTxtBox_KsTsKn.IsInputErrorOccured = false;
            this.numTxtBox_KsTsKn.LinkedRadioButtonArray = new string[0];
            this.numTxtBox_KsTsKn.Location = new System.Drawing.Point(0, 24);
            this.numTxtBox_KsTsKn.Name = "numTxtBox_KsTsKn";
            this.numTxtBox_KsTsKn.PopupAfterExecute = null;
            this.numTxtBox_KsTsKn.PopupBeforeExecute = null;
            this.numTxtBox_KsTsKn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("numTxtBox_KsTsKn.PopupSearchSendParams")));
            this.numTxtBox_KsTsKn.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.numTxtBox_KsTsKn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("numTxtBox_KsTsKn.popupWindowSetting")));
            this.numTxtBox_KsTsKn.prevText = null;
            this.numTxtBox_KsTsKn.PrevText = null;
            rangeSettingDto1.Max = new decimal(new int[] {
            10,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTxtBox_KsTsKn.RangeSetting = rangeSettingDto1;
            this.numTxtBox_KsTsKn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("numTxtBox_KsTsKn.RegistCheckMethod")));
            this.numTxtBox_KsTsKn.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numTxtBox_KsTsKn.Size = new System.Drawing.Size(20, 20);
            this.numTxtBox_KsTsKn.TabIndex = 1;
            this.numTxtBox_KsTsKn.Tag = "【1～10】のいずれかで入力してください";
            this.numTxtBox_KsTsKn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numTxtBox_KsTsKn.WordWrap = false;
            this.numTxtBox_KsTsKn.TextChanged += new System.EventHandler(this.numTxtBox_KsTsKn_TextChanged);
            this.numTxtBox_KsTsKn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numTxtBox_KsTsKn_KeyPress);
            this.numTxtBox_KsTsKn.Leave += new System.EventHandler(this.numTxtBox_KsTsKn_Leave);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "検索対象機能";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Denpyou_Date_Panel
            // 
            this.Denpyou_Date_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Denpyou_Date_Panel.Controls.Add(this.Denpyou_Date_RdoBtn4);
            this.Denpyou_Date_Panel.Controls.Add(this.Denpyou_Date_RdoBtn3);
            this.Denpyou_Date_Panel.Controls.Add(this.Denpyou_Date_RdoBtn2);
            this.Denpyou_Date_Panel.Controls.Add(this.Denpyou_Date_RdoBtn1);
            this.Denpyou_Date_Panel.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Denpyou_Date_Panel.Location = new System.Drawing.Point(459, 24);
            this.Denpyou_Date_Panel.Name = "Denpyou_Date_Panel";
            this.Denpyou_Date_Panel.Size = new System.Drawing.Size(488, 20);
            this.Denpyou_Date_Panel.TabIndex = 8;
            // 
            // Denpyou_Date_RdoBtn4
            // 
            this.Denpyou_Date_RdoBtn4.AutoSize = true;
            this.Denpyou_Date_RdoBtn4.DefaultBackColor = System.Drawing.Color.Empty;
            this.Denpyou_Date_RdoBtn4.DisplayItemName = "";
            this.Denpyou_Date_RdoBtn4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Denpyou_Date_RdoBtn4.FocusOutCheckMethod")));
            this.Denpyou_Date_RdoBtn4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Denpyou_Date_RdoBtn4.LinkedTextBox = "numTxtBox_Denpyou_Date";
            this.Denpyou_Date_RdoBtn4.Location = new System.Drawing.Point(347, 1);
            this.Denpyou_Date_RdoBtn4.Name = "Denpyou_Date_RdoBtn4";
            this.Denpyou_Date_RdoBtn4.PopupAfterExecute = null;
            this.Denpyou_Date_RdoBtn4.PopupBeforeExecute = null;
            this.Denpyou_Date_RdoBtn4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Denpyou_Date_RdoBtn4.PopupSearchSendParams")));
            this.Denpyou_Date_RdoBtn4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Denpyou_Date_RdoBtn4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Denpyou_Date_RdoBtn4.popupWindowSetting")));
            this.Denpyou_Date_RdoBtn4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Denpyou_Date_RdoBtn4.RegistCheckMethod")));
            this.Denpyou_Date_RdoBtn4.Size = new System.Drawing.Size(137, 17);
            this.Denpyou_Date_RdoBtn4.TabIndex = 4;
            this.Denpyou_Date_RdoBtn4.Tag = "伝票日付を選択します";
            this.Denpyou_Date_RdoBtn4.Text = "4.最終処分終了日";
            this.Denpyou_Date_RdoBtn4.UseVisualStyleBackColor = true;
            this.Denpyou_Date_RdoBtn4.Value = "4";
            this.Denpyou_Date_RdoBtn4.CheckedChanged += new System.EventHandler(this.Denpyou_Date_RdoBtn4_CheckedChanged);
            // 
            // Denpyou_Date_RdoBtn3
            // 
            this.Denpyou_Date_RdoBtn3.AutoSize = true;
            this.Denpyou_Date_RdoBtn3.DefaultBackColor = System.Drawing.Color.Empty;
            this.Denpyou_Date_RdoBtn3.DisplayItemName = "";
            this.Denpyou_Date_RdoBtn3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Denpyou_Date_RdoBtn3.FocusOutCheckMethod")));
            this.Denpyou_Date_RdoBtn3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Denpyou_Date_RdoBtn3.LinkedTextBox = "numTxtBox_Denpyou_Date";
            this.Denpyou_Date_RdoBtn3.Location = new System.Drawing.Point(232, 1);
            this.Denpyou_Date_RdoBtn3.Name = "Denpyou_Date_RdoBtn3";
            this.Denpyou_Date_RdoBtn3.PopupAfterExecute = null;
            this.Denpyou_Date_RdoBtn3.PopupBeforeExecute = null;
            this.Denpyou_Date_RdoBtn3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Denpyou_Date_RdoBtn3.PopupSearchSendParams")));
            this.Denpyou_Date_RdoBtn3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Denpyou_Date_RdoBtn3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Denpyou_Date_RdoBtn3.popupWindowSetting")));
            this.Denpyou_Date_RdoBtn3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Denpyou_Date_RdoBtn3.RegistCheckMethod")));
            this.Denpyou_Date_RdoBtn3.Size = new System.Drawing.Size(109, 17);
            this.Denpyou_Date_RdoBtn3.TabIndex = 3;
            this.Denpyou_Date_RdoBtn3.Tag = "伝票日付を選択します";
            this.Denpyou_Date_RdoBtn3.Text = "3.処分終了日";
            this.Denpyou_Date_RdoBtn3.UseVisualStyleBackColor = true;
            this.Denpyou_Date_RdoBtn3.Value = "3";
            this.Denpyou_Date_RdoBtn3.CheckedChanged += new System.EventHandler(this.Denpyou_Date_RdoBtn3_CheckedChanged);
            // 
            // Denpyou_Date_RdoBtn2
            // 
            this.Denpyou_Date_RdoBtn2.AutoSize = true;
            this.Denpyou_Date_RdoBtn2.DefaultBackColor = System.Drawing.Color.Empty;
            this.Denpyou_Date_RdoBtn2.DisplayItemName = "";
            this.Denpyou_Date_RdoBtn2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Denpyou_Date_RdoBtn2.FocusOutCheckMethod")));
            this.Denpyou_Date_RdoBtn2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Denpyou_Date_RdoBtn2.LinkedTextBox = "numTxtBox_Denpyou_Date";
            this.Denpyou_Date_RdoBtn2.Location = new System.Drawing.Point(117, 1);
            this.Denpyou_Date_RdoBtn2.Name = "Denpyou_Date_RdoBtn2";
            this.Denpyou_Date_RdoBtn2.PopupAfterExecute = null;
            this.Denpyou_Date_RdoBtn2.PopupBeforeExecute = null;
            this.Denpyou_Date_RdoBtn2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Denpyou_Date_RdoBtn2.PopupSearchSendParams")));
            this.Denpyou_Date_RdoBtn2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Denpyou_Date_RdoBtn2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Denpyou_Date_RdoBtn2.popupWindowSetting")));
            this.Denpyou_Date_RdoBtn2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Denpyou_Date_RdoBtn2.RegistCheckMethod")));
            this.Denpyou_Date_RdoBtn2.Size = new System.Drawing.Size(109, 17);
            this.Denpyou_Date_RdoBtn2.TabIndex = 1;
            this.Denpyou_Date_RdoBtn2.Tag = "伝票日付を選択します";
            this.Denpyou_Date_RdoBtn2.Text = "2.運搬終了日";
            this.Denpyou_Date_RdoBtn2.UseVisualStyleBackColor = true;
            this.Denpyou_Date_RdoBtn2.Value = "2";
            this.Denpyou_Date_RdoBtn2.CheckedChanged += new System.EventHandler(this.Denpyou_Date_RdoBtn2_CheckedChanged);
            // 
            // Denpyou_Date_RdoBtn1
            // 
            this.Denpyou_Date_RdoBtn1.AutoSize = true;
            this.Denpyou_Date_RdoBtn1.DefaultBackColor = System.Drawing.Color.Empty;
            this.Denpyou_Date_RdoBtn1.DisplayItemName = "";
            this.Denpyou_Date_RdoBtn1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Denpyou_Date_RdoBtn1.FocusOutCheckMethod")));
            this.Denpyou_Date_RdoBtn1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Denpyou_Date_RdoBtn1.LinkedTextBox = "numTxtBox_Denpyou_Date";
            this.Denpyou_Date_RdoBtn1.Location = new System.Drawing.Point(2, 1);
            this.Denpyou_Date_RdoBtn1.Name = "Denpyou_Date_RdoBtn1";
            this.Denpyou_Date_RdoBtn1.PopupAfterExecute = null;
            this.Denpyou_Date_RdoBtn1.PopupBeforeExecute = null;
            this.Denpyou_Date_RdoBtn1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Denpyou_Date_RdoBtn1.PopupSearchSendParams")));
            this.Denpyou_Date_RdoBtn1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Denpyou_Date_RdoBtn1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Denpyou_Date_RdoBtn1.popupWindowSetting")));
            this.Denpyou_Date_RdoBtn1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Denpyou_Date_RdoBtn1.RegistCheckMethod")));
            this.Denpyou_Date_RdoBtn1.Size = new System.Drawing.Size(109, 17);
            this.Denpyou_Date_RdoBtn1.TabIndex = 0;
            this.Denpyou_Date_RdoBtn1.Tag = "伝票日付を選択します";
            this.Denpyou_Date_RdoBtn1.Text = "1.交付年月日";
            this.Denpyou_Date_RdoBtn1.UseVisualStyleBackColor = true;
            this.Denpyou_Date_RdoBtn1.Value = "1";
            this.Denpyou_Date_RdoBtn1.CheckedChanged += new System.EventHandler(this.Denpyou_Date_RdoBtn1_CheckedChanged);
            // 
            // DenoyouDate_Label
            // 
            this.DenoyouDate_Label.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.DenoyouDate_Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DenoyouDate_Label.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DenoyouDate_Label.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.DenoyouDate_Label.ForeColor = System.Drawing.Color.White;
            this.DenoyouDate_Label.Location = new System.Drawing.Point(440, 0);
            this.DenoyouDate_Label.Name = "DenoyouDate_Label";
            this.DenoyouDate_Label.Size = new System.Drawing.Size(110, 20);
            this.DenoyouDate_Label.TabIndex = 3;
            this.DenoyouDate_Label.Text = "伝票日付";
            this.DenoyouDate_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.BackColor = System.Drawing.SystemColors.Window;
            this.dtpDateFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtpDateFrom.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.dtpDateFrom.Checked = false;
            this.dtpDateFrom.CustomFormat = "yyyy/MM/dd(ddd)";
            this.dtpDateFrom.DateTimeNowYear = "";
            this.dtpDateFrom.DefaultBackColor = System.Drawing.Color.Empty;
            this.dtpDateFrom.DisplayPopUp = null;
            this.dtpDateFrom.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtpDateFrom.FocusOutCheckMethod")));
            this.dtpDateFrom.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.dtpDateFrom.ForeColor = System.Drawing.Color.Black;
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateFrom.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtpDateFrom.IsInputErrorOccured = false;
            this.dtpDateFrom.Location = new System.Drawing.Point(555, 0);
            this.dtpDateFrom.MaxLength = 10;
            this.dtpDateFrom.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.NullValue = "";
            this.dtpDateFrom.PopupAfterExecute = null;
            this.dtpDateFrom.PopupBeforeExecute = null;
            this.dtpDateFrom.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dtpDateFrom.PopupSearchSendParams")));
            this.dtpDateFrom.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dtpDateFrom.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dtpDateFrom.popupWindowSetting")));
            this.dtpDateFrom.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtpDateFrom.RegistCheckMethod")));
            this.dtpDateFrom.Size = new System.Drawing.Size(138, 20);
            this.dtpDateFrom.TabIndex = 4;
            this.dtpDateFrom.Tag = "伝票日付Fromを入力してください";
            this.dtpDateFrom.Value = null;
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.Transparent;
            this.label38.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label38.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label38.ForeColor = System.Drawing.Color.Black;
            this.label38.Location = new System.Drawing.Point(695, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(20, 20);
            this.label38.TabIndex = 5;
            this.label38.Text = "～";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.BackColor = System.Drawing.SystemColors.Window;
            this.dtpDateTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtpDateTo.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.dtpDateTo.Checked = false;
            this.dtpDateTo.CustomFormat = "yyyy/MM/dd(ddd)";
            this.dtpDateTo.DateTimeNowYear = "";
            this.dtpDateTo.DefaultBackColor = System.Drawing.Color.Empty;
            this.dtpDateTo.DisplayPopUp = null;
            this.dtpDateTo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtpDateTo.FocusOutCheckMethod")));
            this.dtpDateTo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.dtpDateTo.ForeColor = System.Drawing.Color.Black;
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDateTo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dtpDateTo.IsInputErrorOccured = false;
            this.dtpDateTo.Location = new System.Drawing.Point(717, 0);
            this.dtpDateTo.MaxLength = 10;
            this.dtpDateTo.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.NullValue = "";
            this.dtpDateTo.PopupAfterExecute = null;
            this.dtpDateTo.PopupBeforeExecute = null;
            this.dtpDateTo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dtpDateTo.PopupSearchSendParams")));
            this.dtpDateTo.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dtpDateTo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dtpDateTo.popupWindowSetting")));
            this.dtpDateTo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtpDateTo.RegistCheckMethod")));
            this.dtpDateTo.Size = new System.Drawing.Size(138, 20);
            this.dtpDateTo.TabIndex = 6;
            this.dtpDateTo.Tag = "伝票日付Toを入力してください";
            this.dtpDateTo.Value = null;
            // 
            // numTxtBox_Denpyou_Date
            // 
            this.numTxtBox_Denpyou_Date.BackColor = System.Drawing.SystemColors.Window;
            this.numTxtBox_Denpyou_Date.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numTxtBox_Denpyou_Date.DefaultBackColor = System.Drawing.Color.Empty;
            this.numTxtBox_Denpyou_Date.DisplayItemName = "検索日付";
            this.numTxtBox_Denpyou_Date.DisplayPopUp = null;
            this.numTxtBox_Denpyou_Date.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("numTxtBox_Denpyou_Date.FocusOutCheckMethod")));
            this.numTxtBox_Denpyou_Date.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.numTxtBox_Denpyou_Date.ForeColor = System.Drawing.Color.Black;
            this.numTxtBox_Denpyou_Date.IsInputErrorOccured = false;
            this.numTxtBox_Denpyou_Date.LinkedRadioButtonArray = new string[] {
        "Denpyou_Date_RdoBtn1",
        "Denpyou_Date_RdoBtn2",
        "Denpyou_Date_RdoBtn3",
        "Denpyou_Date_RdoBtn4"};
            this.numTxtBox_Denpyou_Date.Location = new System.Drawing.Point(440, 24);
            this.numTxtBox_Denpyou_Date.Name = "numTxtBox_Denpyou_Date";
            this.numTxtBox_Denpyou_Date.PopupAfterExecute = null;
            this.numTxtBox_Denpyou_Date.PopupBeforeExecute = null;
            this.numTxtBox_Denpyou_Date.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("numTxtBox_Denpyou_Date.PopupSearchSendParams")));
            this.numTxtBox_Denpyou_Date.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.numTxtBox_Denpyou_Date.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("numTxtBox_Denpyou_Date.popupWindowSetting")));
            this.numTxtBox_Denpyou_Date.prevText = null;
            this.numTxtBox_Denpyou_Date.PrevText = null;
            rangeSettingDto2.Max = new decimal(new int[] {
            4,
            0,
            0,
            0});
            rangeSettingDto2.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numTxtBox_Denpyou_Date.RangeSetting = rangeSettingDto2;
            this.numTxtBox_Denpyou_Date.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("numTxtBox_Denpyou_Date.RegistCheckMethod")));
            this.numTxtBox_Denpyou_Date.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numTxtBox_Denpyou_Date.Size = new System.Drawing.Size(20, 20);
            this.numTxtBox_Denpyou_Date.TabIndex = 7;
            this.numTxtBox_Denpyou_Date.Tag = "【1～4】のいずれかで入力してください";
            this.numTxtBox_Denpyou_Date.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numTxtBox_Denpyou_Date.WordWrap = false;
            // 
            // customPanel1
            // 
            this.customPanel1.Controls.Add(this.label6);
            this.customPanel1.Controls.Add(this.numTxtBox_KsTsKn);
            this.customPanel1.Controls.Add(this.customPanel3);
            this.customPanel1.Controls.Add(this.DenoyouDate_Label);
            this.customPanel1.Controls.Add(this.numTxtBox_Denpyou_Date);
            this.customPanel1.Controls.Add(this.Denpyou_Date_Panel);
            this.customPanel1.Controls.Add(this.dtpDateFrom);
            this.customPanel1.Controls.Add(this.dtpDateTo);
            this.customPanel1.Controls.Add(this.label38);
            this.customPanel1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.customPanel1.Location = new System.Drawing.Point(0, 0);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(955, 90);
            this.customPanel1.TabIndex = 0;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.customPanel1);
            this.Name = "UIForm";
            this.SystemId = ((long)(277));
            this.Text = "UIForm";
            this.WindowId = r_framework.Const.WINDOW_ID.C_DENPYOU_HIMODUKE_ICHIRAN;
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.customPanel1, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.customPanel3.ResumeLayout(false);
            this.customPanel3.PerformLayout();
            this.Denpyou_Date_Panel.ResumeLayout(false);
            this.Denpyou_Date_Panel.PerformLayout();
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private r_framework.CustomControl.CustomPanel customPanel3;
        public r_framework.CustomControl.CustomRadioButton cstRdoBtn1;
        public r_framework.CustomControl.CustomRadioButton cstRdoBtn2;
        public r_framework.CustomControl.CustomRadioButton cstRdoBtn3;
        public r_framework.CustomControl.CustomNumericTextBox2 numTxtBox_KsTsKn;
        public r_framework.CustomControl.CustomRadioButton cstRdoBtn4;
        internal Label label6;
        public r_framework.CustomControl.CustomRadioButton cstRdoBtn10;
        public r_framework.CustomControl.CustomRadioButton cstRdoBtn9;
        public r_framework.CustomControl.CustomRadioButton cstRdoBtn8;
        public r_framework.CustomControl.CustomRadioButton cstRdoBtn7;
        public r_framework.CustomControl.CustomRadioButton cstRdoBtn6;
        public r_framework.CustomControl.CustomRadioButton cstRdoBtn5;
        public r_framework.CustomControl.CustomPanel Denpyou_Date_Panel;
        internal Label DenoyouDate_Label;
        private Label label38;
        public r_framework.CustomControl.CustomNumericTextBox2 numTxtBox_Denpyou_Date;
        public r_framework.CustomControl.CustomRadioButton Denpyou_Date_RdoBtn1;
        public r_framework.CustomControl.CustomRadioButton Denpyou_Date_RdoBtn4;
        public r_framework.CustomControl.CustomRadioButton Denpyou_Date_RdoBtn3;
        public r_framework.CustomControl.CustomRadioButton Denpyou_Date_RdoBtn2;
        private r_framework.CustomControl.CustomPanel customPanel1;
        internal r_framework.CustomControl.CustomDateTimePicker dtpDateTo;
        internal r_framework.CustomControl.CustomDateTimePicker dtpDateFrom;
    }
}