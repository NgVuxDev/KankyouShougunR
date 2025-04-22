using System.Collections.ObjectModel;
namespace JushoKensakuPopup1.MultiRowTemplate
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class JushoKensakuDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JushoKensakuDetail));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.gcCustomColumnHeader1 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader2 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader3 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader4 = new r_framework.CustomControl.GcCustomColumnHeader();
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
            this.Row.Width = 658;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader1);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader2);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader3);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader4);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 658;
            // 
            // gcCustomColumnHeader1
            // 
            this.gcCustomColumnHeader1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader1.Location = new System.Drawing.Point(0, 0);
            this.gcCustomColumnHeader1.Name = "gcCustomColumnHeader1";
            this.gcCustomColumnHeader1.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.gcCustomColumnHeader1.Size = new System.Drawing.Size(80, 22);
            cellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle5.Border = border1;
            cellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle5.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader1.Style = cellStyle5;
            this.gcCustomColumnHeader1.TabIndex = 4;
            this.gcCustomColumnHeader1.Value = "郵便番号";
            this.gcCustomColumnHeader1.ViewSearchItem = false;
            // 
            // gcCustomColumnHeader2
            // 
            this.gcCustomColumnHeader2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader2.Location = new System.Drawing.Point(80, 0);
            this.gcCustomColumnHeader2.Name = "gcCustomColumnHeader2";
            this.gcCustomColumnHeader2.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.gcCustomColumnHeader2.Size = new System.Drawing.Size(80, 22);
            cellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle6.Border = border2;
            cellStyle6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle6.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader2.Style = cellStyle6;
            this.gcCustomColumnHeader2.TabIndex = 5;
            this.gcCustomColumnHeader2.Value = "都道府県";
            this.gcCustomColumnHeader2.ViewSearchItem = false;
            // 
            // gcCustomColumnHeader3
            // 
            this.gcCustomColumnHeader3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader3.Location = new System.Drawing.Point(160, 0);
            this.gcCustomColumnHeader3.Name = "gcCustomColumnHeader3";
            this.gcCustomColumnHeader3.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.gcCustomColumnHeader3.Size = new System.Drawing.Size(200, 22);
            cellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle7.Border = border3;
            cellStyle7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle7.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader3.Style = cellStyle7;
            this.gcCustomColumnHeader3.TabIndex = 6;
            this.gcCustomColumnHeader3.Value = "市町村";
            this.gcCustomColumnHeader3.ViewSearchItem = false;
            // 
            // gcCustomColumnHeader4
            // 
            this.gcCustomColumnHeader4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader4.Location = new System.Drawing.Point(360, 0);
            this.gcCustomColumnHeader4.Name = "gcCustomColumnHeader4";
            this.gcCustomColumnHeader4.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.gcCustomColumnHeader4.Size = new System.Drawing.Size(298, 22);
            cellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle8.Border = border4;
            cellStyle8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle8.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader4.Style = cellStyle8;
            this.gcCustomColumnHeader4.TabIndex = 7;
            this.gcCustomColumnHeader4.Value = "その他";
            this.gcCustomColumnHeader4.ViewSearchItem = false;
            // 
            // POST7
            // 
            this.POST7.DataField = "POST7";
            this.POST7.DBFieldsName = "POST7";
            this.POST7.FocusOutCheckMethod = ((Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("POST7.FocusOutCheckMethod")));
            this.POST7.ItemDefinedTypes = "varchar";
            this.POST7.Location = new System.Drawing.Point(0, 0);
            this.POST7.MaxLength = 0;
            this.POST7.Name = "POST7";
            this.POST7.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.POST7.popupWindowSetting = ((Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("POST7.popupWindowSetting")));
            this.POST7.ReadOnly = true;
            this.POST7.RegistCheckMethod = ((Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("POST7.RegistCheckMethod")));
            cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle1.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.POST7.Style = cellStyle1;
            this.POST7.TabIndex = 0;
            this.POST7.Tag = " ";
            // 
            // TODOUFUKEN
            // 
            this.TODOUFUKEN.DataField = "TODOUFUKEN";
            this.TODOUFUKEN.DBFieldsName = "TODOUFUKEN";
            this.TODOUFUKEN.FocusOutCheckMethod = ((Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TODOUFUKEN.FocusOutCheckMethod")));
            this.TODOUFUKEN.ItemDefinedTypes = "varchar";
            this.TODOUFUKEN.Location = new System.Drawing.Point(80, 0);
            this.TODOUFUKEN.MaxLength = 0;
            this.TODOUFUKEN.Name = "TODOUFUKEN";
            this.TODOUFUKEN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TODOUFUKEN.popupWindowSetting = ((Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TODOUFUKEN.popupWindowSetting")));
            this.TODOUFUKEN.ReadOnly = true;
            this.TODOUFUKEN.RegistCheckMethod = ((Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TODOUFUKEN.RegistCheckMethod")));
            cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle2.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.TODOUFUKEN.Style = cellStyle2;
            this.TODOUFUKEN.TabIndex = 1;
            this.TODOUFUKEN.Tag = " ";
            // 
            // SIKUCHOUSON
            // 
            this.SIKUCHOUSON.DataField = "SIKUCHOUSON";
            this.SIKUCHOUSON.DBFieldsName = "SIKUCHOUSON";
            this.SIKUCHOUSON.FocusOutCheckMethod = ((Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SIKUCHOUSON.FocusOutCheckMethod")));
            this.SIKUCHOUSON.ItemDefinedTypes = "varchar";
            this.SIKUCHOUSON.Location = new System.Drawing.Point(160, 0);
            this.SIKUCHOUSON.MaxLength = 0;
            this.SIKUCHOUSON.Name = "SIKUCHOUSON";
            this.SIKUCHOUSON.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SIKUCHOUSON.popupWindowSetting = ((Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SIKUCHOUSON.popupWindowSetting")));
            this.SIKUCHOUSON.ReadOnly = true;
            this.SIKUCHOUSON.RegistCheckMethod = ((Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SIKUCHOUSON.RegistCheckMethod")));
            this.SIKUCHOUSON.Size = new System.Drawing.Size(200, 21);
            cellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle3.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.SIKUCHOUSON.Style = cellStyle3;
            this.SIKUCHOUSON.TabIndex = 2;
            this.SIKUCHOUSON.Tag = " ";
            // 
            // OTHER1
            // 
            this.OTHER1.DataField = "OTHER1";
            this.OTHER1.DBFieldsName = "OTHER1";
            this.OTHER1.FocusOutCheckMethod = ((Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("OTHER1.FocusOutCheckMethod")));
            this.OTHER1.ItemDefinedTypes = "varchar";
            this.OTHER1.Location = new System.Drawing.Point(360, 0);
            this.OTHER1.MaxLength = 0;
            this.OTHER1.Name = "OTHER1";
            this.OTHER1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.OTHER1.popupWindowSetting = ((Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("OTHER1.popupWindowSetting")));
            this.OTHER1.ReadOnly = true;
            this.OTHER1.RegistCheckMethod = ((Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("OTHER1.RegistCheckMethod")));
            this.OTHER1.Size = new System.Drawing.Size(298, 21);
            cellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle4.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.OTHER1.Style = cellStyle4;
            this.OTHER1.TabIndex = 3;
            this.OTHER1.Tag = " ";
            // 
            // JushoKensakuDetail
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 42;
            this.Width = 658;

        }
        

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader1;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader2;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader3;
        private r_framework.CustomControl.GcCustomTextBoxCell POST7;
        private r_framework.CustomControl.GcCustomTextBoxCell TODOUFUKEN;
        private r_framework.CustomControl.GcCustomTextBoxCell SIKUCHOUSON;
        private r_framework.CustomControl.GcCustomTextBoxCell OTHER1;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader4;
        
    }
}
