namespace Shougun.Core.SalesPayment.SyukkaNyuuryoku2.MultiRowTemplate
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class RirekiDetail
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
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RirekiDetail));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.RIREKI_HEADER = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.RIREKI_FOOTER1 = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.SYSTEM_ID = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.RIREKI_FOOTER2 = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.RIREKI_FOOTER3 = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.RIREKI_FOOTER4 = new r_framework.CustomControl.GcCustomTextBoxCell();
            // 
            // Row
            // 
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Medium, System.Drawing.Color.Black);
            this.Row.Border = border1;
            this.Row.Cells.Add(this.RIREKI_FOOTER1);
            this.Row.Cells.Add(this.RIREKI_HEADER);
            this.Row.Cells.Add(this.SYSTEM_ID);
            this.Row.Cells.Add(this.RIREKI_FOOTER2);
            this.Row.Cells.Add(this.RIREKI_FOOTER3);
            this.Row.Cells.Add(this.RIREKI_FOOTER4);
            this.Row.Height = 52;
            this.Row.Width = 155;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Height = 1;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 155;
            // 
            // RIREKI_HEADER
            // 
            this.RIREKI_HEADER.AutoChangeBackColorEnabled = false;
            this.RIREKI_HEADER.DefaultBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(160)))));
            this.RIREKI_HEADER.DisplayPopUp = null;
            this.RIREKI_HEADER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("RIREKI_HEADER.FocusOutCheckMethod")));
            this.RIREKI_HEADER.IsInputErrorOccured = false;
            this.RIREKI_HEADER.Location = new System.Drawing.Point(0, 0);
            this.RIREKI_HEADER.Name = "RIREKI_HEADER";
            this.RIREKI_HEADER.PopupAfterExecute = null;
            this.RIREKI_HEADER.PopupBeforeExecute = null;
            this.RIREKI_HEADER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("RIREKI_HEADER.PopupSearchSendParams")));
            this.RIREKI_HEADER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.RIREKI_HEADER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("RIREKI_HEADER.popupWindowSetting")));
            this.RIREKI_HEADER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("RIREKI_HEADER.RegistCheckMethod")));
            this.RIREKI_HEADER.Size = new System.Drawing.Size(155, 21);
            cellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(160)))));
            cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle2.ForeColor = System.Drawing.Color.White;
            this.RIREKI_HEADER.Style = cellStyle2;
            this.RIREKI_HEADER.TabIndex = 1;
            this.RIREKI_HEADER.TabStop = false;
            this.RIREKI_HEADER.Tag = " ";
            // 
            // RIREKI_FOOTER1
            // 
            this.RIREKI_FOOTER1.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.RIREKI_FOOTER1.DBFieldsName = "";
            this.RIREKI_FOOTER1.DefaultBackColor = System.Drawing.Color.Empty;
            this.RIREKI_FOOTER1.DisplayItemName = "履歴（下）";
            this.RIREKI_FOOTER1.DisplayPopUp = null;
            this.RIREKI_FOOTER1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("RIREKI_FOOTER1.FocusOutCheckMethod")));
            this.RIREKI_FOOTER1.IsInputErrorOccured = false;
            this.RIREKI_FOOTER1.ItemDefinedTypes = "varchar";
            this.RIREKI_FOOTER1.Location = new System.Drawing.Point(0, 21);
            this.RIREKI_FOOTER1.MaxLength = 40;
            this.RIREKI_FOOTER1.Name = "RIREKI_FOOTER1";
            this.RIREKI_FOOTER1.PopupAfterExecute = null;
            this.RIREKI_FOOTER1.PopupBeforeExecute = null;
            this.RIREKI_FOOTER1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("RIREKI_FOOTER1.PopupSearchSendParams")));
            this.RIREKI_FOOTER1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.RIREKI_FOOTER1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("RIREKI_FOOTER1.popupWindowSetting")));
            this.RIREKI_FOOTER1.ReadOnly = true;
            this.RIREKI_FOOTER1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("RIREKI_FOOTER1.RegistCheckMethod")));
            this.RIREKI_FOOTER1.Size = new System.Drawing.Size(155, 31);
            cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            cellStyle1.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle1.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.TopLeft;
            this.RIREKI_FOOTER1.Style = cellStyle1;
            this.RIREKI_FOOTER1.TabIndex = 0;
            this.RIREKI_FOOTER1.Tag = " ";
            // 
            // SYSTEM_ID
            // 
            this.SYSTEM_ID.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.SYSTEM_ID.DataField = "SYSTEM_ID";
            this.SYSTEM_ID.DBFieldsName = "SYSTEM_ID";
            this.SYSTEM_ID.DefaultBackColor = System.Drawing.Color.Empty;
            this.SYSTEM_ID.DisplayItemName = "システムID";
            this.SYSTEM_ID.DisplayPopUp = null;
            this.SYSTEM_ID.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SYSTEM_ID.FocusOutCheckMethod")));
            this.SYSTEM_ID.IsInputErrorOccured = false;
            this.SYSTEM_ID.ItemDefinedTypes = "varchar";
            this.SYSTEM_ID.Location = new System.Drawing.Point(0, 126);
            this.SYSTEM_ID.MaxLength = 40;
            this.SYSTEM_ID.Name = "SYSTEM_ID";
            this.SYSTEM_ID.PopupAfterExecute = null;
            this.SYSTEM_ID.PopupBeforeExecute = null;
            this.SYSTEM_ID.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SYSTEM_ID.PopupSearchSendParams")));
            this.SYSTEM_ID.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SYSTEM_ID.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SYSTEM_ID.popupWindowSetting")));
            this.SYSTEM_ID.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SYSTEM_ID.RegistCheckMethod")));
            this.SYSTEM_ID.Size = new System.Drawing.Size(64, 21);
            cellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            cellStyle3.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.SYSTEM_ID.Style = cellStyle3;
            this.SYSTEM_ID.TabIndex = 358;
            this.SYSTEM_ID.Tag = "";
            this.SYSTEM_ID.Visible = false;
            // 
            // RIREKI_FOOTER2
            // 
            this.RIREKI_FOOTER2.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.RIREKI_FOOTER2.DBFieldsName = "";
            this.RIREKI_FOOTER2.DefaultBackColor = System.Drawing.Color.Empty;
            this.RIREKI_FOOTER2.DisplayItemName = "履歴（下）";
            this.RIREKI_FOOTER2.DisplayPopUp = null;
            this.RIREKI_FOOTER2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("RIREKI_FOOTER2.FocusOutCheckMethod")));
            this.RIREKI_FOOTER2.IsInputErrorOccured = false;
            this.RIREKI_FOOTER2.ItemDefinedTypes = "varchar";
            this.RIREKI_FOOTER2.Location = new System.Drawing.Point(0, 52);
            this.RIREKI_FOOTER2.MaxLength = 40;
            this.RIREKI_FOOTER2.Name = "RIREKI_FOOTER2";
            this.RIREKI_FOOTER2.PopupAfterExecute = null;
            this.RIREKI_FOOTER2.PopupBeforeExecute = null;
            this.RIREKI_FOOTER2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("RIREKI_FOOTER2.PopupSearchSendParams")));
            this.RIREKI_FOOTER2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.RIREKI_FOOTER2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("RIREKI_FOOTER2.popupWindowSetting")));
            this.RIREKI_FOOTER2.ReadOnly = true;
            this.RIREKI_FOOTER2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("RIREKI_FOOTER2.RegistCheckMethod")));
            this.RIREKI_FOOTER2.Size = new System.Drawing.Size(155, 31);
            cellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            cellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle4.ForeColor = System.Drawing.Color.Black;
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            cellStyle4.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle4.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            cellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            cellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.TopLeft;
            this.RIREKI_FOOTER2.Style = cellStyle4;
            this.RIREKI_FOOTER2.TabIndex = 359;
            this.RIREKI_FOOTER2.Tag = " ";
            // 
            // RIREKI_FOOTER3
            // 
            this.RIREKI_FOOTER3.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.RIREKI_FOOTER3.DBFieldsName = "";
            this.RIREKI_FOOTER3.DefaultBackColor = System.Drawing.Color.Empty;
            this.RIREKI_FOOTER3.DisplayItemName = "履歴（下）";
            this.RIREKI_FOOTER3.DisplayPopUp = null;
            this.RIREKI_FOOTER3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("RIREKI_FOOTER3.FocusOutCheckMethod")));
            this.RIREKI_FOOTER3.IsInputErrorOccured = false;
            this.RIREKI_FOOTER3.ItemDefinedTypes = "varchar";
            this.RIREKI_FOOTER3.Location = new System.Drawing.Point(0, 83);
            this.RIREKI_FOOTER3.MaxLength = 40;
            this.RIREKI_FOOTER3.Name = "RIREKI_FOOTER3";
            this.RIREKI_FOOTER3.PopupAfterExecute = null;
            this.RIREKI_FOOTER3.PopupBeforeExecute = null;
            this.RIREKI_FOOTER3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("RIREKI_FOOTER3.PopupSearchSendParams")));
            this.RIREKI_FOOTER3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.RIREKI_FOOTER3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("RIREKI_FOOTER3.popupWindowSetting")));
            this.RIREKI_FOOTER3.ReadOnly = true;
            this.RIREKI_FOOTER3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("RIREKI_FOOTER3.RegistCheckMethod")));
            this.RIREKI_FOOTER3.Size = new System.Drawing.Size(155, 31);
            cellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            cellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle5.ForeColor = System.Drawing.Color.Black;
            cellStyle5.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            cellStyle5.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle5.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            cellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            cellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            cellStyle5.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.TopLeft;
            this.RIREKI_FOOTER3.Style = cellStyle5;
            this.RIREKI_FOOTER3.TabIndex = 360;
            this.RIREKI_FOOTER3.Tag = " ";
            // 
            // RIREKI_FOOTER4
            // 
            this.RIREKI_FOOTER4.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.RIREKI_FOOTER4.DBFieldsName = "";
            this.RIREKI_FOOTER4.DefaultBackColor = System.Drawing.Color.Empty;
            this.RIREKI_FOOTER4.DisplayItemName = "履歴（下）";
            this.RIREKI_FOOTER4.DisplayPopUp = null;
            this.RIREKI_FOOTER4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("RIREKI_FOOTER4.FocusOutCheckMethod")));
            this.RIREKI_FOOTER4.IsInputErrorOccured = false;
            this.RIREKI_FOOTER4.ItemDefinedTypes = "varchar";
            this.RIREKI_FOOTER4.Location = new System.Drawing.Point(0, 114);
            this.RIREKI_FOOTER4.MaxLength = 40;
            this.RIREKI_FOOTER4.Name = "RIREKI_FOOTER4";
            this.RIREKI_FOOTER4.PopupAfterExecute = null;
            this.RIREKI_FOOTER4.PopupBeforeExecute = null;
            this.RIREKI_FOOTER4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("RIREKI_FOOTER4.PopupSearchSendParams")));
            this.RIREKI_FOOTER4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.RIREKI_FOOTER4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("RIREKI_FOOTER4.popupWindowSetting")));
            this.RIREKI_FOOTER4.ReadOnly = true;
            this.RIREKI_FOOTER4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("RIREKI_FOOTER4.RegistCheckMethod")));
            this.RIREKI_FOOTER4.Size = new System.Drawing.Size(155, 31);
            cellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            cellStyle6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle6.ForeColor = System.Drawing.Color.Black;
            cellStyle6.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            cellStyle6.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle6.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            cellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            cellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            cellStyle6.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.TopLeft;
            this.RIREKI_FOOTER4.Style = cellStyle6;
            this.RIREKI_FOOTER4.TabIndex = 361;
            this.RIREKI_FOOTER4.Tag = " ";
            // 
            // RirekiDetail
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 53;
            this.Width = 155;

        }
        

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        internal r_framework.CustomControl.GcCustomTextBoxCell RIREKI_HEADER;
        internal r_framework.CustomControl.GcCustomTextBoxCell RIREKI_FOOTER1;
        internal r_framework.CustomControl.GcCustomTextBoxCell SYSTEM_ID;
        internal r_framework.CustomControl.GcCustomTextBoxCell RIREKI_FOOTER2;
        internal r_framework.CustomControl.GcCustomTextBoxCell RIREKI_FOOTER3;
        internal r_framework.CustomControl.GcCustomTextBoxCell RIREKI_FOOTER4;
        
    }
}
