namespace Shougun.Core.BusinessManagement.DenpyouIkkatuPopupUpdate.APP
{
    partial class DenpyouiTankakkatsuPopupForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DenpyouiTankakkatsuPopupForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto5 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto6 = new r_framework.Dto.RangeSettingDto();
            this.DENPYOU_JYOUHOU_LABLE = new System.Windows.Forms.Label();
            this.bt_func12 = new r_framework.CustomControl.CustomButton();
            this.bt_func11 = new r_framework.CustomControl.CustomButton();
            this.bt_func8 = new r_framework.CustomControl.CustomButton();
            this.TANNKA = new r_framework.CustomControl.CustomNumericTextBox2();
            this.tannkaLabel = new System.Windows.Forms.Label();
            this.KINNGAKU = new r_framework.CustomControl.CustomNumericTextBox2();
            this.kinngakuLabel = new System.Windows.Forms.Label();
            this.TANNKA_ZOUGENN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.tankaZougennLabel = new System.Windows.Forms.Label();
            this.hinmeiLabel = new System.Windows.Forms.Label();
            this.HINMEI_CD_UKEIRE = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.HINMEI_NAME_UKEIRE = new r_framework.CustomControl.CustomTextBox();
            this.denpyoukbnLabel = new System.Windows.Forms.Label();
            this.DENPYOU_KBN_NAME = new r_framework.CustomControl.CustomTextBox();
            this.SUURYOU = new r_framework.CustomControl.CustomNumericTextBox2();
            this.suryouLabel = new System.Windows.Forms.Label();
            this.UNIT_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label2 = new System.Windows.Forms.Label();
            this.UNIT_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.MEISAI_BIKOU = new r_framework.CustomControl.CustomTextBox();
            this.MEISAI_BIKOU_LABEL = new System.Windows.Forms.Label();
            this.DENPYOU_KBN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.HINMEI_CD_SHUKKA = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.HINMEI_NAME_SHUKKA = new r_framework.CustomControl.CustomTextBox();
            this.HINMEI_CD_URSH = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.HINMEI_NAME_URSH = new r_framework.CustomControl.CustomTextBox();
            this.SuspendLayout();
            // 
            // DENPYOU_JYOUHOU_LABLE
            // 
            this.DENPYOU_JYOUHOU_LABLE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.DENPYOU_JYOUHOU_LABLE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DENPYOU_JYOUHOU_LABLE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DENPYOU_JYOUHOU_LABLE.Font = new System.Drawing.Font("ＭＳ ゴシック", 20.25F);
            this.DENPYOU_JYOUHOU_LABLE.ForeColor = System.Drawing.Color.White;
            this.DENPYOU_JYOUHOU_LABLE.Location = new System.Drawing.Point(12, 9);
            this.DENPYOU_JYOUHOU_LABLE.Name = "DENPYOU_JYOUHOU_LABLE";
            this.DENPYOU_JYOUHOU_LABLE.Size = new System.Drawing.Size(304, 34);
            this.DENPYOU_JYOUHOU_LABLE.TabIndex = 0;
            this.DENPYOU_JYOUHOU_LABLE.Text = "伝票明細　一括入力";
            this.DENPYOU_JYOUHOU_LABLE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_func12
            // 
            this.bt_func12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func12.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_func12.Location = new System.Drawing.Point(508, 324);
            this.bt_func12.Name = "bt_func12";
            this.bt_func12.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func12.Size = new System.Drawing.Size(80, 35);
            this.bt_func12.TabIndex = 11;
            this.bt_func12.TabStop = false;
            this.bt_func12.Tag = "";
            this.bt_func12.Text = "[F12]\r\n閉じる";
            this.bt_func12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func12.UseVisualStyleBackColor = false;
            // 
            // bt_func11
            // 
            this.bt_func11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func11.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func11.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_func11.Location = new System.Drawing.Point(427, 324);
            this.bt_func11.Name = "bt_func11";
            this.bt_func11.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func11.Size = new System.Drawing.Size(80, 35);
            this.bt_func11.TabIndex = 10;
            this.bt_func11.TabStop = false;
            this.bt_func11.Tag = "";
            this.bt_func11.Text = "[F11]\r\n取消";
            this.bt_func11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func11.UseVisualStyleBackColor = false;
            // 
            // bt_func8
            // 
            this.bt_func8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func8.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_func8.Location = new System.Drawing.Point(236, 324);
            this.bt_func8.Name = "bt_func8";
            this.bt_func8.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func8.Size = new System.Drawing.Size(80, 35);
            this.bt_func8.TabIndex = 856;
            this.bt_func8.TabStop = false;
            this.bt_func8.Tag = "";
            this.bt_func8.Text = "[F8]\r\n入力";
            this.bt_func8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func8.UseVisualStyleBackColor = false;
            // 
            // TANNKA
            // 
            this.TANNKA.BackColor = System.Drawing.SystemColors.Window;
            this.TANNKA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TANNKA.DBFieldsName = "";
            this.TANNKA.DefaultBackColor = System.Drawing.Color.Empty;
            this.TANNKA.DisplayItemName = "検索条件";
            this.TANNKA.DisplayPopUp = null;
            this.TANNKA.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TANNKA.FocusOutCheckMethod")));
            this.TANNKA.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TANNKA.ForeColor = System.Drawing.Color.Black;
            this.TANNKA.FormatSetting = "システム設定(単価書式)";
            this.TANNKA.IsInputErrorOccured = false;
            this.TANNKA.ItemDefinedTypes = "";
            this.TANNKA.Location = new System.Drawing.Point(105, 62);
            this.TANNKA.Name = "TANNKA";
            this.TANNKA.PopupAfterExecute = null;
            this.TANNKA.PopupBeforeExecute = null;
            this.TANNKA.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TANNKA.PopupSearchSendParams")));
            this.TANNKA.PopupSetFormField = "";
            this.TANNKA.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TANNKA.PopupWindowName = "";
            this.TANNKA.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TANNKA.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            1410065407,
            2,
            0,
            196608});
            rangeSettingDto1.Min = new decimal(new int[] {
            1410065407,
            2,
            0,
            -2147287040});
            this.TANNKA.RangeSetting = rangeSettingDto1;
            this.TANNKA.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TANNKA.RegistCheckMethod")));
            this.TANNKA.SetFormField = "";
            this.TANNKA.ShortItemName = "検索条件";
            this.TANNKA.Size = new System.Drawing.Size(107, 20);
            this.TANNKA.TabIndex = 0;
            this.TANNKA.Tag = "単価を入力してください";
            this.TANNKA.WordWrap = false;
            this.TANNKA.TextChanged += new System.EventHandler(this.TANNKA_TextChanged);
            this.TANNKA.Leave += new System.EventHandler(this.TANNKA_Leave);
            // 
            // tannkaLabel
            // 
            this.tannkaLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.tannkaLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tannkaLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tannkaLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tannkaLabel.ForeColor = System.Drawing.Color.White;
            this.tannkaLabel.Location = new System.Drawing.Point(12, 62);
            this.tannkaLabel.Name = "tannkaLabel";
            this.tannkaLabel.Size = new System.Drawing.Size(93, 20);
            this.tannkaLabel.TabIndex = 858;
            this.tannkaLabel.Text = "単価";
            this.tannkaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KINNGAKU
            // 
            this.KINNGAKU.BackColor = System.Drawing.SystemColors.Window;
            this.KINNGAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KINNGAKU.CustomFormatSetting = "#,##0";
            this.KINNGAKU.DBFieldsName = "";
            this.KINNGAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.KINNGAKU.DisplayItemName = "検索条件";
            this.KINNGAKU.DisplayPopUp = null;
            this.KINNGAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KINNGAKU.FocusOutCheckMethod")));
            this.KINNGAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KINNGAKU.ForeColor = System.Drawing.Color.Black;
            this.KINNGAKU.FormatSetting = "カスタム";
            this.KINNGAKU.IsInputErrorOccured = false;
            this.KINNGAKU.ItemDefinedTypes = "";
            this.KINNGAKU.Location = new System.Drawing.Point(338, 62);
            this.KINNGAKU.Name = "KINNGAKU";
            this.KINNGAKU.PopupAfterExecute = null;
            this.KINNGAKU.PopupBeforeExecute = null;
            this.KINNGAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KINNGAKU.PopupSearchSendParams")));
            this.KINNGAKU.PopupSetFormField = "";
            this.KINNGAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KINNGAKU.PopupWindowName = "";
            this.KINNGAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KINNGAKU.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            -727379969,
            232,
            0,
            196608});
            rangeSettingDto2.Min = new decimal(new int[] {
            -727379969,
            232,
            0,
            -2147287040});
            this.KINNGAKU.RangeSetting = rangeSettingDto2;
            this.KINNGAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KINNGAKU.RegistCheckMethod")));
            this.KINNGAKU.SetFormField = "";
            this.KINNGAKU.ShortItemName = "";
            this.KINNGAKU.Size = new System.Drawing.Size(107, 20);
            this.KINNGAKU.TabIndex = 1;
            this.KINNGAKU.Tag = "金額を入力してください";
            this.KINNGAKU.WordWrap = false;
            this.KINNGAKU.TextChanged += new System.EventHandler(this.KINNGAKU_TextChanged);
            this.KINNGAKU.Leave += new System.EventHandler(this.KINNGAKU_Leave);
            // 
            // kinngakuLabel
            // 
            this.kinngakuLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.kinngakuLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.kinngakuLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.kinngakuLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.kinngakuLabel.ForeColor = System.Drawing.Color.White;
            this.kinngakuLabel.Location = new System.Drawing.Point(245, 62);
            this.kinngakuLabel.Name = "kinngakuLabel";
            this.kinngakuLabel.Size = new System.Drawing.Size(93, 20);
            this.kinngakuLabel.TabIndex = 860;
            this.kinngakuLabel.Text = "金額";
            this.kinngakuLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TANNKA_ZOUGENN
            // 
            this.TANNKA_ZOUGENN.BackColor = System.Drawing.SystemColors.Window;
            this.TANNKA_ZOUGENN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TANNKA_ZOUGENN.DBFieldsName = "";
            this.TANNKA_ZOUGENN.DefaultBackColor = System.Drawing.Color.Empty;
            this.TANNKA_ZOUGENN.DisplayItemName = "検索条件";
            this.TANNKA_ZOUGENN.DisplayPopUp = null;
            this.TANNKA_ZOUGENN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TANNKA_ZOUGENN.FocusOutCheckMethod")));
            this.TANNKA_ZOUGENN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TANNKA_ZOUGENN.ForeColor = System.Drawing.Color.Black;
            this.TANNKA_ZOUGENN.FormatSetting = "システム設定(単価書式)";
            this.TANNKA_ZOUGENN.IsInputErrorOccured = false;
            this.TANNKA_ZOUGENN.ItemDefinedTypes = "";
            this.TANNKA_ZOUGENN.Location = new System.Drawing.Point(105, 100);
            this.TANNKA_ZOUGENN.Name = "TANNKA_ZOUGENN";
            this.TANNKA_ZOUGENN.PopupAfterExecute = null;
            this.TANNKA_ZOUGENN.PopupBeforeExecute = null;
            this.TANNKA_ZOUGENN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TANNKA_ZOUGENN.PopupSearchSendParams")));
            this.TANNKA_ZOUGENN.PopupSetFormField = "";
            this.TANNKA_ZOUGENN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TANNKA_ZOUGENN.PopupWindowName = "";
            this.TANNKA_ZOUGENN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TANNKA_ZOUGENN.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            1410065407,
            2,
            0,
            196608});
            rangeSettingDto3.Min = new decimal(new int[] {
            1410065407,
            2,
            0,
            -2147287040});
            this.TANNKA_ZOUGENN.RangeSetting = rangeSettingDto3;
            this.TANNKA_ZOUGENN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TANNKA_ZOUGENN.RegistCheckMethod")));
            this.TANNKA_ZOUGENN.SetFormField = "";
            this.TANNKA_ZOUGENN.ShortItemName = "検索条件";
            this.TANNKA_ZOUGENN.Size = new System.Drawing.Size(107, 20);
            this.TANNKA_ZOUGENN.TabIndex = 2;
            this.TANNKA_ZOUGENN.Tag = "単価を入力してください";
            this.TANNKA_ZOUGENN.WordWrap = false;
            this.TANNKA_ZOUGENN.TextChanged += new System.EventHandler(this.TANNKA_ZOUGENN_TextChanged);
            this.TANNKA_ZOUGENN.Leave += new System.EventHandler(this.TANNKA_ZOUGENN_Leave);
            // 
            // tankaZougennLabel
            // 
            this.tankaZougennLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.tankaZougennLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tankaZougennLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.tankaZougennLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tankaZougennLabel.ForeColor = System.Drawing.Color.White;
            this.tankaZougennLabel.Location = new System.Drawing.Point(12, 100);
            this.tankaZougennLabel.Name = "tankaZougennLabel";
            this.tankaZougennLabel.Size = new System.Drawing.Size(93, 20);
            this.tankaZougennLabel.TabIndex = 862;
            this.tankaZougennLabel.Text = "単価増減";
            this.tankaZougennLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // hinmeiLabel
            // 
            this.hinmeiLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.hinmeiLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hinmeiLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hinmeiLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.hinmeiLabel.ForeColor = System.Drawing.Color.White;
            this.hinmeiLabel.Location = new System.Drawing.Point(12, 157);
            this.hinmeiLabel.Name = "hinmeiLabel";
            this.hinmeiLabel.Size = new System.Drawing.Size(93, 20);
            this.hinmeiLabel.TabIndex = 863;
            this.hinmeiLabel.Text = "品名";
            this.hinmeiLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HINMEI_CD_UKEIRE
            // 
            this.HINMEI_CD_UKEIRE.BackColor = System.Drawing.SystemColors.Window;
            this.HINMEI_CD_UKEIRE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HINMEI_CD_UKEIRE.ChangeUpperCase = true;
            this.HINMEI_CD_UKEIRE.CharacterLimitList = null;
            this.HINMEI_CD_UKEIRE.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.HINMEI_CD_UKEIRE.DBFieldsName = "HINMEI_CD";
            this.HINMEI_CD_UKEIRE.DefaultBackColor = System.Drawing.Color.Empty;
            this.HINMEI_CD_UKEIRE.DisplayItemName = "CD";
            this.HINMEI_CD_UKEIRE.DisplayPopUp = null;
            this.HINMEI_CD_UKEIRE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_CD_UKEIRE.FocusOutCheckMethod")));
            this.HINMEI_CD_UKEIRE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HINMEI_CD_UKEIRE.ForeColor = System.Drawing.Color.Black;
            this.HINMEI_CD_UKEIRE.GetCodeMasterField = "HINMEI_CD,HINMEI_NAME_RYAKU";
            this.HINMEI_CD_UKEIRE.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.HINMEI_CD_UKEIRE.IsInputErrorOccured = false;
            this.HINMEI_CD_UKEIRE.ItemDefinedTypes = "varchar";
            this.HINMEI_CD_UKEIRE.Location = new System.Drawing.Point(105, 157);
            this.HINMEI_CD_UKEIRE.MaxLength = 6;
            this.HINMEI_CD_UKEIRE.Name = "HINMEI_CD_UKEIRE";
            this.HINMEI_CD_UKEIRE.PopupAfterExecute = null;
            this.HINMEI_CD_UKEIRE.PopupBeforeExecute = null;
            this.HINMEI_CD_UKEIRE.PopupGetMasterField = "HINMEI_CD,HINMEI_NAME_RYAKU";
            this.HINMEI_CD_UKEIRE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HINMEI_CD_UKEIRE.PopupSearchSendParams")));
            this.HINMEI_CD_UKEIRE.PopupSetFormField = "HINMEI_CD_UKEIRE,HINMEI_NAME_UKEIRE";
            this.HINMEI_CD_UKEIRE.PopupWindowId = r_framework.Const.WINDOW_ID.M_HINMEI;
            this.HINMEI_CD_UKEIRE.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.HINMEI_CD_UKEIRE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HINMEI_CD_UKEIRE.popupWindowSetting")));
            this.HINMEI_CD_UKEIRE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_CD_UKEIRE.RegistCheckMethod")));
            this.HINMEI_CD_UKEIRE.SetFormField = "HINMEI_CD_UKEIRE,HINMEI_NAME_UKEIRE";
            this.HINMEI_CD_UKEIRE.ShortItemName = "品名CD";
            this.HINMEI_CD_UKEIRE.Size = new System.Drawing.Size(55, 20);
            this.HINMEI_CD_UKEIRE.TabIndex = 7;
            this.HINMEI_CD_UKEIRE.Tag = "品名を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.HINMEI_CD_UKEIRE.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.HINMEI_CD_UKEIRE.ZeroPaddengFlag = true;
            this.HINMEI_CD_UKEIRE.Validating += new System.ComponentModel.CancelEventHandler(this.HINMEI_CD_UKEIRE_Validating);
            // 
            // HINMEI_NAME_UKEIRE
            // 
            this.HINMEI_NAME_UKEIRE.BackColor = System.Drawing.SystemColors.Window;
            this.HINMEI_NAME_UKEIRE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HINMEI_NAME_UKEIRE.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.HINMEI_NAME_UKEIRE.DBFieldsName = "";
            this.HINMEI_NAME_UKEIRE.DefaultBackColor = System.Drawing.Color.Empty;
            this.HINMEI_NAME_UKEIRE.DisplayItemName = "品名略称名";
            this.HINMEI_NAME_UKEIRE.DisplayPopUp = null;
            this.HINMEI_NAME_UKEIRE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_NAME_UKEIRE.FocusOutCheckMethod")));
            this.HINMEI_NAME_UKEIRE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HINMEI_NAME_UKEIRE.ForeColor = System.Drawing.Color.Black;
            this.HINMEI_NAME_UKEIRE.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.HINMEI_NAME_UKEIRE.IsInputErrorOccured = false;
            this.HINMEI_NAME_UKEIRE.ItemDefinedTypes = "";
            this.HINMEI_NAME_UKEIRE.Location = new System.Drawing.Point(159, 157);
            this.HINMEI_NAME_UKEIRE.MaxLength = 40;
            this.HINMEI_NAME_UKEIRE.Name = "HINMEI_NAME_UKEIRE";
            this.HINMEI_NAME_UKEIRE.PopupAfterExecute = null;
            this.HINMEI_NAME_UKEIRE.PopupBeforeExecute = null;
            this.HINMEI_NAME_UKEIRE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HINMEI_NAME_UKEIRE.PopupSearchSendParams")));
            this.HINMEI_NAME_UKEIRE.PopupSetFormField = "";
            this.HINMEI_NAME_UKEIRE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HINMEI_NAME_UKEIRE.PopupWindowName = "";
            this.HINMEI_NAME_UKEIRE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HINMEI_NAME_UKEIRE.popupWindowSetting")));
            this.HINMEI_NAME_UKEIRE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_NAME_UKEIRE.RegistCheckMethod")));
            this.HINMEI_NAME_UKEIRE.SetFormField = "";
            this.HINMEI_NAME_UKEIRE.ShortItemName = "品名略称名";
            this.HINMEI_NAME_UKEIRE.Size = new System.Drawing.Size(286, 20);
            this.HINMEI_NAME_UKEIRE.TabIndex = 8;
            this.HINMEI_NAME_UKEIRE.Tag = "  ";
            // 
            // denpyoukbnLabel
            // 
            this.denpyoukbnLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.denpyoukbnLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.denpyoukbnLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.denpyoukbnLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.denpyoukbnLabel.ForeColor = System.Drawing.Color.White;
            this.denpyoukbnLabel.Location = new System.Drawing.Point(12, 197);
            this.denpyoukbnLabel.Name = "denpyoukbnLabel";
            this.denpyoukbnLabel.Size = new System.Drawing.Size(93, 20);
            this.denpyoukbnLabel.TabIndex = 866;
            this.denpyoukbnLabel.Text = "伝票区分";
            this.denpyoukbnLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DENPYOU_KBN_NAME
            // 
            this.DENPYOU_KBN_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.DENPYOU_KBN_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DENPYOU_KBN_NAME.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.DENPYOU_KBN_NAME.DBFieldsName = "";
            this.DENPYOU_KBN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.DENPYOU_KBN_NAME.DisplayItemName = "";
            this.DENPYOU_KBN_NAME.DisplayPopUp = null;
            this.DENPYOU_KBN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_KBN_NAME.FocusOutCheckMethod")));
            this.DENPYOU_KBN_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DENPYOU_KBN_NAME.ForeColor = System.Drawing.Color.Black;
            this.DENPYOU_KBN_NAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.DENPYOU_KBN_NAME.IsInputErrorOccured = false;
            this.DENPYOU_KBN_NAME.ItemDefinedTypes = "";
            this.DENPYOU_KBN_NAME.Location = new System.Drawing.Point(105, 197);
            this.DENPYOU_KBN_NAME.MaxLength = 40;
            this.DENPYOU_KBN_NAME.Name = "DENPYOU_KBN_NAME";
            this.DENPYOU_KBN_NAME.PopupAfterExecute = null;
            this.DENPYOU_KBN_NAME.PopupBeforeExecute = null;
            this.DENPYOU_KBN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DENPYOU_KBN_NAME.PopupSearchSendParams")));
            this.DENPYOU_KBN_NAME.PopupSetFormField = "";
            this.DENPYOU_KBN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DENPYOU_KBN_NAME.PopupWindowName = "";
            this.DENPYOU_KBN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DENPYOU_KBN_NAME.popupWindowSetting")));
            this.DENPYOU_KBN_NAME.ReadOnly = true;
            this.DENPYOU_KBN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_KBN_NAME.RegistCheckMethod")));
            this.DENPYOU_KBN_NAME.SetFormField = "";
            this.DENPYOU_KBN_NAME.ShortItemName = "";
            this.DENPYOU_KBN_NAME.Size = new System.Drawing.Size(55, 20);
            this.DENPYOU_KBN_NAME.TabIndex = 9;
            this.DENPYOU_KBN_NAME.TabStop = false;
            this.DENPYOU_KBN_NAME.Tag = "  ";
            // 
            // SUURYOU
            // 
            this.SUURYOU.BackColor = System.Drawing.SystemColors.Window;
            this.SUURYOU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SUURYOU.DBFieldsName = "";
            this.SUURYOU.DefaultBackColor = System.Drawing.Color.Empty;
            this.SUURYOU.DisplayItemName = "";
            this.SUURYOU.DisplayPopUp = null;
            this.SUURYOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SUURYOU.FocusOutCheckMethod")));
            this.SUURYOU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SUURYOU.ForeColor = System.Drawing.Color.Black;
            this.SUURYOU.FormatSetting = "システム設定(数量書式)";
            this.SUURYOU.IsInputErrorOccured = false;
            this.SUURYOU.ItemDefinedTypes = "";
            this.SUURYOU.Location = new System.Drawing.Point(105, 236);
            this.SUURYOU.Name = "SUURYOU";
            this.SUURYOU.PopupAfterExecute = null;
            this.SUURYOU.PopupBeforeExecute = null;
            this.SUURYOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SUURYOU.PopupSearchSendParams")));
            this.SUURYOU.PopupSetFormField = "";
            this.SUURYOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SUURYOU.PopupWindowName = "";
            this.SUURYOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SUURYOU.popupWindowSetting")));
            rangeSettingDto4.Max = new decimal(new int[] {
            -727379969,
            232,
            0,
            196608});
            rangeSettingDto4.Min = new decimal(new int[] {
            -727379969,
            232,
            0,
            -2147287040});
            this.SUURYOU.RangeSetting = rangeSettingDto4;
            this.SUURYOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SUURYOU.RegistCheckMethod")));
            this.SUURYOU.SetFormField = "";
            this.SUURYOU.ShortItemName = "";
            this.SUURYOU.Size = new System.Drawing.Size(131, 20);
            this.SUURYOU.TabIndex = 11;
            this.SUURYOU.Tag = "数量を入力してください";
            this.SUURYOU.WordWrap = false;
            // 
            // suryouLabel
            // 
            this.suryouLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.suryouLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.suryouLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.suryouLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.suryouLabel.ForeColor = System.Drawing.Color.White;
            this.suryouLabel.Location = new System.Drawing.Point(12, 236);
            this.suryouLabel.Name = "suryouLabel";
            this.suryouLabel.Size = new System.Drawing.Size(93, 20);
            this.suryouLabel.TabIndex = 869;
            this.suryouLabel.Text = "数量";
            this.suryouLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UNIT_CD
            // 
            this.UNIT_CD.BackColor = System.Drawing.SystemColors.Window;
            this.UNIT_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UNIT_CD.ChangeUpperCase = true;
            this.UNIT_CD.DBFieldsName = "UNIT_CD";
            this.UNIT_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNIT_CD.DisplayItemName = "単位CD";
            this.UNIT_CD.DisplayPopUp = null;
            this.UNIT_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNIT_CD.FocusOutCheckMethod")));
            this.UNIT_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UNIT_CD.ForeColor = System.Drawing.Color.Black;
            this.UNIT_CD.GetCodeMasterField = "UNIT_CD, UNIT_NAME_RYAKU";
            this.UNIT_CD.IsInputErrorOccured = false;
            this.UNIT_CD.ItemDefinedTypes = "varchar";
            this.UNIT_CD.Location = new System.Drawing.Point(348, 236);
            this.UNIT_CD.Name = "UNIT_CD";
            this.UNIT_CD.PopupAfterExecute = null;
            this.UNIT_CD.PopupBeforeExecute = null;
            this.UNIT_CD.PopupGetMasterField = "UNIT_CD,UNIT_NAME_RYAKU";
            this.UNIT_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNIT_CD.PopupSearchSendParams")));
            this.UNIT_CD.PopupSetFormField = "UNIT_CD, UNIT_NAME_RYAKU";
            this.UNIT_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_UNIT;
            this.UNIT_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.UNIT_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNIT_CD.popupWindowSetting")));
            rangeSettingDto5.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.UNIT_CD.RangeSetting = rangeSettingDto5;
            this.UNIT_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNIT_CD.RegistCheckMethod")));
            this.UNIT_CD.SetFormField = "UNIT_CD, UNIT_NAME_RYAKU";
            this.UNIT_CD.ShortItemName = "単位CD";
            this.UNIT_CD.Size = new System.Drawing.Size(31, 20);
            this.UNIT_CD.TabIndex = 12;
            this.UNIT_CD.Tag = "半角2桁以内で入力してください";
            this.UNIT_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UNIT_CD.WordWrap = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(255, 236);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 20);
            this.label2.TabIndex = 871;
            this.label2.Text = "単位";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UNIT_NAME_RYAKU
            // 
            this.UNIT_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.UNIT_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UNIT_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.UNIT_NAME_RYAKU.DBFieldsName = "";
            this.UNIT_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNIT_NAME_RYAKU.DisplayItemName = "";
            this.UNIT_NAME_RYAKU.DisplayPopUp = null;
            this.UNIT_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNIT_NAME_RYAKU.FocusOutCheckMethod")));
            this.UNIT_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UNIT_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.UNIT_NAME_RYAKU.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.UNIT_NAME_RYAKU.IsInputErrorOccured = false;
            this.UNIT_NAME_RYAKU.ItemDefinedTypes = "";
            this.UNIT_NAME_RYAKU.Location = new System.Drawing.Point(378, 236);
            this.UNIT_NAME_RYAKU.MaxLength = 40;
            this.UNIT_NAME_RYAKU.Name = "UNIT_NAME_RYAKU";
            this.UNIT_NAME_RYAKU.PopupAfterExecute = null;
            this.UNIT_NAME_RYAKU.PopupBeforeExecute = null;
            this.UNIT_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNIT_NAME_RYAKU.PopupSearchSendParams")));
            this.UNIT_NAME_RYAKU.PopupSetFormField = "";
            this.UNIT_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UNIT_NAME_RYAKU.PopupWindowName = "";
            this.UNIT_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNIT_NAME_RYAKU.popupWindowSetting")));
            this.UNIT_NAME_RYAKU.ReadOnly = true;
            this.UNIT_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNIT_NAME_RYAKU.RegistCheckMethod")));
            this.UNIT_NAME_RYAKU.SetFormField = "";
            this.UNIT_NAME_RYAKU.ShortItemName = "";
            this.UNIT_NAME_RYAKU.Size = new System.Drawing.Size(55, 20);
            this.UNIT_NAME_RYAKU.TabIndex = 13;
            this.UNIT_NAME_RYAKU.TabStop = false;
            this.UNIT_NAME_RYAKU.Tag = "  ";
            // 
            // MEISAI_BIKOU
            // 
            this.MEISAI_BIKOU.BackColor = System.Drawing.SystemColors.Window;
            this.MEISAI_BIKOU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MEISAI_BIKOU.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.MEISAI_BIKOU.DBFieldsName = "DENPYOU_BIKOU";
            this.MEISAI_BIKOU.DefaultBackColor = System.Drawing.Color.Empty;
            this.MEISAI_BIKOU.DisplayItemName = "伝票備考";
            this.MEISAI_BIKOU.DisplayPopUp = null;
            this.MEISAI_BIKOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MEISAI_BIKOU.FocusOutCheckMethod")));
            this.MEISAI_BIKOU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.MEISAI_BIKOU.ForeColor = System.Drawing.Color.Black;
            this.MEISAI_BIKOU.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.MEISAI_BIKOU.IsInputErrorOccured = false;
            this.MEISAI_BIKOU.ItemDefinedTypes = "varchar";
            this.MEISAI_BIKOU.Location = new System.Drawing.Point(105, 275);
            this.MEISAI_BIKOU.MaxLength = 40;
            this.MEISAI_BIKOU.Name = "MEISAI_BIKOU";
            this.MEISAI_BIKOU.PopupAfterExecute = null;
            this.MEISAI_BIKOU.PopupBeforeExecute = null;
            this.MEISAI_BIKOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MEISAI_BIKOU.PopupSearchSendParams")));
            this.MEISAI_BIKOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MEISAI_BIKOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("MEISAI_BIKOU.popupWindowSetting")));
            this.MEISAI_BIKOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MEISAI_BIKOU.RegistCheckMethod")));
            this.MEISAI_BIKOU.Size = new System.Drawing.Size(285, 20);
            this.MEISAI_BIKOU.TabIndex = 14;
            this.MEISAI_BIKOU.Tag = "全角20文字/半角40文字以内で入力してください";
            // 
            // MEISAI_BIKOU_LABEL
            // 
            this.MEISAI_BIKOU_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.MEISAI_BIKOU_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MEISAI_BIKOU_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MEISAI_BIKOU_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.MEISAI_BIKOU_LABEL.ForeColor = System.Drawing.Color.White;
            this.MEISAI_BIKOU_LABEL.Location = new System.Drawing.Point(12, 275);
            this.MEISAI_BIKOU_LABEL.Name = "MEISAI_BIKOU_LABEL";
            this.MEISAI_BIKOU_LABEL.Size = new System.Drawing.Size(93, 20);
            this.MEISAI_BIKOU_LABEL.TabIndex = 874;
            this.MEISAI_BIKOU_LABEL.Text = "明細備考";
            this.MEISAI_BIKOU_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DENPYOU_KBN_CD
            // 
            this.DENPYOU_KBN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.DENPYOU_KBN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DENPYOU_KBN_CD.DBFieldsName = "DENPYOU_KBN_CD";
            this.DENPYOU_KBN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.DENPYOU_KBN_CD.DisplayItemName = "";
            this.DENPYOU_KBN_CD.DisplayPopUp = null;
            this.DENPYOU_KBN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_KBN_CD.FocusOutCheckMethod")));
            this.DENPYOU_KBN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.DENPYOU_KBN_CD.ForeColor = System.Drawing.Color.Black;
            this.DENPYOU_KBN_CD.FormatSetting = "カスタム";
            this.DENPYOU_KBN_CD.GetCodeMasterField = "";
            this.DENPYOU_KBN_CD.IsInputErrorOccured = false;
            this.DENPYOU_KBN_CD.ItemDefinedTypes = "smallint";
            this.DENPYOU_KBN_CD.Location = new System.Drawing.Point(159, 197);
            this.DENPYOU_KBN_CD.Name = "DENPYOU_KBN_CD";
            this.DENPYOU_KBN_CD.PopupAfterExecute = null;
            this.DENPYOU_KBN_CD.PopupBeforeExecute = null;
            this.DENPYOU_KBN_CD.PopupGetMasterField = "DENPYOU_KBN_CD, DENPYOU_KBN_NAME_RYAKU";
            this.DENPYOU_KBN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DENPYOU_KBN_CD.PopupSearchSendParams")));
            this.DENPYOU_KBN_CD.PopupSetFormField = "DENPYOU_KBN_CD, DENPYOU_KBN_NAME";
            this.DENPYOU_KBN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_DENPYOU_KBN;
            this.DENPYOU_KBN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.DENPYOU_KBN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DENPYOU_KBN_CD.popupWindowSetting")));
            rangeSettingDto6.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.DENPYOU_KBN_CD.RangeSetting = rangeSettingDto6;
            this.DENPYOU_KBN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_KBN_CD.RegistCheckMethod")));
            this.DENPYOU_KBN_CD.SetFormField = "";
            this.DENPYOU_KBN_CD.Size = new System.Drawing.Size(10, 20);
            this.DENPYOU_KBN_CD.TabIndex = 10;
            this.DENPYOU_KBN_CD.Tag = "";
            this.DENPYOU_KBN_CD.Visible = false;
            this.DENPYOU_KBN_CD.WordWrap = false;
            // 
            // HINMEI_CD_SHUKKA
            // 
            this.HINMEI_CD_SHUKKA.BackColor = System.Drawing.SystemColors.Window;
            this.HINMEI_CD_SHUKKA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HINMEI_CD_SHUKKA.ChangeUpperCase = true;
            this.HINMEI_CD_SHUKKA.CharacterLimitList = null;
            this.HINMEI_CD_SHUKKA.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.HINMEI_CD_SHUKKA.DBFieldsName = "HINMEI_CD";
            this.HINMEI_CD_SHUKKA.DefaultBackColor = System.Drawing.Color.Empty;
            this.HINMEI_CD_SHUKKA.DisplayItemName = "CD";
            this.HINMEI_CD_SHUKKA.DisplayPopUp = null;
            this.HINMEI_CD_SHUKKA.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_CD_SHUKKA.FocusOutCheckMethod")));
            this.HINMEI_CD_SHUKKA.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HINMEI_CD_SHUKKA.ForeColor = System.Drawing.Color.Black;
            this.HINMEI_CD_SHUKKA.GetCodeMasterField = "HINMEI_CD,HINMEI_NAME_RYAKU";
            this.HINMEI_CD_SHUKKA.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.HINMEI_CD_SHUKKA.IsInputErrorOccured = false;
            this.HINMEI_CD_SHUKKA.ItemDefinedTypes = "varchar";
            this.HINMEI_CD_SHUKKA.Location = new System.Drawing.Point(245, 100);
            this.HINMEI_CD_SHUKKA.MaxLength = 6;
            this.HINMEI_CD_SHUKKA.Name = "HINMEI_CD_SHUKKA";
            this.HINMEI_CD_SHUKKA.PopupAfterExecute = null;
            this.HINMEI_CD_SHUKKA.PopupBeforeExecute = null;
            this.HINMEI_CD_SHUKKA.PopupGetMasterField = "HINMEI_CD,HINMEI_NAME_RYAKU";
            this.HINMEI_CD_SHUKKA.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HINMEI_CD_SHUKKA.PopupSearchSendParams")));
            this.HINMEI_CD_SHUKKA.PopupSetFormField = "HINMEI_CD_SHUUKKA,HINMEI_NAME_SHUUKKA";
            this.HINMEI_CD_SHUKKA.PopupWindowId = r_framework.Const.WINDOW_ID.M_HINMEI;
            this.HINMEI_CD_SHUKKA.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.HINMEI_CD_SHUKKA.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HINMEI_CD_SHUKKA.popupWindowSetting")));
            this.HINMEI_CD_SHUKKA.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_CD_SHUKKA.RegistCheckMethod")));
            this.HINMEI_CD_SHUKKA.SetFormField = "HINMEI_CD_SHUUKKA,HINMEI_NAME_SHUUKKA";
            this.HINMEI_CD_SHUKKA.ShortItemName = "品名CD";
            this.HINMEI_CD_SHUKKA.Size = new System.Drawing.Size(55, 20);
            this.HINMEI_CD_SHUKKA.TabIndex = 3;
            this.HINMEI_CD_SHUKKA.Tag = "品名を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.HINMEI_CD_SHUKKA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.HINMEI_CD_SHUKKA.Visible = false;
            this.HINMEI_CD_SHUKKA.ZeroPaddengFlag = true;
            this.HINMEI_CD_SHUKKA.Validating += new System.ComponentModel.CancelEventHandler(this.HINMEI_CD_SHUKKA_Validating);
            // 
            // HINMEI_NAME_SHUKKA
            // 
            this.HINMEI_NAME_SHUKKA.BackColor = System.Drawing.SystemColors.Window;
            this.HINMEI_NAME_SHUKKA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HINMEI_NAME_SHUKKA.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.HINMEI_NAME_SHUKKA.DBFieldsName = "";
            this.HINMEI_NAME_SHUKKA.DefaultBackColor = System.Drawing.Color.Empty;
            this.HINMEI_NAME_SHUKKA.DisplayItemName = "品名略称名";
            this.HINMEI_NAME_SHUKKA.DisplayPopUp = null;
            this.HINMEI_NAME_SHUKKA.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_NAME_SHUKKA.FocusOutCheckMethod")));
            this.HINMEI_NAME_SHUKKA.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HINMEI_NAME_SHUKKA.ForeColor = System.Drawing.Color.Black;
            this.HINMEI_NAME_SHUKKA.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.HINMEI_NAME_SHUKKA.IsInputErrorOccured = false;
            this.HINMEI_NAME_SHUKKA.ItemDefinedTypes = "";
            this.HINMEI_NAME_SHUKKA.Location = new System.Drawing.Point(299, 100);
            this.HINMEI_NAME_SHUKKA.MaxLength = 40;
            this.HINMEI_NAME_SHUKKA.Name = "HINMEI_NAME_SHUKKA";
            this.HINMEI_NAME_SHUKKA.PopupAfterExecute = null;
            this.HINMEI_NAME_SHUKKA.PopupBeforeExecute = null;
            this.HINMEI_NAME_SHUKKA.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HINMEI_NAME_SHUKKA.PopupSearchSendParams")));
            this.HINMEI_NAME_SHUKKA.PopupSetFormField = "";
            this.HINMEI_NAME_SHUKKA.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HINMEI_NAME_SHUKKA.PopupWindowName = "";
            this.HINMEI_NAME_SHUKKA.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HINMEI_NAME_SHUKKA.popupWindowSetting")));
            this.HINMEI_NAME_SHUKKA.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_NAME_SHUKKA.RegistCheckMethod")));
            this.HINMEI_NAME_SHUKKA.SetFormField = "";
            this.HINMEI_NAME_SHUKKA.ShortItemName = "品名略称名";
            this.HINMEI_NAME_SHUKKA.Size = new System.Drawing.Size(200, 20);
            this.HINMEI_NAME_SHUKKA.TabIndex = 4;
            this.HINMEI_NAME_SHUKKA.Tag = "  ";
            this.HINMEI_NAME_SHUKKA.Visible = false;
            // 
            // HINMEI_CD_URSH
            // 
            this.HINMEI_CD_URSH.BackColor = System.Drawing.SystemColors.Window;
            this.HINMEI_CD_URSH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HINMEI_CD_URSH.ChangeUpperCase = true;
            this.HINMEI_CD_URSH.CharacterLimitList = null;
            this.HINMEI_CD_URSH.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.HINMEI_CD_URSH.DBFieldsName = "HINMEI_CD";
            this.HINMEI_CD_URSH.DefaultBackColor = System.Drawing.Color.Empty;
            this.HINMEI_CD_URSH.DisplayItemName = "CD";
            this.HINMEI_CD_URSH.DisplayPopUp = null;
            this.HINMEI_CD_URSH.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_CD_URSH.FocusOutCheckMethod")));
            this.HINMEI_CD_URSH.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HINMEI_CD_URSH.ForeColor = System.Drawing.Color.Black;
            this.HINMEI_CD_URSH.GetCodeMasterField = "HINMEI_CD,HINMEI_NAME_RYAKU";
            this.HINMEI_CD_URSH.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.HINMEI_CD_URSH.IsInputErrorOccured = false;
            this.HINMEI_CD_URSH.ItemDefinedTypes = "varchar";
            this.HINMEI_CD_URSH.Location = new System.Drawing.Point(245, 126);
            this.HINMEI_CD_URSH.MaxLength = 6;
            this.HINMEI_CD_URSH.Name = "HINMEI_CD_URSH";
            this.HINMEI_CD_URSH.PopupAfterExecute = null;
            this.HINMEI_CD_URSH.PopupBeforeExecute = null;
            this.HINMEI_CD_URSH.PopupGetMasterField = "HINMEI_CD,HINMEI_NAME_RYAKU";
            this.HINMEI_CD_URSH.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HINMEI_CD_URSH.PopupSearchSendParams")));
            this.HINMEI_CD_URSH.PopupSetFormField = "HINMEI_CD_URSH,HINMEI_NAME_URSH";
            this.HINMEI_CD_URSH.PopupWindowId = r_framework.Const.WINDOW_ID.M_HINMEI;
            this.HINMEI_CD_URSH.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.HINMEI_CD_URSH.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HINMEI_CD_URSH.popupWindowSetting")));
            this.HINMEI_CD_URSH.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_CD_URSH.RegistCheckMethod")));
            this.HINMEI_CD_URSH.SetFormField = "HINMEI_CD_URSH,HINMEI_NAME_URSH";
            this.HINMEI_CD_URSH.ShortItemName = "品名CD";
            this.HINMEI_CD_URSH.Size = new System.Drawing.Size(55, 20);
            this.HINMEI_CD_URSH.TabIndex = 5;
            this.HINMEI_CD_URSH.Tag = "品名を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.HINMEI_CD_URSH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.HINMEI_CD_URSH.Visible = false;
            this.HINMEI_CD_URSH.ZeroPaddengFlag = true;
            this.HINMEI_CD_URSH.Validating += new System.ComponentModel.CancelEventHandler(this.HINMEI_CD_URSH_Validating);
            // 
            // HINMEI_NAME_URSH
            // 
            this.HINMEI_NAME_URSH.BackColor = System.Drawing.SystemColors.Window;
            this.HINMEI_NAME_URSH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HINMEI_NAME_URSH.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.HINMEI_NAME_URSH.DBFieldsName = "";
            this.HINMEI_NAME_URSH.DefaultBackColor = System.Drawing.Color.Empty;
            this.HINMEI_NAME_URSH.DisplayItemName = "品名略称名";
            this.HINMEI_NAME_URSH.DisplayPopUp = null;
            this.HINMEI_NAME_URSH.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_NAME_URSH.FocusOutCheckMethod")));
            this.HINMEI_NAME_URSH.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HINMEI_NAME_URSH.ForeColor = System.Drawing.Color.Black;
            this.HINMEI_NAME_URSH.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.HINMEI_NAME_URSH.IsInputErrorOccured = false;
            this.HINMEI_NAME_URSH.ItemDefinedTypes = "";
            this.HINMEI_NAME_URSH.Location = new System.Drawing.Point(299, 126);
            this.HINMEI_NAME_URSH.MaxLength = 40;
            this.HINMEI_NAME_URSH.Name = "HINMEI_NAME_URSH";
            this.HINMEI_NAME_URSH.PopupAfterExecute = null;
            this.HINMEI_NAME_URSH.PopupBeforeExecute = null;
            this.HINMEI_NAME_URSH.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HINMEI_NAME_URSH.PopupSearchSendParams")));
            this.HINMEI_NAME_URSH.PopupSetFormField = "";
            this.HINMEI_NAME_URSH.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HINMEI_NAME_URSH.PopupWindowName = "";
            this.HINMEI_NAME_URSH.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HINMEI_NAME_URSH.popupWindowSetting")));
            this.HINMEI_NAME_URSH.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_NAME_URSH.RegistCheckMethod")));
            this.HINMEI_NAME_URSH.SetFormField = "";
            this.HINMEI_NAME_URSH.ShortItemName = "品名略称名";
            this.HINMEI_NAME_URSH.Size = new System.Drawing.Size(200, 20);
            this.HINMEI_NAME_URSH.TabIndex = 6;
            this.HINMEI_NAME_URSH.Tag = "  ";
            this.HINMEI_NAME_URSH.Visible = false;
            // 
            // DenpyouiTankakkatsuPopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(601, 360);
            this.Controls.Add(this.HINMEI_CD_URSH);
            this.Controls.Add(this.HINMEI_NAME_URSH);
            this.Controls.Add(this.HINMEI_CD_SHUKKA);
            this.Controls.Add(this.HINMEI_NAME_SHUKKA);
            this.Controls.Add(this.DENPYOU_KBN_CD);
            this.Controls.Add(this.MEISAI_BIKOU);
            this.Controls.Add(this.MEISAI_BIKOU_LABEL);
            this.Controls.Add(this.UNIT_NAME_RYAKU);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.UNIT_CD);
            this.Controls.Add(this.suryouLabel);
            this.Controls.Add(this.SUURYOU);
            this.Controls.Add(this.DENPYOU_KBN_NAME);
            this.Controls.Add(this.denpyoukbnLabel);
            this.Controls.Add(this.HINMEI_CD_UKEIRE);
            this.Controls.Add(this.HINMEI_NAME_UKEIRE);
            this.Controls.Add(this.hinmeiLabel);
            this.Controls.Add(this.TANNKA_ZOUGENN);
            this.Controls.Add(this.tankaZougennLabel);
            this.Controls.Add(this.KINNGAKU);
            this.Controls.Add(this.kinngakuLabel);
            this.Controls.Add(this.TANNKA);
            this.Controls.Add(this.tannkaLabel);
            this.Controls.Add(this.bt_func8);
            this.Controls.Add(this.bt_func12);
            this.Controls.Add(this.bt_func11);
            this.Controls.Add(this.DENPYOU_JYOUHOU_LABLE);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DenpyouiTankakkatsuPopupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "伝票明細　一括入力";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label DENPYOU_JYOUHOU_LABLE;
        public r_framework.CustomControl.CustomButton bt_func12;
        public r_framework.CustomControl.CustomButton bt_func11;
        public r_framework.CustomControl.CustomButton bt_func8;
        internal r_framework.CustomControl.CustomNumericTextBox2 TANNKA;
        internal System.Windows.Forms.Label tannkaLabel;
        internal r_framework.CustomControl.CustomNumericTextBox2 KINNGAKU;
        internal System.Windows.Forms.Label kinngakuLabel;
        internal r_framework.CustomControl.CustomNumericTextBox2 TANNKA_ZOUGENN;
        internal System.Windows.Forms.Label tankaZougennLabel;
        internal System.Windows.Forms.Label hinmeiLabel;
        internal r_framework.CustomControl.CustomAlphaNumTextBox HINMEI_CD_UKEIRE;
        internal r_framework.CustomControl.CustomTextBox HINMEI_NAME_UKEIRE;
        internal System.Windows.Forms.Label denpyoukbnLabel;
        internal r_framework.CustomControl.CustomTextBox DENPYOU_KBN_NAME;
        internal r_framework.CustomControl.CustomNumericTextBox2 SUURYOU;
        internal System.Windows.Forms.Label suryouLabel;
        internal r_framework.CustomControl.CustomNumericTextBox2 UNIT_CD;
        internal System.Windows.Forms.Label label2;
        internal r_framework.CustomControl.CustomTextBox UNIT_NAME_RYAKU;
        internal r_framework.CustomControl.CustomTextBox MEISAI_BIKOU;
        internal System.Windows.Forms.Label MEISAI_BIKOU_LABEL;
        internal r_framework.CustomControl.CustomNumericTextBox2 DENPYOU_KBN_CD;
        internal r_framework.CustomControl.CustomAlphaNumTextBox HINMEI_CD_SHUKKA;
        internal r_framework.CustomControl.CustomTextBox HINMEI_NAME_SHUKKA;
        internal r_framework.CustomControl.CustomAlphaNumTextBox HINMEI_CD_URSH;
        internal r_framework.CustomControl.CustomTextBox HINMEI_NAME_URSH;
    }
}