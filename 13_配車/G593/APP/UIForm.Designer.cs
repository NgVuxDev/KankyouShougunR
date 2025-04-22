namespace Shougun.Core.Allocation.KaraContenaIchiranHyou
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
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.label8 = new System.Windows.Forms.Label();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.customPanel2 = new r_framework.CustomControl.CustomPanel();
            this.JuchuHaishaHukumu2 = new r_framework.CustomControl.CustomRadioButton();
            this.JuchuHaishaHukumu1 = new r_framework.CustomControl.CustomRadioButton();
            this.JuchuHaishaHukumu = new r_framework.CustomControl.CustomNumericTextBox2();
            this.Kijunbi = new r_framework.CustomControl.CustomDateTimePicker();
            this.customPanelSyukeiKomoku8 = new r_framework.CustomControl.CustomPanel();
            this.ContenaShuruiEndSearch = new r_framework.CustomControl.CustomPopupOpenButton();
            this.ContenaShuruiMeiEnd = new r_framework.CustomControl.CustomTextBox();
            this.ContenaShuruiCDEnd = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.ContenaShuruiStartSearch = new r_framework.CustomControl.CustomPopupOpenButton();
            this.ContenaShuruiMeiStart = new r_framework.CustomControl.CustomTextBox();
            this.ContenaShuruiCDStart = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.customPanel1.SuspendLayout();
            this.customPanel2.SuspendLayout();
            this.customPanelSyukeiKomoku8.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(123, 20);
            this.label8.TabIndex = 10004;
            this.label8.Text = "抽出範囲";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel1
            // 
            this.customPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel1.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.customPanel1.Controls.Add(this.customPanel2);
            this.customPanel1.Controls.Add(this.Kijunbi);
            this.customPanel1.Controls.Add(this.customPanelSyukeiKomoku8);
            this.customPanel1.Controls.Add(this.label8);
            this.customPanel1.Controls.Add(this.label2);
            this.customPanel1.Controls.Add(this.label1);
            this.customPanel1.Location = new System.Drawing.Point(1, 4);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(735, 111);
            this.customPanel1.TabIndex = 10005;
            // 
            // customPanel2
            // 
            this.customPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel2.Controls.Add(this.JuchuHaishaHukumu2);
            this.customPanel2.Controls.Add(this.JuchuHaishaHukumu1);
            this.customPanel2.Controls.Add(this.JuchuHaishaHukumu);
            this.customPanel2.Location = new System.Drawing.Point(148, 47);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(234, 20);
            this.customPanel2.TabIndex = 2;
            // 
            // JuchuHaishaHukumu2
            // 
            this.JuchuHaishaHukumu2.AutoSize = true;
            this.JuchuHaishaHukumu2.DefaultBackColor = System.Drawing.Color.Empty;
            this.JuchuHaishaHukumu2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JuchuHaishaHukumu2.FocusOutCheckMethod")));
            this.JuchuHaishaHukumu2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.JuchuHaishaHukumu2.LinkedTextBox = "JuchuHaishaHukumu";
            this.JuchuHaishaHukumu2.Location = new System.Drawing.Point(130, 1);
            this.JuchuHaishaHukumu2.Name = "JuchuHaishaHukumu2";
            this.JuchuHaishaHukumu2.PopupAfterExecute = null;
            this.JuchuHaishaHukumu2.PopupBeforeExecute = null;
            this.JuchuHaishaHukumu2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JuchuHaishaHukumu2.PopupSearchSendParams")));
            this.JuchuHaishaHukumu2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JuchuHaishaHukumu2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JuchuHaishaHukumu2.popupWindowSetting")));
            this.JuchuHaishaHukumu2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JuchuHaishaHukumu2.RegistCheckMethod")));
            this.JuchuHaishaHukumu2.Size = new System.Drawing.Size(95, 17);
            this.JuchuHaishaHukumu2.TabIndex = 3;
            this.JuchuHaishaHukumu2.Text = "2.含まない";
            this.JuchuHaishaHukumu2.UseVisualStyleBackColor = true;
            this.JuchuHaishaHukumu2.Value = "2";
            // 
            // JuchuHaishaHukumu1
            // 
            this.JuchuHaishaHukumu1.AutoSize = true;
            this.JuchuHaishaHukumu1.DefaultBackColor = System.Drawing.Color.Empty;
            this.JuchuHaishaHukumu1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JuchuHaishaHukumu1.FocusOutCheckMethod")));
            this.JuchuHaishaHukumu1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.JuchuHaishaHukumu1.LinkedTextBox = "JuchuHaishaHukumu";
            this.JuchuHaishaHukumu1.Location = new System.Drawing.Point(50, 1);
            this.JuchuHaishaHukumu1.Name = "JuchuHaishaHukumu1";
            this.JuchuHaishaHukumu1.PopupAfterExecute = null;
            this.JuchuHaishaHukumu1.PopupBeforeExecute = null;
            this.JuchuHaishaHukumu1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JuchuHaishaHukumu1.PopupSearchSendParams")));
            this.JuchuHaishaHukumu1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JuchuHaishaHukumu1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JuchuHaishaHukumu1.popupWindowSetting")));
            this.JuchuHaishaHukumu1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JuchuHaishaHukumu1.RegistCheckMethod")));
            this.JuchuHaishaHukumu1.Size = new System.Drawing.Size(67, 17);
            this.JuchuHaishaHukumu1.TabIndex = 2;
            this.JuchuHaishaHukumu1.Text = "1.含む";
            this.JuchuHaishaHukumu1.UseVisualStyleBackColor = true;
            this.JuchuHaishaHukumu1.Value = "1";
            // 
            // JuchuHaishaHukumu
            // 
            this.JuchuHaishaHukumu.BackColor = System.Drawing.SystemColors.Window;
            this.JuchuHaishaHukumu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JuchuHaishaHukumu.DefaultBackColor = System.Drawing.Color.Empty;
            this.JuchuHaishaHukumu.DisplayPopUp = null;
            this.JuchuHaishaHukumu.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JuchuHaishaHukumu.FocusOutCheckMethod")));
            this.JuchuHaishaHukumu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.JuchuHaishaHukumu.ForeColor = System.Drawing.Color.Black;
            this.JuchuHaishaHukumu.IsInputErrorOccured = false;
            this.JuchuHaishaHukumu.LinkedRadioButtonArray = new string[] {
        "JuchuHaishaHukumu1",
        "JuchuHaishaHukumu2"};
            this.JuchuHaishaHukumu.Location = new System.Drawing.Point(-1, -1);
            this.JuchuHaishaHukumu.Name = "JuchuHaishaHukumu";
            this.JuchuHaishaHukumu.PopupAfterExecute = null;
            this.JuchuHaishaHukumu.PopupBeforeExecute = null;
            this.JuchuHaishaHukumu.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JuchuHaishaHukumu.PopupSearchSendParams")));
            this.JuchuHaishaHukumu.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JuchuHaishaHukumu.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JuchuHaishaHukumu.popupWindowSetting")));
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
            this.JuchuHaishaHukumu.RangeSetting = rangeSettingDto2;
            this.JuchuHaishaHukumu.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JuchuHaishaHukumu.RegistCheckMethod")));
            this.JuchuHaishaHukumu.Size = new System.Drawing.Size(29, 20);
            this.JuchuHaishaHukumu.TabIndex = 1;
            this.JuchuHaishaHukumu.Tag = "収集受付に登録されている配車状況：受注または配車のデータを含む場合は【1】を入力してください";
            this.JuchuHaishaHukumu.WordWrap = false;
            // 
            // Kijunbi
            // 
            this.Kijunbi.BackColor = System.Drawing.SystemColors.Window;
            this.Kijunbi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Kijunbi.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Kijunbi.Checked = false;
            this.Kijunbi.DateTimeNowYear = "";
            this.Kijunbi.DefaultBackColor = System.Drawing.Color.Empty;
            this.Kijunbi.DisplayPopUp = null;
            this.Kijunbi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Kijunbi.FocusOutCheckMethod")));
            this.Kijunbi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Kijunbi.ForeColor = System.Drawing.Color.Black;
            this.Kijunbi.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.Kijunbi.IsInputErrorOccured = false;
            this.Kijunbi.Location = new System.Drawing.Point(148, 25);
            this.Kijunbi.MaxLength = 10;
            this.Kijunbi.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.Kijunbi.Name = "Kijunbi";
            this.Kijunbi.NullValue = "";
            this.Kijunbi.PopupAfterExecute = null;
            this.Kijunbi.PopupBeforeExecute = null;
            this.Kijunbi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Kijunbi.PopupSearchSendParams")));
            this.Kijunbi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Kijunbi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Kijunbi.popupWindowSetting")));
            this.Kijunbi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Kijunbi.RegistCheckMethod")));
            this.Kijunbi.Size = new System.Drawing.Size(138, 20);
            this.Kijunbi.TabIndex = 0;
            this.Kijunbi.Tag = "指定した日付の空コンテナの数量を確認できます";
            this.Kijunbi.Value = null;
            // 
            // customPanelSyukeiKomoku8
            // 
            this.customPanelSyukeiKomoku8.Controls.Add(this.ContenaShuruiEndSearch);
            this.customPanelSyukeiKomoku8.Controls.Add(this.ContenaShuruiMeiEnd);
            this.customPanelSyukeiKomoku8.Controls.Add(this.ContenaShuruiCDEnd);
            this.customPanelSyukeiKomoku8.Controls.Add(this.ContenaShuruiStartSearch);
            this.customPanelSyukeiKomoku8.Controls.Add(this.ContenaShuruiMeiStart);
            this.customPanelSyukeiKomoku8.Controls.Add(this.ContenaShuruiCDStart);
            this.customPanelSyukeiKomoku8.Controls.Add(this.label16);
            this.customPanelSyukeiKomoku8.Controls.Add(this.label17);
            this.customPanelSyukeiKomoku8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.customPanelSyukeiKomoku8.Location = new System.Drawing.Point(21, 69);
            this.customPanelSyukeiKomoku8.Name = "customPanelSyukeiKomoku8";
            this.customPanelSyukeiKomoku8.Size = new System.Drawing.Size(703, 20);
            this.customPanelSyukeiKomoku8.TabIndex = 3;
            // 
            // ContenaShuruiEndSearch
            // 
            this.ContenaShuruiEndSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.ContenaShuruiEndSearch.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ContenaShuruiEndSearch.DBFieldsName = null;
            this.ContenaShuruiEndSearch.DefaultBackColor = System.Drawing.Color.Empty;
            this.ContenaShuruiEndSearch.DisplayItemName = null;
            this.ContenaShuruiEndSearch.DisplayPopUp = null;
            this.ContenaShuruiEndSearch.ErrorMessage = null;
            this.ContenaShuruiEndSearch.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ContenaShuruiEndSearch.FocusOutCheckMethod")));
            this.ContenaShuruiEndSearch.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ContenaShuruiEndSearch.GetCodeMasterField = null;
            this.ContenaShuruiEndSearch.Image = ((System.Drawing.Image)(resources.GetObject("ContenaShuruiEndSearch.Image")));
            this.ContenaShuruiEndSearch.ItemDefinedTypes = null;
            this.ContenaShuruiEndSearch.LinkedSettingTextBox = null;
            this.ContenaShuruiEndSearch.LinkedTextBoxs = null;
            this.ContenaShuruiEndSearch.Location = new System.Drawing.Point(651, -1);
            this.ContenaShuruiEndSearch.Name = "ContenaShuruiEndSearch";
            this.ContenaShuruiEndSearch.PopupAfterExecute = null;
            this.ContenaShuruiEndSearch.PopupAfterExecuteMethod = "";
            this.ContenaShuruiEndSearch.PopupBeforeExecute = null;
            this.ContenaShuruiEndSearch.PopupGetMasterField = "BUNRUI_CD, BUNRUI_NAME_RYAKU";
            this.ContenaShuruiEndSearch.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ContenaShuruiEndSearch.PopupSearchSendParams")));
            this.ContenaShuruiEndSearch.PopupSetFormField = "ContenaShuruiCDEnd, ContenaShuruiMeiEnd";
            this.ContenaShuruiEndSearch.PopupWindowId = r_framework.Const.WINDOW_ID.M_CONTENA_SHURUI;
            this.ContenaShuruiEndSearch.PopupWindowName = "マスタ共通ポップアップ";
            this.ContenaShuruiEndSearch.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ContenaShuruiEndSearch.popupWindowSetting")));
            this.ContenaShuruiEndSearch.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ContenaShuruiEndSearch.RegistCheckMethod")));
            this.ContenaShuruiEndSearch.SearchDisplayFlag = 0;
            this.ContenaShuruiEndSearch.SetFormField = "ContenaShuruiCDEnd, ContenaShuruiMeiEnd";
            this.ContenaShuruiEndSearch.ShortItemName = null;
            this.ContenaShuruiEndSearch.Size = new System.Drawing.Size(22, 22);
            this.ContenaShuruiEndSearch.TabIndex = 145;
            this.ContenaShuruiEndSearch.TabStop = false;
            this.ContenaShuruiEndSearch.UseVisualStyleBackColor = false;
            this.ContenaShuruiEndSearch.ZeroPaddengFlag = false;
            // 
            // ContenaShuruiMeiEnd
            // 
            this.ContenaShuruiMeiEnd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ContenaShuruiMeiEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContenaShuruiMeiEnd.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ContenaShuruiMeiEnd.DBFieldsName = "CONTENA_SHURUI_NAME_RYAKU";
            this.ContenaShuruiMeiEnd.DefaultBackColor = System.Drawing.Color.Empty;
            this.ContenaShuruiMeiEnd.DisplayPopUp = null;
            this.ContenaShuruiMeiEnd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ContenaShuruiMeiEnd.FocusOutCheckMethod")));
            this.ContenaShuruiMeiEnd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ContenaShuruiMeiEnd.ForeColor = System.Drawing.Color.Black;
            this.ContenaShuruiMeiEnd.IsInputErrorOccured = false;
            this.ContenaShuruiMeiEnd.ItemDefinedTypes = "varchar";
            this.ContenaShuruiMeiEnd.Location = new System.Drawing.Point(462, 0);
            this.ContenaShuruiMeiEnd.MaxLength = 0;
            this.ContenaShuruiMeiEnd.Name = "ContenaShuruiMeiEnd";
            this.ContenaShuruiMeiEnd.PopupAfterExecute = null;
            this.ContenaShuruiMeiEnd.PopupBeforeExecute = null;
            this.ContenaShuruiMeiEnd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ContenaShuruiMeiEnd.PopupSearchSendParams")));
            this.ContenaShuruiMeiEnd.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ContenaShuruiMeiEnd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ContenaShuruiMeiEnd.popupWindowSetting")));
            this.ContenaShuruiMeiEnd.ReadOnly = true;
            this.ContenaShuruiMeiEnd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ContenaShuruiMeiEnd.RegistCheckMethod")));
            this.ContenaShuruiMeiEnd.Size = new System.Drawing.Size(186, 20);
            this.ContenaShuruiMeiEnd.TabIndex = 144;
            this.ContenaShuruiMeiEnd.TabStop = false;
            this.ContenaShuruiMeiEnd.Tag = " ";
            // 
            // ContenaShuruiCDEnd
            // 
            this.ContenaShuruiCDEnd.BackColor = System.Drawing.SystemColors.Window;
            this.ContenaShuruiCDEnd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContenaShuruiCDEnd.CharacterLimitList = null;
            this.ContenaShuruiCDEnd.CharactersNumber = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.ContenaShuruiCDEnd.DBFieldsName = "CONTENA_SHURUI_CD";
            this.ContenaShuruiCDEnd.DefaultBackColor = System.Drawing.Color.Empty;
            this.ContenaShuruiCDEnd.DisplayItemName = "コンテナ種類To";
            this.ContenaShuruiCDEnd.DisplayPopUp = null;
            this.ContenaShuruiCDEnd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ContenaShuruiCDEnd.FocusOutCheckMethod")));
            this.ContenaShuruiCDEnd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ContenaShuruiCDEnd.ForeColor = System.Drawing.Color.Black;
            this.ContenaShuruiCDEnd.GetCodeMasterField = "CONTENA_SHURUI_CD, CONTENA_SHURUI_NAME_RYAKU";
            this.ContenaShuruiCDEnd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ContenaShuruiCDEnd.IsInputErrorOccured = false;
            this.ContenaShuruiCDEnd.ItemDefinedTypes = "smallint";
            this.ContenaShuruiCDEnd.Location = new System.Drawing.Point(414, 0);
            this.ContenaShuruiCDEnd.MaxLength = 3;
            this.ContenaShuruiCDEnd.Name = "ContenaShuruiCDEnd";
            this.ContenaShuruiCDEnd.PopupAfterExecute = null;
            this.ContenaShuruiCDEnd.PopupBeforeExecute = null;
            this.ContenaShuruiCDEnd.PopupGetMasterField = "CONTENA_SHURUI_CD, CONTENA_SHURUI_NAME_RYAKU";
            this.ContenaShuruiCDEnd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ContenaShuruiCDEnd.PopupSearchSendParams")));
            this.ContenaShuruiCDEnd.PopupSetFormField = "ContenaShuruiCDEnd, ContenaShuruiMeiEnd";
            this.ContenaShuruiCDEnd.PopupWindowId = r_framework.Const.WINDOW_ID.M_CONTENA_SHURUI;
            this.ContenaShuruiCDEnd.PopupWindowName = "マスタ共通ポップアップ";
            this.ContenaShuruiCDEnd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ContenaShuruiCDEnd.popupWindowSetting")));
            this.ContenaShuruiCDEnd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ContenaShuruiCDEnd.RegistCheckMethod")));
            this.ContenaShuruiCDEnd.SetFormField = "ContenaShuruiCDEnd, ContenaShuruiMeiEnd";
            this.ContenaShuruiCDEnd.Size = new System.Drawing.Size(49, 20);
            this.ContenaShuruiCDEnd.TabIndex = 143;
            this.ContenaShuruiCDEnd.Tag = "分類を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.ContenaShuruiCDEnd.ZeroPaddengFlag = true;
            // 
            // ContenaShuruiStartSearch
            // 
            this.ContenaShuruiStartSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.ContenaShuruiStartSearch.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ContenaShuruiStartSearch.DBFieldsName = null;
            this.ContenaShuruiStartSearch.DefaultBackColor = System.Drawing.Color.Empty;
            this.ContenaShuruiStartSearch.DisplayItemName = null;
            this.ContenaShuruiStartSearch.DisplayPopUp = null;
            this.ContenaShuruiStartSearch.ErrorMessage = null;
            this.ContenaShuruiStartSearch.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ContenaShuruiStartSearch.FocusOutCheckMethod")));
            this.ContenaShuruiStartSearch.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ContenaShuruiStartSearch.GetCodeMasterField = null;
            this.ContenaShuruiStartSearch.Image = ((System.Drawing.Image)(resources.GetObject("ContenaShuruiStartSearch.Image")));
            this.ContenaShuruiStartSearch.ItemDefinedTypes = null;
            this.ContenaShuruiStartSearch.LinkedSettingTextBox = null;
            this.ContenaShuruiStartSearch.LinkedTextBoxs = null;
            this.ContenaShuruiStartSearch.Location = new System.Drawing.Point(364, -1);
            this.ContenaShuruiStartSearch.Name = "ContenaShuruiStartSearch";
            this.ContenaShuruiStartSearch.PopupAfterExecute = null;
            this.ContenaShuruiStartSearch.PopupAfterExecuteMethod = "";
            this.ContenaShuruiStartSearch.PopupBeforeExecute = null;
            this.ContenaShuruiStartSearch.PopupGetMasterField = "CONTENA_SHURUI_CD, CONTENA_SHURUI_NAME_RYAKU";
            this.ContenaShuruiStartSearch.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ContenaShuruiStartSearch.PopupSearchSendParams")));
            this.ContenaShuruiStartSearch.PopupSetFormField = "ContenaShuruiCDStart, ContenaShuruiMeiStart";
            this.ContenaShuruiStartSearch.PopupWindowId = r_framework.Const.WINDOW_ID.M_CONTENA_SHURUI;
            this.ContenaShuruiStartSearch.PopupWindowName = "マスタ共通ポップアップ";
            this.ContenaShuruiStartSearch.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ContenaShuruiStartSearch.popupWindowSetting")));
            this.ContenaShuruiStartSearch.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ContenaShuruiStartSearch.RegistCheckMethod")));
            this.ContenaShuruiStartSearch.SearchDisplayFlag = 0;
            this.ContenaShuruiStartSearch.SetFormField = "ContenaShuruiCDStart, ContenaShuruiMeiStart";
            this.ContenaShuruiStartSearch.ShortItemName = null;
            this.ContenaShuruiStartSearch.Size = new System.Drawing.Size(22, 22);
            this.ContenaShuruiStartSearch.TabIndex = 142;
            this.ContenaShuruiStartSearch.TabStop = false;
            this.ContenaShuruiStartSearch.UseVisualStyleBackColor = false;
            this.ContenaShuruiStartSearch.ZeroPaddengFlag = false;
            // 
            // ContenaShuruiMeiStart
            // 
            this.ContenaShuruiMeiStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ContenaShuruiMeiStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContenaShuruiMeiStart.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ContenaShuruiMeiStart.DBFieldsName = "CONTENA_SHURUI_NAME_RYAKU";
            this.ContenaShuruiMeiStart.DefaultBackColor = System.Drawing.Color.Empty;
            this.ContenaShuruiMeiStart.DisplayPopUp = null;
            this.ContenaShuruiMeiStart.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ContenaShuruiMeiStart.FocusOutCheckMethod")));
            this.ContenaShuruiMeiStart.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ContenaShuruiMeiStart.ForeColor = System.Drawing.Color.Black;
            this.ContenaShuruiMeiStart.IsInputErrorOccured = false;
            this.ContenaShuruiMeiStart.ItemDefinedTypes = "varchar";
            this.ContenaShuruiMeiStart.Location = new System.Drawing.Point(175, 0);
            this.ContenaShuruiMeiStart.MaxLength = 0;
            this.ContenaShuruiMeiStart.Name = "ContenaShuruiMeiStart";
            this.ContenaShuruiMeiStart.PopupAfterExecute = null;
            this.ContenaShuruiMeiStart.PopupBeforeExecute = null;
            this.ContenaShuruiMeiStart.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ContenaShuruiMeiStart.PopupSearchSendParams")));
            this.ContenaShuruiMeiStart.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ContenaShuruiMeiStart.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ContenaShuruiMeiStart.popupWindowSetting")));
            this.ContenaShuruiMeiStart.ReadOnly = true;
            this.ContenaShuruiMeiStart.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ContenaShuruiMeiStart.RegistCheckMethod")));
            this.ContenaShuruiMeiStart.Size = new System.Drawing.Size(186, 20);
            this.ContenaShuruiMeiStart.TabIndex = 141;
            this.ContenaShuruiMeiStart.TabStop = false;
            this.ContenaShuruiMeiStart.Tag = " ";
            // 
            // ContenaShuruiCDStart
            // 
            this.ContenaShuruiCDStart.BackColor = System.Drawing.SystemColors.Window;
            this.ContenaShuruiCDStart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContenaShuruiCDStart.CharacterLimitList = null;
            this.ContenaShuruiCDStart.CharactersNumber = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.ContenaShuruiCDStart.DBFieldsName = "CONTENA_SHURUI_CD";
            this.ContenaShuruiCDStart.DefaultBackColor = System.Drawing.Color.Empty;
            this.ContenaShuruiCDStart.DisplayItemName = "コンテナ種類From";
            this.ContenaShuruiCDStart.DisplayPopUp = null;
            this.ContenaShuruiCDStart.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ContenaShuruiCDStart.FocusOutCheckMethod")));
            this.ContenaShuruiCDStart.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ContenaShuruiCDStart.ForeColor = System.Drawing.Color.Black;
            this.ContenaShuruiCDStart.GetCodeMasterField = "CONTENA_SHURUI_CD, CONTENA_SHURUI_NAME_RYAKU";
            this.ContenaShuruiCDStart.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ContenaShuruiCDStart.IsInputErrorOccured = false;
            this.ContenaShuruiCDStart.ItemDefinedTypes = "smallint";
            this.ContenaShuruiCDStart.Location = new System.Drawing.Point(127, 0);
            this.ContenaShuruiCDStart.MaxLength = 3;
            this.ContenaShuruiCDStart.Name = "ContenaShuruiCDStart";
            this.ContenaShuruiCDStart.PopupAfterExecute = null;
            this.ContenaShuruiCDStart.PopupBeforeExecute = null;
            this.ContenaShuruiCDStart.PopupGetMasterField = "CONTENA_SHURUI_CD, CONTENA_SHURUI_NAME_RYAKU";
            this.ContenaShuruiCDStart.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ContenaShuruiCDStart.PopupSearchSendParams")));
            this.ContenaShuruiCDStart.PopupSetFormField = "ContenaShuruiCDStart, ContenaShuruiMeiStart";
            this.ContenaShuruiCDStart.PopupWindowId = r_framework.Const.WINDOW_ID.M_CONTENA_SHURUI;
            this.ContenaShuruiCDStart.PopupWindowName = "マスタ共通ポップアップ";
            this.ContenaShuruiCDStart.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ContenaShuruiCDStart.popupWindowSetting")));
            this.ContenaShuruiCDStart.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ContenaShuruiCDStart.RegistCheckMethod")));
            this.ContenaShuruiCDStart.SetFormField = "ContenaShuruiCDStart, ContenaShuruiMeiStart";
            this.ContenaShuruiCDStart.Size = new System.Drawing.Size(49, 20);
            this.ContenaShuruiCDStart.TabIndex = 140;
            this.ContenaShuruiCDStart.Tag = "コンテナ種類を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.ContenaShuruiCDStart.ZeroPaddengFlag = true;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label16.Location = new System.Drawing.Point(389, -1);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(15, 20);
            this.label16.TabIndex = 10006;
            this.label16.Text = "～";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label17.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(0, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(123, 20);
            this.label17.TabIndex = 9999;
            this.label17.Text = "コンテナ種類";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(21, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 20);
            this.label2.TabIndex = 9999;
            this.label2.Text = "受注・配車含む";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(21, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 20);
            this.label1.TabIndex = 9999;
            this.label1.Text = "基準日";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ISNOT_NEED_DELETE_FLG
            // 
            this.ISNOT_NEED_DELETE_FLG.BackColor = System.Drawing.SystemColors.Window;
            this.ISNOT_NEED_DELETE_FLG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ISNOT_NEED_DELETE_FLG.CharactersNumber = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.ISNOT_NEED_DELETE_FLG.DBFieldsName = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.ISNOT_NEED_DELETE_FLG.DisplayItemName = "";
            this.ISNOT_NEED_DELETE_FLG.DisplayPopUp = null;
            this.ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ISNOT_NEED_DELETE_FLG.ForeColor = System.Drawing.Color.Black;
            this.ISNOT_NEED_DELETE_FLG.GetCodeMasterField = "";
            this.ISNOT_NEED_DELETE_FLG.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ISNOT_NEED_DELETE_FLG.IsInputErrorOccured = false;
            this.ISNOT_NEED_DELETE_FLG.ItemDefinedTypes = "bit";
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(483, 43);
            this.ISNOT_NEED_DELETE_FLG.MaxLength = 2;
            this.ISNOT_NEED_DELETE_FLG.Name = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.PopupAfterExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupBeforeExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupGetMasterField = "";
            this.ISNOT_NEED_DELETE_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.PopupSearchSendParams")));
            this.ISNOT_NEED_DELETE_FLG.PopupSetFormField = "";
            this.ISNOT_NEED_DELETE_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ISNOT_NEED_DELETE_FLG.PopupWindowName = "";
            this.ISNOT_NEED_DELETE_FLG.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.popupWindowSetting")));
            this.ISNOT_NEED_DELETE_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.RegistCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.SetFormField = "";
            this.ISNOT_NEED_DELETE_FLG.ShortItemName = "";
            this.ISNOT_NEED_DELETE_FLG.Size = new System.Drawing.Size(59, 20);
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 10005;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "True";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            this.ISNOT_NEED_DELETE_FLG.ZeroPaddengFlag = true;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(748, 129);
            this.Controls.Add(this.customPanel1);
            this.Name = "UIForm";
            this.Text = "UIForm";
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.customPanel2.ResumeLayout(false);
            this.customPanel2.PerformLayout();
            this.customPanelSyukeiKomoku8.ResumeLayout(false);
            this.customPanelSyukeiKomoku8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label8;
        private r_framework.CustomControl.CustomPanel customPanel1;
        internal r_framework.CustomControl.CustomPanel customPanelSyukeiKomoku8;
        internal r_framework.CustomControl.CustomPopupOpenButton ContenaShuruiEndSearch;
        internal r_framework.CustomControl.CustomTextBox ContenaShuruiMeiEnd;
        internal r_framework.CustomControl.CustomAlphaNumTextBox ContenaShuruiCDEnd;
        internal r_framework.CustomControl.CustomPopupOpenButton ContenaShuruiStartSearch;
        internal r_framework.CustomControl.CustomTextBox ContenaShuruiMeiStart;
        internal r_framework.CustomControl.CustomAlphaNumTextBox ContenaShuruiCDStart;
        private System.Windows.Forms.Label label16;
        private r_framework.CustomControl.CustomRadioButton JuchuHaishaHukumu1;
        private r_framework.CustomControl.CustomRadioButton JuchuHaishaHukumu2;
        internal r_framework.CustomControl.CustomDateTimePicker Kijunbi;
        internal r_framework.CustomControl.CustomNumericTextBox2 JuchuHaishaHukumu;
        internal System.Windows.Forms.Label label2;
        internal r_framework.CustomControl.CustomPanel customPanel2;
        private System.Windows.Forms.Label label17;
        internal System.Windows.Forms.Label label1;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
    }
}