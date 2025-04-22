namespace Shougun.Core.Adjustment.ShiharaiMeisaishoHakko
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto5 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto6 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto7 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto8 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto9 = new r_framework.Dto.RangeSettingDto();
            this.lblKyoten = new System.Windows.Forms.Label();
            this.txtKyotenMei = new r_framework.CustomControl.CustomTextBox();
            this.txtKyotenCd = new r_framework.CustomControl.CustomNumericTextBox2();
            this.lblShimebi = new System.Windows.Forms.Label();
            this.lblInsatsuJun = new System.Windows.Forms.Label();
            this.lblShiharaiPaper = new System.Windows.Forms.Label();
            this.lblShiharaiStyle = new System.Windows.Forms.Label();
            this.rdoShiharaiDataJisya = new r_framework.CustomControl.CustomRadioButton();
            this.rdoShiharaiDataSitei = new r_framework.CustomControl.CustomRadioButton();
            this.rdoJisya = new r_framework.CustomControl.CustomRadioButton();
            this.rdoCreateDataOfStyle = new r_framework.CustomControl.CustomRadioButton();
            this.rdoTangetsu = new r_framework.CustomControl.CustomRadioButton();
            this.rdoKurikoshi = new r_framework.CustomControl.CustomRadioButton();
            this.txtKensakuJouken = new r_framework.CustomControl.CustomTextBox();
            this.lblShiharaiMeisaisyoInsatsubi = new System.Windows.Forms.Label();
            this.rdoShimebi = new r_framework.CustomControl.CustomRadioButton();
            this.rdoHakkobi = new r_framework.CustomControl.CustomRadioButton();
            this.rdoNashiOfInsatsubi = new r_framework.CustomControl.CustomRadioButton();
            this.rdoShiteiOfInsatsubi = new r_framework.CustomControl.CustomRadioButton();
            this.dtpSiteiPrintHiduke = new r_framework.CustomControl.CustomDateTimePicker();
            this.dgvSeisanDenpyouItiran = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.colHakkou = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colHakkouzumi = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colDenpyoNumber = new r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn();
            this.colSeisanDate = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.colTorihikisakiCd = new r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn();
            this.colTorihikisakiName = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.colShimebi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colZenkaiKurikoshiGaku = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colShiharaiGaku = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colChouseiGaku = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKonkaiShiharaiGaku = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colShohizei = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKonkaiSeisanGaku = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colShiharaiYoteiBi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTimeStamp = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.rdoTorihikisakiCD = new r_framework.CustomControl.CustomRadioButton();
            this.rdoFurigana = new r_framework.CustomControl.CustomRadioButton();
            this.pnlInsatsuJun = new r_framework.CustomControl.CustomPanel();
            this.txtInsatsuJun = new r_framework.CustomControl.CustomNumericTextBox2();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.txtInsatsubi = new r_framework.CustomControl.CustomNumericTextBox2();
            this.radShiharaiPaper = new r_framework.CustomControl.CustomPanel();
            this.rdoSitei = new r_framework.CustomControl.CustomRadioButton();
            this.txtShiharaiPaper = new r_framework.CustomControl.CustomNumericTextBox2();
            this.customPanel4 = new r_framework.CustomControl.CustomPanel();
            this.txtShiharaiStyle = new r_framework.CustomControl.CustomNumericTextBox2();
            this.chkHakko = new r_framework.CustomControl.CustomCheckBox();
            this.cmbShimebi = new r_framework.CustomControl.CustomComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.customPanel6 = new r_framework.CustomControl.CustomPanel();
            this.txtShiharaiHakkou = new r_framework.CustomControl.CustomNumericTextBox2();
            this.rdoSuru = new r_framework.CustomControl.CustomRadioButton();
            this.rdoShinai = new r_framework.CustomControl.CustomRadioButton();
            this.lblHakkoKbn = new System.Windows.Forms.Label();
            this.customPanel2 = new r_framework.CustomControl.CustomPanel();
            this.rdoHakkoHakkozumi = new r_framework.CustomControl.CustomRadioButton();
            this.rdoHakkoMihakko = new r_framework.CustomControl.CustomRadioButton();
            this.rdoHakkoSubete = new r_framework.CustomControl.CustomRadioButton();
            this.txtHakkoKbn = new r_framework.CustomControl.CustomNumericTextBox2();
            this.TORIHIKISAKI_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.TORIHIKISAKI_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.TORIHIKISAKI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.TORIHIKISAKI_LABEL = new System.Windows.Forms.Label();
            this.customPanel8 = new r_framework.CustomControl.CustomPanel();
            this.FILTERING_DATA = new r_framework.CustomControl.CustomNumericTextBox2();
            this.crbIncludeOtherData = new r_framework.CustomControl.CustomRadioButton();
            this.crbPaymentOnly = new r_framework.CustomControl.CustomRadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.checkBoxAll_zumi = new r_framework.CustomControl.CustomCheckBox();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.ZERO_KINGAKU_TAISHOGAI = new r_framework.CustomControl.CustomCheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.customPanel5 = new r_framework.CustomControl.CustomPanel();
            this.HIKAE_OUTPUT_KBN_3 = new r_framework.CustomControl.CustomRadioButton();
            this.HIKAE_OUTPUT_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.HIKAE_OUTPUT_KBN_2 = new r_framework.CustomControl.CustomRadioButton();
            this.HIKAE_OUTPUT_KBN_1 = new r_framework.CustomControl.CustomRadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSeisanDenpyouItiran)).BeginInit();
            this.pnlInsatsuJun.SuspendLayout();
            this.customPanel1.SuspendLayout();
            this.radShiharaiPaper.SuspendLayout();
            this.customPanel4.SuspendLayout();
            this.customPanel6.SuspendLayout();
            this.customPanel2.SuspendLayout();
            this.customPanel8.SuspendLayout();
            this.customPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblKyoten
            // 
            this.lblKyoten.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblKyoten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKyoten.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblKyoten.ForeColor = System.Drawing.Color.White;
            this.lblKyoten.Location = new System.Drawing.Point(3, 3);
            this.lblKyoten.Name = "lblKyoten";
            this.lblKyoten.Size = new System.Drawing.Size(110, 20);
            this.lblKyoten.TabIndex = 1;
            this.lblKyoten.Text = "拠点";
            this.lblKyoten.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblKyoten.Visible = false;
            // 
            // txtKyotenMei
            // 
            this.txtKyotenMei.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtKyotenMei.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKyotenMei.CharactersNumber = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.txtKyotenMei.DBFieldsName = "";
            this.txtKyotenMei.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtKyotenMei.DisplayItemName = "検索条件";
            this.txtKyotenMei.DisplayPopUp = null;
            this.txtKyotenMei.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenMei.FocusOutCheckMethod")));
            this.txtKyotenMei.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtKyotenMei.ForeColor = System.Drawing.Color.Black;
            this.txtKyotenMei.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtKyotenMei.IsInputErrorOccured = false;
            this.txtKyotenMei.Location = new System.Drawing.Point(157, 3);
            this.txtKyotenMei.Name = "txtKyotenMei";
            this.txtKyotenMei.PopupAfterExecute = null;
            this.txtKyotenMei.PopupBeforeExecute = null;
            this.txtKyotenMei.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtKyotenMei.PopupSearchSendParams")));
            this.txtKyotenMei.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtKyotenMei.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtKyotenMei.popupWindowSetting")));
            this.txtKyotenMei.ReadOnly = true;
            this.txtKyotenMei.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenMei.RegistCheckMethod")));
            this.txtKyotenMei.Size = new System.Drawing.Size(195, 20);
            this.txtKyotenMei.TabIndex = 2;
            this.txtKyotenMei.TabStop = false;
            this.txtKyotenMei.Tag = "検索する文字を入力してください";
            this.txtKyotenMei.Visible = false;
            // 
            // txtKyotenCd
            // 
            this.txtKyotenCd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtKyotenCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKyotenCd.CustomFormatSetting = "00";
            this.txtKyotenCd.DBFieldsName = "KYOTEN_CD";
            this.txtKyotenCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtKyotenCd.DisplayItemName = "拠点CD";
            this.txtKyotenCd.DisplayPopUp = null;
            this.txtKyotenCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenCd.FocusOutCheckMethod")));
            this.txtKyotenCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtKyotenCd.ForeColor = System.Drawing.Color.Black;
            this.txtKyotenCd.FormatSetting = "カスタム";
            this.txtKyotenCd.GetCodeMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.txtKyotenCd.IsInputErrorOccured = false;
            this.txtKyotenCd.ItemDefinedTypes = "smallint";
            this.txtKyotenCd.Location = new System.Drawing.Point(119, 3);
            this.txtKyotenCd.Name = "txtKyotenCd";
            this.txtKyotenCd.PopupAfterExecute = null;
            this.txtKyotenCd.PopupBeforeExecute = null;
            this.txtKyotenCd.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.txtKyotenCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtKyotenCd.PopupSearchSendParams")));
            this.txtKyotenCd.PopupSetFormField = "txtKyotenCd,txtKyotenMei";
            this.txtKyotenCd.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.txtKyotenCd.PopupWindowName = "マスタ共通ポップアップ";
            this.txtKyotenCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtKyotenCd.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.txtKyotenCd.RangeSetting = rangeSettingDto1;
            this.txtKyotenCd.ReadOnly = true;
            this.txtKyotenCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKyotenCd.RegistCheckMethod")));
            this.txtKyotenCd.SetFormField = "txtKyotenCd,txtKyotenMei";
            this.txtKyotenCd.ShortItemName = "拠点CD";
            this.txtKyotenCd.Size = new System.Drawing.Size(39, 20);
            this.txtKyotenCd.TabIndex = 1;
            this.txtKyotenCd.TabStop = false;
            this.txtKyotenCd.Tag = "拠点を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.txtKyotenCd.Visible = false;
            this.txtKyotenCd.WordWrap = false;
            // 
            // lblShimebi
            // 
            this.lblShimebi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblShimebi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblShimebi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblShimebi.ForeColor = System.Drawing.Color.White;
            this.lblShimebi.Location = new System.Drawing.Point(3, 3);
            this.lblShimebi.Name = "lblShimebi";
            this.lblShimebi.Size = new System.Drawing.Size(110, 20);
            this.lblShimebi.TabIndex = 1;
            this.lblShimebi.Text = "締日";
            this.lblShimebi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInsatsuJun
            // 
            this.lblInsatsuJun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblInsatsuJun.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInsatsuJun.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblInsatsuJun.ForeColor = System.Drawing.Color.White;
            this.lblInsatsuJun.Location = new System.Drawing.Point(3, 94);
            this.lblInsatsuJun.Name = "lblInsatsuJun";
            this.lblInsatsuJun.Size = new System.Drawing.Size(110, 20);
            this.lblInsatsuJun.TabIndex = 1;
            this.lblInsatsuJun.Text = "印刷順※";
            this.lblInsatsuJun.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblShiharaiPaper
            // 
            this.lblShiharaiPaper.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblShiharaiPaper.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblShiharaiPaper.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblShiharaiPaper.ForeColor = System.Drawing.Color.White;
            this.lblShiharaiPaper.Location = new System.Drawing.Point(3, 48);
            this.lblShiharaiPaper.Name = "lblShiharaiPaper";
            this.lblShiharaiPaper.Size = new System.Drawing.Size(110, 20);
            this.lblShiharaiPaper.TabIndex = 1;
            this.lblShiharaiPaper.Text = "支払用紙※";
            this.lblShiharaiPaper.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblShiharaiStyle
            // 
            this.lblShiharaiStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblShiharaiStyle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblShiharaiStyle.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblShiharaiStyle.ForeColor = System.Drawing.Color.White;
            this.lblShiharaiStyle.Location = new System.Drawing.Point(3, 175);
            this.lblShiharaiStyle.Name = "lblShiharaiStyle";
            this.lblShiharaiStyle.Size = new System.Drawing.Size(110, 20);
            this.lblShiharaiStyle.TabIndex = 1;
            this.lblShiharaiStyle.Text = "支払形態※";
            this.lblShiharaiStyle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rdoShiharaiDataJisya
            // 
            this.rdoShiharaiDataJisya.AutoSize = true;
            this.rdoShiharaiDataJisya.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoShiharaiDataJisya.FocusOutCheckMethod = null;
            this.rdoShiharaiDataJisya.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoShiharaiDataJisya.LinkedTextBox = "txtShiharaiPaper";
            this.rdoShiharaiDataJisya.Location = new System.Drawing.Point(26, 0);
            this.rdoShiharaiDataJisya.Name = "rdoShiharaiDataJisya";
            this.rdoShiharaiDataJisya.PopupAfterExecute = null;
            this.rdoShiharaiDataJisya.PopupBeforeExecute = null;
            this.rdoShiharaiDataJisya.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoShiharaiDataJisya.PopupSearchSendParams")));
            this.rdoShiharaiDataJisya.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoShiharaiDataJisya.popupWindowSetting = null;
            this.rdoShiharaiDataJisya.RegistCheckMethod = null;
            this.rdoShiharaiDataJisya.Size = new System.Drawing.Size(186, 17);
            this.rdoShiharaiDataJisya.TabIndex = 5;
            this.rdoShiharaiDataJisya.Tag = "支払用紙が支払データ作成時/自社の場合にはチェックを付けてください";
            this.rdoShiharaiDataJisya.Text = "1.支払データ作成時/自社";
            this.rdoShiharaiDataJisya.UseVisualStyleBackColor = true;
            this.rdoShiharaiDataJisya.Value = "1";
            // 
            // rdoShiharaiDataSitei
            // 
            this.rdoShiharaiDataSitei.AutoSize = true;
            this.rdoShiharaiDataSitei.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoShiharaiDataSitei.FocusOutCheckMethod = null;
            this.rdoShiharaiDataSitei.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoShiharaiDataSitei.LinkedTextBox = "txtShiharaiPaper";
            this.rdoShiharaiDataSitei.Location = new System.Drawing.Point(208, 0);
            this.rdoShiharaiDataSitei.Name = "rdoShiharaiDataSitei";
            this.rdoShiharaiDataSitei.PopupAfterExecute = null;
            this.rdoShiharaiDataSitei.PopupBeforeExecute = null;
            this.rdoShiharaiDataSitei.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoShiharaiDataSitei.PopupSearchSendParams")));
            this.rdoShiharaiDataSitei.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoShiharaiDataSitei.popupWindowSetting = null;
            this.rdoShiharaiDataSitei.RegistCheckMethod = null;
            this.rdoShiharaiDataSitei.Size = new System.Drawing.Size(186, 17);
            this.rdoShiharaiDataSitei.TabIndex = 6;
            this.rdoShiharaiDataSitei.Tag = "支払用紙が支払データ作成時/指定の場合にはチェックを付けてください";
            this.rdoShiharaiDataSitei.Text = "2.支払データ作成時/指定";
            this.rdoShiharaiDataSitei.UseVisualStyleBackColor = true;
            this.rdoShiharaiDataSitei.Value = "2";
            // 
            // rdoJisya
            // 
            this.rdoJisya.AutoSize = true;
            this.rdoJisya.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoJisya.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoJisya.FocusOutCheckMethod")));
            this.rdoJisya.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoJisya.LinkedTextBox = "txtShiharaiPaper";
            this.rdoJisya.Location = new System.Drawing.Point(391, 0);
            this.rdoJisya.Name = "rdoJisya";
            this.rdoJisya.PopupAfterExecute = null;
            this.rdoJisya.PopupBeforeExecute = null;
            this.rdoJisya.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoJisya.PopupSearchSendParams")));
            this.rdoJisya.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoJisya.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoJisya.popupWindowSetting")));
            this.rdoJisya.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoJisya.RegistCheckMethod")));
            this.rdoJisya.Size = new System.Drawing.Size(67, 17);
            this.rdoJisya.TabIndex = 7;
            this.rdoJisya.Tag = "支払用紙が自社の場合にはチェックを付けてください";
            this.rdoJisya.Text = "3.自社";
            this.rdoJisya.UseVisualStyleBackColor = true;
            this.rdoJisya.Value = "3";
            // 
            // rdoCreateDataOfStyle
            // 
            this.rdoCreateDataOfStyle.AutoSize = true;
            this.rdoCreateDataOfStyle.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoCreateDataOfStyle.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoCreateDataOfStyle.FocusOutCheckMethod")));
            this.rdoCreateDataOfStyle.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoCreateDataOfStyle.LinkedTextBox = "txtShiharaiStyle";
            this.rdoCreateDataOfStyle.Location = new System.Drawing.Point(26, 0);
            this.rdoCreateDataOfStyle.Name = "rdoCreateDataOfStyle";
            this.rdoCreateDataOfStyle.PopupAfterExecute = null;
            this.rdoCreateDataOfStyle.PopupBeforeExecute = null;
            this.rdoCreateDataOfStyle.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoCreateDataOfStyle.PopupSearchSendParams")));
            this.rdoCreateDataOfStyle.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoCreateDataOfStyle.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoCreateDataOfStyle.popupWindowSetting")));
            this.rdoCreateDataOfStyle.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoCreateDataOfStyle.RegistCheckMethod")));
            this.rdoCreateDataOfStyle.Size = new System.Drawing.Size(151, 17);
            this.rdoCreateDataOfStyle.TabIndex = 9;
            this.rdoCreateDataOfStyle.Tag = "支払形態が支払データ作成時の場合にはチェックを付けてください";
            this.rdoCreateDataOfStyle.Text = "1.支払データ作成時";
            this.rdoCreateDataOfStyle.UseVisualStyleBackColor = true;
            this.rdoCreateDataOfStyle.Value = "1";
            // 
            // rdoTangetsu
            // 
            this.rdoTangetsu.AutoSize = true;
            this.rdoTangetsu.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoTangetsu.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoTangetsu.FocusOutCheckMethod")));
            this.rdoTangetsu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoTangetsu.LinkedTextBox = "txtShiharaiStyle";
            this.rdoTangetsu.Location = new System.Drawing.Point(184, 0);
            this.rdoTangetsu.Name = "rdoTangetsu";
            this.rdoTangetsu.PopupAfterExecute = null;
            this.rdoTangetsu.PopupBeforeExecute = null;
            this.rdoTangetsu.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoTangetsu.PopupSearchSendParams")));
            this.rdoTangetsu.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoTangetsu.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoTangetsu.popupWindowSetting")));
            this.rdoTangetsu.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoTangetsu.RegistCheckMethod")));
            this.rdoTangetsu.Size = new System.Drawing.Size(95, 17);
            this.rdoTangetsu.TabIndex = 10;
            this.rdoTangetsu.Tag = "支払形態が単月精算の場合にはチェックを付けてください";
            this.rdoTangetsu.Text = "2.単月精算";
            this.rdoTangetsu.UseVisualStyleBackColor = true;
            this.rdoTangetsu.Value = "2";
            // 
            // rdoKurikoshi
            // 
            this.rdoKurikoshi.AutoSize = true;
            this.rdoKurikoshi.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoKurikoshi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoKurikoshi.FocusOutCheckMethod")));
            this.rdoKurikoshi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoKurikoshi.LinkedTextBox = "txtShiharaiStyle";
            this.rdoKurikoshi.Location = new System.Drawing.Point(293, 0);
            this.rdoKurikoshi.Name = "rdoKurikoshi";
            this.rdoKurikoshi.PopupAfterExecute = null;
            this.rdoKurikoshi.PopupBeforeExecute = null;
            this.rdoKurikoshi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoKurikoshi.PopupSearchSendParams")));
            this.rdoKurikoshi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoKurikoshi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoKurikoshi.popupWindowSetting")));
            this.rdoKurikoshi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoKurikoshi.RegistCheckMethod")));
            this.rdoKurikoshi.Size = new System.Drawing.Size(95, 17);
            this.rdoKurikoshi.TabIndex = 11;
            this.rdoKurikoshi.Tag = "支払形態が繰越精算の場合にはチェックを付けてください";
            this.rdoKurikoshi.Text = "3.繰越精算";
            this.rdoKurikoshi.UseVisualStyleBackColor = true;
            this.rdoKurikoshi.Value = "3";
            // 
            // txtKensakuJouken
            // 
            this.txtKensakuJouken.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtKensakuJouken.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtKensakuJouken.DisplayPopUp = null;
            this.txtKensakuJouken.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKensakuJouken.FocusOutCheckMethod")));
            this.txtKensakuJouken.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtKensakuJouken.ForeColor = System.Drawing.Color.Black;
            this.txtKensakuJouken.IsInputErrorOccured = false;
            this.txtKensakuJouken.Location = new System.Drawing.Point(3, 69);
            this.txtKensakuJouken.Multiline = true;
            this.txtKensakuJouken.Name = "txtKensakuJouken";
            this.txtKensakuJouken.PopupAfterExecute = null;
            this.txtKensakuJouken.PopupBeforeExecute = null;
            this.txtKensakuJouken.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtKensakuJouken.PopupSearchSendParams")));
            this.txtKensakuJouken.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtKensakuJouken.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtKensakuJouken.popupWindowSetting")));
            this.txtKensakuJouken.ReadOnly = true;
            this.txtKensakuJouken.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKensakuJouken.RegistCheckMethod")));
            this.txtKensakuJouken.Size = new System.Drawing.Size(730, 125);
            this.txtKensakuJouken.TabIndex = 19;
            this.txtKensakuJouken.TabStop = false;
            this.txtKensakuJouken.Tag = "検索条件設定画面で設定した条件が表示されます";
            this.txtKensakuJouken.Visible = false;
            // 
            // lblShiharaiMeisaisyoInsatsubi
            // 
            this.lblShiharaiMeisaisyoInsatsubi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblShiharaiMeisaisyoInsatsubi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblShiharaiMeisaisyoInsatsubi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblShiharaiMeisaisyoInsatsubi.ForeColor = System.Drawing.Color.White;
            this.lblShiharaiMeisaisyoInsatsubi.Location = new System.Drawing.Point(3, 197);
            this.lblShiharaiMeisaisyoInsatsubi.Name = "lblShiharaiMeisaisyoInsatsubi";
            this.lblShiharaiMeisaisyoInsatsubi.Size = new System.Drawing.Size(110, 20);
            this.lblShiharaiMeisaisyoInsatsubi.TabIndex = 1;
            this.lblShiharaiMeisaisyoInsatsubi.Text = "支払年月日※";
            this.lblShiharaiMeisaisyoInsatsubi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rdoShimebi
            // 
            this.rdoShimebi.AutoSize = true;
            this.rdoShimebi.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoShimebi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoShimebi.FocusOutCheckMethod")));
            this.rdoShimebi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoShimebi.LinkedTextBox = "txtInsatsubi";
            this.rdoShimebi.Location = new System.Drawing.Point(26, 0);
            this.rdoShimebi.Name = "rdoShimebi";
            this.rdoShimebi.PopupAfterExecute = null;
            this.rdoShimebi.PopupBeforeExecute = null;
            this.rdoShimebi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoShimebi.PopupSearchSendParams")));
            this.rdoShimebi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoShimebi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoShimebi.popupWindowSetting")));
            this.rdoShimebi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoShimebi.RegistCheckMethod")));
            this.rdoShimebi.Size = new System.Drawing.Size(67, 17);
            this.rdoShimebi.TabIndex = 20;
            this.rdoShimebi.Tag = "支払明細書印刷日が締日の場合にはチェックを付けてください";
            this.rdoShimebi.Text = "1.締日";
            this.rdoShimebi.UseVisualStyleBackColor = true;
            this.rdoShimebi.Value = "1";
            // 
            // rdoHakkobi
            // 
            this.rdoHakkobi.AutoSize = true;
            this.rdoHakkobi.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoHakkobi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoHakkobi.FocusOutCheckMethod")));
            this.rdoHakkobi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoHakkobi.LinkedTextBox = "txtInsatsubi";
            this.rdoHakkobi.Location = new System.Drawing.Point(99, 0);
            this.rdoHakkobi.Name = "rdoHakkobi";
            this.rdoHakkobi.PopupAfterExecute = null;
            this.rdoHakkobi.PopupBeforeExecute = null;
            this.rdoHakkobi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoHakkobi.PopupSearchSendParams")));
            this.rdoHakkobi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoHakkobi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoHakkobi.popupWindowSetting")));
            this.rdoHakkobi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoHakkobi.RegistCheckMethod")));
            this.rdoHakkobi.Size = new System.Drawing.Size(81, 17);
            this.rdoHakkobi.TabIndex = 21;
            this.rdoHakkobi.Tag = "支払明細書印刷日が発行日の場合にはチェックを付けてください";
            this.rdoHakkobi.Text = "2.発行日";
            this.rdoHakkobi.UseVisualStyleBackColor = true;
            this.rdoHakkobi.Value = "2";
            // 
            // rdoNashiOfInsatsubi
            // 
            this.rdoNashiOfInsatsubi.AutoSize = true;
            this.rdoNashiOfInsatsubi.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoNashiOfInsatsubi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoNashiOfInsatsubi.FocusOutCheckMethod")));
            this.rdoNashiOfInsatsubi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoNashiOfInsatsubi.LinkedTextBox = "txtInsatsubi";
            this.rdoNashiOfInsatsubi.Location = new System.Drawing.Point(184, 0);
            this.rdoNashiOfInsatsubi.Name = "rdoNashiOfInsatsubi";
            this.rdoNashiOfInsatsubi.PopupAfterExecute = null;
            this.rdoNashiOfInsatsubi.PopupBeforeExecute = null;
            this.rdoNashiOfInsatsubi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoNashiOfInsatsubi.PopupSearchSendParams")));
            this.rdoNashiOfInsatsubi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoNashiOfInsatsubi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoNashiOfInsatsubi.popupWindowSetting")));
            this.rdoNashiOfInsatsubi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoNashiOfInsatsubi.RegistCheckMethod")));
            this.rdoNashiOfInsatsubi.Size = new System.Drawing.Size(67, 17);
            this.rdoNashiOfInsatsubi.TabIndex = 22;
            this.rdoNashiOfInsatsubi.Tag = "支払明細書印刷日が無しの場合にはチェックを付けてください";
            this.rdoNashiOfInsatsubi.Text = "3.無し";
            this.rdoNashiOfInsatsubi.UseVisualStyleBackColor = true;
            this.rdoNashiOfInsatsubi.Value = "3";
            // 
            // rdoShiteiOfInsatsubi
            // 
            this.rdoShiteiOfInsatsubi.AutoSize = true;
            this.rdoShiteiOfInsatsubi.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoShiteiOfInsatsubi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoShiteiOfInsatsubi.FocusOutCheckMethod")));
            this.rdoShiteiOfInsatsubi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoShiteiOfInsatsubi.LinkedTextBox = "txtInsatsubi";
            this.rdoShiteiOfInsatsubi.Location = new System.Drawing.Point(254, 0);
            this.rdoShiteiOfInsatsubi.Name = "rdoShiteiOfInsatsubi";
            this.rdoShiteiOfInsatsubi.PopupAfterExecute = null;
            this.rdoShiteiOfInsatsubi.PopupBeforeExecute = null;
            this.rdoShiteiOfInsatsubi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoShiteiOfInsatsubi.PopupSearchSendParams")));
            this.rdoShiteiOfInsatsubi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoShiteiOfInsatsubi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoShiteiOfInsatsubi.popupWindowSetting")));
            this.rdoShiteiOfInsatsubi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoShiteiOfInsatsubi.RegistCheckMethod")));
            this.rdoShiteiOfInsatsubi.Size = new System.Drawing.Size(67, 17);
            this.rdoShiteiOfInsatsubi.TabIndex = 23;
            this.rdoShiteiOfInsatsubi.Tag = "支払明細書印刷日が指定日の場合にはチェックを付けてください";
            this.rdoShiteiOfInsatsubi.Text = "4.指定";
            this.rdoShiteiOfInsatsubi.UseVisualStyleBackColor = true;
            this.rdoShiteiOfInsatsubi.Value = "4";
            // 
            // dtpSiteiPrintHiduke
            // 
            this.dtpSiteiPrintHiduke.BackColor = System.Drawing.SystemColors.Window;
            this.dtpSiteiPrintHiduke.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtpSiteiPrintHiduke.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.dtpSiteiPrintHiduke.Checked = false;
            this.dtpSiteiPrintHiduke.CustomFormat = "yyyy/MM/dd(ddd)";
            this.dtpSiteiPrintHiduke.DateTimeNowYear = "";
            this.dtpSiteiPrintHiduke.DefaultBackColor = System.Drawing.Color.Empty;
            this.dtpSiteiPrintHiduke.DisplayPopUp = null;
            this.dtpSiteiPrintHiduke.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtpSiteiPrintHiduke.FocusOutCheckMethod")));
            this.dtpSiteiPrintHiduke.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.dtpSiteiPrintHiduke.ForeColor = System.Drawing.Color.Black;
            this.dtpSiteiPrintHiduke.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSiteiPrintHiduke.IsInputErrorOccured = false;
            this.dtpSiteiPrintHiduke.Location = new System.Drawing.Point(449, 197);
            this.dtpSiteiPrintHiduke.MaxLength = 10;
            this.dtpSiteiPrintHiduke.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpSiteiPrintHiduke.Name = "dtpSiteiPrintHiduke";
            this.dtpSiteiPrintHiduke.NullValue = "";
            this.dtpSiteiPrintHiduke.PopupAfterExecute = null;
            this.dtpSiteiPrintHiduke.PopupBeforeExecute = null;
            this.dtpSiteiPrintHiduke.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dtpSiteiPrintHiduke.PopupSearchSendParams")));
            this.dtpSiteiPrintHiduke.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dtpSiteiPrintHiduke.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dtpSiteiPrintHiduke.popupWindowSetting")));
            this.dtpSiteiPrintHiduke.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtpSiteiPrintHiduke.RegistCheckMethod")));
            this.dtpSiteiPrintHiduke.Size = new System.Drawing.Size(138, 20);
            this.dtpSiteiPrintHiduke.TabIndex = 11;
            this.dtpSiteiPrintHiduke.Tag = "支払明細書印刷日付を入力してください";
            this.dtpSiteiPrintHiduke.Text = "2013/12/09(月)";
            this.dtpSiteiPrintHiduke.Value = new System.DateTime(2013, 12, 9, 0, 0, 0, 0);
            // 
            // dgvSeisanDenpyouItiran
            // 
            this.dgvSeisanDenpyouItiran.AllowUserToAddRows = false;
            this.dgvSeisanDenpyouItiran.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSeisanDenpyouItiran.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSeisanDenpyouItiran.ColumnHeadersHeight = 47;
            this.dgvSeisanDenpyouItiran.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colHakkou,
            this.colHakkouzumi,
            this.colDenpyoNumber,
            this.colSeisanDate,
            this.colTorihikisakiCd,
            this.colTorihikisakiName,
            this.colShimebi,
            this.colZenkaiKurikoshiGaku,
            this.colShiharaiGaku,
            this.colChouseiGaku,
            this.colKonkaiShiharaiGaku,
            this.colShohizei,
            this.colKonkaiSeisanGaku,
            this.colShiharaiYoteiBi,
            this.colTimeStamp});
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSeisanDenpyouItiran.DefaultCellStyle = dataGridViewCellStyle17;
            this.dgvSeisanDenpyouItiran.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvSeisanDenpyouItiran.EnableHeadersVisualStyles = false;
            this.dgvSeisanDenpyouItiran.GridColor = System.Drawing.Color.White;
            this.dgvSeisanDenpyouItiran.IsReload = false;
            this.dgvSeisanDenpyouItiran.LinkedDataPanelName = null;
            this.dgvSeisanDenpyouItiran.Location = new System.Drawing.Point(3, 220);
            this.dgvSeisanDenpyouItiran.MultiSelect = false;
            this.dgvSeisanDenpyouItiran.Name = "dgvSeisanDenpyouItiran";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle18.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSeisanDenpyouItiran.RowHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.dgvSeisanDenpyouItiran.RowHeadersVisible = false;
            this.dgvSeisanDenpyouItiran.RowTemplate.Height = 21;
            this.dgvSeisanDenpyouItiran.ShowCellToolTips = false;
            this.dgvSeisanDenpyouItiran.Size = new System.Drawing.Size(989, 246);
            this.dgvSeisanDenpyouItiran.TabIndex = 27;
            this.dgvSeisanDenpyouItiran.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SeisanDenpyouItiran_CellClick);
            this.dgvSeisanDenpyouItiran.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.SeisanDenpyouItiran_CellPainting);
            //this.dgvSeisanDenpyouItiran.Enter += new System.EventHandler(this.dgvSeisanDenpyouItiran_Enter);
            // 
            // colHakkou
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = false;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.colHakkou.DefaultCellStyle = dataGridViewCellStyle2;
            this.colHakkou.HeaderText = "発行";
            this.colHakkou.Name = "colHakkou";
            this.colHakkou.ToolTipText = "支払明細書を印刷する明細をチェックしてください";
            this.colHakkou.Width = 80;
            // 
            // colHakkouzumi
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = false;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.colHakkouzumi.DefaultCellStyle = dataGridViewCellStyle3;
            this.colHakkouzumi.HeaderText = "発行済ﾁｪｯｸ";
            this.colHakkouzumi.Name = "colHakkouzumi";
            // 
            // colDenpyoNumber
            // 
            this.colDenpyoNumber.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.colDenpyoNumber.DefaultCellStyle = dataGridViewCellStyle4;
            this.colDenpyoNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("colDenpyoNumber.FocusOutCheckMethod")));
            this.colDenpyoNumber.HeaderText = "伝票番号";
            this.colDenpyoNumber.Name = "colDenpyoNumber";
            this.colDenpyoNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("colDenpyoNumber.PopupSearchSendParams")));
            this.colDenpyoNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.colDenpyoNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("colDenpyoNumber.popupWindowSetting")));
            this.colDenpyoNumber.ReadOnly = true;
            this.colDenpyoNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("colDenpyoNumber.RegistCheckMethod")));
            this.colDenpyoNumber.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colDenpyoNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colSeisanDate
            // 
            this.colSeisanDate.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle5.Format = "d";
            dataGridViewCellStyle5.NullValue = null;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.colSeisanDate.DefaultCellStyle = dataGridViewCellStyle5;
            this.colSeisanDate.FocusOutCheckMethod = null;
            this.colSeisanDate.HeaderText = "精算日付";
            this.colSeisanDate.Name = "colSeisanDate";
            this.colSeisanDate.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("colSeisanDate.PopupSearchSendParams")));
            this.colSeisanDate.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.colSeisanDate.popupWindowSetting = null;
            this.colSeisanDate.ReadOnly = true;
            this.colSeisanDate.RegistCheckMethod = null;
            this.colSeisanDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colSeisanDate.Width = 90;
            // 
            // colTorihikisakiCd
            // 
            this.colTorihikisakiCd.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.colTorihikisakiCd.DefaultCellStyle = dataGridViewCellStyle6;
            this.colTorihikisakiCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("colTorihikisakiCd.FocusOutCheckMethod")));
            this.colTorihikisakiCd.HeaderText = "取引先CD";
            this.colTorihikisakiCd.Name = "colTorihikisakiCd";
            this.colTorihikisakiCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("colTorihikisakiCd.PopupSearchSendParams")));
            this.colTorihikisakiCd.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.colTorihikisakiCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("colTorihikisakiCd.popupWindowSetting")));
            this.colTorihikisakiCd.ReadOnly = true;
            this.colTorihikisakiCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("colTorihikisakiCd.RegistCheckMethod")));
            this.colTorihikisakiCd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colTorihikisakiName
            // 
            this.colTorihikisakiName.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.colTorihikisakiName.DefaultCellStyle = dataGridViewCellStyle7;
            this.colTorihikisakiName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("colTorihikisakiName.FocusOutCheckMethod")));
            this.colTorihikisakiName.HeaderText = "取引先名";
            this.colTorihikisakiName.Name = "colTorihikisakiName";
            this.colTorihikisakiName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("colTorihikisakiName.PopupSearchSendParams")));
            this.colTorihikisakiName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.colTorihikisakiName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("colTorihikisakiName.popupWindowSetting")));
            this.colTorihikisakiName.ReadOnly = true;
            this.colTorihikisakiName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("colTorihikisakiName.RegistCheckMethod")));
            this.colTorihikisakiName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTorihikisakiName.Width = 200;
            // 
            // colShimebi
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.colShimebi.DefaultCellStyle = dataGridViewCellStyle8;
            this.colShimebi.HeaderText = "締日";
            this.colShimebi.Name = "colShimebi";
            this.colShimebi.ReadOnly = true;
            this.colShimebi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colShimebi.Width = 60;
            // 
            // colZenkaiKurikoshiGaku
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "N0";
            dataGridViewCellStyle9.NullValue = null;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            this.colZenkaiKurikoshiGaku.DefaultCellStyle = dataGridViewCellStyle9;
            this.colZenkaiKurikoshiGaku.HeaderText = "前回繰越額";
            this.colZenkaiKurikoshiGaku.Name = "colZenkaiKurikoshiGaku";
            this.colZenkaiKurikoshiGaku.ReadOnly = true;
            this.colZenkaiKurikoshiGaku.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colZenkaiKurikoshiGaku.Width = 120;
            // 
            // colShiharaiGaku
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "N0";
            dataGridViewCellStyle10.NullValue = null;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            this.colShiharaiGaku.DefaultCellStyle = dataGridViewCellStyle10;
            this.colShiharaiGaku.HeaderText = "支払額";
            this.colShiharaiGaku.Name = "colShiharaiGaku";
            this.colShiharaiGaku.ReadOnly = true;
            this.colShiharaiGaku.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colShiharaiGaku.Width = 120;
            // 
            // colChouseiGaku
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.Format = "N0";
            dataGridViewCellStyle11.NullValue = null;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            this.colChouseiGaku.DefaultCellStyle = dataGridViewCellStyle11;
            this.colChouseiGaku.HeaderText = "調整額";
            this.colChouseiGaku.Name = "colChouseiGaku";
            this.colChouseiGaku.ReadOnly = true;
            this.colChouseiGaku.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colChouseiGaku.Width = 120;
            // 
            // colKonkaiShiharaiGaku
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "N0";
            dataGridViewCellStyle12.NullValue = null;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            this.colKonkaiShiharaiGaku.DefaultCellStyle = dataGridViewCellStyle12;
            this.colKonkaiShiharaiGaku.HeaderText = "今回支払金額";
            this.colKonkaiShiharaiGaku.Name = "colKonkaiShiharaiGaku";
            this.colKonkaiShiharaiGaku.ReadOnly = true;
            this.colKonkaiShiharaiGaku.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colKonkaiShiharaiGaku.Width = 120;
            // 
            // colShohizei
            // 
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.Format = "N0";
            dataGridViewCellStyle13.NullValue = null;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Black;
            this.colShohizei.DefaultCellStyle = dataGridViewCellStyle13;
            this.colShohizei.HeaderText = "消費税";
            this.colShohizei.Name = "colShohizei";
            this.colShohizei.ReadOnly = true;
            this.colShohizei.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colShohizei.Width = 120;
            // 
            // colKonkaiSeisanGaku
            // 
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle14.Format = "N0";
            dataGridViewCellStyle14.NullValue = null;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black;
            this.colKonkaiSeisanGaku.DefaultCellStyle = dataGridViewCellStyle14;
            this.colKonkaiSeisanGaku.HeaderText = "今回精算額";
            this.colKonkaiSeisanGaku.Name = "colKonkaiSeisanGaku";
            this.colKonkaiSeisanGaku.ReadOnly = true;
            this.colKonkaiSeisanGaku.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colKonkaiSeisanGaku.Width = 120;
            // 
            // colShiharaiYoteiBi
            // 
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.Black;
            this.colShiharaiYoteiBi.DefaultCellStyle = dataGridViewCellStyle15;
            this.colShiharaiYoteiBi.HeaderText = "支払予定日";
            this.colShiharaiYoteiBi.MinimumWidth = 8;
            this.colShiharaiYoteiBi.Name = "colShiharaiYoteiBi";
            this.colShiharaiYoteiBi.ReadOnly = true;
            this.colShiharaiYoteiBi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colShiharaiYoteiBi.Width = 120;
            // 
            // colTimeStamp
            // 
            this.colTimeStamp.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.Black;
            this.colTimeStamp.DefaultCellStyle = dataGridViewCellStyle16;
            this.colTimeStamp.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("colTimeStamp.FocusOutCheckMethod")));
            this.colTimeStamp.HeaderText = "タイムスタンプ";
            this.colTimeStamp.Name = "colTimeStamp";
            this.colTimeStamp.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("colTimeStamp.PopupSearchSendParams")));
            this.colTimeStamp.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.colTimeStamp.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("colTimeStamp.popupWindowSetting")));
            this.colTimeStamp.ReadOnly = true;
            this.colTimeStamp.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("colTimeStamp.RegistCheckMethod")));
            this.colTimeStamp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colTimeStamp.Visible = false;
            // 
            // rdoTorihikisakiCD
            // 
            this.rdoTorihikisakiCD.AutoSize = true;
            this.rdoTorihikisakiCD.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoTorihikisakiCD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoTorihikisakiCD.FocusOutCheckMethod")));
            this.rdoTorihikisakiCD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoTorihikisakiCD.LinkedTextBox = "txtInsatsuJun";
            this.rdoTorihikisakiCD.Location = new System.Drawing.Point(117, 0);
            this.rdoTorihikisakiCD.Name = "rdoTorihikisakiCD";
            this.rdoTorihikisakiCD.PopupAfterExecute = null;
            this.rdoTorihikisakiCD.PopupBeforeExecute = null;
            this.rdoTorihikisakiCD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoTorihikisakiCD.PopupSearchSendParams")));
            this.rdoTorihikisakiCD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoTorihikisakiCD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoTorihikisakiCD.popupWindowSetting")));
            this.rdoTorihikisakiCD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoTorihikisakiCD.RegistCheckMethod")));
            this.rdoTorihikisakiCD.Size = new System.Drawing.Size(95, 17);
            this.rdoTorihikisakiCD.TabIndex = 9;
            this.rdoTorihikisakiCD.Tag = "印刷順が取引先CDの場合にはチェックを付けてください";
            this.rdoTorihikisakiCD.Text = "2.取引先CD";
            this.rdoTorihikisakiCD.UseVisualStyleBackColor = true;
            this.rdoTorihikisakiCD.Value = "2";
            // 
            // rdoFurigana
            // 
            this.rdoFurigana.AutoSize = true;
            this.rdoFurigana.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoFurigana.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoFurigana.FocusOutCheckMethod")));
            this.rdoFurigana.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoFurigana.LinkedTextBox = "txtInsatsuJun";
            this.rdoFurigana.Location = new System.Drawing.Point(26, 0);
            this.rdoFurigana.Name = "rdoFurigana";
            this.rdoFurigana.PopupAfterExecute = null;
            this.rdoFurigana.PopupBeforeExecute = null;
            this.rdoFurigana.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoFurigana.PopupSearchSendParams")));
            this.rdoFurigana.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoFurigana.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoFurigana.popupWindowSetting")));
            this.rdoFurigana.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoFurigana.RegistCheckMethod")));
            this.rdoFurigana.Size = new System.Drawing.Size(95, 17);
            this.rdoFurigana.TabIndex = 8;
            this.rdoFurigana.Tag = "印刷順がフリガナの場合にはチェックを付けてください";
            this.rdoFurigana.Text = "1.フリガナ";
            this.rdoFurigana.UseVisualStyleBackColor = true;
            this.rdoFurigana.Value = "1";
            // 
            // pnlInsatsuJun
            // 
            this.pnlInsatsuJun.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInsatsuJun.Controls.Add(this.txtInsatsuJun);
            this.pnlInsatsuJun.Controls.Add(this.rdoTorihikisakiCD);
            this.pnlInsatsuJun.Controls.Add(this.rdoFurigana);
            this.pnlInsatsuJun.Location = new System.Drawing.Point(119, 94);
            this.pnlInsatsuJun.Name = "pnlInsatsuJun";
            this.pnlInsatsuJun.Size = new System.Drawing.Size(520, 20);
            this.pnlInsatsuJun.TabIndex = 7;
            // 
            // txtInsatsuJun
            // 
            this.txtInsatsuJun.BackColor = System.Drawing.SystemColors.Window;
            this.txtInsatsuJun.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInsatsuJun.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtInsatsuJun.DisplayPopUp = null;
            this.txtInsatsuJun.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtInsatsuJun.FocusOutCheckMethod")));
            this.txtInsatsuJun.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtInsatsuJun.ForeColor = System.Drawing.Color.Black;
            this.txtInsatsuJun.IsInputErrorOccured = false;
            this.txtInsatsuJun.LinkedRadioButtonArray = new string[] {
        "rdoFurigana",
        "rdoTorihikisakiCD"};
            this.txtInsatsuJun.Location = new System.Drawing.Point(-1, -1);
            this.txtInsatsuJun.Name = "txtInsatsuJun";
            this.txtInsatsuJun.PopupAfterExecute = null;
            this.txtInsatsuJun.PopupBeforeExecute = null;
            this.txtInsatsuJun.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtInsatsuJun.PopupSearchSendParams")));
            this.txtInsatsuJun.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtInsatsuJun.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtInsatsuJun.popupWindowSetting")));
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
            this.txtInsatsuJun.RangeSetting = rangeSettingDto2;
            this.txtInsatsuJun.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtInsatsuJun.RegistCheckMethod")));
            this.txtInsatsuJun.Size = new System.Drawing.Size(20, 20);
            this.txtInsatsuJun.TabIndex = 7;
            this.txtInsatsuJun.Tag = "【1、2】のいずれかで入力してください";
            this.txtInsatsuJun.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtInsatsuJun.WordWrap = false;
            // 
            // customPanel1
            // 
            this.customPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel1.Controls.Add(this.txtInsatsubi);
            this.customPanel1.Controls.Add(this.rdoShimebi);
            this.customPanel1.Controls.Add(this.rdoHakkobi);
            this.customPanel1.Controls.Add(this.rdoNashiOfInsatsubi);
            this.customPanel1.Controls.Add(this.rdoShiteiOfInsatsubi);
            this.customPanel1.Location = new System.Drawing.Point(119, 197);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(325, 20);
            this.customPanel1.TabIndex = 10;
            // 
            // txtInsatsubi
            // 
            this.txtInsatsubi.BackColor = System.Drawing.SystemColors.Window;
            this.txtInsatsubi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInsatsubi.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtInsatsubi.DisplayPopUp = null;
            this.txtInsatsubi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtInsatsubi.FocusOutCheckMethod")));
            this.txtInsatsubi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtInsatsubi.ForeColor = System.Drawing.Color.Black;
            this.txtInsatsubi.IsInputErrorOccured = false;
            this.txtInsatsubi.LinkedRadioButtonArray = new string[] {
        "rdoShimebi",
        "rdoHakkobi",
        "rdoNashiOfInsatsubi",
        "rdoShiteiOfInsatsubi"};
            this.txtInsatsubi.Location = new System.Drawing.Point(-1, -1);
            this.txtInsatsubi.Name = "txtInsatsubi";
            this.txtInsatsubi.PopupAfterExecute = null;
            this.txtInsatsubi.PopupBeforeExecute = null;
            this.txtInsatsubi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtInsatsubi.PopupSearchSendParams")));
            this.txtInsatsubi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtInsatsubi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtInsatsubi.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            4,
            0,
            0,
            0});
            rangeSettingDto3.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtInsatsubi.RangeSetting = rangeSettingDto3;
            this.txtInsatsubi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtInsatsubi.RegistCheckMethod")));
            this.txtInsatsubi.Size = new System.Drawing.Size(20, 20);
            this.txtInsatsubi.TabIndex = 10;
            this.txtInsatsubi.Tag = "【1～4】のいずれかで入力してください";
            this.txtInsatsubi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtInsatsubi.WordWrap = false;
            this.txtInsatsubi.TextChanged += new System.EventHandler(this.txtInsatsubi_TextChanged);
            // 
            // radShiharaiPaper
            // 
            this.radShiharaiPaper.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.radShiharaiPaper.Controls.Add(this.rdoSitei);
            this.radShiharaiPaper.Controls.Add(this.rdoJisya);
            this.radShiharaiPaper.Controls.Add(this.rdoShiharaiDataSitei);
            this.radShiharaiPaper.Controls.Add(this.rdoShiharaiDataJisya);
            this.radShiharaiPaper.Controls.Add(this.txtShiharaiPaper);
            this.radShiharaiPaper.Location = new System.Drawing.Point(119, 48);
            this.radShiharaiPaper.Name = "radShiharaiPaper";
            this.radShiharaiPaper.Size = new System.Drawing.Size(520, 20);
            this.radShiharaiPaper.TabIndex = 4;
            // 
            // rdoSitei
            // 
            this.rdoSitei.AutoSize = true;
            this.rdoSitei.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoSitei.FocusOutCheckMethod = null;
            this.rdoSitei.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoSitei.LinkedTextBox = "txtShiharaiPaper";
            this.rdoSitei.Location = new System.Drawing.Point(456, 0);
            this.rdoSitei.Name = "rdoSitei";
            this.rdoSitei.PopupAfterExecute = null;
            this.rdoSitei.PopupBeforeExecute = null;
            this.rdoSitei.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoSitei.PopupSearchSendParams")));
            this.rdoSitei.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoSitei.popupWindowSetting = null;
            this.rdoSitei.RegistCheckMethod = null;
            this.rdoSitei.Size = new System.Drawing.Size(67, 17);
            this.rdoSitei.TabIndex = 8;
            this.rdoSitei.Tag = "支払用紙が指定の場合にはチェックを付けてください";
            this.rdoSitei.Text = "4.指定";
            this.rdoSitei.UseVisualStyleBackColor = true;
            this.rdoSitei.Value = "4";
            // 
            // txtShiharaiPaper
            // 
            this.txtShiharaiPaper.BackColor = System.Drawing.SystemColors.Window;
            this.txtShiharaiPaper.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtShiharaiPaper.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtShiharaiPaper.DisplayPopUp = null;
            this.txtShiharaiPaper.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtShiharaiPaper.FocusOutCheckMethod")));
            this.txtShiharaiPaper.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtShiharaiPaper.ForeColor = System.Drawing.Color.Black;
            this.txtShiharaiPaper.IsInputErrorOccured = false;
            this.txtShiharaiPaper.LinkedRadioButtonArray = new string[] {
        "rdoShiharaiDataJisya",
        "rdoShiharaiDataSitei",
        "rdoJisya",
        "rdoSitei"};
            this.txtShiharaiPaper.Location = new System.Drawing.Point(-1, -1);
            this.txtShiharaiPaper.Name = "txtShiharaiPaper";
            this.txtShiharaiPaper.PopupAfterExecute = null;
            this.txtShiharaiPaper.PopupBeforeExecute = null;
            this.txtShiharaiPaper.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtShiharaiPaper.PopupSearchSendParams")));
            this.txtShiharaiPaper.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtShiharaiPaper.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtShiharaiPaper.popupWindowSetting")));
            rangeSettingDto4.Max = new decimal(new int[] {
            4,
            0,
            0,
            0});
            rangeSettingDto4.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtShiharaiPaper.RangeSetting = rangeSettingDto4;
            this.txtShiharaiPaper.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtShiharaiPaper.RegistCheckMethod")));
            this.txtShiharaiPaper.Size = new System.Drawing.Size(20, 20);
            this.txtShiharaiPaper.TabIndex = 4;
            this.txtShiharaiPaper.Tag = "【1～4】のいずれかで入力してください";
            this.txtShiharaiPaper.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtShiharaiPaper.WordWrap = false;
            // 
            // customPanel4
            // 
            this.customPanel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel4.Controls.Add(this.txtShiharaiStyle);
            this.customPanel4.Controls.Add(this.rdoCreateDataOfStyle);
            this.customPanel4.Controls.Add(this.rdoTangetsu);
            this.customPanel4.Controls.Add(this.rdoKurikoshi);
            this.customPanel4.Location = new System.Drawing.Point(119, 175);
            this.customPanel4.Name = "customPanel4";
            this.customPanel4.Size = new System.Drawing.Size(468, 20);
            this.customPanel4.TabIndex = 8;
            // 
            // txtShiharaiStyle
            // 
            this.txtShiharaiStyle.BackColor = System.Drawing.SystemColors.Window;
            this.txtShiharaiStyle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtShiharaiStyle.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtShiharaiStyle.DisplayPopUp = null;
            this.txtShiharaiStyle.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtShiharaiStyle.FocusOutCheckMethod")));
            this.txtShiharaiStyle.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtShiharaiStyle.ForeColor = System.Drawing.Color.Black;
            this.txtShiharaiStyle.IsInputErrorOccured = false;
            this.txtShiharaiStyle.LinkedRadioButtonArray = new string[] {
        "rdoCreateDataOfStyle",
        "rdoTangetsu",
        "rdoKurikoshi"};
            this.txtShiharaiStyle.Location = new System.Drawing.Point(-1, -1);
            this.txtShiharaiStyle.Name = "txtShiharaiStyle";
            this.txtShiharaiStyle.PopupAfterExecute = null;
            this.txtShiharaiStyle.PopupBeforeExecute = null;
            this.txtShiharaiStyle.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtShiharaiStyle.PopupSearchSendParams")));
            this.txtShiharaiStyle.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtShiharaiStyle.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtShiharaiStyle.popupWindowSetting")));
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
            this.txtShiharaiStyle.RangeSetting = rangeSettingDto5;
            this.txtShiharaiStyle.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtShiharaiStyle.RegistCheckMethod")));
            this.txtShiharaiStyle.Size = new System.Drawing.Size(20, 20);
            this.txtShiharaiStyle.TabIndex = 8;
            this.txtShiharaiStyle.Tag = "【1～3】のいずれかで入力してください";
            this.txtShiharaiStyle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtShiharaiStyle.WordWrap = false;
            // 
            // chkHakko
            // 
            this.chkHakko.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.chkHakko.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkHakko.DefaultBackColor = System.Drawing.Color.Empty;
            this.chkHakko.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("chkHakko.FocusOutCheckMethod")));
            this.chkHakko.ForeColor = System.Drawing.Color.White;
            this.chkHakko.Location = new System.Drawing.Point(62, 236);
            this.chkHakko.Name = "chkHakko";
            this.chkHakko.PopupAfterExecute = null;
            this.chkHakko.PopupBeforeExecute = null;
            this.chkHakko.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("chkHakko.PopupSearchSendParams")));
            this.chkHakko.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.chkHakko.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("chkHakko.popupWindowSetting")));
            this.chkHakko.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("chkHakko.RegistCheckMethod")));
            this.chkHakko.Size = new System.Drawing.Size(13, 14);
            this.chkHakko.TabIndex = 25;
            this.chkHakko.Tag = "全明細を選択／解除する場合、チェックしてください";
            this.chkHakko.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkHakko.UseVisualStyleBackColor = false;
            this.chkHakko.CheckedChanged += new System.EventHandler(this.checkBoxAll_CheckedChanged);
            // 
            // cmbShimebi
            // 
            this.cmbShimebi.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbShimebi.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbShimebi.BackColor = System.Drawing.SystemColors.Window;
            this.cmbShimebi.DefaultBackColor = System.Drawing.Color.Empty;
            this.cmbShimebi.DisplayPopUp = null;
            this.cmbShimebi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbShimebi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cmbShimebi.FocusOutCheckMethod")));
            this.cmbShimebi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbShimebi.FormattingEnabled = true;
            this.cmbShimebi.IsInputErrorOccured = false;
            this.cmbShimebi.Items.AddRange(new object[] {
            "",
            "0",
            "5",
            "10",
            "15",
            "20",
            "25",
            "31"});
            this.cmbShimebi.Location = new System.Drawing.Point(119, 3);
            this.cmbShimebi.Name = "cmbShimebi";
            this.cmbShimebi.PopupAfterExecute = null;
            this.cmbShimebi.PopupBeforeExecute = null;
            this.cmbShimebi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cmbShimebi.PopupSearchSendParams")));
            this.cmbShimebi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cmbShimebi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cmbShimebi.popupWindowSetting")));
            this.cmbShimebi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cmbShimebi.RegistCheckMethod")));
            this.cmbShimebi.Size = new System.Drawing.Size(39, 21);
            this.cmbShimebi.TabIndex = 3;
            this.cmbShimebi.Tag = "締日を入力してください";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(593, 197);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(140, 20);
            this.label8.TabIndex = 27;
            this.label8.Text = "支払明細書発行日※";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel6
            // 
            this.customPanel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel6.Controls.Add(this.txtShiharaiHakkou);
            this.customPanel6.Controls.Add(this.rdoSuru);
            this.customPanel6.Controls.Add(this.rdoShinai);
            this.customPanel6.Location = new System.Drawing.Point(739, 197);
            this.customPanel6.Name = "customPanel6";
            this.customPanel6.Size = new System.Drawing.Size(253, 20);
            this.customPanel6.TabIndex = 12;
            // 
            // txtShiharaiHakkou
            // 
            this.txtShiharaiHakkou.BackColor = System.Drawing.SystemColors.Window;
            this.txtShiharaiHakkou.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtShiharaiHakkou.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtShiharaiHakkou.DisplayPopUp = null;
            this.txtShiharaiHakkou.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtShiharaiHakkou.FocusOutCheckMethod")));
            this.txtShiharaiHakkou.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtShiharaiHakkou.ForeColor = System.Drawing.Color.Black;
            this.txtShiharaiHakkou.IsInputErrorOccured = false;
            this.txtShiharaiHakkou.LinkedRadioButtonArray = new string[] {
        "rdoSuru",
        "rdoShinai"};
            this.txtShiharaiHakkou.Location = new System.Drawing.Point(-1, -1);
            this.txtShiharaiHakkou.Name = "txtShiharaiHakkou";
            this.txtShiharaiHakkou.PopupAfterExecute = null;
            this.txtShiharaiHakkou.PopupBeforeExecute = null;
            this.txtShiharaiHakkou.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtShiharaiHakkou.PopupSearchSendParams")));
            this.txtShiharaiHakkou.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtShiharaiHakkou.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtShiharaiHakkou.popupWindowSetting")));
            rangeSettingDto6.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto6.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtShiharaiHakkou.RangeSetting = rangeSettingDto6;
            this.txtShiharaiHakkou.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtShiharaiHakkou.RegistCheckMethod")));
            this.txtShiharaiHakkou.Size = new System.Drawing.Size(20, 20);
            this.txtShiharaiHakkou.TabIndex = 12;
            this.txtShiharaiHakkou.Tag = "【1、2】のいずれかで入力してください";
            this.txtShiharaiHakkou.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtShiharaiHakkou.WordWrap = false;
            // 
            // rdoSuru
            // 
            this.rdoSuru.AutoSize = true;
            this.rdoSuru.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoSuru.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoSuru.FocusOutCheckMethod")));
            this.rdoSuru.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoSuru.LinkedTextBox = "txtShiharaiHakkou";
            this.rdoSuru.Location = new System.Drawing.Point(23, 0);
            this.rdoSuru.Name = "rdoSuru";
            this.rdoSuru.PopupAfterExecute = null;
            this.rdoSuru.PopupBeforeExecute = null;
            this.rdoSuru.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoSuru.PopupSearchSendParams")));
            this.rdoSuru.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoSuru.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoSuru.popupWindowSetting")));
            this.rdoSuru.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoSuru.RegistCheckMethod")));
            this.rdoSuru.Size = new System.Drawing.Size(95, 17);
            this.rdoSuru.TabIndex = 13;
            this.rdoSuru.Tag = "発行日を印字する場合にはチェックを付けてください";
            this.rdoSuru.Text = "1.印刷する";
            this.rdoSuru.UseVisualStyleBackColor = true;
            this.rdoSuru.Value = "1";
            // 
            // rdoShinai
            // 
            this.rdoShinai.AutoSize = true;
            this.rdoShinai.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoShinai.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoShinai.FocusOutCheckMethod")));
            this.rdoShinai.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoShinai.LinkedTextBox = "txtShiharaiHakkou";
            this.rdoShinai.Location = new System.Drawing.Point(124, 0);
            this.rdoShinai.Name = "rdoShinai";
            this.rdoShinai.PopupAfterExecute = null;
            this.rdoShinai.PopupBeforeExecute = null;
            this.rdoShinai.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoShinai.PopupSearchSendParams")));
            this.rdoShinai.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoShinai.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoShinai.popupWindowSetting")));
            this.rdoShinai.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoShinai.RegistCheckMethod")));
            this.rdoShinai.Size = new System.Drawing.Size(109, 17);
            this.rdoShinai.TabIndex = 14;
            this.rdoShinai.Tag = "発行日を印字しない場合にはチェックを付けてください";
            this.rdoShinai.Text = "2.印刷しない";
            this.rdoShinai.UseVisualStyleBackColor = true;
            this.rdoShinai.Value = "2";
            // 
            // lblHakkoKbn
            // 
            this.lblHakkoKbn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblHakkoKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHakkoKbn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblHakkoKbn.ForeColor = System.Drawing.Color.White;
            this.lblHakkoKbn.Location = new System.Drawing.Point(3, 71);
            this.lblHakkoKbn.Name = "lblHakkoKbn";
            this.lblHakkoKbn.Size = new System.Drawing.Size(110, 20);
            this.lblHakkoKbn.TabIndex = 1;
            this.lblHakkoKbn.Text = "発行区分※";
            this.lblHakkoKbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel2
            // 
            this.customPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel2.Controls.Add(this.rdoHakkoHakkozumi);
            this.customPanel2.Controls.Add(this.rdoHakkoMihakko);
            this.customPanel2.Controls.Add(this.rdoHakkoSubete);
            this.customPanel2.Controls.Add(this.txtHakkoKbn);
            this.customPanel2.Location = new System.Drawing.Point(119, 71);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(520, 20);
            this.customPanel2.TabIndex = 6;
            // 
            // rdoHakkoHakkozumi
            // 
            this.rdoHakkoHakkozumi.AutoSize = true;
            this.rdoHakkoHakkozumi.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoHakkoHakkozumi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoHakkoHakkozumi.FocusOutCheckMethod")));
            this.rdoHakkoHakkozumi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoHakkoHakkozumi.LinkedTextBox = "txtHakkoKbn";
            this.rdoHakkoHakkozumi.Location = new System.Drawing.Point(117, 0);
            this.rdoHakkoHakkozumi.Name = "rdoHakkoHakkozumi";
            this.rdoHakkoHakkozumi.PopupAfterExecute = null;
            this.rdoHakkoHakkozumi.PopupBeforeExecute = null;
            this.rdoHakkoHakkozumi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoHakkoHakkozumi.PopupSearchSendParams")));
            this.rdoHakkoHakkozumi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoHakkoHakkozumi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoHakkoHakkozumi.popupWindowSetting")));
            this.rdoHakkoHakkozumi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoHakkoHakkozumi.RegistCheckMethod")));
            this.rdoHakkoHakkozumi.Size = new System.Drawing.Size(81, 17);
            this.rdoHakkoHakkozumi.TabIndex = 8;
            this.rdoHakkoHakkozumi.Tag = "発行済のみ抽出したい場合にはチェックを付けてください";
            this.rdoHakkoHakkozumi.Text = "2.発行済";
            this.rdoHakkoHakkozumi.UseVisualStyleBackColor = true;
            this.rdoHakkoHakkozumi.Value = "2";
            // 
            // rdoHakkoMihakko
            // 
            this.rdoHakkoMihakko.AutoSize = true;
            this.rdoHakkoMihakko.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoHakkoMihakko.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoHakkoMihakko.FocusOutCheckMethod")));
            this.rdoHakkoMihakko.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoHakkoMihakko.LinkedTextBox = "txtHakkoKbn";
            this.rdoHakkoMihakko.Location = new System.Drawing.Point(26, 0);
            this.rdoHakkoMihakko.Name = "rdoHakkoMihakko";
            this.rdoHakkoMihakko.PopupAfterExecute = null;
            this.rdoHakkoMihakko.PopupBeforeExecute = null;
            this.rdoHakkoMihakko.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoHakkoMihakko.PopupSearchSendParams")));
            this.rdoHakkoMihakko.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoHakkoMihakko.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoHakkoMihakko.popupWindowSetting")));
            this.rdoHakkoMihakko.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoHakkoMihakko.RegistCheckMethod")));
            this.rdoHakkoMihakko.Size = new System.Drawing.Size(81, 17);
            this.rdoHakkoMihakko.TabIndex = 7;
            this.rdoHakkoMihakko.Tag = "未発行のみ抽出したい場合にはチェックを付けてください";
            this.rdoHakkoMihakko.Text = "1.未発行";
            this.rdoHakkoMihakko.UseVisualStyleBackColor = true;
            this.rdoHakkoMihakko.Value = "1";
            // 
            // rdoHakkoSubete
            // 
            this.rdoHakkoSubete.AutoSize = true;
            this.rdoHakkoSubete.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoHakkoSubete.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoHakkoSubete.FocusOutCheckMethod")));
            this.rdoHakkoSubete.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoHakkoSubete.LinkedTextBox = "txtHakkoKbn";
            this.rdoHakkoSubete.Location = new System.Drawing.Point(208, 0);
            this.rdoHakkoSubete.Name = "rdoHakkoSubete";
            this.rdoHakkoSubete.PopupAfterExecute = null;
            this.rdoHakkoSubete.PopupBeforeExecute = null;
            this.rdoHakkoSubete.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoHakkoSubete.PopupSearchSendParams")));
            this.rdoHakkoSubete.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoHakkoSubete.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoHakkoSubete.popupWindowSetting")));
            this.rdoHakkoSubete.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoHakkoSubete.RegistCheckMethod")));
            this.rdoHakkoSubete.Size = new System.Drawing.Size(67, 17);
            this.rdoHakkoSubete.TabIndex = 9;
            this.rdoHakkoSubete.Tag = "全てを抽出したい場合にはチェックを付けてください";
            this.rdoHakkoSubete.Text = "3.全て";
            this.rdoHakkoSubete.UseVisualStyleBackColor = true;
            this.rdoHakkoSubete.Value = "3";
            // 
            // txtHakkoKbn
            // 
            this.txtHakkoKbn.BackColor = System.Drawing.SystemColors.Window;
            this.txtHakkoKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHakkoKbn.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtHakkoKbn.DisplayPopUp = null;
            this.txtHakkoKbn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtHakkoKbn.FocusOutCheckMethod")));
            this.txtHakkoKbn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtHakkoKbn.ForeColor = System.Drawing.Color.Black;
            this.txtHakkoKbn.IsInputErrorOccured = false;
            this.txtHakkoKbn.LinkedRadioButtonArray = new string[] {
        "rdoHakkoMihakko",
        "rdoHakkoHakkozumi",
        "rdoHakkoSubete"};
            this.txtHakkoKbn.Location = new System.Drawing.Point(-1, -1);
            this.txtHakkoKbn.Name = "txtHakkoKbn";
            this.txtHakkoKbn.PopupAfterExecute = null;
            this.txtHakkoKbn.PopupBeforeExecute = null;
            this.txtHakkoKbn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtHakkoKbn.PopupSearchSendParams")));
            this.txtHakkoKbn.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtHakkoKbn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtHakkoKbn.popupWindowSetting")));
            rangeSettingDto7.Max = new decimal(new int[] {
            3,
            0,
            0,
            0});
            rangeSettingDto7.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtHakkoKbn.RangeSetting = rangeSettingDto7;
            this.txtHakkoKbn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtHakkoKbn.RegistCheckMethod")));
            this.txtHakkoKbn.Size = new System.Drawing.Size(20, 20);
            this.txtHakkoKbn.TabIndex = 6;
            this.txtHakkoKbn.Tag = "【1～3】のいずれかで入力してください";
            this.txtHakkoKbn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtHakkoKbn.WordWrap = false;
            // 
            // TORIHIKISAKI_SEARCH_BUTTON
            // 
            this.TORIHIKISAKI_SEARCH_BUTTON.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.TORIHIKISAKI_SEARCH_BUTTON.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TORIHIKISAKI_SEARCH_BUTTON.DBFieldsName = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_SEARCH_BUTTON.DisplayItemName = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.DisplayPopUp = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.ErrorMessage = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_SEARCH_BUTTON.FocusOutCheckMethod")));
            this.TORIHIKISAKI_SEARCH_BUTTON.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TORIHIKISAKI_SEARCH_BUTTON.GetCodeMasterField = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.Image = ((System.Drawing.Image)(resources.GetObject("TORIHIKISAKI_SEARCH_BUTTON.Image")));
            this.TORIHIKISAKI_SEARCH_BUTTON.ItemDefinedTypes = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.LinkedSettingTextBox = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.LinkedTextBoxs = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.Location = new System.Drawing.Point(618, 2);
            this.TORIHIKISAKI_SEARCH_BUTTON.Name = "TORIHIKISAKI_SEARCH_BUTTON";
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupAfterExecute = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupGetMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_SEARCH_BUTTON.PopupSearchSendParams")));
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupSendParams = new string[] {
        "cmbShimebi"};
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupSetFormField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupWindowName = "検索共通ポップアップ";
            this.TORIHIKISAKI_SEARCH_BUTTON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_SEARCH_BUTTON.popupWindowSetting")));
            this.TORIHIKISAKI_SEARCH_BUTTON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_SEARCH_BUTTON.RegistCheckMethod")));
            this.TORIHIKISAKI_SEARCH_BUTTON.SearchDisplayFlag = 0;
            this.TORIHIKISAKI_SEARCH_BUTTON.SetFormField = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.ShortItemName = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.Size = new System.Drawing.Size(22, 22);
            this.TORIHIKISAKI_SEARCH_BUTTON.TabIndex = 28;
            this.TORIHIKISAKI_SEARCH_BUTTON.TabStop = false;
            this.TORIHIKISAKI_SEARCH_BUTTON.UseVisualStyleBackColor = false;
            this.TORIHIKISAKI_SEARCH_BUTTON.ZeroPaddengFlag = false;
            // 
            // TORIHIKISAKI_NAME_RYAKU
            // 
            this.TORIHIKISAKI_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.TORIHIKISAKI_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_NAME_RYAKU.DBFieldsName = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_NAME_RYAKU.DisplayPopUp = null;
            this.TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TORIHIKISAKI_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_NAME_RYAKU.IsInputErrorOccured = false;
            this.TORIHIKISAKI_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_NAME_RYAKU.Location = new System.Drawing.Point(329, 3);
            this.TORIHIKISAKI_NAME_RYAKU.MaxLength = 0;
            this.TORIHIKISAKI_NAME_RYAKU.Name = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_RYAKU.PopupAfterExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU.PopupBeforeExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams")));
            this.TORIHIKISAKI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.popupWindowSetting")));
            this.TORIHIKISAKI_NAME_RYAKU.ReadOnly = true;
            this.TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU.Size = new System.Drawing.Size(286, 20);
            this.TORIHIKISAKI_NAME_RYAKU.TabIndex = 31;
            this.TORIHIKISAKI_NAME_RYAKU.TabStop = false;
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
            this.TORIHIKISAKI_CD.DisplayItemName = "取引先";
            this.TORIHIKISAKI_CD.DisplayPopUp = null;
            this.TORIHIKISAKI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.FocusOutCheckMethod")));
            this.TORIHIKISAKI_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TORIHIKISAKI_CD.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_CD.GetCodeMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TORIHIKISAKI_CD.IsInputErrorOccured = false;
            this.TORIHIKISAKI_CD.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_CD.Location = new System.Drawing.Point(280, 3);
            this.TORIHIKISAKI_CD.MaxLength = 6;
            this.TORIHIKISAKI_CD.Name = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.PopupAfterExecute = null;
            this.TORIHIKISAKI_CD.PopupBeforeExecute = null;
            this.TORIHIKISAKI_CD.PopupGetMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_CD.PopupSearchSendParams")));
            this.TORIHIKISAKI_CD.PopupSendParams = new string[] {
        "cmbShimebi"};
            this.TORIHIKISAKI_CD.PopupSetFormField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TORIHIKISAKI_CD.PopupWindowName = "検索共通ポップアップ";
            this.TORIHIKISAKI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_CD.popupWindowSetting")));
            this.TORIHIKISAKI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.RegistCheckMethod")));
            this.TORIHIKISAKI_CD.SetFormField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.Size = new System.Drawing.Size(50, 20);
            this.TORIHIKISAKI_CD.TabIndex = 5;
            this.TORIHIKISAKI_CD.Tag = "取引先を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.TORIHIKISAKI_CD.ZeroPaddengFlag = true;
            // 
            // TORIHIKISAKI_LABEL
            // 
            this.TORIHIKISAKI_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.TORIHIKISAKI_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TORIHIKISAKI_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TORIHIKISAKI_LABEL.ForeColor = System.Drawing.Color.White;
            this.TORIHIKISAKI_LABEL.Location = new System.Drawing.Point(164, 3);
            this.TORIHIKISAKI_LABEL.Name = "TORIHIKISAKI_LABEL";
            this.TORIHIKISAKI_LABEL.Size = new System.Drawing.Size(110, 20);
            this.TORIHIKISAKI_LABEL.TabIndex = 29;
            this.TORIHIKISAKI_LABEL.Text = "取引先";
            this.TORIHIKISAKI_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel8
            // 
            this.customPanel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel8.Controls.Add(this.FILTERING_DATA);
            this.customPanel8.Controls.Add(this.crbIncludeOtherData);
            this.customPanel8.Controls.Add(this.crbPaymentOnly);
            this.customPanel8.Location = new System.Drawing.Point(119, 26);
            this.customPanel8.Name = "customPanel8";
            this.customPanel8.Size = new System.Drawing.Size(322, 20);
            this.customPanel8.TabIndex = 0;
            // 
            // FILTERING_DATA
            // 
            this.FILTERING_DATA.BackColor = System.Drawing.SystemColors.Window;
            this.FILTERING_DATA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FILTERING_DATA.DefaultBackColor = System.Drawing.Color.Empty;
            this.FILTERING_DATA.DisplayPopUp = null;
            this.FILTERING_DATA.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FILTERING_DATA.FocusOutCheckMethod")));
            this.FILTERING_DATA.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.FILTERING_DATA.ForeColor = System.Drawing.Color.Black;
            this.FILTERING_DATA.IsInputErrorOccured = false;
            this.FILTERING_DATA.LinkedRadioButtonArray = new string[] {
        "crbPaymentOnly",
        "crbIncludeOtherData"};
            this.FILTERING_DATA.Location = new System.Drawing.Point(-1, -1);
            this.FILTERING_DATA.Name = "FILTERING_DATA";
            this.FILTERING_DATA.PopupAfterExecute = null;
            this.FILTERING_DATA.PopupBeforeExecute = null;
            this.FILTERING_DATA.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("FILTERING_DATA.PopupSearchSendParams")));
            this.FILTERING_DATA.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.FILTERING_DATA.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("FILTERING_DATA.popupWindowSetting")));
            rangeSettingDto8.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto8.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FILTERING_DATA.RangeSetting = rangeSettingDto8;
            this.FILTERING_DATA.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FILTERING_DATA.RegistCheckMethod")));
            this.FILTERING_DATA.Size = new System.Drawing.Size(20, 20);
            this.FILTERING_DATA.TabIndex = 1;
            this.FILTERING_DATA.Tag = "【1、2】のいずれかで入力してください";
            this.FILTERING_DATA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FILTERING_DATA.WordWrap = false;
            // 
            // crbIncludeOtherData
            // 
            this.crbIncludeOtherData.AutoSize = true;
            this.crbIncludeOtherData.DefaultBackColor = System.Drawing.Color.Empty;
            this.crbIncludeOtherData.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("crbIncludeOtherData.FocusOutCheckMethod")));
            this.crbIncludeOtherData.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.crbIncludeOtherData.LinkedTextBox = "FILTERING_DATA";
            this.crbIncludeOtherData.Location = new System.Drawing.Point(147, 0);
            this.crbIncludeOtherData.Name = "crbIncludeOtherData";
            this.crbIncludeOtherData.PopupAfterExecute = null;
            this.crbIncludeOtherData.PopupBeforeExecute = null;
            this.crbIncludeOtherData.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("crbIncludeOtherData.PopupSearchSendParams")));
            this.crbIncludeOtherData.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.crbIncludeOtherData.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("crbIncludeOtherData.popupWindowSetting")));
            this.crbIncludeOtherData.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("crbIncludeOtherData.RegistCheckMethod")));
            this.crbIncludeOtherData.Size = new System.Drawing.Size(165, 17);
            this.crbIncludeOtherData.TabIndex = 4;
            this.crbIncludeOtherData.Tag = "支払が発生している情報と、残高または出金情報が存在する情報を抽出したい場合はチェックを付けてください";
            this.crbIncludeOtherData.Text = "2.支払発生なしを含む";
            this.crbIncludeOtherData.UseVisualStyleBackColor = true;
            this.crbIncludeOtherData.Value = "2";
            // 
            // crbPaymentOnly
            // 
            this.crbPaymentOnly.AutoSize = true;
            this.crbPaymentOnly.DefaultBackColor = System.Drawing.Color.Empty;
            this.crbPaymentOnly.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("crbPaymentOnly.FocusOutCheckMethod")));
            this.crbPaymentOnly.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.crbPaymentOnly.LinkedTextBox = "FILTERING_DATA";
            this.crbPaymentOnly.Location = new System.Drawing.Point(23, 0);
            this.crbPaymentOnly.Name = "crbPaymentOnly";
            this.crbPaymentOnly.PopupAfterExecute = null;
            this.crbPaymentOnly.PopupBeforeExecute = null;
            this.crbPaymentOnly.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("crbPaymentOnly.PopupSearchSendParams")));
            this.crbPaymentOnly.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.crbPaymentOnly.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("crbPaymentOnly.popupWindowSetting")));
            this.crbPaymentOnly.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("crbPaymentOnly.RegistCheckMethod")));
            this.crbPaymentOnly.Size = new System.Drawing.Size(123, 17);
            this.crbPaymentOnly.TabIndex = 3;
            this.crbPaymentOnly.Tag = "支払が発生している情報のみを抽出したい場合はチェックを付けてください";
            this.crbPaymentOnly.Text = "1.支払発生のみ";
            this.crbPaymentOnly.UseVisualStyleBackColor = true;
            this.crbPaymentOnly.Value = "1";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(3, 26);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(110, 20);
            this.label11.TabIndex = 32;
            this.label11.Text = "抽出データ※";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBoxAll_zumi
            // 
            this.checkBoxAll_zumi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.checkBoxAll_zumi.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.checkBoxAll_zumi.DefaultBackColor = System.Drawing.Color.Empty;
            this.checkBoxAll_zumi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("checkBoxAll_zumi.FocusOutCheckMethod")));
            this.checkBoxAll_zumi.ForeColor = System.Drawing.Color.White;
            this.checkBoxAll_zumi.Location = new System.Drawing.Point(157, 236);
            this.checkBoxAll_zumi.Name = "checkBoxAll_zumi";
            this.checkBoxAll_zumi.PopupAfterExecute = null;
            this.checkBoxAll_zumi.PopupBeforeExecute = null;
            this.checkBoxAll_zumi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("checkBoxAll_zumi.PopupSearchSendParams")));
            this.checkBoxAll_zumi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.checkBoxAll_zumi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("checkBoxAll_zumi.popupWindowSetting")));
            this.checkBoxAll_zumi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("checkBoxAll_zumi.RegistCheckMethod")));
            this.checkBoxAll_zumi.Size = new System.Drawing.Size(13, 14);
            this.checkBoxAll_zumi.TabIndex = 33;
            this.checkBoxAll_zumi.Tag = "全明細を選択／解除する場合、チェックしてください";
            this.checkBoxAll_zumi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxAll_zumi.UseVisualStyleBackColor = false;
            this.checkBoxAll_zumi.CheckedChanged += new System.EventHandler(this.checkBoxAll_zumi_CheckedChanged);
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
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(671, 12);
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
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 10032;
            this.ISNOT_NEED_DELETE_FLG.TabStop = false;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // ZERO_KINGAKU_TAISHOGAI
            // 
            this.ZERO_KINGAKU_TAISHOGAI.AutoSize = true;
            this.ZERO_KINGAKU_TAISHOGAI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ZERO_KINGAKU_TAISHOGAI.DefaultBackColor = System.Drawing.Color.Empty;
            this.ZERO_KINGAKU_TAISHOGAI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZERO_KINGAKU_TAISHOGAI.FocusOutCheckMethod")));
            this.ZERO_KINGAKU_TAISHOGAI.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ZERO_KINGAKU_TAISHOGAI.Location = new System.Drawing.Point(450, 29);
            this.ZERO_KINGAKU_TAISHOGAI.Name = "ZERO_KINGAKU_TAISHOGAI";
            this.ZERO_KINGAKU_TAISHOGAI.PopupAfterExecute = null;
            this.ZERO_KINGAKU_TAISHOGAI.PopupBeforeExecute = null;
            this.ZERO_KINGAKU_TAISHOGAI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZERO_KINGAKU_TAISHOGAI.PopupSearchSendParams")));
            this.ZERO_KINGAKU_TAISHOGAI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ZERO_KINGAKU_TAISHOGAI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZERO_KINGAKU_TAISHOGAI.popupWindowSetting")));
            this.ZERO_KINGAKU_TAISHOGAI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZERO_KINGAKU_TAISHOGAI.RegistCheckMethod")));
            this.ZERO_KINGAKU_TAISHOGAI.Size = new System.Drawing.Size(397, 17);
            this.ZERO_KINGAKU_TAISHOGAI.TabIndex = 2;
            this.ZERO_KINGAKU_TAISHOGAI.Tag = "支払金額が発生していない支払明細書を対象外とします";
            this.ZERO_KINGAKU_TAISHOGAI.Text = "「今回御精算額」　が0円の支払明細書を抽出対象外とする";
            this.ZERO_KINGAKU_TAISHOGAI.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(3, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 20);
            this.label6.TabIndex = 10036;
            this.label6.Text = "支払(控)印刷※";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel5
            // 
            this.customPanel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel5.Controls.Add(this.HIKAE_OUTPUT_KBN_3);
            this.customPanel5.Controls.Add(this.HIKAE_OUTPUT_KBN);
            this.customPanel5.Controls.Add(this.HIKAE_OUTPUT_KBN_2);
            this.customPanel5.Controls.Add(this.HIKAE_OUTPUT_KBN_1);
            this.customPanel5.Location = new System.Drawing.Point(119, 118);
            this.customPanel5.Name = "customPanel5";
            this.customPanel5.Size = new System.Drawing.Size(577, 40);
            this.customPanel5.TabIndex = 8;
            // 
            // HIKAE_OUTPUT_KBN_3
            // 
            this.HIKAE_OUTPUT_KBN_3.AutoSize = true;
            this.HIKAE_OUTPUT_KBN_3.DefaultBackColor = System.Drawing.Color.Empty;
            this.HIKAE_OUTPUT_KBN_3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIKAE_OUTPUT_KBN_3.FocusOutCheckMethod")));
            this.HIKAE_OUTPUT_KBN_3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HIKAE_OUTPUT_KBN_3.LinkedTextBox = "HIKAE_OUTPUT_KBN";
            this.HIKAE_OUTPUT_KBN_3.Location = new System.Drawing.Point(23, 20);
            this.HIKAE_OUTPUT_KBN_3.Name = "HIKAE_OUTPUT_KBN_3";
            this.HIKAE_OUTPUT_KBN_3.PopupAfterExecute = null;
            this.HIKAE_OUTPUT_KBN_3.PopupBeforeExecute = null;
            this.HIKAE_OUTPUT_KBN_3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIKAE_OUTPUT_KBN_3.PopupSearchSendParams")));
            this.HIKAE_OUTPUT_KBN_3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIKAE_OUTPUT_KBN_3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HIKAE_OUTPUT_KBN_3.popupWindowSetting")));
            this.HIKAE_OUTPUT_KBN_3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIKAE_OUTPUT_KBN_3.RegistCheckMethod")));
            this.HIKAE_OUTPUT_KBN_3.Size = new System.Drawing.Size(151, 17);
            this.HIKAE_OUTPUT_KBN_3.TabIndex = 7;
            this.HIKAE_OUTPUT_KBN_3.Tag = "控えを印刷しない";
            this.HIKAE_OUTPUT_KBN_3.Text = "3.控えを印刷しない";
            this.HIKAE_OUTPUT_KBN_3.UseVisualStyleBackColor = true;
            this.HIKAE_OUTPUT_KBN_3.Value = "3";
            // 
            // HIKAE_OUTPUT_KBN
            // 
            this.HIKAE_OUTPUT_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.HIKAE_OUTPUT_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HIKAE_OUTPUT_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.HIKAE_OUTPUT_KBN.DisplayPopUp = null;
            this.HIKAE_OUTPUT_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIKAE_OUTPUT_KBN.FocusOutCheckMethod")));
            this.HIKAE_OUTPUT_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HIKAE_OUTPUT_KBN.ForeColor = System.Drawing.Color.Black;
            this.HIKAE_OUTPUT_KBN.IsInputErrorOccured = false;
            this.HIKAE_OUTPUT_KBN.LinkedRadioButtonArray = new string[] {
        "HIKAE_OUTPUT_KBN_1",
        "HIKAE_OUTPUT_KBN_2",
        "HIKAE_OUTPUT_KBN_3"};
            this.HIKAE_OUTPUT_KBN.Location = new System.Drawing.Point(-1, -1);
            this.HIKAE_OUTPUT_KBN.Name = "HIKAE_OUTPUT_KBN";
            this.HIKAE_OUTPUT_KBN.PopupAfterExecute = null;
            this.HIKAE_OUTPUT_KBN.PopupBeforeExecute = null;
            this.HIKAE_OUTPUT_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIKAE_OUTPUT_KBN.PopupSearchSendParams")));
            this.HIKAE_OUTPUT_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIKAE_OUTPUT_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HIKAE_OUTPUT_KBN.popupWindowSetting")));
            rangeSettingDto9.Max = new decimal(new int[] {
            3,
            0,
            0,
            0});
            rangeSettingDto9.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.HIKAE_OUTPUT_KBN.RangeSetting = rangeSettingDto9;
            this.HIKAE_OUTPUT_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIKAE_OUTPUT_KBN.RegistCheckMethod")));
            this.HIKAE_OUTPUT_KBN.Size = new System.Drawing.Size(20, 20);
            this.HIKAE_OUTPUT_KBN.TabIndex = 8;
            this.HIKAE_OUTPUT_KBN.Tag = "【1、2、3】のいずれかで入力してください";
            this.HIKAE_OUTPUT_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.HIKAE_OUTPUT_KBN.WordWrap = false;
            // 
            // HIKAE_OUTPUT_KBN_2
            // 
            this.HIKAE_OUTPUT_KBN_2.AutoSize = true;
            this.HIKAE_OUTPUT_KBN_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.HIKAE_OUTPUT_KBN_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIKAE_OUTPUT_KBN_2.FocusOutCheckMethod")));
            this.HIKAE_OUTPUT_KBN_2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HIKAE_OUTPUT_KBN_2.LinkedTextBox = "HIKAE_OUTPUT_KBN";
            this.HIKAE_OUTPUT_KBN_2.Location = new System.Drawing.Point(295, 0);
            this.HIKAE_OUTPUT_KBN_2.Name = "HIKAE_OUTPUT_KBN_2";
            this.HIKAE_OUTPUT_KBN_2.PopupAfterExecute = null;
            this.HIKAE_OUTPUT_KBN_2.PopupBeforeExecute = null;
            this.HIKAE_OUTPUT_KBN_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIKAE_OUTPUT_KBN_2.PopupSearchSendParams")));
            this.HIKAE_OUTPUT_KBN_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIKAE_OUTPUT_KBN_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HIKAE_OUTPUT_KBN_2.popupWindowSetting")));
            this.HIKAE_OUTPUT_KBN_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIKAE_OUTPUT_KBN_2.RegistCheckMethod")));
            this.HIKAE_OUTPUT_KBN_2.Size = new System.Drawing.Size(277, 17);
            this.HIKAE_OUTPUT_KBN_2.TabIndex = 1;
            this.HIKAE_OUTPUT_KBN_2.Tag = "全ての支払明細書印刷後、控えを印刷";
            this.HIKAE_OUTPUT_KBN_2.Text = "2.全ての支払明細書印刷後、控えを印刷";
            this.HIKAE_OUTPUT_KBN_2.UseVisualStyleBackColor = true;
            this.HIKAE_OUTPUT_KBN_2.Value = "2";
            // 
            // HIKAE_OUTPUT_KBN_1
            // 
            this.HIKAE_OUTPUT_KBN_1.AutoSize = true;
            this.HIKAE_OUTPUT_KBN_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.HIKAE_OUTPUT_KBN_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIKAE_OUTPUT_KBN_1.FocusOutCheckMethod")));
            this.HIKAE_OUTPUT_KBN_1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HIKAE_OUTPUT_KBN_1.LinkedTextBox = "HIKAE_OUTPUT_KBN";
            this.HIKAE_OUTPUT_KBN_1.Location = new System.Drawing.Point(23, 0);
            this.HIKAE_OUTPUT_KBN_1.Name = "HIKAE_OUTPUT_KBN_1";
            this.HIKAE_OUTPUT_KBN_1.PopupAfterExecute = null;
            this.HIKAE_OUTPUT_KBN_1.PopupBeforeExecute = null;
            this.HIKAE_OUTPUT_KBN_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HIKAE_OUTPUT_KBN_1.PopupSearchSendParams")));
            this.HIKAE_OUTPUT_KBN_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HIKAE_OUTPUT_KBN_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HIKAE_OUTPUT_KBN_1.popupWindowSetting")));
            this.HIKAE_OUTPUT_KBN_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HIKAE_OUTPUT_KBN_1.RegistCheckMethod")));
            this.HIKAE_OUTPUT_KBN_1.Size = new System.Drawing.Size(270, 17);
            this.HIKAE_OUTPUT_KBN_1.TabIndex = 0;
            this.HIKAE_OUTPUT_KBN_1.Tag = "支払明細書、控え の順で繰返し印刷";
            this.HIKAE_OUTPUT_KBN_1.Text = "1.支払明細書、控え の順で繰返し印刷";
            this.HIKAE_OUTPUT_KBN_1.UseVisualStyleBackColor = true;
            this.HIKAE_OUTPUT_KBN_1.Value = "1";
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(998, 468);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.customPanel5);
            this.Controls.Add(this.ZERO_KINGAKU_TAISHOGAI);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.checkBoxAll_zumi);
            this.Controls.Add(this.customPanel8);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.TORIHIKISAKI_SEARCH_BUTTON);
            this.Controls.Add(this.TORIHIKISAKI_CD);
            this.Controls.Add(this.TORIHIKISAKI_LABEL);
            this.Controls.Add(this.customPanel6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbShimebi);
            this.Controls.Add(this.chkHakko);
            this.Controls.Add(this.customPanel4);
            this.Controls.Add(this.customPanel2);
            this.Controls.Add(this.radShiharaiPaper);
            this.Controls.Add(this.customPanel1);
            this.Controls.Add(this.pnlInsatsuJun);
            this.Controls.Add(this.dgvSeisanDenpyouItiran);
            this.Controls.Add(this.dtpSiteiPrintHiduke);
            this.Controls.Add(this.txtKyotenCd);
            this.Controls.Add(this.txtKyotenMei);
            this.Controls.Add(this.lblShiharaiMeisaisyoInsatsubi);
            this.Controls.Add(this.lblInsatsuJun);
            this.Controls.Add(this.lblShiharaiStyle);
            this.Controls.Add(this.lblShimebi);
            this.Controls.Add(this.lblHakkoKbn);
            this.Controls.Add(this.lblShiharaiPaper);
            this.Controls.Add(this.lblKyoten);
            this.Controls.Add(this.txtKensakuJouken);
            this.Controls.Add(this.TORIHIKISAKI_NAME_RYAKU);
            this.Name = "UIForm";
            this.Text = "UIForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSeisanDenpyouItiran)).EndInit();
            this.pnlInsatsuJun.ResumeLayout(false);
            this.pnlInsatsuJun.PerformLayout();
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.radShiharaiPaper.ResumeLayout(false);
            this.radShiharaiPaper.PerformLayout();
            this.customPanel4.ResumeLayout(false);
            this.customPanel4.PerformLayout();
            this.customPanel6.ResumeLayout(false);
            this.customPanel6.PerformLayout();
            this.customPanel2.ResumeLayout(false);
            this.customPanel2.PerformLayout();
            this.customPanel8.ResumeLayout(false);
            this.customPanel8.PerformLayout();
            this.customPanel5.ResumeLayout(false);
            this.customPanel5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblKyoten;
        private System.Windows.Forms.Label lblShimebi;
        private System.Windows.Forms.Label lblInsatsuJun;
        private System.Windows.Forms.Label lblShiharaiPaper;
        private System.Windows.Forms.Label lblShiharaiStyle;
        private System.Windows.Forms.Label lblShiharaiMeisaisyoInsatsubi;
        private r_framework.CustomControl.CustomPanel pnlInsatsuJun;
        private r_framework.CustomControl.CustomPanel customPanel1;
        private r_framework.CustomControl.CustomPanel radShiharaiPaper;
        private r_framework.CustomControl.CustomPanel customPanel4;
        public r_framework.CustomControl.CustomTextBox txtKyotenMei;
        public r_framework.CustomControl.CustomNumericTextBox2 txtKyotenCd;
        public r_framework.CustomControl.CustomTextBox txtKensakuJouken;
        public r_framework.CustomControl.CustomRadioButton rdoTorihikisakiCD;
        public r_framework.CustomControl.CustomRadioButton rdoFurigana;
        public r_framework.CustomControl.CustomRadioButton rdoShiharaiDataJisya;
        public r_framework.CustomControl.CustomRadioButton rdoShiharaiDataSitei;
        public r_framework.CustomControl.CustomRadioButton rdoJisya;
        public r_framework.CustomControl.CustomRadioButton rdoCreateDataOfStyle;
        public r_framework.CustomControl.CustomRadioButton rdoTangetsu;
        public r_framework.CustomControl.CustomRadioButton rdoKurikoshi;
        public r_framework.CustomControl.CustomRadioButton rdoShimebi;
        public r_framework.CustomControl.CustomRadioButton rdoHakkobi;
        public r_framework.CustomControl.CustomRadioButton rdoNashiOfInsatsubi;
        public r_framework.CustomControl.CustomRadioButton rdoShiteiOfInsatsubi;
        public r_framework.CustomControl.CustomDateTimePicker dtpSiteiPrintHiduke;
        public r_framework.CustomControl.CustomDataGridView dgvSeisanDenpyouItiran;
        internal r_framework.CustomControl.CustomCheckBox chkHakko;
        internal r_framework.CustomControl.CustomComboBox cmbShimebi;
        public r_framework.CustomControl.CustomNumericTextBox2 txtInsatsuJun;
        public r_framework.CustomControl.CustomNumericTextBox2 txtShiharaiPaper;
        public r_framework.CustomControl.CustomNumericTextBox2 txtShiharaiStyle;
        public r_framework.CustomControl.CustomNumericTextBox2 txtInsatsubi;
        public r_framework.CustomControl.CustomRadioButton rdoSitei;
        private System.Windows.Forms.Label label8;
        private r_framework.CustomControl.CustomPanel customPanel6;
        public r_framework.CustomControl.CustomNumericTextBox2 txtShiharaiHakkou;
        public r_framework.CustomControl.CustomRadioButton rdoSuru;
        public r_framework.CustomControl.CustomRadioButton rdoShinai;
        private System.Windows.Forms.Label lblHakkoKbn;
        private r_framework.CustomControl.CustomPanel customPanel2;
        public r_framework.CustomControl.CustomRadioButton rdoHakkoSubete;
        public r_framework.CustomControl.CustomNumericTextBox2 txtHakkoKbn;
        public r_framework.CustomControl.CustomRadioButton rdoHakkoHakkozumi;
        public r_framework.CustomControl.CustomRadioButton rdoHakkoMihakko;
        internal r_framework.CustomControl.CustomPopupOpenButton TORIHIKISAKI_SEARCH_BUTTON;
        internal r_framework.CustomControl.CustomTextBox TORIHIKISAKI_NAME_RYAKU;
        internal r_framework.CustomControl.CustomAlphaNumTextBox TORIHIKISAKI_CD;
        private System.Windows.Forms.Label TORIHIKISAKI_LABEL;
        private r_framework.CustomControl.CustomPanel customPanel8;
        public r_framework.CustomControl.CustomNumericTextBox2 FILTERING_DATA;
        public r_framework.CustomControl.CustomRadioButton crbIncludeOtherData;
        public r_framework.CustomControl.CustomRadioButton crbPaymentOnly;
        private System.Windows.Forms.Label label11;
        internal r_framework.CustomControl.CustomCheckBox checkBoxAll_zumi;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colHakkou;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colHakkouzumi;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn colDenpyoNumber;
        private r_framework.CustomControl.DgvCustomTextBoxColumn colSeisanDate;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn colTorihikisakiCd;
        private r_framework.CustomControl.DgvCustomTextBoxColumn colTorihikisakiName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShimebi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colZenkaiKurikoshiGaku;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShiharaiGaku;
        private System.Windows.Forms.DataGridViewTextBoxColumn colChouseiGaku;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKonkaiShiharaiGaku;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShohizei;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKonkaiSeisanGaku;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShiharaiYoteiBi;
        private r_framework.CustomControl.DgvCustomTextBoxColumn colTimeStamp;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
        internal r_framework.CustomControl.CustomCheckBox ZERO_KINGAKU_TAISHOGAI;
        internal System.Windows.Forms.Label label6;
        internal r_framework.CustomControl.CustomPanel customPanel5;
        public r_framework.CustomControl.CustomRadioButton HIKAE_OUTPUT_KBN_3;
        public r_framework.CustomControl.CustomNumericTextBox2 HIKAE_OUTPUT_KBN;
        public r_framework.CustomControl.CustomRadioButton HIKAE_OUTPUT_KBN_2;
        public r_framework.CustomControl.CustomRadioButton HIKAE_OUTPUT_KBN_1;
    }
}