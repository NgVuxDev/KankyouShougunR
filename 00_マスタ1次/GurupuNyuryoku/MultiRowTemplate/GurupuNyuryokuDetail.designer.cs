namespace GurupuNyuryoku.MultiRowTemplate
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class GurupuNyuryokuDetail
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle13 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border6 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle14 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border7 = new GrapeCity.Win.MultiRow.Border();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GurupuNyuryokuDetail));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.columnHeaderCell0 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell1 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell2 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader2 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader3 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell9 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell10 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.DELETE_FLG = new r_framework.CustomControl.GcCustomCheckBoxCell();
            this.GURUPU_CD = new r_framework.CustomControl.GcCustomNumericTextBox2Cell();
            this.GURUPU_NAME = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.UPDATE_USER = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.UPDATE_DATE = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.CREATE_USER = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.CREATE_DATE = new r_framework.CustomControl.GcCustomTextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.DELETE_FLG);
            this.Row.Cells.Add(this.GURUPU_CD);
            this.Row.Cells.Add(this.GURUPU_NAME);
            this.Row.Cells.Add(this.UPDATE_USER);
            this.Row.Cells.Add(this.UPDATE_DATE);
            this.Row.Cells.Add(this.CREATE_USER);
            this.Row.Cells.Add(this.CREATE_DATE);
            this.Row.Height = 21;
            this.Row.Width = 820;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell0);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell1);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell2);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader2);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader3);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell9);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell10);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 820;
            // 
            // columnHeaderCell0
            // 
            this.columnHeaderCell0.FilterCellIndex = 0;
            this.columnHeaderCell0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell0.Location = new System.Drawing.Point(0, 0);
            this.columnHeaderCell0.Name = "columnHeaderCell0";
            this.columnHeaderCell0.Size = new System.Drawing.Size(40, 21);
            cellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle8.Border = border1;
            cellStyle8.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle8.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell0.Style = cellStyle8;
            this.columnHeaderCell0.TabIndex = 1;
            this.columnHeaderCell0.Value = "削除";
            this.columnHeaderCell0.ViewSearchItem = false;
            // 
            // columnHeaderCell1
            // 
            this.columnHeaderCell1.FilterCellIndex = 1;
            this.columnHeaderCell1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell1.Location = new System.Drawing.Point(40, 0);
            this.columnHeaderCell1.Name = "columnHeaderCell1";
            this.columnHeaderCell1.Size = new System.Drawing.Size(110, 21);
            cellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle9.Border = border2;
            cellStyle9.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle9.ForeColor = System.Drawing.Color.White;
            cellStyle9.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.columnHeaderCell1.Style = cellStyle9;
            this.columnHeaderCell1.TabIndex = 18;
            this.columnHeaderCell1.Value = "グループCD";
            this.columnHeaderCell1.ViewSearchItem = true;
            // 
            // columnHeaderCell2
            // 
            this.columnHeaderCell2.FilterCellIndex = 2;
            this.columnHeaderCell2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell2.Location = new System.Drawing.Point(150, 0);
            this.columnHeaderCell2.Name = "columnHeaderCell2";
            this.columnHeaderCell2.Size = new System.Drawing.Size(150, 21);
            cellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle10.Border = border3;
            cellStyle10.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle10.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell2.Style = cellStyle10;
            this.columnHeaderCell2.TabIndex = 19;
            this.columnHeaderCell2.Value = "グループ名";
            this.columnHeaderCell2.ViewSearchItem = true;
            // 
            // gcCustomColumnHeader2
            // 
            this.gcCustomColumnHeader2.FilterCellIndex = 7;
            this.gcCustomColumnHeader2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader2.Location = new System.Drawing.Point(300, 0);
            this.gcCustomColumnHeader2.Name = "gcCustomColumnHeader2";
            this.gcCustomColumnHeader2.Size = new System.Drawing.Size(120, 21);
            cellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle11.Border = border4;
            cellStyle11.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle11.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader2.Style = cellStyle11;
            this.gcCustomColumnHeader2.TabIndex = 23;
            this.gcCustomColumnHeader2.Value = "更新者";
            this.gcCustomColumnHeader2.ViewSearchItem = true;
            // 
            // gcCustomColumnHeader3
            // 
            this.gcCustomColumnHeader3.FilterCellIndex = 8;
            this.gcCustomColumnHeader3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader3.Location = new System.Drawing.Point(420, 0);
            this.gcCustomColumnHeader3.Name = "gcCustomColumnHeader3";
            this.gcCustomColumnHeader3.Size = new System.Drawing.Size(140, 21);
            cellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border5.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle12.Border = border5;
            cellStyle12.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle12.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader3.Style = cellStyle12;
            this.gcCustomColumnHeader3.TabIndex = 24;
            this.gcCustomColumnHeader3.Value = "更新日";
            this.gcCustomColumnHeader3.ViewSearchItem = true;
            // 
            // columnHeaderCell9
            // 
            this.columnHeaderCell9.FilterCellIndex = 9;
            this.columnHeaderCell9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell9.Location = new System.Drawing.Point(560, 0);
            this.columnHeaderCell9.Name = "columnHeaderCell9";
            this.columnHeaderCell9.Size = new System.Drawing.Size(120, 21);
            cellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border6.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle13.Border = border6;
            cellStyle13.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle13.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell9.Style = cellStyle13;
            this.columnHeaderCell9.TabIndex = 14;
            this.columnHeaderCell9.Value = "作成者";
            this.columnHeaderCell9.ViewSearchItem = true;
            // 
            // columnHeaderCell10
            // 
            this.columnHeaderCell10.FilterCellIndex = 10;
            this.columnHeaderCell10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell10.Location = new System.Drawing.Point(680, 0);
            this.columnHeaderCell10.Name = "columnHeaderCell10";
            this.columnHeaderCell10.Size = new System.Drawing.Size(140, 21);
            cellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border7.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle14.Border = border7;
            cellStyle14.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle14.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell10.Style = cellStyle14;
            this.columnHeaderCell10.TabIndex = 15;
            this.columnHeaderCell10.Value = "作成日";
            this.columnHeaderCell10.ViewSearchItem = true;
            // 
            // DELETE_FLG
            // 
            this.DELETE_FLG.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.DELETE_FLG.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.DELETE_FLG.DataField = "DELETE_FLG";
            this.DELETE_FLG.DBFieldsName = null;
            this.DELETE_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.DELETE_FLG.DisplayItemName = null;
            this.DELETE_FLG.ErrorMessage = null;
            this.DELETE_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DELETE_FLG.FocusOutCheckMethod")));
            this.DELETE_FLG.GetCodeMasterField = null;
            this.DELETE_FLG.IndeterminateValue = "False";
            this.DELETE_FLG.IsInputErrorOccured = false;
            this.DELETE_FLG.ItemDefinedTypes = null;
            this.DELETE_FLG.Location = new System.Drawing.Point(0, 0);
            this.DELETE_FLG.Name = "DELETE_FLG";
            this.DELETE_FLG.PopupAfterExecute = null;
            this.DELETE_FLG.PopupBeforeExecute = null;
            this.DELETE_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DELETE_FLG.PopupSearchSendParams")));
            this.DELETE_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DELETE_FLG.PopupWindowName = null;
            this.DELETE_FLG.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DELETE_FLG.popupWindowSetting")));
            this.DELETE_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DELETE_FLG.RegistCheckMethod")));
            this.DELETE_FLG.SearchDisplayFlag = 0;
            this.DELETE_FLG.SetFormField = null;
            this.DELETE_FLG.ShortItemName = null;
            this.DELETE_FLG.Size = new System.Drawing.Size(40, 21);
            cellStyle1.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.DELETE_FLG.Style = cellStyle1;
            this.DELETE_FLG.TabIndex = 0;
            this.DELETE_FLG.Tag = "削除する場合にはチェックを付けてください";
            this.DELETE_FLG.ZeroPaddengFlag = false;
            // 
            // GURUPU_CD
            // 
            this.GURUPU_CD.CustomFormatSetting = "000";
            this.GURUPU_CD.DataField = "GURUPU_CD";
            this.GURUPU_CD.DBFieldsName = "GURUPU_CD";
            this.GURUPU_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GURUPU_CD.DisplayItemName = "グループCD";
            this.GURUPU_CD.DisplayPopUp = null;
            this.GURUPU_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GURUPU_CD.FocusOutCheckMethod")));
            this.GURUPU_CD.FormatSetting = "カスタム";
            this.GURUPU_CD.IsInputErrorOccured = false;
            this.GURUPU_CD.ItemDefinedTypes = "varchar";
            this.GURUPU_CD.Location = new System.Drawing.Point(40, 0);
            this.GURUPU_CD.Name = "GURUPU_CD";
            this.GURUPU_CD.PopupAfterExecute = null;
            this.GURUPU_CD.PopupBeforeExecute = null;
            this.GURUPU_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GURUPU_CD.PopupSearchSendParams")));
            this.GURUPU_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GURUPU_CD.PopupWindowName = "";
            this.GURUPU_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GURUPU_CD.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.GURUPU_CD.RangeSetting = rangeSettingDto1;
            this.GURUPU_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GURUPU_CD.RegistCheckMethod")));
            this.GURUPU_CD.ShortItemName = "グループCD";
            this.GURUPU_CD.Size = new System.Drawing.Size(110, 21);
            cellStyle2.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle2.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.GURUPU_CD.Style = cellStyle2;
            this.GURUPU_CD.TabIndex = 1;
            this.GURUPU_CD.Tag = "半角3文字以内で入力してください。";
            // 
            // GURUPU_NAME
            // 
            this.GURUPU_NAME.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.GURUPU_NAME.CopyAutoSetControl = "GURUPU_NAME";
            this.GURUPU_NAME.CopyAutoSetWithSpace = true;
            this.GURUPU_NAME.DataField = "GURUPU_NAME";
            this.GURUPU_NAME.DBFieldsName = "GURUPU_NAME";
            this.GURUPU_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.GURUPU_NAME.DisplayItemName = "グループ名";
            this.GURUPU_NAME.DisplayPopUp = null;
            this.GURUPU_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GURUPU_NAME.FocusOutCheckMethod")));
            this.GURUPU_NAME.FuriganaAutoSetControl = "";
            this.GURUPU_NAME.IsInputErrorOccured = false;
            this.GURUPU_NAME.ItemDefinedTypes = "varchar";
            this.GURUPU_NAME.Location = new System.Drawing.Point(150, 0);
            this.GURUPU_NAME.MaxLength = 20;
            this.GURUPU_NAME.Name = "GURUPU_NAME";
            this.GURUPU_NAME.PopupAfterExecute = null;
            this.GURUPU_NAME.PopupBeforeExecute = null;
            this.GURUPU_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GURUPU_NAME.PopupSearchSendParams")));
            this.GURUPU_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GURUPU_NAME.PopupWindowName = "";
            this.GURUPU_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GURUPU_NAME.popupWindowSetting")));
            this.GURUPU_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GURUPU_NAME.RegistCheckMethod")));
            this.GURUPU_NAME.ShortItemName = "グループ名";
            this.GURUPU_NAME.Size = new System.Drawing.Size(150, 21);
            cellStyle3.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            cellStyle3.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.GURUPU_NAME.Style = cellStyle3;
            this.GURUPU_NAME.TabIndex = 2;
            this.GURUPU_NAME.Tag = "全角10文字/半角20文字以内で入力してください";
            // 
            // UPDATE_USER
            // 
            this.UPDATE_USER.DataField = "UPDATE_USER";
            this.UPDATE_USER.DBFieldsName = "UPDATE_USER";
            this.UPDATE_USER.DefaultBackColor = System.Drawing.Color.Empty;
            this.UPDATE_USER.DisplayItemName = "更新者";
            this.UPDATE_USER.DisplayPopUp = null;
            this.UPDATE_USER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_USER.FocusOutCheckMethod")));
            this.UPDATE_USER.IsInputErrorOccured = false;
            this.UPDATE_USER.ItemDefinedTypes = "varchar";
            this.UPDATE_USER.Location = new System.Drawing.Point(300, 0);
            this.UPDATE_USER.MaxLength = 0;
            this.UPDATE_USER.Name = "UPDATE_USER";
            this.UPDATE_USER.PopupAfterExecute = null;
            this.UPDATE_USER.PopupBeforeExecute = null;
            this.UPDATE_USER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPDATE_USER.PopupSearchSendParams")));
            this.UPDATE_USER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPDATE_USER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPDATE_USER.popupWindowSetting")));
            this.UPDATE_USER.ReadOnly = true;
            this.UPDATE_USER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_USER.RegistCheckMethod")));
            this.UPDATE_USER.Selectable = false;
            this.UPDATE_USER.ShortItemName = "更新者";
            this.UPDATE_USER.Size = new System.Drawing.Size(120, 21);
            cellStyle4.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle4.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.UPDATE_USER.Style = cellStyle4;
            this.UPDATE_USER.TabIndex = 8;
            this.UPDATE_USER.Tag = "最終更新者が表示されます";
            // 
            // UPDATE_DATE
            // 
            this.UPDATE_DATE.DataField = "UPDATE_DATE";
            this.UPDATE_DATE.DBFieldsName = "UPDATE_DATE";
            this.UPDATE_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            this.UPDATE_DATE.DisplayItemName = "更新日";
            this.UPDATE_DATE.DisplayPopUp = null;
            this.UPDATE_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_DATE.FocusOutCheckMethod")));
            this.UPDATE_DATE.IsInputErrorOccured = false;
            this.UPDATE_DATE.ItemDefinedTypes = "datetime";
            this.UPDATE_DATE.Location = new System.Drawing.Point(420, 0);
            this.UPDATE_DATE.MaxLength = 0;
            this.UPDATE_DATE.Name = "UPDATE_DATE";
            this.UPDATE_DATE.PopupAfterExecute = null;
            this.UPDATE_DATE.PopupBeforeExecute = null;
            this.UPDATE_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPDATE_DATE.PopupSearchSendParams")));
            this.UPDATE_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPDATE_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPDATE_DATE.popupWindowSetting")));
            this.UPDATE_DATE.ReadOnly = true;
            this.UPDATE_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_DATE.RegistCheckMethod")));
            this.UPDATE_DATE.Selectable = false;
            this.UPDATE_DATE.ShortItemName = "更新日";
            this.UPDATE_DATE.Size = new System.Drawing.Size(140, 21);
            cellStyle5.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            cellStyle5.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle5.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle5.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.UPDATE_DATE.Style = cellStyle5;
            this.UPDATE_DATE.TabIndex = 9;
            this.UPDATE_DATE.Tag = "最終更新日が表示されます";
            // 
            // CREATE_USER
            // 
            this.CREATE_USER.DataField = "CREATE_USER";
            this.CREATE_USER.DBFieldsName = "CREATE_USER";
            this.CREATE_USER.DefaultBackColor = System.Drawing.Color.Empty;
            this.CREATE_USER.DisplayItemName = "作成者";
            this.CREATE_USER.DisplayPopUp = null;
            this.CREATE_USER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_USER.FocusOutCheckMethod")));
            this.CREATE_USER.IsInputErrorOccured = false;
            this.CREATE_USER.ItemDefinedTypes = "varchar";
            this.CREATE_USER.Location = new System.Drawing.Point(560, 0);
            this.CREATE_USER.MaxLength = 0;
            this.CREATE_USER.Name = "CREATE_USER";
            this.CREATE_USER.PopupAfterExecute = null;
            this.CREATE_USER.PopupBeforeExecute = null;
            this.CREATE_USER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CREATE_USER.PopupSearchSendParams")));
            this.CREATE_USER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_USER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_USER.popupWindowSetting")));
            this.CREATE_USER.ReadOnly = true;
            this.CREATE_USER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_USER.RegistCheckMethod")));
            this.CREATE_USER.Selectable = false;
            this.CREATE_USER.ShortItemName = "作成者";
            this.CREATE_USER.Size = new System.Drawing.Size(120, 21);
            cellStyle6.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            cellStyle6.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle6.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.CREATE_USER.Style = cellStyle6;
            this.CREATE_USER.TabIndex = 10;
            this.CREATE_USER.Tag = "初回作成者が表示されます";
            // 
            // CREATE_DATE
            // 
            this.CREATE_DATE.DataField = "CREATE_DATE";
            this.CREATE_DATE.DBFieldsName = "CREATE_DATE";
            this.CREATE_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            this.CREATE_DATE.DisplayItemName = "作成日";
            this.CREATE_DATE.DisplayPopUp = null;
            this.CREATE_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_DATE.FocusOutCheckMethod")));
            this.CREATE_DATE.IsInputErrorOccured = false;
            this.CREATE_DATE.ItemDefinedTypes = "datetime";
            this.CREATE_DATE.Location = new System.Drawing.Point(680, 0);
            this.CREATE_DATE.MaxLength = 0;
            this.CREATE_DATE.Name = "CREATE_DATE";
            this.CREATE_DATE.PopupAfterExecute = null;
            this.CREATE_DATE.PopupBeforeExecute = null;
            this.CREATE_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CREATE_DATE.PopupSearchSendParams")));
            this.CREATE_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_DATE.popupWindowSetting")));
            this.CREATE_DATE.ReadOnly = true;
            this.CREATE_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_DATE.RegistCheckMethod")));
            this.CREATE_DATE.Selectable = false;
            this.CREATE_DATE.ShortItemName = "作成日";
            this.CREATE_DATE.Size = new System.Drawing.Size(140, 21);
            cellStyle7.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            cellStyle7.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle7.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle7.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.CREATE_DATE.Style = cellStyle7;
            this.CREATE_DATE.TabIndex = 11;
            this.CREATE_DATE.Tag = "初回作成日が表示されます";
            // 
            // GurupuNyuryokuDetail
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 42;
            this.Width = 820;

        }
        

        #endregion

        internal r_framework.CustomControl.GcCustomCheckBoxCell DELETE_FLG;
        internal GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        internal r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell0;
        internal r_framework.CustomControl.GcCustomNumericTextBox2Cell GURUPU_CD;
        internal r_framework.CustomControl.GcCustomTextBoxCell GURUPU_NAME;
        internal r_framework.CustomControl.GcCustomTextBoxCell UPDATE_USER;
        internal r_framework.CustomControl.GcCustomTextBoxCell UPDATE_DATE;
        internal r_framework.CustomControl.GcCustomTextBoxCell CREATE_USER;
        internal r_framework.CustomControl.GcCustomTextBoxCell CREATE_DATE;
        internal r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell9;
        internal r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell10;
        internal r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell1;
        internal r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell2;
        internal r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader2;
        internal r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader3;
    }
}
