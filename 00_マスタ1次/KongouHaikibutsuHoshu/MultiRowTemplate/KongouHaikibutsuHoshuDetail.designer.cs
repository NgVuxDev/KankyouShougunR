namespace KongouHaikibutsuHoshu.MultiRowTemplate
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class KongouHaikibutsuHoshuDetail
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle10 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle11 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle12 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle13 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border4 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle14 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border5 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle15 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border6 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle16 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border7 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle17 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border8 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle18 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border9 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KongouHaikibutsuHoshuDetail));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle19 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border10 = new GrapeCity.Win.MultiRow.Border();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.columnHeaderCell1 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell2 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell3 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell4 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell5 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell8 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell9 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell10 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell11 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.CREATE_USER = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.UPDATE_USER = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.CREATE_DATE = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.UPDATE_DATE = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.DELETE_FLG = new r_framework.CustomControl.GcCustomCheckBoxCell();
            this.HAIKI_SHURUI_NAME_RYAKU = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.HAIKI_SHURUI_CD = new r_framework.CustomControl.GcCustomAlphaNumTextBoxCell();
            this.KONGOU_HAIKIBUTSU_BIKOU = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.HAIKI_HIRITSU = new r_framework.CustomControl.GcCustomNumericTextBox2Cell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.DELETE_FLG);
            this.Row.Cells.Add(this.HAIKI_SHURUI_CD);
            this.Row.Cells.Add(this.HAIKI_SHURUI_NAME_RYAKU);
            this.Row.Cells.Add(this.HAIKI_HIRITSU);
            this.Row.Cells.Add(this.KONGOU_HAIKIBUTSU_BIKOU);
            this.Row.Cells.Add(this.UPDATE_USER);
            this.Row.Cells.Add(this.UPDATE_DATE);
            this.Row.Cells.Add(this.CREATE_USER);
            this.Row.Cells.Add(this.CREATE_DATE);
            this.Row.Height = 21;
            this.Row.Width = 1035;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell1);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell2);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell3);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell4);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell5);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell8);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell9);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell10);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell11);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 1035;
            // 
            // columnHeaderCell1
            // 
            this.columnHeaderCell1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell1.Location = new System.Drawing.Point(0, 0);
            this.columnHeaderCell1.Name = "columnHeaderCell1";
            this.columnHeaderCell1.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell1.Size = new System.Drawing.Size(40, 21);
            cellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle10.Border = border1;
            cellStyle10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle10.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell1.Style = cellStyle10;
            this.columnHeaderCell1.TabIndex = 0;
            this.columnHeaderCell1.Value = "削除";
            this.columnHeaderCell1.ViewSearchItem = false;
            // 
            // columnHeaderCell2
            // 
            this.columnHeaderCell2.FilterCellIndex = 3;
            this.columnHeaderCell2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell2.Location = new System.Drawing.Point(40, 0);
            this.columnHeaderCell2.Name = "columnHeaderCell2";
            this.columnHeaderCell2.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell2.Size = new System.Drawing.Size(95, 21);
            cellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle11.Border = border2;
            cellStyle11.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle11.ForeColor = System.Drawing.Color.White;
            cellStyle11.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.columnHeaderCell2.Style = cellStyle11;
            this.columnHeaderCell2.TabIndex = 1;
            this.columnHeaderCell2.Value = "廃棄物種類CD";
            this.columnHeaderCell2.ViewSearchItem = true;
            // 
            // columnHeaderCell3
            // 
            this.columnHeaderCell3.FilterCellIndex = 3;
            this.columnHeaderCell3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell3.Location = new System.Drawing.Point(135, 0);
            this.columnHeaderCell3.Name = "columnHeaderCell3";
            this.columnHeaderCell3.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell3.Size = new System.Drawing.Size(150, 21);
            cellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle12.Border = border3;
            cellStyle12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle12.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell3.Style = cellStyle12;
            this.columnHeaderCell3.TabIndex = 2;
            this.columnHeaderCell3.Value = "廃棄物種類名";
            this.columnHeaderCell3.ViewSearchItem = true;
            // 
            // columnHeaderCell4
            // 
            this.columnHeaderCell4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell4.Location = new System.Drawing.Point(285, 0);
            this.columnHeaderCell4.Name = "columnHeaderCell4";
            this.columnHeaderCell4.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell4.Size = new System.Drawing.Size(80, 22);
            cellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle13.Border = border4;
            cellStyle13.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle13.ForeColor = System.Drawing.Color.White;
            cellStyle13.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.columnHeaderCell4.Style = cellStyle13;
            this.columnHeaderCell4.TabIndex = 3;
            this.columnHeaderCell4.Value = "廃棄物比率";
            this.columnHeaderCell4.ViewSearchItem = true;
            // 
            // columnHeaderCell5
            // 
            this.columnHeaderCell5.FilterCellIndex = 3;
            this.columnHeaderCell5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell5.Location = new System.Drawing.Point(365, 0);
            this.columnHeaderCell5.Name = "columnHeaderCell5";
            this.columnHeaderCell5.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell5.Size = new System.Drawing.Size(150, 21);
            cellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border5.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle14.Border = border5;
            cellStyle14.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle14.ForeColor = System.Drawing.Color.White;
            cellStyle14.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.columnHeaderCell5.Style = cellStyle14;
            this.columnHeaderCell5.TabIndex = 4;
            this.columnHeaderCell5.Value = "備考";
            this.columnHeaderCell5.ViewSearchItem = true;
            // 
            // columnHeaderCell8
            // 
            this.columnHeaderCell8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell8.Location = new System.Drawing.Point(515, 0);
            this.columnHeaderCell8.Name = "columnHeaderCell8";
            this.columnHeaderCell8.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell8.Size = new System.Drawing.Size(120, 22);
            cellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border6.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle15.Border = border6;
            cellStyle15.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle15.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell8.Style = cellStyle15;
            this.columnHeaderCell8.TabIndex = 5;
            this.columnHeaderCell8.Value = "更新者";
            this.columnHeaderCell8.ViewSearchItem = true;
            // 
            // columnHeaderCell9
            // 
            this.columnHeaderCell9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell9.Location = new System.Drawing.Point(635, 0);
            this.columnHeaderCell9.Name = "columnHeaderCell9";
            this.columnHeaderCell9.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell9.Size = new System.Drawing.Size(140, 22);
            cellStyle16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border7.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle16.Border = border7;
            cellStyle16.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle16.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell9.Style = cellStyle16;
            this.columnHeaderCell9.TabIndex = 6;
            this.columnHeaderCell9.Value = "更新日";
            this.columnHeaderCell9.ViewSearchItem = true;
            // 
            // columnHeaderCell10
            // 
            this.columnHeaderCell10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell10.Location = new System.Drawing.Point(775, 0);
            this.columnHeaderCell10.Name = "columnHeaderCell10";
            this.columnHeaderCell10.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell10.Size = new System.Drawing.Size(120, 22);
            cellStyle17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border8.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle17.Border = border8;
            cellStyle17.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle17.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell10.Style = cellStyle17;
            this.columnHeaderCell10.TabIndex = 7;
            this.columnHeaderCell10.Value = "作成者";
            this.columnHeaderCell10.ViewSearchItem = true;
            // 
            // columnHeaderCell11
            // 
            this.columnHeaderCell11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell11.Location = new System.Drawing.Point(895, 0);
            this.columnHeaderCell11.Name = "columnHeaderCell11";
            this.columnHeaderCell11.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell11.Size = new System.Drawing.Size(140, 22);
            cellStyle18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border9.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle18.Border = border9;
            cellStyle18.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle18.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell11.Style = cellStyle18;
            this.columnHeaderCell11.TabIndex = 8;
            this.columnHeaderCell11.Value = "作成日";
            this.columnHeaderCell11.ViewSearchItem = true;
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
            this.CREATE_USER.Location = new System.Drawing.Point(775, 0);
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
            cellStyle8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle8.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle8.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle8.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.CREATE_USER.Style = cellStyle8;
            this.CREATE_USER.TabIndex = 9;
            this.CREATE_USER.Tag = "初回作成者が表示されます";
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
            this.UPDATE_USER.Location = new System.Drawing.Point(515, 0);
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
            cellStyle6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle6.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle6.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle6.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.UPDATE_USER.Style = cellStyle6;
            this.UPDATE_USER.TabIndex = 7;
            this.UPDATE_USER.Tag = "最終更新者が表示されます";
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
            this.CREATE_DATE.Location = new System.Drawing.Point(895, 0);
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
            cellStyle9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle9.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle9.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle9.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.CREATE_DATE.Style = cellStyle9;
            this.CREATE_DATE.TabIndex = 10;
            this.CREATE_DATE.Tag = "初回作成日が表示されます";
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
            this.UPDATE_DATE.Location = new System.Drawing.Point(635, 0);
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
            cellStyle7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle7.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle7.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle7.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.UPDATE_DATE.Style = cellStyle7;
            this.UPDATE_DATE.TabIndex = 8;
            this.UPDATE_DATE.Tag = "最終更新日が表示されます";
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
            cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle1.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.DELETE_FLG.Style = cellStyle1;
            this.DELETE_FLG.TabIndex = 0;
            this.DELETE_FLG.Tag = "削除する場合にはチェックを付けてください";
            this.DELETE_FLG.Value = false;
            this.DELETE_FLG.ZeroPaddengFlag = false;
            // 
            // HAIKI_SHURUI_NAME_RYAKU
            // 
            this.HAIKI_SHURUI_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.HAIKI_SHURUI_NAME_RYAKU.DataField = "HAIKI_SHURUI_NAME_RYAKU";
            this.HAIKI_SHURUI_NAME_RYAKU.DBFieldsName = "HAIKI_SHURUI_NAME_RYAKU";
            this.HAIKI_SHURUI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.HAIKI_SHURUI_NAME_RYAKU.DisplayItemName = "廃棄物種類名";
            this.HAIKI_SHURUI_NAME_RYAKU.DisplayPopUp = null;
            this.HAIKI_SHURUI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAIKI_SHURUI_NAME_RYAKU.FocusOutCheckMethod")));
            this.HAIKI_SHURUI_NAME_RYAKU.IsInputErrorOccured = false;
            this.HAIKI_SHURUI_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.HAIKI_SHURUI_NAME_RYAKU.Location = new System.Drawing.Point(135, 0);
            this.HAIKI_SHURUI_NAME_RYAKU.MaxLength = 10;
            this.HAIKI_SHURUI_NAME_RYAKU.Name = "HAIKI_SHURUI_NAME_RYAKU";
            this.HAIKI_SHURUI_NAME_RYAKU.PopupAfterExecute = null;
            this.HAIKI_SHURUI_NAME_RYAKU.PopupBeforeExecute = null;
            this.HAIKI_SHURUI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HAIKI_SHURUI_NAME_RYAKU.PopupSearchSendParams")));
            this.HAIKI_SHURUI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HAIKI_SHURUI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HAIKI_SHURUI_NAME_RYAKU.popupWindowSetting")));
            this.HAIKI_SHURUI_NAME_RYAKU.ReadOnly = true;
            this.HAIKI_SHURUI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAIKI_SHURUI_NAME_RYAKU.RegistCheckMethod")));
            this.HAIKI_SHURUI_NAME_RYAKU.Selectable = false;
            this.HAIKI_SHURUI_NAME_RYAKU.ShortItemName = "廃棄物種類名";
            this.HAIKI_SHURUI_NAME_RYAKU.Size = new System.Drawing.Size(150, 21);
            cellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            cellStyle3.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.HAIKI_SHURUI_NAME_RYAKU.Style = cellStyle3;
            this.HAIKI_SHURUI_NAME_RYAKU.TabIndex = 2;
            this.HAIKI_SHURUI_NAME_RYAKU.TabStop = false;
            this.HAIKI_SHURUI_NAME_RYAKU.Tag = "廃棄物種類名略称が表示されます";
            // 
            // HAIKI_SHURUI_CD
            // 
            this.HAIKI_SHURUI_CD.ChangeUpperCase = true;
            this.HAIKI_SHURUI_CD.CharacterLimitList = null;
            this.HAIKI_SHURUI_CD.CharactersNumber = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.HAIKI_SHURUI_CD.DataField = "HAIKI_SHURUI_CD";
            this.HAIKI_SHURUI_CD.DBFieldsName = "HAIKI_SHURUI_CD";
            this.HAIKI_SHURUI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.HAIKI_SHURUI_CD.DisplayItemName = "廃棄物種類CD";
            this.HAIKI_SHURUI_CD.DisplayPopUp = null;
            this.HAIKI_SHURUI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAIKI_SHURUI_CD.FocusOutCheckMethod")));
            this.HAIKI_SHURUI_CD.GetCodeMasterField = "HAIKI_SHURUI_CD,HAIKI_SHURUI_NAME_RYAKU";
            this.HAIKI_SHURUI_CD.IsInputErrorOccured = false;
            this.HAIKI_SHURUI_CD.ItemDefinedTypes = "varchar";
            this.HAIKI_SHURUI_CD.Location = new System.Drawing.Point(40, 0);
            this.HAIKI_SHURUI_CD.MaxLength = 4;
            this.HAIKI_SHURUI_CD.Name = "HAIKI_SHURUI_CD";
            this.HAIKI_SHURUI_CD.PopupAfterExecute = null;
            this.HAIKI_SHURUI_CD.PopupBeforeExecute = null;
            this.HAIKI_SHURUI_CD.PopupGetMasterField = "HAIKI_SHURUI_CD,HAIKI_SHURUI_NAME_RYAKU";
            this.HAIKI_SHURUI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HAIKI_SHURUI_CD.PopupSearchSendParams")));
            this.HAIKI_SHURUI_CD.PopupSetFormField = "HAIKI_SHURUI_CD,HAIKI_SHURUI_NAME_RYAKU";
            this.HAIKI_SHURUI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_HAIKI_SHURUI;
            this.HAIKI_SHURUI_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.HAIKI_SHURUI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HAIKI_SHURUI_CD.popupWindowSetting")));
            this.HAIKI_SHURUI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAIKI_SHURUI_CD.RegistCheckMethod")));
            this.HAIKI_SHURUI_CD.SetFormField = "HAIKI_SHURUI_CD,HAIKI_SHURUI_NAME_RYAKU";
            this.HAIKI_SHURUI_CD.Size = new System.Drawing.Size(95, 21);
            cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle2.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.HAIKI_SHURUI_CD.Style = cellStyle2;
            this.HAIKI_SHURUI_CD.TabIndex = 1;
            this.HAIKI_SHURUI_CD.Tag = "廃棄物種類を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.HAIKI_SHURUI_CD.ZeroPaddengFlag = true;
            // 
            // KONGOU_HAIKIBUTSU_BIKOU
            // 
            this.KONGOU_HAIKIBUTSU_BIKOU.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.KONGOU_HAIKIBUTSU_BIKOU.DataField = "KONGOU_HAIKIBUTSU_BIKOU";
            this.KONGOU_HAIKIBUTSU_BIKOU.DBFieldsName = "KONGOU_HAIKIBUTSU_BIKOU";
            this.KONGOU_HAIKIBUTSU_BIKOU.DefaultBackColor = System.Drawing.Color.Empty;
            this.KONGOU_HAIKIBUTSU_BIKOU.DisplayItemName = "備考";
            this.KONGOU_HAIKIBUTSU_BIKOU.DisplayPopUp = null;
            this.KONGOU_HAIKIBUTSU_BIKOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KONGOU_HAIKIBUTSU_BIKOU.FocusOutCheckMethod")));
            this.KONGOU_HAIKIBUTSU_BIKOU.IsInputErrorOccured = false;
            this.KONGOU_HAIKIBUTSU_BIKOU.ItemDefinedTypes = "varchar";
            this.KONGOU_HAIKIBUTSU_BIKOU.Location = new System.Drawing.Point(365, 0);
            this.KONGOU_HAIKIBUTSU_BIKOU.MaxLength = 20;
            this.KONGOU_HAIKIBUTSU_BIKOU.Name = "KONGOU_HAIKIBUTSU_BIKOU";
            this.KONGOU_HAIKIBUTSU_BIKOU.PopupAfterExecute = null;
            this.KONGOU_HAIKIBUTSU_BIKOU.PopupBeforeExecute = null;
            this.KONGOU_HAIKIBUTSU_BIKOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KONGOU_HAIKIBUTSU_BIKOU.PopupSearchSendParams")));
            this.KONGOU_HAIKIBUTSU_BIKOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KONGOU_HAIKIBUTSU_BIKOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KONGOU_HAIKIBUTSU_BIKOU.popupWindowSetting")));
            this.KONGOU_HAIKIBUTSU_BIKOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KONGOU_HAIKIBUTSU_BIKOU.RegistCheckMethod")));
            this.KONGOU_HAIKIBUTSU_BIKOU.ShortItemName = "備考";
            this.KONGOU_HAIKIBUTSU_BIKOU.Size = new System.Drawing.Size(150, 21);
            cellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle5.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            cellStyle5.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.KONGOU_HAIKIBUTSU_BIKOU.Style = cellStyle5;
            this.KONGOU_HAIKIBUTSU_BIKOU.TabIndex = 4;
            this.KONGOU_HAIKIBUTSU_BIKOU.Tag = "全角１０文字以内で入力してください";
            // 
            // HAIKI_HIRITSU
            // 
            this.HAIKI_HIRITSU.CustomFormatSetting = "#.##";
            this.HAIKI_HIRITSU.DataField = "HAIKI_HIRITSU";
            this.HAIKI_HIRITSU.DBFieldsName = "HAIKI_HIRITSU";
            this.HAIKI_HIRITSU.DefaultBackColor = System.Drawing.Color.Empty;
            this.HAIKI_HIRITSU.DisplayItemName = "廃棄物比率";
            this.HAIKI_HIRITSU.DisplayPopUp = null;
            this.HAIKI_HIRITSU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAIKI_HIRITSU.FocusOutCheckMethod")));
            this.HAIKI_HIRITSU.FormatSetting = "カスタム";
            this.HAIKI_HIRITSU.IsInputErrorOccured = false;
            this.HAIKI_HIRITSU.ItemDefinedTypes = "decimal";
            this.HAIKI_HIRITSU.Location = new System.Drawing.Point(285, 0);
            this.HAIKI_HIRITSU.Name = "HAIKI_HIRITSU";
            this.HAIKI_HIRITSU.PopupAfterExecute = null;
            this.HAIKI_HIRITSU.PopupBeforeExecute = null;
            this.HAIKI_HIRITSU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HAIKI_HIRITSU.PopupSearchSendParams")));
            this.HAIKI_HIRITSU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HAIKI_HIRITSU.PopupWindowName = "";
            this.HAIKI_HIRITSU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HAIKI_HIRITSU.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.HAIKI_HIRITSU.RangeSetting = rangeSettingDto1;
            this.HAIKI_HIRITSU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAIKI_HIRITSU.RegistCheckMethod")));
            this.HAIKI_HIRITSU.ShortItemName = "廃棄物比率";
            cellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle4.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.HAIKI_HIRITSU.Style = cellStyle4;
            this.HAIKI_HIRITSU.TabIndex = 3;
            this.HAIKI_HIRITSU.Tag = "数字2桁以内で入力してください";
            // 
            // KongouHaikibutsuHoshuDetail
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            cellStyle19.BackColor = System.Drawing.Color.Transparent;
            cellStyle19.BackgroundGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            border10.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Silver);
            cellStyle19.Border = border10;
            cellStyle19.DisabledBackColor = System.Drawing.SystemColors.Control;
            cellStyle19.DisabledForeColor = System.Drawing.SystemColors.GrayText;
            cellStyle19.DisabledGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle19.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
            cellStyle19.Format = "";
            cellStyle19.GradientDirection = GrapeCity.Win.MultiRow.GradientDirection.Center;
            cellStyle19.GradientStyle = GrapeCity.Win.MultiRow.GradientStyle.None;
            cellStyle19.ImageAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            cellStyle19.ImeSentenceMode = GrapeCity.Win.MultiRow.ImeSentenceMode.NoControl;
            cellStyle19.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle19.LineAdjustment = GrapeCity.Win.MultiRow.LineAdjustment.None;
            cellStyle19.Margin = new System.Windows.Forms.Padding(0);
            cellStyle19.MouseOverGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle19.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle19.Padding = new System.Windows.Forms.Padding(0);
            cellStyle19.PatternColor = System.Drawing.SystemColors.WindowText;
            cellStyle19.PatternStyle = GrapeCity.Win.MultiRow.MultiRowHatchStyle.None;
            cellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle19.SelectionGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle19.TextAdjustment = GrapeCity.Win.MultiRow.TextAdjustment.Near;
            cellStyle19.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle19.TextAngle = 0F;
            cellStyle19.TextEffect = GrapeCity.Win.MultiRow.TextEffect.Flat;
            cellStyle19.TextImageRelation = GrapeCity.Win.MultiRow.MultiRowTextImageRelation.Overlay;
            cellStyle19.TextIndent = 0;
            cellStyle19.TextVertical = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle19.UseCompatibleTextRendering = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle19.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            this.DefaultCellStyle = cellStyle19;
            this.Height = 42;
            this.Width = 1035;

        }


        #endregion

        internal GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        internal r_framework.CustomControl.GcCustomTextBoxCell CREATE_USER;
        internal r_framework.CustomControl.GcCustomTextBoxCell UPDATE_USER;
        internal r_framework.CustomControl.GcCustomTextBoxCell CREATE_DATE;
        internal r_framework.CustomControl.GcCustomTextBoxCell UPDATE_DATE;
        internal r_framework.CustomControl.GcCustomCheckBoxCell DELETE_FLG;
        internal r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell3;
        internal r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell2;
        internal r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell4;
        internal r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell5;
        internal r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell8;
        internal r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell9;
        internal r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell10;
        internal r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell11;
        internal r_framework.CustomControl.GcCustomTextBoxCell HAIKI_SHURUI_NAME_RYAKU;
        internal r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell1;
        internal r_framework.CustomControl.GcCustomAlphaNumTextBoxCell HAIKI_SHURUI_CD;
        internal r_framework.CustomControl.GcCustomTextBoxCell KONGOU_HAIKIBUTSU_BIKOU;
        internal r_framework.CustomControl.GcCustomNumericTextBox2Cell HAIKI_HIRITSU;
    }
}
