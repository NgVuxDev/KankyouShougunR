namespace GamenSeigyoHoshu.MultiRowTemplate
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class GamenSeigyoHoshuDetail
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle10 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle11 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle12 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border4 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle13 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border5 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle14 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border6 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle15 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border7 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle16 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border8 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GamenSeigyoHoshuDetail));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle17 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border9 = new GrapeCity.Win.MultiRow.Border();
            this.columnHeaderSection11 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.gcCustomColumnHeader1 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader2 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader3 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader4 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader5 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader6 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader7 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader8 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.SHAIN_NAME = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.UPDATE_USER = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.CREATE_USER = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.UPDATE_DATE = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.CREATE_DATE = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.MAX_WINDOW_COUNT = new r_framework.CustomControl.GcCustomNumericTextBox2Cell();
            this.SHAIN_CD = new r_framework.CustomControl.GcCustomAlphaNumTextBoxCell();
            this.DELETE_FLG = new r_framework.CustomControl.GcCustomCheckBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.DELETE_FLG);
            this.Row.Cells.Add(this.SHAIN_CD);
            this.Row.Cells.Add(this.SHAIN_NAME);
            this.Row.Cells.Add(this.MAX_WINDOW_COUNT);
            this.Row.Cells.Add(this.UPDATE_USER);
            this.Row.Cells.Add(this.UPDATE_DATE);
            this.Row.Cells.Add(this.CREATE_USER);
            this.Row.Cells.Add(this.CREATE_DATE);
            this.Row.Height = 21;
            this.Row.Width = 950;
            // 
            // columnHeaderSection11
            // 
            this.columnHeaderSection11.Cells.Add(this.gcCustomColumnHeader1);
            this.columnHeaderSection11.Cells.Add(this.gcCustomColumnHeader2);
            this.columnHeaderSection11.Cells.Add(this.gcCustomColumnHeader3);
            this.columnHeaderSection11.Cells.Add(this.gcCustomColumnHeader4);
            this.columnHeaderSection11.Cells.Add(this.gcCustomColumnHeader5);
            this.columnHeaderSection11.Cells.Add(this.gcCustomColumnHeader6);
            this.columnHeaderSection11.Cells.Add(this.gcCustomColumnHeader7);
            this.columnHeaderSection11.Cells.Add(this.gcCustomColumnHeader8);
            this.columnHeaderSection11.Height = 21;
            this.columnHeaderSection11.Name = "columnHeaderSection11";
            this.columnHeaderSection11.Width = 950;
            // 
            // gcCustomColumnHeader1
            // 
            this.gcCustomColumnHeader1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader1.Location = new System.Drawing.Point(0, 0);
            this.gcCustomColumnHeader1.Name = "gcCustomColumnHeader1";
            this.gcCustomColumnHeader1.Size = new System.Drawing.Size(40, 22);
            cellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle9.Border = border1;
            cellStyle9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle9.ForeColor = System.Drawing.Color.White;
            cellStyle9.ImageAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle9.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.gcCustomColumnHeader1.Style = cellStyle9;
            this.gcCustomColumnHeader1.TabIndex = 0;
            this.gcCustomColumnHeader1.Value = "削除";
            this.gcCustomColumnHeader1.ViewSearchItem = false;
            // 
            // gcCustomColumnHeader2
            // 
            this.gcCustomColumnHeader2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader2.Location = new System.Drawing.Point(40, 0);
            this.gcCustomColumnHeader2.Name = "gcCustomColumnHeader2";
            this.gcCustomColumnHeader2.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.gcCustomColumnHeader2.Size = new System.Drawing.Size(120, 22);
            cellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle10.Border = border2;
            cellStyle10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle10.ForeColor = System.Drawing.Color.White;
            cellStyle10.ImageAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle10.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.gcCustomColumnHeader2.Style = cellStyle10;
            this.gcCustomColumnHeader2.TabIndex = 1;
            this.gcCustomColumnHeader2.Value = "社員CD※";
            this.gcCustomColumnHeader2.ViewSearchItem = true;
            // 
            // gcCustomColumnHeader3
            // 
            this.gcCustomColumnHeader3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader3.Location = new System.Drawing.Point(160, 0);
            this.gcCustomColumnHeader3.Name = "gcCustomColumnHeader3";
            this.gcCustomColumnHeader3.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.gcCustomColumnHeader3.Size = new System.Drawing.Size(120, 22);
            cellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle11.Border = border3;
            cellStyle11.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle11.ForeColor = System.Drawing.Color.White;
            cellStyle11.ImageAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle11.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.gcCustomColumnHeader3.Style = cellStyle11;
            this.gcCustomColumnHeader3.TabIndex = 2;
            this.gcCustomColumnHeader3.Value = "社員名";
            this.gcCustomColumnHeader3.ViewSearchItem = true;
            // 
            // gcCustomColumnHeader4
            // 
            this.gcCustomColumnHeader4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader4.Location = new System.Drawing.Point(280, 0);
            this.gcCustomColumnHeader4.Name = "gcCustomColumnHeader4";
            this.gcCustomColumnHeader4.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.gcCustomColumnHeader4.Size = new System.Drawing.Size(150, 22);
            cellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle12.Border = border4;
            cellStyle12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle12.ForeColor = System.Drawing.Color.White;
            cellStyle12.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.gcCustomColumnHeader4.Style = cellStyle12;
            this.gcCustomColumnHeader4.TabIndex = 3;
            this.gcCustomColumnHeader4.Value = "最大画面数※";
            this.gcCustomColumnHeader4.ViewSearchItem = true;
            // 
            // gcCustomColumnHeader5
            // 
            this.gcCustomColumnHeader5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader5.Location = new System.Drawing.Point(430, 0);
            this.gcCustomColumnHeader5.Name = "gcCustomColumnHeader5";
            this.gcCustomColumnHeader5.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.gcCustomColumnHeader5.Size = new System.Drawing.Size(120, 22);
            cellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border5.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle13.Border = border5;
            cellStyle13.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle13.ForeColor = System.Drawing.Color.White;
            cellStyle13.ImageAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle13.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.gcCustomColumnHeader5.Style = cellStyle13;
            this.gcCustomColumnHeader5.TabIndex = 4;
            this.gcCustomColumnHeader5.Value = "更新者";
            this.gcCustomColumnHeader5.ViewSearchItem = true;
            // 
            // gcCustomColumnHeader6
            // 
            this.gcCustomColumnHeader6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader6.Location = new System.Drawing.Point(550, 0);
            this.gcCustomColumnHeader6.Name = "gcCustomColumnHeader6";
            this.gcCustomColumnHeader6.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.gcCustomColumnHeader6.Size = new System.Drawing.Size(140, 22);
            cellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border6.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle14.Border = border6;
            cellStyle14.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle14.ForeColor = System.Drawing.Color.White;
            cellStyle14.ImageAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle14.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.gcCustomColumnHeader6.Style = cellStyle14;
            this.gcCustomColumnHeader6.TabIndex = 5;
            this.gcCustomColumnHeader6.Value = "更新日";
            this.gcCustomColumnHeader6.ViewSearchItem = true;
            // 
            // gcCustomColumnHeader7
            // 
            this.gcCustomColumnHeader7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader7.Location = new System.Drawing.Point(690, 0);
            this.gcCustomColumnHeader7.Name = "gcCustomColumnHeader7";
            this.gcCustomColumnHeader7.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.gcCustomColumnHeader7.Size = new System.Drawing.Size(120, 22);
            cellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border7.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle15.Border = border7;
            cellStyle15.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle15.ForeColor = System.Drawing.Color.White;
            cellStyle15.ImageAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle15.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.gcCustomColumnHeader7.Style = cellStyle15;
            this.gcCustomColumnHeader7.TabIndex = 6;
            this.gcCustomColumnHeader7.Value = "作成者";
            this.gcCustomColumnHeader7.ViewSearchItem = true;
            // 
            // gcCustomColumnHeader8
            // 
            this.gcCustomColumnHeader8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader8.Location = new System.Drawing.Point(810, 0);
            this.gcCustomColumnHeader8.Name = "gcCustomColumnHeader8";
            this.gcCustomColumnHeader8.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.gcCustomColumnHeader8.Size = new System.Drawing.Size(140, 22);
            cellStyle16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border8.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle16.Border = border8;
            cellStyle16.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle16.ForeColor = System.Drawing.Color.White;
            cellStyle16.ImageAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle16.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.gcCustomColumnHeader8.Style = cellStyle16;
            this.gcCustomColumnHeader8.TabIndex = 7;
            this.gcCustomColumnHeader8.Value = "作成日";
            this.gcCustomColumnHeader8.ViewSearchItem = true;
            // 
            // SHAIN_NAME
            // 
            this.SHAIN_NAME.DataField = "SHAIN_NAME";
            this.SHAIN_NAME.DBFieldsName = "SHAIN_NAME";
            this.SHAIN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHAIN_NAME.DisplayItemName = "社員名";
            this.SHAIN_NAME.DisplayPopUp = null;
            this.SHAIN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHAIN_NAME.FocusOutCheckMethod")));
            this.SHAIN_NAME.IsInputErrorOccured = false;
            this.SHAIN_NAME.ItemDefinedTypes = "varchar";
            this.SHAIN_NAME.Location = new System.Drawing.Point(160, 0);
            this.SHAIN_NAME.MaxLength = 0;
            this.SHAIN_NAME.Name = "SHAIN_NAME";
            this.SHAIN_NAME.PopupAfterExecute = null;
            this.SHAIN_NAME.PopupBeforeExecute = null;
            this.SHAIN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHAIN_NAME.PopupSearchSendParams")));
            this.SHAIN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHAIN_NAME.PopupWindowName = "";
            this.SHAIN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHAIN_NAME.popupWindowSetting")));
            this.SHAIN_NAME.ReadOnly = true;
            this.SHAIN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHAIN_NAME.RegistCheckMethod")));
            this.SHAIN_NAME.Selectable = false;
            this.SHAIN_NAME.ShortItemName = "社員名";
            this.SHAIN_NAME.Size = new System.Drawing.Size(120, 21);
            cellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            cellStyle3.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.SHAIN_NAME.Style = cellStyle3;
            this.SHAIN_NAME.TabIndex = 2;
            this.SHAIN_NAME.Tag = " ";
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
            this.UPDATE_USER.Location = new System.Drawing.Point(430, 0);
            this.UPDATE_USER.MaxLength = 0;
            this.UPDATE_USER.Name = "UPDATE_USER";
            this.UPDATE_USER.PopupAfterExecute = null;
            this.UPDATE_USER.PopupBeforeExecute = null;
            this.UPDATE_USER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPDATE_USER.PopupSearchSendParams")));
            this.UPDATE_USER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPDATE_USER.PopupWindowName = "";
            this.UPDATE_USER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPDATE_USER.popupWindowSetting")));
            this.UPDATE_USER.ReadOnly = true;
            this.UPDATE_USER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_USER.RegistCheckMethod")));
            this.UPDATE_USER.Selectable = false;
            this.UPDATE_USER.ShortItemName = "更新者";
            this.UPDATE_USER.Size = new System.Drawing.Size(120, 21);
            cellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle5.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle5.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle5.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.UPDATE_USER.Style = cellStyle5;
            this.UPDATE_USER.TabIndex = 4;
            this.UPDATE_USER.Tag = "最終更新者が表示されます";
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
            this.CREATE_USER.Location = new System.Drawing.Point(690, 0);
            this.CREATE_USER.MaxLength = 0;
            this.CREATE_USER.Name = "CREATE_USER";
            this.CREATE_USER.PopupAfterExecute = null;
            this.CREATE_USER.PopupBeforeExecute = null;
            this.CREATE_USER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CREATE_USER.PopupSearchSendParams")));
            this.CREATE_USER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_USER.PopupWindowName = "";
            this.CREATE_USER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_USER.popupWindowSetting")));
            this.CREATE_USER.ReadOnly = true;
            this.CREATE_USER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_USER.RegistCheckMethod")));
            this.CREATE_USER.Selectable = false;
            this.CREATE_USER.ShortItemName = "作成者";
            this.CREATE_USER.Size = new System.Drawing.Size(120, 21);
            cellStyle7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle7.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle7.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle7.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.CREATE_USER.Style = cellStyle7;
            this.CREATE_USER.TabIndex = 6;
            this.CREATE_USER.Tag = "初回作成者が表示されます";
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
            this.UPDATE_DATE.Location = new System.Drawing.Point(550, 0);
            this.UPDATE_DATE.MaxLength = 0;
            this.UPDATE_DATE.Name = "UPDATE_DATE";
            this.UPDATE_DATE.PopupAfterExecute = null;
            this.UPDATE_DATE.PopupBeforeExecute = null;
            this.UPDATE_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPDATE_DATE.PopupSearchSendParams")));
            this.UPDATE_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPDATE_DATE.PopupWindowName = "";
            this.UPDATE_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPDATE_DATE.popupWindowSetting")));
            this.UPDATE_DATE.ReadOnly = true;
            this.UPDATE_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_DATE.RegistCheckMethod")));
            this.UPDATE_DATE.Selectable = false;
            this.UPDATE_DATE.ShortItemName = "更新日";
            this.UPDATE_DATE.Size = new System.Drawing.Size(140, 21);
            cellStyle6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle6.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle6.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle6.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.UPDATE_DATE.Style = cellStyle6;
            this.UPDATE_DATE.TabIndex = 5;
            this.UPDATE_DATE.Tag = "最終更新日が表示されます";
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
            this.CREATE_DATE.Location = new System.Drawing.Point(810, 0);
            this.CREATE_DATE.MaxLength = 0;
            this.CREATE_DATE.Name = "CREATE_DATE";
            this.CREATE_DATE.PopupAfterExecute = null;
            this.CREATE_DATE.PopupBeforeExecute = null;
            this.CREATE_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CREATE_DATE.PopupSearchSendParams")));
            this.CREATE_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_DATE.PopupWindowName = "";
            this.CREATE_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_DATE.popupWindowSetting")));
            this.CREATE_DATE.ReadOnly = true;
            this.CREATE_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_DATE.RegistCheckMethod")));
            this.CREATE_DATE.Selectable = false;
            this.CREATE_DATE.ShortItemName = "作成日";
            this.CREATE_DATE.Size = new System.Drawing.Size(140, 21);
            cellStyle8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle8.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle8.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle8.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.CREATE_DATE.Style = cellStyle8;
            this.CREATE_DATE.TabIndex = 7;
            this.CREATE_DATE.Tag = "初回作成日が表示されます";
            // 
            // MAX_WINDOW_COUNT
            // 
            this.MAX_WINDOW_COUNT.DataField = "MAX_WINDOW_COUNT";
            this.MAX_WINDOW_COUNT.DBFieldsName = "MAX_WINDOW_COUNT";
            this.MAX_WINDOW_COUNT.DefaultBackColor = System.Drawing.Color.Empty;
            this.MAX_WINDOW_COUNT.DisplayItemName = "最大表示画面数";
            this.MAX_WINDOW_COUNT.DisplayPopUp = null;
            this.MAX_WINDOW_COUNT.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MAX_WINDOW_COUNT.FocusOutCheckMethod")));
            this.MAX_WINDOW_COUNT.IsInputErrorOccured = false;
            this.MAX_WINDOW_COUNT.ItemDefinedTypes = "smallint";
            this.MAX_WINDOW_COUNT.Location = new System.Drawing.Point(280, 0);
            this.MAX_WINDOW_COUNT.Name = "MAX_WINDOW_COUNT";
            this.MAX_WINDOW_COUNT.PopupAfterExecute = null;
            this.MAX_WINDOW_COUNT.PopupBeforeExecute = null;
            this.MAX_WINDOW_COUNT.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MAX_WINDOW_COUNT.PopupSearchSendParams")));
            this.MAX_WINDOW_COUNT.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MAX_WINDOW_COUNT.PopupWindowName = "";
            this.MAX_WINDOW_COUNT.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("MAX_WINDOW_COUNT.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            10,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.MAX_WINDOW_COUNT.RangeSetting = rangeSettingDto1;
            this.MAX_WINDOW_COUNT.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MAX_WINDOW_COUNT.RegistCheckMethod")));
            this.MAX_WINDOW_COUNT.ShortItemName = "最大表示画面数";
            this.MAX_WINDOW_COUNT.Size = new System.Drawing.Size(150, 21);
            cellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            cellStyle4.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.MAX_WINDOW_COUNT.Style = cellStyle4;
            this.MAX_WINDOW_COUNT.TabIndex = 3;
            this.MAX_WINDOW_COUNT.Tag = "【3～10】のいずれかで入力してください。";
            // 
            // SHAIN_CD
            // 
            this.SHAIN_CD.ChangeUpperCase = true;
            this.SHAIN_CD.CharacterLimitList = null;
            this.SHAIN_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.SHAIN_CD.DataField = "SHAIN_CD";
            this.SHAIN_CD.DBFieldsName = "SHAIN_CD";
            this.SHAIN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHAIN_CD.DisplayItemName = "社員CD";
            this.SHAIN_CD.DisplayPopUp = null;
            this.SHAIN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHAIN_CD.FocusOutCheckMethod")));
            this.SHAIN_CD.GetCodeMasterField = "SHAIN_CD,SHAIN_NAME_RYAKU";
            this.SHAIN_CD.IsInputErrorOccured = false;
            this.SHAIN_CD.ItemDefinedTypes = "varchar";
            this.SHAIN_CD.Location = new System.Drawing.Point(40, 0);
            this.SHAIN_CD.MaxLength = 6;
            this.SHAIN_CD.Name = "SHAIN_CD";
            this.SHAIN_CD.PopupAfterExecute = null;
            this.SHAIN_CD.PopupBeforeExecute = null;
            this.SHAIN_CD.PopupGetMasterField = "SHAIN_CD,SHAIN_NAME_RYAKU";
            this.SHAIN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHAIN_CD.PopupSearchSendParams")));
            this.SHAIN_CD.PopupSetFormField = "SHAIN_CD,SHAIN_NAME";
            this.SHAIN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHAIN;
            this.SHAIN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.SHAIN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHAIN_CD.popupWindowSetting")));
            this.SHAIN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHAIN_CD.RegistCheckMethod")));
            this.SHAIN_CD.SetFormField = "SHAIN_CD,SHAIN_NAME";
            this.SHAIN_CD.ShortItemName = "社員CD";
            this.SHAIN_CD.Size = new System.Drawing.Size(120, 21);
            cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle2.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.SHAIN_CD.Style = cellStyle2;
            this.SHAIN_CD.TabIndex = 1;
            this.SHAIN_CD.Tag = "社員CDは6文字以内で入力してください";
            this.SHAIN_CD.ZeroPaddengFlag = true;
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
            // GamenSeigyoHoshuDetail
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection11});
            cellStyle17.BackColor = System.Drawing.Color.Transparent;
            cellStyle17.BackgroundGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            border9.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Silver);
            cellStyle17.Border = border9;
            cellStyle17.DisabledBackColor = System.Drawing.SystemColors.Control;
            cellStyle17.DisabledForeColor = System.Drawing.SystemColors.GrayText;
            cellStyle17.DisabledGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle17.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle17.ForeColor = System.Drawing.SystemColors.WindowText;
            cellStyle17.Format = "";
            cellStyle17.GradientDirection = GrapeCity.Win.MultiRow.GradientDirection.Center;
            cellStyle17.GradientStyle = GrapeCity.Win.MultiRow.GradientStyle.None;
            cellStyle17.ImageAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            cellStyle17.ImeSentenceMode = GrapeCity.Win.MultiRow.ImeSentenceMode.NoControl;
            cellStyle17.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle17.LineAdjustment = GrapeCity.Win.MultiRow.LineAdjustment.None;
            cellStyle17.Margin = new System.Windows.Forms.Padding(0);
            cellStyle17.MouseOverGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle17.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle17.Padding = new System.Windows.Forms.Padding(0);
            cellStyle17.PatternColor = System.Drawing.SystemColors.WindowText;
            cellStyle17.PatternStyle = GrapeCity.Win.MultiRow.MultiRowHatchStyle.None;
            cellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle17.SelectionGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle17.TextAdjustment = GrapeCity.Win.MultiRow.TextAdjustment.Near;
            cellStyle17.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle17.TextAngle = 0F;
            cellStyle17.TextEffect = GrapeCity.Win.MultiRow.TextEffect.Flat;
            cellStyle17.TextImageRelation = GrapeCity.Win.MultiRow.MultiRowTextImageRelation.Overlay;
            cellStyle17.TextIndent = 0;
            cellStyle17.TextVertical = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle17.UseCompatibleTextRendering = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle17.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            this.DefaultCellStyle = cellStyle17;
            this.Height = 42;
            this.Width = 950;

        }


        #endregion

        public GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection11;
        internal r_framework.CustomControl.GcCustomTextBoxCell SHAIN_NAME;
        internal r_framework.CustomControl.GcCustomTextBoxCell UPDATE_USER;
        internal r_framework.CustomControl.GcCustomTextBoxCell CREATE_USER;
        internal r_framework.CustomControl.GcCustomTextBoxCell UPDATE_DATE;
        internal r_framework.CustomControl.GcCustomTextBoxCell CREATE_DATE;
        internal r_framework.CustomControl.GcCustomNumericTextBox2Cell MAX_WINDOW_COUNT;
        internal r_framework.CustomControl.GcCustomAlphaNumTextBoxCell SHAIN_CD;
        internal r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader1;
        internal r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader2;
        internal r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader3;
        internal r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader4;
        internal r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader5;
        internal r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader6;
        internal r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader7;
        internal r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader8;
        internal r_framework.CustomControl.GcCustomCheckBoxCell DELETE_FLG;

    }
}
