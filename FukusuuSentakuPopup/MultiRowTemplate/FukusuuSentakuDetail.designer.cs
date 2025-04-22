namespace FukusuuSentakuPopup
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class FukusuuSentakuDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FukusuuSentakuDetail));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border4 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle10 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border5 = new GrapeCity.Win.MultiRow.Border();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.gcCustomColumnHeader1 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader2 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.CHECKED = new r_framework.CustomControl.GcCustomCheckBoxCell();
            this.HAIKI_KBN_CD = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.HAIKI_KBN_NAME_RYAKU = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.HAIKI_SHURUI_CD = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.HAIKI_SHURUI_NAME_RYAKU = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.gcCustomColumnHeader3 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader4 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader5 = new r_framework.CustomControl.GcCustomColumnHeader();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.CHECKED);
            this.Row.Cells.Add(this.HAIKI_KBN_CD);
            this.Row.Cells.Add(this.HAIKI_KBN_NAME_RYAKU);
            this.Row.Cells.Add(this.HAIKI_SHURUI_CD);
            this.Row.Cells.Add(this.HAIKI_SHURUI_NAME_RYAKU);
            this.Row.Height = 21;
            this.Row.Width = 425;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader1);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader2);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader3);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader4);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader5);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 425;
            // 
            // gcCustomColumnHeader1
            // 
            this.gcCustomColumnHeader1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader1.Location = new System.Drawing.Point(1, 1);
            this.gcCustomColumnHeader1.Name = "gcCustomColumnHeader1";
            this.gcCustomColumnHeader1.Size = new System.Drawing.Size(24, 20);
            cellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle6.Border = border1;
            cellStyle6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle6.ForeColor = System.Drawing.Color.White;
            cellStyle6.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.gcCustomColumnHeader1.Style = cellStyle6;
            this.gcCustomColumnHeader1.TabIndex = 3;
            this.gcCustomColumnHeader1.Value = "選";
            this.gcCustomColumnHeader1.ViewSearchItem = false;
            // 
            // gcCustomColumnHeader2
            // 
            this.gcCustomColumnHeader2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader2.Location = new System.Drawing.Point(25, 1);
            this.gcCustomColumnHeader2.Name = "gcCustomColumnHeader2";
            this.gcCustomColumnHeader2.Size = new System.Drawing.Size(100, 20);
            cellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle7.Border = border2;
            cellStyle7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle7.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader2.Style = cellStyle7;
            this.gcCustomColumnHeader2.TabIndex = 4;
            this.gcCustomColumnHeader2.Value = "廃棄物区分CD";
            this.gcCustomColumnHeader2.ViewSearchItem = false;
            // 
            // CHECKED
            // 
            this.CHECKED.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.CHECKED.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CHECKED.DataField = "CHECKED";
            this.CHECKED.DBFieldsName = null;
            this.CHECKED.DisplayItemName = null;
            this.CHECKED.ErrorMessage = null;
            this.CHECKED.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CHECKED.FocusOutCheckMethod")));
            this.CHECKED.GetCodeMasterField = null;
            this.CHECKED.ItemDefinedTypes = null;
            this.CHECKED.Location = new System.Drawing.Point(0, 0);
            this.CHECKED.Name = "CHECKED";
            this.CHECKED.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CHECKED.PopupSearchSendParams")));
            this.CHECKED.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CHECKED.PopupWindowName = null;
            this.CHECKED.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CHECKED.popupWindowSetting")));
            this.CHECKED.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CHECKED.RegistCheckMethod")));
            this.CHECKED.SearchDisplayFlag = 0;
            this.CHECKED.SetFormField = null;
            this.CHECKED.ShortItemName = null;
            this.CHECKED.Size = new System.Drawing.Size(24, 21);
            cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle1.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.CHECKED.Style = cellStyle1;
            this.CHECKED.TabIndex = 3;
            this.CHECKED.ZeroPaddengFlag = false;
            // 
            // HAIKI_KBN_CD
            // 
            this.HAIKI_KBN_CD.DataField = "HAIKI_KBN_CD";
            this.HAIKI_KBN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAIKI_KBN_CD.FocusOutCheckMethod")));
            this.HAIKI_KBN_CD.Location = new System.Drawing.Point(24, 0);
            this.HAIKI_KBN_CD.Name = "HAIKI_KBN_CD";
            this.HAIKI_KBN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HAIKI_KBN_CD.PopupSearchSendParams")));
            this.HAIKI_KBN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HAIKI_KBN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HAIKI_KBN_CD.popupWindowSetting")));
            this.HAIKI_KBN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAIKI_KBN_CD.RegistCheckMethod")));
            this.HAIKI_KBN_CD.ShortItemName = "1";
            this.HAIKI_KBN_CD.Size = new System.Drawing.Size(100, 21);
            cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle2.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.HAIKI_KBN_CD.Style = cellStyle2;
            this.HAIKI_KBN_CD.TabIndex = 4;
            // 
            // HAIKI_KBN_NAME_RYAKU
            // 
            this.HAIKI_KBN_NAME_RYAKU.DataField = "HAIKI_KBN_NAME_RYAKU";
            this.HAIKI_KBN_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAIKI_KBN_NAME_RYAKU.FocusOutCheckMethod")));
            this.HAIKI_KBN_NAME_RYAKU.Location = new System.Drawing.Point(125, 0);
            this.HAIKI_KBN_NAME_RYAKU.Name = "HAIKI_KBN_NAME_RYAKU";
            this.HAIKI_KBN_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HAIKI_KBN_NAME_RYAKU.PopupSearchSendParams")));
            this.HAIKI_KBN_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HAIKI_KBN_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HAIKI_KBN_NAME_RYAKU.popupWindowSetting")));
            this.HAIKI_KBN_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAIKI_KBN_NAME_RYAKU.RegistCheckMethod")));
            this.HAIKI_KBN_NAME_RYAKU.ShortItemName = "2";
            this.HAIKI_KBN_NAME_RYAKU.Size = new System.Drawing.Size(100, 21);
            cellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle3.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.HAIKI_KBN_NAME_RYAKU.Style = cellStyle3;
            this.HAIKI_KBN_NAME_RYAKU.TabIndex = 5;
            // 
            // HAIKI_SHURUI_CD
            // 
            this.HAIKI_SHURUI_CD.DataField = "HAIKI_SHURUI_CD";
            this.HAIKI_SHURUI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAIKI_SHURUI_CD.FocusOutCheckMethod")));
            this.HAIKI_SHURUI_CD.Location = new System.Drawing.Point(225, 0);
            this.HAIKI_SHURUI_CD.Name = "HAIKI_SHURUI_CD";
            this.HAIKI_SHURUI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HAIKI_SHURUI_CD.PopupSearchSendParams")));
            this.HAIKI_SHURUI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HAIKI_SHURUI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HAIKI_SHURUI_CD.popupWindowSetting")));
            this.HAIKI_SHURUI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAIKI_SHURUI_CD.RegistCheckMethod")));
            this.HAIKI_SHURUI_CD.ShortItemName = "2";
            this.HAIKI_SHURUI_CD.Size = new System.Drawing.Size(100, 21);
            cellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle4.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.HAIKI_SHURUI_CD.Style = cellStyle4;
            this.HAIKI_SHURUI_CD.TabIndex = 6;
            // 
            // HAIKI_SHURUI_NAME_RYAKU
            // 
            this.HAIKI_SHURUI_NAME_RYAKU.DataField = "HAIKI_SHURUI_NAME_RYAKU";
            this.HAIKI_SHURUI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAIKI_SHURUI_NAME_RYAKU.FocusOutCheckMethod")));
            this.HAIKI_SHURUI_NAME_RYAKU.Location = new System.Drawing.Point(325, 0);
            this.HAIKI_SHURUI_NAME_RYAKU.Name = "HAIKI_SHURUI_NAME_RYAKU";
            this.HAIKI_SHURUI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HAIKI_SHURUI_NAME_RYAKU.PopupSearchSendParams")));
            this.HAIKI_SHURUI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HAIKI_SHURUI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HAIKI_SHURUI_NAME_RYAKU.popupWindowSetting")));
            this.HAIKI_SHURUI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAIKI_SHURUI_NAME_RYAKU.RegistCheckMethod")));
            this.HAIKI_SHURUI_NAME_RYAKU.ShortItemName = "2";
            this.HAIKI_SHURUI_NAME_RYAKU.Size = new System.Drawing.Size(100, 21);
            cellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle5.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle5.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.HAIKI_SHURUI_NAME_RYAKU.Style = cellStyle5;
            this.HAIKI_SHURUI_NAME_RYAKU.TabIndex = 7;
            // 
            // gcCustomColumnHeader3
            // 
            this.gcCustomColumnHeader3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader3.Location = new System.Drawing.Point(125, 1);
            this.gcCustomColumnHeader3.Name = "gcCustomColumnHeader3";
            this.gcCustomColumnHeader3.Size = new System.Drawing.Size(100, 20);
            cellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle8.Border = border3;
            cellStyle8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle8.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader3.Style = cellStyle8;
            this.gcCustomColumnHeader3.TabIndex = 5;
            this.gcCustomColumnHeader3.Value = "廃棄物区分名";
            this.gcCustomColumnHeader3.ViewSearchItem = false;
            // 
            // gcCustomColumnHeader4
            // 
            this.gcCustomColumnHeader4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader4.Location = new System.Drawing.Point(225, 1);
            this.gcCustomColumnHeader4.Name = "gcCustomColumnHeader4";
            this.gcCustomColumnHeader4.Size = new System.Drawing.Size(100, 20);
            cellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle9.Border = border4;
            cellStyle9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle9.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader4.Style = cellStyle9;
            this.gcCustomColumnHeader4.TabIndex = 6;
            this.gcCustomColumnHeader4.Value = "廃棄物種類CD";
            this.gcCustomColumnHeader4.ViewSearchItem = false;
            // 
            // gcCustomColumnHeader5
            // 
            this.gcCustomColumnHeader5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader5.Location = new System.Drawing.Point(325, 1);
            this.gcCustomColumnHeader5.Name = "gcCustomColumnHeader5";
            this.gcCustomColumnHeader5.Size = new System.Drawing.Size(100, 20);
            cellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border5.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle10.Border = border5;
            cellStyle10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle10.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader5.Style = cellStyle10;
            this.gcCustomColumnHeader5.TabIndex = 7;
            this.gcCustomColumnHeader5.Value = "廃棄物種類名";
            this.gcCustomColumnHeader5.ViewSearchItem = false;
            // 
            // FukusuuSentakuDetail
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 42;
            this.Width = 425;

        }


        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private r_framework.CustomControl.GcCustomCheckBoxCell CHECKED;
        private r_framework.CustomControl.GcCustomTextBoxCell HAIKI_KBN_CD;
        private r_framework.CustomControl.GcCustomTextBoxCell HAIKI_KBN_NAME_RYAKU;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader1;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader2;
        private r_framework.CustomControl.GcCustomTextBoxCell HAIKI_SHURUI_CD;
        private r_framework.CustomControl.GcCustomTextBoxCell HAIKI_SHURUI_NAME_RYAKU;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader3;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader4;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader5;

    }
}
