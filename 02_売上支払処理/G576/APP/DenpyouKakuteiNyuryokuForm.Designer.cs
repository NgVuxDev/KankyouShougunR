using System.Windows.Forms;
using System;

namespace Shougun.Core.SalesPayment.DenpyouKakuteiNyuryoku
{
	partial class DenpyouKakuteiNyuryokuForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DenpyouKakuteiNyuryokuForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            this.lab_DenpyouKind = new System.Windows.Forms.Label();
            this.lblKakuteiKbn = new System.Windows.Forms.Label();
            this.customPanel2 = new r_framework.CustomControl.CustomPanel();
            this.CheckBox_Dainou = new r_framework.CustomControl.CustomCheckBox();
            this.CheckBox_Uriageshiharai = new r_framework.CustomControl.CustomCheckBox();
            this.CheckBox_Syuka = new r_framework.CustomControl.CustomCheckBox();
            this.CheckBox_Jyunyu = new r_framework.CustomControl.CustomCheckBox();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.txtNum_KakuteiKbnSentaku = new r_framework.CustomControl.CustomNumericTextBox2();
            this.radbtn_KakuKbnKakutei = new r_framework.CustomControl.CustomRadioButton();
            this.radbtn_KakuKbnMikakutei = new r_framework.CustomControl.CustomRadioButton();
            this.radbtn_KakuKbnSubete = new r_framework.CustomControl.CustomRadioButton();
            this.Ichiran = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.KAKUTEI_KBN = new r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn();
            this.DENPYOU_KBN_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.DENPYOU_SHURUI = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.DENPYOU_NUMBER = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.DENPYOU_DATE = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.TORIHIKISAKI_CD = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.TORIHIKISAKI_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.GYOUSHA_CD = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.GYOUSHA_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.GENBA_CD = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.GENBA_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.URIAGE_KINGAKU_TOTAL = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.SHIHARAI_KINGAKU_TOTAL = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.EIGYOU_TANTOUSHA_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CREATE_DATE = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.lblDenpyouKbn = new System.Windows.Forms.Label();
            this.txtNum_DenpyouKbnSentaku = new r_framework.CustomControl.CustomNumericTextBox2();
            this.customPanel3 = new r_framework.CustomControl.CustomPanel();
            this.radbtn_DenKbnShiharai = new r_framework.CustomControl.CustomRadioButton();
            this.radbtn_DenKbnUriage = new r_framework.CustomControl.CustomRadioButton();
            this.radbtn_DenKbnSubete = new r_framework.CustomControl.CustomRadioButton();
            this.KAKUTEI_KBN_CHECK_ALL = new r_framework.CustomControl.CustomCheckBox();
            this.dgvCustomCheckBoxColumn1 = new r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn();
            this.dgvCustomTextBoxColumn1 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn2 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn3 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn4 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn5 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn6 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn7 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn8 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn9 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn10 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn11 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn12 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn13 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn14 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomNumericTextBox2Column1 = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.customPanel2.SuspendLayout();
            this.customPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).BeginInit();
            this.customPanel3.SuspendLayout();
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
            this.bt_ptn1.Visible = false;
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn2.Location = new System.Drawing.Point(206, 427);
            this.bt_ptn2.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn2.TabIndex = 11;
            this.bt_ptn2.Text = "パターン2";
            this.bt_ptn2.Visible = false;
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn3.Location = new System.Drawing.Point(406, 427);
            this.bt_ptn3.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn3.TabIndex = 12;
            this.bt_ptn3.Text = "パターン3";
            this.bt_ptn3.Visible = false;
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn4.Location = new System.Drawing.Point(606, 427);
            this.bt_ptn4.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn4.TabIndex = 13;
            this.bt_ptn4.Text = "パターン4";
            this.bt_ptn4.Visible = false;
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn5.Location = new System.Drawing.Point(806, 427);
            this.bt_ptn5.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn5.TabIndex = 14;
            this.bt_ptn5.Text = "パターン5";
            this.bt_ptn5.Visible = false;
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.AutoSize = true;
            this.customSortHeader1.LinkedDataGridViewName = "Ichiran";
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
            this.lab_DenpyouKind.Location = new System.Drawing.Point(0, 64);
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
            this.lblKakuteiKbn.Location = new System.Drawing.Point(0, 26);
            this.lblKakuteiKbn.Name = "lblKakuteiKbn";
            this.lblKakuteiKbn.Size = new System.Drawing.Size(83, 20);
            this.lblKakuteiKbn.TabIndex = 397;
            this.lblKakuteiKbn.Text = "確定区分";
            this.lblKakuteiKbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel2
            // 
            this.customPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel2.Controls.Add(this.CheckBox_Dainou);
            this.customPanel2.Controls.Add(this.CheckBox_Uriageshiharai);
            this.customPanel2.Controls.Add(this.CheckBox_Syuka);
            this.customPanel2.Controls.Add(this.CheckBox_Jyunyu);
            this.customPanel2.Location = new System.Drawing.Point(88, 64);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(272, 20);
            this.customPanel2.TabIndex = 5;
            // 
            // CheckBox_Dainou
            // 
            this.CheckBox_Dainou.AllowDrop = true;
            this.CheckBox_Dainou.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.CheckBox_Dainou.DefaultBackColor = System.Drawing.Color.Empty;
            this.CheckBox_Dainou.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CheckBox_Dainou.FocusOutCheckMethod")));
            this.CheckBox_Dainou.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CheckBox_Dainou.Location = new System.Drawing.Point(217, 1);
            this.CheckBox_Dainou.Name = "CheckBox_Dainou";
            this.CheckBox_Dainou.PopupAfterExecute = null;
            this.CheckBox_Dainou.PopupBeforeExecute = null;
            this.CheckBox_Dainou.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CheckBox_Dainou.PopupSearchSendParams")));
            this.CheckBox_Dainou.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CheckBox_Dainou.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CheckBox_Dainou.popupWindowSetting")));
            this.CheckBox_Dainou.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CheckBox_Dainou.RegistCheckMethod")));
            this.CheckBox_Dainou.Size = new System.Drawing.Size(54, 17);
            this.CheckBox_Dainou.TabIndex = 400;
            this.CheckBox_Dainou.Text = "代納";
            this.CheckBox_Dainou.UseVisualStyleBackColor = false;
            this.CheckBox_Dainou.Visible = false;
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
            this.CheckBox_Uriageshiharai.TabIndex = 4;
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
            this.CheckBox_Syuka.TabIndex = 3;
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
            this.CheckBox_Jyunyu.TabIndex = 2;
            this.CheckBox_Jyunyu.Text = "受入";
            this.CheckBox_Jyunyu.UseVisualStyleBackColor = false;
            // 
            // customPanel1
            // 
            this.customPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel1.Controls.Add(this.txtNum_KakuteiKbnSentaku);
            this.customPanel1.Controls.Add(this.radbtn_KakuKbnKakutei);
            this.customPanel1.Controls.Add(this.radbtn_KakuKbnMikakutei);
            this.customPanel1.Controls.Add(this.radbtn_KakuKbnSubete);
            this.customPanel1.Location = new System.Drawing.Point(88, 26);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(272, 20);
            this.customPanel1.TabIndex = 1;
            // 
            // txtNum_KakuteiKbnSentaku
            // 
            this.txtNum_KakuteiKbnSentaku.BackColor = System.Drawing.SystemColors.Window;
            this.txtNum_KakuteiKbnSentaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNum_KakuteiKbnSentaku.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtNum_KakuteiKbnSentaku.DisplayPopUp = null;
            this.txtNum_KakuteiKbnSentaku.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_KakuteiKbnSentaku.FocusOutCheckMethod")));
            this.txtNum_KakuteiKbnSentaku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtNum_KakuteiKbnSentaku.ForeColor = System.Drawing.Color.Black;
            this.txtNum_KakuteiKbnSentaku.IsInputErrorOccured = false;
            this.txtNum_KakuteiKbnSentaku.LinkedRadioButtonArray = new string[] {
        "radbtn_KakuKbnSubete",
        "radbtn_KakuKbnMikakutei",
        "radbtn_KakuKbnKakutei"};
            this.txtNum_KakuteiKbnSentaku.Location = new System.Drawing.Point(-1, -1);
            this.txtNum_KakuteiKbnSentaku.Name = "txtNum_KakuteiKbnSentaku";
            this.txtNum_KakuteiKbnSentaku.PopupAfterExecute = null;
            this.txtNum_KakuteiKbnSentaku.PopupBeforeExecute = null;
            this.txtNum_KakuteiKbnSentaku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtNum_KakuteiKbnSentaku.PopupSearchSendParams")));
            this.txtNum_KakuteiKbnSentaku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtNum_KakuteiKbnSentaku.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtNum_KakuteiKbnSentaku.popupWindowSetting")));
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
            this.txtNum_KakuteiKbnSentaku.RangeSetting = rangeSettingDto1;
            this.txtNum_KakuteiKbnSentaku.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_KakuteiKbnSentaku.RegistCheckMethod")));
            this.txtNum_KakuteiKbnSentaku.Size = new System.Drawing.Size(20, 20);
            this.txtNum_KakuteiKbnSentaku.TabIndex = 1;
            this.txtNum_KakuteiKbnSentaku.Tag = "確定区分を入力してください";
            this.txtNum_KakuteiKbnSentaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNum_KakuteiKbnSentaku.WordWrap = false;
            // 
            // radbtn_KakuKbnKakutei
            // 
            this.radbtn_KakuKbnKakutei.AutoSize = true;
            this.radbtn_KakuKbnKakutei.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_KakuKbnKakutei.DisplayItemName = "asdasd";
            this.radbtn_KakuKbnKakutei.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_KakuKbnKakutei.FocusOutCheckMethod")));
            this.radbtn_KakuKbnKakutei.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_KakuKbnKakutei.LinkedTextBox = "txtNum_KakuteiKbnSentaku";
            this.radbtn_KakuKbnKakutei.Location = new System.Drawing.Point(176, 0);
            this.radbtn_KakuKbnKakutei.Name = "radbtn_KakuKbnKakutei";
            this.radbtn_KakuKbnKakutei.PopupAfterExecute = null;
            this.radbtn_KakuKbnKakutei.PopupBeforeExecute = null;
            this.radbtn_KakuKbnKakutei.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_KakuKbnKakutei.PopupSearchSendParams")));
            this.radbtn_KakuKbnKakutei.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_KakuKbnKakutei.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_KakuKbnKakutei.popupWindowSetting")));
            this.radbtn_KakuKbnKakutei.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_KakuKbnKakutei.RegistCheckMethod")));
            this.radbtn_KakuKbnKakutei.Size = new System.Drawing.Size(95, 17);
            this.radbtn_KakuKbnKakutei.TabIndex = 4;
            this.radbtn_KakuKbnKakutei.Tag = "確定区分が確定済みのみ対象とする場合にはチェックを付けてください";
            this.radbtn_KakuKbnKakutei.Text = "3.確定済み";
            this.radbtn_KakuKbnKakutei.UseVisualStyleBackColor = true;
            this.radbtn_KakuKbnKakutei.Value = "3";
            // 
            // radbtn_KakuKbnMikakutei
            // 
            this.radbtn_KakuKbnMikakutei.AutoSize = true;
            this.radbtn_KakuKbnMikakutei.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_KakuKbnMikakutei.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_KakuKbnMikakutei.FocusOutCheckMethod")));
            this.radbtn_KakuKbnMikakutei.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_KakuKbnMikakutei.LinkedTextBox = "txtNum_KakuteiKbnSentaku";
            this.radbtn_KakuKbnMikakutei.Location = new System.Drawing.Point(88, 0);
            this.radbtn_KakuKbnMikakutei.Name = "radbtn_KakuKbnMikakutei";
            this.radbtn_KakuKbnMikakutei.PopupAfterExecute = null;
            this.radbtn_KakuKbnMikakutei.PopupBeforeExecute = null;
            this.radbtn_KakuKbnMikakutei.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_KakuKbnMikakutei.PopupSearchSendParams")));
            this.radbtn_KakuKbnMikakutei.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_KakuKbnMikakutei.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_KakuKbnMikakutei.popupWindowSetting")));
            this.radbtn_KakuKbnMikakutei.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_KakuKbnMikakutei.RegistCheckMethod")));
            this.radbtn_KakuKbnMikakutei.Size = new System.Drawing.Size(81, 17);
            this.radbtn_KakuKbnMikakutei.TabIndex = 3;
            this.radbtn_KakuKbnMikakutei.Tag = "確定区分が未確定のみ対象とする場合にはチェックを付けてください";
            this.radbtn_KakuKbnMikakutei.Text = "2.未確定";
            this.radbtn_KakuKbnMikakutei.UseVisualStyleBackColor = true;
            this.radbtn_KakuKbnMikakutei.Value = "2";
            // 
            // radbtn_KakuKbnSubete
            // 
            this.radbtn_KakuKbnSubete.AutoSize = true;
            this.radbtn_KakuKbnSubete.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_KakuKbnSubete.DisplayItemName = "asdasd";
            this.radbtn_KakuKbnSubete.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_KakuKbnSubete.FocusOutCheckMethod")));
            this.radbtn_KakuKbnSubete.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_KakuKbnSubete.LinkedTextBox = "txtNum_KakuteiKbnSentaku";
            this.radbtn_KakuKbnSubete.Location = new System.Drawing.Point(20, 0);
            this.radbtn_KakuKbnSubete.Name = "radbtn_KakuKbnSubete";
            this.radbtn_KakuKbnSubete.PopupAfterExecute = null;
            this.radbtn_KakuKbnSubete.PopupBeforeExecute = null;
            this.radbtn_KakuKbnSubete.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_KakuKbnSubete.PopupSearchSendParams")));
            this.radbtn_KakuKbnSubete.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_KakuKbnSubete.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_KakuKbnSubete.popupWindowSetting")));
            this.radbtn_KakuKbnSubete.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_KakuKbnSubete.RegistCheckMethod")));
            this.radbtn_KakuKbnSubete.Size = new System.Drawing.Size(67, 17);
            this.radbtn_KakuKbnSubete.TabIndex = 2;
            this.radbtn_KakuKbnSubete.Tag = "確定区分全てを対象とする場合にはチェックを付けてください";
            this.radbtn_KakuKbnSubete.Text = "1.全て";
            this.radbtn_KakuKbnSubete.UseVisualStyleBackColor = true;
            this.radbtn_KakuKbnSubete.Value = "1";
            // 
            // Ichiran
            // 
            this.Ichiran.AllowUserToAddRows = false;
            this.Ichiran.AllowUserToDeleteRows = false;
            this.Ichiran.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Ichiran.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Ichiran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Ichiran.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.KAKUTEI_KBN,
            this.DENPYOU_KBN_NAME,
            this.DENPYOU_SHURUI,
            this.DENPYOU_NUMBER,
            this.DENPYOU_DATE,
            this.TORIHIKISAKI_CD,
            this.TORIHIKISAKI_NAME,
            this.GYOUSHA_CD,
            this.GYOUSHA_NAME,
            this.GENBA_CD,
            this.GENBA_NAME,
            this.URIAGE_KINGAKU_TOTAL,
            this.SHIHARAI_KINGAKU_TOTAL,
            this.EIGYOU_TANTOUSHA_NAME,
            this.CREATE_DATE});
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Ichiran.DefaultCellStyle = dataGridViewCellStyle17;
            this.Ichiran.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.Ichiran.EnableHeadersVisualStyles = false;
            this.Ichiran.GridColor = System.Drawing.Color.White;
            this.Ichiran.IsReload = false;
            this.Ichiran.LinkedDataPanelName = "customSortHeader1";
            this.Ichiran.Location = new System.Drawing.Point(3, 183);
            this.Ichiran.MultiSelect = false;
            this.Ichiran.Name = "Ichiran";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle18.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Ichiran.RowHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.Ichiran.RowHeadersVisible = false;
            this.Ichiran.RowTemplate.Height = 21;
            this.Ichiran.ShowCellToolTips = false;
            this.Ichiran.Size = new System.Drawing.Size(997, 238);
            this.Ichiran.TabIndex = 7;
            // 
            // KAKUTEI_KBN
            // 
            this.KAKUTEI_KBN.DataPropertyName = "KAKUTEI_KBN";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = false;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.KAKUTEI_KBN.DefaultCellStyle = dataGridViewCellStyle2;
            this.KAKUTEI_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KAKUTEI_KBN.FocusOutCheckMethod")));
            this.KAKUTEI_KBN.HeaderText = "確定区分";
            this.KAKUTEI_KBN.Name = "KAKUTEI_KBN";
            this.KAKUTEI_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KAKUTEI_KBN.RegistCheckMethod")));
            // 
            // DENPYOU_KBN_NAME
            // 
            this.DENPYOU_KBN_NAME.DataPropertyName = "DENPYOU_KBN_NAME";
            this.DENPYOU_KBN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.DENPYOU_KBN_NAME.DefaultCellStyle = dataGridViewCellStyle3;
            this.DENPYOU_KBN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_KBN_NAME.FocusOutCheckMethod")));
            this.DENPYOU_KBN_NAME.HeaderText = "伝票区分";
            this.DENPYOU_KBN_NAME.Name = "DENPYOU_KBN_NAME";
            this.DENPYOU_KBN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DENPYOU_KBN_NAME.PopupSearchSendParams")));
            this.DENPYOU_KBN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DENPYOU_KBN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DENPYOU_KBN_NAME.popupWindowSetting")));
            this.DENPYOU_KBN_NAME.ReadOnly = true;
            this.DENPYOU_KBN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_KBN_NAME.RegistCheckMethod")));
            this.DENPYOU_KBN_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DENPYOU_SHURUI
            // 
            this.DENPYOU_SHURUI.DataPropertyName = "DENPYOU_SHURUI";
            this.DENPYOU_SHURUI.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.DENPYOU_SHURUI.DefaultCellStyle = dataGridViewCellStyle4;
            this.DENPYOU_SHURUI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_SHURUI.FocusOutCheckMethod")));
            this.DENPYOU_SHURUI.HeaderText = "伝票種類";
            this.DENPYOU_SHURUI.Name = "DENPYOU_SHURUI";
            this.DENPYOU_SHURUI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DENPYOU_SHURUI.PopupSearchSendParams")));
            this.DENPYOU_SHURUI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DENPYOU_SHURUI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DENPYOU_SHURUI.popupWindowSetting")));
            this.DENPYOU_SHURUI.ReadOnly = true;
            this.DENPYOU_SHURUI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_SHURUI.RegistCheckMethod")));
            this.DENPYOU_SHURUI.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DENPYOU_NUMBER
            // 
            this.DENPYOU_NUMBER.DataPropertyName = "DENPYOU_NUMBER";
            this.DENPYOU_NUMBER.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.DENPYOU_NUMBER.DefaultCellStyle = dataGridViewCellStyle5;
            this.DENPYOU_NUMBER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_NUMBER.FocusOutCheckMethod")));
            this.DENPYOU_NUMBER.HeaderText = "伝票番号";
            this.DENPYOU_NUMBER.Name = "DENPYOU_NUMBER";
            this.DENPYOU_NUMBER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DENPYOU_NUMBER.PopupSearchSendParams")));
            this.DENPYOU_NUMBER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DENPYOU_NUMBER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DENPYOU_NUMBER.popupWindowSetting")));
            this.DENPYOU_NUMBER.ReadOnly = true;
            this.DENPYOU_NUMBER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_NUMBER.RegistCheckMethod")));
            this.DENPYOU_NUMBER.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DENPYOU_NUMBER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DENPYOU_DATE
            // 
            this.DENPYOU_DATE.DataPropertyName = "DENPYOU_DATE";
            this.DENPYOU_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.DENPYOU_DATE.DefaultCellStyle = dataGridViewCellStyle6;
            this.DENPYOU_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_DATE.FocusOutCheckMethod")));
            this.DENPYOU_DATE.HeaderText = "伝票日付";
            this.DENPYOU_DATE.Name = "DENPYOU_DATE";
            this.DENPYOU_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DENPYOU_DATE.PopupSearchSendParams")));
            this.DENPYOU_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DENPYOU_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DENPYOU_DATE.popupWindowSetting")));
            this.DENPYOU_DATE.ReadOnly = true;
            this.DENPYOU_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_DATE.RegistCheckMethod")));
            this.DENPYOU_DATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TORIHIKISAKI_CD
            // 
            this.TORIHIKISAKI_CD.DataPropertyName = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_CD.DefaultCellStyle = dataGridViewCellStyle7;
            this.TORIHIKISAKI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.FocusOutCheckMethod")));
            this.TORIHIKISAKI_CD.HeaderText = "取引先CD";
            this.TORIHIKISAKI_CD.Name = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_CD.PopupSearchSendParams")));
            this.TORIHIKISAKI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_CD.popupWindowSetting")));
            this.TORIHIKISAKI_CD.ReadOnly = true;
            this.TORIHIKISAKI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.RegistCheckMethod")));
            this.TORIHIKISAKI_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TORIHIKISAKI_NAME
            // 
            this.TORIHIKISAKI_NAME.DataPropertyName = "TORIHIKISAKI_NAME";
            this.TORIHIKISAKI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_NAME.DefaultCellStyle = dataGridViewCellStyle8;
            this.TORIHIKISAKI_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME.FocusOutCheckMethod")));
            this.TORIHIKISAKI_NAME.HeaderText = "取引先名";
            this.TORIHIKISAKI_NAME.Name = "TORIHIKISAKI_NAME";
            this.TORIHIKISAKI_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_NAME.PopupSearchSendParams")));
            this.TORIHIKISAKI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_NAME.popupWindowSetting")));
            this.TORIHIKISAKI_NAME.ReadOnly = true;
            this.TORIHIKISAKI_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME.RegistCheckMethod")));
            this.TORIHIKISAKI_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // GYOUSHA_CD
            // 
            this.GYOUSHA_CD.DataPropertyName = "GYOUSHA_CD";
            this.GYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_CD.DefaultCellStyle = dataGridViewCellStyle9;
            this.GYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.FocusOutCheckMethod")));
            this.GYOUSHA_CD.HeaderText = "業者CD";
            this.GYOUSHA_CD.Name = "GYOUSHA_CD";
            this.GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_CD.PopupSearchSendParams")));
            this.GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_CD.popupWindowSetting")));
            this.GYOUSHA_CD.ReadOnly = true;
            this.GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.RegistCheckMethod")));
            this.GYOUSHA_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // GYOUSHA_NAME
            // 
            this.GYOUSHA_NAME.DataPropertyName = "GYOUSHA_NAME";
            this.GYOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_NAME.DefaultCellStyle = dataGridViewCellStyle10;
            this.GYOUSHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME.FocusOutCheckMethod")));
            this.GYOUSHA_NAME.HeaderText = "業者名";
            this.GYOUSHA_NAME.Name = "GYOUSHA_NAME";
            this.GYOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_NAME.PopupSearchSendParams")));
            this.GYOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_NAME.popupWindowSetting")));
            this.GYOUSHA_NAME.ReadOnly = true;
            this.GYOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME.RegistCheckMethod")));
            this.GYOUSHA_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // GENBA_CD
            // 
            this.GENBA_CD.DataPropertyName = "GENBA_CD";
            this.GENBA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            this.GENBA_CD.DefaultCellStyle = dataGridViewCellStyle11;
            this.GENBA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.FocusOutCheckMethod")));
            this.GENBA_CD.HeaderText = "現場CD";
            this.GENBA_CD.Name = "GENBA_CD";
            this.GENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_CD.PopupSearchSendParams")));
            this.GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_CD.popupWindowSetting")));
            this.GENBA_CD.ReadOnly = true;
            this.GENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.RegistCheckMethod")));
            this.GENBA_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // GENBA_NAME
            // 
            this.GENBA_NAME.DataPropertyName = "GENBA_NAME";
            this.GENBA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            this.GENBA_NAME.DefaultCellStyle = dataGridViewCellStyle12;
            this.GENBA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME.FocusOutCheckMethod")));
            this.GENBA_NAME.HeaderText = "現場名";
            this.GENBA_NAME.Name = "GENBA_NAME";
            this.GENBA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_NAME.PopupSearchSendParams")));
            this.GENBA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_NAME.popupWindowSetting")));
            this.GENBA_NAME.ReadOnly = true;
            this.GENBA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME.RegistCheckMethod")));
            this.GENBA_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // URIAGE_KINGAKU_TOTAL
            // 
            this.URIAGE_KINGAKU_TOTAL.DataPropertyName = "URIAGE_KINGAKU_TOTAL";
            this.URIAGE_KINGAKU_TOTAL.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.Format = "#,##0";
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Black;
            this.URIAGE_KINGAKU_TOTAL.DefaultCellStyle = dataGridViewCellStyle13;
            this.URIAGE_KINGAKU_TOTAL.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("URIAGE_KINGAKU_TOTAL.FocusOutCheckMethod")));
            this.URIAGE_KINGAKU_TOTAL.HeaderText = "売上金額合計";
            this.URIAGE_KINGAKU_TOTAL.Name = "URIAGE_KINGAKU_TOTAL";
            this.URIAGE_KINGAKU_TOTAL.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("URIAGE_KINGAKU_TOTAL.PopupSearchSendParams")));
            this.URIAGE_KINGAKU_TOTAL.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.URIAGE_KINGAKU_TOTAL.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("URIAGE_KINGAKU_TOTAL.popupWindowSetting")));
            this.URIAGE_KINGAKU_TOTAL.ReadOnly = true;
            this.URIAGE_KINGAKU_TOTAL.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("URIAGE_KINGAKU_TOTAL.RegistCheckMethod")));
            this.URIAGE_KINGAKU_TOTAL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SHIHARAI_KINGAKU_TOTAL
            // 
            this.SHIHARAI_KINGAKU_TOTAL.DataPropertyName = "SHIHARAI_KINGAKU_TOTAL";
            this.SHIHARAI_KINGAKU_TOTAL.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle14.Format = "#,##0";
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black;
            this.SHIHARAI_KINGAKU_TOTAL.DefaultCellStyle = dataGridViewCellStyle14;
            this.SHIHARAI_KINGAKU_TOTAL.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHIHARAI_KINGAKU_TOTAL.FocusOutCheckMethod")));
            this.SHIHARAI_KINGAKU_TOTAL.HeaderText = "支払金額合計";
            this.SHIHARAI_KINGAKU_TOTAL.Name = "SHIHARAI_KINGAKU_TOTAL";
            this.SHIHARAI_KINGAKU_TOTAL.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHIHARAI_KINGAKU_TOTAL.PopupSearchSendParams")));
            this.SHIHARAI_KINGAKU_TOTAL.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHIHARAI_KINGAKU_TOTAL.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHIHARAI_KINGAKU_TOTAL.popupWindowSetting")));
            this.SHIHARAI_KINGAKU_TOTAL.ReadOnly = true;
            this.SHIHARAI_KINGAKU_TOTAL.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHIHARAI_KINGAKU_TOTAL.RegistCheckMethod")));
            this.SHIHARAI_KINGAKU_TOTAL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // EIGYOU_TANTOUSHA_NAME
            // 
            this.EIGYOU_TANTOUSHA_NAME.DataPropertyName = "EIGYOU_TANTOUSHA_NAME";
            this.EIGYOU_TANTOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.Black;
            this.EIGYOU_TANTOUSHA_NAME.DefaultCellStyle = dataGridViewCellStyle15;
            this.EIGYOU_TANTOUSHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("EIGYOU_TANTOUSHA_NAME.FocusOutCheckMethod")));
            this.EIGYOU_TANTOUSHA_NAME.HeaderText = "営業担当者";
            this.EIGYOU_TANTOUSHA_NAME.Name = "EIGYOU_TANTOUSHA_NAME";
            this.EIGYOU_TANTOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("EIGYOU_TANTOUSHA_NAME.PopupSearchSendParams")));
            this.EIGYOU_TANTOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.EIGYOU_TANTOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("EIGYOU_TANTOUSHA_NAME.popupWindowSetting")));
            this.EIGYOU_TANTOUSHA_NAME.ReadOnly = true;
            this.EIGYOU_TANTOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("EIGYOU_TANTOUSHA_NAME.RegistCheckMethod")));
            this.EIGYOU_TANTOUSHA_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // CREATE_DATE
            // 
            this.CREATE_DATE.DataPropertyName = "CREATE_DATE";
            this.CREATE_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.Black;
            this.CREATE_DATE.DefaultCellStyle = dataGridViewCellStyle16;
            this.CREATE_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_DATE.FocusOutCheckMethod")));
            this.CREATE_DATE.HeaderText = "入力日付";
            this.CREATE_DATE.Name = "CREATE_DATE";
            this.CREATE_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CREATE_DATE.PopupSearchSendParams")));
            this.CREATE_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_DATE.popupWindowSetting")));
            this.CREATE_DATE.ReadOnly = true;
            this.CREATE_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_DATE.RegistCheckMethod")));
            this.CREATE_DATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // lblDenpyouKbn
            // 
            this.lblDenpyouKbn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblDenpyouKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDenpyouKbn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDenpyouKbn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblDenpyouKbn.ForeColor = System.Drawing.Color.White;
            this.lblDenpyouKbn.Location = new System.Drawing.Point(0, 102);
            this.lblDenpyouKbn.Name = "lblDenpyouKbn";
            this.lblDenpyouKbn.Size = new System.Drawing.Size(83, 20);
            this.lblDenpyouKbn.TabIndex = 399;
            this.lblDenpyouKbn.Text = "伝票区分";
            this.lblDenpyouKbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNum_DenpyouKbnSentaku
            // 
            this.txtNum_DenpyouKbnSentaku.BackColor = System.Drawing.SystemColors.Window;
            this.txtNum_DenpyouKbnSentaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNum_DenpyouKbnSentaku.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtNum_DenpyouKbnSentaku.DisplayPopUp = null;
            this.txtNum_DenpyouKbnSentaku.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_DenpyouKbnSentaku.FocusOutCheckMethod")));
            this.txtNum_DenpyouKbnSentaku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtNum_DenpyouKbnSentaku.ForeColor = System.Drawing.Color.Black;
            this.txtNum_DenpyouKbnSentaku.IsInputErrorOccured = false;
            this.txtNum_DenpyouKbnSentaku.LinkedRadioButtonArray = new string[] {
        "radbtn_DenKbnSubete",
        "radbtn_DenKbnUriage",
        "radbtn_DenKbnShiharai"};
            this.txtNum_DenpyouKbnSentaku.Location = new System.Drawing.Point(-1, -1);
            this.txtNum_DenpyouKbnSentaku.Name = "txtNum_DenpyouKbnSentaku";
            this.txtNum_DenpyouKbnSentaku.PopupAfterExecute = null;
            this.txtNum_DenpyouKbnSentaku.PopupBeforeExecute = null;
            this.txtNum_DenpyouKbnSentaku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtNum_DenpyouKbnSentaku.PopupSearchSendParams")));
            this.txtNum_DenpyouKbnSentaku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtNum_DenpyouKbnSentaku.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtNum_DenpyouKbnSentaku.popupWindowSetting")));
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
            this.txtNum_DenpyouKbnSentaku.RangeSetting = rangeSettingDto2;
            this.txtNum_DenpyouKbnSentaku.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_DenpyouKbnSentaku.RegistCheckMethod")));
            this.txtNum_DenpyouKbnSentaku.Size = new System.Drawing.Size(20, 20);
            this.txtNum_DenpyouKbnSentaku.TabIndex = 5;
            this.txtNum_DenpyouKbnSentaku.Tag = "伝票区分を入力してください";
            this.txtNum_DenpyouKbnSentaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNum_DenpyouKbnSentaku.WordWrap = false;
            // 
            // customPanel3
            // 
            this.customPanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel3.Controls.Add(this.txtNum_DenpyouKbnSentaku);
            this.customPanel3.Controls.Add(this.radbtn_DenKbnShiharai);
            this.customPanel3.Controls.Add(this.radbtn_DenKbnUriage);
            this.customPanel3.Controls.Add(this.radbtn_DenKbnSubete);
            this.customPanel3.Location = new System.Drawing.Point(88, 102);
            this.customPanel3.Name = "customPanel3";
            this.customPanel3.Size = new System.Drawing.Size(272, 20);
            this.customPanel3.TabIndex = 5;
            // 
            // radbtn_DenKbnShiharai
            // 
            this.radbtn_DenKbnShiharai.AutoSize = true;
            this.radbtn_DenKbnShiharai.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_DenKbnShiharai.DisplayItemName = "asdasd";
            this.radbtn_DenKbnShiharai.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_DenKbnShiharai.FocusOutCheckMethod")));
            this.radbtn_DenKbnShiharai.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_DenKbnShiharai.LinkedTextBox = "txtNum_DenpyouKbnSentaku";
            this.radbtn_DenKbnShiharai.Location = new System.Drawing.Point(176, 0);
            this.radbtn_DenKbnShiharai.Name = "radbtn_DenKbnShiharai";
            this.radbtn_DenKbnShiharai.PopupAfterExecute = null;
            this.radbtn_DenKbnShiharai.PopupBeforeExecute = null;
            this.radbtn_DenKbnShiharai.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_DenKbnShiharai.PopupSearchSendParams")));
            this.radbtn_DenKbnShiharai.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_DenKbnShiharai.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_DenKbnShiharai.popupWindowSetting")));
            this.radbtn_DenKbnShiharai.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_DenKbnShiharai.RegistCheckMethod")));
            this.radbtn_DenKbnShiharai.Size = new System.Drawing.Size(67, 17);
            this.radbtn_DenKbnShiharai.TabIndex = 4;
            this.radbtn_DenKbnShiharai.Tag = "伝票区分が支払のみ対象とする場合にはチェックを付けてください";
            this.radbtn_DenKbnShiharai.Text = "3.支払";
            this.radbtn_DenKbnShiharai.UseVisualStyleBackColor = true;
            this.radbtn_DenKbnShiharai.Value = "3";
            // 
            // radbtn_DenKbnUriage
            // 
            this.radbtn_DenKbnUriage.AutoSize = true;
            this.radbtn_DenKbnUriage.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_DenKbnUriage.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_DenKbnUriage.FocusOutCheckMethod")));
            this.radbtn_DenKbnUriage.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_DenKbnUriage.LinkedTextBox = "txtNum_DenpyouKbnSentaku";
            this.radbtn_DenKbnUriage.Location = new System.Drawing.Point(88, 0);
            this.radbtn_DenKbnUriage.Name = "radbtn_DenKbnUriage";
            this.radbtn_DenKbnUriage.PopupAfterExecute = null;
            this.radbtn_DenKbnUriage.PopupBeforeExecute = null;
            this.radbtn_DenKbnUriage.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_DenKbnUriage.PopupSearchSendParams")));
            this.radbtn_DenKbnUriage.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_DenKbnUriage.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_DenKbnUriage.popupWindowSetting")));
            this.radbtn_DenKbnUriage.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_DenKbnUriage.RegistCheckMethod")));
            this.radbtn_DenKbnUriage.Size = new System.Drawing.Size(67, 17);
            this.radbtn_DenKbnUriage.TabIndex = 3;
            this.radbtn_DenKbnUriage.Tag = "伝票区分が売上のみ対象とする場合にはチェックを付けてください";
            this.radbtn_DenKbnUriage.Text = "2.売上";
            this.radbtn_DenKbnUriage.UseVisualStyleBackColor = true;
            this.radbtn_DenKbnUriage.Value = "2";
            // 
            // radbtn_DenKbnSubete
            // 
            this.radbtn_DenKbnSubete.AutoSize = true;
            this.radbtn_DenKbnSubete.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_DenKbnSubete.DisplayItemName = "asdasd";
            this.radbtn_DenKbnSubete.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_DenKbnSubete.FocusOutCheckMethod")));
            this.radbtn_DenKbnSubete.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_DenKbnSubete.LinkedTextBox = "txtNum_DenpyouKbnSentaku";
            this.radbtn_DenKbnSubete.Location = new System.Drawing.Point(20, 0);
            this.radbtn_DenKbnSubete.Name = "radbtn_DenKbnSubete";
            this.radbtn_DenKbnSubete.PopupAfterExecute = null;
            this.radbtn_DenKbnSubete.PopupBeforeExecute = null;
            this.radbtn_DenKbnSubete.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_DenKbnSubete.PopupSearchSendParams")));
            this.radbtn_DenKbnSubete.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_DenKbnSubete.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_DenKbnSubete.popupWindowSetting")));
            this.radbtn_DenKbnSubete.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_DenKbnSubete.RegistCheckMethod")));
            this.radbtn_DenKbnSubete.Size = new System.Drawing.Size(67, 17);
            this.radbtn_DenKbnSubete.TabIndex = 2;
            this.radbtn_DenKbnSubete.Tag = "伝票区分全てを対象とする場合にはチェックを付けてください";
            this.radbtn_DenKbnSubete.Text = "1.全て";
            this.radbtn_DenKbnSubete.UseVisualStyleBackColor = true;
            this.radbtn_DenKbnSubete.Value = "1";
            // 
            // KAKUTEI_KBN_CHECK_ALL
            // 
            this.KAKUTEI_KBN_CHECK_ALL.AutoSize = true;
            this.KAKUTEI_KBN_CHECK_ALL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.KAKUTEI_KBN_CHECK_ALL.DefaultBackColor = System.Drawing.Color.Empty;
            this.KAKUTEI_KBN_CHECK_ALL.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KAKUTEI_KBN_CHECK_ALL.FocusOutCheckMethod")));
            this.KAKUTEI_KBN_CHECK_ALL.Location = new System.Drawing.Point(638, 124);
            this.KAKUTEI_KBN_CHECK_ALL.Name = "KAKUTEI_KBN_CHECK_ALL";
            this.KAKUTEI_KBN_CHECK_ALL.PopupAfterExecute = null;
            this.KAKUTEI_KBN_CHECK_ALL.PopupBeforeExecute = null;
            this.KAKUTEI_KBN_CHECK_ALL.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KAKUTEI_KBN_CHECK_ALL.PopupSearchSendParams")));
            this.KAKUTEI_KBN_CHECK_ALL.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KAKUTEI_KBN_CHECK_ALL.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KAKUTEI_KBN_CHECK_ALL.popupWindowSetting")));
            this.KAKUTEI_KBN_CHECK_ALL.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KAKUTEI_KBN_CHECK_ALL.RegistCheckMethod")));
            this.KAKUTEI_KBN_CHECK_ALL.Size = new System.Drawing.Size(15, 14);
            this.KAKUTEI_KBN_CHECK_ALL.TabIndex = 6;
            this.KAKUTEI_KBN_CHECK_ALL.UseVisualStyleBackColor = false;
            this.KAKUTEI_KBN_CHECK_ALL.Visible = false;
            // 
            // dgvCustomCheckBoxColumn1
            // 
            this.dgvCustomCheckBoxColumn1.DataPropertyName = "KAKUTEI_KBN";
            this.dgvCustomCheckBoxColumn1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomCheckBoxColumn1.FocusOutCheckMethod")));
            this.dgvCustomCheckBoxColumn1.HeaderText = "確定区分";
            this.dgvCustomCheckBoxColumn1.Name = "dgvCustomCheckBoxColumn1";
            this.dgvCustomCheckBoxColumn1.ReadOnly = true;
            this.dgvCustomCheckBoxColumn1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomCheckBoxColumn1.RegistCheckMethod")));
            // 
            // dgvCustomTextBoxColumn1
            // 
            this.dgvCustomTextBoxColumn1.DataPropertyName = "DENPYOU_KBN_NAME";
            this.dgvCustomTextBoxColumn1.DefaultBackColor = System.Drawing.Color.Empty;
            this.dgvCustomTextBoxColumn1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn1.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn1.HeaderText = "伝票区分";
            this.dgvCustomTextBoxColumn1.Name = "dgvCustomTextBoxColumn1";
            this.dgvCustomTextBoxColumn1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn1.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn1.popupWindowSetting")));
            this.dgvCustomTextBoxColumn1.ReadOnly = true;
            this.dgvCustomTextBoxColumn1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn1.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvCustomTextBoxColumn2
            // 
            this.dgvCustomTextBoxColumn2.DataPropertyName = "DENSHU_KBN_NAME";
            this.dgvCustomTextBoxColumn2.DefaultBackColor = System.Drawing.Color.Empty;
            this.dgvCustomTextBoxColumn2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn2.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn2.HeaderText = "伝票種類";
            this.dgvCustomTextBoxColumn2.Name = "dgvCustomTextBoxColumn2";
            this.dgvCustomTextBoxColumn2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn2.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn2.popupWindowSetting")));
            this.dgvCustomTextBoxColumn2.ReadOnly = true;
            this.dgvCustomTextBoxColumn2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn2.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvCustomTextBoxColumn3
            // 
            this.dgvCustomTextBoxColumn3.DataPropertyName = "DENPYOU_NUM";
            this.dgvCustomTextBoxColumn3.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgvCustomTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle19;
            this.dgvCustomTextBoxColumn3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn3.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn3.HeaderText = "伝票番号";
            this.dgvCustomTextBoxColumn3.Name = "dgvCustomTextBoxColumn3";
            this.dgvCustomTextBoxColumn3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn3.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn3.popupWindowSetting")));
            this.dgvCustomTextBoxColumn3.ReadOnly = true;
            this.dgvCustomTextBoxColumn3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn3.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCustomTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvCustomTextBoxColumn4
            // 
            this.dgvCustomTextBoxColumn4.DataPropertyName = "DENPYOU_DATE";
            this.dgvCustomTextBoxColumn4.DefaultBackColor = System.Drawing.Color.Empty;
            this.dgvCustomTextBoxColumn4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn4.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn4.HeaderText = "伝票日付";
            this.dgvCustomTextBoxColumn4.Name = "dgvCustomTextBoxColumn4";
            this.dgvCustomTextBoxColumn4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn4.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn4.popupWindowSetting")));
            this.dgvCustomTextBoxColumn4.ReadOnly = true;
            this.dgvCustomTextBoxColumn4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn4.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvCustomTextBoxColumn5
            // 
            this.dgvCustomTextBoxColumn5.DataPropertyName = "TORIHIKISAKI_CD";
            this.dgvCustomTextBoxColumn5.DefaultBackColor = System.Drawing.Color.Empty;
            this.dgvCustomTextBoxColumn5.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn5.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn5.HeaderText = "取引先CD";
            this.dgvCustomTextBoxColumn5.Name = "dgvCustomTextBoxColumn5";
            this.dgvCustomTextBoxColumn5.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn5.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn5.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn5.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn5.popupWindowSetting")));
            this.dgvCustomTextBoxColumn5.ReadOnly = true;
            this.dgvCustomTextBoxColumn5.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn5.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvCustomTextBoxColumn6
            // 
            this.dgvCustomTextBoxColumn6.DataPropertyName = "TORIHIKISAKI_NAME";
            this.dgvCustomTextBoxColumn6.DefaultBackColor = System.Drawing.Color.Empty;
            this.dgvCustomTextBoxColumn6.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn6.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn6.HeaderText = "取引先名";
            this.dgvCustomTextBoxColumn6.Name = "dgvCustomTextBoxColumn6";
            this.dgvCustomTextBoxColumn6.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn6.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn6.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn6.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn6.popupWindowSetting")));
            this.dgvCustomTextBoxColumn6.ReadOnly = true;
            this.dgvCustomTextBoxColumn6.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn6.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvCustomTextBoxColumn7
            // 
            this.dgvCustomTextBoxColumn7.DataPropertyName = "GYOUSHA_CD";
            this.dgvCustomTextBoxColumn7.DefaultBackColor = System.Drawing.Color.Empty;
            this.dgvCustomTextBoxColumn7.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn7.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn7.HeaderText = "業者CD";
            this.dgvCustomTextBoxColumn7.Name = "dgvCustomTextBoxColumn7";
            this.dgvCustomTextBoxColumn7.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn7.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn7.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn7.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn7.popupWindowSetting")));
            this.dgvCustomTextBoxColumn7.ReadOnly = true;
            this.dgvCustomTextBoxColumn7.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn7.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvCustomTextBoxColumn8
            // 
            this.dgvCustomTextBoxColumn8.DataPropertyName = "GYOUSHA_NAME";
            this.dgvCustomTextBoxColumn8.DefaultBackColor = System.Drawing.Color.Empty;
            this.dgvCustomTextBoxColumn8.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn8.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn8.HeaderText = "業者名";
            this.dgvCustomTextBoxColumn8.Name = "dgvCustomTextBoxColumn8";
            this.dgvCustomTextBoxColumn8.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn8.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn8.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn8.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn8.popupWindowSetting")));
            this.dgvCustomTextBoxColumn8.ReadOnly = true;
            this.dgvCustomTextBoxColumn8.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn8.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvCustomTextBoxColumn9
            // 
            this.dgvCustomTextBoxColumn9.DataPropertyName = "GENBA_CD";
            this.dgvCustomTextBoxColumn9.DefaultBackColor = System.Drawing.Color.Empty;
            this.dgvCustomTextBoxColumn9.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn9.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn9.HeaderText = "現場CD";
            this.dgvCustomTextBoxColumn9.Name = "dgvCustomTextBoxColumn9";
            this.dgvCustomTextBoxColumn9.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn9.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn9.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn9.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn9.popupWindowSetting")));
            this.dgvCustomTextBoxColumn9.ReadOnly = true;
            this.dgvCustomTextBoxColumn9.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn9.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvCustomTextBoxColumn10
            // 
            this.dgvCustomTextBoxColumn10.DataPropertyName = "GENBA_NAME";
            this.dgvCustomTextBoxColumn10.DefaultBackColor = System.Drawing.Color.Empty;
            this.dgvCustomTextBoxColumn10.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn10.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn10.HeaderText = "現場名";
            this.dgvCustomTextBoxColumn10.Name = "dgvCustomTextBoxColumn10";
            this.dgvCustomTextBoxColumn10.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn10.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn10.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn10.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn10.popupWindowSetting")));
            this.dgvCustomTextBoxColumn10.ReadOnly = true;
            this.dgvCustomTextBoxColumn10.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn10.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvCustomTextBoxColumn11
            // 
            this.dgvCustomTextBoxColumn11.DataPropertyName = "URIAGE_KINGAKU_TOTAL";
            this.dgvCustomTextBoxColumn11.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgvCustomTextBoxColumn11.DefaultCellStyle = dataGridViewCellStyle20;
            this.dgvCustomTextBoxColumn11.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn11.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn11.HeaderText = "売上金額合計";
            this.dgvCustomTextBoxColumn11.Name = "dgvCustomTextBoxColumn11";
            this.dgvCustomTextBoxColumn11.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn11.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn11.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn11.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn11.popupWindowSetting")));
            this.dgvCustomTextBoxColumn11.ReadOnly = true;
            this.dgvCustomTextBoxColumn11.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn11.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvCustomTextBoxColumn12
            // 
            this.dgvCustomTextBoxColumn12.DataPropertyName = "SHIHARAI_KINGAKU_TOTAL";
            this.dgvCustomTextBoxColumn12.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dgvCustomTextBoxColumn12.DefaultCellStyle = dataGridViewCellStyle21;
            this.dgvCustomTextBoxColumn12.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn12.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn12.HeaderText = "支払金額合計";
            this.dgvCustomTextBoxColumn12.Name = "dgvCustomTextBoxColumn12";
            this.dgvCustomTextBoxColumn12.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn12.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn12.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn12.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn12.popupWindowSetting")));
            this.dgvCustomTextBoxColumn12.ReadOnly = true;
            this.dgvCustomTextBoxColumn12.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn12.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvCustomTextBoxColumn13
            // 
            this.dgvCustomTextBoxColumn13.DataPropertyName = "EIGYOU_TANTOU_NAME";
            this.dgvCustomTextBoxColumn13.DefaultBackColor = System.Drawing.Color.Empty;
            this.dgvCustomTextBoxColumn13.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn13.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn13.HeaderText = "営業担当者";
            this.dgvCustomTextBoxColumn13.Name = "dgvCustomTextBoxColumn13";
            this.dgvCustomTextBoxColumn13.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn13.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn13.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn13.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn13.popupWindowSetting")));
            this.dgvCustomTextBoxColumn13.ReadOnly = true;
            this.dgvCustomTextBoxColumn13.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn13.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvCustomTextBoxColumn14
            // 
            this.dgvCustomTextBoxColumn14.DataPropertyName = "NYUURYOKU_DATE";
            this.dgvCustomTextBoxColumn14.DefaultBackColor = System.Drawing.Color.Empty;
            this.dgvCustomTextBoxColumn14.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn14.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn14.HeaderText = "入力日付";
            this.dgvCustomTextBoxColumn14.Name = "dgvCustomTextBoxColumn14";
            this.dgvCustomTextBoxColumn14.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn14.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn14.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn14.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn14.popupWindowSetting")));
            this.dgvCustomTextBoxColumn14.ReadOnly = true;
            this.dgvCustomTextBoxColumn14.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn14.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvCustomNumericTextBox2Column1
            // 
            this.dgvCustomNumericTextBox2Column1.DefaultBackColor = System.Drawing.Color.Empty;
            this.dgvCustomNumericTextBox2Column1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomNumericTextBox2Column1.FocusOutCheckMethod")));
            this.dgvCustomNumericTextBox2Column1.HeaderText = "伝票番号";
            this.dgvCustomNumericTextBox2Column1.Name = "dgvCustomNumericTextBox2Column1";
            this.dgvCustomNumericTextBox2Column1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomNumericTextBox2Column1.PopupSearchSendParams")));
            this.dgvCustomNumericTextBox2Column1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomNumericTextBox2Column1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomNumericTextBox2Column1.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.dgvCustomNumericTextBox2Column1.RangeSetting = rangeSettingDto3;
            this.dgvCustomNumericTextBox2Column1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomNumericTextBox2Column1.RegistCheckMethod")));
            this.dgvCustomNumericTextBox2Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DenpyouKakuteiNyuryokuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.KAKUTEI_KBN_CHECK_ALL);
            this.Controls.Add(this.customPanel3);
            this.Controls.Add(this.lblDenpyouKbn);
            this.Controls.Add(this.Ichiran);
            this.Controls.Add(this.customPanel2);
            this.Controls.Add(this.customPanel1);
            this.Controls.Add(this.lblKakuteiKbn);
            this.Controls.Add(this.lab_DenpyouKind);
            this.Name = "DenpyouKakuteiNyuryokuForm";
            this.Text = "UIForm";
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.lab_DenpyouKind, 0);
            this.Controls.SetChildIndex(this.lblKakuteiKbn, 0);
            this.Controls.SetChildIndex(this.customPanel1, 0);
            this.Controls.SetChildIndex(this.customPanel2, 0);
            this.Controls.SetChildIndex(this.Ichiran, 0);
            this.Controls.SetChildIndex(this.lblDenpyouKbn, 0);
            this.Controls.SetChildIndex(this.customPanel3, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.KAKUTEI_KBN_CHECK_ALL, 0);
            this.customPanel2.ResumeLayout(false);
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).EndInit();
            this.customPanel3.ResumeLayout(false);
            this.customPanel3.PerformLayout();
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
        public r_framework.CustomControl.CustomRadioButton radbtn_KakuKbnKakutei;
        public r_framework.CustomControl.CustomRadioButton radbtn_KakuKbnMikakutei;
        public r_framework.CustomControl.CustomRadioButton radbtn_KakuKbnSubete;
		public r_framework.CustomControl.CustomNumericTextBox2 txtNum_KakuteiKbnSentaku;
		public r_framework.CustomControl.CustomDataGridView Ichiran;
		private r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn dgvCustomCheckBoxColumn1;
		private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn1;
		private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn2;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column dgvCustomNumericTextBox2Column1;
		private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn3;
		private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn4;
		private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn5;
		private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn6;
		private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn7;
		private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn8;
		private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn9;
		private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn10;
		private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn11;
		private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn12;
		private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn13;
		private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn14;
        internal Label lblDenpyouKbn;
        public r_framework.CustomControl.CustomNumericTextBox2 txtNum_DenpyouKbnSentaku;
        private r_framework.CustomControl.CustomPanel customPanel3;
        public r_framework.CustomControl.CustomRadioButton radbtn_DenKbnShiharai;
        public r_framework.CustomControl.CustomRadioButton radbtn_DenKbnUriage;
        public r_framework.CustomControl.CustomRadioButton radbtn_DenKbnSubete;
        public r_framework.CustomControl.CustomCheckBox KAKUTEI_KBN_CHECK_ALL;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn KAKUTEI_KBN;
        private r_framework.CustomControl.DgvCustomTextBoxColumn DENPYOU_KBN_NAME;
        private r_framework.CustomControl.DgvCustomTextBoxColumn DENPYOU_SHURUI;
        private r_framework.CustomControl.DgvCustomTextBoxColumn DENPYOU_NUMBER;
        private r_framework.CustomControl.DgvCustomTextBoxColumn DENPYOU_DATE;
        private r_framework.CustomControl.DgvCustomTextBoxColumn TORIHIKISAKI_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn TORIHIKISAKI_NAME;
        private r_framework.CustomControl.DgvCustomTextBoxColumn GYOUSHA_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn GYOUSHA_NAME;
        private r_framework.CustomControl.DgvCustomTextBoxColumn GENBA_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn GENBA_NAME;
        private r_framework.CustomControl.DgvCustomTextBoxColumn URIAGE_KINGAKU_TOTAL;
        private r_framework.CustomControl.DgvCustomTextBoxColumn SHIHARAI_KINGAKU_TOTAL;
        private r_framework.CustomControl.DgvCustomTextBoxColumn EIGYOU_TANTOUSHA_NAME;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CREATE_DATE;
        internal r_framework.CustomControl.CustomCheckBox CheckBox_Dainou;
    }
}