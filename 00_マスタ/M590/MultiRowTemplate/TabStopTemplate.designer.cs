namespace Shougun.Core.Master.TabOrderSettei
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class TabStopTemplate
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.gcCustomColumnHeader1 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell2 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.columnHeaderCell3 = new GrapeCity.Win.MultiRow.ColumnHeaderCell();
            this.DENPYOU_NUMBER = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.DENPYOU_KOUMOKU = new GrapeCity.Win.MultiRow.TextBoxCell();
            this.DENPYOU_SENNI = new GrapeCity.Win.MultiRow.CheckBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.DENPYOU_NUMBER);
            this.Row.Cells.Add(this.DENPYOU_KOUMOKU);
            this.Row.Cells.Add(this.DENPYOU_SENNI);
            this.Row.Height = 21;
            this.Row.Width = 240;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader1);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell2);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell3);
            this.columnHeaderSection1.Height = 20;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.ReadOnly = false;
            this.columnHeaderSection1.Selectable = true;
            this.columnHeaderSection1.Width = 240;
            // 
            // gcCustomColumnHeader1
            // 
            this.gcCustomColumnHeader1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader1.Location = new System.Drawing.Point(0, 0);
            this.gcCustomColumnHeader1.Name = "gcCustomColumnHeader1";
            this.gcCustomColumnHeader1.Size = new System.Drawing.Size(40, 20);
            cellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Bottom = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            border1.Left = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            border1.Right = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            border1.Top = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle3.Border = border1;
            cellStyle3.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader1.Style = cellStyle3;
            this.gcCustomColumnHeader1.TabIndex = 0;
            this.gcCustomColumnHeader1.TabStop = false;
            this.gcCustomColumnHeader1.Value = "No.";
            // 
            // columnHeaderCell2
            // 
            this.columnHeaderCell2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell2.Location = new System.Drawing.Point(40, 0);
            this.columnHeaderCell2.Name = "columnHeaderCell2";
            this.columnHeaderCell2.Size = new System.Drawing.Size(150, 20);
            cellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Bottom = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            border2.Left = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            border2.Right = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            border2.Top = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle4.Border = border2;
            cellStyle4.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell2.Style = cellStyle4;
            this.columnHeaderCell2.TabIndex = 1;
            this.columnHeaderCell2.TabStop = false;
            this.columnHeaderCell2.Value = "項目";
            // 
            // columnHeaderCell3
            // 
            this.columnHeaderCell3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell3.Location = new System.Drawing.Point(190, 0);
            this.columnHeaderCell3.Name = "columnHeaderCell3";
            this.columnHeaderCell3.Size = new System.Drawing.Size(50, 20);
            cellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border3.Bottom = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            border3.Left = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            border3.Right = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            border3.Top = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle5.Border = border3;
            cellStyle5.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell3.Style = cellStyle5;
            this.columnHeaderCell3.TabIndex = 2;
            this.columnHeaderCell3.TabStop = false;
            this.columnHeaderCell3.Value = "遷移";
            // 
            // DENPYOU_NUMBER
            // 
            this.DENPYOU_NUMBER.Location = new System.Drawing.Point(0, 0);
            this.DENPYOU_NUMBER.Name = "DENPYOU_NUMBER";
            this.DENPYOU_NUMBER.ReadOnly = true;
            this.DENPYOU_NUMBER.Selectable = false;
            this.DENPYOU_NUMBER.Size = new System.Drawing.Size(40, 21);
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.DENPYOU_NUMBER.Style = cellStyle1;
            this.DENPYOU_NUMBER.TabIndex = 0;
            this.DENPYOU_NUMBER.TabStop = false;
            // 
            // DENPYOU_KOUMOKU
            // 
            this.DENPYOU_KOUMOKU.Location = new System.Drawing.Point(40, 0);
            this.DENPYOU_KOUMOKU.Name = "DENPYOU_KOUMOKU";
            this.DENPYOU_KOUMOKU.ReadOnly = true;
            this.DENPYOU_KOUMOKU.Selectable = false;
            this.DENPYOU_KOUMOKU.Size = new System.Drawing.Size(150, 21);
            this.DENPYOU_KOUMOKU.TabIndex = 1;
            this.DENPYOU_KOUMOKU.TabStop = false;
            // 
            // DENPYOU_SENNI
            // 
            this.DENPYOU_SENNI.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.DENPYOU_SENNI.Location = new System.Drawing.Point(190, 0);
            this.DENPYOU_SENNI.Name = "DENPYOU_SENNI";
            this.DENPYOU_SENNI.Size = new System.Drawing.Size(50, 21);
            this.DENPYOU_SENNI.Style = cellStyle2;
            this.DENPYOU_SENNI.TabIndex = 2;
            // 
            // TabStopTemplate
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 41;
            this.Width = 240;

        }
        

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderCell gcCustomColumnHeader1;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell2;
        private GrapeCity.Win.MultiRow.ColumnHeaderCell columnHeaderCell3;
        private GrapeCity.Win.MultiRow.TextBoxCell DENPYOU_NUMBER;
        private GrapeCity.Win.MultiRow.TextBoxCell DENPYOU_KOUMOKU;
        private GrapeCity.Win.MultiRow.CheckBoxCell DENPYOU_SENNI;
        public GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        
    }
}
