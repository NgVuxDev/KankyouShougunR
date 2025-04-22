using r_framework.CustomControl;
namespace KihonHinmeiTankaHoshu.APP
{
    partial class KihonHinmeiTankaHoshuForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KihonHinmeiTankaHoshuForm));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.ShortcutKeyManager shortcutKeyManager1 = new GrapeCity.Win.MultiRow.ShortcutKeyManager();
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI = new System.Windows.Forms.CheckBox();
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED = new System.Windows.Forms.CheckBox();
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SAISHUU_SHOBUNJOU_KBN = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.HINMEI_USE_KBN = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.SYURUI_ALL = new r_framework.CustomControl.CustomRadioButton();
            this.SYURUI_SHITEI = new r_framework.CustomControl.CustomRadioButton();
            this.SHURUI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.SHURUI_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.Ichiran = new r_framework.CustomControl.GcCustomMultiRow(this.components);
            this.kihonHinmeiTankaHoshuDetail1 = new KihonHinmeiTankaHoshu.MultiRowTemplate.KihonHinmeiTankaHoshuDetail();
            this.UNPAN_JUTAKUSHA_KAISHA_KBN = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.SHOBUN_NIOROSHI_GENBA_KBN = new r_framework.CustomControl.CustomAlphaNumTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).BeginInit();
            this.SuspendLayout();
            // 
            // ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Location = new System.Drawing.Point(636, 7);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Name = "ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Size = new System.Drawing.Size(84, 16);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.TabIndex = 5;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Text = "適用期間外";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.UseVisualStyleBackColor = true;
            // 
            // ICHIRAN_HYOUJI_JOUKEN_DELETED
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Location = new System.Drawing.Point(582, 7);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Name = "ICHIRAN_HYOUJI_JOUKEN_DELETED";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Size = new System.Drawing.Size(48, 16);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.TabIndex = 4;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Text = "削除";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.UseVisualStyleBackColor = true;
            // 
            // ICHIRAN_HYOUJI_JOUKEN_TEKIYOU
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Location = new System.Drawing.Point(516, 7);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Name = "ICHIRAN_HYOUJI_JOUKEN_TEKIYOU";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Size = new System.Drawing.Size(60, 16);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.TabIndex = 3;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Text = "適用中";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(411, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 20);
            this.label1.TabIndex = 403;
            this.label1.Text = "表示条件";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SAISHUU_SHOBUNJOU_KBN
            // 
            this.SAISHUU_SHOBUNJOU_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.SAISHUU_SHOBUNJOU_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SAISHUU_SHOBUNJOU_KBN.CharacterLimitList = null;
            this.SAISHUU_SHOBUNJOU_KBN.CharactersNumber = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SAISHUU_SHOBUNJOU_KBN.DBFieldsName = "SAISHUU_SHOBUNJOU_KBN";
            this.SAISHUU_SHOBUNJOU_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.SAISHUU_SHOBUNJOU_KBN.DisplayItemName = "最終処分場区分";
            this.SAISHUU_SHOBUNJOU_KBN.DisplayPopUp = null;
            this.SAISHUU_SHOBUNJOU_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SAISHUU_SHOBUNJOU_KBN.FocusOutCheckMethod")));
            this.SAISHUU_SHOBUNJOU_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SAISHUU_SHOBUNJOU_KBN.ForeColor = System.Drawing.Color.Black;
            this.SAISHUU_SHOBUNJOU_KBN.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SAISHUU_SHOBUNJOU_KBN.IsInputErrorOccured = false;
            this.SAISHUU_SHOBUNJOU_KBN.ItemDefinedTypes = "bit";
            this.SAISHUU_SHOBUNJOU_KBN.Location = new System.Drawing.Point(894, 2);
            this.SAISHUU_SHOBUNJOU_KBN.MaxLength = 1;
            this.SAISHUU_SHOBUNJOU_KBN.Name = "SAISHUU_SHOBUNJOU_KBN";
            this.SAISHUU_SHOBUNJOU_KBN.PopupAfterExecute = null;
            this.SAISHUU_SHOBUNJOU_KBN.PopupBeforeExecute = null;
            this.SAISHUU_SHOBUNJOU_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SAISHUU_SHOBUNJOU_KBN.PopupSearchSendParams")));
            this.SAISHUU_SHOBUNJOU_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SAISHUU_SHOBUNJOU_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SAISHUU_SHOBUNJOU_KBN.popupWindowSetting")));
            this.SAISHUU_SHOBUNJOU_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SAISHUU_SHOBUNJOU_KBN.RegistCheckMethod")));
            this.SAISHUU_SHOBUNJOU_KBN.ShortItemName = "最終処分場区分";
            this.SAISHUU_SHOBUNJOU_KBN.Size = new System.Drawing.Size(20, 20);
            this.SAISHUU_SHOBUNJOU_KBN.TabIndex = 611;
            this.SAISHUU_SHOBUNJOU_KBN.Text = "1";
            this.SAISHUU_SHOBUNJOU_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.SAISHUU_SHOBUNJOU_KBN.Visible = false;
            // 
            // HINMEI_USE_KBN
            // 
            this.HINMEI_USE_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.HINMEI_USE_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HINMEI_USE_KBN.CharacterLimitList = null;
            this.HINMEI_USE_KBN.CharactersNumber = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.HINMEI_USE_KBN.DBFieldsName = "HINMEI_USE_KBN";
            this.HINMEI_USE_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.HINMEI_USE_KBN.DisplayItemName = "品名使用区分";
            this.HINMEI_USE_KBN.DisplayPopUp = null;
            this.HINMEI_USE_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_USE_KBN.FocusOutCheckMethod")));
            this.HINMEI_USE_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HINMEI_USE_KBN.ForeColor = System.Drawing.Color.Black;
            this.HINMEI_USE_KBN.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.HINMEI_USE_KBN.IsInputErrorOccured = false;
            this.HINMEI_USE_KBN.ItemDefinedTypes = "bit";
            this.HINMEI_USE_KBN.Location = new System.Drawing.Point(790, 2);
            this.HINMEI_USE_KBN.MaxLength = 1;
            this.HINMEI_USE_KBN.Name = "HINMEI_USE_KBN";
            this.HINMEI_USE_KBN.PopupAfterExecute = null;
            this.HINMEI_USE_KBN.PopupBeforeExecute = null;
            this.HINMEI_USE_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HINMEI_USE_KBN.PopupSearchSendParams")));
            this.HINMEI_USE_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HINMEI_USE_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HINMEI_USE_KBN.popupWindowSetting")));
            this.HINMEI_USE_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_USE_KBN.RegistCheckMethod")));
            this.HINMEI_USE_KBN.ShortItemName = "品名使用区分";
            this.HINMEI_USE_KBN.Size = new System.Drawing.Size(20, 20);
            this.HINMEI_USE_KBN.TabIndex = 639;
            this.HINMEI_USE_KBN.Text = "1";
            this.HINMEI_USE_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.HINMEI_USE_KBN.Visible = false;
            // 
            // SYURUI_ALL
            // 
            this.SYURUI_ALL.AutoSize = true;
            this.SYURUI_ALL.Checked = true;
            this.SYURUI_ALL.DefaultBackColor = System.Drawing.Color.Empty;
            this.SYURUI_ALL.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SYURUI_ALL.FocusOutCheckMethod")));
            this.SYURUI_ALL.Location = new System.Drawing.Point(31, 6);
            this.SYURUI_ALL.Name = "SYURUI_ALL";
            this.SYURUI_ALL.PopupAfterExecute = null;
            this.SYURUI_ALL.PopupBeforeExecute = null;
            this.SYURUI_ALL.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SYURUI_ALL.PopupSearchSendParams")));
            this.SYURUI_ALL.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SYURUI_ALL.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SYURUI_ALL.popupWindowSetting")));
            this.SYURUI_ALL.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SYURUI_ALL.RegistCheckMethod")));
            this.SYURUI_ALL.Size = new System.Drawing.Size(47, 16);
            this.SYURUI_ALL.TabIndex = 640;
            this.SYURUI_ALL.Text = "全件";
            this.SYURUI_ALL.UseVisualStyleBackColor = true;
            this.SYURUI_ALL.CheckedChanged += new System.EventHandler(this.SYURUI_CheckedChanged);
            // 
            // SYURUI_SHITEI
            // 
            this.SYURUI_SHITEI.AutoSize = true;
            this.SYURUI_SHITEI.DefaultBackColor = System.Drawing.Color.Empty;
            this.SYURUI_SHITEI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SYURUI_SHITEI.FocusOutCheckMethod")));
            this.SYURUI_SHITEI.Location = new System.Drawing.Point(84, 6);
            this.SYURUI_SHITEI.Name = "SYURUI_SHITEI";
            this.SYURUI_SHITEI.PopupAfterExecute = null;
            this.SYURUI_SHITEI.PopupBeforeExecute = null;
            this.SYURUI_SHITEI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SYURUI_SHITEI.PopupSearchSendParams")));
            this.SYURUI_SHITEI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SYURUI_SHITEI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SYURUI_SHITEI.popupWindowSetting")));
            this.SYURUI_SHITEI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SYURUI_SHITEI.RegistCheckMethod")));
            this.SYURUI_SHITEI.Size = new System.Drawing.Size(71, 16);
            this.SYURUI_SHITEI.TabIndex = 641;
            this.SYURUI_SHITEI.Text = "種類指定";
            this.SYURUI_SHITEI.UseVisualStyleBackColor = true;
            this.SYURUI_SHITEI.CheckedChanged += new System.EventHandler(this.SYURUI_CheckedChanged);
            // 
            // SHURUI_CD
            // 
            this.SHURUI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.SHURUI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHURUI_CD.ChangeUpperCase = true;
            this.SHURUI_CD.CharacterLimitList = null;
            this.SHURUI_CD.CharactersNumber = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.SHURUI_CD.DBFieldsName = "SHURUI_CD";
            this.SHURUI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHURUI_CD.DisplayItemName = "種類CD";
            this.SHURUI_CD.DisplayPopUp = null;
            this.SHURUI_CD.Enabled = false;
            this.SHURUI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHURUI_CD.FocusOutCheckMethod")));
            this.SHURUI_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHURUI_CD.ForeColor = System.Drawing.Color.Black;
            this.SHURUI_CD.GetCodeMasterField = "SHURUI_CD,SHURUI_NAME_RYAKU";
            this.SHURUI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SHURUI_CD.IsInputErrorOccured = false;
            this.SHURUI_CD.Location = new System.Drawing.Point(162, 6);
            this.SHURUI_CD.MaxLength = 3;
            this.SHURUI_CD.Name = "SHURUI_CD";
            this.SHURUI_CD.PopupAfterExecute = null;
            this.SHURUI_CD.PopupBeforeExecute = null;
            this.SHURUI_CD.PopupGetMasterField = "SHURUI_CD,SHURUI_NAME_RYAKU";
            this.SHURUI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHURUI_CD.PopupSearchSendParams")));
            this.SHURUI_CD.PopupSetFormField = "SHURUI_CD,SHURUI_NAME_RYAKU";
            this.SHURUI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHURUI;
            this.SHURUI_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.SHURUI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHURUI_CD.popupWindowSetting")));
            this.SHURUI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHURUI_CD.RegistCheckMethod")));
            this.SHURUI_CD.SetFormField = "SHURUI_CD,SHURUI_NAME_RYAKU";
            this.SHURUI_CD.Size = new System.Drawing.Size(38, 20);
            this.SHURUI_CD.TabIndex = 642;
            this.SHURUI_CD.Tag = "種類を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.SHURUI_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.SHURUI_CD.ZeroPaddengFlag = true;
            this.SHURUI_CD.TextChanged += new System.EventHandler(this.SHURUI_CD_TextChanged);
            this.SHURUI_CD.Validating += new System.ComponentModel.CancelEventHandler(this.SHURUI_CD_Validating);
            // 
            // SHURUI_NAME_RYAKU
            // 
            this.SHURUI_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.SHURUI_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHURUI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHURUI_NAME_RYAKU.DisplayPopUp = null;
            this.SHURUI_NAME_RYAKU.Enabled = false;
            this.SHURUI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHURUI_NAME_RYAKU.FocusOutCheckMethod")));
            this.SHURUI_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.SHURUI_NAME_RYAKU.IsInputErrorOccured = false;
            this.SHURUI_NAME_RYAKU.Location = new System.Drawing.Point(200, 6);
            this.SHURUI_NAME_RYAKU.Name = "SHURUI_NAME_RYAKU";
            this.SHURUI_NAME_RYAKU.PopupAfterExecute = null;
            this.SHURUI_NAME_RYAKU.PopupBeforeExecute = null;
            this.SHURUI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHURUI_NAME_RYAKU.PopupSearchSendParams")));
            this.SHURUI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHURUI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHURUI_NAME_RYAKU.popupWindowSetting")));
            this.SHURUI_NAME_RYAKU.ReadOnly = true;
            this.SHURUI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHURUI_NAME_RYAKU.RegistCheckMethod")));
            this.SHURUI_NAME_RYAKU.Size = new System.Drawing.Size(197, 19);
            this.SHURUI_NAME_RYAKU.TabIndex = 643;
            this.SHURUI_NAME_RYAKU.TabStop = false;
            // 
            // Ichiran
            // 
            this.Ichiran.AllowUserToDeleteRows = false;
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.Ichiran.ColumnHeadersDefaultHeaderCellStyle = cellStyle1;
            this.Ichiran.CurrentRowBorderLine = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Medium, System.Drawing.Color.Red);
            cellStyle2.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            cellStyle2.SelectionBackColor = System.Drawing.Color.Transparent;
            cellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.Ichiran.DefaultCellStyle = cellStyle2;
            this.Ichiran.EditMode = GrapeCity.Win.MultiRow.EditMode.EditOnEnter;
            this.Ichiran.Location = new System.Drawing.Point(10, 28);
            this.Ichiran.MultiSelect = false;
            this.Ichiran.Name = "Ichiran";
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveUp)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveDown)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveLeft)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveRight)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftLeft)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftRight)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCellInRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCellInRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCellInRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCellInRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.PageUp)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Next)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Space)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.CommitRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Clear)), System.Windows.Forms.Keys.Delete));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.NumPad0)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), System.Windows.Forms.Keys.F4));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollUp)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollDown)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollLeft)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollRight)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToFirstPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToLastPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToPreviousPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Space)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToNextPage)), System.Windows.Forms.Keys.Space));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToFirstPage)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToFirstPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToLastPage)), System.Windows.Forms.Keys.End));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToLastPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ReverseSelectCurrentRow)), System.Windows.Forms.Keys.Space));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToPreviousPage)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToNextPage)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
                    | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToPreviousRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToPreviousRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToNextRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToNextRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.PageUp)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Next)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.CommitRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Clear)), System.Windows.Forms.Keys.Delete));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.NumPad0)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), System.Windows.Forms.Keys.F4));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            this.Ichiran.ShortcutKeyManager = shortcutKeyManager1;
            this.Ichiran.Size = new System.Drawing.Size(986, 420);
            this.Ichiran.TabIndex = 6;
            this.Ichiran.Template = this.kihonHinmeiTankaHoshuDetail1;
            this.Ichiran.CellValidating += new System.EventHandler<GrapeCity.Win.MultiRow.CellValidatingEventArgs>(this.Ichiran_CellValidating);
            this.Ichiran.CellValidated += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.Ichiran_CellValidated);
            this.Ichiran.CellValueChanged += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.Ichiran_CellValueChanged);
            this.Ichiran.CellFormatting += new System.EventHandler<GrapeCity.Win.MultiRow.CellFormattingEventArgs>(this.Ichiran_CellFormatting);
            this.Ichiran.CellEnter += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.Ichiran_CellEnter);
            this.Ichiran.EditingControlShowing += new System.EventHandler<GrapeCity.Win.MultiRow.EditingControlShowingEventArgs>(this.Ichiran_EditingControlShowing);
            // 
            // UNPAN_JUTAKUSHA_KAISHA_KBN
            // 
            this.UNPAN_JUTAKUSHA_KAISHA_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.UNPAN_JUTAKUSHA_KAISHA_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UNPAN_JUTAKUSHA_KAISHA_KBN.CharacterLimitList = null;
            this.UNPAN_JUTAKUSHA_KAISHA_KBN.CharactersNumber = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UNPAN_JUTAKUSHA_KAISHA_KBN.DBFieldsName = "UNPAN_JUTAKUSHA_KAISHA_KBN";
            this.UNPAN_JUTAKUSHA_KAISHA_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNPAN_JUTAKUSHA_KAISHA_KBN.DisplayItemName = "運搬受託者/運搬会社区分";
            this.UNPAN_JUTAKUSHA_KAISHA_KBN.DisplayPopUp = null;
            this.UNPAN_JUTAKUSHA_KAISHA_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_JUTAKUSHA_KAISHA_KBN.FocusOutCheckMethod")));
            this.UNPAN_JUTAKUSHA_KAISHA_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UNPAN_JUTAKUSHA_KAISHA_KBN.ForeColor = System.Drawing.Color.Black;
            this.UNPAN_JUTAKUSHA_KAISHA_KBN.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.UNPAN_JUTAKUSHA_KAISHA_KBN.IsInputErrorOccured = false;
            this.UNPAN_JUTAKUSHA_KAISHA_KBN.ItemDefinedTypes = "bit";
            this.UNPAN_JUTAKUSHA_KAISHA_KBN.Location = new System.Drawing.Point(816, 2);
            this.UNPAN_JUTAKUSHA_KAISHA_KBN.MaxLength = 1;
            this.UNPAN_JUTAKUSHA_KAISHA_KBN.Name = "UNPAN_JUTAKUSHA_KAISHA_KBN";
            this.UNPAN_JUTAKUSHA_KAISHA_KBN.PopupAfterExecute = null;
            this.UNPAN_JUTAKUSHA_KAISHA_KBN.PopupBeforeExecute = null;
            this.UNPAN_JUTAKUSHA_KAISHA_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNPAN_JUTAKUSHA_KAISHA_KBN.PopupSearchSendParams")));
            this.UNPAN_JUTAKUSHA_KAISHA_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UNPAN_JUTAKUSHA_KAISHA_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNPAN_JUTAKUSHA_KAISHA_KBN.popupWindowSetting")));
            this.UNPAN_JUTAKUSHA_KAISHA_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_JUTAKUSHA_KAISHA_KBN.RegistCheckMethod")));
            this.UNPAN_JUTAKUSHA_KAISHA_KBN.ShortItemName = "運搬受託者/運搬会社区分";
            this.UNPAN_JUTAKUSHA_KAISHA_KBN.Size = new System.Drawing.Size(20, 20);
            this.UNPAN_JUTAKUSHA_KAISHA_KBN.TabIndex = 644;
            this.UNPAN_JUTAKUSHA_KAISHA_KBN.Text = "1";
            this.UNPAN_JUTAKUSHA_KAISHA_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UNPAN_JUTAKUSHA_KAISHA_KBN.Visible = false;
            // 
            // SHOBUN_NIOROSHI_GYOUSHA_KBN
            // 
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN.CharacterLimitList = null;
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN.CharactersNumber = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN.DBFieldsName = "SHOBUN_NIOROSHI_GYOUSHA_KBN";
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN.DisplayItemName = "運搬受託者区分";
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN.DisplayPopUp = null;
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_NIOROSHI_GYOUSHA_KBN.FocusOutCheckMethod")));
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN.ForeColor = System.Drawing.Color.Black;
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsInputErrorOccured = false;
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN.ItemDefinedTypes = "bit";
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN.Location = new System.Drawing.Point(842, 2);
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN.MaxLength = 1;
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN.Name = "SHOBUN_NIOROSHI_GYOUSHA_KBN";
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN.PopupAfterExecute = null;
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN.PopupBeforeExecute = null;
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHOBUN_NIOROSHI_GYOUSHA_KBN.PopupSearchSendParams")));
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHOBUN_NIOROSHI_GYOUSHA_KBN.popupWindowSetting")));
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_NIOROSHI_GYOUSHA_KBN.RegistCheckMethod")));
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN.ShortItemName = "運搬受託者区分";
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN.Size = new System.Drawing.Size(20, 20);
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN.TabIndex = 645;
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN.Text = "1";
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.SHOBUN_NIOROSHI_GYOUSHA_KBN.Visible = false;
            // 
            // SHOBUN_NIOROSHI_GENBA_KBN
            // 
            this.SHOBUN_NIOROSHI_GENBA_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.SHOBUN_NIOROSHI_GENBA_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHOBUN_NIOROSHI_GENBA_KBN.CharacterLimitList = null;
            this.SHOBUN_NIOROSHI_GENBA_KBN.CharactersNumber = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SHOBUN_NIOROSHI_GENBA_KBN.DBFieldsName = "SHOBUN_NIOROSHI_GENBA_KBN";
            this.SHOBUN_NIOROSHI_GENBA_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHOBUN_NIOROSHI_GENBA_KBN.DisplayItemName = "処分事業場/荷降現場区分";
            this.SHOBUN_NIOROSHI_GENBA_KBN.DisplayPopUp = null;
            this.SHOBUN_NIOROSHI_GENBA_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_NIOROSHI_GENBA_KBN.FocusOutCheckMethod")));
            this.SHOBUN_NIOROSHI_GENBA_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHOBUN_NIOROSHI_GENBA_KBN.ForeColor = System.Drawing.Color.Black;
            this.SHOBUN_NIOROSHI_GENBA_KBN.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SHOBUN_NIOROSHI_GENBA_KBN.IsInputErrorOccured = false;
            this.SHOBUN_NIOROSHI_GENBA_KBN.ItemDefinedTypes = "bit";
            this.SHOBUN_NIOROSHI_GENBA_KBN.Location = new System.Drawing.Point(868, 2);
            this.SHOBUN_NIOROSHI_GENBA_KBN.MaxLength = 1;
            this.SHOBUN_NIOROSHI_GENBA_KBN.Name = "SHOBUN_NIOROSHI_GENBA_KBN";
            this.SHOBUN_NIOROSHI_GENBA_KBN.PopupAfterExecute = null;
            this.SHOBUN_NIOROSHI_GENBA_KBN.PopupBeforeExecute = null;
            this.SHOBUN_NIOROSHI_GENBA_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHOBUN_NIOROSHI_GENBA_KBN.PopupSearchSendParams")));
            this.SHOBUN_NIOROSHI_GENBA_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHOBUN_NIOROSHI_GENBA_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHOBUN_NIOROSHI_GENBA_KBN.popupWindowSetting")));
            this.SHOBUN_NIOROSHI_GENBA_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_NIOROSHI_GENBA_KBN.RegistCheckMethod")));
            this.SHOBUN_NIOROSHI_GENBA_KBN.ShortItemName = "処分事業場/荷降現場区分";
            this.SHOBUN_NIOROSHI_GENBA_KBN.Size = new System.Drawing.Size(20, 20);
            this.SHOBUN_NIOROSHI_GENBA_KBN.TabIndex = 646;
            this.SHOBUN_NIOROSHI_GENBA_KBN.Text = "1";
            this.SHOBUN_NIOROSHI_GENBA_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.SHOBUN_NIOROSHI_GENBA_KBN.Visible = false;
            // 
            // KihonHinmeiTankaHoshuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(999, 530);
            this.Controls.Add(this.SHOBUN_NIOROSHI_GENBA_KBN);
            this.Controls.Add(this.SHOBUN_NIOROSHI_GYOUSHA_KBN);
            this.Controls.Add(this.UNPAN_JUTAKUSHA_KAISHA_KBN);
            this.Controls.Add(this.SHURUI_NAME_RYAKU);
            this.Controls.Add(this.SHURUI_CD);
            this.Controls.Add(this.SYURUI_SHITEI);
            this.Controls.Add(this.SYURUI_ALL);
            this.Controls.Add(this.HINMEI_USE_KBN);
            this.Controls.Add(this.SAISHUU_SHOBUNJOU_KBN);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI);
            this.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_DELETED);
            this.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU);
            this.Controls.Add(this.Ichiran);
            this.Name = "KihonHinmeiTankaHoshuForm";
            this.Text = "KihonHinmeiTankaHoshuForm";
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MultiRowTemplate.KihonHinmeiTankaHoshuDetail kihonHinmeiTankaHoshuDetail1;
        internal GcCustomMultiRow Ichiran;
        internal System.Windows.Forms.CheckBox ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI;
        internal System.Windows.Forms.CheckBox ICHIRAN_HYOUJI_JOUKEN_DELETED;
        internal System.Windows.Forms.CheckBox ICHIRAN_HYOUJI_JOUKEN_TEKIYOU;
        internal System.Windows.Forms.Label label1;
        internal CustomAlphaNumTextBox SAISHUU_SHOBUNJOU_KBN;
        internal CustomAlphaNumTextBox HINMEI_USE_KBN;
        internal CustomRadioButton SYURUI_ALL;
        internal CustomRadioButton SYURUI_SHITEI;
        internal CustomAlphaNumTextBox SHURUI_CD;
        internal CustomTextBox SHURUI_NAME_RYAKU;
        internal CustomAlphaNumTextBox UNPAN_JUTAKUSHA_KAISHA_KBN;
        internal CustomAlphaNumTextBox SHOBUN_NIOROSHI_GYOUSHA_KBN;
        internal CustomAlphaNumTextBox SHOBUN_NIOROSHI_GENBA_KBN;
    }
}
