using System.Windows.Forms;
using System;

namespace Shougun.Core.BusinessManagement.MitumoriIchiran
{
    partial class MitumoriIchiranForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MitumoriIchiranForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBox_Eigyotantosya = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.txtBox_Eigyosyamei = new r_framework.CustomControl.CustomTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numTxtbox_TrhkskCD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.chkBox_Trhksk = new r_framework.CustomControl.CustomCheckBox();
            this.txtBox_TrhkskName = new r_framework.CustomControl.CustomTextBox();
            this.txtBox_GyousyaName = new r_framework.CustomControl.CustomTextBox();
            this.numTxtBox_GyousyaCD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.chkBox_Gyosya = new r_framework.CustomControl.CustomCheckBox();
            this.txtBox_GbName = new r_framework.CustomControl.CustomTextBox();
            this.numTxtBox_GbCD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.chkBox_Gb = new r_framework.CustomControl.CustomCheckBox();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.popBtn_Gb = new r_framework.CustomControl.CustomPopupOpenButton();
            this.popBtn_Gyousya = new r_framework.CustomControl.CustomPopupOpenButton();
            this.popBtn_Torihiki = new r_framework.CustomControl.CustomPopupOpenButton();
            this.txtNum_Jyoukyou = new r_framework.CustomControl.CustomNumericTextBox2();
            this.customPanel2 = new r_framework.CustomControl.CustomPanel();
            this.radbtn_Subete = new r_framework.CustomControl.CustomRadioButton();
            this.radbtn_Sinkoutyuu = new r_framework.CustomControl.CustomRadioButton();
            this.radbtn_Jyutyuu = new r_framework.CustomControl.CustomRadioButton();
            this.radbtn_Situtyuu = new r_framework.CustomControl.CustomRadioButton();
            this.customPanel1.SuspendLayout();
            this.customPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.FocusOutCheckMethod")));
            this.searchString.Font = new System.Drawing.Font("MS UI Gothic", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.searchString.Location = new System.Drawing.Point(920, 10);
            this.searchString.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("searchString.PopupSearchSendParams")));
            this.searchString.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("searchString.popupWindowSetting")));
            this.searchString.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.RegistCheckMethod")));
            this.searchString.Size = new System.Drawing.Size(20, 20);
            this.searchString.TabIndex = 4;
            this.searchString.Visible = false;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Location = new System.Drawing.Point(3, 467);
            this.bt_ptn1.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn1.TabIndex = 23;
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Location = new System.Drawing.Point(203, 467);
            this.bt_ptn2.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn2.TabIndex = 24;
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Location = new System.Drawing.Point(403, 467);
            this.bt_ptn3.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn3.TabIndex = 25;
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Location = new System.Drawing.Point(603, 467);
            this.bt_ptn4.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn4.TabIndex = 26;
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Location = new System.Drawing.Point(803, 467);
            this.bt_ptn5.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn5.TabIndex = 27;
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.AutoSize = true;
            this.customSortHeader1.Location = new System.Drawing.Point(5, 90);
            this.customSortHeader1.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(419, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "取引先";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(419, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "業者";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(8, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "営業担当者";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBox_Eigyotantosya
            // 
            this.txtBox_Eigyotantosya.BackColor = System.Drawing.SystemColors.Window;
            this.txtBox_Eigyotantosya.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBox_Eigyotantosya.ChangeUpperCase = true;
            this.txtBox_Eigyotantosya.CharacterLimitList = null;
            this.txtBox_Eigyotantosya.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.txtBox_Eigyotantosya.DBFieldsName = "SHAIN_CD";
            this.txtBox_Eigyotantosya.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtBox_Eigyotantosya.DisplayItemName = "社員CD";
            this.txtBox_Eigyotantosya.DisplayPopUp = null;
            this.txtBox_Eigyotantosya.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtBox_Eigyotantosya.FocusOutCheckMethod")));
            this.txtBox_Eigyotantosya.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtBox_Eigyotantosya.ForeColor = System.Drawing.Color.Black;
            this.txtBox_Eigyotantosya.GetCodeMasterField = "SHAIN_CD,SHAIN_NAME_RYAKU";
            this.txtBox_Eigyotantosya.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtBox_Eigyotantosya.IsInputErrorOccured = false;
            this.txtBox_Eigyotantosya.ItemDefinedTypes = "smallint";
            this.txtBox_Eigyotantosya.Location = new System.Drawing.Point(106, 6);
            this.txtBox_Eigyotantosya.MaxLength = 6;
            this.txtBox_Eigyotantosya.Name = "txtBox_Eigyotantosya";
            this.txtBox_Eigyotantosya.PopupAfterExecute = null;
            this.txtBox_Eigyotantosya.PopupBeforeExecute = null;
            this.txtBox_Eigyotantosya.PopupGetMasterField = "SHAIN_CD,SHAIN_NAME_RYAKU";
            this.txtBox_Eigyotantosya.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtBox_Eigyotantosya.PopupSearchSendParams")));
            this.txtBox_Eigyotantosya.PopupSetFormField = "txtBox_Eigyotantosya,txtBox_Eigyosyamei";
            this.txtBox_Eigyotantosya.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHAIN;
            this.txtBox_Eigyotantosya.PopupWindowName = "マスタ共通ポップアップ";
            this.txtBox_Eigyotantosya.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtBox_Eigyotantosya.popupWindowSetting")));
            this.txtBox_Eigyotantosya.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtBox_Eigyotantosya.RegistCheckMethod")));
            this.txtBox_Eigyotantosya.SetFormField = "txtBox_Eigyotantosya,txtBox_Eigyosyamei";
            this.txtBox_Eigyotantosya.Size = new System.Drawing.Size(50, 20);
            this.txtBox_Eigyotantosya.TabIndex = 0;
            this.txtBox_Eigyotantosya.Tag = "営業担当者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.txtBox_Eigyotantosya.ZeroPaddengFlag = true;
            this.txtBox_Eigyotantosya.Validated += new System.EventHandler(this.txtBox_Eigyotantosya_OnValidated);
            // 
            // txtBox_Eigyosyamei
            // 
            this.txtBox_Eigyosyamei.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtBox_Eigyosyamei.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBox_Eigyosyamei.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtBox_Eigyosyamei.DisplayPopUp = null;
            this.txtBox_Eigyosyamei.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtBox_Eigyosyamei.FocusOutCheckMethod")));
            this.txtBox_Eigyosyamei.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtBox_Eigyosyamei.ForeColor = System.Drawing.Color.Black;
            this.txtBox_Eigyosyamei.IsInputErrorOccured = false;
            this.txtBox_Eigyosyamei.Location = new System.Drawing.Point(155, 6);
            this.txtBox_Eigyosyamei.Name = "txtBox_Eigyosyamei";
            this.txtBox_Eigyosyamei.PopupAfterExecute = null;
            this.txtBox_Eigyosyamei.PopupBeforeExecute = null;
            this.txtBox_Eigyosyamei.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtBox_Eigyosyamei.PopupSearchSendParams")));
            this.txtBox_Eigyosyamei.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtBox_Eigyosyamei.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtBox_Eigyosyamei.popupWindowSetting")));
            this.txtBox_Eigyosyamei.ReadOnly = true;
            this.txtBox_Eigyosyamei.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtBox_Eigyosyamei.RegistCheckMethod")));
            this.txtBox_Eigyosyamei.Size = new System.Drawing.Size(117, 20);
            this.txtBox_Eigyosyamei.TabIndex = 1;
            this.txtBox_Eigyosyamei.TabStop = false;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(8, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "状況";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(419, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 20);
            this.label5.TabIndex = 10;
            this.label5.Text = "現場";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numTxtbox_TrhkskCD
            // 
            this.numTxtbox_TrhkskCD.BackColor = System.Drawing.SystemColors.Window;
            this.numTxtbox_TrhkskCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numTxtbox_TrhkskCD.ChangeUpperCase = true;
            this.numTxtbox_TrhkskCD.CharacterLimitList = null;
            this.numTxtbox_TrhkskCD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numTxtbox_TrhkskCD.DBFieldsName = "TORIHIKISAKI_CD";
            this.numTxtbox_TrhkskCD.DefaultBackColor = System.Drawing.Color.Empty;
            this.numTxtbox_TrhkskCD.DisplayItemName = "取引先CD";
            this.numTxtbox_TrhkskCD.DisplayPopUp = null;
            this.numTxtbox_TrhkskCD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("numTxtbox_TrhkskCD.FocusOutCheckMethod")));
            this.numTxtbox_TrhkskCD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.numTxtbox_TrhkskCD.ForeColor = System.Drawing.Color.Black;
            this.numTxtbox_TrhkskCD.GetCodeMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU,TORIHIKISAKI_HIKIAI_FLG";
            this.numTxtbox_TrhkskCD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.numTxtbox_TrhkskCD.IsInputErrorOccured = false;
            this.numTxtbox_TrhkskCD.ItemDefinedTypes = "smallint";
            this.numTxtbox_TrhkskCD.Location = new System.Drawing.Point(515, 6);
            this.numTxtbox_TrhkskCD.MaxLength = 6;
            this.numTxtbox_TrhkskCD.Name = "numTxtbox_TrhkskCD";
            this.numTxtbox_TrhkskCD.PopupAfterExecute = null;
            this.numTxtbox_TrhkskCD.PopupBeforeExecute = null;
            this.numTxtbox_TrhkskCD.PopupGetMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU,TORIHIKISAKI_HIKIAI_FLG";
            this.numTxtbox_TrhkskCD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("numTxtbox_TrhkskCD.PopupSearchSendParams")));
            this.numTxtbox_TrhkskCD.PopupSetFormField = "numTxtbox_TrhkskCD,txtBox_TrhkskName,chkBox_Trhksk";
            this.numTxtbox_TrhkskCD.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.numTxtbox_TrhkskCD.PopupWindowName = "引合既存用検索ポップアップ";
            this.numTxtbox_TrhkskCD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("numTxtbox_TrhkskCD.popupWindowSetting")));
            this.numTxtbox_TrhkskCD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("numTxtbox_TrhkskCD.RegistCheckMethod")));
            this.numTxtbox_TrhkskCD.SetFormField = "numTxtbox_TrhkskCD,txtBox_TrhkskName,chkBox_Trhksk";
            this.numTxtbox_TrhkskCD.Size = new System.Drawing.Size(50, 20);
            this.numTxtbox_TrhkskCD.TabIndex = 2;
            this.numTxtbox_TrhkskCD.Tag = "取引先を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.numTxtbox_TrhkskCD.ZeroPaddengFlag = true;
            this.numTxtbox_TrhkskCD.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numTxtbox_TrhkskCD_KeyUp);
            this.numTxtbox_TrhkskCD.Validating += new System.ComponentModel.CancelEventHandler(this.numTxtbox_TrhkskCD_Validating);
            // 
            // chkBox_Trhksk
            // 
            this.chkBox_Trhksk.AutoSize = true;
            this.chkBox_Trhksk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.chkBox_Trhksk.DefaultBackColor = System.Drawing.Color.Empty;
            this.chkBox_Trhksk.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("chkBox_Trhksk.FocusOutCheckMethod")));
            this.chkBox_Trhksk.Location = new System.Drawing.Point(860, 10);
            this.chkBox_Trhksk.Name = "chkBox_Trhksk";
            this.chkBox_Trhksk.PopupAfterExecute = null;
            this.chkBox_Trhksk.PopupBeforeExecute = null;
            this.chkBox_Trhksk.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("chkBox_Trhksk.PopupSearchSendParams")));
            this.chkBox_Trhksk.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.chkBox_Trhksk.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("chkBox_Trhksk.popupWindowSetting")));
            this.chkBox_Trhksk.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("chkBox_Trhksk.RegistCheckMethod")));
            this.chkBox_Trhksk.Size = new System.Drawing.Size(15, 14);
            this.chkBox_Trhksk.TabIndex = 3;
            this.chkBox_Trhksk.UseVisualStyleBackColor = false;
            this.chkBox_Trhksk.Visible = false;
            // 
            // txtBox_TrhkskName
            // 
            this.txtBox_TrhkskName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtBox_TrhkskName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBox_TrhkskName.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtBox_TrhkskName.DisplayPopUp = null;
            this.txtBox_TrhkskName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtBox_TrhkskName.FocusOutCheckMethod")));
            this.txtBox_TrhkskName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtBox_TrhkskName.ForeColor = System.Drawing.Color.Black;
            this.txtBox_TrhkskName.IsInputErrorOccured = false;
            this.txtBox_TrhkskName.Location = new System.Drawing.Point(564, 6);
            this.txtBox_TrhkskName.Name = "txtBox_TrhkskName";
            this.txtBox_TrhkskName.PopupAfterExecute = null;
            this.txtBox_TrhkskName.PopupBeforeExecute = null;
            this.txtBox_TrhkskName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtBox_TrhkskName.PopupSearchSendParams")));
            this.txtBox_TrhkskName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtBox_TrhkskName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtBox_TrhkskName.popupWindowSetting")));
            this.txtBox_TrhkskName.ReadOnly = true;
            this.txtBox_TrhkskName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtBox_TrhkskName.RegistCheckMethod")));
            this.txtBox_TrhkskName.Size = new System.Drawing.Size(293, 20);
            this.txtBox_TrhkskName.TabIndex = 5;
            this.txtBox_TrhkskName.TabStop = false;
            // 
            // txtBox_GyousyaName
            // 
            this.txtBox_GyousyaName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtBox_GyousyaName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBox_GyousyaName.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtBox_GyousyaName.DisplayPopUp = null;
            this.txtBox_GyousyaName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtBox_GyousyaName.FocusOutCheckMethod")));
            this.txtBox_GyousyaName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtBox_GyousyaName.ForeColor = System.Drawing.Color.Black;
            this.txtBox_GyousyaName.IsInputErrorOccured = false;
            this.txtBox_GyousyaName.Location = new System.Drawing.Point(564, 28);
            this.txtBox_GyousyaName.Name = "txtBox_GyousyaName";
            this.txtBox_GyousyaName.PopupAfterExecute = null;
            this.txtBox_GyousyaName.PopupBeforeExecute = null;
            this.txtBox_GyousyaName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtBox_GyousyaName.PopupSearchSendParams")));
            this.txtBox_GyousyaName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtBox_GyousyaName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtBox_GyousyaName.popupWindowSetting")));
            this.txtBox_GyousyaName.ReadOnly = true;
            this.txtBox_GyousyaName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtBox_GyousyaName.RegistCheckMethod")));
            this.txtBox_GyousyaName.Size = new System.Drawing.Size(293, 20);
            this.txtBox_GyousyaName.TabIndex = 9;
            this.txtBox_GyousyaName.TabStop = false;
            // 
            // numTxtBox_GyousyaCD
            // 
            this.numTxtBox_GyousyaCD.BackColor = System.Drawing.SystemColors.Window;
            this.numTxtBox_GyousyaCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numTxtBox_GyousyaCD.ChangeUpperCase = true;
            this.numTxtBox_GyousyaCD.CharacterLimitList = null;
            this.numTxtBox_GyousyaCD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numTxtBox_GyousyaCD.DBFieldsName = "GYOUSHA_CD";
            this.numTxtBox_GyousyaCD.DefaultBackColor = System.Drawing.Color.Empty;
            this.numTxtBox_GyousyaCD.DisplayItemName = "業者";
            this.numTxtBox_GyousyaCD.DisplayPopUp = null;
            this.numTxtBox_GyousyaCD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("numTxtBox_GyousyaCD.FocusOutCheckMethod")));
            this.numTxtBox_GyousyaCD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.numTxtBox_GyousyaCD.ForeColor = System.Drawing.Color.Black;
            this.numTxtBox_GyousyaCD.GetCodeMasterField = "";
            this.numTxtBox_GyousyaCD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.numTxtBox_GyousyaCD.IsInputErrorOccured = false;
            this.numTxtBox_GyousyaCD.ItemDefinedTypes = "varchar";
            this.numTxtBox_GyousyaCD.Location = new System.Drawing.Point(515, 28);
            this.numTxtBox_GyousyaCD.MaxLength = 6;
            this.numTxtBox_GyousyaCD.Name = "numTxtBox_GyousyaCD";
            this.numTxtBox_GyousyaCD.PopupAfterExecute = null;
            this.numTxtBox_GyousyaCD.PopupAfterExecuteMethod = "GyoushaCD_PopupAfter";
            this.numTxtBox_GyousyaCD.PopupBeforeExecute = null;
            this.numTxtBox_GyousyaCD.PopupBeforeExecuteMethod = "GyousyaCD_PopupBefore";
            this.numTxtBox_GyousyaCD.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GYOUSHA_HIKIAI_FLG";
            this.numTxtBox_GyousyaCD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("numTxtBox_GyousyaCD.PopupSearchSendParams")));
            this.numTxtBox_GyousyaCD.PopupSetFormField = "numTxtBox_GyousyaCD,txtBox_GyousyaName,chkBox_Gyosya";
            this.numTxtBox_GyousyaCD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.numTxtBox_GyousyaCD.PopupWindowName = "引合既存用検索ポップアップ";
            this.numTxtBox_GyousyaCD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("numTxtBox_GyousyaCD.popupWindowSetting")));
            this.numTxtBox_GyousyaCD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("numTxtBox_GyousyaCD.RegistCheckMethod")));
            this.numTxtBox_GyousyaCD.SetFormField = "";
            this.numTxtBox_GyousyaCD.ShortItemName = "業者CD";
            this.numTxtBox_GyousyaCD.Size = new System.Drawing.Size(50, 20);
            this.numTxtBox_GyousyaCD.TabIndex = 3;
            this.numTxtBox_GyousyaCD.Tag = "業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.numTxtBox_GyousyaCD.ZeroPaddengFlag = true;
            this.numTxtBox_GyousyaCD.Enter += new System.EventHandler(this.numTxtBox_GyousyaCD_Enter);
            this.numTxtBox_GyousyaCD.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numTxtBox_GyousyaCD_KeyUp);
            this.numTxtBox_GyousyaCD.Validating += new System.ComponentModel.CancelEventHandler(this.numTxtBox_GyousyaCD_Validating);
            // 
            // chkBox_Gyosya
            // 
            this.chkBox_Gyosya.AutoSize = true;
            this.chkBox_Gyosya.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.chkBox_Gyosya.DefaultBackColor = System.Drawing.Color.Empty;
            this.chkBox_Gyosya.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("chkBox_Gyosya.FocusOutCheckMethod")));
            this.chkBox_Gyosya.Location = new System.Drawing.Point(860, 40);
            this.chkBox_Gyosya.Name = "chkBox_Gyosya";
            this.chkBox_Gyosya.PopupAfterExecute = null;
            this.chkBox_Gyosya.PopupBeforeExecute = null;
            this.chkBox_Gyosya.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("chkBox_Gyosya.PopupSearchSendParams")));
            this.chkBox_Gyosya.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.chkBox_Gyosya.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("chkBox_Gyosya.popupWindowSetting")));
            this.chkBox_Gyosya.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("chkBox_Gyosya.RegistCheckMethod")));
            this.chkBox_Gyosya.Size = new System.Drawing.Size(15, 14);
            this.chkBox_Gyosya.TabIndex = 6;
            this.chkBox_Gyosya.UseVisualStyleBackColor = false;
            this.chkBox_Gyosya.Visible = false;
            // 
            // txtBox_GbName
            // 
            this.txtBox_GbName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtBox_GbName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBox_GbName.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtBox_GbName.DisplayPopUp = null;
            this.txtBox_GbName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtBox_GbName.FocusOutCheckMethod")));
            this.txtBox_GbName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtBox_GbName.ForeColor = System.Drawing.Color.Black;
            this.txtBox_GbName.IsInputErrorOccured = false;
            this.txtBox_GbName.Location = new System.Drawing.Point(564, 50);
            this.txtBox_GbName.Name = "txtBox_GbName";
            this.txtBox_GbName.PopupAfterExecute = null;
            this.txtBox_GbName.PopupBeforeExecute = null;
            this.txtBox_GbName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtBox_GbName.PopupSearchSendParams")));
            this.txtBox_GbName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtBox_GbName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtBox_GbName.popupWindowSetting")));
            this.txtBox_GbName.ReadOnly = true;
            this.txtBox_GbName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtBox_GbName.RegistCheckMethod")));
            this.txtBox_GbName.Size = new System.Drawing.Size(293, 20);
            this.txtBox_GbName.TabIndex = 12;
            this.txtBox_GbName.TabStop = false;
            // 
            // numTxtBox_GbCD
            // 
            this.numTxtBox_GbCD.BackColor = System.Drawing.SystemColors.Window;
            this.numTxtBox_GbCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numTxtBox_GbCD.ChangeUpperCase = true;
            this.numTxtBox_GbCD.CharacterLimitList = null;
            this.numTxtBox_GbCD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.numTxtBox_GbCD.DBFieldsName = "GENBA_CD";
            this.numTxtBox_GbCD.DefaultBackColor = System.Drawing.Color.Empty;
            this.numTxtBox_GbCD.DisplayPopUp = null;
            this.numTxtBox_GbCD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("numTxtBox_GbCD.FocusOutCheckMethod")));
            this.numTxtBox_GbCD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.numTxtBox_GbCD.ForeColor = System.Drawing.Color.Black;
            this.numTxtBox_GbCD.GetCodeMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GENBA_HIKIAI_FLG";
            this.numTxtBox_GbCD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.numTxtBox_GbCD.IsInputErrorOccured = false;
            this.numTxtBox_GbCD.ItemDefinedTypes = "varchar";
            this.numTxtBox_GbCD.Location = new System.Drawing.Point(515, 50);
            this.numTxtBox_GbCD.MaxLength = 6;
            this.numTxtBox_GbCD.Name = "numTxtBox_GbCD";
            this.numTxtBox_GbCD.PopupAfterExecute = null;
            this.numTxtBox_GbCD.PopupAfterExecuteMethod = "GenbaCD_PopupAfter";
            this.numTxtBox_GbCD.PopupBeforeExecute = null;
            this.numTxtBox_GbCD.PopupBeforeExecuteMethod = "GenbaCD_PopupBefore";
            this.numTxtBox_GbCD.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GENBA_HIKIAI_FLG,GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GYOUSHA_" +
    "HIKIAI_FLG";
            this.numTxtBox_GbCD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("numTxtBox_GbCD.PopupSearchSendParams")));
            this.numTxtBox_GbCD.PopupSetFormField = "numTxtBox_GbCD,txtBox_GbName,chkBox_Gb,numTxtBox_GyousyaCD,txtBox_GyousyaName,chk" +
    "Box_Gyosya";
            this.numTxtBox_GbCD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.numTxtBox_GbCD.PopupWindowName = "引合既存用複数キー検索共通ポップアップ";
            this.numTxtBox_GbCD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("numTxtBox_GbCD.popupWindowSetting")));
            this.numTxtBox_GbCD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("numTxtBox_GbCD.RegistCheckMethod")));
            this.numTxtBox_GbCD.SetFormField = "numTxtBox_GbCD,chkBox_Gb";
            this.numTxtBox_GbCD.Size = new System.Drawing.Size(50, 20);
            this.numTxtBox_GbCD.TabIndex = 4;
            this.numTxtBox_GbCD.Tag = "現場を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.numTxtBox_GbCD.ZeroPaddengFlag = true;
            this.numTxtBox_GbCD.Enter += new System.EventHandler(this.numTxtBox_GbCD_Enter);
            this.numTxtBox_GbCD.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numTxtBox_GbCD_KeyUp);
            this.numTxtBox_GbCD.Validating += new System.ComponentModel.CancelEventHandler(this.numTxtBox_GbCD_Validating);
            // 
            // chkBox_Gb
            // 
            this.chkBox_Gb.AutoSize = true;
            this.chkBox_Gb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.chkBox_Gb.DefaultBackColor = System.Drawing.Color.Empty;
            this.chkBox_Gb.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("chkBox_Gb.FocusOutCheckMethod")));
            this.chkBox_Gb.Location = new System.Drawing.Point(860, 70);
            this.chkBox_Gb.Name = "chkBox_Gb";
            this.chkBox_Gb.PopupAfterExecute = null;
            this.chkBox_Gb.PopupBeforeExecute = null;
            this.chkBox_Gb.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("chkBox_Gb.PopupSearchSendParams")));
            this.chkBox_Gb.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.chkBox_Gb.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("chkBox_Gb.popupWindowSetting")));
            this.chkBox_Gb.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("chkBox_Gb.RegistCheckMethod")));
            this.chkBox_Gb.Size = new System.Drawing.Size(15, 14);
            this.chkBox_Gb.TabIndex = 40;
            this.chkBox_Gb.UseVisualStyleBackColor = false;
            this.chkBox_Gb.Visible = false;
            // 
            // customPanel1
            // 
            this.customPanel1.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.customPanel1.Controls.Add(this.popBtn_Gb);
            this.customPanel1.Controls.Add(this.popBtn_Gyousya);
            this.customPanel1.Controls.Add(this.popBtn_Torihiki);
            this.customPanel1.Controls.Add(this.txtNum_Jyoukyou);
            this.customPanel1.Controls.Add(this.customPanel2);
            this.customPanel1.Controls.Add(this.txtBox_GbName);
            this.customPanel1.Controls.Add(this.numTxtBox_GbCD);
            this.customPanel1.Controls.Add(this.txtBox_GyousyaName);
            this.customPanel1.Controls.Add(this.numTxtBox_GyousyaCD);
            this.customPanel1.Controls.Add(this.txtBox_TrhkskName);
            this.customPanel1.Controls.Add(this.numTxtbox_TrhkskCD);
            this.customPanel1.Controls.Add(this.label5);
            this.customPanel1.Controls.Add(this.label4);
            this.customPanel1.Controls.Add(this.txtBox_Eigyosyamei);
            this.customPanel1.Controls.Add(this.txtBox_Eigyotantosya);
            this.customPanel1.Controls.Add(this.label2);
            this.customPanel1.Controls.Add(this.label1);
            this.customPanel1.Controls.Add(this.label3);
            this.customPanel1.Location = new System.Drawing.Point(10, 0);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(890, 88);
            this.customPanel1.TabIndex = 0;
            // 
            // ISNOT_NEED_DELETE_FLG
            // 
            this.ISNOT_NEED_DELETE_FLG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ISNOT_NEED_DELETE_FLG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ISNOT_NEED_DELETE_FLG.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.ISNOT_NEED_DELETE_FLG.DBFieldsName = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.ISNOT_NEED_DELETE_FLG.DisplayPopUp = null;
            this.ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ISNOT_NEED_DELETE_FLG.ForeColor = System.Drawing.Color.Black;
            this.ISNOT_NEED_DELETE_FLG.IsInputErrorOccured = false;
            this.ISNOT_NEED_DELETE_FLG.ItemDefinedTypes = "bit";
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(347, 53);
            this.ISNOT_NEED_DELETE_FLG.MaxLength = 20;
            this.ISNOT_NEED_DELETE_FLG.Name = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.PopupAfterExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupBeforeExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.PopupSearchSendParams")));
            this.ISNOT_NEED_DELETE_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ISNOT_NEED_DELETE_FLG.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.popupWindowSetting")));
            this.ISNOT_NEED_DELETE_FLG.ReadOnly = true;
            this.ISNOT_NEED_DELETE_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.RegistCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.Size = new System.Drawing.Size(40, 20);
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 10028;
            this.ISNOT_NEED_DELETE_FLG.TabStop = false;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // popBtn_Gb
            // 
            this.popBtn_Gb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.popBtn_Gb.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.popBtn_Gb.DBFieldsName = null;
            this.popBtn_Gb.DefaultBackColor = System.Drawing.Color.Empty;
            this.popBtn_Gb.DisplayItemName = "現場CD";
            this.popBtn_Gb.DisplayPopUp = null;
            this.popBtn_Gb.ErrorMessage = null;
            this.popBtn_Gb.FocusOutCheckMethod = null;
            this.popBtn_Gb.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.popBtn_Gb.GetCodeMasterField = null;
            this.popBtn_Gb.Image = ((System.Drawing.Image)(resources.GetObject("popBtn_Gb.Image")));
            this.popBtn_Gb.ItemDefinedTypes = null;
            this.popBtn_Gb.LinkedSettingTextBox = null;
            this.popBtn_Gb.LinkedTextBoxs = null;
            this.popBtn_Gb.Location = new System.Drawing.Point(861, 49);
            this.popBtn_Gb.Name = "popBtn_Gb";
            this.popBtn_Gb.PopupAfterExecute = null;
            this.popBtn_Gb.PopupAfterExecuteMethod = "GenbaCD_PopupAfter";
            this.popBtn_Gb.PopupBeforeExecute = null;
            this.popBtn_Gb.PopupBeforeExecuteMethod = "GenbaCD_PopupBefore";
            this.popBtn_Gb.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GENBA_HIKIAI_FLG,GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GYOUSHA_" +
    "HIKIAI_FLG";
            this.popBtn_Gb.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("popBtn_Gb.PopupSearchSendParams")));
            this.popBtn_Gb.PopupSetFormField = "numTxtBox_GbCD,txtBox_GbName,chkBox_Gb,numTxtBox_GyousyaCD,txtBox_GyousyaName,chk" +
    "Box_Gyosya";
            this.popBtn_Gb.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.popBtn_Gb.PopupWindowName = "引合既存用複数キー検索共通ポップアップ";
            this.popBtn_Gb.popupWindowSetting = null;
            this.popBtn_Gb.RegistCheckMethod = null;
            this.popBtn_Gb.SearchDisplayFlag = 0;
            this.popBtn_Gb.SetFormField = "numTxtBox_GbCD,txtBox_GbName";
            this.popBtn_Gb.ShortItemName = "現場CD";
            this.popBtn_Gb.Size = new System.Drawing.Size(22, 22);
            this.popBtn_Gb.TabIndex = 20;
            this.popBtn_Gb.TabStop = false;
            this.popBtn_Gb.UseVisualStyleBackColor = false;
            this.popBtn_Gb.ZeroPaddengFlag = false;
            // 
            // popBtn_Gyousya
            // 
            this.popBtn_Gyousya.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.popBtn_Gyousya.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.popBtn_Gyousya.DBFieldsName = null;
            this.popBtn_Gyousya.DefaultBackColor = System.Drawing.Color.Empty;
            this.popBtn_Gyousya.DisplayItemName = "業者CD";
            this.popBtn_Gyousya.DisplayPopUp = null;
            this.popBtn_Gyousya.ErrorMessage = null;
            this.popBtn_Gyousya.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("popBtn_Gyousya.FocusOutCheckMethod")));
            this.popBtn_Gyousya.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.popBtn_Gyousya.GetCodeMasterField = null;
            this.popBtn_Gyousya.Image = ((System.Drawing.Image)(resources.GetObject("popBtn_Gyousya.Image")));
            this.popBtn_Gyousya.ItemDefinedTypes = null;
            this.popBtn_Gyousya.LinkedSettingTextBox = null;
            this.popBtn_Gyousya.LinkedTextBoxs = null;
            this.popBtn_Gyousya.Location = new System.Drawing.Point(861, 27);
            this.popBtn_Gyousya.Name = "popBtn_Gyousya";
            this.popBtn_Gyousya.PopupAfterExecute = null;
            this.popBtn_Gyousya.PopupAfterExecuteMethod = "GyoushaCD_PopupAfter";
            this.popBtn_Gyousya.PopupBeforeExecute = null;
            this.popBtn_Gyousya.PopupBeforeExecuteMethod = "GyousyaCD_PopupBefore";
            this.popBtn_Gyousya.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GYOUSHA_HIKIAI_FLG";
            this.popBtn_Gyousya.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("popBtn_Gyousya.PopupSearchSendParams")));
            this.popBtn_Gyousya.PopupSetFormField = "numTxtBox_GyousyaCD,txtBox_GyousyaName,chkBox_Gyosya";
            this.popBtn_Gyousya.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.popBtn_Gyousya.PopupWindowName = "引合既存用検索ポップアップ";
            this.popBtn_Gyousya.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("popBtn_Gyousya.popupWindowSetting")));
            this.popBtn_Gyousya.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("popBtn_Gyousya.RegistCheckMethod")));
            this.popBtn_Gyousya.SearchDisplayFlag = 0;
            this.popBtn_Gyousya.SetFormField = "numTxtBox_GyousyaCD,txtBox_GyousyaName";
            this.popBtn_Gyousya.ShortItemName = "業者CD";
            this.popBtn_Gyousya.Size = new System.Drawing.Size(22, 22);
            this.popBtn_Gyousya.TabIndex = 18;
            this.popBtn_Gyousya.TabStop = false;
            this.popBtn_Gyousya.UseVisualStyleBackColor = false;
            this.popBtn_Gyousya.ZeroPaddengFlag = false;
            // 
            // popBtn_Torihiki
            // 
            this.popBtn_Torihiki.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.popBtn_Torihiki.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.popBtn_Torihiki.DBFieldsName = null;
            this.popBtn_Torihiki.DefaultBackColor = System.Drawing.Color.Empty;
            this.popBtn_Torihiki.DisplayItemName = "取引先CD";
            this.popBtn_Torihiki.DisplayPopUp = null;
            this.popBtn_Torihiki.ErrorMessage = null;
            this.popBtn_Torihiki.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("popBtn_Torihiki.FocusOutCheckMethod")));
            this.popBtn_Torihiki.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.popBtn_Torihiki.GetCodeMasterField = null;
            this.popBtn_Torihiki.Image = ((System.Drawing.Image)(resources.GetObject("popBtn_Torihiki.Image")));
            this.popBtn_Torihiki.ItemDefinedTypes = null;
            this.popBtn_Torihiki.LinkedSettingTextBox = null;
            this.popBtn_Torihiki.LinkedTextBoxs = null;
            this.popBtn_Torihiki.Location = new System.Drawing.Point(861, 5);
            this.popBtn_Torihiki.Name = "popBtn_Torihiki";
            this.popBtn_Torihiki.PopupAfterExecute = null;
            this.popBtn_Torihiki.PopupBeforeExecute = null;
            this.popBtn_Torihiki.PopupGetMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU,TORIHIKISAKI_HIKIAI_FLG";
            this.popBtn_Torihiki.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("popBtn_Torihiki.PopupSearchSendParams")));
            this.popBtn_Torihiki.PopupSetFormField = "numTxtbox_TrhkskCD,txtBox_TrhkskName,chkBox_Trhksk";
            this.popBtn_Torihiki.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.popBtn_Torihiki.PopupWindowName = "引合既存用検索ポップアップ";
            this.popBtn_Torihiki.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("popBtn_Torihiki.popupWindowSetting")));
            this.popBtn_Torihiki.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("popBtn_Torihiki.RegistCheckMethod")));
            this.popBtn_Torihiki.SearchDisplayFlag = 0;
            this.popBtn_Torihiki.SetFormField = "numTxtbox_TrhkskCD,txtBox_TrhkskName";
            this.popBtn_Torihiki.ShortItemName = "取引先CD";
            this.popBtn_Torihiki.Size = new System.Drawing.Size(22, 22);
            this.popBtn_Torihiki.TabIndex = 16;
            this.popBtn_Torihiki.TabStop = false;
            this.popBtn_Torihiki.UseVisualStyleBackColor = false;
            this.popBtn_Torihiki.ZeroPaddengFlag = false;
            // 
            // txtNum_Jyoukyou
            // 
            this.txtNum_Jyoukyou.BackColor = System.Drawing.SystemColors.Window;
            this.txtNum_Jyoukyou.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNum_Jyoukyou.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtNum_Jyoukyou.DisplayItemName = "状況";
            this.txtNum_Jyoukyou.DisplayPopUp = null;
            this.txtNum_Jyoukyou.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_Jyoukyou.FocusOutCheckMethod")));
            this.txtNum_Jyoukyou.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtNum_Jyoukyou.ForeColor = System.Drawing.Color.Black;
            this.txtNum_Jyoukyou.IsInputErrorOccured = false;
            this.txtNum_Jyoukyou.LinkedRadioButtonArray = new string[] {
        "radbtn_Subete",
        "radbtn_Jyutyuu",
        "radbtn_Situtyuu"};
            this.txtNum_Jyoukyou.Location = new System.Drawing.Point(106, 28);
            this.txtNum_Jyoukyou.Name = "txtNum_Jyoukyou";
            this.txtNum_Jyoukyou.PopupAfterExecute = null;
            this.txtNum_Jyoukyou.PopupBeforeExecute = null;
            this.txtNum_Jyoukyou.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtNum_Jyoukyou.PopupSearchSendParams")));
            this.txtNum_Jyoukyou.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtNum_Jyoukyou.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtNum_Jyoukyou.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.txtNum_Jyoukyou.RangeSetting = rangeSettingDto1;
            this.txtNum_Jyoukyou.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_Jyoukyou.RegistCheckMethod")));
            this.txtNum_Jyoukyou.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtNum_Jyoukyou.Size = new System.Drawing.Size(20, 20);
            this.txtNum_Jyoukyou.TabIndex = 1;
            this.txtNum_Jyoukyou.Tag = "【0～2】のいずれかで入力してください";
            this.txtNum_Jyoukyou.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNum_Jyoukyou.WordWrap = false;
            this.txtNum_Jyoukyou.TextChanged += new System.EventHandler(this.txtNum_Jyoukyou_TextChanged);
            // 
            // customPanel2
            // 
            this.customPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel2.Controls.Add(this.radbtn_Subete);
            this.customPanel2.Controls.Add(this.radbtn_Sinkoutyuu);
            this.customPanel2.Controls.Add(this.radbtn_Jyutyuu);
            this.customPanel2.Controls.Add(this.radbtn_Situtyuu);
            this.customPanel2.Location = new System.Drawing.Point(125, 28);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(283, 20);
            this.customPanel2.TabIndex = 2;
            this.customPanel2.TabStop = true;
            // 
            // radbtn_Subete
            // 
            this.radbtn_Subete.AutoSize = true;
            this.radbtn_Subete.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Subete.DisplayItemName = "";
            this.radbtn_Subete.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Subete.FocusOutCheckMethod")));
            this.radbtn_Subete.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_Subete.LinkedTextBox = "txtNum_Jyoukyou";
            this.radbtn_Subete.Location = new System.Drawing.Point(0, 0);
            this.radbtn_Subete.Name = "radbtn_Subete";
            this.radbtn_Subete.PopupAfterExecute = null;
            this.radbtn_Subete.PopupBeforeExecute = null;
            this.radbtn_Subete.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Subete.PopupSearchSendParams")));
            this.radbtn_Subete.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Subete.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Subete.popupWindowSetting")));
            this.radbtn_Subete.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Subete.RegistCheckMethod")));
            this.radbtn_Subete.Size = new System.Drawing.Size(67, 17);
            this.radbtn_Subete.TabIndex = 1;
            this.radbtn_Subete.Tag = "全てが対象の場合チェックを付けてください";
            this.radbtn_Subete.Text = "0.全て";
            this.radbtn_Subete.UseVisualStyleBackColor = true;
            this.radbtn_Subete.Value = "0";
            this.radbtn_Subete.CheckedChanged += new System.EventHandler(this.radbtn_Subete_CheckedChanged);
            // 
            // radbtn_Sinkoutyuu
            // 
            this.radbtn_Sinkoutyuu.AutoSize = true;
            this.radbtn_Sinkoutyuu.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Sinkoutyuu.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Sinkoutyuu.FocusOutCheckMethod")));
            this.radbtn_Sinkoutyuu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_Sinkoutyuu.Location = new System.Drawing.Point(67, 0);
            this.radbtn_Sinkoutyuu.Name = "radbtn_Sinkoutyuu";
            this.radbtn_Sinkoutyuu.PopupAfterExecute = null;
            this.radbtn_Sinkoutyuu.PopupBeforeExecute = null;
            this.radbtn_Sinkoutyuu.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Sinkoutyuu.PopupSearchSendParams")));
            this.radbtn_Sinkoutyuu.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Sinkoutyuu.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Sinkoutyuu.popupWindowSetting")));
            this.radbtn_Sinkoutyuu.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Sinkoutyuu.RegistCheckMethod")));
            this.radbtn_Sinkoutyuu.Size = new System.Drawing.Size(81, 17);
            this.radbtn_Sinkoutyuu.TabIndex = 2;
            this.radbtn_Sinkoutyuu.Tag = "進行中が対象の場合チェックを付けてください";
            this.radbtn_Sinkoutyuu.Text = "4.進行中";
            this.radbtn_Sinkoutyuu.UseVisualStyleBackColor = true;
            // 
            // radbtn_Jyutyuu
            // 
            this.radbtn_Jyutyuu.AutoSize = true;
            this.radbtn_Jyutyuu.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Jyutyuu.DisplayItemName = "asdasd";
            this.radbtn_Jyutyuu.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Jyutyuu.FocusOutCheckMethod")));
            this.radbtn_Jyutyuu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_Jyutyuu.LinkedTextBox = "txtNum_Jyoukyou";
            this.radbtn_Jyutyuu.Location = new System.Drawing.Point(148, 0);
            this.radbtn_Jyutyuu.Name = "radbtn_Jyutyuu";
            this.radbtn_Jyutyuu.PopupAfterExecute = null;
            this.radbtn_Jyutyuu.PopupBeforeExecute = null;
            this.radbtn_Jyutyuu.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Jyutyuu.PopupSearchSendParams")));
            this.radbtn_Jyutyuu.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Jyutyuu.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Jyutyuu.popupWindowSetting")));
            this.radbtn_Jyutyuu.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Jyutyuu.RegistCheckMethod")));
            this.radbtn_Jyutyuu.Size = new System.Drawing.Size(67, 17);
            this.radbtn_Jyutyuu.TabIndex = 3;
            this.radbtn_Jyutyuu.Tag = "受注が対象の場合チェックを付けてください";
            this.radbtn_Jyutyuu.Text = "1.受注";
            this.radbtn_Jyutyuu.UseVisualStyleBackColor = true;
            this.radbtn_Jyutyuu.Value = "1";
            this.radbtn_Jyutyuu.CheckedChanged += new System.EventHandler(this.radbtn_Jyutyuu_CheckedChanged);
            // 
            // radbtn_Situtyuu
            // 
            this.radbtn_Situtyuu.AutoSize = true;
            this.radbtn_Situtyuu.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Situtyuu.DisplayItemName = "asdasd";
            this.radbtn_Situtyuu.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Situtyuu.FocusOutCheckMethod")));
            this.radbtn_Situtyuu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_Situtyuu.LinkedTextBox = "txtNum_Jyoukyou";
            this.radbtn_Situtyuu.Location = new System.Drawing.Point(215, 0);
            this.radbtn_Situtyuu.Name = "radbtn_Situtyuu";
            this.radbtn_Situtyuu.PopupAfterExecute = null;
            this.radbtn_Situtyuu.PopupBeforeExecute = null;
            this.radbtn_Situtyuu.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Situtyuu.PopupSearchSendParams")));
            this.radbtn_Situtyuu.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Situtyuu.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Situtyuu.popupWindowSetting")));
            this.radbtn_Situtyuu.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Situtyuu.RegistCheckMethod")));
            this.radbtn_Situtyuu.Size = new System.Drawing.Size(67, 17);
            this.radbtn_Situtyuu.TabIndex = 4;
            this.radbtn_Situtyuu.Tag = "失注が対象の場合チェックを付けてください";
            this.radbtn_Situtyuu.Text = "2.失注";
            this.radbtn_Situtyuu.UseVisualStyleBackColor = true;
            this.radbtn_Situtyuu.Value = "2";
            this.radbtn_Situtyuu.CheckedChanged += new System.EventHandler(this.radbtn_Situtyuu_CheckedChanged);
            // 
            // MitumoriIchiranForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1002, 503);
            this.Controls.Add(this.customPanel1);
            this.Controls.Add(this.chkBox_Gb);
            this.Controls.Add(this.chkBox_Gyosya);
            this.Controls.Add(this.chkBox_Trhksk);
            this.Name = "MitumoriIchiranForm";
            this.SystemId = ((long)(277));
            this.Text = "UIForm";
            this.WindowId = r_framework.Const.WINDOW_ID.T_MITSUMORI_ICHRAN;
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.chkBox_Trhksk, 0);
            this.Controls.SetChildIndex(this.chkBox_Gyosya, 0);
            this.Controls.SetChildIndex(this.chkBox_Gb, 0);
            this.Controls.SetChildIndex(this.customPanel1, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.customPanel2.ResumeLayout(false);
            this.customPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal Label label3;
        internal Label label1;
        internal Label label2;
        public r_framework.CustomControl.CustomAlphaNumTextBox txtBox_Eigyotantosya;
        public r_framework.CustomControl.CustomTextBox txtBox_Eigyosyamei;
        internal Label label4;
        internal Label label5;
        public r_framework.CustomControl.CustomAlphaNumTextBox numTxtbox_TrhkskCD;
        public r_framework.CustomControl.CustomCheckBox chkBox_Trhksk;
        public r_framework.CustomControl.CustomTextBox txtBox_TrhkskName;
        public r_framework.CustomControl.CustomTextBox txtBox_GyousyaName;
        public r_framework.CustomControl.CustomAlphaNumTextBox numTxtBox_GyousyaCD;
        public r_framework.CustomControl.CustomCheckBox chkBox_Gyosya;
        public r_framework.CustomControl.CustomTextBox txtBox_GbName;
        public r_framework.CustomControl.CustomCheckBox chkBox_Gb;
        private r_framework.CustomControl.CustomPanel customPanel1;
        public r_framework.CustomControl.CustomAlphaNumTextBox numTxtBox_GbCD;
        private r_framework.CustomControl.CustomPopupOpenButton popBtn_Torihiki;
        private r_framework.CustomControl.CustomPopupOpenButton popBtn_Gyousya;
        private r_framework.CustomControl.CustomPopupOpenButton popBtn_Gb;
        private r_framework.CustomControl.CustomPanel customPanel2;
        public r_framework.CustomControl.CustomRadioButton radbtn_Subete;
        public r_framework.CustomControl.CustomRadioButton radbtn_Sinkoutyuu;
        public r_framework.CustomControl.CustomRadioButton radbtn_Jyutyuu;
        public r_framework.CustomControl.CustomNumericTextBox2 txtNum_Jyoukyou;
        public r_framework.CustomControl.CustomRadioButton radbtn_Situtyuu;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
    }
}