namespace MenuKengenHoshu.MultiRowTemplate
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class MenuKengenHoshuDetail_Shain
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.HD_SHAIN_ALL_CHECK = new r_framework.CustomControl.GcCustomCheckBoxCell();
            this.HD_SHAIN_CD = new r_framework.CustomControl.GcCustomColumnHeader();
            this.HD_SHAIN_NAME = new r_framework.CustomControl.GcCustomColumnHeader();
            this.SHAIN_CD = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.SHAIN_CHECK = new r_framework.CustomControl.GcCustomCheckBoxCell();
            this.SHAIN_NAME = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.BUSHO_CD = new r_framework.CustomControl.GcCustomTextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.SHAIN_CHECK);
            this.Row.Cells.Add(this.SHAIN_CD);
            this.Row.Cells.Add(this.SHAIN_NAME);
            this.Row.Cells.Add(this.BUSHO_CD);
            this.Row.Height = 21;
            this.Row.Width = 310;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.HD_SHAIN_ALL_CHECK);
            this.columnHeaderSection1.Cells.Add(this.HD_SHAIN_CD);
            this.columnHeaderSection1.Cells.Add(this.HD_SHAIN_NAME);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.ReadOnly = false;
            this.columnHeaderSection1.Selectable = true;
            this.columnHeaderSection1.Width = 310;
            // 
            // HD_SHAIN_ALL_CHECK
            // 
            this.HD_SHAIN_ALL_CHECK.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.HD_SHAIN_ALL_CHECK.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.HD_SHAIN_ALL_CHECK.DBFieldsName = null;
            this.HD_SHAIN_ALL_CHECK.DefaultBackColor = System.Drawing.Color.Empty;
            this.HD_SHAIN_ALL_CHECK.DisplayItemName = "";
            this.HD_SHAIN_ALL_CHECK.ErrorMessage = null;
            this.HD_SHAIN_ALL_CHECK.FalseValue = "0";
            this.HD_SHAIN_ALL_CHECK.FlatAppearance.BorderSize = 0;
            this.HD_SHAIN_ALL_CHECK.GetCodeMasterField = null;
            this.HD_SHAIN_ALL_CHECK.IndeterminateValue = "False";
            this.HD_SHAIN_ALL_CHECK.ItemDefinedTypes = null;
            this.HD_SHAIN_ALL_CHECK.Location = new System.Drawing.Point(0, 0);
            this.HD_SHAIN_ALL_CHECK.Name = "HD_SHAIN_ALL_CHECK";
            this.HD_SHAIN_ALL_CHECK.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HD_SHAIN_ALL_CHECK.PopupWindowName = null;
            this.HD_SHAIN_ALL_CHECK.SearchDisplayFlag = 0;
            this.HD_SHAIN_ALL_CHECK.SetFormField = null;
            this.HD_SHAIN_ALL_CHECK.ShortItemName = null;
            this.HD_SHAIN_ALL_CHECK.Size = new System.Drawing.Size(21, 21);
            cellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle5.Border = border1;
            cellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle5.ForeColor = System.Drawing.Color.White;
            cellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.HD_SHAIN_ALL_CHECK.Style = cellStyle5;
            this.HD_SHAIN_ALL_CHECK.TabIndex = 1;
            this.HD_SHAIN_ALL_CHECK.TabStop = false;
            this.HD_SHAIN_ALL_CHECK.Text = "";
            this.HD_SHAIN_ALL_CHECK.TrueValue = "1";
            this.HD_SHAIN_ALL_CHECK.Value = false;
            this.HD_SHAIN_ALL_CHECK.ZeroPaddengFlag = false;
            // 
            // HD_SHAIN_CD
            // 
            this.HD_SHAIN_CD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HD_SHAIN_CD.Location = new System.Drawing.Point(21, 0);
            this.HD_SHAIN_CD.Name = "HD_SHAIN_CD";
            this.HD_SHAIN_CD.Size = new System.Drawing.Size(59, 21);
            cellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle6.Border = border2;
            cellStyle6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle6.ForeColor = System.Drawing.Color.White;
            cellStyle6.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.HD_SHAIN_CD.Style = cellStyle6;
            this.HD_SHAIN_CD.TabIndex = 2;
            this.HD_SHAIN_CD.Value = "社員CD";
            this.HD_SHAIN_CD.ViewSearchItem = false;
            // 
            // HD_SHAIN_NAME
            // 
            this.HD_SHAIN_NAME.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HD_SHAIN_NAME.Location = new System.Drawing.Point(80, 0);
            this.HD_SHAIN_NAME.Name = "HD_SHAIN_NAME";
            this.HD_SHAIN_NAME.Size = new System.Drawing.Size(230, 21);
            cellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle7.Border = border3;
            cellStyle7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle7.ForeColor = System.Drawing.Color.White;
            cellStyle7.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.HD_SHAIN_NAME.Style = cellStyle7;
            this.HD_SHAIN_NAME.TabIndex = 13;
            this.HD_SHAIN_NAME.Value = "社員名";
            this.HD_SHAIN_NAME.ViewSearchItem = false;
            // 
            // SHAIN_CD
            // 
            this.SHAIN_CD.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.SHAIN_CD.DBFieldsName = "";
            this.SHAIN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHAIN_CD.DisplayItemName = "";
            this.SHAIN_CD.DisplayPopUp = null;
            this.SHAIN_CD.ItemDefinedTypes = "varchar";
            this.SHAIN_CD.Location = new System.Drawing.Point(21, 0);
            this.SHAIN_CD.MaxLength = 6;
            this.SHAIN_CD.Name = "SHAIN_CD";
            this.SHAIN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHAIN_CD.ReadOnly = true;
            this.SHAIN_CD.SetFormField = "";
            this.SHAIN_CD.Size = new System.Drawing.Size(59, 21);
            cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.SHAIN_CD.Style = cellStyle2;
            this.SHAIN_CD.TabIndex = 2;
            this.SHAIN_CD.TabStop = false;
            this.SHAIN_CD.Tag = "社員CDが表示されます";
            // 
            // SHAIN_CHECK
            // 
            this.SHAIN_CHECK.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SHAIN_CHECK.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SHAIN_CHECK.DBFieldsName = null;
            this.SHAIN_CHECK.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHAIN_CHECK.DisplayItemName = "";
            this.SHAIN_CHECK.ErrorMessage = null;
            this.SHAIN_CHECK.GetCodeMasterField = null;
            this.SHAIN_CHECK.ItemDefinedTypes = "bit";
            this.SHAIN_CHECK.Location = new System.Drawing.Point(0, 0);
            this.SHAIN_CHECK.Name = "SHAIN_CHECK";
            this.SHAIN_CHECK.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHAIN_CHECK.PopupWindowName = null;
            this.SHAIN_CHECK.SearchDisplayFlag = 0;
            this.SHAIN_CHECK.SetFormField = null;
            this.SHAIN_CHECK.ShortItemName = "";
            this.SHAIN_CHECK.Size = new System.Drawing.Size(21, 21);
            cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle1.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.SHAIN_CHECK.Style = cellStyle1;
            this.SHAIN_CHECK.TabIndex = 1;
            this.SHAIN_CHECK.Tag = "権限を登録する社員にチェックを付けてください";
            this.SHAIN_CHECK.ZeroPaddengFlag = false;
            // 
            // SHAIN_NAME
            // 
            this.SHAIN_NAME.CharactersNumber = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.SHAIN_NAME.DBFieldsName = "";
            this.SHAIN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHAIN_NAME.DisplayItemName = "";
            this.SHAIN_NAME.DisplayPopUp = null;
            this.SHAIN_NAME.ItemDefinedTypes = "varchar";
            this.SHAIN_NAME.Location = new System.Drawing.Point(80, 0);
            this.SHAIN_NAME.MaxLength = 16;
            this.SHAIN_NAME.Name = "SHAIN_NAME";
            this.SHAIN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHAIN_NAME.ReadOnly = true;
            this.SHAIN_NAME.SetFormField = "";
            this.SHAIN_NAME.Size = new System.Drawing.Size(230, 21);
            cellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.SHAIN_NAME.Style = cellStyle3;
            this.SHAIN_NAME.TabIndex = 3;
            this.SHAIN_NAME.TabStop = false;
            this.SHAIN_NAME.Tag = "社員名が表示されます";
            // 
            // BUSHO_CD
            // 
            this.BUSHO_CD.CharactersNumber = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.BUSHO_CD.DataField = "BUSHO_CD";
            this.BUSHO_CD.DBFieldsName = "BUSHO_CD";
            this.BUSHO_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.BUSHO_CD.DisplayItemName = "部署CD";
            this.BUSHO_CD.DisplayPopUp = null;
            this.BUSHO_CD.ItemDefinedTypes = "varchar";
            this.BUSHO_CD.Location = new System.Drawing.Point(310, 0);
            this.BUSHO_CD.Name = "BUSHO_CD";
            this.BUSHO_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BUSHO_CD.Selectable = false;
            this.BUSHO_CD.SetFormField = "";
            this.BUSHO_CD.Size = new System.Drawing.Size(49, 21);
            cellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BUSHO_CD.Style = cellStyle4;
            this.BUSHO_CD.TabIndex = 4;
            this.BUSHO_CD.TabStop = false;
            this.BUSHO_CD.Visible = false;
            // 
            // MenuKengenHoshuDetail_Shain
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 42;
            this.Width = 310;

        }
        

        #endregion

        public GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private r_framework.CustomControl.GcCustomTextBoxCell SHAIN_CD;
        internal r_framework.CustomControl.GcCustomCheckBoxCell SHAIN_CHECK;
        internal r_framework.CustomControl.GcCustomCheckBoxCell HD_SHAIN_ALL_CHECK;
        internal r_framework.CustomControl.GcCustomColumnHeader HD_SHAIN_CD;
        private r_framework.CustomControl.GcCustomTextBoxCell SHAIN_NAME;
        internal r_framework.CustomControl.GcCustomColumnHeader HD_SHAIN_NAME;
        internal r_framework.CustomControl.GcCustomTextBoxCell BUSHO_CD;
        
    }
}
