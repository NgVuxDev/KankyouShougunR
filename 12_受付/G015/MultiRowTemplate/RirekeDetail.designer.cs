namespace Shougun.Core.Reception.UketsukeSyuusyuuNyuuryoku
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class RirekeDetail
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
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RirekeDetail));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.CELL_RIREKE_SHITA1 = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.SYSTEM_ID = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.CELL_RIREKE_SS = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.CELL_RIREKI_KBN = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.CELL_RIREKE_SHITA2 = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.CELL_RIREKE_SHITA3 = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.CELL_RIREKE_SHITA4 = new r_framework.CustomControl.GcCustomTextBoxCell();
            // 
            // Row
            // 
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Medium, System.Drawing.Color.Black);
            this.Row.Border = border1;
            this.Row.Cells.Add(this.CELL_RIREKE_SHITA1);
            this.Row.Cells.Add(this.CELL_RIREKE_SS);
            this.Row.Cells.Add(this.SYSTEM_ID);
            this.Row.Cells.Add(this.CELL_RIREKI_KBN);
            this.Row.Cells.Add(this.CELL_RIREKE_SHITA2);
            this.Row.Cells.Add(this.CELL_RIREKE_SHITA3);
            this.Row.Cells.Add(this.CELL_RIREKE_SHITA4);
            this.Row.Height = 49;
            this.Row.Width = 162;
            // 
            // columnHeaderSection1
            // 
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Black);
            this.columnHeaderSection1.Border = border2;
            this.columnHeaderSection1.Height = 1;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 162;
            // 
            // CELL_RIREKE_SHITA1
            // 
            this.CELL_RIREKE_SHITA1.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.CELL_RIREKE_SHITA1.DBFieldsName = "";
            this.CELL_RIREKE_SHITA1.DefaultBackColor = System.Drawing.Color.Empty;
            this.CELL_RIREKE_SHITA1.DisplayItemName = "";
            this.CELL_RIREKE_SHITA1.DisplayPopUp = null;
            this.CELL_RIREKE_SHITA1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CELL_RIREKE_SHITA1.FocusOutCheckMethod")));
            this.CELL_RIREKE_SHITA1.IsInputErrorOccured = false;
            this.CELL_RIREKE_SHITA1.ItemDefinedTypes = "varchar";
            this.CELL_RIREKE_SHITA1.Location = new System.Drawing.Point(0, 21);
            this.CELL_RIREKE_SHITA1.MaxLength = 40;
            this.CELL_RIREKE_SHITA1.Name = "CELL_RIREKE_SHITA1";
            this.CELL_RIREKE_SHITA1.PopupAfterExecute = null;
            this.CELL_RIREKE_SHITA1.PopupBeforeExecute = null;
            this.CELL_RIREKE_SHITA1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CELL_RIREKE_SHITA1.PopupSearchSendParams")));
            this.CELL_RIREKE_SHITA1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CELL_RIREKE_SHITA1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CELL_RIREKE_SHITA1.popupWindowSetting")));
            this.CELL_RIREKE_SHITA1.ReadOnly = true;
            this.CELL_RIREKE_SHITA1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CELL_RIREKE_SHITA1.RegistCheckMethod")));
            this.CELL_RIREKE_SHITA1.Size = new System.Drawing.Size(162, 28);
            cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            cellStyle1.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle1.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            cellStyle1.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.TopLeft;
            this.CELL_RIREKE_SHITA1.Style = cellStyle1;
            this.CELL_RIREKE_SHITA1.TabIndex = 353;
            this.CELL_RIREKE_SHITA1.TabStop = false;
            this.CELL_RIREKE_SHITA1.Tag = " ";
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
            // CELL_RIREKE_SS
            // 
            this.CELL_RIREKE_SS.AutoChangeBackColorEnabled = false;
            this.CELL_RIREKE_SS.CharactersNumber = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.CELL_RIREKE_SS.DBFieldsName = "";
            this.CELL_RIREKE_SS.DefaultBackColor =System.Drawing.Color.Empty;
            this.CELL_RIREKE_SS.DisplayItemName = "";
            this.CELL_RIREKE_SS.DisplayPopUp = null;
            this.CELL_RIREKE_SS.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CELL_RIREKE_SS.FocusOutCheckMethod")));
            this.CELL_RIREKE_SS.IsInputErrorOccured = false;
            this.CELL_RIREKE_SS.ItemDefinedTypes = "varchar";
            this.CELL_RIREKE_SS.Location = new System.Drawing.Point(0, 0);
            this.CELL_RIREKE_SS.MaxLength = 30;
            this.CELL_RIREKE_SS.Name = "CELL_RIREKE_SS";
            this.CELL_RIREKE_SS.PopupAfterExecute = null;
            this.CELL_RIREKE_SS.PopupBeforeExecute = null;
            this.CELL_RIREKE_SS.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CELL_RIREKE_SS.PopupSearchSendParams")));
            this.CELL_RIREKE_SS.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CELL_RIREKE_SS.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CELL_RIREKE_SS.popupWindowSetting")));
            this.CELL_RIREKE_SS.ReadOnly = false;
            this.CELL_RIREKE_SS.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CELL_RIREKE_SS.RegistCheckMethod")));
            this.CELL_RIREKE_SS.Size = new System.Drawing.Size(162, 21);
            cellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle2.ForeColor = System.Drawing.Color.White;
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            cellStyle2.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            cellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.CELL_RIREKE_SS.Style = cellStyle2;
            this.CELL_RIREKE_SS.TabIndex = 354;
            this.CELL_RIREKE_SS.TabStop = false;
            this.CELL_RIREKE_SS.Tag = " ";
            // 
            // CELL_RIREKI_KBN
            // 
            this.CELL_RIREKI_KBN.CharactersNumber = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.CELL_RIREKI_KBN.DBFieldsName = "";
            this.CELL_RIREKI_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.CELL_RIREKI_KBN.DisplayItemName = "";
            this.CELL_RIREKI_KBN.DisplayPopUp = null;
            this.CELL_RIREKI_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CELL_RIREKI_KBN.FocusOutCheckMethod")));
            this.CELL_RIREKI_KBN.IsInputErrorOccured = false;
            this.CELL_RIREKI_KBN.ItemDefinedTypes = "varchar";
            this.CELL_RIREKI_KBN.Location = new System.Drawing.Point(0, 0);
            this.CELL_RIREKI_KBN.MaxLength = 4;
            this.CELL_RIREKI_KBN.Name = "CELL_RIREKI_KBN";
            this.CELL_RIREKI_KBN.PopupAfterExecute = null;
            this.CELL_RIREKI_KBN.PopupBeforeExecute = null;
            this.CELL_RIREKI_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CELL_RIREKI_KBN.PopupSearchSendParams")));
            this.CELL_RIREKI_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CELL_RIREKI_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CELL_RIREKI_KBN.popupWindowSetting")));
            this.CELL_RIREKI_KBN.ReadOnly = true;
            this.CELL_RIREKI_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CELL_RIREKI_KBN.RegistCheckMethod")));
            this.CELL_RIREKI_KBN.Size = new System.Drawing.Size(36, 21);
            cellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            cellStyle4.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.CELL_RIREKI_KBN.Style = cellStyle4;
            this.CELL_RIREKI_KBN.TabIndex = 359;
            this.CELL_RIREKI_KBN.TabStop = false;
            this.CELL_RIREKI_KBN.Tag = " ";
            this.CELL_RIREKI_KBN.Value = "KBN";
            this.CELL_RIREKI_KBN.Visible = false;
            // 
            // CELL_RIREKE_SHITA2
            // 
            this.CELL_RIREKE_SHITA2.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.CELL_RIREKE_SHITA2.DBFieldsName = "";
            this.CELL_RIREKE_SHITA2.DefaultBackColor = System.Drawing.Color.Empty;
            this.CELL_RIREKE_SHITA2.DisplayItemName = "";
            this.CELL_RIREKE_SHITA2.DisplayPopUp = null;
            this.CELL_RIREKE_SHITA2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CELL_RIREKE_SHITA2.FocusOutCheckMethod")));
            this.CELL_RIREKE_SHITA2.IsInputErrorOccured = false;
            this.CELL_RIREKE_SHITA2.ItemDefinedTypes = "varchar";
            this.CELL_RIREKE_SHITA2.Location = new System.Drawing.Point(0, 49);
            this.CELL_RIREKE_SHITA2.MaxLength = 40;
            this.CELL_RIREKE_SHITA2.Name = "CELL_RIREKE_SHITA2";
            this.CELL_RIREKE_SHITA2.PopupAfterExecute = null;
            this.CELL_RIREKE_SHITA2.PopupBeforeExecute = null;
            this.CELL_RIREKE_SHITA2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CELL_RIREKE_SHITA2.PopupSearchSendParams")));
            this.CELL_RIREKE_SHITA2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CELL_RIREKE_SHITA2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CELL_RIREKE_SHITA2.popupWindowSetting")));
            this.CELL_RIREKE_SHITA2.ReadOnly = true;
            this.CELL_RIREKE_SHITA2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CELL_RIREKE_SHITA2.RegistCheckMethod")));
            this.CELL_RIREKE_SHITA2.Size = new System.Drawing.Size(162, 28);
            cellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            cellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle5.ForeColor = System.Drawing.Color.Black;
            cellStyle5.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            cellStyle5.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle5.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            cellStyle5.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            cellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            cellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            cellStyle5.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.TopLeft;
            this.CELL_RIREKE_SHITA2.Style = cellStyle5;
            this.CELL_RIREKE_SHITA2.TabIndex = 360;
            this.CELL_RIREKE_SHITA2.TabStop = false;
            this.CELL_RIREKE_SHITA2.Tag = " ";
            // 
            // CELL_RIREKE_SHITA3
            // 
            this.CELL_RIREKE_SHITA3.CharactersNumber = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.CELL_RIREKE_SHITA3.DBFieldsName = "";
            this.CELL_RIREKE_SHITA3.DefaultBackColor = System.Drawing.Color.Empty;
            this.CELL_RIREKE_SHITA3.DisplayItemName = "";
            this.CELL_RIREKE_SHITA3.DisplayPopUp = null;
            this.CELL_RIREKE_SHITA3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CELL_RIREKE_SHITA3.FocusOutCheckMethod")));
            this.CELL_RIREKE_SHITA3.IsInputErrorOccured = false;
            this.CELL_RIREKE_SHITA3.ItemDefinedTypes = "varchar";
            this.CELL_RIREKE_SHITA3.Location = new System.Drawing.Point(0, 77);
            this.CELL_RIREKE_SHITA3.MaxLength = 500;
            this.CELL_RIREKE_SHITA3.Name = "CELL_RIREKE_SHITA3";
            this.CELL_RIREKE_SHITA3.PopupAfterExecute = null;
            this.CELL_RIREKE_SHITA3.PopupBeforeExecute = null;
            this.CELL_RIREKE_SHITA3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CELL_RIREKE_SHITA3.PopupSearchSendParams")));
            this.CELL_RIREKE_SHITA3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CELL_RIREKE_SHITA3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CELL_RIREKE_SHITA3.popupWindowSetting")));
            this.CELL_RIREKE_SHITA3.ReadOnly = true;
            this.CELL_RIREKE_SHITA3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CELL_RIREKE_SHITA3.RegistCheckMethod")));
            this.CELL_RIREKE_SHITA3.Size = new System.Drawing.Size(162, 28);
            cellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            cellStyle6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle6.ForeColor = System.Drawing.Color.Black;
            cellStyle6.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            cellStyle6.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle6.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            cellStyle6.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            cellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            cellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            cellStyle6.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.TopLeft;
            this.CELL_RIREKE_SHITA3.Style = cellStyle6;
            this.CELL_RIREKE_SHITA3.TabIndex = 361;
            this.CELL_RIREKE_SHITA3.TabStop = false;
            this.CELL_RIREKE_SHITA3.Tag = " ";
            // 
            // CELL_RIREKE_SHITA4
            // 
            this.CELL_RIREKE_SHITA4.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.CELL_RIREKE_SHITA4.DBFieldsName = "";
            this.CELL_RIREKE_SHITA4.DefaultBackColor = System.Drawing.Color.Empty;
            this.CELL_RIREKE_SHITA4.DisplayItemName = "";
            this.CELL_RIREKE_SHITA4.DisplayPopUp = null;
            this.CELL_RIREKE_SHITA4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CELL_RIREKE_SHITA4.FocusOutCheckMethod")));
            this.CELL_RIREKE_SHITA4.IsInputErrorOccured = false;
            this.CELL_RIREKE_SHITA4.ItemDefinedTypes = "varchar";
            this.CELL_RIREKE_SHITA4.Location = new System.Drawing.Point(0, 105);
            this.CELL_RIREKE_SHITA4.MaxLength = 40;
            this.CELL_RIREKE_SHITA4.Name = "CELL_RIREKE_SHITA4";
            this.CELL_RIREKE_SHITA4.PopupAfterExecute = null;
            this.CELL_RIREKE_SHITA4.PopupBeforeExecute = null;
            this.CELL_RIREKE_SHITA4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CELL_RIREKE_SHITA4.PopupSearchSendParams")));
            this.CELL_RIREKE_SHITA4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CELL_RIREKE_SHITA4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CELL_RIREKE_SHITA4.popupWindowSetting")));
            this.CELL_RIREKE_SHITA4.ReadOnly = true;
            this.CELL_RIREKE_SHITA4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CELL_RIREKE_SHITA4.RegistCheckMethod")));
            this.CELL_RIREKE_SHITA4.Size = new System.Drawing.Size(162, 28);
            cellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            cellStyle7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle7.ForeColor = System.Drawing.Color.Black;
            cellStyle7.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            cellStyle7.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle7.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            cellStyle7.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            cellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            cellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            cellStyle7.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.TopLeft;
            this.CELL_RIREKE_SHITA4.Style = cellStyle7;
            this.CELL_RIREKE_SHITA4.TabIndex = 362;
            this.CELL_RIREKE_SHITA4.TabStop = false;
            this.CELL_RIREKE_SHITA4.Tag = " ";
            // 
            // RirekeDetail
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 49;
            this.Width = 162;

        }
        

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        internal r_framework.CustomControl.GcCustomTextBoxCell CELL_RIREKE_SHITA1;
        internal r_framework.CustomControl.GcCustomTextBoxCell SYSTEM_ID;
        internal r_framework.CustomControl.GcCustomTextBoxCell CELL_RIREKE_SS;
        internal r_framework.CustomControl.GcCustomTextBoxCell CELL_RIREKI_KBN;
        internal r_framework.CustomControl.GcCustomTextBoxCell CELL_RIREKE_SHITA2;
        internal r_framework.CustomControl.GcCustomTextBoxCell CELL_RIREKE_SHITA3;
        internal r_framework.CustomControl.GcCustomTextBoxCell CELL_RIREKE_SHITA4;
    }
}
