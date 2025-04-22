namespace Shougun.Core.Allocation.TeikiJissekiHoukoku
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
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto5 = new r_framework.Dto.RangeSettingDto();
            this.LBL_KIKAN = new System.Windows.Forms.Label();
            this.lbl_FromTo = new System.Windows.Forms.Label();
            this.KIKAN_TO = new r_framework.CustomControl.CustomDateTimePicker();
            this.KIKAN_FROM = new r_framework.CustomControl.CustomDateTimePicker();
            this.KYOTEN_NAME = new r_framework.CustomControl.CustomTextBox();
            this.KYOTEN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.LBL_KYOTEN = new System.Windows.Forms.Label();
            this.SHUUKEISYUURYOU_1 = new r_framework.CustomControl.CustomRadioButton();
            this.SHUUKEISYUURYOU = new r_framework.CustomControl.CustomNumericTextBox2();
            this.LBL_SHUUKEISYUURYOU = new System.Windows.Forms.Label();
            this.customPanel2 = new r_framework.CustomControl.CustomPanel();
            this.SHUUKEISYUURYOU_2 = new r_framework.CustomControl.CustomRadioButton();
            this.SHIKUCHOUSON_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.SHIKUCHOUSON_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.LBL_SHIKUCHOUSON = new System.Windows.Forms.Label();
            this.DGV_TEIKI_JISSEKI_HOUKOKU = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.SHUUKEI_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.LBL_SHUUKEIKBN = new System.Windows.Forms.Label();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.SHUUKEI_KBN_3 = new r_framework.CustomControl.CustomRadioButton();
            this.SHUUKEI_KBN_1 = new r_framework.CustomControl.CustomRadioButton();
            this.SHUUKEI_KBN_2 = new r_framework.CustomControl.CustomRadioButton();
            this.KIKAN_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.customPanel3 = new r_framework.CustomControl.CustomPanel();
            this.KIKAN_KBN_1 = new r_framework.CustomControl.CustomRadioButton();
            this.KIKAN_KBN_2 = new r_framework.CustomControl.CustomRadioButton();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.customPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_TEIKI_JISSEKI_HOUKOKU)).BeginInit();
            this.customPanel1.SuspendLayout();
            this.customPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // LBL_KIKAN
            // 
            this.LBL_KIKAN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.LBL_KIKAN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LBL_KIKAN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LBL_KIKAN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LBL_KIKAN.ForeColor = System.Drawing.Color.White;
            this.LBL_KIKAN.Location = new System.Drawing.Point(2, 67);
            this.LBL_KIKAN.Name = "LBL_KIKAN";
            this.LBL_KIKAN.Size = new System.Drawing.Size(110, 20);
            this.LBL_KIKAN.TabIndex = 30;
            this.LBL_KIKAN.Text = "期間※";
            this.LBL_KIKAN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_FromTo
            // 
            this.lbl_FromTo.BackColor = System.Drawing.Color.Transparent;
            this.lbl_FromTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_FromTo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_FromTo.ForeColor = System.Drawing.Color.Black;
            this.lbl_FromTo.Location = new System.Drawing.Point(270, 87);
            this.lbl_FromTo.Name = "lbl_FromTo";
            this.lbl_FromTo.Size = new System.Drawing.Size(19, 20);
            this.lbl_FromTo.TabIndex = 32;
            this.lbl_FromTo.Text = "～";
            this.lbl_FromTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KIKAN_TO
            // 
            this.KIKAN_TO.BackColor = System.Drawing.SystemColors.Window;
            this.KIKAN_TO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KIKAN_TO.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.KIKAN_TO.Checked = false;
            this.KIKAN_TO.CustomFormat = "yyyy/MM/dd ddd";
            this.KIKAN_TO.DateTimeNowYear = "";
            this.KIKAN_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.KIKAN_TO.DisplayItemName = "期間To";
            this.KIKAN_TO.DisplayPopUp = null;
            this.KIKAN_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KIKAN_TO.FocusOutCheckMethod")));
            this.KIKAN_TO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KIKAN_TO.ForeColor = System.Drawing.Color.Black;
            this.KIKAN_TO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.KIKAN_TO.IsInputErrorOccured = false;
            this.KIKAN_TO.Location = new System.Drawing.Point(301, 87);
            this.KIKAN_TO.MaxLength = 10;
            this.KIKAN_TO.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.KIKAN_TO.Name = "KIKAN_TO";
            this.KIKAN_TO.NullValue = "";
            this.KIKAN_TO.PopupAfterExecute = null;
            this.KIKAN_TO.PopupBeforeExecute = null;
            this.KIKAN_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KIKAN_TO.PopupSearchSendParams")));
            this.KIKAN_TO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KIKAN_TO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KIKAN_TO.popupWindowSetting")));
            this.KIKAN_TO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KIKAN_TO.RegistCheckMethod")));
            this.KIKAN_TO.Size = new System.Drawing.Size(138, 20);
            this.KIKAN_TO.TabIndex = 35;
            this.KIKAN_TO.Tag = "期間(To)を指定してください";
            this.KIKAN_TO.Text = "2013/12/01(日)";
            this.KIKAN_TO.Value = new System.DateTime(2013, 12, 1, 0, 0, 0, 0);
            this.KIKAN_TO.DoubleClick += new System.EventHandler(this.KIKAN_TO_DoubleClick);
            // 
            // KIKAN_FROM
            // 
            this.KIKAN_FROM.BackColor = System.Drawing.SystemColors.Window;
            this.KIKAN_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KIKAN_FROM.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.KIKAN_FROM.Checked = false;
            this.KIKAN_FROM.CustomFormat = "yyyy/MM/dd ddd";
            this.KIKAN_FROM.DateTimeNowYear = "";
            this.KIKAN_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.KIKAN_FROM.DisplayItemName = "期間From";
            this.KIKAN_FROM.DisplayPopUp = null;
            this.KIKAN_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KIKAN_FROM.FocusOutCheckMethod")));
            this.KIKAN_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KIKAN_FROM.ForeColor = System.Drawing.Color.Black;
            this.KIKAN_FROM.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.KIKAN_FROM.IsInputErrorOccured = false;
            this.KIKAN_FROM.Location = new System.Drawing.Point(117, 87);
            this.KIKAN_FROM.MaxLength = 10;
            this.KIKAN_FROM.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.KIKAN_FROM.Name = "KIKAN_FROM";
            this.KIKAN_FROM.NullValue = "";
            this.KIKAN_FROM.PopupAfterExecute = null;
            this.KIKAN_FROM.PopupBeforeExecute = null;
            this.KIKAN_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KIKAN_FROM.PopupSearchSendParams")));
            this.KIKAN_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KIKAN_FROM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KIKAN_FROM.popupWindowSetting")));
            this.KIKAN_FROM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KIKAN_FROM.RegistCheckMethod")));
            this.KIKAN_FROM.Size = new System.Drawing.Size(138, 20);
            this.KIKAN_FROM.TabIndex = 34;
            this.KIKAN_FROM.Tag = "期間(From)を指定してください";
            this.KIKAN_FROM.Text = "2013/12/01(日)";
            this.KIKAN_FROM.Value = new System.DateTime(2013, 12, 1, 0, 0, 0, 0);
            // 
            // KYOTEN_NAME
            // 
            this.KYOTEN_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KYOTEN_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_NAME.CharactersNumber = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.KYOTEN_NAME.DBFieldsName = "";
            this.KYOTEN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_NAME.DisplayPopUp = null;
            this.KYOTEN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME.FocusOutCheckMethod")));
            this.KYOTEN_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KYOTEN_NAME.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_NAME.IsInputErrorOccured = false;
            this.KYOTEN_NAME.Location = new System.Drawing.Point(180, 19);
            this.KYOTEN_NAME.MaxLength = 10;
            this.KYOTEN_NAME.Name = "KYOTEN_NAME";
            this.KYOTEN_NAME.PopupAfterExecute = null;
            this.KYOTEN_NAME.PopupBeforeExecute = null;
            this.KYOTEN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_NAME.PopupSearchSendParams")));
            this.KYOTEN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOTEN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_NAME.popupWindowSetting")));
            this.KYOTEN_NAME.prevText = null;
            this.KYOTEN_NAME.PrevText = null;
            this.KYOTEN_NAME.ReadOnly = true;
            this.KYOTEN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME.RegistCheckMethod")));
            this.KYOTEN_NAME.Size = new System.Drawing.Size(157, 20);
            this.KYOTEN_NAME.TabIndex = 12;
            this.KYOTEN_NAME.TabStop = false;
            this.KYOTEN_NAME.Tag = "拠点名が表示されます";
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
            this.KYOTEN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KYOTEN_CD.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_CD.FormatSetting = "カスタム";
            this.KYOTEN_CD.GetCodeMasterField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.IsInputErrorOccured = false;
            this.KYOTEN_CD.ItemDefinedTypes = "smallint";
            this.KYOTEN_CD.Location = new System.Drawing.Point(117, 19);
            this.KYOTEN_CD.Name = "KYOTEN_CD";
            this.KYOTEN_CD.PopupAfterExecute = null;
            this.KYOTEN_CD.PopupBeforeExecute = null;
            this.KYOTEN_CD.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_CD.PopupSearchSendParams")));
            this.KYOTEN_CD.PopupSetFormField = "KYOTEN_CD, KYOTEN_NAME";
            this.KYOTEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.KYOTEN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.KYOTEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_CD.popupWindowSetting")));
            this.KYOTEN_CD.prevText = "";
            this.KYOTEN_CD.PrevText = "";
            rangeSettingDto1.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.KYOTEN_CD.RangeSetting = rangeSettingDto1;
            this.KYOTEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.RegistCheckMethod")));
            this.KYOTEN_CD.SetFormField = "KYOTEN_CD, KYOTEN_NAME";
            this.KYOTEN_CD.Size = new System.Drawing.Size(64, 20);
            this.KYOTEN_CD.TabIndex = 11;
            this.KYOTEN_CD.Tag = "拠点を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.KYOTEN_CD.WordWrap = false;
            // 
            // LBL_KYOTEN
            // 
            this.LBL_KYOTEN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.LBL_KYOTEN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LBL_KYOTEN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LBL_KYOTEN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LBL_KYOTEN.ForeColor = System.Drawing.Color.White;
            this.LBL_KYOTEN.Location = new System.Drawing.Point(2, 19);
            this.LBL_KYOTEN.Name = "LBL_KYOTEN";
            this.LBL_KYOTEN.Size = new System.Drawing.Size(110, 20);
            this.LBL_KYOTEN.TabIndex = 10;
            this.LBL_KYOTEN.Text = "拠点※";
            this.LBL_KYOTEN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SHUUKEISYUURYOU_1
            // 
            this.SHUUKEISYUURYOU_1.AutoSize = true;
            this.SHUUKEISYUURYOU_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUUKEISYUURYOU_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEISYUURYOU_1.FocusOutCheckMethod")));
            this.SHUUKEISYUURYOU_1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHUUKEISYUURYOU_1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SHUUKEISYUURYOU_1.LinkedTextBox = "SHUUKEISYUURYOU";
            this.SHUUKEISYUURYOU_1.Location = new System.Drawing.Point(7, 0);
            this.SHUUKEISYUURYOU_1.Name = "SHUUKEISYUURYOU_1";
            this.SHUUKEISYUURYOU_1.PopupAfterExecute = null;
            this.SHUUKEISYUURYOU_1.PopupBeforeExecute = null;
            this.SHUUKEISYUURYOU_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUUKEISYUURYOU_1.PopupSearchSendParams")));
            this.SHUUKEISYUURYOU_1.PopupSetFormField = "";
            this.SHUUKEISYUURYOU_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUUKEISYUURYOU_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUUKEISYUURYOU_1.popupWindowSetting")));
            this.SHUUKEISYUURYOU_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEISYUURYOU_1.RegistCheckMethod")));
            this.SHUUKEISYUURYOU_1.SetFormField = "";
            this.SHUUKEISYUURYOU_1.Size = new System.Drawing.Size(165, 17);
            this.SHUUKEISYUURYOU_1.TabIndex = 53;
            this.SHUUKEISYUURYOU_1.Tag = "集計対象数量を「実績数量」にする場合は、チェックを付けてください";
            this.SHUUKEISYUURYOU_1.Text = "1.実績数量・換算数量";
            this.SHUUKEISYUURYOU_1.UseVisualStyleBackColor = true;
            this.SHUUKEISYUURYOU_1.Value = "1";
            // 
            // SHUUKEISYUURYOU
            // 
            this.SHUUKEISYUURYOU.BackColor = System.Drawing.SystemColors.Window;
            this.SHUUKEISYUURYOU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHUUKEISYUURYOU.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUUKEISYUURYOU.DisplayItemName = "集計対象数量";
            this.SHUUKEISYUURYOU.DisplayPopUp = null;
            this.SHUUKEISYUURYOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEISYUURYOU.FocusOutCheckMethod")));
            this.SHUUKEISYUURYOU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHUUKEISYUURYOU.ForeColor = System.Drawing.Color.Black;
            this.SHUUKEISYUURYOU.GetCodeMasterField = "";
            this.SHUUKEISYUURYOU.IsInputErrorOccured = false;
            this.SHUUKEISYUURYOU.LinkedRadioButtonArray = new string[] {
        "SHUUKEISYUURYOU_1",
        "SHUUKEISYUURYOU_2"};
            this.SHUUKEISYUURYOU.Location = new System.Drawing.Point(117, 178);
            this.SHUUKEISYUURYOU.Name = "SHUUKEISYUURYOU";
            this.SHUUKEISYUURYOU.PopupAfterExecute = null;
            this.SHUUKEISYUURYOU.PopupBeforeExecute = null;
            this.SHUUKEISYUURYOU.PopupGetMasterField = "";
            this.SHUUKEISYUURYOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUUKEISYUURYOU.PopupSearchSendParams")));
            this.SHUUKEISYUURYOU.PopupSetFormField = "";
            this.SHUUKEISYUURYOU.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
            this.SHUUKEISYUURYOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUUKEISYUURYOU.popupWindowSetting")));
            this.SHUUKEISYUURYOU.prevText = "";
            this.SHUUKEISYUURYOU.PrevText = "";
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
            this.SHUUKEISYUURYOU.RangeSetting = rangeSettingDto2;
            this.SHUUKEISYUURYOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEISYUURYOU.RegistCheckMethod")));
            this.SHUUKEISYUURYOU.SetFormField = "";
            this.SHUUKEISYUURYOU.Size = new System.Drawing.Size(20, 20);
            this.SHUUKEISYUURYOU.TabIndex = 51;
            this.SHUUKEISYUURYOU.Tag = "【1～2】のいずれかで入力してください";
            this.SHUUKEISYUURYOU.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.SHUUKEISYUURYOU.WordWrap = false;
            // 
            // LBL_SHUUKEISYUURYOU
            // 
            this.LBL_SHUUKEISYUURYOU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.LBL_SHUUKEISYUURYOU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LBL_SHUUKEISYUURYOU.Location = new System.Drawing.Point(2, 178);
            this.LBL_SHUUKEISYUURYOU.Name = "LBL_SHUUKEISYUURYOU";
            this.LBL_SHUUKEISYUURYOU.Size = new System.Drawing.Size(110, 20);
            this.LBL_SHUUKEISYUURYOU.TabIndex = 50;
            this.LBL_SHUUKEISYUURYOU.Text = "集計対象数量※";
            this.LBL_SHUUKEISYUURYOU.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel2
            // 
            this.customPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel2.Controls.Add(this.SHUUKEISYUURYOU_1);
            this.customPanel2.Controls.Add(this.SHUUKEISYUURYOU_2);
            this.customPanel2.Location = new System.Drawing.Point(136, 178);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(303, 20);
            this.customPanel2.TabIndex = 52;
            // 
            // SHUUKEISYUURYOU_2
            // 
            this.SHUUKEISYUURYOU_2.AutoSize = true;
            this.SHUUKEISYUURYOU_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUUKEISYUURYOU_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEISYUURYOU_2.FocusOutCheckMethod")));
            this.SHUUKEISYUURYOU_2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHUUKEISYUURYOU_2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SHUUKEISYUURYOU_2.LinkedTextBox = "SHUUKEISYUURYOU";
            this.SHUUKEISYUURYOU_2.Location = new System.Drawing.Point(187, 0);
            this.SHUUKEISYUURYOU_2.Name = "SHUUKEISYUURYOU_2";
            this.SHUUKEISYUURYOU_2.PopupAfterExecute = null;
            this.SHUUKEISYUURYOU_2.PopupBeforeExecute = null;
            this.SHUUKEISYUURYOU_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUUKEISYUURYOU_2.PopupSearchSendParams")));
            this.SHUUKEISYUURYOU_2.PopupSetFormField = "";
            this.SHUUKEISYUURYOU_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUUKEISYUURYOU_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUUKEISYUURYOU_2.popupWindowSetting")));
            this.SHUUKEISYUURYOU_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEISYUURYOU_2.RegistCheckMethod")));
            this.SHUUKEISYUURYOU_2.SetFormField = "";
            this.SHUUKEISYUURYOU_2.Size = new System.Drawing.Size(95, 17);
            this.SHUUKEISYUURYOU_2.TabIndex = 54;
            this.SHUUKEISYUURYOU_2.Tag = "集計対象数量を「按分数量」にする場合は、チェックを付けてください";
            this.SHUUKEISYUURYOU_2.Text = "2.按分数量";
            this.SHUUKEISYUURYOU_2.UseVisualStyleBackColor = true;
            this.SHUUKEISYUURYOU_2.Value = "2";
            // 
            // SHIKUCHOUSON_CD
            // 
            this.SHIKUCHOUSON_CD.BackColor = System.Drawing.SystemColors.Window;
            this.SHIKUCHOUSON_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHIKUCHOUSON_CD.ChangeUpperCase = true;
            this.SHIKUCHOUSON_CD.CharacterLimitList = null;
            this.SHIKUCHOUSON_CD.CharactersNumber = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.SHIKUCHOUSON_CD.DBFieldsName = "SHIKUCHOUSON_CD";
            this.SHIKUCHOUSON_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHIKUCHOUSON_CD.DisplayItemName = "市区町村CD";
            this.SHIKUCHOUSON_CD.DisplayPopUp = null;
            this.SHIKUCHOUSON_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHIKUCHOUSON_CD.FocusOutCheckMethod")));
            this.SHIKUCHOUSON_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHIKUCHOUSON_CD.ForeColor = System.Drawing.Color.Black;
            this.SHIKUCHOUSON_CD.GetCodeMasterField = "SHIKUCHOUSON_CD,SHIKUCHOUSON_NAME_RYAKU";
            this.SHIKUCHOUSON_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SHIKUCHOUSON_CD.IsInputErrorOccured = false;
            this.SHIKUCHOUSON_CD.Location = new System.Drawing.Point(117, 43);
            this.SHIKUCHOUSON_CD.MaxLength = 3;
            this.SHIKUCHOUSON_CD.Name = "SHIKUCHOUSON_CD";
            this.SHIKUCHOUSON_CD.PopupAfterExecute = null;
            this.SHIKUCHOUSON_CD.PopupAfterExecuteMethod = "";
            this.SHIKUCHOUSON_CD.PopupBeforeExecute = null;
            this.SHIKUCHOUSON_CD.PopupGetMasterField = "SHIKUCHOUSON_CD,SHIKUCHOUSON_NAME_RYAKU";
            this.SHIKUCHOUSON_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHIKUCHOUSON_CD.PopupSearchSendParams")));
            this.SHIKUCHOUSON_CD.PopupSendParams = new string[0];
            this.SHIKUCHOUSON_CD.PopupSetFormField = "SHIKUCHOUSON_CD,SHIKUCHOUSON_NAME_RYAKU";
            this.SHIKUCHOUSON_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHIKUCHOUSON;
            this.SHIKUCHOUSON_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.SHIKUCHOUSON_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHIKUCHOUSON_CD.popupWindowSetting")));
            this.SHIKUCHOUSON_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHIKUCHOUSON_CD.RegistCheckMethod")));
            this.SHIKUCHOUSON_CD.SetFormField = "SHIKUCHOUSON_CD,SHIKUCHOUSON_NAME_RYAKU";
            this.SHIKUCHOUSON_CD.ShortItemName = "";
            this.SHIKUCHOUSON_CD.Size = new System.Drawing.Size(64, 20);
            this.SHIKUCHOUSON_CD.TabIndex = 21;
            this.SHIKUCHOUSON_CD.Tag = "市区町村を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.SHIKUCHOUSON_CD.ZeroPaddengFlag = true;
            // 
            // SHIKUCHOUSON_NAME_RYAKU
            // 
            this.SHIKUCHOUSON_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.SHIKUCHOUSON_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHIKUCHOUSON_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.SHIKUCHOUSON_NAME_RYAKU.DBFieldsName = "";
            this.SHIKUCHOUSON_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHIKUCHOUSON_NAME_RYAKU.DisplayItemName = "";
            this.SHIKUCHOUSON_NAME_RYAKU.DisplayPopUp = null;
            this.SHIKUCHOUSON_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHIKUCHOUSON_NAME_RYAKU.FocusOutCheckMethod")));
            this.SHIKUCHOUSON_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHIKUCHOUSON_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.SHIKUCHOUSON_NAME_RYAKU.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SHIKUCHOUSON_NAME_RYAKU.IsInputErrorOccured = false;
            this.SHIKUCHOUSON_NAME_RYAKU.ItemDefinedTypes = "";
            this.SHIKUCHOUSON_NAME_RYAKU.Location = new System.Drawing.Point(180, 43);
            this.SHIKUCHOUSON_NAME_RYAKU.MaxLength = 0;
            this.SHIKUCHOUSON_NAME_RYAKU.Name = "SHIKUCHOUSON_NAME_RYAKU";
            this.SHIKUCHOUSON_NAME_RYAKU.PopupAfterExecute = null;
            this.SHIKUCHOUSON_NAME_RYAKU.PopupBeforeExecute = null;
            this.SHIKUCHOUSON_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHIKUCHOUSON_NAME_RYAKU.PopupSearchSendParams")));
            this.SHIKUCHOUSON_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHIKUCHOUSON_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHIKUCHOUSON_NAME_RYAKU.popupWindowSetting")));
            this.SHIKUCHOUSON_NAME_RYAKU.prevText = null;
            this.SHIKUCHOUSON_NAME_RYAKU.PrevText = null;
            this.SHIKUCHOUSON_NAME_RYAKU.ReadOnly = true;
            this.SHIKUCHOUSON_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHIKUCHOUSON_NAME_RYAKU.RegistCheckMethod")));
            this.SHIKUCHOUSON_NAME_RYAKU.ShortItemName = "";
            this.SHIKUCHOUSON_NAME_RYAKU.Size = new System.Drawing.Size(157, 20);
            this.SHIKUCHOUSON_NAME_RYAKU.TabIndex = 22;
            this.SHIKUCHOUSON_NAME_RYAKU.TabStop = false;
            this.SHIKUCHOUSON_NAME_RYAKU.Tag = "市区町村名が表示されます";
            // 
            // LBL_SHIKUCHOUSON
            // 
            this.LBL_SHIKUCHOUSON.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.LBL_SHIKUCHOUSON.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LBL_SHIKUCHOUSON.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LBL_SHIKUCHOUSON.ForeColor = System.Drawing.Color.White;
            this.LBL_SHIKUCHOUSON.Location = new System.Drawing.Point(2, 43);
            this.LBL_SHIKUCHOUSON.Name = "LBL_SHIKUCHOUSON";
            this.LBL_SHIKUCHOUSON.Size = new System.Drawing.Size(110, 20);
            this.LBL_SHIKUCHOUSON.TabIndex = 20;
            this.LBL_SHIKUCHOUSON.Text = "市区町村";
            this.LBL_SHIKUCHOUSON.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DGV_TEIKI_JISSEKI_HOUKOKU
            // 
            this.DGV_TEIKI_JISSEKI_HOUKOKU.AllowUserToAddRows = false;
            this.DGV_TEIKI_JISSEKI_HOUKOKU.AllowUserToDeleteRows = false;
            this.DGV_TEIKI_JISSEKI_HOUKOKU.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV_TEIKI_JISSEKI_HOUKOKU.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGV_TEIKI_JISSEKI_HOUKOKU.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_TEIKI_JISSEKI_HOUKOKU.ColumnHeadersVisible = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGV_TEIKI_JISSEKI_HOUKOKU.DefaultCellStyle = dataGridViewCellStyle2;
            this.DGV_TEIKI_JISSEKI_HOUKOKU.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DGV_TEIKI_JISSEKI_HOUKOKU.EnableHeadersVisualStyles = false;
            this.DGV_TEIKI_JISSEKI_HOUKOKU.GridColor = System.Drawing.Color.White;
            this.DGV_TEIKI_JISSEKI_HOUKOKU.IsReload = false;
            this.DGV_TEIKI_JISSEKI_HOUKOKU.LinkedDataPanelName = null;
            this.DGV_TEIKI_JISSEKI_HOUKOKU.Location = new System.Drawing.Point(2, 200);
            this.DGV_TEIKI_JISSEKI_HOUKOKU.MultiSelect = false;
            this.DGV_TEIKI_JISSEKI_HOUKOKU.Name = "DGV_TEIKI_JISSEKI_HOUKOKU";
            this.DGV_TEIKI_JISSEKI_HOUKOKU.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV_TEIKI_JISSEKI_HOUKOKU.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DGV_TEIKI_JISSEKI_HOUKOKU.RowHeadersVisible = false;
            this.DGV_TEIKI_JISSEKI_HOUKOKU.RowTemplate.Height = 21;
            this.DGV_TEIKI_JISSEKI_HOUKOKU.ShowCellToolTips = false;
            this.DGV_TEIKI_JISSEKI_HOUKOKU.Size = new System.Drawing.Size(985, 332);
            this.DGV_TEIKI_JISSEKI_HOUKOKU.TabIndex = 60;
            // 
            // SHUUKEI_KBN
            // 
            this.SHUUKEI_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.SHUUKEI_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHUUKEI_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUUKEI_KBN.DisplayItemName = "集計区分";
            this.SHUUKEI_KBN.DisplayPopUp = null;
            this.SHUUKEI_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_KBN.FocusOutCheckMethod")));
            this.SHUUKEI_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHUUKEI_KBN.ForeColor = System.Drawing.Color.Black;
            this.SHUUKEI_KBN.GetCodeMasterField = "";
            this.SHUUKEI_KBN.IsInputErrorOccured = false;
            this.SHUUKEI_KBN.LinkedRadioButtonArray = new string[] {
        "SHUUKEI_KBN_1",
        "SHUUKEI_KBN_2",
        "SHUUKEI_KBN_3"};
            this.SHUUKEI_KBN.Location = new System.Drawing.Point(117, 112);
            this.SHUUKEI_KBN.Name = "SHUUKEI_KBN";
            this.SHUUKEI_KBN.PopupAfterExecute = null;
            this.SHUUKEI_KBN.PopupBeforeExecute = null;
            this.SHUUKEI_KBN.PopupGetMasterField = "";
            this.SHUUKEI_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUUKEI_KBN.PopupSearchSendParams")));
            this.SHUUKEI_KBN.PopupSetFormField = "";
            this.SHUUKEI_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
            this.SHUUKEI_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUUKEI_KBN.popupWindowSetting")));
            this.SHUUKEI_KBN.prevText = "";
            this.SHUUKEI_KBN.PrevText = "";
            rangeSettingDto4.Max = new decimal(new int[] {
            3,
            0,
            0,
            0});
            rangeSettingDto4.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SHUUKEI_KBN.RangeSetting = rangeSettingDto4;
            this.SHUUKEI_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_KBN.RegistCheckMethod")));
            this.SHUUKEI_KBN.SetFormField = "";
            this.SHUUKEI_KBN.Size = new System.Drawing.Size(20, 20);
            this.SHUUKEI_KBN.TabIndex = 41;
            this.SHUUKEI_KBN.Tag = "【1～3】のいずれかで入力してください";
            this.SHUUKEI_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.SHUUKEI_KBN.WordWrap = false;
            // 
            // LBL_SHUUKEIKBN
            // 
            this.LBL_SHUUKEIKBN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.LBL_SHUUKEIKBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LBL_SHUUKEIKBN.Location = new System.Drawing.Point(2, 112);
            this.LBL_SHUUKEIKBN.Name = "LBL_SHUUKEIKBN";
            this.LBL_SHUUKEIKBN.Size = new System.Drawing.Size(110, 20);
            this.LBL_SHUUKEIKBN.TabIndex = 40;
            this.LBL_SHUUKEIKBN.Text = "集計区分※";
            this.LBL_SHUUKEIKBN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel1
            // 
            this.customPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel1.Controls.Add(this.SHUUKEI_KBN_3);
            this.customPanel1.Controls.Add(this.SHUUKEI_KBN_1);
            this.customPanel1.Controls.Add(this.SHUUKEI_KBN_2);
            this.customPanel1.Location = new System.Drawing.Point(136, 112);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(203, 60);
            this.customPanel1.TabIndex = 42;
            // 
            // SHUUKEI_KBN_3
            // 
            this.SHUUKEI_KBN_3.AutoSize = true;
            this.SHUUKEI_KBN_3.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUUKEI_KBN_3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_KBN_3.FocusOutCheckMethod")));
            this.SHUUKEI_KBN_3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHUUKEI_KBN_3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SHUUKEI_KBN_3.LinkedTextBox = "SHUUKEI_KBN";
            this.SHUUKEI_KBN_3.Location = new System.Drawing.Point(7, 40);
            this.SHUUKEI_KBN_3.Name = "SHUUKEI_KBN_3";
            this.SHUUKEI_KBN_3.PopupAfterExecute = null;
            this.SHUUKEI_KBN_3.PopupBeforeExecute = null;
            this.SHUUKEI_KBN_3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUUKEI_KBN_3.PopupSearchSendParams")));
            this.SHUUKEI_KBN_3.PopupSetFormField = "";
            this.SHUUKEI_KBN_3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUUKEI_KBN_3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUUKEI_KBN_3.popupWindowSetting")));
            this.SHUUKEI_KBN_3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_KBN_3.RegistCheckMethod")));
            this.SHUUKEI_KBN_3.SetFormField = "";
            this.SHUUKEI_KBN_3.Size = new System.Drawing.Size(123, 17);
            this.SHUUKEI_KBN_3.TabIndex = 45;
            this.SHUUKEI_KBN_3.Tag = "降先毎品名毎で集計する場合は、チェックを付けてください";
            this.SHUUKEI_KBN_3.Text = "3.降先毎品名毎";
            this.SHUUKEI_KBN_3.UseVisualStyleBackColor = true;
            this.SHUUKEI_KBN_3.Value = "3";
            // 
            // SHUUKEI_KBN_1
            // 
            this.SHUUKEI_KBN_1.AutoSize = true;
            this.SHUUKEI_KBN_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUUKEI_KBN_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_KBN_1.FocusOutCheckMethod")));
            this.SHUUKEI_KBN_1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHUUKEI_KBN_1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SHUUKEI_KBN_1.LinkedTextBox = "SHUUKEI_KBN";
            this.SHUUKEI_KBN_1.Location = new System.Drawing.Point(7, 0);
            this.SHUUKEI_KBN_1.Name = "SHUUKEI_KBN_1";
            this.SHUUKEI_KBN_1.PopupAfterExecute = null;
            this.SHUUKEI_KBN_1.PopupBeforeExecute = null;
            this.SHUUKEI_KBN_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUUKEI_KBN_1.PopupSearchSendParams")));
            this.SHUUKEI_KBN_1.PopupSetFormField = "";
            this.SHUUKEI_KBN_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUUKEI_KBN_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUUKEI_KBN_1.popupWindowSetting")));
            this.SHUUKEI_KBN_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_KBN_1.RegistCheckMethod")));
            this.SHUUKEI_KBN_1.SetFormField = "";
            this.SHUUKEI_KBN_1.Size = new System.Drawing.Size(123, 17);
            this.SHUUKEI_KBN_1.TabIndex = 43;
            this.SHUUKEI_KBN_1.Tag = "現場毎品名毎で集計する場合は、チェックを付けてください";
            this.SHUUKEI_KBN_1.Text = "1.現場毎品名毎";
            this.SHUUKEI_KBN_1.UseVisualStyleBackColor = true;
            this.SHUUKEI_KBN_1.Value = "1";
            // 
            // SHUUKEI_KBN_2
            // 
            this.SHUUKEI_KBN_2.AutoSize = true;
            this.SHUUKEI_KBN_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUUKEI_KBN_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_KBN_2.FocusOutCheckMethod")));
            this.SHUUKEI_KBN_2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHUUKEI_KBN_2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SHUUKEI_KBN_2.LinkedTextBox = "SHUUKEI_KBN";
            this.SHUUKEI_KBN_2.Location = new System.Drawing.Point(7, 20);
            this.SHUUKEI_KBN_2.Name = "SHUUKEI_KBN_2";
            this.SHUUKEI_KBN_2.PopupAfterExecute = null;
            this.SHUUKEI_KBN_2.PopupBeforeExecute = null;
            this.SHUUKEI_KBN_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUUKEI_KBN_2.PopupSearchSendParams")));
            this.SHUUKEI_KBN_2.PopupSetFormField = "";
            this.SHUUKEI_KBN_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUUKEI_KBN_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUUKEI_KBN_2.popupWindowSetting")));
            this.SHUUKEI_KBN_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_KBN_2.RegistCheckMethod")));
            this.SHUUKEI_KBN_2.SetFormField = "";
            this.SHUUKEI_KBN_2.Size = new System.Drawing.Size(165, 17);
            this.SHUUKEI_KBN_2.TabIndex = 44;
            this.SHUUKEI_KBN_2.Tag = "現場毎品名毎降先毎で集計する場合は、チェックを付けてください";
            this.SHUUKEI_KBN_2.Text = "2.現場毎品名毎降先毎";
            this.SHUUKEI_KBN_2.UseVisualStyleBackColor = true;
            this.SHUUKEI_KBN_2.Value = "2";
            // 
            // KIKAN_KBN
            // 
            this.KIKAN_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.KIKAN_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KIKAN_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.KIKAN_KBN.DisplayItemName = "期間";
            this.KIKAN_KBN.DisplayPopUp = null;
            this.KIKAN_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KIKAN_KBN.FocusOutCheckMethod")));
            this.KIKAN_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KIKAN_KBN.ForeColor = System.Drawing.Color.Black;
            this.KIKAN_KBN.GetCodeMasterField = "";
            this.KIKAN_KBN.IsInputErrorOccured = false;
            this.KIKAN_KBN.LinkedRadioButtonArray = new string[] {
        "KIKAN_KBN_1",
        "KIKAN_KBN_2"};
            this.KIKAN_KBN.Location = new System.Drawing.Point(117, 67);
            this.KIKAN_KBN.Name = "KIKAN_KBN";
            this.KIKAN_KBN.PopupAfterExecute = null;
            this.KIKAN_KBN.PopupBeforeExecute = null;
            this.KIKAN_KBN.PopupGetMasterField = "";
            this.KIKAN_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KIKAN_KBN.PopupSearchSendParams")));
            this.KIKAN_KBN.PopupSetFormField = "";
            this.KIKAN_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
            this.KIKAN_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KIKAN_KBN.popupWindowSetting")));
            this.KIKAN_KBN.prevText = "";
            this.KIKAN_KBN.PrevText = "";
            rangeSettingDto5.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto5.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.KIKAN_KBN.RangeSetting = rangeSettingDto5;
            this.KIKAN_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KIKAN_KBN.RegistCheckMethod")));
            this.KIKAN_KBN.SetFormField = "";
            this.KIKAN_KBN.Size = new System.Drawing.Size(20, 20);
            this.KIKAN_KBN.TabIndex = 31;
            this.KIKAN_KBN.Tag = "【1～2】のいずれかで入力してください";
            this.KIKAN_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.KIKAN_KBN.WordWrap = false;
            // 
            // customPanel3
            // 
            this.customPanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel3.Controls.Add(this.KIKAN_KBN_1);
            this.customPanel3.Controls.Add(this.KIKAN_KBN_2);
            this.customPanel3.Location = new System.Drawing.Point(136, 67);
            this.customPanel3.Name = "customPanel3";
            this.customPanel3.Size = new System.Drawing.Size(203, 20);
            this.customPanel3.TabIndex = 32;
            // 
            // KIKAN_KBN_1
            // 
            this.KIKAN_KBN_1.AutoSize = true;
            this.KIKAN_KBN_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.KIKAN_KBN_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KIKAN_KBN_1.FocusOutCheckMethod")));
            this.KIKAN_KBN_1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KIKAN_KBN_1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.KIKAN_KBN_1.LinkedTextBox = "KIKAN_KBN";
            this.KIKAN_KBN_1.Location = new System.Drawing.Point(7, 0);
            this.KIKAN_KBN_1.Name = "KIKAN_KBN_1";
            this.KIKAN_KBN_1.PopupAfterExecute = null;
            this.KIKAN_KBN_1.PopupBeforeExecute = null;
            this.KIKAN_KBN_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KIKAN_KBN_1.PopupSearchSendParams")));
            this.KIKAN_KBN_1.PopupSetFormField = "";
            this.KIKAN_KBN_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KIKAN_KBN_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KIKAN_KBN_1.popupWindowSetting")));
            this.KIKAN_KBN_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KIKAN_KBN_1.RegistCheckMethod")));
            this.KIKAN_KBN_1.SetFormField = "";
            this.KIKAN_KBN_1.Size = new System.Drawing.Size(95, 17);
            this.KIKAN_KBN_1.TabIndex = 33;
            this.KIKAN_KBN_1.Tag = "期間を「期間合算」にする場合は、チェックを付けてください";
            this.KIKAN_KBN_1.Text = "1.期間合算";
            this.KIKAN_KBN_1.UseVisualStyleBackColor = true;
            this.KIKAN_KBN_1.Value = "1";
            // 
            // KIKAN_KBN_2
            // 
            this.KIKAN_KBN_2.AutoSize = true;
            this.KIKAN_KBN_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.KIKAN_KBN_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KIKAN_KBN_2.FocusOutCheckMethod")));
            this.KIKAN_KBN_2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KIKAN_KBN_2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.KIKAN_KBN_2.LinkedTextBox = "KIKAN_KBN";
            this.KIKAN_KBN_2.Location = new System.Drawing.Point(110, 0);
            this.KIKAN_KBN_2.Name = "KIKAN_KBN_2";
            this.KIKAN_KBN_2.PopupAfterExecute = null;
            this.KIKAN_KBN_2.PopupBeforeExecute = null;
            this.KIKAN_KBN_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KIKAN_KBN_2.PopupSearchSendParams")));
            this.KIKAN_KBN_2.PopupSetFormField = "";
            this.KIKAN_KBN_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KIKAN_KBN_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KIKAN_KBN_2.popupWindowSetting")));
            this.KIKAN_KBN_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KIKAN_KBN_2.RegistCheckMethod")));
            this.KIKAN_KBN_2.SetFormField = "";
            this.KIKAN_KBN_2.Size = new System.Drawing.Size(81, 17);
            this.KIKAN_KBN_2.TabIndex = 34;
            this.KIKAN_KBN_2.Tag = "期間を「月合算」にする場合は、チェックを付けてください";
            this.KIKAN_KBN_2.Text = "2.月合算";
            this.KIKAN_KBN_2.UseVisualStyleBackColor = true;
            this.KIKAN_KBN_2.Value = "2";
            this.KIKAN_KBN_2.CheckedChanged += new System.EventHandler(this.KIKAN_KBN_2_CheckedChanged);
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
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(354, 20);
            this.ISNOT_NEED_DELETE_FLG.Name = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.PopupAfterExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupBeforeExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.PopupSearchSendParams")));
            this.ISNOT_NEED_DELETE_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ISNOT_NEED_DELETE_FLG.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.popupWindowSetting")));
            this.ISNOT_NEED_DELETE_FLG.prevText = null;
            this.ISNOT_NEED_DELETE_FLG.PrevText = null;
            this.ISNOT_NEED_DELETE_FLG.ReadOnly = true;
            this.ISNOT_NEED_DELETE_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.RegistCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.Size = new System.Drawing.Size(20, 20);
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 20028;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(999, 660);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.LBL_KYOTEN);
            this.Controls.Add(this.KYOTEN_CD);
            this.Controls.Add(this.KYOTEN_NAME);
            this.Controls.Add(this.LBL_SHIKUCHOUSON);
            this.Controls.Add(this.SHIKUCHOUSON_CD);
            this.Controls.Add(this.SHIKUCHOUSON_NAME_RYAKU);
            this.Controls.Add(this.LBL_KIKAN);
            this.Controls.Add(this.KIKAN_KBN);
            this.Controls.Add(this.customPanel3);
            this.Controls.Add(this.KIKAN_FROM);
            this.Controls.Add(this.lbl_FromTo);
            this.Controls.Add(this.KIKAN_TO);
            this.Controls.Add(this.LBL_SHUUKEIKBN);
            this.Controls.Add(this.SHUUKEI_KBN);
            this.Controls.Add(this.customPanel1);
            this.Controls.Add(this.DGV_TEIKI_JISSEKI_HOUKOKU);
            this.Controls.Add(this.LBL_SHUUKEISYUURYOU);
            this.Controls.Add(this.SHUUKEISYUURYOU);
            this.Controls.Add(this.customPanel2);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "UIForm";
            this.Text = "UIForm";
            this.customPanel2.ResumeLayout(false);
            this.customPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_TEIKI_JISSEKI_HOUKOKU)).EndInit();
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.customPanel3.ResumeLayout(false);
            this.customPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LBL_KIKAN;
        private System.Windows.Forms.Label lbl_FromTo;
        internal r_framework.CustomControl.CustomDateTimePicker KIKAN_TO;
        internal r_framework.CustomControl.CustomDateTimePicker KIKAN_FROM;
        internal r_framework.CustomControl.CustomTextBox KYOTEN_NAME;
        internal r_framework.CustomControl.CustomNumericTextBox2 KYOTEN_CD;
        private System.Windows.Forms.Label LBL_KYOTEN;
		internal r_framework.CustomControl.CustomRadioButton SHUUKEISYUURYOU_1;
		internal r_framework.CustomControl.CustomNumericTextBox2 SHUUKEISYUURYOU;
		private System.Windows.Forms.Label LBL_SHUUKEISYUURYOU;
		private r_framework.CustomControl.CustomPanel customPanel2;
		internal r_framework.CustomControl.CustomRadioButton SHUUKEISYUURYOU_2;
        internal r_framework.CustomControl.CustomAlphaNumTextBox SHIKUCHOUSON_CD;
        internal r_framework.CustomControl.CustomTextBox SHIKUCHOUSON_NAME_RYAKU;
        private System.Windows.Forms.Label LBL_SHIKUCHOUSON;
        internal r_framework.CustomControl.CustomDataGridView DGV_TEIKI_JISSEKI_HOUKOKU;
        internal r_framework.CustomControl.CustomNumericTextBox2 SHUUKEI_KBN;
        private System.Windows.Forms.Label LBL_SHUUKEIKBN;
        private r_framework.CustomControl.CustomPanel customPanel1;
        internal r_framework.CustomControl.CustomRadioButton SHUUKEI_KBN_1;
        internal r_framework.CustomControl.CustomRadioButton SHUUKEI_KBN_2;
        internal r_framework.CustomControl.CustomRadioButton SHUUKEI_KBN_3;
        internal r_framework.CustomControl.CustomNumericTextBox2 KIKAN_KBN;
        private r_framework.CustomControl.CustomPanel customPanel3;
        internal r_framework.CustomControl.CustomRadioButton KIKAN_KBN_1;
        internal r_framework.CustomControl.CustomRadioButton KIKAN_KBN_2;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
    }
}