namespace ItakuKeiyakuHoshu.MultiRowTemplate
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class ItakuKeiyakuBetsu1YoteiKenpai
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItakuKeiyakuBetsu1YoteiKenpai));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
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
            ((System.ComponentModel.ISupportInitialize)(this.ComboDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.YOTEI_KIKAN_TABLE)).BeginInit();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.HOUKOKUSHO_BUNRUI_CD);
            this.Row.Cells.Add(this.HOUKOKUSHO_BUNRUI_NAME);
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gainsboro);
            cellStyle3.Border = border1;
            this.Row.DefaultCellStyle = cellStyle3;
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
            cellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(160)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle4.Border = border2;
            cellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle4.ForeColor = System.Drawing.Color.White;
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.gcCustomColumnHeader3.Style = cellStyle4;
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
            cellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(160)))));
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle5.Border = border3;
            cellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle5.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader4.Style = cellStyle5;
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
            this.YOTEI_KIKAN_CD.ColumnName = "YOTEI_KIKAN_CD";
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
            this.HOUKOKUSHO_BUNRUI_CD.PopupSetFormField = "HOUKOKUSHO_BUNRUI_CD,HOUKOKUSHO_BUNRUI_NAME";
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
            this.HOUKOKUSHO_BUNRUI_CD.TabIndex = 0;
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
            this.HOUKOKUSHO_BUNRUI_NAME.TabIndex = 1;
            this.HOUKOKUSHO_BUNRUI_NAME.TabStop = false;
            this.HOUKOKUSHO_BUNRUI_NAME.Tag = "報告書分類名が表示されます";
            // 
            // ItakuKeiyakuBetsu1YoteiKenpai
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            cellStyle6.BackColor = System.Drawing.Color.White;
            cellStyle6.BackgroundGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle6.Border = border4;
            cellStyle6.DisabledBackColor = System.Drawing.SystemColors.Control;
            cellStyle6.DisabledForeColor = System.Drawing.SystemColors.GrayText;
            cellStyle6.DisabledGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle6.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            cellStyle6.Format = "";
            cellStyle6.GradientDirection = GrapeCity.Win.MultiRow.GradientDirection.Center;
            cellStyle6.GradientStyle = GrapeCity.Win.MultiRow.GradientStyle.None;
            cellStyle6.ImageAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            cellStyle6.ImeSentenceMode = GrapeCity.Win.MultiRow.ImeSentenceMode.NoControl;
            cellStyle6.LineAdjustment = GrapeCity.Win.MultiRow.LineAdjustment.None;
            cellStyle6.Margin = new System.Windows.Forms.Padding(0);
            cellStyle6.MouseOverGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle6.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle6.Padding = new System.Windows.Forms.Padding(0);
            cellStyle6.PatternColor = System.Drawing.SystemColors.WindowText;
            cellStyle6.PatternStyle = GrapeCity.Win.MultiRow.MultiRowHatchStyle.None;
            cellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle6.SelectionGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle6.TextAdjustment = GrapeCity.Win.MultiRow.TextAdjustment.Near;
            cellStyle6.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle6.TextAngle = 0F;
            cellStyle6.TextEffect = GrapeCity.Win.MultiRow.TextEffect.Flat;
            cellStyle6.TextImageRelation = GrapeCity.Win.MultiRow.MultiRowTextImageRelation.Overlay;
            cellStyle6.TextIndent = 0;
            cellStyle6.TextVertical = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle6.UseCompatibleTextRendering = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle6.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            this.DefaultCellStyle = cellStyle6;
            this.Height = 41;
            this.Width = 245;
            ((System.ComponentModel.ISupportInitialize)(this.ComboDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.YOTEI_KIKAN_TABLE)).EndInit();

        }

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader3;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader4;
        internal System.Data.DataSet ComboDataSet;
        private System.Data.DataTable YOTEI_KIKAN_TABLE;
        private System.Data.DataColumn YOTEI_KIKAN_CD;
        private System.Data.DataColumn YOTEI_KIKAN_NAME;
        internal r_framework.CustomControl.GcCustomAlphaNumTextBoxCell HOUKOKUSHO_BUNRUI_CD;
        internal r_framework.CustomControl.GcCustomTextBoxCell HOUKOKUSHO_BUNRUI_NAME;
    }
}
