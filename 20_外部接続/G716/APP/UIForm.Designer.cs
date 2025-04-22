namespace Shougun.Core.ExternalConnection.DenshiKeiyakuRirekiIchiran
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
            this.ITAKU_KEIYAKU_NO = new r_framework.CustomControl.CustomTextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.KEIYAKUSHO_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.KEIYAKUSHO_SHURUI_NAME = new r_framework.CustomControl.CustomTextBox();
            this.KEIYAKUSHO_SHURUI_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label3 = new System.Windows.Forms.Label();
            this.KEIYAKU_JYOUKYOU_NAME = new r_framework.CustomControl.CustomTextBox();
            this.KEIYAKU_JYOUKYOU_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label6 = new System.Windows.Forms.Label();
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.KYOKASHOU_SHURUI_NAME = new r_framework.CustomControl.CustomTextBox();
            this.KYOKASHOU_SHURUI_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label1 = new System.Windows.Forms.Label();
            this.DATE_SELECT_7 = new r_framework.CustomControl.CustomRadioButton();
            this.DATE_SELECT_6 = new r_framework.CustomControl.CustomRadioButton();
            this.DATE_SELECT_5 = new r_framework.CustomControl.CustomRadioButton();
            this.DATE_SELECT_4 = new r_framework.CustomControl.CustomRadioButton();
            this.DATE_SELECT_3 = new r_framework.CustomControl.CustomRadioButton();
            this.DATE_SELECT_2 = new r_framework.CustomControl.CustomRadioButton();
            this.DATE_SELECT_1 = new r_framework.CustomControl.CustomRadioButton();
            this.DATE_SELECT = new r_framework.CustomControl.CustomNumericTextBox2();
            this.labelInfo = new System.Windows.Forms.Label();
            this.DATE_TO = new r_framework.CustomControl.CustomDateTimePicker();
            this.DATE_FROM = new r_framework.CustomControl.CustomDateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.UNPANGYOUSHA_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.GENBA_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.GYOUSHA_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.UNPANGYOUSHA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.GYOUSHA_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.UNPANGYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GENBA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.GENBA_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.SHOBUN_GENBA_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.SAISHUU_SHOBUNJOU_NAME = new r_framework.CustomControl.CustomTextBox();
            this.SHOBUN_GENBA_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.SHOBUN_JYUTAKUSHA_SAISHU_NAME = new r_framework.CustomControl.CustomTextBox();
            this.SHOBUN_JYUTAKUSHA_SHOBUN_NAME = new r_framework.CustomControl.CustomTextBox();
            this.SAISHUU_SHOBUNJOU_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.SHOBUN_GENBA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.searchString.ReadOnly = true;
            this.searchString.Visible = false;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Location = new System.Drawing.Point(3, 466);
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Location = new System.Drawing.Point(204, 466);
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Location = new System.Drawing.Point(405, 466);
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Location = new System.Drawing.Point(606, 466);
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Location = new System.Drawing.Point(807, 466);
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.Location = new System.Drawing.Point(4, 172);
            // 
            // customSearchHeader1
            // 
            this.customSearchHeader1.Location = new System.Drawing.Point(6, 147);
            // 
            // ITAKU_KEIYAKU_NO
            // 
            this.ITAKU_KEIYAKU_NO.BackColor = System.Drawing.SystemColors.Window;
            this.ITAKU_KEIYAKU_NO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ITAKU_KEIYAKU_NO.DBFieldsName = "ITAKU_KEIYAKU_NO";
            this.ITAKU_KEIYAKU_NO.DefaultBackColor = System.Drawing.Color.Empty;
            this.ITAKU_KEIYAKU_NO.DisplayItemName = "委託契約番号";
            this.ITAKU_KEIYAKU_NO.DisplayPopUp = null;
            this.ITAKU_KEIYAKU_NO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ITAKU_KEIYAKU_NO.FocusOutCheckMethod")));
            this.ITAKU_KEIYAKU_NO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ITAKU_KEIYAKU_NO.ForeColor = System.Drawing.Color.Black;
            this.ITAKU_KEIYAKU_NO.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ITAKU_KEIYAKU_NO.IsInputErrorOccured = false;
            this.ITAKU_KEIYAKU_NO.Location = new System.Drawing.Point(117, 9);
            this.ITAKU_KEIYAKU_NO.MaxLength = 20;
            this.ITAKU_KEIYAKU_NO.Name = "ITAKU_KEIYAKU_NO";
            this.ITAKU_KEIYAKU_NO.PopupAfterExecute = null;
            this.ITAKU_KEIYAKU_NO.PopupBeforeExecute = null;
            this.ITAKU_KEIYAKU_NO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ITAKU_KEIYAKU_NO.PopupSearchSendParams")));
            this.ITAKU_KEIYAKU_NO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ITAKU_KEIYAKU_NO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ITAKU_KEIYAKU_NO.popupWindowSetting")));
            this.ITAKU_KEIYAKU_NO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ITAKU_KEIYAKU_NO.RegistCheckMethod")));
            this.ITAKU_KEIYAKU_NO.ShortItemName = "委託契約番号";
            this.ITAKU_KEIYAKU_NO.Size = new System.Drawing.Size(113, 20);
            this.ITAKU_KEIYAKU_NO.TabIndex = 654;
            this.ITAKU_KEIYAKU_NO.Tag = "半角２０桁以内で入力してください";
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label19.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(4, 9);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(110, 20);
            this.label19.TabIndex = 655;
            this.label19.Text = "契約番号";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KEIYAKUSHO_SEARCH_BUTTON
            // 
            this.KEIYAKUSHO_SEARCH_BUTTON.BackColor = System.Drawing.SystemColors.Control;
            this.KEIYAKUSHO_SEARCH_BUTTON.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.KEIYAKUSHO_SEARCH_BUTTON.DBFieldsName = null;
            this.KEIYAKUSHO_SEARCH_BUTTON.DefaultBackColor = System.Drawing.Color.Empty;
            this.KEIYAKUSHO_SEARCH_BUTTON.DisplayItemName = null;
            this.KEIYAKUSHO_SEARCH_BUTTON.DisplayPopUp = null;
            this.KEIYAKUSHO_SEARCH_BUTTON.ErrorMessage = null;
            this.KEIYAKUSHO_SEARCH_BUTTON.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIYAKUSHO_SEARCH_BUTTON.FocusOutCheckMethod")));
            this.KEIYAKUSHO_SEARCH_BUTTON.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KEIYAKUSHO_SEARCH_BUTTON.GetCodeMasterField = null;
            this.KEIYAKUSHO_SEARCH_BUTTON.Image = ((System.Drawing.Image)(resources.GetObject("KEIYAKUSHO_SEARCH_BUTTON.Image")));
            this.KEIYAKUSHO_SEARCH_BUTTON.ItemDefinedTypes = null;
            this.KEIYAKUSHO_SEARCH_BUTTON.LinkedSettingTextBox = null;
            this.KEIYAKUSHO_SEARCH_BUTTON.LinkedTextBoxs = new string[0];
            this.KEIYAKUSHO_SEARCH_BUTTON.Location = new System.Drawing.Point(497, 9);
            this.KEIYAKUSHO_SEARCH_BUTTON.Name = "KEIYAKUSHO_SEARCH_BUTTON";
            this.KEIYAKUSHO_SEARCH_BUTTON.PopupAfterExecute = null;
            this.KEIYAKUSHO_SEARCH_BUTTON.PopupAfterExecuteMethod = "";
            this.KEIYAKUSHO_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.KEIYAKUSHO_SEARCH_BUTTON.PopupGetMasterField = "";
            this.KEIYAKUSHO_SEARCH_BUTTON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KEIYAKUSHO_SEARCH_BUTTON.PopupSearchSendParams")));
            this.KEIYAKUSHO_SEARCH_BUTTON.PopupSetFormField = "";
            this.KEIYAKUSHO_SEARCH_BUTTON.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KEIYAKUSHO_SEARCH_BUTTON.PopupWindowName = "";
            this.KEIYAKUSHO_SEARCH_BUTTON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KEIYAKUSHO_SEARCH_BUTTON.popupWindowSetting")));
            this.KEIYAKUSHO_SEARCH_BUTTON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIYAKUSHO_SEARCH_BUTTON.RegistCheckMethod")));
            this.KEIYAKUSHO_SEARCH_BUTTON.SearchDisplayFlag = 0;
            this.KEIYAKUSHO_SEARCH_BUTTON.SetFormField = "";
            this.KEIYAKUSHO_SEARCH_BUTTON.ShortItemName = null;
            this.KEIYAKUSHO_SEARCH_BUTTON.Size = new System.Drawing.Size(25, 20);
            this.KEIYAKUSHO_SEARCH_BUTTON.TabIndex = 662;
            this.KEIYAKUSHO_SEARCH_BUTTON.TabStop = false;
            this.KEIYAKUSHO_SEARCH_BUTTON.Tag = "契約書種類検索画面を表示します";
            this.KEIYAKUSHO_SEARCH_BUTTON.UseVisualStyleBackColor = false;
            this.KEIYAKUSHO_SEARCH_BUTTON.ZeroPaddengFlag = false;
            this.KEIYAKUSHO_SEARCH_BUTTON.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Keiyakusho_MouseClick);
            // 
            // KEIYAKUSHO_SHURUI_NAME
            // 
            this.KEIYAKUSHO_SHURUI_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KEIYAKUSHO_SHURUI_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KEIYAKUSHO_SHURUI_NAME.DBFieldsName = "KEIYAKUSHO_SHURUI_NAME";
            this.KEIYAKUSHO_SHURUI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.KEIYAKUSHO_SHURUI_NAME.DisplayPopUp = null;
            this.KEIYAKUSHO_SHURUI_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIYAKUSHO_SHURUI_NAME.FocusOutCheckMethod")));
            this.KEIYAKUSHO_SHURUI_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KEIYAKUSHO_SHURUI_NAME.ForeColor = System.Drawing.Color.Black;
            this.KEIYAKUSHO_SHURUI_NAME.IsInputErrorOccured = false;
            this.KEIYAKUSHO_SHURUI_NAME.Location = new System.Drawing.Point(342, 9);
            this.KEIYAKUSHO_SHURUI_NAME.Name = "KEIYAKUSHO_SHURUI_NAME";
            this.KEIYAKUSHO_SHURUI_NAME.PopupAfterExecute = null;
            this.KEIYAKUSHO_SHURUI_NAME.PopupBeforeExecute = null;
            this.KEIYAKUSHO_SHURUI_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KEIYAKUSHO_SHURUI_NAME.PopupSearchSendParams")));
            this.KEIYAKUSHO_SHURUI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KEIYAKUSHO_SHURUI_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KEIYAKUSHO_SHURUI_NAME.popupWindowSetting")));
            this.KEIYAKUSHO_SHURUI_NAME.ReadOnly = true;
            this.KEIYAKUSHO_SHURUI_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIYAKUSHO_SHURUI_NAME.RegistCheckMethod")));
            this.KEIYAKUSHO_SHURUI_NAME.Size = new System.Drawing.Size(153, 20);
            this.KEIYAKUSHO_SHURUI_NAME.TabIndex = 661;
            this.KEIYAKUSHO_SHURUI_NAME.TabStop = false;
            // 
            // KEIYAKUSHO_SHURUI_CD
            // 
            this.KEIYAKUSHO_SHURUI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.KEIYAKUSHO_SHURUI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KEIYAKUSHO_SHURUI_CD.DBFieldsName = "KEIYAKUSHO_SHURUI_CD";
            this.KEIYAKUSHO_SHURUI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.KEIYAKUSHO_SHURUI_CD.DisplayItemName = "契約書種類";
            this.KEIYAKUSHO_SHURUI_CD.DisplayPopUp = null;
            this.KEIYAKUSHO_SHURUI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIYAKUSHO_SHURUI_CD.FocusOutCheckMethod")));
            this.KEIYAKUSHO_SHURUI_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KEIYAKUSHO_SHURUI_CD.ForeColor = System.Drawing.Color.Black;
            this.KEIYAKUSHO_SHURUI_CD.IsInputErrorOccured = false;
            this.KEIYAKUSHO_SHURUI_CD.Location = new System.Drawing.Point(321, 9);
            this.KEIYAKUSHO_SHURUI_CD.Name = "KEIYAKUSHO_SHURUI_CD";
            this.KEIYAKUSHO_SHURUI_CD.PopupAfterExecute = null;
            this.KEIYAKUSHO_SHURUI_CD.PopupBeforeExecute = null;
            this.KEIYAKUSHO_SHURUI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KEIYAKUSHO_SHURUI_CD.PopupSearchSendParams")));
            this.KEIYAKUSHO_SHURUI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KEIYAKUSHO_SHURUI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KEIYAKUSHO_SHURUI_CD.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.KEIYAKUSHO_SHURUI_CD.RangeSetting = rangeSettingDto1;
            this.KEIYAKUSHO_SHURUI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIYAKUSHO_SHURUI_CD.RegistCheckMethod")));
            this.KEIYAKUSHO_SHURUI_CD.ShortItemName = "契約書種類";
            this.KEIYAKUSHO_SHURUI_CD.Size = new System.Drawing.Size(20, 20);
            this.KEIYAKUSHO_SHURUI_CD.TabIndex = 656;
            this.KEIYAKUSHO_SHURUI_CD.Tag = "契約書種類を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.KEIYAKUSHO_SHURUI_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.KEIYAKUSHO_SHURUI_CD.WordWrap = false;
            this.KEIYAKUSHO_SHURUI_CD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeiyakushoShuruiCd_KeyDown);
            this.KEIYAKUSHO_SHURUI_CD.Validated += new System.EventHandler(this.KEIYAKUSHO_SHURUI_CD_Validated);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(234, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 20);
            this.label3.TabIndex = 659;
            this.label3.Text = "契約書種類";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KEIYAKU_JYOUKYOU_NAME
            // 
            this.KEIYAKU_JYOUKYOU_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KEIYAKU_JYOUKYOU_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KEIYAKU_JYOUKYOU_NAME.DBFieldsName = "KEIYAKU_JYOUKYOU_NAME";
            this.KEIYAKU_JYOUKYOU_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.KEIYAKU_JYOUKYOU_NAME.DisplayPopUp = null;
            this.KEIYAKU_JYOUKYOU_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIYAKU_JYOUKYOU_NAME.FocusOutCheckMethod")));
            this.KEIYAKU_JYOUKYOU_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KEIYAKU_JYOUKYOU_NAME.ForeColor = System.Drawing.Color.Black;
            this.KEIYAKU_JYOUKYOU_NAME.IsInputErrorOccured = false;
            this.KEIYAKU_JYOUKYOU_NAME.Location = new System.Drawing.Point(833, 9);
            this.KEIYAKU_JYOUKYOU_NAME.Name = "KEIYAKU_JYOUKYOU_NAME";
            this.KEIYAKU_JYOUKYOU_NAME.PopupAfterExecute = null;
            this.KEIYAKU_JYOUKYOU_NAME.PopupBeforeExecute = null;
            this.KEIYAKU_JYOUKYOU_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KEIYAKU_JYOUKYOU_NAME.PopupSearchSendParams")));
            this.KEIYAKU_JYOUKYOU_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KEIYAKU_JYOUKYOU_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KEIYAKU_JYOUKYOU_NAME.popupWindowSetting")));
            this.KEIYAKU_JYOUKYOU_NAME.ReadOnly = true;
            this.KEIYAKU_JYOUKYOU_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIYAKU_JYOUKYOU_NAME.RegistCheckMethod")));
            this.KEIYAKU_JYOUKYOU_NAME.Size = new System.Drawing.Size(91, 20);
            this.KEIYAKU_JYOUKYOU_NAME.TabIndex = 660;
            this.KEIYAKU_JYOUKYOU_NAME.TabStop = false;
            // 
            // KEIYAKU_JYOUKYOU_CD
            // 
            this.KEIYAKU_JYOUKYOU_CD.BackColor = System.Drawing.SystemColors.Window;
            this.KEIYAKU_JYOUKYOU_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KEIYAKU_JYOUKYOU_CD.DBFieldsName = "KEIYAKU_JYOUKYOU_CD";
            this.KEIYAKU_JYOUKYOU_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.KEIYAKU_JYOUKYOU_CD.DisplayItemName = "契約状況";
            this.KEIYAKU_JYOUKYOU_CD.DisplayPopUp = null;
            this.KEIYAKU_JYOUKYOU_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIYAKU_JYOUKYOU_CD.FocusOutCheckMethod")));
            this.KEIYAKU_JYOUKYOU_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KEIYAKU_JYOUKYOU_CD.ForeColor = System.Drawing.Color.Black;
            this.KEIYAKU_JYOUKYOU_CD.IsInputErrorOccured = false;
            this.KEIYAKU_JYOUKYOU_CD.Location = new System.Drawing.Point(812, 9);
            this.KEIYAKU_JYOUKYOU_CD.Name = "KEIYAKU_JYOUKYOU_CD";
            this.KEIYAKU_JYOUKYOU_CD.PopupAfterExecute = null;
            this.KEIYAKU_JYOUKYOU_CD.PopupBeforeExecute = null;
            this.KEIYAKU_JYOUKYOU_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KEIYAKU_JYOUKYOU_CD.PopupSearchSendParams")));
            this.KEIYAKU_JYOUKYOU_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KEIYAKU_JYOUKYOU_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KEIYAKU_JYOUKYOU_CD.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.KEIYAKU_JYOUKYOU_CD.RangeSetting = rangeSettingDto2;
            this.KEIYAKU_JYOUKYOU_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIYAKU_JYOUKYOU_CD.RegistCheckMethod")));
            this.KEIYAKU_JYOUKYOU_CD.ShortItemName = "契約状況";
            this.KEIYAKU_JYOUKYOU_CD.Size = new System.Drawing.Size(20, 20);
            this.KEIYAKU_JYOUKYOU_CD.TabIndex = 658;
            this.KEIYAKU_JYOUKYOU_CD.Tag = "契約状況を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.KEIYAKU_JYOUKYOU_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.KEIYAKU_JYOUKYOU_CD.WordWrap = false;
            this.KEIYAKU_JYOUKYOU_CD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Keiyaku_Jyoukyou_KeyDown);
            this.KEIYAKU_JYOUKYOU_CD.Validated += new System.EventHandler(this.KEIYAKU_JYOUKYOU_CD_Validated);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(736, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 20);
            this.label6.TabIndex = 658;
            this.label6.Text = "契約状況";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ICHIRAN_HYOUJI_JOUKEN_DELETED
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Location = new System.Drawing.Point(1000, 11);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Name = "ICHIRAN_HYOUJI_JOUKEN_DELETED";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Size = new System.Drawing.Size(180, 17);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.TabIndex = 659;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Text = "削除済も含めて全て表示";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(927, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 20);
            this.label2.TabIndex = 665;
            this.label2.Text = "表示条件";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KYOKASHOU_SHURUI_NAME
            // 
            this.KYOKASHOU_SHURUI_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KYOKASHOU_SHURUI_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOKASHOU_SHURUI_NAME.DBFieldsName = "KYOKASHOU_SHURUI_NAME";
            this.KYOKASHOU_SHURUI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOKASHOU_SHURUI_NAME.DisplayPopUp = null;
            this.KYOKASHOU_SHURUI_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOKASHOU_SHURUI_NAME.FocusOutCheckMethod")));
            this.KYOKASHOU_SHURUI_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOKASHOU_SHURUI_NAME.ForeColor = System.Drawing.Color.Black;
            this.KYOKASHOU_SHURUI_NAME.IsInputErrorOccured = false;
            this.KYOKASHOU_SHURUI_NAME.Location = new System.Drawing.Point(631, 9);
            this.KYOKASHOU_SHURUI_NAME.Name = "KYOKASHOU_SHURUI_NAME";
            this.KYOKASHOU_SHURUI_NAME.PopupAfterExecute = null;
            this.KYOKASHOU_SHURUI_NAME.PopupBeforeExecute = null;
            this.KYOKASHOU_SHURUI_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOKASHOU_SHURUI_NAME.PopupSearchSendParams")));
            this.KYOKASHOU_SHURUI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOKASHOU_SHURUI_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOKASHOU_SHURUI_NAME.popupWindowSetting")));
            this.KYOKASHOU_SHURUI_NAME.ReadOnly = true;
            this.KYOKASHOU_SHURUI_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOKASHOU_SHURUI_NAME.RegistCheckMethod")));
            this.KYOKASHOU_SHURUI_NAME.Size = new System.Drawing.Size(99, 20);
            this.KYOKASHOU_SHURUI_NAME.TabIndex = 668;
            this.KYOKASHOU_SHURUI_NAME.TabStop = false;
            // 
            // KYOKASHOU_SHURUI_CD
            // 
            this.KYOKASHOU_SHURUI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.KYOKASHOU_SHURUI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOKASHOU_SHURUI_CD.DBFieldsName = "KYOKASHOU_SHURUI_CD";
            this.KYOKASHOU_SHURUI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOKASHOU_SHURUI_CD.DisplayItemName = "許可証種類";
            this.KYOKASHOU_SHURUI_CD.DisplayPopUp = null;
            this.KYOKASHOU_SHURUI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOKASHOU_SHURUI_CD.FocusOutCheckMethod")));
            this.KYOKASHOU_SHURUI_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KYOKASHOU_SHURUI_CD.ForeColor = System.Drawing.Color.Black;
            this.KYOKASHOU_SHURUI_CD.IsInputErrorOccured = false;
            this.KYOKASHOU_SHURUI_CD.Location = new System.Drawing.Point(610, 9);
            this.KYOKASHOU_SHURUI_CD.Name = "KYOKASHOU_SHURUI_CD";
            this.KYOKASHOU_SHURUI_CD.PopupAfterExecute = null;
            this.KYOKASHOU_SHURUI_CD.PopupBeforeExecute = null;
            this.KYOKASHOU_SHURUI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOKASHOU_SHURUI_CD.PopupSearchSendParams")));
            this.KYOKASHOU_SHURUI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOKASHOU_SHURUI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOKASHOU_SHURUI_CD.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.KYOKASHOU_SHURUI_CD.RangeSetting = rangeSettingDto3;
            this.KYOKASHOU_SHURUI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOKASHOU_SHURUI_CD.RegistCheckMethod")));
            this.KYOKASHOU_SHURUI_CD.ShortItemName = "許可証種類";
            this.KYOKASHOU_SHURUI_CD.Size = new System.Drawing.Size(20, 20);
            this.KYOKASHOU_SHURUI_CD.TabIndex = 657;
            this.KYOKASHOU_SHURUI_CD.Tag = "許可証種類を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.KYOKASHOU_SHURUI_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.KYOKASHOU_SHURUI_CD.WordWrap = false;
            this.KYOKASHOU_SHURUI_CD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Kyokashou_KeyDown);
            this.KYOKASHOU_SHURUI_CD.Validated += new System.EventHandler(this.KYOKASHOU_SHURUI_CD_Validated);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(525, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 20);
            this.label1.TabIndex = 667;
            this.label1.Text = "許可証種類";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DATE_SELECT_7
            // 
            this.DATE_SELECT_7.AutoSize = true;
            this.DATE_SELECT_7.DBFieldsName = "";
            this.DATE_SELECT_7.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_SELECT_7.DisplayItemName = "更新種別";
            this.DATE_SELECT_7.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT_7.FocusOutCheckMethod")));
            this.DATE_SELECT_7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DATE_SELECT_7.LinkedTextBox = "DATE_SELECT";
            this.DATE_SELECT_7.Location = new System.Drawing.Point(720, 49);
            this.DATE_SELECT_7.Name = "DATE_SELECT_7";
            this.DATE_SELECT_7.PopupAfterExecute = null;
            this.DATE_SELECT_7.PopupBeforeExecute = null;
            this.DATE_SELECT_7.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DATE_SELECT_7.PopupSearchSendParams")));
            this.DATE_SELECT_7.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DATE_SELECT_7.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DATE_SELECT_7.popupWindowSetting")));
            this.DATE_SELECT_7.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT_7.RegistCheckMethod")));
            this.DATE_SELECT_7.ShortItemName = "更新種別";
            this.DATE_SELECT_7.Size = new System.Drawing.Size(95, 17);
            this.DATE_SELECT_7.TabIndex = 676;
            this.DATE_SELECT_7.Tag = "日付なしで検索を行う場合チェックを付けてください";
            this.DATE_SELECT_7.Text = "7.日付なし";
            this.DATE_SELECT_7.UseVisualStyleBackColor = true;
            this.DATE_SELECT_7.Value = "7";
            // 
            // DATE_SELECT_6
            // 
            this.DATE_SELECT_6.AutoSize = true;
            this.DATE_SELECT_6.DBFieldsName = "";
            this.DATE_SELECT_6.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_SELECT_6.DisplayItemName = "更新種別";
            this.DATE_SELECT_6.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT_6.FocusOutCheckMethod")));
            this.DATE_SELECT_6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DATE_SELECT_6.LinkedTextBox = "DATE_SELECT";
            this.DATE_SELECT_6.Location = new System.Drawing.Point(580, 49);
            this.DATE_SELECT_6.Name = "DATE_SELECT_6";
            this.DATE_SELECT_6.PopupAfterExecute = null;
            this.DATE_SELECT_6.PopupBeforeExecute = null;
            this.DATE_SELECT_6.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DATE_SELECT_6.PopupSearchSendParams")));
            this.DATE_SELECT_6.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DATE_SELECT_6.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DATE_SELECT_6.popupWindowSetting")));
            this.DATE_SELECT_6.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT_6.RegistCheckMethod")));
            this.DATE_SELECT_6.ShortItemName = "更新種別";
            this.DATE_SELECT_6.Size = new System.Drawing.Size(137, 17);
            this.DATE_SELECT_6.TabIndex = 675;
            this.DATE_SELECT_6.Tag = "自動更新終了日で検索を行う場合チェックを付けてください";
            this.DATE_SELECT_6.Text = "6.自動更新終了日";
            this.DATE_SELECT_6.UseVisualStyleBackColor = true;
            this.DATE_SELECT_6.Value = "6";
            // 
            // DATE_SELECT_5
            // 
            this.DATE_SELECT_5.AutoSize = true;
            this.DATE_SELECT_5.DBFieldsName = "";
            this.DATE_SELECT_5.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_SELECT_5.DisplayItemName = "更新種別";
            this.DATE_SELECT_5.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT_5.FocusOutCheckMethod")));
            this.DATE_SELECT_5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DATE_SELECT_5.LinkedTextBox = "DATE_SELECT";
            this.DATE_SELECT_5.Location = new System.Drawing.Point(485, 49);
            this.DATE_SELECT_5.Name = "DATE_SELECT_5";
            this.DATE_SELECT_5.PopupAfterExecute = null;
            this.DATE_SELECT_5.PopupBeforeExecute = null;
            this.DATE_SELECT_5.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DATE_SELECT_5.PopupSearchSendParams")));
            this.DATE_SELECT_5.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DATE_SELECT_5.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DATE_SELECT_5.popupWindowSetting")));
            this.DATE_SELECT_5.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT_5.RegistCheckMethod")));
            this.DATE_SELECT_5.ShortItemName = "更新種別";
            this.DATE_SELECT_5.Size = new System.Drawing.Size(95, 17);
            this.DATE_SELECT_5.TabIndex = 674;
            this.DATE_SELECT_5.Tag = "有効期間終了日で検索を行う場合チェックを付けてください";
            this.DATE_SELECT_5.Text = "5.有効終了";
            this.DATE_SELECT_5.UseVisualStyleBackColor = true;
            this.DATE_SELECT_5.Value = "5";
            // 
            // DATE_SELECT_4
            // 
            this.DATE_SELECT_4.AutoSize = true;
            this.DATE_SELECT_4.DBFieldsName = "";
            this.DATE_SELECT_4.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_SELECT_4.DisplayItemName = "更新種別";
            this.DATE_SELECT_4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT_4.FocusOutCheckMethod")));
            this.DATE_SELECT_4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DATE_SELECT_4.LinkedTextBox = "DATE_SELECT";
            this.DATE_SELECT_4.Location = new System.Drawing.Point(392, 49);
            this.DATE_SELECT_4.Name = "DATE_SELECT_4";
            this.DATE_SELECT_4.PopupAfterExecute = null;
            this.DATE_SELECT_4.PopupBeforeExecute = null;
            this.DATE_SELECT_4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DATE_SELECT_4.PopupSearchSendParams")));
            this.DATE_SELECT_4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DATE_SELECT_4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DATE_SELECT_4.popupWindowSetting")));
            this.DATE_SELECT_4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT_4.RegistCheckMethod")));
            this.DATE_SELECT_4.ShortItemName = "更新種別";
            this.DATE_SELECT_4.Size = new System.Drawing.Size(95, 17);
            this.DATE_SELECT_4.TabIndex = 673;
            this.DATE_SELECT_4.Tag = "有効期間開始日で検索を行う場合チェックを付けてください";
            this.DATE_SELECT_4.Text = "4.有効開始";
            this.DATE_SELECT_4.UseVisualStyleBackColor = true;
            this.DATE_SELECT_4.Value = "4";
            // 
            // DATE_SELECT_3
            // 
            this.DATE_SELECT_3.AutoSize = true;
            this.DATE_SELECT_3.DBFieldsName = "";
            this.DATE_SELECT_3.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_SELECT_3.DisplayItemName = "更新種別";
            this.DATE_SELECT_3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT_3.FocusOutCheckMethod")));
            this.DATE_SELECT_3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DATE_SELECT_3.LinkedTextBox = "DATE_SELECT";
            this.DATE_SELECT_3.Location = new System.Drawing.Point(311, 49);
            this.DATE_SELECT_3.Name = "DATE_SELECT_3";
            this.DATE_SELECT_3.PopupAfterExecute = null;
            this.DATE_SELECT_3.PopupBeforeExecute = null;
            this.DATE_SELECT_3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DATE_SELECT_3.PopupSearchSendParams")));
            this.DATE_SELECT_3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DATE_SELECT_3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DATE_SELECT_3.popupWindowSetting")));
            this.DATE_SELECT_3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT_3.RegistCheckMethod")));
            this.DATE_SELECT_3.ShortItemName = "更新種別";
            this.DATE_SELECT_3.Size = new System.Drawing.Size(81, 17);
            this.DATE_SELECT_3.TabIndex = 672;
            this.DATE_SELECT_3.Tag = "契約日で検索を行う場合チェックを付けてください";
            this.DATE_SELECT_3.Text = "3.契約日";
            this.DATE_SELECT_3.UseVisualStyleBackColor = true;
            this.DATE_SELECT_3.Value = "3";
            // 
            // DATE_SELECT_2
            // 
            this.DATE_SELECT_2.AutoSize = true;
            this.DATE_SELECT_2.DBFieldsName = "";
            this.DATE_SELECT_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_SELECT_2.DisplayItemName = "更新種別";
            this.DATE_SELECT_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT_2.FocusOutCheckMethod")));
            this.DATE_SELECT_2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DATE_SELECT_2.LinkedTextBox = "DATE_SELECT";
            this.DATE_SELECT_2.Location = new System.Drawing.Point(230, 49);
            this.DATE_SELECT_2.Name = "DATE_SELECT_2";
            this.DATE_SELECT_2.PopupAfterExecute = null;
            this.DATE_SELECT_2.PopupBeforeExecute = null;
            this.DATE_SELECT_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DATE_SELECT_2.PopupSearchSendParams")));
            this.DATE_SELECT_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DATE_SELECT_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DATE_SELECT_2.popupWindowSetting")));
            this.DATE_SELECT_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT_2.RegistCheckMethod")));
            this.DATE_SELECT_2.ShortItemName = "更新種別";
            this.DATE_SELECT_2.Size = new System.Drawing.Size(81, 17);
            this.DATE_SELECT_2.TabIndex = 671;
            this.DATE_SELECT_2.Tag = "送付日で検索を行う場合チェックを付けてください";
            this.DATE_SELECT_2.Text = "2.送付日";
            this.DATE_SELECT_2.UseVisualStyleBackColor = true;
            this.DATE_SELECT_2.Value = "2";
            // 
            // DATE_SELECT_1
            // 
            this.DATE_SELECT_1.AutoSize = true;
            this.DATE_SELECT_1.DBFieldsName = "";
            this.DATE_SELECT_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_SELECT_1.DisplayItemName = "更新種別";
            this.DATE_SELECT_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT_1.FocusOutCheckMethod")));
            this.DATE_SELECT_1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DATE_SELECT_1.LinkedTextBox = "DATE_SELECT";
            this.DATE_SELECT_1.Location = new System.Drawing.Point(142, 49);
            this.DATE_SELECT_1.Name = "DATE_SELECT_1";
            this.DATE_SELECT_1.PopupAfterExecute = null;
            this.DATE_SELECT_1.PopupBeforeExecute = null;
            this.DATE_SELECT_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DATE_SELECT_1.PopupSearchSendParams")));
            this.DATE_SELECT_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DATE_SELECT_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DATE_SELECT_1.popupWindowSetting")));
            this.DATE_SELECT_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT_1.RegistCheckMethod")));
            this.DATE_SELECT_1.ShortItemName = "更新種別";
            this.DATE_SELECT_1.Size = new System.Drawing.Size(88, 17);
            this.DATE_SELECT_1.TabIndex = 670;
            this.DATE_SELECT_1.Tag = "作成日で検索を行う場合チェックを付けてください";
            this.DATE_SELECT_1.Text = "1. 作成日";
            this.DATE_SELECT_1.UseVisualStyleBackColor = true;
            this.DATE_SELECT_1.Value = "1";
            // 
            // DATE_SELECT
            // 
            this.DATE_SELECT.BackColor = System.Drawing.SystemColors.Window;
            this.DATE_SELECT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DATE_SELECT.DBFieldsName = "";
            this.DATE_SELECT.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_SELECT.DisplayItemName = "検索条件";
            this.DATE_SELECT.DisplayPopUp = null;
            this.DATE_SELECT.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT.FocusOutCheckMethod")));
            this.DATE_SELECT.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DATE_SELECT.ForeColor = System.Drawing.Color.Black;
            this.DATE_SELECT.IsInputErrorOccured = false;
            this.DATE_SELECT.ItemDefinedTypes = "";
            this.DATE_SELECT.LinkedRadioButtonArray = new string[] {
        "DATE_SELECT_1",
        "DATE_SELECT_2",
        "DATE_SELECT_3",
        "DATE_SELECT_4",
        "DATE_SELECT_5",
        "DATE_SELECT_6",
        "DATE_SELECT_7"};
            this.DATE_SELECT.Location = new System.Drawing.Point(119, 47);
            this.DATE_SELECT.Name = "DATE_SELECT";
            this.DATE_SELECT.PopupAfterExecute = null;
            this.DATE_SELECT.PopupBeforeExecute = null;
            this.DATE_SELECT.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DATE_SELECT.PopupSearchSendParams")));
            this.DATE_SELECT.PopupSetFormField = "";
            this.DATE_SELECT.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DATE_SELECT.PopupWindowName = "";
            this.DATE_SELECT.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DATE_SELECT.popupWindowSetting")));
            rangeSettingDto4.Max = new decimal(new int[] {
            7,
            0,
            0,
            0});
            rangeSettingDto4.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DATE_SELECT.RangeSetting = rangeSettingDto4;
            this.DATE_SELECT.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT.RegistCheckMethod")));
            this.DATE_SELECT.SetFormField = "";
            this.DATE_SELECT.ShortItemName = "検索条件";
            this.DATE_SELECT.Size = new System.Drawing.Size(20, 20);
            this.DATE_SELECT.TabIndex = 669;
            this.DATE_SELECT.Tag = "【1～7】のいずれかで入力してください";
            this.DATE_SELECT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.DATE_SELECT.WordWrap = false;
            this.DATE_SELECT.TextChanged += new System.EventHandler(this.DATE_SELECT_TextChanged);
            // 
            // labelInfo
            // 
            this.labelInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.labelInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelInfo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelInfo.ForeColor = System.Drawing.Color.White;
            this.labelInfo.Location = new System.Drawing.Point(4, 47);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(110, 20);
            this.labelInfo.TabIndex = 678;
            this.labelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DATE_TO
            // 
            this.DATE_TO.BackColor = System.Drawing.SystemColors.Window;
            this.DATE_TO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DATE_TO.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.DATE_TO.Checked = false;
            this.DATE_TO.DateTimeNowYear = "";
            this.DATE_TO.DBFieldsName = "";
            this.DATE_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_TO.DisplayItemName = "";
            this.DATE_TO.DisplayPopUp = null;
            this.DATE_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_TO.FocusOutCheckMethod")));
            this.DATE_TO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.DATE_TO.ForeColor = System.Drawing.Color.Black;
            this.DATE_TO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DATE_TO.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.DATE_TO.IsInputErrorOccured = false;
            this.DATE_TO.ItemDefinedTypes = "datetime";
            this.DATE_TO.Location = new System.Drawing.Point(941, 47);
            this.DATE_TO.MaxLength = 10;
            this.DATE_TO.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.DATE_TO.Name = "DATE_TO";
            this.DATE_TO.NullValue = "";
            this.DATE_TO.PopupAfterExecute = null;
            this.DATE_TO.PopupBeforeExecute = null;
            this.DATE_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DATE_TO.PopupSearchSendParams")));
            this.DATE_TO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DATE_TO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DATE_TO.popupWindowSetting")));
            this.DATE_TO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_TO.RegistCheckMethod")));
            this.DATE_TO.ShortItemName = "";
            this.DATE_TO.Size = new System.Drawing.Size(100, 20);
            this.DATE_TO.TabIndex = 680;
            this.DATE_TO.Tag = "";
            this.DATE_TO.Value = null;
            this.DATE_TO.DoubleClick += new System.EventHandler(this.DATE_TO_DoubleClick);
            // 
            // DATE_FROM
            // 
            this.DATE_FROM.BackColor = System.Drawing.SystemColors.Window;
            this.DATE_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DATE_FROM.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.DATE_FROM.Checked = false;
            this.DATE_FROM.DateTimeNowYear = "";
            this.DATE_FROM.DBFieldsName = "";
            this.DATE_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_FROM.DisplayItemName = "";
            this.DATE_FROM.DisplayPopUp = null;
            this.DATE_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_FROM.FocusOutCheckMethod")));
            this.DATE_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.DATE_FROM.ForeColor = System.Drawing.Color.Black;
            this.DATE_FROM.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DATE_FROM.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.DATE_FROM.IsInputErrorOccured = false;
            this.DATE_FROM.ItemDefinedTypes = "datetime";
            this.DATE_FROM.Location = new System.Drawing.Point(820, 47);
            this.DATE_FROM.MaxLength = 10;
            this.DATE_FROM.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.DATE_FROM.Name = "DATE_FROM";
            this.DATE_FROM.NullValue = "";
            this.DATE_FROM.PopupAfterExecute = null;
            this.DATE_FROM.PopupBeforeExecute = null;
            this.DATE_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DATE_FROM.PopupSearchSendParams")));
            this.DATE_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DATE_FROM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DATE_FROM.popupWindowSetting")));
            this.DATE_FROM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_FROM.RegistCheckMethod")));
            this.DATE_FROM.ShortItemName = "";
            this.DATE_FROM.Size = new System.Drawing.Size(100, 20);
            this.DATE_FROM.TabIndex = 679;
            this.DATE_FROM.Tag = "";
            this.DATE_FROM.Value = null;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(923, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 20);
            this.label4.TabIndex = 681;
            this.label4.Text = "～";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UNPANGYOUSHA_SEARCH_BUTTON
            // 
            this.UNPANGYOUSHA_SEARCH_BUTTON.BackColor = System.Drawing.SystemColors.Control;
            this.UNPANGYOUSHA_SEARCH_BUTTON.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.UNPANGYOUSHA_SEARCH_BUTTON.DBFieldsName = null;
            this.UNPANGYOUSHA_SEARCH_BUTTON.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNPANGYOUSHA_SEARCH_BUTTON.DisplayItemName = null;
            this.UNPANGYOUSHA_SEARCH_BUTTON.DisplayPopUp = null;
            this.UNPANGYOUSHA_SEARCH_BUTTON.ErrorMessage = null;
            this.UNPANGYOUSHA_SEARCH_BUTTON.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPANGYOUSHA_SEARCH_BUTTON.FocusOutCheckMethod")));
            this.UNPANGYOUSHA_SEARCH_BUTTON.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UNPANGYOUSHA_SEARCH_BUTTON.GetCodeMasterField = null;
            this.UNPANGYOUSHA_SEARCH_BUTTON.Image = ((System.Drawing.Image)(resources.GetObject("UNPANGYOUSHA_SEARCH_BUTTON.Image")));
            this.UNPANGYOUSHA_SEARCH_BUTTON.ItemDefinedTypes = null;
            this.UNPANGYOUSHA_SEARCH_BUTTON.LinkedSettingTextBox = null;
            this.UNPANGYOUSHA_SEARCH_BUTTON.LinkedTextBoxs = new string[] {
        "GyoushaCode"};
            this.UNPANGYOUSHA_SEARCH_BUTTON.Location = new System.Drawing.Point(457, 109);
            this.UNPANGYOUSHA_SEARCH_BUTTON.Name = "UNPANGYOUSHA_SEARCH_BUTTON";
            this.UNPANGYOUSHA_SEARCH_BUTTON.PopupAfterExecute = null;
            this.UNPANGYOUSHA_SEARCH_BUTTON.PopupAfterExecuteMethod = "PopupAfterGyoushaCode";
            this.UNPANGYOUSHA_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.UNPANGYOUSHA_SEARCH_BUTTON.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.UNPANGYOUSHA_SEARCH_BUTTON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNPANGYOUSHA_SEARCH_BUTTON.PopupSearchSendParams")));
            this.UNPANGYOUSHA_SEARCH_BUTTON.PopupSetFormField = "UNPANGYOUSHA_CD, UNPANGYOUSHA_NAME";
            this.UNPANGYOUSHA_SEARCH_BUTTON.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.UNPANGYOUSHA_SEARCH_BUTTON.PopupWindowName = "検索共通ポップアップ";
            this.UNPANGYOUSHA_SEARCH_BUTTON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNPANGYOUSHA_SEARCH_BUTTON.popupWindowSetting")));
            this.UNPANGYOUSHA_SEARCH_BUTTON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPANGYOUSHA_SEARCH_BUTTON.RegistCheckMethod")));
            this.UNPANGYOUSHA_SEARCH_BUTTON.SearchDisplayFlag = 0;
            this.UNPANGYOUSHA_SEARCH_BUTTON.SetFormField = null;
            this.UNPANGYOUSHA_SEARCH_BUTTON.ShortItemName = null;
            this.UNPANGYOUSHA_SEARCH_BUTTON.Size = new System.Drawing.Size(30, 20);
            this.UNPANGYOUSHA_SEARCH_BUTTON.TabIndex = 692;
            this.UNPANGYOUSHA_SEARCH_BUTTON.TabStop = false;
            this.UNPANGYOUSHA_SEARCH_BUTTON.Tag = "運搬業者検索画面を表示します";
            this.UNPANGYOUSHA_SEARCH_BUTTON.UseVisualStyleBackColor = false;
            this.UNPANGYOUSHA_SEARCH_BUTTON.ZeroPaddengFlag = false;
            // 
            // GENBA_SEARCH_BUTTON
            // 
            this.GENBA_SEARCH_BUTTON.BackColor = System.Drawing.SystemColors.Control;
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
            this.GENBA_SEARCH_BUTTON.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GENBA_SEARCH_BUTTON.GetCodeMasterField = null;
            this.GENBA_SEARCH_BUTTON.Image = ((System.Drawing.Image)(resources.GetObject("GENBA_SEARCH_BUTTON.Image")));
            this.GENBA_SEARCH_BUTTON.ItemDefinedTypes = null;
            this.GENBA_SEARCH_BUTTON.LinkedSettingTextBox = null;
            this.GENBA_SEARCH_BUTTON.LinkedTextBoxs = new string[] {
        "GyoushaCode"};
            this.GENBA_SEARCH_BUTTON.Location = new System.Drawing.Point(946, 87);
            this.GENBA_SEARCH_BUTTON.Name = "GENBA_SEARCH_BUTTON";
            this.GENBA_SEARCH_BUTTON.PopupAfterExecute = null;
            this.GENBA_SEARCH_BUTTON.PopupAfterExecuteMethod = "PopupAfterGyoushaCode";
            this.GENBA_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.GENBA_SEARCH_BUTTON.PopupGetMasterField = "GENBA_CD, GENBA_NAME_RYAKU,GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GENBA_SEARCH_BUTTON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_SEARCH_BUTTON.PopupSearchSendParams")));
            this.GENBA_SEARCH_BUTTON.PopupSetFormField = "GENBA_CD, GENBA_NAME_RYAKU,GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GENBA_SEARCH_BUTTON.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_SEARCH_BUTTON.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_SEARCH_BUTTON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_SEARCH_BUTTON.popupWindowSetting")));
            this.GENBA_SEARCH_BUTTON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_SEARCH_BUTTON.RegistCheckMethod")));
            this.GENBA_SEARCH_BUTTON.SearchDisplayFlag = 0;
            this.GENBA_SEARCH_BUTTON.SetFormField = "";
            this.GENBA_SEARCH_BUTTON.ShortItemName = null;
            this.GENBA_SEARCH_BUTTON.Size = new System.Drawing.Size(30, 20);
            this.GENBA_SEARCH_BUTTON.TabIndex = 693;
            this.GENBA_SEARCH_BUTTON.TabStop = false;
            this.GENBA_SEARCH_BUTTON.Tag = "現場検索画面を表示します";
            this.GENBA_SEARCH_BUTTON.UseVisualStyleBackColor = false;
            this.GENBA_SEARCH_BUTTON.ZeroPaddengFlag = false;
            // 
            // GYOUSHA_SEARCH_BUTTON
            // 
            this.GYOUSHA_SEARCH_BUTTON.BackColor = System.Drawing.SystemColors.Control;
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
            this.GYOUSHA_SEARCH_BUTTON.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GYOUSHA_SEARCH_BUTTON.GetCodeMasterField = null;
            this.GYOUSHA_SEARCH_BUTTON.Image = ((System.Drawing.Image)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.Image")));
            this.GYOUSHA_SEARCH_BUTTON.ItemDefinedTypes = null;
            this.GYOUSHA_SEARCH_BUTTON.LinkedSettingTextBox = null;
            this.GYOUSHA_SEARCH_BUTTON.LinkedTextBoxs = new string[] {
        "GyoushaCode"};
            this.GYOUSHA_SEARCH_BUTTON.Location = new System.Drawing.Point(457, 86);
            this.GYOUSHA_SEARCH_BUTTON.Name = "GYOUSHA_SEARCH_BUTTON";
            this.GYOUSHA_SEARCH_BUTTON.PopupAfterExecute = null;
            this.GYOUSHA_SEARCH_BUTTON.PopupAfterExecuteMethod = "GYOUSHA_PopupAfterExecuteMethod";
            this.GYOUSHA_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.GYOUSHA_SEARCH_BUTTON.PopupBeforeExecuteMethod = "GYOUSHA_PopupBeforeExecuteMethod";
            this.GYOUSHA_SEARCH_BUTTON.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams")));
            this.GYOUSHA_SEARCH_BUTTON.PopupSetFormField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_SEARCH_BUTTON.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_SEARCH_BUTTON.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_SEARCH_BUTTON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.popupWindowSetting")));
            this.GYOUSHA_SEARCH_BUTTON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.RegistCheckMethod")));
            this.GYOUSHA_SEARCH_BUTTON.SearchDisplayFlag = 0;
            this.GYOUSHA_SEARCH_BUTTON.SetFormField = "GYOUSHA_CD";
            this.GYOUSHA_SEARCH_BUTTON.ShortItemName = null;
            this.GYOUSHA_SEARCH_BUTTON.Size = new System.Drawing.Size(30, 20);
            this.GYOUSHA_SEARCH_BUTTON.TabIndex = 691;
            this.GYOUSHA_SEARCH_BUTTON.TabStop = false;
            this.GYOUSHA_SEARCH_BUTTON.Tag = "業者検索画面を表示します";
            this.GYOUSHA_SEARCH_BUTTON.UseVisualStyleBackColor = false;
            this.GYOUSHA_SEARCH_BUTTON.ZeroPaddengFlag = false;
            // 
            // UNPANGYOUSHA_NAME
            // 
            this.UNPANGYOUSHA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.UNPANGYOUSHA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UNPANGYOUSHA_NAME.DBFieldsName = "UNPANGYOUSHA_NAME_RYAKU";
            this.UNPANGYOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNPANGYOUSHA_NAME.DisplayPopUp = null;
            this.UNPANGYOUSHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPANGYOUSHA_NAME.FocusOutCheckMethod")));
            this.UNPANGYOUSHA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.UNPANGYOUSHA_NAME.ForeColor = System.Drawing.Color.Black;
            this.UNPANGYOUSHA_NAME.IsInputErrorOccured = false;
            this.UNPANGYOUSHA_NAME.Location = new System.Drawing.Point(169, 108);
            this.UNPANGYOUSHA_NAME.Name = "UNPANGYOUSHA_NAME";
            this.UNPANGYOUSHA_NAME.PopupAfterExecute = null;
            this.UNPANGYOUSHA_NAME.PopupBeforeExecute = null;
            this.UNPANGYOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNPANGYOUSHA_NAME.PopupSearchSendParams")));
            this.UNPANGYOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UNPANGYOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNPANGYOUSHA_NAME.popupWindowSetting")));
            this.UNPANGYOUSHA_NAME.ReadOnly = true;
            this.UNPANGYOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPANGYOUSHA_NAME.RegistCheckMethod")));
            this.UNPANGYOUSHA_NAME.Size = new System.Drawing.Size(286, 20);
            this.UNPANGYOUSHA_NAME.TabIndex = 688;
            this.UNPANGYOUSHA_NAME.TabStop = false;
            // 
            // GYOUSHA_NAME_RYAKU
            // 
            this.GYOUSHA_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GYOUSHA_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_NAME_RYAKU.DBFieldsName = "GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_NAME_RYAKU.DisplayPopUp = null;
            this.GYOUSHA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.FocusOutCheckMethod")));
            this.GYOUSHA_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GYOUSHA_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_NAME_RYAKU.IsInputErrorOccured = false;
            this.GYOUSHA_NAME_RYAKU.Location = new System.Drawing.Point(169, 86);
            this.GYOUSHA_NAME_RYAKU.Name = "GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_NAME_RYAKU.PopupAfterExecute = null;
            this.GYOUSHA_NAME_RYAKU.PopupBeforeExecute = null;
            this.GYOUSHA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.PopupSearchSendParams")));
            this.GYOUSHA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.popupWindowSetting")));
            this.GYOUSHA_NAME_RYAKU.ReadOnly = true;
            this.GYOUSHA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.RegistCheckMethod")));
            this.GYOUSHA_NAME_RYAKU.Size = new System.Drawing.Size(286, 20);
            this.GYOUSHA_NAME_RYAKU.TabIndex = 689;
            this.GYOUSHA_NAME_RYAKU.TabStop = false;
            // 
            // UNPANGYOUSHA_CD
            // 
            this.UNPANGYOUSHA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.UNPANGYOUSHA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UNPANGYOUSHA_CD.ChangeUpperCase = true;
            this.UNPANGYOUSHA_CD.CharacterLimitList = null;
            this.UNPANGYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNPANGYOUSHA_CD.DisplayItemName = "運搬業者";
            this.UNPANGYOUSHA_CD.DisplayPopUp = null;
            this.UNPANGYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPANGYOUSHA_CD.FocusOutCheckMethod")));
            this.UNPANGYOUSHA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.UNPANGYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.UNPANGYOUSHA_CD.GetCodeMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.UNPANGYOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.UNPANGYOUSHA_CD.IsInputErrorOccured = false;
            this.UNPANGYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.UNPANGYOUSHA_CD.Location = new System.Drawing.Point(117, 108);
            this.UNPANGYOUSHA_CD.MaxLength = 6;
            this.UNPANGYOUSHA_CD.Name = "UNPANGYOUSHA_CD";
            this.UNPANGYOUSHA_CD.PopupAfterExecute = null;
            this.UNPANGYOUSHA_CD.PopupBeforeExecute = null;
            this.UNPANGYOUSHA_CD.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.UNPANGYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNPANGYOUSHA_CD.PopupSearchSendParams")));
            this.UNPANGYOUSHA_CD.PopupSetFormField = "UNPANGYOUSHA_CD, UNPANGYOUSHA_NAME";
            this.UNPANGYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.UNPANGYOUSHA_CD.PopupWindowName = "検索共通ポップアップ";
            this.UNPANGYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNPANGYOUSHA_CD.popupWindowSetting")));
            this.UNPANGYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPANGYOUSHA_CD.RegistCheckMethod")));
            this.UNPANGYOUSHA_CD.SetFormField = "UNPANGYOUSHA_CD, UNPANGYOUSHA_NAME";
            this.UNPANGYOUSHA_CD.Size = new System.Drawing.Size(50, 20);
            this.UNPANGYOUSHA_CD.TabIndex = 684;
            this.UNPANGYOUSHA_CD.Tag = "運搬業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.UNPANGYOUSHA_CD.ZeroPaddengFlag = true;
            // 
            // GENBA_CD
            // 
            this.GENBA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.GENBA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_CD.ChangeUpperCase = true;
            this.GENBA_CD.CharacterLimitList = null;
            this.GENBA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_CD.DisplayItemName = "排出事業者";
            this.GENBA_CD.DisplayPopUp = null;
            this.GENBA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.FocusOutCheckMethod")));
            this.GENBA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GENBA_CD.ForeColor = System.Drawing.Color.Black;
            this.GENBA_CD.GetCodeMasterField = "GENBA_CD, GENBA_NAME_RYAKU,GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GENBA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GENBA_CD.IsInputErrorOccured = false;
            this.GENBA_CD.ItemDefinedTypes = "varchar";
            this.GENBA_CD.Location = new System.Drawing.Point(606, 86);
            this.GENBA_CD.MaxLength = 6;
            this.GENBA_CD.Name = "GENBA_CD";
            this.GENBA_CD.PopupAfterExecute = null;
            this.GENBA_CD.PopupBeforeExecute = null;
            this.GENBA_CD.PopupGetMasterField = "GENBA_CD, GENBA_NAME_RYAKU,GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_CD.PopupSearchSendParams")));
            this.GENBA_CD.PopupSetFormField = "GENBA_CD, GENBA_NAME_RYAKU,GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_CD.popupWindowSetting")));
            this.GENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.RegistCheckMethod")));
            this.GENBA_CD.SetFormField = "GENBA_CD, GENBA_NAME_RYAKU,GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GENBA_CD.Size = new System.Drawing.Size(50, 20);
            this.GENBA_CD.TabIndex = 683;
            this.GENBA_CD.Tag = "排出事業場を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GENBA_CD.ZeroPaddengFlag = true;
            // 
            // GYOUSHA_CD
            // 
            this.GYOUSHA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.GYOUSHA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_CD.ChangeUpperCase = true;
            this.GYOUSHA_CD.CharacterLimitList = null;
            this.GYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_CD.DisplayItemName = "排出事業者";
            this.GYOUSHA_CD.DisplayPopUp = null;
            this.GYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.FocusOutCheckMethod")));
            this.GYOUSHA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_CD.GetCodeMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GYOUSHA_CD.IsInputErrorOccured = false;
            this.GYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.GYOUSHA_CD.Location = new System.Drawing.Point(117, 86);
            this.GYOUSHA_CD.MaxLength = 6;
            this.GYOUSHA_CD.Name = "GYOUSHA_CD";
            this.GYOUSHA_CD.PopupAfterExecute = null;
            this.GYOUSHA_CD.PopupAfterExecuteMethod = "GYOUSHA_PopupAfterExecuteMethod";
            this.GYOUSHA_CD.PopupBeforeExecute = null;
            this.GYOUSHA_CD.PopupBeforeExecuteMethod = "GYOUSHA_PopupBeforeExecuteMethod";
            this.GYOUSHA_CD.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_CD.PopupSearchSendParams")));
            this.GYOUSHA_CD.PopupSetFormField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_CD.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_CD.popupWindowSetting")));
            this.GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.RegistCheckMethod")));
            this.GYOUSHA_CD.SetFormField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.Size = new System.Drawing.Size(50, 20);
            this.GYOUSHA_CD.TabIndex = 682;
            this.GYOUSHA_CD.Tag = "排出事業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GYOUSHA_CD.ZeroPaddengFlag = true;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(4, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 20);
            this.label5.TabIndex = 687;
            this.label5.Text = "運搬業者";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(506, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 20);
            this.label7.TabIndex = 686;
            this.label7.Text = "排出事業場";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(4, 86);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 20);
            this.label8.TabIndex = 685;
            this.label8.Text = "排出事業者";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GENBA_NAME_RYAKU
            // 
            this.GENBA_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GENBA_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_NAME_RYAKU.DBFieldsName = "GENBA_NAME_RYAKU";
            this.GENBA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_NAME_RYAKU.DisplayPopUp = null;
            this.GENBA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_RYAKU.FocusOutCheckMethod")));
            this.GENBA_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GENBA_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.GENBA_NAME_RYAKU.IsInputErrorOccured = false;
            this.GENBA_NAME_RYAKU.Location = new System.Drawing.Point(658, 86);
            this.GENBA_NAME_RYAKU.Name = "GENBA_NAME_RYAKU";
            this.GENBA_NAME_RYAKU.PopupAfterExecute = null;
            this.GENBA_NAME_RYAKU.PopupBeforeExecute = null;
            this.GENBA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_NAME_RYAKU.PopupSearchSendParams")));
            this.GENBA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_NAME_RYAKU.popupWindowSetting")));
            this.GENBA_NAME_RYAKU.ReadOnly = true;
            this.GENBA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_RYAKU.RegistCheckMethod")));
            this.GENBA_NAME_RYAKU.Size = new System.Drawing.Size(286, 20);
            this.GENBA_NAME_RYAKU.TabIndex = 690;
            this.GENBA_NAME_RYAKU.TabStop = false;
            // 
            // SAISHUU_SHOBUNJOU_SEARCH_BUTTON
            // 
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.BackColor = System.Drawing.SystemColors.Control;
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.DBFieldsName = null;
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.DefaultBackColor = System.Drawing.Color.Empty;
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.DisplayItemName = null;
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.DisplayPopUp = null;
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.ErrorMessage = null;
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SAISHUU_SHOBUNJOU_SEARCH_BUTTON.FocusOutCheckMethod")));
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.GetCodeMasterField = null;
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.Image = ((System.Drawing.Image)(resources.GetObject("SAISHUU_SHOBUNJOU_SEARCH_BUTTON.Image")));
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.ItemDefinedTypes = null;
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.LinkedSettingTextBox = null;
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.LinkedTextBoxs = new string[] {
        "GyoushaCode"};
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.Location = new System.Drawing.Point(947, 150);
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.Name = "SAISHUU_SHOBUNJOU_SEARCH_BUTTON";
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.PopupAfterExecute = null;
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.PopupAfterExecuteMethod = "PopupAfterGyoushaCode";
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.PopupGetMasterField = "GENBA_CD, GENBA_NAME_RYAKU,GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SAISHUU_SHOBUNJOU_SEARCH_BUTTON.PopupSearchSendParams")));
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.PopupSetFormField = "SAISHUU_SHOBUNJOU_CD, SAISHUU_SHOBUNJOU_NAME,SHOBUN_JYUTAKUSHA_SAISHU_CD,SHOBUN_J" +
    "YUTAKUSHA_SAISHU_NAME";
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SAISHUU_SHOBUNJOU_SEARCH_BUTTON.popupWindowSetting")));
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SAISHUU_SHOBUNJOU_SEARCH_BUTTON.RegistCheckMethod")));
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.SearchDisplayFlag = 0;
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.SetFormField = null;
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.ShortItemName = null;
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.Size = new System.Drawing.Size(30, 20);
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.TabIndex = 707;
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.TabStop = false;
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.Tag = "最終処分場画面を表示します";
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.UseVisualStyleBackColor = false;
            this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON.ZeroPaddengFlag = false;
            // 
            // SHOBUN_GENBA_SEARCH_BUTTON
            // 
            this.SHOBUN_GENBA_SEARCH_BUTTON.BackColor = System.Drawing.SystemColors.Control;
            this.SHOBUN_GENBA_SEARCH_BUTTON.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SHOBUN_GENBA_SEARCH_BUTTON.DBFieldsName = null;
            this.SHOBUN_GENBA_SEARCH_BUTTON.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHOBUN_GENBA_SEARCH_BUTTON.DisplayItemName = null;
            this.SHOBUN_GENBA_SEARCH_BUTTON.DisplayPopUp = null;
            this.SHOBUN_GENBA_SEARCH_BUTTON.ErrorMessage = null;
            this.SHOBUN_GENBA_SEARCH_BUTTON.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_GENBA_SEARCH_BUTTON.FocusOutCheckMethod")));
            this.SHOBUN_GENBA_SEARCH_BUTTON.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHOBUN_GENBA_SEARCH_BUTTON.GetCodeMasterField = null;
            this.SHOBUN_GENBA_SEARCH_BUTTON.Image = ((System.Drawing.Image)(resources.GetObject("SHOBUN_GENBA_SEARCH_BUTTON.Image")));
            this.SHOBUN_GENBA_SEARCH_BUTTON.ItemDefinedTypes = null;
            this.SHOBUN_GENBA_SEARCH_BUTTON.LinkedSettingTextBox = null;
            this.SHOBUN_GENBA_SEARCH_BUTTON.LinkedTextBoxs = new string[] {
        "GyoushaCode"};
            this.SHOBUN_GENBA_SEARCH_BUTTON.Location = new System.Drawing.Point(947, 128);
            this.SHOBUN_GENBA_SEARCH_BUTTON.Name = "SHOBUN_GENBA_SEARCH_BUTTON";
            this.SHOBUN_GENBA_SEARCH_BUTTON.PopupAfterExecute = null;
            this.SHOBUN_GENBA_SEARCH_BUTTON.PopupAfterExecuteMethod = "PopupAfterGyoushaCode";
            this.SHOBUN_GENBA_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.SHOBUN_GENBA_SEARCH_BUTTON.PopupGetMasterField = "GENBA_CD, GENBA_NAME_RYAKU,GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.SHOBUN_GENBA_SEARCH_BUTTON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHOBUN_GENBA_SEARCH_BUTTON.PopupSearchSendParams")));
            this.SHOBUN_GENBA_SEARCH_BUTTON.PopupSetFormField = "SHOBUN_GENBA_CD, SHOBUN_GENBA_NAME_RYAKU,SHOBUN_JYUTAKUSHA_SHOBUN_CD, SHOBUN_JYUT" +
    "AKUSHA_SHOBUN_NAME";
            this.SHOBUN_GENBA_SEARCH_BUTTON.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.SHOBUN_GENBA_SEARCH_BUTTON.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.SHOBUN_GENBA_SEARCH_BUTTON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHOBUN_GENBA_SEARCH_BUTTON.popupWindowSetting")));
            this.SHOBUN_GENBA_SEARCH_BUTTON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_GENBA_SEARCH_BUTTON.RegistCheckMethod")));
            this.SHOBUN_GENBA_SEARCH_BUTTON.SearchDisplayFlag = 0;
            this.SHOBUN_GENBA_SEARCH_BUTTON.SetFormField = null;
            this.SHOBUN_GENBA_SEARCH_BUTTON.ShortItemName = null;
            this.SHOBUN_GENBA_SEARCH_BUTTON.Size = new System.Drawing.Size(30, 20);
            this.SHOBUN_GENBA_SEARCH_BUTTON.TabIndex = 706;
            this.SHOBUN_GENBA_SEARCH_BUTTON.TabStop = false;
            this.SHOBUN_GENBA_SEARCH_BUTTON.Tag = "処分事業場画面を表示します";
            this.SHOBUN_GENBA_SEARCH_BUTTON.UseVisualStyleBackColor = false;
            this.SHOBUN_GENBA_SEARCH_BUTTON.ZeroPaddengFlag = false;
            // 
            // JYUTAKUSHA_SAISHU_SEARCH_BUTTON
            // 
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.BackColor = System.Drawing.SystemColors.Control;
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.DBFieldsName = null;
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.DefaultBackColor = System.Drawing.Color.Empty;
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.DisplayItemName = null;
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.DisplayPopUp = null;
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.ErrorMessage = null;
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JYUTAKUSHA_SAISHU_SEARCH_BUTTON.FocusOutCheckMethod")));
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.GetCodeMasterField = null;
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.Image = ((System.Drawing.Image)(resources.GetObject("JYUTAKUSHA_SAISHU_SEARCH_BUTTON.Image")));
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.ItemDefinedTypes = null;
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.LinkedSettingTextBox = null;
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.LinkedTextBoxs = new string[] {
        "GyoushaCode"};
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.Location = new System.Drawing.Point(457, 153);
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.Name = "JYUTAKUSHA_SAISHU_SEARCH_BUTTON";
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.PopupAfterExecute = null;
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.PopupAfterExecuteMethod = "SHOBUN_JYUTAKUSHA_SAISHU_PopupAfterExecuteMethod";
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.PopupBeforeExecuteMethod = "SHOBUN_JYUTAKUSHA_SAISHU_PopupBeforeExecuteMethod";
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JYUTAKUSHA_SAISHU_SEARCH_BUTTON.PopupSearchSendParams")));
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.PopupSetFormField = "SHOBUN_JYUTAKUSHA_SAISHU_CD,SHOBUN_JYUTAKUSHA_SAISHU_NAME";
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.PopupWindowName = "検索共通ポップアップ";
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JYUTAKUSHA_SAISHU_SEARCH_BUTTON.popupWindowSetting")));
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JYUTAKUSHA_SAISHU_SEARCH_BUTTON.RegistCheckMethod")));
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.SearchDisplayFlag = 0;
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.SetFormField = null;
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.ShortItemName = null;
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.Size = new System.Drawing.Size(30, 20);
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.TabIndex = 709;
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.TabStop = false;
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.Tag = "処分受託者(最終)検索画面を表示します";
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.UseVisualStyleBackColor = false;
            this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON.ZeroPaddengFlag = false;
            // 
            // JYUTAKUSHA_SHOBUN_SEARCH_BUTTON
            // 
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.BackColor = System.Drawing.SystemColors.Control;
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.DBFieldsName = null;
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.DefaultBackColor = System.Drawing.Color.Empty;
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.DisplayItemName = null;
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.DisplayPopUp = null;
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.ErrorMessage = null;
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.FocusOutCheckMethod")));
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.GetCodeMasterField = null;
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.Image = ((System.Drawing.Image)(resources.GetObject("JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.Image")));
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.ItemDefinedTypes = null;
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.LinkedSettingTextBox = null;
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.LinkedTextBoxs = new string[] {
        "GyoushaCode"};
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.Location = new System.Drawing.Point(457, 131);
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.Name = "JYUTAKUSHA_SHOBUN_SEARCH_BUTTON";
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.PopupAfterExecute = null;
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.PopupAfterExecuteMethod = "SHOBUN_JYUTAKUSHA_SHOBUN_PopupAfterExecuteMethod";
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.PopupBeforeExecuteMethod = "SHOBUN_JYUTAKUSHA_SHOBUN_PopupBeforeExecuteMethod";
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.PopupSearchSendParams")));
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.PopupSetFormField = "SHOBUN_JYUTAKUSHA_SHOBUN_CD, SHOBUN_JYUTAKUSHA_SHOBUN_NAME";
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.PopupWindowName = "検索共通ポップアップ";
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.popupWindowSetting")));
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.RegistCheckMethod")));
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.SearchDisplayFlag = 0;
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.SetFormField = null;
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.ShortItemName = null;
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.Size = new System.Drawing.Size(30, 20);
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.TabIndex = 708;
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.TabStop = false;
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.Tag = "処分受託者(処分)検索画面を表示します";
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.UseVisualStyleBackColor = false;
            this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON.ZeroPaddengFlag = false;
            // 
            // SAISHUU_SHOBUNJOU_NAME
            // 
            this.SAISHUU_SHOBUNJOU_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.SAISHUU_SHOBUNJOU_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SAISHUU_SHOBUNJOU_NAME.DBFieldsName = "SAISHUU_SHOBUNJOU_NAME";
            this.SAISHUU_SHOBUNJOU_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.SAISHUU_SHOBUNJOU_NAME.DisplayPopUp = null;
            this.SAISHUU_SHOBUNJOU_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SAISHUU_SHOBUNJOU_NAME.FocusOutCheckMethod")));
            this.SAISHUU_SHOBUNJOU_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SAISHUU_SHOBUNJOU_NAME.ForeColor = System.Drawing.Color.Black;
            this.SAISHUU_SHOBUNJOU_NAME.IsInputErrorOccured = false;
            this.SAISHUU_SHOBUNJOU_NAME.Location = new System.Drawing.Point(659, 150);
            this.SAISHUU_SHOBUNJOU_NAME.Name = "SAISHUU_SHOBUNJOU_NAME";
            this.SAISHUU_SHOBUNJOU_NAME.PopupAfterExecute = null;
            this.SAISHUU_SHOBUNJOU_NAME.PopupBeforeExecute = null;
            this.SAISHUU_SHOBUNJOU_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SAISHUU_SHOBUNJOU_NAME.PopupSearchSendParams")));
            this.SAISHUU_SHOBUNJOU_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SAISHUU_SHOBUNJOU_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SAISHUU_SHOBUNJOU_NAME.popupWindowSetting")));
            this.SAISHUU_SHOBUNJOU_NAME.ReadOnly = true;
            this.SAISHUU_SHOBUNJOU_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SAISHUU_SHOBUNJOU_NAME.RegistCheckMethod")));
            this.SAISHUU_SHOBUNJOU_NAME.Size = new System.Drawing.Size(286, 20);
            this.SAISHUU_SHOBUNJOU_NAME.TabIndex = 703;
            this.SAISHUU_SHOBUNJOU_NAME.TabStop = false;
            // 
            // SHOBUN_GENBA_NAME_RYAKU
            // 
            this.SHOBUN_GENBA_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.SHOBUN_GENBA_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHOBUN_GENBA_NAME_RYAKU.DBFieldsName = "SHOBUN_GENBA_NAME_RYAKU";
            this.SHOBUN_GENBA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHOBUN_GENBA_NAME_RYAKU.DisplayPopUp = null;
            this.SHOBUN_GENBA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_GENBA_NAME_RYAKU.FocusOutCheckMethod")));
            this.SHOBUN_GENBA_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHOBUN_GENBA_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.SHOBUN_GENBA_NAME_RYAKU.IsInputErrorOccured = false;
            this.SHOBUN_GENBA_NAME_RYAKU.Location = new System.Drawing.Point(659, 128);
            this.SHOBUN_GENBA_NAME_RYAKU.Name = "SHOBUN_GENBA_NAME_RYAKU";
            this.SHOBUN_GENBA_NAME_RYAKU.PopupAfterExecute = null;
            this.SHOBUN_GENBA_NAME_RYAKU.PopupBeforeExecute = null;
            this.SHOBUN_GENBA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHOBUN_GENBA_NAME_RYAKU.PopupSearchSendParams")));
            this.SHOBUN_GENBA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHOBUN_GENBA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHOBUN_GENBA_NAME_RYAKU.popupWindowSetting")));
            this.SHOBUN_GENBA_NAME_RYAKU.ReadOnly = true;
            this.SHOBUN_GENBA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_GENBA_NAME_RYAKU.RegistCheckMethod")));
            this.SHOBUN_GENBA_NAME_RYAKU.Size = new System.Drawing.Size(286, 20);
            this.SHOBUN_GENBA_NAME_RYAKU.TabIndex = 702;
            this.SHOBUN_GENBA_NAME_RYAKU.TabStop = false;
            // 
            // SHOBUN_JYUTAKUSHA_SAISHU_NAME
            // 
            this.SHOBUN_JYUTAKUSHA_SAISHU_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.SHOBUN_JYUTAKUSHA_SAISHU_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHOBUN_JYUTAKUSHA_SAISHU_NAME.DBFieldsName = "GENBA_NAME_RYAKU";
            this.SHOBUN_JYUTAKUSHA_SAISHU_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHOBUN_JYUTAKUSHA_SAISHU_NAME.DisplayPopUp = null;
            this.SHOBUN_JYUTAKUSHA_SAISHU_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_JYUTAKUSHA_SAISHU_NAME.FocusOutCheckMethod")));
            this.SHOBUN_JYUTAKUSHA_SAISHU_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHOBUN_JYUTAKUSHA_SAISHU_NAME.ForeColor = System.Drawing.Color.Black;
            this.SHOBUN_JYUTAKUSHA_SAISHU_NAME.IsInputErrorOccured = false;
            this.SHOBUN_JYUTAKUSHA_SAISHU_NAME.Location = new System.Drawing.Point(169, 152);
            this.SHOBUN_JYUTAKUSHA_SAISHU_NAME.Name = "SHOBUN_JYUTAKUSHA_SAISHU_NAME";
            this.SHOBUN_JYUTAKUSHA_SAISHU_NAME.PopupAfterExecute = null;
            this.SHOBUN_JYUTAKUSHA_SAISHU_NAME.PopupBeforeExecute = null;
            this.SHOBUN_JYUTAKUSHA_SAISHU_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHOBUN_JYUTAKUSHA_SAISHU_NAME.PopupSearchSendParams")));
            this.SHOBUN_JYUTAKUSHA_SAISHU_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHOBUN_JYUTAKUSHA_SAISHU_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHOBUN_JYUTAKUSHA_SAISHU_NAME.popupWindowSetting")));
            this.SHOBUN_JYUTAKUSHA_SAISHU_NAME.ReadOnly = true;
            this.SHOBUN_JYUTAKUSHA_SAISHU_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_JYUTAKUSHA_SAISHU_NAME.RegistCheckMethod")));
            this.SHOBUN_JYUTAKUSHA_SAISHU_NAME.Size = new System.Drawing.Size(286, 20);
            this.SHOBUN_JYUTAKUSHA_SAISHU_NAME.TabIndex = 705;
            this.SHOBUN_JYUTAKUSHA_SAISHU_NAME.TabStop = false;
            // 
            // SHOBUN_JYUTAKUSHA_SHOBUN_NAME
            // 
            this.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.DBFieldsName = "GENBA_NAME_RYAKU";
            this.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.DisplayPopUp = null;
            this.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_JYUTAKUSHA_SHOBUN_NAME.FocusOutCheckMethod")));
            this.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.ForeColor = System.Drawing.Color.Black;
            this.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.IsInputErrorOccured = false;
            this.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.Location = new System.Drawing.Point(169, 130);
            this.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.Name = "SHOBUN_JYUTAKUSHA_SHOBUN_NAME";
            this.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.PopupAfterExecute = null;
            this.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.PopupBeforeExecute = null;
            this.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHOBUN_JYUTAKUSHA_SHOBUN_NAME.PopupSearchSendParams")));
            this.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHOBUN_JYUTAKUSHA_SHOBUN_NAME.popupWindowSetting")));
            this.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.ReadOnly = true;
            this.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_JYUTAKUSHA_SHOBUN_NAME.RegistCheckMethod")));
            this.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.Size = new System.Drawing.Size(286, 20);
            this.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.TabIndex = 704;
            this.SHOBUN_JYUTAKUSHA_SHOBUN_NAME.TabStop = false;
            // 
            // SAISHUU_SHOBUNJOU_CD
            // 
            this.SAISHUU_SHOBUNJOU_CD.BackColor = System.Drawing.SystemColors.Window;
            this.SAISHUU_SHOBUNJOU_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SAISHUU_SHOBUNJOU_CD.ChangeUpperCase = true;
            this.SAISHUU_SHOBUNJOU_CD.CharacterLimitList = null;
            this.SAISHUU_SHOBUNJOU_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.SAISHUU_SHOBUNJOU_CD.DisplayItemName = "最終処分場";
            this.SAISHUU_SHOBUNJOU_CD.DisplayPopUp = null;
            this.SAISHUU_SHOBUNJOU_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SAISHUU_SHOBUNJOU_CD.FocusOutCheckMethod")));
            this.SAISHUU_SHOBUNJOU_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SAISHUU_SHOBUNJOU_CD.ForeColor = System.Drawing.Color.Black;
            this.SAISHUU_SHOBUNJOU_CD.GetCodeMasterField = "GENBA_CD, GENBA_NAME_RYAKU,GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.SAISHUU_SHOBUNJOU_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SAISHUU_SHOBUNJOU_CD.IsInputErrorOccured = false;
            this.SAISHUU_SHOBUNJOU_CD.ItemDefinedTypes = "varchar";
            this.SAISHUU_SHOBUNJOU_CD.Location = new System.Drawing.Point(607, 150);
            this.SAISHUU_SHOBUNJOU_CD.MaxLength = 6;
            this.SAISHUU_SHOBUNJOU_CD.Name = "SAISHUU_SHOBUNJOU_CD";
            this.SAISHUU_SHOBUNJOU_CD.PopupAfterExecute = null;
            this.SAISHUU_SHOBUNJOU_CD.PopupBeforeExecute = null;
            this.SAISHUU_SHOBUNJOU_CD.PopupGetMasterField = "GENBA_CD, GENBA_NAME_RYAKU,GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.SAISHUU_SHOBUNJOU_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SAISHUU_SHOBUNJOU_CD.PopupSearchSendParams")));
            this.SAISHUU_SHOBUNJOU_CD.PopupSetFormField = "SAISHUU_SHOBUNJOU_CD, SAISHUU_SHOBUNJOU_NAME,SHOBUN_JYUTAKUSHA_SAISHU_CD,SHOBUN_J" +
    "YUTAKUSHA_SAISHU_NAME";
            this.SAISHUU_SHOBUNJOU_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.SAISHUU_SHOBUNJOU_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.SAISHUU_SHOBUNJOU_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SAISHUU_SHOBUNJOU_CD.popupWindowSetting")));
            this.SAISHUU_SHOBUNJOU_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SAISHUU_SHOBUNJOU_CD.RegistCheckMethod")));
            this.SAISHUU_SHOBUNJOU_CD.SetFormField = "SAISHUU_SHOBUNJOU_CD, SAISHUU_SHOBUNJOU_NAME,SHOBUN_JYUTAKUSHA_SAISHU_CD,SHOBUN_J" +
    "YUTAKUSHA_SAISHU_NAME";
            this.SAISHUU_SHOBUNJOU_CD.Size = new System.Drawing.Size(50, 20);
            this.SAISHUU_SHOBUNJOU_CD.TabIndex = 697;
            this.SAISHUU_SHOBUNJOU_CD.Tag = "最終処分場を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.SAISHUU_SHOBUNJOU_CD.ZeroPaddengFlag = true;
            // 
            // SHOBUN_GENBA_CD
            // 
            this.SHOBUN_GENBA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.SHOBUN_GENBA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHOBUN_GENBA_CD.ChangeUpperCase = true;
            this.SHOBUN_GENBA_CD.CharacterLimitList = null;
            this.SHOBUN_GENBA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHOBUN_GENBA_CD.DisplayItemName = "処分事業場";
            this.SHOBUN_GENBA_CD.DisplayPopUp = null;
            this.SHOBUN_GENBA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_GENBA_CD.FocusOutCheckMethod")));
            this.SHOBUN_GENBA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHOBUN_GENBA_CD.ForeColor = System.Drawing.Color.Black;
            this.SHOBUN_GENBA_CD.GetCodeMasterField = "GENBA_CD, GENBA_NAME_RYAKU,GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.SHOBUN_GENBA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SHOBUN_GENBA_CD.IsInputErrorOccured = false;
            this.SHOBUN_GENBA_CD.ItemDefinedTypes = "varchar";
            this.SHOBUN_GENBA_CD.Location = new System.Drawing.Point(607, 128);
            this.SHOBUN_GENBA_CD.MaxLength = 6;
            this.SHOBUN_GENBA_CD.Name = "SHOBUN_GENBA_CD";
            this.SHOBUN_GENBA_CD.PopupAfterExecute = null;
            this.SHOBUN_GENBA_CD.PopupBeforeExecute = null;
            this.SHOBUN_GENBA_CD.PopupGetMasterField = "GENBA_CD, GENBA_NAME_RYAKU,GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.SHOBUN_GENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHOBUN_GENBA_CD.PopupSearchSendParams")));
            this.SHOBUN_GENBA_CD.PopupSetFormField = "SHOBUN_GENBA_CD, SHOBUN_GENBA_NAME_RYAKU,SHOBUN_JYUTAKUSHA_SHOBUN_CD, SHOBUN_JYUT" +
    "AKUSHA_SHOBUN_NAME";
            this.SHOBUN_GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.SHOBUN_GENBA_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.SHOBUN_GENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHOBUN_GENBA_CD.popupWindowSetting")));
            this.SHOBUN_GENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_GENBA_CD.RegistCheckMethod")));
            this.SHOBUN_GENBA_CD.SetFormField = "SHOBUN_GENBA_CD, SHOBUN_GENBA_NAME_RYAKU,SHOBUN_JYUTAKUSHA_SHOBUN_CD, SHOBUN_JYUT" +
    "AKUSHA_SHOBUN_NAME";
            this.SHOBUN_GENBA_CD.Size = new System.Drawing.Size(50, 20);
            this.SHOBUN_GENBA_CD.TabIndex = 695;
            this.SHOBUN_GENBA_CD.Tag = "処分事業場を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.SHOBUN_GENBA_CD.ZeroPaddengFlag = true;
            // 
            // SHOBUN_JYUTAKUSHA_SAISHU_CD
            // 
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.BackColor = System.Drawing.SystemColors.Window;
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.ChangeUpperCase = true;
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.CharacterLimitList = null;
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.DisplayItemName = "処分受託者(最終)";
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.DisplayPopUp = null;
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_JYUTAKUSHA_SAISHU_CD.FocusOutCheckMethod")));
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.ForeColor = System.Drawing.Color.Black;
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.GetCodeMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.IsInputErrorOccured = false;
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.ItemDefinedTypes = "varchar";
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.Location = new System.Drawing.Point(117, 152);
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.MaxLength = 6;
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.Name = "SHOBUN_JYUTAKUSHA_SAISHU_CD";
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.PopupAfterExecute = null;
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.PopupAfterExecuteMethod = "SHOBUN_JYUTAKUSHA_SAISHU_PopupAfterExecuteMethod";
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.PopupBeforeExecute = null;
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.PopupBeforeExecuteMethod = "SHOBUN_JYUTAKUSHA_SAISHU_PopupBeforeExecuteMethod";
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHOBUN_JYUTAKUSHA_SAISHU_CD.PopupSearchSendParams")));
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.PopupSetFormField = "SHOBUN_JYUTAKUSHA_SAISHU_CD,SHOBUN_JYUTAKUSHA_SAISHU_NAME";
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.PopupWindowName = "検索共通ポップアップ";
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHOBUN_JYUTAKUSHA_SAISHU_CD.popupWindowSetting")));
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_JYUTAKUSHA_SAISHU_CD.RegistCheckMethod")));
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.SetFormField = "SHOBUN_JYUTAKUSHA_SAISHU_CD,SHOBUN_JYUTAKUSHA_SAISHU_NAME";
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.Size = new System.Drawing.Size(50, 20);
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.TabIndex = 696;
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.Tag = "処分受託者(最終)を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.SHOBUN_JYUTAKUSHA_SAISHU_CD.ZeroPaddengFlag = true;
            // 
            // SHOBUN_JYUTAKUSHA_SHOBUN_CD
            // 
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.ChangeUpperCase = true;
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.CharacterLimitList = null;
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.DisplayItemName = "処分受託者(処分)";
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.DisplayPopUp = null;
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_JYUTAKUSHA_SHOBUN_CD.FocusOutCheckMethod")));
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.ForeColor = System.Drawing.Color.Black;
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.GetCodeMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.IsInputErrorOccured = false;
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.ItemDefinedTypes = "varchar";
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Location = new System.Drawing.Point(117, 130);
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.MaxLength = 6;
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Name = "SHOBUN_JYUTAKUSHA_SHOBUN_CD";
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.PopupAfterExecute = null;
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.PopupAfterExecuteMethod = "SHOBUN_JYUTAKUSHA_SHOBUN_PopupAfterExecuteMethod";
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.PopupBeforeExecute = null;
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.PopupBeforeExecuteMethod = "SHOBUN_JYUTAKUSHA_SHOBUN_PopupBeforeExecuteMethod";
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHOBUN_JYUTAKUSHA_SHOBUN_CD.PopupSearchSendParams")));
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.PopupSetFormField = "SHOBUN_JYUTAKUSHA_SHOBUN_CD, SHOBUN_JYUTAKUSHA_SHOBUN_NAME";
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.PopupWindowName = "検索共通ポップアップ";
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHOBUN_JYUTAKUSHA_SHOBUN_CD.popupWindowSetting")));
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_JYUTAKUSHA_SHOBUN_CD.RegistCheckMethod")));
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.SetFormField = "SHOBUN_JYUTAKUSHA_SHOBUN_CD, SHOBUN_JYUTAKUSHA_SHOBUN_NAME";
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Size = new System.Drawing.Size(50, 20);
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.TabIndex = 694;
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.Tag = "処分受託者(処分)を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.SHOBUN_JYUTAKUSHA_SHOBUN_CD.ZeroPaddengFlag = true;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(506, 128);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(94, 20);
            this.label9.TabIndex = 701;
            this.label9.Text = "処分事業場";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label14.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(506, 150);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(94, 20);
            this.label14.TabIndex = 700;
            this.label14.Text = "最終処分場";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label10.Font = new System.Drawing.Font("ＭＳ ゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(4, 152);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(110, 20);
            this.label10.TabIndex = 699;
            this.label10.Text = "処分受託者(最終)";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label11.Font = new System.Drawing.Font("ＭＳ ゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(4, 130);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(110, 20);
            this.label11.TabIndex = 698;
            this.label11.Text = "処分受託者(処分)";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1022, 490);
            this.Controls.Add(this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON);
            this.Controls.Add(this.SHOBUN_GENBA_SEARCH_BUTTON);
            this.Controls.Add(this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON);
            this.Controls.Add(this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON);
            this.Controls.Add(this.SAISHUU_SHOBUNJOU_NAME);
            this.Controls.Add(this.SHOBUN_GENBA_NAME_RYAKU);
            this.Controls.Add(this.SHOBUN_JYUTAKUSHA_SAISHU_NAME);
            this.Controls.Add(this.SHOBUN_JYUTAKUSHA_SHOBUN_NAME);
            this.Controls.Add(this.SAISHUU_SHOBUNJOU_CD);
            this.Controls.Add(this.SHOBUN_GENBA_CD);
            this.Controls.Add(this.SHOBUN_JYUTAKUSHA_SAISHU_CD);
            this.Controls.Add(this.SHOBUN_JYUTAKUSHA_SHOBUN_CD);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.UNPANGYOUSHA_SEARCH_BUTTON);
            this.Controls.Add(this.GENBA_SEARCH_BUTTON);
            this.Controls.Add(this.GYOUSHA_SEARCH_BUTTON);
            this.Controls.Add(this.UNPANGYOUSHA_NAME);
            this.Controls.Add(this.GYOUSHA_NAME_RYAKU);
            this.Controls.Add(this.UNPANGYOUSHA_CD);
            this.Controls.Add(this.GENBA_CD);
            this.Controls.Add(this.GYOUSHA_CD);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.GENBA_NAME_RYAKU);
            this.Controls.Add(this.DATE_TO);
            this.Controls.Add(this.DATE_FROM);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DATE_SELECT_7);
            this.Controls.Add(this.DATE_SELECT_6);
            this.Controls.Add(this.DATE_SELECT_5);
            this.Controls.Add(this.DATE_SELECT_4);
            this.Controls.Add(this.DATE_SELECT_3);
            this.Controls.Add(this.DATE_SELECT_2);
            this.Controls.Add(this.DATE_SELECT_1);
            this.Controls.Add(this.DATE_SELECT);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.KYOKASHOU_SHURUI_NAME);
            this.Controls.Add(this.KYOKASHOU_SHURUI_CD);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_DELETED);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.KEIYAKUSHO_SEARCH_BUTTON);
            this.Controls.Add(this.KEIYAKUSHO_SHURUI_NAME);
            this.Controls.Add(this.KEIYAKUSHO_SHURUI_CD);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.KEIYAKU_JYOUKYOU_NAME);
            this.Controls.Add(this.KEIYAKU_JYOUKYOU_CD);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ITAKU_KEIYAKU_NO);
            this.Controls.Add(this.label19);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Name = "UIForm";
            this.PreviousValue = "";
            this.Text = "UIForm";
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.label19, 0);
            this.Controls.SetChildIndex(this.ITAKU_KEIYAKU_NO, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.KEIYAKU_JYOUKYOU_CD, 0);
            this.Controls.SetChildIndex(this.KEIYAKU_JYOUKYOU_NAME, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.KEIYAKUSHO_SHURUI_CD, 0);
            this.Controls.SetChildIndex(this.KEIYAKUSHO_SHURUI_NAME, 0);
            this.Controls.SetChildIndex(this.KEIYAKUSHO_SEARCH_BUTTON, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.ICHIRAN_HYOUJI_JOUKEN_DELETED, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.KYOKASHOU_SHURUI_CD, 0);
            this.Controls.SetChildIndex(this.KYOKASHOU_SHURUI_NAME, 0);
            this.Controls.SetChildIndex(this.labelInfo, 0);
            this.Controls.SetChildIndex(this.DATE_SELECT, 0);
            this.Controls.SetChildIndex(this.DATE_SELECT_1, 0);
            this.Controls.SetChildIndex(this.DATE_SELECT_2, 0);
            this.Controls.SetChildIndex(this.DATE_SELECT_3, 0);
            this.Controls.SetChildIndex(this.DATE_SELECT_4, 0);
            this.Controls.SetChildIndex(this.DATE_SELECT_5, 0);
            this.Controls.SetChildIndex(this.DATE_SELECT_6, 0);
            this.Controls.SetChildIndex(this.DATE_SELECT_7, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.DATE_FROM, 0);
            this.Controls.SetChildIndex(this.DATE_TO, 0);
            this.Controls.SetChildIndex(this.GENBA_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_CD, 0);
            this.Controls.SetChildIndex(this.GENBA_CD, 0);
            this.Controls.SetChildIndex(this.UNPANGYOUSHA_CD, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.UNPANGYOUSHA_NAME, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_SEARCH_BUTTON, 0);
            this.Controls.SetChildIndex(this.GENBA_SEARCH_BUTTON, 0);
            this.Controls.SetChildIndex(this.UNPANGYOUSHA_SEARCH_BUTTON, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.label14, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.SHOBUN_JYUTAKUSHA_SHOBUN_CD, 0);
            this.Controls.SetChildIndex(this.SHOBUN_JYUTAKUSHA_SAISHU_CD, 0);
            this.Controls.SetChildIndex(this.SHOBUN_GENBA_CD, 0);
            this.Controls.SetChildIndex(this.SAISHUU_SHOBUNJOU_CD, 0);
            this.Controls.SetChildIndex(this.SHOBUN_JYUTAKUSHA_SHOBUN_NAME, 0);
            this.Controls.SetChildIndex(this.SHOBUN_JYUTAKUSHA_SAISHU_NAME, 0);
            this.Controls.SetChildIndex(this.SHOBUN_GENBA_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.SAISHUU_SHOBUNJOU_NAME, 0);
            this.Controls.SetChildIndex(this.JYUTAKUSHA_SHOBUN_SEARCH_BUTTON, 0);
            this.Controls.SetChildIndex(this.JYUTAKUSHA_SAISHU_SEARCH_BUTTON, 0);
            this.Controls.SetChildIndex(this.SHOBUN_GENBA_SEARCH_BUTTON, 0);
            this.Controls.SetChildIndex(this.SAISHUU_SHOBUNJOU_SEARCH_BUTTON, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomTextBox ITAKU_KEIYAKU_NO;
        private System.Windows.Forms.Label label19;
        internal r_framework.CustomControl.CustomPopupOpenButton KEIYAKUSHO_SEARCH_BUTTON;
        internal r_framework.CustomControl.CustomTextBox KEIYAKUSHO_SHURUI_NAME;
        internal r_framework.CustomControl.CustomNumericTextBox2 KEIYAKUSHO_SHURUI_CD;
        internal System.Windows.Forms.Label label3;
        internal r_framework.CustomControl.CustomTextBox KEIYAKU_JYOUKYOU_NAME;
        internal r_framework.CustomControl.CustomNumericTextBox2 KEIYAKU_JYOUKYOU_CD;
        private System.Windows.Forms.Label label6;
        internal System.Windows.Forms.CheckBox ICHIRAN_HYOUJI_JOUKEN_DELETED;
        internal System.Windows.Forms.Label label2;
        internal r_framework.CustomControl.CustomTextBox KYOKASHOU_SHURUI_NAME;
        internal r_framework.CustomControl.CustomNumericTextBox2 KYOKASHOU_SHURUI_CD;
        private System.Windows.Forms.Label label1;
        internal r_framework.CustomControl.CustomRadioButton DATE_SELECT_7;
        internal r_framework.CustomControl.CustomRadioButton DATE_SELECT_6;
        internal r_framework.CustomControl.CustomRadioButton DATE_SELECT_5;
        internal r_framework.CustomControl.CustomRadioButton DATE_SELECT_4;
        internal r_framework.CustomControl.CustomRadioButton DATE_SELECT_3;
        internal r_framework.CustomControl.CustomRadioButton DATE_SELECT_2;
        internal r_framework.CustomControl.CustomRadioButton DATE_SELECT_1;
        internal r_framework.CustomControl.CustomNumericTextBox2 DATE_SELECT;
        internal System.Windows.Forms.Label labelInfo;
        internal r_framework.CustomControl.CustomDateTimePicker DATE_TO;
        internal r_framework.CustomControl.CustomDateTimePicker DATE_FROM;
        internal System.Windows.Forms.Label label4;
        internal r_framework.CustomControl.CustomPopupOpenButton UNPANGYOUSHA_SEARCH_BUTTON;
        internal r_framework.CustomControl.CustomPopupOpenButton GENBA_SEARCH_BUTTON;
        internal r_framework.CustomControl.CustomPopupOpenButton GYOUSHA_SEARCH_BUTTON;
        internal r_framework.CustomControl.CustomTextBox UNPANGYOUSHA_NAME;
        internal r_framework.CustomControl.CustomTextBox GYOUSHA_NAME_RYAKU;
        internal r_framework.CustomControl.CustomAlphaNumTextBox UNPANGYOUSHA_CD;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GENBA_CD;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GYOUSHA_CD;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        internal r_framework.CustomControl.CustomTextBox GENBA_NAME_RYAKU;
        internal r_framework.CustomControl.CustomPopupOpenButton SAISHUU_SHOBUNJOU_SEARCH_BUTTON;
        internal r_framework.CustomControl.CustomPopupOpenButton SHOBUN_GENBA_SEARCH_BUTTON;
        internal r_framework.CustomControl.CustomPopupOpenButton JYUTAKUSHA_SAISHU_SEARCH_BUTTON;
        internal r_framework.CustomControl.CustomPopupOpenButton JYUTAKUSHA_SHOBUN_SEARCH_BUTTON;
        internal r_framework.CustomControl.CustomTextBox SAISHUU_SHOBUNJOU_NAME;
        internal r_framework.CustomControl.CustomTextBox SHOBUN_GENBA_NAME_RYAKU;
        internal r_framework.CustomControl.CustomTextBox SHOBUN_JYUTAKUSHA_SAISHU_NAME;
        internal r_framework.CustomControl.CustomTextBox SHOBUN_JYUTAKUSHA_SHOBUN_NAME;
        internal r_framework.CustomControl.CustomAlphaNumTextBox SAISHUU_SHOBUNJOU_CD;
        internal r_framework.CustomControl.CustomAlphaNumTextBox SHOBUN_GENBA_CD;
        internal r_framework.CustomControl.CustomAlphaNumTextBox SHOBUN_JYUTAKUSHA_SAISHU_CD;
        internal r_framework.CustomControl.CustomAlphaNumTextBox SHOBUN_JYUTAKUSHA_SHOBUN_CD;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
    }
}