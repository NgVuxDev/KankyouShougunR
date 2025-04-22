namespace GenbaHoshu.MultiRowTemplate
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class GenbaHoshuDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenbaHoshuDetail));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.columnHeaderCell1 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell4 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell7 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell9 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.gcCustomColumnHeader5 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.ITAKU_KEIYAKU_NO = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.ITAKU_KEIYAKU_SHURUI = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.YUUKOU_BEGIN = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.YUUKOU_END = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.ITAKU_KEIYAKU_STATUS = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.label1 = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.ITAKU_SYSTEM_ID = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.ITAKU_KEIYAKU_TOUROKU_HOUHOU = new r_framework.CustomControl.GcCustomTextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.ITAKU_KEIYAKU_NO);
            this.Row.Cells.Add(this.ITAKU_KEIYAKU_SHURUI);
            this.Row.Cells.Add(this.YUUKOU_BEGIN);
            this.Row.Cells.Add(this.YUUKOU_END);
            this.Row.Cells.Add(this.ITAKU_KEIYAKU_STATUS);
            this.Row.Cells.Add(this.label1);
            this.Row.Cells.Add(this.ITAKU_SYSTEM_ID);
            this.Row.Cells.Add(this.ITAKU_KEIYAKU_TOUROKU_HOUHOU);
            this.Row.Height = 21;
            this.Row.Width = 550;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell1);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell4);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell7);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell9);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader5);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 550;
            // 
            // columnHeaderCell1
            // 
            this.columnHeaderCell1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell1.Location = new System.Drawing.Point(0, 0);
            this.columnHeaderCell1.Name = "columnHeaderCell1";
            this.columnHeaderCell1.Size = new System.Drawing.Size(95, 21);
            cellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle8.Border = border1;
            cellStyle8.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle8.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell1.Style = cellStyle8;
            this.columnHeaderCell1.TabIndex = 0;
            this.columnHeaderCell1.Value = "委託契約番号";
            // 
            // columnHeaderCell4
            // 
            this.columnHeaderCell4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell4.Location = new System.Drawing.Point(270, 0);
            this.columnHeaderCell4.Name = "columnHeaderCell4";
            this.columnHeaderCell4.Size = new System.Drawing.Size(200, 21);
            cellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle9.Border = border2;
            cellStyle9.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle9.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell4.Style = cellStyle9;
            this.columnHeaderCell4.TabIndex = 3;
            this.columnHeaderCell4.Value = "有効期間";
            // 
            // columnHeaderCell7
            // 
            this.columnHeaderCell7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell7.Location = new System.Drawing.Point(95, 0);
            this.columnHeaderCell7.Name = "columnHeaderCell7";
            this.columnHeaderCell7.Size = new System.Drawing.Size(175, 21);
            cellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle10.Border = border3;
            cellStyle10.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle10.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell7.Style = cellStyle10;
            this.columnHeaderCell7.TabIndex = 7;
            this.columnHeaderCell7.Value = "契約書種類";
            // 
            // columnHeaderCell9
            // 
            this.columnHeaderCell9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell9.Location = new System.Drawing.Point(470, 0);
            this.columnHeaderCell9.Name = "columnHeaderCell9";
            this.columnHeaderCell9.Size = new System.Drawing.Size(80, 21);
            cellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle11.Border = border4;
            cellStyle11.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle11.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell9.Style = cellStyle11;
            this.columnHeaderCell9.TabIndex = 9;
            this.columnHeaderCell9.Value = "ステータス";
            // 
            // gcCustomColumnHeader5
            // 
            this.gcCustomColumnHeader5.FilterCellIndex = 4;
            this.gcCustomColumnHeader5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader5.Location = new System.Drawing.Point(630, 0);
            this.gcCustomColumnHeader5.Name = "gcCustomColumnHeader5";
            this.gcCustomColumnHeader5.Size = new System.Drawing.Size(124, 21);
            cellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border5.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle12.Border = border5;
            cellStyle12.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle12.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader5.Style = cellStyle12;
            this.gcCustomColumnHeader5.TabIndex = 10;
            this.gcCustomColumnHeader5.Value = "システムID(隠)";
            this.gcCustomColumnHeader5.ViewSearchItem = true;
            this.gcCustomColumnHeader5.Visible = false;
            // 
            // ITAKU_KEIYAKU_NO
            // 
            this.ITAKU_KEIYAKU_NO.DataField = "ITAKU_KEIYAKU_NO";
            this.ITAKU_KEIYAKU_NO.DBFieldsName = "ITAKU_KEIYAKU_NO";
            this.ITAKU_KEIYAKU_NO.DefaultBackColor = System.Drawing.Color.Empty;
            this.ITAKU_KEIYAKU_NO.DisplayItemName = "委託契約番号";
            this.ITAKU_KEIYAKU_NO.DisplayPopUp = null;
            this.ITAKU_KEIYAKU_NO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ITAKU_KEIYAKU_NO.FocusOutCheckMethod")));
            this.ITAKU_KEIYAKU_NO.IsInputErrorOccured = false;
            this.ITAKU_KEIYAKU_NO.ItemDefinedTypes = "varchar";
            this.ITAKU_KEIYAKU_NO.Location = new System.Drawing.Point(0, 0);
            this.ITAKU_KEIYAKU_NO.Name = "ITAKU_KEIYAKU_NO";
            this.ITAKU_KEIYAKU_NO.PopupAfterExecute = null;
            this.ITAKU_KEIYAKU_NO.PopupBeforeExecute = null;
            this.ITAKU_KEIYAKU_NO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ITAKU_KEIYAKU_NO.PopupSearchSendParams")));
            this.ITAKU_KEIYAKU_NO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ITAKU_KEIYAKU_NO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ITAKU_KEIYAKU_NO.popupWindowSetting")));
            this.ITAKU_KEIYAKU_NO.ReadOnly = true;
            this.ITAKU_KEIYAKU_NO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ITAKU_KEIYAKU_NO.RegistCheckMethod")));
            this.ITAKU_KEIYAKU_NO.ShortItemName = "委託契約番号";
            this.ITAKU_KEIYAKU_NO.Size = new System.Drawing.Size(95, 21);
            cellStyle1.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle1.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.ITAKU_KEIYAKU_NO.Style = cellStyle1;
            this.ITAKU_KEIYAKU_NO.TabIndex = 8;
            this.ITAKU_KEIYAKU_NO.TabStop = false;
            this.ITAKU_KEIYAKU_NO.Tag = "委託契約番号が表示されます";
            // 
            // ITAKU_KEIYAKU_SHURUI
            // 
            this.ITAKU_KEIYAKU_SHURUI.DataField = "ITAKU_KEIYAKU_SHURUI";
            this.ITAKU_KEIYAKU_SHURUI.DBFieldsName = "ITAKU_KEIYAKU_SHURUI";
            this.ITAKU_KEIYAKU_SHURUI.DefaultBackColor = System.Drawing.Color.Empty;
            this.ITAKU_KEIYAKU_SHURUI.DisplayItemName = "委託契約書種類";
            this.ITAKU_KEIYAKU_SHURUI.DisplayPopUp = null;
            this.ITAKU_KEIYAKU_SHURUI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ITAKU_KEIYAKU_SHURUI.FocusOutCheckMethod")));
            this.ITAKU_KEIYAKU_SHURUI.IsInputErrorOccured = false;
            this.ITAKU_KEIYAKU_SHURUI.ItemDefinedTypes = "varchar";
            this.ITAKU_KEIYAKU_SHURUI.Location = new System.Drawing.Point(95, 0);
            this.ITAKU_KEIYAKU_SHURUI.Name = "ITAKU_KEIYAKU_SHURUI";
            this.ITAKU_KEIYAKU_SHURUI.PopupAfterExecute = null;
            this.ITAKU_KEIYAKU_SHURUI.PopupBeforeExecute = null;
            this.ITAKU_KEIYAKU_SHURUI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ITAKU_KEIYAKU_SHURUI.PopupSearchSendParams")));
            this.ITAKU_KEIYAKU_SHURUI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ITAKU_KEIYAKU_SHURUI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ITAKU_KEIYAKU_SHURUI.popupWindowSetting")));
            this.ITAKU_KEIYAKU_SHURUI.ReadOnly = true;
            this.ITAKU_KEIYAKU_SHURUI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ITAKU_KEIYAKU_SHURUI.RegistCheckMethod")));
            this.ITAKU_KEIYAKU_SHURUI.ShortItemName = "委託契約書種類";
            this.ITAKU_KEIYAKU_SHURUI.Size = new System.Drawing.Size(175, 21);
            cellStyle2.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle2.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.ITAKU_KEIYAKU_SHURUI.Style = cellStyle2;
            this.ITAKU_KEIYAKU_SHURUI.TabIndex = 9;
            this.ITAKU_KEIYAKU_SHURUI.TabStop = false;
            this.ITAKU_KEIYAKU_SHURUI.Tag = "契約書種類が表示されます";
            // 
            // YUUKOU_BEGIN
            // 
            this.YUUKOU_BEGIN.DataField = "YUUKOU_BEGIN";
            this.YUUKOU_BEGIN.DBFieldsName = "YUUKOU_BEGIN";
            this.YUUKOU_BEGIN.DefaultBackColor = System.Drawing.Color.Empty;
            this.YUUKOU_BEGIN.DisplayItemName = "有効期限開始";
            this.YUUKOU_BEGIN.DisplayPopUp = null;
            this.YUUKOU_BEGIN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("YUUKOU_BEGIN.FocusOutCheckMethod")));
            this.YUUKOU_BEGIN.IsInputErrorOccured = false;
            this.YUUKOU_BEGIN.ItemDefinedTypes = "datetime";
            this.YUUKOU_BEGIN.Location = new System.Drawing.Point(270, 0);
            this.YUUKOU_BEGIN.Name = "YUUKOU_BEGIN";
            this.YUUKOU_BEGIN.PopupAfterExecute = null;
            this.YUUKOU_BEGIN.PopupBeforeExecute = null;
            this.YUUKOU_BEGIN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("YUUKOU_BEGIN.PopupSearchSendParams")));
            this.YUUKOU_BEGIN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.YUUKOU_BEGIN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("YUUKOU_BEGIN.popupWindowSetting")));
            this.YUUKOU_BEGIN.ReadOnly = true;
            this.YUUKOU_BEGIN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("YUUKOU_BEGIN.RegistCheckMethod")));
            this.YUUKOU_BEGIN.ShortItemName = "有効期限開始";
            this.YUUKOU_BEGIN.Size = new System.Drawing.Size(79, 21);
            cellStyle3.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle3.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.YUUKOU_BEGIN.Style = cellStyle3;
            this.YUUKOU_BEGIN.TabIndex = 10;
            this.YUUKOU_BEGIN.TabStop = false;
            this.YUUKOU_BEGIN.Tag = "有効期間開始が表示されます";
            // 
            // YUUKOU_END
            // 
            this.YUUKOU_END.DataField = "YUUKOU_END";
            this.YUUKOU_END.DBFieldsName = "YUUKOU_END";
            this.YUUKOU_END.DefaultBackColor = System.Drawing.Color.Empty;
            this.YUUKOU_END.DisplayItemName = "有効期限終了";
            this.YUUKOU_END.DisplayPopUp = null;
            this.YUUKOU_END.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("YUUKOU_END.FocusOutCheckMethod")));
            this.YUUKOU_END.IsInputErrorOccured = false;
            this.YUUKOU_END.ItemDefinedTypes = "datetime";
            this.YUUKOU_END.Location = new System.Drawing.Point(389, 0);
            this.YUUKOU_END.Name = "YUUKOU_END";
            this.YUUKOU_END.PopupAfterExecute = null;
            this.YUUKOU_END.PopupBeforeExecute = null;
            this.YUUKOU_END.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("YUUKOU_END.PopupSearchSendParams")));
            this.YUUKOU_END.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.YUUKOU_END.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("YUUKOU_END.popupWindowSetting")));
            this.YUUKOU_END.ReadOnly = true;
            this.YUUKOU_END.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("YUUKOU_END.RegistCheckMethod")));
            this.YUUKOU_END.ShortItemName = "有効期限終了";
            this.YUUKOU_END.Size = new System.Drawing.Size(81, 21);
            cellStyle4.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle4.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.YUUKOU_END.Style = cellStyle4;
            this.YUUKOU_END.TabIndex = 11;
            this.YUUKOU_END.TabStop = false;
            this.YUUKOU_END.Tag = "有効期間終了が表示されます";
            // 
            // ITAKU_KEIYAKU_STATUS
            // 
            this.ITAKU_KEIYAKU_STATUS.DataField = "ITAKU_KEIYAKU_STATUS";
            this.ITAKU_KEIYAKU_STATUS.DBFieldsName = "ITAKU_KEIYAKU_STATUS";
            this.ITAKU_KEIYAKU_STATUS.DefaultBackColor = System.Drawing.Color.Empty;
            this.ITAKU_KEIYAKU_STATUS.DisplayItemName = "委託契約ステータス";
            this.ITAKU_KEIYAKU_STATUS.DisplayPopUp = null;
            this.ITAKU_KEIYAKU_STATUS.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ITAKU_KEIYAKU_STATUS.FocusOutCheckMethod")));
            this.ITAKU_KEIYAKU_STATUS.IsInputErrorOccured = false;
            this.ITAKU_KEIYAKU_STATUS.ItemDefinedTypes = "varchar";
            this.ITAKU_KEIYAKU_STATUS.Location = new System.Drawing.Point(470, 0);
            this.ITAKU_KEIYAKU_STATUS.Name = "ITAKU_KEIYAKU_STATUS";
            this.ITAKU_KEIYAKU_STATUS.PopupAfterExecute = null;
            this.ITAKU_KEIYAKU_STATUS.PopupBeforeExecute = null;
            this.ITAKU_KEIYAKU_STATUS.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ITAKU_KEIYAKU_STATUS.PopupSearchSendParams")));
            this.ITAKU_KEIYAKU_STATUS.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ITAKU_KEIYAKU_STATUS.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ITAKU_KEIYAKU_STATUS.popupWindowSetting")));
            this.ITAKU_KEIYAKU_STATUS.ReadOnly = true;
            this.ITAKU_KEIYAKU_STATUS.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ITAKU_KEIYAKU_STATUS.RegistCheckMethod")));
            this.ITAKU_KEIYAKU_STATUS.ShortItemName = "委託契約ステータス";
            cellStyle5.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            cellStyle5.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle5.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle5.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.ITAKU_KEIYAKU_STATUS.Style = cellStyle5;
            this.ITAKU_KEIYAKU_STATUS.TabIndex = 12;
            this.ITAKU_KEIYAKU_STATUS.TabStop = false;
            this.ITAKU_KEIYAKU_STATUS.Tag = "ステータスが表示されます";
            // 
            // label1
            // 
            this.label1.DBFieldsName = "";
            this.label1.DefaultBackColor = System.Drawing.Color.Empty;
            this.label1.DisplayItemName = "";
            this.label1.DisplayPopUp = null;
            this.label1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("label1.FocusOutCheckMethod")));
            this.label1.IsInputErrorOccured = false;
            this.label1.Location = new System.Drawing.Point(349, 0);
            this.label1.Name = "label1";
            this.label1.PopupAfterExecute = null;
            this.label1.PopupBeforeExecute = null;
            this.label1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("label1.PopupSearchSendParams")));
            this.label1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.label1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("label1.popupWindowSetting")));
            this.label1.ReadOnly = true;
            this.label1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("label1.RegistCheckMethod")));
            this.label1.Size = new System.Drawing.Size(40, 21);
            cellStyle6.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            cellStyle6.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle6.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle6.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.label1.Style = cellStyle6;
            this.label1.TabIndex = 13;
            this.label1.TabStop = false;
            this.label1.Tag = "";
            this.label1.Value = "～";
            // 
            // ITAKU_SYSTEM_ID
            // 
            this.ITAKU_SYSTEM_ID.DataField = "SYSTEM_ID";
            this.ITAKU_SYSTEM_ID.DBFieldsName = "SYSTEM_ID";
            this.ITAKU_SYSTEM_ID.DefaultBackColor = System.Drawing.Color.Empty;
            this.ITAKU_SYSTEM_ID.DisplayItemName = "システムID";
            this.ITAKU_SYSTEM_ID.DisplayPopUp = null;
            this.ITAKU_SYSTEM_ID.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ITAKU_SYSTEM_ID.FocusOutCheckMethod")));
            this.ITAKU_SYSTEM_ID.IsInputErrorOccured = false;
            this.ITAKU_SYSTEM_ID.ItemDefinedTypes = "varchar";
            this.ITAKU_SYSTEM_ID.Location = new System.Drawing.Point(550, 0);
            this.ITAKU_SYSTEM_ID.Name = "ITAKU_SYSTEM_ID";
            this.ITAKU_SYSTEM_ID.PopupAfterExecute = null;
            this.ITAKU_SYSTEM_ID.PopupBeforeExecute = null;
            this.ITAKU_SYSTEM_ID.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ITAKU_SYSTEM_ID.PopupSearchSendParams")));
            this.ITAKU_SYSTEM_ID.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ITAKU_SYSTEM_ID.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ITAKU_SYSTEM_ID.popupWindowSetting")));
            this.ITAKU_SYSTEM_ID.ReadOnly = true;
            this.ITAKU_SYSTEM_ID.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ITAKU_SYSTEM_ID.RegistCheckMethod")));
            this.ITAKU_SYSTEM_ID.Selectable = false;
            this.ITAKU_SYSTEM_ID.ShortItemName = "システムID";
            this.ITAKU_SYSTEM_ID.Size = new System.Drawing.Size(123, 21);
            cellStyle7.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            cellStyle7.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle7.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle7.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.ITAKU_SYSTEM_ID.Style = cellStyle7;
            this.ITAKU_SYSTEM_ID.TabIndex = 14;
            this.ITAKU_SYSTEM_ID.TabStop = false;
            this.ITAKU_SYSTEM_ID.Tag = "システムIDが表示されます";
            this.ITAKU_SYSTEM_ID.Visible = false;
            // 
            // ITAKU_KEIYAKU_TOUROKU_HOUHOU
            // 
            this.ITAKU_KEIYAKU_TOUROKU_HOUHOU.DataField = "ITAKU_KEIYAKU_TOUROKU_HOUHOU";
            this.ITAKU_KEIYAKU_TOUROKU_HOUHOU.DBFieldsName = "ITAKU_KEIYAKU_TOUROKU_HOUHOU";
            this.ITAKU_KEIYAKU_TOUROKU_HOUHOU.DefaultBackColor = System.Drawing.Color.Empty;
            this.ITAKU_KEIYAKU_TOUROKU_HOUHOU.DisplayItemName = "委託契約登録方法";
            this.ITAKU_KEIYAKU_TOUROKU_HOUHOU.DisplayPopUp = null;
            this.ITAKU_KEIYAKU_TOUROKU_HOUHOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ITAKU_KEIYAKU_TOUROKU_HOUHOU.FocusOutCheckMethod")));
            this.ITAKU_KEIYAKU_TOUROKU_HOUHOU.IsInputErrorOccured = false;
            this.ITAKU_KEIYAKU_TOUROKU_HOUHOU.ItemDefinedTypes = "varchar";
            this.ITAKU_KEIYAKU_TOUROKU_HOUHOU.Location = new System.Drawing.Point(673, 0);
            this.ITAKU_KEIYAKU_TOUROKU_HOUHOU.Name = "ITAKU_KEIYAKU_TOUROKU_HOUHOU";
            this.ITAKU_KEIYAKU_TOUROKU_HOUHOU.PopupAfterExecute = null;
            this.ITAKU_KEIYAKU_TOUROKU_HOUHOU.PopupBeforeExecute = null;
            this.ITAKU_KEIYAKU_TOUROKU_HOUHOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ITAKU_KEIYAKU_TOUROKU_HOUHOU.PopupSearchSendParams")));
            this.ITAKU_KEIYAKU_TOUROKU_HOUHOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ITAKU_KEIYAKU_TOUROKU_HOUHOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ITAKU_KEIYAKU_TOUROKU_HOUHOU.popupWindowSetting")));
            this.ITAKU_KEIYAKU_TOUROKU_HOUHOU.ReadOnly = true;
            this.ITAKU_KEIYAKU_TOUROKU_HOUHOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ITAKU_KEIYAKU_TOUROKU_HOUHOU.RegistCheckMethod")));
            this.ITAKU_KEIYAKU_TOUROKU_HOUHOU.ShortItemName = "委託契約登録方法";
            cellStyle13.BackColor = System.Drawing.Color.White;
            border6.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle13.Border = border6;
            cellStyle13.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle13.ForeColor = System.Drawing.Color.White;
            cellStyle13.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle13.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle13.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.ITAKU_KEIYAKU_TOUROKU_HOUHOU.Style = cellStyle13;
            this.ITAKU_KEIYAKU_TOUROKU_HOUHOU.TabIndex = 15;
            this.ITAKU_KEIYAKU_TOUROKU_HOUHOU.TabStop = false;
            this.ITAKU_KEIYAKU_TOUROKU_HOUHOU.Tag = "登録方法が表示されます";
            this.ITAKU_KEIYAKU_TOUROKU_HOUHOU.Visible = false;
            // 
            // GenbaHoshuDetail
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 42;
            this.Width = 550;

        }

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell4;
        private r_framework.CustomControl.GcCustomTextBoxCell textBoxCell1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell7;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell9;
        internal r_framework.CustomControl.GcCustomTextBoxCell ITAKU_KEIYAKU_NO;
        internal r_framework.CustomControl.GcCustomTextBoxCell ITAKU_KEIYAKU_SHURUI;
        internal r_framework.CustomControl.GcCustomTextBoxCell YUUKOU_BEGIN;
        internal r_framework.CustomControl.GcCustomTextBoxCell YUUKOU_END;
        internal r_framework.CustomControl.GcCustomTextBoxCell ITAKU_KEIYAKU_STATUS;
        internal r_framework.CustomControl.GcCustomTextBoxCell label1;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader5;
        internal r_framework.CustomControl.GcCustomTextBoxCell ITAKU_SYSTEM_ID;
        internal r_framework.CustomControl.GcCustomTextBoxCell ITAKU_KEIYAKU_TOUROKU_HOUHOU;
    }
}