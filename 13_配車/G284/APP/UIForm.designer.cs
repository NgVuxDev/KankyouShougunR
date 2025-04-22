namespace Shougun.Core.Allocation.MobileShougunShutsuryoku.APP
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
            this.txt_KyotenName = new r_framework.CustomControl.CustomTextBox();
            this.txt_KyotenCD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.lbl_Kyoten = new System.Windows.Forms.Label();
            this.dtp_TaishoKikanTo = new r_framework.CustomControl.CustomDateTimePicker();
            this.lbl_FromTo = new System.Windows.Forms.Label();
            this.lbl_Sagyobi = new System.Windows.Forms.Label();
            this.dtp_TaishoKikanFrom = new r_framework.CustomControl.CustomDateTimePicker();
            this.lbl_Sagyokubun = new System.Windows.Forms.Label();
            this.txt_Sagyokubun = new r_framework.CustomControl.CustomNumericTextBox2();
            this.rdo_Haisha = new r_framework.CustomControl.CustomRadioButton();
            this.rdo_All = new r_framework.CustomControl.CustomRadioButton();
            this.rdo_Master = new r_framework.CustomControl.CustomRadioButton();
            this.customPanel2 = new r_framework.CustomControl.CustomPanel();
            this.customPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_KyotenName
            // 
            this.txt_KyotenName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txt_KyotenName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_KyotenName.CharactersNumber = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txt_KyotenName.DBFieldsName = "";
            this.txt_KyotenName.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_KyotenName.DisplayPopUp = null;
            this.txt_KyotenName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_KyotenName.FocusOutCheckMethod")));
            this.txt_KyotenName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_KyotenName.ForeColor = System.Drawing.Color.Black;
            this.txt_KyotenName.IsInputErrorOccured = false;
            this.txt_KyotenName.Location = new System.Drawing.Point(222, 102);
            this.txt_KyotenName.MaxLength = 10;
            this.txt_KyotenName.Name = "txt_KyotenName";
            this.txt_KyotenName.PopupAfterExecute = null;
            this.txt_KyotenName.PopupBeforeExecute = null;
            this.txt_KyotenName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_KyotenName.PopupSearchSendParams")));
            this.txt_KyotenName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_KyotenName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_KyotenName.popupWindowSetting")));
            this.txt_KyotenName.prevText = null;
            this.txt_KyotenName.PrevText = null;
            this.txt_KyotenName.ReadOnly = true;
            this.txt_KyotenName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_KyotenName.RegistCheckMethod")));
            this.txt_KyotenName.Size = new System.Drawing.Size(254, 20);
            this.txt_KyotenName.TabIndex = 302;
            this.txt_KyotenName.TabStop = false;
            this.txt_KyotenName.Tag = "拠点名が表示されます";
            // 
            // txt_KyotenCD
            // 
            this.txt_KyotenCD.BackColor = System.Drawing.SystemColors.Window;
            this.txt_KyotenCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_KyotenCD.CustomFormatSetting = "00";
            this.txt_KyotenCD.DBFieldsName = "KYOTEN_CD";
            this.txt_KyotenCD.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_KyotenCD.DisplayItemName = "";
            this.txt_KyotenCD.DisplayPopUp = null;
            this.txt_KyotenCD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_KyotenCD.FocusOutCheckMethod")));
            this.txt_KyotenCD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txt_KyotenCD.ForeColor = System.Drawing.Color.Black;
            this.txt_KyotenCD.FormatSetting = "カスタム";
            this.txt_KyotenCD.GetCodeMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.txt_KyotenCD.IsInputErrorOccured = false;
            this.txt_KyotenCD.ItemDefinedTypes = "smallint";
            this.txt_KyotenCD.LinkedRadioButtonArray = new string[0];
            this.txt_KyotenCD.Location = new System.Drawing.Point(147, 102);
            this.txt_KyotenCD.Name = "txt_KyotenCD";
            this.txt_KyotenCD.PopupAfterExecute = null;
            this.txt_KyotenCD.PopupBeforeExecute = null;
            this.txt_KyotenCD.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.txt_KyotenCD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_KyotenCD.PopupSearchSendParams")));
            this.txt_KyotenCD.PopupSendParams = new string[0];
            this.txt_KyotenCD.PopupSetFormField = "txt_KyotenCD,txt_KyotenName";
            this.txt_KyotenCD.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.txt_KyotenCD.PopupWindowName = "マスタ共通ポップアップ";
            this.txt_KyotenCD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_KyotenCD.popupWindowSetting")));
            this.txt_KyotenCD.prevText = "";
            this.txt_KyotenCD.PrevText = "";
            rangeSettingDto1.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.txt_KyotenCD.RangeSetting = rangeSettingDto1;
            this.txt_KyotenCD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_KyotenCD.RegistCheckMethod")));
            this.txt_KyotenCD.SetFormField = "txt_KyotenCD,txt_KyotenName";
            this.txt_KyotenCD.Size = new System.Drawing.Size(74, 20);
            this.txt_KyotenCD.TabIndex = 301;
            this.txt_KyotenCD.Tag = "拠点を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.txt_KyotenCD.WordWrap = false;
            // 
            // lbl_Kyoten
            // 
            this.lbl_Kyoten.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Kyoten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Kyoten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Kyoten.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Kyoten.ForeColor = System.Drawing.Color.White;
            this.lbl_Kyoten.Location = new System.Drawing.Point(17, 102);
            this.lbl_Kyoten.Name = "lbl_Kyoten";
            this.lbl_Kyoten.Size = new System.Drawing.Size(128, 20);
            this.lbl_Kyoten.TabIndex = 300;
            this.lbl_Kyoten.Text = "拠点";
            this.lbl_Kyoten.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtp_TaishoKikanTo
            // 
            this.dtp_TaishoKikanTo.BackColor = System.Drawing.SystemColors.Window;
            this.dtp_TaishoKikanTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtp_TaishoKikanTo.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.dtp_TaishoKikanTo.Checked = false;
            this.dtp_TaishoKikanTo.CustomFormat = "yyyy/MM/dd ddd";
            this.dtp_TaishoKikanTo.DateTimeNowYear = "";
            this.dtp_TaishoKikanTo.DefaultBackColor = System.Drawing.Color.Empty;
            this.dtp_TaishoKikanTo.DisplayPopUp = null;
            this.dtp_TaishoKikanTo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtp_TaishoKikanTo.FocusOutCheckMethod")));
            this.dtp_TaishoKikanTo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.dtp_TaishoKikanTo.ForeColor = System.Drawing.Color.Black;
            this.dtp_TaishoKikanTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_TaishoKikanTo.IsInputErrorOccured = false;
            this.dtp_TaishoKikanTo.Location = new System.Drawing.Point(298, 80);
            this.dtp_TaishoKikanTo.MaxLength = 10;
            this.dtp_TaishoKikanTo.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_TaishoKikanTo.Name = "dtp_TaishoKikanTo";
            this.dtp_TaishoKikanTo.NullValue = "";
            this.dtp_TaishoKikanTo.PopupAfterExecute = null;
            this.dtp_TaishoKikanTo.PopupBeforeExecute = null;
            this.dtp_TaishoKikanTo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dtp_TaishoKikanTo.PopupSearchSendParams")));
            this.dtp_TaishoKikanTo.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dtp_TaishoKikanTo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dtp_TaishoKikanTo.popupWindowSetting")));
            this.dtp_TaishoKikanTo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtp_TaishoKikanTo.RegistCheckMethod")));
            this.dtp_TaishoKikanTo.Size = new System.Drawing.Size(125, 20);
            this.dtp_TaishoKikanTo.TabIndex = 203;
            this.dtp_TaishoKikanTo.Tag = "作業終了日を入力してください";
            this.dtp_TaishoKikanTo.Text = "2013/11/10(日)";
            this.dtp_TaishoKikanTo.Value = new System.DateTime(2013, 11, 10, 0, 0, 0, 0);
            // 
            // lbl_FromTo
            // 
            this.lbl_FromTo.BackColor = System.Drawing.Color.Transparent;
            this.lbl_FromTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_FromTo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_FromTo.ForeColor = System.Drawing.Color.Black;
            this.lbl_FromTo.Location = new System.Drawing.Point(276, 79);
            this.lbl_FromTo.Name = "lbl_FromTo";
            this.lbl_FromTo.Size = new System.Drawing.Size(22, 22);
            this.lbl_FromTo.TabIndex = 202;
            this.lbl_FromTo.Text = "～";
            this.lbl_FromTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Sagyobi
            // 
            this.lbl_Sagyobi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Sagyobi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Sagyobi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Sagyobi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Sagyobi.ForeColor = System.Drawing.Color.White;
            this.lbl_Sagyobi.Location = new System.Drawing.Point(17, 80);
            this.lbl_Sagyobi.Name = "lbl_Sagyobi";
            this.lbl_Sagyobi.Size = new System.Drawing.Size(128, 20);
            this.lbl_Sagyobi.TabIndex = 200;
            this.lbl_Sagyobi.Text = "作業日※";
            this.lbl_Sagyobi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtp_TaishoKikanFrom
            // 
            this.dtp_TaishoKikanFrom.BackColor = System.Drawing.SystemColors.Window;
            this.dtp_TaishoKikanFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtp_TaishoKikanFrom.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.dtp_TaishoKikanFrom.Checked = false;
            this.dtp_TaishoKikanFrom.CustomFormat = "yyyy/MM/dd ddd";
            this.dtp_TaishoKikanFrom.DateTimeNowYear = "";
            this.dtp_TaishoKikanFrom.DefaultBackColor = System.Drawing.Color.Empty;
            this.dtp_TaishoKikanFrom.DisplayPopUp = null;
            this.dtp_TaishoKikanFrom.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtp_TaishoKikanFrom.FocusOutCheckMethod")));
            this.dtp_TaishoKikanFrom.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.dtp_TaishoKikanFrom.ForeColor = System.Drawing.Color.Black;
            this.dtp_TaishoKikanFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_TaishoKikanFrom.IsInputErrorOccured = false;
            this.dtp_TaishoKikanFrom.Location = new System.Drawing.Point(147, 80);
            this.dtp_TaishoKikanFrom.MaxLength = 10;
            this.dtp_TaishoKikanFrom.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_TaishoKikanFrom.Name = "dtp_TaishoKikanFrom";
            this.dtp_TaishoKikanFrom.NullValue = "";
            this.dtp_TaishoKikanFrom.PopupAfterExecute = null;
            this.dtp_TaishoKikanFrom.PopupBeforeExecute = null;
            this.dtp_TaishoKikanFrom.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dtp_TaishoKikanFrom.PopupSearchSendParams")));
            this.dtp_TaishoKikanFrom.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dtp_TaishoKikanFrom.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dtp_TaishoKikanFrom.popupWindowSetting")));
            this.dtp_TaishoKikanFrom.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtp_TaishoKikanFrom.RegistCheckMethod")));
            this.dtp_TaishoKikanFrom.Size = new System.Drawing.Size(125, 20);
            this.dtp_TaishoKikanFrom.TabIndex = 201;
            this.dtp_TaishoKikanFrom.Tag = "作業開始日を入力してください";
            this.dtp_TaishoKikanFrom.Text = "2013/11/10(日)";
            this.dtp_TaishoKikanFrom.Value = new System.DateTime(2013, 11, 10, 0, 0, 0, 0);
            // 
            // lbl_Sagyokubun
            // 
            this.lbl_Sagyokubun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Sagyokubun.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Sagyokubun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Sagyokubun.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Sagyokubun.ForeColor = System.Drawing.Color.White;
            this.lbl_Sagyokubun.Location = new System.Drawing.Point(17, 58);
            this.lbl_Sagyokubun.Name = "lbl_Sagyokubun";
            this.lbl_Sagyokubun.Size = new System.Drawing.Size(128, 20);
            this.lbl_Sagyokubun.TabIndex = 100;
            this.lbl_Sagyokubun.Text = "作業区分";
            this.lbl_Sagyokubun.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_Sagyokubun
            // 
            this.txt_Sagyokubun.BackColor = System.Drawing.SystemColors.Window;
            this.txt_Sagyokubun.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Sagyokubun.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_Sagyokubun.DisplayPopUp = null;
            this.txt_Sagyokubun.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Sagyokubun.FocusOutCheckMethod")));
            this.txt_Sagyokubun.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txt_Sagyokubun.ForeColor = System.Drawing.Color.Black;
            this.txt_Sagyokubun.GetCodeMasterField = "";
            this.txt_Sagyokubun.IsInputErrorOccured = false;
            this.txt_Sagyokubun.LinkedRadioButtonArray = new string[] {
        "rdo_Master",
        "rdo_Haisha",
        "rdo_All"};
            this.txt_Sagyokubun.Location = new System.Drawing.Point(148, 59);
            this.txt_Sagyokubun.Multiline = true;
            this.txt_Sagyokubun.Name = "txt_Sagyokubun";
            this.txt_Sagyokubun.PopupAfterExecute = null;
            this.txt_Sagyokubun.PopupBeforeExecute = null;
            this.txt_Sagyokubun.PopupGetMasterField = "";
            this.txt_Sagyokubun.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_Sagyokubun.PopupSearchSendParams")));
            this.txt_Sagyokubun.PopupSetFormField = "";
            this.txt_Sagyokubun.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_Sagyokubun.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_Sagyokubun.popupWindowSetting")));
            this.txt_Sagyokubun.prevText = "";
            this.txt_Sagyokubun.PrevText = "";
            rangeSettingDto2.Max = new decimal(new int[] {
            3,
            0,
            0,
            0});
            rangeSettingDto2.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txt_Sagyokubun.RangeSetting = rangeSettingDto2;
            this.txt_Sagyokubun.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Sagyokubun.RegistCheckMethod")));
            this.txt_Sagyokubun.SetFormField = "";
            this.txt_Sagyokubun.Size = new System.Drawing.Size(18, 17);
            this.txt_Sagyokubun.TabIndex = 101;
            this.txt_Sagyokubun.Tag = "【1、2、3】のいずれかで作業区分を入力してください";
            this.txt_Sagyokubun.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_Sagyokubun.WordWrap = false;
            // 
            // rdo_Haisha
            // 
            this.rdo_Haisha.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdo_Haisha.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_Haisha.FocusOutCheckMethod")));
            this.rdo_Haisha.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.rdo_Haisha.LinkedTextBox = "txt_Sagyokubun";
            this.rdo_Haisha.Location = new System.Drawing.Point(155, 1);
            this.rdo_Haisha.Name = "rdo_Haisha";
            this.rdo_Haisha.PopupAfterExecute = null;
            this.rdo_Haisha.PopupBeforeExecute = null;
            this.rdo_Haisha.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdo_Haisha.PopupSearchSendParams")));
            this.rdo_Haisha.PopupSetFormField = "";
            this.rdo_Haisha.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdo_Haisha.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdo_Haisha.popupWindowSetting")));
            this.rdo_Haisha.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_Haisha.RegistCheckMethod")));
            this.rdo_Haisha.SetFormField = "";
            this.rdo_Haisha.Size = new System.Drawing.Size(123, 17);
            this.rdo_Haisha.TabIndex = 103;
            this.rdo_Haisha.Text = "2.配車ファイル";
            this.rdo_Haisha.UseVisualStyleBackColor = true;
            this.rdo_Haisha.Value = "2";
            // 
            // rdo_All
            // 
            this.rdo_All.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdo_All.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_All.FocusOutCheckMethod")));
            this.rdo_All.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.rdo_All.LinkedTextBox = "txt_Sagyokubun";
            this.rdo_All.Location = new System.Drawing.Point(268, 1);
            this.rdo_All.Name = "rdo_All";
            this.rdo_All.PopupAfterExecute = null;
            this.rdo_All.PopupBeforeExecute = null;
            this.rdo_All.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdo_All.PopupSearchSendParams")));
            this.rdo_All.PopupSetFormField = "";
            this.rdo_All.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdo_All.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdo_All.popupWindowSetting")));
            this.rdo_All.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_All.RegistCheckMethod")));
            this.rdo_All.SetFormField = "";
            this.rdo_All.Size = new System.Drawing.Size(60, 17);
            this.rdo_All.TabIndex = 104;
            this.rdo_All.Text = "3.全て";
            this.rdo_All.UseVisualStyleBackColor = true;
            this.rdo_All.Value = "3";
            // 
            // rdo_Master
            // 
            this.rdo_Master.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdo_Master.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_Master.FocusOutCheckMethod")));
            this.rdo_Master.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.rdo_Master.LinkedTextBox = "txt_Sagyokubun";
            this.rdo_Master.Location = new System.Drawing.Point(22, 1);
            this.rdo_Master.Name = "rdo_Master";
            this.rdo_Master.PopupAfterExecute = null;
            this.rdo_Master.PopupBeforeExecute = null;
            this.rdo_Master.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdo_Master.PopupSearchSendParams")));
            this.rdo_Master.PopupSetFormField = "";
            this.rdo_Master.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdo_Master.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdo_Master.popupWindowSetting")));
            this.rdo_Master.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_Master.RegistCheckMethod")));
            this.rdo_Master.SetFormField = "";
            this.rdo_Master.Size = new System.Drawing.Size(130, 17);
            this.rdo_Master.TabIndex = 102;
            this.rdo_Master.Text = "1.マスタファイル";
            this.rdo_Master.UseVisualStyleBackColor = true;
            this.rdo_Master.Value = "1";
            // 
            // customPanel2
            // 
            this.customPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel2.Controls.Add(this.rdo_All);
            this.customPanel2.Controls.Add(this.rdo_Master);
            this.customPanel2.Controls.Add(this.rdo_Haisha);
            this.customPanel2.Location = new System.Drawing.Point(147, 58);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(342, 19);
            this.customPanel2.TabIndex = 610;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(952, 715);
            this.Controls.Add(this.txt_Sagyokubun);
            this.Controls.Add(this.lbl_Sagyokubun);
            this.Controls.Add(this.dtp_TaishoKikanTo);
            this.Controls.Add(this.lbl_FromTo);
            this.Controls.Add(this.lbl_Sagyobi);
            this.Controls.Add(this.dtp_TaishoKikanFrom);
            this.Controls.Add(this.txt_KyotenName);
            this.Controls.Add(this.txt_KyotenCD);
            this.Controls.Add(this.lbl_Kyoten);
            this.Controls.Add(this.customPanel2);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Name = "UIForm";
            this.Tag = "";
            this.Text = "UIForm";
            this.customPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Kyoten;
        private System.Windows.Forms.Label lbl_FromTo;
        private System.Windows.Forms.Label lbl_Sagyobi;
        private System.Windows.Forms.Label lbl_Sagyokubun;
        internal r_framework.CustomControl.CustomDateTimePicker dtp_TaishoKikanFrom;
        internal r_framework.CustomControl.CustomDateTimePicker dtp_TaishoKikanTo;
        internal r_framework.CustomControl.CustomNumericTextBox2 txt_Sagyokubun;
        internal r_framework.CustomControl.CustomTextBox txt_KyotenName;
        internal r_framework.CustomControl.CustomNumericTextBox2 txt_KyotenCD;
        internal r_framework.CustomControl.CustomRadioButton rdo_Haisha;
        internal r_framework.CustomControl.CustomRadioButton rdo_All;
        internal r_framework.CustomControl.CustomRadioButton rdo_Master;
        private r_framework.CustomControl.CustomPanel customPanel2;

    }
}