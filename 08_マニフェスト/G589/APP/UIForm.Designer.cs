using System.Windows.Forms;
using System;

namespace Shougun.Core.PaperManifest.Himodukeichiran
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
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto5 = new r_framework.Dto.RangeSettingDto();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNum_ChushutuTaishouKbn = new r_framework.CustomControl.CustomNumericTextBox2();
            this.radbtn_Ijimanifest = new r_framework.CustomControl.CustomRadioButton();
            this.radbtn_Nijimanifest = new r_framework.CustomControl.CustomRadioButton();
            this.customPanel2 = new r_framework.CustomControl.CustomPanel();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.radbtn_All = new r_framework.CustomControl.CustomRadioButton();
            this.radbtn_Mi = new r_framework.CustomControl.CustomRadioButton();
            this.txtNum_HimodukeJyoukyou = new r_framework.CustomControl.CustomNumericTextBox2();
            this.radbtn_Sumi = new r_framework.CustomControl.CustomRadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.cantxt_KohuNo = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.lbl_KohuNo = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.customPanel3 = new r_framework.CustomControl.CustomPanel();
            this.radbtn_SaishuShuryouDate = new r_framework.CustomControl.CustomRadioButton();
            this.radbtn_ShobunShuryouDate = new r_framework.CustomControl.CustomRadioButton();
            this.radbtn_UnpanShuryouDate = new r_framework.CustomControl.CustomRadioButton();
            this.txtNum_ChushutuHiduke = new r_framework.CustomControl.CustomNumericTextBox2();
            this.radbtn_KoufuDate = new r_framework.CustomControl.CustomRadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.customPanel4 = new r_framework.CustomControl.CustomPanel();
            this.radbtn_AllManifest = new r_framework.CustomControl.CustomRadioButton();
            this.radbtn_Denshi = new r_framework.CustomControl.CustomRadioButton();
            this.radbtn_Kenpai = new r_framework.CustomControl.CustomRadioButton();
            this.radbtn_SanpaiTumikae = new r_framework.CustomControl.CustomRadioButton();
            this.txtNum_ManifestShurui = new r_framework.CustomControl.CustomNumericTextBox2();
            this.radbtn_SanpaiChokko = new r_framework.CustomControl.CustomRadioButton();
            this.HIDUKE_FROM = new r_framework.CustomControl.CustomDateTimePicker();
            this.HIDUKE_TO = new r_framework.CustomControl.CustomDateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.customPanel5 = new r_framework.CustomControl.CustomPanel();
            this.radbtn_SaishuShobunBasho = new r_framework.CustomControl.CustomRadioButton();
            this.radbtn_ShobunJutakusha = new r_framework.CustomControl.CustomRadioButton();
            this.radbtn_UnpanJutakusha = new r_framework.CustomControl.CustomRadioButton();
            this.txtNum_ChushutuGyosha = new r_framework.CustomControl.CustomNumericTextBox2();
            this.radbtn_HaishuJigyosha = new r_framework.CustomControl.CustomRadioButton();
            this.cbtn_GenbaSan = new r_framework.CustomControl.CustomPopupOpenButton();
            this.ctxt_GenbaName = new r_framework.CustomControl.CustomTextBox();
            this.cantxt_GenbaCd = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.cbtn_GyousyaSan = new r_framework.CustomControl.CustomPopupOpenButton();
            this.ctxt_GyousyaName = new r_framework.CustomControl.CustomTextBox();
            this.cantxt_GyousyaCd = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.cbtn_JigyoubaSan = new r_framework.CustomControl.CustomPopupOpenButton();
            this.customPanel2.SuspendLayout();
            this.customPanel1.SuspendLayout();
            this.customPanel3.SuspendLayout();
            this.customPanel4.SuspendLayout();
            this.customPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.searchString.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.FocusOutCheckMethod")));
            this.searchString.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.searchString.Location = new System.Drawing.Point(442, 75);
            this.searchString.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("searchString.PopupSearchSendParams")));
            this.searchString.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("searchString.popupWindowSetting")));
            this.searchString.ReadOnly = true;
            this.searchString.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.RegistCheckMethod")));
            this.searchString.Size = new System.Drawing.Size(556, 23);
            this.searchString.TabIndex = 1;
            this.searchString.TabStop = false;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Location = new System.Drawing.Point(4, 427);
            this.bt_ptn1.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn1.TabIndex = 12;
            this.bt_ptn1.Visible = false;
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn2.TabIndex = 13;
            this.bt_ptn2.Visible = false;
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Location = new System.Drawing.Point(404, 427);
            this.bt_ptn3.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn3.TabIndex = 14;
            this.bt_ptn3.Visible = false;
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Location = new System.Drawing.Point(604, 427);
            this.bt_ptn4.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn4.TabIndex = 15;
            this.bt_ptn4.Visible = false;
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Location = new System.Drawing.Point(804, 427);
            this.bt_ptn5.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn5.TabIndex = 16;
            this.bt_ptn5.Visible = false;
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.AutoSize = true;
            this.customSortHeader1.Location = new System.Drawing.Point(4, 157);
            this.customSortHeader1.TabIndex = 32;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "抽出対象区分";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNum_ChushutuTaishouKbn
            // 
            this.txtNum_ChushutuTaishouKbn.BackColor = System.Drawing.SystemColors.Window;
            this.txtNum_ChushutuTaishouKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNum_ChushutuTaishouKbn.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtNum_ChushutuTaishouKbn.DisplayItemName = "抽出対象区分";
            this.txtNum_ChushutuTaishouKbn.DisplayPopUp = null;
            this.txtNum_ChushutuTaishouKbn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_ChushutuTaishouKbn.FocusOutCheckMethod")));
            this.txtNum_ChushutuTaishouKbn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtNum_ChushutuTaishouKbn.ForeColor = System.Drawing.Color.Black;
            this.txtNum_ChushutuTaishouKbn.IsInputErrorOccured = false;
            this.txtNum_ChushutuTaishouKbn.LinkedRadioButtonArray = new string[] {
        "radbtn_Ijimanifest",
        "radbtn_Nijimanifest"};
            this.txtNum_ChushutuTaishouKbn.Location = new System.Drawing.Point(-1, -1);
            this.txtNum_ChushutuTaishouKbn.Name = "txtNum_ChushutuTaishouKbn";
            this.txtNum_ChushutuTaishouKbn.PopupAfterExecute = null;
            this.txtNum_ChushutuTaishouKbn.PopupBeforeExecute = null;
            this.txtNum_ChushutuTaishouKbn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtNum_ChushutuTaishouKbn.PopupSearchSendParams")));
            this.txtNum_ChushutuTaishouKbn.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtNum_ChushutuTaishouKbn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtNum_ChushutuTaishouKbn.popupWindowSetting")));
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
            this.txtNum_ChushutuTaishouKbn.RangeSetting = rangeSettingDto1;
            this.txtNum_ChushutuTaishouKbn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_ChushutuTaishouKbn.RegistCheckMethod")));
            this.txtNum_ChushutuTaishouKbn.Size = new System.Drawing.Size(20, 20);
            this.txtNum_ChushutuTaishouKbn.TabIndex = 0;
            this.txtNum_ChushutuTaishouKbn.Tag = "【1～2】のいずれかで入力してください";
            this.txtNum_ChushutuTaishouKbn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNum_ChushutuTaishouKbn.WordWrap = false;
            this.txtNum_ChushutuTaishouKbn.TextChanged += new System.EventHandler(this.txtNum_ChushutuTaishouKbn_TextChanged);
            this.txtNum_ChushutuTaishouKbn.Validated += new System.EventHandler(this.txtNum_ChushutuTaishouKbn_Validated);
            // 
            // radbtn_Ijimanifest
            // 
            this.radbtn_Ijimanifest.AutoSize = true;
            this.radbtn_Ijimanifest.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Ijimanifest.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Ijimanifest.FocusOutCheckMethod")));
            this.radbtn_Ijimanifest.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_Ijimanifest.LinkedTextBox = "txtNum_ChushutuTaishouKbn";
            this.radbtn_Ijimanifest.Location = new System.Drawing.Point(22, 1);
            this.radbtn_Ijimanifest.Name = "radbtn_Ijimanifest";
            this.radbtn_Ijimanifest.PopupAfterExecute = null;
            this.radbtn_Ijimanifest.PopupBeforeExecute = null;
            this.radbtn_Ijimanifest.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Ijimanifest.PopupSearchSendParams")));
            this.radbtn_Ijimanifest.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Ijimanifest.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Ijimanifest.popupWindowSetting")));
            this.radbtn_Ijimanifest.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Ijimanifest.RegistCheckMethod")));
            this.radbtn_Ijimanifest.Size = new System.Drawing.Size(151, 17);
            this.radbtn_Ijimanifest.TabIndex = 0;
            this.radbtn_Ijimanifest.Tag = "抽出対象区分が「1.一次マニフェスト」の場合にはチェックを付けてください";
            this.radbtn_Ijimanifest.Text = "1.一次マニフェスト";
            this.radbtn_Ijimanifest.UseVisualStyleBackColor = true;
            this.radbtn_Ijimanifest.Value = "1";
            // 
            // radbtn_Nijimanifest
            // 
            this.radbtn_Nijimanifest.AutoSize = true;
            this.radbtn_Nijimanifest.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Nijimanifest.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Nijimanifest.FocusOutCheckMethod")));
            this.radbtn_Nijimanifest.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_Nijimanifest.LinkedTextBox = "txtNum_ChushutuTaishouKbn";
            this.radbtn_Nijimanifest.Location = new System.Drawing.Point(168, 1);
            this.radbtn_Nijimanifest.Name = "radbtn_Nijimanifest";
            this.radbtn_Nijimanifest.PopupAfterExecute = null;
            this.radbtn_Nijimanifest.PopupBeforeExecute = null;
            this.radbtn_Nijimanifest.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Nijimanifest.PopupSearchSendParams")));
            this.radbtn_Nijimanifest.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Nijimanifest.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Nijimanifest.popupWindowSetting")));
            this.radbtn_Nijimanifest.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Nijimanifest.RegistCheckMethod")));
            this.radbtn_Nijimanifest.Size = new System.Drawing.Size(151, 17);
            this.radbtn_Nijimanifest.TabIndex = 302;
            this.radbtn_Nijimanifest.Tag = "抽出対象区分が「2.二次マニフェスト」の場合にはチェックを付けてください";
            this.radbtn_Nijimanifest.Text = "2.二次マニフェスト";
            this.radbtn_Nijimanifest.UseVisualStyleBackColor = true;
            this.radbtn_Nijimanifest.Value = "2";
            // 
            // customPanel2
            // 
            this.customPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel2.Controls.Add(this.radbtn_Nijimanifest);
            this.customPanel2.Controls.Add(this.txtNum_ChushutuTaishouKbn);
            this.customPanel2.Controls.Add(this.radbtn_Ijimanifest);
            this.customPanel2.Location = new System.Drawing.Point(138, 0);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(319, 20);
            this.customPanel2.TabIndex = 4;
            // 
            // customPanel1
            // 
            this.customPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel1.Controls.Add(this.radbtn_All);
            this.customPanel1.Controls.Add(this.radbtn_Mi);
            this.customPanel1.Controls.Add(this.txtNum_HimodukeJyoukyou);
            this.customPanel1.Controls.Add(this.radbtn_Sumi);
            this.customPanel1.Location = new System.Drawing.Point(536, 0);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(185, 20);
            this.customPanel1.TabIndex = 5;
            // 
            // radbtn_All
            // 
            this.radbtn_All.AutoSize = true;
            this.radbtn_All.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_All.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_All.FocusOutCheckMethod")));
            this.radbtn_All.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_All.LinkedTextBox = "txtNum_HimodukeJyoukyou";
            this.radbtn_All.Location = new System.Drawing.Point(120, 1);
            this.radbtn_All.Name = "radbtn_All";
            this.radbtn_All.PopupAfterExecute = null;
            this.radbtn_All.PopupBeforeExecute = null;
            this.radbtn_All.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_All.PopupSearchSendParams")));
            this.radbtn_All.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_All.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_All.popupWindowSetting")));
            this.radbtn_All.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_All.RegistCheckMethod")));
            this.radbtn_All.Size = new System.Drawing.Size(67, 17);
            this.radbtn_All.TabIndex = 305;
            this.radbtn_All.Tag = "紐付状況が「3.全て」の場合にはチェックを付けてください";
            this.radbtn_All.Text = "3.全て";
            this.radbtn_All.UseVisualStyleBackColor = true;
            this.radbtn_All.Value = "3";
            // 
            // radbtn_Mi
            // 
            this.radbtn_Mi.AutoSize = true;
            this.radbtn_Mi.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Mi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Mi.FocusOutCheckMethod")));
            this.radbtn_Mi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_Mi.LinkedTextBox = "txtNum_HimodukeJyoukyou";
            this.radbtn_Mi.Location = new System.Drawing.Point(71, 1);
            this.radbtn_Mi.Name = "radbtn_Mi";
            this.radbtn_Mi.PopupAfterExecute = null;
            this.radbtn_Mi.PopupBeforeExecute = null;
            this.radbtn_Mi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Mi.PopupSearchSendParams")));
            this.radbtn_Mi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Mi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Mi.popupWindowSetting")));
            this.radbtn_Mi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Mi.RegistCheckMethod")));
            this.radbtn_Mi.Size = new System.Drawing.Size(53, 17);
            this.radbtn_Mi.TabIndex = 304;
            this.radbtn_Mi.Tag = "紐付状況が「2.未」の場合にはチェックを付けてください";
            this.radbtn_Mi.Text = "2.未";
            this.radbtn_Mi.UseVisualStyleBackColor = true;
            this.radbtn_Mi.Value = "2";
            // 
            // txtNum_HimodukeJyoukyou
            // 
            this.txtNum_HimodukeJyoukyou.BackColor = System.Drawing.SystemColors.Window;
            this.txtNum_HimodukeJyoukyou.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNum_HimodukeJyoukyou.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtNum_HimodukeJyoukyou.DisplayItemName = "紐付状況";
            this.txtNum_HimodukeJyoukyou.DisplayPopUp = null;
            this.txtNum_HimodukeJyoukyou.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_HimodukeJyoukyou.FocusOutCheckMethod")));
            this.txtNum_HimodukeJyoukyou.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtNum_HimodukeJyoukyou.ForeColor = System.Drawing.Color.Black;
            this.txtNum_HimodukeJyoukyou.IsInputErrorOccured = false;
            this.txtNum_HimodukeJyoukyou.LinkedRadioButtonArray = new string[] {
        "radbtn_Sumi",
        "radbtn_Mi",
        "radbtn_All"};
            this.txtNum_HimodukeJyoukyou.Location = new System.Drawing.Point(-1, -1);
            this.txtNum_HimodukeJyoukyou.Name = "txtNum_HimodukeJyoukyou";
            this.txtNum_HimodukeJyoukyou.PopupAfterExecute = null;
            this.txtNum_HimodukeJyoukyou.PopupBeforeExecute = null;
            this.txtNum_HimodukeJyoukyou.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtNum_HimodukeJyoukyou.PopupSearchSendParams")));
            this.txtNum_HimodukeJyoukyou.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtNum_HimodukeJyoukyou.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtNum_HimodukeJyoukyou.popupWindowSetting")));
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
            this.txtNum_HimodukeJyoukyou.RangeSetting = rangeSettingDto2;
            this.txtNum_HimodukeJyoukyou.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_HimodukeJyoukyou.RegistCheckMethod")));
            this.txtNum_HimodukeJyoukyou.Size = new System.Drawing.Size(20, 20);
            this.txtNum_HimodukeJyoukyou.TabIndex = 1;
            this.txtNum_HimodukeJyoukyou.Tag = "【1～3】のいずれかで入力してください";
            this.txtNum_HimodukeJyoukyou.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNum_HimodukeJyoukyou.WordWrap = false;
            this.txtNum_HimodukeJyoukyou.Validated += new System.EventHandler(this.txtNum_HimodukeJyoukyou_Validated);
            // 
            // radbtn_Sumi
            // 
            this.radbtn_Sumi.AutoSize = true;
            this.radbtn_Sumi.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Sumi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Sumi.FocusOutCheckMethod")));
            this.radbtn_Sumi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_Sumi.LinkedTextBox = "txtNum_HimodukeJyoukyou";
            this.radbtn_Sumi.Location = new System.Drawing.Point(22, 1);
            this.radbtn_Sumi.Name = "radbtn_Sumi";
            this.radbtn_Sumi.PopupAfterExecute = null;
            this.radbtn_Sumi.PopupBeforeExecute = null;
            this.radbtn_Sumi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Sumi.PopupSearchSendParams")));
            this.radbtn_Sumi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Sumi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Sumi.popupWindowSetting")));
            this.radbtn_Sumi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Sumi.RegistCheckMethod")));
            this.radbtn_Sumi.Size = new System.Drawing.Size(53, 17);
            this.radbtn_Sumi.TabIndex = 303;
            this.radbtn_Sumi.Tag = "紐付状況が「1.済」の場合にはチェックを付けてください";
            this.radbtn_Sumi.Text = "1.済";
            this.radbtn_Sumi.UseVisualStyleBackColor = true;
            this.radbtn_Sumi.Value = "1";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(464, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 20);
            this.label2.TabIndex = 29;
            this.label2.Text = "紐付状況";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cantxt_KohuNo
            // 
            this.cantxt_KohuNo.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_KohuNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_KohuNo.ChangeUpperCase = true;
            this.cantxt_KohuNo.CharacterLimitList = null;
            this.cantxt_KohuNo.CharactersNumber = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.cantxt_KohuNo.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_KohuNo.DisplayPopUp = null;
            this.cantxt_KohuNo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_KohuNo.FocusOutCheckMethod")));
            this.cantxt_KohuNo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.cantxt_KohuNo.ForeColor = System.Drawing.Color.Black;
            this.cantxt_KohuNo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cantxt_KohuNo.IsInputErrorOccured = false;
            this.cantxt_KohuNo.Location = new System.Drawing.Point(798, 0);
            this.cantxt_KohuNo.MaxLength = 11;
            this.cantxt_KohuNo.Name = "cantxt_KohuNo";
            this.cantxt_KohuNo.PopupAfterExecute = null;
            this.cantxt_KohuNo.PopupBeforeExecute = null;
            this.cantxt_KohuNo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_KohuNo.PopupSearchSendParams")));
            this.cantxt_KohuNo.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
            this.cantxt_KohuNo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_KohuNo.popupWindowSetting")));
            this.cantxt_KohuNo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_KohuNo.RegistCheckMethod")));
            this.cantxt_KohuNo.Size = new System.Drawing.Size(80, 20);
            this.cantxt_KohuNo.TabIndex = 6;
            this.cantxt_KohuNo.Tag = "半角11桁以内で入力してください";
            this.cantxt_KohuNo.Validating += new System.ComponentModel.CancelEventHandler(this.cantxt_KohuNo_Validating);
            // 
            // lbl_KohuNo
            // 
            this.lbl_KohuNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_KohuNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_KohuNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_KohuNo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_KohuNo.ForeColor = System.Drawing.Color.White;
            this.lbl_KohuNo.Location = new System.Drawing.Point(727, 0);
            this.lbl_KohuNo.Name = "lbl_KohuNo";
            this.lbl_KohuNo.Size = new System.Drawing.Size(66, 20);
            this.lbl_KohuNo.TabIndex = 34;
            this.lbl_KohuNo.Text = "交付番号";
            this.lbl_KohuNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(4, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 20);
            this.label3.TabIndex = 35;
            this.label3.Text = "廃棄物区分";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel3
            // 
            this.customPanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel3.Controls.Add(this.radbtn_SaishuShuryouDate);
            this.customPanel3.Controls.Add(this.radbtn_ShobunShuryouDate);
            this.customPanel3.Controls.Add(this.radbtn_UnpanShuryouDate);
            this.customPanel3.Controls.Add(this.txtNum_ChushutuHiduke);
            this.customPanel3.Controls.Add(this.radbtn_KoufuDate);
            this.customPanel3.Location = new System.Drawing.Point(138, 52);
            this.customPanel3.Name = "customPanel3";
            this.customPanel3.Size = new System.Drawing.Size(583, 20);
            this.customPanel3.TabIndex = 8;
            // 
            // radbtn_SaishuShuryouDate
            // 
            this.radbtn_SaishuShuryouDate.AutoSize = true;
            this.radbtn_SaishuShuryouDate.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_SaishuShuryouDate.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_SaishuShuryouDate.FocusOutCheckMethod")));
            this.radbtn_SaishuShuryouDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_SaishuShuryouDate.LinkedTextBox = "txtNum_ChushutuHiduke";
            this.radbtn_SaishuShuryouDate.Location = new System.Drawing.Point(442, 1);
            this.radbtn_SaishuShuryouDate.Name = "radbtn_SaishuShuryouDate";
            this.radbtn_SaishuShuryouDate.PopupAfterExecute = null;
            this.radbtn_SaishuShuryouDate.PopupBeforeExecute = null;
            this.radbtn_SaishuShuryouDate.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_SaishuShuryouDate.PopupSearchSendParams")));
            this.radbtn_SaishuShuryouDate.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_SaishuShuryouDate.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_SaishuShuryouDate.popupWindowSetting")));
            this.radbtn_SaishuShuryouDate.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_SaishuShuryouDate.RegistCheckMethod")));
            this.radbtn_SaishuShuryouDate.Size = new System.Drawing.Size(137, 17);
            this.radbtn_SaishuShuryouDate.TabIndex = 316;
            this.radbtn_SaishuShuryouDate.Tag = "抽出日付が「4.最終処分終了日」の場合にはチェックを付けてください";
            this.radbtn_SaishuShuryouDate.Text = "4.最終処分終了日";
            this.radbtn_SaishuShuryouDate.UseVisualStyleBackColor = true;
            this.radbtn_SaishuShuryouDate.Value = "4";
            // 
            // radbtn_ShobunShuryouDate
            // 
            this.radbtn_ShobunShuryouDate.AutoSize = true;
            this.radbtn_ShobunShuryouDate.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_ShobunShuryouDate.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_ShobunShuryouDate.FocusOutCheckMethod")));
            this.radbtn_ShobunShuryouDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_ShobunShuryouDate.LinkedTextBox = "txtNum_ChushutuHiduke";
            this.radbtn_ShobunShuryouDate.Location = new System.Drawing.Point(305, 1);
            this.radbtn_ShobunShuryouDate.Name = "radbtn_ShobunShuryouDate";
            this.radbtn_ShobunShuryouDate.PopupAfterExecute = null;
            this.radbtn_ShobunShuryouDate.PopupBeforeExecute = null;
            this.radbtn_ShobunShuryouDate.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_ShobunShuryouDate.PopupSearchSendParams")));
            this.radbtn_ShobunShuryouDate.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_ShobunShuryouDate.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_ShobunShuryouDate.popupWindowSetting")));
            this.radbtn_ShobunShuryouDate.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_ShobunShuryouDate.RegistCheckMethod")));
            this.radbtn_ShobunShuryouDate.Size = new System.Drawing.Size(109, 17);
            this.radbtn_ShobunShuryouDate.TabIndex = 315;
            this.radbtn_ShobunShuryouDate.Tag = "抽出日付が「3.処分終了日」の場合にはチェックを付けてください";
            this.radbtn_ShobunShuryouDate.Text = "3.処分終了日";
            this.radbtn_ShobunShuryouDate.UseVisualStyleBackColor = true;
            this.radbtn_ShobunShuryouDate.Value = "3";
            // 
            // radbtn_UnpanShuryouDate
            // 
            this.radbtn_UnpanShuryouDate.AutoSize = true;
            this.radbtn_UnpanShuryouDate.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_UnpanShuryouDate.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_UnpanShuryouDate.FocusOutCheckMethod")));
            this.radbtn_UnpanShuryouDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_UnpanShuryouDate.LinkedTextBox = "txtNum_ChushutuHiduke";
            this.radbtn_UnpanShuryouDate.Location = new System.Drawing.Point(168, 1);
            this.radbtn_UnpanShuryouDate.Name = "radbtn_UnpanShuryouDate";
            this.radbtn_UnpanShuryouDate.PopupAfterExecute = null;
            this.radbtn_UnpanShuryouDate.PopupBeforeExecute = null;
            this.radbtn_UnpanShuryouDate.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_UnpanShuryouDate.PopupSearchSendParams")));
            this.radbtn_UnpanShuryouDate.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_UnpanShuryouDate.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_UnpanShuryouDate.popupWindowSetting")));
            this.radbtn_UnpanShuryouDate.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_UnpanShuryouDate.RegistCheckMethod")));
            this.radbtn_UnpanShuryouDate.Size = new System.Drawing.Size(109, 17);
            this.radbtn_UnpanShuryouDate.TabIndex = 314;
            this.radbtn_UnpanShuryouDate.Tag = "抽出日付が「2.運搬終了日」の場合にはチェックを付けてください";
            this.radbtn_UnpanShuryouDate.Text = "2.運搬終了日";
            this.radbtn_UnpanShuryouDate.UseVisualStyleBackColor = true;
            this.radbtn_UnpanShuryouDate.Value = "2";
            // 
            // txtNum_ChushutuHiduke
            // 
            this.txtNum_ChushutuHiduke.BackColor = System.Drawing.SystemColors.Window;
            this.txtNum_ChushutuHiduke.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNum_ChushutuHiduke.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtNum_ChushutuHiduke.DisplayItemName = "抽出日付";
            this.txtNum_ChushutuHiduke.DisplayPopUp = null;
            this.txtNum_ChushutuHiduke.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_ChushutuHiduke.FocusOutCheckMethod")));
            this.txtNum_ChushutuHiduke.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtNum_ChushutuHiduke.ForeColor = System.Drawing.Color.Black;
            this.txtNum_ChushutuHiduke.IsInputErrorOccured = false;
            this.txtNum_ChushutuHiduke.LinkedRadioButtonArray = new string[] {
        "radbtn_KoufuDate",
        "radbtn_UnpanShuryouDate",
        "radbtn_ShobunShuryouDate",
        "radbtn_SaishuShuryouDate"};
            this.txtNum_ChushutuHiduke.Location = new System.Drawing.Point(-1, -1);
            this.txtNum_ChushutuHiduke.Name = "txtNum_ChushutuHiduke";
            this.txtNum_ChushutuHiduke.PopupAfterExecute = null;
            this.txtNum_ChushutuHiduke.PopupBeforeExecute = null;
            this.txtNum_ChushutuHiduke.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtNum_ChushutuHiduke.PopupSearchSendParams")));
            this.txtNum_ChushutuHiduke.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtNum_ChushutuHiduke.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtNum_ChushutuHiduke.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            4,
            0,
            0,
            0});
            rangeSettingDto3.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtNum_ChushutuHiduke.RangeSetting = rangeSettingDto3;
            this.txtNum_ChushutuHiduke.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_ChushutuHiduke.RegistCheckMethod")));
            this.txtNum_ChushutuHiduke.Size = new System.Drawing.Size(20, 20);
            this.txtNum_ChushutuHiduke.TabIndex = 4;
            this.txtNum_ChushutuHiduke.Tag = "【1～4】のいずれかで入力してください";
            this.txtNum_ChushutuHiduke.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNum_ChushutuHiduke.WordWrap = false;
            this.txtNum_ChushutuHiduke.Validated += new System.EventHandler(this.txtNum_ChushutuHiduke_Validated);
            // 
            // radbtn_KoufuDate
            // 
            this.radbtn_KoufuDate.AutoSize = true;
            this.radbtn_KoufuDate.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_KoufuDate.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_KoufuDate.FocusOutCheckMethod")));
            this.radbtn_KoufuDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_KoufuDate.LinkedTextBox = "txtNum_ChushutuHiduke";
            this.radbtn_KoufuDate.Location = new System.Drawing.Point(22, 1);
            this.radbtn_KoufuDate.Name = "radbtn_KoufuDate";
            this.radbtn_KoufuDate.PopupAfterExecute = null;
            this.radbtn_KoufuDate.PopupBeforeExecute = null;
            this.radbtn_KoufuDate.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_KoufuDate.PopupSearchSendParams")));
            this.radbtn_KoufuDate.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_KoufuDate.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_KoufuDate.popupWindowSetting")));
            this.radbtn_KoufuDate.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_KoufuDate.RegistCheckMethod")));
            this.radbtn_KoufuDate.Size = new System.Drawing.Size(109, 17);
            this.radbtn_KoufuDate.TabIndex = 313;
            this.radbtn_KoufuDate.Tag = "抽出日付が「1.交付年月日」の場合にはチェックを付けてください";
            this.radbtn_KoufuDate.Text = "1.交付年月日";
            this.radbtn_KoufuDate.UseVisualStyleBackColor = true;
            this.radbtn_KoufuDate.Value = "1";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(4, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 20);
            this.label4.TabIndex = 37;
            this.label4.Text = "抽出日付";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel4
            // 
            this.customPanel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel4.Controls.Add(this.radbtn_AllManifest);
            this.customPanel4.Controls.Add(this.radbtn_Denshi);
            this.customPanel4.Controls.Add(this.radbtn_Kenpai);
            this.customPanel4.Controls.Add(this.radbtn_SanpaiTumikae);
            this.customPanel4.Controls.Add(this.txtNum_ManifestShurui);
            this.customPanel4.Controls.Add(this.radbtn_SanpaiChokko);
            this.customPanel4.Location = new System.Drawing.Point(138, 26);
            this.customPanel4.Name = "customPanel4";
            this.customPanel4.Size = new System.Drawing.Size(583, 20);
            this.customPanel4.TabIndex = 7;
            // 
            // radbtn_AllManifest
            // 
            this.radbtn_AllManifest.AutoSize = true;
            this.radbtn_AllManifest.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_AllManifest.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_AllManifest.FocusOutCheckMethod")));
            this.radbtn_AllManifest.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_AllManifest.LinkedTextBox = "txtNum_ManifestShurui";
            this.radbtn_AllManifest.Location = new System.Drawing.Point(511, 1);
            this.radbtn_AllManifest.Name = "radbtn_AllManifest";
            this.radbtn_AllManifest.PopupAfterExecute = null;
            this.radbtn_AllManifest.PopupBeforeExecute = null;
            this.radbtn_AllManifest.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_AllManifest.PopupSearchSendParams")));
            this.radbtn_AllManifest.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_AllManifest.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_AllManifest.popupWindowSetting")));
            this.radbtn_AllManifest.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_AllManifest.RegistCheckMethod")));
            this.radbtn_AllManifest.Size = new System.Drawing.Size(67, 17);
            this.radbtn_AllManifest.TabIndex = 312;
            this.radbtn_AllManifest.Tag = "マニフェスト種類が「5.全て」の場合にはチェックを付けてください";
            this.radbtn_AllManifest.Text = "5.全て";
            this.radbtn_AllManifest.UseVisualStyleBackColor = true;
            this.radbtn_AllManifest.Value = "5";
            // 
            // radbtn_Denshi
            // 
            this.radbtn_Denshi.AutoSize = true;
            this.radbtn_Denshi.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Denshi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Denshi.FocusOutCheckMethod")));
            this.radbtn_Denshi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_Denshi.LinkedTextBox = "txtNum_ManifestShurui";
            this.radbtn_Denshi.Location = new System.Drawing.Point(410, 1);
            this.radbtn_Denshi.Name = "radbtn_Denshi";
            this.radbtn_Denshi.PopupAfterExecute = null;
            this.radbtn_Denshi.PopupBeforeExecute = null;
            this.radbtn_Denshi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Denshi.PopupSearchSendParams")));
            this.radbtn_Denshi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Denshi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Denshi.popupWindowSetting")));
            this.radbtn_Denshi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Denshi.RegistCheckMethod")));
            this.radbtn_Denshi.Size = new System.Drawing.Size(67, 17);
            this.radbtn_Denshi.TabIndex = 311;
            this.radbtn_Denshi.Tag = "抽出対象区分が「4.処分終了日」の場合にはチェックを付けてください";
            this.radbtn_Denshi.Text = "4.電子";
            this.radbtn_Denshi.UseVisualStyleBackColor = true;
            this.radbtn_Denshi.Value = "4";
            // 
            // radbtn_Kenpai
            // 
            this.radbtn_Kenpai.AutoSize = true;
            this.radbtn_Kenpai.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Kenpai.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Kenpai.FocusOutCheckMethod")));
            this.radbtn_Kenpai.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_Kenpai.LinkedTextBox = "txtNum_ManifestShurui";
            this.radbtn_Kenpai.Location = new System.Drawing.Point(268, 1);
            this.radbtn_Kenpai.Name = "radbtn_Kenpai";
            this.radbtn_Kenpai.PopupAfterExecute = null;
            this.radbtn_Kenpai.PopupBeforeExecute = null;
            this.radbtn_Kenpai.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Kenpai.PopupSearchSendParams")));
            this.radbtn_Kenpai.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Kenpai.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Kenpai.popupWindowSetting")));
            this.radbtn_Kenpai.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Kenpai.RegistCheckMethod")));
            this.radbtn_Kenpai.Size = new System.Drawing.Size(109, 17);
            this.radbtn_Kenpai.TabIndex = 310;
            this.radbtn_Kenpai.Tag = "抽出対象区分が「3.産廃(積替)」の場合にはチェックを付けてください";
            this.radbtn_Kenpai.Text = "3.産廃(積替)";
            this.radbtn_Kenpai.UseVisualStyleBackColor = true;
            this.radbtn_Kenpai.Value = "3";
            // 
            // radbtn_SanpaiTumikae
            // 
            this.radbtn_SanpaiTumikae.AutoSize = true;
            this.radbtn_SanpaiTumikae.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_SanpaiTumikae.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_SanpaiTumikae.FocusOutCheckMethod")));
            this.radbtn_SanpaiTumikae.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_SanpaiTumikae.LinkedTextBox = "txtNum_ManifestShurui";
            this.radbtn_SanpaiTumikae.Location = new System.Drawing.Point(168, 1);
            this.radbtn_SanpaiTumikae.Name = "radbtn_SanpaiTumikae";
            this.radbtn_SanpaiTumikae.PopupAfterExecute = null;
            this.radbtn_SanpaiTumikae.PopupBeforeExecute = null;
            this.radbtn_SanpaiTumikae.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_SanpaiTumikae.PopupSearchSendParams")));
            this.radbtn_SanpaiTumikae.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_SanpaiTumikae.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_SanpaiTumikae.popupWindowSetting")));
            this.radbtn_SanpaiTumikae.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_SanpaiTumikae.RegistCheckMethod")));
            this.radbtn_SanpaiTumikae.Size = new System.Drawing.Size(67, 17);
            this.radbtn_SanpaiTumikae.TabIndex = 309;
            this.radbtn_SanpaiTumikae.Tag = "抽出対象区分が「2.建廃」の場合にはチェックを付けてください";
            this.radbtn_SanpaiTumikae.Text = "2.建廃";
            this.radbtn_SanpaiTumikae.UseVisualStyleBackColor = true;
            this.radbtn_SanpaiTumikae.Value = "2";
            // 
            // txtNum_ManifestShurui
            // 
            this.txtNum_ManifestShurui.BackColor = System.Drawing.SystemColors.Window;
            this.txtNum_ManifestShurui.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNum_ManifestShurui.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtNum_ManifestShurui.DisplayItemName = "マニフェスト種類";
            this.txtNum_ManifestShurui.DisplayPopUp = null;
            this.txtNum_ManifestShurui.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_ManifestShurui.FocusOutCheckMethod")));
            this.txtNum_ManifestShurui.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtNum_ManifestShurui.ForeColor = System.Drawing.Color.Black;
            this.txtNum_ManifestShurui.IsInputErrorOccured = false;
            this.txtNum_ManifestShurui.LinkedRadioButtonArray = new string[] {
        "radbtn_SanpaiChokko",
        "radbtn_SanpaiTumikae",
        "radbtn_Kenpai",
        "radbtn_Denshi",
        "radbtn_AllManifest"};
            this.txtNum_ManifestShurui.Location = new System.Drawing.Point(-1, -1);
            this.txtNum_ManifestShurui.Name = "txtNum_ManifestShurui";
            this.txtNum_ManifestShurui.PopupAfterExecute = null;
            this.txtNum_ManifestShurui.PopupBeforeExecute = null;
            this.txtNum_ManifestShurui.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtNum_ManifestShurui.PopupSearchSendParams")));
            this.txtNum_ManifestShurui.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtNum_ManifestShurui.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtNum_ManifestShurui.popupWindowSetting")));
            rangeSettingDto4.Max = new decimal(new int[] {
            5,
            0,
            0,
            0});
            rangeSettingDto4.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtNum_ManifestShurui.RangeSetting = rangeSettingDto4;
            this.txtNum_ManifestShurui.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_ManifestShurui.RegistCheckMethod")));
            this.txtNum_ManifestShurui.Size = new System.Drawing.Size(20, 20);
            this.txtNum_ManifestShurui.TabIndex = 3;
            this.txtNum_ManifestShurui.Tag = "【1～5】のいずれかで入力してください";
            this.txtNum_ManifestShurui.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNum_ManifestShurui.WordWrap = false;
            this.txtNum_ManifestShurui.Validated += new System.EventHandler(this.txtNum_ManifestShurui_Validated);
            // 
            // radbtn_SanpaiChokko
            // 
            this.radbtn_SanpaiChokko.AutoSize = true;
            this.radbtn_SanpaiChokko.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_SanpaiChokko.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_SanpaiChokko.FocusOutCheckMethod")));
            this.radbtn_SanpaiChokko.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_SanpaiChokko.LinkedTextBox = "txtNum_ManifestShurui";
            this.radbtn_SanpaiChokko.Location = new System.Drawing.Point(22, 1);
            this.radbtn_SanpaiChokko.Name = "radbtn_SanpaiChokko";
            this.radbtn_SanpaiChokko.PopupAfterExecute = null;
            this.radbtn_SanpaiChokko.PopupBeforeExecute = null;
            this.radbtn_SanpaiChokko.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_SanpaiChokko.PopupSearchSendParams")));
            this.radbtn_SanpaiChokko.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_SanpaiChokko.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_SanpaiChokko.popupWindowSetting")));
            this.radbtn_SanpaiChokko.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_SanpaiChokko.RegistCheckMethod")));
            this.radbtn_SanpaiChokko.Size = new System.Drawing.Size(109, 17);
            this.radbtn_SanpaiChokko.TabIndex = 308;
            this.radbtn_SanpaiChokko.Tag = "抽出対象区分が「1.産廃(直行)」の場合にはチェックを付けてください";
            this.radbtn_SanpaiChokko.Text = "1.産廃(直行)";
            this.radbtn_SanpaiChokko.UseVisualStyleBackColor = true;
            this.radbtn_SanpaiChokko.Value = "1";
            // 
            // HIDUKE_FROM
            // 
            this.HIDUKE_FROM.BackColor = System.Drawing.SystemColors.Window;
            this.HIDUKE_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HIDUKE_FROM.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.HIDUKE_FROM.Checked = false;
            this.HIDUKE_FROM.CustomFormat = "yyyy/MM/dd(ddd)";
            this.HIDUKE_FROM.DateTimeNowYear = global::Shougun.Core.PaperManifest.Himodukeichiran.Properties.Resources.String1;
            this.HIDUKE_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.HIDUKE_FROM.DisplayItemName = "開始日付";
            this.HIDUKE_FROM.DisplayPopUp = null;
            this.HIDUKE_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_FROM.FocusOutCheckMethod")));
            this.HIDUKE_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HIDUKE_FROM.ForeColor = System.Drawing.Color.Black;
            this.HIDUKE_FROM.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.HIDUKE_FROM.IsInputErrorOccured = false;
            this.HIDUKE_FROM.Location = new System.Drawing.Point(138, 78);
            this.HIDUKE_FROM.MaxLength = 10;
            this.HIDUKE_FROM.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.HIDUKE_FROM.Name = "HIDUKE_FROM";
            this.HIDUKE_FROM.NullValue = global::Shougun.Core.PaperManifest.Himodukeichiran.Properties.Resources.String1;
            this.HIDUKE_FROM.PopupAfterExecute = null;
            this.HIDUKE_FROM.PopupBeforeExecute = null;
            this.HIDUKE_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIDUKE_FROM.PopupSearchSendParams")));
            this.HIDUKE_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIDUKE_FROM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HIDUKE_FROM.popupWindowSetting")));
            this.HIDUKE_FROM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_FROM.RegistCheckMethod")));
            this.HIDUKE_FROM.Size = new System.Drawing.Size(138, 20);
            this.HIDUKE_FROM.TabIndex = 9;
            this.HIDUKE_FROM.Tag = "日付を選択してください";
            this.HIDUKE_FROM.Text = "2013/10/31(木)";
            this.HIDUKE_FROM.Value = new System.DateTime(2013, 10, 31, 0, 0, 0, 0);
            this.HIDUKE_FROM.Leave += new System.EventHandler(this.HIDUKE_FROM_Leave);
            // 
            // HIDUKE_TO
            // 
            this.HIDUKE_TO.BackColor = System.Drawing.SystemColors.Window;
            this.HIDUKE_TO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HIDUKE_TO.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.HIDUKE_TO.Checked = false;
            this.HIDUKE_TO.CustomFormat = "yyyy/MM/dd(ddd)";
            this.HIDUKE_TO.DateTimeNowYear = global::Shougun.Core.PaperManifest.Himodukeichiran.Properties.Resources.String1;
            this.HIDUKE_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.HIDUKE_TO.DisplayItemName = "終了日付";
            this.HIDUKE_TO.DisplayPopUp = null;
            this.HIDUKE_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_TO.FocusOutCheckMethod")));
            this.HIDUKE_TO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HIDUKE_TO.ForeColor = System.Drawing.Color.Black;
            this.HIDUKE_TO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.HIDUKE_TO.IsInputErrorOccured = false;
            this.HIDUKE_TO.Location = new System.Drawing.Point(296, 78);
            this.HIDUKE_TO.MaxLength = 10;
            this.HIDUKE_TO.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.HIDUKE_TO.Name = "HIDUKE_TO";
            this.HIDUKE_TO.NullValue = global::Shougun.Core.PaperManifest.Himodukeichiran.Properties.Resources.String1;
            this.HIDUKE_TO.PopupAfterExecute = null;
            this.HIDUKE_TO.PopupBeforeExecute = null;
            this.HIDUKE_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIDUKE_TO.PopupSearchSendParams")));
            this.HIDUKE_TO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIDUKE_TO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HIDUKE_TO.popupWindowSetting")));
            this.HIDUKE_TO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_TO.RegistCheckMethod")));
            this.HIDUKE_TO.Size = new System.Drawing.Size(138, 20);
            this.HIDUKE_TO.TabIndex = 10;
            this.HIDUKE_TO.Tag = "日付を選択してください";
            this.HIDUKE_TO.Text = "2013/10/31(木)";
            this.HIDUKE_TO.Value = new System.DateTime(2013, 10, 31, 0, 0, 0, 0);
            this.HIDUKE_TO.Leave += new System.EventHandler(this.HIDUKE_TO_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(276, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 398;
            this.label5.Text = "～";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(4, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(128, 20);
            this.label6.TabIndex = 399;
            this.label6.Text = "抽出業者";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel5
            // 
            this.customPanel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel5.Controls.Add(this.radbtn_SaishuShobunBasho);
            this.customPanel5.Controls.Add(this.radbtn_ShobunJutakusha);
            this.customPanel5.Controls.Add(this.radbtn_UnpanJutakusha);
            this.customPanel5.Controls.Add(this.txtNum_ChushutuGyosha);
            this.customPanel5.Controls.Add(this.radbtn_HaishuJigyosha);
            this.customPanel5.Location = new System.Drawing.Point(138, 104);
            this.customPanel5.Name = "customPanel5";
            this.customPanel5.Size = new System.Drawing.Size(583, 20);
            this.customPanel5.TabIndex = 11;
            // 
            // radbtn_SaishuShobunBasho
            // 
            this.radbtn_SaishuShobunBasho.AutoSize = true;
            this.radbtn_SaishuShobunBasho.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_SaishuShobunBasho.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_SaishuShobunBasho.FocusOutCheckMethod")));
            this.radbtn_SaishuShobunBasho.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_SaishuShobunBasho.LinkedTextBox = "txtNum_ChushutuGyosha";
            this.radbtn_SaishuShobunBasho.Location = new System.Drawing.Point(442, 1);
            this.radbtn_SaishuShobunBasho.Name = "radbtn_SaishuShobunBasho";
            this.radbtn_SaishuShobunBasho.PopupAfterExecute = null;
            this.radbtn_SaishuShobunBasho.PopupBeforeExecute = null;
            this.radbtn_SaishuShobunBasho.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_SaishuShobunBasho.PopupSearchSendParams")));
            this.radbtn_SaishuShobunBasho.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_SaishuShobunBasho.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_SaishuShobunBasho.popupWindowSetting")));
            this.radbtn_SaishuShobunBasho.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_SaishuShobunBasho.RegistCheckMethod")));
            this.radbtn_SaishuShobunBasho.Size = new System.Drawing.Size(123, 17);
            this.radbtn_SaishuShobunBasho.TabIndex = 320;
            this.radbtn_SaishuShobunBasho.Tag = "抽出日付が「4.最終処分場所」の場合にはチェックを付けてください";
            this.radbtn_SaishuShobunBasho.Text = "4.最終処分場所";
            this.radbtn_SaishuShobunBasho.UseVisualStyleBackColor = true;
            this.radbtn_SaishuShobunBasho.Value = "4";
            // 
            // radbtn_ShobunJutakusha
            // 
            this.radbtn_ShobunJutakusha.AutoSize = true;
            this.radbtn_ShobunJutakusha.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_ShobunJutakusha.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_ShobunJutakusha.FocusOutCheckMethod")));
            this.radbtn_ShobunJutakusha.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_ShobunJutakusha.LinkedTextBox = "txtNum_ChushutuGyosha";
            this.radbtn_ShobunJutakusha.Location = new System.Drawing.Point(305, 1);
            this.radbtn_ShobunJutakusha.Name = "radbtn_ShobunJutakusha";
            this.radbtn_ShobunJutakusha.PopupAfterExecute = null;
            this.radbtn_ShobunJutakusha.PopupBeforeExecute = null;
            this.radbtn_ShobunJutakusha.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_ShobunJutakusha.PopupSearchSendParams")));
            this.radbtn_ShobunJutakusha.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_ShobunJutakusha.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_ShobunJutakusha.popupWindowSetting")));
            this.radbtn_ShobunJutakusha.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_ShobunJutakusha.RegistCheckMethod")));
            this.radbtn_ShobunJutakusha.Size = new System.Drawing.Size(109, 17);
            this.radbtn_ShobunJutakusha.TabIndex = 319;
            this.radbtn_ShobunJutakusha.Tag = "抽出日付が「3.処分受託者」の場合にはチェックを付けてください";
            this.radbtn_ShobunJutakusha.Text = "3.処分受託者";
            this.radbtn_ShobunJutakusha.UseVisualStyleBackColor = true;
            this.radbtn_ShobunJutakusha.Value = "3";
            // 
            // radbtn_UnpanJutakusha
            // 
            this.radbtn_UnpanJutakusha.AutoSize = true;
            this.radbtn_UnpanJutakusha.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_UnpanJutakusha.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_UnpanJutakusha.FocusOutCheckMethod")));
            this.radbtn_UnpanJutakusha.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_UnpanJutakusha.LinkedTextBox = "txtNum_ChushutuGyosha";
            this.radbtn_UnpanJutakusha.Location = new System.Drawing.Point(168, 1);
            this.radbtn_UnpanJutakusha.Name = "radbtn_UnpanJutakusha";
            this.radbtn_UnpanJutakusha.PopupAfterExecute = null;
            this.radbtn_UnpanJutakusha.PopupBeforeExecute = null;
            this.radbtn_UnpanJutakusha.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_UnpanJutakusha.PopupSearchSendParams")));
            this.radbtn_UnpanJutakusha.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_UnpanJutakusha.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_UnpanJutakusha.popupWindowSetting")));
            this.radbtn_UnpanJutakusha.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_UnpanJutakusha.RegistCheckMethod")));
            this.radbtn_UnpanJutakusha.Size = new System.Drawing.Size(109, 17);
            this.radbtn_UnpanJutakusha.TabIndex = 318;
            this.radbtn_UnpanJutakusha.Tag = "抽出日付が「2.運搬受託者」の場合にはチェックを付けてください";
            this.radbtn_UnpanJutakusha.Text = "2.運搬受託者";
            this.radbtn_UnpanJutakusha.UseVisualStyleBackColor = true;
            this.radbtn_UnpanJutakusha.Value = "2";
            // 
            // txtNum_ChushutuGyosha
            // 
            this.txtNum_ChushutuGyosha.BackColor = System.Drawing.SystemColors.Window;
            this.txtNum_ChushutuGyosha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNum_ChushutuGyosha.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtNum_ChushutuGyosha.DisplayItemName = "抽出業者";
            this.txtNum_ChushutuGyosha.DisplayPopUp = null;
            this.txtNum_ChushutuGyosha.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_ChushutuGyosha.FocusOutCheckMethod")));
            this.txtNum_ChushutuGyosha.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtNum_ChushutuGyosha.ForeColor = System.Drawing.Color.Black;
            this.txtNum_ChushutuGyosha.IsInputErrorOccured = false;
            this.txtNum_ChushutuGyosha.LinkedRadioButtonArray = new string[] {
        "radbtn_HaishuJigyosha",
        "radbtn_UnpanJutakusha",
        "radbtn_ShobunJutakusha",
        "radbtn_SaishuShobunBasho"};
            this.txtNum_ChushutuGyosha.Location = new System.Drawing.Point(-1, -1);
            this.txtNum_ChushutuGyosha.Name = "txtNum_ChushutuGyosha";
            this.txtNum_ChushutuGyosha.PopupAfterExecute = null;
            this.txtNum_ChushutuGyosha.PopupBeforeExecute = null;
            this.txtNum_ChushutuGyosha.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtNum_ChushutuGyosha.PopupSearchSendParams")));
            this.txtNum_ChushutuGyosha.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtNum_ChushutuGyosha.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtNum_ChushutuGyosha.popupWindowSetting")));
            rangeSettingDto5.Max = new decimal(new int[] {
            4,
            0,
            0,
            0});
            rangeSettingDto5.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtNum_ChushutuGyosha.RangeSetting = rangeSettingDto5;
            this.txtNum_ChushutuGyosha.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_ChushutuGyosha.RegistCheckMethod")));
            this.txtNum_ChushutuGyosha.Size = new System.Drawing.Size(20, 20);
            this.txtNum_ChushutuGyosha.TabIndex = 7;
            this.txtNum_ChushutuGyosha.Tag = "【1～4】のいずれかで入力してください";
            this.txtNum_ChushutuGyosha.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNum_ChushutuGyosha.WordWrap = false;
            this.txtNum_ChushutuGyosha.Validated += new System.EventHandler(this.txtNum_ChushutuGyosha_Validated);
            // 
            // radbtn_HaishuJigyosha
            // 
            this.radbtn_HaishuJigyosha.AutoSize = true;
            this.radbtn_HaishuJigyosha.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_HaishuJigyosha.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_HaishuJigyosha.FocusOutCheckMethod")));
            this.radbtn_HaishuJigyosha.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_HaishuJigyosha.LinkedTextBox = "txtNum_ChushutuGyosha";
            this.radbtn_HaishuJigyosha.Location = new System.Drawing.Point(22, 1);
            this.radbtn_HaishuJigyosha.Name = "radbtn_HaishuJigyosha";
            this.radbtn_HaishuJigyosha.PopupAfterExecute = null;
            this.radbtn_HaishuJigyosha.PopupBeforeExecute = null;
            this.radbtn_HaishuJigyosha.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_HaishuJigyosha.PopupSearchSendParams")));
            this.radbtn_HaishuJigyosha.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_HaishuJigyosha.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_HaishuJigyosha.popupWindowSetting")));
            this.radbtn_HaishuJigyosha.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_HaishuJigyosha.RegistCheckMethod")));
            this.radbtn_HaishuJigyosha.Size = new System.Drawing.Size(109, 17);
            this.radbtn_HaishuJigyosha.TabIndex = 317;
            this.radbtn_HaishuJigyosha.Tag = "抽出日付が「1.排出事業者」の場合にはチェックを付けてください";
            this.radbtn_HaishuJigyosha.Text = "1.排出事業者";
            this.radbtn_HaishuJigyosha.UseVisualStyleBackColor = true;
            this.radbtn_HaishuJigyosha.Value = "1";
            // 
            // cbtn_GenbaSan
            // 
            this.cbtn_GenbaSan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.cbtn_GenbaSan.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cbtn_GenbaSan.DBFieldsName = null;
            this.cbtn_GenbaSan.DefaultBackColor = System.Drawing.Color.Empty;
            this.cbtn_GenbaSan.DisplayItemName = null;
            this.cbtn_GenbaSan.DisplayPopUp = null;
            this.cbtn_GenbaSan.ErrorMessage = null;
            this.cbtn_GenbaSan.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_GenbaSan.FocusOutCheckMethod")));
            this.cbtn_GenbaSan.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.cbtn_GenbaSan.GetCodeMasterField = null;
            this.cbtn_GenbaSan.Image = ((System.Drawing.Image)(resources.GetObject("cbtn_GenbaSan.Image")));
            this.cbtn_GenbaSan.ItemDefinedTypes = null;
            this.cbtn_GenbaSan.LinkedSettingTextBox = null;
            this.cbtn_GenbaSan.LinkedTextBoxs = null;
            this.cbtn_GenbaSan.Location = new System.Drawing.Point(788, 129);
            this.cbtn_GenbaSan.Name = "cbtn_GenbaSan";
            this.cbtn_GenbaSan.PopupAfterExecute = null;
            this.cbtn_GenbaSan.PopupAfterExecuteMethod = "cantxt_GenbaName_PopupAfterExecuteMethod";
            this.cbtn_GenbaSan.PopupBeforeExecute = null;
            this.cbtn_GenbaSan.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.cbtn_GenbaSan.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cbtn_GenbaSan.PopupSearchSendParams")));
            this.cbtn_GenbaSan.PopupSetFormField = "cantxt_GenbaCd,ctxt_GenbaName,cantxt_GyousyaCd,ctxt_GyousyaName";
            this.cbtn_GenbaSan.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.cbtn_GenbaSan.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.cbtn_GenbaSan.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cbtn_GenbaSan.popupWindowSetting")));
            this.cbtn_GenbaSan.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_GenbaSan.RegistCheckMethod")));
            this.cbtn_GenbaSan.SearchDisplayFlag = 0;
            this.cbtn_GenbaSan.SetFormField = null;
            this.cbtn_GenbaSan.ShortItemName = null;
            this.cbtn_GenbaSan.Size = new System.Drawing.Size(22, 22);
            this.cbtn_GenbaSan.TabIndex = 406;
            this.cbtn_GenbaSan.TabStop = false;
            this.cbtn_GenbaSan.Tag = "現場の検索を行います";
            this.cbtn_GenbaSan.Text = " ";
            this.cbtn_GenbaSan.UseVisualStyleBackColor = false;
            this.cbtn_GenbaSan.ZeroPaddengFlag = false;
            // 
            // ctxt_GenbaName
            // 
            this.ctxt_GenbaName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_GenbaName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_GenbaName.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.ctxt_GenbaName.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_GenbaName.DisplayPopUp = null;
            this.ctxt_GenbaName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_GenbaName.FocusOutCheckMethod")));
            this.ctxt_GenbaName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.ctxt_GenbaName.ForeColor = System.Drawing.Color.Black;
            this.ctxt_GenbaName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.ctxt_GenbaName.IsInputErrorOccured = false;
            this.ctxt_GenbaName.Location = new System.Drawing.Point(537, 130);
            this.ctxt_GenbaName.MaxLength = 40;
            this.ctxt_GenbaName.Name = "ctxt_GenbaName";
            this.ctxt_GenbaName.PopupAfterExecute = null;
            this.ctxt_GenbaName.PopupBeforeExecute = null;
            this.ctxt_GenbaName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_GenbaName.PopupSearchSendParams")));
            this.ctxt_GenbaName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_GenbaName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_GenbaName.popupWindowSetting")));
            this.ctxt_GenbaName.ReadOnly = true;
            this.ctxt_GenbaName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_GenbaName.RegistCheckMethod")));
            this.ctxt_GenbaName.Size = new System.Drawing.Size(245, 20);
            this.ctxt_GenbaName.TabIndex = 322;
            this.ctxt_GenbaName.TabStop = false;
            this.ctxt_GenbaName.Tag = "全角40文字以内で入力してください";
            // 
            // cantxt_GenbaCd
            // 
            this.cantxt_GenbaCd.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_GenbaCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_GenbaCd.ChangeUpperCase = true;
            this.cantxt_GenbaCd.CharacterLimitList = null;
            this.cantxt_GenbaCd.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.cantxt_GenbaCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_GenbaCd.DisplayItemName = "現場";
            this.cantxt_GenbaCd.DisplayPopUp = null;
            this.cantxt_GenbaCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_GenbaCd.FocusOutCheckMethod")));
            this.cantxt_GenbaCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.cantxt_GenbaCd.ForeColor = System.Drawing.Color.Black;
            this.cantxt_GenbaCd.GetCodeMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.cantxt_GenbaCd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cantxt_GenbaCd.IsInputErrorOccured = false;
            this.cantxt_GenbaCd.Location = new System.Drawing.Point(478, 130);
            this.cantxt_GenbaCd.MaxLength = 6;
            this.cantxt_GenbaCd.Name = "cantxt_GenbaCd";
            this.cantxt_GenbaCd.PopupAfterExecute = null;
            this.cantxt_GenbaCd.PopupAfterExecuteMethod = "cantxt_GenbaName_PopupAfterExecuteMethod";
            this.cantxt_GenbaCd.PopupBeforeExecute = null;
            this.cantxt_GenbaCd.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.cantxt_GenbaCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_GenbaCd.PopupSearchSendParams")));
            this.cantxt_GenbaCd.PopupSendParams = new string[0];
            this.cantxt_GenbaCd.PopupSetFormField = "cantxt_GenbaCd,ctxt_GenbaName,cantxt_GyousyaCd,ctxt_GyousyaName";
            this.cantxt_GenbaCd.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.cantxt_GenbaCd.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.cantxt_GenbaCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_GenbaCd.popupWindowSetting")));
            this.cantxt_GenbaCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_GenbaCd.RegistCheckMethod")));
            this.cantxt_GenbaCd.SetFormField = "cantxt_GenbaCd,ctxt_GenbaName,cantxt_GyousyaCd,ctxt_GyousyaName";
            this.cantxt_GenbaCd.Size = new System.Drawing.Size(60, 20);
            this.cantxt_GenbaCd.TabIndex = 13;
            this.cantxt_GenbaCd.Tag = "半角6桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.cantxt_GenbaCd.ZeroPaddengFlag = true;
            this.cantxt_GenbaCd.Validated += new System.EventHandler(this.cantxt_GenbaCd_Validated);
            // 
            // cbtn_GyousyaSan
            // 
            this.cbtn_GyousyaSan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.cbtn_GyousyaSan.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cbtn_GyousyaSan.DBFieldsName = null;
            this.cbtn_GyousyaSan.DefaultBackColor = System.Drawing.Color.Empty;
            this.cbtn_GyousyaSan.DisplayItemName = null;
            this.cbtn_GyousyaSan.DisplayPopUp = null;
            this.cbtn_GyousyaSan.ErrorMessage = null;
            this.cbtn_GyousyaSan.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_GyousyaSan.FocusOutCheckMethod")));
            this.cbtn_GyousyaSan.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.cbtn_GyousyaSan.GetCodeMasterField = null;
            this.cbtn_GyousyaSan.Image = ((System.Drawing.Image)(resources.GetObject("cbtn_GyousyaSan.Image")));
            this.cbtn_GyousyaSan.ItemDefinedTypes = null;
            this.cbtn_GyousyaSan.LinkedSettingTextBox = null;
            this.cbtn_GyousyaSan.LinkedTextBoxs = null;
            this.cbtn_GyousyaSan.Location = new System.Drawing.Point(450, 129);
            this.cbtn_GyousyaSan.Name = "cbtn_GyousyaSan";
            this.cbtn_GyousyaSan.PopupAfterExecute = null;
            this.cbtn_GyousyaSan.PopupAfterExecuteMethod = "cantxt_GyousyaCd_PopupAfterExecuteMethod";
            this.cbtn_GyousyaSan.PopupBeforeExecute = null;
            this.cbtn_GyousyaSan.PopupBeforeExecuteMethod = "cantxt_GyousyaCd_PopupBeforeExecuteMethod";
            this.cbtn_GyousyaSan.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.cbtn_GyousyaSan.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cbtn_GyousyaSan.PopupSearchSendParams")));
            this.cbtn_GyousyaSan.PopupSetFormField = "cantxt_GyousyaCd,ctxt_GyousyaName";
            this.cbtn_GyousyaSan.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.cbtn_GyousyaSan.PopupWindowName = "検索共通ポップアップ";
            this.cbtn_GyousyaSan.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cbtn_GyousyaSan.popupWindowSetting")));
            this.cbtn_GyousyaSan.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_GyousyaSan.RegistCheckMethod")));
            this.cbtn_GyousyaSan.SearchDisplayFlag = 0;
            this.cbtn_GyousyaSan.SetFormField = "cantxt_GyousyaCd,ctxt_GyousyaName";
            this.cbtn_GyousyaSan.ShortItemName = null;
            this.cbtn_GyousyaSan.Size = new System.Drawing.Size(22, 22);
            this.cbtn_GyousyaSan.TabIndex = 403;
            this.cbtn_GyousyaSan.TabStop = false;
            this.cbtn_GyousyaSan.Tag = "業者の検索を行います";
            this.cbtn_GyousyaSan.Text = " ";
            this.cbtn_GyousyaSan.UseVisualStyleBackColor = false;
            this.cbtn_GyousyaSan.ZeroPaddengFlag = false;
            // 
            // ctxt_GyousyaName
            // 
            this.ctxt_GyousyaName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_GyousyaName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_GyousyaName.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.ctxt_GyousyaName.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_GyousyaName.DisplayPopUp = null;
            this.ctxt_GyousyaName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_GyousyaName.FocusOutCheckMethod")));
            this.ctxt_GyousyaName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.ctxt_GyousyaName.ForeColor = System.Drawing.Color.Black;
            this.ctxt_GyousyaName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.ctxt_GyousyaName.IsInputErrorOccured = false;
            this.ctxt_GyousyaName.Location = new System.Drawing.Point(197, 130);
            this.ctxt_GyousyaName.MaxLength = 40;
            this.ctxt_GyousyaName.Name = "ctxt_GyousyaName";
            this.ctxt_GyousyaName.PopupAfterExecute = null;
            this.ctxt_GyousyaName.PopupBeforeExecute = null;
            this.ctxt_GyousyaName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_GyousyaName.PopupSearchSendParams")));
            this.ctxt_GyousyaName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_GyousyaName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_GyousyaName.popupWindowSetting")));
            this.ctxt_GyousyaName.ReadOnly = true;
            this.ctxt_GyousyaName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_GyousyaName.RegistCheckMethod")));
            this.ctxt_GyousyaName.Size = new System.Drawing.Size(247, 20);
            this.ctxt_GyousyaName.TabIndex = 321;
            this.ctxt_GyousyaName.TabStop = false;
            this.ctxt_GyousyaName.Tag = "全角40文字以内で入力してください";
            // 
            // cantxt_GyousyaCd
            // 
            this.cantxt_GyousyaCd.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_GyousyaCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_GyousyaCd.ChangeUpperCase = true;
            this.cantxt_GyousyaCd.CharacterLimitList = null;
            this.cantxt_GyousyaCd.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.cantxt_GyousyaCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_GyousyaCd.DisplayItemName = "業者";
            this.cantxt_GyousyaCd.DisplayPopUp = null;
            this.cantxt_GyousyaCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_GyousyaCd.FocusOutCheckMethod")));
            this.cantxt_GyousyaCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.cantxt_GyousyaCd.ForeColor = System.Drawing.Color.Black;
            this.cantxt_GyousyaCd.GetCodeMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GYOUSHA_POST,GYOUSHA_TEL,GYOUSHA_ADDRESS1";
            this.cantxt_GyousyaCd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cantxt_GyousyaCd.IsInputErrorOccured = false;
            this.cantxt_GyousyaCd.Location = new System.Drawing.Point(138, 130);
            this.cantxt_GyousyaCd.MaxLength = 6;
            this.cantxt_GyousyaCd.Name = "cantxt_GyousyaCd";
            this.cantxt_GyousyaCd.PopupAfterExecute = null;
            this.cantxt_GyousyaCd.PopupAfterExecuteMethod = "cantxt_GyousyaCd_PopupAfterExecuteMethod";
            this.cantxt_GyousyaCd.PopupBeforeExecute = null;
            this.cantxt_GyousyaCd.PopupBeforeExecuteMethod = "cantxt_GyousyaCd_PopupBeforeExecuteMethod";
            this.cantxt_GyousyaCd.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.cantxt_GyousyaCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_GyousyaCd.PopupSearchSendParams")));
            this.cantxt_GyousyaCd.PopupSendParams = new string[0];
            this.cantxt_GyousyaCd.PopupSetFormField = "cantxt_GyousyaCd,ctxt_GyousyaName";
            this.cantxt_GyousyaCd.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.cantxt_GyousyaCd.PopupWindowName = "検索共通ポップアップ";
            this.cantxt_GyousyaCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_GyousyaCd.popupWindowSetting")));
            this.cantxt_GyousyaCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_GyousyaCd.RegistCheckMethod")));
            this.cantxt_GyousyaCd.SetFormField = "cantxt_GyousyaCd,ctxt_GyousyaName";
            this.cantxt_GyousyaCd.Size = new System.Drawing.Size(60, 20);
            this.cantxt_GyousyaCd.TabIndex = 12;
            this.cantxt_GyousyaCd.Tag = "半角6桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.cantxt_GyousyaCd.ZeroPaddengFlag = true;
            this.cantxt_GyousyaCd.Validated += new System.EventHandler(this.cantxt_GyousyaCd_Validated);
            // 
            // cbtn_JigyoubaSan
            // 
            this.cbtn_JigyoubaSan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.cbtn_JigyoubaSan.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cbtn_JigyoubaSan.DBFieldsName = null;
            this.cbtn_JigyoubaSan.DefaultBackColor = System.Drawing.Color.Empty;
            this.cbtn_JigyoubaSan.DisplayItemName = null;
            this.cbtn_JigyoubaSan.DisplayPopUp = null;
            this.cbtn_JigyoubaSan.ErrorMessage = null;
            this.cbtn_JigyoubaSan.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_JigyoubaSan.FocusOutCheckMethod")));
            this.cbtn_JigyoubaSan.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.cbtn_JigyoubaSan.GetCodeMasterField = null;
            this.cbtn_JigyoubaSan.Image = ((System.Drawing.Image)(resources.GetObject("cbtn_JigyoubaSan.Image")));
            this.cbtn_JigyoubaSan.ItemDefinedTypes = null;
            this.cbtn_JigyoubaSan.LinkedSettingTextBox = null;
            this.cbtn_JigyoubaSan.LinkedTextBoxs = null;
            this.cbtn_JigyoubaSan.Location = new System.Drawing.Point(663, 129);
            this.cbtn_JigyoubaSan.Name = "cbtn_JigyoubaSan";
            this.cbtn_JigyoubaSan.PopupAfterExecute = null;
            this.cbtn_JigyoubaSan.PopupAfterExecuteMethod = "cantxt_GenbaName_PopupAfterExecuteMethod";
            this.cbtn_JigyoubaSan.PopupBeforeExecute = null;
            this.cbtn_JigyoubaSan.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU";
            this.cbtn_JigyoubaSan.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cbtn_JigyoubaSan.PopupSearchSendParams")));
            this.cbtn_JigyoubaSan.PopupSetFormField = "cantxt_GenbaCd,ctxt_GenbaName";
            this.cbtn_JigyoubaSan.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.cbtn_JigyoubaSan.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.cbtn_JigyoubaSan.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cbtn_JigyoubaSan.popupWindowSetting")));
            this.cbtn_JigyoubaSan.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_JigyoubaSan.RegistCheckMethod")));
            this.cbtn_JigyoubaSan.SearchDisplayFlag = 0;
            this.cbtn_JigyoubaSan.SetFormField = null;
            this.cbtn_JigyoubaSan.ShortItemName = null;
            this.cbtn_JigyoubaSan.Size = new System.Drawing.Size(22, 22);
            this.cbtn_JigyoubaSan.TabIndex = 406;
            this.cbtn_JigyoubaSan.TabStop = false;
            this.cbtn_JigyoubaSan.Tag = "現場の検索を行います";
            this.cbtn_JigyoubaSan.Text = " ";
            this.cbtn_JigyoubaSan.UseVisualStyleBackColor = false;
            this.cbtn_JigyoubaSan.ZeroPaddengFlag = false;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 458);
            this.Controls.Add(this.cbtn_GenbaSan);
            this.Controls.Add(this.cantxt_GenbaCd);
            this.Controls.Add(this.ctxt_GenbaName);
            this.Controls.Add(this.cbtn_GyousyaSan);
            this.Controls.Add(this.cantxt_GyousyaCd);
            this.Controls.Add(this.ctxt_GyousyaName);
            this.Controls.Add(this.customPanel5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.HIDUKE_FROM);
            this.Controls.Add(this.HIDUKE_TO);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.customPanel4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.customPanel3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cantxt_KohuNo);
            this.Controls.Add(this.lbl_KohuNo);
            this.Controls.Add(this.customPanel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.customPanel2);
            this.Controls.Add(this.label1);
            this.Name = "UIForm";
            this.Text = "UIForm";
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.customPanel2, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.customPanel1, 0);
            this.Controls.SetChildIndex(this.lbl_KohuNo, 0);
            this.Controls.SetChildIndex(this.cantxt_KohuNo, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.customPanel3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.customPanel4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.HIDUKE_TO, 0);
            this.Controls.SetChildIndex(this.HIDUKE_FROM, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.customPanel5, 0);
            this.Controls.SetChildIndex(this.ctxt_GyousyaName, 0);
            this.Controls.SetChildIndex(this.cantxt_GyousyaCd, 0);
            this.Controls.SetChildIndex(this.cbtn_GyousyaSan, 0);
            this.Controls.SetChildIndex(this.ctxt_GenbaName, 0);
            this.Controls.SetChildIndex(this.cantxt_GenbaCd, 0);
            this.Controls.SetChildIndex(this.cbtn_GenbaSan, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.customPanel2.ResumeLayout(false);
            this.customPanel2.PerformLayout();
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.customPanel3.ResumeLayout(false);
            this.customPanel3.PerformLayout();
            this.customPanel4.ResumeLayout(false);
            this.customPanel4.PerformLayout();
            this.customPanel5.ResumeLayout(false);
            this.customPanel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal Label label1;
        public r_framework.CustomControl.CustomNumericTextBox2 txtNum_ChushutuTaishouKbn;
        public r_framework.CustomControl.CustomRadioButton radbtn_Ijimanifest;
        public r_framework.CustomControl.CustomRadioButton radbtn_Nijimanifest;
        private r_framework.CustomControl.CustomPanel customPanel2;
        private r_framework.CustomControl.CustomPanel customPanel1;
        public r_framework.CustomControl.CustomRadioButton radbtn_Mi;
        public r_framework.CustomControl.CustomNumericTextBox2 txtNum_HimodukeJyoukyou;
        public r_framework.CustomControl.CustomRadioButton radbtn_Sumi;
        internal Label label2;
        public r_framework.CustomControl.CustomRadioButton radbtn_All;
        internal r_framework.CustomControl.CustomAlphaNumTextBox cantxt_KohuNo;
        internal Label lbl_KohuNo;
        internal Label label3;
        private r_framework.CustomControl.CustomPanel customPanel3;
        public r_framework.CustomControl.CustomRadioButton radbtn_UnpanShuryouDate;
        public r_framework.CustomControl.CustomNumericTextBox2 txtNum_ChushutuHiduke;
        public r_framework.CustomControl.CustomRadioButton radbtn_KoufuDate;
        public r_framework.CustomControl.CustomRadioButton radbtn_ShobunShuryouDate;
        public r_framework.CustomControl.CustomRadioButton radbtn_SaishuShuryouDate;
        internal Label label4;
        private r_framework.CustomControl.CustomPanel customPanel4;
        public r_framework.CustomControl.CustomRadioButton radbtn_Denshi;
        public r_framework.CustomControl.CustomRadioButton radbtn_Kenpai;
        public r_framework.CustomControl.CustomRadioButton radbtn_SanpaiTumikae;
        public r_framework.CustomControl.CustomNumericTextBox2 txtNum_ManifestShurui;
        public r_framework.CustomControl.CustomRadioButton radbtn_SanpaiChokko;
        public r_framework.CustomControl.CustomRadioButton radbtn_AllManifest;
        internal Label label6;
        private r_framework.CustomControl.CustomPanel customPanel5;
        public r_framework.CustomControl.CustomRadioButton radbtn_SaishuShobunBasho;
        public r_framework.CustomControl.CustomRadioButton radbtn_ShobunJutakusha;
        public r_framework.CustomControl.CustomRadioButton radbtn_UnpanJutakusha;
        public r_framework.CustomControl.CustomNumericTextBox2 txtNum_ChushutuGyosha;
        public r_framework.CustomControl.CustomRadioButton radbtn_HaishuJigyosha;
        internal r_framework.CustomControl.CustomPopupOpenButton cbtn_GenbaSan;
        internal r_framework.CustomControl.CustomTextBox ctxt_GenbaName;
        internal r_framework.CustomControl.CustomAlphaNumTextBox cantxt_GenbaCd;
        internal r_framework.CustomControl.CustomPopupOpenButton cbtn_GyousyaSan;
        internal r_framework.CustomControl.CustomTextBox ctxt_GyousyaName;
        internal r_framework.CustomControl.CustomAlphaNumTextBox cantxt_GyousyaCd;
        internal r_framework.CustomControl.CustomPopupOpenButton cbtn_JigyoubaSan;
        internal r_framework.CustomControl.CustomDateTimePicker HIDUKE_FROM;
        internal r_framework.CustomControl.CustomDateTimePicker HIDUKE_TO;
        public Label label5;
    }
}