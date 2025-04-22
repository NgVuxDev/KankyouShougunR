namespace TodouFukenHoshu.MultiRowTemplate
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class TodouFukenHoshuDetail
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle11 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle12 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle13 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle14 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border4 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle15 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border5 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle16 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border6 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle17 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border7 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle18 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border8 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle19 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border9 = new GrapeCity.Win.MultiRow.Border();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.TextLengthValidator textLengthValidator1 = new GrapeCity.Win.MultiRow.TextLengthValidator();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TodouFukenHoshuDetail));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle10 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.columnHeaderCell1 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell2 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell3 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell4 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell5 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell8 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell9 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell10 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell11 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.TODOUFUKEN_CD = new r_framework.CustomControl.GcCustomNumericTextBox2Cell();
            this.TODOUFUKEN_NAME = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.TODOUFUKEN_NAME_FURIGANA = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.CREATE_DATE = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.CREATE_USER = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.DELETE_FLG = new r_framework.CustomControl.GcCustomCheckBoxCell();
            this.UPDATE_DATE = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.UPDATE_USER = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.TODOUFUKEN_BIKOU = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.TODOUFUKEN_NAME_RYAKU = new r_framework.CustomControl.GcCustomTextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.DELETE_FLG);
            this.Row.Cells.Add(this.TODOUFUKEN_CD);
            this.Row.Cells.Add(this.TODOUFUKEN_NAME);
            this.Row.Cells.Add(this.TODOUFUKEN_NAME_FURIGANA);
            this.Row.Cells.Add(this.TODOUFUKEN_BIKOU);
            this.Row.Cells.Add(this.CREATE_USER);
            this.Row.Cells.Add(this.CREATE_DATE);
            this.Row.Cells.Add(this.UPDATE_USER);
            this.Row.Cells.Add(this.UPDATE_DATE);
            this.Row.Cells.Add(this.TODOUFUKEN_NAME_RYAKU);
            this.Row.Height = 21;
            this.Row.Width = 965;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell1);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell2);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell3);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell4);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell5);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell8);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell9);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell10);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell11);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 965;
            // 
            // columnHeaderCell1
            // 
            this.columnHeaderCell1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell1.Location = new System.Drawing.Point(0, 0);
            this.columnHeaderCell1.Name = "columnHeaderCell1";
            this.columnHeaderCell1.Size = new System.Drawing.Size(40, 21);
            cellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle11.Border = border1;
            cellStyle11.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle11.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell1.Style = cellStyle11;
            this.columnHeaderCell1.TabIndex = 1;
            this.columnHeaderCell1.Value = "削除";
            this.columnHeaderCell1.ViewSearchItem = false;
            // 
            // columnHeaderCell2
            // 
            this.columnHeaderCell2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell2.Location = new System.Drawing.Point(40, 0);
            this.columnHeaderCell2.Name = "columnHeaderCell2";
            this.columnHeaderCell2.Size = new System.Drawing.Size(80, 21);
            cellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle12.Border = border2;
            cellStyle12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle12.ForeColor = System.Drawing.Color.White;
            cellStyle12.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.columnHeaderCell2.Style = cellStyle12;
            this.columnHeaderCell2.TabIndex = 2;
            this.columnHeaderCell2.Value = "都道府県CD";
            this.columnHeaderCell2.ViewSearchItem = true;
            // 
            // columnHeaderCell3
            // 
            this.columnHeaderCell3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell3.Location = new System.Drawing.Point(120, 0);
            this.columnHeaderCell3.Name = "columnHeaderCell3";
            this.columnHeaderCell3.Size = new System.Drawing.Size(80, 21);
            cellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle13.Border = border3;
            cellStyle13.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle13.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell3.Style = cellStyle13;
            this.columnHeaderCell3.TabIndex = 3;
            this.columnHeaderCell3.Value = "都道府県名";
            this.columnHeaderCell3.ViewSearchItem = true;
            // 
            // columnHeaderCell4
            // 
            this.columnHeaderCell4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell4.Location = new System.Drawing.Point(200, 0);
            this.columnHeaderCell4.Name = "columnHeaderCell4";
            this.columnHeaderCell4.Size = new System.Drawing.Size(95, 21);
            cellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle14.Border = border4;
            cellStyle14.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle14.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell4.Style = cellStyle14;
            this.columnHeaderCell4.TabIndex = 4;
            this.columnHeaderCell4.Value = "フリガナ";
            this.columnHeaderCell4.ViewSearchItem = true;
            // 
            // columnHeaderCell5
            // 
            this.columnHeaderCell5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell5.Location = new System.Drawing.Point(295, 0);
            this.columnHeaderCell5.Name = "columnHeaderCell5";
            this.columnHeaderCell5.Size = new System.Drawing.Size(150, 21);
            cellStyle15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border5.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle15.Border = border5;
            cellStyle15.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle15.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell5.Style = cellStyle15;
            this.columnHeaderCell5.TabIndex = 5;
            this.columnHeaderCell5.Value = "備考";
            this.columnHeaderCell5.ViewSearchItem = true;
            // 
            // columnHeaderCell8
            // 
            this.columnHeaderCell8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell8.Location = new System.Drawing.Point(445, 0);
            this.columnHeaderCell8.Name = "columnHeaderCell8";
            this.columnHeaderCell8.Size = new System.Drawing.Size(120, 21);
            cellStyle16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border6.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle16.Border = border6;
            cellStyle16.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle16.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell8.Style = cellStyle16;
            this.columnHeaderCell8.TabIndex = 8;
            this.columnHeaderCell8.Value = "作成者";
            this.columnHeaderCell8.ViewSearchItem = true;
            // 
            // columnHeaderCell9
            // 
            this.columnHeaderCell9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell9.Location = new System.Drawing.Point(565, 0);
            this.columnHeaderCell9.Name = "columnHeaderCell9";
            this.columnHeaderCell9.Size = new System.Drawing.Size(140, 21);
            cellStyle17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border7.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle17.Border = border7;
            cellStyle17.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle17.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell9.Style = cellStyle17;
            this.columnHeaderCell9.TabIndex = 9;
            this.columnHeaderCell9.Value = "作成日";
            this.columnHeaderCell9.ViewSearchItem = true;
            // 
            // columnHeaderCell10
            // 
            this.columnHeaderCell10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell10.Location = new System.Drawing.Point(705, 0);
            this.columnHeaderCell10.Name = "columnHeaderCell10";
            this.columnHeaderCell10.Size = new System.Drawing.Size(120, 21);
            cellStyle18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border8.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle18.Border = border8;
            cellStyle18.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle18.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell10.Style = cellStyle18;
            this.columnHeaderCell10.TabIndex = 10;
            this.columnHeaderCell10.Value = "更新者";
            this.columnHeaderCell10.ViewSearchItem = true;
            // 
            // columnHeaderCell11
            // 
            this.columnHeaderCell11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell11.Location = new System.Drawing.Point(825, 0);
            this.columnHeaderCell11.Name = "columnHeaderCell11";
            this.columnHeaderCell11.Size = new System.Drawing.Size(140, 21);
            cellStyle19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border9.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle19.Border = border9;
            cellStyle19.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle19.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell11.Style = cellStyle19;
            this.columnHeaderCell11.TabIndex = 11;
            this.columnHeaderCell11.Value = "更新日";
            this.columnHeaderCell11.ViewSearchItem = true;
            // 
            // TODOUFUKEN_CD
            // 
            this.TODOUFUKEN_CD.CustomFormatSetting = "00";
            this.TODOUFUKEN_CD.DataField = "TODOUFUKEN_CD";
            this.TODOUFUKEN_CD.DBFieldsName = "TODOUFUKEN_CD";
            this.TODOUFUKEN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.TODOUFUKEN_CD.DisplayItemName = "都道府県CD";
            this.TODOUFUKEN_CD.DisplayPopUp = null;
            this.TODOUFUKEN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TODOUFUKEN_CD.FocusOutCheckMethod")));
            this.TODOUFUKEN_CD.FormatSetting = "カスタム";
            this.TODOUFUKEN_CD.GetCodeMasterField = "";
            this.TODOUFUKEN_CD.IsInputErrorOccured = false;
            this.TODOUFUKEN_CD.ItemDefinedTypes = "smallint";
            this.TODOUFUKEN_CD.Location = new System.Drawing.Point(40, 0);
            this.TODOUFUKEN_CD.Name = "TODOUFUKEN_CD";
            this.TODOUFUKEN_CD.PopupAfterExecute = null;
            this.TODOUFUKEN_CD.PopupBeforeExecute = null;
            this.TODOUFUKEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TODOUFUKEN_CD.PopupSearchSendParams")));
            this.TODOUFUKEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TODOUFUKEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TODOUFUKEN_CD.popupWindowSetting")));
            this.TODOUFUKEN_CD.PrevText = null;
            rangeSettingDto1.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.TODOUFUKEN_CD.RangeSetting = rangeSettingDto1;
            this.TODOUFUKEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TODOUFUKEN_CD.RegistCheckMethod")));
            this.TODOUFUKEN_CD.SetFormField = "";
            this.TODOUFUKEN_CD.ShortItemName = "都道府県CD";
            cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.TODOUFUKEN_CD.Style = cellStyle2;
            this.TODOUFUKEN_CD.TabIndex = 1;
            this.TODOUFUKEN_CD.Tag = "数字2桁以内で入力してください";
            textLengthValidator1.LengthUnit = GrapeCity.Win.MultiRow.LengthUnit.Byte;
            textLengthValidator1.MaximumLength = 5;
            textLengthValidator1.MinimumLength = 1;
            textLengthValidator1.NullIsValid = false;
            this.TODOUFUKEN_CD.Validators.Add(textLengthValidator1);
            // 
            // TODOUFUKEN_NAME
            // 
            this.TODOUFUKEN_NAME.CharactersNumber = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.TODOUFUKEN_NAME.CopyAutoSetControl = "TODOUFUKEN_NAME_RYAKU";
            this.TODOUFUKEN_NAME.DataField = "TODOUFUKEN_NAME";
            this.TODOUFUKEN_NAME.DBFieldsName = "TODOUFUKEN_NAME";
            this.TODOUFUKEN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.TODOUFUKEN_NAME.DisplayItemName = "都道府県名";
            this.TODOUFUKEN_NAME.DisplayPopUp = null;
            this.TODOUFUKEN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TODOUFUKEN_NAME.FocusOutCheckMethod")));
            this.TODOUFUKEN_NAME.FuriganaAutoSetControl = "TODOUFUKEN_NAME_FURIGANA";
            this.TODOUFUKEN_NAME.GetCodeMasterField = "";
            this.TODOUFUKEN_NAME.IsInputErrorOccured = false;
            this.TODOUFUKEN_NAME.ItemDefinedTypes = "varchar";
            this.TODOUFUKEN_NAME.Location = new System.Drawing.Point(120, 0);
            this.TODOUFUKEN_NAME.MaxLength = 4;
            this.TODOUFUKEN_NAME.Name = "TODOUFUKEN_NAME";
            this.TODOUFUKEN_NAME.PopupAfterExecute = null;
            this.TODOUFUKEN_NAME.PopupBeforeExecute = null;
            this.TODOUFUKEN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TODOUFUKEN_NAME.PopupSearchSendParams")));
            this.TODOUFUKEN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TODOUFUKEN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TODOUFUKEN_NAME.popupWindowSetting")));
            this.TODOUFUKEN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TODOUFUKEN_NAME.RegistCheckMethod")));
            this.TODOUFUKEN_NAME.ShortItemName = "都道府県名";
            cellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.TODOUFUKEN_NAME.Style = cellStyle3;
            this.TODOUFUKEN_NAME.TabIndex = 2;
            this.TODOUFUKEN_NAME.Tag = "全角４文字以内で入力してください";
            // 
            // TODOUFUKEN_NAME_FURIGANA
            // 
            this.TODOUFUKEN_NAME_FURIGANA.CharactersNumber = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.TODOUFUKEN_NAME_FURIGANA.DataField = "TODOUFUKEN_NAME_FURIGANA";
            this.TODOUFUKEN_NAME_FURIGANA.DBFieldsName = "TODOUFUKEN_NAME_FURIGANA";
            this.TODOUFUKEN_NAME_FURIGANA.DefaultBackColor = System.Drawing.Color.Empty;
            this.TODOUFUKEN_NAME_FURIGANA.DisplayItemName = "都道府県ふりがな";
            this.TODOUFUKEN_NAME_FURIGANA.DisplayPopUp = null;
            this.TODOUFUKEN_NAME_FURIGANA.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TODOUFUKEN_NAME_FURIGANA.FocusOutCheckMethod")));
            this.TODOUFUKEN_NAME_FURIGANA.IsInputErrorOccured = false;
            this.TODOUFUKEN_NAME_FURIGANA.ItemDefinedTypes = "varchar";
            this.TODOUFUKEN_NAME_FURIGANA.Location = new System.Drawing.Point(200, 0);
            this.TODOUFUKEN_NAME_FURIGANA.MaxLength = 6;
            this.TODOUFUKEN_NAME_FURIGANA.Name = "TODOUFUKEN_NAME_FURIGANA";
            this.TODOUFUKEN_NAME_FURIGANA.PopupAfterExecute = null;
            this.TODOUFUKEN_NAME_FURIGANA.PopupBeforeExecute = null;
            this.TODOUFUKEN_NAME_FURIGANA.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TODOUFUKEN_NAME_FURIGANA.PopupSearchSendParams")));
            this.TODOUFUKEN_NAME_FURIGANA.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TODOUFUKEN_NAME_FURIGANA.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TODOUFUKEN_NAME_FURIGANA.popupWindowSetting")));
            this.TODOUFUKEN_NAME_FURIGANA.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TODOUFUKEN_NAME_FURIGANA.RegistCheckMethod")));
            this.TODOUFUKEN_NAME_FURIGANA.ShortItemName = "ふりがな";
            this.TODOUFUKEN_NAME_FURIGANA.Size = new System.Drawing.Size(95, 21);
            cellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.Katakana;
            this.TODOUFUKEN_NAME_FURIGANA.Style = cellStyle4;
            this.TODOUFUKEN_NAME_FURIGANA.TabIndex = 3;
            this.TODOUFUKEN_NAME_FURIGANA.Tag = "全角６文字以内で入力してください";
            // 
            // CREATE_DATE
            // 
            this.CREATE_DATE.CharactersNumber = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.CREATE_DATE.DataField = "CREATE_DATE";
            this.CREATE_DATE.DBFieldsName = "CREATE_DATE";
            this.CREATE_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            this.CREATE_DATE.DisplayItemName = "作成日時";
            this.CREATE_DATE.DisplayPopUp = null;
            this.CREATE_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_DATE.FocusOutCheckMethod")));
            this.CREATE_DATE.IsInputErrorOccured = false;
            this.CREATE_DATE.ItemDefinedTypes = "dateTime";
            this.CREATE_DATE.Location = new System.Drawing.Point(565, 0);
            this.CREATE_DATE.MaxLength = 10;
            this.CREATE_DATE.Name = "CREATE_DATE";
            this.CREATE_DATE.PopupAfterExecute = null;
            this.CREATE_DATE.PopupBeforeExecute = null;
            this.CREATE_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CREATE_DATE.PopupSearchSendParams")));
            this.CREATE_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_DATE.popupWindowSetting")));
            this.CREATE_DATE.ReadOnly = true;
            this.CREATE_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_DATE.RegistCheckMethod")));
            this.CREATE_DATE.Selectable = false;
            this.CREATE_DATE.ShortItemName = "作成日時";
            this.CREATE_DATE.Size = new System.Drawing.Size(140, 21);
            cellStyle7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle7.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.CREATE_DATE.Style = cellStyle7;
            this.CREATE_DATE.TabIndex = 8;
            this.CREATE_DATE.Tag = "初回作成日が表示されます";
            // 
            // CREATE_USER
            // 
            this.CREATE_USER.CharactersNumber = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.CREATE_USER.DataField = "CREATE_USER";
            this.CREATE_USER.DBFieldsName = "CREATE_USER";
            this.CREATE_USER.DefaultBackColor = System.Drawing.Color.Empty;
            this.CREATE_USER.DisplayItemName = "作成者";
            this.CREATE_USER.DisplayPopUp = null;
            this.CREATE_USER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_USER.FocusOutCheckMethod")));
            this.CREATE_USER.IsInputErrorOccured = false;
            this.CREATE_USER.ItemDefinedTypes = "varchar";
            this.CREATE_USER.Location = new System.Drawing.Point(445, 0);
            this.CREATE_USER.Name = "CREATE_USER";
            this.CREATE_USER.PopupAfterExecute = null;
            this.CREATE_USER.PopupBeforeExecute = null;
            this.CREATE_USER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CREATE_USER.PopupSearchSendParams")));
            this.CREATE_USER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_USER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_USER.popupWindowSetting")));
            this.CREATE_USER.ReadOnly = true;
            this.CREATE_USER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_USER.RegistCheckMethod")));
            this.CREATE_USER.Selectable = false;
            this.CREATE_USER.ShortItemName = "作成者";
            this.CREATE_USER.Size = new System.Drawing.Size(120, 21);
            cellStyle6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CREATE_USER.Style = cellStyle6;
            this.CREATE_USER.TabIndex = 7;
            this.CREATE_USER.Tag = "初回作成者が表示されます";
            // 
            // DELETE_FLG
            // 
            this.DELETE_FLG.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.DELETE_FLG.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.DELETE_FLG.DataField = "DELETE_FLG";
            this.DELETE_FLG.DBFieldsName = "";
            this.DELETE_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.DELETE_FLG.DisplayItemName = null;
            this.DELETE_FLG.ErrorMessage = null;
            this.DELETE_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DELETE_FLG.FocusOutCheckMethod")));
            this.DELETE_FLG.GetCodeMasterField = null;
            this.DELETE_FLG.IsInputErrorOccured = false;
            this.DELETE_FLG.ItemDefinedTypes = null;
            this.DELETE_FLG.Location = new System.Drawing.Point(0, 0);
            this.DELETE_FLG.Name = "DELETE_FLG";
            this.DELETE_FLG.PopupAfterExecute = null;
            this.DELETE_FLG.PopupBeforeExecute = null;
            this.DELETE_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DELETE_FLG.PopupSearchSendParams")));
            this.DELETE_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DELETE_FLG.PopupWindowName = null;
            this.DELETE_FLG.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DELETE_FLG.popupWindowSetting")));
            this.DELETE_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DELETE_FLG.RegistCheckMethod")));
            this.DELETE_FLG.SearchDisplayFlag = 0;
            this.DELETE_FLG.SetFormField = null;
            this.DELETE_FLG.ShortItemName = null;
            this.DELETE_FLG.Size = new System.Drawing.Size(40, 21);
            cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.DELETE_FLG.Style = cellStyle1;
            this.DELETE_FLG.TabIndex = 0;
            this.DELETE_FLG.Tag = "削除する場合にはチェックを付けてください";
            this.DELETE_FLG.ZeroPaddengFlag = false;
            // 
            // UPDATE_DATE
            // 
            this.UPDATE_DATE.CharactersNumber = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.UPDATE_DATE.DataField = "UPDATE_DATE";
            this.UPDATE_DATE.DBFieldsName = "UPDATE_DATE";
            this.UPDATE_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            this.UPDATE_DATE.DisplayItemName = "最終更新日時";
            this.UPDATE_DATE.DisplayPopUp = null;
            this.UPDATE_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_DATE.FocusOutCheckMethod")));
            this.UPDATE_DATE.IsInputErrorOccured = false;
            this.UPDATE_DATE.ItemDefinedTypes = "dateTime";
            this.UPDATE_DATE.Location = new System.Drawing.Point(825, 0);
            this.UPDATE_DATE.MaxLength = 10;
            this.UPDATE_DATE.Name = "UPDATE_DATE";
            this.UPDATE_DATE.PopupAfterExecute = null;
            this.UPDATE_DATE.PopupBeforeExecute = null;
            this.UPDATE_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPDATE_DATE.PopupSearchSendParams")));
            this.UPDATE_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPDATE_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPDATE_DATE.popupWindowSetting")));
            this.UPDATE_DATE.ReadOnly = true;
            this.UPDATE_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_DATE.RegistCheckMethod")));
            this.UPDATE_DATE.Selectable = false;
            this.UPDATE_DATE.ShortItemName = "更新日時";
            this.UPDATE_DATE.Size = new System.Drawing.Size(140, 21);
            cellStyle9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle9.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.UPDATE_DATE.Style = cellStyle9;
            this.UPDATE_DATE.TabIndex = 10;
            this.UPDATE_DATE.Tag = "最終更新日が表示されます";
            // 
            // UPDATE_USER
            // 
            this.UPDATE_USER.CharactersNumber = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.UPDATE_USER.DataField = "UPDATE_USER";
            this.UPDATE_USER.DBFieldsName = "UPDATE_USER";
            this.UPDATE_USER.DefaultBackColor = System.Drawing.Color.Empty;
            this.UPDATE_USER.DisplayItemName = "最終更新者";
            this.UPDATE_USER.DisplayPopUp = null;
            this.UPDATE_USER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_USER.FocusOutCheckMethod")));
            this.UPDATE_USER.IsInputErrorOccured = false;
            this.UPDATE_USER.ItemDefinedTypes = "varchar";
            this.UPDATE_USER.Location = new System.Drawing.Point(705, 0);
            this.UPDATE_USER.MaxLength = 16;
            this.UPDATE_USER.Name = "UPDATE_USER";
            this.UPDATE_USER.PopupAfterExecute = null;
            this.UPDATE_USER.PopupBeforeExecute = null;
            this.UPDATE_USER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPDATE_USER.PopupSearchSendParams")));
            this.UPDATE_USER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPDATE_USER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPDATE_USER.popupWindowSetting")));
            this.UPDATE_USER.ReadOnly = true;
            this.UPDATE_USER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_USER.RegistCheckMethod")));
            this.UPDATE_USER.Selectable = false;
            this.UPDATE_USER.ShortItemName = "更新者";
            this.UPDATE_USER.Size = new System.Drawing.Size(120, 21);
            cellStyle8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UPDATE_USER.Style = cellStyle8;
            this.UPDATE_USER.TabIndex = 9;
            this.UPDATE_USER.Tag = "最終更新者が表示されます";
            // 
            // TODOUFUKEN_BIKOU
            // 
            this.TODOUFUKEN_BIKOU.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.TODOUFUKEN_BIKOU.DataField = "TODOUFUKEN_BIKOU";
            this.TODOUFUKEN_BIKOU.DBFieldsName = "TODOUFUKEN_BIKOU";
            this.TODOUFUKEN_BIKOU.DefaultBackColor = System.Drawing.Color.Empty;
            this.TODOUFUKEN_BIKOU.DisplayItemName = "都道府県備考";
            this.TODOUFUKEN_BIKOU.DisplayPopUp = null;
            this.TODOUFUKEN_BIKOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TODOUFUKEN_BIKOU.FocusOutCheckMethod")));
            this.TODOUFUKEN_BIKOU.IsInputErrorOccured = false;
            this.TODOUFUKEN_BIKOU.ItemDefinedTypes = "varchar";
            this.TODOUFUKEN_BIKOU.Location = new System.Drawing.Point(295, 0);
            this.TODOUFUKEN_BIKOU.MaxLength = 10;
            this.TODOUFUKEN_BIKOU.Name = "TODOUFUKEN_BIKOU";
            this.TODOUFUKEN_BIKOU.PopupAfterExecute = null;
            this.TODOUFUKEN_BIKOU.PopupBeforeExecute = null;
            this.TODOUFUKEN_BIKOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TODOUFUKEN_BIKOU.PopupSearchSendParams")));
            this.TODOUFUKEN_BIKOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TODOUFUKEN_BIKOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TODOUFUKEN_BIKOU.popupWindowSetting")));
            this.TODOUFUKEN_BIKOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TODOUFUKEN_BIKOU.RegistCheckMethod")));
            this.TODOUFUKEN_BIKOU.ShortItemName = "備考";
            this.TODOUFUKEN_BIKOU.Size = new System.Drawing.Size(150, 21);
            cellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle5.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.TODOUFUKEN_BIKOU.Style = cellStyle5;
            this.TODOUFUKEN_BIKOU.TabIndex = 4;
            this.TODOUFUKEN_BIKOU.Tag = "全角１０文字以内で入力してください";
            // 
            // TODOUFUKEN_NAME_RYAKU
            // 
            this.TODOUFUKEN_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.TODOUFUKEN_NAME_RYAKU.CopyAutoSetControl = "";
            this.TODOUFUKEN_NAME_RYAKU.DataField = "TODOUFUKEN_NAME_RYAKU";
            this.TODOUFUKEN_NAME_RYAKU.DBFieldsName = "TODOUFUKEN_NAME_RYAKU";
            this.TODOUFUKEN_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.TODOUFUKEN_NAME_RYAKU.DisplayItemName = "都道府県名略称";
            this.TODOUFUKEN_NAME_RYAKU.DisplayPopUp = null;
            this.TODOUFUKEN_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TODOUFUKEN_NAME_RYAKU.FocusOutCheckMethod")));
            this.TODOUFUKEN_NAME_RYAKU.FuriganaAutoSetControl = "";
            this.TODOUFUKEN_NAME_RYAKU.GetCodeMasterField = "";
            this.TODOUFUKEN_NAME_RYAKU.IsInputErrorOccured = false;
            this.TODOUFUKEN_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.TODOUFUKEN_NAME_RYAKU.Location = new System.Drawing.Point(162, 0);
            this.TODOUFUKEN_NAME_RYAKU.MaxLength = 4;
            this.TODOUFUKEN_NAME_RYAKU.Name = "TODOUFUKEN_NAME_RYAKU";
            this.TODOUFUKEN_NAME_RYAKU.PopupAfterExecute = null;
            this.TODOUFUKEN_NAME_RYAKU.PopupBeforeExecute = null;
            this.TODOUFUKEN_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TODOUFUKEN_NAME_RYAKU.PopupSearchSendParams")));
            this.TODOUFUKEN_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TODOUFUKEN_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TODOUFUKEN_NAME_RYAKU.popupWindowSetting")));
            this.TODOUFUKEN_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TODOUFUKEN_NAME_RYAKU.RegistCheckMethod")));
            this.TODOUFUKEN_NAME_RYAKU.Selectable = false;
            this.TODOUFUKEN_NAME_RYAKU.ShortItemName = "";
            this.TODOUFUKEN_NAME_RYAKU.Size = new System.Drawing.Size(38, 21);
            cellStyle10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle10.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.TODOUFUKEN_NAME_RYAKU.Style = cellStyle10;
            this.TODOUFUKEN_NAME_RYAKU.TabIndex = 11;
            this.TODOUFUKEN_NAME_RYAKU.TabStop = false;
            this.TODOUFUKEN_NAME_RYAKU.Tag = "";
            this.TODOUFUKEN_NAME_RYAKU.Visible = false;
            // 
            // TodouFukenHoshuDetail
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 42;
            this.Width = 965;

        }


        #endregion

        internal GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        internal r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell1;
        internal r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell2;
        internal r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell3;
        internal r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell4;
        internal r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell5;
        internal r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell8;
        internal r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell9;
        internal r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell10;
        internal r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell11;
        internal r_framework.CustomControl.GcCustomTextBoxCell TODOUFUKEN_BIKOU;
        internal r_framework.CustomControl.GcCustomNumericTextBox2Cell TODOUFUKEN_CD;
        internal r_framework.CustomControl.GcCustomTextBoxCell TODOUFUKEN_NAME_FURIGANA;
        internal r_framework.CustomControl.GcCustomTextBoxCell TODOUFUKEN_NAME;
        internal r_framework.CustomControl.GcCustomTextBoxCell UPDATE_USER;
        internal r_framework.CustomControl.GcCustomTextBoxCell UPDATE_DATE;
        internal r_framework.CustomControl.GcCustomTextBoxCell CREATE_USER;
        internal r_framework.CustomControl.GcCustomTextBoxCell CREATE_DATE;
        internal r_framework.CustomControl.GcCustomCheckBoxCell DELETE_FLG;
        internal r_framework.CustomControl.GcCustomTextBoxCell TODOUFUKEN_NAME_RYAKU;

    }
}
