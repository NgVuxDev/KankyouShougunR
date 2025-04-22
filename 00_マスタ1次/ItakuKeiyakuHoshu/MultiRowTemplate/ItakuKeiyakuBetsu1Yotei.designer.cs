namespace ItakuKeiyakuHoshu.MultiRowTemplate
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class ItakuKeiyakuBetsu1Yotei
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItakuKeiyakuBetsu1Yotei));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border4 = new GrapeCity.Win.MultiRow.Border();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.gcCustomColumnHeader3 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader4 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.ComboDataSet = new System.Data.DataSet();
            this.YOTEI_KIKAN_TABLE = new System.Data.DataTable();
            this.YOTEI_KIKAN_CD = new System.Data.DataColumn();
            this.YOTEI_KIKAN_NAME = new System.Data.DataColumn();
            this.HOUKOKUSHO_BUNRUI_CD = new r_framework.CustomControl.GcCustomAlphaNumTextBoxCell();
            this.HOUKOKUSHO_BUNRUI_NAME = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS = new r_framework.CustomControl.GcCustomTextBoxCell();
            ((System.ComponentModel.ISupportInitialize)(this.ComboDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YOTEI_KIKAN_TABLE)).BeginInit();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.HOUKOKUSHO_BUNRUI_CD);
            this.Row.Cells.Add(this.HOUKOKUSHO_BUNRUI_NAME);
            this.Row.Cells.Add(this.YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS);
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gainsboro);
            cellStyle4.Border = border1;
            this.Row.DefaultCellStyle = cellStyle4;
            this.Row.Height = 21;
            this.Row.Width = 245;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader3);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader4);
            this.columnHeaderSection1.Height = 20;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 245;
            // 
            // gcCustomColumnHeader3
            // 
            this.gcCustomColumnHeader3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader3.Location = new System.Drawing.Point(0, 0);
            this.gcCustomColumnHeader3.Name = "gcCustomColumnHeader3";
            this.gcCustomColumnHeader3.Size = new System.Drawing.Size(95, 20);
            cellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(160)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle5.Border = border2;
            cellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle5.ForeColor = System.Drawing.Color.White;
            cellStyle5.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.gcCustomColumnHeader3.Style = cellStyle5;
            this.gcCustomColumnHeader3.TabIndex = 2;
            this.gcCustomColumnHeader3.Value = "報告書分類CD";
            this.gcCustomColumnHeader3.ViewSearchItem = false;
            // 
            // gcCustomColumnHeader4
            // 
            this.gcCustomColumnHeader4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader4.Location = new System.Drawing.Point(95, 0);
            this.gcCustomColumnHeader4.Name = "gcCustomColumnHeader4";
            this.gcCustomColumnHeader4.Size = new System.Drawing.Size(150, 20);
            cellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(160)))));
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle6.Border = border3;
            cellStyle6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle6.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader4.Style = cellStyle6;
            this.gcCustomColumnHeader4.TabIndex = 3;
            this.gcCustomColumnHeader4.Value = "報告書分類名";
            this.gcCustomColumnHeader4.ViewSearchItem = false;
            // 
            // ComboDataSet
            // 
            this.ComboDataSet.DataSetName = "ComboDataSet";
            this.ComboDataSet.Tables.AddRange(new System.Data.DataTable[] {
            this.YOTEI_KIKAN_TABLE});
            // 
            // YOTEI_KIKAN_TABLE
            // 
            this.YOTEI_KIKAN_TABLE.Columns.AddRange(new System.Data.DataColumn[] {
            this.YOTEI_KIKAN_CD,
            this.YOTEI_KIKAN_NAME});
            this.YOTEI_KIKAN_TABLE.TableName = "YOTEI_KIKAN_TABLE";
            // 
            // YOTEI_KIKAN_CD
            // 
            this.YOTEI_KIKAN_CD.AllowDBNull = false;
            this.YOTEI_KIKAN_CD.Caption = "YOTEI_KIKAN_CD";
            this.YOTEI_KIKAN_CD.ColumnName = "YOTEI_KIKAN_CD";
            this.YOTEI_KIKAN_CD.DataType = typeof(short);
            // 
            // YOTEI_KIKAN_NAME
            // 
            this.YOTEI_KIKAN_NAME.AllowDBNull = false;
            this.YOTEI_KIKAN_NAME.ColumnName = "YOTEI_KIKAN_NAME";
            // 
            // HOUKOKUSHO_BUNRUI_CD
            // 
            this.HOUKOKUSHO_BUNRUI_CD.ChangeUpperCase = true;
            this.HOUKOKUSHO_BUNRUI_CD.CharacterLimitList = null;
            this.HOUKOKUSHO_BUNRUI_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.HOUKOKUSHO_BUNRUI_CD.DataField = "HOUKOKUSHO_BUNRUI_CD";
            this.HOUKOKUSHO_BUNRUI_CD.DBFieldsName = "HOUKOKUSHO_BUNRUI_CD";
            this.HOUKOKUSHO_BUNRUI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.HOUKOKUSHO_BUNRUI_CD.DisplayItemName = "報告書分類CD";
            this.HOUKOKUSHO_BUNRUI_CD.DisplayPopUp = null;
            this.HOUKOKUSHO_BUNRUI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOUKOKUSHO_BUNRUI_CD.FocusOutCheckMethod")));
            this.HOUKOKUSHO_BUNRUI_CD.GetCodeMasterField = "HOUKOKUSHO_BUNRUI_CD, HOUKOKUSHO_BUNRUI_NAME_RYAKU";
            this.HOUKOKUSHO_BUNRUI_CD.IsInputErrorOccured = false;
            this.HOUKOKUSHO_BUNRUI_CD.ItemDefinedTypes = "varchar";
            this.HOUKOKUSHO_BUNRUI_CD.Location = new System.Drawing.Point(0, 0);
            this.HOUKOKUSHO_BUNRUI_CD.MaxLength = 6;
            this.HOUKOKUSHO_BUNRUI_CD.Name = "HOUKOKUSHO_BUNRUI_CD";
            this.HOUKOKUSHO_BUNRUI_CD.PopupAfterExecute = null;
            this.HOUKOKUSHO_BUNRUI_CD.PopupBeforeExecute = null;
            this.HOUKOKUSHO_BUNRUI_CD.PopupGetMasterField = "";
            this.HOUKOKUSHO_BUNRUI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HOUKOKUSHO_BUNRUI_CD.PopupSearchSendParams")));
            this.HOUKOKUSHO_BUNRUI_CD.PopupSendParams = new string[0];
            this.HOUKOKUSHO_BUNRUI_CD.PopupSetFormField = "";
            this.HOUKOKUSHO_BUNRUI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_HOUKOKUSHO_BUNRUI;
            this.HOUKOKUSHO_BUNRUI_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.HOUKOKUSHO_BUNRUI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HOUKOKUSHO_BUNRUI_CD.popupWindowSetting")));
            this.HOUKOKUSHO_BUNRUI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOUKOKUSHO_BUNRUI_CD.RegistCheckMethod")));
            this.HOUKOKUSHO_BUNRUI_CD.SetFormField = "HOUKOKUSHO_BUNRUI_CD,HOUKOKUSHO_BUNRUI_NAME";
            this.HOUKOKUSHO_BUNRUI_CD.ShortItemName = "報告書分類CD";
            this.HOUKOKUSHO_BUNRUI_CD.Size = new System.Drawing.Size(95, 21);
            cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.HOUKOKUSHO_BUNRUI_CD.Style = cellStyle1;
            this.HOUKOKUSHO_BUNRUI_CD.TabIndex = 2;
            this.HOUKOKUSHO_BUNRUI_CD.Tag = "報告書分類を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.HOUKOKUSHO_BUNRUI_CD.ZeroPaddengFlag = true;
            // 
            // HOUKOKUSHO_BUNRUI_NAME
            // 
            this.HOUKOKUSHO_BUNRUI_NAME.DataField = "HOUKOKUSHO_BUNRUI_NAME";
            this.HOUKOKUSHO_BUNRUI_NAME.DBFieldsName = "HOUKOKUSHO_BUNRUI_NAME_RYAKU";
            this.HOUKOKUSHO_BUNRUI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.HOUKOKUSHO_BUNRUI_NAME.DisplayItemName = "報告書分類名";
            this.HOUKOKUSHO_BUNRUI_NAME.DisplayPopUp = null;
            this.HOUKOKUSHO_BUNRUI_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOUKOKUSHO_BUNRUI_NAME.FocusOutCheckMethod")));
            this.HOUKOKUSHO_BUNRUI_NAME.IsInputErrorOccured = false;
            this.HOUKOKUSHO_BUNRUI_NAME.ItemDefinedTypes = "varchar";
            this.HOUKOKUSHO_BUNRUI_NAME.Location = new System.Drawing.Point(95, 0);
            this.HOUKOKUSHO_BUNRUI_NAME.Name = "HOUKOKUSHO_BUNRUI_NAME";
            this.HOUKOKUSHO_BUNRUI_NAME.PopupAfterExecute = null;
            this.HOUKOKUSHO_BUNRUI_NAME.PopupBeforeExecute = null;
            this.HOUKOKUSHO_BUNRUI_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HOUKOKUSHO_BUNRUI_NAME.PopupSearchSendParams")));
            this.HOUKOKUSHO_BUNRUI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HOUKOKUSHO_BUNRUI_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HOUKOKUSHO_BUNRUI_NAME.popupWindowSetting")));
            this.HOUKOKUSHO_BUNRUI_NAME.ReadOnly = true;
            this.HOUKOKUSHO_BUNRUI_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOUKOKUSHO_BUNRUI_NAME.RegistCheckMethod")));
            this.HOUKOKUSHO_BUNRUI_NAME.Selectable = false;
            this.HOUKOKUSHO_BUNRUI_NAME.ShortItemName = "報告書分類名";
            this.HOUKOKUSHO_BUNRUI_NAME.Size = new System.Drawing.Size(150, 21);
            cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HOUKOKUSHO_BUNRUI_NAME.Style = cellStyle2;
            this.HOUKOKUSHO_BUNRUI_NAME.TabIndex = 3;
            this.HOUKOKUSHO_BUNRUI_NAME.TabStop = false;
            this.HOUKOKUSHO_BUNRUI_NAME.Tag = "報告書分類名が表示されます";
            // 
            // YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS
            // 
            this.YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS.DataField = "HAISHUTSU_JIGYOUJOU_ADDRESS";
            this.YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS.DBFieldsName = "HAISHUTSU_JIGYOUJOU_ADDRESS";
            this.YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS.DefaultBackColor = System.Drawing.Color.Empty;
            this.YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS.DisplayItemName = "";
            this.YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS.DisplayPopUp = null;
            this.YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS.FocusOutCheckMethod = null;
            this.YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS.IsInputErrorOccured = false;
            this.YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS.ItemDefinedTypes = "varchar";
            this.YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS.Location = new System.Drawing.Point(800, 0);
            this.YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS.Name = "YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS";
            this.YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS.PopupAfterExecute = null;
            this.YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS.PopupBeforeExecute = null;
            this.YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS.popupWindowSetting = null;
            this.YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS.ReadOnly = true;
            this.YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS.RegistCheckMethod = null;
            this.YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS.Selectable = false;
            this.YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS.ShortItemName = "";
            this.YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS.Size = new System.Drawing.Size(20, 21);
            cellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS.Style = cellStyle3;
            this.YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS.TabIndex = 8;
            this.YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS.TabStop = false;
            this.YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS.Tag = "";
            this.YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS.Visible = false;
            // 
            // ItakuKeiyakuBetsu1Yotei
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            cellStyle7.BackColor = System.Drawing.Color.White;
            cellStyle7.BackgroundGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle7.Border = border4;
            cellStyle7.DisabledBackColor = System.Drawing.SystemColors.Control;
            cellStyle7.DisabledForeColor = System.Drawing.SystemColors.GrayText;
            cellStyle7.DisabledGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle7.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            cellStyle7.Format = "";
            cellStyle7.GradientDirection = GrapeCity.Win.MultiRow.GradientDirection.Center;
            cellStyle7.GradientStyle = GrapeCity.Win.MultiRow.GradientStyle.None;
            cellStyle7.ImageAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            cellStyle7.ImeSentenceMode = GrapeCity.Win.MultiRow.ImeSentenceMode.NoControl;
            cellStyle7.LineAdjustment = GrapeCity.Win.MultiRow.LineAdjustment.None;
            cellStyle7.Margin = new System.Windows.Forms.Padding(0);
            cellStyle7.MouseOverGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle7.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle7.Padding = new System.Windows.Forms.Padding(0);
            cellStyle7.PatternColor = System.Drawing.SystemColors.WindowText;
            cellStyle7.PatternStyle = GrapeCity.Win.MultiRow.MultiRowHatchStyle.None;
            cellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle7.SelectionGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle7.TextAdjustment = GrapeCity.Win.MultiRow.TextAdjustment.Near;
            cellStyle7.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle7.TextAngle = 0F;
            cellStyle7.TextEffect = GrapeCity.Win.MultiRow.TextEffect.Flat;
            cellStyle7.TextImageRelation = GrapeCity.Win.MultiRow.MultiRowTextImageRelation.Overlay;
            cellStyle7.TextIndent = 0;
            cellStyle7.TextVertical = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle7.UseCompatibleTextRendering = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle7.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            this.DefaultCellStyle = cellStyle7;
            this.Height = 41;
            this.Width = 245;
            ((System.ComponentModel.ISupportInitialize)(this.ComboDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YOTEI_KIKAN_TABLE)).EndInit();

        }

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader3;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader4;
        internal r_framework.CustomControl.GcCustomAlphaNumTextBoxCell HOUKOKUSHO_BUNRUI_CD;
        internal r_framework.CustomControl.GcCustomTextBoxCell HOUKOKUSHO_BUNRUI_NAME;
        internal r_framework.CustomControl.GcCustomTextBoxCell YOTEI_HAISHUTSU_JIGYOUJOU_ADDRESS;
        internal System.Data.DataSet ComboDataSet;
        private System.Data.DataTable YOTEI_KIKAN_TABLE;
        private System.Data.DataColumn YOTEI_KIKAN_CD;
        private System.Data.DataColumn YOTEI_KIKAN_NAME;
    }
}
