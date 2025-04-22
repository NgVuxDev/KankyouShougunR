using System.Windows.Forms;
using System;

namespace KyokaShouIchiran
{
    partial class KyokaShouIchiranForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KyokaShouIchiranForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            this.label4 = new System.Windows.Forms.Label();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.CHIIKI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.HOUKOKU_SHO_BUNRUI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.HOUKOKU_SHO_BUNRUI_NAME = new r_framework.CustomControl.CustomTextBox();
            this.KYOKA_NO = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.GENBA_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.GYOUSHA_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.GENBA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GYOUSHA_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.GENBA_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.HIDUKE_FROM = new r_framework.CustomControl.CustomDateTimePicker();
            this.HIDUKE_TO = new r_framework.CustomControl.CustomDateTimePicker();
            this.lab_HidukeNyuuryoku = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.KIKAN_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.KYOKA_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.customPanel3 = new r_framework.CustomControl.CustomPanel();
            this.KIKAN_KBN1 = new r_framework.CustomControl.CustomRadioButton();
            this.KIKAN_KBN2 = new r_framework.CustomControl.CustomRadioButton();
            this.customPanel2 = new r_framework.CustomControl.CustomPanel();
            this.KYOKA_KBN4 = new r_framework.CustomControl.CustomRadioButton();
            this.KYOKA_KBN1 = new r_framework.CustomControl.CustomRadioButton();
            this.KYOKA_KBN2 = new r_framework.CustomControl.CustomRadioButton();
            this.KYOKA_KBN3 = new r_framework.CustomControl.CustomRadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.FUTSU_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.customPanel4 = new r_framework.CustomControl.CustomPanel();
            this.FUTSU_KBN1 = new r_framework.CustomControl.CustomRadioButton();
            this.FUTSU_KBN2 = new r_framework.CustomControl.CustomRadioButton();
            this.FUTSU_KBN3 = new r_framework.CustomControl.CustomRadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.CHIIKI_NAME = new r_framework.CustomControl.CustomTextBox();
            this.customPanel1.SuspendLayout();
            this.customPanel3.SuspendLayout();
            this.customPanel2.SuspendLayout();
            this.customPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.FocusOutCheckMethod")));
            this.searchString.Font = new System.Drawing.Font("MS UI Gothic", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.searchString.Location = new System.Drawing.Point(918, 8);
            this.searchString.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("searchString.PopupSearchSendParams")));
            this.searchString.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("searchString.popupWindowSetting")));
            this.searchString.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.RegistCheckMethod")));
            this.searchString.Size = new System.Drawing.Size(36, 35);
            this.searchString.TabIndex = 4;
            this.searchString.TabStop = false;
            this.searchString.Visible = false;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Location = new System.Drawing.Point(3, 442);
            this.bt_ptn1.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn1.TabIndex = 23;
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Location = new System.Drawing.Point(203, 442);
            this.bt_ptn2.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn2.TabIndex = 24;
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Location = new System.Drawing.Point(403, 442);
            this.bt_ptn3.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn3.TabIndex = 25;
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Location = new System.Drawing.Point(603, 442);
            this.bt_ptn4.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn4.TabIndex = 26;
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Location = new System.Drawing.Point(803, 442);
            this.bt_ptn5.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn5.TabIndex = 27;
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.Location = new System.Drawing.Point(1, 133);
            this.customSortHeader1.TabIndex = 5;
            // 
            // customSearchHeader1
            // 
            this.customSearchHeader1.AutoScroll = true;
            this.customSearchHeader1.Location = new System.Drawing.Point(1, 111);
            this.customSearchHeader1.TabStop = false;
            this.customSearchHeader1.Visible = true;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(1, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "許可区分";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel1
            // 
            this.customPanel1.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.customPanel1.Controls.Add(this.CHIIKI_CD);
            this.customPanel1.Controls.Add(this.HOUKOKU_SHO_BUNRUI_CD);
            this.customPanel1.Controls.Add(this.HOUKOKU_SHO_BUNRUI_NAME);
            this.customPanel1.Controls.Add(this.KYOKA_NO);
            this.customPanel1.Controls.Add(this.label10);
            this.customPanel1.Controls.Add(this.label9);
            this.customPanel1.Controls.Add(this.GENBA_SEARCH_BUTTON);
            this.customPanel1.Controls.Add(this.GYOUSHA_SEARCH_BUTTON);
            this.customPanel1.Controls.Add(this.GENBA_CD);
            this.customPanel1.Controls.Add(this.GYOUSHA_CD);
            this.customPanel1.Controls.Add(this.GYOUSHA_NAME_RYAKU);
            this.customPanel1.Controls.Add(this.GENBA_NAME_RYAKU);
            this.customPanel1.Controls.Add(this.label6);
            this.customPanel1.Controls.Add(this.label8);
            this.customPanel1.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_DELETED);
            this.customPanel1.Controls.Add(this.label3);
            this.customPanel1.Controls.Add(this.HIDUKE_FROM);
            this.customPanel1.Controls.Add(this.HIDUKE_TO);
            this.customPanel1.Controls.Add(this.lab_HidukeNyuuryoku);
            this.customPanel1.Controls.Add(this.label2);
            this.customPanel1.Controls.Add(this.KIKAN_KBN);
            this.customPanel1.Controls.Add(this.KYOKA_KBN);
            this.customPanel1.Controls.Add(this.customPanel3);
            this.customPanel1.Controls.Add(this.customPanel2);
            this.customPanel1.Controls.Add(this.label7);
            this.customPanel1.Controls.Add(this.label4);
            this.customPanel1.Controls.Add(this.FUTSU_KBN);
            this.customPanel1.Controls.Add(this.customPanel4);
            this.customPanel1.Controls.Add(this.label5);
            this.customPanel1.Controls.Add(this.CHIIKI_NAME);
            this.customPanel1.Location = new System.Drawing.Point(1, 0);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(997, 112);
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
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(834, 85);
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
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 671;
            this.ISNOT_NEED_DELETE_FLG.TabStop = false;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // CHIIKI_CD
            // 
            this.CHIIKI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.CHIIKI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CHIIKI_CD.ChangeUpperCase = true;
            this.CHIIKI_CD.CharacterLimitList = null;
            this.CHIIKI_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.CHIIKI_CD.DBFieldsName = "CHIIKI_CD";
            this.CHIIKI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.CHIIKI_CD.DisplayItemName = "地域CD";
            this.CHIIKI_CD.DisplayPopUp = null;
            this.CHIIKI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CHIIKI_CD.FocusOutCheckMethod")));
            this.CHIIKI_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CHIIKI_CD.ForeColor = System.Drawing.Color.Black;
            this.CHIIKI_CD.GetCodeMasterField = "CHIIKI_CD,CHIIKI_NAME_RYAKU";
            this.CHIIKI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.CHIIKI_CD.IsInputErrorOccured = false;
            this.CHIIKI_CD.ItemDefinedTypes = "varchar";
            this.CHIIKI_CD.Location = new System.Drawing.Point(115, 69);
            this.CHIIKI_CD.MaxLength = 6;
            this.CHIIKI_CD.Name = "CHIIKI_CD";
            this.CHIIKI_CD.PopupAfterExecute = null;
            this.CHIIKI_CD.PopupBeforeExecute = null;
            this.CHIIKI_CD.PopupGetMasterField = "CHIIKI_CD,CHIIKI_NAME_RYAKU";
            this.CHIIKI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CHIIKI_CD.PopupSearchSendParams")));
            this.CHIIKI_CD.PopupSetFormField = "CHIIKI_CD,CHIIKI_NAME";
            this.CHIIKI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_CHIIKI;
            this.CHIIKI_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.CHIIKI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CHIIKI_CD.popupWindowSetting")));
            this.CHIIKI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CHIIKI_CD.RegistCheckMethod")));
            this.CHIIKI_CD.SetFormField = "CHIIKI_CD,CHIIKI_NAME";
            this.CHIIKI_CD.ShortItemName = "";
            this.CHIIKI_CD.Size = new System.Drawing.Size(55, 20);
            this.CHIIKI_CD.TabIndex = 50;
            this.CHIIKI_CD.Tag = "地域を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.CHIIKI_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.CHIIKI_CD.ZeroPaddengFlag = true;
            // 
            // HOUKOKU_SHO_BUNRUI_CD
            // 
            this.HOUKOKU_SHO_BUNRUI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.HOUKOKU_SHO_BUNRUI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HOUKOKU_SHO_BUNRUI_CD.ChangeUpperCase = true;
            this.HOUKOKU_SHO_BUNRUI_CD.CharacterLimitList = null;
            this.HOUKOKU_SHO_BUNRUI_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.HOUKOKU_SHO_BUNRUI_CD.DBFieldsName = "HOUKOKU_SHO_BUNRUI_CD";
            this.HOUKOKU_SHO_BUNRUI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.HOUKOKU_SHO_BUNRUI_CD.DisplayItemName = "報告書分類CD";
            this.HOUKOKU_SHO_BUNRUI_CD.DisplayPopUp = null;
            this.HOUKOKU_SHO_BUNRUI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOUKOKU_SHO_BUNRUI_CD.FocusOutCheckMethod")));
            this.HOUKOKU_SHO_BUNRUI_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HOUKOKU_SHO_BUNRUI_CD.ForeColor = System.Drawing.Color.Black;
            this.HOUKOKU_SHO_BUNRUI_CD.GetCodeMasterField = "HOUKOKUSHO_BUNRUI_CD,HOUKOKUSHO_BUNRUI_NAME_RYAKU";
            this.HOUKOKU_SHO_BUNRUI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.HOUKOKU_SHO_BUNRUI_CD.IsInputErrorOccured = false;
            this.HOUKOKU_SHO_BUNRUI_CD.ItemDefinedTypes = "varchar";
            this.HOUKOKU_SHO_BUNRUI_CD.Location = new System.Drawing.Point(115, 91);
            this.HOUKOKU_SHO_BUNRUI_CD.MaxLength = 6;
            this.HOUKOKU_SHO_BUNRUI_CD.Name = "HOUKOKU_SHO_BUNRUI_CD";
            this.HOUKOKU_SHO_BUNRUI_CD.PopupAfterExecute = null;
            this.HOUKOKU_SHO_BUNRUI_CD.PopupAfterExecuteMethod = "";
            this.HOUKOKU_SHO_BUNRUI_CD.PopupBeforeExecute = null;
            this.HOUKOKU_SHO_BUNRUI_CD.PopupBeforeExecuteMethod = "";
            this.HOUKOKU_SHO_BUNRUI_CD.PopupGetMasterField = "HOUKOKUSHO_BUNRUI_CD,HOUKOKUSHO_BUNRUI_NAME_RYAKU";
            this.HOUKOKU_SHO_BUNRUI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HOUKOKU_SHO_BUNRUI_CD.PopupSearchSendParams")));
            this.HOUKOKU_SHO_BUNRUI_CD.PopupSetFormField = "HOUKOKU_SHO_BUNRUI_CD,HOUKOKU_SHO_BUNRUI_NAME";
            this.HOUKOKU_SHO_BUNRUI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_HOUKOKUSHO_BUNRUI;
            this.HOUKOKU_SHO_BUNRUI_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.HOUKOKU_SHO_BUNRUI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HOUKOKU_SHO_BUNRUI_CD.popupWindowSetting")));
            this.HOUKOKU_SHO_BUNRUI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOUKOKU_SHO_BUNRUI_CD.RegistCheckMethod")));
            this.HOUKOKU_SHO_BUNRUI_CD.SetFormField = "HOUKOKU_SHO_BUNRUI_CD,HOUKOKU_SHO_BUNRUI_NAME";
            this.HOUKOKU_SHO_BUNRUI_CD.ShortItemName = "報告書分類CD";
            this.HOUKOKU_SHO_BUNRUI_CD.Size = new System.Drawing.Size(55, 20);
            this.HOUKOKU_SHO_BUNRUI_CD.TabIndex = 60;
            this.HOUKOKU_SHO_BUNRUI_CD.Tag = "報告書分類を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.HOUKOKU_SHO_BUNRUI_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.HOUKOKU_SHO_BUNRUI_CD.ZeroPaddengFlag = true;
            // 
            // HOUKOKU_SHO_BUNRUI_NAME
            // 
            this.HOUKOKU_SHO_BUNRUI_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.HOUKOKU_SHO_BUNRUI_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HOUKOKU_SHO_BUNRUI_NAME.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.HOUKOKU_SHO_BUNRUI_NAME.DBFieldsName = "";
            this.HOUKOKU_SHO_BUNRUI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.HOUKOKU_SHO_BUNRUI_NAME.DisplayItemName = "";
            this.HOUKOKU_SHO_BUNRUI_NAME.DisplayPopUp = null;
            this.HOUKOKU_SHO_BUNRUI_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOUKOKU_SHO_BUNRUI_NAME.FocusOutCheckMethod")));
            this.HOUKOKU_SHO_BUNRUI_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HOUKOKU_SHO_BUNRUI_NAME.ForeColor = System.Drawing.Color.Black;
            this.HOUKOKU_SHO_BUNRUI_NAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.HOUKOKU_SHO_BUNRUI_NAME.IsInputErrorOccured = false;
            this.HOUKOKU_SHO_BUNRUI_NAME.ItemDefinedTypes = "";
            this.HOUKOKU_SHO_BUNRUI_NAME.Location = new System.Drawing.Point(169, 91);
            this.HOUKOKU_SHO_BUNRUI_NAME.MaxLength = 40;
            this.HOUKOKU_SHO_BUNRUI_NAME.Name = "HOUKOKU_SHO_BUNRUI_NAME";
            this.HOUKOKU_SHO_BUNRUI_NAME.PopupAfterExecute = null;
            this.HOUKOKU_SHO_BUNRUI_NAME.PopupBeforeExecute = null;
            this.HOUKOKU_SHO_BUNRUI_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HOUKOKU_SHO_BUNRUI_NAME.PopupSearchSendParams")));
            this.HOUKOKU_SHO_BUNRUI_NAME.PopupSetFormField = "";
            this.HOUKOKU_SHO_BUNRUI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HOUKOKU_SHO_BUNRUI_NAME.PopupWindowName = "";
            this.HOUKOKU_SHO_BUNRUI_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HOUKOKU_SHO_BUNRUI_NAME.popupWindowSetting")));
            this.HOUKOKU_SHO_BUNRUI_NAME.ReadOnly = true;
            this.HOUKOKU_SHO_BUNRUI_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOUKOKU_SHO_BUNRUI_NAME.RegistCheckMethod")));
            this.HOUKOKU_SHO_BUNRUI_NAME.SetFormField = "";
            this.HOUKOKU_SHO_BUNRUI_NAME.ShortItemName = "";
            this.HOUKOKU_SHO_BUNRUI_NAME.Size = new System.Drawing.Size(150, 20);
            this.HOUKOKU_SHO_BUNRUI_NAME.TabIndex = 61;
            this.HOUKOKU_SHO_BUNRUI_NAME.TabStop = false;
            this.HOUKOKU_SHO_BUNRUI_NAME.Tag = "  ";
            // 
            // KYOKA_NO
            // 
            this.KYOKA_NO.BackColor = System.Drawing.SystemColors.Window;
            this.KYOKA_NO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOKA_NO.ChangeUpperCase = true;
            this.KYOKA_NO.CharacterLimitList = null;
            this.KYOKA_NO.CharactersNumber = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.KYOKA_NO.DBFieldsName = "";
            this.KYOKA_NO.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOKA_NO.DisplayItemName = "許可番号";
            this.KYOKA_NO.DisplayPopUp = null;
            this.KYOKA_NO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOKA_NO.FocusOutCheckMethod")));
            this.KYOKA_NO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KYOKA_NO.ForeColor = System.Drawing.Color.Black;
            this.KYOKA_NO.GetCodeMasterField = "";
            this.KYOKA_NO.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.KYOKA_NO.IsInputErrorOccured = false;
            this.KYOKA_NO.ItemDefinedTypes = "varchar";
            this.KYOKA_NO.Location = new System.Drawing.Point(844, 25);
            this.KYOKA_NO.MaxLength = 11;
            this.KYOKA_NO.Name = "KYOKA_NO";
            this.KYOKA_NO.PopupAfterExecute = null;
            this.KYOKA_NO.PopupAfterExecuteMethod = "";
            this.KYOKA_NO.PopupBeforeExecute = null;
            this.KYOKA_NO.PopupBeforeExecuteMethod = "";
            this.KYOKA_NO.PopupGetMasterField = "";
            this.KYOKA_NO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOKA_NO.PopupSearchSendParams")));
            this.KYOKA_NO.PopupSetFormField = "";
            this.KYOKA_NO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOKA_NO.PopupWindowName = "";
            this.KYOKA_NO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOKA_NO.popupWindowSetting")));
            this.KYOKA_NO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOKA_NO.RegistCheckMethod")));
            this.KYOKA_NO.SetFormField = "";
            this.KYOKA_NO.ShortItemName = "許可番号";
            this.KYOKA_NO.Size = new System.Drawing.Size(123, 20);
            this.KYOKA_NO.TabIndex = 25;
            this.KYOKA_NO.Tag = "許可番号を入力してください";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(1, 91);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(110, 20);
            this.label10.TabIndex = 659;
            this.label10.Text = "報告書分類";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(1, 69);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 20);
            this.label9.TabIndex = 658;
            this.label9.Text = "地域";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GENBA_SEARCH_BUTTON
            // 
            this.GENBA_SEARCH_BUTTON.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.GENBA_SEARCH_BUTTON.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.GENBA_SEARCH_BUTTON.DBFieldsName = null;
            this.GENBA_SEARCH_BUTTON.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_SEARCH_BUTTON.DisplayItemName = null;
            this.GENBA_SEARCH_BUTTON.DisplayPopUp = null;
            this.GENBA_SEARCH_BUTTON.ErrorMessage = null;
            this.GENBA_SEARCH_BUTTON.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_SEARCH_BUTTON.FocusOutCheckMethod")));
            this.GENBA_SEARCH_BUTTON.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GENBA_SEARCH_BUTTON.GetCodeMasterField = null;
            this.GENBA_SEARCH_BUTTON.Image = ((System.Drawing.Image)(resources.GetObject("GENBA_SEARCH_BUTTON.Image")));
            this.GENBA_SEARCH_BUTTON.ItemDefinedTypes = null;
            this.GENBA_SEARCH_BUTTON.LinkedSettingTextBox = null;
            this.GENBA_SEARCH_BUTTON.LinkedTextBoxs = null;
            this.GENBA_SEARCH_BUTTON.Location = new System.Drawing.Point(946, 46);
            this.GENBA_SEARCH_BUTTON.Name = "GENBA_SEARCH_BUTTON";
            this.GENBA_SEARCH_BUTTON.PopupAfterExecute = null;
            this.GENBA_SEARCH_BUTTON.PopupAfterExecuteMethod = "";
            this.GENBA_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.GENBA_SEARCH_BUTTON.PopupBeforeExecuteMethod = "";
            this.GENBA_SEARCH_BUTTON.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GENBA_CD,GENBA_NAME_RYAKU";
            this.GENBA_SEARCH_BUTTON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_SEARCH_BUTTON.PopupSearchSendParams")));
            this.GENBA_SEARCH_BUTTON.PopupSetFormField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GENBA_CD,GENBA_NAME_RYAKU";
            this.GENBA_SEARCH_BUTTON.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_SEARCH_BUTTON.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_SEARCH_BUTTON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_SEARCH_BUTTON.popupWindowSetting")));
            this.GENBA_SEARCH_BUTTON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_SEARCH_BUTTON.RegistCheckMethod")));
            this.GENBA_SEARCH_BUTTON.SearchDisplayFlag = 0;
            this.GENBA_SEARCH_BUTTON.SetFormField = null;
            this.GENBA_SEARCH_BUTTON.ShortItemName = null;
            this.GENBA_SEARCH_BUTTON.Size = new System.Drawing.Size(22, 22);
            this.GENBA_SEARCH_BUTTON.TabIndex = 42;
            this.GENBA_SEARCH_BUTTON.TabStop = false;
            this.GENBA_SEARCH_BUTTON.UseVisualStyleBackColor = false;
            this.GENBA_SEARCH_BUTTON.ZeroPaddengFlag = false;
            // 
            // GYOUSHA_SEARCH_BUTTON
            // 
            this.GYOUSHA_SEARCH_BUTTON.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.GYOUSHA_SEARCH_BUTTON.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.GYOUSHA_SEARCH_BUTTON.DBFieldsName = null;
            this.GYOUSHA_SEARCH_BUTTON.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_SEARCH_BUTTON.DisplayItemName = null;
            this.GYOUSHA_SEARCH_BUTTON.DisplayPopUp = null;
            this.GYOUSHA_SEARCH_BUTTON.ErrorMessage = null;
            this.GYOUSHA_SEARCH_BUTTON.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.FocusOutCheckMethod")));
            this.GYOUSHA_SEARCH_BUTTON.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GYOUSHA_SEARCH_BUTTON.GetCodeMasterField = null;
            this.GYOUSHA_SEARCH_BUTTON.Image = ((System.Drawing.Image)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.Image")));
            this.GYOUSHA_SEARCH_BUTTON.ItemDefinedTypes = null;
            this.GYOUSHA_SEARCH_BUTTON.LinkedSettingTextBox = null;
            this.GYOUSHA_SEARCH_BUTTON.LinkedTextBoxs = null;
            this.GYOUSHA_SEARCH_BUTTON.Location = new System.Drawing.Point(457, 46);
            this.GYOUSHA_SEARCH_BUTTON.Name = "GYOUSHA_SEARCH_BUTTON";
            this.GYOUSHA_SEARCH_BUTTON.PopupAfterExecute = null;
            this.GYOUSHA_SEARCH_BUTTON.PopupAfterExecuteMethod = "PopupAfterGyoushaCode";
            this.GYOUSHA_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.GYOUSHA_SEARCH_BUTTON.PopupBeforeExecuteMethod = "PopupBeforeGyoushaCode";
            this.GYOUSHA_SEARCH_BUTTON.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams")));
            this.GYOUSHA_SEARCH_BUTTON.PopupSetFormField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_SEARCH_BUTTON.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_SEARCH_BUTTON.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_SEARCH_BUTTON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.popupWindowSetting")));
            this.GYOUSHA_SEARCH_BUTTON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.RegistCheckMethod")));
            this.GYOUSHA_SEARCH_BUTTON.SearchDisplayFlag = 0;
            this.GYOUSHA_SEARCH_BUTTON.SetFormField = null;
            this.GYOUSHA_SEARCH_BUTTON.ShortItemName = null;
            this.GYOUSHA_SEARCH_BUTTON.Size = new System.Drawing.Size(22, 22);
            this.GYOUSHA_SEARCH_BUTTON.TabIndex = 32;
            this.GYOUSHA_SEARCH_BUTTON.TabStop = false;
            this.GYOUSHA_SEARCH_BUTTON.UseVisualStyleBackColor = false;
            this.GYOUSHA_SEARCH_BUTTON.ZeroPaddengFlag = false;
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
            this.GENBA_CD.DisplayItemName = "現場CD";
            this.GENBA_CD.DisplayPopUp = null;
            this.GENBA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.FocusOutCheckMethod")));
            this.GENBA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GENBA_CD.ForeColor = System.Drawing.Color.Black;
            this.GENBA_CD.GetCodeMasterField = "GENBA_CD,GENBA_NAME_RYAKU";
            this.GENBA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GENBA_CD.IsInputErrorOccured = false;
            this.GENBA_CD.ItemDefinedTypes = "varchar";
            this.GENBA_CD.Location = new System.Drawing.Point(604, 47);
            this.GENBA_CD.MaxLength = 6;
            this.GENBA_CD.Name = "GENBA_CD";
            this.GENBA_CD.PopupAfterExecute = null;
            this.GENBA_CD.PopupAfterExecuteMethod = "";
            this.GENBA_CD.PopupBeforeExecute = null;
            this.GENBA_CD.PopupBeforeExecuteMethod = "";
            this.GENBA_CD.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GENBA_CD,GENBA_NAME_RYAKU";
            this.GENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_CD.PopupSearchSendParams")));
            this.GENBA_CD.PopupSetFormField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GENBA_CD,GENBA_NAME_RYAKU";
            this.GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_CD.popupWindowSetting")));
            this.GENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.RegistCheckMethod")));
            this.GENBA_CD.SetFormField = "GENBA_CD,GENBA_NAME_RYAKU";
            this.GENBA_CD.ShortItemName = "現場CD";
            this.GENBA_CD.Size = new System.Drawing.Size(55, 20);
            this.GENBA_CD.TabIndex = 40;
            this.GENBA_CD.Tag = "現場を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GENBA_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.GENBA_CD.ZeroPaddengFlag = true;
            this.GENBA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.GENBA_CD_Validating);
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
            this.GYOUSHA_CD.DisplayItemName = "業者CD";
            this.GYOUSHA_CD.DisplayPopUp = null;
            this.GYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.FocusOutCheckMethod")));
            this.GYOUSHA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_CD.GetCodeMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GYOUSHA_CD.IsInputErrorOccured = false;
            this.GYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.GYOUSHA_CD.Location = new System.Drawing.Point(115, 47);
            this.GYOUSHA_CD.MaxLength = 6;
            this.GYOUSHA_CD.Name = "GYOUSHA_CD";
            this.GYOUSHA_CD.PopupAfterExecute = null;
            this.GYOUSHA_CD.PopupAfterExecuteMethod = "PopupAfterGyoushaCode";
            this.GYOUSHA_CD.PopupBeforeExecute = null;
            this.GYOUSHA_CD.PopupBeforeExecuteMethod = "PopupBeforeGyoushaCode";
            this.GYOUSHA_CD.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_CD.PopupSearchSendParams")));
            this.GYOUSHA_CD.PopupSetFormField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_CD.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_CD.popupWindowSetting")));
            this.GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.RegistCheckMethod")));
            this.GYOUSHA_CD.SetFormField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.ShortItemName = "業者CD";
            this.GYOUSHA_CD.Size = new System.Drawing.Size(55, 20);
            this.GYOUSHA_CD.TabIndex = 30;
            this.GYOUSHA_CD.Tag = "業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GYOUSHA_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.GYOUSHA_CD.ZeroPaddengFlag = true;
            this.GYOUSHA_CD.Enter += new System.EventHandler(this.GYOUSHA_CD_Enter);
            this.GYOUSHA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.GYOUSHA_CD_Validating);
            // 
            // GYOUSHA_NAME_RYAKU
            // 
            this.GYOUSHA_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GYOUSHA_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.GYOUSHA_NAME_RYAKU.DBFieldsName = "GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_NAME_RYAKU.DisplayItemName = "業者略称名";
            this.GYOUSHA_NAME_RYAKU.DisplayPopUp = null;
            this.GYOUSHA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.FocusOutCheckMethod")));
            this.GYOUSHA_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GYOUSHA_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_NAME_RYAKU.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.GYOUSHA_NAME_RYAKU.IsInputErrorOccured = false;
            this.GYOUSHA_NAME_RYAKU.ItemDefinedTypes = "";
            this.GYOUSHA_NAME_RYAKU.Location = new System.Drawing.Point(169, 47);
            this.GYOUSHA_NAME_RYAKU.MaxLength = 40;
            this.GYOUSHA_NAME_RYAKU.Name = "GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_NAME_RYAKU.PopupAfterExecute = null;
            this.GYOUSHA_NAME_RYAKU.PopupBeforeExecute = null;
            this.GYOUSHA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.PopupSearchSendParams")));
            this.GYOUSHA_NAME_RYAKU.PopupSetFormField = "";
            this.GYOUSHA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_NAME_RYAKU.PopupWindowName = "";
            this.GYOUSHA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.popupWindowSetting")));
            this.GYOUSHA_NAME_RYAKU.ReadOnly = true;
            this.GYOUSHA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.RegistCheckMethod")));
            this.GYOUSHA_NAME_RYAKU.SetFormField = "";
            this.GYOUSHA_NAME_RYAKU.ShortItemName = "業者略称名";
            this.GYOUSHA_NAME_RYAKU.Size = new System.Drawing.Size(285, 20);
            this.GYOUSHA_NAME_RYAKU.TabIndex = 31;
            this.GYOUSHA_NAME_RYAKU.TabStop = false;
            this.GYOUSHA_NAME_RYAKU.Tag = "  ";
            // 
            // GENBA_NAME_RYAKU
            // 
            this.GENBA_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GENBA_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.GENBA_NAME_RYAKU.DBFieldsName = "GENBA_NAME_RYAKU";
            this.GENBA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_NAME_RYAKU.DisplayItemName = "現場略称名";
            this.GENBA_NAME_RYAKU.DisplayPopUp = null;
            this.GENBA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_RYAKU.FocusOutCheckMethod")));
            this.GENBA_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GENBA_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.GENBA_NAME_RYAKU.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.GENBA_NAME_RYAKU.IsInputErrorOccured = false;
            this.GENBA_NAME_RYAKU.ItemDefinedTypes = "";
            this.GENBA_NAME_RYAKU.Location = new System.Drawing.Point(658, 47);
            this.GENBA_NAME_RYAKU.MaxLength = 40;
            this.GENBA_NAME_RYAKU.Name = "GENBA_NAME_RYAKU";
            this.GENBA_NAME_RYAKU.PopupAfterExecute = null;
            this.GENBA_NAME_RYAKU.PopupBeforeExecute = null;
            this.GENBA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_NAME_RYAKU.PopupSearchSendParams")));
            this.GENBA_NAME_RYAKU.PopupSetFormField = "";
            this.GENBA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_NAME_RYAKU.PopupWindowName = "";
            this.GENBA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_NAME_RYAKU.popupWindowSetting")));
            this.GENBA_NAME_RYAKU.ReadOnly = true;
            this.GENBA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_RYAKU.RegistCheckMethod")));
            this.GENBA_NAME_RYAKU.SetFormField = "";
            this.GENBA_NAME_RYAKU.ShortItemName = "現場略称名";
            this.GENBA_NAME_RYAKU.Size = new System.Drawing.Size(285, 20);
            this.GENBA_NAME_RYAKU.TabIndex = 41;
            this.GENBA_NAME_RYAKU.TabStop = false;
            this.GENBA_NAME_RYAKU.Tag = "  ";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(1, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 20);
            this.label6.TabIndex = 654;
            this.label6.Text = "業者";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(490, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 20);
            this.label8.TabIndex = 655;
            this.label8.Text = "現場";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ICHIRAN_HYOUJI_JOUKEN_DELETED
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Location = new System.Drawing.Point(606, 93);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Name = "ICHIRAN_HYOUJI_JOUKEN_DELETED";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Size = new System.Drawing.Size(156, 16);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.TabIndex = 70;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Text = "削除済みも含めて全て表示";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(492, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.TabIndex = 401;
            this.label3.Text = "表示条件";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.HIDUKE_FROM.DisplayItemName = "開始日";
            this.HIDUKE_FROM.DisplayPopUp = null;
            this.HIDUKE_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_FROM.FocusOutCheckMethod")));
            this.HIDUKE_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HIDUKE_FROM.ForeColor = System.Drawing.Color.Black;
            this.HIDUKE_FROM.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.HIDUKE_FROM.GetCodeMasterField = "HIDUKE_FROM,HIDUKE_TO";
            this.HIDUKE_FROM.IsInputErrorOccured = false;
            this.HIDUKE_FROM.Location = new System.Drawing.Point(403, 25);
            this.HIDUKE_FROM.MaxLength = 10;
            this.HIDUKE_FROM.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.HIDUKE_FROM.Name = "HIDUKE_FROM";
            this.HIDUKE_FROM.NullValue = "";
            this.HIDUKE_FROM.PopupAfterExecute = null;
            this.HIDUKE_FROM.PopupBeforeExecute = null;
            this.HIDUKE_FROM.PopupGetMasterField = "HIDUKE_FROM,HIDUKE_TO";
            this.HIDUKE_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIDUKE_FROM.PopupSearchSendParams")));
            this.HIDUKE_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIDUKE_FROM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HIDUKE_FROM.popupWindowSetting")));
            this.HIDUKE_FROM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_FROM.RegistCheckMethod")));
            this.HIDUKE_FROM.Size = new System.Drawing.Size(110, 20);
            this.HIDUKE_FROM.TabIndex = 23;
            this.HIDUKE_FROM.Tag = "許可証有効期限を入力してください";
            this.HIDUKE_FROM.Value = null;
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
            this.HIDUKE_TO.DisplayItemName = "終了日";
            this.HIDUKE_TO.DisplayPopUp = null;
            this.HIDUKE_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_TO.FocusOutCheckMethod")));
            this.HIDUKE_TO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HIDUKE_TO.ForeColor = System.Drawing.Color.Black;
            this.HIDUKE_TO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.HIDUKE_TO.GetCodeMasterField = "HIDUKE_FROM,HIDUKE_TO";
            this.HIDUKE_TO.IsInputErrorOccured = false;
            this.HIDUKE_TO.Location = new System.Drawing.Point(539, 25);
            this.HIDUKE_TO.MaxLength = 10;
            this.HIDUKE_TO.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.HIDUKE_TO.Name = "HIDUKE_TO";
            this.HIDUKE_TO.NullValue = "";
            this.HIDUKE_TO.PopupAfterExecute = null;
            this.HIDUKE_TO.PopupBeforeExecute = null;
            this.HIDUKE_TO.PopupGetMasterField = "HIDUKE_FROM,HIDUKE_TO";
            this.HIDUKE_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIDUKE_TO.PopupSearchSendParams")));
            this.HIDUKE_TO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIDUKE_TO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HIDUKE_TO.popupWindowSetting")));
            this.HIDUKE_TO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_TO.RegistCheckMethod")));
            this.HIDUKE_TO.Size = new System.Drawing.Size(110, 20);
            this.HIDUKE_TO.TabIndex = 24;
            this.HIDUKE_TO.Tag = "許可証有効期限を入力してください";
            this.HIDUKE_TO.Value = null;
            this.HIDUKE_TO.DoubleClick += new System.EventHandler(this.HIDUKE_TO_DoubleClick);
            this.HIDUKE_TO.Leave += new System.EventHandler(this.HIDUKE_TO_Leave);
            // 
            // lab_HidukeNyuuryoku
            // 
            this.lab_HidukeNyuuryoku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lab_HidukeNyuuryoku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lab_HidukeNyuuryoku.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lab_HidukeNyuuryoku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lab_HidukeNyuuryoku.ForeColor = System.Drawing.Color.White;
            this.lab_HidukeNyuuryoku.Location = new System.Drawing.Point(730, 25);
            this.lab_HidukeNyuuryoku.Name = "lab_HidukeNyuuryoku";
            this.lab_HidukeNyuuryoku.Size = new System.Drawing.Size(110, 20);
            this.lab_HidukeNyuuryoku.TabIndex = 398;
            this.lab_HidukeNyuuryoku.Text = "許可番号";
            this.lab_HidukeNyuuryoku.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(518, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 399;
            this.label2.Text = "～";
            // 
            // KIKAN_KBN
            // 
            this.KIKAN_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.KIKAN_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KIKAN_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.KIKAN_KBN.DisplayItemName = "有効期限";
            this.KIKAN_KBN.DisplayPopUp = null;
            this.KIKAN_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KIKAN_KBN.FocusOutCheckMethod")));
            this.KIKAN_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KIKAN_KBN.ForeColor = System.Drawing.Color.Black;
            this.KIKAN_KBN.IsInputErrorOccured = false;
            this.KIKAN_KBN.LinkedRadioButtonArray = new string[] {
        "KIKAN_KBN1",
        "KIKAN_KBN2"};
            this.KIKAN_KBN.Location = new System.Drawing.Point(115, 25);
            this.KIKAN_KBN.Name = "KIKAN_KBN";
            this.KIKAN_KBN.PopupAfterExecute = null;
            this.KIKAN_KBN.PopupBeforeExecute = null;
            this.KIKAN_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KIKAN_KBN.PopupSearchSendParams")));
            this.KIKAN_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KIKAN_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KIKAN_KBN.popupWindowSetting")));
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
            this.KIKAN_KBN.RangeSetting = rangeSettingDto1;
            this.KIKAN_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KIKAN_KBN.RegistCheckMethod")));
            this.KIKAN_KBN.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.KIKAN_KBN.Size = new System.Drawing.Size(20, 20);
            this.KIKAN_KBN.TabIndex = 20;
            this.KIKAN_KBN.Tag = "【1、2】 のいずれかで入力してください";
            this.KIKAN_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.KIKAN_KBN.WordWrap = false;
            // 
            // KYOKA_KBN
            // 
            this.KYOKA_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.KYOKA_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOKA_KBN.CharacterLimitList = new char[] {
        '1',
        '2',
        '3',
        '9'};
            this.KYOKA_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOKA_KBN.DisplayItemName = "許可区分";
            this.KYOKA_KBN.DisplayPopUp = null;
            this.KYOKA_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOKA_KBN.FocusOutCheckMethod")));
            this.KYOKA_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOKA_KBN.ForeColor = System.Drawing.Color.Black;
            this.KYOKA_KBN.IsInputErrorOccured = false;
            this.KYOKA_KBN.LinkedRadioButtonArray = new string[] {
        "KYOKA_KBN1",
        "KYOKA_KBN2",
        "KYOKA_KBN3",
        "KYOKA_KBN4"};
            this.KYOKA_KBN.Location = new System.Drawing.Point(115, 3);
            this.KYOKA_KBN.Name = "KYOKA_KBN";
            this.KYOKA_KBN.PopupAfterExecute = null;
            this.KYOKA_KBN.PopupBeforeExecute = null;
            this.KYOKA_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOKA_KBN.PopupSearchSendParams")));
            this.KYOKA_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOKA_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOKA_KBN.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            9,
            0,
            0,
            0});
            rangeSettingDto2.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.KYOKA_KBN.RangeSetting = rangeSettingDto2;
            this.KYOKA_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOKA_KBN.RegistCheckMethod")));
            this.KYOKA_KBN.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.KYOKA_KBN.Size = new System.Drawing.Size(20, 20);
            this.KYOKA_KBN.TabIndex = 1;
            this.KYOKA_KBN.Tag = "【1、2、３、９】 のいずれかで入力してください";
            this.KYOKA_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.KYOKA_KBN.WordWrap = false;
            this.KYOKA_KBN.TextChanged += new System.EventHandler(this.KYOKA_KBN_TextChanged);
            // 
            // customPanel3
            // 
            this.customPanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel3.Controls.Add(this.KIKAN_KBN1);
            this.customPanel3.Controls.Add(this.KIKAN_KBN2);
            this.customPanel3.Location = new System.Drawing.Point(134, 25);
            this.customPanel3.Name = "customPanel3";
            this.customPanel3.Size = new System.Drawing.Size(265, 20);
            this.customPanel3.TabIndex = 2;
            this.customPanel3.TabStop = true;
            // 
            // KIKAN_KBN1
            // 
            this.KIKAN_KBN1.AutoSize = true;
            this.KIKAN_KBN1.DefaultBackColor = System.Drawing.Color.Empty;
            this.KIKAN_KBN1.DisplayItemName = "";
            this.KIKAN_KBN1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KIKAN_KBN1.FocusOutCheckMethod")));
            this.KIKAN_KBN1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KIKAN_KBN1.LinkedTextBox = "KIKAN_KBN";
            this.KIKAN_KBN1.Location = new System.Drawing.Point(2, 0);
            this.KIKAN_KBN1.Name = "KIKAN_KBN1";
            this.KIKAN_KBN1.PopupAfterExecute = null;
            this.KIKAN_KBN1.PopupBeforeExecute = null;
            this.KIKAN_KBN1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KIKAN_KBN1.PopupSearchSendParams")));
            this.KIKAN_KBN1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KIKAN_KBN1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KIKAN_KBN1.popupWindowSetting")));
            this.KIKAN_KBN1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KIKAN_KBN1.RegistCheckMethod")));
            this.KIKAN_KBN1.Size = new System.Drawing.Size(109, 17);
            this.KIKAN_KBN1.TabIndex = 21;
            this.KIKAN_KBN1.Tag = "抽出日付を選択してください";
            this.KIKAN_KBN1.Text = "1.開始日検索";
            this.KIKAN_KBN1.UseVisualStyleBackColor = true;
            this.KIKAN_KBN1.Value = "1";
            // 
            // KIKAN_KBN2
            // 
            this.KIKAN_KBN2.AutoSize = true;
            this.KIKAN_KBN2.DefaultBackColor = System.Drawing.Color.Empty;
            this.KIKAN_KBN2.DisplayItemName = "";
            this.KIKAN_KBN2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KIKAN_KBN2.FocusOutCheckMethod")));
            this.KIKAN_KBN2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KIKAN_KBN2.LinkedTextBox = "KIKAN_KBN";
            this.KIKAN_KBN2.Location = new System.Drawing.Point(131, 0);
            this.KIKAN_KBN2.Name = "KIKAN_KBN2";
            this.KIKAN_KBN2.PopupAfterExecute = null;
            this.KIKAN_KBN2.PopupBeforeExecute = null;
            this.KIKAN_KBN2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KIKAN_KBN2.PopupSearchSendParams")));
            this.KIKAN_KBN2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KIKAN_KBN2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KIKAN_KBN2.popupWindowSetting")));
            this.KIKAN_KBN2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KIKAN_KBN2.RegistCheckMethod")));
            this.KIKAN_KBN2.Size = new System.Drawing.Size(109, 17);
            this.KIKAN_KBN2.TabIndex = 22;
            this.KIKAN_KBN2.Tag = "抽出日付を選択してください";
            this.KIKAN_KBN2.Text = "2.終了日検索";
            this.KIKAN_KBN2.UseVisualStyleBackColor = true;
            this.KIKAN_KBN2.Value = "2";
            // 
            // customPanel2
            // 
            this.customPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel2.Controls.Add(this.KYOKA_KBN4);
            this.customPanel2.Controls.Add(this.KYOKA_KBN1);
            this.customPanel2.Controls.Add(this.KYOKA_KBN2);
            this.customPanel2.Controls.Add(this.KYOKA_KBN3);
            this.customPanel2.Location = new System.Drawing.Point(134, 3);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(410, 20);
            this.customPanel2.TabIndex = 2;
            this.customPanel2.TabStop = true;
            // 
            // KYOKA_KBN4
            // 
            this.KYOKA_KBN4.AutoSize = true;
            this.KYOKA_KBN4.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOKA_KBN4.DisplayItemName = "";
            this.KYOKA_KBN4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOKA_KBN4.FocusOutCheckMethod")));
            this.KYOKA_KBN4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOKA_KBN4.LinkedTextBox = "KYOKA_KBN";
            this.KYOKA_KBN4.Location = new System.Drawing.Point(320, 1);
            this.KYOKA_KBN4.Name = "KYOKA_KBN4";
            this.KYOKA_KBN4.PopupAfterExecute = null;
            this.KYOKA_KBN4.PopupBeforeExecute = null;
            this.KYOKA_KBN4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOKA_KBN4.PopupSearchSendParams")));
            this.KYOKA_KBN4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOKA_KBN4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOKA_KBN4.popupWindowSetting")));
            this.KYOKA_KBN4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOKA_KBN4.RegistCheckMethod")));
            this.KYOKA_KBN4.Size = new System.Drawing.Size(67, 17);
            this.KYOKA_KBN4.TabIndex = 5;
            this.KYOKA_KBN4.Tag = "全ての許可証を検索する場合、チェックを付けてください";
            this.KYOKA_KBN4.Text = "9.全て";
            this.KYOKA_KBN4.UseVisualStyleBackColor = true;
            this.KYOKA_KBN4.Value = "9";
            // 
            // KYOKA_KBN1
            // 
            this.KYOKA_KBN1.AutoSize = true;
            this.KYOKA_KBN1.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOKA_KBN1.DisplayItemName = "";
            this.KYOKA_KBN1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOKA_KBN1.FocusOutCheckMethod")));
            this.KYOKA_KBN1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOKA_KBN1.LinkedTextBox = "KYOKA_KBN";
            this.KYOKA_KBN1.Location = new System.Drawing.Point(2, 0);
            this.KYOKA_KBN1.Name = "KYOKA_KBN1";
            this.KYOKA_KBN1.PopupAfterExecute = null;
            this.KYOKA_KBN1.PopupBeforeExecute = null;
            this.KYOKA_KBN1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOKA_KBN1.PopupSearchSendParams")));
            this.KYOKA_KBN1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOKA_KBN1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOKA_KBN1.popupWindowSetting")));
            this.KYOKA_KBN1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOKA_KBN1.RegistCheckMethod")));
            this.KYOKA_KBN1.Size = new System.Drawing.Size(67, 17);
            this.KYOKA_KBN1.TabIndex = 2;
            this.KYOKA_KBN1.Tag = "運搬許可証を検索する場合、チェックを付けてください";
            this.KYOKA_KBN1.Text = "1.運搬";
            this.KYOKA_KBN1.UseVisualStyleBackColor = true;
            this.KYOKA_KBN1.Value = "1";
            // 
            // KYOKA_KBN2
            // 
            this.KYOKA_KBN2.AutoSize = true;
            this.KYOKA_KBN2.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOKA_KBN2.DisplayItemName = "";
            this.KYOKA_KBN2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOKA_KBN2.FocusOutCheckMethod")));
            this.KYOKA_KBN2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOKA_KBN2.LinkedTextBox = "KYOKA_KBN";
            this.KYOKA_KBN2.Location = new System.Drawing.Point(90, 0);
            this.KYOKA_KBN2.Name = "KYOKA_KBN2";
            this.KYOKA_KBN2.PopupAfterExecute = null;
            this.KYOKA_KBN2.PopupBeforeExecute = null;
            this.KYOKA_KBN2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOKA_KBN2.PopupSearchSendParams")));
            this.KYOKA_KBN2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOKA_KBN2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOKA_KBN2.popupWindowSetting")));
            this.KYOKA_KBN2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOKA_KBN2.RegistCheckMethod")));
            this.KYOKA_KBN2.Size = new System.Drawing.Size(95, 17);
            this.KYOKA_KBN2.TabIndex = 3;
            this.KYOKA_KBN2.Tag = "処分許可証を検索する場合、チェックを付けてください";
            this.KYOKA_KBN2.Text = "2.中間処分";
            this.KYOKA_KBN2.UseVisualStyleBackColor = true;
            this.KYOKA_KBN2.Value = "2";
            // 
            // KYOKA_KBN3
            // 
            this.KYOKA_KBN3.AutoSize = true;
            this.KYOKA_KBN3.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOKA_KBN3.DisplayItemName = "";
            this.KYOKA_KBN3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOKA_KBN3.FocusOutCheckMethod")));
            this.KYOKA_KBN3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOKA_KBN3.LinkedTextBox = "KYOKA_KBN";
            this.KYOKA_KBN3.Location = new System.Drawing.Point(205, 0);
            this.KYOKA_KBN3.Name = "KYOKA_KBN3";
            this.KYOKA_KBN3.PopupAfterExecute = null;
            this.KYOKA_KBN3.PopupBeforeExecute = null;
            this.KYOKA_KBN3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOKA_KBN3.PopupSearchSendParams")));
            this.KYOKA_KBN3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOKA_KBN3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOKA_KBN3.popupWindowSetting")));
            this.KYOKA_KBN3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOKA_KBN3.RegistCheckMethod")));
            this.KYOKA_KBN3.Size = new System.Drawing.Size(95, 17);
            this.KYOKA_KBN3.TabIndex = 4;
            this.KYOKA_KBN3.Tag = "最終処分許可証を検索する場合、チェックを付けてください";
            this.KYOKA_KBN3.Text = "3.最終処分";
            this.KYOKA_KBN3.UseVisualStyleBackColor = true;
            this.KYOKA_KBN3.Value = "3";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(1, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "有効期限";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FUTSU_KBN
            // 
            this.FUTSU_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.FUTSU_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FUTSU_KBN.CharacterLimitList = new char[] {
        '1',
        '2',
        '9'};
            this.FUTSU_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.FUTSU_KBN.DisplayItemName = "普通/特管";
            this.FUTSU_KBN.DisplayPopUp = null;
            this.FUTSU_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FUTSU_KBN.FocusOutCheckMethod")));
            this.FUTSU_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.FUTSU_KBN.ForeColor = System.Drawing.Color.Black;
            this.FUTSU_KBN.IsInputErrorOccured = false;
            this.FUTSU_KBN.LinkedRadioButtonArray = new string[] {
        "FUTSU_KBN1",
        "FUTSU_KBN2",
        "FUTSU_KBN3"};
            this.FUTSU_KBN.Location = new System.Drawing.Point(678, 3);
            this.FUTSU_KBN.Name = "FUTSU_KBN";
            this.FUTSU_KBN.PopupAfterExecute = null;
            this.FUTSU_KBN.PopupBeforeExecute = null;
            this.FUTSU_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("FUTSU_KBN.PopupSearchSendParams")));
            this.FUTSU_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.FUTSU_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("FUTSU_KBN.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            9,
            0,
            0,
            0});
            rangeSettingDto3.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FUTSU_KBN.RangeSetting = rangeSettingDto3;
            this.FUTSU_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FUTSU_KBN.RegistCheckMethod")));
            this.FUTSU_KBN.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.FUTSU_KBN.Size = new System.Drawing.Size(20, 20);
            this.FUTSU_KBN.TabIndex = 10;
            this.FUTSU_KBN.Tag = "【1、2、９】 のいずれかで入力してください";
            this.FUTSU_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.FUTSU_KBN.WordWrap = false;
            // 
            // customPanel4
            // 
            this.customPanel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel4.Controls.Add(this.FUTSU_KBN1);
            this.customPanel4.Controls.Add(this.FUTSU_KBN2);
            this.customPanel4.Controls.Add(this.FUTSU_KBN3);
            this.customPanel4.Location = new System.Drawing.Point(697, 3);
            this.customPanel4.Name = "customPanel4";
            this.customPanel4.Size = new System.Drawing.Size(270, 20);
            this.customPanel4.TabIndex = 660;
            this.customPanel4.TabStop = true;
            // 
            // FUTSU_KBN1
            // 
            this.FUTSU_KBN1.AutoSize = true;
            this.FUTSU_KBN1.DefaultBackColor = System.Drawing.Color.Empty;
            this.FUTSU_KBN1.DisplayItemName = "";
            this.FUTSU_KBN1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FUTSU_KBN1.FocusOutCheckMethod")));
            this.FUTSU_KBN1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.FUTSU_KBN1.LinkedTextBox = "FUTSU_KBN";
            this.FUTSU_KBN1.Location = new System.Drawing.Point(2, 0);
            this.FUTSU_KBN1.Name = "FUTSU_KBN1";
            this.FUTSU_KBN1.PopupAfterExecute = null;
            this.FUTSU_KBN1.PopupBeforeExecute = null;
            this.FUTSU_KBN1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("FUTSU_KBN1.PopupSearchSendParams")));
            this.FUTSU_KBN1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.FUTSU_KBN1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("FUTSU_KBN1.popupWindowSetting")));
            this.FUTSU_KBN1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FUTSU_KBN1.RegistCheckMethod")));
            this.FUTSU_KBN1.Size = new System.Drawing.Size(67, 17);
            this.FUTSU_KBN1.TabIndex = 11;
            this.FUTSU_KBN1.Tag = "普通産廃許可証を検索する場合、チェックを付けてください";
            this.FUTSU_KBN1.Text = "1.普通";
            this.FUTSU_KBN1.UseVisualStyleBackColor = true;
            this.FUTSU_KBN1.Value = "1";
            // 
            // FUTSU_KBN2
            // 
            this.FUTSU_KBN2.AutoSize = true;
            this.FUTSU_KBN2.DefaultBackColor = System.Drawing.Color.Empty;
            this.FUTSU_KBN2.DisplayItemName = "";
            this.FUTSU_KBN2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FUTSU_KBN2.FocusOutCheckMethod")));
            this.FUTSU_KBN2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.FUTSU_KBN2.LinkedTextBox = "FUTSU_KBN";
            this.FUTSU_KBN2.Location = new System.Drawing.Point(94, 0);
            this.FUTSU_KBN2.Name = "FUTSU_KBN2";
            this.FUTSU_KBN2.PopupAfterExecute = null;
            this.FUTSU_KBN2.PopupBeforeExecute = null;
            this.FUTSU_KBN2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("FUTSU_KBN2.PopupSearchSendParams")));
            this.FUTSU_KBN2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.FUTSU_KBN2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("FUTSU_KBN2.popupWindowSetting")));
            this.FUTSU_KBN2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FUTSU_KBN2.RegistCheckMethod")));
            this.FUTSU_KBN2.Size = new System.Drawing.Size(67, 17);
            this.FUTSU_KBN2.TabIndex = 12;
            this.FUTSU_KBN2.Tag = "特別管理許可証を検索する場合、チェックを付けてください";
            this.FUTSU_KBN2.Text = "2.特管";
            this.FUTSU_KBN2.UseVisualStyleBackColor = true;
            this.FUTSU_KBN2.Value = "2";
            // 
            // FUTSU_KBN3
            // 
            this.FUTSU_KBN3.AutoSize = true;
            this.FUTSU_KBN3.DefaultBackColor = System.Drawing.Color.Empty;
            this.FUTSU_KBN3.DisplayItemName = "";
            this.FUTSU_KBN3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FUTSU_KBN3.FocusOutCheckMethod")));
            this.FUTSU_KBN3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.FUTSU_KBN3.LinkedTextBox = "FUTSU_KBN";
            this.FUTSU_KBN3.Location = new System.Drawing.Point(186, 0);
            this.FUTSU_KBN3.Name = "FUTSU_KBN3";
            this.FUTSU_KBN3.PopupAfterExecute = null;
            this.FUTSU_KBN3.PopupBeforeExecute = null;
            this.FUTSU_KBN3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("FUTSU_KBN3.PopupSearchSendParams")));
            this.FUTSU_KBN3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.FUTSU_KBN3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("FUTSU_KBN3.popupWindowSetting")));
            this.FUTSU_KBN3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FUTSU_KBN3.RegistCheckMethod")));
            this.FUTSU_KBN3.Size = new System.Drawing.Size(67, 17);
            this.FUTSU_KBN3.TabIndex = 13;
            this.FUTSU_KBN3.Tag = "普通/特管の両方を検索する場合、チェックを付けてください";
            this.FUTSU_KBN3.Text = "9.全て";
            this.FUTSU_KBN3.UseVisualStyleBackColor = true;
            this.FUTSU_KBN3.Value = "9";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(564, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 20);
            this.label5.TabIndex = 402;
            this.label5.Text = "普通/特管";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CHIIKI_NAME
            // 
            this.CHIIKI_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CHIIKI_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CHIIKI_NAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CHIIKI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.CHIIKI_NAME.DisplayPopUp = null;
            this.CHIIKI_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CHIIKI_NAME.FocusOutCheckMethod")));
            this.CHIIKI_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CHIIKI_NAME.ForeColor = System.Drawing.Color.Black;
            this.CHIIKI_NAME.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.CHIIKI_NAME.IsInputErrorOccured = false;
            this.CHIIKI_NAME.Location = new System.Drawing.Point(169, 69);
            this.CHIIKI_NAME.MaxLength = 0;
            this.CHIIKI_NAME.Name = "CHIIKI_NAME";
            this.CHIIKI_NAME.PopupAfterExecute = null;
            this.CHIIKI_NAME.PopupBeforeExecute = null;
            this.CHIIKI_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CHIIKI_NAME.PopupSearchSendParams")));
            this.CHIIKI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CHIIKI_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CHIIKI_NAME.popupWindowSetting")));
            this.CHIIKI_NAME.ReadOnly = true;
            this.CHIIKI_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CHIIKI_NAME.RegistCheckMethod")));
            this.CHIIKI_NAME.Size = new System.Drawing.Size(150, 20);
            this.CHIIKI_NAME.TabIndex = 51;
            this.CHIIKI_NAME.TabStop = false;
            this.CHIIKI_NAME.Tag = " ";
            // 
            // KyokaShouIchiranForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 470);
            this.Controls.Add(this.customPanel1);
            this.Name = "KyokaShouIchiranForm";
            this.SystemId = ((long)(277));
            this.Text = "UIForm";
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.customPanel1, 0);
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.customPanel3.ResumeLayout(false);
            this.customPanel3.PerformLayout();
            this.customPanel2.ResumeLayout(false);
            this.customPanel2.PerformLayout();
            this.customPanel4.ResumeLayout(false);
            this.customPanel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal Label label4;
        private r_framework.CustomControl.CustomPanel customPanel1;
        private r_framework.CustomControl.CustomPanel customPanel2;
        public r_framework.CustomControl.CustomRadioButton KYOKA_KBN1;
        public r_framework.CustomControl.CustomRadioButton KYOKA_KBN2;
        public r_framework.CustomControl.CustomNumericTextBox2 KYOKA_KBN;
        public r_framework.CustomControl.CustomRadioButton KYOKA_KBN3;
        public r_framework.CustomControl.CustomNumericTextBox2 KIKAN_KBN;
        private r_framework.CustomControl.CustomPanel customPanel3;
        public r_framework.CustomControl.CustomRadioButton KIKAN_KBN1;
        public r_framework.CustomControl.CustomRadioButton KIKAN_KBN2;
        internal Label label7;
        public r_framework.CustomControl.CustomDateTimePicker HIDUKE_FROM;
        public r_framework.CustomControl.CustomDateTimePicker HIDUKE_TO;
        internal Label lab_HidukeNyuuryoku;
        private Label label2;
        internal Label label3;
        internal CheckBox ICHIRAN_HYOUJI_JOUKEN_DELETED;
        internal Label label5;
        public r_framework.CustomControl.CustomNumericTextBox2 FUTSU_KBN;
        private r_framework.CustomControl.CustomPanel customPanel4;
        public r_framework.CustomControl.CustomRadioButton FUTSU_KBN1;
        public r_framework.CustomControl.CustomRadioButton FUTSU_KBN2;
        public r_framework.CustomControl.CustomRadioButton FUTSU_KBN3;
        internal Label label10;
        internal Label label9;
        internal r_framework.CustomControl.CustomPopupOpenButton GENBA_SEARCH_BUTTON;
        internal r_framework.CustomControl.CustomPopupOpenButton GYOUSHA_SEARCH_BUTTON;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GENBA_CD;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GYOUSHA_CD;
        internal r_framework.CustomControl.CustomTextBox GYOUSHA_NAME_RYAKU;
        internal r_framework.CustomControl.CustomTextBox GENBA_NAME_RYAKU;
        internal Label label6;
        internal Label label8;
        public r_framework.CustomControl.CustomRadioButton KYOKA_KBN4;
        internal r_framework.CustomControl.CustomAlphaNumTextBox HOUKOKU_SHO_BUNRUI_CD;
        internal r_framework.CustomControl.CustomTextBox HOUKOKU_SHO_BUNRUI_NAME;
        internal r_framework.CustomControl.CustomAlphaNumTextBox KYOKA_NO;
        internal r_framework.CustomControl.CustomAlphaNumTextBox CHIIKI_CD;
        internal r_framework.CustomControl.CustomTextBox CHIIKI_NAME;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
    }
}