namespace Shougun.Core.PaperManifest.ManifestoJissekiIchiran
{
    partial class UIHeader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIHeader));
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto5 = new r_framework.Dto.RangeSettingDto();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.AlertNumber = new r_framework.CustomControl.CustomNumericTextBox2();
            this.ReadDataNumber = new r_framework.CustomControl.CustomNumericTextBox2();
            this.KYOTEN_NAME = new r_framework.CustomControl.CustomTextBox();
            this.KYOTEN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label4 = new System.Windows.Forms.Label();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.HaikiKbn_1 = new r_framework.CustomControl.CustomCheckBox();
            this.HaikiKbn_2 = new r_framework.CustomControl.CustomCheckBox();
            this.HaikiKbn_3 = new r_framework.CustomControl.CustomCheckBox();
            this.HaikiKbn_4 = new r_framework.CustomControl.CustomCheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.MANI_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.radbtn_All = new r_framework.CustomControl.CustomRadioButton();
            this.radbtn_Ni = new r_framework.CustomControl.CustomRadioButton();
            this.radbtn_Ichi = new r_framework.CustomControl.CustomRadioButton();
            this.customPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Visible = false;
            // 
            // lb_title
            // 
            this.lb_title.Size = new System.Drawing.Size(306, 34);
            this.lb_title.Text = "マニフェスト実績一覧";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(973, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 530;
            this.label2.Text = "アラート件数";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(973, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 528;
            this.label1.Text = "読込データ件数";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AlertNumber
            // 
            this.AlertNumber.BackColor = System.Drawing.SystemColors.Window;
            this.AlertNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AlertNumber.CustomFormatSetting = "#,##0";
            this.AlertNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.AlertNumber.DisplayPopUp = null;
            this.AlertNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("AlertNumber.FocusOutCheckMethod")));
            this.AlertNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.AlertNumber.ForeColor = System.Drawing.Color.Black;
            this.AlertNumber.FormatSetting = "カスタム";
            this.AlertNumber.IsInputErrorOccured = false;
            this.AlertNumber.Location = new System.Drawing.Point(1088, 2);
            this.AlertNumber.Name = "AlertNumber";
            this.AlertNumber.PopupAfterExecute = null;
            this.AlertNumber.PopupBeforeExecute = null;
            this.AlertNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("AlertNumber.PopupSearchSendParams")));
            this.AlertNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.AlertNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("AlertNumber.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.AlertNumber.RangeSetting = rangeSettingDto2;
            this.AlertNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("AlertNumber.RegistCheckMethod")));
            this.AlertNumber.Size = new System.Drawing.Size(80, 20);
            this.AlertNumber.TabIndex = 3;
            this.AlertNumber.TabStop = false;
            this.AlertNumber.Tag = "検索結果の総件数でアラートメッセージを表示させたい上限数を入力してください";
            this.AlertNumber.Text = "0";
            this.AlertNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.AlertNumber.WordWrap = false;
            this.AlertNumber.TextChanged += new System.EventHandler(this.NUMBER_ALERT_TextChanged);
            this.AlertNumber.Validating += new System.ComponentModel.CancelEventHandler(this.AlertNumber_Validating);
            this.AlertNumber.Validated += new System.EventHandler(this.AlertNumber_Validated);
            // 
            // ReadDataNumber
            // 
            this.ReadDataNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ReadDataNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ReadDataNumber.CustomFormatSetting = "#,##0";
            this.ReadDataNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.ReadDataNumber.DisplayPopUp = null;
            this.ReadDataNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ReadDataNumber.FocusOutCheckMethod")));
            this.ReadDataNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ReadDataNumber.ForeColor = System.Drawing.Color.Black;
            this.ReadDataNumber.FormatSetting = "カスタム";
            this.ReadDataNumber.IsInputErrorOccured = false;
            this.ReadDataNumber.Location = new System.Drawing.Point(1088, 24);
            this.ReadDataNumber.Name = "ReadDataNumber";
            this.ReadDataNumber.PopupAfterExecute = null;
            this.ReadDataNumber.PopupBeforeExecute = null;
            this.ReadDataNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ReadDataNumber.PopupSearchSendParams")));
            this.ReadDataNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ReadDataNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ReadDataNumber.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.ReadDataNumber.RangeSetting = rangeSettingDto3;
            this.ReadDataNumber.ReadOnly = true;
            this.ReadDataNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ReadDataNumber.RegistCheckMethod")));
            this.ReadDataNumber.Size = new System.Drawing.Size(80, 20);
            this.ReadDataNumber.TabIndex = 532;
            this.ReadDataNumber.TabStop = false;
            this.ReadDataNumber.Tag = "検索結果の総件数が表示されます";
            this.ReadDataNumber.Text = "0";
            this.ReadDataNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ReadDataNumber.WordWrap = false;
            // 
            // KYOTEN_NAME
            // 
            this.KYOTEN_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KYOTEN_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_NAME.DisplayPopUp = null;
            this.KYOTEN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME.FocusOutCheckMethod")));
            this.KYOTEN_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_NAME.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_NAME.IsInputErrorOccured = false;
            this.KYOTEN_NAME.Location = new System.Drawing.Point(538, 2);
            this.KYOTEN_NAME.Name = "KYOTEN_NAME";
            this.KYOTEN_NAME.PopupAfterExecute = null;
            this.KYOTEN_NAME.PopupBeforeExecute = null;
            this.KYOTEN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_NAME.PopupSearchSendParams")));
            this.KYOTEN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOTEN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_NAME.popupWindowSetting")));
            this.KYOTEN_NAME.ReadOnly = true;
            this.KYOTEN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME.RegistCheckMethod")));
            this.KYOTEN_NAME.Size = new System.Drawing.Size(160, 20);
            this.KYOTEN_NAME.TabIndex = 537;
            this.KYOTEN_NAME.TabStop = false;
            // 
            // KYOTEN_CD
            // 
            this.KYOTEN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.KYOTEN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_CD.CustomFormatSetting = "00";
            this.KYOTEN_CD.DBFieldsName = "KYOTEN_CD";
            this.KYOTEN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_CD.DisplayItemName = "拠点";
            this.KYOTEN_CD.DisplayPopUp = null;
            this.KYOTEN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.FocusOutCheckMethod")));
            this.KYOTEN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_CD.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_CD.FormatSetting = "カスタム";
            this.KYOTEN_CD.GetCodeMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.IsInputErrorOccured = false;
            this.KYOTEN_CD.ItemDefinedTypes = "varchar";
            this.KYOTEN_CD.Location = new System.Drawing.Point(509, 2);
            this.KYOTEN_CD.Name = "KYOTEN_CD";
            this.KYOTEN_CD.PopupAfterExecute = null;
            this.KYOTEN_CD.PopupBeforeExecute = null;
            this.KYOTEN_CD.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_CD.PopupSearchSendParams")));
            this.KYOTEN_CD.PopupSetFormField = "KYOTEN_CD,KYOTEN_NAME";
            this.KYOTEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.KYOTEN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.KYOTEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_CD.popupWindowSetting")));
            rangeSettingDto4.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.KYOTEN_CD.RangeSetting = rangeSettingDto4;
            this.KYOTEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.RegistCheckMethod")));
            this.KYOTEN_CD.SetFormField = "KYOTEN_CD,KYOTEN_NAME";
            this.KYOTEN_CD.ShortItemName = "拠点CD";
            this.KYOTEN_CD.Size = new System.Drawing.Size(30, 20);
            this.KYOTEN_CD.TabIndex = 1;
            this.KYOTEN_CD.Tag = "半角2桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.KYOTEN_CD.WordWrap = false;
            this.KYOTEN_CD.Validating += new System.ComponentModel.CancelEventHandler(this.KYOTEN_CD_Validating);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(394, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 535;
            this.label4.Text = "拠点";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(704, 2);
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
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 676;
            this.ISNOT_NEED_DELETE_FLG.TabStop = false;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(394, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.TabIndex = 677;
            this.label3.Text = "廃棄物区分";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HaikiKbn_1
            // 
            this.HaikiKbn_1.AutoSize = true;
            this.HaikiKbn_1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.HaikiKbn_1.DBFieldsName = "";
            this.HaikiKbn_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.HaikiKbn_1.DisplayItemName = "廃棄物区分";
            this.HaikiKbn_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HaikiKbn_1.FocusOutCheckMethod")));
            this.HaikiKbn_1.ItemDefinedTypes = "bit";
            this.HaikiKbn_1.Location = new System.Drawing.Point(509, 27);
            this.HaikiKbn_1.Name = "HaikiKbn_1";
            this.HaikiKbn_1.PopupAfterExecute = null;
            this.HaikiKbn_1.PopupBeforeExecute = null;
            this.HaikiKbn_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HaikiKbn_1.PopupSearchSendParams")));
            this.HaikiKbn_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HaikiKbn_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HaikiKbn_1.popupWindowSetting")));
            this.HaikiKbn_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HaikiKbn_1.RegistCheckMethod")));
            this.HaikiKbn_1.ShortItemName = "廃棄物区分";
            this.HaikiKbn_1.Size = new System.Drawing.Size(110, 17);
            this.HaikiKbn_1.TabIndex = 4;
            this.HaikiKbn_1.Tag = " ";
            this.HaikiKbn_1.Text = "産廃（直行）";
            this.HaikiKbn_1.UseVisualStyleBackColor = false;
            this.HaikiKbn_1.CheckedChanged += new System.EventHandler(this.HaikiKbn_CheckedChanged);
            // 
            // HaikiKbn_2
            // 
            this.HaikiKbn_2.AutoSize = true;
            this.HaikiKbn_2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.HaikiKbn_2.DBFieldsName = "";
            this.HaikiKbn_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.HaikiKbn_2.DisplayItemName = "廃棄物区分";
            this.HaikiKbn_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HaikiKbn_2.FocusOutCheckMethod")));
            this.HaikiKbn_2.ItemDefinedTypes = "bit";
            this.HaikiKbn_2.Location = new System.Drawing.Point(619, 27);
            this.HaikiKbn_2.Name = "HaikiKbn_2";
            this.HaikiKbn_2.PopupAfterExecute = null;
            this.HaikiKbn_2.PopupBeforeExecute = null;
            this.HaikiKbn_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HaikiKbn_2.PopupSearchSendParams")));
            this.HaikiKbn_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HaikiKbn_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HaikiKbn_2.popupWindowSetting")));
            this.HaikiKbn_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HaikiKbn_2.RegistCheckMethod")));
            this.HaikiKbn_2.ShortItemName = "廃棄物区分";
            this.HaikiKbn_2.Size = new System.Drawing.Size(110, 17);
            this.HaikiKbn_2.TabIndex = 5;
            this.HaikiKbn_2.Tag = " ";
            this.HaikiKbn_2.Text = "産廃（積替）";
            this.HaikiKbn_2.UseVisualStyleBackColor = false;
            this.HaikiKbn_2.CheckedChanged += new System.EventHandler(this.HaikiKbn_CheckedChanged);
            // 
            // HaikiKbn_3
            // 
            this.HaikiKbn_3.AutoSize = true;
            this.HaikiKbn_3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.HaikiKbn_3.DBFieldsName = "";
            this.HaikiKbn_3.DefaultBackColor = System.Drawing.Color.Empty;
            this.HaikiKbn_3.DisplayItemName = "廃棄物区分";
            this.HaikiKbn_3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HaikiKbn_3.FocusOutCheckMethod")));
            this.HaikiKbn_3.ItemDefinedTypes = "bit";
            this.HaikiKbn_3.Location = new System.Drawing.Point(729, 27);
            this.HaikiKbn_3.Name = "HaikiKbn_3";
            this.HaikiKbn_3.PopupAfterExecute = null;
            this.HaikiKbn_3.PopupBeforeExecute = null;
            this.HaikiKbn_3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HaikiKbn_3.PopupSearchSendParams")));
            this.HaikiKbn_3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HaikiKbn_3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HaikiKbn_3.popupWindowSetting")));
            this.HaikiKbn_3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HaikiKbn_3.RegistCheckMethod")));
            this.HaikiKbn_3.ShortItemName = "廃棄物区分";
            this.HaikiKbn_3.Size = new System.Drawing.Size(54, 17);
            this.HaikiKbn_3.TabIndex = 6;
            this.HaikiKbn_3.Tag = " ";
            this.HaikiKbn_3.Text = "建廃";
            this.HaikiKbn_3.UseVisualStyleBackColor = false;
            this.HaikiKbn_3.CheckedChanged += new System.EventHandler(this.HaikiKbn_CheckedChanged);
            // 
            // HaikiKbn_4
            // 
            this.HaikiKbn_4.AutoSize = true;
            this.HaikiKbn_4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.HaikiKbn_4.DBFieldsName = "";
            this.HaikiKbn_4.DefaultBackColor = System.Drawing.Color.Empty;
            this.HaikiKbn_4.DisplayItemName = "廃棄物区分";
            this.HaikiKbn_4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HaikiKbn_4.FocusOutCheckMethod")));
            this.HaikiKbn_4.ItemDefinedTypes = "bit";
            this.HaikiKbn_4.Location = new System.Drawing.Point(783, 27);
            this.HaikiKbn_4.Name = "HaikiKbn_4";
            this.HaikiKbn_4.PopupAfterExecute = null;
            this.HaikiKbn_4.PopupBeforeExecute = null;
            this.HaikiKbn_4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HaikiKbn_4.PopupSearchSendParams")));
            this.HaikiKbn_4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HaikiKbn_4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HaikiKbn_4.popupWindowSetting")));
            this.HaikiKbn_4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HaikiKbn_4.RegistCheckMethod")));
            this.HaikiKbn_4.ShortItemName = "廃棄物区分";
            this.HaikiKbn_4.Size = new System.Drawing.Size(54, 17);
            this.HaikiKbn_4.TabIndex = 7;
            this.HaikiKbn_4.Tag = " ";
            this.HaikiKbn_4.Text = "電子";
            this.HaikiKbn_4.UseVisualStyleBackColor = false;
            this.HaikiKbn_4.CheckedChanged += new System.EventHandler(this.HaikiKbn_CheckedChanged);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(394, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 20);
            this.label5.TabIndex = 683;
            this.label5.Text = "一次二次区分";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MANI_KBN
            // 
            this.MANI_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.MANI_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MANI_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.MANI_KBN.DisplayItemName = "一次二次区分";
            this.MANI_KBN.DisplayPopUp = null;
            this.MANI_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MANI_KBN.FocusOutCheckMethod")));
            this.MANI_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.MANI_KBN.ForeColor = System.Drawing.Color.Black;
            this.MANI_KBN.IsInputErrorOccured = false;
            this.MANI_KBN.LinkedRadioButtonArray = new string[] {
        "radbtn_Ichi",
        "radbtn_Ni",
        "radbtn_All"};
            this.MANI_KBN.Location = new System.Drawing.Point(509, 48);
            this.MANI_KBN.Name = "MANI_KBN";
            this.MANI_KBN.PopupAfterExecute = null;
            this.MANI_KBN.PopupBeforeExecute = null;
            this.MANI_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MANI_KBN.PopupSearchSendParams")));
            this.MANI_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MANI_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("MANI_KBN.popupWindowSetting")));
            rangeSettingDto5.Max = new decimal(new int[] {
            3,
            0,
            0,
            0});
            rangeSettingDto5.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MANI_KBN.RangeSetting = rangeSettingDto5;
            this.MANI_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MANI_KBN.RegistCheckMethod")));
            this.MANI_KBN.Size = new System.Drawing.Size(20, 20);
            this.MANI_KBN.TabIndex = 9;
            this.MANI_KBN.Tag = "【1～3】のいずれかで入力してください";
            this.MANI_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MANI_KBN.WordWrap = false;
            // 
            // customPanel1
            // 
            this.customPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel1.Controls.Add(this.radbtn_All);
            this.customPanel1.Controls.Add(this.radbtn_Ni);
            this.customPanel1.Controls.Add(this.radbtn_Ichi);
            this.customPanel1.Location = new System.Drawing.Point(528, 48);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(298, 20);
            this.customPanel1.TabIndex = 685;
            // 
            // radbtn_All
            // 
            this.radbtn_All.AutoSize = true;
            this.radbtn_All.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_All.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_All.FocusOutCheckMethod")));
            this.radbtn_All.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_All.LinkedTextBox = "MANI_KBN";
            this.radbtn_All.Location = new System.Drawing.Point(192, 1);
            this.radbtn_All.Name = "radbtn_All";
            this.radbtn_All.PopupAfterExecute = null;
            this.radbtn_All.PopupBeforeExecute = null;
            this.radbtn_All.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_All.PopupSearchSendParams")));
            this.radbtn_All.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_All.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_All.popupWindowSetting")));
            this.radbtn_All.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_All.RegistCheckMethod")));
            this.radbtn_All.Size = new System.Drawing.Size(67, 17);
            this.radbtn_All.TabIndex = 305;
            this.radbtn_All.Tag = " ";
            this.radbtn_All.Text = "3.全て";
            this.radbtn_All.UseVisualStyleBackColor = true;
            this.radbtn_All.Value = "3";
            // 
            // radbtn_Ni
            // 
            this.radbtn_Ni.AutoSize = true;
            this.radbtn_Ni.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Ni.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Ni.FocusOutCheckMethod")));
            this.radbtn_Ni.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_Ni.LinkedTextBox = "MANI_KBN";
            this.radbtn_Ni.Location = new System.Drawing.Point(98, 1);
            this.radbtn_Ni.Name = "radbtn_Ni";
            this.radbtn_Ni.PopupAfterExecute = null;
            this.radbtn_Ni.PopupBeforeExecute = null;
            this.radbtn_Ni.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Ni.PopupSearchSendParams")));
            this.radbtn_Ni.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Ni.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Ni.popupWindowSetting")));
            this.radbtn_Ni.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Ni.RegistCheckMethod")));
            this.radbtn_Ni.Size = new System.Drawing.Size(67, 17);
            this.radbtn_Ni.TabIndex = 304;
            this.radbtn_Ni.Tag = " ";
            this.radbtn_Ni.Text = "2.二次";
            this.radbtn_Ni.UseVisualStyleBackColor = true;
            this.radbtn_Ni.Value = "2";
            // 
            // radbtn_Ichi
            // 
            this.radbtn_Ichi.AutoSize = true;
            this.radbtn_Ichi.Checked = true;
            this.radbtn_Ichi.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Ichi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Ichi.FocusOutCheckMethod")));
            this.radbtn_Ichi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.radbtn_Ichi.LinkedTextBox = "MANI_KBN";
            this.radbtn_Ichi.Location = new System.Drawing.Point(4, 1);
            this.radbtn_Ichi.Name = "radbtn_Ichi";
            this.radbtn_Ichi.PopupAfterExecute = null;
            this.radbtn_Ichi.PopupBeforeExecute = null;
            this.radbtn_Ichi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Ichi.PopupSearchSendParams")));
            this.radbtn_Ichi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Ichi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Ichi.popupWindowSetting")));
            this.radbtn_Ichi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Ichi.RegistCheckMethod")));
            this.radbtn_Ichi.Size = new System.Drawing.Size(67, 17);
            this.radbtn_Ichi.TabIndex = 303;
            this.radbtn_Ichi.Tag = " ";
            this.radbtn_Ichi.Text = "1.一次";
            this.radbtn_Ichi.UseVisualStyleBackColor = true;
            this.radbtn_Ichi.Value = "1";
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 77);
            this.Controls.Add(this.customPanel1);
            this.Controls.Add(this.MANI_KBN);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.HaikiKbn_4);
            this.Controls.Add(this.HaikiKbn_3);
            this.Controls.Add(this.HaikiKbn_2);
            this.Controls.Add(this.HaikiKbn_1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.KYOTEN_NAME);
            this.Controls.Add(this.KYOTEN_CD);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ReadDataNumber);
            this.Controls.Add(this.AlertNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "UIHeader";
            this.Text = "HeaderSample";
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.AlertNumber, 0);
            this.Controls.SetChildIndex(this.ReadDataNumber, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.KYOTEN_CD, 0);
            this.Controls.SetChildIndex(this.KYOTEN_NAME, 0);
            this.Controls.SetChildIndex(this.ISNOT_NEED_DELETE_FLG, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.HaikiKbn_1, 0);
            this.Controls.SetChildIndex(this.HaikiKbn_2, 0);
            this.Controls.SetChildIndex(this.HaikiKbn_3, 0);
            this.Controls.SetChildIndex(this.HaikiKbn_4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.MANI_KBN, 0);
            this.Controls.SetChildIndex(this.customPanel1, 0);
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomNumericTextBox2 ReadDataNumber;
        internal r_framework.CustomControl.CustomNumericTextBox2 AlertNumber;
        internal r_framework.CustomControl.CustomTextBox KYOTEN_NAME;
        internal r_framework.CustomControl.CustomNumericTextBox2 KYOTEN_CD;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label4;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
        public System.Windows.Forms.Label label3;
        internal r_framework.CustomControl.CustomCheckBox HaikiKbn_1;
        internal r_framework.CustomControl.CustomCheckBox HaikiKbn_2;
        internal r_framework.CustomControl.CustomCheckBox HaikiKbn_3;
        internal r_framework.CustomControl.CustomCheckBox HaikiKbn_4;
        public System.Windows.Forms.Label label5;
        internal r_framework.CustomControl.CustomNumericTextBox2 MANI_KBN;
        private r_framework.CustomControl.CustomPanel customPanel1;
        internal r_framework.CustomControl.CustomRadioButton radbtn_Ichi;
        internal r_framework.CustomControl.CustomRadioButton radbtn_Ni;
        internal r_framework.CustomControl.CustomRadioButton radbtn_All;

    }
}