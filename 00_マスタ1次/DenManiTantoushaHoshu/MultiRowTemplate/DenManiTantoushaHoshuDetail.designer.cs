namespace DenManiTantoushaHoshu.MultiRowTemplate
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class DenManiTantoushaHoshuDetail
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle10 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle11 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border4 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle12 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border5 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle13 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border6 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle14 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border7 = new GrapeCity.Win.MultiRow.Border();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DenManiTantoushaHoshuDetail));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.columnHeaderCell7 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell1 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell2 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell4 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell9 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell5 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell6 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.TANTOUSHA_CD = new r_framework.CustomControl.GcCustomNumericTextBox2Cell();
            this.TANTOUSHA_NAME = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.CREATE_DATE = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.CREATE_USER = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.UPDATE_DATE = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.UPDATE_USER = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.DELETE_FLG = new r_framework.CustomControl.GcCustomCheckBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.DELETE_FLG);
            this.Row.Cells.Add(this.TANTOUSHA_CD);
            this.Row.Cells.Add(this.TANTOUSHA_NAME);
            this.Row.Cells.Add(this.UPDATE_USER);
            this.Row.Cells.Add(this.UPDATE_DATE);
            this.Row.Cells.Add(this.CREATE_USER);
            this.Row.Cells.Add(this.CREATE_DATE);
            this.Row.Height = 21;
            this.Row.Width = 930;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell7);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell1);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell2);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell4);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell9);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell5);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell6);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 930;
            // 
            // columnHeaderCell7
            // 
            this.columnHeaderCell7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell7.Location = new System.Drawing.Point(0, 0);
            this.columnHeaderCell7.Name = "columnHeaderCell7";
            this.columnHeaderCell7.Size = new System.Drawing.Size(40, 21);
            cellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle8.Border = border1;
            cellStyle8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle8.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell7.Style = cellStyle8;
            this.columnHeaderCell7.TabIndex = 7;
            this.columnHeaderCell7.Value = "削除";
            this.columnHeaderCell7.ViewSearchItem = false;
            // 
            // columnHeaderCell1
            // 
            this.columnHeaderCell1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell1.Location = new System.Drawing.Point(40, 0);
            this.columnHeaderCell1.Name = "columnHeaderCell1";
            this.columnHeaderCell1.Size = new System.Drawing.Size(80, 21);
            cellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle9.Border = border2;
            cellStyle9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle9.ForeColor = System.Drawing.Color.White;
            cellStyle9.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.columnHeaderCell1.Style = cellStyle9;
            this.columnHeaderCell1.TabIndex = 0;
            this.columnHeaderCell1.Value = "担当者CD";
            this.columnHeaderCell1.ViewSearchItem = true;
            // 
            // columnHeaderCell2
            // 
            this.columnHeaderCell2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell2.Location = new System.Drawing.Point(120, 0);
            this.columnHeaderCell2.Name = "columnHeaderCell2";
            this.columnHeaderCell2.Size = new System.Drawing.Size(290, 21);
            cellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle10.Border = border3;
            cellStyle10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle10.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell2.Style = cellStyle10;
            this.columnHeaderCell2.TabIndex = 1;
            this.columnHeaderCell2.Value = "担当者名";
            this.columnHeaderCell2.ViewSearchItem = true;
            // 
            // columnHeaderCell4
            // 
            this.columnHeaderCell4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell4.Location = new System.Drawing.Point(410, 0);
            this.columnHeaderCell4.Name = "columnHeaderCell4";
            this.columnHeaderCell4.Size = new System.Drawing.Size(120, 21);
            cellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle11.Border = border4;
            cellStyle11.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle11.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell4.Style = cellStyle11;
            this.columnHeaderCell4.TabIndex = 9;
            this.columnHeaderCell4.Value = "更新者";
            this.columnHeaderCell4.ViewSearchItem = true;
            // 
            // columnHeaderCell9
            // 
            this.columnHeaderCell9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell9.Location = new System.Drawing.Point(530, 0);
            this.columnHeaderCell9.Name = "columnHeaderCell9";
            this.columnHeaderCell9.Size = new System.Drawing.Size(140, 21);
            cellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border5.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle12.Border = border5;
            cellStyle12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle12.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell9.Style = cellStyle12;
            this.columnHeaderCell9.TabIndex = 10;
            this.columnHeaderCell9.Value = "更新日";
            this.columnHeaderCell9.ViewSearchItem = true;
            // 
            // columnHeaderCell5
            // 
            this.columnHeaderCell5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell5.Location = new System.Drawing.Point(670, 0);
            this.columnHeaderCell5.Name = "columnHeaderCell5";
            this.columnHeaderCell5.Size = new System.Drawing.Size(120, 21);
            cellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border6.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle13.Border = border6;
            cellStyle13.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle13.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell5.Style = cellStyle13;
            this.columnHeaderCell5.TabIndex = 4;
            this.columnHeaderCell5.Value = "作成者";
            this.columnHeaderCell5.ViewSearchItem = true;
            // 
            // columnHeaderCell6
            // 
            this.columnHeaderCell6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell6.Location = new System.Drawing.Point(790, 0);
            this.columnHeaderCell6.Name = "columnHeaderCell6";
            this.columnHeaderCell6.Size = new System.Drawing.Size(140, 21);
            cellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border7.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle14.Border = border7;
            cellStyle14.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle14.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell6.Style = cellStyle14;
            this.columnHeaderCell6.TabIndex = 5;
            this.columnHeaderCell6.Value = "作成日";
            this.columnHeaderCell6.ViewSearchItem = true;
            // 
            // TANTOUSHA_CD
            // 
            this.TANTOUSHA_CD.ChangeUpperCase = true;
            this.TANTOUSHA_CD.CustomFormatSetting = "0000000000";
            this.TANTOUSHA_CD.DataField = "TANTOUSHA_CD";
            this.TANTOUSHA_CD.DBFieldsName = "TANTOUSHA_CD";
            this.TANTOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.TANTOUSHA_CD.DisplayItemName = "担当者CD";
            this.TANTOUSHA_CD.DisplayPopUp = null;
            this.TANTOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TANTOUSHA_CD.FocusOutCheckMethod")));
            this.TANTOUSHA_CD.FormatSetting = "カスタム";
            this.TANTOUSHA_CD.IsInputErrorOccured = false;
            this.TANTOUSHA_CD.ItemDefinedTypes = "varchar";
            this.TANTOUSHA_CD.Location = new System.Drawing.Point(40, 0);
            this.TANTOUSHA_CD.Name = "TANTOUSHA_CD";
            this.TANTOUSHA_CD.PopupAfterExecute = null;
            this.TANTOUSHA_CD.PopupBeforeExecute = null;
            this.TANTOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TANTOUSHA_CD.PopupSearchSendParams")));
            this.TANTOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TANTOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TANTOUSHA_CD.popupWindowSetting")));
            this.TANTOUSHA_CD.PrevText = null;
            rangeSettingDto1.Max = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.TANTOUSHA_CD.RangeSetting = rangeSettingDto1;
            this.TANTOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TANTOUSHA_CD.RegistCheckMethod")));
            this.TANTOUSHA_CD.ShortItemName = "担当者CD";
            cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.TANTOUSHA_CD.Style = cellStyle2;
            this.TANTOUSHA_CD.TabIndex = 1;
            this.TANTOUSHA_CD.Tag = "半角１０文字以内で入力してください";
            // 
            // TANTOUSHA_NAME
            // 
            this.TANTOUSHA_NAME.CharactersNumber = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.TANTOUSHA_NAME.DataField = "TANTOUSHA_NAME";
            this.TANTOUSHA_NAME.DBFieldsName = "TANTOUSHA_NAME";
            this.TANTOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.TANTOUSHA_NAME.DisplayItemName = "担当者名";
            this.TANTOUSHA_NAME.DisplayPopUp = null;
            this.TANTOUSHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TANTOUSHA_NAME.FocusOutCheckMethod")));
            this.TANTOUSHA_NAME.IsInputErrorOccured = false;
            this.TANTOUSHA_NAME.ItemDefinedTypes = "varchar";
            this.TANTOUSHA_NAME.Location = new System.Drawing.Point(120, 0);
            this.TANTOUSHA_NAME.MaxLength = 24;
            this.TANTOUSHA_NAME.Name = "TANTOUSHA_NAME";
            this.TANTOUSHA_NAME.PopupAfterExecute = null;
            this.TANTOUSHA_NAME.PopupBeforeExecute = null;
            this.TANTOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TANTOUSHA_NAME.PopupSearchSendParams")));
            this.TANTOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TANTOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TANTOUSHA_NAME.popupWindowSetting")));
            this.TANTOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TANTOUSHA_NAME.RegistCheckMethod")));
            this.TANTOUSHA_NAME.ShortItemName = "担当者名";
            this.TANTOUSHA_NAME.Size = new System.Drawing.Size(290, 21);
            cellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.TANTOUSHA_NAME.Style = cellStyle3;
            this.TANTOUSHA_NAME.TabIndex = 2;
            this.TANTOUSHA_NAME.Tag = "全角１２文字以内で入力してください";
            // 
            // CREATE_DATE
            // 
            this.CREATE_DATE.DataField = "CREATE_DATE";
            this.CREATE_DATE.DBFieldsName = "CREATE_DATE";
            this.CREATE_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            this.CREATE_DATE.DisplayPopUp = null;
            this.CREATE_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_DATE.FocusOutCheckMethod")));
            this.CREATE_DATE.IsInputErrorOccured = false;
            this.CREATE_DATE.ItemDefinedTypes = "datetime";
            this.CREATE_DATE.Location = new System.Drawing.Point(790, 0);
            this.CREATE_DATE.Name = "CREATE_DATE";
            this.CREATE_DATE.PopupAfterExecute = null;
            this.CREATE_DATE.PopupBeforeExecute = null;
            this.CREATE_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CREATE_DATE.PopupSearchSendParams")));
            this.CREATE_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_DATE.popupWindowSetting")));
            this.CREATE_DATE.ReadOnly = true;
            this.CREATE_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_DATE.RegistCheckMethod")));
            this.CREATE_DATE.Selectable = false;
            this.CREATE_DATE.Size = new System.Drawing.Size(140, 21);
            cellStyle7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle7.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.CREATE_DATE.Style = cellStyle7;
            this.CREATE_DATE.TabIndex = 8;
            this.CREATE_DATE.TabStop = false;
            this.CREATE_DATE.Tag = "初回作成日が表示されます";
            // 
            // CREATE_USER
            // 
            this.CREATE_USER.DataField = "CREATE_USER";
            this.CREATE_USER.DBFieldsName = "CREATE_USER";
            this.CREATE_USER.DefaultBackColor = System.Drawing.Color.Empty;
            this.CREATE_USER.DisplayPopUp = null;
            this.CREATE_USER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_USER.FocusOutCheckMethod")));
            this.CREATE_USER.IsInputErrorOccured = false;
            this.CREATE_USER.ItemDefinedTypes = "varchar";
            this.CREATE_USER.Location = new System.Drawing.Point(670, 0);
            this.CREATE_USER.Name = "CREATE_USER";
            this.CREATE_USER.PopupAfterExecute = null;
            this.CREATE_USER.PopupBeforeExecute = null;
            this.CREATE_USER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CREATE_USER.PopupSearchSendParams")));
            this.CREATE_USER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_USER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_USER.popupWindowSetting")));
            this.CREATE_USER.ReadOnly = true;
            this.CREATE_USER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_USER.RegistCheckMethod")));
            this.CREATE_USER.Selectable = false;
            this.CREATE_USER.Size = new System.Drawing.Size(120, 21);
            cellStyle6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle6.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.CREATE_USER.Style = cellStyle6;
            this.CREATE_USER.TabIndex = 7;
            this.CREATE_USER.TabStop = false;
            this.CREATE_USER.Tag = "初回作成者が表示されます";
            // 
            // UPDATE_DATE
            // 
            this.UPDATE_DATE.DataField = "UPDATE_DATE";
            this.UPDATE_DATE.DBFieldsName = "UPDATE_DATE";
            this.UPDATE_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            this.UPDATE_DATE.DisplayPopUp = null;
            this.UPDATE_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_DATE.FocusOutCheckMethod")));
            this.UPDATE_DATE.IsInputErrorOccured = false;
            this.UPDATE_DATE.ItemDefinedTypes = "datetime";
            this.UPDATE_DATE.Location = new System.Drawing.Point(530, 0);
            this.UPDATE_DATE.Name = "UPDATE_DATE";
            this.UPDATE_DATE.PopupAfterExecute = null;
            this.UPDATE_DATE.PopupBeforeExecute = null;
            this.UPDATE_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPDATE_DATE.PopupSearchSendParams")));
            this.UPDATE_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPDATE_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPDATE_DATE.popupWindowSetting")));
            this.UPDATE_DATE.ReadOnly = true;
            this.UPDATE_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_DATE.RegistCheckMethod")));
            this.UPDATE_DATE.Selectable = false;
            this.UPDATE_DATE.Size = new System.Drawing.Size(140, 21);
            cellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle5.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.UPDATE_DATE.Style = cellStyle5;
            this.UPDATE_DATE.TabIndex = 6;
            this.UPDATE_DATE.TabStop = false;
            this.UPDATE_DATE.Tag = "最終更新日が表示されます";
            // 
            // UPDATE_USER
            // 
            this.UPDATE_USER.DataField = "UPDATE_USER";
            this.UPDATE_USER.DBFieldsName = "UPDATE_USER";
            this.UPDATE_USER.DefaultBackColor = System.Drawing.Color.Empty;
            this.UPDATE_USER.DisplayPopUp = null;
            this.UPDATE_USER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_USER.FocusOutCheckMethod")));
            this.UPDATE_USER.IsInputErrorOccured = false;
            this.UPDATE_USER.ItemDefinedTypes = "varchar";
            this.UPDATE_USER.Location = new System.Drawing.Point(410, 0);
            this.UPDATE_USER.Name = "UPDATE_USER";
            this.UPDATE_USER.PopupAfterExecute = null;
            this.UPDATE_USER.PopupBeforeExecute = null;
            this.UPDATE_USER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPDATE_USER.PopupSearchSendParams")));
            this.UPDATE_USER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPDATE_USER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPDATE_USER.popupWindowSetting")));
            this.UPDATE_USER.ReadOnly = true;
            this.UPDATE_USER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_USER.RegistCheckMethod")));
            this.UPDATE_USER.Selectable = false;
            this.UPDATE_USER.Size = new System.Drawing.Size(120, 21);
            cellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.UPDATE_USER.Style = cellStyle4;
            this.UPDATE_USER.TabIndex = 5;
            this.UPDATE_USER.TabStop = false;
            this.UPDATE_USER.Tag = "最終更新者が表示されます";
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
            this.DELETE_FLG.DisplayItemName = "";
            this.DELETE_FLG.ErrorMessage = null;
            this.DELETE_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DELETE_FLG.FocusOutCheckMethod")));
            this.DELETE_FLG.GetCodeMasterField = null;
            this.DELETE_FLG.IsInputErrorOccured = false;
            this.DELETE_FLG.ItemDefinedTypes = "";
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
            this.DELETE_FLG.ShortItemName = "";
            this.DELETE_FLG.Size = new System.Drawing.Size(40, 21);
            cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.DELETE_FLG.Style = cellStyle1;
            this.DELETE_FLG.TabIndex = 0;
            this.DELETE_FLG.Tag = "削除する場合にはチェックを付けてください";
            this.DELETE_FLG.ZeroPaddengFlag = false;
            // 
            // DenManiTantoushaHoshuDetail
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 42;
            this.Width = 930;

        }


        #endregion

        private r_framework.CustomControl.GcCustomTextBoxCell CREATE_DATE;
        private r_framework.CustomControl.GcCustomTextBoxCell CREATE_USER;
        private r_framework.CustomControl.GcCustomTextBoxCell UPDATE_DATE;
        public r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell1;
        public GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        public r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell2;
        public r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell5;
        public r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell6;
        public r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell4;
        public r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell9;
        internal r_framework.CustomControl.GcCustomNumericTextBox2Cell TANTOUSHA_CD;
        internal r_framework.CustomControl.GcCustomTextBoxCell TANTOUSHA_NAME;
        internal r_framework.CustomControl.GcCustomTextBoxCell UPDATE_USER;
        public r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell7;
        private r_framework.CustomControl.GcCustomCheckBoxCell DELETE_FLG;
    }
}
