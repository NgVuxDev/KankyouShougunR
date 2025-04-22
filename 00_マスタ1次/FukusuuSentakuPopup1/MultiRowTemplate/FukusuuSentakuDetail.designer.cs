namespace FukusuuSentakuPopup1
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class FukusuuSentakuDetail
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
            GrapeCity.Win.MultiRow.Border border6 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle10 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border7 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle11 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border8 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle12 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle13 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border9 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle14 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border10 = new GrapeCity.Win.MultiRow.Border();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.gcCustomColumnHeader1 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader2 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader3 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.CHECKED = new r_framework.CustomControl.GcCustomCheckBoxCell();
            this.HAIKI_KBN_CD = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.HAIKI_KBN_NAME_RYAKU = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.gcCustomColumnHeader5 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader4 = new r_framework.CustomControl.GcCustomColumnHeader();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.CHECKED);
            this.Row.Cells.Add(this.HAIKI_KBN_CD);
            this.Row.Cells.Add(this.HAIKI_KBN_NAME_RYAKU);
            this.Row.Height = 21;
            this.Row.Width = 425;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader1);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader2);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader3);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 425;
            // 
            // gcCustomColumnHeader1
            // 
            this.gcCustomColumnHeader1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader1.Location = new System.Drawing.Point(1, 1);
            this.gcCustomColumnHeader1.Name = "gcCustomColumnHeader1";
            this.gcCustomColumnHeader1.Size = new System.Drawing.Size(24, 20);
            cellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border6.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle9.Border = border6;
            cellStyle9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle9.ForeColor = System.Drawing.Color.White;
            cellStyle9.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.gcCustomColumnHeader1.Style = cellStyle9;
            this.gcCustomColumnHeader1.TabIndex = 3;
            this.gcCustomColumnHeader1.Value = "選";
            this.gcCustomColumnHeader1.ViewSearchItem = false;
            // 
            // gcCustomColumnHeader2
            // 
            this.gcCustomColumnHeader2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader2.Location = new System.Drawing.Point(25, 1);
            this.gcCustomColumnHeader2.Name = "gcCustomColumnHeader2";
            this.gcCustomColumnHeader2.Size = new System.Drawing.Size(100, 20);
            cellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border7.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle10.Border = border7;
            cellStyle10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle10.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader2.Style = cellStyle10;
            this.gcCustomColumnHeader2.TabIndex = 4;
            this.gcCustomColumnHeader2.Value = "廃棄物区分CD";
            this.gcCustomColumnHeader2.ViewSearchItem = false;
            // 
            // gcCustomColumnHeader3
            // 
            this.gcCustomColumnHeader3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader3.Location = new System.Drawing.Point(125, 1);
            this.gcCustomColumnHeader3.Name = "gcCustomColumnHeader3";
            this.gcCustomColumnHeader3.Size = new System.Drawing.Size(100, 20);
            cellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border8.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle11.Border = border8;
            cellStyle11.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle11.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader3.Style = cellStyle11;
            this.gcCustomColumnHeader3.TabIndex = 5;
            this.gcCustomColumnHeader3.Value = "廃棄物区分名";
            this.gcCustomColumnHeader3.ViewSearchItem = false;
            // 
            // CHECKED
            // 
            this.CHECKED.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CHECKED.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CHECKED.DataField = "CHECKED";
            this.CHECKED.DBFieldsName = null;
            this.CHECKED.DefaultBackColor = System.Drawing.Color.Empty;
            this.CHECKED.DisplayItemName = null;
            this.CHECKED.ErrorMessage = null;
            this.CHECKED.GetCodeMasterField = null;
            this.CHECKED.ItemDefinedTypes = null;
            this.CHECKED.Location = new System.Drawing.Point(0, 0);
            this.CHECKED.Name = "CHECKED";
            this.CHECKED.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CHECKED.PopupWindowName = null;
            this.CHECKED.SearchDisplayFlag = 0;
            this.CHECKED.SetFormField = null;
            this.CHECKED.ShortItemName = null;
            this.CHECKED.Size = new System.Drawing.Size(24, 21);
            cellStyle12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle12.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle12.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.CHECKED.Style = cellStyle12;
            this.CHECKED.TabIndex = 3;
            this.CHECKED.ZeroPaddengFlag = false;
            // 
            // HAIKI_KBN_CD
            // 
            this.HAIKI_KBN_CD.DataField = "HAIKI_KBN_CD";
            this.HAIKI_KBN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.HAIKI_KBN_CD.DisplayPopUp = null;
            this.HAIKI_KBN_CD.Location = new System.Drawing.Point(24, 0);
            this.HAIKI_KBN_CD.Name = "HAIKI_KBN_CD";
            this.HAIKI_KBN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HAIKI_KBN_CD.ShortItemName = "1";
            this.HAIKI_KBN_CD.Size = new System.Drawing.Size(100, 21);
            cellStyle13.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle13.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle13.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle13.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.HAIKI_KBN_CD.Style = cellStyle13;
            this.HAIKI_KBN_CD.TabIndex = 4;
            // 
            // HAIKI_KBN_NAME_RYAKU
            // 
            this.HAIKI_KBN_NAME_RYAKU.DataField = "HAIKI_KBN_NAME_RYAKU";
            this.HAIKI_KBN_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.HAIKI_KBN_NAME_RYAKU.DisplayPopUp = null;
            this.HAIKI_KBN_NAME_RYAKU.Location = new System.Drawing.Point(125, 0);
            this.HAIKI_KBN_NAME_RYAKU.Name = "HAIKI_KBN_NAME_RYAKU";
            this.HAIKI_KBN_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HAIKI_KBN_NAME_RYAKU.ShortItemName = "2";
            this.HAIKI_KBN_NAME_RYAKU.Size = new System.Drawing.Size(100, 21);
            cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle2.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.HAIKI_KBN_NAME_RYAKU.Style = cellStyle2;
            this.HAIKI_KBN_NAME_RYAKU.TabIndex = 5;
            // 
            // gcCustomColumnHeader5
            // 
            this.gcCustomColumnHeader5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader5.Location = new System.Drawing.Point(325, 1);
            this.gcCustomColumnHeader5.Name = "gcCustomColumnHeader5";
            this.gcCustomColumnHeader5.Size = new System.Drawing.Size(100, 20);
            cellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border9.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle3.Border = border9;
            cellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle3.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader5.Style = cellStyle3;
            this.gcCustomColumnHeader5.TabIndex = 7;
            this.gcCustomColumnHeader5.Value = "廃棄物種類名";
            this.gcCustomColumnHeader5.ViewSearchItem = false;
            // 
            // gcCustomColumnHeader4
            // 
            this.gcCustomColumnHeader4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader4.Location = new System.Drawing.Point(225, 1);
            this.gcCustomColumnHeader4.Name = "gcCustomColumnHeader4";
            this.gcCustomColumnHeader4.Size = new System.Drawing.Size(100, 20);
            cellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border10.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle14.Border = border10;
            cellStyle14.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle14.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader4.Style = cellStyle14;
            this.gcCustomColumnHeader4.TabIndex = 6;
            this.gcCustomColumnHeader4.Value = "廃棄物種類CD";
            this.gcCustomColumnHeader4.ViewSearchItem = false;
            // 
            // FukusuuSentakuDetail
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 42;
            this.Width = 225;

        }


        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private r_framework.CustomControl.GcCustomCheckBoxCell CHECKED;
        private r_framework.CustomControl.GcCustomTextBoxCell HAIKI_KBN_CD;
        private r_framework.CustomControl.GcCustomTextBoxCell HAIKI_KBN_NAME_RYAKU;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader1;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader2;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader3;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader4;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader5;

    }
}
