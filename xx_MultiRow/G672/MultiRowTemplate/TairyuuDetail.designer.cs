namespace Shougun.Core.Scale.KeiryouNyuuryoku.MultiRowTemplate
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class TairyuuDetail
    {
        /// <summary> 
        /// 必要なデザイナ変数です。
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

        #region MultiRow Template Designer generated code

		/// <summary> 
		/// デザイナ サポートに必要なメソッドです。このメソッドの内容を
		/// コード エディタで変更しないでください。
		/// </summary>
        private void InitializeComponent()
        {
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TairyuuDetail));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.DENPYOU_NUMBER = new r_framework.CustomControl.GcCustomNumericTextBox2Cell();
            this.DENSHU_KBN_NAME = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.DENPYOU_DATE = new r_framework.CustomControl.GcCustomDataTime();
            this.BIKOU = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.SHASHU_NAME = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.SHARYOU_NAME = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.SYSTEM_ID = new r_framework.CustomControl.GcCustomTextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.DENSHU_KBN_NAME);
            this.Row.Cells.Add(this.DENPYOU_NUMBER);
            this.Row.Cells.Add(this.DENPYOU_DATE);
            this.Row.Cells.Add(this.BIKOU);
            this.Row.Cells.Add(this.SHASHU_NAME);
            this.Row.Cells.Add(this.SHARYOU_NAME);
            this.Row.Cells.Add(this.SYSTEM_ID);
            this.Row.Height = 105;
            this.Row.Width = 222;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Height = 1;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 222;
            // 
            // DENPYOU_NUMBER
            // 
            this.DENPYOU_NUMBER.DataField = "DENPYOU_NUMBER";
            this.DENPYOU_NUMBER.DBFieldsName = "DENPYOU_NUMBER";
            this.DENPYOU_NUMBER.DefaultBackColor = System.Drawing.Color.Empty;
            this.DENPYOU_NUMBER.DisplayItemName = "伝票番号";
            this.DENPYOU_NUMBER.DisplayPopUp = null;
            this.DENPYOU_NUMBER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_NUMBER.FocusOutCheckMethod")));
            this.DENPYOU_NUMBER.IsInputErrorOccured = false;
            this.DENPYOU_NUMBER.ItemDefinedTypes = "float";
            this.DENPYOU_NUMBER.Location = new System.Drawing.Point(143, 0);
            this.DENPYOU_NUMBER.Name = "DENPYOU_NUMBER";
            this.DENPYOU_NUMBER.PopupAfterExecute = null;
            this.DENPYOU_NUMBER.PopupBeforeExecute = null;
            this.DENPYOU_NUMBER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DENPYOU_NUMBER.PopupSearchSendParams")));
            this.DENPYOU_NUMBER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DENPYOU_NUMBER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DENPYOU_NUMBER.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DENPYOU_NUMBER.RangeSetting = rangeSettingDto1;
            this.DENPYOU_NUMBER.ReadOnly = true;
            this.DENPYOU_NUMBER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_NUMBER.RegistCheckMethod")));
            this.DENPYOU_NUMBER.Size = new System.Drawing.Size(79, 21);
            cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle2.Format = "N0";
            cellStyle2.ImageAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.DENPYOU_NUMBER.Style = cellStyle2;
            this.DENPYOU_NUMBER.TabIndex = 150;
            this.DENPYOU_NUMBER.TabStop = false;
            this.DENPYOU_NUMBER.Tag = " ";
            this.DENPYOU_NUMBER.Value = "9999999999";
            // 
            // DENSHU_KBN_NAME
            // 
            this.DENSHU_KBN_NAME.CharactersNumber = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.DENSHU_KBN_NAME.DataField = "DENSHU_KBN_NAME";
            this.DENSHU_KBN_NAME.DBFieldsName = "DENSHU_KBN_NAME";
            this.DENSHU_KBN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.DENSHU_KBN_NAME.DisplayItemName = "伝種名";
            this.DENSHU_KBN_NAME.DisplayPopUp = null;
            this.DENSHU_KBN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENSHU_KBN_NAME.FocusOutCheckMethod")));
            this.DENSHU_KBN_NAME.IsInputErrorOccured = false;
            this.DENSHU_KBN_NAME.ItemDefinedTypes = "varchar";
            this.DENSHU_KBN_NAME.Location = new System.Drawing.Point(0, 0);
            this.DENSHU_KBN_NAME.MaxLength = 4;
            this.DENSHU_KBN_NAME.Name = "DENSHU_KBN_NAME";
            this.DENSHU_KBN_NAME.PopupAfterExecute = null;
            this.DENSHU_KBN_NAME.PopupBeforeExecute = null;
            this.DENSHU_KBN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DENSHU_KBN_NAME.PopupSearchSendParams")));
            this.DENSHU_KBN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DENSHU_KBN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DENSHU_KBN_NAME.popupWindowSetting")));
            this.DENSHU_KBN_NAME.ReadOnly = true;
            this.DENSHU_KBN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENSHU_KBN_NAME.RegistCheckMethod")));
            this.DENSHU_KBN_NAME.Size = new System.Drawing.Size(36, 21);
            cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            cellStyle1.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.DENSHU_KBN_NAME.Style = cellStyle1;
            this.DENSHU_KBN_NAME.TabIndex = 40;
            this.DENSHU_KBN_NAME.TabStop = false;
            this.DENSHU_KBN_NAME.Tag = " ";
            this.DENSHU_KBN_NAME.Value = "受入";
            // 
            // DENPYOU_DATE
            // 
            this.DENPYOU_DATE.CustomFormatSetting = "yyyy/MM/dd(ddd)";
            this.DENPYOU_DATE.DataField = "DENPYOU_DATE";
            this.DENPYOU_DATE.DBFieldsName = "DENPYOU_DATE";
            this.DENPYOU_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            this.DENPYOU_DATE.DisplayItemName = "伝票日付";
            this.DENPYOU_DATE.DisplayPopUp = null;
            this.DENPYOU_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_DATE.FocusOutCheckMethod")));
            this.DENPYOU_DATE.IsInputErrorOccured = false;
            this.DENPYOU_DATE.ItemDefinedTypes = "";
            this.DENPYOU_DATE.Location = new System.Drawing.Point(36, 0);
            this.DENPYOU_DATE.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.DENPYOU_DATE.Name = "DENPYOU_DATE";
            this.DENPYOU_DATE.PopupAfterExecute = null;
            this.DENPYOU_DATE.PopupBeforeExecute = null;
            this.DENPYOU_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DENPYOU_DATE.PopupSearchSendParams")));
            this.DENPYOU_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DENPYOU_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DENPYOU_DATE.popupWindowSetting")));
            this.DENPYOU_DATE.PrevText = null;
            this.DENPYOU_DATE.ReadOnly = true;
            this.DENPYOU_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_DATE.RegistCheckMethod")));
            this.DENPYOU_DATE.Size = new System.Drawing.Size(107, 21);
            cellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle3.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.DENPYOU_DATE.Style = cellStyle3;
            this.DENPYOU_DATE.TabIndex = 50;
            this.DENPYOU_DATE.TabStop = false;
            this.DENPYOU_DATE.Tag = " ";
            // 
            // BIKOU
            // 
            this.BIKOU.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.BIKOU.DataField = "BIKOU";
            this.BIKOU.DBFieldsName = "BIKOU";
            this.BIKOU.DefaultBackColor = System.Drawing.Color.Empty;
            this.BIKOU.DisplayItemName = "運搬業者";
            this.BIKOU.DisplayPopUp = null;
            this.BIKOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BIKOU.FocusOutCheckMethod")));
            this.BIKOU.IsInputErrorOccured = false;
            this.BIKOU.ItemDefinedTypes = "varchar";
            this.BIKOU.Location = new System.Drawing.Point(0, 42);
            this.BIKOU.MaxLength = 40;
            this.BIKOU.Name = "BIKOU";
            this.BIKOU.PopupAfterExecute = null;
            this.BIKOU.PopupBeforeExecute = null;
            this.BIKOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BIKOU.PopupSearchSendParams")));
            this.BIKOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BIKOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BIKOU.popupWindowSetting")));
            this.BIKOU.ReadOnly = true;
            this.BIKOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BIKOU.RegistCheckMethod")));
            this.BIKOU.Size = new System.Drawing.Size(222, 63);
            cellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            cellStyle4.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle4.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.TopLeft;
            this.BIKOU.Style = cellStyle4;
            this.BIKOU.TabIndex = 353;
            this.BIKOU.TabStop = false;
            this.BIKOU.Tag = " ";
            this.BIKOU.Value = "運搬業者全角で１５文字１２３４\r\naaaa";
            // 
            // SHASHU_NAME
            // 
            this.SHASHU_NAME.CharactersNumber = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.SHASHU_NAME.DataField = "SHASHU_NAME";
            this.SHASHU_NAME.DBFieldsName = "SHASHU_NAME";
            this.SHASHU_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHASHU_NAME.DisplayItemName = "車種";
            this.SHASHU_NAME.DisplayPopUp = null;
            this.SHASHU_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHASHU_NAME.FocusOutCheckMethod")));
            this.SHASHU_NAME.IsInputErrorOccured = false;
            this.SHASHU_NAME.ItemDefinedTypes = "varchar";
            this.SHASHU_NAME.Location = new System.Drawing.Point(0, 21);
            this.SHASHU_NAME.MaxLength = 30;
            this.SHASHU_NAME.Name = "SHASHU_NAME";
            this.SHASHU_NAME.PopupAfterExecute = null;
            this.SHASHU_NAME.PopupBeforeExecute = null;
            this.SHASHU_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHASHU_NAME.PopupSearchSendParams")));
            this.SHASHU_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHASHU_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHASHU_NAME.popupWindowSetting")));
            this.SHASHU_NAME.ReadOnly = true;
            this.SHASHU_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHASHU_NAME.RegistCheckMethod")));
            this.SHASHU_NAME.Size = new System.Drawing.Size(82, 21);
            cellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle5.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            cellStyle5.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle5.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.SHASHU_NAME.Style = cellStyle5;
            this.SHASHU_NAME.TabIndex = 354;
            this.SHASHU_NAME.TabStop = false;
            this.SHASHU_NAME.Tag = " ";
            this.SHASHU_NAME.Value = "車種";
            // 
            // SHARYOU_NAME
            // 
            this.SHARYOU_NAME.CharactersNumber = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.SHARYOU_NAME.DataField = "SHARYOU_NAME";
            this.SHARYOU_NAME.DBFieldsName = "SHARYOU_NAME";
            this.SHARYOU_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHARYOU_NAME.DisplayItemName = "車輌名";
            this.SHARYOU_NAME.DisplayPopUp = null;
            this.SHARYOU_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHARYOU_NAME.FocusOutCheckMethod")));
            this.SHARYOU_NAME.IsInputErrorOccured = false;
            this.SHARYOU_NAME.ItemDefinedTypes = "varchar";
            this.SHARYOU_NAME.Location = new System.Drawing.Point(82, 21);
            this.SHARYOU_NAME.MaxLength = 30;
            this.SHARYOU_NAME.Name = "SHARYOU_NAME";
            this.SHARYOU_NAME.PopupAfterExecute = null;
            this.SHARYOU_NAME.PopupBeforeExecute = null;
            this.SHARYOU_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHARYOU_NAME.PopupSearchSendParams")));
            this.SHARYOU_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHARYOU_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHARYOU_NAME.popupWindowSetting")));
            this.SHARYOU_NAME.ReadOnly = true;
            this.SHARYOU_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHARYOU_NAME.RegistCheckMethod")));
            this.SHARYOU_NAME.Size = new System.Drawing.Size(140, 21);
            cellStyle6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle6.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            cellStyle6.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle6.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.SHARYOU_NAME.Style = cellStyle6;
            this.SHARYOU_NAME.TabIndex = 355;
            this.SHARYOU_NAME.TabStop = false;
            this.SHARYOU_NAME.Tag = " ";
            this.SHARYOU_NAME.Value = "車輌名";
            // 
            // SYSTEM_ID
            // 
            this.SYSTEM_ID.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.SYSTEM_ID.DataField = "SYSTEM_ID";
            this.SYSTEM_ID.DBFieldsName = "SYSTEM_ID";
            this.SYSTEM_ID.DefaultBackColor = System.Drawing.Color.Empty;
            this.SYSTEM_ID.DisplayItemName = "システムID";
            this.SYSTEM_ID.DisplayPopUp = null;
            this.SYSTEM_ID.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SYSTEM_ID.FocusOutCheckMethod")));
            this.SYSTEM_ID.IsInputErrorOccured = false;
            this.SYSTEM_ID.ItemDefinedTypes = "varchar";
            this.SYSTEM_ID.Location = new System.Drawing.Point(0, 126);
            this.SYSTEM_ID.MaxLength = 40;
            this.SYSTEM_ID.Name = "SYSTEM_ID";
            this.SYSTEM_ID.PopupAfterExecute = null;
            this.SYSTEM_ID.PopupBeforeExecute = null;
            this.SYSTEM_ID.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SYSTEM_ID.PopupSearchSendParams")));
            this.SYSTEM_ID.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SYSTEM_ID.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SYSTEM_ID.popupWindowSetting")));
            this.SYSTEM_ID.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SYSTEM_ID.RegistCheckMethod")));
            this.SYSTEM_ID.Size = new System.Drawing.Size(64, 21);
            cellStyle9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle9.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            cellStyle9.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle9.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.SYSTEM_ID.Style = cellStyle9;
            this.SYSTEM_ID.TabIndex = 358;
            this.SYSTEM_ID.Tag = "";
            this.SYSTEM_ID.Visible = false;
            // 
            // TairyuuDetail
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 106;
            this.Width = 222;

        }
        

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        internal r_framework.CustomControl.GcCustomTextBoxCell DENSHU_KBN_NAME;
        internal r_framework.CustomControl.GcCustomNumericTextBox2Cell DENPYOU_NUMBER;
        internal r_framework.CustomControl.GcCustomDataTime DENPYOU_DATE;
        internal r_framework.CustomControl.GcCustomTextBoxCell BIKOU;
        internal r_framework.CustomControl.GcCustomTextBoxCell SHASHU_NAME;
        internal r_framework.CustomControl.GcCustomTextBoxCell SHARYOU_NAME;
        internal r_framework.CustomControl.GcCustomTextBoxCell SYSTEM_ID;
        
    }
}
