namespace Shougun.Core.ReceiptPayManagement.NyuukinDataTorikomi
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
            this.SEARCH_TORIHIKISAKI = new r_framework.CustomControl.CustomPopupOpenButton();
            this.TORIHIKISAKI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.TORIHIKISAKI_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.TORIHIKISAKI_LABEL = new System.Windows.Forms.Label();
            this.SEARCH_TORIKOMI = new r_framework.CustomControl.CustomButton();
            this.TORIKOMI = new r_framework.CustomControl.CustomTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SAKUSEI_BI_FROM = new r_framework.CustomControl.CustomDateTimePicker();
            this.SAKUSEI_BI_TO = new r_framework.CustomControl.CustomDateTimePicker();
            this.SAKUSEI_BI_LABEL = new System.Windows.Forms.Label();
            this.ALERT_DATE_FROM_TO_LABEL = new System.Windows.Forms.Label();
            this.customPanel7 = new r_framework.CustomControl.CustomPanel();
            this.SAKUSEI_KBN_3 = new r_framework.CustomControl.CustomRadioButton();
            this.SAKUSEI_KBN_2 = new r_framework.CustomControl.CustomRadioButton();
            this.SAKUSEI_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.SAKUSEI_KBN_1 = new r_framework.CustomControl.CustomRadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.BANK_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BANK_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.BANK_SHITEN_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.BANK_SHITEN_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.KOUZA_NO = new r_framework.CustomControl.CustomTextBox();
            this.KOUZA_SHURUI = new r_framework.CustomControl.CustomTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SEARCH_BANK = new r_framework.CustomControl.CustomPopupOpenButton();
            this.SEARCH_BANK_SHITEN = new r_framework.CustomControl.CustomPopupOpenButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SEL_FURIKOMI_JINMEI_1 = new r_framework.CustomControl.CustomTextBox();
            this.SEL_LB_FURIKOMI_JINMEI_1 = new System.Windows.Forms.Label();
            this.SEL_SETTEI_BTN_1 = new r_framework.CustomControl.CustomButton();
            this.SEL_SENTAKU_SAKI = new r_framework.CustomControl.CustomTextBox();
            this.SEL_LB_SENTAKU_SAKI = new System.Windows.Forms.Label();
            this.SEL_TORIHIKISAKI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.SEL_TORIHIKISAKI_BTN = new r_framework.CustomControl.CustomPopupOpenButton();
            this.SEL_TORIHIKISAKI_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.SEL_LB_TORIHIKISAKI = new System.Windows.Forms.Label();
            this.SEL_SETTEI_BTN_2 = new r_framework.CustomControl.CustomButton();
            this.SEL_FURIKOMI_JINMEI_2 = new r_framework.CustomControl.CustomTextBox();
            this.SEL_LB_FURIKOMI_JINMEI_2 = new System.Windows.Forms.Label();
            this.customPanel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.searchString.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchString.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.FocusOutCheckMethod")));
            this.searchString.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.searchString.Location = new System.Drawing.Point(3, 289);
            this.searchString.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("searchString.PopupSearchSendParams")));
            this.searchString.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("searchString.popupWindowSetting")));
            this.searchString.ReadOnly = true;
            this.searchString.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.RegistCheckMethod")));
            this.searchString.Size = new System.Drawing.Size(997, 95);
            this.searchString.TabIndex = 1;
            this.searchString.Visible = false;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.bt_ptn1.Location = new System.Drawing.Point(2, 444);
            this.bt_ptn1.Size = new System.Drawing.Size(192, 24);
            this.bt_ptn1.TabIndex = 80;
            this.bt_ptn1.Text = "パターン１";
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.bt_ptn2.Location = new System.Drawing.Point(203, 444);
            this.bt_ptn2.Size = new System.Drawing.Size(192, 24);
            this.bt_ptn2.TabIndex = 90;
            this.bt_ptn2.Text = "パターン２";
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.bt_ptn3.Location = new System.Drawing.Point(405, 444);
            this.bt_ptn3.Size = new System.Drawing.Size(192, 24);
            this.bt_ptn3.TabIndex = 100;
            this.bt_ptn3.Text = "パターン３";
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.bt_ptn4.Location = new System.Drawing.Point(607, 444);
            this.bt_ptn4.Size = new System.Drawing.Size(192, 24);
            this.bt_ptn4.TabIndex = 110;
            this.bt_ptn4.Text = "パターン４";
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.bt_ptn5.Location = new System.Drawing.Point(808, 444);
            this.bt_ptn5.Size = new System.Drawing.Size(192, 24);
            this.bt_ptn5.TabIndex = 120;
            this.bt_ptn5.Text = "パターン５";
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.Location = new System.Drawing.Point(3, 259);
            this.customSortHeader1.Size = new System.Drawing.Size(997, 24);
            this.customSortHeader1.Visible = false;
            // 
            // customSearchHeader1
            // 
            this.customSearchHeader1.Location = new System.Drawing.Point(3, 227);
            // 
            // SEARCH_TORIHIKISAKI
            // 
            this.SEARCH_TORIHIKISAKI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.SEARCH_TORIHIKISAKI.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SEARCH_TORIHIKISAKI.DBFieldsName = null;
            this.SEARCH_TORIHIKISAKI.DefaultBackColor = System.Drawing.Color.Empty;
            this.SEARCH_TORIHIKISAKI.DisplayItemName = "取引先CD";
            this.SEARCH_TORIHIKISAKI.DisplayPopUp = null;
            this.SEARCH_TORIHIKISAKI.ErrorMessage = null;
            this.SEARCH_TORIHIKISAKI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEARCH_TORIHIKISAKI.FocusOutCheckMethod")));
            this.SEARCH_TORIHIKISAKI.Font = new System.Drawing.Font("MS Gothic", 11.25F);
            this.SEARCH_TORIHIKISAKI.GetCodeMasterField = null;
            this.SEARCH_TORIHIKISAKI.Image = ((System.Drawing.Image)(resources.GetObject("SEARCH_TORIHIKISAKI.Image")));
            this.SEARCH_TORIHIKISAKI.ItemDefinedTypes = null;
            this.SEARCH_TORIHIKISAKI.LinkedSettingTextBox = null;
            this.SEARCH_TORIHIKISAKI.LinkedTextBoxs = null;
            this.SEARCH_TORIHIKISAKI.Location = new System.Drawing.Point(469, 71);
            this.SEARCH_TORIHIKISAKI.Name = "SEARCH_TORIHIKISAKI";
            this.SEARCH_TORIHIKISAKI.PopupAfterExecute = null;
            this.SEARCH_TORIHIKISAKI.PopupBeforeExecute = null;
            this.SEARCH_TORIHIKISAKI.PopupGetMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.SEARCH_TORIHIKISAKI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEARCH_TORIHIKISAKI.PopupSearchSendParams")));
            this.SEARCH_TORIHIKISAKI.PopupSetFormField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.SEARCH_TORIHIKISAKI.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.SEARCH_TORIHIKISAKI.PopupWindowName = "検索共通ポップアップ";
            this.SEARCH_TORIHIKISAKI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEARCH_TORIHIKISAKI.popupWindowSetting")));
            this.SEARCH_TORIHIKISAKI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEARCH_TORIHIKISAKI.RegistCheckMethod")));
            this.SEARCH_TORIHIKISAKI.SearchDisplayFlag = 0;
            this.SEARCH_TORIHIKISAKI.SetFormField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.SEARCH_TORIHIKISAKI.ShortItemName = "取引先CD";
            this.SEARCH_TORIHIKISAKI.Size = new System.Drawing.Size(22, 22);
            this.SEARCH_TORIHIKISAKI.TabIndex = 42;
            this.SEARCH_TORIHIKISAKI.TabStop = false;
            this.SEARCH_TORIHIKISAKI.UseVisualStyleBackColor = false;
            this.SEARCH_TORIHIKISAKI.ZeroPaddengFlag = false;
            // 
            // TORIHIKISAKI_CD
            // 
            this.TORIHIKISAKI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.TORIHIKISAKI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_CD.CharacterLimitList = null;
            this.TORIHIKISAKI_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.TORIHIKISAKI_CD.DBFieldsName = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_CD.DisplayItemName = "取引先CD";
            this.TORIHIKISAKI_CD.DisplayPopUp = null;
            this.TORIHIKISAKI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.FocusOutCheckMethod")));
            this.TORIHIKISAKI_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.TORIHIKISAKI_CD.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_CD.GetCodeMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TORIHIKISAKI_CD.IsInputErrorOccured = false;
            this.TORIHIKISAKI_CD.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_CD.Location = new System.Drawing.Point(115, 72);
            this.TORIHIKISAKI_CD.MaxLength = 6;
            this.TORIHIKISAKI_CD.Name = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.PopupAfterExecute = null;
            this.TORIHIKISAKI_CD.PopupBeforeExecute = null;
            this.TORIHIKISAKI_CD.PopupGetMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_CD.PopupSearchSendParams")));
            this.TORIHIKISAKI_CD.PopupSetFormField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TORIHIKISAKI_CD.PopupWindowName = "検索共通ポップアップ";
            this.TORIHIKISAKI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_CD.popupWindowSetting")));
            this.TORIHIKISAKI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.RegistCheckMethod")));
            this.TORIHIKISAKI_CD.SetFormField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.ShortItemName = "取引先CD";
            this.TORIHIKISAKI_CD.Size = new System.Drawing.Size(60, 20);
            this.TORIHIKISAKI_CD.TabIndex = 40;
            this.TORIHIKISAKI_CD.Tag = "取引先を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.TORIHIKISAKI_CD.ZeroPaddengFlag = true;
            // 
            // TORIHIKISAKI_NAME_RYAKU
            // 
            this.TORIHIKISAKI_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.TORIHIKISAKI_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.TORIHIKISAKI_NAME_RYAKU.DBFieldsName = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_NAME_RYAKU.DisplayItemName = "";
            this.TORIHIKISAKI_NAME_RYAKU.DisplayPopUp = null;
            this.TORIHIKISAKI_NAME_RYAKU.ErrorMessage = "";
            this.TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.TORIHIKISAKI_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_NAME_RYAKU.GetCodeMasterField = "";
            this.TORIHIKISAKI_NAME_RYAKU.IsInputErrorOccured = false;
            this.TORIHIKISAKI_NAME_RYAKU.ItemDefinedTypes = "";
            this.TORIHIKISAKI_NAME_RYAKU.Location = new System.Drawing.Point(174, 72);
            this.TORIHIKISAKI_NAME_RYAKU.MaxLength = 20;
            this.TORIHIKISAKI_NAME_RYAKU.Name = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_RYAKU.PopupAfterExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU.PopupBeforeExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU.PopupGetMasterField = "";
            this.TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams")));
            this.TORIHIKISAKI_NAME_RYAKU.PopupSetFormField = "";
            this.TORIHIKISAKI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.popupWindowSetting")));
            this.TORIHIKISAKI_NAME_RYAKU.ReadOnly = true;
            this.TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU.SetFormField = "";
            this.TORIHIKISAKI_NAME_RYAKU.Size = new System.Drawing.Size(291, 20);
            this.TORIHIKISAKI_NAME_RYAKU.TabIndex = 41;
            this.TORIHIKISAKI_NAME_RYAKU.TabStop = false;
            this.TORIHIKISAKI_NAME_RYAKU.Tag = "　";
            // 
            // TORIHIKISAKI_LABEL
            // 
            this.TORIHIKISAKI_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.TORIHIKISAKI_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TORIHIKISAKI_LABEL.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.TORIHIKISAKI_LABEL.ForeColor = System.Drawing.Color.White;
            this.TORIHIKISAKI_LABEL.Location = new System.Drawing.Point(3, 72);
            this.TORIHIKISAKI_LABEL.Name = "TORIHIKISAKI_LABEL";
            this.TORIHIKISAKI_LABEL.Size = new System.Drawing.Size(106, 20);
            this.TORIHIKISAKI_LABEL.TabIndex = 372;
            this.TORIHIKISAKI_LABEL.Text = "取引先";
            this.TORIHIKISAKI_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SEARCH_TORIKOMI
            // 
            this.SEARCH_TORIKOMI.DefaultBackColor = System.Drawing.Color.Empty;
            this.SEARCH_TORIKOMI.Location = new System.Drawing.Point(577, -1);
            this.SEARCH_TORIKOMI.Name = "SEARCH_TORIKOMI";
            this.SEARCH_TORIKOMI.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.SEARCH_TORIKOMI.Size = new System.Drawing.Size(50, 22);
            this.SEARCH_TORIKOMI.TabIndex = 5;
            this.SEARCH_TORIKOMI.Tag = "";
            this.SEARCH_TORIKOMI.Text = "参照";
            this.SEARCH_TORIKOMI.UseVisualStyleBackColor = true;
            this.SEARCH_TORIKOMI.Click += new System.EventHandler(this.SEARCH_TORIKOMI_Click);
            // 
            // TORIKOMI
            // 
            this.TORIKOMI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.TORIKOMI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIKOMI.CharactersNumber = new decimal(new int[] {
            65,
            0,
            0,
            0});
            this.TORIKOMI.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIKOMI.DisplayPopUp = null;
            this.TORIKOMI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIKOMI.FocusOutCheckMethod")));
            this.TORIKOMI.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.TORIKOMI.ForeColor = System.Drawing.Color.Black;
            this.TORIKOMI.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.TORIKOMI.IsInputErrorOccured = false;
            this.TORIKOMI.Location = new System.Drawing.Point(115, 0);
            this.TORIKOMI.MaxLength = 65;
            this.TORIKOMI.Name = "TORIKOMI";
            this.TORIKOMI.PopupAfterExecute = null;
            this.TORIKOMI.PopupBeforeExecute = null;
            this.TORIKOMI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIKOMI.PopupSearchSendParams")));
            this.TORIKOMI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIKOMI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIKOMI.popupWindowSetting")));
            this.TORIKOMI.ReadOnly = true;
            this.TORIKOMI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIKOMI.RegistCheckMethod")));
            this.TORIKOMI.Size = new System.Drawing.Size(460, 20);
            this.TORIKOMI.TabIndex = 1;
            this.TORIKOMI.TabStop = false;
            this.TORIKOMI.Tag = " ";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 20);
            this.label2.TabIndex = 374;
            this.label2.Text = "取込先※";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SAKUSEI_BI_FROM
            // 
            this.SAKUSEI_BI_FROM.BackColor = System.Drawing.SystemColors.Window;
            this.SAKUSEI_BI_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SAKUSEI_BI_FROM.CalendarFont = new System.Drawing.Font("MS Gothic", 9F);
            this.SAKUSEI_BI_FROM.Checked = false;
            this.SAKUSEI_BI_FROM.CustomFormat = "yyyy/MM/dd(ddd)";
            this.SAKUSEI_BI_FROM.DateTimeNowYear = "";
            this.SAKUSEI_BI_FROM.DBFieldsName = "";
            this.SAKUSEI_BI_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.SAKUSEI_BI_FROM.DisplayItemName = "預入日(From)";
            this.SAKUSEI_BI_FROM.DisplayPopUp = null;
            this.SAKUSEI_BI_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SAKUSEI_BI_FROM.FocusOutCheckMethod")));
            this.SAKUSEI_BI_FROM.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.SAKUSEI_BI_FROM.ForeColor = System.Drawing.Color.Black;
            this.SAKUSEI_BI_FROM.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.SAKUSEI_BI_FROM.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SAKUSEI_BI_FROM.IsInputErrorOccured = false;
            this.SAKUSEI_BI_FROM.ItemDefinedTypes = "";
            this.SAKUSEI_BI_FROM.Location = new System.Drawing.Point(115, 48);
            this.SAKUSEI_BI_FROM.MaxLength = 10;
            this.SAKUSEI_BI_FROM.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.SAKUSEI_BI_FROM.Name = "SAKUSEI_BI_FROM";
            this.SAKUSEI_BI_FROM.NullValue = "";
            this.SAKUSEI_BI_FROM.PopupAfterExecute = null;
            this.SAKUSEI_BI_FROM.PopupBeforeExecute = null;
            this.SAKUSEI_BI_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SAKUSEI_BI_FROM.PopupSearchSendParams")));
            this.SAKUSEI_BI_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SAKUSEI_BI_FROM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SAKUSEI_BI_FROM.popupWindowSetting")));
            this.SAKUSEI_BI_FROM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SAKUSEI_BI_FROM.RegistCheckMethod")));
            this.SAKUSEI_BI_FROM.ShortItemName = "預入日(From)";
            this.SAKUSEI_BI_FROM.Size = new System.Drawing.Size(138, 20);
            this.SAKUSEI_BI_FROM.TabIndex = 20;
            this.SAKUSEI_BI_FROM.Tag = "日付を選択してください";
            this.SAKUSEI_BI_FROM.Text = "2013/10/29(火)";
            this.SAKUSEI_BI_FROM.Value = new System.DateTime(2013, 10, 29, 0, 0, 0, 0);
            // 
            // SAKUSEI_BI_TO
            // 
            this.SAKUSEI_BI_TO.BackColor = System.Drawing.SystemColors.Window;
            this.SAKUSEI_BI_TO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SAKUSEI_BI_TO.CalendarFont = new System.Drawing.Font("MS Gothic", 9F);
            this.SAKUSEI_BI_TO.Checked = false;
            this.SAKUSEI_BI_TO.CustomFormat = "yyyy/MM/dd(ddd)";
            this.SAKUSEI_BI_TO.DateTimeNowYear = "";
            this.SAKUSEI_BI_TO.DBFieldsName = "";
            this.SAKUSEI_BI_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.SAKUSEI_BI_TO.DisplayItemName = "預入日(To)";
            this.SAKUSEI_BI_TO.DisplayPopUp = null;
            this.SAKUSEI_BI_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SAKUSEI_BI_TO.FocusOutCheckMethod")));
            this.SAKUSEI_BI_TO.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.SAKUSEI_BI_TO.ForeColor = System.Drawing.Color.Black;
            this.SAKUSEI_BI_TO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.SAKUSEI_BI_TO.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SAKUSEI_BI_TO.IsInputErrorOccured = false;
            this.SAKUSEI_BI_TO.ItemDefinedTypes = "";
            this.SAKUSEI_BI_TO.Location = new System.Drawing.Point(275, 48);
            this.SAKUSEI_BI_TO.MaxLength = 10;
            this.SAKUSEI_BI_TO.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.SAKUSEI_BI_TO.Name = "SAKUSEI_BI_TO";
            this.SAKUSEI_BI_TO.NullValue = "";
            this.SAKUSEI_BI_TO.PopupAfterExecute = null;
            this.SAKUSEI_BI_TO.PopupBeforeExecute = null;
            this.SAKUSEI_BI_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SAKUSEI_BI_TO.PopupSearchSendParams")));
            this.SAKUSEI_BI_TO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SAKUSEI_BI_TO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SAKUSEI_BI_TO.popupWindowSetting")));
            this.SAKUSEI_BI_TO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SAKUSEI_BI_TO.RegistCheckMethod")));
            this.SAKUSEI_BI_TO.ShortItemName = "預入日(To)";
            this.SAKUSEI_BI_TO.Size = new System.Drawing.Size(138, 20);
            this.SAKUSEI_BI_TO.TabIndex = 30;
            this.SAKUSEI_BI_TO.Tag = "日付を選択してください";
            this.SAKUSEI_BI_TO.Text = "2013/10/29(火)";
            this.SAKUSEI_BI_TO.Value = new System.DateTime(2013, 10, 29, 0, 0, 0, 0);
            this.SAKUSEI_BI_TO.DoubleClick += new System.EventHandler(this.SAKUSEI_BI_TO_DoubleClick);
            // 
            // SAKUSEI_BI_LABEL
            // 
            this.SAKUSEI_BI_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.SAKUSEI_BI_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SAKUSEI_BI_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SAKUSEI_BI_LABEL.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SAKUSEI_BI_LABEL.ForeColor = System.Drawing.Color.White;
            this.SAKUSEI_BI_LABEL.Location = new System.Drawing.Point(3, 48);
            this.SAKUSEI_BI_LABEL.Name = "SAKUSEI_BI_LABEL";
            this.SAKUSEI_BI_LABEL.Size = new System.Drawing.Size(106, 20);
            this.SAKUSEI_BI_LABEL.TabIndex = 377;
            this.SAKUSEI_BI_LABEL.Text = "預入日";
            this.SAKUSEI_BI_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ALERT_DATE_FROM_TO_LABEL
            // 
            this.ALERT_DATE_FROM_TO_LABEL.BackColor = System.Drawing.Color.Transparent;
            this.ALERT_DATE_FROM_TO_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ALERT_DATE_FROM_TO_LABEL.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ALERT_DATE_FROM_TO_LABEL.ForeColor = System.Drawing.Color.Black;
            this.ALERT_DATE_FROM_TO_LABEL.Location = new System.Drawing.Point(256, 48);
            this.ALERT_DATE_FROM_TO_LABEL.Name = "ALERT_DATE_FROM_TO_LABEL";
            this.ALERT_DATE_FROM_TO_LABEL.Size = new System.Drawing.Size(20, 20);
            this.ALERT_DATE_FROM_TO_LABEL.TabIndex = 379;
            this.ALERT_DATE_FROM_TO_LABEL.Text = "～";
            this.ALERT_DATE_FROM_TO_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel7
            // 
            this.customPanel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel7.Controls.Add(this.SAKUSEI_KBN_3);
            this.customPanel7.Controls.Add(this.SAKUSEI_KBN_2);
            this.customPanel7.Controls.Add(this.SAKUSEI_KBN);
            this.customPanel7.Controls.Add(this.SAKUSEI_KBN_1);
            this.customPanel7.Location = new System.Drawing.Point(115, 24);
            this.customPanel7.Name = "customPanel7";
            this.customPanel7.Size = new System.Drawing.Size(298, 20);
            this.customPanel7.TabIndex = 10;
            // 
            // SAKUSEI_KBN_3
            // 
            this.SAKUSEI_KBN_3.AutoSize = true;
            this.SAKUSEI_KBN_3.DefaultBackColor = System.Drawing.Color.Empty;
            this.SAKUSEI_KBN_3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SAKUSEI_KBN_3.FocusOutCheckMethod")));
            this.SAKUSEI_KBN_3.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.SAKUSEI_KBN_3.LinkedTextBox = "SAKUSEI_KBN";
            this.SAKUSEI_KBN_3.Location = new System.Drawing.Point(207, 0);
            this.SAKUSEI_KBN_3.Name = "SAKUSEI_KBN_3";
            this.SAKUSEI_KBN_3.PopupAfterExecute = null;
            this.SAKUSEI_KBN_3.PopupBeforeExecute = null;
            this.SAKUSEI_KBN_3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SAKUSEI_KBN_3.PopupSearchSendParams")));
            this.SAKUSEI_KBN_3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SAKUSEI_KBN_3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SAKUSEI_KBN_3.popupWindowSetting")));
            this.SAKUSEI_KBN_3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SAKUSEI_KBN_3.RegistCheckMethod")));
            this.SAKUSEI_KBN_3.Size = new System.Drawing.Size(74, 17);
            this.SAKUSEI_KBN_3.TabIndex = 13;
            this.SAKUSEI_KBN_3.Tag = "伝票作成が「3．全て」の場合にはチェックを付けてください";
            this.SAKUSEI_KBN_3.Text = "3．全て";
            this.SAKUSEI_KBN_3.UseVisualStyleBackColor = true;
            this.SAKUSEI_KBN_3.Value = "3";
            // 
            // SAKUSEI_KBN_2
            // 
            this.SAKUSEI_KBN_2.AutoSize = true;
            this.SAKUSEI_KBN_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.SAKUSEI_KBN_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SAKUSEI_KBN_2.FocusOutCheckMethod")));
            this.SAKUSEI_KBN_2.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.SAKUSEI_KBN_2.LinkedTextBox = "SAKUSEI_KBN";
            this.SAKUSEI_KBN_2.Location = new System.Drawing.Point(117, 0);
            this.SAKUSEI_KBN_2.Name = "SAKUSEI_KBN_2";
            this.SAKUSEI_KBN_2.PopupAfterExecute = null;
            this.SAKUSEI_KBN_2.PopupBeforeExecute = null;
            this.SAKUSEI_KBN_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SAKUSEI_KBN_2.PopupSearchSendParams")));
            this.SAKUSEI_KBN_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SAKUSEI_KBN_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SAKUSEI_KBN_2.popupWindowSetting")));
            this.SAKUSEI_KBN_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SAKUSEI_KBN_2.RegistCheckMethod")));
            this.SAKUSEI_KBN_2.Size = new System.Drawing.Size(74, 17);
            this.SAKUSEI_KBN_2.TabIndex = 12;
            this.SAKUSEI_KBN_2.Tag = "伝票作成が「2．不可」の場合にはチェックを付けてください";
            this.SAKUSEI_KBN_2.Text = "2．不可";
            this.SAKUSEI_KBN_2.UseVisualStyleBackColor = true;
            this.SAKUSEI_KBN_2.Value = "2";
            // 
            // SAKUSEI_KBN
            // 
            this.SAKUSEI_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.SAKUSEI_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SAKUSEI_KBN.CharacterLimitList = new char[] {
        '1',
        '2',
        '3'};
            this.SAKUSEI_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.SAKUSEI_KBN.DisplayItemName = "伝票作成";
            this.SAKUSEI_KBN.DisplayPopUp = null;
            this.SAKUSEI_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SAKUSEI_KBN.FocusOutCheckMethod")));
            this.SAKUSEI_KBN.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.SAKUSEI_KBN.ForeColor = System.Drawing.Color.Black;
            this.SAKUSEI_KBN.IsInputErrorOccured = false;
            this.SAKUSEI_KBN.LinkedRadioButtonArray = new string[] {
        "SAKUSEI_KBN_1",
        "SAKUSEI_KBN_2",
        "SAKUSEI_KBN_3"};
            this.SAKUSEI_KBN.Location = new System.Drawing.Point(-1, -1);
            this.SAKUSEI_KBN.Name = "SAKUSEI_KBN";
            this.SAKUSEI_KBN.PopupAfterExecute = null;
            this.SAKUSEI_KBN.PopupBeforeExecute = null;
            this.SAKUSEI_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SAKUSEI_KBN.PopupSearchSendParams")));
            this.SAKUSEI_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SAKUSEI_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SAKUSEI_KBN.popupWindowSetting")));
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
            this.SAKUSEI_KBN.RangeSetting = rangeSettingDto1;
            this.SAKUSEI_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SAKUSEI_KBN.RegistCheckMethod")));
            this.SAKUSEI_KBN.ShortItemName = "伝票作成";
            this.SAKUSEI_KBN.Size = new System.Drawing.Size(20, 20);
            this.SAKUSEI_KBN.TabIndex = 10;
            this.SAKUSEI_KBN.Tag = "【1～3】のいずれかで入力してください";
            this.SAKUSEI_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.SAKUSEI_KBN.WordWrap = false;
            // 
            // SAKUSEI_KBN_1
            // 
            this.SAKUSEI_KBN_1.AutoSize = true;
            this.SAKUSEI_KBN_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.SAKUSEI_KBN_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SAKUSEI_KBN_1.FocusOutCheckMethod")));
            this.SAKUSEI_KBN_1.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.SAKUSEI_KBN_1.LinkedTextBox = "SAKUSEI_KBN";
            this.SAKUSEI_KBN_1.Location = new System.Drawing.Point(25, 0);
            this.SAKUSEI_KBN_1.Name = "SAKUSEI_KBN_1";
            this.SAKUSEI_KBN_1.PopupAfterExecute = null;
            this.SAKUSEI_KBN_1.PopupBeforeExecute = null;
            this.SAKUSEI_KBN_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SAKUSEI_KBN_1.PopupSearchSendParams")));
            this.SAKUSEI_KBN_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SAKUSEI_KBN_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SAKUSEI_KBN_1.popupWindowSetting")));
            this.SAKUSEI_KBN_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SAKUSEI_KBN_1.RegistCheckMethod")));
            this.SAKUSEI_KBN_1.Size = new System.Drawing.Size(60, 17);
            this.SAKUSEI_KBN_1.TabIndex = 12;
            this.SAKUSEI_KBN_1.Tag = "伝票作成が「1．可」の場合にはチェックを付けてください";
            this.SAKUSEI_KBN_1.Text = "1．可";
            this.SAKUSEI_KBN_1.UseVisualStyleBackColor = true;
            this.SAKUSEI_KBN_1.Value = "1";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(3, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 20);
            this.label9.TabIndex = 381;
            this.label9.Text = "伝票作成※";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BANK_NAME_RYAKU
            // 
            this.BANK_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.BANK_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BANK_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.BANK_NAME_RYAKU.DisplayPopUp = null;
            this.BANK_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_NAME_RYAKU.FocusOutCheckMethod")));
            this.BANK_NAME_RYAKU.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.BANK_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.BANK_NAME_RYAKU.IsInputErrorOccured = false;
            this.BANK_NAME_RYAKU.Location = new System.Drawing.Point(174, 96);
            this.BANK_NAME_RYAKU.Name = "BANK_NAME_RYAKU";
            this.BANK_NAME_RYAKU.PopupAfterExecute = null;
            this.BANK_NAME_RYAKU.PopupBeforeExecute = null;
            this.BANK_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BANK_NAME_RYAKU.PopupSearchSendParams")));
            this.BANK_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BANK_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BANK_NAME_RYAKU.popupWindowSetting")));
            this.BANK_NAME_RYAKU.ReadOnly = true;
            this.BANK_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_NAME_RYAKU.RegistCheckMethod")));
            this.BANK_NAME_RYAKU.Size = new System.Drawing.Size(160, 20);
            this.BANK_NAME_RYAKU.TabIndex = 51;
            this.BANK_NAME_RYAKU.TabStop = false;
            this.BANK_NAME_RYAKU.Tag = "";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 20);
            this.label3.TabIndex = 383;
            this.label3.Text = "銀行";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BANK_CD
            // 
            this.BANK_CD.AlphabetLimitFlag = false;
            this.BANK_CD.BackColor = System.Drawing.SystemColors.Window;
            this.BANK_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BANK_CD.CharacterLimitList = null;
            this.BANK_CD.CharactersNumber = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.BANK_CD.DBFieldsName = "BANK_CD";
            this.BANK_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.BANK_CD.DisplayItemName = "";
            this.BANK_CD.DisplayPopUp = null;
            this.BANK_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_CD.FocusOutCheckMethod")));
            this.BANK_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.BANK_CD.ForeColor = System.Drawing.Color.Black;
            this.BANK_CD.GetCodeMasterField = "BANK_CD,BANK_NAME_RYAKU";
            this.BANK_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.BANK_CD.IsInputErrorOccured = false;
            this.BANK_CD.ItemDefinedTypes = "";
            this.BANK_CD.Location = new System.Drawing.Point(115, 96);
            this.BANK_CD.MaxLength = 4;
            this.BANK_CD.Name = "BANK_CD";
            this.BANK_CD.PopupAfterExecute = null;
            this.BANK_CD.PopupBeforeExecute = null;
            this.BANK_CD.PopupGetMasterField = "BANK_CD,BANK_NAME_RYAKU";
            this.BANK_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BANK_CD.PopupSearchSendParams")));
            this.BANK_CD.PopupSetFormField = "BANK_CD,BANK_NAME_RYAKU";
            this.BANK_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_BANK;
            this.BANK_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.BANK_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BANK_CD.popupWindowSetting")));
            this.BANK_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_CD.RegistCheckMethod")));
            this.BANK_CD.SetFormField = "BANK_CD,BANK_NAME_RYAKU";
            this.BANK_CD.Size = new System.Drawing.Size(60, 20);
            this.BANK_CD.TabIndex = 50;
            this.BANK_CD.Tag = "銀行を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.BANK_CD.ZeroPaddengFlag = true;
            this.BANK_CD.TextChanged += new System.EventHandler(this.BANK_CD_TextChanged);
            // 
            // BANK_SHITEN_NAME_RYAKU
            // 
            this.BANK_SHITEN_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.BANK_SHITEN_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BANK_SHITEN_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.BANK_SHITEN_NAME_RYAKU.DisplayPopUp = null;
            this.BANK_SHITEN_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_SHITEN_NAME_RYAKU.FocusOutCheckMethod")));
            this.BANK_SHITEN_NAME_RYAKU.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.BANK_SHITEN_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.BANK_SHITEN_NAME_RYAKU.IsInputErrorOccured = false;
            this.BANK_SHITEN_NAME_RYAKU.Location = new System.Drawing.Point(174, 120);
            this.BANK_SHITEN_NAME_RYAKU.Name = "BANK_SHITEN_NAME_RYAKU";
            this.BANK_SHITEN_NAME_RYAKU.PopupAfterExecute = null;
            this.BANK_SHITEN_NAME_RYAKU.PopupBeforeExecute = null;
            this.BANK_SHITEN_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BANK_SHITEN_NAME_RYAKU.PopupSearchSendParams")));
            this.BANK_SHITEN_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BANK_SHITEN_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BANK_SHITEN_NAME_RYAKU.popupWindowSetting")));
            this.BANK_SHITEN_NAME_RYAKU.ReadOnly = true;
            this.BANK_SHITEN_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_SHITEN_NAME_RYAKU.RegistCheckMethod")));
            this.BANK_SHITEN_NAME_RYAKU.Size = new System.Drawing.Size(160, 20);
            this.BANK_SHITEN_NAME_RYAKU.TabIndex = 61;
            this.BANK_SHITEN_NAME_RYAKU.TabStop = false;
            this.BANK_SHITEN_NAME_RYAKU.Tag = "";
            // 
            // BANK_SHITEN_CD
            // 
            this.BANK_SHITEN_CD.AlphabetLimitFlag = false;
            this.BANK_SHITEN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.BANK_SHITEN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BANK_SHITEN_CD.CharacterLimitList = null;
            this.BANK_SHITEN_CD.CharactersNumber = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.BANK_SHITEN_CD.DBFieldsName = "";
            this.BANK_SHITEN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.BANK_SHITEN_CD.DisplayItemName = "";
            this.BANK_SHITEN_CD.DisplayPopUp = null;
            this.BANK_SHITEN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_SHITEN_CD.FocusOutCheckMethod")));
            this.BANK_SHITEN_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.BANK_SHITEN_CD.ForeColor = System.Drawing.Color.Black;
            this.BANK_SHITEN_CD.GetCodeMasterField = "BANK_CD,BANK_NAME_RYAKU,BANK_SHITEN_CD,BANK_SHIETN_NAME_RYAKU,KOUZA_SHURUI,KOUZA_" +
    "NO";
            this.BANK_SHITEN_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.BANK_SHITEN_CD.IsInputErrorOccured = false;
            this.BANK_SHITEN_CD.ItemDefinedTypes = "";
            this.BANK_SHITEN_CD.Location = new System.Drawing.Point(116, 120);
            this.BANK_SHITEN_CD.MaxLength = 3;
            this.BANK_SHITEN_CD.Name = "BANK_SHITEN_CD";
            this.BANK_SHITEN_CD.PopupAfterExecute = null;
            this.BANK_SHITEN_CD.PopupAfterExecuteMethod = "BANK_SHITEN_CD_PopupAfter";
            this.BANK_SHITEN_CD.PopupBeforeExecute = null;
            this.BANK_SHITEN_CD.PopupGetMasterField = "BANK_CD,BANK_NAME_RYAKU,BANK_SHITEN_CD,BANK_SHIETN_NAME_RYAKU,KOUZA_SHURUI,KOUZA_" +
    "NO";
            this.BANK_SHITEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BANK_SHITEN_CD.PopupSearchSendParams")));
            this.BANK_SHITEN_CD.PopupSetFormField = "BANK_CD,BANK_NAME_RYAKU,BANK_SHITEN_CD,BANK_SHITEN_NAME_RYAKU,KOUZA_SHURUI,KOUZA_" +
    "NO";
            this.BANK_SHITEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_BANK_SHITEN;
            this.BANK_SHITEN_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.BANK_SHITEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BANK_SHITEN_CD.popupWindowSetting")));
            this.BANK_SHITEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BANK_SHITEN_CD.RegistCheckMethod")));
            this.BANK_SHITEN_CD.SetFormField = "BANK_CD,BANK_NAME_RYAKU,BANK_SHITEN_CD,BANK_SHITEN_NAME_RYAKU,KOUZA_SHURUI,KOUZA_" +
    "NO";
            this.BANK_SHITEN_CD.Size = new System.Drawing.Size(59, 20);
            this.BANK_SHITEN_CD.TabIndex = 60;
            this.BANK_SHITEN_CD.Tag = "銀行支店を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.BANK_SHITEN_CD.ZeroPaddengFlag = true;
            this.BANK_SHITEN_CD.TextChanged += new System.EventHandler(this.BANK_SHITEN_CD_TextChanged);
            this.BANK_SHITEN_CD.Validating += new System.ComponentModel.CancelEventHandler(this.BANK_SHITEN_CD_Validating);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 20);
            this.label4.TabIndex = 386;
            this.label4.Text = "銀行支店";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KOUZA_NO
            // 
            this.KOUZA_NO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KOUZA_NO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KOUZA_NO.DBFieldsName = "";
            this.KOUZA_NO.DefaultBackColor = System.Drawing.Color.Empty;
            this.KOUZA_NO.DisplayItemName = "";
            this.KOUZA_NO.DisplayPopUp = null;
            this.KOUZA_NO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_NO.FocusOutCheckMethod")));
            this.KOUZA_NO.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.KOUZA_NO.ForeColor = System.Drawing.Color.Black;
            this.KOUZA_NO.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.KOUZA_NO.IsInputErrorOccured = false;
            this.KOUZA_NO.ItemDefinedTypes = "";
            this.KOUZA_NO.Location = new System.Drawing.Point(297, 144);
            this.KOUZA_NO.Name = "KOUZA_NO";
            this.KOUZA_NO.PopupAfterExecute = null;
            this.KOUZA_NO.PopupBeforeExecute = null;
            this.KOUZA_NO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KOUZA_NO.PopupSearchSendParams")));
            this.KOUZA_NO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KOUZA_NO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KOUZA_NO.popupWindowSetting")));
            this.KOUZA_NO.ReadOnly = true;
            this.KOUZA_NO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_NO.RegistCheckMethod")));
            this.KOUZA_NO.Size = new System.Drawing.Size(64, 20);
            this.KOUZA_NO.TabIndex = 80;
            this.KOUZA_NO.TabStop = false;
            this.KOUZA_NO.Tag = " ";
            // 
            // KOUZA_SHURUI
            // 
            this.KOUZA_SHURUI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KOUZA_SHURUI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KOUZA_SHURUI.DBFieldsName = "KOUZA_SHURUI";
            this.KOUZA_SHURUI.DefaultBackColor = System.Drawing.Color.Empty;
            this.KOUZA_SHURUI.DisplayItemName = "口座種類";
            this.KOUZA_SHURUI.DisplayPopUp = null;
            this.KOUZA_SHURUI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_SHURUI.FocusOutCheckMethod")));
            this.KOUZA_SHURUI.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.KOUZA_SHURUI.ForeColor = System.Drawing.Color.Black;
            this.KOUZA_SHURUI.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.KOUZA_SHURUI.IsInputErrorOccured = false;
            this.KOUZA_SHURUI.ItemDefinedTypes = "VARCHAR";
            this.KOUZA_SHURUI.Location = new System.Drawing.Point(116, 144);
            this.KOUZA_SHURUI.Name = "KOUZA_SHURUI";
            this.KOUZA_SHURUI.PopupAfterExecute = null;
            this.KOUZA_SHURUI.PopupBeforeExecute = null;
            this.KOUZA_SHURUI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KOUZA_SHURUI.PopupSearchSendParams")));
            this.KOUZA_SHURUI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KOUZA_SHURUI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KOUZA_SHURUI.popupWindowSetting")));
            this.KOUZA_SHURUI.ReadOnly = true;
            this.KOUZA_SHURUI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KOUZA_SHURUI.RegistCheckMethod")));
            this.KOUZA_SHURUI.Size = new System.Drawing.Size(63, 20);
            this.KOUZA_SHURUI.TabIndex = 70;
            this.KOUZA_SHURUI.TabStop = false;
            this.KOUZA_SHURUI.Tag = " ";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label6.Font = new System.Drawing.Font("MS Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(3, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 19);
            this.label6.TabIndex = 389;
            this.label6.Text = "口座種類";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SEARCH_BANK
            // 
            this.SEARCH_BANK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.SEARCH_BANK.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SEARCH_BANK.DBFieldsName = null;
            this.SEARCH_BANK.DefaultBackColor = System.Drawing.Color.Empty;
            this.SEARCH_BANK.DisplayItemName = "取引先CD";
            this.SEARCH_BANK.DisplayPopUp = null;
            this.SEARCH_BANK.ErrorMessage = null;
            this.SEARCH_BANK.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEARCH_BANK.FocusOutCheckMethod")));
            this.SEARCH_BANK.Font = new System.Drawing.Font("MS Gothic", 11.25F);
            this.SEARCH_BANK.GetCodeMasterField = null;
            this.SEARCH_BANK.Image = ((System.Drawing.Image)(resources.GetObject("SEARCH_BANK.Image")));
            this.SEARCH_BANK.ItemDefinedTypes = null;
            this.SEARCH_BANK.LinkedSettingTextBox = null;
            this.SEARCH_BANK.LinkedTextBoxs = null;
            this.SEARCH_BANK.Location = new System.Drawing.Point(339, 94);
            this.SEARCH_BANK.Name = "SEARCH_BANK";
            this.SEARCH_BANK.PopupAfterExecute = null;
            this.SEARCH_BANK.PopupBeforeExecute = null;
            this.SEARCH_BANK.PopupGetMasterField = "BANK_CD,BANK_NAME_RYAKU";
            this.SEARCH_BANK.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEARCH_BANK.PopupSearchSendParams")));
            this.SEARCH_BANK.PopupSetFormField = "BANK_CD,BANK_NAME_RYAKU";
            this.SEARCH_BANK.PopupWindowId = r_framework.Const.WINDOW_ID.M_BANK;
            this.SEARCH_BANK.PopupWindowName = "マスタ共通ポップアップ";
            this.SEARCH_BANK.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEARCH_BANK.popupWindowSetting")));
            this.SEARCH_BANK.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEARCH_BANK.RegistCheckMethod")));
            this.SEARCH_BANK.SearchDisplayFlag = 0;
            this.SEARCH_BANK.SetFormField = "BANK_CD,BANK_NAME_RYAKU";
            this.SEARCH_BANK.ShortItemName = "取引先CD";
            this.SEARCH_BANK.Size = new System.Drawing.Size(22, 22);
            this.SEARCH_BANK.TabIndex = 52;
            this.SEARCH_BANK.TabStop = false;
            this.SEARCH_BANK.UseVisualStyleBackColor = false;
            this.SEARCH_BANK.ZeroPaddengFlag = false;
            // 
            // SEARCH_BANK_SHITEN
            // 
            this.SEARCH_BANK_SHITEN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.SEARCH_BANK_SHITEN.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SEARCH_BANK_SHITEN.DBFieldsName = null;
            this.SEARCH_BANK_SHITEN.DefaultBackColor = System.Drawing.Color.Empty;
            this.SEARCH_BANK_SHITEN.DisplayItemName = "";
            this.SEARCH_BANK_SHITEN.DisplayPopUp = null;
            this.SEARCH_BANK_SHITEN.ErrorMessage = null;
            this.SEARCH_BANK_SHITEN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEARCH_BANK_SHITEN.FocusOutCheckMethod")));
            this.SEARCH_BANK_SHITEN.Font = new System.Drawing.Font("MS Gothic", 11.25F);
            this.SEARCH_BANK_SHITEN.GetCodeMasterField = null;
            this.SEARCH_BANK_SHITEN.Image = ((System.Drawing.Image)(resources.GetObject("SEARCH_BANK_SHITEN.Image")));
            this.SEARCH_BANK_SHITEN.ItemDefinedTypes = null;
            this.SEARCH_BANK_SHITEN.LinkedSettingTextBox = null;
            this.SEARCH_BANK_SHITEN.LinkedTextBoxs = null;
            this.SEARCH_BANK_SHITEN.Location = new System.Drawing.Point(339, 118);
            this.SEARCH_BANK_SHITEN.Name = "SEARCH_BANK_SHITEN";
            this.SEARCH_BANK_SHITEN.PopupAfterExecute = null;
            this.SEARCH_BANK_SHITEN.PopupBeforeExecute = null;
            this.SEARCH_BANK_SHITEN.PopupGetMasterField = "BANK_CD,BANK_NAME_RYAKU,BANK_SHITEN_CD,BANK_SHIETN_NAME_RYAKU,KOUZA_SHURUI,KOUZA_" +
    "NO";
            this.SEARCH_BANK_SHITEN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEARCH_BANK_SHITEN.PopupSearchSendParams")));
            this.SEARCH_BANK_SHITEN.PopupSetFormField = "BANK_CD,BANK_NAME_RYAKU,BANK_SHITEN_CD,BANK_SHITEN_NAME_RYAKU,KOUZA_SHURUI,KOUZA_" +
    "NO";
            this.SEARCH_BANK_SHITEN.PopupWindowId = r_framework.Const.WINDOW_ID.M_BANK_SHITEN;
            this.SEARCH_BANK_SHITEN.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.SEARCH_BANK_SHITEN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEARCH_BANK_SHITEN.popupWindowSetting")));
            this.SEARCH_BANK_SHITEN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEARCH_BANK_SHITEN.RegistCheckMethod")));
            this.SEARCH_BANK_SHITEN.SearchDisplayFlag = 0;
            this.SEARCH_BANK_SHITEN.SetFormField = "BANK_CD,BANK_NAME_RYAKU,BANK_SHITEN_CD,BANK_SHITEN_NAME_RYAKU,KOUZA_SHURUI,KOUZA_" +
    "NO";
            this.SEARCH_BANK_SHITEN.ShortItemName = "";
            this.SEARCH_BANK_SHITEN.Size = new System.Drawing.Size(22, 22);
            this.SEARCH_BANK_SHITEN.TabIndex = 62;
            this.SEARCH_BANK_SHITEN.TabStop = false;
            this.SEARCH_BANK_SHITEN.UseVisualStyleBackColor = false;
            this.SEARCH_BANK_SHITEN.ZeroPaddengFlag = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.Font = new System.Drawing.Font("MS Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(185, 144);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 19);
            this.label1.TabIndex = 395;
            this.label1.Text = "口座番号";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SEL_FURIKOMI_JINMEI_1
            // 
            this.SEL_FURIKOMI_JINMEI_1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SEL_FURIKOMI_JINMEI_1.BackColor = System.Drawing.SystemColors.Window;
            this.SEL_FURIKOMI_JINMEI_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SEL_FURIKOMI_JINMEI_1.ChangeUpperCase = true;
            this.SEL_FURIKOMI_JINMEI_1.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.SEL_FURIKOMI_JINMEI_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.SEL_FURIKOMI_JINMEI_1.DisplayPopUp = null;
            this.SEL_FURIKOMI_JINMEI_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEL_FURIKOMI_JINMEI_1.FocusOutCheckMethod")));
            this.SEL_FURIKOMI_JINMEI_1.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.SEL_FURIKOMI_JINMEI_1.ForeColor = System.Drawing.Color.Black;
            this.SEL_FURIKOMI_JINMEI_1.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.SEL_FURIKOMI_JINMEI_1.IsInputErrorOccured = false;
            this.SEL_FURIKOMI_JINMEI_1.Location = new System.Drawing.Point(605, 390);
            this.SEL_FURIKOMI_JINMEI_1.MaxLength = 40;
            this.SEL_FURIKOMI_JINMEI_1.Name = "SEL_FURIKOMI_JINMEI_1";
            this.SEL_FURIKOMI_JINMEI_1.PopupAfterExecute = null;
            this.SEL_FURIKOMI_JINMEI_1.PopupBeforeExecute = null;
            this.SEL_FURIKOMI_JINMEI_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEL_FURIKOMI_JINMEI_1.PopupSearchSendParams")));
            this.SEL_FURIKOMI_JINMEI_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SEL_FURIKOMI_JINMEI_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEL_FURIKOMI_JINMEI_1.popupWindowSetting")));
            this.SEL_FURIKOMI_JINMEI_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEL_FURIKOMI_JINMEI_1.RegistCheckMethod")));
            this.SEL_FURIKOMI_JINMEI_1.Size = new System.Drawing.Size(349, 20);
            this.SEL_FURIKOMI_JINMEI_1.TabIndex = 75;
            this.SEL_FURIKOMI_JINMEI_1.Tag = " ";
            // 
            // SEL_LB_FURIKOMI_JINMEI_1
            // 
            this.SEL_LB_FURIKOMI_JINMEI_1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SEL_LB_FURIKOMI_JINMEI_1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.SEL_LB_FURIKOMI_JINMEI_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SEL_LB_FURIKOMI_JINMEI_1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SEL_LB_FURIKOMI_JINMEI_1.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.SEL_LB_FURIKOMI_JINMEI_1.ForeColor = System.Drawing.Color.White;
            this.SEL_LB_FURIKOMI_JINMEI_1.Location = new System.Drawing.Point(493, 390);
            this.SEL_LB_FURIKOMI_JINMEI_1.Name = "SEL_LB_FURIKOMI_JINMEI_1";
            this.SEL_LB_FURIKOMI_JINMEI_1.Size = new System.Drawing.Size(106, 20);
            this.SEL_LB_FURIKOMI_JINMEI_1.TabIndex = 476;
            this.SEL_LB_FURIKOMI_JINMEI_1.Text = "振込人名１";
            this.SEL_LB_FURIKOMI_JINMEI_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SEL_SETTEI_BTN_1
            // 
            this.SEL_SETTEI_BTN_1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SEL_SETTEI_BTN_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.SEL_SETTEI_BTN_1.Location = new System.Drawing.Point(956, 389);
            this.SEL_SETTEI_BTN_1.Name = "SEL_SETTEI_BTN_1";
            this.SEL_SETTEI_BTN_1.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.SEL_SETTEI_BTN_1.Size = new System.Drawing.Size(50, 22);
            this.SEL_SETTEI_BTN_1.TabIndex = 477;
            this.SEL_SETTEI_BTN_1.TabStop = false;
            this.SEL_SETTEI_BTN_1.Tag = " ";
            this.SEL_SETTEI_BTN_1.Text = "設定";
            this.SEL_SETTEI_BTN_1.UseVisualStyleBackColor = true;
            this.SEL_SETTEI_BTN_1.Click += new System.EventHandler(this.SEL_SETTEI_BTN_1_Click);
            // 
            // SEL_SENTAKU_SAKI
            // 
            this.SEL_SENTAKU_SAKI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SEL_SENTAKU_SAKI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.SEL_SENTAKU_SAKI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SEL_SENTAKU_SAKI.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.SEL_SENTAKU_SAKI.DBFieldsName = "";
            this.SEL_SENTAKU_SAKI.DefaultBackColor = System.Drawing.Color.Empty;
            this.SEL_SENTAKU_SAKI.DisplayItemName = "";
            this.SEL_SENTAKU_SAKI.DisplayPopUp = null;
            this.SEL_SENTAKU_SAKI.ErrorMessage = "";
            this.SEL_SENTAKU_SAKI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEL_SENTAKU_SAKI.FocusOutCheckMethod")));
            this.SEL_SENTAKU_SAKI.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.SEL_SENTAKU_SAKI.ForeColor = System.Drawing.Color.Black;
            this.SEL_SENTAKU_SAKI.GetCodeMasterField = "";
            this.SEL_SENTAKU_SAKI.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.SEL_SENTAKU_SAKI.IsInputErrorOccured = false;
            this.SEL_SENTAKU_SAKI.ItemDefinedTypes = "varchar";
            this.SEL_SENTAKU_SAKI.Location = new System.Drawing.Point(116, 390);
            this.SEL_SENTAKU_SAKI.MaxLength = 40;
            this.SEL_SENTAKU_SAKI.Name = "SEL_SENTAKU_SAKI";
            this.SEL_SENTAKU_SAKI.PopupAfterExecute = null;
            this.SEL_SENTAKU_SAKI.PopupBeforeExecute = null;
            this.SEL_SENTAKU_SAKI.PopupGetMasterField = "";
            this.SEL_SENTAKU_SAKI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEL_SENTAKU_SAKI.PopupSearchSendParams")));
            this.SEL_SENTAKU_SAKI.PopupSetFormField = "";
            this.SEL_SENTAKU_SAKI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SEL_SENTAKU_SAKI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEL_SENTAKU_SAKI.popupWindowSetting")));
            this.SEL_SENTAKU_SAKI.ReadOnly = true;
            this.SEL_SENTAKU_SAKI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEL_SENTAKU_SAKI.RegistCheckMethod")));
            this.SEL_SENTAKU_SAKI.SetFormField = "";
            this.SEL_SENTAKU_SAKI.Size = new System.Drawing.Size(349, 20);
            this.SEL_SENTAKU_SAKI.TabIndex = 71;
            this.SEL_SENTAKU_SAKI.TabStop = false;
            this.SEL_SENTAKU_SAKI.Tag = " ";
            // 
            // SEL_LB_SENTAKU_SAKI
            // 
            this.SEL_LB_SENTAKU_SAKI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SEL_LB_SENTAKU_SAKI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.SEL_LB_SENTAKU_SAKI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SEL_LB_SENTAKU_SAKI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SEL_LB_SENTAKU_SAKI.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.SEL_LB_SENTAKU_SAKI.ForeColor = System.Drawing.Color.White;
            this.SEL_LB_SENTAKU_SAKI.Location = new System.Drawing.Point(3, 390);
            this.SEL_LB_SENTAKU_SAKI.Name = "SEL_LB_SENTAKU_SAKI";
            this.SEL_LB_SENTAKU_SAKI.Size = new System.Drawing.Size(106, 20);
            this.SEL_LB_SENTAKU_SAKI.TabIndex = 475;
            this.SEL_LB_SENTAKU_SAKI.Text = "選択先";
            this.SEL_LB_SENTAKU_SAKI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SEL_TORIHIKISAKI_CD
            // 
            this.SEL_TORIHIKISAKI_CD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SEL_TORIHIKISAKI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.SEL_TORIHIKISAKI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SEL_TORIHIKISAKI_CD.CharacterLimitList = null;
            this.SEL_TORIHIKISAKI_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.SEL_TORIHIKISAKI_CD.DBFieldsName = "TORIHIKISAKI_CD";
            this.SEL_TORIHIKISAKI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.SEL_TORIHIKISAKI_CD.DisplayItemName = "取引先CD";
            this.SEL_TORIHIKISAKI_CD.DisplayPopUp = null;
            this.SEL_TORIHIKISAKI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEL_TORIHIKISAKI_CD.FocusOutCheckMethod")));
            this.SEL_TORIHIKISAKI_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.SEL_TORIHIKISAKI_CD.ForeColor = System.Drawing.Color.Black;
            this.SEL_TORIHIKISAKI_CD.GetCodeMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.SEL_TORIHIKISAKI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SEL_TORIHIKISAKI_CD.IsInputErrorOccured = false;
            this.SEL_TORIHIKISAKI_CD.ItemDefinedTypes = "varchar";
            this.SEL_TORIHIKISAKI_CD.Location = new System.Drawing.Point(116, 413);
            this.SEL_TORIHIKISAKI_CD.MaxLength = 6;
            this.SEL_TORIHIKISAKI_CD.Name = "SEL_TORIHIKISAKI_CD";
            this.SEL_TORIHIKISAKI_CD.PopupAfterExecute = new System.Action<r_framework.CustomControl.ICustomControl, System.Windows.Forms.DialogResult>(this.SEL_TORIHIKISAKI_CD_After);
            this.SEL_TORIHIKISAKI_CD.PopupBeforeExecute = null;
            this.SEL_TORIHIKISAKI_CD.PopupBeforeExecuteMethod = "";
            this.SEL_TORIHIKISAKI_CD.PopupGetMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.SEL_TORIHIKISAKI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEL_TORIHIKISAKI_CD.PopupSearchSendParams")));
            this.SEL_TORIHIKISAKI_CD.PopupSetFormField = "SEL_TORIHIKISAKI_CD,SEL_TORIHIKISAKI_NAME_RYAKU";
            this.SEL_TORIHIKISAKI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.SEL_TORIHIKISAKI_CD.PopupWindowName = "検索共通ポップアップ";
            this.SEL_TORIHIKISAKI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEL_TORIHIKISAKI_CD.popupWindowSetting")));
            this.SEL_TORIHIKISAKI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEL_TORIHIKISAKI_CD.RegistCheckMethod")));
            this.SEL_TORIHIKISAKI_CD.SetFormField = "SEL_TORIHIKISAKI_CD,SEL_TORIHIKISAKI_NAME_RYAKU";
            this.SEL_TORIHIKISAKI_CD.ShortItemName = "取引先CD";
            this.SEL_TORIHIKISAKI_CD.Size = new System.Drawing.Size(60, 20);
            this.SEL_TORIHIKISAKI_CD.TabIndex = 72;
            this.SEL_TORIHIKISAKI_CD.Tag = " ";
            this.SEL_TORIHIKISAKI_CD.ZeroPaddengFlag = true;
            this.SEL_TORIHIKISAKI_CD.Enter += new System.EventHandler(this.SEL_TORIHIKISAKI_CD_Enter);
            this.SEL_TORIHIKISAKI_CD.Validating += new System.ComponentModel.CancelEventHandler(this.SEL_TORIHIKISAKI_CD_Validating);
            // 
            // SEL_TORIHIKISAKI_BTN
            // 
            this.SEL_TORIHIKISAKI_BTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SEL_TORIHIKISAKI_BTN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.SEL_TORIHIKISAKI_BTN.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SEL_TORIHIKISAKI_BTN.DBFieldsName = null;
            this.SEL_TORIHIKISAKI_BTN.DefaultBackColor = System.Drawing.Color.Empty;
            this.SEL_TORIHIKISAKI_BTN.DisplayItemName = "取引先CD";
            this.SEL_TORIHIKISAKI_BTN.DisplayPopUp = null;
            this.SEL_TORIHIKISAKI_BTN.ErrorMessage = null;
            this.SEL_TORIHIKISAKI_BTN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEL_TORIHIKISAKI_BTN.FocusOutCheckMethod")));
            this.SEL_TORIHIKISAKI_BTN.Font = new System.Drawing.Font("MS Gothic", 11.25F);
            this.SEL_TORIHIKISAKI_BTN.GetCodeMasterField = null;
            this.SEL_TORIHIKISAKI_BTN.Image = ((System.Drawing.Image)(resources.GetObject("SEL_TORIHIKISAKI_BTN.Image")));
            this.SEL_TORIHIKISAKI_BTN.ItemDefinedTypes = null;
            this.SEL_TORIHIKISAKI_BTN.LinkedSettingTextBox = "SEL_TORIHIKISAKI_CD";
            this.SEL_TORIHIKISAKI_BTN.LinkedTextBoxs = null;
            this.SEL_TORIHIKISAKI_BTN.Location = new System.Drawing.Point(467, 413);
            this.SEL_TORIHIKISAKI_BTN.Name = "SEL_TORIHIKISAKI_BTN";
            this.SEL_TORIHIKISAKI_BTN.PopupAfterExecute = new System.Action<r_framework.CustomControl.ICustomControl, System.Windows.Forms.DialogResult>(this.SEL_TORIHIKISAKI_CD_After);
            this.SEL_TORIHIKISAKI_BTN.PopupBeforeExecute = null;
            this.SEL_TORIHIKISAKI_BTN.PopupGetMasterField = "";
            this.SEL_TORIHIKISAKI_BTN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEL_TORIHIKISAKI_BTN.PopupSearchSendParams")));
            this.SEL_TORIHIKISAKI_BTN.PopupSetFormField = "";
            this.SEL_TORIHIKISAKI_BTN.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
            this.SEL_TORIHIKISAKI_BTN.PopupWindowName = "";
            this.SEL_TORIHIKISAKI_BTN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEL_TORIHIKISAKI_BTN.popupWindowSetting")));
            this.SEL_TORIHIKISAKI_BTN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEL_TORIHIKISAKI_BTN.RegistCheckMethod")));
            this.SEL_TORIHIKISAKI_BTN.SearchDisplayFlag = 0;
            this.SEL_TORIHIKISAKI_BTN.SetFormField = "";
            this.SEL_TORIHIKISAKI_BTN.ShortItemName = "取引先CD";
            this.SEL_TORIHIKISAKI_BTN.Size = new System.Drawing.Size(22, 21);
            this.SEL_TORIHIKISAKI_BTN.TabIndex = 74;
            this.SEL_TORIHIKISAKI_BTN.TabStop = false;
            this.SEL_TORIHIKISAKI_BTN.Tag = " ";
            this.SEL_TORIHIKISAKI_BTN.UseVisualStyleBackColor = false;
            this.SEL_TORIHIKISAKI_BTN.ZeroPaddengFlag = false;
            // 
            // SEL_TORIHIKISAKI_NAME_RYAKU
            // 
            this.SEL_TORIHIKISAKI_NAME_RYAKU.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SEL_TORIHIKISAKI_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.SEL_TORIHIKISAKI_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SEL_TORIHIKISAKI_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.SEL_TORIHIKISAKI_NAME_RYAKU.DBFieldsName = "";
            this.SEL_TORIHIKISAKI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.SEL_TORIHIKISAKI_NAME_RYAKU.DisplayItemName = "";
            this.SEL_TORIHIKISAKI_NAME_RYAKU.DisplayPopUp = null;
            this.SEL_TORIHIKISAKI_NAME_RYAKU.ErrorMessage = "";
            this.SEL_TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEL_TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod")));
            this.SEL_TORIHIKISAKI_NAME_RYAKU.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.SEL_TORIHIKISAKI_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.SEL_TORIHIKISAKI_NAME_RYAKU.GetCodeMasterField = "";
            this.SEL_TORIHIKISAKI_NAME_RYAKU.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.SEL_TORIHIKISAKI_NAME_RYAKU.IsInputErrorOccured = false;
            this.SEL_TORIHIKISAKI_NAME_RYAKU.ItemDefinedTypes = "";
            this.SEL_TORIHIKISAKI_NAME_RYAKU.Location = new System.Drawing.Point(174, 413);
            this.SEL_TORIHIKISAKI_NAME_RYAKU.MaxLength = 40;
            this.SEL_TORIHIKISAKI_NAME_RYAKU.Name = "SEL_TORIHIKISAKI_NAME_RYAKU";
            this.SEL_TORIHIKISAKI_NAME_RYAKU.PopupAfterExecute = null;
            this.SEL_TORIHIKISAKI_NAME_RYAKU.PopupBeforeExecute = null;
            this.SEL_TORIHIKISAKI_NAME_RYAKU.PopupGetMasterField = "";
            this.SEL_TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEL_TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams")));
            this.SEL_TORIHIKISAKI_NAME_RYAKU.PopupSetFormField = "";
            this.SEL_TORIHIKISAKI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SEL_TORIHIKISAKI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEL_TORIHIKISAKI_NAME_RYAKU.popupWindowSetting")));
            this.SEL_TORIHIKISAKI_NAME_RYAKU.ReadOnly = true;
            this.SEL_TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEL_TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod")));
            this.SEL_TORIHIKISAKI_NAME_RYAKU.SetFormField = "";
            this.SEL_TORIHIKISAKI_NAME_RYAKU.Size = new System.Drawing.Size(291, 20);
            this.SEL_TORIHIKISAKI_NAME_RYAKU.TabIndex = 73;
            this.SEL_TORIHIKISAKI_NAME_RYAKU.TabStop = false;
            this.SEL_TORIHIKISAKI_NAME_RYAKU.Tag = "　";
            // 
            // SEL_LB_TORIHIKISAKI
            // 
            this.SEL_LB_TORIHIKISAKI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SEL_LB_TORIHIKISAKI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.SEL_LB_TORIHIKISAKI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SEL_LB_TORIHIKISAKI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SEL_LB_TORIHIKISAKI.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.SEL_LB_TORIHIKISAKI.ForeColor = System.Drawing.Color.White;
            this.SEL_LB_TORIHIKISAKI.Location = new System.Drawing.Point(3, 413);
            this.SEL_LB_TORIHIKISAKI.Name = "SEL_LB_TORIHIKISAKI";
            this.SEL_LB_TORIHIKISAKI.Size = new System.Drawing.Size(106, 20);
            this.SEL_LB_TORIHIKISAKI.TabIndex = 481;
            this.SEL_LB_TORIHIKISAKI.Text = "取引先";
            this.SEL_LB_TORIHIKISAKI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SEL_SETTEI_BTN_2
            // 
            this.SEL_SETTEI_BTN_2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SEL_SETTEI_BTN_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.SEL_SETTEI_BTN_2.Location = new System.Drawing.Point(956, 413);
            this.SEL_SETTEI_BTN_2.Name = "SEL_SETTEI_BTN_2";
            this.SEL_SETTEI_BTN_2.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.SEL_SETTEI_BTN_2.Size = new System.Drawing.Size(50, 22);
            this.SEL_SETTEI_BTN_2.TabIndex = 484;
            this.SEL_SETTEI_BTN_2.TabStop = false;
            this.SEL_SETTEI_BTN_2.Tag = " ";
            this.SEL_SETTEI_BTN_2.Text = "設定";
            this.SEL_SETTEI_BTN_2.UseVisualStyleBackColor = true;
            this.SEL_SETTEI_BTN_2.Click += new System.EventHandler(this.SEL_SETTEI_BTN_2_Click);
            // 
            // SEL_FURIKOMI_JINMEI_2
            // 
            this.SEL_FURIKOMI_JINMEI_2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SEL_FURIKOMI_JINMEI_2.BackColor = System.Drawing.SystemColors.Window;
            this.SEL_FURIKOMI_JINMEI_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SEL_FURIKOMI_JINMEI_2.ChangeUpperCase = true;
            this.SEL_FURIKOMI_JINMEI_2.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.SEL_FURIKOMI_JINMEI_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.SEL_FURIKOMI_JINMEI_2.DisplayPopUp = null;
            this.SEL_FURIKOMI_JINMEI_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEL_FURIKOMI_JINMEI_2.FocusOutCheckMethod")));
            this.SEL_FURIKOMI_JINMEI_2.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.SEL_FURIKOMI_JINMEI_2.ForeColor = System.Drawing.Color.Black;
            this.SEL_FURIKOMI_JINMEI_2.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            this.SEL_FURIKOMI_JINMEI_2.IsInputErrorOccured = false;
            this.SEL_FURIKOMI_JINMEI_2.Location = new System.Drawing.Point(605, 413);
            this.SEL_FURIKOMI_JINMEI_2.MaxLength = 40;
            this.SEL_FURIKOMI_JINMEI_2.Name = "SEL_FURIKOMI_JINMEI_2";
            this.SEL_FURIKOMI_JINMEI_2.PopupAfterExecute = null;
            this.SEL_FURIKOMI_JINMEI_2.PopupBeforeExecute = null;
            this.SEL_FURIKOMI_JINMEI_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEL_FURIKOMI_JINMEI_2.PopupSearchSendParams")));
            this.SEL_FURIKOMI_JINMEI_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SEL_FURIKOMI_JINMEI_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEL_FURIKOMI_JINMEI_2.popupWindowSetting")));
            this.SEL_FURIKOMI_JINMEI_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEL_FURIKOMI_JINMEI_2.RegistCheckMethod")));
            this.SEL_FURIKOMI_JINMEI_2.Size = new System.Drawing.Size(349, 20);
            this.SEL_FURIKOMI_JINMEI_2.TabIndex = 76;
            this.SEL_FURIKOMI_JINMEI_2.Tag = " ";
            // 
            // SEL_LB_FURIKOMI_JINMEI_2
            // 
            this.SEL_LB_FURIKOMI_JINMEI_2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SEL_LB_FURIKOMI_JINMEI_2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.SEL_LB_FURIKOMI_JINMEI_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SEL_LB_FURIKOMI_JINMEI_2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SEL_LB_FURIKOMI_JINMEI_2.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.SEL_LB_FURIKOMI_JINMEI_2.ForeColor = System.Drawing.Color.White;
            this.SEL_LB_FURIKOMI_JINMEI_2.Location = new System.Drawing.Point(493, 412);
            this.SEL_LB_FURIKOMI_JINMEI_2.Name = "SEL_LB_FURIKOMI_JINMEI_2";
            this.SEL_LB_FURIKOMI_JINMEI_2.Size = new System.Drawing.Size(106, 20);
            this.SEL_LB_FURIKOMI_JINMEI_2.TabIndex = 483;
            this.SEL_LB_FURIKOMI_JINMEI_2.Text = "振込人名２";
            this.SEL_LB_FURIKOMI_JINMEI_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1024, 480);
            this.Controls.Add(this.SEL_SETTEI_BTN_2);
            this.Controls.Add(this.SEL_FURIKOMI_JINMEI_2);
            this.Controls.Add(this.SEL_LB_FURIKOMI_JINMEI_2);
            this.Controls.Add(this.SEL_TORIHIKISAKI_CD);
            this.Controls.Add(this.SEL_TORIHIKISAKI_BTN);
            this.Controls.Add(this.SEL_TORIHIKISAKI_NAME_RYAKU);
            this.Controls.Add(this.SEL_LB_TORIHIKISAKI);
            this.Controls.Add(this.SEL_FURIKOMI_JINMEI_1);
            this.Controls.Add(this.SEL_LB_FURIKOMI_JINMEI_1);
            this.Controls.Add(this.SEL_SETTEI_BTN_1);
            this.Controls.Add(this.SEL_SENTAKU_SAKI);
            this.Controls.Add(this.SEL_LB_SENTAKU_SAKI);
            this.Controls.Add(this.BANK_SHITEN_CD);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SEARCH_BANK_SHITEN);
            this.Controls.Add(this.SEARCH_BANK);
            this.Controls.Add(this.KOUZA_NO);
            this.Controls.Add(this.KOUZA_SHURUI);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.BANK_SHITEN_NAME_RYAKU);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.BANK_CD);
            this.Controls.Add(this.BANK_NAME_RYAKU);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.customPanel7);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.SAKUSEI_BI_FROM);
            this.Controls.Add(this.SAKUSEI_BI_TO);
            this.Controls.Add(this.SAKUSEI_BI_LABEL);
            this.Controls.Add(this.ALERT_DATE_FROM_TO_LABEL);
            this.Controls.Add(this.SEARCH_TORIKOMI);
            this.Controls.Add(this.TORIKOMI);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SEARCH_TORIHIKISAKI);
            this.Controls.Add(this.TORIHIKISAKI_CD);
            this.Controls.Add(this.TORIHIKISAKI_NAME_RYAKU);
            this.Controls.Add(this.TORIHIKISAKI_LABEL);
            this.Name = "UIForm";
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_LABEL, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_CD, 0);
            this.Controls.SetChildIndex(this.SEARCH_TORIHIKISAKI, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.TORIKOMI, 0);
            this.Controls.SetChildIndex(this.SEARCH_TORIKOMI, 0);
            this.Controls.SetChildIndex(this.ALERT_DATE_FROM_TO_LABEL, 0);
            this.Controls.SetChildIndex(this.SAKUSEI_BI_LABEL, 0);
            this.Controls.SetChildIndex(this.SAKUSEI_BI_TO, 0);
            this.Controls.SetChildIndex(this.SAKUSEI_BI_FROM, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.customPanel7, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.BANK_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.BANK_CD, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.BANK_SHITEN_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.KOUZA_SHURUI, 0);
            this.Controls.SetChildIndex(this.KOUZA_NO, 0);
            this.Controls.SetChildIndex(this.SEARCH_BANK, 0);
            this.Controls.SetChildIndex(this.SEARCH_BANK_SHITEN, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.BANK_SHITEN_CD, 0);
            this.Controls.SetChildIndex(this.SEL_LB_SENTAKU_SAKI, 0);
            this.Controls.SetChildIndex(this.SEL_SENTAKU_SAKI, 0);
            this.Controls.SetChildIndex(this.SEL_SETTEI_BTN_1, 0);
            this.Controls.SetChildIndex(this.SEL_LB_FURIKOMI_JINMEI_1, 0);
            this.Controls.SetChildIndex(this.SEL_FURIKOMI_JINMEI_1, 0);
            this.Controls.SetChildIndex(this.SEL_LB_TORIHIKISAKI, 0);
            this.Controls.SetChildIndex(this.SEL_TORIHIKISAKI_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.SEL_TORIHIKISAKI_BTN, 0);
            this.Controls.SetChildIndex(this.SEL_TORIHIKISAKI_CD, 0);
            this.Controls.SetChildIndex(this.SEL_LB_FURIKOMI_JINMEI_2, 0);
            this.Controls.SetChildIndex(this.SEL_FURIKOMI_JINMEI_2, 0);
            this.Controls.SetChildIndex(this.SEL_SETTEI_BTN_2, 0);
            this.customPanel7.ResumeLayout(false);
            this.customPanel7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private r_framework.CustomControl.CustomAlphaNumTextBox txt_TorihikisakiCD;
        private System.Windows.Forms.Label lbl_Torohikisaki;
        private r_framework.CustomControl.CustomTextBox txt_TorihikisakiName;
        internal r_framework.CustomControl.CustomPopupOpenButton SEARCH_TORIHIKISAKI;
        internal r_framework.CustomControl.CustomAlphaNumTextBox TORIHIKISAKI_CD;
        internal r_framework.CustomControl.CustomTextBox TORIHIKISAKI_NAME_RYAKU;
        internal System.Windows.Forms.Label TORIHIKISAKI_LABEL;
        internal r_framework.CustomControl.CustomButton SEARCH_TORIKOMI;
        internal r_framework.CustomControl.CustomTextBox TORIKOMI;
        private System.Windows.Forms.Label label2;
        internal r_framework.CustomControl.CustomDateTimePicker SAKUSEI_BI_FROM;
        internal r_framework.CustomControl.CustomDateTimePicker SAKUSEI_BI_TO;
        private System.Windows.Forms.Label SAKUSEI_BI_LABEL;
        private System.Windows.Forms.Label ALERT_DATE_FROM_TO_LABEL;
        private r_framework.CustomControl.CustomPanel customPanel7;
        public r_framework.CustomControl.CustomRadioButton SAKUSEI_KBN_3;
        public r_framework.CustomControl.CustomRadioButton SAKUSEI_KBN_2;
        public r_framework.CustomControl.CustomNumericTextBox2 SAKUSEI_KBN;
        public r_framework.CustomControl.CustomRadioButton SAKUSEI_KBN_1;
        private System.Windows.Forms.Label label9;
        internal r_framework.CustomControl.CustomTextBox BANK_NAME_RYAKU;
        public System.Windows.Forms.Label label3;
        internal r_framework.CustomControl.CustomAlphaNumTextBox BANK_CD;
        internal r_framework.CustomControl.CustomTextBox BANK_SHITEN_NAME_RYAKU;
        internal r_framework.CustomControl.CustomAlphaNumTextBox BANK_SHITEN_CD;
        public System.Windows.Forms.Label label4;
        internal r_framework.CustomControl.CustomTextBox KOUZA_NO;
        internal r_framework.CustomControl.CustomTextBox KOUZA_SHURUI;
        public System.Windows.Forms.Label label6;
        internal r_framework.CustomControl.CustomPopupOpenButton SEARCH_BANK;
        internal r_framework.CustomControl.CustomPopupOpenButton SEARCH_BANK_SHITEN;
        public System.Windows.Forms.Label label1;
        public r_framework.CustomControl.CustomTextBox SEL_FURIKOMI_JINMEI_1;
        internal System.Windows.Forms.Label SEL_LB_FURIKOMI_JINMEI_1;
        internal r_framework.CustomControl.CustomButton SEL_SETTEI_BTN_1;
        internal r_framework.CustomControl.CustomTextBox SEL_SENTAKU_SAKI;
        internal System.Windows.Forms.Label SEL_LB_SENTAKU_SAKI;
        public r_framework.CustomControl.CustomAlphaNumTextBox SEL_TORIHIKISAKI_CD;
        internal r_framework.CustomControl.CustomPopupOpenButton SEL_TORIHIKISAKI_BTN;
        internal r_framework.CustomControl.CustomTextBox SEL_TORIHIKISAKI_NAME_RYAKU;
        internal System.Windows.Forms.Label SEL_LB_TORIHIKISAKI;
        internal r_framework.CustomControl.CustomButton SEL_SETTEI_BTN_2;
        public r_framework.CustomControl.CustomTextBox SEL_FURIKOMI_JINMEI_2;
        internal System.Windows.Forms.Label SEL_LB_FURIKOMI_JINMEI_2;
    }
}