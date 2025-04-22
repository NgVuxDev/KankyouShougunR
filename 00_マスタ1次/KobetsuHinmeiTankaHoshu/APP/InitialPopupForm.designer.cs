using System.Windows.Forms;
namespace KobetsuHinmeiTankaHoshu.APP
{
    partial class InitialPopupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InitialPopupForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.bt_func12 = new r_framework.CustomControl.CustomButton();
            this.bt_func8 = new r_framework.CustomControl.CustomButton();
            this.GENBA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TORIHIKISAKI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GYOUSHA_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.TORIHIKISAKI_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.GENBA_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pl_SYS_KAKUTEI__TANNI_KBN = new System.Windows.Forms.Panel();
            this.TOROKU_HOUHOU_KBN2 = new r_framework.CustomControl.CustomRadioButton();
            this.TOROKU_HOUHOU_KBN1 = new r_framework.CustomControl.CustomRadioButton();
            this.TOROKU_HOUHOU_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.YUUKOU_END = new r_framework.CustomControl.CustomDateTimePicker();
            this.YUUKOU_BEGIN = new r_framework.CustomControl.CustomDateTimePicker();
            this.TEKIYOU_KBN = new System.Windows.Forms.CheckBox();
            this.pl_SYS_KAKUTEI__TANNI_KBN.SuspendLayout();
            this.SuspendLayout();
            // 
            // bt_func12
            // 
            this.bt_func12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func12.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func12.Enabled = false;
            this.bt_func12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_func12.Location = new System.Drawing.Point(370, 185);
            this.bt_func12.Name = "bt_func12";
            this.bt_func12.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func12.Size = new System.Drawing.Size(80, 35);
            this.bt_func12.TabIndex = 180;
            this.bt_func12.TabStop = false;
            this.bt_func12.Tag = "";
            this.bt_func12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func12.UseVisualStyleBackColor = false;
            // 
            // bt_func8
            // 
            this.bt_func8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func8.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func8.Enabled = false;
            this.bt_func8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_func8.Location = new System.Drawing.Point(285, 185);
            this.bt_func8.Name = "bt_func8";
            this.bt_func8.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func8.Size = new System.Drawing.Size(80, 35);
            this.bt_func8.TabIndex = 140;
            this.bt_func8.TabStop = false;
            this.bt_func8.Tag = "";
            this.bt_func8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func8.UseVisualStyleBackColor = false;
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
            this.GENBA_CD.Location = new System.Drawing.Point(111, 60);
            this.GENBA_CD.MaxLength = 6;
            this.GENBA_CD.Name = "GENBA_CD";
            this.GENBA_CD.PopupAfterExecute = null;
            this.GENBA_CD.PopupAfterExecuteMethod = "PopupAfterGenbaCode";
            this.GENBA_CD.PopupBeforeExecute = null;
            this.GENBA_CD.PopupBeforeExecuteMethod = "PopupBeforeGenbaCode";
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
            this.GENBA_CD.TabIndex = 20;
            this.GENBA_CD.Tag = " ";
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
            this.GYOUSHA_CD.Location = new System.Drawing.Point(111, 38);
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
            this.GYOUSHA_CD.TabIndex = 10;
            this.GYOUSHA_CD.Tag = " ";
            this.GYOUSHA_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.GYOUSHA_CD.ZeroPaddengFlag = true;
            this.GYOUSHA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.GYOUSHA_CD_Validating);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(12, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 20);
            this.label4.TabIndex = 409;
            this.label4.Text = "取引先";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.TORIHIKISAKI_CD.DisplayItemName = "取引先CD";
            this.TORIHIKISAKI_CD.DisplayPopUp = null;
            this.TORIHIKISAKI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.FocusOutCheckMethod")));
            this.TORIHIKISAKI_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TORIHIKISAKI_CD.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_CD.GetCodeMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TORIHIKISAKI_CD.IsInputErrorOccured = false;
            this.TORIHIKISAKI_CD.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_CD.Location = new System.Drawing.Point(111, 16);
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
            this.TORIHIKISAKI_CD.Size = new System.Drawing.Size(55, 20);
            this.TORIHIKISAKI_CD.TabIndex = 1;
            this.TORIHIKISAKI_CD.Tag = " ";
            this.TORIHIKISAKI_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TORIHIKISAKI_CD.ZeroPaddengFlag = true;
            this.TORIHIKISAKI_CD.Validating += new System.ComponentModel.CancelEventHandler(this.TORIHIKISAKI_CD_Validating);
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
            this.GYOUSHA_NAME_RYAKU.Location = new System.Drawing.Point(165, 38);
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
            this.GYOUSHA_NAME_RYAKU.TabIndex = 11;
            this.GYOUSHA_NAME_RYAKU.TabStop = false;
            this.GYOUSHA_NAME_RYAKU.Tag = "  ";
            // 
            // TORIHIKISAKI_NAME_RYAKU
            // 
            this.TORIHIKISAKI_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.TORIHIKISAKI_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.TORIHIKISAKI_NAME_RYAKU.DBFieldsName = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_NAME_RYAKU.DisplayItemName = "取引先略称名";
            this.TORIHIKISAKI_NAME_RYAKU.DisplayPopUp = null;
            this.TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TORIHIKISAKI_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_NAME_RYAKU.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.TORIHIKISAKI_NAME_RYAKU.IsInputErrorOccured = false;
            this.TORIHIKISAKI_NAME_RYAKU.ItemDefinedTypes = "";
            this.TORIHIKISAKI_NAME_RYAKU.Location = new System.Drawing.Point(165, 16);
            this.TORIHIKISAKI_NAME_RYAKU.MaxLength = 40;
            this.TORIHIKISAKI_NAME_RYAKU.Name = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_RYAKU.PopupAfterExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU.PopupBeforeExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams")));
            this.TORIHIKISAKI_NAME_RYAKU.PopupSetFormField = "";
            this.TORIHIKISAKI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_NAME_RYAKU.PopupWindowName = "";
            this.TORIHIKISAKI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.popupWindowSetting")));
            this.TORIHIKISAKI_NAME_RYAKU.ReadOnly = true;
            this.TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU.SetFormField = "";
            this.TORIHIKISAKI_NAME_RYAKU.ShortItemName = "取引先略称名";
            this.TORIHIKISAKI_NAME_RYAKU.Size = new System.Drawing.Size(285, 20);
            this.TORIHIKISAKI_NAME_RYAKU.TabIndex = 2;
            this.TORIHIKISAKI_NAME_RYAKU.TabStop = false;
            this.TORIHIKISAKI_NAME_RYAKU.Tag = "  ";
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
            this.GENBA_NAME_RYAKU.Location = new System.Drawing.Point(165, 60);
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
            this.GENBA_NAME_RYAKU.TabIndex = 21;
            this.GENBA_NAME_RYAKU.TabStop = false;
            this.GENBA_NAME_RYAKU.Tag = "  ";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 20);
            this.label1.TabIndex = 410;
            this.label1.Text = "業者";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 20);
            this.label2.TabIndex = 411;
            this.label2.Text = "現場";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 20);
            this.label3.TabIndex = 412;
            this.label3.Text = "適用開始日";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(12, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 20);
            this.label5.TabIndex = 413;
            this.label5.Text = "適用終了日";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(12, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 20);
            this.label6.TabIndex = 414;
            this.label6.Text = "登録方法";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pl_SYS_KAKUTEI__TANNI_KBN
            // 
            this.pl_SYS_KAKUTEI__TANNI_KBN.Controls.Add(this.TOROKU_HOUHOU_KBN2);
            this.pl_SYS_KAKUTEI__TANNI_KBN.Controls.Add(this.TOROKU_HOUHOU_KBN1);
            this.pl_SYS_KAKUTEI__TANNI_KBN.Location = new System.Drawing.Point(132, 126);
            this.pl_SYS_KAKUTEI__TANNI_KBN.Name = "pl_SYS_KAKUTEI__TANNI_KBN";
            this.pl_SYS_KAKUTEI__TANNI_KBN.Size = new System.Drawing.Size(273, 52);
            this.pl_SYS_KAKUTEI__TANNI_KBN.TabIndex = 415;
            // 
            // TOROKU_HOUHOU_KBN2
            // 
            this.TOROKU_HOUHOU_KBN2.AutoSize = true;
            this.TOROKU_HOUHOU_KBN2.DefaultBackColor = System.Drawing.Color.Empty;
            this.TOROKU_HOUHOU_KBN2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TOROKU_HOUHOU_KBN2.FocusOutCheckMethod")));
            this.TOROKU_HOUHOU_KBN2.LinkedTextBox = "TOROKU_HOUHOU_KBN";
            this.TOROKU_HOUHOU_KBN2.Location = new System.Drawing.Point(3, 24);
            this.TOROKU_HOUHOU_KBN2.Name = "TOROKU_HOUHOU_KBN2";
            this.TOROKU_HOUHOU_KBN2.PopupAfterExecute = null;
            this.TOROKU_HOUHOU_KBN2.PopupBeforeExecute = null;
            this.TOROKU_HOUHOU_KBN2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TOROKU_HOUHOU_KBN2.PopupSearchSendParams")));
            this.TOROKU_HOUHOU_KBN2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TOROKU_HOUHOU_KBN2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TOROKU_HOUHOU_KBN2.popupWindowSetting")));
            this.TOROKU_HOUHOU_KBN2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TOROKU_HOUHOU_KBN2.RegistCheckMethod")));
            this.TOROKU_HOUHOU_KBN2.Size = new System.Drawing.Size(250, 16);
            this.TOROKU_HOUHOU_KBN2.TabIndex = 2;
            this.TOROKU_HOUHOU_KBN2.Tag = " ";
            this.TOROKU_HOUHOU_KBN2.Text = "2.登録済み個別単価がある場合は、登録しない";
            this.TOROKU_HOUHOU_KBN2.UseVisualStyleBackColor = true;
            this.TOROKU_HOUHOU_KBN2.Value = "2";
            // 
            // TOROKU_HOUHOU_KBN1
            // 
            this.TOROKU_HOUHOU_KBN1.AutoSize = true;
            this.TOROKU_HOUHOU_KBN1.DefaultBackColor = System.Drawing.Color.Empty;
            this.TOROKU_HOUHOU_KBN1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TOROKU_HOUHOU_KBN1.FocusOutCheckMethod")));
            this.TOROKU_HOUHOU_KBN1.LinkedTextBox = "TOROKU_HOUHOU_KBN";
            this.TOROKU_HOUHOU_KBN1.Location = new System.Drawing.Point(3, 3);
            this.TOROKU_HOUHOU_KBN1.Name = "TOROKU_HOUHOU_KBN1";
            this.TOROKU_HOUHOU_KBN1.PopupAfterExecute = null;
            this.TOROKU_HOUHOU_KBN1.PopupBeforeExecute = null;
            this.TOROKU_HOUHOU_KBN1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TOROKU_HOUHOU_KBN1.PopupSearchSendParams")));
            this.TOROKU_HOUHOU_KBN1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TOROKU_HOUHOU_KBN1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TOROKU_HOUHOU_KBN1.popupWindowSetting")));
            this.TOROKU_HOUHOU_KBN1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TOROKU_HOUHOU_KBN1.RegistCheckMethod")));
            this.TOROKU_HOUHOU_KBN1.Size = new System.Drawing.Size(192, 16);
            this.TOROKU_HOUHOU_KBN1.TabIndex = 1;
            this.TOROKU_HOUHOU_KBN1.Tag = "";
            this.TOROKU_HOUHOU_KBN1.Text = "1.登録済み個別単価を削除し登録";
            this.TOROKU_HOUHOU_KBN1.UseVisualStyleBackColor = true;
            this.TOROKU_HOUHOU_KBN1.Value = "1";
            // 
            // TOROKU_HOUHOU_KBN
            // 
            this.TOROKU_HOUHOU_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.TOROKU_HOUHOU_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TOROKU_HOUHOU_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.TOROKU_HOUHOU_KBN.DisplayItemName = "登録方法";
            this.TOROKU_HOUHOU_KBN.DisplayPopUp = null;
            this.TOROKU_HOUHOU_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TOROKU_HOUHOU_KBN.FocusOutCheckMethod")));
            this.TOROKU_HOUHOU_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TOROKU_HOUHOU_KBN.ForeColor = System.Drawing.Color.Black;
            this.TOROKU_HOUHOU_KBN.IsInputErrorOccured = false;
            this.TOROKU_HOUHOU_KBN.LinkedRadioButtonArray = new string[] {
        "TOROKU_HOUHOU_KBN1",
        "TOROKU_HOUHOU_KBN2"};
            this.TOROKU_HOUHOU_KBN.Location = new System.Drawing.Point(111, 126);
            this.TOROKU_HOUHOU_KBN.Name = "TOROKU_HOUHOU_KBN";
            this.TOROKU_HOUHOU_KBN.PopupAfterExecute = null;
            this.TOROKU_HOUHOU_KBN.PopupBeforeExecute = null;
            this.TOROKU_HOUHOU_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TOROKU_HOUHOU_KBN.PopupSearchSendParams")));
            this.TOROKU_HOUHOU_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TOROKU_HOUHOU_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TOROKU_HOUHOU_KBN.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto2.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.TOROKU_HOUHOU_KBN.RangeSetting = rangeSettingDto2;
            this.TOROKU_HOUHOU_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TOROKU_HOUHOU_KBN.RegistCheckMethod")));
            this.TOROKU_HOUHOU_KBN.ShortItemName = "登録方法";
            this.TOROKU_HOUHOU_KBN.Size = new System.Drawing.Size(20, 20);
            this.TOROKU_HOUHOU_KBN.TabIndex = 50;
            this.TOROKU_HOUHOU_KBN.Tag = "";
            this.TOROKU_HOUHOU_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TOROKU_HOUHOU_KBN.WordWrap = false;
            // 
            // YUUKOU_END
            // 
            this.YUUKOU_END.BackColor = System.Drawing.SystemColors.Window;
            this.YUUKOU_END.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.YUUKOU_END.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.YUUKOU_END.Checked = false;
            this.YUUKOU_END.CustomFormat = "yyyy/MM/dd ddd";
            this.YUUKOU_END.DateTimeNowYear = "";
            this.YUUKOU_END.DBFieldsName = "YUUKOU_END";
            this.YUUKOU_END.DefaultBackColor = System.Drawing.Color.Empty;
            this.YUUKOU_END.DisplayItemName = "適用終了日";
            this.YUUKOU_END.DisplayPopUp = null;
            this.YUUKOU_END.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("YUUKOU_END.FocusOutCheckMethod")));
            this.YUUKOU_END.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.YUUKOU_END.ForeColor = System.Drawing.Color.Black;
            this.YUUKOU_END.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.YUUKOU_END.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.YUUKOU_END.IsInputErrorOccured = false;
            this.YUUKOU_END.Location = new System.Drawing.Point(111, 104);
            this.YUUKOU_END.MaxLength = 10;
            this.YUUKOU_END.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.YUUKOU_END.Name = "YUUKOU_END";
            this.YUUKOU_END.NullValue = "";
            this.YUUKOU_END.PopupAfterExecute = null;
            this.YUUKOU_END.PopupBeforeExecute = null;
            this.YUUKOU_END.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("YUUKOU_END.PopupSearchSendParams")));
            this.YUUKOU_END.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.YUUKOU_END.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("YUUKOU_END.popupWindowSetting")));
            this.YUUKOU_END.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("YUUKOU_END.RegistCheckMethod")));
            this.YUUKOU_END.ShortItemName = "適用終了日";
            this.YUUKOU_END.Size = new System.Drawing.Size(126, 20);
            this.YUUKOU_END.TabIndex = 40;
            this.YUUKOU_END.Tag = " ";
            this.YUUKOU_END.Text = "2013/09/11(水)";
            this.YUUKOU_END.Value = new System.DateTime(2013, 9, 11, 0, 0, 0, 0);
            this.YUUKOU_END.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.YUUKOU_END_MouseDoubleClick);
            // 
            // YUUKOU_BEGIN
            // 
            this.YUUKOU_BEGIN.BackColor = System.Drawing.SystemColors.Window;
            this.YUUKOU_BEGIN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.YUUKOU_BEGIN.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.YUUKOU_BEGIN.Checked = false;
            this.YUUKOU_BEGIN.CustomFormat = "yyyy/MM/dd ddd";
            this.YUUKOU_BEGIN.DateTimeNowYear = "";
            this.YUUKOU_BEGIN.DBFieldsName = "YUUKOU_BEGIN";
            this.YUUKOU_BEGIN.DefaultBackColor = System.Drawing.Color.Empty;
            this.YUUKOU_BEGIN.DisplayItemName = "適用開始日";
            this.YUUKOU_BEGIN.DisplayPopUp = null;
            this.YUUKOU_BEGIN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("YUUKOU_BEGIN.FocusOutCheckMethod")));
            this.YUUKOU_BEGIN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.YUUKOU_BEGIN.ForeColor = System.Drawing.Color.Black;
            this.YUUKOU_BEGIN.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.YUUKOU_BEGIN.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.YUUKOU_BEGIN.IsInputErrorOccured = false;
            this.YUUKOU_BEGIN.Location = new System.Drawing.Point(111, 82);
            this.YUUKOU_BEGIN.MaxLength = 10;
            this.YUUKOU_BEGIN.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.YUUKOU_BEGIN.Name = "YUUKOU_BEGIN";
            this.YUUKOU_BEGIN.NullValue = "";
            this.YUUKOU_BEGIN.PopupAfterExecute = null;
            this.YUUKOU_BEGIN.PopupBeforeExecute = null;
            this.YUUKOU_BEGIN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("YUUKOU_BEGIN.PopupSearchSendParams")));
            this.YUUKOU_BEGIN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.YUUKOU_BEGIN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("YUUKOU_BEGIN.popupWindowSetting")));
            this.YUUKOU_BEGIN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("YUUKOU_BEGIN.RegistCheckMethod")));
            this.YUUKOU_BEGIN.ShortItemName = "適用開始日";
            this.YUUKOU_BEGIN.Size = new System.Drawing.Size(126, 20);
            this.YUUKOU_BEGIN.TabIndex = 30;
            this.YUUKOU_BEGIN.Tag = " ";
            this.YUUKOU_BEGIN.Text = "2013/09/11(水)";
            this.YUUKOU_BEGIN.Value = new System.DateTime(2013, 9, 11, 0, 0, 0, 0);
            // 
            // TEKIYOU_KBN
            // 
            this.TEKIYOU_KBN.AutoSize = true;
            this.TEKIYOU_KBN.Location = new System.Drawing.Point(240, 86);
            this.TEKIYOU_KBN.Name = "TEKIYOU_KBN";
            this.TEKIYOU_KBN.Size = new System.Drawing.Size(181, 16);
            this.TEKIYOU_KBN.TabIndex = 31;
            this.TEKIYOU_KBN.Text = "適用開始日/終了日を含め複写";
            this.TEKIYOU_KBN.UseVisualStyleBackColor = true;
            this.TEKIYOU_KBN.CheckedChanged += new System.EventHandler(this.TEKIYOU_KBN_CheckedChanged);
            // 
            // InitialPopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(459, 232);
            this.Controls.Add(this.TEKIYOU_KBN);
            this.Controls.Add(this.YUUKOU_END);
            this.Controls.Add(this.YUUKOU_BEGIN);
            this.Controls.Add(this.TOROKU_HOUHOU_KBN);
            this.Controls.Add(this.pl_SYS_KAKUTEI__TANNI_KBN);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.GENBA_CD);
            this.Controls.Add(this.GYOUSHA_CD);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TORIHIKISAKI_CD);
            this.Controls.Add(this.GYOUSHA_NAME_RYAKU);
            this.Controls.Add(this.TORIHIKISAKI_NAME_RYAKU);
            this.Controls.Add(this.GENBA_NAME_RYAKU);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bt_func12);
            this.Controls.Add(this.bt_func8);
            this.Name = "InitialPopupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "個別品名単価複写";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.InitialPopupForm_KeyUp);
            this.pl_SYS_KAKUTEI__TANNI_KBN.ResumeLayout(false);
            this.pl_SYS_KAKUTEI__TANNI_KBN.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomButton bt_func12;
        public r_framework.CustomControl.CustomButton bt_func8;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GENBA_CD;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GYOUSHA_CD;
        internal Label label4;
        internal r_framework.CustomControl.CustomAlphaNumTextBox TORIHIKISAKI_CD;
        internal r_framework.CustomControl.CustomTextBox GYOUSHA_NAME_RYAKU;
        internal r_framework.CustomControl.CustomTextBox TORIHIKISAKI_NAME_RYAKU;
        internal r_framework.CustomControl.CustomTextBox GENBA_NAME_RYAKU;
        internal Label label1;
        internal Label label2;
        internal Label label3;
        internal Label label5;
        internal Label label6;
        private Panel pl_SYS_KAKUTEI__TANNI_KBN;
        internal r_framework.CustomControl.CustomRadioButton TOROKU_HOUHOU_KBN2;
        internal r_framework.CustomControl.CustomRadioButton TOROKU_HOUHOU_KBN1;
        internal r_framework.CustomControl.CustomNumericTextBox2 TOROKU_HOUHOU_KBN;
        internal r_framework.CustomControl.CustomDateTimePicker YUUKOU_END;
        internal r_framework.CustomControl.CustomDateTimePicker YUUKOU_BEGIN;
        internal CheckBox TEKIYOU_KBN;
    }
}
