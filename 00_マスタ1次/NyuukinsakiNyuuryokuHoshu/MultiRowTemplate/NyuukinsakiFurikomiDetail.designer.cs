namespace NyuukinsakiNyuuryokuHoshu.MultiRowTemplate
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class NyuukinsakiFurikomiDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NyuukinsakiFurikomiDetail));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.gcCustomColumnHeader1 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomCheckBoxCell1 = new r_framework.CustomControl.GcCustomCheckBoxCell();
            this.DELETE_FLG = new r_framework.CustomControl.GcCustomCheckBoxCell();
            this.FURIKOMI_NAME = new r_framework.CustomControl.GcCustomTextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.DELETE_FLG);
            this.Row.Cells.Add(this.FURIKOMI_NAME);
            this.Row.Height = 21;
            this.Row.Width = 610;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader1);
            this.columnHeaderSection1.Cells.Add(this.gcCustomCheckBoxCell1);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.ReadOnly = false;
            this.columnHeaderSection1.Selectable = true;
            this.columnHeaderSection1.Width = 610;
            // 
            // gcCustomColumnHeader1
            // 
            this.gcCustomColumnHeader1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader1.Location = new System.Drawing.Point(40, 0);
            this.gcCustomColumnHeader1.Name = "gcCustomColumnHeader1";
            this.gcCustomColumnHeader1.Size = new System.Drawing.Size(570, 22);
            cellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle3.Border = border1;
            cellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle3.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader1.Style = cellStyle3;
            this.gcCustomColumnHeader1.TabIndex = 0;
            this.gcCustomColumnHeader1.Value = "フリコミ人名";
            this.gcCustomColumnHeader1.ViewSearchItem = false;
            // 
            // gcCustomCheckBoxCell1
            // 
            this.gcCustomCheckBoxCell1.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.gcCustomCheckBoxCell1.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.gcCustomCheckBoxCell1.DBFieldsName = "";
            this.gcCustomCheckBoxCell1.DefaultBackColor = System.Drawing.Color.Empty;
            this.gcCustomCheckBoxCell1.DisplayItemName = null;
            this.gcCustomCheckBoxCell1.ErrorMessage = null;
            this.gcCustomCheckBoxCell1.FalseValue = "0";
            this.gcCustomCheckBoxCell1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("gcCustomCheckBoxCell1.FocusOutCheckMethod")));
            this.gcCustomCheckBoxCell1.GetCodeMasterField = null;
            this.gcCustomCheckBoxCell1.IndeterminateValue = "False";
            this.gcCustomCheckBoxCell1.IsInputErrorOccured = false;
            this.gcCustomCheckBoxCell1.ItemDefinedTypes = null;
            this.gcCustomCheckBoxCell1.Location = new System.Drawing.Point(0, 0);
            this.gcCustomCheckBoxCell1.Name = "gcCustomCheckBoxCell1";
            this.gcCustomCheckBoxCell1.PopupAfterExecute = null;
            this.gcCustomCheckBoxCell1.PopupBeforeExecute = null;
            this.gcCustomCheckBoxCell1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("gcCustomCheckBoxCell1.PopupSearchSendParams")));
            this.gcCustomCheckBoxCell1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.gcCustomCheckBoxCell1.PopupWindowName = null;
            this.gcCustomCheckBoxCell1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("gcCustomCheckBoxCell1.popupWindowSetting")));
            this.gcCustomCheckBoxCell1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("gcCustomCheckBoxCell1.RegistCheckMethod")));
            this.gcCustomCheckBoxCell1.SearchDisplayFlag = 0;
            this.gcCustomCheckBoxCell1.SetFormField = null;
            this.gcCustomCheckBoxCell1.ShortItemName = null;
            this.gcCustomCheckBoxCell1.Size = new System.Drawing.Size(40, 21);
            cellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Bottom = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            border2.Left = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            border2.Right = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            border2.Top = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle4.Border = border2;
            cellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.gcCustomCheckBoxCell1.Style = cellStyle4;
            this.gcCustomCheckBoxCell1.TabIndex = 1;
            this.gcCustomCheckBoxCell1.TabStop = false;
            this.gcCustomCheckBoxCell1.Tag = "削除する場合にはチェックを付けてください";
            this.gcCustomCheckBoxCell1.TrueValue = "1";
            this.gcCustomCheckBoxCell1.Value = false;
            this.gcCustomCheckBoxCell1.ZeroPaddengFlag = false;
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
            this.DELETE_FLG.DBFieldsName = "DELETE_FLG";
            this.DELETE_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.DELETE_FLG.DisplayItemName = "削除";
            this.DELETE_FLG.ErrorMessage = null;
            this.DELETE_FLG.FalseValue = "0";
            this.DELETE_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DELETE_FLG.FocusOutCheckMethod")));
            this.DELETE_FLG.GetCodeMasterField = null;
            this.DELETE_FLG.IndeterminateValue = "False";
            this.DELETE_FLG.IsInputErrorOccured = false;
            this.DELETE_FLG.ItemDefinedTypes = "bool";
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
            this.DELETE_FLG.ShortItemName = "削除";
            this.DELETE_FLG.Size = new System.Drawing.Size(40, 21);
            cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.DELETE_FLG.Style = cellStyle1;
            this.DELETE_FLG.TabIndex = 0;
            this.DELETE_FLG.TabStop = false;
            this.DELETE_FLG.Tag = "削除する行の場合チェックしてください";
            this.DELETE_FLG.ZeroPaddengFlag = false;
            // 
            // FURIKOMI_NAME
            // 
            this.FURIKOMI_NAME.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.FURIKOMI_NAME.CopyAutoSetControl = "";
            this.FURIKOMI_NAME.DataField = "FURIKOMI_NAME";
            this.FURIKOMI_NAME.DBFieldsName = "FURIKOMI_NAME";
            this.FURIKOMI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.FURIKOMI_NAME.DisplayItemName = "フリコミ人名";
            this.FURIKOMI_NAME.DisplayPopUp = null;
            this.FURIKOMI_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FURIKOMI_NAME.FocusOutCheckMethod")));
            this.FURIKOMI_NAME.FuriganaAutoSetControl = "";
            this.FURIKOMI_NAME.GetCodeMasterField = "";
            this.FURIKOMI_NAME.IsInputErrorOccured = false;
            this.FURIKOMI_NAME.ItemDefinedTypes = "varchar";
            this.FURIKOMI_NAME.Location = new System.Drawing.Point(40, 0);
            this.FURIKOMI_NAME.MaxLength = 80;
            this.FURIKOMI_NAME.Name = "FURIKOMI_NAME";
            this.FURIKOMI_NAME.PopupAfterExecute = null;
            this.FURIKOMI_NAME.PopupBeforeExecute = null;
            this.FURIKOMI_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("FURIKOMI_NAME.PopupSearchSendParams")));
            this.FURIKOMI_NAME.PopupSendParams = new string[0];
            this.FURIKOMI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.FURIKOMI_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("FURIKOMI_NAME.popupWindowSetting")));
            this.FURIKOMI_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FURIKOMI_NAME.RegistCheckMethod")));
            this.FURIKOMI_NAME.SetFormField = "";
            this.FURIKOMI_NAME.ShortItemName = "フリコミ人名";
            this.FURIKOMI_NAME.Size = new System.Drawing.Size(570, 21);
            cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf;
            cellStyle2.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.FURIKOMI_NAME.Style = cellStyle2;
            this.FURIKOMI_NAME.TabIndex = 1;
            this.FURIKOMI_NAME.Tag = "半角80文字以内で入力してください";
            // 
            // NyuukinsakiFurikomiDetail
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            this.Height = 42;
            this.Width = 610;

        }


        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        internal r_framework.CustomControl.GcCustomCheckBoxCell DELETE_FLG;
        internal r_framework.CustomControl.GcCustomTextBoxCell FURIKOMI_NAME;
        internal r_framework.CustomControl.GcCustomCheckBoxCell gcCustomCheckBoxCell1;
        internal r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader1;
    }
}
