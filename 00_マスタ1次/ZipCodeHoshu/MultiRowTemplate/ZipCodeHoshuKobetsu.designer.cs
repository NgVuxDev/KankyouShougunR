namespace ZipCodeHoshu.MultiRowTemplate
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class ZipCodeHoshuKobetsu
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZipCodeHoshuKobetsu));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.ALL_CHECK = new r_framework.CustomControl.GcCustomCheckBoxCell();
            this.columnHeaderCell2 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell1 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell9 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell5 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell3 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.MENU_NAME = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.ADDRESS1 = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.POST = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.CHANGE_FLG = new r_framework.CustomControl.GcCustomCheckBoxCell();
            this.ITEM_NAME = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.ADDRESS2 = new r_framework.CustomControl.GcCustomTextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.CHANGE_FLG);
            this.Row.Cells.Add(this.MENU_NAME);
            this.Row.Cells.Add(this.ITEM_NAME);
            this.Row.Cells.Add(this.POST);
            this.Row.Cells.Add(this.ADDRESS1);
            this.Row.Cells.Add(this.ADDRESS2);
            this.Row.Height = 21;
            this.Row.Width = 1130;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.ALL_CHECK);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell2);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell1);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell9);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell5);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell3);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.ReadOnly = false;
            this.columnHeaderSection1.Selectable = true;
            this.columnHeaderSection1.Width = 1130;
            // 
            // ALL_CHECK
            // 
            this.ALL_CHECK.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ALL_CHECK.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ALL_CHECK.DBFieldsName = null;
            this.ALL_CHECK.DefaultBackColor = System.Drawing.Color.Empty;
            this.ALL_CHECK.DisplayItemName = null;
            this.ALL_CHECK.ErrorMessage = null;
            this.ALL_CHECK.FalseValue = "0";
            this.ALL_CHECK.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ALL_CHECK.FocusOutCheckMethod")));
            this.ALL_CHECK.GetCodeMasterField = null;
            this.ALL_CHECK.IndeterminateValue = "False";
            this.ALL_CHECK.IsInputErrorOccured = false;
            this.ALL_CHECK.ItemDefinedTypes = null;
            this.ALL_CHECK.Location = new System.Drawing.Point(0, 0);
            this.ALL_CHECK.Name = "ALL_CHECK";
            this.ALL_CHECK.PopupAfterExecute = null;
            this.ALL_CHECK.PopupBeforeExecute = null;
            this.ALL_CHECK.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ALL_CHECK.PopupSearchSendParams")));
            this.ALL_CHECK.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ALL_CHECK.PopupWindowName = null;
            this.ALL_CHECK.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ALL_CHECK.popupWindowSetting")));
            this.ALL_CHECK.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ALL_CHECK.RegistCheckMethod")));
            this.ALL_CHECK.SearchDisplayFlag = 0;
            this.ALL_CHECK.SetFormField = null;
            this.ALL_CHECK.ShortItemName = null;
            this.ALL_CHECK.Size = new System.Drawing.Size(45, 21);
            cellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            cellStyle7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle7.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.ALL_CHECK.Style = cellStyle7;
            this.ALL_CHECK.TabIndex = 1;
            this.ALL_CHECK.TabStop = false;
            this.ALL_CHECK.TrueValue = "1";
            this.ALL_CHECK.Value = false;
            this.ALL_CHECK.ZeroPaddengFlag = false;
            // 
            // columnHeaderCell2
            // 
            this.columnHeaderCell2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell2.Location = new System.Drawing.Point(45, 0);
            this.columnHeaderCell2.Name = "columnHeaderCell2";
            this.columnHeaderCell2.Size = new System.Drawing.Size(150, 21);
            cellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle8.Border = border1;
            cellStyle8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle8.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell2.Style = cellStyle8;
            this.columnHeaderCell2.TabIndex = 2;
            this.columnHeaderCell2.Value = "メニュー";
            this.columnHeaderCell2.ViewSearchItem = false;
            // 
            // columnHeaderCell1
            // 
            this.columnHeaderCell1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell1.Location = new System.Drawing.Point(195, 0);
            this.columnHeaderCell1.Name = "columnHeaderCell1";
            this.columnHeaderCell1.Size = new System.Drawing.Size(290, 21);
            cellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle9.Border = border2;
            cellStyle9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle9.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell1.Style = cellStyle9;
            this.columnHeaderCell1.TabIndex = 3;
            this.columnHeaderCell1.Value = "名称";
            this.columnHeaderCell1.ViewSearchItem = false;
            // 
            // columnHeaderCell9
            // 
            this.columnHeaderCell9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell9.Location = new System.Drawing.Point(485, 0);
            this.columnHeaderCell9.Name = "columnHeaderCell9";
            this.columnHeaderCell9.Size = new System.Drawing.Size(65, 21);
            cellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle10.Border = border3;
            cellStyle10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle10.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell9.Style = cellStyle10;
            this.columnHeaderCell9.TabIndex = 4;
            this.columnHeaderCell9.Value = "郵便番号";
            this.columnHeaderCell9.ViewSearchItem = false;
            // 
            // columnHeaderCell5
            // 
            this.columnHeaderCell5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell5.Location = new System.Drawing.Point(550, 0);
            this.columnHeaderCell5.Name = "columnHeaderCell5";
            this.columnHeaderCell5.Size = new System.Drawing.Size(290, 21);
            cellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle11.Border = border4;
            cellStyle11.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle11.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell5.Style = cellStyle11;
            this.columnHeaderCell5.TabIndex = 5;
            this.columnHeaderCell5.Value = "住所1";
            this.columnHeaderCell5.ViewSearchItem = false;
            // 
            // columnHeaderCell3
            // 
            this.columnHeaderCell3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell3.Location = new System.Drawing.Point(840, 0);
            this.columnHeaderCell3.Name = "columnHeaderCell3";
            this.columnHeaderCell3.Size = new System.Drawing.Size(290, 21);
            cellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border5.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle12.Border = border5;
            cellStyle12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle12.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell3.Style = cellStyle12;
            this.columnHeaderCell3.TabIndex = 6;
            this.columnHeaderCell3.Value = "住所2";
            this.columnHeaderCell3.ViewSearchItem = false;
            // 
            // MENU_NAME
            // 
            this.MENU_NAME.DataField = "MENU_NAME";
            this.MENU_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.MENU_NAME.DisplayPopUp = null;
            this.MENU_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MENU_NAME.FocusOutCheckMethod")));
            this.MENU_NAME.IsInputErrorOccured = false;
            this.MENU_NAME.Location = new System.Drawing.Point(45, 0);
            this.MENU_NAME.Name = "MENU_NAME";
            this.MENU_NAME.PopupAfterExecute = null;
            this.MENU_NAME.PopupBeforeExecute = null;
            this.MENU_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MENU_NAME.PopupSearchSendParams")));
            this.MENU_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MENU_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("MENU_NAME.popupWindowSetting")));
            this.MENU_NAME.ReadOnly = true;
            this.MENU_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MENU_NAME.RegistCheckMethod")));
            this.MENU_NAME.Selectable = false;
            this.MENU_NAME.Size = new System.Drawing.Size(150, 21);
            cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MENU_NAME.Style = cellStyle2;
            this.MENU_NAME.TabIndex = 2;
            this.MENU_NAME.TabStop = false;
            this.MENU_NAME.Tag = "メニュー名が表示されます";
            // 
            // ADDRESS1
            // 
            this.ADDRESS1.DataField = "ADDRESS1";
            this.ADDRESS1.DefaultBackColor = System.Drawing.Color.Empty;
            this.ADDRESS1.DisplayPopUp = null;
            this.ADDRESS1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ADDRESS1.FocusOutCheckMethod")));
            this.ADDRESS1.IsInputErrorOccured = false;
            this.ADDRESS1.Location = new System.Drawing.Point(550, 0);
            this.ADDRESS1.Name = "ADDRESS1";
            this.ADDRESS1.PopupAfterExecute = null;
            this.ADDRESS1.PopupBeforeExecute = null;
            this.ADDRESS1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ADDRESS1.PopupSearchSendParams")));
            this.ADDRESS1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ADDRESS1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ADDRESS1.popupWindowSetting")));
            this.ADDRESS1.ReadOnly = true;
            this.ADDRESS1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ADDRESS1.RegistCheckMethod")));
            this.ADDRESS1.Selectable = false;
            this.ADDRESS1.Size = new System.Drawing.Size(290, 21);
            cellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ADDRESS1.Style = cellStyle5;
            this.ADDRESS1.TabIndex = 5;
            this.ADDRESS1.TabStop = false;
            this.ADDRESS1.Tag = "住所１が表示されます";
            // 
            // POST
            // 
            this.POST.DataField = "POST";
            this.POST.DefaultBackColor = System.Drawing.Color.Empty;
            this.POST.DisplayPopUp = null;
            this.POST.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("POST.FocusOutCheckMethod")));
            this.POST.IsInputErrorOccured = false;
            this.POST.Location = new System.Drawing.Point(485, 0);
            this.POST.Name = "POST";
            this.POST.PopupAfterExecute = null;
            this.POST.PopupBeforeExecute = null;
            this.POST.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("POST.PopupSearchSendParams")));
            this.POST.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.POST.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("POST.popupWindowSetting")));
            this.POST.ReadOnly = true;
            this.POST.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("POST.RegistCheckMethod")));
            this.POST.Selectable = false;
            this.POST.Size = new System.Drawing.Size(65, 21);
            cellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.POST.Style = cellStyle4;
            this.POST.TabIndex = 4;
            this.POST.TabStop = false;
            this.POST.Tag = "郵便番号が表示されます";
            // 
            // CHANGE_FLG
            // 
            this.CHANGE_FLG.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CHANGE_FLG.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CHANGE_FLG.DataField = "CHANGE_FLG";
            this.CHANGE_FLG.DBFieldsName = null;
            this.CHANGE_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.CHANGE_FLG.DisplayItemName = null;
            this.CHANGE_FLG.ErrorMessage = null;
            this.CHANGE_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CHANGE_FLG.FocusOutCheckMethod")));
            this.CHANGE_FLG.GetCodeMasterField = null;
            this.CHANGE_FLG.IsInputErrorOccured = false;
            this.CHANGE_FLG.ItemDefinedTypes = null;
            this.CHANGE_FLG.Location = new System.Drawing.Point(0, 0);
            this.CHANGE_FLG.Name = "CHANGE_FLG";
            this.CHANGE_FLG.PopupAfterExecute = null;
            this.CHANGE_FLG.PopupBeforeExecute = null;
            this.CHANGE_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CHANGE_FLG.PopupSearchSendParams")));
            this.CHANGE_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CHANGE_FLG.PopupWindowName = null;
            this.CHANGE_FLG.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CHANGE_FLG.popupWindowSetting")));
            this.CHANGE_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CHANGE_FLG.RegistCheckMethod")));
            this.CHANGE_FLG.SearchDisplayFlag = 0;
            this.CHANGE_FLG.SetFormField = null;
            this.CHANGE_FLG.ShortItemName = null;
            this.CHANGE_FLG.Size = new System.Drawing.Size(45, 21);
            cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.CHANGE_FLG.Style = cellStyle1;
            this.CHANGE_FLG.TabIndex = 1;
            this.CHANGE_FLG.Tag = "登録する場合にはチェックを付けてください";
            this.CHANGE_FLG.Value = false;
            this.CHANGE_FLG.ZeroPaddengFlag = false;
            // 
            // ITEM_NAME
            // 
            this.ITEM_NAME.DataField = "ITEM_NAME";
            this.ITEM_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.ITEM_NAME.DisplayPopUp = null;
            this.ITEM_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ITEM_NAME.FocusOutCheckMethod")));
            this.ITEM_NAME.IsInputErrorOccured = false;
            this.ITEM_NAME.Location = new System.Drawing.Point(195, 0);
            this.ITEM_NAME.Name = "ITEM_NAME";
            this.ITEM_NAME.PopupAfterExecute = null;
            this.ITEM_NAME.PopupBeforeExecute = null;
            this.ITEM_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ITEM_NAME.PopupSearchSendParams")));
            this.ITEM_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ITEM_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ITEM_NAME.popupWindowSetting")));
            this.ITEM_NAME.ReadOnly = true;
            this.ITEM_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ITEM_NAME.RegistCheckMethod")));
            this.ITEM_NAME.Selectable = false;
            this.ITEM_NAME.Size = new System.Drawing.Size(290, 21);
            cellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ITEM_NAME.Style = cellStyle3;
            this.ITEM_NAME.TabIndex = 3;
            this.ITEM_NAME.TabStop = false;
            this.ITEM_NAME.Tag = "名称が表示されます";
            // 
            // ADDRESS2
            // 
            this.ADDRESS2.DataField = "ADDRESS2";
            this.ADDRESS2.DefaultBackColor = System.Drawing.Color.Empty;
            this.ADDRESS2.DisplayPopUp = null;
            this.ADDRESS2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ADDRESS2.FocusOutCheckMethod")));
            this.ADDRESS2.IsInputErrorOccured = false;
            this.ADDRESS2.Location = new System.Drawing.Point(840, 0);
            this.ADDRESS2.Name = "ADDRESS2";
            this.ADDRESS2.PopupAfterExecute = null;
            this.ADDRESS2.PopupBeforeExecute = null;
            this.ADDRESS2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ADDRESS2.PopupSearchSendParams")));
            this.ADDRESS2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ADDRESS2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ADDRESS2.popupWindowSetting")));
            this.ADDRESS2.ReadOnly = true;
            this.ADDRESS2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ADDRESS2.RegistCheckMethod")));
            this.ADDRESS2.Selectable = false;
            this.ADDRESS2.Size = new System.Drawing.Size(290, 21);
            cellStyle6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ADDRESS2.Style = cellStyle6;
            this.ADDRESS2.TabIndex = 6;
            this.ADDRESS2.TabStop = false;
            this.ADDRESS2.Tag = "住所２が表示されます";
            // 
            // ZipCodeHoshuKobetsu
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 42;
            this.Width = 1130;

        }
        

        #endregion

        public GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        public r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell2;
        public r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell5;
        public r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell9;
        public r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell1;
        public r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell3;
        internal r_framework.CustomControl.GcCustomCheckBoxCell ALL_CHECK;
        internal r_framework.CustomControl.GcCustomCheckBoxCell CHANGE_FLG;
        internal r_framework.CustomControl.GcCustomTextBoxCell MENU_NAME;
        internal r_framework.CustomControl.GcCustomTextBoxCell ITEM_NAME;
        internal r_framework.CustomControl.GcCustomTextBoxCell POST;
        internal r_framework.CustomControl.GcCustomTextBoxCell ADDRESS1;
        internal r_framework.CustomControl.GcCustomTextBoxCell ADDRESS2;        
    }
}
