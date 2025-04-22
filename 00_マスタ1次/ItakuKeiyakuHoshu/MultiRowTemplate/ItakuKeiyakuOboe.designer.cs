namespace ItakuKeiyakuHoshu.MultiRowTemplate
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class ItakuKeiyakuOboe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItakuKeiyakuOboe));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.columnHeaderCell1 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell2 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.MEMO = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.UPDATE_DATE = new r_framework.CustomControl.GcCustomDataTime();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.MEMO);
            this.Row.Cells.Add(this.UPDATE_DATE);
            this.Row.Height = 21;
            this.Row.Width = 650;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell1);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell2);
            this.columnHeaderSection1.Height = 20;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 650;
            // 
            // columnHeaderCell1
            // 
            this.columnHeaderCell1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell1.HoverDirection = GrapeCity.Win.MultiRow.HoverDirection.None;
            this.columnHeaderCell1.Location = new System.Drawing.Point(0, 0);
            this.columnHeaderCell1.Name = "columnHeaderCell1";
            this.columnHeaderCell1.Size = new System.Drawing.Size(100, 20);
            cellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle3.Border = border1;
            cellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle3.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell1.Style = cellStyle3;
            this.columnHeaderCell1.TabIndex = 0;
            this.columnHeaderCell1.Value = "更新日";
            this.columnHeaderCell1.ViewSearchItem = false;
            // 
            // columnHeaderCell2
            // 
            this.columnHeaderCell2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell2.HoverDirection = GrapeCity.Win.MultiRow.HoverDirection.None;
            this.columnHeaderCell2.Location = new System.Drawing.Point(100, 0);
            this.columnHeaderCell2.Name = "columnHeaderCell2";
            this.columnHeaderCell2.Size = new System.Drawing.Size(550, 20);
            cellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle4.Border = border2;
            cellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle4.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell2.Style = cellStyle4;
            this.columnHeaderCell2.TabIndex = 1;
            this.columnHeaderCell2.Value = "覚書";
            this.columnHeaderCell2.ViewSearchItem = false;
            // 
            // MEMO
            // 
            this.MEMO.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.MEMO.DataField = "MEMO";
            this.MEMO.DBFieldsName = "MEMO";
            this.MEMO.DefaultBackColor = System.Drawing.Color.Empty;
            this.MEMO.DisplayItemName = "覚書";
            this.MEMO.DisplayPopUp = null;
            this.MEMO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MEMO.FocusOutCheckMethod")));
            this.MEMO.IsInputErrorOccured = false;
            this.MEMO.Location = new System.Drawing.Point(100, 0);
            this.MEMO.MaxLength = 80;
            this.MEMO.Name = "MEMO";
            this.MEMO.PopupAfterExecute = null;
            this.MEMO.PopupBeforeExecute = null;
            this.MEMO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MEMO.PopupSearchSendParams")));
            this.MEMO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MEMO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("MEMO.popupWindowSetting")));
            this.MEMO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MEMO.RegistCheckMethod")));
            this.MEMO.ShortItemName = "覚書";
            this.MEMO.Size = new System.Drawing.Size(550, 21);
            cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.MEMO.Style = cellStyle1;
            this.MEMO.TabIndex = 1;
            this.MEMO.Tag = "全角４０文字以内で入力してください";
            // 
            // UPDATE_DATE
            // 
            this.UPDATE_DATE.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UPDATE_DATE.CustomFormat = "yyyy/MM/dd";
            this.UPDATE_DATE.DataField = "UPDATE_DATE";
            this.UPDATE_DATE.DBFieldsName = "UPDATE_DATE";
            this.UPDATE_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            this.UPDATE_DATE.DisplayPopUp = null;
            this.UPDATE_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_DATE.FocusOutCheckMethod")));
            this.UPDATE_DATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.UPDATE_DATE.IsInputErrorOccured = false;
            this.UPDATE_DATE.ItemDefinedTypes = "datetime";
            this.UPDATE_DATE.Location = new System.Drawing.Point(0, 0);
            this.UPDATE_DATE.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.UPDATE_DATE.Name = "UPDATE_DATE";
            this.UPDATE_DATE.PopupAfterExecute = null;
            this.UPDATE_DATE.PopupBeforeExecute = null;
            this.UPDATE_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPDATE_DATE.PopupSearchSendParams")));
            this.UPDATE_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPDATE_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPDATE_DATE.popupWindowSetting")));
            this.UPDATE_DATE.PrevText = null;
            this.UPDATE_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_DATE.RegistCheckMethod")));
            this.UPDATE_DATE.Size = new System.Drawing.Size(100, 21);
            cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle2.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.UPDATE_DATE.Style = cellStyle2;
            this.UPDATE_DATE.TabIndex = 0;
            this.UPDATE_DATE.Tag = "更新日を入力してください";
            // 
            // ItakuKeiyakuOboe
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 41;
            cellStyle5.SelectionBackColor = System.Drawing.Color.White;
            cellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.RowsDefaultCellStyle = cellStyle5;
            this.Width = 650;

        }

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell1;
        private r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell2;
        internal r_framework.CustomControl.GcCustomTextBoxCell MEMO;
        private r_framework.CustomControl.GcCustomDataTime UPDATE_DATE;
    }
}
