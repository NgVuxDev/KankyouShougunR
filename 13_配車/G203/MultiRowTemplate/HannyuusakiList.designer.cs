namespace Shougun.Core.Allocation.HannyuusakiKyuudouNyuuryoku
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class HannyuusakiList
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border4 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border5 = new GrapeCity.Win.MultiRow.Border();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HannyuusakiList));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.cornerHeaderCell1 = new GrapeCity.Win.MultiRow.CornerHeaderCell();
            this.GYOUSHA_CD_LABEL = new r_framework.CustomControl.GcCustomColumnHeader();
            this.GYOUSHA_NAME_LABEL = new r_framework.CustomControl.GcCustomColumnHeader();
            this.GENBA_CD_LABEL = new r_framework.CustomControl.GcCustomColumnHeader();
            this.GENBA_NAME_LABEL = new r_framework.CustomControl.GcCustomColumnHeader();
            this.rowHeaderCell1 = new GrapeCity.Win.MultiRow.RowHeaderCell();
            this.GYOUSHA_CD = new r_framework.CustomControl.GcCustomAlphaNumTextBoxCell();
            this.GYOUSHA_NAME_RYAKU = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.GENBA_CD = new r_framework.CustomControl.GcCustomAlphaNumTextBoxCell();
            this.GENBA_NAME_RYAKU = new r_framework.CustomControl.GcCustomTextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.rowHeaderCell1);
            this.Row.Cells.Add(this.GYOUSHA_CD);
            this.Row.Cells.Add(this.GYOUSHA_NAME_RYAKU);
            this.Row.Cells.Add(this.GENBA_CD);
            this.Row.Cells.Add(this.GENBA_NAME_RYAKU);
            this.Row.Height = 42;
            this.Row.Width = 397;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.cornerHeaderCell1);
            this.columnHeaderSection1.Cells.Add(this.GYOUSHA_CD_LABEL);
            this.columnHeaderSection1.Cells.Add(this.GYOUSHA_NAME_LABEL);
            this.columnHeaderSection1.Cells.Add(this.GENBA_CD_LABEL);
            this.columnHeaderSection1.Cells.Add(this.GENBA_NAME_LABEL);
            this.columnHeaderSection1.Height = 40;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 397;
            // 
            // cornerHeaderCell1
            // 
            this.cornerHeaderCell1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cornerHeaderCell1.Location = new System.Drawing.Point(0, 0);
            this.cornerHeaderCell1.Name = "cornerHeaderCell1";
            this.cornerHeaderCell1.Size = new System.Drawing.Size(30, 42);
            cellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.White);
            cellStyle5.Border = border1;
            cellStyle5.ForeColor = System.Drawing.Color.White;
            this.cornerHeaderCell1.Style = cellStyle5;
            this.cornerHeaderCell1.TabIndex = 0;
            this.cornerHeaderCell1.TabStop = false;
            // 
            // GYOUSHA_CD_LABEL
            // 
            this.GYOUSHA_CD_LABEL.FilterCellIndex = 1;
            this.GYOUSHA_CD_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GYOUSHA_CD_LABEL.Location = new System.Drawing.Point(30, 0);
            this.GYOUSHA_CD_LABEL.Name = "GYOUSHA_CD_LABEL";
            this.GYOUSHA_CD_LABEL.Size = new System.Drawing.Size(80, 21);
            cellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle6.Border = border2;
            cellStyle6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle6.ForeColor = System.Drawing.Color.White;
            this.GYOUSHA_CD_LABEL.Style = cellStyle6;
            this.GYOUSHA_CD_LABEL.TabIndex = 1;
            this.GYOUSHA_CD_LABEL.Value = "荷降業者CD";
            this.GYOUSHA_CD_LABEL.ViewSearchItem = true;
            // 
            // GYOUSHA_NAME_LABEL
            // 
            this.GYOUSHA_NAME_LABEL.FilterCellIndex = 3;
            this.GYOUSHA_NAME_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GYOUSHA_NAME_LABEL.Location = new System.Drawing.Point(110, 0);
            this.GYOUSHA_NAME_LABEL.Name = "GYOUSHA_NAME_LABEL";
            this.GYOUSHA_NAME_LABEL.Size = new System.Drawing.Size(287, 21);
            cellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle7.Border = border3;
            cellStyle7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle7.ForeColor = System.Drawing.Color.White;
            this.GYOUSHA_NAME_LABEL.Style = cellStyle7;
            this.GYOUSHA_NAME_LABEL.TabIndex = 2;
            this.GYOUSHA_NAME_LABEL.Value = "荷降業者名";
            this.GYOUSHA_NAME_LABEL.ViewSearchItem = true;
            // 
            // GENBA_CD_LABEL
            // 
            this.GENBA_CD_LABEL.FilterCellIndex = 2;
            this.GENBA_CD_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GENBA_CD_LABEL.Location = new System.Drawing.Point(30, 21);
            this.GENBA_CD_LABEL.Name = "GENBA_CD_LABEL";
            this.GENBA_CD_LABEL.Size = new System.Drawing.Size(80, 21);
            cellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle8.Border = border4;
            cellStyle8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle8.ForeColor = System.Drawing.Color.White;
            this.GENBA_CD_LABEL.Style = cellStyle8;
            this.GENBA_CD_LABEL.TabIndex = 3;
            this.GENBA_CD_LABEL.Value = "荷降現場CD";
            this.GENBA_CD_LABEL.ViewSearchItem = true;
            // 
            // GENBA_NAME_LABEL
            // 
            this.GENBA_NAME_LABEL.FilterCellIndex = 4;
            this.GENBA_NAME_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GENBA_NAME_LABEL.Location = new System.Drawing.Point(110, 21);
            this.GENBA_NAME_LABEL.Name = "GENBA_NAME_LABEL";
            this.GENBA_NAME_LABEL.Size = new System.Drawing.Size(287, 21);
            cellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border5.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle9.Border = border5;
            cellStyle9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle9.ForeColor = System.Drawing.Color.White;
            this.GENBA_NAME_LABEL.Style = cellStyle9;
            this.GENBA_NAME_LABEL.TabIndex = 4;
            this.GENBA_NAME_LABEL.Value = "荷降現場名";
            this.GENBA_NAME_LABEL.ViewSearchItem = true;
            // 
            // rowHeaderCell1
            // 
            this.rowHeaderCell1.Location = new System.Drawing.Point(0, 0);
            this.rowHeaderCell1.Name = "rowHeaderCell1";
            this.rowHeaderCell1.Size = new System.Drawing.Size(30, 42);
            this.rowHeaderCell1.TabIndex = 0;
            this.rowHeaderCell1.TabStop = false;
            // 
            // GYOUSHA_CD
            // 
            this.GYOUSHA_CD.CharacterLimitList = null;
            this.GYOUSHA_CD.DataField = "GYOUSHA_CD";
            this.GYOUSHA_CD.DBFieldsName = "GYOUSHA_CD";
            this.GYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_CD.DisplayItemName = "業者CD";
            this.GYOUSHA_CD.DisplayPopUp = null;
            this.GYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.FocusOutCheckMethod")));
            this.GYOUSHA_CD.IsInputErrorOccured = false;
            this.GYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.GYOUSHA_CD.Location = new System.Drawing.Point(30, 0);
            this.GYOUSHA_CD.Name = "GYOUSHA_CD";
            this.GYOUSHA_CD.PopupAfterExecute = null;
            this.GYOUSHA_CD.PopupBeforeExecute = null;
            this.GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_CD.PopupSearchSendParams")));
            this.GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_CD.popupWindowSetting")));
            this.GYOUSHA_CD.ReadOnly = true;
            this.GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.RegistCheckMethod")));
            cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle1.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.GYOUSHA_CD.Style = cellStyle1;
            this.GYOUSHA_CD.TabIndex = 1;
            this.GYOUSHA_CD.Tag = " ";
            // 
            // GYOUSHA_NAME_RYAKU
            // 
            this.GYOUSHA_NAME_RYAKU.DataField = "GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_NAME_RYAKU.DBFieldsName = "GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_NAME_RYAKU.DisplayItemName = "業者名";
            this.GYOUSHA_NAME_RYAKU.DisplayPopUp = null;
            this.GYOUSHA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.FocusOutCheckMethod")));
            this.GYOUSHA_NAME_RYAKU.IsInputErrorOccured = false;
            this.GYOUSHA_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.GYOUSHA_NAME_RYAKU.Location = new System.Drawing.Point(110, 0);
            this.GYOUSHA_NAME_RYAKU.Name = "GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_NAME_RYAKU.PopupAfterExecute = null;
            this.GYOUSHA_NAME_RYAKU.PopupBeforeExecute = null;
            this.GYOUSHA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.PopupSearchSendParams")));
            this.GYOUSHA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.popupWindowSetting")));
            this.GYOUSHA_NAME_RYAKU.ReadOnly = true;
            this.GYOUSHA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.RegistCheckMethod")));
            this.GYOUSHA_NAME_RYAKU.Size = new System.Drawing.Size(287, 21);
            cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle2.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.GYOUSHA_NAME_RYAKU.Style = cellStyle2;
            this.GYOUSHA_NAME_RYAKU.TabIndex = 2;
            this.GYOUSHA_NAME_RYAKU.Tag = " ";
            // 
            // GENBA_CD
            // 
            this.GENBA_CD.CharacterLimitList = null;
            this.GENBA_CD.DataField = "GENBA_CD";
            this.GENBA_CD.DBFieldsName = "GENBA_CD";
            this.GENBA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_CD.DisplayItemName = "現場CD";
            this.GENBA_CD.DisplayPopUp = null;
            this.GENBA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.FocusOutCheckMethod")));
            this.GENBA_CD.IsInputErrorOccured = false;
            this.GENBA_CD.ItemDefinedTypes = "varchar";
            this.GENBA_CD.Location = new System.Drawing.Point(30, 21);
            this.GENBA_CD.Name = "GENBA_CD";
            this.GENBA_CD.PopupAfterExecute = null;
            this.GENBA_CD.PopupBeforeExecute = null;
            this.GENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_CD.PopupSearchSendParams")));
            this.GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_CD.popupWindowSetting")));
            this.GENBA_CD.ReadOnly = true;
            this.GENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.RegistCheckMethod")));
            cellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle3.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.GENBA_CD.Style = cellStyle3;
            this.GENBA_CD.TabIndex = 3;
            this.GENBA_CD.Tag = " ";
            // 
            // GENBA_NAME_RYAKU
            // 
            this.GENBA_NAME_RYAKU.DataField = "GENBA_NAME_RYAKU";
            this.GENBA_NAME_RYAKU.DBFieldsName = "GENBA_NAME_RYAKU";
            this.GENBA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_NAME_RYAKU.DisplayItemName = "現場名";
            this.GENBA_NAME_RYAKU.DisplayPopUp = null;
            this.GENBA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_RYAKU.FocusOutCheckMethod")));
            this.GENBA_NAME_RYAKU.IsInputErrorOccured = false;
            this.GENBA_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.GENBA_NAME_RYAKU.Location = new System.Drawing.Point(110, 21);
            this.GENBA_NAME_RYAKU.Name = "GENBA_NAME_RYAKU";
            this.GENBA_NAME_RYAKU.PopupAfterExecute = null;
            this.GENBA_NAME_RYAKU.PopupBeforeExecute = null;
            this.GENBA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_NAME_RYAKU.PopupSearchSendParams")));
            this.GENBA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_NAME_RYAKU.popupWindowSetting")));
            this.GENBA_NAME_RYAKU.ReadOnly = true;
            this.GENBA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_RYAKU.RegistCheckMethod")));
            this.GENBA_NAME_RYAKU.Size = new System.Drawing.Size(287, 21);
            cellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle4.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.GENBA_NAME_RYAKU.Style = cellStyle4;
            this.GENBA_NAME_RYAKU.TabIndex = 4;
            this.GENBA_NAME_RYAKU.Tag = " ";
            // 
            // HannyuusakiList
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 82;
            this.Width = 397;

        }
        

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.CornerHeaderCell cornerHeaderCell1;
        private r_framework.CustomControl.GcCustomColumnHeader GYOUSHA_CD_LABEL;
        private r_framework.CustomControl.GcCustomColumnHeader GYOUSHA_NAME_LABEL;
        private r_framework.CustomControl.GcCustomColumnHeader GENBA_CD_LABEL;
        private r_framework.CustomControl.GcCustomColumnHeader GENBA_NAME_LABEL;
        private GrapeCity.Win.MultiRow.RowHeaderCell rowHeaderCell1;
        private r_framework.CustomControl.GcCustomAlphaNumTextBoxCell GYOUSHA_CD;
        private r_framework.CustomControl.GcCustomTextBoxCell GYOUSHA_NAME_RYAKU;
        private r_framework.CustomControl.GcCustomAlphaNumTextBoxCell GENBA_CD;
        private r_framework.CustomControl.GcCustomTextBoxCell GENBA_NAME_RYAKU;
        
    }
}
