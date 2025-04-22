namespace ItakuKeiyakuHoshu.MultiRowTemplate
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class ItakuKeiyakuSaiseiHinmokuKenpai
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItakuKeiyakuSaiseiHinmokuKenpai));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.gcCustomColumnHeader11 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.売却先名 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.SAISEI_HINMOKU_NAME = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.BAIKYAKUSAKI_NAME = new r_framework.CustomControl.GcCustomTextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.SAISEI_HINMOKU_NAME);
            this.Row.Cells.Add(this.BAIKYAKUSAKI_NAME);
            this.Row.Height = 21;
            this.Row.Width = 618;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader11);
            this.columnHeaderSection1.Cells.Add(this.売却先名);
            this.columnHeaderSection1.Height = 20;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 618;
            // 
            // gcCustomColumnHeader11
            // 
            this.gcCustomColumnHeader11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader11.Location = new System.Drawing.Point(0, 0);
            this.gcCustomColumnHeader11.Name = "gcCustomColumnHeader11";
            this.gcCustomColumnHeader11.Size = new System.Drawing.Size(309, 20);
            cellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle3.Border = border1;
            cellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle3.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader11.Style = cellStyle3;
            this.gcCustomColumnHeader11.TabIndex = 11;
            this.gcCustomColumnHeader11.Value = "再生品目";
            this.gcCustomColumnHeader11.ViewSearchItem = false;
            // 
            // 売却先名
            // 
            this.売却先名.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.売却先名.Location = new System.Drawing.Point(309, 0);
            this.売却先名.Name = "売却先名";
            this.売却先名.Size = new System.Drawing.Size(309, 20);
            cellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle4.Border = border2;
            cellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle4.ForeColor = System.Drawing.Color.White;
            this.売却先名.Style = cellStyle4;
            this.売却先名.TabIndex = 12;
            this.売却先名.Value = "売却先等";
            this.売却先名.ViewSearchItem = false;
            // 
            // SAISEI_HINMOKU_NAME
            // 
            this.SAISEI_HINMOKU_NAME.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.SAISEI_HINMOKU_NAME.DataField = "SAISEI_HINMOKU_NAME";
            this.SAISEI_HINMOKU_NAME.DBFieldsName = "SAISEI_HINMOKU_NAME";
            this.SAISEI_HINMOKU_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.SAISEI_HINMOKU_NAME.DisplayItemName = "再生品目名";
            this.SAISEI_HINMOKU_NAME.DisplayPopUp = null;
            this.SAISEI_HINMOKU_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SAISEI_HINMOKU_NAME.FocusOutCheckMethod")));
            this.SAISEI_HINMOKU_NAME.FormatSetting = "";
            this.SAISEI_HINMOKU_NAME.IsInputErrorOccured = false;
            this.SAISEI_HINMOKU_NAME.Location = new System.Drawing.Point(0, 0);
            this.SAISEI_HINMOKU_NAME.MaxLength = 40;
            this.SAISEI_HINMOKU_NAME.Name = "SAISEI_HINMOKU_NAME";
            this.SAISEI_HINMOKU_NAME.PopupAfterExecute = null;
            this.SAISEI_HINMOKU_NAME.PopupBeforeExecute = null;
            this.SAISEI_HINMOKU_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SAISEI_HINMOKU_NAME.PopupSearchSendParams")));
            this.SAISEI_HINMOKU_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SAISEI_HINMOKU_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SAISEI_HINMOKU_NAME.popupWindowSetting")));
            this.SAISEI_HINMOKU_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SAISEI_HINMOKU_NAME.RegistCheckMethod")));
            this.SAISEI_HINMOKU_NAME.ShortItemName = "";
            this.SAISEI_HINMOKU_NAME.Size = new System.Drawing.Size(309, 21);
            cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.SAISEI_HINMOKU_NAME.Style = cellStyle1;
            this.SAISEI_HINMOKU_NAME.TabIndex = 0;
            this.SAISEI_HINMOKU_NAME.Tag = "20桁以内で入力してください";
            // 
            // BAIKYAKUSAKI_NAME
            // 
            this.BAIKYAKUSAKI_NAME.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.BAIKYAKUSAKI_NAME.DataField = "BAIKYAKUSAKI_NAME";
            this.BAIKYAKUSAKI_NAME.DBFieldsName = "BAIKYAKUSAKI_NAME";
            this.BAIKYAKUSAKI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.BAIKYAKUSAKI_NAME.DisplayItemName = "売却先名";
            this.BAIKYAKUSAKI_NAME.DisplayPopUp = null;
            this.BAIKYAKUSAKI_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BAIKYAKUSAKI_NAME.FocusOutCheckMethod")));
            this.BAIKYAKUSAKI_NAME.FormatSetting = "";
            this.BAIKYAKUSAKI_NAME.IsInputErrorOccured = false;
            this.BAIKYAKUSAKI_NAME.Location = new System.Drawing.Point(309, 0);
            this.BAIKYAKUSAKI_NAME.MaxLength = 40;
            this.BAIKYAKUSAKI_NAME.Name = "BAIKYAKUSAKI_NAME";
            this.BAIKYAKUSAKI_NAME.PopupAfterExecute = null;
            this.BAIKYAKUSAKI_NAME.PopupBeforeExecute = null;
            this.BAIKYAKUSAKI_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BAIKYAKUSAKI_NAME.PopupSearchSendParams")));
            this.BAIKYAKUSAKI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BAIKYAKUSAKI_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BAIKYAKUSAKI_NAME.popupWindowSetting")));
            this.BAIKYAKUSAKI_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BAIKYAKUSAKI_NAME.RegistCheckMethod")));
            this.BAIKYAKUSAKI_NAME.ShortItemName = "";
            this.BAIKYAKUSAKI_NAME.Size = new System.Drawing.Size(309, 21);
            cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.BAIKYAKUSAKI_NAME.Style = cellStyle2;
            this.BAIKYAKUSAKI_NAME.TabIndex = 1;
            this.BAIKYAKUSAKI_NAME.Tag = "20桁以内で入力してください";
            // 
            // ItakuKeiyakuSaiseiHinmokuKenpai
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 41;
            this.Width = 618;

        }


        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader11;
        internal r_framework.CustomControl.GcCustomTextBoxCell SAISEI_HINMOKU_NAME;
        internal r_framework.CustomControl.GcCustomTextBoxCell BAIKYAKUSAKI_NAME;
        private r_framework.CustomControl.GcCustomColumnHeader 売却先名;

    }
}
