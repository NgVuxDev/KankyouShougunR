namespace ItakuKeiyakuHoshu.MultiRowTemplate
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class ItakuKeiyakuKihonHstGenba
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
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border4 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle10 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border5 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItakuKeiyakuKihonHstGenba));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.columnHeaderCell1 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell2 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell3 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader1 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader2 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.GENBA_HAISHUTSU_JIGYOUJOU_NAME = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1 = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD = new r_framework.CustomControl.GcCustomAlphaNumTextBoxCell();
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2 = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME = new r_framework.CustomControl.GcCustomTextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.GENBA_HAISHUTSU_JIGYOUJOU_CD);
            this.Row.Cells.Add(this.GENBA_HAISHUTSU_JIGYOUJOU_NAME);
            this.Row.Cells.Add(this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1);
            this.Row.Cells.Add(this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2);
            this.Row.Cells.Add(this.HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME);
            this.Row.Height = 21;
            this.Row.Width = 830;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell1);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell2);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell3);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader1);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader2);
            this.columnHeaderSection1.Height = 20;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 830;
            // 
            // columnHeaderCell1
            // 
            this.columnHeaderCell1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell1.HoverDirection = GrapeCity.Win.MultiRow.HoverDirection.None;
            this.columnHeaderCell1.Location = new System.Drawing.Point(0, 0);
            this.columnHeaderCell1.Name = "columnHeaderCell1";
            this.columnHeaderCell1.Size = new System.Drawing.Size(100, 20);
            cellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(160)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle6.Border = border1;
            cellStyle6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle6.ForeColor = System.Drawing.Color.White;
            cellStyle6.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.columnHeaderCell1.Style = cellStyle6;
            this.columnHeaderCell1.TabIndex = 0;
            this.columnHeaderCell1.Value = "排出事業場CD";
            this.columnHeaderCell1.ViewSearchItem = false;
            // 
            // columnHeaderCell2
            // 
            this.columnHeaderCell2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell2.HoverDirection = GrapeCity.Win.MultiRow.HoverDirection.None;
            this.columnHeaderCell2.Location = new System.Drawing.Point(100, 0);
            this.columnHeaderCell2.Name = "columnHeaderCell2";
            this.columnHeaderCell2.Size = new System.Drawing.Size(140, 20);
            cellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(160)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle7.Border = border2;
            cellStyle7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle7.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell2.Style = cellStyle7;
            this.columnHeaderCell2.TabIndex = 1;
            this.columnHeaderCell2.Value = "排出事業場名";
            this.columnHeaderCell2.ViewSearchItem = false;
            // 
            // columnHeaderCell3
            // 
            this.columnHeaderCell3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell3.HoverDirection = GrapeCity.Win.MultiRow.HoverDirection.None;
            this.columnHeaderCell3.Location = new System.Drawing.Point(310, 0);
            this.columnHeaderCell3.Name = "columnHeaderCell3";
            this.columnHeaderCell3.Size = new System.Drawing.Size(260, 20);
            cellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(160)))));
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle8.Border = border3;
            cellStyle8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle8.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell3.Style = cellStyle8;
            this.columnHeaderCell3.TabIndex = 5;
            this.columnHeaderCell3.Value = "排出事業場住所１";
            this.columnHeaderCell3.ViewSearchItem = false;
            // 
            // gcCustomColumnHeader1
            // 
            this.gcCustomColumnHeader1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader1.HoverDirection = GrapeCity.Win.MultiRow.HoverDirection.None;
            this.gcCustomColumnHeader1.Location = new System.Drawing.Point(570, 0);
            this.gcCustomColumnHeader1.Name = "gcCustomColumnHeader1";
            this.gcCustomColumnHeader1.Size = new System.Drawing.Size(260, 20);
            cellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(160)))));
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle9.Border = border4;
            cellStyle9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle9.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader1.Style = cellStyle9;
            this.gcCustomColumnHeader1.TabIndex = 6;
            this.gcCustomColumnHeader1.Value = "排出事業場住所２";
            this.gcCustomColumnHeader1.ViewSearchItem = false;
            // 
            // gcCustomColumnHeader2
            // 
            this.gcCustomColumnHeader2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader2.HoverDirection = GrapeCity.Win.MultiRow.HoverDirection.None;
            this.gcCustomColumnHeader2.Location = new System.Drawing.Point(240, 0);
            this.gcCustomColumnHeader2.Name = "gcCustomColumnHeader2";
            this.gcCustomColumnHeader2.Size = new System.Drawing.Size(70, 20);
            cellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(160)))));
            border5.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle10.Border = border5;
            cellStyle10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle10.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader2.Style = cellStyle10;
            this.gcCustomColumnHeader2.TabIndex = 7;
            this.gcCustomColumnHeader2.Value = "都道府県";
            this.gcCustomColumnHeader2.ViewSearchItem = false;
            // 
            // GENBA_HAISHUTSU_JIGYOUJOU_NAME
            // 
            this.GENBA_HAISHUTSU_JIGYOUJOU_NAME.DataField = "HAISHUTSU_JIGYOUJOU_NAME";
            this.GENBA_HAISHUTSU_JIGYOUJOU_NAME.DBFieldsName = "HAISHUTSU_JIGYOUJOU_NAME";
            this.GENBA_HAISHUTSU_JIGYOUJOU_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_HAISHUTSU_JIGYOUJOU_NAME.DisplayItemName = "排出事業場名";
            this.GENBA_HAISHUTSU_JIGYOUJOU_NAME.DisplayPopUp = null;
            this.GENBA_HAISHUTSU_JIGYOUJOU_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_HAISHUTSU_JIGYOUJOU_NAME.FocusOutCheckMethod")));
            this.GENBA_HAISHUTSU_JIGYOUJOU_NAME.IsInputErrorOccured = false;
            this.GENBA_HAISHUTSU_JIGYOUJOU_NAME.ItemDefinedTypes = "varchar";
            this.GENBA_HAISHUTSU_JIGYOUJOU_NAME.Location = new System.Drawing.Point(100, 0);
            this.GENBA_HAISHUTSU_JIGYOUJOU_NAME.Name = "GENBA_HAISHUTSU_JIGYOUJOU_NAME";
            this.GENBA_HAISHUTSU_JIGYOUJOU_NAME.PopupAfterExecute = null;
            this.GENBA_HAISHUTSU_JIGYOUJOU_NAME.PopupBeforeExecute = null;
            this.GENBA_HAISHUTSU_JIGYOUJOU_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_HAISHUTSU_JIGYOUJOU_NAME.PopupSearchSendParams")));
            this.GENBA_HAISHUTSU_JIGYOUJOU_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_HAISHUTSU_JIGYOUJOU_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_HAISHUTSU_JIGYOUJOU_NAME.popupWindowSetting")));
            this.GENBA_HAISHUTSU_JIGYOUJOU_NAME.ReadOnly = true;
            this.GENBA_HAISHUTSU_JIGYOUJOU_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_HAISHUTSU_JIGYOUJOU_NAME.RegistCheckMethod")));
            this.GENBA_HAISHUTSU_JIGYOUJOU_NAME.Selectable = false;
            this.GENBA_HAISHUTSU_JIGYOUJOU_NAME.ShortItemName = "排出事業場名";
            this.GENBA_HAISHUTSU_JIGYOUJOU_NAME.Size = new System.Drawing.Size(140, 21);
            cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GENBA_HAISHUTSU_JIGYOUJOU_NAME.Style = cellStyle2;
            this.GENBA_HAISHUTSU_JIGYOUJOU_NAME.TabIndex = 1;
            this.GENBA_HAISHUTSU_JIGYOUJOU_NAME.TabStop = false;
            this.GENBA_HAISHUTSU_JIGYOUJOU_NAME.Tag = "排出事業場名が表示されます";
            // 
            // GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1
            // 
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1.DataField = "HAISHUTSU_JIGYOUJOU_ADDRESS1";
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1.DBFieldsName = "HAISHUTSU_JIGYOUJOU_ADDRESS1";
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1.DisplayItemName = "排出事業場住所1";
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1.DisplayPopUp = null;
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1.FocusOutCheckMethod")));
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1.IsInputErrorOccured = false;
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1.ItemDefinedTypes = "varchar";
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1.Location = new System.Drawing.Point(310, 0);
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1.Name = "GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1";
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1.PopupAfterExecute = null;
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1.PopupBeforeExecute = null;
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1.PopupSearchSendParams")));
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1.popupWindowSetting")));
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1.ReadOnly = true;
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1.RegistCheckMethod")));
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1.Selectable = false;
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1.ShortItemName = "排出事業場住所1";
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1.Size = new System.Drawing.Size(260, 21);
            cellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1.Style = cellStyle3;
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1.TabIndex = 3;
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1.TabStop = false;
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1.Tag = "排出事業場住所1が表示されます";
            // 
            // GENBA_HAISHUTSU_JIGYOUJOU_CD
            // 
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.ChangeUpperCase = true;
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.CharacterLimitList = null;
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.DataField = "HAISHUTSU_JIGYOUJOU_CD";
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.DBFieldsName = "HAISHUTSU_JIGYOUJOU_CD";
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.DisplayItemName = "排出事業場CD";
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.DisplayPopUp = null;
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_HAISHUTSU_JIGYOUJOU_CD.FocusOutCheckMethod")));
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.GetCodeMasterField = "";
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.IsInputErrorOccured = false;
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.ItemDefinedTypes = "varchar";
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.Location = new System.Drawing.Point(0, 0);
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.MaxLength = 6;
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.Name = "GENBA_HAISHUTSU_JIGYOUJOU_CD";
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.PopupAfterExecute = null;
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.PopupAfterExecuteMethod = "PopupAfterHaishutsuJigyoujouCDHstGenba";
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.PopupBeforeExecute = null;
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.PopupGetMasterField = "GYOUSHA_CD,GENBA_CD";
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_HAISHUTSU_JIGYOUJOU_CD.PopupSearchSendParams")));
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.PopupSetFormField = "HAISHUTSU_JIGYOUSHA_CD,GENBA_HAISHUTSU_JIGYOUJOU_CD";
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_HAISHUTSU_JIGYOUJOU_CD.popupWindowSetting")));
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_HAISHUTSU_JIGYOUJOU_CD.RegistCheckMethod")));
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.SetFormField = "";
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.ShortItemName = "排出事業場CD";
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.Size = new System.Drawing.Size(100, 21);
            cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.Style = cellStyle1;
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.TabIndex = 0;
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.Tag = "排出事業場を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GENBA_HAISHUTSU_JIGYOUJOU_CD.ZeroPaddengFlag = true;
            // 
            // GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2
            // 
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2.DataField = "HAISHUTSU_JIGYOUJOU_ADDRESS2";
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2.DBFieldsName = "HAISHUTSU_JIGYOUJOU_ADDRESS2";
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2.DisplayItemName = "排出事業場住所2";
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2.DisplayPopUp = null;
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2.FocusOutCheckMethod")));
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2.IsInputErrorOccured = false;
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2.ItemDefinedTypes = "varchar";
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2.Location = new System.Drawing.Point(570, 0);
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2.Name = "GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2";
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2.PopupAfterExecute = null;
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2.PopupBeforeExecute = null;
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2.PopupSearchSendParams")));
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2.popupWindowSetting")));
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2.ReadOnly = true;
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2.RegistCheckMethod")));
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2.Selectable = false;
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2.ShortItemName = "排出事業場住所2";
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2.Size = new System.Drawing.Size(260, 21);
            cellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2.Style = cellStyle4;
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2.TabIndex = 4;
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2.TabStop = false;
            this.GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2.Tag = "排出事業場住所2が表示されます";
            // 
            // HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME
            // 
            this.HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME.DataField = "HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME";
            this.HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME.DBFieldsName = "TODOUFUKEN_NAME_RYAKU";
            this.HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME.DisplayItemName = "排出事業場都道府県";
            this.HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME.DisplayPopUp = null;
            this.HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME.FocusOutCheckMethod")));
            this.HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME.IsInputErrorOccured = false;
            this.HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME.ItemDefinedTypes = "varchar";
            this.HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME.Location = new System.Drawing.Point(240, 0);
            this.HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME.Name = "HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME";
            this.HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME.PopupAfterExecute = null;
            this.HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME.PopupBeforeExecute = null;
            this.HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME.PopupSearchSendParams")));
            this.HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME.popupWindowSetting")));
            this.HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME.ReadOnly = true;
            this.HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME.RegistCheckMethod")));
            this.HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME.Selectable = false;
            this.HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME.ShortItemName = "排出事業場都道府県";
            this.HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME.Size = new System.Drawing.Size(70, 21);
            cellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME.Style = cellStyle5;
            this.HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME.TabIndex = 2;
            this.HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME.TabStop = false;
            this.HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME.Tag = "排出事業場都道府県が表示されます";
            // 
            // ItakuKeiyakuKihonHstGenba
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 41;
            this.Width = 830;

        }


        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell1;
        private r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell2;
        private r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell3;
        internal r_framework.CustomControl.GcCustomTextBoxCell GENBA_HAISHUTSU_JIGYOUJOU_NAME;
        internal r_framework.CustomControl.GcCustomAlphaNumTextBoxCell GENBA_HAISHUTSU_JIGYOUJOU_CD;
        internal r_framework.CustomControl.GcCustomTextBoxCell GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS1;
        internal r_framework.CustomControl.GcCustomTextBoxCell GENBA_HAISHUTSU_JIGYOUJOU_ADDRESS2;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader1;
        internal r_framework.CustomControl.GcCustomTextBoxCell HAISHUTSU_JIGYOUJOU_TODOUFUKEN_NAME;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader2;
    }
}
