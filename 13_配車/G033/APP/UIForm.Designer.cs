namespace Shougun.Core.Allocation.HaishaMeisai
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbl_Kikan = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.UNTENSHA_NAME_FROM = new r_framework.CustomControl.CustomTextBox();
            this.UNTENSHA_LABEL = new System.Windows.Forms.Label();
            this.UNTENSHA_CD_FROM = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.UNTENSHA_CD_TO = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.UNTENSHA_NAME_TO = new r_framework.CustomControl.CustomTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.HIDUKE_FROM = new r_framework.CustomControl.CustomDateTimePicker();
            this.HIDUKE_TO = new r_framework.CustomControl.CustomDateTimePicker();
            this.customDataGridView1 = new r_framework.CustomControl.CustomDataGridView(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Kikan
            // 
            this.lbl_Kikan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Kikan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Kikan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Kikan.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Kikan.ForeColor = System.Drawing.Color.White;
            this.lbl_Kikan.Location = new System.Drawing.Point(0, 2);
            this.lbl_Kikan.Name = "lbl_Kikan";
            this.lbl_Kikan.Size = new System.Drawing.Size(110, 20);
            this.lbl_Kikan.TabIndex = 200;
            this.lbl_Kikan.Text = "作業日※";
            this.lbl_Kikan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(291, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 20);
            this.label1.TabIndex = 536;
            this.label1.Text = "～";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UNTENSHA_NAME_FROM
            // 
            this.UNTENSHA_NAME_FROM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.UNTENSHA_NAME_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UNTENSHA_NAME_FROM.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.UNTENSHA_NAME_FROM.DBFieldsName = "";
            this.UNTENSHA_NAME_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNTENSHA_NAME_FROM.DisplayPopUp = null;
            this.UNTENSHA_NAME_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNTENSHA_NAME_FROM.FocusOutCheckMethod")));
            this.UNTENSHA_NAME_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.UNTENSHA_NAME_FROM.ForeColor = System.Drawing.Color.Black;
            this.UNTENSHA_NAME_FROM.IsInputErrorOccured = false;
            this.UNTENSHA_NAME_FROM.ItemDefinedTypes = "varchar";
            this.UNTENSHA_NAME_FROM.Location = new System.Drawing.Point(165, 24);
            this.UNTENSHA_NAME_FROM.MaxLength = 0;
            this.UNTENSHA_NAME_FROM.Name = "UNTENSHA_NAME_FROM";
            this.UNTENSHA_NAME_FROM.PopupAfterExecute = null;
            this.UNTENSHA_NAME_FROM.PopupBeforeExecute = null;
            this.UNTENSHA_NAME_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNTENSHA_NAME_FROM.PopupSearchSendParams")));
            this.UNTENSHA_NAME_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UNTENSHA_NAME_FROM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNTENSHA_NAME_FROM.popupWindowSetting")));
            this.UNTENSHA_NAME_FROM.prevText = null;
            this.UNTENSHA_NAME_FROM.ReadOnly = true;
            this.UNTENSHA_NAME_FROM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNTENSHA_NAME_FROM.RegistCheckMethod")));
            this.UNTENSHA_NAME_FROM.Size = new System.Drawing.Size(117, 20);
            this.UNTENSHA_NAME_FROM.TabIndex = 803;
            this.UNTENSHA_NAME_FROM.TabStop = false;
            this.UNTENSHA_NAME_FROM.Tag = " ";
            // 
            // UNTENSHA_LABEL
            // 
            this.UNTENSHA_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.UNTENSHA_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UNTENSHA_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UNTENSHA_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.UNTENSHA_LABEL.ForeColor = System.Drawing.Color.White;
            this.UNTENSHA_LABEL.Location = new System.Drawing.Point(0, 24);
            this.UNTENSHA_LABEL.Name = "UNTENSHA_LABEL";
            this.UNTENSHA_LABEL.Size = new System.Drawing.Size(110, 20);
            this.UNTENSHA_LABEL.TabIndex = 804;
            this.UNTENSHA_LABEL.Text = "運転者※";
            this.UNTENSHA_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UNTENSHA_CD_FROM
            // 
            this.UNTENSHA_CD_FROM.BackColor = System.Drawing.SystemColors.Window;
            this.UNTENSHA_CD_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UNTENSHA_CD_FROM.CharacterLimitList = null;
            this.UNTENSHA_CD_FROM.ChangeUpperCase = true;
            this.UNTENSHA_CD_FROM.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.UNTENSHA_CD_FROM.DBFieldsName = "";
            this.UNTENSHA_CD_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNTENSHA_CD_FROM.DisplayItemName = "運転者開始CD";
            this.UNTENSHA_CD_FROM.DisplayPopUp = null;
            this.UNTENSHA_CD_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNTENSHA_CD_FROM.FocusOutCheckMethod")));
            this.UNTENSHA_CD_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.UNTENSHA_CD_FROM.ForeColor = System.Drawing.Color.Black;
            this.UNTENSHA_CD_FROM.GetCodeMasterField = "SHAIN_CD, SHAIN_NAME_RYAKU";
            this.UNTENSHA_CD_FROM.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.UNTENSHA_CD_FROM.IsInputErrorOccured = false;
            this.UNTENSHA_CD_FROM.ItemDefinedTypes = "varchar";
            this.UNTENSHA_CD_FROM.Location = new System.Drawing.Point(115, 24);
            this.UNTENSHA_CD_FROM.MaxLength = 6;
            this.UNTENSHA_CD_FROM.Name = "UNTENSHA_CD_FROM";
            this.UNTENSHA_CD_FROM.PopupAfterExecute = null;
            this.UNTENSHA_CD_FROM.PopupBeforeExecute = null;
            this.UNTENSHA_CD_FROM.PopupGetMasterField = "SHAIN_CD, SHAIN_NAME_RYAKU";
            this.UNTENSHA_CD_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNTENSHA_CD_FROM.PopupSearchSendParams")));
            this.UNTENSHA_CD_FROM.PopupSetFormField = "UNTENSHA_CD_FROM, UNTENSHA_NAME_FROM";
            this.UNTENSHA_CD_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
            this.UNTENSHA_CD_FROM.PopupWindowName = "マスタ共通ポップアップ";
            this.UNTENSHA_CD_FROM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNTENSHA_CD_FROM.popupWindowSetting")));
            this.UNTENSHA_CD_FROM.prevText = null;
            this.UNTENSHA_CD_FROM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNTENSHA_CD_FROM.RegistCheckMethod")));
            this.UNTENSHA_CD_FROM.SetFormField = "UNTENSHA_CD_FROM, UNTENSHA_NAME_FROM";
            this.UNTENSHA_CD_FROM.Size = new System.Drawing.Size(50, 20);
            this.UNTENSHA_CD_FROM.TabIndex = 2;
            this.UNTENSHA_CD_FROM.Tag = "運転者(開始)を選択してください";
            this.UNTENSHA_CD_FROM.ZeroPaddengFlag = true;
            this.UNTENSHA_CD_FROM.Validating += new System.ComponentModel.CancelEventHandler(this.UNTENSHA_CD_FROM_Validating);
            // 
            // UNTENSHA_CD_TO
            // 
            this.UNTENSHA_CD_TO.BackColor = System.Drawing.SystemColors.Window;
            this.UNTENSHA_CD_TO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UNTENSHA_CD_TO.CharacterLimitList = null;
            this.UNTENSHA_CD_TO.ChangeUpperCase = true;
            this.UNTENSHA_CD_TO.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.UNTENSHA_CD_TO.DBFieldsName = "";
            this.UNTENSHA_CD_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNTENSHA_CD_TO.DisplayItemName = "運転者終了CD";
            this.UNTENSHA_CD_TO.DisplayPopUp = null;
            this.UNTENSHA_CD_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNTENSHA_CD_TO.FocusOutCheckMethod")));
            this.UNTENSHA_CD_TO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.UNTENSHA_CD_TO.ForeColor = System.Drawing.Color.Black;
            this.UNTENSHA_CD_TO.GetCodeMasterField = "SHAIN_CD, SHAIN_NAME_RYAKU";
            this.UNTENSHA_CD_TO.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.UNTENSHA_CD_TO.IsInputErrorOccured = false;
            this.UNTENSHA_CD_TO.ItemDefinedTypes = "varchar";
            this.UNTENSHA_CD_TO.Location = new System.Drawing.Point(315, 24);
            this.UNTENSHA_CD_TO.MaxLength = 6;
            this.UNTENSHA_CD_TO.Name = "UNTENSHA_CD_TO";
            this.UNTENSHA_CD_TO.PopupAfterExecute = null;
            this.UNTENSHA_CD_TO.PopupBeforeExecute = null;
            this.UNTENSHA_CD_TO.PopupGetMasterField = "SHAIN_CD, SHAIN_NAME_RYAKU";
            this.UNTENSHA_CD_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNTENSHA_CD_TO.PopupSearchSendParams")));
            this.UNTENSHA_CD_TO.PopupSetFormField = "UNTENSHA_CD_TO, UNTENSHA_NAME_TO";
            this.UNTENSHA_CD_TO.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
            this.UNTENSHA_CD_TO.PopupWindowName = "マスタ共通ポップアップ";
            this.UNTENSHA_CD_TO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNTENSHA_CD_TO.popupWindowSetting")));
            this.UNTENSHA_CD_TO.prevText = null;
            this.UNTENSHA_CD_TO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNTENSHA_CD_TO.RegistCheckMethod")));
            this.UNTENSHA_CD_TO.SetFormField = "UNTENSHA_CD_TO, UNTENSHA_NAME_TO";
            this.UNTENSHA_CD_TO.Size = new System.Drawing.Size(50, 20);
            this.UNTENSHA_CD_TO.TabIndex = 3;
            this.UNTENSHA_CD_TO.Tag = "運転者(終了)を選択してください";
            this.UNTENSHA_CD_TO.ZeroPaddengFlag = true;
            this.UNTENSHA_CD_TO.Validating += new System.ComponentModel.CancelEventHandler(this.UNTENSHA_CD_TO_Validating);
            // 
            // UNTENSHA_NAME_TO
            // 
            this.UNTENSHA_NAME_TO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.UNTENSHA_NAME_TO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UNTENSHA_NAME_TO.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.UNTENSHA_NAME_TO.DBFieldsName = "";
            this.UNTENSHA_NAME_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNTENSHA_NAME_TO.DisplayPopUp = null;
            this.UNTENSHA_NAME_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNTENSHA_NAME_TO.FocusOutCheckMethod")));
            this.UNTENSHA_NAME_TO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.UNTENSHA_NAME_TO.ForeColor = System.Drawing.Color.Black;
            this.UNTENSHA_NAME_TO.IsInputErrorOccured = false;
            this.UNTENSHA_NAME_TO.ItemDefinedTypes = "varchar";
            this.UNTENSHA_NAME_TO.Location = new System.Drawing.Point(365, 24);
            this.UNTENSHA_NAME_TO.MaxLength = 0;
            this.UNTENSHA_NAME_TO.Name = "UNTENSHA_NAME_TO";
            this.UNTENSHA_NAME_TO.PopupAfterExecute = null;
            this.UNTENSHA_NAME_TO.PopupBeforeExecute = null;
            this.UNTENSHA_NAME_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNTENSHA_NAME_TO.PopupSearchSendParams")));
            this.UNTENSHA_NAME_TO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UNTENSHA_NAME_TO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNTENSHA_NAME_TO.popupWindowSetting")));
            this.UNTENSHA_NAME_TO.prevText = null;
            this.UNTENSHA_NAME_TO.ReadOnly = true;
            this.UNTENSHA_NAME_TO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNTENSHA_NAME_TO.RegistCheckMethod")));
            this.UNTENSHA_NAME_TO.Size = new System.Drawing.Size(117, 20);
            this.UNTENSHA_NAME_TO.TabIndex = 803;
            this.UNTENSHA_NAME_TO.TabStop = false;
            this.UNTENSHA_NAME_TO.Tag = " ";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(224, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 20);
            this.label2.TabIndex = 1022;
            this.label2.Text = "～";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.HIDUKE_FROM.DisplayItemName = "作業日開始日";
            this.HIDUKE_FROM.DisplayPopUp = null;
            this.HIDUKE_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_FROM.FocusOutCheckMethod")));
            this.HIDUKE_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HIDUKE_FROM.ForeColor = System.Drawing.Color.Black;
            this.HIDUKE_FROM.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.HIDUKE_FROM.IsInputErrorOccured = false;
            this.HIDUKE_FROM.Location = new System.Drawing.Point(115, 2);
            this.HIDUKE_FROM.MaxLength = 10;
            this.HIDUKE_FROM.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.HIDUKE_FROM.Name = "HIDUKE_FROM";
            this.HIDUKE_FROM.NullValue = "";
            this.HIDUKE_FROM.PopupAfterExecute = null;
            this.HIDUKE_FROM.PopupBeforeExecute = null;
            this.HIDUKE_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIDUKE_FROM.PopupSearchSendParams")));
            this.HIDUKE_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIDUKE_FROM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HIDUKE_FROM.popupWindowSetting")));
            this.HIDUKE_FROM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_FROM.RegistCheckMethod")));
            this.HIDUKE_FROM.Size = new System.Drawing.Size(108, 20);
            this.HIDUKE_FROM.TabIndex = 0;
            this.HIDUKE_FROM.Tag = "作業日(開始)を選択してください";
            this.HIDUKE_FROM.Text = "2013/12/10(火)";
            this.HIDUKE_FROM.Value = new System.DateTime(2013, 12, 10, 0, 0, 0, 0);
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
            this.HIDUKE_TO.DisplayItemName = "作業日終了日";
            this.HIDUKE_TO.DisplayPopUp = null;
            this.HIDUKE_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_TO.FocusOutCheckMethod")));
            this.HIDUKE_TO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HIDUKE_TO.ForeColor = System.Drawing.Color.Black;
            this.HIDUKE_TO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.HIDUKE_TO.IsInputErrorOccured = false;
            this.HIDUKE_TO.Location = new System.Drawing.Point(244, 2);
            this.HIDUKE_TO.MaxLength = 10;
            this.HIDUKE_TO.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.HIDUKE_TO.Name = "HIDUKE_TO";
            this.HIDUKE_TO.NullValue = "";
            this.HIDUKE_TO.PopupAfterExecute = null;
            this.HIDUKE_TO.PopupBeforeExecute = null;
            this.HIDUKE_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIDUKE_TO.PopupSearchSendParams")));
            this.HIDUKE_TO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIDUKE_TO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HIDUKE_TO.popupWindowSetting")));
            this.HIDUKE_TO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIDUKE_TO.RegistCheckMethod")));
            this.HIDUKE_TO.Size = new System.Drawing.Size(108, 20);
            this.HIDUKE_TO.TabIndex = 1;
            this.HIDUKE_TO.Tag = "作業日(終了)を選択してください";
            this.HIDUKE_TO.Text = "2013/12/10(火)";
            this.HIDUKE_TO.Value = new System.DateTime(2013, 12, 10, 0, 0, 0, 0);
            this.HIDUKE_TO.Leave += new System.EventHandler(this.HIDUKE_TO_Leave);
            // 
            // customDataGridView1
            // 
            this.customDataGridView1.AllowUserToAddRows = false;
            this.customDataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.customDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridView1.DefaultCellStyle = dataGridViewCellStyle8;
            this.customDataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.customDataGridView1.EnableHeadersVisualStyles = false;
            this.customDataGridView1.GridColor = System.Drawing.Color.White;
            this.customDataGridView1.IsReload = false;
            this.customDataGridView1.LinkedDataPanelName = null;
            this.customDataGridView1.Location = new System.Drawing.Point(12, 74);
            this.customDataGridView1.MultiSelect = false;
            this.customDataGridView1.Name = "customDataGridView1";
            this.customDataGridView1.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.customDataGridView1.RowHeadersVisible = false;
            this.customDataGridView1.RowTemplate.Height = 21;
            this.customDataGridView1.ShowCellToolTips = false;
            this.customDataGridView1.Size = new System.Drawing.Size(621, 365);
            this.customDataGridView1.TabIndex = 1024;
            this.customDataGridView1.Visible = false;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(645, 660);
            this.Controls.Add(this.customDataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.HIDUKE_FROM);
            this.Controls.Add(this.HIDUKE_TO);
            this.Controls.Add(this.UNTENSHA_NAME_TO);
            this.Controls.Add(this.UNTENSHA_NAME_FROM);
            this.Controls.Add(this.UNTENSHA_CD_TO);
            this.Controls.Add(this.UNTENSHA_LABEL);
            this.Controls.Add(this.UNTENSHA_CD_FROM);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_Kikan);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "UIForm";
            this.Text = "UIForm";
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Kikan;
        private System.Windows.Forms.Label label1;
        internal r_framework.CustomControl.CustomTextBox UNTENSHA_NAME_FROM;
        internal System.Windows.Forms.Label UNTENSHA_LABEL;
        internal r_framework.CustomControl.CustomAlphaNumTextBox UNTENSHA_CD_FROM;
        internal r_framework.CustomControl.CustomAlphaNumTextBox UNTENSHA_CD_TO;
        internal r_framework.CustomControl.CustomTextBox UNTENSHA_NAME_TO;
        private System.Windows.Forms.Label label2;
        public r_framework.CustomControl.CustomDateTimePicker HIDUKE_FROM;
        public r_framework.CustomControl.CustomDateTimePicker HIDUKE_TO;
        public r_framework.CustomControl.CustomDataGridView customDataGridView1;
    }
}