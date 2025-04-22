namespace BikoSentakushiNyuryoku.MultiRowTemplate
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class BikoSentakushiNyuryokuDetail
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BikoSentakushiNyuryokuDetail));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border5 = new GrapeCity.Win.MultiRow.Border();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.columnHeaderCell1 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell2 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell3 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader1 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.BIKO_CD = new r_framework.CustomControl.GcCustomAlphaNumTextBoxCell();
            this.BIKO_NOTE = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.DELETE_FLG = new r_framework.CustomControl.GcCustomCheckBoxCell();
            this.BIKO_DEFAULT_KBN = new r_framework.CustomControl.GcCustomCheckBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.DELETE_FLG);
            this.Row.Cells.Add(this.BIKO_CD);
            this.Row.Cells.Add(this.BIKO_NOTE);
            this.Row.Cells.Add(this.BIKO_DEFAULT_KBN);
            this.Row.Height = 21;
            this.Row.Width = 1070;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell1);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell2);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell3);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader1);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 1070;
            // 
            // columnHeaderCell1
            // 
            this.columnHeaderCell1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell1.Location = new System.Drawing.Point(0, 0);
            this.columnHeaderCell1.Name = "columnHeaderCell1";
            this.columnHeaderCell1.Size = new System.Drawing.Size(54, 21);
            cellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle5.Border = border1;
            cellStyle5.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle5.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell1.Style = cellStyle5;
            this.columnHeaderCell1.TabIndex = 0;
            this.columnHeaderCell1.Value = "削除";
            this.columnHeaderCell1.ViewSearchItem = false;
            // 
            // columnHeaderCell2
            // 
            this.columnHeaderCell2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell2.Location = new System.Drawing.Point(125, 0);
            this.columnHeaderCell2.Name = "columnHeaderCell2";
            this.columnHeaderCell2.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell2.Size = new System.Drawing.Size(87, 21);
            cellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle6.Border = border2;
            cellStyle6.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle6.ForeColor = System.Drawing.Color.White;
            cellStyle6.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.columnHeaderCell2.Style = cellStyle6;
            this.columnHeaderCell2.TabIndex = 1;
            this.columnHeaderCell2.Value = "備考CD";
            this.columnHeaderCell2.ViewSearchItem = true;
            // 
            // columnHeaderCell3
            // 
            this.columnHeaderCell3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell3.Location = new System.Drawing.Point(212, 0);
            this.columnHeaderCell3.Name = "columnHeaderCell3";
            this.columnHeaderCell3.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell3.Size = new System.Drawing.Size(858, 21);
            cellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle7.Border = border3;
            cellStyle7.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle7.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell3.Style = cellStyle7;
            this.columnHeaderCell3.TabIndex = 2;
            this.columnHeaderCell3.Value = "備考内容";
            this.columnHeaderCell3.ViewSearchItem = true;
            // 
            // gcCustomColumnHeader1
            // 
            this.gcCustomColumnHeader1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader1.Location = new System.Drawing.Point(54, 0);
            this.gcCustomColumnHeader1.Name = "gcCustomColumnHeader1";
            this.gcCustomColumnHeader1.Size = new System.Drawing.Size(71, 21);
            cellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle8.Border = border4;
            cellStyle8.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle8.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader1.Style = cellStyle8;
            this.gcCustomColumnHeader1.TabIndex = 3;
            this.gcCustomColumnHeader1.Value = "規定値";
            this.gcCustomColumnHeader1.ViewSearchItem = false;
            // 
            // BIKO_CD
            // 
            this.BIKO_CD.ChangeUpperCase = true;
            this.BIKO_CD.CharacterLimitList = null;
            this.BIKO_CD.CharactersNumber = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.BIKO_CD.DataField = "BIKO_CD";
            this.BIKO_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.BIKO_CD.DisplayItemName = "備考CD";
            this.BIKO_CD.DisplayPopUp = null;
            this.BIKO_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BIKO_CD.FocusOutCheckMethod")));
            this.BIKO_CD.IsInputErrorOccured = false;
            this.BIKO_CD.ItemDefinedTypes = "varchar";
            this.BIKO_CD.Location = new System.Drawing.Point(125, 0);
            this.BIKO_CD.MaxLength = 3;
            this.BIKO_CD.Name = "BIKO_CD";
            this.BIKO_CD.PopupAfterExecute = null;
            this.BIKO_CD.PopupBeforeExecute = null;
            this.BIKO_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BIKO_CD.PopupSearchSendParams")));
            this.BIKO_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BIKO_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BIKO_CD.popupWindowSetting")));
            this.BIKO_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BIKO_CD.RegistCheckMethod")));
            this.BIKO_CD.Size = new System.Drawing.Size(87, 21);
            cellStyle2.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle2.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.BIKO_CD.Style = cellStyle2;
            this.BIKO_CD.TabIndex = 2;
            this.BIKO_CD.Tag = "半角3文字以内で入力してください。";
            this.BIKO_CD.ZeroPaddengFlag = true;
            // 
            // BIKO_NOTE
            // 
            this.BIKO_NOTE.CharactersNumber = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.BIKO_NOTE.CopyAutoSetControl = "";
            this.BIKO_NOTE.CopyAutoSetWithSpace = true;
            this.BIKO_NOTE.DataField = "BIKO_NOTE";
            this.BIKO_NOTE.DefaultBackColor = System.Drawing.Color.Empty;
            this.BIKO_NOTE.DisplayItemName = "備考内容";
            this.BIKO_NOTE.DisplayPopUp = null;
            this.BIKO_NOTE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BIKO_NOTE.FocusOutCheckMethod")));
            this.BIKO_NOTE.FuriganaAutoSetControl = "";
            this.BIKO_NOTE.IsInputErrorOccured = false;
            this.BIKO_NOTE.ItemDefinedTypes = "varchar";
            this.BIKO_NOTE.Location = new System.Drawing.Point(212, 0);
            this.BIKO_NOTE.MaxLength = 120;
            this.BIKO_NOTE.Name = "BIKO_NOTE";
            this.BIKO_NOTE.PopupAfterExecute = null;
            this.BIKO_NOTE.PopupBeforeExecute = null;
            this.BIKO_NOTE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BIKO_NOTE.PopupSearchSendParams")));
            this.BIKO_NOTE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BIKO_NOTE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BIKO_NOTE.popupWindowSetting")));
            this.BIKO_NOTE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BIKO_NOTE.RegistCheckMethod")));
            this.BIKO_NOTE.Size = new System.Drawing.Size(858, 21);
            cellStyle3.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            cellStyle3.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.BIKO_NOTE.Style = cellStyle3;
            this.BIKO_NOTE.TabIndex = 3;
            this.BIKO_NOTE.Tag = "全角60文字/半角120文字以内で入力してください。";
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
            this.DELETE_FLG.DBFieldsName = null;
            this.DELETE_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.DELETE_FLG.DisplayItemName = null;
            this.DELETE_FLG.ErrorMessage = null;
            this.DELETE_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DELETE_FLG.FocusOutCheckMethod")));
            this.DELETE_FLG.GetCodeMasterField = null;
            this.DELETE_FLG.IndeterminateValue = "False";
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
            this.DELETE_FLG.Size = new System.Drawing.Size(54, 21);
            cellStyle1.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle1.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.DELETE_FLG.Style = cellStyle1;
            this.DELETE_FLG.TabIndex = 0;
            this.DELETE_FLG.Tag = "削除する場合にはチェックを付けてください";
            this.DELETE_FLG.Value = false;
            this.DELETE_FLG.ZeroPaddengFlag = false;
            // 
            // BIKO_DEFAULT_KBN
            // 
            this.BIKO_DEFAULT_KBN.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.BIKO_DEFAULT_KBN.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.BIKO_DEFAULT_KBN.DataField = "BIKO_DEFAULT_KBN";
            this.BIKO_DEFAULT_KBN.DBFieldsName = null;
            this.BIKO_DEFAULT_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.BIKO_DEFAULT_KBN.DisplayItemName = null;
            this.BIKO_DEFAULT_KBN.ErrorMessage = null;
            this.BIKO_DEFAULT_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BIKO_DEFAULT_KBN.FocusOutCheckMethod")));
            this.BIKO_DEFAULT_KBN.GetCodeMasterField = null;
            this.BIKO_DEFAULT_KBN.IndeterminateValue = "False";
            this.BIKO_DEFAULT_KBN.IsInputErrorOccured = false;
            this.BIKO_DEFAULT_KBN.ItemDefinedTypes = null;
            this.BIKO_DEFAULT_KBN.Location = new System.Drawing.Point(54, 0);
            this.BIKO_DEFAULT_KBN.Name = "BIKO_DEFAULT_KBN";
            this.BIKO_DEFAULT_KBN.PopupAfterExecute = null;
            this.BIKO_DEFAULT_KBN.PopupBeforeExecute = null;
            this.BIKO_DEFAULT_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BIKO_DEFAULT_KBN.PopupSearchSendParams")));
            this.BIKO_DEFAULT_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BIKO_DEFAULT_KBN.PopupWindowName = null;
            this.BIKO_DEFAULT_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BIKO_DEFAULT_KBN.popupWindowSetting")));
            this.BIKO_DEFAULT_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BIKO_DEFAULT_KBN.RegistCheckMethod")));
            this.BIKO_DEFAULT_KBN.SearchDisplayFlag = 0;
            this.BIKO_DEFAULT_KBN.SetFormField = null;
            this.BIKO_DEFAULT_KBN.ShortItemName = null;
            this.BIKO_DEFAULT_KBN.Size = new System.Drawing.Size(71, 21);
            cellStyle4.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle4.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.BIKO_DEFAULT_KBN.Style = cellStyle4;
            this.BIKO_DEFAULT_KBN.TabIndex = 1;
            this.BIKO_DEFAULT_KBN.Tag = "規定値の場合、チェックをしてください。";
            this.BIKO_DEFAULT_KBN.Value = false;
            this.BIKO_DEFAULT_KBN.ZeroPaddengFlag = false;
            // 
            // BikoSentakushiNyuryokuDetail
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            cellStyle9.BackColor = System.Drawing.Color.Transparent;
            cellStyle9.BackgroundGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            border5.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Silver);
            cellStyle9.Border = border5;
            cellStyle9.DisabledBackColor = System.Drawing.SystemColors.Control;
            cellStyle9.DisabledForeColor = System.Drawing.SystemColors.GrayText;
            cellStyle9.DisabledGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle9.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            cellStyle9.Format = "";
            cellStyle9.GradientDirection = GrapeCity.Win.MultiRow.GradientDirection.Center;
            cellStyle9.GradientStyle = GrapeCity.Win.MultiRow.GradientStyle.None;
            cellStyle9.ImageAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            cellStyle9.ImeSentenceMode = GrapeCity.Win.MultiRow.ImeSentenceMode.NoControl;
            cellStyle9.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle9.LineAdjustment = GrapeCity.Win.MultiRow.LineAdjustment.None;
            cellStyle9.Margin = new System.Windows.Forms.Padding(0);
            cellStyle9.MouseOverGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle9.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle9.Padding = new System.Windows.Forms.Padding(0);
            cellStyle9.PatternColor = System.Drawing.SystemColors.WindowText;
            cellStyle9.PatternStyle = GrapeCity.Win.MultiRow.MultiRowHatchStyle.None;
            cellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle9.SelectionGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle9.TextAdjustment = GrapeCity.Win.MultiRow.TextAdjustment.Near;
            cellStyle9.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle9.TextAngle = 0F;
            cellStyle9.TextEffect = GrapeCity.Win.MultiRow.TextEffect.Flat;
            cellStyle9.TextImageRelation = GrapeCity.Win.MultiRow.MultiRowTextImageRelation.Overlay;
            cellStyle9.TextIndent = 0;
            cellStyle9.TextVertical = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle9.UseCompatibleTextRendering = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle9.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            this.DefaultCellStyle = cellStyle9;
            this.Height = 42;
            this.Width = 1070;

        }


        #endregion

        public GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        internal r_framework.CustomControl.GcCustomCheckBoxCell DELETE_FLG;
        internal r_framework.CustomControl.GcCustomAlphaNumTextBoxCell BIKO_CD;
        internal r_framework.CustomControl.GcCustomTextBoxCell BIKO_NOTE;
        internal r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell1;
        internal r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell2;
        internal r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell3;
        internal r_framework.CustomControl.GcCustomCheckBoxCell BIKO_DEFAULT_KBN;
        internal r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader1;
    }
}
