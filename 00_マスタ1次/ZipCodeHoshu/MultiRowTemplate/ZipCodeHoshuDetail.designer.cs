namespace ZipCodeHoshu.MultiRowTemplate
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class ZipCodeHoshuDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZipCodeHoshuDetail));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.columnHeaderCell1 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell2 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell3 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell4 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.POST7 = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.TODOUFUKEN = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.SIKUCHOUSON = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.OTHER1 = new r_framework.CustomControl.GcCustomTextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.POST7);
            this.Row.Cells.Add(this.TODOUFUKEN);
            this.Row.Cells.Add(this.SIKUCHOUSON);
            this.Row.Cells.Add(this.OTHER1);
            this.Row.Height = 21;
            this.Row.Width = 995;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell1);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell2);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell3);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell4);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 995;
            // 
            // columnHeaderCell1
            // 
            this.columnHeaderCell1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell1.Location = new System.Drawing.Point(0, 0);
            this.columnHeaderCell1.Name = "columnHeaderCell1";
            this.columnHeaderCell1.Size = new System.Drawing.Size(65, 21);
            cellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle5.Border = border1;
            cellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle5.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell1.Style = cellStyle5;
            this.columnHeaderCell1.TabIndex = 0;
            this.columnHeaderCell1.Value = "郵便番号";
            this.columnHeaderCell1.ViewSearchItem = false;
            // 
            // columnHeaderCell2
            // 
            this.columnHeaderCell2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell2.Location = new System.Drawing.Point(65, 0);
            this.columnHeaderCell2.Name = "columnHeaderCell2";
            this.columnHeaderCell2.Size = new System.Drawing.Size(70, 21);
            cellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle6.Border = border2;
            cellStyle6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle6.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell2.Style = cellStyle6;
            this.columnHeaderCell2.TabIndex = 1;
            this.columnHeaderCell2.Value = "都道府県";
            this.columnHeaderCell2.ViewSearchItem = false;
            // 
            // columnHeaderCell3
            // 
            this.columnHeaderCell3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell3.Location = new System.Drawing.Point(135, 0);
            this.columnHeaderCell3.Name = "columnHeaderCell3";
            this.columnHeaderCell3.Size = new System.Drawing.Size(290, 21);
            cellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle7.Border = border3;
            cellStyle7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle7.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell3.Style = cellStyle7;
            this.columnHeaderCell3.TabIndex = 2;
            this.columnHeaderCell3.Value = "市区町村";
            this.columnHeaderCell3.ViewSearchItem = false;
            // 
            // columnHeaderCell4
            // 
            this.columnHeaderCell4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell4.Location = new System.Drawing.Point(425, 0);
            this.columnHeaderCell4.Name = "columnHeaderCell4";
            this.columnHeaderCell4.Size = new System.Drawing.Size(570, 21);
            cellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle8.Border = border4;
            cellStyle8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle8.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell4.Style = cellStyle8;
            this.columnHeaderCell4.TabIndex = 3;
            this.columnHeaderCell4.Value = "地名";
            this.columnHeaderCell4.ViewSearchItem = false;
            // 
            // POST7
            // 
            this.POST7.DataField = "POST7";
            this.POST7.DBFieldsName = "POST7";
            this.POST7.DefaultBackColor = System.Drawing.Color.Empty;
            this.POST7.DisplayPopUp = null;
            this.POST7.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("POST7.FocusOutCheckMethod")));
            this.POST7.IsInputErrorOccured = false;
            this.POST7.Location = new System.Drawing.Point(0, 0);
            this.POST7.Name = "POST7";
            this.POST7.PopupAfterExecute = null;
            this.POST7.PopupBeforeExecute = null;
            this.POST7.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("POST7.PopupSearchSendParams")));
            this.POST7.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.POST7.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("POST7.popupWindowSetting")));
            this.POST7.ReadOnly = true;
            this.POST7.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("POST7.RegistCheckMethod")));
            this.POST7.Size = new System.Drawing.Size(65, 21);
            cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.POST7.Style = cellStyle1;
            this.POST7.TabIndex = 0;
            this.POST7.TabStop = false;
            this.POST7.Tag = "郵便番号が表示されます";
            // 
            // TODOUFUKEN
            // 
            this.TODOUFUKEN.DataField = "TODOUFUKEN";
            this.TODOUFUKEN.DBFieldsName = "TODOUFUKEN";
            this.TODOUFUKEN.DefaultBackColor = System.Drawing.Color.Empty;
            this.TODOUFUKEN.DisplayPopUp = null;
            this.TODOUFUKEN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TODOUFUKEN.FocusOutCheckMethod")));
            this.TODOUFUKEN.IsInputErrorOccured = false;
            this.TODOUFUKEN.Location = new System.Drawing.Point(65, 0);
            this.TODOUFUKEN.Name = "TODOUFUKEN";
            this.TODOUFUKEN.PopupAfterExecute = null;
            this.TODOUFUKEN.PopupBeforeExecute = null;
            this.TODOUFUKEN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TODOUFUKEN.PopupSearchSendParams")));
            this.TODOUFUKEN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TODOUFUKEN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TODOUFUKEN.popupWindowSetting")));
            this.TODOUFUKEN.ReadOnly = true;
            this.TODOUFUKEN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TODOUFUKEN.RegistCheckMethod")));
            this.TODOUFUKEN.Size = new System.Drawing.Size(70, 21);
            cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TODOUFUKEN.Style = cellStyle2;
            this.TODOUFUKEN.TabIndex = 1;
            this.TODOUFUKEN.TabStop = false;
            this.TODOUFUKEN.Tag = "都道府県が表示されます";
            // 
            // SIKUCHOUSON
            // 
            this.SIKUCHOUSON.DataField = "SIKUCHOUSON";
            this.SIKUCHOUSON.DBFieldsName = "SIKUCHOUSON";
            this.SIKUCHOUSON.DefaultBackColor = System.Drawing.Color.Empty;
            this.SIKUCHOUSON.DisplayPopUp = null;
            this.SIKUCHOUSON.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SIKUCHOUSON.FocusOutCheckMethod")));
            this.SIKUCHOUSON.IsInputErrorOccured = false;
            this.SIKUCHOUSON.Location = new System.Drawing.Point(135, 0);
            this.SIKUCHOUSON.Name = "SIKUCHOUSON";
            this.SIKUCHOUSON.PopupAfterExecute = null;
            this.SIKUCHOUSON.PopupBeforeExecute = null;
            this.SIKUCHOUSON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SIKUCHOUSON.PopupSearchSendParams")));
            this.SIKUCHOUSON.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SIKUCHOUSON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SIKUCHOUSON.popupWindowSetting")));
            this.SIKUCHOUSON.ReadOnly = true;
            this.SIKUCHOUSON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SIKUCHOUSON.RegistCheckMethod")));
            this.SIKUCHOUSON.Size = new System.Drawing.Size(290, 21);
            cellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SIKUCHOUSON.Style = cellStyle3;
            this.SIKUCHOUSON.TabIndex = 2;
            this.SIKUCHOUSON.TabStop = false;
            this.SIKUCHOUSON.Tag = "市区町村が表示されます";
            // 
            // OTHER1
            // 
            this.OTHER1.DataField = "OTHER1";
            this.OTHER1.DBFieldsName = "OTHER1";
            this.OTHER1.DefaultBackColor = System.Drawing.Color.Empty;
            this.OTHER1.DisplayPopUp = null;
            this.OTHER1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("OTHER1.FocusOutCheckMethod")));
            this.OTHER1.IsInputErrorOccured = false;
            this.OTHER1.Location = new System.Drawing.Point(425, 0);
            this.OTHER1.Name = "OTHER1";
            this.OTHER1.PopupAfterExecute = null;
            this.OTHER1.PopupBeforeExecute = null;
            this.OTHER1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("OTHER1.PopupSearchSendParams")));
            this.OTHER1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.OTHER1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("OTHER1.popupWindowSetting")));
            this.OTHER1.ReadOnly = true;
            this.OTHER1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("OTHER1.RegistCheckMethod")));
            this.OTHER1.Size = new System.Drawing.Size(570, 21);
            cellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.OTHER1.Style = cellStyle4;
            this.OTHER1.TabIndex = 3;
            this.OTHER1.TabStop = false;
            this.OTHER1.Tag = "地名が表示されます";
            // 
            // ZipCodeHoshuDetail
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 42;
            this.Width = 995;

        }


        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell1;
        private r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell2;
        private r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell3;
        private r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell4;
        internal r_framework.CustomControl.GcCustomTextBoxCell POST7;
        internal r_framework.CustomControl.GcCustomTextBoxCell TODOUFUKEN;
        internal r_framework.CustomControl.GcCustomTextBoxCell SIKUCHOUSON;
        internal r_framework.CustomControl.GcCustomTextBoxCell OTHER1;

    }
}
