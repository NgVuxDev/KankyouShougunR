using System.Windows.Forms;
using System;

namespace Shougun.Core.SalesPayment.Denpyouichiran
{
    partial class DenpyouichiranForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DenpyouichiranForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            this.label3 = new System.Windows.Forms.Label();
            this.radbtn_Shuka = new r_framework.CustomControl.CustomRadioButton();
            this.radbtn_Ukeire = new r_framework.CustomControl.CustomRadioButton();
            this.radbtn_Uriageshiharai = new r_framework.CustomControl.CustomRadioButton();
            this.txtNum_DenpyoKind = new r_framework.CustomControl.CustomNumericTextBox2();
            this.txtNum_Denpyoukubun = new r_framework.CustomControl.CustomNumericTextBox2();
            this.radbtn_Uriage = new r_framework.CustomControl.CustomRadioButton();
            this.radbtn_Shihari = new r_framework.CustomControl.CustomRadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.radbtn_Subete = new r_framework.CustomControl.CustomRadioButton();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.radbtn_Dainou = new r_framework.CustomControl.CustomRadioButton();
            this.customPanel2 = new r_framework.CustomControl.CustomPanel();
            this.customPanel3 = new r_framework.CustomControl.CustomPanel();
            this.radbtn_All = new r_framework.CustomControl.CustomRadioButton();
            this.radbtn_True = new r_framework.CustomControl.CustomRadioButton();
            this.txtNum_KenshuMustKbn = new r_framework.CustomControl.CustomNumericTextBox2();
            this.radbtn_Falsa = new r_framework.CustomControl.CustomRadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.customPanel4 = new r_framework.CustomControl.CustomPanel();
            this.radbtn_KenshuZumi = new r_framework.CustomControl.CustomRadioButton();
            this.txtNum_KenshuJyoukyou = new r_framework.CustomControl.CustomNumericTextBox2();
            this.radbtn_MiKenshu = new r_framework.CustomControl.CustomRadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.radbtn_DenshuSubete = new r_framework.CustomControl.CustomRadioButton();
            this.customPanel1.SuspendLayout();
            this.customPanel2.SuspendLayout();
            this.customPanel3.SuspendLayout();
            this.customPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.searchString.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.FocusOutCheckMethod")));
            this.searchString.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.searchString.Location = new System.Drawing.Point(0, 3);
            this.searchString.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("searchString.PopupSearchSendParams")));
            this.searchString.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("searchString.popupWindowSetting")));
            this.searchString.ReadOnly = true;
            this.searchString.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.RegistCheckMethod")));
            this.searchString.Size = new System.Drawing.Size(740, 180);
            this.searchString.TabIndex = 1;
            this.searchString.TabStop = false;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Location = new System.Drawing.Point(4, 427);
            this.bt_ptn1.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn1.TabIndex = 51;
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn2.TabIndex = 61;
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Location = new System.Drawing.Point(404, 427);
            this.bt_ptn3.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn3.TabIndex = 71;
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Location = new System.Drawing.Point(604, 427);
            this.bt_ptn4.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn4.TabIndex = 81;
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Location = new System.Drawing.Point(804, 427);
            this.bt_ptn5.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn5.TabIndex = 91;
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.AutoSize = true;
            this.customSortHeader1.Location = new System.Drawing.Point(4, 157);
            this.customSortHeader1.TabIndex = 41;
            // 
            // customSearchHeader1
            // 
            this.customSearchHeader1.Location = new System.Drawing.Point(4, 182);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(676, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "伝票種類※";
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
            this.txtNum_DenpyoKind.DisplayItemName = "伝票種類";
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
        "radbtn_DenshuSubete"};
            this.txtNum_DenpyoKind.Location = new System.Drawing.Point(0, 0);
            this.txtNum_DenpyoKind.Name = "txtNum_DenpyoKind";
            this.txtNum_DenpyoKind.PopupAfterExecute = null;
            this.txtNum_DenpyoKind.PopupBeforeExecute = null;
            this.txtNum_DenpyoKind.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtNum_DenpyoKind.PopupSearchSendParams")));
            this.txtNum_DenpyoKind.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtNum_DenpyoKind.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtNum_DenpyoKind.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            5,
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
            this.txtNum_DenpyoKind.Tag = "【1~5】　のいずれかで入力してください";
            this.txtNum_DenpyoKind.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNum_DenpyoKind.WordWrap = false;
            this.txtNum_DenpyoKind.Validating += new System.ComponentModel.CancelEventHandler(this.txtNum_DenpyoKind_Validating);
            // 
            // txtNum_Denpyoukubun
            // 
            this.txtNum_Denpyoukubun.BackColor = System.Drawing.SystemColors.Window;
            this.txtNum_Denpyoukubun.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNum_Denpyoukubun.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtNum_Denpyoukubun.DisplayItemName = "伝票区分";
            this.txtNum_Denpyoukubun.DisplayPopUp = null;
            this.txtNum_Denpyoukubun.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_Denpyoukubun.FocusOutCheckMethod")));
            this.txtNum_Denpyoukubun.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtNum_Denpyoukubun.ForeColor = System.Drawing.Color.Black;
            this.txtNum_Denpyoukubun.IsInputErrorOccured = false;
            this.txtNum_Denpyoukubun.LinkedRadioButtonArray = new string[] {
        "radbtn_Uriage",
        "radbtn_Shihari",
        "radbtn_Subete"};
            this.txtNum_Denpyoukubun.Location = new System.Drawing.Point(0, 0);
            this.txtNum_Denpyoukubun.Name = "txtNum_Denpyoukubun";
            this.txtNum_Denpyoukubun.PopupAfterExecute = null;
            this.txtNum_Denpyoukubun.PopupBeforeExecute = null;
            this.txtNum_Denpyoukubun.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtNum_Denpyoukubun.PopupSearchSendParams")));
            this.txtNum_Denpyoukubun.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtNum_Denpyoukubun.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtNum_Denpyoukubun.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            3,
            0,
            0,
            0});
            rangeSettingDto2.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtNum_Denpyoukubun.RangeSetting = rangeSettingDto2;
            this.txtNum_Denpyoukubun.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_Denpyoukubun.RegistCheckMethod")));
            this.txtNum_Denpyoukubun.Size = new System.Drawing.Size(20, 20);
            this.txtNum_Denpyoukubun.TabIndex = 1;
            this.txtNum_Denpyoukubun.Tag = "【1～3】のいずれかで入力してください";
            this.txtNum_Denpyoukubun.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNum_Denpyoukubun.WordWrap = false;
            this.txtNum_Denpyoukubun.Validating += new System.ComponentModel.CancelEventHandler(this.txtNum_Denpyoukubun_Validating);
            // 
            // radbtn_Uriage
            // 
            this.radbtn_Uriage.AutoSize = true;
            this.radbtn_Uriage.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Uriage.DisplayItemName = "asdasd";
            this.radbtn_Uriage.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Uriage.FocusOutCheckMethod")));
            this.radbtn_Uriage.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_Uriage.LinkedTextBox = "txtNum_Denpyoukubun";
            this.radbtn_Uriage.Location = new System.Drawing.Point(22, 1);
            this.radbtn_Uriage.Name = "radbtn_Uriage";
            this.radbtn_Uriage.PopupAfterExecute = null;
            this.radbtn_Uriage.PopupBeforeExecute = null;
            this.radbtn_Uriage.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Uriage.PopupSearchSendParams")));
            this.radbtn_Uriage.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Uriage.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Uriage.popupWindowSetting")));
            this.radbtn_Uriage.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Uriage.RegistCheckMethod")));
            this.radbtn_Uriage.Size = new System.Drawing.Size(67, 17);
            this.radbtn_Uriage.TabIndex = 20;
            this.radbtn_Uriage.Tag = "伝票区分が「1.売上」の場合にはチェックを付けてください";
            this.radbtn_Uriage.Text = "1.売上";
            this.radbtn_Uriage.UseVisualStyleBackColor = true;
            this.radbtn_Uriage.Value = "1";
            // 
            // radbtn_Shihari
            // 
            this.radbtn_Shihari.AutoSize = true;
            this.radbtn_Shihari.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Shihari.DisplayItemName = "asdasd";
            this.radbtn_Shihari.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Shihari.FocusOutCheckMethod")));
            this.radbtn_Shihari.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_Shihari.LinkedTextBox = "txtNum_Denpyoukubun";
            this.radbtn_Shihari.Location = new System.Drawing.Point(95, 1);
            this.radbtn_Shihari.Name = "radbtn_Shihari";
            this.radbtn_Shihari.PopupAfterExecute = null;
            this.radbtn_Shihari.PopupBeforeExecute = null;
            this.radbtn_Shihari.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Shihari.PopupSearchSendParams")));
            this.radbtn_Shihari.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Shihari.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Shihari.popupWindowSetting")));
            this.radbtn_Shihari.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Shihari.RegistCheckMethod")));
            this.radbtn_Shihari.Size = new System.Drawing.Size(67, 17);
            this.radbtn_Shihari.TabIndex = 21;
            this.radbtn_Shihari.Tag = "伝票区分が「2.支払」の場合にはチェックを付けてください";
            this.radbtn_Shihari.Text = "2.支払";
            this.radbtn_Shihari.UseVisualStyleBackColor = true;
            this.radbtn_Shihari.Value = "2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(676, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "伝票区分※";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radbtn_Subete
            // 
            this.radbtn_Subete.AutoSize = true;
            this.radbtn_Subete.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Subete.DisplayItemName = "asdasd";
            this.radbtn_Subete.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Subete.FocusOutCheckMethod")));
            this.radbtn_Subete.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_Subete.LinkedTextBox = "txtNum_Denpyoukubun";
            this.radbtn_Subete.Location = new System.Drawing.Point(168, 1);
            this.radbtn_Subete.Name = "radbtn_Subete";
            this.radbtn_Subete.PopupAfterExecute = null;
            this.radbtn_Subete.PopupBeforeExecute = null;
            this.radbtn_Subete.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Subete.PopupSearchSendParams")));
            this.radbtn_Subete.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Subete.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Subete.popupWindowSetting")));
            this.radbtn_Subete.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Subete.RegistCheckMethod")));
            this.radbtn_Subete.Size = new System.Drawing.Size(67, 17);
            this.radbtn_Subete.TabIndex = 23;
            this.radbtn_Subete.Tag = "伝票区分が「3.全て」の場合にはチェックを付けてください";
            this.radbtn_Subete.Text = "3.全て";
            this.radbtn_Subete.UseVisualStyleBackColor = true;
            this.radbtn_Subete.Value = "3";
            // 
            // customPanel1
            // 
            this.customPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel1.Controls.Add(this.radbtn_DenshuSubete);
            this.customPanel1.Controls.Add(this.radbtn_Dainou);
            this.customPanel1.Controls.Add(this.radbtn_Ukeire);
            this.customPanel1.Controls.Add(this.radbtn_Shuka);
            this.customPanel1.Controls.Add(this.radbtn_Uriageshiharai);
            this.customPanel1.Controls.Add(this.txtNum_DenpyoKind);
            this.customPanel1.Location = new System.Drawing.Point(676, 20);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(326, 40);
            this.customPanel1.TabIndex = 1;
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
            // customPanel2
            // 
            this.customPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel2.Controls.Add(this.radbtn_Subete);
            this.customPanel2.Controls.Add(this.radbtn_Shihari);
            this.customPanel2.Controls.Add(this.txtNum_Denpyoukubun);
            this.customPanel2.Controls.Add(this.radbtn_Uriage);
            this.customPanel2.Location = new System.Drawing.Point(676, 80);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(326, 22);
            this.customPanel2.TabIndex = 11;
            // 
            // customPanel3
            // 
            this.customPanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel3.Controls.Add(this.radbtn_All);
            this.customPanel3.Controls.Add(this.radbtn_True);
            this.customPanel3.Controls.Add(this.txtNum_KenshuMustKbn);
            this.customPanel3.Controls.Add(this.radbtn_Falsa);
            this.customPanel3.Location = new System.Drawing.Point(676, 122);
            this.customPanel3.Name = "customPanel3";
            this.customPanel3.Size = new System.Drawing.Size(163, 36);
            this.customPanel3.TabIndex = 21;
            // 
            // radbtn_All
            // 
            this.radbtn_All.AutoSize = true;
            this.radbtn_All.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_All.DisplayItemName = "asdasd";
            this.radbtn_All.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_All.FocusOutCheckMethod")));
            this.radbtn_All.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_All.LinkedTextBox = "txtNum_KenshuMustKbn";
            this.radbtn_All.Location = new System.Drawing.Point(22, 18);
            this.radbtn_All.Name = "radbtn_All";
            this.radbtn_All.PopupAfterExecute = null;
            this.radbtn_All.PopupBeforeExecute = null;
            this.radbtn_All.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_All.PopupSearchSendParams")));
            this.radbtn_All.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_All.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_All.popupWindowSetting")));
            this.radbtn_All.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_All.RegistCheckMethod")));
            this.radbtn_All.Size = new System.Drawing.Size(67, 17);
            this.radbtn_All.TabIndex = 22;
            this.radbtn_All.Tag = "要検収に関わらずすべての情報を表示する場合にはチェックを付けてください";
            this.radbtn_All.Text = "3.全て";
            this.radbtn_All.UseVisualStyleBackColor = true;
            this.radbtn_All.Value = "3";
            // 
            // radbtn_True
            // 
            this.radbtn_True.AutoSize = true;
            this.radbtn_True.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_True.DisplayItemName = "asdasd";
            this.radbtn_True.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_True.FocusOutCheckMethod")));
            this.radbtn_True.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_True.LinkedTextBox = "txtNum_KenshuMustKbn";
            this.radbtn_True.Location = new System.Drawing.Point(95, 1);
            this.radbtn_True.Name = "radbtn_True";
            this.radbtn_True.PopupAfterExecute = null;
            this.radbtn_True.PopupBeforeExecute = null;
            this.radbtn_True.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_True.PopupSearchSendParams")));
            this.radbtn_True.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_True.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_True.popupWindowSetting")));
            this.radbtn_True.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_True.RegistCheckMethod")));
            this.radbtn_True.Size = new System.Drawing.Size(53, 17);
            this.radbtn_True.TabIndex = 21;
            this.radbtn_True.Tag = "要検収にチェックが付いている情報を表示する場合にはチェックを付けてください";
            this.radbtn_True.Text = "2.有";
            this.radbtn_True.UseVisualStyleBackColor = true;
            this.radbtn_True.Value = "2";
            // 
            // txtNum_KenshuMustKbn
            // 
            this.txtNum_KenshuMustKbn.BackColor = System.Drawing.SystemColors.Window;
            this.txtNum_KenshuMustKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNum_KenshuMustKbn.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtNum_KenshuMustKbn.DisplayItemName = "検収有無";
            this.txtNum_KenshuMustKbn.DisplayPopUp = null;
            this.txtNum_KenshuMustKbn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_KenshuMustKbn.FocusOutCheckMethod")));
            this.txtNum_KenshuMustKbn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtNum_KenshuMustKbn.ForeColor = System.Drawing.Color.Black;
            this.txtNum_KenshuMustKbn.IsInputErrorOccured = false;
            this.txtNum_KenshuMustKbn.LinkedRadioButtonArray = new string[] {
        "radbtn_Falsa",
        "radbtn_True",
        "radbtn_All"};
            this.txtNum_KenshuMustKbn.Location = new System.Drawing.Point(0, 0);
            this.txtNum_KenshuMustKbn.Name = "txtNum_KenshuMustKbn";
            this.txtNum_KenshuMustKbn.PopupAfterExecute = null;
            this.txtNum_KenshuMustKbn.PopupBeforeExecute = null;
            this.txtNum_KenshuMustKbn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtNum_KenshuMustKbn.PopupSearchSendParams")));
            this.txtNum_KenshuMustKbn.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtNum_KenshuMustKbn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtNum_KenshuMustKbn.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            3,
            0,
            0,
            0});
            rangeSettingDto3.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtNum_KenshuMustKbn.RangeSetting = rangeSettingDto3;
            this.txtNum_KenshuMustKbn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_KenshuMustKbn.RegistCheckMethod")));
            this.txtNum_KenshuMustKbn.Size = new System.Drawing.Size(20, 20);
            this.txtNum_KenshuMustKbn.TabIndex = 1;
            this.txtNum_KenshuMustKbn.Tag = "【1～3】のいずれかで入力してください";
            this.txtNum_KenshuMustKbn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNum_KenshuMustKbn.WordWrap = false;
            this.txtNum_KenshuMustKbn.Validating += new System.ComponentModel.CancelEventHandler(this.txtNum_KenshuMustKbn_Validating);
            // 
            // radbtn_Falsa
            // 
            this.radbtn_Falsa.AutoSize = true;
            this.radbtn_Falsa.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Falsa.DisplayItemName = "asdasd";
            this.radbtn_Falsa.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Falsa.FocusOutCheckMethod")));
            this.radbtn_Falsa.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_Falsa.LinkedTextBox = "txtNum_KenshuMustKbn";
            this.radbtn_Falsa.Location = new System.Drawing.Point(22, 1);
            this.radbtn_Falsa.Name = "radbtn_Falsa";
            this.radbtn_Falsa.PopupAfterExecute = null;
            this.radbtn_Falsa.PopupBeforeExecute = null;
            this.radbtn_Falsa.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Falsa.PopupSearchSendParams")));
            this.radbtn_Falsa.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Falsa.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Falsa.popupWindowSetting")));
            this.radbtn_Falsa.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Falsa.RegistCheckMethod")));
            this.radbtn_Falsa.Size = new System.Drawing.Size(53, 17);
            this.radbtn_Falsa.TabIndex = 20;
            this.radbtn_Falsa.Tag = "要検収にチェックが付いていない情報を表示する場合にはチェックを付けてください";
            this.radbtn_Falsa.Text = "1.無";
            this.radbtn_Falsa.UseVisualStyleBackColor = true;
            this.radbtn_Falsa.Value = "1";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(676, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 24;
            this.label2.Text = "検収有無※";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel4
            // 
            this.customPanel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel4.Controls.Add(this.radbtn_KenshuZumi);
            this.customPanel4.Controls.Add(this.txtNum_KenshuJyoukyou);
            this.customPanel4.Controls.Add(this.radbtn_MiKenshu);
            this.customPanel4.Location = new System.Drawing.Point(840, 122);
            this.customPanel4.Name = "customPanel4";
            this.customPanel4.Size = new System.Drawing.Size(162, 36);
            this.customPanel4.TabIndex = 31;
            // 
            // radbtn_KenshuZumi
            // 
            this.radbtn_KenshuZumi.AutoSize = true;
            this.radbtn_KenshuZumi.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_KenshuZumi.DisplayItemName = "asdasd";
            this.radbtn_KenshuZumi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_KenshuZumi.FocusOutCheckMethod")));
            this.radbtn_KenshuZumi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_KenshuZumi.LinkedTextBox = "txtNum_KenshuJyoukyou";
            this.radbtn_KenshuZumi.Location = new System.Drawing.Point(22, 18);
            this.radbtn_KenshuZumi.Name = "radbtn_KenshuZumi";
            this.radbtn_KenshuZumi.PopupAfterExecute = null;
            this.radbtn_KenshuZumi.PopupBeforeExecute = null;
            this.radbtn_KenshuZumi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_KenshuZumi.PopupSearchSendParams")));
            this.radbtn_KenshuZumi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_KenshuZumi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_KenshuZumi.popupWindowSetting")));
            this.radbtn_KenshuZumi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_KenshuZumi.RegistCheckMethod")));
            this.radbtn_KenshuZumi.Size = new System.Drawing.Size(81, 17);
            this.radbtn_KenshuZumi.TabIndex = 22;
            this.radbtn_KenshuZumi.Tag = "検収状況が「1.検収済」の場合にはチェックを付けてください";
            this.radbtn_KenshuZumi.Text = "2.検収済";
            this.radbtn_KenshuZumi.UseVisualStyleBackColor = true;
            this.radbtn_KenshuZumi.Value = "2";
            // 
            // txtNum_KenshuJyoukyou
            // 
            this.txtNum_KenshuJyoukyou.BackColor = System.Drawing.SystemColors.Window;
            this.txtNum_KenshuJyoukyou.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNum_KenshuJyoukyou.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtNum_KenshuJyoukyou.DisplayItemName = "検収状況";
            this.txtNum_KenshuJyoukyou.DisplayPopUp = null;
            this.txtNum_KenshuJyoukyou.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_KenshuJyoukyou.FocusOutCheckMethod")));
            this.txtNum_KenshuJyoukyou.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtNum_KenshuJyoukyou.ForeColor = System.Drawing.Color.Black;
            this.txtNum_KenshuJyoukyou.IsInputErrorOccured = false;
            this.txtNum_KenshuJyoukyou.LinkedRadioButtonArray = new string[] {
        "radbtn_MiKenshu",
        "radbtn_KenshuZumi"};
            this.txtNum_KenshuJyoukyou.Location = new System.Drawing.Point(0, 0);
            this.txtNum_KenshuJyoukyou.Name = "txtNum_KenshuJyoukyou";
            this.txtNum_KenshuJyoukyou.PopupAfterExecute = null;
            this.txtNum_KenshuJyoukyou.PopupBeforeExecute = null;
            this.txtNum_KenshuJyoukyou.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtNum_KenshuJyoukyou.PopupSearchSendParams")));
            this.txtNum_KenshuJyoukyou.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtNum_KenshuJyoukyou.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtNum_KenshuJyoukyou.popupWindowSetting")));
            rangeSettingDto4.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto4.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtNum_KenshuJyoukyou.RangeSetting = rangeSettingDto4;
            this.txtNum_KenshuJyoukyou.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_KenshuJyoukyou.RegistCheckMethod")));
            this.txtNum_KenshuJyoukyou.Size = new System.Drawing.Size(20, 20);
            this.txtNum_KenshuJyoukyou.TabIndex = 1;
            this.txtNum_KenshuJyoukyou.Tag = "【1～2】のいずれかで入力してください";
            this.txtNum_KenshuJyoukyou.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNum_KenshuJyoukyou.WordWrap = false;
            this.txtNum_KenshuJyoukyou.Validating += new System.ComponentModel.CancelEventHandler(this.txtNum_KenshuJyoukyou_Validating);
            // 
            // radbtn_MiKenshu
            // 
            this.radbtn_MiKenshu.AutoSize = true;
            this.radbtn_MiKenshu.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_MiKenshu.DisplayItemName = "asdasd";
            this.radbtn_MiKenshu.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_MiKenshu.FocusOutCheckMethod")));
            this.radbtn_MiKenshu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_MiKenshu.LinkedTextBox = "txtNum_KenshuJyoukyou";
            this.radbtn_MiKenshu.Location = new System.Drawing.Point(22, 1);
            this.radbtn_MiKenshu.Name = "radbtn_MiKenshu";
            this.radbtn_MiKenshu.PopupAfterExecute = null;
            this.radbtn_MiKenshu.PopupBeforeExecute = null;
            this.radbtn_MiKenshu.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_MiKenshu.PopupSearchSendParams")));
            this.radbtn_MiKenshu.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_MiKenshu.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_MiKenshu.popupWindowSetting")));
            this.radbtn_MiKenshu.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_MiKenshu.RegistCheckMethod")));
            this.radbtn_MiKenshu.Size = new System.Drawing.Size(81, 17);
            this.radbtn_MiKenshu.TabIndex = 20;
            this.radbtn_MiKenshu.Tag = "検収状況が「1.未検収」の場合にはチェックを付けてください";
            this.radbtn_MiKenshu.Text = "1.未検収";
            this.radbtn_MiKenshu.UseVisualStyleBackColor = true;
            this.radbtn_MiKenshu.Value = "1";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(840, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 26;
            this.label4.Text = "検収状況※";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radbtn_DenshuSubete
            // 
            this.radbtn_DenshuSubete.AutoSize = true;
            this.radbtn_DenshuSubete.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_DenshuSubete.DisplayItemName = "5.受入+出荷+売上/支払";
            this.radbtn_DenshuSubete.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_DenshuSubete.FocusOutCheckMethod")));
            this.radbtn_DenshuSubete.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_DenshuSubete.LinkedTextBox = "txtNum_DenpyoKind";
            this.radbtn_DenshuSubete.Location = new System.Drawing.Point(94, 21);
            this.radbtn_DenshuSubete.Name = "radbtn_DenshuSubete";
            this.radbtn_DenshuSubete.PopupAfterExecute = null;
            this.radbtn_DenshuSubete.PopupBeforeExecute = null;
            this.radbtn_DenshuSubete.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_DenshuSubete.PopupSearchSendParams")));
            this.radbtn_DenshuSubete.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_DenshuSubete.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_DenshuSubete.popupWindowSetting")));
            this.radbtn_DenshuSubete.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_DenshuSubete.RegistCheckMethod")));
            this.radbtn_DenshuSubete.Size = new System.Drawing.Size(172, 17);
            this.radbtn_DenshuSubete.TabIndex = 21;
            this.radbtn_DenshuSubete.Tag = "伝票種類が「5.受入+出荷+売上/支払」の場合にはチェックを付けてください";
            this.radbtn_DenshuSubete.Text = "5.受入+出荷+売上/支払";
            this.radbtn_DenshuSubete.UseVisualStyleBackColor = true;
            this.radbtn_DenshuSubete.Value = "5";
            // 
            // DenpyouichiranForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 458);
            this.Controls.Add(this.customPanel4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.customPanel3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.customPanel2);
            this.Controls.Add(this.customPanel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Name = "DenpyouichiranForm";
            this.Text = "UIForm";
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.customPanel1, 0);
            this.Controls.SetChildIndex(this.customPanel2, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.customPanel3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.customPanel4, 0);
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.customPanel2.ResumeLayout(false);
            this.customPanel2.PerformLayout();
            this.customPanel3.ResumeLayout(false);
            this.customPanel3.PerformLayout();
            this.customPanel4.ResumeLayout(false);
            this.customPanel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal Label label3;
        public r_framework.CustomControl.CustomRadioButton radbtn_Shuka;
        public r_framework.CustomControl.CustomRadioButton radbtn_Ukeire;
        public r_framework.CustomControl.CustomRadioButton radbtn_Uriageshiharai;
        public r_framework.CustomControl.CustomNumericTextBox2 txtNum_DenpyoKind;
        public r_framework.CustomControl.CustomNumericTextBox2 txtNum_Denpyoukubun;
        public r_framework.CustomControl.CustomRadioButton radbtn_Uriage;
        public r_framework.CustomControl.CustomRadioButton radbtn_Shihari;
        internal Label label1;
        public r_framework.CustomControl.CustomRadioButton radbtn_Subete;
        private r_framework.CustomControl.CustomPanel customPanel1;
        public r_framework.CustomControl.CustomRadioButton radbtn_Dainou;
        public r_framework.CustomControl.CustomRadioButton radbtn_All;
        public r_framework.CustomControl.CustomRadioButton radbtn_True;
        public r_framework.CustomControl.CustomNumericTextBox2 txtNum_KenshuMustKbn;
        public r_framework.CustomControl.CustomRadioButton radbtn_Falsa;
        internal Label label2;
        public r_framework.CustomControl.CustomRadioButton radbtn_KenshuZumi;
        public r_framework.CustomControl.CustomNumericTextBox2 txtNum_KenshuJyoukyou;
        public r_framework.CustomControl.CustomRadioButton radbtn_MiKenshu;
        internal Label label4;
        public r_framework.CustomControl.CustomPanel customPanel3;
        public r_framework.CustomControl.CustomPanel customPanel4;
        internal r_framework.CustomControl.CustomPanel customPanel2;
        public r_framework.CustomControl.CustomRadioButton radbtn_DenshuSubete;
    }
}