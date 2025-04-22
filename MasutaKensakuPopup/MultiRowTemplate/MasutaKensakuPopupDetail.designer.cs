namespace MasutaKensakuPopup.MultiRowTemplate
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class MasutaKensakuPopupDetail
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MasutaKensakuPopupDetail));
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle10 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.HEADER_NAME = new GrapeCity.Win.MultiRow.CornerHeaderCell();
            this.Text = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.ImeMode = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.DBFieldsName = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.ItemDefinedTypes = new r_framework.CustomControl.GcCustomTextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.Text);
            this.Row.Cells.Add(this.ImeMode);
            this.Row.Cells.Add(this.DBFieldsName);
            this.Row.Cells.Add(this.ItemDefinedTypes);
            this.Row.Height = 21;
            this.Row.Width = 479;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.HEADER_NAME);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 479;
            // 
            // HEADER_NAME
            // 
            this.HEADER_NAME.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HEADER_NAME.Location = new System.Drawing.Point(0, 0);
            this.HEADER_NAME.Name = "HEADER_NAME";
            this.HEADER_NAME.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.HEADER_NAME.Size = new System.Drawing.Size(238, 21);
            cellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Bottom = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Black);
            border2.Left = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Black);
            border2.Right = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Black);
            border2.Top = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Black);
            cellStyle6.Border = border2;
            cellStyle6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle6.ForeColor = System.Drawing.Color.White;
            this.HEADER_NAME.Style = cellStyle6;
            this.HEADER_NAME.TabIndex = 2;
            this.HEADER_NAME.Value = "項目名";
            // 
            // Text
            // 
            this.Text.DataField = "Item";
            this.Text.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Text.FocusOutCheckMethod")));
            this.Text.Location = new System.Drawing.Point(0, 0);
            this.Text.MaxLength = 0;
            this.Text.Name = "Text";
            this.Text.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Text.ReadOnly = true;
            this.Text.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Text.RegistCheckMethod")));
            this.Text.SetFormField = "";
            this.Text.ShortItemName = "1";
            this.Text.Size = new System.Drawing.Size(238, 21);
            cellStyle7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle7.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle7.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.Text.Style = cellStyle7;
            this.Text.TabIndex = 1;
            // 
            // ImeMode
            // 
            this.ImeMode.DataField = "ImeMode";
            this.ImeMode.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ImeMode.FocusOutCheckMethod")));
            this.ImeMode.Location = new System.Drawing.Point(238, 0);
            this.ImeMode.MaxLength = 0;
            this.ImeMode.Name = "ImeMode";
            this.ImeMode.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ImeMode.ReadOnly = true;
            this.ImeMode.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ImeMode.RegistCheckMethod")));
            this.ImeMode.Selectable = false;
            this.ImeMode.ShortItemName = "2";
            this.ImeMode.Size = new System.Drawing.Size(38, 21);
            cellStyle8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle8.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle8.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.ImeMode.Style = cellStyle8;
            this.ImeMode.TabIndex = 2;
            this.ImeMode.TabStop = false;
            this.ImeMode.Visible = false;
            // 
            // DBFieldsName
            // 
            this.DBFieldsName.DataField = "DBFieldsName";
            this.DBFieldsName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DBFieldsName.FocusOutCheckMethod")));
            this.DBFieldsName.Location = new System.Drawing.Point(276, 0);
            this.DBFieldsName.MaxLength = 0;
            this.DBFieldsName.Name = "DBFieldsName";
            this.DBFieldsName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DBFieldsName.ReadOnly = true;
            this.DBFieldsName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DBFieldsName.RegistCheckMethod")));
            this.DBFieldsName.Selectable = false;
            this.DBFieldsName.ShortItemName = "2";
            this.DBFieldsName.Size = new System.Drawing.Size(33, 21);
            cellStyle9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle9.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle9.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.DBFieldsName.Style = cellStyle9;
            this.DBFieldsName.TabIndex = 3;
            this.DBFieldsName.TabStop = false;
            this.DBFieldsName.Visible = false;
            // 
            // ItemDefinedTypes
            // 
            this.ItemDefinedTypes.DataField = "ItemDefinedTypes";
            this.ItemDefinedTypes.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ItemDefinedTypes.FocusOutCheckMethod")));
            this.ItemDefinedTypes.Location = new System.Drawing.Point(309, 0);
            this.ItemDefinedTypes.MaxLength = 0;
            this.ItemDefinedTypes.Name = "ItemDefinedTypes";
            this.ItemDefinedTypes.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ItemDefinedTypes.ReadOnly = true;
            this.ItemDefinedTypes.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ItemDefinedTypes.RegistCheckMethod")));
            this.ItemDefinedTypes.Selectable = false;
            this.ItemDefinedTypes.ShortItemName = "2";
            this.ItemDefinedTypes.Size = new System.Drawing.Size(33, 21);
            cellStyle10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle10.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle10.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.ItemDefinedTypes.Style = cellStyle10;
            this.ItemDefinedTypes.TabIndex = 4;
            this.ItemDefinedTypes.TabStop = false;
            this.ItemDefinedTypes.Visible = false;
            // 
            // MasutaKensakuPopupDetail
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 42;
            this.Width = 238;

        }
        

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private GrapeCity.Win.MultiRow.CornerHeaderCell HEADER_NAME;
        internal r_framework.CustomControl.GcCustomTextBoxCell Text;
        internal r_framework.CustomControl.GcCustomTextBoxCell ImeMode;
        internal r_framework.CustomControl.GcCustomTextBoxCell DBFieldsName;
        internal r_framework.CustomControl.GcCustomTextBoxCell ItemDefinedTypes;
        
    }
}
