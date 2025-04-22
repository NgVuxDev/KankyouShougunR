// $Id: UIForm.Designer.cs 55371 2015-07-10 11:07:15Z t-thanhson@e-mall.co.jp $
using System.Windows.Forms;
using System;

namespace Shougun.Core.ExternalConnection.GenbamemoIchiran
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
            this.pnlSearchString = new r_framework.CustomControl.CustomPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.radbtnTourokushaEigyou = new r_framework.CustomControl.CustomRadioButton();
            this.radbtnTourokushaUnten = new r_framework.CustomControl.CustomRadioButton();
            this.radbtnTourokushaNyuuryoku = new r_framework.CustomControl.CustomRadioButton();
            this.radbtnTourokushaAll = new r_framework.CustomControl.CustomRadioButton();
            this.txtNum_ShokaiTourokushaSentaku = new r_framework.CustomControl.CustomNumericTextBox2();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radbtnHasseimotoShiteiHukusuu = new r_framework.CustomControl.CustomRadioButton();
            this.radbtnHasseimotoShitei = new r_framework.CustomControl.CustomRadioButton();
            this.radbtnHasseimotoAll = new r_framework.CustomControl.CustomRadioButton();
            this.txtNum_HasseimotoSentaku = new r_framework.CustomControl.CustomNumericTextBox2();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radbtnHyoujiKubunHihyouji = new r_framework.CustomControl.CustomRadioButton();
            this.radbtnHyoujiKubunHyouji = new r_framework.CustomControl.CustomRadioButton();
            this.radbtnHyoujiKubunAll = new r_framework.CustomControl.CustomRadioButton();
            this.txtNum_HyoujiKubunSentaku = new r_framework.CustomControl.CustomNumericTextBox2();
            this.TourokuDate_To = new r_framework.CustomControl.CustomDateTimePicker();
            this.label19 = new System.Windows.Forms.Label();
            this.TourokuDate_From = new r_framework.CustomControl.CustomDateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.SHAIN_NAME = new r_framework.CustomControl.CustomTextBox();
            this.SHAIN_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.HASSEIMOTO_MEISAI_NUMBER = new r_framework.CustomControl.CustomNumericTextBox2();
            this.HASSEIMOTO_MEISAI_NUMBER_LABEL = new System.Windows.Forms.Label();
            this.HASSEIMOTO_NUMBER = new r_framework.CustomControl.CustomNumericTextBox2();
            this.HASSEIMOTO_NUMBER_LABEL = new System.Windows.Forms.Label();
            this.hasseimoto_label5 = new System.Windows.Forms.Label();
            this.chkTeikiHaisha = new System.Windows.Forms.CheckBox();
            this.hasseimoto_label4 = new System.Windows.Forms.Label();
            this.chkMochikomiUketsuke = new System.Windows.Forms.CheckBox();
            this.hasseimoto_label3 = new System.Windows.Forms.Label();
            this.chkShukkaUketsuke = new System.Windows.Forms.CheckBox();
            this.hasseimoto_label2 = new System.Windows.Forms.Label();
            this.chkShuushuuUketsuke = new System.Windows.Forms.CheckBox();
            this.hasseimoto_label1 = new System.Windows.Forms.Label();
            this.chkHasseimotoNashi = new System.Windows.Forms.CheckBox();
            this.GENBAMEMO_BUNRUI_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.GENBAMEMO_BUNRUI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.testGYOUSHA_CD = new r_framework.CustomControl.CustomTextBox();
            this.testGENBA_CD = new r_framework.CustomControl.CustomTextBox();
            this.GENBA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.GENBA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.lable5 = new System.Windows.Forms.Label();
            this.GYOUSHA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.TORIHIKISAKI_NAME = new r_framework.CustomControl.CustomTextBox();
            this.TORIHIKISAKI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.lable3 = new System.Windows.Forms.Label();
            this.lable1 = new System.Windows.Forms.Label();
            this.pnlSearchString.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.Enabled = false;
            this.searchString.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.FocusOutCheckMethod")));
            this.searchString.Location = new System.Drawing.Point(0, 0);
            this.searchString.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("searchString.PopupSearchSendParams")));
            this.searchString.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("searchString.popupWindowSetting")));
            this.searchString.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.RegistCheckMethod")));
            this.searchString.Size = new System.Drawing.Size(687, 90);
            this.searchString.TabStop = false;
            this.searchString.Tag = "検索条件設定画面で設定した条件を表示します";
            this.searchString.Visible = false;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Location = new System.Drawing.Point(0, 408);
            this.bt_ptn1.TabIndex = 61;
            this.bt_ptn1.TabStop = false;
            this.bt_ptn1.Text = "パターン設定なし";
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Location = new System.Drawing.Point(200, 408);
            this.bt_ptn2.TabIndex = 62;
            this.bt_ptn2.TabStop = false;
            this.bt_ptn2.Text = "パターン設定なし";
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Location = new System.Drawing.Point(400, 408);
            this.bt_ptn3.TabIndex = 63;
            this.bt_ptn3.TabStop = false;
            this.bt_ptn3.Text = "パターン設定なし";
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Location = new System.Drawing.Point(600, 408);
            this.bt_ptn4.TabIndex = 64;
            this.bt_ptn4.TabStop = false;
            this.bt_ptn4.Text = "パターン設定なし";
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Location = new System.Drawing.Point(800, 408);
            this.bt_ptn5.TabIndex = 65;
            this.bt_ptn5.TabStop = false;
            this.bt_ptn5.Text = "パターン設定なし";
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.AutoSize = true;
            this.customSortHeader1.Location = new System.Drawing.Point(3, 322);
            this.customSortHeader1.TabIndex = 190;
            // 
            // customSearchHeader1
            // 
            this.customSearchHeader1.Location = new System.Drawing.Point(4, 158);
            this.customSearchHeader1.TabIndex = 180;
            // 
            // pnlSearchString
            // 
            this.pnlSearchString.Controls.Add(this.panel3);
            this.pnlSearchString.Controls.Add(this.panel2);
            this.pnlSearchString.Controls.Add(this.panel1);
            this.pnlSearchString.Controls.Add(this.TourokuDate_To);
            this.pnlSearchString.Controls.Add(this.label19);
            this.pnlSearchString.Controls.Add(this.TourokuDate_From);
            this.pnlSearchString.Controls.Add(this.label5);
            this.pnlSearchString.Controls.Add(this.SHAIN_NAME);
            this.pnlSearchString.Controls.Add(this.SHAIN_CD);
            this.pnlSearchString.Controls.Add(this.label4);
            this.pnlSearchString.Controls.Add(this.HASSEIMOTO_MEISAI_NUMBER);
            this.pnlSearchString.Controls.Add(this.HASSEIMOTO_MEISAI_NUMBER_LABEL);
            this.pnlSearchString.Controls.Add(this.HASSEIMOTO_NUMBER);
            this.pnlSearchString.Controls.Add(this.HASSEIMOTO_NUMBER_LABEL);
            this.pnlSearchString.Controls.Add(this.hasseimoto_label5);
            this.pnlSearchString.Controls.Add(this.chkTeikiHaisha);
            this.pnlSearchString.Controls.Add(this.hasseimoto_label4);
            this.pnlSearchString.Controls.Add(this.chkMochikomiUketsuke);
            this.pnlSearchString.Controls.Add(this.hasseimoto_label3);
            this.pnlSearchString.Controls.Add(this.chkShukkaUketsuke);
            this.pnlSearchString.Controls.Add(this.hasseimoto_label2);
            this.pnlSearchString.Controls.Add(this.chkShuushuuUketsuke);
            this.pnlSearchString.Controls.Add(this.hasseimoto_label1);
            this.pnlSearchString.Controls.Add(this.chkHasseimotoNashi);
            this.pnlSearchString.Controls.Add(this.GENBAMEMO_BUNRUI_NAME_RYAKU);
            this.pnlSearchString.Controls.Add(this.GENBAMEMO_BUNRUI_CD);
            this.pnlSearchString.Controls.Add(this.label3);
            this.pnlSearchString.Controls.Add(this.label2);
            this.pnlSearchString.Controls.Add(this.label1);
            this.pnlSearchString.Controls.Add(this.testGYOUSHA_CD);
            this.pnlSearchString.Controls.Add(this.testGENBA_CD);
            this.pnlSearchString.Controls.Add(this.GENBA_NAME);
            this.pnlSearchString.Controls.Add(this.GENBA_CD);
            this.pnlSearchString.Controls.Add(this.lable5);
            this.pnlSearchString.Controls.Add(this.GYOUSHA_NAME);
            this.pnlSearchString.Controls.Add(this.GYOUSHA_CD);
            this.pnlSearchString.Controls.Add(this.TORIHIKISAKI_NAME);
            this.pnlSearchString.Controls.Add(this.TORIHIKISAKI_CD);
            this.pnlSearchString.Controls.Add(this.lable3);
            this.pnlSearchString.Controls.Add(this.lable1);
            this.pnlSearchString.Location = new System.Drawing.Point(0, 0);
            this.pnlSearchString.Name = "pnlSearchString";
            this.pnlSearchString.Size = new System.Drawing.Size(1126, 152);
            this.pnlSearchString.TabIndex = 10;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.radbtnTourokushaEigyou);
            this.panel3.Controls.Add(this.radbtnTourokushaUnten);
            this.panel3.Controls.Add(this.radbtnTourokushaNyuuryoku);
            this.panel3.Controls.Add(this.radbtnTourokushaAll);
            this.panel3.Controls.Add(this.txtNum_ShokaiTourokushaSentaku);
            this.panel3.Location = new System.Drawing.Point(601, 70);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(483, 25);
            this.panel3.TabIndex = 9;
            this.panel3.TabStop = true;
            // 
            // radbtnTourokushaEigyou
            // 
            this.radbtnTourokushaEigyou.AutoSize = true;
            this.radbtnTourokushaEigyou.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnTourokushaEigyou.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnTourokushaEigyou.FocusOutCheckMethod")));
            this.radbtnTourokushaEigyou.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtnTourokushaEigyou.LinkedTextBox = "txtNum_ShokaiTourokushaSentaku";
            this.radbtnTourokushaEigyou.Location = new System.Drawing.Point(344, 5);
            this.radbtnTourokushaEigyou.Name = "radbtnTourokushaEigyou";
            this.radbtnTourokushaEigyou.PopupAfterExecute = null;
            this.radbtnTourokushaEigyou.PopupBeforeExecute = null;
            this.radbtnTourokushaEigyou.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnTourokushaEigyou.PopupSearchSendParams")));
            this.radbtnTourokushaEigyou.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnTourokushaEigyou.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnTourokushaEigyou.popupWindowSetting")));
            this.radbtnTourokushaEigyou.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnTourokushaEigyou.RegistCheckMethod")));
            this.radbtnTourokushaEigyou.Size = new System.Drawing.Size(116, 17);
            this.radbtnTourokushaEigyou.TabIndex = 820;
            this.radbtnTourokushaEigyou.Tag = "";
            this.radbtnTourokushaEigyou.Text = "4. 営業担当者";
            this.radbtnTourokushaEigyou.UseVisualStyleBackColor = true;
            this.radbtnTourokushaEigyou.Value = "4";
            // 
            // radbtnTourokushaUnten
            // 
            this.radbtnTourokushaUnten.AutoSize = true;
            this.radbtnTourokushaUnten.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnTourokushaUnten.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnTourokushaUnten.FocusOutCheckMethod")));
            this.radbtnTourokushaUnten.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtnTourokushaUnten.LinkedTextBox = "txtNum_ShokaiTourokushaSentaku";
            this.radbtnTourokushaUnten.Location = new System.Drawing.Point(254, 5);
            this.radbtnTourokushaUnten.Name = "radbtnTourokushaUnten";
            this.radbtnTourokushaUnten.PopupAfterExecute = null;
            this.radbtnTourokushaUnten.PopupBeforeExecute = null;
            this.radbtnTourokushaUnten.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnTourokushaUnten.PopupSearchSendParams")));
            this.radbtnTourokushaUnten.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnTourokushaUnten.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnTourokushaUnten.popupWindowSetting")));
            this.radbtnTourokushaUnten.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnTourokushaUnten.RegistCheckMethod")));
            this.radbtnTourokushaUnten.Size = new System.Drawing.Size(88, 17);
            this.radbtnTourokushaUnten.TabIndex = 819;
            this.radbtnTourokushaUnten.Tag = "";
            this.radbtnTourokushaUnten.Text = "3. 運転者";
            this.radbtnTourokushaUnten.UseVisualStyleBackColor = true;
            this.radbtnTourokushaUnten.Value = "3";
            // 
            // radbtnTourokushaNyuuryoku
            // 
            this.radbtnTourokushaNyuuryoku.AutoSize = true;
            this.radbtnTourokushaNyuuryoku.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnTourokushaNyuuryoku.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnTourokushaNyuuryoku.FocusOutCheckMethod")));
            this.radbtnTourokushaNyuuryoku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtnTourokushaNyuuryoku.LinkedTextBox = "txtNum_ShokaiTourokushaSentaku";
            this.radbtnTourokushaNyuuryoku.Location = new System.Drawing.Point(136, 5);
            this.radbtnTourokushaNyuuryoku.Name = "radbtnTourokushaNyuuryoku";
            this.radbtnTourokushaNyuuryoku.PopupAfterExecute = null;
            this.radbtnTourokushaNyuuryoku.PopupBeforeExecute = null;
            this.radbtnTourokushaNyuuryoku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnTourokushaNyuuryoku.PopupSearchSendParams")));
            this.radbtnTourokushaNyuuryoku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnTourokushaNyuuryoku.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnTourokushaNyuuryoku.popupWindowSetting")));
            this.radbtnTourokushaNyuuryoku.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnTourokushaNyuuryoku.RegistCheckMethod")));
            this.radbtnTourokushaNyuuryoku.Size = new System.Drawing.Size(116, 17);
            this.radbtnTourokushaNyuuryoku.TabIndex = 818;
            this.radbtnTourokushaNyuuryoku.Tag = "";
            this.radbtnTourokushaNyuuryoku.Text = "2. 入力担当者";
            this.radbtnTourokushaNyuuryoku.UseVisualStyleBackColor = true;
            this.radbtnTourokushaNyuuryoku.Value = "2";
            // 
            // radbtnTourokushaAll
            // 
            this.radbtnTourokushaAll.AutoSize = true;
            this.radbtnTourokushaAll.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnTourokushaAll.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnTourokushaAll.FocusOutCheckMethod")));
            this.radbtnTourokushaAll.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtnTourokushaAll.LinkedTextBox = "txtNum_ShokaiTourokushaSentaku";
            this.radbtnTourokushaAll.Location = new System.Drawing.Point(56, 5);
            this.radbtnTourokushaAll.Name = "radbtnTourokushaAll";
            this.radbtnTourokushaAll.PopupAfterExecute = null;
            this.radbtnTourokushaAll.PopupBeforeExecute = null;
            this.radbtnTourokushaAll.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnTourokushaAll.PopupSearchSendParams")));
            this.radbtnTourokushaAll.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnTourokushaAll.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnTourokushaAll.popupWindowSetting")));
            this.radbtnTourokushaAll.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnTourokushaAll.RegistCheckMethod")));
            this.radbtnTourokushaAll.Size = new System.Drawing.Size(74, 17);
            this.radbtnTourokushaAll.TabIndex = 817;
            this.radbtnTourokushaAll.Tag = "";
            this.radbtnTourokushaAll.Text = "1. 全て";
            this.radbtnTourokushaAll.UseVisualStyleBackColor = true;
            this.radbtnTourokushaAll.Value = "1";
            // 
            // txtNum_ShokaiTourokushaSentaku
            // 
            this.txtNum_ShokaiTourokushaSentaku.BackColor = System.Drawing.SystemColors.Window;
            this.txtNum_ShokaiTourokushaSentaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNum_ShokaiTourokushaSentaku.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtNum_ShokaiTourokushaSentaku.DisplayPopUp = null;
            this.txtNum_ShokaiTourokushaSentaku.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_ShokaiTourokushaSentaku.FocusOutCheckMethod")));
            this.txtNum_ShokaiTourokushaSentaku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtNum_ShokaiTourokushaSentaku.ForeColor = System.Drawing.Color.Black;
            this.txtNum_ShokaiTourokushaSentaku.IsInputErrorOccured = false;
            this.txtNum_ShokaiTourokushaSentaku.LinkedRadioButtonArray = new string[] {
        "radbtnTourokushaAll",
        "radbtnTourokushaNyuuryoku",
        "radbtnTourokushaUnten",
        "radbtnTourokushaEigyou"};
            this.txtNum_ShokaiTourokushaSentaku.Location = new System.Drawing.Point(0, 2);
            this.txtNum_ShokaiTourokushaSentaku.Name = "txtNum_ShokaiTourokushaSentaku";
            this.txtNum_ShokaiTourokushaSentaku.PopupAfterExecute = null;
            this.txtNum_ShokaiTourokushaSentaku.PopupBeforeExecute = null;
            this.txtNum_ShokaiTourokushaSentaku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtNum_ShokaiTourokushaSentaku.PopupSearchSendParams")));
            this.txtNum_ShokaiTourokushaSentaku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtNum_ShokaiTourokushaSentaku.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtNum_ShokaiTourokushaSentaku.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            4,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtNum_ShokaiTourokushaSentaku.RangeSetting = rangeSettingDto1;
            this.txtNum_ShokaiTourokushaSentaku.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_ShokaiTourokushaSentaku.RegistCheckMethod")));
            this.txtNum_ShokaiTourokushaSentaku.Size = new System.Drawing.Size(50, 20);
            this.txtNum_ShokaiTourokushaSentaku.TabIndex = 9;
            this.txtNum_ShokaiTourokushaSentaku.Tag = "【1～4】のいずれかで入力してください";
            this.txtNum_ShokaiTourokushaSentaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNum_ShokaiTourokushaSentaku.WordWrap = false;
            this.txtNum_ShokaiTourokushaSentaku.TextChanged += new System.EventHandler(this.SHOKAI_TOUROKUSHA_CD_TextChanged);
            this.txtNum_ShokaiTourokushaSentaku.Leave += new System.EventHandler(this.txtNum_ShokaiTourokusha_Leave);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radbtnHasseimotoShiteiHukusuu);
            this.panel2.Controls.Add(this.radbtnHasseimotoShitei);
            this.panel2.Controls.Add(this.radbtnHasseimotoAll);
            this.panel2.Controls.Add(this.txtNum_HasseimotoSentaku);
            this.panel2.Location = new System.Drawing.Point(599, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(369, 25);
            this.panel2.TabIndex = 6;
            this.panel2.TabStop = true;
            // 
            // radbtnHasseimotoShiteiHukusuu
            // 
            this.radbtnHasseimotoShiteiHukusuu.AutoSize = true;
            this.radbtnHasseimotoShiteiHukusuu.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnHasseimotoShiteiHukusuu.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnHasseimotoShiteiHukusuu.FocusOutCheckMethod")));
            this.radbtnHasseimotoShiteiHukusuu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtnHasseimotoShiteiHukusuu.LinkedTextBox = "txtNum_HasseimotoSentaku";
            this.radbtnHasseimotoShiteiHukusuu.Location = new System.Drawing.Point(216, 4);
            this.radbtnHasseimotoShiteiHukusuu.Name = "radbtnHasseimotoShiteiHukusuu";
            this.radbtnHasseimotoShiteiHukusuu.PopupAfterExecute = null;
            this.radbtnHasseimotoShiteiHukusuu.PopupBeforeExecute = null;
            this.radbtnHasseimotoShiteiHukusuu.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnHasseimotoShiteiHukusuu.PopupSearchSendParams")));
            this.radbtnHasseimotoShiteiHukusuu.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnHasseimotoShiteiHukusuu.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnHasseimotoShiteiHukusuu.popupWindowSetting")));
            this.radbtnHasseimotoShiteiHukusuu.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnHasseimotoShiteiHukusuu.RegistCheckMethod")));
            this.radbtnHasseimotoShiteiHukusuu.Size = new System.Drawing.Size(130, 17);
            this.radbtnHasseimotoShiteiHukusuu.TabIndex = 799;
            this.radbtnHasseimotoShiteiHukusuu.Tag = "";
            this.radbtnHasseimotoShiteiHukusuu.Text = "3. 指定（複数）";
            this.radbtnHasseimotoShiteiHukusuu.UseVisualStyleBackColor = true;
            this.radbtnHasseimotoShiteiHukusuu.Value = "3";
            // 
            // radbtnHasseimotoShitei
            // 
            this.radbtnHasseimotoShitei.AutoSize = true;
            this.radbtnHasseimotoShitei.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnHasseimotoShitei.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnHasseimotoShitei.FocusOutCheckMethod")));
            this.radbtnHasseimotoShitei.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtnHasseimotoShitei.LinkedTextBox = "txtNum_HasseimotoSentaku";
            this.radbtnHasseimotoShitei.Location = new System.Drawing.Point(136, 4);
            this.radbtnHasseimotoShitei.Name = "radbtnHasseimotoShitei";
            this.radbtnHasseimotoShitei.PopupAfterExecute = null;
            this.radbtnHasseimotoShitei.PopupBeforeExecute = null;
            this.radbtnHasseimotoShitei.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnHasseimotoShitei.PopupSearchSendParams")));
            this.radbtnHasseimotoShitei.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnHasseimotoShitei.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnHasseimotoShitei.popupWindowSetting")));
            this.radbtnHasseimotoShitei.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnHasseimotoShitei.RegistCheckMethod")));
            this.radbtnHasseimotoShitei.Size = new System.Drawing.Size(74, 17);
            this.radbtnHasseimotoShitei.TabIndex = 798;
            this.radbtnHasseimotoShitei.Tag = "";
            this.radbtnHasseimotoShitei.Text = "2. 指定";
            this.radbtnHasseimotoShitei.UseVisualStyleBackColor = true;
            this.radbtnHasseimotoShitei.Value = "2";
            // 
            // radbtnHasseimotoAll
            // 
            this.radbtnHasseimotoAll.AutoSize = true;
            this.radbtnHasseimotoAll.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnHasseimotoAll.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnHasseimotoAll.FocusOutCheckMethod")));
            this.radbtnHasseimotoAll.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtnHasseimotoAll.LinkedTextBox = "txtNum_HasseimotoSentaku";
            this.radbtnHasseimotoAll.Location = new System.Drawing.Point(56, 4);
            this.radbtnHasseimotoAll.Name = "radbtnHasseimotoAll";
            this.radbtnHasseimotoAll.PopupAfterExecute = null;
            this.radbtnHasseimotoAll.PopupBeforeExecute = null;
            this.radbtnHasseimotoAll.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnHasseimotoAll.PopupSearchSendParams")));
            this.radbtnHasseimotoAll.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnHasseimotoAll.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnHasseimotoAll.popupWindowSetting")));
            this.radbtnHasseimotoAll.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnHasseimotoAll.RegistCheckMethod")));
            this.radbtnHasseimotoAll.Size = new System.Drawing.Size(74, 17);
            this.radbtnHasseimotoAll.TabIndex = 797;
            this.radbtnHasseimotoAll.Tag = "";
            this.radbtnHasseimotoAll.Text = "1. 全て";
            this.radbtnHasseimotoAll.UseVisualStyleBackColor = true;
            this.radbtnHasseimotoAll.Value = "1";
            // 
            // txtNum_HasseimotoSentaku
            // 
            this.txtNum_HasseimotoSentaku.BackColor = System.Drawing.SystemColors.Window;
            this.txtNum_HasseimotoSentaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNum_HasseimotoSentaku.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtNum_HasseimotoSentaku.DisplayPopUp = null;
            this.txtNum_HasseimotoSentaku.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_HasseimotoSentaku.FocusOutCheckMethod")));
            this.txtNum_HasseimotoSentaku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtNum_HasseimotoSentaku.ForeColor = System.Drawing.Color.Black;
            this.txtNum_HasseimotoSentaku.IsInputErrorOccured = false;
            this.txtNum_HasseimotoSentaku.LinkedRadioButtonArray = new string[] {
        "radbtnHasseimotoAll",
        "radbtnHasseimotoShitei",
        "radbtnHasseimotoShiteiHukusuu"};
            this.txtNum_HasseimotoSentaku.Location = new System.Drawing.Point(0, 2);
            this.txtNum_HasseimotoSentaku.Name = "txtNum_HasseimotoSentaku";
            this.txtNum_HasseimotoSentaku.PopupAfterExecute = null;
            this.txtNum_HasseimotoSentaku.PopupBeforeExecute = null;
            this.txtNum_HasseimotoSentaku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtNum_HasseimotoSentaku.PopupSearchSendParams")));
            this.txtNum_HasseimotoSentaku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtNum_HasseimotoSentaku.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtNum_HasseimotoSentaku.popupWindowSetting")));
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
            this.txtNum_HasseimotoSentaku.RangeSetting = rangeSettingDto2;
            this.txtNum_HasseimotoSentaku.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_HasseimotoSentaku.RegistCheckMethod")));
            this.txtNum_HasseimotoSentaku.Size = new System.Drawing.Size(50, 20);
            this.txtNum_HasseimotoSentaku.TabIndex = 6;
            this.txtNum_HasseimotoSentaku.Tag = "【1～3】のいずれかで入力してください";
            this.txtNum_HasseimotoSentaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNum_HasseimotoSentaku.WordWrap = false;
            this.txtNum_HasseimotoSentaku.TextChanged += new System.EventHandler(this.HASSEIMOTO_CD_TextChanged);
            this.txtNum_HasseimotoSentaku.Leave += new System.EventHandler(this.txtNum_Hasseimoto_Leave);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radbtnHyoujiKubunHihyouji);
            this.panel1.Controls.Add(this.radbtnHyoujiKubunHyouji);
            this.panel1.Controls.Add(this.radbtnHyoujiKubunAll);
            this.panel1.Controls.Add(this.txtNum_HyoujiKubunSentaku);
            this.panel1.Location = new System.Drawing.Point(114, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(369, 25);
            this.panel1.TabIndex = 1;
            this.panel1.TabStop = true;
            // 
            // radbtnHyoujiKubunHihyouji
            // 
            this.radbtnHyoujiKubunHihyouji.AutoSize = true;
            this.radbtnHyoujiKubunHihyouji.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnHyoujiKubunHihyouji.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnHyoujiKubunHihyouji.FocusOutCheckMethod")));
            this.radbtnHyoujiKubunHihyouji.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtnHyoujiKubunHihyouji.LinkedTextBox = "txtNum_HyoujiKubunSentaku";
            this.radbtnHyoujiKubunHihyouji.Location = new System.Drawing.Point(220, 4);
            this.radbtnHyoujiKubunHihyouji.Name = "radbtnHyoujiKubunHihyouji";
            this.radbtnHyoujiKubunHihyouji.PopupAfterExecute = null;
            this.radbtnHyoujiKubunHihyouji.PopupBeforeExecute = null;
            this.radbtnHyoujiKubunHihyouji.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnHyoujiKubunHihyouji.PopupSearchSendParams")));
            this.radbtnHyoujiKubunHihyouji.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnHyoujiKubunHihyouji.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnHyoujiKubunHihyouji.popupWindowSetting")));
            this.radbtnHyoujiKubunHihyouji.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnHyoujiKubunHihyouji.RegistCheckMethod")));
            this.radbtnHyoujiKubunHihyouji.Size = new System.Drawing.Size(88, 17);
            this.radbtnHyoujiKubunHihyouji.TabIndex = 53;
            this.radbtnHyoujiKubunHihyouji.Tag = "";
            this.radbtnHyoujiKubunHihyouji.Text = "3. 非表示";
            this.radbtnHyoujiKubunHihyouji.UseVisualStyleBackColor = true;
            this.radbtnHyoujiKubunHihyouji.Value = "3";
            // 
            // radbtnHyoujiKubunHyouji
            // 
            this.radbtnHyoujiKubunHyouji.AutoSize = true;
            this.radbtnHyoujiKubunHyouji.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnHyoujiKubunHyouji.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnHyoujiKubunHyouji.FocusOutCheckMethod")));
            this.radbtnHyoujiKubunHyouji.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtnHyoujiKubunHyouji.LinkedTextBox = "txtNum_HyoujiKubunSentaku";
            this.radbtnHyoujiKubunHyouji.Location = new System.Drawing.Point(140, 4);
            this.radbtnHyoujiKubunHyouji.Name = "radbtnHyoujiKubunHyouji";
            this.radbtnHyoujiKubunHyouji.PopupAfterExecute = null;
            this.radbtnHyoujiKubunHyouji.PopupBeforeExecute = null;
            this.radbtnHyoujiKubunHyouji.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnHyoujiKubunHyouji.PopupSearchSendParams")));
            this.radbtnHyoujiKubunHyouji.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnHyoujiKubunHyouji.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnHyoujiKubunHyouji.popupWindowSetting")));
            this.radbtnHyoujiKubunHyouji.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnHyoujiKubunHyouji.RegistCheckMethod")));
            this.radbtnHyoujiKubunHyouji.Size = new System.Drawing.Size(74, 17);
            this.radbtnHyoujiKubunHyouji.TabIndex = 52;
            this.radbtnHyoujiKubunHyouji.Tag = "";
            this.radbtnHyoujiKubunHyouji.Text = "2. 表示";
            this.radbtnHyoujiKubunHyouji.UseVisualStyleBackColor = true;
            this.radbtnHyoujiKubunHyouji.Value = "2";
            // 
            // radbtnHyoujiKubunAll
            // 
            this.radbtnHyoujiKubunAll.AutoSize = true;
            this.radbtnHyoujiKubunAll.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnHyoujiKubunAll.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnHyoujiKubunAll.FocusOutCheckMethod")));
            this.radbtnHyoujiKubunAll.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtnHyoujiKubunAll.LinkedTextBox = "txtNum_HyoujiKubunSentaku";
            this.radbtnHyoujiKubunAll.Location = new System.Drawing.Point(60, 4);
            this.radbtnHyoujiKubunAll.Name = "radbtnHyoujiKubunAll";
            this.radbtnHyoujiKubunAll.PopupAfterExecute = null;
            this.radbtnHyoujiKubunAll.PopupBeforeExecute = null;
            this.radbtnHyoujiKubunAll.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnHyoujiKubunAll.PopupSearchSendParams")));
            this.radbtnHyoujiKubunAll.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnHyoujiKubunAll.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnHyoujiKubunAll.popupWindowSetting")));
            this.radbtnHyoujiKubunAll.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnHyoujiKubunAll.RegistCheckMethod")));
            this.radbtnHyoujiKubunAll.Size = new System.Drawing.Size(74, 17);
            this.radbtnHyoujiKubunAll.TabIndex = 51;
            this.radbtnHyoujiKubunAll.Tag = "";
            this.radbtnHyoujiKubunAll.Text = "1. 全て";
            this.radbtnHyoujiKubunAll.UseVisualStyleBackColor = true;
            this.radbtnHyoujiKubunAll.Value = "1";
            // 
            // txtNum_HyoujiKubunSentaku
            // 
            this.txtNum_HyoujiKubunSentaku.BackColor = System.Drawing.SystemColors.Window;
            this.txtNum_HyoujiKubunSentaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNum_HyoujiKubunSentaku.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtNum_HyoujiKubunSentaku.DisplayPopUp = null;
            this.txtNum_HyoujiKubunSentaku.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_HyoujiKubunSentaku.FocusOutCheckMethod")));
            this.txtNum_HyoujiKubunSentaku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtNum_HyoujiKubunSentaku.ForeColor = System.Drawing.Color.Black;
            this.txtNum_HyoujiKubunSentaku.IsInputErrorOccured = false;
            this.txtNum_HyoujiKubunSentaku.LinkedRadioButtonArray = new string[] {
        "radbtnHyoujiKubunAll",
        "radbtnHyoujiKubunHyouji",
        "radbtnHyoujiKubunHihyouji"};
            this.txtNum_HyoujiKubunSentaku.Location = new System.Drawing.Point(4, 2);
            this.txtNum_HyoujiKubunSentaku.Name = "txtNum_HyoujiKubunSentaku";
            this.txtNum_HyoujiKubunSentaku.PopupAfterExecute = null;
            this.txtNum_HyoujiKubunSentaku.PopupBeforeExecute = null;
            this.txtNum_HyoujiKubunSentaku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtNum_HyoujiKubunSentaku.PopupSearchSendParams")));
            this.txtNum_HyoujiKubunSentaku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtNum_HyoujiKubunSentaku.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtNum_HyoujiKubunSentaku.popupWindowSetting")));
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
            this.txtNum_HyoujiKubunSentaku.RangeSetting = rangeSettingDto3;
            this.txtNum_HyoujiKubunSentaku.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_HyoujiKubunSentaku.RegistCheckMethod")));
            this.txtNum_HyoujiKubunSentaku.Size = new System.Drawing.Size(50, 20);
            this.txtNum_HyoujiKubunSentaku.TabIndex = 1;
            this.txtNum_HyoujiKubunSentaku.Tag = "【1～3】のいずれかで入力してください";
            this.txtNum_HyoujiKubunSentaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNum_HyoujiKubunSentaku.WordWrap = false;
            this.txtNum_HyoujiKubunSentaku.Leave += new System.EventHandler(this.txtNum_Hyoujikubun_Leave);
            // 
            // TourokuDate_To
            // 
            this.TourokuDate_To.BackColor = System.Drawing.SystemColors.Window;
            this.TourokuDate_To.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TourokuDate_To.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.TourokuDate_To.Checked = false;
            this.TourokuDate_To.CustomFormat = "yyyy/MM/dd(ddd)";
            this.TourokuDate_To.DateTimeNowYear = "";
            this.TourokuDate_To.DefaultBackColor = System.Drawing.Color.Empty;
            this.TourokuDate_To.DisplayItemName = "";
            this.TourokuDate_To.DisplayPopUp = null;
            this.TourokuDate_To.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TourokuDate_To.FocusOutCheckMethod")));
            this.TourokuDate_To.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TourokuDate_To.ForeColor = System.Drawing.Color.Black;
            this.TourokuDate_To.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TourokuDate_To.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TourokuDate_To.IsInputErrorOccured = false;
            this.TourokuDate_To.Location = new System.Drawing.Point(763, 121);
            this.TourokuDate_To.MaxLength = 10;
            this.TourokuDate_To.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.TourokuDate_To.Name = "TourokuDate_To";
            this.TourokuDate_To.NullValue = "";
            this.TourokuDate_To.PopupAfterExecute = null;
            this.TourokuDate_To.PopupBeforeExecute = null;
            this.TourokuDate_To.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TourokuDate_To.PopupSearchSendParams")));
            this.TourokuDate_To.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TourokuDate_To.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TourokuDate_To.popupWindowSetting")));
            this.TourokuDate_To.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TourokuDate_To.RegistCheckMethod")));
            this.TourokuDate_To.Size = new System.Drawing.Size(135, 20);
            this.TourokuDate_To.TabIndex = 12;
            this.TourokuDate_To.Tag = "";
            this.TourokuDate_To.Value = null;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label19.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(742, 121);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(15, 20);
            this.label19.TabIndex = 820;
            this.label19.Text = "～";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TourokuDate_From
            // 
            this.TourokuDate_From.BackColor = System.Drawing.SystemColors.Window;
            this.TourokuDate_From.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TourokuDate_From.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.TourokuDate_From.Checked = false;
            this.TourokuDate_From.CustomFormat = "yyyy/MM/dd(ddd)";
            this.TourokuDate_From.DateTimeNowYear = "";
            this.TourokuDate_From.DefaultBackColor = System.Drawing.Color.Empty;
            this.TourokuDate_From.DisplayItemName = "";
            this.TourokuDate_From.DisplayPopUp = null;
            this.TourokuDate_From.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TourokuDate_From.FocusOutCheckMethod")));
            this.TourokuDate_From.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TourokuDate_From.ForeColor = System.Drawing.Color.Black;
            this.TourokuDate_From.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TourokuDate_From.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TourokuDate_From.IsInputErrorOccured = false;
            this.TourokuDate_From.Location = new System.Drawing.Point(601, 121);
            this.TourokuDate_From.MaxLength = 10;
            this.TourokuDate_From.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.TourokuDate_From.Name = "TourokuDate_From";
            this.TourokuDate_From.NullValue = "";
            this.TourokuDate_From.PopupAfterExecute = null;
            this.TourokuDate_From.PopupBeforeExecute = null;
            this.TourokuDate_From.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TourokuDate_From.PopupSearchSendParams")));
            this.TourokuDate_From.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TourokuDate_From.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TourokuDate_From.popupWindowSetting")));
            this.TourokuDate_From.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TourokuDate_From.RegistCheckMethod")));
            this.TourokuDate_From.Size = new System.Drawing.Size(135, 20);
            this.TourokuDate_From.TabIndex = 11;
            this.TourokuDate_From.Tag = "";
            this.TourokuDate_From.Value = null;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(486, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 20);
            this.label5.TabIndex = 819;
            this.label5.Text = "初回登録日";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SHAIN_NAME
            // 
            this.SHAIN_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.SHAIN_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHAIN_NAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.SHAIN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHAIN_NAME.DisplayPopUp = null;
            this.SHAIN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHAIN_NAME.FocusOutCheckMethod")));
            this.SHAIN_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHAIN_NAME.ForeColor = System.Drawing.Color.Black;
            this.SHAIN_NAME.IsInputErrorOccured = false;
            this.SHAIN_NAME.Location = new System.Drawing.Point(650, 97);
            this.SHAIN_NAME.MaxLength = 0;
            this.SHAIN_NAME.Name = "SHAIN_NAME";
            this.SHAIN_NAME.PopupAfterExecute = null;
            this.SHAIN_NAME.PopupBeforeExecute = null;
            this.SHAIN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHAIN_NAME.PopupSearchSendParams")));
            this.SHAIN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHAIN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHAIN_NAME.popupWindowSetting")));
            this.SHAIN_NAME.ReadOnly = true;
            this.SHAIN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHAIN_NAME.RegistCheckMethod")));
            this.SHAIN_NAME.Size = new System.Drawing.Size(117, 20);
            this.SHAIN_NAME.TabIndex = 817;
            this.SHAIN_NAME.TabStop = false;
            this.SHAIN_NAME.Tag = "";
            // 
            // SHAIN_CD
            // 
            this.SHAIN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.SHAIN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHAIN_CD.ChangeUpperCase = true;
            this.SHAIN_CD.CharacterLimitList = null;
            this.SHAIN_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.SHAIN_CD.DBFieldsName = "UNTENSHA_CD";
            this.SHAIN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHAIN_CD.DisplayItemName = "運転者";
            this.SHAIN_CD.DisplayPopUp = null;
            this.SHAIN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHAIN_CD.FocusOutCheckMethod")));
            this.SHAIN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHAIN_CD.ForeColor = System.Drawing.Color.Black;
            this.SHAIN_CD.GetCodeMasterField = "SHAIN_CD, SHAIN_NAME_RYAKU";
            this.SHAIN_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SHAIN_CD.IsInputErrorOccured = false;
            this.SHAIN_CD.ItemDefinedTypes = "varchar";
            this.SHAIN_CD.Location = new System.Drawing.Point(601, 97);
            this.SHAIN_CD.MaxLength = 6;
            this.SHAIN_CD.Name = "SHAIN_CD";
            this.SHAIN_CD.PopupAfterExecute = null;
            this.SHAIN_CD.PopupBeforeExecute = null;
            this.SHAIN_CD.PopupGetMasterField = "SHAIN_CD, SHAIN_NAME_RYAKU";
            this.SHAIN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHAIN_CD.PopupSearchSendParams")));
            this.SHAIN_CD.PopupSetFormField = "SHAIN_CD, SHAIN_NAME";
            this.SHAIN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHAIN_CLOSED;
            this.SHAIN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.SHAIN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHAIN_CD.popupWindowSetting")));
            this.SHAIN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHAIN_CD.RegistCheckMethod")));
            this.SHAIN_CD.SetFormField = "SHAIN_CD, SHAIN_NAME";
            this.SHAIN_CD.Size = new System.Drawing.Size(50, 20);
            this.SHAIN_CD.TabIndex = 10;
            this.SHAIN_CD.Tag = "初回登録者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.SHAIN_CD.ZeroPaddengFlag = true;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(486, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 810;
            this.label4.Text = "初回登録者";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HASSEIMOTO_MEISAI_NUMBER
            // 
            this.HASSEIMOTO_MEISAI_NUMBER.BackColor = System.Drawing.SystemColors.Window;
            this.HASSEIMOTO_MEISAI_NUMBER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HASSEIMOTO_MEISAI_NUMBER.DBFieldsName = "HASSEIMOTO_MEISAI_NUMBER";
            this.HASSEIMOTO_MEISAI_NUMBER.DefaultBackColor = System.Drawing.Color.Empty;
            this.HASSEIMOTO_MEISAI_NUMBER.DisplayItemName = "";
            this.HASSEIMOTO_MEISAI_NUMBER.DisplayPopUp = null;
            this.HASSEIMOTO_MEISAI_NUMBER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HASSEIMOTO_MEISAI_NUMBER.FocusOutCheckMethod")));
            this.HASSEIMOTO_MEISAI_NUMBER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HASSEIMOTO_MEISAI_NUMBER.ForeColor = System.Drawing.Color.Black;
            this.HASSEIMOTO_MEISAI_NUMBER.IsInputErrorOccured = false;
            this.HASSEIMOTO_MEISAI_NUMBER.Location = new System.Drawing.Point(884, 47);
            this.HASSEIMOTO_MEISAI_NUMBER.Name = "HASSEIMOTO_MEISAI_NUMBER";
            this.HASSEIMOTO_MEISAI_NUMBER.PopupAfterExecute = null;
            this.HASSEIMOTO_MEISAI_NUMBER.PopupBeforeExecute = null;
            this.HASSEIMOTO_MEISAI_NUMBER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HASSEIMOTO_MEISAI_NUMBER.PopupSearchSendParams")));
            this.HASSEIMOTO_MEISAI_NUMBER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HASSEIMOTO_MEISAI_NUMBER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HASSEIMOTO_MEISAI_NUMBER.popupWindowSetting")));
            rangeSettingDto4.Max = new decimal(new int[] {
            1316134911,
            2328,
            0,
            0});
            this.HASSEIMOTO_MEISAI_NUMBER.RangeSetting = rangeSettingDto4;
            this.HASSEIMOTO_MEISAI_NUMBER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HASSEIMOTO_MEISAI_NUMBER.RegistCheckMethod")));
            this.HASSEIMOTO_MEISAI_NUMBER.Size = new System.Drawing.Size(50, 20);
            this.HASSEIMOTO_MEISAI_NUMBER.TabIndex = 8;
            this.HASSEIMOTO_MEISAI_NUMBER.Tag = "明細Noを入力してください";
            this.HASSEIMOTO_MEISAI_NUMBER.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.HASSEIMOTO_MEISAI_NUMBER.WordWrap = false;
            // 
            // HASSEIMOTO_MEISAI_NUMBER_LABEL
            // 
            this.HASSEIMOTO_MEISAI_NUMBER_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.HASSEIMOTO_MEISAI_NUMBER_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HASSEIMOTO_MEISAI_NUMBER_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HASSEIMOTO_MEISAI_NUMBER_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HASSEIMOTO_MEISAI_NUMBER_LABEL.ForeColor = System.Drawing.Color.White;
            this.HASSEIMOTO_MEISAI_NUMBER_LABEL.Location = new System.Drawing.Point(827, 47);
            this.HASSEIMOTO_MEISAI_NUMBER_LABEL.Name = "HASSEIMOTO_MEISAI_NUMBER_LABEL";
            this.HASSEIMOTO_MEISAI_NUMBER_LABEL.Size = new System.Drawing.Size(55, 20);
            this.HASSEIMOTO_MEISAI_NUMBER_LABEL.TabIndex = 809;
            this.HASSEIMOTO_MEISAI_NUMBER_LABEL.Text = "明細No";
            this.HASSEIMOTO_MEISAI_NUMBER_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HASSEIMOTO_NUMBER
            // 
            this.HASSEIMOTO_NUMBER.BackColor = System.Drawing.SystemColors.Window;
            this.HASSEIMOTO_NUMBER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HASSEIMOTO_NUMBER.DBFieldsName = "HASSEIMOTO_NUMBER";
            this.HASSEIMOTO_NUMBER.DefaultBackColor = System.Drawing.Color.Empty;
            this.HASSEIMOTO_NUMBER.DisplayItemName = "";
            this.HASSEIMOTO_NUMBER.DisplayPopUp = null;
            this.HASSEIMOTO_NUMBER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HASSEIMOTO_NUMBER.FocusOutCheckMethod")));
            this.HASSEIMOTO_NUMBER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HASSEIMOTO_NUMBER.ForeColor = System.Drawing.Color.Black;
            this.HASSEIMOTO_NUMBER.IsInputErrorOccured = false;
            this.HASSEIMOTO_NUMBER.Location = new System.Drawing.Point(736, 47);
            this.HASSEIMOTO_NUMBER.Name = "HASSEIMOTO_NUMBER";
            this.HASSEIMOTO_NUMBER.PopupAfterExecute = null;
            this.HASSEIMOTO_NUMBER.PopupBeforeExecute = null;
            this.HASSEIMOTO_NUMBER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HASSEIMOTO_NUMBER.PopupSearchSendParams")));
            this.HASSEIMOTO_NUMBER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HASSEIMOTO_NUMBER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HASSEIMOTO_NUMBER.popupWindowSetting")));
            rangeSettingDto5.Max = new decimal(new int[] {
            1316134911,
            2328,
            0,
            0});
            this.HASSEIMOTO_NUMBER.RangeSetting = rangeSettingDto5;
            this.HASSEIMOTO_NUMBER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HASSEIMOTO_NUMBER.RegistCheckMethod")));
            this.HASSEIMOTO_NUMBER.Size = new System.Drawing.Size(84, 20);
            this.HASSEIMOTO_NUMBER.TabIndex = 7;
            this.HASSEIMOTO_NUMBER.Tag = "発生元の番号を入力してください";
            this.HASSEIMOTO_NUMBER.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.HASSEIMOTO_NUMBER.WordWrap = false;
            // 
            // HASSEIMOTO_NUMBER_LABEL
            // 
            this.HASSEIMOTO_NUMBER_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.HASSEIMOTO_NUMBER_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HASSEIMOTO_NUMBER_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HASSEIMOTO_NUMBER_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HASSEIMOTO_NUMBER_LABEL.ForeColor = System.Drawing.Color.White;
            this.HASSEIMOTO_NUMBER_LABEL.Location = new System.Drawing.Point(655, 47);
            this.HASSEIMOTO_NUMBER_LABEL.Name = "HASSEIMOTO_NUMBER_LABEL";
            this.HASSEIMOTO_NUMBER_LABEL.Size = new System.Drawing.Size(79, 20);
            this.HASSEIMOTO_NUMBER_LABEL.TabIndex = 808;
            this.HASSEIMOTO_NUMBER_LABEL.Text = "発生元番号";
            this.HASSEIMOTO_NUMBER_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // hasseimoto_label5
            // 
            this.hasseimoto_label5.BackColor = System.Drawing.Color.Transparent;
            this.hasseimoto_label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hasseimoto_label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.hasseimoto_label5.ForeColor = System.Drawing.Color.Black;
            this.hasseimoto_label5.Location = new System.Drawing.Point(1019, 22);
            this.hasseimoto_label5.Name = "hasseimoto_label5";
            this.hasseimoto_label5.Size = new System.Drawing.Size(63, 20);
            this.hasseimoto_label5.TabIndex = 805;
            this.hasseimoto_label5.Text = "定期配車";
            this.hasseimoto_label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkTeikiHaisha
            // 
            this.chkTeikiHaisha.AutoSize = true;
            this.chkTeikiHaisha.Location = new System.Drawing.Point(1001, 26);
            this.chkTeikiHaisha.Name = "chkTeikiHaisha";
            this.chkTeikiHaisha.Size = new System.Drawing.Size(82, 17);
            this.chkTeikiHaisha.TabIndex = 110;
            this.chkTeikiHaisha.TabStop = false;
            this.chkTeikiHaisha.Text = "定期配車";
            this.chkTeikiHaisha.UseVisualStyleBackColor = true;
            this.chkTeikiHaisha.CheckedChanged += new System.EventHandler(this.Hasseimoto_Teiihaisha_CheckedChanged);
            // 
            // hasseimoto_label4
            // 
            this.hasseimoto_label4.BackColor = System.Drawing.Color.Transparent;
            this.hasseimoto_label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hasseimoto_label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.hasseimoto_label4.ForeColor = System.Drawing.Color.Black;
            this.hasseimoto_label4.Location = new System.Drawing.Point(937, 22);
            this.hasseimoto_label4.Name = "hasseimoto_label4";
            this.hasseimoto_label4.Size = new System.Drawing.Size(63, 20);
            this.hasseimoto_label4.TabIndex = 803;
            this.hasseimoto_label4.Text = "持込受付";
            this.hasseimoto_label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkMochikomiUketsuke
            // 
            this.chkMochikomiUketsuke.AutoSize = true;
            this.chkMochikomiUketsuke.Location = new System.Drawing.Point(919, 26);
            this.chkMochikomiUketsuke.Name = "chkMochikomiUketsuke";
            this.chkMochikomiUketsuke.Size = new System.Drawing.Size(82, 17);
            this.chkMochikomiUketsuke.TabIndex = 100;
            this.chkMochikomiUketsuke.TabStop = false;
            this.chkMochikomiUketsuke.Text = "持込受付";
            this.chkMochikomiUketsuke.UseVisualStyleBackColor = true;
            this.chkMochikomiUketsuke.CheckedChanged += new System.EventHandler(this.Hasseimoto_Mochikomi_CheckedChanged);
            // 
            // hasseimoto_label3
            // 
            this.hasseimoto_label3.BackColor = System.Drawing.Color.Transparent;
            this.hasseimoto_label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hasseimoto_label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.hasseimoto_label3.ForeColor = System.Drawing.Color.Black;
            this.hasseimoto_label3.Location = new System.Drawing.Point(854, 22);
            this.hasseimoto_label3.Name = "hasseimoto_label3";
            this.hasseimoto_label3.Size = new System.Drawing.Size(63, 20);
            this.hasseimoto_label3.TabIndex = 801;
            this.hasseimoto_label3.Text = "出荷受付";
            this.hasseimoto_label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkShukkaUketsuke
            // 
            this.chkShukkaUketsuke.AutoSize = true;
            this.chkShukkaUketsuke.Location = new System.Drawing.Point(836, 26);
            this.chkShukkaUketsuke.Name = "chkShukkaUketsuke";
            this.chkShukkaUketsuke.Size = new System.Drawing.Size(82, 17);
            this.chkShukkaUketsuke.TabIndex = 90;
            this.chkShukkaUketsuke.TabStop = false;
            this.chkShukkaUketsuke.Text = "出荷受付";
            this.chkShukkaUketsuke.UseVisualStyleBackColor = true;
            this.chkShukkaUketsuke.CheckedChanged += new System.EventHandler(this.Hasseimoto_Shukka_CheckedChanged);
            // 
            // hasseimoto_label2
            // 
            this.hasseimoto_label2.BackColor = System.Drawing.Color.Transparent;
            this.hasseimoto_label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hasseimoto_label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.hasseimoto_label2.ForeColor = System.Drawing.Color.Black;
            this.hasseimoto_label2.Location = new System.Drawing.Point(767, 22);
            this.hasseimoto_label2.Name = "hasseimoto_label2";
            this.hasseimoto_label2.Size = new System.Drawing.Size(63, 20);
            this.hasseimoto_label2.TabIndex = 799;
            this.hasseimoto_label2.Text = "収集受付";
            this.hasseimoto_label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkShuushuuUketsuke
            // 
            this.chkShuushuuUketsuke.AutoSize = true;
            this.chkShuushuuUketsuke.Location = new System.Drawing.Point(749, 26);
            this.chkShuushuuUketsuke.Name = "chkShuushuuUketsuke";
            this.chkShuushuuUketsuke.Size = new System.Drawing.Size(82, 17);
            this.chkShuushuuUketsuke.TabIndex = 80;
            this.chkShuushuuUketsuke.TabStop = false;
            this.chkShuushuuUketsuke.Text = "収集受付";
            this.chkShuushuuUketsuke.UseVisualStyleBackColor = true;
            this.chkShuushuuUketsuke.CheckedChanged += new System.EventHandler(this.Hasseimoto_Uketsuke_CheckedChanged);
            // 
            // hasseimoto_label1
            // 
            this.hasseimoto_label1.BackColor = System.Drawing.Color.Transparent;
            this.hasseimoto_label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hasseimoto_label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.hasseimoto_label1.ForeColor = System.Drawing.Color.Black;
            this.hasseimoto_label1.Location = new System.Drawing.Point(673, 22);
            this.hasseimoto_label1.Name = "hasseimoto_label1";
            this.hasseimoto_label1.Size = new System.Drawing.Size(79, 20);
            this.hasseimoto_label1.TabIndex = 70;
            this.hasseimoto_label1.Text = "発生元無し";
            this.hasseimoto_label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkHasseimotoNashi
            // 
            this.chkHasseimotoNashi.AutoSize = true;
            this.chkHasseimotoNashi.Location = new System.Drawing.Point(655, 26);
            this.chkHasseimotoNashi.Name = "chkHasseimotoNashi";
            this.chkHasseimotoNashi.Size = new System.Drawing.Size(96, 17);
            this.chkHasseimotoNashi.TabIndex = 70;
            this.chkHasseimotoNashi.TabStop = false;
            this.chkHasseimotoNashi.Text = "発生元無し";
            this.chkHasseimotoNashi.UseVisualStyleBackColor = true;
            this.chkHasseimotoNashi.CheckedChanged += new System.EventHandler(this.Hasseimoto_Nashi_CheckedChanged);
            // 
            // GENBAMEMO_BUNRUI_NAME_RYAKU
            // 
            this.GENBAMEMO_BUNRUI_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GENBAMEMO_BUNRUI_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBAMEMO_BUNRUI_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.GENBAMEMO_BUNRUI_NAME_RYAKU.DBFieldsName = "GENBAMEMO_BUNRUI_NAME_RYAKU";
            this.GENBAMEMO_BUNRUI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBAMEMO_BUNRUI_NAME_RYAKU.DisplayItemName = "現場メモ分類名";
            this.GENBAMEMO_BUNRUI_NAME_RYAKU.DisplayPopUp = null;
            this.GENBAMEMO_BUNRUI_NAME_RYAKU.FocusOutCheckMethod = null;
            this.GENBAMEMO_BUNRUI_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GENBAMEMO_BUNRUI_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.GENBAMEMO_BUNRUI_NAME_RYAKU.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.GENBAMEMO_BUNRUI_NAME_RYAKU.IsInputErrorOccured = false;
            this.GENBAMEMO_BUNRUI_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.GENBAMEMO_BUNRUI_NAME_RYAKU.Location = new System.Drawing.Point(167, 24);
            this.GENBAMEMO_BUNRUI_NAME_RYAKU.MaxLength = 40;
            this.GENBAMEMO_BUNRUI_NAME_RYAKU.Name = "GENBAMEMO_BUNRUI_NAME_RYAKU";
            this.GENBAMEMO_BUNRUI_NAME_RYAKU.PopupAfterExecute = null;
            this.GENBAMEMO_BUNRUI_NAME_RYAKU.PopupBeforeExecute = null;
            this.GENBAMEMO_BUNRUI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBAMEMO_BUNRUI_NAME_RYAKU.PopupSearchSendParams")));
            this.GENBAMEMO_BUNRUI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBAMEMO_BUNRUI_NAME_RYAKU.popupWindowSetting = null;
            this.GENBAMEMO_BUNRUI_NAME_RYAKU.ReadOnly = true;
            this.GENBAMEMO_BUNRUI_NAME_RYAKU.RegistCheckMethod = null;
            this.GENBAMEMO_BUNRUI_NAME_RYAKU.Size = new System.Drawing.Size(287, 20);
            this.GENBAMEMO_BUNRUI_NAME_RYAKU.TabIndex = 791;
            this.GENBAMEMO_BUNRUI_NAME_RYAKU.TabStop = false;
            this.GENBAMEMO_BUNRUI_NAME_RYAKU.Tag = " ";
            // 
            // GENBAMEMO_BUNRUI_CD
            // 
            this.GENBAMEMO_BUNRUI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.GENBAMEMO_BUNRUI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBAMEMO_BUNRUI_CD.ChangeUpperCase = true;
            this.GENBAMEMO_BUNRUI_CD.CharacterLimitList = null;
            this.GENBAMEMO_BUNRUI_CD.CharactersNumber = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.GENBAMEMO_BUNRUI_CD.DBFieldsName = "GENBAMEMO_BUNRUI_CD";
            this.GENBAMEMO_BUNRUI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBAMEMO_BUNRUI_CD.DisplayItemName = "現場メモ分類CD";
            this.GENBAMEMO_BUNRUI_CD.DisplayPopUp = null;
            this.GENBAMEMO_BUNRUI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBAMEMO_BUNRUI_CD.FocusOutCheckMethod")));
            this.GENBAMEMO_BUNRUI_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GENBAMEMO_BUNRUI_CD.ForeColor = System.Drawing.Color.Black;
            this.GENBAMEMO_BUNRUI_CD.GetCodeMasterField = "GENBAMEMO_BUNRUI_CD, GENBAMEMO_BUNRUI_NAME_RYAKU";
            this.GENBAMEMO_BUNRUI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GENBAMEMO_BUNRUI_CD.IsInputErrorOccured = false;
            this.GENBAMEMO_BUNRUI_CD.ItemDefinedTypes = "varchar";
            this.GENBAMEMO_BUNRUI_CD.Location = new System.Drawing.Point(118, 24);
            this.GENBAMEMO_BUNRUI_CD.MaxLength = 3;
            this.GENBAMEMO_BUNRUI_CD.Name = "GENBAMEMO_BUNRUI_CD";
            this.GENBAMEMO_BUNRUI_CD.PopupAfterExecute = null;
            this.GENBAMEMO_BUNRUI_CD.PopupBeforeExecute = null;
            this.GENBAMEMO_BUNRUI_CD.PopupGetMasterField = "";
            this.GENBAMEMO_BUNRUI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBAMEMO_BUNRUI_CD.PopupSearchSendParams")));
            this.GENBAMEMO_BUNRUI_CD.PopupSetFormField = "";
            this.GENBAMEMO_BUNRUI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBAMEMO_BUNRUI;
            this.GENBAMEMO_BUNRUI_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.GENBAMEMO_BUNRUI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBAMEMO_BUNRUI_CD.popupWindowSetting")));
            this.GENBAMEMO_BUNRUI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBAMEMO_BUNRUI_CD.RegistCheckMethod")));
            this.GENBAMEMO_BUNRUI_CD.SetFormField = "GENBAMEMO_BUNRUI_CD, GENBAMEMO_BUNRUI_NAME_RYAKU";
            this.GENBAMEMO_BUNRUI_CD.Size = new System.Drawing.Size(50, 20);
            this.GENBAMEMO_BUNRUI_CD.TabIndex = 2;
            this.GENBAMEMO_BUNRUI_CD.Tag = "現場メモ分類を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GENBAMEMO_BUNRUI_CD.ZeroPaddengFlag = true;
            this.GENBAMEMO_BUNRUI_CD.Validating += new System.ComponentModel.CancelEventHandler(this.GENBAMEMO_BUNRUI_CD_Validating);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(484, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.TabIndex = 45;
            this.label3.Text = "発生元";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 44;
            this.label2.Text = "現場メモ分類";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 43;
            this.label1.Text = "表示区分";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // testGYOUSHA_CD
            // 
            this.testGYOUSHA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.testGYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.testGYOUSHA_CD.DisplayPopUp = null;
            this.testGYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("testGYOUSHA_CD.FocusOutCheckMethod")));
            this.testGYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.testGYOUSHA_CD.IsInputErrorOccured = false;
            this.testGYOUSHA_CD.Location = new System.Drawing.Point(456, 71);
            this.testGYOUSHA_CD.Name = "testGYOUSHA_CD";
            this.testGYOUSHA_CD.PopupAfterExecute = null;
            this.testGYOUSHA_CD.PopupBeforeExecute = null;
            this.testGYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("testGYOUSHA_CD.PopupSearchSendParams")));
            this.testGYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.testGYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("testGYOUSHA_CD.popupWindowSetting")));
            this.testGYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("testGYOUSHA_CD.RegistCheckMethod")));
            this.testGYOUSHA_CD.Size = new System.Drawing.Size(11, 20);
            this.testGYOUSHA_CD.TabIndex = 16;
            this.testGYOUSHA_CD.TabStop = false;
            this.testGYOUSHA_CD.Visible = false;
            // 
            // testGENBA_CD
            // 
            this.testGENBA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.testGENBA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.testGENBA_CD.DisplayPopUp = null;
            this.testGENBA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("testGENBA_CD.FocusOutCheckMethod")));
            this.testGENBA_CD.ForeColor = System.Drawing.Color.Black;
            this.testGENBA_CD.IsInputErrorOccured = false;
            this.testGENBA_CD.Location = new System.Drawing.Point(457, 96);
            this.testGENBA_CD.Name = "testGENBA_CD";
            this.testGENBA_CD.PopupAfterExecute = null;
            this.testGENBA_CD.PopupBeforeExecute = null;
            this.testGENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("testGENBA_CD.PopupSearchSendParams")));
            this.testGENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.testGENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("testGENBA_CD.popupWindowSetting")));
            this.testGENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("testGENBA_CD.RegistCheckMethod")));
            this.testGENBA_CD.Size = new System.Drawing.Size(10, 20);
            this.testGENBA_CD.TabIndex = 19;
            this.testGENBA_CD.TabStop = false;
            this.testGENBA_CD.Visible = false;
            // 
            // GENBA_NAME
            // 
            this.GENBA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GENBA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_NAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.GENBA_NAME.DBFieldsName = "";
            this.GENBA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_NAME.DisplayPopUp = null;
            this.GENBA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME.FocusOutCheckMethod")));
            this.GENBA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GENBA_NAME.ForeColor = System.Drawing.Color.Black;
            this.GENBA_NAME.IsInputErrorOccured = false;
            this.GENBA_NAME.ItemDefinedTypes = "varchar";
            this.GENBA_NAME.Location = new System.Drawing.Point(167, 95);
            this.GENBA_NAME.MaxLength = 0;
            this.GENBA_NAME.Name = "GENBA_NAME";
            this.GENBA_NAME.PopupAfterExecute = null;
            this.GENBA_NAME.PopupBeforeExecute = null;
            this.GENBA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_NAME.PopupSearchSendParams")));
            this.GENBA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_NAME.popupWindowSetting")));
            this.GENBA_NAME.ReadOnly = true;
            this.GENBA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME.RegistCheckMethod")));
            this.GENBA_NAME.Size = new System.Drawing.Size(286, 20);
            this.GENBA_NAME.TabIndex = 42;
            this.GENBA_NAME.TabStop = false;
            this.GENBA_NAME.Tag = " ";
            // 
            // GENBA_CD
            // 
            this.GENBA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.GENBA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_CD.ChangeUpperCase = true;
            this.GENBA_CD.CharacterLimitList = null;
            this.GENBA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.GENBA_CD.DBFieldsName = "GENBA_CD";
            this.GENBA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_CD.DisplayPopUp = null;
            this.GENBA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.FocusOutCheckMethod")));
            this.GENBA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GENBA_CD.ForeColor = System.Drawing.Color.Black;
            this.GENBA_CD.GetCodeMasterField = "";
            this.GENBA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GENBA_CD.IsInputErrorOccured = false;
            this.GENBA_CD.ItemDefinedTypes = "varchar";
            this.GENBA_CD.Location = new System.Drawing.Point(118, 95);
            this.GENBA_CD.MaxLength = 6;
            this.GENBA_CD.Name = "GENBA_CD";
            this.GENBA_CD.PopupAfterExecute = null;
            this.GENBA_CD.PopupBeforeExecute = null;
            this.GENBA_CD.PopupGetMasterField = "GENBA_CD, GENBA_NAME_RYAKU,GENBA_CD,GYOUSHA_CD, GYOUSHA_NAME_RYAKU,GYOUSHA_CD";
            this.GENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_CD.PopupSearchSendParams")));
            this.GENBA_CD.PopupSetFormField = "GENBA_CD, GENBA_NAME,testGENBA_CD,GYOUSHA_CD, GYOUSHA_NAME,testGYOUSHA_CD";
            this.GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_CD.popupWindowSetting")));
            this.GENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.RegistCheckMethod")));
            this.GENBA_CD.SetFormField = "";
            this.GENBA_CD.Size = new System.Drawing.Size(50, 20);
            this.GENBA_CD.TabIndex = 5;
            this.GENBA_CD.Tag = "現場を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GENBA_CD.ZeroPaddengFlag = true;
            this.GENBA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.GENBA_CD_Validating);
            // 
            // lable5
            // 
            this.lable5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lable5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lable5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lable5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lable5.ForeColor = System.Drawing.Color.White;
            this.lable5.Location = new System.Drawing.Point(3, 95);
            this.lable5.Name = "lable5";
            this.lable5.Size = new System.Drawing.Size(110, 20);
            this.lable5.TabIndex = 6;
            this.lable5.Text = "現場";
            this.lable5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GYOUSHA_NAME
            // 
            this.GYOUSHA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GYOUSHA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_NAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.GYOUSHA_NAME.DBFieldsName = "";
            this.GYOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_NAME.DisplayPopUp = null;
            this.GYOUSHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME.FocusOutCheckMethod")));
            this.GYOUSHA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GYOUSHA_NAME.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_NAME.IsInputErrorOccured = false;
            this.GYOUSHA_NAME.ItemDefinedTypes = "varchar";
            this.GYOUSHA_NAME.Location = new System.Drawing.Point(167, 71);
            this.GYOUSHA_NAME.MaxLength = 0;
            this.GYOUSHA_NAME.Name = "GYOUSHA_NAME";
            this.GYOUSHA_NAME.PopupAfterExecute = null;
            this.GYOUSHA_NAME.PopupBeforeExecute = null;
            this.GYOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_NAME.PopupSearchSendParams")));
            this.GYOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_NAME.popupWindowSetting")));
            this.GYOUSHA_NAME.ReadOnly = true;
            this.GYOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME.RegistCheckMethod")));
            this.GYOUSHA_NAME.Size = new System.Drawing.Size(286, 20);
            this.GYOUSHA_NAME.TabIndex = 32;
            this.GYOUSHA_NAME.TabStop = false;
            this.GYOUSHA_NAME.Tag = " ";
            // 
            // GYOUSHA_CD
            // 
            this.GYOUSHA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.GYOUSHA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_CD.ChangeUpperCase = true;
            this.GYOUSHA_CD.CharacterLimitList = null;
            this.GYOUSHA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.GYOUSHA_CD.DBFieldsName = "GYOUSHA_CD";
            this.GYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_CD.DisplayItemName = "業者";
            this.GYOUSHA_CD.DisplayPopUp = null;
            this.GYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.FocusOutCheckMethod")));
            this.GYOUSHA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_CD.GetCodeMasterField = "";
            this.GYOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GYOUSHA_CD.IsInputErrorOccured = false;
            this.GYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.GYOUSHA_CD.Location = new System.Drawing.Point(118, 71);
            this.GYOUSHA_CD.MaxLength = 6;
            this.GYOUSHA_CD.Name = "GYOUSHA_CD";
            this.GYOUSHA_CD.PopupAfterExecute = null;
            this.GYOUSHA_CD.PopupAfterExecuteMethod = "GyoushaCdPopUpAfter";
            this.GYOUSHA_CD.PopupBeforeExecute = null;
            this.GYOUSHA_CD.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_CD.PopupSearchSendParams")));
            this.GYOUSHA_CD.PopupSetFormField = "GYOUSHA_CD, GYOUSHA_NAME";
            this.GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_CD.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_CD.popupWindowSetting")));
            this.GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.RegistCheckMethod")));
            this.GYOUSHA_CD.SetFormField = "";
            this.GYOUSHA_CD.Size = new System.Drawing.Size(50, 20);
            this.GYOUSHA_CD.TabIndex = 4;
            this.GYOUSHA_CD.Tag = "業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GYOUSHA_CD.ZeroPaddengFlag = true;
            this.GYOUSHA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.GYOUSHA_CD_Validating);
            // 
            // TORIHIKISAKI_NAME
            // 
            this.TORIHIKISAKI_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.TORIHIKISAKI_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_NAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.TORIHIKISAKI_NAME.DBFieldsName = "";
            this.TORIHIKISAKI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_NAME.DisplayPopUp = null;
            this.TORIHIKISAKI_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME.FocusOutCheckMethod")));
            this.TORIHIKISAKI_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TORIHIKISAKI_NAME.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_NAME.IsInputErrorOccured = false;
            this.TORIHIKISAKI_NAME.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_NAME.Location = new System.Drawing.Point(167, 48);
            this.TORIHIKISAKI_NAME.MaxLength = 0;
            this.TORIHIKISAKI_NAME.Name = "TORIHIKISAKI_NAME";
            this.TORIHIKISAKI_NAME.PopupAfterExecute = null;
            this.TORIHIKISAKI_NAME.PopupBeforeExecute = null;
            this.TORIHIKISAKI_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_NAME.PopupSearchSendParams")));
            this.TORIHIKISAKI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_NAME.popupWindowSetting")));
            this.TORIHIKISAKI_NAME.ReadOnly = true;
            this.TORIHIKISAKI_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME.RegistCheckMethod")));
            this.TORIHIKISAKI_NAME.Size = new System.Drawing.Size(286, 20);
            this.TORIHIKISAKI_NAME.TabIndex = 22;
            this.TORIHIKISAKI_NAME.TabStop = false;
            this.TORIHIKISAKI_NAME.Tag = " ";
            // 
            // TORIHIKISAKI_CD
            // 
            this.TORIHIKISAKI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.TORIHIKISAKI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_CD.ChangeUpperCase = true;
            this.TORIHIKISAKI_CD.CharacterLimitList = null;
            this.TORIHIKISAKI_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.TORIHIKISAKI_CD.DBFieldsName = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_CD.DisplayItemName = "取引先";
            this.TORIHIKISAKI_CD.DisplayPopUp = null;
            this.TORIHIKISAKI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.FocusOutCheckMethod")));
            this.TORIHIKISAKI_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TORIHIKISAKI_CD.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_CD.GetCodeMasterField = "";
            this.TORIHIKISAKI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TORIHIKISAKI_CD.IsInputErrorOccured = false;
            this.TORIHIKISAKI_CD.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_CD.Location = new System.Drawing.Point(118, 48);
            this.TORIHIKISAKI_CD.MaxLength = 6;
            this.TORIHIKISAKI_CD.Name = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.PopupAfterExecute = null;
            this.TORIHIKISAKI_CD.PopupBeforeExecute = null;
            this.TORIHIKISAKI_CD.PopupGetMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_CD.PopupSearchSendParams")));
            this.TORIHIKISAKI_CD.PopupSetFormField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME";
            this.TORIHIKISAKI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TORIHIKISAKI_CD.PopupWindowName = "検索共通ポップアップ";
            this.TORIHIKISAKI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_CD.popupWindowSetting")));
            this.TORIHIKISAKI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.RegistCheckMethod")));
            this.TORIHIKISAKI_CD.SetFormField = "";
            this.TORIHIKISAKI_CD.Size = new System.Drawing.Size(50, 20);
            this.TORIHIKISAKI_CD.TabIndex = 3;
            this.TORIHIKISAKI_CD.Tag = "取引先を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.TORIHIKISAKI_CD.ZeroPaddengFlag = true;
            this.TORIHIKISAKI_CD.Validating += new System.ComponentModel.CancelEventHandler(this.TORIHIKISAKI_CD_Validating);
            // 
            // lable3
            // 
            this.lable3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lable3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lable3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lable3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lable3.ForeColor = System.Drawing.Color.White;
            this.lable3.Location = new System.Drawing.Point(3, 71);
            this.lable3.Name = "lable3";
            this.lable3.Size = new System.Drawing.Size(110, 20);
            this.lable3.TabIndex = 3;
            this.lable3.Text = "業者";
            this.lable3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lable1
            // 
            this.lable1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lable1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lable1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lable1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lable1.ForeColor = System.Drawing.Color.White;
            this.lable1.Location = new System.Drawing.Point(3, 48);
            this.lable1.Name = "lable1";
            this.lable1.Size = new System.Drawing.Size(110, 20);
            this.lable1.TabIndex = 0;
            this.lable1.Text = "取引先";
            this.lable1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1199, 507);
            this.Controls.Add(this.pnlSearchString);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Name = "UIForm";
            this.Text = "";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.pnlSearchString, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.pnlSearchString.ResumeLayout(false);
            this.pnlSearchString.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomPanel pnlSearchString;
        internal r_framework.CustomControl.CustomTextBox GYOUSHA_NAME;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GYOUSHA_CD;
        internal r_framework.CustomControl.CustomTextBox TORIHIKISAKI_NAME;
        internal r_framework.CustomControl.CustomAlphaNumTextBox TORIHIKISAKI_CD;
        private Label lable3;
        private Label lable1;
        internal r_framework.CustomControl.CustomTextBox GENBA_NAME;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GENBA_CD;
        private Label lable5;
        public r_framework.CustomControl.CustomTextBox testGENBA_CD;
        public r_framework.CustomControl.CustomTextBox testGYOUSHA_CD;
        private Label label3;
        private Label label2;
        private Label label1;
        internal r_framework.CustomControl.CustomTextBox GENBAMEMO_BUNRUI_NAME_RYAKU;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GENBAMEMO_BUNRUI_CD;
        private Label hasseimoto_label5;
        internal CheckBox chkTeikiHaisha;
        private Label hasseimoto_label4;
        internal CheckBox chkMochikomiUketsuke;
        private Label hasseimoto_label3;
        internal CheckBox chkShukkaUketsuke;
        private Label hasseimoto_label2;
        internal CheckBox chkShuushuuUketsuke;
        private Label hasseimoto_label1;
        internal CheckBox chkHasseimotoNashi;
        internal r_framework.CustomControl.CustomNumericTextBox2 HASSEIMOTO_MEISAI_NUMBER;
        internal Label HASSEIMOTO_MEISAI_NUMBER_LABEL;
        internal r_framework.CustomControl.CustomNumericTextBox2 HASSEIMOTO_NUMBER;
        internal Label HASSEIMOTO_NUMBER_LABEL;
        private Label label4;
        internal r_framework.CustomControl.CustomTextBox SHAIN_NAME;
        internal r_framework.CustomControl.CustomAlphaNumTextBox SHAIN_CD;
        internal r_framework.CustomControl.CustomDateTimePicker TourokuDate_From;
        private Label label5;
        private Label label19;
        internal r_framework.CustomControl.CustomDateTimePicker TourokuDate_To;
        private Panel panel1;
        public r_framework.CustomControl.CustomRadioButton radbtnHyoujiKubunHihyouji;
        public r_framework.CustomControl.CustomRadioButton radbtnHyoujiKubunHyouji;
        public r_framework.CustomControl.CustomRadioButton radbtnHyoujiKubunAll;
        public r_framework.CustomControl.CustomNumericTextBox2 txtNum_HyoujiKubunSentaku;
        private Panel panel2;
        public r_framework.CustomControl.CustomRadioButton radbtnHasseimotoShiteiHukusuu;
        public r_framework.CustomControl.CustomRadioButton radbtnHasseimotoShitei;
        public r_framework.CustomControl.CustomRadioButton radbtnHasseimotoAll;
        public r_framework.CustomControl.CustomNumericTextBox2 txtNum_HasseimotoSentaku;
        private Panel panel3;
        public r_framework.CustomControl.CustomRadioButton radbtnTourokushaEigyou;
        public r_framework.CustomControl.CustomRadioButton radbtnTourokushaUnten;
        public r_framework.CustomControl.CustomRadioButton radbtnTourokushaNyuuryoku;
        public r_framework.CustomControl.CustomRadioButton radbtnTourokushaAll;
        public r_framework.CustomControl.CustomNumericTextBox2 txtNum_ShokaiTourokushaSentaku;

    }
}