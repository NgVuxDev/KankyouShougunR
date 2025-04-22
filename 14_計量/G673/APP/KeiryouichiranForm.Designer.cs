using System.Windows.Forms;
using System;

namespace Shougun.Core.Scale.KeiryouIchiran
{
    partial class KeiryouIchiranForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeiryouIchiranForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto5 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto6 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto7 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto8 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto9 = new r_framework.Dto.RangeSettingDto();
            this.KIHON_KEIRYOU_LABEL = new System.Windows.Forms.Label();
            this.rb_KIHON_KEIRYOU_SHUKKA = new r_framework.CustomControl.CustomRadioButton();
            this.rb_KIHON_KEIRYOU_UKEIRE = new r_framework.CustomControl.CustomRadioButton();
            this.rb_KIHON_KEIRYOU_ALL = new r_framework.CustomControl.CustomRadioButton();
            this.KIHON_KEIRYOU_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.KEIRYOU_KBN_LABEL = new System.Windows.Forms.Label();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.customPanel2 = new r_framework.CustomControl.CustomPanel();
            this.rb_KEIRYOU_KBN_KEIJOU = new r_framework.CustomControl.CustomRadioButton();
            this.rb_KEIRYOU_KBN_ALL = new r_framework.CustomControl.CustomRadioButton();
            this.rb_KEIRYOU_KBN_KARI = new r_framework.CustomControl.CustomRadioButton();
            this.KEIRYOU_KBN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.rb_KEIRYOU_KBN_TSUUJOU = new r_framework.CustomControl.CustomRadioButton();
            this.customPanel4 = new r_framework.CustomControl.CustomPanel();
            this.rb_KEIRYOU_JOUKYOU_TAIRYUU = new r_framework.CustomControl.CustomRadioButton();
            this.KEIRYOU_JOUKYOU_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.rb_KEIRYOU_JOUKYOU_KANRYOU = new r_framework.CustomControl.CustomRadioButton();
            this.KEIRYOU_JOUKYOU_LABEL = new System.Windows.Forms.Label();
            this.customPanel3 = new r_framework.CustomControl.CustomPanel();
            this.label15 = new System.Windows.Forms.Label();
            this.txtHonzituSyukkaMount = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label7 = new System.Windows.Forms.Label();
            this.txtHonzituSyukkaNumber = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label8 = new System.Windows.Forms.Label();
            this.txtHonzituUkeireMount = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label9 = new System.Windows.Forms.Label();
            this.txtHonzituUkeireNumber = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label14 = new System.Windows.Forms.Label();
            this.txtSyukkaTairyuNumber = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label13 = new System.Windows.Forms.Label();
            this.txtUkeireTairyuNumber = new r_framework.CustomControl.CustomNumericTextBox2();
            this.lblSyukkaSuryoTani = new System.Windows.Forms.Label();
            this.lblSyukkaTairyuTnai = new System.Windows.Forms.Label();
            this.lblUkeireSuryoTani = new System.Windows.Forms.Label();
            this.lblHonzituUkeireTani = new System.Windows.Forms.Label();
            this.lblHonzituSyukkaTani = new System.Windows.Forms.Label();
            this.lblUkeireTairyuTani = new System.Windows.Forms.Label();
            this.HIDUKE_FROM = new r_framework.CustomControl.CustomDateTimePicker();
            this.HIDUKE_TO = new r_framework.CustomControl.CustomDateTimePicker();
            this.HIDUKE_LABEL = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.customPanel1.SuspendLayout();
            this.customPanel2.SuspendLayout();
            this.customPanel4.SuspendLayout();
            this.customPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.searchString.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.FocusOutCheckMethod")));
            this.searchString.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.searchString.Location = new System.Drawing.Point(3, 3);
            this.searchString.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("searchString.PopupSearchSendParams")));
            this.searchString.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("searchString.popupWindowSetting")));
            this.searchString.ReadOnly = true;
            this.searchString.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.RegistCheckMethod")));
            this.searchString.Size = new System.Drawing.Size(459, 137);
            this.searchString.TabIndex = 10;
            this.searchString.TabStop = false;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Location = new System.Drawing.Point(4, 427);
            this.bt_ptn1.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn1.TabIndex = 840;
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn2.TabIndex = 850;
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Location = new System.Drawing.Point(404, 427);
            this.bt_ptn3.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn3.TabIndex = 860;
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Location = new System.Drawing.Point(604, 427);
            this.bt_ptn4.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn4.TabIndex = 870;
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Location = new System.Drawing.Point(804, 427);
            this.bt_ptn5.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn5.TabIndex = 880;
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.AutoSize = true;
            this.customSortHeader1.Location = new System.Drawing.Point(4, 167);
            this.customSortHeader1.TabIndex = 810;
            // 
            // customSearchHeader1
            // 
            this.customSearchHeader1.Location = new System.Drawing.Point(4, 140);
            this.customSearchHeader1.TabIndex = 820;
            // 
            // KIHON_KEIRYOU_LABEL
            // 
            this.KIHON_KEIRYOU_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.KIHON_KEIRYOU_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KIHON_KEIRYOU_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.KIHON_KEIRYOU_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KIHON_KEIRYOU_LABEL.ForeColor = System.Drawing.Color.White;
            this.KIHON_KEIRYOU_LABEL.Location = new System.Drawing.Point(468, 26);
            this.KIHON_KEIRYOU_LABEL.Name = "KIHON_KEIRYOU_LABEL";
            this.KIHON_KEIRYOU_LABEL.Size = new System.Drawing.Size(110, 20);
            this.KIHON_KEIRYOU_LABEL.TabIndex = 340;
            this.KIHON_KEIRYOU_LABEL.Text = "基本計量※";
            this.KIHON_KEIRYOU_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rb_KIHON_KEIRYOU_SHUKKA
            // 
            this.rb_KIHON_KEIRYOU_SHUKKA.AutoSize = true;
            this.rb_KIHON_KEIRYOU_SHUKKA.DefaultBackColor = System.Drawing.Color.Empty;
            this.rb_KIHON_KEIRYOU_SHUKKA.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_KIHON_KEIRYOU_SHUKKA.FocusOutCheckMethod")));
            this.rb_KIHON_KEIRYOU_SHUKKA.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rb_KIHON_KEIRYOU_SHUKKA.LinkedTextBox = "KIHON_KEIRYOU_CD";
            this.rb_KIHON_KEIRYOU_SHUKKA.Location = new System.Drawing.Point(95, 0);
            this.rb_KIHON_KEIRYOU_SHUKKA.Name = "rb_KIHON_KEIRYOU_SHUKKA";
            this.rb_KIHON_KEIRYOU_SHUKKA.PopupAfterExecute = null;
            this.rb_KIHON_KEIRYOU_SHUKKA.PopupBeforeExecute = null;
            this.rb_KIHON_KEIRYOU_SHUKKA.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rb_KIHON_KEIRYOU_SHUKKA.PopupSearchSendParams")));
            this.rb_KIHON_KEIRYOU_SHUKKA.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rb_KIHON_KEIRYOU_SHUKKA.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rb_KIHON_KEIRYOU_SHUKKA.popupWindowSetting")));
            this.rb_KIHON_KEIRYOU_SHUKKA.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_KIHON_KEIRYOU_SHUKKA.RegistCheckMethod")));
            this.rb_KIHON_KEIRYOU_SHUKKA.Size = new System.Drawing.Size(67, 17);
            this.rb_KIHON_KEIRYOU_SHUKKA.TabIndex = 20;
            this.rb_KIHON_KEIRYOU_SHUKKA.Tag = "基本計量が「2.出荷」の場合にはチェックを付けてください";
            this.rb_KIHON_KEIRYOU_SHUKKA.Text = "2.出荷";
            this.rb_KIHON_KEIRYOU_SHUKKA.UseVisualStyleBackColor = true;
            this.rb_KIHON_KEIRYOU_SHUKKA.Value = "2";
            // 
            // rb_KIHON_KEIRYOU_UKEIRE
            // 
            this.rb_KIHON_KEIRYOU_UKEIRE.AutoSize = true;
            this.rb_KIHON_KEIRYOU_UKEIRE.DefaultBackColor = System.Drawing.Color.Empty;
            this.rb_KIHON_KEIRYOU_UKEIRE.DisplayItemName = "asdasd";
            this.rb_KIHON_KEIRYOU_UKEIRE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_KIHON_KEIRYOU_UKEIRE.FocusOutCheckMethod")));
            this.rb_KIHON_KEIRYOU_UKEIRE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rb_KIHON_KEIRYOU_UKEIRE.LinkedTextBox = "KIHON_KEIRYOU_CD";
            this.rb_KIHON_KEIRYOU_UKEIRE.Location = new System.Drawing.Point(22, 0);
            this.rb_KIHON_KEIRYOU_UKEIRE.Name = "rb_KIHON_KEIRYOU_UKEIRE";
            this.rb_KIHON_KEIRYOU_UKEIRE.PopupAfterExecute = null;
            this.rb_KIHON_KEIRYOU_UKEIRE.PopupBeforeExecute = null;
            this.rb_KIHON_KEIRYOU_UKEIRE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rb_KIHON_KEIRYOU_UKEIRE.PopupSearchSendParams")));
            this.rb_KIHON_KEIRYOU_UKEIRE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rb_KIHON_KEIRYOU_UKEIRE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rb_KIHON_KEIRYOU_UKEIRE.popupWindowSetting")));
            this.rb_KIHON_KEIRYOU_UKEIRE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_KIHON_KEIRYOU_UKEIRE.RegistCheckMethod")));
            this.rb_KIHON_KEIRYOU_UKEIRE.Size = new System.Drawing.Size(67, 17);
            this.rb_KIHON_KEIRYOU_UKEIRE.TabIndex = 10;
            this.rb_KIHON_KEIRYOU_UKEIRE.Tag = "基本計量が「1.受入」の場合にはチェックを付けてください";
            this.rb_KIHON_KEIRYOU_UKEIRE.Text = "1.受入";
            this.rb_KIHON_KEIRYOU_UKEIRE.UseVisualStyleBackColor = true;
            this.rb_KIHON_KEIRYOU_UKEIRE.Value = "1";
            // 
            // rb_KIHON_KEIRYOU_ALL
            // 
            this.rb_KIHON_KEIRYOU_ALL.AutoSize = true;
            this.rb_KIHON_KEIRYOU_ALL.DefaultBackColor = System.Drawing.Color.Empty;
            this.rb_KIHON_KEIRYOU_ALL.DisplayItemName = "asdasd";
            this.rb_KIHON_KEIRYOU_ALL.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_KIHON_KEIRYOU_ALL.FocusOutCheckMethod")));
            this.rb_KIHON_KEIRYOU_ALL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rb_KIHON_KEIRYOU_ALL.LinkedTextBox = "KIHON_KEIRYOU_CD";
            this.rb_KIHON_KEIRYOU_ALL.Location = new System.Drawing.Point(168, 0);
            this.rb_KIHON_KEIRYOU_ALL.Name = "rb_KIHON_KEIRYOU_ALL";
            this.rb_KIHON_KEIRYOU_ALL.PopupAfterExecute = null;
            this.rb_KIHON_KEIRYOU_ALL.PopupBeforeExecute = null;
            this.rb_KIHON_KEIRYOU_ALL.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rb_KIHON_KEIRYOU_ALL.PopupSearchSendParams")));
            this.rb_KIHON_KEIRYOU_ALL.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rb_KIHON_KEIRYOU_ALL.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rb_KIHON_KEIRYOU_ALL.popupWindowSetting")));
            this.rb_KIHON_KEIRYOU_ALL.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_KIHON_KEIRYOU_ALL.RegistCheckMethod")));
            this.rb_KIHON_KEIRYOU_ALL.Size = new System.Drawing.Size(67, 17);
            this.rb_KIHON_KEIRYOU_ALL.TabIndex = 30;
            this.rb_KIHON_KEIRYOU_ALL.Tag = "基本計量が「3.全て」の場合にはチェックを付けてください";
            this.rb_KIHON_KEIRYOU_ALL.Text = "3.全て";
            this.rb_KIHON_KEIRYOU_ALL.UseVisualStyleBackColor = true;
            this.rb_KIHON_KEIRYOU_ALL.Value = "3";
            // 
            // KIHON_KEIRYOU_CD
            // 
            this.KIHON_KEIRYOU_CD.BackColor = System.Drawing.SystemColors.Window;
            this.KIHON_KEIRYOU_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KIHON_KEIRYOU_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.KIHON_KEIRYOU_CD.DisplayItemName = "基本計量";
            this.KIHON_KEIRYOU_CD.DisplayPopUp = null;
            this.KIHON_KEIRYOU_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KIHON_KEIRYOU_CD.FocusOutCheckMethod")));
            this.KIHON_KEIRYOU_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KIHON_KEIRYOU_CD.ForeColor = System.Drawing.Color.Black;
            this.KIHON_KEIRYOU_CD.IsInputErrorOccured = false;
            this.KIHON_KEIRYOU_CD.LinkedRadioButtonArray = new string[] {
        "rb_KIHON_KEIRYOU_UKEIRE",
        "rb_KIHON_KEIRYOU_SHUKKA",
        "rb_KIHON_KEIRYOU_ALL"};
            this.KIHON_KEIRYOU_CD.Location = new System.Drawing.Point(-1, -1);
            this.KIHON_KEIRYOU_CD.Name = "KIHON_KEIRYOU_CD";
            this.KIHON_KEIRYOU_CD.PopupAfterExecute = null;
            this.KIHON_KEIRYOU_CD.PopupBeforeExecute = null;
            this.KIHON_KEIRYOU_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KIHON_KEIRYOU_CD.PopupSearchSendParams")));
            this.KIHON_KEIRYOU_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KIHON_KEIRYOU_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KIHON_KEIRYOU_CD.popupWindowSetting")));
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
            this.KIHON_KEIRYOU_CD.RangeSetting = rangeSettingDto1;
            this.KIHON_KEIRYOU_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KIHON_KEIRYOU_CD.RegistCheckMethod")));
            this.KIHON_KEIRYOU_CD.Size = new System.Drawing.Size(20, 20);
            this.KIHON_KEIRYOU_CD.TabIndex = 0;
            this.KIHON_KEIRYOU_CD.Tag = "【1～3】のいずれかで入力してください";
            this.KIHON_KEIRYOU_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.KIHON_KEIRYOU_CD.WordWrap = false;
            // 
            // KEIRYOU_KBN_LABEL
            // 
            this.KEIRYOU_KBN_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.KEIRYOU_KBN_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KEIRYOU_KBN_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.KEIRYOU_KBN_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KEIRYOU_KBN_LABEL.ForeColor = System.Drawing.Color.White;
            this.KEIRYOU_KBN_LABEL.Location = new System.Drawing.Point(468, 49);
            this.KEIRYOU_KBN_LABEL.Name = "KEIRYOU_KBN_LABEL";
            this.KEIRYOU_KBN_LABEL.Size = new System.Drawing.Size(110, 20);
            this.KEIRYOU_KBN_LABEL.TabIndex = 360;
            this.KEIRYOU_KBN_LABEL.Text = "計量区分※";
            this.KEIRYOU_KBN_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel1
            // 
            this.customPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel1.Controls.Add(this.rb_KIHON_KEIRYOU_UKEIRE);
            this.customPanel1.Controls.Add(this.rb_KIHON_KEIRYOU_SHUKKA);
            this.customPanel1.Controls.Add(this.rb_KIHON_KEIRYOU_ALL);
            this.customPanel1.Controls.Add(this.KIHON_KEIRYOU_CD);
            this.customPanel1.Location = new System.Drawing.Point(583, 26);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(361, 20);
            this.customPanel1.TabIndex = 350;
            // 
            // customPanel2
            // 
            this.customPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel2.Controls.Add(this.rb_KEIRYOU_KBN_KEIJOU);
            this.customPanel2.Controls.Add(this.rb_KEIRYOU_KBN_ALL);
            this.customPanel2.Controls.Add(this.rb_KEIRYOU_KBN_KARI);
            this.customPanel2.Controls.Add(this.KEIRYOU_KBN_CD);
            this.customPanel2.Controls.Add(this.rb_KEIRYOU_KBN_TSUUJOU);
            this.customPanel2.Location = new System.Drawing.Point(583, 49);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(361, 20);
            this.customPanel2.TabIndex = 370;
            // 
            // rb_KEIRYOU_KBN_KEIJOU
            // 
            this.rb_KEIRYOU_KBN_KEIJOU.AutoSize = true;
            this.rb_KEIRYOU_KBN_KEIJOU.DefaultBackColor = System.Drawing.Color.Empty;
            this.rb_KEIRYOU_KBN_KEIJOU.DisplayItemName = "asdasd";
            this.rb_KEIRYOU_KBN_KEIJOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_KEIRYOU_KBN_KEIJOU.FocusOutCheckMethod")));
            this.rb_KEIRYOU_KBN_KEIJOU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rb_KEIRYOU_KBN_KEIJOU.LinkedTextBox = "KEIRYOU_KBN_CD";
            this.rb_KEIRYOU_KBN_KEIJOU.Location = new System.Drawing.Point(210, 0);
            this.rb_KEIRYOU_KBN_KEIJOU.Name = "rb_KEIRYOU_KBN_KEIJOU";
            this.rb_KEIRYOU_KBN_KEIJOU.PopupAfterExecute = null;
            this.rb_KEIRYOU_KBN_KEIJOU.PopupBeforeExecute = null;
            this.rb_KEIRYOU_KBN_KEIJOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rb_KEIRYOU_KBN_KEIJOU.PopupSearchSendParams")));
            this.rb_KEIRYOU_KBN_KEIJOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rb_KEIRYOU_KBN_KEIJOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rb_KEIRYOU_KBN_KEIJOU.popupWindowSetting")));
            this.rb_KEIRYOU_KBN_KEIJOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_KEIRYOU_KBN_KEIJOU.RegistCheckMethod")));
            this.rb_KEIRYOU_KBN_KEIJOU.Size = new System.Drawing.Size(67, 17);
            this.rb_KEIRYOU_KBN_KEIJOU.TabIndex = 30;
            this.rb_KEIRYOU_KBN_KEIJOU.Tag = "計量区分が「3.計上」の場合にはチェックを付けてください";
            this.rb_KEIRYOU_KBN_KEIJOU.Text = "3.計上";
            this.rb_KEIRYOU_KBN_KEIJOU.UseVisualStyleBackColor = true;
            this.rb_KEIRYOU_KBN_KEIJOU.Value = "3";
            // 
            // rb_KEIRYOU_KBN_ALL
            // 
            this.rb_KEIRYOU_KBN_ALL.AutoSize = true;
            this.rb_KEIRYOU_KBN_ALL.DefaultBackColor = System.Drawing.Color.Empty;
            this.rb_KEIRYOU_KBN_ALL.DisplayItemName = "asdasd";
            this.rb_KEIRYOU_KBN_ALL.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_KEIRYOU_KBN_ALL.FocusOutCheckMethod")));
            this.rb_KEIRYOU_KBN_ALL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rb_KEIRYOU_KBN_ALL.LinkedTextBox = "KEIRYOU_KBN_CD";
            this.rb_KEIRYOU_KBN_ALL.Location = new System.Drawing.Point(283, 0);
            this.rb_KEIRYOU_KBN_ALL.Name = "rb_KEIRYOU_KBN_ALL";
            this.rb_KEIRYOU_KBN_ALL.PopupAfterExecute = null;
            this.rb_KEIRYOU_KBN_ALL.PopupBeforeExecute = null;
            this.rb_KEIRYOU_KBN_ALL.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rb_KEIRYOU_KBN_ALL.PopupSearchSendParams")));
            this.rb_KEIRYOU_KBN_ALL.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rb_KEIRYOU_KBN_ALL.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rb_KEIRYOU_KBN_ALL.popupWindowSetting")));
            this.rb_KEIRYOU_KBN_ALL.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_KEIRYOU_KBN_ALL.RegistCheckMethod")));
            this.rb_KEIRYOU_KBN_ALL.Size = new System.Drawing.Size(67, 17);
            this.rb_KEIRYOU_KBN_ALL.TabIndex = 40;
            this.rb_KEIRYOU_KBN_ALL.Tag = "計量区分が「4.全て」の場合にはチェックを付けてください";
            this.rb_KEIRYOU_KBN_ALL.Text = "4.全て";
            this.rb_KEIRYOU_KBN_ALL.UseVisualStyleBackColor = true;
            this.rb_KEIRYOU_KBN_ALL.Value = "4";
            // 
            // rb_KEIRYOU_KBN_KARI
            // 
            this.rb_KEIRYOU_KBN_KARI.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.rb_KEIRYOU_KBN_KARI.AutoSize = true;
            this.rb_KEIRYOU_KBN_KARI.DefaultBackColor = System.Drawing.Color.Empty;
            this.rb_KEIRYOU_KBN_KARI.DisplayItemName = "asdasd";
            this.rb_KEIRYOU_KBN_KARI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_KEIRYOU_KBN_KARI.FocusOutCheckMethod")));
            this.rb_KEIRYOU_KBN_KARI.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rb_KEIRYOU_KBN_KARI.LinkedTextBox = "KEIRYOU_KBN_CD";
            this.rb_KEIRYOU_KBN_KARI.Location = new System.Drawing.Point(123, 0);
            this.rb_KEIRYOU_KBN_KARI.Name = "rb_KEIRYOU_KBN_KARI";
            this.rb_KEIRYOU_KBN_KARI.PopupAfterExecute = null;
            this.rb_KEIRYOU_KBN_KARI.PopupBeforeExecute = null;
            this.rb_KEIRYOU_KBN_KARI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rb_KEIRYOU_KBN_KARI.PopupSearchSendParams")));
            this.rb_KEIRYOU_KBN_KARI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rb_KEIRYOU_KBN_KARI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rb_KEIRYOU_KBN_KARI.popupWindowSetting")));
            this.rb_KEIRYOU_KBN_KARI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_KEIRYOU_KBN_KARI.RegistCheckMethod")));
            this.rb_KEIRYOU_KBN_KARI.Size = new System.Drawing.Size(81, 17);
            this.rb_KEIRYOU_KBN_KARI.TabIndex = 20;
            this.rb_KEIRYOU_KBN_KARI.Tag = "計量区分が「2.仮登録」の場合にはチェックを付けてください";
            this.rb_KEIRYOU_KBN_KARI.Text = "2.仮登録";
            this.rb_KEIRYOU_KBN_KARI.UseVisualStyleBackColor = true;
            this.rb_KEIRYOU_KBN_KARI.Value = "2";
            // 
            // KEIRYOU_KBN_CD
            // 
            this.KEIRYOU_KBN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.KEIRYOU_KBN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KEIRYOU_KBN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.KEIRYOU_KBN_CD.DisplayItemName = "計量区分";
            this.KEIRYOU_KBN_CD.DisplayPopUp = null;
            this.KEIRYOU_KBN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIRYOU_KBN_CD.FocusOutCheckMethod")));
            this.KEIRYOU_KBN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KEIRYOU_KBN_CD.ForeColor = System.Drawing.Color.Black;
            this.KEIRYOU_KBN_CD.IsInputErrorOccured = false;
            this.KEIRYOU_KBN_CD.LinkedRadioButtonArray = new string[] {
        "rb_KEIRYOU_KBN_TSUUJOU",
        "rb_KEIRYOU_KBN_KARI",
        "rb_KEIRYOU_KBN_KEIJOU",
        "rb_KEIRYOU_KBN_ALL"};
            this.KEIRYOU_KBN_CD.Location = new System.Drawing.Point(-1, -1);
            this.KEIRYOU_KBN_CD.Name = "KEIRYOU_KBN_CD";
            this.KEIRYOU_KBN_CD.PopupAfterExecute = null;
            this.KEIRYOU_KBN_CD.PopupBeforeExecute = null;
            this.KEIRYOU_KBN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KEIRYOU_KBN_CD.PopupSearchSendParams")));
            this.KEIRYOU_KBN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KEIRYOU_KBN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KEIRYOU_KBN_CD.popupWindowSetting")));
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
            this.KEIRYOU_KBN_CD.RangeSetting = rangeSettingDto2;
            this.KEIRYOU_KBN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIRYOU_KBN_CD.RegistCheckMethod")));
            this.KEIRYOU_KBN_CD.Size = new System.Drawing.Size(20, 20);
            this.KEIRYOU_KBN_CD.TabIndex = 0;
            this.KEIRYOU_KBN_CD.Tag = "【1～4】のいずれかで入力してください";
            this.KEIRYOU_KBN_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.KEIRYOU_KBN_CD.WordWrap = false;
            // 
            // rb_KEIRYOU_KBN_TSUUJOU
            // 
            this.rb_KEIRYOU_KBN_TSUUJOU.AutoSize = true;
            this.rb_KEIRYOU_KBN_TSUUJOU.DefaultBackColor = System.Drawing.Color.Empty;
            this.rb_KEIRYOU_KBN_TSUUJOU.DisplayItemName = "asdasd";
            this.rb_KEIRYOU_KBN_TSUUJOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_KEIRYOU_KBN_TSUUJOU.FocusOutCheckMethod")));
            this.rb_KEIRYOU_KBN_TSUUJOU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rb_KEIRYOU_KBN_TSUUJOU.LinkedTextBox = "KEIRYOU_KBN_CD";
            this.rb_KEIRYOU_KBN_TSUUJOU.Location = new System.Drawing.Point(22, 0);
            this.rb_KEIRYOU_KBN_TSUUJOU.Name = "rb_KEIRYOU_KBN_TSUUJOU";
            this.rb_KEIRYOU_KBN_TSUUJOU.PopupAfterExecute = null;
            this.rb_KEIRYOU_KBN_TSUUJOU.PopupBeforeExecute = null;
            this.rb_KEIRYOU_KBN_TSUUJOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rb_KEIRYOU_KBN_TSUUJOU.PopupSearchSendParams")));
            this.rb_KEIRYOU_KBN_TSUUJOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rb_KEIRYOU_KBN_TSUUJOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rb_KEIRYOU_KBN_TSUUJOU.popupWindowSetting")));
            this.rb_KEIRYOU_KBN_TSUUJOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_KEIRYOU_KBN_TSUUJOU.RegistCheckMethod")));
            this.rb_KEIRYOU_KBN_TSUUJOU.Size = new System.Drawing.Size(95, 17);
            this.rb_KEIRYOU_KBN_TSUUJOU.TabIndex = 10;
            this.rb_KEIRYOU_KBN_TSUUJOU.Tag = "計量区分が「1.通常登録」の場合にはチェックを付けてください";
            this.rb_KEIRYOU_KBN_TSUUJOU.Text = "1.通常登録";
            this.rb_KEIRYOU_KBN_TSUUJOU.UseVisualStyleBackColor = true;
            this.rb_KEIRYOU_KBN_TSUUJOU.Value = "1";
            // 
            // customPanel4
            // 
            this.customPanel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel4.Controls.Add(this.rb_KEIRYOU_JOUKYOU_TAIRYUU);
            this.customPanel4.Controls.Add(this.KEIRYOU_JOUKYOU_CD);
            this.customPanel4.Controls.Add(this.rb_KEIRYOU_JOUKYOU_KANRYOU);
            this.customPanel4.Location = new System.Drawing.Point(583, 72);
            this.customPanel4.Name = "customPanel4";
            this.customPanel4.Size = new System.Drawing.Size(361, 20);
            this.customPanel4.TabIndex = 390;
            // 
            // rb_KEIRYOU_JOUKYOU_TAIRYUU
            // 
            this.rb_KEIRYOU_JOUKYOU_TAIRYUU.AutoSize = true;
            this.rb_KEIRYOU_JOUKYOU_TAIRYUU.DefaultBackColor = System.Drawing.Color.Empty;
            this.rb_KEIRYOU_JOUKYOU_TAIRYUU.DisplayItemName = "asdasd";
            this.rb_KEIRYOU_JOUKYOU_TAIRYUU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_KEIRYOU_JOUKYOU_TAIRYUU.FocusOutCheckMethod")));
            this.rb_KEIRYOU_JOUKYOU_TAIRYUU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rb_KEIRYOU_JOUKYOU_TAIRYUU.LinkedTextBox = "KEIRYOU_JOUKYOU_CD";
            this.rb_KEIRYOU_JOUKYOU_TAIRYUU.Location = new System.Drawing.Point(123, 0);
            this.rb_KEIRYOU_JOUKYOU_TAIRYUU.Name = "rb_KEIRYOU_JOUKYOU_TAIRYUU";
            this.rb_KEIRYOU_JOUKYOU_TAIRYUU.PopupAfterExecute = null;
            this.rb_KEIRYOU_JOUKYOU_TAIRYUU.PopupBeforeExecute = null;
            this.rb_KEIRYOU_JOUKYOU_TAIRYUU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rb_KEIRYOU_JOUKYOU_TAIRYUU.PopupSearchSendParams")));
            this.rb_KEIRYOU_JOUKYOU_TAIRYUU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rb_KEIRYOU_JOUKYOU_TAIRYUU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rb_KEIRYOU_JOUKYOU_TAIRYUU.popupWindowSetting")));
            this.rb_KEIRYOU_JOUKYOU_TAIRYUU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_KEIRYOU_JOUKYOU_TAIRYUU.RegistCheckMethod")));
            this.rb_KEIRYOU_JOUKYOU_TAIRYUU.Size = new System.Drawing.Size(67, 17);
            this.rb_KEIRYOU_JOUKYOU_TAIRYUU.TabIndex = 20;
            this.rb_KEIRYOU_JOUKYOU_TAIRYUU.Tag = "計量状況が「2.滞留」の場合にはチェックを付けてください";
            this.rb_KEIRYOU_JOUKYOU_TAIRYUU.Text = "2.滞留";
            this.rb_KEIRYOU_JOUKYOU_TAIRYUU.UseVisualStyleBackColor = true;
            this.rb_KEIRYOU_JOUKYOU_TAIRYUU.Value = "2";
            // 
            // KEIRYOU_JOUKYOU_CD
            // 
            this.KEIRYOU_JOUKYOU_CD.BackColor = System.Drawing.SystemColors.Window;
            this.KEIRYOU_JOUKYOU_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KEIRYOU_JOUKYOU_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.KEIRYOU_JOUKYOU_CD.DisplayItemName = "計量状況";
            this.KEIRYOU_JOUKYOU_CD.DisplayPopUp = null;
            this.KEIRYOU_JOUKYOU_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIRYOU_JOUKYOU_CD.FocusOutCheckMethod")));
            this.KEIRYOU_JOUKYOU_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KEIRYOU_JOUKYOU_CD.ForeColor = System.Drawing.Color.Black;
            this.KEIRYOU_JOUKYOU_CD.IsInputErrorOccured = false;
            this.KEIRYOU_JOUKYOU_CD.LinkedRadioButtonArray = new string[] {
        "rb_KEIRYOU_JOUKYOU_KANRYOU",
        "rb_KEIRYOU_JOUKYOU_TAIRYUU"};
            this.KEIRYOU_JOUKYOU_CD.Location = new System.Drawing.Point(-1, -1);
            this.KEIRYOU_JOUKYOU_CD.Name = "KEIRYOU_JOUKYOU_CD";
            this.KEIRYOU_JOUKYOU_CD.PopupAfterExecute = null;
            this.KEIRYOU_JOUKYOU_CD.PopupBeforeExecute = null;
            this.KEIRYOU_JOUKYOU_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KEIRYOU_JOUKYOU_CD.PopupSearchSendParams")));
            this.KEIRYOU_JOUKYOU_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KEIRYOU_JOUKYOU_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KEIRYOU_JOUKYOU_CD.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto3.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.KEIRYOU_JOUKYOU_CD.RangeSetting = rangeSettingDto3;
            this.KEIRYOU_JOUKYOU_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIRYOU_JOUKYOU_CD.RegistCheckMethod")));
            this.KEIRYOU_JOUKYOU_CD.Size = new System.Drawing.Size(20, 20);
            this.KEIRYOU_JOUKYOU_CD.TabIndex = 0;
            this.KEIRYOU_JOUKYOU_CD.Tag = "【1～2】のいずれかで入力してください";
            this.KEIRYOU_JOUKYOU_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.KEIRYOU_JOUKYOU_CD.WordWrap = false;
            // 
            // rb_KEIRYOU_JOUKYOU_KANRYOU
            // 
            this.rb_KEIRYOU_JOUKYOU_KANRYOU.AutoSize = true;
            this.rb_KEIRYOU_JOUKYOU_KANRYOU.DefaultBackColor = System.Drawing.Color.Empty;
            this.rb_KEIRYOU_JOUKYOU_KANRYOU.DisplayItemName = "asdasd";
            this.rb_KEIRYOU_JOUKYOU_KANRYOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_KEIRYOU_JOUKYOU_KANRYOU.FocusOutCheckMethod")));
            this.rb_KEIRYOU_JOUKYOU_KANRYOU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rb_KEIRYOU_JOUKYOU_KANRYOU.LinkedTextBox = "KEIRYOU_JOUKYOU_CD";
            this.rb_KEIRYOU_JOUKYOU_KANRYOU.Location = new System.Drawing.Point(22, 0);
            this.rb_KEIRYOU_JOUKYOU_KANRYOU.Name = "rb_KEIRYOU_JOUKYOU_KANRYOU";
            this.rb_KEIRYOU_JOUKYOU_KANRYOU.PopupAfterExecute = null;
            this.rb_KEIRYOU_JOUKYOU_KANRYOU.PopupBeforeExecute = null;
            this.rb_KEIRYOU_JOUKYOU_KANRYOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rb_KEIRYOU_JOUKYOU_KANRYOU.PopupSearchSendParams")));
            this.rb_KEIRYOU_JOUKYOU_KANRYOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rb_KEIRYOU_JOUKYOU_KANRYOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rb_KEIRYOU_JOUKYOU_KANRYOU.popupWindowSetting")));
            this.rb_KEIRYOU_JOUKYOU_KANRYOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rb_KEIRYOU_JOUKYOU_KANRYOU.RegistCheckMethod")));
            this.rb_KEIRYOU_JOUKYOU_KANRYOU.Size = new System.Drawing.Size(95, 17);
            this.rb_KEIRYOU_JOUKYOU_KANRYOU.TabIndex = 10;
            this.rb_KEIRYOU_JOUKYOU_KANRYOU.Tag = "計量状況が「1.計量完了」の場合にはチェックを付けてください";
            this.rb_KEIRYOU_JOUKYOU_KANRYOU.Text = "1.計量完了";
            this.rb_KEIRYOU_JOUKYOU_KANRYOU.UseVisualStyleBackColor = true;
            this.rb_KEIRYOU_JOUKYOU_KANRYOU.Value = "1";
            // 
            // KEIRYOU_JOUKYOU_LABEL
            // 
            this.KEIRYOU_JOUKYOU_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.KEIRYOU_JOUKYOU_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KEIRYOU_JOUKYOU_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.KEIRYOU_JOUKYOU_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KEIRYOU_JOUKYOU_LABEL.ForeColor = System.Drawing.Color.White;
            this.KEIRYOU_JOUKYOU_LABEL.Location = new System.Drawing.Point(468, 72);
            this.KEIRYOU_JOUKYOU_LABEL.Name = "KEIRYOU_JOUKYOU_LABEL";
            this.KEIRYOU_JOUKYOU_LABEL.Size = new System.Drawing.Size(110, 20);
            this.KEIRYOU_JOUKYOU_LABEL.TabIndex = 380;
            this.KEIRYOU_JOUKYOU_LABEL.Text = "計量状況※";
            this.KEIRYOU_JOUKYOU_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel3
            // 
            this.customPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.customPanel3.Controls.Add(this.label15);
            this.customPanel3.Controls.Add(this.txtHonzituSyukkaMount);
            this.customPanel3.Controls.Add(this.label7);
            this.customPanel3.Controls.Add(this.txtHonzituSyukkaNumber);
            this.customPanel3.Controls.Add(this.label8);
            this.customPanel3.Controls.Add(this.txtHonzituUkeireMount);
            this.customPanel3.Controls.Add(this.label9);
            this.customPanel3.Controls.Add(this.txtHonzituUkeireNumber);
            this.customPanel3.Controls.Add(this.label14);
            this.customPanel3.Controls.Add(this.txtSyukkaTairyuNumber);
            this.customPanel3.Controls.Add(this.label13);
            this.customPanel3.Controls.Add(this.txtUkeireTairyuNumber);
            this.customPanel3.Controls.Add(this.lblSyukkaSuryoTani);
            this.customPanel3.Controls.Add(this.lblSyukkaTairyuTnai);
            this.customPanel3.Controls.Add(this.lblUkeireSuryoTani);
            this.customPanel3.Controls.Add(this.lblHonzituUkeireTani);
            this.customPanel3.Controls.Add(this.lblHonzituSyukkaTani);
            this.customPanel3.Controls.Add(this.lblUkeireTairyuTani);
            this.customPanel3.Location = new System.Drawing.Point(949, 0);
            this.customPanel3.Name = "customPanel3";
            this.customPanel3.Size = new System.Drawing.Size(184, 138);
            this.customPanel3.TabIndex = 700;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label15.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(93, 3);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(86, 20);
            this.label15.TabIndex = 30;
            this.label15.Text = "出荷滞留数";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtHonzituSyukkaMount
            // 
            this.txtHonzituSyukkaMount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtHonzituSyukkaMount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHonzituSyukkaMount.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtHonzituSyukkaMount.DisplayPopUp = null;
            this.txtHonzituSyukkaMount.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtHonzituSyukkaMount.FocusOutCheckMethod")));
            this.txtHonzituSyukkaMount.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.txtHonzituSyukkaMount.ForeColor = System.Drawing.Color.Black;
            this.txtHonzituSyukkaMount.FormatSetting = "システム設定(重量書式)";
            this.txtHonzituSyukkaMount.IsInputErrorOccured = false;
            this.txtHonzituSyukkaMount.Location = new System.Drawing.Point(93, 109);
            this.txtHonzituSyukkaMount.Name = "txtHonzituSyukkaMount";
            this.txtHonzituSyukkaMount.PopupAfterExecute = null;
            this.txtHonzituSyukkaMount.PopupBeforeExecute = null;
            this.txtHonzituSyukkaMount.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtHonzituSyukkaMount.PopupSearchSendParams")));
            this.txtHonzituSyukkaMount.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtHonzituSyukkaMount.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtHonzituSyukkaMount.popupWindowSetting")));
            this.txtHonzituSyukkaMount.RangeSetting = rangeSettingDto4;
            this.txtHonzituSyukkaMount.ReadOnly = true;
            this.txtHonzituSyukkaMount.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtHonzituSyukkaMount.RegistCheckMethod")));
            this.txtHonzituSyukkaMount.Size = new System.Drawing.Size(64, 19);
            this.txtHonzituSyukkaMount.TabIndex = 160;
            this.txtHonzituSyukkaMount.TabStop = false;
            this.txtHonzituSyukkaMount.Tag = "本日の出荷数量が表示されます";
            this.txtHonzituSyukkaMount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtHonzituSyukkaMount.WordWrap = false;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(2, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "受入滞留数";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtHonzituSyukkaNumber
            // 
            this.txtHonzituSyukkaNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtHonzituSyukkaNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHonzituSyukkaNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtHonzituSyukkaNumber.DisplayPopUp = null;
            this.txtHonzituSyukkaNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtHonzituSyukkaNumber.FocusOutCheckMethod")));
            this.txtHonzituSyukkaNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.txtHonzituSyukkaNumber.ForeColor = System.Drawing.Color.Black;
            this.txtHonzituSyukkaNumber.FormatSetting = "カスタム";
            this.txtHonzituSyukkaNumber.IsInputErrorOccured = false;
            this.txtHonzituSyukkaNumber.Location = new System.Drawing.Point(93, 66);
            this.txtHonzituSyukkaNumber.Name = "txtHonzituSyukkaNumber";
            this.txtHonzituSyukkaNumber.PopupAfterExecute = null;
            this.txtHonzituSyukkaNumber.PopupBeforeExecute = null;
            this.txtHonzituSyukkaNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtHonzituSyukkaNumber.PopupSearchSendParams")));
            this.txtHonzituSyukkaNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtHonzituSyukkaNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtHonzituSyukkaNumber.popupWindowSetting")));
            this.txtHonzituSyukkaNumber.RangeSetting = rangeSettingDto5;
            this.txtHonzituSyukkaNumber.ReadOnly = true;
            this.txtHonzituSyukkaNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtHonzituSyukkaNumber.RegistCheckMethod")));
            this.txtHonzituSyukkaNumber.Size = new System.Drawing.Size(64, 19);
            this.txtHonzituSyukkaNumber.TabIndex = 100;
            this.txtHonzituSyukkaNumber.TabStop = false;
            this.txtHonzituSyukkaNumber.Tag = "本日の出荷台数が表示されます";
            this.txtHonzituSyukkaNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtHonzituSyukkaNumber.WordWrap = false;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(2, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 20);
            this.label8.TabIndex = 60;
            this.label8.Text = "本日受入台数";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtHonzituUkeireMount
            // 
            this.txtHonzituUkeireMount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtHonzituUkeireMount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHonzituUkeireMount.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtHonzituUkeireMount.DisplayPopUp = null;
            this.txtHonzituUkeireMount.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtHonzituUkeireMount.FocusOutCheckMethod")));
            this.txtHonzituUkeireMount.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.txtHonzituUkeireMount.ForeColor = System.Drawing.Color.Black;
            this.txtHonzituUkeireMount.FormatSetting = "システム設定(重量書式)";
            this.txtHonzituUkeireMount.IsInputErrorOccured = false;
            this.txtHonzituUkeireMount.Location = new System.Drawing.Point(2, 109);
            this.txtHonzituUkeireMount.Name = "txtHonzituUkeireMount";
            this.txtHonzituUkeireMount.PopupAfterExecute = null;
            this.txtHonzituUkeireMount.PopupBeforeExecute = null;
            this.txtHonzituUkeireMount.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtHonzituUkeireMount.PopupSearchSendParams")));
            this.txtHonzituUkeireMount.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtHonzituUkeireMount.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtHonzituUkeireMount.popupWindowSetting")));
            this.txtHonzituUkeireMount.RangeSetting = rangeSettingDto6;
            this.txtHonzituUkeireMount.ReadOnly = true;
            this.txtHonzituUkeireMount.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtHonzituUkeireMount.RegistCheckMethod")));
            this.txtHonzituUkeireMount.Size = new System.Drawing.Size(64, 19);
            this.txtHonzituUkeireMount.TabIndex = 130;
            this.txtHonzituUkeireMount.TabStop = false;
            this.txtHonzituUkeireMount.Tag = "本日の受入数量が表示されます";
            this.txtHonzituUkeireMount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtHonzituUkeireMount.WordWrap = false;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(2, 89);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 20);
            this.label9.TabIndex = 120;
            this.label9.Text = "本日受入数量";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtHonzituUkeireNumber
            // 
            this.txtHonzituUkeireNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtHonzituUkeireNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHonzituUkeireNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtHonzituUkeireNumber.DisplayPopUp = null;
            this.txtHonzituUkeireNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtHonzituUkeireNumber.FocusOutCheckMethod")));
            this.txtHonzituUkeireNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.txtHonzituUkeireNumber.ForeColor = System.Drawing.Color.Black;
            this.txtHonzituUkeireNumber.FormatSetting = "カスタム";
            this.txtHonzituUkeireNumber.IsInputErrorOccured = false;
            this.txtHonzituUkeireNumber.Location = new System.Drawing.Point(2, 66);
            this.txtHonzituUkeireNumber.Name = "txtHonzituUkeireNumber";
            this.txtHonzituUkeireNumber.PopupAfterExecute = null;
            this.txtHonzituUkeireNumber.PopupBeforeExecute = null;
            this.txtHonzituUkeireNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtHonzituUkeireNumber.PopupSearchSendParams")));
            this.txtHonzituUkeireNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtHonzituUkeireNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtHonzituUkeireNumber.popupWindowSetting")));
            this.txtHonzituUkeireNumber.RangeSetting = rangeSettingDto7;
            this.txtHonzituUkeireNumber.ReadOnly = true;
            this.txtHonzituUkeireNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtHonzituUkeireNumber.RegistCheckMethod")));
            this.txtHonzituUkeireNumber.Size = new System.Drawing.Size(64, 19);
            this.txtHonzituUkeireNumber.TabIndex = 70;
            this.txtHonzituUkeireNumber.TabStop = false;
            this.txtHonzituUkeireNumber.Tag = "本日の受入台数が表示されます";
            this.txtHonzituUkeireNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtHonzituUkeireNumber.WordWrap = false;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label14.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(93, 46);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(86, 20);
            this.label14.TabIndex = 90;
            this.label14.Text = "本日出荷台数";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSyukkaTairyuNumber
            // 
            this.txtSyukkaTairyuNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtSyukkaTairyuNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSyukkaTairyuNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtSyukkaTairyuNumber.DisplayPopUp = null;
            this.txtSyukkaTairyuNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtSyukkaTairyuNumber.FocusOutCheckMethod")));
            this.txtSyukkaTairyuNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.txtSyukkaTairyuNumber.ForeColor = System.Drawing.Color.Black;
            this.txtSyukkaTairyuNumber.FormatSetting = "カスタム";
            this.txtSyukkaTairyuNumber.IsInputErrorOccured = false;
            this.txtSyukkaTairyuNumber.Location = new System.Drawing.Point(93, 23);
            this.txtSyukkaTairyuNumber.Name = "txtSyukkaTairyuNumber";
            this.txtSyukkaTairyuNumber.PopupAfterExecute = null;
            this.txtSyukkaTairyuNumber.PopupBeforeExecute = null;
            this.txtSyukkaTairyuNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtSyukkaTairyuNumber.PopupSearchSendParams")));
            this.txtSyukkaTairyuNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtSyukkaTairyuNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtSyukkaTairyuNumber.popupWindowSetting")));
            this.txtSyukkaTairyuNumber.RangeSetting = rangeSettingDto8;
            this.txtSyukkaTairyuNumber.ReadOnly = true;
            this.txtSyukkaTairyuNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtSyukkaTairyuNumber.RegistCheckMethod")));
            this.txtSyukkaTairyuNumber.Size = new System.Drawing.Size(64, 19);
            this.txtSyukkaTairyuNumber.TabIndex = 40;
            this.txtSyukkaTairyuNumber.TabStop = false;
            this.txtSyukkaTairyuNumber.Tag = "出荷滞留数が表示されます";
            this.txtSyukkaTairyuNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSyukkaTairyuNumber.WordWrap = false;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label13.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(93, 89);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(86, 20);
            this.label13.TabIndex = 150;
            this.label13.Text = "本日出荷数量";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtUkeireTairyuNumber
            // 
            this.txtUkeireTairyuNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtUkeireTairyuNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUkeireTairyuNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtUkeireTairyuNumber.DisplayPopUp = null;
            this.txtUkeireTairyuNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtUkeireTairyuNumber.FocusOutCheckMethod")));
            this.txtUkeireTairyuNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.txtUkeireTairyuNumber.ForeColor = System.Drawing.Color.Black;
            this.txtUkeireTairyuNumber.FormatSetting = "カスタム";
            this.txtUkeireTairyuNumber.IsInputErrorOccured = false;
            this.txtUkeireTairyuNumber.Location = new System.Drawing.Point(2, 23);
            this.txtUkeireTairyuNumber.Name = "txtUkeireTairyuNumber";
            this.txtUkeireTairyuNumber.PopupAfterExecute = null;
            this.txtUkeireTairyuNumber.PopupBeforeExecute = null;
            this.txtUkeireTairyuNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtUkeireTairyuNumber.PopupSearchSendParams")));
            this.txtUkeireTairyuNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtUkeireTairyuNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtUkeireTairyuNumber.popupWindowSetting")));
            this.txtUkeireTairyuNumber.RangeSetting = rangeSettingDto9;
            this.txtUkeireTairyuNumber.ReadOnly = true;
            this.txtUkeireTairyuNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtUkeireTairyuNumber.RegistCheckMethod")));
            this.txtUkeireTairyuNumber.Size = new System.Drawing.Size(64, 19);
            this.txtUkeireTairyuNumber.TabIndex = 10;
            this.txtUkeireTairyuNumber.TabStop = false;
            this.txtUkeireTairyuNumber.Tag = "受入滞留数が表示されます";
            this.txtUkeireTairyuNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtUkeireTairyuNumber.WordWrap = false;
            // 
            // lblSyukkaSuryoTani
            // 
            this.lblSyukkaSuryoTani.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.lblSyukkaSuryoTani.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSyukkaSuryoTani.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSyukkaSuryoTani.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.lblSyukkaSuryoTani.ForeColor = System.Drawing.Color.Black;
            this.lblSyukkaSuryoTani.Location = new System.Drawing.Point(156, 109);
            this.lblSyukkaSuryoTani.Name = "lblSyukkaSuryoTani";
            this.lblSyukkaSuryoTani.Size = new System.Drawing.Size(23, 19);
            this.lblSyukkaSuryoTani.TabIndex = 170;
            this.lblSyukkaSuryoTani.Text = "kg";
            this.lblSyukkaSuryoTani.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSyukkaTairyuTnai
            // 
            this.lblSyukkaTairyuTnai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.lblSyukkaTairyuTnai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSyukkaTairyuTnai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSyukkaTairyuTnai.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.lblSyukkaTairyuTnai.ForeColor = System.Drawing.Color.Black;
            this.lblSyukkaTairyuTnai.Location = new System.Drawing.Point(156, 23);
            this.lblSyukkaTairyuTnai.Name = "lblSyukkaTairyuTnai";
            this.lblSyukkaTairyuTnai.Size = new System.Drawing.Size(23, 19);
            this.lblSyukkaTairyuTnai.TabIndex = 50;
            this.lblSyukkaTairyuTnai.Text = "台";
            this.lblSyukkaTairyuTnai.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUkeireSuryoTani
            // 
            this.lblUkeireSuryoTani.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.lblUkeireSuryoTani.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUkeireSuryoTani.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblUkeireSuryoTani.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.lblUkeireSuryoTani.ForeColor = System.Drawing.Color.Black;
            this.lblUkeireSuryoTani.Location = new System.Drawing.Point(65, 109);
            this.lblUkeireSuryoTani.Name = "lblUkeireSuryoTani";
            this.lblUkeireSuryoTani.Size = new System.Drawing.Size(23, 19);
            this.lblUkeireSuryoTani.TabIndex = 140;
            this.lblUkeireSuryoTani.Text = "kg";
            this.lblUkeireSuryoTani.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHonzituUkeireTani
            // 
            this.lblHonzituUkeireTani.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.lblHonzituUkeireTani.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHonzituUkeireTani.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblHonzituUkeireTani.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.lblHonzituUkeireTani.ForeColor = System.Drawing.Color.Black;
            this.lblHonzituUkeireTani.Location = new System.Drawing.Point(65, 66);
            this.lblHonzituUkeireTani.Name = "lblHonzituUkeireTani";
            this.lblHonzituUkeireTani.Size = new System.Drawing.Size(23, 19);
            this.lblHonzituUkeireTani.TabIndex = 80;
            this.lblHonzituUkeireTani.Text = "台";
            this.lblHonzituUkeireTani.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHonzituSyukkaTani
            // 
            this.lblHonzituSyukkaTani.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.lblHonzituSyukkaTani.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHonzituSyukkaTani.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblHonzituSyukkaTani.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.lblHonzituSyukkaTani.ForeColor = System.Drawing.Color.Black;
            this.lblHonzituSyukkaTani.Location = new System.Drawing.Point(156, 66);
            this.lblHonzituSyukkaTani.Name = "lblHonzituSyukkaTani";
            this.lblHonzituSyukkaTani.Size = new System.Drawing.Size(23, 19);
            this.lblHonzituSyukkaTani.TabIndex = 110;
            this.lblHonzituSyukkaTani.Text = "台";
            this.lblHonzituSyukkaTani.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUkeireTairyuTani
            // 
            this.lblUkeireTairyuTani.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.lblUkeireTairyuTani.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUkeireTairyuTani.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblUkeireTairyuTani.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.lblUkeireTairyuTani.ForeColor = System.Drawing.Color.Black;
            this.lblUkeireTairyuTani.Location = new System.Drawing.Point(65, 23);
            this.lblUkeireTairyuTani.Name = "lblUkeireTairyuTani";
            this.lblUkeireTairyuTani.Size = new System.Drawing.Size(23, 19);
            this.lblUkeireTairyuTani.TabIndex = 20;
            this.lblUkeireTairyuTani.Text = "台";
            this.lblUkeireTairyuTani.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HIDUKE_FROM
            // 
            this.HIDUKE_FROM.BackColor = System.Drawing.SystemColors.Window;
            this.HIDUKE_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HIDUKE_FROM.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.HIDUKE_FROM.Checked = false;
            this.HIDUKE_FROM.CustomFormat = "yyyy/MM/dd(ddd)";
            this.HIDUKE_FROM.DateTimeNowYear = "";
            this.HIDUKE_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.HIDUKE_FROM.DisplayItemName = "開始日付";
            this.HIDUKE_FROM.DisplayPopUp = null;
            this.HIDUKE_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_FROM.FocusOutCheckMethod")));
            this.HIDUKE_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HIDUKE_FROM.ForeColor = System.Drawing.Color.Black;
            this.HIDUKE_FROM.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.HIDUKE_FROM.IsInputErrorOccured = false;
            this.HIDUKE_FROM.Location = new System.Drawing.Point(583, 3);
            this.HIDUKE_FROM.MaxLength = 10;
            this.HIDUKE_FROM.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.HIDUKE_FROM.Name = "HIDUKE_FROM";
            this.HIDUKE_FROM.NullValue = "";
            this.HIDUKE_FROM.PopupAfterExecute = null;
            this.HIDUKE_FROM.PopupBeforeExecute = null;
            this.HIDUKE_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIDUKE_FROM.PopupSearchSendParams")));
            this.HIDUKE_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIDUKE_FROM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HIDUKE_FROM.popupWindowSetting")));
            this.HIDUKE_FROM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_FROM.RegistCheckMethod")));
            this.HIDUKE_FROM.Size = new System.Drawing.Size(138, 20);
            this.HIDUKE_FROM.TabIndex = 320;
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
            this.HIDUKE_TO.DateTimeNowYear = "";
            this.HIDUKE_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.HIDUKE_TO.DisplayItemName = "終了日付";
            this.HIDUKE_TO.DisplayPopUp = null;
            this.HIDUKE_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_TO.FocusOutCheckMethod")));
            this.HIDUKE_TO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HIDUKE_TO.ForeColor = System.Drawing.Color.Black;
            this.HIDUKE_TO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.HIDUKE_TO.IsInputErrorOccured = false;
            this.HIDUKE_TO.Location = new System.Drawing.Point(741, 3);
            this.HIDUKE_TO.MaxLength = 10;
            this.HIDUKE_TO.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.HIDUKE_TO.Name = "HIDUKE_TO";
            this.HIDUKE_TO.NullValue = "";
            this.HIDUKE_TO.PopupAfterExecute = null;
            this.HIDUKE_TO.PopupBeforeExecute = null;
            this.HIDUKE_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIDUKE_TO.PopupSearchSendParams")));
            this.HIDUKE_TO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIDUKE_TO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HIDUKE_TO.popupWindowSetting")));
            this.HIDUKE_TO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_TO.RegistCheckMethod")));
            this.HIDUKE_TO.Size = new System.Drawing.Size(138, 20);
            this.HIDUKE_TO.TabIndex = 330;
            this.HIDUKE_TO.Tag = "日付を選択してください";
            this.HIDUKE_TO.Text = "2013/10/31(木)";
            this.HIDUKE_TO.Value = new System.DateTime(2013, 10, 31, 0, 0, 0, 0);
            this.HIDUKE_TO.Leave += new System.EventHandler(this.HIDUKE_TO_Leave);
            // 
            // HIDUKE_LABEL
            // 
            this.HIDUKE_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.HIDUKE_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HIDUKE_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HIDUKE_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HIDUKE_LABEL.ForeColor = System.Drawing.Color.White;
            this.HIDUKE_LABEL.Location = new System.Drawing.Point(468, 3);
            this.HIDUKE_LABEL.Name = "HIDUKE_LABEL";
            this.HIDUKE_LABEL.Size = new System.Drawing.Size(110, 20);
            this.HIDUKE_LABEL.TabIndex = 310;
            this.HIDUKE_LABEL.Text = "伝票日付※";
            this.HIDUKE_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(721, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 50;
            this.label2.Text = "～";
            // 
            // KeiryouIchiranForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 458);
            this.Controls.Add(this.customPanel3);
            this.Controls.Add(this.HIDUKE_FROM);
            this.Controls.Add(this.HIDUKE_TO);
            this.Controls.Add(this.HIDUKE_LABEL);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.customPanel4);
            this.Controls.Add(this.KEIRYOU_JOUKYOU_LABEL);
            this.Controls.Add(this.customPanel2);
            this.Controls.Add(this.customPanel1);
            this.Controls.Add(this.KEIRYOU_KBN_LABEL);
            this.Controls.Add(this.KIHON_KEIRYOU_LABEL);
            this.Name = "KeiryouIchiranForm";
            this.Text = "UIForm";
            this.Controls.SetChildIndex(this.KIHON_KEIRYOU_LABEL, 0);
            this.Controls.SetChildIndex(this.KEIRYOU_KBN_LABEL, 0);
            this.Controls.SetChildIndex(this.customPanel1, 0);
            this.Controls.SetChildIndex(this.customPanel2, 0);
            this.Controls.SetChildIndex(this.KEIRYOU_JOUKYOU_LABEL, 0);
            this.Controls.SetChildIndex(this.customPanel4, 0);
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.HIDUKE_LABEL, 0);
            this.Controls.SetChildIndex(this.HIDUKE_TO, 0);
            this.Controls.SetChildIndex(this.HIDUKE_FROM, 0);
            this.Controls.SetChildIndex(this.customPanel3, 0);
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.customPanel2.ResumeLayout(false);
            this.customPanel2.PerformLayout();
            this.customPanel4.ResumeLayout(false);
            this.customPanel4.PerformLayout();
            this.customPanel3.ResumeLayout(false);
            this.customPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal Label KIHON_KEIRYOU_LABEL;
        public r_framework.CustomControl.CustomRadioButton rb_KIHON_KEIRYOU_SHUKKA;
        public r_framework.CustomControl.CustomRadioButton rb_KIHON_KEIRYOU_UKEIRE;
        public r_framework.CustomControl.CustomRadioButton rb_KIHON_KEIRYOU_ALL;
        public r_framework.CustomControl.CustomNumericTextBox2 KIHON_KEIRYOU_CD;
        internal Label KEIRYOU_KBN_LABEL;
        private r_framework.CustomControl.CustomPanel customPanel1;
        private r_framework.CustomControl.CustomPanel customPanel2;
        internal Label KEIRYOU_JOUKYOU_LABEL;
        public r_framework.CustomControl.CustomPanel customPanel4;
        private r_framework.CustomControl.CustomPanel customPanel3;
        private Label label15;
        public r_framework.CustomControl.CustomNumericTextBox2 txtHonzituSyukkaMount;
        private Label label7;
        public r_framework.CustomControl.CustomNumericTextBox2 txtHonzituSyukkaNumber;
        private Label label8;
        public r_framework.CustomControl.CustomNumericTextBox2 txtHonzituUkeireMount;
        private Label label9;
        public r_framework.CustomControl.CustomNumericTextBox2 txtHonzituUkeireNumber;
        private Label label14;
        public r_framework.CustomControl.CustomNumericTextBox2 txtSyukkaTairyuNumber;
        private Label label13;
        public r_framework.CustomControl.CustomNumericTextBox2 txtUkeireTairyuNumber;
        private Label lblUkeireTairyuTani;
        private Label lblSyukkaSuryoTani;
        private Label lblSyukkaTairyuTnai;
        private Label lblUkeireSuryoTani;
        private Label lblHonzituUkeireTani;
        private Label lblHonzituSyukkaTani;
        public r_framework.CustomControl.CustomDateTimePicker HIDUKE_FROM;
        public r_framework.CustomControl.CustomDateTimePicker HIDUKE_TO;
        internal Label HIDUKE_LABEL;
        private Label label2;
        public r_framework.CustomControl.CustomRadioButton rb_KEIRYOU_JOUKYOU_TAIRYUU;
        public r_framework.CustomControl.CustomNumericTextBox2 KEIRYOU_JOUKYOU_CD;
        public r_framework.CustomControl.CustomRadioButton rb_KEIRYOU_JOUKYOU_KANRYOU;
        public r_framework.CustomControl.CustomRadioButton rb_KEIRYOU_KBN_KEIJOU;
        public r_framework.CustomControl.CustomRadioButton rb_KEIRYOU_KBN_ALL;
        public r_framework.CustomControl.CustomRadioButton rb_KEIRYOU_KBN_KARI;
        public r_framework.CustomControl.CustomNumericTextBox2 KEIRYOU_KBN_CD;
        public r_framework.CustomControl.CustomRadioButton rb_KEIRYOU_KBN_TSUUJOU;
    }
}