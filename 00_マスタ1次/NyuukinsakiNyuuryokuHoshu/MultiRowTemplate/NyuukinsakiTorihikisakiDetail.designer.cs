namespace NyuukinsakiNyuuryokuHoshu.MultiRowTemplate
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class NyuukinsakiTorihikisakiDetail
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border4 = new GrapeCity.Win.MultiRow.Border();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NyuukinsakiTorihikisakiDetail));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.gcCustomColumnHeader1 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader2 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader3 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader4 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.TORIHIKISAKI_CD = new r_framework.CustomControl.GcCustomPhoneNumberTextBoxCell();
            this.TORIHIKISAKI_NAME_RYAKU = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.TORIHIKISAKI_ADDRESS = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.TORIHIKISAKI_TEL = new r_framework.CustomControl.GcCustomPhoneNumberTextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.TORIHIKISAKI_CD);
            this.Row.Cells.Add(this.TORIHIKISAKI_NAME_RYAKU);
            this.Row.Cells.Add(this.TORIHIKISAKI_ADDRESS);
            this.Row.Cells.Add(this.TORIHIKISAKI_TEL);
            this.Row.Height = 21;
            this.Row.ReadOnly = true;
            this.Row.Width = 750;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader1);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader2);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader3);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader4);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 750;
            // 
            // gcCustomColumnHeader1
            // 
            this.gcCustomColumnHeader1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader1.Location = new System.Drawing.Point(0, 0);
            this.gcCustomColumnHeader1.Name = "gcCustomColumnHeader1";
            this.gcCustomColumnHeader1.Size = new System.Drawing.Size(70, 22);
            cellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle5.Border = border1;
            cellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle5.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader1.Style = cellStyle5;
            this.gcCustomColumnHeader1.TabIndex = 0;
            this.gcCustomColumnHeader1.Value = "取引先CD";
            this.gcCustomColumnHeader1.ViewSearchItem = false;
            // 
            // gcCustomColumnHeader2
            // 
            this.gcCustomColumnHeader2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader2.Location = new System.Drawing.Point(70, 0);
            this.gcCustomColumnHeader2.Name = "gcCustomColumnHeader2";
            this.gcCustomColumnHeader2.Size = new System.Drawing.Size(290, 22);
            cellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle6.Border = border2;
            cellStyle6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle6.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader2.Style = cellStyle6;
            this.gcCustomColumnHeader2.TabIndex = 0;
            this.gcCustomColumnHeader2.Value = "取引先名";
            this.gcCustomColumnHeader2.ViewSearchItem = false;
            // 
            // gcCustomColumnHeader3
            // 
            this.gcCustomColumnHeader3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader3.Location = new System.Drawing.Point(360, 0);
            this.gcCustomColumnHeader3.Name = "gcCustomColumnHeader3";
            this.gcCustomColumnHeader3.Size = new System.Drawing.Size(290, 22);
            cellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle7.Border = border3;
            cellStyle7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle7.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader3.Style = cellStyle7;
            this.gcCustomColumnHeader3.TabIndex = 0;
            this.gcCustomColumnHeader3.Value = "住所";
            this.gcCustomColumnHeader3.ViewSearchItem = false;
            // 
            // gcCustomColumnHeader4
            // 
            this.gcCustomColumnHeader4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader4.Location = new System.Drawing.Point(650, 0);
            this.gcCustomColumnHeader4.Name = "gcCustomColumnHeader4";
            this.gcCustomColumnHeader4.Size = new System.Drawing.Size(100, 22);
            cellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle8.Border = border4;
            cellStyle8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle8.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader4.Style = cellStyle8;
            this.gcCustomColumnHeader4.TabIndex = 0;
            this.gcCustomColumnHeader4.Value = "電話番号";
            this.gcCustomColumnHeader4.ViewSearchItem = false;
            // 
            // TORIHIKISAKI_CD
            // 
            this.TORIHIKISAKI_CD.CustomFormatSetting = "00";
            this.TORIHIKISAKI_CD.DataField = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.DBFieldsName = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_CD.DisplayItemName = "取引先CD";
            this.TORIHIKISAKI_CD.DisplayPopUp = null;
            this.TORIHIKISAKI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.FocusOutCheckMethod")));
            this.TORIHIKISAKI_CD.FormatSetting = "カスタム";
            this.TORIHIKISAKI_CD.GetCodeMasterField = "";
            this.TORIHIKISAKI_CD.IsInputErrorOccured = false;
            this.TORIHIKISAKI_CD.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_CD.Location = new System.Drawing.Point(0, 0);
            this.TORIHIKISAKI_CD.Name = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.PopupAfterExecute = null;
            this.TORIHIKISAKI_CD.PopupBeforeExecute = null;
            this.TORIHIKISAKI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_CD.PopupSearchSendParams")));
            this.TORIHIKISAKI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_CD.popupWindowSetting")));
            this.TORIHIKISAKI_CD.ReadOnly = true;
            this.TORIHIKISAKI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.RegistCheckMethod")));
            this.TORIHIKISAKI_CD.SetFormField = "";
            this.TORIHIKISAKI_CD.ShortItemName = "取引先CD";
            this.TORIHIKISAKI_CD.Size = new System.Drawing.Size(70, 21);
            cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.TORIHIKISAKI_CD.Style = cellStyle1;
            this.TORIHIKISAKI_CD.TabIndex = 1;
            this.TORIHIKISAKI_CD.Tag = "取引先CDが表示されます";
            // 
            // TORIHIKISAKI_NAME_RYAKU
            // 
            this.TORIHIKISAKI_NAME_RYAKU.CopyAutoSetControl = "";
            this.TORIHIKISAKI_NAME_RYAKU.DataField = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_RYAKU.DBFieldsName = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_NAME_RYAKU.DisplayItemName = "取引先名";
            this.TORIHIKISAKI_NAME_RYAKU.DisplayPopUp = null;
            this.TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU.FuriganaAutoSetControl = "";
            this.TORIHIKISAKI_NAME_RYAKU.GetCodeMasterField = "";
            this.TORIHIKISAKI_NAME_RYAKU.IsInputErrorOccured = false;
            this.TORIHIKISAKI_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_NAME_RYAKU.Location = new System.Drawing.Point(70, 0);
            this.TORIHIKISAKI_NAME_RYAKU.MaxLength = 0;
            this.TORIHIKISAKI_NAME_RYAKU.Name = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_RYAKU.PopupAfterExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU.PopupBeforeExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams")));
            this.TORIHIKISAKI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.popupWindowSetting")));
            this.TORIHIKISAKI_NAME_RYAKU.ReadOnly = true;
            this.TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU.ShortItemName = "取引先名";
            this.TORIHIKISAKI_NAME_RYAKU.Size = new System.Drawing.Size(290, 21);
            cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.TORIHIKISAKI_NAME_RYAKU.Style = cellStyle2;
            this.TORIHIKISAKI_NAME_RYAKU.TabIndex = 2;
            this.TORIHIKISAKI_NAME_RYAKU.Tag = "取引先名が表示されます";
            // 
            // TORIHIKISAKI_ADDRESS
            // 
            this.TORIHIKISAKI_ADDRESS.DataField = "TORIHIKISAKI_ADDRESS1";
            this.TORIHIKISAKI_ADDRESS.DBFieldsName = "TORIHIKISAKI_ADDRESS1";
            this.TORIHIKISAKI_ADDRESS.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_ADDRESS.DisplayItemName = "住所";
            this.TORIHIKISAKI_ADDRESS.DisplayPopUp = null;
            this.TORIHIKISAKI_ADDRESS.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_ADDRESS.FocusOutCheckMethod")));
            this.TORIHIKISAKI_ADDRESS.IsInputErrorOccured = false;
            this.TORIHIKISAKI_ADDRESS.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_ADDRESS.Location = new System.Drawing.Point(360, 0);
            this.TORIHIKISAKI_ADDRESS.MaxLength = 0;
            this.TORIHIKISAKI_ADDRESS.Name = "TORIHIKISAKI_ADDRESS";
            this.TORIHIKISAKI_ADDRESS.PopupAfterExecute = null;
            this.TORIHIKISAKI_ADDRESS.PopupBeforeExecute = null;
            this.TORIHIKISAKI_ADDRESS.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_ADDRESS.PopupSearchSendParams")));
            this.TORIHIKISAKI_ADDRESS.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_ADDRESS.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_ADDRESS.popupWindowSetting")));
            this.TORIHIKISAKI_ADDRESS.ReadOnly = true;
            this.TORIHIKISAKI_ADDRESS.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_ADDRESS.RegistCheckMethod")));
            this.TORIHIKISAKI_ADDRESS.ShortItemName = "住所";
            this.TORIHIKISAKI_ADDRESS.Size = new System.Drawing.Size(290, 21);
            cellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.TORIHIKISAKI_ADDRESS.Style = cellStyle3;
            this.TORIHIKISAKI_ADDRESS.TabIndex = 3;
            this.TORIHIKISAKI_ADDRESS.Tag = "取引先住所が表示されます";
            // 
            // TORIHIKISAKI_TEL
            // 
            this.TORIHIKISAKI_TEL.DataField = "TORIHIKISAKI_TEL";
            this.TORIHIKISAKI_TEL.DBFieldsName = "TORIHIKISAKI_TEL";
            this.TORIHIKISAKI_TEL.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_TEL.DisplayItemName = "電話番号";
            this.TORIHIKISAKI_TEL.DisplayPopUp = null;
            this.TORIHIKISAKI_TEL.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_TEL.FocusOutCheckMethod")));
            this.TORIHIKISAKI_TEL.IsInputErrorOccured = false;
            this.TORIHIKISAKI_TEL.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_TEL.Location = new System.Drawing.Point(650, 0);
            this.TORIHIKISAKI_TEL.MaxLength = 0;
            this.TORIHIKISAKI_TEL.Name = "TORIHIKISAKI_TEL";
            this.TORIHIKISAKI_TEL.PopupAfterExecute = null;
            this.TORIHIKISAKI_TEL.PopupBeforeExecute = null;
            this.TORIHIKISAKI_TEL.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_TEL.PopupSearchSendParams")));
            this.TORIHIKISAKI_TEL.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_TEL.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_TEL.popupWindowSetting")));
            this.TORIHIKISAKI_TEL.ReadOnly = true;
            this.TORIHIKISAKI_TEL.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_TEL.RegistCheckMethod")));
            this.TORIHIKISAKI_TEL.ShortItemName = "電話番号";
            this.TORIHIKISAKI_TEL.Size = new System.Drawing.Size(100, 21);
            cellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.TORIHIKISAKI_TEL.Style = cellStyle4;
            this.TORIHIKISAKI_TEL.TabIndex = 4;
            this.TORIHIKISAKI_TEL.Tag = "取引先電話番号が表示されます";
            // 
            // NyuukinsakiTorihikisakiDetail
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 42;
            this.Width = 750;

        }

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        internal r_framework.CustomControl.GcCustomPhoneNumberTextBoxCell TORIHIKISAKI_CD;
        internal r_framework.CustomControl.GcCustomTextBoxCell TORIHIKISAKI_NAME_RYAKU;
        internal r_framework.CustomControl.GcCustomTextBoxCell TORIHIKISAKI_ADDRESS;
        internal r_framework.CustomControl.GcCustomPhoneNumberTextBoxCell TORIHIKISAKI_TEL;
        internal r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader1;
        internal r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader2;
        internal r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader3;
        internal r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader4;
    }
}
