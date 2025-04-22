namespace ItakuKeiyakuHoshu.MultiRowTemplate
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class ItakuKeiyakuBetsu2Kenpai
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle10 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border4 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle11 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border5 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle12 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border6 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItakuKeiyakuBetsu2Kenpai));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.gcCustomColumnHeader1 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader2 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader4 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader5 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell3 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader3 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.UNPAN_GYOUSHA_NAME = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.UNPAN_GYOUSHA_CD = new r_framework.CustomControl.GcCustomAlphaNumTextBoxCell();
            this.KYOKA_SHARYOU_SUU = new r_framework.CustomControl.GcCustomNumericTextBox2Cell();
            this.UNPAN_TODOUFUKEN_NAME = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.UNPAN_GYOUSHA_ADDRESS1 = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.UNPAN_GYOUSHA_ADDRESS2 = new r_framework.CustomControl.GcCustomTextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.UNPAN_GYOUSHA_CD);
            this.Row.Cells.Add(this.UNPAN_GYOUSHA_NAME);
            this.Row.Cells.Add(this.KYOKA_SHARYOU_SUU);
            this.Row.Cells.Add(this.UNPAN_TODOUFUKEN_NAME);
            this.Row.Cells.Add(this.UNPAN_GYOUSHA_ADDRESS1);
            this.Row.Cells.Add(this.UNPAN_GYOUSHA_ADDRESS2);
            this.Row.Height = 21;
            this.Row.Width = 980;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader1);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader2);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader4);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader5);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell3);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader3);
            this.columnHeaderSection1.Height = 20;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 980;
            // 
            // gcCustomColumnHeader1
            // 
            this.gcCustomColumnHeader1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader1.HoverDirection = GrapeCity.Win.MultiRow.HoverDirection.None;
            this.gcCustomColumnHeader1.Location = new System.Drawing.Point(0, 0);
            this.gcCustomColumnHeader1.Name = "gcCustomColumnHeader1";
            cellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle7.Border = border1;
            cellStyle7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle7.ForeColor = System.Drawing.Color.White;
            cellStyle7.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.gcCustomColumnHeader1.Style = cellStyle7;
            this.gcCustomColumnHeader1.TabIndex = 0;
            this.gcCustomColumnHeader1.Value = "運搬業者CD";
            this.gcCustomColumnHeader1.ViewSearchItem = false;
            // 
            // gcCustomColumnHeader2
            // 
            this.gcCustomColumnHeader2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader2.HoverDirection = GrapeCity.Win.MultiRow.HoverDirection.None;
            this.gcCustomColumnHeader2.Location = new System.Drawing.Point(80, 0);
            this.gcCustomColumnHeader2.Name = "gcCustomColumnHeader2";
            this.gcCustomColumnHeader2.Size = new System.Drawing.Size(150, 20);
            cellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle8.Border = border2;
            cellStyle8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle8.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader2.Style = cellStyle8;
            this.gcCustomColumnHeader2.TabIndex = 1;
            this.gcCustomColumnHeader2.Value = "運搬業者名";
            this.gcCustomColumnHeader2.ViewSearchItem = false;
            // 
            // gcCustomColumnHeader4
            // 
            this.gcCustomColumnHeader4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader4.HoverDirection = GrapeCity.Win.MultiRow.HoverDirection.None;
            this.gcCustomColumnHeader4.Location = new System.Drawing.Point(880, 0);
            this.gcCustomColumnHeader4.Name = "gcCustomColumnHeader4";
            this.gcCustomColumnHeader4.Size = new System.Drawing.Size(100, 20);
            cellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle9.Border = border3;
            cellStyle9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle9.ForeColor = System.Drawing.Color.White;
            cellStyle9.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.gcCustomColumnHeader4.Style = cellStyle9;
            this.gcCustomColumnHeader4.TabIndex = 6;
            this.gcCustomColumnHeader4.Value = "許可車輛台数";
            this.gcCustomColumnHeader4.ViewSearchItem = false;
            // 
            // gcCustomColumnHeader5
            // 
            this.gcCustomColumnHeader5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader5.HoverDirection = GrapeCity.Win.MultiRow.HoverDirection.None;
            this.gcCustomColumnHeader5.Location = new System.Drawing.Point(230, 0);
            this.gcCustomColumnHeader5.Name = "gcCustomColumnHeader5";
            this.gcCustomColumnHeader5.Size = new System.Drawing.Size(70, 20);
            cellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle10.Border = border4;
            cellStyle10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle10.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader5.Style = cellStyle10;
            this.gcCustomColumnHeader5.TabIndex = 7;
            this.gcCustomColumnHeader5.Value = "都道府県";
            this.gcCustomColumnHeader5.ViewSearchItem = false;
            // 
            // columnHeaderCell3
            // 
            this.columnHeaderCell3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell3.HoverDirection = GrapeCity.Win.MultiRow.HoverDirection.None;
            this.columnHeaderCell3.Location = new System.Drawing.Point(300, 0);
            this.columnHeaderCell3.Name = "columnHeaderCell3";
            this.columnHeaderCell3.Size = new System.Drawing.Size(290, 20);
            cellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border5.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle11.Border = border5;
            cellStyle11.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle11.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell3.Style = cellStyle11;
            this.columnHeaderCell3.TabIndex = 8;
            this.columnHeaderCell3.Value = "運搬業者住所１";
            this.columnHeaderCell3.ViewSearchItem = false;
            // 
            // gcCustomColumnHeader3
            // 
            this.gcCustomColumnHeader3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader3.HoverDirection = GrapeCity.Win.MultiRow.HoverDirection.None;
            this.gcCustomColumnHeader3.Location = new System.Drawing.Point(590, 0);
            this.gcCustomColumnHeader3.Name = "gcCustomColumnHeader3";
            this.gcCustomColumnHeader3.Size = new System.Drawing.Size(290, 20);
            cellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border6.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle12.Border = border6;
            cellStyle12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle12.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader3.Style = cellStyle12;
            this.gcCustomColumnHeader3.TabIndex = 9;
            this.gcCustomColumnHeader3.Value = "運搬業者住所２";
            this.gcCustomColumnHeader3.ViewSearchItem = false;
            // 
            // UNPAN_GYOUSHA_NAME
            // 
            this.UNPAN_GYOUSHA_NAME.DataField = "UNPAN_GYOUSHA_NAME";
            this.UNPAN_GYOUSHA_NAME.DBFieldsName = "GENBA_NAME_RYAKU";
            this.UNPAN_GYOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNPAN_GYOUSHA_NAME.DisplayItemName = "運搬業者名";
            this.UNPAN_GYOUSHA_NAME.DisplayPopUp = null;
            this.UNPAN_GYOUSHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_NAME.FocusOutCheckMethod")));
            this.UNPAN_GYOUSHA_NAME.IsInputErrorOccured = false;
            this.UNPAN_GYOUSHA_NAME.ItemDefinedTypes = "varchar";
            this.UNPAN_GYOUSHA_NAME.Location = new System.Drawing.Point(80, 0);
            this.UNPAN_GYOUSHA_NAME.Name = "UNPAN_GYOUSHA_NAME";
            this.UNPAN_GYOUSHA_NAME.PopupAfterExecute = null;
            this.UNPAN_GYOUSHA_NAME.PopupBeforeExecute = null;
            this.UNPAN_GYOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNPAN_GYOUSHA_NAME.PopupSearchSendParams")));
            this.UNPAN_GYOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UNPAN_GYOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNPAN_GYOUSHA_NAME.popupWindowSetting")));
            this.UNPAN_GYOUSHA_NAME.ReadOnly = true;
            this.UNPAN_GYOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_NAME.RegistCheckMethod")));
            this.UNPAN_GYOUSHA_NAME.Selectable = false;
            this.UNPAN_GYOUSHA_NAME.ShortItemName = "運搬業者名";
            this.UNPAN_GYOUSHA_NAME.Size = new System.Drawing.Size(150, 21);
            cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UNPAN_GYOUSHA_NAME.Style = cellStyle2;
            this.UNPAN_GYOUSHA_NAME.TabIndex = 1;
            this.UNPAN_GYOUSHA_NAME.TabStop = false;
            this.UNPAN_GYOUSHA_NAME.Tag = "運搬業者名が表示されます";
            // 
            // UNPAN_GYOUSHA_CD
            // 
            this.UNPAN_GYOUSHA_CD.ChangeUpperCase = true;
            this.UNPAN_GYOUSHA_CD.CharacterLimitList = null;
            this.UNPAN_GYOUSHA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.UNPAN_GYOUSHA_CD.DataField = "UNPAN_GYOUSHA_CD";
            this.UNPAN_GYOUSHA_CD.DBFieldsName = "GYOUSHA_CD";
            this.UNPAN_GYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNPAN_GYOUSHA_CD.DisplayItemName = "運搬業者CD";
            this.UNPAN_GYOUSHA_CD.DisplayPopUp = null;
            this.UNPAN_GYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_CD.FocusOutCheckMethod")));
            this.UNPAN_GYOUSHA_CD.GetCodeMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GYOUSHA_ADDRESS1";
            this.UNPAN_GYOUSHA_CD.IsInputErrorOccured = false;
            this.UNPAN_GYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.UNPAN_GYOUSHA_CD.Location = new System.Drawing.Point(0, 0);
            this.UNPAN_GYOUSHA_CD.MaxLength = 6;
            this.UNPAN_GYOUSHA_CD.Name = "UNPAN_GYOUSHA_CD";
            this.UNPAN_GYOUSHA_CD.PopupAfterExecute = null;
            this.UNPAN_GYOUSHA_CD.PopupAfterExecuteMethod = "PopupAfterUnpanGyousha";
            this.UNPAN_GYOUSHA_CD.PopupBeforeExecute = null;
            this.UNPAN_GYOUSHA_CD.PopupGetMasterField = "GYOUSHA_CD";
            this.UNPAN_GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNPAN_GYOUSHA_CD.PopupSearchSendParams")));
            this.UNPAN_GYOUSHA_CD.PopupSendParams = new string[] {
        "HAISHUTSU_JIGYOUSHA_CD"};
            this.UNPAN_GYOUSHA_CD.PopupSetFormField = "";
            this.UNPAN_GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.UNPAN_GYOUSHA_CD.PopupWindowName = "検索共通ポップアップ";
            this.UNPAN_GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNPAN_GYOUSHA_CD.popupWindowSetting")));
            this.UNPAN_GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_CD.RegistCheckMethod")));
            this.UNPAN_GYOUSHA_CD.SetFormField = "UNPAN_GYOUSHA_CD,UNPAN_GYOUSHA_NAME,UNPAN_GYOUSHA_ADDRESS1";
            this.UNPAN_GYOUSHA_CD.ShortItemName = "運搬業者CD";
            cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.UNPAN_GYOUSHA_CD.Style = cellStyle1;
            this.UNPAN_GYOUSHA_CD.TabIndex = 0;
            this.UNPAN_GYOUSHA_CD.Tag = "運搬業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.UNPAN_GYOUSHA_CD.ZeroPaddengFlag = true;
            // 
            // KYOKA_SHARYOU_SUU
            // 
            this.KYOKA_SHARYOU_SUU.DataField = "KYOKA_SHARYOU_SUU";
            this.KYOKA_SHARYOU_SUU.DBFieldsName = "KYOKA_SHARYOU_SUU";
            this.KYOKA_SHARYOU_SUU.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOKA_SHARYOU_SUU.DisplayItemName = "許可車輛台数";
            this.KYOKA_SHARYOU_SUU.DisplayPopUp = null;
            this.KYOKA_SHARYOU_SUU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOKA_SHARYOU_SUU.FocusOutCheckMethod")));
            this.KYOKA_SHARYOU_SUU.IsInputErrorOccured = false;
            this.KYOKA_SHARYOU_SUU.Location = new System.Drawing.Point(880, 0);
            this.KYOKA_SHARYOU_SUU.Name = "KYOKA_SHARYOU_SUU";
            this.KYOKA_SHARYOU_SUU.PopupAfterExecute = null;
            this.KYOKA_SHARYOU_SUU.PopupBeforeExecute = null;
            this.KYOKA_SHARYOU_SUU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOKA_SHARYOU_SUU.PopupSearchSendParams")));
            this.KYOKA_SHARYOU_SUU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOKA_SHARYOU_SUU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOKA_SHARYOU_SUU.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.KYOKA_SHARYOU_SUU.RangeSetting = rangeSettingDto1;
            this.KYOKA_SHARYOU_SUU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOKA_SHARYOU_SUU.RegistCheckMethod")));
            this.KYOKA_SHARYOU_SUU.ShortItemName = "";
            this.KYOKA_SHARYOU_SUU.Size = new System.Drawing.Size(100, 21);
            cellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.KYOKA_SHARYOU_SUU.Style = cellStyle3;
            this.KYOKA_SHARYOU_SUU.TabIndex = 5;
            this.KYOKA_SHARYOU_SUU.Tag = "半角6桁以内で入力してください";
            // 
            // UNPAN_TODOUFUKEN_NAME
            // 
            this.UNPAN_TODOUFUKEN_NAME.DataField = "UNPAN_TODOUFUKEN_NAME";
            this.UNPAN_TODOUFUKEN_NAME.DBFieldsName = "TODOUFUKEN_NAME_RYAKU";
            this.UNPAN_TODOUFUKEN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNPAN_TODOUFUKEN_NAME.DisplayItemName = "運搬業者都道府県";
            this.UNPAN_TODOUFUKEN_NAME.DisplayPopUp = null;
            this.UNPAN_TODOUFUKEN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_TODOUFUKEN_NAME.FocusOutCheckMethod")));
            this.UNPAN_TODOUFUKEN_NAME.IsInputErrorOccured = false;
            this.UNPAN_TODOUFUKEN_NAME.ItemDefinedTypes = "varchar";
            this.UNPAN_TODOUFUKEN_NAME.Location = new System.Drawing.Point(230, 0);
            this.UNPAN_TODOUFUKEN_NAME.Name = "UNPAN_TODOUFUKEN_NAME";
            this.UNPAN_TODOUFUKEN_NAME.PopupAfterExecute = null;
            this.UNPAN_TODOUFUKEN_NAME.PopupBeforeExecute = null;
            this.UNPAN_TODOUFUKEN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNPAN_TODOUFUKEN_NAME.PopupSearchSendParams")));
            this.UNPAN_TODOUFUKEN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UNPAN_TODOUFUKEN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNPAN_TODOUFUKEN_NAME.popupWindowSetting")));
            this.UNPAN_TODOUFUKEN_NAME.ReadOnly = true;
            this.UNPAN_TODOUFUKEN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_TODOUFUKEN_NAME.RegistCheckMethod")));
            this.UNPAN_TODOUFUKEN_NAME.Selectable = false;
            this.UNPAN_TODOUFUKEN_NAME.ShortItemName = "運搬業者都道府県";
            this.UNPAN_TODOUFUKEN_NAME.Size = new System.Drawing.Size(70, 21);
            cellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UNPAN_TODOUFUKEN_NAME.Style = cellStyle4;
            this.UNPAN_TODOUFUKEN_NAME.TabIndex = 2;
            this.UNPAN_TODOUFUKEN_NAME.TabStop = false;
            this.UNPAN_TODOUFUKEN_NAME.Tag = "運搬業者都道府県が表示されます";
            // 
            // UNPAN_GYOUSHA_ADDRESS1
            // 
            this.UNPAN_GYOUSHA_ADDRESS1.DataField = "UNPAN_GYOUSHA_ADDRESS1";
            this.UNPAN_GYOUSHA_ADDRESS1.DBFieldsName = "GENBA_ADDRESS1";
            this.UNPAN_GYOUSHA_ADDRESS1.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNPAN_GYOUSHA_ADDRESS1.DisplayItemName = "運搬業者住所1";
            this.UNPAN_GYOUSHA_ADDRESS1.DisplayPopUp = null;
            this.UNPAN_GYOUSHA_ADDRESS1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_ADDRESS1.FocusOutCheckMethod")));
            this.UNPAN_GYOUSHA_ADDRESS1.IsInputErrorOccured = false;
            this.UNPAN_GYOUSHA_ADDRESS1.ItemDefinedTypes = "varchar";
            this.UNPAN_GYOUSHA_ADDRESS1.Location = new System.Drawing.Point(300, 0);
            this.UNPAN_GYOUSHA_ADDRESS1.Name = "UNPAN_GYOUSHA_ADDRESS1";
            this.UNPAN_GYOUSHA_ADDRESS1.PopupAfterExecute = null;
            this.UNPAN_GYOUSHA_ADDRESS1.PopupBeforeExecute = null;
            this.UNPAN_GYOUSHA_ADDRESS1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNPAN_GYOUSHA_ADDRESS1.PopupSearchSendParams")));
            this.UNPAN_GYOUSHA_ADDRESS1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UNPAN_GYOUSHA_ADDRESS1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNPAN_GYOUSHA_ADDRESS1.popupWindowSetting")));
            this.UNPAN_GYOUSHA_ADDRESS1.ReadOnly = true;
            this.UNPAN_GYOUSHA_ADDRESS1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_ADDRESS1.RegistCheckMethod")));
            this.UNPAN_GYOUSHA_ADDRESS1.Selectable = false;
            this.UNPAN_GYOUSHA_ADDRESS1.ShortItemName = "運搬業者住所1";
            this.UNPAN_GYOUSHA_ADDRESS1.Size = new System.Drawing.Size(290, 21);
            cellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UNPAN_GYOUSHA_ADDRESS1.Style = cellStyle5;
            this.UNPAN_GYOUSHA_ADDRESS1.TabIndex = 3;
            this.UNPAN_GYOUSHA_ADDRESS1.TabStop = false;
            this.UNPAN_GYOUSHA_ADDRESS1.Tag = "運搬業者住所1が表示されます";
            // 
            // UNPAN_GYOUSHA_ADDRESS2
            // 
            this.UNPAN_GYOUSHA_ADDRESS2.DataField = "UNPAN_GYOUSHA_ADDRESS2";
            this.UNPAN_GYOUSHA_ADDRESS2.DBFieldsName = "GENBA_ADDRESS2";
            this.UNPAN_GYOUSHA_ADDRESS2.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNPAN_GYOUSHA_ADDRESS2.DisplayItemName = "運搬業者住所2";
            this.UNPAN_GYOUSHA_ADDRESS2.DisplayPopUp = null;
            this.UNPAN_GYOUSHA_ADDRESS2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_ADDRESS2.FocusOutCheckMethod")));
            this.UNPAN_GYOUSHA_ADDRESS2.IsInputErrorOccured = false;
            this.UNPAN_GYOUSHA_ADDRESS2.ItemDefinedTypes = "varchar";
            this.UNPAN_GYOUSHA_ADDRESS2.Location = new System.Drawing.Point(590, 0);
            this.UNPAN_GYOUSHA_ADDRESS2.Name = "UNPAN_GYOUSHA_ADDRESS2";
            this.UNPAN_GYOUSHA_ADDRESS2.PopupAfterExecute = null;
            this.UNPAN_GYOUSHA_ADDRESS2.PopupBeforeExecute = null;
            this.UNPAN_GYOUSHA_ADDRESS2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNPAN_GYOUSHA_ADDRESS2.PopupSearchSendParams")));
            this.UNPAN_GYOUSHA_ADDRESS2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UNPAN_GYOUSHA_ADDRESS2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNPAN_GYOUSHA_ADDRESS2.popupWindowSetting")));
            this.UNPAN_GYOUSHA_ADDRESS2.ReadOnly = true;
            this.UNPAN_GYOUSHA_ADDRESS2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_ADDRESS2.RegistCheckMethod")));
            this.UNPAN_GYOUSHA_ADDRESS2.Selectable = false;
            this.UNPAN_GYOUSHA_ADDRESS2.ShortItemName = "運搬業者住所2";
            this.UNPAN_GYOUSHA_ADDRESS2.Size = new System.Drawing.Size(290, 21);
            cellStyle6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UNPAN_GYOUSHA_ADDRESS2.Style = cellStyle6;
            this.UNPAN_GYOUSHA_ADDRESS2.TabIndex = 4;
            this.UNPAN_GYOUSHA_ADDRESS2.TabStop = false;
            this.UNPAN_GYOUSHA_ADDRESS2.Tag = "運搬業者住所2が表示されます";
            // 
            // ItakuKeiyakuBetsu2Kenpai
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 41;
            this.Width = 980;

        }

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader1;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader2;
        internal r_framework.CustomControl.GcCustomAlphaNumTextBoxCell UNPAN_GYOUSHA_CD;
        internal r_framework.CustomControl.GcCustomTextBoxCell UNPAN_GYOUSHA_NAME;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader4;
        internal r_framework.CustomControl.GcCustomNumericTextBox2Cell KYOKA_SHARYOU_SUU;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader5;
        internal r_framework.CustomControl.GcCustomTextBoxCell UNPAN_TODOUFUKEN_NAME;
        private r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell3;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader3;
        internal r_framework.CustomControl.GcCustomTextBoxCell UNPAN_GYOUSHA_ADDRESS1;
        internal r_framework.CustomControl.GcCustomTextBoxCell UNPAN_GYOUSHA_ADDRESS2;
    }
}
