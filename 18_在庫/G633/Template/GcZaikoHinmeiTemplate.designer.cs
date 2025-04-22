namespace Shougun.Core.Stock.ZaikoHinmeiHuriwake
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class GcZaikoHinmeiTemplate
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle10 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle11 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border4 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle12 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border5 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GcZaikoHinmeiTemplate));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.columnHeaderCellHinmeiCd = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCellHinmeiName = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCellHiritsu = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCellRyou = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.cornerHeaderCell1 = new GrapeCity.Win.MultiRow.CornerHeaderCell();
            this.rowHeaderCell1 = new GrapeCity.Win.MultiRow.RowHeaderCell();
            this.ZAIKO_HINMEI_CD = new r_framework.CustomControl.GcCustomAlphaNumTextBoxCell();
            this.ZAIKO_HINMEI_NAME = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.PERCENT_SIGN = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.ZAIKO_RYOU = new r_framework.CustomControl.GcCustomNumericTextBox2Cell();
            this.ZAIKO_TANKA = new r_framework.CustomControl.GcCustomNumericTextBox2Cell();
            this.ZAIKO_HIRITSU = new r_framework.CustomControl.GcCustomNumericTextBox2Cell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.ZAIKO_HINMEI_CD);
            this.Row.Cells.Add(this.ZAIKO_HINMEI_NAME);
            this.Row.Cells.Add(this.ZAIKO_HIRITSU);
            this.Row.Cells.Add(this.PERCENT_SIGN);
            this.Row.Cells.Add(this.ZAIKO_RYOU);
            this.Row.Cells.Add(this.rowHeaderCell1);
            this.Row.Cells.Add(this.ZAIKO_TANKA);
            this.Row.Height = 21;
            this.Row.Width = 572;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCellHinmeiCd);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCellHinmeiName);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCellHiritsu);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCellRyou);
            this.columnHeaderSection1.Cells.Add(this.cornerHeaderCell1);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 572;
            // 
            // columnHeaderCellHinmeiCd
            // 
            this.columnHeaderCellHinmeiCd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCellHinmeiCd.Location = new System.Drawing.Point(20, 0);
            this.columnHeaderCellHinmeiCd.Name = "columnHeaderCellHinmeiCd";
            this.columnHeaderCellHinmeiCd.Size = new System.Drawing.Size(96, 21);
            cellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.White);
            cellStyle8.Border = border1;
            cellStyle8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle8.ForeColor = System.Drawing.Color.White;
            cellStyle8.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.columnHeaderCellHinmeiCd.Style = cellStyle8;
            this.columnHeaderCellHinmeiCd.TabIndex = 1;
            this.columnHeaderCellHinmeiCd.Value = "在庫品名CD※";
            // 
            // columnHeaderCellHinmeiName
            // 
            this.columnHeaderCellHinmeiName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCellHinmeiName.Location = new System.Drawing.Point(116, 0);
            this.columnHeaderCellHinmeiName.Name = "columnHeaderCellHinmeiName";
            this.columnHeaderCellHinmeiName.Size = new System.Drawing.Size(220, 21);
            cellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.White);
            cellStyle9.Border = border2;
            cellStyle9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle9.ForeColor = System.Drawing.Color.White;
            cellStyle9.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.columnHeaderCellHinmeiName.Style = cellStyle9;
            this.columnHeaderCellHinmeiName.TabIndex = 2;
            this.columnHeaderCellHinmeiName.Value = "在庫品名";
            // 
            // columnHeaderCellHiritsu
            // 
            this.columnHeaderCellHiritsu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCellHiritsu.Location = new System.Drawing.Point(336, 0);
            this.columnHeaderCellHiritsu.Name = "columnHeaderCellHiritsu";
            this.columnHeaderCellHiritsu.Size = new System.Drawing.Size(96, 21);
            cellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.White);
            cellStyle10.Border = border3;
            cellStyle10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle10.ForeColor = System.Drawing.Color.White;
            cellStyle10.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.columnHeaderCellHiritsu.Style = cellStyle10;
            this.columnHeaderCellHiritsu.TabIndex = 3;
            this.columnHeaderCellHiritsu.Value = "在庫比率※";
            // 
            // columnHeaderCellRyou
            // 
            this.columnHeaderCellRyou.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCellRyou.Location = new System.Drawing.Point(432, 0);
            this.columnHeaderCellRyou.Name = "columnHeaderCellRyou";
            this.columnHeaderCellRyou.Size = new System.Drawing.Size(140, 21);
            cellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.White);
            cellStyle11.Border = border4;
            cellStyle11.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle11.ForeColor = System.Drawing.Color.White;
            cellStyle11.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.columnHeaderCellRyou.Style = cellStyle11;
            this.columnHeaderCellRyou.TabIndex = 4;
            this.columnHeaderCellRyou.Value = "在庫量";
            // 
            // cornerHeaderCell1
            // 
            this.cornerHeaderCell1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cornerHeaderCell1.Location = new System.Drawing.Point(0, 0);
            this.cornerHeaderCell1.Name = "cornerHeaderCell1";
            this.cornerHeaderCell1.Size = new System.Drawing.Size(20, 21);
            cellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border5.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.White);
            cellStyle12.Border = border5;
            cellStyle12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle12.ForeColor = System.Drawing.Color.White;
            cellStyle12.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.cornerHeaderCell1.Style = cellStyle12;
            this.cornerHeaderCell1.TabIndex = 0;
            // 
            // rowHeaderCell1
            // 
            this.rowHeaderCell1.Location = new System.Drawing.Point(0, 0);
            this.rowHeaderCell1.Name = "rowHeaderCell1";
            this.rowHeaderCell1.Size = new System.Drawing.Size(20, 21);
            cellStyle6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.rowHeaderCell1.Style = cellStyle6;
            this.rowHeaderCell1.TabIndex = 0;
            // 
            // ZAIKO_HINMEI_CD
            // 
            this.ZAIKO_HINMEI_CD.ChangeUpperCase = true;
            this.ZAIKO_HINMEI_CD.CharacterLimitList = null;
            this.ZAIKO_HINMEI_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.ZAIKO_HINMEI_CD.DataField = "ZAIKO_HINMEI_CD";
            this.ZAIKO_HINMEI_CD.DBFieldsName = "ZAIKO_HINMEI_CD";
            this.ZAIKO_HINMEI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.ZAIKO_HINMEI_CD.DisplayItemName = "在庫品名CD";
            this.ZAIKO_HINMEI_CD.DisplayPopUp = null;
            this.ZAIKO_HINMEI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_CD.FocusOutCheckMethod")));
            this.ZAIKO_HINMEI_CD.GetCodeMasterField = "ZAIKO_HINMEI_CD, ZAIKO_HINMEI_NAME_RYAKU, ZAIKO_TANKA";
            this.ZAIKO_HINMEI_CD.IsInputErrorOccured = false;
            this.ZAIKO_HINMEI_CD.ItemDefinedTypes = "";
            this.ZAIKO_HINMEI_CD.Location = new System.Drawing.Point(20, 0);
            this.ZAIKO_HINMEI_CD.MaxLength = 6;
            this.ZAIKO_HINMEI_CD.Name = "ZAIKO_HINMEI_CD";
            this.ZAIKO_HINMEI_CD.PopupAfterExecute = null;
            this.ZAIKO_HINMEI_CD.PopupAfterExecuteMethod = "ZaikoHinmeiCdPopupAfter";
            this.ZAIKO_HINMEI_CD.PopupBeforeExecute = null;
            this.ZAIKO_HINMEI_CD.PopupGetMasterField = "ZAIKO_HINMEI_CD, ZAIKO_HINMEI_NAME_RYAKU, ZAIKO_TANKA";
            this.ZAIKO_HINMEI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZAIKO_HINMEI_CD.PopupSearchSendParams")));
            this.ZAIKO_HINMEI_CD.PopupSetFormField = "ZAIKO_HINMEI_CD, ZAIKO_HINMEI_NAME, ZAIKO_TANKA";
            this.ZAIKO_HINMEI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_ZAIKO_HINMEI;
            this.ZAIKO_HINMEI_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.ZAIKO_HINMEI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZAIKO_HINMEI_CD.popupWindowSetting")));
            this.ZAIKO_HINMEI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_CD.RegistCheckMethod")));
            this.ZAIKO_HINMEI_CD.SetFormField = "ZAIKO_HINMEI_CD, ZAIKO_HINMEI_NAME, ZAIKO_TANKA";
            this.ZAIKO_HINMEI_CD.Size = new System.Drawing.Size(96, 21);
            cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle1.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.ZAIKO_HINMEI_CD.Style = cellStyle1;
            this.ZAIKO_HINMEI_CD.TabIndex = 1;
            this.ZAIKO_HINMEI_CD.Tag = "半角6桁以内で入力してください";
            this.ZAIKO_HINMEI_CD.ZeroPaddengFlag = true;
            // 
            // ZAIKO_HINMEI_NAME
            // 
            this.ZAIKO_HINMEI_NAME.DataField = "ZAIKO_HINMEI_NAME";
            this.ZAIKO_HINMEI_NAME.DBFieldsName = "ZAIKO_HINMEI_NAME";
            this.ZAIKO_HINMEI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.ZAIKO_HINMEI_NAME.DisplayItemName = "在庫品名";
            this.ZAIKO_HINMEI_NAME.DisplayPopUp = null;
            this.ZAIKO_HINMEI_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_NAME.FocusOutCheckMethod")));
            this.ZAIKO_HINMEI_NAME.IsInputErrorOccured = false;
            this.ZAIKO_HINMEI_NAME.Location = new System.Drawing.Point(116, 0);
            this.ZAIKO_HINMEI_NAME.Name = "ZAIKO_HINMEI_NAME";
            this.ZAIKO_HINMEI_NAME.PopupAfterExecute = null;
            this.ZAIKO_HINMEI_NAME.PopupBeforeExecute = null;
            this.ZAIKO_HINMEI_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZAIKO_HINMEI_NAME.PopupSearchSendParams")));
            this.ZAIKO_HINMEI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ZAIKO_HINMEI_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZAIKO_HINMEI_NAME.popupWindowSetting")));
            this.ZAIKO_HINMEI_NAME.ReadOnly = true;
            this.ZAIKO_HINMEI_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_NAME.RegistCheckMethod")));
            this.ZAIKO_HINMEI_NAME.Size = new System.Drawing.Size(220, 21);
            cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle2.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.ZAIKO_HINMEI_NAME.Style = cellStyle2;
            this.ZAIKO_HINMEI_NAME.TabIndex = 2;
            this.ZAIKO_HINMEI_NAME.TabStop = false;
            // 
            // PERCENT_SIGN
            // 
            this.PERCENT_SIGN.DefaultBackColor = System.Drawing.Color.Empty;
            this.PERCENT_SIGN.DisplayPopUp = null;
            this.PERCENT_SIGN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PERCENT_SIGN.FocusOutCheckMethod")));
            this.PERCENT_SIGN.IsInputErrorOccured = false;
            this.PERCENT_SIGN.Location = new System.Drawing.Point(412, 0);
            this.PERCENT_SIGN.Name = "PERCENT_SIGN";
            this.PERCENT_SIGN.PopupAfterExecute = null;
            this.PERCENT_SIGN.PopupBeforeExecute = null;
            this.PERCENT_SIGN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PERCENT_SIGN.PopupSearchSendParams")));
            this.PERCENT_SIGN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PERCENT_SIGN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("PERCENT_SIGN.popupWindowSetting")));
            this.PERCENT_SIGN.ReadOnly = true;
            this.PERCENT_SIGN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PERCENT_SIGN.RegistCheckMethod")));
            this.PERCENT_SIGN.Size = new System.Drawing.Size(20, 21);
            cellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.PERCENT_SIGN.Style = cellStyle4;
            this.PERCENT_SIGN.TabIndex = 4;
            this.PERCENT_SIGN.TabStop = false;
            this.PERCENT_SIGN.Value = "%";
            // 
            // ZAIKO_RYOU
            // 
            this.ZAIKO_RYOU.DataField = "ZAIKO_RYOU";
            this.ZAIKO_RYOU.DBFieldsName = "ZAIKO_RYOU";
            this.ZAIKO_RYOU.DefaultBackColor = System.Drawing.Color.Empty;
            this.ZAIKO_RYOU.DisplayItemName = "在庫量";
            this.ZAIKO_RYOU.DisplayPopUp = null;
            this.ZAIKO_RYOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_RYOU.FocusOutCheckMethod")));
            this.ZAIKO_RYOU.FormatSetting = "システム設定(重量書式)";
            this.ZAIKO_RYOU.IsInputErrorOccured = false;
            this.ZAIKO_RYOU.Location = new System.Drawing.Point(432, 0);
            this.ZAIKO_RYOU.Name = "ZAIKO_RYOU";
            this.ZAIKO_RYOU.PopupAfterExecute = null;
            this.ZAIKO_RYOU.PopupBeforeExecute = null;
            this.ZAIKO_RYOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZAIKO_RYOU.PopupSearchSendParams")));
            this.ZAIKO_RYOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ZAIKO_RYOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZAIKO_RYOU.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.ZAIKO_RYOU.RangeSetting = rangeSettingDto2;
            this.ZAIKO_RYOU.ReadOnly = true;
            this.ZAIKO_RYOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_RYOU.RegistCheckMethod")));
            this.ZAIKO_RYOU.Size = new System.Drawing.Size(140, 21);
            cellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle5.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.ZAIKO_RYOU.Style = cellStyle5;
            this.ZAIKO_RYOU.TabIndex = 5;
            this.ZAIKO_RYOU.TabStop = false;
            // 
            // ZAIKO_TANKA
            // 
            this.ZAIKO_TANKA.DataField = "ZAIKO_TANKA";
            this.ZAIKO_TANKA.DBFieldsName = "ZAIKO_TANKA";
            this.ZAIKO_TANKA.DefaultBackColor = System.Drawing.Color.Empty;
            this.ZAIKO_TANKA.DisplayItemName = "在庫単価";
            this.ZAIKO_TANKA.DisplayPopUp = null;
            this.ZAIKO_TANKA.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_TANKA.FocusOutCheckMethod")));
            this.ZAIKO_TANKA.FormatSetting = "システム設定(単価書式)";
            this.ZAIKO_TANKA.IsInputErrorOccured = false;
            this.ZAIKO_TANKA.Location = new System.Drawing.Point(256, 0);
            this.ZAIKO_TANKA.Name = "ZAIKO_TANKA";
            this.ZAIKO_TANKA.PopupAfterExecute = null;
            this.ZAIKO_TANKA.PopupBeforeExecute = null;
            this.ZAIKO_TANKA.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZAIKO_TANKA.PopupSearchSendParams")));
            this.ZAIKO_TANKA.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ZAIKO_TANKA.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZAIKO_TANKA.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.ZAIKO_TANKA.RangeSetting = rangeSettingDto3;
            this.ZAIKO_TANKA.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_TANKA.RegistCheckMethod")));
            cellStyle7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle7.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle7.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.ZAIKO_TANKA.Style = cellStyle7;
            this.ZAIKO_TANKA.TabIndex = 6;
            this.ZAIKO_TANKA.TabStop = false;
            this.ZAIKO_TANKA.Visible = false;
            // 
            // ZAIKO_HIRITSU
            // 
            this.ZAIKO_HIRITSU.DataField = "ZAIKO_HIRITSU";
            this.ZAIKO_HIRITSU.DefaultBackColor = System.Drawing.Color.Empty;
            this.ZAIKO_HIRITSU.DisplayItemName = "在庫比率";
            this.ZAIKO_HIRITSU.DisplayPopUp = null;
            this.ZAIKO_HIRITSU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HIRITSU.FocusOutCheckMethod")));
            this.ZAIKO_HIRITSU.FormatSetting = "数値(#)フォーマット";
            this.ZAIKO_HIRITSU.IsInputErrorOccured = false;
            this.ZAIKO_HIRITSU.Location = new System.Drawing.Point(336, 0);
            this.ZAIKO_HIRITSU.Name = "ZAIKO_HIRITSU";
            this.ZAIKO_HIRITSU.PopupAfterExecute = null;
            this.ZAIKO_HIRITSU.PopupBeforeExecute = null;
            this.ZAIKO_HIRITSU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZAIKO_HIRITSU.PopupSearchSendParams")));
            this.ZAIKO_HIRITSU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ZAIKO_HIRITSU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZAIKO_HIRITSU.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            100,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ZAIKO_HIRITSU.RangeSetting = rangeSettingDto1;
            this.ZAIKO_HIRITSU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HIRITSU.RegistCheckMethod")));
            this.ZAIKO_HIRITSU.Size = new System.Drawing.Size(76, 21);
            cellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.ZAIKO_HIRITSU.Style = cellStyle3;
            this.ZAIKO_HIRITSU.TabIndex = 3;
            this.ZAIKO_HIRITSU.Tag = "半角3桁以内で入力してください";
            // 
            // GcZaikoHinmeiTemplate
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 42;
            this.Width = 572;

        }
        

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCellHinmeiCd;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCellHinmeiName;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCellHiritsu;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCellRyou;
        private GrapeCity.Win.MultiRow.CornerHeaderCell cornerHeaderCell1;
        private GrapeCity.Win.MultiRow.RowHeaderCell rowHeaderCell1;
        private r_framework.CustomControl.GcCustomTextBoxCell PERCENT_SIGN;
        internal r_framework.CustomControl.GcCustomAlphaNumTextBoxCell ZAIKO_HINMEI_CD;
        internal r_framework.CustomControl.GcCustomTextBoxCell ZAIKO_HINMEI_NAME;
        internal r_framework.CustomControl.GcCustomNumericTextBox2Cell ZAIKO_RYOU;
        internal r_framework.CustomControl.GcCustomNumericTextBox2Cell ZAIKO_TANKA;
        internal r_framework.CustomControl.GcCustomNumericTextBox2Cell ZAIKO_HIRITSU;
        
    }
}
