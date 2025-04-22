namespace Shougun.Core.Scale.KeiryouNyuuryoku
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class KeiryouNyuuryokuJisseki
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle10 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border4 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle11 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border5 = new GrapeCity.Win.MultiRow.Border();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeiryouNyuuryokuJisseki));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.columnHeaderCell1 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell2 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell3 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell4 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.cornerHeaderCell1 = new GrapeCity.Win.MultiRow.CornerHeaderCell();
            this.rowHeaderCell1 = new GrapeCity.Win.MultiRow.RowHeaderCell();
            this.HINMEI_CD = new r_framework.CustomControl.GcCustomAlphaNumTextBoxCell();
            this.HINMEI_NAME = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.SUURYOU_WARIAI = new r_framework.CustomControl.GcCustomNumericTextBox2Cell();
            this.ROW_NO = new r_framework.CustomControl.GcCustomNumericTextBox2Cell();
            this.DENPYOU_SYSTEM_ID = new r_framework.CustomControl.GcCustomNumericTextBox2Cell();
            this.SEQ = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.DETAIL_SYSTEM_ID = new r_framework.CustomControl.GcCustomTextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.rowHeaderCell1);
            this.Row.Cells.Add(this.HINMEI_CD);
            this.Row.Cells.Add(this.HINMEI_NAME);
            this.Row.Cells.Add(this.SUURYOU_WARIAI);
            this.Row.Cells.Add(this.ROW_NO);
            this.Row.Cells.Add(this.DENPYOU_SYSTEM_ID);
            this.Row.Cells.Add(this.SEQ);
            this.Row.Cells.Add(this.DETAIL_SYSTEM_ID);
            this.Row.Height = 21;
            this.Row.Width = 456;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell1);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell2);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell3);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell4);
            this.columnHeaderSection1.Cells.Add(this.cornerHeaderCell1);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 456;
            // 
            // columnHeaderCell1
            // 
            this.columnHeaderCell1.FlatAppearance.BorderColor = System.Drawing.Color.Teal;
            this.columnHeaderCell1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell1.HoverDirection = GrapeCity.Win.MultiRow.HoverDirection.None;
            this.columnHeaderCell1.Location = new System.Drawing.Point(21, 0);
            this.columnHeaderCell1.Name = "columnHeaderCell1";
            this.columnHeaderCell1.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.None;
            this.columnHeaderCell1.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.AllRows;
            this.columnHeaderCell1.Size = new System.Drawing.Size(40, 21);
            cellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Bottom = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Black);
            border1.Left = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Black);
            border1.Right = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Black);
            border1.Top = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Black);
            cellStyle7.Border = border1;
            cellStyle7.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            cellStyle7.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell1.Style = cellStyle7;
            this.columnHeaderCell1.TabIndex = 0;
            this.columnHeaderCell1.Value = "No";
            // 
            // columnHeaderCell2
            // 
            this.columnHeaderCell2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell2.HoverDirection = GrapeCity.Win.MultiRow.HoverDirection.None;
            this.columnHeaderCell2.Location = new System.Drawing.Point(61, 0);
            this.columnHeaderCell2.Name = "columnHeaderCell2";
            this.columnHeaderCell2.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.None;
            this.columnHeaderCell2.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.AllRows;
            this.columnHeaderCell2.Size = new System.Drawing.Size(79, 21);
            cellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Bottom = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Black);
            border2.Left = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Black);
            border2.Right = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Black);
            border2.Top = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Black);
            cellStyle8.Border = border2;
            cellStyle8.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            cellStyle8.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell2.Style = cellStyle8;
            this.columnHeaderCell2.TabIndex = 1;
            this.columnHeaderCell2.Value = "品名CD";
            // 
            // columnHeaderCell3
            // 
            this.columnHeaderCell3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell3.HoverDirection = GrapeCity.Win.MultiRow.HoverDirection.None;
            this.columnHeaderCell3.Location = new System.Drawing.Point(140, 0);
            this.columnHeaderCell3.Name = "columnHeaderCell3";
            this.columnHeaderCell3.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.None;
            this.columnHeaderCell3.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.AllRows;
            this.columnHeaderCell3.Size = new System.Drawing.Size(216, 21);
            cellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border3.Bottom = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Black);
            border3.Left = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Black);
            border3.Right = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Black);
            border3.Top = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Black);
            cellStyle9.Border = border3;
            cellStyle9.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            cellStyle9.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell3.Style = cellStyle9;
            this.columnHeaderCell3.TabIndex = 2;
            this.columnHeaderCell3.Value = "品名";
            // 
            // columnHeaderCell4
            // 
            this.columnHeaderCell4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell4.HoverDirection = GrapeCity.Win.MultiRow.HoverDirection.None;
            this.columnHeaderCell4.Location = new System.Drawing.Point(356, 0);
            this.columnHeaderCell4.Name = "columnHeaderCell4";
            this.columnHeaderCell4.ResizeMode = GrapeCity.Win.MultiRow.ResizeMode.None;
            this.columnHeaderCell4.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.AllRows;
            this.columnHeaderCell4.Size = new System.Drawing.Size(100, 21);
            cellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border4.Bottom = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Black);
            border4.Left = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Black);
            border4.Right = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Black);
            border4.Top = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Black);
            cellStyle10.Border = border4;
            cellStyle10.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            cellStyle10.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell4.Style = cellStyle10;
            this.columnHeaderCell4.TabIndex = 3;
            this.columnHeaderCell4.Value = "数量割合(%)";
            // 
            // cornerHeaderCell1
            // 
            this.cornerHeaderCell1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cornerHeaderCell1.Location = new System.Drawing.Point(0, 0);
            this.cornerHeaderCell1.Name = "cornerHeaderCell1";
            this.cornerHeaderCell1.Size = new System.Drawing.Size(21, 21);
            cellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border5.Bottom = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Black);
            border5.Left = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Black);
            border5.Right = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Black);
            border5.Top = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Black);
            cellStyle11.Border = border5;
            cellStyle11.ForeColor = System.Drawing.Color.White;
            this.cornerHeaderCell1.Style = cellStyle11;
            this.cornerHeaderCell1.TabIndex = 4;
            // 
            // rowHeaderCell1
            // 
            this.rowHeaderCell1.Location = new System.Drawing.Point(0, 0);
            this.rowHeaderCell1.Name = "rowHeaderCell1";
            this.rowHeaderCell1.Size = new System.Drawing.Size(21, 21);
            this.rowHeaderCell1.TabIndex = 4;
            // 
            // HINMEI_CD
            // 
            this.HINMEI_CD.ChangeUpperCase = true;
            this.HINMEI_CD.CharacterLimitList = null;
            this.HINMEI_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.HINMEI_CD.DataField = "HINMEI_CD";
            this.HINMEI_CD.DBFieldsName = "HINMEI_CD";
            this.HINMEI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.HINMEI_CD.DisplayItemName = "品名CD";
            this.HINMEI_CD.DisplayPopUp = null;
            this.HINMEI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_CD.FocusOutCheckMethod")));
            this.HINMEI_CD.GetCodeMasterField = "HINMEI_CD";
            this.HINMEI_CD.IsInputErrorOccured = false;
            this.HINMEI_CD.ItemDefinedTypes = "varchar";
            this.HINMEI_CD.Location = new System.Drawing.Point(61, 0);
            this.HINMEI_CD.MaxLength = 6;
            this.HINMEI_CD.Name = "HINMEI_CD";
            this.HINMEI_CD.PopupAfterExecute = null;
            this.HINMEI_CD.PopupAfterExecuteMethod = "HINMEI_CD_PopupAfterExecuteMethod2";
            this.HINMEI_CD.PopupBeforeExecute = null;
            this.HINMEI_CD.PopupBeforeExecuteMethod = "HINMEI_CD_PopupBeforeExecuteMethod2";
            this.HINMEI_CD.PopupGetMasterField = "HINMEI_CD, HINMEI_NAME_RYAKU";
            this.HINMEI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HINMEI_CD.PopupSearchSendParams")));
            this.HINMEI_CD.PopupSetFormField = "HINMEI_CD, HINMEI_NAME_RYAKU";
            this.HINMEI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_HINMEI;
            this.HINMEI_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.HINMEI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HINMEI_CD.popupWindowSetting")));
            this.HINMEI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_CD.RegistCheckMethod")));
            this.HINMEI_CD.SetFormField = "HINMEI_CD";
            this.HINMEI_CD.Size = new System.Drawing.Size(79, 21);
            cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle1.ImeSentenceMode = GrapeCity.Win.MultiRow.ImeSentenceMode.Normal;
            cellStyle1.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.HINMEI_CD.Style = cellStyle1;
            this.HINMEI_CD.TabIndex = 5;
            this.HINMEI_CD.Tag = "品名CDを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.HINMEI_CD.ZeroPaddengFlag = true;
            // 
            // HINMEI_NAME
            // 
            this.HINMEI_NAME.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.HINMEI_NAME.DBFieldsName = "HINMEI_NAME";
            this.HINMEI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.HINMEI_NAME.DisplayItemName = "品名";
            this.HINMEI_NAME.DisplayPopUp = null;
            this.HINMEI_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_NAME.FocusOutCheckMethod")));
            this.HINMEI_NAME.IsInputErrorOccured = false;
            this.HINMEI_NAME.ItemDefinedTypes = "varchar";
            this.HINMEI_NAME.Location = new System.Drawing.Point(140, 0);
            this.HINMEI_NAME.MaxLength = 40;
            this.HINMEI_NAME.Name = "HINMEI_NAME";
            this.HINMEI_NAME.PopupAfterExecute = null;
            this.HINMEI_NAME.PopupBeforeExecute = null;
            this.HINMEI_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HINMEI_NAME.PopupSearchSendParams")));
            this.HINMEI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HINMEI_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HINMEI_NAME.popupWindowSetting")));
            this.HINMEI_NAME.ReadOnly = true;
            this.HINMEI_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_NAME.RegistCheckMethod")));
            this.HINMEI_NAME.Size = new System.Drawing.Size(216, 21);
            cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle2.ImeSentenceMode = GrapeCity.Win.MultiRow.ImeSentenceMode.Normal;
            cellStyle2.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.HINMEI_NAME.Style = cellStyle2;
            this.HINMEI_NAME.TabIndex = 6;
            // 
            // SUURYOU_WARIAI
            // 
            this.SUURYOU_WARIAI.DataField = "SUURYOU_WARIAI";
            this.SUURYOU_WARIAI.DBFieldsName = "SUURYOU_WARIAI";
            this.SUURYOU_WARIAI.DefaultBackColor = System.Drawing.Color.Empty;
            this.SUURYOU_WARIAI.DisplayItemName = "数量割合";
            this.SUURYOU_WARIAI.DisplayPopUp = null;
            this.SUURYOU_WARIAI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SUURYOU_WARIAI.FocusOutCheckMethod")));
            this.SUURYOU_WARIAI.FormatSetting = "数値(#)フォーマット";
            this.SUURYOU_WARIAI.IsInputErrorOccured = false;
            this.SUURYOU_WARIAI.ItemDefinedTypes = "smallint";
            this.SUURYOU_WARIAI.Location = new System.Drawing.Point(356, 0);
            this.SUURYOU_WARIAI.Name = "SUURYOU_WARIAI";
            this.SUURYOU_WARIAI.PopupAfterExecute = null;
            this.SUURYOU_WARIAI.PopupBeforeExecute = null;
            this.SUURYOU_WARIAI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SUURYOU_WARIAI.PopupSearchSendParams")));
            this.SUURYOU_WARIAI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SUURYOU_WARIAI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SUURYOU_WARIAI.popupWindowSetting")));
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
            this.SUURYOU_WARIAI.RangeSetting = rangeSettingDto1;
            this.SUURYOU_WARIAI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SUURYOU_WARIAI.RegistCheckMethod")));
            this.SUURYOU_WARIAI.Size = new System.Drawing.Size(100, 21);
            cellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle3.ImeSentenceMode = GrapeCity.Win.MultiRow.ImeSentenceMode.Normal;
            cellStyle3.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.SUURYOU_WARIAI.Style = cellStyle3;
            this.SUURYOU_WARIAI.TabIndex = 7;
            this.SUURYOU_WARIAI.Tag = "整数値(1-100)で入力してください";
            // 
            // ROW_NO
            // 
            this.ROW_NO.DataField = "ROW_NO";
            this.ROW_NO.DBFieldsName = "ROW_NO";
            this.ROW_NO.DefaultBackColor = System.Drawing.Color.Empty;
            this.ROW_NO.DisplayItemName = "No";
            this.ROW_NO.DisplayPopUp = null;
            this.ROW_NO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ROW_NO.FocusOutCheckMethod")));
            this.ROW_NO.IsInputErrorOccured = false;
            this.ROW_NO.ItemDefinedTypes = "smallint";
            this.ROW_NO.Location = new System.Drawing.Point(21, 0);
            this.ROW_NO.Name = "ROW_NO";
            this.ROW_NO.PopupAfterExecute = null;
            this.ROW_NO.PopupBeforeExecute = null;
            this.ROW_NO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ROW_NO.PopupSearchSendParams")));
            this.ROW_NO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ROW_NO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ROW_NO.popupWindowSetting")));
            this.ROW_NO.RangeSetting = rangeSettingDto2;
            this.ROW_NO.ReadOnly = true;
            this.ROW_NO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ROW_NO.RegistCheckMethod")));
            this.ROW_NO.Size = new System.Drawing.Size(40, 21);
            cellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle4.ImeSentenceMode = GrapeCity.Win.MultiRow.ImeSentenceMode.Normal;
            cellStyle4.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.ROW_NO.Style = cellStyle4;
            this.ROW_NO.TabIndex = 8;
            this.ROW_NO.TabStop = false;
            // 
            // DENPYOU_SYSTEM_ID
            // 
            this.DENPYOU_SYSTEM_ID.DataField = "DENPYOU_SYSTEM_ID";
            this.DENPYOU_SYSTEM_ID.DBFieldsName = "DENPYOU_SYSTEM_ID";
            this.DENPYOU_SYSTEM_ID.DefaultBackColor = System.Drawing.Color.Empty;
            this.DENPYOU_SYSTEM_ID.DisplayPopUp = null;
            this.DENPYOU_SYSTEM_ID.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_SYSTEM_ID.FocusOutCheckMethod")));
            this.DENPYOU_SYSTEM_ID.IsInputErrorOccured = false;
            this.DENPYOU_SYSTEM_ID.ItemDefinedTypes = "bigint";
            this.DENPYOU_SYSTEM_ID.Location = new System.Drawing.Point(299, 0);
            this.DENPYOU_SYSTEM_ID.Name = "DENPYOU_SYSTEM_ID";
            this.DENPYOU_SYSTEM_ID.PopupAfterExecute = null;
            this.DENPYOU_SYSTEM_ID.PopupBeforeExecute = null;
            this.DENPYOU_SYSTEM_ID.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DENPYOU_SYSTEM_ID.PopupSearchSendParams")));
            this.DENPYOU_SYSTEM_ID.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DENPYOU_SYSTEM_ID.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DENPYOU_SYSTEM_ID.popupWindowSetting")));
            this.DENPYOU_SYSTEM_ID.RangeSetting = rangeSettingDto3;
            this.DENPYOU_SYSTEM_ID.ReadOnly = true;
            this.DENPYOU_SYSTEM_ID.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_SYSTEM_ID.RegistCheckMethod")));
            this.DENPYOU_SYSTEM_ID.Size = new System.Drawing.Size(24, 21);
            cellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle5.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle5.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle5.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.DENPYOU_SYSTEM_ID.Style = cellStyle5;
            this.DENPYOU_SYSTEM_ID.TabIndex = 9;
            this.DENPYOU_SYSTEM_ID.TabStop = false;
            this.DENPYOU_SYSTEM_ID.Visible = false;
            // 
            // SEQ
            // 
            this.SEQ.DataField = "SEQ";
            this.SEQ.DefaultBackColor = System.Drawing.Color.Empty;
            this.SEQ.DisplayItemName = "SEQ";
            this.SEQ.DisplayPopUp = null;
            this.SEQ.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEQ.FocusOutCheckMethod")));
            this.SEQ.IsInputErrorOccured = false;
            this.SEQ.ItemDefinedTypes = "bigint";
            this.SEQ.Location = new System.Drawing.Point(323, 0);
            this.SEQ.Name = "SEQ";
            this.SEQ.PopupAfterExecute = null;
            this.SEQ.PopupBeforeExecute = null;
            this.SEQ.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEQ.PopupSearchSendParams")));
            this.SEQ.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SEQ.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEQ.popupWindowSetting")));
            this.SEQ.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEQ.RegistCheckMethod")));
            this.SEQ.Size = new System.Drawing.Size(20, 21);
            cellStyle6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle6.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle6.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.SEQ.Style = cellStyle6;
            this.SEQ.TabIndex = 10;
            this.SEQ.TabStop = false;
            this.SEQ.Tag = "マニフェスト連携用の設定";
            this.SEQ.Visible = false;
            // 
            // DETAIL_SYSTEM_ID
            // 
            this.DETAIL_SYSTEM_ID.DataField = "DETAIL_SYSTEM_ID";
            this.DETAIL_SYSTEM_ID.DefaultBackColor = System.Drawing.Color.Empty;
            this.DETAIL_SYSTEM_ID.DisplayItemName = "DETAIL_SYSTEM_ID";
            this.DETAIL_SYSTEM_ID.DisplayPopUp = null;
            this.DETAIL_SYSTEM_ID.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DETAIL_SYSTEM_ID.FocusOutCheckMethod")));
            this.DETAIL_SYSTEM_ID.IsInputErrorOccured = false;
            this.DETAIL_SYSTEM_ID.ItemDefinedTypes = "bigint";
            this.DETAIL_SYSTEM_ID.Location = new System.Drawing.Point(279, 0);
            this.DETAIL_SYSTEM_ID.Name = "DETAIL_SYSTEM_ID";
            this.DETAIL_SYSTEM_ID.PopupAfterExecute = null;
            this.DETAIL_SYSTEM_ID.PopupBeforeExecute = null;
            this.DETAIL_SYSTEM_ID.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DETAIL_SYSTEM_ID.PopupSearchSendParams")));
            this.DETAIL_SYSTEM_ID.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DETAIL_SYSTEM_ID.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DETAIL_SYSTEM_ID.popupWindowSetting")));
            this.DETAIL_SYSTEM_ID.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DETAIL_SYSTEM_ID.RegistCheckMethod")));
            this.DETAIL_SYSTEM_ID.Size = new System.Drawing.Size(20, 21);
            cellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle5.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle5.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.DETAIL_SYSTEM_ID.Style = cellStyle5;
            this.DETAIL_SYSTEM_ID.TabIndex = 9;
            this.DETAIL_SYSTEM_ID.TabStop = false;
            this.DETAIL_SYSTEM_ID.Tag = "マニフェスト連携用の設定";
            this.DETAIL_SYSTEM_ID.Visible = false;

            // 
            // KeiryouNyuuryokuJisseki
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 42;
            this.Width = 456;

        }
        

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.RowHeaderCell rowHeaderCell1;
        internal GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell1;
        internal GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell2;
        internal GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell3;
        internal GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell4;
        internal GrapeCity.Win.MultiRow.CornerHeaderCell cornerHeaderCell1;
        internal r_framework.CustomControl.GcCustomNumericTextBox2Cell ROW_NO;
        internal r_framework.CustomControl.GcCustomAlphaNumTextBoxCell HINMEI_CD;
        internal r_framework.CustomControl.GcCustomTextBoxCell HINMEI_NAME;
        internal r_framework.CustomControl.GcCustomNumericTextBox2Cell SUURYOU_WARIAI;
        internal r_framework.CustomControl.GcCustomNumericTextBox2Cell DENPYOU_SYSTEM_ID;
        private r_framework.CustomControl.GcCustomTextBoxCell SEQ;
        private r_framework.CustomControl.GcCustomTextBoxCell DETAIL_SYSTEM_ID;
        
    }
}
