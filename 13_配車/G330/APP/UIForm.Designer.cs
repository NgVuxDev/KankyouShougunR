namespace Shougun.Core.Allocation.JissekiUriageShiharaiKakutei.APP
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
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            this.lbl_Kyoten = new System.Windows.Forms.Label();
            this.customPanel2 = new r_framework.CustomControl.CustomPanel();
            this.TORIHIKISAKI_NAME_custom = new r_framework.CustomControl.CustomTextBox();
            this.TORIHIKISAKI_CD_custom = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.fixConditionValue = new r_framework.CustomControl.CustomNumericTextBox2();
            this.unFixFlg = new r_framework.CustomControl.CustomRadioButton();
            this.fixedFlg = new r_framework.CustomControl.CustomRadioButton();
            this.txtBox_KyotenCd = new r_framework.CustomControl.CustomNumericTextBox2();
            this.lbl_Torihikisaki = new System.Windows.Forms.Label();
            this.customSortHeader1 = new r_framework.CustomControl.DataGridCustomControl.CustomSortHeader();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBox_KyotenNameRyaku = new r_framework.CustomControl.CustomTextBox();
            this.cmb_Shimebi = new r_framework.CustomControl.CustomComboBox();
            this.dtp_KikanTo = new r_framework.CustomControl.CustomDateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtp_KikanFrom = new r_framework.CustomControl.CustomDateTimePicker();
            this.lbl_Kikan = new System.Windows.Forms.Label();
            this.lbl_Kakutei = new System.Windows.Forms.Label();
            this.customPanel3 = new r_framework.CustomControl.CustomPanel();
            this.customDataGridView1 = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.CHECK_SELECT = new r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn();
            this.TEIKI_FLG = new r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn();
            this.TANKA_FLG = new r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn();
            this.TORIHIKISAKI_CD = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.TORIHIKISAKI_NAME_RYAKU = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.TORIHIKISAKI_FURIGANA = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.TorihikiKbnValue = new r_framework.CustomControl.CustomNumericTextBox2();
            this.KakeFlg = new r_framework.CustomControl.CustomRadioButton();
            this.GenkinFlg = new r_framework.CustomControl.CustomRadioButton();
            this.customPanel4 = new r_framework.CustomControl.CustomPanel();
            this.lbl_TorihikiKbn = new System.Windows.Forms.Label();
            this.customPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Kyoten
            // 
            this.lbl_Kyoten.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Kyoten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Kyoten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Kyoten.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Kyoten.ForeColor = System.Drawing.Color.White;
            this.lbl_Kyoten.Location = new System.Drawing.Point(0, 0);
            this.lbl_Kyoten.Name = "lbl_Kyoten";
            this.lbl_Kyoten.Size = new System.Drawing.Size(110, 20);
            this.lbl_Kyoten.TabIndex = 0;
            this.lbl_Kyoten.Text = "締日";
            this.lbl_Kyoten.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel2
            // 
            this.customPanel2.Controls.Add(this.TORIHIKISAKI_NAME_custom);
            this.customPanel2.Controls.Add(this.TORIHIKISAKI_CD_custom);
            this.customPanel2.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.customPanel2.Controls.Add(this.fixConditionValue);
            this.customPanel2.Controls.Add(this.unFixFlg);
            this.customPanel2.Controls.Add(this.fixedFlg);
            this.customPanel2.Controls.Add(this.txtBox_KyotenCd);
            this.customPanel2.Controls.Add(this.lbl_Torihikisaki);
            this.customPanel2.Controls.Add(this.customSortHeader1);
            this.customPanel2.Controls.Add(this.label2);
            this.customPanel2.Controls.Add(this.txtBox_KyotenNameRyaku);
            this.customPanel2.Controls.Add(this.cmb_Shimebi);
            this.customPanel2.Controls.Add(this.dtp_KikanTo);
            this.customPanel2.Controls.Add(this.label5);
            this.customPanel2.Controls.Add(this.dtp_KikanFrom);
            this.customPanel2.Controls.Add(this.lbl_Kikan);
            this.customPanel2.Controls.Add(this.lbl_Kakutei);
            this.customPanel2.Controls.Add(this.lbl_Kyoten);
            this.customPanel2.Controls.Add(this.customPanel3);
            this.customPanel2.Location = new System.Drawing.Point(0, 22);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(953, 104);
            this.customPanel2.TabIndex = 1;
            // 
            // TORIHIKISAKI_NAME_custom
            // 
            this.TORIHIKISAKI_NAME_custom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.TORIHIKISAKI_NAME_custom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_NAME_custom.DBFieldsName = "";
            this.TORIHIKISAKI_NAME_custom.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_NAME_custom.DisplayItemName = "";
            this.TORIHIKISAKI_NAME_custom.DisplayPopUp = null;
            this.TORIHIKISAKI_NAME_custom.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_custom.FocusOutCheckMethod")));
            this.TORIHIKISAKI_NAME_custom.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TORIHIKISAKI_NAME_custom.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_NAME_custom.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TORIHIKISAKI_NAME_custom.IsInputErrorOccured = false;
            this.TORIHIKISAKI_NAME_custom.ItemDefinedTypes = "";
            this.TORIHIKISAKI_NAME_custom.Location = new System.Drawing.Point(162, 44);
            this.TORIHIKISAKI_NAME_custom.MaxLength = 0;
            this.TORIHIKISAKI_NAME_custom.Name = "TORIHIKISAKI_NAME_custom";
            this.TORIHIKISAKI_NAME_custom.PopupAfterExecute = null;
            this.TORIHIKISAKI_NAME_custom.PopupBeforeExecute = null;
            this.TORIHIKISAKI_NAME_custom.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_NAME_custom.PopupSearchSendParams")));
            this.TORIHIKISAKI_NAME_custom.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_NAME_custom.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_NAME_custom.popupWindowSetting")));
            this.TORIHIKISAKI_NAME_custom.ReadOnly = true;
            this.TORIHIKISAKI_NAME_custom.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_custom.RegistCheckMethod")));
            this.TORIHIKISAKI_NAME_custom.Size = new System.Drawing.Size(287, 20);
            this.TORIHIKISAKI_NAME_custom.TabIndex = 18;
            this.TORIHIKISAKI_NAME_custom.TabStop = false;
            this.TORIHIKISAKI_NAME_custom.Tag = " ";
            // 
            // TORIHIKISAKI_CD_custom
            // 
            this.TORIHIKISAKI_CD_custom.BackColor = System.Drawing.SystemColors.Window;
            this.TORIHIKISAKI_CD_custom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_CD_custom.ChangeUpperCase = true;
            this.TORIHIKISAKI_CD_custom.CharacterLimitList = null;
            this.TORIHIKISAKI_CD_custom.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.TORIHIKISAKI_CD_custom.DBFieldsName = "";
            this.TORIHIKISAKI_CD_custom.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_CD_custom.DisplayItemName = "取引先CD";
            this.TORIHIKISAKI_CD_custom.DisplayPopUp = null;
            this.TORIHIKISAKI_CD_custom.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD_custom.FocusOutCheckMethod")));
            this.TORIHIKISAKI_CD_custom.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TORIHIKISAKI_CD_custom.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_CD_custom.GetCodeMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD_custom.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TORIHIKISAKI_CD_custom.IsInputErrorOccured = false;
            this.TORIHIKISAKI_CD_custom.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_CD_custom.Location = new System.Drawing.Point(113, 44);
            this.TORIHIKISAKI_CD_custom.MaxLength = 6;
            this.TORIHIKISAKI_CD_custom.Name = "TORIHIKISAKI_CD_custom";
            this.TORIHIKISAKI_CD_custom.PopupAfterExecute = null;
            this.TORIHIKISAKI_CD_custom.PopupAfterExecuteMethod = "";
            this.TORIHIKISAKI_CD_custom.PopupBeforeExecute = null;
            this.TORIHIKISAKI_CD_custom.PopupGetMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD_custom.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_CD_custom.PopupSearchSendParams")));
            this.TORIHIKISAKI_CD_custom.PopupSendParams = new string[] {
        "cmb_Shimebi"};
            this.TORIHIKISAKI_CD_custom.PopupSetFormField = "TORIHIKISAKI_CD_custom,TORIHIKISAKI_NAME_custom";
            this.TORIHIKISAKI_CD_custom.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TORIHIKISAKI_CD_custom.PopupWindowName = "検索共通ポップアップ";
            this.TORIHIKISAKI_CD_custom.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_CD_custom.popupWindowSetting")));
            this.TORIHIKISAKI_CD_custom.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD_custom.RegistCheckMethod")));
            this.TORIHIKISAKI_CD_custom.SetFormField = "TORIHIKISAKI_CD_custom,TORIHIKISAKI_NAME_custom";
            this.TORIHIKISAKI_CD_custom.Size = new System.Drawing.Size(50, 20);
            this.TORIHIKISAKI_CD_custom.TabIndex = 17;
            this.TORIHIKISAKI_CD_custom.Tag = "取引先を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.TORIHIKISAKI_CD_custom.ZeroPaddengFlag = true;
            this.TORIHIKISAKI_CD_custom.Validated += new System.EventHandler(this.TORIHIKISAKI_CD_custom_Validated);
            // 
            // ISNOT_NEED_DELETE_FLG
            // 
            this.ISNOT_NEED_DELETE_FLG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ISNOT_NEED_DELETE_FLG.DBFieldsName = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.ISNOT_NEED_DELETE_FLG.DisplayPopUp = null;
            this.ISNOT_NEED_DELETE_FLG.Enabled = false;
            this.ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ISNOT_NEED_DELETE_FLG.ForeColor = System.Drawing.Color.Black;
            this.ISNOT_NEED_DELETE_FLG.IsInputErrorOccured = false;
            this.ISNOT_NEED_DELETE_FLG.ItemDefinedTypes = "bit";
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(919, 1);
            this.ISNOT_NEED_DELETE_FLG.Name = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.PopupAfterExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupBeforeExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.PopupSearchSendParams")));
            this.ISNOT_NEED_DELETE_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ISNOT_NEED_DELETE_FLG.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.popupWindowSetting")));
            this.ISNOT_NEED_DELETE_FLG.ReadOnly = true;
            this.ISNOT_NEED_DELETE_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.RegistCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.Size = new System.Drawing.Size(20, 20);
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 20028;
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // fixConditionValue
            // 
            this.fixConditionValue.BackColor = System.Drawing.SystemColors.Window;
            this.fixConditionValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fixConditionValue.DefaultBackColor = System.Drawing.Color.Empty;
            this.fixConditionValue.DisplayPopUp = null;
            this.fixConditionValue.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("fixConditionValue.FocusOutCheckMethod")));
            this.fixConditionValue.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.fixConditionValue.ForeColor = System.Drawing.Color.Black;
            this.fixConditionValue.IsInputErrorOccured = false;
            this.fixConditionValue.LinkedRadioButtonArray = new string[] {
        "fixedFlg",
        "unFixFlg"};
            this.fixConditionValue.Location = new System.Drawing.Point(237, 0);
            this.fixConditionValue.Name = "fixConditionValue";
            this.fixConditionValue.PopupAfterExecute = null;
            this.fixConditionValue.PopupBeforeExecute = null;
            this.fixConditionValue.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("fixConditionValue.PopupSearchSendParams")));
            this.fixConditionValue.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.fixConditionValue.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("fixConditionValue.popupWindowSetting")));
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
            this.fixConditionValue.RangeSetting = rangeSettingDto1;
            this.fixConditionValue.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("fixConditionValue.RegistCheckMethod")));
            this.fixConditionValue.Size = new System.Drawing.Size(21, 20);
            this.fixConditionValue.TabIndex = 4;
            this.fixConditionValue.Tag = "【1、2】のいずれかで入力してください　1（確定済みのキャンセルができます）、2（確定処理ができます）";
            this.fixConditionValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.fixConditionValue.WordWrap = false;
            // 
            // unFixFlg
            // 
            this.unFixFlg.AutoSize = true;
            this.unFixFlg.DefaultBackColor = System.Drawing.Color.Empty;
            this.unFixFlg.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("unFixFlg.FocusOutCheckMethod")));
            this.unFixFlg.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.unFixFlg.LinkedTextBox = "fixConditionValue";
            this.unFixFlg.Location = new System.Drawing.Point(357, 1);
            this.unFixFlg.Name = "unFixFlg";
            this.unFixFlg.PopupAfterExecute = null;
            this.unFixFlg.PopupBeforeExecute = null;
            this.unFixFlg.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("unFixFlg.PopupSearchSendParams")));
            this.unFixFlg.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.unFixFlg.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("unFixFlg.popupWindowSetting")));
            this.unFixFlg.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("unFixFlg.RegistCheckMethod")));
            this.unFixFlg.Size = new System.Drawing.Size(81, 17);
            this.unFixFlg.TabIndex = 6;
            this.unFixFlg.Text = "2.未確定";
            this.unFixFlg.UseVisualStyleBackColor = true;
            this.unFixFlg.Value = "2";
            // 
            // fixedFlg
            // 
            this.fixedFlg.AutoSize = true;
            this.fixedFlg.DefaultBackColor = System.Drawing.Color.Empty;
            this.fixedFlg.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("fixedFlg.FocusOutCheckMethod")));
            this.fixedFlg.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.fixedFlg.LinkedTextBox = "fixConditionValue";
            this.fixedFlg.Location = new System.Drawing.Point(260, 1);
            this.fixedFlg.Name = "fixedFlg";
            this.fixedFlg.PopupAfterExecute = null;
            this.fixedFlg.PopupBeforeExecute = null;
            this.fixedFlg.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("fixedFlg.PopupSearchSendParams")));
            this.fixedFlg.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.fixedFlg.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("fixedFlg.popupWindowSetting")));
            this.fixedFlg.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("fixedFlg.RegistCheckMethod")));
            this.fixedFlg.Size = new System.Drawing.Size(95, 17);
            this.fixedFlg.TabIndex = 5;
            this.fixedFlg.Text = "1.確定済み";
            this.fixedFlg.UseVisualStyleBackColor = true;
            this.fixedFlg.Value = "1";
            // 
            // txtBox_KyotenCd
            // 
            this.txtBox_KyotenCd.BackColor = System.Drawing.SystemColors.Window;
            this.txtBox_KyotenCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBox_KyotenCd.CustomFormatSetting = "00";
            this.txtBox_KyotenCd.DBFieldsName = "KYOTEN_CD";
            this.txtBox_KyotenCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtBox_KyotenCd.DisplayItemName = "拠点CD";
            this.txtBox_KyotenCd.DisplayPopUp = null;
            this.txtBox_KyotenCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtBox_KyotenCd.FocusOutCheckMethod")));
            this.txtBox_KyotenCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtBox_KyotenCd.ForeColor = System.Drawing.Color.Black;
            this.txtBox_KyotenCd.FormatSetting = "カスタム";
            this.txtBox_KyotenCd.GetCodeMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.txtBox_KyotenCd.IsInputErrorOccured = false;
            this.txtBox_KyotenCd.ItemDefinedTypes = "smallint";
            this.txtBox_KyotenCd.Location = new System.Drawing.Point(566, 0);
            this.txtBox_KyotenCd.Name = "txtBox_KyotenCd";
            this.txtBox_KyotenCd.PopupAfterExecute = null;
            this.txtBox_KyotenCd.PopupBeforeExecute = null;
            this.txtBox_KyotenCd.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.txtBox_KyotenCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtBox_KyotenCd.PopupSearchSendParams")));
            this.txtBox_KyotenCd.PopupSetFormField = "txtBox_KyotenCd,txtBox_KyotenNameRyaku";
            this.txtBox_KyotenCd.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.txtBox_KyotenCd.PopupWindowName = "マスタ共通ポップアップ";
            this.txtBox_KyotenCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtBox_KyotenCd.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.txtBox_KyotenCd.RangeSetting = rangeSettingDto2;
            this.txtBox_KyotenCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtBox_KyotenCd.RegistCheckMethod")));
            this.txtBox_KyotenCd.SetFormField = "txtBox_KyotenCd,txtBox_KyotenNameRyaku";
            this.txtBox_KyotenCd.Size = new System.Drawing.Size(64, 20);
            this.txtBox_KyotenCd.TabIndex = 8;
            this.txtBox_KyotenCd.Tag = "拠点を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.txtBox_KyotenCd.WordWrap = false;
            this.txtBox_KyotenCd.Validated += new System.EventHandler(this.txtBox_KyotenCd_Validated);
            // 
            // lbl_Torihikisaki
            // 
            this.lbl_Torihikisaki.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Torihikisaki.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Torihikisaki.ForeColor = System.Drawing.Color.White;
            this.lbl_Torihikisaki.Location = new System.Drawing.Point(0, 44);
            this.lbl_Torihikisaki.Name = "lbl_Torihikisaki";
            this.lbl_Torihikisaki.Size = new System.Drawing.Size(110, 20);
            this.lbl_Torihikisaki.TabIndex = 16;
            this.lbl_Torihikisaki.Text = "取引先";
            this.lbl_Torihikisaki.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.customSortHeader1.LinkedDataGridViewName = "customDataGridView1";
            this.customSortHeader1.Location = new System.Drawing.Point(0, 75);
            this.customSortHeader1.Name = "customSortHeader1";
            this.customSortHeader1.Size = new System.Drawing.Size(941, 26);
            this.customSortHeader1.SortFlag = false;
            this.customSortHeader1.TabIndex = 3;
            this.customSortHeader1.TabStop = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(453, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "拠点";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBox_KyotenNameRyaku
            // 
            this.txtBox_KyotenNameRyaku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtBox_KyotenNameRyaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBox_KyotenNameRyaku.DBFieldsName = "KYOTEN_NAME_RYAKU";
            this.txtBox_KyotenNameRyaku.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtBox_KyotenNameRyaku.DisplayPopUp = null;
            this.txtBox_KyotenNameRyaku.FocusOutCheckMethod = null;
            this.txtBox_KyotenNameRyaku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtBox_KyotenNameRyaku.ForeColor = System.Drawing.Color.Black;
            this.txtBox_KyotenNameRyaku.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtBox_KyotenNameRyaku.IsInputErrorOccured = false;
            this.txtBox_KyotenNameRyaku.Location = new System.Drawing.Point(629, 0);
            this.txtBox_KyotenNameRyaku.MaxLength = 0;
            this.txtBox_KyotenNameRyaku.Name = "txtBox_KyotenNameRyaku";
            this.txtBox_KyotenNameRyaku.PopupAfterExecute = null;
            this.txtBox_KyotenNameRyaku.PopupBeforeExecute = null;
            this.txtBox_KyotenNameRyaku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtBox_KyotenNameRyaku.PopupSearchSendParams")));
            this.txtBox_KyotenNameRyaku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtBox_KyotenNameRyaku.popupWindowSetting = null;
            this.txtBox_KyotenNameRyaku.ReadOnly = true;
            this.txtBox_KyotenNameRyaku.RegistCheckMethod = null;
            this.txtBox_KyotenNameRyaku.Size = new System.Drawing.Size(310, 20);
            this.txtBox_KyotenNameRyaku.TabIndex = 9;
            this.txtBox_KyotenNameRyaku.TabStop = false;
            // 
            // cmb_Shimebi
            // 
            this.cmb_Shimebi.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmb_Shimebi.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmb_Shimebi.BackColor = System.Drawing.SystemColors.Window;
            this.cmb_Shimebi.DefaultBackColor = System.Drawing.Color.Empty;
            this.cmb_Shimebi.DisplayPopUp = null;
            this.cmb_Shimebi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Shimebi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cmb_Shimebi.FocusOutCheckMethod")));
            this.cmb_Shimebi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmb_Shimebi.IsInputErrorOccured = false;
            this.cmb_Shimebi.Items.AddRange(new object[] {
            " ",
            "0",
            "5",
            "10",
            "15",
            "20",
            "25",
            "31"});
            this.cmb_Shimebi.Location = new System.Drawing.Point(113, 0);
            this.cmb_Shimebi.Name = "cmb_Shimebi";
            this.cmb_Shimebi.PopupAfterExecute = null;
            this.cmb_Shimebi.PopupBeforeExecute = null;
            this.cmb_Shimebi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cmb_Shimebi.PopupSearchSendParams")));
            this.cmb_Shimebi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cmb_Shimebi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cmb_Shimebi.popupWindowSetting")));
            this.cmb_Shimebi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cmb_Shimebi.RegistCheckMethod")));
            this.cmb_Shimebi.Size = new System.Drawing.Size(50, 20);
            this.cmb_Shimebi.TabIndex = 2;
            this.cmb_Shimebi.Tag = "締日を選択してください";
            // 
            // dtp_KikanTo
            // 
            this.dtp_KikanTo.BackColor = System.Drawing.SystemColors.Window;
            this.dtp_KikanTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtp_KikanTo.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.dtp_KikanTo.Checked = false;
            this.dtp_KikanTo.CustomFormat = "yyyy/MM/dd(ddd)";
            this.dtp_KikanTo.DateTimeNowYear = "";
            this.dtp_KikanTo.DefaultBackColor = System.Drawing.Color.Empty;
            this.dtp_KikanTo.DisplayPopUp = null;
            this.dtp_KikanTo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtp_KikanTo.FocusOutCheckMethod")));
            this.dtp_KikanTo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.dtp_KikanTo.ForeColor = System.Drawing.Color.Black;
            this.dtp_KikanTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_KikanTo.IsInputErrorOccured = false;
            this.dtp_KikanTo.Location = new System.Drawing.Point(287, 22);
            this.dtp_KikanTo.MaxLength = 10;
            this.dtp_KikanTo.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_KikanTo.Name = "dtp_KikanTo";
            this.dtp_KikanTo.NullValue = "";
            this.dtp_KikanTo.PopupAfterExecute = null;
            this.dtp_KikanTo.PopupBeforeExecute = null;
            this.dtp_KikanTo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dtp_KikanTo.PopupSearchSendParams")));
            this.dtp_KikanTo.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dtp_KikanTo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dtp_KikanTo.popupWindowSetting")));
            this.dtp_KikanTo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtp_KikanTo.RegistCheckMethod")));
            this.dtp_KikanTo.Size = new System.Drawing.Size(135, 20);
            this.dtp_KikanTo.TabIndex = 13;
            this.dtp_KikanTo.Tag = "期間を入力してください";
            this.dtp_KikanTo.Text = "2013/12/05(木)";
            this.dtp_KikanTo.Value = new System.DateTime(2013, 12, 5, 0, 0, 0, 0);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(259, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "～";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtp_KikanFrom
            // 
            this.dtp_KikanFrom.BackColor = System.Drawing.SystemColors.Window;
            this.dtp_KikanFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtp_KikanFrom.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.dtp_KikanFrom.Checked = false;
            this.dtp_KikanFrom.CustomFormat = "yyyy/MM/dd(ddd)";
            this.dtp_KikanFrom.DateTimeNowYear = "";
            this.dtp_KikanFrom.DefaultBackColor = System.Drawing.Color.Empty;
            this.dtp_KikanFrom.DisplayPopUp = null;
            this.dtp_KikanFrom.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtp_KikanFrom.FocusOutCheckMethod")));
            this.dtp_KikanFrom.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.dtp_KikanFrom.ForeColor = System.Drawing.Color.Black;
            this.dtp_KikanFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_KikanFrom.IsInputErrorOccured = false;
            this.dtp_KikanFrom.Location = new System.Drawing.Point(113, 22);
            this.dtp_KikanFrom.MaxLength = 10;
            this.dtp_KikanFrom.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_KikanFrom.Name = "dtp_KikanFrom";
            this.dtp_KikanFrom.NullValue = "";
            this.dtp_KikanFrom.PopupAfterExecute = null;
            this.dtp_KikanFrom.PopupBeforeExecute = null;
            this.dtp_KikanFrom.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dtp_KikanFrom.PopupSearchSendParams")));
            this.dtp_KikanFrom.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dtp_KikanFrom.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dtp_KikanFrom.popupWindowSetting")));
            this.dtp_KikanFrom.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtp_KikanFrom.RegistCheckMethod")));
            this.dtp_KikanFrom.Size = new System.Drawing.Size(135, 20);
            this.dtp_KikanFrom.TabIndex = 11;
            this.dtp_KikanFrom.Tag = "期間を入力してください";
            this.dtp_KikanFrom.Text = "2013/12/05(木)";
            this.dtp_KikanFrom.Value = new System.DateTime(2013, 12, 5, 0, 0, 0, 0);
            // 
            // lbl_Kikan
            // 
            this.lbl_Kikan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Kikan.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Kikan.ForeColor = System.Drawing.Color.White;
            this.lbl_Kikan.Location = new System.Drawing.Point(0, 22);
            this.lbl_Kikan.Name = "lbl_Kikan";
            this.lbl_Kikan.Size = new System.Drawing.Size(110, 20);
            this.lbl_Kikan.TabIndex = 10;
            this.lbl_Kikan.Text = "期間※";
            this.lbl_Kikan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Kakutei
            // 
            this.lbl_Kakutei.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Kakutei.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Kakutei.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Kakutei.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Kakutei.ForeColor = System.Drawing.Color.White;
            this.lbl_Kakutei.Location = new System.Drawing.Point(168, 0);
            this.lbl_Kakutei.Name = "lbl_Kakutei";
            this.lbl_Kakutei.Size = new System.Drawing.Size(67, 20);
            this.lbl_Kakutei.TabIndex = 3;
            this.lbl_Kakutei.Text = "確定区分";
            this.lbl_Kakutei.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel3
            // 
            this.customPanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel3.Location = new System.Drawing.Point(237, 0);
            this.customPanel3.Name = "customPanel3";
            this.customPanel3.Size = new System.Drawing.Size(211, 20);
            this.customPanel3.TabIndex = 16;
            // 
            // customDataGridView1
            // 
            this.customDataGridView1.AllowUserToAddRows = false;
            this.customDataGridView1.AllowUserToDeleteRows = false;
            this.customDataGridView1.AllowUserToResizeRows = false;
            this.customDataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.customDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.customDataGridView1.ColumnHeadersHeight = 18;
            this.customDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.customDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CHECK_SELECT,
            this.TEIKI_FLG,
            this.TANKA_FLG,
            this.TORIHIKISAKI_CD,
            this.TORIHIKISAKI_NAME_RYAKU,
            this.TORIHIKISAKI_FURIGANA});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridView1.DefaultCellStyle = dataGridViewCellStyle8;
            this.customDataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.customDataGridView1.EnableHeadersVisualStyles = false;
            this.customDataGridView1.GridColor = System.Drawing.Color.White;
            this.customDataGridView1.IsReload = false;
            this.customDataGridView1.LinkedDataPanelName = "customSortHeader1";
            this.customDataGridView1.Location = new System.Drawing.Point(0, 128);
            this.customDataGridView1.MultiSelect = false;
            this.customDataGridView1.Name = "customDataGridView1";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.customDataGridView1.RowHeadersVisible = false;
            this.customDataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.customDataGridView1.RowTemplate.Height = 21;
            this.customDataGridView1.ShowCellToolTips = false;
            this.customDataGridView1.Size = new System.Drawing.Size(941, 318);
            this.customDataGridView1.TabIndex = 4;
            this.customDataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.customDataGridView1_CellMouseClick);
            this.customDataGridView1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.customDataGridView1_PreviewKeyDown);
            // 
            // CHECK_SELECT
            // 
            this.CHECK_SELECT.DataPropertyName = "CHECK_SELECT";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = System.Windows.Forms.CheckState.Indeterminate;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.CHECK_SELECT.DefaultCellStyle = dataGridViewCellStyle2;
            this.CHECK_SELECT.FalseValue = "0";
            this.CHECK_SELECT.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CHECK_SELECT.FocusOutCheckMethod")));
            this.CHECK_SELECT.HeaderText = "選択";
            this.CHECK_SELECT.MinimumWidth = 50;
            this.CHECK_SELECT.Name = "CHECK_SELECT";
            this.CHECK_SELECT.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CHECK_SELECT.RegistCheckMethod")));
            this.CHECK_SELECT.ToolTipText = "確定する明細を選択してください";
            this.CHECK_SELECT.TrueValue = "1";
            this.CHECK_SELECT.Width = 50;
            // 
            // TEIKI_FLG
            // 
            this.TEIKI_FLG.DataPropertyName = "TEIKI_FLG";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = false;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.TEIKI_FLG.DefaultCellStyle = dataGridViewCellStyle3;
            this.TEIKI_FLG.FalseValue = "0";
            this.TEIKI_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TEIKI_FLG.FocusOutCheckMethod")));
            this.TEIKI_FLG.HeaderText = "定期";
            this.TEIKI_FLG.MinimumWidth = 50;
            this.TEIKI_FLG.Name = "TEIKI_FLG";
            this.TEIKI_FLG.ReadOnly = true;
            this.TEIKI_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TEIKI_FLG.RegistCheckMethod")));
            this.TEIKI_FLG.TrueValue = "1";
            this.TEIKI_FLG.Width = 50;
            // 
            // TANKA_FLG
            // 
            this.TANKA_FLG.DataPropertyName = "TANKA_FLG";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.NullValue = false;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.TANKA_FLG.DefaultCellStyle = dataGridViewCellStyle4;
            this.TANKA_FLG.FalseValue = "0";
            this.TANKA_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TANKA_FLG.FocusOutCheckMethod")));
            this.TANKA_FLG.HeaderText = "単価";
            this.TANKA_FLG.MinimumWidth = 50;
            this.TANKA_FLG.Name = "TANKA_FLG";
            this.TANKA_FLG.ReadOnly = true;
            this.TANKA_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TANKA_FLG.RegistCheckMethod")));
            this.TANKA_FLG.TrueValue = "1";
            this.TANKA_FLG.Width = 50;
            // 
            // TORIHIKISAKI_CD
            // 
            this.TORIHIKISAKI_CD.DataPropertyName = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_CD.DefaultCellStyle = dataGridViewCellStyle5;
            this.TORIHIKISAKI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.FocusOutCheckMethod")));
            this.TORIHIKISAKI_CD.HeaderText = "取引先CD";
            this.TORIHIKISAKI_CD.MinimumWidth = 100;
            this.TORIHIKISAKI_CD.Name = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_CD.PopupSearchSendParams")));
            this.TORIHIKISAKI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_CD.popupWindowSetting")));
            this.TORIHIKISAKI_CD.ReadOnly = true;
            this.TORIHIKISAKI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.RegistCheckMethod")));
            this.TORIHIKISAKI_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TORIHIKISAKI_NAME_RYAKU
            // 
            this.TORIHIKISAKI_NAME_RYAKU.DataPropertyName = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_NAME_RYAKU.DefaultCellStyle = dataGridViewCellStyle6;
            this.TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU.HeaderText = "取引先名";
            this.TORIHIKISAKI_NAME_RYAKU.MinimumWidth = 300;
            this.TORIHIKISAKI_NAME_RYAKU.Name = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams")));
            this.TORIHIKISAKI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.popupWindowSetting")));
            this.TORIHIKISAKI_NAME_RYAKU.ReadOnly = true;
            this.TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TORIHIKISAKI_NAME_RYAKU.Width = 300;
            // 
            // TORIHIKISAKI_FURIGANA
            // 
            this.TORIHIKISAKI_FURIGANA.DataPropertyName = "TORIHIKISAKI_FURIGANA";
            this.TORIHIKISAKI_FURIGANA.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_FURIGANA.DefaultCellStyle = dataGridViewCellStyle7;
            this.TORIHIKISAKI_FURIGANA.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_FURIGANA.FocusOutCheckMethod")));
            this.TORIHIKISAKI_FURIGANA.HeaderText = "取引先フリガナ";
            this.TORIHIKISAKI_FURIGANA.MinimumWidth = 470;
            this.TORIHIKISAKI_FURIGANA.Name = "TORIHIKISAKI_FURIGANA";
            this.TORIHIKISAKI_FURIGANA.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_FURIGANA.PopupSearchSendParams")));
            this.TORIHIKISAKI_FURIGANA.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_FURIGANA.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_FURIGANA.popupWindowSetting")));
            this.TORIHIKISAKI_FURIGANA.ReadOnly = true;
            this.TORIHIKISAKI_FURIGANA.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_FURIGANA.RegistCheckMethod")));
            this.TORIHIKISAKI_FURIGANA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TORIHIKISAKI_FURIGANA.Width = 470;
            // 
            // TorihikiKbnValue
            // 
            this.TorihikiKbnValue.BackColor = System.Drawing.SystemColors.Window;
            this.TorihikiKbnValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TorihikiKbnValue.DefaultBackColor = System.Drawing.Color.Empty;
            this.TorihikiKbnValue.DisplayPopUp = null;
            this.TorihikiKbnValue.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TorihikiKbnValue.FocusOutCheckMethod")));
            this.TorihikiKbnValue.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TorihikiKbnValue.ForeColor = System.Drawing.Color.Black;
            this.TorihikiKbnValue.IsInputErrorOccured = false;
            this.TorihikiKbnValue.LinkedRadioButtonArray = new string[] {
        "GenkinFlg",
        "KakeFlg"};
            this.TorihikiKbnValue.Location = new System.Drawing.Point(113, 0);
            this.TorihikiKbnValue.Name = "TorihikiKbnValue";
            this.TorihikiKbnValue.PopupAfterExecute = null;
            this.TorihikiKbnValue.PopupBeforeExecute = null;
            this.TorihikiKbnValue.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TorihikiKbnValue.PopupSearchSendParams")));
            this.TorihikiKbnValue.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TorihikiKbnValue.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TorihikiKbnValue.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto3.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.TorihikiKbnValue.RangeSetting = rangeSettingDto3;
            this.TorihikiKbnValue.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TorihikiKbnValue.RegistCheckMethod")));
            this.TorihikiKbnValue.Size = new System.Drawing.Size(21, 20);
            this.TorihikiKbnValue.TabIndex = 1;
            this.TorihikiKbnValue.Tag = "【1、2】のいずれかで入力してください";
            this.TorihikiKbnValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TorihikiKbnValue.WordWrap = false;
            this.TorihikiKbnValue.TextChanged += new System.EventHandler(this.TorihikiKbnValue_TextChanged);
            // 
            // KakeFlg
            // 
            this.KakeFlg.AutoSize = true;
            this.KakeFlg.DefaultBackColor = System.Drawing.Color.Empty;
            this.KakeFlg.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KakeFlg.FocusOutCheckMethod")));
            this.KakeFlg.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KakeFlg.LinkedTextBox = "TorihikiKbnValue";
            this.KakeFlg.Location = new System.Drawing.Point(233, 1);
            this.KakeFlg.Name = "KakeFlg";
            this.KakeFlg.PopupAfterExecute = null;
            this.KakeFlg.PopupBeforeExecute = null;
            this.KakeFlg.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KakeFlg.PopupSearchSendParams")));
            this.KakeFlg.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KakeFlg.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KakeFlg.popupWindowSetting")));
            this.KakeFlg.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KakeFlg.RegistCheckMethod")));
            this.KakeFlg.Size = new System.Drawing.Size(67, 17);
            this.KakeFlg.TabIndex = 21;
            this.KakeFlg.Text = "2.掛け";
            this.KakeFlg.UseVisualStyleBackColor = true;
            this.KakeFlg.Value = "2";
            // 
            // GenkinFlg
            // 
            this.GenkinFlg.AutoSize = true;
            this.GenkinFlg.DefaultBackColor = System.Drawing.Color.Empty;
            this.GenkinFlg.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GenkinFlg.FocusOutCheckMethod")));
            this.GenkinFlg.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GenkinFlg.LinkedTextBox = "TorihikiKbnValue";
            this.GenkinFlg.Location = new System.Drawing.Point(136, 1);
            this.GenkinFlg.Name = "GenkinFlg";
            this.GenkinFlg.PopupAfterExecute = null;
            this.GenkinFlg.PopupBeforeExecute = null;
            this.GenkinFlg.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GenkinFlg.PopupSearchSendParams")));
            this.GenkinFlg.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GenkinFlg.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GenkinFlg.popupWindowSetting")));
            this.GenkinFlg.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GenkinFlg.RegistCheckMethod")));
            this.GenkinFlg.Size = new System.Drawing.Size(67, 17);
            this.GenkinFlg.TabIndex = 20;
            this.GenkinFlg.Text = "1.現金";
            this.GenkinFlg.UseVisualStyleBackColor = true;
            this.GenkinFlg.Value = "1";
            // 
            // customPanel4
            // 
            this.customPanel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel4.Location = new System.Drawing.Point(113, 0);
            this.customPanel4.Name = "customPanel4";
            this.customPanel4.Size = new System.Drawing.Size(211, 20);
            this.customPanel4.TabIndex = 22;
            // 
            // lbl_TorihikiKbn
            // 
            this.lbl_TorihikiKbn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_TorihikiKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_TorihikiKbn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_TorihikiKbn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_TorihikiKbn.ForeColor = System.Drawing.Color.White;
            this.lbl_TorihikiKbn.Location = new System.Drawing.Point(0, 0);
            this.lbl_TorihikiKbn.Name = "lbl_TorihikiKbn";
            this.lbl_TorihikiKbn.Size = new System.Drawing.Size(110, 20);
            this.lbl_TorihikiKbn.TabIndex = 18;
            this.lbl_TorihikiKbn.Text = "取引区分";
            this.lbl_TorihikiKbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 450);
            this.Controls.Add(this.TorihikiKbnValue);
            this.Controls.Add(this.customDataGridView1);
            this.Controls.Add(this.KakeFlg);
            this.Controls.Add(this.customPanel2);
            this.Controls.Add(this.GenkinFlg);
            this.Controls.Add(this.customPanel4);
            this.Controls.Add(this.lbl_TorihikiKbn);
            this.Name = "UIForm";
            this.Text = "UIForm";
            this.customPanel2.ResumeLayout(false);
            this.customPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Kyoten;
        private r_framework.CustomControl.CustomPanel customPanel2;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label lbl_Kikan;
        internal r_framework.CustomControl.CustomDataGridView customDataGridView1;
        public System.Windows.Forms.Label lbl_Torihikisaki;
        internal r_framework.CustomControl.CustomDateTimePicker dtp_KikanTo;
        internal r_framework.CustomControl.CustomDateTimePicker dtp_KikanFrom;
        internal r_framework.CustomControl.DataGridCustomControl.CustomSortHeader customSortHeader1;
        internal r_framework.CustomControl.CustomComboBox cmb_Shimebi;
        internal System.Windows.Forms.Label label2;
        public r_framework.CustomControl.CustomTextBox txtBox_KyotenNameRyaku;
        internal r_framework.CustomControl.CustomNumericTextBox2 txtBox_KyotenCd;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn CHECK_SELECT;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn TEIKI_FLG;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn TANKA_FLG;
        private r_framework.CustomControl.DgvCustomTextBoxColumn TORIHIKISAKI_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn TORIHIKISAKI_NAME_RYAKU;
        private r_framework.CustomControl.DgvCustomTextBoxColumn TORIHIKISAKI_FURIGANA;
        private System.Windows.Forms.Label lbl_Kakutei;
        private r_framework.CustomControl.CustomPanel customPanel3;
        internal r_framework.CustomControl.CustomNumericTextBox2 fixConditionValue;
        internal r_framework.CustomControl.CustomRadioButton fixedFlg;
        internal r_framework.CustomControl.CustomRadioButton unFixFlg;
        internal r_framework.CustomControl.CustomNumericTextBox2 TorihikiKbnValue;
        internal r_framework.CustomControl.CustomRadioButton KakeFlg;
        internal r_framework.CustomControl.CustomRadioButton GenkinFlg;
        private r_framework.CustomControl.CustomPanel customPanel4;
        private System.Windows.Forms.Label lbl_TorihikiKbn;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
        internal r_framework.CustomControl.CustomTextBox TORIHIKISAKI_NAME_custom;
        public r_framework.CustomControl.CustomAlphaNumTextBox TORIHIKISAKI_CD_custom;
    }
}